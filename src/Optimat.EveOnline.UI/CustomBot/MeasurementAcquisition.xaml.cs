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
using Bib3;
using Newtonsoft.Json;
using Optimat.EveOnline.CustomBot;

namespace Optimat.EveOnline.UI.CustomBot
{
	/// <summary>
	/// Interaction logic for MeasurementAcquisition.xaml
	/// </summary>
	public partial class MeasurementAcquisition : UserControl
	{
		public MeasurementAcquisition()
		{
			InitializeComponent();
		}

		public int FromWebserverRequestTimeDistance = 1000;

		public int FromWebserverRequestDurationLimit = 1000;

		Int64 FromWebserverRequestLastTime = 0;

		Optimat.EveOnline.CustomBot.SnapshotRequest Request;

		/// <summary>
		/// will be send to the Sensor with every request.
		/// </summary>
		public FromCustomBotSnapshot FromCustomBotSnapshot;

		public EveOnline.CustomBot.InputSnapshot InputSnapshotLast
		{
			private set;
			get;
		}

		public EveOnline.VonSensorikMesung AcquisitionLastSensorMeasurement
		{
			get
			{
				var InputSnapshotLast = this.InputSnapshotLast;

				if (null == InputSnapshotLast)
				{
					return null;
				}

				return InputSnapshotLast.SensorMeasurement;
			}
		}

		void InputSnapshot(
			ToCustomBotSnapshot Snapshot,
			string SourceUri)
		{
			InputSnapshotLast = new EveOnline.CustomBot.InputSnapshot(Bib3.Glob.StopwatchZaitMiliSictInt(), Snapshot, SourceUri);
		}

		public bool FromWebserverRequestContinuously
		{
			get
			{
				return CheckBoxFromWebserverRequestContinuously.IsChecked ?? false;
			}
		}

		void FromWebserverRequest()
		{
			FromWebserverRequestLastTime = Bib3.Glob.StopwatchZaitMiliSictInt();

			var Request = new Optimat.EveOnline.CustomBot.SnapshotRequest(ConnectionParam.ApiUri, FromWebserverRequestDurationLimit, FromCustomBotSnapshot);

			this.Request = Request;

			Task.Run(() =>
				{
					Request.Execute();

					InputSnapshot(Request.ResultResponseSnapshot, Request.ApiUri);
				});
		}

		private void ButtonFromWebserverRequestOnce_Click(object sender, RoutedEventArgs e)
		{
			FromWebserverRequest();
		}

		private void ButtonReadFromFile_Drop(object sender, DragEventArgs e)
		{
			Bib3.FCL.GBS.Extension.CatchNaacMessageBoxException(() =>
			{
				string FilePath = null;
				ToCustomBotSnapshot FileContentAsSnapshot = null;

				try
				{
					var Time = Bib3.Glob.StopwatchZaitMiliSictInt();

					var Files = Bib3.FCL.Glob.LaadeMengeDataiInhaltAusDropFileDrop(e);

					if (null == Files)
					{
						throw new ArgumentNullException("Files");
					}

					var File = Files.FirstOrDefaultNullable();

					//	foreach (var File in Files)
					{
						FilePath = File.Key;

						var FileContent = File.Value;

						try
						{
							if (null == FileContent)
							{
								throw new ArgumentNullException("FileContent");
							}

							var FileContentAsUTF8 = Encoding.UTF8.GetString(FileContent);

							var FileContentAsObject = ToCustomBotSnapshot.DeserializeFromString<object>(FileContentAsUTF8);

							FileContentAsSnapshot = FileContentAsObject as ToCustomBotSnapshot;

							if (null == FileContentAsSnapshot)
							{
								var FileContentAsMeasurement = FileContentAsObject as VonSensorikMesung;

								if (null != FileContentAsMeasurement)
								{
									FileContentAsSnapshot = new ToCustomBotSnapshot(
										Time,
										new VonProcessMesung<VonSensorikMesung>(FileContentAsMeasurement, Time));
								}
							}
						}
						catch (System.Exception Exception)
						{
							throw new ApplicationException("Error for File \"" + (FilePath ?? "") + "\"", Exception);
						}
					}
				}
				finally
				{
					InputSnapshot(FileContentAsSnapshot, FilePath);
				}
			});

		}

		public void Present()
		{
			FromWebserverRequestLastInspect.Present(Request);

			AcquisitionLastInspect.Present(InputSnapshotLast);
		}

		public void Acquire()
		{
			var Time = Bib3.Glob.StopwatchZaitMiliSictInt();

			if (FromWebserverRequestContinuously)
			{
				var FromWebserverRequestLastAge = Time - FromWebserverRequestLastTime;

				if (!(FromWebserverRequestLastAge < FromWebserverRequestTimeDistance))
				{
					FromWebserverRequest();
				}
			}
		}
	}
}
