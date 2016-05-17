using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Optimat.EveOnline;
using Bib3;
using ExtractFromOldAssembly.Bib3;

namespace Optimat.EveO.Nuzer
{
	public	partial	class App
	{
		SictNaacProcessWirkung[] ListeNaacProcessWirkung;

		Random Zuufal = new Random((int)Bib3.Glob.StopwatchZaitMikroSictInt());

		static	public	System.Windows.Point AlsSystemWindowsPoint(SictVektor2DSingle	Vektor)
		{
			return new System.Windows.Point(Vektor.A, Vektor.B);
		}

		static	public	System.Windows.Point AlsSystemWindowsPoint(Vektor2DInt	Vektor)
		{
			return new System.Windows.Point(Vektor.A, Vektor.B);
		}

		Vektor2DInt? ZuufäligePunktAusFläce(
			OrtogoonInt Fläce)
		{
			if (null == Fläce)
			{
				return null;
			}

			var	Grööse	= Fläce.Grööse;

			if(Grööse.A < 1 || Grööse.B < 1)
			{
				return	null;
			}

			var ZuufalA = Zuufal.Next();
			var ZuufalB = Zuufal.Next();

			return Fläce.PunktMin + new Vektor2DInt(ZuufalA % Grööse.A, ZuufalB % Grööse.B);
		}

		//	2014.02.18	Beobactung mit Wartezait=111:	auf langsame Testsystem Probleem Drag in Inventory Item
		static int MouseWartezaitMili = 166;

