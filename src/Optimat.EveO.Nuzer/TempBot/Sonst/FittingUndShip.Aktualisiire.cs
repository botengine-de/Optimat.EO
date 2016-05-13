using System;
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

namespace Optimat.EveOnline.Anwendung
{
	public partial class SictShipZuusctandMitFitting
	{
		public void Aktualisiire(
			ISictAutomatZuusctand AutomaatZuusctand,
			VonSensorikMesung AusScnapscusAuswertungZuusctand)
		{
			AktualisiireTailTargetingRange(AutomaatZuusctand, AusScnapscusAuswertungZuusctand);

			AktualisiireTailDroneCommandRangeUndDroneCommandCount(AutomaatZuusctand, AusScnapscusAuswertungZuusctand);

			AktualisiireTailInventoryMesungShipIsPodLezteZaitUndWert(AutomaatZuusctand);

			if (null == AusScnapscusAuswertungZuusctand)
			{
				return;
			}

			if (null == AutomaatZuusctand)
			{
				return;
			}

			var ZaitMili = AutomaatZuusctand.NuzerZaitMili;

			TempDebugAktualisiireLezteZait = ZaitMili;

			ShipState ScnapscusSelbsctShipZuusctand = null;
			ShipState ShipZuusctand = null;
			var VorhersaageSelbstScifTreferpunkte = new SictWertMitZait<ShipHitpointsAndEnergy>();
			bool? SelbsctShipWarpScrambled = null;
			Int64? AnnaameModuleDestruktRangeMax = null;
			Int64? AnnaameModuleDestruktRangeOptimum = null;
			SictShipUiModuleReprZuusctand AnnaameModuleAfterburner = null;
			SictShipUiModuleReprZuusctand AnnaameModuleShieldBoosterSelbsct = null;
			SictShipUiModuleReprZuusctand AnnaameModuleArmorRepairerSelbsct = null;
			bool? Cloaked = null;
			bool ZaitraumDockedLezteHinraicendAltFürBeurtailungShipZuusctand = false;
			var MengeModuleRepr = this.MengeModuleRepr;

			var UndockedMesungShipTypIstPodLezteZaitMili = this.UndockedMesungShipIsPodLezteZaitMili;

			ShipUiModule	AusShipUiModuleReprHilite = null;

			var ListeAusShipUIIndicationMitZait = this.ListeAusShipUIIndicationMitZait;

			bool? ScritNääxteJammed = null;
			bool? ScritÜüberNääxteJammed = null;

			bool? AnnaameOreHoldLeer = null;
			Int64? AnnaameOreInMengeModuleMinerZyyklusVolumeMili = null;
			int? AnnaameNaacAbbrucMinerZyyklusOreHoldGefültMili = null;


			try
			{
				var Gbs = AutomaatZuusctand.Gbs;
				var OverviewUndTarget = AutomaatZuusctand.OverviewUndTarget;

				var OverviewMengeObjekt = (null == OverviewUndTarget) ? null : OverviewUndTarget.MengeOverViewObjekt;

				var ListeAusGbsAbovemainMessageMitZait = AutomaatZuusctand.ListeAusGbsAbovemainMessageMitZait();
				var ListeAbovemainMessageAuswertMitZait = (null == Gbs) ? null : Gbs.ListeAbovemainMessageAuswertMitZait;

				var AusScnapscusMengeMessageBox = AusScnapscusAuswertungZuusctand.MengeMessageBox();
				var ScnapscusWindowDroneView = AusScnapscusAuswertungZuusctand.WindowDroneView;

				var AusScnapscusModuleButtonHint = AusScnapscusAuswertungZuusctand.ModuleButtonHint;
				var ShipUi = AusScnapscusAuswertungZuusctand.ShipUi;

				var ScnapscusMengeWindowInventory = AusScnapscusAuswertungZuusctand.MengeWindowInventory;

				var ShipUiIndication = (null == ShipUi) ? null : ShipUi.Indication;

				ScnapscusSelbsctShipZuusctand = (null == ShipUi) ? null : AusScnapscusAuswertungZuusctand.SelfShipState;

				var ShipUiMengeEWarElement = (null == ShipUi) ? null : ShipUi.MengeEWarElement;

				var ShipUiMengeModuleRepr = (null == ScnapscusSelbsctShipZuusctand) ? null : ShipUi.MengeModule;

				var GrupeDronesInLocalSpace = (null == ScnapscusWindowDroneView) ? null : ScnapscusWindowDroneView.GrupeDronesInLocalSpace;

				var GrupeDronesInLocalSpaceMengeDroneEntry = (null == GrupeDronesInLocalSpace) ? null : GrupeDronesInLocalSpace.MengeDroneEntry;

				if (null != GrupeDronesInLocalSpaceMengeDroneEntry)
				{
					bool DroneRepairNüzlic = false;

					foreach (var DroneEntry in GrupeDronesInLocalSpaceMengeDroneEntry)
					{
						if (null == DroneEntry)
						{
							continue;
						}

						var DroneEntryTreferpunkte = DroneEntry.Treferpunkte;

						var DroneEntryTreferpunkteArmor = (null == DroneEntryTreferpunkte) ? null : DroneEntryTreferpunkte.Armor;
						var DroneEntryTreferpunkteStruct = (null == DroneEntryTreferpunkte) ? null : DroneEntryTreferpunkte.Struct;

						if (null != DroneEntryTreferpunkteArmor)
						{
							if (DroneEntryTreferpunkteArmor < 888)
							{
								DroneRepairNüzlic = true;
								break;
							}
						}

						if (null != DroneEntryTreferpunkteStruct)
						{
							if (DroneEntryTreferpunkteStruct < 888)
							{
								DroneRepairNüzlic = true;
								break;
							}
						}
					}

					if (DroneRepairNüzlic)
					{
						RepairNüzlicLezteZait = ZaitMili;
					}
				}

				{
					//	Berecnung ListeAusShipUIIndication

					if (null == ListeAusShipUIIndicationMitZait)
					{
						this.ListeAusShipUIIndicationMitZait = ListeAusShipUIIndicationMitZait =
							new List<SictVerlaufBeginUndEndeRef<ShipUiIndicationAuswert>>();
					}

					var AusShipUIIndicationMitZaitLezteNocAktiiv = (null == ListeAusShipUIIndicationMitZait) ? null : ListeAusShipUIIndicationMitZait.LastOrDefault();

					if (null != AusShipUIIndicationMitZaitLezteNocAktiiv)
					{
						if (AusShipUIIndicationMitZaitLezteNocAktiiv.EndeZait.HasValue)
						{
							AusShipUIIndicationMitZaitLezteNocAktiiv = null;
						}
					}

					if (null != AusShipUIIndicationMitZaitLezteNocAktiiv)
					{
						var ListeAusShipUIIndicationLezteNocAktiiv = AusShipUIIndicationMitZaitLezteNocAktiiv.Wert;

						var IndicationNocAktiiv = false;

						if (null != ListeAusShipUIIndicationLezteNocAktiiv)
						{
							if (VonSensor.ShipUiIndication.HinraicendGlaicwertigFürFortsaz(ListeAusShipUIIndicationLezteNocAktiiv.ShipUIIndication, ShipUiIndication))
							{
								IndicationNocAktiiv = true;
							}
						}

						if (!IndicationNocAktiiv)
						{
							AusShipUIIndicationMitZaitLezteNocAktiiv.EndeZait = ZaitMili;
							AusShipUIIndicationMitZaitLezteNocAktiiv = null;
						}
					}

					if (null != ShipUiIndication)
					{
						if (null != ShipUiIndication.IndicationCaption &&
							null == AusShipUIIndicationMitZaitLezteNocAktiiv)
						{
							var ShipUiIndicationAuswert = new ShipUiIndicationAuswert(ShipUiIndication);

							ListeAusShipUIIndicationMitZait.Add(new SictVerlaufBeginUndEndeRef<ShipUiIndicationAuswert>(ZaitMili, null, ShipUiIndicationAuswert));
						}
					}

					Bib3.Extension.ListeKürzeBegin(ListeAusShipUIIndicationMitZait, 10);
				}

				var GbsAbovemainMessageAmmoLoadMengeRegexPattern = SictShipZuusctandMitFitting.GbsAbovemainMessageAmmoLoadMengeRegexPattern;
				var AbovemainMessageCloakingMengeRegexPattern = SictShipZuusctandMitFitting.AbovemainMessageCloakingMengeRegexPattern;
				var AbovemainMessageDockingMengeRegexPattern = SictShipZuusctandMitFitting.AbovemainMessageDockingMengeRegexPattern;
				var AbovemainMessageCannotWarpThereMengeRegexPattern = SictShipZuusctandMitFitting.AbovemainMessageCannotWarpThereMengeRegexPattern;

				{
					var ListeSelbstScifZuusctandVergangenhait = this.ListeSelbstScifZuusctandVergangenhait;

					if (null == ListeSelbstScifZuusctandVergangenhait)
					{
						this.ListeSelbstScifZuusctandVergangenhait = ListeSelbstScifZuusctandVergangenhait =
							new List<SictWertMitZait<ShipState>>();
					}

					if (null != ScnapscusSelbsctShipZuusctand)
					{
						ListeSelbstScifZuusctandVergangenhait.Add(new SictWertMitZait<ShipState>(ZaitMili, ScnapscusSelbsctShipZuusctand));
					}
				}

				if (null != ShipUiMengeEWarElement)
				{
					var ShipUiEWarElementWarpScramble =
						ShipUiMengeEWarElement.FirstOrDefault((EWarElement) => (null == EWarElement) ? false : SictEWarTypeEnum.WarpScramble == EWarElement.EWarTypeEnum);

					SelbsctShipWarpScrambled = null != ShipUiEWarElementWarpScramble;
				}

				var AbovemainMessageCloakingMitZaitLezte = MessageRegexMatchLezte(ListeAusGbsAbovemainMessageMitZait, AbovemainMessageCloakingMengeRegexPattern, RegexOptions.IgnoreCase);

				var AbovemainMessageCloakingLezteBeginZaitMili = (null == AbovemainMessageCloakingMitZaitLezte) ? null : AbovemainMessageCloakingMitZaitLezte.BeginZait;

				var AbovemainMessageCloakingLezteBeginAlterMili = ZaitMili - AbovemainMessageCloakingLezteBeginZaitMili;

				var AbovemainMessageDockingMitZaitLezte = MessageRegexMatchLezte(ListeAusGbsAbovemainMessageMitZait, AbovemainMessageDockingMengeRegexPattern, RegexOptions.IgnoreCase);

				var AbovemainMessageCannotWarpThereMitZaitLezte = MessageRegexMatchLezte(ListeAusGbsAbovemainMessageMitZait, AbovemainMessageCannotWarpThereMengeRegexPattern, RegexOptions.IgnoreCase);

				var AbovemainMessageDockingLezteBeginZaitMili = (null == AbovemainMessageDockingMitZaitLezte) ? null : AbovemainMessageDockingMitZaitLezte.BeginZait;

				var AbovemainMessageDockingLezteBeginAlterMili = ZaitMili - AbovemainMessageDockingLezteBeginZaitMili;

				{
					var AbovemainMessageAmmoLoadLezte = MessageRegexMatchLezte(ListeAusGbsAbovemainMessageMitZait, GbsAbovemainMessageAmmoLoadMengeRegexPattern, RegexOptions.IgnoreCase);

					if (null != AbovemainMessageAmmoLoadLezte)
					{
						AmmoLoadLezteBeginZait = Bib3.Glob.Max(AmmoLoadLezteBeginZait, AbovemainMessageAmmoLoadLezte.BeginZait);
					}
				}

				{
					this.AbovemainMessageMengeTargetAnzaalSkillScrankeMaxLezte =
						ListeAbovemainMessageAuswertMitZait
						.LastOrDefaultNullable((Kandidaat) =>
							null == Kandidaat.Wert ? false : Kandidaat.Wert.MengeTargetAnzaalSkillScrankeMax.HasValue);

					MengeTargetAnzaalSkillScrankeMax = null;

					if (null != AbovemainMessageMengeTargetAnzaalSkillScrankeMaxLezte)
					{
						if (null	!= AbovemainMessageMengeTargetAnzaalSkillScrankeMaxLezte.Wert	&&
							!(6e+4	< ZaitMili - AbovemainMessageMengeTargetAnzaalSkillScrankeMaxLezte.EndeZait))
						{
							MengeTargetAnzaalSkillScrankeMax = AbovemainMessageMengeTargetAnzaalSkillScrankeMaxLezte.Wert.MengeTargetAnzaalSkillScrankeMax;
						}
					}

					MengeTargetAnzaalScrankeMax = MengeTargetAnzaalSkillScrankeMax;
				}

				{
					if (null != AbovemainMessageCloakingMitZaitLezte)
					{
						this.AbovemainMessageCloakingLezte = AbovemainMessageCloakingMitZaitLezte;
					}
				}

				{
					if (null != AbovemainMessageDockingMitZaitLezte)
					{
						this.AbovemainMessageDockingLezte = AbovemainMessageDockingMitZaitLezte;
					}
				}

				if (null != AbovemainMessageCannotWarpThereMitZaitLezte)
				{
					this.AbovemainMessageCannotWarpThereLezte = AbovemainMessageCannotWarpThereMitZaitLezte;
				}

				var ShipUiIndicationAuswertNictLeerMitZaitLezte =
					(null == ListeAusShipUIIndicationMitZait) ? null :
					ListeAusShipUIIndicationMitZait.LastOrDefault((Kandidaat) => Kandidaat.Wert.NictLeer());

				var ShipUiIndicationNictLeerAuswertLezte = (null == ShipUiIndicationAuswertNictLeerMitZaitLezte) ? null : ShipUiIndicationAuswertNictLeerMitZaitLezte.Wert;

				var ShipUiIndicationNictLeerLezte = (null == ShipUiIndicationNictLeerAuswertLezte) ? null : ShipUiIndicationNictLeerAuswertLezte.ShipUIIndication;

				var ShipUiIndicationNictLeerLezteBeginZait = (null == ShipUiIndicationAuswertNictLeerMitZaitLezte) ? null : ShipUiIndicationAuswertNictLeerMitZaitLezte.BeginZait;

				{
					var ShipUiIndicationJumpingLezte = IndicationCaptionMitManöverTypLezte(ListeAusShipUIIndicationMitZait, SictZuInRaumObjektManööverTypEnum.Jump);

					if (null != ShipUiIndicationJumpingLezte)
					{
						if (ShipUiIndicationJumpingLezte.EndeZait.HasValue)
						{
							this.JumpingLezteZaitMili = ShipUiIndicationJumpingLezte.EndeZait;
						}
						else
						{
							this.JumpingLezteZaitMili = ZaitMili;
						}
					}
				}

				var ShipUiIndicationDockingMitZaitLezte = IndicationCaptionMitManöverTypLezte(ListeAusShipUIIndicationMitZait, SictZuInRaumObjektManööverTypEnum.Dock);

				var ShipUiIndicationDockingLezteBeginZaitMili = (null == ShipUiIndicationDockingMitZaitLezte) ? null : ShipUiIndicationDockingMitZaitLezte.BeginZait;

				var ShipUiIndicationDockingLezteBeginAlterMili = ZaitMili - ShipUiIndicationDockingLezteBeginZaitMili;

				{
					//	Berecnung Cloaked

					var JumpingLezteZaitMili = this.JumpingLezteZaitMili;

					var JumpingLezteAlterMili = ZaitMili - JumpingLezteZaitMili;

					if (JumpingLezteAlterMili / 1000 < NaacJumpingAnnaameCloakDauer &&
						ShipUiIndicationNictLeerLezteBeginZait < JumpingLezteZaitMili)
					{
						Cloaked = true;
					}

					if (AbovemainMessageCloakingLezteBeginAlterMili / 1000 < 7 &&
						ShipUiIndicationNictLeerLezteBeginZait < AbovemainMessageCloakingLezteBeginZaitMili)
					{
						Cloaked = true;
					}
				}

				{
					{
						//	Berecnung ShipZuusctand

						/*
						 * 2014.00.27	Beobactung:
						 * Geleegentlic werd von Nuzer Mesung Oone Struct Treferpunkte geliifert. es isc davon auszugehe das diise für ale
						 * Aigescafte pasiire kan da mancmaal Bescriftunge feelen.
						 * Daher werd fals Tail=null diiser fals vorhande mit deem Wert aus leztem Scnapscus aufgefült.
						 * */

						bool? ShipDocking = null;

						{
							//	Berecnung Docking

							if (AbovemainMessageDockingLezteBeginAlterMili / 1000 < 5 ||
								ShipUiIndicationDockingLezteBeginAlterMili / 1000 < 5)
							{
								ShipDocking = true;
							}
						}

						var ShipZuusctandAusAndereInfo =
							new ShipState(
								null,
								null,
								AusScnapscusAuswertungZuusctand.Docked(),
								ShipDocking,
								null,
								null,
								Cloaked);

						/*
						 * 2015.02.24
						 * 
						 * Ersaz durc Bib3.RefNezDiferenz.Extension.InRefNezApliziireErsazWoUnglaicDefault.
						 * 
						ShipZuusctand =
							ShipState.KombiAusElementTailUnglaicNul(
							new ShipState[]{
									ShipZuusctandAusAndereInfo,
									ScnapscusSelbsctShipZuusctand,
									this.ScnapscusVorLezteSelbsctShipZuusctand});
						 * */

						ShipZuusctand =
							Bib3.RefNezDiferenz.Extension.ObjektKopiiKonstrukt(ScnapscusSelbsctShipZuusctand)	??
							new ShipState();

						Bib3.RefNezDiferenz.Extension.InRefNezApliziireErsazWoUnglaicDefault(ShipZuusctand, ShipZuusctandAusAndereInfo);
					}

					while (0 < ListeSelbstScifZuusctandVergangenhait.Count &&
						13000 < (ZaitMili - ListeSelbstScifZuusctandVergangenhait.FirstOrDefault().Zait))
					{
						ListeSelbstScifZuusctandVergangenhait.RemoveAt(0);
					}

					var FürVorhersaageSelbstScifZuusctandListeMesung =
						ListeSelbstScifZuusctandVergangenhait
						.Where((Kandidaat) => Kandidaat.Wert.HitpointsNotNull())
						.OrderBy((Kandidaat) => Kandidaat.Zait)
						.ToArray();

					var SelbstScifZuusctandVergangenhaitListeKandidaat =
						FürVorhersaageSelbstScifZuusctandListeMesung
						.OrderBy((Kandidaat) => Math.Abs(ZaitMili - Kandidaat.Zait - 10000))
						.ToArray();

					var SelbstScifZuusctandVergangenhait =
						SelbstScifZuusctandVergangenhaitListeKandidaat
						.FirstOrDefault();

					ShipHitpointsAndEnergy VorhersaageTreferpunkte = null;

					var ZaitDiferenz = ZaitMili - SelbstScifZuusctandVergangenhait.Zait;

					var ShipZuusctandTreferpunkte = (null == ShipZuusctand) ? null : ShipZuusctand.HitpointsRelMili;

					if (null != SelbstScifZuusctandVergangenhait.Wert &&
						null != ShipZuusctandTreferpunkte &&
						0 < ZaitDiferenz)
					{
						var ShipZuusctandVergangenhaitTreferpunkte = SelbstScifZuusctandVergangenhait.Wert.HitpointsRelMili;

						var ShipZuusctandTreferpunkteShield = ShipZuusctandTreferpunkte.Shield;
						var ShipZuusctandTreferpunkteArmor = ShipZuusctandTreferpunkte.Armor;

						if (null != ShipZuusctandTreferpunkteShield && null != ShipZuusctandTreferpunkteArmor)
						{
							var DiferenzShield =
								(null == ShipZuusctandTreferpunkteShield) ? null :
								(ShipZuusctandTreferpunkteShield - ShipZuusctandVergangenhaitTreferpunkte.Shield);

							var DiferenzArmor =
								(null == ShipZuusctandTreferpunkteArmor) ? null :
								(ShipZuusctandTreferpunkteArmor - ShipZuusctandVergangenhaitTreferpunkte.Armor);

							var GescwindigkaitShieldMili =
								(DiferenzShield * 1000) / ZaitDiferenz;

							var GescwindigkaitArmorMili =
								(DiferenzArmor * 1000) / ZaitDiferenz;

							var VorhersaageZaitDistanzMili = VorhersaageSelbstScifTreferpunkteZaitDistanz * 1000;

							var VorhersaageShieldMili =
								(null == ShipZuusctandTreferpunkteShield) ? null :
								ShipZuusctandTreferpunkteShield +
								(GescwindigkaitShieldMili * VorhersaageZaitDistanzMili) / 1000;

							var ÜbertraagShield = Bib3.Glob.Min(0, VorhersaageShieldMili) ?? 0;

							VorhersaageShieldMili = Bib3.Glob.Max(VorhersaageShieldMili, 0);

							var VorhersaageArmorMili =
								(null == ShipZuusctandTreferpunkteArmor) ? null :
								ShipZuusctandTreferpunkteArmor +
								(GescwindigkaitArmorMili * VorhersaageZaitDistanzMili) / 1000 +
								ÜbertraagShield;

							VorhersaageTreferpunkte = new ShipHitpointsAndEnergy(
								ShipZuusctandTreferpunkte.Struct,
								(int?)VorhersaageArmorMili,
								(int?)VorhersaageShieldMili,
								ShipZuusctandTreferpunkte.Capacitor);

							VorhersaageSelbstScifTreferpunkte = new SictWertMitZait<ShipHitpointsAndEnergy>(
								ZaitMili + VorhersaageZaitDistanzMili, VorhersaageTreferpunkte);
						}
					}

					var ShipZuusctandDocked = (null == ShipZuusctand) ? null : ShipZuusctand.Docked;

					if (null != ShipZuusctand)
					{
						var SpeedDurcMeterProSekunde = ShipZuusctand.SpeedDurcMeterProSekunde;

						if (SpeedDurcMeterProSekunde.HasValue)
						{
							ListeZuZaitGescwindigkait.Enqueue(new SictWertMitZait<Int64>(ZaitMili, SpeedDurcMeterProSekunde.Value + 1));
							ListeZuZaitGescwindigkait.ListeKürzeBegin(60);
						}

						if (true == ShipZuusctand.Docking)
						{
							this.DockingLezteZaitMili = ZaitMili;
						}

						if (true == ShipZuusctand.Warping)
						{
							this.WarpingLezteZaitMili = ZaitMili;
						}

						if (true == ShipZuusctand.Jumping)
						{
							this.JumpingLezteZaitMili = ZaitMili;
						}
					}

					if (true == ShipZuusctandDocked)
					{
						this.DockedLezteZaitMili = ZaitMili;

						if (null != MengeModuleRepr)
						{
							MengeModuleRepr.Clear();
						}
					}

					var DockedLezteAlterMili = ZaitMili - DockedLezteZaitMili;

					var ShipDockedLezteAlterScritAnzaal = AutomaatZuusctand.ZuNuzerZaitMiliBerecneAlterScnapscusAnzaal(DockedLezteZaitMili);

					ZaitraumDockedLezteHinraicendAltFürBeurtailungShipZuusctand =
						!(DockedLezteAlterMili < 8888) ||
						(2 < ShipDockedLezteAlterScritAnzaal && 4444 < DockedLezteAlterMili);
				}

				if (null != ShipUi && ZaitraumDockedLezteHinraicendAltFürBeurtailungShipZuusctand)
				{
					var ScnapscusIndikatorShipTypIstPod = ShipUiMengeModuleRepr.NullOderLeer();

					UndockedMesungShipTypIstPodIndikator.AingangWertZuZait(ZaitMili, ScnapscusIndikatorShipTypIstPod);

					if (ScnapscusIndikatorShipTypIstPod)
					{
						if (2 < UndockedMesungShipTypIstPodIndikator.SaitTransitionLezteListeAingangAnzaal)
						{
							UndockedMesungShipTypIstPodLezteZaitMili = ZaitMili;
						}
					}
					else
					{
						UndockedMesungShipTypIstPodLezteZaitMili = null;
					}
				}

				if (null != AusScnapscusMengeMessageBox)
				{
					var AusScnapscusMessageBox = AusScnapscusMengeMessageBox.FirstOrDefault((Kandidaat) => null != Kandidaat);

					if (null != AusScnapscusMessageBox)
					{
						AusGbsMessageBoxLezte = new SictWertMitZait<VonSensor.MessageBox>(ZaitMili, AusScnapscusMessageBox);
					}
				}

				if (null != ShipUiMengeModuleRepr)
				{
					//	Noie GBS Repr zu Module ainfüüge

					foreach (var AusUiModuleRepr in ShipUiMengeModuleRepr)
					{
						if (null == AusUiModuleRepr)
						{
							continue;
						}

						var AusUiModuleReprInGbsFläce = AusUiModuleRepr.InGbsFläce;

						if (AusUiModuleReprInGbsFläce.IsLeer)
						{
							continue;
						}

						var MengeBisherNääxteModuleReprMitSctreke =
							MengeModuleRepr.Select((BisherModuleRepr) =>
								new KeyValuePair<SictShipUiModuleReprZuusctand, Vektor2DInt>(
									BisherModuleRepr,
									AusUiModuleReprInGbsFläce.ZentrumLaage -
									BisherModuleRepr.ListeLaageLezteBerecne() ?? new Vektor2DInt(int.MinValue, int.MinValue))).ToArray();

						var BisherModuleReprNääxteMitSctreke =
							MengeBisherNääxteModuleReprMitSctreke.OrderBy((Kandidaat) => Kandidaat.Value.BetraagQuadriirt).FirstOrDefault();

						SictShipUiModuleReprZuusctand ModuleRepr = null;

						if (null != BisherModuleReprNääxteMitSctreke.Key && BisherModuleReprNääxteMitSctreke.Value.Betraag < 10)
						{
							ModuleRepr = BisherModuleReprNääxteMitSctreke.Key;
						}
						else
						{
							ModuleRepr = new SictShipUiModuleReprZuusctand();

							MengeModuleRepr.Add(ModuleRepr);
						}

						TempDebugAktualisiireLezteZait1 = ZaitMili;

						ModuleRepr.AingangScnapscus(
							ZaitMili,
							AusUiModuleRepr,
							new AutomaatZuusctandUndGeneric<int>(AutomaatZuusctand));
					}
				}

				{
					//	Module entferne welce scon zu lange unsictbar sind.
					//	Wurde 2013.11.27 verscoobe naac Aktualisatioon Sictbarkait (SictbarLezteZait.AingangWert) damit durc lange Zaitraum zwisce Scnapscus kain entferne meer ausgelööst werd.

					var MengeModuleReprZuErhalte =
						MengeModuleRepr
						.Where((Kandidaat) => (ZaitMili - Kandidaat.SictbarLezteZait.AingangHasValueLezte) < ModuleSictbarNictDauerScrankeMax * 1000)
						.ToArray();

					var MengeModuleReprZuEntferne =
						MengeModuleRepr.Except(MengeModuleReprZuErhalte).ToArray();

					foreach (var ModuleReprZuEntferne in MengeModuleReprZuEntferne)
					{
						MengeModuleRepr.Remove(ModuleReprZuEntferne);
					}
				}

				var MengeSlotSpriteHiliteSictbar = new List<ShipUiModule>();

				if (null != ShipUiMengeModuleRepr)
				{
					MengeSlotSpriteHiliteSictbar.AddRange(
						ShipUiMengeModuleRepr
						.Where((Slot) => true == Slot.SpriteHiliteSictbar));
				}

				if (1 == MengeSlotSpriteHiliteSictbar.Count)
				{
					AusShipUiModuleReprHilite = MengeSlotSpriteHiliteSictbar.FirstOrDefault();
				}

				var ModuleReprHilite = MengeModuleRepr.FirstOrDefault((Kandidaat) => Kandidaat.HiliteLezteZait.AingangLezte.HasValue);

				var GbsMengeMenuMitBeginZait = AutomaatZuusctand.GbsListeMenuNocOfeMitBeginZaitBerecne();

				Int64? AusGbsMessageBoxLezteAlterMili = null;

				if (null != AusGbsMessageBoxLezte.Wert)
				{
					AusGbsMessageBoxLezteAlterMili = ZaitMili - AusGbsMessageBoxLezte.Zait;
				}

				{
					var ListeVersuucFitLoad = this.ListeVersuucFitLoad;

					{
						//	Noi sait 2014.03.28: Ainbau VersuucFitLoad naac ListeVersuucFitLoad aus AutomaatZuusctand.MengeAufgaabeZuusctand

						var MengeAufgaabeZuusctand = AutomaatZuusctand.MengeAufgaabeZuusctand;

						if (null != MengeAufgaabeZuusctand)
						{
							foreach (var Aufgaabe in MengeAufgaabeZuusctand)
							{
								if (null == Aufgaabe)
								{
									continue;
								}

								if (!Aufgaabe.AbsclusTailWirkungZait.HasValue)
								{
									continue;
								}

								var AufgaabeParam = Aufgaabe.AufgaabeParam as AufgaabeParamAndere;

								if (null == AufgaabeParam)
								{
									continue;
								}

								var AufgaabeParamFittingZuApliziire = AufgaabeParam.FittingZuApliziire;

								if (null == AufgaabeParamFittingZuApliziire)
								{
									continue;
								}

								var AusFittingManagementFittingZuLaade = AufgaabeParamFittingZuApliziire.AusFittingManagementFittingZuLaade;

								if (null == AusFittingManagementFittingZuLaade)
								{
									continue;
								}

								if (null == ListeVersuucFitLoad)
								{
									this.ListeVersuucFitLoad = ListeVersuucFitLoad =
										new List<SictWertMitZait<SictVersuucFitLoadErgeebnis>>();
								}

								{
									if (ListeVersuucFitLoad.Any((VersuucFitLoad) => VersuucFitLoad.Zait == Aufgaabe.AbsclusTailWirkungZait))
									{
										continue;
									}
								}

								ListeVersuucFitLoad.Add(new SictWertMitZait<SictVersuucFitLoadErgeebnis>(
									Aufgaabe.AbsclusTailWirkungZait.Value,
									new SictVersuucFitLoadErgeebnis(AusFittingManagementFittingZuLaade)));
							}
						}
					}

					//	Berecnung Annaame Versuuc Fit Load Erfolg/Feelsclaag.

					if (null != ListeVersuucFitLoad)
					{
						Bib3.Extension.ListeKürzeBegin(ListeVersuucFitLoad, ListeVersuucFitLoadAnzaalScrankeMax);

						var ListeVersuucFitLoadLezte = ListeVersuucFitLoad.LastOrDefault();

						var ListeVersuucFitLoadLezteAlterMili = ZaitMili - ListeVersuucFitLoadLezte.Zait;

						if (null != ListeVersuucFitLoadLezte.Wert)
						{
							if (ListeVersuucFitLoadLezte.Wert.EntscaidungErfolg.HasValue)
							{
								if (!ListeVersuucFitLoadLezte.Wert.EntscaidungErfolg.Value.Wert)
								{
									FitLoadedLezteNocAktiiv = null;
								}
							}
							else
							{
								FitLoadedLezteNocAktiiv = null;
							}

							if (1555 < ListeVersuucFitLoadLezteAlterMili)
							{
								if (!ListeVersuucFitLoadLezte.Wert.EntscaidungErfolg.HasValue)
								{
									if (7777 < ListeVersuucFitLoadLezteAlterMili)
									{
										ListeVersuucFitLoadLezte.Wert.EntscaidungErfolg = new SictWertMitZait<bool>(ZaitMili, false);
									}
									else
									{
										if (AusGbsMessageBoxLezteAlterMili < 4444 &&
											null != AusGbsMessageBoxLezte.Wert)
										{
											ListeVersuucFitLoadLezte.Wert.EntscaidungErfolg = new SictWertMitZait<bool>(ZaitMili, false);
											ListeVersuucFitLoadLezte.Wert.ErgeebnisMessageBox = AusGbsMessageBoxLezte.Wert;
										}
										else
										{
											var IndikatorFitErfolgt = false;

											if (null != ListeAusGbsAbovemainMessageMitZait)
											{
												var ListeAusGbsAbovemainMessageLezte = ListeAusGbsAbovemainMessageMitZait.LastOrDefault();

												if (null != ListeAusGbsAbovemainMessageLezte)
												{
													if (ListeVersuucFitLoadLezte.Zait <= ListeAusGbsAbovemainMessageLezte.BeginZait &&
														null != ListeAusGbsAbovemainMessageLezte.Wert)
													{
														/*
														 * 2013.09.09 Bsp:
														 * "The rigs in the saved fitting were not fitted as your active ship already has rigs"
														 * */

														if (Regex.Match(ListeAusGbsAbovemainMessageLezte.Wert.LabelText ?? "", "rigs in the saved fitting", RegexOptions.IgnoreCase).Success)
														{
															IndikatorFitErfolgt = true;
														}
													}
												}
											}

											/*
											 * 2013.09.09
											 * Bisher nur Scpeziaalfal Abgedekt:
											 * IndikatorFitErfolgt volsctändig abhängig von AbovemainMessage, es erscaint jedoc bai Erfolgraicem Fit nit imer aine Message,
											 * Zuusäzlic könte z.B. di Veränderung von resistenzwerte in Defence berüksictigt were.
											 * */

											if (IndikatorFitErfolgt)
											{
												ListeVersuucFitLoadLezte.Wert.EntscaidungErfolg = new SictWertMitZait<bool>(ZaitMili, true);
												FitLoadedLezteNocAktiiv = new SictWertMitZait<string>(ZaitMili, ListeVersuucFitLoadLezte.Wert.FittingBezaicner);
											}
										}
									}
								}
							}
						}
					}
				}

				ScritNääxteJammed = false;
				ScritÜüberNääxteJammed = false;

				var ScritNääxteTimerRestZaitScranke = AutomaatZuusctand.ScritDauerDurcscnit() / 3;
				var ScritÜüberNääxteTimerRestZaitScranke = ScritNääxteTimerRestZaitScranke + AutomaatZuusctand.ScritDauerDurcscnit();

				if (null != ShipUi)
				{
					var ShipUiMengeTimer = ShipUi.MengeTimer;

					if (null != ShipUiMengeTimer)
					{
						foreach (var Timer in ShipUiMengeTimer)
						{
							if (null == Timer)
							{
								continue;
							}

							/*
							 * 2014.10.00
							 * 
							if (!Timer.EWarTypSictEnum.HasValue	||
								Timer.EWarTypSictEnum == SictEWarTypeEnum.Jam)
							 * */
							if (SictEWarTypeEnum.Jam == Timer.EWarTypSictEnum)
							{
								if (ScritNääxteTimerRestZaitScranke < Timer.DauerRestMili)
								{
									ScritNääxteJammed = true;
								}

								if (ScritÜüberNääxteTimerRestZaitScranke < Timer.DauerRestMili)
								{
									ScritÜüberNääxteJammed = true;
								}
							}
						}
					}
				}

				{
					//	Berecnung Gefect Distanz

					foreach (var ModuleRepr in MengeModuleRepr)
					{
						if (null == ModuleRepr)
						{
							continue;
						}

						var ModuleButtonHintNocGültig = ModuleRepr.ModuleButtonHintGültigMitZait;
						var ModuleButtonHintVorherigMitZait = ModuleRepr.ModuleButtonHintVorherigMitZait;

						var ModuleButtonHintVorherigAlterMili = ZaitMili - ModuleButtonHintVorherigMitZait.Zait;

						Int64? FürModuleRangeMax = null;

						var ModuleIstWirkmitelDestrukt = ModuleRepr.IstWirkmitelDestrukt;

						if (null != ModuleButtonHintNocGültig.Wert)
						{
							if (true == ModuleButtonHintNocGültig.Wert.IstAfterburner)
							{
								AnnaameModuleAfterburner = ModuleRepr;
							}

							if (true == ModuleButtonHintNocGültig.Wert.IstShieldBoosterSelbsct)
							{
								AnnaameModuleShieldBoosterSelbsct = ModuleRepr;
							}

							if (true == ModuleButtonHintNocGültig.Wert.IstArmorRepairerSelbsct)
							{
								AnnaameModuleArmorRepairerSelbsct = ModuleRepr;
							}

							FürModuleRangeMax = ModuleButtonHintNocGültig.Wert.RangeMax;
						}

						if (null != ModuleButtonHintVorherigMitZait.Wert.Wert &&
							ModuleButtonHintVorherigAlterMili / 1000 < 19 &&
							!FürModuleRangeMax.HasValue)
						{
							/*
							 * z.B. um für den Fal das Module gerade Ammo Naaclaad di Angaabe wii z.B. AnnaameGefectDistanzMax nit zu verliire werde
							 * di Stats aus kurz vorher gültige ModuleButtonHint übernome.
							 * */

							FürModuleRangeMax = ModuleButtonHintVorherigMitZait.Wert.Wert.RangeMax;
						}

						if (true == ModuleIstWirkmitelDestrukt)
						{
							if (FürModuleRangeMax.HasValue)
							{
								AnnaameModuleDestruktRangeMax = Bib3.Glob.Max(FürModuleRangeMax, AnnaameModuleDestruktRangeMax);
							}
						}
					}

					{
						/*
						 * 2013.09.14
						 * !!!!	Noc zu erseze: Scpeziaalfal für Missile, angaabe klainerer Wert für Optimum um zu verhindere das bai ungenau durcgefüürte Befeele (z.B. Orbit Distance) di Distanz zu groos werd.
						 * */
						AnnaameModuleDestruktRangeOptimum = (AnnaameModuleDestruktRangeMax * 19) / 20 - 400;
					}
				}

				if (null != ScnapscusMengeWindowInventory)
				{
					foreach (var WindowInventory in ScnapscusMengeWindowInventory)
					{
						if (null == WindowInventory)
						{
							continue;
						}

						var AuswaalReczCapacity = WindowInventory.AuswaalReczCapacity;

						if (null == AuswaalReczCapacity)
						{
							continue;
						}

						if (!AuswaalReczCapacity.UsedMikro.HasValue || !AuswaalReczCapacity.MaxMikro.HasValue)
						{
							continue;
						}

						var WindowInventoryLinxTreeEntryActiveShip = WindowInventory.LinxTreeEntryActiveShip;
						var WindowInventoryZuAuswaalReczMengeKandidaatLinxTreeEntry = WindowInventory.ZuAuswaalReczMengeKandidaatLinxTreeEntry;

						if (null == WindowInventoryLinxTreeEntryActiveShip)
						{
							continue;
						}

						var WindowInventoryLinxTreeEntryActiveShipMengeChild = WindowInventoryLinxTreeEntryActiveShip.MengeChild;

						if (null == WindowInventoryLinxTreeEntryActiveShipMengeChild)
						{
							continue;
						}

						if (!(1 == Bib3.Extension.CountNullable(WindowInventoryZuAuswaalReczMengeKandidaatLinxTreeEntry)))
						{
							continue;
						}

						var ZuAuswaalReczTreeEntry = WindowInventoryZuAuswaalReczMengeKandidaatLinxTreeEntry.FirstOrDefault();

						if (null == ZuAuswaalReczTreeEntry)
						{
							continue;
						}

						if (WindowInventoryLinxTreeEntryActiveShipMengeChild.Contains(ZuAuswaalReczTreeEntry))
						{
							if (WindowInventoryLinxTreeEntryActiveShip.TreeEntryBerecneAusCargoTyp(SictShipCargoTypSictEnum.OreHold) ==
								ZuAuswaalReczTreeEntry)
							{
								OreHoldCapacityMesungLezteMitZait = new SictWertMitZait<VonSensor.InventoryCapacityGaugeInfo>(
									ZaitMili, AuswaalReczCapacity);
							}
						}
					}
				}

				AnnaameOreInMengeModuleMinerZyyklusVolumeMili = 0;

				foreach (var Module in MengeModuleRepr)
				{
					var ModuleButtonHintGültigMitZait = Module.ModuleButtonHintGültigMitZait;
					var ModuleZyyklusFortscritMili = Module.RotatioonMiliGefiltertRangordnungBerecne(6, 150);

					if (true == Module.IstMiner && Module.AktiivBerecne(1))
					{
						ModuleMinerAktiivLezteZait = ZaitMili;

						if (null != ModuleButtonHintGültigMitZait.Wert &&
							ModuleZyyklusFortscritMili.HasValue)
						{
							var ModuleVolumeMiliPerTimespanMili = ModuleButtonHintGültigMitZait.Wert.VolumeMiliPerTimespanMili;

							if (ModuleVolumeMiliPerTimespanMili.HasValue)
							{
								var ModuleVolumeInZyyklusMili = ((Int64)ModuleVolumeMiliPerTimespanMili.Value.Key * (ModuleZyyklusFortscritMili ?? 0)) / 1000;

								AnnaameOreInMengeModuleMinerZyyklusVolumeMili += ModuleVolumeInZyyklusMili;
							}
						}
					}
				}

				OreHoldCapacityMesungLezteMitZaitNocGültig = OreHoldCapacityMesungLezteMitZait;

				if (null != OreHoldCapacityMesungLezteMitZaitNocGültig.Wert)
				{
					if (ZaitMili - OreHoldCapacityMesungLezteMitZaitNocGültig.Zait < 1000 * 60 * 15)
					{
						if (OreHoldCapacityMesungLezteMitZaitNocGültig.Wert.UsedMikro < 1)
						{
							if (!(OreHoldCapacityMesungLezteMitZaitNocGültig.Zait < ModuleMinerAktiivLezteZait))
							{
								AnnaameOreHoldLeer = true;

								AnnaameNaacAbbrucMinerZyyklusOreHoldGefültMili = 0;
							}
						}
						else
						{
							AnnaameOreHoldLeer = false;

							AnnaameNaacAbbrucMinerZyyklusOreHoldGefültMili =
								(int?)
								(((AnnaameOreInMengeModuleMinerZyyklusVolumeMili * 1000 + OreHoldCapacityMesungLezteMitZaitNocGültig.Wert.UsedMikro) * 1000) /
								OreHoldCapacityMesungLezteMitZaitNocGültig.Wert.MaxMikro);

						}
					}
				}
			}
			finally
			{
				this.ShipZuusctand = ShipZuusctand;

				this.UndockedMesungShipIsPodLezteZaitMili = UndockedMesungShipTypIstPodLezteZaitMili;

				this.ZaitraumDockedLezteHinraicendAltFürBeurtailungShipZuusctand = ZaitraumDockedLezteHinraicendAltFürBeurtailungShipZuusctand;

				this.ScnapscusVorLezteSelbsctShipZuusctand = ScnapscusSelbsctShipZuusctand;

				this.VorhersaageSelbstScifTreferpunkte = VorhersaageSelbstScifTreferpunkte;

				this.SelbsctShipWarpScrambled = SelbsctShipWarpScrambled;

				this.AnnaameModuleAfterburner = AnnaameModuleAfterburner;
				this.AnnaameModuleShieldBoosterSelbsct = AnnaameModuleShieldBoosterSelbsct;
				this.AnnaameModuleArmorRepairerSelbsct = AnnaameModuleArmorRepairerSelbsct;

				this.AnnaameModuleDestruktRangeMax = AnnaameModuleDestruktRangeMax;
				this.AnnaameModuleDestruktRangeOptimum = AnnaameModuleDestruktRangeOptimum;

				this.ScritNääxteJammed = ScritNääxteJammed;
				this.ScritÜüberNääxteJammed = ScritÜüberNääxteJammed;

				this.AnnaameOreHoldLeer = AnnaameOreHoldLeer;
				this.AnnaameOreInMengeModuleMinerZyyklusVolumeMili = AnnaameOreInMengeModuleMinerZyyklusVolumeMili;
				this.AnnaameNaacAbbrucMinerZyyklusOreHoldGefültMili = AnnaameNaacAbbrucMinerZyyklusOreHoldGefültMili;
			}
		}

