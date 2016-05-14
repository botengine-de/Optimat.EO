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
using Optimat.EveOnline;

namespace Optimat.EveOnline.GBS
{
	/// <summary>
	/// Interaction logic for SictShipTreferpunkteUndCapacitorInspekt.xaml
	/// </summary>
	public partial class SictShipTreferpunkteUndCapacitorInspekt : UserControl
	{
		public SictShipTreferpunkteUndCapacitorInspekt()
		{
			InitializeComponent();
		}

		public ShipHitpointsAndEnergy Repräsentiirte
		{
			private set;
			get;
		}

		public void Repräsentiire(ShipHitpointsAndEnergy Repräsentiirte)
		{
			this.Repräsentiirte = Repräsentiirte;

			SictTreferpunkteTotalUndRel Shield	= null;
			SictTreferpunkteTotalUndRel Armor	= null;
			SictTreferpunkteTotalUndRel Struct = null;
			SictTreferpunkteTotalUndRel Capacitor = null;

			if (null != Repräsentiirte)
			{
				Shield = SictTreferpunkteTotalUndRel.FürNormiirtMili(Repräsentiirte.Shield);
				Armor = SictTreferpunkteTotalUndRel.FürNormiirtMili(Repräsentiirte.Armor);
				Struct = SictTreferpunkteTotalUndRel.FürNormiirtMili(Repräsentiirte.Struct);
				Capacitor = SictTreferpunkteTotalUndRel.FürNormiirtMili(Repräsentiirte.Capacitor);
			}

			ShieldTreferpunktInspekt.Treferpunkte = Shield;
			ArmorTreferpunktInspekt.Treferpunkte = Armor;
			StructTreferpunktInspekt.Treferpunkte = Struct;
			CapacitorCapacityInspekt.Treferpunkte = Capacitor;
		}

		public bool ZaigeNumeerisc
		{
			set
			{
				ShieldTreferpunktInspekt.ZaigeNumeerisc = value;
				ArmorTreferpunktInspekt.ZaigeNumeerisc = value;
				StructTreferpunktInspekt.ZaigeNumeerisc = value;
				CapacitorCapacityInspekt.ZaigeNumeerisc = value;
			}

			get
			{
				return
					ShieldTreferpunktInspekt.ZaigeNumeerisc ||
					ArmorTreferpunktInspekt.ZaigeNumeerisc ||
					StructTreferpunktInspekt.ZaigeNumeerisc	||
					CapacitorCapacityInspekt.ZaigeNumeerisc;
			}
		}

	}
}
