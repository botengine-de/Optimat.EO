using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
//using Optimat.EveOnline.AuswertGbs;
using Optimat.EveOnline.VonSensor;


namespace Optimat.EveOnline.Anwendung
{
	public class SictFaction
	{
		static public KeyValuePair<SictFactionSictEnum, string>[] ZuFactionTitelStringExtra =
			new KeyValuePair<SictFactionSictEnum, string>[]{
				//	new	KeyValuePair<SictFactionSictEnum,	string>(SictFactionSictEnum.Mordus_Legion, "Mordu's Legion"),
			};

	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictStringEntscaidungBinärPerRegexAinfacMitZuEntfernende
	{
		[JsonProperty]
		public string RegexPattern;

		/// <summary>
		/// Werd für RegexPattern und MengeZuEntfernendeRegexPattern angewandt.
		/// </summary>
		[JsonConverter(typeof(StringEnumConverter))]
		[JsonProperty]
		public RegexOptions RegexOptions;

		/// <summary>
		/// Menge von Pattern deren Matche zuvor aus dem String entfernt were sole.
		/// </summary>
		[JsonProperty]
		public string[] MengeZuEntfernendeRegexPattern;

		public SictStringEntscaidungBinärPerRegexAinfacMitZuEntfernende()
		{
		}

		public SictStringEntscaidungBinärPerRegexAinfacMitZuEntfernende(
			string RegexPattern,
			RegexOptions RegexOptions,
			string[] MengeZuEntfernendeRegexPattern = null)
		{
			this.RegexPattern = RegexPattern;
			this.RegexOptions = RegexOptions;
			this.MengeZuEntfernendeRegexPattern = MengeZuEntfernendeRegexPattern;
		}

		public bool? EntscaidungNulbarBerecneFürString(string String)
		{
			var Match = MatchBerecneFürString(String);

			if (null == Match)
			{
				return null;
			}

			return Match.Success;
		}

		public Match MatchBerecneFürString(string String)
		{
			if (null == String)
			{
				return null;
			}

			var RegexPattern = this.RegexPattern;
			var RegexOptions = this.RegexOptions;
			var MengeZuEntfernendeRegexPattern = this.MengeZuEntfernendeRegexPattern;

			if (null == RegexPattern)
			{
				return null;
			}

			var StringSictReplaceAbbild = String;

			if (null != MengeZuEntfernendeRegexPattern)
			{
				for (int ZuEntfernendeIndex = 0; ZuEntfernendeIndex < MengeZuEntfernendeRegexPattern.Length; ZuEntfernendeIndex++)
				{
					var ZuEntfernendeRegexPattern = MengeZuEntfernendeRegexPattern[ZuEntfernendeIndex];

					if (null == ZuEntfernendeRegexPattern)
					{
						continue;
					}

					StringSictReplaceAbbild = Regex.Replace(StringSictReplaceAbbild, ZuEntfernendeRegexPattern, "", RegexOptions);
				}
			}

			var Match = Regex.Match(StringSictReplaceAbbild, RegexPattern, RegexOptions);

			return Match;
		}

		static public SictStringEntscaidungBinärPerRegexAinfacMitZuEntfernende EntscaidungUnbedingtErsctele()
		{
			var Entscaidung = new SictStringEntscaidungBinärPerRegexAinfacMitZuEntfernende(".*", RegexOptions.None);

			return Entscaidung;
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictMissionTitel
	{
		[JsonProperty]
		public string MissionTitelRegexPattern;

		/// <summary>
		/// Sctandardwert fals kaine Faction in AgentDialogue gemese werd.
		/// </summary>
		[JsonProperty]
		readonly public SictFactionSictEnum? Faction;

		/*
		 * 2015.02.07
		 * 
		[JsonProperty]
		public SictMissionObjectiveUndDetailsStringEntscaidungAnnaameFaction[] MengeEntscaidungAnnaameFaction;
		 * */

		public bool PasendZuTitel(string ZuPrüüfeMissionTitel)
		{
			var MissionTitelRegexPattern = this.MissionTitelRegexPattern;

			if (null == ZuPrüüfeMissionTitel || null == MissionTitelRegexPattern)
			{
				return false;
			}

			return Regex.Match(ZuPrüüfeMissionTitel, MissionTitelRegexPattern, RegexOptions.IgnoreCase).Success;
		}

		/// <summary>
		/// </summary>
		/// <param name="MissionInfo"></param>
		/// <returns>
		/// Falls Titel unpasend: null.
		/// Sunsct: Menge aller Faction welce zu MissionObjective und MissionDetails passen.
		/// </returns>
		public SictFactionSictEnum[] ZuMissionMengeFaction(
			WindowAgentMissionInfo MissionInfo)
		{
			if (null == MissionInfo)
			{
				return null;
			}

			if (!PasendZuTitel(MissionInfo.MissionTitel))
			{
				return null;
			}

			var MengeFaction = new List<SictFactionSictEnum>();

			if (null != MissionInfo.MengeFaction)
			{
				MengeFaction.AddRange(MissionInfo.MengeFaction);
			}

			return MengeFaction.Distinct().ToArray();
		}

		public SictMissionTitel()
		{
		}

		public SictMissionTitel(
			string MissionTitelRegexPattern,
			SictFactionSictEnum? Faction	= null)
		{
			this.MissionTitelRegexPattern = MissionTitelRegexPattern;
			this.Faction = Faction;
		}

		static public SictMissionTitel MissionTitelMitEntscaidungFactionStandard(
			string MissionTitelRegexPattern)
		{
			return new SictMissionTitel(MissionTitelRegexPattern);
		}

		static public SictMissionTitel MissionTitelMitEntscaidungFactionKonstant(
			string MissionTitelRegexPattern,
			SictFactionSictEnum Faction)
		{
			return new SictMissionTitel(MissionTitelRegexPattern, Faction);
		}
	}
}
