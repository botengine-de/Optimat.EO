using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Optimat.EveOnline;
using Optimat.EveOnline.Anwendung;
using Bib3;
using Optimat.EveOnline.Base;
//using Optimat.EveOnline.AuswertGbs;
using VonSensor = Optimat.EveOnline.VonSensor;
using Optimat.EveOnline.VonSensor;
using ExtractFromOldAssembly.Bib3;
using Optimat.EveOnline.Base.TempUsedByBotAndUI;

namespace Optimat.ScpezEveOnln
{
	public partial class SictAutomatZuusctand
	{
		static readonly public string[] MengeMessageBoxOkCaptionRegexPattern = new string[]{
			"Character Not Reachable",
			"Information",
			"Shutdown In Progress"};

		/// <summary>
		/// 2014.04.19	Bsp:	"Remove agent(s)?"
		/// </summary>
		static readonly public string[] MengeMessageBoxNoCaptionRegexPattern = new string[]{
			"Remove agent",
			"Delete Fitting",
			"Fit Rigs",
			"Cargo Capacity Warning"};

		public void AktualisiireTailListePrioMengeAufgaabe()
		{
			this.ScritLezteListePrioMengeAufgaabe = ListePrioMengeAufgaabeBerecne();
		}

		SictAufgaabeGrupePrio[] ListePrioMengeAufgaabeBerecne()
		{
			var AusScnapscusAuswertungZuusctand = this.ListeScnapscusLezteAuswertungErgeebnisNaacSimu;

			var ScnapscusCharacterAuswaalAbgesclose =
				(null == AusScnapscusAuswertungZuusctand) ? null : AusScnapscusAuswertungZuusctand.CharacterAuswaalAbgesclose;

			var ScnapscusShipUi =
				(null == AusScnapscusAuswertungZuusctand) ? null : AusScnapscusAuswertungZuusctand.ShipUi;

			var MengeZuInfoPanelTypeButtonUndInfoPanel =
				(null == AusScnapscusAuswertungZuusctand) ? null : AusScnapscusAuswertungZuusctand.MengeZuInfoPanelTypeButtonUndInfoPanel();

			var ScnapscusMengeMessageBox = (null == AusScnapscusAuswertungZuusctand) ? null : AusScnapscusAuswertungZuusctand.MengeMessageBox();

			var ScnapscusMengeHybridWindow = (null == AusScnapscusAuswertungZuusctand) ? null : AusScnapscusAuswertungZuusctand.MengeHybridWindow();

			var ScnapscusMengeMessageBoxUndHybridWindow = new List<VonSensor.MessageBox>();

			if (null != ScnapscusMengeMessageBox)
			{
				ScnapscusMengeMessageBoxUndHybridWindow.AddRange(ScnapscusMengeMessageBox);
			}

			if (null != ScnapscusMengeHybridWindow)
			{
				ScnapscusMengeMessageBoxUndHybridWindow.AddRange(ScnapscusMengeHybridWindow);
			}

			//	ScnapscusMengeMessageBoxUndHybridWindow.AddRange(AusScnapscusAuswertungZuusctand?.MengeWindowSonstige?.OfType<VonSensor.Me>)

			var ScnapscusMengeMessageBoxUndHybridWindowModal =
				ScnapscusMengeMessageBoxUndHybridWindow
				.Where((Kandidaat) => true == Kandidaat.isModal).ToArray();

			var LayerMainMengeWindowStack = (null == AusScnapscusAuswertungZuusctand) ? null : AusScnapscusAuswertungZuusctand.MengeWindowStack;

			var ScnapscusMengeWindowZuBearbaiteVorrangigOrdnet =
				(null == ScnapscusMengeMessageBoxUndHybridWindowModal) ? null :
				ScnapscusMengeMessageBoxUndHybridWindowModal
				/*
				2015.09.01
				Änderung InGbsBaumAstIndex: Übernaame von noie Sensor InTreeIndex: Element occludes other Elements with lower Value.
			.OrderBy((Kandidaat) => Kandidaat.InGbsBaumAstIndex)
			*/
				.OrderByDescending((Kandidaat) => Kandidaat.InGbsBaumAstIndex)
				.ToArray();

			var ScnapscusWindowOverview =
				(null == ScnapscusMengeMessageBox) ? null : AusScnapscusAuswertungZuusctand.WindowOverview;

			var ScnapscusWindowOverviewTabSelected =
				(null == ScnapscusWindowOverview) ? null : ScnapscusWindowOverview.TabSelected;

			var LanguageNotSetToEnglish =
				(null == AusScnapscusAuswertungZuusctand) ? null : AusScnapscusAuswertungZuusctand.LanguageNotSetToEnglish();

			var ScnapscusCurrentLocationInfo =
				(null == AusScnapscusAuswertungZuusctand) ? null : AusScnapscusAuswertungZuusctand.CurrentLocationInfo();

			var Gbs = this.Gbs;
			var FittingUndShipZuusctand = this.FittingUndShipZuusctand;
			var InRaumAktioonUndGefect = this.InRaumAktioonUndGefect;
			var OverviewUndTarget = this.OverviewUndTarget;
			var OptimatParam = this.OptimatParam();

			var VonNuzerParamAutoMineFraigaabe = (null == OptimatParam) ? null : OptimatParam.AutoMineFraigaabe;
			var VonNuzerParamAutoMissionFraigaabe = (null == OptimatParam) ? null : OptimatParam.AutoMissionFraigaabe;

			var VonNuzerParamSimu = (null == OptimatParam) ? null : OptimatParam.SimuNaacBerüksictigungFraigaabeBerecne();

			var FittingMengeModule = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.MengeModuleRepr;
			var FittingAmmoLoadLezteBeginZait = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.AmmoLoadLezteBeginZait;
			var FittingAmmoLoadLezteAlterMili = NuzerZaitMili - FittingAmmoLoadLezteBeginZait;

			var AnnaameModuleShieldBoosterSelbsct =
				(null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.AnnaameModuleShieldBoosterSelbsct;

			var ShipZuusctand = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.ShipZuusctand;
			var SelbsctShipDocked = (null == ShipZuusctand) ? null : ShipZuusctand.Docked;
			var SelbsctShipCloaked = (null == ShipZuusctand) ? null : ShipZuusctand.Cloaked;
			var SelbsctShipJammed = (null == ShipZuusctand) ? null : ShipZuusctand.Jammed;

			var AnnaameModuleDestruktRangeMax = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.AnnaameModuleDestruktRangeMax;

			var MengeTargetNocSictbar = (null == OverviewUndTarget) ? null : OverviewUndTarget.MengeTargetNocSictbar;

			var MengeOverviewObjektGrupeVerwendetNaacBerüksictigungPrio =
				(null == InRaumAktioonUndGefect) ? null : InRaumAktioonUndGefect.MengeOverviewObjektGrupeVerwendetNaacBerüksictigungPrio;

			var MengeObjektZuDestruiireNääxteDistance = (null == InRaumAktioonUndGefect) ? null : InRaumAktioonUndGefect.MengeObjektZuDestruiireNääxteDistance;

			var AnforderungShieldBoosterLezte = (null == InRaumAktioonUndGefect) ? null : InRaumAktioonUndGefect.AnforderungShieldBoosterLezte;

			var AnforderungDockUrsace = (null == InRaumAktioonUndGefect) ? null : InRaumAktioonUndGefect.AnforderungDockUrsace;
			var AnforderungFluctUrsace = (null == InRaumAktioonUndGefect) ? null : InRaumAktioonUndGefect.AnforderungFluctUrsace;

			var SelbsctShipDockedLezteZaitMili = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.DockedLezteZaitMili;
			var SelbsctShipWarpingLezteZaitMili = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.WarpingLezteZaitMili;
			var SelbsctShipJumpingLezteZaitMili = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.JumpingLezteZaitMili;

			var WarpingLezteAlterMili = NuzerZaitMili - SelbsctShipWarpingLezteZaitMili;
			var DockedLezteAlterMili = NuzerZaitMili - SelbsctShipDockedLezteZaitMili;

			var SelbsctShipWarpScrambled = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.SelbsctShipWarpScrambled;

			var OverviewPresetZuLaadeIdent =
				(null == OverviewUndTarget) ? null : OverviewUndTarget.OverviewPresetZuLaadeIdent;

			var MengeOverviewPresetFeelend =
				(null == OverviewUndTarget) ? null : OverviewUndTarget.MengeOverviewPresetFeelend;

			var ListePrioMengeAufgaabe = new List<SictAufgaabeGrupePrio>();

			var GbsMengeWindow = (null == Gbs) ? null : Gbs.MengeWindow;

			var FürGefectListeDamageTypePrio = InRaumAktioonUndGefect.FürGefectListeDamageTypePrio;

			SictDamageMitBetraagIntValue[] VonNuzerParamVorgaabeFürGefectListeDamageTypePrio = null;

			if (null != VonNuzerParamSimu)
			{
				VonNuzerParamVorgaabeFürGefectListeDamageTypePrio = VonNuzerParamSimu.VorgaabeFürGefectListeDamageTypePrio;
			}

			if (null != VonNuzerParamVorgaabeFürGefectListeDamageTypePrio)
			{
				FürGefectListeDamageTypePrio = VonNuzerParamVorgaabeFürGefectListeDamageTypePrio;
			}

			var VonNuzerParamSimuMausAufWindowVordersteEkeOderKanteIndex =
				(null == VonNuzerParamSimu) ? null :
				VonNuzerParamSimu.AufgaabeMausAufWindowVordersteEkeOderKanteIndex;

			var FürGefectListePrioMengeDamageTypePrioOrdnet =
				(null == FürGefectListeDamageTypePrio) ? null :
				FürGefectListeDamageTypePrio
				.GroupBy((Kandidaat) => Kandidaat.BetraagInt)
				.OrderByDescending((Kandidaat) => Kandidaat.Key ?? -1)
				.Select((Grupe) => new KeyValuePair<SictDamageTypeSictEnum[], int?>(
					Grupe.Select((DamageTypeMitBetraag) => DamageTypeMitBetraag.DamageType)
					.Where((DamageTypeNulbar) => DamageTypeNulbar.HasValue)
					.Select((DamageTypeNulbar) => DamageTypeNulbar.Value).ToArray(), Grupe.Key))
				.ToArray();

			var FürGefectPrioMengeDamageType =
				(null == FürGefectListePrioMengeDamageTypePrioOrdnet) ? null :
				FürGefectListePrioMengeDamageTypePrioOrdnet.FirstOrDefault().Key;

			var InRaumAktioonUndGefectListePrioMengeAufgaabe = InRaumAktioonUndGefect?.ListePrioGrupeMengeAufgaabeParam;

			var InRaumAktioonUndGefectMengeTargetZuUnLockeMitPrio =
				(null == InRaumAktioonUndGefect) ? null :
				InRaumAktioonUndGefect.MengeTargetZuUnLockeMitPrio;

			var InRaumAktioonFortsaz =
				(null == InRaumAktioonUndGefect) ? null :
				InRaumAktioonUndGefect.InRaumAktioonFortsaz;

			var OverviewTabZuAktiviireFürMaidungScroll =
				(null == InRaumAktioonUndGefect) ? null :
				InRaumAktioonUndGefect.OverviewTabZuAktiviireFürMaidungScroll;

			var ScnapscusWindowOverviewScroll =
				(null == ScnapscusWindowOverview) ? null : ScnapscusWindowOverview.Scroll;

			var ScnapscusWindowOverviewScrollHandleAntailAnGesamtMili =
				(null == ScnapscusWindowOverviewScroll) ? null : ScnapscusWindowOverviewScroll.ScrollHandleAntailAnGesamtMili;

			if (!(true == ScnapscusCharacterAuswaalAbgesclose))
			{
				ListePrioMengeAufgaabe.Add(
					new SictAufgaabeGrupePrio(
					AufgaabeParamAndere.KonstruktNaacNuzerMeldungZuEveOnline(
						SictNaacNuzerMeldungZuEveOnline.ErrorGenerel(
						-1, new SictNaacNuzerMeldungZuEveOnlineCause(CauseBinary: SictNaacNuzerMeldungZuEveOnlineCauseTypeBinär.CharSelectionCompletedNot))),
					"Meldung Feeler CharSelectionCompletedNot"));
			}
			else
			{
				{
					//	Fluct/Dock

					if (!(true == SelbsctShipWarpScrambled) &&
						!(DockedLezteAlterMili < 3555))
					{
						if (null != AnforderungFluctUrsace ||
							null != AnforderungDockUrsace)
						{
							//	Naac Station flücte

							if (!(WarpingLezteAlterMili < 1800) &&
								!(true == AusScnapscusAuswertungZuusctand.Docked()))
							{
								this.FluctLezte = new SictWertMitZait<object>(NuzerZaitMili, null);

								var NaacNuzerMeldung = new SictNaacNuzerMeldungZuEveOnline(
									-1, Severity: SictNaacNuzerMeldungZuEveOnlineSeverity.Warning, DockForcedCause: AnforderungDockUrsace ?? AnforderungFluctUrsace);

								var ZwekSictString =
									NaacNuzerMeldung.MeldungSictStringBerecneAusNaacNuzerMeldung();

								var NaacNuzerMeldungAufgaabeParam =
									AufgaabeParamAndere.KonstruktNaacNuzerMeldungZuEveOnline(NaacNuzerMeldung);

								ListePrioMengeAufgaabe.Add(new SictAufgaabeGrupePrio(
									new SictAufgaabeParam[]{
									AufgaabeParamAndere.KonstruktAufgaabeParam(
									new AufgaabeParamShipFluct(),
									new string[]{ZwekSictString}),
									NaacNuzerMeldungAufgaabeParam},
											"Fluct ooder Dock"));
							}
						}
					}
				}

				if (0 < ExtractFromOldAssembly.Bib3.Extension.CountNullable(MengeOverviewPresetFeelend))
				{
					var MengeMeldungAufgaabeParam =
						MengeOverviewPresetFeelend
						.Select((OverviewPresetFeelend) =>
							AufgaabeParamAndere.KonstruktNaacNuzerMeldungZuEveOnline(
							SictNaacNuzerMeldungZuEveOnline.ErrorGenerel(
							-1, new SictNaacNuzerMeldungZuEveOnlineCause(OverviewMissingPreset: OverviewPresetFeelend)))).ToArray();

					ListePrioMengeAufgaabe.Add(
						new SictAufgaabeGrupePrio(
						MengeMeldungAufgaabeParam,
						"Meldung Feeler ListeOverviewPresetFeelend"));
				}

				if (null != ScnapscusWindowOverview)
				{
					if (!(3 <= ScnapscusWindowOverview.ListeTabNuzbar.CountNullable()))
					{
						ListePrioMengeAufgaabe.Add(
							new SictAufgaabeGrupePrio(
							AufgaabeParamAndere.KonstruktNaacNuzerMeldungZuEveOnline(
							SictNaacNuzerMeldungZuEveOnline.ErrorGenerel(-1,
							new SictNaacNuzerMeldungZuEveOnlineCause(SictNaacNuzerMeldungZuEveOnlineCauseTypeBinär.OverviewTabCountTooLow))),
							"Meldung Feeler OverviewTabCountTooLow"));
					}
				}

				if (LanguageNotSetToEnglish ?? false)
				{
					ListePrioMengeAufgaabe.Add(
						new SictAufgaabeGrupePrio(
						AufgaabeParamAndere.KonstruktNaacNuzerMeldungZuEveOnline(
						SictNaacNuzerMeldungZuEveOnline.WarningGenerel(-1,
						new SictNaacNuzerMeldungZuEveOnlineCause(SictNaacNuzerMeldungZuEveOnlineCauseTypeBinär.LanguageNotSetToEnglish))),
						"Meldung Warnung LanguageNotSetToEnglish"));
				}

				if (VonNuzerParamAutoMissionFraigaabe ?? false)
				{
					if (!(DockedLezteAlterMili < 3333) &&
						FittingMengeModule.All((Module) => null != Module.ModuleButtonHintGültigMitZait.Wert) &&
						!FittingMengeModule.Any((Module) => Module.IstWirkmitelDestrukt ?? false))
					{
						//	Alle Module gemese und kaines davon isc Wirkmitel destrukt.

						ListePrioMengeAufgaabe.Add(
							new SictAufgaabeGrupePrio(
							AufgaabeParamAndere.KonstruktNaacNuzerMeldungZuEveOnline(
							SictNaacNuzerMeldungZuEveOnline.ErrorGenerel(-1, new SictNaacNuzerMeldungZuEveOnlineCause(SictNaacNuzerMeldungZuEveOnlineCauseTypeBinär.ShipFittingMissingWeapon))),
							"Meldung Feeler AutoMission ShipFittingMissingWeapon"));
					}
				}

			}

			if (null != ScnapscusMengeWindowZuBearbaiteVorrangigOrdnet)
			{
				foreach (var ScnapscusWindowZuBearbaiteVorrangigNääxte in ScnapscusMengeWindowZuBearbaiteVorrangigOrdnet)
				{
					//	Modale Window bearbaite

					var WindowZuBearbaiteVorrangigNääxte = (null == Gbs) ? null : Gbs.ZuHerkunftAdreseWindow(ScnapscusWindowZuBearbaiteVorrangigNääxte.Ident);

					if (null == WindowZuBearbaiteVorrangigNääxte)
					{
						//	Meldung Feeler
					}

					var WindowZuBearbaiteVorrangigNääxteTopCaptionText = ScnapscusWindowZuBearbaiteVorrangigNääxte.TopCaptionText;
					var WindowZuBearbaiteVorrangigNääxteCaption = ScnapscusWindowZuBearbaiteVorrangigNääxte.Caption;

					var NaacBerictMeldunWindowIdent = WindowZuBearbaiteVorrangigNääxteTopCaptionText ?? WindowZuBearbaiteVorrangigNääxteCaption;

					var ButtonGroup = ScnapscusWindowZuBearbaiteVorrangigNääxte.ButtonGroup;

					var ScnapscusMengeMessageBoxOberscteMengeButton =
						(null == ButtonGroup) ? null : ButtonGroup.MengeButton;

					var ButtonOk =
						ScnapscusMengeMessageBoxOberscteMengeButton
						.FirstOrDefaultNullable((KandidaatButton) => Regex.Match(KandidaatButton.Bescriftung ?? "", "ok", RegexOptions.IgnoreCase).Success);

					var ButtonYes =
						ScnapscusMengeMessageBoxOberscteMengeButton
						.FirstOrDefaultNullable((KandidaatButton) => Regex.Match(KandidaatButton.Bescriftung ?? "", "yes", RegexOptions.IgnoreCase).Success);

					var ButtonNo =
						ScnapscusMengeMessageBoxOberscteMengeButton
						.FirstOrDefaultNullable((KandidaatButton) => Regex.Match(KandidaatButton.Bescriftung ?? "", "no", RegexOptions.IgnoreCase).Success);

					var TypMessageBoxCantActivate =
						Optimat.EveOnline.Anwendung.SictMissionZuusctand.MessageBoxIstMeldungAccGateCantActivate(ScnapscusWindowZuBearbaiteVorrangigNääxte);

					var TypProceedDangerousActMatch = Regex.Match(WindowZuBearbaiteVorrangigNääxteTopCaptionText ?? "", "Proceed with this dangerous act", RegexOptions.IgnoreCase);
					var TypAbandonLootMatch = Regex.Match(WindowZuBearbaiteVorrangigNääxteTopCaptionText ?? "", "Abandon loot", RegexOptions.IgnoreCase);
					var TypQuitMissionMatch = Regex.Match(WindowZuBearbaiteVorrangigNääxteTopCaptionText ?? "", "Quit Mission", RegexOptions.IgnoreCase);
					var TypDeclineMissionMatch = Regex.Match(WindowZuBearbaiteVorrangigNääxteTopCaptionText ?? "", "Decline Mission", RegexOptions.IgnoreCase);

					var TypLocationSettingsMatch = Regex.Match(WindowZuBearbaiteVorrangigNääxteCaption ?? "", "Location Settings", RegexOptions.IgnoreCase);

					var AufgaabeMenuEntryAbgescloseTailWirkungLezteMitZait = this.AufgaabeMenuEntryAbgescloseTailWirkungLezteMitZait;

					var AufgaabeMenuEntryAbgescloseTailWirkungLezte = AufgaabeMenuEntryAbgescloseTailWirkungLezteMitZait.Wert;

					var AufgaabeMenuEntryAbgescloseTailWirkungLezteBescriftung =
						(null == AufgaabeMenuEntryAbgescloseTailWirkungLezte) ? null : AufgaabeMenuEntryAbgescloseTailWirkungLezte.Bescriftung;

					var AufgaabeMenuEntryAbgescloseTailWirkungLezteAlterMili = NuzerZaitMili - AufgaabeMenuEntryAbgescloseTailWirkungLezteMitZait.Zait;

					bool AnforderungAbandonLoot = false;
					bool AnforderungDeclineMission = false;
					bool AnforderungQuitMission = false;

					bool? EntscaidungYesNo = null;

					if (null != AufgaabeMenuEntryAbgescloseTailWirkungLezteBescriftung &&
						AufgaabeMenuEntryAbgescloseTailWirkungLezteAlterMili < 4444)
					{
						/*
						 * 2014.04.24
						 * (T:\Günta\Projekt\Optimat.EveOnline\Markt\von Nuzer Berict\2014.04.24.10 Test durc curyl\Auswert\Temp von Server Berict\[ZAK=2014.04.24.10.06.51,NB=8].Anwendung.Berict)
						 * Beobactung Probleem:
						 * 
						 * Der pasende MenuEntry enthiilt tatsäclic deen Text "Abandon Container" (und Matched daher nit mit deem bisher verwendete Pattern)
						 * 
						if (Regex.Match(AufgaabeMenuEntryAbgescloseTailWirkungLezteBescriftung, "Abandon\\s*(wreck|cargo)").Success)
						 * */
						if (Regex.Match(AufgaabeMenuEntryAbgescloseTailWirkungLezteBescriftung, "Abandon", RegexOptions.IgnoreCase).Success)
						{
							AnforderungAbandonLoot = true;
						}
					}

					SictAufgaabeParam WindowBearbaitungAufgaabeParam = null;

					if (TypLocationSettingsMatch.Success)
					{
					}

					if (null == WindowBearbaitungAufgaabeParam)
					{
						if (null == ScnapscusMengeMessageBoxOberscteMengeButton)
						{
						}
						else
						{
							GbsElementMitBescriftung ButtonZuBetäätige = null;

							if (TypMessageBoxCantActivate)
							{
								ButtonZuBetäätige = ButtonOk;
							}

							if (TypAbandonLootMatch.Success)
							{
								EntscaidungYesNo = AnforderungAbandonLoot;
							}

							if (TypDeclineMissionMatch.Success)
							{
								EntscaidungYesNo = AnforderungDeclineMission;
							}

							if (TypQuitMissionMatch.Success)
							{
								EntscaidungYesNo = AnforderungQuitMission;
							}

							if (TypProceedDangerousActMatch.Success)
							{
								EntscaidungYesNo = false;
							}

							if (EntscaidungYesNo.HasValue)
							{
								if (EntscaidungYesNo.Value)
								{
									ButtonZuBetäätige = ButtonYes;
								}
								else
								{
									ButtonZuBetäätige = ButtonNo;
								}
							}

							if (null == ButtonZuBetäätige)
							{
								if (Optimat.Glob.MengeRegexPatternEnthaltSuccessFürString(
									WindowZuBearbaiteVorrangigNääxteTopCaptionText,
									MengeMessageBoxOkCaptionRegexPattern,
									RegexOptions.IgnoreCase))
								{
									ButtonZuBetäätige = ButtonOk;
								}

								if (Optimat.Glob.MengeRegexPatternEnthaltSuccessFürString(
									WindowZuBearbaiteVorrangigNääxteTopCaptionText,
									MengeMessageBoxNoCaptionRegexPattern,
									RegexOptions.IgnoreCase))
								{
									ButtonZuBetäätige = ButtonNo;
								}

							}

							if (null != ButtonZuBetäätige)
							{
								WindowBearbaitungAufgaabeParam =
									SictAufgaabeParam.KonstruktAufgaabeParam(
										AufgaabeParamAndere.KonstruktMausPfaad(
										SictAufgaabeParamMausPfaad.KonstruktMausKlikLinx(ButtonZuBetäätige)),
										new string[]{
											"Button[" + (ButtonZuBetäätige.Bescriftung ?? "") + "]"});
							}
						}
					}

					if (null == WindowBearbaitungAufgaabeParam)
					{
						WindowBearbaitungAufgaabeParam = AufgaabeParamAndere.KonstruktWindowMinimize(WindowZuBearbaiteVorrangigNääxte);
					}

					if (null == WindowBearbaitungAufgaabeParam)
					{
						//	Meldung Feeler
					}
					else
					{
						ListePrioMengeAufgaabe.Add(new SictAufgaabeGrupePrio(
							new SictAufgaabeParam[]{
								SictAufgaabeParam.KonstruktAufgaabeParam(
								WindowBearbaitungAufgaabeParam,
								new string[]{
									"Window[" + (NaacBerictMeldunWindowIdent ?? "") + "]"})},
									"Message Box bearbaite"));

						break;
					}
				}
			}

			//	Aufgaabe Info Panel Expand unbedingt ainfüüge, fals diis zu viil Recenzait beanspruct umsctele z.B. auf Enumerable bzw Lazy initialized SictAufgaabeGrupePrio
			if (null != MengeInfoPanelExpandSol)
			{
				ListePrioMengeAufgaabe.Add(new SictAufgaabeGrupePrio(
					MengeInfoPanelExpandSol.Select(InfoPanelTyp => AufgaabeParamAndere.KonstruktInfoPanelExpand(InfoPanelTyp)).ToArray(),
					"Info Panel Expand"));
			}

			if (true == SelbsctShipDocked)
			{
				if (null != ScnapscusCurrentLocationInfo)
				{
					if (Bib3.Extension.NullOderLeer(ScnapscusCurrentLocationInfo.NearestName))
					{
						ListePrioMengeAufgaabe.Add(new SictAufgaabeGrupePrio(
							new SictAufgaabeParam[]{
									AufgaabeParamAndere.KonstruktNaacNuzerMeldungZuEveOnline(
									SictNaacNuzerMeldungZuEveOnline.ErrorGenerel(-1, SictNaacNuzerMeldungZuEveOnlineCauseTypeBinär.InfoPanelLocationNearestMissing))},
							"Naac Nuzer Meldung InfoPanelLocationNearestMissing"));
					}
				}
			}

			if (null != OverviewPresetZuLaadeIdent)
			{
				//	Type Selection in gerade aktuelem Tab ainsctele.

				if (null != ScnapscusWindowOverviewTabSelected)
				{
					var ScnapscusWindowOverviewTabSelectedIdent = ScnapscusWindowOverviewTabSelected.LabelBescriftung;

					ListePrioMengeAufgaabe.Add(new SictAufgaabeGrupePrio(
						new AufgaabeParamOverviewPresetLaade(OverviewPresetTyp.Preset, OverviewPresetZuLaadeIdent, ScnapscusWindowOverviewTabSelectedIdent),
						"Overview Preset laade"));
				}
			}

			var AmmoLoadNaacrangigMengeAufgaabeParam = new List<SictAufgaabeParam>();

			{
				if (!(FittingAmmoLoadLezteAlterMili < 11000) &&
					!(true == SelbsctShipCloaked))
				{
					//	Für ale Module welce Wirkmitel Destrukt sind pasende Ammo laade

					var AmmoLoadMengeAufgaabeParam = new List<SictAufgaabeParam>();

					foreach (var Module in FittingMengeModule)
					{
						if (null == Module)
						{
							continue;
						}

						if (Module.AktiivBerecne())
						{
							continue;
						}

						if (!Module.SictbarBerecne())
						{
							continue;
						}

						var AusShipUiModuleRepr = Module.ScnapscusLezteModuleReprMitZait;

						if (null == AusShipUiModuleRepr.Wert)
						{
							continue;
						}

						var ModuleButtonHintGültigMitZait = Module.ModuleButtonHintGültigMitZait;

						var ModuleButtonHintGültig = ModuleButtonHintGültigMitZait.Wert;

						if (null == ModuleButtonHintGültig)
						{
							//	ModuleButtonHint werd an anderer Sctele gemese.
							continue;
						}

						if (!(true == ModuleButtonHintGültig.IstWirkmitelDestrukt))
						{
							continue;
						}

						var BisherChargeLoaded = ModuleButtonHintGültig.ChargeLoaded;

						var BisherModuleDamage = new SictDamageMitBetraagIntValue();

						{
							var MengeDamage = Module.MengeDamageBerecne();

							if (!Bib3.Extension.NullOderLeer(MengeDamage))
							{
								BisherModuleDamage =
									MengeDamage
									.Where((Kandidaat) => Kandidaat.DamageType.HasValue)
									.OrderByDescending((Kandidaat) => Kandidaat.BetraagInt ?? -1).First();
							}
						}

						var ModuleButtonFürMenuWurzelFläce = Module.ToggleFläceBerecne();

						var BisherModuleDamageTypeNulbar = BisherModuleDamage.DamageType;

						var BisherModuleDamageTypeRangPrio =
							Optimat.Glob.FrühesteIndex(FürGefectListePrioMengeDamageTypePrioOrdnet,
								(t) => t.Key.Any((DamageType) => DamageType == BisherModuleDamageTypeNulbar)) ?? int.MaxValue;

						var ModuleMenuLezteMitZait = Module.MenuLezteScpezModuleButtonMitZait;

						var ModuleMenuLezte = ModuleMenuLezteMitZait.Wert;
						var ModuleMenuLezteAlterMili = NuzerZaitMili - ModuleMenuLezteMitZait.Zait;
						var ModuleMenuLezteAlter = ModuleMenuLezteAlterMili / 1000;

						var ModuleMenuLezteJüngerAlsModuleButtonHint = ModuleButtonHintGültigMitZait.Zait < ModuleMenuLezteMitZait.Zait;
						var ModuleMenuLezteJüngerAlsAmmoLoadLezte = ModuleButtonHintGültigMitZait.Zait < FittingAmmoLoadLezteBeginZait;

						var ModuleMenuLezteMengeZuAmmoDamageTypeMenuEntry =
							(null == ModuleMenuLezte) ? null :
							ModuleMenuLezte.MengeZuAmmoDamageTypeMenuEntry;

						var ModuleMenuLezteMengeZuDamageTypePrioMengeMenuEntryOrdnetNaacPrio =
							(null == ModuleMenuLezteMengeZuAmmoDamageTypeMenuEntry) ? null :
							ModuleMenuLezteMengeZuAmmoDamageTypeMenuEntry
							.GroupBy((DamageTypeUndMenuEntry) =>
								Optimat.Glob.FrühesteIndex(FürGefectListePrioMengeDamageTypePrioOrdnet,
								(t) => t.Key.Any((DamageType) => DamageType == DamageTypeUndMenuEntry.Key)) ?? int.MaxValue)
							.OrderBy((GrupeDamageTypeUndMenuEntry) => GrupeDamageTypeUndMenuEntry.Key)
							.ToArray();

						var ModuleMenuLezteMengeZuDamageTypePrioHööcsteMengeMenuEntry =
							(null == ModuleMenuLezteMengeZuDamageTypePrioMengeMenuEntryOrdnetNaacPrio) ? null :
							ModuleMenuLezteMengeZuDamageTypePrioMengeMenuEntryOrdnetNaacPrio.FirstOrDefault();

						var ModuleDamageTypeÄndere = false;

						try
						{
							if (BisherModuleDamageTypeNulbar.HasValue)
							{
								//	Es isc beraits hinraicend Ammo gelaade um Wirkmitel zu aktiviire.

								if (Bib3.Extension.NullOderLeer(FürGefectListePrioMengeDamageTypePrioOrdnet))
								{
									//	Aus Konfig isc kaine Priorisatioon für DamageType vorgegebe.
									continue;
								}

								if (FürGefectPrioMengeDamageType.Any((DamageType) => DamageType == BisherModuleDamageTypeNulbar))
								{
									//	bisher gelaadene DamageType isc glaic hööcst Priorisiirte DamageType.
									continue;
								}
							}

							if (null != ModuleMenuLezte &&
								ModuleMenuLezteAlter < 900 &&
								(ModuleMenuLezteJüngerAlsModuleButtonHint || ModuleMenuLezteJüngerAlsAmmoLoadLezte))
							{
								if (null == ExtractFromOldAssembly.Bib3.Extension.CountNullable(ModuleMenuLezteMengeZuDamageTypePrioHööcsteMengeMenuEntry))
								{
									//	kain Menu Entry zu Damage Type vorhande. Diis isc z.B. dan der Fal wen nur ain Typ von Munitioon in Cargo und diiser beraits in Module gelaade.
									continue;
								}

								if (0 < ExtractFromOldAssembly.Bib3.Extension.CountNullable(ModuleMenuLezteMengeZuDamageTypePrioHööcsteMengeMenuEntry))
								{
									if (!(ModuleMenuLezteMengeZuDamageTypePrioHööcsteMengeMenuEntry.Key < BisherModuleDamageTypeRangPrio))
									{
										//	Der bisher gelaadene DamageType hat scon mindesctens glaic hohe Prio wii der in zulezt gesictete Menu verfüügbaare DamageType.
										continue;
									}
								}
							}

							ModuleDamageTypeÄndere = true;
						}
						finally
						{
							VonSensor.MenuEntry MenuEntry = null;

							bool AnforderungMenu = false;

							if (ModuleDamageTypeÄndere)
							{
								AnforderungMenu = true;

								if (null != ModuleMenuLezteMengeZuDamageTypePrioHööcsteMengeMenuEntry)
								{
									MenuEntry = ModuleMenuLezteMengeZuDamageTypePrioHööcsteMengeMenuEntry.FirstOrDefault().Value;
								}

							}
							else
							{
								//	Prüüfe ob reload sinvol

								var ModuleAnnaameChargeAnzaalScrankeMax = Module.AnnaameChargeAnzaalScrankeMax;
								var ModuleAnnaameChargeAnzaalScrankeMaxScrankeMin = Module.AnnaameChargeAnzaalScrankeMaxScrankeMin;
								var ModuleChargeAnzaal = Module.ChargeAnzaal;

								var AnnaameReloadDauer = Module.AnnaameReloadDauer ?? 10;

								var AnnaameReloadAllVerfüügbar = ModuleChargeAnzaal < ModuleAnnaameChargeAnzaalScrankeMaxScrankeMin;

								var JammedDauerRestHinraicendLang =
									(true == SelbsctShipJammed) &&
									(AnnaameReloadDauer < ((null == ScnapscusShipUi ? null : ScnapscusShipUi.MengeTimerDauerRestMinimumMiliBerecne()) - 3333) / 1000);

								if ((!(MengeObjektZuDestruiireNääxteDistance < AnnaameModuleDestruktRangeMax + 14444) ||
									JammedDauerRestHinraicendLang) &&
									((4 < ModuleAnnaameChargeAnzaalScrankeMaxScrankeMin - ModuleChargeAnzaal) ||
									(!ModuleAnnaameChargeAnzaalScrankeMax.HasValue)))
								{
									if (AnnaameReloadAllVerfüügbar)
									{
										AnforderungMenu = true;
									}

									if (null == ModuleMenuLezteMitZait.Wert)
									{
										AnforderungMenu = true;
									}
									else
									{
										MenuEntry = ModuleMenuLezteMitZait.Wert.ReloadAllMenuEntry;
									}
								}
							}

							{
								//	2015.05.00	Änderung für Turret.

								if (null == MenuEntry &&
									(ModuleButtonHintGültig?.IstWirkmitelDestrukt ?? false) &&
									!(ModuleButtonHintGültig?.ChargeLoaded ?? false))
								{
									//	irgendaine Charge laade. Nuzer mus daher vorersct beraits baim Fitting di Auswaal der Ammo bescranke.

									var MenuEntryChargeLoad =
										ModuleMenuLezteMitZait.Wert?.ReloadAllMenuEntry ??
										ModuleMenuLezte?.GbsMenu?.ListeEntry?.FirstOrDefault(Entry => Regex.Match(Entry?.Bescriftung ?? "",
										EveO.Nuzer.TempBot.ScpezEveOnln.AuswertGbs.Extension.ModuleTurretMenuEntryChargeRegexPattern, RegexOptions.IgnoreCase).Success);

									if (null != MenuEntryChargeLoad)
									{
										MenuEntry = MenuEntryChargeLoad;
										ModuleDamageTypeÄndere = true;
									}
								}
							}

							var MenuEntryText = (null == MenuEntry) ? null : MenuEntry.Bescriftung;

							var MenuEntryRegexPattern =
								(null == MenuEntryText) ? null : Regex.Escape(MenuEntryText);

							if (null != MenuEntryRegexPattern ||
								AnforderungMenu)
							{
								//	Anforderung Ammo load.

								var AufgaabeParam =
									AufgaabeParamAndere.AufgaabeAktioonMenu(Module.GbsObjektToggleBerecne(),
									new SictAnforderungMenuKaskaadeAstBedingung(MenuEntryRegexPattern, true),
									new string[] { "load Ammo[" + (MenuEntryText ?? "") + "]" });

								if (ModuleDamageTypeÄndere)
								{
									AmmoLoadMengeAufgaabeParam.Add(AufgaabeParam);
								}
								else
								{
									AmmoLoadNaacrangigMengeAufgaabeParam.Add(AufgaabeParam);
								}
							}
						}
					}

					if (0 < AmmoLoadMengeAufgaabeParam.Count)
					{
						ListePrioMengeAufgaabe.Add(new SictAufgaabeGrupePrio(AmmoLoadMengeAufgaabeParam.ToArray(), "Ammo Load"));
					}
				}
			}

			if (null != InRaumAktioonUndGefectMengeTargetZuUnLockeMitPrio &&
				true == InRaumAktioonFortsaz)
			{
				//	Target UnLocke

				var MengeAufgaabeTargetUnLock = new List<SictAufgaabeParam>();

				foreach (var ScnapscusTargetUndPrio in InRaumAktioonUndGefectMengeTargetZuUnLockeMitPrio)
				{
					var TargetZuusctand =
						ExtractFromOldAssembly.Bib3.Extension.FirstOrDefaultNullable(
						MengeTargetNocSictbar, (KandidaatTarget) => KandidaatTarget.PasendZuBisherige(ScnapscusTargetUndPrio.Key));

					if (null == TargetZuusctand)
					{
						continue;
					}

					MengeAufgaabeTargetUnLock.Add(AufgaabeParamAndere.KonstruktTargetUnLock(TargetZuusctand));
				}

				ListePrioMengeAufgaabe.Add(new SictAufgaabeGrupePrio(MengeAufgaabeTargetUnLock.ToArray(), "Target.UnLock"));
			}

			ListePrioMengeAufgaabe.AddRange(InRaumAktioonUndGefectListePrioMengeAufgaabe.EmptyIfNull());

			if (0 < AmmoLoadNaacrangigMengeAufgaabeParam.Count)
			{
				ListePrioMengeAufgaabe.Add(new SictAufgaabeGrupePrio(AmmoLoadNaacrangigMengeAufgaabeParam.ToArray(), "Ammo Load Naacrangig"));
			}

			{
				//	Neocom scliise
			}

			if (!VonNuzerParamSimuMausAufWindowVordersteEkeOderKanteIndex.HasValue)
			{
				//	Scliise nit benöötigte Window, diis reduziirt auc Menge der von Nuzer zu sendende Daate (GBS Baum)

				var MengeKandidaatOklusioon = MengeKandidaatOklusioonBerecne();

				var MengeWindowZuErhalte = this.MengeWindowZuErhalte;

				var MengeKandidaatOklusioonFiltert =
					MengeKandidaatOklusioon
					.WhereNullable((KandidaatOklusioon) =>
						{
							var KandidaatOklusioonWindow = KandidaatOklusioon.Window;

							if (null == KandidaatOklusioonWindow)
							{
								return null != KandidaatOklusioon.PanelGroup;
							}

							if (false)  //	2015.02.29	Berüksictigung WindowStack
							{
								if (KandidaatOklusioonWindow.AingangScnapscusTailObjektIdentLezteBerecne() is VonSensor.WindowInventoryPrimary)
								{
									if (VonNuzerParamAutoMineFraigaabe ?? false)
									{
										return false;
									}
								}

								if (KandidaatOklusioonWindow.AingangScnapscusTailObjektIdentLezteBerecne() is VonSensor.WindowSurveyScanView)
								{
									if (VonNuzerParamAutoMineFraigaabe ?? false)
									{
										return false;
									}
								}
							}
							else
							{
								var KandidaatOklusioonWindowEnthalteMengeWindowZuusctand =
									Gbs.MengeWindowEnthalteInWindow(KandidaatOklusioonWindow)
									.WhereNotDefault()
									.ToArrayNullable();

								var KandidaatOklusioonWindowEnthalteMengeWindowVonSensor =
									KandidaatOklusioonWindowEnthalteMengeWindowZuusctand
									.SelectNullable((WindowZuusctand) => WindowZuusctand.ScnapscusWindowLezte)
									.ToArrayNullable();

								if (VonNuzerParamAutoMineFraigaabe ?? false &&
									null != KandidaatOklusioonWindowEnthalteMengeWindowVonSensor)
								{
									if (KandidaatOklusioonWindowEnthalteMengeWindowVonSensor
										.AnyNullable((Window) =>
										Window is VonSensor.WindowInventoryPrimary ||
										Window is VonSensor.WindowSurveyScanView) ?? false)
									{
										return false;
									}
								}
							}

							return !MengeWindowZuErhalte.Contains(KandidaatOklusioonWindow);
						})
					/*
					2015.09.01
					Änderung InGbsBaumAstIndex: Übernaame von noie Sensor InTreeIndex: Element occludes other Elements with lower Value.
					.OrderByNullable((Kandidaat) => Kandidaat.GbsAstInBaumIndex ?? int.MaxValue)
					*/
					?.OrderByDescending(Kandidaat => Kandidaat.GbsAstInBaumIndex ?? int.MaxValue)
					.ToArrayFalsNitLeer();

				if (null != MengeKandidaatOklusioonFiltert)
				{
					ListePrioMengeAufgaabe.Add(new SictAufgaabeGrupePrio(
						MengeKandidaatOklusioonFiltert.Select((KandidaatOklusioon) =>
						new AufgaabeParamGbsElementVerberge(KandidaatOklusioon.GbsElementScnapscus)).ToArray(),
						"nit benöötigte Window.scliise"));
				}
			}

			ListePrioMengeAufgaabe.Add(new SictAufgaabeGrupePrio(new AufgaabeParamScteleSicerOverviewSortNaacDistance(), "Overview Sort for Distance"));

			SictAufgaabeGrupePrio AufgaabeGrupeOverviewTabAktiviire = null;

			if (null != ScnapscusWindowOverview &&
				null != OverviewTabZuAktiviireFürMaidungScroll &&
				(VonNuzerParamAutoMissionFraigaabe ?? false))
			{
				AufgaabeGrupeOverviewTabAktiviire =
					new SictAufgaabeGrupePrio(
						AufgaabeParamAndere.KonstruktOverviewTabAktiviire(OverviewTabZuAktiviireFürMaidungScroll, new string[]{
							"activate Overview Tab which is suited for requested Type selection"}),
							"Overview Tab aktiviire");

				ListePrioMengeAufgaabe.Add(AufgaabeGrupeOverviewTabAktiviire);
			}

			var ScnapscusMengeWindowInventory = (null == AusScnapscusAuswertungZuusctand) ? null : AusScnapscusAuswertungZuusctand.MengeWindowInventory;

			if (null != ScnapscusMengeWindowInventory)
			{
				var InInventoryAinklapeNictBenöötigteAstListeAufgaabeParam = new List<SictAufgaabeParam>();
				var NaacNuzerWarnungInventoryPfaadAindoitigNictListeAufgaabeParam = new List<SictAufgaabeParam>();

				foreach (var ScnapscusWindowInventory in ScnapscusMengeWindowInventory)
				{
					if (null == ScnapscusWindowInventory)
					{
						continue;
					}

					var AuswaalReczObjektPfaadListeAst = ScnapscusWindowInventory.AuswaalReczObjektPfaadListeAst;
					var ZuAuswaalReczMengeKandidaatLinxTreeEntry = ScnapscusWindowInventory.ZuAuswaalReczMengeKandidaatLinxTreeEntry;
					var ScnapscusWindowInventoryLinxTreeListeEntry = ScnapscusWindowInventory.LinxTreeListeEntry;

					if (null != ScnapscusWindowInventoryLinxTreeListeEntry)
					{
						var ScnapscusWindowInventoryLinxTreeListeEntryNitBenöötigt =
							ScnapscusWindowInventoryLinxTreeListeEntry
							.Where((Kandidaat) => !(Kandidaat == ScnapscusWindowInventory.LinxTreeEntryActiveShip))
							.ToArray();

						foreach (var LinxTreeEntryNitBenöötigt in ScnapscusWindowInventoryLinxTreeListeEntryNitBenöötigt)
						{
							if (!(0 < LinxTreeEntryNitBenöötigt.MengeChild.CountNullable()))
							{
								continue;
							}

							var LinxTreeEntryNitBenöötigtExpandCollapseToggleFläce = LinxTreeEntryNitBenöötigt.ExpandCollapseToggleFläce;

							if (null == LinxTreeEntryNitBenöötigtExpandCollapseToggleFläce)
							{
							}
							else
							{
								InInventoryAinklapeNictBenöötigteAstListeAufgaabeParam.Add(
									SictAufgaabeParam.KonstruktAufgaabeParam(
									AufgaabeParamAndere.KonstruktMausPfaad(SictAufgaabeParamMausPfaad.KonstruktMausKlikLinx(LinxTreeEntryNitBenöötigtExpandCollapseToggleFläce)),
									"In Inventory collapse unnecessary branch"));
							}
						}
					}

					if (null == AuswaalReczObjektPfaadListeAst || null == ZuAuswaalReczMengeKandidaatLinxTreeEntry)
					{
						continue;
					}

					if (!(1 < ZuAuswaalReczMengeKandidaatLinxTreeEntry.CountNullable()))
					{
						continue;
					}

					var AuswaalReczObjektPfaadAgregatioon = string.Join(".", AuswaalReczObjektPfaadListeAst.Select((Kandidaat) => Kandidaat ?? ""));

					NaacNuzerWarnungInventoryPfaadAindoitigNictListeAufgaabeParam.Add(AufgaabeParamAndere.KonstruktNaacNuzerMeldungZuEveOnline(
						SictNaacNuzerMeldungZuEveOnline.WarningGenerel(-1,
						new SictNaacNuzerMeldungZuEveOnlineCause(CauseText: "in Inventory Path \"" + AuswaalReczObjektPfaadAgregatioon + "\" not unique"))));
				}

				ListePrioMengeAufgaabe.Add(
					new SictAufgaabeGrupePrio(InInventoryAinklapeNictBenöötigteAstListeAufgaabeParam.ToArrayFalsNitLeer(), "In Inventory Linx ainklape nit benöötigte Ast"));

				ListePrioMengeAufgaabe.Add(
					new SictAufgaabeGrupePrio(NaacNuzerWarnungInventoryPfaadAindoitigNictListeAufgaabeParam.ToArrayFalsNitLeer(), "Naac Nuzer Warnung Inventory Pfaad Aindoitig nict"));
			}

			if (null != VonNuzerParamSimu)
			{
				if (VonNuzerParamSimu.AufgaabeOverviewScroll ?? false)
				{
					ListePrioMengeAufgaabe.Add(
					new SictAufgaabeGrupePrio(
						SictAufgaabeParam.KonstruktAufgaabeParam(
						AufgaabeParamAndere.KonstruktInOverviewTabFolgeViewportDurcGrid(),
						"Simulation.InOverviewScroll"),
						"Simu.AufgaabeOverviewScroll"));
				}

				if (VonNuzerParamSimuMausAufWindowVordersteEkeOderKanteIndex.HasValue)
				{
					var IstKante = 1 == ((VonNuzerParamSimuMausAufWindowVordersteEkeOderKanteIndex / 4) % 2);

					var EkeIndex = (int)VonNuzerParamSimuMausAufWindowVordersteEkeOderKanteIndex.Value.SictUmgebrocen(0, 4);

					var WindowTopmost =
						AusScnapscusAuswertungZuusctand
						.MengeWindowBerecne()
						/*
						2015.09.01
						Änderung InGbsBaumAstIndex: Übernaame von noie Sensor InTreeIndex: Element occludes other Elements with lower Value.
							.OrderByNullable((Kandidaat) => Kandidaat.InGbsBaumAstIndex ?? int.MaxValue)
							*/
						?.OrderByDescending(Kandidaat => Kandidaat.InGbsBaumAstIndex ?? int.MaxValue)
						.FirstOrDefaultNullable();

					var WindowTopmostInGbsFläce = WindowTopmost.InGbsFläce;

					if (null != WindowTopmostInGbsFläce)
					{
						var ZiilLaage =
							IstKante ?
							(WindowTopmostInGbsFläce.ListeEkeLaage().ElementAtOrDefaultNullable(EkeIndex) +
							WindowTopmostInGbsFläce.ListeEkeLaage().ElementAtOrDefaultNullable((int)(EkeIndex + 1).SictUmgebrocen(0, 4))) / 2 :
							WindowTopmostInGbsFläce.ListeEkeLaage().ElementAtOrDefaultNullable(EkeIndex);

						var ZiilFläce = OrtogoonInt.AusPunktZentrumUndGrööse(ZiilLaage, new Vektor2DInt(4, 4));

						var MouseZiilFäce = SictGbsWindowZuusctand.FläceProjeziirtAufGbsAst(ZiilFläce, WindowTopmost);

						ListePrioMengeAufgaabe.Add(
							new SictAufgaabeGrupePrio(
								SictAufgaabeParam.KonstruktAufgaabeParam(
								AufgaabeParamAndere.KonstruktMausPfaad(SictAufgaabeParamMausPfaad.Konstrukt(MouseZiilFäce)),
								"Simulation.Mouse Move to Topmost Window Corner"),
								"Simu.AufgaabeMouseMoveWindowTopmostCornerIndex"));
					}
				}
			}

			return ListePrioMengeAufgaabe.ToArray();
		}

	}
}
