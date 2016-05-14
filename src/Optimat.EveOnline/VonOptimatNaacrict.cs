using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Optimat.EveOnline
{
	[JsonObject(MemberSerialization.OptIn)]
	public class SictVonOptimatNaacrict : Optimat.EveOnline.Base.VonAutomatNaacrict
	{
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public Int64? AnwendungServerZaitMili;

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public Int64? AnwendungServerTaagBeginZaitMili;

		public SictVonOptimatNaacrict()
		{
		}

		public SictVonOptimatNaacrict(
			Int64? AnwendungServerZaitMili,
			Int64? AnwendungServerTaagBeginZaitMili,
			Bib3.RefNezDiferenz.SictRefNezDiferenzScritSictJson ZuusctandDiferenz)
			:
			base(ZuusctandDiferenz)
		{
			this.AnwendungServerZaitMili = AnwendungServerZaitMili;
			this.AnwendungServerTaagBeginZaitMili = AnwendungServerTaagBeginZaitMili;
		}
	}
}
