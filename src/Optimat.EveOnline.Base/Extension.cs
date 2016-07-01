using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Bib3;
using VonSensor = Optimat.EveOnline.VonSensor;
using Optimat.EveOnline.VonSensor;
using ExtractFromOldAssembly.Bib3;

namespace Optimat.EveOnline
{
	static public class Extension
	{
		static public VonProcessMesung<T> AlsVonProcessMesung<T>(this BotEngine.Interface.FromProcessMeasurement<T> FromProcessMeasurement) =>
			null == FromProcessMeasurement ? null :
			new VonProcessMesung<T>(FromProcessMeasurement.Value, FromProcessMeasurement.Begin, FromProcessMeasurement.End, FromProcessMeasurement.ProcessId);

		static public VonProcessMesung<AusT> Sict<AinT, AusT>(
			this VonProcessMesung<AinT> Ain,
			Func<AinT, AusT> Sict) =>
			null == Ain ? null :
			new VonProcessMesung<AusT>(Sict.Invoke(Ain.Wert), Ain.Begin, Ain.Ende, Ain.ProcessId);

		static public ShipHitpointsAndEnergy WithCapacitySetTo0(
			this ShipHitpointsAndEnergy O)
		{
			if (null == O)
			{
				return null;
			}

			return new ShipHitpointsAndEnergy(O.Struct, O.Armor, O.Shield, 0);
		}

		static public IEnumerable<Window> MengeWindowBerecne(
			this VonSensorikMesung VonSensorikMesung)
		{
			return MengeWindowBerecne<Window>(VonSensorikMesung);
		}

		static public IEnumerable<WindowType> MengeWindowBerecne<WindowType>(
			this VonSensorikMesung VonSensorikMesung)
			where WindowType : Window
		{
			if (null == VonSensorikMesung)
			{
				return null;
			}

			var MengeWindowAinzel = new Window[]{
				VonSensorikMesung.SystemMenu,
				VonSensorikMesung.WindowOverview,
				VonSensorikMesung.WindowSelectedItemView,
				VonSensorikMesung.WindowDroneView,
				VonSensorikMesung.WindowStationLobby,
				VonSensorikMesung.WindowFittingWindow,
				VonSensorikMesung.WindowFittingMgmt,
				VonSensorikMesung.WindowSurveyScanView,
			};

			return
				Optimat.Glob.ConcatTailmengeUnglaicNul(
				new IEnumerable<Window>[]{
					MengeWindowAinzel,
					VonSensorikMesung.MengeWindowInventory,
					VonSensorikMesung.MengeWindowAgentBrowser,
					VonSensorikMesung.MengeWindowAgentDialogue,
					VonSensorikMesung.MengeWindowTelecom,
					VonSensorikMesung.MengeWindowStack,
					VonSensorikMesung.MengeWindowSonstige,
				})
				.OfType<WindowType>();
		}

		static public MessageBox[] MengeMessageBox(
			this VonSensorikMesung VonSensorikMesung)
		{
			return MengeWindowBerecne<MessageBox>(VonSensorikMesung).ToArrayNullable();
		}

		static public HybridWindow[] MengeHybridWindow(
			this VonSensorikMesung VonSensorikMesung)
		{
			return MengeWindowBerecne<HybridWindow>(VonSensorikMesung).ToArrayNullable();
		}

		static public OverviewZaile[] AusWindowOverviewTabListeZaile(
			this VonSensorikMesung VonSensorikMesung)
		{
			if (null == VonSensorikMesung)
			{
				return null;
			}

			var WindowOverview = VonSensorikMesung.WindowOverview;

			if (null == WindowOverview)
			{
				return null;
			}

			return WindowOverview.AusTabListeZaileOrdnetNaacLaage;
		}

		static public IEnumerable<KeyValuePair<InfoPanelTypSictEnum, KeyValuePair<GbsElement, InfoPanel>>> MengeZuInfoPanelTypeButtonUndInfoPanel(
			this VonSensorikMesung VonSensorikMesung)
		{
			if (null == VonSensorikMesung)
			{
				return null;
			}

			return new KeyValuePair<InfoPanelTypSictEnum, KeyValuePair<GbsElement, InfoPanel>>[]{

				new KeyValuePair<InfoPanelTypSictEnum, KeyValuePair<GbsElement, InfoPanel>>(InfoPanelTypSictEnum.SystemInfo,
					new KeyValuePair<GbsElement,    InfoPanel>(VonSensorikMesung.InfoPanelButtonLocationInfo,   VonSensorikMesung.InfoPanelLocationInfo)),

				new KeyValuePair<InfoPanelTypSictEnum, KeyValuePair<GbsElement, InfoPanel>>(InfoPanelTypSictEnum.Route,
					new KeyValuePair<GbsElement,    InfoPanel>(VonSensorikMesung.InfoPanelButtonRoute,  VonSensorikMesung.InfoPanelRoute)),

				new KeyValuePair<InfoPanelTypSictEnum, KeyValuePair<GbsElement, InfoPanel>>(InfoPanelTypSictEnum.AgentMission,
					new KeyValuePair<GbsElement,    InfoPanel>(VonSensorikMesung.InfoPanelButtonMissions,   VonSensorikMesung.InfoPanelMissions)),
			};
		}

		static public ShipUiIndication ShipUiIndication(
			this VonSensorikMesung VonSensorikMesung)
		{
			if (null == VonSensorikMesung)
			{
				return null;
			}

			var ShipUi = VonSensorikMesung.ShipUi;

			if (null == ShipUi)
			{
				return null;
			}

			return ShipUi.Indication;
		}

		static public string ShipUiIndicationCaption(
			this VonSensorikMesung VonSensorikMesung)
		{
			if (null == VonSensorikMesung)
			{
				return null;
			}

			var ShipUiIndication = VonSensorikMesung.ShipUiIndication();

			if (null == ShipUiIndication)
			{
				return null;
			}

			return ShipUiIndication.IndicationCaption;
		}

		static public bool? LanguageNotSetToEnglish(
			this VonSensorikMesung VonSensorikMesung)
		{
			if (null == VonSensorikMesung)
			{
				return null;
			}

			var InfoPanelLocationInfo = VonSensorikMesung.InfoPanelLocationInfo;

			if (null != InfoPanelLocationInfo)
			{
				if (InfoPanelLocationInfo.LanguageNotSetToEnglish ?? false)
				{
					return true;
				}
			}

			return null;
		}

		static public SictAusGbsLocationInfo CurrentLocationInfo(
			this VonSensorikMesung VonSensorikMesung)
		{
			if (null == VonSensorikMesung)
			{
				return null;
			}

			var InfoPanelLocationInfo = VonSensorikMesung.InfoPanelLocationInfo;

			if (null == InfoPanelLocationInfo)
			{
				return null;
			}

			return InfoPanelLocationInfo.CurrentLocationInfo;
		}

