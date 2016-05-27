using Bib3;
using Bib3.Geometrik;
using BotEngine;
using BotEngine.Common;
using ExtractFromOldAssembly.Bib3;
using Sanderling.Interface.MemoryStruct;
using Sanderling.Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MapToOldInterface
{
	static public class MapToOldInterface
	{
		static public BotEngine.EveOnline.Interface.MemoryStruct.MemoryMeasurement AsOld(
			this Sanderling.Interface.MemoryStruct.IMemoryMeasurement memoryMeasurement)
		{
			var parsed = memoryMeasurement?.Parse();

			if (parsed == null)
				return null;

			return new BotEngine.EveOnline.Interface.MemoryStruct.MemoryMeasurement
			{
				Menu = parsed?.Menu?.Select(AsOld)?.ToArray(),
				AbovemainMessage = parsed?.AbovemainMessage?.AsOldUIElementLabelString()?.ToArray(),
				AbovemainPanelEveMenu = parsed?.AbovemainPanelEveMenu?.Select(AsOld)?.ToArray(),
				AbovemainPanelGroup = parsed?.AbovemainPanelGroup?.Select(AsOld)?.ToArray(),
				InfoPanelButtonIncursions = parsed?.InfoPanelButtonIncursions?.AsOldUIElement(),
				InfoPanelButtonLocationInfo = parsed?.InfoPanelButtonCurrentSystem?.AsOldUIElement(),
				InfoPanelButtonRoute = parsed?.InfoPanelButtonRoute?.AsOldUIElement(),
				InfoPanelButtonMissions = parsed?.InfoPanelButtonMissions?.AsOldUIElement(),
				InfoPanelLocationInfo = parsed?.InfoPanelCurrentSystem?.AsOld(),
				InfoPanelRoute = parsed?.InfoPanelRoute?.AsOld(),
				InfoPanelMissions = parsed?.InfoPanelMissions?.AsOld(),
				ModuleButtonTooltip = parsed?.ModuleButtonTooltip?.AsOld(),
				Neocom = parsed?.Neocom?.AsOld(),
				ShipUi = parsed?.ShipUi?.AsOld(),
				SystemMenu = parsed?.SystemMenu?.AsOldSystemMenu(),
				Target = parsed?.Target?.Select(AsOld)?.ToArray(),
				UtilmenuMission = parsed?.Utilmenu?.Select(c => c.AsOldUtilmenuMission(parsed))?.WhereNotDefault()?.FirstOrDefault(),
				VersionString = parsed?.VersionString,
				WindowAgentBrowser = parsed?.WindowAgentBrowser?.Select(AsOldWindowAgentBrowser)?.ToArray(),
				WindowAgentDialogue = parsed?.WindowAgentDialogue?.Select(AsOld)?.ToArray(),
				WindowChatChannel = parsed?.WindowChatChannel?.Select(AsOld)?.ToArray(),
				WindowDroneView = parsed?.WindowDroneView?.Select(AsOld)?.ToArray(),
				WindowFittingMgmt = parsed?.WindowFittingMgmt?.Select(AsOld)?.ToArray(),
				WindowFittingWindow = parsed?.WindowShipFitting?.Select(AsOld)?.ToArray(),
				WindowInventory = parsed?.WindowInventory?.Select(AsOld)?.ToArray(),
				WindowItemSell = parsed?.WindowItemSell?.Select(AsOld)?.ToArray(),
				WindowMarketAction = parsed?.WindowMarketAction?.Select(AsOld)?.ToArray(),
				WindowOverview = parsed?.WindowOverview?.Select(AsOld)?.ToArray(),
				WindowOther = parsed?.WindowOther?.Select(AsOldWindow)?.ToArray(),
				WindowRegionalMarket = parsed?.WindowRegionalMarket?.Select(AsOld)?.ToArray(),
				WindowSelectedItemView = parsed?.WindowSelectedItemView?.Select(AsOld)?.ToArray(),
				WindowStack = parsed?.WindowStack?.Select(AsOld)?.ToArray(),
				WindowStationLobby = parsed?.WindowStation?.Select(AsOld)?.ToArray(),
				WindowSurveyScanView = parsed?.WindowSurveyScanView?.Select(AsOld)?.ToArray(),
				WindowTelecom = parsed?.WindowTelecom?.Select(AsOld)?.ToArray(),
			};
		}

		static public OrtogoonInt AsOrtogoon(this RectInt rect) =>
			new OrtogoonInt(rect.Min0, rect.Min1, rect.Max0, rect.Max1);

		static public BotEngine.EveOnline.Interface.MemoryStruct.UIElement AsOldUIElement(
			this IUIElement uiElement)
		{
			if (uiElement == null)
				return null;

			return new BotEngine.EveOnline.Interface.MemoryStruct.UIElement(Base: ((IObjectIdInMemory)uiElement).AsOld())
			{
				Region = uiElement.Region.AsOrtogoon(),
				InTreeIndex = uiElement.InTreeIndex,
			};
		}

		static public BotEngine.EveOnline.Interface.MemoryStruct.UIElementLabelString AsOldUIElementLabelString(
			this IUIElementText uiElement)
		{
			if (uiElement == null)
				return null;

			return new BotEngine.EveOnline.Interface.MemoryStruct.UIElementLabelString(Base: uiElement.AsOldUIElement(), Label: uiElement?.Text);
		}

		static public IEnumerable<BotEngine.EveOnline.Interface.MemoryStruct.UIElementLabelString> AsOldUIElementLabelString(
			this IEnumerable<IUIElementText> seq) => seq?.Select(AsOldUIElementLabelString);

		static public BotEngine.EveOnline.Interface.MemoryStruct.UIElementTextBox AsOldUIElementTextBox(
			this IUIElementInputText uiElement)
		{
			if (uiElement == null)
				return null;

			return new BotEngine.EveOnline.Interface.MemoryStruct.UIElementTextBox(Base: uiElement.AsOldUIElement(), Label: uiElement?.Text);
		}

		static public BotEngine.EveOnline.Interface.MemoryStruct.MenuEntry AsOld(
			this Sanderling.Parse.IMenuEntry menuEntry)
		{
			if (menuEntry == null)
				return null;

			return new BotEngine.EveOnline.Interface.MemoryStruct.MenuEntry(menuEntry.AsOldUIElementLabelString())
			{
				HighlightVisible = menuEntry?.HighlightVisible,
			};
		}

		static public BotEngine.EveOnline.Interface.MemoryStruct.Menu AsOld(
			this Sanderling.Parse.IMenu menu)
		{
			if (menu == null)
				return null;

			return new BotEngine.EveOnline.Interface.MemoryStruct.Menu(menu.AsOldUIElement())
			{
				Entry = menu.Entry?.Select(AsOld)?.ToArray(),
			};
		}

		static public BotEngine.EveOnline.Interface.MemoryStruct.PanelGroup AsOld(
			this PanelGroup panelGroup) =>
			panelGroup == null ? null : new BotEngine.EveOnline.Interface.MemoryStruct.PanelGroup(panelGroup.AsOldUIElement());

		static public BotEngine.EveOnline.Interface.MemoryStruct.InfoPanel AsOldInfoPanel(
			this IInfoPanel infoPanel)
		{
			if (infoPanel == null)
				return null;

			return new BotEngine.EveOnline.Interface.MemoryStruct.InfoPanel(infoPanel.AsOldUIElement())
			{
				IsExpanded = infoPanel?.IsExpanded,
				ExpandedContentLabel = infoPanel?.ExpandedContent?.LabelText?.AsOldUIElementLabelString()?.ToArray(),
				HeaderButtonExpand = infoPanel?.ExpandToggleButton?.AsOldUIElement(),
				HeaderLabel = infoPanel?.HeaderContent?.LabelText?.Largest()?.AsOldUIElementLabelString(),
			};
		}

		static public BotEngine.EveOnline.Interface.MemoryStruct.InfoPanelLocationInfo AsOld(
			this IInfoPanelSystem infoPanelSystem)
		{
			if (infoPanelSystem == null)
				return null;

			return new BotEngine.EveOnline.Interface.MemoryStruct.InfoPanelLocationInfo(infoPanelSystem.AsOldInfoPanel())
			{
				ButtonListSurroundings = infoPanelSystem?.ListSurroundingsButton?.AsOldUIElement(),
			};
		}

		static public BotEngine.EveOnline.Interface.MemoryStruct.InfoPanelRoute AsOld(
			this IInfoPanelRoute infoPanelRoute)
		{
			if (infoPanelRoute == null)
				return null;

			return new BotEngine.EveOnline.Interface.MemoryStruct.InfoPanelRoute(infoPanelRoute.AsOldInfoPanel())
			{
				WaypointMarker = infoPanelRoute?.RouteElementMarker?.Select(AsOldUIElement)?.ToArray(),
				DestinationLabel = infoPanelRoute?.DestinationLabel?.AsOldUIElementLabelString(),
				NextLabel = infoPanelRoute?.NextLabel?.AsOldUIElementLabelString(),
			};
		}

		static public BotEngine.EveOnline.Interface.MemoryStruct.InfoPanelMissions AsOld(
			this IInfoPanelMissions infoPanelMissions)
		{
			if (infoPanelMissions == null)
				return null;

			return new BotEngine.EveOnline.Interface.MemoryStruct.InfoPanelMissions(infoPanelMissions.AsOldInfoPanel())
			{
				ListMissionButton = infoPanelMissions?.ListMissionButton?.AsOldUIElementLabelString()?.ToArray(),
			};
		}

		static public BotEngine.EveOnline.Interface.MemoryStruct.ObjectIdInMemory AsOld(this IObjectIdInMemory objectIdInMemory) =>
			objectIdInMemory == null ? null : new BotEngine.EveOnline.Interface.MemoryStruct.ObjectIdInMemory(objectIdInMemory.Id);

		static public BotEngine.EveOnline.Interface.MemoryStruct.ModuleButtonTooltip AsOld(
			this IModuleButtonTooltip moduleButtonTooltip)
		{
			if (moduleButtonTooltip == null)
				return null;

			var listRow = new List<BotEngine.EveOnline.Interface.MemoryStruct.ModuleButtonTooltipRow>();

			var listElementToGroup = new[]
			{
				(IEnumerable<IUIElement>)moduleButtonTooltip.LabelText,
				moduleButtonTooltip.Sprite,
			}
			.ConcatNullable().WhereNotDefault()
			.OrderByCenterVerticalDown()
			.ToArray();

			var rowContainer = new List<IUIElement>();

			var rowContainerToRow = new Action(() =>
			{
				if (rowContainer.Count < 1)
					return;

				//	order elements horizontally
				var rowContainerListElement = rowContainer?.OrderBy(elem => elem.Region.Center().A)?.ToArray();

				listRow.Add(new BotEngine.EveOnline.Interface.MemoryStruct.ModuleButtonTooltipRow
				{
					ListLabelString = rowContainerListElement?.OfType<IUIElementText>()?.AsOldUIElementLabelString()?.ToArray(),
					IconTextureId = rowContainerListElement?.OfType<ISprite>()?.Select(sprite => sprite.Texture0Id.AsOld())?.ToArray(),
					ShortcutText = rowContainerListElement.Contains(moduleButtonTooltip.ToggleKeyTextLabel) ? moduleButtonTooltip?.ToggleKeyTextLabel?.Text : null,
				});

				rowContainer.Clear();
			});

			//	group UIElements into rows dependent on overlapping.
			foreach (var item in listElementToGroup)
			{
				if (0 < rowContainer.Count)
					if (rowContainer.Last().Region.Max1 < item.Region.Center().B)
						rowContainerToRow();

				rowContainer.Add(item);
			}

			rowContainerToRow();

			return new BotEngine.EveOnline.Interface.MemoryStruct.ModuleButtonTooltip(moduleButtonTooltip.AsOldUIElement())
			{
				ListLabelString = moduleButtonTooltip?.LabelText?.AsOldUIElementLabelString()?.ToArray(),
				ListRow = listRow?.ToArray(),
			};
		}

		static public BotEngine.EveOnline.Interface.MemoryStruct.Sprite AsOld(
			this ISprite sprite) =>
			sprite == null ? null : new BotEngine.EveOnline.Interface.MemoryStruct.Sprite(sprite.AsOldUIElement())
			{
				Color = sprite.Color,
				HintText = sprite.HintText,
				Texture0Id = sprite.Texture0Id?.AsOld(),
				TexturePath = sprite.TexturePath,
				Name = sprite.Name,
			};

		static public BotEngine.EveOnline.Interface.MemoryStruct.Neocom AsOld(
			this Sanderling.Parse.INeocom neocom)
		{
			if (neocom == null)
				return null;

			return new BotEngine.EveOnline.Interface.MemoryStruct.Neocom(neocom.AsOldUIElement())
			{
				Button = neocom.Button?.Select(AsOld)?.ToArray(),
				CharButton = neocom.CharButton?.AsOldUIElement(),
				EveMenuButton = neocom.EveMenuButton?.AsOldUIElement(),
				Clock = neocom.Clock?.AsOldUIElementLabelString(),
			};
		}

		static public BotEngine.EveOnline.Interface.MemoryStruct.ShipUiEWarElement AsOld(
			this ShipUiEWarElement ewarElement) =>
			ewarElement == null ? null :
			new BotEngine.EveOnline.Interface.MemoryStruct.ShipUiEWarElement
			{
				EWarType = ewarElement.EWarType,
				IconTexture = ewarElement.IconTexture.AsOld(),
			};

		static public BotEngine.EveOnline.Interface.MemoryStruct.ShipHitpointsAndEnergy AsOld(
			this IShipHitpointsAndEnergy shipHitpointsAndEnergy) =>
			shipHitpointsAndEnergy == null ? null :
			new BotEngine.EveOnline.Interface.MemoryStruct.ShipHitpointsAndEnergy
			{
				Shield = shipHitpointsAndEnergy.Shield,
				Armor = shipHitpointsAndEnergy.Armor,
				Struct = shipHitpointsAndEnergy.Struct,
				Capacitor = shipHitpointsAndEnergy.Capacitor,
			};

		static public BotEngine.EveOnline.Interface.MemoryStruct.ShipUiIndication AsOld(
			this IShipUiIndication shipUiIndication) =>
			shipUiIndication == null ? null :
			new BotEngine.EveOnline.Interface.MemoryStruct.ShipUiIndication(shipUiIndication.AsOldUIElement())
			{
				ListLabelString = shipUiIndication.LabelText?.AsOldUIElementLabelString()?.ToArray(),
			};

		static public BotEngine.EveOnline.Interface.MemoryStruct.ShipUiModule AsOld(
			this IShipUiModule module) =>
			module == null ? null :
			new BotEngine.EveOnline.Interface.MemoryStruct.ShipUiModule(module.AsOldUIElement())
			{
				BusyVisible = module.BusyVisible,
				GlowVisible = module.GlowVisible,
				HiliteVisible = module.HiliteVisible,
				ModuleButtonIconTexture = module.ModuleButtonIconTexture.AsOld(),
				ModuleButtonQuantity = module.ModuleButtonQuantity,
				ModuleButtonVisible = module.ModuleButtonVisible,
				RampActive = module.RampActive,
				RampRotationMilli = module.RampRotationMilli,
			};

		static public BotEngine.EveOnline.Interface.MemoryStruct.ShipUiTimer AsOld(
			this IShipUiTimer timer) =>
			timer == null ? null :
			new BotEngine.EveOnline.Interface.MemoryStruct.ShipUiTimer(timer.AsOldUIElement())
			{
				Label = timer.LabelText?.AsOldUIElementLabelString()?.ToArray(),
				Name = timer.Name,
			};

		static public BotEngine.EveOnline.Interface.MemoryStruct.ShipUi AsOld(
			this Sanderling.Parse.IShipUi shipUI) =>
			shipUI == null ? null :
			new BotEngine.EveOnline.Interface.MemoryStruct.ShipUi(shipUI.AsOldUIElement())
			{
				ButtonSpeed0 = shipUI.ButtonSpeed0?.AsOldUIElement(),
				ButtonSpeedMax = shipUI.ButtonSpeedMax?.AsOldUIElement(),
				Center = shipUI.Center?.AsOldUIElement(),
				EWarElement = shipUI.EWarElement?.Select(AsOld)?.ToArray(),
				Hitpoints = shipUI.HitpointsAndEnergy?.AsOld(),
				Indication = shipUI.Indication?.AsOld(),
				Module = shipUI.Module?.Select(AsOld)?.ToArray(),
				Readout = shipUI.Readout?.AsOldUIElementLabelString()?.ToArray(),
				SpeedLabel = shipUI.SpeedLabel?.AsOldUIElementLabelString(),
				SpeedMilli = shipUI.SpeedMilli,
				Timer = shipUI.Timer?.Select(AsOld)?.ToArray(),
			};

		static public BotEngine.EveOnline.Interface.MemoryStruct.Window AsOldWindow(this IWindow window) =>
			window == null ? null :
			new BotEngine.EveOnline.Interface.MemoryStruct.Window(window.AsOldUIElement())
			{
				Button = window?.ButtonText?.AsOldUIElementLabelString()?.ToArray(),
				Caption = window?.Caption,
				HeaderButton = window?.HeaderButton?.Select(AsOld)?.ToArray(),
				HeaderButtonsVisible = window?.HeaderButtonsVisible,
				isModal = window?.isModal,
				Label = window?.LabelText?.AsOldUIElementLabelString()?.ToArray(),
				TextBox = window?.InputText?.Select(AsOldUIElementTextBox)?.ToArray(),
			};

		static public BotEngine.EveOnline.Interface.MemoryStruct.SystemMenu AsOldSystemMenu(this IWindow window) =>
			window == null ? null :
			new BotEngine.EveOnline.Interface.MemoryStruct.SystemMenu(window.AsOldWindow());

		static public BotEngine.EveOnline.Interface.MemoryStruct.ShipUiTargetAssignedGroup AsOld(this ShipUiTargetAssignedGroup assignedGroup) =>
			assignedGroup == null ? null :
			new BotEngine.EveOnline.Interface.MemoryStruct.ShipUiTargetAssignedGroup(assignedGroup.AsOldUIElement())
			{
				IconTexture = assignedGroup.IconTexture?.AsOld(),
			};

		static public BotEngine.EveOnline.Interface.MemoryStruct.ShipUiTarget AsOld(this Sanderling.Parse.IShipUiTarget target) =>
			target == null ? null :
			new BotEngine.EveOnline.Interface.MemoryStruct.ShipUiTarget(target.AsOldUIElement())
			{
				Active = target?.IsSelected,
				Assigned = target?.Assigned?.Select(AsOld)?.ToArray(),
				Hitpoints = target?.Hitpoints?.AsOld(),
				ListLabelString = target?.LabelText?.Select(AsOldUIElementLabelString)?.ToArray(),
				RegionInteraction = target?.RegionInteraction?.AsOldUIElement(),
			};

		static string UtilmenuMission_AusAstLocationTextLocationNameRegexPattern = Regex.Escape(">") + "([^<]{1,999})" + Regex.Escape("<");

		static string UtilmenuMission_AusAstLocationTextLocationName(string AstLocationText)
		{
			if (null == AstLocationText)
				return null;

			var MengeMatch = Regex.Matches(AstLocationText, UtilmenuMission_AusAstLocationTextLocationNameRegexPattern);

			var Match =
				MengeMatch
				.OfType<Match>()
				.OrderByDescending((Kandidaat) => Kandidaat.Groups[1].Length)
				.FirstOrDefault();

			if (!(Match?.Success ?? false))
				return null;

			return Match.Groups[1].Value;
		}

		static public IUIElementText WithText(
			this IUIElementText orig, string text) =>
			orig == null ? null :
			new UIElementText((IUIElement)orig, text);

		static public BotEngine.EveOnline.Interface.MemoryStruct.UtilmenuMission AsOldUtilmenuMission(
			this IContainer container,
			Sanderling.Parse.IMemoryMeasurement measurement)
		{
			if (container == null)
				return null;

			var buttonReadDetails = container?.LabelText?.FirstOrDefault(c => c?.Text?.RegexMatchSuccessIgnoreCase("read details") ?? false);
			var buttonStartConversation = container?.LabelText?.FirstOrDefault(c => c?.Text?.RegexMatchSuccessIgnoreCase("start conversation") ?? false);

			if (buttonReadDetails == null && buttonStartConversation == null)
				return null;    //	assume it is not a mission utilmenu

			var labelTopmost = container?.LabelText?.OrderByCenterVerticalDown()?.FirstOrDefault();

			//	header is not contained in container, take the label which was most likely clicked to open the menu.
			//	search for this label by distance to UIElement contained in menu while assuming that the menu is opened under the clicked label.
			var listUIElementTextWithDistance =
				measurement?.EnumerateReferencedUIElementTransitive()?.OfType<IUIElementText>()?.Select(c =>
				{
					var leftDist = c.Region.Min0 - labelTopmost.Region.Min0;
					var heightDist = c.Region.Max1 - labelTopmost.Region.Min1;

					return new
					{
						uiElement = c,
						dist = new[] { leftDist, heightDist }.Select(dist => Math.Abs(dist)).Max(),
					};
				})
				?.OrderBy(c => c.dist)
				?.Take(4)
				?.ToArray();

			var header = listUIElementTextWithDistance?.FirstOrDefault(c => c.dist <= 4)?.uiElement;

			var listLocationBottom =
				new[] { buttonReadDetails, buttonStartConversation }.WhereNotDefault().Select(elem => elem.Region.Min1).Min();

			var listLocationListUIElement =
				new[]
				{
					(IEnumerable<IUIElement>)container?.LabelText,
					container?.ButtonText,
					container?.Sprite,
				}
				.ConcatNullable().WhereNotDefault()
				.Where(elem => elem.Region.Max1 <= listLocationBottom)
				.OrderByCenterVerticalDown()
				.ToArray();

			var listLocation = new List<BotEngine.EveOnline.Interface.MemoryStruct.UtilmenuMissionLocationInfo>();

			var inLocationListElement = new List<IUIElement>();

			var locationConstruct = new Action(() =>
			{
				if (inLocationListElement.Count < 1)
					return;

				var labelMatching = new Func<string, IUIElementText>(pattern =>
				inLocationListElement.OfType<IUIElementText>().FirstOrDefault(elem => elem.Text?.RegexMatchSuccessIgnoreCase(pattern) ?? false));

				var buttonLocation = inLocationListElement.OfType<IUIElementText>()?.FirstOrDefault(c => c?.Text?.RegexMatchSuccessIgnoreCase(@"url=showinfo:\d") ?? false);

				listLocation.Add(new BotEngine.EveOnline.Interface.MemoryStruct.UtilmenuMissionLocationInfo
				{
					ButtonApproach = labelMatching("approach").AsOldUIElementLabelString(),
					ButtonDock = labelMatching("dock").AsOldUIElementLabelString(),
					ButtonSetDestination = labelMatching("set destination").AsOldUIElementLabelString(),
					ButtonWarpTo = labelMatching("warp to").AsOldUIElementLabelString(),

					HeaderLabel = inLocationListElement.OfType<IUIElementText>()?.FirstOrDefault().Text,
					ButtonLocation = buttonLocation?.WithText(UtilmenuMission_AusAstLocationTextLocationName(buttonLocation?.Text)?.Trim()).AsOldUIElementLabelString(),
				});

				inLocationListElement.Clear();
			});

			//	group UIElements into locations.
			foreach (var elem in listLocationListUIElement)
			{
				if ((elem as IUIElementText)?.Text?.RegexMatchSuccessIgnoreCase(@"(objective|encounter)") ?? false)
					locationConstruct();

				inLocationListElement.Add(elem);
			}

			locationConstruct();

			return new BotEngine.EveOnline.Interface.MemoryStruct.UtilmenuMission(container.AsOldUIElement())
			{
				ButtonReadDetails = buttonReadDetails?.AsOldUIElementLabelString(),
				ButtonStartConversation = buttonStartConversation?.AsOldUIElementLabelString(),
				Header = header.AsOldUIElementLabelString(),
				Location = listLocation?.ToArray(),
			};
		}

		static public BotEngine.EveOnline.Interface.MemoryStruct.WindowAgent AsOldWindowAgent(this IWindowAgent window) =>
			window == null ? null :
			new BotEngine.EveOnline.Interface.MemoryStruct.WindowAgent(window.AsOldWindow());

		static public BotEngine.EveOnline.Interface.MemoryStruct.WindowAgentBrowser AsOldWindowAgentBrowser(this IWindow window) =>
			window == null ? null :
			new BotEngine.EveOnline.Interface.MemoryStruct.WindowAgentBrowser(new BotEngine.EveOnline.Interface.MemoryStruct.WindowAgent(window.AsOldWindow()));

		static public BotEngine.EveOnline.Interface.MemoryStruct.WindowAgentPane AsOld(this Sanderling.Parse.IWindowAgentPane pane) =>
			pane == null ? null :
			new BotEngine.EveOnline.Interface.MemoryStruct.WindowAgentPane(pane.AsOldUIElement())
			{
				Html = pane?.Html,
			};

		static public BotEngine.EveOnline.Interface.MemoryStruct.WindowAgentDialogue AsOld(this Sanderling.Parse.IWindowAgentDialogue window) =>
			window == null ? null :
			new BotEngine.EveOnline.Interface.MemoryStruct.WindowAgentDialogue(window.AsOldWindowAgent())
			{
				LeftPane = window?.LeftPane?.AsOld(),
				RightPane = window?.RightPane?.AsOld(),
			};

		static public BotEngine.EveOnline.Interface.MemoryStruct.WindowChatChannel AsOld(this WindowChatChannel window) =>
			window == null ? null :
			new BotEngine.EveOnline.Interface.MemoryStruct.WindowChatChannel(window.AsOldWindow())
			{
				//	at the moment I guess the bot does not use those anyway.
			};

		static public BotEngine.EveOnline.Interface.MemoryStruct.Scroll AsOld(this IScroll scroll)
		{
			if (scroll == null)
				return null;

			return new BotEngine.EveOnline.Interface.MemoryStruct.Scroll(scroll.AsOldUIElement())
			{
				Clipper = scroll?.Clipper?.AsOldUIElement(),
				ScrollHandleBound = scroll?.ScrollHandleBound?.AsOldUIElement(),
				ScrollHandle = scroll?.ScrollHandle?.AsOldUIElement(),
			};
		}

		static public BotEngine.EveOnline.Interface.MemoryStruct.ListColumnHeader AsOld(this IColumnHeader columnHeader)
		{
			if (columnHeader == null)
				return null;

			return new BotEngine.EveOnline.Interface.MemoryStruct.ListColumnHeader(columnHeader.AsOldUIElementLabelString())
			{
				ColumnIndex = columnHeader.ColumnIndex,
				SortDirection = columnHeader.SortDirection,
			};
		}

		static public BotEngine.EveOnline.Interface.MemoryStruct.ListEntry AsOldListEntry(
			this Sanderling.Interface.MemoryStruct.IListEntry entry,
			bool toDrone)
		{
			if (entry == null)
				return null;

			var @base = new BotEngine.EveOnline.Interface.MemoryStruct.ListEntry(entry.AsOldUIElement())
			{
				ContentBoundLeft = entry?.ContentBoundLeft,
				GroupExpander = entry?.GroupExpander?.AsOldUIElement(),
				IsExpanded = entry?.IsExpanded,
				IsGroup = entry?.IsGroup,
				IsSelected = entry?.IsSelected,
				SetSprite = entry?.SetSprite?.Select(AsOld)?.ToArray(),
				ListBackgroundColor = entry?.ListBackgroundColor,
				ListColumnCellLabel = entry?.ListColumnCellLabel?.Select(c => new KeyValuePair<BotEngine.EveOnline.Interface.MemoryStruct.ListColumnHeader, string>(c.Key.AsOld(), c.Value))?.ToArray(),
			};

			var droneEntry = entry as IDroneViewEntryItem;
			var overviewEntry = entry as Sanderling.Parse.IOverviewEntry;

			if (overviewEntry != null)
				return new BotEngine.EveOnline.Interface.MemoryStruct.OverviewEntry(@base)
				{
					RightIcon = overviewEntry?.RightIcon?.Select(AsOld)?.ToArray(),
				};

			if (droneEntry != null)
				return new BotEngine.EveOnline.Interface.MemoryStruct.DroneViewEntryItem(@base)
				{
					Hitpoints = droneEntry?.Hitpoints?.AsOld(),
				};

			if (toDrone && (entry?.IsGroup ?? false))
				return new BotEngine.EveOnline.Interface.MemoryStruct.DroneViewEntryGroup(@base)
				{
					Caption = entry?.LabelTextLargest()?.AsOldUIElementLabelString(),
				};

			return @base;
		}

		static public BotEngine.EveOnline.Interface.MemoryStruct.ListViewport AsOldListView(this IListViewAndControl listView)
		{
			//	this is a case of the graph probably not being a tree since the entries refer to the column headers too.

			if (listView == null)
				return null;

			var toDrone = listView?.Entry?.Any(entry => entry is IDroneViewEntryItem) ?? false;

			return new BotEngine.EveOnline.Interface.MemoryStruct.ListViewport()
			{
				Scroll = listView?.Scroll?.AsOld(),
				Entry = listView?.Entry?.Select(entry => entry?.AsOldListEntry(toDrone))?.ToArray(),
				ColumnHeader = listView?.ColumnHeader?.Select(AsOld)?.ToArray(),
			};
		}

		static public BotEngine.EveOnline.Interface.MemoryStruct.ListViewport AsOld(this IListViewAndControl listView) => AsOldListView(listView);

		static public BotEngine.EveOnline.Interface.MemoryStruct.WindowDroneView AsOld(this IWindowDroneView window) =>
			window == null ? null :
			new BotEngine.EveOnline.Interface.MemoryStruct.WindowDroneView(window.AsOldWindow())
			{
				ListViewport = window?.ListView?.AsOld(),
			};

		static public BotEngine.EveOnline.Interface.MemoryStruct.WindowFittingMgmt AsOld(this WindowFittingMgmt window) =>
			window == null ? null :
			new BotEngine.EveOnline.Interface.MemoryStruct.WindowFittingMgmt(window.AsOldWindow())
			{
				FittingViewport = window?.FittingView?.AsOld(),
			};

		static public BotEngine.EveOnline.Interface.MemoryStruct.WindowFittingWindow AsOld(this WindowShipFitting window) =>
			window == null ? null :
			new BotEngine.EveOnline.Interface.MemoryStruct.WindowFittingWindow(window.AsOldWindow());

		static public BotEngine.EveOnline.Interface.MemoryStruct.TreeViewEntry AsOld(this ITreeViewEntry entry) =>
			entry == null ? null :
			new BotEngine.EveOnline.Interface.MemoryStruct.TreeViewEntry(entry.AsOldUIElement())
			{
				Child = entry?.Child?.Select(AsOld)?.ToArray(),
				ExpandCollapseToggleRegion = entry?.ExpandToggleButton?.AsOldUIElement(),
				TopContLabel = entry?.LabelText?.OrderByCenterVerticalDown()?.FirstOrDefault()?.AsOldUIElementLabelString(),
			};

		static public BotEngine.EveOnline.Interface.MemoryStruct.Inventory AsOld(this IInventory inventory) =>
			inventory == null ? null :
			new BotEngine.EveOnline.Interface.MemoryStruct.Inventory(inventory.AsOldUIElement())
			{
				ViewList = inventory?.ListView?.AsOld(),
			};

		static public BotEngine.EveOnline.Interface.MemoryStruct.WindowInventory AsOld(this Sanderling.Parse.IWindowInventory window) =>
			window == null ? null :
			new BotEngine.EveOnline.Interface.MemoryStruct.WindowInventory(window.AsOldWindow())
			{
				LeftTreeListEntry = window?.LeftTreeListEntry?.Select(AsOld)?.ToArray(),
				LeftTreeViewportScroll = window?.LeftTreeViewportScroll?.AsOld(),
				SelectedRightControlViewButton = window?.SelectedRightControlViewButton?.Select(AsOld)?.ToArray(),
				SelectedRightFilterButtonClear = window?.SelectedRightFilterButtonClear?.AsOldUIElement(),
				SelectedRightFilterTextBox = window?.SelectedRightFilterTextBox?.AsOldUIElementTextBox(),
				SelectedRightInventory = window?.SelectedRightInventory?.AsOld(),
				SelectedRightInventoryCapacity = window?.SelectedRightInventoryCapacity?.AsOldUIElementLabelString(),
				SelectedRightInventoryPathLabel = window?.SelectedRightInventoryPathLabel?.AsOldUIElementLabelString(),
				SelectedRightItemDisplayedCount = null,
				SelectedRightItemFilteredCount = null,
			};

		static public BotEngine.EveOnline.Interface.MemoryStruct.WindowItemSell AsOld(this WindowItemSell window) =>
			window == null ? null :
			new BotEngine.EveOnline.Interface.MemoryStruct.WindowItemSell(window.AsOldWindow());

		static public BotEngine.EveOnline.Interface.MemoryStruct.WindowMarketAction AsOld(this WindowMarketAction window) =>
			window == null ? null :
			new BotEngine.EveOnline.Interface.MemoryStruct.WindowMarketAction(window.AsOldWindow());

		static public BotEngine.EveOnline.Interface.MemoryStruct.Tab AsOld(this Tab tab) =>
			tab == null ? null :
			new BotEngine.EveOnline.Interface.MemoryStruct.Tab(tab.AsOldUIElement(), tab?.Label?.AsOldUIElementLabelString(), tab?.LabelColorOpacityMilli, tab?.BackgroundOpacityMilli);

		static public BotEngine.EveOnline.Interface.MemoryStruct.WindowOverView AsOld(this Sanderling.Parse.IWindowOverview window) =>
			window == null ? null :
			new BotEngine.EveOnline.Interface.MemoryStruct.WindowOverView(window.AsOldWindow())
			{
				ListViewport = window?.ListView?.AsOldListView(),
				ViewportOverallLabelString = window?.ViewportOverallLabelString,
				PresetTab = window?.PresetTab?.Select(AsOld)?.ToArray(),
			};

		static public BotEngine.EveOnline.Interface.MemoryStruct.WindowRegionalMarket AsOld(this WindowRegionalMarket window) =>
			window == null ? null :
			new BotEngine.EveOnline.Interface.MemoryStruct.WindowRegionalMarket(window.AsOldWindow());

		static public BotEngine.EveOnline.Interface.MemoryStruct.WindowSelectedItemView AsOld(this IWindowSelectedItemView window) =>
			window == null ? null :
			new BotEngine.EveOnline.Interface.MemoryStruct.WindowSelectedItemView(window.AsOldWindow());

		static public BotEngine.EveOnline.Interface.MemoryStruct.WindowStack AsOld(this WindowStack window) =>
			window == null ? null :
			new BotEngine.EveOnline.Interface.MemoryStruct.WindowStack(window.AsOldWindow())
			{
				Tab = window?.Tab?.Select(AsOld)?.ToArray(),
				TabSelectedWindow = window?.TabSelectedWindow?.AsOldWindow(),
			};

		static public BotEngine.EveOnline.Interface.MemoryStruct.LobbyAgentEntry AsOld(this LobbyAgentEntry entry) =>
			entry == null ? null :
			new BotEngine.EveOnline.Interface.MemoryStruct.LobbyAgentEntry(
				entry.AsOldUIElement(),
				entry?.LabelText?.AsOldUIElementLabelString()?.ToArray(),
				entry?.StartConversationButton?.AsOldUIElement());

		static public BotEngine.EveOnline.Interface.MemoryStruct.WindowStationLobby AsOld(this IWindowStation window) =>
			window == null ? null :
			new BotEngine.EveOnline.Interface.MemoryStruct.WindowStationLobby(window.AsOldWindow())
			{
				AboveServicesLabel = window?.AboveServicesLabel?.AsOldUIElementLabelString()?.ToArray(),
				AgentEntry = window?.AgentEntry?.Select(AsOld)?.ToArray(),
				AgentEntryHeader = window?.AgentEntryHeader?.AsOldUIElementLabelString()?.ToArray(),
				ButtonUndock = window?.UndockButton?.AsOldUIElement(),
				ServiceButton = window?.ServiceButton?.Select(AsOld)?.ToArray(),
			};

		static public BotEngine.EveOnline.Interface.MemoryStruct.WindowSurveyScanView AsOld(this IWindowSurveyScanView window) =>
			window == null ? null :
			new BotEngine.EveOnline.Interface.MemoryStruct.WindowSurveyScanView(window.AsOldWindow());

		static public BotEngine.EveOnline.Interface.MemoryStruct.WindowTelecom AsOld(this WindowTelecom window) =>
			window == null ? null :
			new BotEngine.EveOnline.Interface.MemoryStruct.WindowTelecom(window.AsOldWindow());
	}
}
