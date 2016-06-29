using System;
using System.Collections.Generic;
using Bib3;
using Newtonsoft.Json;
using Optimat.EveOnline.Base;
using Optimat.EveOnline.Anwendung.AuswertGbs;
//using Optimat.EveOnline.AuswertGbs;
using Optimat.EveOnline.VonSensor;
using Optimat.ScpezEveOnln;
using VonSensor = Optimat.EveOnline.VonSensor;
using Optimat.EveO.Nuzer.TempBot.Sonst;
using Optimat.EveOnline.CustomBot;
using ExtractFromOldAssembly.Bib3;

namespace Optimat.EveOnline.Anwendung
{
	public interface ISictAutomatZuusctandMitErwaiterungFürBerict : ISictAutomatZuusctand, ISictAutomatZuusctandErwaiterungFürBerict
	{
	}

	public interface ISictAutomatZuusctandErwaiterungFürBerict
	{
		byte[] AnwendungSizungIdent
		{
			set;
			get;
		}
	}

	public	interface ISictAutomatZuusctand
	{
		ToCustomBotSnapshot VonSensorScnapscus { set;  get; }

		global::Optimat.EveOnline.Anwendung.SictAgentUndMissionZuusctand AgentUndMission { get; }
		
		int ScritDauerDurcscnit();

		SictPräferenzZuZaitVerhalte VonWirtParamZuZaitVerhalte
		{
			set;
			get;
		}

