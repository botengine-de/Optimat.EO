using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bib3;
using Newtonsoft.Json;
using Optimat.EveOnline.Base;
using ExtractFromOldAssembly.Bib3;

namespace Optimat.EveOnline
{
	[JsonObject(MemberSerialization.OptIn)]
	public class SictOptimatScrit
	{
		[JsonProperty]
		public Int64 NuzerZait;

		[JsonProperty]
		public byte[] NuzerSizungIdent;

		[JsonProperty]
		public byte[] AnwendungSizungIdent;

		[JsonProperty]
		public Int64? AnwendungZaitMili;

		/*
		 * 2015.03.03
		 * 
		[JsonProperty]
		public SictVonProcessLeese VonProcessLeese;
		 * */

		[JsonProperty]
		public VonProcessMesung<VonSensorikMesung> VonProcessMesung;

		[JsonProperty]
		public SictVonWindowLeeseErgeebnis VonWindowLeese;

		[JsonProperty]
		public SictVorsclaagNaacProcessWirkung[] VorsclaagListeWirkung;

		[JsonProperty]
		public SictNaacProcessWirkung[] NaacProcessListeWirkung;

		[JsonProperty]
		public Bib3.RefNezDiferenz.SictZuNezSictDiferenzScritAbbild NaacNuzerBerictAutomaatZuusctandScritDiferenz;

		/*
		 * 2015.02.03
		 * 
		[JsonProperty]
		public SictVonOptimatMeldungZuusctand NaacNuzerBerictAutomaatZuusctand;
		 * */

		[JsonProperty]
		public VonAutomatMeldungZuusctand NaacNuzerBerictAutomaatZuusctand;

		[JsonProperty]
		public SictDataiIdentUndSuuceHinwais ProcessWindowClientRasterIdentUndSuuceHinwais;

		public Int64? VonProcessLeeseBeginZait
		{
			get
			{
				var VonProcessLeese = this.VonProcessMesung;

				if (null == VonProcessLeese)
				{
					return null;
				}

				return VonProcessLeese.BeginZait;
			}
		}

		/*
		 * 2015.03.03
		 * 
		public GbsAstInfo VonProcessLeeseGbsBaum
		{
			get
			{
				var VonProcessLeese = this.VonProcessLeese;

				if (null == VonProcessLeese)
				{
					return null;
				}

				return VonProcessLeese.ErgeebnisGbsBaum;
			}
		}

		public Int64? VonProcessLeeseEndeZait
		{
			get
			{
				var VonProcessLeese = this.VonProcessLeese;

				if (null == VonProcessLeese)
				{
					return null;
				}

				return VonProcessLeese.EndeZait;
			}
		}
		 * */

		public SictOptimatScrit()
		{
		}

		/*
		 * 2015.03.03
		 * 
		public SictOptimatScrit(
			Int64	NuzerZait,
			SictVonProcessLeese VonProcessLeese,
			SictVonWindowLeeseErgeebnis VonWindowLeese = null
			,
			SictVorsclaagNaacProcessWirkung[] VorsclaagListeWirkung = null,
			SictNaacProcessWirkung[] NaacProcessListeWirkung = null
			)
		{
			this.NuzerZait = NuzerZait;
			this.VonProcessLeese = VonProcessLeese;
			this.VonWindowLeese = VonWindowLeese;

			this.VorsclaagListeWirkung = VorsclaagListeWirkung;
			this.NaacProcessListeWirkung = NaacProcessListeWirkung;

		}
		 * */

		public SictOptimatScrit(
			Int64 NuzerZait,
			VonProcessMesung<VonSensorikMesung> VonSensorikMesung,
			SictVonWindowLeeseErgeebnis VonWindowLeese = null
			,
			SictVorsclaagNaacProcessWirkung[] VorsclaagListeWirkung = null,
			SictNaacProcessWirkung[] NaacProcessListeWirkung = null
			)
		{
			this.NuzerZait = NuzerZait;
			this.VonProcessMesung = VonSensorikMesung;
			this.VonWindowLeese = VonWindowLeese;

			this.VorsclaagListeWirkung = VorsclaagListeWirkung;
			this.NaacProcessListeWirkung = NaacProcessListeWirkung;
		}

