using System;
using Bib3;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Optimat.EveOnline;
using Optimat.EveOnline.VonSensor;


namespace Optimat.EveOnline.AuswertGbs
{
	public class SictAuswertGbsMissionLocationAusHtmlNodeUndSiblings
	{
		readonly public HtmlAgilityPack.HtmlNode[] MengeNode;

		public HtmlAgilityPack.HtmlNode SecurityLevelHtmlNode
		{
			private set;
			get;
		}

		public HtmlAgilityPack.HtmlNode LocationLinkHtmlNode
		{
			private set;
			get;
		}

		public string SecurityLevelString
		{
			private set;
			get;
		}

		public int? LocationSecurityLevelMili
		{
			private set;
			get;
		}

		public string LocationName
		{
			private set;
			get;
		}

		public string LocationNameTailSystem
		{
			private set;
			get;
		}

		public MissionLocation Ergeebnis
		{
			private set;
			get;
		}

		public SictAuswertGbsMissionLocationAusHtmlNodeUndSiblings(
			HtmlAgilityPack.HtmlNode[] MengeNode)
		{
			this.MengeNode = MengeNode;
		}

		public void Berecne()
		{
			var MengeNode = this.MengeNode;

			if (null == MengeNode)
			{
				return;
			}

			/*
			 * 2013.11.21 Bsp:
			 <td><font color=#00FF7F>0,8</font>&nbsp;<a href=showinfo:1530//60003985>Oipo II - Moon 19 - Ishukone Watch Logistic Support</a> <font color=#E3170D></font></td>
			 * 
			 * 2014.07.17 Bsp (Security Level Dezimaaltrenzaice "."):
			 <td width=200><font color=#00FFBF>0.9</font>&nbsp;<a href=showinfo:5//30004970//1.54581996781e+12//-2.7761830639e+11//-2.2459826125e+12//3009708//0//dungeon>Renyn</a> <font color=#E3170D></font></td>
			 * 
			 * **/

			foreach (var Node in MengeNode)
			{
				if (null == SecurityLevelHtmlNode)
				{
					if (string.Equals(Node.Name, "font", StringComparison.InvariantCultureIgnoreCase))
					{
						var SecurityLevelMatch = TempAuswertGbs.Extension.SctandardNumberFormatRegex.Match(Node.InnerText ?? "");

						if (SecurityLevelMatch.Success)
						{
							SecurityLevelHtmlNode = Node;

							SecurityLevelString = SecurityLevelMatch.Value;
						}
					}
				}

				if (null == LocationLinkHtmlNode)
				{
					if (string.Equals(Node.Name, "a", StringComparison.InvariantCultureIgnoreCase))
					{
						var Href = Node.GetAttributeValue("href", (string)null);

						if (Regex.Match(Href ?? "", "showinfo", RegexOptions.IgnoreCase).Success)
						{
							LocationLinkHtmlNode = Node;

							LocationName = Node.InnerText;
						}
					}
				}
			}

			if (null == SecurityLevelString || null == LocationName)
			{
				return;
			}

			if (null != LocationName)
			{
				var Location = TempAuswertGbs.Extension.VonStringMissionLocationNameExtraktLocationInfo(LocationName);

				LocationNameTailSystem = (null == Location) ? null : Location.SolarSystemName;
			}

			LocationSecurityLevelMili = (int?)TempAuswertGbs.Extension.SctandardNumberFormatZaalBetraagMili(SecurityLevelString);

			var Ergeebnis = new MissionLocation(LocationName, LocationSecurityLevelMili, LocationNameTailSystem);

			this.Ergeebnis = Ergeebnis;
		}
	}

