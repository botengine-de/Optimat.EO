using System.Collections.Generic;
using System.Linq;

namespace BotEngine.EveOnline.Interface.MemoryStruct
{
	public class ChatParticipant : ListEntry
	{
		public UIElementLabelString NameLabel;

		public Sprite StatusIcon;

		public ChatParticipant(ListEntry Base)
			:
			base(Base)
		{
		}

		public ChatParticipant()
		{
		}
	}

	public class ChatMessage : ListEntry
	{
		public ChatMessage(ChatMessage Base)
			:
			base(Base)
		{
		}

		public ChatMessage()
		{
		}

	}

	public class WindowChatChannel : Window
	{
		public ListViewport ParticipantViewport;

		public ListViewport MessageViewport;

		public IEnumerable<ChatParticipant> Participant => ParticipantViewport?.Entry?.OfType<ChatParticipant>();

		public IEnumerable<ChatMessage> Message => MessageViewport?.Entry?.OfType<ChatMessage>();

		/// <summary>
		/// Label not contained in Message or Participant.
		/// </summary>
		public UIElementLabelString[] LabelOther;

		public WindowChatChannel(Window Window)
			:
			base(Window)
		{
		}

		public WindowChatChannel()
		{
		}
	}

}
