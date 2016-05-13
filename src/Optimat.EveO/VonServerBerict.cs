using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Optimat.EveOnline;

namespace Optimat.EveOnline
{
	[JsonObject(MemberSerialization.OptIn)]
	public class SictVonServerBerict
	{
		[JsonProperty]
		public Int64? ZaitMili;

		[JsonProperty]
		public SictMissionZuusctand[] MengeMissionZuusctand;
	}
}
