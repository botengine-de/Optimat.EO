using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Optimat.EveOnline;
using Optimat.EveOnline.Anwendung;
using Bib3;
using Optimat.EveOnline;
using Optimat.EveOnline.Base;
//using Optimat.EveOnline.AuswertGbs;
using Optimat.EveOnline.Anwendung.AuswertGbs;
using Optimat.EveOnline.VonSensor;
using VonSensor = Optimat.EveOnline.VonSensor;
using ExtractFromOldAssembly.Bib3;

namespace Optimat.ScpezEveOnln
{
	public partial class SictAutomatZuusctand
	{
		static public int FürVerdrängendeAufgaabeZuWarteScritAnzaalBerecne(SictAufgaabeParam AufgaabeParam)
		{
			if (null == AufgaabeParam)
			{
				return 0;
			}

			return AufgaabeParam.WartezaitBisEntscaidungErfolgScritAnzaalMax();

			var FürVerdrängendeAufgaabeZuWarteScritAnzaal = 0;

			var AufgaabeParamAndere = AufgaabeParam as AufgaabeParamAndere;

			if (null != AufgaabeParamAndere)
			{
				if (AufgaabeParamAndere.AktioonMenuBegin ?? false)
				{
					FürVerdrängendeAufgaabeZuWarteScritAnzaal = Math.Max(2, FürVerdrängendeAufgaabeZuWarteScritAnzaal);
				}

				if (null != AufgaabeParamAndere.MenuEntry)
				{
					FürVerdrängendeAufgaabeZuWarteScritAnzaal = Math.Max(2, FürVerdrängendeAufgaabeZuWarteScritAnzaal);
				}
			}

			return FürVerdrängendeAufgaabeZuWarteScritAnzaal;
		}

		public void AktualisiireTailPräferenzZuZaitVerhalteKombi()
		{
			this.ParamZuZaitVerhalteKombi = PräferenzZuZaitVerhalteKombiBerecne();
		}

		public void AktualisiireTailScritZait()
		{
			var NuzerZaitMili = this.NuzerZaitMili;
			var ServerZaitMili = this.ServerZaitMili;

			if (0 < InternListeScritZait.Count)
			{
				if (NuzerZaitMili <= InternListeScritZait.LastOrDefault().NuzerZait)
				{
					return;
				}
			}

			InternListeScritZait.Enqueue(new ZuScritInfoZait((ScritLezteIndex + 1) ?? 0, NuzerZaitMili, ServerZaitMili));
			InternListeScritZait.ListeKürzeBegin(10);

			this.InternScritDauerDurcscnit = ScritDauerDurcscnitBerecne(InternListeScritZait);
		}

		override public void Update()
		{
			Aktualisiire();
		}

		void Aktualisiire()
		{
			AktualisiireTailScritZait();

			AktualisiireTailAingangNaacProcessWirkung();

			AktualisiireTailPräferenzZuZaitVerhalteKombi();

			{
				var tDebug =
					1 < ListeScnapscusLezteAuswertungErgeebnisNaacSimu?.MengeMenu?.Length &&
					(ListeScnapscusLezteAuswertungErgeebnisNaacSimu?.MengeMenu?.FirstOrDefault()?.ListeEntry?.Any(Entry => Regex.Match(Entry?.Bescriftung ?? "", @"load.*default", RegexOptions.IgnoreCase).Success) ?? false);

				if(tDebug)
				{

				}
            }

			AktualisiireZuusctandAusScnapscusAuswertung();

			{
				var t = this.AufgaabeBerecneAktueleTailaufgaabeCall.ToArray();

				AufgaabeBerecneAktueleTailaufgaabeCall.Clear();
            }
			AktualisiireTailNaacNuzerMeldungZuusctand();
		}

		public void AktualisiireTailNaacNuzerMeldungZuusctand()
		{
			var NaacNuzerBerict = this.NaacNuzerMeldungZuusctand;

			if (null == NaacNuzerBerict)
			{
				NaacNuzerBerict = new Optimat.EveOnline.SictVonOptimatMeldungZuusctand();
			}

			AktualisiireTailNaacNuzerMeldungZuusctand(NaacNuzerBerict);

			this.NaacNuzerMeldungZuusctand = NaacNuzerBerict;
		}

		public void AktualisiireTailNaacNuzerMeldungZuusctand(
			Optimat.EveOnline.SictVonOptimatMeldungZuusctand NaacNuzerBerict)
		{
			if (null == NaacNuzerBerict)
			{
				return;
			}

			var Gbs = this.Gbs;
			var FittingUndShipZuusctand = this.FittingUndShipZuusctand;
			var AgentUndMission = this.AgentUndMission;
			var ScnapscusAuswertungErgeebnis = this.ListeScnapscusLezteAuswertungErgeebnisNaacSimu;

			var NeocomClockZaitModuloTaagMinMax = (null == Gbs) ? null : Gbs.NeocomClockZaitModuloTaagMinMax;
			var GbsMengeNaacNuzerMeldung = (null == Gbs) ? null : Gbs.MengeNaacNuzerMeldung;

			var CurrentLocationInfo = (null == ScnapscusAuswertungErgeebnis) ? null : ScnapscusAuswertungErgeebnis.CurrentLocationInfo();

			var ShipZuusctand = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.ShipZuusctand;

			var MengeMission = (null == AgentUndMission) ? null : AgentUndMission.MengeMission;

			var ScnapscusWindowFittingMgmt =
				(null == ScnapscusAuswertungErgeebnis) ? null : ScnapscusAuswertungErgeebnis.WindowFittingMgmt;

			var ScnapscusNeocomClock =
				(null == ScnapscusAuswertungErgeebnis) ? null : ScnapscusAuswertungErgeebnis.NeocomClock;

			var ScnapscusNeocomClockBescriftung = (null == ScnapscusNeocomClock) ? null : ScnapscusNeocomClock.Bescriftung;

			var ScnapscusWindowFittingMgmtMengeFittingEntry =
				(null == ScnapscusWindowFittingMgmt) ? null : ScnapscusWindowFittingMgmt.MengeFittingEntry;

			NaacNuzerBerict.SizungIdent = this.AnwendungSizungIdent;

			var MengeMeldungZuEveOnline = NaacNuzerBerict.MengeMeldungZuEveOnline;
			var FittingManagementMengeFittingPfaadListeGrupeNaame = NaacNuzerBerict.FittingManagementMengeFittingPfaadListeGrupeNaame;

			if (null == MengeMeldungZuEveOnline)
			{
				MengeMeldungZuEveOnline = new List<SictNaacNuzerMeldungZuEveOnline>();
			}

			if (null == FittingManagementMengeFittingPfaadListeGrupeNaame)
			{
				FittingManagementMengeFittingPfaadListeGrupeNaame = new List<string[]>();
			}

			if (null != ScnapscusWindowFittingMgmtMengeFittingEntry)
			{
				var ScnapscusWindowFittingMgmtMengeFittingEntryName =
					ScnapscusWindowFittingMgmtMengeFittingEntry
					.Select((FittingEntry) => FittingEntry.Bescriftung)
					.Where((FittingEntryName) => null != FittingEntryName)
					.ToArray();

				var ScnapscusWindowFittingMgmtMengeFittingEntryListeGrupeName =
					ScnapscusWindowFittingMgmtMengeFittingEntryName
					.Select((FittingEntryName) => new string[] { FittingEntryName })
					.ToArray();

				Bib3.Glob.PropagiireListeRepräsentatioon(
					ScnapscusWindowFittingMgmtMengeFittingEntryListeGrupeName,
					FittingManagementMengeFittingPfaadListeGrupeNaame as IList<string[]>,
					(FittingEntryName) => FittingEntryName,
					(KandidaatRepr, FittingEntryName) => Bib3.Glob.SequenceEqualPerObjectEquals(KandidaatRepr, FittingEntryName),
					null,
					true);

				Bib3.Extension.ListeKürzeBegin(FittingManagementMengeFittingPfaadListeGrupeNaame, 44);
			}

			var GbsMengeNaacNuzerMeldungTailmengeZuÜbertraage =
				ExtractFromOldAssembly.Bib3.Extension.WhereNullable(
				GbsMengeNaacNuzerMeldung,
				(Kandidaat) =>
					//	nur Meldunge übertraage welce scon mindesctens in zwai Scrite aktiiv waare.
					Kandidaat.BeginZait < Kandidaat.AktiivLezteZait);

			Bib3.Glob.PropagiireListeRepräsentatioonMitReprUndIdentPerClrReferenz(
				GbsMengeNaacNuzerMeldungTailmengeZuÜbertraage,
				MengeMeldungZuEveOnline);

			var MengeMissionTailNuzer =
				ExtractFromOldAssembly.Bib3.Extension.SelectNullable(
				MengeMission,
				(Mission) => Mission.TailFürNuzer)
				.ToArrayNullable();

			NaacNuzerBerict.BerecnungVorsclaagLezteZait = NuzerZaitMili;

			NaacNuzerBerict.MengeMeldungZuEveOnline = MengeMeldungZuEveOnline;

			NaacNuzerBerict.EveWeltTopologii = this.EveWeltTopologii;

			NaacNuzerBerict.EveOnlineZaitKalenderModuloTaagMinMax = NeocomClockZaitModuloTaagMinMax;

			NaacNuzerBerict.EveOnlineZaitKalenderModuloTaagString = ScnapscusNeocomClockBescriftung;

			NaacNuzerBerict.ShipZuusctand = ShipZuusctand;
			NaacNuzerBerict.CurrentLocation = CurrentLocationInfo;

			NaacNuzerBerict.FittingInfoAgrString = SictShipZuusctandMitFitting.MengeModuleInfoAgrStringBerecne(this.ShipMengeModule());

			var NaacNuzerBerictMengeMission = NaacNuzerBerict.MengeMission;

			if (null == NaacNuzerBerictMengeMission)
			{
				NaacNuzerBerictMengeMission = new List<Optimat.EveOnline.SictMissionZuusctand>();
			}

			Bib3.Glob.PropagiireListeRepräsentatioonMitReprUndIdentPerClrReferenz(
				MengeMissionTailNuzer,
				NaacNuzerBerictMengeMission);

			NaacNuzerBerict.MengeMission = NaacNuzerBerictMengeMission;
			NaacNuzerBerict.FittingManagementMengeFittingPfaadListeGrupeNaame = FittingManagementMengeFittingPfaadListeGrupeNaame;
			NaacNuzerBerict.MengeZuMissionFilterAktioonVerfüügbar = SictAutomat.MengeZuMissionFilterAktioonVerfüügbar();

			var VorsclaagListeWirkung = FürNuzerVorsclaagWirkungBerecne().ToArrayFalsNitLeer();

			if (!VorsclaagListeWirkung.NullOderLeer())
			{
				NaacNuzerBerict.VorsclaagListeWirkung = VorsclaagListeWirkung.ToList();
			}

			{
				//	MesungNääxteZaitScrankeMin

				if(VorsclaagListeWirkung.NullOderLeer())
				{
					var	ListeZuZaitNaacNuzerVorsclaagNaacProcessWirkungLezte	=
						ListeZuZaitNaacNuzerVorsclaagNaacProcessWirkung.LastOrDefaultNullable();

					var NaacNuzerVorsclaagNaacProcessWirkungLezteZait =
						ListeZuZaitNaacNuzerVorsclaagNaacProcessWirkungLezte.Zait;

					var NaacNuzerVorsclaagNaacProcessWirkungLezteAlter =
						NuzerZaitMili - NaacNuzerVorsclaagNaacProcessWirkungLezteZait;

					if (3333 < NaacNuzerVorsclaagNaacProcessWirkungLezteAlter)
					{
						NaacNuzerBerict.MesungNääxteZaitScrankeMin = NuzerZaitMili + 1777;
					}
				}
			}
		}

		public void AktualisiireZuusctandAusScnapscusAuswertung()
		{
			AktualisiireTailScritZait();

			var ListeScnapscusLezteAuswertungErgeebnis = this.ListeScnapscusLezteAuswertungErgeebnisNaacSimu;

			AktualisiireZuusctandAusScnapscusAuswertung(ListeScnapscusLezteAuswertungErgeebnis);
		}

		public void AktualisiireTailMengeZuOreTypSurveyScanInfo()
		{
			ZuOreTypAusSurveyScanInfo.MengeZuOreTypZuusctandAktualisiire(
				this,
				this.InternMengeZuOreTypSictStringSurveyScanInfo);
		}

		public void AktualisiireTailMengeZuTargetSurveyScanInfo()
		{
			ZuTargetAinscrankungMengeSurveyScanItem.MengeZuTargetAinscrankungAktualisiire(
				this,
				this.InternMengeZuTargetSurveyScanInfo);
		}

		public void AktualisiireTailMengeZuListEntryInRaumObjekt()
		{
			//	VerbindungListEntryUndInRaumObjektPerDistance<SictTargetZuusctand,	SictAusGbsTargetInfo>

			VerbindungListEntryUndTargetPerDistance.AktualisiireMengeZuEntryInRaumObjekt(
				this.MengeListEntryZuusctand(),
				this.InternMengeZuListEntryTarget,
				this);
		}

		public void AktualisiireTailUtilmenu(VonSensorikMesung AusScnapscusAuswertungZuusctand)
		{
			SictWertMitZait<VonSensor.UtilmenuMissionInfo>? UtilmenuMissionLezte = null;

			try
			{
				if (null == AusScnapscusAuswertungZuusctand)
				{
					return;
				}

				var BisherUtilmenuMissionLezte = this.UtilmenuMissionLezte;

				var AusScnapscusUtilmenuMission = AusScnapscusAuswertungZuusctand.UtilmenuMission;

				if (null != AusScnapscusUtilmenuMission)
				{
					var UtilmenuBeginZait = NuzerZaitMili;

					if (BisherUtilmenuMissionLezte.HasValue)
					{
						if (null != BisherUtilmenuMissionLezte.Value.Wert)
						{
							if (string.Equals(AusScnapscusUtilmenuMission.MissionTitelText, BisherUtilmenuMissionLezte.Value.Wert.MissionTitelText))
							{
								UtilmenuBeginZait = BisherUtilmenuMissionLezte.Value.Zait;
							}
						}
					}

					UtilmenuMissionLezte = new SictWertMitZait<VonSensor.UtilmenuMissionInfo>(UtilmenuBeginZait, AusScnapscusUtilmenuMission);
				}
			}
			finally
			{
				this.UtilmenuMissionLezte = UtilmenuMissionLezte;
			}
		}

