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
using Optimat.EveOnline.VonSensor;
using ExtractFromOldAssembly.Bib3;

namespace Optimat.EveOnline.Anwendung
{
	public partial class SictAgentUndMissionZuusctand
	{
		public void Aktualisiire(
			SictAutomatZuusctand AutomaatZuusctand,
			VonSensorikMesung AusScnapscusAuswertungZuusctand,
			Int64 ZuAgentMissionInfoLezteAlterScrankeMax = 1800)
		{
			if (null == AutomaatZuusctand)
			{
				return;
			}

			var ZaitMili = AutomaatZuusctand.NuzerZaitMili;

			var OptimatParam = AutomaatZuusctand.OptimatParam();

			var VonNuzerParamMission = (null == OptimatParam) ? null : OptimatParam.Mission;

			var VonNuzerParamMissionMengeZuFactionFittingBezaicner = (null == VonNuzerParamMission) ? null : VonNuzerParamMission.MengeZuFactionFittingBezaicner;

			VonNuzerParamScpezVerzictAufCargoLeere = VonNuzerParamMissionMengeZuFactionFittingBezaicner.IsNullOrEmpty();

			AktualisiireZuusctandAusScnapscusAuswertungTailAgent(
				AusScnapscusAuswertungZuusctand,
				ZaitMili,
				OptimatParam,
				ZuAgentMissionInfoLezteAlterScrankeMax);

			var Gbs = AutomaatZuusctand.Gbs;

			var MengeAufgaabeZuusctand = AutomaatZuusctand.MengeAufgaabeZuusctand;

			var MengeWindowAgentDialogue =
				(null == Gbs) ? null :
				ExtractFromOldAssembly.Bib3.Extension.WhereNullable(
				Gbs.MengeWindow, (KandidaatWindow) => KandidaatWindow.AingangScnapscusTailObjektIdentLezteBerecne() is VonSensor.WindowAgentDialogue)
				.ToArrayNullable();

			this.MengeWindowAgentDialogue = MengeWindowAgentDialogue;

			var AusScnapscusGbsInfoPanelMissions =
				(null == AusScnapscusAuswertungZuusctand) ? null : AusScnapscusAuswertungZuusctand.InfoPanelMissions;

			var ScnapscusCharacterAuswaalAbgesclose =
				(null == AusScnapscusAuswertungZuusctand) ? false :
				AusScnapscusAuswertungZuusctand.CharacterAuswaalAbgesclose;

			var AusScnapscusMengeMissionButton =
				(null == AusScnapscusGbsInfoPanelMissions) ? null : AusScnapscusGbsInfoPanelMissions.ListeMissionButton;

			var AusScnapscusGbsInfoPanelMissionsExpanded =
				(null == AusScnapscusGbsInfoPanelMissions) ? null : AusScnapscusGbsInfoPanelMissions.Expanded;

			var MengeZuInfoPanelTypeButtonUndInfoPanel =
				(null == AusScnapscusAuswertungZuusctand) ? null :
				AusScnapscusAuswertungZuusctand.MengeZuInfoPanelTypeButtonUndInfoPanel();

			var InfoPanelAgentMission =
				(null == AusScnapscusAuswertungZuusctand) ? null :
				AusScnapscusAuswertungZuusctand.InfoPanelMissions;

			var MengeMission = this.MengeMission;

			var ListeMissionDeclineZait = this.ListeMissionDeclineZait;

			if (null != MengeAufgaabeZuusctand)
			{
				//	Berecnung ListeMissionDeclineZait

				foreach (var AufgaabeZuusctand in MengeAufgaabeZuusctand)
				{
					if (null == AufgaabeZuusctand)
					{
						continue;
					}

					var MengeAufgaabeKomponenteMissionDecline =
						AufgaabeZuusctand.MengeKomponenteBerecneTransitiivTailmengeAufgaabeParamPasendZuPrädikaat<AufgaabeParamAndere>(
						(KandidaatAufgaabeParam) => null == KandidaatAufgaabeParam ? false : (null != KandidaatAufgaabeParam.MissionDecline),
						int.MinValue);

					if (null == MengeAufgaabeKomponenteMissionDecline)
					{
						continue;
					}

					foreach (var AufgaabeKomponenteMissionDecline in MengeAufgaabeKomponenteMissionDecline)
					{
						var AufgaabeKomponenteMissionDeclineAbsclusTailWirkungZait = AufgaabeKomponenteMissionDecline.AbsclusTailWirkungZait;

						if (!AufgaabeKomponenteMissionDeclineAbsclusTailWirkungZait.HasValue)
						{
							continue;
						}

						if (null == ListeMissionDeclineZait)
						{
							this.ListeMissionDeclineZait = ListeMissionDeclineZait = new List<Int64>();
						}

						if (ListeMissionDeclineZait.Contains(AufgaabeKomponenteMissionDeclineAbsclusTailWirkungZait.Value))
						{
							continue;
						}

						ListeMissionDeclineZait.Add(AufgaabeKomponenteMissionDeclineAbsclusTailWirkungZait.Value);
					}
				}
			}

			Bib3.Extension.ListeKürzeBegin(ListeMissionDeclineZait, 30);

			var ListeMissionDeclineAlterMili =
				ExtractFromOldAssembly.Bib3.Extension.SelectNullable(
				ListeMissionDeclineZait,
				(DeclineZait) => ZaitMili - DeclineZait)
				.ToArrayNullable();

			//	Fraigaabe wen in lezte Sctunde weeniger als 8 Decline durcgefüürt wurde.
			MissionDeclineUnabhängigVonStandingLossFraigaabe =
				!(8 < ExtractFromOldAssembly.Bib3.Extension.CountNullable(
				ListeMissionDeclineAlterMili,
				(DeclineAlterMili) => DeclineAlterMili < 1000 * 60 * 60));

			var AusInfoPanelListeMissionTitel = new List<string>();

			if (null != AusScnapscusGbsInfoPanelMissions)
			{
				if (null != AusScnapscusMengeMissionButton)
				{
					InfoPanelMengeMissionButtonAusScnapscusVorherig = InfoPanelMengeMissionButtonAusScnapscusAktuel;

					InfoPanelMengeMissionButtonAusScnapscusAktuel =
						new SictWertMitZait<VonSensor.InfoPanelMissionsMission[]>(
						ZaitMili,
						AusScnapscusMengeMissionButton
						.Where((Kandidaat) => FilterÜbernaameNaacAutomaat(Kandidaat))
						.ToArray());

					var VorherInfoPanelMengeMissionButtonAktuel = this.InfoPanelMengeMissionButtonAktuel;

					if (AusGbsMengeMissionButtonHinraicendGlaicwertigFürAnnaameVolsctändig(
						InfoPanelMengeMissionButtonAusScnapscusVorherig.Wert,
						InfoPanelMengeMissionButtonAusScnapscusAktuel.Wert))
					{
						this.InfoPanelMengeMissionButtonAktuel = InfoPanelMengeMissionButtonAusScnapscusAktuel;
					}

					for (int MissionButtonIndex = 0; MissionButtonIndex < AusScnapscusMengeMissionButton.Length; MissionButtonIndex++)
					{
						var MissionButton = AusScnapscusMengeMissionButton[MissionButtonIndex];

						if (null == MissionButton)
						{
							continue;
						}

						var MissionTitel = MissionButton.Bescriftung;

						if (null == MissionTitel)
						{
							continue;
						}

						MissionTitel = MissionTitel.Trim();

						AusInfoPanelListeMissionTitel.Add(MissionTitel);

						SictMissionZuusctand Mission = null;

						if (null != MengeMission)
						{
							Mission =
								MengeMission
								.FirstOrDefault((Kandidaat) => string.Equals((Kandidaat.Titel() ?? "").Trim(), MissionTitel, StringComparison.InvariantCultureIgnoreCase));
						}
					}
				}
			}

			if (null != MengeMission)
			{
				//	Fertige Mission aus MengeMission entferne wen Zait Distanz zur Fertigsctelung hinraicend groos.

				var MissionFertigAlterScrankeMax = 60;

				var MissionEndeZaitScrankeMinMili = ZaitMili - MissionFertigAlterScrankeMax * 1000;
				//	(fertige Mission werd nit sofort entfernt damit zu diise Mission noc Benaacrictigung über lezte Zuusctand naac Nuzer erfolge kan.)

				Int64? MissionEntferntLezteEndeZaitMili;

				AusMengeMissionEntferneMitEndeZaitMiliKlainerScranke(
					MengeMission,
					MissionEndeZaitScrankeMinMili,
					out	MissionEntferntLezteEndeZaitMili);

				if (!(true == VonNuzerParamScpezVerzictAufCargoLeere))
				{
					this.AnforderungActiveShipCargoLeereLezteZaitMili =
						Bib3.Glob.Max(this.AnforderungActiveShipCargoLeereLezteZaitMili, MissionEntferntLezteEndeZaitMili);
				}

				foreach (var Mission in MengeMission)
				{
					if (null == Mission)
					{
						continue;
					}

					var MissionTailFürNuzer = Mission.TailFürNuzer;

					if (null == MissionTailFürNuzer)
					{
						continue;
					}

					MissionTailFürNuzer.AktioonFüüreAusAktiiv = false;
					MissionTailFürNuzer.VersuucFittingAktiiv = false;

					var ZuMissionVerhalte =
						(null == VonNuzerParamMission) ? null :
						VonNuzerParamMission.ZuMissionVerhalteBerecne(MissionTailFürNuzer);

					MissionTailFürNuzer.AusPräferenzEntscaidungVerhalte = ZuMissionVerhalte;
				}
			}

			if ((null != AusScnapscusGbsInfoPanelMissions ||
				null == InfoPanelAgentMission) &&
				(ScnapscusCharacterAuswaalAbgesclose ?? false))
			{
				//	Erkenung Mission Ende

				if (null != MengeMission)
				{
					foreach (var Mission in MengeMission)
					{
						if (null == Mission)
						{
							continue;
						}

						var MissionTailFürNuzer = Mission.TailFürNuzer;

						if (null == MissionTailFürNuzer)
						{
							continue;
						}

						if (MissionTailFürNuzer.EndeZaitMili.HasValue)
						{
							//	Mission wurde beraits beendet.
							continue;
						}

						Int64? MissionEndeZaitMili = null;

						try
						{
							var MissionWindowAgentDialogueZuZaitNulbar = Mission.WindowAgentDialogueZuZaitLezteBerecne();

							if (!MissionWindowAgentDialogueZuZaitNulbar.HasValue)
							{
								MissionEndeZaitMili = ZaitMili;
								continue;
							}

							var MissionWindowAgentDialogueZuZait = MissionWindowAgentDialogueZuZaitNulbar.Value;

							if (MissionTailFürNuzer.AcceptedFrühesteZaitMili.HasValue)
							{
								//	Mission wurde beraits Accepted.

								if (AusInfoPanelListeMissionTitel
									.Any((AusInfoPanelMissionTitel) => string.Equals(AusInfoPanelMissionTitel, Mission.Titel(), StringComparison.InvariantCultureIgnoreCase)) ||
									(!(true == AusScnapscusGbsInfoPanelMissionsExpanded) && (null != InfoPanelAgentMission)))
								{
									Mission.ZaitraumNictInInfoPanelSictbarBeginMili = null;
								}
								else
								{
									if (Mission.ZaitraumNictInInfoPanelSictbarBeginMili.HasValue)
									{
										/*
										 * 2014.02.09
										 * Beobactung bai Jump zwai aufainanderfolgende Scnapscus mit in InfoPanelMission Liste MissionKnopf = leer
										 * Vergrööserung Zait Distanz von 3333 auf 8888
										 * */
										if (Mission.ZaitraumNictInInfoPanelSictbarBeginMili.Value < ZaitMili - 8888)
										{
											MissionEndeZaitMili = Mission.ZaitraumNictInInfoPanelSictbarBeginMili;
											continue;
										}
									}
									else
									{
										Mission.ZaitraumNictInInfoPanelSictbarBeginMili = ZaitMili;
									}
								}
							}

							{
								//	Mission beende fals von glaice Agent andere Mission zu scpäätere Zait angezaigt wurde.

								Int64? MengeMissionTailmengeVonAgentUndJüngerOfferFrühesteZait = null;

								foreach (var KandidaatMission in MengeMission)
								{
									if (null == KandidaatMission)
									{
										continue;
									}

									var KandidaatMissionTailFürNuzer = KandidaatMission.TailFürNuzer;

									if (null == KandidaatMissionTailFürNuzer)
									{
										continue;
									}

									if (!string.Equals(MissionTailFürNuzer.AgentName, KandidaatMissionTailFürNuzer.AgentName))
									{
										continue;
									}

									if (KandidaatMission == Mission)
									{
										continue;
									}

									var KandidaatMissionSictungZaitFrüheste = KandidaatMissionTailFürNuzer.SictungFrühesteZaitMili;

									if (!KandidaatMissionSictungZaitFrüheste.HasValue)
									{
										continue;
									}

									if (KandidaatMissionSictungZaitFrüheste <= MissionTailFürNuzer.SictungFrühesteZaitMili)
									{
										continue;
									}

									MengeMissionTailmengeVonAgentUndJüngerOfferFrühesteZait =
										Bib3.Glob.Min(MengeMissionTailmengeVonAgentUndJüngerOfferFrühesteZait,
										KandidaatMissionSictungZaitFrüheste);
								}

								if (MengeMissionTailmengeVonAgentUndJüngerOfferFrühesteZait.HasValue)
								{
									MissionEndeZaitMili = MengeMissionTailmengeVonAgentUndJüngerOfferFrühesteZait;
									continue;
								}
							}
						}
						finally
						{
							if (MissionEndeZaitMili.HasValue)
							{
								MissionTailFürNuzer.EndeZaitMili = MissionEndeZaitMili;

								var ListeMesungObjectiveZuusctandZuZaitLezteNulbar =
									Mission.ListeMesungObjectiveZuusctandZuZaitLezteBerecne();

								var ListeMesungObjectiveZuusctandZuZaitLezte =
									ListeMesungObjectiveZuusctandZuZaitLezteNulbar.HasValue ?
									ListeMesungObjectiveZuusctandZuZaitLezteNulbar.Value :
									default(SictWertMitZait<VonSensor.WindowAgentMissionInfo>);

								var MissionObjective = (null == ListeMesungObjectiveZuusctandZuZaitLezte.Wert) ? null : ListeMesungObjectiveZuusctandZuZaitLezte.Wert.Objective;

								if (null != MissionObjective)
								{
									if (true == MissionObjective.Complete)
									{
										MissionTailFürNuzer.CompleteErfolg = true;
									}
								}
							}
						}
					}
				}
			}

			var UtilmenuMissionLezteNocOfeMitBeginZait = default(SictWertMitZait<VonSensor.UtilmenuMissionInfo>);

			{
				var ScnapcusUtilmenuMission =
					(null == AusScnapscusAuswertungZuusctand) ? null :
					AusScnapscusAuswertungZuusctand.UtilmenuMission;

				if (null != ScnapcusUtilmenuMission)
				{
					var ScnapcusUtilmenuMissionMitZait = new SictWertMitZait<VonSensor.UtilmenuMissionInfo>(ZaitMili, ScnapcusUtilmenuMission);

					{
						//	Utilmenu in Liste ainfüüge

						var MengeZuAusInfoPanelMissionStapelIndexUtilmenu = this.MengeZuAusInfoPanelMissionStapelIndexUtilmenu;

						if (null == MengeZuAusInfoPanelMissionStapelIndexUtilmenu)
						{
							MengeZuAusInfoPanelMissionStapelIndexUtilmenu = new Dictionary<int, SictWertMitZait<VonSensor.UtilmenuMissionInfo>>();
						}

						var InListeMissionIndex =
							FürUtilmenuMissionIndexInListeMissionButton(
							AusScnapscusAuswertungZuusctand.UtilmenuMission,
							this.InfoPanelMengeMissionButtonAktuel.Wert);

						if (InListeMissionIndex.HasValue)
						{
							MengeZuAusInfoPanelMissionStapelIndexUtilmenu[InListeMissionIndex.Value] = ScnapcusUtilmenuMissionMitZait;
						}

						this.MengeZuAusInfoPanelMissionStapelIndexUtilmenu = MengeZuAusInfoPanelMissionStapelIndexUtilmenu;
					}

					var UtilmenuIdentiscZuVorherige = UtilmenuMissionIdentisc(this.UtilmenuMissionLezteNocOfeMitBeginZait.Wert, ScnapcusUtilmenuMission);

					if (UtilmenuIdentiscZuVorherige)
					{
						UtilmenuMissionLezteNocOfeMitBeginZait = new SictWertMitZait<VonSensor.UtilmenuMissionInfo>(
							this.UtilmenuMissionLezteNocOfeMitBeginZait.Zait,
							ScnapcusUtilmenuMission);
					}
					else
					{
						UtilmenuMissionLezteNocOfeMitBeginZait = ScnapcusUtilmenuMissionMitZait;
					}
				}
			}

			this.UtilmenuMissionLezteNocOfeMitBeginZait = UtilmenuMissionLezteNocOfeMitBeginZait;

			var UtilmenuMissionLezteAlterMili = ZaitMili - this.UtilmenuMissionLezteNocOfeMitBeginZait.Zait;

			if (null != MengeWindowAgentDialogue)
			{
				var MengeMissionFertigNict = Optimat.Glob.Tailmenge(MengeMission, PrädikaatMissionFertigNict);

				foreach (var WindowAgentDialogue in MengeWindowAgentDialogue)
				{
					if (null == WindowAgentDialogue)
					{
						continue;
					}

					var WindowAgentDialogueScnapscusLezte = WindowAgentDialogue.AingangScnapscusTailObjektIdentLezteBerecne() as VonSensor.WindowAgentDialogue;

					if (null == WindowAgentDialogueScnapscusLezte)
					{
						continue;
					}

					var MissionInfo = WindowAgentDialogueScnapscusLezte.ZusamefasungMissionInfo;

					if (null == MissionInfo)
					{
						continue;
					}

					var MissionInfoObjective = MissionInfo.Objective;

					if (null == MissionInfoObjective)
					{
						continue;
					}

					var WindowAgentDialogueAgentLocation = WindowAgentDialogueScnapscusLezte.AgentLocation;
					var WindowAgentDialogueAgentName = WindowAgentDialogueScnapscusLezte.AgentName;

					if (null == WindowAgentDialogueAgentLocation || null == WindowAgentDialogueAgentName)
					{
						continue;
					}

					/*
					 * 2015.01.11
					 * Änderung: Msn sol zuukünftig auc dan erfast werde wen AgentEntry noc nit erfast wurde.
					 * 
					var AgentEntry = ZuStationLocationUndAgentNameAgentEntry(
						WindowAgentDialogueAgentLocation,
						WindowAgentDialogueAgentName);

					var AgentLevel = (null == AgentEntry) ? null : AgentEntry.AgentLevel;

					if (!AgentLevel.HasValue)
					{
						continue;
					}
					 * */

					int? AgentLevel = null;

					var AgentEntry = ZuStationLocationUndAgentNameAgentEntry(
						WindowAgentDialogueAgentLocation,
						WindowAgentDialogueAgentName);

					if (null != AgentEntry)
					{
						AgentLevel = AgentEntry.AgentLevel;
					}

					var BisherMission =
						(null == MengeMission) ? null :
						MengeMission
						.FirstOrDefault((KandidaatMission) => EveOnline.Anwendung.SictMissionZuusctand.WindowAgentPastZuMission(KandidaatMission,
							WindowAgentDialogueScnapscusLezte, true, true, true, true));

					if (null == BisherMission)
					{
						if (true == WindowAgentDialogueScnapscusLezte.IstOffer ||
							true == WindowAgentDialogueScnapscusLezte.IstAccepted)
						{
							var MissionBezaicner = (this.MissionIdentLezte + 1) ?? 0;

							var SictungFrühesteZaitMili = ZaitMili;

							var OfferFrühesteZaitMili =
								(true == WindowAgentDialogueScnapscusLezte.IstOffer) ? SictungFrühesteZaitMili : (Int64?)null;

							var MengeFactionUndMissionStrategikon = SictKonfigMissionStrategikon.MengeFactionUndStrategikonFürMissionInfo(MissionInfo);

							var MissionTailFürNuzer =
								new EveOnline.SictMissionZuusctand(
								MissionBezaicner,
								SictungFrühesteZaitMili,
								OfferFrühesteZaitMili,
								MissionInfo.MissionTitel,
								WindowAgentDialogueAgentName,
								AgentLevel,
								WindowAgentDialogueAgentLocation);

							/*
							 * 2015.02.13
							 * 
							MissionTailFürNuzer.ObjectiveMengeFaction = MengeFactionUndMissionStrategikon.Key;
							 * */

							MissionTailFürNuzer.ObjectiveMengeFaction =
								MengeFactionUndMissionStrategikon.Key ??
								MissionInfo.MengeFaction;

							MissionTailFürNuzer.SecurityLevelMinimumMili = MissionInfoObjective.SecurityLevelMinimumMili;

							var Mission = new SictMissionZuusctand(
								MissionTailFürNuzer,
								WindowAgentDialogue,
								MengeFactionUndMissionStrategikon.Value);

							var MissionInfoZuZaitLezte = Mission.MissionInfoZuZaitLezteBerecne();

							if (null != MissionInfoZuZaitLezte.Wert)
							{
								MissionTailFürNuzer.MissionInfoLezte = MissionInfoZuZaitLezte;

								//	Fitting Bezaicner berecne damit Nuzer direkt naac erscte Sictung der Mission prüüfe kan ob Fitting pasend entsciide wurde.

								Mission.ConstraintFittingBerecne(OptimatParam, this);

								if (null == MengeMission)
								{
									this.MengeMission = MengeMission = new List<SictMissionZuusctand>();
								}

								this.MissionIdentLezte = MissionBezaicner;

								MengeMission.Add(Mission);
							}
						}
					}
					else
					{
						BisherMission.AingangMissionInfo(WindowAgentDialogue);
					}
				}
			}

			if (null != MengeMission)
			{
				//	2014.07.30	Fitting Bezaicner für ale Mission aktualisiire.

				foreach (var Mission in MengeMission)
				{
					Mission.ConstraintFittingBerecne(OptimatParam, this);

					var MissionTailFürNuzer = Mission.TailFürNuzer;

					if (null != MissionTailFürNuzer)
					{
						if (!MissionTailFürNuzer.AgentLevel.HasValue)
						{
							var AgentEntry =
								ZuStationLocationUndAgentNameAgentEntry(
								MissionTailFürNuzer.AgentLocation,
								MissionTailFürNuzer.AgentName);

							if (null != AgentEntry)
							{
								MissionTailFürNuzer.AgentLevel = AgentEntry.AgentLevel;
							}
						}
					}
				}
			}

			{
				//	Aktualisatioon AnnaameInInfoPanelButtonIndex

				var MengeMissionAkzeptiirtUndNictBeendet = this.MengeMissionAkzeptiirtUndNictBeendetBerecne();

				if (null != MengeMissionAkzeptiirtUndNictBeendet &&
					null != InfoPanelMengeMissionButtonAktuel.Wert)
				{
					foreach (var MissionAkzeptiirtUndNictBeendet in MengeMissionAkzeptiirtUndNictBeendet)
					{
						if (null == MissionAkzeptiirtUndNictBeendet)
						{
							continue;
						}

						var MengeMissionButtonMitGlaiceTitel =
							InfoPanelMengeMissionButtonAktuel.Wert
							.Select((Kandidaat, Index) => new KeyValuePair<VonSensor.InfoPanelMissionsMission, int>(
								Kandidaat, Index))
							.Where((Kandidaat) => string.Equals(Kandidaat.Key.Bescriftung, MissionAkzeptiirtUndNictBeendet.Titel()))
							.ToArray();

						if (1 == MengeMissionButtonMitGlaiceTitel.Length)
						{
							//	Scpeziaalfal nur aine Mission mit glaicem Titel in Info Panel

							MissionAkzeptiirtUndNictBeendet.AnnaameInInfoPanelButtonIndex = MengeMissionButtonMitGlaiceTitel[0].Value;
						}
						else
						{
							//	Erwaitere um Identitäät z.B. aus Location in lezte Utilmenu scliise zu köne.
						}
					}
				}
			}

			AktualisiireTailAuswaalMission(AutomaatZuusctand, AusScnapscusAuswertungZuusctand);

			{
				var MissionAktuel = this.MissionAktuel;

				if (null != MissionAktuel)
				{
					MissionAktuel.Aktualisiire(AutomaatZuusctand, AusScnapscusAuswertungZuusctand);
				}
			}

			if (true == VonNuzerParamScpezVerzictAufCargoLeere)
			{
				AnforderungActiveShipCargoLeereLezteZaitMili = null;
			}
		}

