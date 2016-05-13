using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Optimat.EveOnline.Anwendung.AuswertGbs;
//using Optimat.EveOnline.AuswertGbs;
using Optimat.EveOnline.VonSensor;
using Optimat.ScpezEveOnln;


namespace Optimat.EveOnline.Anwendung
{
	public partial class AufgaabeParamAndere
	{
		static public AufgaabeParamAndere KonstruktNaacNuzerVorsclaagWirkung(
			SictVorsclaagNaacProcessWirkung VorsclaagWirkung,
			string ZwekKomponenteZuusaz)
		{
			return KonstruktNaacNuzerVorsclaagWirkung(VorsclaagWirkung, new string[] { ZwekKomponenteZuusaz });
		}

		static public AufgaabeParamAndere KonstruktMausPfaad(
			SictAufgaabeParamMausPfaad MausPfaad)
		{
			return new AufgaabeParamAndere(MausPfaad: MausPfaad);
		}

		static public AufgaabeParamAndere KonstruktNaacNuzerVorsclaagWirkung(
			SictVorsclaagNaacProcessWirkung VorsclaagWirkung,
			string[] ZwekListeKomponenteZuusaz = null)
		{
			var Aufgaabe = new AufgaabeParamAndere(ZwekListeKomponenteZuusaz: ZwekListeKomponenteZuusaz);

			Aufgaabe.NaacNuzerVorsclaagWirkung = VorsclaagWirkung;

			return Aufgaabe;
		}

		static public AufgaabeParamAndere KonstruktColumnHeaderSort(
			VonSensor.ListColumnHeader	ColumnHeaderSort)
		{
			return new AufgaabeParamAndere(ColumnHeaderSort: ColumnHeaderSort);
		}

		static public AufgaabeParamAndere KonstruktListEntryExpand(
			GbsListGroupedEntryZuusctand ListEntryExpand)
		{
			return new AufgaabeParamAndere(ListEntryExpand: ListEntryExpand);
		}

		static public AufgaabeParamAndere KonstruktListEntryMaceSictbar(
			GbsListGroupedEntryZuusctand ListEntryMaceSictbar)
		{
			return new AufgaabeParamAndere(ListEntryMaceSictbar: ListEntryMaceSictbar);
		}

		static public AufgaabeParamAndere KonstruktListEntryCollapse(
			GbsListGroupedEntryZuusctand ListEntryCollapse)
		{
			return new AufgaabeParamAndere(ListEntryCollapse: ListEntryCollapse);
		}

		static public AufgaabeParamAndere KonstruktListEntryToggleExpandCollapse(
			GbsListGroupedEntryZuusctand ListEntryToggleExpandCollapse)
		{
			return new AufgaabeParamAndere(ListEntryToggleExpandCollapse: ListEntryToggleExpandCollapse);
		}

		static public AufgaabeParamAndere KonstruktTargetInputFookusSeze(SictTargetZuusctand TargetInputFookusSeze)
		{
			return new AufgaabeParamAndere(TargetInputFookusSeze: TargetInputFookusSeze);
		}

		static public AufgaabeParamAndere KonstruktModuleScalteAin(SictShipUiModuleReprZuusctand ModuleScalteAin)
		{
			return new AufgaabeParamAndere(ModuleScalteAin: ModuleScalteAin);
		}

		static public AufgaabeParamAndere KonstruktModuleScalteAus(SictShipUiModuleReprZuusctand ModuleScalteAus)
		{
			return new AufgaabeParamAndere(ModuleScalteAus: ModuleScalteAus);
		}

		static public AufgaabeParamAndere KonstruktModuleScalteUm(SictShipUiModuleReprZuusctand ModuleScalteUm)
		{
			return new AufgaabeParamAndere(ModuleScalteUm: ModuleScalteUm);
		}

		static public AufgaabeParamAndere KonstruktModuleMesungModuleButtonHint(SictShipUiModuleReprZuusctand ModuleMesungModuleButtonHint)
		{
			return new AufgaabeParamAndere(ModuleMesungModuleButtonHint: ModuleMesungModuleButtonHint);
		}

		static public AufgaabeParamAndere KonstruktMenuEntferne()
		{
			return new AufgaabeParamAndere(AktioonMenuEntferne: true);
		}

		static public AufgaabeParamAndere KonstruktGbsAstOklusioonVermaidung(
			SictAufgaabeParamGbsAstOklusioonVermaidung GbsAstOklusioonVermaidung,
			string[] ZwekListeKomponenteZuusaz = null)
		{
			return new AufgaabeParamAndere(
				GbsAstOklusioonVermaidung: GbsAstOklusioonVermaidung,
				ZwekListeKomponenteZuusaz: ZwekListeKomponenteZuusaz);
		}

