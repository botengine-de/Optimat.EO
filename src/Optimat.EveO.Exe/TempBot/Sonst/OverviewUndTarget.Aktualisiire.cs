using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Optimat.ScpezEveOnln;
using Bib3;
using Optimat.EveOnline.Base;
//using Optimat.EveOnline.AuswertGbs;
using Optimat.EveOnline.Anwendung.AuswertGbs;
using ExtractFromOldAssembly.Bib3;

namespace Optimat.EveOnline.Anwendung
{
	/// <summary>
	/// Samelt Info über Menge InRaumObjekt aus WindowOverview und LayerTarget.
	/// Ship Class: Frigate/Destroyer/Cruiser/Battlecruiser/Battleship/....
	/// EWar: Jam,WarpScramble,Web
	/// </summary>
	public partial class SictOverviewUndTargetZuusctand
	{
		/// <summary>
		/// 2014.09.14	Bsp:
		/// "Asteroid ( Concentrated Veldspar )"
		/// "Asteroid(Veldspar)"
		/// </summary>
		const string TargetBescriftungIstAsteroidRegexPattern = "Asteroid.*[^\\d\\w\\s]+([\\d\\w\\s]+)";

		static readonly public KeyValuePair<OverviewPresetDefaultTyp, SictOverviewObjektGrupeEnum[]>[] Manuel_MengeZuOverviewDefaultMengeObjektGrupeSictbar =
			new KeyValuePair<OverviewPresetDefaultTyp, SictOverviewObjektGrupeEnum[]>[]{
			new KeyValuePair<OverviewPresetDefaultTyp,SictOverviewObjektGrupeEnum[]>(
		OverviewPresetDefaultTyp.General,
		new SictOverviewObjektGrupeEnum[]{
		SictOverviewObjektGrupeEnum.Rat,
		SictOverviewObjektGrupeEnum.AccelerationGate,
		SictOverviewObjektGrupeEnum.CargoContainer,
		SictOverviewObjektGrupeEnum.SpawnContainer,
		}),
			new KeyValuePair<OverviewPresetDefaultTyp,SictOverviewObjektGrupeEnum[]>(
		OverviewPresetDefaultTyp.Loot,
		new SictOverviewObjektGrupeEnum[]{
		SictOverviewObjektGrupeEnum.CargoContainer,
		SictOverviewObjektGrupeEnum.SpawnContainer,
		SictOverviewObjektGrupeEnum.Wreck,
		}),
			new KeyValuePair<OverviewPresetDefaultTyp,SictOverviewObjektGrupeEnum[]>(
		OverviewPresetDefaultTyp.Mining,
		new SictOverviewObjektGrupeEnum[]{
		SictOverviewObjektGrupeEnum.Asteroid,
		SictOverviewObjektGrupeEnum.CargoContainer,
		}),};

		static public KeyValuePair<OverviewPresetDefaultTyp, SictOverviewObjektGrupeEnum[]>[] MengeZuOverviewDefaultMengeObjektGrupeSictbarBerecne()
		{
			return
				new KeyValuePair<OverviewPresetDefaultTyp, SictOverviewObjektGrupeEnum[]>[]{
					new KeyValuePair<OverviewPresetDefaultTyp, SictOverviewObjektGrupeEnum[]>(
						OverviewPresetDefaultTyp.All, Enum.GetValues(typeof(SictOverviewObjektGrupeEnum)).Cast<SictOverviewObjektGrupeEnum>().ToArray())
				}.Concat(
				Manuel_MengeZuOverviewDefaultMengeObjektGrupeSictbar)
				.ToArray();
		}

		static readonly KeyValuePair<OverviewPresetDefaultTyp, SictOverviewObjektGrupeEnum[]>[] MengeZuOverviewDefaultMengeObjektGrupeSictbar =
			MengeZuOverviewDefaultMengeObjektGrupeSictbarBerecne();

		static public SictOverviewObjektGrupeEnum[] ZuOverviewDefaultMengeObjektGrupeSictbar(
			OverviewPresetDefaultTyp PresetDefault)
		{
			return
				MengeZuOverviewDefaultMengeObjektGrupeSictbar.FirstOrDefault((Kandidaat) => Kandidaat.Key == PresetDefault).Value;
		}

		static KeyValuePair<string, OverviewPresetDefaultTyp>[] MengeZuTabNameRegexPatternOverviewDefaultSctandardBerecne()
		{
			var ListeDefaultTypBerüksictigt = new OverviewPresetDefaultTyp[]{
				OverviewPresetDefaultTyp.General,
				OverviewPresetDefaultTyp.Mining,
				OverviewPresetDefaultTyp.Loot,
				OverviewPresetDefaultTyp.All,
				OverviewPresetDefaultTyp.Drones,
				OverviewPresetDefaultTyp.WarpTo,
				OverviewPresetDefaultTyp.PvP};

			return
				ListeDefaultTypBerüksictigt
				.Select((DefaultTyp) =>
					new KeyValuePair<string, OverviewPresetDefaultTyp>(Bib3.FCL.Glob.RegexPatternStringGlaicwertig(DefaultTyp.ToString()), DefaultTyp))
				.Concat(
				ListeDefaultTypBerüksictigt.Select((DefaultTyp) =>
					new KeyValuePair<string, OverviewPresetDefaultTyp>(DefaultTyp.ToString(), DefaultTyp)))
				.ToArray();
		}

		readonly static KeyValuePair<string, OverviewPresetDefaultTyp>[] MengeZuTabNameRegexPatternOverviewDefault =
			MengeZuTabNameRegexPatternOverviewDefaultSctandardBerecne()
			.Concat(
			new KeyValuePair<string, OverviewPresetDefaultTyp>[]{
			new KeyValuePair<string,    OverviewPresetDefaultTyp>("default", OverviewPresetDefaultTyp.General),
			new KeyValuePair<string,    OverviewPresetDefaultTyp>("combat", OverviewPresetDefaultTyp.General),
			new KeyValuePair<string,    OverviewPresetDefaultTyp>("wreck", OverviewPresetDefaultTyp.Loot),
			new KeyValuePair<string,    OverviewPresetDefaultTyp>("Asteroid", OverviewPresetDefaultTyp.Mining),
			new KeyValuePair<string,    OverviewPresetDefaultTyp>("roid", OverviewPresetDefaultTyp.Mining),
			new KeyValuePair<string,    OverviewPresetDefaultTyp>("rock", OverviewPresetDefaultTyp.Mining),
		}).ToArray();