		public VonSensor.LobbyAgentEntry ZuMissionOfferAgentEntry(
			VonSensor.WindowAgent WindowAgentMissionOffer)
		{
			if (null == WindowAgentMissionOffer)
			{
				return null;
			}

			var AgentName = WindowAgentMissionOffer.AgentName;
			var AgentLocation = WindowAgentMissionOffer.AgentLocation;

			return ZuStationLocationUndAgentNameAgentEntry(AgentLocation, AgentName);
		}

		public VonSensor.LobbyAgentEntry ZuMissionAgentEntry(
			SictMissionZuusctand Mission)
		{
			if (null == Mission)
			{
				return null;
			}

			var MissionTailFürNuzer = Mission.TailFürNuzer;

			if (null == MissionTailFürNuzer)
			{
				return null;
			}

			return ZuStationLocationUndAgentNameAgentEntry(MissionTailFürNuzer.AgentLocation, MissionTailFürNuzer.AgentName);
		}

		public VonSensor.LobbyAgentEntry ZuStationLocationUndAgentNameAgentEntry(
			MissionLocation StationLocation,
			string AgentName)
		{
			if (null == StationLocation)
			{
				return null;
			}

			if (null == AgentName)
			{
				return null;
			}

			var StationLocationLocationName = StationLocation.LocationName;

			var MengeZuStationMengeAgent = this.MengeZuStationMengeAgent;

			if (null == StationLocationLocationName)
			{
				return null;
			}

			if (null == MengeZuStationMengeAgent)
			{
				return null;
			}

			var ZuStationMengeAgentEntryMitZait =
				MengeZuStationMengeAgent.FirstOrDefault((KandidaatStation) =>
					string.Equals(KandidaatStation.Key.StationName, StationLocationLocationName)).Value;

			if (null == ZuStationMengeAgentEntryMitZait)
			{
				return null;
			}

			var ZuZaitAgentEntry =
				Optimat.Glob.TAD(ZuStationMengeAgentEntryMitZait, AgentName);

			var AgentEntry = ZuZaitAgentEntry.Wert;

			return AgentEntry;
		}

