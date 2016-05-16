namespace BotEngine.EveOnline.Interface.MemoryStruct
{
	public class UtilmenuMissionLocationInfo : UIElement
	{
		public string HeaderLabel;

		public UIElementLabelString ButtonLocation;

		public UIElementLabelString ButtonDock;

		public UIElementLabelString ButtonApproach;

		public UIElementLabelString ButtonSetDestination;

		public UIElementLabelString ButtonWarpTo;

		public UtilmenuMissionLocationInfo()
			:
			this(null)
		{
		}

		public UtilmenuMissionLocationInfo(UIElement Base)
			:
			base(Base)
		{
		}
	}


	public class UtilmenuMission : UIElement
	{
		public UIElementLabelString Header;

		public UIElementLabelString ButtonReadDetails;

		public UIElementLabelString ButtonStartConversation;

		public UtilmenuMissionLocationInfo[] Location;

		public UtilmenuMission()
		{
		}

		public UtilmenuMission(UIElement Base)
			:
			base(Base)
		{
		}
	}

}
