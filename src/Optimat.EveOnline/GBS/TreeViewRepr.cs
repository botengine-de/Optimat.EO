using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json.Linq;

namespace JsonViewer
{
	static	public	class SictTreeViewRepr
	{
		static public IEnumerable<TreeViewItem> InBaumTreeViewItemExpandiirePfaadTag(
			TreeViewItem Wurzel,
			object[] PfaadListeTag)
		{
			if (null == Wurzel)
			{
				return null;
			}

			if (null == PfaadListeTag)
			{
				return null;
			}

			if (PfaadListeTag.Length < 1)
			{
				return null;
			}

			var PfaadListeTagNääxte = PfaadListeTag[0];

			if (Wurzel.Tag != PfaadListeTagNääxte)
			{
				return null;
			}

			Wurzel.IsExpanded = true;

			if (PfaadListeTag.Length < 2)
			{
				return new	TreeViewItem[]{	Wurzel};
			}

			FürReprScteleSicerItemsErsctelt(Wurzel);

			var PfaadListeTagFürItem = PfaadListeTag.Skip(1).ToArray();

			var MengeItem = Wurzel.Items.OfType<TreeViewItem>().ToArray();

			IEnumerable<TreeViewItem> MengeRepr = new TreeViewItem[0];

			foreach (var item in MengeItem)
			{
				var AusItemMengeRepr = InBaumTreeViewItemExpandiirePfaadTag(item, PfaadListeTagFürItem);

				if (null != AusItemMengeRepr)
				{
					MengeRepr = MengeRepr.Concat(AusItemMengeRepr);
				}
			}

			return MengeRepr;
		}

		static public TreeViewItem ReprErsctele(object Repräsentiirte, string HeaderVorgaabe = null)
		{
			var RepräsentiirteAsJProperty = Repräsentiirte as JProperty;

			var Repr = new TreeViewItem();

			Repr.Tag = Repräsentiirte;

			string Header = "JSON";

			ContextMenu Menu = null;

			if (null != RepräsentiirteAsJProperty)
			{
				Header = RepräsentiirteAsJProperty.Name;

				var PropertyValue = RepräsentiirteAsJProperty.Value;

				var ValueType = PropertyValue.Type;

				if (JTokenType.Object != ValueType)
				{
					if (JTokenType.Array == ValueType)
					{
						Header = "\"" + RepräsentiirteAsJProperty.Name + "\" : " + ValueType.ToString();
					}
					else
					{
						Header = RepräsentiirteAsJProperty.ToString();
					}

					Menu = new ContextMenu();

					{
						var MenuItemScraibeNaacClipboard = new MenuItem();

						MenuItemScraibeNaacClipboard.Header = ".scraibe naac Clipboard";
						MenuItemScraibeNaacClipboard.Click += ButtonScraibeNaacClipboard_Click;
						MenuItemScraibeNaacClipboard.Tag = Repr;

						Menu.Items.Add(MenuItemScraibeNaacClipboard);
					}
					{
						var MenuItemValueScraibeNaacClipboard = new MenuItem();

						MenuItemValueScraibeNaacClipboard.Header = ".Value.scraibe naac Clipboard";
						MenuItemValueScraibeNaacClipboard.Click += MenuItemValueScraibeNaacClipboard_Click;
						MenuItemValueScraibeNaacClipboard.Tag = Repr;

						Menu.Items.Add(MenuItemValueScraibeNaacClipboard);
					}
				}
			}

			if (null != HeaderVorgaabe)
			{
				Header = HeaderVorgaabe;
			}

			Repr.Header = Header;
			Repr.ContextMenu = Menu;

			Repr.Loaded += Repr_Loaded;

			return Repr;
		}

		static public JContainer JContainerFürContextMenuEvent(object sender, RoutedEventArgs e)
		{
			var SenderAsFrameworkElement = sender as FrameworkElement;

			if (null == SenderAsFrameworkElement)
			{
				return null;
			}

			var Repr = SenderAsFrameworkElement.Tag as FrameworkElement;

			if (null == Repr)
			{
				return null;
			}

			var TagAsJContainer = Repr.Tag as JContainer;

			return TagAsJContainer;
		}

		static public void MenuItemValueScraibeNaacClipboard_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				var Container = JContainerFürContextMenuEvent(sender, e);

				if (null == Container)
				{
					return;
				}

				var ContainerAsProperty = Container as JProperty;

				if (null == ContainerAsProperty)
				{
					return;
				}

				Clipboard.SetText(ContainerAsProperty.Value.ToString());
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}

		static public	void ButtonScraibeNaacClipboard_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				var Container = JContainerFürContextMenuEvent(sender, e);

				if (null == Container)
				{
					return;
				}

				Clipboard.SetText(Container.ToString());
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}

		static public void FürReprScteleSicerItemsErsctelt(ItemsControl Repr)
		{
			if (null == Repr)
			{
				return;
			}

			if (0 < Repr.Items.Count)
			{
				return;
			}

			var Tag = Repr.Tag;

			var TagAlsJObject = Tag as JObject;
			var TagAlsJProperty = Tag as JProperty;
			var TagAlsJContainer = Tag as JContainer;

			var ListeNaameMitChild = new List<KeyValuePair<string, object>>();

			if (null != TagAlsJProperty)
			{
				var TagAlsJPropertyValue = TagAlsJProperty.Value;

				if (null != TagAlsJPropertyValue)
				{
					ListeNaameMitChild.AddRange(
						TagAlsJPropertyValue.Children()
						.Select((Child, Index) => new KeyValuePair<string, object>(
							(TagAlsJPropertyValue is JArray) ?
							("[" + Index.ToString() + "]") : null,
							Child)));
				}
			}
			else
			{
				if (null != TagAlsJContainer)
				{
					ListeNaameMitChild.AddRange(
						TagAlsJContainer.Children()
						.Select((Child, Index) => new KeyValuePair<string, object>(null, Child)));
				}
			}

			foreach (var Child in ListeNaameMitChild)
			{
				var ChildRepr = ReprErsctele(Child.Value, Child.Key);

				Repr.Items.Add(ChildRepr);
			}
		}

		static public void Repr_Loaded(object sender, RoutedEventArgs e)
		{
			var SenderAsItemsControl = sender as ItemsControl;

			FürReprScteleSicerItemsErsctelt(SenderAsItemsControl);
		}

	}
}