		public void AktualisiireTailInventoryMesungShipIsPodLezteZaitUndWert(
			ISictAutomatZuusctand Automaat)
		{
			if (null == Automaat)
			{
				return;
			}

			var NuzerZaitMili = Automaat.NuzerZaitMili;

			var Scnapscus = Automaat.ListeScnapscusLezteAuswertungErgeebnisNaacSimu;

			if (null == Scnapscus)
			{
				return;
			}

			var ScnapscusMengeWindowInventory = Scnapscus.MengeWindowInventory;

			if (null == ScnapscusMengeWindowInventory)
			{
				return;
			}

			foreach (var ScnapscusWindowInventory in ScnapscusMengeWindowInventory)
			{
				if (null == ScnapscusWindowInventory)
				{
					continue;
				}

				var ShipType =
					Bib3.Glob.TrimNullable(
					Optimat.EveOnline.Extension.AusLinxTreeEntryShipExtraktShipType(
					ScnapscusWindowInventory.LinxTreeEntryActiveShip));

				if (ShipType.NullOderLeer())
				{
					continue;
				}

				var IsPod =
					string.Equals("Capsule", ShipType, StringComparison.InvariantCultureIgnoreCase) ||
					string.Equals("Pod", ShipType, StringComparison.InvariantCultureIgnoreCase);

				InventoryMesungShipIsPodLezteZaitUndWert = new SictWertMitZait<bool>(NuzerZaitMili, IsPod);
			}
		}

