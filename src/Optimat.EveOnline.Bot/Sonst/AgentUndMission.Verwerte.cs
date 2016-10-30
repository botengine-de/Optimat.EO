using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Optimat.EveOnline.Base;
using Optimat.ScpezEveOnln;
using Bib3;

namespace Optimat.EveOnline.Anwendung
{
	public partial class SictAgentUndMissionZuusctand
	{
		public IEnumerable<SictAufgaabeParam> FürMissionListeAufgaabeNääxteParamBerecne(
			ISictAutomatZuusctand AutomaatZuusctand)
		{
			IEnumerable<SictAufgaabeParam> FürMissionListeAufgaabeNääxteParam = null;

			FürMissionListeAufgaabeNääxteParamBerecne(AutomaatZuusctand, out	FürMissionListeAufgaabeNääxteParam);

			return FürMissionListeAufgaabeNääxteParam;
		}

		public void FürMissionListeAufgaabeNääxteParamBerecne(
			ISictAutomatZuusctand AutomaatZuusctand,
			out	IEnumerable<SictAufgaabeParam> FürMissionListeAufgaabeNääxteParam)
		{
			var ListeAufgaabeNääxteParam = new List<SictAufgaabeParam>();

			FürMissionListeAufgaabeNääxteParam = ListeAufgaabeNääxteParam;

			if (null == AutomaatZuusctand)
			{
				return;
			}

			var OptimatParam = AutomaatZuusctand.OptimatParam();

			if (null == OptimatParam)
			{
				return;
			}

			var ZaitMili = AutomaatZuusctand.NuzerZaitMili;

			var AusScnapscusAuswertungZuusctand = AutomaatZuusctand.ListeScnapscusLezteAuswertungErgeebnisNaacSimu;

			var GbsButtonListSurroundings =
				(null == AusScnapscusAuswertungZuusctand) ? null :
				AusScnapscusAuswertungZuusctand.InfoPanelLocationInfoButtonListSurroundings();

			var UtilmenuMission = (null == AusScnapscusAuswertungZuusctand) ? null : AusScnapscusAuswertungZuusctand.UtilmenuMission;

			var MengeWindowAgentDialogue = (null == AusScnapscusAuswertungZuusctand) ? null : AusScnapscusAuswertungZuusctand.MengeWindowAgentDialogue;

			var FittingUndShipZuusctand = AutomaatZuusctand.FittingUndShipZuusctand;
			var InRaumAktioonUndGefect = AutomaatZuusctand.InRaumAktioonUndGefect;

			var UtilmenuMissionLezteNocOfeMitBeginZait = this.UtilmenuMissionLezteNocOfeMitBeginZait;

			var UtilmenuMissionLezteNocOfeAlter = (ZaitMili - UtilmenuMissionLezteNocOfeMitBeginZait.Zait) / 1000;

			var VonNuzerParamMission = (null == OptimatParam) ? null : OptimatParam.Mission;

			var VonNuzerParamMissionAktioonAcceptFraigaabe = (null == VonNuzerParamMission) ? null : VonNuzerParamMission.AktioonAcceptFraigaabe;

			var MissionAktuel = this.MissionAktuel;

			var MissionAktuelObjectiveZuZaitLezteNulbar =
				(null == MissionAktuel) ? null : MissionAktuel.ListeMesungObjectiveZuusctandZuZaitLezteBerecne();

			var MissionAktuelObjectiveLezte =
				MissionAktuelObjectiveZuZaitLezteNulbar.HasValue ? MissionAktuelObjectiveZuZaitLezteNulbar.Value.Wert : null;

			var MissionAktuelWindowAgentDialogueZuZaitLezte = (null == MissionAktuel) ? null : MissionAktuel.WindowAgentDialogueZuZaitLezteBerecne();

			var MissionAktuelTailFürNuzer = (null == MissionAktuel) ? null : MissionAktuel.TailFürNuzer;

			var MissionAktuelAgentLevel = (null == MissionAktuelTailFürNuzer) ? null : MissionAktuelTailFürNuzer.AgentLevel;

			var MissionAktuelStrategikon = (null == MissionAktuel) ? null : MissionAktuel.Strategikon;

			var MissionAktuelStrategikonFürLevel4WarpToWithinFürDistanceAbhängigVonWirkungDestruktRange =
				(null == MissionAktuelStrategikon) ? null :
				MissionAktuelStrategikon.FürLevel4WarpToWithinFürDistanceAbhängigVonWirkungDestruktRange;

			var MissionZiilLocationNääxteInUtilmenu = (null == MissionAktuel) ? null : MissionAktuel.ZiilLocationNääxteInUtilmenu;
			var MissionButtonUtilmenu = (null == MissionAktuel) ? null : MissionAktuel.ButtonUtilmenu;

			var ShipZuusctandLezte = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.ShipZuusctand;

			var MissionAnforderungActiveShipCargoLeereLezteZaitMili = this.AnforderungActiveShipCargoLeereLezteZaitMili;

			var AnnaameActiveShipCargoLeerLezteZaitMili = AutomaatZuusctand.AnnaameActiveShipCargoGeneralLeerLezteZaitMili;

			var SelbsctShipDocking = (null == ShipZuusctandLezte) ? null : ShipZuusctandLezte.Docking;
			var SelbsctShipDocked = (null == ShipZuusctandLezte) ? null : ShipZuusctandLezte.Docked;
			var SelbsctShipWarping = (null == ShipZuusctandLezte) ? null : ShipZuusctandLezte.Warping;
			var WarpingLezteZaitMili = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.WarpingLezteZaitMili;
			var JumpingLezteZaitMili = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.JumpingLezteZaitMili;

			var WarpingLezteAlterMili = ZaitMili - WarpingLezteZaitMili;
			var JumpingLezteAlterMili = ZaitMili - JumpingLezteZaitMili;

			var AnnaameGefectDistanceOptimum = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.AnnaameModuleDestruktRangeOptimum;
			var FittingUndShipZuusctandAnnaameTargetingDistanceScrankeMax = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.AnnaameTargetingDistanceScrankeMax;

			var LobbyAgentEntryStartConversation = this.LobbyAgentEntryStartConversation;
			var MissionButtonUtilmenuObjectiveZuMese = this.MissionButtonUtilmenuObjectiveZuMese;
			var WindowAgentDialogueMissionAcceptOderRequest = this.WindowAgentDialogueMissionAcceptOderRequest;

			var MissionAcceptNääxte = this.MissionAcceptNääxte;
			var MissionDeclineNääxte = this.MissionDeclineNääxte;
			var InAktuelemSystemMengeStationFürWelcePasendeMissionNictAusgesclose = this.InAktuelemSystemMengeStationFürWelcePasendeMissionNictAusgesclose;

			var ZuBeginZaitMissionFittingZuTesteNääxte = this.ZuBeginZaitMissionFittingZuTesteNääxte;

			var CurrentLocation = (null == AusScnapscusAuswertungZuusctand) ? null : AusScnapscusAuswertungZuusctand.CurrentLocationInfo();

			var CurrentLocationNearestName = (null == CurrentLocation) ? null : CurrentLocation.NearestName;

			var MissionZiilLocationNääxteAuswert = (null == MissionAktuel) ? null : MissionAktuel.ZiilLocationNääxteAuswert;

			var MissionZiilLocationNääxteErraict = (null == MissionZiilLocationNääxteAuswert) ? null : MissionZiilLocationNääxteAuswert.LocationErraict;

			var MissionZiilLocationNääxteAuswertObjectiveLocation = (null == MissionZiilLocationNääxteAuswert) ? null : MissionZiilLocationNääxteAuswert.Objective;

			var MissionAktuelWindowAgentDialogueVolsctändigGeöfnet =
				(null == MengeWindowAgentDialogue || null == MissionAktuel) ? null :
				MengeWindowAgentDialogue.FirstOrDefault((KandidaatWindow) => EveOnline.Anwendung.SictMissionZuusctand.WindowAgentPastZuMission(MissionAktuel, KandidaatWindow, true, true, true, true));

			var MissionAktuelWindowAgentDialogueHalbGeöfnet =
				(null == MengeWindowAgentDialogue || null == MissionAktuel) ? null :
				MengeWindowAgentDialogue.FirstOrDefault((KandidaatWindow) => EveOnline.Anwendung.SictMissionZuusctand.WindowAgentPastZuMission(MissionAktuel, KandidaatWindow, true, true, false, false));

			var RaumVerlaseFraigaabe = (null == InRaumAktioonUndGefect) ? null : InRaumAktioonUndGefect.RaumVerlaseFraigaabe;

			var GefectBaitritFraigaabe = (null == InRaumAktioonUndGefect) ? null : InRaumAktioonUndGefect.GefectBaitritFraigaabe;

			var AbovemainMessageCannotWarpThereLezte = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.AbovemainMessageCannotWarpThereLezte;

			var AbovemainMessageCannotWarpThereLezteBeginZait =
				(null == AbovemainMessageCannotWarpThereLezte) ? null : AbovemainMessageCannotWarpThereLezte.BeginZait;

			var AbovemainMessageCannotWarpThereLezteBeginAlterMili = ZaitMili - AbovemainMessageCannotWarpThereLezteBeginZait;

			var MissionListeMesungObjectiveZuusctandLezteMitZaitNulbar =
				(null == MissionAktuel) ? null : MissionAktuel.ListeMesungObjectiveZuusctandZuZaitLezteBerecne();

			var MissionListeMesungObjectiveZuusctandLezte =
				MissionListeMesungObjectiveZuusctandLezteMitZaitNulbar.HasValue ?
				MissionListeMesungObjectiveZuusctandLezteMitZaitNulbar.Value.Wert : null;

			var MissionListeMesungObjectiveZuusctandLezteCompleteBisAufLocationDropOff =
				(null == MissionListeMesungObjectiveZuusctandLezte) ? null : MissionListeMesungObjectiveZuusctandLezte.CompleteBisAufLocationDropOffBerecne();

			var MissionAktuelAgentLocation =
				(null == MissionAktuelTailFürNuzer) ? null : MissionAktuelTailFürNuzer.AgentLocation;

			var MissionAktuelAgentLocationLocationNameTailSystem =
				(null == MissionAktuelAgentLocation) ? null : MissionAktuelAgentLocation.LocationNameTailSystem;

			var CurrentLocationSolarSystemName =
				(null == CurrentLocation) ? null : CurrentLocation.SolarSystemName;

			var UtilmenuZuEntferneAlter =
				null == UtilmenuMissionLezteNocOfeMitBeginZait.Wert ? (Int64?)null :
				UtilmenuMissionLezteNocOfeAlter;

			if (null != MissionAktuelTailFürNuzer)
			{
				if (MissionAktuelTailFürNuzer.FürMissionFittingBezaicner.IsNullOrEmpty())
				{
					ListeAufgaabeNääxteParam.Add(AufgaabeParamAndere.KonstruktNaacNuzerMeldungZuEveOnline(
						SictNaacNuzerMeldungZuEveOnline.WarningGenerel(-1, SictNaacNuzerMeldungZuEveOnlineCauseTypeBinär.MissionCurrentNoFittingConfigured)));
				}
			}

			if (33 < UtilmenuZuEntferneAlter)
			{
				ListeAufgaabeNääxteParam.Add(AufgaabeParamAndere.KonstruktMenuEntferne());
			}

			if (null != MissionAktuelWindowAgentDialogueVolsctändigGeöfnet &&
				null != MissionAktuelObjectiveLezte)
			{
				if (true == MissionAktuelObjectiveLezte.Complete)
				{
					//	Button Complete sol ersct naac Messung Objective Complete betäätigt werde da sunsct der Erfolg der Mission scpääter nit erkant werd.

					var ButtonComplete = MissionAktuelWindowAgentDialogueVolsctändigGeöfnet.ButtonComplete;

					if (null == ButtonComplete)
					{
					}
					else
					{
						/*
						2014.04.24 ("20:06") Beobactung Probleeem:
						"Mission of Mercy" werd von Automaat "complete remotely" -> mit Complete warte bis in Station "Agent Home Base" gedockt.
						 * 
						 * Daher noie Bedingung für Complete: ersct wenn Scpiiler in glaiche System gedockt isc in welcem sic der Agent zur Mission befindet.
						 * */

						if (true == SelbsctShipDocked &&
							string.Equals(MissionAktuelAgentLocationLocationNameTailSystem, CurrentLocationSolarSystemName, StringComparison.InvariantCultureIgnoreCase))
						{
							ListeAufgaabeNääxteParam.Add(
								SictAufgaabeParam.KonstruktAufgaabeParam(
								AufgaabeParamAndere.KonstruktMausPfaad(
								SictAufgaabeParamMausPfaad.KonstruktMausKlikLinx(ButtonComplete)),
								new	string[]{	SictAufgaabeParam.ObjektSictZwekKomponente(MissionAktuel) + ".Complete"}));
						}
					}
				}
			}

			if (null != MissionListeMesungObjectiveZuusctandLezte &&
				!(false == MissionZiilLocationNääxteErraict))
			{
				//	Mission Complete

				if (true == MissionListeMesungObjectiveZuusctandLezte.Complete ||
					true == MissionListeMesungObjectiveZuusctandLezteCompleteBisAufLocationDropOff)
				{
					if (null != MissionAktuelWindowAgentDialogueHalbGeöfnet)
					{
						var ButtonRequestMission = MissionAktuelWindowAgentDialogueHalbGeöfnet.ButtonRequestMission;
						var ButtonViewMission = MissionAktuelWindowAgentDialogueHalbGeöfnet.ButtonViewMission;

						var Button = ButtonRequestMission ?? ButtonViewMission;

						if (null != Button)
						{
							ListeAufgaabeNääxteParam.Add(
								SictAufgaabeParam.KonstruktAufgaabeParam(
								AufgaabeParamAndere.KonstruktMausPfaad(SictAufgaabeParamMausPfaad.KonstruktMausKlikLinx(Button)),
								new string[]{
								"Button[" + (Button.Bescriftung ?? "") + "].click"}));
							return;
						}
					}

					var DialogueBeraitsGeöfnet = false;

					if (MissionAktuelWindowAgentDialogueZuZaitLezte.HasValue)
					{
						if (ZaitMili <= MissionAktuelWindowAgentDialogueZuZaitLezte.Value.Zait &&
							null != MissionAktuelWindowAgentDialogueZuZaitLezte.Value.Wert)
						{
							DialogueBeraitsGeöfnet = true;
						}
					}

					if (!DialogueBeraitsGeöfnet)
					{
						ListeAufgaabeNääxteParam.Add(AufgaabeParamAndere.KonstruktMissionStartConversation(MissionAktuel));
					}
				}
			}

			if (null != MissionZiilLocationNääxteAuswert)
			{
				//	zu nääxte Ziil Location raise

				if (JumpingLezteAlterMili < 5e+3 ||
					WarpingLezteAlterMili < 4e+3)
				{
					return;
				}

				var MissionZiilLocationNääxteInfoAggr = MissionAktuel.ZuObjectiveLocationLocationInfoAggr(MissionZiilLocationNääxteAuswert, true);

				var MissionZiilLocationNääxteIstDeadspace = (null == MissionZiilLocationNääxteInfoAggr) ? null : MissionZiilLocationNääxteInfoAggr.IstDeadspace;

				if (false == MissionZiilLocationNääxteAuswert.LocationErraict)
				{
					if (false == MissionZiilLocationNääxteAuswert.LocationErraictTailSystem &&
							true == MissionZiilLocationNääxteAuswert.LocationSystemGlaicInfoPanelRouteDestinationSystem)
					{
						//	Ziilsystem isc in InfoPanel Route als Ziil gesezt.

						ListeAufgaabeNääxteParam.Add(new AufgaabeParamInfoPanelRouteFüüreAus());
					}

					if (!(JumpingLezteAlterMili < 5e+3))
					{
						if (true == SelbsctShipDocked)
						{
							ListeAufgaabeNääxteParam.Add(AufgaabeParamAndere.KonstruktUnDock());
						}

						var InZiilLocationSystemUnDockedAktioon =
							(true == MissionZiilLocationNääxteAuswert.LocationErraictTailSystem &&
							!(true == SelbsctShipWarping) &&
							!(true == SelbsctShipDocking) &&
							false == SelbsctShipDocked);

						{
							//	2014.02.26	Noie Verzwaigung: Für LVL4 Mission sol "Warp to Location" -> "Within" verwendet werde.

							if (InZiilLocationSystemUnDockedAktioon &&
								(false == MissionZiilLocationNääxteAuswert.LocationErfordertDock) &&
								!(MissionAktuelAgentLevel < 4) &&
								!(true == MissionZiilLocationNääxteIstDeadspace) &&
								null != MissionZiilLocationNääxteInfoAggr &&
								true == MissionAktuelStrategikonFürLevel4WarpToWithinFürDistanceAbhängigVonWirkungDestruktRange &&
								!(true == SelbsctShipDocked))
							{
								var MenuEntryMissionRegexPattern = VonSensor.MenuEntryScpez.InSurroundingsMenuEntryAgentMissionRegexPattern(
									MissionAktuelTailFürNuzer);

								//	!!!!	Noc zu sctabilisiire: tatsäclice Bezaicnung ermitle, z.B. aus UtilMenu
								var MenuEntryLocationRegexPattern = "Encounter";

								var MenuEntryWarpToLocationRegexPattern = "Warp to Location";

								var WarpDistanceOptimum = Math.Min((AnnaameGefectDistanceOptimum + 10000) ?? 100000, FittingUndShipZuusctandAnnaameTargetingDistanceScrankeMax ?? 80000);

								var AnnaameMengeWarpDistanceVerfüügbar =
									Enumerable.Range(0, 21).Select((Index) => Index * 5000).ToArray();

								//	!!!!	Noc zu sctabilisiire: tatsäclic verfüügbaare Entry ermitle aus lezte Menu
								var AnnaameMengeWarpDistanceVerfüügbarMitRegexPattern =
									AnnaameMengeWarpDistanceVerfüügbar
									.Select((Distance) => new KeyValuePair<int, string>(Distance, "within\\s*" + (Distance / 1000).ToString() + "\\s*km"))
									.ToArray();

								var ListeEntryDistanceRegexPatternOrdnetNaacGüte =
									AnnaameMengeWarpDistanceVerfüügbarMitRegexPattern
									.OrderBy((Kandidaat) => Math.Abs(Kandidaat.Key - WarpDistanceOptimum))
									.Select((Kandidaat) => Kandidaat.Value)
									.ToArray();

								ListeAufgaabeNääxteParam.Add(AufgaabeParamAndere.AufgaabeAktioonMenu(
									GbsButtonListSurroundings,
									new SictAnforderungMenuKaskaadeAstBedingung[]{
											new	SictAnforderungMenuKaskaadeAstBedingung(MenuEntryMissionRegexPattern),
											new	SictAnforderungMenuKaskaadeAstBedingung(MenuEntryLocationRegexPattern),
											new	SictAnforderungMenuKaskaadeAstBedingung(new	string[]{
												MenuEntryWarpToLocationRegexPattern	+ "$",
												MenuEntryWarpToLocationRegexPattern}),
											new	SictAnforderungMenuKaskaadeAstBedingung(ListeEntryDistanceRegexPatternOrdnetNaacGüte),
										}));
							}
						}

						if ((false == MissionZiilLocationNääxteAuswert.LocationErraictTailSystem &&
							false == MissionZiilLocationNääxteAuswert.LocationSystemGlaicInfoPanelRouteDestinationSystem) ||
							InZiilLocationSystemUnDockedAktioon)
						{
							var MissionZiilLocationNääxteLocationName =
								MissionZiilLocationNääxteAuswert.Objective.Location.LocationName;

							var ZwekListeKomponente = new string[] { "travel to Mission Location[" + (MissionZiilLocationNääxteLocationName ?? "") + "]" };

							//	Wen (Ship sic noc nit im Ziil Solarsysteem befindet und Route noc nit naac InfoPanelRoute gesezt wurde) oder (Ship sic scon im Ziil Solarsysteem und nit in Warp befindet) sol über Utilmenu waitergesctoiert werde.

							if (AbovemainMessageCannotWarpThereLezteBeginAlterMili < 11111)
							{
								ListeAufgaabeNääxteParam.Add(AufgaabeParamAndere.KonstruktGridVerlase());
							}

							if (null == MissionZiilLocationNääxteInUtilmenu)
							{
								if (null == MissionButtonUtilmenu)
								{
								}
								else
								{
									ListeAufgaabeNääxteParam.Add(
										SictAufgaabeParam.KonstruktAufgaabeParam(
										AufgaabeParamAndere.KonstruktMausPfaad(SictAufgaabeParamMausPfaad.KonstruktMausKlikLinx(MissionButtonUtilmenu)),
										Optimat.Glob.ListeErwaitertAlsArray(ZwekListeKomponente, new string[] { "InfoPanelMenu.Create" })));
								}
							}
							else
							{
								var KnopfSetDestination = MissionZiilLocationNääxteInUtilmenu.KnopfSetDestination;
								var KnopfWarpTo = MissionZiilLocationNääxteInUtilmenu.KnopfWarpTo;
								var KnopfDock = MissionZiilLocationNääxteInUtilmenu.KnopfDock;
								var KnopfApproach = MissionZiilLocationNääxteInUtilmenu.KnopfApproach;

								//	WarpTo oder Dock oder Approach nur dan betäätige wen nict geraade in Warp

								var KnopfZuBetäätige =
									((true == SelbsctShipWarping) ? null : (KnopfWarpTo ?? KnopfDock)) ??
									KnopfSetDestination;

								var KnopfBetäätigeFraigaabe =
									KnopfSetDestination == KnopfZuBetäätige;

								if (true == RaumVerlaseFraigaabe)
								{
									if (KnopfWarpTo == KnopfZuBetäätige &&
										true == GefectBaitritFraigaabe)
									{
										KnopfBetäätigeFraigaabe = true;
									}

									if (KnopfDock == KnopfZuBetäätige)
									{
										KnopfBetäätigeFraigaabe = true;
									}
								}

								if (null != KnopfZuBetäätige && KnopfBetäätigeFraigaabe)
								{
									ListeAufgaabeNääxteParam.Add(
										SictAufgaabeParam.KonstruktAufgaabeParam(
										AufgaabeParamAndere.KonstruktMausPfaad(SictAufgaabeParamMausPfaad.KonstruktMausKlikLinx(KnopfZuBetäätige)),
										Optimat.Glob.ListeErwaitertAlsArray(ZwekListeKomponente, new string[] { "Button[" + KnopfZuBetäätige.Bescriftung + "]" })));
								}
							}
						}
					}
				}
			}

			if (null != WindowAgentDialogueMissionAcceptOderRequest)
			{
				ListeAufgaabeNääxteParam.Add(AufgaabeParamAndere.KonstruktWindowAgentDialogueMissionAcceptOderRequest(WindowAgentDialogueMissionAcceptOderRequest));
				return;
			}

			/*
			 * 2014.06.10
			 * 
			if (null != WindowAgentDialogueMissionDecline)
			{
				ListeAufgaabeNääxteParam.Add(SictAufgaabeParam.KonstruktWindowAgentDialogueMissionDecline(WindowAgentDialogueMissionDecline));
				return;
			}
			 * */

			if (null != MissionDeclineNääxte)
			{
				ListeAufgaabeNääxteParam.Add(AufgaabeParamAndere.KonstruktMissionDecline(MissionDeclineNääxte));
				return;
			}

			if (null != MissionAcceptNääxte)
			{
				ListeAufgaabeNääxteParam.Add(AufgaabeParamAndere.KonstruktMissionAccept(MissionAcceptNääxte));
				return;
			}

			if (null != LobbyAgentEntryStartConversation)
			{
				var WindowAgentDialoguePasendZuAgentEntry =
					ExtractFromOldAssembly.Bib3.Extension.FirstOrDefaultNullable(
					MengeWindowAgentDialogue,
					(Kandidaat) => string.Equals(Kandidaat.AgentName, LobbyAgentEntryStartConversation.AgentName, StringComparison.InvariantCultureIgnoreCase));

				if (null != WindowAgentDialoguePasendZuAgentEntry)
				{
					var ButtonRequestMission = WindowAgentDialoguePasendZuAgentEntry.ButtonRequestMission;
					var ButtonViewMission = WindowAgentDialoguePasendZuAgentEntry.ButtonViewMission;

					var Button = ButtonRequestMission ?? ButtonViewMission;

					if (null != Button)
					{
						ListeAufgaabeNääxteParam.Add(
							SictAufgaabeParam.KonstruktAufgaabeParam(
							AufgaabeParamAndere.KonstruktMausPfaad(SictAufgaabeParamMausPfaad.KonstruktMausKlikLinx(Button)),
							SictAufgaabeParam.ObjektSictZwekKomponente(WindowAgentDialoguePasendZuAgentEntry) + "." + (Button.Bescriftung	?? "")));
						return;
					}
				}

				ListeAufgaabeNääxteParam.Add(AufgaabeParamAndere.KonstruktLobbyAgentEntryStartConversation(LobbyAgentEntryStartConversation));
				return;
			}

			if (null != MissionButtonUtilmenuObjectiveZuMese)
			{
				ListeAufgaabeNääxteParam.Add(AufgaabeParamAndere.KonstruktMissionButtonUtilmenuObjectiveZuMese(MissionButtonUtilmenuObjectiveZuMese));
				return;
			}

			if (null != ZuBeginZaitMissionFittingZuTesteNääxte.Wert)
			{
				if (!(true == SelbsctShipDocked))
				{
					var MissionFittingAgentLocation = ZuBeginZaitMissionFittingZuTesteNääxte.Wert.AgentLocation();

					if (null == MissionFittingAgentLocation)
					{
					}
					else
					{
						ListeAufgaabeNääxteParam.Add(new	AufgaabeParamShipDock(MissionFittingAgentLocation.LocationName));
						return;
					}
				}

				if (!(MissionAnforderungActiveShipCargoLeereLezteZaitMili < AnnaameActiveShipCargoLeerLezteZaitMili))
				{
					//	Warte bis Cargo Leere abgesclose
					return;
				}

				if (null != ZuBeginZaitMissionFittingZuTesteNääxte.Wert.FürMissionFittingBezaicner())
				{
					ListeAufgaabeNääxteParam.Add(AufgaabeParamAndere.KonstruktFittingZuApliziire(
						new SictOptimatParamFitting(null, ZuBeginZaitMissionFittingZuTesteNääxte.Wert.FürMissionFittingBezaicner())));
					return;
				}
			}

			if (ZaitMili - WarpingLezteZaitMili < 3333)
			{
				//	Auf Ende Warp warte.
				return;
			}

			if (!(true == VonNuzerParamMissionAktioonAcceptFraigaabe))
			{
				return;
			}

			if (null != InAktuelemSystemMengeStationFürWelcePasendeMissionNictAusgesclose &&
				null != CurrentLocation)
			{
				//	Aine der Station aufsuuce in welce nit jeeder Agent unpasende Mission geOffered hat

				if (0 < InAktuelemSystemMengeStationFürWelcePasendeMissionNictAusgesclose.Length)
				{
					var InAktuelemSystemMengeStationFürWelcePasendeMissionNictAusgescloseOrdnet =
						InAktuelemSystemMengeStationFürWelcePasendeMissionNictAusgesclose
						.OrderByDescending((StationUndAnzaal) => StationUndAnzaal.Value ?? 1)
						.ToArray();

					var MengeStationName =
						InAktuelemSystemMengeStationFürWelcePasendeMissionNictAusgescloseOrdnet
						.Select((Kandidaat) => Kandidaat.Key)
						.Where((Kandidaat) => null != Kandidaat)
						.ToArray();

					var BeraitsDockedInStationMitAgentOoneUnpasendeOffer =
						true == SelbsctShipDocked &&
						null != CurrentLocationNearestName &&
						MengeStationName.Any((StationName) => string.Equals(StationName, CurrentLocationNearestName));

					if (!BeraitsDockedInStationMitAgentOoneUnpasendeOffer)
					{
						if (0 < MengeStationName.Length)
						{
							if (true == AusScnapscusAuswertungZuusctand.Docked())
							{
								ListeAufgaabeNääxteParam.Add(AufgaabeParamAndere.KonstruktUnDock());
								return;
							}

							ListeAufgaabeNääxteParam.AddRange(
								MengeStationName.Select((StationName) => new AufgaabeParamShipDock(StationName)));
						}
					}
				}
			}

			if (11 < UtilmenuZuEntferneAlter)
			{
				ListeAufgaabeNääxteParam.Add(AufgaabeParamAndere.KonstruktMenuEntferne());
			}
		}
	}
}