		static public KeyValuePair<string, OverviewPresetDefaultTyp>? ZuMengeTabNameBerecneFrüühesteDefault(
			string[] MengeTabName,
			OverviewPresetDefaultTyp[] MengePresetDefaultBerüksictigt)
		{
			if (null == MengeTabName)
			{
				return null;
			}

			if (null == MengePresetDefaultBerüksictigt)
			{
				return null;
			}

			foreach (var ZuTabNameRegexPatternOverviewDefault in MengeZuTabNameRegexPatternOverviewDefault)
			{
				if (null == ZuTabNameRegexPatternOverviewDefault.Key)
				{
					continue;
				}

				if (!MengePresetDefaultBerüksictigt.Contains(ZuTabNameRegexPatternOverviewDefault.Value))
				{
					continue;
				}

				foreach (var TabName in MengeTabName)
				{
					if (null == TabName)
					{
						continue;
					}

					if (Regex.Match(TabName, ZuTabNameRegexPatternOverviewDefault.Key, RegexOptions.IgnoreCase).Success)
					{
						return new KeyValuePair<string, OverviewPresetDefaultTyp>(TabName, ZuTabNameRegexPatternOverviewDefault.Value);
					}
				}
			}

			return null;
		}

		static public IEnumerable<OverviewPresetDefaultTyp> MengePresetDefaultOrdnetNaacPrioSctaatisc(
			IEnumerable<OverviewPresetDefaultTyp> MengeObjGrupe)
		{
			if (null == MengeObjGrupe)
			{
				yield break;
			}

			foreach (var ObjGrupe in ListePresetDefaultPrioSctaatisc)
			{
				if (MengeObjGrupe.Contains(ObjGrupe))
				{
					yield return ObjGrupe;
				}
			}
		}

		static public OverviewPresetDefaultTyp[] ListePresetDefaultPrioSctaatisc = new OverviewPresetDefaultTyp[]{
			OverviewPresetDefaultTyp.General,
			OverviewPresetDefaultTyp.All,
			OverviewPresetDefaultTyp.Loot,
			OverviewPresetDefaultTyp.Drones,
			OverviewPresetDefaultTyp.PvP,
			OverviewPresetDefaultTyp.WarpTo,
			OverviewPresetDefaultTyp.Mining,
		};

		static public OverviewPresetDefaultTyp[] ListePresetDefaultPrioFüleAufMitNaacrangige(
			OverviewPresetDefaultTyp[] ListePresetDefaultPrio,
			int LängeSol)
		{
			ListePresetDefaultPrio = ListePresetDefaultPrio ?? new OverviewPresetDefaultTyp[0];

			var ListeNaacrang =
				ListePresetDefaultPrioSctaatisc
				.Except(ListePresetDefaultPrio)
				.Take(LängeSol - (int)(ListePresetDefaultPrio.CountNullable() ?? 0))
				.ToArray();

			return
				ListePresetDefaultPrio
				.Concat(ListeNaacrang)
				.ToArray();
		}

		static public KeyValuePair<string, OverviewPresetDefaultTyp>[] MengeZuTabNameDefaultBerecne(
			string[] MengeTabName,
			OverviewPresetDefaultTyp[] ListePresetDefaultPrio)
		{
			if (null == MengeTabName)
			{
				return null;
			}

			MengeTabName =
				MengeTabName
				.WhereNotDefault()
				.Distinct(new Bib3.StringEqualityComparerCaseInsensitive()).ToArray();

			var ListePresetDefaultPrioMacbar =
				ListePresetDefaultPrio
				.TakeNullable(MengeTabName.Length)
				.ToArrayNullable();

			var MengeZuTabNameRegexPatternOverviewDefaultPrio =
				null == ListePresetDefaultPrio ? null :
				MengeZuTabNameRegexPatternOverviewDefault
				.Where((Kandidaat) => ListePresetDefaultPrioMacbar.Contains(Kandidaat.Value))
				.ToArray();

			var MengeZuTabNameDefault = new List<KeyValuePair<string, OverviewPresetDefaultTyp>>();

			do
			{
				var MengeTabNameÜbrig = MengeTabName.Except(MengeZuTabNameDefault.Keys()).ToArray();
				var ListePresetDefaultPrioÜbrig = ListePresetDefaultPrioMacbar.Except(MengeZuTabNameDefault.Values()).ToArray();

				if (MengeTabNameÜbrig.NullOderLeer() || ListePresetDefaultPrioÜbrig.NullOderLeer())
				{
					break;
				}

				var Match = ZuMengeTabNameBerecneFrüühesteDefault(MengeTabNameÜbrig, ListePresetDefaultPrioÜbrig);

				if (Match.HasValue)
				{
					MengeZuTabNameDefault.Add(Match.Value);
				}
				else
				{
					MengeZuTabNameDefault.Add(new KeyValuePair<string, OverviewPresetDefaultTyp>(MengeTabNameÜbrig.First(), ListePresetDefaultPrioÜbrig.First()));
				}
			} while (true);

			return MengeZuTabNameDefault.ToArray();
		}

		static public bool TargetBescriftungIstAsteroid(
			SictTargetZuusctand Target,
			out string OreTypSictString,
			out OreTypSictEnum? OreTyp)
		{
			OreTypSictString = null;
			OreTyp = null;

			if (null == Target)
			{
				return false;
			}

			var TargetBescriftungOoberhalbDistanceListeZaile = Target.OoberhalbDistanceListeZaile;

			if (null == TargetBescriftungOoberhalbDistanceListeZaile)
			{
				return false;
			}

			var TargetBescriftungAgregiirt =
				string.Join(" ", TargetBescriftungOoberhalbDistanceListeZaile);

			var TargetBescriftungAgregiirtOoneXml = ExtractFromOldAssembly.Bib3.Glob.TrimNullable(Optimat.Glob.StringEntferneMengeXmlTag(TargetBescriftungAgregiirt));

			var Match = Regex.Match(
				TargetBescriftungAgregiirtOoneXml, TargetBescriftungIstAsteroidRegexPattern, RegexOptions.IgnoreCase);

			if (!Match.Success)
			{
				return false;
			}

			OreTypSictString = Match.Groups[1].Value.Trim();

			OreTyp = TempAuswertGbs.Extension.OreTypBerecneAusOreTypSictString(OreTypSictString);

			return true;
		}

