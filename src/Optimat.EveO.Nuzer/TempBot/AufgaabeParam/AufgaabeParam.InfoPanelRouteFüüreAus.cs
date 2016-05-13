using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Optimat.ScpezEveOnln;
using Bib3;
using Optimat.EveOnline.Base;
//using Optimat.EveOnline.AuswertGbs;
using Optimat.EveOnline.VonSensor;

namespace Optimat.EveOnline.Anwendung
{
	[JsonObject(MemberSerialization.OptIn)]
	public class AufgaabeParamInfoPanelRouteFüüreAus : SictAufgaabeParam
	{
		[JsonProperty]
		readonly public bool	LowSecFraigaabe;

		public AufgaabeParamInfoPanelRouteFüüreAus()
		{
		}

		public AufgaabeParamInfoPanelRouteFüüreAus(
			bool LowSecFraigaabe)
		{
			this.LowSecFraigaabe = LowSecFraigaabe;
		}

		override	public SictAufgaabeParamZerleegungErgeebnis Zerleege(
			ISictAutomatZuusctand AutomaatZuusctand,
			SictAufgaabeKombiZuusctand KombiZuusctand)
		{
			return ZerleegeInfoPanelRouteFüüreAus(AutomaatZuusctand, LowSecFraigaabe);
		}

		override public IEnumerable<string> ZwekListeKomponenteAusParamBerecne()
		{
			return new string[] { "in InfoPanel Route travel route" };
		}

		override public bool HinraicendGlaicwertigFürFortsazScpez(
			SictAufgaabeParam Andere)
		{
			var AndereScpez = Andere as AufgaabeParamInfoPanelRouteFüüreAus;

			if (null == AndereScpez)
			{
				return false;
			}

			return LowSecFraigaabe == AndereScpez.LowSecFraigaabe;
		}

		static public SictAufgaabeParamZerleegungErgeebnis ZerleegeInfoPanelRouteFüüreAus(
			ISictAutomatZuusctand AutomaatZuusctand,
			bool LowSecFraigaabe)
		{
			var AufgaabeParamZerleegungErgeebnis = new SictAufgaabeParamZerleegungErgeebnis(null, false);

			if (null == AutomaatZuusctand)
			{
				return AufgaabeParamZerleegungErgeebnis;
			}

			var NuzerZaitMili = AutomaatZuusctand.NuzerZaitMili;
			var SensorikScnapscus = AutomaatZuusctand.ListeScnapscusLezteAuswertungErgeebnisNaacSimu;
			var InRaumAktioonUndGefect = AutomaatZuusctand.InRaumAktioonUndGefect;
			var FittingUndShipZuusctand = AutomaatZuusctand.FittingUndShipZuusctand;

			if (null == SensorikScnapscus)
			{
				return AufgaabeParamZerleegungErgeebnis;
			}

			var WarpingLezteZaitMili = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.WarpingLezteZaitMili;
			var JumpingLezteZaitMili = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.JumpingLezteZaitMili;
			var DockedLezteZaitMili = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.DockedLezteZaitMili;
			var WarpingLezteAlterMili = NuzerZaitMili - WarpingLezteZaitMili;
			var JumpingLezteAlterMili = NuzerZaitMili - JumpingLezteZaitMili;
			var DockedLezteAlterMili = NuzerZaitMili - DockedLezteZaitMili;
			var SelbsctShipWarpScrambled = (null == FittingUndShipZuusctand) ? null : FittingUndShipZuusctand.SelbsctShipWarpScrambled;

			var CurrentLocationInfo = SensorikScnapscus.CurrentLocationInfo();
			var InfoPanelRoute = SensorikScnapscus.InfoPanelRoute;

			var InfoPanelRouteExpanded = (null == InfoPanelRoute) ? null : InfoPanelRoute.Expanded;

			var InfoPanelRouteCurrentInfo = (null == InfoPanelRoute) ? null : InfoPanelRoute.CurrentInfo;

			var AktioonUndockFraigaabeNictUrsace = (null == InRaumAktioonUndGefect) ? null : InRaumAktioonUndGefect.AktioonUndockFraigaabeNictUrsace;

			if (null != AktioonUndockFraigaabeNictUrsace)
			{
				AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAus();
			}
			else
			{
				if (null == CurrentLocationInfo)
				{
					AufgaabeParamZerleegungErgeebnis.FüügeAn(
						AufgaabeParamAndere.KonstruktInfoPanelExpand(InfoPanelTypSictEnum.SystemInfo),
						false);
				}

				if (true == InfoPanelRouteExpanded &&
					null != InfoPanelRouteCurrentInfo)
				{
					if ((true == SelbsctShipWarpScrambled) ||
						(DockedLezteAlterMili < 3555) ||
						(WarpingLezteAlterMili < 4444) ||
						(JumpingLezteAlterMili < 4444))
					{
						AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAus();
					}
					else
					{
						if (500 <= InfoPanelRouteCurrentInfo.SolarSystemSecurityLevelMili || LowSecFraigaabe)
						{
							var MarkerNääxte = InfoPanelRoute.MengeMarker.FirstOrDefaultNullable();

							if (null == MarkerNääxte)
							{
								//	!!!!	Meldung Feeler
							}
							else
							{
								AufgaabeParamZerleegungErgeebnis.FüügeAn(
									AufgaabeParamAndere.AufgaabeAktioonMenu(MarkerNääxte,
									new SictAnforderungMenuKaskaadeAstBedingung(
										new string[] { "Jump through stargate", "Dock", "Warp to within\\s*\\d" }, true)));
							}
						}
						else
						{
							AufgaabeParamZerleegungErgeebnis.FüügeAn(
								AufgaabeParamAndere.KonstruktNaacNuzerMeldungZuEveOnline(
								SictNaacNuzerMeldungZuEveOnline.ErrorGenerel(-1, SictNaacNuzerMeldungZuEveOnlineCauseTypeBinär.RoutePathSecurityLevelTooLow)),
								false);
						}
					}
				}
				else
				{
					AufgaabeParamZerleegungErgeebnis.FüügeAn(
						AufgaabeParamAndere.KonstruktInfoPanelExpand(InfoPanelTypSictEnum.Route),
						false);
				}
			}

			return AufgaabeParamZerleegungErgeebnis;
		}
	}
}