		global::Optimat.SictWertMitZait<VonSensor.MenuEntry> AufgaabeMenuEntryAbgescloseTailWirkungLezteMitZait { get; }
		void AufgaabeScrit(long Zait, global::Optimat.EveOnline.Anwendung.SictAufgaabeZuusctand Aufgaabe, global::Bib3.SictIdentInt64Fabrik AufgaabeIdentFabrik, global::Optimat.ScpezEveOnln.SictAufgaabeKombiZuusctand KombiZuusctand);
		SictAusGbsScnapscusAuswertungSrv AusListeScnapscusVorLezte();
		global::System.Collections.Generic.IEnumerable<GbsElement> AusListeScnapscusVorLezteMengeAuswertungErgeebnisZuAstMitHerkunftAdrese(long AstHerkunftAdrese);
		global::Optimat.ScpezEveOnln.SictAusZuusctandAblaitungFürEntscaidungVorsclaagWirkung AusZuusctandAblaitungBerecne();
		global::Optimat.EveOnline.Anwendung.SictAutoMine AutoMine { get; }
		//global::Optimat.EveOnline.SictEveWeltTopologii EveWeltTopologii { get; }
		global::Optimat.EveOnline.Anwendung.SictShipZuusctandMitFitting FittingUndShipZuusctand { get; }
		global::Optimat.EveOnline.Anwendung.SictGbsZuusctand Gbs { get; }
		global::System.Collections.Generic.IEnumerable<global::Optimat.EveOnline.Anwendung.SictAufgaabeParam> GbsAstOklusioonVermaidungBerecne(global::Optimat.EveOnline.Anwendung.SictAufgaabeParamGbsAstOklusioonVermaidung OklusioonVermaidungParam);
		global::System.Collections.Generic.IEnumerable<global::Optimat.EveOnline.Anwendung.SictAufgaabeParam> GbsAstOklusioonVermaidungBerecne(global::Optimat.EveOnline.Anwendung.SictAufgaabeParamGbsAstOklusioonVermaidung OklusioonVermaidungParam, out global::System.Collections.Generic.IEnumerable<SictFläceRectekOrtoAbhängigVonGbsAst> MengeFläceZuMaide);
		global::System.Collections.Generic.IEnumerable<global::Optimat.EveOnline.Anwendung.SictAufgaabeParam> GbsAstOklusioonVermaidungBerecne(global::Optimat.EveOnline.Anwendung.SictAufgaabeParamGbsAstOklusioonVermaidung OklusioonVermaidungParam, out global::System.Collections.Generic.IEnumerable<SictFläceRectekOrtoAbhängigVonGbsAst> MengeFläceZuMaide, out global::System.Collections.Generic.IEnumerable<OrtogoonInt> NaacOklusioonRestMengeFläce);
		global::Optimat.SictWertMitZait<VonSensor.Menu>[] GbsListeMenuNocOfeMitBeginZaitBerecne();
		global::Optimat.EveOnline.Anwendung.SictInRaumAktioonUndGefect InRaumAktioonUndGefect { get; }
		global::Optimat.ScpezEveOnln.SictAutomatZuusctand KopiiBerecne();
		global::System.Collections.Generic.IEnumerable<global::Optimat.SictVerlaufBeginUndEndeRef<Optimat.EveOnline.Anwendung.AuswertGbs.ShipUiIndicationAuswert>> ListeAusShipUIIndicationMitZait { get; }
		global::System.Collections.Generic.List<global::Optimat.SictWertMitZait<global::Optimat.ScpezEveOnln.SictInventoryItemTransport>> ListeInventoryItemTransportMitZait { get; }
		SictAusGbsScnapscusAuswertungSrv ListeScnapscusLezteAuswertungErgeebnisNaacSimu { get; }
		global::System.Collections.Generic.IEnumerable<global::Optimat.ScpezEveOnln.ZuScritInfoZait> ListeScritZait { get; }
		global::System.Collections.Generic.IEnumerable<global::Optimat.SictWertMitZait<global::Optimat.EveOnline.SictVorsclaagNaacProcessWirkung>> ListeVorsclaagWirkungErfolgraic();
		global::System.Collections.Generic.List<global::System.Collections.Generic.KeyValuePair<global::Optimat.EveOnline.Anwendung.SictGbsWindowZuusctand, long>> ListeZuWindowNuzungLezteWindowScritAnzaal { get; }
		global::System.Collections.Generic.List<global::Optimat.SictWertMitZait<global::Optimat.EveOnline.SictVorsclaagNaacProcessWirkung>> ListeZuZaitNaacNuzerVorsclaagNaacProcessWirkung { get; }
		global::System.Collections.Generic.List<global::Optimat.SictWertMitZait<global::Optimat.EveOnline.SictNaacProcessWirkung>> ListeZuZaitVonNuzerMeldungNaacProcessWirkung { get; }
		global::Optimat.EveOnline.Anwendung.SictAufgaabeZuusctand ManööverAusgefüürtLezteAufgaabeBerecne();
		global::Optimat.EveOnline.Anwendung.SictAufgaabeZuusctand ManööverAusgefüürtLezteAufgaabeBerecne(global::Optimat.EveOnline.Anwendung.SictOverViewObjektZuusctand OverviewObjekt, global::Optimat.EveOnline.Anwendung.SictTargetZuusctand Target, SictZuInRaumObjektManööverTypEnum ManööverTyp, long? DistanceScrankeMin = null, long? DistanceScrankeMax = null, bool BedingungNocAktiiv = false);
		global::System.Collections.Generic.List<global::Optimat.EveOnline.Anwendung.SictAufgaabeZuusctand> MengeAufgaabeZuusctand { get; set; }
		global::System.Collections.Generic.IEnumerable<global::Optimat.ScpezEveOnln.SictAutomatZuusctand.SictGbsAstOklusioonInfo> MengeKandidaatOklusioonBerecne();
		global::Optimat.EveOnline.Anwendung.SictShipUiModuleReprZuusctand[] MengeModuleUmscaltFraigaabe { get; }
		global::System.Collections.Generic.IEnumerable<global::Optimat.SictWertMitZait<global::System.Collections.Generic.KeyValuePair<global::Optimat.EveOnline.Anwendung.SictAufgaabeParam, VonSensor.MenuEntry[]>>> MengeVersuucMenuEntryKlikBerecne(bool NurFertige = false);
		global::System.Collections.Generic.IEnumerable<global::Optimat.EveOnline.Anwendung.SictAufgaabeZuusctand> MengeVersuucMenuEntryKlikErfolgAufgaabeBerecne();
		global::System.Collections.Generic.List<global::Optimat.SictWertMitZait<long>> MengeVonNuzerMeldungWirkungErfolgZuZait { get; set; }
		global::System.Collections.Generic.IEnumerable<global::Optimat.SictWertMitZait<long>> MengeVonNuzerMeldungWirkungErfolgZuZaitBerecne();
		global::Optimat.EveOnline.Anwendung.SictGbsWindowZuusctand[] MengeWindowWirkungGescpert { get; }
		global::Optimat.EveOnline.Anwendung.SictGbsWindowZuusctand[] MengeWindowZuErhalte { get; }
		global::Optimat.EveOnline.SictVonOptimatMeldungZuusctand NaacNuzerMeldungZuusctand { get; }
		long? NuzerAlterMili<T>(global::Optimat.SictWertMitZait<T>? WertMitZaitNuzer);
		long NuzerZaitMili { set; get; }

		/*
		 * 2014.10.15
		 * 
		void AingangNaacProcessWirkung(long Zait, global::Optimat.EveOnline.SictNaacProcessWirkung Wirkung);

		global::Optimat.ScpezEveOnln.AuswertGbs.SictGbsAstInfoSictAuswert OptimatScritAktuelGbsBaum { get; }
		long? OptimatScritAktuelVonZiilProcessLeeseBeginZaitMili { get; }
		global::Optimat.EveOnline.Anwendung.SictVonZiilProcessLeeseSictAuswert OptimatScritAktuelVonZiilProcessLeese();
		 * */

