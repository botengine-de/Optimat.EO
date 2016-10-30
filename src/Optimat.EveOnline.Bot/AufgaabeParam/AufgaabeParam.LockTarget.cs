using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Optimat.ScpezEveOnln;
using Bib3;
using System.Text.RegularExpressions;
using ExtractFromOldAssembly.Bib3;

namespace Optimat.EveOnline.Anwendung
{
	[JsonObject(MemberSerialization.OptIn)]
	public class AufgaabeParamLockTarget : SictAufgaabeParam
	{
		[JsonProperty]
		readonly public SictOverViewObjektZuusctand OverviewObjekt;

		public AufgaabeParamLockTarget()
		{
		}

		public AufgaabeParamLockTarget(
			SictOverViewObjektZuusctand OverviewObjekt)
		{
			this.OverviewObjekt = OverviewObjekt;
		}

		override public SictAufgaabeParamZerleegungErgeebnis Zerleege(
			ISictAutomatZuusctand AutomaatZuusctand,
			SictAufgaabeKombiZuusctand KombiZuusctand)
		{
			return ZerleegeLockTarget(AutomaatZuusctand, OverviewObjekt);
		}

		override public IEnumerable<string> ZwekListeKomponenteAusParamBerecne()
		{
			return new string[] { "Lock Target[" + SictAufgaabeParam.OverViewObjektSictZwekKomponente(OverviewObjekt) + "]" };
		}

		override public bool HinraicendGlaicwertigFürFortsazScpez(
			SictAufgaabeParam Andere)
		{
			var AndereScpez = Andere as AufgaabeParamLockTarget;

			if (null == AndereScpez)
			{
				return false;
			}

			return OverviewObjekt == AndereScpez.OverviewObjekt;
		}

		static public SictAufgaabeParamZerleegungErgeebnis ZerleegeLockTarget(
			ISictAutomatZuusctand AutomaatZuusctand,
			SictOverViewObjektZuusctand OverviewObjekt)
		{
			var AufgaabeParamZerleegungErgeebnis = new SictAufgaabeParamZerleegungErgeebnis(null, true);

			if (null == OverviewObjekt)
			{
				return AufgaabeParamZerleegungErgeebnis;
			}

			if (true == OverviewObjekt.TargetingOderTargeted)
			{
				return AufgaabeParamZerleegungErgeebnis;
			}

			AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAus();

			if (null == AutomaatZuusctand)
			{
				return AufgaabeParamZerleegungErgeebnis;
			}

			var FittingUndShipZuusctand = AutomaatZuusctand.FittingUndShipZuusctand;
			var OverviewUndTarget = AutomaatZuusctand.OverviewUndTarget;

			var MengeTargetNocSictbar = (null == OverviewUndTarget) ? null : OverviewUndTarget.MengeTargetNocSictbar;

			var MengeTargetAnzaalScrankeMax = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.MengeTargetAnzaalScrankeMax;
			var ScritNääxteJammed = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.ScritNääxteJammed;
			var ScritÜüberNääxteJammed = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.ScritÜüberNääxteJammed;

			if (ScritÜüberNääxteJammed ?? false)
			{
				return AufgaabeParamZerleegungErgeebnis;
			}

			if (MengeTargetAnzaalScrankeMax.HasValue)
			{
				if (MengeTargetAnzaalScrankeMax <= MengeTargetNocSictbar.CountNullable())
				{
					return AufgaabeParamZerleegungErgeebnis;
				}
			}

			var MenuKaskaadeLezte = AutomaatZuusctand.GbsMenuLezteInAst(OverviewObjekt);

			var MenuAstBedingung =
				new SictAnforderungMenuKaskaadeAstBedingung(SictShipZuusctandMitFitting.MenuEntryLockTargetRegexPattern, true);

			if (null != MenuKaskaadeLezte)
			{
				var MenuKaskaadeLezteAlter = AutomaatZuusctand.NuzerZaitMili - MenuKaskaadeLezte.BeginZait;

				var Menu0ListeEntry = MenuKaskaadeLezte.AusMenu0ListeEntryBerecne();

				if (!Menu0ListeEntry.IsNullOrEmpty())
				{
					var MenuEntryLock = MenuAstBedingung.AusMengeMenuEntryGibUntermengePasendFürFrüühestePasendePrio(Menu0ListeEntry);

					if (null == MenuEntryLock &&
						!(8888 < MenuKaskaadeLezteAlter))
					{
						//	Kain Entry "Lock" vorhande. Daher Vermuutung das Objekt zu wait entfernt für Lock.

						AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.AufgaabeDistanceAinzusctele(OverviewObjekt, null, 4444));
					}
				}

				if (!MenuKaskaadeLezte.EndeZait.HasValue)
				{
					//	Menu zu OverviewObjekt isc noc geöfnet.

					if (ScritNääxteJammed ?? false)
					{
						return AufgaabeParamZerleegungErgeebnis;
					}
				}
			}

			AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAin();

			AufgaabeParamZerleegungErgeebnis.FüügeAn(
				AufgaabeParamAndere.AufgaabeAktioonMenu(OverviewObjekt,
				MenuAstBedingung));

			return AufgaabeParamZerleegungErgeebnis;
		}
	}
}
