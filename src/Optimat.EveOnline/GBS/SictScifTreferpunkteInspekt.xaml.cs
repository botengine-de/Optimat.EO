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
	/// Interaction logic for SictScifTreferpunkteInspekt.xaml
	/// </summary>
	public partial class SictScifTreferpunkteInspekt : UserControl
	{
		public SictScifTreferpunkteInspekt()
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
			SictTreferpunkteTotalUndRel Struct	= null;

			if (null != Repräsentiirte)
			{
				Shield = SictTreferpunkteTotalUndRel.FürNormiirtMili(Repräsentiirte.Shield);
				Armor = SictTreferpunkteTotalUndRel.FürNormiirtMili(Repräsentiirte.Armor);
				Struct = SictTreferpunkteTotalUndRel.FürNormiirtMili(Repräsentiirte.Struct);
			}

			ShieldTreferpunktInspekt.Treferpunkte = Shield;
			ArmorTreferpunktInspekt.Treferpunkte = Armor;
			StructTreferpunktInspekt.Treferpunkte = Struct;
		}

		public bool ZaigeNumeerisc
		{
			set
			{
				ShieldTreferpunktInspekt.ZaigeNumeerisc = value;
				ArmorTreferpunktInspekt.ZaigeNumeerisc = value;
				StructTreferpunktInspekt.ZaigeNumeerisc = value;
			}

			get
			{
				return
					ShieldTreferpunktInspekt.ZaigeNumeerisc ||
					ArmorTreferpunktInspekt.ZaigeNumeerisc ||
					StructTreferpunktInspekt.ZaigeNumeerisc;
			}
		}

	}
}
