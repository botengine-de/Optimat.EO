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

namespace Optimat.EveO.Nuzer.GBS
{
	/// <summary>
	/// Interaction logic for SictInProcessSuuceInspektAinfac.xaml
	/// </summary>
	public partial class SictInProcessSuuceInspektAinfac : UserControl
	{
		public SictInProcessSuuceInspektAinfac()
		{
			InitializeComponent();
		}

		public void Repräsentiire(
			Int64? BeginZaitMili,
			Int64? EndeZaitMili,
			string ErgeebnisSictString)
		{
			var ZaitMikro = Bib3.Glob.StopwatchZaitMikroSictInt();

			var ZaitMili = ZaitMikro / 1000;

			var	AlterMili = ZaitMili - BeginZaitMili;

			var DauerMili = EndeZaitMili - BeginZaitMili;

			string BeginZaitKalenderString = null;

			try
			{
				var BeginZaitSictDateTime = BeginZaitMili.HasValue ? Bib3.Glob.SictDateTimeVonStopwatchZaitMili(BeginZaitMili.Value) : (DateTime?)null;

				BeginZaitKalenderString = BeginZaitSictDateTime.HasValue ? Bib3.Glob.SictwaiseKalenderString(BeginZaitSictDateTime.Value, ".", 0) : null;
			}
			finally
			{
				TextBoxBeginAlter.Text = (AlterMili / 1000).ToString();

				TextBoxBeginZaitKalender.Text = BeginZaitKalenderString;

				TextBoxDauerMili.Text = DauerMili.ToString();

				TextBoxErgeebnisSictString.Text = ErgeebnisSictString;
			}
		}
	}
}
