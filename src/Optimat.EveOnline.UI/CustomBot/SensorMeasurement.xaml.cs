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
using Optimat.EveOnline.CustomBot;
using ExtractFromOldAssembly.Bib3;

namespace Optimat.EveOnline.UI.CustomBot
{
	/// <summary>
	/// Interaction logic for SensorMeasurement.xaml
	/// </summary>
	public partial class SensorMeasurement : UserControl
	{
		Optimat.EveOnline.VonSensorikMesung Presented;

		public SensorMeasurement()
		{
			InitializeComponent();
		}

		Bib3.RefNezDiferenz.SictTypeBehandlungRictliinieMitTransportIdentScatescpaicer
			ObjectCountRule =
			new Bib3.RefNezDiferenz.SictTypeBehandlungRictliinieMitTransportIdentScatescpaicer(
			Optimat.EveOnline.Extension.GbsBaumMengeTypeBehandlungRictliinieKonstrukt());

		public void Present(Optimat.EveOnline.VonSensorikMesung Presented)
		{
			Int64? ObjectCount = null;

			try
			{
				if (null == Presented)
				{
					return;
				}

				ObjectCount = Bib3.RefNezDiferenz.Extension.EnumMengeRefAusNezAusWurzel(Presented, ObjectCountRule).CountNullable();
			}
			finally
			{
				this.Presented = Presented;

				TextBoxObjectCount.Text = ObjectCount.ToString();
			}
		}

		private void ButtonWriteToFile_Drop(object sender, DragEventArgs e)
		{
			Bib3.FCL.GBS.Extension.CatchNaacMessageBoxException(() =>
			{
				var FilePath = Bib3.FCL.Glob.DataiPfaadAlsKombinatioonAusSctandardPfaadUndFileDrop("SensorMeasurement", e);

				if (null == Presented)
				{
					throw new ArgumentNullException("Presented");
				}

				var SensorSnapshotSerial = ToCustomBotSnapshot.SerializeToString(Presented);

				var SensorSnapshotSerialUTF8 = Encoding.UTF8.GetBytes(SensorSnapshotSerial);

				Bib3.Glob.ScraibeInhaltNaacDataiPfaad(FilePath, SensorSnapshotSerialUTF8);
			});
		}
	}
}
