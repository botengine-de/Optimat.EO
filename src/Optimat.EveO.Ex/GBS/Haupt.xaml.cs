using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Bib3;
using Newtonsoft.Json;
using Optimat.EveOnline;
using ExtractFromOldAssembly.Bib3;
using Bib3.FCL;
using BotEngine.Interface;

namespace Optimat.EveO.Nuzer.GBS
{
	/// <summary>
	/// Interaction logic for Haupt.xaml
	/// </summary>
	public partial class Haupt : UserControl
	{
		public SictKonfiguratioon KonfigSctandardWert;

		public SictOptimatParam EveOnlinePräferenzSctandardWert;

		SictOptimatParam InternEveOnlinePräferenzAingaabeLezte;

		public bool ErwaitertFraigaabe
		{
			private set;
			get;
		}

		public SictOptimatParam EveOnlinePräferenzAingaabeLezte
		{
			get
			{
				return InternEveOnlinePräferenzAingaabeLezte;
			}
		}

		public Func<FromProcessMeasurement<Sanderling.Interface.MemoryStruct.IMemoryMeasurement>> FromProcessMeasurementLastGetDelegate;

		public Haupt()
		{
			InitializeComponent();

			MemoryMeasurementSimulationUIUpdate();

			ZiilProcessAuswaalZiilProcess.BewertungMainModuleDataiNaameAingaabe = "ExeFile.exe";

			ZiilProcessAuswaalZiilProcessRicteMengeProcessBegin(true, true);

			foreach (var DamageTypeName in Enum.GetNames(typeof(SictDamageTypeSictEnum)))
			{
				ComboBoxEveOnlineSimuDamageType.Items.Add(DamageTypeName);
			}

			this.AddHandler(Optimat.EveOnline.GBS.SictAutoKonfig.KonfigGeändertEvent, new RoutedEventHandler(EveOnlinePräferenzGeändertEventHandler));

			ComboBoxSimuMausAufWindowVordersteEkeIndex.Items.Add("none");

			Enumerable.Range(0, 8).ForEachNullable((ekeIndex) =>
				{
					ComboBoxSimuMausAufWindowVordersteEkeIndex.Items.Add(ekeIndex);
				});
		}

		public void EveOnlinePräferenzAusGbsAingaabeLeese()
		{
			SctoierelementEveOnlinePräferenz.KonfigBerecneAusGbs(ref	InternEveOnlinePräferenzAingaabeLezte);
		}

