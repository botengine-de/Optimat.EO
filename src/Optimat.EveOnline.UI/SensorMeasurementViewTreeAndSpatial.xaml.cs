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

namespace Optimat.EveOnline.UI
{
	/// <summary>
	/// Interaction logic for SensorMeasurementViewTreeAndSpatial.xaml
	/// </summary>
	public partial class SensorMeasurementViewTreeAndSpatial : UserControl
	{
		public VonSensorikMesung Presented
		{
			private set;
			get;
		}

		public SensorMeasurementViewTreeAndSpatial()
		{
			InitializeComponent();

			ViewTree.SictAstSictArgument = Optimat.Inspektor.GBS.SictTreeViewReprRefNezKopii.SictAstSictArgumentKonstrukt();
			ViewTree.CallbackAstRaiheWertMitZaitRepr = Optimat.Inspektor.GBS.SictTreeViewReprRefNezKopii.AstRaiheWertMitZaitReprRefNezKopiiKonstrukt;

			ViewTree.CallbackPfaadSictAst = Optimat.Inspektor.GBS.SictTreeViewReprRefNezKopii.PfaadSictKomponente;
		}

		public	void	Present(VonSensorikMesung Presented)
		{
			try
			{
				ViewTree.Präsentiire(new Bib3.WertZuZaitpunktStruct<object>[] { new Bib3.WertZuZaitpunktStruct<object>(Presented, 0) });
			}
			finally
			{
				this.Presented = Presented;
			}
		}
	}
}
