using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bib3;
using Newtonsoft.Json;
using Optimat.ScpezEveOnln;
using ExtractFromOldAssembly.Bib3;

namespace Optimat.EveOnline.Anwendung
{
	[JsonObject(MemberSerialization.OptIn)]
	public class AufgaabeParamDestrukt : SictAufgaabeParam
	{
		[JsonProperty]
		readonly public SictOverViewObjektZuusctand ZiilOverviewObjekt;

		[JsonProperty]
		readonly public SictTargetZuusctand ZiilTarget;

		static	readonly	int	GefectListeLockedTargetScranke = 4;


		public AufgaabeParamDestrukt()
		{
		}

		public AufgaabeParamDestrukt(
			SictOverViewObjektZuusctand ZiilOverviewObjekt)
			:
			this(ZiilOverviewObjekt,	null)
		{
		}

		public AufgaabeParamDestrukt(
			SictOverViewObjektZuusctand ZiilOverviewObjekt,
			SictTargetZuusctand ZiilTarget)
		{
			this.ZiilOverviewObjekt = ZiilOverviewObjekt;
			this.ZiilTarget = ZiilTarget;
		}

		override public bool? AktioonWirkungDestruktVirt()
		{
			return true;
		}

		override public SictOverViewObjektZuusctand OverViewObjektZuBearbaiteVirt()
		{
			return this.ZiilOverviewObjekt;
		}

		override public SictTargetZuusctand TargetZuBearbaiteVirt()
		{
			return this.ZiilTarget;
		}

		override public SictAufgaabeParamZerleegungErgeebnis Zerleege(
			ISictAutomatZuusctand AutomaatZuusctand,
			SictAufgaabeKombiZuusctand KombiZuusctand)
		{
			return ZerleegeDestrukt(AutomaatZuusctand, KombiZuusctand, ZiilOverviewObjekt, ZiilTarget);
		}

		override public IEnumerable<string> ZwekListeKomponenteAusParamBerecne()
		{
			var ZwekListeKomponente = new List<string>();

			var ZiilTarget = this.ZiilTarget;
			var ZiilOverviewObjekt = this.ZiilOverviewObjekt;

			if (null != ZiilOverviewObjekt)
			{
				ZwekListeKomponente.Add("OverviewRow[" + (OverViewObjektSictZwekKomponente(ZiilOverviewObjekt) ?? "") + "]");
			}

			if (null != ZiilTarget)
			{
				ZwekListeKomponente.Add(ObjektSictZwekKomponente(ZiilTarget));
			}

			ZwekListeKomponente.Add(".destruct");

			return ZwekListeKomponente;
		}

		override public bool HinraicendGlaicwertigFürFortsazScpez(
			SictAufgaabeParam Andere)
		{
			var AndereScpez = Andere as AufgaabeParamDestrukt;

			if (null == AndereScpez)
			{
				return false;
			}

			return
				this.ZiilOverviewObjekt == AndereScpez.ZiilOverviewObjekt &&
				this.ZiilTarget == AndereScpez.ZiilTarget;
		}