		void EveOnlinePräferenzGeändertEventHandler(object sender, RoutedEventArgs e)
		{
			try
			{

			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}

		public SictKonfiguratioon KonfigBerecneAusGbs()
		{
			SictAuswaalWindowsProcessPräferenz AuswaalZiilProcess = null;
			Key[][] ZiilProcessWirkungPauseMengeKeyKombi = null;

			string ScnitOptimatServerLizenzDataiPfaad = TextBoxOptimatServerVerbindungLizenzDataiPfaad.Text;
			string BerictHauptPersistVerzaicnisPfaad = BerictPersistAingaabeBerictHaupt.Pfaad;
			Int64? BerictHauptPersistKapazitäätScranke = BerictPersistAingaabeBerictHaupt.Kapazitäät;
			string BerictWindowClientRasterPersistVerzaicnisPfaad = BerictPersistAingaabeBerictWindowClientRaster.Pfaad;
			Int64? BerictWindowClientRasterPersistKapazitäätScranke = BerictPersistAingaabeBerictWindowClientRaster.Kapazitäät;

			var ZiilProcessAuswaalZiilProcess = this.ZiilProcessAuswaalZiilProcess;
			var ZiilProcessWirkungPauseAuswaalMengeKeyKombi = this.ZiilProcessWirkungPauseAuswaalMengeKeyKombi;

			if (null != ZiilProcessAuswaalZiilProcess)
			{
				AuswaalZiilProcess = ZiilProcessAuswaalZiilProcess.PräferenzBerecneAusGbs();
			}

			if (null != ZiilProcessWirkungPauseAuswaalMengeKeyKombi)
			{
				ZiilProcessWirkungPauseMengeKeyKombi =
					ZiilProcessWirkungPauseAuswaalMengeKeyKombi.MengeKeyKombiBerecne()
					.ToArrayNullable();
			}

			var Konfig = new SictKonfiguratioon();

			Konfig.AuswaalZiilProcess = AuswaalZiilProcess;

			Konfig.ZiilProcessWirkungPauseMengeKeyKombi = ZiilProcessWirkungPauseMengeKeyKombi;

			Konfig.ScnitOptimatServerLizenzDataiPfaad = ScnitOptimatServerLizenzDataiPfaad;

			Konfig.SensorServerApiUri = TextBoxSensorServerApiUri?.Text;

			Konfig.BerictHauptPersistVerzaicnisPfaad = BerictHauptPersistVerzaicnisPfaad;
			Konfig.BerictHauptPersistKapazitäätScranke = BerictHauptPersistKapazitäätScranke;

			Konfig.BerictSizungPersistVerzaicnisPfaad = TextBoxSizungBerictVerzaicnisPfaad.Text;

			Konfig.BerictWindowClientRasterPersistVerzaicnisPfaad = BerictWindowClientRasterPersistVerzaicnisPfaad;
			Konfig.BerictWindowClientRasterPersistKapazitäätScranke = BerictWindowClientRasterPersistKapazitäätScranke;

			return Konfig;
		}

		public void KonfigScraibeNaacGbs(SictKonfiguratioon konfig)
		{
			if (!IsInitialized)
			{
				return;
			}

			var AuswaalZiilProcess = (null == konfig) ? null : konfig.AuswaalZiilProcess;

			var ZiilProcessWirkungPauseMengeKeyKombi = (null == konfig) ? null : konfig.ZiilProcessWirkungPauseMengeKeyKombi;

			var ScnitOptimatServerLizenzDataiPfaad = (null == konfig) ? null : konfig.ScnitOptimatServerLizenzDataiPfaad;
			var ScnitOptimatServerVersuucVerbindungZaitDistanz = (null == konfig) ? null : konfig.ScnitOptimatServerVersuucVerbindungZaitDistanz;
			var BerictHauptPersistVerzaicnisPfaad = (null == konfig) ? null : konfig.BerictHauptPersistVerzaicnisPfaad;
			var BerictHauptPersistKapazitäätScranke = (null == konfig) ? null : konfig.BerictHauptPersistKapazitäätScranke;
			var BerictWindowClientRasterPersistVerzaicnisPfaad = (null == konfig) ? null : konfig.BerictWindowClientRasterPersistVerzaicnisPfaad;
			var BerictWindowClientRasterPersistKapazitäätScranke = (null == konfig) ? null : konfig.BerictWindowClientRasterPersistKapazitäätScranke;
			var BerictSizungPersistVerzaicnisPfaad = (null == konfig) ? null : konfig.BerictSizungPersistVerzaicnisPfaad;

			ZiilProcessAuswaalZiilProcess.PräferenzScraibeNaacGbs(AuswaalZiilProcess);

			ZiilProcessWirkungPauseAuswaalMengeKeyKombi.MengeKeyKombiSeze(ZiilProcessWirkungPauseMengeKeyKombi);

			TextBoxOptimatServerVerbindungLizenzDataiPfaad.Text = ScnitOptimatServerLizenzDataiPfaad;

			var SensorServerApiUri = konfig?.SensorServerApiUri;

			if(SensorServerApiUri.IsNullOrEmpty())
			{
				SensorServerApiUri = App.BotEngineApiUriDefault;
			}

			TextBoxSensorServerApiUri.Text = SensorServerApiUri;

			BerictPersistAingaabeBerictHaupt.Pfaad = BerictHauptPersistVerzaicnisPfaad;
			BerictPersistAingaabeBerictHaupt.Kapazitäät = BerictHauptPersistKapazitäätScranke;

			BerictPersistAingaabeBerictWindowClientRaster.Pfaad = BerictWindowClientRasterPersistVerzaicnisPfaad;
			BerictPersistAingaabeBerictWindowClientRaster.Kapazitäät = BerictWindowClientRasterPersistKapazitäätScranke;

			TextBoxSizungBerictVerzaicnisPfaad.Text = BerictSizungPersistVerzaicnisPfaad;
		}

		void ZiilProcessAuswaalZiilProcessRicteMengeProcessBegin(
			bool sortiireNaacBewertungPunkteAnzaal,
			bool selectedItemsSezeAufGrupeBewertungPunktAnzaalMax)
		{
			ZiilProcessAuswaalZiilProcess.MengeProcessRicteNaacSelbsctBegin(sortiireNaacBewertungPunkteAnzaal, selectedItemsSezeAufGrupeBewertungPunktAnzaalMax);
		}

		private void TextBoxOptimatServerVerbindungLizenzDataiPfaad_Drop(object sender, DragEventArgs e)
		{
			try
			{
				TextBoxOptimatServerVerbindungLizenzDataiPfaad.Text = Optimat.Glob.AusDropFileDropListeDataiPfaadFrüüheste(e);
			}
			catch (System.Exception Ausnaame)
			{
				MessageBox.Show(Bib3.Glob.SictString(Ausnaame));
			}
		}

		private void TextBoxOptimatServerVerbindungLizenzDataiPfaad_PreviewDragOver(object sender, DragEventArgs e)
		{
			e.Handled = true;
		}

		private void TextBoxBrowserProcessAssemblyDataiPfaad_PreviewDragOver(object sender, DragEventArgs e)
		{
			e.Handled = true;
		}

		[JsonObject(MemberSerialization.OptIn)]
		class SictMengeScnapscusZuZait
		{
			[JsonProperty]
			public SictWertMitZait<object>[] MengeScnapscus;

			public SictMengeScnapscusZuZait()
				:
				this(null)
			{
			}

			public SictMengeScnapscusZuZait(SictWertMitZait<object>[] mengeScnapscus)
			{
				this.MengeScnapscus = mengeScnapscus;
			}
		}

		private void ButtonEveOnlineAutoKonfigScraibeNaacDatai_Drop(object sender, DragEventArgs e)
		{
			try
			{
				var DataiPfaad = Bib3.FCL.Glob.DataiPfaadAlsKombinatioonAusSctandardPfaadUndFileDrop(EveOnlinePräferenzDataiNaame, e);

				EveOnlinePräferenzScraibeNaacDataiUndBericteNaacGbs(DataiPfaad);
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}

		private void ButtonEveOnlineAutoKonfigLaadeVonDatai_Drop(object sender, DragEventArgs e)
		{
			try
			{
				var DataiPfaad = Bib3.FCL.Glob.DataiPfaadAlsKombinatioonAusSctandardPfaadUndFileDrop(EveOnlinePräferenzDataiNaame, e);

				EveOnlinePräferenzLaadeVonDataiPfaadUndBericteNaacGbs(DataiPfaad);
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}

		public string KonfigSictSeriel(object konfig)
		{
			if (null == konfig)
				return null;

			var KonfigSictSeriel = JsonConvert.SerializeObject(konfig, Formatting.Indented);

			return KonfigSictSeriel;
		}

		public byte[] KonfigSictListeOktet(object konfig)
		{
			var KonfigSictSerielAbbild = KonfigSictSeriel(konfig);

			if (null == KonfigSictSerielAbbild)
				return null;

			var KonfigSictListeOktet = Encoding.UTF8.GetBytes(KonfigSictSerielAbbild);

			return KonfigSictListeOktet;
		}

		public SictOptimatParam VonSictListeOktetKonfig(byte[] sictListeOktetAbbild)
		{
			return Optimat.Glob.ParseJsonUTF8<SictOptimatParam>(sictListeOktetAbbild);
		}

		public string EveOnlinePräferenzDataiNaame = "EveOnline.Preference";

		public string KonfigDataiNaame = "Konfig";

		public string EveOnlinePräferenzDataiPfaad()
		{
			return
				Optimat.Glob.ZuProcessSelbsctMainModuleDirectoryInfo() +
				System.IO.Path.DirectorySeparatorChar.ToString() +
				EveOnlinePräferenzDataiNaame;
		}

		public string KonfigDataiPfaad()
		{
			return
				Optimat.Glob.ZuProcessSelbsctMainModuleDirectoryInfo() +
				System.IO.Path.DirectorySeparatorChar.ToString() +
				KonfigDataiNaame;
		}

		public void EveOnlinePräferenzScraibeNaacDataiUndBericteNaacGbs(string dataiPfaad)
		{
			var Aktioon = new Action(() =>
			{
				var Konfig = EveOnlinePräferenzAingaabeLezte;

				Optimat.Glob.ScraibeNaacDataiMitPfaad(dataiPfaad, KonfigSictListeOktet(Konfig));
			});

			Optimat.GBS.Glob.VersuucAktioonMitBerictNaacTextBoxAnim(
				Aktioon,
				"write preferences to file (path= \"" + dataiPfaad + "\")",
				EveOnlineAutoKonfigAktioonErgeebnisBerict);
		}

		public void EveOnlinePräferenzLaadeVonDataiPfaadUndBericteNaacGbs(string dataiPfaad = null)
		{
			if (null == dataiPfaad)
				dataiPfaad = EveOnlinePräferenzDataiPfaad();

			var Aktioon = new Action(() =>
			{
				var KonfigSictListeOktetAbbild = Bib3.Glob.InhaltAusDataiMitPfaad(dataiPfaad);

				var Konfig = VonSictListeOktetKonfig(KonfigSictListeOktetAbbild);

				SctoierelementEveOnlinePräferenz.KonfigScraibeNaacGbs(Konfig);
			});

			Optimat.GBS.Glob.VersuucAktioonMitBerictNaacTextBoxAnim(
				Aktioon,
				"load preferences from file (path= \"" + dataiPfaad + "\")",
				EveOnlineAutoKonfigAktioonErgeebnisBerict);
		}

		private void ButtonEveOnlineAutoKonfigScraibeNaacDatai_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				var PräferenzDataiPfaad = EveOnlinePräferenzDataiPfaad();

				EveOnlinePräferenzScraibeNaacDataiUndBericteNaacGbs(PräferenzDataiPfaad);
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}

		private void ButtonEveOnlineAutoKonfigLaadeVonDatai_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				EveOnlinePräferenzLaadeVonDataiPfaadUndBericteNaacGbs();
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}

		private void ButtonEveOnlineAutoKonfigSezeZurük_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				EveOnlineAutoKonfigSezeZurük();
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}

