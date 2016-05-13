using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Bib3;
using Optimat.EveOnline.Base;
using Optimat.ScpezEveOnln;
using Newtonsoft.Json;
//using Optimat.EveOnline.AuswertGbs;

namespace Optimat.EveOnline.Anwendung
{
	[JsonObject(MemberSerialization.OptIn)]
	public class ZuInRaumObjektInfoMine
	{
		[JsonProperty]
		readonly public string OreTypSictString;

		[JsonProperty]
		readonly public OreTypSictEnum? OreTyp;

		[JsonProperty]
		readonly public bool? OreTypFraigaabe;

		[JsonProperty]
		readonly public ZuTargetAinscrankungMengeSurveyScanItem AusSurveyScanInfo;

		[JsonProperty]
		readonly public KeyValuePair<ModuleAktivitäätZaitraum, int?>[] MengeModuleAktivitäätUndZyyklusFortscritMili;

		[JsonProperty]
		readonly public Int64? ErzMengeRestScrankeMin;

		[JsonProperty]
		readonly public Int64? ErzMengeRestScrankeMax;

		[JsonProperty]
		readonly public bool? MinerAktivitäätZyyklusGrenzeNaheSurveyScan;

		[JsonProperty]
		readonly public int? InFalAnfluugDirektDauerBisInRaicwaiteMiner;

		[JsonProperty]
		readonly public int? MengeAssignedAnzaal;

		[JsonProperty]
		readonly public Int64? MengeAssignedVolumePerZyyklusAgregatioonScrankeMax;

		public Int64? AusSurveyScanOreVolumeScrankeMin
		{
			get
			{
				var AusSurveyScanInfo = this.AusSurveyScanInfo;

				if (null == AusSurveyScanInfo)
				{
					return null;
				}

				return AusSurveyScanInfo.OreVolumeMiliScrankeMin / 1000;
			}
		}

		public GbsListGroupedEntryZuusctand AusSurveyScanListGroup
		{
			get
			{
				var AusSurveyScanInfo = this.AusSurveyScanInfo;

				if (null == AusSurveyScanInfo)
				{
					return null;
				}

				return AusSurveyScanInfo.ListGroup;
			}
		}

		public IEnumerable<GbsListGroupedEntryZuusctand> AusSurveyScanMengeListItemPasendZuInRaumObjekt
		{
			get
			{
				var AusSurveyScanInfo = this.AusSurveyScanInfo;

				if (null == AusSurveyScanInfo)
				{
					return null;
				}

				return AusSurveyScanInfo.MengeKandidaatListItem;
			}
		}

		public ZuInRaumObjektInfoMine()
		{
		}

