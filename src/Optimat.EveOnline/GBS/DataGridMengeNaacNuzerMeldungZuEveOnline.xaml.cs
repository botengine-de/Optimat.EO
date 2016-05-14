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
	/// Interaction logic for DataGridMengeNaacNuzerMeldungZuEveOnline.xaml
	/// </summary>
	public partial class SictDataGridMengeNaacNuzerMeldungZuEveOnline : UserControl
	{
		readonly ObservableCollection<SictObservable<SictNaacNuzerMeldungZuEveOnlineRepr>> MengeMeldungRepr =
			new ObservableCollection<SictObservable<SictNaacNuzerMeldungZuEveOnlineRepr>>();

		public SictDataGridMengeNaacNuzerMeldungZuEveOnline()
		{
			InitializeComponent();

			DataGrid.ItemsSource = MengeMeldungRepr;
		}

		public void Repräsentiire(
			IEnumerable<SictNaacNuzerMeldungZuEveOnlineSictNuzer> MengeMeldung,
			Int64? ZaitMili = null)
		{
			var StopwatchZaitMili = Bib3.Glob.StopwatchZaitMiliSictInt();

			Bib3.Glob.PropagiireListeRepräsentatioon(
				MengeMeldung,
				MengeMeldungRepr as IList<SictObservable<SictNaacNuzerMeldungZuEveOnlineRepr>>,
				(Meldung) => new SictObservable<SictNaacNuzerMeldungZuEveOnlineRepr>(new SictNaacNuzerMeldungZuEveOnlineRepr(Meldung)),
				(KandidaatRepr, Meldung) => KandidaatRepr.Wert.Repräsentiirte == Meldung,
				(Repr, Meldung) => Repr.Wert.ZaitMili	= ZaitMili);

			foreach (var Repr in MengeMeldungRepr)
			{
				var ReprRaisePropertyChangedLezteAlterMili = StopwatchZaitMili - Repr.RaisePropertyChangedLezteZaitStopwatchMili;

				if (ReprRaisePropertyChangedLezteAlterMili < 500)
				{
					continue;
				}

				Repr.RaisePropertyChanged();
			}
		}
	}
}
