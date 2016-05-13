using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Optimat.EveOnline.Anwendung.AuswertGbs;
//using Optimat.EveOnline.AuswertGbs;
using Optimat.EveOnline.VonSensor;
using Optimat.ScpezEveOnln;


namespace Optimat.EveOnline.Anwendung
{
	[JsonObject(MemberSerialization.OptIn)]
	public partial class AufgaabeParamAndere : SictAufgaabeParam
	{
		[JsonProperty]
		public SictVorsclaagNaacProcessWirkung NaacNuzerVorsclaagWirkung
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictNaacNuzerMeldungZuEveOnline NaacNuzerMeldungZuEveOnline
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictOverViewObjektZuusctand OverViewObjektZuBearbaite
		{
			private set;
			get;
		}

		[JsonProperty]
		public GbsElement MenuWurzelGbsObjekt
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictTargetZuusctand TargetZuBearbaite
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? AktioonInOverviewMaceSictbar
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? AktioonTargetActivate
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictAnforderungMenuKaskaadeAstBedingung[] MenuListeAstBedingung
		{
			private set;
			get;
		}

		[JsonProperty]
		public VonSensor.MenuEntry MenuEntry
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? AktioonNeocomMenuEntferne
		{
			private set;
			get;
		}

		/// <summary>
		/// es sol sicergesctelt werde das referenziirte GBS Ast nict sowait Okludiirt das Saitenlänge des gröösten Kwadraat in nict Okludiirte Tailfläce klainer als Value.
		/// Fals solc aine Oklusioon erkant werd, werd di Aufgaabe zerleegt um Oklusioon zu beende (z.B. durc minimiire von Window oder Scliise von Menu)
		/// </summary>
		[JsonProperty]
		public SictAufgaabeParamGbsAstOklusioonVermaidung GbsAstOklusioonVermaidung
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? DistanzAinzuscteleScrankeMin
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? DistanzAinzuscteleScrankeMax
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictZuInRaumObjektManööverTypEnum? ManööverAuszufüüreTyp
		{
			private set;
			get;
		}

		[JsonProperty]
		readonly public SictVerlaufBeginUndEndeRef<ShipUiIndicationAuswert> ManööverUnterbreceNict;

		[JsonProperty]
		readonly public bool? VorrangVorManööverUnterbreceNict;

		/// <summary>
		/// z.B. für Acc-Gate
		/// </summary>
		[JsonProperty]
		public bool? AktioonInRaumObjektActivate
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? AktioonCargoDurcsuuce
		{
			private set;
			get;
		}

		[JsonProperty]
		public string AktioonInOverviewTabZuAktiviire
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? AktioonUnDock
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? AktioonWirkungTraktor
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? AktioonDroneLaunch
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? AktioonDroneReturn
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictAufgaabeParamMausPfaad MausPfaad
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? AktioonDroneEngage
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictTargetZuusctand TargetInputFookusSeze
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictTargetZuusctand TargetUnLock
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictShipUiModuleReprZuusctand ModuleScalteAin
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictShipUiModuleReprZuusctand ModuleScalteAus
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictShipUiModuleReprZuusctand ModuleScalteUm
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictShipUiModuleReprZuusctand ModuleMesungModuleButtonHint
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? GridVerlase
		{
			private set;
			get;
		}

		/*
		2015.03.20

		[JsonProperty]
		public GbsElement GbsAstVerberge
		{
			private set;
			get;
		}
		*/

		[JsonProperty]
		public bool? AktioonMenuBegin
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? AktioonMenuEntferne
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictMissionZuusctand MissionObjectiveMese
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictMissionZuusctand MissionStartConversation
		{
			private set;
			get;
		}

		[JsonProperty]
		public VonSensor.LobbyAgentEntry LobbyAgentEntryStartConversation
		{
			private set;
			get;
		}

		[JsonProperty]
		public VonSensor.InfoPanelMissionsMission MissionButtonUtilmenuObjectiveZuMese
		{
			private set;
			get;
		}

		[JsonProperty]
		public VonSensor.WindowAgentDialogue WindowAgentDialogueMissionAcceptOderRequest
		{
			private set;
			get;
		}

		[JsonProperty]
		public VonSensor.WindowAgentDialogue WindowAgentDialogueMissionDecline
		{
			private set;
			get;
		}

		[JsonProperty]
		public InfoPanelTypSictEnum? InfoPanelEnable
		{
			private set;
			get;
		}

		[JsonProperty]
		public InfoPanelTypSictEnum? InfoPanelExpand
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictGbsWindowZuusctand WindowMinimize
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictGbsWindowZuusctand WindowHooleNaacVorne
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictMissionZuusctand MissionAccept
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictMissionZuusctand MissionDecline
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictOptimatParamFitting FittingZuApliziire
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? AfterburnerScalteAin
		{
			private set;
			get;
		}

		/// <summary>
		/// In aktiive Overview Tab scrole so das zusamehängende Folge von Viewport gemese werd welce bai Distance=0 begint und bis Ende des Grid raict.
		/// </summary>
		[JsonProperty]
		readonly public bool? InOverviewTabFolgeViewportDurcGrid;

		[JsonProperty]
		readonly public SictInventoryItemTransport InventoryItemTransport;