		static public SictAufgaabeParamZerleegungErgeebnis ZerleegeDestrukt(
			ISictAutomatZuusctand AutomaatZuusctand,
			SictAufgaabeKombiZuusctand KombiZuusctand,
			SictOverViewObjektZuusctand ZiilOverviewObjekt,
			SictTargetZuusctand ZiilTarget)
		{
			var AufgaabeParamZerleegungErgeebnis = new SictAufgaabeParamZerleegungErgeebnis(null, false);

			if (null == AutomaatZuusctand)
			{
				return AufgaabeParamZerleegungErgeebnis;
			}

			var NuzerZaitMili = AutomaatZuusctand.NuzerZaitMili;

			var ZiilOverviewObjektSictungLezteAlterMili = NuzerZaitMili - ((null == ZiilOverviewObjekt) ? null : ZiilOverviewObjekt.SictungLezteZait);

			var Gbs = AutomaatZuusctand.Gbs;
			var OverviewUndTarget = AutomaatZuusctand.OverviewUndTarget;
			var FittingUndShipZuusctand = AutomaatZuusctand.FittingUndShipZuusctand;
			var InRaumAktioonUndGefect = AutomaatZuusctand.InRaumAktioonUndGefect;
			var AusScnapscusAuswertungZuusctand = AutomaatZuusctand.ListeScnapscusLezteAuswertungErgeebnisNaacSimu;

			var FürWirkungDestruktAufgaabeDroneEngageTarget = (null == InRaumAktioonUndGefect) ? null : InRaumAktioonUndGefect.FürWirkungDestruktAufgaabeDroneEngageTarget;

			var ScnapscusWindowDroneView = (null == AusScnapscusAuswertungZuusctand) ? null : AusScnapscusAuswertungZuusctand.WindowDroneView;

			var AnforderungDroneReturnLezte = (null == InRaumAktioonUndGefect) ? null : InRaumAktioonUndGefect.AnforderungDroneReturnLezte;

			var AnnaameDroneControlCountScrankeMaxNulbar = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.AnnaameDroneControlCountScrankeMax;

			var MengeModuleRepr = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.MengeModuleRepr;

			var MengeModuleUmscaltFraigaabe =
				AutomaatZuusctand.MengeModuleUmscaltFraigaabe
				.WhereNullable((KandidaatModule) => (null == KombiZuusctand) ? true : KombiZuusctand.ModuleVerwendungFraigaabe(KandidaatModule))
				.ToArrayNullable();

			var AnnaameTargetingDistanceScrankeMax = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.AnnaameTargetingDistanceScrankeMax;

			var AnnaameModuleDestruktRangeMax = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.AnnaameModuleDestruktRangeMax;
			var AnnaameModuleDestruktRangeOptimumNulbar = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.AnnaameModuleDestruktRangeOptimum;

			var AnnaameGefectDistanzOptimum = Bib3.Glob.Min(AnnaameModuleDestruktRangeOptimumNulbar, AnnaameTargetingDistanceScrankeMax);

			var ScritNääxteJammed = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.ScritNääxteJammed;
			var ScritÜüberNääxteJammed = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.ScritÜüberNääxteJammed;

			var ListeAbovemainMessageDronesLezteAlter = (null == Gbs) ? null : Gbs.ListeAbovemainMessageDronesLezteAlter;
			var MengeTargetNocSictbar = (null == OverviewUndTarget) ? null : OverviewUndTarget.MengeTargetNocSictbar;

			bool DroneLaunchVolsctändig = false;

			if (ListeAbovemainMessageDronesLezteAlter < 3e+4)
			{
				DroneLaunchVolsctändig = true;
			}

			if (!DroneLaunchVolsctändig &&
				0 < ExtractFromOldAssembly.Bib3.Extension.CountNullable(MengeTargetNocSictbar) || (ScritNääxteJammed ?? false))
			{
				//	Drones Launch

				if (false == AnforderungDroneReturnLezte &&
					null != ScnapscusWindowDroneView)
				{
					//	Drones Launch

					if (0 < ScnapscusWindowDroneView.DronesInBayAnzaal)
					{
						/*
						 * 2013.09.24
						 * Anforderung vorerst nur für Scpeziaalfal das nur fünf drones in space sain köne.
						 * Scpääter sol Anzaal nuzbaarer drones berüksictigt were.
						 * */
						if ((AnnaameDroneControlCountScrankeMaxNulbar ?? 5) <= ScnapscusWindowDroneView.DronesInLocalSpaceAnzaal)
						{
							DroneLaunchVolsctändig = true;
						}
						else
						{
							AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.AufgaabeAktioonDroneLaunch());
						}
					}
					else
					{
						DroneLaunchVolsctändig = true;
					}
				}
			}

			var MengeModuleAinSol =
				MengeModuleUmscaltFraigaabe.IntersectNullable(
				MengeModuleRepr.WhereNullable((KandidaatModule) =>
					(((true == KandidaatModule.IstWirkmitelDestrukt) &&
					(true == KandidaatModule.ChargeLoaded)) ||
					(true == KandidaatModule.IstTargetPainter)) &&
					!(true == KandidaatModule.AktiivBerecne(1))))
					.ToArrayNullable();

			if (null == ZiilTarget)
			{
				var MengeTargetPasend =
					OverviewUndTarget.MengeTargetTailmengePasendZuOverviewObjektBerecne(ZiilOverviewObjekt);

				if (null != MengeTargetPasend)
				{
					ZiilTarget = MengeTargetPasend.OrderBy((KandidaatTarget) => KandidaatTarget.SictungLezteDistanceScrankeMaxScpezTarget ?? int.MaxValue).FirstOrDefault();
				}
			}