		global::Optimat.EveOnline.Anwendung.SictOverviewUndTargetZuusctand OverviewUndTarget { get; }
		global::Optimat.EveOnline.SictPräferenzZuZaitVerhalte PräferenzZuZaitVerhalteKombiBerecne();
		int? ScritLezteIndex { get; }
		global::System.Collections.Generic.KeyValuePair<global::Optimat.EveOnline.Anwendung.SictAufgaabeZuusctand, string>[] ScritLezteListeAufgaabeBerecnet { get; }
		global::Optimat.ScpezEveOnln.SictAufgaabeKombiZuusctand ScritLezteListeAufgaabeKombiZuusctand { get; }
		global::Optimat.EveOnline.Anwendung.SictAufgaabeGrupePrio[] ScritLezteListePrioMengeAufgaabe { get; }
		global::System.Collections.Generic.KeyValuePair<global::Optimat.EveOnline.Anwendung.SictAufgaabeZuusctand[], string>[] ScritLezteWirkungMengeAufgaabePfaadZuBlatMitGrupePrioNaame { get; }
		long ServerZaitMili { set;  get; }
		global::Optimat.SictWertMitZait<VonSensor.UtilmenuMissionInfo>? UtilmenuMissionLezte { get; }
		global::Optimat.SictWertMitZait<global::System.Collections.Generic.KeyValuePair<global::Optimat.EveOnline.Anwendung.SictAufgaabeParam, VonSensor.MenuEntry[]>>? VersuucMenuEntryKlikLezteBerecne(bool NurFertige = false);
		global::Optimat.ScpezEveOnln.SictAutomatZuusctand.SictGbsAstOklusioonInfo ZuGbsAstHerkunftAdreseKandidaatOklusioonBerecne(long GbsAstHerkunftAdrese);

		void Update();

		/*
		 * 2015.03.12
		 * 
		 * Ersaz durc ToCustomBotSnapshot
		SictGbsAstInfoSictAuswert VonNuzerMeldungZuusctandTailGbsBaum
		{
			set;
			get;
		}
		 * */

		Int64? VorsclaagWirkungAusgefüürtNictLezteAlterBerecne();

		SictNaacOptimatMeldungZuusctand VonNuzerMeldungZuusctand
		{
			set;
			get;
		}

		SictOptimatParam OptimatParam();

		SictPräferenzZuZaitVerhalte ParamZuZaitVerhalteKombi
		{
			set;
			get;
		}

		SictAusGbsScnapscusAuswertungSrv ListeScnapscusLezteAuswertungErgeebnis
		{
			set;
			get;
		}

		List<SictAusGbsScnapscusAuswertungSrv> ListeScnapscusAuswertungErgeebnisNaacSimu
		{
			set;
			get;
		}

		Int64? AnnaameActiveShipCargoGeneralLeerLezteZaitMili
		{
			set;
			get;
		}

		Int64? AnforderungActiveShipCargoGeneralLeereLezteZaitMili
		{
			set;
			get;
		}

		SictWertMitZait<ShipHitpointsAndEnergy> VorhersaageSelbstScifTreferpunkte
		{
			get;
		}

		IEnumerable<SictVerlaufBeginUndEndeRef<VonSensor.Message>> ListeAusGbsAbovemainMessageMitZait();

		VonSensor.WindowOverView WindowOverviewScnapscusLezte();

		IEnumerable<VonSensor.Tab> WindowOverviewScnapscusLezteListeTabNuzbar();

		VonSensor.Tab	WindowOverviewScnapscusLezteTabAktiiv();

		VonSensor.Scroll WindowOverviewScnapscusLezteScroll();

		Int64? OverViewScrolledToTopLezteZait();

		Int64? OverViewScrolledToTopLezteAlter();

		Int64? ShipSctrekeZurükgeleegtMiliInZaitraum(Int64 ZaitraumBegin, Int64 ZaitraumEnde);

		IEnumerable<VerbindungListEntryUndTargetPerDistance> MengeZuListEntryTargetAinscrankungPerDistance();

		IEnumerable<VerbindungListEntryUndOverviewObjektPerDistance> MengeZuListEntryOverviewObjekt();

		IEnumerable<SictGbsWindowZuusctand> MengeWindowZuusctand();

		IEnumerable<SictGbsWindowZuusctand> MengeWindowZuusctand<WindowScnapscusType>();

		IEnumerable<GbsListGroupedEntryZuusctand> MengeListEntryZuusctand();

		int? TargetInputFookusTransitioonLezteAlterScritAnzaal();

		int? TargetInputFookusTransitioonLezteScritIndex();

		ZuOreTypAusSurveyScanInfo ZuOreTypSictStringSurveyScanInfo(string OreTypSictString);

		IEnumerable<ZuTargetAinscrankungMengeSurveyScanItem> MengeZuTargetSurveyScanInfo();
		ZuTargetAinscrankungMengeSurveyScanItem ZuTargetSurveyScanInfo(SictTargetZuusctand Target);
		IEnumerable<SictTargetZuusctand> ZuSurveyScanEntryMengeKandidaatTarget(GbsListGroupedEntryZuusctand SurveyScanEntry);

		SictWertMitZait<bool>? MesungShipIsPodLezteZaitUndWert();

		IEnumerable<SictVerlaufBeginUndEndeRef<string>> ListeLocationNearest();