		[JsonProperty]
		readonly public VonSensor.WindowInventoryPrimary InventorySezeSictTypAufList;

		[JsonProperty]
		readonly public VonSensor.ListColumnHeader ColumnHeaderSort;

		[JsonProperty]
		readonly public GbsListGroupedEntryZuusctand ListEntryMaceSictbar;

		[JsonProperty]
		readonly public GbsListGroupedEntryZuusctand ListEntryExpand;

		[JsonProperty]
		readonly public GbsListGroupedEntryZuusctand ListEntryCollapse;

		[JsonProperty]
		readonly public GbsListGroupedEntryZuusctand ListEntryToggleExpandCollapse;


		public AufgaabeParamAndere()
		{
		}

		public AufgaabeParamAndere(
			SictOverViewObjektZuusctand OverViewObjektZuBearbeite = null,
			SictTargetZuusctand TargetZuBearbeite = null,
			bool? AktioonInOverviewMaceSictbar = null,
			string AktioonInOverviewTabZuAktiviire = null,
			SictAnforderungMenuKaskaadeAstBedingung[] MenuListeAstBedingung = null,
			Int64? DistanzAinzuscteleScrankeMin = null,
			Int64? DistanzAinzuscteleScrankeMax = null,
			bool? AktioonInRaumObjektActivate = null,
			bool? AktioonCargoDurcsuuce = null,
			bool? AktioonLock = null,
			bool? AktioonWirkungTraktor = null,
			bool? AktioonDroneLaunch = null,
			bool? AktioonDroneEngage = null,
			bool? AktioonDroneReturn = null,
			bool? AktioonUnDock = null,
			SictAufgaabeParam AufgaabeParam = null,
			SictAufgaabeParamMausPfaad MausPfaad = null,
			SictShipUiModuleReprZuusctand ModuleScalteAin = null,
			SictShipUiModuleReprZuusctand ModuleScalteAus = null,
			SictShipUiModuleReprZuusctand ModuleScalteUm = null,
			SictShipUiModuleReprZuusctand ModuleMesungModuleButtonHint = null,
			bool? AfterburnerScalteAin = null,
			bool? AktioonMenuBegin = null,
			bool? AktioonMenuEntferne = null,
			bool? AktioonNeocomMenuEntferne = null,
			GbsElement MenuWurzelGbsObjekt = null,
			VonSensor.MenuEntry MenuEntry = null,
			InfoPanelTypSictEnum? InfoPanelEnable = null,
			InfoPanelTypSictEnum? InfoPanelExpand = null,
			SictAufgaabeParamGbsAstOklusioonVermaidung GbsAstOklusioonVermaidung = null,
			SictGbsWindowZuusctand WindowMinimize = null,
			SictGbsWindowZuusctand WindowHooleNaacVorne = null,
			//	GbsElement GbsAstVerberge = null,
			SictZuInRaumObjektManööverTypEnum? ManööverAuszufüüreTyp = null,
			SictVerlaufBeginUndEndeRef<ShipUiIndicationAuswert> ManööverUnterbreceNict = null,
			bool? VorrangVorManööverUnterbreceNict = null,
			bool? GridVerlase = null,
			VonSensor.ListColumnHeader ColumnHeaderSort = null,
			GbsListGroupedEntryZuusctand ListEntryMaceSictbar = null,
			GbsListGroupedEntryZuusctand ListEntryExpand = null,
			GbsListGroupedEntryZuusctand ListEntryCollapse = null,
			GbsListGroupedEntryZuusctand ListEntryToggleExpandCollapse = null,
			SictTargetZuusctand TargetUnLock = null,
			SictTargetZuusctand TargetInputFookusSeze = null,
			VonSensor.LobbyAgentEntry LobbyAgentEntryStartConversation = null,
			SictMissionZuusctand MissionObjectiveMese = null,
			SictMissionZuusctand MissionStartConversation = null,
			VonSensor.InfoPanelMissionsMission MissionButtonUtilmenuObjectiveZuMese = null,
			VonSensor.WindowAgentDialogue WindowAgentDialogueMissionAcceptOderRequest = null,
			VonSensor.WindowAgentDialogue WindowAgentDialogueMissionDecline = null,
			SictMissionZuusctand MissionAccept = null,
			SictMissionZuusctand MissionDecline = null,
			SictOptimatParamFitting FittingZuApliziire = null,
			SictNaacNuzerMeldungZuEveOnline NaacNuzerMeldungZuEveOnline = null,
			string[] ZwekListeKomponenteZuusaz = null,
			bool? InOverviewTabFolgeViewportDurcGrid = null,
			SictInventoryItemTransport InventoryItemTransport = null,
			VonSensor.WindowInventoryPrimary InventorySezeSictTypAufList = null)
			:
			base(ZwekListeKomponenteZuusaz)
		{
			this.NaacNuzerMeldungZuEveOnline = NaacNuzerMeldungZuEveOnline;

			this.OverViewObjektZuBearbaite = OverViewObjektZuBearbeite;
			this.TargetZuBearbaite = TargetZuBearbeite;

			this.AktioonInOverviewMaceSictbar = AktioonInOverviewMaceSictbar;

			this.AktioonInOverviewTabZuAktiviire = AktioonInOverviewTabZuAktiviire;
			this.InOverviewTabFolgeViewportDurcGrid = InOverviewTabFolgeViewportDurcGrid;

			this.MenuListeAstBedingung = MenuListeAstBedingung;

			this.AktioonUnDock = AktioonUnDock;

			this.AktioonInRaumObjektActivate = AktioonInRaumObjektActivate;
			this.AktioonCargoDurcsuuce = AktioonCargoDurcsuuce;

			this.AktioonWirkungTraktor = AktioonWirkungTraktor;

			this.AktioonDroneLaunch = AktioonDroneLaunch;
			this.AktioonDroneEngage = AktioonDroneEngage;
			this.AktioonDroneReturn = AktioonDroneReturn;

			this.MausPfaad = MausPfaad;

			this.AktioonMenuBegin = AktioonMenuBegin;
			this.AktioonMenuEntferne = AktioonMenuEntferne;
			this.MenuWurzelGbsObjekt = MenuWurzelGbsObjekt;
			this.MenuEntry = MenuEntry;

			this.AktioonNeocomMenuEntferne = AktioonNeocomMenuEntferne;

			this.InfoPanelEnable = InfoPanelEnable;
			this.InfoPanelExpand = InfoPanelExpand;

			this.WindowMinimize = WindowMinimize;
			this.WindowHooleNaacVorne = WindowHooleNaacVorne;

			//	this.GbsAstVerberge = GbsAstVerberge;

			this.DistanzAinzuscteleScrankeMin = DistanzAinzuscteleScrankeMin;
			this.DistanzAinzuscteleScrankeMax = DistanzAinzuscteleScrankeMax;
			this.ManööverAuszufüüreTyp = ManööverAuszufüüreTyp;
			this.ManööverUnterbreceNict = ManööverUnterbreceNict;
			this.VorrangVorManööverUnterbreceNict = VorrangVorManööverUnterbreceNict;

			this.GbsAstOklusioonVermaidung = GbsAstOklusioonVermaidung;

			this.ModuleScalteAin = ModuleScalteAin;
			this.ModuleScalteAus = ModuleScalteAus;
			this.ModuleScalteUm = ModuleScalteUm;
			this.ModuleMesungModuleButtonHint = ModuleMesungModuleButtonHint;

			this.AfterburnerScalteAin = AfterburnerScalteAin;

			this.ColumnHeaderSort = ColumnHeaderSort;
			this.ListEntryMaceSictbar = ListEntryMaceSictbar;
			this.ListEntryExpand = ListEntryExpand;
			this.ListEntryCollapse = ListEntryCollapse;
			this.ListEntryToggleExpandCollapse = ListEntryToggleExpandCollapse;

			this.GridVerlase = GridVerlase;

			this.TargetUnLock = TargetUnLock;
			this.TargetInputFookusSeze = TargetInputFookusSeze;

			this.LobbyAgentEntryStartConversation = LobbyAgentEntryStartConversation;

			this.MissionObjectiveMese = MissionObjectiveMese;
			this.MissionStartConversation = MissionStartConversation;
			this.MissionButtonUtilmenuObjectiveZuMese = MissionButtonUtilmenuObjectiveZuMese;
			this.WindowAgentDialogueMissionAcceptOderRequest = WindowAgentDialogueMissionAcceptOderRequest;
			this.WindowAgentDialogueMissionDecline = WindowAgentDialogueMissionDecline;
			this.MissionAccept = MissionAccept;
			this.MissionDecline = MissionDecline;

			this.FittingZuApliziire = FittingZuApliziire;

			this.InventoryItemTransport = InventoryItemTransport;

			this.InventorySezeSictTypAufList = InventorySezeSictTypAufList;
		}

