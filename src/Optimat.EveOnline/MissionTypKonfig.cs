using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Optimat.EveOnline
{
	[JsonObject(MemberSerialization.OptIn)]
	public class SictMissionTypKonfig
	{
		[JsonProperty]
		public	int?	AgentLevel
		{
			private set;
			get;
		}

		[JsonProperty]
		public string MissionTitelRegexPattern
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictFactionSictEnum[]	MengeFaction
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? FüüreAusFraigaabe
		{
			set;
			get;
		}

		[JsonProperty]
		public bool? OfferAcceptFraigaabe
		{
			set;
			get;
		}

		[JsonProperty]
		public bool? OfferDeclineFraigaabe
		{
			set;
			get;
		}

		public SictMissionTypKonfig(
			int?	AgentLevel,
			string MissionTitelRegexPattern,
			SictFactionSictEnum[] MengeFaction)
		{
			this.AgentLevel = AgentLevel;
			this.MissionTitelRegexPattern = MissionTitelRegexPattern;
			this.MengeFaction = MengeFaction;
		}
	}
}