		public void AktualisiireZuusctandAusScnapscusAuswertungTailGbs(
			SictAusGbsScnapscusAuswertungSrv AusScnapscusAuswertungZuusctand)
		{
			var InternZuusctand = this;

			AktualisiireTailUtilmenu(AusScnapscusAuswertungZuusctand);

			var ListeAusGbsAbovemainMessage = InternZuusctand.InternListeAusGbsAbovemainMessageMitZait;

			var GbsZuusctand = InternZuusctand.Gbs;
			var FittingUndShipZuusctand = InternZuusctand.FittingUndShipZuusctand;
			var OverviewUndTarget = InternZuusctand.OverviewUndTarget;
			var AgentUndMission = InternZuusctand.AgentUndMission;
			var InRaumAktioonUndGefect = InternZuusctand.InRaumAktioonUndGefect;
			var AutoMine = InternZuusctand.AutoMine;

			var ShipZuusctandLezte = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.ShipZuusctand;

			var SelbsctShipDocking = (null == ShipZuusctandLezte) ? null : ShipZuusctandLezte.Docking;
			var SelbsctShipDocked = (null == AusScnapscusAuswertungZuusctand) ? null : AusScnapscusAuswertungZuusctand.Docked();
			var SelbsctShipWarping = (null == ShipZuusctandLezte) ? null : ShipZuusctandLezte.Warping;
			var WarpingLezteZaitMili = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.WarpingLezteZaitMili;

			var AusGbsMengeAbovemainMessage = (null == AusScnapscusAuswertungZuusctand) ? null : AusScnapscusAuswertungZuusctand.MengeAbovemainMessage;

			var ShipUi = (null == AusScnapscusAuswertungZuusctand) ? null : AusScnapscusAuswertungZuusctand.ShipUi;

			var MengeZuInfoPanelTypeButtonUndInfoPanel = (null == AusScnapscusAuswertungZuusctand) ? null :
				AusScnapscusAuswertungZuusctand.MengeZuInfoPanelTypeButtonUndInfoPanel();

			if (null != AusGbsMengeAbovemainMessage)
			{
				if (null == ListeAusGbsAbovemainMessage)
				{
					InternZuusctand.InternListeAusGbsAbovemainMessageMitZait = ListeAusGbsAbovemainMessage = new Queue<SictVerlaufBeginUndEndeRef<VonSensor.Message>>();
				}

				foreach (var AusGbsAbovemainMessage in AusGbsMengeAbovemainMessage)
				{
					var MessageBeraitsEnthalte = false;

					if (ListeAusGbsAbovemainMessage.Any((Kandidaat) =>
					{
						if (Kandidaat.EndeZait.HasValue)
						{
							return false;
						}

						if (null == Kandidaat.Wert)
						{
							return false;
						}

						return string.Equals(Kandidaat.Wert.LabelText, AusGbsAbovemainMessage.LabelText, StringComparison.InvariantCulture);
					}))
					{
						MessageBeraitsEnthalte = true;
					}

					if (MessageBeraitsEnthalte)
					{
						continue;
					}

					ListeAusGbsAbovemainMessage.Enqueue(new SictVerlaufBeginUndEndeRef<VonSensor.Message>(NuzerZaitMili, null, AusGbsAbovemainMessage));
				}
			}

			if (null == GbsZuusctand)
			{
				InternZuusctand.Gbs = GbsZuusctand = new SictGbsZuusctand();
			}

			var VorherListeMenu = GbsZuusctand.ListeMenuNocOfeBerecne();

			GbsZuusctand.Aktualisiire(InternZuusctand, AusScnapscusAuswertungZuusctand);

			var ListeMenu = GbsZuusctand.ListeMenuNocOfeBerecne();

			var GbsMengeMenuMitBeginZait = InternZuusctand.GbsListeMenuNocOfeMitBeginZaitBerecne() ?? new SictWertMitZait<VonSensor.Menu>[0];

			if (Bib3.Extension.NullOderLeer(ListeMenu) && 0 < ExtractFromOldAssembly.Bib3.Extension.CountNullable(VorherListeMenu))
			{
				//	MengeVersuucMenuEntryKlikErfolg	werd zurzait waiter unte berecnet
			}

			if (null == FittingUndShipZuusctand)
			{
				InternZuusctand.FittingUndShipZuusctand = FittingUndShipZuusctand = new SictShipZuusctandMitFitting();
			}

			FittingUndShipZuusctand.Aktualisiire(InternZuusctand, AusScnapscusAuswertungZuusctand);

			if (null == OverviewUndTarget)
			{
				InternZuusctand.OverviewUndTarget = OverviewUndTarget = new SictOverviewUndTargetZuusctand();
			}

			OverviewUndTarget.AktualisiireZuusctandAusScnapscus(InternZuusctand, AusScnapscusAuswertungZuusctand);

			if (null == AgentUndMission)
			{
				InternZuusctand.AgentUndMission = AgentUndMission = new SictAgentUndMissionZuusctand();
			}

			AgentUndMission.Aktualisiire(InternZuusctand, AusScnapscusAuswertungZuusctand);

			var AgentUndMissionAnforderungActiveShipCargoLeereLezteZaitMili = AgentUndMission.AnforderungActiveShipCargoLeereLezteZaitMili;

			InternZuusctand.AnforderungActiveShipCargoGeneralLeereLezteZaitMili =
				Bib3.Glob.Max(InternZuusctand.AnforderungActiveShipCargoGeneralLeereLezteZaitMili, AgentUndMissionAnforderungActiveShipCargoLeereLezteZaitMili);

			if (null == AutoMine)
			{
				InternZuusctand.AutoMine = AutoMine = new SictAutoMine();
			}

			AutoMine.Aktualisiire(InternZuusctand, AusScnapscusAuswertungZuusctand);

			if (null == InRaumAktioonUndGefect)
			{
				InternZuusctand.InRaumAktioonUndGefect = InRaumAktioonUndGefect = new SictInRaumAktioonUndGefect();
			}

			InRaumAktioonUndGefect.Aktualisiire(InternZuusctand);

			try
			{
				var FittingAnnaameSlotAfterburner = FittingUndShipZuusctand.AnnaameModuleAfterburner;

				if (null == AusScnapscusAuswertungZuusctand)
				{
					return;
				}

				var MengeWindowInventory = AusScnapscusAuswertungZuusctand.MengeWindowInventory;

				if (null != MengeWindowInventory)
				{
					//	Aktualis AnnaameActiveShipCargoLeerLezteZaitMili

					foreach (var WindowInventory in MengeWindowInventory)
					{
						if (null == WindowInventory)
						{
							continue;
						}

						if (null == WindowInventory.AuswaalReczObjektName ||
							null == WindowInventory.LinxTreeEntryActiveShip ||
							null == WindowInventory.AuswaalReczCapacity)
						{
							continue;
						}

						if (string.Equals(WindowInventory.AuswaalReczObjektName, WindowInventory.LinxTreeEntryActiveShip.LabelText))
						{
							if (WindowInventory.AuswaalReczCapacity.UsedMikro <= 0)
							{
								InternZuusctand.AnnaameActiveShipCargoGeneralLeerLezteZaitMili = NuzerZaitMili;
							}
						}
					}
				}

				if (null != ListeAusGbsAbovemainMessage)
				{
					foreach (var AusGbsAbovemainMessage in ListeAusGbsAbovemainMessage)
					{
						if (AusGbsAbovemainMessage.EndeZait.HasValue)
						{
							continue;
						}

						var MessageNocSictbar = false;

						if (null != AusGbsAbovemainMessage.Wert)
						{
							if (null != AusGbsMengeAbovemainMessage)
							{
								MessageNocSictbar =
									AusGbsMengeAbovemainMessage
									.Any((MessageSictbar) => string.Equals(AusGbsAbovemainMessage.Wert.LabelText, MessageSictbar.LabelText));
							}
						}

						if (!MessageNocSictbar)
						{
							AusGbsAbovemainMessage.EndeZait = NuzerZaitMili;
						}
					}

					Bib3.Extension.ListeKürzeBegin(ListeAusGbsAbovemainMessage, 10);
				}
			}
			finally
			{
			}
		}

		public void AktualisiireTailMengeAufgaabeVonNuzerMeldungWirkungErfolg()
		{
			var ZaitMili = this.NuzerZaitMili;

			var MengeVonNuzerMeldungWirkungErfolg = MengeVonNuzerMeldungWirkungErfolgZuZaitBerecne();

			var ScritLezteWirkungMengeAufgaabePfaadZuBlatMitGrupePrioNaame = this.ScritLezteWirkungMengeAufgaabePfaadZuBlatMitGrupePrioNaame;

			if (null == ScritLezteWirkungMengeAufgaabePfaadZuBlatMitGrupePrioNaame)
			{
				return;
			}

			foreach (var WirkungAufgaabePfaadZuBlatMitGrupePrioNaame in ScritLezteWirkungMengeAufgaabePfaadZuBlatMitGrupePrioNaame)
			{
				var WirkungAufgaabePfaadZuBlat = WirkungAufgaabePfaadZuBlatMitGrupePrioNaame.Key;

				if (null == WirkungAufgaabePfaadZuBlat)
				{
					continue;
				}

				foreach (var PfaadAst in WirkungAufgaabePfaadZuBlat.Reversed())
				{
					/*
					 * 2014.03.28
					 * 
					 * Änderung Berecnung AbsclusTailWirkungZaitSeze:
					 * 
					 * Aine Aufgaabe isc dan Abgesclose Tail Wirkung wen:
					 * Alle Tailaufgaabe Transitiiv sind Volsctändig zerleegt
					 * Alle Tailaufgaabe Transitiiv welce Vorsclaag Wirkung enthalte sind durc Nuzer umgesezt.
					 * 
					var PfaadAstMengeKomponente = PfaadAst.MengeKomponenteBerecne();

					var PfaadAstMengeKomponenteZerleegungVolsctändig =
						(null == PfaadAstMengeKomponente) ? true :
						PfaadAstMengeKomponente.All((PfaadAstKomponente) => true == PfaadAstKomponente.ZerleegungVolsctändig);

					var PfaadAstMengeBlat = PfaadAst.MengeBlatBerecne();

					if (Optimat.Glob.NullOderLeer(PfaadAstMengeBlat))
					{
						continue;
					}

					foreach (var PfaadAstBlat in PfaadAstMengeBlat)
					{
						if (null != MengeVonNuzerMeldungWirkungErfolg)
						{
							if (MengeVonNuzerMeldungWirkungErfolg.Any((VonNuzerMeldungErfolgZuZait) => VonNuzerMeldungErfolgZuZait.Wert == PfaadAstBlat.Bezaicner))
							{
								PfaadAstBlat.AbsclusTailWirkungZaitSeze(ZaitMili);
							}
						}
					}

					if (true	== PfaadAst.ZerleegungVolsctändig	&&
						PfaadAstMengeKomponenteZerleegungVolsctändig)
					{
						if (PfaadAstMengeBlat.All((PfaadAstBlat) => PfaadAstBlat.AbsclusTailWirkungZait.HasValue))
						{
							PfaadAst.AbsclusTailWirkungZaitSeze(ZaitMili);
						}
					}
					 * */

					var PfaadAstAufgaabeParam = PfaadAst.AufgaabeParam as AufgaabeParamAndere;

					var PfaadAstNaacNuzerVorsclaagWirkung =
						(null == PfaadAstAufgaabeParam) ? null :
						PfaadAstAufgaabeParam.NaacNuzerVorsclaagWirkung;

					if (null == PfaadAstNaacNuzerVorsclaagWirkung)
					{
						//	Da der Pfaad vom Blat aus durclaufe werd isc hiir verglaic mit nääxt Eebene ausraicend da di waiter verzwaigte Ast beraits in vorheerige Iteratioon bearbaitet wurde.

						/*
						 * 2014.03.29
						 * 
						var PfaadAstMengeKomponente = PfaadAst.MengeKomponenteBerecne();
						 * */

						var PfaadAstZerleegungErgeebnisLezteZuZait = PfaadAst.ZerleegungErgeebnisLezteZuZait;

						var PfaadAstMengeKomponenteAktiiv =
							(null == PfaadAstZerleegungErgeebnisLezteZuZait.Wert) ? null :
							PfaadAstZerleegungErgeebnisLezteZuZait.Wert.MengeKomponente;

						var PfaadAstZerleegungVolsctändig =
							(null == PfaadAstZerleegungErgeebnisLezteZuZait.Wert) ? null : PfaadAstZerleegungErgeebnisLezteZuZait.Wert.ZerleegungVolsctändig;

						var PfaadAstAbsclusTailWirkung = false;

						if (null == PfaadAstMengeKomponenteAktiiv)
						{
							PfaadAstAbsclusTailWirkung = true;
						}
						else
						{
							if (true == PfaadAstZerleegungVolsctändig &&
								PfaadAstMengeKomponenteAktiiv.All((Komponente) => true == Komponente.ZerleegungVolsctändig && Komponente.AbsclusTailWirkungZait.HasValue))
							{
								PfaadAstAbsclusTailWirkung = true;
							}
						}

						if (PfaadAstAbsclusTailWirkung)
						{
							PfaadAst.AbsclusTailWirkungZaitSeze(ZaitMili);
						}
					}
					else
					{
						if (null != PfaadAstNaacNuzerVorsclaagWirkung &&
							null != MengeVonNuzerMeldungWirkungErfolg &&
							true == PfaadAst.ZerleegungVolsctändig)
						{
							var VonNuzerMeldungWirkungErfolg =
								MengeVonNuzerMeldungWirkungErfolg.FirstOrDefault((KandidaatVonNuzerMeldungWirkungErfolg) => KandidaatVonNuzerMeldungWirkungErfolg.Wert == PfaadAst.Bezaicner);

							if (VonNuzerMeldungWirkungErfolg.Wert == PfaadAst.Bezaicner)
							{
								PfaadAst.AbsclusTailWirkungZaitSeze(VonNuzerMeldungWirkungErfolg.Zait);
							}
						}
					}
				}
			}
		}

