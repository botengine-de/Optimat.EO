using Bib3;
using BotEngine.Common;
using BotEngine.EveOnline;
using BotEngine.EveOnline.Parse;
using ExtractFromOldAssembly.Bib3;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MemoryStruct = BotEngine.EveOnline.Interface.MemoryStruct;

namespace Optimat.EveOnline
{
	/// <summary>
	/// 2015.09.00 Map für Wexel auf Interface aus BotEngine.EveOnline.
	/// </summary>
	static public class MemoryStructMap
	{
		static public VonSensorikMesung AlsVonSensorikMesung(this MemoryStruct.MemoryMeasurement MemoryMeasurement)
		{
			if (null == MemoryMeasurement)
			{
				return null;
			}

			//	!!!!	Mööglicerwaise in konsumiirende berüksictigung verkeerte InTreeIndex nootwendig	!!!!

			var ButtonInventory =
				MemoryMeasurement?.Neocom?.Button?.FirstOrDefault(button => Regex.Match(button?.TexturePath ?? "", @"item(|s)\.").Success);

            return new VonSensorikMesung()
			{
				SessionDurationRemaining = MemoryMeasurement?.SessionDurationRemaining,
				VersionString = MemoryMeasurement.VersionString,
				MengeMenu = MemoryMeasurement.Menu?.OrderBy(t => t.InTreeIndex)?.Select(AlsMenu)?.ToArray(),
				SystemMenu = MemoryMeasurement.SystemMenu.AlsSystemMenu(),
				CharacterAuswaalAbgesclose = MemoryMeasurement.CharacterAuswaalAbgesclose(),
				InfoPanelButtonLocationInfo = MemoryMeasurement?.InfoPanelButtonLocationInfo?.AlsGbsElement(),
				InfoPanelButtonRoute = MemoryMeasurement?.InfoPanelButtonRoute?.AlsGbsElement(),
				InfoPanelButtonMissions = MemoryMeasurement?.InfoPanelButtonMissions?.AlsGbsElement(),
				InfoPanelIncursionsExistent = null != MemoryMeasurement?.InfoPanelButtonIncursions,
				InfoPanelLocationInfo = MemoryMeasurement.InfoPanelLocationInfo.AlsInfoPanelLocationInfo(),
				InfoPanelMissions = MemoryMeasurement.InfoPanelMissions.AlsInfoPanelMissions(),
				InfoPanelRoute = MemoryMeasurement.InfoPanelRoute?.AlsInfoPanelRoute(),
				MengeAbovemainMessage = MemoryMeasurement.AbovemainMessage?.Select(AlsMessage)?.ToArray(),
				MengeTarget = MemoryMeasurement.Target?.Select(AlsTarget)?.ToArray(),
				MengeWindowAgentBrowser = MemoryMeasurement?.WindowAgentBrowser?.Select(MemoryStructMapAgent.AlsWindowAgentBrowser)?.ToArray(),
				MengeWindowAgentDialogue = MemoryMeasurement?.WindowAgentDialogue?.Select(MemoryStructMapAgent.AlsWindowAgentDialogue)?.ToArray(),
				NeocomButtonInventory = ButtonInventory.AlsGbsElement(),
				ShipUi = MemoryMeasurement.ShipUi.AlsShipUi(),

				MengeWindowSonstige =
				new[] {
					MemoryMeasurement.WindowOther,
					MemoryMeasurement.WindowChatChannel,
				}.ConcatNullable()?.Select(AlsWindowUpcast)?.ToArray(),

				NeocomClock = MemoryMeasurement?.Neocom?.Clock?.AlsGbsElementMitBescriftung(),
				MengeWindowInventory = MemoryMeasurement?.WindowInventory?.Select(AlsWindowInventory)?.ToArray(),
				MengeWindowTelecom = MemoryMeasurement?.WindowTelecom?.Select(AlsWindowTelecom)?.ToArray(),
				UtilmenuMission = MemoryMeasurement.UtilmenuMission?.AlsUtilmenuMission(),
				MengeWindowStack = MemoryMeasurement?.WindowStack?.Select(AlsWindowStack)?.ToArray(),
				WindowSelectedItemView = MemoryMeasurement?.WindowSelectedItemView?.FirstOrDefault()?.AlsWindowSelectedItemView(),
				WindowFittingWindow = MemoryMeasurement.WindowFittingWindow?.FirstOrDefault()?.AlsFittingWindow(),
				WindowFittingMgmt = MemoryMeasurement.WindowFittingMgmt?.FirstOrDefault()?.AlsWindowFittingMgmt(),
				NeocomClockZaitKalenderModuloTaagMinMax = MemoryMeasurement?.Neocom?.Clock?.Label?.ZaitMinMaxAusZaitKalenderString(),
				ModuleButtonHint = MemoryMeasurement?.ModuleButtonTooltip?.AlsModuleButtonHint(),
				WindowDroneView = MemoryMeasurement?.WindowDroneView?.FirstOrDefault()?.AlsWindowDroneView(),
				WindowStationLobby = MemoryMeasurement?.WindowStationLobby?.FirstOrDefault()?.AlsWindowStationLobby(),
				SelfShipState = MemoryMeasurement?.ShipState(),
				WindowSurveyScanView = MemoryMeasurement.WindowSurveyScanView?.FirstOrDefault()?.AlsWindowSurvesScanView(),
				MengeAbovemainPanelEveMenu = MemoryMeasurement?.AbovemainPanelEveMenu?.Select(AlsPanelGroup)?.ToArray(),
				MengeAbovemainPanelGroup = MemoryMeasurement?.AbovemainPanelGroup?.Select(AlsPanelGroup)?.ToArray(),
				WindowOverview = MemoryMeasurement?.WindowOverview?.FirstOrDefault()?.AlsWindowOverview(),
			};
		}

		static public VonSensor.OverviewZaile AlsOverviewZaile(this MemoryStruct.OverviewEntry Entry)
		{
			if (null == Entry)
			{
				return null;
			}

			var CellValue = new Func<string, string>(ColumnHeaderLabelPattern =>
			Entry?.ListColumnCellLabel?.FirstOrDefault(k => Regex.Match(k.Key?.Label ?? "", ColumnHeaderLabelPattern, RegexOptions.IgnoreCase).Success).Value);

			var DistanceSictString = CellValue("distance");

			var Name = CellValue("Name");

			var Type = CellValue("Type");

			Int64? DistanceMin;
			Int64? DistanceMax;

			TempAuswertGbs.Extension.AusDistanceSictStringAbbildGanzzaalDurcMeeter(
				DistanceSictString, out DistanceMin, out DistanceMax);

			var SpriteMatch = new Func<string, MemoryStruct.Sprite>(Pattern => Entry?.SetSprite?.FirstOrDefault(k => Regex.Match(k?.Name ?? "", Pattern, RegexOptions.IgnoreCase).Success));

			var IconMain = SpriteMatch("iconSprite");

			var IconTargeting = SpriteMatch("targeting");
			var IconTargetedByMe = SpriteMatch("targetedByMeIndicator");
			var IconMyActiveTarget = SpriteMatch("myActiveTargetIndicator");
			var IconAttackingMe = SpriteMatch("attackingMe");
			var IconHostile = SpriteMatch("Hostile");

			return new VonSensor.OverviewZaile(Entry.AlsGbsElement(),
				Entry?.IsSelected,
				DistanceMin,
				DistanceMax,
				Name,
				Type,
				IconMain?.Texture0Id?.AlsObjektMitIdentInt64().AlsGbsElement(),
				IconMain?.Color?.AlsFarbeARGB(),
				null != IconTargeting,
				null != IconTargetedByMe,
				null != IconMyActiveTarget,
				null != IconAttackingMe,
				null != IconHostile);
		}

		static public int MengeZaileAnzaalLaageAbwaicendVonSortedDistanceAufsctaigendBerecne(
			IEnumerable<VonSensor.OverviewZaile> AusTabListeZaileOrdnetNaacLaage)
		{
			int MengeZaileAnzaalLaageAbwaicendVonSortedDistanceAufsctaigend = 0;

			if (null == AusTabListeZaileOrdnetNaacLaage)
			{
				return 0;
			}

			Int64 BisherDistanceScranke = -1;

			foreach (var Zaile in AusTabListeZaileOrdnetNaacLaage)
			{
				if (null == Zaile)
				{
					continue;
				}

				var ZaileDistance = Zaile.DistanceMax;

				if (!ZaileDistance.HasValue)
				{
					continue;
				}

				if (ZaileDistance.HasValue)
				{
					BisherDistanceScranke = Math.Max(BisherDistanceScranke, ZaileDistance.Value);
				}

				if (!((BisherDistanceScranke * 95) / 100 - 1000 <= ZaileDistance))
				{
					++MengeZaileAnzaalLaageAbwaicendVonSortedDistanceAufsctaigend;
				}
			}

			return MengeZaileAnzaalLaageAbwaicendVonSortedDistanceAufsctaigend;
		}

		/*
		 * 2013.08.18 Bsp:
		 * "Overview (mission)"
		 * */
		static readonly string AusHeaderCaptionTextTypeSelectionNameRegexPattern = Regex.Escape("Overview (") + "([^" + Regex.Escape(")") + "]+)";

