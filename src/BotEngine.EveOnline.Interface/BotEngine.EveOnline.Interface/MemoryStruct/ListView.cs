using BotEngine.Interface;
using System.Collections.Generic;

namespace BotEngine.EveOnline.Interface.MemoryStruct
{
	public class ListColumnHeader : UIElementLabelString
	{
		public int? ColumnIndex;

		/// <summary>
		/// positive means ascending, negative descending.
		/// </summary>
		public int? SortDirection;

		public ListColumnHeader()
			:
			this((ListColumnHeader)null)
		{
		}

		public ListColumnHeader(UIElementLabelString Base)
			:
			base(Base)
		{
			ColumnIndex = (Base as ListColumnHeader)?.ColumnIndex;
			SortDirection = (Base as ListColumnHeader)?.SortDirection;
		}
	}

	/// <summary>
	/// can represent Item or Group.
	/// </summary>
	public class ListEntry : UIElement
	{
		public int? ContentBoundLeft;

		public UIElementLabelString Label;

		/// <summary>
		/// for each column, a reference of its header and the content for the cell.
		/// </summary>
		public KeyValuePair<ListColumnHeader, string>[] ListColumnCellLabel;

		public UIElement GroupExpander;

		public bool? IsGroup;

		public bool? IsExpanded;

		public bool? IsSelected;

		public ColorORGB[] ListBackgroundColor;

		public Sprite[] SetSprite;

		public ListEntry()
			:
			this((ListEntry)null)
		{
		}

		public ListEntry(UIElement Base)
			:
			base(Base)
		{
		}

		public ListEntry(ListEntry Base)
			:
			this((UIElement)Base)
		{
			ContentBoundLeft = Base?.ContentBoundLeft;

			Label = Base?.Label;
			ListColumnCellLabel = Base?.ListColumnCellLabel;

			GroupExpander = Base?.GroupExpander;
			IsGroup = Base?.IsGroup;
			IsExpanded = Base?.IsExpanded;
			IsSelected = Base?.IsSelected;
			ListBackgroundColor = Base?.ListBackgroundColor;
			SetSprite = Base?.SetSprite;
		}
	}


	public class ListViewport : UIElement
	{
		public ListColumnHeader[] ColumnHeader;

		public ListEntry[] Entry;

		public Scroll Scroll;

		public ListViewport()
		{
		}

		public ListViewport(UIElement Base)
			:
			base(Base)
		{
		}
	}

}
