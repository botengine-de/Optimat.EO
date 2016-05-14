using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Optimat.EveOnline;
using Bib3;
using Optimat.EveOnline.Base;
using ExtractFromOldAssembly.Bib3;

namespace Optimat.EveOnline.VonSensor
{
	/// <summary>
	/// Not using readonly because some of the serializers used in the project cannot write on readonly in struct.
	/// </summary>
	public struct ScrollHeaderInfoFürItem
	{
		public int Index;

		public string Bescriftung;

		public int GrenzeLinx;

		public int GrenzeRecz;

		public ScrollHeaderInfoFürItem(
			int Index,
			string Bescriftung,
			int GrenzeLinx,
			int GrenzeRecz)
		{
			this.Index = Index;
			this.Bescriftung = Bescriftung;
			this.GrenzeLinx = GrenzeLinx;
			this.GrenzeRecz = GrenzeRecz;
		}
	}

	public class LobbyAgentEntry : GbsElement
	{
		readonly public string AgentName;

		readonly public string AgentTyp;

		readonly public int? AgentLevel;

		readonly public string ZaileTypUndLevelText;

		public string HeaderText;

		readonly public GbsElement ButtonStartConversation;

		public LobbyAgentEntry(
			GbsElement ZuKopiire,
			string AgentName,
			string AgentTyp,
			int? AgentLevel,
			string ZaileTypUndLevelText,
			GbsElement ButtonStartConversation)
			:
			base(ZuKopiire)
		{
			this.AgentName = AgentName;
			this.AgentTyp = AgentTyp;
			this.AgentLevel = AgentLevel;
			this.ZaileTypUndLevelText = ZaileTypUndLevelText;
			this.ButtonStartConversation = ButtonStartConversation;
		}

		public LobbyAgentEntry()
		{
		}

		override public string ToString()
		{
			return JsonConvert.SerializeObject(this, Formatting.Indented);
		}
	}


	public class TabGroup : GbsElement
	{
		readonly public Tab[] ListeTab;

		readonly public Tab TabSelected;

		public TabGroup()
		{
		}

		public TabGroup(
			Tab[] ListeTab,
			Tab TabSelected = null)
		{
			this.ListeTab = ListeTab;
			this.TabSelected = TabSelected;
		}
	}


	public class Tab : GbsElement
	{
		readonly public GbsElementMitBescriftung Label;

		readonly public int? LabelColorOpazitäätMili;

		public string LabelBescriftung
		{
			get
			{
				var Label = this.Label;

				if (null == Label)
				{
					return null;
				}

				return Label.Bescriftung;
			}
		}

		public Tab()
		{
		}

		public Tab(
			GbsElement GbsAst,
			GbsElementMitBescriftung Label,
			int? LabelColorOpazitäätMili)
			:
			base(GbsAst)
		{
			this.Label = Label;
			this.LabelColorOpazitäätMili = LabelColorOpazitäätMili;
		}

		static public bool TabMitBescriftungPräsentInMengeTab(string TabBescriftung, Tab[] MengeTab)
		{
			if (null == TabBescriftung)
			{
				return false;
			}

			if (null == MengeTab)
			{
				return false;
			}

			return MengeTab.Any((Kandidaat) => string.Equals(Kandidaat.LabelBescriftung, TabBescriftung, StringComparison.InvariantCultureIgnoreCase));
		}
	}


	public class WindowOverView : Window, ICloneable
	{
		readonly public OverviewZaile[] AusTabListeZaileOrdnetNaacLaage;

		readonly public BasicDynamicScroll Scroll;

		readonly public string OverviewPresetIdent;

		readonly public OverviewPresetDefaultTyp? PresetAlsDefaultTyp;

		readonly public TabGroup TabGroup;

		readonly public Tab[] ListeTabNuzbar;

		readonly public bool? MeldungMengeZaileLeer;

		readonly public int? MengeZaileAnzaalLaageAbwaicendVonSortedDistanceAufsctaigend;

		public ListColumnHeader[] ListeColumnHeader
		{
			get
			{
				var Scroll = this.Scroll;

				if (null == Scroll)
				{
					return null;
				}

				return Scroll.ListeColumnHeader;
			}
		}

		public ListColumnHeader ColumnHeaderDistance
		{
			get
			{
				return ListeColumnHeader.FirstOrDefaultNullable((Kandidaat) => Regex.Match(Kandidaat.HeaderBescriftung ?? "", "Dist", RegexOptions.IgnoreCase).Success);
			}
		}

		public Tab TabSelected
		{
			get
			{
				var TabGroup = this.TabGroup;

				if (null == TabGroup)
				{
					return null;
				}

				return TabGroup.TabSelected;
			}
		}

		public bool? ZaileSindSortiirtNaacDistanceAufsctaigend
		{
			get
			{
				if (!MengeZaileAnzaalLaageAbwaicendVonSortedDistanceAufsctaigend.HasValue)
				{
					return null;
				}

				return 0 == (MengeZaileAnzaalLaageAbwaicendVonSortedDistanceAufsctaigend ?? -1);
			}
		}

		public Int64? ExtraktScrollHandleFläceGrenzeOobenAntailAnGesamtMili()
		{
			var Scroll = this.Scroll;

			if (null == Scroll)
			{
				return null;
			}

			return Scroll.ScrollHandleFläceGrenzeOobnAntailAnGesamtMili;
		}

		public Int64? ExtraktScrollHandleFläceGrenzeUntenAntailAnGesamtMili()
		{
			var Scroll = this.Scroll;

			if (null == Scroll)
			{
				return null;
			}

			return Scroll.ScrollHandleFläceGrenzeUntnAntailAnGesamtMili;
		}

		public WindowOverView(
			Window ZuKopiire,
			string OverviewPresetIdent,
			TabGroup TabGroup,
			Tab[] ListeTabNuzbar,
			BasicDynamicScroll Scroll,
			OverviewZaile[] AusTabListeZaileOrdnetNaacLaage,
			bool? MeldungMengeZaileLeer,
			int? MengeZaileAnzaalLaageAbwaicendVonSortedDistanceAufsctaigend)
			:
			base(ZuKopiire)
		{
			this.OverviewPresetIdent = OverviewPresetIdent;

			PresetAlsDefaultTyp = Optimat.Glob.EnumParseNulbar<OverviewPresetDefaultTyp>(OverviewPresetIdent);

			this.TabGroup = TabGroup;
			this.ListeTabNuzbar = ListeTabNuzbar;
			this.Scroll = Scroll;
			this.AusTabListeZaileOrdnetNaacLaage = AusTabListeZaileOrdnetNaacLaage;
			this.MeldungMengeZaileLeer = MeldungMengeZaileLeer;

			this.MengeZaileAnzaalLaageAbwaicendVonSortedDistanceAufsctaigend = MengeZaileAnzaalLaageAbwaicendVonSortedDistanceAufsctaigend;
		}

		public WindowOverView()
		{
		}

		public WindowOverView Kopii()
		{
			return SictRefBaumKopii.ObjektKopiiErsctele(this);
		}

		public object Clone()
		{
			return Kopii();
		}
	}


	public class WindowFittingWindowDefenceStatsRow : GbsElement
	{
		public WindowFittingWindowDefenceStatsRow(
			GbsElement ZuKopiire)
			:
			base(ZuKopiire)
		{
		}

		public WindowFittingWindowDefenceStatsRow()
			:
			this(null)
		{
		}

		public Int64? Kapazitäät;

		public Int64? RechargeDauer;

		public SictDamageMitBetraagIntValue[] MengeResistanceMili;
	}


	public class FittingEntry : GbsElementMitBescriftung
	{
		public bool? Selected;

		public FittingEntry()
		{
		}

		public FittingEntry(GbsElementMitBescriftung ZuKopiire)
			:
			base(ZuKopiire)
		{
		}
	}


	public class FittingSlot : GbsElement
	{
		[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
		public SictShipSlotTyp? SlotTyp;

		public int? InGrupeTypSlotIndex;

		public FittingSlot(
			GbsElement ZuKopiire)
			:
			base(ZuKopiire)
		{
		}

		public FittingSlot()
			:
			this(null)
		{
		}
	}


	public class WindowFittingMgmt : Window
	{
		public FittingEntry[] MengeFittingEntry;

		public WindowFittingMgmt(Window ZuKopiire)
			:
			base(ZuKopiire)
		{
		}

		public WindowFittingMgmt()
		{
		}
	}


	public class WindowSurveyScanView : Window
	{
		readonly public ScnapscusListGrouped List;

		readonly public GbsElement ButtonClear;

		public WindowSurveyScanView(
			Window ZuKopiire,
			ScnapscusListGrouped List = null,
			GbsElement ButtonClear = null)
			:
			base(ZuKopiire)
		{
			this.List = List;
			this.ButtonClear = ButtonClear;
		}

		public WindowSurveyScanView()
		{
		}

		override public ScnapscusListGrouped ListHaupt()
		{
			return List;
		}
	}


	public class WindowFittingWindow : Window
	{
		readonly public GbsElementMitBescriftung ButtonStoredFittingsBrowse;

		public WindowFittingWindow(
			Window ZuKopiire,
			GbsElementMitBescriftung ButtonStoredFittingsBrowse)
			:
			base(ZuKopiire)
		{
			this.ButtonStoredFittingsBrowse = ButtonStoredFittingsBrowse;
		}

		public WindowFittingWindow()
		{
		}
	}

	public enum DroneEntryStatusSictEnum
	{
		Kain = 0,
		Idle = 1,
		Returning = 3,
		Fighting = 4,
	}


	public class WindowDroneViewEntry : GbsElement
	{
		readonly public string DroneName;

		readonly public ShipHitpointsAndEnergy Treferpunkte;

		readonly public string StatusSictString;

		[JsonConverter(typeof(StringEnumConverter))]
		readonly public DroneEntryStatusSictEnum? StatusSictEnum;

		public WindowDroneViewEntry(
			GbsElement ZuKopiire,
			string DroneName = null,
			ShipHitpointsAndEnergy Treferpunkte = null,
			string StatusSictString = null,
			DroneEntryStatusSictEnum? StatusSictEnum = null)
			:
			base(ZuKopiire)
		{
			this.DroneName = DroneName;
			this.Treferpunkte = Treferpunkte;
			this.StatusSictString = StatusSictString;
			this.StatusSictEnum = StatusSictEnum;
		}

		public WindowDroneViewEntry()
		{
		}
	}


	public class WindowDroneViewGrupe : GbsElement
	{
		readonly public GbsElementMitBescriftung Header;

