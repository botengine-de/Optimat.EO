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
	/// Interaction logic for SictLocationInfo.xaml
	/// </summary>
	public partial class SictLocationInfo : UserControl
	{
		public SictLocationInfo()
		{
			InitializeComponent();
		}

		public void Repräsentiire(SictAusGbsLocationInfo Repräsentiirte)
		{
			string NearestName = null;
			string SystemName = null;
			string	ConstellationName	= null;
			string	RegionName	= null;
			int? SolarSystemSecurityLevelMili = null;

			try
			{
				if (null != Repräsentiirte)
				{
					NearestName = Repräsentiirte.NearestName;
					SystemName = Repräsentiirte.SolarSystemName;
					ConstellationName = Repräsentiirte.ConstellationName;
					RegionName = Repräsentiirte.RegionName;
					SolarSystemSecurityLevelMili = Repräsentiirte.SolarSystemSecurityLevelMili;
				}
			}
			finally
			{
				TextBoxNearestNameInspekt.Text = NearestName;
				TextBoxSystemNaameInspekt.Text = SystemName;
				TextBoxConstellationNaameInspekt.Text = ConstellationName;
				TextBoxRegionNaameInspekt.Text = RegionName;
				TextBoxSecurityLevelInspekt.Text = (SolarSystemSecurityLevelMili * 1e-3).ToString();
			}
		}
	}
}