		List<Int64> MengeNaacProcessWirkungVerarbaitetIdent = new List<Int64>();

		public void AktualisiireTailAingangNaacProcessWirkung()
		{
			var VonNuzerMeldungZuusctand = this.VonNuzerMeldungZuusctand;

			if (null == VonNuzerMeldungZuusctand)
			{
				return;
			}

			var ListeNaacProcessWirkung = VonNuzerMeldungZuusctand.ListeNaacProcessWirkung;

			if (null == ListeNaacProcessWirkung)
			{
				return;
			}

			foreach (var NaacProcessWirkung in ListeNaacProcessWirkung)
			{
				if (null == NaacProcessWirkung)
				{
					continue;
				}

				var VorsclaagWirkungIdent = NaacProcessWirkung.VorsclaagWirkungIdent;

				if (!VorsclaagWirkungIdent.HasValue)
				{
					continue;
				}

				if (MengeNaacProcessWirkungVerarbaitetIdent.Contains(VorsclaagWirkungIdent.Value))
				{
					continue;
				}

				MengeNaacProcessWirkungVerarbaitetIdent.Add(VorsclaagWirkungIdent.Value);

				AingangNaacProcessWirkung(NuzerZaitMili, NaacProcessWirkung);
			}

			var MengeZuEntferne =
				MengeNaacProcessWirkungVerarbaitetIdent
				.Where((KandidaatWirkungIdent) => !ListeNaacProcessWirkung.Any((KandidaatWirkung) => KandidaatWirkungIdent == KandidaatWirkung.VorsclaagWirkungIdent))
				.ToArray();

			MengeNaacProcessWirkungVerarbaitetIdent.RemoveAll((Kandidaat) => MengeZuEntferne.Contains(Kandidaat));
		}

		public void AingangNaacProcessWirkung(
			Int64 Zait,
			SictNaacProcessWirkung Wirkung)
		{
			if (null == Wirkung)
			{
				return;
			}

			var VorsclaagWirkungIdent = Wirkung.VorsclaagWirkungIdent;

			if (!VorsclaagWirkungIdent.HasValue)
			{
				return;
			}

			var ListeZuZaitVonNuzerMeldungNaacZiilProcessWirkung = this.ListeZuZaitVonNuzerMeldungNaacProcessWirkung;

			if (null == ListeZuZaitVonNuzerMeldungNaacZiilProcessWirkung)
			{
				this.ListeZuZaitVonNuzerMeldungNaacProcessWirkung =
					ListeZuZaitVonNuzerMeldungNaacZiilProcessWirkung = new List<SictWertMitZait<Optimat.EveOnline.SictNaacProcessWirkung>>();
			}

			if (!ListeZuZaitVonNuzerMeldungNaacZiilProcessWirkung
				.Any((Kandidaat) => ((null == Kandidaat.Wert) ? null : Kandidaat.Wert.VorsclaagWirkungIdent) == VorsclaagWirkungIdent))
			{
				Bib3.Glob.InListeOrdnetFüügeAin(
					ListeZuZaitVonNuzerMeldungNaacZiilProcessWirkung,
					(Element) => Element.Zait,
					new SictWertMitZait<Optimat.EveOnline.SictNaacProcessWirkung>(Zait, Wirkung),
					false);

				Bib3.Extension.ListeKürzeBegin(ListeZuZaitVonNuzerMeldungNaacZiilProcessWirkung, 10);
			}

			var MengeVonNuzerMeldungWirkungErfolgZuZait = this.MengeVonNuzerMeldungWirkungErfolgZuZait;

			if (true == Wirkung.Erfolg && VorsclaagWirkungIdent.HasValue)
			{
				if (null == MengeVonNuzerMeldungWirkungErfolgZuZait)
				{
					this.MengeVonNuzerMeldungWirkungErfolgZuZait = MengeVonNuzerMeldungWirkungErfolgZuZait =
						new List<SictWertMitZait<Int64>>();
				}

				if (!MengeVonNuzerMeldungWirkungErfolgZuZait.Any((t) => t.Wert == VorsclaagWirkungIdent))
				{
					MengeVonNuzerMeldungWirkungErfolgZuZait.Add(new SictWertMitZait<Int64>(Zait, VorsclaagWirkungIdent.Value));
					Bib3.Extension.ListeKürzeBegin(MengeVonNuzerMeldungWirkungErfolgZuZait, 10);
				}
			}
		}

		/// <summary>
		/// Aine Aufgaabe werd als Abgesclose Tail Wirkung klasifiziirt wen in lezte Optimat Scrit ale naac Nuzer Blat (Vorsclaag Wirkung) von Nuzer als erfolgraic durcgefüürt gemeldet wurde.
		/// 
		/// Noc zu erwaitere: Fal MenuPfaad Auswaal: hiir werde meerere Blat über meerere Optimat Scrit vertailt durcgefüürt.
		/// </summary>
		public void AktualisiireTailMengeAufgaabeAbgescloseTailWirkung()
		{
			var ZaitMili = this.NuzerZaitMili;

			var MengeAufgaabeZuusctand = this.MengeAufgaabeZuusctand;

			if (null == MengeAufgaabeZuusctand)
			{
				this.MengeAufgaabeZuusctand = MengeAufgaabeZuusctand = new List<SictAufgaabeZuusctand>();
			}

			var ScritLezteWirkungMengeAufgaabePfaadZuBlatMitGrupePrioNaame = this.ScritLezteWirkungMengeAufgaabePfaadZuBlatMitGrupePrioNaame;

			if (null == ScritLezteWirkungMengeAufgaabePfaadZuBlatMitGrupePrioNaame)
			{
				return;
			}

			foreach (var WirkungAufgaabePfaadZuBlatMitGrupePrioNaame in ScritLezteWirkungMengeAufgaabePfaadZuBlatMitGrupePrioNaame)
			{
				var WirkungAufgaabePfaadZuBlat = WirkungAufgaabePfaadZuBlatMitGrupePrioNaame.Key;

				if (null == WirkungAufgaabePfaadZuBlat)
				{
					continue;
				}

				foreach (var PfaadAst in WirkungAufgaabePfaadZuBlat.Reverse())
				{
					if (!(true == PfaadAst.ZerleegungVolsctändig))
					{
						break;
					}

					if (MengeAufgaabeZuusctand.Contains(PfaadAst))
					{
						//	Tailaufgaabe wurde beraits naac Ziilmenge kopiirt
						continue;
					}

					/*
					 * 2014.03.23
					 * 
					 * Ersaz durc Berecnung PfaadAst.AbsclusTailWirkungZait in Funktioon AktualisiireTailMengeAufgaabeVonNuzerMeldungWirkungErfolg
					 * 
					var PfaadAstMengeBlat = PfaadAst.MengeBlatBerecne();

					if (Optimat.Glob.NullOderLeer(PfaadAstMengeBlat))
					{
						continue;
					}

					if (!(PfaadAstMengeBlat.All((AufgaabeBlat) => !AufgaabeBlat.IstBlatNaacNuzerVorsclaagWirkung() ||
						(AufgaabeBlat.AbsclusTailWirkungZait.HasValue	&& (true	== AufgaabeBlat.ZerleegungVolsctändig)))))
					{
						break;
					}
					 * */

					if (!PfaadAst.AbsclusTailWirkungZait.HasValue)
					{
						continue;
					}

					MengeAufgaabeZuusctand.Add(PfaadAst);
				}
			}

			//	Ale Ainträäge entferne welce älter sin als aine Minuute
			var AufgaabeBeginAlterScrankeMax = 60;

			Bib3.Extension.ListeKürzeBegin(MengeAufgaabeZuusctand, (Aufgaabe) => !((ZaitMili - Aufgaabe.BeginZait) / 1000 < AufgaabeBeginAlterScrankeMax));
		}

		public void AktualisiireTailMengeModuleUmscaltFraigaabe()
		{
			SictShipUiModuleReprZuusctand[] MengeModuleUmscaltFraigaabe = null;

			try
			{
				var MengeAufgaabeZuusctand = this.MengeAufgaabeZuusctand;

				var FittingUndShipZuusctand = this.FittingUndShipZuusctand;

				if (null == FittingUndShipZuusctand)
				{
					return;
				}

				var MengeModuleRepr = FittingUndShipZuusctand.MengeModuleRepr;

				if (null == MengeModuleRepr)
				{
					return;
				}

				var MengeModuleUmscaltFraigaabeNict = new List<SictShipUiModuleReprZuusctand>();

				if (null != MengeAufgaabeZuusctand)
				{
					foreach (var Aufgaabe in MengeAufgaabeZuusctand)
					{
						if (null == Aufgaabe)
						{
							continue;
						}

						var AufgaabeParam = Aufgaabe.AufgaabeParam as AufgaabeParamAndere;

						if (null == AufgaabeParam)
						{
							continue;
						}

						var AbsclusTailWirkungAlterMili = NuzerZaitMili - Aufgaabe.AbsclusTailWirkungZait;

						if (!(AbsclusTailWirkungAlterMili < 4444))
						{
							continue;
						}

						var AufgaabeModuleScalteUm = AufgaabeParam.ModuleScalteUm;

						if (null == AufgaabeModuleScalteUm)
						{
							continue;
						}

						MengeModuleUmscaltFraigaabeNict.Add(AufgaabeModuleScalteUm);
					}
				}

				MengeModuleUmscaltFraigaabe =
					MengeModuleRepr.Except(MengeModuleUmscaltFraigaabeNict).ToArray();
			}
			finally
			{
				this.MengeModuleUmscaltFraigaabe = MengeModuleUmscaltFraigaabe;
			}
		}

		public void AktualisiireTailTransportMenuEntryAuswaalErfolgNaacGbsMenuKaskaade()
		{
			var NuzerZaitMili = this.NuzerZaitMili;

			/*
			 * 2014.07.29
			 * 
			 * Ersaz durc Verwendung MengeVersuucMenuEntryKlikErfolgBerecne.
			 * 
			var MengeAufgaabeZuusctand = this.MengeAufgaabeZuusctand;

			if (null == MengeAufgaabeZuusctand)
			{
				return;
			}
			 * */

			var Gbs = this.Gbs;

			if (null == Gbs)
			{
				return;
			}

			var MenuKaskaadeLezteNocOfe = Gbs.MenuKaskaadeLezteNocOfe;

			if (null == MenuKaskaadeLezteNocOfe)
			{
				return;
			}

			if (MenuKaskaadeLezteNocOfe.EndeZait.HasValue)
			{
				return;
			}

			var MenuKaskaadeLezteListeMenu = MenuKaskaadeLezteNocOfe.ListeMenu;

			if (null == MenuKaskaadeLezteListeMenu)
			{
				return;
			}

			var AufgaabeMenuEntryAbgescloseTailWirkungLezteMitZait = this.AufgaabeMenuEntryAbgescloseTailWirkungLezteMitZait;

			{
				var MenuEntryAktiviirt = AufgaabeMenuEntryAbgescloseTailWirkungLezteMitZait.Wert;

				if (null == MenuEntryAktiviirt)
				{
					return;
				}

				var MenuEntryAktiviirtHerkunftAdrese = (Int64?)MenuEntryAktiviirt.Ident;

				if (!MenuEntryAktiviirtHerkunftAdrese.HasValue)
				{
					return;
				}

				foreach (var MenuZuusctand in MenuKaskaadeLezteListeMenu)
				{
					if (null == MenuZuusctand)
					{
						continue;
					}

					var MenuZuusctandListeEntry = MenuZuusctand.ListeEntryBerecne();

					if (null == MenuZuusctandListeEntry)
					{
						continue;
					}

					foreach (var MenuEntry in MenuZuusctandListeEntry)
					{
						if (null == MenuEntry)
						{
							continue;
						}

						if (MenuEntry.Ident == MenuEntryAktiviirtHerkunftAdrese)
						{
							MenuZuusctand.MenuEntryAktiviirtLezteMitZaitSeze(new SictWertMitZait<VonSensor.MenuEntry>(
								AufgaabeMenuEntryAbgescloseTailWirkungLezteMitZait.Zait, MenuEntryAktiviirt));
							break;
						}
					}
				}
			}
		}

