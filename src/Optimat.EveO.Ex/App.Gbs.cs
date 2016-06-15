using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using Newtonsoft.Json;
using System.IO;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using Bib3;
using Optimat.GBS;
using Optimat.EveOnline;
using Optimat.EveOnline.Base;
using Optimat.EveOnline.CustomBot;
using ExtractFromOldAssembly.Bib3;
using Sanderling;

namespace Optimat.EveO.Nuzer
{
	public enum SictSctaatusSictEnum
	{
		Feeler = 2,
		Gehalte = 3,
		Loift = 4,
	}

	public partial class App
	{
		/*
		 * 2014.02.02
		 * 
		readonly List<KeyValuePair<Chrome.RemoteDebugging.SictChromeRemoteDebuggingNaacrict, bool>> GbsFürBrowserScnitRemoteDebuggingInspektListeNaacrict =
			new List<KeyValuePair<Chrome.RemoteDebugging.SictChromeRemoteDebuggingNaacrict, bool>>();

		readonly List<KeyValuePair<Chrome.RemoteDebugging.SictChromeRemoteDebuggingNaacrict, bool>> GbsFürBrowserScnitAnwendungInspektListeNaacrict =
			new List<KeyValuePair<Chrome.RemoteDebugging.SictChromeRemoteDebuggingNaacrict, bool>>();
		 * */

		static System.Windows.Input.Key[] ZiilProcessWirkungPauseKeyKombiSctandard = new System.Windows.Input.Key[]{
			System.Windows.Input.Key.LeftCtrl,
			System.Windows.Input.Key.LeftAlt};

		SictOptimatScrit OptimatScritZwiscescpaicer;

		int? GbsAingaabeZiilProcessId;

		IntPtr GbsAingaabeWaalZiilProcessMainWindowHandle;

		//	bool AingaabeUserscriptActive = false;
		bool AingaabeErwaitertFraigaabe = false;


		static public SictSctaatusSictEnum? Kombiniire(
			SictSctaatusSictEnum? O0,
			SictSctaatusSictEnum? O1)
		{
			if (!O0.HasValue)
			{
				return O1;
			}

			if (!O1.HasValue)
			{
				return O0;
			}

			return (SictSctaatusSictEnum)Math.Min((int)O0.Value, (int)O1.Value);
		}

		static Brush ZuSctaatusBrush(SictSctaatusSictEnum? SctaatusNulbar)
		{
			if (!SctaatusNulbar.HasValue)
			{
				return null;
			}

			switch (SctaatusNulbar.Value)
			{
				case SictSctaatusSictEnum.Feeler:
					return Brushes.Red;
				case SictSctaatusSictEnum.Gehalte:
					return Brushes.Gold;
				case SictSctaatusSictEnum.Loift:
					return Brushes.LimeGreen;
			}

			return null;
		}

		static public string TimeSpanSictString(TimeSpan? TimeSpan)
		{
			if (!TimeSpan.HasValue)
			{
				return null;
			}

			var InTaagBetraagSictString = string.Format("{0:hh\\:mm\\:ss}", TimeSpan.Value);

			var BetraagSictString = InTaagBetraagSictString;

			if (1 <= Math.Abs(TimeSpan.Value.TotalDays))
			{
				BetraagSictString = string.Format("{0:dd}", TimeSpan.Value) + " " + InTaagBetraagSictString;
			}

			if (TimeSpan.Value.TotalMilliseconds < 0)
			{
				return "-" + BetraagSictString;
			}

			return BetraagSictString;
		}

		bool GbsAingaabeEveOnlnWirkungFraigaabe = false;

		bool GbsAingaabeEveOnlnAutoWirkungSetForegroundNict = false;
		bool GbsAingaabeEveOnlnSimuFraigaabe = false;
		bool GbsAingaabeSimuAufgaabeDistanceScteleAinObjektNääxteFraigaabe = false;
		Int64? GbsAingaabeSimuAufgaabeDistanceScteleAinObjektNääxteDistanceSol = null;
		bool? GbsAingaabeSimuOverviewScroll = null;
		int? GbsAingaabeSimuMausAufWindowVordersteEkeIndex = null;
		ShipState GbsAingaabeEveOnlnSimuSelbstShipZuusctand = null;
		SictDamageTypeSictEnum? GbsAingaabeVorgaabeDamageType = null;
		bool? GbsAingaabeSimuAnforderungFittingIgnoriire = null;

		string GbsAingaabeScnitOptimatServerLizenzDataiPfaad;
		string GbsAingaabeSensorServerApiUri;
		Int64? GbsAingaabeScnitOptimatServerVersuucVerbindungZaitDistanz;

		SictOptimatParam GbsAingaabeEveOnlinePräferenz;

		static public IntPtr ZuProcessIdMainWindowHandle(int? ProcessId)
		{
			if (!ProcessId.HasValue)
			{
				return IntPtr.Zero;
			}

			try
			{
				var Process = System.Diagnostics.Process.GetProcessById(ProcessId.Value);

				return ZuProcessMainWindowHandle(Process);
			}
			catch { }

			return IntPtr.Zero;
		}

		public int GbsFürZiilprozesWaalWindowProzesId()
		{
			var GbsAingaabeWaalZiilProzesWindowHandle = this.GbsAingaabeWaalZiilProcessMainWindowHandle;

			var Prozes = Optimat.Glob.ProzesZuWindowHandle(GbsAingaabeWaalZiilProzesWindowHandle);

			return Prozes.Id;
		}

		static public IntPtr ZuProcessMainWindowHandle(System.Diagnostics.Process Process)
		{
			if (null == Process)
			{
				return IntPtr.Zero;
			}

			return Process.MainWindowHandle;
		}

		static readonly public string ButtonZiilProzesWirkungUnterbreceTooltipTextBegin =
			"pause generating input by clicking here or by using one of the following keyboard shortcuts:";

		static public string ButtonZiilProzesWirkungUnterbreceTooltipTextBerecne(
			IEnumerable<System.Windows.Input.Key[]> MengeKeyKombiMengeKey)
		{
			var StringBuilder = new StringBuilder(ButtonZiilProzesWirkungUnterbreceTooltipTextBegin);

			int KeyKombiAnzaal = 0;

			if (null != MengeKeyKombiMengeKey)
			{
				foreach (var KeyKombiMengeKey in MengeKeyKombiMengeKey)
				{
					if (null == KeyKombiMengeKey)
					{
						continue;
					}

					if (KeyKombiMengeKey.Length < 1)
					{
						continue;
					}

					StringBuilder.Append(Environment.NewLine + Optimat.GBS.SictKeyKombiRepr.MengeKeySictString(KeyKombiMengeKey));
					++KeyKombiAnzaal;
				}
			}

			if (KeyKombiAnzaal < 1)
			{
				StringBuilder.Append(Environment.NewLine + "error, no keyboard shortcuts available");
			}

			return StringBuilder.ToString();
		}