			if (null == ZiilTarget)
			{
				if (null != ZiilOverviewObjekt)
				{
					if (!(GefectListeLockedTargetScranke <= ExtractFromOldAssembly.Bib3.Extension.CountNullable(MengeTargetNocSictbar)))
					{
						if (true == ZiilOverviewObjekt.TargetingOderTargeted &&
							7777 < ZiilOverviewObjektSictungLezteAlterMili)
						{
							AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.AufgaabeInOverViewMaceSictbar(ZiilOverviewObjekt));
						}
						else
						{
							var DistanceHinraicendGeringFürLock = true;

							if (0 < ExtractFromOldAssembly.Bib3.Extension.CountNullable(MengeTargetNocSictbar))
							{
								if (ZiilOverviewObjekt.SaitSictbarkaitLezteListeScritAnzaal < 1)
								{
									if (AnnaameTargetingDistanceScrankeMax < ZiilOverviewObjekt.SictungLezteDistanceScrankeMaxScpezOverview &&
										NuzerZaitMili - 4444 < ZiilOverviewObjekt.SictungLezteZait)
									{
										DistanceHinraicendGeringFürLock = false;
									}

									if (AnnaameGefectDistanzOptimum < ZiilOverviewObjekt.SictungLezteDistanceScrankeMaxScpezOverview)
									{
										DistanceHinraicendGeringFürLock = false;
									}
								}
							}

							if (DistanceHinraicendGeringFürLock)
							{
								AufgaabeParamZerleegungErgeebnis.FüügeAn(new AufgaabeParamLockTarget(ZiilOverviewObjekt));
							}
							else
							{
								AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAus();
							}
						}
					}

					if (!(ZiilOverviewObjekt.SictungLezteDistanceScrankeMaxScpezOverview < AnnaameGefectDistanzOptimum))
					{
						AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.AufgaabeDistanceAinzusctele(ZiilOverviewObjekt, null, AnnaameGefectDistanzOptimum));
					}
				}
			}
			else
			{
				var AnnaameDroneCommandRange = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.AnnaameDroneCommandRange;

				var TargetDistancePasendFürModule = false;
				var TargetDistancePasendFürDrone = false;

				//	!!!!	zu ergänze: Berecnung soldistance für Turret
				//	!!!!	zu ergänze: Berecnung TargetMengeModuleAinSol: untermenge von MengeModuleAinSol da Module untersciidlice optimaale Distance haabe (TargetPainter)
				if (ZiilTarget.SictungLezteDistanceScrankeMaxScpezTarget < AnnaameGefectDistanzOptimum)
				{
					TargetDistancePasendFürModule = true;
				}

				{
					AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.AufgaabeDistanceAinzusctele(ZiilTarget, null, AnnaameGefectDistanzOptimum));
				}

				if (ZiilTarget.SictungLezteDistanceScrankeMaxScpezTarget < AnnaameDroneCommandRange)
				{
					TargetDistancePasendFürDrone = true;
				}

				var DroneEngage =
					DroneLaunchVolsctändig &&
					TargetDistancePasendFürDrone &&
					true == FürWirkungDestruktAufgaabeDroneEngageTarget;

				if (DroneEngage || !MengeModuleAinSol.IsNullOrEmpty())
				{
					{
						if (TargetDistancePasendFürModule)
						{
							AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.KonstruktTargetInputFookusSeze(ZiilTarget));
						}
					}
				}

				if (!MengeModuleAinSol.IsNullOrEmpty())
				{
					//	Hiir werd nuur waitergemact wen noc mindesctens ain Module noc aigescaltet werde sol.

					if (true == ZiilTarget.InputFookusTransitioonLezteZiilWert)
					{
						if (TargetDistancePasendFürModule)
						{
							AufgaabeParamZerleegungErgeebnis.FüügeAn(
								MengeModuleAinSol.Select((ModuleAinSol) => AufgaabeParamAndere.KonstruktModuleScalteAin(ModuleAinSol)));
						}
					}
				}

				if (DroneEngage)
				{
					if (true == ZiilTarget.InputFookusTransitioonLezteZiilWert)
					{
						AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.KonstruktDronesEngage(ZiilTarget));
					}
				}
			}

			return AufgaabeParamZerleegungErgeebnis;
		}
	}
}