		static public GbsElement InfoPanelLocationInfoButtonListSurroundings(
			this VonSensorikMesung VonSensorikMesung)
		{
			if (null == VonSensorikMesung)
			{
				return null;
			}

			var InfoPanelLocationInfo = VonSensorikMesung.InfoPanelLocationInfo;

			if (null == InfoPanelLocationInfo)
			{
				return null;
			}

			return InfoPanelLocationInfo.ButtonListSurroundings;
		}

		static public bool? Docked(
			this VonSensorikMesung Scnapscus)
		{
			if (null == Scnapscus)
			{
				return null;
			}

			var SelfShipState = Scnapscus.SelfShipState;

			if (null == SelfShipState)
			{
				return null;
			}

			return SelfShipState.Docked;
		}

		static public bool? UnDocking(
			this VonSensorikMesung Scnapscus)
		{
			if (null == Scnapscus)
			{
				return null;
			}

			var SelfShipState = Scnapscus.SelfShipState;

			if (null == SelfShipState)
			{
				return null;
			}

			return SelfShipState.UnDocking;
		}

		static public IEnumerable<MissionObjective> MissionObjectiveMengeKomponente(
			this MissionObjective Objective)
		{
			if (null == Objective)
			{
				return null;
			}

			return Objective.MengeObjective;
		}

		static public int? SecurityLevelMinimumMiliBerecne(
			this IEnumerable<WindowAgentMissionObjectiveObjective> MengeObjective)
		{
			int? SecurityLevelMinimumMili = null;

			try
			{
				var MengeObjectiveTransitiiveHüle =
					MengeObjective
					.SelectNullable(MengeObjectiveTransitiveHüleBerecne)
					.ConcatNullable()
					.WhereNotDefault()
					.ToArrayNullable();

				var MengeObjectiveLocation =
					MengeObjectiveTransitiiveHüle
					.Select((Kandidaat) => Kandidaat.Location)
					.Where((Kandidaat) => null != Kandidaat)
					.ToArray();

				if (MengeObjectiveLocation.Length < 1)
				{
					return SecurityLevelMinimumMili;
				}

				var MengeObjectiveLocationSecurityLevelMili =
					MengeObjectiveLocation
					.Select((Location) => Location.LocationSecurityLevelMili)
					.ToArray();

				if (MengeObjectiveLocationSecurityLevelMili.Any((ObjectiveLocationSecurityLevelMili) => !ObjectiveLocationSecurityLevelMili.HasValue))
				{
					return SecurityLevelMinimumMili;
				}

				SecurityLevelMinimumMili =
					MengeObjectiveLocationSecurityLevelMili
					.Select((ObjectiveLocationSecurityLevelMiliNulbar) => ObjectiveLocationSecurityLevelMiliNulbar.Value)
					.Min();

				return SecurityLevelMinimumMili;
			}
			finally
			{
				/*
				 * 2015.02.18
				 * 
				MissionObjective.SecurityLevelMinimumMili = SecurityLevelMinimumMili;
				 * */
			}
		}

		static public bool? MengeObjectiveCompleteBisAufLocationDropOffBerecne(
			this MissionObjective MissionObjective)
		{
			if (null == MissionObjective)
			{
				return null;
			}

			var Typ = MissionObjective.Typ;

			if (SictMissionObjectiveObjectiveElementTyp.LocationDropOff == Typ)
			{
				return true;
			}

			var MengeObjective = MissionObjective.MengeObjective;

			if (null == MengeObjective)
			{
				return MissionObjective.Complete;
			}
			else
			{
				return Optimat.Glob.MengeBoolNulbarKonjunkt(
					MengeObjective
					.Select((Objective) => (null == Objective) ? null : Objective.MengeObjectiveCompleteBisAufLocationDropOffBerecne()));
			}
		}

		static public bool? MissionObjectiveCompleteBerecne(
			IEnumerable<MissionObjective> MengeObjective,
			bool? AusDirektComplete = null)
		{
			bool? Complete = null;

			try
			{
				if (null == MengeObjective)
				{
					Complete = AusDirektComplete;
				}
				else
				{
					Complete = Optimat.Glob.MengeBoolNulbarKonjunkt(
						MengeObjective
						.Select((Objective) => (null == Objective) ? null : Objective.MissionObjectiveCompleteBerecne()));
				}
			}
			finally
			{
				/*
				 * 2015.02.18
				 * 
				MissionObjective.Complete = Complete;
				 * */
			}

			return Complete;
		}

		static public bool? MissionObjectiveCompleteBerecne(
			this MissionObjective MissionObjective)
		{
			if (null == MissionObjective)
			{
				return null;
			}

			bool? AusDirektComplete = null;

			var MissionObjectiveScpez = MissionObjective as WindowAgentMissionObjectiveObjective;

			if (null != MissionObjectiveScpez)
			{
				AusDirektComplete = MissionObjectiveScpez.AusDirektComplete;
			}

			return
				MissionObjectiveCompleteBerecne(MissionObjective.MengeObjective, AusDirektComplete);
		}

		/// <summary>
		/// Menge aler im Baum enthaltene Objective
		/// </summary>
		/// <returns></returns>
		static public IEnumerable<MissionObjective> MengeObjectiveTransitiveHüleBerecne(
			this MissionObjective MissionObjective)
		{
			return
				MissionObjective.BaumEnumFlacListeKnoote(MissionObjectiveMengeKomponente);

			if (null == MissionObjective)
			{
				return null;
			}

			var MengeObjectiveElement = new List<MissionObjective>();

			var MengeObjective = MissionObjective.MengeObjective;

			if (null != MengeObjective)
			{
				foreach (var Objective in MengeObjective)
				{
					if (null == Objective)
					{
						continue;
					}

					MengeObjectiveElement.Add(Objective);

					MengeObjectiveElement.AddRange(Objective.MengeObjectiveTransitiveHüleBerecne());
				}
			}

			return MengeObjectiveElement.ToArray();
		}

		/// <summary>
		/// String welcer im Name (Dict['name']) des GbsAst enthalte ist welcer das Icon des entsprecende EWar typ enthalt.
		/// </summary>
		static readonly KeyValuePair<SictEWarTypeEnum, string>[]
			MengeZuEwarTypeNameString = new KeyValuePair<SictEWarTypeEnum, string>[]{
				//	Scnapscus 2014.00.07.21 - Jam : Jam isc scainbar als "electronic" aingetraage
				new KeyValuePair<SictEWarTypeEnum,  string>(SictEWarTypeEnum.Jam, "electronic"),
				new KeyValuePair<SictEWarTypeEnum,  string>(SictEWarTypeEnum.TrackingDisrupt, "TrackingDisrupt"),
				new KeyValuePair<SictEWarTypeEnum,  string>(SictEWarTypeEnum.RemoteSensorDamp, "RemoteSensorDamp"),
				new KeyValuePair<SictEWarTypeEnum,  string>(SictEWarTypeEnum.WarpScramble, "warpScrambler"),
				new KeyValuePair<SictEWarTypeEnum,  string>(SictEWarTypeEnum.Webify, "webify"),
				new KeyValuePair<SictEWarTypeEnum,  string>(SictEWarTypeEnum.EnergyNeut, "EnergyNeut"),
				new KeyValuePair<SictEWarTypeEnum,  string>(SictEWarTypeEnum.EnergyVampire, "EnergyVampire"),
				new KeyValuePair<SictEWarTypeEnum,  string>(SictEWarTypeEnum.TargetPaint, "TargetPaint"),
				new KeyValuePair<SictEWarTypeEnum,  string>(SictEWarTypeEnum.Miscellaneous, "Misc"),
				};