		static public string ButtonZiilProzesWirkungUnterbreceTooltipTextBerecne(
			SictKonfiguratioon Konfig)
		{
			var ZiilProcessWirkungPauseMengeKeyKombi = (null == Konfig) ? null : Konfig.ZiilProcessWirkungPauseMengeKeyKombi;

			return ButtonZiilProzesWirkungUnterbreceTooltipTextBerecne(ZiilProcessWirkungPauseMengeKeyKombi);
		}

		SictKonfiguratioon GbsAingaabeKonfig = null;

		GBS.Haupt GbsSctoierelementHaupt
		{
			set
			{
				MainWindow.Content = value;
			}

			get
			{
				return MainWindow?.Content as GBS.Haupt;
			}
		}

		void GbsSctoierelementHauptErsctele()
		{
			var SctoierelementHaupt = new GBS.Haupt
			{
				FromProcessMeasurementLastGetDelegate = () => FromProcessMeasurementLast,
			};

			this.GbsSctoierelementHaupt = SctoierelementHaupt;

			SctoierelementHaupt.EveOnlinePräferenzSctandardWert = EveOnlinePräferenzSctandardWertBerecne();

			SctoierelementHaupt.KonfigSctandardWert = KonfigSctandardWertBerecne();

			/*
			 * 2015.03.03
			 * 
				SctoierelementHaupt.ButtonOptimatServerListeVerbindungLezteTreneJezt.Click +=
					ButtonOptimatServerListeVerbindungLezteTreneJezt_Click;

				SctoierelementHaupt.ButtonInspektOptimatScritZwiscescpaicerVonProcessGbsBaumScraibeNaacDatai.Drop +=
					ButtonInspektOptimatScritZwiscescpaicerVonProcessGbsBaumScraibeNaacDatai_Drop;
			 * */

			SctoierelementHaupt.ButtonInspektOptimatScritZwiscescpaicerWindowClientRasterScraibeNaacDatai.Drop += ButtonInspektOptimatScritZwiscescpaicerWindowClientRasterScraibeNaacDatai_Drop;
		}