		Int64? LocationNearestLezteZait(string LocationNearest);

		Optimat.EveOnline.SictEveWeltTopologii EveWeltTopologii
		{
			set;
			get;
		}

		SictSolarSystem EveWeltSolarSystemCurrent();

		Int64? ShipWarpingLezteZaitMili();
		Int64? ShipDockedLezteZaitMili();
		Int64? ShipWarpingLezteAlterMili();
		Int64? ShipDockedLezteAlterMili();
	}

	[JsonObject(MemberSerialization.OptIn)]
	public abstract class SictAutomatZuusctandWaiterlaitung : Optimat.EveOnline.Anwendung.ISictAutomatZuusctand
	{
		[JsonProperty]
		public ToCustomBotSnapshot VonSensorScnapscus
		{
			set;
			get;
		}

		public SictAutomatZuusctandWaiterlaitung()
		{
			ListeScnapscusAuswertungErgeebnisNaacSimu = new List<SictAusGbsScnapscusAuswertungSrv>();
		}

		virtual	public int ScritDauerDurcscnit()
		{
			return ScritDauerDurcscnitBerecne(this.ListeScritZait);
		}

		static	public int ScritDauerDurcscnitBerecne(
			IEnumerable<ZuScritInfoZait> ListeScritZait)
		{
			if (null == ListeScritZait)
			{
				return 0;
			}

			var ScritLezteZaitNuzer =
				ListeScritZait
				.LastOrDefaultNullable().NuzerZait;

			var ZaitraumBerüksictigtDauer = 30000;

			var ListeScritZaitBescrankt =
				ListeScritZait
				.SkipWhileNullable((ScritZait) => ZaitraumBerüksictigtDauer < Math.Abs(ScritLezteZaitNuzer - ScritZait.NuzerZait))
				.ToArrayNullable();

			if (!(1 < ListeScritZaitBescrankt.CountNullable()))
			{
				return ZaitraumBerüksictigtDauer;
			}

			var FrüühesteZait = ListeScritZaitBescrankt[0].NuzerZait;

			var LezteZait = ListeScritZaitBescrankt.LastOrDefaultNullable().NuzerZait;

			var Durcscnit = (int)((LezteZait - FrüühesteZait) / (ListeScritZaitBescrankt.Length - 1));

			return Durcscnit;
		}

		public Int64? VorsclaagWirkungAusgefüürtNictLezteAlterBerecne()
		{
			var Alter = NuzerZaitMili - VorsclaagWirkungAusgefüürtNictLezteZaitBerecne();

			return Alter;
		}

		Int64? VorsclaagWirkungAusgefüürtNictLezteZaitBerecne()
		{
			var ListeZuZaitNaacNuzerVorsclaagNaacProcessWirkung = this.ListeZuZaitNaacNuzerVorsclaagNaacProcessWirkung;
			var VonNuzerMeldungZuusctand = this.VonNuzerMeldungZuusctand;

			if (null == ListeZuZaitNaacNuzerVorsclaagNaacProcessWirkung)
			{
				return null;
			}

			foreach (var ZuZaitNaacNuzerVorsclaagNaacProcessWirkung in ListeZuZaitNaacNuzerVorsclaagNaacProcessWirkung.Reversed())
			{
				var VorsclaagWirkung = ZuZaitNaacNuzerVorsclaagNaacProcessWirkung.Wert;

				if (null == VorsclaagWirkung)
				{
					continue;
				}

				if (null != VonNuzerMeldungZuusctand)
				{
					var Wirkung =
						VonNuzerMeldungZuusctand.ListeNaacProcessWirkung
						.FirstOrDefaultNullable((Kandidaat) => (null == Kandidaat) ? false : Kandidaat.VorsclaagWirkungIdent == VorsclaagWirkung.Ident);

					if (null != Wirkung)
					{
						continue;
					}
				}

				return ZuZaitNaacNuzerVorsclaagNaacProcessWirkung.Zait;
			}

			return null;
		}

		public SictWertMitZait<ShipHitpointsAndEnergy> VorhersaageSelbstScifTreferpunkte
		{
			get
			{
				var FittingUndShipZuusctand = this.FittingUndShipZuusctand();

				if (null == FittingUndShipZuusctand)
				{
					return default(SictWertMitZait<ShipHitpointsAndEnergy>);
				}

				return FittingUndShipZuusctand.VorhersaageSelbstScifTreferpunkte;
			}
		}

		[JsonProperty]
		public SictNaacOptimatMeldungZuusctand VonNuzerMeldungZuusctand
		{
			set;
			get;
		}

		/*
		 * 2015.03.12
		 * 
		 * Ersaz durc ToCustomBotSnapshot
		[JsonProperty]
		public	SictGbsAstInfoSictAuswert VonNuzerMeldungZuusctandTailGbsBaum
		{
			set;
			get;
		}
		 * */

		[JsonProperty]
		public SictVonOptimatMeldungZuusctand NaacNuzerMeldungZuusctand
		{
			set;
			get;
		}

