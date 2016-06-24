using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Optimat.EveOnline.Anwendung;
using Optimat.ScpezEveOnln;
using Bib3;
using Optimat.EveOnline.Base;
//using Optimat.EveOnline.AuswertGbs;
using Optimat.EveOnline.VonSensor;
using ExtractFromOldAssembly.Bib3;

namespace Optimat.EveOnline.Anwendung
{
	public partial class SictInRaumAktioonUndGefect
	{
		static readonly public SictShipClassEnum[] ShipClassOrdnungSignatureRadiusAufstaigend = new SictShipClassEnum[]{
			SictShipClassEnum.frigate,
			SictShipClassEnum.destroyer,
			SictShipClassEnum.cruiser,
			SictShipClassEnum.battlecruiser,
			SictShipClassEnum.battleship};

		static readonly public string AktioonTargetActivateAusGrupePrioHööcsteZwekKomponente = "activate one of Targets in Group with highest Priority";

		static readonly OverviewPresetDefaultTyp[] AutoPilotOverviewListePresetDefaultPrio = new OverviewPresetDefaultTyp[]{
			OverviewPresetDefaultTyp.General,
			OverviewPresetDefaultTyp.WarpTo,
			OverviewPresetDefaultTyp.All};

		static readonly OverviewPresetDefaultTyp[] AutoMineOverviewListePresetDefaultPrio = new OverviewPresetDefaultTyp[]{
			OverviewPresetDefaultTyp.Mining,
			OverviewPresetDefaultTyp.General,
			OverviewPresetDefaultTyp.All,
			OverviewPresetDefaultTyp.WarpTo,};

		static readonly OverviewPresetDefaultTyp[] AutoMissionOverviewListePresetDefaultPrio = new OverviewPresetDefaultTyp[]{
			OverviewPresetDefaultTyp.General,
			OverviewPresetDefaultTyp.Loot,
			OverviewPresetDefaultTyp.All,
			OverviewPresetDefaultTyp.WarpTo,};

