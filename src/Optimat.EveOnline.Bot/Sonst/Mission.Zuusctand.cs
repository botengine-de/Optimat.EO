using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Optimat.ScpezEveOnln;
using Bib3;
using Optimat.EveOnline.Base;
//using Optimat.EveOnline.AuswertGbs;
using Optimat.EveOnline.Anwendung.AuswertGbs;
using Optimat.EveOnline.VonSensor;
using ExtractFromOldAssembly.Bib3;

namespace Optimat.EveOnline.Anwendung
{
	/// <summary>
	/// Hiirin werde Info zu Location aus Mission Aggregiirt.
	/// Info kan aus versciidene GBS Objekt kome, z.B. AgentDialogue oder Utilmenu zu Mission
	/// </summary>
	[JsonObject(MemberSerialization.OptIn)]
	public class SictMissionLocationInfoAggr
	{
		[JsonProperty]
		public bool? IstDeadspace
		{
			private set;
			get;
		}

		[JsonProperty]
		public string LocationNameTailSystem
		{
			private set;
			get;
		}

		[JsonProperty]
		public string InUtilmenuBescriftung
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictGbsMenuZuusctand SurroundingsMenuLezte
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictWertMitZait<VonSensor.UtilmenuMissionLocationInfo> UtilMenuLocationInfoLezteZuZait
		{
			private set;
			get;
		}

		public SictWertMitZait<VonSensor.Menu> SurroundingsMenuLezteScnapscusZuZaitBerecne()
		{
			return SictGbsMenuZuusctand.AingangScnapscusTailObjektIdentMitZaitLezteBerecne(SurroundingsMenuLezte) ?? default(SictWertMitZait<VonSensor.Menu>);
		}

		public void AingangUtilMenu(
			Int64 Zait,
			VonSensor.UtilmenuMissionLocationInfo UtilMenuLocationInfo)
		{
			if (null == UtilMenuLocationInfo)
			{
				return;
			}

			bool? IstDeadspace = null;
			string LocationNameTailSystem = null;
			string InUtilmenuBescriftung = null;

			try
			{
				InUtilmenuBescriftung = UtilMenuLocationInfo.HeaderText;

				var KnopfLocation = UtilMenuLocationInfo.KnopfLocation;

				var LocationName = (null == KnopfLocation) ? null : KnopfLocation.Bescriftung;

				if (null != InUtilmenuBescriftung)
				{
					var DeadspaceMatch = Regex.Match(InUtilmenuBescriftung, "deadspace", RegexOptions.IgnoreCase);

					IstDeadspace = DeadspaceMatch.Success;
				}

				if (null != LocationName)
				{
					var Location = TempAuswertGbs.Extension.VonStringMissionLocationNameExtraktLocationInfo(LocationName);

					if (null != Location)
					{
						LocationNameTailSystem = Location.SolarSystemName;
					}
				}
			}
			finally
			{
				this.UtilMenuLocationInfoLezteZuZait = new SictWertMitZait<VonSensor.UtilmenuMissionLocationInfo>(Zait, UtilMenuLocationInfo);
				this.InUtilmenuBescriftung = InUtilmenuBescriftung;
				this.IstDeadspace = IstDeadspace;
				this.LocationNameTailSystem = LocationNameTailSystem;
			}
		}

