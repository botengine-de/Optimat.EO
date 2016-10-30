using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Optimat.ScpezEveOnln;
using Bib3;
//using Optimat.EveOnline.AuswertGbs;
using Optimat.EveOnline.Anwendung.AuswertGbs;
using Optimat.EveOnline.Base;
using VonSensor = Optimat.EveOnline.VonSensor;
using Optimat.EveOnline.VonSensor;
using ExtractFromOldAssembly.Bib3;

namespace Optimat.EveOnline.Anwendung
{
	public partial class SictMissionZuusctand
	{
		static	public	bool	MenuEntryRepräsentiirtMission(
			EveOnline.SictMissionZuusctand	Mission,
			MenuEntryScpez	KandidaatMenuEntry)
		{
			if(null	== Mission	||	null	== KandidaatMenuEntry)
			{
				return	false;
			}

			return
				string.Equals(ExtractFromOldAssembly.Bib3.Glob.TrimNullable(KandidaatMenuEntry.AgentMissionTitel), ExtractFromOldAssembly.Bib3.Glob.TrimNullable(Mission.Titel)) &&
				string.Equals(ExtractFromOldAssembly.Bib3.Glob.TrimNullable(KandidaatMenuEntry.AgentName), ExtractFromOldAssembly.Bib3.Glob.TrimNullable(Mission.AgentName));
		}

		static	public	VonSensor.MenuEntryScpez	AusMenuEntryRepräsentiirendMission(
			EveOnline.SictMissionZuusctand	Mission,
			VonSensor.Menu	Menu)
		{
			if(null	== Mission	||	null	== Menu)
			{
				return	null;
			}

			var	MengeEntryAgentMission	= Menu.MengeEntryAgentMissionBerecne();

			if(null	== MengeEntryAgentMission)
			{
				return	null;
			}

			return	MengeEntryAgentMission.FirstOrDefault((KandidaatEntry) => MenuEntryRepräsentiirtMission(Mission, KandidaatEntry));
		}

		public void Aktualisiire(
			SictAutomatZuusctand AutomaatZuusctand,
			VonSensorikMesung AusScnapscusAuswertungZuusctand)
		{
			VonSensor.InfoPanelMissionsMission ButtonUtilmenu = null;
			SictWertMitZait<VonSensor.UtilmenuMissionInfo>? UtilmenuZuZait = null;
			VonSensor.WindowAgentMissionObjectiveObjective ZiilLocationNääxteObjective = null;
			SictObjectiveLocationAuswert ZiilLocationNääxteAuswert = null;

			VonSensor.UtilmenuMissionLocationInfo ZiilLocationNääxteInUtilmenu	= null;

			bool? AnnaameCompleteFallsInAgentStation = null;

			try
			{
				if (null == AutomaatZuusctand)
				{
					return;
				}

				var ZaitMili = AutomaatZuusctand.NuzerZaitMili;

				this.ZaitMili = ZaitMili;

				var TailFürNuzer = this.TailFürNuzer;

				if (null == TailFürNuzer)
				{
					return;
				}

				var Gbs = AutomaatZuusctand.Gbs;

				var	AgentLocation	= (null	== TailFürNuzer)	? null	: TailFürNuzer.AgentLocation;

				var MissionTitel = (null == TailFürNuzer) ? null : TailFürNuzer.Titel;

				var CurrentLocation = (null == AusScnapscusAuswertungZuusctand) ? null : AusScnapscusAuswertungZuusctand.CurrentLocationInfo();

				var InfoPanelMissions = (null == AusScnapscusAuswertungZuusctand) ? null : AusScnapscusAuswertungZuusctand.InfoPanelMissions;

				var InfoPanelRoute = (null == AusScnapscusAuswertungZuusctand) ? null : AusScnapscusAuswertungZuusctand.InfoPanelRoute;

				var InfoPanelRouteEnd = (null == InfoPanelRoute) ? null : InfoPanelRoute.EndInfo;

				var InfoPanelListeMissionButton = (null == InfoPanelMissions) ? null : InfoPanelMissions.ListeMissionButton;

				var Docked = (null == AusScnapscusAuswertungZuusctand) ? null : AusScnapscusAuswertungZuusctand.Docked();

				var AusScnapscusUtilmenuMissionLezte = AutomaatZuusctand.UtilmenuMissionLezte;

				var ListeMesungObjectiveZuusctandZuZaitLezteNulbar =
					ListeMesungObjectiveZuusctandZuZaitLezteBerecne();

				var ListeMesungObjectiveZuusctandZuZaitLezte =
					ListeMesungObjectiveZuusctandZuZaitLezteNulbar.HasValue ?
					ListeMesungObjectiveZuusctandZuZaitLezteNulbar.Value :
					default(SictWertMitZait<VonSensor.WindowAgentMissionInfo>);

				var MissionObjective = (null == ListeMesungObjectiveZuusctandZuZaitLezte.Wert) ? null : ListeMesungObjectiveZuusctandZuZaitLezte.Wert.Objective;

				var MissionMengeObjective = (null == MissionObjective) ? null : MissionObjective.MengeObjectiveTransitiveHüleBerecne();

				if (TailFürNuzer.EndeZaitMili.HasValue)
				{
					return;
				}

				var MissionMengeObjectiveLocation =
					(null == MissionMengeObjective) ? null :
					MissionMengeObjective
					.Where((Kandidaat) => null != Kandidaat.Location)
					.ToArray();

				var MissionMengeObjectiveItem =
					(null == MissionMengeObjective) ? null :
					MissionMengeObjective.Where((Kandidaat) => null != Kandidaat.ItemName)
					.ToArray();

				var MissionMengeObjectiveItemNictComplete =
					(null == MissionMengeObjectiveItem) ? null :
					MissionMengeObjectiveItem.Where((Kandidaat) => !(true	== Kandidaat.Complete))
					.ToArray();

				var MissionMengeObjectiveLocationOoneDropOff =
					MissionMengeObjectiveLocation
					.Where((Kandidaat) => !(SictMissionObjectiveObjectiveElementTyp.LocationDropOff == Kandidaat.Typ))
					.ToArray();

				if (null != Gbs)
				{
					var AusButtonListSurroundingsMenuLezteMitBeginZait = Gbs.AusButtonListSurroundingsMenuLezteMitBeginZait;

					var	GbsListeMenu	= Gbs.ListeMenuNocOfeBerecne();

					if (null != AusButtonListSurroundingsMenuLezteMitBeginZait.Wert	&&
						1 < ExtractFromOldAssembly.Bib3.Extension.CountNullable(GbsListeMenu))
					{
						if (GbsListeMenu.FirstOrDefault() == AusButtonListSurroundingsMenuLezteMitBeginZait.Wert)
						{
							var AusButtonListSurroundingsMenuLezteScnapscus = AusButtonListSurroundingsMenuLezteMitBeginZait.Wert.AingangScnapscusTailObjektIdentLezteBerecne();

							var ButtonListSurroundingsMenuEntryMission = AusMenuEntryRepräsentiirendMission(TailFürNuzer, AusButtonListSurroundingsMenuLezteScnapscus);

							if (null != ButtonListSurroundingsMenuEntryMission)
							{
								if (true == ButtonListSurroundingsMenuEntryMission.Highlight)
								{
									var KaskaadeMenuAbMission = GbsListeMenu.Skip(1);

									AingangListSurroundingsButtonMenu(KaskaadeMenuAbMission, CurrentLocation);
								}
							}
						}
					}
				}

				if (null != InfoPanelListeMissionButton)
				{
					foreach (var KandidaatMissionButtonUtilmenu in InfoPanelListeMissionButton)
					{
						if (null == KandidaatMissionButtonUtilmenu)
						{
							continue;
						}

						var KandidaatKnopf = KandidaatMissionButtonUtilmenu;

						if (null == KandidaatKnopf)
						{
							continue;
						}

						var KandidaatKnopfBescriftung = KandidaatKnopf.Bescriftung;

						if (null == KandidaatKnopfBescriftung)
						{
							continue;
						}

						//	2014.02.29	Beobactung: KandidaatKnopf.Bescriftung enthalt an ende Leerzaice

						if (string.Equals(MissionTitel.Trim(), KandidaatKnopfBescriftung.Trim()))
						{
							ButtonUtilmenu = KandidaatMissionButtonUtilmenu;
							break;
						}
					}
				}

				if (AusScnapscusUtilmenuMissionLezte.HasValue)
				{
					if (UtilmenuPastZuMission(AusScnapscusUtilmenuMissionLezte.Value.Wert))
					{
						UtilmenuZuZait = AusScnapscusUtilmenuMissionLezte;
					}
				}

				/*
				 * 2015.02.18
				 * 
				ZiilLocationNääxteObjective =
					(null == MissionMengeObjectiveLocationOoneDropOff) ? null :
					MissionMengeObjectiveLocationOoneDropOff.FirstOrDefault((Kandidaat) => !(true == Kandidaat.Complete));
				 * */
				ZiilLocationNääxteObjective =
					MissionMengeObjectiveLocationOoneDropOff
					.OfTypeNullable<VonSensor.WindowAgentMissionObjectiveObjective>()
					.FirstOrDefaultNullable((Kandidaat) => !(true == Kandidaat.Complete));

				AnnaameCompleteFallsInAgentStation =
					MissionMengeObjective
					.WhereNullable((Objective) => !(SictMissionObjectiveObjectiveElementTyp.LocationDropOff == Objective.Typ))
					.AllNullable((Objective) => Objective.Complete	?? false);

				if (null != MissionObjective)
				{
					if (MissionObjective.Complete ?? false)
					{
						AnnaameCompleteFallsInAgentStation = true;
					}
				}

				if (null == ZiilLocationNääxteObjective)
				{
					if (null != MissionMengeObjectiveItemNictComplete	&&
						null != MissionMengeObjectiveLocationOoneDropOff)
					{
						if (0 < MissionMengeObjectiveItemNictComplete.Length)
						{
							//	Nict ale Item Objective sind Complete, in diisem Fal werd davon ausgegange das Item in aine Location welce nit Drop-Off Location isc zu finde sind. auc wen diise Location beraits als Complete markiirt.

							ZiilLocationNääxteObjective =
								MissionMengeObjectiveLocationOoneDropOff
								.OfTypeNullable < VonSensor.WindowAgentMissionObjectiveObjective>()
								.FirstOrDefaultNullable();
						}
					}

					if (null == ZiilLocationNääxteObjective)
					{
						ZiilLocationNääxteObjective =
							MissionMengeObjectiveLocation
							.OfTypeNullable < VonSensor.WindowAgentMissionObjectiveObjective>()
							.FirstOrDefaultNullable((Kandidaat) => !(true == Kandidaat.Complete));
					}
				}

				if (null == ZiilLocationNääxteObjective && null != MissionObjective && null != AgentLocation)
				{
					//	Fals kaine ZiilLocation vorhande, werd angenome das AgentLocation als nääxtes angeflooge werde sol.

					if (true	== MissionObjective.Complete)
					{
						ZiilLocationNääxteObjective =
							new VonSensor.WindowAgentMissionObjectiveObjective(
								false,
								SictMissionObjectiveObjectiveElementTyp.Location,
								AgentLocation);
					}
				}

				var PfaadAktuel = this.PfaadAktuel;

				if(null	!= ZiilLocationNääxteObjective)
				{
					//	Fals zu Ziil Location beraits ain Pfaad vorhande, werd ZiilLocationNääxte zurükgesezt.

					var ZiilLocationNääxte = ZiilLocationNääxteObjective.Location;

					if (null != PfaadAktuel && null != ZiilLocationNääxte)
					{
						var PfaadAktuelLocation = PfaadAktuel.Location;

						if (null != PfaadAktuelLocation)
						{
							if (string.Equals(PfaadAktuelLocation.LocationName, ZiilLocationNääxte.LocationName))
							{
								ZiilLocationNääxteObjective = null;
							}
						}
					}
				}

				if (null != ZiilLocationNääxteObjective)
				{
					if (UtilmenuZuZait.HasValue)
					{
						if (null != UtilmenuZuZait.Value.Wert)
						{
							ZiilLocationNääxteInUtilmenu =
								UtilmenuZuZait.Value.Wert.MengeLocation.FirstOrDefault((Kandidaat) => AusUtilmenuLocationRepräsentiirtObjectiveLocation(ZiilLocationNääxteObjective, Kandidaat));
						}
					}

					ZiilLocationNääxteAuswert = new SictObjectiveLocationAuswert(ZiilLocationNääxteObjective);

					ZiilLocationNääxteAuswert.Berecne(AusScnapscusAuswertungZuusctand);
				}
			}
			finally
			{
				this.ButtonUtilmenu = ButtonUtilmenu;

				//	this.UtilmenuZuZait = UtilmenuZuZait;
				AingangUtilmenu(UtilmenuZuZait);

				this.ZiilLocationNääxteAuswert = ZiilLocationNääxteAuswert;
				this.ZiilLocationNääxteInUtilmenu	= ZiilLocationNääxteInUtilmenu;
				this.AnnaameCompleteFallsInAgentStation = AnnaameCompleteFallsInAgentStation;
			}

			AktualisiireTailPfaad(AutomaatZuusctand, AusScnapscusAuswertungZuusctand);
		}