		public	SictOptimatParam OptimatParam()
		{
			var VonNuzerMeldungZuusctand = this.VonNuzerMeldungZuusctand;

			if (null == VonNuzerMeldungZuusctand)
			{
				return null;
			}

			return VonNuzerMeldungZuusctand.OptimatParam;
		}

		[JsonProperty]
		public SictPräferenzZuZaitVerhalte VonWirtParamZuZaitVerhalte
		{
			set;
			get;
		}

		[JsonProperty]
		public SictAusGbsScnapscusAuswertungSrv ListeScnapscusLezteAuswertungErgeebnis
		{
			set;
			get;
		}

		abstract public SictAgentUndMissionZuusctand AgentUndMission
		{
			set;
			get;
		}

		abstract public void Update();

		[JsonProperty]
		public List<SictAusGbsScnapscusAuswertungSrv> ListeScnapscusAuswertungErgeebnisNaacSimu
		{
			set;
			get;
		}

		public SictAusGbsScnapscusAuswertungSrv ListeScnapscusLezteAuswertungErgeebnisNaacSimu
		{
			get
			{
				return ListeScnapscusAuswertungErgeebnisNaacSimu.LastOrDefaultNullable();
			}
		}

		abstract public SictWertMitZait<VonSensor.MenuEntry> AufgaabeMenuEntryAbgescloseTailWirkungLezteMitZait
		{
			set;
			get;
		}

		abstract public void AufgaabeScrit(long Zait, SictAufgaabeZuusctand Aufgaabe, SictIdentInt64Fabrik AufgaabeIdentFabrik, SictAufgaabeKombiZuusctand KombiZuusctand);

		abstract public SictAusGbsScnapscusAuswertungSrv AusListeScnapscusVorLezte();

		abstract public IEnumerable<GbsElement> AusListeScnapscusVorLezteMengeAuswertungErgeebnisZuAstMitHerkunftAdrese(long AstHerkunftAdrese);

		abstract public SictAusZuusctandAblaitungFürEntscaidungVorsclaagWirkung AusZuusctandAblaitungBerecne();

		abstract public SictAutoMine AutoMine
		{
			set;
			get;
		}

		abstract public SictShipZuusctandMitFitting FittingUndShipZuusctand
		{
			set;
			get;
		}

		abstract public SictGbsZuusctand Gbs
		{
			set;
			get;
		}

		abstract public IEnumerable<SictAufgaabeParam> GbsAstOklusioonVermaidungBerecne(SictAufgaabeParamGbsAstOklusioonVermaidung OklusioonVermaidungParam);

		abstract public IEnumerable<SictAufgaabeParam> GbsAstOklusioonVermaidungBerecne(SictAufgaabeParamGbsAstOklusioonVermaidung OklusioonVermaidungParam, out IEnumerable<SictFläceRectekOrtoAbhängigVonGbsAst> MengeFläceZuMaide);

		abstract public IEnumerable<SictAufgaabeParam> GbsAstOklusioonVermaidungBerecne(SictAufgaabeParamGbsAstOklusioonVermaidung OklusioonVermaidungParam, out IEnumerable<SictFläceRectekOrtoAbhängigVonGbsAst> MengeFläceZuMaide, out IEnumerable<OrtogoonInt> NaacOklusioonRestMengeFläce);

		public SictWertMitZait<VonSensor.Menu>[] GbsListeMenuNocOfeMitBeginZaitBerecne()
		{
			var Gbs = this.Gbs;

			if (null == Gbs)
			{
				return null;
			}

			var MenuKaskaadeLezteNocOfe = Gbs.MenuKaskaadeLezteNocOfe;

			if (null == MenuKaskaadeLezteNocOfe)
			{
				return null;
			}

			return MenuKaskaadeLezteNocOfe.ListeMenuScnapscusLezteMitBeginZaitBerecne().ToArrayNullable();
		}

		abstract public SictInRaumAktioonUndGefect InRaumAktioonUndGefect
		{
			set;
			get;
		}

		abstract public SictAutomatZuusctand KopiiBerecne();

		abstract public IEnumerable<SictVerlaufBeginUndEndeRef<Optimat.EveOnline.Anwendung.AuswertGbs.ShipUiIndicationAuswert>> ListeAusShipUIIndicationMitZait
		{
			get;
		}

		abstract public List<SictWertMitZait<SictInventoryItemTransport>> ListeInventoryItemTransportMitZait
		{
			set;
			get;
		}

		abstract public IEnumerable<ZuScritInfoZait> ListeScritZait
		{
			get;
		}

		abstract public IEnumerable<SictWertMitZait<SictVorsclaagNaacProcessWirkung>> ListeVorsclaagWirkungErfolgraic();

		abstract public List<KeyValuePair<SictGbsWindowZuusctand, long>> ListeZuWindowNuzungLezteWindowScritAnzaal
		{
			set;
			get;
		}