		readonly public GbsElement FürMenuBeginFläce;

		readonly public int? MengeEntryAnzaal;

		/// <summary>
		/// Count kan hiir klainer sai als MengeEntryAnzaal z.B. fals Entry nit sictbar isc.
		/// </summary>
		readonly public WindowDroneViewEntry[] MengeDroneEntry;

		public WindowDroneViewGrupe(
			GbsElement ZuKopiire,
			GbsElementMitBescriftung Header = null,
			GbsElement FürMenuBeginFläce = null,
			int? MengeEntryAnzaal = null,
			WindowDroneViewEntry[] MengeDroneEntry = null)
			:
			base(ZuKopiire)
		{
			this.Header = Header;
			this.FürMenuBeginFläce = FürMenuBeginFläce;
			this.MengeEntryAnzaal = MengeEntryAnzaal;
			this.MengeDroneEntry = MengeDroneEntry;
		}

		public WindowDroneViewGrupe()
		{
		}
	}


	public class WindowSelectedItemViewScaltfläceAction : Window
	{
		readonly public bool? Verfüügbar;

		public WindowSelectedItemViewScaltfläceAction(
			bool? Verfüügbar)
		{
			this.Verfüügbar = Verfüügbar;
		}

		public WindowSelectedItemViewScaltfläceAction()
		{
		}
	}


	public class WindowSelectedItemView : Window
	{
		readonly public string SelectedItemName;

		readonly public Int64? SelectedItemDistanceScrankeMin;

		readonly public Int64? SelectedItemDistanceScrankeMax;

		readonly public WindowSelectedItemViewScaltfläceAction[] MengeScaltfläceAction;

		public WindowSelectedItemView(
			Window ZuKopiire,
			string SelectedItemName,
			Int64? SelectedItemDistanceScrankeMin,
			Int64? SelectedItemDistanceScrankeMax,
			WindowSelectedItemViewScaltfläceAction[] MengeScaltfläceAction = null)
			:
			base(ZuKopiire)
		{
			this.SelectedItemName = SelectedItemName;
			this.SelectedItemDistanceScrankeMin = SelectedItemDistanceScrankeMin;
			this.SelectedItemDistanceScrankeMax = SelectedItemDistanceScrankeMax;
			this.MengeScaltfläceAction = MengeScaltfläceAction;
		}

		public WindowSelectedItemView()
		{
		}
	}


	public class WindowDroneView : Window
	{
		public WindowDroneViewGrupe[] ListeGrupe;

		public WindowDroneViewGrupe GrupeDronesInBay;

		public WindowDroneViewGrupe GrupeDronesInLocalSpace;

		public int? DronesInBayAnzaal
		{
			get
			{
				var GrupeDronesInBay = this.GrupeDronesInBay;

				if (null == GrupeDronesInBay)
				{
					return null;
				}

				return GrupeDronesInBay.MengeEntryAnzaal;
			}
		}

		public int? DronesInLocalSpaceAnzaal
		{
			get
			{
				var GrupeDronesInLocalSpace = this.GrupeDronesInLocalSpace;

				if (null == GrupeDronesInLocalSpace)
				{
					return null;
				}

				return GrupeDronesInLocalSpace.MengeEntryAnzaal;
			}
		}

		public WindowDroneView(Window ZuKopiire)
			:
			base(ZuKopiire)
		{
		}

		public WindowDroneView()
		{
		}
	}


	public class WindowStack : Window
	{
		readonly public TabGroup TabGroup;

		readonly public Window WindowAktiiv;

		public WindowStack()
		{
		}

		public WindowStack(
			Window Base,
			TabGroup TabGroup = null,
			Window WindowAktiiv = null)
			:
			base(Base)
		{
			this.TabGroup = TabGroup;
			this.WindowAktiiv = WindowAktiiv;
		}
	}


	public class SystemMenu : Window
	{
		public SystemMenu()
		{
		}

		public SystemMenu(
			Window Base)
			:
			base(Base)
		{
		}
	}


	public class WindowStationLobby : Window
	{
		readonly public GbsElement KnopfUndock;

		readonly public GbsElement KnopfFitting;

		readonly public LobbyAgentEntry[] MengeAgentEntry;

		readonly public bool? UnDocking;

		public WindowStationLobby(
			Window ZuKopiire,
			GbsElement KnopfUndock,
			GbsElement KnopfFitting,
			LobbyAgentEntry[] MengeAgentEntry,
			bool? UnDocking)
			:
			base(ZuKopiire)
		{
			this.KnopfUndock = KnopfUndock;
			this.KnopfFitting = KnopfFitting;
			this.MengeAgentEntry = MengeAgentEntry;
			this.UnDocking = UnDocking;
		}

		public WindowStationLobby()
		{
		}

	}


	public class InfoPanelMissionsMission : GbsElementMitBescriftung
	{
		public InfoPanelMissionsMission()
		{
		}

		public InfoPanelMissionsMission(GbsElementMitBescriftung ZuKopiire)
			:
			base(ZuKopiire)
		{
		}
	}


	public class UtilmenuMissionLocationInfo : GbsElement
	{
		public string HeaderText;

		public GbsElementMitBescriftung KnopfLocation;

		public GbsElementMitBescriftung KnopfDock;

		public GbsElementMitBescriftung KnopfApproach;

		public GbsElementMitBescriftung KnopfSetDestination;

		public GbsElementMitBescriftung KnopfWarpTo;

		public UtilmenuMissionLocationInfo()
			:
			this(null)
		{
		}

		public UtilmenuMissionLocationInfo(
			GbsElement GbsAst)
			:
			base(GbsAst)
		{
		}
	}


	public class UtilmenuMissionInfo : GbsElement
	{
		public GbsElementMitBescriftung Header
		{
			private set;
			get;
		}

		public GbsElementMitBescriftung ButtonReadDetails
		{
			private set;
			get;
		}

		public GbsElementMitBescriftung ButtonSctartConversation
		{
			private set;
			get;
		}

		public UtilmenuMissionLocationInfo[] MengeLocation
		{
			private set;
			get;
		}

		public string MissionTitelText
		{
			get
			{
				var Header = this.Header;

				if (null == Header)
				{
					return null;
				}

				return Header.Bescriftung;
			}
		}

		public UtilmenuMissionInfo()
		{
		}

		public UtilmenuMissionInfo(
			GbsElement GbsAst,
			GbsElementMitBescriftung Header,
			GbsElementMitBescriftung ButtonReadDetails,
			GbsElementMitBescriftung ButtonSctartConversation,
			UtilmenuMissionLocationInfo[] MengeLocation)
			:
			base(GbsAst)
		{
			this.Header = Header;
			this.ButtonReadDetails = ButtonReadDetails;
			this.ButtonSctartConversation = ButtonSctartConversation;
			this.MengeLocation = MengeLocation;
		}
	}


	public class ShipUiTargetAssignedGrupe
	{
		readonly public bool? IstDrone;

		readonly public GbsElement IconTexture;

		public Int64? IconTextureIdent
		{
			get
			{
				return IconTexture.IdentNullable();
			}
		}

		public ShipUiTargetAssignedGrupe()
		{
		}

		public ShipUiTargetAssignedGrupe(
			bool? IstDrone,
			GbsElement IconTexture)
		{
			this.IstDrone = IstDrone;
			this.IconTexture = IconTexture;
		}
	}


	public class InventoryItem : GbsElement
	{
		readonly public ListItem ListItem;

		public string Name
		{
			set;
			get;
		}

		public string QuantitySictStringAbbild
		{
			set;
			get;
		}

		public int? Quantity
		{
			set;
			get;
		}

		public string Group
		{
			set;
			get;
		}

		public string Category
		{
			set;
			get;
		}

		public InventoryItem(
			ListItem ListItem,
			string Name = null,
			string QuantitySictStringAbbild = null,
			int? Quantity = null,
			string Group = null,
			string Category = null)
			:
			base(ListItem)
		{
			this.ListItem = ListItem;

			this.Name = Name;
			this.QuantitySictStringAbbild = QuantitySictStringAbbild;
			this.Quantity = Quantity;
			this.Group = Group;
			this.Category = Category;
		}

		public InventoryItem()
			:
			this(null)
		{
		}

		override public string ToString()
		{
			return JsonConvert.SerializeObject(this, Formatting.Indented);
		}

		static public bool HinraicendGlaicwertig(
			InventoryItem O0,
			InventoryItem O1)
		{
			if (O0 == O1)
			{
				return true;
			}

			if (null == O0 || null == O1)
			{
				return false;
			}

			if (!(O0.Ident == O1.Ident))
			{
				return false;
			}

			var O0ListItem = O0.ListItem;
			var O1ListItem = O1.ListItem;

			var O0ListItemListeZeleText = (null == O0ListItem) ? null : O0ListItem.ListeZeleText;
			var O1ListItemListeZeleText = (null == O1ListItem) ? null : O1ListItem.ListeZeleText;

			if (!Bib3.Glob.SequenceEqualPerObjectEquals(O0ListItemListeZeleText, O1ListItemListeZeleText))
			{
				return false;
			}

			return true;
		}
	}


	public class TreeViewEntry : GbsElement
	{
		public GbsElement TopContFläce
		{
			private set;
			get;
		}

		public GbsElement ExpandCollapseToggleFläce
		{
			private set;
			get;
		}

		public bool? IsSelected;

		readonly public GbsElementMitBescriftung TopContLabel;

		public FarbeARGB TopContIconColor
		{
			private set;
			get;
		}

		public string LabelText
		{
			private set;
			get;
		}

		public string LabelTextTailObjektName
		{
			set;
			get;
		}

		public string LabelTextTailObjektDistance
		{
			set;
			get;
		}

		public Int64? ObjektDistance
		{
			set;
			get;
		}

		public Int64? TopContIconTyp
		{
			set;
			get;
		}

		public TreeViewEntry[] MengeChild
		{
			private set;
			get;
		}

		public TreeViewEntry()
		{
		}

		public TreeViewEntry(
			GbsElement ZuKopiire,
			GbsElement TopContFläce,
			GbsElementMitBescriftung TopContLabel,
			Int64? TopContIconTyp,
			FarbeARGB TopContIconColor,
			string LabelText,
			TreeViewEntry[] MengeChild,
			GbsElement ExpandCollapseToggleFläce)
			:
			base(ZuKopiire)
		{
			this.TopContFläce = TopContFläce;
			this.TopContLabel = TopContLabel;
			this.TopContIconTyp = TopContIconTyp;
			this.TopContIconColor = TopContIconColor;
			this.LabelText = LabelText;
			this.MengeChild = MengeChild;
			this.ExpandCollapseToggleFläce = ExpandCollapseToggleFläce;
		}
	}


