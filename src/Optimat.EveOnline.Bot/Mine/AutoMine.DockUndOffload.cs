using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Optimat.ScpezEveOnln;
using Bib3;
using Newtonsoft.Json;

namespace Optimat.EveOnline.Anwendung
{
	public partial class SictAutoMine
	{
		public IEnumerable<SictAufgaabeParam> FürMineListeAufgaabeNääxteParamBerecneTailDockUndOffload(
			ISictAutomatZuusctand AutomaatZuusctand)
		{
			if (null == AutomaatZuusctand)
			{
				return null;
			}

			var ListeAufgaabeParam = new List<SictAufgaabeParam>();
			return ListeAufgaabeParam;
		}
	}
}
