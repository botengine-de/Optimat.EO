namespace BotEngine.EveOnline.Interface.MemoryStruct
{
	public class Window : UIElement
	{
		public bool? isModal;

		public string Caption;

		public bool? HeaderButtonsVisible;

		/// <summary>
		/// e.g. pin, minimize, close
		/// </summary>
		public Sprite[] HeaderButton;

		public UIElementLabelString[] Button;

		public UIElementTextBox[] TextBox;

		public UIElementLabelString[] Label;

		virtual public ListViewport ListMain()
		{
			return null;
		}

		public Window()
		{
		}

		public Window(
			UIElement Base,
			bool? isModal = null,
			string Caption = null,
			bool? HeaderButtonsVisible = null,
			Sprite[] HeaderButton = null,
			UIElementLabelString[] Button = null)
			:
			base(Base)
		{
			if (null != Base)
			{
			}

			this.isModal = isModal;
			this.Caption = Caption;

			this.HeaderButtonsVisible = HeaderButtonsVisible;
			this.HeaderButton = HeaderButton;

			this.Button = Button;
		}

		public Window(Window Base)
			:
			this((UIElement)Base)
		{
			isModal = Base?.isModal;
			Caption = Base?.Caption;
			HeaderButtonsVisible = Base?.HeaderButtonsVisible;
			HeaderButton = Base?.HeaderButton;
			Button = Base?.Button;
			Label = Base?.Label;
			TextBox = Base?.TextBox;
		}
	}

	/// <summary>
	/// In the eve online UI, windows can be stacked.
	/// </summary>
	public class WindowStack : Window
	{
		/// <summary>
		/// Contains one element for each window in this stack.
		/// </summary>
		public Tab[] Tab;

		/// <summary>
		/// Window whose tab is currently selected (and therefore the only window in the stack which is currently visible).
		/// </summary>
		public Window TabSelectedWindow;

		public WindowStack()
		{
		}

		public WindowStack(Window Base)
			:
			base(Base)
		{
		}
	}

}
