using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Bib3;

namespace Optimat.EveOnline
{
	public class SictBerictAusDsLeeserFund
	{
		readonly	public	FileSystemInfo FileSystemInfo;

		public byte[] DataiInhalt;

		public byte[] DataiInhaltHashSHA1;

		public Optimat.SictBerictKeteGliidBehältnisScpez<SictBerictKeteGliid> Behältnis;

		public SictBerictAusDsLeeserFund(
			FileSystemInfo FileSystemInfo,
			byte[] DataiInhalt = null,
			byte[] DataiInhaltHashSHA1 = null,
			Optimat.SictBerictKeteGliidBehältnisScpez<SictBerictKeteGliid> Behältnis = null)
		{
			this.FileSystemInfo = FileSystemInfo;
			this.DataiInhalt = DataiInhalt;
			this.DataiInhaltHashSHA1 = DataiInhaltHashSHA1;
			this.Behältnis = Behältnis;
		}
	}

	public class SictBerictAusDsLeeserBegin
	{
		readonly	public	DirectoryInfo	Directory;

		readonly public System.Exception Exception;

		public SictBerictAusDsLeeserBegin(
			DirectoryInfo Directory,
			System.Exception Exception)
		{
			this.Directory = Directory;
			this.Exception = Exception;
		}
	}

	public	class SictBerictAusDsLeeser
	{
		/*
		 * 2013.09.27
		 * 
		public string VerzaicnisPfaad
		{
			private set;
			get;
		}

		public	System.Exception	DataisysteemZuugrifException
		{
			private set;
			get;
		}

		public bool VerzaicnisZuugrifErfolg
		{
			get
			{
				return null == DataisysteemZuugrifException;
			}
		}
		 * */

		public	SictBerictAusDsLeeserBegin	BeginErgeebnis
		{
			private set;
			get;
		}

		public string VerzaicnisPfaad
		{
			get
			{
				var BeginErgeebnis = this.BeginErgeebnis;

				if (null == BeginErgeebnis)
				{
					return null;
				}

				var Directory = BeginErgeebnis.Directory;

				if (null == Directory)
				{
					return null;
				}

				return Directory.FullName;
			}
		}

		List<SictBerictAusDsLeeserFund> Kete;

		public int? ListeDataiAnzaal
		{
			get
			{
				var Kete = this.Kete;

				if (null == Kete)
				{
					return null;
				}

				return Kete.Count;
			}
		}

		public SictBerictAusDsLeeser(string VerzaicnisPfaad)
		{
			if (null != VerzaicnisPfaad)
			{
				this.Begin(VerzaicnisPfaad);
			}
		}

		static public Optimat.SictBerictKeteGliidBehältnisScpez<SictBerictKeteGliid> BerictKeteGliidBehältnisAusDatai(byte[] DataiSictListeOktet)
		{
			if (null == DataiSictListeOktet)
			{
				return null;
			}

			var DataiSictUTF8 = Encoding.UTF8.GetString(DataiSictListeOktet);

			var BerictKeteGliidBehältnis = JsonConvert.DeserializeObject<Optimat.SictBerictKeteGliidBehältnisScpez<SictBerictKeteGliid>>(DataiSictUTF8);

			return BerictKeteGliidBehältnis;
		}

		static	public SictBerictAusDsLeeserFund FundAusFileSystemInfo(
			FileSystemInfo	DataiFileSystemInfo,
			byte[] BedinungHashSHA1	= null)
		{
			if (null == DataiFileSystemInfo)
			{
				return null;
			}

			var	DataiInhalt	= Bib3.Glob.InhaltAusDataiMitPfaad(DataiFileSystemInfo.FullName);

			if(null	== DataiInhalt)
			{
				return	null;
			}

			var DataiInhaltHashSHA1 = new SHA1Managed().ComputeHash(DataiInhalt);

			if (null != BedinungHashSHA1)
			{
				if (!BedinungHashSHA1.SequenceEqual(DataiInhaltHashSHA1))
				{
					return null;
				}
			}

			var SictSctruktur = BerictKeteGliidBehältnisAusDatai(DataiInhalt);

			var Fund = new SictBerictAusDsLeeserFund(DataiFileSystemInfo, DataiInhalt, DataiInhaltHashSHA1, SictSctruktur);

			return Fund;
		}