		static public SictEWarTypeEnum? EWarTypeSictEnum(
			this string EWarTypeSictString)
		{
			if (null == EWarTypeSictString)
			{
				return null;
			}

			foreach (var ZuEwarTypeNameString in MengeZuEwarTypeNameString)
			{
				var Match = Regex.Match(EWarTypeSictString, ZuEwarTypeNameString.Value, RegexOptions.IgnoreCase);

				if (Match.Success)
				{
					return ZuEwarTypeNameString.Key;
				}
			}

			return null;
		}

		static public Int64? IdentNullable(
			this GbsElement GbsElement)
		{
			if (null == GbsElement)
			{
				return null;
			}

			return GbsElement.Ident;
		}

		static public IEnumerable<Int64> MengeAssignedModuleOderDroneGrupeTexturIdent(
			this ShipUiTarget Target)
		{
			if (null == Target)
			{
				yield break;
			}

			var MengeAssignedGrupe = Target.MengeAssignedGrupe;

			if (null == MengeAssignedGrupe)
			{
				yield break;
			}

			var Menge = new List<Int64>();

			foreach (var AssignedModuleOderDroneGrupe in MengeAssignedGrupe)
			{
				var IconTextureIdent = AssignedModuleOderDroneGrupe.IconTextureIdent;

				if (!IconTextureIdent.HasValue)
				{
					continue;
				}

				yield return IconTextureIdent.Value;
			}
		}

		/*
		 * 2015.02.26
		 * 
		static public IEnumerable<KeyValuePair<ScnapscusListEntry, Int64>> MengeEntryExpanderIconTexturIdentTransition(
			ScnapscusListGrouped Scnapscus0,
			ScnapscusListGrouped Scnapscus1)
		{
			if (null == Scnapscus0 || null == Scnapscus1)
			{
				yield break;
			}

			var Scnapscus1ListeEntry = Scnapscus1.ListeEntry;

			if (null == Scnapscus1ListeEntry)
			{
				yield break;
			}

			foreach (var Scnapscus1Entry in Scnapscus1ListeEntry)
			{
				if (null == Scnapscus1Entry)
				{
					continue;
				}

				var Scnapscus1EntryGroupExpanderTexturIdent = Scnapscus1Entry.GroupExpanderTexturIdent;

				if (!Scnapscus1EntryGroupExpanderTexturIdent.HasValue)
				{
					continue;
				}

				//var Scnapscus0Entry =
				//	Scnapscus0.ListeEntry.FirstOrDefaultNullable((Kandidaat) => ((null == Kandidaat) ? null : Kandidaat.Bezaicner) == Scnapscus1Entry.Bezaicner);

				var Scnapscus0Entry =
					Scnapscus0.ListeEntry.FirstOrDefaultNullable((Kandidaat) => (null == Kandidaat) ? false : (Kandidaat.Ident == Scnapscus1Entry.Ident));

				if (null == Scnapscus0Entry)
				{
					continue;
				}

				if (Scnapscus0Entry.GroupExpanderTexturIdent == Scnapscus1EntryGroupExpanderTexturIdent)
				{
					continue;
				}

				yield return new KeyValuePair<ScnapscusListEntry, Int64>(
					Scnapscus1Entry, Scnapscus1EntryGroupExpanderTexturIdent.Value);
			}
		}
		 * */

		static public string ZuHeaderRegexUndItemBerecneZeleWert(
			Regex HeaderRegex,
			ScnapscusListEntry Item)
		{
			if (null == HeaderRegex || null == Item)
			{
				return null;
			}

			return ZuHeaderPrädikaatUndItemBerecneZeleWert((KandidaatHeader) => HeaderRegex.Match(KandidaatHeader.Bescriftung ?? "").Success, Item);
		}

		static public string ZuHeaderPrädikaatUndItemBerecneZeleWert(
			Func<ScrollHeaderInfoFürItem, bool> HeaderPrädikaat,
			ScnapscusListEntry Item)
		{
			if (null == HeaderPrädikaat || null == Item)
			{
				return null;
			}

			var ListeZuHeaderZeleString = Item.ListeZuHeaderZeleString;

			if (null == ListeZuHeaderZeleString)
			{
				return null;
			}

			foreach (var ZuHeaderZeleString in ListeZuHeaderZeleString)
			{
				if (HeaderPrädikaat(ZuHeaderZeleString.Key))
				{
					return ZuHeaderZeleString.Value;
				}
			}

			return null;
		}

		static public bool HatInputFookus(
			this OverviewZaile OverviewZaile)
		{
			if (null == OverviewZaile)
			{
				return false;
			}

			return OverviewZaile.IsSelected ?? false;
		}

		/*
		 * 2015.02.17
		 * 
	static public bool HinraicendGlaicwertigFürIdent(
		VonSensor.OverviewZaile O0,
		VonSensor.OverviewZaile O1,
		bool BedingungOverviewScrollEntryHerkunftAdreseLaseAus)
	{
		if (object.Equals(O0, O1))
		{
			return true;
		}

		if (!IdentiscPerTypeUndName(O0, O1))
		{
			return false;
		}

		if (!(O0.IconMainTextureIdent == O1.IconMainTextureIdent))
		{
			return false;
		}

		if (!FarbeARGB.Glaicwertig(O0.IconMainColor, O1.IconMainColor))
		{
			return false;
		}

		if (!BedingungOverviewScrollEntryHerkunftAdreseLaseAus)
		{
			if (!(O0.OverviewScrollEntryHerkunftAdrese == O1.OverviewScrollEntryHerkunftAdrese))
			{
				return false;
			}
		}

		return true;
	}
		 * */