	public class List : GbsElement
	{
		public ListColumnHeader[] ListeHeader;

		public ListItem[] ListeItem;

		public List()
			:
			this((List)null)
		{
		}

		public List(
			GbsElement ZuKopiire)
			:
			base(ZuKopiire)
		{
		}

		public List(List ZuKopiire)
			:
			base(ZuKopiire)
		{
			if (null != ZuKopiire)
			{
				ListeHeader = ZuKopiire.ListeHeader;
				ListeItem = ZuKopiire.ListeItem;
			}
		}

		override public string ToString()
		{
			return JsonConvert.SerializeObject(this, Formatting.Indented);
		}
	}


	public class ScnapscusListGrouped : GbsElement
	{
		readonly public ListColumnHeader[] ListeHeader;

		readonly public ScnapscusListEntry[] ListeEntry;

		readonly public Scroll Scroll;

		public Int64? ScrollHandleAntailAnGesamtMili
		{
			get
			{
				var Scroll = this.Scroll;

				if (null == Scroll)
				{
					return null;
				}

				return Scroll.ScrollHandleAntailAnGesamtMili;
			}
		}

		public ScnapscusListGrouped()
			:
			this((ScnapscusListGrouped)null)
		{
		}

		public ScnapscusListGrouped(
			GbsElement ZuKopiire,
			ListColumnHeader[] ListeHeader = null,
			ScnapscusListEntry[] ListeEntry = null,
			Scroll Scroll = null)
			:
			base(ZuKopiire)
		{
			this.ListeHeader = ListeHeader;
			this.ListeEntry = ListeEntry;
			this.Scroll = Scroll;
		}

		public ScnapscusListGrouped(ScnapscusListGrouped ZuKopiire)
			:
			this(ZuKopiire,
			(null == ZuKopiire) ? null : ZuKopiire.ListeHeader,
			(null == ZuKopiire) ? null : ZuKopiire.ListeEntry,
			(null == ZuKopiire) ? null : ZuKopiire.Scroll)
		{
		}

		override public string ToString()
		{
			return JsonConvert.SerializeObject(this, Formatting.Indented);
		}
	}


	public class ListColumnHeader : GbsElement
	{
		readonly public GbsElementMitBescriftung Header;

		readonly public bool? Sorted;

		public string HeaderBescriftung
		{
			get
			{
				var Header = this.Header;

				if (null == Header)
				{
					return null;
				}

				return Header.Bescriftung;
			}
		}

		public ListColumnHeader()
			:
			this((ListColumnHeader)null)
		{
		}

		public ListColumnHeader(
			GbsElement ZuKopiire,
			GbsElementMitBescriftung Header = null,
			bool? Sorted = null)
			:
			base(ZuKopiire)
		{
			this.Header = Header;
			this.Sorted = Sorted;
		}

		public ListColumnHeader(
			ListColumnHeader ZuKopiire)
			:
			this(ZuKopiire,
			(null == ZuKopiire) ? null : ZuKopiire.Header,
			(null == ZuKopiire) ? null : ZuKopiire.Sorted)
		{
		}

		override public string ToString()
		{
			return JsonConvert.SerializeObject(this, Formatting.Indented);
		}
	}

	/// <summary>
	/// Kan Item oder Group abbilde.
	/// </summary>

	public class ScnapscusListEntry : GbsElement
	{
		readonly public int? InhaltGrenzeLinx;

		readonly public string Bescriftung;

		readonly public string BescriftungTailTitel;

		readonly public int? BescriftungQuantitäät;

		readonly public bool? IstGroup;

		readonly public GbsElement GroupExpander;

		readonly public GbsElement FläceMenu;

		/*
		 * 2015.02.26
		 * 
		/// <summary>
		/// Textur Ident des Expander Icon isc interesant da diises mit Expand/Collapse zu wexle scaint.
		/// </summary>
		readonly public GbsElement GroupExpanderTextur;

		public Int64? GroupExpanderTexturIdent
		{
			get
			{
				return GroupExpanderTextur.IdentNullable();
			}
		}
		 * */

		readonly public bool? IsExpanded;

		readonly public KeyValuePair<ScrollHeaderInfoFürItem, string>[] ListeZuHeaderZeleString;

		readonly public string Name;

		readonly public string DistanceSictString;

		readonly public Int64? VolumeMili;

		readonly public Int64? Distance;

		readonly public string QuantitySictString;

		readonly public Int64? Quantity;

		readonly public string OreTypSictString;

		readonly public OreTypSictEnum? OreTypSictEnum;

		readonly public Int64? OreVolumeMili;

		override public GbsElement FläceMenuWurzelBerecne()
		{
			return FläceMenu;
		}

		public ScnapscusListEntry()
			:
			this((ScnapscusListEntry)null)
		{
		}

		public ScnapscusListEntry(
			GbsElement ZuKopiire,
			int? InhaltGrenzeLinx = null,
			string Bescriftung = null,
			string BescriftungTailTitel = null,
			int? BescriftungQuantitäät = null,
			bool? IstGroup = null,
			GbsElement GroupExpander = null,
			bool? IsExpanded = null,
			GbsElement FläceMenu = null,
			KeyValuePair<ScrollHeaderInfoFürItem, string>[] ListeZuHeaderZeleString = null,
			string Name = null,
			string DistanceSictString = null,
			Int64? Distance = null,
			string QuantitySictString = null,
			Int64? Quantity = null,
			Int64? VolumeMili = null,
			string OreTypSictString = null,
			OreTypSictEnum? OreTypSictEnum = null,
			Int64? OreVolumeMili = null
			)
			:
			base(ZuKopiire)
		{
			this.InhaltGrenzeLinx = InhaltGrenzeLinx;

			this.Bescriftung = Bescriftung;
			this.BescriftungTailTitel = BescriftungTailTitel;
			this.BescriftungQuantitäät = BescriftungQuantitäät;

			this.IstGroup = IstGroup;
			this.GroupExpander = GroupExpander;
			this.IsExpanded = IsExpanded;

			this.FläceMenu = FläceMenu;

			this.ListeZuHeaderZeleString = ListeZuHeaderZeleString;

			this.Name = Name;

			this.DistanceSictString = DistanceSictString;
			this.Distance = Distance;

			this.QuantitySictString = QuantitySictString;
			this.Quantity = Quantity;

			this.VolumeMili = VolumeMili;

			this.OreTypSictString = OreTypSictString;
			this.OreTypSictEnum = OreTypSictEnum;
			this.OreVolumeMili = OreVolumeMili;
		}

		public ScnapscusListEntry(ScnapscusListEntry ZuKopiire)
			:
			this(
			(GbsElement)ZuKopiire,
			(null == ZuKopiire) ? null : ZuKopiire.InhaltGrenzeLinx,
			(null == ZuKopiire) ? null : ZuKopiire.Bescriftung,
			(null == ZuKopiire) ? null : ZuKopiire.BescriftungTailTitel,
			(null == ZuKopiire) ? null : ZuKopiire.BescriftungQuantitäät,
			(null == ZuKopiire) ? null : ZuKopiire.IstGroup,
			(null == ZuKopiire) ? null : ZuKopiire.GroupExpander,
			(null == ZuKopiire) ? null : ZuKopiire.IsExpanded,

			(null == ZuKopiire) ? null : ZuKopiire.FläceMenu,

			(null == ZuKopiire) ? null : ZuKopiire.ListeZuHeaderZeleString,

			(null == ZuKopiire) ? null : ZuKopiire.Name,

			(null == ZuKopiire) ? null : ZuKopiire.DistanceSictString,
			(null == ZuKopiire) ? null : ZuKopiire.Distance,

			(null == ZuKopiire) ? null : ZuKopiire.QuantitySictString,
			(null == ZuKopiire) ? null : ZuKopiire.Quantity,

			(null == ZuKopiire) ? null : ZuKopiire.VolumeMili,

			(null == ZuKopiire) ? null : ZuKopiire.OreTypSictString,
			(null == ZuKopiire) ? null : ZuKopiire.OreTypSictEnum,
			(null == ZuKopiire) ? null : ZuKopiire.OreVolumeMili
			)
		{
		}

		override public string ToString()
		{
			return JsonConvert.SerializeObject(this, Formatting.Indented);
		}
	}


	public class ListItem : GbsElement
	{
		readonly public string[] ListeZeleText;

		readonly bool? Selected;

		public ListItem()
			:
			this((ListItem)null)
		{
		}

		public ListItem(
			GbsElement ZuKopiire,
			string[] ListeZeleText = null,
			bool? Selected = null)
			:
			base(ZuKopiire)
		{
			this.ListeZeleText = ListeZeleText;
			this.Selected = Selected;
		}

		public ListItem(ListItem ZuKopiire)
			:
			base(ZuKopiire)
		{
			if (null != ZuKopiire)
			{
				var ZuKopiireListeZele = ZuKopiire.ListeZeleText;

				ListeZeleText = (null == ZuKopiireListeZele) ? null : ZuKopiireListeZele.ToArray();
			}
		}

		override public string ToString()
		{
			return JsonConvert.SerializeObject(this, Formatting.Indented);
		}
	}


	public class Inventory : GbsElement
	{
		readonly public KeyValuePair<ListColumnHeader, InventoryItemDetailsColumnTyp?>[] ListeHeaderMitColumnTyp;

		readonly public BasicDynamicScroll Scroll;

		readonly public InventoryItem[] ListeItem;

		readonly public bool? SictwaiseScaintGeseztAufListNict;

		public Inventory()
			:
			this(null)
		{
		}

		public Inventory(
			GbsElement ZuKopiire,
			KeyValuePair<ListColumnHeader, InventoryItemDetailsColumnTyp?>[] ListeHeaderMitColumnTyp = null,
			BasicDynamicScroll Scroll = null,
			InventoryItem[] ListeItem = null,
			bool? SictwaiseScaintGeseztAufListNict = null)
			:
			base(ZuKopiire)
		{
			this.ListeHeaderMitColumnTyp = ListeHeaderMitColumnTyp;
			this.Scroll = Scroll;
			this.ListeItem = ListeItem;
			this.SictwaiseScaintGeseztAufListNict = SictwaiseScaintGeseztAufListNict;
		}

		override public string ToString()
		{
			return JsonConvert.SerializeObject(this, Formatting.Indented);
		}
	}