		abstract public List<SictWertMitZait<SictVorsclaagNaacProcessWirkung>> ListeZuZaitNaacNuzerVorsclaagNaacProcessWirkung
		{
			set;
			get;
		}

		abstract public List<SictWertMitZait<SictNaacProcessWirkung>> ListeZuZaitVonNuzerMeldungNaacProcessWirkung
		{
			set;
			get;
		}

		abstract public SictAufgaabeZuusctand ManööverAusgefüürtLezteAufgaabeBerecne();

		abstract public SictAufgaabeZuusctand ManööverAusgefüürtLezteAufgaabeBerecne(SictOverViewObjektZuusctand OverviewObjekt, SictTargetZuusctand Target, SictZuInRaumObjektManööverTypEnum ManööverTyp, long? DistanceScrankeMin = null, long? DistanceScrankeMax = null, bool BedingungNocAktiiv = false);

		abstract public List<SictAufgaabeZuusctand> MengeAufgaabeZuusctand
		{
			get;
			set;
		}

		abstract public IEnumerable<SictAutomatZuusctand.SictGbsAstOklusioonInfo> MengeKandidaatOklusioonBerecne();

		abstract public SictShipUiModuleReprZuusctand[] MengeModuleUmscaltFraigaabe
		{
			set;
			get;
		}

		abstract public IEnumerable<SictWertMitZait<KeyValuePair<SictAufgaabeParam, VonSensor.MenuEntry[]>>> MengeVersuucMenuEntryKlikBerecne(bool NurFertige = false);

		abstract public IEnumerable<SictAufgaabeZuusctand> MengeVersuucMenuEntryKlikErfolgAufgaabeBerecne();

		abstract public List<SictWertMitZait<long>> MengeVonNuzerMeldungWirkungErfolgZuZait
		{
			get;
			set;
		}

		abstract public IEnumerable<SictWertMitZait<long>> MengeVonNuzerMeldungWirkungErfolgZuZaitBerecne();

		abstract public SictGbsWindowZuusctand[] MengeWindowWirkungGescpert
		{
			set;
			get;
		}

		abstract public SictGbsWindowZuusctand[] MengeWindowZuErhalte
		{
			set;
			get;
		}

		abstract public long? NuzerAlterMili<T>(SictWertMitZait<T>? WertMitZaitNuzer);

		[JsonProperty]
		public long NuzerZaitMili
		{
			set;
			get;
		}

		[JsonProperty]
		public long ServerZaitMili
		{
			set;
			get;
		}

		[JsonProperty]
		public KeyValuePair<Int64, Int64>[] TempDebugListeScritBerecneStopwatchZaitUndNuzerZait
		{
			set;
			get;
		}

		abstract public SictOverviewUndTargetZuusctand OverviewUndTarget
		{
			set;
			get;
		}

		abstract public SictPräferenzZuZaitVerhalte PräferenzZuZaitVerhalteKombiBerecne();

		abstract public int? ScritLezteIndex
		{
			get;
		}

		abstract public KeyValuePair<SictAufgaabeZuusctand, string>[] ScritLezteListeAufgaabeBerecnet
		{
			set;
			get;
		}

		abstract public SictAufgaabeKombiZuusctand ScritLezteListeAufgaabeKombiZuusctand
		{
			set;
			get;
		}

		abstract public SictAufgaabeGrupePrio[] ScritLezteListePrioMengeAufgaabe
		{
			set;
			get;
		}

		abstract public KeyValuePair<SictAufgaabeZuusctand[], string>[] ScritLezteWirkungMengeAufgaabePfaadZuBlatMitGrupePrioNaame
		{
			set;
			get;
		}

		abstract public SictWertMitZait<VonSensor.UtilmenuMissionInfo>? UtilmenuMissionLezte
		{
			set;
			get;
		}

		abstract public SictWertMitZait<KeyValuePair<SictAufgaabeParam, VonSensor.MenuEntry[]>>? VersuucMenuEntryKlikLezteBerecne(bool NurFertige = false);

		abstract public SictAutomatZuusctand.SictGbsAstOklusioonInfo ZuGbsAstHerkunftAdreseKandidaatOklusioonBerecne(long GbsAstHerkunftAdrese);

		/*
		 * 2014.10.15
		 * 
		abstract public SictOptimatParam OptimatParam
		{
			get;
			set;
		}
		 * */

		abstract public SictPräferenzZuZaitVerhalte ParamZuZaitVerhalteKombi
		{
			get;
			set;
		}

		abstract public long? AnnaameActiveShipCargoGeneralLeerLezteZaitMili
		{
			get;
			set;
		}

		abstract public long? AnforderungActiveShipCargoGeneralLeereLezteZaitMili
		{
			get;
			set;
		}

		abstract public IEnumerable<SictVerlaufBeginUndEndeRef<VonSensor.Message>> ListeAusGbsAbovemainMessageMitZait();