		static public bool HinraicendGlaicwertigFürIdent(
			OverviewZaile O0,
			OverviewZaile O1)
		{
			if (object.Equals(O0, O1))
			{
				return true;
			}

			if (!(O0.Ident == O1.Ident))
			{
				return false;
			}

			if (!(O0.DistanceMin == O1.DistanceMin &&
				O0.DistanceMax == O1.DistanceMax))
			{
				return false;
			}

			if (!OverviewZaile.IdentiscPerTypeUndName(O0, O1))
			{
				return false;
			}

			if (!(O0.IconMainTextureIdent == O1.IconMainTextureIdent))
			{
				return false;
			}

			if (!FarbeARGB.Glaicwertig(O0.IconMainColor, O1.IconMainColor))
			{
				return false;
			}

			return true;
		}
		/*
		 * 2015.02.17
		 * 
		static public bool HinraicendGlaicwertigFürIdent(
			VonSensor.OverviewZaileInterpretiirt O0,
			VonSensor.OverviewZaileInterpretiirt O1)
		{
			if (object.Equals(O0, O1))
			{
				return true;
			}

			if (!(O0.DistanceScrankeMin == O1.DistanceScrankeMin &&
				O0.DistanceScrankeMax == O1.DistanceScrankeMax))
			{
				return false;
			}

			if (!VonSensor.OverviewZaile.HinraicendGlaicwertigFürIdent(O0.AusOverviewZaile, O1.AusOverviewZaile, BedingungOverviewScrollEntryHerkunftAdreseLaseAus))
			{
				return false;
			}

			if(!ObjektMitIdentInt64.Identisc(O0.AusOverviewZaile, O1.AusOverviewZaile))
			{
				return false;
			}

			return true;
		}
		 * */

		/*
		 * 2015.02.17
		 * 
		static public bool OverviewIconMainColorIsRedAsRat(
			this	VonSensor.OverviewZaileInterpretiirt OverviewRow)
		{
			if (null == OverviewRow)
			{
				return false;
			}

			return OverviewRow.AusOverviewZaile.OverviewIconMainColorIsRedAsRat();
		}
		 * */

		static public IEnumerable<OverviewZaile> MengeZaileMitInputFookus(
			this WindowOverView Overview)
		{
			if (null == Overview)
			{
				return null;
			}

			return Overview.AusTabListeZaileOrdnetNaacLaage.WhereNullable((Zaile) => Zaile.HatInputFookus());
		}

		static public OverviewZaile ZaileMitInputFookusExklusiiv(
			this WindowOverView Overview)
		{
			var MengeZaileMitInputFookus = Overview.MengeZaileMitInputFookus().ToArrayFalsNitLeer();

			if (1 == MengeZaileMitInputFookus.CountNullable())
			{
				return MengeZaileMitInputFookus.FirstOrDefaultNullable();
			}

			return null;
		}

		static public OverviewZaile OverviewRowEnthaltendGbsAstBerecne(
			this WindowOverView Overview,
			ObjektMitIdentInt64 GbsAst)
		{
			if (null == Overview)
			{
				return null;
			}

			if (null == GbsAst)
			{
				return null;
			}

			var GbsAstHerkunftAdrese = GbsAst.Ident;

			var AusTabListeZaileOrdnetNaacLaage = Overview.AusTabListeZaileOrdnetNaacLaage;

			if (null == AusTabListeZaileOrdnetNaacLaage)
			{
				return null;
			}

			foreach (var Zaile in AusTabListeZaileOrdnetNaacLaage)
			{
				if (null == Zaile)
				{
					continue;
				}

				if (Zaile.Ident == GbsAstHerkunftAdrese)
				{
					return Zaile;
				}
			}

			return null;
		}

		static public bool OverviewIconMainColorIsRedAsRat(
			this OverviewZaile OverviewRow)
		{
			if (null == OverviewRow)
			{
				return false;
			}

			return OverviewRow.IconMainColor.OverviewIconMainColorIsRedAsRat();
		}

		static public bool OverviewIconMainColorIsRedAsRat(
			this FarbeARGB IconMainColor)
		{
			if (null == IconMainColor)
			{
				return false;
			}

			return
				IconMainColor.BMilli * 3 < IconMainColor.RMilli &&
				IconMainColor.GMilli * 3 < IconMainColor.RMilli;
		}

		static public bool AstEnthalteInAst(
			this GbsElement GbsAst,
			Func<GbsElement, bool> Prädikaat,
			int? TiifeScrankeMax = null)
		{
			return GbsAst.AstEnthalteInBaum(Prädikaat, (tAst) => tAst.ListeGbsKindBerecne(), TiifeScrankeMax);
		}

		static public ShipUiTarget TargetEnthaltendGbsAstBerecne(
			this VonSensorikMesung VonSensorikScnapscus,
			ObjektMitIdentInt64 GbsAst)
		{
			if (null == VonSensorikScnapscus)
			{
				return null;
			}

			if (null == GbsAst)
			{
				return null;
			}

			var GbsAstHerkunftAdrese = GbsAst.Ident;

			return
				VonSensorikScnapscus.MengeTarget
				.FirstOrDefaultNullable((KandidaatTarget) =>
					KandidaatTarget.AstEnthalteInAst((KandidaatAstEnthalte) => KandidaatAstEnthalte.Ident == GbsAstHerkunftAdrese));
		}

		static public OrtogoonInt? InGbsFläceNullable(
			this GbsElement Ast)
		{
			if (null == Ast)
			{
				return null;
			}

			return Ast.InGbsFläce;
		}

		static public Vektor2DInt? InGbsFläceZentrumNullable(
			this GbsElement Ast)
		{
			if (null == Ast)
			{
				return null;
			}

			return Ast.InGbsFläce.ZentrumLaage;
		}

		/*
		 * 2015.02.23
		 * 
		static public Vektor2DInt	InGbsFläceMiteLaage(
			this	GbsElement GbsElement)
		{
			if (null == GbsElement)
			{
				return null;
			}

			var InGbsFläce = GbsElement.InGbsFläce;

			if (null == InGbsFläce)
			{
				return null;
			}

			return InGbsFläce.ZentrumLaage;
		}

		static	public	Vektor2DInt?	InGbsFläceMiteLaageInt(
			this	GbsElement GbsElement)
		{
			var InGbsFläceMiteLaage = GbsElement.InGbsFläceMiteLaage();

			if(!InGbsFläceMiteLaage.HasValue)
			{
				return null;
			}

			return new Vektor2DInt((Int64)InGbsFläceMiteLaage.Value.A, (Int64)InGbsFläceMiteLaage.Value.B);
		}
		 * */

		static public IEnumerable<ScnapscusListGrouped> MengeListGroupedBerecne(
			this VonSensorikMesung VonSensorikScnapscus)
		{
			return
				VonSensorikScnapscus.MengeWindowBerecne()
				.SelectNullable((Window) => Window.ListHaupt())
				.WhereNullable((List) => null != List);
		}