		public void Aktualisiire(
			ISictAutomatZuusctand AutomaatZuusctand)
		{
			if (null == AutomaatZuusctand)
			{
				return;
			}

			var AusScnapscusAuswertungZuusctand = AutomaatZuusctand.ListeScnapscusLezteAuswertungErgeebnisNaacSimu;

			if (null == AusScnapscusAuswertungZuusctand)
			{
				return;
			}

			var NuzerZaitMili = AutomaatZuusctand.NuzerZaitMili;
			var ServerZaitMili = AutomaatZuusctand.ServerZaitMili;

			{
				SictVerzwaigungNaacShipZuusctandScranke InRaumVerhalteGefectFortsazScranke = null;
				SictVerzwaigungNaacShipZuusctandScranke InRaumVerhalteBeweegungUnabhängigVonGefectScranke = null;
				var MengeOverviewObjektGrupeVerwendet = new List<SictOverviewObjektGrupeEnum>();
				SictOverviewObjektGrupeEnum[] MengeOverviewObjektGrupeVerwendetNaacBerüksictigungPrio = null;
				OverviewPresetDefaultTyp[] OverviewListePresetDefaultPrio = AutoPilotOverviewListePresetDefaultPrio;
				KeyValuePair<string, OverviewPresetDefaultTyp>[] OverviewMengeZuTabNamePresetDefault = null;
				string OverviewTabZuAktiviireFürMaidungScroll = null;
				SictOverviewObjektGrupeEnum[] MengeOverviewObjektGrupeMesungZuErsctele = null;
				bool? ZiilLocationNääxteErraict = null;
				bool? InRaumAktioonFortsaz = null;
				bool? InfoPanelRouteFraigaabe = null;
				var GefectBaitritFraigaabe = false;
				var InRaumAktuelTreferpunkteVolsctändigRegeneriirbar = false;
				var GefectBescteeheFraigaabe = false;
				var GefectUnabhängigVonBeweegungFraigaabe = false;
				SictNaacNuzerMeldungZuEveOnlineCause AnforderungFluctUrsace = null;
				SictNaacNuzerMeldungZuEveOnlineCause AktioonUndockFraigaabeNictUrsace = null;
				SictNaacNuzerMeldungZuEveOnlineCause AnforderungDockUrsace = null;
				bool? FürWirkungDestruktAufgaabeDroneEngageTarget = null;
				bool? WarteAufDronePrioVorRaumVerlase = null;
				bool? RaumVerlaseFraigaabe = true;
				var ShipFittingPasendNictZuMissionAktuelLezteZait = this.ShipFittingPasendNictZuMissionAktuelLezteZait;

				try
				{
					var ParamZuZaitVerhalteKombi = AutomaatZuusctand.ParamZuZaitVerhalteKombi;
					var OptimatParam = AutomaatZuusctand.OptimatParam();
					var FittingUndShipZuusctand = AutomaatZuusctand.FittingUndShipZuusctand;
					var OverviewUndTarget = AutomaatZuusctand.OverviewUndTarget;
					var AgentUndMission = AutomaatZuusctand.AgentUndMission;
					var GbsZuusctand = AutomaatZuusctand.Gbs;

					WarteAufDronePrioVorRaumVerlase =
						!(this.AnforderungDroneReturnBeginZaitMili + NaacEntscaidungDroneReturnWartezaitBisFraigaabeRaumVerlase < NuzerZaitMili);

					var AbovemainMessageClusterShutdownLezte = (null == GbsZuusctand) ? null : GbsZuusctand.AbovemainMessageClusterShutdownLezte;

					if (AutomaatZuusctand?.VonSensorScnapscus?.MemoryMeasurement?.Wert?.SessionDurationRemaining < 60 * 15)
						AnforderungDockUrsace = new SictNaacNuzerMeldungZuEveOnlineCause(SictNaacNuzerMeldungZuEveOnlineCauseTypeBinär.OptimatServerSessionEnd);

					if (null != ParamZuZaitVerhalteKombi)
					{
						if (ParamZuZaitVerhalteKombi.DiinstUnterbrecungNääxteZait - SictAutomatZuusctand.InRaumAktioonEndeZaitDistanzBisDiinstUnterbrecung < ServerZaitMili / 1000)
						{
							AnforderungDockUrsace = new SictNaacNuzerMeldungZuEveOnlineCause(SictNaacNuzerMeldungZuEveOnlineCauseTypeBinär.OptimatServerSessionEnd);
						}

						if (ParamZuZaitVerhalteKombi.DiinstUnterbrecungNääxteZait - SictAutomatZuusctand.MissionAktioonFüüreAusEndeZaitDistanzBisDiinstUnterbrecung < ServerZaitMili / 1000)
						{
							AktioonUndockFraigaabeNictUrsace = new SictNaacNuzerMeldungZuEveOnlineCause(SictNaacNuzerMeldungZuEveOnlineCauseTypeBinär.OptimatServerSessionEnd);
						}
					}

					if (null != AbovemainMessageClusterShutdownLezte)
					{
						var AbovemainMessageClusterShutdownLezteAlter = (NuzerZaitMili - AbovemainMessageClusterShutdownLezte.BeginZait) / 1000;

						if (AbovemainMessageClusterShutdownLezteAlter < 60 * 4)
						{
							AnforderungDockUrsace = new SictNaacNuzerMeldungZuEveOnlineCause(SictNaacNuzerMeldungZuEveOnlineCauseTypeBinär.EveServerExpectedDowntime);
						}
					}

					var VonNuzerParamAutoMineFraigaabe = (null == OptimatParam) ? null : OptimatParam.AutoMineFraigaabe;
					var VonNuzerParamAutoMissionFraigaabe = (null == OptimatParam) ? null : OptimatParam.AutoMissionFraigaabe;

					var VonNuzerParamInRaumVerhalteBaasis = (null == OptimatParam) ? null : OptimatParam.InRaumVerhalteBaasis;

					InRaumVerhalteGefectFortsazScranke = (null == VonNuzerParamInRaumVerhalteBaasis) ? null : VonNuzerParamInRaumVerhalteBaasis.GefectFortsazScranke;
					var InRaumVerhalteGefectBaitritScranke = (null == VonNuzerParamInRaumVerhalteBaasis) ? null : VonNuzerParamInRaumVerhalteBaasis.GefectBaitritScranke;
					InRaumVerhalteBeweegungUnabhängigVonGefectScranke = (null == VonNuzerParamInRaumVerhalteBaasis) ? null : VonNuzerParamInRaumVerhalteBaasis.BeweegungUnabhängigVonGefectScranke;

					var ModuleRegenAinScrankeMiliNulbar = (null == VonNuzerParamInRaumVerhalteBaasis) ? null : VonNuzerParamInRaumVerhalteBaasis.ModuleRegenAinScrankeMili;
					var ModuleRegenAusScrankeMiliNulbar = (null == VonNuzerParamInRaumVerhalteBaasis) ? null : VonNuzerParamInRaumVerhalteBaasis.ModuleRegenAusScrankeMili;

					var ListeSelbstScifZuusctandVergangenhait = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.ListeSelbstScifZuusctandVergangenhait;

					var ModuleRegenAinScrankeMili = ModuleRegenAinScrankeMiliNulbar ?? 600;
					var ModuleRegenAusScrankeMili = ModuleRegenAusScrankeMiliNulbar ?? 800;

					var ScnapscusWindowDroneView = (null == AusScnapscusAuswertungZuusctand) ? null : AusScnapscusAuswertungZuusctand.WindowDroneView;

					var ScnapscusGrupeDronesInLocalSpace = (null == ScnapscusWindowDroneView) ? null : ScnapscusWindowDroneView.GrupeDronesInLocalSpace;

					var ScnapscusGrupeDronesInLocalSpaceMengeDroneEntry = (null == ScnapscusGrupeDronesInLocalSpace) ? null : ScnapscusGrupeDronesInLocalSpace.MengeDroneEntry;

					FürWirkungDestruktAufgaabeDroneEngageTarget =
						(null == ScnapscusGrupeDronesInLocalSpaceMengeDroneEntry) ? false :
						ScnapscusGrupeDronesInLocalSpaceMengeDroneEntry.Any((DroneEntry) => DroneEntry.StatusSictEnum == DroneEntryStatusSictEnum.Idle);

					if (true == WarteAufDronePrioVorRaumVerlase)
					{
						if (null != ScnapscusGrupeDronesInLocalSpace)
						{
							if (0 < ScnapscusGrupeDronesInLocalSpace.MengeEntryAnzaal)
							{
								RaumVerlaseFraigaabe = false;
							}
						}
					}

					var MengeZuOverviewTabMengeObjektGrupeSictbar =
						(null == OverviewUndTarget) ? null : OverviewUndTarget.MengeZuOverviewTabMengeObjektGrupeSictbar;

					/*
					 * 2014.09.27
					 * 
					 * Ersaz durc AutomaatZuusctand.MesungShipIsPodLezteZaitUndWert.
					 * 
					var UndockedMesungShipTypIstPodLezteZaitMili = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.UndockedMesungShipIsPodLezteZaitMili;
					 * */

					var ShipZuusctandLezte = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.ShipZuusctand;

					var SelbsctShipDocked = (null == ShipZuusctandLezte) ? null : ShipZuusctandLezte.Docked;
					var SelbsctShipWarping = (null == ShipZuusctandLezte) ? null : ShipZuusctandLezte.Warping;
					var SelbsctShipWarpScrambled = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.SelbsctShipWarpScrambled;

					var ShipDockedLezteZaitMili = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.DockedLezteZaitMili;

					var ZaitraumDockedLezteHinraicendAltFürBeurtailungShipZuusctand =
						(null == FittingUndShipZuusctand) ? (bool?)null :
						FittingUndShipZuusctand.ZaitraumDockedLezteHinraicendAltFürBeurtailungShipZuusctand;

					var MissionAktuel = (null == AgentUndMission) ? null : AgentUndMission.MissionAktuel;

					var MissionAktuelAnforderungInRaum = (null == MissionAktuel) ? null : MissionAktuel.NaacAutomaatAnforderungInRaum;

					var MissionMengeObjektZuBearbaiteMitPrio =
						(null == MissionAktuelAnforderungInRaum) ? null :
						MissionAktuelAnforderungInRaum.MengeObjektZuBearbaiteMitPrio;

					var MissionZiilLocationNääxteAuswert = (null == MissionAktuel) ? null : MissionAktuel.ZiilLocationNääxteAuswert;

					ZiilLocationNääxteErraict =
						(null == MissionZiilLocationNääxteAuswert) ? null : MissionZiilLocationNääxteAuswert.LocationErraict;

					if (null != MissionZiilLocationNääxteAuswert)
					{
						if (false == MissionZiilLocationNääxteAuswert.LocationErraictTailSystem &&
							true == MissionZiilLocationNääxteAuswert.LocationSystemGlaicInfoPanelRouteDestinationSystem)
						{
							InfoPanelRouteFraigaabe = true;
						}
					}

					var MissionAktuelStrategikon = (null == MissionAktuel) ? null : MissionAktuel.Strategikon;

					var MissionAnforderungInRaumMengeOverviewObjektGrupeVerwendet =
						(null == MissionAktuelAnforderungInRaum) ? null : MissionAktuelAnforderungInRaum.MengeOverviewObjektGrupeVerwendet;

					if (null != MissionAnforderungInRaumMengeOverviewObjektGrupeVerwendet)
					{
						MengeOverviewObjektGrupeVerwendet.AddRange(MissionAnforderungInRaumMengeOverviewObjektGrupeVerwendet);
					}

					if (null != MissionAktuelAnforderungInRaum)
					{
						MengeOverviewObjektGrupeMesungZuErsctele = MissionAktuelAnforderungInRaum.MengeOverviewObjektGrupeMesungZuErsctele;
					}

					if (null != ListeSelbstScifZuusctandVergangenhait)
					{
						//	Berecnung InRaumAktuelTreferpunkteVolsctändigRegeneriirbar

						var ListeSelbstScifZuusctandVergangenhaitTailmenge =
							ListeSelbstScifZuusctandVergangenhait
							.Where((Kandidaat) => (NuzerZaitMili - Kandidaat.Zait) < 11111)
							.OrderBy((Kandidaat) => Kandidaat.Zait)
							.ToArray();

						var TreferpunkteNictAbsctaigend = true;
						ShipHitpointsAndEnergy TreferpunkteLezte = null;

						foreach (var SelbstScifZuusctandVergangenhait in ListeSelbstScifZuusctandVergangenhaitTailmenge)
						{
							if (null == SelbstScifZuusctandVergangenhait.Wert)
							{
								continue;
							}

							var TreferpunkteNoi = SelbstScifZuusctandVergangenhait.Wert.HitpointsRelMili;

							if (!ShipHitpointsAndEnergy.O0KlainerglaicO1(TreferpunkteLezte.WithCapacitySetTo0(), TreferpunkteNoi.WithCapacitySetTo0()))
							{
								TreferpunkteNictAbsctaigend = false;
								break;
							}
						}

						if (TreferpunkteNictAbsctaigend)
						{
							InRaumAktuelTreferpunkteVolsctändigRegeneriirbar = true;
						}
					}

					var AnforderungShieldBoosterScrankeAinErfült = false;
					var AnforderungShieldBoosterScrankeAusErfült = false;
					var AnforderungArmorRepairerScrankeAinErfült = false;
					var AnforderungArmorRepairerScrankeAusErfült = false;

					/*
					 * 2014.09.20
					 * 
					var VorhersaageTreferpunkteMitZait = FittingUndShipZuusctand.VorhersaageSelbstScifTreferpunkte;
					 * */
					var VorhersaageTreferpunkteMitZait = AutomaatZuusctand.VorhersaageSelbstScifTreferpunkte;

					var VorhersaageTreferpunkte = VorhersaageTreferpunkteMitZait.Wert;

					if (null != ShipZuusctandLezte)
					{
						var Treferpunkte = ShipZuusctandLezte.HitpointsRelMili;

						if (null != Treferpunkte)
						{
							var Shield = Treferpunkte.Shield;
							var Armor = Treferpunkte.Armor;
							var Struct = Treferpunkte.Struct;

							var CapacitorCapacityNormiirtMili = Treferpunkte.Capacitor;

							if (null != Shield && null != Armor && null != Struct)
							{
								var ShieldNormiirtMili = Shield;
								var ArmorNormiirtMili = Armor;
								var StructNormiirtMili = Struct;

								if (null != VorhersaageTreferpunkte)
								{
									ShieldNormiirtMili = Bib3.Glob.Min(ShieldNormiirtMili, VorhersaageTreferpunkte.Shield);
									ArmorNormiirtMili = Bib3.Glob.Min(ArmorNormiirtMili, VorhersaageTreferpunkte.Armor);
									StructNormiirtMili = Bib3.Glob.Min(StructNormiirtMili, VorhersaageTreferpunkte.Struct);
								}

								var TreferpunkteKombiMitVorhersaage = new ShipHitpointsAndEnergy(
									StructNormiirtMili,
									ArmorNormiirtMili,
									ShieldNormiirtMili,
									CapacitorCapacityNormiirtMili);

								if (StructNormiirtMili < 900)
								{
									//	Verzwaigung für Debug Haltepunkt
								}

								AnforderungShieldBoosterScrankeAinErfült = ShieldNormiirtMili < ModuleRegenAinScrankeMili;
								AnforderungShieldBoosterScrankeAusErfült = ModuleRegenAusScrankeMili < ShieldNormiirtMili;

								AnforderungArmorRepairerScrankeAinErfült = ArmorNormiirtMili < ModuleRegenAinScrankeMili;
								AnforderungArmorRepairerScrankeAusErfült = ModuleRegenAusScrankeMili < ArmorNormiirtMili;

								if (!(VonNuzerParamAutoMissionFraigaabe ?? false) && (VonNuzerParamAutoMineFraigaabe ?? false))
								{
									InRaumVerhalteGefectFortsazScranke = new SictVerzwaigungNaacShipZuusctandScranke(500, 950, 990, null);

									InRaumVerhalteBeweegungUnabhängigVonGefectScranke = new SictVerzwaigungNaacShipZuusctandScranke(800, 950, 990, null);
								}

								if (null != InRaumVerhalteGefectFortsazScranke)
								{
									GefectBescteeheFraigaabe =
										!(true == AusScnapscusAuswertungZuusctand.InfoPanelIncursionsExistent) &&
										(!(ZaitraumDockedLezteHinraicendAltFürBeurtailungShipZuusctand ?? false) ||
										InRaumVerhalteGefectFortsazScranke.AingehalteDurcTreferpunkteUndCapacitor(
										TreferpunkteKombiMitVorhersaage));
								}

								if (GefectBescteeheFraigaabe)
								{
									if (null != InRaumVerhalteGefectBaitritScranke)
									{
										/*
										 * 2014.11.05 vorersct Hack:
										 * 
----------------------------------------------
2014.11.05
"B:\Berict\Berict.Nuzer\[ZAK=2014.11.05.12.33.30,NB=32].Anwendung.Berict":
Auto.Fraigaabe.Ende um "14:34:04"
Probleem von "13:23:05" bis "14:34:04": Automaat hängegebliibe in Mission.
--->Automaat versuuct naac MSN Location zu Warpe "travel to Mission Location[Kusomonmon]".
Probleem:Automaat Tail Msn wartet auf in RaumAktioonUndGefect.GefectBaitritFraigaabe, diise blaibt jeedoc false Aufgrund von unterscraiten des minimaal benöötigte Capacitor (Capacitor kaam in diisem Fal nit üüber 65%).
--->GefectBaitritFraigaabe solte uurscprünglic verhindere das Automaat in nääxte Msn Raum vorgeet oder in aktuele Raum waitere Aggro auslööst obwool das Ship vorher noc reegeneriire (Capacitor oder Shield) solte.
--->GefectBaitritFraigaabe solte auf True gesezt werde wen erkenbar isc das kaine waitere Erhoolung erfolgt.
----------------------------------------------
										 * */

										InRaumVerhalteGefectBaitritScranke.CapacitorScrankeBetraagMili = null;

										GefectBaitritFraigaabe =
											InRaumVerhalteGefectBaitritScranke.AingehalteDurcTreferpunkteUndCapacitor(
											TreferpunkteKombiMitVorhersaage);
									}

									if (null != InRaumVerhalteBeweegungUnabhängigVonGefectScranke)
									{
										GefectUnabhängigVonBeweegungFraigaabe =
											InRaumVerhalteBeweegungUnabhängigVonGefectScranke.AingehalteDurcTreferpunkteUndCapacitor(
											TreferpunkteKombiMitVorhersaage);
									}
								}
							}
						}
					}

					InRaumAktioonFortsaz =
						!(true == SelbsctShipWarping) &&
						(!(false == ZiilLocationNääxteErraict) &&
						GefectBescteeheFraigaabe) ||
						(true == SelbsctShipWarpScrambled);

					this.AnforderungShieldBooster.AingangTransitionZuZait(NuzerZaitMili, AnforderungShieldBoosterScrankeAusErfült, AnforderungShieldBoosterScrankeAinErfült);
					this.AnforderungArmorRepairer.AingangTransitionZuZait(NuzerZaitMili, AnforderungArmorRepairerScrankeAusErfült, AnforderungArmorRepairerScrankeAinErfült);

					if (ZaitraumDockedLezteHinraicendAltFürBeurtailungShipZuusctand ?? true)
					{
						if (!(true == GefectBescteeheFraigaabe))
						{
							AnforderungFluctUrsace = new SictNaacNuzerMeldungZuEveOnlineCause(SictNaacNuzerMeldungZuEveOnlineCauseTypeBinär.ShipCapacitorOrHitpointsTooLow);
						}
					}

					if (true == AusScnapscusAuswertungZuusctand.InfoPanelIncursionsExistent)
					{
						AnforderungDockUrsace = new SictNaacNuzerMeldungZuEveOnlineCause(SictNaacNuzerMeldungZuEveOnlineCauseTypeBinär.Incursion);
					}

					if (true == VonNuzerParamAutoMissionFraigaabe)
					{
						if (null != MissionAktuel)
						{
							bool? ShipFittingPasendZuMissionAktuel = null;

							//	!!!!	zu ergänze: prüüfe ob Fitting pasend für MissionAktuel

							var FittingUndShipZuusctandMengeModuleRepr = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.MengeModuleRepr;
							var FittingUndShipFitLoadedLezteNocAktiiv = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.FitLoadedLezteNocAktiiv;

							var FittingUndShipFitLoadedLezteNocAktiivZait =
								FittingUndShipFitLoadedLezteNocAktiiv.HasValue ? (Int64?)FittingUndShipFitLoadedLezteNocAktiiv.Value.Zait : null;

							if (Bib3.Extension.NullOderLeer(FittingUndShipZuusctandMengeModuleRepr))
							{
								if (ZaitraumDockedLezteHinraicendAltFürBeurtailungShipZuusctand ?? true)
								{
									ShipFittingPasendZuMissionAktuel = false;
								}
							}

							if (false == SelbsctShipDocked)
							{
								if (false == ShipFittingPasendZuMissionAktuel)
								{
									ShipFittingPasendNictZuMissionAktuelLezteZait = NuzerZaitMili;
								}
								else
								{
									if (ZaitraumDockedLezteHinraicendAltFürBeurtailungShipZuusctand ?? true)
									{
										ShipFittingPasendNictZuMissionAktuelLezteZait = null;
									}
								}
							}

							var ShipFittingPasendNictZuMissionAktuelLezteAlter =
								(NuzerZaitMili - ShipFittingPasendNictZuMissionAktuelLezteZait) / 1000;

							if (ShipFittingPasendNictZuMissionAktuelLezteAlter < 60 * 30 &&
								!(ShipFittingPasendNictZuMissionAktuelLezteZait < FittingUndShipFitLoadedLezteNocAktiivZait) &&
								!(ShipFittingPasendNictZuMissionAktuelLezteZait < MissionAktuel.SictungFrühesteZaitMili()))
							{
								AnforderungDockUrsace = new SictNaacNuzerMeldungZuEveOnlineCause(SictNaacNuzerMeldungZuEveOnlineCauseTypeBinär.ShipFittingInappropriate);
							}
						}
					}

					MengeOverviewObjektGrupeVerwendetNaacBerüksictigungPrio =
						MengeOverviewObjektGrupeVerwendet.Distinct().ToArray();

					if (false == SelbsctShipDocked)
					{
						if (!(true == GefectUnabhängigVonBeweegungFraigaabe))
						{
							MengeOverviewObjektGrupeVerwendetNaacBerüksictigungPrio = new SictOverviewObjektGrupeEnum[] { SictOverviewObjektGrupeEnum.Rat };
						}
					}

					if (VonNuzerParamAutoMineFraigaabe ?? false)
					{
						OverviewListePresetDefaultPrio = AutoMineOverviewListePresetDefaultPrio;
					}

					if (VonNuzerParamAutoMissionFraigaabe ?? false)
					{
						OverviewListePresetDefaultPrio = AutoMissionOverviewListePresetDefaultPrio;
					}

					var WindowOverview = AusScnapscusAuswertungZuusctand.WindowOverview;

					var MengeTab = (null == WindowOverview) ? null : WindowOverview.ListeTabNuzbar;

					var MengeTabName =
						MengeTab.SelectNullable((Tab) => Tab.LabelBescriftung).ToArrayNullable();

					OverviewMengeZuTabNamePresetDefault =
						SictOverviewUndTargetZuusctand.MengeZuTabNameDefaultBerecne(
						MengeTabName,
						SictOverviewUndTargetZuusctand.ListePresetDefaultPrioFüleAufMitNaacrangige(OverviewListePresetDefaultPrio, 99));

					OverviewTabZuAktiviireFürMaidungScroll =
						ZuBevorzugendeOverviewPresetBerecneFürAnforderungMengeGrupeSictbar(
						MengeOverviewObjektGrupeVerwendetNaacBerüksictigungPrio,
						MengeZuOverviewTabMengeObjektGrupeSictbar);
				}
				finally
				{
					if (null != AnforderungDockUrsace)
					{
						AktioonUndockFraigaabeNictUrsace = AnforderungDockUrsace;
					}

					this.InRaumVerhalteGefectFortsazScranke = InRaumVerhalteGefectFortsazScranke;
					this.InRaumVerhalteBeweegungUnabhängigVonGefectScranke = InRaumVerhalteBeweegungUnabhängigVonGefectScranke;
					this.MengeOverviewObjektGrupeVerwendet = MengeOverviewObjektGrupeVerwendet.Distinct().ToArray();
					this.MengeOverviewObjektGrupeVerwendetNaacBerüksictigungPrio = MengeOverviewObjektGrupeVerwendetNaacBerüksictigungPrio;
					this.OverviewListePresetDefaultPrio = OverviewListePresetDefaultPrio;
					this.OverviewMengeZuTabNamePresetDefault = OverviewMengeZuTabNamePresetDefault;
					this.OverviewTabZuAktiviireFürMaidungScroll = OverviewTabZuAktiviireFürMaidungScroll;
					this.MengeOverviewObjektGrupeMesungZuErsctele = MengeOverviewObjektGrupeMesungZuErsctele;
					this.ZiilLocationNääxteErraict = ZiilLocationNääxteErraict;
					this.InRaumAktioonFortsaz = InRaumAktioonFortsaz;
					this.InfoPanelRouteFraigaabe = InfoPanelRouteFraigaabe;
					this.GefectBaitritFraigaabe = GefectBaitritFraigaabe;
					this.InRaumAktuelTreferpunkteVolsctändigRegeneriirbar = InRaumAktuelTreferpunkteVolsctändigRegeneriirbar;
					this.GefectBescteeheFraigaabe = GefectBescteeheFraigaabe;
					this.GefectUnabhängigVonBeweegungFraigaabe = GefectUnabhängigVonBeweegungFraigaabe;
					this.AnforderungFluctUrsace = AnforderungFluctUrsace;
					this.AktioonUndockFraigaabeNictUrsace = AktioonUndockFraigaabeNictUrsace;
					this.AnforderungDockUrsace = AnforderungDockUrsace;
					this.FürWirkungDestruktAufgaabeDroneEngageTarget = FürWirkungDestruktAufgaabeDroneEngageTarget;
					this.WarteAufDronePrioVorRaumVerlase = WarteAufDronePrioVorRaumVerlase;
					this.RaumVerlaseFraigaabe = RaumVerlaseFraigaabe;
					this.ShipFittingPasendNictZuMissionAktuelLezteZait = ShipFittingPasendNictZuMissionAktuelLezteZait;
				}
			}

			AktualisiireTailListePrioMengeObjektZuZersctööre(AutomaatZuusctand);

			AktualisiireTailMengeObjektZuBearbaite(AutomaatZuusctand, AusScnapscusAuswertungZuusctand);

			AktualisiireTailMengeObjektZuUnLocke(AutomaatZuusctand, AusScnapscusAuswertungZuusctand);

			AktualisiireTailAnforderungDroneLaunchUndReturn(AutomaatZuusctand);

			AktualisiireTailAfterburner(AutomaatZuusctand);
		}