	public class InventoryCapacityGaugeInfo
	{
		readonly public Int64? MaxMikro;

		readonly public Int64? UsedMikro;

		readonly public Int64? SelectedMikro;

		public InventoryCapacityGaugeInfo()
		{
		}

		public InventoryCapacityGaugeInfo(
			Int64? MaxMikro,
			Int64? UsedMikro,
			Int64? SelectedMikro = null)
		{
			this.MaxMikro = MaxMikro;
			this.UsedMikro = UsedMikro;
			this.SelectedMikro = SelectedMikro;
		}
	}


	public class WindowInventoryPrimary : Window
	{
		readonly public TreeViewEntry[] LinxTreeListeEntry;

		readonly public TreeViewEntry LinxTreeEntryActiveShip;

		public string AuswaalReczObjektName
		{
			get
			{
				return AuswaalReczObjektPfaadSictString;
			}
		}

		readonly public string AuswaalReczObjektPfaadSictString;

		readonly public string[] AuswaalReczObjektPfaadListeAst;

		readonly public Inventory AuswaalReczInventory;

		readonly public InventoryCapacityGaugeInfo AuswaalReczCapacity;

		public TreeViewEntry[] ZuAuswaalReczMengeKandidaatLinxTreeEntry
		{
			private set;
			get;
		}

		readonly public GbsElement[] AuswaalReczInventorySictMengeButton;

		readonly public GbsElement AuswaalReczFilterAingaabeTextZiil;

		readonly public string AuswaalReczFilterText;

		readonly public GbsElement AuswaalReczFilterButtonClear;

		readonly public int? AuswaalReczMengeItemAbgebildetAnzaal;

		readonly public int? AuswaalReczMengeItemFilteredAnzaal;

		public TreeViewEntry[] LinxTreeListeEntryMengeTransitiiv()
		{
			var LinxTreeListeEntry = this.LinxTreeListeEntry;

			if (null == LinxTreeListeEntry)
			{
				return null;
			}

			return
				Bib3.Glob.SuuceFlacMengeAstAusMengeWurzel(
				LinxTreeListeEntry,
				(t) => true,
				(Ast) => Ast.MengeChild,
				null,
				null,
				null,
				false);
		}

		public InventoryItem[] AuswaalReczListeItem
		{
			get
			{
				var AuswaalReczInventory = this.AuswaalReczInventory;

				if (null == AuswaalReczInventory)
				{
					return null;
				}

				return AuswaalReczInventory.ListeItem;
			}
		}

		public WindowInventoryPrimary()
			:
			this(null)
		{
		}

		public WindowInventoryPrimary(
			Window Window,
			TreeViewEntry[] LinxTreeListeEntry = null,
			TreeViewEntry LinxTreeEntryActiveShip = null,
			string AuswaalReczObjektPfaadSictString = null,
			string[] AuswaalReczObjektPfaadListeAst = null,
			Inventory AuswaalReczInventory = null,
			InventoryCapacityGaugeInfo AuswaalReczCapacity = null,
			GbsElement[] AuswaalReczInventorySictMengeButton = null,
			GbsElement AuswaalReczFilterAingaabeTextZiil = null,
			string AuswaalReczFilterText = null,
			GbsElement AuswaalReczFilterButtonClear = null,
			int? AuswaalReczMengeItemAbgebildetAnzaal = null,
			int? AuswaalReczMengeItemFilteredAnzaal = null)
			:
			base(Window)
		{
			this.LinxTreeListeEntry = LinxTreeListeEntry;
			this.LinxTreeEntryActiveShip = LinxTreeEntryActiveShip;
			this.AuswaalReczObjektPfaadSictString = AuswaalReczObjektPfaadSictString;
			this.AuswaalReczObjektPfaadListeAst = AuswaalReczObjektPfaadListeAst;
			this.AuswaalReczInventory = AuswaalReczInventory;
			this.AuswaalReczCapacity = AuswaalReczCapacity;
			this.AuswaalReczInventorySictMengeButton = AuswaalReczInventorySictMengeButton;
			this.AuswaalReczFilterAingaabeTextZiil = AuswaalReczFilterAingaabeTextZiil;
			this.AuswaalReczFilterText = AuswaalReczFilterText;
			this.AuswaalReczFilterButtonClear = AuswaalReczFilterButtonClear;
			this.AuswaalReczMengeItemAbgebildetAnzaal = AuswaalReczMengeItemAbgebildetAnzaal;
			this.AuswaalReczMengeItemFilteredAnzaal = AuswaalReczMengeItemFilteredAnzaal;
		}

		override public string ToString()
		{
			return JsonConvert.SerializeObject(this, Formatting.Indented);
		}

		static public TreeViewEntry[] ZuAuswaalReczBerecneMengeKandidaatLinxTreeEntry(
			string[] AuswaalReczObjektPfaadListeAst,
			TreeViewEntry[] LinxTreeListeEntry)
		{
			if (null == AuswaalReczObjektPfaadListeAst)
			{
				return null;
			}

			var AuswaalReczObjektPfaadListeAstFrüheste =
				AuswaalReczObjektPfaadListeAst.FirstOrDefault();

			if (null == AuswaalReczObjektPfaadListeAstFrüheste)
			{
				return null;
			}

			var LinxTreeListeEntryPasend =
				ExtractFromOldAssembly.Bib3.Extension.WhereNullable(
				LinxTreeListeEntry,
				(TreeEntry) => string.Equals(TreeEntry.LabelTextTailObjektName, AuswaalReczObjektPfaadListeAstFrüheste, StringComparison.InvariantCultureIgnoreCase))
				.ToArrayNullable();

			if (null == LinxTreeListeEntryPasend)
			{
				return null;
			}

			if (AuswaalReczObjektPfaadListeAst.Length < 2)
			{
				return LinxTreeListeEntryPasend;
			}

			return
				Bib3.Glob.ListeEnumerableAgregiirt(
				LinxTreeListeEntryPasend
				.Select((LinxTreeEntryPasend) =>
					ZuAuswaalReczBerecneMengeKandidaatLinxTreeEntry(
					AuswaalReczObjektPfaadListeAst.Skip(1).ToArray(),
					LinxTreeEntryPasend.MengeChild))
				.Where((Kandidaat) => null != Kandidaat))
				.ToArrayNullable();

			/*
			 * !!!	Scpääter eventuel entscaidung aufgrund von Hintergrundfarbe von TreeViewEntry mööglic.
			 * Diise wurde noc nict in GBS Baum gefunde, vermuutlic werd diise in andere DictEntry gescpaicert.
			 * */
		}

		public void ZuAuswaalReczMengeKandidaatLinxTreeEntryBerecne()
		{
			{
				//	2015.09.01

				this.ZuAuswaalReczMengeKandidaatLinxTreeEntry = LinxTreeListeEntryMengeTransitiiv()?.Where(k => k.IsSelected ?? false)?.ToArray();
				return;
			}

			this.ZuAuswaalReczMengeKandidaatLinxTreeEntry =
				ZuAuswaalReczBerecneMengeKandidaatLinxTreeEntry(AuswaalReczObjektPfaadListeAst, LinxTreeListeEntryMengeTransitiiv());
		}
	}


	public class ShipUiTarget : GbsElement
	{
		/*
		 * 2015.02.17
		 * 
		/// <summary>
		/// Aus Sensoorik Python Objekt TargetInBar Adrese.
		/// </summary>
				public Int64? TargetInBarHerkunftAdrese
		{
			private set;
			get;
		}
		 * */

		readonly public ShipHitpointsAndEnergy Treferpunkte;

		readonly public string[] ÜberDistanceListeZaile;

		readonly public Int64? DistanceScrankeMin;

		readonly public Int64? DistanceScrankeMax;

		readonly public bool? Active;

		readonly public ShipUiTargetAssignedGrupe[] MengeAssignedGrupe;

		override public GbsElement FläceMenuWurzelBerecne()
		{
			return GbsObjektInputFookusSeze ?? this;
		}

		/// <summary>
		/// GBS Objekt welces zum seze des Input Fookus auf Target angeklikt werde sol.
		/// </summary>
		readonly public GbsElement GbsObjektInputFookusSeze;

		public ShipUiTarget()
			:
			this(null)
		{
		}

		public ShipUiTarget(
			GbsElement GbsAst,
			ShipHitpointsAndEnergy Treferpunkte = null,
			string[] ÜberDistanceListeZaile = null,
			Int64? DistanceScrankeMin = null,
			Int64? DistanceScrankeMax = null,
			bool? Active = null,
			ShipUiTargetAssignedGrupe[] MengeAssignedGrupe = null,
			GbsElement GbsObjektInputFookusSeze = null)
			:
			base(GbsAst)
		{
			/*
			 * 2015.02.17
			 * 
			this.TargetInBarHerkunftAdrese = (null == GbsAst) ? null : GbsAst.Ident;
			 * */

			this.Treferpunkte = Treferpunkte;
			this.ÜberDistanceListeZaile = ÜberDistanceListeZaile;
			this.DistanceScrankeMin = DistanceScrankeMin;
			this.DistanceScrankeMax = DistanceScrankeMax;
			this.Active = Active;
			this.MengeAssignedGrupe = MengeAssignedGrupe;
			this.GbsObjektInputFookusSeze = GbsObjektInputFookusSeze;
		}

		override public string ToString()
		{
			return JsonConvert.SerializeObject(this, Formatting.Indented);
		}
	}


	public class ShipUiTimer
	{
		readonly public string[] TextListeZaile;

		readonly public string DauerLabelText;

		readonly public int? DauerRestMili;

		readonly public SictEWarTypeEnum? EWarTypSictEnum;

		public ShipUiTimer()
		{
		}

		public ShipUiTimer(
			string[] TextListeZaile = null,
			string DauerLabelText = null,
			int? DauerRestMili = null,
			SictEWarTypeEnum? EWarTypSictEnum = null)
		{
			this.TextListeZaile = TextListeZaile;
			this.DauerLabelText = DauerLabelText;
			this.DauerRestMili = DauerRestMili;
			this.EWarTypSictEnum = EWarTypSictEnum;
		}
	}


	public class ShipUiEWarElement
	{
		readonly public string EWarTypeString;

		[JsonConverter(typeof(StringEnumConverter))]
		readonly public SictEWarTypeEnum? EWarTypeEnum;

		readonly public GbsElement IconTexture;

		public Int64? IconTextureIdent
		{
			get
			{
				return IconTexture.IdentNullable();
			}
		}