		public void EveOnlineAutoKonfigSezeZurük()
		{
			var Aktioon = new Action(() =>
				{
					SctoierelementEveOnlinePräferenz.KonfigScraibeNaacGbs(EveOnlinePräferenzSctandardWert);
				});

			Optimat.GBS.Glob.VersuucAktioonMitBerictNaacTextBoxAnim(
				Aktioon,
				"settings reset to default",
				EveOnlineAutoKonfigAktioonErgeebnisBerict);
		}

		public TextBlock ButtonZiilProzesWirkungUnterbreceTooltipTextBlock
		{
			get
			{
				if (!IsInitialized)
				{
					return null;
				}

				return ((TextBlock)ButtonZiilProzesWirkungFraigaabe.ButtonLinxContent).ToolTip as TextBlock;
			}
		}


		private void ButtonKonfigScraibeNaacDatai_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				KonfigScraibeNaacDataiUndBericteNaacGbs();
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}

		private void ButtonKonfigScraibeNaacDatai_Drop(object sender, DragEventArgs e)
		{
			try
			{
				var DataiPfaad = Bib3.FCL.Glob.DataiPfaadAlsKombinatioonAusSctandardPfaadUndFileDrop(EveOnlinePräferenzDataiNaame, e);

				KonfigScraibeNaacDataiUndBericteNaacGbs(DataiPfaad);
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}

		private void ButtonKonfigLaadeVonDatai_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				KonfigLaadeVonDataiPfaadUndBericteNaacGbs();
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}

