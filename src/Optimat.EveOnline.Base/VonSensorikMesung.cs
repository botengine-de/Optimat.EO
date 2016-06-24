using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bib3;
using Newtonsoft.Json;
using Optimat.EveOnline.AuswertGbs;
using Optimat.EveOnline.VonSensor;
using ExtractFromOldAssembly.Bib3;

namespace Optimat.EveOnline
{

	public class VonSensorikMesung : GbsElement, ICloneable
	{
		public int? SessionDurationRemaining;

		public Menu[] MengeMenu;

		public string VersionString;

		public Window[] MengeWindowSonstige;

		public WindowStack[] MengeWindowStack;

		public WindowStationLobby WindowStationLobby;

		public WindowDroneView WindowDroneView;

		public WindowFittingWindow WindowFittingWindow;

		public WindowFittingMgmt WindowFittingMgmt;

		public WindowOverView WindowOverview;

		public Window WindowSelectedItemView;

		public WindowSurveyScanView WindowSurveyScanView;

		public WindowInventoryPrimary[] MengeWindowInventory;

		public WindowAgentDialogue[] MengeWindowAgentDialogue;

		public WindowAgentBrowser[] MengeWindowAgentBrowser;

		public ShipUi ShipUi;

		public ShipState SelfShipState;

		public GbsElement InfoPanelButtonLocationInfo;

		public GbsElement InfoPanelButtonRoute;

		public GbsElement InfoPanelButtonMissions;

		public InfoPanelLocationInfo InfoPanelLocationInfo;

		public InfoPanelRoute InfoPanelRoute;

		public InfoPanelMissions InfoPanelMissions;

		public bool? InfoPanelIncursionsExistent;

		public UtilmenuMissionInfo UtilmenuMission;

		public Message[] MengeAbovemainMessage;

		public PanelGroup[] MengeAbovemainPanelGroup;

		public PanelGroup[] MengeAbovemainPanelEveMenu;

		public ShipUiTarget[] MengeTarget;

		public SystemMenu SystemMenu;

		public ModuleButtonHint ModuleButtonHint;

		public WindowTelecom[] MengeWindowTelecom;

		public GbsElement NeocomButtonInventory;

		public GbsElementMitBescriftung NeocomClock;

		public bool? CharacterAuswaalAbgesclose;

		public KeyValuePair<int, int>? NeocomClockZaitKalenderModuloTaagMinMax;

		public VonSensorikMesung Kopii()
		{
			return SictRefBaumKopii.ObjektKopiiErsctele(this);
		}

		public object Clone()
		{
			return Kopii();
		}

	}

}