		public ShipUiEWarElement()
		{
		}

		public ShipUiEWarElement(
			string EWarTypeString,
			SictEWarTypeEnum? EWarTypeEnum,
			GbsElement IconTexture)
		{
			this.EWarTypeString = EWarTypeString;
			this.EWarTypeEnum = EWarTypeEnum;
			this.IconTexture = IconTexture;
		}
	}


	public class ShipUiIndication : GbsElement
	{
		readonly public string IndicationCaption;

		readonly public string IndicationText;

		public ShipUiIndication()
		{
		}

		public ShipUiIndication(
			GbsElement GbsAst,
			string IndicationCaption,
			string IndicationText)
			:
			base(GbsAst)
		{
			this.IndicationCaption = IndicationCaption;
			this.IndicationText = IndicationText;
		}

		static public bool HinraicendGlaicwertigFürFortsaz(
			ShipUiIndication O0,
			ShipUiIndication O1)
		{
			if (O0 == O1)
			{
				return true;
			}

			if (null == O0 || null == O1)
			{
				return false;
			}

			return
				string.Equals(O0.IndicationCaption, O1.IndicationCaption) &&
				string.Equals(O0.IndicationText, O1.IndicationText);
		}
	}


	public class ShipUi : GbsElement, ICloneable
	{
		readonly public ShipUiIndication Indication;

		readonly public ShipUiEWarElement[] MengeEWarElement;

		readonly public ShipUiTimer[] MengeTimer;

		readonly public GbsElement ButtonStop;

		readonly public ShipUiModule[] MengeModule;

		public IEnumerable<int?> MengeTimerDauerRestMiliBerecne()
		{
			var MengeTimer = this.MengeTimer;

			if (null == MengeTimer)
			{
				return null;
			}

			return MengeTimer.Select((TimerErgeebnis) => (null == TimerErgeebnis) ? null : TimerErgeebnis.DauerRestMili);
		}

		public int? MengeTimerDauerRestMinimumMiliBerecne()
		{
			return Bib3.Glob.Min(MengeTimerDauerRestMiliBerecne());
		}

		public int? MengeTimerDauerRestMaximumMiliBerecne()
		{
			return Bib3.Glob.Max(MengeTimerDauerRestMiliBerecne());
		}

		public ShipUi()
		{
		}

		public ShipUi(
			GbsElement ZuKopiire,
			ShipUiIndication Indication,
			ShipUiEWarElement[] MengeEWarElement,
			ShipUiTimer[] MengeTimer,
			GbsElement ButtonStop,
			ShipUiModule[] MengeModule)
			:
			base(ZuKopiire)
		{
			this.Indication = Indication;
			this.MengeEWarElement = MengeEWarElement;
			this.MengeTimer = MengeTimer;
			this.ButtonStop = ButtonStop;
			this.MengeModule = MengeModule;
		}

		/*
		 * 2015.01.18
		 * 
		public void SezeTailwerte(ShipZuusctand ErsazSelbstShipZuusctand)
		{
			if (null == ErsazSelbstShipZuusctand)
			{
				return;
			}

			var SelbstShipZuusctand = this.SelbstShipZuusctand;

			if (null == SelbstShipZuusctand)
			{
			}
			else
			{
				var ErsazSelbsctScifZuusctandTreferpunkte = ErsazSelbstShipZuusctand.Treferpunkte;

				if (null != ErsazSelbsctScifZuusctandTreferpunkte)
				{
					SelbstShipZuusctand = SelbstShipZuusctand.KopiiMitErsazTreferpunkte(ErsazSelbsctScifZuusctandTreferpunkte);
				}
			}

			this.SelbstShipZuusctand = SelbstShipZuusctand;
		}

		public void SezeTailwerte(SictAusGbsShipUi Ersaz)
		{
			if (null == Ersaz)
			{
				return;
			}

			var ErsazSelbstShipZuusctand = Ersaz.SelbstShipZuusctand;

			if (null != ErsazSelbstShipZuusctand)
			{
				SezeTailwerte(ErsazSelbstShipZuusctand);
			}
		}
		 * */

		public ShipUi Kopii()
		{
			return SictRefBaumKopii.ObjektKopiiErsctele(this);
		}

		public object Clone()
		{
			return Kopii();
		}
	}



	public class MenuEntry : GbsElementMitBescriftung
	{
		readonly public bool? EnthältAndere;

		readonly public bool? Highlight;

		public MenuEntry()
			:
			this(null)
		{
		}

		public MenuEntry(MenuEntry ZuKopiire)
			:
			this(
			ZuKopiire,
			(null == ZuKopiire) ? null : ZuKopiire.EnthältAndere,
			(null == ZuKopiire) ? null : ZuKopiire.Highlight)
		{
		}

		public MenuEntry(
			GbsElementMitBescriftung Baasis,
			bool? EnthältAndere = null,
			bool? Highlight = null)
			:
			base(Baasis)
		{
			this.EnthältAndere = EnthältAndere;
			this.Highlight = Highlight;
		}

		override public string ToString()
		{
			return JsonConvert.SerializeObject(this, Formatting.Indented);
		}

		static public bool MengeEntryEnthaltEntryMitBescriftung(
			IEnumerable<MenuEntry> MengeEntry,
			string Bescriftung,
			RegexOptions RegexOptions = RegexOptions.None)
		{
			if (null == Bescriftung)
			{
				return false;
			}

			return MengeEntryEnthaltEntryMitBescriftungRegexPattern(
				MengeEntry,
				"^" + Regex.Escape(Bescriftung) + "$",
				RegexOptions);
		}

		static public bool MengeEntryEnthaltEntryMitBescriftungRegexPattern(
			IEnumerable<MenuEntry> MengeEntry,
			string RegexPattern,
			RegexOptions RegexOptions = RegexOptions.None)
		{
			if (null == MengeEntry)
			{
				return false;
			}

			if (null == RegexPattern)
			{
				return false;
			}

			return MengeEntry.Any((Entry) => EntryBescriftungPasendZuRegexPattern(Entry, RegexPattern, RegexOptions));
		}

		static public bool EntryBescriftungPasendZuRegexPattern(
			MenuEntry KandidaatEntry,
			string RegexPattern,
			RegexOptions RegexOptions = RegexOptions.None)
		{
			if (null == KandidaatEntry)
			{
				return false;
			}

			var KandidaatEntryBescriftung = KandidaatEntry.Bescriftung;

			if (null == KandidaatEntryBescriftung)
			{
				return false;
			}

			return Regex.Match(KandidaatEntryBescriftung, RegexPattern, RegexOptions).Success;
		}

	}


	public class Scroll : GbsElement
	{
		readonly public ListColumnHeader[] ListeColumnHeader;

		readonly public GbsElement Clipper;

		readonly public GbsElement ScrollHandleGrenze;

		readonly public GbsElement ScrollHandle;

		public int? ScrollHandleFläceGrenzeOobnAntailAnGesamtMili
		{
			private set;
			get;
		}

		public int? ScrollHandleFläceGrenzeUntnAntailAnGesamtMili
		{
			private set;
			get;
		}

		public int? ScrollHandleAntailAnGesamtMili
		{
			private set;
			get;
		}

		public OrtogoonInt? ScrollHandleGrenzeFläce
		{
			get
			{
				return ScrollHandleGrenze.InGbsFläceNullable();
			}
		}

		public OrtogoonInt? ScrollHandleFläce
		{
			get
			{
				return ScrollHandle.InGbsFläceNullable();
			}
		}

		public OrtogoonInt? ClipperFläce
		{
			get
			{
				return Clipper.InGbsFläceNullable();
			}
		}

		void AbgelaiteteBerecne()
		{
			int? ScrollHandleAntailAnGesamtMili = null;
			int? ScrollHandleFläceGrenzeOobenAntailAnGesamtMili = null;
			int? ScrollHandleFläceGrenzeUntenAntailAnGesamtMili = null;

			try
			{
				var ScrollHandleGrenzeFläce = this.ScrollHandleGrenzeFläce;
				var ScrollHandleFläce = this.ScrollHandleFläce;

				if (!ScrollHandleGrenzeFläce.HasValue)
				{
					return;
				}

				if (!ScrollHandleFläce.HasValue)
				{
					return;
				}

				if (ScrollHandleGrenzeFläce.Value.Grööse.B < 1)
				{
					return;
				}

				ScrollHandleAntailAnGesamtMili = (int)((ScrollHandleFläce.Value.Grööse.B * 1000) / ScrollHandleGrenzeFläce.Value.Grööse.B);

				/*
				 * 2015.03.20
				 * Korektur
				 * 
				ScrollHandleFläceGrenzeOobenAntailAnGesamtMili =
					(int)(((ScrollHandleGrenzeFläce.Value.PunktMin.B - ScrollHandleGrenzeFläce.Value.PunktMin.B) * 1000) / ScrollHandleGrenzeFläce.Value.Grööse.B);

				ScrollHandleFläceGrenzeUntenAntailAnGesamtMili =
					(int)(((ScrollHandleGrenzeFläce.Value.PunktMax.B - ScrollHandleGrenzeFläce.Value.PunktMin.B) * 1000) / ScrollHandleGrenzeFläce.Value.Grööse.B);
				 * */
				ScrollHandleFläceGrenzeOobenAntailAnGesamtMili =
					(int)(((ScrollHandleFläce.Value.PunktMin.B - ScrollHandleGrenzeFläce.Value.PunktMin.B) * 1000) / ScrollHandleGrenzeFläce.Value.Grööse.B);

				ScrollHandleFläceGrenzeUntenAntailAnGesamtMili =
					(int)(((ScrollHandleFläce.Value.PunktMax.B - ScrollHandleGrenzeFläce.Value.PunktMin.B) * 1000) / ScrollHandleGrenzeFläce.Value.Grööse.B);
			}
			finally
			{
				this.ScrollHandleAntailAnGesamtMili = ScrollHandleAntailAnGesamtMili;
				this.ScrollHandleFläceGrenzeOobnAntailAnGesamtMili = ScrollHandleFläceGrenzeOobenAntailAnGesamtMili;
				this.ScrollHandleFläceGrenzeUntnAntailAnGesamtMili = ScrollHandleFläceGrenzeUntenAntailAnGesamtMili;
			}
		}

		public Scroll()
			:
			this((Scroll)null)
		{
		}

