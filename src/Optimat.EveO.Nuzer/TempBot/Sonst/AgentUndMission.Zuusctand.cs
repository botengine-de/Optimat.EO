using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Bib3;
using Newtonsoft.Json;
//using Optimat.EveOnline.AuswertGbs;
using Optimat.ScpezEveOnln;


namespace Optimat.EveOnline.Anwendung
{
	[JsonObject(MemberSerialization.OptIn)]
	public partial class SictAgentUndMissionZuusctand
	{
		[JsonProperty]
		public IDictionary<SictAgentIdentSystemStationName, SictWertMitZait<VonSensor.WindowAgentDialogue>> MengeZuAgentDialogLezte;

		[JsonProperty]
		public IDictionary<SictAgentIdentSystemStationName, SictWertMitZait<VonSensor.WindowAgentDialogue>> MengeZuAgentMissionInfoLezte;

		[JsonProperty]
		public IDictionary<SictAgentIdentSystemStationName, IDictionary<string, SictWertMitZait<VonSensor.LobbyAgentEntry>>> MengeZuStationMengeAgent;

		[JsonProperty]
		public List<VonSensor.LobbyAgentEntry> AusScnapscusLobbyMengeAgentEntryPasendZuVonNuzerKonfig;

		[JsonProperty]
		public List<VonSensor.LobbyAgentEntry> AusScnapscusLobbyMengeAgentEntryPasendZuVonNuzerKonfigUndOhneUnpasendeMission;

		[JsonProperty]
		public SictWertMitZait<VonSensor.UtilmenuMissionInfo> UtilmenuMissionLezteNocOfeMitBeginZait;

		[JsonProperty]
		public SictWertMitZait<VonSensor.MessageBox>? MessageBoxCannotCompleteMissionLezte;

		/// <summary>
		/// enthalt Mission Offer, Mission in Arbait und Mission Fertig.
		/// </summary>
		[JsonProperty]
		public List<SictMissionZuusctand> MengeMission;

		/*
		 * 2014.04.19
		 * 
		 * Ersaz durc GBS.MengeWindow
		 * 
		[JsonProperty]
		public List<SictWindowAgentDialogueZuusctand> MengeWindowAgentDialogue;
		 * */

		[JsonProperty]
		public SictGbsWindowZuusctand[] MengeWindowAgentDialogue
		{
			private set;
			get;
		}

		/// <summary>
		/// Auswaal Scranke:
		/// Mit vorläufig verwendetem Program werd kompleter Zuusctand des Automaat naac jeedem Scrit kopiirt.
		/// Daher hat di länge der Liste auswirkung auf Recenzait in jeedem Scrit.
		/// </summary>
		[JsonProperty]
		public	int?	MengeMissionFertigAnzaalScrankeMax = 4;

		[JsonProperty]
		public SictWertMitZait<SictMissionZuusctand> ZuBeginZaitMissionFittingZuTesteNääxte;

		/// <summary>
		/// Mission welce als leztes waiter bearbaitet wurde.
		/// </summary>
		[JsonProperty]
		public SictMissionZuusctand MissionAktuel
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictWertMitZait<VonSensor.InfoPanelMissionsMission[]> InfoPanelMengeMissionButtonAusScnapscusVorherig;

		[JsonProperty]
		public SictWertMitZait<VonSensor.InfoPanelMissionsMission[]> InfoPanelMengeMissionButtonAusScnapscusAktuel;

		[JsonProperty]
		public	SictWertMitZait<VonSensor.InfoPanelMissionsMission[]> InfoPanelMengeMissionButtonAktuel;

		[JsonProperty]
		public VonSensor.InfoPanelMissionsMission MissionButtonUtilmenuObjectiveZuMese
		{
			private set;
			get;
		}

		/// <summary>
		/// Window desen Mission "Accept"ed oder "Request Mission" were sol.
		/// </summary>
		[JsonProperty]
		public VonSensor.WindowAgentDialogue WindowAgentDialogueMissionAcceptOderRequest
		{
			private set;
			get;
		}

		/// <summary>
		/// Agent Entry aus Lobby für welcen "Start Conversation" ausgefüürt were sol.
		/// </summary>
		[JsonProperty]
		public VonSensor.LobbyAgentEntry	LobbyAgentEntryStartConversation
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictMissionZuusctand MissionAcceptNääxte
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictMissionZuusctand MissionDeclineNääxte
		{
			private set;
			get;
		}