		public void AingangSurroundingsMenu(SictGbsMenuZuusctand Menu)
		{
			if (null == Menu)
			{
				return;
			}

			this.SurroundingsMenuLezte = Menu;
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictVonMissionAutomatAnforderungNaacAutomat
	{
		/// <summary>
		/// Menge der Window welce nit vum Automaate entfernt were soln.
		/// Value zaigt an das Window nit minimiirt were sol.
		/// </summary>
		[JsonProperty]
		public KeyValuePair<VonSensor.Window, bool>[] AnforderungGbsMengeWindowZuErhalte;

		[JsonProperty]
		public bool? AnforderungWarpNaacGateOderStation;

		[JsonProperty]
		public bool? AnforderungGbsUtilMenuFürMission;

		[JsonProperty]
		public bool? AnforderungRouteFüüreAus;

		public SictVonMissionAutomatAnforderungNaacAutomat()
		{
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictVonMissionAnforderungInRaum
	{
		[JsonProperty]
		public SictOverviewObjektGrupeEnum[] MengeOverviewObjektGrupeVerwendet
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictOverviewObjektGrupeEnum[] MengeOverviewObjektGrupeMesungZuErsctele
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictAufgaabeInRaumObjektZuBearbaiteMitPrio[] MengeObjektZuBearbaiteMitPrio
		{
			private set;
			get;
		}

		[JsonProperty]
		public VonSensor.InventoryItem AusInventoryItemZuÜbertraageNaacActiveShip
		{
			private set;
			get;
		}

		public SictVonMissionAnforderungInRaum()
		{
		}

		public SictVonMissionAnforderungInRaum(
			SictOverviewObjektGrupeEnum[] MengeOverviewObjektGrupeVerwendet,
			SictOverviewObjektGrupeEnum[] MengeOverviewObjektGrupeMesungZuErsctele = null,
			SictAufgaabeInRaumObjektZuBearbaiteMitPrio[] MengeObjektZuBearbaiteMitPrio = null,
			VonSensor.InventoryItem AusInventoryItemZuÜbertraageNaacActiveShip = null)
		{
			this.MengeOverviewObjektGrupeVerwendet = MengeOverviewObjektGrupeVerwendet;
			this.MengeOverviewObjektGrupeMesungZuErsctele = MengeOverviewObjektGrupeMesungZuErsctele;
			this.MengeObjektZuBearbaiteMitPrio = MengeObjektZuBearbaiteMitPrio;
			this.AusInventoryItemZuÜbertraageNaacActiveShip = AusInventoryItemZuÜbertraageNaacActiveShip;
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictMissionRaumZuusctandScnapscus
	{
		[JsonProperty]
		public VonSensor.OverviewZaile[] AnnaameMengeObjekt;

		[JsonProperty]
		public VonSensor.OverviewZaile	AnnaameMengeObjektAccelerationGateNääxte;

		public SictMissionRaumZuusctandScnapscus()
		{
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictMissionRaumZuusctandEndeSictServer : Optimat.EveOnline.SictMissionLocationRaumEnde
	{
		[JsonProperty]
		readonly public KeyValuePair<SictAufgaabeZuusctand, Int64> AufgaabeAccGateActivateLezteMitScnapscusAccGateDistance;

		public SictMissionRaumZuusctandEndeSictServer()
		{
		}

		public SictMissionRaumZuusctandEndeSictServer(
			KeyValuePair<SictAufgaabeZuusctand, Int64> AufgaabeAccGateActivateLezteMitScnapscusAccGateDistance)
		{
			this.AufgaabeAccGateActivateLezteMitScnapscusAccGateDistance = AufgaabeAccGateActivateLezteMitScnapscusAccGateDistance;
		}

		public bool AnnaameFortsazInMissionRaumNääxteErfolg(Int64 AccGateDistanceScrankeMax)
		{
			var AufgaabeAccGateActivateLezteMitScnapscusAccGateDistance = this.AufgaabeAccGateActivateLezteMitScnapscusAccGateDistance;

			if (null == AufgaabeAccGateActivateLezteMitScnapscusAccGateDistance.Key)
			{
				return false;
			}

			return AufgaabeAccGateActivateLezteMitScnapscusAccGateDistance.Value < AccGateDistanceScrankeMax;
		}

		static public SictMissionRaumZuusctandEndeSictServer Berecne(
			SictAutomatZuusctand Automaat)
		{
			var AufgaabeAccGateActivateLezteMitScnapscusAccGateDistance = default(KeyValuePair<SictAufgaabeZuusctand, Int64>);

			var MengeAufgaabeZuusctand = (null == Automaat) ? null : Automaat.MengeAufgaabeZuusctand;

			var MengeAufgaabeAccGateActivateErfolg =
				ExtractFromOldAssembly.Bib3.Extension.WhereNullable(
				MengeAufgaabeZuusctand,
				(KandidaatAufgaabe) =>
				{
					if (null == KandidaatAufgaabe)
					{
						return false;
					}

					if (false)
					{
						//	2014.07.21	ErfolgZait werd für diise Aufgaabe noc nit berecnet. Daher werd ersazwaise AbsclusTailWirkungZait geprüüft.
						if (!KandidaatAufgaabe.ErfolgZait.HasValue)
						{
							return false;
						}
					}
					else
					{
						if (!KandidaatAufgaabe.AbsclusTailWirkungZait.HasValue)
						{
							return false;
						}
					}

					var KandidaatAufgaabeParam = KandidaatAufgaabe.AufgaabeParam as AufgaabeParamAndere;

					if (null == KandidaatAufgaabeParam)
					{
						return false;
					}

					if (!(true == KandidaatAufgaabeParam.AktioonInRaumObjektActivate))
					{
						return false;
					}

					var KandidaatAufgaabeParamOverViewObjektZuBearbaite = KandidaatAufgaabeParam.OverViewObjektZuBearbaiteVirt();

					if (null == KandidaatAufgaabeParamOverViewObjektZuBearbaite)
					{
						return false;
					}

					return SictMissionZuusctand.PrädikaatIstAccGate(KandidaatAufgaabeParamOverViewObjektZuBearbaite);
				})
				.ToArrayNullable();

			var AufgaabeAccGateActivateErfolg =
				(null == MengeAufgaabeAccGateActivateErfolg) ? null :
				MengeAufgaabeAccGateActivateErfolg
				.OrderByDescending((Aufgaabe) => Aufgaabe.AbsclusTailWirkungZait)
				.FirstOrDefault();

			if (null != AufgaabeAccGateActivateErfolg)
			{
				var AufgaabeAccGateActivateErfolgParam = AufgaabeAccGateActivateErfolg.AufgaabeParam;

				if (null != AufgaabeAccGateActivateErfolgParam)
				{
					var AccGate = AufgaabeAccGateActivateErfolgParam.OverViewObjektZuBearbaiteVirt();

					if (null != AccGate)
					{
						var AccGateDistance = AccGate.SictungLezteDistanceScrankeMaxScpezOverview;

						if (AccGateDistance.HasValue)
						{
							AufgaabeAccGateActivateLezteMitScnapscusAccGateDistance =
								new KeyValuePair<SictAufgaabeZuusctand, Int64>(AufgaabeAccGateActivateErfolg, AccGateDistance.Value);
						}
					}
				}
			}

			return new SictMissionRaumZuusctandEndeSictServer(
				AufgaabeAccGateActivateLezteMitScnapscusAccGateDistance);
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public partial class SictMissionLocationRaumZuusctand : Optimat.EveOnline.SictMissionLocationRaum
	{
		[JsonProperty]
		readonly public List<SictMissionStrategikonInstanz> ListeStrategikonInstanz = new List<SictMissionStrategikonInstanz>();

		[JsonProperty]
		public Int64? MeldungAccGateLockedLezteZait
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictWertMitZait<SictMissionRaumZuusctandEndeSictServer>? KandidaatEnde
		{
			private set;
			get;
		}

		public void MeldungAccGateLockedAingang(Int64? MeldungZait)
		{
			MeldungAccGateLockedLezteZait = Bib3.Glob.Max(MeldungAccGateLockedLezteZait, MeldungZait);
		}

		public SictMissionStrategikonInstanz StrategikonInstanzLezte()
		{
			return ListeStrategikonInstanz.LastOrDefaultNullable();
		}

		public bool? ErfolgBerecne()
		{
			var StrategikonInstanzLezte = this.StrategikonInstanzLezte();

			if (null == StrategikonInstanzLezte)
			{
				return null;
			}

			return StrategikonInstanzLezte.ErfolgBerecne();
		}

		public void AktualisiireTailStrategikonInstanz(Int64 Zait)
		{
			var StrategikonInstanzLezte = this.ListeStrategikonInstanz.LastOrDefaultNullable();

			var StrategikonInstanzLezteErhalte = false;

			if (null != StrategikonInstanzLezte)
			{
				var StrategikonInstanzLezteErfolgFrüühesteAlter = Zait - StrategikonInstanzLezte.ErfolgFrühesteZaitBerecne();

				if (!(40 * 1000 < StrategikonInstanzLezteErfolgFrüühesteAlter))
				{
					StrategikonInstanzLezteErhalte = true;
				}
			}

			if(StrategikonInstanzLezteErhalte)
			{
				return;
			}

			ListeStrategikonInstanz.Add(new SictMissionStrategikonInstanz(Zait));
			ListeStrategikonInstanz.ListeKürzeBegin(4);
		}

		public void KandidaatEndeSeze(SictWertMitZait<SictMissionRaumZuusctandEndeSictServer>? KandidaatEnde)
		{
			this.KandidaatEnde = KandidaatEnde;
		}

		public void Aktualisiire(
			Int64 Zait,
			SictVerlaufBeginUndEndeRef<ShipUiIndicationAuswert> ShipUiIndication,
			IEnumerable<SictOverViewObjektZuusctand> MengeOverviewObjekt)
		{
			if (this.Ende.HasValue)
			{
				return;
			}

			var KandidaatEnde = this.KandidaatEnde;

			if (KandidaatEnde.HasValue)
			{
				var MengeOverviewObjektInGrid =
					ExtractFromOldAssembly.Bib3.Extension.WhereNullable(
					MengeOverviewObjekt,
					(KandidaatObjekt) => KandidaatObjekt.SictungLezteDistanceScrankeMinScpezOverview < 1000 * 1000);

				if (MengeOverviewObjektInGrid.IsNullOrEmpty())
				{
					base.EndeSeze(new SictWertMitZait<Optimat.EveOnline.SictMissionLocationRaumEnde>(Zait, KandidaatEnde.Value.Wert));
				}
			}
		}

		public SictMissionLocationRaumZuusctand()
			:
			this(null,	null)
		{
		}

		public SictMissionLocationRaumZuusctand(
			Int64? BeginZait,
			SictAusGbsLocationInfo	BeginLocation)
			:
			base(BeginZait, BeginLocation)
		{
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictMissionLocationPfaadZuusctand : SictMissionLocationPfaad
	{
		[JsonProperty]
		public MissionLocation Location
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictWertMitZait<VonSensor.WindowAgentMissionObjectiveObjective>? LocationObjectiveMesungLezte
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? EndeZaitMili;

		[JsonProperty]
		public Int64? AnnaameErfolgZaitMili;

		public SictMissionLocationPfaadZuusctand()
			:
			this(null, null)
		{
		}

		public SictMissionLocationPfaadZuusctand(
			VonSensor.WindowAgentMissionObjectiveObjective LocationObjective,
			SictMissionLocationRaumZuusctand BeginRaum)
			:
			this(
			(null == BeginRaum) ? null : BeginRaum.BeginZait,
			LocationObjective,
			BeginRaum)
		{
		}

		public SictMissionLocationPfaadZuusctand(
			Int64? BeginZaitMili,
			VonSensor.WindowAgentMissionObjectiveObjective LocationObjective,
			SictMissionLocationRaumZuusctand BeginRaum)
			:
			base(BeginZaitMili)
		{
			var Location = (null == LocationObjective) ? null : LocationObjective.Location;

			this.Location = Location;

			this.ListeRaum = new List<SictMissionLocationRaum>();

			if (null != BeginRaum)
			{
				ListeRaum.Add(BeginRaum);
			}

			if (BeginZaitMili.HasValue)
			{
				MesungObjectiveAingang(new SictWertMitZait<VonSensor.WindowAgentMissionObjectiveObjective>(BeginZaitMili.Value, LocationObjective));
			}
		}

		public void MesungObjectiveAingang(SictWertMitZait<VonSensor.WindowAgentMissionObjectiveObjective> MesungObjectiveMitZait)
		{
			if (null == MesungObjectiveMitZait.Wert)
			{
				return;
			}

			var Location = this.Location;

			if (null == Location)
			{
				return;
			}

			var LocationName = Location.LocationName;

			if (null == LocationName)
			{
				return;
			}

			var LocationMesungObjektiveLezte =
				MesungObjectiveMitZait.Wert.MengeObjectiveTransitiveHüleBerecne()
				.OfTypeNullable<VonSensor.WindowAgentMissionObjectiveObjective>()
				.FirstOrDefaultNullable((KandidaatObjective) =>
					{
						var KandidaatObjectiveLocation = KandidaatObjective.Location;

						if (null == KandidaatObjectiveLocation)
						{
							return false;
						}

						return string.Equals(KandidaatObjectiveLocation.LocationName, LocationName);
					});

			if (null != LocationMesungObjektiveLezte)
			{
				this.LocationObjectiveMesungLezte = new SictWertMitZait<VonSensor.WindowAgentMissionObjectiveObjective>(
					MesungObjectiveMitZait.Zait, LocationMesungObjektiveLezte);
			}
		}
	}

	public partial class SictMissionZuusctand
	{
		public Optimat.EveOnline.SictMissionZuusctand TailFürNuzer
		{
			private set;
			get;
		}

		/*
		2015.03.21

			Ersaz durc Extension

		public Int64? Ident
		{
			get
			{
				var TailFürNuzer = this.TailFürNuzer;

				if (null == TailFürNuzer)
				{
					return null;
				}

				return TailFürNuzer.Ident;
			}
		}

		public Int64? SictungFrühesteZaitMili
		{
			get
			{
				var TailFürNuzer = this.TailFürNuzer;

				if (null == TailFürNuzer)
				{
					return null;
				}

				return TailFürNuzer.SictungFrühesteZaitMili;
			}
		}

		public Int64? EndeZaitMili
		{
			get
			{
				var TailFürNuzer = this.TailFürNuzer;

				if (null == TailFürNuzer)
				{
					return null;
				}

				return TailFürNuzer.EndeZaitMili;
			}
		}

		public MissionLocation AgentLocation
		{
			get
			{
				var TailFürNuzer = this.TailFürNuzer;

				if (null == TailFürNuzer)
				{
					return null;
				}

				return TailFürNuzer.AgentLocation;
			}
		}

		public string Titel
		{
			get
			{
				var TailFürNuzer = this.TailFürNuzer;

				if (null == TailFürNuzer)
				{
					return null;
				}

				return TailFürNuzer.Titel;
			}
		}
		public string FürMissionFittingBezaicner
		{
			get
			{
				var TailFürNuzer = this.TailFürNuzer;

				if (null == TailFürNuzer)
				{
					return null;
				}

				return TailFürNuzer.FürMissionFittingBezaicner;
			}
		}

		public bool? ConstraintFittingSatisfied
		{
			get
			{
				var TailFürNuzer = this.TailFürNuzer;

				if (null == TailFürNuzer)
				{
					return null;
				}

				return TailFürNuzer.ConstraintFittingSatisfied;
			}
		}
		public IEnumerable<SictMissionLocationPfaadZuusctand> ListePfaad
		{
			get
			{
				var TailFürNuzer = this.TailFürNuzer;

				if (null == TailFürNuzer)
				{
					return null;
				}

				return Bib3.Extension.SelectNullable(
					TailFürNuzer.ListeLocationPfaad,
					(LocationPfaad) => LocationPfaad as SictMissionLocationPfaadZuusctand);
			}
		}

		*/

		public List<SictMissionLocationInfoAggr> MengeLocation
		{
			private set;
			get;
		}

		public Int64? ZaitMili
		{
			private set;
			get;
		}

		public void ConstraintFittingBerecne(
			SictOptimatParam OptimatParam,
			SictAgentUndMissionZuusctand AgentUndMissionZuusctand)
		{
			string FittingBezaicner = null;
			bool? ConstraintFittingSatisfied = null;
			KeyValuePair<string, string>[] MengeZuFactionFittingBezaicner = null;

			var TailFürNuzer = this.TailFürNuzer;

			if (null == TailFürNuzer)
			{
				return;
			}

			try
			{
				if (TailFürNuzer.EndeZaitMili.HasValue)
				{
					return;
				}

				try
				{
					var Strategikon = this.Strategikon;

					if (null == Strategikon)
					{
						return;
					}

					/*
					 * 2015.02.13
					 * 
					 * Änderung: Ermitlung Faction aus Mission.
					 * 
					FittingBezaicner = Strategikon.FürMissionFittingBezaicnerBerecne(OptimatParam, out	MengeZuFactionFittingBezaicner);
					 * */
					FittingBezaicner = Strategikon.FürMissionFittingBezaicnerBerecne(OptimatParam, TailFürNuzer.ObjectiveMengeFaction, out	MengeZuFactionFittingBezaicner);

					if (FittingBezaicner.IsNullOrEmpty())
					{
						//	Wen kain ainziges Fitting konfiguriirt ist dan darf Mission oone Fitting geflooge werde.
						ConstraintFittingSatisfied = MengeZuFactionFittingBezaicner.IsNullOrEmpty();
						return;
					}

					var FittingFitLoadedLezteNocAktiivMitZait = (null == AgentUndMissionZuusctand) ? null : AgentUndMissionZuusctand.FittingFitLoadedLezteNocAktiivMitZait;

					if (!FittingFitLoadedLezteNocAktiivMitZait.HasValue)
					{
						ConstraintFittingSatisfied = false;
						return;
					}

					ConstraintFittingSatisfied =
						string.Equals(
						ExtractFromOldAssembly.Bib3.Glob.TrimNullable(FittingFitLoadedLezteNocAktiivMitZait.Value.Wert),
						ExtractFromOldAssembly.Bib3.Glob.TrimNullable(FittingBezaicner),
						StringComparison.InvariantCultureIgnoreCase);
				}
				finally
				{
					TailFürNuzer.FürMissionFittingBezaicner = FittingBezaicner;
				}
			}
			finally
			{
				TailFürNuzer.ConstraintFittingSatisfied = ConstraintFittingSatisfied;
			}
		}


		/// <summary>
		/// Index der Repräsentatioon der Mission im InfoPanel
		/// </summary>
		public int? AnnaameInInfoPanelButtonIndex;

		/// <summary>
		/// Begin des lezten noc andauernden Zaitraums in deem die Mission nict im Info Panel abgebildet ist.
		/// Diiser Zwiscenscpaicer ist nootwendig da das InfoPanel anscainend mancmaal auc in unfertigem Zuusctand abgebildet werd.
		/// </summary>
		public Int64? ZaitraumNictInInfoPanelSictbarBeginMili;

		public Int64? AnnaameErfolgZaitMili
		{
			private set;
			get;
		}

		public bool? AnnaameCompleteFallsInAgentStation
		{
			private set;
			get;
		}

		public List<SictWertMitZait<SictAusWindowScnapscusInfo<VonSensor.WindowAgentDialogue>>> ListeMessungObjectiveWindowAgentDialogueScnapscusZuZait
		{
			private set;
			get;
		}

		public SictWertMitZait<VonSensor.WindowAgentDialogue>? ScnapscusWindowAgentDialogueZuZaitLezte
		{
			private set;
			get;
		}

		public SictMissionLocationPfaadZuusctand PfaadAktuel
		{
			private set;
			get;
		}

		public SictMissionLocationRaumZuusctand RaumAktuel
		{
			private set;
			get;
		}

		public SictMissionStrategikon Strategikon
		{
			private set;
			get;
		}

		public VonSensor.InfoPanelMissionsMission ButtonUtilmenu
		{
			private set;
			get;
		}

		public SictWertMitZait<VonSensor.UtilmenuMissionInfo>? UtilmenuZuZait
		{
			private set;
			get;
		}

		public SictObjectiveLocationAuswert ZiilLocationNääxteAuswert
		{
			private set;
			get;
		}

		/// <summary>
		/// Im Utilmenu zu Mission der Aintraag welcer ZiilLocationNääxte repräsentiirt.
		/// </summary>
		public VonSensor.UtilmenuMissionLocationInfo ZiilLocationNääxteInUtilmenu
		{
			private set;
			get;
		}

		/// <summary>
		/// Der lezte Zaitpunkt naac deem aine Mesung des Mission Objective ersctelt were sol.
		/// </summary>
		public Int64? AusPfaadMessungMissionObjectiveSolNaacZaitMili
		{
			private set;
			get;
		}

		public SictInRaumObjektBearbaitungPrio? MessungMissionObjectivePrio
		{
			private set;
			get;
		}

		public SictVonMissionAnforderungInRaum NaacAutomaatAnforderungInRaum
		{
			private set;
			get;
		}

		public SictMissionZuusctand()
		{
		}

		public SictMissionZuusctand Kopii()
		{
			return SictRefNezKopii.ObjektKopiiErsctele(this, null, null);
		}

		public SictMissionZuusctand(
			EveOnline.SictMissionZuusctand TailFürNuzer,
			SictGbsWindowZuusctand WindowAgentDialogue,
			SictMissionStrategikon Strategikon = null)
		{
			this.TailFürNuzer = TailFürNuzer;
			this.Strategikon = Strategikon;

			AingangMissionInfo(WindowAgentDialogue);
		}

		public void AingangMissionInfo(
			SictGbsWindowZuusctand WindowAgentDialogue)
		{
			if (null == WindowAgentDialogue)
			{
				return;
			}

			var WindowAgentDialogueScnapscusMitZaitLezteNulbar = WindowAgentDialogue.AingangScnapscusTailObjektIdentMitZaitLezteBerecne();
			var WindowAgentDialogueScnapscusMitZaitVorLezteNulbar = WindowAgentDialogue.AingangScnapscusTailObjektIdentMitZaitVorLezteBerecne();

			if (!WindowAgentDialogueScnapscusMitZaitLezteNulbar.HasValue)
			{
				return;
			}

			var WindowAgentDialogueScnapscusLezte = WindowAgentDialogueScnapscusMitZaitLezteNulbar.Value.Wert as VonSensor.WindowAgentDialogue;

			var ListeMessungObjectiveWindowAgentDialogueScnapscusZuZait = this.ListeMessungObjectiveWindowAgentDialogueScnapscusZuZait;

			if (null == ListeMessungObjectiveWindowAgentDialogueScnapscusZuZait)
			{
				this.ListeMessungObjectiveWindowAgentDialogueScnapscusZuZait = ListeMessungObjectiveWindowAgentDialogueScnapscusZuZait =
					new List<SictWertMitZait<SictAusWindowScnapscusInfo<VonSensor.WindowAgentDialogue>>>();
			}

			var ListeMessungObjectiveWindowAgentDialogueScnapscusZuZaitLezte = ListeMessungObjectiveWindowAgentDialogueScnapscusZuZait.LastOrDefault();

			var ListeMessungObjectiveWindowAgentDialogueScnapscusLezte = ListeMessungObjectiveWindowAgentDialogueScnapscusZuZaitLezte.Wert;

			if (null != ListeMessungObjectiveWindowAgentDialogueScnapscusLezte)
			{
				if (!WindowAgentPastZuMission(this, WindowAgentDialogueScnapscusLezte, true, true, true, true))
				{
					return;
				}
			}

			var WindowAgentDialogueScnapscusLezteZuZait = new SictWertMitZait<VonSensor.WindowAgentDialogue>(
				WindowAgentDialogueScnapscusMitZaitLezteNulbar.Value.Zait,
				WindowAgentDialogueScnapscusLezte);

			this.ScnapscusWindowAgentDialogueZuZaitLezte = WindowAgentDialogueScnapscusLezteZuZait;

			if (!WindowAgentDialogueScnapscusMitZaitVorLezteNulbar.HasValue)
			{
				return;
			}

			var WindowAgentDialogueScnapscusVorLezte = WindowAgentDialogueScnapscusMitZaitVorLezteNulbar.Value.Wert as VonSensor.WindowAgentDialogue;

			if (!SictWindowAgentDialogueZuusctand.HinraicendGlaicwertigFürÜbernaameMissionInfo(WindowAgentDialogueScnapscusLezte, WindowAgentDialogueScnapscusVorLezte))
			{
				return;
			}

			var MissionInfo = (null == WindowAgentDialogue) ? null : WindowAgentDialogueScnapscusLezte.ZusamefasungMissionInfo;

			if (null == MissionInfo)
			{
				return;
			}

			/*
			 * 2014.04.19
			 * 
			var ListeScnapscusWindowAgentDialogueZuZait = this.ListeScnapscusWindowAgentDialogueZuZait;

			if (0 < Optimat.Glob.CountNullable(ListeScnapscusWindowAgentDialogueZuZait))
			{
				if (!WindowAgentPastZuMission(this, WindowAgentDialogueScnapscusLezte, true, true, true, true))
				{
					return;
				}
			}
			 * */

			if (true == WindowAgentDialogueScnapscusLezte.IstOffer)
			{
				if (!TailFürNuzer.OfferFrühesteZaitMili.HasValue)
				{
					TailFürNuzer.OfferFrühesteZaitMili = WindowAgentDialogueScnapscusMitZaitLezteNulbar.Value.Zait;
				}
			}

			if (true == WindowAgentDialogueScnapscusLezte.IstAccepted)
			{
				if (!TailFürNuzer.AcceptedFrühesteZaitMili.HasValue)
				{
					TailFürNuzer.AcceptedFrühesteZaitMili = WindowAgentDialogueScnapscusMitZaitLezteNulbar.Value.Zait;
				}
			}

			/*
			 * 2014.04.19
			 * 
			var WindowAgentDialogueSictVerglaic =
				new SictWindowAgentDialogueSictFürEntscaidungHinraicendGlaicwertigFürÜbernaameMissionInfo(WindowAgentDialogueScnapscusLezte);
			 * */

			var AnnaameWindowInhaltNoi = true;

			if (null != ListeMessungObjectiveWindowAgentDialogueScnapscusLezte)
			{
				var ListeScnapscusWindowAgentDialogueLezteAlterMili =
					WindowAgentDialogueScnapscusMitZaitLezteNulbar.Value.Zait - ListeMessungObjectiveWindowAgentDialogueScnapscusZuZaitLezte.Zait;

				if (ListeScnapscusWindowAgentDialogueLezteAlterMili < 1000 * 60 * 4 &&
					ListeMessungObjectiveWindowAgentDialogueScnapscusLezte.WindowSictungFrüühesteZait == WindowAgentDialogue.ScnapscusFrühesteZait)
				{
					/*
					 * 2014.04.19
					 * 
					var ListeScnapscusWindowAgentDialogueLezteSictVerglaic =
						new SictWindowAgentDialogueSictFürEntscaidungHinraicendGlaicwertigFürÜbernaameMissionInfo(ListeScnapscusWindowAgentDialogueLezte.WindowScnapscus);

					if (object.Equals(WindowAgentDialogueSictVerglaic, ListeScnapscusWindowAgentDialogueLezteSictVerglaic))
					 * */

					if (SictWindowAgentDialogueZuusctand.HinraicendGlaicwertigFürÜbernaameMissionInfo(
						WindowAgentDialogueScnapscusLezte, ListeMessungObjectiveWindowAgentDialogueScnapscusLezte.WindowScnapscus))
					{
						AnnaameWindowInhaltNoi = false;
					}
				}
			}

			var AusWindowScnapscusInfo =
				new SictWertMitZait<SictAusWindowScnapscusInfo<VonSensor.WindowAgentDialogue>>(
					WindowAgentDialogueScnapscusMitZaitLezteNulbar.Value.Zait,
					new SictAusWindowScnapscusInfo<VonSensor.WindowAgentDialogue>(WindowAgentDialogue));

			if (AnnaameWindowInhaltNoi)
			{
				/*
				 * 2014.04.19
				 * Beobactung:
				 * Mission Objective Item werd aingesamelt wäärend AgentDialogue noc ofe, Inhalt des AgentDialogue werd nit aktualisiirt um noie Zuusctand zu präsentiire.
				 * */
				ListeMessungObjectiveWindowAgentDialogueScnapscusZuZait.Add(AusWindowScnapscusInfo);
			}

			TailFürNuzer.MissionInfoLezte = MissionInfoZuZaitLezteBerecne();
		}

		public void AingangUtilmenu(SictWertMitZait<VonSensor.UtilmenuMissionInfo>? UtilmenuZuZait)
		{
			this.UtilmenuZuZait = UtilmenuZuZait;

			var MengeLocation = this.MengeLocation;

			if (UtilmenuZuZait.HasValue)
			{
				var Zait = UtilmenuZuZait.Value.Zait;

				if (null != UtilmenuZuZait.Value.Wert)
				{
					var UtilmenuMengeLocation = UtilmenuZuZait.Value.Wert.MengeLocation;

					if (null != UtilmenuMengeLocation)
					{
						foreach (var UtilmenuLocation in UtilmenuMengeLocation)
						{
							if (null == UtilmenuLocation)
							{
								continue;
							}

							var UtilmenuLocationHeaderText = UtilmenuLocation.HeaderText;

							if (null == UtilmenuLocationHeaderText)
							{
								continue;
							}

							if (null == MengeLocation)
							{
								this.MengeLocation = MengeLocation = new List<SictMissionLocationInfoAggr>();
							}

							var Location = MengeLocation.FirstOrDefault((Kandidaat) => Kandidaat.InUtilmenuBescriftung == UtilmenuLocation.HeaderText);

							if (null == Location)
							{
								Location = new SictMissionLocationInfoAggr();

								MengeLocation.Add(Location);
							}

							Location.AingangUtilMenu(Zait, UtilmenuLocation);
						}
					}
				}
			}
		}

		public bool? WindowAgentDialoguePasendZuAgent(VonSensor.WindowAgentDialogue KandidaatWindow)
		{
			if (null == KandidaatWindow)
			{
				return null;
			}

			var WindowAgentName = KandidaatWindow.AgentName;

			var TailFürNuzer = this.TailFürNuzer;

			if (null == TailFürNuzer)
			{
				return null;
			}

			return string.Equals(ExtractFromOldAssembly.Bib3.Glob.TrimNullable(TailFürNuzer.AgentName), ExtractFromOldAssembly.Bib3.Glob.TrimNullable(WindowAgentName));
		}

		public bool? LobbyAgentEntryPasendZuMission(VonSensor.LobbyAgentEntry AgentEntry)
		{
			if (null == AgentEntry)
			{
				return null;
			}

			var TailFürNuzer = this.TailFürNuzer;

			if (null == TailFürNuzer)
			{
				return null;
			}

			return
				string.Equals(AgentEntry.AgentName, TailFürNuzer.AgentName) &&
				AgentEntry.AgentLevel == TailFürNuzer.AgentLevel;
		}

		/// <summary>
		/// Vorerst nur Prüüfung des Titel.
		/// </summary>
		/// <param name="Utilmenu"></param>
		/// <returns></returns>
		public bool UtilmenuPastZuMission(VonSensor.UtilmenuMissionInfo Utilmenu)
		{
			if (null == Utilmenu)
			{
				return false;
			}

			var TailFürNuzer = this.TailFürNuzer;

			if (null == TailFürNuzer)
			{
				return false;
			}

			var Titel = TailFürNuzer.Titel;
			var UtilmenuMissionTitelText = Utilmenu.MissionTitelText;

			if (null == Titel || null == UtilmenuMissionTitelText)
			{
				return false;
			}

			//	2014.02.29	Beobactung: Mission Titel enthalt an ende Leerzaice, daher vor Verglaic Trim

			if (!string.Equals(UtilmenuMissionTitelText.Trim(), Titel.Trim()))
			{
				return false;
			}

			return true;
		}

		static public bool WindowAgentPastZuMission(
			SictMissionZuusctand Mission,
			VonSensor.WindowAgent WindowAgent,
			bool BedingungAgentName,
			bool BedingungAgentLocation,
			bool BedingungMissionTitel,
			bool BedingungObjective)
		{
			if (null == Mission)
			{
				return false;
			}

			if (null == WindowAgent)
			{
				return false;
			}

			if (BedingungAgentName)
			{
				if (null == WindowAgent.AgentName)
				{
					return false;
				}

				/*
				 * 2013.09.04 Beobactung Feeler in EVE Odyssey Version: 8.32.616505:
				 * bai Verwendung von "Read Details" in Utilmenu Mission werd falscer Agent-Name angezaigt wen beraits
				 * vorher ain Fenster diises Typ mit ainer Mission von anderem Agent geöfnet war.
				 * */
				if (!string.Equals(Mission.TailFürNuzer.AgentName, WindowAgent.AgentName))
				{
					return false;
				}
			}

			if (BedingungAgentLocation)
			{
				var WindowAgentAgentLocation = WindowAgent.AgentLocation;

				var MissionTailFürNuzerAgentLocation = Mission.TailFürNuzer.AgentLocation;

				if (!(MissionTailFürNuzerAgentLocation == WindowAgentAgentLocation))
				{
					if (null == MissionTailFürNuzerAgentLocation || null == WindowAgentAgentLocation)
					{
						return false;
					}

					if (!string.Equals(WindowAgentAgentLocation.LocationName, MissionTailFürNuzerAgentLocation.LocationName))
					{
						return false;
					}
				}
			}

			if (BedingungMissionTitel || BedingungObjective)
			{
				var WindowAgentZusamefasungMissionInfo = WindowAgent.ZusamefasungMissionInfo;

				if (null == WindowAgentZusamefasungMissionInfo)
				{
					return false;
				}

				if (BedingungMissionTitel)
				{
					if (!string.Equals(Mission.TailFürNuzer.Titel, WindowAgentZusamefasungMissionInfo.MissionTitel))
					{
						return false;
					}
				}

				if (BedingungObjective)
				{
					/*
					 * 2014.02.00
					 * 
					var ListeMesungObjectiveZuusctand = Mission.ListeMesungObjectiveZuusctand;

					if (null != ListeMesungObjectiveZuusctand)
					 * */
					{
						/*
						 * 2014.02.00
						 * 
						var ListeMesungObjectiveZuusctandLezte = ListeMesungObjectiveZuusctand.LastOrDefault();
						 * */

						var ListeMesungObjectiveZuusctandLezte =
							Mission.ListeMesungObjectiveZuusctandZuZaitLezteBerecne() ??
							default(SictWertMitZait<VonSensor.WindowAgentMissionInfo>);

						if (null == ListeMesungObjectiveZuusctandLezte.Wert)
						{
							return false;
						}

						var MissionMissionObjectiveMengeElementLocation = ListeMesungObjectiveZuusctandLezte.Wert.MengeObjectiveElementTypLocationBerecne();
						var WindowAgentMissionObjectiveMengeElementLocation = WindowAgentZusamefasungMissionInfo.MengeObjectiveElementTypLocationBerecne();

						if (null != MissionMissionObjectiveMengeElementLocation)
						{
							//	Alle Location aus bisherige Messung des Objective müse in Window aufgefüürt sain.
							if (!MissionMissionObjectiveMengeElementLocation.All((BisherLocationObjective) =>
							{
								var BisherLocation = BisherLocationObjective.Location;

								if (null == BisherLocation)
								{
									return true;
								}

								return WindowAgentMissionObjectiveMengeElementLocation.Any((KandidaatLocationObjective) =>
								{
									var KandidaatLocation = KandidaatLocationObjective.Location;

									if (null == KandidaatLocation)
									{
										return false;
									}

									return string.Equals(KandidaatLocation.LocationName, BisherLocation.LocationName, StringComparison.InvariantCultureIgnoreCase);
								});
							}))
							{
								return false;
							}
						}
					}
				}
			}

			return true;
		}

		static public SictObjektIdentitäätPerTypeUndNameRegex[] MengeFilterAccGate =
			new SictObjektIdentitäätPerTypeUndNameRegex[]{
			new SictObjektIdentitäätPerTypeUndNameRegex("Acc.*Gate"),
			/*
			 * 2014.04.26	Fund Acc-Gate:
			 * Agent:"Pourpes Andonore"
			 * Gate.Type:"Gate To Abandoned Stargate"
			 * Gate.Name:"Acceleration Gate"
			 * */
			new SictObjektIdentitäätPerTypeUndNameRegex("Gate", "Acc.*Gate"),
			};

		static public bool PrädikaatIstAccGate(SictOverViewObjektZuusctand InRaumObjekt)
		{
			if (null == InRaumObjekt)
			{
				return false;
			}

			return MengeFilterAccGate.Any((Filter) => Filter.Pasend(InRaumObjekt));
		}

		static public bool AusUtilmenuLocationRepräsentiirtObjectiveLocation(
			VonSensor.WindowAgentMissionObjectiveObjective Objective,
			VonSensor.UtilmenuMissionLocationInfo AusUtilmenuLocation)
		{
			var Location = (null == Objective) ? null : Objective.Location;

			return AusUtilmenuLocationRepräsentiirtObjectiveLocation(
				Location,
				AusUtilmenuLocation);
		}

		static public bool AusUtilmenuLocationRepräsentiirtObjectiveLocation(
			MissionLocation Location,
			VonSensor.UtilmenuMissionLocationInfo AusUtilmenuLocation)
		{
			if (null == Location || null == AusUtilmenuLocation)
			{
				return false;
			}

			var KnopfLocation = AusUtilmenuLocation.KnopfLocation;

			if (null == KnopfLocation)
			{
				return false;
			}

			return string.Equals(Location.LocationName, KnopfLocation.Bescriftung);
		}

		public SictWertMitZait<VonSensor.WindowAgentMissionInfo>? ListeMesungObjectiveZuusctandZuZaitLezteBerecne()
		{
			var ListeMessungObjectiveWindowAgentDialogueScnapscusZuZait = this.ListeMessungObjectiveWindowAgentDialogueScnapscusZuZait;

			if (null == ListeMessungObjectiveWindowAgentDialogueScnapscusZuZait)
			{
				return null;
			}

			var MessungObjectiveLezteWindowScnapscusZuZait = ListeMessungObjectiveWindowAgentDialogueScnapscusZuZait.LastOrDefault();

			var MissionInfo =
				(null == MessungObjectiveLezteWindowScnapscusZuZait.Wert) ? null :
				MessungObjectiveLezteWindowScnapscusZuZait.Wert.WindowScnapscus.ZusamefasungMissionInfo;

			return new SictWertMitZait<VonSensor.WindowAgentMissionInfo>(MessungObjectiveLezteWindowScnapscusZuZait.Zait, MissionInfo);
		}

		public VonSensor.WindowAgentDialogue WindowAgentDialogueLezteBerecne(
			Int64? ScnapscusZaitScrankeMin = null)
		{
			var WindowAgentDialogueZuZaitLezte = WindowAgentDialogueZuZaitLezteBerecne(ScnapscusZaitScrankeMin);

			if (!WindowAgentDialogueZuZaitLezte.HasValue)
			{
				return null;
			}

			return WindowAgentDialogueZuZaitLezte.Value.Wert;
		}

		public SictWertMitZait<VonSensor.WindowAgentDialogue>? WindowAgentDialogueZuZaitLezteBerecne(
			Int64? ScnapscusZaitScrankeMin = null)
		{
			var ScnapscusWindowAgentDialogueZuZaitLezte = this.ScnapscusWindowAgentDialogueZuZaitLezte;

			if (!ScnapscusWindowAgentDialogueZuZaitLezte.HasValue)
			{
				return null;
			}

			if (ScnapscusWindowAgentDialogueZuZaitLezte.Value.Zait < ScnapscusZaitScrankeMin)
			{
				return null;
			}

			return ScnapscusWindowAgentDialogueZuZaitLezte;
		}

		public SictWertMitZait<MissionInfo> MissionInfoZuZaitLezteBerecne()
		{
			var ListeMesungObjectiveZuusctandLezte = ListeMesungObjectiveZuusctandZuZaitLezteBerecne();

			if (!ListeMesungObjectiveZuusctandLezte.HasValue)
			{
				return default(SictWertMitZait<MissionInfo>);
			}

			if (null == ListeMesungObjectiveZuusctandLezte.Value.Wert)
			{
				return default(SictWertMitZait<MissionInfo>);
			}

			return new SictWertMitZait<MissionInfo>(ListeMesungObjectiveZuusctandLezte.Value.Zait, ListeMesungObjectiveZuusctandLezte.Value.Wert.TailFürNuzerBerecne());
		}

		static public bool MenuEntryLocationPasendZuLocation(
			SictMissionLocationInfoAggr Location,
			VonSensor.MenuEntry MenuEntry)
		{
			if (null == MenuEntry || null == Location)
			{
				return false;
			}

			var MenuEntryBescriftung = MenuEntry.Bescriftung;
			var LocationInUtilmenuBescriftung = Location.InUtilmenuBescriftung;

			if (null == MenuEntryBescriftung || null == LocationInUtilmenuBescriftung)
			{
				return false;
			}

			//	String mus nit glaic sain. z.B. an "Agent Home Base" werd anscainend mancmaal noc der Naame des Solar System angehängt
			return Regex.Match(MenuEntryBescriftung, LocationInUtilmenuBescriftung, RegexOptions.IgnoreCase).Success;
		}

		public void AingangListSurroundingsButtonMenu(
			IEnumerable<SictGbsMenuZuusctand> KaskaadeMenuAbMission,
			SictAusGbsLocationInfo CurrentLocation)
		{
			if (null == KaskaadeMenuAbMission)
			{
				return;
			}

			var CurrentLocationSolarSystemName = (null == CurrentLocation) ? null : CurrentLocation.SolarSystemName;

			var MengeLocation = this.MengeLocation;

			var MissionMenu = KaskaadeMenuAbMission.FirstOrDefault();

			var MissionMenuMenu = KaskaadeMenuAbMission.ElementAtOrDefault(1);

			if (null == MissionMenu)
			{
				return;
			}

			var MissionMenuScnapscus = MissionMenu.AingangScnapscusTailObjektIdentLezteBerecne();

			if (null == MissionMenuScnapscus)
			{
				return;
			}

			var MissionMenuListeEntry = MissionMenuScnapscus.ListeEntry;

			var MissionMenuEntryHighlight =
				(null == MissionMenuListeEntry) ? null :
				MissionMenuListeEntry.FirstOrDefault((Kandidaat) => true == Kandidaat.Highlight);

			if (null != MissionMenuEntryHighlight &&
				null != CurrentLocationSolarSystemName &&
				null != MissionMenuMenu)
			{
				var MengeKandidaatLocation =
					(null == MengeLocation) ? null :
					MengeLocation.Where((Kandidaat) => string.Equals(Kandidaat.LocationNameTailSystem, CurrentLocationSolarSystemName))
					.ToArray();

				var ZuEntryLocation =
					(null == MengeKandidaatLocation) ? null :
					MengeKandidaatLocation
					.FirstOrDefault((KandidaatLocation) => MenuEntryLocationPasendZuLocation(KandidaatLocation, MissionMenuEntryHighlight));

				if (null != ZuEntryLocation)
				{
					ZuEntryLocation.AingangSurroundingsMenu(MissionMenuMenu);
				}
			}
		}

		static readonly public string[] TempHackMengeLocationBescriftungInSpacePattern = new string[]{
			"Encounter"};

		public SictMissionLocationInfoAggr ZuObjectiveLocationLocationInfoAggr(
			SictObjectiveLocationAuswert ObjectiveLocation,
			bool FilterInSpace = false)
		{
			if (null == ObjectiveLocation)
			{
				return null;
			}

			var ObjectiveLocationLocationInfo = ObjectiveLocation.Objective.Location;

			if (null == ObjectiveLocationLocationInfo)
			{
				return null;
			}

			var LocationNameTailSystem = ObjectiveLocationLocationInfo.LocationNameTailSystem;

			if (null == LocationNameTailSystem)
			{
				return null;
			}

			var MengeLocation = this.MengeLocation;

			if (null == MengeLocation)
			{
				return null;
			}

			var TempHackMengeLocationBescriftungInSpacePattern = SictMissionZuusctand.TempHackMengeLocationBescriftungInSpacePattern;

			foreach (var KandidaatLocation in MengeLocation)
			{
				if (null == KandidaatLocation)
				{
					continue;
				}

				if (!string.Equals(LocationNameTailSystem, KandidaatLocation.LocationNameTailSystem))
				{
					continue;
				}

				if (FilterInSpace)
				{
					var InUtilmenuBescriftung = KandidaatLocation.InUtilmenuBescriftung;

					if (null == InUtilmenuBescriftung)
					{
						continue;
					}

					if (null == TempHackMengeLocationBescriftungInSpacePattern)
					{
						continue;
					}

					if (!TempHackMengeLocationBescriftungInSpacePattern.Any((LocationBescriftungInSpacePattern) =>
						Regex.Match(InUtilmenuBescriftung, LocationBescriftungInSpacePattern, RegexOptions.IgnoreCase).Success))
					{
						continue;
					}
				}

				return KandidaatLocation;
			}

			return null;
		}

		public bool AbbrucSolBerecne(
			int? RaumAlterScrankeMax,
			int? RaumZaitNaacLezteAnforderungErfültMax)
		{
			var Strategikon = this.Strategikon;
			var ListePfaad = this.ListePfaad();
			var RaumAktuel = this.RaumAktuel;

			if (null != ListePfaad)
			{
				if (5 < ExtractFromOldAssembly.Bib3.Extension.CountNullable(ListePfaad))
				{
					return true;
				}
			}

			if (null == Strategikon)
			{
				return true;
			}
			else
			{
				if (null != RaumAktuel)
				{
					var RaumAlterMili = ZaitMili - RaumAktuel.BeginZait;

					var RaumAlter = RaumAlterMili / 1000;

					if (RaumAlterScrankeMax < RaumAlter)
					{
						return true;
					}
				}
			}

			return false;
		}

	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictAusWindowScnapscusInfo<TScnapscus>
		where TScnapscus : VonSensor.Window
	{
		[JsonProperty]
		readonly public Int64? WindowSictungFrüühesteZait;

		[JsonProperty]
		readonly public TScnapscus WindowScnapscus;

		public SictAusWindowScnapscusInfo()
		{
		}

		public SictAusWindowScnapscusInfo(
			Int64? WindowSictungFrüühesteZait,
			TScnapscus WindowScnapscus)
		{
			this.WindowSictungFrüühesteZait = WindowSictungFrüühesteZait;
			this.WindowScnapscus = WindowScnapscus;
		}

		public SictAusWindowScnapscusInfo(
			SictGbsWindowZuusctand Window)
			:
			this(
			(null == Window) ? null : Window.ScnapscusFrühesteZait,
			(null == Window) ? null : Window.AingangScnapscusTailObjektIdentLezteBerecne() as TScnapscus)
		{
		}
	}
}