		static public bool AusGbsMessageGlaicwertig(
			SictVerlaufBeginUndEndeRef<SictMessageStringAuswert> O0,
			SictVerlaufBeginUndEndeRef<SictMessageStringAuswert> O1)
		{
			if (object.ReferenceEquals(O0, O1))
			{
				return true;
			}

			if (null == O0 || null == O1)
			{
				return false;
			}

			return O0.BeginZait == O1.BeginZait;
		}

		public void AktualisiireTailDroneCommandRangeUndDroneCommandCount(
			ISictAutomatZuusctand AutomaatZuusctand,
			VonSensorikMesung AusScnapscusAuswertungZuusctand)
		{
			Int64 AnnaameDroneCommandRange = 80000;
			Int64? AnnaameDroneControlCountScrankeMax = null;
			var ListeMessungDroneCommandRange = this.ListeMessungDroneCommandRange;
			var ListeMessungDroneControlCount = this.ListeMessungDroneControlCount;
			var ListeMessungDroneCommandRangeBerüksictigungAlterScrankeMax = 60 * 15;

			try
			{
				if (null == AutomaatZuusctand)
				{
					return;
				}

				var ZaitMili = AutomaatZuusctand.NuzerZaitMili;

				var ListeMessungDroneCommandRangeBerüksictigungZaitMiliScrankeMin =
					ZaitMili - ListeMessungDroneCommandRangeBerüksictigungAlterScrankeMax * 1000;

				var GbsZuusctand = AutomaatZuusctand.Gbs;

				if (null != GbsZuusctand)
				{
					var ListeAbovemainMessageAuswertMitZait = GbsZuusctand.ListeAbovemainMessageAuswertMitZait;

					if (null != ListeAbovemainMessageAuswertMitZait)
					{
						if (null == ListeMessungDroneCommandRange)
						{
							this.ListeMessungDroneCommandRange = ListeMessungDroneCommandRange =
								new List<SictVerlaufBeginUndEndeRef<SictMessageStringAuswert>>();
						}

						if (null == ListeMessungDroneControlCount)
						{
							this.ListeMessungDroneControlCount = ListeMessungDroneControlCount =
								new List<SictVerlaufBeginUndEndeRef<SictMessageStringAuswert>>();
						}

						var ListeAbovemainMessageAuswertDroneCommandRangeMitZait =
							ListeAbovemainMessageAuswertMitZait
							.Where((Kandidaat) => Kandidaat.Wert.DroneCommandRange.HasValue)
							.ToArray();

						var ListeAbovemainMessageAuswertDroneControlCountMitZait =
							ListeAbovemainMessageAuswertMitZait
							.Where((Kandidaat) => Kandidaat.Wert.DroneControlCountScrankeMax.HasValue)
							.ToArray();

						Bib3.Glob.PropagiireListeRepräsentatioon(
							ListeAbovemainMessageAuswertDroneCommandRangeMitZait,
							ListeMessungDroneCommandRange as IList<SictVerlaufBeginUndEndeRef<SictMessageStringAuswert>>,
							(AusGbsMessageAuswert) => AusGbsMessageAuswert,
							AusGbsMessageGlaicwertig,
							null,
							true);

						Bib3.Glob.PropagiireListeRepräsentatioon(
							ListeAbovemainMessageAuswertDroneControlCountMitZait,
							ListeMessungDroneControlCount as IList<SictVerlaufBeginUndEndeRef<SictMessageStringAuswert>>,
							(AusGbsMessageAuswert) => AusGbsMessageAuswert,
							AusGbsMessageGlaicwertig,
							null,
							true);

						Bib3.Extension.ListeKürzeBegin(ListeMessungDroneCommandRange, 4);
						Bib3.Extension.ListeKürzeBegin(ListeMessungDroneControlCount, 4);
					}
				}

				var OptimatParam = AutomaatZuusctand.OptimatParam();

				if (null != ListeMessungDroneCommandRange)
				{
					//	Lezte zwai Naacricte zu Drone Command Range werde berüksictigt fals diise nit älter sin als Scranke.

					AnnaameDroneCommandRange = Bib3.Glob.Max(
						ListeMessungDroneCommandRange.Skip(
						ListeMessungDroneCommandRange.Count - 2)
						.Where((MessageAuswertMitZait) => ListeMessungDroneCommandRangeBerüksictigungZaitMiliScrankeMin < MessageAuswertMitZait.BeginZait)
						.Select((MessageAuswertMitZait) => MessageAuswertMitZait.Wert.DroneCommandRange)) ?? AnnaameDroneCommandRange;
				}

				if (null != ListeMessungDroneControlCount)
				{
					var ListeMessungDroneControlCountLezte =
						ListeMessungDroneControlCount
						.OrderBy((Kandidaat) => Kandidaat.BeginZait ?? int.MinValue)
						.LastOrDefault();

					if (null != ListeMessungDroneControlCountLezte)
					{
						var ListeMessungDroneControlCountLezteAlterMili = ZaitMili - ListeMessungDroneControlCountLezte.BeginZait;

						if (ListeMessungDroneControlCountLezteAlterMili / 1000 < 60 * 15 &&
							!(ListeMessungDroneControlCountLezte.BeginZait < DockedLezteZaitMili))
						{
							AnnaameDroneControlCountScrankeMax = ListeMessungDroneControlCountLezte.Wert.DroneControlCountScrankeMax;
						}
					}
				}
			}
			finally
			{
				this.AnnaameDroneCommandRange = AnnaameDroneCommandRange;
				this.AnnaameDroneControlCountScrankeMax = AnnaameDroneControlCountScrankeMax;
			}
		}

