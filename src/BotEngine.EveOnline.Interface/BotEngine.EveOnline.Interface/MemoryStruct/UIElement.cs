using ExtractFromOldAssembly.Bib3;
using System;

namespace BotEngine.EveOnline.Interface.MemoryStruct
{
	public class ObjectIdInMemory : ObjectIdInt64
	{
		ObjectIdInMemory()
		{
		}

		public ObjectIdInMemory(ObjectIdInt64 Base)
			: base(Base)
		{
		}

		public ObjectIdInMemory(Int64 Id)
			: base(Id)
		{
		}
	}

	public class UIElement : ObjectIdInMemory
	{
		public OrtogoonInt Region;

		/// <summary>
		/// Element occludes other Elements with lower Value.
		/// </summary>
		public int? InTreeIndex;

		/// <summary>
		/// subregion which should be used to open a contextmenu.
		/// </summary>
		/// <returns></returns>
		virtual public UIElement MenuRootRegionCompute()
		{
			return this;
		}

		public UIElement()
			:
			this((UIElement)null)
		{
		}

		public UIElement(UIElement Base)
			:
			this(Base, Base?.Region ?? OrtogoonInt.Leer, Base?.InTreeIndex)
		{
		}

		public UIElement(
			ObjectIdInt64 Base,
			OrtogoonInt Region = default(OrtogoonInt),
			int? InTreeIndex = null)
			:
			base(Base)
		{
			this.Region = Region;

			this.InTreeIndex = InTreeIndex;
		}

		public override string ToString() =>
			this.SensorObjectToString();
	}

	public class UIElementLabelString : UIElement
	{
		public string Label;

		public UIElementLabelString(
			UIElement Base,
			string Label = null)
			:
			base(Base)
		{
			this.Label = Label;
		}

		public UIElementLabelString(UIElementLabelString Base)
			:
			this(Base, Base?.Label)
		{
		}

		public UIElementLabelString()
		{
		}
	}

	public class UIElementTextBox : UIElementLabelString
	{
		public UIElementTextBox(UIElement Base, string Label = null)
			:
			base(Base, Label)
		{
		}

		public UIElementTextBox(UIElementLabelString Base)
			:
			this(Base, Base?.Label)
		{
		}

		public UIElementTextBox()
		{
		}
	}
}