		static public bool MessageBoxIstMeldungAccGateCantActivate(VonSensor.MessageBox	MessageBox)
		{
			if (null == MessageBox)
			{
				return false;
			}

			var TopCaptionText = MessageBox.TopCaptionText;

			if (null == TopCaptionText)
			{
				return false;
			}

			/*
			 * 2014.00.01	Bsp:
			 * "Can't Activate"
			 * */

			var TopCaptionMatch = Regex.Match(TopCaptionText, "Can.?t Activate", RegexOptions.IgnoreCase);

			if (!TopCaptionMatch.Success)
			{
				return false;
			}

			return true;
		}

		public void AktualisiireTailPfaad(
			SictAutomatZuusctand AutomaatZuusctand,
			VonSensorikMesung AusScnapscusAuswertungZuusctand)
		{
			var TailFürNuzer = this.TailFürNuzer;

			if (null == TailFürNuzer)
			{
				return;
			}

			SictVonMissionAnforderungInRaum AnforderungInRaum = null;
			List<Optimat.EveOnline.SictMissionLocationPfaad> ListeLocationPfaad = null;
			SictMissionLocationPfaadZuusctand PfaadAktuel = null;
			SictMissionLocationRaumZuusctand RaumAktuel = null;
			Int64? AusPfaadMessungMissionObjectiveSolNaacZaitMili = null;

			try
			{
				if (null == AutomaatZuusctand)
				{
					return;
				}

				var ZaitMiliNulbar = this.ZaitMili;

				if (!ZaitMiliNulbar.HasValue)
				{
					return;
				}

				var ZaitMili = ZaitMiliNulbar.Value;

				ListeLocationPfaad = TailFürNuzer.ListeLocationPfaad;

				/*
				 * 2014.05.14
				 * 
				TailFürNuzer.ListeLocationPfaad =
					(null == ListePfaad) ? null :
					ListePfaad.Select((Pfaad) => Pfaad.TailFürNuzerBerecne())
					.ToArray();
				 * */

				var FittingUndShipZuusctand = (null == AutomaatZuusctand) ? null : AutomaatZuusctand.FittingUndShipZuusctand;
				var OverviewUndTarget = (null == AutomaatZuusctand) ? null : AutomaatZuusctand.OverviewUndTarget;

				var ShipZuusctandLezte = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.ShipZuusctand;

				var SelbsctShipDocked = (null == ShipZuusctandLezte) ? null : (bool?)ShipZuusctandLezte.Docked;
				var SelbsctShipWarping = (null == ShipZuusctandLezte) ? null : ShipZuusctandLezte.Warping;
				var SelbsctShipWarpScrambled = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.SelbsctShipWarpScrambled;

				var	AusShipUIIndicationAktiivMitZait	= (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.AusShipUIIndicationAktiivMitZait;

				var SelbsctShipWarpingLezteZaitMili = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.WarpingLezteZaitMili;
				var SelbsctShipJumpingLezteZaitMili = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.JumpingLezteZaitMili;

				var MengeOverViewObjekt = (null == OverviewUndTarget) ? null : OverviewUndTarget.MengeOverViewObjekt;

				var tDebugFilterRat = SictStrategikonOverviewObjektFilter.FilterRat();

				var tDebugOverviewMengeRat = MengeOverViewObjekt?.Where(k => tDebugFilterRat.Pasend(k))?.ToArray();

				var FluctLezte = AutomaatZuusctand.FluctLezte;

				var Strategikon = this.Strategikon;

				var MengeMessageBox = (null == AusScnapscusAuswertungZuusctand) ? null : AusScnapscusAuswertungZuusctand.MengeMessageBox();

				var MengeMessageBoxAccGateLocked =
					(null == MengeMessageBox) ? null :
					MengeMessageBox.Where((MessageBox) => MessageBoxIstMeldungAccGateCantActivate(MessageBox)).ToArray();

				/*
				 * 2014.04.16
				 * 
				var ListeAusObjektMitNameCargoItemÜbernomeZuZait = AutomaatZuusctand.ListeAusObjektMitNameCargoItemÜbernomeZuZait;
				 * */
				var ListeInventoryItemTransportMitZait = AutomaatZuusctand.ListeInventoryItemTransportMitZait;

				Int64? ListeInventoryItemTransportLezteZaitMili = null;

				if (null != ListeInventoryItemTransportMitZait)
				{
					var ListeAusObjektMitNameCargoItemÜbernomeZuZaitLezte = ListeInventoryItemTransportMitZait.LastOrDefault();

					if (null != ListeAusObjektMitNameCargoItemÜbernomeZuZaitLezte.Wert)
					{
						ListeInventoryItemTransportLezteZaitMili = ListeAusObjektMitNameCargoItemÜbernomeZuZaitLezte.Zait;
					}
				}

				var ListeMesungObjectiveZuusctandZuZaitLezte = default(SictWertMitZait<VonSensor.WindowAgentMissionInfo>);

				var ListeMesungObjectiveZuusctandZuZaitLezteNulbar = ListeMesungObjectiveZuusctandZuZaitLezteBerecne();

				if (ListeMesungObjectiveZuusctandZuZaitLezteNulbar.HasValue)
				{
					ListeMesungObjectiveZuusctandZuZaitLezte = ListeMesungObjectiveZuusctandZuZaitLezteNulbar.Value;
				}

				var ListeMesungObjectiveZuusctandLezteZaitMili =
					(null == ListeMesungObjectiveZuusctandZuZaitLezte.Wert) ? (Int64?)null : ListeMesungObjectiveZuusctandZuZaitLezte.Zait;

				var ListeMesungObjectiveZuusctandLezteAlterMili = (ZaitMili - ListeMesungObjectiveZuusctandLezteZaitMili);

				var ListeMesungObjectiveZuusctandLezteAlter = ListeMesungObjectiveZuusctandLezteAlterMili / 1000;

				var StrategikonRaumMengeZuBezaicnerAtom = (null == Strategikon) ? null : Strategikon.RaumMengeZuBezaicnerAtom;

				var ListeMesungObjectiveZuusctandLezteObjective =
					(null == ListeMesungObjectiveZuusctandZuZaitLezte.Wert) ? null :
					ListeMesungObjectiveZuusctandZuZaitLezte.Wert.Objective;

				var ListeMesungObjectiveZuusctandLezteObjectiveComplete =
					(null == ListeMesungObjectiveZuusctandLezteObjective) ? null :
					ListeMesungObjectiveZuusctandLezteObjective.Complete;

				var ListeMesungObjectiveZuusctandLezteObjectiveMengeAst =
					(null == ListeMesungObjectiveZuusctandLezteObjective) ? null :
					ListeMesungObjectiveZuusctandLezteObjective.MengeObjectiveTransitiveHüleBerecne();

				var ListeMesungObjectiveZuusctandLezteObjectiveMengeAstItem =
					(null == ListeMesungObjectiveZuusctandLezteObjectiveMengeAst) ? null :
					ListeMesungObjectiveZuusctandLezteObjectiveMengeAst
					.Where((Kandidaat) => null != Kandidaat.ItemName).ToArray();

				var ListeMesungObjectiveZuusctandLezteObjectiveMengeAstItemNictComplete =
					(null == ListeMesungObjectiveZuusctandLezteObjectiveMengeAstItem) ? null :
					ListeMesungObjectiveZuusctandLezteObjectiveMengeAstItem.Where((Objective) => !(true == Objective.Complete)).ToArray();

				var MengeZuWindowInventoryAuswaalReczInRaumObjekt =
					(null == OverviewUndTarget) ? null :
					OverviewUndTarget.MengeZuWindowInventoryAuswaalReczMengeKandidaatInRaumObjekt;

				var StrategikonMengeOverviewObjektGrupeVerwendet =
					(null == Strategikon) ? null :
					Strategikon.MengeOverviewObjektGrupeVerwendetBerecne();

				if (null != ListeLocationPfaad)
				{
					var ListePfaadLezte = ListeLocationPfaad.LastOrDefault()	as	SictMissionLocationPfaadZuusctand;

					if (null != ListePfaadLezte)
					{
						if (!ListePfaadLezte.EndeZaitMili.HasValue)
						{
							PfaadAktuel = ListePfaadLezte;
						}
					}
				}

				if (null != PfaadAktuel)
				{
					var PfaadAktuelListeRaum = PfaadAktuel.ListeRaum;

					if (null != PfaadAktuelListeRaum)
					{
						var PfaadAktuelRaumLetze = PfaadAktuelListeRaum.LastOrDefault();

						if (null != PfaadAktuelRaumLetze)
						{
							if (!PfaadAktuelRaumLetze.EndeZait.HasValue)
							{
								RaumAktuel = PfaadAktuelRaumLetze	as	SictMissionLocationRaumZuusctand;
							}
						}
					}
				}

				var ZiilLocationNääxteAuswert = this.ZiilLocationNääxteAuswert;

				var ZiilLocationNääxteObjective = (null == ZiilLocationNääxteAuswert) ? null : ZiilLocationNääxteAuswert.Objective;

				var ZiilLocationNääxte = (null == ZiilLocationNääxteObjective) ? null : ZiilLocationNääxteObjective.Location;

				var ZiilLocationNääxteInUtilmenu = this.ZiilLocationNääxteInUtilmenu;

				var AnforderungInRaumMengeOverviewObjektGrupeMesungZuErsctele = new List<SictOverviewObjektGrupeEnum>();
				var AnforderungInRaumMengeOverviewObjektGrupeVerwendet = new List<SictOverviewObjektGrupeEnum>();

				var AnforderungInRaumMengeObjektZuBearbaiteMitPrio = new List<SictAufgaabeInRaumObjektZuBearbaiteMitPrio>();
				VonSensor.InventoryItem AusInventoryItemZuÜbertraageNaacActiveShip = null;

				SictAufgaabeInRaumObjektZuBearbaiteMitPrio AnforderungInRaumAccGateZuAktiviireMitPrio = null;

				var ScnapscusCurrentLocationInfo = (null == AusScnapscusAuswertungZuusctand) ? null : AusScnapscusAuswertungZuusctand.CurrentLocationInfo();

				var ScnapscusCurrentLocationInfoSolarSystemName = (null == ScnapscusCurrentLocationInfo) ? null : ScnapscusCurrentLocationInfo.SolarSystemName.TrimNullable();

				try
				{
					if (null != PfaadAktuel)
					{
						if (null != StrategikonMengeOverviewObjektGrupeVerwendet)
						{
							AnforderungInRaumMengeOverviewObjektGrupeVerwendet.AddRange(StrategikonMengeOverviewObjektGrupeVerwendet);
						}
					}

					if (null != RaumAktuel)
					{
						RaumAktuel.Aktualisiire(ZaitMili, AusShipUIIndicationAktiivMitZait, MengeOverViewObjekt);
					}

					if (true == SelbsctShipWarping)
					{
						//	Ship isc noc am Warpe

						if (null != RaumAktuel)
						{
							//	Raum Ende

							if (!RaumAktuel.KandidaatEnde.HasValue)
							{
								RaumAktuel.KandidaatEndeSeze(new SictWertMitZait<SictMissionRaumZuusctandEndeSictServer>(ZaitMili,
									SictMissionRaumZuusctandEndeSictServer.Berecne(AutomaatZuusctand)));
							}
						}

						return;
					}

					if (null != RaumAktuel)
					{
						RaumAktuel.KandidaatEndeSeze(null);
					}

					if (null == RaumAktuel &&
						null != StrategikonRaumMengeZuBezaicnerAtom &&
						null != ZiilLocationNääxteAuswert)
					{
						if (false == ZiilLocationNääxteAuswert.LocationErfordertDock)
						{
							//	Pfaad Begin

							var PfaadBegin = false;

							if (null != ZiilLocationNääxteInUtilmenu)
							{
								var KnopfApproach = ZiilLocationNääxteInUtilmenu.KnopfApproach;

								if (null != KnopfApproach)
								{
									//	Wen KnopfApproach für Ziil Location sictbar, dan begint noier Pfaad für diise Location.

									PfaadBegin = true;
								}
							}

							var Location = ZuObjectiveLocationLocationInfoAggr(ZiilLocationNääxteAuswert, true);

							if (null != Location)
							{
								var LocationSurroundingsMenuLezte = Location.SurroundingsMenuLezteScnapscusZuZaitBerecne();

								if (SelbsctShipWarpingLezteZaitMili < LocationSurroundingsMenuLezte.Zait &&
									null != LocationSurroundingsMenuLezte.Wert)
								{
									//	Mesung isc jünger als lezte Warp

									var ListeEntry = LocationSurroundingsMenuLezte.Wert.ListeEntry;

									if (null != ListeEntry)
									{
										if (ListeEntry.Any((Entry) => Regex.Match(Entry.Bescriftung ?? "", "Approach Location", RegexOptions.IgnoreCase).Success))
										{
											//	Menu Entry Approach isc für Ziil Location sictbar, dan begint noier Pfaad für diise Location.
											PfaadBegin = true;
										}
									}
								}
							}

							if (PfaadBegin)
							{
								RaumAktuel = new SictMissionLocationRaumZuusctand(ZaitMili, ScnapscusCurrentLocationInfo);

								PfaadAktuel = new SictMissionLocationPfaadZuusctand(ZiilLocationNääxteObjective, RaumAktuel);
							}
						}
					}

					if (null == PfaadAktuel)
					{
						return;
					}

					var PfaadAktuelLocation = PfaadAktuel.Location;

					if (null != ListeMesungObjectiveZuusctandZuZaitLezte.Wert && null != PfaadAktuelLocation)
					{
						var Objective = ListeMesungObjectiveZuusctandZuZaitLezte.Wert.Objective;

						if (null != Objective)
						{
							PfaadAktuel.MesungObjectiveAingang(new SictWertMitZait<VonSensor.WindowAgentMissionObjectiveObjective>(
								ListeMesungObjectiveZuusctandZuZaitLezte.Zait, Objective));
						}
					}

					if (null == RaumAktuel)
					{
						{
							//	Pfaad Fortsaz

							var PfaadAktuelRaumLezte = PfaadAktuel.ListeRaum.LastOrDefault()	as	SictMissionLocationRaumZuusctand;

							if (null != PfaadAktuelRaumLezte)
							{
								var PfaadAktuelRaumLezteErfolg = PfaadAktuelRaumLezte.ErfolgBerecne();

								var PfaadAktuelRaumLezteEndeNulbar = PfaadAktuelRaumLezte.Ende;

								if (PfaadAktuelRaumLezteEndeNulbar.HasValue)
								{
									var PfaadAktuelRaumLezteEnde = PfaadAktuelRaumLezteEndeNulbar.Value;

									var PfaadAktuelRaumLezteEndeScpez = PfaadAktuelRaumLezteEnde.Wert as SictMissionRaumZuusctandEndeSictServer;

									if (null != PfaadAktuelRaumLezteEndeScpez)
									{
										var PfaadAktuelRaumLezteEndeAlterMili = ZaitMili - PfaadAktuelRaumLezteEnde.Zait;

										var BedingungJumpErfült = !(PfaadAktuel.BeginZait < SelbsctShipJumpingLezteZaitMili);

										var BedingungAccGateErfült = PfaadAktuelRaumLezteEndeScpez.AnnaameFortsazInMissionRaumNääxteErfolg(3333);

										/*
										 * !!!!
										 * Vorersct mit Bedingung das in lezte Raum AccGate in nähe, diise mus mööglicerwaise geändert were wen auc OverviewTypeSelection genuzt were welce AccGate nit zaigen.
										 * */

										if (true == PfaadAktuelRaumLezteErfolg &&
											/*
											 * 2014.02.28
											 * 
											 * Auf Fluct Lezte Zait werd hiir vorersct kai Rüksict meer gnome.
											 *
											 * 2014.06.12
											 * Pfaad wiider unterbrece wen Fluct darinliigt.
											 * */
											FluctLezte.Zait < PfaadAktuel.BeginZait &&
											PfaadAktuelRaumLezteEndeAlterMili < 1000 * 60 * 3 &&
											BedingungJumpErfült &&
											BedingungAccGateErfült)
										{
											RaumAktuel = new SictMissionLocationRaumZuusctand(ZaitMili, ScnapscusCurrentLocationInfo);
										}
									}
								}
							}
						}
					}

					if (true == SelbsctShipDocked)
					{
						//	Pfaad werd durch Dock unbedingt unterbroce.
						RaumAktuel = null;
					}

					var	PfaadAktuelBeginSolarSystemUnPasendZuCurrentLocation	=
						0	< ScnapscusCurrentLocationInfoSolarSystemName.CountNullable()	&&
						!string.Equals(
						PfaadAktuel.BeginRaumBeginLocationSolarSystemName.TrimNullable(),
						ScnapscusCurrentLocationInfoSolarSystemName,
						StringComparison.InvariantCultureIgnoreCase);

					if (null == RaumAktuel	||
						(SelbsctShipDocked	?? false)	||
						PfaadAktuelBeginSolarSystemUnPasendZuCurrentLocation)
					{
						PfaadAktuel.EndeZaitMili = ZaitMili;

						return;
					}

					if (null == OverviewUndTarget)
					{
						return;
					}

					if (null == MengeOverViewObjekt)
					{
						return;
					}

					var MengeInRaumObjektAccGate =
						MengeOverViewObjekt.Where(PrädikaatIstAccGate).ToArray();

					var MengeInRaumObjektAccGateNääxte =
						MengeInRaumObjektAccGate.OrderBy((InRaumObjekt) => InRaumObjekt.SictungLezteDistanceScrankeMaxScpezOverview ?? Int64.MaxValue).FirstOrDefault();

					RaumAktuel.AktualisiireTailStrategikonInstanz(ZaitMili);

					var RaumAktuelStrategikonInstanzLezte = RaumAktuel.StrategikonInstanzLezte();

					var RaumAktuelErfolgFrühesteZait =
						(null == RaumAktuelStrategikonInstanzLezte) ? null : RaumAktuelStrategikonInstanzLezte.ErfolgFrühesteZaitBerecne();

					var RaumAktuelBeginZaitMili = (null == RaumAktuel) ? null : RaumAktuel.BeginZait;

					var RaumAktuelAlterMili = ZaitMili - RaumAktuelBeginZaitMili;

					var RaumAktuelAlter = RaumAktuelAlterMili / 1000;

					var InRaumFortsazMengeAtomTrozAusPfaadAnnaameErfolg = false;

					if (null != MengeMessageBoxAccGateLocked &&
						null != MengeInRaumObjektAccGateNääxte)
					{
						if (0 < MengeMessageBoxAccGateLocked.Length)
						{
							RaumAktuel.MeldungAccGateLockedAingang(ZaitMili);
						}
					}

					{
						//	Pfaad Fertigsctele

						Int64? PfaadAnnaameErfolgZaitMili = null;

						if (null != ListeMesungObjectiveZuusctandLezteObjective)
						{
							if (true == ListeMesungObjectiveZuusctandLezteObjective.Complete)
							{
								PfaadAnnaameErfolgZaitMili = ListeMesungObjectiveZuusctandZuZaitLezte.Zait;
							}
						}

						if (RaumAktuelErfolgFrühesteZait.HasValue && !PfaadAnnaameErfolgZaitMili.HasValue)
						{
							/*
							 * Im Moment isc Aktiviirung AccGate als Bedingung für Erfolg für Pfaad in Program fesctgescriibe.
							 * Diis werd für ainige Misioone nit pase, daher mus diise Bedingung noc naac Strategikon ausgelaager so das
							 * RaumAktuelErfolg von AccGate abhängig.
							 * */
							if (null == MengeInRaumObjektAccGateNääxte)
							{
								var OverviewObjektGrupeAccGate = SictOverviewObjektGrupeEnum.AccelerationGate;

								var AccGateAussclussScpätesteZait = OverviewUndTarget.ZuOverviewObjektGrupeFolgeVolsctändigBeginZaitFrüheste(OverviewObjektGrupeAccGate);

								if (RaumAktuelErfolgFrühesteZait < AccGateAussclussScpätesteZait)
								{
									//	lezte Suuce naac AccGate wurde scpääter begone als Raum Erfolg Zait, daher Annaame kain AccGate vorhande.

									AusPfaadMessungMissionObjectiveSolNaacZaitMili = RaumAktuelErfolgFrühesteZait;

									if (AusPfaadMessungMissionObjectiveSolNaacZaitMili < ListeMesungObjectiveZuusctandZuZaitLezte.Zait)
									{
										/*
										* 2014.00.29
										* Umgehung beobactetes Probleem in Mission "Intercept The Pirate Smugglers":
										* Raum werd als erfolgraic abgesclose angenome und es sind (wiider) Rat in Raum welce noc zersctöört were solen.
										* Di Startegikon Atome solen jedoc nur dan waiter bearbaitet werde wen sait in Raum Annaame Erfolg beraits
										* MissionObjective noi gemese wurde.
										* */
										InRaumFortsazMengeAtomTrozAusPfaadAnnaameErfolg = true;

										if (120 < ListeMesungObjectiveZuusctandLezteAlter)
										{
											var MessungMissionObjectivePrio = new SictInRaumObjektBearbaitungPrio(null, null, null, -44);

											AnforderungInRaumMengeObjektZuBearbaiteMitPrio.Add(
												new SictAufgaabeInRaumObjektZuBearbaiteMitPrio(
													AufgaabeParamAndere.AufgaabeMissionObjectiveMese(this),
													MessungMissionObjectivePrio));
										}

										if (ListeMesungObjectiveZuusctandLezteObjectiveMengeAstItemNictComplete.IsNullOrEmpty())
											PfaadAnnaameErfolgZaitMili = ListeMesungObjectiveZuusctandZuZaitLezte.Zait;
										else
										{
											if (ListeMesungObjectiveZuusctandLezteZaitMili < ListeInventoryItemTransportLezteZaitMili)
											{
												//	Sait lezte Mesung Mission Objective wurde ain Item im Inventory verscoobe.
												//	noie Mesung Mission Objective veranlase.

												if (30 < ListeMesungObjectiveZuusctandLezteAlter)
												{
													var MessungMissionObjectivePrio = new SictInRaumObjektBearbaitungPrio(null, null, null, 44);

													AnforderungInRaumMengeObjektZuBearbaiteMitPrio.Add(
														new SictAufgaabeInRaumObjektZuBearbaiteMitPrio(
															AufgaabeParamAndere.AufgaabeMissionObjectiveMese(this),
															MessungMissionObjectivePrio));
												}
											}


											//	Da Mission Objective mit Item welce noc nit als Complete markiirt sind vorhande sind werden ale Container und Wreck in Raum durcsuuct.

											if (null != MengeOverViewObjekt)
											{
												var FilterCargoContainer = SictOverviewObjektFilterTypeUndName.FilterCargoContainer();
												var FilterWreck = SictOverviewObjektFilterTypeUndName.FilterWreckMitNameRegex();

												var MengeInRaumObjektCargoContainer =
													MengeOverViewObjekt.Where((InRaumObjekt) => FilterCargoContainer.Pasend(InRaumObjekt)).ToArray();

												//	!!!!	Noc zu erseze:	Wen Texture Ident für Wreck bekant, diise für Filter verwende.
												var MengeInRaumObjektWreck =
													MengeOverViewObjekt.Where((InRaumObjekt) => FilterWreck.Pasend(InRaumObjekt)).ToArray();

												var MengeInRaumObjektZuDurcsuuce =
													MengeInRaumObjektCargoContainer
													.Concat(MengeInRaumObjektWreck)
													.Where((InRaumObjekt) => SictMissionZuusctand.FilterCargoZuDurcsuuce(InRaumObjekt))
													.ToArray();

												//	Prio so wääle das zuusäzlice Aufgaabe zum durcsuuce von Cargo naacrangig naac uursprünglic aus Mission Strategikon abgelaitete Aufgaabe ausgefüürt werd.
												var AufgaabeZuusazDurcsuucePrio = new SictInRaumObjektBearbaitungPrio(null, null, null, -99);

												AnforderungInRaumMengeObjektZuBearbaiteMitPrio.AddRange(
													MengeInRaumObjektZuDurcsuuce
													.Select((InRaumObjektZuDurcsuuce) =>
														new SictAufgaabeInRaumObjektZuBearbaiteMitPrio(
															AufgaabeParamAndere.AufgaabeAktioonCargoDurcsuuce(InRaumObjektZuDurcsuuce),
															AufgaabeZuusazDurcsuucePrio)));

												/*
												 * 2013.11.27
												 * !!!!	Noc zu erseze:	in Fal das Item in Objective vorhande sol fals diise noc nict erfült ale Container und Wreck durcsuuct werde.
												 * 
												 * */
											}
										}
									}
									else
									{
										//	Naac aus Raum Annaame Erfolg isc Mesung von Objective fälig.

										AnforderungInRaumMengeObjektZuBearbaiteMitPrio.Add(
											new SictAufgaabeInRaumObjektZuBearbaiteMitPrio(
												AufgaabeParamAndere.AufgaabeMissionObjectiveMese(this)));
									}
								}
								else
								{
									AnforderungInRaumMengeOverviewObjektGrupeVerwendet.Add(OverviewObjektGrupeAccGate);
									AnforderungInRaumMengeOverviewObjektGrupeMesungZuErsctele.Add(OverviewObjektGrupeAccGate);
								}
							}
							else
							{
								AnforderungInRaumAccGateZuAktiviireMitPrio =
									new SictAufgaabeInRaumObjektZuBearbaiteMitPrio(
										AufgaabeParamAndere.AufgaabeAktioonInRaumObjektActivate(MengeInRaumObjektAccGateNääxte));

								AnforderungInRaumMengeObjektZuBearbaiteMitPrio.Add(AnforderungInRaumAccGateZuAktiviireMitPrio);
							}
						}

						if (PfaadAnnaameErfolgZaitMili.HasValue)
						{
							PfaadAktuel.AnnaameErfolgZaitMili = PfaadAnnaameErfolgZaitMili;
						}
					}

					SictMissionStrategikonInstanz.StrategikonInRaumFortscritBerecne(AutomaatZuusctand, Strategikon, RaumAktuelStrategikonInstanzLezte);

					var RaumAktuelZuStrategikonAtomZwisceergeebnis = RaumAktuelStrategikonInstanzLezte.ZuStrategikonAtomZwisceergeebnis;

					var RaumAktuelMeldungAccGateLockedLezteZait = RaumAktuel.MeldungAccGateLockedLezteZait;

					var RaumAktuelMeldungAccGateLockedLezteAlterMili = ZaitMili - RaumAktuelMeldungAccGateLockedLezteZait;

					if (RaumAktuelMeldungAccGateLockedLezteAlterMili.HasValue &&
						null != AnforderungInRaumAccGateZuAktiviireMitPrio)
					{
						//	Vermuutung Acc-Gate ist Locked, ale Rat zersctööre.

						var FilterRat = SictStrategikonOverviewObjektFilter.FilterRat();

						var MengeInRaumObjektRat =
							MengeOverViewObjekt.Where((InRaumObjekt) => FilterRat.Pasend(InRaumObjekt)).ToArray();

						var RatDestruktPrio = AnforderungInRaumAccGateZuAktiviireMitPrio.Prioritäät;

						if (RaumAktuelMeldungAccGateLockedLezteAlterMili < 1000 * 60 * 4 &&
							0 < MengeInRaumObjektRat.Length)
						{
							//	Di lezte Meldung "Can't activate" isc noc nit lange her

							/*
							 * 2014.01.15
							 * 
							//	Aufgaabe Activate entferne um Gefect nit zu behindere.
							AnforderungInRaumMengeObjektZuBearbaiteMitPrio.Remove(AnforderungInRaumAccGateZuAktiviireMitPrio);
							 * */

							if (RaumAktuelMeldungAccGateLockedLezteAlterMili < 1000 * 60 * 2)
							{
								//	Aufgaabe Activate entferne um Gefect nit zu behindere.
								AnforderungInRaumMengeObjektZuBearbaiteMitPrio.Remove(AnforderungInRaumAccGateZuAktiviireMitPrio);
							}

							//	Destruktion der Rat erhält hörere Prio als Activate
							RatDestruktPrio = RatDestruktPrio.PrioritäätVersezt(1);
						}

						AnforderungInRaumMengeObjektZuBearbaiteMitPrio.AddRange(
							MengeInRaumObjektRat
							.Select((InRaumObjektRat) => new SictAufgaabeInRaumObjektZuBearbaiteMitPrio(
								new	AufgaabeParamDestrukt(InRaumObjektRat), RatDestruktPrio)));
					}

					if (!RaumAktuelErfolgFrühesteZait.HasValue ||
						InRaumFortsazMengeAtomTrozAusPfaadAnnaameErfolg)
					{
						if (null != RaumAktuelZuStrategikonAtomZwisceergeebnis && null != StrategikonRaumMengeZuBezaicnerAtom)
						{
							//	Naac Automaat Anforderung berecne. Diise werden aus deen in Arbait befindlice Startegikon Atome berecnet.

							foreach (var ZuAtomBezaicnerZwisceergeebnis in RaumAktuelZuStrategikonAtomZwisceergeebnis)
							{
								var AtomBezaicner = ZuAtomBezaicnerZwisceergeebnis.Key;

								if (null == ZuAtomBezaicnerZwisceergeebnis.Value)
								{
									continue;
								}

								var RaumMengeZuBezaicnerAtom = Strategikon.RaumMengeZuBezaicnerAtom;

								if (null == RaumMengeZuBezaicnerAtom)
								{
									continue;
								}

								var AtomSctruktuur =
									ExtractFromOldAssembly.Bib3.Extension.FirstOrDefaultNullable(
									RaumMengeZuBezaicnerAtom,
									(Kandidaat) => Kandidaat.Key == AtomBezaicner)
									.Value;

								if (null == AtomSctruktuur)
								{
									continue;
								}

								var Atom = AtomSctruktuur.Atom;

								var AtomMengeAufgaabeObjektZuBearbaite = ZuAtomBezaicnerZwisceergeebnis.Value.MengeAufgaabeObjektZuBearbaite;

								var AtomMengeAufgaabeObjektZuBearbaiteMitPrio =
									(null == AtomMengeAufgaabeObjektZuBearbaite) ? null :
									AtomMengeAufgaabeObjektZuBearbaite
									.Select((AufgaabeObjektZuBearbaite) => new SictAufgaabeInRaumObjektZuBearbaiteMitPrio(
										AufgaabeObjektZuBearbaite,
										Atom.Prioritäät)).ToArray();

								if (null != AtomMengeAufgaabeObjektZuBearbaiteMitPrio)
								{
									AnforderungInRaumMengeObjektZuBearbaiteMitPrio.AddRange(AtomMengeAufgaabeObjektZuBearbaiteMitPrio);
								}

								var AtomZwisceergeebnisMengeOverviewObjektGrupeMesungZuErsctele =
									ZuAtomBezaicnerZwisceergeebnis.Value.MengeOverviewObjektGrupeMesungZuErsctele;

								if (null != AtomZwisceergeebnisMengeOverviewObjektGrupeMesungZuErsctele)
								{
									AnforderungInRaumMengeOverviewObjektGrupeMesungZuErsctele.AddRange(
										AtomZwisceergeebnisMengeOverviewObjektGrupeMesungZuErsctele);
								}
							}
						}
					}

					var MengeZuInRaumObjektWindowInventoryCargoZuDurcsuuce =
						(null == MengeZuWindowInventoryAuswaalReczInRaumObjekt) ? null :
						MengeZuWindowInventoryAuswaalReczInRaumObjekt
						.Where((Kandidaat) => AnforderungInRaumMengeObjektZuBearbaiteMitPrio.Any((AufgaabeObjektZuBearbaiteMitPrio) =>
							{
								var AufgaabeObjektZuBearbaiteScpez = AufgaabeObjektZuBearbaiteMitPrio.AufgaabeObjektZuBearbaite as AufgaabeParamAndere;

								if (null == AufgaabeObjektZuBearbaiteScpez)
								{
									return false;
								}

								if (!true == AufgaabeObjektZuBearbaiteScpez.AktioonCargoDurcsuuce)
								{
									return false;
								}

								var WindowInventoryAuswaalReczMengeKandidaatOverViewObjekt = Kandidaat.Value;

								if (null == WindowInventoryAuswaalReczMengeKandidaatOverViewObjekt)
								{
									return false;
								}

								return WindowInventoryAuswaalReczMengeKandidaatOverViewObjekt.Contains(
									AufgaabeObjektZuBearbaiteMitPrio.AufgaabeObjektZuBearbaite.OverViewObjektZuBearbaiteVirt());
							}))
						.ToArray();

					/*
					 * 2015.02.17
					 * 

							var MengeZuInRaumObjektWindowInventoryCargoZuDurcsuuceVorderste =
								(null == MengeZuInRaumObjektWindowInventoryCargoZuDurcsuuce) ?
								default(KeyValuePair<SictWindowInventoryVerknüpfungMitOverview, SictOverViewObjektZuusctand[]>) :
								MengeZuInRaumObjektWindowInventoryCargoZuDurcsuuce.OrderByDescending((Kandidaat) => Kandidaat.Key.WindowInventory.InGbsParentChildIndex ?? -1)
								.FirstOrDefault();
					 * */

					var MengeZuInRaumObjektWindowInventoryCargoZuDurcsuuceVorderste =
						(null == MengeZuInRaumObjektWindowInventoryCargoZuDurcsuuce) ?
						default(KeyValuePair<SictWindowInventoryVerknüpfungMitOverview, SictOverViewObjektZuusctand[]>) :
						/*
						2015.09.01
						Änderung InGbsBaumAstIndex: Übernaame von noie Sensor InTreeIndex: Element occludes other Elements with lower Value.
							MengeZuInRaumObjektWindowInventoryCargoZuDurcsuuce.OrderBy((Kandidaat) => Kandidaat.Key.WindowInventory.InGbsBaumAstIndex ?? int.MaxValue)
							*/
						MengeZuInRaumObjektWindowInventoryCargoZuDurcsuuce.OrderByDescending(Kandidaat => Kandidaat.Key?.WindowInventory?.InGbsBaumAstIndex ?? int.MaxValue)
						.FirstOrDefault();

							/*
							 * 2014.00.08
							 * 
							 * z.B. in Mission "Unauthorized Military Presence" wen Objective Item "Militants" in Station vorhande und das AgentDialogueWindow dort geöfnet wird,
							 * ist das betrefende Objective in diisem Dialog als Complete gekenzaicnet.
							 * Wen jedoc Selbst Ship in Mission Location isc in der Mission noc di Objective Messung aus der Station noc aktuel so das der Automat das Item nit mitneeme würde.
							 * Daher wird das Verhalten hiir dahingeehend geändert das auc Item welce in vorheriger Mesung des Objective als Complete gekenzaicnet waren mitgenome werde.
							 * 
							if (null != ListeMesungObjectiveZuusctandLezteObjectiveMengeAstItemNictComplete	&&
								null != MengeZuInRaumObjektWindowInventoryCargoZuDurcsuuceVorderste.Key	&&
								null != RaumAktuel)
							 * */
					if (null != ListeMesungObjectiveZuusctandLezteObjectiveMengeAstItem &&
						null != MengeZuInRaumObjektWindowInventoryCargoZuDurcsuuceVorderste.Key &&
						null != RaumAktuel)
					{
						//	Mission Objective Loot aus WindowInventory

						var WindowInventory = MengeZuInRaumObjektWindowInventoryCargoZuDurcsuuceVorderste.Key.WindowInventory;

						var WindowInventoryAuswaalReczMengeKandidaatOverviewObjekt = MengeZuInRaumObjektWindowInventoryCargoZuDurcsuuceVorderste.Value;

						if (null != WindowInventory)
						{
							var InventoryAuswaalReczListeItem = WindowInventory.AuswaalReczListeItem;

							if (null != InventoryAuswaalReczListeItem)
							{
								var InventoryAuswaalReczListeItemZuÜberneeme = InventoryAuswaalReczListeItem.Take(0).ToList();

								foreach (var InventoryItem in InventoryAuswaalReczListeItem)
								{
									var ItemÜberneemeSol = false;

									try
									{
										if (null == InventoryItem)
										{
											continue;
										}

										foreach (var ObjectiveItem in ListeMesungObjectiveZuusctandLezteObjectiveMengeAstItem)
										{
											if (string.Equals(ObjectiveItem.ItemName, InventoryItem.Name, StringComparison.InvariantCultureIgnoreCase))
											{
												ItemÜberneemeSol = true;
												break;
											}
										}
									}
									finally
									{
										if (ItemÜberneemeSol)
										{
											InventoryAuswaalReczListeItemZuÜberneeme.Add(InventoryItem);
										}
									}
								}

								if (0 < InventoryAuswaalReczListeItemZuÜberneeme.Count)
								{
									AusInventoryItemZuÜbertraageNaacActiveShip = InventoryAuswaalReczListeItemZuÜberneeme.FirstOrDefault();
								}
								else
								{
									if (1 == ExtractFromOldAssembly.Bib3.Extension.CountNullable(WindowInventoryAuswaalReczMengeKandidaatOverviewObjekt))
									{
										if (null != RaumAktuelStrategikonInstanzLezte)
										{
											RaumAktuelStrategikonInstanzLezte.MengeObjektCargoDurcsuuctFüügeAin(
												WindowInventoryAuswaalReczMengeKandidaatOverviewObjekt[0]);
										}
									}
								}
							}
						}
					}

					if ((null == AnforderungInRaumAccGateZuAktiviireMitPrio ||
						!AnforderungInRaumMengeObjektZuBearbaiteMitPrio.Contains(AnforderungInRaumAccGateZuAktiviireMitPrio)) &&
						null != MengeInRaumObjektAccGateNääxte)
					{
						//	Strategikon In Raum isc eventuel noc nit fertiggesctelt, um naac desen fertigsctelung Raisezait bis Acc-Gate scpaare diises jezt scon anfliige.

						var AufgaabeAccGateAnfliige = AufgaabeParamAndere.AufgaabeDistanceAinzusctele(MengeInRaumObjektAccGateNääxte, null, 2000);

						AnforderungInRaumMengeObjektZuBearbaiteMitPrio.Add(new SictAufgaabeInRaumObjektZuBearbaiteMitPrio(
							AufgaabeAccGateAnfliige, new SictInRaumObjektBearbaitungPrio(null, null, null, -1)));
					}

					if (60 * 30 < RaumAktuelAlter &&
						60 * 20 < ListeMesungObjectiveZuusctandLezteAlter)
					{
						//	zaitabhängig Mesung Mission Objective durcfüüre

						var MessungMissionObjectivePrio = new SictInRaumObjektBearbaitungPrio(true, true, null, 111);

						AnforderungInRaumMengeObjektZuBearbaiteMitPrio.Add(
							new SictAufgaabeInRaumObjektZuBearbaiteMitPrio(
								AufgaabeParamAndere.AufgaabeMissionObjectiveMese(this),
								MessungMissionObjectivePrio));
					}

					if (60 * 10 < RaumAktuelAlter &&
						60 * 1 < ListeMesungObjectiveZuusctandLezteAlter &&
						((60 * 5 < ListeMesungObjectiveZuusctandLezteAlter) ||
						(ListeMesungObjectiveZuusctandLezteZaitMili < ListeInventoryItemTransportLezteZaitMili)))
					{
						//	zaitabhängig Mesung Mission Objective durcfüüre

						var MessungMissionObjectivePrio = new SictInRaumObjektBearbaitungPrio(null, null, null, -111);

						AnforderungInRaumMengeObjektZuBearbaiteMitPrio.Add(
							new SictAufgaabeInRaumObjektZuBearbaiteMitPrio(
								AufgaabeParamAndere.AufgaabeMissionObjectiveMese(this),
								MessungMissionObjectivePrio));
					}
				}
				finally
				{
					AnforderungInRaum = new SictVonMissionAnforderungInRaum(
						AnforderungInRaumMengeOverviewObjektGrupeVerwendet.Distinct().ToArray(),
						AnforderungInRaumMengeOverviewObjektGrupeMesungZuErsctele.Distinct().ToArray(),
						AnforderungInRaumMengeObjektZuBearbaiteMitPrio.ToArray(),
						AusInventoryItemZuÜbertraageNaacActiveShip);
				}
			}
			finally
			{
				if (null != PfaadAktuel)
				{
					if (null == ListeLocationPfaad)
					{
						ListeLocationPfaad = new List<SictMissionLocationPfaad>();
					}

					if (!(ListeLocationPfaad.LastOrDefault() == PfaadAktuel))
					{
						ListeLocationPfaad.Add(PfaadAktuel);
					}

					if (null != RaumAktuel)
					{
						var PfaadAktuelListeRaum = PfaadAktuel.ListeRaum;

						if (null != PfaadAktuelListeRaum)
						{
							if (!(PfaadAktuelListeRaum.LastOrDefault() == RaumAktuel))
							{
								PfaadAktuelListeRaum.Add(RaumAktuel);
							}
						}
					}
				}

				TailFürNuzer.ListeLocationPfaad = ListeLocationPfaad;

				this.NaacAutomaatAnforderungInRaum = AnforderungInRaum;
				this.PfaadAktuel = PfaadAktuel;
				this.RaumAktuel = RaumAktuel;
				this.AusPfaadMessungMissionObjectiveSolNaacZaitMili = AusPfaadMessungMissionObjectiveSolNaacZaitMili;
			}
		}

		static public	bool	FilterCargoZuDurcsuuce(
			SictOverViewObjektZuusctand	KandidaatCargoZuDursuuce,
			IEnumerable<SictOverViewObjektZuusctand>	RaumMengeObjektCargoDurcsuuct	=	null)
		{
			if(null	== KandidaatCargoZuDursuuce)
			{
				return	false;
			}

			if (!true == KandidaatCargoZuDursuuce.IconMainColorSätigungGering)
			{
				//	z.B. sind Abandoned blau und fremde gelb gefärbt.
				return false;
			}

			if (null != RaumMengeObjektCargoDurcsuuct)
			{
				if (RaumMengeObjektCargoDurcsuuct.Contains(KandidaatCargoZuDursuuce))
				{
					return false;
				}
			}

			return true;
		}

		/*
		 * 2014.00.28
		 * Ersaz durc Mission.Raum
		 * 
		/// <summary>
		/// Berecnet anhand der bisherige Entscaidunge zu Atome welce Atome bearbaitet werde sole.
		/// Berecnet zu Atom MengeObjekt.
		/// Berecnet zu Atom Entscaidung Erfolg.
		/// </summary>
		/// <param name="Strategikon"></param>
		/// <param name="Raum"></param>
		/// <param name="MengeAtomInArbait"></param>
		static public void StrategikonInRaumFortscritBerecne(
			SictAutomatZuusctand	AutomaatZuusctand,
			SictMissionStrategikon	Strategikon,
			SictMissionRaumZuusctand	Raum)
		{
			if (null == Strategikon)
			{
				return;
			}

			if (null == Raum)
			{
				return;
			}

			if (null == AutomaatZuusctand)
			{
				return;
			}

			var ZaitMili	= AutomaatZuusctand.ZaitMili;

			var OverviewUndTarget = (null == AutomaatZuusctand) ? null : AutomaatZuusctand.OverviewUndTarget;

			var	MengeInRaumObjekt	= (null	== OverviewUndTarget)	? null	: OverviewUndTarget.MengeOverViewObjekt;

			var RaumZuStrategikonAtomZwisceergeebnis = Raum.ZuStrategikonAtomZwisceergeebnis;

			var RaumMengeObjektCargoDurcsuuct = Raum.MengeObjektCargoDurcsuuct;

			var StrategikonRaumMengeZuBezaicnerAtom = Strategikon.RaumMengeZuBezaicnerAtom;

			if (null == StrategikonRaumMengeZuBezaicnerAtom)
			{
				return;
			}

			if(null	!= RaumZuStrategikonAtomZwisceergeebnis)
			{
				//	Berecnung noi zu beginende Atome

				foreach (var StrategikonRaumZuBezaicnerAtom in StrategikonRaumMengeZuBezaicnerAtom)
				{
					var InStrategikonAtomBezaicner = StrategikonRaumZuBezaicnerAtom.Key;
					var StrategikonRaumAtom = StrategikonRaumZuBezaicnerAtom.Value;

					if (null == StrategikonRaumAtom)
					{
						continue;
					}

					var BisherZwisceergeebnis = Optimat.Glob.TAD(RaumZuStrategikonAtomZwisceergeebnis, InStrategikonAtomBezaicner);

					if (null	!= BisherZwisceergeebnis)
					{
						//	Abarbaitung diises Atom wurde beraits begone.
						continue;
					}

					bool BeginBedingungErfült = false;

					try
					{
						var MengeBedingungKonjunkt = StrategikonRaumAtom.MengeBedingungKonjunkt;
						var MengeBedingungDisjunkt = StrategikonRaumAtom.MengeBedingungDisjunkt;

						bool? BedingungKonjunktErfült = null;
						bool? BedingungDisjunktErfült = null;

						if (null != MengeBedingungKonjunkt)
						{
							BedingungKonjunktErfült = true;

							foreach (var BedingungKonjunkt in MengeBedingungKonjunkt)
							{
								var ZuBedingungKonjunktTailBedingungZwisceergeebnis =
									Optimat.Glob.TAD(RaumZuStrategikonAtomZwisceergeebnis, BedingungKonjunkt.Key);

								if (null == ZuBedingungKonjunktTailBedingungZwisceergeebnis)
								{
									BedingungKonjunktErfült = false;
									break;
								}

								if (!((!BedingungKonjunkt.Value) == ZuBedingungKonjunktTailBedingungZwisceergeebnis.EntscaidungErfolg))
								{
									BedingungKonjunktErfült = false;
									break;
								}
							}
						}

						if (null != MengeBedingungDisjunkt)
						{
							BedingungDisjunktErfült = false;

							foreach (var BedingungDisjunkt in MengeBedingungDisjunkt)
							{
								var ZuBedingungDisjunktTailBedingungZwisceergeebnis =
									Optimat.Glob.TAD(RaumZuStrategikonAtomZwisceergeebnis, BedingungDisjunkt.Key);

								if (null == ZuBedingungDisjunktTailBedingungZwisceergeebnis)
								{
									continue;
								}

								if (((!BedingungDisjunkt.Value) == ZuBedingungDisjunktTailBedingungZwisceergeebnis.EntscaidungErfolg))
								{
									BedingungDisjunktErfült = true;
									break;
								}
							}
						}

						if ((null == BedingungKonjunktErfült && null == BedingungDisjunktErfült) ||
							true == BedingungKonjunktErfült || true == BedingungDisjunktErfült)
						{
							if (0 == StrategikonRaumZuBezaicnerAtom.Key)
							{
								//	Verzwaigung für Haltepunkt (Wurzel Atom werd aingescaltet)
							}

							BeginBedingungErfült = true;
						}
					}
					finally
					{
						if (BeginBedingungErfült)
						{
							RaumZuStrategikonAtomZwisceergeebnis[InStrategikonAtomBezaicner] = new SictStrategikonInRaumAtomZwisceergeebnis();
						}
					}
				}
			}

			if (null != RaumZuStrategikonAtomZwisceergeebnis)
			{
				//	Berecnung welce Atome beraits fertiggesctelt

				foreach (var AtomZwisceergeebnisMitBezaicner in RaumZuStrategikonAtomZwisceergeebnis)
				{
					var InStrategikonAtomBezaicner = AtomZwisceergeebnisMitBezaicner.Key;
					var AtomZwisceergeebnis = AtomZwisceergeebnisMitBezaicner.Value;

					var InStrategikonAtomSctruktuur = Optimat.Glob.TAD(StrategikonRaumMengeZuBezaicnerAtom,	InStrategikonAtomBezaicner);

					if (null == AtomZwisceergeebnis)
					{
						continue;
					}

					var AtomZwisceergeebnisErfolg = false;

					try
					{
						if (null == InStrategikonAtomSctruktuur)
						{
							continue;
						}

						var InStrategikonAtom = InStrategikonAtomSctruktuur.Atom;

						if (null == InStrategikonAtom)
						{
							AtomZwisceergeebnisErfolg = true;
							continue;
						}

						if (null == MengeInRaumObjekt)
						{
							continue;
						}

						var InStrategikonAtomMengeObjektFilter = InStrategikonAtom.MengeObjektFilter;

						var InStrategikonAtomMengeObjektFilterGrupe =
							(null == InStrategikonAtomMengeObjektFilter) ? null :
							Bib3.Glob.ArrayAusListeFeldGeflact(
							InStrategikonAtomMengeObjektFilter.Select((ObjektFilter) =>
								{
									var BedingungTypeUndName = ObjektFilter.BedingungTypeUndName;

									if (null == BedingungTypeUndName)
									{
										return null;
									}

									return BedingungTypeUndName.MengeGrupeZuDurcsuuce;
								})
							.Where((Kandidaat) => null != Kandidaat))
							.Distinct()
							.ToArray();

						var MengeObjektGefiltert = MengeInRaumObjektGefiltert(MengeInRaumObjekt, InStrategikonAtomMengeObjektFilter);

						var MengeObjektGefiltertNocSictbar =
							(null == MengeObjektGefiltert) ? null :
							MengeObjektGefiltert.Where((OverViewObjekt) => ZaitMili <= OverViewObjekt.SictungLezteZait).ToArray();

						var BedingungObjektExistentErfült = false;

						SictOverViewObjektZuusctand[] MengeObjektCargoZuDurcsuuce = null;
						SictOverViewObjektZuusctand[] MengeObjektZuZersctööre = null;
						KeyValuePair<SictOverViewObjektZuusctand, Int64>? ObjektAnzufliigeUndDistanceScranke = null;

						var FilterCargoZuDurcsuuce =
							new Func<SictOverViewObjektZuusctand, bool>((InRaumObjekt) => SictMissionZuusctand.FilterCargoZuDurcsuuce(InRaumObjekt,	RaumMengeObjektCargoDurcsuuct));

						if (!(true == InStrategikonAtom.BedingungObjektExistent) ||
							!Optimat.Glob.NullOderLeer(MengeObjektGefiltertNocSictbar))
						{
							BedingungObjektExistentErfült = true;
						}

						var	InStrategikonAtomZuObjektDistanzAinzuscteleScrankeMax	= InStrategikonAtom.ZuObjektDistanzAinzuscteleScrankeMax;

						if (null != MengeObjektGefiltert)
						{
							if (true == InStrategikonAtom.ObjektDurcsuuceCargo)
							{
								MengeObjektCargoZuDurcsuuce = MengeObjektGefiltert.Where(FilterCargoZuDurcsuuce).ToArray();
							}

							if (true == InStrategikonAtom.ObjektZersctööre)
							{
								MengeObjektZuZersctööre = MengeObjektGefiltert;
							}

							if (InStrategikonAtomZuObjektDistanzAinzuscteleScrankeMax.HasValue)
							{
								var MengeObjektGefiltertDistanceNitPasend =
									MengeObjektGefiltert.Where((InRaumObjekt) => !(InRaumObjekt.SictungLezteDistanceScrankeMax <= InStrategikonAtomZuObjektDistanzAinzuscteleScrankeMax.Value)).ToArray();

								if (0 < MengeObjektGefiltertDistanceNitPasend.Length)
								{
									ObjektAnzufliigeUndDistanceScranke = new KeyValuePair<SictOverViewObjektZuusctand, Int64>(
										MengeObjektGefiltertDistanceNitPasend.FirstOrDefault(), InStrategikonAtomZuObjektDistanzAinzuscteleScrankeMax.Value);
								}
							}
						}

						var	AtomZwisceergeebnisMengeAufgaabeObjektZuBearbaite	= new	List<SictAufgaabeInRaumObjektZuBearbaite>();

						if (ObjektAnzufliigeUndDistanceScranke.HasValue)
						{
							if (null != ObjektAnzufliigeUndDistanceScranke.Value.Key)
							{
								AtomZwisceergeebnisMengeAufgaabeObjektZuBearbaite.Add(
									SictAufgaabeInRaumObjektZuBearbaite.AufgaabeDistanceAinzusctele(
									ObjektAnzufliigeUndDistanceScranke.Value.Key, null, ObjektAnzufliigeUndDistanceScranke.Value.Value));
							}
						}

						if (null != MengeObjektCargoZuDurcsuuce)
						{
							AtomZwisceergeebnisMengeAufgaabeObjektZuBearbaite.AddRange(
								MengeObjektCargoZuDurcsuuce.Select((InRaumObjekt) => SictAufgaabeInRaumObjektZuBearbaite.AufgaabeAktioonCargoDurcsuuce(InRaumObjekt)));
						}

						if (null != MengeObjektZuZersctööre)
						{
							AtomZwisceergeebnisMengeAufgaabeObjektZuBearbaite.AddRange(
								MengeObjektZuZersctööre.Select((InRaumObjekt) => SictAufgaabeInRaumObjektZuBearbaite.AufgaabeWirkungDestrukt(InRaumObjekt)));
						}

						AtomZwisceergeebnis.MengeAufgaabeObjektZuBearbaite = AtomZwisceergeebnisMengeAufgaabeObjektZuBearbaite.ToArray();

						if (Optimat.Glob.NullOderLeer(MengeObjektCargoZuDurcsuuce))
						{
							if (!AtomZwisceergeebnis.CargoDurcsuuceErfolgZait.HasValue)
							{
								AtomZwisceergeebnis.CargoDurcsuuceErfolgZait = ZaitMili;
							}
						}
						else
						{
							AtomZwisceergeebnis.CargoDurcsuuceErfolgZait = null;
						}

						if (Optimat.Glob.NullOderLeer(MengeObjektZuZersctööre))
						{
							if (!AtomZwisceergeebnis.ZersctööreErfolgZait.HasValue)
							{
								AtomZwisceergeebnis.ZersctööreErfolgZait = ZaitMili;
							}
						}
						else
						{
							AtomZwisceergeebnis.ZersctööreErfolgZait = null;
						}

						if (ObjektAnzufliigeUndDistanceScranke.HasValue)
						{
							AtomZwisceergeebnis.AnfliigeErfolgZait = null;
						}
						else
						{
							if (!AtomZwisceergeebnis.AnfliigeErfolgZait.HasValue)
							{
								AtomZwisceergeebnis.AnfliigeErfolgZait = ZaitMili;
							}
						}

						if (BedingungObjektExistentErfült)
						{
							if (!AtomZwisceergeebnis.ObjektExistentErfolgZait.HasValue)
							{
								AtomZwisceergeebnis.ObjektExistentErfolgZait = ZaitMili;
							}
						}
						else
						{
							AtomZwisceergeebnis.ObjektExistentErfolgZait = null;
						}

						var ScritBedingungErfültBeruhigungszaitMili = InStrategikonAtom.ScritBedingungErfültBeruhigungszaitMili ?? 1000;

						var AtomZwisceergeebnisMengeScpezielErfültZaitNulbar = new Int64?[]{
							AtomZwisceergeebnis.CargoDurcsuuceErfolgZait,
							AtomZwisceergeebnis.ZersctööreErfolgZait,
							AtomZwisceergeebnis.AnfliigeErfolgZait,
							AtomZwisceergeebnis.ObjektExistentErfolgZait};

						if (AtomZwisceergeebnisMengeScpezielErfültZaitNulbar.Any((ScpezielErfültZaitNulbar) => !ScpezielErfültZaitNulbar.HasValue))
						{
							//	Aine der Scpezialisiirte Aufgaabe isc noc nit erfült.
							continue;
						}

						var AtomZwisceergeebnisScpezielErfültZaitMax =
							Optimat.Glob.Max(AtomZwisceergeebnisMengeScpezielErfültZaitNulbar) ?? 0;

						var AtomZwisceergeebnisScpezielErfültZaitMaxPlusBeruhigungszait =
							AtomZwisceergeebnisScpezielErfültZaitMax + ScritBedingungErfültBeruhigungszaitMili;

						var ZuMengeObjektGrupeMesungVolsctändigFeelend =
							OverviewUndTarget.MengeObjektGrupeUntermengeOoneOverviewViewportFolgeVolsctändigNaacZait(
							InStrategikonAtomMengeObjektFilterGrupe,
							AtomZwisceergeebnisScpezielErfültZaitMax);

						if (!Optimat.Glob.NullOderLeer(ZuMengeObjektGrupeMesungVolsctändigFeelend))
						{
							//	Verzwaigung Für Debug Haltepunkt
						}

						if (AtomZwisceergeebnisScpezielErfültZaitMaxPlusBeruhigungszait < ZaitMili)
						{
							AtomZwisceergeebnis.MengeOverviewObjektGrupeMesungZuErsctele = ZuMengeObjektGrupeMesungVolsctändigFeelend;

							//	if (ZuMengeObjektGrupeMesungVolsctändigFeelend.Length < 1)
							if (Optimat.Glob.NullOderLeer(ZuMengeObjektGrupeMesungVolsctändigFeelend))
							{
								AtomZwisceergeebnisErfolg	= true;
							}
						}
					}
					finally
					{
						if (AtomZwisceergeebnisErfolg)
						{
							AtomZwisceergeebnis.EntscaidungErfolgLezteZait = ZaitMili;
						}
						else
						{
							AtomZwisceergeebnis.EntscaidungErfolgLezteZait = null;
						}

						AtomZwisceergeebnis.Aktualisiire();
					}
				}
			}
		}

		static public SictOverViewObjektZuusctand[] MengeInRaumObjektGefiltert(
			IEnumerable<SictOverViewObjektZuusctand> MengeInRaumObjekt,
			SictStrategikonOverviewObjektFilter[] MengeFilterDisjunkt)
		{
			if (null == MengeInRaumObjekt)
			{
				return null;
			}

			if (null == MengeFilterDisjunkt)
			{
				return null;
			}

			var MengeInRaumObjektFiltert = new List<SictOverViewObjektZuusctand>();

			foreach (var InRaumObjekt in MengeInRaumObjekt)
			{
				if (null == InRaumObjekt)
				{
					continue;
				}

				foreach (var FilterDisjunkt in MengeFilterDisjunkt)
				{
					if (null == FilterDisjunkt)
					{
						continue;
					}

					if (FilterDisjunkt.Pasend(InRaumObjekt))
					{
						MengeInRaumObjektFiltert.Add(InRaumObjekt);
						break;
					}
				}
			}

			return MengeInRaumObjektFiltert.ToArray();
		}
		 * */

	}
}
