using System;
using System.Collections.Generic;
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
using ExtractFromOldAssembly.Bib3;

namespace Optimat.EveOnline.GBS
{
	/// <summary>
	/// Interaction logic for AutoKonfigMine.xaml
	/// </summary>
	public partial class SictAutoKonfigMine : UserControl
	{
		readonly List<KeyValuePair<OreTypSictEnum, CheckBox>> MengeZuOreTypRepr = new List<KeyValuePair<OreTypSictEnum, CheckBox>>(MengeZuOreTypReprBerecne());

		static public OreTypSictEnum[] MengeOreTypRepräsentiirt =
			Enum.GetValues(typeof(OreTypSictEnum)).Cast<OreTypSictEnum>().Except(new OreTypSictEnum[] { OreTypSictEnum.Kain }).ToArray();

		static public IEnumerable<KeyValuePair<OreTypSictEnum, CheckBox>> MengeZuOreTypReprBerecne()
		{
			var ListeOreTypRepräsentiirt =
				MengeOreTypRepräsentiirt.OrderBy((Kandidaat) => Kandidaat.ToString()).ToArray();

			foreach (var OreTypRepräsentiirt in MengeOreTypRepräsentiirt)
			{
				var CheckBoxLabelString = Regex.Replace(OreTypRepräsentiirt.ToString(), "_", " ");

				var	CheckBoxLabel	= new	TextBlock();
				CheckBoxLabel.Text	= CheckBoxLabelString;
				CheckBoxLabel.Margin = new Thickness(0);
				CheckBoxLabel.VerticalAlignment = VerticalAlignment.Center;

				var CheckBox = new CheckBox();

				CheckBox.Content = CheckBoxLabel;
				CheckBox.Margin = new Thickness(4);
				CheckBox.VerticalAlignment = VerticalAlignment.Center;

				yield return new KeyValuePair<OreTypSictEnum, CheckBox>(OreTypRepräsentiirt, CheckBox);
			}
		}

		public SictAutoKonfigMine()
		{
			InitializeComponent();

			MengeZuOreTypRepr.ForEachNullable((OreTypRepr) => PanelMengeTypOreFraigaabe.Children.Add(OreTypRepr.Value));
		}

		private void ButtonMengeTypOreFraigaabeKaine_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				MengeZuOreTypRepr.ForEachNullable((OreTypRepr) => OreTypRepr.Value.IsChecked	= false);
			}
			catch (System.Exception Exception)
			{
				Bib3.FCL.GBS.Extension.MessageBoxException(Exception);
			}
		}

		private void ButtonMengeTypOreFraigaabeAle_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				MengeZuOreTypRepr.ForEachNullable((OreTypRepr) => OreTypRepr.Value.IsChecked = true);
			}
			catch (System.Exception Exception)
			{
				Bib3.FCL.GBS.Extension.MessageBoxException(Exception);
			}
		}

		public void KonfigBerecneAusGbs(ref	SictOptimatParamMine Konfig)
		{
			bool? SurveyScannerFraigaabe = null;
			bool? MengeOreTypBescrankeNaacMiningCrystal = null;

			SurveyScannerFraigaabe = CheckBoxAutoMineSurveyScannerFraigaabe.IsChecked;
			MengeOreTypBescrankeNaacMiningCrystal = CheckBoxMengeOreTypBescrankeNaacMiningCrystal.IsChecked;

			var MengeOreTypFraigaabe =
				MengeZuOreTypRepr
				.WhereNullable((ZuOreTypRepr) => (ZuOreTypRepr.Value.IsChecked ?? false))
				.SelectNullable((ZuOreTypRepr) => ZuOreTypRepr.Key)
				.ToArrayNullable();

			if (null == Konfig)
			{
				Konfig = new SictOptimatParamMine();
			}

			Konfig.SurveyScannerFraigaabe = SurveyScannerFraigaabe;
			Konfig.MengeOreTypeBescrankeNaacMiningCrystal = MengeOreTypBescrankeNaacMiningCrystal;

			if (!Bib3.Glob.SequenceEqual(
				MengeOreTypFraigaabe,
				Konfig.MengeOreTypFraigaabe))
			{
				Konfig.MengeOreTypFraigaabe = MengeOreTypFraigaabe;
			}
		}

		public void KonfigScraibeNaacGbs(
			SictOptimatParamMine Konfig,
			bool VorherigeErhalte = false)
		{
			bool? SurveyScannerFraigaabe = null;
			bool? MengeOreTypeBescrankeNaacMiningCrystal = null;
			OreTypSictEnum[] MengeOreTypFraigaabe = null;

			if (null != Konfig)
			{
				SurveyScannerFraigaabe = Konfig.SurveyScannerFraigaabe;
				MengeOreTypeBescrankeNaacMiningCrystal = Konfig.MengeOreTypeBescrankeNaacMiningCrystal;
				MengeOreTypFraigaabe = Konfig.MengeOreTypFraigaabe;
			}

			CheckBoxAutoMineSurveyScannerFraigaabe.IsChecked = SurveyScannerFraigaabe ?? false;

			CheckBoxMengeOreTypBescrankeNaacMiningCrystal.IsChecked = MengeOreTypeBescrankeNaacMiningCrystal ?? false;

			MengeZuOreTypRepr.ForEachNullable((OreTypeRepr) =>
				OreTypeRepr.Value.IsChecked = MengeOreTypFraigaabe.AnyNullable((OreTypeFraigaabe) => OreTypeFraigaabe == OreTypeRepr.Key) ?? false);

		}

	}
}