		static public IEnumerable<GbsElement> OrderByCenterDistanceToPoint(
			this IEnumerable<GbsElement> MengeGbsAst,
			Vektor2DInt Punkt)
		{
			if (null == MengeGbsAst)
			{
				return null;
			}

			var MengeGbsAstMitDistanzQuadraat =
				MengeGbsAst
				.SelectNullable((GbsAst) =>
				{
					double? Distanz = null;

					if (null != GbsAst)
					{
						var GbsAstInGbsFläce = GbsAst.InGbsFläce;

						if (null != GbsAstInGbsFläce)
						{
							Distanz = (Punkt - GbsAstInGbsFläce.ZentrumLaage).BetraagQuadriirt;
						}
					}
					return new KeyValuePair<GbsElement, double?>(GbsAst, Distanz);
				})
				.Where((Kandidaat) => Kandidaat.Value.HasValue);

			return
				MengeGbsAstMitDistanzQuadraat
				.OrderBy((t) => t.Value)
				.Select((t) => t.Key);
		}

		static public Bib3.RefNezDiferenz.SictMengeTypeBehandlungRictliinie GbsBaumMengeTypeBehandlungRictliinieKonstrukt()
		{
			return Bib3.RefNezDiferenz.NewtonsoftJson.SictMengeTypeBehandlungRictliinieNewtonsoftJson.KonstruktMengeTypeBehandlungRictliinie();
		}

		static readonly Bib3.RefNezDiferenz.SictTypeBehandlungRictliinieMitTransportIdentScatescpaicer
			GbsAstListeKindTypeRictliinie =
			new Bib3.RefNezDiferenz.SictTypeBehandlungRictliinieMitTransportIdentScatescpaicer(
				GbsBaumMengeTypeBehandlungRictliinieKonstrukt());

		static public IEnumerable<GbsElement> ListeGbsKindBerecneDurcReflection(
			this object GbsAst)
		{
			if (null == GbsAst)
			{
				yield break;
			}

			var GbsAstTypeRictliinie = GbsAstListeKindTypeRictliinie.ZuTypeBehandlung(GbsAst.GetType());

			if (null == GbsAstTypeRictliinie)
			{
				yield break;
			}

			var MengeMemberBehandlung = GbsAstTypeRictliinie.MengeMemberBehandlung;

			if (null == MengeMemberBehandlung)
			{
				yield break;
			}

			foreach (var MemberBehandlung in MengeMemberBehandlung)
			{
				var HerkunftMemberType = MemberBehandlung.HerkunftMemberType;

				var HerkunftTypeMemberGetter = MemberBehandlung.HerkunftTypeMemberGetter;

				if (null == HerkunftTypeMemberGetter)
				{
					continue;
				}

				var MemberWertAlsKind = HerkunftTypeMemberGetter(GbsAst) as GbsElement;
				var MemberWertAlsMengeKind = HerkunftTypeMemberGetter(GbsAst) as IEnumerable<GbsElement>;

				if (null != MemberWertAlsKind)
				{
					yield return MemberWertAlsKind;
				}

				if (null != MemberWertAlsMengeKind)
				{
					foreach (var Element in MemberWertAlsMengeKind)
					{
						if (null == Element)
						{
							continue;
						}

						yield return Element;
					}
				}
			}
		}

		static public IEnumerable<GbsElement> ListeGbsKindBerecne(
			this object GbsAst)
		{
			return ListeGbsKindBerecneDurcReflection(GbsAst);
		}

		static public IEnumerable<GbsElement[]> GbsBaumEnumFlacListeNaacKnootePfaad(
			this object SuuceUrscprung)
		{
			return
				ExtractFromOldAssembly.Bib3.Extension.BaumEnumFlacListeNaacKnootePfaad(
				SuuceUrscprung,
				Ast => Ast.ListeGbsKindBerecne())
				.SelectNullable(Pfaad => Pfaad.SelectNullable(Ast => Ast as GbsElement).ToArrayNullable());
		}

		static public IEnumerable<GbsElement[]> SuuceFlacMengeAstMitPfaad(
			this object SuuceUrscprung,
			Func<GbsElement, bool> Prädikaat)
		{
			return
				GbsBaumEnumFlacListeNaacKnootePfaad(SuuceUrscprung)
				.WhereNullable(KandidaatPfaad => null == Prädikaat ? false : Prädikaat(KandidaatPfaad.LastOrDefaultNullable()));
		}

		static public IEnumerable<GbsElement> GbsBaumEnumFlacListeKnoote(
			this object SuuceUrscprung)
		{
			return
				GbsBaumEnumFlacListeNaacKnootePfaad(
				SuuceUrscprung)
				.SelectNullable(ExtractFromOldAssembly.Bib3.Extension.LastOrDefaultNullable);
		}

		static public GbsElement SuuceFlacMengeGbsAstFrühesteMitIdent(
			this object SuuceUrscprung,
			Int64 Ident)
		{
			return
				GbsBaumEnumFlacListeKnoote(SuuceUrscprung).FirstOrDefaultNullable(Kandidaat => Kandidaat.Ident == Ident);
		}

		static public IEnumerable<GbsElement> ListeKnooteUnterPfaadPrädikaat(
			this GbsElement EnthaltendeKnoote,
			Func<GbsElement, bool>[] ListePrädikaat,
			int? TiifeScrankeMax = null)
		{
			if (null == EnthaltendeKnoote)
			{
				yield break;
			}

			if (ListePrädikaat.NullOderLeer())
			{
				yield break;
			}

			var LastPredicate = ListePrädikaat.Last();

			var MengeKandidaatPfaad = EnthaltendeKnoote.SuuceFlacMengeAstMitPfaad(LastPredicate);

			if (null == MengeKandidaatPfaad)
			{
				yield break;
			}

			foreach (var KandidaatPfaad in MengeKandidaatPfaad)
			{
				if (TiifeScrankeMax.HasValue)
				{
					if (TiifeScrankeMax < KandidaatPfaad.Length)
					{
						break;
					}
				}

				int BisherInPfaadKnooteIndex = 0;

				for (int PrädikaatIndex = 0; PrädikaatIndex < ListePrädikaat.Length - 1; PrädikaatIndex++)
				{
					var KandidaatPfaadRest =
						KandidaatPfaad
						.Skip(BisherInPfaadKnooteIndex)
						.ToArray();

					var FürPrädikaatInPfaadFrüühesteKnooteIndex =
						KandidaatPfaadRest.FrühesteIndex(ListePrädikaat[PrädikaatIndex]);

					if (!FürPrädikaatInPfaadFrüühesteKnooteIndex.HasValue)
					{
						goto PfaadNääxte;
					}

					BisherInPfaadKnooteIndex += FürPrädikaatInPfaadFrüühesteKnooteIndex.Value;
				}

				yield return KandidaatPfaad.Last();

				PfaadNääxte:

				continue;
			}
		}

		static bool PunktLiigtInRegioon1D(
			Int64 RegioonMin,
			Int64 RegioonMax,
			Int64 Punkt)
		{
			return (RegioonMin <= Punkt && Punkt <= RegioonMax);
		}

