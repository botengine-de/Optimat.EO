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
	/// Interaction logic for CustomBotServer.xaml
	/// </summary>
	public partial class CustomBotServer : UserControl
	{
		Optimat.EveO.Nuzer.CustomBotServer Präsentiirte;

		public CustomBotServer()
		{
			InitializeComponent();
		}

		public void Präsentiire(Optimat.EveO.Nuzer.CustomBotServer Präsentiirte)
		{
			Int64? BeginZait = null;
			string ApiUri = null;
			Int64? ExceptionLezteZait = null;
			string ExceptionLezteSictString = null;
			bool IsListening = false;
			int? ListeRequestAnzaal = null;
			Int64? ListeRequestLezteZait = null;
			Int64?	ListeRequestMesungLezteZait	= null;

			try
			{
				if (null == Präsentiirte)
				{
					return;
				}

				var ExceptionLezte = Präsentiirte.ListeExceptionLezte;

				BeginZait = Präsentiirte.BeginZait;
				ApiUri = Präsentiirte.ApiUri;
				IsListening = Präsentiirte.IsListening;
				ListeRequestAnzaal = Präsentiirte.RequestAnzaal;
				ListeRequestLezteZait = Präsentiirte.ListeRequestLezteZait;

				ListeRequestMesungLezteZait	= Präsentiirte.RequestedMeasurementTimeLezte;

				var	VonBotScnapscusLezte	= Präsentiirte.VonBotScnapscusLezte;

				if(null	!= VonBotScnapscusLezte)
				{

				}

				if (null != ExceptionLezte.Wert)
				{
					ExceptionLezteZait = ExceptionLezte.Zait;
					ExceptionLezteSictString = Bib3.Glob.SictString(ExceptionLezte.Wert);
				}
			}
			finally
			{
				this.Präsentiirte = Präsentiirte;

				TextBoxApiUri.Text = ApiUri;
				this.BeginZait.Präsentiire(BeginZait);
				ListeExceptionLezteZait.Präsentiire(ExceptionLezteZait);
				TextBoxListeExceptionLezteSictString.Text = ExceptionLezteSictString;
				CheckBoxIsListening.IsChecked = IsListening;
				TextBoxListeRequestAnzaal.Text = ListeRequestAnzaal.ToString();
				this.ListeRequestLezteZait.Präsentiire(ListeRequestLezteZait);
				this.ListeRequestMesungLezteZait.Präsentiire(ListeRequestMesungLezteZait);
			}
		}
	}
}
