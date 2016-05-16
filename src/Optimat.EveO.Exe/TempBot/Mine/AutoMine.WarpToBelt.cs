using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Optimat.ScpezEveOnln;
using Bib3;
using Newtonsoft.Json;
using ExtractFromOldAssembly.Bib3;

namespace Optimat.EveOnline.Anwendung
{
	public partial class SictAutoMine
	{
		public IEnumerable<SictAufgaabeParam> FürMineListeAufgaabeNääxteParamBerecneTailWarpToBelt(
			ISictAutomatZuusctand AutomaatZuusctand)
		{
			if (null == AutomaatZuusctand)
			{
				return null;
			}

			var ListeAufgaabeParam = new List<SictAufgaabeParam>();

			var NuzerZaitMili = AutomaatZuusctand.NuzerZaitMili;
			var FittingUndShipZuusctand = AutomaatZuusctand.FittingUndShipZuusctand;

			if (null == FittingUndShipZuusctand)
			{
				return	null;
			}

			var LocationNearestLezteVerlauf = AutomaatZuusctand.ListeLocationNearest().LastOrDefaultNullable();

			var LocationNearestLezteName = (null == LocationNearestLezteVerlauf) ? null : LocationNearestLezteVerlauf.Wert;

			var ShipZuusctand = FittingUndShipZuusctand.ShipZuusctand;

			var CharZuusctandDocked = (null == ShipZuusctand) ? null : ShipZuusctand.Docked;
			var CharZuusctandWarping = (null == ShipZuusctand) ? null : ShipZuusctand.Warping;

			if (CharZuusctandDocked	?? false)
			{
				ListeAufgaabeParam.Add(AufgaabeParamAndere.KonstruktUnDock());
			}
			else
			{
				if (Regex.Match(LocationNearestLezteName ?? "", "Asteroid", RegexOptions.IgnoreCase).Success)
				{
					if (null == OverviewTabBevorzuugtTitel)
					{
					}
					else
					{
						//	Laade Overview Default "Mining" naac Tab.

						ListeAufgaabeParam.Add(new AufgaabeParamOverviewPresetLaade(OverviewPresetTyp.Default, "Mining", OverviewTabBevorzuugtTitel));
					}
				}

				if (!(CharZuusctandWarping	?? false))
				{
					var MenuAsteroidBeltListePrioRegexPattern =
						(ListePrioAsteroidBeltBescriftung.SelectNullable((Kandidaat) => Regex.Escape(Kandidaat)) ?? new string[0])
						.Concat(new string[] { "Asteroid\\s*Belt" })
						.ToArrayNullable();

					ListeAufgaabeParam.Add(new AufgaabeParamInSolarSystemMenuAktioon(
						new SictAnforderungMenuKaskaadeAstBedingung[]{
									new	SictAnforderungMenuKaskaadeAstBedingung("Asteroid"),
									new	SictAnforderungMenuKaskaadeAstBedingung(MenuAsteroidBeltListePrioRegexPattern),
									new	SictAnforderungMenuKaskaadeAstBedingung("warp to within\\s*0", true)}));
				}
			}

			return ListeAufgaabeParam;
		}
	}
}
