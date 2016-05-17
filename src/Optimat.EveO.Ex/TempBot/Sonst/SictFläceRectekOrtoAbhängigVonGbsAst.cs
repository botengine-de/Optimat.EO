using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bib3;
using Optimat.EveOnline;
using Optimat.EveOnline.VonSensor;
using ExtractFromOldAssembly.Bib3;

namespace Optimat.EveO.Nuzer.TempBot.Sonst
{
	public class SictFläceRectekOrtoAbhängigVonGbsAst : InProcessGbsFläceRectekOrto
	{
		readonly public GbsElement GbsElement;

		public SictFläceRectekOrtoAbhängigVonGbsAst()
		{
		}

		public SictFläceRectekOrtoAbhängigVonGbsAst(
			OrtogoonInt FläceTailSctaatisc,
			GbsElement GbsElement)
			:
			base(FläceTailSctaatisc)
		{
			this.GbsElement = GbsElement;
		}
	}
}