	public class SictAuswertGbsAgentDialogueMissionObjectiveElementAusHtmlNodeTr
	{
		static public KeyValuePair<string, SictMissionObjectiveObjectiveElementTyp>[] MengeTypTitel =
			new KeyValuePair<string, SictMissionObjectiveObjectiveElementTyp>[]{
				new	KeyValuePair<string,	SictMissionObjectiveObjectiveElementTyp>("Location", SictMissionObjectiveObjectiveElementTyp.Location),
				new	KeyValuePair<string,	SictMissionObjectiveObjectiveElementTyp>("Drop-off Location", SictMissionObjectiveObjectiveElementTyp.LocationDropOff),
				new	KeyValuePair<string,	SictMissionObjectiveObjectiveElementTyp>("Pick-up Location", SictMissionObjectiveObjectiveElementTyp.LocationPickUp),
				new	KeyValuePair<string,	SictMissionObjectiveObjectiveElementTyp>("Item", SictMissionObjectiveObjectiveElementTyp.Item),
				new	KeyValuePair<string,	SictMissionObjectiveObjectiveElementTyp>("Cargo", SictMissionObjectiveObjectiveElementTyp.Cargo),
			};
		/*
		 * 2013.08.07	Bsp:
		 * 
		 <tr valign=middle>
			<td><img src=icon:38_193 size=16></td>
			<td width=32><a href=showinfo:2//1000041><img src="corplogo:1000041" width=32 height=32 hspace=2 vspace=2></a></td>
			<td>Drop-off Location</td>
			<td><font color=#00FF3F>0,7</font>&nbsp;<a href=showinfo:1529//60004168>Sankkasen II - Spacelane Patrol Assembly Plant</a> <font color=#E3170D></font></td>
		</tr>
		 * */

		/*
		 * 2013.11.21	Bsp:
		 * 
        <tr valign=middle>
            <td><img src=icon:38_193 size=16></td>
            <td width=32><a href=showinfo:2//1000038><img src="corplogo:1000038" width=32 height=32 hspace=2 vspace=2></a></td>
            <td>Drop-off Location</td>
            <td><font color=#00FF7F>0,8</font>&nbsp;<a href=showinfo:1530//60003985>Oipo II - Moon 19 - Ishukone Watch Logistic Support</a> <font color=#E3170D></font></td>
        </tr>
		 * */

		/*
		 * 2013.08.07	Bsp:
		 * 
        <tr valign=middle>
            <td><img src=icon:38_195 size=16></td>
            <td width=32><a href=showinfo:3814><img src="typeicon:3814" width=32 height=32 align=left></a></td>
            <td>Item</td>
            <td>1 x Reports (0,1 m&sup3;)</td>
        </tr>
		 * */

		/*
		 * 2013.08.07 Bsp:
		 * 
		 * 1 x Reports (0,1 m&sup3;)
		 * */
		static readonly public string TypItemTailAnzaalUndItemNameRegexPattern = "(\\d+)\\s*" + Regex.Escape("x") + "\\s*([^" + Regex.Escape("(") + "]+)";

		public const string AnnaameIconTypComplete = "38_193";

		readonly public HtmlAgilityPack.HtmlNode HtmlNodeTr;

		public HtmlAgilityPack.HtmlNode HtmlNodeSctaatusIcon
		{
			private set;
			get;
		}

		public HtmlAgilityPack.HtmlNode[] ListeHtmlNodeTd
		{
			private set;
			get;
		}

		public HtmlAgilityPack.HtmlNode HtmlNodeTyp
		{
			private set;
			get;
		}

		/*
		 * 2013.11.26
		 * 
		public HtmlAgilityPack.HtmlNode SecurityLevelHtmlNode
		{
			private set;
			get;
		}

		public string SecurityLevelString
		{
			private set;
			get;
		}

		public string LocationName
		{
			private set;
			get;
		}

		public string LocationNameTailSystem
		{
			private set;
			get;
		}
		 * */

		public HtmlAgilityPack.HtmlNode[] LocationMengeHtmlNode
		{
			private set;
			get;
		}

		public SictAuswertGbsMissionLocationAusHtmlNodeUndSiblings LocationAuswert
		{
			private set;
			get;
		}

		public string SctaatusIconTyp
		{
			private set;
			get;
		}

		public bool? Complete
		{
			private set;
			get;
		}

		public string ZaileLezteInnerText
		{
			private set;
			get;
		}

		public string ItemName
		{
			private set;
			get;
		}

		public SictMissionObjectiveObjectiveElementTyp? ObjectiveTyp
		{
			private set;
			get;
		}

		public WindowAgentMissionObjectiveObjective Ergeebnis
		{
			private set;
			get;
		}

		public SictAuswertGbsAgentDialogueMissionObjectiveElementAusHtmlNodeTr(HtmlAgilityPack.HtmlNode HtmlNodeTr)
		{
			this.HtmlNodeTr = HtmlNodeTr;
		}

