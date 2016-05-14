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
using Optimat.GBS;

namespace Optimat.EveOnline.GBS
{
	/// <summary>
	/// Interaction logic for KonfigAntailShieldArmorStructMitScalterBinär.xaml
	/// </summary>
	public partial class SictKonfigAntailShieldArmorStructCapacitorMitScalterBinär : UserControl
	{
		public SictKonfigAntailShieldArmorStructCapacitorMitScalterBinär()
		{
			InitializeComponent();
		}

		static void	ScraibeWertMiliNaacSctoierelementProzent(
			Bib3.FCL.GBS.SictAuswaalZaalGanzMitSliderUndTextBox SctoierelementZiilProzent,
			KeyValuePair<int,	bool>?	WertZiil)
		{
			if (null == SctoierelementZiilProzent)
			{
				return;
			}

			if (WertZiil.HasValue)
			{
				SctoierelementZiilProzent.AuswaalScalterBinär = WertZiil.Value.Value;
				SctoierelementZiilProzent.AuswaalZaal = WertZiil.Value.Key / 10;
			}
			else
			{
				SctoierelementZiilProzent.AuswaalScalterBinär = false;
				SctoierelementZiilProzent.AuswaalZaal = null;
			}
		}
			

		public void KonfigScraibeNaacGbs(
			SictVerzwaigungNaacShipZuusctandScranke Konfig)
		{
			var ShieldScrankeBetraagMiliUndAin = (null == Konfig) ? null : Konfig.ShieldScrankeBetraagMiliUndAin;
			var ArmorScrankeBetraagMiliUndAin = (null == Konfig) ? null : Konfig.ArmorScrankeBetraagMiliUndAin;
			var StructScrankeBetraagMiliUndAin = (null == Konfig) ? null : Konfig.StructScrankeBetraagMiliUndAin;
			var CapacitorScrankeBetraagMiliUndAin = (null == Konfig) ? null : Konfig.CapacitorScrankeBetraagMiliUndAin;

			ScraibeWertMiliNaacSctoierelementProzent(AuswaalShieldProzent, ShieldScrankeBetraagMiliUndAin);
			ScraibeWertMiliNaacSctoierelementProzent(AuswaalArmorProzent, ArmorScrankeBetraagMiliUndAin);
			ScraibeWertMiliNaacSctoierelementProzent(AuswaalStructProzent, StructScrankeBetraagMiliUndAin);
			ScraibeWertMiliNaacSctoierelementProzent(AuswaalCapacitorProzent, CapacitorScrankeBetraagMiliUndAin);
		}

		public SictVerzwaigungNaacShipZuusctandScranke KonfigBerecneAusGbs()
		{
			var Konfig = new SictVerzwaigungNaacShipZuusctandScranke();

			var ShieldProzent = (int?)AuswaalShieldProzent.AuswaalZaal;
			var ArmorProzent = (int?)AuswaalArmorProzent.AuswaalZaal;
			var StructProzent = (int?)AuswaalStructProzent.AuswaalZaal;
			var CapacitorProzent = (int?)AuswaalCapacitorProzent.AuswaalZaal;

			Konfig.ShieldScrankeBetraagMiliUndAin	=
				ShieldProzent.HasValue	? new	KeyValuePair<int,	bool>(ShieldProzent.Value	* 10,	true	== AuswaalShieldProzent.AuswaalScalterBinär)	:
				(KeyValuePair<int,	bool>?)null;

			Konfig.ArmorScrankeBetraagMiliUndAin	=
				ArmorProzent.HasValue ? new KeyValuePair<int, bool>(ArmorProzent.Value * 10, true == AuswaalArmorProzent.AuswaalScalterBinär) :
				(KeyValuePair<int,	bool>?)null;

			Konfig.StructScrankeBetraagMiliUndAin	=
				StructProzent.HasValue	? new	KeyValuePair<int,	bool>(StructProzent.Value	* 10,	true	== AuswaalStructProzent.AuswaalScalterBinär)	:
				(KeyValuePair<int, bool>?)null;

			Konfig.CapacitorScrankeBetraagMiliUndAin	=
				CapacitorProzent.HasValue ? new KeyValuePair<int, bool>(CapacitorProzent.Value * 10, true == AuswaalCapacitorProzent.AuswaalScalterBinär) :
				(KeyValuePair<int, bool>?)null;

			return Konfig;
		}