		public Scroll(
			GbsElement GbsAst,
			ListColumnHeader[] ListeColumnHeader = null,
			GbsElement Clipper = null,
			GbsElement ScrollHandleGrenze = null,
			GbsElement ScrollHandle = null)
			:
			base(GbsAst)
		{
			this.ListeColumnHeader = ListeColumnHeader;
			this.Clipper = Clipper;
			this.ScrollHandleGrenze = ScrollHandleGrenze;
			this.ScrollHandle = ScrollHandle;

			AbgelaiteteBerecne();
		}

		public Scroll(Scroll ZuKopiire)
			:
			this(ZuKopiire,
			(null == ZuKopiire) ? null : ZuKopiire.ListeColumnHeader,
			(null == ZuKopiire) ? null : ZuKopiire.Clipper,
			(null == ZuKopiire) ? null : ZuKopiire.ScrollHandleGrenze,
			(null == ZuKopiire) ? null : ZuKopiire.ScrollHandle)
		{
		}

		override public string ToString()
		{
			return JsonConvert.SerializeObject(this, Formatting.Indented);
		}
	}


	public class BasicDynamicScroll : Scroll
	{
		public BasicDynamicScroll(Scroll ZuKopiire)
			:
			base(ZuKopiire)
		{
		}

		public BasicDynamicScroll()
			:
			this(null)
		{
		}

		override public string ToString()
		{
			return JsonConvert.SerializeObject(this, Formatting.Indented);
		}
	}


	public class EveButtonGroup : GbsElement
	{
		readonly public GbsElementMitBescriftung[] MengeButton;

		public EveButtonGroup(
			GbsElement GbsAst,
			IEnumerable<GbsElementMitBescriftung> MengeButton = null)
			:
			base(GbsAst)
		{
			this.MengeButton = MengeButton.ToArrayNullable();
		}

		public EveButtonGroup()
			:
			this(null)
		{
		}

		override public string ToString()
		{
			return JsonConvert.SerializeObject(this, Formatting.Indented);
		}
	}


	public class Window : GbsElement
	{
		readonly public bool? isModal;

		readonly public string Caption;

		public string HeaderCaptionText;

		public bool? Sictbar;

		public OrtogoonInt? HeaderButtonsFläce;

		public bool? HeaderButtonsSictbar;

		public GbsElement HeaderButtonClose;

		public GbsElement HeaderButtonMinimize;

		public GbsElement HeaderIcon;

		virtual public ScnapscusListGrouped ListHaupt()
		{
			return null;
		}

		public Window()
		{
		}

		public Window(
			GbsElement ZuKopiire,
			bool? isModal = null,
			string Caption = null)
			:
			base(ZuKopiire)
		{
			if (null != ZuKopiire)
			{
			}

			this.isModal = isModal;
			this.Caption = Caption;
		}

		public Window(Window ZuKopiire)
			:
			this((GbsElement)ZuKopiire)
		{
			if (null != ZuKopiire)
			{
				HeaderCaptionText = ZuKopiire.HeaderCaptionText;
				Sictbar = ZuKopiire.Sictbar;
				HeaderButtonsSictbar = ZuKopiire.HeaderButtonsSictbar;
				HeaderButtonsFläce = ZuKopiire.HeaderButtonsFläce;
				HeaderButtonClose = ZuKopiire.HeaderButtonClose;
				HeaderButtonMinimize = ZuKopiire.HeaderButtonMinimize;
				HeaderIcon = ZuKopiire.HeaderIcon;
				isModal = ZuKopiire.isModal;
				Caption = ZuKopiire.Caption;
			}
		}

		override public string ToString()
		{
			return JsonConvert.SerializeObject(this, Formatting.Indented);
		}
	}


	public class InfoPanel : GbsElement
	{
		readonly public bool? MainContSictbar;

		readonly public GbsElement HeaderButtonExpand;

		public bool? Expanded
		{
			get
			{
				return MainContSictbar;
			}
		}

		public InfoPanel()
		{
		}

		public InfoPanel(GbsElement ZuKopiire)
			:
			base(ZuKopiire)
		{
		}

		public InfoPanel(InfoPanel ZuKopiire)
			:
			this((GbsElement)ZuKopiire)
		{
			if (null != ZuKopiire)
			{
				MainContSictbar = ZuKopiire.MainContSictbar;
				HeaderButtonExpand = ZuKopiire.HeaderButtonExpand;
			}
		}

		public InfoPanel(
			GbsElement GbsAst,
			bool? MainContSictbar,
			GbsElement HeaderButtonExpand)
			:
			base(GbsAst)
		{
			this.MainContSictbar = MainContSictbar;
			this.HeaderButtonExpand = HeaderButtonExpand;
		}

		override public string ToString()
		{
			return JsonConvert.SerializeObject(this, Formatting.Indented);
		}
	}


	public class InfoPanelMissions : InfoPanel
	{
		public InfoPanelMissionsMission[] ListeMissionButton;

		public InfoPanelMissions()
		{
		}

		public InfoPanelMissions(InfoPanel ZuKopiire)
			:
			base(ZuKopiire)
		{
		}
	}


	public class InfoPanelLocationInfo : InfoPanel
	{
		readonly public SictAusGbsLocationInfo CurrentLocationInfo;

		readonly public GbsElement ButtonListSurroundings;

		readonly public bool? LanguageNotSetToEnglish;

		override public string ToString()
		{
			return JsonConvert.SerializeObject(this, Formatting.Indented);
		}

		public InfoPanelLocationInfo()
		{
		}

		public InfoPanelLocationInfo(
			InfoPanel ZuKopiire,
			SictAusGbsLocationInfo CurrentLocationInfo = null,
			GbsElement ButtonListSurroundings = null,
			bool? LanguageNotSetToEnglish = null)
			:
			base(ZuKopiire)
		{
			this.CurrentLocationInfo = CurrentLocationInfo;
			this.ButtonListSurroundings = ButtonListSurroundings;
			this.LanguageNotSetToEnglish = LanguageNotSetToEnglish;
		}
	}


	public class InfoPanelRoute : InfoPanel
	{
		readonly public SictAusGbsLocationInfo CurrentInfo;

		readonly public SictAusGbsLocationInfo EndInfo;

		readonly public bool? NoDestination;

		readonly public GbsElement[] MengeMarker;

		override public string ToString()
		{
			return JsonConvert.SerializeObject(this, Formatting.Indented);
		}

		public InfoPanelRoute()
		{
		}

		public InfoPanelRoute(
			InfoPanel ZuKopiire,
			SictAusGbsLocationInfo CurrentInfo = null,
			SictAusGbsLocationInfo EndInfo = null,
			bool? NoDestination = null,
			GbsElement[] MengeMarker = null)
			:
			base(ZuKopiire)
		{
			this.CurrentInfo = CurrentInfo;
			this.EndInfo = EndInfo;
			this.NoDestination = NoDestination;
			this.MengeMarker = MengeMarker;
		}
	}


	public class WindowAgentMissionObjectiveObjective : MissionObjective
	{
		readonly public bool? AusDirektComplete;

		/// <summary>
		/// Minimum aus Menge Location.SecurityLevelMili
		/// </summary>
		readonly public int? SecurityLevelMinimumMili;

		public WindowAgentMissionObjectiveObjective()
		{
		}

		public WindowAgentMissionObjectiveObjective(
			WindowAgentMissionObjectiveObjective[] MengeObjective,
			bool? AgregatioonComplete = null,
			int? SecurityLevelMinimumMili = null)
			:
			this(null, null, null, null, MengeObjective, AgregatioonComplete: AgregatioonComplete, SecurityLevelMinimumMili: SecurityLevelMinimumMili)
		{
		}

		public WindowAgentMissionObjectiveObjective(
			bool? AusDirektComplete,
			SictMissionObjectiveObjectiveElementTyp? Typ,
			MissionLocation Location,
			string ItemName = null,
			WindowAgentMissionObjectiveObjective[] MengeObjective = null,
			//	2015.02.20	string TypSictString = null,
			//	2015.02.20	string SctaatusIconTyp = null,
			bool? AgregatioonComplete = null,
			int? SecurityLevelMinimumMili = null)
			:
			base(
			AgregatioonComplete,
			Typ,
			Location,
			ItemName,
			MengeObjective: MengeObjective)
		{
			this.AusDirektComplete = AusDirektComplete;

			/*
			 * 2015.02.18
			 * 
				this.TypSictString = TypSictString;
				this.SctaatusIconTyp = SctaatusIconTyp;
			 * */

			this.SecurityLevelMinimumMili = SecurityLevelMinimumMili;
		}

		override public string ToString()
		{
			return JsonConvert.SerializeObject(this, Formatting.Indented);
		}

		public MissionObjective TailFürNuzerBerecne()
		{
			/*
			 * 2015.02.18
			 * 
			SictMissionObjective[] TailFürNuzerMengeObjective = null;

			var MengeObjective = this.MengeObjective;

			if (null != MengeObjective)
			{
				TailFürNuzerMengeObjective =
					MengeObjective.Select((Objective) => (null == Objective) ? null : Objective.TailFürNuzerBerecne()).ToArray();
			}
			 * */

			var TailFürNuzerMengeObjective =
				MengeObjective
				.OfTypeNullable<WindowAgentMissionObjectiveObjective>()
				.SelectNullable((Objective) => Objective.TailFürNuzerBerecne())
				.ToArrayNullable();

			var Ergeebnis = new MissionObjective(this, TailFürNuzerMengeObjective);

			return Ergeebnis;
		}
	}


	public class WindowAgentMissionInfo : ICloneable
	{
		/*
		 * 2015.02.07
		 * 
				public string Htmlstr;
		 * */

		readonly public string MissionTitel;

		readonly public WindowAgentMissionObjectiveObjective Objective;

		readonly public SictFactionSictEnum[] MengeFaction;

		readonly public bool? Complete;

		readonly public int? RewardIskAnzaal;

		readonly public int? RewardLpAnzaal;

		readonly public int? BonusRewardIskAnzaal;

		/*
		 * 2015.02.13
		 * 
		/// <summary>
		/// Text welcer im "Mission Journal" (Window typ "AgentBrowser") unter "Mission Briefing" enthalte isc.
		/// </summary>
				public string MissionBriefingText;
		 * */

		public WindowAgentMissionInfo()
		{
		}