		static public VonSensor.WindowOverView AlsWindowOverview(this MemoryStruct.WindowOverView Window)
		{
			if (null == Window)
			{
				return null;
			}

			string TypeSelectionName = null;

			var TypeSelectionNameMatch = Regex.Match(Window?.Caption ?? "", AusHeaderCaptionTextTypeSelectionNameRegexPattern, RegexOptions.IgnoreCase);

			if (TypeSelectionNameMatch.Success)
			{
				TypeSelectionName = TypeSelectionNameMatch.Groups[1].Value;
			}

			var ListeTab = Window?.PresetTab?.Select(AlsTab)?.ToArray();

			var TabSelected =
				ListeTab?.FirstOrDefault(KandidataSelected => ListeTab.Except(KandidataSelected.Yield()).All(t => t.LabelColorOpazitäätMili < KandidataSelected.LabelColorOpazitäätMili));

			var ListeTabNuzbar = ListeTab?.Where(k => !string.Equals("+", k.LabelBescriftung?.Trim(), StringComparison.InvariantCultureIgnoreCase))?.ToArray();

			var Scroll = Window?.ListViewport?.Scroll?.AlsScroll();

			var MeldungMengeZaileLeer = Window?.ViewportOverallLabelString?.ToLowerInvariant().Contains("no");

			var TabGroup = new VonSensor.TabGroup(ListeTab, TabSelected);

			var AusTabListeZaileOrdnetNaacLaage =
				Window?.ListViewport?.Entry?.Cast<MemoryStruct.OverviewEntry>()?.Select(AlsOverviewZaile)?.ToArray();

			var MengeZaileAnzaalLaageAbwaicendVonSortedDistanceAufsctaigend =
				MengeZaileAnzaalLaageAbwaicendVonSortedDistanceAufsctaigendBerecne(AusTabListeZaileOrdnetNaacLaage);

			return new VonSensor.WindowOverView(
				Window.AlsWindowNurBase(),
				TypeSelectionName,
				TabGroup,
				ListeTabNuzbar,
				Scroll,
				AusTabListeZaileOrdnetNaacLaage,
				MeldungMengeZaileLeer,
				MengeZaileAnzaalLaageAbwaicendVonSortedDistanceAufsctaigend);
		}

		static public VonSensor.PanelGroup AlsPanelGroup(this MemoryStruct.PanelGroup PanelGroup) =>
			null == PanelGroup ? null :
			new VonSensor.PanelGroup(PanelGroup.AlsGbsElement());

		const string RegexGrupeInKlamerNaame = "inklamer";

		/// <summary>
		/// 2014.09.08	Bsp:
		/// "Personal Locations [7]"
		/// "Drones in Bay (4)"
		/// </summary>
		const string GroupBescriftungZerleegungRegexPattern = "([^\\(\\[]+)((\\(|\\[)(?<" + RegexGrupeInKlamerNaame + ">[^\\)\\]]+)(\\)|\\])|)";

		static public void GroupBescriftungZerleegung(
			string Bescriftung,
			out string BescriftungTailTitel,
			out string BescriftungTailInKlamer,
			out int? BescriftungTailQuantitäät)
		{
			BescriftungTailTitel = null;
			BescriftungTailInKlamer = null;
			BescriftungTailQuantitäät = null;

			if (null == Bescriftung)
			{
				return;
			}

			var Match = Regex.Match(Bescriftung, GroupBescriftungZerleegungRegexPattern);

			if (!Match.Success)
			{
				BescriftungTailTitel = Bescriftung.TrimNullable();
				return;
			}

			BescriftungTailTitel = Match.Groups[1].Value.TrimNullable();
			BescriftungTailInKlamer = Match.Groups[RegexGrupeInKlamerNaame].Value.TrimNullable();

			BescriftungTailQuantitäät = (int?)TempAuswertGbs.Extension.SctandardNumberFormatZaalBetraag(BescriftungTailInKlamer);
		}

		static public string ZeleStringZuHeaderMitBescriftungRegex(
			IEnumerable<KeyValuePair<VonSensor.ScrollHeaderInfoFürItem, string>> ListeZuHeaderZeleString,
			Regex HeaderBescriftungRegex)
		{
			if (null == HeaderBescriftungRegex)
			{
				return null;
			}

			if (null == ListeZuHeaderZeleString)
			{
				return null;
			}

			foreach (var ZuHeaderZeleString in ListeZuHeaderZeleString)
			{
				if (null == ZuHeaderZeleString.Key.Bescriftung)
				{
					continue;
				}

				var Match = HeaderBescriftungRegex.Match(ZuHeaderZeleString.Key.Bescriftung);

				if (Match.Success)
				{
					return ZuHeaderZeleString.Value;
				}
			}

			return null;
		}

		static public VonSensor.ScrollHeaderInfoFürItem AlsScrollHeader(this MemoryStruct.ListColumnHeader Header)
		{
			if (null == Header)
			{
				return default(VonSensor.ScrollHeaderInfoFürItem);
			}
            
			return new VonSensor.ScrollHeaderInfoFürItem(Header.ColumnIndex ?? -1, Header.Label, (int)Header.Region.Min0, (int)Header.Region.Max0);
		}

