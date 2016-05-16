using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
	/// Interaction logic for SictAutoKonfigFittingVerkürzt.xaml
	/// </summary>
	public partial class SictAutoKonfigFittingVerkürzt : UserControl
	{
		readonly ObservableCollection<SictZuFactionFittingBezaicnerReprInDataGrid> DataGridZuFactionFittingBezaicnerMengeRepr =
			new ObservableCollection<SictZuFactionFittingBezaicnerReprInDataGrid>();

		static readonly SictFactionSictEnum[] MengeFactionZurAuswaal = new SictFactionSictEnum[]{
			SictFactionSictEnum.Andere,
			SictFactionSictEnum.Amarr_Empire,
			SictFactionSictEnum.Angel_Cartel,
			SictFactionSictEnum.Caldari_State,
			SictFactionSictEnum.EoM,
			SictFactionSictEnum.Galente_Federation,
			SictFactionSictEnum.Guristas_Pirates,
			SictFactionSictEnum.Khanid_Kingdom,
			SictFactionSictEnum.Mercenaries,
			SictFactionSictEnum.Minmatar_Republic,
			SictFactionSictEnum.Mordus_Legion,
			SictFactionSictEnum.Rogue_Drones,
			SictFactionSictEnum.Sanshas_Nation,
			SictFactionSictEnum.Scions_of_the_Superior_Gene,
			SictFactionSictEnum.Serpentis,
			SictFactionSictEnum.The_Blood_Raider_Covenant,
			SictFactionSictEnum.Thukker_Tribe,};

		public SictAutoKonfigFittingVerkürzt()
		{
			InitializeComponent();

			DataGridMengeZuFactionFitting.ItemsSource = DataGridZuFactionFittingBezaicnerMengeRepr;

			ComboBoxMengeZuFactionFittingBezaicnerErscteleAuswaalFactionInit();
		}

		static	readonly	KeyValuePair<SictFactionSictEnum,	string>[]	MengeFactionZurAuswaalMitBescriftung	=
			MengeFactionZurAuswaalMitBescriftungBerecne();

		static public string BescriftungFürFaction(SictFactionSictEnum FactionSictEnum)
		{
			return (SictFactionSictEnum.Andere == FactionSictEnum) ? "Any" : Regex.Replace(FactionSictEnum.ToString(), "_", " ");
		}

		static	KeyValuePair<SictFactionSictEnum,	string>[]	MengeFactionZurAuswaalMitBescriftungBerecne()
		{
			return
				MengeFactionZurAuswaal
				.Select((FactionSictEnum) =>
					new	KeyValuePair<SictFactionSictEnum,	string>(
						FactionSictEnum, BescriftungFürFaction(FactionSictEnum))).ToArray();
		}

		static SictFactionSictEnum? FactionSictEnumBerecneFürFactionBescriftung(string FactionBescriftung)
		{
			if (null == FactionBescriftung)
			{
				return null;
			}

			foreach (var Kandidaat in MengeFactionZurAuswaalMitBescriftung)
			{
				if (string.Equals(Kandidaat.Value, FactionBescriftung, StringComparison.InvariantCultureIgnoreCase))
				{
					return Kandidaat.Key;
				}
			}

			return null;
		}

		void ComboBoxMengeZuFactionFittingBezaicnerErscteleAuswaalFactionInit()
		{
			var MengeFactionZurAuswaalSictString =
				MengeFactionZurAuswaalMitBescriftung
				.Select((FactionZurAuswaalMitBescriftung) => FactionZurAuswaalMitBescriftung.Value).ToArray();

			Bib3.Glob.PropagiireListeRepräsentatioonStringEquals(
				MengeFactionZurAuswaalSictString,
				ComboBoxMengeZuFactionFittingBezaicnerErscteleAuswaalFaction.Items);
		}

		public void VonOptimatZuusctandApliziire(
			SictVonOptimatMeldungZuusctand	VonOptimatZuusctand)
		{
			var FittingManagementMengeFittingPfaadListeGrupeNaame =
				(null == VonOptimatZuusctand) ? null :
				VonOptimatZuusctand.FittingManagementMengeFittingPfaadListeGrupeNaame;

			var FittingManagementMengeFittingNaame =
				ExtractFromOldAssembly.Bib3.Extension.SelectNullable(
				FittingManagementMengeFittingPfaadListeGrupeNaame,
				(FittingListeGrupeNaame) => FittingListeGrupeNaame.LastOrDefault())
				.ToArrayNullable();

			Bib3.Glob.PropagiireListeRepräsentatioonStringEquals(
				FittingManagementMengeFittingNaame,
				ComboBoxMengeZuFactionFittingBezaicnerErscteleAuswaalFittingBezaicner.Items);
		}

		public void KonfigScraibeNaacGbs(
			SictOptimatParamFittingVerkürzt Konfig,
			bool VorherigeErhalte = false)
		{
			var InRaumVerhalte = (null == Konfig) ? null : Konfig.InRaumVerhalte;

			var MengeZuFactionAusFittingManagementFittingZuLaade = (null == Konfig) ? null : Konfig.MengeZuFactionAusFittingManagementFittingZuLaade;

			SctoierelementInRaumAktioonVerzwaigung.KonfigScraibeNaacGbs(InRaumVerhalte);

			DataGridZuFactionFittingBezaicnerMengeRepr.Clear();

			Bib3.Glob.PropagiireListeRepräsentatioon(
				MengeZuFactionAusFittingManagementFittingZuLaade,
				DataGridZuFactionFittingBezaicnerMengeRepr	as	IList<SictZuFactionFittingBezaicnerReprInDataGrid>,
				(ZuFactionAusFittingManagementFittingZuLaade) =>
					new SictZuFactionFittingBezaicnerReprInDataGrid(
						ZuFactionAusFittingManagementFittingZuLaade.Key,
						ZuFactionAusFittingManagementFittingZuLaade.Value),
				(Repr, ZuFactionAusFittingManagementFittingZuLaade) =>
					string.Equals(Repr.FactionSictString, ZuFactionAusFittingManagementFittingZuLaade.Key) &&
					string.Equals(Repr.FittingBezaicner, ZuFactionAusFittingManagementFittingZuLaade.Value));
		}

		public SictOptimatParamFittingVerkürzt KonfigBerecneAusGbs()
		{
			if (!IsInitialized)
			{
				return null;
			}

			var InRaumVerhalte = SctoierelementInRaumAktioonVerzwaigung.KonfigBerecneAusGbs();

			var MengeZuFactionFittingBezaicner =
				(null == DataGridZuFactionFittingBezaicnerMengeRepr) ? null :
				DataGridZuFactionFittingBezaicnerMengeRepr
				.Select((Repr) => new KeyValuePair<string, string>(Repr.FactionSictEnum.ToString(), Repr.FittingBezaicner)).ToArray();

			var Konfig = new SictOptimatParamFittingVerkürzt();

			Konfig.InRaumVerhalte = InRaumVerhalte;
			Konfig.MengeZuFactionAusFittingManagementFittingZuLaade = MengeZuFactionFittingBezaicner;

			return Konfig;
		}

		void NaacKonfigGeändert()
		{
			this.RaiseEvent(new RoutedEventArgs(SictAutoKonfig.KonfigGeändertEvent, this));
		}

		private void ButtonZuFactionFittingBezaicnerErsctele_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				var FactionBezaicnerSictString = ComboBoxMengeZuFactionFittingBezaicnerErscteleAuswaalFaction.Text;

				/*
				 * 2014.07.25
				 * 
				 * Ainbau Faction "Any" und Änderung Bescriftung
				 * 
				var FactionBezaicner = Optimat.Glob.EnumParseNulbar<SictFactionSictEnum>(FactionBezaicnerSictString, true);
				 * */

				var FactionBezaicner = FactionSictEnumBerecneFürFactionBescriftung(FactionBezaicnerSictString);

				var FittingBezaicner = ComboBoxMengeZuFactionFittingBezaicnerErscteleAuswaalFittingBezaicner.Text;

				if (!FactionBezaicner.HasValue)
				{
					throw new ArgumentOutOfRangeException("unknown Faction identifier");
				}

				DataGridZuFactionFittingBezaicnerMengeRepr.Add(new SictZuFactionFittingBezaicnerReprInDataGrid(FactionBezaicner.Value, FittingBezaicner));

				NaacKonfigGeändert();
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}

		private void DataGridMengeZuFactionFittingBezaicnerButtonEntferne_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				DataGridZuFactionFittingBezaicnerMengeRepr.Remove(Optimat.Glob.DataContextAusFrameworkElement<SictZuFactionFittingBezaicnerReprInDataGrid>(sender));

				NaacKonfigGeändert();
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}
	}

	public class SictZuFactionFittingBezaicnerReprInDataGrid
	{
		public SictFactionSictEnum? FactionSictEnum
		{
			set;
			get;
		}

		public string FactionSictString
		{
			get
			{
				var FactionSictEnum = this.FactionSictEnum;

				if (!FactionSictEnum.HasValue)
				{
					return null;
				}

				return SictAutoKonfigFittingVerkürzt.BescriftungFürFaction(FactionSictEnum.Value);
			}
		}

		public string FittingBezaicner
		{
			set;
			get;
		}

		public SictZuFactionFittingBezaicnerReprInDataGrid(
			string FactionSictString,
			string FittingBezaicner)
			:
			this(
			Optimat.Glob.EnumParseNulbar<SictFactionSictEnum>(FactionSictString),
			FittingBezaicner)
		{
		}

		public SictZuFactionFittingBezaicnerReprInDataGrid(
			SictFactionSictEnum? FactionSictEnum,
			string FittingBezaicner)
		{
			this.FactionSictEnum = FactionSictEnum;
			this.FittingBezaicner = FittingBezaicner;
		}
	}
}
