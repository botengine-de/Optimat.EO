using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Optimat.GBS;

namespace Optimat.EveOnline.GBS
{
	/// <summary>
	/// Interaction logic for SictDataGridMengeMission.xaml
	/// </summary>
	public partial class SictDataGridMengeMission : UserControl
	{
		public static readonly RoutedEvent AuswaalZaitraumEvent =
			EventManager.RegisterRoutedEvent("AuswaalZaitraum", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(SictDataGridMengeMission));

		readonly public ObservableCollection<SictObservable<SictMissionReprInDataGrid>> MengeMissionRepr = new ObservableCollection<SictObservable<SictMissionReprInDataGrid>>();

		public SictDataGridMengeMission()
		{
			ColumnAktioonBescriftung = "Aktioon";
			ColumnTitelBescriftung = "Titel";
			ColumnInLocationSystemListePfaadAnzaalBescriftung = "Versuuc Pfaad" + Environment.NewLine + "Anzaal";
			ColumnDauerVonFüüreAusBeginBisCompleteBescriftung = "Dauer" + Environment.NewLine + "von Accept" + Environment.NewLine + "bis Complete";

			ColumnAktioonAuswaalZaitraumVonAcceptBisCompleteBescriftung = "Auswaal Zaitraum von Accept bis Complete";
			ColumnAktioonAuswaalZaitraumVonSictungFrühesteBisLezteBescriftung = "Auswaal Zaitraum von Sictung früheste bis lezte";

			InitializeComponent();

			DataGridMengeMission.ItemsSource = MengeMissionRepr;

			ColumnSictbarkaitAktualisiire();
		}

		void ColumnSictbarkaitAktualisiire()
		{
			if (!IsInitialized)
			{
				return;
			}

			ColumnAktioon.Visibility = InternColumnAktioonVerberge ? System.Windows.Visibility.Collapsed : System.Windows.Visibility.Visible;
		}

		bool InternColumnAktioonVerberge;

		public bool ColumnAktioonVerberge
		{
			set
			{
				InternColumnAktioonVerberge = value;

				ColumnSictbarkaitAktualisiire();
			}

			get
			{
				return InternColumnAktioonVerberge;
			}
		}

		public string ColumnAktioonBescriftung
		{
			set;
			get;
		}

		public string ColumnTitelBescriftung
		{
			set;
			get;
		}

		public string ColumnInLocationSystemListePfaadAnzaalBescriftung
		{
			set;
			get;
		}

		public string ColumnDauerVonFüüreAusBeginBisCompleteBescriftung
		{
			set;
			get;
		}

		public string ColumnAktioonAuswaalZaitraumVonFüüreAusBeginBisCompleteBescriftung
		{
			set;
			get;
		}

		public string ColumnAktioonAuswaalZaitraumVonAcceptBisCompleteBescriftung
		{
			set;
			get;
		}

		public string ColumnAktioonAuswaalZaitraumVonSictungFrühesteBisLezteBescriftung
		{
			set;
			get;
		}

		ContextMenu FürColumnAktioonContextMenuErsctele(
			SictObservable<SictMissionReprInDataGrid>	MissionRepr)
		{
			var	ListeItemBescriftungUndDelegate	= new	KeyValuePair<string,	Action<object,	RoutedEventArgs>>[]{
				new	KeyValuePair<string,	Action<object,	RoutedEventArgs>>(
					ColumnAktioonAuswaalZaitraumVonFüüreAusBeginBisCompleteBescriftung,
					DataGridMengeMission_MenuItem_AuswaalZaitraumVonFüüreAusBeginBisComplete),
				new	KeyValuePair<string,	Action<object,	RoutedEventArgs>>(
					ColumnAktioonAuswaalZaitraumVonAcceptBisCompleteBescriftung,
					DataGridMengeMission_MenuItem_AuswaalZaitraumVonAcceptBisComplete),
				new	KeyValuePair<string,	Action<object,	RoutedEventArgs>>(
					ColumnAktioonAuswaalZaitraumVonSictungFrühesteBisLezteBescriftung,
					DataGridMengeMission_MenuItem_AuswaalZaitraumVonSictungFrühesteBisLezte),
			};

			var ContextMenu = new ContextMenu();

			foreach (var MenuItemBescriftungUndDelegate in ListeItemBescriftungUndDelegate)
			{
				var MenuItem = new MenuItem();

				MenuItem.Header = MenuItemBescriftungUndDelegate.Key;
				MenuItem.Click += new	RoutedEventHandler(MenuItemBescriftungUndDelegate.Value);

				MenuItem.Tag = MissionRepr;

				ContextMenu.Items.Add(MenuItem);
			}

			return ContextMenu;
		}

