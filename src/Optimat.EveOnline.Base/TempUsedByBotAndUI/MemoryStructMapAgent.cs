using Optimat.EveOnline.AuswertGbs;
using Optimat.EveOnline.VonSensor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MemoryStruct = BotEngine.EveOnline.Interface.MemoryStruct;

namespace Optimat.EveOnline
{
	static public class MemoryStructMapAgent
	{
		static string WindowAgentHeaderCaptionTextRegexPatternAgentConversation()
		{
			return "(Agent Conversation|Mission Journal)" + "\\s*" + Regex.Escape("-") + "([\\s\\w\\d]+)";
		}

		static public string AgentName(this MemoryStruct.WindowAgent WindowAgent)
		{
			var HeaderCaptionTextMatch = Regex.Match(WindowAgent?.Caption ?? "", WindowAgentHeaderCaptionTextRegexPatternAgentConversation(), RegexOptions.IgnoreCase);

			if (HeaderCaptionTextMatch.Success)
			{
				return HeaderCaptionTextMatch.Groups[2].Value.Trim();
			}

			return null;
		}

		static public VonSensor.WindowAgent AlsWindowAgent(this MemoryStruct.WindowAgent WindowAgent)
		{
			if (null == WindowAgent)
			{
				return null;
			}

			return new VonSensor.WindowAgent(WindowAgent.AlsWindowNurBase(), WindowAgent.AgentName(), null, null);
		}

		static public VonSensor.WindowAgentBrowser AlsWindowAgentBrowser(this MemoryStruct.WindowAgentBrowser WindowAgentBrowser)
		{
			if (null == WindowAgentBrowser)
			{
				return null;
			}

			return new VonSensor.WindowAgentBrowser(WindowAgentBrowser.AlsWindowAgent());
		}

		/// <summary>
		/// 2014.00.07	Bsp:
		/// "<br>Declining a mission from a particular agent more than once every 4 hours will result in a loss of standing with that agent.<br>"
		/// 
		/// 16.02.10 Bsp:
		/// Declining a mission from a particular agent more than once every 4 hours may result in a loss of standing with that agent
		/// </summary>
		static readonly string MissionDeclineFraigaabeRegexPattern =
			"Declining a mission from a particular agent more than once every (\\d+) hours (may|will) result in a loss of standing with that agent";

		/// <summary>
		/// 2014.00.07	Bsp:
		/// "<br>Declining a mission from this agent within the next 3 hours and 57 minutes will result in a loss of standing with this agent.<br>"
		/// </summary>
		static readonly string MissionDeclineFraigaabeNictMitDauerRegexPattern =
			"Declining a mission from this agent within the next ([\\w\\d\\s]{1,40}) will result in a loss";

		static readonly string MissionDeclineFraigaabeNictDauerKomponenteMitAinhaitRegexPattern =
			"(\\d+)\\s*(\\w+)";

		static readonly KeyValuePair<string, int>[] MissionDeclineDauerMengeAinhaitRegexPatternUndBetraagSekunde =
			new KeyValuePair<string, int>[]{
				new KeyValuePair<string,    int>("hour", 60 * 60),
				new KeyValuePair<string,    int>("minute", 60),
			};