		public void AktualisiireTailListeInventoryItemTransportMitZait()
		{
			var MengeAufgaabeZuusctand = this.MengeAufgaabeZuusctand;

			if (null == MengeAufgaabeZuusctand)
			{
				return;
			}

			var Gbs = this.Gbs;

			var ListeInventoryItemTransportMitZait = this.ListeInventoryItemTransportMitZait;

			foreach (var Aufgaabe in MengeAufgaabeZuusctand)
			{
				//	Berecnung ListeInventoryItemTransportMitZait

				var AufgaabeAbsclusTailWirkungZaitMiliNulbar = Aufgaabe.AbsclusTailWirkungZait;

				if (!AufgaabeAbsclusTailWirkungZaitMiliNulbar.HasValue)
				{
					continue;
				}

				var AufgaabeAbsclusTailWirkungZaitMili = AufgaabeAbsclusTailWirkungZaitMiliNulbar.Value;

				var AufgaabeAbsclusTailWirkungAlterMili = NuzerZaitMili - AufgaabeAbsclusTailWirkungZaitMili;

				if (!(AufgaabeAbsclusTailWirkungAlterMili < 4444))
				{
					continue;
				}

				var AufgaabeParam = Aufgaabe.AufgaabeParam as AufgaabeParamAndere;

				if (null == AufgaabeParam)
				{
					continue;
				}

				var AufgaabeParamInventoryItemTransport = AufgaabeParam.InventoryItemTransport;

				if (null == AufgaabeParamInventoryItemTransport)
				{
					continue;
				}

				if (null != ListeInventoryItemTransportMitZait)
				{
					var ListeInventoryItemTransportMitZaitLezte = ListeInventoryItemTransportMitZait.LastOrDefault();

					if (!(ListeInventoryItemTransportMitZaitLezte.Zait < AufgaabeAbsclusTailWirkungZaitMili))
					{
						continue;
					}
				}

				var VorherScnapscusWindowInventory = AufgaabeParamInventoryItemTransport.KweleWindowInventory;

				if (null == VorherScnapscusWindowInventory)
				{
					continue;
				}

				var VorherScnapscusWindowInventoryAuswaalReczListeItem = VorherScnapscusWindowInventory.AuswaalReczListeItem;

				if (null == VorherScnapscusWindowInventoryAuswaalReczListeItem)
				{
					continue;
				}

				var WindowInventoryHerkunftAdreseNulbar = (Int64?)VorherScnapscusWindowInventory.Ident;

				if (!WindowInventoryHerkunftAdreseNulbar.HasValue)
				{
					continue;
				}

				var WindowInventoryHerkunftAdrese = WindowInventoryHerkunftAdreseNulbar.Value;

				var WindowInventory = (null == Gbs) ? null : Gbs.ZuHerkunftAdreseWindow(WindowInventoryHerkunftAdrese);

				if (null == WindowInventory)
				{
					continue;
				}

				var NaacherScnapscusWindow = WindowInventory.AingangScnapscusTailObjektIdentLezteBerecne();

				if (null == NaacherScnapscusWindow)
				{
					continue;
				}

				var NaacherScnapscusWindowInventory = NaacherScnapscusWindow as VonSensor.WindowInventoryPrimary;

				if (null == NaacherScnapscusWindowInventory)
				{
					continue;
				}

				var NaacherScnapscusWindowInventoryLinxTreeListeEntry = NaacherScnapscusWindowInventory.LinxTreeListeEntry;
				var NaacherScnapscusWindowInventoryAuswaalReczListeItem = NaacherScnapscusWindowInventory.AuswaalReczListeItem;

				if (Bib3.Extension.NullOderLeer(NaacherScnapscusWindowInventoryLinxTreeListeEntry))
				{
					continue;
				}

				var VorherAuswaalReczObjektPfaadListeAst = VorherScnapscusWindowInventory.AuswaalReczObjektPfaadListeAst;
				var NaacherAuswaalReczObjektPfaadListeAst = NaacherScnapscusWindowInventory.AuswaalReczObjektPfaadListeAst;

				if (null == VorherAuswaalReczObjektPfaadListeAst || null == NaacherAuswaalReczObjektPfaadListeAst)
				{
					continue;
				}

				VonSensor.InventoryItem[] AnnaameMengeItemTransportiirt = null;

				if (Bib3.Glob.SequenceEqualPerObjectEquals(VorherAuswaalReczObjektPfaadListeAst, NaacherAuswaalReczObjektPfaadListeAst))
				{
					//	ausgewäälte Objekt isc glaic gebliibe, Diferenz der Items berecne.

					//	!!!!	Noc zu Ergänze: Berüksictigung der Änderung der Quantity aines der abgebildete Item.

					AnnaameMengeItemTransportiirt =
						VorherScnapscusWindowInventoryAuswaalReczListeItem
						.Where((KandidaatItemTransportiirt) =>
						{
							if (null == KandidaatItemTransportiirt)
							{
								return false;
							}

							if (null == NaacherScnapscusWindowInventoryAuswaalReczListeItem)
							{
								return true;
							}

							return !NaacherScnapscusWindowInventoryAuswaalReczListeItem.Any(
								(KandidaatItemReprInNaacher) => string.Equals(KandidaatItemReprInNaacher.Name, KandidaatItemTransportiirt.Name));
						})
						.ToArrayNullable();
				}
				else
				{
					//	ausgewäälte Objekt wurde geändert.

					//	Fals das zvor ausgewäälte Objekt auc in der TreeView Linx nit meer zu finde isc werd davon ausgegange das Transport erfolgraic war.
					//	Diises Verhalte des EVE Client isc zu beoobacte z.B. CargoContainer welcer zerfält (und aus Inventory verscwindet) sobald diiser Leer isc.

					var VorherAuswaalObjektMengeKandidaatReprInNaacherScnapscus =
						VonSensor.WindowInventoryPrimary.ZuAuswaalReczBerecneMengeKandidaatLinxTreeEntry(
						VorherAuswaalReczObjektPfaadListeAst,
						NaacherScnapscusWindowInventoryLinxTreeListeEntry);

					if (Bib3.Extension.NullOderLeer(VorherAuswaalObjektMengeKandidaatReprInNaacherScnapscus))
					{
						AnnaameMengeItemTransportiirt = VorherScnapscusWindowInventory.AuswaalReczListeItem;
					}
				}

				if (!Bib3.Extension.NullOderLeer(AnnaameMengeItemTransportiirt))
				{
					if (null == ListeInventoryItemTransportMitZait)
					{
						this.ListeInventoryItemTransportMitZait = ListeInventoryItemTransportMitZait =
							new List<SictWertMitZait<SictInventoryItemTransport>>();
					}

					var Transport = new SictInventoryItemTransport(
						AufgaabeParamInventoryItemTransport.KweleWindowInventory,
						AufgaabeParamInventoryItemTransport.ZiilObjektTreeViewEntry,
						AnnaameMengeItemTransportiirt);

					ListeInventoryItemTransportMitZait.Add(new SictWertMitZait<SictInventoryItemTransport>(
						AufgaabeAbsclusTailWirkungZaitMili,
						Transport));

					Bib3.Extension.ListeKürzeBegin(ListeInventoryItemTransportMitZait, 4);
				}
			}
		}

		public void AktualisiireTailMengeAufgaabeZuusctand()
		{
			var FittingUndShipZuusctand = this.FittingUndShipZuusctand;

			var ListeAusShipUIIndicationMitZaitLezte =
				(null == FittingUndShipZuusctand) ? null :
				ExtractFromOldAssembly.Bib3.Extension.LastOrDefaultNullable(FittingUndShipZuusctand.ListeAusShipUIIndicationMitZait);

			if (null == MengeAufgaabeZuusctand)
			{
				return;
			}

			foreach (var Aufgaabe in MengeAufgaabeZuusctand)
			{
				if (null == Aufgaabe)
				{
					continue;
				}

				var AufgaabeParam = Aufgaabe.AufgaabeParam;
				var AufgaabeParamAndere = AufgaabeParam as AufgaabeParamAndere;

				if (null == AufgaabeParam)
				{
					continue;
				}

				var AufgaabeAbsclusTailWirkungZait = Aufgaabe.AbsclusTailWirkungZait;

				if (!AufgaabeAbsclusTailWirkungZait.HasValue)
				{
					continue;
				}

				var AufgaabeParamManööverAuszufüüreTyp = (null == AufgaabeParamAndere) ? null : AufgaabeParamAndere.ManööverAuszufüüreTyp;

				if (AufgaabeParamManööverAuszufüüreTyp.HasValue && null != ListeAusShipUIIndicationMitZaitLezte)
				{
					//	Berecnung Aufgaabe.ManööverErgeebnis

					var ListeAusShipUIIndicationLezte = ListeAusShipUIIndicationMitZaitLezte.Wert;

					if (null != ListeAusShipUIIndicationLezte)
					{
						var VonWirkungAbsclusBisMesungZaitDistanzScrankeMax = 4000;

						var VonWirkungAbsclusBisMesungZaitDistanz = ListeAusShipUIIndicationMitZaitLezte.BeginZait - AufgaabeAbsclusTailWirkungZait;

						if ((VonWirkungAbsclusBisMesungZaitDistanz <= VonWirkungAbsclusBisMesungZaitDistanzScrankeMax) &&
							ListeAusShipUIIndicationLezte.ManöverTyp == AufgaabeParamManööverAuszufüüreTyp)
						{
							Aufgaabe.ManööverErgeebnisSeze(ListeAusShipUIIndicationMitZaitLezte);
						}
					}
				}
			}

			var MengeAufgaabeZuusctandAbgescloseTailWirkung =
				MengeAufgaabeZuusctand
				.Where((KandidaatAufgaabe) => KandidaatAufgaabe.AbsclusTailWirkungZait.HasValue)
				.OrderByDescending((KandidaatAufgaabe) => KandidaatAufgaabe.AbsclusTailWirkungZait)
				.ToArray();

			foreach (var Aufgaabe in MengeAufgaabeZuusctandAbgescloseTailWirkung)
			{
				if (null == Aufgaabe)
				{
					continue;
				}

				var AufgaabeParam = Aufgaabe.AufgaabeParam as AufgaabeParamAndere;

				if (null == AufgaabeParam)
				{
					continue;
				}

				var AufgaabeParamMenuEntry = AufgaabeParam.MenuEntry;

				if (null == AufgaabeParamMenuEntry)
				{
					continue;
				}

				AufgaabeMenuEntryAbgescloseTailWirkungLezteMitZait = new SictWertMitZait<VonSensor.MenuEntry>(Aufgaabe.AbsclusTailWirkungZait.Value, AufgaabeParamMenuEntry);
				break;
			}
		}

		static void AktualisiireSolarSystem(
			SictSolarSystem SolarSystem,
			ISictAutomatZuusctand Automaat)
		{
			if (null == SolarSystem)
			{
				return;
			}

			if (null == Automaat)
			{
				return;
			}

			var MenuKaskaadeLezte = Automaat.MenuKaskaadeLezte();

			if (null == MenuKaskaadeLezte)
			{
				return;
			}

			var ListeMenu = MenuKaskaadeLezte.ListeMenu;

			if (null == ListeMenu)
			{
				return;
			}

			var ListeMenu0 = ListeMenu.FirstOrDefault();
			var ListeMenu1 = ListeMenu.ElementAtOrDefault(1);

			if (null == ListeMenu0 || null == ListeMenu1)
			{
				return;
			}

			var EntryStations =
				ListeMenu0.ListeEntryBerecne().FirstOrDefaultNullable((Kandidaat) => Regex.Match(Kandidaat.Bescriftung ?? "", "Station", RegexOptions.IgnoreCase).Success);

			var EntryStargates =
				ListeMenu0.ListeEntryBerecne().FirstOrDefaultNullable((Kandidaat) => Regex.Match(Kandidaat.Bescriftung ?? "", "Stargate", RegexOptions.IgnoreCase).Success);

			var EntryAsteroidBelts =
				ListeMenu0.ListeEntryBerecne().FirstOrDefaultNullable((Kandidaat) => Regex.Match(Kandidaat.Bescriftung ?? "", "Asteroid\\s*Belt", RegexOptions.IgnoreCase).Success);

			var EntryPlanets =
				ListeMenu0.ListeEntryBerecne().FirstOrDefaultNullable((Kandidaat) => Regex.Match(Kandidaat.Bescriftung ?? "", "Planet", RegexOptions.IgnoreCase).Success);

			var ListeEntryIndikatorSystemInfo =
				new VonSensor.MenuEntry[] { EntryStations, EntryStargates, EntryAsteroidBelts, EntryPlanets };

			if (!(1 < ListeEntryIndikatorSystemInfo.CountNullable((Kandidaat) => null != Kandidaat)))
			{
				return;
			}

			if (null == EntryAsteroidBelts)
			{
				SolarSystem.MengeAsteroidBelt.Clear();
			}
			else
			{
				if (ListeMenu0.AnnaameMenuEntryAktiiv == EntryAsteroidBelts)
				{
					var MengeKandidaatAsteroidBeltBescriftung =
						ListeMenu1.ListeEntryBerecne()
						.SelectNullable((Kandidaat) => ExtractFromOldAssembly.Bib3.Glob.TrimNullable(Kandidaat.Bescriftung))
						.WhereNullable((KandidaatBescriftung) => !KandidaatBescriftung.NullOderLeer())
						.ToArrayNullable();

					var MengeAsteroidBeltBescriftung =
						MengeKandidaatAsteroidBeltBescriftung
						.WhereNullable((KandidaatBescriftung) => Regex.Match(KandidaatBescriftung ?? "", "Asteroid\\s*Belt", RegexOptions.IgnoreCase).Success)
						.ToArrayNullable();

					if (0 < MengeAsteroidBeltBescriftung.CountNullable())
					{
						Bib3.Glob.PropagiireListeRepräsentatioon(
							MengeAsteroidBeltBescriftung,
							SolarSystem.MengeAsteroidBelt as IList<SictAsteroidBelt>,
							(AsteroidBeltBescriftung) => new SictAsteroidBelt(AsteroidBeltBescriftung),
							(KandidaatRepr, AsteroidBeltBescriftung) => SictAsteroidBelt.Glaicwertig(KandidaatRepr, AsteroidBeltBescriftung),
							null,
							false);
					}
				}
			}
		}