		override	public int WartezaitBisEntscaidungErfolgScritAnzaalMax()
		{
			if (AktioonMenuBegin ?? false)
			{
				return 2;
			}

			if (null	!= MenuEntry)
			{
				return 2;
			}

			return 0;
		}

		override public SictAufgaabeParamZerleegungErgeebnis Zerleege(
			ISictAutomatZuusctand AutomaatZuusctand,
			SictAufgaabeKombiZuusctand KombiZuusctand)
		{
			SictAufgaabeParamZerleegungErgeebnis AufgaabeParamZerleegungErgeebnis;
			bool? Erfolg;
			bool? ErfolgAnhaltend;
			Int64? ReegelungDistanceScpiilraumRest;

			var AutomaatZuusctandScpez = AutomaatZuusctand as SictAutomatZuusctand;

			if (null == AutomaatZuusctandScpez)
			{
				throw new NotImplementedException();
			}

			AutomaatZuusctandScpez.AufgaabeBerecneAktueleTailaufgaabe(
				this,
				out	AufgaabeParamZerleegungErgeebnis,
				out	Erfolg,
				out	ErfolgAnhaltend,
				out	ReegelungDistanceScpiilraumRest,
				KombiZuusctand);

			AufgaabeParamZerleegungErgeebnis.FüügeAnReegelungDistanceScpiilraumRest(ReegelungDistanceScpiilraumRest);

			return AufgaabeParamZerleegungErgeebnis;
		}

