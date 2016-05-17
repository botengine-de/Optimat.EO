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
using Optimat.EveOnline;
using Optimat.EveOnline.Base;

namespace Optimat.EveO.Nuzer.GBS
{
	/// <summary>
	/// Interaction logic for Erwaitert.xaml
	/// </summary>
	public partial class Erwaitert : UserControl
	{
		public Erwaitert()
		{
			InitializeComponent();
		}

		/*
		 * 2015.02.16
		 * 
		byte[] AssemblySerial = null;

		byte[] AssemblySymbolStoreSerial = null;

		public UserscriptHost UserscriptHost
		{
			private set;
			get;
		}

		public bool UserscriptActive
		{
			get
			{
				return null != UserscriptHost;
			}
		}

		static public string SymbolStoreDataiPfaadBerecneAusAssemblyDataiPfaad(
			string AssemblyDataiPfaad)
		{
			if (null == AssemblyDataiPfaad)
			{
				return null;
			}

			return
				AssemblyDataiPfaad.Substring(0, AssemblyDataiPfaad.LastIndexOf('.') + 1) + "pdb";
		}

		private void UserscriptAssemblyLaadeAusDatai_Drop(object sender, DragEventArgs e)
		{
			Bib3.FCL.GBS.Glob.CatchNaacMessageBoxException(() =>
				{
					UserscriptClear();

					var DroppedFileContent = Bib3.FCL.Glob.LaadeFrüühestInhaltDataiAusDropFileDrop(e);

					if (null == DroppedFileContent.Value)
					{
						throw new ArgumentNullException("DroppedFileContent");
					}

					AssemblySerial = DroppedFileContent.Value;

					var KandidaatSymbolStoreDataiPfaad = SymbolStoreDataiPfaadBerecneAusAssemblyDataiPfaad(DroppedFileContent.Key);

					var SymbolStore = Bib3.Glob.InhaltAusDataiMitPfaad(KandidaatSymbolStoreDataiPfaad);

					AssemblySymbolStoreSerial = SymbolStore;
				});
		}

		public void UserscriptClear()
		{
			if (null != UserscriptHost)
			{
				UserscriptHost.Dispose();
			}

			UserscriptHost = null;
		}

		public void UserscriptCreate()
		{
			UserscriptClear();

			var AssemblySerial = this.AssemblySerial;
			var AssemblySymbolStoreSerial = this.AssemblySymbolStoreSerial;

			if (null == AssemblySerial)
			{
				throw new ArgumentNullException("AssemblySerial");
			}

			UserscriptHost = new UserscriptHost(AssemblySerial, AssemblySymbolStoreSerial);
		}

		private void ButtonUITreeWriteToFile_Drop(object sender, DragEventArgs e)
		{
			Bib3.FCL.GBS.Glob.CatchNaacMessageBoxException(() =>
			{
				throw new NotImplementedException();
			});
		}

		private void ButtonUserscriptInstanceRemove_Click(object sender, RoutedEventArgs e)
		{
			Bib3.FCL.GBS.Glob.CatchNaacMessageBoxException(() =>
			{
				UserscriptClear();
			});
		}

		private void ButtonUserscriptInstanceCreate_Click(object sender, RoutedEventArgs e)
		{
			Bib3.FCL.GBS.Glob.CatchNaacMessageBoxException(() =>
			{
				UserscriptCreate();
			});
		}

		public VonAutomatMeldungZuusctand NaacUserscriptMeldung(Optimat.EveOnline.Base.NaacAutomatMeldungZuusctand Zuusctand)
		{
			var Userscript = this.UserscriptHost;

			if (null == Userscript)
			{
				return null;
			}

			return Userscript.NaacUserscriptMeldung(Zuusctand);
		}
		 * */

		public Optimat.EveO.Nuzer.CustomBotServer CustomBotServer
		{
			private set;
			get;
		}

		public void Present()
		{
			/*
			 * 2015.02.16
			 * 
			var ZaitMili = Bib3.Glob.StopwatchZaitMiliSictInt();

			string TextBoxUserscriptInstanceCreatedTimeCal = null;

			Int64? UserscriptInstanceAgeMili = null;

			Int64? UserscriptInstanceExceptionLastTimeMili = null;
			string UserscriptInstanceExceptionLastSummary = null;

			try
			{
				if (null != UserscriptHost)
				{
					UserscriptInstanceAgeMili = ZaitMili - UserscriptHost.BeginZait;

					TextBoxUserscriptInstanceCreatedTimeCal = Bib3.Glob.SictwaiseKalenderString(Bib3.Glob.SictDateTimeVonStopwatchZaitMili(UserscriptHost.BeginZait), ".", 0);

					var ScriptExceptionLezte = UserscriptHost.ScriptExceptionLezte;

					if (null != ScriptExceptionLezte.Wert)
					{
						UserscriptInstanceExceptionLastTimeMili = ScriptExceptionLezte.Zait;
						UserscriptInstanceExceptionLastSummary = Bib3.Glob.SictString(ScriptExceptionLezte.Wert);
					}
				}
			}
			finally
			{
				TextBoxUserscriptInstanceCreatedTime.Text = TextBoxUserscriptInstanceCreatedTimeCal;

				TextBoxUserscriptInstanceExceptionLastSummary.Text = UserscriptInstanceExceptionLastSummary;
				UserscriptInstanceExceptionLast.Präsentiire(UserscriptInstanceExceptionLastTimeMili);
			}
			 * */

			try
			{

			}
			finally
			{
				CustomBotServerRepr.Präsentiire(CustomBotServer);
			}
		}

		void AktualisiireTailCustomBotServer()
		{
			var CustomBotServerFraigaabe = CheckBoxCustomBotFraigaabe.IsChecked ?? false;

			var AdreseTcp = TextBoxCustomBotServerAdreseTcp.Text.TryParseInt();

			var CustomBotServerErhalte = false;

			if (CustomBotServerFraigaabe)
			{
				var CustomBotServer = this.CustomBotServer;

				if (null != CustomBotServer &&
					CustomBotServer.IsListening)
				{
					if (CustomBotServer.AdreseTcp == AdreseTcp)
					{
						CustomBotServerErhalte = true;
					}
				}
			}

			if (!CustomBotServerErhalte)
			{
				var CustomBotServer = this.CustomBotServer;

				if (null != CustomBotServer)
				{
					CustomBotServer.Stop();

					this.CustomBotServer = null;
				}
			}

			if (!CustomBotServerFraigaabe)
			{
				return;
			}

			{
				if (!AdreseTcp.HasValue)
				{
					throw new ArgumentNullException("AdreseTcp");
				}

				var CustomBotServer = new Optimat.EveO.Nuzer.CustomBotServer(AdreseTcp.Value);

				this.CustomBotServer = CustomBotServer;
			}
		}

		private void CheckBoxCustomBotFraigaabe_Checked(object sender, RoutedEventArgs e)
		{
			Bib3.FCL.GBS.Extension.CatchNaacMessageBoxException(AktualisiireTailCustomBotServer);
		}

		private void CheckBoxCustomBotFraigaabe_Unchecked(object sender, RoutedEventArgs e)
		{
			Bib3.FCL.GBS.Extension.CatchNaacMessageBoxException(AktualisiireTailCustomBotServer);
		}
	}
}