		/// <summary>
		/// Berecnet folgende Menge:
		/// - ListePrioMengeEWarTypeZuZersctööre
		/// - ListePrioMengeObjektZuZersctööre
		/// </summary>
		/// <param name="AutomaatZuusctand"></param>
		void AktualisiireTailListePrioMengeObjektZuZersctööre(
			ISictAutomatZuusctand AutomaatZuusctand)
		{
			SictEWarTypeEnum[][] ListePrioMengeEWarTypeZuZersctööre = null;

			try
			{
				if (null == AutomaatZuusctand)
				{
					return;
				}

				var ZaitMili = AutomaatZuusctand.NuzerZaitMili;

				var OptimatParam = AutomaatZuusctand.OptimatParam();
				var FittingUndShipZuusctand = AutomaatZuusctand.FittingUndShipZuusctand;
				var OverviewUndTarget = AutomaatZuusctand.OverviewUndTarget;
				var AgentUndMission = AutomaatZuusctand.AgentUndMission;

				var MissionAktuel = (null == AgentUndMission) ? null : AgentUndMission.MissionAktuel;
				var MissionAnforderungInRaum = (null == MissionAktuel) ? null : MissionAktuel.NaacAutomaatAnforderungInRaum;

				/*
				 * !!!!	Hiir noc ainzufüüge: TrackingDisrupt fals Fitting Turret
				 * */
				ListePrioMengeEWarTypeZuZersctööre = new SictEWarTypeEnum[][]{
					new SictEWarTypeEnum[]{ SictEWarTypeEnum.Jam},
					new SictEWarTypeEnum[]{ SictEWarTypeEnum.WarpScramble},
					new SictEWarTypeEnum[]{ SictEWarTypeEnum.Webify},};
			}
			finally
			{
				this.ListePrioMengeEWarTypeZuZersctööre = ListePrioMengeEWarTypeZuZersctööre;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="AufgaabeObjektZuBearbaiteMitPrio"></param>
		/// <param name="MengeEWarType">Fals diiser Param null ist werd true zurükgegeebe fals irgend ain EWar Type in AufgaabePrio oder Objekt enthalte.</param>
		/// <param name="EWarWirkungZuBerüksictigeZaitScrankeMin"></param>
		/// <returns></returns>
		static public bool AufgaabeMitPrioritäätEnthältAinesAusMengeEWarType(
			SictAufgaabeInRaumObjektZuBearbaiteMitPrio AufgaabeObjektZuBearbaiteMitPrio,
			SictEWarTypeEnum[] MengeEWarType,
			Int64? EWarWirkungZuBerüksictigeZaitScrankeMin)
		{
			if (null == AufgaabeObjektZuBearbaiteMitPrio)
			{
				return false;
			}

			var AufgaabeObjektZuBearbaitePrioritäät = AufgaabeObjektZuBearbaiteMitPrio.Prioritäät;

			var AufgaabePrioritäätInGrupeEWar = AufgaabeObjektZuBearbaitePrioritäät.InGrupeEWar;

			if (null != AufgaabePrioritäätInGrupeEWar)
			{
				foreach (var AusPrioritäätEWarType in AufgaabePrioritäätInGrupeEWar)
				{
					if (null == MengeEWarType)
					{
						//	Irgend
						return true;
					}

					if (MengeEWarType.Contains(AusPrioritäätEWarType))
					{
						return true;
					}
				}
			}

			var AufgaabeObjektZuBearbaite = AufgaabeObjektZuBearbaiteMitPrio.AufgaabeObjektZuBearbaite;

			if (null == AufgaabeObjektZuBearbaite)
			{
				return false;
			}

			var ObjektZuBearbeite = AufgaabeObjektZuBearbaite.OverViewObjektZuBearbaiteVirt();

			if (null == ObjektZuBearbeite)
			{
				return false;
			}

			if (null == MengeEWarType)
			{
				//	Irgend

				var ObjektZuBearbeiteEWarWirkungLezteZait = ObjektZuBearbeite.EWarWirkungLezteZait;

				if (ObjektZuBearbeiteEWarWirkungLezteZait.HasValue)
				{
					if (!(ObjektZuBearbeiteEWarWirkungLezteZait < EWarWirkungZuBerüksictigeZaitScrankeMin))
					{
						return true;
					}
				}

				return false;
			}

			var DictZuEWarTypeWirkungLezteZait = ObjektZuBearbeite.DictZuEWarTypeWirkungLezteZait;

			if (null != DictZuEWarTypeWirkungLezteZait)
			{
				foreach (var EWarType in MengeEWarType)
				{
					var ZuEWarTypeWirkungZaitLezte = Optimat.Glob.TADNulbar(DictZuEWarTypeWirkungLezteZait, EWarType);

					if (!ZuEWarTypeWirkungZaitLezte.HasValue)
					{
						continue;
					}

					if (!(ZuEWarTypeWirkungZaitLezte < EWarWirkungZuBerüksictigeZaitScrankeMin))
					{
						return true;
					}
				}
			}

			return false;
		}

		static SictAufgaabeGrupePrio[] MengeAufgaabeMitPrioGrupiireMitKaskaadeFunkGrupiirung(
			IEnumerable<SictAufgaabeInRaumObjektZuBearbaiteMitPrio> MengeAufgaabeZuGrupiire,
			Func<IEnumerable<SictAufgaabeInRaumObjektZuBearbaiteMitPrio>, SictAufgaabeGrupePrio[]>[] KaskaadeListeFunk)
		{
			if (null == MengeAufgaabeZuGrupiire || null == KaskaadeListeFunk)
			{
				return null;
			}

			var ZwisceergeebnisListeGrupe =
				new SictAufgaabeGrupePrio[]{    new SictAufgaabeGrupePrio(
				MengeAufgaabeZuGrupiire.Select((AufgaabeMitPrio) => AufgaabeMitPrio.AufgaabeObjektZuBearbaite)
				.Where((Aufgaabe) => null   != Aufgaabe)
				.ToArray(), null)};

			for (int KaskaadeFunkIndex = 0; KaskaadeFunkIndex < KaskaadeListeFunk.Length; KaskaadeFunkIndex++)
			{
				var KaskaadeFunk = KaskaadeListeFunk[KaskaadeFunkIndex];

				if (null == KaskaadeFunk)
				{
					continue;
				}

				var InRundeListeListeGrupeAggregiirt = new List<SictAufgaabeGrupePrio>();

				for (int AusRundeVorherGrupeIndex = 0; AusRundeVorherGrupeIndex < ZwisceergeebnisListeGrupe.Length; AusRundeVorherGrupeIndex++)
				{
					var AusRundeVorherGrupe = ZwisceergeebnisListeGrupe[AusRundeVorherGrupeIndex];

					if (null == AusRundeVorherGrupe)
					{
						continue;
					}

					var AusRundeVorherGrupeMengeAufgaabe = AusRundeVorherGrupe.MengeAufgaabe;

					var AusRundeVorherGrupeMengeAufgaabeMitPrio =
						(null == AusRundeVorherGrupeMengeAufgaabe) ? null :
						AusRundeVorherGrupeMengeAufgaabe
						.Where((Aufgaabe) => null != Aufgaabe)
						.Select((Aufgaabe) => MengeAufgaabeZuGrupiire.FirstOrDefault((AufgaabeMitPrio) => AufgaabeMitPrio.AufgaabeObjektZuBearbaite == Aufgaabe))
						.Where((AufgaabeMitPrio) => null != AufgaabeMitPrio)
						.ToArray();

					if (null == AusRundeVorherGrupeMengeAufgaabeMitPrio)
					{
						continue;
					}

					var InRundeListeGrupe = KaskaadeFunk(AusRundeVorherGrupeMengeAufgaabeMitPrio);

					if (null == InRundeListeGrupe)
					{
						continue;
					}

					foreach (var InRundeGrupe in InRundeListeGrupe)
					{
						var InRundeGrupeMengeAufgaabe = InRundeGrupe.MengeAufgaabe;

						if (null == InRundeGrupeMengeAufgaabe)
						{
							continue;
						}

						InRundeListeListeGrupeAggregiirt.Add(
							new SictAufgaabeGrupePrio(
								InRundeGrupeMengeAufgaabe,
								(AusRundeVorherGrupe.GrupePrioNaame ?? "") +
								"." + (InRundeGrupe.GrupePrioNaame ?? "")));
					}
				}

				ZwisceergeebnisListeGrupe = InRundeListeListeGrupeAggregiirt.ToArray();
			}

			return ZwisceergeebnisListeGrupe;
		}

		static public SictAufgaabeGrupePrio[] MengeAufgaabeMitPrioGrupiireNaacInGrupeIndex(
			IEnumerable<SictAufgaabeInRaumObjektZuBearbaiteMitPrio> MengeAufgaabeZuGrupiire)
		{
			if (null == MengeAufgaabeZuGrupiire)
			{
				return null;
			}

			var ListeGrupeGrouping =
				MengeAufgaabeZuGrupiire
				.GroupBy((AufgaabeZuGrupiire) => AufgaabeZuGrupiire.Prioritäät.InGrupeIndex ?? 0)
				.OrderByDescending((Grupe) => Grupe.Key)
				.ToArray();

			var ListeGrupeMitNaame =
				ListeGrupeGrouping
				.Select((Grouping) => new SictAufgaabeGrupePrio(
					Grouping.Select((AufgaabeMitPrio) => AufgaabeMitPrio.AufgaabeObjektZuBearbaite).ToArray(), "Index[" + Grouping.Key.ToString() + "]"))
				.ToArray();

			return ListeGrupeMitNaame;
		}

		static public SictAufgaabeGrupePrio[] MengeAufgaabeMitPrioGrupiireNaacEnthalteInMengeMitNaame(
			IEnumerable<SictAufgaabeInRaumObjektZuBearbaiteMitPrio> MengeAufgaabeZuGrupiire,
			IEnumerable<KeyValuePair<SictAufgaabeInRaumObjektZuBearbaiteMitPrio[], string>> ListeMengeMitNaame)
		{
			if (null == MengeAufgaabeZuGrupiire)
			{
				return null;
			}

			if (null == ListeMengeMitNaame)
			{
				return null;
			}

			var ListeGrupeMitNaame = new List<SictAufgaabeGrupePrio>();

			var MengeAufgaabeBeraitsZugetailt = new List<SictAufgaabeInRaumObjektZuBearbaiteMitPrio>();

			foreach (var MengeMitNaame in ListeMengeMitNaame)
			{
				var GrupeMengeAufgaabeMitPrio =
					(null == MengeMitNaame.Key) ? null :
					MengeAufgaabeZuGrupiire
					.Intersect(MengeMitNaame.Key)
					.Except(MengeAufgaabeBeraitsZugetailt)
					.ToArray();

				var GrupeMengeAufgaabe =
					(null == GrupeMengeAufgaabeMitPrio) ? null :
					GrupeMengeAufgaabeMitPrio
					.Select((AufgaabeMitPrio) => (null == AufgaabeMitPrio) ? null : AufgaabeMitPrio.AufgaabeObjektZuBearbaite)
					.ToArray();

				if (null != GrupeMengeAufgaabeMitPrio)
				{
					MengeAufgaabeBeraitsZugetailt.AddRange(GrupeMengeAufgaabeMitPrio);
				}

				var GrupeMitNaame = new SictAufgaabeGrupePrio(GrupeMengeAufgaabe, MengeMitNaame.Value);

				ListeGrupeMitNaame.Add(GrupeMitNaame);
			}

			var MengeAufgaabeMitPrioNocNictZuugetailt =
				MengeAufgaabeZuGrupiire.Except(MengeAufgaabeBeraitsZugetailt).ToArray();

			var MengeAufgaabeNocNictZuugetailt =
				(null == MengeAufgaabeMitPrioNocNictZuugetailt) ? null :
				MengeAufgaabeMitPrioNocNictZuugetailt
				.Select((AufgaabeMitPrio) => (null == AufgaabeMitPrio) ? null : AufgaabeMitPrio.AufgaabeObjektZuBearbaite)
				.ToArray();

			var GrupeKain = new SictAufgaabeGrupePrio(MengeAufgaabeNocNictZuugetailt, "Kain");

			ListeGrupeMitNaame.Add(GrupeKain);

			return ListeGrupeMitNaame.ToArray();
		}

		static public SictAufgaabeGrupePrio[] MengeAufgaabeMitPrioGrupiireNaacFürGefectReegelungDistance(
			IEnumerable<SictAufgaabeInRaumObjektZuBearbaiteMitPrio> MengeAufgaabeZuGrupiire)
		{
			if (null == MengeAufgaabeZuGrupiire)
			{
				return null;
			}

			var ListeAufgaabeGrupe =
				MengeAufgaabeZuGrupiire
				.GroupBy((AufgaabeMitPrio) => AufgaabeMitPrio.Prioritäät.InGrupeFürGefectAngraiferAufDistanzHalte ?? false)
				.OrderBy((Grupe) => Grupe.Key ? 0 : 4)
				.ToArray();

			var ListeAufgaabeGrupeMitNaame =
				ListeAufgaabeGrupe
				.Select((AufgaabeGrupe) => new SictAufgaabeGrupePrio(
					AufgaabeGrupe.Select((AufgaabeMitPrio) => AufgaabeMitPrio.AufgaabeObjektZuBearbaite).ToArray(),
					"FürGefectAngraiferAufDistanzHalte[" + AufgaabeGrupe.Key.ToString() + "]"))
				.ToArray();

			return ListeAufgaabeGrupeMitNaame;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="AufgaabeGrupeZuUntertaile"></param>
		/// <param name="ListePrioMengeEWarTypeZuZersctööre">Umgekeert sortiirt: Menge bai Index 0 hat hööcste Prioritäät.</param>
		/// <param name="EWarWirkungZurüksictigenZaitScrankeMin"></param>
		/// <returns></returns>
		static public SictAufgaabeGrupePrio[] MengeAufgaabeMitPrioGrupiireNaacEWar(
			IEnumerable<SictAufgaabeInRaumObjektZuBearbaiteMitPrio> MengeAufgaabeZuGrupiire,
			SictEWarTypeEnum[][] ListePrioMengeEWarTypeZuZersctööre,
			Int64? EWarWirkungZuBerüksictigeZaitScrankeMin)
		{
			if (null == MengeAufgaabeZuGrupiire)
			{
				return null;
			}

			var ListeMengeMitNaame = new List<KeyValuePair<SictAufgaabeInRaumObjektZuBearbaiteMitPrio[], string>>();

			var GrupeEWarAnzaal = ((null == ListePrioMengeEWarTypeZuZersctööre) ? 0 : ListePrioMengeEWarTypeZuZersctööre.Length) + 2;

			for (int GrupeEWarIndex = 0; GrupeEWarIndex < GrupeEWarAnzaal; GrupeEWarIndex++)
			{
				var GrupeEWarNaameTailMengeEWar = "Kain";

				var InGrupeMengeEWarType =
					(null == ListePrioMengeEWarTypeZuZersctööre) ? null :
					ListePrioMengeEWarTypeZuZersctööre.ElementAtOrDefault(GrupeEWarIndex);

				Func<SictAufgaabeInRaumObjektZuBearbaiteMitPrio, bool> FunkZugehörigkaitZuGrupe = null;

				if (GrupeEWarIndex < GrupeEWarAnzaal - 1)
				{
					FunkZugehörigkaitZuGrupe = new Func<SictAufgaabeInRaumObjektZuBearbaiteMitPrio, bool>((Aufgaabe) =>
						AufgaabeMitPrioritäätEnthältAinesAusMengeEWarType(Aufgaabe, InGrupeMengeEWarType, EWarWirkungZuBerüksictigeZaitScrankeMin));

					if (Bib3.Extension.NullOderLeer(InGrupeMengeEWarType))
					{
						GrupeEWarNaameTailMengeEWar = "Sonstige";
					}
					else
					{
						GrupeEWarNaameTailMengeEWar = string.Join(",", InGrupeMengeEWarType.Select((EWarType) => EWarType.ToString()).ToArray());
					}
				}

				var InGrupeMengeAufgaabeMitPrio =
					MengeAufgaabeZuGrupiire
					.Where((AufgaabeMitPrio) => (null == FunkZugehörigkaitZuGrupe) ? true : FunkZugehörigkaitZuGrupe(AufgaabeMitPrio))
					.ToArray();

				var GrupeEWarNaame = "EWar[" + GrupeEWarNaameTailMengeEWar + "]";

				var MengeMitNaame = new KeyValuePair<SictAufgaabeInRaumObjektZuBearbaiteMitPrio[], string>(InGrupeMengeAufgaabeMitPrio, GrupeEWarNaame);

				ListeMengeMitNaame.Add(MengeMitNaame);
			}

			var ListeGrupeMitNaame =
				MengeAufgaabeMitPrioGrupiireNaacEnthalteInMengeMitNaame(
				MengeAufgaabeZuGrupiire,
				ListeMengeMitNaame);

			return ListeGrupeMitNaame.ToArray();
		}

		/// <summary>
		/// Berecnet für in RaumObjekt den von EWar abhängigen Antail der Prioritäät der Zersctöörung des Objekt.
		/// </summary>
		/// <param name="InRaumObjekt"></param>
		/// <param name="ListePrioMengeEWarTypeZuZersctööre">Umgekeert sortiirt: Menge bai Index 0 hat hööcste Prioritäät.</param>
		/// <param name="EWarWirkungZuBerüksictigeZaitScrankeMin"></param>
		/// <returns>höhere Rükgaabewert bedoitet hööhere Prioritäät.</returns>
		static int FürInRaumObjektZersctöörungPrioTailEWar(
			SictOverViewObjektZuusctand InRaumObjekt,
			SictEWarTypeEnum[][] ListePrioMengeEWarTypeZuZersctööre,
			Int64? EWarWirkungZuBerüksictigeZaitScrankeMin)
		{
			if (null == InRaumObjekt || null == ListePrioMengeEWarTypeZuZersctööre)
			{
				return 0;
			}

			var DictZuEWarTypeWirkungLezteZait = InRaumObjekt.DictZuEWarTypeWirkungLezteZait;

			if (null == DictZuEWarTypeWirkungLezteZait)
			{
				return 0;
			}

			var MengeEWarTypeBerüksictigt =
				DictZuEWarTypeWirkungLezteZait
				.Where((EWarTypeUndWirkungLezteZait) => !(EWarTypeUndWirkungLezteZait.Value < EWarWirkungZuBerüksictigeZaitScrankeMin))
				.Select((EWarTypeUndWirkungLezteZait) => EWarTypeUndWirkungLezteZait.Key).ToArray();

			var EWarWirkungLezteZait = InRaumObjekt.EWarWirkungLezteZait;

			if (0 < MengeEWarTypeBerüksictigt.Length)
			{
				for (int EWarGrupeIndex = 0; EWarGrupeIndex < ListePrioMengeEWarTypeZuZersctööre.Length; EWarGrupeIndex++)
				{
					var EWarGrupeMengeEWarType = ListePrioMengeEWarTypeZuZersctööre.ElementAtOrDefault(EWarGrupeIndex);

					if (null == EWarGrupeMengeEWarType)
					{
						continue;
					}

					if (!MengeEWarTypeBerüksictigt.Any((EWarTypeWirkungGesictet) => EWarGrupeMengeEWarType.Contains(EWarTypeWirkungGesictet)))
					{
						continue;
					}

					return ListePrioMengeEWarTypeZuZersctööre.Length - EWarGrupeIndex + 1;
				}

				return 1;
			}

			if (EWarWirkungLezteZait.HasValue &&
				!(EWarWirkungLezteZait < EWarWirkungZuBerüksictigeZaitScrankeMin))
			{
				//	Irgendain nict nääher scpezifiziirte EWar wurde gewirkt.
				return 1;
			}

			//	ale EWar Wirkung liigen so lange zurük (vor EWarWirkungZurüksictigenZaitScrankeMin) das diise nict meer berüksictigt wern.
			return 0;
		}

		/// <summary>
		/// Berecnet MengeTargetZuUnLockeMitPrio.
		/// Berecnet MengeObjektZuLockePrioHööcste.
		/// </summary>
		void AktualisiireTailMengeObjektZuUnLocke(
			ISictAutomatZuusctand AutomaatZuusctand,
			VonSensorikMesung AusScnapscusAuswertungZuusctand)
		{
			KeyValuePair<ShipUiTarget, SictAktioonPrioEnum>[] MengeTargetZuUnLockeMitPrio = null;

			try
			{
				if (null == AutomaatZuusctand)
				{
					return;
				}

				var ZaitMili = AutomaatZuusctand.NuzerZaitMili;

				var OptimatParam = AutomaatZuusctand.OptimatParam();

				var AutoMineFraigaabe = (null == OptimatParam) ? null : OptimatParam.AutoMineFraigaabe;

				var Fitting = (null == AutomaatZuusctand) ? null : AutomaatZuusctand.FittingUndShipZuusctand;
				var OverviewUndTarget = (null == AutomaatZuusctand) ? null : AutomaatZuusctand.OverviewUndTarget;

				var AutoMine = (null == AutomaatZuusctand) ? null : AutomaatZuusctand.AutoMine;

				var AutoMineMengeTargetVerwendet = (null == AutoMine) ? null : AutoMine.MengeTargetVerwendet;

				var AnnaameModuleDestruktRangeMax = (null == Fitting) ? null : Fitting.AnnaameModuleDestruktRangeMax;

				var AnnaameDroneCommandRange = (null == Fitting) ? null : Fitting.AnnaameDroneCommandRange;

				var FürTargetDistanceScrankeMax = Bib3.Glob.Max(AnnaameModuleDestruktRangeMax, AnnaameDroneCommandRange);

				var ListePrioGrupeMengeAufgaabeObjektZuBearbaite = this.ListePrioGrupeMengeAufgaabeParam;

				var MengeAufgaabeObjektZuBearbaiteOrdnetNaacPrio =
					(null == ListePrioGrupeMengeAufgaabeObjektZuBearbaite) ? null :
					Bib3.Glob.ArrayAusListeFeldGeflact(ListePrioGrupeMengeAufgaabeObjektZuBearbaite.Select((GrupePrio) => GrupePrio.MengeAufgaabe));

				var MengeAufgaabeDestrukt =
					(null == MengeAufgaabeObjektZuBearbaiteOrdnetNaacPrio) ? null :
					MengeAufgaabeObjektZuBearbaiteOrdnetNaacPrio.Where((Aufgaabe) => true == Aufgaabe.AktioonWirkungDestruktVirt()).ToArray();

				var MengeTarget = (null == OverviewUndTarget) ? null : OverviewUndTarget.MengeTargetNocSictbar;

				var ListeTargetOrdnetNaacDistance =
					(null == MengeTarget) ? null :
					MengeTarget.OrderBy((Target) => Target.SictungLezteDistanceScrankeMaxScpezTarget).ToArray();

				if (null == ListeTargetOrdnetNaacDistance)
				{
					return;
				}

				if (null == Fitting)
				{
					return;
				}

				var AnnaameLockDistanceScrankeMax = Fitting.AnnaameTargetingDistanceScrankeMax;

				if (null == OverviewUndTarget)
				{
					return;
				}

				var MengeInRaumObjekt = OverviewUndTarget.MengeOverViewObjekt;

				if (null == MengeInRaumObjekt)
				{
					return;
				}

				bool MindestensAinZuErhaltendesTargetVorhande = false;

				{
					//	Berecnung MengeObjektZuUnLockeMitPrio

					var InternMengeObjektZuUnLockeMitPrio = new List<KeyValuePair<ShipUiTarget, SictAktioonPrioEnum>>();

					foreach (var Target in ListeTargetOrdnetNaacDistance.Reversed())
					{
						if (null == Target)
						{
							continue;
						}

						var TargetScnapscus = Target.AingangScnapscusTailObjektIdentLezteBerecne();

						if (null == TargetScnapscus)
						{
							continue;
						}

						var AlsTargetErlaubt = false;
						var DistanzHinraicendGering = false;

						if (true == AutoMineFraigaabe && null != AutoMineMengeTargetVerwendet)
						{
							if (AutoMineMengeTargetVerwendet.Any((AutoMineTarget) => AutoMineTarget.Key == Target))
							{
								AlsTargetErlaubt = true;
								DistanzHinraicendGering = true;
							}
						}

						var NurAinTargetÜbrig = ListeTargetOrdnetNaacDistance.Length - 1 <= InternMengeObjektZuUnLockeMitPrio.Count;

						try
						{
							if (null == MengeAufgaabeDestrukt)
							{
								continue;
							}

							var TargetDistanceScrankeMax = Target.SictungLezteDistanceScrankeMaxScpezTarget;

							DistanzHinraicendGering = TargetDistanceScrankeMax <= FürTargetDistanceScrankeMax;

							foreach (var AufgaabeObjektZuZersctööre in MengeAufgaabeDestrukt)
							{
								if (AlsTargetErlaubt)
								{
									break;
								}

								if (null == AufgaabeObjektZuZersctööre)
								{
									continue;
								}

								var ObjektZuZersctööre = AufgaabeObjektZuZersctööre.OverViewObjektZuBearbaiteVirt();

								if (null == ObjektZuZersctööre)
								{
									continue;
								}

								/*
								 * 2014.04.27
								 * Baiscpiil aus:
								 * \\rs211275.rs.hosteurope.de\Optimat.Demo 0 .Berict\Berict\Berict.Nuzer\[ZAK=2014.04.26.21.20.10,NB=6].Anwendung.Berict:
								 * "Wifrerante Vaydaerer" Level2
								 * "The Blockade", "The_Blood_Raider_Covenant"
								 * OverviewZaile.Type:"Starbase Stasis Tower"
								 * OverviewZaile.Name:"Amarr Stasis Tower"
								 * Target.OoberhalbDistanceListeZaile[0]:"Starbase Stasis Tower"
								 * ->
								 * OverviewZaile.Name nict enthalte in Target.OoberhalbDistanceListeZaile
								 *
								var ObjektZuZersctööreNaameOoneLeerzaice = SictOverviewUndTargetZuusctand.StringSictFürVerglaicZwisceTargetUndOverviewName(ObjektZuZersctööre.Name);

								if(TargetReprBescriftungOberhalbDistanceOoneLeerzaice.Contains(ObjektZuZersctööreNaameOoneLeerzaice))
								{
									AlsTargetErlaubt = true;
								}
								 * */

								if (SictOverviewUndTargetZuusctand.OverViewObjektPastZuTarget(ObjektZuZersctööre, Target))
								{
									AlsTargetErlaubt = true;
								}
							}
						}
						finally
						{
							if (AlsTargetErlaubt)
							{
								if (DistanzHinraicendGering || NurAinTargetÜbrig)
								{
									//	Wen nur noc ain Target übrig werd diises troz grööserer Distanz baibehalte.
									MindestensAinZuErhaltendesTargetVorhande = true;
								}
								else
								{
									InternMengeObjektZuUnLockeMitPrio.Add(new KeyValuePair<ShipUiTarget, SictAktioonPrioEnum>(
										TargetScnapscus, SictAktioonPrioEnum.VorWirkung));
								}
							}
							else
							{
								InternMengeObjektZuUnLockeMitPrio.Add(new KeyValuePair<ShipUiTarget, SictAktioonPrioEnum>(
									TargetScnapscus, SictAktioonPrioEnum.VorLock));
							}
						}
					}

					MengeTargetZuUnLockeMitPrio = InternMengeObjektZuUnLockeMitPrio.ToArray();
				}
			}
			finally
			{
				this.MengeTargetZuUnLockeMitPrio = MengeTargetZuUnLockeMitPrio;
			}
		}

		static public Int64? ObjektAufDistanceZuHalteDistanceScrankeMaxBerecneAusObjektAufDistanceZuHalteNääxteDistance(
			Int64? ObjektAufDistanceZuHalteNääxteDistance)
		{
			return ((ObjektAufDistanceZuHalteNääxteDistance + 1600) * 13) / 10;
		}

		void AktualisiireTailMengeObjektZuBearbaite(
			ISictAutomatZuusctand AutomaatZuusctand,
			VonSensorikMesung AusScnapscusAuswertungZuusctand)
		{
			VonSensor.InventoryItem AusInventoryItemZuÜbertraageNaacActiveShip = null;
			var ListePrioGrupeMengeAufgaabeObjektZuBearbaite = new List<SictAufgaabeGrupePrio>();
			SictDamageMitBetraagIntValue[] FürGefectListeDamageTypePrio = null;
			SictOverViewObjektZuusctand WirkungDestruktDroneTargetNääxteOverviewObj = null;
			bool? GefectModusAngraifendeObjekteAufDistanzHalte = null;
			Int64? MengeObjektZuDestruiireNääxteDistance = null;

			var FürGefectAufgaabeManööverErgeebnis = this.FürGefectAufgaabeManööverErgeebnis;

			try
			{
				if (null == AutomaatZuusctand)
				{
					return;
				}

				var ZaitMili = AutomaatZuusctand.NuzerZaitMili;

				var OptimatParam = AutomaatZuusctand.OptimatParam();
				var FittingUndShipZuusctand = AutomaatZuusctand.FittingUndShipZuusctand;
				var OverviewUndTarget = AutomaatZuusctand.OverviewUndTarget;
				var AutoMine = AutomaatZuusctand.AutoMine;
				var AgentUndMission = AutomaatZuusctand.AgentUndMission;

				var AnforderungActiveShipCargoLeereLezteZaitMili = AutomaatZuusctand.AnforderungActiveShipCargoGeneralLeereLezteZaitMili;
				var AnnaameActiveShipCargoLeerLezteZaitMili = AutomaatZuusctand.AnnaameActiveShipCargoGeneralLeerLezteZaitMili;

				var InRaumAktioonFortsaz = this.InRaumAktioonFortsaz;

				var MengeOverviewObjektGrupeMesungZuErsctele = this.MengeOverviewObjektGrupeMesungZuErsctele;

				var ShipZuusctandLezte = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.ShipZuusctand;

				var SelbsctShipDocked = (null == ShipZuusctandLezte) ? null : (bool?)ShipZuusctandLezte.Docked;

				var ListeAusShipUIIndicationMitZait = (null == AutomaatZuusctand) ? null : AutomaatZuusctand.ListeAusShipUIIndicationMitZait;

				var ListeAusShipUIIndicationMitZaitLezte = (null == ListeAusShipUIIndicationMitZait) ? null : ListeAusShipUIIndicationMitZait.LastOrDefault();

				var VonNuzerParamSimu = (null == OptimatParam) ? null : OptimatParam.SimuNaacBerüksictigungFraigaabeBerecne();

				var AutoPilotFraigaabe = (null == OptimatParam) ? null : OptimatParam.AutoPilotFraigaabe;
				var AutoMineFraigaabe = (null == OptimatParam) ? null : OptimatParam.AutoMineFraigaabe;
				var AutoMissionFraigaabe = (null == OptimatParam) ? null : OptimatParam.AutoMissionFraigaabe;

				var ShipMengeModule = AutomaatZuusctand.ShipMengeModule();

				var ShipMengeModuleMitInfo =
					ShipMengeModule
					.WhereNullable((Kandidaat) => null != Kandidaat.ModuleButtonHintGültigMitZait.Wert)
					.ToArrayNullable();

				var MengeModuleReprHardener =
					(null == FittingUndShipZuusctand) ? null :
					FittingUndShipZuusctand.MengeModuleReprHardenerBerecne().ToArrayNullable();

				var MengeModuleReprWirkmitelDestrukt =
					(null == FittingUndShipZuusctand) ? null :
					FittingUndShipZuusctand.MengeModuleReprWirkmitelDestruktBerecne().ToArrayNullable();

				var AnnaameModuleShieldBoosterSelbsct =
					(null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.AnnaameModuleShieldBoosterSelbsct;

				var AnnaameModuleArmorRepairerSelbsct =
					(null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.AnnaameModuleArmorRepairerSelbsct;

				var AnnaameLockDistanceScrankeMax = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.AnnaameTargetingDistanceScrankeMax;

				var AnnaameModuleDestruktRangeMax = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.AnnaameModuleDestruktRangeMax;
				var AnnaameModuleDestruktRangeOptimumNulbar = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.AnnaameModuleDestruktRangeOptimum;

				var AnnaameGefectDistanzOptimum = AnnaameModuleDestruktRangeOptimumNulbar ?? 10000;

				var ListePrioMengeEWarTypeZuZersctööre = this.ListePrioMengeEWarTypeZuZersctööre;

				var GefectUnabhängigVonBeweegungFraigaabe = this.GefectUnabhängigVonBeweegungFraigaabe;

				var MengeOverViewObjekt = (null == OverviewUndTarget) ? null : OverviewUndTarget.MengeOverViewObjekt;

				var AnforderungLeereCargoGeneral =
					(true == SelbsctShipDocked) &&
					AnforderungActiveShipCargoLeereLezteZaitMili.HasValue &&
					!(AnforderungActiveShipCargoLeereLezteZaitMili < AnnaameActiveShipCargoLeerLezteZaitMili);

				var OverviewTabAktiivBezaicner = (null == OverviewUndTarget) ? null : OverviewUndTarget.OverviewTabAktiivBezaicner;
				var OverviewProfileAktiivBezaicnerUndMengeObjektGrupeSictbarNulbar = (null == OverviewUndTarget) ? null :
					OverviewUndTarget.OverviewPresetAktiivIdentUndMengeObjektGrupeSictbar;

				var MengeOverViewObjektSictbar =
					(null == MengeOverViewObjekt) ? null :
					MengeOverViewObjekt.Where((OverViewObjekt) => ZaitMili <= OverViewObjekt.SictungLezteZait).ToArray();

				var MengeTarget = (null == OverviewUndTarget) ? null : OverviewUndTarget.MengeTargetNocSictbar;

				var TargetAktiiv = (null == MengeTarget) ? null : MengeTarget.FirstOrDefault((Target) => true == Target.InputFookusTransitioonLezteZiilWert);

				var MengeInRaumObjektAttackingMe =
					(null == MengeOverViewObjekt) ? null :
					MengeOverViewObjekt.Where((InRaumObjekt) => true == InRaumObjekt.AttackingMe).ToArray();

				var AusScnapscusGbsWindowOverview =
					(null == AusScnapscusAuswertungZuusctand) ? null :
					AusScnapscusAuswertungZuusctand.WindowOverview;

				var MissionAktuel = (null == AgentUndMission) ? null : AgentUndMission.MissionAktuel;
				var MissionAnforderungInRaum = (null == MissionAktuel) ? null : MissionAktuel.NaacAutomaatAnforderungInRaum;

				var MissionAktuelZiilLocationNääxteAuswert = (null == MissionAktuel) ? null : MissionAktuel.ZiilLocationNääxteAuswert;

				var MissionAktuelZiilLocationNääxteErraict =
					(null == MissionAktuelZiilLocationNääxteAuswert) ? null : MissionAktuelZiilLocationNääxteAuswert.LocationErraict;

				var MissionAktuelStrategikon = (null == MissionAktuel) ? null : MissionAktuel.Strategikon;

				var MissionAktuelStrategikonAnnaameTank =
					(null == MissionAktuelStrategikon) ? null : MissionAktuelStrategikon.AnnaameTank;

				var MissionAktuelListeMesungObjectiveZuusctandLezteMitZaitNulbar =
					(null == MissionAktuel) ? null :
					MissionAktuel.ListeMesungObjectiveZuusctandZuZaitLezteBerecne();

				var MissionAktuelListeMesungObjectiveZuusctandLezte =
					MissionAktuelListeMesungObjectiveZuusctandLezteMitZaitNulbar.HasValue ?
					MissionAktuelListeMesungObjectiveZuusctandLezteMitZaitNulbar.Value.Wert : null;

				var MissionAktuelListeMesungObjectiveZuusctandLezteObjective =
					(null == MissionAktuelListeMesungObjectiveZuusctandLezte) ? null :
					MissionAktuelListeMesungObjectiveZuusctandLezte.Objective;

				var MissionAktuelListeMesungObjectiveZuusctandLezteObjectiveComplete =
					(null == MissionAktuelListeMesungObjectiveZuusctandLezteObjective) ? null :
					MissionAktuelListeMesungObjectiveZuusctandLezteObjective.Complete;

				var MissionAnforderungInRaumMengeObjektZuBearbaiteMitPrio =
					(null == MissionAnforderungInRaum) ? null : MissionAnforderungInRaum.MengeObjektZuBearbaiteMitPrio;

				if (null != MissionAktuelStrategikonAnnaameTank)
				{
					FürGefectListeDamageTypePrio = MissionAktuelStrategikonAnnaameTank;
				}

				var MengeObjektZuBearbaiteMitPrio = new List<SictAufgaabeInRaumObjektZuBearbaiteMitPrio>();

				if (null != MissionAnforderungInRaumMengeObjektZuBearbaiteMitPrio &&
					!(true == MissionAktuelListeMesungObjectiveZuusctandLezteObjectiveComplete) &&
					!(false == InRaumAktioonFortsaz))
				{
					MengeObjektZuBearbaiteMitPrio.AddRange(MissionAnforderungInRaumMengeObjektZuBearbaiteMitPrio);
				}

				var SelbsctShipWarpScrambled = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.SelbsctShipWarpScrambled;

				if (!(false == InRaumAktioonFortsaz) &&
					null != VonNuzerParamSimu)
				{
					var OverViewObjektNääxte = (null == MengeOverViewObjekt) ? null : MengeOverViewObjekt.OrderBy((Objekt) => Objekt.SictungLezteDistanceScrankeMaxScpezOverview).FirstOrDefault();

					if (true == VonNuzerParamSimu.AufgaabeDistanceScteleAinObjektNääxteFraigaabe)
					{
						var Aufgaabe = new SictAufgaabeInRaumObjektZuBearbaiteMitPrio(
							AufgaabeParamAndere.AufgaabeDistanceAinzusctele(
							OverViewObjektNääxte,
							VonNuzerParamSimu.AufgaabeDistanceScteleAinObjektNääxteDistanceSol,
							VonNuzerParamSimu.AufgaabeDistanceScteleAinObjektNääxteDistanceSol));

						MengeObjektZuBearbaiteMitPrio.Add(Aufgaabe);
					}
				}

				GefectModusAngraifendeObjekteAufDistanzHalte =
					!(true == GefectUnabhängigVonBeweegungFraigaabe) &&
					!(false == InRaumAktioonFortsaz) &&
					0 < ExtractFromOldAssembly.Bib3.Extension.CountNullable(MengeInRaumObjektAttackingMe);

				if (null != MengeInRaumObjektAttackingMe &&
					!(false == InRaumAktioonFortsaz))
				{
					//	Fals selbsct Ship geWarpScrambled oder von Mission Aktuel nict andere Location angeflooge werde sol werden angraifende Objekte als zu zersctööre aingetraage.

					var MengeInRaumObjektAttackingMeZuBearbaiteMitPrio =
						MengeInRaumObjektAttackingMe
						.Select((InRaumObjektAttackingMe) =>
							new SictAufgaabeInRaumObjektZuBearbaiteMitPrio(new AufgaabeParamDestrukt(InRaumObjektAttackingMe)))
						.ToArray();

					MengeObjektZuBearbaiteMitPrio.AddRange(MengeInRaumObjektAttackingMeZuBearbaiteMitPrio);
				}

				if (null != AnnaameModuleShieldBoosterSelbsct)
				{
					SictAufgaabeParam ShieldBoosterAufgaabeParam = null;

					if (true == AnforderungShieldBoosterLezte)
					{
						ShieldBoosterAufgaabeParam = AufgaabeParamAndere.KonstruktModuleScalteAin(AnnaameModuleShieldBoosterSelbsct);
					}
					else
					{
						ShieldBoosterAufgaabeParam = AufgaabeParamAndere.KonstruktModuleScalteAus(AnnaameModuleShieldBoosterSelbsct);
					}

					ListePrioGrupeMengeAufgaabeObjektZuBearbaite.Add(new SictAufgaabeGrupePrio(
						new SictAufgaabeParam[] { ShieldBoosterAufgaabeParam },
						"Shield Booster"));
				}

				if (null != AnnaameModuleArmorRepairerSelbsct)
				{
					SictAufgaabeParam ArmorRepairerAufgaabeParam = null;

					if (true == AnforderungArmorRepairerLezte)
					{
						ArmorRepairerAufgaabeParam = AufgaabeParamAndere.KonstruktModuleScalteAin(AnnaameModuleArmorRepairerSelbsct);
					}
					else
					{
						ArmorRepairerAufgaabeParam = AufgaabeParamAndere.KonstruktModuleScalteAus(AnnaameModuleArmorRepairerSelbsct);
					}

					ListePrioGrupeMengeAufgaabeObjektZuBearbaite.Add(new SictAufgaabeGrupePrio(
						new SictAufgaabeParam[] { ArmorRepairerAufgaabeParam },
						"Armor Repairer"));
				}

				if (true == GefectModusAngraifendeObjekteAufDistanzHalte)
				{
					//	OverviewTab aktiviire welces di für Gefect benöötigte Objekte und ansonste mööglicst weenig Objekte enthalt.

					var AnforderungMengeGrupeSictbar = new SictOverviewObjektGrupeEnum[]{
							SictOverviewObjektGrupeEnum.Rat};

					/*
					 * 2015.00.04
					 * 
						var OverviewTabZuAktiviire =
							ZuBevorzugendeOverviewPresetBerecneFürAnforderungMengeGrupeSictbar(
							AnforderungMengeGrupeSictbar,
							MengeZuOverviewTabMengeObjektGrupeSictbar);

						if (null == OverviewTabZuAktiviire)
						{
						}
						else
						{
							ListePrioGrupeMengeAufgaabeObjektZuBearbaite.Add(new SictAufgaabeGrupePrio(
								AufgaabeParamAndere.KonstruktOverviewTabAktiviire(OverviewTabZuAktiviire, new	string[]{
									"activate Overview Tab best suited for combat"}),
									"Overview Tab für Gefect"));
						}
					 * */
					ListePrioGrupeMengeAufgaabeObjektZuBearbaite.Add(new SictAufgaabeGrupePrio(
						AufgaabeParamAndere.KonstruktAufgaabeParam(
						new AufgaabeParamOverviewPresetLaadeNaacVorgeseheneTab(OverviewPresetDefaultTyp.General),
						"activate Overview Tab best suited for combat"),
							"Overview Tab für Gefect"));
				}

				var EWarZuBerüksictigeZaitScrankeMin = ZaitMili - 1000 * 60 * 5;

				var MissionListeGrupePrio = new KeyValuePair<SictAufgaabeInRaumObjektZuBearbaiteMitPrio[], string>[]{
					new KeyValuePair<SictAufgaabeInRaumObjektZuBearbaiteMitPrio[],  string>(
						MissionAnforderungInRaumMengeObjektZuBearbaiteMitPrio,  "Mission"),
				};

				if (true == AnforderungDroneReturnLezte)
				{
					//	Drone Return

					ListePrioGrupeMengeAufgaabeObjektZuBearbaite.Add(new SictAufgaabeGrupePrio(
						new SictAufgaabeParam[] { AufgaabeParamAndere.KonstruktDronesReturn() }, "Drone Return"));
				}

				var MengeAufgaabeObjektZuBearbaiteGrupiirungPrioKaskaadeListeFunk =
					new Func<IEnumerable<SictAufgaabeInRaumObjektZuBearbaiteMitPrio>, SictAufgaabeGrupePrio[]>[]{
						MengeAufgaabeMitPrioGrupiireNaacFürGefectReegelungDistance,
						(MengeAufgaabe) => MengeAufgaabeMitPrioGrupiireNaacEWar(MengeAufgaabe, ListePrioMengeEWarTypeZuZersctööre, EWarZuBerüksictigeZaitScrankeMin),
						(MengeAufgaabe) => MengeAufgaabeMitPrioGrupiireNaacEnthalteInMengeMitNaame(MengeAufgaabe,   MissionListeGrupePrio),
						MengeAufgaabeMitPrioGrupiireNaacInGrupeIndex,
					};

				if (null != GefectAngraifendeHalteAufDistanceLezteErgeebnisZuZait.Wert)
				{
					var GefectAngraifendeHalteAufDistanceLezteListeAufgaabeAufDistanceZuHalteParam =
						GefectAngraifendeHalteAufDistanceLezteErgeebnisZuZait.Wert.ListeAufgaabeAufDistanceZuHalteParam;

					var MengeAufgaabeZuusctand = AutomaatZuusctand.MengeAufgaabeZuusctand;

					var MengeKandidaatFürGefectAufgaabeManööverErgeebnis = new List<SictFürGefectAufgaabeManööverErgeebnis>();

					if (null != MengeAufgaabeZuusctand &&
						!Bib3.Extension.NullOderLeer(GefectAngraifendeHalteAufDistanceLezteListeAufgaabeAufDistanceZuHalteParam))
					{
						foreach (var KandidaatAufgaabe in MengeAufgaabeZuusctand)
						{
							var KandidaatAufgaabeParam = KandidaatAufgaabe.AufgaabeParam;

							var AufgaabeManööverErgeebnis = KandidaatAufgaabe.ManööverErgeebnis;

							if (null == AufgaabeManööverErgeebnis)
							{
								continue;
							}

							if (null == KandidaatAufgaabeParam)
							{
								continue;
							}

							var KandidaatAufgaabeParamOverViewObjektZuBearbaite = KandidaatAufgaabeParam.OverViewObjektZuBearbaiteVirt();

							if (null == KandidaatAufgaabeParamOverViewObjektZuBearbaite)
							{
								continue;
							}

							if (!KandidaatAufgaabeParam.DistanzAinzuscteleScrankeMinVirt().HasValue)
							{
								continue;
							}

							if (!GefectAngraifendeHalteAufDistanceLezteListeAufgaabeAufDistanceZuHalteParam.Any((GefectAngraifendeHalteAufDistanceAufgaabeParam) =>
								KandidaatAufgaabeParam.OverViewObjektZuBearbaiteVirt() == GefectAngraifendeHalteAufDistanceAufgaabeParam.OverViewObjektZuBearbaiteVirt()))
							{
								continue;
							}

							MengeKandidaatFürGefectAufgaabeManööverErgeebnis.Add(
								new SictFürGefectAufgaabeManööverErgeebnis(
									KandidaatAufgaabeParam,
									AufgaabeManööverErgeebnis));
						}

						var ListeKandidaatFürGefectAufgaabeManööverErgeebnis =
							MengeKandidaatFürGefectAufgaabeManööverErgeebnis
							.OrderBy((Kandidaat) => Kandidaat.ShipUIIndication.EndeZait ?? Int64.MaxValue)
							.ToArray();

						if (!Bib3.Extension.NullOderLeer(MengeKandidaatFürGefectAufgaabeManööverErgeebnis))
						{
							FürGefectAufgaabeManööverErgeebnis =
								ListeKandidaatFürGefectAufgaabeManööverErgeebnis
								.FirstOrDefault();
						}
					}
				}

				if (true == GefectModusAngraifendeObjekteAufDistanzHalte)
				{
					//	Angraifende Objekte auf Distanz halte

					int AinfüügePrioGrupeIndex = 0;
					SictOverViewObjektZuusctand[] MengeObjektAufDistanceZuHalte = null;

					for (int PrioGrupeIndex = 0; PrioGrupeIndex < ListePrioGrupeMengeAufgaabeObjektZuBearbaite.Count; PrioGrupeIndex++)
					{
						var PrioGrupe = ListePrioGrupeMengeAufgaabeObjektZuBearbaite[PrioGrupeIndex];

						if (null == PrioGrupe)
						{
							continue;
						}

						var PrioGrupeMengeAufgaabe = PrioGrupe.MengeAufgaabe;

						if (null == PrioGrupeMengeAufgaabe)
						{
							continue;
						}

						var PrioGrupeMengeAufgaabeDestrukt =
							PrioGrupeMengeAufgaabe
							.Where((KandidaatAufgaabe) => true == KandidaatAufgaabe.AktioonWirkungDestruktVirt())
							.ToArray();

						var PrioGrupeMengeAufgaabeDestruktOverviewObjekt =
							PrioGrupeMengeAufgaabeDestrukt
							.Select((AufgaabeParam) => AufgaabeParam.OverViewObjektZuBearbaiteVirt())
							.Where((t) => null != t)
							.ToArray();

						if (0 < PrioGrupeMengeAufgaabeDestruktOverviewObjekt.Length)
						{
							MengeObjektAufDistanceZuHalte = PrioGrupeMengeAufgaabeDestruktOverviewObjekt;
							break;
						}
					}

					if (Bib3.Extension.NullOderLeer(MengeObjektAufDistanceZuHalte) &&
						!Bib3.Extension.NullOderLeer(MengeInRaumObjektAttackingMe))
					{
						MengeObjektAufDistanceZuHalte = MengeInRaumObjektAttackingMe;
					}

					//	!!!!	Vorersct Bedingung auf Distance halte nur fals näher als 26km da vorersct nur üwer Orbit->30km uf Distance gehalte werd.
					var ObjektAufDistanceZuHalteBeginDistanceScrankeMaxKonstant = 26000;
					var ObjektAufDistanceZuHalteFortsazDistanceScrankeMaxKonstant = ObjektAufDistanceZuHalteBeginDistanceScrankeMaxKonstant + 5000;

					if (!Bib3.Extension.NullOderLeer(MengeObjektAufDistanceZuHalte))
					{
						var ObjektAufDistanceZuHalteNääxteDistance =
							Bib3.Glob.Min(
							MengeObjektAufDistanceZuHalte
							.Select((ObjektAufDistanceZuHalte) => ObjektAufDistanceZuHalte.SictungLezteDistanceScrankeMinScpezOverview));

						var ObjektAufDistanceZuHalteDistanceScrankeMax =
							Bib3.Glob.Min(
							AnnaameGefectDistanzOptimum,
							ObjektAufDistanceZuHalteDistanceScrankeMaxBerecneAusObjektAufDistanceZuHalteNääxteDistance(ObjektAufDistanceZuHalteNääxteDistance));

						var ListeObjektAufDistanceZuHalteFortsaz =
							MengeObjektAufDistanceZuHalte
							.Where((Kandidaat) => Kandidaat.SictungLezteDistanceScrankeMinScpezOverview <
								Bib3.Glob.Min(ObjektAufDistanceZuHalteFortsazDistanceScrankeMaxKonstant, ObjektAufDistanceZuHalteDistanceScrankeMax))
							.OrderBy((ObjektAufDistanceZuHalte) => ObjektAufDistanceZuHalte.SictungLezteDistanceScrankeMinScpezOverview)
							.ToArray();

						var ListeObjektAufDistanceZuHalteBegin =
							ListeObjektAufDistanceZuHalteFortsaz
							.Where((Kandidaat) => Kandidaat.SictungLezteDistanceScrankeMinScpezOverview <
								Bib3.Glob.Min(ObjektAufDistanceZuHalteBeginDistanceScrankeMaxKonstant, ObjektAufDistanceZuHalteDistanceScrankeMax))
							.ToArray();

						if (!Bib3.Extension.NullOderLeer(ListeObjektAufDistanceZuHalteFortsaz))
						{
							bool AufgaabeAufDistanceZuHalteBeraitsErleedigt = false;
							bool AufgaabeAufDistanceZuHalteBeginNaacrangig = false;

							if (null != FürGefectAufgaabeManööverErgeebnis)
							{
								var GefectAngraifendeHalteAufDistanceAufgaabeManööverErgeebnisBeginAlterMili =
									ZaitMili - FürGefectAufgaabeManööverErgeebnis.ShipUIIndication.BeginZait;

								var GefectAngraifendeHalteAufDistanceAufgaabeManööverErgeebnisEndeAlterMili =
									ZaitMili - FürGefectAufgaabeManööverErgeebnis.ShipUIIndication.EndeZait;

								if ((GefectAngraifendeHalteAufDistanceAufgaabeManööverErgeebnisBeginAlterMili / 1000) < 40 &&
									null != FürGefectAufgaabeManööverErgeebnis.OverViewObjekt)
								{
									if (FürGefectAufgaabeManööverErgeebnis.OverViewObjekt.SictungLezteDistanceScrankeMinScpezOverview <
										ObjektAufDistanceZuHalteDistanceScrankeMax)
									{
										if (!(7000 < GefectAngraifendeHalteAufDistanceAufgaabeManööverErgeebnisEndeAlterMili))
										{
											AufgaabeAufDistanceZuHalteBeraitsErleedigt = true;

											if (GefectAngraifendeHalteAufDistanceAufgaabeManööverErgeebnisEndeAlterMili.HasValue)
											{
												AufgaabeAufDistanceZuHalteBeginNaacrangig = true;
											}
										}
									}
								}
							}

							var ListeObjektAufDistanceZuHalteAufgaabeParamMitPrio = new List<SictAufgaabeInRaumObjektZuBearbaiteMitPrio>();

							var AngraifendeAufDistanzZuHalteManööverPrioVorrangig = new SictInRaumObjektBearbaitungPrio(InGrupeFürGefectAngraiferAufDistanzHalte: true);
							var AngraifendeAufDistanzZuHalteManööverPrioNaacrangig = SictInRaumObjektBearbaitungPrio.KonstruktFürInGrupeIndex(-1);

							if (AufgaabeAufDistanceZuHalteBeraitsErleedigt)
							{
								ListeObjektAufDistanceZuHalteAufgaabeParamMitPrio.Add(
									new SictAufgaabeInRaumObjektZuBearbaiteMitPrio(
									AufgaabeParamAndere.KonstruktManööverUnterbreceNict(
									FürGefectAufgaabeManööverErgeebnis.ShipUIIndication),
									AngraifendeAufDistanzZuHalteManööverPrioVorrangig));
							}

							if (!AufgaabeAufDistanceZuHalteBeraitsErleedigt ||
								AufgaabeAufDistanceZuHalteBeginNaacrangig)
							{
								var ManööverBeginPrio =
									AufgaabeAufDistanceZuHalteBeginNaacrangig ?
									AngraifendeAufDistanzZuHalteManööverPrioNaacrangig :
									AngraifendeAufDistanzZuHalteManööverPrioVorrangig;

								if (!Bib3.Extension.NullOderLeer(ListeObjektAufDistanceZuHalteBegin))
								{
									var ListeObjektAufDistanceZuHalteAufgaabeParam =
										ListeObjektAufDistanceZuHalteBegin
										.Select((ObjektAufDistanceZuHalte) =>
											AufgaabeParamAndere.AufgaabeDistanceAinzusctele(ObjektAufDistanceZuHalte, AnnaameGefectDistanzOptimum * 9 / 10 - 1000, AnnaameGefectDistanzOptimum))
										.OfType<SictAufgaabeParam>()
										.ToList();

									ListeObjektAufDistanceZuHalteAufgaabeParamMitPrio.AddRange(
										ListeObjektAufDistanceZuHalteAufgaabeParam
										.Select((AufgaabeParam) => new SictAufgaabeInRaumObjektZuBearbaiteMitPrio(AufgaabeParam, ManööverBeginPrio)));

									if (AufgaabeAufDistanceZuHalteBeraitsErleedigt)
									{
										ListeObjektAufDistanceZuHalteAufgaabeParam.Add(FürGefectAufgaabeManööverErgeebnis.AufgaabeParam);
									}

									GefectAngraifendeHalteAufDistanceLezteErgeebnisZuZait =
									new SictWertMitZait<SictGefectAngraifendeHalteAufDistanceScritErgeebnis>(
									ZaitMili, new SictGefectAngraifendeHalteAufDistanceScritErgeebnis(
										ObjektAufDistanceZuHalteDistanceScrankeMax,
										ListeObjektAufDistanceZuHalteAufgaabeParam.ToArray(),
										AufgaabeAufDistanceZuHalteBeginNaacrangig));
								}
							}

							MengeObjektZuBearbaiteMitPrio.AddRange(ListeObjektAufDistanceZuHalteAufgaabeParamMitPrio);
						}
					}
				}

				ListePrioGrupeMengeAufgaabeObjektZuBearbaite.AddRange(
					MengeAufgaabeMitPrioGrupiireMitKaskaadeFunkGrupiirung(
					MengeObjektZuBearbaiteMitPrio,
					MengeAufgaabeObjektZuBearbaiteGrupiirungPrioKaskaadeListeFunk));

				var GbsMengeMenuMitBeginZait = AutomaatZuusctand.GbsListeMenuNocOfeMitBeginZaitBerecne();

				{
					//	Ale Hardener ainscalte

					var ModuleScalteAinMengeAufgaabe = new List<SictAufgaabeParam>();

					if (null != MengeModuleReprHardener)
					{
						foreach (var ModuleRepr in MengeModuleReprHardener)
						{
							//	Module mus für mindesctens drai Scrit inaktiiv geweese sain bevor ainscalte versuuct werd.
							var AnnaameModuleAktiiv = ModuleRepr.AktiivBerecne(2);

							if (!AnnaameModuleAktiiv)
							{
								ModuleScalteAinMengeAufgaabe.Add(AufgaabeParamAndere.KonstruktModuleScalteAin(ModuleRepr));
							}
						}
					}

					if (0 < ModuleScalteAinMengeAufgaabe.Count)
					{
						var HardenerScalteAinGrupePrio = new SictAufgaabeGrupePrio(ModuleScalteAinMengeAufgaabe.ToArray(), "Module[Hardener].scalte[ain]");

						ListePrioGrupeMengeAufgaabeObjektZuBearbaite.Add(HardenerScalteAinGrupePrio);
					}
				}

				{
					//	Leere Cargo

					if (AnforderungLeereCargoGeneral)
					{
						ListePrioGrupeMengeAufgaabeObjektZuBearbaite.Add(
							new SictAufgaabeGrupePrio(new AufgaabeParamShipAktuelCargoLeereTyp(SictShipCargoTypSictEnum.General), "Leere Cargo"));
					}
				}

				{
					if (true == AutoMineFraigaabe &&
						null != AutoMine)
					{
						var FürMineListeAufgaabeNääxteParam =
							AutoMine.FürMineListeAufgaabeNääxteParamBerecne(AutomaatZuusctand)
							.ToArrayNullable();

						if (!Bib3.Extension.NullOderLeer(FürMineListeAufgaabeNääxteParam))
						{
							var AusMineGrupePrio = new SictAufgaabeGrupePrio(FürMineListeAufgaabeNääxteParam, "aus Auto.Mine");

							ListePrioGrupeMengeAufgaabeObjektZuBearbaite.Add(AusMineGrupePrio);
						}
					}
				}

				{
					//	Aus Mission Aufgaabe

					if (true == AutoMissionFraigaabe)
					{
						var FürMissionListeAufgaabeNääxteParam =
							AgentUndMission.FürMissionListeAufgaabeNääxteParamBerecne(AutomaatZuusctand)
							.ToArrayNullable();

						if (!Bib3.Extension.NullOderLeer(FürMissionListeAufgaabeNääxteParam))
						{
							var AusMissionGrupePrio = new SictAufgaabeGrupePrio(FürMissionListeAufgaabeNääxteParam, "aus Mission direkt");

							ListePrioGrupeMengeAufgaabeObjektZuBearbaite.Add(AusMissionGrupePrio);
						}
					}
				}

				{
					if (true == AutoPilotFraigaabe)
					{
						var InfoPanelRouteGrupePrio = new SictAufgaabeGrupePrio(new SictAufgaabeParam[] {
							new AufgaabeParamInfoPanelRouteFüüreAus(OptimatParam.AutoPilotLowSecFraigaabe   ?? false) }, "Info Panel Route");

						ListePrioGrupeMengeAufgaabeObjektZuBearbaite.Add(InfoPanelRouteGrupePrio);
					}
				}

				{
					if (0 < MengeOverviewObjektGrupeMesungZuErsctele?.Length)
					{
						var OverviewObjektGrupeTabGrupePrio = new SictAufgaabeGrupePrio(
							new AufgaabeParamMengeOverviewObjGrupeMesung(MengeOverviewObjektGrupeMesungZuErsctele), "Overview Objekt Grupe mese");

						ListePrioGrupeMengeAufgaabeObjektZuBearbaite.Add(OverviewObjektGrupeTabGrupePrio);
					}
				}

				if (null != MengeObjektZuBearbaiteMitPrio)
				{
					foreach (var AufgaabeMitPrio in MengeObjektZuBearbaiteMitPrio)
					{
						if (null == AufgaabeMitPrio)
						{
							continue;
						}

						var AufgaabeParam = AufgaabeMitPrio.AufgaabeObjektZuBearbaite;

						if (null == AufgaabeParam)
						{
							continue;
						}

						var AufgaabeParamTarget = AufgaabeParam.TargetZuBearbaiteVirt();
						var AufgaabeParamOverViewObjektZuBearbaite = AufgaabeParam.OverViewObjektZuBearbaiteVirt();

						if (!(true == AufgaabeParam.AktioonWirkungDestruktVirt()))
						{
							continue;
						}

						if (null != AufgaabeParamTarget)
						{
							MengeObjektZuDestruiireNääxteDistance = Bib3.Glob.Min(MengeObjektZuDestruiireNääxteDistance, AufgaabeParamTarget.SictungLezteDistanceScrankeMaxScpezTarget);
						}

						if (null != AufgaabeParamOverViewObjektZuBearbaite)
						{
							MengeObjektZuDestruiireNääxteDistance = Bib3.Glob.Min(MengeObjektZuDestruiireNääxteDistance, AufgaabeParamOverViewObjektZuBearbaite.SictungLezteDistanceScrankeMaxScpezOverview);
						}
					}
				}

				{
					var MesungModuleButtonHintMengeAufgaabe = new List<SictAufgaabeParam>();

					//	Info zu ale Module mese aus ModuleButtonHint

					var AusShipUiMengeModuleReprOrdnetFürMesungInfo =
						ShipMengeModule
						.OrderByNullable((ModuleRepr) => (ModuleRepr.ListeLaageMitZaitLezteBerecne() ?? new SictWertMitZait<Vektor2DInt>()).Wert.A)
						.ToArrayNullable();

					if (null != AusShipUiMengeModuleReprOrdnetFürMesungInfo)
					{
						foreach (var ModuleRepr in AusShipUiMengeModuleReprOrdnetFürMesungInfo)
						{
							if (null == ModuleRepr)
							{
								continue;
							}

							if (!ModuleRepr.SictbarBerecne())
							{
								continue;
							}

							var ModuleButtonFürMenuWurzelFläce = ModuleRepr.ToggleFläceBerecne();

							var ModuleButtonHintGültig = ModuleRepr.ModuleButtonHintGültigMitZait;

							var FittingZuSlotInfoAlterMili = ZaitMili - ModuleButtonHintGültig.Zait;

							if (null != ModuleButtonHintGültig.Wert)
							{
								var AlterScranke = 900;

								if (!(
									(true == ModuleButtonHintGültig.Wert.IstAfterburner) ||
									(true == ModuleButtonHintGültig.Wert.IstHardener) ||
									(true == ModuleButtonHintGültig.Wert.IstShieldBoosterSelbsct) ||
									(true == ModuleButtonHintGültig.Wert.IstArmorRepairerSelbsct) ||
									(true == ModuleButtonHintGültig.Wert.IstTargetPainter) ||
									(true == ModuleButtonHintGültig.Wert.IstWirkmitelDestrukt) ||
									(true == ModuleButtonHintGültig.Wert.IstMiner) ||
									(true == ModuleButtonHintGültig.Wert.IstSurveyScanner)))
								{
									AlterScranke = AlterScranke / 2;
								}

								if (FittingZuSlotInfoAlterMili < AlterScranke * 1e+3)
								{
									//	Zu diisem Slot scaint Info beraits vorhande.
									continue;
								}
							}

							MesungModuleButtonHintMengeAufgaabe.Add(AufgaabeParamAndere.KonstruktModuleMesungModuleButtonHint(ModuleRepr));
						}
					}

					if (0 < MesungModuleButtonHintMengeAufgaabe.Count)
					{
						var MesungModuleButtonHintGrupePrio = new SictAufgaabeGrupePrio(MesungModuleButtonHintMengeAufgaabe.ToArray(), "Mesung ModuleButtonHint");

						ListePrioGrupeMengeAufgaabeObjektZuBearbaite.Add(MesungModuleButtonHintGrupePrio);
					}
				}

				var PrädikaatInRaumObjektInRaicwaite = new Func<SictOverViewObjektZuusctand, bool>((InRaumObjekt) => InRaumObjekt.SictungLezteDistanceScrankeMaxScpezOverview <= AnnaameGefectDistanzOptimum);

				var MissionMesungObjectiveZuusctandLezteMitZaitNulbar =
					(null == MissionAktuel) ? null :
					MissionAktuel.ListeMesungObjectiveZuusctandZuZaitLezteBerecne();

				var MissionMesungObjectiveZuusctandLezte =
					MissionMesungObjectiveZuusctandLezteMitZaitNulbar.HasValue ?
					MissionMesungObjectiveZuusctandLezteMitZaitNulbar.Value.Wert : null;

				var MissionMesungObjectiveZuusctandLezteObjective =
					(null == MissionMesungObjectiveZuusctandLezte) ? null : MissionMesungObjectiveZuusctandLezte.Objective;

				var MissionMesungObjectiveZuusctandLezteObjectiveMengeAst =
					(null == MissionMesungObjectiveZuusctandLezteObjective) ? null : MissionMesungObjectiveZuusctandLezteObjective.MengeObjectiveTransitiveHüleBerecne();

				var MissionMesungObjectiveZuusctandLezteObjectiveMengeAstItem =
					(null == MissionMesungObjectiveZuusctandLezteObjectiveMengeAst) ? null :
					MissionMesungObjectiveZuusctandLezteObjectiveMengeAst.Where((Kandidaat) => null != Kandidaat.ItemName).ToArray();

				if (null != MissionAnforderungInRaum)
				{
					AusInventoryItemZuÜbertraageNaacActiveShip = MissionAnforderungInRaum.AusInventoryItemZuÜbertraageNaacActiveShip;
				}
			}
			finally
			{
				this.AusInventoryItemZuÜbertraageNaacActiveShip = AusInventoryItemZuÜbertraageNaacActiveShip;
				this.ListePrioGrupeMengeAufgaabeParam = ListePrioGrupeMengeAufgaabeObjektZuBearbaite;
				this.FürGefectListeDamageTypePrio = FürGefectListeDamageTypePrio;
				this.WirkungDestruktDroneTargetNääxteOverviewObj = WirkungDestruktDroneTargetNääxteOverviewObj;
				this.FürGefectAufgaabeManööverErgeebnis = FürGefectAufgaabeManööverErgeebnis;
				this.GefectModusAngraifendeObjekteAufDistanzHalte = GefectModusAngraifendeObjekteAufDistanzHalte;
				this.MengeObjektZuDestruiireNääxteDistance = MengeObjektZuDestruiireNääxteDistance;
			}
		}

		static public string ZuBevorzugendeOverviewPresetBerecneFürAnforderungMengeGrupeSictbar(
			SictOverviewObjektGrupeEnum[] AnforderungMengeObjektGrupeSictbar,
			KeyValuePair<string, SictOverviewObjektGrupeEnum[]>[] MengeZuOverviewPresetIdentMengeObjektGrupeSictbar)
		{
			if (null == MengeZuOverviewPresetIdentMengeObjektGrupeSictbar)
			{
				return null;
			}

			/*
			 * 2014.00.24
			 * 
			 * Vorersct werd Bewertung oone berüksictigung der Anzaal der in diisem Profiil insgesamt vorkomende Objekte berecnet, sctatdese werd nur di Anzaal der
			 * sictbaare ObjektGrupe berüksictigt. Somit isc di Ordnung zumindesct zwisce zwai Grupe korekt welce zuainander Untermenge/Obermenge sin.
			 * */

			//	Berecnet zu jeede benante Profiil di gesamtanzaal und di anzaal der im Verglaic zur AnforderungMengeObjektGrupeSictbar feelende OverviewObjektGrupe

			var MengeZuOverviewPresetIdentMengeObjektGrupeSictbarMitBewertung =
				MengeZuOverviewPresetIdentMengeObjektGrupeSictbar
				.Select((ZuOverviewPresetIdentMengeObjektGrupeSictbar) =>
					{
						var MengeObjektGrupeAnzaal = 0;
						var MengeObjektGrupeFeelendAnzaal = 0;

						if (null != AnforderungMengeObjektGrupeSictbar)
						{
							MengeObjektGrupeFeelendAnzaal =
								AnforderungMengeObjektGrupeSictbar.Count((Kandidaat) =>
									(null == ZuOverviewPresetIdentMengeObjektGrupeSictbar.Value) ? true :
									!ZuOverviewPresetIdentMengeObjektGrupeSictbar.Value.Contains(Kandidaat));
						}

						if (null != ZuOverviewPresetIdentMengeObjektGrupeSictbar.Value)
						{
							MengeObjektGrupeAnzaal = ZuOverviewPresetIdentMengeObjektGrupeSictbar.Value.Length;
						}

						return new KeyValuePair<string, KeyValuePair<int, int>>(
							ZuOverviewPresetIdentMengeObjektGrupeSictbar.Key,
							new KeyValuePair<int, int>(MengeObjektGrupeAnzaal, MengeObjektGrupeFeelendAnzaal));
					})
				.ToArray();

			//	Ordnung: gesamtanzaal sictbaarer Grupe sol mööglicst gering sain, hööhere Prio: anzaal feelender Grupe sol mööglicst gering sain.

			var MengeZuOverviewPresetIdentMengeObjektGrupeSictbarMitBewertungOrdnet =
				MengeZuOverviewPresetIdentMengeObjektGrupeSictbarMitBewertung
				.Where((Kandidaat) => 0 < Kandidaat.Value.Key)
				.OrderBy((Kandidaat) => Kandidaat.Value.Key)
				.OrderBy((Kandidaat) => Kandidaat.Value.Value)
				.ToArray();

			return MengeZuOverviewPresetIdentMengeObjektGrupeSictbarMitBewertungOrdnet.FirstOrDefault().Key;
		}

		public void AktualisiireTailAfterburner(ISictAutomatZuusctand AutomaatZuusctand)
		{
			if (null == AutomaatZuusctand)
			{
				return;
			}

			var ZaitMili = AutomaatZuusctand.NuzerZaitMili;

			var FürAinsctelungDistanceZurükzuleegendeSctrekeBetraag = this.FürAinsctelungDistanceZurükzuleegendeSctrekeBetraag;

			{
				var ModuleAfterburnerAktiivSol = false;

				if (3333 < FürAinsctelungDistanceZurükzuleegendeSctrekeBetraag)
				{
					ModuleAfterburnerAktiivSol = true;
				}

				InternAnforderungAfterburnerLezteZaitMili.AingangWert(ModuleAfterburnerAktiivSol ? (Int64?)ZaitMili : null);
			}
		}

		void AktualisiireTailAnforderungDroneLaunchUndReturn(ISictAutomatZuusctand AutomaatZuusctand)
		{
			if (null == AutomaatZuusctand)
			{
				return;
			}

			var InternZuusctand = AutomaatZuusctand;

			var ZaitMili = AutomaatZuusctand.NuzerZaitMili;

			var OptimatParam = InternZuusctand.OptimatParam();

			var FittingUndShipZuusctand = AutomaatZuusctand.FittingUndShipZuusctand;
			var AgentUndMissionInfo = AutomaatZuusctand.AgentUndMission;

			var AnnaameDroneCommandRange =
				(null == FittingUndShipZuusctand) ? null :
				FittingUndShipZuusctand.AnnaameDroneCommandRange;

			var DroneRangeLaunch = AnnaameDroneCommandRange ?? 40000;

			var DroneRangeReturn = DroneRangeLaunch + 20000;

			var ShipZuusctandLezte = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.ShipZuusctand;

			var SelbsctShipDocked = (null == ShipZuusctandLezte) ? null : (bool?)ShipZuusctandLezte.Docked;
			var SelbsctShipWarping = (null == ShipZuusctandLezte) ? null : ShipZuusctandLezte.Warping;
			var SelbsctShipWarpScrambled = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.SelbsctShipWarpScrambled;

			var SelbsctShipWarpingLezteZaitMili = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.WarpingLezteZaitMili;
			var SelbsctShipJumpingLezteZaitMili = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.JumpingLezteZaitMili;

			var MissionAktuel = (null == AgentUndMissionInfo) ? null : AgentUndMissionInfo.MissionAktuel;

			var MissionAktuelZiilLocationNääxteAuswert = (null == MissionAktuel) ? null : MissionAktuel.ZiilLocationNääxteAuswert;

			var ListePrioGrupeMengeAufgaabeObjektZuBearbaite = this.ListePrioGrupeMengeAufgaabeParam;

			var MengeAufgaabeObjektZuBearbaiteOrdnetNaacPrio =
				(null == ListePrioGrupeMengeAufgaabeObjektZuBearbaite) ? null :
				Bib3.Glob.ArrayAusListeFeldGeflact(ListePrioGrupeMengeAufgaabeObjektZuBearbaite.Select((GrupePrio) => GrupePrio.MengeAufgaabe));

			var PrioHööcsteGrupeMengeAufgaabeObjektZuBearbaite =
				(null == ListePrioGrupeMengeAufgaabeObjektZuBearbaite) ? null :
				ListePrioGrupeMengeAufgaabeObjektZuBearbaite.FirstOrDefault();

			var PrioHööcsteMengeAufgaabeObjektZuBearbaite =
				(null == PrioHööcsteGrupeMengeAufgaabeObjektZuBearbaite) ? null :
				PrioHööcsteGrupeMengeAufgaabeObjektZuBearbaite.MengeAufgaabe;

			var PrioHööcsteGrupeMengeAufgaabeObjektActivate =
				(null == PrioHööcsteMengeAufgaabeObjektZuBearbaite) ? null :
				PrioHööcsteMengeAufgaabeObjektZuBearbaite
				.OfType<AufgaabeParamAndere>()
				.Where((AufgaabeObjektZuBearbaite) => true == AufgaabeObjektZuBearbaite.AktioonInRaumObjektActivate)
				.ToArray();

			var MengeAufgaabeObjektZuZersctööre =
				(null == MengeAufgaabeObjektZuBearbaiteOrdnetNaacPrio) ? null :
				MengeAufgaabeObjektZuBearbaiteOrdnetNaacPrio.Where((Aufgaabe) => true == Aufgaabe.AktioonWirkungDestruktVirt()).ToArray();

			var AufgaabeAuszufüüreNääxte = this.AufgaabeAuszufüüreNääxte;

			var InfoPanelRouteFraigaabe = this.InfoPanelRouteFraigaabe;

			var InRaumAktioonFortsaz = this.InRaumAktioonFortsaz;

			var AnforderungDroneLaunch = false;
			var AnforderungDroneReturn = false;

			{
				//	Aktualisatioon AnforderungDroneLaunchLezteZaitMili

				if (null != MengeAufgaabeObjektZuZersctööre)
				{
					var ListeObjektAnzugraifeUntermengeZiilFürDroneNaacTyp =
						MengeAufgaabeObjektZuZersctööre
						.Where((Kandidaat) =>
						{
							if (null == Kandidaat)
							{
								return false;
							}

							return true;    //	Vorersct Drone für ale Ziilobjekte aktiviire.
						}).ToArray();

					var ListeObjektAnzugraifeUntermengeZiilFürDroneInRangeReturnNict =
						ListeObjektAnzugraifeUntermengeZiilFürDroneNaacTyp
						.Where((Kandidaat) =>
						{
							var ObjektZuBearbeite = Kandidaat.OverViewObjektZuBearbaiteVirt();

							if (null == ObjektZuBearbeite)
							{
								return false;
							}

							return !(DroneRangeReturn < (ObjektZuBearbeite.SictungLezteDistanceScrankeMaxScpezOverview ?? Int64.MaxValue));
						})
						.ToArray();

					var ListeObjektAnzugraifeUntermengeZiilFürDroneInRangeLaunch =
						ListeObjektAnzugraifeUntermengeZiilFürDroneInRangeReturnNict
						.Where((Kandidaat) =>
						{
							var ObjektZuBearbeite = Kandidaat.OverViewObjektZuBearbaiteVirt();

							return !(DroneRangeLaunch < (ObjektZuBearbeite.SictungLezteDistanceScrankeMaxScpezOverview ?? Int64.MaxValue));
						})
						.ToArray();

					if (0 < ListeObjektAnzugraifeUntermengeZiilFürDroneInRangeLaunch.Length)
					{
						AnforderungDroneLaunch = true;
					}

					if (!(0 < ListeObjektAnzugraifeUntermengeZiilFürDroneInRangeReturnNict.Length))
					{
						AnforderungDroneReturn = true;
					}
				}

				if (true == SelbsctShipWarping)
				{
					AnforderungDroneReturn = true;
				}

				if (!(true == InRaumAktioonFortsaz))
				{
					AnforderungDroneReturn = true;
				}

				var AufgaabeAuszufüüreNääxteAlsAndere = AufgaabeAuszufüüreNääxte as AufgaabeParamAndere;

				if (null != AufgaabeAuszufüüreNääxteAlsAndere)
				{
					if (true == AufgaabeAuszufüüreNääxteAlsAndere.AktioonInRaumObjektActivate)
					{
						//	Objekt könnte Acc-Gate sain und Warp auslöse.
						AnforderungDroneReturn = true;
					}
				}

				if (true == InfoPanelRouteFraigaabe)
				{
					AnforderungDroneReturn = true;
				}

				if (0 < ExtractFromOldAssembly.Bib3.Extension.CountNullable(PrioHööcsteGrupeMengeAufgaabeObjektActivate))
				{
					//	Es gebt mindesctens än Objekt welces aktiviirt were sol.
					//	Aktiviirung des Objekt könte zum verlase des Raum füüre (Acceleration Gate)
					AnforderungDroneReturn = true;
				}

				//	!!!!	Berüksictigung Zaitraum kurz vor Fluct (gescäzt) feelt noc
			}

			if (AnforderungDroneReturn)
			{
				AnforderungDroneLaunch = false;
			}

			this.AnforderungDroneLaunch.AingangWertZuZait(ZaitMili, AnforderungDroneLaunch);
			this.AnforderungDroneReturn.AingangWertZuZait(ZaitMili, AnforderungDroneReturn);
		}
	}
}