		public void Berecne()
		{
			var HtmlNodeTr = this.HtmlNodeTr;

			if (null == HtmlNodeTr)
			{
				return;
			}

			HtmlNodeSctaatusIcon = HtmlNodeTr.SelectSingleNode(".//img[starts-with(@src, 'icon:')]");

			if (null != HtmlNodeSctaatusIcon)
			{
				var SctaatusIconTypMatch = Regex.Match(HtmlNodeSctaatusIcon.GetAttributeValue("src", ""), "icon:([\\d\\w" + Regex.Escape("_") + "]+)", RegexOptions.IgnoreCase);

				if (SctaatusIconTypMatch.Success)
				{
					SctaatusIconTyp = SctaatusIconTypMatch.Groups[1].Value;
				}
			}

			{
				var ListeHtmlNodeTd = HtmlNodeTr.SelectNodes("./td");

				if (null != ListeHtmlNodeTd)
				{
					this.ListeHtmlNodeTd = ListeHtmlNodeTd.ToArray();
				}
			}

			{
				if (null == ListeHtmlNodeTd)
				{
					return;
				}

				if (ListeHtmlNodeTd.Length < 2)
				{
					return;
				}

				HtmlNodeTyp =
					ListeHtmlNodeTd.Reversed().ElementAtOrDefault(1);

				var ZaileLezteNode = ListeHtmlNodeTd.LastOrDefault();

				ZaileLezteInnerText = (null == ZaileLezteNode) ? null : ZaileLezteNode.InnerText;

				Complete = string.Equals(SctaatusIconTyp, AnnaameIconTypComplete, StringComparison.InvariantCultureIgnoreCase);

				string ObjectiveTypString = null;

				if (null != HtmlNodeTyp)
				{
					ObjectiveTypString = HtmlNodeTyp.InnerText.Trim();
				}

				ObjectiveTyp = MengeTypTitel.FirstOrDefault((Kandidaat) => string.Equals(Kandidaat.Key, ObjectiveTypString, StringComparison.InvariantCultureIgnoreCase)).Value;

				if (SictMissionObjectiveObjectiveElementTyp.Item == ObjectiveTyp &&
					null != ZaileLezteInnerText)
				{
					var Match = Regex.Match(ZaileLezteInnerText, TypItemTailAnzaalUndItemNameRegexPattern, RegexOptions.IgnoreCase);

					if (Match.Success)
					{
						ItemName = ExtractFromOldAssembly.Bib3.Glob.TrimNullable(Match.Groups[2].Value);
					}
				}

				if (SictMissionObjectiveObjectiveElementTyp.Location == ObjectiveTyp ||
					SictMissionObjectiveObjectiveElementTyp.LocationDropOff == ObjectiveTyp ||
					SictMissionObjectiveObjectiveElementTyp.LocationPickUp == ObjectiveTyp)
				{
					if (null != ZaileLezteNode)
					{
						LocationMengeHtmlNode = ZaileLezteNode.ChildNodes.ToArray();

						LocationAuswert = new SictAuswertGbsMissionLocationAusHtmlNodeUndSiblings(LocationMengeHtmlNode);
						LocationAuswert.Berecne();
					}
				}

				var Location = (null == LocationAuswert) ? null : LocationAuswert.Ergeebnis;

				int? SecurityLevelMinimumMili = null;

				if (null != Location)
				{
					SecurityLevelMinimumMili = Location.LocationSecurityLevelMili	?? 0;
				}

				var Ergeebnis = new WindowAgentMissionObjectiveObjective(
					Complete,
					ObjectiveTyp,
					Location,
					ItemName,
					null,
					//	2015.02.20	ObjectiveTypString,
					AgregatioonComplete: Complete,
					SecurityLevelMinimumMili: SecurityLevelMinimumMili);

				this.Ergeebnis = Ergeebnis;
			}
		}
	}