		public ZuInRaumObjektInfoMine(
			string OreTypSictString,
			OreTypSictEnum? OreTyp,
			bool? OreTypFraigaabe,

			ZuTargetAinscrankungMengeSurveyScanItem AusSurveyScanInfo,

			KeyValuePair<ModuleAktivitäätZaitraum, int?>[] MengeModuleAktivitäätUndZyyklusFortscritMili,
			Int64? ErzMengeRestScrankeMin,
			Int64? ErzMengeRestScrankeMax,
			bool? MinerAktivitäätZyyklusGrenzeNaheSurveyScan,
			int? MengeAssignedAnzaal,
			Int64? MengeAssignedVolumePerZyyklusAgregatioonScrankeMax)
		{
			this.OreTypSictString = OreTypSictString;
			this.OreTyp = OreTyp;
			this.OreTypFraigaabe = OreTypFraigaabe;

			this.AusSurveyScanInfo = AusSurveyScanInfo;

			this.MengeModuleAktivitäätUndZyyklusFortscritMili = MengeModuleAktivitäätUndZyyklusFortscritMili;
			this.ErzMengeRestScrankeMin = ErzMengeRestScrankeMin;
			this.ErzMengeRestScrankeMax = ErzMengeRestScrankeMax;
			this.MinerAktivitäätZyyklusGrenzeNaheSurveyScan = MinerAktivitäätZyyklusGrenzeNaheSurveyScan;
			this.MengeAssignedAnzaal = MengeAssignedAnzaal;
			this.MengeAssignedVolumePerZyyklusAgregatioonScrankeMax = MengeAssignedVolumePerZyyklusAgregatioonScrankeMax;
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public partial class SictAutoMine
	{
		[JsonProperty]
		readonly int TargetAssignmentMeerereKarenzDauer = 11000;

		[JsonProperty]
		public string StationZiil
		{
			private set;
			get;
		}

		[JsonProperty]
		public string[] ListePrioAsteroidBeltBescriftung
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? AnnaameNaacAbbrucMinerZyyklusOreHoldGefült
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? CurrentLocationIstBelt
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictOverviewPresetFolgeViewport OverviewDefaultMiningFolgeViewportFertigLezte
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? OverviewDefaultMiningFolgeViewportFertigLezteHinraicendAktuel
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? DockUndOffloadPrioVorMine
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? WarpToBeltPrioVorMine
		{
			private set;
			get;
		}

		[JsonProperty]
		public string OverviewTabBevorzuugtTitel
		{
			private set;
			get;
		}

		[JsonProperty]
		public OreTypSictEnum[] MengeMiningCrystalTypGelaade
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? ShipMengeModuleMinerZyyklusVolumeMax
		{
			private set;
			get;
		}

		[JsonProperty]
		public OreTypSictEnum[] MengeMiningCrystalTypVerfüügbar
		{
			private set;
			get;
		}

		[JsonProperty]
		public OreTypSictEnum[] MengeOreTypFraigaabe
		{
			private set;
			get;
		}

		[JsonProperty]
		public int? MengeAsteroidTargetAnzaalScranke
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? TargetDistanceScrankeMax
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? MengeAsteroidInRaicwaiteAnzaalAusraicend
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictTargetZuusctand TargetAnzufliigeNääxte
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? MengeAsteroidLockedAusraicend
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? TargetAssignmentKarenzDauerRest
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? TargetAssignmentMeerereKarenzDauerRest
		{
			private set;
			get;
		}

		[JsonProperty]
		public KeyValuePair<SictTargetZuusctand, ZuInRaumObjektInfoMine>[] MengeTargetVerwendet
		{
			private set;
			get;
		}

		[JsonProperty]
		public KeyValuePair<SictOverViewObjektZuusctand, ZuInRaumObjektInfoMine>[] MengeOverviewObjektVerwendet
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictOverViewObjektZuusctand AsteroidZuLockeNääxte
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? InOverviewSuuceAsteroid
		{
			private set;
			get;
		}

		[JsonProperty]
		public KeyValuePair<SictOverViewObjektZuusctand, ZuInRaumObjektInfoMine> OverviewObjektFraigaabeLockedNictNääxte
		{
			private set;
			get;
		}

		[JsonProperty]
		public KeyValuePair<SictTargetZuusctand, ZuInRaumObjektInfoMine> ListeTargetVerwendetMengeErzRestZuMeseNääxte
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64 ShipMengeModuleMinerKandidaatCrystalMenuLezteZait
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictShipUiModuleReprZuusctand MesungMiningCrystalVerfüügbarModule
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64 ShipMengeModuleMinerRange
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? ShipMengeModuleMinerNuldurcgangLezteZait
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? ShipMengeModuleMinerNuldurcgangNääxteZait
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? SurveyScanBeginZaitScrankeMin
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? SaitSurveyScanLezteShipStreke
		{
			private set;
			get;
		}

		static public IEnumerable<OreTypSictEnum> MengeOreTypFraigaabeBerecne(
			IEnumerable<OreTypSictEnum> VonNuzerParamMengeOreTypFraigaabe,
			IEnumerable<OreTypSictEnum> MengeMiningCrystalTypVerfüügbar,
			bool VonNuzerParamMengeOreTypeBescrankeNaacMiningCrystal)
		{
			var MengeOreTypFraigaabe = VonNuzerParamMengeOreTypFraigaabe;

			if (MengeOreTypFraigaabe.NullOderLeer())
			{
				MengeOreTypFraigaabe = MengeOreTypAleBerecne();
			}

			if (VonNuzerParamMengeOreTypeBescrankeNaacMiningCrystal)
			{
				MengeOreTypFraigaabe =
					MengeOreTypFraigaabe.IntersectNullable(MengeMiningCrystalTypVerfüügbar).ToArrayNullable();
			}

			if (MengeOreTypFraigaabe.NullOderLeer())
			{
				MengeOreTypFraigaabe = MengeOreTypAleBerecne();
			}

			return MengeOreTypFraigaabe;
		}

		public IEnumerable<SictAufgaabeParam> FürMineListeAufgaabeNääxteParamBerecne(
			ISictAutomatZuusctand AutomaatZuusctand)
		{
			if (null == AutomaatZuusctand)
			{
				return null;
			}

			var ListeAufgaabeParam = new List<SictAufgaabeParam>();

			var ScnapscusLezte = AutomaatZuusctand.ListeScnapscusLezteAuswertungErgeebnisNaacSimu;

			var NuzerZaitMili = AutomaatZuusctand.NuzerZaitMili;
			var FittingUndShipZuusctand = AutomaatZuusctand.FittingUndShipZuusctand;
			var OverviewUndTarget = AutomaatZuusctand.OverviewUndTarget;

			if (null == FittingUndShipZuusctand)
			{
				return null;
			}

			var GbsButtonListSurroundings =
				(null == ScnapscusLezte) ? null :
				ScnapscusLezte.InfoPanelLocationInfoButtonListSurroundings();

			var AnnaameOreHoldLeer = FittingUndShipZuusctand.AnnaameOreHoldLeer;
			var AnnaameNaacAbbrucMinerZyyklusOreHoldGefültMili = FittingUndShipZuusctand.AnnaameNaacAbbrucMinerZyyklusOreHoldGefültMili;

			var OreHoldCapacityMesungLezteMitZait = FittingUndShipZuusctand.OreHoldCapacityMesungLezteMitZait;

			var OreHoldCapacityMesungLezteAlterMili = NuzerZaitMili - OreHoldCapacityMesungLezteMitZait.Zait;

			var ShipZuusctand = FittingUndShipZuusctand.ShipZuusctand;

			if (null == ShipZuusctand)
			{
				return null;
			}

			var ShipZuusctandWarping = ShipZuusctand.Warping;
			var ShipZuusctandDocking = ShipZuusctand.Docking;
			var ShipZuusctandWarpingLezteZaitMili = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.WarpingLezteZaitMili;

			var MengeOverViewObjekt = (null == OverviewUndTarget) ? null : OverviewUndTarget.MengeOverViewObjekt;
			var MengeTargetNocSictbar = (null == OverviewUndTarget) ? null : OverviewUndTarget.MengeTargetNocSictbar;

			var MengeAsteroidLockedAusraicend = this.MengeAsteroidLockedAusraicend;
			var MengeAsteroidInRaicwaiteAnzaalAusraicend = this.MengeAsteroidInRaicwaiteAnzaalAusraicend;
			var TargetAnzufliigeNääxte = this.TargetAnzufliigeNääxte;
			var MengeTargetVerwendet = this.MengeTargetVerwendet;
			var MengeOverviewObjektVerwendet = this.MengeOverviewObjektVerwendet;
			var ListeTargetVerwendetMengeErzRestZuMeseNääxte = this.ListeTargetVerwendetMengeErzRestZuMeseNääxte;

			var MengeOverviewObjektVerwendetFraigaabe =
				MengeOverviewObjektVerwendet
				.WhereNullable((Kandidaat) => Kandidaat.Value.OreTypFraigaabe ?? false)
				.ToArrayNullable();

			var VonNuzerParamMine = AutomaatZuusctand.VonNuzerParamMine();

			var VonNuzerParamMineSurveyScannerFraigaabe = (null == VonNuzerParamMine) ? null : VonNuzerParamMine.SurveyScannerFraigaabe;

			var ShipMengeModule = AutomaatZuusctand.ShipMengeModule();

			var MesungShipIsPodLezteZaitUndWert = AutomaatZuusctand.MesungShipIsPodLezteZaitUndWert();

			bool ShipIsPod = false;

			if (MesungShipIsPodLezteZaitUndWert.HasValue)
			{
				if (MesungShipIsPodLezteZaitUndWert.Value.Wert)
				{
					ShipIsPod = true;
				}
			}

			var ListeTargetVerwendet =
				MengeTargetVerwendet
				.OrderByNullable((Kandidaat) => Kandidaat.Key.SictungLezteDistanceScrankeMaxScpezTarget ?? int.MaxValue)
				.OrderByNullable((Kandidaat) => true == Kandidaat.Key.InputFookusTransitioonLezteZiilWert ? -1 : 0)
				.ToArrayNullable();

			var ShipMengeModuleMiner =
				ShipMengeModule
				.WhereNullable((Kandidaat) => Kandidaat.IstMiner ?? false)
				.ToArrayNullable();

			/*
			 * 2014.09.30
			 * 
			var ShipMengeModuleMinerMenuAuswertScpez =
				ShipMengeModuleMiner
				.SelectNullable((Kandidaat) => Kandidaat.MenuLezteScpezModuleButtonMitZait)
				.WhereNullable((Kandidaat) => null != Kandidaat.Wert)
				.ToArrayNullable();
			 * */

			/*
			 * 2014.09.30
			 * 
			 * für inklusioon in Berict Verlaagerung naac Member.
			 * 
			var AnnaameNaacAbbrucMinerZyyklusOreHoldGefült = 990 < AnnaameNaacAbbrucMinerZyyklusOreHoldGefültMili;

			var DockUndOffloadPrioVorMine = AnnaameNaacAbbrucMinerZyyklusOreHoldGefült;
			 * */

			var Gbs = AutomaatZuusctand.Gbs();

			var OverViewScrolledToTopLezteAlter = AutomaatZuusctand.OverViewScrolledToTopLezteAlter();
			var WindowOverviewScroll = AutomaatZuusctand.WindowOverviewScnapscusLezteScroll();

			if (ShipIsPod)
			{
				ListeAufgaabeParam.Add(new AufgaabeParamShipFluct());

				ListeAufgaabeParam.Add(AufgaabeParamAndere.KonstruktNaacNuzerMeldungZuEveOnline(
					SictNaacNuzerMeldungZuEveOnline.ErrorGenerel(-1,
					new SictNaacNuzerMeldungZuEveOnlineCause(SictNaacNuzerMeldungZuEveOnlineCauseTypeBinär.ShipFittingInappropriate))));

				return ListeAufgaabeParam;
			}

			if (DockUndOffloadPrioVorMine ?? false)
			{
				//	Ore Hold Vol, Warp naac Station.

				if (!(ShipZuusctandDocking ?? false))
				{
					ListeAufgaabeParam.Add(new AufgaabeParamShipDock(StationZiil));
				}

				if (AnnaameOreHoldLeer ?? false)
				{
				}
				else
				{
					//	Wen Docked, Ore Hold leere.

					ListeAufgaabeParam.Add(new AufgaabeParamShipAktuelCargoLeereTyp(SictShipCargoTypSictEnum.OreHold));
				}
			}
			else
			{
				if (WarpToBeltPrioVorMine ?? false)
				{
					var WarpToBeltListeAufgaabeParam = this.FürMineListeAufgaabeNääxteParamBerecneTailWarpToBelt(AutomaatZuusctand);

					if (null != WarpToBeltListeAufgaabeParam)
					{
						ListeAufgaabeParam.AddRange(
							WarpToBeltListeAufgaabeParam
							.Select((AufgaabeParam) => SictAufgaabeParam.KonstruktAufgaabeParam(AufgaabeParam, "Warp to Belt")));
					}
				}
				else
				{
					if (!(ShipZuusctandWarping ?? false))
					{
						{
							var InBeltMineListeAufgaabeParam = this.FürMineListeAufgaabeNääxteParamBerecneTailInBeltMine(AutomaatZuusctand);

							if (null != InBeltMineListeAufgaabeParam)
							{
								ListeAufgaabeParam.AddRange(
									InBeltMineListeAufgaabeParam
									.Select((AufgaabeParam) => SictAufgaabeParam.KonstruktAufgaabeParam(AufgaabeParam, "Mine")));
							}
						}

						if (VonNuzerParamMineSurveyScannerFraigaabe ?? false)
						{
							var SurveyScanListeAufgaabeParam = this.FürMineListeAufgaabeNääxteParamBerecneTailSurveyScan(AutomaatZuusctand);

							if (null != SurveyScanListeAufgaabeParam)
							{
								ListeAufgaabeParam.AddRange(
									SurveyScanListeAufgaabeParam
									.Select((AufgaabeParam) => SictAufgaabeParam.KonstruktAufgaabeParam(AufgaabeParam, "Survey Scan")));
							}
						}
					}
				}
			}

			if (!(DockUndOffloadPrioVorMine ?? false) &&
				null != MesungMiningCrystalVerfüügbarModule)
			{
				ListeAufgaabeParam.Add(AufgaabeParamAndere.AufgaabeAktioonMenu(
					MesungMiningCrystalVerfüügbarModule.GbsObjektToggleBerecne(),
					new SictAnforderungMenuKaskaadeAstBedingung("non existant Entry", false), new string[] { "measure available Mining Crystal Types" }));
			}

			if (0 < MengeTargetNocSictbar.CountNullable())
			{
				//	mit niidrige(lezte) Prio Window Inventory öfne und Ore Hold auswääle.
				ListeAufgaabeParam.Add(new AufgaabeParamShipAktuelOpenInventoryCargoTyp(SictShipCargoTypSictEnum.OreHold));
			}

			if (!(OverViewScrolledToTopLezteAlter < 16666) &&
				null != WindowOverviewScroll)
			{
				ListeAufgaabeParam.Add(new AufgaabeParamScrollToTop(WindowOverviewScroll));
			}

			return ListeAufgaabeParam;
		}

		static public OreTypSictEnum[] MengeOreTypAleBerecne()
		{
			return Enum.GetValues(typeof(OreTypSictEnum)).Cast<OreTypSictEnum>().ToArray();
		}

		public void Aktualisiire(
			SictAutomatZuusctand AutomaatZuusctand,
			VonSensorikMesung AusScnapscusAuswertungZuusctand)
		{
			var MengeTargetVerwendet = new List<KeyValuePair<SictTargetZuusctand, ZuInRaumObjektInfoMine>>();

			bool? AnnaameNaacAbbrucMinerZyyklusOreHoldGefült = null;
			bool? OverviewDefaultMiningFolgeViewportFertigLezteHinraicendAktuel = null;
			bool? CurrentLocationIstBelt = null;
			bool? DockUndOffloadPrioVorMine = null;
			bool? WarpToBeltPrioVorMine = null;
			var MengeOverviewObjektVerwendet = new List<KeyValuePair<SictOverViewObjektZuusctand, ZuInRaumObjektInfoMine>>();
			SictOverViewObjektZuusctand AsteroidZuLockeNääxte = null;
			bool? InOverviewSuuceAsteroid = null;
			var ListeTargetVerwendetMengeErzRestZuMeseNääxte = default(KeyValuePair<SictTargetZuusctand, ZuInRaumObjektInfoMine>);
			var MengeMiningCrystalTypGelaade = new List<OreTypSictEnum>();
			var MengeMiningCrystalTypVerfüügbar = new List<OreTypSictEnum>();
			Int64? ShipMengeModuleMinerZyyklusVolumeMax = null;
			OreTypSictEnum[] MengeOreTypFraigaabe = null;
			Int64 ShipMengeModuleMinerKandidaatCrystalMenuLezteZait = int.MinValue;
			SictShipUiModuleReprZuusctand MesungMiningCrystalVerfüügbarModule = null;
			Int64 ShipMengeModuleMinerRange = 0;
			Int64? SaitSurveyScanLezteShipStreke = null;
			Int64? ShipMengeModuleMinerNuldurcgangLezteZait = null;
			Int64? SurveyScanBeginZaitScrankeMin = null;
			string OverviewTabBevorzuugtTitel = null;
			int? MengeAsteroidTargetAnzaalScranke = null;
			Int64? TargetDistanceScrankeMax = null;
			bool? MengeAsteroidLockedAusraicend = null;
			bool? MengeAsteroidInRaicwaiteAnzaalAusraicend = null;
			SictTargetZuusctand TargetAnzufliigeNääxte = null;
			Int64? TargetAssignmentKarenzDauerRest = null;
			Int64? TargetAssignmentMeerereKarenzDauerRest = null;
			Int64? ShipMengeModuleMinerNuldurcgangNääxteZait = null;
			var OverviewObjektFraigaabeLockedNictNääxte = default(KeyValuePair<SictOverViewObjektZuusctand, ZuInRaumObjektInfoMine>);
			string[] ListePrioAsteroidBeltBescriftung = null;
			Int64? MengeOverviewObjektVerwendetFraigaabeNääxteDistance = null;

			try
			{
				if (null == AutomaatZuusctand)
				{
					return;
				}

				if (null == AusScnapscusAuswertungZuusctand)
				{
					return;
				}

				var OptimatParam = AutomaatZuusctand.OptimatParam();

				var VonNuzerParamMine = (null == OptimatParam) ? null : OptimatParam.Mine;

				var VonNuzerParamMengeOreTypFraigaabe = (null == VonNuzerParamMine) ? null : VonNuzerParamMine.MengeOreTypFraigaabe;
				var VonNuzerParamMengeOreTypeBescrankeNaacMiningCrystal = (null == VonNuzerParamMine) ? null : VonNuzerParamMine.MengeOreTypeBescrankeNaacMiningCrystal;

				var NuzerZaitMili = AutomaatZuusctand.NuzerZaitMili;
				var FittingUndShipZuusctand = AutomaatZuusctand.FittingUndShipZuusctand;
				var OverviewUndTarget = AutomaatZuusctand.OverviewUndTarget;

				var CurrentLocationInfo =
					AusScnapscusAuswertungZuusctand.CurrentLocationInfo();

				var MengeTarget = AutomaatZuusctand.MengeTarget();

				var MengeZuListEntryTarget = AutomaatZuusctand.MengeZuListEntryTargetAinscrankungPerDistance();

				var ShipMengeModule = AutomaatZuusctand.ShipMengeModule();

				var ListeLocationNearest = AutomaatZuusctand.ListeLocationNearest();

				var SolarSystem = AutomaatZuusctand.EveWeltSolarSystemCurrent();

				if (null == FittingUndShipZuusctand)
				{
					return;
				}

				if (null != CurrentLocationInfo)
				{
					CurrentLocationIstBelt = Regex.Match(CurrentLocationInfo.NearestName ?? "", "roid\\s*belt", RegexOptions.IgnoreCase).Success;
				}

				var ShipZuusctand = FittingUndShipZuusctand.ShipZuusctand;

				var AnnaameOreHoldLeer = FittingUndShipZuusctand.AnnaameOreHoldLeer;
				var CharZuusctandDocked = (null == ShipZuusctand) ? null : ShipZuusctand.Docked;
				var CharZuusctandWarpingLezteZaitMili = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.WarpingLezteZaitMili;

				var MengeOverViewObjekt = (null == OverviewUndTarget) ? null : OverviewUndTarget.MengeOverViewObjekt;

				var ShipMengeModuleMiner =
					ShipMengeModule
					.WhereNullable((Kandidaat) => Kandidaat.IstMiner ?? false)
					.ToArrayNullable();

				var ShipMengeModuleMinerKandidaatMiningCrystal =
					ShipMengeModuleMiner
					.WhereNullable((Kandidaat) => Kandidaat.KanLaadeMiningCrystal ?? false)
					.ToArrayNullable();

				var ShipMengeModuleMinerFraigaabeNict =
					ShipMengeModuleMiner
					.WhereNullable((ModuleMiner) => !AutomaatZuusctand.MengeModuleUmscaltFraigaabe.ContainsNullable(ModuleMiner))
					.ToArrayNullable();

				var OverViewScrolledToTopLezteAlter = AutomaatZuusctand.OverViewScrolledToTopLezteAlter();

				var OverviewPresetFolgeViewportFertigLezte = (null == OverviewUndTarget) ? null : OverviewUndTarget.OverviewPresetFolgeViewportFertigLezte;

				if (null != OverviewPresetFolgeViewportFertigLezte)
				{
					if (OverviewPresetFolgeViewportFertigLezte.VolsctändigTailInGrid ?? false)
					{
						if (string.Equals(
							OverviewDefaultMiningIdent,
							Bib3.Glob.TrimNullable(OverviewPresetFolgeViewportFertigLezte.OverviewPresetName) ?? "",
							StringComparison.InvariantCultureIgnoreCase))
						{
							OverviewDefaultMiningFolgeViewportFertigLezte = OverviewPresetFolgeViewportFertigLezte;
						}
					}
				}

				if (null != OverviewDefaultMiningFolgeViewportFertigLezte)
				{
					var OverviewDefaultMiningFolgeViewportFertigLezteDauer =
						(OverviewDefaultMiningFolgeViewportFertigLezte.EndeZait ?? NuzerZaitMili) -
						OverviewDefaultMiningFolgeViewportFertigLezte.BeginZait;

					var OverviewDefaultMiningFolgeViewportFertigLezteAlter =
						NuzerZaitMili - OverviewDefaultMiningFolgeViewportFertigLezte.BeginZait;

					if (OverviewDefaultMiningFolgeViewportFertigLezteDauer.HasValue)
					{
						OverviewDefaultMiningFolgeViewportFertigLezteHinraicendAktuel =
							OverviewDefaultMiningFolgeViewportFertigLezteAlter <
							Math.Min(33333, OverviewDefaultMiningFolgeViewportFertigLezteDauer.Value * 2 + 5555);
					}
				}

				var AnnaameNaacAbbrucMinerZyyklusOreHoldGefültMili = FittingUndShipZuusctand.AnnaameNaacAbbrucMinerZyyklusOreHoldGefültMili;

				AnnaameNaacAbbrucMinerZyyklusOreHoldGefült = 999 <= AnnaameNaacAbbrucMinerZyyklusOreHoldGefültMili;

				/*
				 * 2014.10.00
				 * 
				DockUndOffloadPrioVorMine = AnnaameNaacAbbrucMinerZyyklusOreHoldGefült;
				 * */

				var OreHoldCapacityMesungLezteMitZaitNocGültig = FittingUndShipZuusctand.OreHoldCapacityMesungLezteMitZaitNocGültig;

				if (CharZuusctandDocked ?? false)
				{
					DockUndOffloadPrioVorMine = !(AnnaameOreHoldLeer ?? false);
				}
				else
				{
					if (null != OreHoldCapacityMesungLezteMitZaitNocGültig.Wert)
					{
						DockUndOffloadPrioVorMine =
							990 < (1000 * OreHoldCapacityMesungLezteMitZaitNocGültig.Wert.UsedMikro) / OreHoldCapacityMesungLezteMitZaitNocGültig.Wert.MaxMikro;
					}
				}

				if (0 < ShipMengeModuleMinerFraigaabeNict.CountNullable())
				{
					TargetAssignmentKarenzDauerRest = 4444;
				}

				var ShipMengeModuleMinerKandidaatCrystalMenuAuswertScpez =
					ShipMengeModuleMinerKandidaatMiningCrystal
					.SelectNullable((Kandidaat) => Kandidaat.MenuLezteScpezModuleButtonMitZait)
					.WhereNullable((Kandidaat) => null != Kandidaat.Wert)
					.ToArrayNullable();

				var WindowSurveyScanView = AutomaatZuusctand.WindowSurveyScanView();

				var WindowSurveyScanViewList = (null == WindowSurveyScanView) ? null : WindowSurveyScanView.ListHaupt;

				var WindowSurveyScanViewListListeEntry = (null == WindowSurveyScanViewList) ? null : WindowSurveyScanViewList.ListeEntry();

				var WindowSurveyScanViewListListeEntryGrupiirtNaacOreTyp =
					(null == WindowSurveyScanViewListListeEntry) ? null :
					WindowSurveyScanViewListListeEntry
					.GroupBy((Kandidaat) => Kandidaat.OreTypSictString ?? Kandidaat.BescriftungTailTitel)
					.ToArray();

				var WindowOverView = AutomaatZuusctand.WindowOverView();

				var WindowOverViewScnapscusTabGroupListeTab = AutomaatZuusctand.WindowOverviewScnapscusLezteListeTabNuzbar();

				if (null != WindowOverViewScnapscusTabGroupListeTab)
				{
					var OverviewTabBevorzuugt =
						WindowOverViewScnapscusTabGroupListeTab
						.OrderByDescending((KandidaatTab) => Regex.Match(KandidaatTab.LabelBescriftung ?? "", "Min(e|i)", RegexOptions.IgnoreCase).Success)
						.ThenByDescending((KandidaatTab) => Regex.Match(KandidaatTab.LabelBescriftung ?? "", "Ore", RegexOptions.IgnoreCase).Success)
						.FirstOrDefault();

					{
						//	2015.09.01

						OverviewTabBevorzuugt = OverviewTabBevorzuugt ?? WindowOverViewScnapscusTabGroupListeTab?.FirstOrDefault();
					}

					if (null != OverviewTabBevorzuugt)
					{
						OverviewTabBevorzuugtTitel = OverviewTabBevorzuugt.LabelBescriftung;
					}
				}

				if (null != WindowSurveyScanView)
				{
					SaitSurveyScanLezteShipStreke =
						(FittingUndShipZuusctand.SctrekeZurükgeleegtMiliInZaitraum(
						WindowSurveyScanView.ScnapscusFrühesteZait ?? int.MinValue,
						NuzerZaitMili + 3000)) / 990;
				}

				ShipMengeModuleMiner
					.ForEachNullable((ModuleMiner) =>
						{
							var ModuleMiningCrystalGelaade = ModuleMiner.MiningCrystalGelaade;

							if (!ModuleMiningCrystalGelaade.HasValue)
							{
								return;
							}

							MengeMiningCrystalTypGelaade.Add(ModuleMiningCrystalGelaade.Value);
						});

				ShipMengeModuleMiner
					.ForEachNullable((ModuleMiner) =>
						{
							var ModuleButtonHintGültigMitZait = ModuleMiner.ModuleButtonHintGültigMitZait;

							if (null == ModuleButtonHintGültigMitZait.Wert)
							{
								return;
							}

							var VolumeMiliPerTimespanMili = ModuleButtonHintGültigMitZait.Wert.VolumeMiliPerTimespanMili;

							if (!VolumeMiliPerTimespanMili.HasValue)
							{
								return;
							}

							ShipMengeModuleMinerZyyklusVolumeMax = Bib3.Glob.Max(ShipMengeModuleMinerZyyklusVolumeMax, VolumeMiliPerTimespanMili.Value.Key / 1000);
						});

				MengeMiningCrystalTypVerfüügbar.AddRange(MengeMiningCrystalTypGelaade);

				/*
				 * 2014.10.05
				 * 
				MengeOreTypFraigaabe =
					((0 < VonNuzerParamMengeOreTypFraigaabe.CountNullable()) ?
					VonNuzerParamMengeOreTypFraigaabe : MengeOreTypAleBerecne())
					.Intersect(
					(0 < MengeMiningCrystalTypVerfüügbar.CountNullable()) ?
					(IEnumerable<OreTypSictEnum>)MengeMiningCrystalTypVerfüügbar : MengeOreTypAleBerecne())
					.ToArray();
				 * */

				/*
				 * 2014.10.05
				 * 
				MengeOreTypFraigaabe = VonNuzerParamMengeOreTypFraigaabe;

				if (MengeOreTypFraigaabe.NullOderLeer())
				{
					MengeOreTypFraigaabe = MengeOreTypAleBerecne();
				}

				if (VonNuzerParamMengeOreTypeBescrankeNaacMiningCrystal ?? false)
				{
					MengeOreTypFraigaabe =
						MengeOreTypFraigaabe.IntersectNullable(MengeMiningCrystalTypVerfüügbar).ToArrayNullable();
				}

				if (MengeOreTypFraigaabe.NullOderLeer())
				{
					MengeOreTypFraigaabe = MengeOreTypAleBerecne();
				}
				 * */

				MengeOreTypFraigaabe =
					MengeOreTypFraigaabeBerecne(
					VonNuzerParamMengeOreTypFraigaabe,
					MengeMiningCrystalTypVerfüügbar,
					VonNuzerParamMengeOreTypeBescrankeNaacMiningCrystal ?? false)
					.ToArrayNullable();

				ShipMengeModuleMiner
					.ForEachNullable((ModuleMiner) =>
						{
							ShipMengeModuleMinerRange = Math.Max(ShipMengeModuleMinerRange, (ModuleMiner.RangeMax ?? ModuleMiner.RangeOptimal) ?? 0);

							var ModuleMinerMenuLezte = ModuleMiner.MenuLezte;

							if (null != ModuleMinerMenuLezte &&
								(ModuleMiner.KanLaadeMiningCrystal ?? false))
							{
								ShipMengeModuleMinerKandidaatCrystalMenuLezteZait =
									Math.Max(ShipMengeModuleMinerKandidaatCrystalMenuLezteZait, ModuleMinerMenuLezte.BeginZait ?? int.MinValue);
							}

							var ModuleMinerListeAktivitäätLezte = ModuleMiner.ListeAktivitäätLezte;

							if (null == ModuleMinerListeAktivitäätLezte)
							{
								return;
							}

							var ShipModuleMinerRotatioonProjektioonNuldurcgangNääxte = ModuleMinerListeAktivitäätLezte.RotatioonProjektioonNuldurcgangNääxte;

							if (NuzerZaitMili < ShipModuleMinerRotatioonProjektioonNuldurcgangNääxte)
							{
								ShipMengeModuleMinerNuldurcgangNääxteZait =
									Bib3.Glob.Min(ShipMengeModuleMinerNuldurcgangNääxteZait, ShipModuleMinerRotatioonProjektioonNuldurcgangNääxte);
							}

							ShipMengeModuleMinerNuldurcgangLezteZait =
								Bib3.Glob.Max(ShipMengeModuleMinerNuldurcgangLezteZait,
								ModuleMinerListeAktivitäätLezte.RotatioonNuldurcgangLezteZait ??
								ModuleMinerListeAktivitäätLezte.BeginZait);
						});

				ShipMengeModuleMinerKandidaatCrystalMenuAuswertScpez
					.ForEachNullable((ModuleMinerMenuAuswertScpez) =>
					{
						if (null == ModuleMinerMenuAuswertScpez.Wert)
						{
							return;
						}

						/*
						 * 2014.10.00
						 * 
						 * Änderung: Abhängig nur von Menu auf Module algemain.
						 * 
						ShipMengeModuleMinerKandidaatCrystalMenuLezteZait =
							Math.Max(ShipMengeModuleMinerKandidaatCrystalMenuLezteZait, ModuleMinerMenuAuswertScpez.Zait);
						 * */

						var ModuleMengeZuMenuEntryMiningCrystalOreType = ModuleMinerMenuAuswertScpez.Wert.MengeZuMenuEntryMiningCrystalOreType;

						ModuleMengeZuMenuEntryMiningCrystalOreType
							.ForEachNullable((MenuEntryMiningCrystal) =>
							{
								var MenuEntryMiningCrystalOreType = MenuEntryMiningCrystal.Value.Value;

								if (!MenuEntryMiningCrystalOreType.HasValue)
								{
									return;
								}

								MengeMiningCrystalTypVerfüügbar.Add(MenuEntryMiningCrystalOreType.Value);
							});
					});

				if (null != MengeOverViewObjekt)
				{
					foreach (var OverViewObjekt in MengeOverViewObjekt)
					{
						if (null == OverViewObjekt)
						{
							continue;
						}

						string OreTypSictString = null;
						OreTypSictEnum? OreTyp = null;

						if (!SictOverViewObjektZuusctand.OverviewObjektBescriftungIstAsteroid(OverViewObjekt, out OreTypSictString, out OreTyp))
						{
							continue;
						}

						if (!OreTyp.HasValue)
						{
							continue;
						}

						var OreTypFraigaabe =
							MengeOreTypFraigaabe.ContainsNullable(OreTyp.Value);

						MengeOverviewObjektVerwendet.Add(new KeyValuePair<SictOverViewObjektZuusctand, ZuInRaumObjektInfoMine>(
							OverViewObjekt, new ZuInRaumObjektInfoMine(
								OreTypSictString,
								OreTyp,
								OreTypFraigaabe,
								null,
								null,
								null,
								null,
								null,
								null,
								null)));
					}
				}

				OverviewObjektFraigaabeLockedNictNääxte =
					MengeOverviewObjektVerwendet
					.WhereNullable((Kandidaat) =>
						(Kandidaat.Value.OreTypFraigaabe ?? false) &&
						!(Kandidaat.Key.TargetingOderTargeted ?? false))
					.OrderByNullable((Kandidaat) => Kandidaat.Key.DistanceScrankeMinKombi ?? int.MaxValue)
					.FirstOrDefaultNullable();

				if (null != MengeTarget)
				{
					foreach (var Target in MengeTarget)
					{
						if (null == Target)
						{
							continue;
						}

						string TargetOreTypSictString = null;
						OreTypSictEnum? TargetOreTyp = null;
						bool? TargetOreTypFraigaabe = null;

						if (!SictOverviewUndTargetZuusctand.TargetBescriftungIstAsteroid(Target, out TargetOreTypSictString, out TargetOreTyp))
						{
							continue;
						}

						if (!TargetOreTyp.HasValue)
						{
							continue;
						}

						TargetOreTypFraigaabe =
							MengeOreTypFraigaabe.ContainsNullable(TargetOreTyp.Value);

						Int64? TargetErzMengeRestAnzaalScrankeMin = null;
						Int64? TargetErzMengeRestAnzaalScrankeMax = null;

						var ZuTargetMengeMinerAktivitäät = new List<KeyValuePair<ModuleAktivitäätZaitraum, int?>>();

						bool? MinerAktivitäätZyyklusGrenzeNaheSurveyScan = null;

						var ZuTargetSurveyScanInfo = AutomaatZuusctand.ZuTargetSurveyScanInfo(Target);

						if (null != ZuTargetSurveyScanInfo)
						{
							var SurveyScanZait = (null == WindowSurveyScanView) ? null : WindowSurveyScanView.ScnapscusFrühesteZait;

							Int64 ZuTargetMengeMinerAktivitäätAgregatioonEntnaame = 0;

							if (null != ShipMengeModuleMiner)
							{
								foreach (var ModuleMiner in ShipMengeModuleMiner)
								{
									KeyValuePair<int, int>? ModuleMinerVolumeMiliPerTimespanMili = null;

									var ModuleMinerModuleButtonHintGültigMitZait = ModuleMiner.ModuleButtonHintGültigMitZait;

									if (null != ModuleMinerModuleButtonHintGültigMitZait.Wert)
									{
										ModuleMinerVolumeMiliPerTimespanMili = ModuleMinerModuleButtonHintGültigMitZait.Wert.VolumeMiliPerTimespanMili;
									}

									var ModuleMinerListeAktivitäät = ModuleMiner.ListeAktivitäät;

									if (null == ModuleMinerListeAktivitäät)
									{
										continue;
									}

									var ModuleMinerAktivitäät = ModuleMinerListeAktivitäät.LastOrDefaultNullable();

									if (null == ModuleMinerAktivitäät)
									{
										continue;
									}

									{
										if (ModuleMinerAktivitäät.EndeZait < SurveyScanZait)
										{
											continue;
										}

										if (!ModuleMinerAktivitäät.BeginMengeTarget.ContainsNullable(Target))
										{
											continue;
										}

										var ModuleMinerAktivitäätListeNuldurcgangZait =
											new Int64[] { ModuleMinerAktivitäät.BeginZait ?? 0 }.Concat(
											ModuleMinerAktivitäät.RotatioonListeNuldurcgangZait).ToArray();

										var ModuleMinerAktivitäätListeNuldurcgangZaitDistanzZuSurveyScan =
											ModuleMinerAktivitäätListeNuldurcgangZait
											.Select((NuldurcgangZait) => NuldurcgangZait - (SurveyScanZait ?? int.MinValue)).ToArray();

										if (ModuleMinerAktivitäätListeNuldurcgangZaitDistanzZuSurveyScan.Any((Distanz) => Math.Abs(Distanz) < 5555))
										{
											MinerAktivitäätZyyklusGrenzeNaheSurveyScan = true;
										}

										int? ModuleMinerZyyklusFortscritMili = null;

										try
										{
											ModuleMinerZyyklusFortscritMili = ModuleMiner.RotatioonMiliGefiltertRangordnungBerecne(5, 500);

											var ModuleMinerAktivitäätMengeErzVolumeMili =
												((Int64)(ModuleMinerZyyklusFortscritMili ?? 0) * (Int64)ModuleMinerVolumeMiliPerTimespanMili.Value.Key) / ((Int64)1e+3);

											ZuTargetMengeMinerAktivitäätAgregatioonEntnaame +=
												(ModuleMinerAktivitäätMengeErzVolumeMili / AutomaatZuusctand.FürOreTypVolumeMili(TargetOreTyp.Value)) ?? 0;
										}
										finally
										{
											ZuTargetMengeMinerAktivitäät.Add(
												new KeyValuePair<ModuleAktivitäätZaitraum, int?>(ModuleMinerAktivitäät, ModuleMinerZyyklusFortscritMili));
										}
									}
								}
							}

							TargetErzMengeRestAnzaalScrankeMin = ZuTargetSurveyScanInfo.OreQuantityScrankeMin - ZuTargetMengeMinerAktivitäätAgregatioonEntnaame;
							TargetErzMengeRestAnzaalScrankeMax = ZuTargetSurveyScanInfo.OreQuantityScrankeMax - ZuTargetMengeMinerAktivitäätAgregatioonEntnaame;
						}

						var MengeAssignedVolumePerZyyklusAgregatioonScrankeMax =
							(Target.ScnapscusMengeAssignedAnzaal() ?? 0) *
							(ShipMengeModuleMinerZyyklusVolumeMax ?? 0);

						MengeTargetVerwendet.Add(new KeyValuePair<SictTargetZuusctand, ZuInRaumObjektInfoMine>(
							Target, new ZuInRaumObjektInfoMine(
								TargetOreTypSictString,
								TargetOreTyp,
								TargetOreTypFraigaabe,
								ZuTargetSurveyScanInfo,
								ZuTargetMengeMinerAktivitäät.ToArrayFalsNitLeer(),
								TargetErzMengeRestAnzaalScrankeMin,
								TargetErzMengeRestAnzaalScrankeMax,
								MinerAktivitäätZyyklusGrenzeNaheSurveyScan,
								Target.ScnapscusMengeAssignedAnzaal(),
								MengeAssignedVolumePerZyyklusAgregatioonScrankeMax)));
					}
				}

				ListeTargetVerwendetMengeErzRestZuMeseNääxte =
					MengeTargetVerwendet
					.WhereNullable((Kandidaat) =>
						(0 < Kandidaat.Value.MengeAssignedAnzaal) &&
						Kandidaat.Key.InLezteScnapscusSictbar() &&
						Kandidaat.Value.OreTyp.HasValue &&
						(!Kandidaat.Value.ErzMengeRestScrankeMin.HasValue ||
						!(Kandidaat.Value.ErzMengeRestScrankeMin == Kandidaat.Value.ErzMengeRestScrankeMax)) &&
						!(Kandidaat.Value.MengeAssignedVolumePerZyyklusAgregatioonScrankeMax <= Kandidaat.Value.AusSurveyScanOreVolumeScrankeMin))
					.OrderByNullable((Kandidaat) => Kandidaat.Value.ErzMengeRestScrankeMin ?? 0)
					.OrderByNullable((Kandidaat) => Kandidaat.Key.InputFookusTransitioonLezteZiilWert ?? false)
					.FirstOrDefaultNullable();

				var ShipMengeModuleMinerAnzaal = ShipMengeModuleMiner.CountNullable();

				MengeAsteroidTargetAnzaalScranke =
					0 < ShipMengeModuleMinerAnzaal ? ((int)ShipMengeModuleMinerAnzaal.Value + 1) : 0;

				if (0 < ShipMengeModuleMinerAnzaal)
				{
					MengeAsteroidLockedAusraicend =
						MengeAsteroidTargetAnzaalScranke <=
						(MengeTargetVerwendet
						.CountNullable((Kandidaat) =>
							(Kandidaat.Value.OreTypFraigaabe ?? false)) ?? 0) +
						(MengeOverviewObjektVerwendet
						.CountNullable((Kandidaat) =>
							(Kandidaat.Key.InLezteScnapscusSictbar()) && (Kandidaat.Value.OreTypFraigaabe ?? false) &&
							(Kandidaat.Key.Targeting ?? false) && !(Kandidaat.Key.Targeted ?? true)) ?? 0);
				}
				else
				{
					MengeAsteroidLockedAusraicend = true;
				}

				TargetDistanceScrankeMax = Math.Max(5555, ShipMengeModuleMinerRange - 888);

				var MengeTargetIsInRaicwaite =
					MengeTargetVerwendet
					.WhereNullable((Kandidaat) =>
						(Kandidaat.Value.OreTypFraigaabe ?? false) &&
						Kandidaat.Key.InLezteScnapscusSictbar())
					.AllNullable((Kandidaat) =>
						Kandidaat.Key.SictungLezteDistanceScrankeMax() <= TargetDistanceScrankeMax);

				if ((MengeAsteroidLockedAusraicend ?? false) && (MengeTargetIsInRaicwaite ?? false))
				{
					MengeAsteroidInRaicwaiteAnzaalAusraicend = true;
				}

				if (MengeAsteroidTargetAnzaalScranke <=
						(MengeOverviewObjektVerwendet
						.CountNullable((Kandidaat) =>
							Math.Max(Kandidaat.Key.SictungLezteDistanceScrankeMax() ?? int.MaxValue, Kandidaat.Key.DistanceScrankeMinKombi ?? int.MinValue) <= TargetDistanceScrankeMax &&
							(Kandidaat.Key.SaitSictbarkaitLezteListeScritAnzaal < 11) && (Kandidaat.Value.OreTypFraigaabe ?? false)) ?? 0))
				{
					MengeAsteroidInRaicwaiteAnzaalAusraicend = true;
				}

				var MengeTargetVerwendetTailmengeAssigned =
					MengeTargetVerwendet.Where((Kandidaat) => 0 < Kandidaat.Value.MengeAssignedAnzaal).ToArray();

				var MengeTargetVerwendetTailmengeAssignedNict =
					MengeTargetVerwendet.Except(MengeTargetVerwendetTailmengeAssigned).ToArray();

				var MengeTargetVerwendetTailmengeAssignedDistanceScrankeMax =
					MengeTargetVerwendetTailmengeAssigned
					.SelectNullable((TargetAssigned) => TargetAssigned.Key.SictungLezteDistanceScrankeMax() ?? int.MaxValue)
					.DefaultIfEmpty(int.MaxValue)
					.Max();

				var MengeTargetVerwendetTailmengeAssignedNictNääxte =
					MengeTargetVerwendetTailmengeAssignedNict
					.OrderBy((TargetAssignedNict) => TargetAssignedNict.Key.SictungLezteDistanceScrankeMax() ?? int.MaxValue)
					.FirstOrDefault();

				if (!(MengeTargetIsInRaicwaite ?? false) &&
					!ShipMengeModuleMiner.NullOderLeer())
				{
					var AusTailmengeAssignedNictTargetAnzufliigeNääxte = MengeTargetVerwendetTailmengeAssignedNictNääxte.Key;

					if (MengeTargetVerwendetTailmengeAssignedDistanceScrankeMax < TargetDistanceScrankeMax - 3333)
					{
						TargetAnzufliigeNääxte = AusTailmengeAssignedNictTargetAnzufliigeNääxte;
					}
					else
					{
						TargetAnzufliigeNääxte =
							MengeTargetVerwendetTailmengeAssigned
							.OrderByNullable((TargetAssigned) => TargetAssigned.Key.SictungLezteDistanceScrankeMax() ?? int.MaxValue)
							.LastOrDefaultNullable().Key ??
							AusTailmengeAssignedNictTargetAnzufliigeNääxte;
					}
				}

				foreach (var TargetVerwendet in MengeTargetVerwendet)
				{
					var TargetVerwendetListeAssignedTransitioon = TargetVerwendet.Key.ListeAssignedTransitioon();

					var TargetVerwendetListeAssignedTransitioonLezte = TargetVerwendetListeAssignedTransitioon.LastOrDefaultNullable();

					if (!(0 < TargetVerwendetListeAssignedTransitioonLezte.ZuusazInfo.ZiilWertMengeInGrupeZuunaame.CountNullable()))
					{
						continue;
					}

					TargetAssignmentMeerereKarenzDauerRest =
						TargetVerwendetListeAssignedTransitioonLezte.ScritIdent +
						TargetAssignmentMeerereKarenzDauer -
						NuzerZaitMili;
				}

				var OreHoldCapacityMesungLezteMitZait = FittingUndShipZuusctand.OreHoldCapacityMesungLezteMitZait;

				var OreHoldCapacityMesungLezteAlterMili = NuzerZaitMili - OreHoldCapacityMesungLezteMitZait.Zait;

				if (null != ShipZuusctand)
				{
					if (true == ShipZuusctand.Docked &&
						null != CurrentLocationInfo)
					{
						if (null != CurrentLocationInfo.NearestName)
						{
							StationZiil = CurrentLocationInfo.NearestName;
						}
					}
				}

				if (!(MengeAsteroidLockedAusraicend ?? false))
				{
					if (OverViewScrolledToTopLezteAlter < 15555)
					{
						var MengeAsteroidKandidaatLock =
							MengeOverviewObjektVerwendet
							.WhereNullable((Kandidaat) =>
								(Kandidaat.Value.OreTypFraigaabe ?? false) &&
								!((Kandidaat.Key.SictungLezteZait ?? 0) < CharZuusctandWarpingLezteZaitMili) &&
								Kandidaat.Key.SaitSictbarkaitLezteListeScritAnzaal < 13);

						AsteroidZuLockeNääxte =
							MengeAsteroidKandidaatLock
							.OrderByNullable((Kandidaat) =>
								(0 < Kandidaat.Key.SaitSictbarkaitLezteListeScritAnzaal &&
								Kandidaat.Key.SaitSictbarkaitLezteListeScritAnzaal < 3))
							.ThenByDescendingNullable((Kandidaat) => MengeMiningCrystalTypGelaade.Contains(Kandidaat.Value.OreTyp.Value))
							.ThenByNullable((Kandidaat) => Kandidaat.Key.DistanceScrankeMaxKombi ?? int.MaxValue)
							.FirstOrDefaultNullable(
							(Kandidaat) => !(true == Kandidaat.Key.TargetingOderTargeted)).Key;
					}
					else
					{
						InOverviewSuuceAsteroid = true;
					}

					if (null == AsteroidZuLockeNääxte)
					{
						if (!(OverviewDefaultMiningFolgeViewportFertigLezteHinraicendAktuel ?? false))
						{
							InOverviewSuuceAsteroid = true;
						}
					}
				}

				var AsteroidZuLockeNääxteDistance =
					(null == AsteroidZuLockeNääxte) ? null : AsteroidZuLockeNääxte.DistanceScrankeMaxKombi;

				if (!(MengeAsteroidLockedAusraicend ?? false) ||
					!(CurrentLocationIstBelt ?? true))
				{
					if (!(DockUndOffloadPrioVorMine ?? false))
					{
						if (Bib3.Extension.NullOderLeer(MengeTargetVerwendet) &&
							!(AsteroidZuLockeNääxteDistance < 7e+4) &&
							!(InOverviewSuuceAsteroid ?? false))
						{
							WarpToBeltPrioVorMine = true;
						}
					}
				}

				SurveyScanBeginZaitScrankeMin = ShipMengeModuleMinerNuldurcgangLezteZait + 10000;

				if (ShipMengeModuleMinerNuldurcgangNääxteZait < NuzerZaitMili + 11111)
				{
					SurveyScanBeginZaitScrankeMin = Bib3.Glob.Max(SurveyScanBeginZaitScrankeMin, ShipMengeModuleMinerNuldurcgangNääxteZait);
				}

				if (null != ListeLocationNearest && null != SolarSystem)
				{
					var Random = new Random((int)Bib3.Glob.StopwatchZaitMiliSictInt());

					ListePrioAsteroidBeltBescriftung =
						SolarSystem.MengeAsteroidBelt
						.OrderByNullable((AsteroidBelt) => AutomaatZuusctand.LocationNearestLezteZait(AsteroidBelt.Bescriftung) ?? int.MinValue)

						//	Zuusaz Random fals ale LocationNearestLezteZait glaic.
						.ThenBy(t => Random.Next())
						.Select((AsteroidBelt) => AsteroidBelt.Bescriftung)
						.ToArrayNullable();
				}

				var ShipMengeModuleMinerMenuLezteAlter = NuzerZaitMili - ShipMengeModuleMinerKandidaatCrystalMenuLezteZait;

				if (!(ShipMengeModuleMinerMenuLezteAlter < 1000 * 60 * 15))
				{
					MesungMiningCrystalVerfüügbarModule = ShipMengeModuleMinerKandidaatMiningCrystal.FirstOrDefaultNullable();
				}
			}
			finally
			{
				this.AnnaameNaacAbbrucMinerZyyklusOreHoldGefült = AnnaameNaacAbbrucMinerZyyklusOreHoldGefült;
				this.OverviewDefaultMiningFolgeViewportFertigLezteHinraicendAktuel = OverviewDefaultMiningFolgeViewportFertigLezteHinraicendAktuel;
				this.CurrentLocationIstBelt = CurrentLocationIstBelt;
				this.DockUndOffloadPrioVorMine = DockUndOffloadPrioVorMine;
				this.WarpToBeltPrioVorMine = WarpToBeltPrioVorMine;
				this.OverviewTabBevorzuugtTitel = OverviewTabBevorzuugtTitel;
				this.MengeTargetVerwendet = MengeTargetVerwendet.ToArrayFalsNitLeer();
				this.MengeOverviewObjektVerwendet = MengeOverviewObjektVerwendet.ToArrayFalsNitLeer();
				this.AsteroidZuLockeNääxte = AsteroidZuLockeNääxte;
				this.InOverviewSuuceAsteroid = InOverviewSuuceAsteroid;
				this.ListeTargetVerwendetMengeErzRestZuMeseNääxte = ListeTargetVerwendetMengeErzRestZuMeseNääxte;
				this.MengeMiningCrystalTypGelaade = MengeMiningCrystalTypGelaade.Distinct().ToArray();
				this.MengeMiningCrystalTypVerfüügbar = MengeMiningCrystalTypVerfüügbar.Distinct().ToArray();
				this.ShipMengeModuleMinerZyyklusVolumeMax = ShipMengeModuleMinerZyyklusVolumeMax;
				this.ShipMengeModuleMinerKandidaatCrystalMenuLezteZait = ShipMengeModuleMinerKandidaatCrystalMenuLezteZait;
				this.MesungMiningCrystalVerfüügbarModule = MesungMiningCrystalVerfüügbarModule;
				this.ShipMengeModuleMinerRange = ShipMengeModuleMinerRange;
				this.SaitSurveyScanLezteShipStreke = SaitSurveyScanLezteShipStreke;
				this.ShipMengeModuleMinerNuldurcgangLezteZait = ShipMengeModuleMinerNuldurcgangLezteZait;
				this.SurveyScanBeginZaitScrankeMin = SurveyScanBeginZaitScrankeMin;
				this.MengeOreTypFraigaabe = MengeOreTypFraigaabe;
				this.MengeAsteroidTargetAnzaalScranke = MengeAsteroidTargetAnzaalScranke;
				this.TargetDistanceScrankeMax = TargetDistanceScrankeMax;
				this.MengeAsteroidLockedAusraicend = MengeAsteroidLockedAusraicend;
				this.MengeAsteroidInRaicwaiteAnzaalAusraicend = MengeAsteroidInRaicwaiteAnzaalAusraicend;
				this.TargetAnzufliigeNääxte = TargetAnzufliigeNääxte;
				this.TargetAssignmentKarenzDauerRest = TargetAssignmentKarenzDauerRest;
				this.TargetAssignmentMeerereKarenzDauerRest = TargetAssignmentMeerereKarenzDauerRest;
				this.ShipMengeModuleMinerNuldurcgangNääxteZait = ShipMengeModuleMinerNuldurcgangNääxteZait;
				this.OverviewObjektFraigaabeLockedNictNääxte = OverviewObjektFraigaabeLockedNictNääxte;
				this.ListePrioAsteroidBeltBescriftung = ListePrioAsteroidBeltBescriftung;
			}
		}
	}
}
