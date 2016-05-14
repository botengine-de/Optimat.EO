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
using Optimat.GBS;

namespace Optimat.EveOnline.GBS
{
	/// <summary>
	/// Interaction logic for SictAutoKonfigMission.xaml
	/// </summary>
	public partial class SictAutoKonfigMission : UserControl
	{
		static IEnumerable<string> FürComboBoxAgentLevelMengeItem = Enum.GetNames(typeof(SictAgentLevelOderAnySictEnum));

		readonly ObservableCollection<SictObservable<SictZuMissionGrupeAktioonReprInDataGrid>> DataGridZuMissionGrupeVerhalteMengeRepr =
			new ObservableCollection<SictObservable<SictZuMissionGrupeAktioonReprInDataGrid>>();

		readonly ObservableCollection<SictObservable<SictSuuceAgentStationRepr>> DataGridSuuceAgentMengeStationRepr = new ObservableCollection<SictObservable<SictSuuceAgentStationRepr>>();

		readonly KeyValuePair<int, CheckBox>[] MengeZuAgentLevelCheckBoxAktioonFüüreAusOderAcceptAktiiv;

		public SictAutoKonfigMission()
		{
			InitializeComponent();

			this.DataGridZuMissionGrupeVerhalte.ItemsSource = DataGridZuMissionGrupeVerhalteMengeRepr;
			this.DataGridSuuceAgentMengeStation.ItemsSource = DataGridSuuceAgentMengeStationRepr;

			MengeZuAgentLevelCheckBoxAktioonFüüreAusOderAcceptAktiiv = new KeyValuePair<int, CheckBox>[]{
				new	KeyValuePair<int, CheckBox>(1, CheckBoxAktioonFüüreAusOderAcceptAktiivFürAgentLevel1),
				new	KeyValuePair<int, CheckBox>(2, CheckBoxAktioonFüüreAusOderAcceptAktiivFürAgentLevel2),
				new	KeyValuePair<int, CheckBox>(3, CheckBoxAktioonFüüreAusOderAcceptAktiivFürAgentLevel3),
				new	KeyValuePair<int, CheckBox>(4, CheckBoxAktioonFüüreAusOderAcceptAktiivFürAgentLevel4),};
		}

		public IEnumerable<string> FürComboBoxAgentLevelItemsSource
		{
			get
			{
				return FürComboBoxAgentLevelMengeItem;
			}
		}

