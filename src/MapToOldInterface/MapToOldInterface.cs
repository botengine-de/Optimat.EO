using Bib3;
using Bib3.Geometrik;
using BotEngine;
using ExtractFromOldAssembly.Bib3;
using Sanderling.Interface.MemoryStruct;
using Sanderling.Parse;
using System;
using System.Collections.Generic;
using System.Linq;

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
				AbovemainMessage = parsed?.AbovemainMessage?.Select(AsOldUIElementLabelString)?.ToArray(),
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
			};
		}

		static public OrtogoonInt AsOrtogoon(this RectInt rect) =>
			new OrtogoonInt(rect.Min0, rect.Min1, rect.Max0, rect.Max1);

		static public BotEngine.EveOnline.Interface.MemoryStruct.UIElement AsOldUIElement(
			this IUIElement uiElement)
		{
			if (uiElement == null)
				return null;

			return new BotEngine.EveOnline.Interface.MemoryStruct.UIElement(Base: uiElement as ObjectIdInt64)
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
				ExpandedContentLabel = infoPanel?.ExpandedContent?.LabelText?.Select(AsOldUIElementLabelString)?.ToArray(),
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
				ListMissionButton = infoPanelMissions?.ListMissionButton?.Select(AsOldUIElementLabelString)?.ToArray(),
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
			.ConcatNullable()
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
					ListLabelString = rowContainerListElement?.OfType<IUIElementText>()?.Select(AsOldUIElementLabelString)?.ToArray(),
					IconTextureId = rowContainerListElement?.OfType<ISprite>()?.Select(AsOld)?.ToArray(),
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
				ListLabelString = moduleButtonTooltip?.LabelText?.Select(AsOldUIElementLabelString)?.ToArray(),
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
	}
}