		[JsonProperty]
		List<Int64> ListeMissionDeclineZait;

		[JsonProperty]
		bool? MissionDeclineUnabhängigVonStandingLossFraigaabe
		{
			set;
			get;
		}

		/// <summary>
		/// Element bescraibt das Utilmenu welces zulezt zu dem Index (Key im Dict) in dem Stapel im Info Panel gesictet wurde.
		/// </summary>
		[JsonProperty]
		public IDictionary<int, SictWertMitZait<VonSensor.UtilmenuMissionInfo>> MengeZuAusInfoPanelMissionStapelIndexUtilmenu;

		[JsonProperty]
		public Int64? AnforderungActiveShipCargoLeereLezteZaitMili
		{
			private set;
			get;
		}

		/// <summary>
		/// 2014.07.25
		/// ainige Nuzer scainen Probleeme zu haabe di Funktioonswaise des Fitting zu verscteehe.
		/// Um diisen Nuzern ain Erfolgserleebnis zu ermööglice sol der Automaat auf das leere des Cargo verzicte wen kaine Fitting konfiguriirt sind damit
		/// der Automaat mit dem vorgefundenen Ship in Msn ziihe kan und trozdeem Ammo zur verfüügung hat.
		/// </summary>
		[JsonProperty]
		public	bool?	VonNuzerParamScpezVerzictAufCargoLeere
		{
			private set;
			get;
		}

		[JsonProperty]
		public KeyValuePair<string, int?>[] InAktuelemSystemMengeStationFürWelcePasendeMissionNictAusgesclose
		{
			private set;
			get;
		}

		public Int64? MissionIdentLezte
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictWertMitZait<string>? FittingFitLoadedLezteNocAktiivMitZait
		{
			private set;
			get;
		}

		public SictWertMitZait<VonSensor.UtilmenuMissionInfo> ZuAusInfoPanelMissionStapelIndexUtilmenu(int Index)
		{
			var MengeZuAusInfoPanelMissionStapelIndexUtilmenu = this.MengeZuAusInfoPanelMissionStapelIndexUtilmenu;

			if (null == MengeZuAusInfoPanelMissionStapelIndexUtilmenu)
			{
				return default(SictWertMitZait<VonSensor.UtilmenuMissionInfo>);
			}

			return Optimat.Glob.TAD(MengeZuAusInfoPanelMissionStapelIndexUtilmenu, Index);
		}

		public SictGbsWindowZuusctand ZuAgentEntryWindowAgentDialogue(
			VonSensor.LobbyAgentEntry AgentEntry)
		{
			if (null == AgentEntry)
			{
				return null;
			}

			var MengeWindowAgentDialogue = this.MengeWindowAgentDialogue;

			if (null == MengeWindowAgentDialogue)
			{
				return null;
			}

			foreach (var WindowAgentDialogue in MengeWindowAgentDialogue)
			{
				if (null == WindowAgentDialogue)
				{
					continue;
				}

				var	ScnapscusWindowAgentDialogue	= WindowAgentDialogue.AingangScnapscusTailObjektIdentLezteBerecne()	as	VonSensor.WindowAgentDialogue;

				if (null == ScnapscusWindowAgentDialogue)
				{
					continue;
				}

				if (!string.Equals(ScnapscusWindowAgentDialogue.AgentName, AgentEntry.AgentName))
				{
					continue;
				}

				return WindowAgentDialogue;
			}

			return null;
		}

		public	VonSensor.WindowAgentDialogue	ZuAgentEntryWindowAgentDialogueScnapscus(
			VonSensor.LobbyAgentEntry AgentEntry)
		{
			var Window = ZuAgentEntryWindowAgentDialogue(AgentEntry);

			if (null == Window)
			{
				return null;
			}

			return Window.AingangScnapscusTailObjektIdentLezteBerecne() as VonSensor.WindowAgentDialogue;
		}

