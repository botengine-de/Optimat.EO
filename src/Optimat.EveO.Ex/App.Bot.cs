using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Bib3;
using Newtonsoft.Json;
using Optimat.EveOnline;
using Optimat.EveOnline.Anwendung;
using ExtractFromOldAssembly.Bib3;

namespace Optimat.EveO.Nuzer
{
	public partial class App
	{
		readonly Optimat.ScpezEveOnln.SictAutomat Automat = new Optimat.ScpezEveOnln.SictAutomat();

		Int64 AutomatScritBerecneLezteNuzerZaitMili;

		Int64 AutomatScritBerecneLezteSensorikMesungZaitMili;

		bool AusAutomaatExceptionScraibeNaacDatai = true;

		Int64 BerecnungVorsclaagZaitScrankeMin = Int64.MaxValue;

		string MengeSizungBerictVerzaicnisPfaad = null;

		string SizungBerictVerzaicnisNaame = null;

		Int64 SizungBeginZait = Bib3.Glob.StopwatchZaitMiliSictInt();

		string SizungBerictVerzaicnisPfaad
		{
			get
			{
				var MengeSizungBerictVerzaicnisPfaad = this.MengeSizungBerictVerzaicnisPfaad;

				if (MengeSizungBerictVerzaicnisPfaad.NullOderLeer())
				{
					return null;
				}

				return ExtractFromOldAssembly.Bib3.Glob.ScteleSicerEndung(MengeSizungBerictVerzaicnisPfaad, @"\") + SizungBerictVerzaicnisNaame;
			}
		}

		void SizungBerictVerzaicnisNaameKonstrukt()
		{
			SizungBerictVerzaicnisNaame =
				"Session[ZAK=" + Bib3.Glob.SictwaiseKalenderString(Bib3.Glob.SictDateTimeVonStopwatchZaitMili(SizungBeginZait), ".", 0) + "]";
		}

		Int64 VonWirkungBisScnapscusWartezaitMili = 300;

		/// <summary>
		/// 2015.03.18 Beobactung Probleem bai erfasung MenuWurzelAnnaameInitiaal.
		/// Versuuc beserung durc vergrööserung Zaitabsctand auf Wert vor April.
		/// </summary>
		Int64 ZwisceMesungZaitScrankeMinMili = 800;

		readonly List<WertZuZaitpunktStruct<Bib3.RefNezDiferenz.SictZuNezSictDiferenzScritAbbild>> NaacBerictListeAutomaatZuusctandDiferenz =
			new List<WertZuZaitpunktStruct<Bib3.RefNezDiferenz.SictZuNezSictDiferenzScritAbbild>>();

		void ScraibeNaacDataiInSizungBerictVerzaicnisPfaad(
			string ZiilDataiNaame,
			byte[] ZiilDataiInhalt)
		{
			ScraibeNaacDataiInSizungBerictVerzaicnisPfaad(
				ZiilDataiNaame,
				new byte[][] { ZiilDataiInhalt });
		}

		void ScraibeNaacDataiInSizungBerictVerzaicnisPfaad(
			string ZiilDataiNaame,
			IEnumerable<byte[]> ZiilDataiInhaltListeSegment)
		{
			var ZiilVerzaicnisPfaad = SizungBerictVerzaicnisPfaad;

			if (ZiilVerzaicnisPfaad.NullOderLeer())
			{
				return;
			}

			if (ZiilVerzaicnisPfaad.Length < 1)
			{
				return;
			}

			var ZiilVerzaicnis = new DirectoryInfo(ZiilVerzaicnisPfaad);

			if (!ZiilVerzaicnis.Exists)
			{
				ZiilVerzaicnis.Create();
			}

			var ZiilDataiPfaad = ZiilVerzaicnis.FullName + Path.DirectorySeparatorChar + ZiilDataiNaame;

			var ZiilDataiStream = new FileStream(ZiilDataiPfaad, FileMode.Create, FileAccess.Write);

			try
			{
				if (null != ZiilDataiInhaltListeSegment)
				{
					foreach (var ZiilDataiInhaltSegment in ZiilDataiInhaltListeSegment)
					{
						if (null == ZiilDataiInhaltSegment)
						{
							continue;
						}

						ZiilDataiStream.Write(ZiilDataiInhaltSegment, 0, ZiilDataiInhaltSegment.Length);
					}
				}
			}
			finally
			{
				ZiilDataiStream.Close();
			}
		}

		void KapseleInCatchAutomaatExceptionScraibeNaacDatai(Action Aktioon)
		{
			var Zait = Bib3.Glob.StopwatchZaitMiliSictInt();

			if (null == Aktioon)
			{
				return;
			}

			try
			{
				Aktioon();
			}
			catch (System.Exception Exception)
			{
				if (AusAutomaatExceptionScraibeNaacDatai)
				{
					var ExceptionSictString = Optimat.Glob.ExceptionSictString(Exception);

					var ExceptionSictUTF8 = Encoding.UTF8.GetBytes(ExceptionSictString);

					var ZaitSictKalenderString =
						Bib3.Glob.SictwaiseKalenderString(Bib3.Glob.SictDateTimeVonStopwatchZaitMili(Zait), ".", 3);

					var ZiilDataiNaame = "[ZAK=" + ZaitSictKalenderString + "].Automaat.Exception";

					ScraibeNaacDataiInSizungBerictVerzaicnisPfaad(ZiilDataiNaame, ExceptionSictUTF8);
				}
				else
				{
					throw;
				}
			}
		}

		Int64 BerictNaacDsZaitDistanzMili = (Int64)4e+3;

		void AktualisiireAutomat()
		{
			var Zait = Bib3.Glob.StopwatchZaitMiliSictInt();

			var MemoryMeasurementLast = SensorClient.MemoryMeasurementLast;

			Automat.VonSensorikMesungLezte = SensorSnapshotLastAgr.OoneMesungWindow();

			if (null == MemoryMeasurementLast)
			{
				return;
			}

			var VonSensorikScnapscus = MemoryMeasurementLast.Value;

			var ScritBerecneLezteNuzerAlterMili = Zait - AutomatScritBerecneLezteNuzerZaitMili;

			if (AutomatScritBerecneLezteSensorikMesungZaitMili < MemoryMeasurementLast.End &&
				((AutomatScritBerecneLezteNuzerZaitMili < BerecnungVorsclaagZaitScrankeMin && 500 <= ScritBerecneLezteNuzerAlterMili) ||
				5000 <= ScritBerecneLezteNuzerAlterMili))
			{
				KapseleInCatchAutomaatExceptionScraibeNaacDatai(() =>
				{
					AutomatScritBerecne();
				});
			}

			var BerictNaacDsLezteAlterMili = Zait - BerictNaacDsLezteZaitMili;

			//	if (true == ScpezEveOnlineVerhalteBerictAutomaatZuusctandDif)
			{
				/*
				if (BerictNaacDsZaitDistanzMili * 3 < BerictNaacDsLezteAlterMili ||
					(!VonNuzerNaacrict && BerictNaacDsZaitDistanzMili < BerictNaacDsLezteAlterMili))
				 * */

				if (BerictNaacDsZaitDistanzMili < BerictNaacDsLezteAlterMili)
				{
					try
					{
						BerictScraibeNaacDsBegin();
					}
					catch (System.Exception)
					{
						throw;
					}
				}
			}
		}

		void AutomatScritBerecne()
		{
			var Zait = Bib3.Glob.StopwatchZaitMiliSictInt();

			AutomatScritBerecneLezteNuzerZaitMili = Zait;

			AutomatScritBerecneLezteSensorikMesungZaitMili = Automat?.VonSensorikMesungLezte?.MemoryMeasurement?.End ?? 0;

			Automat.ServerZaitMili = Zait;
			Automat.NuzerZaitMili = Automat?.VonSensorikMesungLezte?.MemoryMeasurement?.End ?? 0;

			var VonNuzerMeldungZuusctand = Automat.VonNuzerMeldungZuusctand;

			if (null == VonNuzerMeldungZuusctand)
			{
				VonNuzerMeldungZuusctand = new SictNaacOptimatMeldungZuusctand();
			}

			VonNuzerMeldungZuusctand.OptimatParam = EveOnlnOptimatParamBerecne();
			VonNuzerMeldungZuusctand.ListeNaacProcessWirkung = this.ListeNaacProcessWirkung;

			//	VonNuzerMeldungZuusctand.VonSensorikScnapscus = VonSensorikScnapscus;

			Automat.VonNuzerMeldungZuusctand = VonNuzerMeldungZuusctand;

			var ScritBerecnet = Automat.ScritBerecne();

			var AutomaatZuusctandDiferenzScrit = Automat.ZuusctandScritDiferenzBerecne(10);

			NaacBerictListeAutomaatZuusctandDiferenz.Add(new WertZuZaitpunktStruct<Bib3.RefNezDiferenz.SictZuNezSictDiferenzScritAbbild>(
				AutomaatZuusctandDiferenzScrit, Automat.ServerZaitMili ?? -1));

			NaacBerictListeAutomaatZuusctandDiferenz.ListeKürzeBegin(30);
		}

		Queue<Int64?> RequestedMeasurementTimeLog = new Queue<Int64?>();

		IEnumerable<Int64?> RequestedMeasurementTimeLogDif
		{
			get
			{
				var RequestedMeasurementTimeLogOoneNull =
					RequestedMeasurementTimeLog.WhereNotDefault().ToArray();

				return
					RequestedMeasurementTimeLogOoneNull
					.Select((Time, Index) => RequestedMeasurementTimeLogOoneNull.ElementAtOrDefault(Index + 1) - Time);
			}
		}

		Int64? RequestedMeasurementTimeKapseltInLog(
			out Int64? AssumptionLastMeasurementTime)
		{
			var RequestedMeasurementTime = this.RequestedMeasurementTime(
				out AssumptionLastMeasurementTime);

			lock (RequestedMeasurementTimeLog)
			{
				var LogNit = false;

				if (0 < RequestedMeasurementTimeLog.Count)
				{
					if (RequestedMeasurementTimeLog.Last() <= RequestedMeasurementTime ||
						RequestedMeasurementTimeLog.Last() == RequestedMeasurementTime)
					{
						LogNit = true;
					}
				}

				if (!LogNit)
				{
					RequestedMeasurementTimeLog.Enqueue(RequestedMeasurementTime);

					RequestedMeasurementTimeLog.ListeKürzeBegin(100);
				}
			}

			return RequestedMeasurementTime;
		}

		Int64? RequestedMeasurementTime(
			out Int64? AssumptionLastMeasurementTime)
		{
			AssumptionLastMeasurementTime = null;
			AssumptionLastMeasurementTime = SensorClient?.MemoryMeasurementLast?.End;

			var VonAutomatMeldungZuusctandLezte = this.VonOptimatMeldungZuusctandLezte;

			if (null == VonAutomatMeldungZuusctandLezte)
			{
				return (AssumptionLastMeasurementTime + 5000) ?? Bib3.Glob.StopwatchZaitMiliSictInt();
			}

			var ScritLeeseBeginZaitScrankeMinMili = VonAutomatMeldungZuusctandLezte.MesungNääxteZaitScrankeMin;

			if (AssumptionLastMeasurementTime.HasValue)
			{
				ScritLeeseBeginZaitScrankeMinMili = Math.Max(
					ScritLeeseBeginZaitScrankeMinMili,
					AssumptionLastMeasurementTime.Value + ZwisceMesungZaitScrankeMinMili);
			}
			else
			{
			}

			var VonOptimatVorsclaagBerecnetZuLezteScnapscus =
				!AssumptionLastMeasurementTime.HasValue ||
				AssumptionLastMeasurementTime <= VonAutomatMeldungZuusctandLezte.BerecnungVorsclaagLezteZait;

			if (!VonOptimatVorsclaagBerecnetZuLezteScnapscus)
			{
				return null;
			}

			var VorsclaagListeWirkung = VonAutomatMeldungZuusctandLezte.VorsclaagListeWirkung.ToArrayFalsNitLeer();

			if (null != ListeNaacProcessWirkung)
			{
				//	lock (ListeNaacProcessWirkungLock)
				{
					foreach (var NaacProcessWirkung in ListeNaacProcessWirkung)
					{
						var VorsclaagWirkung =
							VonAutomatMeldungZuusctandLezte.VorsclaagListeWirkung
							.FirstOrDefaultNullable((KandidaatVorsclaagWirkung) => KandidaatVorsclaagWirkung.Ident == NaacProcessWirkung.VorsclaagWirkungIdent);

						var VonWirkungBisScnapscusWartezaitMili = this.VonWirkungBisScnapscusWartezaitMili;

						if (null != VorsclaagWirkung)
						{
							VonWirkungBisScnapscusWartezaitMili = Math.Max(VonWirkungBisScnapscusWartezaitMili, (VorsclaagWirkung.VonWirkungBisVonZiilProcessLeeseWartezaitMili ?? 0));
						}

						var FürWirkungKarenzzaitEnde =
							NaacProcessWirkung.EndeZaitMili + VonWirkungBisScnapscusWartezaitMili;

						if (FürWirkungKarenzzaitEnde.HasValue)
						{
							ScritLeeseBeginZaitScrankeMinMili = Math.Max(ScritLeeseBeginZaitScrankeMinMili, FürWirkungKarenzzaitEnde.Value);
						}
					}

					if (null != VorsclaagListeWirkung)
					{
						foreach (var VorsclaagWirkung in VorsclaagListeWirkung)
						{
							if (ListeNaacProcessWirkung.Any((KandidaatWirkung) => KandidaatWirkung.VorsclaagWirkungIdent == VorsclaagWirkung.Ident))
							{
								continue;
							}

							return null;
						}
					}
				}
			}

			return ScritLeeseBeginZaitScrankeMinMili;
		}

		void ListeAutomaatZuusctandDiferenzScritScraibeNaacZipArchiveNaacVerzaicnisBerictNaacDataiMitNaame(
			IEnumerable<WertZuZaitpunktStruct<Bib3.RefNezDiferenz.SictZuNezSictDiferenzScritAbbild>> ListeAutomaatZuusctand,
			string ZiilDataiNaame)
		{
			var ZipArchiveStream = new MemoryStream();

			var SerializeSettings = new JsonSerializerSettings();

			SerializeSettings.DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate;
			SerializeSettings.TypeNameHandling = TypeNameHandling.Auto;
			SerializeSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;

			/*
			 * 2014.04.05
			 * 
			 * Cannot preserve reference to array or readonly list, or list created from a non-default constructor: Optimat.SictWertMitZait`1[Optimat.Anwendung.RefNezDiferenz.SictRefNezDiferenzScritSictJson][]. Path 'MengeAutomaatZuusctandDiferenzScritMitZait.$values', line 1, position 78.
			 * 
			SerializeSettings.PreserveReferencesHandling = PreserveReferencesHandling.All;
			 * */

			using (var ZipArchive = new System.IO.Compression.ZipArchive(ZipArchiveStream, System.IO.Compression.ZipArchiveMode.Create, true))
			{
				if (null != ListeAutomaatZuusctand)
				{
					foreach (var AutomaatZuusctandZuBericte in ListeAutomaatZuusctand)
					{
						if (null == AutomaatZuusctandZuBericte.Wert)
						{
							continue;
						}

						var AutomaatZuusctandZuBericteZaitSictKalenderString =
							Bib3.Glob.SictwaiseKalenderString(Bib3.Glob.SictDateTimeVonStopwatchZaitMili(AutomaatZuusctandZuBericte.Zait), ".", 3);

						var EntryNaame = "[ZAK=" + AutomaatZuusctandZuBericteZaitSictKalenderString + "]";

						string EntrySictString = null;

						try
						{
							var EntrySctrukt = SictVonAnwendungBerict.KonstruktFürAutomaatZuusctandDiferenzScrit(
								AutomaatZuusctandZuBericte);

							EntrySictString = JsonConvert.SerializeObject(EntrySctrukt, Formatting.None, SerializeSettings);
						}
						catch (System.Exception Exception)
						{
							EntrySictString = Optimat.Glob.ExceptionSictString(Exception);
						}

						var EntrySictUTF8 = Encoding.UTF8.GetBytes(EntrySictString);

						var Entry = ZipArchive.CreateEntry(EntryNaame);

						var EntryStream = Entry.Open();

						EntryStream.Write(EntrySictUTF8, 0, EntrySictUTF8.Length);

						EntryStream.Close();
					}
				}
			}

			ZipArchiveStream.Seek(0, SeekOrigin.Begin);

			var ZipArchiveListeSegment = Optimat.Glob.VonStreamLeeseNaacListeArray(ZipArchiveStream, 0x10000);

			ZipArchiveStream.Dispose();

			ScraibeNaacDataiInSizungBerictVerzaicnisPfaad(ZiilDataiNaame, ZipArchiveListeSegment);
		}

		Int64 BerictNaacDsLezteZaitMili;

		byte[] AnwendungSizungIdent = AnwendungSizungIdentZuufal();

		static RNGCryptoServiceProvider Random = new RNGCryptoServiceProvider();

		static byte[] AnwendungSizungIdentZuufal()
		{
			var IdentArray = new byte[8];

			Random.GetBytes(IdentArray);

			return IdentArray;
		}

		public byte[] AnwendungSizungIdentBerecne()
		{
			return AnwendungSizungIdent;
		}

		void BerictScraibeNaacDsBegin()
		{
			var ZaitMili = Bib3.Glob.StopwatchZaitMiliSictInt();

			BerictNaacDsLezteZaitMili = ZaitMili;

			var ZaitSictKalenderString =
				Bib3.Glob.SictwaiseKalenderString(Bib3.Glob.SictDateTimeVonStopwatchZaitMili(ZaitMili), ".", 3);

			var AnwendungSizungIdent = AnwendungSizungIdentBerecne();

			var ZiilDataiNaame = "[ASId=" + (Bib3.Glob.SictZaalSictStringBaasis16(AnwendungSizungIdent) ?? "null") + ",ZAK=" + ZaitSictKalenderString + "].Berict";

			var ListeAutomaatZuusctandDiferenzScritZuBericte = NaacBerictListeAutomaatZuusctandDiferenz.ToArray();

			NaacBerictListeAutomaatZuusctandDiferenz.Clear();

			if (ListeAutomaatZuusctandDiferenzScritZuBericte.NullOderLeer())
			{
				return;
			}

			var Task = new Task(() =>
			{
				ListeAutomaatZuusctandDiferenzScritScraibeNaacZipArchiveNaacVerzaicnisBerictNaacDataiMitNaame(
					ListeAutomaatZuusctandDiferenzScritZuBericte,
					ZiilDataiNaame);
			});

			Task.Start();
		}


	}
}
