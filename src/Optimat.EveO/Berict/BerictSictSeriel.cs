using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Optimat.EveOnline.Berict
{
	[JsonObject(MemberSerialization.OptIn)]
	public class SictBerictKeteGliid
	{
		[JsonProperty]
		public Int64? ZaitVonNuzerNaacServerVersazMili;

		[JsonProperty]
		public SictOptimatScrit[] ListeOptimatScrit;

		[JsonProperty]
		public Optimat.EveOnline.SictVonOptimatNaacrict[] MengeVonServerBerict;

		[JsonProperty]
		public SictWertMitZait<SictDataiIdentUndSuuceHinwais>[] MengeWindowClientRasterMitZaitMili;

		/// <summary>
		/// Menge der Pfaade der Verzaicnise in welce WindowClientRaster gescriibe wurde.
		/// </summary>
		[JsonProperty]
		public string[] MengeWindowClientRasterBerictVerzaicnisPfaad;

		public SictBerictKeteGliid()
		{
		}

		public SictBerictKeteGliid(
			Int64? ZaitVonNuzerNaacServerVersazMili,
			SictOptimatScrit[] ListeOptimatScrit,
			Optimat.EveOnline.SictVonOptimatNaacrict[] MengeVonServerBerict,
			SictWertMitZait<SictDataiIdentUndSuuceHinwais>[] MengeWindowClientRasterMitZaitMili = null)
		{
			this.ZaitVonNuzerNaacServerVersazMili = ZaitVonNuzerNaacServerVersazMili;

			this.ListeOptimatScrit = ListeOptimatScrit;
			this.MengeVonServerBerict = MengeVonServerBerict;
			this.MengeWindowClientRasterMitZaitMili = MengeWindowClientRasterMitZaitMili;
		}
	}
}
