using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Net;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Bib3;
using ExtractFromOldAssembly.Bib3;
using Bib3.Synchronization;

namespace Optimat.EveO.Nuzer
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App
	{
		static App()
		{
			new SensorAppDomainSetup();
		}

		public string VersioonSictString
		{
			get
			{
				return "16.05.27";
			}
		}

		public string AnwendungTitel	=>
			"Optimat.EveOnline v" + VersioonSictString;

		/*
		 * 16.04.15
		 * 
		override public double TimerInterval
		{
			get
			{
				return 1.0 / 16;
			}
		}

		override public bool BerictErscteleSol
		{
			get
			{
				return false;
			}
		}

		override public string BerictUnterverzaicnisPfaad
		{
			get
			{
				return ".\\..\\Anwendung.Berict";
			}
		}
		*/

		readonly string ListeOptimatScritScraibeNaacDataiDataiNaameSctandardEnde = "List Optimat Step";

		readonly string ListeOptimatScritScraibeNaacDataiDataiUnterverzaicnisNaame	= "List Optimat Step";

		CustomBotServer CustomBotServer = null;

		static	public	byte[]	ClientIdentBerecne()
		{
			return new System.Security.Cryptography.SHA1Managed().ComputeHash(Bib3.FCL.Glob.ProcessCurrentMainModuleDataiInhalt());
		}

		static public byte[] ClientIdent = ClientIdentBerecne();

		public string ListeOptimatScritScraibeNaacDataiDataiNaameSctandardBerecne()
		{
			var ListeOptimatScritScraibeNaacDataiDataiNaameSctandardEnde = this.ListeOptimatScritScraibeNaacDataiDataiNaameSctandardEnde;

			var ZaitSictDateTime = DateTime.Now;

			var ZaitSictKalenderString = Bib3.Glob.SictwaiseKalenderString(ZaitSictDateTime, ".", 0);

			var ListeOptimatScritScraibeNaacDataiDataiNaameSctandard =
				ZaitSictKalenderString + " " + ListeOptimatScritScraibeNaacDataiDataiNaameSctandardEnde;

			return ListeOptimatScritScraibeNaacDataiDataiNaameSctandard;
		}

		readonly List<EveOnline.SictOptimatScrit> ListeOptimatScrit = new List<EveOnline.SictOptimatScrit>();

		/*
		 * 2015.03.03
		 * 
		void ListeOptimatScritAktualisiire()
		{
			ListeOptimatScritInspektAktualisiireTailWirkung();

			var EveOnlnSensoScnapscusAuswertLezteTask = this.EveOnlnSensoScnapscusAuswertLezteTask;
			var EveOnlnSensoWindowScnapscusHersctelungLezteTask = this.EveOnlnSensoWindowScnapscusHersctelungLezteTask;

			if (null != EveOnlnSensoScnapscusAuswertLezteTask.Wert)
			{
				if (EveOnlnSensoScnapscusAuswertLezteTask.Wert.IsCompleted)
				{
					var EveOnlnSensoScnapscusAuswertLezte = EveOnlnSensoScnapscusAuswertLezteTask.Wert.Result;

					var EveOnlnSensoScnapscusAuswertLezteBeginZaitMili = EveOnlnSensoScnapscusAuswertLezteTask.Zait / 1000;

					var OptimatScrit = EveOnline.SictOptimatScrit.AusMengeOptimatScritZuBeginZaitScrit(ListeOptimatScrit, EveOnlnSensoScnapscusAuswertLezteBeginZaitMili);

					if (null == OptimatScrit)
					{
						var VonZiilProcessLeese = new EveOnline.SictVonProcessLeese(
							EveOnlnSensoScnapscusAuswertLezteBeginZaitMili,
							EveOnlnSensoScnapscusAuswertLezte.ScritLezteEndeZaitMili,
							EveOnlnSensoScnapscusAuswertLezte.GbsWurzelHauptInfo,
							null,
							EveOnlnSensoScnapscusAuswertLezte.ScritLezteMainWindowTitle);

						OptimatScrit = new EveOnline.SictOptimatScrit(
							VonZiilProcessLeese.BeginZaitMili	?? -1,
							VonZiilProcessLeese);

						ListeOptimatScrit.Add(OptimatScrit);
					}
				}
			}

			if (null != EveOnlnSensoWindowScnapscusHersctelungLezteTask.Wert)
			{
				var EveOnlnSensoWindowScnapscusHersctelungLezteTaskBeginZaitMili = EveOnlnSensoWindowScnapscusHersctelungLezteTask.Zait / 1000;

				if (EveOnlnSensoWindowScnapscusHersctelungLezteTask.Wert.IsCompleted)
				{
					var EveOnlnSensoWindowScnapscusHersctelungLezte = EveOnlnSensoWindowScnapscusHersctelungLezteTask.Wert.Result;

					var OptimatScrit = EveOnline.SictOptimatScrit.AusMengeOptimatScritZuBeginZaitScrit(ListeOptimatScrit, EveOnlnSensoWindowScnapscusHersctelungLezteTaskBeginZaitMili);

					if (null != OptimatScrit)
					{
						if (null == OptimatScrit.VonWindowLeese)
						{
							var VonWindowLeeseErgeebnis = new EveOnline.SictVonWindowLeeseErgeebnis(
								EveOnlnSensoWindowScnapscusHersctelungLezteTaskBeginZaitMili,
								EveOnlnSensoWindowScnapscusHersctelungLezte.AusfüürungEndeZaitMili,
								Optimat.SictExceptionSictJson.ExceptionSictJson(EveOnlnSensoWindowScnapscusHersctelungLezte.Exception),
								EveOnlnSensoWindowScnapscusHersctelungLezte.ZiilDataiPfaad,
								EveOnlnSensoWindowScnapscusHersctelungLezte.WindowClientRasterGrööseBerecne(),
								EveOnlnSensoWindowScnapscusHersctelungLezte.GescriibeDataiInhaltSHA1);

							OptimatScrit.VonWindowLeese = VonWindowLeeseErgeebnis;
						}
					}
				}
			}

			Bib3.Extension.ListeKürzeBegin(ListeOptimatScrit, 60);
		}
		 * */

		protected void NaacGbsErscteltVorTimerErsctelt()
		{
			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
			AppDomain.MonitoringIsEnabled = true;

			SizungBerictVerzaicnisNaameKonstrukt();

			SanderlingMemreadInitConnection();

			SensorKümereRateBescranke = new Action(SensorKümere).CallRateScrankeStopwatchMili(SensorKümereZaitDistanz);

			CreateWindow();
			this.GbsSctoierelementHauptErsctele();

			try
			{
				GbsSctoierelementHaupt.KonfigLaadeVonDataiPfaadUndBericteNaacGbs();
			}
			catch { }

			GbsSctoierelementHaupt?.LicenseView?.LicenseClientConfigViewModel?.PropagateFromClrMemberToDependencyProperty(Sanderling.ExeConfig.LicenseClientDefault);

			ConstructTimer();
		}

		void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			try
			{
				var	BeginZaitMikro	= Bib3.Glob.StopwatchZaitMikroSictInt();

				var	BeginZaitDateTime	= Bib3.Glob.SictDateTimeVonStopwatchZaitMikro(BeginZaitMikro);

				var	BeginZaitSictKalenderString	= Bib3.Glob.SictwaiseKalenderString(BeginZaitDateTime, ".", 3);

				var Exception = e.ExceptionObject as Exception;

				var ExceptionSictString = Optimat.Glob.ExceptionSictString(Exception);

				var ExceptionSictStringUTF8 = Encoding.UTF8.GetBytes(ExceptionSictString);

				var ZiilDataiPfaad = BeginZaitSictKalenderString + ".Exception";

				Optimat.Glob.ScraibeNaacDataiMitPfaad(ZiilDataiPfaad, ExceptionSictStringUTF8);
			}
			catch (System.Exception Ausnaame)
			{
				Optimat.Glob.ZaigeMessageBoxException(Ausnaame);
			}
		}

		void TimerElapsedDispatched()
		{
			var	BeginZaitMikro	= Bib3.Glob.StopwatchZaitMikroSictInt();

			var	BeginZaitMili	= BeginZaitMikro	/ 1000;

			var GbsAktualisatioonLezteAlterMili = BeginZaitMili - GbsAktualisatioonLezteZaitMili;

			if (GbsAktualisatioonLezteAlterMili < 300)
			{
				return;
			}

			var GbsBeginZaitMikro = Bib3.Glob.StopwatchZaitMikroSictInt();

			GbsAingaabeLeese();

			GbsAktualisiire();

			var GbsEndeZaitMikro = Bib3.Glob.StopwatchZaitMikroSictInt();

			var GbsDauerMikro = GbsEndeZaitMikro - GbsBeginZaitMikro;

			if (333111 < GbsDauerMikro)
			{
			}

			ScnapscusPropagiireNaacCustomBot();

		}

		SictWertMitZait<string> TimerExceptionLezteZaitUndSictString
		{
			get
			{
				var TimerElapsedLockedInRaameBerictExceptionLezte = this.TimerElapsedLockedInRaameBerictExceptionLezte;

				return new SictWertMitZait<string>(TimerElapsedLockedInRaameBerictExceptionLezte.Zait,
					Optimat.Glob.ExceptionSictString(TimerElapsedLockedInRaameBerictExceptionLezte.Wert));
			}
		}

		SictWertMitZait<System.Exception> TimerElapsedLockedInRaameBerictExceptionLezte;

		static public Optimat.EveOnline.SictOptimatScrit AusListeOptimatScritSuucePasendeZuVorsclaagWirkung(
			IEnumerable<Optimat.EveOnline.SictOptimatScrit> ListeOptimatScrit,
			Optimat.EveOnline.SictVorsclaagNaacProcessWirkung	VorsclaagWirkung)
		{
			if (null == ListeOptimatScrit)
			{
				return null;
			}

			if (null == VorsclaagWirkung)
			{
				return null;
			}

			foreach (var OptimatScrit in ListeOptimatScrit.OrderByDescending((Kandidaat) => Kandidaat.NuzerZait))
			{
				if (VorsclaagWirkung.NuzerZaitMili < OptimatScrit.NuzerZait)
				{
					continue;
				}

				return OptimatScrit;
			}

			return null;
		}

		void ListeOptimatScritInspektAktualisiireTailWirkung()
		{
			var ListeOptimatScrit = this.ListeOptimatScrit;
			var VonOptimatMeldungZuusctandLezte = this.VonOptimatMeldungZuusctandLezte;

			IEnumerable<Optimat.EveOnline.SictVorsclaagNaacProcessWirkung> VorsclaagListeWirkung = null;

			{
				if (null == VonOptimatMeldungZuusctandLezte)
				{
					return;
				}

				var ListeOptimatScritLezte = ListeOptimatScrit.LastOrDefaultNullable();

				if (null == ListeOptimatScrit)
				{
					return;
				}

				if (null == ListeOptimatScritLezte)
				{
					return;
				}

				VorsclaagListeWirkung = VonOptimatMeldungZuusctandLezte.VorsclaagListeWirkung;
			}

			if (null != VorsclaagListeWirkung)
			{
				foreach (var VorsclaagWirkung in VorsclaagListeWirkung)
				{
					if (ListeOptimatScrit.Any((KandidaatOptimatScrit) =>
						KandidaatOptimatScrit.VorsclaagListeWirkung
						.AnyNullable((KandidaatVorsclaagWirkung) => KandidaatVorsclaagWirkung.Ident == VorsclaagWirkung.Ident) ?? false))
					{
						//	Vorsclaag Wirkung wurde beraits in ain Scrit aingefüügt.
						continue;
					}

					var OptimatScrit =
						AusListeOptimatScritSuucePasendeZuVorsclaagWirkung(ListeOptimatScrit, VorsclaagWirkung);

					if (null != OptimatScrit)
					{
						OptimatScrit.VorsclaagListeWirkung =
							(OptimatScrit.VorsclaagListeWirkung ?? new Optimat.EveOnline.SictVorsclaagNaacProcessWirkung[0])
							.Concat(new Optimat.EveOnline.SictVorsclaagNaacProcessWirkung[] { VorsclaagWirkung })
							.ToArrayNullable();
					}
				}
			}

			ListeNaacProcessWirkung.ForEachNullable((Wirkung) =>
			{
				var OptimatScrit =
					ListeOptimatScrit.FirstOrDefaultNullable((KandidaatOptimatScrit) =>
						KandidaatOptimatScrit.VorsclaagListeWirkung.AnyNullable((KandidaatWirkung) =>
							KandidaatWirkung.Ident == Wirkung.VorsclaagWirkungIdent)	?? false);

				if (null != OptimatScrit)
				{
					OptimatScrit.NaacProcessListeWirkung =
						(OptimatScrit.NaacProcessListeWirkung ?? new Optimat.EveOnline.SictNaacProcessWirkung[0])
						.Concat(new Optimat.EveOnline.SictNaacProcessWirkung[] { Wirkung })
						.ToArrayNullable();
				}
			});
		}

		protected void TimerElapsedLockedInRaameBerict()
		{
			this.Dispatcher.Invoke(new Action(TimerElapsedDispatched), new object[0]);

			var	BeginZaitMikro	= Bib3.Glob.StopwatchZaitMikroSictInt();
			var BeginZaitMili = BeginZaitMikro / 1000;

			try
			{
				var CustomBotServer = this.CustomBotServer;

				/*
				* 16.04.15
				* This is probably the place to call licensensing communication dispatch.
				* 
				LicenseClientKümereRateBescranke.Call();
				*/

				GetMeasurementIfDue();

				SensorKümereRateBescranke.Call();

				if (null != CustomBotServer)
				{
					CustomBotServer.NaacBotScnapscus = SensorSnapshotLastAgrClassic;
				}

				AktualisiireAutomat();

				var EveOnlnSensoBeginZaitMikro = Bib3.Glob.StopwatchZaitMikroSictInt();

				var EveOnlnWirkungKümereBeginZaitMikro = Bib3.Glob.StopwatchZaitMikroSictInt();

				EveOnlnWirkungKümere();
			}
			catch (System.Exception Exception)
			{
				TimerElapsedLockedInRaameBerictExceptionLezte = new SictWertMitZait<System.Exception>(BeginZaitMili, Exception);
			}
		}

		System.Timers.Timer timer;

		readonly object timerLock = new object();

		void ConstructTimer()
		{
			timer = new System.Timers.Timer(1.0 / 16);

			timer.Elapsed += Timer_Elapsed;
			timer.AutoReset = true;
			timer.Start();
		}

		private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			timerLock.InvokeIfNotLocked(() =>
			{
				try
				{
					TimerElapsedLockedInRaameBerict();
				}
				catch (Exception ex)
				{
					throw new Exception("in this place, the old exe would have handled the exception somehow.", ex);
				}
			});

			Dispatcher.BeginInvoke(new Action(TimerElapsedDispatched));
		}

		void CreateWindow()
		{
			var window = new Window
			{
				Visibility = Visibility.Visible,
				Title = AnwendungTitel,
			};

			MainWindow = window;
		}

		private void Application_Startup(object sender, StartupEventArgs e)
		{
			NaacGbsErscteltVorTimerErsctelt();
		}
	}
}