		private void ButtonKonfigLaadeVonDatai_Drop(object sender, DragEventArgs e)
		{
			try
			{
				var DataiPfaad = Bib3.FCL.Glob.DataiPfaadAlsKombinatioonAusSctandardPfaadUndFileDrop(EveOnlinePräferenzDataiNaame, e);

				KonfigLaadeVonDataiPfaadUndBericteNaacGbs(DataiPfaad);
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}

		public void KonfigSezeZurük()
		{
			var Aktioon = new Action(() =>
			{
				KonfigScraibeNaacGbs(KonfigSctandardWert);
			});

			Optimat.GBS.Glob.VersuucAktioonMitBerictNaacTextBoxAnim(
				Aktioon,
				"settings reset to default",
				KonfigAktioonErgeebnisBerict);
		}

		private void ButtonKonfigSezeZurük_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				KonfigSezeZurük();
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}

		public void KonfigScraibeNaacDataiUndBericteNaacGbs(string dataiPfaad = null)
		{
			if (null == dataiPfaad)
				dataiPfaad = KonfigDataiPfaad();

			var Aktioon = new Action(() =>
			{
				var Konfig = KonfigBerecneAusGbs();

				Optimat.Glob.ScraibeNaacDataiMitPfaad(dataiPfaad, KonfigSictListeOktet(Konfig));
			});

			Optimat.GBS.Glob.VersuucAktioonMitBerictNaacTextBoxAnim(
				Aktioon,
				"write Settings to file (path= \"" + dataiPfaad + "\")",
				KonfigAktioonErgeebnisBerict);
		}

