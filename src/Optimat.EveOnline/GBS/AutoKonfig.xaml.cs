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
	/// Interaction logic for AutoKonfig.xaml
	/// </summary>
	public partial class SictAutoKonfig : UserControl
	{
		public static readonly RoutedEvent KonfigGeändertEvent =
			EventManager.RegisterRoutedEvent("KonfigGeändert", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(SictAutoKonfig));

		Optimat.GBS.SictDataGridColumnAinfac[] DataGridOverviewMengePresetMengeColumn;

		ObservableCollection<Optimat.GBS.SictObservable<SictOverviewPresetUndTabMengeObjektGrupeSictbarReprInDataGrid>> OverviewDataGridMengePresetRepr =
			new ObservableCollection<Optimat.GBS.SictObservable<SictOverviewPresetUndTabMengeObjektGrupeSictbarReprInDataGrid>>();

		public SictAutoKonfig()
		{
			InitializeComponent();

			DataGridOverviewMengePresetInitialize();

			OverviewDataGridMengePresetRepr.CollectionChanged += OverviewDataGridMengePreset_CollectionChanged;
		}

		public Optimat.GBS.SictObservable<SictOverviewPresetUndTabMengeObjektGrupeSictbarReprInDataGrid> FürOverviewDataGridMengePresetErscteleElement(
			SictOverviewPresetUndTabMengeObjektGrupeSictbarReprInDataGrid RowRepr)
		{
			if (null != RowRepr)
			{
				RowRepr.PropertyChanged += DataGridMengePresetObservable_PropertyChanged;
			}

			var Observable = new Optimat.GBS.SictObservable<SictOverviewPresetUndTabMengeObjektGrupeSictbarReprInDataGrid>(RowRepr);

			Observable.PropertyChanged += DataGridMengePresetObservable_PropertyChanged;

			return Observable;
		}

		void DataGridMengePresetObservable_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			try
			{
				this.RaiseEvent(new RoutedEventArgs(KonfigGeändertEvent, this));
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}

		void OverviewDataGridMengePreset_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			try
			{
				this.RaiseEvent(new RoutedEventArgs(KonfigGeändertEvent, this));
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}

		static SictOverviewPresetUndTabMengeObjektGrupeSictbarReprInDataGrid ListeBuildOrderAusEventAusDataGridRow(object sender)
		{
			var Observable = Optimat.Glob.DataContextAusFrameworkElement<SictObservable<SictOverviewPresetUndTabMengeObjektGrupeSictbarReprInDataGrid>>(sender);

			if (null == Observable)
			{
				return null;
			}

			return Observable.Wert;
		}

		static readonly SictOverviewObjektGrupeEnum[] OverviewMengeObjektGrupeSictbarSictEnumAbbild =
			new SictOverviewObjektGrupeEnum[]{
				SictOverviewObjektGrupeEnum.AccelerationGate,
				SictOverviewObjektGrupeEnum.Station,
				SictOverviewObjektGrupeEnum.Rat,
				SictOverviewObjektGrupeEnum.Wreck,
				SictOverviewObjektGrupeEnum.CargoContainer,
				SictOverviewObjektGrupeEnum.SpawnContainer,
				SictOverviewObjektGrupeEnum.LargeCollidableStructure,
				SictOverviewObjektGrupeEnum.DeadspaceStructure,
			};

		void DataGridOverviewMengePresetInitialize()
		{
			var DataGridOverviewMengePresetMengeColumnObjektGrupe = new List<Optimat.GBS.SictDataGridColumnAinfac>();

			var OverviewMengeObjektGrupeSictbarSictEnumAbbild = SictAutoKonfig.OverviewMengeObjektGrupeSictbarSictEnumAbbild;

			var BindingPfaadBegin = "Wert.";

			{
				foreach (var OverviewObjektGrupeBezaicner in OverviewMengeObjektGrupeSictbarSictEnumAbbild)
				{
					var Column = Optimat.GBS.SictDataGridColumnAinfac.DataGridColumnCheckBox(
						ZuOverviewObjektGrupeGbsReprText(OverviewObjektGrupeBezaicner),
						BindingPfaadBegin + OverviewObjektGrupeBezaicner.ToString(),
						BindingMode.TwoWay,
						new RoutedEventHandler(OverviewPresetColumn_OnChecked),
						new RoutedEventHandler(OverviewPresetColumn_OnChecked)
						);

					DataGridOverviewMengePresetMengeColumnObjektGrupe.Add(Column);
				}
			}

			DataGridOverviewMengePresetMengeColumn = DataGridOverviewMengePresetMengeColumnObjektGrupe.ToArray();

			foreach (var Column in DataGridOverviewMengePresetMengeColumn)
			{
				Column.BaueAinNaacDataGrid(DataGridOverviewMengePreset);
			}

			DataGridOverviewMengePreset.ItemsSource = OverviewDataGridMengePresetRepr;
		}

		/*
		 * 2014.10.04
		 * 
		 * nit meer verwendet. Ersaz durc KonfigScraibeNaacGbs.
		 * 
public void Repräsentiire(Optimat.EveOnline.SictVonNuzerAutomatParam Repräsentiirte)
{
	bool? AutoFraigaabe = null;
	bool? AutoPilotFraigaabe = null;
	bool? AutoMineFraigaabe = null;
	bool? AutoMissionFraigaabe = null;
	SictVonNuzerAutomatParamMission Mission = null;

	try
	{
		if (null != Repräsentiirte)
		{
			AutoFraigaabe = Repräsentiirte.AutoFraigaabe;
			AutoPilotFraigaabe = Repräsentiirte.AutoPilotFraigaabe;
			AutoMineFraigaabe = Repräsentiirte.AutoMineFraigaabe;
			AutoMissionFraigaabe = Repräsentiirte.AutoMissionFraigaabe;
			Mission = Repräsentiirte.Mission;
		}
	}
	finally
	{
		CheckBoxAutoPilotFraigaabe.IsChecked = AutoPilotFraigaabe;
		CheckBoxAutoMineFraigaabe.IsChecked = AutoMineFraigaabe;
		CheckBoxAutoMissionFraigaabe.IsChecked = AutoMissionFraigaabe;

	}
}
		 * */

		void OverviewPresetColumn_OnTextChanged(object sender, TextChangedEventArgs e)
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

		void OverviewPresetColumn_OnChecked(object sender, RoutedEventArgs e)
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

		private void DataGridOverviewMengePresetColumnButtonEntferne_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				OverviewDataGridMengePresetRepr.Remove(
					Optimat.Glob.DataContextAusFrameworkElement<SictObservable<SictOverviewPresetUndTabMengeObjektGrupeSictbarReprInDataGrid>>(sender));

				NaacKonfigGeändert();
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}

		private void ButtonOverviewPresetFüügeAin_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				var OverviewPresetIdent = TextBoxOverviewPresetFüügeAinIdent.Text;

				var OverviewPresetRepr = new SictOverviewPresetUndTabMengeObjektGrupeSictbarReprInDataGrid(OverviewPresetIdent);

				OverviewDataGridMengePresetRepr.Add(FürOverviewDataGridMengePresetErscteleElement(OverviewPresetRepr));
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}

		static readonly public KeyValuePair<SictOverviewObjektGrupeEnum, string>[] MengeZuOverviewObjektGrupeGbsReprText =
			new KeyValuePair<SictOverviewObjektGrupeEnum, string>[]{
				new	KeyValuePair<SictOverviewObjektGrupeEnum,	string>(SictOverviewObjektGrupeEnum.AccelerationGate, "Acceleration" + Environment.NewLine + "Gate"),
				new	KeyValuePair<SictOverviewObjektGrupeEnum,	string>(SictOverviewObjektGrupeEnum.CargoContainer, "Cargo" + Environment.NewLine + "Container"),
				new	KeyValuePair<SictOverviewObjektGrupeEnum,	string>(SictOverviewObjektGrupeEnum.DeadspaceStructure, "Deadspace" + Environment.NewLine + "Structure"),
				new	KeyValuePair<SictOverviewObjektGrupeEnum,	string>(SictOverviewObjektGrupeEnum.LargeCollidableStructure, "Large Collidable" + Environment.NewLine + "Structure"),
				new	KeyValuePair<SictOverviewObjektGrupeEnum,	string>(SictOverviewObjektGrupeEnum.Rat, "All Rats"),
				new	KeyValuePair<SictOverviewObjektGrupeEnum,	string>(SictOverviewObjektGrupeEnum.SpawnContainer, "Spawn" + Environment.NewLine + "Container"),
			};

		static public string ZuOverviewObjektGrupeGbsReprText(SictOverviewObjektGrupeEnum OverviewObjektGrupeSictEnum)
		{
			var MengeZuOverviewObjektGrupeGbsReprText = SictAutoKonfig.MengeZuOverviewObjektGrupeGbsReprText;

			if (null != MengeZuOverviewObjektGrupeGbsReprText)
			{
				foreach (var ZuOverviewObjektGrupeGbsReprText in MengeZuOverviewObjektGrupeGbsReprText)
				{
					if (ZuOverviewObjektGrupeGbsReprText.Key == OverviewObjektGrupeSictEnum)
					{
						return ZuOverviewObjektGrupeGbsReprText.Value;
					}
				}
			}

			return OverviewObjektGrupeSictEnum.ToString();
		}

		public void KonfigScraibeNaacGbs(
			SictOptimatParam Konfig,
			bool VorherigeErhalte = false)
		{
			var InRaumVerhalteBaasis = (null == Konfig) ? null : Konfig.InRaumVerhalteBaasis;
			var MengeFitting = (null == Konfig) ? null : Konfig.MengeFitting;
			var Mine = (null == Konfig) ? null : Konfig.Mine;
			var Mission = (null == Konfig) ? null : Konfig.Mission;

			var AutoPilotLowSecFraigaabe = ((null == Konfig) ? null : Konfig.AutoPilotLowSecFraigaabe) ?? false;

			CheckBoxAutoPilotLowSecFraigaabe.IsChecked = AutoPilotLowSecFraigaabe;

			var AutoShipRepairFraigaabe = (null == Konfig) ? null : Konfig.AutoShipRepairFraigaabe;
			var AutoChatLocalVerbergeNictFraigaabe = (null == Konfig) ? null : Konfig.AutoChatLocalVerbergeNictFraigaabe;
			var AutoChatLocalÖfneFraigaabe = (null == Konfig) ? null : Konfig.AutoChatLocalÖfneFraigaabe;

			var AutoPilotFraigaabe = (null == Konfig) ? null : Konfig.AutoPilotFraigaabe;
			var AutoMineFraigaabe = (null == Konfig) ? null : Konfig.AutoMineFraigaabe;
			var AutoMissionFraigaabe = (null == Konfig) ? null : Konfig.AutoMissionFraigaabe;

			var MissionMengeZuFactionFittingBezaicner = (null == Mission) ? null : Mission.MengeZuFactionFittingBezaicner;

			KeyValuePair<string, string>[] MengeZuFactionAusFittingManagementFittingZuLaade = null;

			MengeZuFactionAusFittingManagementFittingZuLaade = MissionMengeZuFactionFittingBezaicner;

			SictOptimatParamFittingVerkürzt FittingKonfigVerkürzt = null;

			if (null != Konfig)
			{
				FittingKonfigVerkürzt = new SictOptimatParamFittingVerkürzt();

				FittingKonfigVerkürzt.InRaumVerhalte = InRaumVerhalteBaasis;
				FittingKonfigVerkürzt.MengeZuFactionAusFittingManagementFittingZuLaade = MengeZuFactionAusFittingManagementFittingZuLaade;
			}

			SctoierelementFittingVerkürzt.KonfigScraibeNaacGbs(FittingKonfigVerkürzt, VorherigeErhalte);
			SctoierelementMine.KonfigScraibeNaacGbs(Mine, VorherigeErhalte);
			SctoierelementMission.KonfigScraibeNaacGbs(Mission, VorherigeErhalte);

			RadioButtonAutoPilotFraigaabe.IsChecked = AutoPilotFraigaabe ?? false;
			RadioButtonAutoMineFraigaabe.IsChecked = AutoMineFraigaabe ?? false;
			RadioButtonAutoMissionFraigaabe.IsChecked = AutoMissionFraigaabe ?? false;

			CheckBoxAutoShipRepairFraigaabe.IsChecked = AutoShipRepairFraigaabe ?? false;
			CheckBoxAutoChatLocalVerbergeNict.IsChecked = AutoChatLocalVerbergeNictFraigaabe ?? false;
			CheckBoxAutoChatLocalÖfne.IsChecked = AutoChatLocalÖfneFraigaabe ?? false;
		}

		public void VonOptimatZuusctandApliziire(
			SictVonOptimatMeldungZuusctand VonOptimatZuusctand)
		{
			SctoierelementMission.VonOptimatZuusctandApliziire(VonOptimatZuusctand);
			SctoierelementFittingVerkürzt.VonOptimatZuusctandApliziire(VonOptimatZuusctand);
		}

		public void KonfigBerecneAusGbs(ref	SictOptimatParam Konfig)
		{
			if (null == Konfig)
			{
				Konfig = new SictOptimatParam();
			}

			var KonfigMine = Konfig.Mine;
			var KonfigMission = Konfig.Mission;

			var AutoPilotLowSecFraigaabe = CheckBoxAutoPilotLowSecFraigaabe.IsChecked ?? false;

			var OverviewDataGridMengePresetRepr = this.OverviewDataGridMengePresetRepr;

			var OverviewDataGridMengePreset =
				(null == OverviewDataGridMengePresetRepr) ? null :
				OverviewDataGridMengePresetRepr
				.Select((OverviewPresetRepr) => (null == OverviewPresetRepr) ? null : OverviewPresetRepr.Wert)
				.ToArray();

			var OverViewMengeZuOverviewPresetIdentMengeObjektGrupeSictbar =
				(null == OverviewDataGridMengePreset) ? null :
				OverviewDataGridMengePreset
				.Select((PresetRepr) => new KeyValuePair<string, SictOverviewObjektGrupeEnum[]>(
					PresetRepr.OverviewPresetName,
					PresetRepr.LeeseTailMengePropertyTrue()))
				.ToList();

			var OverViewMengeZuTabNameOverviewPresetIdent =
				(null == OverviewDataGridMengePreset) ? null :
				OverviewDataGridMengePreset
				.Select((PresetRepr) => new KeyValuePair<string, string>(
					PresetRepr.OverviewTabName,
					PresetRepr.OverviewPresetName))
				.ToList();

			var KonfigFittingVerkürzt = SctoierelementFittingVerkürzt.KonfigBerecneAusGbs();

			KeyValuePair<string, SictOptimatParamFitting>[] MengeZuFactionFitting = null;

			SictParamInRaumAktioonVerzwaigungNaacShipZuusctandScranke InRaumVerhalteBaasis = null;

			if (null != KonfigFittingVerkürzt)
			{
				MengeZuFactionFitting = KonfigFittingVerkürzt.MengeFittingErsctele();
				InRaumVerhalteBaasis = KonfigFittingVerkürzt.InRaumVerhalte;
			}

			SctoierelementMine.KonfigBerecneAusGbs(ref	KonfigMine);
			SctoierelementMission.KonfigBerecneAusGbs(ref	KonfigMission);

			var MengeZuFactionFittingBezaicner =
				(null == MengeZuFactionFitting) ? null :
				MengeZuFactionFitting
				.Select((ZuFactionFitting) => new KeyValuePair<string, string>(ZuFactionFitting.Key, ZuFactionFitting.Value.FittingBezaicner))
				.ToArray();

			if (!Bib3.Glob.SequenceEqual(
				MengeZuFactionFittingBezaicner,
				KonfigMission.MengeZuFactionFittingBezaicner,
				(Element0, Element1) => string.Equals(Element0.Key, Element1.Key) &&
					string.Equals(Element0.Value, Element1.Value)))
			{
				KonfigMission.MengeZuFactionFittingBezaicner = MengeZuFactionFittingBezaicner;
			}

			var MengeFitting =
				(null == MengeZuFactionFitting) ? null :
				MengeZuFactionFitting
				.Select((ZuFactionFitting) => ZuFactionFitting.Value)
				.Distinct(new SictOptimatParamFittingComparer())
				.ToArray();

			Konfig.AutoPilotLowSecFraigaabe = AutoPilotLowSecFraigaabe;

			Konfig.AutoShipRepairFraigaabe = CheckBoxAutoShipRepairFraigaabe.IsChecked;
			Konfig.AutoChatLocalVerbergeNictFraigaabe = CheckBoxAutoChatLocalVerbergeNict.IsChecked;
			Konfig.AutoChatLocalÖfneFraigaabe = CheckBoxAutoChatLocalÖfne.IsChecked;

			Konfig.AutoPilotFraigaabe = RadioButtonAutoPilotFraigaabe.IsChecked;
			Konfig.AutoMineFraigaabe = RadioButtonAutoMineFraigaabe.IsChecked;
			Konfig.AutoMissionFraigaabe = RadioButtonAutoMissionFraigaabe.IsChecked;

			if (!Bib3.Glob.SequenceEqual(
				MengeFitting,
				Konfig.MengeFitting,
				(Element0, Element1) => SictOptimatParamFitting.HinraicendGlaicwertigFürInVonNuzerParamIdent(
					Element0, Element1)))
			{
				Konfig.MengeFitting = MengeFitting;
			}

			if (!SictParamInRaumAktioonVerzwaigungNaacShipZuusctandScranke.HinraicendGlaicwertigFürInVonNuzerParamIdent(
				InRaumVerhalteBaasis,
				Konfig.InRaumVerhalteBaasis))
			{
				Konfig.InRaumVerhalteBaasis = InRaumVerhalteBaasis;
			}

			Konfig.Mine = KonfigMine;
			Konfig.Mission = KonfigMission;
		}

		void NaacKonfigGeändert()
		{
			this.RaiseEvent(new RoutedEventArgs(KonfigGeändertEvent, this));
		}

		IEnumerable<DataGridColumn> DataGridOverviewMengePresetMengeColumnObjektGrupeSictbarBerecne()
		{
			var MengeColumnObjektGrupeSictbar = new List<DataGridColumn>();

			bool MengeColumnObjektGrupeSictbarBegin = false;

			foreach (var Column in DataGridOverviewMengePreset.Columns)
			{
				if (MengeColumnObjektGrupeSictbarBegin)
				{
					MengeColumnObjektGrupeSictbar.Add(Column);
				}

				if (Column == DataGridOverviewMengePresetColumnOverviewTabName)
				{
					MengeColumnObjektGrupeSictbarBegin = true;
				}
			}

			return MengeColumnObjektGrupeSictbar;
		}

		private void DataGridOverviewMengePreset_LayoutUpdated(object sender, EventArgs e)
		{
			try
			{
				Bib3.FCL.GBS.Extension.FrameworkElementInCanvasLaageRicteNaacDataGridMengeColumnHeader(
					DataGridOverviewMengePresetHeaderGrupeObjGrupeSictbar,
					DataGridOverviewMengePreset,
					DataGridOverviewMengePresetMengeColumnObjektGrupeSictbarBerecne());
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}

		private void CheckBoxAutoChatLocalÖfne_Checked(object sender, RoutedEventArgs e)
		{
			CheckBoxAutoChatLocalVerbergeNict.IsChecked = true;
		}

		private void CheckBoxAutoChatLocalVerbergeNict_Unchecked_1(object sender, RoutedEventArgs e)
		{
			CheckBoxAutoChatLocalVerbergeNict.IsChecked = false;
		}

		private void RadioButtonAutoPilotFraigaabe_Checked(object sender, RoutedEventArgs e)
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

		private void RadioButtonAutoMineFraigaabe_Checked(object sender, RoutedEventArgs e)
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

		private void RadioButtonAutoMissionFraigaabe_Checked(object sender, RoutedEventArgs e)
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
	}

	public class SictOverviewPresetUndTabMengeObjektGrupeSictbarReprInDataGrid : System.ComponentModel.INotifyPropertyChanged
	{
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

		public string OverviewPresetName
		{
			set;
			get;
		}

		public string OverviewTabName
		{
			set;
			get;
		}

		[JsonProperty]
		public bool AccelerationGate
		{
			set;
			get;
		}

		[JsonProperty]
		public bool Station
		{
			set;
			get;
		}

		[JsonProperty]
		public bool Rat
		{
			set;
			get;
		}

		[JsonProperty]
		public bool Wreck
		{
			set;
			get;
		}

		[JsonProperty]
		public bool CargoContainer
		{
			set;
			get;
		}

		[JsonProperty]
		public bool SpawnContainer
		{
			set;
			get;
		}

		[JsonProperty]
		public bool LargeCollidableStructure
		{
			set;
			get;
		}

		[JsonProperty]
		public bool DeadspaceStructure
		{
			set;
			get;
		}

		public SictOverviewPresetUndTabMengeObjektGrupeSictbarReprInDataGrid()
		{
		}

		public SictOverviewPresetUndTabMengeObjektGrupeSictbarReprInDataGrid(
			string OverviewPresetName,
			string OverviewTabName = null,
			SictOverviewObjektGrupeEnum[] MengePropertyTrue = null)
		{
			this.OverviewPresetName = OverviewPresetName;
			this.OverviewTabName = OverviewTabName;

			PropertySeze(MengePropertyTrue);
		}

		public void PropertySeze(SictOverviewObjektGrupeEnum[] MengePropertyTrue)
		{
			Optimat.Glob.MengeMemberSezeAlsBoolEnthalteInMengeEnum(this, MengePropertyTrue);
		}

		public SictOverviewObjektGrupeEnum[] LeeseTailMengePropertyTrue()
		{
			var Enumerable = Optimat.Glob.TailMengeMemberEnthalteInEnumMengeWertUndEqualZuObjekt<SictOverviewObjektGrupeEnum>(this, true);

			return (null == Enumerable) ? null : Enumerable.ToArray();
		}
	}
}