		static public bool MissionButtonHinraicendGlaicwertigFürAnnaameVolsctändig(
			VonSensor.InfoPanelMissionsMission Scnapscus0MissionButton,
			VonSensor.InfoPanelMissionsMission Scnapscus1MissionButton)
		{
			if (object.Equals(Scnapscus0MissionButton, Scnapscus1MissionButton))
			{
				return true;
			}

			if (object.Equals(Scnapscus0MissionButton, Scnapscus1MissionButton))
			{
				return true;
			}

			if (null == Scnapscus0MissionButton)
			{
				return false;
			}

			if (null == Scnapscus1MissionButton)
			{
				return false;
			}

			if (!string.Equals(Scnapscus0MissionButton.Bescriftung, Scnapscus1MissionButton.Bescriftung))
			{
				return false;
			}

			var Scnapscus0MissionButtonMissionKnopfInGbsFläce =
				(null == Scnapscus0MissionButton) ? OrtogoonInt.Leer : Scnapscus0MissionButton.InGbsFläce;

			var Scnapscus1MissionButtonMissionKnopfInGbsFläce =
				(null == Scnapscus1MissionButton) ? OrtogoonInt.Leer : Scnapscus1MissionButton.InGbsFläce;

			if (Scnapscus0MissionButtonMissionKnopfInGbsFläce == Scnapscus1MissionButtonMissionKnopfInGbsFläce)
			{
				return true;
			}

			if (Scnapscus0MissionButtonMissionKnopfInGbsFläce.IsLeer)
			{
				return false;
			}

			if (Scnapscus1MissionButtonMissionKnopfInGbsFläce.IsLeer)
			{
				return false;
			}

			var DistanzKwadraat = (Scnapscus0MissionButtonMissionKnopfInGbsFläce.ZentrumLaage - Scnapscus1MissionButtonMissionKnopfInGbsFläce.ZentrumLaage).BetraagQuadriirt;

			if (1 < DistanzKwadraat)
			{
				return false;
			}

			return true;
		}

		static public bool AusGbsMengeMissionButtonHinraicendGlaicwertigFürAnnaameVolsctändig(
			VonSensor.InfoPanelMissionsMission[] Scnapscus0ListeMissionButton,
			VonSensor.InfoPanelMissionsMission[] Scnapscus1ListeMissionButton)
		{
			if (object.Equals(Scnapscus0ListeMissionButton, Scnapscus1ListeMissionButton))
			{
				return true;
			}

			var ListeMissionButtonAmzaalMax = Math.Max(
				(null == Scnapscus0ListeMissionButton) ? 0 : Scnapscus0ListeMissionButton.Length,
				(null == Scnapscus1ListeMissionButton) ? 0 : Scnapscus1ListeMissionButton.Length);

			var Unglaic = false;

			for (int MissionButtonIndex = 0; MissionButtonIndex < ListeMissionButtonAmzaalMax; MissionButtonIndex++)
			{
				var Scnapscus0MissionButton = Optimat.Glob.ElementAtOrDefault(Scnapscus0ListeMissionButton, MissionButtonIndex);

				var Scnapscus1MissionButton = Optimat.Glob.ElementAtOrDefault(Scnapscus1ListeMissionButton, MissionButtonIndex);

				if (!MissionButtonHinraicendGlaicwertigFürAnnaameVolsctändig(Scnapscus0MissionButton, Scnapscus1MissionButton))
				{
					Unglaic = true;
					break;
				}
			}

			return !Unglaic;
		}

		static bool PrädikaatMissionAkzeptiirtUndNictBeendet(SictMissionZuusctand Mission)
		{
			if (null == Mission)
			{
				return false;
			}

			var TailFürNuzer = Mission.TailFürNuzer;

			if (null == TailFürNuzer)
			{
				return false;
			}

			return TailFürNuzer.IstAkzeptiirtUndNictBeendet();
		}

		static bool PrädikaatMissionFertig(SictMissionZuusctand Mission)
		{
			if (null == Mission)
			{
				return false;
			}

			var TailFürNuzer = Mission.TailFürNuzer;

			if (null == TailFürNuzer)
			{
				return false;
			}

			return TailFürNuzer.EndeZaitMili.HasValue;
		}

		static bool PrädikaatMissionFertigNict(SictMissionZuusctand Mission)
		{
			if (null == Mission)
			{
				return false;
			}

			return !PrädikaatMissionFertig(Mission);
		}

		public IEnumerable<SictMissionZuusctand> MengeMissionAkzeptiirtUndNictBeendetBerecne()
		{
			return	Optimat.Glob.Tailmenge(MengeMission,	PrädikaatMissionAkzeptiirtUndNictBeendet);
		}