		static public KeyValuePair<VonSensor.LobbyAgentEntry, SictMissionZuusctand> AgentMitMissionOfferPrioHööcsteBerecneAusMengeAgentMitMissionOffer(
			IEnumerable<KeyValuePair<VonSensor.LobbyAgentEntry, SictMissionZuusctand>> MengeAgentMitMissionOffer)
		{
			if (null == MengeAgentMitMissionOffer)
			{
				return default(KeyValuePair<VonSensor.LobbyAgentEntry, SictMissionZuusctand>);
			}

			return
				MengeAgentMitMissionOffer.OrderBy((Kandidaat) => WertOrdnungFürAgentMitMissionOffer(Kandidaat.Key, Kandidaat.Value)).FirstOrDefault();
		}

		static public int WertOrdnungFürAgentMitMissionOffer(
			VonSensor.LobbyAgentEntry Agent,
			SictMissionZuusctand MissionOffer)
		{
			if (null == MissionOffer)
			{
				return int.MaxValue;
			}

			var DialogueZuZaitLezte = MissionOffer.WindowAgentDialogueZuZaitLezteBerecne(null);

			var DialogueLezte = DialogueZuZaitLezte.HasValue ? DialogueZuZaitLezte.Value.Wert : null;

			if (null == DialogueLezte)
			{
				return int.MaxValue;
			}

			if (true == DialogueLezte.DeclineOoneStandingLossFraigaabe)
			{
				if (!DialogueLezte.DeclineWartezait.HasValue)
				{
					//	kaine Wartezait -> hööcste Prio
					return int.MinValue;
				}
			}

			return DialogueLezte.DeclineWartezait ?? 0;
		}