		static readonly public Regex ColumnHeaderNameRegex = new Regex("Name", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		static readonly public Regex ColumnHeaderDistanceRegex = new Regex("Dist", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		static readonly public Regex ColumnHeaderQuantityRegex = new Regex("Quant", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		static readonly public Regex ColumnHeaderVolume = new Regex("Volum", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		static readonly public Regex ColumnHeaderOreTypRegex = new Regex("Ore", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		static public VonSensor.ScnapscusListEntry AlsScnapscusListEntry(this MemoryStruct.ListEntry Entry)
		{
			if (null == Entry)
			{
				return null;
			}

			var Bescriftung = Entry?.Label?.Label;

			string BescriftungTailTitel;
			string BescriftungTailInKlamer;
			int? BescriftungTailQuantitäät;

			GroupBescriftungZerleegung(Bescriftung, out BescriftungTailTitel, out BescriftungTailInKlamer, out BescriftungTailQuantitäät);

			var ListeZuHeaderZeleString =
				Entry?.ListColumnCellLabel?.Select(t => new KeyValuePair<VonSensor.ScrollHeaderInfoFürItem, string>(t.Key.AlsScrollHeader(), t.Value))?.ToArray();

			Int64? OreVolumeMili = null;

			var Name = ZeleStringZuHeaderMitBescriftungRegex(ListeZuHeaderZeleString, ColumnHeaderNameRegex);

			var QuantitySictString = ZeleStringZuHeaderMitBescriftungRegex(ListeZuHeaderZeleString, ColumnHeaderQuantityRegex);

			var Quantity = TempAuswertGbs.Extension.SctandardNumberFormatZaalBetraag(QuantitySictString);

			var DistanceSictString = ZeleStringZuHeaderMitBescriftungRegex(ListeZuHeaderZeleString, ColumnHeaderDistanceRegex);

			var Distance = TempAuswertGbs.Extension.AusDistanceSictStringAbbildGanzzaalDurcMeeterScrankeMin(DistanceSictString);

			var OreTypSictString = ZeleStringZuHeaderMitBescriftungRegex(ListeZuHeaderZeleString, ColumnHeaderOreTypRegex);

			if (OreTypSictString.NullOderLeer())
			{
				if (TempAuswertGbs.Extension.OreTypBerecneAusOreTypSictString(BescriftungTailTitel).HasValue)
				{
					OreTypSictString = BescriftungTailTitel;
				}
			}

			var OreTypSictEnum = TempAuswertGbs.Extension.OreTypBerecneAusOreTypSictString(OreTypSictString);

			var VolumeSictString = ZeleStringZuHeaderMitBescriftungRegex(ListeZuHeaderZeleString, ColumnHeaderVolume);

			var VolumeMili = TempAuswertGbs.Extension.SctandardNumberFormatZaalBetraagMili(VolumeSictString);

			if (OreTypSictEnum.HasValue && Quantity.HasValue)
			{
				OreVolumeMili = (WorldConfig.FürOreTypVolumeMili(OreTypSictEnum.Value) * Quantity);
			}

			return new VonSensor.ScnapscusListEntry(
				Entry.AlsGbsElement(),
				Entry.ContentBoundLeft,
				Bescriftung,
				BescriftungTailTitel,
				BescriftungTailQuantitäät,
				Entry.IsGroup,
				Entry?.GroupExpander?.AlsGbsElement(),
				Entry?.IsExpanded,
				Entry?.Label?.AlsGbsElement(),
				ListeZuHeaderZeleString,
				Name,
				DistanceSictString,
				Distance,
				QuantitySictString,
				Quantity,
				VolumeMili,
				OreTypSictString,
				OreTypSictEnum,
				OreVolumeMili);
		}

		static public VonSensor.ScnapscusListGrouped AlsListGrouped(this MemoryStruct.ListViewport List)
		{
			if (null == List)
			{
				return null;
			}

			return new VonSensor.ScnapscusListGrouped(
				List.AlsGbsElement(),
				List.ColumnHeader?.Select(AlsHeader)?.ToArray(),
				List.Entry?.Select(AlsScnapscusListEntry)?.ToArray(),
				List.Scroll?.AlsScroll());
		}

		static public VonSensor.WindowSurveyScanView AlsWindowSurvesScanView(this MemoryStruct.WindowSurveyScanView Window)
		{
			if (null == Window)
			{
				return null;
			}

			return new VonSensor.WindowSurveyScanView(
				Window.AlsWindowNurBase(),
				Window.ListViewport.AlsListGrouped(),
				Window.Button?.FirstOrDefault(k => Regex.Match(k?.Label ?? "", "clear", RegexOptions.IgnoreCase).Success).AlsGbsElementMitBescriftung());
		}

		static public Int64? SpeedMilimeeterProSekunde(this string SpeedLabel)
		{
			if (null == SpeedLabel)
			{
				return null;
			}

			Int64? ShipSpeedDurcMeeterProSekunde = null;
			int? SpeedNeedleAussclaagMili = null;
			bool? ShipWarping = null;

			/*
			 * 2013.07.14
			 * Bsp: "<center>140 m/s"
			 * Bsp: "<center>98.1 m/s"
			 * */
			var ShipSpeedMatch = Regex.Match(SpeedLabel, "(\\d+)(" + Regex.Escape(".") + "\\d+)?\\s*" + Regex.Escape("m/s"));

			if (ShipSpeedMatch.Success)
			{
				var SpeedTailVorDezimaaltrenzaice = ShipSpeedMatch.Groups[1].Value;

				ShipSpeedDurcMeeterProSekunde = SpeedTailVorDezimaaltrenzaice?.TryParseInt();
			}

			var ShipWarpingMatch = Regex.Match(SpeedLabel, "warping", RegexOptions.IgnoreCase);

			ShipWarping = ShipWarpingMatch.Success;

			return ShipSpeedDurcMeeterProSekunde * 1000;
		}

		static public ShipState ShipState(this MemoryStruct.MemoryMeasurement Measurement)
		{
			if (null == Measurement)
			{
				return null;
			}

			var SelbstShipZuusctandTreferpunkte = Measurement?.ShipUi?.Hitpoints?.AlsHitpoints();

			bool? SelbstShipZuusctandDocked = null;

			if (0 < Measurement?.WindowStationLobby?.Length)
			{
				SelbstShipZuusctandDocked = true;
			}

			if (null != Measurement?.ShipUi?.Center ||
				0 < Measurement?.WindowOverview?.Length)
			{
				SelbstShipZuusctandDocked = false;
			}

			var IndicationMatch = new Func<string, bool>(Pattern =>
			Measurement?.ShipUi?.Indication?.ListLabelString?.Any(l => Regex.Match(l?.Label ?? "", Pattern, RegexOptions.IgnoreCase).Success) ?? false);

			var SelbstShipZuusctandDocking = IndicationMatch("docking");

			var SelbstShipZuusctandWarping = IndicationMatch("warp");

			var SelbstShipZuusctandJumping = IndicationMatch("jump");

			bool? SelbstShipZuusctandCloaked = null;

			var SelbstShipZuusctandUnDocking = Measurement?.WindowStationLobby?.Any(w => w?.AlsWindowStationLobby()?.UnDocking ?? false) ?? false;

			bool? SelbstShipZuusctandJammed = null;

			foreach (var EWarElementErgeebnis in (Measurement?.ShipUi?.EWarElement?.Select(AlsEWarElement)).EmptyIfNull())
			{
				if (null == EWarElementErgeebnis)
				{
					continue;
				}

				if (SictEWarTypeEnum.Jam == EWarElementErgeebnis.EWarTypeEnum)
				{
					SelbstShipZuusctandJammed = true;
				}
			}

			var SelbstShipCargoHoldCapacity = default(CargoCapacityInfo);
			var SelbstShipOreHoldCapacity = default(CargoCapacityInfo);
			var SelbstShipDroneBayCapacity = default(CargoCapacityInfo);

			Measurement?.WindowInventory?.Select(AlsWindowInventory).ForEachNullable((WindowInventory) =>
			{
				if (null == WindowInventory)
				{
					return;
				}

				var WindowInventoryZuAuswaalReczMengeKandidaatLinxTreeEntry =
					WindowInventory.ZuAuswaalReczMengeKandidaatLinxTreeEntry;

				if (!(1 == WindowInventoryZuAuswaalReczMengeKandidaatLinxTreeEntry.CountNullable()))
				{
					return;
				}

				var LinxTreeEntryActiveShip = WindowInventory.LinxTreeEntryActiveShip;
				var ZuAuswaalReczLinxTreeEntry = WindowInventoryZuAuswaalReczMengeKandidaatLinxTreeEntry.First();
				var WindowInventoryAuswaalReczCapacity = WindowInventory.AuswaalReczCapacity;

				if (null != WindowInventoryAuswaalReczCapacity &&
					null != LinxTreeEntryActiveShip &&
					null != ZuAuswaalReczLinxTreeEntry)
				{
					var WindowInventoryAuswaalReczCapacityAlsCargoCapacityInfo =
						new CargoCapacityInfo(
							WindowInventoryAuswaalReczCapacity.MaxMikro / 1000,
							WindowInventoryAuswaalReczCapacity.UsedMikro / 1000);

					var TreeEntryCargoHold = Optimat.EveOnline.Extension.TreeEntryBerecneAusCargoTyp(LinxTreeEntryActiveShip, SictShipCargoTypSictEnum.General);
					var TreeEntryOreHold = Optimat.EveOnline.Extension.TreeEntryBerecneAusCargoTyp(LinxTreeEntryActiveShip, SictShipCargoTypSictEnum.OreHold);
					var TreeEntryDroneBay = Optimat.EveOnline.Extension.TreeEntryBerecneAusCargoTyp(LinxTreeEntryActiveShip, SictShipCargoTypSictEnum.DroneBay);

					if (ZuAuswaalReczLinxTreeEntry == TreeEntryCargoHold)
					{
						SelbstShipCargoHoldCapacity = WindowInventoryAuswaalReczCapacityAlsCargoCapacityInfo;
					}

					if (ZuAuswaalReczLinxTreeEntry == TreeEntryOreHold)
					{
						SelbstShipOreHoldCapacity = WindowInventoryAuswaalReczCapacityAlsCargoCapacityInfo;
					}

					if (ZuAuswaalReczLinxTreeEntry == TreeEntryDroneBay)
					{
						SelbstShipDroneBayCapacity = WindowInventoryAuswaalReczCapacityAlsCargoCapacityInfo;
					}
				}
			});

			return
				new ShipState(
					SelbstShipZuusctandTreferpunkte,
					Measurement?.ShipUi?.SpeedLabel?.Label?.SpeedMilimeeterProSekunde() / 1000,
					SelbstShipZuusctandDocked,
					SelbstShipZuusctandDocking,
					SelbstShipZuusctandWarping,
					SelbstShipZuusctandJumping,
					SelbstShipZuusctandCloaked,
					SelbstShipZuusctandJammed,
					SelbstShipZuusctandUnDocking,
					SelbstShipCargoHoldCapacity,
					SelbstShipOreHoldCapacity,
					SelbstShipDroneBayCapacity);
		}

		static Regex ButtonFittingRegex = @"fitting\.".AsRegexCompiledIgnoreCase();

		static public VonSensor.WindowStationLobby AlsWindowStationLobby(this MemoryStruct.WindowStationLobby Window)
		{
			if (null == Window)
			{
				return null;
			}

			bool? UnDocking = null;

			var LabelMatch = new Func<string, bool>(Pattern => Window?.Label?.Any(t => Regex.Match(t?.Label ?? "", Pattern, RegexOptions.IgnoreCase).Success) ?? false);

			if (LabelMatch("abort undock") || LabelMatch("undocking"))
			{
				UnDocking = true;
			}

			var MengeAgentEntry = Window?.AgentEntry?.Select(Entry => Entry.AlsAgentEntry(Window?.AgentEntryHeader))?.ToArray();

			var ButtonFitting = Window?.ServiceButton?.FirstOrDefault(k => ButtonFittingRegex.MatchSuccess(k?.TexturePath));

            return new VonSensor.WindowStationLobby(
				Window.AlsWindowNurBase(),
				Window.ButtonUndock.AlsGbsElement(),
				ButtonFitting.AlsGbsElement(),
				MengeAgentEntry,
				UnDocking);
		}

		/*
		 * 2013.07.30
		 * Bsp: "Level I - Security"
		 * */
		static string AusAgentEntryTextAgentTypUndLevelRegexPattern = "Level\\s+([IV]{1,3})\\s*" + Regex.Escape("-") + "\\s*(\\w+)";

		static string[] AgentLevelString = new string[] { null, "I", "II", "III", "IV", "V" };

		static KeyValuePair<string, int?>[] ListeLevelStringMitLevel =
			AgentLevelString
			.Select((String, Index) => new KeyValuePair<string, int?>(String, Index))
			.ToArray();

		static public KeyValuePair<string, int>? AgentTypUndLevel(string AusAgentEntryText)
		{
			if (null == AusAgentEntryText)
			{
				return null;
			}

			var Match = Regex.Match(AusAgentEntryText, AusAgentEntryTextAgentTypUndLevelRegexPattern, RegexOptions.IgnoreCase);

			if (!Match.Success)
			{
				return null;
			}

			var Typ = Match.Groups[2].Value;

			var LevelSictString = Match.Groups[1].Value;

			var Level =
				ListeLevelStringMitLevel
				.FirstOrDefault((Kandidaat) => string.Equals(Kandidaat.Key, LevelSictString, StringComparison.InvariantCultureIgnoreCase)).Value;

			return new KeyValuePair<string, int>(Typ, Level ?? -1);
		}

		static public VonSensor.LobbyAgentEntry AlsAgentEntry(
			this MemoryStruct.LobbyAgentEntry Entry,
			IEnumerable<MemoryStruct.UIElementLabelString> AgentEntryHeader)
		{
			if (null == Entry)
			{
				return null;
			}

			string AgentName = null;
			string AgentTyp = null;
			int? AgentLevel = null;
			string ZaileTypUndLevelText = null;

			var ListLabel = Entry?.ListLabelString;

			if (null != ListLabel)
			{
				if (1 < ListLabel.Length)
				{
					AgentName = ListLabel.FirstOrDefault().Label;
					ZaileTypUndLevelText = ListLabel.LastOrDefault().Label;
				}
			}

			var TypUndLevel = AgentTypUndLevel(ZaileTypUndLevelText);

			if (TypUndLevel.HasValue)
			{
				AgentTyp = TypUndLevel.Value.Key;
				AgentLevel = TypUndLevel.Value.Value;
			}

			var Header =
				AgentEntryHeader?.Where(k => k.Region.ZentrumLaage.B < Entry.Region.ZentrumLaage.B)
				?.OrderBy(k => k.Region.ZentrumLaage.B)
				?.LastOrDefault();

			return new VonSensor.LobbyAgentEntry(Entry.AlsGbsElement(), AgentName, AgentTyp, AgentLevel, ZaileTypUndLevelText, Entry.ButtonStartConversation.AlsGbsElement())
			{
				HeaderText = Header?.Label,
			};
		}

		static VonSensor.DroneEntryStatusSictEnum? DroneStatusSictEnum(string DroneStatusSictString)
		{
			if (null == DroneStatusSictString)
			{
				return null;
			}

			return Bib3.Extension.EnumParseNullable<VonSensor.DroneEntryStatusSictEnum>(DroneStatusSictString, true);
		}

		static public VonSensor.WindowDroneViewEntry AlsEntry(this MemoryStruct.DroneViewEntryItem Entry)
		{
			if (null == Entry)
			{
				return null;
			}

			/*
			string LabelText = null;
			string DroneName = Entry?.DroneName;
			string DroneStatusSictStringAbbild = Entry?.StatusString;

			var Match = Regex.Match(DroneName ?? "", DroneEntryLabelRegexPattern, RegexOptions.IgnoreCase);

			if (Match.Success)
			{
				LabelText = Entry?.Label?.Label;

				DroneName = Bib3.Glob.TrimNullable(Match.Groups[1].Value);

				var StatusMitFormat = Match.Groups[2].Value;

				DroneStatusSictStringAbbild = Bib3.Glob.TrimNullable(Optimat.Glob.StringEntferneMengeXmlTag(StatusMitFormat));
			}
			*/

			var DroneLabel = Entry?.Label?.Label?.AsDroneLabel();

			var DroneName = DroneLabel?.Name;
			var DroneStatus = DroneLabel?.Status;

			var DroneStatusEnum = DroneStatusSictEnum(DroneStatus);

			return new VonSensor.WindowDroneViewEntry(Entry.AlsGbsElement(), DroneName, Entry.Hitpoints.AlsHitpoints(), DroneStatus, DroneStatusEnum);
		}

		static readonly string HeaderLabelTextRegexPattern = Regex.Escape("(") + "\\s*([\\d]+)\\s*" + Regex.Escape(")");

		static public VonSensor.WindowDroneViewGrupe AlsGrupe(
			this KeyValuePair<MemoryStruct.DroneViewEntryGroup, MemoryStruct.DroneViewEntryItem[]> Group)
		{
			if (null == Group.Key)
			{
				return null;
			}

			var HeaderLabelText = Group.Key.Caption?.Label;

			int? MengeEntryAnzaal = null;

			var HeaderLabelTextMatch = Regex.Match(HeaderLabelText ?? "", HeaderLabelTextRegexPattern, RegexOptions.IgnoreCase);

			if (HeaderLabelTextMatch.Success)
			{
				MengeEntryAnzaal = ExtractFromOldAssembly.Bib3.Glob.TryParseInt(HeaderLabelTextMatch.Groups[1].Value);
			}

			var Header = Group.Key.Caption.AlsGbsElementMitBescriftung();

			var MengeEntryDrone = Group.Value?.Where(Entry => !Regex.Match(Entry?.Label?.Label ?? Entry?.Label?.Label ?? "", @"no\s*item", RegexOptions.IgnoreCase).Success)?.ToArray();

			var MengeEntryDroneAbbild = MengeEntryDrone?.Select(AlsEntry)?.ToArray();

			return new VonSensor.WindowDroneViewGrupe(
				Group.Key.AlsGbsElement(),
				Header,
				Header,
				MengeEntryAnzaal,
				MengeEntryDroneAbbild)
			{
			};
		}

		static public VonSensor.WindowDroneView AlsWindowDroneView(this MemoryStruct.WindowDroneView Window)
		{
			if (null == Window)
			{
				return null;
			}

			var ListGroup = Window.ListViewport?.Entry?.ListDroneViewEntryGrouped();
			var ListeGrupe = ListGroup?.Select(AlsGrupe)?.ToArray();

			var GrupeDronesInBay = ListeGrupe?.FirstOrDefault(k => Regex.Match(k?.Header?.Bescriftung ?? "", @"in\s+bay", RegexOptions.IgnoreCase).Success);
			var GrupeDronesInLocalSpace = ListeGrupe?.FirstOrDefault(k => Regex.Match(k?.Header?.Bescriftung ?? "", @"in\s+Local\s+Space", RegexOptions.IgnoreCase).Success);

			return new VonSensor.WindowDroneView(Window.AlsWindowNurBase())
			{
				ListeGrupe = ListeGrupe,
				GrupeDronesInBay = GrupeDronesInBay,
				GrupeDronesInLocalSpace = GrupeDronesInLocalSpace,
			};
		}

		static public VonSensor.ModuleButtonHint AlsModuleButtonHint(this MemoryStruct.ModuleButtonTooltip Tooltip)
		{
			if (null == Tooltip)
			{
				return null;
			}

			var ListeZaile = Tooltip?.ListRow?.Select(AlsZaile)?.OrderBy(Zaile => Zaile?.InGbsFläceMiteLaageB() ?? int.MaxValue)?.ToArray();

			return
				new VonSensor.ModuleButtonHint(Tooltip.AlsGbsElement(), ListeZaile, ListeZaile?.FirstOrDefault());
		}

		/// <summary>
		/// 2014.04.12	Bsp:
		/// "F1"
		/// "ALT-F1"
		/// </summary>
		static readonly string ShortcutTextRegexPattern = "(\\w+\\s*-|)\\s*F(\\d+)";

		static public VonSensor.ModuleButtonHintZaile AlsZaile(this MemoryStruct.ModuleButtonTooltipRow Row)
		{
			var Bescriftung = string.Join(" ", Row?.ListLabelString?.Select(t => t?.Label).EmptyIfNull());

			bool? ShortcutModifierNict = null;
			int? ShortcutTasteFIndex = null;

			var ShortcutLabelText = Row?.ShortcutText;

			if (null != ShortcutLabelText)
			{
				var ShortcutMatch = Regex.Match(ShortcutLabelText, ShortcutTextRegexPattern, RegexOptions.IgnoreCase);

				if (ShortcutMatch.Success)
				{
					ShortcutModifierNict = ShortcutMatch.Groups[1].Value.Length < 1;

					ShortcutTasteFIndex = ExtractFromOldAssembly.Bib3.Glob.TryParseInt(ShortcutMatch.Groups[2].Value);
				}
			}

			var BescriftungMiinusFormat = Optimat.Glob.StringEntferneMengeXmlTag(Bescriftung);

			return new VonSensor.ModuleButtonHintZaile(new VonSensor.GbsElementMitBescriftung(Row.AlsGbsElement(), Bescriftung),
				Row.IconTextureId?.Select(t => t.AlsObjektMitIdentInt64().AlsGbsElement())?.ToArray(),
				BescriftungMiinusFormat,
				ShortcutLabelText,
				ShortcutModifierNict,
				ShortcutTasteFIndex);
		}

		/// <summary>
		/// 2014.05.29	Bsp:
		/// "19:31"
		/// </summary>
		static readonly string ZaitKalenderRegexPattern = "(\\d{2})\\D+(\\d{2})";

		static public KeyValuePair<int, int>? ZaitMinMaxAusZaitKalenderString(this string ZaitKalenderString)
		{
			if (null == ZaitKalenderString)
			{
				return null;
			}

			var Match = Regex.Match(ZaitKalenderString, ZaitKalenderRegexPattern);

			if (!Match.Success)
			{
				return null;
			}

			var SctundeString = Match.Groups[1].Value;
			var MinuuteString = Match.Groups[2].Value;

			var Sctunde = ExtractFromOldAssembly.Bib3.Glob.TryParseInt(SctundeString);
			var Minuute = ExtractFromOldAssembly.Bib3.Glob.TryParseInt(MinuuteString);

			if (!Sctunde.HasValue || !Minuute.HasValue)
			{
				return null;
			}

			var Min = (((Sctunde * 60) + Minuute) * 60).Value;
			var Max = Min + 59;

			return new KeyValuePair<int, int>(Min, Max);
		}

		static public VonSensor.WindowFittingMgmt AlsWindowFittingMgmt(this MemoryStruct.WindowFittingMgmt Window)
		{
			if (null == Window)
			{
				return null;
			}

			return new VonSensor.WindowFittingMgmt(Window.AlsWindowNurBase())
			{
				MengeFittingEntry = Window?.FittingViewport?.Entry?.Select(AlsFittingEntry)?.ToArray(),
			};
		}

		static public VonSensor.FittingEntry AlsFittingEntry(this MemoryStruct.ListEntry Entry) =>
			null == Entry ? null :
			new VonSensor.FittingEntry(new VonSensor.GbsElementMitBescriftung(Entry.AlsGbsElement(), Entry?.Label?.Label));

		static public VonSensor.WindowFittingWindow AlsFittingWindow(this MemoryStruct.WindowFittingWindow Window) =>
			null == Window ? null :
			new VonSensor.WindowFittingWindow(Window.AlsWindowNurBase(),
				Window.Button?.FirstOrDefault(k => Regex.Match(k?.Label ?? "", "browse", RegexOptions.IgnoreCase).Success)?.AlsGbsElementMitBescriftung());

		static public VonSensor.WindowSelectedItemView AlsWindowSelectedItemView(this MemoryStruct.WindowSelectedItemView Window) =>
			null == Window ? null :
			new VonSensor.WindowSelectedItemView(Window.AlsWindowNurBase(), null, null, null);

		static public VonSensor.WindowStack AlsWindowStack(this MemoryStruct.WindowStack WindowStack) =>
			null == WindowStack ? null :
			new VonSensor.WindowStack(WindowStack.AlsWindowNurBase(), new VonSensor.TabGroup(WindowStack.Tab?.Select(AlsTab)?.ToArray()), WindowStack?.TabSelectedWindow?.AlsWindowUpcast());

		static public VonSensor.Tab AlsTab(this MemoryStruct.Tab Tab) =>
			null == Tab ? null :
			new VonSensor.Tab(Tab.AlsGbsElement(), Tab.Label?.AlsGbsElementMitBescriftung(), Tab.LabelColorOpacityMilli);

		static public VonSensor.UtilmenuMissionInfo AlsUtilmenuMission(this MemoryStruct.UtilmenuMission Utilmenu) =>
			null == Utilmenu ? null :
			new VonSensor.UtilmenuMissionInfo(
				Utilmenu.AlsGbsElement(),
				Utilmenu.Header.AlsGbsElementMitBescriftung(),
				Utilmenu.ButtonReadDetails.AlsGbsElementMitBescriftung(),
				Utilmenu.ButtonStartConversation.AlsGbsElementMitBescriftung(),
				Utilmenu.Location?.Select(AlsLocation)?.ToArray());

		static public VonSensor.UtilmenuMissionLocationInfo AlsLocation(this MemoryStruct.UtilmenuMissionLocationInfo Location) =>
			null == Location ? null :
			new VonSensor.UtilmenuMissionLocationInfo(
				Location.AlsGbsElement())
			{
				KnopfApproach = Location.ButtonApproach.AlsGbsElementMitBescriftung(),
				KnopfDock = Location.ButtonDock.AlsGbsElementMitBescriftung(),
				KnopfLocation = Location.ButtonLocation.AlsGbsElementMitBescriftung(),
				KnopfSetDestination = Location.ButtonSetDestination.AlsGbsElementMitBescriftung(),
				KnopfWarpTo = Location.ButtonWarpTo.AlsGbsElementMitBescriftung(),
				HeaderText = Location.HeaderLabel,
			};

		static public VonSensor.WindowTelecom AlsWindowTelecom(this MemoryStruct.WindowTelecom Window) =>
			null == Window ? null :
			new VonSensor.WindowTelecom(Window.AlsWindowNurBase());

		static public string[] AuswaalReczObjektPfaadListeAstBerecneAusPfaadSictString(
			string PfaadSictString)
		{
			if (null == PfaadSictString)
			{
				return null;
			}

			return
				PfaadSictString
				.Split(new string[] { ">" }, StringSplitOptions.RemoveEmptyEntries)
				.Select((Ast) => Ast.Trim())
				.Where((Ast) => !Bib3.Extension.NullOderLeer(Ast))
				.ToArray();
		}

		static public VonSensor.WindowInventoryPrimary AlsWindowInventory(this MemoryStruct.WindowInventory Window)
		{
			if (null == Window)
			{
				return null;
			}

			var LinxTreeListeEntry = Window.LeftTreeListEntry?.Select(AlsTreeEntry)?.ToArray();

			var LinxTreeListeEntryActiveShip =
				LinxTreeListeEntry
				?.Where(Kandidaat => Regex.Match(Kandidaat.LabelText ?? "", Regex.Escape("(") + "[^\\(]+" + Regex.Escape(")"), RegexOptions.IgnoreCase).Success)
				?.ToArray();

			var LinxTreeEntryActiveShip = LinxTreeListeEntryActiveShip?.FirstOrDefault();

			var AuswaalReczObjektPfaadSictString = Optimat.Glob.StringEntferneMengeXmlTag(Window?.SelectedRightInventoryPathLabel?.Label);

			var AuswaalReczObjektPfaadListeAst = AuswaalReczObjektPfaadListeAstBerecneAusPfaadSictString(AuswaalReczObjektPfaadSictString);

			var WindowAbbild =
				new VonSensor.WindowInventoryPrimary(
				Window.AlsWindowNurBase(),
				LinxTreeListeEntry,
				LinxTreeEntryActiveShip,
				AuswaalReczObjektPfaadSictString,
				AuswaalReczObjektPfaadListeAst,
				Window.SelectedRightInventory.AlsInventory(),
				BotEngine.EveOnline.Parse.Inventory.ParseAsInventoryCapacityGaugeMilli(Window.SelectedRightInventoryCapacity?.Label).AlsCapacity(),
				Window.SelectedRightControlViewButton?.Select(AlsGbsElement)?.ToArray(),
				Window.SelectedRightFilterTextBox?.AlsGbsElement(),
				Window.SelectedRightFilterTextBox?.Label,
				Window.SelectedRightFilterButtonClear.AlsGbsElement(),
				Window.SelectedRightItemDisplayedCount,
				Window.SelectedRightItemFilteredCount);

			WindowAbbild.ZuAuswaalReczMengeKandidaatLinxTreeEntryBerecne();

			return WindowAbbild;
		}

		static public VonSensor.BasicDynamicScroll AlsScroll(this MemoryStruct.Scroll Scroll)
		{
			if (null == Scroll)
			{
				return null;
			}

			return new VonSensor.BasicDynamicScroll(new VonSensor.Scroll(
				Scroll.AlsGbsElement(),
				Scroll.ListColumnHeader?.Select(AlsHeader)?.ToArray(),
				Scroll.Clipper.AlsGbsElement(),
				Scroll.ScrollHandleBound.AlsGbsElement(),
				Scroll.ScrollHandle.AlsGbsElement()));
		}

		static public VonSensor.ListColumnHeader AlsHeader(this MemoryStruct.ListColumnHeader Header) =>
			null == Header ? null :
			new VonSensor.ListColumnHeader(
				Header.AlsGbsElement(),
				Header.AlsGbsElementMitBescriftung(),
				Header.SortDirection.HasValue ?
				0 < Header.SortDirection || Header.SortDirection < 0 : (bool?)null);

		static KeyValuePair<VonSensor.InventoryItemDetailsColumnTyp, string>[] ListeHeaderStringFürColumnTyp = ListeHeaderStringFürColumnTypBerecne();

		static KeyValuePair<VonSensor.InventoryItemDetailsColumnTyp, string>[] ListeHeaderStringFürColumnTypBerecne()
		{
			return Optimat.Glob.ListeEnumWertUndSictStringAbbildBerecne<VonSensor.InventoryItemDetailsColumnTyp>((SictEnumAbbild) => SictEnumAbbild.ToString().Replace("_", " "));
		}

		static public KeyValuePair<VonSensor.ListColumnHeader, VonSensor.InventoryItemDetailsColumnTyp?>[] ListeColumnTypAusListeHeader(
			VonSensor.ListColumnHeader[] ListeHeader)
		{
			if (null == ListeHeader)
			{
				return null;
			}

			var ListeColumnTyp = new List<KeyValuePair<VonSensor.ListColumnHeader, VonSensor.InventoryItemDetailsColumnTyp?>>();

			for (int HeaderIndex = 0; HeaderIndex < ListeHeader.Length; HeaderIndex++)
			{
				VonSensor.InventoryItemDetailsColumnTyp? ColumnTyp = null;

				var Header = ListeHeader[HeaderIndex];

				try
				{
					if (null == Header)
					{
						continue;
					}

					if (null == Header.Header)
					{
						continue;
					}

					if (null == ListeHeaderStringFürColumnTyp)
					{
						continue;
					}

					foreach (var HeaderStringFürColumnTyp in ListeHeaderStringFürColumnTyp)
					{
						if (string.Equals(HeaderStringFürColumnTyp.Value, Header.Header.Bescriftung, StringComparison.InvariantCultureIgnoreCase))
						{
							ColumnTyp = HeaderStringFürColumnTyp.Key;
							break;
						}
					}
				}
				finally
				{
					ListeColumnTyp.Add(new KeyValuePair<VonSensor.ListColumnHeader, VonSensor.InventoryItemDetailsColumnTyp?>(Header, ColumnTyp));
				}
			}

			return ListeColumnTyp.ToArray();
		}

		static public VonSensor.ListItem AlsItem(this MemoryStruct.ListEntry Entry) =>
			null == Entry ? null :
			new VonSensor.ListItem(Entry.AlsGbsElement(), Entry.ListColumnCellLabel?.Select(t => t.Value)?.ToArray(), Entry.IsSelected);

		static public NumberFormatInfo SctandardNumberFormatInfoErsctele()
		{
			var FormatInfo = CultureInfo.InvariantCulture.NumberFormat.Clone() as NumberFormatInfo;

			FormatInfo.NumberGroupSeparator = ".";
			FormatInfo.NumberDecimalSeparator = ",";

			return FormatInfo;
		}

		static readonly public NumberFormatInfo SctandardNumberFormatInfo = SctandardNumberFormatInfoErsctele();

		static public void FüleMengeFeldAusListItem(
			this VonSensor.InventoryItem InventoryItem,
			VonSensor.InventoryItemDetailsColumnTyp?[] ListeColumnTyp)
		{
			string Name = null;
			string Group = null;
			string Category = null;
			string QuantitySictStringAbbild = null;
			int? Quantity = null;

			try
			{
				var ListItem = InventoryItem.ListItem;

				if (null == ListeColumnTyp)
				{
					return;
				}

				if (null == ListItem)
				{
					return;
				}

				var ListItemListeZeleText = ListItem.ListeZeleText;

				if (null == ListItemListeZeleText)
				{
					return;
				}

				for (int ColumnIndex = 0; ColumnIndex < ListeColumnTyp.Length; ColumnIndex++)
				{
					var ColumnTyp = ListeColumnTyp[ColumnIndex];

					var ZeleText = ListItemListeZeleText.ElementAtOrDefault(ColumnIndex);

					if (!ColumnTyp.HasValue)
					{
						continue;
					}

					switch (ColumnTyp.Value)
					{
						case VonSensor.InventoryItemDetailsColumnTyp.Name:
							Name = ZeleText;
							break;
						case VonSensor.InventoryItemDetailsColumnTyp.Quantity:
							QuantitySictStringAbbild = ZeleText;
							break;
						case VonSensor.InventoryItemDetailsColumnTyp.Group:
							Group = ZeleText;
							break;
						case VonSensor.InventoryItemDetailsColumnTyp.Category:
							Category = ZeleText;
							break;
						case VonSensor.InventoryItemDetailsColumnTyp.Size:
							break;
						case VonSensor.InventoryItemDetailsColumnTyp.Slot:
							break;
						case VonSensor.InventoryItemDetailsColumnTyp.Volume:
							break;
						case VonSensor.InventoryItemDetailsColumnTyp.Meta_Level:
							break;
						case VonSensor.InventoryItemDetailsColumnTyp.Tech_Level:
							break;
						default:
							break;
					}
				}

				Quantity = ExtractFromOldAssembly.Bib3.Glob.TryParseInt(
					QuantitySictStringAbbild,
					SctandardNumberFormatInfo,
					System.Globalization.NumberStyles.Integer | System.Globalization.NumberStyles.AllowThousands);
			}
			finally
			{
				InventoryItem.Name = Name;
				InventoryItem.Group = Group;
				InventoryItem.Category = Category;
				InventoryItem.QuantitySictStringAbbild = QuantitySictStringAbbild;
				InventoryItem.Quantity = Quantity;
			}
		}

		static public VonSensor.InventoryItem AlsInventoryItem(
			this MemoryStruct.ListEntry Entry,
			VonSensor.InventoryItemDetailsColumnTyp?[] ListeHeaderColumnTyp)
		{
			if (null == Entry)
			{
				return null;
			}

			var Item =
				new VonSensor.InventoryItem(Entry.AlsItem());

			Item.FüleMengeFeldAusListItem(ListeHeaderColumnTyp);

			return Item;
		}

		static public VonSensor.Inventory AlsInventory(this MemoryStruct.Inventory Inventory)
		{
			if (null == Inventory)
			{
				return null;
			}

			var Scroll = Inventory?.ViewList?.Scroll?.AlsScroll();

			var ListeHeaderMitColumnTyp = ListeColumnTypAusListeHeader(Inventory?.ViewList?.ColumnHeader?.Select(AlsHeader)?.ToArray());

			var ListeHeaderColumnTyp = ListeHeaderMitColumnTyp?.Values()?.ToArray();

			var ListeItem = Inventory?.ViewList?.Entry?.Select(t => t.AlsInventoryItem(ListeHeaderColumnTyp))?.ToArray();

			bool? SictwaiseScaintGeseztAufListNict = null;

			return new VonSensor.Inventory(
				Inventory.AlsGbsElement(),
				ListeHeaderMitColumnTyp,
				Scroll,
				ListeItem,
				SictwaiseScaintGeseztAufListNict);
		}

		static public VonSensor.InventoryCapacityGaugeInfo AlsCapacity(this BotEngine.EveOnline.Parse.InventoryCapacityGaugeNumeric Capacity) =>
			null == Capacity ? null :
			new VonSensor.InventoryCapacityGaugeInfo(Capacity.Max * 1000, Capacity.Used * 1000, Capacity.Selected * 1000);

		/// <summary>
		/// 2014.00.00	Bsp:
		/// "Corpum Priest Wreck <color=#66FFFFFF>145 m</color>"
		/// 2014.00.07	Bsp:
		/// "Guristas Personnel Transport Wreck <color=#66FFFFFF>2.291 m</color>"
		/// 2014.09.04	Bsp:
		/// "Drone Bay"
		/// "Procure (Procurer)"
		/// </summary>
		static string TreeViewEntryLabelTextRegexPattern = "([^\\<]+)(\\<[^\\>]+\\>([\\s\\d\\w\\,\\.]+)|$)";

		static public void LabelTaileBerecne(
			this VonSensor.TreeViewEntry Entry)
		{
			string LabelTextTailObjektName = null;
			string LabelTextTailObjektDistance = null;
			Int64? ObjektDistance = null;

			try
			{
				var LabelText = Entry.LabelText;

				if (null == LabelText)
				{
					return;
				}

				var LabelTextMatch = Regex.Match(LabelText, TreeViewEntryLabelTextRegexPattern);

				if (!LabelTextMatch.Success)
				{
					return;
				}

				LabelTextTailObjektName = ExtractFromOldAssembly.Bib3.Glob.TrimNullable(LabelTextMatch.Groups[1].Value);
				LabelTextTailObjektDistance = ExtractFromOldAssembly.Bib3.Glob.TrimNullable(LabelTextMatch.Groups[3].Value);
			}
			finally
			{
				ObjektDistance = TempAuswertGbs.Extension.AusDistanceSictStringAbbildGanzzaalDurcMeeterScrankeMax(LabelTextTailObjektDistance);

				Entry.LabelTextTailObjektName = LabelTextTailObjektName;
				Entry.LabelTextTailObjektDistance = LabelTextTailObjektDistance;
				Entry.ObjektDistance = ObjektDistance;
			}
		}

		static public VonSensor.TreeViewEntry AlsTreeEntry(MemoryStruct.TreeViewEntry Entry)
		{
			var TreeViewEntry =
				new VonSensor.TreeViewEntry(
				Entry.AlsGbsElement(),
				Entry.TopContRegion.AlsGbsElement(),
				Entry.TopContLabel.AlsGbsElementMitBescriftung(),
				Entry.TopContIconType?.Id,
				Entry.TopContIconColor?.AlsFarbeARGB(),
				Entry.LabelText,
				Entry.Child?.Select(AlsTreeEntry)?.ToArray(),
				Entry.ExpandCollapseToggleRegion.AlsGbsElement())
				{
					IsSelected = Entry?.IsSelected,
				};

			TreeViewEntry.LabelTaileBerecne();

			return TreeViewEntry;
		}

		static public VonSensor.FarbeARGB AlsFarbeARGB(this BotEngine.Interface.ColorORGB Color) =>
			null == Color ? null :
			new VonSensor.FarbeARGB(
				Color.OMilli,
				Color.RMilli,
				Color.GMilli,
				Color.BMilli);

		static public VonSensor.ShipUi AlsShipUi(this MemoryStruct.ShipUi ShipUi)
		{
			if (null == ShipUi)
			{
				return null;
			}

			return new VonSensor.ShipUi(
				ShipUi.AlsGbsElement(),
				ShipUi?.Indication?.AlsIndication(),
				ShipUi.EWarElement?.Select(AlsEWarElement)?.ToArray(),
				ShipUi.Timer?.Select(AlsTimer)?.ToArray(),
				ShipUi?.ButtonSpeed0?.AlsGbsElement(),
				ShipUi?.Module?.Select(AlsModule)?.ToArray());
		}

		static public VonSensor.ShipUiModule AlsModule(this MemoryStruct.ShipUiModule Module)
		{
			if (null == Module)
			{
				return null;
			}

			var ModuleButtonFlächeToggle = new VonSensor.GbsElement(
				new ObjektMitIdentInt64(Module.Id),
				Module.Region.GrööseGeseztAngelpunktZentrum(new Vektor2DInt(30, 30)), Module.InTreeIndex);

			return new VonSensor.ShipUiModule(
				Module.AlsGbsElement(),
				ModuleButtonFlächeToggle,
				Module.ModuleButtonVisible,
				Module.ModuleButtonIconTexture.AlsObjektMitIdentInt64().AlsGbsElement(),
				Module.ModuleButtonQuantity?.TryParseInt(),
				Module.RampActive,
				Module.RampRotationMilli,
				Module.HiliteVisible,
				Module.GlowVisible,
				Module.BusyVisible);
		}

		static public VonSensor.ShipUiEWarElement AlsEWarElement(this MemoryStruct.ShipUiEWarElement EWarElement)
		{
			if (null == EWarElement)
			{
				return null;
			}

			return new VonSensor.ShipUiEWarElement(EWarElement.EWarType, EWarElement?.EWarType?.EWarTypeSictEnum(), EWarElement.IconTexture.AlsObjektMitIdentInt64().AlsGbsElement());
		}

		/// <summary>
		/// 2014.05.09	Bsp:
		/// "12,342 secs"
		/// </summary>
		static public string DauerLabelTextRegexPattern = TempAuswertGbs.Extension.SctandardZaalRegexPatternGroup + "\\s*sec";

		/// <summary>
		/// 2014.09.20	Bsp:
		/// "2,645 secs"
		/// </summary>
		const string ShipUITimerTextRegexPattern = "(\\d+)([\\.\\,](\\d+)|)";

		static public int? ZaitMiliAusShipUITimerText(string Text)
		{
			if (null == Text)
			{
				return null;
			}

			var Match = Regex.Match(Text, ShipUITimerTextRegexPattern);

			if (!Match.Success)
			{
				return null;
			}

			var VorDezimaaltrenzaice = Match.Groups[1].Value;
			var NaacDezimaaltrenzaice = Match.Groups[3].Value;

			var VorDezimaaltrenzaiceInt = VorDezimaaltrenzaice.TryParseInt();
			var NaacDezimaaltrenzaiceInt = NaacDezimaaltrenzaice.TryParseInt();

			return
				VorDezimaaltrenzaiceInt * 1000 +
				(((NaacDezimaaltrenzaiceInt ?? 0) * 1000) / (int)Math.Pow(10, NaacDezimaaltrenzaice.Length));
		}

		static public VonSensor.ShipUiTimer AlsTimer(this MemoryStruct.ShipUiTimer Timer)
		{
			if (null == Timer)
			{
				return null;
			}

			var TextListeZaile =
				Timer?.Label?.Select(Label => Label?.Label)
				?.WhereNotDefault()
				?.Select(Label => Label.Split(new string[] { "<br>" }, StringSplitOptions.None))
				?.ConcatNullable()
				?.ToArray();

			string DauerLabelText = null;

			foreach (var KandidaatDauerLabelText in TextListeZaile.EmptyIfNull())
			{
				var KandidaatDauerLabelTextMatch = Regex.Match(KandidaatDauerLabelText, DauerLabelTextRegexPattern, RegexOptions.IgnoreCase);

				if (!KandidaatDauerLabelTextMatch.Success)
				{
					continue;
				}

				DauerLabelText = KandidaatDauerLabelText;
				break;
			}

			var DauerRestMili = ZaitMiliAusShipUITimerText(DauerLabelText);

			var EWarTypSictEnum = Timer.Name.EWarTypeSictEnum();

			return new VonSensor.ShipUiTimer(TextListeZaile, DauerLabelText, DauerRestMili, EWarTypSictEnum);
		}

		static public VonSensor.ShipUiIndication AlsIndication(
			this MemoryStruct.ShipUiIndication ShipUiIndication)
		{
			if (null == ShipUiIndication)
			{
				return null;
			}

			return new VonSensor.ShipUiIndication(ShipUiIndication.AlsGbsElement(),
				ShipUiIndication.ListLabelString?.ElementAtOrDefault(0)?.Label,
				ShipUiIndication.ListLabelString?.ElementAtOrDefault(1)?.Label);
		}

		static public VonSensor.Message AlsMessage(this MemoryStruct.UIElementLabelString UIElement) =>
			null == UIElement ? null :
			new VonSensor.Message(UIElement.AlsGbsElement()) { LabelText = UIElement.Label };

		static string AusTargetLabelStringEntferneFormiirung(string StringFormiirt)
		{
			return Optimat.Glob.StringEntferneMengeXmlTag(StringFormiirt);
		}

		static public ShipHitpointsAndEnergy AlsHitpoints(this MemoryStruct.ShipHitpointsAndEnergy Hitpoints) =>
			null == Hitpoints ? null :
			new ShipHitpointsAndEnergy(Hitpoints.Struct, Hitpoints.Armor, Hitpoints.Shield, Hitpoints.Capacitor);

		static public VonSensor.ShipUiTarget AlsTarget(this MemoryStruct.ShipUiTarget Target)
		{
			if (null == Target)
			{
				return null;
			}

			var OoberhalbDistanceListeZaile =
				Target.ListLabelString
				?.Take(Target?.ListLabelString?.Length - 1 ?? 0)
				?.Select(Label => Label?.Label)
				?.Select(AusTargetLabelStringEntferneFormiirung)
				?.ToArray();

			Int64? DistanceScrankeMin;
			Int64? DistanceScrankeMax;

			var DistanceSictStringFormiirt = Target?.ListLabelString?.LastOrDefault()?.Label;

			TempAuswertGbs.Extension.AusDistanceSictStringAbbildGanzzaalDurcMeeter(
				AusTargetLabelStringEntferneFormiirung(DistanceSictStringFormiirt),
				out DistanceScrankeMin,
				out DistanceScrankeMax);


			return new VonSensor.ShipUiTarget(
				Target.AlsGbsElement(),
				Target.Hitpoints.AlsHitpoints(),
				OoberhalbDistanceListeZaile,
				DistanceScrankeMin,
				DistanceScrankeMax,
				Target.Active,
				Target.Assigned?.Select(AlsAssigned)?.ToArray(),
				Target.RegionInteraction.AlsGbsElement());
		}

		static public VonSensor.ShipUiTargetAssignedGrupe AlsAssigned(this MemoryStruct.ShipUiTargetAssignedGroup Assigned) =>
			null == Assigned ? null :
			new VonSensor.ShipUiTargetAssignedGrupe(null, Assigned.IconTexture.AlsObjektMitIdentInt64()?.AlsGbsElement());

		static public ObjektMitIdentInt64 AlsObjektMitIdentInt64(this BotEngine.ObjectIdInt64 Obj) =>
			null == Obj ? null : new ObjektMitIdentInt64(Obj.Id);

		static public VonSensor.GbsElement AlsGbsElement(this MemoryStruct.UIElement UIElement) =>
			null == UIElement ? null : new VonSensor.GbsElement(UIElement.AlsObjektMitIdentInt64(), UIElement.Region, UIElement.InTreeIndex);

		static public VonSensor.GbsElement AlsGbsElement(this ObjektMitIdentInt64 Obj) =>
			null == Obj ? null : new VonSensor.GbsElement(Obj);

		static public VonSensor.GbsElementMitBescriftung AlsGbsElementMitBescriftung(this MemoryStruct.UIElementLabelString UIElement) =>
			null == UIElement ? null : new VonSensor.GbsElementMitBescriftung(
				UIElement.AlsGbsElement(), UIElement.Label);

		static public VonSensor.MenuEntry AlsMenuEntry(this MemoryStruct.MenuEntry MenuEntry) =>
			null == MenuEntry ? null : new VonSensor.MenuEntry(MenuEntry?.AlsGbsElementMitBescriftung(), null, MenuEntry?.HighlightVisible);

		static public VonSensor.Menu AlsMenu(this MemoryStruct.Menu Menu) =>
			null == Menu ? null : new VonSensor.Menu(Menu.AlsGbsElement(), Menu?.Entry?.Select(Entry => Entry?.AlsMenuEntry())?.ToArray());

		static public VonSensor.Window AlsWindowNurBase(this MemoryStruct.Window Window)
		{
			if (null == Window)
			{
				return null;
			}

			var HeaderButtonMatchingHint = new Func<string, MemoryStruct.Sprite>(Pattern =>
			Window?.HeaderButton?.FirstOrDefault(k => Regex.Match(k?.HintText ?? "", Pattern, RegexOptions.IgnoreCase).Success));

			var HeaderButtonMinimize = HeaderButtonMatchingHint("minim");
			var HeaderButtonClose = HeaderButtonMatchingHint("close");

			return new VonSensor.Window(Window.AlsGbsElement(), Window.isModal, Window.Caption)
			{
				Sictbar = true,
				HeaderButtonMinimize = HeaderButtonMinimize.AlsGbsElement(),
				HeaderButtonClose = HeaderButtonClose.AlsGbsElement(),
				HeaderButtonsSictbar = Window.HeaderButtonsVisible,
			};
		}

		static public VonSensor.MessageBox AlsMessageBox(this MemoryStruct.MessageBox MessageBox) =>
			null == MessageBox ? null :
			new VonSensor.MessageBox(
				MessageBox?.AlsWindowNurBase(),
				MessageBox?.TopCaptionText,
				0 < MessageBox?.Button?.Length ? new VonSensor.EveButtonGroup(null, MessageBox?.Button?.Select(AlsGbsElementMitBescriftung)?.ToArray()) : null,
					MessageBox.MainEditText);

		static public VonSensor.Window AlsWindowUpcast(this MemoryStruct.Window Window) =>
			(Window as MemoryStruct.WindowInventory)?.AlsWindowInventory() ??
			(Window as MemoryStruct.WindowOverView)?.AlsWindowOverview() ??
			(Window as MemoryStruct.WindowSelectedItemView)?.AlsWindowSelectedItemView() ??
			(Window as MemoryStruct.WindowDroneView)?.AlsWindowDroneView() ??
			(Window as MemoryStruct.WindowAgentDialogue)?.AlsWindowAgentDialogue() ??
			(Window as MemoryStruct.WindowAgentBrowser)?.AlsWindowAgentBrowser() ??
			(Window as MemoryStruct.WindowStack)?.AlsWindowStack() ??
			(Window as MemoryStruct.WindowSurveyScanView)?.AlsWindowSurvesScanView() ??
			(Window as MemoryStruct.WindowStationLobby)?.AlsWindowStationLobby() ??
			(Window as MemoryStruct.WindowFittingWindow)?.AlsFittingWindow() ??
			(Window as MemoryStruct.WindowFittingMgmt)?.AlsWindowFittingMgmt() ??
			(Window as MemoryStruct.MessageBox)?.AlsMessageBox() ??
			Window?.AlsWindowNurBase();

		static public VonSensor.SystemMenu AlsSystemMenu(this MemoryStruct.SystemMenu SystemMenu) =>
			null == SystemMenu ? null :
			new VonSensor.SystemMenu(SystemMenu.AlsWindowNurBase());

		static public VonSensor.InfoPanel AlsInfoPanel(this MemoryStruct.InfoPanel InfoPanel) =>
			null == InfoPanel ? null :
			new VonSensor.InfoPanel(InfoPanel.AlsGbsElement(), InfoPanel?.IsExpanded, InfoPanel?.HeaderButtonExpand.AlsGbsElement());

		/// <summary>
		/// <url=showinfo:5//30021672 alt='Solar System* (aktuell)'><localized hint="Pasha">Pasha*</localized></url></b> <color=0xff4dffccL><hint='Sicherheitsstatus'>0.9</hint></color><fontsize=12><fontsize=8> </fontsize>&lt;<fontsize=8> </fontsize><url=showinfo:4//20000247><localized hint="Arvachah">Arvachah*</localized></url><fontsize=8> </fontsize>&lt;<fontsize=8> </fontsize><url=showinfo:3//10000020><localized hint="Tash-Murkon">Tash-Murkon*</localized></url>
		/// </summary>
		static readonly Regex LanguageNotSetToEnglishIndikatorRegex = new Regex(@"\<\/localized\>", RegexOptions.Compiled | RegexOptions.IgnoreCase);

		static public NumberFormatInfo HeaderLabelSecurityLevelNumberFormatInfoErsctele()
		{
			var FormatInfo = CultureInfo.InvariantCulture.NumberFormat.Clone() as NumberFormatInfo;

			FormatInfo.NumberDecimalSeparator = ".";

			return FormatInfo;
		}

		static public bool? LanguageNotSetToEnglish(this MemoryStruct.InfoPanelLocationInfo InfoPanel) =>
			null == InfoPanel ? (bool?)null :
			LanguageNotSetToEnglishIndikatorRegex.Match(InfoPanel.HeaderLabel?.Label ?? "").Success;

		static readonly public NumberFormatInfo HeaderLabelSecurityLevelNumberFormatInfo = HeaderLabelSecurityLevelNumberFormatInfoErsctele();

		static string AusHeaderTopLabelTextInfoRegexPatternHirarciiKomponente =
			Regex.Escape("<url") + "[^" + Regex.Escape(">") + "]+" + Regex.Escape(">") +
			"(|" + Regex.Escape("<localized") + "[^" + Regex.Escape(">") + "]*" + Regex.Escape(">") + ")" +
			"([^<]+)" +
			"(|" + Regex.Escape("</localized>") + ")" + Regex.Escape("</url>");

		static public SictAusGbsLocationInfo AusHeaderTopLabelTextCurrentLocationInfo(
			this string HeaderText,
			string ErsazNearestName = null)
		{
			var ListeKomponente =
				HeaderText?.ListeStringZwisceXmlTag()
				?.Select((InnerText) => InnerText?.Trim())
				?.Where((InnerText) => !InnerText.NullOderLeer())
				?.ToArray();

			if (null == ListeKomponente)
			{
				return null;
			}

			if (6 != ListeKomponente.Length)
			{
				return null;
			}

			var ListeKomponenteTrenung = new string[] { ListeKomponente[2], ListeKomponente[4] };

			if (!ListeKomponenteTrenung.All((Komponente) => string.Equals(Komponente, "&lt;", StringComparison.InvariantCultureIgnoreCase)))
			{
				return null;
			}

			var SolarSystemName = ListeKomponente[0];
			var SolarSystemSecurityLevelString = ListeKomponente[1];
			var ConstellationName = ListeKomponente[3];
			var RegionName = ListeKomponente[5];

			var SolarSystemSecurityLevel =
				SolarSystemSecurityLevelString.TryParseDoubleNulbar(HeaderLabelSecurityLevelNumberFormatInfo, System.Globalization.NumberStyles.Number);

			var SolarSystemSecurityLevelMili = (int?)(SolarSystemSecurityLevel * 1000);

			var Ergeebnis = new SictAusGbsLocationInfo(
				ExtractFromOldAssembly.Bib3.Glob.TrimNullable(ErsazNearestName),
				SolarSystemName,
				SolarSystemSecurityLevelString,
				SolarSystemSecurityLevelMili,
				ConstellationName,
				RegionName);

			return Ergeebnis;
		}

		static public SictAusGbsLocationInfo CurrentLocationInfo(
			this MemoryStruct.InfoPanelLocationInfo InfoPanel)
		{
			var HeaderText = InfoPanel?.HeaderLabel?.Label;

			var ContentLabelTop = InfoPanel?.ExpandedContentLabel?.FirstOrDefault();

			string NearestName = null;

			{
				var NearestNameMatch = Regex.Match(ContentLabelTop?.Label ?? "", AusHeaderTopLabelTextInfoRegexPatternHirarciiKomponente, RegexOptions.IgnoreCase);

				if (NearestNameMatch.Success)
				{
					NearestName = ExtractFromOldAssembly.Bib3.Glob.TrimNullable(NearestNameMatch.Groups[2].Value);
				}
			}

			return AusHeaderTopLabelTextCurrentLocationInfo(HeaderText, NearestName);
		}

		static public VonSensor.InfoPanelMissionsMission AlsInfoPanelMissionsMission(this MemoryStruct.UIElementLabelString UIElement) =>
			null == UIElement ? null :
			new VonSensor.InfoPanelMissionsMission(UIElement.AlsGbsElementMitBescriftung());

		static public VonSensor.InfoPanelLocationInfo AlsInfoPanelLocationInfo(this MemoryStruct.InfoPanelLocationInfo InfoPanel) =>
			null == InfoPanel ? null :
			new VonSensor.InfoPanelLocationInfo(InfoPanel.AlsInfoPanel(), InfoPanel.CurrentLocationInfo(), InfoPanel.ButtonListSurroundings.AlsGbsElement(), InfoPanel.LanguageNotSetToEnglish());

		static public VonSensor.InfoPanelMissions AlsInfoPanelMissions(this MemoryStruct.InfoPanelMissions InfoPanel) =>
			null == InfoPanel ? null :
			new VonSensor.InfoPanelMissions(InfoPanel.AlsInfoPanel())
			{
				ListeMissionButton = InfoPanel.ListMissionButton?.Select(AlsInfoPanelMissionsMission)?.ToArray()
			};

		static public bool? NoDestination(this MemoryStruct.InfoPanelRoute InfoPanel) =>
			null == InfoPanel ? (bool?)null :
			(InfoPanel?.ExpandedContentLabel?.Any(label => Regex.Match(label?.Label ?? "", @"no\s*dest", RegexOptions.IgnoreCase).Success) ?? false);

		static public VonSensor.InfoPanelRoute AlsInfoPanelRoute(this MemoryStruct.InfoPanelRoute InfoPanel) =>
			null == InfoPanel ? null :
			new VonSensor.InfoPanelRoute(
				InfoPanel.AlsInfoPanel(),
				InfoPanel?.NextLabel?.Label?.AusHeaderTopLabelTextCurrentLocationInfo(),
				InfoPanel?.DestinationLabel?.Label?.AusHeaderTopLabelTextCurrentLocationInfo(),
				InfoPanel.NoDestination(),
				InfoPanel?.WaypointMarker?.Select(AlsGbsElement)?.ToArray());

		static public bool? CharacterAuswaalAbgesclose(this MemoryStruct.MemoryMeasurement MemoryMeasurement) =>
			null == MemoryMeasurement ? (bool?)null :
			(null != MemoryMeasurement?.ShipUi ||
			null != MemoryMeasurement?.InfoPanelButtonLocationInfo ||
			null != MemoryMeasurement?.InfoPanelButtonRoute ||
			null != MemoryMeasurement?.WindowOverview ||
			null != MemoryMeasurement?.WindowStationLobby);
	}
}