		public VonSensor.WindowOverView WindowOverviewScnapscusLezte()
		{
			var WindowOverview = this.WindowOverView();

			if (null == WindowOverview)
			{
				return null;
			}

			return	WindowOverview.AingangScnapscusTailObjektIdentLezteBerecne() as VonSensor.WindowOverView;
		}

		public IEnumerable<VonSensor.Tab> WindowOverviewScnapscusLezteListeTabNuzbar()
		{
			var WindowOverviewScnapscus = WindowOverviewScnapscusLezte();

			if (null == WindowOverviewScnapscus)
			{
				return null;
			}

			return WindowOverviewScnapscus.ListeTabNuzbar;
		}

		public VonSensor.Scroll WindowOverviewScnapscusLezteScroll()
		{
			var WindowOverviewScnapscus = WindowOverviewScnapscusLezte();

			if (null == WindowOverviewScnapscus)
			{
				return null;
			}

			return WindowOverviewScnapscus.Scroll;
		}

		public VonSensor.Tab WindowOverviewScnapscusLezteTabAktiiv()
		{
			var WindowOverviewScnapscus = WindowOverviewScnapscusLezte();

			if (null == WindowOverviewScnapscus)
			{
				return null;
			}

			var WindowOverviewScnapscusTabGroup = WindowOverviewScnapscus.TabGroup;

			if (null == WindowOverviewScnapscusTabGroup)
			{
				return null;
			}

			return WindowOverviewScnapscusTabGroup.TabSelected;
		}

		public Int64? OverViewScrolledToTopLezteZait()
		{
			var OverviewUndTarget = this.OverviewUndTarget;

			if (null == OverviewUndTarget)
			{
				return null;
			}

			return	OverviewUndTarget.OverViewScrolledToTopLezteZait;
		}

		public Int64? OverViewScrolledToTopLezteAlter()
		{
			return NuzerZaitMili - OverViewScrolledToTopLezteZait();
		}

		public Int64? ShipSctrekeZurükgeleegtMiliInZaitraum(
			Int64	ZaitraumBegin,
			Int64	ZaitraumEnde)
		{
			var FittingUndShipZuusctand = this.FittingUndShipZuusctand;

			if (null == FittingUndShipZuusctand)
			{
				return null;
			}

			return FittingUndShipZuusctand.SctrekeZurükgeleegtMiliInZaitraum(ZaitraumBegin, ZaitraumEnde);
		}

		abstract public IEnumerable<VerbindungListEntryUndTargetPerDistance> MengeZuListEntryTargetAinscrankungPerDistance();

		abstract public IEnumerable<VerbindungListEntryUndOverviewObjektPerDistance> MengeZuListEntryOverviewObjekt();

		public IEnumerable<SictGbsWindowZuusctand> MengeWindowZuusctand()
		{
			var Gbs = this.Gbs;

			if (null == Gbs)
			{
				return null;
			}

			return Gbs.MengeWindow;
		}

		public IEnumerable<SictGbsWindowZuusctand> MengeWindowZuusctand<WindowScnapscusType>()
		{
			return MengeWindowZuusctand()
				.WhereNullable((Kandidaat) => Kandidaat.AingangScnapscusTailObjektIdentLezteBerecne() is WindowScnapscusType);
		}

		public IEnumerable<GbsListGroupedEntryZuusctand> MengeListEntryZuusctand()
		{
			var MengeWindow = this.MengeWindowZuusctand();

			var Menge = new List<GbsListGroupedEntryZuusctand>();

			if (null == MengeWindow)
			{
				return null;
			}

			foreach (var WindowZuusctand in MengeWindow)
			{
				var WindowZuusctandListHaupt = WindowZuusctand.ListHaupt;

				if (null == WindowZuusctandListHaupt)
				{
					continue;
				}

				Menge.AddRange(WindowZuusctandListHaupt.ListeEntry());
			}

			return Menge;
		}

		public int? TargetInputFookusTransitioonLezteAlterScritAnzaal()
		{
			return ScritLezteIndex - TargetInputFookusTransitioonLezteScritIndex();
		}

		public int? TargetInputFookusTransitioonLezteScritIndex()
		{
			var	MengeTarget	= this.MengeTarget();

			if (null == MengeTarget)
			{
				return null;
			}

			Int64?	InputFookusTransitioonLezteZait	= null;

			foreach (var Target in MengeTarget)
			{
				if (null == Target)
				{
					continue;
				}

				InputFookusTransitioonLezteZait = Bib3.Glob.Max(InputFookusTransitioonLezteZait, Target.InputFookusTransitioonLezteZait);
			}

			if (!InputFookusTransitioonLezteZait.HasValue)
			{
				return null;
			}

			return this.ZuNuzerZaitBerecneScritIndex(InputFookusTransitioonLezteZait.Value);
		}

		abstract public IEnumerable<ZuOreTypAusSurveyScanInfo> MengeZuOreTypSictStringSurveyScanInfo();