		override public IEnumerable<string> ZwekListeKomponenteAusParamBerecne()
		{
			var ZwekListeKomponenteAusParam = new List<string>();

			var AktioonInOverviewTabZuAktiviire = this.AktioonInOverviewTabZuAktiviire;

			var ColumnHeaderSort = this.ColumnHeaderSort;
			var ListEntryCollapse = this.ListEntryCollapse;
			var ListEntryMaceSictbar = this.ListEntryMaceSictbar;
			var ListEntryExpand = this.ListEntryExpand;
			var ListEntryToggleExpandCollapse = this.ListEntryToggleExpandCollapse;

			var ModuleMesungModuleButtonHint = this.ModuleMesungModuleButtonHint;
			var ModuleScalteAin = this.ModuleScalteAin;
			var ModuleScalteAus = this.ModuleScalteAus;
			var ModuleScalteUm = this.ModuleScalteUm;
			var TargetZuBearbeite = this.TargetZuBearbaite;
			var TargetUnLock = this.TargetUnLock;
			var OverViewObjektZuBearbeite = this.OverViewObjektZuBearbaite;

			var AktioonMenuBegin = this.AktioonMenuBegin;

			var InfoPanelEnable = this.InfoPanelEnable;
			var InfoPanelExpand = this.InfoPanelExpand;

			var GbsAstOklusioonVermaidung = this.GbsAstOklusioonVermaidung;

			var FittingZuApliziire = this.FittingZuApliziire;

			var LobbyAgentEntryStartConversation = this.LobbyAgentEntryStartConversation;
			var WindowAgentDialogueMissionAcceptOderRequest = this.WindowAgentDialogueMissionAcceptOderRequest;

			var WindowMinimize = this.WindowMinimize;
			var WindowHooleNaacVorne = this.WindowHooleNaacVorne;

			//	var GbsAstVerberge = this.GbsAstVerberge;

			var MissionStartConversation = this.MissionStartConversation;
			var MissionAccept = this.MissionAccept;
			var MissionDecline = this.MissionDecline;

			var TargetInputFookusSeze = this.TargetInputFookusSeze;

			var MenuEntry = this.MenuEntry;

			var InventoryItemTransport = this.InventoryItemTransport;

			var AktioonUnDock = this.AktioonUnDock;

			var MissionButtonUtilmenuObjectiveZuMese = this.MissionButtonUtilmenuObjectiveZuMese;

			var InventorySezeSictTypAufList = this.InventorySezeSictTypAufList;

			var ManööverUnterbreceNict = this.ManööverUnterbreceNict;

			if (null != AktioonInOverviewTabZuAktiviire)
			{
				ZwekListeKomponenteAusParam.AddRange(new string[] { "Overview.Tab[" + AktioonInOverviewTabZuAktiviire + "].activate" });
			}

			if (null != ColumnHeaderSort)
			{
				ZwekListeKomponenteAusParam.AddRange(new string[] { "Sort " + ObjektSictZwekKomponente(ColumnHeaderSort) });
			}

			if (null != ListEntryMaceSictbar)
			{
				ZwekListeKomponenteAusParam.AddRange(new string[] { "make visible " + ObjektSictZwekKomponente(ListEntryMaceSictbar) });
			}

			if (null != ListEntryExpand)
			{
				ZwekListeKomponenteAusParam.AddRange(new string[] { "expand " + ObjektSictZwekKomponente(ListEntryExpand) });
			}

			if (null != ListEntryCollapse)
			{
				ZwekListeKomponenteAusParam.AddRange(new string[] { "collapse " + ObjektSictZwekKomponente(ListEntryExpand) });
			}

			if (null != ModuleMesungModuleButtonHint)
			{
				ZwekListeKomponenteAusParam.AddRange(new string[] { ObjektSictZwekKomponente(ModuleMesungModuleButtonHint), "create Tooltip" });
			}

			if (null != ModuleScalteAin)
			{
				ZwekListeKomponenteAusParam.AddRange(new string[] { ObjektSictZwekKomponente(ModuleScalteAin), "switch[on]" });
			}

			if (null != ModuleScalteAus)
			{
				ZwekListeKomponenteAusParam.AddRange(new string[] { ObjektSictZwekKomponente(ModuleScalteAus), "switch[off]" });
			}

			if (null != OverViewObjektZuBearbeite)
			{
				ZwekListeKomponenteAusParam.Add("OverviewRow[" + (OverViewObjektSictZwekKomponente(OverViewObjektZuBearbeite) ?? "") + "]");
			}

			if (null != TargetZuBearbeite)
			{
				ZwekListeKomponenteAusParam.Add(ObjektSictZwekKomponente(TargetZuBearbeite));
			}

			if (null != TargetUnLock)
			{
				ZwekListeKomponenteAusParam.AddRange(new string[] { ObjektSictZwekKomponente(TargetUnLock), "UnLock" });
			}

			if (true == AktioonMenuBegin)
			{
				ZwekListeKomponenteAusParam.Add("create Menu");
			}

			if (true == AktioonMenuEntferne)
			{
				ZwekListeKomponenteAusParam.Add("close Menu");
			}

			/*
			 * 2015.01.06
			 * 
			if (true == AktioonWirkungDestrukt)
			{
				ZwekListeKomponenteAusParam.Add("destruct");
			}
			 * */

			if (true == AktioonInRaumObjektActivate)
			{
				ZwekListeKomponenteAusParam.Add("activate");
			}

			if (InfoPanelEnable.HasValue)
			{
				ZwekListeKomponenteAusParam.AddRange(new string[] { ObjektSictZwekKomponente(InfoPanelEnable.Value), ".Enable" });
			}

			if (InfoPanelExpand.HasValue)
			{
				ZwekListeKomponenteAusParam.AddRange(new string[] { ObjektSictZwekKomponente(InfoPanelExpand.Value), ".Expand" });
			}

			if (null != WindowMinimize)
			{
				ZwekListeKomponenteAusParam.AddRange(new string[] { ObjektSictZwekKomponente(WindowMinimize), ".minimize" });
			}

			if (null != WindowHooleNaacVorne)
			{
				ZwekListeKomponenteAusParam.AddRange(new string[] { ObjektSictZwekKomponente(WindowHooleNaacVorne), ".bring to front" });
			}

			if (true == AktioonNeocomMenuEntferne)
			{
				ZwekListeKomponenteAusParam.Add("NeocomMenu.Close");
			}

			if (null != GbsAstOklusioonVermaidung)
			{
				ZwekListeKomponenteAusParam.Add("ensure UIElement not occluded");
			}

			if (null != FittingZuApliziire)
			{
				ZwekListeKomponenteAusParam.AddRange(new string[] { ObjektSictZwekKomponente(FittingZuApliziire), "Apply" });
			}

			if (null != LobbyAgentEntryStartConversation)
			{
				ZwekListeKomponenteAusParam.AddRange(new string[] { ObjektSictZwekKomponente(LobbyAgentEntryStartConversation), "start Conversation" });
			}

			if (null != WindowAgentDialogueMissionAcceptOderRequest)
			{
				ZwekListeKomponenteAusParam.AddRange(new string[] { ObjektSictZwekKomponente(WindowAgentDialogueMissionAcceptOderRequest), "accept or request" });
			}

			/*
			if (null != GbsAstVerberge)
			{
				var GbsAstVerbergeInGbsFläce = GbsAstVerberge.InGbsFläce;

				ZwekListeKomponenteAusParam.Add("hide UIElement at " + ObjektSictZwekKomponente(GbsAstVerbergeInGbsFläce));
			}
			*/

			if (true == AktioonDroneLaunch)
			{
				ZwekListeKomponenteAusParam.Add("Drones.Launch");
			}

			if (true == AktioonDroneEngage)
			{
				ZwekListeKomponenteAusParam.Add(".Drones.Engage");
			}

			if (true == AktioonDroneReturn)
			{
				ZwekListeKomponenteAusParam.Add("Drones.Return");
			}

			if (null != MissionStartConversation)
			{
				ZwekListeKomponenteAusParam.AddRange(new string[] { ObjektSictZwekKomponente(MissionStartConversation), "start conversation" });
			}

			if (null != MissionAccept)
			{
				ZwekListeKomponenteAusParam.AddRange(new string[] { ObjektSictZwekKomponente(MissionAccept), "accept" });
			}

			if (null != MissionDecline)
			{
				ZwekListeKomponenteAusParam.AddRange(new string[] { ObjektSictZwekKomponente(MissionDecline), "decline" });
			}

			if (null != TargetInputFookusSeze)
			{
				ZwekListeKomponenteAusParam.AddRange(new string[] { ObjektSictZwekKomponente(TargetInputFookusSeze), "set Input Focus" });
			}

			if (null != MenuEntry)
			{
				ZwekListeKomponenteAusParam.Add("Menu.Entry[" + (MenuEntry.Bescriftung ?? "") + "].activate");
			}

			if (true == AfterburnerScalteAin)
			{
				ZwekListeKomponenteAusParam.Add("Afterburner.switch[on]");
			}

			if (true == AktioonCargoDurcsuuce)
			{
				ZwekListeKomponenteAusParam.Add("search Cargo");
			}

			if (true == AktioonInOverviewMaceSictbar)
			{
				ZwekListeKomponenteAusParam.Add("make visible in Overview");
			}

			if (true == InOverviewTabFolgeViewportDurcGrid)
			{
				ZwekListeKomponenteAusParam.Add("scroll through Overview");
			}

			if (null != InventoryItemTransport)
			{
				var MengeItemSictString = ObjektSictZwekKomponente(InventoryItemTransport.MengeItem);

				var ZiilSictString = TreeViewEntrySictZwekKomponente(InventoryItemTransport.ZiilObjektTreeViewEntry);

				ZwekListeKomponenteAusParam.Add(
					"InInventory." + (MengeItemSictString ?? "") +
					".MoveTo[" + (ZiilSictString ?? "") + "]");
			}

			if (null != InventorySezeSictTypAufList)
			{
				ZwekListeKomponenteAusParam.Add(
					"InInventory.set View Type to List");
			}

			if (DistanzAinzuscteleScrankeMin.HasValue ||
				DistanzAinzuscteleScrankeMax.HasValue)
			{
				ZwekListeKomponenteAusParam.Add("regulate Distance");
			}

			/*
			 * 2014.09.28
			 * 
			if (null != AktioonDockStationName)
			{
				ZwekListeKomponenteAusParam.Add("Dock at Station[" + AktioonDockStationName + "]");
			}
			 * */

			if (true == AktioonUnDock)
			{
				ZwekListeKomponenteAusParam.Add("UnDock");
			}

			if (null != MissionButtonUtilmenuObjectiveZuMese)
			{
				ZwekListeKomponenteAusParam.Add("measure Objective from Mission behind Info Panel.Mission[" + (MissionButtonUtilmenuObjectiveZuMese.Bescriftung ?? "") + "]");
			}

			if (null != ManööverUnterbreceNict)
			{
				ZwekListeKomponenteAusParam.Add("inhibit disruption of Maneuver[" + (ManööverEntrySictZwekKomponente(ManööverUnterbreceNict) ?? "") + "]");
			}

			return ZwekListeKomponenteAusParam;
		}