		public	void	KonfigBerecneAusGbs(ref	SictOptimatParamMission	Konfig)
		{
			var DataGridZuMissionGrupeVerhalteMengeRepr = this.DataGridZuMissionGrupeVerhalteMengeRepr;

			var MengeZuMissionFilterVerhalte =
				(null == DataGridZuMissionGrupeVerhalteMengeRepr) ? null :
				DataGridZuMissionGrupeVerhalteMengeRepr
				.Select((Observable) => Observable.Wert)
				.Where((ZuMissionFilterVerhalteRepr) => null != ZuMissionFilterVerhalteRepr)
				.Select((ZuMissionFilterVerhalteRepr) => ZuMissionFilterVerhalteRepr.KonfigBerecne())
				.ToArray();

			var AktioonFüüreAusOderAcceptMengeAgentLevel = new List<int>();

			var MengeZuAgentLevelCheckBoxAktioonFüüreAusOderAcceptAktiiv = this.MengeZuAgentLevelCheckBoxAktioonFüüreAusOderAcceptAktiiv;

			if (null != MengeZuAgentLevelCheckBoxAktioonFüüreAusOderAcceptAktiiv)
			{
				foreach (var ZuAgentLevelCheckBoxAktioonFüüreAusOderAcceptAktiiv in MengeZuAgentLevelCheckBoxAktioonFüüreAusOderAcceptAktiiv)
				{
					if (null == ZuAgentLevelCheckBoxAktioonFüüreAusOderAcceptAktiiv.Value)
					{
						continue;
					}

					if (true == ZuAgentLevelCheckBoxAktioonFüüreAusOderAcceptAktiiv.Value.IsChecked)
					{
						AktioonFüüreAusOderAcceptMengeAgentLevel.Add(ZuAgentLevelCheckBoxAktioonFüüreAusOderAcceptAktiiv.Key);
					}
				}
			}

			var SuuceAgentMengeStation =
				DataGridSuuceAgentMengeStationRepr
				.Select((Repr) => new KeyValuePair<string, bool>(Repr.Wert.StationBezaicner, Repr.Wert.AktioonSuuceAktiiv))
				.ToArray();

			var AktioonDeclineAlsSctandardFürSonstigeFraigaabe = CheckBoxAktioonDeclineAlsSctandardFürSonstigeFraigaabe.IsChecked;

			if (null == Konfig)
			{
				Konfig = new SictOptimatParamMission();
			}

			Konfig.AktioonAcceptFraigaabe = true	== CheckBoxAktioonAcceptAktiiv.IsChecked;
			Konfig.AktioonDeclineFraigaabe = true	== CheckBoxAktioonDeclineAktiiv.IsChecked;
			Konfig.AktioonDeclineUnabhängigVonStandingLossFraigaabe = true == CheckBoxAktioonDeclineUnabhängigVonStandingLossFraigaabe.IsChecked;

			if (!Bib3.Glob.SequenceEqual(
				AktioonFüüreAusOderAcceptMengeAgentLevel,
				Konfig.AktioonAcceptMengeAgentLevelFraigaabe))
			{
				Konfig.AktioonAcceptMengeAgentLevelFraigaabe = AktioonFüüreAusOderAcceptMengeAgentLevel.ToArray();
			}

			if (!Bib3.Glob.SequenceEqual(
				MengeZuMissionFilterVerhalte,
				Konfig.MengeZuMissionFilterVerhalte,
				SictKonfigMissionZuMissionFilterVerhalte.HinraicendGlaicwertigFürIdentInOptimatParam))
			{
				Konfig.MengeZuMissionFilterVerhalte = MengeZuMissionFilterVerhalte;
			}

			Konfig.AktioonDeclineAlsSctandardFürSonstigeFraigaabe = AktioonDeclineAlsSctandardFürSonstigeFraigaabe;

			if (!Bib3.Glob.SequenceEqual(
				SuuceAgentMengeStation,
				Konfig.SuuceAgentMengeStation,
				(Element0, Element1) => string.Equals(Element0.Key, Element1.Key)	&& Element0.Value	== Element1.Value))
			{
				Konfig.SuuceAgentMengeStation = SuuceAgentMengeStation;
			}
		}