		static readonly public string MenuEntryLockTargetRegexPattern = "Lock Target";

		public void AktualisiireTailTargetingRange(
			ISictAutomatZuusctand AutomaatZuusctand,
			VonSensorikMesung AusScnapscusAuswertungZuusctand)
		{
			SictWertMitZait<Int64>? ZwisceergeebnisTargetingRangeScrankeMin = null;
			SictWertMitZait<Int64>? ZwisceergeebnisTargetingRangeScrankeMax = null;
			Int64? AnnaameTargetingDistanceScrankeMax = 1000000;

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

				var ZaitMili = AutomaatZuusctand.NuzerZaitMili;

				var ListeMesungZuDistanceIndikatorTargetingVerfügbar = this.ListeMesungZuDistanceIndikatorTargetingVerfügbar;

				var OverviewUndTarget = (null == AutomaatZuusctand) ? null : AutomaatZuusctand.OverviewUndTarget;

				var OverviewMengeObjekt = (null == OverviewUndTarget) ? null : OverviewUndTarget.MengeOverViewObjekt;

				var ShipZuusctand = this.ShipZuusctand;

				if (null != ListeMesungZuDistanceIndikatorTargetingVerfügbar)
				{
					if (null == ShipZuusctand)
					{
						ListeMesungZuDistanceIndikatorTargetingVerfügbar.Clear();
					}
					else
					{
						if (true == ShipZuusctand.Docked)
						{
							ListeMesungZuDistanceIndikatorTargetingVerfügbar.Clear();
						}
					}
				}

				{
					//	Berecnung ListeMesungZuDistanceIndikatorTargetingVerfügbar

					Int64? AusScnapscusMesungTargetingRangeScrankeMin = null;
					Int64? AusScnapscusMesungTargetingRangeScrankeMax = null;

					var MenuEntryIndikatorTargetingVerfügbarRegexPattern = MenuEntryLockTargetRegexPattern;

					if (null == ListeMesungZuDistanceIndikatorTargetingVerfügbar)
					{
						this.ListeMesungZuDistanceIndikatorTargetingVerfügbar =
							ListeMesungZuDistanceIndikatorTargetingVerfügbar = new List<SictWertMitZait<KeyValuePair<Int64, bool>>>();
					}

					var ScnapscusMengeTarget = AusScnapscusAuswertungZuusctand.MengeTarget;

					if (null != ScnapscusMengeTarget)
					{
						foreach (var ScnapscusTarget in ScnapscusMengeTarget)
						{
							if (null == ScnapscusTarget)
							{
								continue;
							}

							var ScnapscusTargetDistanceScrankeMin = ScnapscusTarget.DistanceScrankeMin;

							if (!ScnapscusTargetDistanceScrankeMin.HasValue)
							{
								continue;
							}

							AusScnapscusMesungTargetingRangeScrankeMin =
								Bib3.Glob.Max(AusScnapscusMesungTargetingRangeScrankeMin, ScnapscusTargetDistanceScrankeMin);
						}
					}

					if (null != OverviewMengeObjekt)
					{
						foreach (var OverviewObjekt in OverviewMengeObjekt)
						{
							if (null == OverviewObjekt)
							{
								continue;
							}

							if (true == OverviewObjekt.TargetingOderTargeted)
							{
								//	mööglicerwaise isc MenuEntry "Lock Target" dan scun nit meer in Menu enthalte -> diises Objekt kan nit als Indikaator verwendet werde.
								continue;
							}

							var OverviewObjektDistanceScrankeMin = OverviewObjekt.SictungLezteDistanceScrankeMinScpezOverview;
							var OverviewObjektDistanceScrankeMax = OverviewObjekt.SictungLezteDistanceScrankeMaxScpezOverview;

							if (!OverviewObjektDistanceScrankeMin.HasValue || !OverviewObjektDistanceScrankeMax.HasValue)
							{
								continue;
							}

							var OverviewObjektMenuLezteMitZait = OverviewObjekt.MenuLezteMitZait;

							if (null == OverviewObjektMenuLezteMitZait)
							{
								continue;
							}

							if (OverviewObjektMenuLezteMitZait.BeginZait < OverviewObjekt.AktualisLezteZait)
							{
								//	Menu scon in vorherige Scrit vorhande geweese, nur noie Menu sole berüksictigt werde.
								continue;
							}

							var OverviewObjektMenuLezte = OverviewObjektMenuLezteMitZait.Wert;

							if (null == OverviewObjektMenuLezte)
							{
								continue;
							}

							var OverviewObjektMenuLezteMengeEntry = OverviewObjektMenuLezte.ListeEntry;

							if (null == OverviewObjektMenuLezteMengeEntry)
							{
								continue;
							}

							var OverviewObjektMenuLezteEntryLockTarget =
								OverviewObjektMenuLezteMengeEntry
								.FirstOrDefault((KandidaatEntry) =>
									{
										var KandidaatEntryBescriftung = KandidaatEntry.Bescriftung;

										if (null == KandidaatEntryBescriftung)
										{
											return false;
										}

										return Regex.Match(KandidaatEntryBescriftung,
											MenuEntryIndikatorTargetingVerfügbarRegexPattern, RegexOptions.IgnoreCase).Success;
									});

							var TargetingVerfüügbar = null != OverviewObjektMenuLezteEntryLockTarget;

							ListeMesungZuDistanceIndikatorTargetingVerfügbar.Add(new SictWertMitZait<KeyValuePair<Int64, bool>>(
								ZaitMili, new KeyValuePair<Int64, bool>(
									OverviewObjektDistanceScrankeMin.Value, TargetingVerfüügbar)));
						}
					}

					if (AusScnapscusMesungTargetingRangeScrankeMin.HasValue)
					{
						ListeMesungZuDistanceIndikatorTargetingVerfügbar.Add(new SictWertMitZait<KeyValuePair<Int64, bool>>(
							ZaitMili, new KeyValuePair<Int64, bool>(AusScnapscusMesungTargetingRangeScrankeMin.Value, true)));
					}

					if (AusScnapscusMesungTargetingRangeScrankeMax.HasValue)
					{
						ListeMesungZuDistanceIndikatorTargetingVerfügbar.Add(new SictWertMitZait<KeyValuePair<Int64, bool>>(
							ZaitMili, new KeyValuePair<Int64, bool>(AusScnapscusMesungTargetingRangeScrankeMax.Value, false)));
					}
				}

				{
					//	Berecnung AnnaameTargetingDistanceScrankeMax

					var BerecnungTargetingDistanceScrankeZaitScranke = ZaitMili - 15 * 60 * 1000;

					/*
					 * Ain ainzelner Meswert zu TargetingRangeScranke bringt kaine Sicerhait da geleegentlic auc zuufälige Werte gemesen werden.
					 * Daher werden di in ListeMesungZuDistanceIndikatorTargetingVerfügbar zwiscegescpaicerte Meswerte (ScrankeMin oder ScrankeMax zu
					 * Zaitpunkt) kombiniirt um aine Vorhersaage für di Scranke zu berecne.
					 * */

					/*
					 * 2014.01.05
					 * 
					AusListeMesungScrankeZuZaitOrdnetBerecneScrankeMitSicerhaitIndex(
						ListeMesungZuDistanceIndikatorTargetingVerfügbar.Reversed(),
						2,
						out	ZwisceergeebnisTargetingRangeScrankeMin,
						out	ZwisceergeebnisTargetingRangeScrankeMax,
						BerecnungTargetingDistanceScrankeZaitScranke,
						true);

					if (ZwisceergeebnisTargetingRangeScrankeMax.HasValue)
					{
						var ScrankeMaxAlter = (ZaitMili - ZwisceergeebnisTargetingRangeScrankeMax.Value.Zait) / 1000;

						AnnaameTargetingDistanceScrankeMax =
							Math.Min(AnnaameTargetingDistanceScrankeMax.Value, ZwisceergeebnisTargetingRangeScrankeMax.Value.Wert);

						AnnaameTargetingDistanceScrankeMax +=
							(AnnaameTargetingDistanceScrankeMax / 300) * (ScrankeMaxAlter - 600) / 60;
					}
					 * */


					var AnnaameTargetingRangeScrankeMaxDriftZaitBisGlaicsctand = 600;

					var AnnaameTargetingRangeScrankeMaxDriftBeginProzent = 5;

					/*
					 * 2014.01.18
					 * 
					var ListeScrankeMinMax =
						VonListeMesungZuDistanceIndikatorVerfügbarNaacListeZuZaitScrankeMinMax(
						ListeMesungZuDistanceIndikatorTargetingVerfügbar,
						ZaitMili + 1000 * AnnaameTargetingRangeScrankeMaxDriftZaitBisGlaicsctand,
						(int)((AnnaameTargetingRangeScrankeMaxDriftBeginProzent * 1e+7) / AnnaameTargetingRangeScrankeMaxDriftZaitBisGlaicsctand));

					AusListeMesungScrankeZuZaitOrdnetBerecneScrankeMitSicerhaitIndex(
						ListeScrankeMinMax.Reversed(),
						2,
						out	ZwisceergeebnisTargetingRangeScrankeMin,
						out	ZwisceergeebnisTargetingRangeScrankeMax,
						BerecnungTargetingDistanceScrankeZaitScranke);
					 * */

					var FixpunktZait = ZaitMili - 1000 * AnnaameTargetingRangeScrankeMaxDriftZaitBisGlaicsctand;
					var ScrankeMaxAbwaicungMiliardstel = -(int)((AnnaameTargetingRangeScrankeMaxDriftBeginProzent * 1e+3) / AnnaameTargetingRangeScrankeMaxDriftZaitBisGlaicsctand);

					VonListeMesungZuDistanceIndikatorVerfügbarBerecneScrankeMitSicerhaitIndex(
						ListeMesungZuDistanceIndikatorTargetingVerfügbar,
						FixpunktZait,
						ScrankeMaxAbwaicungMiliardstel,
						2,
						out	ZwisceergeebnisTargetingRangeScrankeMin,
						out	ZwisceergeebnisTargetingRangeScrankeMax,
						BerecnungTargetingDistanceScrankeZaitScranke);

					if (ZwisceergeebnisTargetingRangeScrankeMax.HasValue)
					{
						AnnaameTargetingDistanceScrankeMax =
							Math.Min(AnnaameTargetingDistanceScrankeMax.Value, ZwisceergeebnisTargetingRangeScrankeMax.Value.Wert);
					}

					if (ZwisceergeebnisTargetingRangeScrankeMin.HasValue)
					{
						AnnaameTargetingDistanceScrankeMax =
							Math.Max(AnnaameTargetingDistanceScrankeMax ?? 0, ZwisceergeebnisTargetingRangeScrankeMin.Value.Wert);
					}

					var ListeMesungZuDistanceIndikatorTargetingVerfügbarRäumeAufZaitScranke =
						BerecnungTargetingDistanceScrankeZaitScranke;

					/*
					 * 2014.01.01
					 * Entferne der Ainträäge werd waiter unte für Min und Max getrent erleedigt.
					 * 
					if (ZwisceergeebnisTargetingRangeScrankeMin.HasValue && ZwisceergeebnisTargetingRangeScrankeMax.HasValue)
					{
						ListeMesungZuDistanceIndikatorTargetingVerfügbarRäumeAufZaitScranke =
							Math.Max(ListeMesungZuDistanceIndikatorTargetingVerfügbarRäumeAufZaitScranke,
							Math.Min(
							ZwisceergeebnisTargetingRangeScrankeMin.Value.Zait,
							ZwisceergeebnisTargetingRangeScrankeMax.Value.Zait));
					}
					 * */

					ListeMesungZuDistanceIndikatorTargetingVerfügbar
						.RemoveAll((Kandidaat) => Kandidaat.Zait < ListeMesungZuDistanceIndikatorTargetingVerfügbarRäumeAufZaitScranke);

					if (ZwisceergeebnisTargetingRangeScrankeMin.HasValue)
					{
						//	Ale Elemente mit Indikator ScrankeMin entferne welce nit meer zu ScrankeMin baigetraage hän.

						ListeMesungZuDistanceIndikatorTargetingVerfügbar
							.RemoveAll((Kandidaat) =>
								Kandidaat.Wert.Value &&
								Kandidaat.Zait < ZwisceergeebnisTargetingRangeScrankeMin.Value.Zait);
					}

					if (ZwisceergeebnisTargetingRangeScrankeMax.HasValue)
					{
						//	Ale Elemente mit Indikator ScrankeMax entferne welce nit meer zu ScrankeMax baigetraage hän.

						ListeMesungZuDistanceIndikatorTargetingVerfügbar
							.RemoveAll((Kandidaat) =>
								!Kandidaat.Wert.Value &&
								Kandidaat.Zait < ZwisceergeebnisTargetingRangeScrankeMax.Value.Zait);
					}
				}
			}
			finally
			{
				this.ZwisceergeebnisTargetingRangeScrankeMin = ZwisceergeebnisTargetingRangeScrankeMin;
				this.ZwisceergeebnisTargetingRangeScrankeMax = ZwisceergeebnisTargetingRangeScrankeMax;

				this.AnnaameTargetingDistanceScrankeMax = AnnaameTargetingDistanceScrankeMax;
			}
		}