		override public bool HinraicendGlaicwertigFürFortsazScpez(
			SictAufgaabeParam Andere)
		{
			var AndereScpez = Andere as AufgaabeParamAndere;

			if (null == AndereScpez)
			{
				return false;
			}

			if (null != WindowAgentDialogueMissionAcceptOderRequest ||
				null != AndereScpez.WindowAgentDialogueMissionAcceptOderRequest)
			{
				//	2014.07.10 Beobactung Probleem: Aufgaabe mit alte Versioon von WindowAgentDialogueMissionAcceptOderRequest werd fortgesezt mit nit meer existente Bezaicner von GBS Ast
				//	2014.07.10 Änderung: kain Fortsaz für Aufgaabe WindowAgentDialogueMissionAcceptOderRequest
				return false;
			}

			return HinraicendGlaicwertigFürFortsazScpezAndere(this, AndereScpez);
		}

		static public bool HinraicendGlaicwertigFürFortsazScpezAndere(
			AufgaabeParamAndere O0,
			AufgaabeParamAndere O1)
		{
			if (object.Equals(O0, O1))
			{
				return true;
			}

			if (null == O0 || null == O1)
			{
				return false;
			}


			return

				O0.NaacNuzerMeldungZuEveOnline == O1.NaacNuzerMeldungZuEveOnline &&

	object.Equals(O0.NaacNuzerVorsclaagWirkung, O1.NaacNuzerVorsclaagWirkung) &&

	object.Equals(O0.NaacAbsclusTailWirkungWartezaitScranke, O1.NaacAbsclusTailWirkungWartezaitScranke) &&

	string.Equals(O0.AktioonInOverviewTabZuAktiviire, O1.AktioonInOverviewTabZuAktiviire, StringComparison.InvariantCultureIgnoreCase) &&

	O0.InOverviewTabFolgeViewportDurcGrid == O1.InOverviewTabFolgeViewportDurcGrid &&

	O0.AktioonInOverviewMaceSictbar == O1.AktioonInOverviewMaceSictbar &&

	object.Equals(O0.OverViewObjektZuBearbaite, O1.OverViewObjektZuBearbaite) &&
	object.Equals(O0.TargetZuBearbaite, O1.TargetZuBearbaite) &&

	object.Equals(O0.AktioonTargetActivate, O1.AktioonTargetActivate) &&

	O0.AktioonCargoDurcsuuce == O1.AktioonCargoDurcsuuce &&

	object.Equals(O0.TargetUnLock, O1.TargetUnLock) &&
	object.Equals(O0.TargetInputFookusSeze, O1.TargetInputFookusSeze) &&

	object.Equals(O0.ModuleMesungModuleButtonHint, O1.ModuleMesungModuleButtonHint) &&
	object.Equals(O0.ModuleScalteAin, O1.ModuleScalteAin) &&
	object.Equals(O0.ModuleScalteAus, O1.ModuleScalteAus) &&
	object.Equals(O0.ModuleScalteUm, O1.ModuleScalteUm) &&

	O0.AfterburnerScalteAin == O1.AfterburnerScalteAin &&

	HinraicendGlaicwertigFürFortsaz(O0.MausPfaad, O1.MausPfaad) &&

	O1.AktioonUnDock == O1.AktioonUnDock &&

	HinraicendGlaicwertigFürFortsaz(O0.MenuListeAstBedingung, O1.MenuListeAstBedingung) &&
	MenuWurzelHinraicendGlaicwertigFürFortsaz(O0.MenuWurzelGbsObjekt, O1.MenuWurzelGbsObjekt) &&
	O0.AktioonMenuBegin == O1.AktioonMenuBegin &&
	HinraicendGlaicwertigFürFortsaz(O0.MenuEntry, O1.MenuEntry) &&

	//	TailIdentHinraicendGlaicwertigFürFortsaz(O0.GbsAstVerberge, O1.GbsAstVerberge) &&

	O0.AktioonNeocomMenuEntferne == O1.AktioonNeocomMenuEntferne &&

	O0.InfoPanelExpand == O1.InfoPanelExpand &&
	O0.InfoPanelEnable == O1.InfoPanelEnable &&

	HinraicendGlaicwertigFürFortsaz(O0.GbsAstOklusioonVermaidung, O1.GbsAstOklusioonVermaidung) &&

	O0.WindowMinimize == O1.WindowMinimize &&
	O0.WindowHooleNaacVorne == O1.WindowHooleNaacVorne &&

	GbsElement.Identisc(O0.ColumnHeaderSort, O1.ColumnHeaderSort) &&
	O0.ListEntryMaceSictbar == O1.ListEntryMaceSictbar &&
	O0.ListEntryExpand == O1.ListEntryExpand &&
	O0.ListEntryCollapse == O1.ListEntryCollapse &&
	O0.ListEntryToggleExpandCollapse == O1.ListEntryToggleExpandCollapse &&

	O0.GridVerlase == O1.GridVerlase &&

	O0.ManööverUnterbreceNict == O1.ManööverUnterbreceNict &&
	O0.VorrangVorManööverUnterbreceNict == O1.VorrangVorManööverUnterbreceNict &&
	O0.ManööverAuszufüüreTyp == O1.ManööverAuszufüüreTyp &&
	O0.DistanzAinzuscteleScrankeMin == O1.DistanzAinzuscteleScrankeMin &&
	O0.DistanzAinzuscteleScrankeMax == O1.DistanzAinzuscteleScrankeMax &&

	HinraicendGlaicwertigFürFortsaz(O0.LobbyAgentEntryStartConversation, O1.LobbyAgentEntryStartConversation) &&
	MenuWurzelHinraicendGlaicwertigFürFortsaz(O0.MissionButtonUtilmenuObjectiveZuMese, O1.MissionButtonUtilmenuObjectiveZuMese) &&
	HinraicendGlaicwertigFürFortsaz(O0.WindowAgentDialogueMissionAcceptOderRequest, O1.WindowAgentDialogueMissionAcceptOderRequest) &&
	HinraicendGlaicwertigFürFortsaz(O0.WindowAgentDialogueMissionDecline, O1.WindowAgentDialogueMissionDecline) &&
	O0.MissionAccept == O1.MissionAccept &&
	O0.MissionDecline == O1.MissionDecline &&

	HinraicendGlaicwertigFürFortsaz(O0.FittingZuApliziire, O1.FittingZuApliziire) &&

	O0.AktioonDroneLaunch == O1.AktioonDroneLaunch &&
	O0.AktioonDroneEngage == O1.AktioonDroneEngage &&
	O0.AktioonDroneReturn == O1.AktioonDroneReturn &&

	HinraicendGlaicwertigFürFortsaz(O0.InventoryItemTransport, O1.InventoryItemTransport) &&

	HinraicendGlaicwertigFürFortsaz(O0.InventorySezeSictTypAufList, O1.InventorySezeSictTypAufList)

	;
		}

