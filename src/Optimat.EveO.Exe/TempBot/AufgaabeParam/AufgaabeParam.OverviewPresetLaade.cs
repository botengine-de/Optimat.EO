using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Optimat.ScpezEveOnln;


namespace Optimat.EveOnline.Anwendung
{
	[JsonObject(MemberSerialization.OptIn)]
	public class AufgaabeParamOverviewPresetLaade : SictAufgaabeParam
	{
		[JsonProperty]
		readonly public OverviewPresetTyp PresetTyp;

		[JsonProperty]
		readonly public string PresetIdent;

		[JsonProperty]
		readonly public string ZiilTabIdent;

		public AufgaabeParamOverviewPresetLaade()
		{
		}

		public AufgaabeParamOverviewPresetLaade(
			OverviewPresetTyp PresetTyp,
			string PresetIdent,
			string ZiilTabIdent)
		{
			this.PresetTyp = PresetTyp;
			this.PresetIdent = PresetIdent;
			this.ZiilTabIdent = ZiilTabIdent;
		}

		override public SictAufgaabeParamZerleegungErgeebnis Zerleege(
			ISictAutomatZuusctand AutomaatZuusctand,
			SictAufgaabeKombiZuusctand KombiZuusctand)
		{
			return ZerleegeOverviewPresetLaade(AutomaatZuusctand, PresetTyp, PresetIdent, ZiilTabIdent);
		}

		override public IEnumerable<string> ZwekListeKomponenteAusParamBerecne()
		{
			//	load Overview Preset \"" + (OverviewPresetZuLaadeIdent ?? "") + "\" to Tab \"" + (ScnapscusWindowOverviewTabSelectedIdent ?? "") + "\""

			return new string[] { "in Overview load " + PresetTyp.ToString() + " \"" + (PresetIdent ?? "") + "\" to Tab \"" + (ZiilTabIdent ?? "") + "\"" };
		}

		override public bool HinraicendGlaicwertigFürFortsazScpez(
			SictAufgaabeParam Andere)
		{
			var AndereScpez = Andere as AufgaabeParamOverviewPresetLaade;

			if (null == AndereScpez)
			{
				return false;
			}

			return
				PresetTyp == AndereScpez.PresetTyp &&
				string.Equals(PresetIdent, AndereScpez.PresetIdent, StringComparison.InvariantCultureIgnoreCase) &&
				string.Equals(ZiilTabIdent, AndereScpez.ZiilTabIdent, StringComparison.InvariantCultureIgnoreCase);
		}

		static public SictAufgaabeParamZerleegungErgeebnis ZerleegeOverviewPresetLaade(
			ISictAutomatZuusctand AutomaatZuusctand,
			OverviewPresetTyp PresetTyp,
			string PresetIdent,
			string ZiilTabIdent)
		{
			var AufgaabeParamZerleegungErgeebnis = new SictAufgaabeParamZerleegungErgeebnis(null, false);

			if (null == PresetIdent)
			{
				return AufgaabeParamZerleegungErgeebnis;
			}

			if (null == ZiilTabIdent)
			{
				return AufgaabeParamZerleegungErgeebnis;
			}

			if (null == AutomaatZuusctand)
			{
				return AufgaabeParamZerleegungErgeebnis;
			}

			var ScnapscusWindowOverview = AutomaatZuusctand.WindowOverviewScnapscusLezte();

			var WindowOverviewScnapscusLezteListeTab = AutomaatZuusctand.WindowOverviewScnapscusLezteListeTabNuzbar();

			if (null == ScnapscusWindowOverview)
			{
				return AufgaabeParamZerleegungErgeebnis;
			}

			if (null == WindowOverviewScnapscusLezteListeTab)
			{
				return AufgaabeParamZerleegungErgeebnis;
			}

			var ZiilTab =
				WindowOverviewScnapscusLezteListeTab
				.FirstOrDefault((Kandidaat) => string.Equals(Kandidaat.LabelBescriftung, ZiilTabIdent, StringComparison.InvariantCultureIgnoreCase));

			if (null == ZiilTab)
			{
				AufgaabeParamZerleegungErgeebnis.FüügeAn(
					AufgaabeParamAndere.KonstruktNaacNuzerMeldungZuEveOnline(
					SictNaacNuzerMeldungZuEveOnline.ErrorGenerel(-1, new SictNaacNuzerMeldungZuEveOnlineCause(
						OverviewMissingTab: ZiilTabIdent))));

				return AufgaabeParamZerleegungErgeebnis;
			}

			if (!(ZiilTab == AutomaatZuusctand.WindowOverviewScnapscusLezteTabAktiiv()))
			{
				AufgaabeParamZerleegungErgeebnis.FüügeAn(
					AufgaabeParamAndere.KonstruktMausPfaad(
						SictAufgaabeParamMausPfaad.KonstruktMausKlikLinx(ZiilTab)));

				return AufgaabeParamZerleegungErgeebnis;
			}

			AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAin();

			if (string.Equals(ScnapscusWindowOverview.OverviewPresetIdent, PresetIdent,
				StringComparison.InvariantCultureIgnoreCase))
			{
				return AufgaabeParamZerleegungErgeebnis;
			}

			var MenuEntryPattern = "load\\s*" + PresetTyp.ToString();

			AufgaabeParamZerleegungErgeebnis.FüügeAn(
			AufgaabeParamAndere.AufgaabeAktioonMenu(
				ZiilTab,
				new SictAnforderungMenuKaskaadeAstBedingung[]{
					new SictAnforderungMenuKaskaadeAstBedingung(MenuEntryPattern),
					new SictAnforderungMenuKaskaadeAstBedingung("^" + Regex.Escape(PresetIdent) + "$", true)}));

			return AufgaabeParamZerleegungErgeebnis;
		}
	}
}
