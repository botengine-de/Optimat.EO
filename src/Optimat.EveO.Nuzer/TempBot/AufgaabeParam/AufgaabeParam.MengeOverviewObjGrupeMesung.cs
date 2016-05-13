using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Optimat.ScpezEveOnln;
using Bib3;

namespace Optimat.EveOnline.Anwendung
{
	[JsonObject(MemberSerialization.OptIn)]
	public class AufgaabeParamMengeOverviewObjGrupeMesung : SictAufgaabeParam
	{
		[JsonProperty]
		readonly public SictOverviewObjektGrupeEnum[] MengeObjGrupe;

		public AufgaabeParamMengeOverviewObjGrupeMesung()
		{
		}

		public AufgaabeParamMengeOverviewObjGrupeMesung(
			SictOverviewObjektGrupeEnum[] MengeObjGrupe)
		{
			this.MengeObjGrupe = MengeObjGrupe;
		}

		override public SictAufgaabeParamZerleegungErgeebnis Zerleege(
			ISictAutomatZuusctand AutomaatZuusctand,
			SictAufgaabeKombiZuusctand KombiZuusctand)
		{
			return ZerleegeMengeOverviewObjGrupeMesung(AutomaatZuusctand, MengeObjGrupe);
		}

		override public IEnumerable<string> ZwekListeKomponenteAusParamBerecne()
		{
			var MengeObjGrupeAgrString =
				null == MengeObjGrupe ? null :
				string.Join(",", MengeObjGrupe.Select((ObjGrupe) => ObjGrupe.ToString()).ToArray());

			return new string[] { "in Overview search for Objects of Type[ " + (MengeObjGrupeAgrString ?? "") + "]" };
		}

		override public bool HinraicendGlaicwertigFürFortsazScpez(
			SictAufgaabeParam Andere)
		{
			var AndereScpez = Andere as AufgaabeParamMengeOverviewObjGrupeMesung;

			if (null == AndereScpez)
			{
				return false;
			}

			return
				AndereScpez.MengeObjGrupe.MengeEqual(this.MengeObjGrupe);
		}

		static public SictAufgaabeParamZerleegungErgeebnis ZerleegeMengeOverviewObjGrupeMesung(
			ISictAutomatZuusctand AutomaatZuusctand,
			SictOverviewObjektGrupeEnum[] MengeObjGrupe)
		{
			var AufgaabeParamZerleegungErgeebnis = new SictAufgaabeParamZerleegungErgeebnis(null, true);

			if (MengeObjGrupe.NullOderLeer())
			{
				return AufgaabeParamZerleegungErgeebnis;
			}

			AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAus();

			if (null == AutomaatZuusctand)
			{
				return AufgaabeParamZerleegungErgeebnis;
			}

			var OverviewUndTarget = AutomaatZuusctand.OverviewUndTarget;

			if (null == OverviewUndTarget)
			{
				return AufgaabeParamZerleegungErgeebnis;
			}

			var InRaumAktioonUndGefect = AutomaatZuusctand.InRaumAktioonUndGefect;

			if (null == InRaumAktioonUndGefect)
			{
				return AufgaabeParamZerleegungErgeebnis;
			}

			/*
			 * 2015.00.04
			 * 
			 * Aufgaabe werd vorersct so zerleegt das für ObjGrupe jewails ainzeln Overview durchsuuct werd.
			 * */

			var MengePresetDefault =
				SictOverviewUndTargetZuusctand.ListePresetDefaultPrioSctaatisc
				.Where((Kandidaat) =>
					0 <
					SictOverviewUndTargetZuusctand.ZuOverviewDefaultMengeObjektGrupeSictbar(Kandidaat)
					.IntersectNullable(MengeObjGrupe).CountNullable())
				.ToArray();

			var ListePresetDefault = SictOverviewUndTargetZuusctand.MengePresetDefaultOrdnetNaacPrioSctaatisc(MengePresetDefault);

			var PresetNääxte = ListePresetDefault.FirstOrDefaultNullable();

			var OverviewMengeZuTabNamePresetDefault = InRaumAktioonUndGefect.OverviewMengeZuTabNamePresetDefault;

			var TabFürPresetDefault =
				OverviewMengeZuTabNamePresetDefault
				.FirstOrDefaultNullable((Kandidaat) => Kandidaat.Value == PresetNääxte).Key;

			if (TabFürPresetDefault.NullOderLeer())
			{
				AufgaabeParamZerleegungErgeebnis.FüügeAn(
					AufgaabeParamAndere.KonstruktNaacNuzerMeldungZuEveOnline(
					SictNaacNuzerMeldungZuEveOnline.ErrorGenerel(-1, new SictNaacNuzerMeldungZuEveOnlineCause(CauseText: "Overview Management"))));

				return AufgaabeParamZerleegungErgeebnis;
			}

			//	Für nääxte Preset vorgesehene Tab öfne.
			AufgaabeParamZerleegungErgeebnis.FüügeAn(
				AufgaabeParamAndere.KonstruktOverviewTabAktiviire(TabFürPresetDefault));

			//	Preset laade.
			AufgaabeParamZerleegungErgeebnis.FüügeAn(
				new AufgaabeParamOverviewPresetLaade(OverviewPresetTyp.Default, PresetNääxte.ToString(), TabFürPresetDefault));

			//	Dirc Viewport Scrolle.
			AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.KonstruktInOverviewTabFolgeViewportDurcGrid());

			AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAin();

			return AufgaabeParamZerleegungErgeebnis;
		}
	}
}
