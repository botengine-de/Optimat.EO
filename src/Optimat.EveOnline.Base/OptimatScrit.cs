using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bib3;
using Newtonsoft.Json;
using Optimat.EveOnline.Base;

namespace Optimat.EveOnline
{
	[JsonObject(MemberSerialization.OptIn)]
	public class SictVerlaufBeginUndEndeRefMitZaitAinhaitMili<T> : SictVerlaufBeginUndEndeRef<T>
	{
		public Int64? BeginZaitMili
		{
			get
			{
				return this.BeginZait;
			}
		}

		public Int64? EndeZaitMili
		{
			get
			{
				return this.EndeZait;
			}
		}

		public SictVerlaufBeginUndEndeRefMitZaitAinhaitMili()
		{
		}

		public SictVerlaufBeginUndEndeRefMitZaitAinhaitMili(
			Int64? BeginZaitMili,
			Int64? EndeZaitMili = null,
			T Wert	= default(T))
			:
			base(
			BeginZaitMili,
			EndeZaitMili,
			Wert)
		{
		}
	}


	[JsonObject(MemberSerialization.OptIn)]
	public class SictVonWindowLeeseErgeebnis : SictVerlaufBeginUndEndeRefMitZaitAinhaitMili<object>
	{
		[JsonProperty]
		public SictVektor2DSingle? ClientRasterGrööse;

		[JsonProperty]
		public string ClientRasterZiilDataiPfaad;

		[JsonProperty]
		public byte[] ClientRasterSictSerielHashSHA1;

		[JsonProperty]
		public bool? Erfolg;

		[JsonProperty]
		public Optimat.SictExceptionSictJson Exception;

		public SictVonWindowLeeseErgeebnis()
		{
		}

		public SictVonWindowLeeseErgeebnis(
			Int64? BeginZaitMili,
			Int64? EndeZaitMili,
			Optimat.SictExceptionSictJson Exception = null,
			string ClientRasterZiilDataiPfaad = null,
			SictVektor2DSingle? ClientRasterGrööse	= null,
			byte[] ClientRasterSictSerielHashSHA1 = null,
			bool? Erfolg = null)
			:
			base(BeginZaitMili, EndeZaitMili)
		{
			this.Exception = Exception;
			this.ClientRasterZiilDataiPfaad = ClientRasterZiilDataiPfaad;
			this.ClientRasterGrööse = ClientRasterGrööse;
			this.ClientRasterSictSerielHashSHA1 = ClientRasterSictSerielHashSHA1;
			this.Erfolg = Erfolg;
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictNaacProcessWirkung : SictVerlaufBeginUndEndeRefMitZaitAinhaitMili<object>
	{
		[JsonProperty]
		public Int64? VorsclaagWirkungIdent;

		[JsonProperty]
		public bool? Erfolg;

		[JsonProperty]
		readonly public SictExceptionSictJson Exception;

		[JsonProperty]
		readonly public SictNaacProcessWirkungTailMausErgeebnis TailMaus;

		public SictNaacProcessWirkung()
		{
		}

		public SictNaacProcessWirkung(
			Int64? BeginZaitMili,
			Int64? EndeZaitMili,
			Int64? VorsclaagWirkungIdent,
			SictNaacProcessWirkungTailMausErgeebnis TailMaus,
			bool? Erfolg,
			SictExceptionSictJson Exception)
			:
			base(BeginZaitMili, EndeZaitMili)
		{
			this.VorsclaagWirkungIdent = VorsclaagWirkungIdent;
			this.TailMaus = TailMaus;
			this.Erfolg = Erfolg;
			this.Exception = Exception;
		}
	}

}
