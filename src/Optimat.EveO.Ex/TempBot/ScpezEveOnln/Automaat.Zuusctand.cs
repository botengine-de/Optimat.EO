using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Optimat.EveOnline;
using Optimat.EveOnline.Anwendung;
using Bib3;
//using Optimat.EveOnline.AuswertGbs;
using Optimat.EveOnline.Anwendung.AuswertGbs;
using Optimat.EveOnline.VonSensor;
using VonSensor = Optimat.EveOnline.VonSensor;
using Optimat.EveOnline.CustomBot;
using ExtractFromOldAssembly.Bib3;

namespace Optimat.ScpezEveOnln
{
	[JsonObject(MemberSerialization.OptIn)]
	public struct ZuScritInfoZait
	{
		[JsonProperty]
		readonly public int ScritIndex;

		[JsonProperty]
		readonly public Int64 NuzerZait;

		[JsonProperty]
		readonly public Int64 ServerZait;

		public	ZuScritInfoZait(
			int	ScritIndex,
			Int64	NuzerZait,
			Int64	ServerZait)
		{
			this.ScritIndex	= ScritIndex;
			this.NuzerZait	= NuzerZait;
			this.ServerZait	= ServerZait;
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public partial class SictAutomatZuusctand : SictAutomatZuusctandWaiterlaitung,	ISictAutomatZuusctandMitErwaiterungFürBerict
	{
		readonly Bib3.SictIdentInt64Fabrik AufgaabeIdentFabrik = new Bib3.SictIdentInt64Fabrik();


		[JsonProperty]
		Queue<ZuScritInfoZait> InternListeScritZait = new Queue<ZuScritInfoZait>();

		[JsonProperty]
		Int64? VorsclaagWirkungAusgefüürtNictLezteAlter;

		[JsonProperty]
		int InternScritDauerDurcscnit
		{
			set;
			get;
		}

		[JsonProperty]
		public byte[] AnwendungSizungIdent
		{
			set;
			get;
		}

		[JsonProperty]
		public byte[][] MengeClientIdentAktuel
		{
			set;
			get;
		}

		[JsonProperty]
		override	public SictPräferenzZuZaitVerhalte ParamZuZaitVerhalteKombi
		{
			set;
			get;
		}

		[JsonProperty]
		public	List<SictWertMitZait<SictDataiIdentUndSuuceHinwais>> TempVonNuzerListeBerictWindowClientRaster;

		[JsonProperty]
		override	public	Optimat.EveOnline.SictEveWeltTopologii EveWeltTopologii
		{
			set;
			get;
		}

		[JsonProperty]
		public Int64? SelbstShipUndockingLezteZaitMili;

		[JsonProperty]
		override	public SictAufgaabeGrupePrio[] ScritLezteListePrioMengeAufgaabe
		{
			set;
			get;
		}

		[JsonProperty]
		override	public SictGbsWindowZuusctand[] MengeWindowZuErhalte
		{
			set;
			get;
		}

		/// <summary>
		/// 2014.06.10 Ainbau untere Scranke für Distanz in OptimatScrit zwisce Aktioone in AgentDialogue.
		/// Grund: Meermaalige Beobactung das Automaat verseehentlic Button "Accept" betäätigt bai dem Versuuc Button "Request" zu betäätige
		/// (nacdeem "Request" beraits in vorherige Scrit erfolgraic betäägt wurde, trit di Änderung in GBS ersct mit Verzöögerung ain).
		/// </summary>
		[JsonProperty]
		override	public SictGbsWindowZuusctand[] MengeWindowWirkungGescpert
		{
			set;
			get;
		}

		[JsonProperty]
		override	public Int64? AnnaameActiveShipCargoGeneralLeerLezteZaitMili
		{
			set;
			get;
		}

		[JsonProperty]
		override	public Int64? AnforderungActiveShipCargoGeneralLeereLezteZaitMili
		{
			set;
			get;
		}

		[JsonProperty]
		public SictWertMitZait<object> FluctLezte;

		[JsonProperty]
		override	public List<SictWertMitZait<SictInventoryItemTransport>> ListeInventoryItemTransportMitZait
		{
			set;
			get;
		}

		[JsonProperty]
		public Queue<SictVerlaufBeginUndEndeRef<VonSensor.Message>> InternListeAusGbsAbovemainMessageMitZait;

		[JsonProperty]
		SictAssets Assets;

		[JsonProperty]
		override	public SictWertMitZait<VonSensor.UtilmenuMissionInfo>? UtilmenuMissionLezte
		{
			set;
			get;
		}

		[JsonProperty]
		override	public SictGbsZuusctand Gbs
		{
			set;
			get;
		}

		[JsonProperty]
		override	public SictShipZuusctandMitFitting FittingUndShipZuusctand
		{
			set;
			get;
		}

		[JsonProperty]
		override	public SictOverviewUndTargetZuusctand OverviewUndTarget
		{
			set;
			get;
		}

		[JsonProperty]
		override	public SictAutoMine AutoMine
		{
			set;
			get;
		}

		[JsonProperty]
		override	public SictAgentUndMissionZuusctand AgentUndMission
		{
			set;
			get;
		}

		[JsonProperty]
		override	public SictInRaumAktioonUndGefect InRaumAktioonUndGefect
		{
			set;
			get;
		}

		[JsonProperty]
		override	public List<SictWertMitZait<Int64>> MengeVonNuzerMeldungWirkungErfolgZuZait
		{
			set;
			get;
		}

		[JsonProperty]
		override	public List<SictAufgaabeZuusctand> MengeAufgaabeZuusctand
		{
			set;
			get;
		}

		/// <summary>
		/// Aus Scrit Lezte:
		/// zu jeede Aufgaabe für welce aine Wirkung naac Nuzer vorgesclaage wurde der Pfaad bis zum Blat.
		/// </summary>
		[JsonProperty]
		override	public KeyValuePair<SictAufgaabeZuusctand[], string>[] ScritLezteWirkungMengeAufgaabePfaadZuBlatMitGrupePrioNaame
		{
			set;
			get;
		}

		[JsonProperty]
		override	public KeyValuePair<SictAufgaabeZuusctand, string>[] ScritLezteListeAufgaabeBerecnet
		{
			set;
			get;
		}

		[JsonProperty]
		override	public SictAufgaabeKombiZuusctand ScritLezteListeAufgaabeKombiZuusctand
		{
			set;
			get;
		}

		/// <summary>
		/// 2015.01.04	Beobactung:
		/// Automaat klikt in Overview Scroll imer linx der Mite des ScrollHandle.
		/// 
		/// Ain Verglaic mit Screenshot in Anwendung Auswert Berict zaigt das dort kain Versaz vorliigt. Daher Ursace vermuutlic eher in Anwendung Nuzer.
		/// </summary>
		[JsonProperty]
		readonly public Vektor2DInt NaacNuzerMausPfaadVersaz = new Vektor2DInt(0, 0);

		[JsonProperty]
		override	public SictWertMitZait<VonSensor.MenuEntry> AufgaabeMenuEntryAbgescloseTailWirkungLezteMitZait
		{
			set;
			get;
		}

		[JsonProperty]
		override	public SictShipUiModuleReprZuusctand[] MengeModuleUmscaltFraigaabe
		{
			set;
			get;
		}

		/// <summary>
		/// scpaicert zu Window desen bisherige ScnapscusScritAnzaal wäärend desen lezte Nuzung durc VorsclaagWirkung.
		/// </summary>
		[JsonProperty]
		override	public List<KeyValuePair<SictGbsWindowZuusctand,	Int64>> ListeZuWindowNuzungLezteWindowScritAnzaal
		{
			set;
			get;
		}

		[JsonProperty]
		override	public List<SictWertMitZait<Optimat.EveOnline.SictVorsclaagNaacProcessWirkung>>
			ListeZuZaitNaacNuzerVorsclaagNaacProcessWirkung
		{
			set;
			get;
		}

		[JsonProperty]
		override	public List<SictWertMitZait<Optimat.EveOnline.SictNaacProcessWirkung>>
			ListeZuZaitVonNuzerMeldungNaacProcessWirkung
		{
			set;
			get;
		}

		[JsonProperty]
		List<VerbindungListEntryUndTargetPerDistance> InternMengeZuListEntryTarget =
		new List<VerbindungListEntryUndTargetPerDistance>();

		[JsonProperty]
		List<ZuOreTypAusSurveyScanInfo> InternMengeZuOreTypSictStringSurveyScanInfo =
		new List<ZuOreTypAusSurveyScanInfo>();

		[JsonProperty]
		List<ZuTargetAinscrankungMengeSurveyScanItem> InternMengeZuTargetSurveyScanInfo =
		new List<ZuTargetAinscrankungMengeSurveyScanItem>();

		[JsonProperty]
		List<SictVerlaufBeginUndEndeRef<string>> InternListeLocationNearest =
		new List<SictVerlaufBeginUndEndeRef<string>>();

		override public IEnumerable<SictVerlaufBeginUndEndeRef<string>> ListeLocationNearest()
		{
			return InternListeLocationNearest;
		}

		override public IEnumerable<VerbindungListEntryUndTargetPerDistance> MengeZuListEntryTargetAinscrankungPerDistance()
		{
			return InternMengeZuListEntryTarget;
		}

		override public	IEnumerable<ZuTargetAinscrankungMengeSurveyScanItem> MengeZuTargetSurveyScanInfo()
		{
			return InternMengeZuTargetSurveyScanInfo;
		}

		override public	IEnumerable<ZuOreTypAusSurveyScanInfo> MengeZuOreTypSictStringSurveyScanInfo()
		{
			return InternMengeZuOreTypSictStringSurveyScanInfo;
		}

		override public IEnumerable<VerbindungListEntryUndOverviewObjektPerDistance> MengeZuListEntryOverviewObjekt()
		{
			return null;
		}

		override public IEnumerable<ZuScritInfoZait> ListeScritZait
		{
			get
			{
				return InternListeScritZait;
			}
		}

		override public int? ScritLezteIndex
		{
			get
			{
				if (InternListeScritZait.Count < 1)
				{
					return null;
				}

				return InternListeScritZait.LastOrDefault().ScritIndex;
			}
		}

		override public int ScritDauerDurcscnit()
		{
			return this.InternScritDauerDurcscnit;
		}

		override public IEnumerable<SictVerlaufBeginUndEndeRef<VonSensor.Message>> ListeAusGbsAbovemainMessageMitZait()
		{
			return InternListeAusGbsAbovemainMessageMitZait;
		}

		override public IEnumerable<SictWertMitZait<SictVorsclaagNaacProcessWirkung>> ListeVorsclaagWirkungErfolgraic()
		{
			var ListeZuZaitNaacNuzerVorsclaagNaacProcessWirkung = this.ListeZuZaitNaacNuzerVorsclaagNaacProcessWirkung;
			var ListeZuZaitVonNuzerMeldungNaacProcessWirkung = this.ListeZuZaitVonNuzerMeldungNaacProcessWirkung;

			if (null == ListeZuZaitNaacNuzerVorsclaagNaacProcessWirkung || null == ListeZuZaitVonNuzerMeldungNaacProcessWirkung)
			{
				return null;
			}

			var Liste = new List<SictWertMitZait<Optimat.EveOnline.SictVorsclaagNaacProcessWirkung>>();

			foreach (var ZuZaitVorsclaag in ListeZuZaitNaacNuzerVorsclaagNaacProcessWirkung)
			{
				if (null == ZuZaitVorsclaag.Wert)
				{
					continue;
				}

				var VorsclaagIdent = (Int64?)ZuZaitVorsclaag.Wert.Ident;

				if (!VorsclaagIdent.HasValue)
				{
					continue;
				}

				var WirkungErgeebnis =
					ListeZuZaitVonNuzerMeldungNaacProcessWirkung
					.LastOrDefaultNullable((KandidaatWirkung) => (null == KandidaatWirkung.Wert ? null : KandidaatWirkung.Wert.VorsclaagWirkungIdent) == VorsclaagIdent);

				if (null == WirkungErgeebnis.Wert)
				{
					continue;
				}

				if (!(WirkungErgeebnis.Wert.Erfolg ?? false))
				{
					continue;
				}

				Liste.Add(new	SictWertMitZait<SictVorsclaagNaacProcessWirkung>(WirkungErgeebnis.Zait, ZuZaitVorsclaag.Wert));
			}

			return Liste;
		}

		override public SictAusGbsScnapscusAuswertungSrv AusListeScnapscusVorLezte()
		{
			return	ListeScnapscusAuswertungErgeebnisNaacSimu.ElementAtOrDefault(ListeScnapscusAuswertungErgeebnisNaacSimu.Count - 2);
		}

		override public IEnumerable<GbsElement> AusListeScnapscusVorLezteMengeAuswertungErgeebnisZuAstMitHerkunftAdrese(
			Int64 AstHerkunftAdrese)
		{
			var AusListeScnapscusVorLezte = this.AusListeScnapscusVorLezte();

			if (null == AusListeScnapscusVorLezte)
			{
				return null;
			}

			return	AusListeScnapscusVorLezte.MengeAuswertungErgeebnisZuAstMitHerkunftAdrese(AstHerkunftAdrese);
		}

		override	public IEnumerable<SictVerlaufBeginUndEndeRef<ShipUiIndicationAuswert>> ListeAusShipUIIndicationMitZait
		{
			get
			{
				var FittingUndShipZuusctand = this.FittingUndShipZuusctand;

				if (null == FittingUndShipZuusctand)
				{
					return null;
				}

				return FittingUndShipZuusctand.ListeAusShipUIIndicationMitZait;
			}
		}

		override	public IEnumerable<SictWertMitZait<Int64>> MengeVonNuzerMeldungWirkungErfolgZuZaitBerecne()
		{
			return MengeVonNuzerMeldungWirkungErfolgZuZait;
		}

		IEnumerable<SictAufgaabeZuusctand> MengeAufgaabeZuusctandTailmengeBerecneMitAbsclusTailWirkungZaitScranke(
			Func<SictAufgaabeParam, bool> PrädikaatAufgaabeParam,
			Int64? AbsclusTailWirkungZaitScrankeMin	= null)
		{
			var	Prädikaat	=	new	Func<SictAufgaabeZuusctand,	bool>((AufgaabeZuusctand) =>
				{
					if(null	== AufgaabeZuusctand)
					{
						return	false;
					}

					if(AbsclusTailWirkungZaitScrankeMin.HasValue)
					{
						if(!(AbsclusTailWirkungZaitScrankeMin	<= AufgaabeZuusctand.AbsclusTailWirkungZait))
						{
							return	false;
						}
					}

					if(null	!= PrädikaatAufgaabeParam)
					{
					var	AufgaabeParam	= AufgaabeZuusctand.AufgaabeParam;

						if(!PrädikaatAufgaabeParam(AufgaabeParam))
						{
							return	false;
						}
					}

					return	true;
				});

			return MengeAufgaabeZuusctandTailmengeBerecne(Prädikaat);
		}

		IEnumerable<SictWertMitZait<T>> MengeAufgaabeAbsclusTailWirkungZaitMitSelectBerecneAnhandAufgaabeParam<T>(
			Func<SictAufgaabeParam, KeyValuePair<bool, T>> PrädikaatUndSelect,
			Int64? AufgaabeAbsclusTailWirkungZaitScrankeMin = null,
			int? KomponenteTiifeScrankeMax = null)
		{
			var MengeAufgaabeZuusctandTailmengeMitSelect =
				MengeAufgaabeZuusctandTailmengeMitSelectBerecneAnhandAufgaabeParam(
				PrädikaatUndSelect, AufgaabeAbsclusTailWirkungZaitScrankeMin, KomponenteTiifeScrankeMax);

			if (null == MengeAufgaabeZuusctandTailmengeMitSelect)
			{
				yield	break;
			}

			foreach (var AufgaabeZuusctandTailmengeMitSelect in MengeAufgaabeZuusctandTailmengeMitSelect)
			{
				var AufgaabeZuusctand = AufgaabeZuusctandTailmengeMitSelect.Key;

				if (null == AufgaabeZuusctand)
				{
					continue;
				}

				var	AufgaabeZuusctandAbsclusTailWirkungZait	= AufgaabeZuusctand.AbsclusTailWirkungZait;

				if (!AufgaabeZuusctandAbsclusTailWirkungZait.HasValue)
				{
					continue;
				}

				yield return new SictWertMitZait<T>(AufgaabeZuusctandAbsclusTailWirkungZait.Value, AufgaabeZuusctandTailmengeMitSelect.Value);
			}
		}

		IEnumerable<KeyValuePair<SictAufgaabeZuusctand, T>> MengeAufgaabeZuusctandTailmengeMitSelectBerecneAnhandAufgaabeParam<T>(
			Func<SictAufgaabeParam, KeyValuePair<bool, T>> PrädikaatUndSelect,
			Int64?	AufgaabeAbsclusTailWirkungZaitScrankeMin	= null,
			int? KomponenteTiifeScrankeMax = null)
		{
			return MengeAufgaabeZuusctandTailmengeMitSelectBerecne(
				(AufgaabeZuusctand) =>
				{
					var AufgaabeParam = (null == AufgaabeZuusctand) ? null : AufgaabeZuusctand.AufgaabeParam;

					var EntscaidungZuugehöörigUndSelect = PrädikaatUndSelect(AufgaabeParam);

					return EntscaidungZuugehöörigUndSelect;
				},
				KomponenteTiifeScrankeMax);
		}

		IEnumerable<KeyValuePair<SictAufgaabeZuusctand, T>> MengeAufgaabeZuusctandTailmengeMitSelectBerecne<T>(
			Func<SictAufgaabeZuusctand, KeyValuePair<bool,	T>> PrädikaatUndSelect,
			Int64? AufgaabeAbsclusTailWirkungZaitScrankeMin = null,
			int?	KomponenteTiifeScrankeMax	= null)
		{
			var MengeAufgaabeZuusctand = this.MengeAufgaabeZuusctand;

			if (null == MengeAufgaabeZuusctand)
			{
				return null;
			}

			if (null == PrädikaatUndSelect)
			{
				return null;
			}

			var MengeAufgaabeZuusctandTailmengeMitSelect = new List<KeyValuePair<SictAufgaabeZuusctand, T>>();

			foreach (var AufgaabeZuusctand in MengeAufgaabeZuusctand)
			{
				if (null == AufgaabeZuusctand)
				{
					continue;
				}

				var	AufgaabeZuusctandMengeKomponente	=
					Bib3.Glob.SuuceFlacMengeAst(
					AufgaabeZuusctand,
					(t) => true,
					(Ast) => Ast.MengeKomponenteBerecne(),
					null,
					KomponenteTiifeScrankeMax);

				foreach (var AufgaabeZuusctandKomponente in AufgaabeZuusctandMengeKomponente)
				{
					if (AufgaabeAbsclusTailWirkungZaitScrankeMin.HasValue)
					{
						var AufgaabeZuusctandKomponenteAbsclusTailWirkungZait = AufgaabeZuusctandKomponente.AbsclusTailWirkungZait;

						if (!(AufgaabeAbsclusTailWirkungZaitScrankeMin <= AufgaabeZuusctandKomponenteAbsclusTailWirkungZait))
						{
							continue;
						}
					}

					var EntscaidungZuugehöörigUndSelect = PrädikaatUndSelect(AufgaabeZuusctandKomponente);

					if (!EntscaidungZuugehöörigUndSelect.Key)
					{
						continue;
					}

					MengeAufgaabeZuusctandTailmengeMitSelect.Add(new KeyValuePair<SictAufgaabeZuusctand, T>(AufgaabeZuusctandKomponente, EntscaidungZuugehöörigUndSelect.Value));
				}
			}

			return MengeAufgaabeZuusctandTailmengeMitSelect;
		}

		IEnumerable<SictAufgaabeZuusctand> MengeAufgaabeZuusctandTailmengeBerecne(
			Func<SictAufgaabeZuusctand, bool> Prädikaat)
		{
			var MengeAufgaabeZuusctand = this.MengeAufgaabeZuusctand;

			var	MengeAufgaabe	=
				Bib3.Glob.SuuceFlacMengeAst(
				MengeAufgaabeZuusctand,
				Prädikaat,
				(Ast) => Ast.MengeKomponenteBerecne());

			if (null == MengeAufgaabe)
			{
				return null;
			}

			return MengeAufgaabe.Distinct();
		}

		override	public IEnumerable<SictAufgaabeZuusctand> MengeVersuucMenuEntryKlikErfolgAufgaabeBerecne()
		{
			return MengeAufgaabeZuusctandTailmengeBerecne(SictAufgaabeZuusctand.PrädikaatMenuEntryKlikErfolg);
		}

		override	public IEnumerable<SictWertMitZait<KeyValuePair<SictAufgaabeParam, VonSensor.MenuEntry[]>>> MengeVersuucMenuEntryKlikBerecne(
			bool NurFertige = false)
		{
			var MengeAufgaabe = MengeVersuucMenuEntryKlikErfolgAufgaabeBerecne();

			if (null == MengeAufgaabe)
			{
				return null;
			}

			return
				MengeAufgaabe.Select((Aufgaabe) => Aufgaabe.SictAufgaabeMenuPfaadErfolgErgeebnisBerecne(NurFertige))
				.Where((Ergeebnis) => Ergeebnis.HasValue)
				.Select((Ergeebnis) => Ergeebnis.Value);
		}

		override	public SictWertMitZait<KeyValuePair<SictAufgaabeParam, VonSensor.MenuEntry[]>>? VersuucMenuEntryKlikLezteBerecne(
			bool NurFertige = false)
		{
			var MengeVersuucMenuEntryKlik = MengeVersuucMenuEntryKlikBerecne(NurFertige);

			if (null == MengeVersuucMenuEntryKlik)
			{
				return null;
			}

			return MengeVersuucMenuEntryKlik.OrderBy((Aufgaabe) => Aufgaabe.Zait).LastOrDefault();
		}

		static readonly public int InRaumAktioonEndeZaitDistanzBisDiinstUnterbrecung = 60 * 15;
		static readonly public int MissionAktioonAcceptEndeZaitDistanzBisDiinstUnterbrecung = 60 * 60;
		static readonly public int MissionAktioonFüüreAusEndeZaitDistanzBisDiinstUnterbrecung = 60 * 70;

		static public SictPräferenzZuZaitVerhalte PräferenzZuZaitVerhalteSictAusDiinstUnterbrecungAblaitungScranke(
			SictPräferenzZuZaitVerhalte PräferenzZuZaitVerhalte,
			int? InRaumAktioonEndeZaitDistanzBisDiinstUnterbrecung,
			int? MissionAktioonAcceptEndeZaitDistanzBisDiinstUnterbrecung,
			int? MissionAktioonFüüreAusEndeZaitDistanzBisDiinstUnterbrecung)
		{
			if (null == PräferenzZuZaitVerhalte)
			{
				return null;
			}

			var VorherInRaumAktioonZaitScrankeMili = PräferenzZuZaitVerhalte.InRaumAktioonZaitScrankeMili;
			var VorherMissionAktioonAcceptZaitScrankeMili = PräferenzZuZaitVerhalte.MissionAktioonAcceptZaitScrankeMili;
			var VorherMissionAktioonFüüreAusZaitScrankeMili = PräferenzZuZaitVerhalte.MissionAktioonFüüreAusZaitScrankeMili;

			var PräferenzZuZaitVerhalteAbbild = PräferenzZuZaitVerhalte.Kopii();

			PräferenzZuZaitVerhalteAbbild.InRaumAktioonZaitScrankeMili =
				Bib3.Glob.Min(VorherInRaumAktioonZaitScrankeMili,
				(PräferenzZuZaitVerhalte.DiinstUnterbrecungNääxteZait - InRaumAktioonEndeZaitDistanzBisDiinstUnterbrecung) * 1000);

			PräferenzZuZaitVerhalteAbbild.MissionAktioonAcceptZaitScrankeMili =
				Bib3.Glob.Min(VorherMissionAktioonAcceptZaitScrankeMili,
				(PräferenzZuZaitVerhalte.DiinstUnterbrecungNääxteZait - MissionAktioonAcceptEndeZaitDistanzBisDiinstUnterbrecung) * 1000);

			PräferenzZuZaitVerhalteAbbild.MissionAktioonFüüreAusZaitScrankeMili =
				Bib3.Glob.Min(VorherMissionAktioonFüüreAusZaitScrankeMili,
				(PräferenzZuZaitVerhalte.DiinstUnterbrecungNääxteZait - MissionAktioonFüüreAusEndeZaitDistanzBisDiinstUnterbrecung) * 1000);

			return PräferenzZuZaitVerhalteAbbild;
		}

		override	public SictPräferenzZuZaitVerhalte PräferenzZuZaitVerhalteKombiBerecne()
		{
			var OptimatParam = this.OptimatParam();

			var VonNuzerParamZuZaitVerhalte = (null == OptimatParam) ? null : OptimatParam.ZuZaitVerhalte;

			var VonWirtParamZuZaitVerhalte = this.VonWirtParamZuZaitVerhalte;

			return
				PräferenzZuZaitVerhalteSictAusDiinstUnterbrecungAblaitungScranke(
				SictPräferenzZuZaitVerhalte.KombiniireNaacMinimum(VonNuzerParamZuZaitVerhalte, VonWirtParamZuZaitVerhalte),
				InRaumAktioonEndeZaitDistanzBisDiinstUnterbrecung,
				MissionAktioonAcceptEndeZaitDistanzBisDiinstUnterbrecung,
				MissionAktioonFüüreAusEndeZaitDistanzBisDiinstUnterbrecung);
		}

		override	public SictAusZuusctandAblaitungFürEntscaidungVorsclaagWirkung AusZuusctandAblaitungBerecne()
		{
			var InternZuusctand = this;

			var Ablaitung = new SictAusZuusctandAblaitungFürEntscaidungVorsclaagWirkung();

			var OptimatParam = InternZuusctand.OptimatParam();

			if (null == OptimatParam)
			{
				return Ablaitung;
			}

			var FittingUndShipZuusctand = (null == InternZuusctand) ? null : InternZuusctand.FittingUndShipZuusctand;

			var ShipZuusctandLezte = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.ShipZuusctand;

			var SelbsctShipDocked = (null == ShipZuusctandLezte) ? null : ShipZuusctandLezte.Docked;
			var SelbsctShipWarping = (null == ShipZuusctandLezte) ? null : ShipZuusctandLezte.Warping;
			var SelbsctShipWarpScrambled = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.SelbsctShipWarpScrambled;

			var SelbsctShipWarpingLezteZaitMili = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.WarpingLezteZaitMili;
			var SelbsctShipJumpingLezteZaitMili = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.JumpingLezteZaitMili;

			Ablaitung.VonNuzerParamAutoRouteFraigaabe = OptimatParam.AutoPilotFraigaabe ?? false;

			Ablaitung.SelbstShipWarpingLezteAlterMiliNulbar = NuzerZaitMili - SelbsctShipWarpingLezteZaitMili;
			Ablaitung.SelbstShipJumpingLezteAlterMiliNulbar = NuzerZaitMili - SelbsctShipJumpingLezteZaitMili;
			Ablaitung.SelbstShipUndockingLezteAlterMiliNulbar = NuzerZaitMili - InternZuusctand.SelbstShipUndockingLezteZaitMili;

			Ablaitung.SelbstShipWarpingOderJumpingLezteAlterMili =
				Math.Min(Ablaitung.SelbstShipWarpingLezteAlterMiliNulbar ?? int.MaxValue, Ablaitung.SelbstShipJumpingLezteAlterMiliNulbar ?? int.MaxValue);

			var FluctLezteAlterMiliNulbar = NuzerAlterMili<object>(InternZuusctand.FluctLezte);

			Ablaitung.FluctLezteAlterMili = FluctLezteAlterMiliNulbar ?? int.MaxValue;

			return Ablaitung;
		}

		override	public Int64? NuzerAlterMili<T>(SictWertMitZait<T>? WertMitZaitNuzer)
		{
			if (!WertMitZaitNuzer.HasValue)
			{
				return null;
			}

			var AlterMili = NuzerZaitMili - WertMitZaitNuzer.Value.Zait;

			return AlterMili;
		}

		static bool TempDebugVerglaicAutomaatZuusctandKopiiMitNewtonsoftJson = false;

		override	public SictAutomatZuusctand KopiiBerecne()
		{
			/*
			 * 16.04.15
			 * 
			var Param = new SictRefBaumKopiiParam(new SictRefBaumKopiiProfile(), SictAutomat.ZuusctandSictDiferenzSictParam.TypeBehandlungRictliinieMitScatescpaicer);

			var Kopii = SictRefNezKopii.ObjektKopiiErsctele(this, Param, null);

			return Kopii;
			*/

			//	This should do as well....
			return Bib3.RefNezDiferenz.Extension.ObjektKopiiKonstrukt(this, SictAutomat.ZuusctandSictDiferenzSictParam.TypeBehandlungRictliinieMitScatescpaicer);
		}

		public	void	VonNuzerListeBerictWindowClientRasterFüügeAin(IEnumerable<SictWertMitZait<SictDataiIdentUndSuuceHinwais>> VonNuzerBerictMengeWindowClientRaster)
		{
			if(null	== VonNuzerBerictMengeWindowClientRaster)
			{
				return;
			}

			var TempVonNuzerListeBerictWindowClientRaster = this.TempVonNuzerListeBerictWindowClientRaster;

			if (null == TempVonNuzerListeBerictWindowClientRaster)
			{
				this.TempVonNuzerListeBerictWindowClientRaster = TempVonNuzerListeBerictWindowClientRaster = new List<SictWertMitZait<SictDataiIdentUndSuuceHinwais>>();
			}

			TempVonNuzerListeBerictWindowClientRaster.AddRange(VonNuzerBerictMengeWindowClientRaster);

			TempVonNuzerListeBerictWindowClientRaster.ListeKürzeBegin(4);
		}
	}

}