		SictNaacProcessWirkungTailMausErgeebnis MausWirkungFüüreAus(
			SictVorsclaagNaacProcessWirkung	Wirkung,
			int	ZiilFläceRandScrankeAkzeptanz,
			int ZiilFläceRandScrankeZuufal)
		{
			if (null == Wirkung)
			{
				return null;
			}

			var WirkungZwekListeKomponente = Wirkung.WirkungZwekListeKomponente;

			{
				//	Temp Verzwaigung für Debug Breakpoint

				if (null != WirkungZwekListeKomponente)
				{
					if (!WirkungZwekListeKomponente.Any((ZwekKomponente) => ZwekKomponente.ToLower().Contains("create")))
					{
					}
				}
			}

			var MausPfaadListeWeegpunktFläceAbhängigVonGbs = Wirkung.MausPfaadListeWeegpunktFläce;
			var MausPfaadMengeFläceZuMaideAbhängigVonGbs = Wirkung.MausPfaadMengeFläceZuMaide;

			var MausPfaadTasteLinksAin = Wirkung.MausPfaadTasteLinksAin;
			var MausPfaadTasteRectsAin = Wirkung.MausPfaadTasteRectsAin;

			var Ergeebnis = new SictNaacProcessWirkungTailMausErgeebnis();

			if (!(true == MausPfaadTasteLinksAin) && !(true == MausPfaadTasteRectsAin) &&
				null == MausPfaadListeWeegpunktFläceAbhängigVonGbs)
			{
				//	Kaine Wirkung durc Maus angefordert
				Ergeebnis.Erfolg = true;
				return Ergeebnis;
			}

			if (null == MausPfaadListeWeegpunktFläceAbhängigVonGbs)
			{
				return Ergeebnis;
			}

			var PfaadEnthaltMeerereWeegpunkt = 1 < MausPfaadListeWeegpunktFläceAbhängigVonGbs.Length;

			var ProcessId = GbsFürZiilprozesWaalWindowProzesId();

			var ZiilWindowHandle = GbsAingaabeWaalZiilProcessMainWindowHandle;

			Bib3.Windows.User32.RECT ZiilWindowClientRect;
			Bib3.Windows.User32.GetClientRect(ZiilWindowHandle, out	ZiilWindowClientRect);

		/*
		 * 2015.03.03
		 * 
			var EveOnlnSensoWurzelSuuceLezteTask = this.EveOnlnSensoWurzelSuuceLezteTask;

			var EveOnlnSensoWurzelSuuceLezteTaskTask = EveOnlnSensoWurzelSuuceLezteTask.Wert;

			if (!EveOnlnSensoWurzelSuuceLezteTaskTask.IsCompleted)
			{
				return Ergeebnis;
			}

			var GbsSuuceWurzel = EveOnlnSensoWurzelSuuceLezteTaskTask.Result;
		 * */

			var MengeFläceAbhängigVonGbsAst =
				Optimat.Glob.ListeErwaitertAlsArray(MausPfaadListeWeegpunktFläceAbhängigVonGbs, MausPfaadMengeFläceZuMaideAbhängigVonGbs);

		/*
		 * 2015.03.03
		 * 
			var MengeFläceErgeebnis =
				Optimat.EveOnline.SictProzesAuswertZuusctand.ListeFläceBerecneAusGbs(
				MengeFläceAbhängigVonGbsAst,
				ProcessId,
				GbsSuuceWurzel);
		 * */

			var MengeFläceErgeebnis	=
				MengeFläceAbhängigVonGbsAst
				.SelectNullable((Fläce) => new KeyValuePair<InProcessGbsFläceRectekOrto, OrtogoonInt?>(
					Fläce, Fläce.FläceTailSctaatisc));

			var MausPfaadListeWeegpunktFläceErgeebnis =
				MausPfaadListeWeegpunktFläceAbhängigVonGbs
				.Select((FläceAbhängigVonGbs) => MengeFläceErgeebnis.FirstOrDefault((Kandidaat) => Kandidaat.Key == FläceAbhängigVonGbs).Value)
				.Select((FläceNulbar) => FläceNulbar ?? OrtogoonInt.Leer)
				.ToArray();

			var MausPfaadMengeFläceZuMaideErgeebnis =
				(null == MausPfaadMengeFläceZuMaideAbhängigVonGbs) ? null :
				MausPfaadMengeFläceZuMaideAbhängigVonGbs
				.Select((FläceAbhängigVonGbs) => MengeFläceErgeebnis.FirstOrDefault((Kandidaat) => Kandidaat.Key == FläceAbhängigVonGbs).Value)
				.Select((FläceNulbar) => FläceNulbar ?? OrtogoonInt.Leer)
				.ToArray();

			Ergeebnis.MausPfaadListeWeegpunktFläceErgeebnis = MausPfaadListeWeegpunktFläceErgeebnis;
			Ergeebnis.MausPfaadMengeFläceZuMaideErgeebnis = MausPfaadMengeFläceZuMaideErgeebnis;

			var MausPfaadListeWeegpunktFläceMiinusFläceZuMaide =
				MausPfaadListeWeegpunktFläceErgeebnis
				.Select((WeegpunktFläce) =>
					{
						var MengeTailfläce = new OrtogoonInt[] { WeegpunktFläce };

						if (null != MausPfaadMengeFläceZuMaideErgeebnis)
						{
							foreach (var FläceZuMaide in MausPfaadMengeFläceZuMaideErgeebnis)
							{
								if (null == FläceZuMaide)
								{
									continue;
								}

								MengeTailfläce =
									Bib3.Glob.ArrayAusListeFeldGeflact(
									MengeTailfläce
									.Select((VorherFläce) =>
									{
										if (null == VorherFläce)
										{
											return null;
										}

										return	Optimat.EveOnline.Extension.FläceMiinusFläce(VorherFläce, FläceZuMaide);
									})
									.Where((t) => null	!= t)
									.ToArray());
							}
						}

						return MengeTailfläce;
					})
				.ToArray();

			var RandDiferenz = ZiilFläceRandScrankeZuufal - ZiilFläceRandScrankeAkzeptanz;

			var MausPfaadListeWeegpunktFläceMiinusFläceZuMaideMiinusRand =
				MausPfaadListeWeegpunktFläceMiinusFläceZuMaide
				.Select((MausPfaadWeegpunktFläceMiinusFläceZuMaide) =>
					{
						if (null == MausPfaadWeegpunktFläceMiinusFläceZuMaide)
						{
							return null;
						}

						return
							MausPfaadWeegpunktFläceMiinusFläceZuMaide
							.Where((Fläce) => null	!= Fläce)
							.Select((Fläce) => Fläce.Vergröösert(
							-ZiilFläceRandScrankeAkzeptanz * 2,
							-ZiilFläceRandScrankeAkzeptanz * 2))
							.Where((KandidaatFläce) => 0 < KandidaatFläce.Grööse.A && 0 < KandidaatFläce.Grööse.B)
							.Select((Fläce) => Fläce.VergröösertBescrankt(-RandDiferenz, -RandDiferenz, 0, 0))
							.ToArray();
					})
				.ToArray();

			var MausPfaadBeginMengeFläce = MausPfaadListeWeegpunktFläceMiinusFläceZuMaideMiinusRand.FirstOrDefault();

			var MausPfaadEndeMengeFläce =
				MausPfaadListeWeegpunktFläceMiinusFläceZuMaideMiinusRand.LastOrDefault();

			var MausPfaadBeginFläce =
				MausPfaadBeginMengeFläce.OrderByNullable((Fläce) => Fläce.Betraag()).LastOrDefaultNullable();

			var MausPfaadEndeFläce =
				MausPfaadEndeMengeFläce.OrderByNullable((Fläce) => Fläce.Betraag()).LastOrDefaultNullable();

			if (null == MausPfaadBeginFläce ||
				(null == MausPfaadEndeFläce && PfaadEnthaltMeerereWeegpunkt))
			{
				return Ergeebnis;
			}

			var MausPfaadBeginPunktNulbar = ZuufäligePunktAusFläce(MausPfaadBeginFläce);
			var MausPfaadEndePunktNulbar = ZuufäligePunktAusFläce(MausPfaadEndeFläce);

			if(!PfaadEnthaltMeerereWeegpunkt)
			{
				MausPfaadEndePunktNulbar = MausPfaadBeginPunktNulbar;
			}

			Ergeebnis.MausPfaadBeginPunkt = MausPfaadBeginPunktNulbar;
			Ergeebnis.MausPfaadEndePunkt = MausPfaadEndePunktNulbar;

			if (!MausPfaadBeginPunktNulbar.HasValue ||
				!MausPfaadEndePunktNulbar.HasValue)
			{
				return Ergeebnis;
			}

			var MausPfaadBeginPunkt = MausPfaadBeginPunktNulbar.Value;
			var MausPfaadEndePunkt = MausPfaadEndePunktNulbar.Value;

			var MengePunktZuPrüüfeAufScnitMitZiilWindowClientRect = new List<Vektor2DInt>();

			MengePunktZuPrüüfeAufScnitMitZiilWindowClientRect.Add(MausPfaadBeginPunkt);
			MengePunktZuPrüüfeAufScnitMitZiilWindowClientRect.Add(MausPfaadEndePunkt);

			var MausPfaadInZiilWindowClientRect =
				MengePunktZuPrüüfeAufScnitMitZiilWindowClientRect
				.All((MausPfaadWeegpunkt) =>
				0 <= MausPfaadWeegpunkt.A &&
				0 <= MausPfaadWeegpunkt.B &&
				MausPfaadWeegpunkt.A <= ZiilWindowClientRect.Width &&
				MausPfaadWeegpunkt.B <= ZiilWindowClientRect.Height);

			Ergeebnis.MausPfaadInZiilWindowClientRect = MausPfaadInZiilWindowClientRect;

			if (!MausPfaadInZiilWindowClientRect)
			{
				return Ergeebnis;
			}

			var MausPfaadBeginPunktAlsWindowsPoint = AlsSystemWindowsPoint(MausPfaadBeginPunkt);
			var MausPfaadEndePunktAlsWindowsPoint = AlsSystemWindowsPoint(MausPfaadEndePunkt);

			if (PfaadEnthaltMeerereWeegpunkt &&
				(true == MausPfaadTasteLinksAin || true == MausPfaadTasteRectsAin))
			{
				Bib3.Windows.User32.DragGebascdelsMitSleep20140211(
					ZiilWindowHandle,
					MausPfaadBeginPunktAlsWindowsPoint,
					MausPfaadEndePunktAlsWindowsPoint,
					true == MausPfaadTasteRectsAin,
					MouseWartezaitMili);
			}
			else
			{
				if (true == MausPfaadTasteLinksAin)
				{
					WirkungMouseClick(ZiilWindowHandle, (int)MausPfaadEndePunkt.A, (int)MausPfaadEndePunkt.B, false);
				}
				else
				{
					if (true == MausPfaadTasteRectsAin)
					{
						WirkungMouseClick(ZiilWindowHandle, (int)MausPfaadEndePunkt.A, (int)MausPfaadEndePunkt.B, true);
					}
					else
					{
						WirkungMouseMove(ZiilWindowHandle, (int)MausPfaadEndePunkt.A, (int)MausPfaadEndePunkt.B);
					}
				}
			}

			Ergeebnis.Erfolg = true;

			return Ergeebnis;
		}

