using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Optimat.ScpezEveOnln;
using Bib3;
using ExtractFromOldAssembly.Bib3;

namespace Optimat.EveOnline.Anwendung
{
	[JsonObject(MemberSerialization.OptIn)]
	public class AufgaabeParamOverviewPresetLaadeNaacVorgeseheneTab : SictAufgaabeParam
	{
		[JsonProperty]
		readonly public OverviewPresetDefaultTyp PresetDefault;

		public AufgaabeParamOverviewPresetLaadeNaacVorgeseheneTab()
		{
		}

		public AufgaabeParamOverviewPresetLaadeNaacVorgeseheneTab(
			OverviewPresetDefaultTyp	PresetDefault)
		{
			this.PresetDefault = PresetDefault;
		}

		override	public SictAufgaabeParamZerleegungErgeebnis Zerleege(
			ISictAutomatZuusctand AutomaatZuusctand,
			SictAufgaabeKombiZuusctand KombiZuusctand)
		{
			return ZerleegeOverviewPresetLaadeNaacVorgeseheneTab(AutomaatZuusctand, PresetDefault);
		}

		override public IEnumerable<string> ZwekListeKomponenteAusParamBerecne()
		{
			return new string[] { "in Overview switch to Preset[" + PresetDefault.ToString() + "]"};
		}

		override public bool HinraicendGlaicwertigFürFortsazScpez(
			SictAufgaabeParam Andere)
		{
			var AndereScpez = Andere as AufgaabeParamOverviewPresetLaadeNaacVorgeseheneTab;

			if (null == AndereScpez)
			{
				return false;
			}

			return
				PresetDefault == AndereScpez.PresetDefault;
		}

		static public SictAufgaabeParamZerleegungErgeebnis ZerleegeOverviewPresetLaadeNaacVorgeseheneTab(
			ISictAutomatZuusctand AutomaatZuusctand,
			OverviewPresetDefaultTyp PresetDefault)
		{
			var AufgaabeParamZerleegungErgeebnis = new SictAufgaabeParamZerleegungErgeebnis(null,	false);

			if (null == AutomaatZuusctand)
			{
				return AufgaabeParamZerleegungErgeebnis;
			}

			var InRaumAktioonUndGefect = AutomaatZuusctand.InRaumAktioonUndGefect;

			if (null == InRaumAktioonUndGefect)
			{
				return	AufgaabeParamZerleegungErgeebnis;
			}

			var ZiilTab =
				InRaumAktioonUndGefect.OverviewMengeZuTabNamePresetDefault
				.FirstOrDefaultNullable((Kandidaat) => Kandidaat.Value == PresetDefault).Key;

			if (ZiilTab.NullOderLeer())
			{
				AufgaabeParamZerleegungErgeebnis.FüügeAn(
					AufgaabeParamAndere.KonstruktNaacNuzerMeldungZuEveOnline(
					SictNaacNuzerMeldungZuEveOnline.ErrorGenerel(-1, new SictNaacNuzerMeldungZuEveOnlineCause(
						CauseText: "Overview Management"))));

				return AufgaabeParamZerleegungErgeebnis;
			}

			AufgaabeParamZerleegungErgeebnis.FüügeAn(
				AufgaabeParamAndere.KonstruktOverviewTabAktiviire(ZiilTab));

			AufgaabeParamZerleegungErgeebnis.FüügeAn(
				new	AufgaabeParamOverviewPresetLaade(OverviewPresetTyp.Default, PresetDefault.ToString(), ZiilTab));

			AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAin();

			return AufgaabeParamZerleegungErgeebnis;
		}
	}
}
