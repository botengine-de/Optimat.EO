namespace BotEngine.EveOnline.Interface.MemoryStruct
{
	public class InfoPanel : UIElement
	{
		public bool? IsExpanded;

		public UIElement HeaderButtonExpand;

		public UIElementLabelString HeaderLabel;

		/// <summary>
		/// content which is only visible when expanded.
		/// </summary>
		public UIElementLabelString[] ExpandedContentLabel;

		public InfoPanel()
		{
		}

		public InfoPanel(UIElement Base)
			:
			base(Base)
		{
		}

		public InfoPanel(InfoPanel Base)
			:
			this((UIElement)Base)
		{
			IsExpanded = Base?.IsExpanded;
			HeaderButtonExpand = Base?.HeaderButtonExpand;
			HeaderLabel = Base?.HeaderLabel;
			ExpandedContentLabel = Base?.ExpandedContentLabel;
		}
	}

	public class InfoPanelLocationInfo : InfoPanel
	{
		public UIElement ButtonListSurroundings;

		public InfoPanelLocationInfo()
		{
		}

		public InfoPanelLocationInfo(InfoPanel Base)
			:
			base(Base)
		{
		}
	}


	public class InfoPanelRoute : InfoPanel
	{
		public UIElementLabelString NextLabel;

		public UIElementLabelString DestinationLabel;

		public UIElement[] WaypointMarker;

		public InfoPanelRoute()
		{
		}

		public InfoPanelRoute(InfoPanel Base)
			:
			base(Base)
		{
		}
	}

	public class InfoPanelMissions : InfoPanel
	{
		public UIElementLabelString[] ListMissionButton;

		public InfoPanelMissions()
		{
		}

		public InfoPanelMissions(InfoPanel Base)
			:
			base(Base)
		{
		}
	}


}