		public IEnumerable<SictBerictAusDsLeeserFund> SuuceRükwärts(int	ScritAnzaalScrankeMax)
		{
			return Suuce(ScritAnzaalScrankeMax, 0);
		}

		public IEnumerable<SictBerictAusDsLeeserFund> Suuce(
			int SuuceRükwärtsScritAnzaalScrankeMax,
			int SuuceVorwärtsScritAnzaalScrankeMax)
		{
			var Kete = this.Kete;
			var BeginErgeebnis = this.BeginErgeebnis;

			if (null == Kete)
			{
				return null;
			}

			if (null == BeginErgeebnis)
			{
				return null;
			}

			var Directory = BeginErgeebnis.Directory;

			if (null == Directory)
			{
				return null;
			}

			if (!Directory.Exists)
			{
				return null;
			}

			/*
			 * 2013.10.03
			 * 
			var MengeFileSystemInfo = Directory.GetFileSystemInfos();
			 * */

			var MengeFileSystemInfo = Directory.GetFiles();

			if (null == MengeFileSystemInfo)
			{
				return null;
			}

			var MengeFileSystemInfoOrdnetNaacLastWriteTime =
				MengeFileSystemInfo
				.OrderBy((Kandidaat) => Kandidaat.LastWriteTimeUtc)
				.ToArray();

			var SuuceRükwärtsListeFund = new List<SictBerictAusDsLeeserFund>();
			var SuuceVorwärtsListeFund = new List<SictBerictAusDsLeeserFund>();

			var BisherBegin = Kete.FirstOrDefault();

			for (int i = 0; i < SuuceRükwärtsScritAnzaalScrankeMax; i++)
			{
				if (null == BisherBegin)
				{
					break;
				}

				var BisherBeginFileSystemInfo = BisherBegin.FileSystemInfo;

				var BisherBeginBehältnis = BisherBegin.Behältnis;

				if (null == BisherBeginBehältnis)
				{
					break;
				}

				var BisherBeginBehältnisAlgemain = BisherBeginBehältnis.Algemain;

				var BisherBeginBehältnisAlgemainVorherGliidDatai = BisherBeginBehältnisAlgemain.VorherGliidDatai;

				if (null == BisherBeginBehältnisAlgemainVorherGliidDatai)
				{
					break;
				}

				var BedinungHashSHA1 = BisherBeginBehältnisAlgemainVorherGliidDatai.IdentSHA1;

				if (null == BedinungHashSHA1)
				{
					break;
				}

				if (null == BisherBeginFileSystemInfo)
				{
					break;
				}

				var MengeKandidaatDatai =
					MengeFileSystemInfoOrdnetNaacLastWriteTime
					.Where((Kandidaat) => Kandidaat.LastWriteTimeUtc < BisherBeginFileSystemInfo.LastWriteTimeUtc)
					.Reverse()
					.Take(4)
					.ToArray();

				SictBerictAusDsLeeserFund Fund = null;

				foreach (var KandidaatDatai in MengeKandidaatDatai)
				{
					Fund = FundAusFileSystemInfo(KandidaatDatai, BedinungHashSHA1);

					if (null != Fund)
					{
						break;
					}
				}

				if (null == Fund)
				{
					break;
				}

				BisherBegin = Fund;

				SuuceRükwärtsListeFund.Add(Fund);
			}

			var BisherEnde = Kete.LastOrDefault();

			for (int i = 0; i < SuuceVorwärtsScritAnzaalScrankeMax; i++)
			{
				if (null == BisherEnde)
				{
					break;
				}

				var BisherEndeFileSystemInfo = BisherEnde.FileSystemInfo;

				var BisherEndeBehältnis = BisherEnde.Behältnis;

				var BedinungHashSHA1 = BisherEnde.DataiInhaltHashSHA1;

				if (null == BedinungHashSHA1)
				{
					break;
				}

				if (null == BisherEndeFileSystemInfo)
				{
					break;
				}

				var MengeKandidaatDatai =
					MengeFileSystemInfoOrdnetNaacLastWriteTime
					.Where((Kandidaat) => BisherEndeFileSystemInfo.LastWriteTimeUtc < Kandidaat.LastWriteTimeUtc)
					.Take(4)
					.ToArray();

				SictBerictAusDsLeeserFund Fund = null;

				foreach (var KandidaatDatai in MengeKandidaatDatai)
				{
					var	KandidaatFund = FundAusFileSystemInfo(KandidaatDatai,	null);

					if (null == KandidaatFund)
					{
						continue;
					}

					var KandidaatFundBehältnis = KandidaatFund.Behältnis;

					if (null == KandidaatFundBehältnis)
					{
						continue;
					}

					var	KandidaatFundBehältnisAlgemain	= KandidaatFundBehältnis.Algemain;

					if(null	== KandidaatFundBehältnisAlgemain)
					{
						continue;
					}

					var	KandidaatFundBehältnisAlgemainVorherGliidDatai	= KandidaatFundBehältnisAlgemain.VorherGliidDatai;

					if(null	== KandidaatFundBehältnisAlgemainVorherGliidDatai)
					{
						continue;
					}

					if (!BedinungHashSHA1.SequenceEqual(KandidaatFundBehältnisAlgemainVorherGliidDatai.IdentSHA1))
					{
						continue;
					}

					Fund = KandidaatFund;
					break;
				}

				if (null == Fund)
				{
					break;
				}

				BisherEnde = Fund;

				SuuceVorwärtsListeFund.Add(Fund);
			}

			var SuuceRükwärtsListeFundOrdnet = SuuceRükwärtsListeFund.Reversed().ToArray();

			Kete.InsertRange(0, SuuceRükwärtsListeFundOrdnet);
			Kete.AddRange(SuuceVorwärtsListeFund);

			return SuuceRükwärtsListeFundOrdnet.Concat(SuuceVorwärtsListeFund).ToArray();
		}

