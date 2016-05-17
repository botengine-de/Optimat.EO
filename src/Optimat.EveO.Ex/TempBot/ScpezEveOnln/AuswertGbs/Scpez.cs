using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Optimat.EveOnline.Base;
using Optimat.EveOnline.VonSensor;

namespace Optimat.EveOnline.VonSensor
{
	[JsonObject(MemberSerialization.OptIn)]
	public class MenuEntryScpez : MenuEntry
	{
		/// <summary>
		/// 2014.02.27	Bsp:
		/// "Stop The Thief (Uuhura Arikari)"
		/// 
		/// </summary>
		readonly static public string EntryAgentMissionRegexPattern = "\\s*([" +
			TempAuswertGbs.Extension.MissionTitelRegexMengeCharClass + "]+)\\s*\\(([\\w\\s\\d]+)\\)";

		[JsonProperty]
		public string AgentName;

		[JsonProperty]
		public string AgentMissionTitel;

		public MenuEntryScpez()
		{
		}

		public MenuEntryScpez(MenuEntry ZuKopiire)
			:
			base(ZuKopiire)
		{
		}

		static public string InSurroundingsMenuEntryAgentMissionRegexPattern(Optimat.EveOnline.SictMissionZuusctand Mission)
		{
			var MissionTitel = (null == Mission) ? null : Mission.Titel;
			var AgentName = (null == Mission) ? null : Mission.AgentName;

			return InSurroundingsMenuEntryAgentMissionRegexPattern(MissionTitel, AgentName);
		}

		/// <summary>
		/// 
		/// 2014.02.28	Bsp Menu Entry Bescriftung:
		/// "Rogue Drone Harassment (Uuhura Arikari)"
		/// 
		/// 2014.02.29	Bsp Menu Entry Bescriftung:
		/// "The Rogue Slave Trader (1 of 2) (Piertalen Enanama)"
		/// </summary>
		/// <param name="Mission"></param>
		/// <returns></returns>
		static public string InSurroundingsMenuEntryAgentMissionRegexPattern(
			string AgentMissionTitel = null,
			string AgentName = null)
		{
			var AgentMissionTitelPattern = "[" + TempAuswertGbs.Extension.MissionTitelRegexMengeCharClass + "]+";
			var AgentNamePattern = "[^\\)]+";

			if (null != AgentMissionTitel)
			{
				AgentMissionTitelPattern = Regex.Escape(AgentMissionTitel);
			}

			if (null != AgentName)
			{
				AgentNamePattern = Regex.Escape(AgentName);
			}

			var Pattern = "(" + AgentMissionTitelPattern + ")\\s*\\((" + AgentNamePattern + ")\\)";

			return Pattern;
		}

		public void BerecneAbgelaitete()
		{
			string AgentName = null;
			string AgentMissionTitel = null;

			try
			{
				var Bescriftung = this.Bescriftung;

				if (null != Bescriftung)
				{
					var SurroundingsButtonMenuEntryAgentMissionRegexPattern = InSurroundingsMenuEntryAgentMissionRegexPattern();

					var Match = Regex.Match(Bescriftung, SurroundingsButtonMenuEntryAgentMissionRegexPattern, RegexOptions.IgnoreCase);

					if (Match.Success)
					{
						AgentMissionTitel = Match.Groups[1].Value.Trim();
						AgentName = Match.Groups[2].Value.Trim();
					}
				}
			}
			finally
			{
				this.AgentName = AgentName;
				this.AgentMissionTitel = AgentMissionTitel;
			}
		}

		static public bool PrädikaatListeEntryAgentMissionTitel(MenuEntry KandidaatEntry)
		{
			return EntryBescriftungPasendZuRegexPattern(KandidaatEntry, "Agent Missions", RegexOptions.IgnoreCase);
		}

		static public MenuEntryScpez SictEntryAgentMission(MenuEntry MenuEntry)
		{
			if (null == MenuEntry)
			{
				return null;
			}

			var EntryScpez = new MenuEntryScpez(MenuEntry);

			/*
			 * 2014.02.28
			 * 
			 * Aigescaft EnthältAndere werd noc nit berecnet
			 * 
			if (!(true == EntryScpez.EnthältAndere))
			{
				return null;
			}
			 * */

			EntryScpez.BerecneAbgelaitete();

			if (null != EntryScpez.AgentMissionTitel && null != EntryScpez.AgentName)
			{
				return EntryScpez;
			}

			return null;
		}
	}

}