		public void KonfigScraibeNaacGbs(
			SictOptimatParamMission Konfig,
			bool VorherigeErhalte = false)
		{
			var AktioonAcceptFraigaabe = (null == Konfig) ? null : Konfig.AktioonAcceptFraigaabe;
			var AktioonDeclineFraigaabe = (null == Konfig) ? null : Konfig.AktioonDeclineFraigaabe;
			var AktioonDeclineUnabhängigVonStandingLossFraigaabe = (null == Konfig) ? null : Konfig.AktioonDeclineUnabhängigVonStandingLossFraigaabe;

			var MengeZuMissionFilterVerhalte = (null == Konfig) ? null : Konfig.MengeZuMissionFilterVerhalte;
			var SuuceAgentMengeStation = (null == Konfig) ? null : Konfig.SuuceAgentMengeStation;

			var AktioonAcceptMengeAgentLevelFraigaabe = (null == Konfig) ? null : Konfig.AktioonAcceptMengeAgentLevelFraigaabe;

			var AktioonDeclineAlsSctandardFürSonstigeFraigaabe = (null == Konfig) ? null : Konfig.AktioonDeclineAlsSctandardFürSonstigeFraigaabe;

			var MengeZuAgentLevelCheckBoxAktioonFüüreAusOderAcceptAktiiv = this.MengeZuAgentLevelCheckBoxAktioonFüüreAusOderAcceptAktiiv;

			if (null != MengeZuAgentLevelCheckBoxAktioonFüüreAusOderAcceptAktiiv)
			{
				foreach (var ZuAgentLevelCheckBoxAktioonFüüreAusOderAcceptAktiiv in MengeZuAgentLevelCheckBoxAktioonFüüreAusOderAcceptAktiiv)
				{
					if (null == ZuAgentLevelCheckBoxAktioonFüüreAusOderAcceptAktiiv.Value)
					{
						continue;
					}

					var Checked = false;

					if (null != AktioonAcceptMengeAgentLevelFraigaabe)
					{
						if (AktioonAcceptMengeAgentLevelFraigaabe.Contains(ZuAgentLevelCheckBoxAktioonFüüreAusOderAcceptAktiiv.Key))
						{
							Checked = true;
						}
					}

					ZuAgentLevelCheckBoxAktioonFüüreAusOderAcceptAktiiv.Value.IsChecked = Checked;
				}
			}

			Bib3.Glob.PropagiireListeRepräsentatioon(
				MengeZuMissionFilterVerhalte,
				DataGridZuMissionGrupeVerhalteMengeRepr as IList<SictObservable<SictZuMissionGrupeAktioonReprInDataGrid>>,
				(Kwele) => new SictObservable<SictZuMissionGrupeAktioonReprInDataGrid>(
					new SictZuMissionGrupeAktioonReprInDataGrid(Kwele.FilterAgentLevel, Kwele.FilterMissionTitelRegexPattern)),
				(KandidaatZiil, KandidaatKwele) =>
				{
					if (null == KandidaatZiil.Wert)
					{
						return false;
					}

					return KandidaatKwele.FilterAgentLevel == KandidaatZiil.Wert.AgentLevelSictEnum &&
						string.Equals(KandidaatKwele.FilterMissionTitelRegexPattern, KandidaatZiil.Wert.MissionTitelRegexPattern);
				},
				(Repr, Kwele) => { Repr.Wert.SezeVerhalte(Kwele); Repr.RaisePropertyChanged(); },
				VorherigeErhalte);

			Bib3.Glob.PropagiireListeRepräsentatioon(
				SuuceAgentMengeStation,
				DataGridSuuceAgentMengeStationRepr as IList<SictObservable<SictSuuceAgentStationRepr>>,
				(Kwele) =>
					new	SictObservable<SictSuuceAgentStationRepr>(
					new SictSuuceAgentStationRepr(Kwele.Key, Kwele.Value)),
				(KandidaatZiil, KandidaatKwele) => string.Equals(KandidaatKwele.Key, KandidaatZiil.Wert.StationBezaicner),
				(Repr, Kwele) => { Repr.Wert.AktioonSuuceAktiiv = Kwele.Value; Repr.RaisePropertyChanged(); },
				VorherigeErhalte);

			CheckBoxAktioonAcceptAktiiv.IsChecked = AktioonAcceptFraigaabe ?? false;
			CheckBoxAktioonDeclineAktiiv.IsChecked = AktioonDeclineFraigaabe ?? false;
			CheckBoxAktioonDeclineUnabhängigVonStandingLossFraigaabe.IsChecked = AktioonDeclineUnabhängigVonStandingLossFraigaabe ?? false;

			CheckBoxAktioonDeclineAlsSctandardFürSonstigeFraigaabe.IsChecked	= AktioonDeclineAlsSctandardFürSonstigeFraigaabe	?? false;

			VonOptimatZuusctandApliziire();
		}

		SictVonOptimatMeldungZuusctand	VonOptimatZuusctandLezte;

		public void VonOptimatZuusctandApliziire()
		{
			VonOptimatZuusctandApliziire(VonOptimatZuusctandLezte);
		}

