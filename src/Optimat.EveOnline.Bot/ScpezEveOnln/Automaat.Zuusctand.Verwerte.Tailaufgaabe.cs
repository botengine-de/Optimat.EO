using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Bib3;
using Newtonsoft.Json;
using Optimat.EveOnline;
using Optimat.EveOnline.Base;
using Optimat.EveOnline.Anwendung;
//using Optimat.EveOnline.AuswertGbs;
using Optimat.EveOnline.VonSensor;
using VonSensor = Optimat.EveOnline.VonSensor;
using Optimat.EveO.Nuzer.TempBot.Sonst;
using ExtractFromOldAssembly.Bib3;

namespace Optimat.ScpezEveOnln
{
	public partial class SictAutomatZuusctand
	{
		readonly public Queue<SictAufgaabeParam> AufgaabeBerecneAktueleTailaufgaabeCall = new Queue<SictAufgaabeParam>();

		public void AufgaabeBerecneAktueleTailaufgaabe(
			AufgaabeParamAndere AufgaabeParam,
			out SictAufgaabeParamZerleegungErgeebnis AufgaabeParamZerleegungErgeebnis,
			out bool? Erfolg,
			out bool? ErfolgAnhaltend,
			out Int64? ReegelungDistanceScpiilraumRest,
			SictAufgaabeKombiZuusctand KombiZuusctand)
		{
			AufgaabeBerecneAktueleTailaufgaabeCall.Enqueue(AufgaabeParam);

			AufgaabeParamZerleegungErgeebnis = new SictAufgaabeParamZerleegungErgeebnis(null, true);
			Erfolg = null;
			ErfolgAnhaltend = null;
			ReegelungDistanceScpiilraumRest = null;

			var AktioonInRaumObjektActivateDistanceScrankeMax = 2000;
			var AktioonInRaumObjektCargoDurcsuuceDistanceScrankeMax = 2000;

			if (null == AufgaabeParam)
			{
				return;
			}

			var NuzerZaitMili = this.NuzerZaitMili;

			/*
			 * 2015.03.12
			 * 
			 * Ersaz durc ToCustomBotSnapshot
				var GbsBaum = this.VonNuzerMeldungZuusctandTailGbsBaum;
			 * */

			var Gbs = this.Gbs;
			var OverviewUndTarget = this.OverviewUndTarget;
			var FittingUndShipZuusctand = this.FittingUndShipZuusctand;
			var AgentUndMission = this.AgentUndMission;
			var InRaumAktioonUndGefect = this.InRaumAktioonUndGefect;

			var OptimatParam = this.OptimatParam();

			var RaumVerlaseFraigaabe = (null == InRaumAktioonUndGefect) ? null : InRaumAktioonUndGefect.RaumVerlaseFraigaabe;
			var AnforderungDroneReturnLezte = (null == InRaumAktioonUndGefect) ? null : InRaumAktioonUndGefect.AnforderungDroneReturnLezte;
			var GefectModusAngraifendeObjekteAufDistanzHalte = (null == InRaumAktioonUndGefect) ? null : InRaumAktioonUndGefect.GefectModusAngraifendeObjekteAufDistanzHalte;
			var GefectAngraifendeHalteAufDistanceLezteErgeebnisZuZait =
				(null == InRaumAktioonUndGefect) ? default(SictWertMitZait<SictGefectAngraifendeHalteAufDistanceScritErgeebnis>) :
				InRaumAktioonUndGefect.GefectAngraifendeHalteAufDistanceLezteErgeebnisZuZait;

			var MengeAufgaabeZuusctand = this.MengeAufgaabeZuusctand;

			var AktioonUndockFraigaabeNictUrsace = (null == InRaumAktioonUndGefect) ? null : InRaumAktioonUndGefect.AktioonUndockFraigaabeNictUrsace;

			var OverviewPresetAktiivIdentUndMengeObjektGrupeSictbar =
				(null == OverviewUndTarget) ? null :
				OverviewUndTarget.OverviewPresetAktiivIdentUndMengeObjektGrupeSictbar;

			var MengeZuOverviewTabMengeObjektGrupeSictbar =
				(null == OverviewUndTarget) ? null : OverviewUndTarget.MengeZuOverviewTabMengeObjektGrupeSictbar;

			var AusOverviewObjektInputFookusExklusiiv =
				(null == OverviewUndTarget) ? null : OverviewUndTarget.AusOverviewObjektInputFookusExklusiiv;

			var MengeWindowZuErhalte = this.MengeWindowZuErhalte;

			var AusScnapscusAuswertungZuusctand = this.ListeScnapscusLezteAuswertungErgeebnisNaacSimu;

			var FürWirkungDestruktAufgaabeDroneEngageTarget = (null == InRaumAktioonUndGefect) ? null : InRaumAktioonUndGefect.FürWirkungDestruktAufgaabeDroneEngageTarget;

			var AusScnapcusShipUi =
				(null == AusScnapscusAuswertungZuusctand) ? null : AusScnapscusAuswertungZuusctand.ShipUi;

			var AusScnapcusMengeWindowInventory =
				(null == AusScnapscusAuswertungZuusctand) ? null : AusScnapscusAuswertungZuusctand.MengeWindowInventory;

			var MengeZuInfoPanelTypeButtonUndInfoPanel =
				(null == AusScnapscusAuswertungZuusctand) ? null :
				AusScnapscusAuswertungZuusctand.MengeZuInfoPanelTypeButtonUndInfoPanel();

			var ScnapscusGbsMengeAbovemainPanelEveMenu =
				(null == AusScnapscusAuswertungZuusctand) ? null : AusScnapscusAuswertungZuusctand.MengeAbovemainPanelEveMenu;

			var GbsButtonListSurroundings = (null == AusScnapscusAuswertungZuusctand) ? null :
				AusScnapscusAuswertungZuusctand.InfoPanelLocationInfoButtonListSurroundings();

			var ScnapscusWindowOverview = (null == AusScnapscusAuswertungZuusctand) ? null : AusScnapscusAuswertungZuusctand.WindowOverview;
			var ScnapscusWindowDroneView = (null == AusScnapscusAuswertungZuusctand) ? null : AusScnapscusAuswertungZuusctand.WindowDroneView;
			var ScnapscusMengeWindowAgentDialogue = (null == AusScnapscusAuswertungZuusctand) ? null : AusScnapscusAuswertungZuusctand.MengeWindowAgentDialogue;

			var ScnapscusWindowOverviewScroll =
				(null == ScnapscusWindowOverview) ? null : ScnapscusWindowOverview.Scroll;

			var ScnapscusWindowOverviewScrollHandleAntailAnGesamtMili =
				(null == ScnapscusWindowOverviewScroll) ? null : ScnapscusWindowOverviewScroll.ScrollHandleAntailAnGesamtMili;

			var WindowOverviewMengeTab = (null == ScnapscusWindowOverview) ? null : ScnapscusWindowOverview.ListeTabNuzbar;

			var WindowOverviewScroll = (null == ScnapscusWindowOverview) ? null : ScnapscusWindowOverview.Scroll;

			var WindowOverviewTabSelected = (null == ScnapscusWindowOverview) ? null : ScnapscusWindowOverview.TabSelected;

			var UtilmenuMission = (null == AusScnapscusAuswertungZuusctand) ? null : AusScnapscusAuswertungZuusctand.UtilmenuMission;

			var MengeTargetNocSictbar = (null == OverviewUndTarget) ? null : OverviewUndTarget.MengeTargetNocSictbar;

			var GefectListeLockedTargetScranke = 4;

			var ScnapscusWindowLobby = (null == AusScnapscusAuswertungZuusctand) ? null : AusScnapscusAuswertungZuusctand.WindowStationLobby;
			var ScnapscusWindowFittingWindow = (null == AusScnapscusAuswertungZuusctand) ? null : AusScnapscusAuswertungZuusctand.WindowFittingWindow;
			var ScnapscusWindowFittingMgmt = (null == AusScnapscusAuswertungZuusctand) ? null : AusScnapscusAuswertungZuusctand.WindowFittingMgmt;

			var WindowDroneViewGrupeDronesInBay = (null == ScnapscusWindowDroneView) ? null : ScnapscusWindowDroneView.GrupeDronesInBay;
			var WindowDroneViewGrupeDronesInLocalSpace = (null == ScnapscusWindowDroneView) ? null : ScnapscusWindowDroneView.GrupeDronesInLocalSpace;

			var WindowLobbyKnopfFitting = (null == ScnapscusWindowLobby) ? null : ScnapscusWindowLobby.KnopfFitting;
			var WindowLobbyMengeAgentEntry = (null == ScnapscusWindowLobby) ? null : ScnapscusWindowLobby.MengeAgentEntry;

			var AusScnapscusGbsInfoPanelRoute = (null == AusScnapscusAuswertungZuusctand) ? null : AusScnapscusAuswertungZuusctand.InfoPanelRoute;
			var AusScnapscusGbsInfoPanelRouteExpanded = (null == AusScnapscusGbsInfoPanelRoute) ? null : AusScnapscusGbsInfoPanelRoute.MainContSictbar;

			var AusScnapscusGbsInfoPanelRouteCurrentInfo = (null == AusScnapscusGbsInfoPanelRoute) ? null : AusScnapscusGbsInfoPanelRoute.CurrentInfo;

			var ShipZuusctandLezte = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.ShipZuusctand;

			var SelbsctShipDocking = (null == ShipZuusctandLezte) ? null : ShipZuusctandLezte.Docking;
			var SelbsctShipDocked = (null == ShipZuusctandLezte) ? null : ShipZuusctandLezte.Docked;
			var SelbsctShipWarping = (null == ShipZuusctandLezte) ? null : ShipZuusctandLezte.Warping;
			var SelbsctShipCloaked = (null == ShipZuusctandLezte) ? null : ShipZuusctandLezte.Cloaked;
			var SelbsctShipJammed = (null == ShipZuusctandLezte) ? null : ShipZuusctandLezte.Jammed;
			var WarpingLezteZaitMili = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.WarpingLezteZaitMili;
			var JumpingLezteZaitMili = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.JumpingLezteZaitMili;
			var DockedLezteZaitMili = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.DockedLezteZaitMili;
			var WarpingLezteAlterMili = NuzerZaitMili - WarpingLezteZaitMili;
			var JumpingLezteAlterMili = NuzerZaitMili - JumpingLezteZaitMili;
			var DockedLezteAlterMili = NuzerZaitMili - DockedLezteZaitMili;
			var SelbsctShipWarpScrambled = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.SelbsctShipWarpScrambled;
			var AnnaameDroneControlCountScrankeMaxNulbar = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.AnnaameDroneControlCountScrankeMax;

			var MengeTargetAnzaalScrankeMax = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.MengeTargetAnzaalScrankeMax;

			var ScritNääxteJammed = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.ScritNääxteJammed;
			var ScritÜüberNääxteJammed = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.ScritÜüberNääxteJammed;

			var ListeAbovemainMessageDronesLezteAlter = (null == Gbs) ? null : Gbs.ListeAbovemainMessageDronesLezteAlter;

			var GbsMenuKaskaadeLezteNocOfe = (null == Gbs) ? null : Gbs.MenuKaskaadeLezteNocOfe;

			var GbsMenuKaskaadeLezteNocOfeBeginZait = (null == GbsMenuKaskaadeLezteNocOfe) ? null : GbsMenuKaskaadeLezteNocOfe.BeginZait;
			var GbsMenuKaskaadeLezteNocOfeAlter = NuzerZaitMili - GbsMenuKaskaadeLezteNocOfeBeginZait;

			var GbsListeMenu = (null == Gbs) ? null : Gbs.ListeMenuNocOfeBerecne();
			var GbsMengeWindow = (null == Gbs) ? null : Gbs.MengeWindow;

			var GbsMengeMenuMitBeginZait = GbsListeMenuNocOfeMitBeginZaitBerecne();

			var GbsListeWindow =
				null == GbsMengeWindow ? null :
					/*
					2015.09.01
					Änderung InGbsBaumAstIndex: Übernaame von noie Sensor InTreeIndex: Element occludes other Elements with lower Value.
				GbsMengeWindow.OrderByDescending((KandidaatWindow) => KandidaatWindow.ZIndex)
				*/
					GbsMengeWindow.OrderBy(KandidaatWindow => KandidaatWindow.ZIndex)
					.ToArray();

			var GbsListeWindowAgentDialogue =
				ExtractFromOldAssembly.Bib3.Extension.WhereNullable(
				GbsListeWindow,
				(KandidaatWindow) => KandidaatWindow.AingangScnapscusTailObjektIdentLezteBerecne() is VonSensor.WindowAgentDialogue)
				.ToArrayNullable();

			var GbsListeWindowAgentDialogueFrüüheste =
				ExtractFromOldAssembly.Bib3.Extension.FirstOrDefaultNullable(GbsListeWindowAgentDialogue);

			var GbsListeMenuFrüheste = ExtractFromOldAssembly.Bib3.Extension.FirstOrDefaultNullable(GbsListeMenu);

			var GbsListeMenuFrühesteScnapscus =
				(null == GbsListeMenuFrüheste) ? null :
				GbsListeMenuFrüheste.AingangScnapscusTailObjektIdentLezteBerecne();

			var GbsListeMenuFrühesteScnapscusInGbsFläce =
				(null == GbsListeMenuFrühesteScnapscus) ? OrtogoonInt.Leer :
				GbsListeMenuFrühesteScnapscus.InGbsFläce;

			var GbsListeMenuFrühesteScnapscusInGbsFläceVergröösertFürÜberscnaidungMitGbsWurzelObjekt =
				(null == GbsListeMenuFrühesteScnapscusInGbsFläce) ? OrtogoonInt.Leer :
				GbsListeMenuFrühesteScnapscusInGbsFläce.Vergröösert(30, 10);

			var AnnaameTargetingDistanceScrankeMax = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.AnnaameTargetingDistanceScrankeMax;

			var AnnaameModuleDestruktRangeMax = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.AnnaameModuleDestruktRangeMax;
			var AnnaameModuleDestruktRangeOptimumNulbar = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.AnnaameModuleDestruktRangeOptimum;

			var AnnaameGefectDistanzOptimum = Bib3.Glob.Min(AnnaameModuleDestruktRangeOptimumNulbar, AnnaameTargetingDistanceScrankeMax);

			var MengeModuleRepr = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.MengeModuleRepr;

			var MengeModuleUmscaltFraigaabe =
				this.MengeModuleUmscaltFraigaabe
				.WhereNullable((KandidaatModule) => (null == KombiZuusctand) ? true : KombiZuusctand.ModuleVerwendungFraigaabe(KandidaatModule))
				.ToArrayNullable();

			var ModuleAfterburner = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.AnnaameModuleAfterburner;

			var AusInventoryItemZuÜbertraageNaacActiveShip =
				(null == InRaumAktioonUndGefect) ? null : InRaumAktioonUndGefect.AusInventoryItemZuÜbertraageNaacActiveShip;

			var MausPfaad = AufgaabeParam.MausPfaad;

			var AktioonInOverviewTabZuAktiviire = AufgaabeParam.AktioonInOverviewTabZuAktiviire;

			//var MengeOverviewObjektGrupeMesungZuErsctele = AufgaabeParam.MengeOverviewObjektGrupeMesungZuErsctele;

			var AktioonInOverviewMaceSictbar = AufgaabeParam.AktioonInOverviewMaceSictbar;
			var InOverviewTabFolgeViewportDurcGrid = AufgaabeParam.InOverviewTabFolgeViewportDurcGrid;

			var OverViewObjektZuBearbaite = AufgaabeParam.OverViewObjektZuBearbaite;
			var TargetZuBearbeite = AufgaabeParam.TargetZuBearbaite;

			var OverViewObjektZuBearbaiteNaame = (null == OverViewObjektZuBearbaite) ? null : OverViewObjektZuBearbaite.Name;

			var OverViewObjektZuBearbaiteSictungLezteAlterMili = NuzerZaitMili - ((null == OverViewObjektZuBearbaite) ? null : OverViewObjektZuBearbaite.SictungLezteZait);

			var ColumnHeaderSort = AufgaabeParam.ColumnHeaderSort;
			var ListEntryMaceSictbar = AufgaabeParam.ListEntryMaceSictbar;
			var ListEntryExpand = AufgaabeParam.ListEntryExpand;
			var ListEntryCollapse = AufgaabeParam.ListEntryCollapse;
			var ListEntryToggleExpandCollapse = AufgaabeParam.ListEntryToggleExpandCollapse;

			var ModuleScalteAin = AufgaabeParam.ModuleScalteAin;
			var ModuleScalteAus = AufgaabeParam.ModuleScalteAus;
			var ModuleScalteUm = AufgaabeParam.ModuleScalteUm;
			var ModuleMesungModuleButtonHint = AufgaabeParam.ModuleMesungModuleButtonHint;
			var AfterburnerScalteAin = AufgaabeParam.AfterburnerScalteAin;

			var TargetUnLock = AufgaabeParam.TargetUnLock;
			var TargetInputFookusSeze = AufgaabeParam.TargetInputFookusSeze;
			var LobbyAgentEntryStartConversation = AufgaabeParam.LobbyAgentEntryStartConversation;
			var AktioonInRaumObjektActivate = AufgaabeParam.AktioonInRaumObjektActivate;

			//	var GbsAstVerberge = AufgaabeParam.GbsAstVerberge;

			var MenuListeAstBedingung = AufgaabeParam.MenuListeAstBedingung;
			var AktioonMenuBegin = AufgaabeParam.AktioonMenuBegin;
			var MenuWurzelGbsObjekt = AufgaabeParam.MenuWurzelGbsObjekt;
			var MenuWurzelGbsObjektBezaicner = (null == MenuWurzelGbsObjekt) ? -1 : MenuWurzelGbsObjekt.Ident;
			var MenuWurzelGbsObjektInGbsFläce = (null == MenuWurzelGbsObjekt) ? OrtogoonInt.Leer : MenuWurzelGbsObjekt.InGbsFläce;
			var MenuEntry = AufgaabeParam.MenuEntry;
			var AktioonMenuEntferne = AufgaabeParam.AktioonMenuEntferne;

			var AktioonNeocomMenuEntferne = AufgaabeParam.AktioonNeocomMenuEntferne;

			var InfoPanelEnable = AufgaabeParam.InfoPanelEnable;
			var InfoPanelExpand = AufgaabeParam.InfoPanelExpand;

			var GbsAstOklusioonVermaidung = AufgaabeParam.GbsAstOklusioonVermaidung;

			var WindowMinimize = AufgaabeParam.WindowMinimize;
			var WindowHooleNaacVorne = AufgaabeParam.WindowHooleNaacVorne;

			var GridVerlase = AufgaabeParam.GridVerlase;

			var ManööverAuszufüüreTyp = AufgaabeParam.ManööverAuszufüüreTyp;
			var DistanzAinzuscteleScrankeMin = AufgaabeParam.DistanzAinzuscteleScrankeMin;
			var DistanzAinzuscteleScrankeMax = AufgaabeParam.DistanzAinzuscteleScrankeMax;

			var MissionObjectiveMese = AufgaabeParam.MissionObjectiveMese;
			var MissionStartConversation = AufgaabeParam.MissionStartConversation;
			var MissionButtonUtilmenuObjectiveZuMese = AufgaabeParam.MissionButtonUtilmenuObjectiveZuMese;
			var WindowAgentDialogueMissionAcceptOderRequest = AufgaabeParam.WindowAgentDialogueMissionAcceptOderRequest;
			var WindowAgentDialogueMissionDecline = AufgaabeParam.WindowAgentDialogueMissionDecline;
			var MissionAccept = AufgaabeParam.MissionAccept;
			var MissionDecline = AufgaabeParam.MissionDecline;

			var FittingZuApliziire = AufgaabeParam.FittingZuApliziire;

			var AktioonUnDock = AufgaabeParam.AktioonUnDock;

			var InventoryItemTransport = AufgaabeParam.InventoryItemTransport;
			var InventorySezeSictTypAufList = AufgaabeParam.InventorySezeSictTypAufList;

			var AufgaabeParamAufgaabeParam = AufgaabeParam.AufgaabeParam;

			if (null != AufgaabeParamAufgaabeParam)
			{
				AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAufgaabeParam);
			}