		/// <summary>
		/// Berecnet auc zu tesctende Fitting.
		/// </summary>
		/// <param name="AutomaatZuusctand"></param>
		void AktualisiireTailAuswaalMission(
			SictAutomatZuusctand AutomaatZuusctand,
			VonSensorikMesung AusScnapscusAuswertungZuusctand)
		{
			if (null == AutomaatZuusctand)
			{
				return;
			}

			VonSensor.InfoPanelMissionsMission MissionButtonUtilmenuObjectiveZuMese = null;
			VonSensor.WindowAgentDialogue WindowAgentDialogueMissionAcceptOderRequest = null;

			VonSensor.LobbyAgentEntry LobbyAgentEntryStartConversation = null;
			KeyValuePair<string, int?>[] InAktuelemSystemMengeStationFürWelcePasendeMissionNictAusgesclose = null;
			SictMissionZuusctand MissionAcceptNääxte = null;
			SictMissionZuusctand MissionDeclineNääxte = null;

			var ZaitMili = AutomaatZuusctand.NuzerZaitMili;

			var OptimatParam = AutomaatZuusctand.OptimatParam();
			var FittingUndShipZuusctand = AutomaatZuusctand.FittingUndShipZuusctand;

			var OptimatParamSimu = (null == OptimatParam) ? null : OptimatParam.SimuNaacBerüksictigungFraigaabeBerecne();

			var ShipZuusctandLezte = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.ShipZuusctand;

			var SelbsctShipDocked = (null == ShipZuusctandLezte) ? null : (bool?)ShipZuusctandLezte.Docked;

			var OptimatParamMission = (null == OptimatParam) ? null : OptimatParam.Mission;

			var AutoMissionAktioonAcceptFraigaabe = (null == OptimatParamMission) ? null : OptimatParamMission.AktioonAcceptFraigaabe;
			var AutoMissionAktioonDeclineFraigaabe = (null == OptimatParamMission) ? null : OptimatParamMission.AktioonDeclineFraigaabe;
			var AutoMissionAktioonDeclineUnabhängigVonStandingLossFraigaabe = (null == OptimatParamMission) ? null : OptimatParamMission.AktioonDeclineUnabhängigVonStandingLossFraigaabe;

			var CurrentLocation = (null == AusScnapscusAuswertungZuusctand) ? null : AusScnapscusAuswertungZuusctand.CurrentLocationInfo();

			var MengeWindowAgentDialogue = (null == AusScnapscusAuswertungZuusctand) ? null : AusScnapscusAuswertungZuusctand.MengeWindowAgentDialogue;

			var MengeWindowAgentDialogueOrdnetNaacZIndex =
				(null == MengeWindowAgentDialogue) ? null :
				MengeWindowAgentDialogue
				/*
				2015.09.01
				Änderung InGbsBaumAstIndex: Übernaame von noie Sensor InTreeIndex: Element occludes other Elements with lower Value.
			.OrderBy((Window) => ((null == Window) ? null : Window.InGbsBaumAstIndex) ?? int.MaxValue)
			*/
				.OrderByDescending(Window => Window?.InGbsBaumAstIndex ?? int.MinValue)
				.ToArray();

			var OptimatParamSimuMissionAnforderungFittingIgnoriire =
				(null == OptimatParamSimu) ? null : OptimatParamSimu.MissionAnforderungFittingIgnoriire;

			var UtilmenuMissionLezte = this.UtilmenuMissionLezteNocOfeMitBeginZait;

			var AusScnapscusLobbyMengeAgentEntryPasendZuVonNuzerKonfig = this.AusScnapscusLobbyMengeAgentEntryPasendZuVonNuzerKonfig;

			var AnnaameActiveShipCargoLeerLezteZaitMili = AutomaatZuusctand.AnnaameActiveShipCargoGeneralLeerLezteZaitMili;

			var FittingListeVersuucFitLoad = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.ListeVersuucFitLoad;

			var MissionAktuel = this.MissionAktuel;

			var MengeMissionAkzeptiirtUndNictBeendet = this.MengeMissionAkzeptiirtUndNictBeendetBerecne();
			var MengeMissionFertig = this.MengeMissionFertigBerecne();

			var InfoPanelMengeMissionButtonAusScnapscusAktuel = this.InfoPanelMengeMissionButtonAusScnapscusAktuel;

			var ZuBeginZaitMissionFittingZuTesteNääxte = this.ZuBeginZaitMissionFittingZuTesteNääxte;

			try
			{
				{
					//	Berecnung AnforderungActiveShipCargoLeereLezteZaitMili

					Int64? NaacMissionFertigLeereCargoZaitMili = null;

					if (null != MengeMissionFertig)
					{
						var MengeMissionFertigEndeZait =
							MengeMissionFertig.Select((Mission) => Mission.EndeZaitMili()).ToArray();

						NaacMissionFertigLeereCargoZaitMili = Bib3.Glob.Max(MengeMissionFertigEndeZait);
					}

					if (!(true == VonNuzerParamScpezVerzictAufCargoLeere))
					{
						if (NaacMissionFertigLeereCargoZaitMili.HasValue)
						{
							this.AnforderungActiveShipCargoLeereLezteZaitMili =
								Bib3.Glob.Max(this.AnforderungActiveShipCargoLeereLezteZaitMili, NaacMissionFertigLeereCargoZaitMili);
						}

						if (!this.AnforderungActiveShipCargoLeereLezteZaitMili.HasValue)
						{
							this.AnforderungActiveShipCargoLeereLezteZaitMili = ZaitMili;
						}
					}
				}

				var FittingFitLoadedLezteNocAktiivMitZait = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.FitLoadedLezteNocAktiiv;

				if (FittingFitLoadedLezteNocAktiivMitZait.HasValue)
				{
					var FittingFitLoadedLezteNocAktiivAlterMili = ZaitMili - FittingFitLoadedLezteNocAktiivMitZait.Value.Zait;

					var FittingFitLoadedLezteNocAktiivAlter = FittingFitLoadedLezteNocAktiivAlterMili / 1000;

					/*
					 * 2014.10.27
					 * 
					 * Beobactung: Wen Automat aus Msn naac Station flüctet versuuct diiser dort das Fitting zu laade was maistens feelscläägt da in der zuufllig für Fluct ausgewäälte Station die Items nit vorhande sind.
					 * Naac Beobactung Probleem Änderung: Unabhängig von Alter.
					 * 
					if (300 < FittingFitLoadedLezteNocAktiivAlter ||
						FittingFitLoadedLezteNocAktiivMitZait.Value.Zait < AnnaameActiveShipCargoLeerLezteZaitMili ||
						FittingFitLoadedLezteNocAktiivMitZait.Value.Zait < this.AnforderungActiveShipCargoLeereLezteZaitMili)
					 * */
					if (FittingFitLoadedLezteNocAktiivMitZait.Value.Zait < AnnaameActiveShipCargoLeerLezteZaitMili ||
						FittingFitLoadedLezteNocAktiivMitZait.Value.Zait < this.AnforderungActiveShipCargoLeereLezteZaitMili)
					{
						FittingFitLoadedLezteNocAktiivMitZait = null;
					}
				}

				this.FittingFitLoadedLezteNocAktiivMitZait = FittingFitLoadedLezteNocAktiivMitZait;

				var MissionFittingZuTesteNääxteAlter = ZaitMili - ZuBeginZaitMissionFittingZuTesteNääxte.Zait;

				if (null != ZuBeginZaitMissionFittingZuTesteNääxte.Wert)
				{
					var ZuBeginZaitMissionFittingZuTesteNääxteErhalte = true;

					if (null != FittingListeVersuucFitLoad)
					{
						if (FittingListeVersuucFitLoad.Any((Kandidaat) =>
							ZuBeginZaitMissionFittingZuTesteNääxte.Zait < Kandidaat.Zait &&
							Kandidaat.Wert.EntscaidungErfolg.HasValue))
						{
							ZuBeginZaitMissionFittingZuTesteNääxteErhalte = false;
						}
					}

					if (ZuBeginZaitMissionFittingZuTesteNääxte.Wert.FürMissionFittingBezaicner().IsNullOrEmpty() || 33333 < MissionFittingZuTesteNääxteAlter ||
						(ZuBeginZaitMissionFittingZuTesteNääxte.Wert.AnnaameCompleteFallsInAgentStation ?? false) ||
						ZuBeginZaitMissionFittingZuTesteNääxte.Wert.EndeZaitMili().HasValue)
					{
						//	Fit Load wurde beraits versuuct, Scpaicer zurükseze

						ZuBeginZaitMissionFittingZuTesteNääxteErhalte = false;
					}

					if (!ZuBeginZaitMissionFittingZuTesteNääxteErhalte)
					{
						ZuBeginZaitMissionFittingZuTesteNääxte = default(SictWertMitZait<SictMissionZuusctand>);
					}
				}

				if (null != MissionAktuel)
				{
					if (((SelbsctShipDocked ?? false) &&
						!(MissionAktuel.ConstraintFittingSatisfied() ?? false)) ||
						MissionAktuel.EndeZaitMili().HasValue)
					{
						MissionAktuel = null;
					}
				}

				{
					//	Für noc geöfnete Window Agent Dialoge Request Button betäätige.

					if (null != AusScnapscusLobbyMengeAgentEntryPasendZuVonNuzerKonfig &&
						null != MengeWindowAgentDialogue)
					{
						var WindowAgentDialogue =
							MengeWindowAgentDialogue
							.FirstOrDefault((Kandidaat) =>
								AusScnapscusLobbyMengeAgentEntryPasendZuVonNuzerKonfig.Any((AgentEntryPasend) =>
								string.Equals(Kandidaat.AgentName, AgentEntryPasend.AgentName, StringComparison.InvariantCultureIgnoreCase)));

						if (null != WindowAgentDialogue)
						{
							var ButtonRequestMission = WindowAgentDialogue.ButtonRequestMission;

							if (null != ButtonRequestMission)
							{
								WindowAgentDialogueMissionAcceptOderRequest = WindowAgentDialogue;
								return;
							}
						}
					}
				}

				if (null != MissionAktuel)
				{
					var MissionAktuelTailNuzer = MissionAktuel.TailFürNuzer;

					if (null != MissionAktuelTailNuzer)
					{
						MissionAktuelTailNuzer.AktioonFüüreAusAktiiv = true;
					}

					return;
				}

				if (null != MengeMissionAkzeptiirtUndNictBeendet)
				{
					foreach (var MissionAkzeptiirtUndNictBeendet in MengeMissionAkzeptiirtUndNictBeendet)
					{
						//	Prüüfe ob Vorraussezunge erfült sin um diise Mission zu begine.

						if (null == MissionAkzeptiirtUndNictBeendet)
						{
							continue;
						}

						var MissionAkzeptiirtUndNictBeendetTailNuzer = MissionAkzeptiirtUndNictBeendet.TailFürNuzer;

						if (null == MissionAkzeptiirtUndNictBeendetTailNuzer)
						{
							continue;
						}

						var MissionInArbaitObjectiveZuusctand = MissionAkzeptiirtUndNictBeendet.ListeMesungObjectiveZuusctandZuZaitLezteBerecne();

						if (!MissionInArbaitObjectiveZuusctand.HasValue)
						{
							continue;
						}

						if (null == MissionInArbaitObjectiveZuusctand.Value.Wert)
						{
							continue;
						}

						if (!(true == MissionInArbaitObjectiveZuusctand.Value.Wert.Complete))
						{
							//	Bedingunge an Strategikon und Ausrüstung werde nur gesctelt wen Mission noc nit Complete.

							var ZuMissionVerhalte = MissionAkzeptiirtUndNictBeendetTailNuzer.AusPräferenzEntscaidungVerhalte;

							if (null == ZuMissionVerhalte)
							{
								continue;
							}

							if (!(true == ZuMissionVerhalte.AktioonFüüreAusAktiiv))
							{
								continue;
							}

							var Strategikon = MissionAkzeptiirtUndNictBeendet.Strategikon;

							if (null == Strategikon)
							{
								continue;
							}

							MissionAkzeptiirtUndNictBeendet.ConstraintFittingBerecne(OptimatParam, this);

							var MissionInArbaitFürMissionFittingBezaicner = MissionAkzeptiirtUndNictBeendet.FürMissionFittingBezaicner();

							//	if (false)
							{
								//	2015.02.12	Filter Faction werd vorersct ignoriirt.

								if (MissionAkzeptiirtUndNictBeendetTailNuzer.ObjectiveMengeFaction.IsNullOrEmpty())
								{
									//	Automat hat in Mission zu erwartende Faction nit erkant.
									continue;
								}
							}

							if (!((MissionAkzeptiirtUndNictBeendetTailNuzer.ConstraintFittingSatisfied ?? false) ||
								(OptimatParamSimuMissionAnforderungFittingIgnoriire ?? false)))
							{
								continue;
							}


							//	!!!	Vorersct werd gesamte Ausrüstung über vorkonfiguriirtes Fitting gelaade, scpääter sol für manuele Versorgung mit Drones und Ammo erwaitert werde.
						}

						//	Ale vorraussezunge zum sctart diiser Mission sin erfült.
						MissionAktuel = MissionAkzeptiirtUndNictBeendet;
						break;
					}
				}

				if (null == MissionAktuel && null == ZuBeginZaitMissionFittingZuTesteNääxte.Wert)
				{
					//	Fitting tescte

					if (null != MengeMissionAkzeptiirtUndNictBeendet)
					{
						foreach (var MissionInArbait in MengeMissionAkzeptiirtUndNictBeendet)
						{
							//	Prüüfe ob Vorraussezunge erfült sin um diise Mission zu begine.

							if (null == MissionInArbait)
							{
								continue;
							}

							var MissionInArbaitTailNuzer = MissionInArbait.TailFürNuzer;

							if (null == MissionInArbaitTailNuzer)
							{
								continue;
							}

							var ZuMissionVerhalte = MissionInArbaitTailNuzer.AusPräferenzEntscaidungVerhalte;

							if (null == ZuMissionVerhalte)
							{
								continue;
							}

							if (!(true == ZuMissionVerhalte.AktioonFüüreAusAktiiv))
							{
								continue;
							}

							var MissionInArbaitFürMissionFittingBezaicner = MissionInArbait.FürMissionFittingBezaicner();
							var Strategikon = MissionInArbait.Strategikon;

							if (null == Strategikon)
							{
								continue;
							}

							if (null == MissionInArbaitFürMissionFittingBezaicner)
							{
								continue;
							}

							//	Wen diise Verzwaigung erraict wurde isc davon auszugehe das Fitting noc nit erfolgraic gelaade wurde.

							ZuBeginZaitMissionFittingZuTesteNääxte = new SictWertMitZait<SictMissionZuusctand>(ZaitMili, MissionInArbait);
						}
					}
				}

				if (null != ZuBeginZaitMissionFittingZuTesteNääxte.Wert)
				{
					return;
				}

				if (null != InfoPanelMengeMissionButtonAusScnapscusAktuel.Wert &&
					ZaitMili <= InfoPanelMengeMissionButtonAusScnapscusAktuel.Zait
					&& true == SelbsctShipDocked
					)
				{
					//	Mission Objective zu noie Mission mese

					foreach (var MissionButtonUtilmenu in InfoPanelMengeMissionButtonAusScnapscusAktuel.Wert)
					{
						if (null == MissionButtonUtilmenu)
						{
							continue;
						}

						if (!ZuMissionAktioonFüüreAusNitAusgesclose(MissionButtonUtilmenu, OptimatParamMission))
						{
							continue;
						}

						MissionButtonUtilmenuObjectiveZuMese = MissionButtonUtilmenu;
						break;
					}
				}

				if (null != MissionButtonUtilmenuObjectiveZuMese)
				{
					return;
				}

				//	Wen diiser Ast erraict werd sind kaine interesanten Mission meer in InfoPanel verfüügbar. -> Daher werden Agente naac noie Missione gefraagt.

				if ((true == AutoMissionAktioonAcceptFraigaabe ||
					true == AutoMissionAktioonDeclineFraigaabe) &&
					null != CurrentLocation)
				{
					var VonNuzerParamMissionAktioonAcceptMengeAgentLevelFraigaabe = (null == OptimatParamMission) ? null : OptimatParamMission.AktioonAcceptMengeAgentLevelFraigaabe;

					var StationAktuelIdent = new SictAgentIdentSystemStationName(CurrentLocation.SolarSystemName, CurrentLocation.NearestName, null);

					if (true == SelbsctShipDocked)
					{
						var ZuStationAktuelMengeAgentMitOffer =
							ZuStationMengeAgentMitMissionInfoLezteBerecne(StationAktuelIdent, ZaitMili - 900 * 1000);

						if (null != ZuStationAktuelMengeAgentMitOffer)
						{
							//	Menge der beraits gemesene Mission Offer von Agent in aktuele Station untertaile in zu declinende, zu aktuelem Fitting pasende und solce für di ain andere Fitting gelaade werde müste.

							var ZuStationAktuelMengeAgentMitOfferTailmengeView =
								ZuStationAktuelMengeAgentMitOffer.Take(0).ToList();

							var ZuStationAktuelMengeAgentMitOfferTailmengeDecline =
								ZuStationAktuelMengeAgentMitOffer.Take(0).ToList();

							var ZuStationAktuelMengeAgentMitOfferTailmengeTestFitting =
								ZuStationAktuelMengeAgentMitOffer.Take(0).ToList();

							var ZuStationAktuelMengeAgentMitOfferTailmengeFertigFürAccept =
								ZuStationAktuelMengeAgentMitOffer.Take(0).ToList();

							foreach (var AgentMitOffer in ZuStationAktuelMengeAgentMitOffer)
							{
								if (null == AgentMitOffer.Key)
								{
									continue;
								}

								var AgentOfferMission = AgentMitOffer.Value;

								var AgentLevelNulbar = AgentMitOffer.Key.AgentLevel;

								if (!AgentLevelNulbar.HasValue)
								{
									continue;
								}

								{
									//	2014.04.24	zuusäzlice Filter

									if (null == AusScnapscusLobbyMengeAgentEntryPasendZuVonNuzerKonfig)
									{
										continue;
									}

									if (!AusScnapscusLobbyMengeAgentEntryPasendZuVonNuzerKonfig
										.Any((AgentEntryPasend) => string.Equals(AgentMitOffer.Key.AgentName, AgentEntryPasend.AgentName, StringComparison.InvariantCultureIgnoreCase)))
									{
										continue;
									}
								}

								var AgentLevel = AgentLevelNulbar.Value;

								if (!VonNuzerParamMissionAktioonAcceptMengeAgentLevelFraigaabe.Contains(AgentLevel))
								{
									continue;
								}

								if (null != AgentOfferMission)
								{
									if (AgentOfferMission.EndeZaitMili().HasValue)
									{
										//	Mission isc scon beendet und werd daher hiir nit meer berüksictigt.
										AgentOfferMission = null;
									}
								}

								if (null == AgentOfferMission)
								{
									//	Von diisem Agent wurde noc kain Ofer gemese.

									//	Vor entscaidung für Mission zunäxt von ale Agente Mission Offer registriire.
									ZuStationAktuelMengeAgentMitOfferTailmengeView.Add(AgentMitOffer);
									continue;
								}

								var AgentOfferMissionTailFürNuzer = AgentOfferMission.TailFürNuzer;

								if (null == AgentOfferMissionTailFürNuzer)
								{
									continue;
								}

								var AgentOfferMissionWindowAgentDialogueZuZaitLezte =
									AgentOfferMission.WindowAgentDialogueZuZaitLezteBerecne() ??
									default(SictWertMitZait<VonSensor.WindowAgentDialogue>);

								var AgentOfferMissionWindowAgentDialogueLezte = AgentOfferMissionWindowAgentDialogueZuZaitLezte.Wert;

								if (null == AgentOfferMissionWindowAgentDialogueLezte)
								{
									continue;
								}

								var MessungOfferAlter = (ZaitMili - AgentOfferMissionWindowAgentDialogueZuZaitLezte.Zait) / 1000;

								var ZuMissionOfferVerhalte = AgentOfferMissionTailFürNuzer.AusPräferenzEntscaidungVerhalte;

								var ZuMissionOfferVerhalteAktioonAcceptAktiiv = (null == ZuMissionOfferVerhalte) ? null : ZuMissionOfferVerhalte.AktioonAcceptAktiiv;

								if (null != ZuMissionOfferVerhalte)
								{
									var ZuMissionDeclineFraigaabe = false;

									if (true == ZuMissionOfferVerhalte.AktioonDeclineAktiiv &&
										true == AutoMissionAktioonDeclineFraigaabe)
									{
										var DeclineWartezaitRest = AgentOfferMissionWindowAgentDialogueLezte.DeclineWartezait - MessungOfferAlter;

										if (true == AgentOfferMissionWindowAgentDialogueLezte.DeclineOoneStandingLossFraigaabe || DeclineWartezaitRest < 0 ||
											60 * 30 < MessungOfferAlter)
										{
											//	Offer kan declined werde oder lezte Mesung isc älter als halbe Sctunde.
											ZuMissionDeclineFraigaabe = true;
										}

										if (true == AutoMissionAktioonDeclineUnabhängigVonStandingLossFraigaabe &&
											true == this.MissionDeclineUnabhängigVonStandingLossFraigaabe &&
											true == ZuMissionOfferVerhalte.AktioonDeclineUnabhängigVonStandingLossAktiiv)
										{
											ZuMissionDeclineFraigaabe = true;
										}
									}

									if (ZuMissionDeclineFraigaabe)
									{
										//	Dialog mit Agent begine damit Offer declined werde kan.
										ZuStationAktuelMengeAgentMitOfferTailmengeDecline.Add(AgentMitOffer);
										continue;
									}
								}

								var ZusamefasungMissionInfo = AgentOfferMissionWindowAgentDialogueLezte.ZusamefasungMissionInfo;

								if (null == ZusamefasungMissionInfo)
								{
									continue;
								}

								if (true == ZusamefasungMissionInfo.Complete)
								{
									//	Diis isc Info zu Mission welce scon complet war, hiirzu brauc kain Fitting meer getesctet were.
									continue;
								}

								var KandidaatMissionStrategikon = AgentOfferMission.Strategikon;

								if (null == KandidaatMissionStrategikon)
								{
									continue;
								}

								if (!(true == ZuMissionOfferVerhalteAktioonAcceptAktiiv))
								{
									continue;
								}

								AgentOfferMission.ConstraintFittingBerecne(OptimatParam, this);

								if (AgentOfferMission.AnnaameCompleteFallsInAgentStation ?? false)
								{
									continue;
								}

								if (AgentOfferMissionTailFürNuzer.ConstraintFittingSatisfied ?? false)
								{
									ZuStationAktuelMengeAgentMitOfferTailmengeFertigFürAccept.Add(AgentMitOffer);
									continue;
								}

								var FürMissionFittingBezaicner = AgentOfferMissionTailFürNuzer.FürMissionFittingBezaicner;

								if (FürMissionFittingBezaicner.IsNullOrEmpty())
									continue;

								ZuStationAktuelMengeAgentMitOfferTailmengeTestFitting.Add(AgentMitOffer);
							}

							var AgentMitOfferView = AgentMitMissionOfferPrioHööcsteBerecneAusMengeAgentMitMissionOffer(ZuStationAktuelMengeAgentMitOfferTailmengeView);
							var AgentMitOfferDecline = AgentMitMissionOfferPrioHööcsteBerecneAusMengeAgentMitMissionOffer(ZuStationAktuelMengeAgentMitOfferTailmengeDecline);
							var AgentMitOfferTestFitting = AgentMitMissionOfferPrioHööcsteBerecneAusMengeAgentMitMissionOffer(ZuStationAktuelMengeAgentMitOfferTailmengeTestFitting);

							var AgentMitOfferAccept = AgentMitMissionOfferPrioHööcsteBerecneAusMengeAgentMitMissionOffer(ZuStationAktuelMengeAgentMitOfferTailmengeFertigFürAccept);

							if (null != AgentMitOfferView.Key)
							{
								LobbyAgentEntryStartConversation = AgentMitOfferView.Key;
								return;
							}

							if (null != AgentMitOfferDecline.Key)
							{
								MissionDeclineNääxte = AgentMitOfferDecline.Value;
								return;
							}

							if (null != AgentMitOfferAccept.Key)
							{
								MissionAcceptNääxte = AgentMitOfferAccept.Value;
								return;
							}

							if (null != AgentMitOfferTestFitting.Key)
							{
								var TestFittingMission = AgentMitOfferTestFitting.Value;

								//	if (!TestFittingMission.FürMissionFittingBezaicner.NullOderLeer())
								{
									ZuBeginZaitMissionFittingZuTesteNääxte = new SictWertMitZait<SictMissionZuusctand>(ZaitMili, TestFittingMission);

									return;
								}
							}
						}
					}
				}

				var OptimatParamMissionSuuceAgentMengeStation = (null == OptimatParamMission) ? null : OptimatParamMission.SuuceAgentMengeStation;

				var OptimatParamMissionSuuceAgentMengeStationLocationInfo =
					(null == OptimatParamMissionSuuceAgentMengeStation) ? null :
					OptimatParamMissionSuuceAgentMengeStation
					.Where((Kandidaat) => true == Kandidaat.Value)
					.Select((Kandidaat) => TempAuswertGbs.Extension.VonStringCurrentStationStationExtraktLocationInfo(Kandidaat.Key))
					.Where((Kandidaat) => null != Kandidaat)
					.ToArray();

				var InAktuelemSystemMengeStationFürSuuceAgent =
					((null == OptimatParamMissionSuuceAgentMengeStationLocationInfo) || (null == CurrentLocation)) ? null :
					OptimatParamMissionSuuceAgentMengeStationLocationInfo
					.Where((Kandidaat) => string.Equals(Kandidaat.SolarSystemName, CurrentLocation.SolarSystemName, StringComparison.InvariantCultureIgnoreCase))
					.ToArray();

				var InAktuelemSystemMengeStationFürSuuceAgentSictStationIdent =
					(null == InAktuelemSystemMengeStationFürSuuceAgent) ? null :
					InAktuelemSystemMengeStationFürSuuceAgent
					.Select((StationLocationInfo) => new SictAgentIdentSystemStationName(StationLocationInfo.SolarSystemName, StationLocationInfo.NearestName))
					.ToArray();

				if (null != InAktuelemSystemMengeStationFürSuuceAgentSictStationIdent)
				{
					var InternInAktuelemSystemMengeStationFürWelcePasendeMissionNictAusgesclose = new List<KeyValuePair<string, int?>>();

					foreach (var StationIdent in InAktuelemSystemMengeStationFürSuuceAgentSictStationIdent)
					{
						var MengeAgentOoneMissionUnpasendAnzaal =
							ZuStationMengeAgentPasendOoneMissionUnpasendAnzaalBerecne(StationIdent, OptimatParamMission, ZaitMili - (60 * 60 * 1000));

						if (0 == MengeAgentOoneMissionUnpasendAnzaal)
						{
							continue;
						}

						InternInAktuelemSystemMengeStationFürWelcePasendeMissionNictAusgesclose.Add(
							new KeyValuePair<string, int?>(StationIdent.StationName, MengeAgentOoneMissionUnpasendAnzaal));
					}

					InAktuelemSystemMengeStationFürWelcePasendeMissionNictAusgesclose = InternInAktuelemSystemMengeStationFürWelcePasendeMissionNictAusgesclose.ToArray();
				}

				//	Wen diiser Ast erraict wird war in der Liste der in Station sictbaare Agent kain interesanter dabai. -> Andere Station aufsuuce.
			}
			finally
			{
				if (null != ZuBeginZaitMissionFittingZuTesteNääxte.Wert)
				{
					var MissionFittingZuTesteNääxteTailNuzer = ZuBeginZaitMissionFittingZuTesteNääxte.Wert.TailFürNuzer;

					if (null != MissionFittingZuTesteNääxteTailNuzer)
					{
						MissionFittingZuTesteNääxteTailNuzer.VersuucFittingAktiiv = true;
					}
				}

				if (null != LobbyAgentEntryStartConversation)
				{
					//	In diise Verzwaigung dafür sorge das Button "Request Mission" und "View Mission" unbedingt betäätigt were.

					var WindowAgentDialogueScnapscus = ZuAgentEntryWindowAgentDialogueScnapscus(LobbyAgentEntryStartConversation);

					if (null != WindowAgentDialogueScnapscus)
					{
						if (null != WindowAgentDialogueScnapscus.ButtonViewMission ||
							null != WindowAgentDialogueScnapscus.ButtonRequestMission)
						{
							WindowAgentDialogueMissionAcceptOderRequest = WindowAgentDialogueScnapscus;
						}
					}
				}

				if (null != MissionAktuel)
				{
					var MissionAktuelTailNuzer = MissionAktuel.TailFürNuzer;

					if (null != MissionAktuelTailNuzer)
					{
						if (!MissionAktuelTailNuzer.AktioonFüüreAusFrühesteZaitMili.HasValue)
						{
							MissionAktuelTailNuzer.AktioonFüüreAusFrühesteZaitMili = ZaitMili;
						}
					}
				}

				this.MissionAktuel = MissionAktuel;
				this.ZuBeginZaitMissionFittingZuTesteNääxte = ZuBeginZaitMissionFittingZuTesteNääxte;
				this.MissionButtonUtilmenuObjectiveZuMese = MissionButtonUtilmenuObjectiveZuMese;
				this.WindowAgentDialogueMissionAcceptOderRequest = WindowAgentDialogueMissionAcceptOderRequest;

				this.LobbyAgentEntryStartConversation = LobbyAgentEntryStartConversation;
				this.MissionAcceptNääxte = MissionAcceptNääxte;
				this.MissionDeclineNääxte = MissionDeclineNääxte;
				this.InAktuelemSystemMengeStationFürWelcePasendeMissionNictAusgesclose = InAktuelemSystemMengeStationFürWelcePasendeMissionNictAusgesclose;
			}
		}