		static public AufgaabeParamAndere KonstruktNeocomMenuEntferne()
		{
			return new AufgaabeParamAndere(AktioonNeocomMenuEntferne: true);
		}

		static public AufgaabeParamAndere KonstruktWindowMinimize(
			SictGbsWindowZuusctand WindowMinimize)
		{
			return new AufgaabeParamAndere(WindowMinimize: WindowMinimize);
		}

		static public AufgaabeParamAndere KonstruktWindowHooleNaacVorne(
			SictGbsWindowZuusctand WindowHooleNaacVorne)
		{
			return new AufgaabeParamAndere(WindowHooleNaacVorne: WindowHooleNaacVorne);
		}

		static public AufgaabeParamAndere KonstruktMenuEntry(MenuEntry MenuEntry)
		{
			return new AufgaabeParamAndere(MenuEntry: MenuEntry);
		}

		static public AufgaabeParamAndere KonstruktUnDock()
		{
			return new AufgaabeParamAndere(AktioonUnDock: true);
		}

		static public AufgaabeParamAndere KonstruktLobbyAgentEntryStartConversation(
			VonSensor.LobbyAgentEntry LobbyAgentEntryStartConversation)
		{
			return new AufgaabeParamAndere(LobbyAgentEntryStartConversation: LobbyAgentEntryStartConversation);
		}

		static public AufgaabeParamAndere KonstruktInfoPanelEnable(InfoPanelTypSictEnum InfoPanelEnable)
		{
			return new AufgaabeParamAndere(InfoPanelEnable: InfoPanelEnable);
		}

		static public AufgaabeParamAndere KonstruktInfoPanelExpand(InfoPanelTypSictEnum InfoPanelExpand)
		{
			return new AufgaabeParamAndere(InfoPanelExpand: InfoPanelExpand);
		}

		static public AufgaabeParamAndere KonstruktMissionAccept(SictMissionZuusctand MissionAccept)
		{
			return new AufgaabeParamAndere(MissionAccept: MissionAccept);
		}

		static public AufgaabeParamAndere KonstruktMissionDecline(SictMissionZuusctand MissionDecline)
		{
			return new AufgaabeParamAndere(MissionDecline: MissionDecline);
		}

		static public AufgaabeParamAndere KonstruktMenuBegin(
			GbsElement MenuWurzelGbsObjekt,
			SictAnforderungMenuKaskaadeAstBedingung[] MenuListeAstBedingung)
		{
			return new AufgaabeParamAndere(
				MenuWurzelGbsObjekt: MenuWurzelGbsObjekt,
				MenuListeAstBedingung: MenuListeAstBedingung,
				AktioonMenuBegin: true);
		}

		static public AufgaabeParamAndere KonstruktOverviewTabAktiviire(
			string OverviewTabZuAktiviire,
			string ZwekKomponente)
		{
			return KonstruktOverviewTabAktiviire(OverviewTabZuAktiviire, new string[] { ZwekKomponente });
		}

		static public AufgaabeParamAndere KonstruktOverviewTabAktiviire(
			string OverviewTabZuAktiviire,
			string[] ZwekListeKomponente = null)
		{
			if (null == OverviewTabZuAktiviire)
			{
				return null;
			}

			var Aufgaabe = new AufgaabeParamAndere(AktioonInOverviewTabZuAktiviire: OverviewTabZuAktiviire, ZwekListeKomponenteZuusaz: ZwekListeKomponente);

			return Aufgaabe;
		}

		static public AufgaabeParamAndere AufgaabeDistanceAinzusctele(
			SictTargetZuusctand Target,
			Int64? DistanceScrankeMin,
			Int64? DistanceScrankeMax)
		{
			if (null == Target)
			{
				return null;
			}

			var Aufgaabe = new AufgaabeParamAndere(TargetZuBearbeite: Target);

			Aufgaabe.DistanzAinzuscteleScrankeMin = DistanceScrankeMin;
			Aufgaabe.DistanzAinzuscteleScrankeMax = DistanceScrankeMax;

			return Aufgaabe;
		}

		static public AufgaabeParamAndere AufgaabeDistanceAinzusctele(
			SictOverViewObjektZuusctand InRaumObjekt,
			Int64? DistanceScrankeMin,
			Int64? DistanceScrankeMax)
		{
			if (null == InRaumObjekt)
			{
				return null;
			}

			return new AufgaabeParamAndere(OverViewObjektZuBearbeite:	InRaumObjekt, DistanzAinzuscteleScrankeMin: DistanceScrankeMin, DistanzAinzuscteleScrankeMax: DistanceScrankeMax);
		}