		public IEnumerable<SictMissionZuusctand> MengeMissionFertigBerecne()
		{
			return Optimat.Glob.Tailmenge(MengeMission, PrädikaatMissionFertig);
		}

		public struct SictStationMengeMissionBewertung
		{
			public int MengeAgentOoneMissionUnpasendAnzaal;
		}

		public int? ZuStationMengeAgentPasendOoneMissionUnpasendAnzaalBerecne(
			SictAgentIdentSystemStationName StationIdent,
			SictOptimatParamMission OptimatParamMission,
			Int64? GültigkaitZaitScrankeMin = null)
		{
			if (null == StationIdent)
			{
				return 0;
			}

			if (null == OptimatParamMission)
			{
				return 0;
			}

			var	StationMengeAgentMitMission	= ZuStationMengeAgentMitMissionInfoLezteBerecne(StationIdent, GültigkaitZaitScrankeMin);

			if(null	== StationMengeAgentMitMission)
			{
				return	null;
			}

			if (StationMengeAgentMitMission.Count < 1)
			{
				return null;
			}

			int	ZuStationMengeAgentPasendOoneMissionUnpasendAnzaal	= 0;

			foreach (var AgentMitMission in StationMengeAgentMitMission)
			{
				var	AgentEntry	= AgentMitMission.Key;

				if(null	== AgentEntry)
				{
					continue;
				}

				var	AgentLevelNulbar	= AgentEntry.AgentLevel;

				if(!AgentLevelNulbar.HasValue)
				{
					continue;
				}

				var	AgentLevel	= AgentLevelNulbar.Value;

				if(!OptimatParamMission.AktioonFüüreAusFraigaabeFürAgentLevel(AgentLevel))
				{
					continue;
				}

				var Mission = AgentMitMission.Value;

				if (null != Mission)
				{
					var MissionTailFürNuzer = Mission.TailFürNuzer;

					var ZuMissionVerhalte = MissionTailFürNuzer.AusPräferenzEntscaidungVerhalte;

					if (null == ZuMissionVerhalte)
					{
						continue;
					}

					if (!(true == ZuMissionVerhalte.AktioonAcceptAktiiv))
					{
						continue;
					}
				}

				++ZuStationMengeAgentPasendOoneMissionUnpasendAnzaal;
			}

			return ZuStationMengeAgentPasendOoneMissionUnpasendAnzaal;
		}

		public IDictionary<VonSensor.LobbyAgentEntry, SictMissionZuusctand>
			ZuStationMengeAgentMitMissionInfoLezteBerecne(
			SictAgentIdentSystemStationName StationIdent,
			Int64? GültigkaitZaitScrankeMin = null)
		{
			if (null == StationIdent)
			{
				return null;
			}

			var	MengeMission	= this.MengeMission;
			var MengeZuStationMengeAgent = this.MengeZuStationMengeAgent;
			var MengeZuAgentMissionInfoLezte = this.MengeZuAgentMissionInfoLezte;

			if (null == MengeZuStationMengeAgent)
			{
				return null;
			}

			var ZuStationMengeAgent = Optimat.Glob.TAD(MengeZuStationMengeAgent, StationIdent);

			if (null == ZuStationMengeAgent)
			{
				return null;
			}

			var MengeAgentMitMissionInfoLezte = new Dictionary<VonSensor.LobbyAgentEntry, SictMissionZuusctand>();

			foreach (var ZuZaitAgentEntry in ZuStationMengeAgent.Values)
			{
				if (ZuZaitAgentEntry.Zait < GültigkaitZaitScrankeMin)
				{
					continue;
				}

				var AgentEntry = ZuZaitAgentEntry.Wert;

				SictMissionZuusctand ZuAgentEntryMissionBerüksictigt = null;

				try
				{
					//	Lezte Mission zu gewäälte AgentEntry suuce

					var MengeKandidaatMissionMitZait = new List<SictWertMitZait<SictMissionZuusctand>>();

					if (null != MengeMission)
					{
						foreach (var KandidaatMission in MengeMission)
						{
							if (null == KandidaatMission)
							{
								continue;
							}

							if (!AgentEntryPasendZuMission(AgentEntry, KandidaatMission))
							{
								continue;
							}

							var ZuAgentEntryMissionInfoLezte = KandidaatMission.MissionInfoZuZaitLezteBerecne();

							if (null == ZuAgentEntryMissionInfoLezte.Wert)
							{
								continue;
							}

							if (ZuAgentEntryMissionInfoLezte.Zait < GültigkaitZaitScrankeMin)
							{
								continue;
							}

							MengeKandidaatMissionMitZait.Add(new SictWertMitZait<SictMissionZuusctand>(ZuAgentEntryMissionInfoLezte.Zait, KandidaatMission));
						}
					}

					ZuAgentEntryMissionBerüksictigt =
						MengeKandidaatMissionMitZait
						.OrderBy((KandidaatMissionMitZait) => KandidaatMissionMitZait.Zait).LastOrDefault().Wert;
				}
				finally
				{
					MengeAgentMitMissionInfoLezte[AgentEntry] = ZuAgentEntryMissionBerüksictigt;
				}
			}

			return MengeAgentMitMissionInfoLezte;
		}

