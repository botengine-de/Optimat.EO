namespace BotEngine.EveOnline.Interface.MemoryStruct
{
	public class ModuleButtonTooltipRow : UIElement
	{
		public UIElementLabelString[] ListLabelString;

		public string ShortcutText;

		public ObjectIdInMemory[] IconTextureId;

		public ModuleButtonTooltipRow()
		{
		}

		public ModuleButtonTooltipRow(UIElement Base)
			:
			base(Base)
		{
		}

	}


	public class ModuleButtonTooltip : UIElement
	{
		public UIElementLabelString[] ListLabelString;

		public ModuleButtonTooltipRow[] ListRow;

		public ModuleButtonTooltip()
		{
		}

		public ModuleButtonTooltip(UIElement Base)
			:
			base(Base)
		{
		}

	}

}
