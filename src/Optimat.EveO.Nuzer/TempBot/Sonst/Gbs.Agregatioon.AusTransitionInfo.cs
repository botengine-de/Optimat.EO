using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Bib3;
using Newtonsoft.Json;
using Optimat.EveOnline.Base;
//using Optimat.EveOnline.AuswertGbs;
using Optimat.EveOnline.TempAuswertGbs;


namespace Optimat.EveOnline.Anwendung
{
	public enum SictGbsSymbolBedoitung
	{
		Kain = 0,
		Expanded = 10,
		Collapsed = 11,
	}

	public struct VerbindungSurveyScanViewListEntryZuTargetDurcMenuErgeebnis
	{
		readonly public SictGbsMenuKaskaadeZuusctand Menu;

		readonly public GbsListGroupedEntryZuusctand ListEntry;

		readonly public SictTargetZuusctand[] TailmengeTargetPasendZuSurveyScanEntry;

		readonly public SictTargetZuusctand[] MengeTargetMitTransitioonInZaitraumUmMenuBegin;

		readonly public SictTargetZuusctand TargetZuListEntry;

		public VerbindungSurveyScanViewListEntryZuTargetDurcMenuErgeebnis(
			SictGbsMenuKaskaadeZuusctand Menu,
			GbsListGroupedEntryZuusctand ListEntry,
			SictTargetZuusctand[] TailmengeTargetPasendZuSurveyScanEntry,
			SictTargetZuusctand[] MengeTargetMitTransitioonInZaitraumUmMenuBegin,
			SictTargetZuusctand TargetZuListEntry)
		{
			this.Menu = Menu;
			this.ListEntry = ListEntry;
			this.TailmengeTargetPasendZuSurveyScanEntry = TailmengeTargetPasendZuSurveyScanEntry;
			this.MengeTargetMitTransitioonInZaitraumUmMenuBegin = MengeTargetMitTransitioonInZaitraumUmMenuBegin;
			this.TargetZuListEntry = TargetZuListEntry;
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class MengeZuTargetMesungSurveyScanListEntryUndErzMengeBerict
	{
		[JsonProperty]
		readonly SictWertMitZait<KeyValuePair<SictTargetZuusctand, GbsListGroupedEntryZuusctand>>[]	ListeIndikatorZuTargetSurveyScanListEntry;

		[JsonProperty]
		public KeyValuePair<SictTargetZuusctand, SictWertMitZait<KeyValuePair<GbsListGroupedEntryZuusctand, Int64>>>[] MengeZuTargetMesungSurveyScanListEntryUndErzMenge
		{
			private set;
			get;
		}

		[JsonProperty]
		SictGbsWindowZuusctand WindowSurveyScanView;

		[JsonProperty]
		SictTargetZuusctand[] MengeTargetNocSictbar;

		public MengeZuTargetMesungSurveyScanListEntryUndErzMengeBerict()
		{
		}

		public MengeZuTargetMesungSurveyScanListEntryUndErzMengeBerict(
			ISictAutomatZuusctand AutomaatZuusctand,
			IEnumerable<SictWertMitZait<KeyValuePair<SictTargetZuusctand, GbsListGroupedEntryZuusctand>>>
			ListeIndikatorZuTargetSurveyScanListEntry)
		{
			this.ListeIndikatorZuTargetSurveyScanListEntry = ListeIndikatorZuTargetSurveyScanListEntry.ToArrayNullable();

			Berecne(AutomaatZuusctand);
		}

		public void Berecne(ISictAutomatZuusctand AutomaatZuusctand)
		{
			var MengeZuTargetMesungSurveyScanListEntryUndErzMenge =
				new List<KeyValuePair<SictTargetZuusctand, SictWertMitZait<KeyValuePair<GbsListGroupedEntryZuusctand, Int64>>>>();

			try
			{
				if (null == AutomaatZuusctand)
				{
					return;
				}

				var MengeZuListEntryTargetPerDistance = AutomaatZuusctand.MengeZuListEntryTargetAinscrankungPerDistance();

				var OverviewUndTarget = AutomaatZuusctand.OverviewUndTarget;
				var Gbs = AutomaatZuusctand.Gbs;

				if (null == OverviewUndTarget)
				{
					return;
				}

				if (null == Gbs)
				{
					return;
				}

				WindowSurveyScanView = Gbs.WindowSurveyScanView();
				MengeTargetNocSictbar = OverviewUndTarget.MengeTargetNocSictbar.ToArrayNullable();

				if (null == WindowSurveyScanView)
				{
					return;
				}

				var WindowSurveyScanViewList = WindowSurveyScanView.ListHaupt;

				if (null == WindowSurveyScanViewList)
				{
					return;
				}

				var WindowSurveyScanViewListListeEntry = WindowSurveyScanViewList.ListeEntry();

				if (null == MengeTargetNocSictbar)
				{
					return;
				}

				foreach (var TargetNocSictbar in MengeTargetNocSictbar)
				{
					SictWertMitZait<GbsListGroupedEntryZuusctand>	ZuZaitSurveyScanEntry = default(SictWertMitZait<GbsListGroupedEntryZuusctand>);

					{
						var ZuTargetIndikatorLezte =
							ListeIndikatorZuTargetSurveyScanListEntry
							.LastOrDefaultNullable((Kandidaat) => Kandidaat.Wert.Key == TargetNocSictbar);

						/*
						 * 2014.09.25
						 * 
						if (null == ZuTargetIndikatorLezte.Wert.Key)
						{
							continue;
						}

						var SurveyScanEntry = ZuTargetIndikatorLezte.Wert.Value;
						 * */

						if (null != ZuTargetIndikatorLezte.Wert.Key)
						{
							ZuZaitSurveyScanEntry = new	SictWertMitZait<GbsListGroupedEntryZuusctand>(ZuTargetIndikatorLezte.Zait, ZuTargetIndikatorLezte.Wert.Value);
						}
					}

					if(null	!= MengeZuListEntryTargetPerDistance)
					{
						var TailmengeListEntryAingescranktDurcDistance =
							WindowSurveyScanViewListListeEntry
							.WhereNullable((KandidaatListEntry) =>
								{
									var ZuListEntryInfoAinscrankungDistance =
										MengeZuListEntryTargetPerDistance.FirstOrDefault((KandidaatZuListEntryInfoAinscrankungDistance) =>
										KandidaatZuListEntryInfoAinscrankungDistance.ListEntry == KandidaatListEntry);

									if (null == ZuListEntryInfoAinscrankungDistance)
									{
										//	Ainscrankung zu diise ListEntry noc nit berecnet.
										return true;
									}

									return ZuListEntryInfoAinscrankungDistance.MengeInRaumObjekt.ContainsNullable((TargetNocSictbar));
								})
							.ToArrayNullable();

						if (1 == TailmengeListEntryAingescranktDurcDistance.CountNullable())
						{
							var	SurveyScanEntry	= TailmengeListEntryAingescranktDurcDistance.First();

							ZuZaitSurveyScanEntry =
								new SictWertMitZait<GbsListGroupedEntryZuusctand>(SurveyScanEntry.ScnapscusFrühesteZait ?? -1, SurveyScanEntry);
						}
					}

					if (null == ZuZaitSurveyScanEntry.Wert)
					{
						continue;
					}

					if (!(WindowSurveyScanView.ScnapscusFrühesteZait <= ZuZaitSurveyScanEntry.Wert.AingangUnglaicDefaultLezteZait))
					{
						continue;
					}

					var Quantity = ZuZaitSurveyScanEntry.Wert.Quantity;

					if (!Quantity.HasValue)
					{
						continue;
					}

					MengeZuTargetMesungSurveyScanListEntryUndErzMenge.Add(
						new KeyValuePair<SictTargetZuusctand, SictWertMitZait<KeyValuePair<GbsListGroupedEntryZuusctand, Int64>>>(
							TargetNocSictbar,
							new SictWertMitZait<KeyValuePair<GbsListGroupedEntryZuusctand, Int64>>(
								ZuZaitSurveyScanEntry.Zait,
								new KeyValuePair<GbsListGroupedEntryZuusctand, Int64>(ZuZaitSurveyScanEntry.Wert, Quantity.Value))));
				}
			}
			finally
			{
				this.MengeZuTargetMesungSurveyScanListEntryUndErzMenge = MengeZuTargetMesungSurveyScanListEntryUndErzMenge.ToArray();
			}
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictGbsAgregatioonAusTransitionInfo
	{
		[JsonProperty]
		readonly List<SictWertMitZait<KeyValuePair<Int64, SictGbsSymbolBedoitung>>> ListeMesungZuSymboolBedoitung =
		new List<SictWertMitZait<KeyValuePair<Int64, SictGbsSymbolBedoitung>>>();

		[JsonProperty]
		readonly Queue<VerbindungSurveyScanViewListEntryZuTargetDurcMenuErgeebnis> ListeVersuucVerbindungSurveyScanViewListEntryZuTarget =
		new Queue<VerbindungSurveyScanViewListEntryZuTargetDurcMenuErgeebnis>();

		[JsonProperty]
		readonly Queue<SictWertMitZait<KeyValuePair<SictTargetZuusctand, GbsListGroupedEntryZuusctand>>>
		InternListeIndikatorZuTargetSurveyScanListEntry =
		new Queue<SictWertMitZait<KeyValuePair<SictTargetZuusctand, GbsListGroupedEntryZuusctand>>>();

		public IEnumerable<SictWertMitZait<KeyValuePair<SictTargetZuusctand, GbsListGroupedEntryZuusctand>>> ListeIndikatorZuTargetSurveyScanListEntry
		{
			get
			{
				return InternListeIndikatorZuTargetSurveyScanListEntry;
			}
		}

		/*
		 * 2014.09.26
		 * 
		[JsonProperty]
		public MengeZuTargetMesungSurveyScanListEntryUndErzMengeBerict MengeZuTargetMesungSurveyScanListEntryUndErzMengeBerict
		{
			private set;
			get;
		}

		public KeyValuePair<SictTargetZuusctand, SictWertMitZait<KeyValuePair<GbsListGroupedEntryZuusctand, Int64>>>[] MengeZuTargetMesungSurveyScanListEntryUndErzMenge
		{
			get
			{
				var MengeZuTargetMesungSurveyScanListEntryUndErzMengeBerict = this.MengeZuTargetMesungSurveyScanListEntryUndErzMengeBerict;

				if (null == MengeZuTargetMesungSurveyScanListEntryUndErzMengeBerict)
				{
					return null;
				}

				return MengeZuTargetMesungSurveyScanListEntryUndErzMengeBerict.MengeZuTargetMesungSurveyScanListEntryUndErzMenge;
			}
		}

		void AktualisiireTailMengeZuTargetMesungSurveyScanListEntryUndErzMenge(
			Optimat.ScpezEveOnln.SictAutomatZuusctand AutomaatZuusctand)
		{
			this.MengeZuTargetMesungSurveyScanListEntryUndErzMengeBerict = new MengeZuTargetMesungSurveyScanListEntryUndErzMengeBerict(
				AutomaatZuusctand,
				ListeIndikatorZuTargetSurveyScanListEntry);
		}
		 * */

		public void AgregiireVerbindungSurveyScanViewListEntryZuTarget(
			Optimat.ScpezEveOnln.SictAutomatZuusctand AutomaatZuusctand)
		{
			try
			{
				if (null == AutomaatZuusctand)
				{
					return;
				}

				var MengeTarget = AutomaatZuusctand.MengeTarget();
				var AutoMine = AutomaatZuusctand.AutoMine;

				var MenuKaskaadeLezte = AutomaatZuusctand.MenuKaskaadeLezte();

				if (null == AutoMine)
				{
					return;
				}

				if (null == MenuKaskaadeLezte)
				{
					return;
				}

				if (MenuKaskaadeLezte.EndeZait.HasValue)
				{
					return;
				}

				var MenuKaskaadeLezteScritVorherZait =
					AutomaatZuusctand.ZuScnapscusFrühesteZaitAlsNuzerZaitBerecneScritVorherNuzerZait(MenuKaskaadeLezte);

				var MenuKaskaadeLezteAlterScritAnzaal =
					AutomaatZuusctand.ZuObjektBerecneAlterScnapscusAnzaal(MenuKaskaadeLezte);

				if (!MenuKaskaadeLezteAlterScritAnzaal.HasValue)
				{
					return;
				}

				if (!(2 < MenuKaskaadeLezteAlterScritAnzaal))
				{
					return;
				}

				if (ListeVersuucVerbindungSurveyScanViewListEntryZuTarget.Any((Kandidaat) => Kandidaat.Menu == MenuKaskaadeLezte))
				{
					//	zu diise Menu isc beraits Verbindung berecnet worde.
					return;
				}

				GbsListGroupedEntryZuusctand MenuWurzelSurveyScanListEntry = null;
				SictTargetZuusctand[] TailmengeTargetPasendZuSurveyScanEntry = null;
				SictTargetZuusctand[] MengeTargetMitTransitioonInZaitraumUmMenuBegin = null;
				SictTargetZuusctand TargetZuListEntry = null;

				try
				{
					var MenuKaskaadeLezteMenu0ListeEntry = MenuKaskaadeLezte.AusMenu0ListeEntryBerecne();

					if (null == MenuKaskaadeLezteMenu0ListeEntry)
					{
						return;
					}

					var GbsWindowSurveyScanView = AutomaatZuusctand.WindowSurveyScanView();

					if (null == GbsWindowSurveyScanView)
					{
						return;
					}

					var SurveyScanList = GbsWindowSurveyScanView.ListHaupt;

					if (null == SurveyScanList)
					{
						return;
					}

					var MengeTargetTailmengeBisVorherigeScrit =
						MengeTarget
						.WhereNullable((Kandidaat) => Kandidaat.SaitAingangUnglaicDefaultLezteListeAingangAnzaal() < 2)
						.ToArrayNullable();

					if (MengeTargetTailmengeBisVorherigeScrit.NullOderLeer())
					{
						return;
					}

					var AutoMineMengeTargetVerwendet = AutoMine.MengeTargetVerwendet;

					if (AutoMineMengeTargetVerwendet.NullOderLeer())
					{
						return;
					}

					var MenuEntryUnlockTarget = MenuKaskaadeLezteMenu0ListeEntry.MenuEntryTargetUnLock();

					if (null == MenuEntryUnlockTarget)
					{
						return;
					}

					MenuWurzelSurveyScanListEntry =
						SurveyScanList.ListeEntry()
						.FirstOrDefaultNullable((KandidaatEntry) =>
							AutomaatZuusctand.GbsMenuLezteInAstMitHerkunftAdrese(KandidaatEntry.GbsAstHerkunftAdrese) ==
							MenuKaskaadeLezte);

					if (null == MenuWurzelSurveyScanListEntry)
					{
						return;
					}

					/*
					 * 2014.09.26
					 * 
					 * Ersaz durc AutomaatZuusctand.ZuSurveyScanEntryMengeKandidaatTarget.
					 * 
					TailmengeTargetPasendZuSurveyScanEntry =
						MengeTargetTailmengeBisVorherigeScrit
						.WhereNullable((KandidaatTarget) =>
							{
								var AusAutoMineTargetInfo =
									AutoMineMengeTargetVerwendet
									.FirstOrDefaultNullable((KandidaatAusAutoMineTargetInfo) => KandidaatAusAutoMineTargetInfo.Key == KandidaatTarget);

								if (null == AusAutoMineTargetInfo.Key)
								{
									return false;
								}

								return
									AusAutoMineTargetInfo.Value.AusSurveyScanMengeListEntryPasendZuOreType
									.ContainsNullable(MenuWurzelSurveyScanListEntry);
							})
						.ToArrayNullable();
					 * */

					TailmengeTargetPasendZuSurveyScanEntry =
						AutomaatZuusctand.ZuSurveyScanEntryMengeKandidaatTarget(MenuWurzelSurveyScanListEntry).ToArrayNullable();

					if (1 == TailmengeTargetPasendZuSurveyScanEntry.CountNullable())
					{
						TargetZuListEntry = TailmengeTargetPasendZuSurveyScanEntry.FirstOrDefault();

						return;
					}

					if (TailmengeTargetPasendZuSurveyScanEntry.NullOderLeer())
					{
						return;
					}

					MengeTargetMitTransitioonInZaitraumUmMenuBegin =
						TailmengeTargetPasendZuSurveyScanEntry
						.WhereNullable((KandidaatTarget) => MenuKaskaadeLezteScritVorherZait <= KandidaatTarget.InputFookusTransitioonLezteZait)
						.ToArrayNullable();

					var MengeTargetMitTransitioonInZaitraumUmMenuBeginTailmengeZiilWertAin =
						MengeTargetMitTransitioonInZaitraumUmMenuBegin
						.WhereNullable((Kandidaat) => Kandidaat.InputFookusTransitioonLezteZiilWert ?? false)
						.ToArrayNullable();

					if (!(1 == MengeTargetMitTransitioonInZaitraumUmMenuBeginTailmengeZiilWertAin.CountNullable()))
					{
						//	meerere Target mit aingescaltete InputFookus in Zaitraum.
						return;
					}

					var Target = MengeTargetMitTransitioonInZaitraumUmMenuBeginTailmengeZiilWertAin.FirstOrDefault();

					if (Target.ScnapscusFrühesteZait < Target.InputFookusTransitioonLezteZait)
					{
						//	Target ist nit noi.

						TargetZuListEntry = Target;
					}
				}
				finally
				{
					ListeVersuucVerbindungSurveyScanViewListEntryZuTarget.Enqueue(
						new VerbindungSurveyScanViewListEntryZuTargetDurcMenuErgeebnis(
							MenuKaskaadeLezte,
							MenuWurzelSurveyScanListEntry,
							TailmengeTargetPasendZuSurveyScanEntry,
							MengeTargetMitTransitioonInZaitraumUmMenuBegin,
							TargetZuListEntry));

					if (null != TargetZuListEntry)
					{
						InternListeIndikatorZuTargetSurveyScanListEntry.Enqueue(
							new SictWertMitZait<KeyValuePair<SictTargetZuusctand, GbsListGroupedEntryZuusctand>>(
								MenuKaskaadeLezte.BeginZait ?? -1,
								new KeyValuePair<SictTargetZuusctand, GbsListGroupedEntryZuusctand>(
									TargetZuListEntry,
									MenuWurzelSurveyScanListEntry)));
					}
				}
			}
			finally
			{
				ListeVersuucVerbindungSurveyScanViewListEntryZuTarget.ListeKürzeBegin(3);
				InternListeIndikatorZuTargetSurveyScanListEntry.ListeKürzeBegin(40);
			}
		}

		public void Agregiire(
			Optimat.ScpezEveOnln.SictAutomatZuusctand AutomaatZuusctand)
		{
			try
			{
				if (null == AutomaatZuusctand)
				{
					return;
				}

				var NuzerZaitMili = AutomaatZuusctand.NuzerZaitMili;

				var Gbs = AutomaatZuusctand.Gbs;
				var OverviewUndTarget = AutomaatZuusctand.OverviewUndTarget;

				var AutoMine = AutomaatZuusctand.AutoMine;

				var Scnapscus = AutomaatZuusctand.ListeScnapscusLezteAuswertungErgeebnis;

				/*
				 * 2015.03.12
				 * 
				var OptimatScritAktuelGbsBaum = AutomaatZuusctand.VonNuzerMeldungZuusctandTailGbsBaum;
				 * */

				var ScnapscusMengeListeGrouped = (null == Scnapscus) ? null : Scnapscus.MengeListGroupedBerecne();

				AgregiireVerbindungSurveyScanViewListEntryZuTarget(AutomaatZuusctand);

				/*
				 * 2015.02.26
				 * 
				 * Ersaz durc ScnapscusEntry.IsExpanded
				 * 
				 * 
				if (null != ScnapscusMengeListeGrouped)
				{
					foreach (var ListGroupedScnapscus in ScnapscusMengeListeGrouped)
					{
						if (null == ListGroupedScnapscus)
						{
							continue;
						}

						var ListGroupedHerkunftAdrese = (Int64?)ListGroupedScnapscus.Ident;

						if (!ListGroupedHerkunftAdrese.HasValue)
						{
							continue;
						}

						var ListGroupedScnapscusVorher =
							AutomaatZuusctand.AusListeScnapscusVorLezteMengeAuswertungErgeebnisZuAstMitHerkunftAdrese(ListGroupedHerkunftAdrese.Value)
							.OfTypeNullable<VonSensor.ScnapscusListGrouped>()
							.FirstOrDefaultNullable();

						if (null == ListGroupedScnapscusVorher)
						{
							continue;
						}

						var MengeEntryTexturIdentTransition =
							Optimat.EveOnline.Extension.MengeEntryExpanderIconTexturIdentTransition(
							ListGroupedScnapscusVorher,
							ListGroupedScnapscus);

						if (null == MengeEntryTexturIdentTransition)
						{
							continue;
						}

						if (MengeEntryTexturIdentTransition.NullOderLeer())
						{
							continue;
						}

						var AleTransitionZuGlaiceZiil = MengeEntryTexturIdentTransition.Select((Transition) => Transition.Value).Distinct().Count() < 2;

						if (!AleTransitionZuGlaiceZiil)
						{
							continue;
						}

						bool AktioonExpand = false;
						bool AktioonCollapse = false;

						if ((Bib3.Extension.CountNullable(ListGroupedScnapscusVorher.ListeEntry) ?? 0) < Bib3.Extension.CountNullable(ListGroupedScnapscus.ListeEntry) ||
							ListGroupedScnapscus.ScrollHandleAntailAnGesamtMili < (ListGroupedScnapscusVorher.ScrollHandleAntailAnGesamtMili ?? 1000))
						{
							AktioonExpand = true;
						}

						if ((Bib3.Extension.CountNullable(ListGroupedScnapscus.ListeEntry) ?? 0) < Bib3.Extension.CountNullable(ListGroupedScnapscusVorher.ListeEntry) ||
							ListGroupedScnapscusVorher.ScrollHandleAntailAnGesamtMili < (ListGroupedScnapscus.ScrollHandleAntailAnGesamtMili ?? 1000))
						{
							AktioonCollapse = true;
						}

						if (AktioonCollapse && AktioonExpand)
						{
							continue;
						}

						if (!(AktioonCollapse || AktioonExpand))
						{
							continue;
						}

						var BedoitungSictEnum =
							AktioonExpand ? SictGbsSymbolBedoitung.Expanded : SictGbsSymbolBedoitung.Collapsed;

						ListeMesungZuSymboolBedoitung.Add(
							new SictWertMitZait<KeyValuePair<Int64, SictGbsSymbolBedoitung>>(
								NuzerZaitMili,
								new KeyValuePair<Int64, SictGbsSymbolBedoitung>(MengeEntryTexturIdentTransition.FirstOrDefault().Value, BedoitungSictEnum)));
					}
				}
				 * */
			}
			finally
			{
				ListeMesungZuSymboolBedoitung.ListeKürzeBegin(10);
			}
		}

		public SictWertMitZait<SictGbsSymbolBedoitung>? ZuTexturIdentBedoitungLezteMitZait(Int64 TexturIdent)
		{
			var Fund = ListeMesungZuSymboolBedoitung.LastOrDefaultNullable((Kandidaat) => Kandidaat.Wert.Key == TexturIdent);

			if (!(TexturIdent == Fund.Wert.Key))
			{
				return null;
			}

			return new SictWertMitZait<SictGbsSymbolBedoitung>(Fund.Zait, Fund.Wert.Value);
		}
	}
}
