using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Optimat.EveOnline;
using Bib3;

namespace Optimat.EveO.Nuzer
{
	public class SictBerictKeteGliidSerialis
	{
		readonly public Optimat.SictBerictKeteGliidBehältnisScpez<Optimat.EveOnline.Berict.SictBerictKeteGliid> BerictKeteGliid;

		public string SerialisSictString
		{
			private set;
			get;
		}

		public byte[]	SerialisSictListeOktet
		{
			private set;
			get;
		}

		public byte[] SerialisHashSHA1;

		public SictBerictKeteGliidSerialis(Optimat.SictBerictKeteGliidBehältnisScpez<Optimat.EveOnline.Berict.SictBerictKeteGliid> BerictKeteGliid)
		{
			this.BerictKeteGliid = BerictKeteGliid;

			if (null != BerictKeteGliid)
			{
				var SerializerSettings = new JsonSerializerSettings();

				SerializerSettings.DefaultValueHandling = DefaultValueHandling.Ignore;

				SerialisSictString = JsonConvert.SerializeObject(BerictKeteGliid, SerializerSettings);
			}

			if (null != SerialisSictString)
			{
				SerialisSictListeOktet = Encoding.UTF8.GetBytes(SerialisSictString);
			}

			if (null != SerialisSictListeOktet)
			{
				SerialisHashSHA1 = new SHA1Managed().ComputeHash(SerialisSictListeOktet);
			}
		}
	}

	public	partial	class App
	{
		readonly List<Optimat.EveOnline.SictVonOptimatNaacrict> NaacDataiBerictNaacBerictVonAnwendungListeNaacrict =
			new List<Optimat.EveOnline.SictVonOptimatNaacrict>();

		Int64 BerictHauptOptimatScritGescriibeLezteZaitMili = -1;

		readonly List<SictWertMitZait<SictDataiIdentUndSuuceHinwais>> NaacDataiBerictListeWindowClientRasterGescriibe = new List<SictWertMitZait<SictDataiIdentUndSuuceHinwais>>();

		Int64 BerictHauptMengeDataiZaitDistanzScrankeMin = 10;
		Int64 BerictHauptMengeDataiLezteZaitMili;
		KeyValuePair<byte[],	Int64> BerictHauptFürASidListeDataiLezteIndex	= default(KeyValuePair<byte[],	Int64>);
		Int64? BerictHauptVonServerMengeBerictBezaicnerLezte = null;

		Int64 BerictHauptPersistKapazitäätScrankeKümereLezteZait;
		Int64 BerictWindowClientRasterPersistKapazitäätScrankeKümereLezteZait;

		KeyValuePair<SictBerictKeteGliidSerialis, string> BerictKeteGliidSerialisLezteMitDataiPfaad;

		void BerictKümere()
		{
			BerictHauptKümere();

			if (false)
			{
				//	Automaatisces entferne der Datai entfalt vorersct
				BerictVerzaicnisKapazitäätScrankeKümere();
			}
		}