		public void VonOptimatZuusctandApliziire(SictVonOptimatMeldungZuusctand VonOptimatZuusctand)
		{
			VonOptimatZuusctandLezte = VonOptimatZuusctand;

			var MengeZuMissionFilterAktioonVerfüügbar =
				(null == VonOptimatZuusctand) ? null :
				VonOptimatZuusctand.MengeZuMissionFilterAktioonVerfüügbar;

			var EveWeltTopologii =
				(null == VonOptimatZuusctand) ? null :
				VonOptimatZuusctand.EveWeltTopologii;

			var EveWeltTopologiiMengeSystem =
				(null == EveWeltTopologii) ? null :
				EveWeltTopologii.MengeSolarSystem;

			var EveWeltTopologiiMengeStation =
				(null == EveWeltTopologiiMengeSystem) ? null :
				Bib3.Glob.ListeEnumerableAgregiirt(
				Bib3.Extension.SelectNullable(
				EveWeltTopologiiMengeSystem, (System) => (null	== System)	?	null	: System.MengeStation));

			var EveWeltTopologiiMengeStationName =
				Bib3.Extension.WhereNullable(
				Bib3.Extension.SelectNullable(
				EveWeltTopologiiMengeStation,
				(Station) => (null == Station) ? null : Station.StationName),
				(StationName) => null	!= StationName);

			Bib3.Glob.PropagiireListeRepräsentatioon(
				EveWeltTopologiiMengeStationName,
				ComboBoxSuuceAgentMengeStationFügeAin.Items,
				(StationName) => StationName,
				(Repr, Kwele) => string.Equals(Repr, Kwele));

			VonServerZuPräferenzInfoApliziire(MengeZuMissionFilterAktioonVerfüügbar);
		}

		public void VonServerZuPräferenzInfoApliziire(SictKonfigMissionZuMissionFilterVerhalte[] VonServerInfo)
		{
			var	VonServerInfoFiltert	=
				(null	== VonServerInfo)	?	null	:
				VonServerInfo.Where((Kandidaat) => null != Kandidaat);

			Bib3.Glob.PropagiireListeRepräsentatioon(
				VonServerInfoFiltert,
				DataGridZuMissionGrupeVerhalteMengeRepr	as	IList<SictObservable<SictZuMissionGrupeAktioonReprInDataGrid>>,
				(Kwele) => new SictObservable<SictZuMissionGrupeAktioonReprInDataGrid>(
					new SictZuMissionGrupeAktioonReprInDataGrid(Kwele.FilterAgentLevel, Kwele.FilterMissionTitelRegexPattern)),
				(KandidaatZiil, KandidaatKwele) =>
				{
					if (null == KandidaatZiil.Wert)
					{
						return false;
					}

					return KandidaatKwele.FilterAgentLevel == KandidaatZiil.Wert.AgentLevelSictEnum &&
						string.Equals(KandidaatKwele.FilterMissionTitelRegexPattern, KandidaatZiil.Wert.MissionTitelRegexPattern);
				},
				(Repr, Konfig) => Repr.Wert.AktualisiireMitVonServerInfo(Konfig, null != VonServerInfo),
				true);
		}

		void NaacKonfigGeändert()
		{
			this.RaiseEvent(new RoutedEventArgs(SictAutoKonfig.KonfigGeändertEvent, this));
		}