	public class SictAuswertGbsAgentDialogueMissionObjectiveAusHtmlNodeCaption
	{
		/*
		 * 2013.08.07	Bsp:
		 * 
		 <span id=caption>Bring Item Objective</span><br>
            <div id=basetext>Acquire these goods:<br><br>
            <table>
                <tr valign=middle>
                    <td><img src=icon:38_193 size=16></td>
                    <td width=32><a href=showinfo:2//1000041><img src="corplogo:1000041" width=32 height=32 hspace=2 vspace=2></a></td>
                    <td>Drop-off Location</td>
                    <td><font color=#00FF3F>0,7</font>&nbsp;<a href=showinfo:1529//60004168>Sankkasen II - Spacelane Patrol Assembly Plant</a> <font color=#E3170D></font></td>
                </tr>
                <tr valign=middle>
                    <td><img src=icon:38_195 size=16></td>
                    <td width=32><a href=showinfo:3814><img src="typeicon:3814" width=32 height=32 align=left></a></td>
                    <td>Item</td>
                    <td>1 x Reports (0,1 m&sup3;)</td>
                </tr>
            </table>
            </div>
            <br>
		 * */

		readonly public HtmlAgilityPack.HtmlNode HtmlNodeCaption;

		public HtmlAgilityPack.HtmlNode HtmlNodeDiv
		{
			private set;
			get;
		}

		public HtmlAgilityPack.HtmlNode HtmlNodeTable
		{
			private set;
			get;
		}

		public HtmlAgilityPack.HtmlNode[] MengeKandidaatElementHtmlNode
		{
			private set;
			get;
		}

		public SictAuswertGbsAgentDialogueMissionObjectiveElementAusHtmlNodeTr[] MengeKandidaatElementAuswert
		{
			private set;
			get;
		}

		public WindowAgentMissionObjectiveObjective Ergeebnis
		{
			private set;
			get;
		}

		public SictAuswertGbsAgentDialogueMissionObjectiveAusHtmlNodeCaption(HtmlAgilityPack.HtmlNode HtmlNodeCaption)
		{
			this.HtmlNodeCaption = HtmlNodeCaption;
		}

		public void Berecne()
		{
			var HtmlNodeCaption = this.HtmlNodeCaption;

			if (null == HtmlNodeCaption)
			{
				return;
			}

			HtmlNodeDiv = Optimat.Glob.ListeSiblingFolgendFrüheste(
				HtmlNodeCaption, (Kandidaat) => string.Equals(Kandidaat.Name, "div", StringComparison.InvariantCultureIgnoreCase));

			if (null == HtmlNodeDiv)
			{
				return;
			}

			HtmlNodeTable = HtmlNodeDiv.SelectSingleNode(".//table");

			if (null == HtmlNodeTable)
			{
				return;
			}

			var MengeKandidaatElementHtmlNodeCollection = HtmlNodeTable.SelectNodes("./tr").ToArray();

			if (null == MengeKandidaatElementHtmlNodeCollection)
			{
				return;
			}

			MengeKandidaatElementHtmlNode = MengeKandidaatElementHtmlNodeCollection.ToArray();

			MengeKandidaatElementAuswert =
				MengeKandidaatElementHtmlNode
				.Select((Node) =>
					{
						var Auswert = new SictAuswertGbsAgentDialogueMissionObjectiveElementAusHtmlNodeTr(Node);
						Auswert.Berecne();
						return Auswert;
					}).ToArray();

			var MengeObjective =
				MengeKandidaatElementAuswert
				.Select((Auswert) => Auswert.Ergeebnis)
				.ToArray();

			var Complete =
				Optimat.EveOnline.Extension.MissionObjectiveCompleteBerecne(MengeObjective);

			var SecurityLevelMinimumMili =
				Optimat.EveOnline.Extension.SecurityLevelMinimumMiliBerecne(MengeObjective);

			var Ergeebnis = new WindowAgentMissionObjectiveObjective(
				MengeObjective: MengeObjective,
				AgregatioonComplete: Complete,
				SecurityLevelMinimumMili: SecurityLevelMinimumMili);

			this.Ergeebnis = Ergeebnis;
		}
	}

	/// <summary>
	/// Auswertung der Info aus dem Htmlstr aus AgentConversation "rightPane" oder Mission Journal (Window Typ "AgentBrowser") (von "Read Details")
	/// </summary>
	public class SictAuswertGbsMissionInfoAusHtmlstrJournalOderAgentConv
	{
		readonly public bool VarianteAgentConvLeftPane;

		readonly public string Htmlstr;

		public MissionLocation AgentLocation
		{
			private set;
			get;
		}

		public HtmlAgilityPack.HtmlNode HtmlstrSictHtmlNode
		{
			private set;
			get;
		}

		public HtmlAgilityPack.HtmlNode HtmlHtmlNode
		{
			private set;
			get;
		}