		/*
		 * 2015.03.03
		 * 
		static	public SictOptimatScrit	AusMengeOptimatScritZuBeginZaitScrit(
			IEnumerable<SictOptimatScrit> MengeOptimatScrit, 
			SictOptimatScrit Scrit)
		{
			if (null == Scrit)
			{
				return null;
			}

			var VonProcessLeese = Scrit.VonProcessLeese;

			if (null == VonProcessLeese)
			{
				return null;
			}

			return AusMengeOptimatScritZuBeginZaitScrit(MengeOptimatScrit,	VonProcessLeese.BeginZaitMili);
		}

		static	public Int64? AusOptimatScritVonProcessLeeseBeginZaitMili(
			SictOptimatScrit OptimatScrit)
		{
			if (null == OptimatScrit)
			{
				return null;
			}

			var OptimatScritVonProcessLeese = OptimatScrit.VonProcessLeese;

			if (null == OptimatScritVonProcessLeese)
			{
				return	null;
			}

			return OptimatScritVonProcessLeese.BeginZaitMili;
		}

		static	public SictOptimatScrit AusMengeOptimatScritZuBeginZaitScrit(
			IEnumerable<SictOptimatScrit>	MengeOptimatScrit,
			Int64?	VonProcessLeeseBeginZaitMili)
		{
			if (null == MengeOptimatScrit)
			{
				return null;
			}

			if (!VonProcessLeeseBeginZaitMili.HasValue)
			{
				return null;
			}

			foreach (var KandidaatOptimatScrit in MengeOptimatScrit)
			{
				if (AusOptimatScritVonProcessLeeseBeginZaitMili(KandidaatOptimatScrit) == VonProcessLeeseBeginZaitMili)
				{
					return KandidaatOptimatScrit;
				}
			}

			return null;
		}
		 * */

		static public VonProcessMesung<VonSensorikMesung> OptimatScritSictFürBerict(
			VonProcessMesung<VonSensorikMesung> VonProcessLeese,
			bool GbsBaumErhalte)
		{
			if (null == VonProcessLeese)
			{
				return null;
			}

			var VonProcessLeeseAbbild = new VonProcessMesung<VonSensorikMesung>(
				null,
				VonProcessLeese.BeginZait,
				VonProcessLeese.EndeZait,
				VonProcessLeese.ProcessId);

			return VonProcessLeeseAbbild;
		}

		static public SictOptimatScrit OptimatScritSictFürBerict(
			SictOptimatScrit OptimatScrit,
			bool GbsBaumErhalte)
		{
			if (null == OptimatScrit)
			{
				return null;
			}

			var VonProcessLeese = OptimatScrit.VonProcessMesung;

			var VonProcessLeeseAbbild = OptimatScritSictFürBerict(VonProcessLeese, GbsBaumErhalte);

			var OptimatScritAbbild = new SictOptimatScrit(
				OptimatScrit.NuzerZait,
				VonProcessLeeseAbbild,
				SictRefBaumKopii.ObjektKopiiErsctele(OptimatScrit.VonWindowLeese),
				SictRefBaumKopii.ObjektKopiiErsctele(OptimatScrit.VorsclaagListeWirkung),
				SictRefBaumKopii.ObjektKopiiErsctele(OptimatScrit.NaacProcessListeWirkung));

			OptimatScritAbbild.AnwendungSizungIdent = SictRefBaumKopii.ObjektKopiiErsctele(OptimatScrit.AnwendungSizungIdent);
			OptimatScritAbbild.AnwendungZaitMili = OptimatScrit.AnwendungZaitMili;

			OptimatScritAbbild.NaacNuzerBerictAutomaatZuusctand = SictRefBaumKopii.ObjektKopiiErsctele(OptimatScrit.NaacNuzerBerictAutomaatZuusctand);
			OptimatScritAbbild.NaacNuzerBerictAutomaatZuusctandScritDiferenz = SictRefBaumKopii.ObjektKopiiErsctele(OptimatScrit.NaacNuzerBerictAutomaatZuusctandScritDiferenz);

			OptimatScritAbbild.ProcessWindowClientRasterIdentUndSuuceHinwais = SictRefBaumKopii.ObjektKopiiErsctele(OptimatScrit.ProcessWindowClientRasterIdentUndSuuceHinwais);

			return OptimatScritAbbild;
		}


	}
}
