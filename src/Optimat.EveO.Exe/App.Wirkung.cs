using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimat.EveO.Nuzer
{
	public	partial	class App
	{
		void WirkungMouseClick(IntPtr WindowHandle,	int	LaageA,	int	LaageB,	bool	TasteRecz	= false)
		{
			Bib3.Windows.User32.ClickOnPointGebascdelsMitSleep20140211(WindowHandle, new System.Windows.Point(LaageA, LaageB), TasteRecz, MouseWartezaitMili);
		}

		void WirkungMouseMove(IntPtr WindowHandle,	int	LaageA,	int	LaageB)
		{
			Bib3.Windows.User32.MouseMoveOnPointGebascdels20140211(WindowHandle, new System.Windows.Point(LaageA, LaageB));
		}
	}
}