			if (null != MausPfaad)
			{
				var ListeWeegpunktGbsObjekt = MausPfaad.ListeWeegpunktGbsObjekt;

				if (null != ListeWeegpunktGbsObjekt)
				{
					//	!!!!	zu ergänze:	Berecnung Clipping durc enthaltende GBS Ast, Berecnung Oklusioon durc andere GBS Ast
					//	!!!!	zu ergänze:	Abhilfe geege Clipping und Oklusioon (Scroll, Collapse TreeItem, minimize Window, Window hoole naac Vordergrund)

					//	!!!!	zu ergänze: bai berecnung Oklusioon berüksictigung von Aufgaabe welce vor diise Aufgaabe aingeplaant sind und dii Menge der Kandidaate für Oklusioon ändere könte (z.B. Menu)

					var MausPfaadMengeFläceZuMaide = new List<SictFläceRectekOrtoAbhängigVonGbsAst>();

					var WeegpunktOklusioonErfordertReaktioon = false;

					foreach (var WeegpunktGbsObjekt in ListeWeegpunktGbsObjekt)
					{
						if (null == WeegpunktGbsObjekt)
						{
							continue;
						}

						IEnumerable<SictFläceRectekOrtoAbhängigVonGbsAst> WeegpunktMengeFläceZuMaide;

						var WeegpunktOklusioonVermaidungListeTailaufgaabe =
							GbsAstOklusioonVermaidungBerecne(new SictAufgaabeParamGbsAstOklusioonVermaidung(WeegpunktGbsObjekt, 8), out WeegpunktMengeFläceZuMaide);

						if (!WeegpunktOklusioonVermaidungListeTailaufgaabe.IsNullOrEmpty())
						{
							AufgaabeParamZerleegungErgeebnis = AufgaabeParamZerleegungErgeebnis.Kombiniire(
								WeegpunktOklusioonVermaidungListeTailaufgaabe
								.Select((TailaufgaabeParam) => SictAufgaabeParam.KonstruktAufgaabeParam(TailaufgaabeParam, new string[] { "remove Occlusion" })),
								false);
							WeegpunktOklusioonErfordertReaktioon = true;
							break;
						}

						{
							//	!!!!	Noc zu korigiire:	Menge der Fläce zu Maide zuukünftig per Weegpunkt festseze

							if (null != WeegpunktMengeFläceZuMaide)
							{
								foreach (var WeegpunktFläceZuMaide in WeegpunktMengeFläceZuMaide)
								{
									if (null == WeegpunktFläceZuMaide)
									{
										continue;
									}

									MausPfaadMengeFläceZuMaide.Add(WeegpunktFläceZuMaide);
								}
							}
						}
					}

					if (!WeegpunktOklusioonErfordertReaktioon)
					{
						var ListeMausZiilFläce =
							ListeWeegpunktGbsObjekt
							.Select((WeegpunktGbsObjekt) => SictAutomatZuusctand.FläceRectekOrtoAbhängigVonGbsAstBerecne(WeegpunktGbsObjekt, null))
							.ToArray();

						/*
						 * 2015.01.04
						 * 
						AufgaabeParamZerleegungErgeebnis = AufgaabeParamZerleegungErgeebnis.Kombiniire(
							AufgaabeParamAndere.KonstruktNaacNuzerVorsclaagWirkung(
							SictVorsclaagNaacProcessWirkung.VorsclaagWirkungMausPfaad(
							ListeMausZiilFläce,
							MausPfaad.MausTasteLinxAin,
							MausPfaad.MausTasteReczAin,
							MausPfaadMengeFläceZuMaide.ToArray())));
						 * */

						var ListeMausZiilFläceVersezt =
							ListeMausZiilFläce
							.SelectNullable((MausZiilFläce) => (null == MausZiilFläce) ? null : MausZiilFläce.Versezt(NaacNuzerMausPfaadVersaz))
							.ToArrayNullable();

						AufgaabeParamZerleegungErgeebnis = AufgaabeParamZerleegungErgeebnis.Kombiniire(
							AufgaabeParamAndere.KonstruktNaacNuzerVorsclaagWirkung(
							SictVorsclaagNaacProcessWirkung.VorsclaagWirkungMausPfaad(
							ListeMausZiilFläceVersezt,
							MausPfaad.MausTasteLinxAin,
							MausPfaad.MausTasteReczAin,
							MausPfaadMengeFläceZuMaide.ToArray())));
					}
				}
			}

			if (null != GbsAstOklusioonVermaidung)
			{
				var WeegpunktOklusioonVermaidungListeTailaufgaabe =
					GbsAstOklusioonVermaidungBerecne(GbsAstOklusioonVermaidung);

				if (!WeegpunktOklusioonVermaidungListeTailaufgaabe.IsNullOrEmpty())
					AufgaabeParamZerleegungErgeebnis = AufgaabeParamZerleegungErgeebnis.Kombiniire(WeegpunktOklusioonVermaidungListeTailaufgaabe);
			}

