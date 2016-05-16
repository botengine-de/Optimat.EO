using System.Collections.Generic;
using System.Linq;

namespace BotEngine.EveOnline.Interface.MemoryStruct
{
	public class WindowSurveyScanView : Window
	{
		public ListViewport ListViewport;

		public WindowSurveyScanView(Window Base)
			:
			base(Base)
		{
		}

		public WindowSurveyScanView()
		{
		}

		override public ListViewport ListMain()
		{
			return ListViewport;
		}
	}
}
