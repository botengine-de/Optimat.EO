using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Optimat.EveOnline.GBS
{
	public	interface IBerictInspektErwaiterung
	{
		void AuswaalZaitpunktSeze(Int64? Zaitpunkt);

		void AuswaalZaitraumSeze(Int64? ZaitraumBegin, Int64? ZaitraumEnde);

		System.Windows.FrameworkElement ReprTabItemHeaderBerecne();

		System.Windows.FrameworkElement ReprTabItemContentBerecne();
	}

	public class SictBerictInspektErwaiterungRepr
	{
		readonly public IBerictInspektErwaiterung Repräsentiirte;

		readonly public TabItem TabItem;

		public SictBerictInspektErwaiterungRepr(
			IBerictInspektErwaiterung Repräsentiirte)
		{
			this.Repräsentiirte = Repräsentiirte;

			TabItem = new TabItem();
		}

		public void Aktualisiire()
		{
			var Repräsentiirte = this.Repräsentiirte;

			if (null != Repräsentiirte)
			{
				TabItem.Header = Repräsentiirte.ReprTabItemHeaderBerecne();
				TabItem.Content = Repräsentiirte.ReprTabItemContentBerecne();
			}
		}
	}

}