		static Int64[] ListeGrenzeAusÜberscnaidung1D(
			Int64 RegioonAScrankeMin,
			Int64 RegioonAScrankeMax,
			Int64 RegioonBScrankeMin,
			Int64 RegioonBScrankeMax)
		{
			var RegioonAMinMax = new KeyValuePair<Int64, Int64>(RegioonAScrankeMin, RegioonAScrankeMax);
			var RegioonBMinMax = new KeyValuePair<Int64, Int64>(RegioonBScrankeMin, RegioonBScrankeMax);

			var MengeKandidaat = new KeyValuePair<Int64, KeyValuePair<Int64, Int64>>[]{
				new KeyValuePair<Int64, KeyValuePair<Int64, Int64>>(RegioonAScrankeMin,RegioonBMinMax),
				new KeyValuePair<Int64, KeyValuePair<Int64, Int64>>(RegioonAScrankeMax,RegioonBMinMax),
				new KeyValuePair<Int64, KeyValuePair<Int64, Int64>>(RegioonBScrankeMin,RegioonAMinMax),
				new KeyValuePair<Int64, KeyValuePair<Int64, Int64>>(RegioonBScrankeMax,RegioonAMinMax),};

			var ListeGrenzePunkt =
				MengeKandidaat
				.Where((Kandidaat) => PunktLiigtInRegioon1D(Kandidaat.Value.Key, Kandidaat.Value.Value, Kandidaat.Key))
				.Select((Kandidaat) => Kandidaat.Key)
				.ToArray();

			var ListeGrenzePunktDistinct =
				ListeGrenzePunkt.Distinct().ToArray();

			return ListeGrenzePunktDistinct;
		}

		static public Int64 Betraag(
			this OrtogoonInt Ortogoon)
		{
			return Math.Max(0, Ortogoon.Grööse.A) * Math.Max(0, Ortogoon.Grööse.B);
		}

		static public OrtogoonInt Vergröösert(
			this OrtogoonInt Ortogoon,
			int VergrööserungA,
			int VergrööserungB)
		{
			return
				Ortogoon.GrööseGeseztAngelpunktZentrum(Ortogoon.Grööse + new Vektor2DInt(VergrööserungA, VergrööserungB));
		}

		static public OrtogoonInt VergröösertBescrankt(
			this OrtogoonInt Ortogoon,
			int VergrööserungRictungA,
			int VergrööserungRictungB,
			int? GrööseAScrankeMin,
			int? GrööseBScrankeMin)
		{
			var GrööseA = Ortogoon.Grööse.A + VergrööserungRictungA;
			var GrööseB = Ortogoon.Grööse.B + VergrööserungRictungB;

			if (GrööseAScrankeMin.HasValue)
			{
				GrööseA = Math.Max(GrööseAScrankeMin.Value, GrööseA);
			}

			if (GrööseBScrankeMin.HasValue)
			{
				GrööseB = Math.Max(GrööseBScrankeMin.Value, GrööseB);
			}

			return OrtogoonInt.AusPunktZentrumUndGrööse(Ortogoon.ZentrumLaage, new Vektor2DInt(GrööseA, GrööseB));
		}

		static public Int64 DiferenzLaageUndGrööseSume(
			OrtogoonInt O0,
			OrtogoonInt O1)
		{
			if (object.Equals(O0, O1))
			{
				return 0;
			}

			var DiferenzLaage = O1.ZentrumLaage - O0.ZentrumLaage;
			var DiferenzGrööse = O1.Grööse - O0.Grööse;

			return DiferenzLaage.A + DiferenzLaage.B + DiferenzGrööse.A + DiferenzGrööse.B;
		}

		/// <summary>
		/// gibt Diferenz als Menge von Rectek zurük
		/// </summary>
		/// <param name="Minuend"></param>
		/// <param name="Subtrahend"></param>
		/// <returns></returns>
		static public OrtogoonInt[] FläceMiinusFläce(OrtogoonInt Minuend, OrtogoonInt Subtrahend)
		{
			if (null == Minuend)
			{
				return null;
			}

			if (null == Subtrahend)
			{
				return new OrtogoonInt[] { new OrtogoonInt(Minuend) };
			}

			var MinuendMinMax = new KeyValuePair<Vektor2DInt, Vektor2DInt>(Minuend.PunktMin, Minuend.PunktMax);
			var SubtrahendMinMax = new KeyValuePair<Vektor2DInt, Vektor2DInt>(Subtrahend.PunktMin, Subtrahend.PunktMax);

			if (MinuendMinMax.Value.A <= SubtrahendMinMax.Key.A ||
				MinuendMinMax.Value.B <= SubtrahendMinMax.Key.B ||
				SubtrahendMinMax.Value.A <= MinuendMinMax.Key.A ||
				SubtrahendMinMax.Value.B <= MinuendMinMax.Key.B)
			{
				//	Scpez Fal kaine Scnitmenge
				return new OrtogoonInt[] { new OrtogoonInt(Minuend) };
			}

			if (MinuendMinMax.Value.A <= SubtrahendMinMax.Value.A &&
				MinuendMinMax.Value.B <= SubtrahendMinMax.Value.B &&
				SubtrahendMinMax.Key.A <= MinuendMinMax.Key.A &&
				SubtrahendMinMax.Key.B <= MinuendMinMax.Key.B)
			{
				//	Scpez Fal Minuend liigt volsctändig in Subtrahend
				return new OrtogoonInt[0];
			}

			Int64[] RictungAMengeScranke =
				ListeGrenzeAusÜberscnaidung1D(
				MinuendMinMax.Key.A,
				MinuendMinMax.Value.A,
				SubtrahendMinMax.Key.A,
				SubtrahendMinMax.Value.A)
				.OrderBy((t) => t)
				.ToArray();

			Int64[] RictungBMengeScranke =
				ListeGrenzeAusÜberscnaidung1D(
				MinuendMinMax.Key.B,
				MinuendMinMax.Value.B,
				SubtrahendMinMax.Key.B,
				SubtrahendMinMax.Value.B)
				.OrderBy((t) => t)
				.ToArray();

			if (RictungAMengeScranke.Length < 1 || RictungBMengeScranke.Length < 1)
			{
				//	Scpez Fal kaine Scnitmenge,	(Redundant zur Prüüfung oobn)
				return new OrtogoonInt[] { new OrtogoonInt(Minuend) };
			}

			/*
			2015.09.02	Korektur.
			var RictungAMengeScrankeMitMinuendGrenze =
				new Int64[] { MinuendMinMax.Key.A }.Concat(RictungAMengeScranke).Concat(new Int64[] { MinuendMinMax.Value.A }).ToArray();
			*/
			var RictungAMengeScrankeMitMinuendGrenze =
				new Int64[] { MinuendMinMax.Key.A }.Concat(RictungAMengeScranke).Concat(new Int64[] { MinuendMinMax.Value.A })
				.Distinct().ToArray();

			var ListeFläce = new List<OrtogoonInt>();

			for (int RictungAScrankeIndex = 0; RictungAScrankeIndex < RictungAMengeScrankeMitMinuendGrenze.Length - 1; RictungAScrankeIndex++)
			{
				var RictungAScrankeMinLaage = RictungAMengeScrankeMitMinuendGrenze[RictungAScrankeIndex];

				var RictungAScrankeMaxLaage = RictungAMengeScrankeMitMinuendGrenze[RictungAScrankeIndex + 1];

				/*
				2015.09.02
				Korektur: Restfläce Komponente wurde for 2015 konstruiirt aus:
				static public SictFläceRectekOrto AusMinMax(float AMin, float AMax, float BMin, float BMax)
				*/
				if (SubtrahendMinMax.Value.A <= RictungAScrankeMinLaage ||
						RictungAScrankeMaxLaage <= SubtrahendMinMax.Key.A)
				{
					//	in RictungB unbescrankter Abscnit
					/*
					2015.09.02	Korektur.
					ListeFläce.Add(new OrtogoonInt(RictungAScrankeMinLaage, RictungAScrankeMaxLaage, MinuendMinMax.Key.B, MinuendMinMax.Value.B));
					*/
					ListeFläce.Add(new OrtogoonInt(RictungAScrankeMinLaage, MinuendMinMax.Key.B, RictungAScrankeMaxLaage, MinuendMinMax.Value.B));
				}
				else
				{
					var RictungBMengeScrankeFrüheste = RictungBMengeScranke.First();
					var RictungBMengeScrankeLezte = RictungBMengeScranke.Last();

					if (MinuendMinMax.Key.B < SubtrahendMinMax.Key.B)
					{
						/*
						2015.09.02	Korektur.
						ListeFläce.Add(new OrtogoonInt(RictungAScrankeMinLaage, RictungAScrankeMaxLaage, MinuendMinMax.Key.B, SubtrahendMinMax.Key.B));
						*/
						ListeFläce.Add(new OrtogoonInt(RictungAScrankeMinLaage, MinuendMinMax.Key.B, RictungAScrankeMaxLaage, SubtrahendMinMax.Key.B));
					}

					if (SubtrahendMinMax.Value.B < MinuendMinMax.Value.B)
					{
						/*
						2015.09.02	Korektur.

						ListeFläce.Add(new OrtogoonInt(RictungAScrankeMinLaage, RictungAScrankeMaxLaage, SubtrahendMinMax.Value.B, MinuendMinMax.Value.B));

						Bsp vor 2015:
						ListeFläce.Add(SictFläceRectekOrto.AusMinMax(RictungAScrankeMinLaage, RictungAScrankeMaxLaage, SubtrahendMinMax.Value.B, MinuendMinMax.Value.B));
						*/

						ListeFläce.Add(new OrtogoonInt(RictungAScrankeMinLaage, SubtrahendMinMax.Value.B, RictungAScrankeMaxLaage, MinuendMinMax.Value.B));
					}
				}
			}

			return ListeFläce.ToArray();
		}