		public SictKonfigMissionZuMissionFilterVerhalte ZuMissionOfferVerhalteBerecne(
			VonSensor.WindowAgent WindowAgentMissionOffer,
			SictOptimatParamMission VonNuzerParamMission)
		{
			if (null == WindowAgentMissionOffer)
			{
				return null;
			}

			var ZusamefasungMissionInfo = WindowAgentMissionOffer.ZusamefasungMissionInfo;

			if (null == ZusamefasungMissionInfo)
			{
				return null;
			}

			var Objective = ZusamefasungMissionInfo.Objective;

			if (null == Objective)
			{
				return null;
			}

			var AgentLocation = WindowAgentMissionOffer.AgentLocation;

			if (null == AgentLocation)
			{
				return null;
			}

			var MissionTitel = ZusamefasungMissionInfo.MissionTitel;

			var AgentEntry = ZuMissionOfferAgentEntry(WindowAgentMissionOffer);

			var AgentLevel = (null == AgentEntry) ? null : AgentEntry.AgentLevel;

			var SecurityLevelMinimumMili = Math.Min(Objective.SecurityLevelMinimumMili ?? -1, AgentLocation.LocationSecurityLevelMili ?? -1);

			var ZuMissionOfferVerhalte = VonNuzerParamMission.ZuMissionOfferVerhalteBerecne(MissionTitel, null, AgentLevel, SecurityLevelMinimumMili);

			return ZuMissionOfferVerhalte;
		}

