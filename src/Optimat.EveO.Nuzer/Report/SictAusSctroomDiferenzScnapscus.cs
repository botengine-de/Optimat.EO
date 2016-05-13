using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Bib3.RefNezDiferenz;
using Optimat.EveOnline.Anwendung;
using Bib3;

namespace Optimat.EveOnline.Anwendung.Berict.Auswert
{
	public class SictAusSctroomDiferenzScnapscusAusDataiVerzaicnis
	{
		readonly public SictDiferenzSictParam SictParam;

		readonly SictAusSctroomDiferenzScnapscus SictSume;

		readonly public string AingangVerzaicnisPfaad;

		readonly public string AusgangVerzaicnisPfaad;

		readonly public string AingangDataiNaameFilterRegex;

		readonly List<string> ListeDataiVerarbaitetPfaad = new List<string>();

		public Int64? DiferenzScritScnapscusBerecnetLezteIndex
		{
			private set;
			get;
		}

		public SictAusSctroomDiferenzScnapscusAusDataiVerzaicnis(
			string AingangVerzaicnisPfaad,
			string AusgangVerzaicnisPfaad,
			SictDiferenzSictParam SictParam = null)
		{
			this.AingangVerzaicnisPfaad = AingangVerzaicnisPfaad;
			this.AusgangVerzaicnisPfaad = AusgangVerzaicnisPfaad;

			this.SictParam = SictParam;

			SictSume = new SictAusSctroomDiferenzScnapscus(SictParam);
		}

		void AingangDatai(
			byte[] DataiInhaltSictListeOktet,
			string AusgangDataiPfaad)
		{
			if (null == DataiInhaltSictListeOktet)
			{
				return;
			}

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

			var AusgangVerzaicnisPfaad = System.IO.Path.GetDirectoryName(AusgangDataiPfaad);

			var AusgangVerzaicnis = new DirectoryInfo(AusgangVerzaicnisPfaad);

			if (!AusgangVerzaicnis.Exists)
			{
				AusgangVerzaicnis.Create();
			}

			var AusgangDataiStream = new MemoryStream();

			using (var AusgangZipArchive = new System.IO.Compression.ZipArchive(AusgangDataiStream, System.IO.Compression.ZipArchiveMode.Create, true))
			{
				using (var AingangZipArchive = new System.IO.Compression.ZipArchive(new MemoryStream(DataiInhaltSictListeOktet), System.IO.Compression.ZipArchiveMode.Read))
				{
					var AingangMengeEntry = AingangZipArchive.Entries;

					if (null != AingangMengeEntry)
					{
						foreach (var AingangEntry in AingangMengeEntry)
						{
							var MengeAutomaatZuusctandMitZait = new List<WertZuZaitpunktStruct<Bib3.RefNezDiferenz.SictZuNezSictDiferenzScritAbbild>>();

							using (var EntryStream = AingangEntry.Open())
							{
								var EntryInhalt = Optimat.Glob.AusStreamLeese(EntryStream, null, 0x400000);

								var EntrySictUTF8 = Encoding.UTF8.GetString(EntryInhalt);

								var EntrySictSctrukt = JsonConvert.DeserializeObject<SictVonAnwendungBerict>(EntrySictUTF8);

								var MengeAutomaatZuusctandDiferenzScritMitZait = EntrySictSctrukt.MengeAutomaatZuusctandDiferenzScritMitZait;

								if (null != MengeAutomaatZuusctandDiferenzScritMitZait)
								{
									foreach (var AutomaatZuusctandDiferenzScritMitZait in MengeAutomaatZuusctandDiferenzScritMitZait)
									{
										var AutomaatZuusctandDiferenzScrit = AutomaatZuusctandDiferenzScritMitZait.Wert;

										if (null == AutomaatZuusctandDiferenzScrit)
										{
											continue;
										}

										var AutomaatZuusctandDiferenzScritIndex = AutomaatZuusctandDiferenzScrit.ScritIndex;

										if (!AutomaatZuusctandDiferenzScritIndex.HasValue)
										{
											continue;
										}

										var ErwartetScritIndex = (DiferenzScritScnapscusBerecnetLezteIndex + 1) ?? 0;

										if (!(AutomaatZuusctandDiferenzScritIndex == ErwartetScritIndex))
										{
											continue;
										}

										DiferenzScritScnapscusBerecnetLezteIndex = AutomaatZuusctandDiferenzScritIndex;

										var ScnapscusListeObjektUndErfolg = SictSume.ScnapscusBerecne(AutomaatZuusctandDiferenzScrit);

										if (!ScnapscusListeObjektUndErfolg.Value)
										{
										}

										var ScnapscusListeObjekt = ScnapscusListeObjektUndErfolg.Key;

										if (null == ScnapscusListeObjekt)
										{
											continue;
										}

										var ScnapscusObjekt = ScnapscusListeObjekt.FirstOrDefault();

										if (null == ScnapscusObjekt)
										{
											continue;
										}

										var ScnapscusAutomaatZuusctand = ScnapscusObjekt as Optimat.ScpezEveOnln.SictAutomatZuusctand;

										if (null == ScnapscusAutomaatZuusctand)
										{
											continue;
										}

										var ScnapscusAutomaatZuusctandSictSeriel =
											//	Bib3.RefNezDiferenz.SictRefNezDiferenzScritSictJson.SerialisiireObjekt(
											Bib3.RefNezDiferenz.Extension.WurzelSerialisiire(
											ScnapscusAutomaatZuusctand,
											Optimat.ScpezEveOnln.SictAutomat.ZuusctandSictDiferenzSictParam.TypeBehandlungRictliinieMitScatescpaicer);

										MengeAutomaatZuusctandMitZait.Add(
											new WertZuZaitpunktStruct<Bib3.RefNezDiferenz.SictZuNezSictDiferenzScritAbbild>(
												ScnapscusAutomaatZuusctandSictSeriel,
												AutomaatZuusctandDiferenzScritMitZait.Zait));
									}
								}
							}

							var AusgangEntryScnapscus = new SictVonAnwendungBerict()
							{
								MengeAutomaatZuusctandDiferenzScritMitZait = MengeAutomaatZuusctandMitZait?.ToArray(),
							};

							var AusgangEntryScnapscusSictString = JsonConvert.SerializeObject(AusgangEntryScnapscus, SerializeSettings);

							var AusgangEntryListeOktet = Encoding.UTF8.GetBytes(AusgangEntryScnapscusSictString);

							var AusgangEntry = AusgangZipArchive.CreateEntry(AingangEntry.Name, System.IO.Compression.CompressionLevel.Optimal);

							var AusgangEntryStream = AusgangEntry.Open();

							AusgangEntryStream.Write(AusgangEntryListeOktet, 0, AusgangEntryListeOktet.Length);

							AusgangEntryStream.Close();
						}
					}
				}
			}

			AusgangDataiStream.Seek(0, SeekOrigin.Begin);

			var AusgangZipArchiveListeSegment = Optimat.Glob.VonStreamLeeseNaacListeArray(AusgangDataiStream, 0x10000);

			AusgangDataiStream.Dispose();

			Bib3.Glob.ScraibeInhaltNaacDataiPfaad(AusgangDataiPfaad, Bib3.Glob.ArrayAusListeListeGeflact(AusgangZipArchiveListeSegment));
		}