		void DataGridZuMissionGrupeVerhalte_MissionTitelRegexPattern_OnTextChanged(object sender, TextChangedEventArgs e)
		{
			try
			{
				NaacKonfigGeändert();
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}

		private void DataGridZuMissionGrupeVerhalte_CheckBox_Checked(object sender, RoutedEventArgs e)
		{
			try
			{
				NaacKonfigGeändert();
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}

		private void DataGridZuMissionGrupeVerhalte_CheckBox_Unchecked(object sender, RoutedEventArgs e)
		{
			try
			{
				NaacKonfigGeändert();
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}

		private void DataGridZuMissionGrupeVerhalte_ButtonEntferne_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				var Observable = Optimat.Glob.DataContextAusFrameworkElement<SictObservable<SictZuMissionGrupeAktioonReprInDataGrid>>(sender);

				if (null == Observable)
				{
					return;
				}

				var ZuMissionFilterVerhalte = Observable.Wert;

				var MissionFilterErhalte = false;

				if (null != ZuMissionFilterVerhalte)
				{
					if (true == ZuMissionFilterVerhalte.VonServerVorgaabe)
					{
						MissionFilterErhalte = true;
					}
				}

				if (MissionFilterErhalte)
				{
					ZuMissionFilterVerhalte.AktioonFüüreAusAktiiv = false;
					ZuMissionFilterVerhalte.AktioonAcceptAktiiv = false;
					ZuMissionFilterVerhalte.AktioonDeclineAktiiv = false;

					Observable.RaisePropertyChanged();
				}
				else
				{
					DataGridZuMissionGrupeVerhalteMengeRepr.Remove(Observable);
				}

				NaacKonfigGeändert();
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}

		private void DataGridSuuceAgentMengeStation_ButtonEntferne_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				var Observable = Optimat.Glob.DataContextAusFrameworkElement<SictObservable<SictSuuceAgentStationRepr>>(sender);

				DataGridSuuceAgentMengeStationRepr.Remove(Observable);

				NaacKonfigGeändert();
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}

		private void DataGridSuuceAgentMengeStation_CheckBox_Checked(object sender, RoutedEventArgs e)
		{
			try
			{
				NaacKonfigGeändert();
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}

		private void DataGridSuuceAgentMengeStation_CheckBox_Unchecked(object sender, RoutedEventArgs e)
		{
			try
			{
				NaacKonfigGeändert();
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}

		private void ButtonMissionFilterKonstruiire_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				var AgentLevelNulbar = Optimat.Glob.EnumParseNulbar<SictAgentLevelOderAnySictEnum>(ComboBoxMissionFilterKonstruiireAuswaalAgentLevel.Text);

				var AgentLevel = AgentLevelNulbar ?? SictAgentLevelOderAnySictEnum.Any;

				var MissionTitelFilterAlsRegexPattern = true	== RadioButtonMissionFilterKonstruiireAuswaalFilterMissionTitelAlsRegexPattern.IsChecked;

				var AingaabeFilterMissionTitel = TextBoxMissionFilterKonstruiireAuswaalMissionTitelPattern.Text;

				var FilterMissionTitelRegexPattern =
					(null == AingaabeFilterMissionTitel) ? null :
					(MissionTitelFilterAlsRegexPattern ? AingaabeFilterMissionTitel : Regex.Escape(AingaabeFilterMissionTitel));

				var ZuMissionVerhalte = new SictZuMissionGrupeAktioonReprInDataGrid(AgentLevel, FilterMissionTitelRegexPattern);

				DataGridZuMissionGrupeVerhalteMengeRepr.Add(new SictObservable<SictZuMissionGrupeAktioonReprInDataGrid>(ZuMissionVerhalte));

				VonOptimatZuusctandApliziire();

				NaacKonfigGeändert();
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}

		public void DataGridZuMissionGrupeVerhalteHeaderLayoutUpdate()
		{
			Bib3.FCL.GBS.Extension.FrameworkElementInCanvasLaageRicteNaacDataGridMengeColumnHeader(
				DataGridZuMissionGrupeVerhalteHeaderPanelFilter,
				DataGridZuMissionGrupeVerhalte,
				new DataGridColumn[]{
					DataGridMengeZuMissionFilterVerhalteColumnFilterAgentLevel,
					DataGridMengeZuMissionFilterVerhalteColumnFilterMissionTitelRegexPattern,});

			Bib3.FCL.GBS.Extension.FrameworkElementInCanvasLaageRicteNaacDataGridMengeColumnHeader(
				DataGridZuMissionGrupeVerhalteHeaderPanelAnzuwendendeAktioon,
				DataGridZuMissionGrupeVerhalte,
				new DataGridColumn[]{
					DataGridMengeZuMissionFilterVerhalteColumnAktioonFüüreAus,
					DataGridMengeZuMissionFilterVerhalteColumnAktioonAccept,
					DataGridMengeZuMissionFilterVerhalteColumnAktioonDecline,});
		}

		private void DataGridZuMissionGrupeVerhalte_LayoutUpdated(object sender, EventArgs e)
		{
			try
			{
				DataGridZuMissionGrupeVerhalteHeaderLayoutUpdate();
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}

		private void ButtonSuuceAgentMengeStationFügeAinJezt_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				var StationBezaicner = ComboBoxSuuceAgentMengeStationFügeAin.Text;

				if (null == StationBezaicner)
				{
					throw new ArgumentNullException("StationBezaicner");
				}

				DataGridSuuceAgentMengeStationRepr.Add(new SictObservable<SictSuuceAgentStationRepr>(new SictSuuceAgentStationRepr(StationBezaicner)));

				NaacKonfigGeändert();
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}
	}

	public class SictSuuceAgentStationRepr
	{
		public string StationBezaicner
		{
			private	set;
			get;
		}

		public bool AktioonSuuceAktiiv
		{
			set;
			get;
		}

		public bool? VonServerInfoStationExistent
		{
			set;
			get;
		}

		public SictSuuceAgentStationRepr(
			string StationBezaicner,
			bool AktioonSuuceAktiiv	= false)
		{
			this.StationBezaicner = StationBezaicner;
			this.AktioonSuuceAktiiv = AktioonSuuceAktiiv;
		}
	}

	public class SictZuMissionGrupeAktioonReprInDataGrid	: System.ComponentModel.INotifyPropertyChanged
	{
		public const	int BrushCellBackgroundOpazitäät = 0x60;

		static readonly public Color BrushKonstantColor = Color.FromArgb(BrushCellBackgroundOpazitäät, 0x80, 0x80, 0x80);
		static readonly public Color BrushVerfüügbarColor = Color.FromArgb(BrushCellBackgroundOpazitäät, 0, 0x80, 0);
		static readonly public Color BrushVerfüügbarNictColor = Color.FromArgb(BrushCellBackgroundOpazitäät, 0x80, 0, 0);

		static public Brush FürCellBrushMitColorErsctele(Color	Color)
		{
			return Optimat.GBS.Glob.BrushGradientDiagonalGesctraiftZwaifarbigKonstruiire(Colors.Transparent, Color, 4);
		}

		static readonly public Brush BrushKonstant = FürCellBrushMitColorErsctele(BrushKonstantColor);
		static readonly public Brush BrushVerfüügbar = FürCellBrushMitColorErsctele(BrushVerfüügbarColor);
		static readonly public Brush BrushVerfüügbarNict = FürCellBrushMitColorErsctele(BrushVerfüügbarNictColor);

		static public Brush CellBackgroundFürWertVerfüügbar(bool? Verfüügbar)
		{
			if (!Verfüügbar.HasValue)
			{
				return null;
			}

			return Verfüügbar.Value ? BrushVerfüügbar : BrushVerfüügbarNict;
		}

		static public Brush CellBackgroundFürWertKonstant(bool? Konstant)
		{
			if (!Konstant.HasValue)
			{
				return null;
			}

			return Konstant.Value ? BrushKonstant : null;
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		// Create the OnPropertyChanged method to raise the event 
		protected void OnPropertyChanged(string name)
		{
			System.ComponentModel.PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
			{
				handler(this, new System.ComponentModel.PropertyChangedEventArgs(name));
			}
		}

		public SictKonfigMissionZuMissionFilterVerhalte KonfigBerecne()
		{
			var Konfig = new SictKonfigMissionZuMissionFilterVerhalte();

			Konfig.FilterAgentLevel = AgentLevelSictEnum;
			Konfig.FilterMissionTitelRegexPattern = MissionTitelRegexPattern;

			Konfig.AktioonFüüreAusAktiiv = AktioonFüüreAusAktiiv;
			Konfig.AktioonAcceptAktiiv = AktioonAcceptAktiiv;
			Konfig.AktioonDeclineAktiiv = AktioonDeclineAktiiv;

			return Konfig;
		}

		public bool? VonServerVorgaabe
		{
			set;
			get;
		}

		public bool? VonServerAktioonFüüreAusVerfüügbar
		{
			set;
			get;
		}

		public SictAgentLevelOderAnySictEnum? AgentLevelSictEnum
		{
			set;
			get;
		}

		public string AgentLevelSictString
		{
			set
			{
				AgentLevelSictEnum = Optimat.Glob.EnumParseNulbar<SictAgentLevelOderAnySictEnum>(value);
			}

			get
			{
				return AgentLevelSictEnum.ToString();
			}
		}

		public string MissionTitelRegexPattern
		{
			set;
			get;
		}

		public bool AktioonFüüreAusAktiiv
		{
			set;
			get;
		}

		public bool AktioonAcceptAktiiv
		{
			set;
			get;
		}

		public bool AktioonDeclineAktiiv
		{
			set;
			get;
		}

		public bool AktioonFüüreAusIsEnabled
		{
			set;
			get;
		}

		public bool AktioonFüüreAusIsReadonly
		{
			get
			{
				return	!AktioonFüüreAusIsEnabled;
			}
		}

		public Brush CellBackgroundAktioonFüüreAus
		{
			get
			{
				return CellBackgroundFürWertVerfüügbar(VonServerAktioonFüüreAusVerfüügbar);
			}
		}

		public Brush CellBackgroundFilterKonstant
		{
			get
			{
				return CellBackgroundFürWertKonstant(VonServerVorgaabe);
			}
		}

		public SictZuMissionGrupeAktioonReprInDataGrid()
		{
		}

		public SictZuMissionGrupeAktioonReprInDataGrid(
			SictKonfigMissionZuMissionFilterVerhalte ZuMissionFilterVerhalte)
			:
			this(
			(null	== ZuMissionFilterVerhalte)	?	null	: ZuMissionFilterVerhalte.FilterAgentLevel,
			(null	== ZuMissionFilterVerhalte)	?	null	: ZuMissionFilterVerhalte.FilterMissionTitelRegexPattern,
			((null	== ZuMissionFilterVerhalte)	?	null	: ZuMissionFilterVerhalte.AktioonFüüreAusAktiiv)	?? false,
			((null	== ZuMissionFilterVerhalte)	?	null	: ZuMissionFilterVerhalte.AktioonAcceptAktiiv)	?? false,
			((null	== ZuMissionFilterVerhalte)	?	null	: ZuMissionFilterVerhalte.AktioonDeclineAktiiv)	?? false)
		{
		}

		public SictZuMissionGrupeAktioonReprInDataGrid(
			SictAgentLevelOderAnySictEnum? AgentLevelSictEnum,
			string MissionTitelRegexPattern	= null,
			bool AktioonFüüreAusAktiiv	= false,
			bool AktioonAcceptAktiiv	= false,
			bool AktioonDeclineAktiiv	= false)
		{
			this.AgentLevelSictEnum = AgentLevelSictEnum;
			this.MissionTitelRegexPattern = MissionTitelRegexPattern;

			this.AktioonFüüreAusAktiiv = AktioonFüüreAusAktiiv;
			this.AktioonAcceptAktiiv = AktioonAcceptAktiiv;
			this.AktioonDeclineAktiiv = AktioonDeclineAktiiv;
		}

		public void SezeVerhalte(
			SictKonfigMissionZuMissionFilterVerhalte Verhalte)
		{
			this.AktioonFüüreAusAktiiv = ((null == Verhalte) ? null : Verhalte.AktioonFüüreAusAktiiv) ?? false;
			this.AktioonAcceptAktiiv = ((null == Verhalte) ? null : Verhalte.AktioonAcceptAktiiv) ?? false;
			this.AktioonDeclineAktiiv = ((null == Verhalte) ? null : Verhalte.AktioonDeclineAktiiv) ?? false;
		}

		public void AktualisiireMitVonServerInfo(
			SictKonfigMissionZuMissionFilterVerhalte	VonServerInfo,
			bool	VonServerInfoVorhande)
		{
			VonServerVorgaabe =
				(null == VonServerInfo) ?
				(VonServerInfoVorhande ? false : (bool?)null) :
				true;

			VonServerAktioonFüüreAusVerfüügbar =
				(null == VonServerInfo) ?
				(VonServerInfoVorhande ? false : (bool?)null) :
				(true == VonServerInfo.AktioonAcceptAktiiv);
		}
	}
}
