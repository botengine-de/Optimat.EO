using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Bib3;
using Optimat.EveOnline.Base;
using Optimat.ScpezEveOnln;

namespace Optimat.EveOnline.Anwendung
{
	[JsonObject(MemberSerialization.OptIn)]
	public class AufgaabeParamInSolarSystemMenuAktioon : SictAufgaabeParam
	{
		[JsonProperty]
		readonly public SictAnforderungMenuKaskaadeAstBedingung[]	ListeMenuKaskaadeAstBedingung;

		public AufgaabeParamInSolarSystemMenuAktioon()
		{
		}

		public AufgaabeParamInSolarSystemMenuAktioon(
			SictAnforderungMenuKaskaadeAstBedingung[] ListeMenuKaskaadeAstBedingung)
		{
			this.ListeMenuKaskaadeAstBedingung = ListeMenuKaskaadeAstBedingung;
		}

		override	public SictAufgaabeParamZerleegungErgeebnis Zerleege(
			ISictAutomatZuusctand AutomaatZuusctand,
			SictAufgaabeKombiZuusctand KombiZuusctand)
		{
			return ZerleegeInSolarSystemMenuAktioon(AutomaatZuusctand, ListeMenuKaskaadeAstBedingung);
		}

		override public IEnumerable<string> ZwekListeKomponenteAusParamBerecne()
		{
			return new string[] { "Solar System Menu" };
		}

		override public bool HinraicendGlaicwertigFürFortsazScpez(
			SictAufgaabeParam Andere)
		{
			var AndereScpez = Andere as AufgaabeParamInSolarSystemMenuAktioon;

			if (null == AndereScpez)
			{
				return false;
			}

			return
				AufgaabeParamAndere.HinraicendGlaicwertigFürFortsaz(
				ListeMenuKaskaadeAstBedingung, AndereScpez.ListeMenuKaskaadeAstBedingung);
		}

		static public SictAufgaabeParamZerleegungErgeebnis ZerleegeInSolarSystemMenuAktioon(
			ISictAutomatZuusctand AutomaatZuusctand,
			SictAnforderungMenuKaskaadeAstBedingung[] ListeMenuKaskaadeAstBedingung)
		{
			var AufgaabeParamZerleegungErgeebnis = new SictAufgaabeParamZerleegungErgeebnis(null, false);

			if (null == AutomaatZuusctand)
			{
				return AufgaabeParamZerleegungErgeebnis;
			}

			var Scnapscus = AutomaatZuusctand.ListeScnapscusLezteAuswertungErgeebnisNaacSimu;

			if (null == Scnapscus)
			{
				return AufgaabeParamZerleegungErgeebnis;
			}

			var GbsButtonListSurroundings = Scnapscus.InfoPanelLocationInfoButtonListSurroundings();

			if (null == GbsButtonListSurroundings)
			{
				AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.KonstruktNaacNuzerMeldungZuEveOnline(
					SictNaacNuzerMeldungZuEveOnline.ErrorGenerel(-1,
					new SictNaacNuzerMeldungZuEveOnlineCause(CauseText: "Solar System Menu not found"))));

				return AufgaabeParamZerleegungErgeebnis;
			}

			AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.AufgaabeAktioonMenu(
				GbsButtonListSurroundings,
				ListeMenuKaskaadeAstBedingung));

			AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAin();

			return AufgaabeParamZerleegungErgeebnis;
		}
	}
}