		/*
		 * 2015.01.06
		 * 
		static public AufgaabeParamAndere AufgaabeWirkungDestrukt(SictOverViewObjektZuusctand InRaumObjekt)
		{
			if (null == InRaumObjekt)
			{
				return null;
			}

			return new AufgaabeParamAndere(InRaumObjekt, null, null, null, null, null, null, null, null, null, true);
		}

		static public AufgaabeParamAndere AufgaabeWirkungDestrukt(SictTargetZuusctand TargetZuBearbaite)
		{
			if (null == TargetZuBearbaite)
			{
				return null;
			}

			return new AufgaabeParamAndere(null, TargetZuBearbaite, null, null, null, null, null, null, null, null, true);
		}
		 * */

		static public AufgaabeParamAndere AufgaabeAktioonCargoDurcsuuce(SictOverViewObjektZuusctand InRaumObjekt)
		{
			if (null == InRaumObjekt)
			{
				return null;
			}

			return new AufgaabeParamAndere(OverViewObjektZuBearbeite:	InRaumObjekt, AktioonCargoDurcsuuce: true);
		}

		static public AufgaabeParamAndere AufgaabeAktioonInRaumObjektActivate(
			SictOverViewObjektZuusctand InRaumObjekt,
			string ZwekKomponente)
		{
			return AufgaabeAktioonInRaumObjektActivate(InRaumObjekt, new string[] { ZwekKomponente });
		}

		static public AufgaabeParamAndere AufgaabeAktioonInRaumObjektActivate(
			SictOverViewObjektZuusctand InRaumObjekt,
			string[] ZwekListeKomponente = null)
		{
			if (null == InRaumObjekt)
			{
				return null;
			}

			var Aufgaabe = new AufgaabeParamAndere(OverViewObjektZuBearbeite:	InRaumObjekt, AktioonInRaumObjektActivate: true, ZwekListeKomponenteZuusaz: ZwekListeKomponente);

			return Aufgaabe;
		}

		static public AufgaabeParamAndere AufgaabeInOverViewMaceSictbar(
			SictOverViewObjektZuusctand InRaumObjekt,
			string ZwekKomponente)
		{
			return AufgaabeInOverViewMaceSictbar(InRaumObjekt, new string[] { ZwekKomponente });
		}

		static public AufgaabeParamAndere AufgaabeInOverViewMaceSictbar(
			SictOverViewObjektZuusctand InRaumObjekt,
			string[] ZwekListeKomponente = null)
		{
			if (null == InRaumObjekt)
			{
				return null;
			}

			var Aufgaabe = new AufgaabeParamAndere(OverViewObjektZuBearbeite:	InRaumObjekt, AktioonInOverviewMaceSictbar: true, ZwekListeKomponenteZuusaz: ZwekListeKomponente);

			return Aufgaabe;
		}

		static public AufgaabeParamAndere AufgaabeAktioonMenu(
			GbsElement MenuWurzelGbsObjekt,
			string MenuAstFrühesteAstRegexPattern,
			bool? AuswaalEntryVerursactMenuSclus = null,
			string[] ZwekListeKomponente = null)
		{
			var MenuAstFrühesteBedingung = new SictAnforderungMenuKaskaadeAstBedingung(MenuAstFrühesteAstRegexPattern, AuswaalEntryVerursactMenuSclus);

			return AufgaabeAktioonMenu(
				MenuWurzelGbsObjekt,
				MenuAstFrühesteBedingung,
				ZwekListeKomponente);
		}

		static public AufgaabeParamAndere AufgaabeAktioonMenu(
			GbsElement MenuWurzelGbsObjekt,
			SictAnforderungMenuKaskaadeAstBedingung MenuAstBedingung,
			string[] ZwekListeKomponente = null)
		{
			return AufgaabeAktioonMenu(
				MenuWurzelGbsObjekt,
				new SictAnforderungMenuKaskaadeAstBedingung[] { MenuAstBedingung },
				ZwekListeKomponente);
		}

		static public AufgaabeParamAndere AufgaabeAktioonMenu(
			GbsElement MenuWurzelGbsObjekt,
			SictAnforderungMenuKaskaadeAstBedingung[] MenuListeAstBedingung,
			string[] ZwekListeKomponente = null)
		{
			if (null == MenuWurzelGbsObjekt)
			{
				return null;
			}

			var Aufgaabe = new AufgaabeParamAndere(
				MenuWurzelGbsObjekt: MenuWurzelGbsObjekt,
				MenuListeAstBedingung: MenuListeAstBedingung,
				ZwekListeKomponenteZuusaz: ZwekListeKomponente);

			return Aufgaabe;
		}

