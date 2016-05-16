using Bib3;
using BotEngine.Common;
using BotEngine.EveOnline.Interface.MemoryStruct;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BotEngine.EveOnline.Parse
{
	static public class SpaceObject
	{
		public const string SpaceObjectMenuEntryRemoveFromOverviewRegexPattern = @"remove.*from\s*overview";

		public const string SpaceObjectMenuEntryLockRegexPattern = @"(?<!\w)lock";

		public const string SpaceObjectMenuEntryUnlockRegexPattern = @"unlock";

		public const string SpaceObjectMenuEntryApproachRegexPattern = "approach";

		static readonly public KeyValuePair<ShipManeuverTypeEnum, string>[] SpaceObjectSetManeuverTypeAndMenuEntryRegexPattern = new[]
		{
			new KeyValuePair<ShipManeuverTypeEnum, string>(ShipManeuverTypeEnum.Approach, SpaceObjectMenuEntryApproachRegexPattern),
		};

		/// <summary>
		/// Example:
		/// "Asteroid ( Concentrated Veldspar )"
		/// "Asteroid(Veldspar)"
		/// </summary>
		const string TargetLabelAsteroidRegexPattern = "Asteroid.*[^\\d\\w\\s]+([\\d\\w\\s]+)";

		static public MenuEntry EntryLock(this Menu Menu) =>
			Menu?.Entry?.FirstOrDefault(Entry => Entry?.Label?.RegexMatchSuccessIgnoreCase(SpaceObjectMenuEntryLockRegexPattern) ?? false);

		static public MenuEntry EntryUnlock(this Menu Menu) =>
			Menu?.Entry?.FirstOrDefault(Entry => Entry?.Label?.RegexMatchSuccessIgnoreCase(SpaceObjectMenuEntryUnlockRegexPattern) ?? false);

		static public MenuEntry EntryRemoveFromOverview(this Menu Menu) =>
			Menu?.Entry?.FirstOrDefault(Entry => Entry?.Label?.RegexMatchSuccessIgnoreCase(SpaceObjectMenuEntryRemoveFromOverviewRegexPattern) ?? false);

		static public KeyValuePair<OverviewEntry, IEnumerable<Menu>>? OverviewEntryMenu(this MemoryMeasurement MemoryMeasurement)
		{
			var OverviewEntry =
				MemoryMeasurement?.WindowOverview
				?.Select(WindowOverview => WindowOverview?.ListViewport?.Entry)
				?.ConcatNullable()
				?.OfType<OverviewEntry>()
				?.FirstOrDefault(Entry => Entry?.IsSelected ?? false);

			if (null == OverviewEntry)
			{
				return null;
			}

			if (null == MemoryMeasurement?.Menu?.FirstOrDefault()?.EntryRemoveFromOverview())
			{
				return null;
			}

			return new KeyValuePair<OverviewEntry, IEnumerable<Menu>>(OverviewEntry, MemoryMeasurement?.Menu);
		}

		static public string OreTypeString(this OverviewEntry OverviewEntry) =>
			(OverviewEntry?.ColumnNameValue().RegexMatchSuccessIgnoreCase("Asteroid.*") ?? false) ?
			OverviewEntry?.ColumnTypeValue() : null;

		static public OreTypeEnum? OreTypeEnum(this OverviewEntry OverviewEntry) =>
			Bib3.Extension.EnumGetValues<OreTypeEnum>()
			?.CastToNullable()
			?.FirstOrDefault(OreType => OverviewEntry.OreTypeString().RegexMatchSuccessIgnoreCase(OreType?.RegexPattern()));

		static public OreTypeEnum? ExtractAsteroidOreType(
			this ShipUiTarget Target,
			out string OreTypeString)
		{
			OreTypeString = null;

			var LabelAggregatedLessXml =
				Target?.ListLabelString?.Select(Label => Label?.Label)?.StringJoin(" ")?.RemoveXmlTag()?.Trim();

			if (null == LabelAggregatedLessXml)
			{
				return null;
			}

			var Match = Regex.Match(
				LabelAggregatedLessXml, TargetLabelAsteroidRegexPattern, RegexOptions.IgnoreCase);

			if (!Match.Success)
			{
				return null;
			}

			OreTypeString = Match.Groups[1].Value.Trim();

			return OreTypeString.AsOreTypeEnum();
		}

		static public OreTypeEnum? ExtractAsteroidOreType(this ShipUiTarget Target)
		{
			string OreTypeString;

			return ExtractAsteroidOreType(Target, out OreTypeString);
		}

	}
}