using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Optimat.ScpezEveOnln;

namespace Optimat.EveOnline.Anwendung
{
	[JsonObject(MemberSerialization.OptIn)]
	public class AufgaabeParamShipStop : SictAufgaabeParam
	{
		public AufgaabeParamShipStop()
		{
		}

		override	public SictAufgaabeParamZerleegungErgeebnis Zerleege(
			ISictAutomatZuusctand AutomaatZuusctand,
			SictAufgaabeKombiZuusctand KombiZuusctand)
		{
			return ZerleegeShipStop(AutomaatZuusctand);
		}

		override public IEnumerable<string> ZwekListeKomponenteAusParamBerecne()
		{
			return new string[] { "stop Ship" };
		}

		override public bool HinraicendGlaicwertigFürFortsazScpez(
			SictAufgaabeParam Andere)
		{
			var AndereScpez = Andere as AufgaabeParamShipStop;

			if (null == AndereScpez)
			{
				return false;
			}

			return true;
		}

		static public SictAufgaabeParamZerleegungErgeebnis ZerleegeShipStop(
			ISictAutomatZuusctand AutomaatZuusctand)
		{
			var AufgaabeParamZerleegungErgeebnis = new SictAufgaabeParamZerleegungErgeebnis(null,	false);

			if (null == AutomaatZuusctand)
			{
				return AufgaabeParamZerleegungErgeebnis;
			}

			var Scnapscus = AutomaatZuusctand.ListeScnapscusLezteAuswertungErgeebnisNaacSimu;

			if (null == Scnapscus)
			{
				return AufgaabeParamZerleegungErgeebnis;
			}

			var ScnapscusShipUi = Scnapscus.ShipUi;

			if (null == ScnapscusShipUi)
			{
				return AufgaabeParamZerleegungErgeebnis;
			}

			var SelbstShipZuusctand = Scnapscus.SelfShipState;

			if (null == SelbstShipZuusctand)
			{
				return AufgaabeParamZerleegungErgeebnis;
			}

			var FittingUndShipZuusctand = AutomaatZuusctand.FittingUndShipZuusctand;

			if (SelbstShipZuusctand.Warping ?? false)
			{
				return AufgaabeParamZerleegungErgeebnis;
			}

			if (SelbstShipZuusctand.SpeedDurcMeterProSekunde < 1)
			{
				AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAin();
			
				return AufgaabeParamZerleegungErgeebnis;
			}

			if (null != FittingUndShipZuusctand)
			{
				var AusShipUIIndicationLezteMitZait = FittingUndShipZuusctand.AusShipUIIndicationLezteMitZait;

				if (null != AusShipUIIndicationLezteMitZait)
				{
					if (null != AusShipUIIndicationLezteMitZait.Wert)
					{
						if (SictZuInRaumObjektManööverTypEnum.Stop == AusShipUIIndicationLezteMitZait.Wert.ManöverTyp)
						{
							AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAin();

							return AufgaabeParamZerleegungErgeebnis;
						}
					}
				}
			}

			var ButtonStop = ScnapscusShipUi.ButtonStop;

			if (null == ButtonStop)
			{
				return AufgaabeParamZerleegungErgeebnis;
			}

			AufgaabeParamZerleegungErgeebnis.FüügeAn(
				AufgaabeParamAndere.KonstruktMausPfaad(
				SictAufgaabeParamMausPfaad.KonstruktMausKlikLinx(ButtonStop)));

			AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAin();

			return AufgaabeParamZerleegungErgeebnis;
		}
	}
}