		public void AktualisiireZuusctandAusScnapscus(
			SictAutomatZuusctand AutomaatZuusctand,
			SictAusGbsScnapscusAuswertungSrv AusScnapscusAuswertungZuusctand)
		{
			if (null == AusScnapscusAuswertungZuusctand)
			{
				return;
			}

			if (null == AutomaatZuusctand)
			{
				return;
			}

			var ZaitMili = AutomaatZuusctand.NuzerZaitMili;

			var MengeOverViewObjekt = this.MengeOverViewObjekt;
			var MengeTarget = this.MengeTarget;

			VonSensor.WindowOverView WindowOverviewScnapscusLezte = null;
			VonSensor.WindowOverView WindowOverviewScnapscusVorLezte = null;
			SictOverViewObjektZuusctand AusOverviewObjektInputFookusExklusiiv = null;
			KeyValuePair<SictWindowInventoryVerknüpfungMitOverview, SictOverViewObjektZuusctand[]>[] MengeZuWindowInventoryAuswaalReczMengeKandidaatInRaumObjekt = null;
			string OverviewTabAktiivBezaicner = null;
			KeyValuePair<string, SictOverviewObjektGrupeEnum[]>? OverviewPresetAktiivIdentUndMengeObjektGrupeSictbar = null;
			SictZuInRaumObjektReprScnapscusZuusazinfo ZuInRaumObjektScnapscusZuusazInfo = null;
			string[] MengeOverviewPresetFeelend = null;
			bool? SortedNaacDistanceNict = null;

			try
			{
				if (null == MengeOverViewObjekt)
				{
					this.MengeOverViewObjekt = MengeOverViewObjekt = new List<SictOverViewObjektZuusctand>();
				}

				if (null == MengeTarget)
				{
					this.MengeTarget = MengeTarget = new List<SictTargetZuusctand>();
				}

				var Gbs = AutomaatZuusctand.Gbs;
				var FittingUndShipZuusctand = AutomaatZuusctand.FittingUndShipZuusctand;

				var DockedLezteZaitMili = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.DockedLezteZaitMili;

				var DockedLezteAlterMili = ZaitMili - DockedLezteZaitMili;

				var GbsMenuKaskaadeLezteNocOfe = (null == Gbs) ? null : Gbs.MenuKaskaadeLezteNocOfe;
				var GbsMengeWindow = (null == Gbs) ? null : Gbs.MengeWindow;

				var GbsMenuKaskaadeLezteListeMenu = (null == GbsMenuKaskaadeLezteNocOfe) ? null : GbsMenuKaskaadeLezteNocOfe.ListeMenu;
				var GbsMenuKaskaadeLezteBeginZait = (null == GbsMenuKaskaadeLezteNocOfe) ? null : GbsMenuKaskaadeLezteNocOfe.BeginZait;

				var OptimatParam = (null == AutomaatZuusctand) ? null : AutomaatZuusctand.OptimatParam();

				var OverViewMengeZuOverviewPresetIdentMengeObjektGrupeSictbar =
					MengeZuOverviewDefaultMengeObjektGrupeSictbar
					.Select((DefaultUndMengeObjGrupeSictbar) => new KeyValuePair<string, SictOverviewObjektGrupeEnum[]>(
						DefaultUndMengeObjGrupeSictbar.Key.ToString(),
						DefaultUndMengeObjGrupeSictbar.Value))
					.ToArray();

				WindowOverviewScnapscusLezte = AusScnapscusAuswertungZuusctand.WindowOverview;

				var WindowOverviewHerkunftAdrese = (null == WindowOverviewScnapscusLezte) ? 0 : WindowOverviewScnapscusLezte.Ident;
				var WindowOverviewScnapscusLeztePresetIdent = (null == WindowOverviewScnapscusLezte) ? null : WindowOverviewScnapscusLezte.OverviewPresetIdent;

				var GbsWindowOverview =
					ExtractFromOldAssembly.Bib3.Extension.FirstOrDefaultNullable(
					GbsMengeWindow,
					(Kandidaat) => Kandidaat.GbsAstHerkunftAdrese == WindowOverviewHerkunftAdrese);

				WindowOverviewScnapscusVorLezte =
					(null == GbsWindowOverview) ? null : GbsWindowOverview.AingangScnapscusTailObjektIdentVorLezteBerecne() as VonSensor.WindowOverView;

				var WindowOverviewScnapscusLezteMengeTab = (null == WindowOverviewScnapscusLezte) ? null : WindowOverviewScnapscusLezte.ListeTabNuzbar;
				var WindowOverviewScnapscusVorLezteMengeTab = (null == WindowOverviewScnapscusVorLezte) ? null : WindowOverviewScnapscusVorLezte.ListeTabNuzbar;

				var ScnapscusMengeTargetRepr = AusScnapscusAuswertungZuusctand.MengeTarget;

				var WindowOverviewTabSelected =
					(null == WindowOverviewScnapscusLezte) ? null : WindowOverviewScnapscusLezte.TabSelected;

				var WindowOverviewPresetIdent = (null == WindowOverviewScnapscusLezte) ? null : WindowOverviewScnapscusLezte.OverviewPresetIdent;

				OverviewTabAktiivBezaicner =
					(null == WindowOverviewTabSelected) ? null :
					((null == WindowOverviewTabSelected.Label) ? null :
					WindowOverviewTabSelected.Label.Bescriftung);

				var ScnapscusVorLezteWindowOverViewMitZait = this.ScnapscusLezteWindowOverViewMitZait;

				var ScnapscusVorLezteWindowOverViewTabSelected =
					(null == ScnapscusVorLezteWindowOverViewMitZait.Wert) ? null :
					ScnapscusVorLezteWindowOverViewMitZait.Wert.TabSelected;

				var ScnapscusVorLezteWindowOverViewTabSelectedName =
					(null == ScnapscusVorLezteWindowOverViewTabSelected) ? null :
					((null == ScnapscusVorLezteWindowOverViewTabSelected.Label) ? null :
					ScnapscusVorLezteWindowOverViewTabSelected.Label.Bescriftung);

				if (null != WindowOverviewScnapscusLezte)
				{
					var WindowOverviewScnapscusLezteScroll = WindowOverviewScnapscusLezte.Scroll;

					if (null != WindowOverviewScnapscusLezteScroll)
					{
						if (!(WindowOverviewScnapscusLezteScroll.ScrollHandleAntailAnGesamtMili < 990) ||
							WindowOverviewScnapscusLezte.ExtraktScrollHandleFläceGrenzeOobenAntailAnGesamtMili() < 3)
						{
							this.OverViewScrolledToTopLezteZait = ZaitMili;
						}
					}

					if (InternListeZuZaitMengeZaileAbwaicendZuSortedDistanceAnzaal.LastOrDefault().Zait < ZaitMili)
					{
						InternListeZuZaitMengeZaileAbwaicendZuSortedDistanceAnzaal.Enqueue(
							new SictWertMitZait<int>(ZaitMili, WindowOverviewScnapscusLezte.MengeZaileAnzaalLaageAbwaicendVonSortedDistanceAufsctaigend ?? -1));

						InternListeZuZaitMengeZaileAbwaicendZuSortedDistanceAnzaal.ListeKürzeBegin(4);
					}

					var ListeZuZaitMengeZaileAbwaicendZuSortedDistanceAnzaalBerüksictigt =
						ListeZuZaitMengeZaileAbwaicendZuSortedDistanceAnzaal
						.SkipWhile((ZuZaitMengeZaileAbwaicendZuSortedDistanceAnzaal) => 11111 < Math.Abs(ZaitMili - ZuZaitMengeZaileAbwaicendZuSortedDistanceAnzaal.Zait))
						.Reversed()
						.Take(3)
						.ToArray();

					if (2 < ListeZuZaitMengeZaileAbwaicendZuSortedDistanceAnzaalBerüksictigt.Length)
					{
						SortedNaacDistanceNict =
							ListeZuZaitMengeZaileAbwaicendZuSortedDistanceAnzaalBerüksictigt
							.All((ZuZaitMengeZaileAbwaicendZuSortedDistanceAnzaal) => 3 <= ZuZaitMengeZaileAbwaicendZuSortedDistanceAnzaal.Wert);
					}

					{
						//	Berecnung ListeAusOverviewMenuLoadPreset

						if (DockedLezteAlterMili < 4444)
						{
							ListeAusOverviewMenuLoadPresetListeEntry.Clear();
						}

						if (1 < ExtractFromOldAssembly.Bib3.Extension.CountNullable(GbsMenuKaskaadeLezteListeMenu) &&
							GbsMenuKaskaadeLezteBeginZait.HasValue)
						{
							var AintraagZuMenuKaskaadeBeraitsVorhande = ListeAusOverviewMenuLoadPresetListeEntry.Any((Kandidaat) => Kandidaat.Zait == GbsMenuKaskaadeLezteBeginZait);

							var GbsMenuKaskaadeLezteListeMenuFrüheste = GbsMenuKaskaadeLezteListeMenu.FirstOrDefault();
							var GbsMenuKaskaadeLezteListeMenuErste = GbsMenuKaskaadeLezteListeMenu.ElementAtOrDefault(1);

							if (null != GbsMenuKaskaadeLezteListeMenuFrüheste &&
								null != GbsMenuKaskaadeLezteListeMenuErste &&
								!AintraagZuMenuKaskaadeBeraitsVorhande)
							{
								var GbsMenuKaskaadeLezteListeMenuErsteListeEntry = GbsMenuKaskaadeLezteListeMenuErste.ListeEntryBerecne();

								var AnnaameMenuEntryAktiiv = GbsMenuKaskaadeLezteListeMenuFrüheste.AnnaameMenuEntryAktiiv;

								if (null == AnnaameMenuEntryAktiiv)
								{
									AnnaameMenuEntryAktiiv = GbsMenuKaskaadeLezteListeMenuFrüheste.MenuEntryAktiviirtLezteMitZait.Wert;
								}

								if (null != AnnaameMenuEntryAktiiv)
								{
									var AnnaameMenuEntryAktiivBescriftung = AnnaameMenuEntryAktiiv.Bescriftung;

									if (null != AnnaameMenuEntryAktiivBescriftung)
									{
										if (Regex.Match(AnnaameMenuEntryAktiivBescriftung, OverviewMenuEntryLaadePresetNaacTabRegexPattern, RegexOptions.IgnoreCase).Success)
										{
											ListeAusOverviewMenuLoadPresetListeEntry.Add(new SictWertMitZait<VonSensor.MenuEntry[]>(
												GbsMenuKaskaadeLezteBeginZait.Value, GbsMenuKaskaadeLezteListeMenuErsteListeEntry));

											Bib3.Extension.ListeKürzeBegin(ListeAusOverviewMenuLoadPresetListeEntry, 3);
										}
									}
								}
							}
						}
					}

					var ListeAusOverviewMenuLoadPresetListeEntryTailmenge =
						ListeAusOverviewMenuLoadPresetListeEntry
						.OrderByDescending((Kandidaat) => Kandidaat.Zait)
						.Take(2)
						.ToArray();

					if (null != WindowOverviewScnapscusLezteMengeTab &&
						null != WindowOverviewScnapscusVorLezteMengeTab)
					{
						MengeOverviewPresetFeelend = null;

						if (1 < ListeAusOverviewMenuLoadPresetListeEntryTailmenge.Length)
						{
							MengeOverviewPresetFeelend =
							ListePresetDefaultPrioSctaatisc
							.Select((Preset) => Preset.ToString())
							.Where((ErwartungPreset) =>
								!ListeAusOverviewMenuLoadPresetListeEntryTailmenge.Any((MesungListePresetMenuEntry) =>
									VonSensor.MenuEntry.MengeEntryEnthaltEntryMitBescriftung(MesungListePresetMenuEntry.Wert, ErwartungPreset, RegexOptions.IgnoreCase)))
									.ToArray();
						}
					}

					var WindowOverviewMitZait = new SictWertMitZait<VonSensor.WindowOverView>(ZaitMili, WindowOverviewScnapscusLezte);

					//	Berecnung DictZuOverviewPresetNameAnnaameFolgeVolsctändig

					var OverviewPresetFolgeViewportAktuel = this.OverviewPresetFolgeViewportAktuel;

					var DictZuOverviewPresetNameFolgeViewportVolsctändig = this.DictZuOverviewPresetNameFolgeViewportVolsctändig;

					try
					{
						if (null == OverviewPresetFolgeViewportAktuel)
						{
							OverviewPresetFolgeViewportAktuel = new SictOverviewPresetFolgeViewport(WindowOverviewMitZait);
						}
						else
						{
							OverviewPresetFolgeViewportAktuel.AingangNääxte(WindowOverviewMitZait);
						}

						{
							if (true == OverviewPresetFolgeViewportAktuel.Fertig ||
								true == OverviewPresetFolgeViewportAktuel.VolsctändigTailInGrid)
							{
								this.OverviewPresetFolgeViewportFertigLezte = OverviewPresetFolgeViewportAktuel;

								if (true == OverviewPresetFolgeViewportAktuel.VolsctändigTailInGrid)
								{
									if (null == DictZuOverviewPresetNameFolgeViewportVolsctändig)
									{
										DictZuOverviewPresetNameFolgeViewportVolsctändig = new Dictionary<string, SictOverviewPresetFolgeViewport>();
									}

									DictZuOverviewPresetNameFolgeViewportVolsctändig[OverviewPresetFolgeViewportAktuel.OverviewPresetName] =
										OverviewPresetFolgeViewportAktuel;

									if (null != OverViewMengeZuOverviewPresetIdentMengeObjektGrupeSictbar)
									{
										var ZuOverviewPresetMengeMengeObjektGrupe =
											OverViewMengeZuOverviewPresetIdentMengeObjektGrupeSictbar
											.Where((Kandidaat) => string.Equals(Kandidaat.Key, OverviewPresetFolgeViewportAktuel.OverviewPresetName,
												StringComparison.InvariantCultureIgnoreCase))
											.ToArray();

										var ZuOverviewPresetMengeObjektGrupe =
											Bib3.Glob.ArrayAusListeFeldGeflact(
											ZuOverviewPresetMengeMengeObjektGrupe
											.Select((Kandidaat) => Kandidaat.Value));

										if (null == DictZuOverviewObjektGrupeFolgeViewportVolsctändig)
										{
											DictZuOverviewObjektGrupeFolgeViewportVolsctändig = new Dictionary<SictOverviewObjektGrupeEnum, SictOverviewPresetFolgeViewport>();
										}

										foreach (var OverviewObjektGrupe in ZuOverviewPresetMengeObjektGrupe)
										{
											DictZuOverviewObjektGrupeFolgeViewportVolsctändig[OverviewObjektGrupe] = OverviewPresetFolgeViewportAktuel;
										}
									}

									this.DictZuOverviewPresetNameFolgeViewportVolsctändig = DictZuOverviewPresetNameFolgeViewportVolsctändig;
									this.DictZuOverviewObjektGrupeFolgeViewportVolsctändig = DictZuOverviewObjektGrupeFolgeViewportVolsctändig;
								}

								OverviewPresetFolgeViewportAktuel = new SictOverviewPresetFolgeViewport(WindowOverviewMitZait);
							}
						}
					}
					finally
					{
						this.OverviewPresetFolgeViewportAktuel = OverviewPresetFolgeViewportAktuel;
					}
				}

				var ShipUi = AusScnapscusAuswertungZuusctand.ShipUi;

				var MengeZuEWarTypeTextureIdent = default(KeyValuePair<SictEWarTypeEnum, Int64>[]);

				if (null != ShipUi)
				{
					var ShipUiMengeEWarElement = ShipUi.MengeEWarElement;

					if (null != ShipUiMengeEWarElement)
					{
						MengeZuEWarTypeTextureIdent =
							ShipUiMengeEWarElement.Select((EWarElement) => new KeyValuePair<SictEWarTypeEnum?, Int64?>(
								EWarElement.EWarTypeEnum, EWarElement.IconTextureIdent))
							.Where((Kandidaat) => Kandidaat.Key.HasValue && Kandidaat.Value.HasValue)
							.Select((Kandidaat) => new KeyValuePair<SictEWarTypeEnum, Int64>(Kandidaat.Key.Value, Kandidaat.Value.Value))
							.ToArray();
					}
				}

				if (true == AusScnapscusAuswertungZuusctand.Docked())
				{
					MengeOverViewObjekt.Clear();
					MengeTarget.Clear();
				}

				/*
				* entferne der Objekte werd vor ainfüüge vorgenome damit auc solce OverviewZaile für welce di Bedingunge zum entferne beraits vorliige
				* am Ende der Funktioon in ainem InRaumObjektZuusctand repräsentiirt sind.
				* */

				var InRaumObjektSictungLezteZaitScrankeMin = ZaitMili - OverviewObjektSictungLezteAlterScrankeMax * 1000;

				MengeObjektEntferneNictMeerGesictete(InRaumObjektSictungLezteZaitScrankeMin);

				var VersuucMenuEntryKlikLezteMitZaitNulbar = AutomaatZuusctand.VersuucMenuEntryKlikLezteBerecne();

				var VersuucMenuEntryKlikLezteListeMenuEntry = VersuucMenuEntryKlikLezteMitZaitNulbar.HasValue ?
					VersuucMenuEntryKlikLezteMitZaitNulbar.Value.Wert.Value : null;

				var VersuucMenuEntryKlikLezteListeAstLezteEntry =
					(null == VersuucMenuEntryKlikLezteListeMenuEntry) ? null : VersuucMenuEntryKlikLezteListeMenuEntry.LastOrDefault();

				ZuInRaumObjektScnapscusZuusazInfo = new SictZuInRaumObjektReprScnapscusZuusazinfo(
					WindowOverviewScnapscusLeztePresetIdent,
					MengeZuEWarTypeTextureIdent,
					GbsMenuKaskaadeLezteNocOfe,
					VersuucMenuEntryKlikLezteListeAstLezteEntry,
					OverviewPresetFolgeViewportFertigLezte,
					this.DictZuOverviewPresetNameFolgeViewportVolsctändig);

				VonSensor.OverviewZaile[] WindowOverviewAusTabListeZaile = null;

				if (null != WindowOverviewScnapscusLezte)
				{
					if (WindowOverviewScnapscusLezte.PresetAlsDefaultTyp.HasValue)
					{
						WindowOverviewAusTabListeZaile =
							WindowOverviewScnapscusLezte.AusTabListeZaileOrdnetNaacLaage;
					}
				}

				var WindowOverviewAusTabListeZaileBeraitsVerarbaitet = new List<VonSensor.OverviewZaile>();

				foreach (var OverViewObjekt in MengeOverViewObjekt)
				{
					if (null == OverViewObjekt)
					{
						continue;
					}

					var ZuObjektOverViewZaile =
						(null == WindowOverviewAusTabListeZaile) ? null :
						WindowOverviewAusTabListeZaile
						.FirstOrDefault((KandidaatZaile) => OverViewObjekt.PasendZuBisherige(KandidaatZaile));

					if (null != ZuObjektOverViewZaile)
					{
						WindowOverviewAusTabListeZaileBeraitsVerarbaitet.Add(ZuObjektOverViewZaile);
					}

					if (null == ZuObjektOverViewZaile)
					{
						OverViewObjekt.AingangScnapscusLeer(ZaitMili, ZuInRaumObjektScnapscusZuusazInfo);
					}
					else
					{
						OverViewObjekt.AingangScnapscus(
							ZaitMili,
							ZuObjektOverViewZaile,
							ZuInRaumObjektScnapscusZuusazInfo);
					}
				}

				if (null != WindowOverviewAusTabListeZaile)
				{
					var WindowOverviewAusTabListeZaileNocNitVerarbaitet =
						WindowOverviewAusTabListeZaile.Except(WindowOverviewAusTabListeZaileBeraitsVerarbaitet).ToArray();

					//	Zuordnung der Overview Zaile zu InRaumObjekt

					foreach (var AusOverviewZaile in WindowOverviewAusTabListeZaileNocNitVerarbaitet)
					{
						if (null == AusOverviewZaile)
						{
							continue;
						}

						/*
						 * 2015.02.17
						 * 
						var OverviewScrollEntryHerkunftAdrese = ZaileAusOverviewZaile.OverviewScrollEntryHerkunftAdrese;

						if (!OverviewScrollEntryHerkunftAdrese.HasValue)
						{
							continue;
						}
						 * */

						var InRaumObjekt = new SictOverViewObjektZuusctand(
							ZaitMili,
							AusOverviewZaile,
							ZuInRaumObjektScnapscusZuusazInfo);

						MengeOverViewObjekt.Add(InRaumObjekt);
					}
				}

				AusOverviewObjektInputFookusExklusiiv =
					(null == WindowOverviewScnapscusLezte) ? null :
					MengeOverViewObjekt.FirstOrDefault((Kandidaat) =>
						WindowOverviewScnapscusLezte.ZaileMitInputFookusExklusiiv() == Kandidaat.AingangScnapscusTailObjektIdentLezteBerecne());

				SictTargetZuusctand.ZuZaitAingangMengeObjektScnapscus(
					ZaitMili,
					ScnapscusMengeTargetRepr,
					MengeTarget,
					true,
					ZuInRaumObjektScnapscusZuusazInfo);

				MengeTarget.RemoveAll((Kandidaat) => !(Kandidaat.SaitAingangUnglaicDefaultLezteListeAingangAnzaal() < 3));

				var MengeWindowInventoryVerknüpfungMitOverview = AusScnapscusAuswertungZuusctand.MengeWindowInventoryVerknüpfungMitOverview;

				if (null != MengeWindowInventoryVerknüpfungMitOverview)
				{
					MengeZuWindowInventoryAuswaalReczMengeKandidaatInRaumObjekt =
						MengeWindowInventoryVerknüpfungMitOverview
						.Select((WindowInventoryVerknüpfungMitOverview) =>
							{
								var ZuAuswaalReczMengeKandidaatOverviewZaile = WindowInventoryVerknüpfungMitOverview.ZuAuswaalReczMengeKandidaatOverviewZaile;

								return new KeyValuePair<SictWindowInventoryVerknüpfungMitOverview, SictOverViewObjektZuusctand[]>(
									WindowInventoryVerknüpfungMitOverview,
									(null == ZuAuswaalReczMengeKandidaatOverviewZaile) ? null :
									ZuAuswaalReczMengeKandidaatOverviewZaile
									.Select((KandidaatOverviewZaile) =>
									InRaumObjektZuusctandFürOverviewZaile(KandidaatOverviewZaile)).ToArray());
							})
						.Where((Kandidaat) => null != Kandidaat.Value)
						.ToArray();
				}
			}
			finally
			{
				this.ScnapscusLezteWindowOverViewMitZait = new SictWertMitZait<VonSensor.WindowOverView>(ZaitMili, WindowOverviewScnapscusLezte);
				this.AusOverviewObjektInputFookusExklusiiv = AusOverviewObjektInputFookusExklusiiv;
				this.MengeZuWindowInventoryAuswaalReczMengeKandidaatInRaumObjekt = MengeZuWindowInventoryAuswaalReczMengeKandidaatInRaumObjekt;
				this.MengeZuOverviewTabMengeObjektGrupeSictbar = MengeZuOverviewTabMengeObjektGrupeSictbar;
				this.OverviewTabAktiivBezaicner = OverviewTabAktiivBezaicner;
				this.OverviewPresetAktiivIdentUndMengeObjektGrupeSictbar = OverviewPresetAktiivIdentUndMengeObjektGrupeSictbar;
				this.OverviewPresetZuLaadeIdent = OverviewPresetZuLaadeIdent;
				this.ScritLezteZuInRaumObjektScnapscusZuusazInfo = ZuInRaumObjektScnapscusZuusazInfo;
				this.MengeOverviewPresetFeelend = MengeOverviewPresetFeelend;
				/*
				 * 2015.00.04
				 * 
				this.MengeOverviewTabFeelend = MengeOverviewTabFeelend;
				this.MengeOverviewPresetOoneTab = MengeOverviewPresetOoneTab;
				this.VonNuzerParamMengeOverviewPresetAbzüüglicFeelende = VonNuzerParamMengeOverviewPresetAbzüüglicFeelende;
				 * */
				this.SortedNaacDistanceNict = SortedNaacDistanceNict;
			}

			AktualisiireTailRelatioonTargetZuOverviewRow();
		}