		private void ButtonDataGridMengeMissionColumnAktioonZele_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				var Observable = Optimat.Glob.DataContextAusFrameworkElement<SictObservable<SictMissionReprInDataGrid>>(sender);

				var Source = e.OriginalSource as FrameworkElement;

				if (null == Source)
				{
					return;
				}

				var ContextMenu = FürColumnAktioonContextMenuErsctele(Observable);

				ContextMenu.Tag = Observable;

				Source.ContextMenu = ContextMenu;

				ContextMenu.IsOpen = true;
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}

		private void DataGridMengeMission_MenuItem_AuswaalZaitraumVonSictungFrühesteBisLezte(object sender, RoutedEventArgs e)
		{
			try
			{
				AuswaalZaitraumFürMission(sender as MenuItem, SictMissionZaitraumTypSictEnum.VonSictungFrühesteBisLezte);
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}

		void AuswaalZaitraumFürMission(
			MenuItem MenuItem,
			SictMissionZaitraumTypSictEnum ZaitraumTyp)
		{
			SictMissionZuusctand Mission = null;

			try
			{
				if (null == MenuItem)
				{
					return;
				}

				var MissionReprObservable = MenuItem.Tag as SictObservable<SictMissionReprInDataGrid>;

				var MissionRepr = MissionReprObservable.Wert;

				if (null == MissionRepr)
				{
					return;
				}

				Mission = MissionRepr.MissionBerecne();
			}
			finally
			{
				AuswaalZaitraumFürMission(Mission, ZaitraumTyp);
			}
		}

		void AuswaalZaitraumFürMission(
			SictMissionZuusctand Mission,
			SictMissionZaitraumTypSictEnum ZaitraumTyp)
		{
			if (!IsInitialized)
			{
				return;
			}

			Int64? AuswaalZaitraumBegin = null;
			Int64? AuswaalZaitraumEnde = null;

			try
			{
				SictMissionZuusctand.FürZaitraumTypBerecneBeginUndEnde(
					Mission,
					ZaitraumTyp,
					out	AuswaalZaitraumBegin,
					out	AuswaalZaitraumEnde);
			}
			finally
			{
				RaiseEvent(new SictAuswaalZaitraumEventArgs(AuswaalZaitraumEvent, this, AuswaalZaitraumBegin, AuswaalZaitraumEnde));
			}
		}

		private void DataGridMengeMission_MenuItem_AuswaalZaitraumVonAcceptBisComplete(object sender, RoutedEventArgs e)
		{
			if (!IsInitialized)
			{
				return;
			}

			try
			{
				AuswaalZaitraumFürMission(sender as MenuItem, SictMissionZaitraumTypSictEnum.VonAcceptBisComplete);
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}

		private void DataGridMengeMission_MenuItem_AuswaalZaitraumVonFüüreAusBeginBisComplete(object sender, RoutedEventArgs e)
		{
			if (!IsInitialized)
			{
				return;
			}

			try
			{
				AuswaalZaitraumFürMission(sender as MenuItem, SictMissionZaitraumTypSictEnum.VonFüüreAusBeginBisComplete);
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}

		public void DataGridMengeMissionHeaderLayoutUpdate()
		{
			Bib3.FCL.GBS.Extension.FrameworkElementInCanvasLaageRicteNaacDataGridMengeColumnHeader(
				DataGridMengeMissionHeaderPanelAusPräferenzEntscaidungVerhalte,
				DataGridMengeMission,
				new DataGridColumn[]{
					ColumnZuMissionVerhalteAktioonAcceptAktiiv,
					ColumnZuMissionVerhalteAktioonFüüreAusAktiiv,
					ColumnZuMissionVerhalteAktioonDeclineAktiiv,
					ColumnAusPräferenzEntscaidungFittingIdent});
		}

		private void UserControl_LayoutUpdated(object sender, EventArgs e)
		{
			try
			{
				DataGridMengeMissionHeaderLayoutUpdate();
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}

	}

	public class SictAuswaalZaitraumEventArgs : RoutedEventArgs
	{
		readonly	public Int64? ZaitraumBegin;
		readonly	public Int64? ZaitraumEnde;

		public SictAuswaalZaitraumEventArgs(
			RoutedEvent routedEvent,
			object	source,
			Int64? ZaitraumBegin,
			Int64? ZaitraumEnde)
			:
			base(routedEvent,	source)
		{
			this.ZaitraumBegin = ZaitraumBegin;
			this.ZaitraumEnde = ZaitraumEnde;
		}
	}
}
