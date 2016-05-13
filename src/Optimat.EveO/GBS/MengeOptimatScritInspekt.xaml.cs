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
using Newtonsoft.Json;
using Optimat.GBS;

namespace Optimat.EveOnline.GBS
{
	/// <summary>
	/// Interaction logic for SictMengeOptimatScritInspekt.xaml
	/// </summary>
	public partial class SictMengeOptimatScritInspekt : UserControl
	{
		readonly	public	ObservableCollection<SictObservable<SictOptimatScritRepr>> MengeOptimatScritRepr =
			new ObservableCollection<SictObservable<SictOptimatScritRepr>>();

		public string ListeOptimatScritScraibeNaacDataiDataiNaameSctandard;

		public string ListeOptimatScritScraibeNaacDataiDataiPfaadSctandard;

		public double DataGridHeight
		{
			set;
			get;
		}

		public SictMengeOptimatScritInspekt()
		{
			DataGridHeight = 144;

			InitializeComponent();

			DataGridMengeOptimationScrit.ItemsSource = MengeOptimatScritRepr;
		}

		public void DataGridMengeOptimationScritHeaderLayoutUpdate()
		{
			Bib3.FCL.GBS.Extension.FrameworkElementInCanvasLaageRicteNaacDataGridMengeColumnHeader(
				DataGridMengeOptimationScritHeaderSaiteNuzerBegin,
				DataGridMengeOptimationScrit,
				new DataGridColumn[]{
					DataGridMengeOptimationScritWirkungColumnZiilProcessLeeseBeginZaitKalender,
					DataGridMengeOptimatScritVonZiilProcessLeeseZaitDistanzVonBeginBisEnde
				});

			Bib3.FCL.GBS.Extension.FrameworkElementInCanvasLaageRicteNaacDataGridMengeColumnHeader(
				DataGridMengeOptimationScritHeaderSaiteServer,
				DataGridMengeOptimationScrit,
				new DataGridColumn[]{
					DataGridMengeOptimationScritVorsclaagWirkungZwekSictString,
				});

			Bib3.FCL.GBS.Extension.FrameworkElementInCanvasLaageRicteNaacDataGridMengeColumnHeader(
				DataGridMengeOptimationScritHeaderSaiteNuzerEnde,
				DataGridMengeOptimationScrit,
				new DataGridColumn[]{
					DataGridMengeOptimationScritZaitDistanzVonProcessLeeseBeginBisNaacProcessWirkungEnde,
					DataGridMengeOptimationScritWirkungFüüreAusErfolg,
				});

			Bib3.FCL.GBS.Extension.FrameworkElementInCanvasLaageRicteNaacDataGridMengeColumnHeader(
				DataGridMengeOptimationScritHeaderSaiteNuzerBeginTailVonZiilProcessLeese,
				DataGridMengeOptimationScrit,
				new DataGridColumn[]{
					DataGridMengeOptimationScritWirkungColumnZiilProcessLeeseBeginZaitKalender,
					DataGridMengeOptimatScritVonZiilProcessLeeseZaitDistanzVonBeginBisEnde
				});
		}

		public void DataGridMengeOptimationScritSortNaacZait()
		{
			var SortOrder = System.ComponentModel.ListSortDirection.Descending;

			var SortDescriptionZiilProcessLeseBeginZait = new System.ComponentModel.SortDescription(
				DataGridMengeOptimationScritWirkungColumnZiilProcessLeeseBeginZaitKalender.SortMemberPath, SortOrder);

			var ListeSortDescription = new System.ComponentModel.SortDescription[]{
				SortDescriptionZiilProcessLeseBeginZait};

			foreach (var Column in DataGridMengeOptimationScrit.Columns)
			{
				Column.SortDirection = null;
			}

			DataGridMengeOptimationScritWirkungColumnZiilProcessLeeseBeginZaitKalender.SortDirection = SortOrder;

			DataGridMengeOptimationScrit.Items.SortDescriptions.Clear();

			foreach (var SortDescription in ListeSortDescription)
			{
				DataGridMengeOptimationScrit.Items.SortDescriptions.Add(SortDescription);
			}
		}

		private void DataGridMengeOptimationScrit_LayoutUpdated(object sender, EventArgs e)
		{
			try
			{
				DataGridMengeOptimationScritHeaderLayoutUpdate();
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}

		string ListeOptimatScritSictSerielBerecne(
			bool GbsBaumScraibe = false)
		{
			var MengeOptimatScritRepr = this.MengeOptimatScritRepr;

			var MengeOptimatScrit = (null == MengeOptimatScritRepr) ? null : MengeOptimatScritRepr.Select((Repr) => Repr.Wert.Repräsentiirte).ToArray();

			var ListeOptimatScritAbbild =
				(null == MengeOptimatScrit) ? null :
				MengeOptimatScrit
				.Select((OptimatScrit) => SictOptimatScrit.OptimatScritSictFürBerict(OptimatScrit,	GbsBaumScraibe))
				.Where((Kandidaat) => null	!= Kandidaat)
				.OrderBy((Kandidaat) =>
					{
						var VonZiilProcessLeese = Kandidaat.VonProcessMesung;

						if (null == VonZiilProcessLeese)
						{
							return null;
						}

						return (Int64?)VonZiilProcessLeese.BeginZait;
					})
				.ToArray();

			var SerializerSettings = new JsonSerializerSettings();

			SerializerSettings.DefaultValueHandling = DefaultValueHandling.Ignore;

			var SictStringAbbild = JsonConvert.SerializeObject(ListeOptimatScritAbbild, Formatting.Indented, SerializerSettings);

			return SictStringAbbild;
		}

		public void ListeOptimatScritScraibeNaacDataiUndBericteNaacGbs(
			string DataiPfaad,
			bool	GbsBaumScraibe	= false)
		{
			var Aktioon = new Action(() =>
			{
				var ListeOptimatScritSictSeriel = this.ListeOptimatScritSictSerielBerecne();

				var ListeOptimatScritSictListeOktet = Encoding.UTF8.GetBytes(ListeOptimatScritSictSeriel);

				Bib3.FCL.Glob.ZuDataiPfaadErscteleVerzaicnisFalsNocNictExistent(DataiPfaad);

				Optimat.Glob.ScraibeNaacDataiMitPfaad(DataiPfaad, ListeOptimatScritSictListeOktet);
			});

			Optimat.GBS.Glob.VersuucAktioonMitBerictNaacTextBoxAnim(
				Aktioon,
				"write log to file (path= \"" + DataiPfaad + "\")",
				ListeOptimatScritScraibeNaacDataiErgeebnisBerict);
		}

		private void ButtonListeOptimatScritScraibeNaacDatai_Drop(object sender, DragEventArgs e)
		{
			try
			{
				var DataiPfaad = Bib3.FCL.Glob.DataiPfaadAlsKombinatioonAusSctandardPfaadUndFileDrop(ListeOptimatScritScraibeNaacDataiDataiNaameSctandard, e);

				ListeOptimatScritScraibeNaacDataiUndBericteNaacGbs(DataiPfaad);
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}

		private void ButtonListeOptimatScritScraibeNaacDatai_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				ListeOptimatScritScraibeNaacDataiUndBericteNaacGbs(ListeOptimatScritScraibeNaacDataiDataiPfaadSctandard);
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}
	}
}