		void AktualisiireTailRelatioonTargetZuOverviewRow()
		{
			var TargetInputFookusExklusiiv =
				ExtractFromOldAssembly.Bib3.Extension.FirstOrDefaultNullable(
				MengeTargetNocSictbar, (Kandidaat) => true == Kandidaat.InputFookusTransitioonLezteZiilWert);

			var OverviewRowInputFookusExklusiiv = this.AusOverviewObjektInputFookusExklusiiv;

			if (null != TargetInputFookusExklusiiv &&
				null != OverviewRowInputFookusExklusiiv)
			{
				var OverviewRowInputFookusExklusiivScnapscusMitZaitNulbar =
					OverviewRowInputFookusExklusiiv.AingangScnapscusTailObjektIdentMitZaitLezteBerecne();

				if (OverviewRowInputFookusExklusiiv.SaitSictbarkaitLezteListeScritAnzaal < 1 &&
					OverviewRowInputFookusExklusiivScnapscusMitZaitNulbar.HasValue)
				{
					var OverviewRowInputFookusExklusiivScnapscusMitZait = OverviewRowInputFookusExklusiivScnapscusMitZaitNulbar.Value;

					var ZaitMili = OverviewRowInputFookusExklusiivScnapscusMitZait.Zait;

					if (!object.ReferenceEquals(TargetInputFookusExklusiiv, ScritLezteTargetInputFookusExklusiiv) &&
						!object.ReferenceEquals(OverviewRowInputFookusExklusiiv, ScritLezteOverviewRowInputFookusExklusiiv))
					{
						//	Input Fookus für Target und OverviewRow wurde in lezte Scrit baide geändert.

						if (OverViewObjektTypeOderNaamePasendZuTargetBescriftung(OverviewRowInputFookusExklusiiv, TargetInputFookusExklusiiv) &&
							true == OverviewRowInputFookusExklusiivScnapscusMitZait.Wert.IconTargetedByMeSictbar)
						{
							ListeIndikatorTargetReprOverViewRow.Add(new SictWertMitZait<SictIndikatorTargetReprOverviewRow>(
								ZaitMili, new SictIndikatorTargetReprOverviewRow(TargetInputFookusExklusiiv, OverviewRowInputFookusExklusiiv)));
						}
					}
				}
			}

			Bib3.Extension.ListeKürzeBegin(ListeIndikatorTargetReprOverViewRow, 30);

			MengeAnnaameZuOverviewRowTarget.Clear();

			var GrupeZuOverviewRowListeIndikatorTargetReprOverViewRow =
				ListeIndikatorTargetReprOverViewRow.GroupBy((Kandidaat) => Kandidaat.Wert.OverviewRow).ToArray();

			foreach (var GrupeOverviewRow in GrupeZuOverviewRowListeIndikatorTargetReprOverViewRow)
			{
				var GrupeOverviewRowListeIndikator = GrupeOverviewRow.OrderBy((Kandidaat) => Kandidaat.Zait).ToArray();

				var GrupeOverviewRowListeIndikatorLezte = GrupeOverviewRowListeIndikator.LastOrDefault();
				var GrupeOverviewRowListeIndikatorVorLezte = GrupeOverviewRowListeIndikator.ElementAtOrDefault(GrupeOverviewRowListeIndikator.Length - 2);

				if (null != GrupeOverviewRowListeIndikatorLezte.Wert &&
					null != GrupeOverviewRowListeIndikatorVorLezte.Wert)
				{
					if (GrupeOverviewRowListeIndikatorLezte.Wert.Target == GrupeOverviewRowListeIndikatorVorLezte.Wert.Target)
					{
						MengeAnnaameZuOverviewRowTarget.Add(new KeyValuePair<SictOverViewObjektZuusctand, SictTargetZuusctand>(
							GrupeOverviewRow.Key, GrupeOverviewRowListeIndikatorLezte.Wert.Target));
					}
				}
			}

			this.ScritLezteTargetInputFookusExklusiiv = TargetInputFookusExklusiiv;
			this.ScritLezteOverviewRowInputFookusExklusiiv = OverviewRowInputFookusExklusiiv;
		}

