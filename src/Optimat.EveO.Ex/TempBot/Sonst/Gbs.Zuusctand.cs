using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Optimat.EveOnline.Anwendung;
using Optimat.ScpezEveOnln;
using Bib3;
using ExtractFromOldAssembly.Bib3;
//using Optimat.EveOnline.AuswertGbs;

namespace Optimat.EveOnline.Anwendung
{
	[JsonObject(MemberSerialization.OptIn)]
	public partial class SictGbsZuusctand
	{
		Int64 NaacNuzerMeldungIdent;

		[JsonProperty]
		public List<SictVerlaufBeginUndEndeRef<SictMessageStringAuswert>> ListeAbovemainMessageAuswertMitZait
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictVerlaufBeginUndEndeRef<SictMessageStringAuswert>	ListeAbovemainMessageDronesLezteAuswertMitZait
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? ListeAbovemainMessageDronesLezteAlter
		{
			private set;
			get;
		}

		[JsonProperty]
		readonly public Queue<SictGbsMenuKaskaadeZuusctand> ListeMenuKaskaade = new Queue<SictGbsMenuKaskaadeZuusctand>();

		[JsonProperty]
		readonly public List<SictNaacNuzerMeldungZuEveOnline> MengeNaacNuzerMeldung = new List<SictNaacNuzerMeldungZuEveOnline>();

		public	SictGbsMenuKaskaadeZuusctand	MenuKaskaadeLezteNocOfe
		{
			get
			{
				var MenuKaskaadeLezte = this.MenuKaskaadeLezte;

				if (null == MenuKaskaadeLezte)
				{
					return null;
				}

				if (MenuKaskaadeLezte.EndeZait.HasValue)
				{
					return null;
				}

				return MenuKaskaadeLezte;
			}
		}

		public	SictGbsMenuKaskaadeZuusctand	MenuKaskaadeLezte
		{
			get
			{
				return ExtractFromOldAssembly.Bib3.Extension.LastOrDefaultNullable(ListeMenuKaskaade);
			}
		}

		[JsonProperty]
		public SictWertMitZait<SictGbsMenuZuusctand> AusButtonListSurroundingsMenuLezteMitBeginZait
		{
			private set;
			get;
		}

		[JsonProperty]
		public List<SictGbsWindowZuusctand> MengeWindow
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictVerlaufBeginUndEndeRef<SictMessageStringAuswert> AbovemainMessageClusterShutdownLezte
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? InfoPanelLocationInfoSictbarLezteZaitMili
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? InfoPanelRouteSictbarLezteZaitMili
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? InfoPanelMissionsSictbarLezteZaitMili
		{
			private set;
			get;
		}

		[JsonProperty]
		public KeyValuePair<int, int>? NeocomClockZaitModuloTaagMinMax
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictGbsAgregatioonAusTransitionInfo AgregatioonAusTransitionInfo
		{
			private set;
			get;
		}

		public SictGbsMenuZuusctand[] ListeMenuNocOfeBerecne()
		{
			var MenuKaskaade = this.MenuKaskaadeLezte;

			if (null == MenuKaskaade)
			{
				return null;
			}

			if (MenuKaskaade.EndeZait.HasValue)
			{
				return null;
			}

			var MenuKaskaadeListeMenu = MenuKaskaade.ListeMenu;

			if (null == MenuKaskaadeListeMenu)
			{
				return null;
			}

			return MenuKaskaadeListeMenu.ToArrayNullable();
		}

		public SictGbsWindowZuusctand ZuHerkunftAdreseWindow(Int64 HerkunftAdrese)
		{
			var MengeWindow = this.MengeWindow;

			if (null == MengeWindow)
			{
				return null;
			}

			return MengeWindow.FirstOrDefault((Kandidaat) => Kandidaat.GbsAstHerkunftAdrese == HerkunftAdrese);
		}

		public SictGbsWindowZuusctand WindowOverView()
		{
			return	MengeWindow
				.FirstOrDefaultNullable((Kandidaat) => Kandidaat.AingangScnapscusTailObjektIdentLezteBerecne() is VonSensor.WindowOverView);
		}