		static public bool AgentEntryPasendZuMission(
			VonSensor.LobbyAgentEntry AgentEntry,
			SictMissionZuusctand Mission)
		{
			if (null == AgentEntry)
			{
				return false;
			}

			if (null == Mission)
			{
				return false;
			}

			var MissionTailFürNuzer = Mission.TailFürNuzer;

			return	AgentEntryPasendZuMission(AgentEntry,	MissionTailFürNuzer);
		}

		static public bool AgentEntryPasendZuMission(
			VonSensor.LobbyAgentEntry AgentEntry,
			EveOnline.SictMissionZuusctand Mission)
		{
			if (null == AgentEntry)
			{
				return false;
			}

			if (null == Mission)
			{
				return false;
			}

			if (!string.Equals(Mission.AgentName, AgentEntry.AgentName))
			{
				return false;
			}

			if (!(Mission.AgentLevel == AgentEntry.AgentLevel))
			{
				return false;
			}

			return true;
		}

		static public bool? AgentEntryPasendZuVonNuzerParam(
			VonSensor.LobbyAgentEntry	AgentEntry,
			SictOptimatParam OptimatParam)
		{
			if (null == AgentEntry)
			{
				return null;
			}

			if (null == OptimatParam)
			{
				return null;
			}

			var OptimatParamMission = OptimatParam.Mission;

			if (null == OptimatParamMission)
			{
				return null;
			}

			if (null == AgentEntry.HeaderText)
			{
				return null;
			}

			if (!Regex.Match(AgentEntry.HeaderText, "Available", RegexOptions.IgnoreCase).Success)
			{
				return	false;
			}

			if (Regex.Match(AgentEntry.HeaderText, "not.*Available", RegexOptions.IgnoreCase).Success)
			{
				return	false;
			}

			var AktioonAcceptMengeAgentLevelFraigaabe = OptimatParamMission.AktioonAcceptMengeAgentLevelFraigaabe;

			if (null == AktioonAcceptMengeAgentLevelFraigaabe)
			{
				return false;
			}

			if (!AktioonAcceptMengeAgentLevelFraigaabe.Contains(AgentEntry.AgentLevel ?? -1))
			{
				return	false;
			}

			return true;
		}

		static public bool? MissionObjectivePasendZuVonNuzerParam(
			VonSensor.LobbyAgentEntry	AgentEntry,
			VonSensor.WindowAgentDialogue AgentDialogue,
			SictOptimatParam OptimatParam)
		{
			var AgentLevel = (null == AgentEntry) ? null : AgentEntry.AgentLevel;

			return MissionObjectivePasendZuVonNuzerParam(
				AgentLevel,
				AgentDialogue,
				OptimatParam);
		}

		static public bool? MissionObjectivePasendZuVonNuzerParam(
			int?	AgentLevel,
			VonSensor.WindowAgentDialogue AgentDialogue,
			SictOptimatParam OptimatParam)
		{
			if (null == AgentDialogue)
			{
				return null;
			}

			if (null == OptimatParam)
			{
				return null;
			}

			/*
			 * 2015.02.20
			var MissionInfo = AgentDialogue.RightPaneMissionInfo;
			 * */
			var MissionInfo = AgentDialogue.ZusamefasungMissionInfo;

			if (null == MissionInfo)
			{
				return null;
			}

			if (true == MissionInfo.Complete)
			{
				//	Nääxte Mission noc nit bekant.
				return null;
			}

			if (MissionInfo.PasendZuVonNuzerParamMissionAcceptFraigaabe(AgentLevel,	OptimatParam))
			{
				return true;
			}

			return false;
		}