		static bool OverViewEntryZuusctandErhalte(
			SictOverViewObjektZuusctand InRaumObjektZuusctand,
			Int64? InRaumObjektSictungLezteZaitScrankeMin,
			SictOverviewPresetFolgeViewport OverviewPresetFolgeViewportAktuel,
			IDictionary<string, SictOverviewPresetFolgeViewport> DictZuOverviewPresetNameFolgeViewportVolsctändig,
			int GesamtObjektGescwindigkaitScrankeMax)
		{
			if (null == InRaumObjektZuusctand)
			{
				return false;
			}

			if (InRaumObjektSictungLezteZaitScrankeMin.HasValue)
			{
				if (!(InRaumObjektSictungLezteZaitScrankeMin <= InRaumObjektZuusctand.SictungLezteZait))
				{
					return false;
				}
			}

			{
				//	Filter Änderung Type oder Name: Wen Type oder Naame in baide lezte Scnapscus versciide zu zuersct gesictete Type und Naame: Objekt entferne.

				if (!string.Equals(InRaumObjektZuusctand.SictungFrühesteType, InRaumObjektZuusctand.Type) ||
					!string.Equals(InRaumObjektZuusctand.SictungFrühesteName, InRaumObjektZuusctand.Name))
				{
					return false;
				}
			}

			if (null != OverviewPresetFolgeViewportAktuel)
			{
				var Zait = OverviewPresetFolgeViewportAktuel.WindowOverviewVorherigMitZait.Zait;

				var ObjektSictungLezteAlterNulbar = Zait - InRaumObjektZuusctand.SictungLezteZait;

				if (!ObjektSictungLezteAlterNulbar.HasValue)
				{
					return false;
				}

				var ObjektSictungLezteAlter = ObjektSictungLezteAlterNulbar.Value;

				var OverviewPresetFolgeViewportAktuelBeginObjektDistance = OverviewPresetFolgeViewportAktuel.BeginObjektDistance;
				var OverviewPresetFolgeViewportAktuelEndeObjektDistance = OverviewPresetFolgeViewportAktuel.EndeObjektDistance;

				if (OverviewPresetFolgeViewportAktuelBeginObjektDistance.HasValue &&
					OverviewPresetFolgeViewportAktuelEndeObjektDistance.HasValue &&
					InRaumObjektZuusctand.SictungLezteZait < OverviewPresetFolgeViewportAktuel.BeginZait)
				{
					var ObjektGescwindigkaitScrankeMax = GesamtObjektGescwindigkaitScrankeMax;

					if (InRaumObjektZuusctand.IstAsteroid ?? false)
					{
						ObjektGescwindigkaitScrankeMax = 1;
					}

					var ObjektZurükgeleegteSctrekeAnnaameScrankeMax = ((ObjektSictungLezteAlter + 1000) * ObjektGescwindigkaitScrankeMax) / 1000;

					var ObjektDistanzScrankeMin = InRaumObjektZuusctand.SictungLezteDistanceScrankeMinScpezOverview - ObjektZurükgeleegteSctrekeAnnaameScrankeMax;
					var ObjektDistanzScrankeMax = InRaumObjektZuusctand.SictungLezteDistanceScrankeMaxScpezOverview + ObjektZurükgeleegteSctrekeAnnaameScrankeMax;

					if (OverviewPresetFolgeViewportAktuelBeginObjektDistance < ObjektDistanzScrankeMin &&
						ObjektDistanzScrankeMax < OverviewPresetFolgeViewportAktuelEndeObjektDistance)
					{
						//	Das Objekt war sait Begin der aktuele Viewport Folge nit sictbar, konte sic aber vermuutlic inerhalb diiser Zait nict bis auserhalb des von der Viewport-Folge scon abgedekte Distanzberaic beweege.
						return false;
					}
				}
			}

			if (null != DictZuOverviewPresetNameFolgeViewportVolsctändig)
			{
				var ObjektDictZuOverviewPresetSictungLezteZait = InRaumObjektZuusctand.DictZuOverviewPresetSictungLezteZait;

				if (null == ObjektDictZuOverviewPresetSictungLezteZait)
				{
					return false;
				}

				if (ObjektDictZuOverviewPresetSictungLezteZait.Count < 1)
				{
					return false;
				}

				var ObjektMengeOverviewFolgeViewportVolsctändig =
					DictZuOverviewPresetNameFolgeViewportVolsctändig
					.Where((KandidaatFolge) => ObjektDictZuOverviewPresetSictungLezteZait
						.Any((KandidaatPresetSictung) => string.Equals(KandidaatPresetSictung.Key, KandidaatFolge.Key)))
					.ToArray();

				if (ObjektMengeOverviewFolgeViewportVolsctändig.Any((OverviewFolge) => InRaumObjektZuusctand.SictungLezteZait < OverviewFolge.Value.BeginZait))
				{
					//	Zaitpunkt der lezten Sictung des Objekt ist klainer als Begin ainer Folge welce ale Objekte in dem OverviewPreset abdekte. Daher werd davon ausgegange das Objekt nit meer im Overview sictbar.
					return false;
				}
			}

			return true;
		}

