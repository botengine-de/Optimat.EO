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
	/// Interaction logic for SictTreferpunkteInspekt.xaml
	/// </summary>
	public partial class SictTreferpunkteInspekt : UserControl
	{
		public SictTreferpunkteInspekt()
		{
			InitializeComponent();

			Treferpunkte = null;
			ZaigeNumeerisc = true;
		}

		public SictTreferpunkteTotalUndRel Treferpunkte
		{
			set
			{
				if (null != value)
				{
					var Maximum = value.Maximum;
					var Antail = value.Antail;
					var NormiirtMilli = value.NormiirtMili;

					if (Maximum.HasValue && Antail.HasValue)
					{
						Rectek.ScpalteMiteAntail = 0;
						Rectek.ScpalteLinksAntail = Antail.Value;
						Rectek.ScpalteReczAntail = Maximum.Value - Antail.Value;

						TextBlokInspektNumeerisc.Text = Antail.ToString() + " / " + Maximum.ToString();

						return;
					}
					else
					{
						if (NormiirtMilli.HasValue)
						{
							Rectek.ScpalteMiteAntail = 0;
							Rectek.ScpalteLinksAntail = NormiirtMilli.Value;
							Rectek.ScpalteReczAntail = 1000 - NormiirtMilli.Value;

							TextBlokInspektNumeerisc.Text = (NormiirtMilli / 10).ToString() + "%";

							return;
						}
					}
				}

				Rectek.ScpalteMiteAntail = 1000;
				Rectek.ScpalteLinksAntail = 0;
				Rectek.ScpalteReczAntail = 0;

				TextBlokInspektNumeerisc.Text = "????";
			}
		}

		public bool ZaigeNumeerisc
		{
			set
			{
				PanelInspektNumeerisc.Visibility = value ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
			}

			get
			{
				return System.Windows.Visibility.Visible	== PanelInspektNumeerisc.Visibility;
			}
		}
	}
}