		static public bool ZuMissionAktioonFüüreAusNitAusgesclose(
			VonSensor.InfoPanelMissionsMission MissionInfo,
			SictOptimatParamMission OptimatParamMission)
		{
			if (null == MissionInfo)
			{
				return false;
			}

			return ZuMissionAktioonFüüreAusNitAusgesclose(MissionInfo.Bescriftung, OptimatParamMission);
		}

		static public bool ZuMissionAktioonFüüreAusNitAusgesclose(
			string MissionTitel,
			SictOptimatParamMission OptimatParamMission)
		{
			if (null == OptimatParamMission)
			{
				return false;
			}

			var Prädikaat = new Func<SictKonfigMissionZuMissionFilterVerhalte, bool>(
				(ZuMissionFilterVerhalte) => ZuMissionFilterVerhalte.FilterMissionTitelPasend(MissionTitel));

			return OptimatParamMission.InMengeZuMissionFilterVerhalteEnthalteMitAktioonFüüreAus(Prädikaat);
		}

		public bool ZuMissionAktioonFüüreAusNitAusgesclose(
			SictMissionZuusctand Mission,
			VonSensor.LobbyAgentEntry AgentEntry,
			SictOptimatParamMission OptimatParamMission)
		{
			if (null == Mission)
			{
				return false;
			}

			if (null == OptimatParamMission)
			{
				return false;
			}

			var MissionTailFürNuzer = Mission.TailFürNuzer;

			if (null == MissionTailFürNuzer)
			{
				return false;
			}

			var ListeMesungObjectiveZuusctandLezte = Mission.ListeMesungObjectiveZuusctandZuZaitLezteBerecne();

			if (!ListeMesungObjectiveZuusctandLezte.HasValue)
			{
				return false;
			}

			if (null == ListeMesungObjectiveZuusctandLezte.Value.Wert)
			{
				return false;
			}

			var Objective = ListeMesungObjectiveZuusctandLezte.Value.Wert.Objective;

			if (null == Objective)
			{
				return false;
			}

			var TailFürNuzer = Mission.TailFürNuzer;

			if (null == TailFürNuzer)
			{
				return false;
			}

			var AgentLocationSecurityLevelMili = TailFürNuzer.AgentLocation.LocationSecurityLevelMili;

			var AgentLevelNulbar = MissionTailFürNuzer.AgentLevel;
			var MissionTitel = Mission.Titel();

			var SecurityLevelMiliMinimum = Math.Min(AgentLocationSecurityLevelMili ?? -1, Objective.SecurityLevelMinimumMili ?? -1);

			if (!AgentLevelNulbar.HasValue)
			{
				return false;
			}

			return OptimatParamMission.ZuMissionFilterAktioonFüüreAusNitAusgesclose(
				MissionTitel,
				null,
				AgentLevelNulbar.Value,
				SecurityLevelMiliMinimum);
		}