		public WindowAgentMissionInfo(
			string MissionTitel,
			WindowAgentMissionObjectiveObjective Objective,
			SictFactionSictEnum[] MengeFaction,
			bool? Complete,
			int? RewardIskAnzaal,
			int? RewardLpAnzaal,
			int? BonusRewardIskAnzaal
			//	2015.02.13	,string MissionBriefingText
			)
		{
			this.MissionTitel = MissionTitel;
			this.Objective = Objective;
			this.MengeFaction = MengeFaction;
			this.Complete = Complete;
			this.RewardIskAnzaal = RewardIskAnzaal;
			this.RewardLpAnzaal = RewardLpAnzaal;
			this.BonusRewardIskAnzaal = BonusRewardIskAnzaal;
			//	2015.02.13	this.MissionBriefingText = MissionBriefingText;
		}

		public MissionInfo TailFürNuzerBerecne()
		{
			MissionObjective ErgeebnisObjective = null;

			var Objective = this.Objective;

			if (null != Objective)
			{
				ErgeebnisObjective = Objective.TailFürNuzerBerecne();
			}

			var Ergeebnis = new MissionInfo(
				this.MissionTitel,
				Objective: ErgeebnisObjective,
				RewardIskAnzaal: this.RewardIskAnzaal,
				RewardLpAnzaal: this.RewardLpAnzaal,
				BonusRewardIskAnzaal: this.BonusRewardIskAnzaal);

			return Ergeebnis;
		}

		public MissionObjective[] MengeObjectiveElementTypLocationBerecne()
		{
			var Objective = this.Objective;

			if (null == Objective)
			{
				return null;
			}

			var MengeObjective = Objective.MengeObjectiveTransitiveHüleBerecne();

			return
				MengeObjective
				.Where((Kandidaat) =>
					(null == Kandidaat) ? false :
					(SictMissionObjectiveObjectiveElementTyp.Location == Kandidaat.Typ ||
					SictMissionObjectiveObjectiveElementTyp.LocationDropOff == Kandidaat.Typ ||
					SictMissionObjectiveObjectiveElementTyp.LocationPickUp == Kandidaat.Typ))
				.ToArray();
		}

		override public string ToString()
		{
			return JsonConvert.SerializeObject(this, Formatting.Indented);
		}

		public bool? CompleteBisAufLocationDropOffBerecne()
		{
			var Objective = this.Objective;

			if (null == Objective)
			{
				return null;
			}

			return Objective.MengeObjectiveCompleteBisAufLocationDropOffBerecne();
		}

		public WindowAgentMissionInfo Kopii()
		{
			/*
			 * 2015.02.18
			 * 
			return Bib3.RefNezDiferenz.Extension.JsonConvertKopii(this);
			 * */

			return Bib3.RefNezDiferenz.Extension.ObjektKopiiKonstrukt(this);
		}

		public object Clone()
		{
			return Kopii();
		}

		static public bool? CompleteBisAufLocationDropOffBerecne(WindowAgentMissionObjectiveObjective[] MengeObjective)
		{
			if (null == MengeObjective)
			{
				return null;
			}

			var Complete =
				Optimat.Glob.MengeBoolNulbarKonjunkt(
				MengeObjective
				.Select((Objective) =>
				{
					if (null == Objective)
					{
						return (bool?)null;
					}

					return Objective.MengeObjectiveCompleteBisAufLocationDropOffBerecne();
				}));

			return Complete;
		}
	}


	public class WindowAgent : Window
	{
		readonly public string AgentName;

		readonly public MissionLocation AgentLocation;

		readonly public WindowAgentMissionInfo ZusamefasungMissionInfo;

		public WindowAgent()
		{
		}

		public WindowAgent(
			Window Window,
			string AgentName,
			MissionLocation AgentLocation,
			WindowAgentMissionInfo ZusamefasungMissionInfo)
			:
			base(Window)
		{
			this.AgentName = AgentName;
			this.AgentLocation = AgentLocation;
			this.ZusamefasungMissionInfo = ZusamefasungMissionInfo;
		}

		public WindowAgent(
			WindowAgent ZuKopiire,
			string AgentName = null,
			MissionLocation AgentLocation = null,
			WindowAgentMissionInfo ZusamefasungMissionInfo = null)
			:
			this(
			(Window)ZuKopiire,
			AgentName ?? ((null == ZuKopiire) ? null : ZuKopiire.AgentName),
			AgentLocation ?? ((null == ZuKopiire) ? null : ZuKopiire.AgentLocation),
			ZusamefasungMissionInfo ?? ((null == ZuKopiire) ? null : ZuKopiire.ZusamefasungMissionInfo))
		{
		}

		override public string ToString()
		{
			return JsonConvert.SerializeObject(this, Formatting.Indented);
		}
	}


	public class WindowAgentBrowser : WindowAgent
	{
		public WindowAgentBrowser(WindowAgent Window)
			:
			base(Window)
		{
		}

		public WindowAgentBrowser()
		{
		}

		override public string ToString()
		{
			return JsonConvert.SerializeObject(this, Formatting.Indented);
		}
	}


	public class WindowAgentDialogue : WindowAgent
	{
		/*
		 * 2015.02.20
		 * 
		readonly public SictAusGbsWindowAgentMissionInfo LeftPaneMissionInfo;

		readonly public SictAusGbsWindowAgentMissionInfo RightPaneMissionInfo;
		 * */

		readonly public GbsElementMitBescriftung ButtonRequestMission;

		readonly public GbsElementMitBescriftung ButtonViewMission;

		readonly public GbsElementMitBescriftung ButtonAccept;

		readonly public GbsElementMitBescriftung ButtonDecline;

		readonly public GbsElementMitBescriftung ButtonComplete;

		readonly public GbsElementMitBescriftung ButtonQuit;

		readonly public bool? IstOffer;

		readonly public bool? IstAccepted;

		/// <summary>
		/// Decline oone Standing Loss mööglic.
		/// </summary>
		readonly public bool? DeclineOoneStandingLossFraigaabe;

		/// <summary>
		/// Zait in Sekunden bis Decline oone Standing Loss mööglic.
		/// </summary>
		readonly public int? DeclineWartezait;

		public WindowAgentDialogue()
		{
		}

		public WindowAgentDialogue(
			WindowAgent Window,
			string AgentName,
			MissionLocation AgentLocation,
			WindowAgentMissionInfo ZusamefasungMissionInfo,
			/*
			 * 2015.02.20
			 * 
				SictAusGbsWindowAgentMissionInfo LeftPaneMissionInfo,
				SictAusGbsWindowAgentMissionInfo RightPaneMissionInfo,
			 * */
			GbsElementMitBescriftung ButtonRequestMission,
			GbsElementMitBescriftung ButtonViewMission,
			GbsElementMitBescriftung ButtonAccept,
			GbsElementMitBescriftung ButtonDecline,
			GbsElementMitBescriftung ButtonComplete,
			GbsElementMitBescriftung ButtonQuit,

			bool? IstOffer,
			bool? IstAccepted,
			bool? DeclineOoneStandingLossFraigaabe,
			int? DeclineWartezait)
			:
			base(
			Window,
			AgentName,
			AgentLocation,
			ZusamefasungMissionInfo)
		{
			/*
			 * 2015.02.20
			 * 
				this.LeftPaneMissionInfo = LeftPaneMissionInfo;
				this.RightPaneMissionInfo = RightPaneMissionInfo;
			 * */

			this.ButtonRequestMission = ButtonRequestMission;
			this.ButtonViewMission = ButtonViewMission;
			this.ButtonAccept = ButtonAccept;
			this.ButtonDecline = ButtonDecline;
			this.ButtonComplete = ButtonComplete;
			this.ButtonQuit = ButtonQuit;

			this.IstOffer = IstOffer;
			this.IstAccepted = IstAccepted;
			this.DeclineOoneStandingLossFraigaabe = DeclineOoneStandingLossFraigaabe;
			this.DeclineWartezait = DeclineWartezait;
		}

		override public string ToString()
		{
			return JsonConvert.SerializeObject(this, Formatting.Indented);
		}
	}


	public class PanelGroup : GbsElement
	{
		public PanelGroup()
		{
		}

		public PanelGroup(GbsElement GbsAst)
			:
			base(GbsAst)
		{
		}

		override public string ToString()
		{
			return JsonConvert.SerializeObject(this, Formatting.Indented);
		}
	}


	public class Message : GbsElement
	{
		public string LabelText;

		public Message()
		{
		}

		public Message(
			GbsElement GbsAst)
			:
			base(GbsAst)
		{
		}

		override public string ToString()
		{
			return JsonConvert.SerializeObject(this, Formatting.Indented);
		}
	}

	/// <summary>
	/// Werd von mance Mission in l_main generiirt.
	/// </summary>

	public class WindowTelecom : Window
	{
		public EveButtonGroup ButtonGroup;

		public WindowTelecom(Window Window)
			:
			base(Window)
		{
		}

		public WindowTelecom()
		{
		}

		override public string ToString()
		{
			return JsonConvert.SerializeObject(this, Formatting.Indented);
		}
	}


	public class MessageBox : Window
	{
		readonly public string TopCaptionText;

		readonly public EveButtonGroup ButtonGroup;

		readonly public string MainEditText;

		public MessageBox(
			MessageBox ZuKopiire)
			:
			this(
			ZuKopiire,
			(null == ZuKopiire) ? null : ZuKopiire.TopCaptionText,
			(null == ZuKopiire) ? null : ZuKopiire.ButtonGroup,
			(null == ZuKopiire) ? null : ZuKopiire.MainEditText)
		{
		}

		public MessageBox(
			Window ZuKopiire,
			string TopCaptionText,
			EveButtonGroup ButtonGroup,
			string MainEditText)
			:
			base(ZuKopiire)
		{
			this.TopCaptionText = TopCaptionText;
			this.ButtonGroup = ButtonGroup;
			this.MainEditText = MainEditText;
		}

		public MessageBox()
		{
		}

		override public string ToString()
		{
			return JsonConvert.SerializeObject(this, Formatting.Indented);
		}
	}


	public class HybridWindow : MessageBox
	{
		public HybridWindow(
			MessageBox ZuKopiire)
			:
			base(ZuKopiire)
		{
		}

		public HybridWindow()
		{
		}

		override public string ToString()
		{
			return JsonConvert.SerializeObject(this, Formatting.Indented);
		}
	}


	public class Menu : GbsElement
	{
		readonly public MenuEntry[] ListeEntry;

