using System;
using System.Collections.Generic;
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
using Bib3;

namespace Optimat.EveOnline.GBS
{
	/// <summary>
	/// Interaction logic for MengeNaacNuzerMeldungZuEveOnlineMitSictDetail.xaml
	/// </summary>
	public partial class SictMengeNaacNuzerMeldungZuEveOnlineMitSictDetail : UserControl
	{
		public SictNaacNuzerMeldungZuEveOnlineSictNuzer[] Repräsentiirte
		{
			private set;
			get;
		}

		public SictMengeNaacNuzerMeldungZuEveOnlineMitSictDetail()
		{
			InitializeComponent();
		}

		public void Repräsentiire(
			IEnumerable<SictNaacNuzerMeldungZuEveOnlineSictNuzer> MengeMeldung,
			Int64? ZaitMili = null)
		{
			var	ArrayMeldung	= MengeMeldung.ToArrayNullable();

			try
			{
				DataGridMengeMeldung.Repräsentiire(ArrayMeldung, ZaitMili);
			}
			finally
			{
				this.Repräsentiirte = ArrayMeldung;
			}
		}
	}

}