		static public AufgaabeParamAndere AufgaabeAktioonMenu(
			SictOverViewObjektZuusctand InRaumObjekt,
			SictAnforderungMenuKaskaadeAstBedingung MenuAstBedingung,
			string[] ZwekListeKomponente = null)
		{
			return AufgaabeAktioonMenu(
				InRaumObjekt,
				new SictAnforderungMenuKaskaadeAstBedingung[] { MenuAstBedingung },
				ZwekListeKomponente);
		}

		static public AufgaabeParamAndere AufgaabeAktioonMenu(
			SictOverViewObjektZuusctand InRaumObjektOverview,
			SictAnforderungMenuKaskaadeAstBedingung[] MenuListeAstBedingung,
			string[] ZwekListeKomponente = null)
		{
			if (null == InRaumObjektOverview)
			{
				return null;
			}

			var Aufgaabe = new AufgaabeParamAndere(OverViewObjektZuBearbeite:	InRaumObjektOverview, MenuListeAstBedingung: MenuListeAstBedingung, ZwekListeKomponenteZuusaz: ZwekListeKomponente);

			return Aufgaabe;
		}

		static public AufgaabeParamAndere AufgaabeAktioonMenu(
			SictTargetZuusctand InRaumObjektReprTarget,
			SictAnforderungMenuKaskaadeAstBedingung[] MenuListeAstBedingung,
			string[] ZwekListeKomponente = null)
		{
			if (null == InRaumObjektReprTarget)
			{
				return null;
			}

			var Aufgaabe = new AufgaabeParamAndere(
				TargetZuBearbeite:InRaumObjektReprTarget,
				MenuListeAstBedingung:	MenuListeAstBedingung,
				ZwekListeKomponenteZuusaz:	ZwekListeKomponente);

			return Aufgaabe;
		}

		static public AufgaabeParamAndere AufgaabeAktioonDroneLaunch()
		{
			return new AufgaabeParamAndere(AktioonDroneLaunch: true);
		}

		static public AufgaabeParamAndere AufgaabeAktioonDroneEngage(
			SictTargetZuusctand TargetZuBearbeite)
		{
			var Aufgaabe = new AufgaabeParamAndere();

			Aufgaabe.TargetZuBearbaite = TargetZuBearbeite;

			Aufgaabe.AktioonDroneEngage = true;

			return Aufgaabe;
		}

		static public AufgaabeParamAndere AufgaabeMissionObjectiveMese(
			SictMissionZuusctand MissionObjectiveMese)
		{
			var Aufgaabe = new AufgaabeParamAndere(MissionObjectiveMese: MissionObjectiveMese);

			return Aufgaabe;
		}

		static public AufgaabeParamAndere KonstruktMissionStartConversation(
			SictMissionZuusctand MissionStartConversation)
		{
			var Aufgaabe = new AufgaabeParamAndere(MissionStartConversation: MissionStartConversation);

			return Aufgaabe;
		}

		static public AufgaabeParamAndere KonstruktMissionButtonUtilmenuObjectiveZuMese(
			VonSensor.InfoPanelMissionsMission MissionButtonUtilmenuObjectiveZuMese)
		{
			var Aufgaabe = new AufgaabeParamAndere(MissionButtonUtilmenuObjectiveZuMese: MissionButtonUtilmenuObjectiveZuMese);

			return Aufgaabe;
		}

		/// <summary>
		/// Noc zu erseze durc MissionAccept
		/// </summary>
		/// <param name="WindowAgentDialogueMissionAcceptOderRequest"></param>
		/// <returns></returns>
		static public AufgaabeParamAndere KonstruktWindowAgentDialogueMissionAcceptOderRequest(
			VonSensor.WindowAgentDialogue WindowAgentDialogueMissionAcceptOderRequest)
		{
			var Aufgaabe = new AufgaabeParamAndere(WindowAgentDialogueMissionAcceptOderRequest: WindowAgentDialogueMissionAcceptOderRequest);

			return Aufgaabe;
		}

		/// <summary>
		/// Noc zu erseze durc MissionDecline
		/// </summary>
		/// <param name="WindowAgentDialogueMissionDecline"></param>
		/// <returns></returns>
		static public AufgaabeParamAndere KonstruktWindowAgentDialogueMissionDecline(
			VonSensor.WindowAgentDialogue WindowAgentDialogueMissionDecline)
		{
			var Aufgaabe = new AufgaabeParamAndere(WindowAgentDialogueMissionDecline: WindowAgentDialogueMissionDecline);

			return Aufgaabe;
		}