		public void Berecne(int? ListeScritAnzaalScrankeMax)
		{
			var AingangVerzaicnisPfaad = this.AingangVerzaicnisPfaad;
			var AusgangVerzaicnisPfaad = this.AusgangVerzaicnisPfaad;

			if (null == AingangVerzaicnisPfaad)
			{
				return;
			}

			if (null == AusgangVerzaicnisPfaad)
			{
				return;
			}

			var AingangVerzaicnis = new System.IO.DirectoryInfo(AingangVerzaicnisPfaad);
			var AusgangVerzaicnis = new System.IO.DirectoryInfo(AusgangVerzaicnisPfaad);

			if (!AingangVerzaicnis.Exists)
			{
				return;
			}

			var AingangMengeDataiKandidaat = AingangVerzaicnis.GetFiles();

			if (null == AingangMengeDataiKandidaat)
			{
				return;
			}

			AingangMengeDataiKandidaat =
				AingangMengeDataiKandidaat
				.Where((KandidaatDatai) => !ListeDataiVerarbaitetPfaad.Contains(KandidaatDatai.FullName)).ToArray();

			//	Hiir werd davon ausgegange das Ordnung naac Naame = Ordnung naac Zait

			var AingangListeDataiKandidaat =
				AingangMengeDataiKandidaat.OrderBy((Kandidaat) => Kandidaat.Name).ToArray();

			var AingangListeDataiKandidaatRest = new Queue<System.IO.FileInfo>(AingangListeDataiKandidaat);

			for (int DataiVerarbaitetAnzaal = 0; true; DataiVerarbaitetAnzaal++)
			{
				if (AingangListeDataiKandidaatRest.Count < 1)
				{
					break;
				}

				if (ListeScritAnzaalScrankeMax < DataiVerarbaitetAnzaal)
				{
					break;
				}

				var AingangDatai = AingangListeDataiKandidaatRest.Dequeue();

				var AingangDataiPfaad = AingangDatai.FullName;

				try
				{
					var AingangDataiInhalt = Bib3.Glob.InhaltAusDataiMitPfaad(AingangDataiPfaad);

					var AusgangDataiPfaad = AusgangVerzaicnisPfaad + System.IO.Path.DirectorySeparatorChar + System.IO.Path.GetFileName(AingangDataiPfaad) + ".geplätet";

					this.AingangDatai(AingangDataiInhalt, AusgangDataiPfaad);
				}
				finally
				{
					ListeDataiVerarbaitetPfaad.Add(AingangDataiPfaad);
				}
			}
		}
	}
}
