using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimat.EveO.Nuzer.TempBot.ScpezEveOnln.AuswertGbs
{
	static public class Extension
	{
		//	"Bescriftung": "Antimatter Charge M [111]",
		//	"Bescriftung": "Antimatter Charge S [1.107]",
		public const string ModuleTurretMenuEntryChargeRegexPattern = @"Charge.*\[[^]*]*\]";

	}
}