		static public AufgaabeParamAndere KonstruktFittingZuApliziire(
			SictOptimatParamFitting FittingZuApliziire)
		{
			var Aufgaabe = new AufgaabeParamAndere(FittingZuApliziire: FittingZuApliziire);

			return Aufgaabe;
		}

		static public AufgaabeParamAndere KonstruktNaacNuzerMeldungZuEveOnline(
			SictNaacNuzerMeldungZuEveOnline NaacNuzerMeldungZuEveOnline)
		{
			return new AufgaabeParamAndere(NaacNuzerMeldungZuEveOnline: NaacNuzerMeldungZuEveOnline);
		}

		static public AufgaabeParamAndere KonstruktGridVerlase()
		{
			return new AufgaabeParamAndere(GridVerlase: true);
		}

		/*
		static public AufgaabeParamAndere KonstruktGbsAstVerberge(
			GbsElement GbsAstVerberge)
		{
			return new AufgaabeParamAndere(GbsAstVerberge: GbsAstVerberge);
		}
		*/

		static public AufgaabeParamAndere KonstruktInRaumManööver(
			SictTargetZuusctand InRaumObjektReprTarget,
			SictOverViewObjektZuusctand InRaumObjektReprOverview,
			SictZuInRaumObjektManööverTypEnum? ManööverAuszufüüreTyp,
			Int64?	DistanceScrankeMin	= null,
			Int64?	DistanceScrankeMax	= null)
		{
			return new AufgaabeParamAndere(
				TargetZuBearbeite:InRaumObjektReprTarget,
				OverViewObjektZuBearbeite: InRaumObjektReprOverview,
				ManööverAuszufüüreTyp: ManööverAuszufüüreTyp,
				DistanzAinzuscteleScrankeMin:	DistanceScrankeMin,
				DistanzAinzuscteleScrankeMax:	DistanceScrankeMax);
		}

		static public AufgaabeParamAndere KonstruktAfterburnerScalteAin()
		{
			return new AufgaabeParamAndere(AfterburnerScalteAin: true);
		}

		/*
		 * 2015.00.04
		 * 
		static public AufgaabeParamAndere KonstruktMengeOverviewObjektGrupeMesungZuErsctele(
			SictOverviewObjektGrupeEnum[] MengeOverviewObjektGrupeMesungZuErsctele)
		{
			return new AufgaabeParamAndere(MengeOverviewObjektGrupeMesungZuErsctele: MengeOverviewObjektGrupeMesungZuErsctele);
		}
		 * */

		static public AufgaabeParamAndere KonstruktInOverviewTabFolgeViewportDurcGrid()
		{
			return new AufgaabeParamAndere(InOverviewTabFolgeViewportDurcGrid: true);
		}

		static public AufgaabeParamAndere KonstruktTargetUnLock(SictTargetZuusctand	TargetUnLock)
		{
			return new AufgaabeParamAndere(TargetUnLock: TargetUnLock);
		}

		static public AufgaabeParamAndere KonstruktDronesLaunch()
		{
			return new AufgaabeParamAndere(AktioonDroneLaunch: true);
		}

		static public AufgaabeParamAndere KonstruktDronesReturn()
		{
			return new AufgaabeParamAndere(AktioonDroneReturn: true);
		}

		static public AufgaabeParamAndere KonstruktDronesEngage(
			SictTargetZuusctand	Target)
		{
			return new AufgaabeParamAndere(
				TargetZuBearbeite: Target,
				AktioonDroneEngage: true);
		}

		static public AufgaabeParamAndere KonstruktInventoryItemTransport(
			SictInventoryItemTransport InventoryItemTransport)
		{
			return new AufgaabeParamAndere(
				InventoryItemTransport: InventoryItemTransport);
		}

		static public AufgaabeParamAndere KonstruktInventorySezeSictTypAufList(
			WindowInventoryPrimary InventorySezeSictTypAufList)
		{
			return new AufgaabeParamAndere(
				InventorySezeSictTypAufList: InventorySezeSictTypAufList);
		}

		static public AufgaabeParamAndere KonstruktManööverUnterbreceNict(
			SictVerlaufBeginUndEndeRef<ShipUiIndicationAuswert>	ManööverUnterbreceNict)
		{
			return new AufgaabeParamAndere(
				ManööverUnterbreceNict: ManööverUnterbreceNict);
		}

		static public AufgaabeParamAndere KonstruktVorrangVorManööverUnterbreceNict()
		{
			return new AufgaabeParamAndere(
				VorrangVorManööverUnterbreceNict: true);
		}

	}
}