		public void KonfigLaadeVonDataiPfaadUndBericteNaacGbs(string dataiPfaad = null)
		{
			if (null == dataiPfaad)
				dataiPfaad = KonfigDataiPfaad();

			KonfigSezeZurük();

			var Aktioon = new Action(() =>
			{
				var KonfigSictListeOktetAbbild = Bib3.Glob.InhaltAusDataiMitPfaad(dataiPfaad);

				var Konfig = Optimat.Glob.ParseJsonUTF8<SictKonfiguratioon>(KonfigSictListeOktetAbbild);

				KonfigScraibeNaacGbs(Konfig);
			});

			Optimat.GBS.Glob.VersuucAktioonMitBerictNaacTextBoxAnim(
				Aktioon,
				"load Settings from file (path= \"" + dataiPfaad + "\")",
				KonfigAktioonErgeebnisBerict);
		}

		static char? ZuKeyBerecneChar(Key key)
		{
			if (Key.A <= key && key <= Key.Z)
				return (char)((int)key - Key.A + (int)'A');

			if (Key.D0 <= key && key <= Key.D9)
				return (char)((int)key - Key.D0 + (int)'0');

			if (Key.Space == key)
				return ' ';

			return null;
		}

		private void UserControl_KeyUp(object sender, KeyEventArgs e)
		{
			if (!IsInitialized)
			{
				return;
			}

			ZiilProcessWirkungPauseAuswaalMengeKeyKombi.Aktualisiire();
		}

		private void UserControl_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (!IsInitialized)
				return;

