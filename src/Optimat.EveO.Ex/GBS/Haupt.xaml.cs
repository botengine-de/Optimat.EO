using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Optimat.EveOnline;
using Optimat.EveOnline.Base;
using Optimat.GBS;
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

		readonly Queue<char> ListeTasteCharAin = new Queue<char>();

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

			Enumerable.Range(0, 8).ForEachNullable((EkeIndex) =>
				{
					ComboBoxSimuMausAufWindowVordersteEkeIndex.Items.Add(EkeIndex);
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
			/*
			 * 2015.03.04
			 * 
				Int64? ScnitOptimatServerVersuucVerbindungZaitDistanz = (int)(SliderScnitOptimatServerKonfigVersuucVerbindungZaitDistanz.Value);
			 * */
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

			/*
			 * 2015.03.04
			 * 
				Konfig.ScnitOptimatServerVersuucVerbindungZaitDistanz = ScnitOptimatServerVersuucVerbindungZaitDistanz;
			 * */

			Konfig.BerictHauptPersistVerzaicnisPfaad = BerictHauptPersistVerzaicnisPfaad;
			Konfig.BerictHauptPersistKapazitäätScranke = BerictHauptPersistKapazitäätScranke;

			Konfig.BerictSizungPersistVerzaicnisPfaad = TextBoxSizungBerictVerzaicnisPfaad.Text;

			Konfig.BerictWindowClientRasterPersistVerzaicnisPfaad = BerictWindowClientRasterPersistVerzaicnisPfaad;
			Konfig.BerictWindowClientRasterPersistKapazitäätScranke = BerictWindowClientRasterPersistKapazitäätScranke;

			return Konfig;
		}

		public void KonfigScraibeNaacGbs(SictKonfiguratioon Konfig)
		{
			if (!IsInitialized)
			{
				return;
			}

			var AuswaalZiilProcess = (null == Konfig) ? null : Konfig.AuswaalZiilProcess;

			var ZiilProcessWirkungPauseMengeKeyKombi = (null == Konfig) ? null : Konfig.ZiilProcessWirkungPauseMengeKeyKombi;

			var ScnitOptimatServerLizenzDataiPfaad = (null == Konfig) ? null : Konfig.ScnitOptimatServerLizenzDataiPfaad;
			var ScnitOptimatServerVersuucVerbindungZaitDistanz = (null == Konfig) ? null : Konfig.ScnitOptimatServerVersuucVerbindungZaitDistanz;
			var BerictHauptPersistVerzaicnisPfaad = (null == Konfig) ? null : Konfig.BerictHauptPersistVerzaicnisPfaad;
			var BerictHauptPersistKapazitäätScranke = (null == Konfig) ? null : Konfig.BerictHauptPersistKapazitäätScranke;
			var BerictWindowClientRasterPersistVerzaicnisPfaad = (null == Konfig) ? null : Konfig.BerictWindowClientRasterPersistVerzaicnisPfaad;
			var BerictWindowClientRasterPersistKapazitäätScranke = (null == Konfig) ? null : Konfig.BerictWindowClientRasterPersistKapazitäätScranke;
			var BerictSizungPersistVerzaicnisPfaad = (null == Konfig) ? null : Konfig.BerictSizungPersistVerzaicnisPfaad;

			ZiilProcessAuswaalZiilProcess.PräferenzScraibeNaacGbs(AuswaalZiilProcess);

			ZiilProcessWirkungPauseAuswaalMengeKeyKombi.MengeKeyKombiSeze(ZiilProcessWirkungPauseMengeKeyKombi);

			TextBoxOptimatServerVerbindungLizenzDataiPfaad.Text = ScnitOptimatServerLizenzDataiPfaad;

			var SensorServerApiUri = Konfig?.SensorServerApiUri;

			if(SensorServerApiUri.NullOderLeer())
			{
				SensorServerApiUri = App.BotEngineApiUriDefault;
			}

			TextBoxSensorServerApiUri.Text = SensorServerApiUri;
			/*
			 * 2015.03.04
			 * 
				SliderScnitOptimatServerKonfigVersuucVerbindungZaitDistanz.Value = ScnitOptimatServerVersuucVerbindungZaitDistanz ?? 10;
			 * */

			BerictPersistAingaabeBerictHaupt.Pfaad = BerictHauptPersistVerzaicnisPfaad;
			BerictPersistAingaabeBerictHaupt.Kapazitäät = BerictHauptPersistKapazitäätScranke;

			BerictPersistAingaabeBerictWindowClientRaster.Pfaad = BerictWindowClientRasterPersistVerzaicnisPfaad;
			BerictPersistAingaabeBerictWindowClientRaster.Kapazitäät = BerictWindowClientRasterPersistKapazitäätScranke;

			TextBoxSizungBerictVerzaicnisPfaad.Text = BerictSizungPersistVerzaicnisPfaad;
		}

		void ZiilProcessAuswaalZiilProcessRicteMengeProcessBegin(
			bool SortiireNaacBewertungPunkteAnzaal,
			bool SelectedItemsSezeAufGrupeBewertungPunktAnzaalMax)
		{
			ZiilProcessAuswaalZiilProcess.MengeProcessRicteNaacSelbsctBegin(SortiireNaacBewertungPunkteAnzaal, SelectedItemsSezeAufGrupeBewertungPunktAnzaalMax);
		}

		/*
		 * 2015.03.04
		 * 
		private void SliderScnitOptimatServerKonfigVersuucVerbindungZaitDistanz_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			TextBoxScnitOptimatServerKonfigVersuucVerbindungZaitDistanzAktualisiire();
		}
		 * */

		/*
		 * 2015.03.04
		 * 
		public void TextBoxScnitOptimatServerKonfigVersuucVerbindungZaitDistanzAktualisiire()
		{
			if (!IsInitialized)
			{
				return;
			}

			TextBoxScnitOptimatServerKonfigVersuucVerbindungZaitDistanz.Text = ((int)SliderScnitOptimatServerKonfigVersuucVerbindungZaitDistanz.Value).ToString();
		}
		 * */

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			/*
			 * 2015.03.04
			 * 
				TextBoxScnitOptimatServerKonfigVersuucVerbindungZaitDistanzAktualisiire();
			 * */
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

			public SictMengeScnapscusZuZait(SictWertMitZait<object>[] MengeScnapscus)
			{
				this.MengeScnapscus = MengeScnapscus;
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

		public string KonfigSictSeriel(object Konfig)
		{
			if (null == Konfig)
			{
				return null;
			}

			var KonfigSictSeriel = JsonConvert.SerializeObject(Konfig, Formatting.Indented);

			return KonfigSictSeriel;
		}

		public byte[] KonfigSictListeOktet(object Konfig)
		{
			var KonfigSictSerielAbbild = KonfigSictSeriel(Konfig);

			if (null == KonfigSictSerielAbbild)
			{
				return null;
			}

			var KonfigSictListeOktet = Encoding.UTF8.GetBytes(KonfigSictSerielAbbild);

			return KonfigSictListeOktet;
		}

		public SictOptimatParam VonSictListeOktetKonfig(byte[] SictListeOktetAbbild)
		{
			return Optimat.Glob.ParseJsonUTF8<SictOptimatParam>(SictListeOktetAbbild);
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

		public void EveOnlinePräferenzScraibeNaacDataiUndBericteNaacGbs(string DataiPfaad)
		{
			var Aktioon = new Action(() =>
			{
				/*
				 * 2014.09.20
				 * 
				var Konfig = SctoierelementEveOnlinePräferenz.KonfigBerecneAusGbs();
				 * */

				var Konfig = EveOnlinePräferenzAingaabeLezte;

				Optimat.Glob.ScraibeNaacDataiMitPfaad(DataiPfaad, KonfigSictListeOktet(Konfig));
			});

			Optimat.GBS.Glob.VersuucAktioonMitBerictNaacTextBoxAnim(
				Aktioon,
				"write preferences to file (path= \"" + DataiPfaad + "\")",
				EveOnlineAutoKonfigAktioonErgeebnisBerict);
		}

		public void EveOnlinePräferenzLaadeVonDataiPfaadUndBericteNaacGbs(string DataiPfaad = null)
		{
			if (null == DataiPfaad)
			{
				DataiPfaad = EveOnlinePräferenzDataiPfaad();
			}

			var Aktioon = new Action(() =>
			{
				var KonfigSictListeOktetAbbild = Bib3.Glob.InhaltAusDataiMitPfaad(DataiPfaad);

				var Konfig = VonSictListeOktetKonfig(KonfigSictListeOktetAbbild);

				SctoierelementEveOnlinePräferenz.KonfigScraibeNaacGbs(Konfig);
			});

			Optimat.GBS.Glob.VersuucAktioonMitBerictNaacTextBoxAnim(
				Aktioon,
				"load preferences from file (path= \"" + DataiPfaad + "\")",
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

		public void KonfigScraibeNaacDataiUndBericteNaacGbs(string DataiPfaad = null)
		{
			if (null == DataiPfaad)
			{
				DataiPfaad = KonfigDataiPfaad();
			}

			var Aktioon = new Action(() =>
			{
				var Konfig = KonfigBerecneAusGbs();

				Optimat.Glob.ScraibeNaacDataiMitPfaad(DataiPfaad, KonfigSictListeOktet(Konfig));
			});

			Optimat.GBS.Glob.VersuucAktioonMitBerictNaacTextBoxAnim(
				Aktioon,
				"write Settings to file (path= \"" + DataiPfaad + "\")",
				KonfigAktioonErgeebnisBerict);
		}

		public void KonfigLaadeVonDataiPfaadUndBericteNaacGbs(string DataiPfaad = null)
		{
			if (null == DataiPfaad)
			{
				DataiPfaad = KonfigDataiPfaad();
			}

			KonfigSezeZurük();

			var Aktioon = new Action(() =>
			{
				var KonfigSictListeOktetAbbild = Bib3.Glob.InhaltAusDataiMitPfaad(DataiPfaad);

				var Konfig = Optimat.Glob.ParseJsonUTF8<SictKonfiguratioon>(KonfigSictListeOktetAbbild);

				KonfigScraibeNaacGbs(Konfig);
			});

			Optimat.GBS.Glob.VersuucAktioonMitBerictNaacTextBoxAnim(
				Aktioon,
				"load Settings from file (path= \"" + DataiPfaad + "\")",
				KonfigAktioonErgeebnisBerict);
		}

		static char? ZuKeyBerecneChar(Key Key)
		{
			if (Key.A <= Key && Key <= Key.Z)
			{
				return (char)((int)Key - Key.A + (int)'A');
			}

			if (Key.D0 <= Key && Key <= Key.D9)
			{
				return (char)((int)Key - Key.D0 + (int)'0');
			}

			if (Key.Space == Key)
			{
				return ' ';
			}

			return null;
		}

		static readonly string TabInspektVisibilitySclüsel = "INSPEKT AIN";

		void TabInspektVisibilityAktualisiire()
		{
			var ListeTasteCharAinAggr = string.Join("", ListeTasteCharAin.ToArray());

			if (ListeTasteCharAinAggr.Contains(TabInspektVisibilitySclüsel))
			{
				TabItemInspekt.Visibility = System.Windows.Visibility.Visible;
			}
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
			{
				return;
			}

			if (null != e)
			{
				var Char = ZuKeyBerecneChar(e.Key);

				if (Char.HasValue)
				{
					ListeTasteCharAin.Enqueue(Char.Value);

					Bib3.Extension.ListeKürzeBegin(ListeTasteCharAin, 100);

					TabInspektVisibilityAktualisiire();
				}
			}

			ZiilProcessWirkungPauseAuswaalMengeKeyKombi.Aktualisiire();
		}

		private void ButtonErwaitertFraigaabe_Click(object sender, RoutedEventArgs e)
		{
			ErwaitertFraigaabe = true;

			TabItemErwaitert.Visibility = Visibility.Visible;

			ButtonErwaitertFraigaabe.IsEnabled = false;
		}

		/*
		 * 2015.02.16
		 * 
		public UserscriptHost UserscriptHost
		{
			get
			{
				if (!ErwaitertFraigaabe)
				{
					return	null;
				}

				var ControlErwaitert = this.ControlErwaitert;

				if (null == ControlErwaitert)
				{
					return null;
				}

				return ControlErwaitert.UserscriptHost;
			}
		}
		 * */

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
	}
}
