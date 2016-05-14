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
using Optimat.EveOnline.CustomBot;


namespace Optimat.EveOnline.UI.CustomBot
{
	/// <summary>
	/// Interaction logic for InputSnapshot.xaml
	/// </summary>
	public partial class InputSnapshot : UserControl
	{
		Optimat.EveOnline.CustomBot.InputSnapshot Presented;

		public InputSnapshot()
		{
			InitializeComponent();
		}

		public void Present(Optimat.EveOnline.CustomBot.InputSnapshot Presented)
		{
			Int64? Time = null;
			string SourceUri = null;
			bool ContainsSensorSnapshot = false;
			int? SensorProcessId = null;
			Int64? SensorMeasurementBeginTime = null;
			Int64? SensorMeasurementDurationMili = null;
			VonSensorikMesung SensorMeasurement = null;

			try
			{
				if (null == Presented)
				{
					return;
				}

				Time = Presented.Time;
				SourceUri = Presented.SourceUri;

				var Snapshot = Presented.Snapshot;

				VonProcessMesung<VonSensorikMesung> MemoryMeasurement = null;

				if (null != Snapshot)
				{
					MemoryMeasurement = Snapshot.MemoryMeasurement;
				}

				if (null != MemoryMeasurement)
				{
					SensorMeasurement = MemoryMeasurement.Wert;
				}

				if (null != MemoryMeasurement)
				{
					SensorProcessId = MemoryMeasurement.ProcessId;
					SensorMeasurementBeginTime = MemoryMeasurement.BeginZait;
					SensorMeasurementDurationMili = MemoryMeasurement.Dauer;
				}

				ContainsSensorSnapshot = null != SensorMeasurement;
			}
			finally
			{
				this.Presented = Presented;

				TimeInspect.Präsentiire(Time);
				TextBoxSourceUriInspect.Text = SourceUri;
				CheckBoxContainsSensorSnapshot.IsChecked = ContainsSensorSnapshot;

				TextBoxSensorProcessId.Text = SensorProcessId.ToString();
				SensorMeasurementBeginTimeInspect.Präsentiire(SensorMeasurementBeginTime);
				SensorMeasurementDurationMiliInspect.Text = SensorMeasurementDurationMili.ToString();
				SensorMeasurementInspect.Present(SensorMeasurement);
			}
		}

		private void ButtonWriteToFile_Drop(object sender, DragEventArgs e)
		{
			Bib3.FCL.GBS.Extension.CatchNaacMessageBoxException(() =>
				{
					var FilePath = Bib3.FCL.Glob.DataiPfaadAlsKombinatioonAusSctandardPfaadUndFileDrop(
						"ToBotSnapshot", e);

					if (null == Presented)
					{
						throw new ArgumentNullException("Presented");
					}

					var Snapshot = Presented.Snapshot;

					if (null == Snapshot)
					{
						throw new ArgumentNullException("Snapshot");
					}

					var SensorSnapshotSerial = ToCustomBotSnapshot.SerializeToString(Snapshot);

					var SensorSnapshotSerialUTF8 = Encoding.UTF8.GetBytes(SensorSnapshotSerial);

					Bib3.Glob.ScraibeInhaltNaacDataiPfaad(FilePath, SensorSnapshotSerialUTF8);
				});
		}
	}
}