		static public OrtogoonInt[] FläceMiinusFläce(OrtogoonInt[] MengeMinuend, OrtogoonInt Subtrahend)
		{
			if (null == MengeMinuend)
			{
				return null;
			}

			var DiferenzMengeFläce = new List<OrtogoonInt>();

			foreach (var Minuend in MengeMinuend)
			{
				var FürMinduendDiff = FläceMiinusFläce(Minuend, Subtrahend);

				if (null == FürMinduendDiff)
				{
					continue;
				}

				DiferenzMengeFläce.AddRange(FürMinduendDiff);
			}

			return DiferenzMengeFläce.ToArray();
		}


		static public GbsElement SictFläceGesezt(
			this GbsElement GbsElement,
			OrtogoonInt FläceErsaz)
		{
			if (null == GbsElement)
			{
				return null;
			}

			var VorherInGbsFläce = GbsElement.InGbsFläce;

			return new GbsElement(GbsElement, FläceErsaz, GbsElement.InGbsBaumAstIndex);
		}

		static public GbsElement SictGrööseGesezt(
			this GbsElement GbsElement,
			Vektor2DInt GrööseErsaz)
		{
			if (null == GbsElement)
			{
				return null;
			}

			return GbsElement.SictFläceGesezt(GbsElement.InGbsFläce.GrööseGeseztAngelpunktZentrum(GrööseErsaz));
		}

		static public string AusLinxTreeEntryShipBescriftungExtraktShipType(
			string TreeEntryShipBescriftung,
			out string ShipName)
		{
			ShipName = null;

			if (null == TreeEntryShipBescriftung)
			{
				return null;
			}

			var Match = Regex.Match(TreeEntryShipBescriftung, "(.*)\\((.*)\\)");

			if (!Match.Success)
			{
				return null;
			}

			ShipName = Match.Groups[1].Value;

			return Match.Groups[2].Value;
		}

		static public string AusLinxTreeEntryShipBescriftungExtraktShipType(
			string TreeEntryShipBescriftung)
		{
			string ShipName;

			return AusLinxTreeEntryShipBescriftungExtraktShipType(TreeEntryShipBescriftung, out ShipName);
		}

		static public string AusLinxTreeEntryShipExtraktShipType(
			this TreeViewEntry TreeEntryShip)
		{
			if (null == TreeEntryShip)
			{
				return null;
			}

			return AusLinxTreeEntryShipBescriftungExtraktShipType(TreeEntryShip.LabelText);
		}

		static public TreeViewEntry TreeEntryBerecneAusCargoTyp(
			this TreeViewEntry ShipTreeEntry,
			SictShipCargoTypSictEnum ShipCargoTyp)
		{
			if (null == ShipTreeEntry)
			{
				return null;
			}

			string IdentString = null;

			switch (ShipCargoTyp)
			{
				case SictShipCargoTypSictEnum.General:
					return ShipTreeEntry;
				case SictShipCargoTypSictEnum.DroneBay:
					IdentString = "Drone Bay";
					break;
				case SictShipCargoTypSictEnum.OreHold:
					IdentString = "Ore Hold";
					break;
				default:
					return null;
			}

			if (null == IdentString)
			{
				return null;
			}

			var ShipTreeEntryMengeChild = ShipTreeEntry.MengeChild;

			if (null == ShipTreeEntryMengeChild)
			{
				return null;
			}

			return
				ExtractFromOldAssembly.Bib3.Extension.FirstOrDefaultNullable(
				ShipTreeEntryMengeChild,
				(Kandidaat) => null == Kandidaat ? false : Regex.Match(Kandidaat.LabelTextTailObjektName ?? "", IdentString, RegexOptions.IgnoreCase).Success);
		}

