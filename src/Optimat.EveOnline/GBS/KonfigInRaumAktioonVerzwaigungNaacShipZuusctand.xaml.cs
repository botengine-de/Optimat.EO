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
using Bib3.FCL.GBS;

namespace Optimat.EveOnline.GBS
{
	/// <summary>
	/// Interaction logic for SictKonfigInRaumAktioonVerzwaigungNaacShipZuusctand.xaml
	/// </summary>
	public partial class SictKonfigInRaumAktioonVerzwaigungNaacShipZuusctand : UserControl
	{
		public SictKonfigInRaumAktioonVerzwaigungNaacShipZuusctand()
		{
			InitializeComponent();

			AddHandler(SictAuswaalZaalGanzMitSliderUndTextBox.WertGeändertEvent, new RoutedEventHandler(AuswaalZaalWertGeändertEventHandler));
		}

		void AuswaalZaalWertGeändertEventHandler(object sender, RoutedEventArgs e)
		{
			try
			{
				if (null == e)
				{
					return;
				}

				var SctoierelementAuswaalZaal = e.OriginalSource as SictAuswaalZaalGanzMitSliderUndTextBox;

				if(null	== SctoierelementAuswaalZaal)
				{
					return;
				}

				var	AuswaalZaal	= SctoierelementAuswaalZaal.AuswaalZaal;

				if (SctoierelementAuswaalZaal == AuswaalScwelwertModuleRegenAinProzent)
				{
					if (AuswaalZaal.HasValue)
					{
						AuswaalScwelwertModuleRegenAusProzent.AuswaalZaal = Math.Max(AuswaalZaal.Value, AuswaalScwelwertModuleRegenAusProzent.AuswaalZaal ?? 0);
					}
				}

				if (SctoierelementAuswaalZaal == AuswaalScwelwertModuleRegenAusProzent)
				{
					if (AuswaalZaal.HasValue)
					{
						AuswaalScwelwertModuleRegenAinProzent.AuswaalZaal = Math.Min(AuswaalZaal.Value, AuswaalScwelwertModuleRegenAinProzent.AuswaalZaal ?? 0);
					}
				}

				if (Optimat.GBS.Glob.ObjektIstChildVonDependencyObject(AuswaalScwelwertGefectFortsaz, SctoierelementAuswaalZaal))
				{
					AuswaalScwelwertGefectBaitrit.KombiniireNaacMaximum(AuswaalScwelwertGefectFortsaz.KonfigBerecneAusGbs());
				}

				if (Optimat.GBS.Glob.ObjektIstChildVonDependencyObject(AuswaalScwelwertGefectBaitrit, SctoierelementAuswaalZaal))
				{
					AuswaalScwelwertGefectFortsaz.KombiniireNaacMininum(AuswaalScwelwertGefectBaitrit.KonfigBerecneAusGbs());
				}

				NaacKonfigGeändert();
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}

		void NaacKonfigGeändert()
		{
			this.RaiseEvent(new RoutedEventArgs(SictAutoKonfig.KonfigGeändertEvent, this));
		}

		public void KonfigScraibeNaacGbs(
			SictParamInRaumAktioonVerzwaigungNaacShipZuusctandScranke Konfig)
		{
			var ModuleRegenAinScrankeMili = (null == Konfig) ? null : Konfig.ModuleRegenAinScrankeMili;
			var ModuleRegenAusScrankeMili = (null == Konfig) ? null : Konfig.ModuleRegenAusScrankeMili;

			var GefectFortsazScranke = (null == Konfig) ? null : Konfig.GefectFortsazScranke;
			var GefectBaitritScranke = (null == Konfig) ? null : Konfig.GefectBaitritScranke;

			AuswaalScwelwertModuleRegenAinProzent.AuswaalZaal = ModuleRegenAinScrankeMili / 10;
			AuswaalScwelwertModuleRegenAusProzent.AuswaalZaal = ModuleRegenAusScrankeMili / 10;

			AuswaalScwelwertGefectFortsaz.KonfigScraibeNaacGbs(GefectFortsazScranke);
			AuswaalScwelwertGefectBaitrit.KonfigScraibeNaacGbs(GefectBaitritScranke);
		}

		static public SictVerzwaigungNaacShipZuusctandScranke BerecneAusFortsazScrankeUndBaitritScranke(
			SictVerzwaigungNaacShipZuusctandScranke	GefectFortsazScranke,
			SictVerzwaigungNaacShipZuusctandScranke	GefectBaitritScranke)
		{
			if (null == GefectFortsazScranke || null == GefectBaitritScranke)
			{
				return null;
			}

			var AnnaameArmorTank = GefectFortsazScranke.ArmorScrankeBetraagMili < 700;

			int? ShieldScrankeBetraagMili = null;
			int? ArmorScrankeBetraagMili = null;

			ShieldScrankeBetraagMili = Bib3.Glob.Max(GefectFortsazScranke.ShieldScrankeBetraagMili,
				(GefectFortsazScranke.ShieldScrankeBetraagMili + GefectBaitritScranke.ShieldScrankeBetraagMili * 2) / 3);

			ArmorScrankeBetraagMili = Bib3.Glob.Max(GefectFortsazScranke.ArmorScrankeBetraagMili,
				(GefectFortsazScranke.ArmorScrankeBetraagMili + GefectBaitritScranke.ArmorScrankeBetraagMili * 2) / 3);

			int? StructScrankeBetraagMili = Bib3.Glob.Max(GefectFortsazScranke.StructScrankeBetraagMili, 990);

			int? CapacitorScrankeBetraagMili = Bib3.Glob.Max(GefectFortsazScranke.CapacitorScrankeBetraagMili,
				(GefectFortsazScranke.CapacitorScrankeBetraagMili + GefectBaitritScranke.CapacitorScrankeBetraagMili)	/ 2);

			return new SictVerzwaigungNaacShipZuusctandScranke(
				ShieldScrankeBetraagMili,
				ArmorScrankeBetraagMili,
				StructScrankeBetraagMili,
				CapacitorScrankeBetraagMili);
		}

		public SictParamInRaumAktioonVerzwaigungNaacShipZuusctandScranke KonfigBerecneAusGbs()
		{
			var Konfig = new SictParamInRaumAktioonVerzwaigungNaacShipZuusctandScranke();

			Konfig.GefectFortsazScranke = AuswaalScwelwertGefectFortsaz.KonfigBerecneAusGbs();
			Konfig.GefectBaitritScranke	= AuswaalScwelwertGefectBaitrit.KonfigBerecneAusGbs();

			//	BeweegungUnabhängigVonGefectScranke vorersct automaatisc wääle, scpääter mööglicerwaise waal über GBS
			Konfig.BeweegungUnabhängigVonGefectScranke = BerecneAusFortsazScrankeUndBaitritScranke(Konfig.GefectFortsazScranke, Konfig.GefectBaitritScranke);

			Konfig.ModuleRegenAinScrankeMili = (int?)AuswaalScwelwertModuleRegenAinProzent.AuswaalZaal * 10;
			Konfig.ModuleRegenAusScrankeMili = (int?)AuswaalScwelwertModuleRegenAusProzent.AuswaalZaal * 10;

			return Konfig;
		}
	}
}
