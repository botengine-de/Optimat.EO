using BotEngine.Common;
using System.Linq;

namespace BotEngine.EveOnline.Parse
{
	static public class ListEntry
	{
		static public string CellValueFromColumnHeader(
			this Interface.MemoryStruct.ListEntry ListEntry,
			string HeaderLabel) =>
			ListEntry?.ListColumnCellLabel
			?.FirstOrDefault(Cell => (Cell.Key?.Label).EqualsIgnoreCase(HeaderLabel))
			.Value;

		static public string ColumnTypeValue(this Interface.MemoryStruct.ListEntry ListEntry) =>
			CellValueFromColumnHeader(ListEntry, "type");

		static public string ColumnNameValue(this Interface.MemoryStruct.ListEntry ListEntry) =>
			CellValueFromColumnHeader(ListEntry, "name");

		static public string ColumnDistanceValue(this Interface.MemoryStruct.ListEntry ListEntry) =>
			CellValueFromColumnHeader(ListEntry, "distance");

	}
}