		public void AktualisiireZuusctandAusScnapscusAuswertungTailAgent(
			VonSensorikMesung AusScnapscusAuswertungZuusctand,
			Int64 ZaitMili,
			SictOptimatParam OptimatParam,
			Int64 ZuAgentMissionInfoLezteAlterScrankeMax = 1800)
		{
			List<VonSensor.LobbyAgentEntry> AusScnapscusLobbyMengeAgentEntryPasendZuVonNuzerKonfig = null;
			List<VonSensor.LobbyAgentEntry> AusScnapscusLobbyMengeAgentEntryPasendZuVonNuzerKonfigUndOhneUnpasendeMission = null;

			var MengeZuAgentDialogLezte = this.MengeZuAgentDialogLezte;
			var MengeZuAgentMissionInfoLezte = this.MengeZuAgentMissionInfoLezte;
			var MengeZuStationMengeAgent = this.MengeZuStationMengeAgent;

			try
			{
				var VonNuzerParamMission = (null == OptimatParam) ? null : OptimatParam.Mission;

				var AktioonAcceptMengeAgentLevelFraigaabe = (null == VonNuzerParamMission) ? null : VonNuzerParamMission.AktioonAcceptMengeAgentLevelFraigaabe;

				if (null == MengeZuAgentDialogLezte)
				{
					MengeZuAgentDialogLezte = new Dictionary<SictAgentIdentSystemStationName, SictWertMitZait<VonSensor.WindowAgentDialogue>>();
				}

				if (null == MengeZuAgentMissionInfoLezte)
				{
					MengeZuAgentMissionInfoLezte = new Dictionary<SictAgentIdentSystemStationName, SictWertMitZait<VonSensor.WindowAgentDialogue>>();
				}

				if (null == MengeZuStationMengeAgent)
				{
					MengeZuStationMengeAgent = new Dictionary<SictAgentIdentSystemStationName, IDictionary<string, SictWertMitZait<VonSensor.LobbyAgentEntry>>>();
				}

				if (null == AusScnapscusAuswertungZuusctand)
				{
					return;
				}

				var AusGbsWindowLobby = AusScnapscusAuswertungZuusctand.WindowStationLobby;

				if (null == AusGbsWindowLobby)
				{
					return;
				}

				var AusGbsWindowLobbyMengeAgentEntry = AusGbsWindowLobby.MengeAgentEntry;

				var ScnapscusCurrentLocationInfo = AusScnapscusAuswertungZuusctand.CurrentLocationInfo();

				if (0 < ExtractFromOldAssembly.Bib3.Extension.CountNullable(AusGbsWindowLobbyMengeAgentEntry))
				{
					if (null != ScnapscusCurrentLocationInfo &&
						true == AusScnapscusAuswertungZuusctand.Docked() &&
						!(true == AusScnapscusAuswertungZuusctand.UnDocking()))
					{
						if (null != ScnapscusCurrentLocationInfo.SolarSystemName &&
							null != ScnapscusCurrentLocationInfo.NearestName)
						{
							var StationIdent = new SictAgentIdentSystemStationName(
								ScnapscusCurrentLocationInfo.SolarSystemName,
								ScnapscusCurrentLocationInfo.NearestName);

							var ZuStationDictAgent = Optimat.Glob.TAD(MengeZuStationMengeAgent, StationIdent);

							if (null == ZuStationDictAgent)
							{
								ZuStationDictAgent = new Dictionary<string, SictWertMitZait<VonSensor.LobbyAgentEntry>>();

								MengeZuStationMengeAgent[StationIdent] = ZuStationDictAgent;
							}

							foreach (var ScnapscusAgentEntry in AusGbsWindowLobbyMengeAgentEntry)
							{
								var AgentName = ScnapscusAgentEntry.AgentName;

								if (null == AgentName)
								{
									continue;
								}

								var BisherAgentEntry = Optimat.Glob.TAD(ZuStationDictAgent, AgentName).Wert;

								var BisherAgentEntryErhalte = false;

								if (null != BisherAgentEntry)
								{
									if (null != BisherAgentEntry.AgentTyp &&
										null == ScnapscusAgentEntry.AgentTyp)
									{
										BisherAgentEntryErhalte = true;
									}

									if (BisherAgentEntry.AgentLevel.HasValue &&
										!ScnapscusAgentEntry.AgentLevel.HasValue)
									{
										BisherAgentEntryErhalte = true;
									}
								}

								if (BisherAgentEntryErhalte)
								{
									continue;
								}

								ZuStationDictAgent[AgentName] = new SictWertMitZait<VonSensor.LobbyAgentEntry>(ZaitMili, ScnapscusAgentEntry);
							}
						}
					}

					if (null != AusScnapscusAuswertungZuusctand.MengeWindowAgentDialogue)
					{
						foreach (var FensterAgentDialogue in AusScnapscusAuswertungZuusctand.MengeWindowAgentDialogue)
						{
							//	Vorerst werd folgende Annaame verwendet: Agent Name sind als aindoitige Bezaicner verwendbar.

							var FensterAgentDialogueAgentName = FensterAgentDialogue.AgentName;

							if (null == FensterAgentDialogueAgentName)
							{
								continue;
							}

							foreach (var KandidaatStation in MengeZuStationMengeAgent)
							{
								if (null == KandidaatStation.Value)
								{
									continue;
								}

								if (KandidaatStation.Value
									.Any((KandidaatZuZaitAgentEntry) =>
										string.Equals(FensterAgentDialogueAgentName, KandidaatZuZaitAgentEntry.Key, StringComparison.InvariantCultureIgnoreCase)))
								{
									var AgentDialogueZuZaitJezt = new SictWertMitZait<VonSensor.WindowAgentDialogue>(ZaitMili, FensterAgentDialogue);

									var AgentIdent = KandidaatStation.Key.Kopii();

									AgentIdent.AgentName = FensterAgentDialogueAgentName;

									MengeZuAgentDialogLezte[AgentIdent] = AgentDialogueZuZaitJezt;

									/*
									 * 2015.02.20
									 * 
									if (null != FensterAgentDialogue.RightPaneMissionInfo)
									{
										if (null != FensterAgentDialogue.RightPaneMissionInfo.Objective)
										{
									 * */
									if (null != FensterAgentDialogue.ZusamefasungMissionInfo)
									{
										{
											MengeZuAgentMissionInfoLezte[AgentIdent] = AgentDialogueZuZaitJezt;
										}
									}
								}
							}
						}
					}
				}

				if (null != OptimatParam)
				{
					if (null != AktioonAcceptMengeAgentLevelFraigaabe)
					{
						AusScnapscusLobbyMengeAgentEntryPasendZuVonNuzerKonfig = new List<VonSensor.LobbyAgentEntry>();
						AusScnapscusLobbyMengeAgentEntryPasendZuVonNuzerKonfigUndOhneUnpasendeMission = new List<VonSensor.LobbyAgentEntry>();

						var MengeAgentEntry = AusGbsWindowLobby.MengeAgentEntry;

						if (null != MengeAgentEntry)
						{
							foreach (var AgentEntry in MengeAgentEntry)
							{
								if (!(true == AgentEntryPasendZuVonNuzerParam(AgentEntry, OptimatParam)))
								{
									continue;
								}

								{
									//	!!!!	Temp Filter, scpääter zu erseze durc VonNuzerParam

									var AgentTyp = AgentEntry.AgentTyp;

									var AgentTypMatch = Regex.Match(AgentTyp ?? "", "security", RegexOptions.IgnoreCase);

									if (!AgentTypMatch.Success)
									{
										continue;
									}
								}

								AusScnapscusLobbyMengeAgentEntryPasendZuVonNuzerKonfig.Add(AgentEntry);

								if (null != ScnapscusCurrentLocationInfo)
								{
									var ZuAgentMissionInfoLezteNulbar =
										Optimat.Glob.TADNulbar(
										MengeZuAgentMissionInfoLezte,
										new SictAgentIdentSystemStationName(
											ScnapscusCurrentLocationInfo.SolarSystemName,
											ScnapscusCurrentLocationInfo.NearestName,
											AgentEntry.AgentName));

									if (ZuAgentMissionInfoLezteNulbar.HasValue)
									{
										var ZuAgentMissionInfoLezteNulbarAlterMili = ZaitMili - ZuAgentMissionInfoLezteNulbar.Value.Zait;

										if (null != ZuAgentMissionInfoLezteNulbar.Value.Wert &&
											ZuAgentMissionInfoLezteNulbarAlterMili < ZuAgentMissionInfoLezteAlterScrankeMax * 1000)
										{
											if (false == MissionObjectivePasendZuVonNuzerParam(AgentEntry, ZuAgentMissionInfoLezteNulbar.Value.Wert, OptimatParam))
											{
												continue;
											}
										}
									}

									AusScnapscusLobbyMengeAgentEntryPasendZuVonNuzerKonfigUndOhneUnpasendeMission.Add(AgentEntry);
								}
							}
						}
					}
				}

			}
			finally
			{
				this.AusScnapscusLobbyMengeAgentEntryPasendZuVonNuzerKonfig =
					AusScnapscusLobbyMengeAgentEntryPasendZuVonNuzerKonfig;

				this.AusScnapscusLobbyMengeAgentEntryPasendZuVonNuzerKonfigUndOhneUnpasendeMission =
					AusScnapscusLobbyMengeAgentEntryPasendZuVonNuzerKonfigUndOhneUnpasendeMission;

				this.MengeZuAgentDialogLezte = MengeZuAgentDialogLezte;
				this.MengeZuAgentMissionInfoLezte = MengeZuAgentMissionInfoLezte;
				this.MengeZuStationMengeAgent = MengeZuStationMengeAgent;
			}
		}


		static public string StationNameSictFürButtonListSurroundingsRegexPattern(string StationName)
		{
			/*
			 * 2013.08.19
			 * Beobactung: In Station Name wii in Button ListSurroundings (Info Panel Location) angezaigt werd "Moon" mit "M" abgekürzt.
			 * Dabei werd auc das Leerzaice zwisce "Moon" und Index entfernt.
			 * */

			var RegexPattern =
				Regex.Replace(
				StationName,
				"Moon\\s*",
				"M([\\w]*\\s*)",
				RegexOptions.IgnoreCase);

			return RegexPattern;
		}

	}

}