		static public void VonListeMesungZuDistanceIndikatorVerfügbarBerecneScrankeMitSicerhaitIndex(
			IEnumerable<SictWertMitZait<KeyValuePair<Int64, bool>>> ListeMesungZuDistanceIndikatorVerfügbar,
			Int64 ScrankeMaxTransformFixpunktZait,
			int ScrankeMaxAbwaicungMiliardstel,
			int SicerhaitIndex,
			out	SictWertMitZait<Int64>? ScrankeMin,
			out	SictWertMitZait<Int64>? ScrankeMax,
			Int64? ZaitScrankeMin = null)
		{
			var ListeScrankeMinMax =
				VonListeMesungZuDistanceIndikatorVerfügbarNaacListeZuZaitScrankeMinMax(
				ListeMesungZuDistanceIndikatorVerfügbar,
				ScrankeMaxTransformFixpunktZait,
				ScrankeMaxAbwaicungMiliardstel);

			AusListeMesungScrankeZuZaitOrdnetBerecneScrankeMitSicerhaitIndex(
				ListeScrankeMinMax.Reversed(),
				SicerhaitIndex,
				out	ScrankeMin,
				out	ScrankeMax,
				ZaitScrankeMin);
		}

		/// <summary>
		/// Der Parameter vom Typ Bool werd negiirt (Verfüügbar -> ScrankeMin).
		/// Für Scranke Max werden di Beträäge proportionaal zu Zait verscoobe. Der Fixpunkt ist der Mespunkt desen Zait = <paramref name="ScrankeMaxTransformFixpunktZait"/>.
		/// </summary>
		/// <param name="ListeMesungZuDistanceIndikatorVerfügbar"></param>
		/// <param name="ScrankeMaxTransformFixpunktZait"></param>
		/// <param name="ScrankeMaxAbwaicungMiliardstel"></param>
		/// <returns></returns>
		static public IEnumerable<SictWertMitZait<KeyValuePair<Int64, bool>>> VonListeMesungZuDistanceIndikatorVerfügbarNaacListeZuZaitScrankeMinMax(
			IEnumerable<SictWertMitZait<KeyValuePair<Int64, bool>>> ListeMesungZuDistanceIndikatorVerfügbar,
			Int64 ScrankeMaxTransformFixpunktZait,
			int ScrankeMaxAbwaicungMiliardstel = 0)
		{
			if (null == ListeMesungZuDistanceIndikatorVerfügbar)
			{
				return null;
			}

			var ListeZuZaitScrankeMinMax = new List<SictWertMitZait<KeyValuePair<Int64, bool>>>();

			foreach (var ZuZaitMesungDistanceIndikatorVerfügbar in ListeMesungZuDistanceIndikatorVerfügbar)
			{
				var IndikatorVerfügbar = ZuZaitMesungDistanceIndikatorVerfügbar.Wert.Value;

				var ScrankeMax = !IndikatorVerfügbar;

				var ScrankeBetraagVorSceerung = ZuZaitMesungDistanceIndikatorVerfügbar.Wert.Key;

				var ScrankeBetraag = ScrankeBetraagVorSceerung;

				if (ScrankeMax)
				{
					ScrankeBetraag =
						ScrankeBetraagVorSceerung +
						(Int64)(((ZuZaitMesungDistanceIndikatorVerfügbar.Zait - ScrankeMaxTransformFixpunktZait) * ScrankeBetraagVorSceerung * ScrankeMaxAbwaicungMiliardstel) / 1e+9);
				}

				ListeZuZaitScrankeMinMax.Add(new SictWertMitZait<KeyValuePair<Int64, bool>>(
					ZuZaitMesungDistanceIndikatorVerfügbar.Zait,
					new KeyValuePair<Int64, bool>(ScrankeBetraag, ScrankeMax)));
			}

			return ListeZuZaitScrankeMinMax;
		}