		public void KombiniireNaacMininum(
			SictVerzwaigungNaacShipZuusctandScranke ScrankeWert)
		{
			ApliziireFunktioonKombinatioon(ScrankeWert, (VorherWert, NoiWert) => VorherWert.HasValue ? Bib3.Glob.Min(VorherWert, NoiWert) : null);
		}

		public void KombiniireNaacMaximum(
			SictVerzwaigungNaacShipZuusctandScranke ScrankeWert)
		{
			ApliziireFunktioonKombinatioon(ScrankeWert, (VorherWert, NoiWert) => VorherWert.HasValue ? Bib3.Glob.Max(VorherWert, NoiWert) : null);
		}

		public void ApliziireFunktioonKombinatioon(
			SictVerzwaigungNaacShipZuusctandScranke	WertNoi,
			Func<int?,	int?,	int?>	FunktioonKombinatioon)
		{
			if (null == WertNoi)
			{
				return;
			}

			if (null == FunktioonKombinatioon)
			{
				return;
			}

			var VorherShield = (int?)AuswaalShieldProzent.AuswaalZaal;
			var VorherArmor = (int?)AuswaalArmorProzent.AuswaalZaal;
			var VorherStruct = (int?)AuswaalStructProzent.AuswaalZaal;
			var VorherCapacitor = (int?)AuswaalCapacitorProzent.AuswaalZaal;

			var	WertNoiShieldScrankeMili	= WertNoi.ShieldScrankeBetraagMiliUndAin;
			var	WertNoiArmorScrankeMili	= WertNoi.ArmorScrankeBetraagMiliUndAin;
			var	WertNoiStructScrankeMili	= WertNoi.StructScrankeBetraagMiliUndAin;
			var	WertNoiCapacitorScrankeMili	= WertNoi.CapacitorScrankeBetraagMiliUndAin;

			var WertNoiShield = WertNoiShieldScrankeMili.HasValue ? (WertNoiShieldScrankeMili.Value.Key / 10) : (int?)null;
			var WertNoiArmor = WertNoiArmorScrankeMili.HasValue ? (WertNoiArmorScrankeMili.Value.Key / 10) : (int?)null;
			var WertNoiStruct = WertNoiStructScrankeMili.HasValue ? (WertNoiStructScrankeMili.Value.Key / 10) : (int?)null;
			var WertNoiCapacitor = WertNoiCapacitorScrankeMili.HasValue ? (WertNoiCapacitorScrankeMili.Value.Key / 10) : (int?)null;

			var KombiShield = FunktioonKombinatioon(VorherShield, WertNoiShield);
			var KombiArmor = FunktioonKombinatioon(VorherArmor, WertNoiArmor);
			var KombiStruct = FunktioonKombinatioon(VorherStruct, WertNoiStruct);
			var KombiCapacitor = FunktioonKombinatioon(VorherCapacitor, WertNoiCapacitor);

			if (KombiShield.HasValue)
			{
				AuswaalShieldProzent.AuswaalZaal = KombiShield;
			}

			if (KombiArmor.HasValue)
			{
				AuswaalArmorProzent.AuswaalZaal = KombiArmor;
			}

			if (KombiStruct.HasValue)
			{
				AuswaalStructProzent.AuswaalZaal = KombiStruct;
			}

			if (KombiCapacitor.HasValue)
			{
				AuswaalCapacitorProzent.AuswaalZaal = KombiCapacitor;
			}
		}
	}
}