		SictWertMitZait<Task> VorsclaagWirkungAusfüürungTask;

		readonly object LockWirkung = new object();

		void EveOnlnWirkungKümere()
		{
			var BeginZaitMikro = Bib3.Glob.StopwatchZaitMikroSictInt();

			var BeginZaitMili = BeginZaitMikro / 1000;

			if (null != VorsclaagWirkungAusfüürungTask.Wert)
			{
				if (!VorsclaagWirkungAusfüürungTask.Wert.IsCompleted &&
					!VorsclaagWirkungAusfüürungTask.Wert.IsCanceled &&
					!VorsclaagWirkungAusfüürungTask.Wert.IsFaulted)
				{
					//	Vorheriger VorsclaagWirkungAusfüürungTask noc aktiiv.

					var VorsclaagWirkungAusfüürungTaskAlterMili = BeginZaitMili - VorsclaagWirkungAusfüürungTask.Zait;

					if (VorsclaagWirkungAusfüürungTaskAlterMili < 44444)
					{
						return;
					}
				}
			}

			var GbsAingaabeWaalZiilProzesWindowHandle = this.GbsAingaabeWaalZiilProcessMainWindowHandle;

			var GbsAingaabeEveOnlnAutoFraigaabe = this.GbsAingaabeEveOnlnWirkungFraigaabe;

			if (!GbsAingaabeEveOnlnAutoFraigaabe)
			{
				return;
			}

			if (IntPtr.Zero == GbsAingaabeWaalZiilProzesWindowHandle)
			{
				return;
			}

		/*
		 * 2015.03.03
		 * 
			var EveOnlnSensoScnapscusAuswertLezteFertig = this.EveOnlnSensoScnapscusAuswertLezteFertig;
		 * */

			var VonOptimatMeldungZuusctandLezte = this.VonOptimatMeldungZuusctandLezte	as	Optimat.EveOnline.Base.VonAutomatMeldungZuusctand;

			if (null == VonOptimatMeldungZuusctandLezte)
			{
				return;
			}

			var Task = new Task(() =>
				{
					lock(LockWirkung)
					{
						var VorsclaagListeWirkung =
							VonOptimatMeldungZuusctandLezte.VorsclaagListeWirkung
							.WhereNullable((KandidaatVorsclaagWirkung) =>
								!(ListeNaacProcessWirkung
								.AnyNullable((KandidaatWirkung) => KandidaatWirkung.VorsclaagWirkungIdent == KandidaatVorsclaagWirkung.Ident)	?? false))
							.ToArrayNullable();

						if (VorsclaagListeWirkung.NullOderLeer())
						{
							return;
						}

						var ScritListeNaacProcessWirkung = ScritWirkungFüüreAus(
							VorsclaagListeWirkung,
							GbsAingaabeWaalZiilProzesWindowHandle);

						if (!ScritListeNaacProcessWirkung.NullOderLeer())
						{
							this.ListeNaacProcessWirkung =
								Bib3.Glob.ListeEnumerableAgregiirt(
								this.ListeNaacProcessWirkung,
								ScritListeNaacProcessWirkung)
								.Reversed()
								.Take(10)
								.Reversed()
								.ToArrayNullable();
						}
					}
				});

			VorsclaagWirkungAusfüürungTask = new SictWertMitZait<Task>(BeginZaitMili, Task);

			Task.Start();
		}