		static public float? InGbsFläceMiteLaageB(
			this GbsElement Ast)
		{
			var InGbsFläceMiteLaage = Ast.InGbsFläceZentrumLaage();

			if (!InGbsFläceMiteLaage.HasValue)
			{
				return null;
			}

			return InGbsFläceMiteLaage.Value.B;
		}

		static public OrtogoonInt? InGbsFläce(
			this GbsElement ObjektMitInGbsFläce)
		{
			if (null == ObjektMitInGbsFläce)
			{
				return null;
			}

			return ObjektMitInGbsFläce.InGbsFläce;
		}

		static public float? InGbsFläceMiteLaageA(
			this GbsElement Ast)
		{
			var InGbsFläceMiteLaage = Ast.InGbsFläceZentrumLaage();

			if (!InGbsFläceMiteLaage.HasValue)
			{
				return null;
			}

			return InGbsFläceMiteLaage.Value.A;
		}

		static public Vektor2DInt? InGbsFläceZentrumLaage(
			this GbsElement ObjektMitInGbsFläce)
		{
			if (null == ObjektMitInGbsFläce)
			{
				return null;
			}

			var InGbsFläce = ObjektMitInGbsFläce.InGbsFläce;

			if (null == InGbsFläce)
			{
				return null;
			}

			return InGbsFläce.ZentrumLaage;
		}

		static public GbsElement GbsElementZuIdentOderNull(
			this Int64? Ident)
		{
			if (!Ident.HasValue)
			{
				return null;
			}

			return new GbsElement(new ObjektMitIdentInt64(Ident.Value));
		}

		static public bool AusMengeScnapscusHinraicendGlaicwertigFürÜbernaame(
			ModuleButtonHintZaile[] O0ListeZaile,
			ModuleButtonHintZaile[] O1ListeZaile)
		{
			if (O0ListeZaile == O1ListeZaile)
			{
				return true;
			}

			if (null == O0ListeZaile || null == O1ListeZaile)
			{
				return false;
			}

			if (O0ListeZaile.Length != O1ListeZaile.Length)
			{
				return false;
			}

			for (int ZaileIndex = 0; ZaileIndex < O0ListeZaile.Length; ZaileIndex++)
			{
				var O0Zaile = O0ListeZaile[ZaileIndex];
				var O1Zaile = O1ListeZaile[ZaileIndex];

				if (!AusMengeScnapscusHinraicendGlaicwertigFürÜbernaame(O0Zaile, O1Zaile))
				{
					return false;
				}
			}

			return true;
		}

		static public bool AusMengeScnapscusHinraicendGlaicwertigFürÜbernaame(
			ModuleButtonHint O0,
			ModuleButtonHint O1)
		{
			if (O0 == O1)
			{
				return true;
			}

			if (null == O0 || null == O1)
			{
				return false;
			}

			if (!(O0.ShortcutText == O1.ShortcutText))
			{
				return false;
			}

			var O0ListeZaile = O0.ListeZaile;
			var O1ListeZaile = O1.ListeZaile;

			return AusMengeScnapscusHinraicendGlaicwertigFürÜbernaame(O0ListeZaile, O1ListeZaile);
		}

		static public bool AusMengeScnapscusHinraicendGlaicwertigFürÜbernaame(
			ModuleButtonHintZaile O0,
			ModuleButtonHintZaile O1)
		{
			if (O0 == O1)
			{
				return true;
			}

			if (null == O0 || null == O1)
			{
				return false;
			}

			return
				string.Equals(O0.Bescriftung, O1.Bescriftung) &&
				string.Equals(O0.ShortcutText, O1.ShortcutText) &&
				Bib3.Glob.SequenceEqualPerObjectEquals(O0.MengeIconTextureIdent, O1.MengeIconTextureIdent);
		}

		static public bool? PasendZuModuleReprBerecne(
			this ModuleButtonHint ModuleButtonHint,
			ShipUiModule ModuleRepr)
		{
			if (null == ModuleButtonHint)
			{
				return null;
			}

			if (null == ModuleRepr)
			{
				return null;
			}

			var ModuleReprModuleButtonIconTextureIdent = ModuleRepr.ModuleButtonIconTextureIdent;

			if (!ModuleReprModuleButtonIconTextureIdent.HasValue)
			{
				return null;
			}

			var ListeZaile = ModuleButtonHint.ListeZaile;

			if (null == ListeZaile)
			{
				return null;
			}

			foreach (var Zaile in ListeZaile)
			{
				if (null == Zaile)
				{
					continue;
				}

				var ZaileMengeIconTextureIdent = Zaile.MengeIconTextureIdent;

				if (null == ZaileMengeIconTextureIdent)
				{
					continue;
				}

				if (ZaileMengeIconTextureIdent.Contains(ModuleReprModuleButtonIconTextureIdent.Value))
				{
					return true;
				}
			}

			return false;
		}

		static public bool AstMitHerkunftAdreseEnthaltAstMitHerkunftAdrese(
			this GbsElement SuuceWurzel,
			Int64 EnthaltendeAstHerkunftAdrese,
			Int64 EnthalteneAstHerkunftAdrese)
		{
			if (EnthaltendeAstHerkunftAdrese == EnthalteneAstHerkunftAdrese)
			{
				return true;
			}

			if (null == SuuceWurzel)
			{
				return false;
			}

			var EnthaltendeAst = SuuceWurzel.SuuceFlacMengeGbsAstFrühesteMitIdent(EnthaltendeAstHerkunftAdrese);

			if (null == EnthaltendeAst)
			{
				return false;
			}

			return null != EnthaltendeAst.SuuceFlacMengeGbsAstFrühesteMitIdent(EnthalteneAstHerkunftAdrese);
		}

		static public VonProcessMesung<T> Abbild<T>(
			this VonProcessMesung<T> VonProcessMesung,
			Func<T, T> SictWert)
		{
			if (null == VonProcessMesung)
			{
				return null;
			}

			return
				new VonProcessMesung<T>(
					SictWert(VonProcessMesung.Wert),
					VonProcessMesung.BeginZait,
					VonProcessMesung.EndeZait,
					VonProcessMesung.ProcessId);
		}

		static public Base.MotionResult MapToNew(this SictNaacProcessWirkung motion) =>
			null == motion ? null :
			new Base.MotionResult
			{
				MotionRecommendationId = motion.VorsclaagWirkungIdent ?? -1,
				Success = motion.Erfolg ?? false,
			};

		static public SictNaacProcessWirkung MapToOld(this Base.MotionResult motion) =>
			null == motion ? null :
			new SictNaacProcessWirkung
			{
				VorsclaagWirkungIdent = motion.MotionRecommendationId,
				Erfolg = motion.Success,
			};

		static public string RegexPatternStringGlaicwertig(this string @string)
		{
			if (null == @string)
			{
				return null;
			}

			return "^" + Regex.Escape(@string) + "$";
		}

	}
}