		void AktualisiireTailListeLocationNearest()
		{
			var Scnapscus = this.ListeScnapscusLezteAuswertungErgeebnisNaacSimu;

			if (null == Scnapscus)
			{
				return;
			}

			var CurrentLocationInfo = Scnapscus.CurrentLocationInfo();

			if (null == CurrentLocationInfo)
			{
				return;
			}

			var ScnapscusNearestName = ExtractFromOldAssembly.Bib3.Glob.TrimNullable(CurrentLocationInfo.NearestName);

			if (ScnapscusNearestName.NullOderLeer())
			{
				return;
			}

			if (null == InternListeLocationNearest)
			{
				InternListeLocationNearest = new List<SictVerlaufBeginUndEndeRef<string>>();
			}

			var BisherLocationVerlauf = InternListeLocationNearest.LastOrDefault();

			var VerlaufNoi = new SictVerlaufBeginUndEndeRef<string>(NuzerZaitMili, null, ScnapscusNearestName);

			if (null == BisherLocationVerlauf)
			{
				InternListeLocationNearest.Add(VerlaufNoi);
			}
			else
			{
				if (string.Equals(BisherLocationVerlauf.Wert, ScnapscusNearestName, StringComparison.InvariantCultureIgnoreCase))
				{
				}
				else
				{
					BisherLocationVerlauf.EndeZait = NuzerZaitMili;

					if (!(11111 < BisherLocationVerlauf.Dauer))
					{
						InternListeLocationNearest.Remove(BisherLocationVerlauf);
					}

					InternListeLocationNearest.Add(VerlaufNoi);
				}
			}

			InternListeLocationNearest.ListeKürzeBegin(16);
		}

		void AktualisiireTailEveOnlineWelt(
			VonSensorikMesung AusScnapscusAuswertungZuusctand)
		{
			AktualisiireTailListeLocationNearest();

			var EveWeltTopologii = this.EveWeltTopologii;

			try
			{
				if (null == AusScnapscusAuswertungZuusctand)
				{
					return;
				}

				if (null == EveWeltTopologii)
				{
					EveWeltTopologii = new SictEveWeltTopologii();
				}

				var CurrentLocationInfo = AusScnapscusAuswertungZuusctand.CurrentLocationInfo();

				var CurrentLocationInfoSolarSystemName =
					(null == CurrentLocationInfo) ? null :
					CurrentLocationInfo.SolarSystemName;

				var CurrentLocationInfoNearestName =
					(null == CurrentLocationInfo) ? null :
					CurrentLocationInfo.NearestName;

				var CurrentLocationInfoSolarSystemSecurityLevelMili =
					(null == CurrentLocationInfo) ? null :
					CurrentLocationInfo.SolarSystemSecurityLevelMili;

				SictSolarSystem SolarSystem = null;
				SictStation Station = null;

				if (null != CurrentLocationInfoSolarSystemName)
				{
					SolarSystem = EveWeltTopologii.MengeSolarSystem.FirstOrDefault((Kandidaat) => string.Equals(Kandidaat.SystemName, CurrentLocationInfoSolarSystemName));

					if (null == SolarSystem)
					{
						SolarSystem = new SictSolarSystem(CurrentLocationInfoSolarSystemName);

						EveWeltTopologii.MengeSolarSystem.Add(SolarSystem);
					}

					AktualisiireSolarSystem(SolarSystem, this);
				}

				if (null != SolarSystem)
				{
					if (CurrentLocationInfoSolarSystemSecurityLevelMili.HasValue)
					{
						SolarSystem.SecurityLevelMili = CurrentLocationInfoSolarSystemSecurityLevelMili;
					}

					if (true == AusScnapscusAuswertungZuusctand.Docked() &&
						null != CurrentLocationInfoNearestName)
					{
						Station = SolarSystem.MengeStation.FirstOrDefault((Kandidaat) => string.Equals(Kandidaat.StationName, CurrentLocationInfoNearestName));

						if (null == Station)
						{
							Station = new SictStation(CurrentLocationInfoNearestName);

							SolarSystem.MengeStation.Add(Station);
						}
					}
				}
			}
			finally
			{
				this.EveWeltTopologii = EveWeltTopologii;
			}
		}

		public void AktualisiireZuusctandAusScnapscusAuswertung(
			SictAusGbsScnapscusAuswertungSrv AusScnapscusAuswertungZuusctand)
		{
			var InternZuusctand = this;

			AktualisiireZuusctandAusScnapscusAuswertungTailGbs(AusScnapscusAuswertungZuusctand);

			if (null == AusScnapscusAuswertungZuusctand)
			{
				return;
			}

			var OptimatParam = InternZuusctand.OptimatParam();

			var AusZuusctandArgument = AusZuusctandAblaitungBerecne();

			if (null != AusScnapscusAuswertungZuusctand)
			{
				var AusOverviewMengeZaile = AusScnapscusAuswertungZuusctand.AusWindowOverviewTabListeZaile();

				var SelbsctScifZuusctand = AusScnapscusAuswertungZuusctand.SelfShipState;

				if (null != SelbsctScifZuusctand)
				{
					if (true == AusScnapscusAuswertungZuusctand.UnDocking())
					{
						InternZuusctand.SelbstShipUndockingLezteZaitMili = NuzerZaitMili;
					}
				}
			}

			AktualisiireTailMengeAufgaabeVonNuzerMeldungWirkungErfolg();

			AktualisiireTailMengeAufgaabeAbgescloseTailWirkung();

			AktualisiireTailEveOnlineWelt(AusScnapscusAuswertungZuusctand);

			AktualisiireTailMengeZuListEntryInRaumObjekt();

			AktualisiireTailMengeZuOreTypSurveyScanInfo();

			AktualisiireTailMengeZuTargetSurveyScanInfo();

			AktualisiireTailMengeModuleUmscaltFraigaabe();

			AktualisiireTailMengeAufgaabeZuusctand();

			AktualisiireTailTransportMenuEntryAuswaalErfolgNaacGbsMenuKaskaade();

			AktualisiireTailListeInventoryItemTransportMitZait();

			AktualisiireTailMengeWindowZuErhalteUndMengeWindowWirkungGescpert();

			AktualisiireTailListePrioMengeAufgaabe();

			AktualisiireTailMengeAufgaabeUndNaacNuzerVorsclaagWirkung(AufgaabeIdentFabrik);

			AktualisiireTailMengeNaacNuzerMeldung();
		}

		void AktualisiireTailMengeNaacNuzerMeldung()
		{
			var Gbs = this.Gbs;
			var ScritLezteWirkungMengeAufgaabePfaadZuBlatMitGrupePrioNaame = this.ScritLezteWirkungMengeAufgaabePfaadZuBlatMitGrupePrioNaame;
			var ScritLezteListeAufgaabeBerecnet = this.ScritLezteListeAufgaabeBerecnet;
			var ScritLezteListePrioMengeAufgaabe = this.ScritLezteListePrioMengeAufgaabe;

			if (null == Gbs)
			{
				return;
			}

			var MengeMeldungAktiiv = new List<SictNaacNuzerMeldungZuEveOnline>();

			if (null != ScritLezteListeAufgaabeBerecnet)
			{
				foreach (var Aufgaabe in ScritLezteListeAufgaabeBerecnet)
				{
					if (null == Aufgaabe.Key)
					{
						continue;
					}

					var AufgaabeMengeKomponente = Aufgaabe.Key.MengeKomponenteTransitiivBerecne();

					foreach (var AufgaabeKomponente in AufgaabeMengeKomponente)
					{
						if (null == AufgaabeKomponente)
						{
							continue;
						}

						var AufgaabeKomponenteParam = AufgaabeKomponente.AufgaabeParam as AufgaabeParamAndere;

						if (null == AufgaabeKomponenteParam)
						{
							continue;
						}

						var AufgaabeKomponenteNaacNuzerMeldungZuEveOnline = AufgaabeKomponenteParam.NaacNuzerMeldungZuEveOnline;

						if (null == AufgaabeKomponenteNaacNuzerMeldungZuEveOnline)
						{
							continue;
						}

						MengeMeldungAktiiv.Add(AufgaabeKomponenteNaacNuzerMeldungZuEveOnline);
					}
				}
			}

			if (null != ScritLezteListePrioMengeAufgaabe)
			{
				foreach (var PrioMengeAufgaabe in ScritLezteListePrioMengeAufgaabe)
				{
					var GrupePrioMengeAufgaabe = PrioMengeAufgaabe.MengeAufgaabe;

					if (null == GrupePrioMengeAufgaabe)
					{
						continue;
					}

					foreach (var AufgaabeParam in GrupePrioMengeAufgaabe.OfType<AufgaabeParamAndere>())
					{
						if (null == AufgaabeParam)
						{
							continue;
						}

						var AufgaabeNaacNuzerMeldungZuEveOnline = AufgaabeParam.NaacNuzerMeldungZuEveOnline;

						if (null == AufgaabeNaacNuzerMeldungZuEveOnline)
						{
							continue;
						}

						MengeMeldungAktiiv.Add(AufgaabeNaacNuzerMeldungZuEveOnline);
					}
				}
			}

			Gbs.AktualisiireTailMengeNaacNuzerMeldung(NuzerZaitMili, MengeMeldungAktiiv);
		}

		SictAufgaabeZuusctand AufgaabeAbgescloseTailWirkungPasendZuAufgaabeParam(
			SictAufgaabeParam AufgaabeParam)
		{
			var MengeAufgaabeZuusctand = this.MengeAufgaabeZuusctand;

			if (null == MengeAufgaabeZuusctand)
			{
				return null;
			}

			var AufgaabePasend =
				MengeAufgaabeZuusctand
				.Where((Kandidaat) =>
					Kandidaat.AbsclusTailWirkungZait.HasValue &&
					SictAufgaabeParam.HinraicendGlaicwertigFürFortsaz(AufgaabeParam, Kandidaat.AufgaabeParam))
				.OrderBy((Aufgaabe) => Aufgaabe.AbsclusTailWirkungZait)
				.LastOrDefault();

			{
				//	Temp Verzwaigung für Debug Breakpoint

				if (null != AufgaabePasend)
				{
				}
			}

			return AufgaabePasend;
		}

		public void AktualisiireTailMengeWindowZuErhalteUndMengeWindowWirkungGescpert()
		{
			var MengeWindowZuErhalte = new List<SictGbsWindowZuusctand>();
			var MengeWindowWirkungGescpert = new List<SictGbsWindowZuusctand>();

			try
			{
				var AusScnapscusAuswertungZuusctand = this.ListeScnapscusLezteAuswertungErgeebnisNaacSimu;
				var ListeZuWindowNuzungLezteWindowScritAnzaal = this.ListeZuWindowNuzungLezteWindowScritAnzaal;

				var Gbs = this.Gbs;

				if (null == Gbs)
				{
					return;
				}

				if (null == AusScnapscusAuswertungZuusctand)
				{
					return;
				}

				var GbsMengeWindow = (null == Gbs) ? null : Gbs.MengeWindow;

				if (null == GbsMengeWindow)
				{
					return;
				}

				var MengeWindowZuErhalteScnapscus = new VonSensor.Window[]{
					AusScnapscusAuswertungZuusctand.WindowStationLobby,
					AusScnapscusAuswertungZuusctand.WindowOverview,
					AusScnapscusAuswertungZuusctand.WindowSelectedItemView,
					AusScnapscusAuswertungZuusctand.WindowDroneView,};

				foreach (var WindowZuErhalteScnapscus in MengeWindowZuErhalteScnapscus)
				{
					if (null == WindowZuErhalteScnapscus)
					{
						continue;
					}

					var WindowZuErhalte = GbsMengeWindow.FirstOrDefault((Kandidaat) => Kandidaat.GbsAstHerkunftAdrese == WindowZuErhalteScnapscus.Ident);

					MengeWindowZuErhalte.Add(WindowZuErhalte);
				}

				{
					foreach (var Window in GbsMengeWindow)
					{
						if (null == Window)
						{
							continue;
						}

						bool WindowErhalte = false;
						bool WindowWirkungGescpert = false;

						try
						{
							var WindowAlterScrankeMinScritAnzaal = 0;
							var WindowVerwendungLezteAlterScrankeMinScritAnzaal = 1;
							var WindowVerwendungDistanzScritAnzaalScrankeMin = 0;

							var WindowScnapscus = Window.AingangScnapscusTailObjektIdentLezteBerecne();

							var MengeWindowEnthalteScnapscus = new List<VonSensor.Window>(new VonSensor.Window[] { WindowScnapscus });

							var WindowScnapscusAlsWindowStack = WindowScnapscus as VonSensor.WindowStack;

							if (null != WindowScnapscusAlsWindowStack)
							{
								var WindowStackWindowAktiiv = WindowScnapscusAlsWindowStack.WindowAktiiv;

								if (null != WindowStackWindowAktiiv)
								{
									MengeWindowEnthalteScnapscus.Add(WindowStackWindowAktiiv);
								}
							}

							foreach (var WindowEnthalteScnapscus in MengeWindowEnthalteScnapscus)
							{
								if (null == WindowEnthalteScnapscus)
								{
									continue;
								}

								if (WindowEnthalteScnapscus is VonSensor.WindowAgentDialogue)
								{
									//	WindowAgentDialogue sole länger erhalte blaibe da Mesung Objective und Mission Zuusctand (IstOffer, IstAccepted) nur naac zwai aufainanderfolgende Scnapscus mit glaice Mesung übernome werd.

									WindowAlterScrankeMinScritAnzaal = Math.Max(WindowAlterScrankeMinScritAnzaal, 3);
									WindowVerwendungLezteAlterScrankeMinScritAnzaal = Math.Max(WindowVerwendungLezteAlterScrankeMinScritAnzaal, 3);

									WindowVerwendungDistanzScritAnzaalScrankeMin = Math.Max(WindowVerwendungDistanzScritAnzaalScrankeMin, 1);
								}

								if (WindowEnthalteScnapscus is VonSensor.WindowInventoryPrimary)
								{
									WindowAlterScrankeMinScritAnzaal = Math.Max(WindowAlterScrankeMinScritAnzaal, 1);
									WindowVerwendungLezteAlterScrankeMinScritAnzaal = Math.Max(WindowVerwendungLezteAlterScrankeMinScritAnzaal, 1);
								}
							}

							if (Window.ListeScnapscusZuZaitAingangBisherAnzaal < WindowAlterScrankeMinScritAnzaal)
							{
								WindowErhalte = true;

								/*
								 * 2014.06.10
								 * 
								 * Verzwaigung geändert da hirnaac auc noc WindowWirkungGescpert berecnet werde sol.
								continue;
								 * */
							}

							if (null != ListeZuWindowNuzungLezteWindowScritAnzaal)
							{
								var VerwendungLezteScritAnzaal =
									ListeZuWindowNuzungLezteWindowScritAnzaal
									.Where((Kandidaat) => Kandidaat.Key == Window)
									.OrderBy((Kandidaat) => Kandidaat.Value)
									.LastOrDefault();

								if (null != VerwendungLezteScritAnzaal.Key)
								{
									/*
									 * 2014.06.10
									 * 
									if (Window.ListeScnapscusZuZaitAingangBisherAnzaal <= VerwendungLezteScritAnzaal.Value + WindowVerwendungLezteAlterScrankeMinScritAnzaal)
									{
										WindowErhalte = true;
									}
									 * */

									var VerwendungLezteAlterScritAnzaal = Window.ListeScnapscusZuZaitAingangBisherAnzaal - VerwendungLezteScritAnzaal.Value;

									if (VerwendungLezteAlterScritAnzaal <= WindowVerwendungLezteAlterScrankeMinScritAnzaal)
									{
										WindowErhalte = true;
									}

									if (VerwendungLezteAlterScritAnzaal <= WindowVerwendungDistanzScritAnzaalScrankeMin)
									{
										WindowWirkungGescpert = true;
									}
								}
							}
						}
						finally
						{
							if (WindowWirkungGescpert)
							{
								WindowErhalte = true;
							}

							if (WindowErhalte)
							{
								MengeWindowZuErhalte.Add(Window);
							}

							if (WindowWirkungGescpert)
							{
								MengeWindowWirkungGescpert.Add(Window);
							}
						}
					}
				}
			}
			finally
			{
				this.MengeWindowWirkungGescpert = MengeWindowWirkungGescpert.Distinct().ToArray();
				this.MengeWindowZuErhalte = MengeWindowZuErhalte.Distinct().ToArray();
			}
		}