		override public bool WirkungGbsMengeZuusctandGrupeVorrausgeseztAlsUnverändertNaacAbsclusTailWirkung()
		{
			if (null != ModuleMesungModuleButtonHint)
			{
				return true;
			}

			return false;
		}

		override public bool IstBlatNaacNuzerVorsclaagWirkung()
		{
			var NaacNuzerVorsclaagWirkung = this.NaacNuzerVorsclaagWirkung;

			if (null == NaacNuzerVorsclaagWirkung)
			{
				return false;
			}

			return
				null == OverViewObjektZuBearbaite &&
				null == TargetZuBearbaite &&
				null == MenuEntry &&
				null == MenuListeAstBedingung &&
				null == ModuleScalteAin &&
				null == ModuleScalteUm &&
				null == ModuleMesungModuleButtonHint;
		}

		static public SictAufgaabeParamZerleegungErgeebnis ZerleegeShipAktuelOpenInventoryCargoTyp(
			SictAutomatZuusctand AutomaatZuusctand,
			SictShipCargoTypSictEnum CargoTyp)
		{
			VonSensor.WindowInventoryPrimary ErgeebnisWindowShipInventory;
			VonSensor.Inventory ErgeebnisShipInventory;

			return ZerleegeShipAktuelOpenInventoryCargoTyp(
				AutomaatZuusctand,
				CargoTyp,
				out	ErgeebnisWindowShipInventory,
				out	ErgeebnisShipInventory);
		}