		/// <summary>
		/// 2014.00.07	Bsp Aingaabe:
		/// " 3 hours and 57 minutes "
		/// </summary>
		/// <param name="WartezaitSictString"></param>
		/// <returns></returns>
		static public int? AusDeclineWartezaitSictStringSekundeAnzaal(string WartezaitSictString)
		{
			if (null == WartezaitSictString)
			{
				return null;
			}

			if (null == MissionDeclineDauerMengeAinhaitRegexPatternUndBetraagSekunde)
			{
				return null;
			}

			var DauerListeKomponenteMatch =
				Regex.Matches(WartezaitSictString, MissionDeclineFraigaabeNictDauerKomponenteMitAinhaitRegexPattern);

			int ZwisceergeebnisDauerSekundeAnzaal = 0;

			if (null != DauerListeKomponenteMatch)
			{
				foreach (var KomponenteMatch in DauerListeKomponenteMatch.Cast<Match>())
				{
					var KomponenteBetraagSictString = KomponenteMatch.Groups[1].Value;
					var KomponenteAinhaitSictString = KomponenteMatch.Groups[2].Value;

					if (null == KomponenteAinhaitSictString)
					{
						return null;
					}

					var KomponenteBetraagNulbar = ExtractFromOldAssembly.Bib3.Glob.TryParseInt(KomponenteBetraagSictString);

					if (!KomponenteBetraagNulbar.HasValue)
					{
						return null;
					}

					var MengeAinhaitPasend =
						MissionDeclineDauerMengeAinhaitRegexPatternUndBetraagSekunde
						.Where((KandidaatAinhaitUndBetraagSekunde) =>
						{
							if (null == KandidaatAinhaitUndBetraagSekunde.Key)
							{
								return false;
							}

							var Match = Regex.Match(KomponenteAinhaitSictString, KandidaatAinhaitUndBetraagSekunde.Key, RegexOptions.IgnoreCase);

							return Match.Success;
						}).ToArray();

					if (!(1 == MengeAinhaitPasend.Length))
					{
						return null;
					}

					var KomponenteAinhaitRegexPatternUndBetraagSekunde = MengeAinhaitPasend[0];

					ZwisceergeebnisDauerSekundeAnzaal += KomponenteBetraagNulbar.Value * KomponenteAinhaitRegexPatternUndBetraagSekunde.Value;
				}
			}

			return ZwisceergeebnisDauerSekundeAnzaal;
		}

