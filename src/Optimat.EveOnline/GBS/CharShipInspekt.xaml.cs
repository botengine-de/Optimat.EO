using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Optimat.EveOnline.GBS
{
	/// <summary>
	/// Interaction logic for CharShipInspekt.xaml
	/// </summary>
	public partial class SictCharShipInspekt : UserControl
	{
		Optimat.EveOnline.ShipState Repräsentiirte;

		public SictCharShipInspekt()
		{
			InitializeComponent();
		}

		public void Repräsentiire(Optimat.EveOnline.ShipState	Repräsentiirte)
		{
			bool? Docked = null;
			bool? Cloaked	= null;
			bool? Warping = null;
			bool? Jumping = null;
			Int64? SpeedDurcMeterProSekunde = null;
			ShipHitpointsAndEnergy	Treferpunkte	= null;

			try
			{
				if (null == Repräsentiirte)
				{
					return;
				}

				Docked = Repräsentiirte.Docked;
				Cloaked = Repräsentiirte.Cloaked;
				Warping = Repräsentiirte.Warping;
				Jumping = Repräsentiirte.Jumping;

				SpeedDurcMeterProSekunde = Repräsentiirte.SpeedDurcMeterProSekunde;

				Treferpunkte = Repräsentiirte.HitpointsRelMili;
			}
			finally
			{
				this.Repräsentiirte = Repräsentiirte;

				CheckBoxDocked.IsChecked = Docked;
				CheckBoxCloaked.IsChecked = Cloaked;
				CheckBoxWarping.IsChecked = Warping;
				CheckBoxJumping.IsChecked = Jumping;

				TextBoxSpeedInspekt.Text = SpeedDurcMeterProSekunde.ToString();

				TreferpunkteInspekt.Repräsentiire(Treferpunkte);
			}
		}
	}
}