			/*
			Ersaz durc AufgaabeParamGbsElementVerberge

			if (null != GbsAstVerberge)
			{
				var GbsAstVerbergeBezaicnerNulbar = (Int64?)GbsAstVerberge.Ident;

				if (GbsAstVerbergeBezaicnerNulbar.HasValue)
				{
					var KandidaatOklusioonInfo = ZuGbsAstHerkunftAdreseKandidaatOklusioonBerecne(GbsAstVerbergeBezaicnerNulbar.Value);

					if (null == KandidaatOklusioonInfo)
					{
						AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAus();
					}
					else
					{
						var KandidaatOklusioonInfoMenu = KandidaatOklusioonInfo.Menu;
						var KandidaatOklusioonInfoUtilmenu = KandidaatOklusioonInfo.Utilmenu;
						var KandidaatOklusioonInfoPanelGroup = KandidaatOklusioonInfo.PanelGroup;
						var KandidaatOklusioonInfoWindow = KandidaatOklusioonInfo.Window;

						var ListeAufgaabeVerberge = new List<SictAufgaabeParam>();

						if (null != KandidaatOklusioonInfoMenu ||
							null != KandidaatOklusioonInfoUtilmenu)
						{
							ListeAufgaabeVerberge.Add(AufgaabeParamAndere.KonstruktMenuEntferne());
						}

						if (null != KandidaatOklusioonInfoPanelGroup)
						{
							//	PanelGroup sctamt vermuutlic aus Neocom
							ListeAufgaabeVerberge.Add(AufgaabeParamAndere.KonstruktNeocomMenuEntferne());
						}

						if (null != KandidaatOklusioonInfoWindow)
						{
							ListeAufgaabeVerberge.Add(AufgaabeParamAndere.KonstruktWindowMinimize(KandidaatOklusioonInfoWindow));
						}

						if (ListeAufgaabeVerberge.Count < 1)
						{
							AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAus();
						}

						AufgaabeParamZerleegungErgeebnis = AufgaabeParamZerleegungErgeebnis.Kombiniire(ListeAufgaabeVerberge);
					}
				}
			}
			*/

			if (true == AktioonMenuEntferne)
			{
				//	Ainfac Linx oobe klike und hofe das Menu nit dort isc (den dan würde das entferne mööglicerwaise nit funktioniire).

				var MausZiilFläce = OrtogoonInt.AusPunktZentrumUndGrööse(new Vektor2DInt(88, 8), new Vektor2DInt(8, 8));

				AufgaabeParamZerleegungErgeebnis = AufgaabeParamZerleegungErgeebnis.Kombiniire(
					AufgaabeParamAndere.KonstruktNaacNuzerVorsclaagWirkung(
					SictVorsclaagNaacProcessWirkung.VorsclaagWirkungMausKlikLinx(MausZiilFläce)));
			}

			if (true == AktioonNeocomMenuEntferne)
			{
				AufgaabeParamZerleegungErgeebnis = AufgaabeParamZerleegungErgeebnis.Kombiniire(AufgaabeParamAndere.KonstruktMenuEntferne());
			}

			if (InfoPanelEnable.HasValue)
			{
				var ButtonUndInfoPanel =
					MengeZuInfoPanelTypeButtonUndInfoPanel
					.FirstOrDefaultNullable((ZuInfoPanelTypeButtonUndInfoPanel) => ZuInfoPanelTypeButtonUndInfoPanel.Key == InfoPanelEnable).Value;

				var InfoPanelEnableButton = ButtonUndInfoPanel.Key;
				var InfoPanel = ButtonUndInfoPanel.Value;

				if (null == InfoPanel)
				{
					if (null == InfoPanelEnableButton)
					{
						if (InfoPanelTypSictEnum.AgentMission == InfoPanelEnable)
						{
							//	2015.09.02	getrente Zwaig für MSN da Button auc verborge sain kan wen kaine MSN vorhande.

						}
						else
						{

							AufgaabeParamZerleegungErgeebnis = AufgaabeParamZerleegungErgeebnis.Kombiniire((SictAufgaabeParam)null, false);

							//	!!!!	Meldung Feeler
						}
					}
					else
					{
						AufgaabeParamZerleegungErgeebnis = AufgaabeParamZerleegungErgeebnis.Kombiniire(
							AufgaabeParamAndere.KonstruktMausPfaad(SictAufgaabeParamMausPfaad.KonstruktMausKlikLinx(InfoPanelEnableButton)));
					}
				}
			}

			if (InfoPanelExpand.HasValue)
			{
				var ButtonUndInfoPanel =
					MengeZuInfoPanelTypeButtonUndInfoPanel
					.FirstOrDefaultNullable(ZuInfoPanelTypeButtonUndInfoPanel => ZuInfoPanelTypeButtonUndInfoPanel.Key == InfoPanelExpand).Value;

				var InfoPanelEnableButton = ButtonUndInfoPanel.Key;
				var InfoPanel = ButtonUndInfoPanel.Value;

				if (null == InfoPanel)
				{
					AufgaabeParamZerleegungErgeebnis = AufgaabeParamZerleegungErgeebnis.Kombiniire(
						AufgaabeParamAndere.KonstruktInfoPanelEnable(InfoPanelExpand.Value),
						false);
				}
				else
				{
					if (!(true == InfoPanel.MainContSictbar))
					{
						var HeaderButtonExpand = InfoPanel.HeaderButtonExpand;

						if (null == HeaderButtonExpand)
						{
							AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAus();
						}
						else
						{
							AufgaabeParamZerleegungErgeebnis = AufgaabeParamZerleegungErgeebnis.Kombiniire(
								AufgaabeParamAndere.KonstruktMausPfaad(SictAufgaabeParamMausPfaad.KonstruktMausKlikLinx(HeaderButtonExpand)));
						}
					}
				}
			}

			if (null != WindowMinimize)
			{
				var WindowZuErhalte =
					(null == MengeWindowZuErhalte) ? false :
					MengeWindowZuErhalte.Contains(WindowMinimize);

				if (WindowZuErhalte)
				{
					AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAus();
				}
				else
				{
					SictGbsWindowZuusctand WindowStack = null;

					if (null != GbsMengeWindow)
					{
						foreach (var GbsWindow in GbsMengeWindow)
						{
							if (null == GbsWindow)
							{
								continue;
							}

							var GbsWindowAingangScnapscusLezte = GbsWindow.AingangScnapscusLezteBerecne();

							if (!GbsWindowAingangScnapscusLezte.HasValue)
							{
								continue;
							}

							var GbsWindowAlsWindowStack = GbsWindowAingangScnapscusLezte.Value.Key as VonSensor.WindowStack;

							if (null == GbsWindowAlsWindowStack)
							{
								continue;
							}

							var GbsWindowAlsWindowStackWindowAktiiv = GbsWindowAlsWindowStack.WindowAktiiv;

							if (null == GbsWindowAlsWindowStackWindowAktiiv)
							{
								continue;
							}

							if (WindowMinimize.GbsAstHerkunftAdrese == GbsWindowAlsWindowStackWindowAktiiv.Ident)
							{
								//	WindowMinimize isc in WindowStack enthalte.

								WindowStack = GbsWindow;
							}
						}
					}

					if (null == WindowStack)
					{
						var HeaderButtonClose = WindowMinimize.HeaderButtonCloseBerecne();
						var HeaderButtonMinimize = WindowMinimize.HeaderButtonMinimizeBerecne();

						var VersuucPerTastatur = false;

						if (true == WindowMinimize.isModal)
						{
							var AufgaabeVersuucMinimizeZaitScranke = NuzerZaitMili - 33333;

							var MengeAufgaabeVersuucMinimize =
								MengeAufgaabeZuusctandTailmengeBerecneMitAbsclusTailWirkungZaitScranke(
								(KandidaatAufgaabeParam) => null != ((null == KandidaatAufgaabeParam) ? null : KandidaatAufgaabeParam.WindowMinimizeVirt()),
								AufgaabeVersuucMinimizeZaitScranke);

							var MengeAufgaabeVersuucMinimizeTailmengeMitVorsclaagWirkungTastatur =
								ExtractFromOldAssembly.Bib3.Extension.WhereNullable(
								MengeAufgaabeVersuucMinimize,
								(Kandidaat) => Kandidaat.EnthaltVorsclaagWirkungKey());

							if (ExtractFromOldAssembly.Bib3.Extension.CountNullable(MengeAufgaabeVersuucMinimizeTailmengeMitVorsclaagWirkungTastatur) < 4)
							{
								VersuucPerTastatur = true;
							}
						}

						if (VersuucPerTastatur)
						{
							AufgaabeParamZerleegungErgeebnis = AufgaabeParamZerleegungErgeebnis.Kombiniire(
								AufgaabeParamAndere.KonstruktNaacNuzerVorsclaagWirkung(
								SictVorsclaagNaacProcessWirkung.VorsclaagWirkungKey(System.Windows.Input.Key.Escape)));
						}
						else
						{
							if (null == HeaderButtonMinimize)
							{
								if (null == HeaderButtonClose)
								{
									AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAus();

									//	Um zu vermaide das durc reczklik irgendwo in Window aine Änderung ausgelööst werd welce naactaile bringt
									//	sol für deen klik auf das Window aine Tailfäce ausgesuuct werde welce mööglicst kaine Sctoierelemente enthalt.
									//	Diise werd zwiscegescpaicert in WindowHooleNaacVorneZiilFläce
									GbsElement WindowHooleNaacVorneZiilFläce = null;

									var WindowMinimizeHeaderFläceOoneSctoierelement = WindowMinimize.HeaderFläceOoneSctoierelement;

									if (null != WindowMinimizeHeaderFläceOoneSctoierelement)
									{
										var FürWindowMinimizeHeaderFläceOoneSctoierelementOklusioonZuVermaideMengeAufgaabe =
											GbsAstOklusioonVermaidungBerecne(new SictAufgaabeParamGbsAstOklusioonVermaidung(WindowMinimizeHeaderFläceOoneSctoierelement, 3));

										if (FürWindowMinimizeHeaderFläceOoneSctoierelementOklusioonZuVermaideMengeAufgaabe.IsNullOrEmpty())
										{
											//	Um Oklusioon von WindowMinimizeHeaderFläceOoneSctoierelement zu vermaide wääre kaine Aufgaabe nootwendig. D.h. das Element isc nit (volsctändig) okludiirt.
											WindowHooleNaacVorneZiilFläce = WindowMinimizeHeaderFläceOoneSctoierelement;
										}
									}

									if (null == WindowHooleNaacVorneZiilFläce)
									{
										WindowHooleNaacVorneZiilFläce = WindowMinimize.ScnapscusWindowLezte;
									}

									AufgaabeParamZerleegungErgeebnis = AufgaabeParamZerleegungErgeebnis.Kombiniire(
										AufgaabeParamAndere.AufgaabeAktioonMenu(WindowHooleNaacVorneZiilFläce,
										new SictAnforderungMenuKaskaadeAstBedingung("minimize", true)));
								}
								else
								{
									/*
									 * 2015.02.25
									 * 
									AufgaabeParamZerleegungErgeebnis = AufgaabeParamZerleegungErgeebnis.Kombiniire(
										AufgaabeParamAndere.KonstruktMausPfaad(SictAufgaabeParamMausPfaad.KonstruktMausKlikLinx(
										HeaderButtonClose.SictGrööseGesezt(new Vektor2DInt(4, 4)))));
									 * */
									AufgaabeParamZerleegungErgeebnis = AufgaabeParamZerleegungErgeebnis.Kombiniire(
																AufgaabeParamAndere.KonstruktMausPfaad(SictAufgaabeParamMausPfaad.KonstruktMausKlikLinx(
																HeaderButtonClose)));
								}
							}
							else
							{
								/*
								 * 2015.02.25
								 * 
							AufgaabeParamZerleegungErgeebnis = AufgaabeParamZerleegungErgeebnis.Kombiniire(
								AufgaabeParamAndere.KonstruktMausPfaad(SictAufgaabeParamMausPfaad.KonstruktMausKlikLinx(
								HeaderButtonMinimize.SictGrööseGesezt(new Vektor2DInt(4, 4)))));
								 * */
								AufgaabeParamZerleegungErgeebnis = AufgaabeParamZerleegungErgeebnis.Kombiniire(
									AufgaabeParamAndere.KonstruktMausPfaad(SictAufgaabeParamMausPfaad.KonstruktMausKlikLinx(
									HeaderButtonMinimize)));
							}
						}
					}
					else
					{
						//	Window isc in WindowStack enthalte.

						AufgaabeParamZerleegungErgeebnis = AufgaabeParamZerleegungErgeebnis.Kombiniire(AufgaabeParamAndere.KonstruktWindowMinimize(WindowStack));
					}
				}
			}

			if (null != WindowHooleNaacVorne)
			{
				AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAus();

				var WindowHooleNaacVorneInGbsFläce = WindowHooleNaacVorne.InGbsFläce;
				var WindowHooleNaacVorneScnapscusWindowLezte = WindowHooleNaacVorne.ScnapscusWindowLezte;

				if (null == WindowHooleNaacVorneScnapscusWindowLezte)
				{
				}
				else
				{
					//	!!!!	noc zu ergänze: nict okludiirte Fläce berecne auf welce geklikt werde sol.

					AufgaabeParamZerleegungErgeebnis = AufgaabeParamZerleegungErgeebnis.Kombiniire(
						AufgaabeParamAndere.KonstruktMausPfaad(SictAufgaabeParamMausPfaad.KonstruktMausKlikRecz(WindowHooleNaacVorneScnapscusWindowLezte)));
				}
			}