		void BerictVerzaicnisKapazitäätScrankeKümere()
		{
			var ZaitMikro = Bib3.Glob.StopwatchZaitMikroSictInt();

			var ZaitMili = ZaitMikro / 1000;

			var Zait = ZaitMili / 1000;

			var GbsAingaabeKonfig = this.GbsAingaabeKonfig;

			if (null == GbsAingaabeKonfig)
			{
				return;
			}

			var BerictHauptPersistVerzaicnisPfaad = GbsAingaabeKonfig.BerictHauptPersistVerzaicnisPfaad;
			var BerictHauptPersistKapazitäätScranke = GbsAingaabeKonfig.BerictHauptPersistKapazitäätScranke;
			var BerictHauptPersistMengeDataiAnzaalScranke = GbsAingaabeKonfig.BerictHauptPersistMengeDataiAnzaalScranke;
			var BerictWindowClientRasterPersistVerzaicnisPfaad = GbsAingaabeKonfig.BerictWindowClientRasterPersistVerzaicnisPfaad;
			var	BerictWindowClientRasterPersistKapazitäätScranke	= GbsAingaabeKonfig.BerictWindowClientRasterPersistKapazitäätScranke;

			var BerictHauptPersistKapazitäätScrankeKümereLezteAlter = Zait - BerictHauptPersistKapazitäätScrankeKümereLezteZait;
			var BerictWindowClientRasterPersistKapazitäätScrankeKümereLezteAlter = Zait - BerictWindowClientRasterPersistKapazitäätScrankeKümereLezteZait;

			if (60 < BerictHauptPersistKapazitäätScrankeKümereLezteAlter)
			{
				BerictHauptPersistKapazitäätScrankeKümereLezteZait = Zait;

				if (null != BerictHauptPersistVerzaicnisPfaad	&&
					(BerictHauptPersistKapazitäätScranke.HasValue	||
					BerictHauptPersistMengeDataiAnzaalScranke.HasValue))
				{
					var Task = new Task(new Action(() =>
					{
						bool Erfolg;

						/*
						 * 2014.01.25
						 * 
						var ListeEraignis =
							Optimat.Glob.InVerzaichnisScteleSicerKapazitäätScrankeUndMengeDataiAnzaalScrankeEntferneÄltere(
							BerictHauptPersistVerzaicnisPfaad,
							BerictHauptPersistKapazitäätScranke,
							BerictHauptPersistMengeDataiAnzaalScranke,
							out	Erfolg);
						 * */

						System.Exception EntferneMengeDataiException;

						Bib3.Glob.AusVerzaicnisEntferneMengeDataiSolangeÜüberKapazitäätScranke(
							BerictHauptPersistVerzaicnisPfaad,
							BerictHauptPersistKapazitäätScranke.Value,
							false,
							out	Erfolg,
							out	EntferneMengeDataiException);

						if (!Erfolg)
						{
						}
					}));

					Task.Start();
				}
			}

			if (60 < BerictWindowClientRasterPersistKapazitäätScrankeKümereLezteAlter)
			{
				BerictWindowClientRasterPersistKapazitäätScrankeKümereLezteZait = Zait;

				if (null != BerictWindowClientRasterPersistVerzaicnisPfaad &&
					BerictWindowClientRasterPersistKapazitäätScranke.HasValue)
				{
					var Task = new Task(new Action(() =>
						{
							bool Erfolg;

							/*
							 * 2014.01.25
							 * 
							var	ListeEraignis	=
								Bib5.Glob.StelleSicherMengeDateiDirektInVerzeichnisKapazitätSchrankeEntferneÄltere(
								BerictWindowClientRasterPersistVerzaicnisPfaad,
								BerictWindowClientRasterPersistKapazitäätScranke.Value, true, out	Erfolg);
							 * */

							System.Exception EntferneMengeDataiException;

							Bib3.Glob.AusVerzaicnisEntferneMengeDataiSolangeÜüberKapazitäätScranke(
								BerictWindowClientRasterPersistVerzaicnisPfaad,
								BerictWindowClientRasterPersistKapazitäätScranke.Value,
								false,
								out	Erfolg,
								out	EntferneMengeDataiException);

							if (!Erfolg)
							{
							}
						}));

					Task.Start();
				}
			}
		}