		static InfoPanelTypSictEnum[] MengeInfoPanelExpandSol =
			new InfoPanelTypSictEnum[] { InfoPanelTypSictEnum.SystemInfo, InfoPanelTypSictEnum.Route, InfoPanelTypSictEnum.AgentMission };

		public bool AufgaabeZaitHinraicendJungZuVermaidungWiiderhoolung(
			Int64? KandidaatAufgaabeErsazZait)
		{
			int KandidaatAufgaabeErsazAlterScritAnzaal;

			return AufgaabeZaitHinraicendJungZuVermaidungWiiderhoolung(
				KandidaatAufgaabeErsazZait, out	KandidaatAufgaabeErsazAlterScritAnzaal);
		}

		public bool AufgaabeZaitHinraicendJungZuVermaidungWiiderhoolung(
			Int64? KandidaatAufgaabeErsazZait,
			out	int KandidaatAufgaabeErsazAlterScritAnzaal)
		{
			KandidaatAufgaabeErsazAlterScritAnzaal = -1;

			var AufgaabeErsazAbsclusTailWirkungAlterScrankeMax = 4;

			if (!KandidaatAufgaabeErsazZait.HasValue)
			{
				return false;
			}

			var InternKandidaatAufgaabeErsazAlterScritAnzaal = this.ZuNuzerZaitBerecneAlterScritAnzaal(KandidaatAufgaabeErsazZait.Value);

			if (InternKandidaatAufgaabeErsazAlterScritAnzaal.HasValue)
			{
				KandidaatAufgaabeErsazAlterScritAnzaal = InternKandidaatAufgaabeErsazAlterScritAnzaal.Value;
			}

			var KandidaatAufgaabeErsazAlter = NuzerZaitMili - KandidaatAufgaabeErsazZait;

			if (1 < KandidaatAufgaabeErsazAlterScritAnzaal)
			{
				return false;
			}

			return (KandidaatAufgaabeErsazAlter / 1000) <= AufgaabeErsazAbsclusTailWirkungAlterScrankeMax;
		}