			if (true == GridVerlase)
			{
				if (null == GbsButtonListSurroundings)
				{
					AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAus();
				}
				else
				{
					AufgaabeParamZerleegungErgeebnis = AufgaabeParamZerleegungErgeebnis.Kombiniire(
						AufgaabeParamAndere.AufgaabeAktioonMenu(
						GbsButtonListSurroundings, AusButtonListSurroundingsMenuPfaadWarpNaacStationOderGate));
				}
			}

			if (ManööverAuszufüüreTyp.HasValue)
			{
				var AufgaabePasendZuAngraifendeObjektAufDistanceHalte = true;

				if (true == GefectModusAngraifendeObjekteAufDistanzHalte)
				{
					AufgaabePasendZuAngraifendeObjektAufDistanceHalte = false;

					if (null != GefectAngraifendeHalteAufDistanceLezteErgeebnisZuZait.Wert)
					{
						var ListeAufgaabeAufDistanceZuHalteParam = GefectAngraifendeHalteAufDistanceLezteErgeebnisZuZait.Wert.ListeAufgaabeAufDistanceZuHalteParam;

						if (null != ListeAufgaabeAufDistanceZuHalteParam)
						{
							foreach (var AufgaabeAufDistanceZuHalteParam in ListeAufgaabeAufDistanceZuHalteParam)
							{
								if (OverViewObjektZuBearbaite == AufgaabeAufDistanceZuHalteParam.OverViewObjektZuBearbaiteVirt() ||
									TargetZuBearbeite == AufgaabeAufDistanceZuHalteParam.TargetZuBearbaiteVirt())
								{
									AufgaabePasendZuAngraifendeObjektAufDistanceHalte = true;
								}

								if (null != AufgaabeAufDistanceZuHalteParam.OverViewObjektZuBearbaiteVirt() &&
									null != TargetZuBearbeite)
								{
									var MengeKandidaatTarget = OverviewUndTarget.MengeTargetTailmengePasendZuOverviewObjektBerecne(
										AufgaabeAufDistanceZuHalteParam.OverViewObjektZuBearbaiteVirt());

									if (null != MengeKandidaatTarget)
									{
										if (MengeKandidaatTarget.Contains(TargetZuBearbeite))
										{
											AufgaabePasendZuAngraifendeObjektAufDistanceHalte = true;
										}
									}
								}
							}
						}
					}
				}

				if (AufgaabePasendZuAngraifendeObjektAufDistanceHalte)
				{
					var KandidaatManööverAufgaabe =
						ManööverAusgefüürtLezteAufgaabeBerecne(
						OverViewObjektZuBearbaite,
						TargetZuBearbeite,
						ManööverAuszufüüreTyp.Value,
						DistanzAinzuscteleScrankeMin,
						DistanzAinzuscteleScrankeMax);

					var ManööverBeraitsAusgefüürt = false;

					if (null != KandidaatManööverAufgaabe)
					{
						var KandidaatManööverAufgaabeAbsclusTailWirkungZait = KandidaatManööverAufgaabe.AbsclusTailWirkungZait;

						var KandidaatManööverAufgaabeAbsclusTailWirkungAlter = (NuzerZaitMili - KandidaatManööverAufgaabeAbsclusTailWirkungZait) / 1000;

						var KandidaatManööverErgeebnis = KandidaatManööverAufgaabe.ManööverErgeebnis;

						if (null != KandidaatManööverErgeebnis &&
							KandidaatManööverAufgaabeAbsclusTailWirkungAlter < 40)
						{
							if (!KandidaatManööverErgeebnis.EndeZait.HasValue)
							{
								ManööverBeraitsAusgefüürt = true;
							}
						}
					}

					if (!ManööverBeraitsAusgefüürt)
					{
						SictAnforderungMenuKaskaadeAstBedingung[] MenuPfaad = null;

						if (SictZuInRaumObjektManööverTypEnum.Approach == ManööverAuszufüüreTyp)
						{
							MenuPfaad =
								new Optimat.ScpezEveOnln.SictAnforderungMenuKaskaadeAstBedingung[]{
								new Optimat.ScpezEveOnln.SictAnforderungMenuKaskaadeAstBedingung("Approach",    true),
						};
						}

						if (SictZuInRaumObjektManööverTypEnum.Orbit == ManööverAuszufüüreTyp)
						{
							MenuPfaad =
								Optimat.ScpezEveOnln.SictAutomat.MenuPfaadOrbitFürDistanzMax((DistanzAinzuscteleScrankeMax ?? DistanzAinzuscteleScrankeMin) ?? 1000);
						}

						if (null != TargetZuBearbeite)
						{
							if (TargetZuBearbeite.SictungLezteDistanceScrankeMinScpezTarget < DistanzAinzuscteleScrankeMin ||
								DistanzAinzuscteleScrankeMax < TargetZuBearbeite.SictungLezteDistanceScrankeMaxScpezTarget)
							{
								AufgaabeParamZerleegungErgeebnis = AufgaabeParamZerleegungErgeebnis.Kombiniire(
									AufgaabeParamAndere.AufgaabeAktioonMenu(TargetZuBearbeite, MenuPfaad));
							}
						}
						else
						{
							if (null != OverViewObjektZuBearbaite)
							{
								if (OverViewObjektZuBearbaite.SictungLezteDistanceScrankeMinScpezOverview < DistanzAinzuscteleScrankeMin ||
									DistanzAinzuscteleScrankeMax < OverViewObjektZuBearbaite.SictungLezteDistanceScrankeMaxScpezOverview)
								{
									AufgaabeParamZerleegungErgeebnis = AufgaabeParamZerleegungErgeebnis.Kombiniire(
										AufgaabeParamAndere.AufgaabeAktioonMenu(OverViewObjektZuBearbaite, MenuPfaad));
								}
							}
						}
					}
				}
				else
				{
					AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAus();
				}
			}
			else
			{
				if (DistanzAinzuscteleScrankeMin.HasValue ||
					DistanzAinzuscteleScrankeMax.HasValue)
				{
					Int64? InRaumObjektDistanceScrankeMin = null;
					Int64? InRaumObjektDistanceScrankeMax = null;

					if (null != TargetZuBearbeite)
					{
						InRaumObjektDistanceScrankeMin = TargetZuBearbeite.SictungLezteDistanceScrankeMinScpezTarget;
						InRaumObjektDistanceScrankeMax = TargetZuBearbeite.SictungLezteDistanceScrankeMaxScpezTarget;
					}
					else
					{
						if (null != OverViewObjektZuBearbaite)
						{
							InRaumObjektDistanceScrankeMin = OverViewObjektZuBearbaite.SictungLezteDistanceScrankeMinScpezOverview;
							InRaumObjektDistanceScrankeMax = OverViewObjektZuBearbaite.SictungLezteDistanceScrankeMaxScpezOverview;
						}
					}

					ReegelungDistanceScpiilraumRest =
						Bib3.Glob.Min(
							InRaumObjektDistanceScrankeMin - DistanzAinzuscteleScrankeMin,
							DistanzAinzuscteleScrankeMax - InRaumObjektDistanceScrankeMax);

					if (ReegelungDistanceScpiilraumRest < 1111)
					{
						//	Absclus Tail Wirkung verhindere da zuukünftig mööglicerwaise wiider Manööver gesctartet werde sole.
						AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAus();

						var ManöverTyp = DistanzAinzuscteleScrankeMin.HasValue ?
							SictZuInRaumObjektManööverTypEnum.Orbit : SictZuInRaumObjektManööverTypEnum.Approach;

						AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.KonstruktInRaumManööver(
							TargetZuBearbeite,
							OverViewObjektZuBearbaite,
							ManöverTyp,
							DistanzAinzuscteleScrankeMin,
							DistanzAinzuscteleScrankeMax));

						if (ReegelungDistanceScpiilraumRest < 4444)
						{
							AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.KonstruktAfterburnerScalteAin());
						}
					}
				}
			}


			/*
			 * 2015.02.07
			 * 
			 * Ersaz durc AufgaabeParamInfoPanelRouteFüüreAus.
			 * 
			if (true == InfoPanelRouteFüüreAus)
			{
				if (null != AktioonUndockFraigaabeNictUrsace)
				{
					AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAus();
				}
				else
				{
					if (null == AusScnapscusGbsInfoPanelRouteCurrentInfo)
					{
						AufgaabeParamZerleegungErgeebnis.FüügeAn(
							AufgaabeParamAndere.KonstruktInfoPanelExpand(SictInfoPanelTypSictEnum.SystemInfo),
							false);
					}

					if (true == AusScnapscusGbsInfoPanelRouteExpanded &&
						null != AusScnapscusGbsInfoPanelRouteCurrentInfo)
					{
						if ((true == SelbsctShipWarpScrambled) ||
							(DockedLezteAlterMili < 3555) ||
							(WarpingLezteAlterMili < 4444) ||
							(JumpingLezteAlterMili < 4444))
						{
							AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAus();
						}
						else
						{
							if (500 <= AusScnapscusGbsInfoPanelRouteCurrentInfo.SolarSystemSecurityLevelMili)
							{
								var MarkerNääxte = AusScnapscusGbsInfoPanelRoute.MengeMarker.FirstOrDefaultNullable();

								if (null == MarkerNääxte)
								{
									//	!!!!	Meldung Feeler
								}
								else
								{
									AufgaabeParamZerleegungErgeebnis.FüügeAn(
										AufgaabeParamAndere.AufgaabeAktioonMenu(MarkerNääxte,
										new SictAnforderungMenuKaskaadeAstBedingung(
											new string[] { "Jump through stargate", "Dock", "Warp to within\\s*\\d" }, true)));
								}
							}
							else
							{
								AufgaabeParamZerleegungErgeebnis.FüügeAn(
									AufgaabeParamAndere.KonstruktNaacNuzerMeldungZuEveOnline(
									SictNaacNuzerMeldungZuEveOnline.ErrorGenerel(-1, SictNaacNuzerMeldungZuEveOnlineCauseTypeBinär.RoutePathSecurityLevelTooLow)),
									false);
							}
						}
					}
					else
					{
						AufgaabeParamZerleegungErgeebnis.FüügeAn(
							AufgaabeParamAndere.KonstruktInfoPanelExpand(SictInfoPanelTypSictEnum.Route),
							false);
					}
				}
			}
			 * */

			if (null != ColumnHeaderSort)
			{
				AufgaabeParamZerleegungErgeebnis.FüügeAn(
					AufgaabeParamAndere.KonstruktMausPfaad(SictAufgaabeParamMausPfaad.KonstruktMausKlikLinx(ColumnHeaderSort)));
			}

			if (null != ListEntryMaceSictbar)
			{
				if (ListEntryExpand.InLezteScnapscusSictbar())
				{
				}
				else
				{
					//	!!!!	zuugehöörige List ermitle.
				}
			}

			if (null != ListEntryExpand)
			{
				if (ListEntryExpand.IstExpanded ?? false)
				{
				}
				else
				{
					AufgaabeParamZerleegungErgeebnis.FüügeAn(
						AufgaabeParamAndere.KonstruktListEntryToggleExpandCollapse(ListEntryExpand));
				}
			}

			if (null != ListEntryCollapse)
			{
				if (ListEntryCollapse.IstExpanded ?? true)
				{
					AufgaabeParamZerleegungErgeebnis.FüügeAn(
						AufgaabeParamAndere.KonstruktListEntryToggleExpandCollapse(ListEntryCollapse));
				}
				else
				{
				}
			}

			if (null != ListEntryToggleExpandCollapse)
			{
				var ListEntryToggleExpandCollapseScnapscus = ListEntryToggleExpandCollapse.AingangScnapscusTailObjektIdentLezteBerecne();

				if (null == ListEntryToggleExpandCollapseScnapscus)
				{
					//	!!!!	Meldung Feeler
				}
				else
				{
					var GroupExpander = ListEntryToggleExpandCollapseScnapscus.GroupExpander;

					if (null == GroupExpander)
					{
						//	!!!!	Meldung Feeler
					}
					else
					{
						AufgaabeParamZerleegungErgeebnis.FüügeAn(
							AufgaabeParamAndere.KonstruktMausPfaad(SictAufgaabeParamMausPfaad.KonstruktMausKlikLinx(GroupExpander)));
					}
				}
			}

			if (null != ModuleScalteAin)
			{
				if (true == ModuleScalteAin.AktiivBerecne(1))
				{
				}
				else
				{
					var AinscalteVerhindert = false;

					if (true == SelbsctShipCloaked)
					{
						if (true == ModuleScalteAin.AnnaameAinscalteVerhindertDurcCloak)
						{
							AinscalteVerhindert = true;
						}
					}

					if (AinscalteVerhindert)
					{
						AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAus();
					}
					else
					{
						AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.KonstruktModuleScalteUm(ModuleScalteAin));
					}
				}
			}

			if (null != ModuleScalteAus)
			{
				if (!(true == ModuleScalteAus.AktiivBerecne()))
				{
				}
				else
				{
					AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.KonstruktModuleScalteUm(ModuleScalteAus));
				}
			}

			if (null != ModuleScalteUm)
			{
				bool UmscalteVerhindert = false;

				if (true == SelbsctShipDocking ||
					true == SelbsctShipDocked)
				{
					UmscalteVerhindert = true;
				}

				if (null == MengeModuleUmscaltFraigaabe)
				{
					UmscalteVerhindert = true;
				}
				else
				{
					if (!MengeModuleUmscaltFraigaabe.Contains(ModuleScalteUm))
					{
						UmscalteVerhindert = true;
					}
				}

				if (UmscalteVerhindert)
				{
					AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAus();
				}
				else
				{
					int? FürSlotToggleTasteFIndex = null;

					var ModuleButtonHintNocGültig = ModuleScalteUm.ModuleButtonHintGültigMitZait;

					if (null != ModuleButtonHintNocGültig.Wert)
					{
						var ModuleButtonHint = ModuleButtonHintNocGültig.Wert.ModuleButtonHint;

						if (null != ModuleButtonHint)
						{
							if (true == ModuleButtonHint.ShortcutModifierNict)
							{
								FürSlotToggleTasteFIndex = ModuleButtonHint.ShortcutTasteFIndex;
							}
						}
					}

					if (FürSlotToggleTasteFIndex.HasValue)
					{
						var IndexKey = (System.Windows.Input.Key)((int)System.Windows.Input.Key.F1 + (FürSlotToggleTasteFIndex.Value - 1));

						var VorsclaagWirkung = SictVorsclaagNaacProcessWirkung.VorsclaagWirkungKey(IndexKey);

						AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.KonstruktNaacNuzerVorsclaagWirkung(VorsclaagWirkung));
					}
					else
					{
						AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.KonstruktMausPfaad(SictAufgaabeParamMausPfaad.KonstruktMausKlikLinx(ModuleScalteUm.GbsObjektToggleBerecne())));
					}
				}
			}

			if (null != ModuleMesungModuleButtonHint)
			{
				//	Um zu vermaide das andere Aufgaabe den Tooltip unterbrece werd in jeedem Scrit das beweege der Maus wiiderhoolt.
				AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAus();

				if (!GbsMengeMenuMitBeginZait.IsNullOrEmpty())
				{
					//	ModuleButtonHint werd nur angezait kain Menu vorhande, daher Menu irgendwii entferne

					AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.KonstruktMenuEntferne());
				}

				AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.KonstruktMausPfaad(SictAufgaabeParamMausPfaad.Konstrukt(ModuleMesungModuleButtonHint.GbsObjektToggleBerecne())));
			}

			if (true == AfterburnerScalteAin)
			{
				if (null == ModuleAfterburner)
				{
					AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAus();
				}
				else
				{
					AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.KonstruktModuleScalteAin(ModuleAfterburner));
				}
			}

			if (true == AufgaabeParam.AktioonCargoDurcsuuce)
			{
				bool AktioonCargoDurcsuuceZerleegungVolsctändig = false;

				if (null != TargetZuBearbeite)
				{
					AufgaabeParamZerleegungErgeebnis.FüügeAn(
						AufgaabeParamAndere.AufgaabeDistanceAinzusctele(
						TargetZuBearbeite,
						null,
						AktioonInRaumObjektCargoDurcsuuceDistanceScrankeMax));
				}

				if (null != OverViewObjektZuBearbaite)
				{
					AufgaabeParamZerleegungErgeebnis.FüügeAn(
						AufgaabeParamAndere.AufgaabeDistanceAinzusctele(
						OverViewObjektZuBearbaite,
						null,
						AktioonInRaumObjektCargoDurcsuuceDistanceScrankeMax));
				}

				var MengeZuWindowInventoryAuswaalReczMengeKandidaatOverViewObjekt =
					OverviewUndTarget.MengeZuWindowInventoryAuswaalReczMengeKandidaatInRaumObjekt;

				if (null != OverViewObjektZuBearbaite)
				{
					VonSensor.WindowInventoryPrimary WindowInventory = null;

					if (null != MengeZuWindowInventoryAuswaalReczMengeKandidaatOverViewObjekt)
					{
						foreach (var ZuWindowInventoryMengeKandidaatOverViewObjekt in MengeZuWindowInventoryAuswaalReczMengeKandidaatOverViewObjekt)
						{
							var WindowInventoryVerknüpfungMitOverView = ZuWindowInventoryMengeKandidaatOverViewObjekt.Key;

							var WindowInventoryMengeKandidaatOverViewObjekt = ZuWindowInventoryMengeKandidaatOverViewObjekt.Value;

							if (null == WindowInventoryVerknüpfungMitOverView ||
								null == WindowInventoryMengeKandidaatOverViewObjekt)
							{
								continue;
							}

							var KandidaatWindowInventory = WindowInventoryVerknüpfungMitOverView.WindowInventory;

							if (null == KandidaatWindowInventory)
							{
								continue;
							}

							if (!(WindowInventoryMengeKandidaatOverViewObjekt.Contains(OverViewObjektZuBearbaite)))
							{
								continue;
							}

							var WindowInventoryZuAuswaalReczMengeKandidaatLinxTreeEntry = KandidaatWindowInventory.ZuAuswaalReczMengeKandidaatLinxTreeEntry;

							if (null == WindowInventoryZuAuswaalReczMengeKandidaatLinxTreeEntry)
							{
								continue;
							}

							WindowInventory = KandidaatWindowInventory;
						}
					}

					if (null == WindowInventory)
					{
						AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAus();

						if (OverViewObjektZuBearbaite.SictungLezteDistanceScrankeMaxScpezOverview < AktioonInRaumObjektCargoDurcsuuceDistanceScrankeMax)
						{
							AufgaabeParamZerleegungErgeebnis.FüügeAn(
								AufgaabeParamAndere.AufgaabeAktioonMenu(
								OverViewObjektZuBearbaite,
								new SictAnforderungMenuKaskaadeAstBedingung("open cargo", true)));
						}
					}
					else
					{
						var WindowInventoryLinxTreeEntryActiveShip = WindowInventory.LinxTreeEntryActiveShip;

						var AuswaalReczInventory = WindowInventory.AuswaalReczInventory;

						if (null != AuswaalReczInventory &&
							null != WindowInventoryLinxTreeEntryActiveShip)
						{
							if (true == AuswaalReczInventory.SictwaiseScaintGeseztAufListNict)
							{
								AufgaabeParamZerleegungErgeebnis.FüügeAn(
									AufgaabeParamAndere.KonstruktInventorySezeSictTypAufList(WindowInventory),
									false);
							}
							else
							{
								if (null != AuswaalReczInventory.ListeItem)
								{
									var ListeItemZuÜberneeme = WindowInventory.AuswaalReczInventory.ListeItem.Take(0).ToList();

									var WindowInventoryZuAuswaalReczMengeKandidaatLinxTreeEntry = WindowInventory.ZuAuswaalReczMengeKandidaatLinxTreeEntry;

									var AnnaameAuswaalReczContainerDistanceScrankeMaxAingehalte = false;

									if (null != WindowInventoryZuAuswaalReczMengeKandidaatLinxTreeEntry)
									{
										AnnaameAuswaalReczContainerDistanceScrankeMaxAingehalte =
											WindowInventoryZuAuswaalReczMengeKandidaatLinxTreeEntry
											.All((Kandidaat) => Kandidaat.ObjektDistance <= AktioonInRaumObjektCargoDurcsuuceDistanceScrankeMax);
									}

									foreach (var InInventoryItem in WindowInventory.AuswaalReczInventory.ListeItem)
									{
										var ItemÜberneeme = false;

										if (VonSensor.InventoryItem.HinraicendGlaicwertig(
											InInventoryItem,
											AusInventoryItemZuÜbertraageNaacActiveShip))
										{
											ItemÜberneeme = true;
										}

										if (!ItemÜberneeme)
										{
											continue;
										}

										ListeItemZuÜberneeme.Add(InInventoryItem);

										if (AnnaameAuswaalReczContainerDistanceScrankeMaxAingehalte)
										{
											AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.KonstruktInventoryItemTransport(
												new SictInventoryItemTransport(
													WindowInventory,
													WindowInventoryLinxTreeEntryActiveShip,
													new VonSensor.InventoryItem[] { InInventoryItem })));
										}

										break;
									}

									if (1 < ExtractFromOldAssembly.Bib3.Extension.CountNullable(WindowInventoryZuAuswaalReczMengeKandidaatLinxTreeEntry))
									{
										AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAus();

										//	In Baum Linx sind meerere Objekte abgebildet welce deen recz abgebildete Container repräsentiire könten.
										//	Um aindoitige Zuuorndung vorneeme zu köne üüberzäälige ainträäge aus Baum Linx scliise.

										var WindowInventoryZuAuswaalReczMengeKandidaatLinxTreeEntryFrüheste =
											WindowInventoryZuAuswaalReczMengeKandidaatLinxTreeEntry
											.FirstOrDefault((Entry) => null != Entry.TopContLabel);

										if (null != WindowInventoryZuAuswaalReczMengeKandidaatLinxTreeEntryFrüheste)
										{
											AufgaabeParamZerleegungErgeebnis.FüügeAn(
												SictAufgaabeParam.KonstruktAufgaabeParam(
												AufgaabeParamAndere.AufgaabeAktioonMenu(
												WindowInventoryZuAuswaalReczMengeKandidaatLinxTreeEntryFrüheste,
												"close",
												true),
												new string[] { "Inventory.Container[" + (WindowInventoryZuAuswaalReczMengeKandidaatLinxTreeEntryFrüheste.LabelText ?? "") + "].close" }));
										}
									}

									if (0 < ListeItemZuÜberneeme.Count)
									{
										AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAus();
									}
									else
									{
										//	Container oder Wreck entferne damit Automaat den nääxte Container ins auge fase kan.

										if (null != WindowInventory.LinxTreeListeEntry)
										{
											var ObjektAusgewäältTreeEntry = WindowInventoryZuAuswaalReczMengeKandidaatLinxTreeEntry.FirstOrDefault();

											if (null != ObjektAusgewäältTreeEntry)
											{
												/*
												 * Fals kain Aintraag für "Abandon" vorhande isc, werd davon ausgegange das Objekt beraits abandoned isc
												 * (könte zuusäzlic durc TopContIconColor geprüüft were), dan werd aintraag "Close" verwendet.
												 * **/

												/*
												 * 2014.00.13
												 * Änderung: Menu werd nit meer für OverView Objekt angefordert da es hiir zu verwexlung kaam (z.B.
												 * "The Damsel In Distress": zwai Cargo Container nahe baiainander waare nit ausainander zu halte).
												 * Sctatdese werd als nääxtes Versuuct das Menu für deen Aintraag im WindowInventory im Baum Linx anzufordere,
												 * dort isc auc Aintraag "Abandon" und "Close" vorhande.
												 * 
												var AbandonAnfoderderungGbsMenu =
													new SictAnforderungMenuBescraibung(
														ObjektAusgewäältTreeEntry.InGbsFläce,
														WindowInventory,
														true,
														new SictAnforderungMenuKaskaadeAstBedingung(new string[] { "Abandon Wreck", "Abandon Container", "Close", }));
												 * */

												AufgaabeParamZerleegungErgeebnis.FüügeAn(
													SictAufgaabeParam.KonstruktAufgaabeParam(
													AufgaabeParamAndere.AufgaabeAktioonMenu(
													ObjektAusgewäältTreeEntry,
													new SictAnforderungMenuKaskaadeAstBedingung(
														new string[] { "Abandon Wreck", "Abandon Container", "Close", },
														true),
													new string[] { "Inventory.Container[" + (ObjektAusgewäältTreeEntry.LabelText ?? "") + "].abandon or close" })));
											}
										}
									}
								}
							}
						}
					}
				}
			}

			if (false)
			{
				//	2015.01.06	Impl in Type AufgaabeParamDestrukt

				//	if (true == AufgaabeParam.AktioonWirkungDestrukt)
				{
					AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAus();

					bool DroneLaunchVolsctändig = false;

					if (ListeAbovemainMessageDronesLezteAlter < 3e+4)
					{
						DroneLaunchVolsctändig = true;
					}

					if (!DroneLaunchVolsctändig &&
						0 < ExtractFromOldAssembly.Bib3.Extension.CountNullable(MengeTargetNocSictbar) || (ScritNääxteJammed ?? false))
					{
						//	Drones Launch

						if (false == AnforderungDroneReturnLezte &&
							null != ScnapscusWindowDroneView)
						{
							//	Drones Launch

							if (0 < ScnapscusWindowDroneView.DronesInBayAnzaal)
							{
								/*
								 * 2013.09.24
								 * Anforderung vorerst nur für Scpeziaalfal das nur fünf drones in space sain köne.
								 * Scpääter sol Anzaal nuzbaarer drones berüksictigt were.
								 * */
								if ((AnnaameDroneControlCountScrankeMaxNulbar ?? 5) <= ScnapscusWindowDroneView.DronesInLocalSpaceAnzaal)
								{
									DroneLaunchVolsctändig = true;
								}
								else
								{
									AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.AufgaabeAktioonDroneLaunch());
								}
							}
							else
							{
								DroneLaunchVolsctändig = true;
							}
						}
					}

					var MengeModuleAinSol =
						ExtractFromOldAssembly.Bib3.Extension.IntersectNullable(
						MengeModuleUmscaltFraigaabe,
						ExtractFromOldAssembly.Bib3.Extension.WhereNullable(MengeModuleRepr, (KandidaatModule) =>
							(((true == KandidaatModule.IstWirkmitelDestrukt) &&
							(true == KandidaatModule.ChargeLoaded)) ||
							(true == KandidaatModule.IstTargetPainter)) &&
							!(true == KandidaatModule.AktiivBerecne(1))))
						.ToArrayNullable();

					var Target = TargetZuBearbeite;

					if (null == Target)
					{
						var MengeTargetPasend =
							OverviewUndTarget.MengeTargetTailmengePasendZuOverviewObjektBerecne(OverViewObjektZuBearbaite);

						if (null != MengeTargetPasend)
						{
							Target = MengeTargetPasend.OrderBy((KandidaatTarget) => KandidaatTarget.SictungLezteDistanceScrankeMaxScpezTarget ?? int.MaxValue).FirstOrDefault();
						}
					}

					if (null == Target)
					{
						if (null != OverViewObjektZuBearbaite)
						{
							if (!(GefectListeLockedTargetScranke <= ExtractFromOldAssembly.Bib3.Extension.CountNullable(MengeTargetNocSictbar)))
							{
								if (true == OverViewObjektZuBearbaite.TargetingOderTargeted &&
									7777 < OverViewObjektZuBearbaiteSictungLezteAlterMili)
								{
									AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.AufgaabeInOverViewMaceSictbar(OverViewObjektZuBearbaite));
								}
								else
								{
									var DistanceHinraicendGeringFürLock = true;

									if (0 < ExtractFromOldAssembly.Bib3.Extension.CountNullable(MengeTargetNocSictbar))
									{
										if (OverViewObjektZuBearbaite.SaitSictbarkaitLezteListeScritAnzaal < 1)
										{
											if (AnnaameTargetingDistanceScrankeMax < OverViewObjektZuBearbaite.SictungLezteDistanceScrankeMaxScpezOverview &&
												NuzerZaitMili - 4444 < OverViewObjektZuBearbaite.SictungLezteZait)
											{
												DistanceHinraicendGeringFürLock = false;
											}

											if (AnnaameGefectDistanzOptimum < OverViewObjektZuBearbaite.SictungLezteDistanceScrankeMaxScpezOverview)
											{
												DistanceHinraicendGeringFürLock = false;
											}
										}
									}

									if (DistanceHinraicendGeringFürLock)
									{
										AufgaabeParamZerleegungErgeebnis.FüügeAn(new AufgaabeParamLockTarget(OverViewObjektZuBearbaite));
									}
									else
									{
										AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAus();
									}
								}
							}

							if (!(OverViewObjektZuBearbaite.SictungLezteDistanceScrankeMaxScpezOverview < AnnaameGefectDistanzOptimum))
							{
								AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.AufgaabeDistanceAinzusctele(OverViewObjektZuBearbaite, null, AnnaameGefectDistanzOptimum));
							}
						}
					}
					else
					{
						var AnnaameDroneCommandRange = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.AnnaameDroneCommandRange;

						var TargetDistancePasendFürModule = false;
						var TargetDistancePasendFürDrone = false;

						//	!!!!	zu ergänze: Berecnung soldistance für Turret
						//	!!!!	zu ergänze: Berecnung TargetMengeModuleAinSol: untermenge von MengeModuleAinSol da Module untersciidlice optimaale Distance haabe (TargetPainter)
						if (Target.SictungLezteDistanceScrankeMaxScpezTarget < AnnaameGefectDistanzOptimum)
						{
							TargetDistancePasendFürModule = true;
						}

						{
							AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.AufgaabeDistanceAinzusctele(Target, null, AnnaameGefectDistanzOptimum));
						}

						if (Target.SictungLezteDistanceScrankeMaxScpezTarget < AnnaameDroneCommandRange)
						{
							TargetDistancePasendFürDrone = true;
						}

						var DroneEngage =
							DroneLaunchVolsctändig &&
							TargetDistancePasendFürDrone &&
							true == FürWirkungDestruktAufgaabeDroneEngageTarget;

						if (DroneEngage || !MengeModuleAinSol.IsNullOrEmpty())
						{
							{
								if (TargetDistancePasendFürModule)
								{
									AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.KonstruktTargetInputFookusSeze(Target));
								}
							}
						}

						if (!MengeModuleAinSol.IsNullOrEmpty())
						{
							//	Hiir werd nuur waitergemact wen noc mindesctens ain Module noc aigescaltet werde sol.

							if (true == Target.InputFookusTransitioonLezteZiilWert)
							{
								if (TargetDistancePasendFürModule)
								{
									AufgaabeParamZerleegungErgeebnis.FüügeAn(
										MengeModuleAinSol.Select((ModuleAinSol) => AufgaabeParamAndere.KonstruktModuleScalteAin(ModuleAinSol)));
								}
							}
						}

						if (DroneEngage)
						{
							if (true == Target.InputFookusTransitioonLezteZiilWert)
							{
								AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.KonstruktDronesEngage(Target));
							}
						}
					}
				}
			}

			if (null != TargetUnLock)
			{
				AufgaabeParamZerleegungErgeebnis.FüügeAn(
					AufgaabeParamAndere.AufgaabeAktioonMenu(TargetUnLock.SezeInputFookusGbsObjektBerecne(), new SictAnforderungMenuKaskaadeAstBedingung("unlock", true)));
			}

			if (null != TargetInputFookusSeze)
			{
				if (true == TargetInputFookusSeze.InputFookusTransitioonLezteZiilWert)
				{
				}
				else
				{
					AufgaabeParamZerleegungErgeebnis.FüügeAn(
						AufgaabeParamAndere.KonstruktMausPfaad(SictAufgaabeParamMausPfaad.KonstruktMausKlikLinx(TargetInputFookusSeze.SezeInputFookusGbsObjektBerecne())));
				}
			}

			if (true == AktioonInRaumObjektActivate)
			{
				if (null == OverViewObjektZuBearbaite)
				{
				}
				else
				{
					if (OverViewObjektZuBearbaite.SictungLezteDistanceScrankeMaxScpezOverview <= AktioonInRaumObjektActivateDistanceScrankeMax)
					{
						if (true == RaumVerlaseFraigaabe)
						{
							AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.AufgaabeAktioonMenu(OverViewObjektZuBearbaite,
								new SictAnforderungMenuKaskaadeAstBedingung("activate", true)));
						}
						else
						{
							//	Aktiviirung könte verlase von Grid (z.B. Acc-Gate) und damit Verlust der Drone auslööse.
							AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAus();
						}
					}
					else
					{
						AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.AufgaabeDistanceAinzusctele(OverViewObjektZuBearbaite,
							null, AktioonInRaumObjektActivateDistanceScrankeMax),
							false);
					}
				}
			}

			if (null != MenuListeAstBedingung)
			{
				var tDebug = MenuListeAstBedingung?.ElementAtOrDefault(1)?.ListePrioEntryRegexPattern?.ElementAtOrDefault(0);

				var tDebug1 = AusScnapscusAuswertungZuusctand?.MengeMenu?.Length ?? 0;

				if (true == AktioonMenuBegin)
				{
					if (null != MenuWurzelGbsObjekt)
					{
						var MenuWurzelGbsFläche = MenuWurzelGbsObjekt;

						var MenuWurzelGbsObjektAlsTarget = MenuWurzelGbsObjekt as ShipUiTarget;

						if (null != MenuWurzelGbsObjektAlsTarget)
						{
							MenuWurzelGbsFläche = MenuWurzelGbsObjektAlsTarget.GbsObjektInputFookusSeze;
						}

						AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.KonstruktMausPfaad(SictAufgaabeParamMausPfaad.KonstruktMausKlikRecz(MenuWurzelGbsObjekt)));
					}
				}
				else
				{
					var MenuZerleegungVolsctändig = false;

					bool MenuBeginMööglic = true;
					bool MenuFortsazFraigaabe = false;

					var MenuWurzelGbsObjektAbbild = MenuWurzelGbsObjekt;

					if (null == MenuWurzelGbsObjektAbbild)
					{
						if (null != TargetZuBearbeite)
						{
							MenuWurzelGbsObjektAbbild = TargetZuBearbeite.FläceFürMenuWurzelBerecne();
						}
						else
						{
							if (null != OverViewObjektZuBearbaite)
							{
								MenuWurzelGbsObjektAbbild = OverViewObjektZuBearbaite.AingangScnapscusUnglaicDefaultTailObjektIdentLezteBerecne();
							}
						}
					}

					if (null != GbsMenuKaskaadeLezteNocOfe)
					{
						MenuFortsazFraigaabe = ObjektMitIdentInt64.Identisc(GbsMenuKaskaadeLezteNocOfe.MenuWurzelAnnaameLezte, MenuWurzelGbsObjektAbbild);
					}

					if (null != OverViewObjektZuBearbaite)
					{
						if (!(AusOverviewObjektInputFookusExklusiiv == OverViewObjektZuBearbaite))
						{
							MenuFortsazFraigaabe = false;
						}

						if (!(OverViewObjektZuBearbaite.SaitSictbarkaitLezteListeScritAnzaal < 1))
						{
							AufgaabeParamZerleegungErgeebnis.FüügeAn(
								AufgaabeParamAndere.AufgaabeInOverViewMaceSictbar(OverViewObjektZuBearbaite));

							MenuBeginMööglic = false;
						}
					}

					VonSensor.MenuEntry MenuEntryAuswaalNääxte = null;
					bool? AuswaalEntryVerursactMenuSclus = null;

					if (MenuFortsazFraigaabe)
					{
						int MenuAstPasendLezteIndex = 0;

						for (int MenuAstIndex = 0; MenuAstIndex < Math.Min(MenuListeAstBedingung.Length, GbsListeMenu.Length); MenuAstIndex++)
						{
							MenuAstPasendLezteIndex = MenuAstIndex;

							var MenuAst = GbsListeMenu[MenuAstIndex];

							var MenuAstBedingung = MenuListeAstBedingung[MenuAstIndex];

							if (null == MenuAst)
							{
								break;
							}

							if (null == MenuAstBedingung)
							{
								break;
							}

							var MenuAstBedingungListePrioEntryRegexPattern = MenuAstBedingung.ListePrioEntryRegexPattern;

							if (null == MenuAstBedingungListePrioEntryRegexPattern)
							{
								break;
							}

							var MenuAstScnapscus = MenuAst.AingangScnapscusTailObjektIdentLezteBerecne();

							if (null == MenuAstScnapscus)
							{
								break;
							}

							var MenuAstListeEntry = MenuAstScnapscus.ListeEntry;

							if (null == MenuAstListeEntry)
							{
								break;
							}

							var MenuAstListePrioMengeMenuEntryPasend = MenuAstBedingung.AusMengeMenuEntryGibUntermengePasendGrupiirtNaacPrio(MenuAstListeEntry);

							if (null == MenuAstListePrioMengeMenuEntryPasend)
							{
								break;
							}

							var ListeMenuEntryPasend =
								Bib3.Glob.ArrayAusListeFeldGeflact(MenuAstListePrioMengeMenuEntryPasend)
								.Where((Entry) => null != Entry)
								.Distinct()
								.ToArray();

							var MenuEntryPasend =
								ListeMenuEntryPasend
								.FirstOrDefault((KandidaatEntry) => true == KandidaatEntry.Highlight) ??
								ListeMenuEntryPasend.FirstOrDefault();

							if (null == MenuEntryPasend)
							{
								break;
							}

							MenuEntryAuswaalNääxte = MenuEntryPasend;

							if (true == MenuAstBedingung.AuswaalEntryVerursactMenuSclus)
							{
								AuswaalEntryVerursactMenuSclus = true;
							}

							/*
							 * 2015.01.16
							 * Anpasung an in Eve Online Client Tiamat Änderung:
							 * nur noc der lezte aktiive MenuEntry enthalt Highlight.
							 * 
							if (!(true == MenuEntryPasend.Highlight))
							{
								break;
							}
							 * */
						}
					}

					if (MenuBeginMööglic)
					{
						if (!MenuFortsazFraigaabe || null == MenuEntryAuswaalNääxte)
						{
							AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.KonstruktMenuBegin(MenuWurzelGbsObjektAbbild, MenuListeAstBedingung));
						}
						else
						{
							{
								//	Temp Debug
								if (1 < GbsMenuKaskaadeLezteNocOfe.ListeMenu.CountNullable())
								{

								}
							}
							AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.KonstruktMenuEntry(MenuEntryAuswaalNääxte));

							if (true == AuswaalEntryVerursactMenuSclus)
							{
								MenuZerleegungVolsctändig = true;
							}
						}
					}

					AufgaabeParamZerleegungErgeebnis.FüügeAn((SictAufgaabeParam)null, MenuZerleegungVolsctändig);
				}
			}

			if (null != MenuEntry)
			{
				if (GbsMenuKaskaadeLezteNocOfeAlter < 1000 * 16)
				{
					AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.KonstruktMausPfaad(SictAufgaabeParamMausPfaad.KonstruktMausKlikLinx(MenuEntry)));
				}
				else
				{
					//	Menu zu alt, entferne.

					AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.KonstruktMenuEntferne(), false);
				}
			}

			if (null != LobbyAgentEntryStartConversation)
			{
				var ButtonStartConversation = LobbyAgentEntryStartConversation.ButtonStartConversation;

				if (null == ButtonStartConversation)
				{
					AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.AufgaabeAktioonMenu(LobbyAgentEntryStartConversation, "start conversation", true));
				}
				else
				{
					AufgaabeParamZerleegungErgeebnis.FüügeAn(
						AufgaabeParamAndere.KonstruktMausPfaad(SictAufgaabeParamMausPfaad.KonstruktMausKlikLinx(ButtonStartConversation)));
				}
			}

			if (null != MissionObjectiveMese)
			{
				if (null != GbsListeWindowAgentDialogueFrüüheste)
				{
					//	vorhandene AgentDialogue werde gesclose damit Automaat naac öfne von AgentDialogue erkent kan das Window aktualisiirt wurde.
					AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.KonstruktWindowMinimize(GbsListeWindowAgentDialogueFrüüheste));
				}
				else
				{
					AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.KonstruktMissionStartConversation(MissionObjectiveMese));
				}
			}

			if (null != MissionButtonUtilmenuObjectiveZuMese)
			{
				//	Zerleegung Unvolsctändig da naac Öfne des AgentDialogue mööglicerwaise noc Button "View Mission" betäätigt werde mus.
				AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAus();

				var MissionButtonUtilmenuObjectiveZuMeseZerleegungVolsctändig = false;

				if (null != ScnapscusMengeWindowAgentDialogue)
				{
					//	Für ale beraits geöfnete WindowAgentDialogue di Button "View Mission" betäätige.
					foreach (var WindowAgentDialogue in ScnapscusMengeWindowAgentDialogue)
					{
						if (null == WindowAgentDialogue)
						{
							continue;
						}

						var ButtonViewMission = WindowAgentDialogue.ButtonViewMission;

						if (null != ButtonViewMission)
						{
							AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.KonstruktMausPfaad(SictAufgaabeParamMausPfaad.KonstruktMausKlikLinx(ButtonViewMission)));
							return;
						}
					}
				}

				if (null != UtilmenuMission)
				{
					var ButtonSctartConversation = UtilmenuMission.ButtonSctartConversation;

					if (string.Equals(UtilmenuMission.MissionTitelText, MissionButtonUtilmenuObjectiveZuMese.Bescriftung) &&
						null != ButtonSctartConversation)
					{
						AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.KonstruktMausPfaad(SictAufgaabeParamMausPfaad.KonstruktMausKlikLinx(ButtonSctartConversation)));

						MissionButtonUtilmenuObjectiveZuMeseZerleegungVolsctändig = true;
					}
				}

				if (!MissionButtonUtilmenuObjectiveZuMeseZerleegungVolsctändig)
				{
					AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.KonstruktMausPfaad(SictAufgaabeParamMausPfaad.KonstruktMausKlikLinx(MissionButtonUtilmenuObjectiveZuMese)));
				}

				AufgaabeParamZerleegungErgeebnis.FüügeAn((SictAufgaabeParam)null, MissionButtonUtilmenuObjectiveZuMeseZerleegungVolsctändig);
			}

			if (null != WindowAgentDialogueMissionAcceptOderRequest)
			{
				var Button = WindowAgentDialogueMissionAcceptOderRequest.ButtonAccept;

				if (null == Button)
				{
					AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAus();

					Button =
						WindowAgentDialogueMissionAcceptOderRequest.ButtonRequestMission ??
						WindowAgentDialogueMissionAcceptOderRequest.ButtonViewMission;

					if (null == Button)
					{
						//	!!!!	Meldung Feeler
					}
					else
					{
						AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.KonstruktMausPfaad(SictAufgaabeParamMausPfaad.KonstruktMausKlikLinx(Button)));
					}
				}
				else
				{
					AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.KonstruktMausPfaad(SictAufgaabeParamMausPfaad.KonstruktMausKlikLinx(Button)));
				}
			}

			if (null != MissionDecline)
			{
				var MissionDeclineWindowScnapscus = MissionDecline.WindowAgentDialogueLezteBerecne(NuzerZaitMili);

				if (null == MissionDeclineWindowScnapscus)
				{
					AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.KonstruktMissionStartConversation(MissionDecline), false);
				}
				else
				{
					AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.KonstruktWindowAgentDialogueMissionDecline(MissionDeclineWindowScnapscus));
				}
			}

			if (null != MissionAccept)
			{
				var MissionAcceptWindowScnapscus = MissionAccept.WindowAgentDialogueLezteBerecne(NuzerZaitMili);

				if (null == MissionAcceptWindowScnapscus)
				{
					AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.KonstruktMissionStartConversation(MissionAccept), false);
				}
				else
				{
					AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.KonstruktWindowAgentDialogueMissionAcceptOderRequest(MissionAcceptWindowScnapscus));
				}
			}

			if (null != WindowAgentDialogueMissionDecline)
			{
				var Button = WindowAgentDialogueMissionDecline.ButtonDecline;

				if (null == Button)
				{
					AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAus();

					Button =
						WindowAgentDialogueMissionDecline.ButtonRequestMission ??
						WindowAgentDialogueMissionDecline.ButtonViewMission;

					if (null == Button)
					{
						//	!!!!	Meldung Feeler
					}
					else
					{
						AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.KonstruktMausPfaad(SictAufgaabeParamMausPfaad.KonstruktMausKlikLinx(Button)));
					}
				}
				else
				{
					AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.KonstruktMausPfaad(SictAufgaabeParamMausPfaad.KonstruktMausKlikLinx(Button)));
				}
			}

			if (null != MissionStartConversation)
			{
				var UtilmenuZuZaitNulbar = MissionStartConversation.UtilmenuZuZait;

				var ButtonUtilmenu = MissionStartConversation.ButtonUtilmenu;

				GbsElementMitBescriftung ButtonSctartConversation = null;

				if (UtilmenuZuZaitNulbar.HasValue)
				{
					if (null != UtilmenuZuZaitNulbar.Value.Wert)
					{
						ButtonSctartConversation = UtilmenuZuZaitNulbar.Value.Wert.ButtonSctartConversation;
					}
				}

				var ZuMissionStartConversationMengeWindowAgentDialogue =
					ExtractFromOldAssembly.Bib3.Extension.WhereNullable(
					ScnapscusMengeWindowAgentDialogue,
					(KandidaatWindowAgentDialogue) => true == MissionStartConversation.WindowAgentDialoguePasendZuAgent(KandidaatWindowAgentDialogue));

				var ZuMissionStartConversationWindowAgentDialogue =
					ExtractFromOldAssembly.Bib3.Extension.FirstOrDefaultNullable(ZuMissionStartConversationMengeWindowAgentDialogue);

				if (null == ZuMissionStartConversationWindowAgentDialogue)
				{
					var WindowLobbyAgentEntry = ExtractFromOldAssembly.Bib3.Extension.FirstOrDefaultNullable(WindowLobbyMengeAgentEntry,
						(Kandidaat) => true == MissionStartConversation.LobbyAgentEntryPasendZuMission(Kandidaat));

					if (null == WindowLobbyAgentEntry)
					{
						if (null == ButtonSctartConversation)
						{
							AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAus();

							if (null == ButtonUtilmenu)
							{
							}
							else
							{
								AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.KonstruktMausPfaad(SictAufgaabeParamMausPfaad.KonstruktMausKlikLinx(ButtonUtilmenu)));
							}
						}
						else
						{
							AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.KonstruktMausPfaad(SictAufgaabeParamMausPfaad.KonstruktMausKlikLinx(ButtonSctartConversation)));
						}
					}
					else
					{
						AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.KonstruktLobbyAgentEntryStartConversation(WindowLobbyAgentEntry));
					}
				}
				else
				{
					var Button =
						ZuMissionStartConversationWindowAgentDialogue.ButtonRequestMission ??
						ZuMissionStartConversationWindowAgentDialogue.ButtonViewMission;

					if (null == Button)
					{
					}
					else
					{
						AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.KonstruktMausPfaad(SictAufgaabeParamMausPfaad.KonstruktMausKlikLinx(Button)));
					}
				}
			}

			if (null != FittingZuApliziire)
			{
				var FittingZuApliziireAusFittingManagementFittingZuLaade = ExtractFromOldAssembly.Bib3.Glob.TrimNullable(FittingZuApliziire.AusFittingManagementFittingZuLaade);

				if (null != FittingZuApliziireAusFittingManagementFittingZuLaade)
				{
					if (null == ScnapscusWindowFittingMgmt)
					{
						AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAus();

						if (null == ScnapscusWindowFittingWindow)
						{
							if (null == WindowLobbyKnopfFitting)
							{
								//	Meldung Feeler

								AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAus();
							}
							else
							{
								AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.KonstruktMausPfaad(SictAufgaabeParamMausPfaad.KonstruktMausKlikLinx(WindowLobbyKnopfFitting)));
							}
						}
						else
						{
							/*
							 * 2015.02.25
							 * 
							var FittingButtonGroup = ScnapscusWindowFittingWindow.FittingButtonGroup;

							var FittingButtonGroupMengeButton = (null == FittingButtonGroup) ? null : FittingButtonGroup.MengeButton;

							var FittingButtonBrowse =
								Bib3.Glob.FirstOrDefaultNullable(
								FittingButtonGroupMengeButton,
								(Kandidaat) => Regex.Match(Kandidaat.Bescriftung ?? "", "browse", RegexOptions.IgnoreCase).Success);
							 * */

							var FittingButtonBrowse = ScnapscusWindowFittingWindow.ButtonStoredFittingsBrowse;

							if (null == FittingButtonBrowse)
							{
								//	Meldung Feeler

								AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAus();
							}
							else
							{
								AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.KonstruktMausPfaad(SictAufgaabeParamMausPfaad.KonstruktMausKlikLinx(FittingButtonBrowse)));
							}
						}
					}
					else
					{
						var WindowFittingMgmtMengeFittingEntry = ScnapscusWindowFittingMgmt.MengeFittingEntry;

						var FittingEntry =
							ExtractFromOldAssembly.Bib3.Extension.FirstOrDefaultNullable(
							WindowFittingMgmtMengeFittingEntry,
							(Kandidaat) => string.Equals(
								Kandidaat.Bescriftung,
								FittingZuApliziireAusFittingManagementFittingZuLaade,
								StringComparison.InvariantCultureIgnoreCase));

						if (null == FittingEntry)
						{
							AufgaabeParamZerleegungErgeebnis.FüügeAn(
								AufgaabeParamAndere.KonstruktNaacNuzerMeldungZuEveOnline(
								SictNaacNuzerMeldungZuEveOnline.ErrorGenerel(-1,
								new SictNaacNuzerMeldungZuEveOnlineCause(FittingManagementMissingFitting: FittingZuApliziireAusFittingManagementFittingZuLaade))),
								false);
						}
						else
						{
							var ListeAusGbsAbovemainMessage = ListeAusGbsAbovemainMessageMitZait();

							var ListeAusGbsAbovemainMessageNocAngezaigt =
								ExtractFromOldAssembly.Bib3.Extension.WhereNullable(ListeAusGbsAbovemainMessage, (Kandidaat) => !Kandidaat.EndeZait.HasValue);

							if (ListeAusGbsAbovemainMessageNocAngezaigt.IsNullOrEmpty())
							{
								AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.AufgaabeAktioonMenu(FittingEntry,
									new SictAnforderungMenuKaskaadeAstBedingung("load Fitting", true)));
							}
							else
							{
								AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAus();

								/*
								* Fals noc Abovemain Message angezaigt werde, warte bis diise weg sind wail Erfolg von Versuuc Fitting in mance Fäle anhand
								* von noier Abovemain Message beurtailt werd.
								* */
							}
						}

					}
				}
			}

			if (true == AktioonUnDock)
			{
				if (null == AktioonUndockFraigaabeNictUrsace)
				{
					if (null == ScnapscusWindowLobby)
					{
						AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAus();
					}
					else
					{
						if (true == ScnapscusWindowLobby.UnDocking)
						{
						}
						else
						{
							var KnopfUndock = ScnapscusWindowLobby.KnopfUndock;

							if (null == KnopfUndock)
							{
								AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAus();
							}
							else
							{
								AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.KonstruktMausPfaad(SictAufgaabeParamMausPfaad.KonstruktMausKlikLinx(KnopfUndock)));
							}
						}
					}
				}
				else
				{
					AufgaabeParamZerleegungErgeebnis.FüügeAn(
						AufgaabeParamAndere.KonstruktNaacNuzerMeldungZuEveOnline(
						new SictNaacNuzerMeldungZuEveOnline(
							-1,
							Severity: SictNaacNuzerMeldungZuEveOnlineSeverity.Error,
							UndockPreventedCause: AktioonUndockFraigaabeNictUrsace)));
				}
			}

			if (null != InventorySezeSictTypAufList)
			{
				var AuswaalReczInventorySictMengeButton = InventorySezeSictTypAufList.AuswaalReczInventorySictMengeButton;

				var AuswaalReczInventorySictButton =
					(null == AuswaalReczInventorySictMengeButton) ? null :
					AuswaalReczInventorySictMengeButton
					.OrderBy((Kandidaat) => (Kandidaat.InGbsFläce).ZentrumLaage.A)
					.LastOrDefault();

				AufgaabeParamZerleegungErgeebnis.FüügeAn(
					AufgaabeParamAndere.KonstruktNaacNuzerMeldungZuEveOnline(
					SictNaacNuzerMeldungZuEveOnline.WarningGenerel(-1, new SictNaacNuzerMeldungZuEveOnlineCause(CauseText: "Inventory View not set to List"))));

				if (null == AuswaalReczInventorySictButton)
				{
					AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAus();
				}
				else
				{
					AufgaabeParamZerleegungErgeebnis.FüügeAn(
						AufgaabeParamAndere.KonstruktMausPfaad(
						SictAufgaabeParamMausPfaad.KonstruktMausKlikLinx(AuswaalReczInventorySictButton)));
				}
			}

			if (null != InventoryItemTransport)
			{
				var InventoryItemTransportKweleWindowInventory = InventoryItemTransport.KweleWindowInventory;
				var InventoryItemTransportMengeItem = InventoryItemTransport.MengeItem;
				var InventoryItemTransportZiilObjektTreeViewEntry = InventoryItemTransport.ZiilObjektTreeViewEntry;

				var InventoryItemTransportItem = ExtractFromOldAssembly.Bib3.Extension.FirstOrDefaultNullable(InventoryItemTransportMengeItem);

				if (null != InventoryItemTransportItem &&
					null != InventoryItemTransportZiilObjektTreeViewEntry)
				{
					if (1 < ExtractFromOldAssembly.Bib3.Extension.CountNullable(InventoryItemTransportMengeItem))
					{
						AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAus();
					}

					var InventoryItemTransportZiilObjektTreeViewEntryTopContLabel = InventoryItemTransportZiilObjektTreeViewEntry.TopContLabel;

					AufgaabeParamZerleegungErgeebnis.FüügeAn(
						SictAufgaabeParam.KonstruktAufgaabeParam(
						AufgaabeParamAndere.KonstruktMausPfaad(
						new SictAufgaabeParamMausPfaad(
							new GbsElement[]{
								InventoryItemTransportItem,
								InventoryItemTransportZiilObjektTreeViewEntryTopContLabel,
							},
							true))));

				}
			}

			if (true == AufgaabeParam.AktioonDroneLaunch)
			{
				if (null == WindowDroneViewGrupeDronesInBay)
				{
					AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAus();
				}
				else
				{
					AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.AufgaabeAktioonMenu(WindowDroneViewGrupeDronesInBay.FürMenuBeginFläce,
						new SictAnforderungMenuKaskaadeAstBedingung("launch", true)));
				}
			}

			if (true == AufgaabeParam.AktioonDroneEngage)
			{
				if (null == WindowDroneViewGrupeDronesInLocalSpace)
				{
					AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAus();
				}
				else
				{
					var ZiilObjektInputFookusGesezt = false;

					if (null == TargetZuBearbeite)
					{
						if (null == OverViewObjektZuBearbaite)
						{
							ZiilObjektInputFookusGesezt = true;
						}
						else
						{
							AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAus();

							//	!!!!	Zu ergänze
						}
					}
					else
					{
						AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.KonstruktTargetInputFookusSeze(TargetZuBearbeite));

						if (true == TargetZuBearbeite.InputFookusTransitioonLezteZiilWert)
						{
							ZiilObjektInputFookusGesezt = true;
						}
						else
						{
						}
					}

					if (ZiilObjektInputFookusGesezt)
					{
						AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.AufgaabeAktioonMenu(WindowDroneViewGrupeDronesInLocalSpace.FürMenuBeginFläce,
							new SictAnforderungMenuKaskaadeAstBedingung("engage", true)));
					}
					else
					{
						AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAus();
					}
				}
			}

			if (true == AufgaabeParam.AktioonDroneReturn)
			{
				if (null == WindowDroneViewGrupeDronesInLocalSpace)
				{
					AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAus();
				}
				else
				{
					var DronesInLocalSpaceAnzaal = ScnapscusWindowDroneView.DronesInLocalSpaceAnzaal;

					var GrupeDronesInLocalSpaceMengeDroneEntry = (null == WindowDroneViewGrupeDronesInLocalSpace) ? null : WindowDroneViewGrupeDronesInLocalSpace.MengeDroneEntry;

					var GrupeDronesInLocalSpaceMengeDroneEntryReturnNict =
						ExtractFromOldAssembly.Bib3.Extension.WhereNullable(GrupeDronesInLocalSpaceMengeDroneEntry,
						(KandidaatDroneEntry) => !(DroneEntryStatusSictEnum.Returning == KandidaatDroneEntry.StatusSictEnum))
						.ToArrayNullable();

					var VersuucAnforderungReturnFälig = true;

					if (GrupeDronesInLocalSpaceMengeDroneEntryReturnNict.IsNullOrEmpty() &&
						DronesInLocalSpaceAnzaal <= ExtractFromOldAssembly.Bib3.Extension.CountNullable(GrupeDronesInLocalSpaceMengeDroneEntry))
					{
						VersuucAnforderungReturnFälig = false;
					}

					if (VersuucAnforderungReturnFälig)
					{
						AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.AufgaabeAktioonMenu(WindowDroneViewGrupeDronesInLocalSpace.FürMenuBeginFläce,
							new SictAnforderungMenuKaskaadeAstBedingung("return.*bay", true)));
					}
				}
			}

			if (null != AktioonInOverviewTabZuAktiviire)
			{
				var WindowOverviewTabZuAktiviire =
					ExtractFromOldAssembly.Bib3.Extension.FirstOrDefaultNullable(WindowOverviewMengeTab,
					(Kandidaat) =>
					{
						if (null == Kandidaat)
						{
							return false;
						}

						var KandidaatLabel = Kandidaat.Label;

						if (null == KandidaatLabel)
						{
							return false;
						}

						return string.Equals(KandidaatLabel.Bescriftung, AktioonInOverviewTabZuAktiviire, StringComparison.InvariantCultureIgnoreCase);
					});

				if (null == WindowOverviewTabZuAktiviire)
				{
					AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAus();
				}
				else
				{
					if (ScnapscusWindowOverview.TabSelected == WindowOverviewTabZuAktiviire)
					{
					}
					else
					{
						AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.KonstruktMausPfaad(SictAufgaabeParamMausPfaad.KonstruktMausKlikLinx(WindowOverviewTabZuAktiviire)));
					}
				}
			}

			if (true == InOverviewTabFolgeViewportDurcGrid)
			{
				AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAus();

				if (null == WindowOverviewScroll)
				{
				}
				else
				{
					if (null == WindowOverviewScroll.ScrollHandleGrenzeFläce ||
						null == WindowOverviewScroll.ScrollHandleFläce)
					{
					}
					else
					{
						bool ScrolNaacOobe = false;
						bool ScrolNaacUnte = false;

						var OverviewPresetFolgeViewportAktuel = OverviewUndTarget.OverviewPresetFolgeViewportAktuel;

						if (null != OverviewPresetFolgeViewportAktuel)
						{
							if (!(OverviewPresetFolgeViewportAktuel.BeginScrollHandleFläceGrenzeOobnAntailAnGesamtMili < 1))
							{
								//	Folge wurde nit oobe Begone
								//	naac Oobe Scrole und noie Folge begine.
								ScrolNaacOobe = true;
							}
							else
							{
								if (!(1e+6 < OverviewPresetFolgeViewportAktuel.EndeObjektDistance))
								{
									//	EndeObjektDistance liigt noc im Grid, daher könte darunter noc Objekte angezaigt werde welce auc noc im Grid sind.
									//	naac Unte waiterscrole.

									ScrolNaacUnte = true;
								}
							}
						}

						if (ScrolNaacOobe)
						{
							AufgaabeParamZerleegungErgeebnis.FüügeAn(new AufgaabeParamScrollToTop(WindowOverviewScroll));
						}

						if (ScrolNaacUnte)
						{
							AufgaabeParamZerleegungErgeebnis.FüügeAn(new AufgaabeParamScrollDown(WindowOverviewScroll));
						}
					}
				}
			}

			if (true == AktioonInOverviewMaceSictbar &&
				null != OverViewObjektZuBearbaite)
			{
				if (null != WindowOverviewScroll)
				{
					if (null != WindowOverviewScroll.ScrollHandle)
					{
						//	Falls im Overview gescrollt werde kan sicersctele das Overview sorted naac Distance.
						AufgaabeParamZerleegungErgeebnis.FüügeAn(new AufgaabeParamScteleSicerOverviewSortNaacDistance());
					}
				}

				if (OverViewObjektZuBearbaite.SaitSictbarkaitLezteListeScritAnzaal <= 1)
				{
					//	Objekt war in lezte oder vorlezte Scnapscus sictbar.

					/*
					 * 2014.04.27	hööher Priorisiirte Ordnung:
					 * Objekte welce geraade erst unsictbar geworde sind sole naacrangig behandelt werde da diise eventuel in nääxte Scrit verscwinde
					 * (Overview Viewport Folge bescteet imer aus mindestens zwai Scnapscus).
					 * */
				}
				else
				{
					AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAus();

					{
						var PresetGeneralFolgeViewportVolLezte =
							OverviewUndTarget.ZuPresetBerecneOverviewPresetFolgeViewportFertigLezte(OverviewPresetDefaultTyp.General);

						var PresetGeneralFolgeViewportVolLezteHinraicendAktuel = false;

						if (null != PresetGeneralFolgeViewportVolLezte)
						{
							var PresetGeneralFolgeViewportVolLezteAlter = NuzerZaitMili - PresetGeneralFolgeViewportVolLezte.BeginZait;

							if (PresetGeneralFolgeViewportVolLezteAlter < 44444)
							{
								PresetGeneralFolgeViewportVolLezteHinraicendAktuel = true;
							}
						}

						var SictungLeztePresetUndZait =
							OverViewObjektZuBearbaite.DictZuOverviewPresetSictungLezteZait
							.OrderByDescendingNullable((PresetUndZait) => PresetUndZait.Value)
							.FirstOrDefaultNullable();

						var ZuVerwendendePresetDefault = Optimat.Glob.EnumParseNulbar<OverviewPresetDefaultTyp>(SictungLeztePresetUndZait.Key, true);

						if (!PresetGeneralFolgeViewportVolLezteHinraicendAktuel)
						{
							ZuVerwendendePresetDefault = OverviewPresetDefaultTyp.General;
						}

						if (ZuVerwendendePresetDefault.HasValue)
						{
							AufgaabeParamZerleegungErgeebnis.FüügeAn(new AufgaabeParamOverviewPresetLaadeNaacVorgeseheneTab(ZuVerwendendePresetDefault.Value));
						}
						else
						{
							AufgaabeParamZerleegungErgeebnis.FüügeAn(
								AufgaabeParamAndere.KonstruktNaacNuzerMeldungZuEveOnline(
								SictNaacNuzerMeldungZuEveOnline.ErrorGenerel(-1, new SictNaacNuzerMeldungZuEveOnlineCause(CauseText: "Overview Management"))));
						}

						AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.KonstruktInOverviewTabFolgeViewportDurcGrid());
					}
				}
			}
		}
	}
}
