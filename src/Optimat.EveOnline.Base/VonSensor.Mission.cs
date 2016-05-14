using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Optimat.EveOnline.AuswertGbs;

namespace Optimat.EveOnline.VonSensor
{
	[JsonObject(MemberSerialization.OptIn)]
	public class MissionLocation
	{
		/// <summary>
		/// Verwendet fals Typ = Location
		/// </summary>
		[JsonProperty]
		readonly public string LocationName;

		[JsonProperty]
		readonly public int? LocationSecurityLevelMili;

		[JsonProperty]
		readonly public	string LocationNameTailSystem;

		public MissionLocation()
		{
		}

		public MissionLocation(
			string LocationName,
			int? LocationSecurityLevelMili,
			string LocationNameTailSystem)
		{
			this.LocationName = LocationName;
			this.LocationSecurityLevelMili = LocationSecurityLevelMili;
			this.LocationNameTailSystem = LocationNameTailSystem;
		}

		public MissionLocation(MissionLocation ZuKopiire)
			:
			this(
			(null == ZuKopiire) ? null : ZuKopiire.LocationName,
			(null == ZuKopiire) ? null : ZuKopiire.LocationSecurityLevelMili,
			(null == ZuKopiire) ? null : ZuKopiire.LocationNameTailSystem)
		{
		}
	}

	public class MissionObjective
	{
		[JsonProperty]
		readonly public bool? Complete;

		[JsonProperty]
		[JsonConverter(typeof(StringEnumConverter))]
		readonly public SictMissionObjectiveObjectiveElementTyp? Typ;

		[JsonProperty]
		readonly public MissionLocation Location;

		/// <summary>
		/// Verwendet fals Typ = Item
		/// </summary>
		[JsonProperty]
		readonly public string ItemName;

		/// <summary>
		/// Objective sind in Baum organisiirt.
		/// </summary>
		[JsonProperty]
		readonly public MissionObjective[] MengeObjective;

		override public string ToString()
		{
			return JsonConvert.SerializeObject(this, Formatting.Indented);
		}

		public MissionObjective()
		{
		}

		public MissionObjective(
			bool? Complete,
			SictMissionObjectiveObjectiveElementTyp? Typ,
			MissionLocation Location,
			string ItemName = null,
			MissionObjective[] MengeObjective = null)
		{
			this.Complete = Complete;
			this.Typ = Typ;
			//	this.Location = new SictMissionLocation(Location);
			this.Location = Location;
			this.ItemName = ItemName;
			this.MengeObjective = MengeObjective;
		}

		public MissionObjective(MissionObjective ZuKopiire)
			:
			this(
			(null == ZuKopiire) ? null : ZuKopiire.Complete,
			(null == ZuKopiire) ? null : ZuKopiire.Typ,
			(null == ZuKopiire) ? null : ZuKopiire.Location,
			(null == ZuKopiire) ? null : ZuKopiire.ItemName,
			(null == ZuKopiire) ? null : ZuKopiire.MengeObjective)
		{
		}

		public MissionObjective(MissionObjective ZuKopiire, MissionObjective[] MengeObjective)
			:
			this(
			(null == ZuKopiire) ? null : ZuKopiire.Complete,
			(null == ZuKopiire) ? null : ZuKopiire.Typ,
			(null == ZuKopiire) ? null : ZuKopiire.Location,
			(null == ZuKopiire) ? null : ZuKopiire.ItemName,
			MengeObjective ?? ((null == ZuKopiire) ? null : ZuKopiire.MengeObjective))
		{
		}

	}

	/// <summary>
	/// Info zu Mission so wii aus AgentDialogue oder AgentJournal erhältlic
	/// </summary>
	[JsonObject(MemberSerialization.OptIn)]
	public class MissionInfo
	{
		[JsonProperty]
		readonly public string MissionTitel;

		[JsonProperty]
		readonly public int? RewardIskAnzaal;

		[JsonProperty]
		readonly public int? RewardLpAnzaal;

		[JsonProperty]
		readonly public int? BonusRewardIskAnzaal;

		[JsonProperty]
		readonly public MissionObjective Objective;

		public MissionInfo()
		{
		}

		public MissionInfo(
			string MissionTitel,
			MissionObjective Objective	= null,
			int? RewardIskAnzaal	= null,
			int? RewardLpAnzaal	= null,
			int? BonusRewardIskAnzaal	= null)
		{
			this.MissionTitel = MissionTitel;
			this.Objective = Objective;
			this.RewardIskAnzaal = RewardIskAnzaal;
			this.RewardLpAnzaal = RewardLpAnzaal;
			this.BonusRewardIskAnzaal = BonusRewardIskAnzaal;
		}
	}


}