		public HtmlAgilityPack.HtmlNode AgentConvPortraitImgHtmlNode
		{
			private set;
			get;
		}

		public HtmlAgilityPack.HtmlNode AgentConvPortraitTdHtmlNode
		{
			private set;
			get;
		}

		public HtmlAgilityPack.HtmlNode AgentConvLocationTdHtmlNode
		{
			private set;
			get;
		}

		public HtmlAgilityPack.HtmlNode AgentConvAgentNameHtmlNode
		{
			private set;
			get;
		}

		public HtmlAgilityPack.HtmlNode[] AgentConvLocationMengeHtmlNode
		{
			private set;
			get;
		}

		public HtmlAgilityPack.HtmlNode AgentConvDivisionHtmlNode
		{
			private set;
			get;
		}

		public HtmlAgilityPack.HtmlNode HtmlNodeMissionBriefingTitel
		{
			private set;
			get;
		}

		public HtmlAgilityPack.HtmlNode HtmlNodeMissionBriefingText
		{
			private set;
			get;
		}

		public HtmlAgilityPack.HtmlNode HtmlNodeTitelObjective
		{
			private set;
			get;
		}

		public HtmlAgilityPack.HtmlNode HtmlNodeTitelReward
		{
			private set;
			get;
		}

		public HtmlAgilityPack.HtmlNode HtmlNodeTitelBonusReward
		{
			private set;
			get;
		}

		public HtmlAgilityPack.HtmlNode[] MengeObjectiveKandidaatCaptionHtmlNode
		{
			private set;
			get;
		}

		public SictAuswertGbsAgentDialogueMissionObjectiveAusHtmlNodeCaption[] MengeObjectiveAuswert
		{
			private set;
			get;
		}

		public string TitelObjectiveText
		{
			private set;
			get;
		}

		public string TitelObjectiveTextTailNaacObjective
		{
			private set;
			get;
		}

		public string MissionTitel
		{
			private set;
			get;
		}

		public bool? Complete
		{
			private set;
			get;
		}

		public string MissionBriefingText
		{
			private set;
			get;
		}

		public string RewardIskBetraagString
		{
			private set;
			get;
		}

		public string RewardLpBetraagString
		{
			private set;
			get;
		}

		public string BonusRewardIskBetraagString
		{
			private set;
			get;
		}

		public WindowAgentMissionInfo Ergeebnis
		{
			private set;
			get;
		}

		/// <summary>
		/// 2013.08.00 Bsp:
		/// "<span id=subheader><font>Gone Berserk Objectives</font></span><br>"
		/// 
		/// 2013.08.07 naac Interpretation Html Bsp:
		/// "Eliminate the Pirate Campers Objectives"
		/// 
		/// 2014.01.26	Bsp:
		/// "After The Seven (1 of 5) \"Replacement\" Objectives"
		/// </summary>
		//	static string TitelObjectiveRegexPattern = "([\\-\\s\\d\\w" + Regex.Escape("(/)'\"#?.") + "]+)" + Regex.Escape("Objective");
		static string TitelObjectiveRegexPattern = "([" + TempAuswertGbs.Extension.MissionTitelRegexMengeCharClass + "]+)" + Regex.Escape("Objective");

		static string RewardBetraagRegexPattern = "[\\d\\.\\,\\s]+";

		/// <summary>
		/// 2015.01.02	Bsp:
		/// "1 530 000 ISK"
		/// </summary>
		static string RewardIskRegexPattern = "(" + RewardBetraagRegexPattern + ")\\s*" + Regex.Escape("ISK");
		static string RewardLpRegexPattern = "(" + RewardBetraagRegexPattern + ")\\s* (L|Loyalty\\s*)P";

		/// <summary>
		/// 
		/// </summary>
		/// <param name="Htmlstr"></param>
		/// <param name="VarianteAgentConvLeftPane">Htmlstr komt aus AgentDialogue LeftPane (andere Struktur)</param>
		public SictAuswertGbsMissionInfoAusHtmlstrJournalOderAgentConv(
			string Htmlstr,
			bool VarianteAgentConvLeftPane = false)
		{
			this.Htmlstr = Htmlstr;
			this.VarianteAgentConvLeftPane = VarianteAgentConvLeftPane;
		}