		/// <summary>
		/// Für Scranke Min und Scranke Max für aine Dimensioon.
		/// Ainzelne Mesungen der Scranke werde geegenainander verrecnet.
		/// </summary>
		/// <param name="ListeMesungScrankeOrdnet">
		/// Erwartete Ordnung: Frühere Elemente mit gröösere Zait.
		/// Wen true=Wert.Value dan isc des ScrankeMax</param>
		/// </param>
		/// <param name="SicerhaitIndex"></param>
		/// <param name="ScrankeMin"></param>
		/// <param name="ScrankeMax"></param>
		/// <param name="ZaitScrankeMin"></param>
		static public void AusListeMesungScrankeZuZaitOrdnetBerecneScrankeMitSicerhaitIndex(
			IEnumerable<SictWertMitZait<KeyValuePair<Int64, bool>>> ListeMesungScrankeOrdnet,
			int SicerhaitIndex,
			out	SictWertMitZait<Int64>? ScrankeMin,
			out	SictWertMitZait<Int64>? ScrankeMax,
			Int64? ZaitScrankeMin = null,
			bool ScrankeTypUmkeer = false)
		{
			ScrankeMin = null;
			ScrankeMax = null;

			if (null == ListeMesungScrankeOrdnet)
			{
				return;
			}

			if (SicerhaitIndex < 1)
			{
				return;
			}

			var ScrankeKomponenteAnzaalMax = SicerhaitIndex * 2 - 1;

			/*
			 * 2014.01.00
			 * 
			var ZuScrankeMinAnzaalScrankeBetraag = new Int64[SicerhaitIndex * 2 - 1];
			var ZuScrankeMaxAnzaalScrankeBetraag = new Int64[SicerhaitIndex * 2 - 1];

			for (int i = 0; i < ZuScrankeMaxAnzaalScrankeBetraag.Length; i++)
			{
				ZuScrankeMaxAnzaalScrankeBetraag[i] = Int64.MaxValue;
			}
			 * */

			IEnumerable<SictWertMitZait<Int64>> ZuScrankeMinAnzaalScrankeBetraag = null;
			IEnumerable<SictWertMitZait<Int64>> ZuScrankeMaxAnzaalScrankeBetraag = null;

			foreach (var Scranke in ListeMesungScrankeOrdnet)
			{
				if (Scranke.Zait < ZaitScrankeMin)
				{
					break;
				}

				var ScrankeArray = new SictWertMitZait<Int64>[] { new SictWertMitZait<Int64>(Scranke.Zait, Scranke.Wert.Key) };

				var ScrankeIscScrankeMin = Scranke.Wert.Value == ScrankeTypUmkeer;

				if (ScrankeIscScrankeMin)
				{
					ZuScrankeMinAnzaalScrankeBetraag =
						(null == ZuScrankeMinAnzaalScrankeBetraag) ?
						ScrankeArray :
						ZuScrankeMinAnzaalScrankeBetraag
						.Concat(ScrankeArray)
						.OrderByDescending((ScrankeBetraag) => ScrankeBetraag.Zait)
						.OrderByDescending((ScrankeBetraag) => ScrankeBetraag.Wert)
						.Take(ScrankeKomponenteAnzaalMax)
						.ToArray();
				}
				else
				{
					ZuScrankeMaxAnzaalScrankeBetraag =
						(null == ZuScrankeMaxAnzaalScrankeBetraag) ?
						ScrankeArray :
						ZuScrankeMaxAnzaalScrankeBetraag
						.Concat(ScrankeArray)
						.OrderByDescending((ScrankeBetraag) => ScrankeBetraag.Zait)
						.OrderBy((ScrankeBetraag) => ScrankeBetraag.Wert)
						.Take(ScrankeKomponenteAnzaalMax)
						.ToArray();
				}

				/*
				 * 2014.01.00
				 * 
				var	ScrankeMinTotal	= Int64.MinValue;
				var	ScrankeMaxTotal	= Int64.MaxValue;
				 * */

				if (SicerhaitIndex <= Bib3.Extension.CountNullable(ZuScrankeMinAnzaalScrankeBetraag))
				{
					/*
					 * 2014.01.00
					 * 
					ScrankeMinTotal	= ZuScrankeMinAnzaalScrankeBetraag.ElementAt(SicerhaitIndex - 1).Wert;
					 * */

					var ListeVarianteScrankeMin = new SictWertMitZait<Int64>[ZuScrankeMinAnzaalScrankeBetraag.Count() - SicerhaitIndex + 1];

					for (int i = 0; i < ListeVarianteScrankeMin.Length; i++)
					{
						var VarianteInListeAnzaalIndex = i + SicerhaitIndex - 1;

						var VarianteScrankeMin = ZuScrankeMinAnzaalScrankeBetraag.ElementAt(VarianteInListeAnzaalIndex);

						var VarianteScrankeMax = Int64.MaxValue;

						if (VarianteInListeAnzaalIndex < Bib3.Extension.CountNullable(ZuScrankeMaxAnzaalScrankeBetraag))
						{
							VarianteScrankeMax = ZuScrankeMaxAnzaalScrankeBetraag.ElementAt(VarianteInListeAnzaalIndex).Wert;
						}

						ListeVarianteScrankeMin[i] = new SictWertMitZait<Int64>(
							VarianteScrankeMin.Zait, Math.Min(VarianteScrankeMin.Wert, VarianteScrankeMax));
					}

					var KandidaatErsazScrankeMin =
						ListeVarianteScrankeMin.OrderByDescending((VarianteScrankeMin) => VarianteScrankeMin.Wert).FirstOrDefault();

					if (ScrankeMin.HasValue)
					{
						if (ScrankeMin.Value.Wert < KandidaatErsazScrankeMin.Wert)
						{
							ScrankeMin = KandidaatErsazScrankeMin;
						}
					}
					else
					{
						ScrankeMin = KandidaatErsazScrankeMin;
					}
				}

				if (SicerhaitIndex <= Bib3.Extension.CountNullable(ZuScrankeMaxAnzaalScrankeBetraag))
				{
					var ListeVarianteScrankeMax = new SictWertMitZait<Int64>[ZuScrankeMaxAnzaalScrankeBetraag.Count() - SicerhaitIndex + 1];

					for (int i = 0; i < ListeVarianteScrankeMax.Length; i++)
					{
						var VarianteInListeAnzaalIndex = i + SicerhaitIndex - 1;

						var VarianteScrankeMax = ZuScrankeMaxAnzaalScrankeBetraag.ElementAt(VarianteInListeAnzaalIndex);

						var VarianteScrankeMin = Int64.MinValue;

						if (VarianteInListeAnzaalIndex < Bib3.Extension.CountNullable(ZuScrankeMinAnzaalScrankeBetraag))
						{
							VarianteScrankeMin = ZuScrankeMinAnzaalScrankeBetraag.ElementAt(VarianteInListeAnzaalIndex).Wert;
						}

						ListeVarianteScrankeMax[i] = new SictWertMitZait<Int64>(
							VarianteScrankeMax.Zait, Math.Max(VarianteScrankeMax.Wert, VarianteScrankeMin));
					}

					var KandidaatErsazScrankeMax =
						ListeVarianteScrankeMax.OrderBy((VarianteScrankeMax) => VarianteScrankeMax.Wert).FirstOrDefault();

					if (ScrankeMax.HasValue)
					{
						if (KandidaatErsazScrankeMax.Wert < ScrankeMax.Value.Wert)
						{
							ScrankeMax = KandidaatErsazScrankeMax;
						}
					}
					else
					{
						ScrankeMax = KandidaatErsazScrankeMax;
					}
				}

				if (ScrankeMin.HasValue && ScrankeMax.HasValue)
				{
					if (ScrankeMax.Value.Wert <= ScrankeMin.Value.Wert)
					{
						break;
					}
				}
			}
		}

	}
}
