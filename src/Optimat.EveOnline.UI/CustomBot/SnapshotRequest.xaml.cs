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

namespace Optimat.EveOnline.UI.CustomBot
{
	/// <summary>
	/// Interaction logic for Request.xaml
	/// </summary>
	public partial class SnapshotRequest : UserControl
	{
		public SnapshotRequest()
		{
			InitializeComponent();
		}

		public void Present(Optimat.EveOnline.CustomBot.SnapshotRequest Request)
		{
			Int64? Time = null;
			string ApiUrl = null;
			System.Exception Exception = null;
			bool? Success = null;

			try
			{
				if (null == Request)
				{
					return;
				}

				ApiUrl = Request.ApiUri;

				var Result = Request.Result;

				if (null == Result)
				{
					return;
				}

				Time = Result.Time;
				Exception = Result.Exception;

				var ResponseSnapshot = Result.ResponseSnapshot;

				Success = null != ResponseSnapshot;
			}
			finally
			{
				TimeInspect.Präsentiire(Time);

				TextBoxUrlInspect.Text = ApiUrl;
				TextBoxExceptionInspect.Text = Bib3.Glob.SictString(Exception);

				ResponseIconSuccess.Value = Success;
			}
		}
	}
}