		static public SictAufgaabeParamZerleegungErgeebnis ZerleegeShipAktuelOpenInventoryCargoTyp(
			SictAutomatZuusctand AutomaatZuusctand,
			SictShipCargoTypSictEnum CargoTyp,
			out	VonSensor.WindowInventoryPrimary ErgeebnisWindowShipInventory,
			out	VonSensor.Inventory ErgeebnisShipInventory)
		{
			bool ZerleegungVolsctändig = true;
			var InternMengeAufgaabeKomponenteParam = new List<SictAufgaabeParam>();

			ErgeebnisWindowShipInventory = null;
			ErgeebnisShipInventory = null;

			VonSensor.WindowInventoryPrimary ScnapscusShipWindowInventory = null;

			var Gbs = AutomaatZuusctand.Gbs();

			var AusScnapscusAuswertungZuusctand = AutomaatZuusctand.ListeScnapscusLezteAuswertungErgeebnisNaacSimu;

			var AusScnapcusMengeWindowInventory =
				(null == AusScnapscusAuswertungZuusctand) ? null : AusScnapscusAuswertungZuusctand.MengeWindowInventory;

			if (null != AusScnapcusMengeWindowInventory)
			{
				if (1 == AusScnapcusMengeWindowInventory.Length)
				{
					var KandidaatAnforderungLeereCargoWindowInventory = AusScnapcusMengeWindowInventory.FirstOrDefault();

					if (null != KandidaatAnforderungLeereCargoWindowInventory)
					{
						if (null != KandidaatAnforderungLeereCargoWindowInventory.LinxTreeEntryActiveShip)
						{
							ScnapscusShipWindowInventory = KandidaatAnforderungLeereCargoWindowInventory;
						}
					}
				}
			}

			var ScnapscusShipWindowInventoryLinxTreeEntryActiveShip =
				(null == ScnapscusShipWindowInventory) ? null : ScnapscusShipWindowInventory.LinxTreeEntryActiveShip;

			if (null == ScnapscusShipWindowInventory)
			{
				ZerleegungVolsctändig = false;

				var NeocomButtonInventory = AusScnapscusAuswertungZuusctand.NeocomButtonInventory;

				if (null == NeocomButtonInventory)
				{
				}
				else
				{
					InternMengeAufgaabeKomponenteParam.Add(AufgaabeParamAndere.KonstruktMausPfaad(SictAufgaabeParamMausPfaad.KonstruktMausKlikLinx(NeocomButtonInventory)));
				}
			}
			else
			{
				var WindowInventoryZuusctand =
					(null == Gbs) ? null :
					Gbs.ZuHerkunftAdreseWindow(ScnapscusShipWindowInventory.Ident);

				if (null == ScnapscusShipWindowInventoryLinxTreeEntryActiveShip)
				{
				}
				else
				{
					var ZuCargoTypTreeEntry =
						ScnapscusShipWindowInventoryLinxTreeEntryActiveShip.TreeEntryBerecneAusCargoTyp(CargoTyp);

					var ZuAuswaalReczMengeKandidaatLinxTreeEntry = ScnapscusShipWindowInventory.ZuAuswaalReczMengeKandidaatLinxTreeEntry;

					if (null == ZuCargoTypTreeEntry)
					{
						//	Sicersctele das AnforderungLeereCargoWindowInventoryLinxTreeEntryActiveShip expanded.

						if (0 < Bib3.Extension.CountNullable(ScnapscusShipWindowInventoryLinxTreeEntryActiveShip.MengeChild))
						{
							//	ist beraits Expanded.
						}
						else
						{
							var ExpandCollapseToggleFläce = ScnapscusShipWindowInventoryLinxTreeEntryActiveShip.ExpandCollapseToggleFläce;

							if (null == ExpandCollapseToggleFläce)
							{
							}
							else
							{
								InternMengeAufgaabeKomponenteParam.Add(AufgaabeParamAndere.KonstruktMausPfaad(
									SictAufgaabeParamMausPfaad.KonstruktMausKlikLinx(ExpandCollapseToggleFläce)));
							}
						}
					}
					else
					{
						if (1 == Bib3.Extension.CountNullable(ZuAuswaalReczMengeKandidaatLinxTreeEntry))
						{
							var ZuAuswaalReczLinxTreeEntry = ZuAuswaalReczMengeKandidaatLinxTreeEntry.FirstOrDefault();
							var AuswaalReczInventory = ScnapscusShipWindowInventory.AuswaalReczInventory;

							if (ZuCargoTypTreeEntry == ZuAuswaalReczLinxTreeEntry &&
								null != AuswaalReczInventory)
							{
								//	Erfolg.

								ErgeebnisWindowShipInventory = ScnapscusShipWindowInventory;
								ErgeebnisShipInventory = AuswaalReczInventory;
							}
							else
							{
								InternMengeAufgaabeKomponenteParam.Add(AufgaabeParamAndere.KonstruktMausPfaad(
									SictAufgaabeParamMausPfaad.KonstruktMausKlikLinx(ZuCargoTypTreeEntry.TopContLabel)));
							}
						}
					}
				}
			}

			return new SictAufgaabeParamZerleegungErgeebnis(InternMengeAufgaabeKomponenteParam, ZerleegungVolsctändig);
		}