		SictNaacProcessWirkung[] ScritWirkungFüüreAus(
			SictVorsclaagNaacProcessWirkung[] VorsclaagListeWirkung,
			IntPtr GbsAingaabeWaalZiilProzesWindowHandle)
		{
			if (null == VorsclaagListeWirkung)
			{
				return null;
			}

			var VorherForegroundWindowHandle = Bib3.Windows.User32.GetForegroundWindow();

			SictNaacProcessWirkungTailMausErgeebnis WirkungTailMausErgeebnis = null;

			var NaacZiilProcessListeWirkung = new SictNaacProcessWirkung[VorsclaagListeWirkung.Length];

			for (int VorsclaagWirkungIndex = 0; VorsclaagWirkungIndex < VorsclaagListeWirkung.Length; VorsclaagWirkungIndex++)
			{
				var VorsclaagWirkung = VorsclaagListeWirkung[VorsclaagWirkungIndex];

				var WirkungBeginZaitMili = Bib3.Glob.StopwatchZaitMikroSictInt() / 1000;

				System.Exception WirkungException	= null;

				try
				{
					var MengeKey = VorsclaagWirkung.MengeWirkungKey;

					var MausPfaadListeWeegpunktFläce = VorsclaagWirkung.MausPfaadListeWeegpunktFläce;
					var MausPfaadTasteLinksAin = VorsclaagWirkung.MausPfaadTasteLinksAin;
					var MausPfaadTasteRectsAin = VorsclaagWirkung.MausPfaadTasteRectsAin;
					var AingaabeText = VorsclaagWirkung.AingaabeText;

					bool WirkungNict = false;

					if (null != MengeKey ||
						null != MausPfaadListeWeegpunktFläce ||
						true == MausPfaadTasteLinksAin ||
						true == MausPfaadTasteRectsAin	||
						!AingaabeText.NullOderLeer())
					{
						if (VorherForegroundWindowHandle != GbsAingaabeWaalZiilProzesWindowHandle)
						{
							if (GbsAingaabeEveOnlnAutoWirkungSetForegroundNict)
							{
								WirkungNict = true;
							}
							else
							{
								var VersuucSetForegroundWindowErgeebnis = Bib3.Windows.User32.SetForegroundWindow(GbsAingaabeWaalZiilProzesWindowHandle);

								if (!VersuucSetForegroundWindowErgeebnis)
								{
									WirkungNict = true;
								}
							}
						}
					}

					if (!WirkungNict)
					{
						var WirkungTailMausDauer = new SictMesungZaitraumAusStopwatch(true);

						WirkungTailMausErgeebnis = MausWirkungFüüreAus(VorsclaagWirkung, 1, 4);

						WirkungTailMausDauer.EndeSezeJezt();

						if (null != MengeKey)
						{
							foreach (var Key in MengeKey)
							{
								var MengeModifier = Key.MengeModifier;

								if (null != MengeModifier)
								{
								}
								else
								{
									if (null != Key.MengeKey)
									{
										foreach (var KeyKey in Key.MengeKey)
										{
											var KeyKeySictInputSimulator = Glob.VonWindowsInputKeyNaacInputSimulatorVirtualKeyCode((System.Windows.Input.Key)KeyKey);

											WindowsInput.InputSimulator.SimulateKeyPress(KeyKeySictInputSimulator);
										}
									}
								}
							}
						}

						if(!AingaabeText.NullOderLeer())
						{
							WindowsInput.InputSimulator.SimulateTextEntry(AingaabeText);
						}
					}
				}
				catch (System.Exception Exception)
				{
					WirkungException = Exception;
				}
				finally
				{
					var WirkungEndeZaitMili = Bib3.Glob.StopwatchZaitMikroSictInt() / 1000;

					var WirkungErfolg = ((null == WirkungTailMausErgeebnis) ? true : WirkungTailMausErgeebnis.Erfolg) ?? false;

					if (null != WirkungTailMausErgeebnis)
					{
						if (!(true == WirkungTailMausErgeebnis.Erfolg))
						{
						}
					}

					var	ExceptionSictJsonAbbild	= SictExceptionSictJson.ExceptionSictJson(WirkungException);

					var NaacZiilProcessWirkung = new SictNaacProcessWirkung(
						WirkungBeginZaitMili,
						WirkungEndeZaitMili,
						VorsclaagWirkung.Ident,
						WirkungTailMausErgeebnis,
						WirkungErfolg,
						ExceptionSictJsonAbbild);

					NaacZiilProcessListeWirkung[VorsclaagWirkungIndex] = NaacZiilProcessWirkung;
				}
			}

			return NaacZiilProcessListeWirkung.ToArray();
		}
	}
}