		public IEnumerable<EntryScpezType> MengeEntryBerecneTailmenge<EntryScpezType>(
			Func<MenuEntry, bool> PrädikaatListeTitel,
			Func<MenuEntry, EntryScpezType> EntrySictwaise)
			where EntryScpezType : class
		{
			var ListeEntryOrdnet = this.ListeEntry;

			if (null == ListeEntryOrdnet)
			{
				return null;
			}

			if (null == PrädikaatListeTitel)
			{
				return null;
			}

			var EntryListeTitel = ListeEntryOrdnet.FirstOrDefault(PrädikaatListeTitel);

			if (null == EntryListeTitel)
			{
				return null;
			}

			var ListeEntryTailmenge = new List<EntryScpezType>();

			bool ListeEntryBegone = false;

			for (int EntryIndex = 0; EntryIndex < ListeEntryOrdnet.Length; EntryIndex++)
			{
				var Entry = ListeEntryOrdnet[EntryIndex];

				if (ListeEntryBegone)
				{
					var EntrySictScpez = (null == EntrySictwaise) ? null : EntrySictwaise(Entry);

					if (null == EntrySictScpez)
					{
						break;
					}

					ListeEntryTailmenge.Add(EntrySictScpez);
				}

				if (Entry == EntryListeTitel)
				{
					ListeEntryBegone = true;
				}
			}

			return ListeEntryTailmenge;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="GbsAst"></param>
		/// <param name="ListeEntry">Ordnet naac Hööhe in GBS Absctaigend (hööcste bai Index 0)</param>
		public Menu(
			GbsElement GbsAst,
			MenuEntry[] ListeEntry)
			:
			base(GbsAst)
		{
			this.ListeEntry = ListeEntry;
		}

		public Menu()
			:
			this(null, null)
		{
		}

		override public string ToString()
		{
			return JsonConvert.SerializeObject(this, Formatting.Indented);
		}
	}

	public class ShipUiModule : GbsElement
	{
		readonly public GbsElement ModuleButtonFlächeToggle;

		readonly public bool? ModuleButtonSictbar;

		readonly public GbsElement ModuleButtonIconTexture;

		public Int64? ModuleButtonIconTextureIdent
		{
			get
			{
				return ModuleButtonIconTexture.IdentNullable();
			}
		}

		readonly public int? ModuleButtonQuantityInt;

		/*
		 * 2015.02.18
		 * 
		readonly public SictAusShipUiModuleRamp Ramp;
		 * */
		readonly public bool RampAktiiv;

		readonly public int? RampRotatioonMili;

		readonly public bool? SpriteHiliteSictbar;

		readonly public bool? SpriteGlowSictbar;

		readonly public bool? SpriteBusySictbar;

		public ShipUiModule()
		{
		}

		public ShipUiModule(
			GbsElement GbsAst,
			GbsElement ModuleButtonFlächeToggle,
			bool? ModuleButtonSictbar,
			GbsElement ModuleButtonIconTexture,
			int? ModuleButtonQuantityInt,
			bool RampAktiiv,
			int? RampRotatioonMili,
			bool? SpriteHiliteSictbar,
			bool? SpriteGlowSictbar,
			bool? SpriteBusySictbar)
			:
			base(GbsAst)
		{
			this.ModuleButtonFlächeToggle = ModuleButtonFlächeToggle;
			this.ModuleButtonSictbar = ModuleButtonSictbar;
			this.ModuleButtonIconTexture = ModuleButtonIconTexture;
			this.ModuleButtonQuantityInt = ModuleButtonQuantityInt;

			this.RampAktiiv = RampAktiiv;
			this.RampRotatioonMili = RampRotatioonMili;

			this.SpriteHiliteSictbar = SpriteHiliteSictbar;
			this.SpriteGlowSictbar = SpriteGlowSictbar;
			this.SpriteBusySictbar = SpriteBusySictbar;
		}

		override public string ToString()
		{
			return JsonConvert.SerializeObject(this, Formatting.Indented);
		}
	}

	public enum InfoPanelTypSictEnum
	{
		Kain = 0,
		SystemInfo = 10,
		Route = 30,
		AgentMission = 40,
		Incursion = 50,
	}

	public enum OverviewZaileIconTyp
	{

	}


	public class AusOverviewZaileIcon
	{
		public OverviewZaileIconTyp Typ;

		public int Farbe;
	}


	public class OverviewZaile : GbsElement
	{
		readonly public bool? IsSelected;

		readonly public Int64? DistanceMin;

		readonly public Int64? DistanceMax;

		readonly public string Name;

		readonly public string Type;

		readonly public GbsElement IconMainTexture;

		readonly public FarbeARGB IconMainColor;

		readonly public bool? IconTargetingSictbar;

		readonly public bool? IconTargetedByMeSictbar;

		readonly public bool? IconMyActiveTargetSictbar;

		readonly public bool? IconAttackingMeSictbar;

		readonly public bool? IconHostileSictbar;

		public Int64? IconMainTextureIdent
		{
			get
			{
				return IconMainTexture.IdentNullable();
			}
		}

		/// <summary>
		/// Im "rightAlignedIconContainer" wern di EWar (Jam,WarpScramble,....) Symbole angezaigt.
		/// </summary>
		readonly public GbsElement[] RightAlignedMengeIconTexture;

		public IEnumerable<Int64> RightAlignedMengeIconTextureIdent
		{
			get
			{
				return
					RightAlignedMengeIconTexture
					.SelectNullable(Optimat.EveOnline.Extension.IdentNullable)
					.WhereNotDefault()
					.SelectNullable((t) => t ?? 0);
			}
		}

		public OverviewZaile()
			:
			this(null)
		{
		}

		public OverviewZaile(
			GbsElement GbsAst,
			bool? IsSelected = null,
			Int64? DistanceMin = null,
			Int64? DistanceMax = null,
			string Name = null,
			string Type = null,
			GbsElement IconMainTexture = null,
			FarbeARGB IconMainColor = null,
			bool? IconTargetingSictbar = null,
			bool? IconTargetedByMeSictbar = null,
			bool? IconMyActiveTargetSictbar = null,
			bool? IconAttackingMeSictbar = null,
			bool? IconHostileSictbar = null)
			:
			base(GbsAst)
		{
			this.IsSelected = IsSelected;

			this.DistanceMin = DistanceMin;
			this.DistanceMax = DistanceMax;

			this.Name = Name;
			this.Type = Type;

			this.IconMainTexture = IconMainTexture;
			this.IconMainColor = IconMainColor;

			this.IconTargetingSictbar = IconTargetingSictbar;
			this.IconTargetedByMeSictbar = IconTargetedByMeSictbar;
			this.IconMyActiveTargetSictbar = IconMyActiveTargetSictbar;
			this.IconAttackingMeSictbar = IconAttackingMeSictbar;
			this.IconHostileSictbar = IconHostileSictbar;
		}

		public bool IdentiscPerTypeUndName(
			OverviewZaile ZuVerglaicende,
			bool IgnoreCase = false)
		{
			return IdentiscPerTypeUndName(
				this,
				ZuVerglaicende,
				IgnoreCase);
		}

		static public bool IdentiscPerTypeUndName(
			OverviewZaile O0,
			OverviewZaile O1,
			bool IgnoreCase = false)
		{
			if (object.Equals(O0, O1))
			{
				return true;
			}

			if (null == O0 || null == O1)
			{
				return false;
			}

			var Comparison = IgnoreCase ? StringComparison.InvariantCultureIgnoreCase : StringComparison.InvariantCulture;

			var TypeGlaic = string.Equals(O0.Type, O1.Type, Comparison);
			var NameGlaic = string.Equals(O0.Name, O1.Name, Comparison);

			return TypeGlaic && NameGlaic;
		}

	}


	public class ModuleButtonHintZaile : GbsElementMitBescriftung
	{
		readonly public string BescriftungMiinusFormat;

		readonly public string ShortcutText;

		readonly public int? ShortcutTasteFIndex;

		readonly public bool? ShortcutModifierNict;

		readonly public GbsElement[] MengeIconTexture;

		public IEnumerable<Int64> MengeIconTextureIdent
		{
			get
			{
				return
					MengeIconTexture
					.SelectNullable(Optimat.EveOnline.Extension.IdentNullable)
					.WhereNotDefault()
					.SelectNullable((t) => t ?? 0);
			}
		}

		public ModuleButtonHintZaile()
		{
		}

		public ModuleButtonHintZaile(
			GbsElementMitBescriftung ZuKopiire,
			GbsElement[] MengeIconTexture = null,
			string BescriftungMiinusFormat = null,
			string ShortcutText = null,
			bool? ShortcutModifierNict = null,
			int? ShortcutTasteFIndex = null)
			:
			base(ZuKopiire)
		{
			this.MengeIconTexture = MengeIconTexture;

			this.BescriftungMiinusFormat = BescriftungMiinusFormat;

			this.ShortcutText = ShortcutText;

			this.ShortcutModifierNict = ShortcutModifierNict;
			this.ShortcutTasteFIndex = ShortcutTasteFIndex;
		}

	}


	public class ModuleButtonHint : GbsElement
	{
		readonly public ModuleButtonHintZaile[] ListeZaile;

		readonly public ModuleButtonHintZaile ZaileTitel;

		readonly public KeyValuePair<Int64, string>[] ZyklusMengeZuDamageTypeIconIdentText;

		public string ShortcutText
		{
			get
			{
				var ZaileTitel = this.ZaileTitel;

				if (null == ZaileTitel)
				{
					return null;
				}

				return ZaileTitel.ShortcutText;
			}
		}

		public int? ShortcutTasteFIndex
		{
			get
			{
				var ZaileTitel = this.ZaileTitel;

				if (null == ZaileTitel)
				{
					return null;
				}

				return ZaileTitel.ShortcutTasteFIndex;
			}
		}

		public bool? ShortcutModifierNict
		{
			get
			{
				var ZaileTitel = this.ZaileTitel;

				if (null == ZaileTitel)
				{
					return null;
				}

				return ZaileTitel.ShortcutModifierNict;
			}
		}

		public ModuleButtonHint()
		{
		}

		public ModuleButtonHint(GbsElement ZuKopiire)
			:
			base(ZuKopiire)
		{
		}

		public ModuleButtonHint(
			GbsElement ZuKopiire,
			ModuleButtonHintZaile[] ListeZaile,
			ModuleButtonHintZaile ZaileTitel
			)
			:
			base(ZuKopiire)
		{
			this.ListeZaile = ListeZaile;
			this.ZaileTitel = ZaileTitel;
		}

	}

	/// <summary>
	/// Bescriftungen der Spalte in "Details" oder "List" Sict.
	/// </summary>
	public enum InventoryItemDetailsColumnTyp
	{
		Kain = 0,
		Name,
		Quantity,
		Group,
		Category,
		Size,
		Slot,
		Volume,
		Meta_Level,
		Tech_Level,
	}

}