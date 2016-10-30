using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Optimat.ScpezEveOnln;
using Bib3;
using ExtractFromOldAssembly.Bib3;

namespace Optimat.EveOnline.Anwendung
{
	[JsonObject(MemberSerialization.OptIn)]
	public class AufgaabeParamShipDock : SictAufgaabeParam
	{
		[JsonProperty]
		readonly public string StationName;

		public AufgaabeParamShipDock()
		{
		}

		public AufgaabeParamShipDock(
			string StationName)
		{
			this.StationName = StationName;
		}

		override	public SictAufgaabeParamZerleegungErgeebnis Zerleege(
			ISictAutomatZuusctand AutomaatZuusctand,
			SictAufgaabeKombiZuusctand KombiZuusctand)
		{
			return ZerleegeDock(AutomaatZuusctand, StationName);
		}

		override public IEnumerable<string> ZwekListeKomponenteAusParamBerecne()
		{
			return new string[] { "Dock at Station[" + (StationName ?? "") + "]" };
		}

		override public bool HinraicendGlaicwertigFürFortsazScpez(
			SictAufgaabeParam Andere)
		{
			var AndereScpez = Andere as AufgaabeParamShipDock;

			if (null == AndereScpez)
			{
				return false;
			}

			return string.Equals(StationName, AndereScpez.StationName, StringComparison.InvariantCultureIgnoreCase);
		}

		static public SictAufgaabeParamZerleegungErgeebnis ZerleegeDock(
			ISictAutomatZuusctand AutomaatZuusctand,
			string StationName)
		{
			StationName = StationName.TrimNullable();

			var AufgaabeParamZerleegungErgeebnis = new SictAufgaabeParamZerleegungErgeebnis(null, false);

			if (null == AutomaatZuusctand)
			{
				return AufgaabeParamZerleegungErgeebnis;
			}

			var WarpingLezteAlterMili = AutomaatZuusctand.ShipWarpingLezteAlterMili();
			var DockedLezteAlterMili = AutomaatZuusctand.ShipDockedLezteAlterMili();

			if (WarpingLezteAlterMili < 5555 || DockedLezteAlterMili < 3333)
			{
				AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAus();
				return AufgaabeParamZerleegungErgeebnis;
			}

			var StationRegexPatternFürMenuAusButtonListSurroundings =
				StationName.IsNullOrEmpty() ? null :
				SictAgentUndMissionZuusctand.StationNameSictFürButtonListSurroundingsRegexPattern(StationName);

			var MenuStationListePrioRegexPattern = new List<string>();

			if (null != StationRegexPatternFürMenuAusButtonListSurroundings)
			{
				MenuStationListePrioRegexPattern.Add(StationRegexPatternFürMenuAusButtonListSurroundings);
			}

			MenuStationListePrioRegexPattern.Add(".+");

			AufgaabeParamZerleegungErgeebnis.FüügeAn(
				new AufgaabeParamInSolarSystemMenuAktioon(
					new SictAnforderungMenuKaskaadeAstBedingung[]{
							new	SictAnforderungMenuKaskaadeAstBedingung("station"),
							new	SictAnforderungMenuKaskaadeAstBedingung(MenuStationListePrioRegexPattern.ToArray()),
							new	SictAnforderungMenuKaskaadeAstBedingung("dock",	true)}));

			AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAin();

			return AufgaabeParamZerleegungErgeebnis;
		}
	}
}