		static public VonSensor.WindowAgentDialogue AlsWindowAgentDialogue(this MemoryStruct.WindowAgentDialogue WindowAgentDialogue)
		{
			if (null == WindowAgentDialogue)
			{
				return null;
			}

			var LeftHtml = WindowAgentDialogue?.LeftPane?.Html;
			var RightHtml = WindowAgentDialogue?.RightPane?.Html;

			SictAuswertGbsMissionInfoAusHtmlstrJournalOderAgentConv LeftPaneEditAuswert = null;

			if (null != LeftHtml)
			{
				LeftPaneEditAuswert = new SictAuswertGbsMissionInfoAusHtmlstrJournalOderAgentConv(LeftHtml, true);
				LeftPaneEditAuswert.Berecne();
			}

			SictAuswertGbsMissionInfoAusHtmlstrJournalOderAgentConv RightPaneMissionObjectiveAuswert = null;

			if (null != RightHtml)
			{
				RightPaneMissionObjectiveAuswert = new SictAuswertGbsMissionInfoAusHtmlstrJournalOderAgentConv(RightHtml);
				RightPaneMissionObjectiveAuswert.Berecne();
			}

			var AgentLocation = (null == LeftPaneEditAuswert) ? null : LeftPaneEditAuswert.AgentLocation;

			var LeftPaneMissionInfo = (null == LeftPaneEditAuswert) ? null : LeftPaneEditAuswert.Ergeebnis;
			var RightPaneMissionInfo = (null == RightPaneMissionObjectiveAuswert) ? null : RightPaneMissionObjectiveAuswert.Ergeebnis;

			var RightPaneMengeButton = WindowAgentDialogue?.Button?.Select(MemoryStructMap.AlsGbsElementMitBescriftung)?.ToArray();

			VonSensor.WindowAgentMissionInfo ZusamefasungMissionInfo = null;

			if (null == RightPaneMissionInfo)
			{
				ZusamefasungMissionInfo = LeftPaneMissionInfo;
			}
			else
			{
				ZusamefasungMissionInfo = RightPaneMissionInfo.Kopii();

				if (null != LeftPaneMissionInfo)
				{
					//	2015.02.13	ZusamefasungMissionInfo.MissionBriefingText = LeftPaneMissionInfo.MissionBriefingText;
				}
			}

			GbsElementMitBescriftung ButtonRequestMission = null;
			GbsElementMitBescriftung ButtonViewMission = null;
			GbsElementMitBescriftung ButtonAccept = null;
			GbsElementMitBescriftung ButtonDecline = null;
			GbsElementMitBescriftung ButtonComplete = null;
			GbsElementMitBescriftung ButtonQuit = null;
			bool? DeclineOoneStandingLossFraigaabe = null;
			int? DeclineWartezait = null;
			bool? IstOffer = null;
			bool? IstAccepted = null;

			if (null != RightPaneMengeButton)
			{
				ButtonRequestMission =
					RightPaneMengeButton.FirstOrDefault((Kandidaat) => Regex.Match(Kandidaat.Bescriftung ?? "", "request mission", RegexOptions.IgnoreCase).Success);

				ButtonViewMission =
					RightPaneMengeButton.FirstOrDefault((Kandidaat) => Regex.Match(Kandidaat.Bescriftung ?? "", "view mission", RegexOptions.IgnoreCase).Success);

				ButtonAccept =
					RightPaneMengeButton.FirstOrDefault((Kandidaat) => Regex.Match(Kandidaat.Bescriftung ?? "", "accept", RegexOptions.IgnoreCase).Success);

				ButtonDecline =
					RightPaneMengeButton.FirstOrDefault((Kandidaat) => Regex.Match(Kandidaat.Bescriftung ?? "", "decline", RegexOptions.IgnoreCase).Success);

				ButtonComplete =
					RightPaneMengeButton.FirstOrDefault((Kandidaat) => Regex.Match(Kandidaat.Bescriftung ?? "", "complete", RegexOptions.IgnoreCase).Success);

				ButtonQuit =
					RightPaneMengeButton.FirstOrDefault((Kandidaat) => Regex.Match(Kandidaat.Bescriftung ?? "", "quit", RegexOptions.IgnoreCase).Success);
			}

			IstOffer =
				null != ButtonAccept &&
				null != ButtonDecline &&
				null == ButtonComplete &&
				null == ButtonQuit;

			IstAccepted =
				null == ButtonAccept &&
				null == ButtonDecline &&
				(null != ButtonComplete ||
				null != ButtonQuit);

			if (null != LeftPaneEditAuswert)
			{
				var LeftPaneMissionInfoHtmlstr = LeftPaneEditAuswert.Htmlstr;

				if (null != LeftPaneMissionInfoHtmlstr)
				{
					var DeclineWartezaitMatch = Regex.Match(LeftPaneMissionInfoHtmlstr, MissionDeclineFraigaabeNictMitDauerRegexPattern, RegexOptions.IgnoreCase);

					if (DeclineWartezaitMatch.Success)
					{
						DeclineOoneStandingLossFraigaabe = false;

						var DauerSictString = DeclineWartezaitMatch.Groups[1].Value;

						DeclineWartezait = AusDeclineWartezaitSictStringSekundeAnzaal(DauerSictString);
					}
					else
					{
						var DeclineFraigaabeMatch = Regex.Match(LeftPaneMissionInfoHtmlstr, MissionDeclineFraigaabeRegexPattern, RegexOptions.IgnoreCase);

						if (DeclineFraigaabeMatch.Success)
						{
							DeclineOoneStandingLossFraigaabe = true;
						}
					}
				}
			}

			return new VonSensor.WindowAgentDialogue(
				WindowAgentDialogue.AlsWindowAgent(),
				WindowAgentDialogue.AgentName(),
				AgentLocation,
				ZusamefasungMissionInfo,
				ButtonRequestMission,
				ButtonViewMission,
				ButtonAccept,
				ButtonDecline,
				ButtonComplete,
				ButtonQuit,
				IstOffer,
				IstAccepted,
				DeclineOoneStandingLossFraigaabe,
				DeclineWartezait);
		}

	}
}