		public SictGbsWindowZuusctand WindowSurveyScanView()
		{
			return	MengeWindow
				.FirstOrDefaultNullable((Kandidaat) => Kandidaat.AingangScnapscusTailObjektIdentLezteBerecne() is VonSensor.WindowSurveyScanView);
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictMessageStringAuswert
	{
		[JsonProperty]
		readonly public string MessageString;

		[JsonProperty]
		public Int64? DroneCommandRange
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? DroneControlCountScrankeMax
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? DroneBandwithAvailableMili
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? ClusterShutdownZaitDistanz
		{
			private set;
			get;
		}

		[JsonProperty]
		public int? MengeTargetAnzaalSkillScrankeMax
		{
			private set;
			get;
		}

		/// <summary>
		/// 2014.01.19	Beobactung Bsp:
		/// "Cluster Shutdown in 3 minutes and 37 seconds"
		/// "Cluster Shutdown in 1 minute and 56 seconds"
		/// </summary>
		static readonly string MessageClusterShutdownRegexPattern = "Cluster Shutdown in (\\d+) minute[\\w]* and (\\d+) second[\\w]*";

		/// <summary>
		/// 2014.10.23	Bsp:
		/// "<center>You are already managing 2 targets, as many as you have skill to."
		/// </summary>
		static readonly string[] MengeTargetAnzaalSkillScrankeMaxRegexPattern = new string[]{
			@"already\s*managing\s*("+ TempAuswertGbs.Extension.SctandardZaalRegexPattern + @")\s*targets.*skill"};

		/// <summary>
		/// 2014.01.01	Beobactung Bsp:
		/// "The drones fail to execute your commands as the target Deadspace Guristas Cruiser is not within your 57000.0 m drone command range."
		/// </summary>
		static readonly string[] MengeMessageDroneCommandRangeRegexPattern = new string[]{
			"your\\s*("+ TempAuswertGbs.Extension.DistanceRegexPattern + ")\\s*drone command range"};

		/// <summary>
		/// B:\Berict\Berict.Nuzer\[ZAK=2015.00.15.14.12.44,NB=11].Anwendung.Berict:
		/// "<center>You don't have enough bandwidth to launch Hammerhead II. You need 10.0 Mbit/s but only have 0.0 Mbit/s available."
		/// </summary>
		static readonly string[] MengeMessageDroneBandwithAvailableRegexPattern = new string[]{
			@"only[\w\s]*\s+("+ TempAuswertGbs.Extension.SctandardZaalRegexPattern + @")\s*Mbit/s\s*available"};

		/// <summary>
		/// 2014.04.24	Beobactung aus T:\Günta\Projekt\Optimat.EveOnline\Markt\von Nuzer Berict\2014.04.24.10 Test durc curyl\Auswert\Temp von Server Berict\[ZAK=2014.04.24.10.06.51,NB=8].Anwendung.Berict:
		/// "You cannot launch Hobgoblin I because you are already controlling 3 drones, as much as you have skill to."
		/// </summary>
		static readonly string[] MengeMessageDroneControlCountScrankeMaxRegexPattern = new string[]{
			"not launch.*already control.*(\\d+)\\s*drone"};

		public SictMessageStringAuswert()
		{
		}

		public SictMessageStringAuswert(
			string MessageString)
		{
			this.MessageString = MessageString;
		}

		static Int64? ZaitBetraagSekundeAusPatternMitMinuteInGrupe1UndSekundeInGrupe2(
			string StringAuszuwerte,
			string	RegexPattern,
			RegexOptions RegexOptions = RegexOptions.None)
		{
			if (null == StringAuszuwerte)
			{
				return null;
			}

			if (null == RegexPattern)
			{
				return null;
			}

			var Match = Regex.Match(StringAuszuwerte, RegexPattern, RegexOptions);

			if (!Match.Success)
			{
				return null;
			}

			var MinuteAnzaalSictString = Match.Groups[1].Value;
			var SekundeAnzaalSictString = Match.Groups[1].Value;

			var MinuteAnzaal = ExtractFromOldAssembly.Bib3.Glob.TryParseInt(MinuteAnzaalSictString);
			var SekundeAnzaal = ExtractFromOldAssembly.Bib3.Glob.TryParseInt(SekundeAnzaalSictString);

			return MinuteAnzaal * 60 + SekundeAnzaal;
		}

		static Int64? ZaalMiliAusMengeRegexPatternMitDistanceInGrupe1Match(
			string	StringAuszuwerte,
			IEnumerable<string>	MengeRegexPatternMitDistanceInGrupe1,
			RegexOptions RegexOptions = RegexOptions.None)
		{
			return ZaalMiliAusMengeRegexPatternMitZaalInGrupeMatch(
				StringAuszuwerte,
				(null == MengeRegexPatternMitDistanceInGrupe1) ? null :
				MengeRegexPatternMitDistanceInGrupe1
				.Select((RegexPatternMitDistanceInGrupe1) => new KeyValuePair<string, int>(RegexPatternMitDistanceInGrupe1, 1)));
		}

		static Int64? ZaalMiliAusMengeRegexPatternMitZaalInGrupeMatch(
			string StringAuszuwerte,
			IEnumerable<KeyValuePair<string, int>> MengeRegexPatternUndGroupMitDistanceIndex,
			RegexOptions RegexOptions = RegexOptions.None)
		{
			return ZaalMiliAusMengeRegexPatternMitZaalInGrupeMatch(
				StringAuszuwerte,
				MengeRegexPatternUndGroupMitDistanceIndex,
				TempAuswertGbs.Extension.SctandardNumberFormatZaalBetraagMili,
				RegexOptions);
		}

		static Int64? ZaalMiliAusMengeRegexPatternMitZaalInGrupeMatch(
			string	StringAuszuwerte,
			IEnumerable<KeyValuePair<string,	int>>	MengeRegexPatternUndGroupMitDistanceIndex,
			Func<string,	Int64?>	CallbackZaalMiliBerecneAusGrupeString,
			RegexOptions RegexOptions	= RegexOptions.None)
		{
			if (null == StringAuszuwerte || null == MengeRegexPatternUndGroupMitDistanceIndex)
			{
				return null;
			}

			if (null == CallbackZaalMiliBerecneAusGrupeString)
			{
				return null;
			}

			foreach (var RegexPatternUndGroupMitDistanceIndex in MengeRegexPatternUndGroupMitDistanceIndex)
			{
				if (null == RegexPatternUndGroupMitDistanceIndex.Key)
				{
					continue;
				}

				var Match = Regex.Match(StringAuszuwerte, RegexPatternUndGroupMitDistanceIndex.Key, RegexOptions);

				if (!Match.Success)
				{
					continue;
				}

				var	Group	= Match.Groups.OfType<Group>().ElementAtOrDefault(RegexPatternUndGroupMitDistanceIndex.Value);

				if (null == Group)
				{
					continue;
				}

				var ZaalSictString = Group.Value;

				var ZaalMili = CallbackZaalMiliBerecneAusGrupeString(ZaalSictString);

				if (!ZaalMili.HasValue)
				{
					continue;
				}

				return ZaalMili;
			}

			return null;
		}

		static Int64? DistanceDurcMeeterAusMengeRegexPatternMitDistanceInGrupeMatch(
			string	StringAuszuwerte,
			IEnumerable<KeyValuePair<string,	int>>	MengeRegexPatternUndGroupMitDistanceIndex,
			RegexOptions RegexOptions	= RegexOptions.None)
		{
			return ZaalMiliAusMengeRegexPatternMitZaalInGrupeMatch(
				StringAuszuwerte,
				MengeRegexPatternUndGroupMitDistanceIndex,
				TempAuswertGbs.Extension.AusDistanceSictStringAbbildGanzzaalDurcMeeterScrankeMin,
				RegexOptions);
		}

		static Int64? DistanceDurcMeeterAusMengeRegexPatternMitDistanceInGrupe1Match(
			string StringAuszuwerte,
			IEnumerable<string> MengeRegexPatternMitDistanceInGrupe1,
			RegexOptions RegexOptions = RegexOptions.None)
		{
			return DistanceDurcMeeterAusMengeRegexPatternMitDistanceInGrupeMatch(
				StringAuszuwerte,
				(null == MengeRegexPatternMitDistanceInGrupe1) ? null :
				MengeRegexPatternMitDistanceInGrupe1
				.Select((RegexPatternMitDistanceInGrupe1) => new KeyValuePair<string, int>(RegexPatternMitDistanceInGrupe1, 1)));
		}

		public void Berecne()
		{
			Int64? ClusterShutdownZaitDistanz = null;
			int? MengeTargetAnzaalSkillScrankeMax = null;
			Int64? DroneCommandRange = null;
			Int64? DroneControlCountScrankeMax = null;
			Int64? DroneBandwithAvailableMili	= null;

			try
			{
				var	MessageString	= this.MessageString;

				ClusterShutdownZaitDistanz =
					ZaitBetraagSekundeAusPatternMitMinuteInGrupe1UndSekundeInGrupe2(
					MessageString,
					MessageClusterShutdownRegexPattern,
					RegexOptions.IgnoreCase);

				MengeTargetAnzaalSkillScrankeMax =
					(int?)ZaalMiliAusMengeRegexPatternMitDistanceInGrupe1Match(
					MessageString,
					MengeTargetAnzaalSkillScrankeMaxRegexPattern,
					RegexOptions.IgnoreCase)	/ 1000;

				DroneCommandRange =
					DistanceDurcMeeterAusMengeRegexPatternMitDistanceInGrupe1Match(
					MessageString,
					MengeMessageDroneCommandRangeRegexPattern,
					RegexOptions.IgnoreCase);

				DroneControlCountScrankeMax =
					ZaalMiliAusMengeRegexPatternMitDistanceInGrupe1Match(
					MessageString,
					MengeMessageDroneControlCountScrankeMaxRegexPattern,
					RegexOptions.IgnoreCase)	/ 1000;

				DroneBandwithAvailableMili =
					ZaalMiliAusMengeRegexPatternMitDistanceInGrupe1Match(
					MessageString,
					MengeMessageDroneBandwithAvailableRegexPattern,
					RegexOptions.IgnoreCase);
			}
			finally
			{
				this.ClusterShutdownZaitDistanz = ClusterShutdownZaitDistanz;
				this.DroneCommandRange = DroneCommandRange;
				this.DroneControlCountScrankeMax = DroneControlCountScrankeMax;
				this.DroneBandwithAvailableMili = DroneBandwithAvailableMili;
				this.MengeTargetAnzaalSkillScrankeMax = MengeTargetAnzaalSkillScrankeMax;
			}
		}
	}
}