		public void Begin(string VerzaicnisPfaad)
		{
			var Kete = new List<SictBerictAusDsLeeserFund>();
			System.Exception DataisysteemZuugrifException	= null;
			DirectoryInfo Directory = null;

			try
			{
				if (null == VerzaicnisPfaad)
				{
					return;
				}

				if (!Path.IsPathRooted(VerzaicnisPfaad))
				{
					return;
				}

				Directory = new DirectoryInfo(VerzaicnisPfaad);

				if (null == Directory)
				{
					return;
				}

				if (!Directory.Exists)
				{
					return;
				}

				/*
				 * 2013.10.03
				 * 
				var MengeFileSystemInfo = Directory.GetFileSystemInfos();
				 * */

				var MengeFileSystemInfo = Directory.GetFiles();

				if (null == MengeFileSystemInfo)
				{
					return;
				}

				var BeginDataiFileSystemInfo =
					MengeFileSystemInfo
					.OrderBy((Kandidaat) => Kandidaat.LastWriteTimeUtc)
					.LastOrDefault();

				var Fund = FundAusFileSystemInfo(BeginDataiFileSystemInfo);

				Kete.Add(Fund);
			}
			catch (System.Exception Exception)
			{
				DataisysteemZuugrifException = Exception;
			}
			finally
			{
				this.Kete = Kete;
				this.BeginErgeebnis = new SictBerictAusDsLeeserBegin(Directory, DataisysteemZuugrifException);
			}
		}
	}

}