		override public SictVorsclaagNaacProcessWirkung NaacNuzerVorsclaagWirkungVirt()
		{
			return this.NaacNuzerVorsclaagWirkung;
		}

		override public SictNaacNuzerMeldungZuEveOnline NaacNuzerMeldungZuEveOnlineVirt()
		{
			return this.NaacNuzerMeldungZuEveOnline;
		}

		override public SictAufgaabeParamMausPfaad MausPfaadVirt()
		{
			return this.MausPfaad;
		}

		override public SictGbsWindowZuusctand WindowMinimizeVirt()
		{
			return this.WindowMinimize;
		}

		override public SictOverViewObjektZuusctand OverViewObjektZuBearbaiteVirt()
		{
			return this.OverViewObjektZuBearbaite;
		}

		override public SictTargetZuusctand TargetZuBearbaiteVirt()
		{
			return this.TargetZuBearbaite ?? this.TargetUnLock ?? this.TargetInputFookusSeze;
		}

		override public Int64? DistanzAinzuscteleScrankeMinVirt()
		{
			return this.DistanzAinzuscteleScrankeMin;
		}

		override public Int64? DistanzAinzuscteleScrankeMaxVirt()
		{
			return this.DistanzAinzuscteleScrankeMax;
		}

		override public bool? VorrangVorManööverUnterbreceNictVirt()
		{
			return this.VorrangVorManööverUnterbreceNict;
		}

		override public SictVerlaufBeginUndEndeRef<ShipUiIndicationAuswert> ManööverUnterbreceNictVirt()
		{
			return this.ManööverUnterbreceNict;
		}


	}
}