		void BerictHauptKümere()
		{
			var ZaitMili = Bib3.Glob.StopwatchZaitMikroSictInt() / 1000;

			string ZiilVerzaicnisPfaad = null;

			var BerictKeteGliidSerialisLezteMitDataiPfaad = this.BerictKeteGliidSerialisLezteMitDataiPfaad;

			var GbsAingaabeKonfig = this.GbsAingaabeKonfig;

			if (null != GbsAingaabeKonfig)
			{
				ZiilVerzaicnisPfaad = GbsAingaabeKonfig.BerictHauptPersistVerzaicnisPfaad;
			}

			if (null == ZiilVerzaicnisPfaad)
			{
				return;
			}

			if (ZiilVerzaicnisPfaad.Length < 1)
			{
				return;
			}

			var BerictHauptLezteAlterMili = ZaitMili - BerictHauptMengeDataiLezteZaitMili;

			if (BerictHauptLezteAlterMili / 1000 < BerictHauptMengeDataiZaitDistanzScrankeMin)
			{
				return;
			}

			BerictHauptMengeDataiLezteZaitMili = ZaitMili;

			try
			{
				var ZiilVerzaicnis = new DirectoryInfo(ZiilVerzaicnisPfaad);

				if (!ZiilVerzaicnis.Exists)
				{
					ZiilVerzaicnis.Create();
				}
			}
			catch (System.Exception Exception)
			{
				return;
			}

			var BerictKeteGliidAlgemain = new SictBerictKeteGliid(ZaitMili * 1000);

			if (null != BerictKeteGliidSerialisLezteMitDataiPfaad.Key)
			{
				var VorherGliidDatai = new SictDataiIdentUndSuuceHinwais(
					BerictKeteGliidSerialisLezteMitDataiPfaad.Key.SerialisHashSHA1,
					new SictDataiSuuceHinwais[] { new SictDataiSuuceHinwais(BerictKeteGliidSerialisLezteMitDataiPfaad.Value) });

				BerictKeteGliidAlgemain.VorherGliidDatai = VorherGliidDatai;
			}

			//	!!!!	Temp Scpez für Nuzer und Server in glaicem Prozes	!!!!
			Int64? ZaitVonNuzerNaacServerVersazMili = null;

			var Nuzer = this.Nuzer;

			if(null	!= Nuzer.Key)
			{
				var NuzerMesungAnwendungServerZaitMikroNulbar = Nuzer.Key.MesungAnwendungServerZaitMikro;

				if (NuzerMesungAnwendungServerZaitMikroNulbar.HasValue)
				{
					var NuzerMesungAnwendungServerZaitMikro = NuzerMesungAnwendungServerZaitMikroNulbar.Value;

					ZaitVonNuzerNaacServerVersazMili = (NuzerMesungAnwendungServerZaitMikro.Wert - NuzerMesungAnwendungServerZaitMikro.Zait) / 1000;
				}
			}

			var VonServerMengeBerictZuScraibe =
				Bib3.Extension.SelectNullable(
				NaacDataiBerictNaacBerictVonAnwendungListeNaacrict,
				(VonAnwendungNaacrict) => new SictVonOptimatNaacrict(
					VonAnwendungNaacrict.AnwendungServerZaitMili,
					VonAnwendungNaacrict.AnwendungServerTaagBeginZaitMili,
					null))
				.ToArrayNullable();

			NaacDataiBerictNaacBerictVonAnwendungListeNaacrict.Clear();

			var BerictListeWindowClientRasterGescriibe = this.NaacDataiBerictListeWindowClientRasterGescriibe.ToArray();

			var	ListeOptimatScritNaacLezteBerict	=
				ListeVonServerMeldungAbbildOptimatScrit
				.SkipWhile((OptimatScrit) => !(BerictHauptOptimatScritGescriibeLezteZaitMili	< OptimatScrit.NuzerZait))
				.ToArray();

			byte[] AnwendungSizungIdent = null;

			var ListeOptimatScritZuScraibe = new List<SictOptimatScrit>();

			foreach (var OptimatScrit in ListeOptimatScritNaacLezteBerict)
			{
				if (ListeOptimatScritZuScraibe.Count < 1)
				{
					AnwendungSizungIdent = OptimatScrit.AnwendungSizungIdent;
				}
				else
				{
					if (!Bib3.Glob.SequenceEqualPerObjectEquals(AnwendungSizungIdent, OptimatScrit.AnwendungSizungIdent))
					{
						//	AnwendungSizungIdent des Optimat Scrit isc unglaic AnwendungSizungIdent des frühescte Optimat Scrit in Liste

						//	Optimat Scrit aus versciidene Sizunge sole nit in aine Datai zusamegefast werde, daher hiir Abbruc Liste
						break;
					}
				}

				if (null == OptimatScrit.NaacProcessListeWirkung)
				{
					var ScritAlterMili = ZaitMili - OptimatScrit.NuzerZait;

					if (!(1e+4 < ScritAlterMili))
					{
						break;
					}
				}

				ListeOptimatScritZuScraibe.Add(OptimatScrit);
			}

			var ListeOptimatScritZuScraibeAbbild =
				ListeOptimatScritZuScraibe
				.Select((OptimatScrit) => SictOptimatScrit.OptimatScritSictFürBerict(OptimatScrit,	false))
				.ToArray();

			var AnwendungSizungIdentSictString =
				Bib3.Glob.SictZaalSictStringBaasis16(AnwendungSizungIdent)	?? "null";

			var BerictKeteGliidScpez = new Optimat.EveOnline.Berict.SictBerictKeteGliid(
				ZaitVonNuzerNaacServerVersazMili,
				ListeOptimatScritZuScraibeAbbild,
				VonServerMengeBerictZuScraibe,
				BerictListeWindowClientRasterGescriibe);

			if (null != ListeOptimatScritZuScraibe)
			{
				foreach (var OptimatScrit in ListeOptimatScritZuScraibe.Reversed())
				{
					/*
					 * 2014.11.07
					 * 
					var OptimatScritVonZiilProcessLeeseBeginZaitMili = SictOptimatScrit.AusOptimatScritVonProcessLeeseBeginZaitMili(OptimatScrit);

					if (OptimatScritVonZiilProcessLeeseBeginZaitMili.HasValue)
					{
						BerictHauptOptimatScritGescriibeLezteZaitMili = OptimatScritVonZiilProcessLeeseBeginZaitMili.Value;
						break;
					}
					 * */

					BerictHauptOptimatScritGescriibeLezteZaitMili = OptimatScrit.NuzerZait;
					break;
				}
			}

			this.NaacDataiBerictListeWindowClientRasterGescriibe.Clear();

			var BerictKeteGliid = new Optimat.SictBerictKeteGliidBehältnisScpez<Optimat.EveOnline.Berict.SictBerictKeteGliid>(BerictKeteGliidAlgemain, BerictKeteGliidScpez);

			var BerictKeteGliidSerialis = new SictBerictKeteGliidSerialis(BerictKeteGliid);

			var ZaitSictKalenderString = Bib3.Glob.SictwaiseKalenderString(Bib3.Glob.SictDateTimeVonStopwatchZaitMili(ZaitMili), ".", 0);

			Int64 BerictHauptListeDataiLezteIndex = 0;

			if (Bib3.Glob.SequenceEqualPerObjectEquals(BerictHauptFürASidListeDataiLezteIndex.Key, AnwendungSizungIdent))
			{
				BerictHauptListeDataiLezteIndex = BerictHauptFürASidListeDataiLezteIndex.Value;
			}

			var	VerzaicnisNaame	= "Berict[ASId=" + AnwendungSizungIdentSictString + "]";

			var FürAnwendungSizungVerzaicnisPfaad = ZiilVerzaicnisPfaad + Path.DirectorySeparatorChar + VerzaicnisNaame;

			var Verzaicnis = new DirectoryInfo(FürAnwendungSizungVerzaicnisPfaad);

			if (!Verzaicnis.Exists)
			{
				Verzaicnis.Create();
			}

			var DataiNaame = ZaitSictKalenderString + ".Berict.Haupt[ASId=" + AnwendungSizungIdentSictString + ",DI=" + BerictHauptListeDataiLezteIndex.ToString() + "]";

			var DataiPfaad = FürAnwendungSizungVerzaicnisPfaad + Path.DirectorySeparatorChar + DataiNaame;

			bool ScraibeDataiErfolg;
			System.Exception ScraibeDataiException;

			Bib3.Glob.ScraibeInhaltNaacDataiPfaad(DataiPfaad, BerictKeteGliidSerialis.SerialisSictListeOktet, out	ScraibeDataiErfolg, out	ScraibeDataiException);

			BerictHauptFürASidListeDataiLezteIndex = new KeyValuePair<byte[], Int64>(AnwendungSizungIdent, BerictHauptListeDataiLezteIndex + 1);

			this.BerictKeteGliidSerialisLezteMitDataiPfaad = new KeyValuePair<SictBerictKeteGliidSerialis, string>(BerictKeteGliidSerialis, DataiNaame);

			if (null != ScraibeDataiException)
			{
				throw new ApplicationException("Scraibe Datai Feelsclaag", ScraibeDataiException);
			}
		}
	}
}
