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
	/// Interaction logic for SictTreferpunkteInspektBalkn.xaml
	/// </summary>
	public partial class SictRectekDraigetailtHoriz : UserControl
	{
		public SictRectekDraigetailtHoriz()
		{
			InitializeComponent();
		}

		public Brush ScpalteLinksBrush
		{
			set
			{
				PanelLinks.Background = value;
			}

			get
			{
				return PanelLinks.Background;
			}
		}

		public Brush ScpalteMiteBrush
		{
			set
			{
				PanelMite.Background = value;
			}

			get
			{
				return PanelMite.Background;
			}
		}

		public Brush ScpalteReczBrush
		{
			set
			{
				PanelRecz.Background = value;
			}

			get
			{
				return PanelRecz.Background;
			}
		}

		public int ScpalteLinksAntail
		{
			set
			{
				ColumnLinks.Width = new GridLength(Math.Max(0, value), GridUnitType.Star);
			}

			get
			{
				return (int)ColumnLinks.Width.Value;
			}
		}

		public int ScpalteMiteAntail
		{
			set
			{
				ColumnMite.Width = new GridLength(Math.Max(0, value), GridUnitType.Star);
			}

			get
			{
				return (int)ColumnMite.Width.Value;
			}
		}

		public int ScpalteReczAntail
		{
			set
			{
				ColumnRecz.Width = new GridLength(Math.Max(0, value), GridUnitType.Star);
			}

			get
			{
				return (int)ColumnRecz.Width.Value;
			}
		}

	}
}