		/// <summary>
		/// Entfernt Objekte für welce aine der folgenden Bedingungen erfült:
		/// <para>
		/// - Di lezte Sictung ainer OverviewZaile zu deem Objekt ist älter als der Begin ainer Folge von Scnapscus deren zusamengefüügte Viewport aines
		/// der Overview Preset in welcem das Objekt scon gesictet wurde volsctändig abdekt.
		/// </para>
		/// <para>
		/// - Type oder Name des Objekt sind unglaic des für die jewailige Aigenscaft frühest gesicteten Wert.
		/// </para>
		/// <para>
		/// - (Das Objekt wurde sait Begin von OverviewPresetFolgeViewportAktuel nict gesictet) UND
		/// (OverviewPresetFolgeViewportAktuel dekt ainen Distanzberaic ab welcer ale unter Berüksictigung zulezt gesicteter Distanz und
		/// angenomener maximaler gescwindigkait vom Objekt in der Zwiscenzait erraicbarer Distanze umfast).
		/// - Di lezte Sictung des Objekt ist waiter zurük als InRaumObjektSictungLezteZaitScrankeMin:
		/// diis sol dafür sorgen das fals andere Mecanismen zur Erkenung von verscwinden von Objekt versaagen der Automaat nit für imer hängeblaibt
		/// </para>
		/// Di entscaidung der Sictung aines Objekt in ainem Overview Preset werd in der Klase SictInRaumObjektZuusctand vorgenome.
		/// </summary>
		void MengeObjektEntferneNictMeerGesictete(
			Int64? InRaumObjektSictungLezteZaitScrankeMin)
		{
			var MengeInRaumObjekt = this.MengeOverViewObjekt;

			var DictZuOverviewPresetNameFolgeViewportVolsctändig = this.DictZuOverviewPresetNameFolgeViewportVolsctändig;

			var OverviewPresetFolgeViewportAktuel = this.OverviewPresetFolgeViewportAktuel;

			if (null == MengeInRaumObjekt)
			{
				return;
			}

			var GesamtObjektGescwindigkaitScrankeMax = 1000;

			/*
			2015.09.03

			var MengeInRaumObjektZuErhalte = new List<SictOverViewObjektZuusctand>();

			foreach (var InRaumObjektZuusctand in MengeInRaumObjekt)
			{
				if (null == InRaumObjektZuusctand)
				{
					continue;
				}

				if (InRaumObjektSictungLezteZaitScrankeMin.HasValue)
				{
					if (!(InRaumObjektSictungLezteZaitScrankeMin <= InRaumObjektZuusctand.SictungLezteZait))
					{
						continue;
					}
				}

				{
					//	Filter Änderung Type oder Name: Wen Type oder Naame in baide lezte Scnapscus versciide zu zuersct gesictete Type und Naame: Objekt entferne.

					if (!string.Equals(InRaumObjektZuusctand.SictungFrühesteType, InRaumObjektZuusctand.Type) ||
						!string.Equals(InRaumObjektZuusctand.SictungFrühesteName, InRaumObjektZuusctand.Name))
					{
						continue;
					}
				}

				if (null != OverviewPresetFolgeViewportAktuel)
				{
					var Zait = OverviewPresetFolgeViewportAktuel.WindowOverviewVorherigMitZait.Zait;

					var ObjektSictungLezteAlterNulbar = Zait - InRaumObjektZuusctand.SictungLezteZait;

					if (!ObjektSictungLezteAlterNulbar.HasValue)
					{
						continue;
					}

					var ObjektSictungLezteAlter = ObjektSictungLezteAlterNulbar.Value;

					var OverviewPresetFolgeViewportAktuelBeginObjektDistance = OverviewPresetFolgeViewportAktuel.BeginObjektDistance;
					var OverviewPresetFolgeViewportAktuelEndeObjektDistance = OverviewPresetFolgeViewportAktuel.EndeObjektDistance;

					if (OverviewPresetFolgeViewportAktuelBeginObjektDistance.HasValue &&
						OverviewPresetFolgeViewportAktuelEndeObjektDistance.HasValue &&
						InRaumObjektZuusctand.SictungLezteZait < OverviewPresetFolgeViewportAktuel.BeginZait)
					{
						var ObjektGescwindigkaitScrankeMax = GesamtObjektGescwindigkaitScrankeMax;

						if (InRaumObjektZuusctand.IstAsteroid ?? false)
						{
							ObjektGescwindigkaitScrankeMax = 1;
						}

						var ObjektZurükgeleegteSctrekeAnnaameScrankeMax = ((ObjektSictungLezteAlter + 1000) * ObjektGescwindigkaitScrankeMax) / 1000;

						var ObjektDistanzScrankeMin = InRaumObjektZuusctand.SictungLezteDistanceScrankeMinScpezOverview - ObjektZurükgeleegteSctrekeAnnaameScrankeMax;
						var ObjektDistanzScrankeMax = InRaumObjektZuusctand.SictungLezteDistanceScrankeMaxScpezOverview + ObjektZurükgeleegteSctrekeAnnaameScrankeMax;

						if (OverviewPresetFolgeViewportAktuelBeginObjektDistance < ObjektDistanzScrankeMin &&
							ObjektDistanzScrankeMax < OverviewPresetFolgeViewportAktuelEndeObjektDistance)
						{
							//	Das Objekt war sait Begin der aktuele Viewport Folge nit sictbar, konte sic aber vermuutlic inerhalb diiser Zait nict bis auserhalb des von der Viewport-Folge scon abgedekte Distanzberaic beweege.
							continue;
						}
					}
				}

				if (null != DictZuOverviewPresetNameFolgeViewportVolsctändig)
				{
					var ObjektDictZuOverviewPresetSictungLezteZait = InRaumObjektZuusctand.DictZuOverviewPresetSictungLezteZait;

					if (null == ObjektDictZuOverviewPresetSictungLezteZait)
					{
						continue;
					}

					if (ObjektDictZuOverviewPresetSictungLezteZait.Count < 1)
					{
						continue;
					}

					var ObjektMengeOverviewFolgeViewportVolsctändig =
						DictZuOverviewPresetNameFolgeViewportVolsctändig
						.Where((KandidaatFolge) => ObjektDictZuOverviewPresetSictungLezteZait
							.Any((KandidaatPresetSictung) => string.Equals(KandidaatPresetSictung.Key, KandidaatFolge.Key)))
						.ToArray();

					if (ObjektMengeOverviewFolgeViewportVolsctändig.Any((OverviewFolge) => InRaumObjektZuusctand.SictungLezteZait < OverviewFolge.Value.BeginZait))
					{
						//	Zaitpunkt der lezten Sictung des Objekt ist klainer als Begin ainer Folge welce ale Objekte in dem OverviewPreset abdekte. Daher werd davon ausgegange das Objekt nit meer im Overview sictbar.
						continue;
					}
				}

				MengeInRaumObjektZuErhalte.Add(InRaumObjektZuusctand);
			}
			*/

			var OverViewEntryErhalte = new Func<SictOverViewObjektZuusctand, bool>(Kandidaat =>
			SictOverviewUndTargetZuusctand.OverViewEntryZuusctandErhalte(
				Kandidaat,
				InRaumObjektSictungLezteZaitScrankeMin,
				OverviewPresetFolgeViewportAktuel,
				DictZuOverviewPresetNameFolgeViewportVolsctändig,
				GesamtObjektGescwindigkaitScrankeMax));

			var IsRat = new Func<SictOverViewObjektZuusctand, bool>(Kandidaat =>
			Kandidaat?.AingangUnglaicDefaultZuZaitLezte?.Wert.Key?.IconMainColor.OverviewIconMainColorIsRedAsRat() ?? false);

			var MengeInRaumObjektZuErhalte = MengeInRaumObjekt?.Where(OverViewEntryErhalte)?.ToArray();

			var MengeInRaumObjektZuEntferne = MengeInRaumObjekt.Except(MengeInRaumObjektZuErhalte).ToArray();

			var RatZuEntferne = MengeInRaumObjektZuEntferne?.Where(IsRat)?.ToArray();

			var MengeRatEnthalte = MengeInRaumObjekt?.Where(IsRat)?.ToArray();

			if (0 < RatZuEntferne?.Length)
			{
				foreach (var item in RatZuEntferne)
				{
					var t = OverViewEntryErhalte(item);
				}
			}

			MengeInRaumObjekt.RemoveAll((InRaumObjektZuusctand) => MengeInRaumObjektZuEntferne.Contains(InRaumObjektZuusctand));
		}
	}
}