		void ButtonInspektOptimatScritZwiscescpaicerWindowClientRasterScraibeNaacDatai_Drop(object sender, DragEventArgs e)
		{
			try
			{
				var ZiilPfaad = Bib3.FCL.Glob.DataiPfaadAlsKombinatioonAusSctandardPfaadUndFileDrop("Optimat.Scrit.Window.Client.Raster", e);

				var OptimatScritZwiscescpaicer = this.OptimatScritZwiscescpaicer;

				if (null == OptimatScritZwiscescpaicer)
				{
					throw new ArgumentNullException("OptimatScritZwiscescpaicer");
				}

				var VonWindowLeese = OptimatScritZwiscescpaicer.VonWindowLeese;

				if (null == VonWindowLeese)
				{
					throw new ArgumentNullException("VonWindowLeese");
				}

				var ClientRasterDataiPfaad = VonWindowLeese.ClientRasterZiilDataiPfaad;

				if (null == ClientRasterDataiPfaad)
				{
					throw new ArgumentNullException("ClientRasterDataiPfaad");
				}

				File.Copy(ClientRasterDataiPfaad, ZiilPfaad, true);
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}

		/*
		 * 2015.03.03
		 * 
		void ButtonEveOnlineAutoVonNuzerParamScraibeNaacDatai_Drop(object sender, DragEventArgs e)
		{
			try
			{
				var ZiilPfaad = Bib3.FCL.Glob.DataiPfaadAlsKombinatioonAusSctandardPfaadUndFileDrop("Auto.VonNuzerParam", e);

				var AutoVonNuzerParam = this.TempNaacAnwendungZuMeldeAutomaatParam;

				if (null == AutoVonNuzerParam)
				{
					throw new ArgumentNullException("AutoVonNuzerParam");
				}

				var AutoVonNuzerParamSictStringAbbild = JsonConvert.SerializeObject(AutoVonNuzerParam, Formatting.Indented);

				Bib3.Glob.ScraibeInhaltNaacDataiPfaad(ZiilPfaad, Encoding.UTF8.GetBytes(AutoVonNuzerParamSictStringAbbild));
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}

		void ButtonInspektOptimatScritZwiscescpaicerVonProcessGbsBaumScraibeNaacDatai_Drop(object sender, DragEventArgs e)
		{
			try
			{
				var ZiilPfaad = Bib3.FCL.Glob.DataiPfaadAlsKombinatioonAusSctandardPfaadUndFileDrop("Optimat.Scrit.GBS.Baum", e);

				var OptimatScritZwiscescpaicer = this.OptimatScritZwiscescpaicer;

				if (null == OptimatScritZwiscescpaicer)
				{
					throw new ArgumentNullException("OptimatScritZwiscescpaicer");
				}

				var VonZiilProcessLeese = OptimatScritZwiscescpaicer.VonProcessLeese;

				if (null == VonZiilProcessLeese)
				{
					throw new ArgumentNullException("VonZiilProcessLeese");
				}

				var GbsBaum = VonZiilProcessLeese.ErgeebnisGbsBaum;

				if (null == GbsBaum)
				{
					throw new ArgumentNullException("GbsBaum");
				}

				var SerializerSettings = new JsonSerializerSettings()
				{
					DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore,
					Formatting = Formatting.Indented
				};

				var GbsBaumSictStringAbbild = JsonConvert.SerializeObject(GbsBaum, SerializerSettings);

				Bib3.Glob.ScraibeInhaltNaacDataiPfaad(ZiilPfaad, Encoding.UTF8.GetBytes(GbsBaumSictStringAbbild));
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}

		static public byte[] FürScnapscusTailGbsBaumSictSeriel(GbsAstInfo GbsBaumWurzelAst)
		{
			if (null == GbsBaumWurzelAst)
			{
				return null;
			}

			var SerializeSettings = new JsonSerializerSettings();

			SerializeSettings.DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate;

			var WurzelAstSictStringAbbild = JsonConvert.SerializeObject(GbsBaumWurzelAst, Formatting.Indented, SerializeSettings);

			var WurzelAstSictUTF8 = Encoding.UTF8.GetBytes(WurzelAstSictStringAbbild);

			return WurzelAstSictUTF8;
		}

		void ButtonEveOnlineSensoScnapscusUnabhängigScraibeNaacDatai_Drop(object sender, DragEventArgs e)
		{
			try
			{
				var EveOnlnSensoScnapscusUnabhängigAuswertLezteFertig = this.EveOnlnSensoScnapscusUnabhängigAuswertLezteFertig;

				if (null == EveOnlnSensoScnapscusUnabhängigAuswertLezteFertig)
				{
					throw new ArgumentNullException("EveOnlnSensoScnapscusUnabhängigAuswertLezteFertig");
				}

				var GbsBaumWurzelInfo = EveOnlnSensoScnapscusUnabhängigAuswertLezteFertig.Wert;

				if (null == GbsBaumWurzelInfo)
				{
					throw new ArgumentNullException("GbsBaumWurzelInfo");
				}

				var ZiilPfaad = Bib3.FCL.Glob.DataiPfaadAlsKombinatioonAusSctandardPfaadUndFileDrop("Scnapscus.Auswert.Unabhängig", e);

				var SictSerielAbbild = FürScnapscusTailGbsBaumSictSeriel(GbsBaumWurzelInfo);

				Bib3.Glob.ScraibeInhaltNaacDataiPfaad(ZiilPfaad, SictSerielAbbild);
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}

		void ButtonEveOnlineSensoScnapscusUnabhängigErscteleJezt_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				var ProcessId = GbsFürZiilprozesWaalWindowProzesId();

				var ScnapscusAuswert = new SictProzesAuswertZuusctandScpezGbsBaum(ProcessId, EveOnlnSensoWurzelSuuceLezteTask.Wert.Result);

				ScnapscusAuswert.BerecneScrit();

				EveOnlnSensoScnapscusUnabhängigAuswertLezteFertig =
					new SictVerlaufBeginUndEndeRef<GbsAstInfo>(
						ScnapscusAuswert.ScritLezteBeginZaitMili,
						ScnapscusAuswert.ScritLezteEndeZaitMili,
						(ScnapscusAuswert.MengeGbsWurzelInfo ?? new GbsAstInfo[0]).FirstOrDefault());
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}

		void ButtonEveOnlineSensoorikScnapscusErscteleJezt_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				ScnapscusErscteleAinzelLezteZaitMili = Bib3.Glob.StopwatchZaitMikroSictInt() / 1000;
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}

		void ButtonEveOnlnSimuVonSensoScnapscus_Drop(object sender, DragEventArgs e)
		{
			try
			{
				var ZaitMikro = Bib3.Glob.StopwatchZaitMikroSictInt();

				var ZaitMili = ZaitMikro / 1000;

				var Datai = Bib3.FCL.Glob.LaadeFrüühestInhaltDataiAusDropFileDrop(e);

				var SictString = Encoding.UTF8.GetString(Datai.Value);

				var GbsAst = JsonConvert.DeserializeObject<GbsAstInfo>(SictString);

				NaacAnwendungZuMeldeGbsBaumWurzel = new SictVerlaufBeginUndEndeRef<GbsAstInfo>(
					ZaitMili, ZaitMili, GbsAst);
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}

		void GbsEveOnlnButtonEveOnlnSensoWurzelBerecne_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				var GbsAingaabeZiilProcessId = this.GbsAingaabeZiilProcessId;

				if (!GbsAingaabeZiilProcessId.HasValue)
				{
					throw new ArgumentNullException("GbsAingaabeZiilProcessId");
				}

				EveOnlnSensoWurzelSuuceErsctele(GbsAingaabeZiilProcessId.Value);
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}

		void ButtonOptimatServerListeVerbindungLezteTreneJezt_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			ScnitOptimatServerListeVerbindungLezteScnitTcpSezeTrenungZaitJezt();
		}
		 * */

		static public void VersuucScraibeNaacDataiMitBerictNaacTextBoxAnim(
			string ZiilDataiPfaad,
			byte[] ZiilDataiInhalt,
			Optimat.GBS.BerictTextUndAnim BerictRepräsentatioon)
		{
			if (null == ZiilDataiPfaad)
			{
				return;
			}

			var BeginZaitMikro = Bib3.Glob.StopwatchZaitMikroSictInt();

			var BeginZaitDateTime = Bib3.Glob.SictDateTimeVonStopwatchZaitMikro(BeginZaitMikro);

			var BeginZaitSictKalenderString = Bib3.Glob.SictwaiseKalenderString(BeginZaitDateTime, ".", 0);

			ZiilDataiInhalt = ZiilDataiInhalt ?? new byte[0];

			System.Exception Exception = null;

			FileInfo Datai = null;

			try
			{
				Datai = new FileInfo(ZiilDataiPfaad);

				var DataiStream = Datai.Create();

				try
				{
					DataiStream.Write(ZiilDataiInhalt, 0, ZiilDataiInhalt.Length);
				}
				finally
				{
					DataiStream.Close();
				}
			}
			catch (System.Exception TempException)
			{
				Exception = TempException;
			}

			var Erfolg = null == Exception;

			string DataiFullName = (null == Datai) ? null : Datai.FullName;

			var BerictTail0 =
				BeginZaitSictKalenderString + Environment.NewLine +
				"tried to write to \"" + ZiilDataiPfaad + "\"" + Environment.NewLine +
				"(full path = " + ((null == DataiFullName) ? "" : ("\"" + DataiFullName + "\"")) + ")" + Environment.NewLine +
				(Erfolg ? "success" : "error") + Environment.NewLine;

			var BerictString =
				BerictTail0 + "(" +
				((Erfolg) ? "" : Optimat.Glob.ExceptionSictString(Exception) ?? "") +
				")";

			if (null != BerictRepräsentatioon)
			{
				BerictRepräsentatioon.Seze(BerictString, Erfolg);
			}
		}

		void MemoryreadInterfaceConfigApplyFromUI() =>
			sensorServerDispatcher.LicenseClientConfig = GbsSctoierelementHaupt?.LicenseView?.LicenseClientConfigViewModel?.PropagateFromDependencyPropertyToClrMember().CompletedWithDefault();

		void GbsAingaabeLeese()
		{
			MemoryreadInterfaceConfigApplyFromUI();

			var GbsSctoierelementHaupt = this.GbsSctoierelementHaupt;

			SimulateMemoryMeasurement = GbsSctoierelementHaupt?.MemoryMeasurementSimulationValue?.Value;

			IntPtr GbsAingaabeWaalZiilProcessMainWindowHandle = IntPtr.Zero;

			bool GbsAingaabeEveOnlnAutoWirkungFraigaabe = false;

			bool GbsAingaabeEveOnlnAutoWirkungSetForegroundNict = false;
			SictDamageTypeSictEnum? VorgaabeDamageType = null;
			bool? GbsAingaabeSimuAnforderungFittingIgnoriire = null;

			bool GbsAingaabeEveOnlnSimuFraigaabe = false;
			ShipState GbsAingaabeEveOnlnSimuSelbstShipZuusctand = null;
			bool? GbsAingaabeSimuAufgaabeDistanceScteleAinObjektNääxteFraigaabe = null;
			Int64? GbsAingaabeSimuAufgaabeDistanceScteleAinObjektNääxteDistanceSol = null;
			bool? GbsAingaabeSimuOverviewScroll = null;
			int? GbsAingaabeSimuMausAufWindowVordersteEkeIndex = null;

			string GbsAingaabeScnitOptimatServerLizenzDataiPfaad = null;
			/*
			 * 2015.03.04
			 * 
				Int64? GbsAingaabeScnitOptimatServerVersuucVerbindungZaitDistanz = null;
			 * */

			SictKonfiguratioon AingaabeKonfig = null;
			int? GbsAingaabeZiilProcessId = null;

			try
			{
				if (GbsSctoierelementHaupt == null)
				{
					return;
				}

				GbsAingaabeSensorServerApiUri = GbsSctoierelementHaupt?.TextBoxSensorServerApiUri?.Text;

				this.MengeSizungBerictVerzaicnisPfaad = GbsSctoierelementHaupt.TextBoxSizungBerictVerzaicnisPfaad.Text;

				this.CustomBotServer = GbsSctoierelementHaupt.CustomBotServer;

				GbsSctoierelementHaupt.EveOnlinePräferenzAusGbsAingaabeLeese();

				/*
				 * 2015.02.16
				 * 
				this.UserscriptHost = GbsSctoierelementHaupt.UserscriptHost;
				 * */

				this.AingaabeErwaitertFraigaabe = GbsSctoierelementHaupt.ErwaitertFraigaabe;
				//	this.AingaabeUserscriptActive = GbsSctoierelementHaupt.UserscriptActive;

				GbsAingaabeEveOnlinePräferenz = GbsSctoierelementHaupt.EveOnlinePräferenzAingaabeLezte;

				EveOnlnOptimatParamBerecne();

				if (!GbsSctoierelementHaupt.IsInitialized)
				{
					return;
				}

				GbsAingaabeZiilProcessId = GbsSctoierelementHaupt.ZiilProcessAuswaalZiilProcess.AuswaalProcessId;

				GbsAingaabeEveOnlnAutoWirkungFraigaabe = true == GbsSctoierelementHaupt.ButtonZiilProzesWirkungFraigaabe.ButtonReczIsChecked;

				GbsAingaabeEveOnlnSimuFraigaabe = true == GbsSctoierelementHaupt.CheckBoxEveOnlineSimuFraigaabe.IsChecked;

				if (GbsAingaabeEveOnlnSimuFraigaabe)
				{
					var SimuAingaabeTreferpunkteUndCapacitor = GbsSctoierelementHaupt.EveOnlineSimuShipTreferpunkteUndCapacitor.KonfigBerecneAusGbs();

					var TreferpunkteUndCapacitor = (null == SimuAingaabeTreferpunkteUndCapacitor) ? null : SimuAingaabeTreferpunkteUndCapacitor.SictShipTreferpunkteUndCapacitorZuusctand();

					var ScpiilerShipZuusctand = new ShipState(TreferpunkteUndCapacitor, null);

					GbsAingaabeEveOnlnSimuSelbstShipZuusctand = ScpiilerShipZuusctand;
				}

				GbsAingaabeScnitOptimatServerLizenzDataiPfaad = GbsSctoierelementHaupt.TextBoxOptimatServerVerbindungLizenzDataiPfaad.Text;
				/*
				 * 2015.03.04
				 * 
						GbsAingaabeScnitOptimatServerVersuucVerbindungZaitDistanz = (Int64)GbsSctoierelementHaupt.SliderScnitOptimatServerKonfigVersuucVerbindungZaitDistanz.Value;
				 * */

				AingaabeKonfig = GbsSctoierelementHaupt.KonfigBerecneAusGbs();

				{
					var DamageTypeString = GbsSctoierelementHaupt.ComboBoxEveOnlineSimuDamageType.SelectedValue as string;

					if (null != DamageTypeString)
					{
						var DamageType = Enum.Parse(typeof(SictDamageTypeSictEnum), DamageTypeString);

						if (null != DamageType)
						{
							VorgaabeDamageType = (SictDamageTypeSictEnum)DamageType;
						}
					}
				}

				GbsAingaabeSimuAnforderungFittingIgnoriire = GbsSctoierelementHaupt.CheckBoxEveOnlineSimuAnforderungFittingIgnoriire.IsChecked;

				GbsAingaabeWaalZiilProcessMainWindowHandle = ZuProcessIdMainWindowHandle(GbsAingaabeZiilProcessId);

				GbsAingaabeSimuOverviewScroll = GbsSctoierelementHaupt.CheckBoxSimuInOverviewScroll.IsChecked;
				GbsAingaabeSimuMausAufWindowVordersteEkeIndex =
					GbsSctoierelementHaupt
					.ComboBoxSimuMausAufWindowVordersteEkeIndex
					.SelectedItem.ToStringNullable().TryParseInt();
			}
			finally
			{
				this.GbsAingaabeWaalZiilProcessMainWindowHandle = GbsAingaabeWaalZiilProcessMainWindowHandle;

				this.GbsAingaabeEveOnlnWirkungFraigaabe = GbsAingaabeEveOnlnAutoWirkungFraigaabe;

				this.GbsAingaabeEveOnlnAutoWirkungSetForegroundNict = GbsAingaabeEveOnlnAutoWirkungSetForegroundNict;

				this.GbsAingaabeVorgaabeDamageType = VorgaabeDamageType;

				this.GbsAingaabeEveOnlnSimuFraigaabe = GbsAingaabeEveOnlnSimuFraigaabe;
				this.GbsAingaabeEveOnlnSimuSelbstShipZuusctand = GbsAingaabeEveOnlnSimuSelbstShipZuusctand;

				this.GbsAingaabeSimuAufgaabeDistanceScteleAinObjektNääxteFraigaabe =
					true == GbsAingaabeSimuAufgaabeDistanceScteleAinObjektNääxteFraigaabe;

				this.GbsAingaabeSimuOverviewScroll = GbsAingaabeSimuOverviewScroll;
				this.GbsAingaabeSimuMausAufWindowVordersteEkeIndex = GbsAingaabeSimuMausAufWindowVordersteEkeIndex;

				this.GbsAingaabeSimuAufgaabeDistanceScteleAinObjektNääxteDistanceSol =
					GbsAingaabeSimuAufgaabeDistanceScteleAinObjektNääxteDistanceSol;

				this.GbsAingaabeSimuAnforderungFittingIgnoriire = GbsAingaabeSimuAnforderungFittingIgnoriire;

				this.GbsAingaabeScnitOptimatServerLizenzDataiPfaad = GbsAingaabeScnitOptimatServerLizenzDataiPfaad;
				this.GbsAingaabeScnitOptimatServerVersuucVerbindungZaitDistanz = GbsAingaabeScnitOptimatServerVersuucVerbindungZaitDistanz;

				this.GbsAingaabeKonfig = AingaabeKonfig;
				this.GbsAingaabeZiilProcessId = GbsAingaabeZiilProcessId;
			}
		}

		void GbsAktualisiireTailLicenseClient()
		{
			GbsSctoierelementHaupt.LicenseClientStateIcon.Value = sensorServerDispatcher?.AppInterfaceAvailable ?? false;

			/*
			 * 16.04.15
			 * This propagated license information to UI.
			 * 
			GbsSctoierelementHaupt.LicenseClientStatusInspect.Value = this.LicenseClientStatusOk;

			GbsSctoierelementHaupt.LicenseClientStateIcon.Value = this.LicenseClientStatusOk;

			GbsSctoierelementHaupt.LicenseClientInspect.Present(LicenseClient.Wert);
			*/
		}

		void GbsAktualisiireTailSensorClient()
		{
			GbsSctoierelementHaupt.SensorClientStatusInspect.SymboolTyp = this.SensorClientStatusSymbool;

			GbsSctoierelementHaupt.VonProcessLeeseLezteInspekt.Present(AlsInputSnapshot(SensorSnapshotLastAgrClassic));
		}

		void OptimatScritZwiscescpaicerSezeAufAusListeTailmengeMitLeeseFertigLezte()
		{
			var ListeOptimatScrit = this.ListeOptimatScrit;

			if (null != ListeOptimatScrit)
			{
				var ListeOptimatScritLezteMitLeeseFertig =
					ListeOptimatScrit.LastOrDefault((Kandidaat) => null != Kandidaat.VonWindowLeese && null != Kandidaat.VonProcessMesung);

				if (null != ListeOptimatScritLezteMitLeeseFertig)
				{
					OptimatScritZwiscescpaicer = ListeOptimatScritLezteMitLeeseFertig;
				}
			}
		}

		void GbsZiilProcessAktualisiire()
		{
			var GbsSctoierelementHaupt = this.GbsSctoierelementHaupt;

			if (null == GbsSctoierelementHaupt)
			{
				return;
			}

			var ZaitStopwatchMili = Bib3.Glob.StopwatchZaitMikroSictInt() / 1000;

			Bib3.FCL.GBS.SictSymboolAkzeptanzFeelerWarnung.SymboolTypSictEnum[] ZiilProcessMengeMeldungTyp;
			var ZiilProcessScraibeMeldungTyp = default(Bib3.FCL.GBS.SictSymboolAkzeptanzFeelerWarnung.SymboolTypSictEnum);
			Bib3.FCL.GBS.SictSymboolAkzeptanzFeelerWarnung.SymboolTypSictEnum[] ZiilProcessLeeseMengeMeldungTyp;
			/*
			 * 2015.03.06
			 * 
			Int64? EveOnlnSensoWurzelSuuceLezteBeginZaitMili = null;
			Int64? EveOnlnSensoWurzelSuuceLezteEndeZaitMili = null;
			string EveOnlnSensoWurzelSuuceLezteErgeebnisSictString = null;
			//	Brush ZiilProcessLeeseSuuceFrühesteInspektSctaatusBrush = null;
			//	SictSctaatusSictEnum? ZiilProcessLeeseSuuceFrühesteSctaatus = null;

			Int64? ZiilProcessLeeseSuuceLezteBeginZaitMili = null;
			Int64? ZiilProcessLeeseSuuceLezteEndeZaitMili = null;
			string ZiilProcessLeeseSuuceLezteErgeebnisSictString = null;
			//	Brush ZiilProcessLeeseSuuceLezteInspektSctaatusBrush = null;
			//	SictSctaatusSictEnum? ZiilProcessLeeseSuuceLezteSctaatus = null;

			//	SictSctaatusSictEnum? ZiilProcessScraibeSctaatus = null;

			var ZiilProcessLeeseSuuceFrüühesteMeldungTyp = default(Bib3.FCL.GBS.SictSymboolAkzeptanzFeelerWarnung.SymboolTypSictEnum);
			var ZiilProcessLeeseSuuceLezteMeldungTyp = default(Bib3.FCL.GBS.SictSymboolAkzeptanzFeelerWarnung.SymboolTypSictEnum);
			 * */

			string ButtonZiilProzesWirkungUnterbreceTooltipText = null;

			//	Brush ZiilProcessBrush = null;

			var ZiilProcessWirkungPauseAuswaalMengeKeyKombi = GbsSctoierelementHaupt.ZiilProcessWirkungPauseAuswaalMengeKeyKombi;

			var GbsAingaabeEveOnlnWirkungFraigaabe = this.GbsAingaabeEveOnlnWirkungFraigaabe;

			try
			{
				var GbsAingaabeKonfig = this.GbsAingaabeKonfig;

				var ZiilProcessWirkungPauseMengeKeyKombi = (null == GbsAingaabeKonfig) ? null : GbsAingaabeKonfig.ZiilProcessWirkungPauseMengeKeyKombi;

				{
					if (null != ZiilProcessWirkungPauseMengeKeyKombi)
					{
						foreach (var PauseKeyKombi in ZiilProcessWirkungPauseMengeKeyKombi)
						{
							if (null == PauseKeyKombi)
							{
								continue;
							}

							if (PauseKeyKombi.All((Key) => System.Windows.Input.Keyboard.IsKeyDown(Key)))
							{
								GbsSctoierelementHaupt.ButtonZiilProzesWirkungFraigaabe.ButtonLinxIsChecked = true;

								OptimatScritZwiscescpaicerSezeAufAusListeTailmengeMitLeeseFertigLezte();
							}
						}
					}

					ButtonZiilProzesWirkungUnterbreceTooltipText = ButtonZiilProzesWirkungUnterbreceTooltipTextBerecne(ZiilProcessWirkungPauseMengeKeyKombi);

					if (null != GbsAingaabeKonfig)
					{
						if (!(0 < ExtractFromOldAssembly.Bib3.Extension.CountNullable(ZiilProcessWirkungPauseMengeKeyKombi)))
						{
							ZiilProcessWirkungPauseAuswaalMengeKeyKombi.ScteleSicerKeyKombiEnthalte(ZiilProcessWirkungPauseKeyKombiSctandard);
						}
					}
				}

				/*
				 * 2015.03.03
				 * 
						var EveOnlnSensoWurzelSuuceLezteTask = this.EveOnlnSensoWurzelSuuceLezteTask;
						var EveOnlnSensoScnapscusAuswertLezteFertig = this.EveOnlnSensoScnapscusAuswertLezteFertig;

						if (null != EveOnlnSensoWurzelSuuceLezteTask.Wert)
						{
							EveOnlnSensoWurzelSuuceLezteBeginZaitMili = EveOnlnSensoWurzelSuuceLezteTask.Zait / 1000;

							//	ZiilProcessLeeseSuuceFrühesteSctaatus = SictSctaatusSictEnum.Gehalte;

							ZiilProcessLeeseSuuceFrüühesteMeldungTyp = Bib3.FCL.GBS.SictSymboolAkzeptanzFeelerWarnung.SymboolTypSictEnum.InArbait;

							//	ZiilProcessLeeseSuuceFrühesteInspektSctaatusBrush = Brushes.Gold;

							if (EveOnlnSensoWurzelSuuceLezteTask.Wert.IsCompleted)
							{
								EveOnlnSensoWurzelSuuceLezteErgeebnisSictString = "failed. please make sure you have selected a 32Bit Eve Online Process.";
								//	ZiilProcessLeeseSuuceFrühesteSctaatus = SictSctaatusSictEnum.Feeler;
								ZiilProcessLeeseSuuceFrüühesteMeldungTyp = Bib3.FCL.GBS.SictSymboolAkzeptanzFeelerWarnung.SymboolTypSictEnum.Feeler;
								//	ZiilProcessLeeseSuuceFrühesteInspektSctaatusBrush = Brushes.Red;

								var WurzelSuuce = EveOnlnSensoWurzelSuuceLezteTask.Wert.Result;

								if (null != WurzelSuuce)
								{
									EveOnlnSensoWurzelSuuceLezteEndeZaitMili = WurzelSuuce.Dauer.EndeZaitMikro / 1000;

									var GbsMengeWurzelObj = WurzelSuuce.GbsMengeWurzelObj;

									if (0 < Bib3.Extension.CountNullable(GbsMengeWurzelObj))
									{
										EveOnlnSensoWurzelSuuceLezteErgeebnisSictString = "success";
										//	ZiilProcessLeeseSuuceFrühesteInspektSctaatusBrush = Brushes.LimeGreen;
										//	ZiilProcessLeeseSuuceFrühesteSctaatus = SictSctaatusSictEnum.Loift;
										ZiilProcessLeeseSuuceFrüühesteMeldungTyp = Bib3.FCL.GBS.SictSymboolAkzeptanzFeelerWarnung.SymboolTypSictEnum.Akzeptanz;
									}
								}
							}
						}

						if (null != EveOnlnSensoScnapscusAuswertLezteFertig)
						{
							ZiilProcessLeeseSuuceLezteBeginZaitMili = EveOnlnSensoScnapscusAuswertLezteFertig.BeginZait;
							ZiilProcessLeeseSuuceLezteEndeZaitMili = EveOnlnSensoScnapscusAuswertLezteFertig.EndeZait;

							var GbsAstInfo = EveOnlnSensoScnapscusAuswertLezteFertig.Wert;

							//	ZiilProcessLeeseSuuceLezteInspektSctaatusBrush = Brushes.LimeGreen;
							//	ZiilProcessLeeseSuuceLezteSctaatus = SictSctaatusSictEnum.Loift;
							ZiilProcessLeeseSuuceLezteMeldungTyp = Bib3.FCL.GBS.SictSymboolAkzeptanzFeelerWarnung.SymboolTypSictEnum.Akzeptanz;

							ZiilProcessLeeseSuuceLezteErgeebnisSictString = "success";
						}
				 * */

				//	ZiilProcessBrush = ZiilProcessLeeseSuuceFrühesteInspektSctaatusBrush;

				//	!!!!	Noc ainzubaue: Berüksictigung ob Aingaabe nit mööglic da Fenster Minimiirt
				//	ZiilProcessScraibeSctaatus	= (true	== GbsAingaabeEveOnlnWirkungFraigaabe)	?	SictSctaatusSictEnum.Loift	: SictSctaatusSictEnum.Gehalte;
				ZiilProcessScraibeMeldungTyp = (true == GbsAingaabeEveOnlnWirkungFraigaabe) ? Bib3.FCL.GBS.SictSymboolAkzeptanzFeelerWarnung.SymboolTypSictEnum.Akzeptanz :
					Bib3.FCL.GBS.SictSymboolAkzeptanzFeelerWarnung.SymboolTypSictEnum.Pause;
			}
			finally
			{
				/*
				 * 2014.06.06
				 * 
				var ZiilProcessLeeseSctaatus = Kombiniire(ZiilProcessLeeseSuuceFrühesteSctaatus, ZiilProcessLeeseSuuceLezteSctaatus);

				var ZiilProcessSctaatus = Kombiniire(ZiilProcessLeeseSctaatus, ZiilProcessScraibeSctaatus);

				var ZiilProcessLeeseTailSuuceFrühesteSctaatusBrush = ZuSctaatusBrush(ZiilProcessLeeseSuuceFrühesteSctaatus);
				var ZiilProcessLeeseTailSuuceLezteSctaatusBrush = ZuSctaatusBrush(ZiilProcessLeeseSuuceLezteSctaatus);

				var ZiilProcessLeeseSctaatusBrush = ZuSctaatusBrush(ZiilProcessLeeseSctaatus);

				var ZiilProcessScraibeSctaatusBrush = ZuSctaatusBrush(ZiilProcessScraibeSctaatus);

				var ZiilProcessSctaatusBrush = ZuSctaatusBrush(ZiilProcessSctaatus);
				 * */

				ZiilProcessLeeseMengeMeldungTyp = new Bib3.FCL.GBS.SictSymboolAkzeptanzFeelerWarnung.SymboolTypSictEnum[]{
					SensorClientStatusSymbool};

				ZiilProcessMengeMeldungTyp =
					new Bib3.FCL.GBS.SictSymboolAkzeptanzFeelerWarnung.SymboolTypSictEnum[]{
						ZiilProcessScraibeMeldungTyp}
					.Concat(ZiilProcessLeeseMengeMeldungTyp).ToArray();

				if (true == GbsAingaabeEveOnlnWirkungFraigaabe)
				{
					GbsSctoierelementHaupt.TextBlockZiilProcessWirkungFraigaabeSctaatusAus.Visibility = Visibility.Collapsed;
					GbsSctoierelementHaupt.TextBlockZiilProcessWirkungFraigaabeSctaatusAin.Visibility = Visibility.Visible;
				}
				else
				{
					GbsSctoierelementHaupt.TextBlockZiilProcessWirkungFraigaabeSctaatusAus.Visibility = Visibility.Visible;
					GbsSctoierelementHaupt.TextBlockZiilProcessWirkungFraigaabeSctaatusAin.Visibility = Visibility.Collapsed;
				}

				GbsSctoierelementHaupt.ButtonZiilProzesWirkungUnterbreceTooltipTextBlock.Text =
					ButtonZiilProzesWirkungUnterbreceTooltipText;

				/*
				 * 16.04.15
				 * This propagated info about selected eve client process to UI.
				 * 
				GbsSctoierelementHaupt.ZiilProcessSctaatusInspekt.Repräsentiire(
					Optimat.GBS.Glob.MengeMeldungAkzeptanzFeelerWarnungAgregatioon(ZiilProcessMengeMeldungTyp));
				*/

				/*
				 * 2015.03.04
				 * 
						GbsSctoierelementHaupt.ZiilProcessSctaatusTailLeeseInspekt.Repräsentiire(
							Optimat.GBS.Glob.MengeMeldungAkzeptanzFeelerWarnungAgregatioon(ZiilProcessLeeseMengeMeldungTyp));
				 * */

				GbsAktualisiireTailSensorClient();

				/*
				 * 2015.03.03
				 * 
						GbsSctoierelementHaupt.ZiilProcessSctaatusTailLeeseSuuceFrüühesteInspekt.Repräsentiire(ZiilProcessLeeseSuuceFrüühesteMeldungTyp);

						GbsSctoierelementHaupt.ZiilProcessSctaatusTailLeeseSuuceLezteInspekt.Repräsentiire(ZiilProcessLeeseSuuceLezteMeldungTyp);

						GbsSctoierelementHaupt.ZiilProcessLeeseSuuceFrühesteInspekt.Repräsentiire(
							EveOnlnSensoWurzelSuuceLezteBeginZaitMili,
							EveOnlnSensoWurzelSuuceLezteEndeZaitMili,
							EveOnlnSensoWurzelSuuceLezteErgeebnisSictString);

						GbsSctoierelementHaupt.ZiilProcessLeeseSuuceLezteInspekt.Repräsentiire(
							ZiilProcessLeeseSuuceLezteBeginZaitMili,
							ZiilProcessLeeseSuuceLezteEndeZaitMili,
							ZiilProcessLeeseSuuceLezteErgeebnisSictString);
				 * */

				GbsSctoierelementHaupt.ZiilProcessSctaatusTailAingaabeInspekt.Repräsentiire(ZiilProcessScraibeMeldungTyp);

				GbsSctoierelementHaupt.ZiilProcessAuswaalZiilProcess.MengeProcessReprInDataGridScteleSicerAktualisatioonAlterScrankeMaxMili(1000);

				GbsSctoierelementHaupt.ZiilProcessAuswaalZiilProcess.AuswaalProcessAktualisiire();
				GbsSctoierelementHaupt.ZiilProcessAuswaalZiilProcess.AuswaalProcessInspektAktualisiire();

				if (null != ZiilProcessWirkungPauseAuswaalMengeKeyKombi)
				{
					ZiilProcessWirkungPauseAuswaalMengeKeyKombi.Aktualisiire();
				}

				var ListeOptimatScritScraibeNaacDataiDataiNaameSctandard = ListeOptimatScritScraibeNaacDataiDataiNaameSctandardBerecne();

				GbsSctoierelementHaupt.ListeOptimatScritInspekt.ListeOptimatScritScraibeNaacDataiDataiNaameSctandard = ListeOptimatScritScraibeNaacDataiDataiNaameSctandard;
				GbsSctoierelementHaupt.ListeOptimatScritInspekt.ListeOptimatScritScraibeNaacDataiDataiPfaadSctandard =
					Bib3.FCL.Glob.ZuProcessSelbsctMainModuleDirectoryPfaadBerecne() + System.IO.Path.DirectorySeparatorChar +
					ListeOptimatScritScraibeNaacDataiDataiUnterverzaicnisNaame + System.IO.Path.DirectorySeparatorChar +
					ListeOptimatScritScraibeNaacDataiDataiNaameSctandard;

				var ListeOptimatScritLezte =
					(null == ListeOptimatScrit) ? null :
					ListeOptimatScrit
					.OrderBy((Kandidaat) => Kandidaat.VonProcessLeeseBeginZait ?? int.MinValue)
					.LastOrDefault();

				var PropagationBerict =
					Bib3.Glob.PropagiireListeRepräsentatioon(
					ListeOptimatScrit,
					GbsSctoierelementHaupt.ListeOptimatScritInspekt.MengeOptimatScritRepr as IList<SictObservable<EveOnline.GBS.SictOptimatScritRepr>>,
					(OptimatScritZuusctand) => new SictObservable<EveOnline.GBS.SictOptimatScritRepr>(new EveOnline.GBS.SictOptimatScritRepr(OptimatScritZuusctand)),
					(Repr, ScritZuusctand) => EveOnline.GBS.SictOptimatScritRepr.ReprPastZuOptimatScrit(Repr.Wert, ScritZuusctand),
					(Repr, ScritZuusctand) =>
					{
						if (ListeOptimatScritLezte == ScritZuusctand)
						{
							var RaisePropertyChangedLezteAlterMili = ZaitStopwatchMili - Repr.RaisePropertyChangedLezteZaitStopwatchMili;

							if (444 < RaisePropertyChangedLezteAlterMili)
							{
								Repr.RaisePropertyChanged();
							}
						}
					});

				if (null != PropagationBerict)
				{
					if (0 < PropagationBerict.KonstruiirtAnzaal)
					{
						GbsSctoierelementHaupt.ListeOptimatScritInspekt.DataGridMengeOptimationScritSortNaacZait();
					}
				}
			}
		}

		void AktualisiireListeOptimatScrit()
		{
			var SensorMeasurementInTimeframe = SensorClient.MemoryMeasurementLast;

			if (null == SensorMeasurementInTimeframe)
			{
				return;
			}

			var ListeOptimatScritLezte = ListeOptimatScrit.LastOrDefaultNullable();

			var ListeOptimatScritLezteAktuel = false;

			if (null != ListeOptimatScritLezte)
			{
				if (null != ListeOptimatScritLezte.VonProcessMesung)
				{
					if (ListeOptimatScritLezte.VonProcessMesung.BeginZait == SensorMeasurementInTimeframe.Begin)
					{
						ListeOptimatScritLezteAktuel = true;
					}
				}
			}

			if (ListeOptimatScritLezteAktuel)
			{
				return;
			}

			ListeOptimatScrit.Add(new EveOnline.SictOptimatScrit(SensorMeasurementInTimeframe.Begin,
				SensorMeasurementInTimeframe.AlsVonProcessMesung().Sict(MemoryStructMap.AlsVonSensorikMesung)));

			ListeOptimatScrit.ListeKürzeBegin(30);
		}

		void GbsInspektAktualisiire()
		{
			var GbsSctoierelementHaupt = this.GbsSctoierelementHaupt;

			if (null == GbsSctoierelementHaupt)
			{
				return;
			}

			if (!GbsSctoierelementHaupt.IsInitialized)
			{
				return;
			}

			Int64? OptimatScritAlter = null;

			try
			{
				var BeginZaitMikro = Bib3.Glob.StopwatchZaitMikroSictInt();

				var BeginZaitMili = BeginZaitMikro / 1000;

				var OptimatScritZwiscescpaicer = this.OptimatScritZwiscescpaicer;

				if (null != OptimatScritZwiscescpaicer)
				{
					var VonProcessLeese = OptimatScritZwiscescpaicer.VonProcessMesung;

					if (null != VonProcessLeese)
					{
						var OptimatScritVonProcessLeeseBeginZaitMili = VonProcessLeese.BeginZait;

						OptimatScritAlter = (BeginZaitMili - OptimatScritVonProcessLeeseBeginZaitMili) / 1000;
					}
				}
			}
			finally
			{
				GbsSctoierelementHaupt.TextBoxInspektOptimatScritZwiscescpaicerAlter.Text = OptimatScritAlter.ToString();
			}
		}

		Int64 GbsAktualisatioonLezteZaitMili;

		string InspektScritZaitBerecne()
		{
			Int64? AssumptionLastMeasurementTime;

			var RequestedMeasurementTime = this.RequestedMeasurementTimeKapseltInLog(
				out AssumptionLastMeasurementTime);

			var SensorSnapshotLast = this.SensorSnapshotLastAgr;

			Int64? SensorSnapshotLastEndeZait = null;

			if (null != SensorSnapshotLast)
			{
				var SensorMeasurementInTimeframe = SensorSnapshotLast.MemoryMeasurement;

				if (null != SensorMeasurementInTimeframe)
				{
					SensorSnapshotLastEndeZait = SensorMeasurementInTimeframe.End;
				}
			}

			return
				"SensorSnapshotLastEndeZait = " + SensorSnapshotLastEndeZait.ToString() + Environment.NewLine +
				"RequestedMeasurementTime = " + RequestedMeasurementTime.ToString() + Environment.NewLine;
		}

		void GbsAktualisiireTailInspektScritZait()
		{
			GbsSctoierelementHaupt.TextBoxInspektScritZait.Text = InspektScritZaitBerecne();
		}

		void GbsAktualisiire()
		{
			var BeginZaitMikro = Bib3.Glob.StopwatchZaitMikroSictInt();

			var BeginZaitMili = BeginZaitMikro / 1000;

			GbsAktualisatioonLezteZaitMili = BeginZaitMili;

			var GbsSctoierelementHaupt = this.GbsSctoierelementHaupt;

			if (null == GbsSctoierelementHaupt)
			{
				return;
			}

			Int64? TimerElapsedLockedInRaameBerictExceptionLezteAlter = null;
			string TimerElapsedLockedInRaameBerictExceptionLezteSictString = null;

			try
			{
				GbsSctoierelementHaupt?.LicenseView?.Present(sensorServerDispatcher);

				GbsZiilProcessAktualisiire();

				GbsAktualisiireTailLicenseClient();

				var TimerElapsedLockedInRaameBerictExceptionLezte = this.TimerElapsedLockedInRaameBerictExceptionLezte;

				if (null != TimerElapsedLockedInRaameBerictExceptionLezte.Wert)
				{
					TimerElapsedLockedInRaameBerictExceptionLezteSictString =
						Optimat.Glob.ExceptionSictString(TimerElapsedLockedInRaameBerictExceptionLezte.Wert);

					TimerElapsedLockedInRaameBerictExceptionLezteAlter =
						(BeginZaitMili - TimerElapsedLockedInRaameBerictExceptionLezte.Zait) / 1000;
				}

				var GbsEveOnlnAktualisiireBeginZaitMikro = Bib3.Glob.StopwatchZaitMikroSictInt();

				GbsEveOnlnAktualisiire();

				var GbsSchnitOptimatServerAktualisiireBeginZaitMikro = Bib3.Glob.StopwatchZaitMikroSictInt();

				/*
				 * 2015.03.04
				 * 
						GbsSchnitOptimatServerAktualisiire();
				 * */

				var GbsDebugAktualisiireBeginZaitMikro = Bib3.Glob.StopwatchZaitMikroSictInt();

				AktualisiireListeOptimatScrit();

				ListeOptimatScritInspektAktualisiireTailWirkung();

				GbsInspektAktualisiire();

				var GbsDebugAktualisiireEndeZaitMikro = Bib3.Glob.StopwatchZaitMikroSictInt();

				var GbsEveOnlnAktualisiireDauerMikro = GbsSchnitOptimatServerAktualisiireBeginZaitMikro - GbsEveOnlnAktualisiireBeginZaitMikro;
				var GbsSchnitOptimatServerAktualisiireDauerMikro = GbsDebugAktualisiireBeginZaitMikro - GbsSchnitOptimatServerAktualisiireBeginZaitMikro;
				var GbsDebugAktualisiireDauerMikro = GbsDebugAktualisiireEndeZaitMikro - GbsDebugAktualisiireBeginZaitMikro;

				GbsSctoierelementHaupt.RepräsentiireErwaitert();

				GbsAktualisiireTailInspektScritZait();
			}
			finally
			{
				GbsSctoierelementHaupt.TextBoxDbgTimerElapsedLockedInRaameBerictExceptionLezteAlter.Text =
					TimerElapsedLockedInRaameBerictExceptionLezteAlter.ToString();

				GbsSctoierelementHaupt.TextBoxDbgTimerElapsedLockedInRaameBerictExceptionLezteSictString.Text =
					TimerElapsedLockedInRaameBerictExceptionLezteSictString;
			}
		}
	}
}
