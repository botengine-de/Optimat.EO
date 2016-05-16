namespace BotEngine.EveOnline.Interface.MemoryStruct
{
	public class WindowStationLobby : Window
	{
		public UIElementLabelString[] AboveServicesLabel;

		public UIElement ButtonUndock;

		/// <summary>
		/// Station services.
		/// The type can be identified by TexturePath.
		/// Some examples:
		/// "res:/ui/Texture/WindowIcons/fitting.png"
		/// "res:/UI/Texture/WindowIcons/Industry.png"
		/// </summary>
		public Sprite[] ServiceButton;

		public LobbyAgentEntry[] AgentEntry;

		/// <summary>
		/// Label which are displayed between Agent Entries ("available to you", "Agents of interest").
		/// </summary>
		public UIElementLabelString[] AgentEntryHeader;

		public WindowStationLobby(Window Base)
			:
			base(Base)
		{
		}

		public WindowStationLobby()
		{
		}

	}

	public class LobbyAgentEntry : UIElement
	{
		public UIElementLabelString[] ListLabelString;

		public UIElement ButtonStartConversation;

		public LobbyAgentEntry(
			UIElement Base,
			UIElementLabelString[] ListLabelString,
			UIElement ButtonStartConversation)
			:
			base(Base)
		{
			this.ListLabelString = ListLabelString;
			this.ButtonStartConversation = ButtonStartConversation;
		}

		public LobbyAgentEntry()
		{
		}
	}

}