		/// <summary>
		/// Regex.Escape("factionlogo")	+ "\\s*\\:\\s*" + Regex.Escape(FürFactionLogoBezaicer.ToString());
		/// </summary>
		static readonly Regex FactionLogoRegex = new Regex(@"factionlogo\s*\:\s*(\d+)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		static SictFactionSictEnum[] MengeFactionBerecneAusHtml(string Html)
		{
			if (null == Html)
			{
				return null;
			}

			var MengeFactionLogoMatch =
				FactionLogoRegex.Matches(Html);

			if (null == MengeFactionLogoMatch)
			{
				return null;
			}

			var MengeFaction = new List<SictFactionSictEnum>();

			foreach (var FactionLogoMatch in MengeFactionLogoMatch.OfType<Match>())
			{
				var FactionLogoId = FactionLogoMatch.Groups[1].Value.TryParseInt64();

				if (!FactionLogoId.HasValue)
				{
					continue;
				}

				var Faction = WorldConfig.AusFactionLogoIdBerecneFaction(FactionLogoId.Value);

				if (!Faction.HasValue)
				{
					continue;
				}

				MengeFaction.Add(Faction.Value);
			}

			return MengeFaction.ToArray();
		}

		public void Berecne()
		{
			if (null == Htmlstr)
			{
				return;
			}

			HtmlstrSictHtmlNode = HtmlAgilityPack.HtmlNode.CreateNode(Htmlstr);

			if (null == HtmlstrSictHtmlNode)
			{
				return;
			}

			HtmlHtmlNode = HtmlstrSictHtmlNode.SelectSingleNode("/html");

			if (null == HtmlHtmlNode)
			{
				return;
			}

			var ListeNodeSpanSuuce = HtmlHtmlNode.SelectNodes("//span");

			/*
			 * 2014.00.09	Beobactung:
			 * In mance Variante kome kaine span vor.
			 * 
			if(null	== ListeNodeSpanSuuce)
			{
				return;
			}
			 * */

			var ListeNodeSpan = (null == ListeNodeSpanSuuce) ? null : ListeNodeSpanSuuce.ToArray();

			HtmlNodeMissionBriefingTitel =
				(null == ListeNodeSpan) ? null :
				ListeNodeSpan.FirstOrDefault((Kandidaat) => Regex.Match(Kandidaat.InnerText ?? "", "Mission briefing", RegexOptions.IgnoreCase).Success);

			if (null != HtmlNodeMissionBriefingTitel)
			{
				HtmlNodeMissionBriefingText = Optimat.Glob.SelectSingleNodeNaacNode(
					HtmlHtmlNode,
					"//div",
					HtmlNodeMissionBriefingTitel);
			}

			if (VarianteAgentConvLeftPane)
			{
				//	Hiir mus noc geändert were, diise Annaame füürt dazu das der MissionBriefing Text anders ausfält als aus dem AgentBrowser Window
				HtmlNodeMissionBriefingText = HtmlHtmlNode.SelectSingleNode("//body");

				AgentConvPortraitImgHtmlNode = HtmlHtmlNode.SelectSingleNode("//img[starts-with(@src, 'portrait')]");

				if (null != AgentConvPortraitImgHtmlNode)
				{
					AgentConvPortraitTdHtmlNode = AgentConvPortraitImgHtmlNode.ParentNode;
				}

				if (null != AgentConvPortraitTdHtmlNode)
				{
					AgentConvLocationTdHtmlNode = Optimat.Glob.SelectSingleNodeNaacNode(AgentConvPortraitTdHtmlNode.ParentNode, "/td", AgentConvPortraitTdHtmlNode);
				}

				if (null != AgentConvLocationTdHtmlNode)
				{
					AgentConvAgentNameHtmlNode = Optimat.Glob.SelectSingleNodeNaacNodeMitZuusazFilter(
						AgentConvLocationTdHtmlNode,
						"/font", null, (KandidaatNode) => (KandidaatNode.GetAttributeValue("color", "") ?? "").Length < 4);
				}

				if (null != AgentConvAgentNameHtmlNode)
				{
					var AgentConvAgentShowInfoHtmlNode =
						Optimat.Glob.SelectSingleNodeNaacNodeMitZuusazFilter(
						AgentConvLocationTdHtmlNode, "/a", AgentConvAgentNameHtmlNode,
						(KandidaatNode) => !Optimat.Glob.SelectNodesPerXPath(KandidaatNode, "/img").IsNullOrEmpty());

					AgentConvLocationMengeHtmlNode =
						Optimat.Glob.SelectNodesNaacNode(AgentConvLocationTdHtmlNode, "/*", AgentConvAgentShowInfoHtmlNode);
				}

				if (null != AgentConvLocationMengeHtmlNode)
				{
					var AgentLocationAuswert = new SictAuswertGbsMissionLocationAusHtmlNodeUndSiblings(AgentConvLocationMengeHtmlNode);
					AgentLocationAuswert.Berecne();

					AgentLocation = AgentLocationAuswert.Ergeebnis;
				}
			}

			if (null != HtmlNodeMissionBriefingText)
			{
				MissionBriefingText = HtmlNodeMissionBriefingText.InnerText;
			}

			{
				/*
				 * 2013.09.01
				 * Ersaz Suuce HtmlNodeTitelObjective:
				 * Suuce sol sowool fer HtmlStr aus Agent Conversation als auc HtmlStr aus Read Details funktioniire:
				 * 
				 * 2013.09.01 Bsp aus Agent Conversation:
				 * "<span id=subheader><font>The Missing Convoy Objectives</font></span><br>"
				 * 
				 * 2013.09.01 Bsp aus Read Details:
				 * "<span id=Span2><font>The Missing Convoy Objectives</font></span><br>"
				 * 
				HtmlNodeTitelObjective = HtmlstrSictHtmlNode.SelectSingleNode("//span[@id='subheader']");

				if (null != HtmlNodeTitelObjective)
				{
					TitelObjectiveText = HtmlNodeTitelObjective.InnerText;
				}

				 * */

				if (null != ListeNodeSpan)
				{
					foreach (var NodeSpan in ListeNodeSpan)
					{
						var NodeSpanInnerText = NodeSpan.InnerText;

						if (null == NodeSpanInnerText)
						{
							continue;
						}

						var TitelObjectiveMatch = Regex.Match(NodeSpanInnerText, TitelObjectiveRegexPattern);

						if (!TitelObjectiveMatch.Success)
						{
							continue;
						}

						HtmlNodeTitelObjective = NodeSpan;

						TitelObjectiveText = NodeSpanInnerText;

						MissionTitel = TitelObjectiveMatch.Groups[1].Value.Trim();

						TitelObjectiveTextTailNaacObjective = TitelObjectiveText.Substring(TitelObjectiveMatch.Index + TitelObjectiveMatch.Length);

						Complete = Regex.Match(TitelObjectiveTextTailNaacObjective, "complete", RegexOptions.IgnoreCase).Success;

						break;
					}
				}
			}

			if (null != TitelObjectiveText)
			{
				MengeObjectiveKandidaatCaptionHtmlNode =
					Optimat.Glob.SelectNodesNaacNode(
					HtmlHtmlNode,
					"//span[@id='caption' and contains(text(), 'Objective')]",
					HtmlNodeTitelObjective);

				if (null != MengeObjectiveKandidaatCaptionHtmlNode)
				{
					MengeObjectiveAuswert =
						MengeObjectiveKandidaatCaptionHtmlNode
						.Select((Node) =>
							{
								var Auswert = new SictAuswertGbsAgentDialogueMissionObjectiveAusHtmlNodeCaption(Node);
								Auswert.Berecne();
								return Auswert;
							}).ToArray();
				}
			}

			if (null != HtmlNodeTitelObjective)
			{
				HtmlNodeTitelReward =
					Optimat.Glob.SelectSingleNodeNaacNodeMitZuusazFilter(
					HtmlHtmlNode,
					"//span",
					HtmlNodeTitelObjective,
					(Node) => Regex.Match(Node.InnerText, "Rewards", RegexOptions.IgnoreCase).Success);

				if (null != HtmlNodeTitelReward)
				{
					var RewardIskBetraagStringNodeUndMatch =
						Optimat.Glob.SelectSingleNodeNaacNodeMitInnerTextRegexPattern(
						HtmlHtmlNode,
						"//td",
						HtmlNodeTitelReward,
						RewardIskRegexPattern,
						RegexOptions.IgnoreCase);

					var RewardLpBetraagStringNodeUndMatch =
						Optimat.Glob.SelectSingleNodeNaacNodeMitInnerTextRegexPattern(
						HtmlHtmlNode,
						"//td",
						HtmlNodeTitelReward,
						RewardLpRegexPattern,
						RegexOptions.IgnoreCase);

					if (null != RewardIskBetraagStringNodeUndMatch.Value)
					{
						RewardIskBetraagString = RewardIskBetraagStringNodeUndMatch.Value.Groups[1].Value;
					}

					if (null != RewardLpBetraagStringNodeUndMatch.Value)
					{
						RewardLpBetraagString = RewardLpBetraagStringNodeUndMatch.Value.Groups[1].Value;
					}

					HtmlNodeTitelBonusReward =
						Optimat.Glob.SelectSingleNodeNaacNodeMitZuusazFilter(
						HtmlHtmlNode,
						"//span",
						HtmlNodeTitelObjective,
						(Node) => Regex.Match(Node.InnerText, "Bonus.*Rewards", RegexOptions.IgnoreCase).Success);

					if (null != HtmlNodeTitelBonusReward)
					{
						var BonusRewardIskBetraagStringNodeUndMatch =
							Optimat.Glob.SelectSingleNodeNaacNodeMitInnerTextRegexPattern(
							HtmlHtmlNode,
							"//td",
							HtmlNodeTitelBonusReward,
							RewardIskRegexPattern,
							RegexOptions.IgnoreCase);

						if (null != BonusRewardIskBetraagStringNodeUndMatch.Value)
						{
							BonusRewardIskBetraagString = BonusRewardIskBetraagStringNodeUndMatch.Value.Groups[1].Value;
						}
					}
				}
			}

			/*
			 * 2015.02.07
			 * 
			Ergeebnis.Htmlstr = Htmlstr;

			Ergeebnis.MissionTitel = MissionTitel;
			Ergeebnis.Complete = Complete;

			Ergeebnis.RewardIskAnzaal = (int?)AuswertGbs.Glob.SctandardNumberFormatZaalBetraag(RewardIskBetraagString);
			Ergeebnis.RewardLpAnzaal = (int?)AuswertGbs.Glob.SctandardNumberFormatZaalBetraag(RewardLpBetraagString);
			Ergeebnis.BonusRewardIskAnzaal	 = (int?)AuswertGbs.Glob.SctandardNumberFormatZaalBetraag(BonusRewardIskBetraagString);
			 * */

			var RewardIskAnzaal = (int?)TempAuswertGbs.Extension.SctandardNumberFormatZaalBetraag(RewardIskBetraagString);
			var RewardLpAnzaal = (int?)TempAuswertGbs.Extension.SctandardNumberFormatZaalBetraag(RewardLpBetraagString);
			var BonusRewardIskAnzaal = (int?)TempAuswertGbs.Extension.SctandardNumberFormatZaalBetraag(BonusRewardIskBetraagString);

			if (RewardIskAnzaal.HasValue)
			{
				if (!BonusRewardIskAnzaal.HasValue)
				{
					BonusRewardIskAnzaal = 0;
				}

				if (!RewardLpAnzaal.HasValue)
				{
					RewardLpAnzaal = 0;
				}
			}

			WindowAgentMissionObjectiveObjective Objective = null;

			if (null != MengeObjectiveAuswert)
			{
				var MengeObjective = MengeObjectiveAuswert.Select((Auswert) => Auswert.Ergeebnis).ToArray();

				var SecurityLevelMinimumMili =
					Optimat.EveOnline.Extension.SecurityLevelMinimumMiliBerecne(MengeObjective);

				Objective =
					new WindowAgentMissionObjectiveObjective(
						MengeObjective: MengeObjective,
						AgregatioonComplete: Optimat.EveOnline.Extension.MissionObjectiveCompleteBerecne(MengeObjective, Complete),
						SecurityLevelMinimumMili: SecurityLevelMinimumMili);
			}

			var MengeFaction = MengeFactionBerecneAusHtml(Htmlstr);

			var Ergeebnis = new WindowAgentMissionInfo(
				MissionTitel,
				Objective,
				MengeFaction,
				Complete,
				RewardIskAnzaal,
				RewardLpAnzaal,
				BonusRewardIskAnzaal
				//	2015.02.13	,MissionBriefingText
				);

			this.Ergeebnis = Ergeebnis;
		}
	}
}