			ZiilProcessWirkungPauseAuswaalMengeKeyKombi.Aktualisiire();
		}

		private void ButtonErwaitertFraigaabe_Click(object sender, RoutedEventArgs e)
		{
			ErwaitertFraigaabe = true;

			TabItemErwaitert.Visibility = Visibility.Visible;

			ButtonErwaitertFraigaabe.IsEnabled = false;
		}

		public Optimat.EveO.Nuzer.CustomBotServer CustomBotServer
		{
			get
			{
				if (!ErwaitertFraigaabe)
				{
					return null;
				}

				var ControlErwaitert = this.ControlErwaitert;

				if (null == ControlErwaitert)
				{
					return null;
				}

				return ControlErwaitert.CustomBotServer;
			}
		}

		public void RepräsentiireErwaitert()
		{
			if (!ErwaitertFraigaabe)
			{
				return;
			}

			var ControlErwaitert = this.ControlErwaitert;

			if (null == ControlErwaitert)
			{
				return;
			}

			ControlErwaitert.Present();
		}

		private void SizungBerictVerzaicnisPfaad_Drop(object sender, DragEventArgs e)
		{
			TextBoxSizungBerictVerzaicnisPfaad.Text = Bib3.FCL.Glob.FrüühestDataiPfaadAusDropFileDrop(e);
		}

		public PropertyGenTimespanInt64<Sanderling.Interface.MemoryStruct.IMemoryMeasurement> MemoryMeasurementSimulationValue
		{
			private set;
			get;
		}

		void MemoryMeasurementSimulationUIUpdate() =>
			MemoryMeasurementSimulationEnabledPanel.Visibility = null == MemoryMeasurementSimulationValue ? Visibility.Hidden : Visibility.Visible;

		private void MemoryMeasurementSimulationDisableButton_Click(object sender, RoutedEventArgs e)
		{
			MemoryMeasurementSimulationValue = null;
			MemoryMeasurementSimulationUIUpdate();
		}

		private void MemoryMeasurementSimulationEnableButton_Drop(object sender, DragEventArgs e)
		{
			Bib3.FCL.GBS.Extension.CatchNaacMessageBoxException(() =>
			{
				var filePathAndContent = e?.LaadeMengeDataiInhaltAusDropFileDrop()?.FirstOrDefault();

				var fileContentUTF8 = Encoding.UTF8.GetString(filePathAndContent?.Value);

				var setRoot = Bib3.RefNezDiferenz.Extension.ListeWurzelDeserialisiireVonJson(fileContentUTF8);

				var root = setRoot?.FirstOrDefault();

				var memoryMeasurement =
					root as Sanderling.Interface.MemoryStruct.IMemoryMeasurement ??
					(root as FromProcessMeasurement<Sanderling.Interface.MemoryStruct.IMemoryMeasurement>)?.Value;

				MemoryMeasurementSimulationValue = new PropertyGenTimespanInt64<Sanderling.Interface.MemoryStruct.IMemoryMeasurement>(memoryMeasurement, Bib3.Glob.StopwatchZaitMiliSictInt());
			});

			MemoryMeasurementSimulationUIUpdate();
		}

		private void MemoryMeasurementLastToFileButton_Drop(object sender, DragEventArgs e)
		{
			Bib3.FCL.GBS.Extension.CatchNaacMessageBoxException(() =>
			{
				var fromProcessMeasurementLast = FromProcessMeasurementLastGetDelegate?.Invoke();

				var fromProcessMeasurementLastSerial = Bib3.RefNezDiferenz.Extension.ListeWurzelSerialisiireZuJson(new[] { fromProcessMeasurementLast }, Sanderling.Interface.FromInterfaceResponse.SerialisPolicyCache);

				var fromProcessMeasurementLastSerialUtf8 = Encoding.UTF8.GetBytes(fromProcessMeasurementLastSerial);

				var filePath = Bib3.FCL.Glob.DataiPfaadAlsKombinatioonAusSctandardPfaadUndFileDrop("memory_measurement", e);

				Bib3.Glob.ScraibeInhaltNaacDataiPfaad(filePath, fromProcessMeasurementLastSerialUtf8);
			});

			MemoryMeasurementSimulationUIUpdate();
		}
	}
}