		public ZuOreTypAusSurveyScanInfo ZuOreTypSictStringSurveyScanInfo(string OreTypSictString)
		{
			return
				this.MengeZuOreTypSictStringSurveyScanInfo()
				.FirstOrDefaultNullable((Kandidaat) => ZuOreTypAusSurveyScanInfo.HinraicendGlaicwertigFürFortsaz(Kandidaat.OreTypSictString, OreTypSictString));
		}

		abstract public IEnumerable<ZuTargetAinscrankungMengeSurveyScanItem> MengeZuTargetSurveyScanInfo();

		public ZuTargetAinscrankungMengeSurveyScanItem ZuTargetSurveyScanInfo(SictTargetZuusctand Target)
		{
			return
				this.MengeZuTargetSurveyScanInfo()
				.FirstOrDefaultNullable((Kandidaat) => Kandidaat.Target == Target);
		}

		public IEnumerable<SictTargetZuusctand> ZuSurveyScanEntryMengeKandidaatTarget(GbsListGroupedEntryZuusctand SurveyScanEntry)
		{
			return
				this.MengeZuTargetSurveyScanInfo()
				.WhereNullable((Kandidaat) => Kandidaat.MengeKandidaatListItem.ContainsNullable(SurveyScanEntry))
				.SelectNullable((Kandidaat) => Kandidaat.Target);
		}

		public SictWertMitZait<bool>? MesungShipIsPodLezteZaitUndWert()
		{
			var FittingUndShipZuusctand = this.FittingUndShipZuusctand;

			if (null == FittingUndShipZuusctand)
			{
				return null;
			}

			return FittingUndShipZuusctand.MesungShipIsPodLezteZaitUndWert;
		}

		abstract public IEnumerable<SictVerlaufBeginUndEndeRef<string>> ListeLocationNearest();

		public Int64? LocationNearestLezteZait(string LocationNearest)
		{
			var ListeLocationNearest = this.ListeLocationNearest();

			if (null == ListeLocationNearest)
			{
				return null;
			}

			var Lezte =
				ListeLocationNearest
				.LastOrDefaultNullable((Kandidaat) => string.Equals(Kandidaat.Wert, LocationNearest, StringComparison.InvariantCultureIgnoreCase));

			if (null == Lezte)
			{
				return null;
			}

			return Lezte.EndeZait ?? NuzerZaitMili;
		}

		abstract public Optimat.EveOnline.SictEveWeltTopologii EveWeltTopologii
		{
			set;
			get;
		}

		public SictSolarSystem EveWeltSolarSystemCurrent()
		{
			var EveWeltTopologii = this.EveWeltTopologii;

			if (null == EveWeltTopologii)
			{
				return null;
			}

			var ListeScnapscusLezteAuswertungErgeebnisNaacSimu = this.ListeScnapscusLezteAuswertungErgeebnisNaacSimu;

			if (null == ListeScnapscusLezteAuswertungErgeebnisNaacSimu)
			{
				return null;
			}

			var CurrentLocationInfo =
				ListeScnapscusLezteAuswertungErgeebnisNaacSimu.CurrentLocationInfo();

			if (null == CurrentLocationInfo)
			{
				return null;
			}

			var SolarSystemName = CurrentLocationInfo.SolarSystemName;

			if (null == SolarSystemName)
			{
				return null;
			}

			return
				EveWeltTopologii
				.MengeSolarSystem
				.FirstOrDefaultNullable((Kandidaat) => string.Equals(Kandidaat.SystemName, SolarSystemName, StringComparison.InvariantCultureIgnoreCase));
		}

		public Int64? ShipWarpingLezteZaitMili()
		{
			var FittingUndShipZuusctand = this.FittingUndShipZuusctand;

			if (null == FittingUndShipZuusctand)
			{
				return null;
			}

			return FittingUndShipZuusctand.WarpingLezteZaitMili;
		}

		public Int64? ShipDockedLezteZaitMili()
		{
			var FittingUndShipZuusctand = this.FittingUndShipZuusctand;

			if (null == FittingUndShipZuusctand)
			{
				return null;
			}

			return FittingUndShipZuusctand.DockedLezteZaitMili;
		}

		public Int64? ShipWarpingLezteAlterMili()
		{
			return	NuzerZaitMili - ShipWarpingLezteZaitMili();
		}

		public Int64? ShipDockedLezteAlterMili()
		{
			return NuzerZaitMili - ShipDockedLezteZaitMili();
		}

	}

	/// <summary>
	/// Um ISictAutomatZuusctand in sctruct zu bringe in welce RefNezDif kaine serialisatioon des ISictAutomatZuusctand versuuct.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[JsonObject(MemberSerialization.OptIn)]
	public struct AutomaatZuusctandUndGeneric<T>
	{
		readonly public ISictAutomatZuusctand Automaat;

		[JsonProperty]
		readonly public T Andere;

		public AutomaatZuusctandUndGeneric(
			ISictAutomatZuusctand Automaat,
			T Andere = default(T))
		{
			this.Automaat = Automaat;
			this.Andere = Andere;
		}
	}

}
