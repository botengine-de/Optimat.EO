using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Optimat.EveOnline
{
	[JsonObject(MemberSerialization.OptIn)]
	public class SictNaacOptimatMeldungZuusctand : Optimat.EveOnline.Base.NaacAutomatMeldungZuusctand
	{
		[JsonProperty]
		public string MainWindowTitle;

		[JsonProperty]
		public SictOptimatParam OptimatParam;

		[JsonProperty]
		public byte[] ClientIdent;

		[JsonProperty]
		public SictWertMitZait<string> TimerExceptionLezte;
	}

	/*
	2015.03.28

	[JsonObject(MemberSerialization.OptIn)]
	public class SictNaacOptimatNaacrict : Optimat.EveOnline.Base.NaacAutomatNaacrict
	{
		/// <summary>
		/// Scpezialisiirte Sict auf GBS Baum um klainere Abbild zu erhalte.
		/// Daher nit in Optimat.EveOnline.Base.SictNaacAutomatNaacrict.ZuusctandDiferenz enthalte.
		/// </summary>
		[JsonProperty]
		public SictGbsBaumSictDiferenzScritAbbild GbsBaumDiferenz;

		[JsonProperty]
		public SictWertMitZait<SictDataiIdentUndSuuceHinwais>[] MengeWindowClientRasterMitZaitMili;
	}
	*/
}