		void AktualisiireTailMengeAufgaabeUndNaacNuzerVorsclaagWirkung(
			Bib3.SictIdentInt64Fabrik AufgaabeIdentFabrik)
		{
			/*
			 * 2015.01.16
			 * Ersaz durc in class benante Methode.
			 * 
			var AufgaabeZaitHinraicendJungZuVermaidungWiiderhoolung = new Func<Int64?, bool>((KandidaatAufgaabeErsazZait) =>
			{
				if(!KandidaatAufgaabeErsazZait.HasValue)
				{
					return false;
				}

				var KandidaatAufgaabeErsazAlterScritAnzaal = this.ZuNuzerZaitBerecneAlterScritAnzaal(KandidaatAufgaabeErsazZait.Value);
				var KandidaatAufgaabeErsazAlter = NuzerZaitMili - KandidaatAufgaabeErsazZait;

				if (1 < KandidaatAufgaabeErsazAlterScritAnzaal)
				{
					return false;
				}

				return (KandidaatAufgaabeErsazAlter / 1000) <= AufgaabeErsazAbsclusTailWirkungAlterScrankeMax;
			});
			 * */

			var ScritLezteWirkungMengeAufgaabePfaadZuBlatMitGrupePrioNaame = new List<KeyValuePair<SictAufgaabeZuusctand[], string>>();

			var ScritLezteListeAufgaabeBerecnet = new List<KeyValuePair<SictAufgaabeZuusctand, string>>();

			var KombiZuusctand = new SictAufgaabeKombiZuusctand(8);

		/*
		 * 2015.03.12
		 * 
		 * Ersaz durc ToCustomBotSnapshot
			var GbsBaum = this.VonNuzerMeldungZuusctandTailGbsBaum;
		 * */

			var GbsBaum = this.ListeScnapscusLezteAuswertungErgeebnisNaacSimu;

			var Gbs = this.Gbs;

			var GbsMengeWindow = (null == Gbs) ? null : Gbs.MengeWindow;

			try
			{
				var BisherScritLezteWirkungMengeAufgaabePfaadZuBlatMitGrupePrioNaame = this.ScritLezteWirkungMengeAufgaabePfaadZuBlatMitGrupePrioNaame;

				var BisherScritLezteMengeAufgaabe = new List<SictAufgaabeZuusctand>();

				if (null != BisherScritLezteWirkungMengeAufgaabePfaadZuBlatMitGrupePrioNaame)
				{
					foreach (var AufgaabePfaadZuBlatMitGrupePrioNaame in BisherScritLezteWirkungMengeAufgaabePfaadZuBlatMitGrupePrioNaame)
					{
						BisherScritLezteMengeAufgaabe.AddRange(AufgaabePfaadZuBlatMitGrupePrioNaame.Key);
					}
				}

				var ZaitMili = this.NuzerZaitMili;

				var OptimatParam = this.OptimatParam();

				var FittingUndShipZuusctand = this.FittingUndShipZuusctand;
				var OverviewUndTarget = this.OverviewUndTarget;

				var MengeWindowWirkungGescpert = this.MengeWindowWirkungGescpert;

				var AnnaameModuleDestruktRangeOptimum = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.AnnaameModuleDestruktRangeOptimum;

				var MengeTargetNocSictbar = (null == OverviewUndTarget) ? null : OverviewUndTarget.MengeTargetNocSictbar;

				var TargetInputFookusAktiiv =
					ExtractFromOldAssembly.Bib3.Extension.FirstOrDefaultNullable(
					MengeTargetNocSictbar,
					(KandidaatTarget) => true == KandidaatTarget.InputFookusTransitioonLezteZiilWert);

				var ListePrioMengeAufgaabe = this.ScritLezteListePrioMengeAufgaabe;

				if (null == ListePrioMengeAufgaabe)
				{
					return;
				}

				var ListeNaacAufgaabeBlatPfaad = new List<KeyValuePair<SictAufgaabeZuusctand[], string>>();

				var KompatibilitäätAinscränkungMengeGrupe = new List<SictGbsZuusctandGrupe>();

				foreach (var GrupePrio in ListePrioMengeAufgaabe)
				{
					var GrupePrioNaame = GrupePrio.GrupePrioNaame;
					var GrupePrioMengeAufgaabe = GrupePrio.MengeAufgaabe;

					if (null == GrupePrioMengeAufgaabe)
					{
						continue;
					}

					var GrupePrioListeAufgaabe =
						GrupePrioMengeAufgaabe
						.OrderBy((AufgaabeParam) =>
							{
								//	Aufgaabe sortiire, Kriterie:
								//	+ Distance
								//	+ Target Input Fookus Aktiiv
								//	+ Overview Objekt Input Fookus Aktiiv

								Int64? Distance = null;
								SictVerlaufBeginUndEndeRef<VonSensor.Menu> MenuLezteMitZait = null;
								bool VorzuugInputFookus = false;

								if (null != AufgaabeParam)
								{
									var KandidaatOverViewObjektZuBearbeite = AufgaabeParam.OverViewObjektZuBearbaiteVirt();
									var KandidaatTargetZuBearbeite = AufgaabeParam.TargetZuBearbaiteVirt();

									if (null != KandidaatOverViewObjektZuBearbeite)
									{
										Distance = KandidaatOverViewObjektZuBearbeite.DistanceScrankeMaxKombi;
										MenuLezteMitZait = KandidaatOverViewObjektZuBearbeite.MenuLezteMitZait;
									}

									if (null != KandidaatTargetZuBearbeite)
									{
										Distance = KandidaatTargetZuBearbeite.SictungLezteDistanceScrankeMaxScpezTarget;
										MenuLezteMitZait = KandidaatTargetZuBearbeite.MenuLezteMitZait;
									}

									var MenuLezteNocVerwendbar =
										(null == MenuLezteMitZait) ? false : !MenuLezteMitZait.EndeZait.HasValue;

									if (Distance < AnnaameModuleDestruktRangeOptimum)
									{
										if (null != KandidaatTargetZuBearbeite)
										{
											if (MenuLezteNocVerwendbar || (true == KandidaatTargetZuBearbeite.InputFookusTransitioonLezteZiilWert))
											{
												VorzuugInputFookus = true;
											}
										}

										if (null != KandidaatOverViewObjektZuBearbeite &&
											MenuLezteNocVerwendbar)
										{
											VorzuugInputFookus = true;
										}

										if (null != TargetInputFookusAktiiv &&
											null != OverviewUndTarget &&
											null != KandidaatOverViewObjektZuBearbeite)
										{
											//	Fals beraits ain Target aktiiv welce zu deem OverView Objekt aus der Aufgaabe past dan sol diiser Aufgaabe Vorzuug gegeeben werde.

											var KandidaatMengeTarget = OverviewUndTarget.MengeTargetTailmengePasendZuOverviewObjektBerecne(KandidaatOverViewObjektZuBearbeite);

											if (null != KandidaatMengeTarget)
											{
												if (KandidaatMengeTarget.Contains(TargetInputFookusAktiiv))
												{
													VorzuugInputFookus = true;
												}
											}
										}
									}
								}

								//	Objekt zu welcem Menu beraits geöfnet isc sol früher bearbaitet werde da so in mance Fäle ain Scrit aingescpaart werde kan.
								var Bewertung =
									VorzuugInputFookus ? (((Distance * 10) / 13) - 1000) : Distance;

								return Bewertung ?? int.MaxValue;
							})
						.OrderBy((AufgaabeParam) =>
							{
								/*
								 * 2014.04.27	hööher Priorisiirte Ordnung:
								 * Objekte welce geraade erst unsictbar geworde sind sole naacrangig behandelt werde da diise eventuel in nääxte Scrit verscwinde
								 * (Overview Viewport Folge bescteet imer aus mindestens zwai Scnapscus).
								 * */

								var NaacrangAufgrundNoierUnsictbarkait = false;

								if (null != AufgaabeParam)
								{
									var AufgaabeParamOverViewObjektZuBearbaite = AufgaabeParam.OverViewObjektZuBearbaiteVirt();

									if (null != AufgaabeParamOverViewObjektZuBearbaite)
									{
										if (AufgaabeParamOverViewObjektZuBearbaite.SaitSictbarkaitLezteListeScritAnzaal == 1 ||
											true == AufgaabeParamOverViewObjektZuBearbaite.ScritLezteSictbarAusgesclose)
										{
											NaacrangAufgrundNoierUnsictbarkait = true;
										}
									}
								}

								return NaacrangAufgrundNoierUnsictbarkait ? 1 : 0;
							})
						.ToArray();

					var GrupePrioMengeAufgaabeRest = new Queue<SictAufgaabeParam>(GrupePrioListeAufgaabe);

					var AufgaabeSctaapel = new Stack<SictAufgaabeZuusctand[]>();

					while (true)
					{
						{
							if (3 < ScritLezteWirkungMengeAufgaabePfaadZuBlatMitGrupePrioNaame.Count)
							{
								//	paralelisatioon von Aufgaabe begrenze auf Viir Aufgaabe pro Scrit
								break;
							}

							if (true)
							{
								bool ParalelisatioonEnde = false;

								foreach (var WirkungAufgaabePfaadZuBlatMitGrupePrioNaame in ScritLezteWirkungMengeAufgaabePfaadZuBlatMitGrupePrioNaame)
								{
									var WirkungAufgaabePfaadBlat = ExtractFromOldAssembly.Bib3.Extension.LastOrDefaultNullable(WirkungAufgaabePfaadZuBlatMitGrupePrioNaame.Key);

									if (null == WirkungAufgaabePfaadBlat)
									{
										continue;
									}

									var WirkungAufgaabePfaadBlatAufgaabeParam = WirkungAufgaabePfaadBlat.AufgaabeParam;

									if (null == WirkungAufgaabePfaadBlatAufgaabeParam)
									{
										continue;
									}

									var AufgaabePfaadBlatNaacNuzerVorsclaagWirkung = WirkungAufgaabePfaadBlatAufgaabeParam.NaacNuzerVorsclaagWirkungVirt();

									if (null == AufgaabePfaadBlatNaacNuzerVorsclaagWirkung)
									{
										continue;
									}

									if (!Bib3.Extension.NullOderLeer(AufgaabePfaadBlatNaacNuzerVorsclaagWirkung.MausPfaadListeWeegpunktFläce) ||
										true == AufgaabePfaadBlatNaacNuzerVorsclaagWirkung.MausPfaadTasteLinksAin ||
										true == AufgaabePfaadBlatNaacNuzerVorsclaagWirkung.MausPfaadTasteRectsAin)
									{
										//	Fals Maus Aktioon vorgesclaage wurde paralelisatioon beende.
										ParalelisatioonEnde = true;
										break;
									}
								}

								if (ParalelisatioonEnde)
								{
									break;
								}
							}
							else
							{
								if (0 < ScritLezteWirkungMengeAufgaabePfaadZuBlatMitGrupePrioNaame.Count)
								{
									//	Vorersct kain paralelisatioon von meerere Aufgaabe
									break;
								}
							}
						}

						if (AufgaabeSctaapel.Count < 1)
						{
							if (GrupePrioMengeAufgaabeRest.Count < 1)
							{
								break;
							}

							var AufgaabeParam = GrupePrioMengeAufgaabeRest.Dequeue();

							SictAufgaabeZuusctand Aufgaabe = null;

							Aufgaabe =
								BisherScritLezteMengeAufgaabe
								.FirstOrDefault((Kandidaat) => AufgaabeHinraicendGlaicwertigFürFortsazFürAufgaabeParamUndBeginZaitHinraicendJung(
									Kandidaat,
									AufgaabeParam,
									ZaitMili - 10000)) ??
								AufgaabeAbgescloseTailWirkungPasendZuAufgaabeParam(AufgaabeParam);

							if (null != Aufgaabe)
							{
								/*
								 * 2015.01.05
								 * 
								if (AufgaabeErsazAbsclusTailWirkungAlterScrankeMax < (ZaitMili - Aufgaabe.AbsclusTailWirkungZait) / 1000)
								 * */
								if (!AufgaabeZaitHinraicendJungZuVermaidungWiiderhoolung(Aufgaabe.AbsclusTailWirkungZait))
								{
									Aufgaabe = null;
								}
							}

							if (null == Aufgaabe)
							{
								Aufgaabe = new SictAufgaabeZuusctand(AufgaabeIdentFabrik.IdentBerecne(), AufgaabeParam, null, ZaitMili);
							}

							AufgaabeSctaapel.Push(new SictAufgaabeZuusctand[] { Aufgaabe });

							ScritLezteListeAufgaabeBerecnet.Add(new KeyValuePair<SictAufgaabeZuusctand, string>(Aufgaabe, GrupePrioNaame));
						}

						{
							var PfaadListeAufgaabe = AufgaabeSctaapel.Pop();

							if (40 < PfaadListeAufgaabe.Length)
							{
								//	!!!!	Meldung Feeler
								continue;
							}

							var Aufgaabe = PfaadListeAufgaabe.LastOrDefault();

							if (false)
							{
								Aufgaabe.ScritLezteVerdrängungDurcAufgaabeVorher = null;
							}

							var AufgaabeParam = Aufgaabe.AufgaabeParam;

							if (null == AufgaabeParam)
							{
								continue;
							}

							var AufgaabeParamMausPfaad = AufgaabeParam.MausPfaadVirt();

							var AufgaabeParamMausPfaadListeWeegpunktGbsObjekt = (null == AufgaabeParamMausPfaad) ? null : AufgaabeParamMausPfaad.ListeWeegpunktGbsObjekt;

							{
								{
									/*
									 * temporäre Scpere von Window für VorsclaagWirkung werd z.B. für AgentDialogue genuzt.
									 * */

									var VorsclaagWirkungBetriftWindowWirkungGescpert = false;

									if (!Bib3.Extension.NullOderLeer(AufgaabeParamMausPfaadListeWeegpunktGbsObjekt) &&
										(true == AufgaabeParamMausPfaad.MausTasteLinxAin ||
										true == AufgaabeParamMausPfaad.MausTasteReczAin) &&
										!Bib3.Extension.NullOderLeer(MengeWindowWirkungGescpert) &&
										null != GbsMengeWindow &&
										null != GbsBaum)
									{
										foreach (var WindowWirkungGescpert in MengeWindowWirkungGescpert)
										{
											if (VorsclaagWirkungBetriftWindowWirkungGescpert)
											{
												break;
											}

											if (null == WindowWirkungGescpert)
											{
												continue;
											}

											var WindowHerkunftAdrese = WindowWirkungGescpert.GbsAstHerkunftAdrese;

											if (!WindowHerkunftAdrese.HasValue)
											{
												continue;
											}

											var WindowScnapscus = WindowWirkungGescpert.ScnapscusWindowLezte;

											if (null == WindowScnapscus)
											{
												continue;
											}

											if (!(WindowScnapscus is VonSensor.WindowAgent))
											{
												continue;
											}

											var WindowGbsAst = GbsBaum.SuuceFlacMengeGbsAstFrühesteMitIdent(WindowHerkunftAdrese.Value);

											if (null == WindowGbsAst)
											{
												continue;
											}

											foreach (var WeegpunktGbsObjekt in AufgaabeParamMausPfaadListeWeegpunktGbsObjekt)
											{
												if (null == WeegpunktGbsObjekt)
												{
													continue;
												}

												var WeegpunktGbsObjektHerkunftAdrese = (Int64?)WeegpunktGbsObjekt.Ident;

												if (!WeegpunktGbsObjektHerkunftAdrese.HasValue)
												{
													continue;
												}

												var InWindowWeegpunktGbsAst =
													WindowGbsAst.SuuceFlacMengeGbsAstFrühesteMitIdent(WeegpunktGbsObjektHerkunftAdrese.Value);

												if (null == InWindowWeegpunktGbsAst)
												{
													continue;
												}

												VorsclaagWirkungBetriftWindowWirkungGescpert = true;
												break;
											}
										}
									}

									if (VorsclaagWirkungBetriftWindowWirkungGescpert)
									{
										continue;
									}
								}

								var AufgaabeParamAlsWindowMinimize = AufgaabeParam as AufgaabeParamAndere;
								var AufgaabeParamAlsTargetInputFookusSeze = AufgaabeParam as AufgaabeParamAndere;
								var AufgaabeParamAlsAktioonInOverviewTabZuAktiviire = AufgaabeParam as AufgaabeParamAndere;
								var AufgaabeParamAlsModuleMesungModuleButtonHint = AufgaabeParam as AufgaabeParamAndere;

								if (null != AufgaabeParam &&
									null != KombiZuusctand)
								{
									if (null != KombiZuusctand.FürVerdrängteAufgaabeZuWarte)
									{
										break;
									}

									if (null != AufgaabeParamAlsTargetInputFookusSeze)
									{
										if (null != AufgaabeParamAlsTargetInputFookusSeze.TargetInputFookusSeze)
										{
											if (null != KombiZuusctand.TargetBeleegtAufgaabe)
											{
												continue;
											}

											KombiZuusctand.TargetBeleegtAufgaabeSeze(Aufgaabe);
										}
									}

									if (null != AufgaabeParamAlsWindowMinimize)
									{
										if (null != AufgaabeParamAlsWindowMinimize.WindowMinimize)
										{
											if (KombiZuusctand.WindowMinimizeBeleegungFraigaabeFürAufgaabePfaad(PfaadListeAufgaabe))
											{
												KombiZuusctand.WindowMinimizeBeleegtAufgaabeSeze(Aufgaabe);
											}
											else
											{
												//	Pro Optimat Scrit nur ain Window minimiire.
												continue;
											}
										}
									}

									if (null != AufgaabeParamAlsAktioonInOverviewTabZuAktiviire)
									{
										if (null != AufgaabeParamAlsAktioonInOverviewTabZuAktiviire.AktioonInOverviewTabZuAktiviire)
										{
											if (null != KombiZuusctand.OverViewTabBeleegtAufgaabe)
											{
												continue;
											}

											KombiZuusctand.OverViewTabBeleegtAufgaabeSeze(Aufgaabe);
										}
									}

									if (null != AufgaabeParamAlsModuleMesungModuleButtonHint)
									{
										if (null != AufgaabeParamAlsModuleMesungModuleButtonHint.ModuleMesungModuleButtonHint)
										{
											KombiZuusctand.MausBeleegtAufgaabeSeze(Aufgaabe);
										}
									}

									//	if (Aufgaabe.AbsclusTailWirkungZait.HasValue)
									{
										//	2014.06.11 Beobactung Feeler: Automaat wexelt zwisce Manööver zwisce zwai Objekte dere Cargo durcsuuct were sol sctat das Manöver zum nähere Objekt baizubehalte.
										//	2014.06.12 Änderung: ManööverBeleegtAufgaabeSeze wen in Aufgaabe begonene Manööver enthalte.

										var MengeKomponenteManööver =
											Aufgaabe.MengeKomponenteBerecneTransitiivTailmengeAufgaabePasendZuPrädikaat(
											(Kandidaat) =>
											{
												var KandidaatManööverErgeebnis = Kandidaat.ManööverErgeebnis;

												if (null == KandidaatManööverErgeebnis)
												{
													return false;
												}

												return !KandidaatManööverErgeebnis.EndeZait.HasValue;
											})
											.ToArrayNullable();

										var KomponenteManöver = ExtractFromOldAssembly.Bib3.Extension.FirstOrDefaultNullable(MengeKomponenteManööver);

										if (null != KomponenteManöver)
										{
											if (null == KombiZuusctand.ManööverBeleegtAufgaabe)
											{
												KombiZuusctand.ManööverBeleegtAufgaabeSeze(Aufgaabe);
											}
										}
									}

									if (null != AufgaabeParam.ManööverUnterbreceNictVirt())
									{
										if (null == KombiZuusctand.ManööverBeleegtAufgaabe)
										{
											KombiZuusctand.ManööverBeleegtAufgaabeSeze(Aufgaabe);
										}
										else
										{
											continue;
										}
									}

									if (AufgaabeParam.DistanzAinzuscteleScrankeMinVirt().HasValue ||
										AufgaabeParam.DistanzAinzuscteleScrankeMaxVirt().HasValue)
									{
										if (KombiZuusctand.AufgaabeReegelungDistanceFürAufgaabeFraigaabeBerecne(Aufgaabe))
										{
										}
										else
										{
											continue;
										}

										if (null == KombiZuusctand.ManööverBeleegtAufgaabe)
										{
										}
										else
										{
											if (!(true == AufgaabeParam.VorrangVorManööverUnterbreceNictVirt()))
											{
												continue;
											}
										}
									}
								}
							}

							if (Aufgaabe.AbsclusTailWirkungZait.HasValue)
							{
								var AufgaabeMengeKomponente = Aufgaabe.MengeKomponenteBerecne();

								var AufgaabeUndMengeKomponenteParam =
									Optimat.Glob.ListeErwaitertAlsArray(new SictAufgaabeParam[] { AufgaabeParam },
									(null == AufgaabeMengeKomponente) ? null :
									AufgaabeMengeKomponente.Select((AufgaabeKomponente) => AufgaabeKomponente.AufgaabeParam));

								if (AufgaabeUndMengeKomponenteParam.Any((AufgaabeKomponenteParam) =>
									{
										if (null == AufgaabeKomponenteParam)
										{
											return false;
										}

										return AufgaabeKomponenteParam.WirkungGbsMengeZuusctandGrupeVorrausgeseztAlsUnverändertNaacAbsclusTailWirkung();
									}))
								{
									//	Vorersct werd noc nit berecnet welce Aufgaabe sic geegesaitig sctööre könten sonder ainfac nuur aine aktiive Aufgaabe Blat in Sclange gehalte.
									break;
								}

								continue;
							}

							/*
							 * 2015.01.16
							 * 
							 * Ersaz durc AufgaabeScritLezteVerdrängtDurcAufgaabeErsazKandidaatUndAlterScritAnzaal
							 * 
							var AufgaabeScritVerdrängtDurcAufgaabeErsaz = false;

							KeyValuePair<SictAufgaabeZuusctand, int>? AufgaabeScritLezteVerdrängtDurcAufgaabeErsazUndAlterScritAnzaal = null;
							bool AufgaabeScritLezteAbzuwarte = false;
							 * */

							AufgaabeVerdrängungUndWarte? VerdrängungDurcAufgaabeVorher = null;

							{
								var AufgaabeErsaz = AufgaabeAbgescloseTailWirkungPasendZuAufgaabeParam(Aufgaabe.AufgaabeParam);

								if (null != AufgaabeErsaz)
								{
									int AufgaabeErsazAbsclusTailWirkungZaitAlterScritAnzaal;

									/*
									 * 2015.01.5
									 * 
									if ((ZaitMili - AufgaabeErsaz.AbsclusTailWirkungZait) / 1000 < AufgaabeErsazAbsclusTailWirkungAlterScrankeMax)
									 * */
									if (AufgaabeZaitHinraicendJungZuVermaidungWiiderhoolung(AufgaabeErsaz.AbsclusTailWirkungZait, out	AufgaabeErsazAbsclusTailWirkungZaitAlterScritAnzaal))
									{
										//	Für Aufgaabe mit hinraicend glaicwertige Param wurde Tail Wirkung beraits abgesclose.
										/*
										 * 2015.01.16
										 * 
										 * Ersaz durc AufgaabeScritLezteVerdrängtDurcAufgaabeErsazKandidaatUndAlterScritAnzaal
										 * 
													AufgaabeScritVerdrängtDurcAufgaabeErsaz	= true;
										 * */

										var FürVerdrängendeAufgaabeZuWarteScritAnzaal =
											FürVerdrängendeAufgaabeZuWarteScritAnzaalBerecne(AufgaabeErsaz.AufgaabeParam);

										/*
										 * 2015.01.16
										 * 
										var AufgaabeErsazParam = AufgaabeErsaz.AufgaabeParam;
										var AufgaabeErsazParamAndere = AufgaabeErsazParam as AufgaabeParamAndere;

										if (null != AufgaabeErsazParamAndere)
										{
											if (AufgaabeErsazParamAndere.AktioonMenuBegin ?? false)
											{
												FürVerdrängendeAufgaabeZuWarteScritAnzaal = Math.Max(2, FürVerdrängendeAufgaabeZuWarteScritAnzaal);
											}
										}
										 * */

										VerdrängungDurcAufgaabeVorher = new AufgaabeVerdrängungUndWarte(
											AufgaabeErsaz,
											AufgaabeErsazAbsclusTailWirkungZaitAlterScritAnzaal,
											FürVerdrängendeAufgaabeZuWarteScritAnzaal);
									}
								}
							}

							Aufgaabe.ScritLezteVerdrängungDurcAufgaabeVorher = VerdrängungDurcAufgaabeVorher;

							if (VerdrängungDurcAufgaabeVorher.HasValue)
							{
								if (VerdrängungDurcAufgaabeVorher.Value.FürVerdrängendeAufgaabeZuWarte)
								{
									KombiZuusctand.FürVerdrängteAufgaabeZuWarteSeze(Aufgaabe);
									break;
								}

								continue;
							}

							AufgaabeScrit(ZaitMili, Aufgaabe, AufgaabeIdentFabrik, KombiZuusctand);

							var AufgaabeZerleegungErgeebnisLezteZuZait = Aufgaabe.ZerleegungErgeebnisLezteZuZait;

							if (AufgaabeZerleegungErgeebnisLezteZuZait.Zait < ZaitMili)
							{
								//	In diisem Scrit wurde kaine Zerleegung berecnet, daher kain Vorsclaag Wirkung
								continue;
							}

							var AufgaabeZerleegungErgeebnisLezte = AufgaabeZerleegungErgeebnisLezteZuZait.Wert;

							var AufgaabeParamNaacNuzerVorsclaagWirkung = AufgaabeParam.NaacNuzerVorsclaagWirkungVirt();

							if (Aufgaabe.IstBlatNaacNuzerVorsclaagWirkung() ||
								null != AufgaabeParam.NaacNuzerMeldungZuEveOnlineVirt())
							{
								var Fraigaabe = true;

								if (null != AufgaabeParamNaacNuzerVorsclaagWirkung)
								{
									if (!Bib3.Extension.NullOderLeer(AufgaabeParamNaacNuzerVorsclaagWirkung.MausPfaadListeWeegpunktFläce) ||
										true == AufgaabeParamNaacNuzerVorsclaagWirkung.MausPfaadTasteLinksAin ||
										true == AufgaabeParamNaacNuzerVorsclaagWirkung.MausPfaadTasteRectsAin)
									{
										if (!KombiZuusctand.MausBeleegungFraigaabeFürAufgaabePfaad(PfaadListeAufgaabe))
										{
											Fraigaabe = false;
										}
									}
								}

								if (Fraigaabe)
								{
									ScritLezteWirkungMengeAufgaabePfaadZuBlatMitGrupePrioNaame.Add(
										new KeyValuePair<SictAufgaabeZuusctand[], string>(PfaadListeAufgaabe, GrupePrioNaame));

									foreach (var PfaadAstAufgaabe in PfaadListeAufgaabe)
									{
										KombiZuusctand.ListeAufgaabeModuleBeleegtFüügeAin(PfaadAstAufgaabe);
									}
								}
							}
							else
							{
								if (null != AufgaabeZerleegungErgeebnisLezte)
								{
									var ListeTailaufgaabeAktiiv = AufgaabeZerleegungErgeebnisLezte.MengeKomponente;

									if (null != ListeTailaufgaabeAktiiv)
									{
										var ListeTailaufgaabeAktiivMitPfaad =
											ListeTailaufgaabeAktiiv
											.Select((AufgaabeKomponente) => Optimat.Glob.ListeErwaitertAlsArray(PfaadListeAufgaabe, AufgaabeKomponente))
											.ToArray();

										foreach (var TailaufgaabeAktiivMitPfaad in ListeTailaufgaabeAktiivMitPfaad.Reverse())
										{
											AufgaabeSctaapel.Push(TailaufgaabeAktiivMitPfaad);
										}
									}
								}
							}
						}
					}
				}
			}
			finally
			{
				{
					//	Temp Verzwaigung für Debug Breakpoint

					if (0 < ScritLezteWirkungMengeAufgaabePfaadZuBlatMitGrupePrioNaame.Count)
					{
						var AufgaabePfaad = ScritLezteWirkungMengeAufgaabePfaadZuBlatMitGrupePrioNaame[0].Key;

						var Aufgaabe = AufgaabePfaad.FirstOrDefault();

						var AufgaabeParam = Aufgaabe.AufgaabeParam as AufgaabeParamAndere;

						if (null != AufgaabeParam)
						{
							var ModuleMesungModuleButtonHint = AufgaabeParam.ModuleMesungModuleButtonHint;

							if (null != ModuleMesungModuleButtonHint)
							{
								if (430 < (ModuleMesungModuleButtonHint.ListeLaageLezteBerecne() ?? new Vektor2DInt(0, 0)).A)
								{
								}
							}
						}
					}
				}

				//	AingangInfoBetailigtAnVorsclaagWirkung

				foreach (var ScritLezteWirkungAufgaabePfaadZuBlatMitGrupePrioNaame in ScritLezteWirkungMengeAufgaabePfaadZuBlatMitGrupePrioNaame)
				{
					if (null == ScritLezteWirkungAufgaabePfaadZuBlatMitGrupePrioNaame.Key)
					{
						continue;
					}

					foreach (var WirkungAufgaabePfaadAst in ScritLezteWirkungAufgaabePfaadZuBlatMitGrupePrioNaame.Key)
					{
						if (null == WirkungAufgaabePfaadAst)
						{
							continue;
						}

						WirkungAufgaabePfaadAst.AingangInfoBetailigtAnVorsclaagWirkung(NuzerZaitMili, true);
					}
				}

				if (null != GbsBaum &&
					null != GbsMengeWindow)
				{
					//	Berecnung ListeZuWindowNuzungLezteWindowScritAnzaal

					/*
					 * 2015.03.12
					 * 
					var GbsMengeWindowGbsAst =
						GbsMengeWindow
						.Select((Window) => new KeyValuePair<SictGbsWindowZuusctand, SictGbsAstInfoSictAuswert>(
							Window,
							GbsBaum.SuuceFlacMengeAstFrühesteMitHerkunftAdrese(Window.GbsAstHerkunftAdrese ?? -1)))
						.Where((t) => null != t.Value)
						.ToArray();

					var GbsMengeWindowMengeGbsAstAdrese =
						GbsMengeWindowGbsAst
						.Select((WindowGbsAst) =>
								new KeyValuePair<SictGbsWindowZuusctand, Int64[]>(
							WindowGbsAst.Key,
							WindowGbsAst.Value.MengeSelbsctUndChildAstHerkunftAdreseTransitiiveHüleBerecne()))
						.ToArray();
					 * */
					var GbsMengeWindowGbsAst =
						GbsMengeWindow
						.Select((Window) => new KeyValuePair<SictGbsWindowZuusctand, GbsElement[]>(
							Window,
							Window.AingangScnapscusTailObjektIdentLezteBerecne().GbsBaumEnumFlacListeKnoote().ToArray()))
						.Where((t) => null != t.Value)
						.ToArray();

					var GbsMengeWindowMengeGbsAstAdrese =
						GbsMengeWindowGbsAst
						.Select((WindowGbsAst) =>
								new KeyValuePair<SictGbsWindowZuusctand, Int64[]>(
							WindowGbsAst.Key,
							WindowGbsAst.Value.SelectNullable(GbsAst => GbsAst.Ident).ToArrayNullable()))
						.ToArray();

					var MengeWindowVerwendet = new List<SictGbsWindowZuusctand>();

					foreach (var ScritLezteWirkungAufgaabePfaadZuBlatMitGrupePrioNaame in ScritLezteWirkungMengeAufgaabePfaadZuBlatMitGrupePrioNaame)
					{
						if (null == ScritLezteWirkungAufgaabePfaadZuBlatMitGrupePrioNaame.Key)
						{
							continue;
						}

						foreach (var WirkungAufgaabePfaadAst in ScritLezteWirkungAufgaabePfaadZuBlatMitGrupePrioNaame.Key)
						{
							if (null == WirkungAufgaabePfaadAst)
							{
								continue;
							}

							var AstAufgaabeParam = WirkungAufgaabePfaadAst.AufgaabeParam;

							var AstAufgaabeParamScpezAndere = AstAufgaabeParam as AufgaabeParamAndere;

							if (null == AstAufgaabeParamScpezAndere)
							{
								continue;
							}

							if (null != AstAufgaabeParamScpezAndere.WindowMinimize ||
								null != AstAufgaabeParamScpezAndere.WindowHooleNaacVorne ||
								AstAufgaabeParam	is	AufgaabeParamGbsElementVerberge)
							{
								//	Diise werde nit als Verwendet gekenzaicnet da hiir scon abseebar isc das kaine waitere Verwendung meer erfolgt.
								break;
							}

							var AstAufgaabeParamMausPfaad = AstAufgaabeParamScpezAndere.MausPfaad;

							if (null == AstAufgaabeParamMausPfaad)
							{
								continue;
							}

							var AstAufgaabeParamMausPfaadListeWeegpunkt = AstAufgaabeParamMausPfaad.ListeWeegpunktGbsObjekt;

							if (null == AstAufgaabeParamMausPfaadListeWeegpunkt)
							{
								continue;
							}

							foreach (var MausPfaadWeegpunktGbsObjekt in AstAufgaabeParamMausPfaadListeWeegpunkt)
							{
								if (null == MausPfaadWeegpunktGbsObjekt)
								{
									continue;
								}

								var MausPfaadWeegpunktGbsObjektHerkunftAdreseNulbar = (Int64?)MausPfaadWeegpunktGbsObjekt.Ident;

								if (!MausPfaadWeegpunktGbsObjektHerkunftAdreseNulbar.HasValue)
								{
									continue;
								}

								foreach (var GbsWindowMengeGbsAstAdrese in GbsMengeWindowMengeGbsAstAdrese)
								{
									if (GbsWindowMengeGbsAstAdrese.Value.Contains(MausPfaadWeegpunktGbsObjektHerkunftAdreseNulbar.Value))
									{
										MengeWindowVerwendet.Add(GbsWindowMengeGbsAstAdrese.Key);
									}
								}
							}
						}
					}

					var ListeZuWindowNuzungLezteWindowScritAnzaal = this.ListeZuWindowNuzungLezteWindowScritAnzaal;

					foreach (var WindowVerwendet in MengeWindowVerwendet.Distinct())
					{
						if (null == ListeZuWindowNuzungLezteWindowScritAnzaal)
						{
							this.ListeZuWindowNuzungLezteWindowScritAnzaal = ListeZuWindowNuzungLezteWindowScritAnzaal =
								new List<KeyValuePair<SictGbsWindowZuusctand, Int64>>();
						}

						Optimat.Glob.NaacListeFüügeAinOoderÜberscraibeFalsKeyBeraitsVorhande(
							ListeZuWindowNuzungLezteWindowScritAnzaal,
							WindowVerwendet,
							WindowVerwendet.ListeScnapscusZuZaitAingangBisherAnzaal);
					}

					if (null != ListeZuWindowNuzungLezteWindowScritAnzaal)
					{
						//	Aufroime ListeZuWindowNuzungLezteWindowScritAnzaal: Ainträäge zu nit meer vorhandene Window entferne.

						ListeZuWindowNuzungLezteWindowScritAnzaal.RemoveAll((Kandidaat) => !GbsMengeWindow.Contains(Kandidaat.Key));
					}
				}

				{
					foreach (var WirkungMengeAufgaabePfaadZuBlatMitGrupePrioNaame in ScritLezteWirkungMengeAufgaabePfaadZuBlatMitGrupePrioNaame)
					{
						var AufgaabeZuusctand = WirkungMengeAufgaabePfaadZuBlatMitGrupePrioNaame.Key.LastOrDefaultNullable();

						if (null == AufgaabeZuusctand)
						{
							continue;
						}

						var AufgaabeParam = AufgaabeZuusctand.AufgaabeParam as AufgaabeParamAndere;

						if (null == AufgaabeParam)
						{
							continue;
						}

						var NaacNuzerVorsclaagWirkung = AufgaabeParam.NaacNuzerVorsclaagWirkung;

						if (null == NaacNuzerVorsclaagWirkung)
						{
							continue;
						}

						if (!NaacNuzerVorsclaagWirkung.NuzerZaitMili.HasValue)
						{
							NaacNuzerVorsclaagWirkung.NuzerZaitMili = NuzerZaitMili;
						}
					}
				}

				this.ScritLezteWirkungMengeAufgaabePfaadZuBlatMitGrupePrioNaame = ScritLezteWirkungMengeAufgaabePfaadZuBlatMitGrupePrioNaame.ToArray();
				this.ScritLezteListeAufgaabeBerecnet = ScritLezteListeAufgaabeBerecnet.ToArray();
				this.ScritLezteListeAufgaabeKombiZuusctand = KombiZuusctand;
			}
		}
	}
}