		static public bool FilterÜbernaameNaacAutomaat(VonSensor.InfoPanelMissionsMission Kandidaat)
		{
			if (null == Kandidaat)
			{
				return false;
			}

			var MissionKnopfInGbsFläce = Kandidaat.InGbsFläce;

			if (MissionKnopfInGbsFläce.IsLeer)
			{
				return false;
			}

			return true;
		}

		static public bool UtilmenuMissionIdentisc(
			VonSensor.UtilmenuMissionInfo Menu0,
			VonSensor.UtilmenuMissionInfo Menu1)
		{
			if (object.Equals(Menu0, Menu1))
			{
				return true;
			}

			if (null == Menu0)
			{
				return false;
			}

			if (null == Menu1)
			{
				return false;
			}

			var Menu0MissionTitel = Menu0.MissionTitelText;
			var Menu1MissionTitel = Menu1.MissionTitelText;

			if (!string.Equals(Menu0MissionTitel, Menu1MissionTitel))
			{
				return false;
			}

			return true;
		}

		static public int? FürUtilmenuMissionIndexInListeMissionButton(
			VonSensor.UtilmenuMissionInfo Utilmenu,
			VonSensor.InfoPanelMissionsMission[]	ListeMission)
		{
			if (null == Utilmenu)
			{
				return null;
			}

			if (null == ListeMission)
			{
				return null;
			}

			var	UtilmenuMissionTitel	= Utilmenu.MissionTitelText;

			if(null	== UtilmenuMissionTitel)
			{
				return	null;
			}

			var	UtilmenuHeader	= Utilmenu.Header;

			if(null	== UtilmenuHeader)
			{
				return	null;
			}

			var	UtilmenuHeaderInGbsFläce	= UtilmenuHeader.InGbsFläce;

			if(UtilmenuHeaderInGbsFläce.IsLeer)
			{
				return	null;
			}

			for (int InListeMissionIndex = 0; InListeMissionIndex < ListeMission.Length; InListeMissionIndex++)
			{
				var InListeMission = ListeMission[InListeMissionIndex];

				if (null == InListeMission)
				{
					continue;
				}

				var InListeMissionInGbsFläce = InListeMission.InGbsFläce;

				if (!string.Equals(UtilmenuMissionTitel, InListeMission.Bescriftung))
				{
					continue;
				}

				if (InListeMissionInGbsFläce.IsLeer)
				{
					continue;
				}

				if (!InListeMissionInGbsFläce.Vergröösert(-2, -2).EnthältPunktFürMinInklusiivUndMaxInklusiiv(UtilmenuHeaderInGbsFläce.ZentrumLaage))
				{
					continue;
				}

				return InListeMissionIndex;
			}

			return null;
		}

		/// <summary>
		/// Entfernt aus <paramref name="MengeMission"/> Tailmenge deren EndeZaitMili unterhalb Scranke <paramref name="EndeZaitScrankeMin"/>.
		/// </summary>
		/// <param name="MengeMission"></param>
		/// <param name="EndeZaitScrankeMin"></param>
		static public void AusMengeMissionEntferneMitEndeZaitMiliKlainerScranke(
			ICollection<SictMissionZuusctand> MengeMission,
			Int64 EndeZaitScrankeMin,
			out	Int64? MissionEntferntLezteEndeZaitMili)
		{
			MissionEntferntLezteEndeZaitMili = null;

			if (null == MengeMission)
			{
				return;
			}

			var MengeMissionFertigZuEntferne = MengeMission.Take(0).ToList();

			foreach (var Mission in MengeMission)
			{
				if (null == Mission)
				{
					continue;
				}

				var MissionEndeZaitMili = Mission.EndeZaitMili();

				if (!MissionEndeZaitMili.HasValue)
				{
					continue;
				}

				if (MissionEndeZaitMili < EndeZaitScrankeMin)
				{
					MengeMissionFertigZuEntferne.Add(Mission);

					MissionEntferntLezteEndeZaitMili = Bib3.Glob.Max(MissionEntferntLezteEndeZaitMili, MissionEndeZaitMili);
				}
			}

			foreach (var MissionFertigZuEntferne in MengeMissionFertigZuEntferne)
			{
				MengeMission.Remove(MissionFertigZuEntferne);
			}
		}

	}

}
