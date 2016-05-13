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
	public class AufgaabeParamShipFluct : SictAufgaabeParam
	{
		static SictAnforderungMenuKaskaadeAstBedingung[] AusButtonListSurroundingsMenuPfaadFluct =
			new SictAnforderungMenuKaskaadeAstBedingung[]{
				new	SictAnforderungMenuKaskaadeAstBedingung(new	string[]{"Stations", "gates"}),
				new	SictAnforderungMenuKaskaadeAstBedingung("."),
				new	SictAnforderungMenuKaskaadeAstBedingung(new	string[]{	"Dock",	"Warp to Within\\s*\\d"},	true),};

		public AufgaabeParamShipFluct()
		{
		}

		override	public SictAufgaabeParamZerleegungErgeebnis Zerleege(
			ISictAutomatZuusctand AutomaatZuusctand,
			SictAufgaabeKombiZuusctand KombiZuusctand)
		{
			return ZerleegeFluct(AutomaatZuusctand);
		}

		override public IEnumerable<string> ZwekListeKomponenteAusParamBerecne()
		{
			return new string[] { "Emergency Warp Out" };
		}

		override public bool HinraicendGlaicwertigFürFortsazScpez(
			SictAufgaabeParam Andere)
		{
			var AndereScpez = Andere as AufgaabeParamShipFluct;

			if (null == AndereScpez)
			{
				return false;
			}

			return true;
		}

		static public SictAufgaabeParamZerleegungErgeebnis ZerleegeFluct(
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

			var SelbsctShipZuusctand = (null == Scnapscus) ? null : Scnapscus.SelfShipState;

			var SelbsctShipZuusctandDocked = (null == SelbsctShipZuusctand) ? null : SelbsctShipZuusctand.Docked;

			if (SelbsctShipZuusctandDocked ?? false)
			{
				return AufgaabeParamZerleegungErgeebnis;
			}

			AufgaabeParamZerleegungErgeebnis.FüügeAn(
				new AufgaabeParamInSolarSystemMenuAktioon(AusButtonListSurroundingsMenuPfaadFluct));

			return AufgaabeParamZerleegungErgeebnis;
		}
	}
}
