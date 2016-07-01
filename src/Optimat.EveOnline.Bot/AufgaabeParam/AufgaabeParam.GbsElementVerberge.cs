using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Optimat.ScpezEveOnln;
using Bib3;
using System.Text.RegularExpressions;
using Optimat.EveOnline.VonSensor;

namespace Optimat.EveOnline.Anwendung
{
	[JsonObject(MemberSerialization.OptIn)]
	public class AufgaabeParamGbsElementVerberge : SictAufgaabeParam
	{
		[JsonProperty]
		readonly public GbsElement GbsElement;

		[JsonProperty]
		readonly public SictAutomatZuusctand.SictGbsAstOklusioonKombi BerictOklusion;

		public AufgaabeParamGbsElementVerberge()
		{
		}

		public AufgaabeParamGbsElementVerberge(
			GbsElement GbsElement,
			SictAutomatZuusctand.SictGbsAstOklusioonKombi BerictOklusion = null)
		{
			this.GbsElement = GbsElement;
			this.BerictOklusion = BerictOklusion;
		}

		override public SictAufgaabeParamZerleegungErgeebnis Zerleege(
			ISictAutomatZuusctand AutomaatZuusctand,
			SictAufgaabeKombiZuusctand KombiZuusctand)
		{
			return ZerleegeGbsElementVerberge(AutomaatZuusctand, GbsElement);
		}

		override public IEnumerable<string> ZwekListeKomponenteAusParamBerecne()
		{
			var GbsAstVerbergeInGbsFläce = GbsElement.InGbsFläce;

			return new string[] { "hide UIElement at " + ObjektSictZwekKomponente(GbsAstVerbergeInGbsFläce) };
		}

		override public bool HinraicendGlaicwertigFürFortsazScpez(
			SictAufgaabeParam Andere)
		{
			var AndereScpez = Andere as AufgaabeParamGbsElementVerberge;

			if (null == AndereScpez)
			{
				return false;
			}

			return GbsElement.Identisc(GbsElement, AndereScpez.GbsElement);
		}

		static public SictAufgaabeParamZerleegungErgeebnis ZerleegeGbsElementVerberge(
			ISictAutomatZuusctand AutomaatZuusctand,
			GbsElement GbsElement)
		{
			var AufgaabeParamZerleegungErgeebnis = new SictAufgaabeParamZerleegungErgeebnis(null, true);

			if (null == GbsElement)
			{
				return AufgaabeParamZerleegungErgeebnis;
			}

			var GbsAstVerbergeBezaicnerNulbar = (Int64?)GbsElement.Ident;

			if (GbsAstVerbergeBezaicnerNulbar.HasValue)
			{
				var KandidaatOklusioonInfo =
					AutomaatZuusctand.ZuGbsAstHerkunftAdreseKandidaatOklusioonBerecne(GbsAstVerbergeBezaicnerNulbar.Value);

				if (null == KandidaatOklusioonInfo)
				{
					AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAus();
				}
				else
				{
					var KandidaatOklusioonInfoMenu = KandidaatOklusioonInfo.Menu;
					var KandidaatOklusioonInfoUtilmenu = KandidaatOklusioonInfo.Utilmenu;
					var KandidaatOklusioonInfoPanelGroup = KandidaatOklusioonInfo.PanelGroup;
					var KandidaatOklusioonInfoWindow = KandidaatOklusioonInfo.Window;

					var ListeAufgaabeVerberge = new List<SictAufgaabeParam>();

					if (null != KandidaatOklusioonInfoMenu ||
						null != KandidaatOklusioonInfoUtilmenu)
					{
						ListeAufgaabeVerberge.Add(AufgaabeParamAndere.KonstruktMenuEntferne());
					}

					if (null != KandidaatOklusioonInfoPanelGroup)
					{
						//	PanelGroup sctamt vermuutlic aus Neocom
						ListeAufgaabeVerberge.Add(AufgaabeParamAndere.KonstruktNeocomMenuEntferne());
					}

					if (null != KandidaatOklusioonInfoWindow)
					{
						ListeAufgaabeVerberge.Add(AufgaabeParamAndere.KonstruktWindowMinimize(KandidaatOklusioonInfoWindow));
					}

					if (ListeAufgaabeVerberge.Count < 1)
					{
						AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAus();
					}

					AufgaabeParamZerleegungErgeebnis = AufgaabeParamZerleegungErgeebnis.Kombiniire(ListeAufgaabeVerberge);
				}
			}

			return AufgaabeParamZerleegungErgeebnis;
		}
	}
}
