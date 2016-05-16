using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
//using Optimat.EveOnline.AuswertGbs;
using Optimat.ScpezEveOnln;


namespace Optimat.EveOnline.Anwendung
{
	[JsonObject(MemberSerialization.OptIn)]
	public class AufgaabeParamScteleSicerOverviewSortNaacDistance : SictAufgaabeParam
	{
		public AufgaabeParamScteleSicerOverviewSortNaacDistance()
		{
		}

		override public SictAufgaabeParamZerleegungErgeebnis Zerleege(
			ISictAutomatZuusctand AutomaatZuusctand,
			SictAufgaabeKombiZuusctand KombiZuusctand)
		{
			return ZerleegeScteleSicerOverviewSortNaacDistance(AutomaatZuusctand);
		}

		override public IEnumerable<string> ZwekListeKomponenteAusParamBerecne()
		{
			return new string[] { "ensure Overview sorted by Distance" };
		}

		override public bool HinraicendGlaicwertigFürFortsazScpez(
			SictAufgaabeParam Andere)
		{
			var AndereScpez = Andere as AufgaabeParamScteleSicerOverviewSortNaacDistance;

			if (null == AndereScpez)
			{
				return false;
			}

			return true;
		}

		public SictAufgaabeParamZerleegungErgeebnis ZerleegeScteleSicerOverviewSortNaacDistance(
			ISictAutomatZuusctand AutomaatZuusctand)
		{
			var AufgaabeParamZerleegungErgeebnis = new SictAufgaabeParamZerleegungErgeebnis();

			AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAus();

			if (null == AutomaatZuusctand)
			{
				return AufgaabeParamZerleegungErgeebnis;
			}

			var OverviewUndTarget = AutomaatZuusctand.OverviewUndTarget;

			var WindowOverview = AutomaatZuusctand.WindowOverView();

			if (null == OverviewUndTarget)
			{
				return AufgaabeParamZerleegungErgeebnis;
			}

			if (null == WindowOverview)
			{
				return AufgaabeParamZerleegungErgeebnis;
			}

			if (!(OverviewUndTarget.SortedNaacDistanceNict ?? false))
			{
				AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAin();

				return AufgaabeParamZerleegungErgeebnis;
			}

			var WindowOverviewScnapscus = WindowOverview.AingangScnapscusTailObjektIdentLezteBerecne() as VonSensor.WindowOverView;

			if (null == WindowOverviewScnapscus)
			{
				return AufgaabeParamZerleegungErgeebnis;
			}

			var ScnapscusWindowOverviewAusTabListeZaileOrdnetNaacLaage = (null == WindowOverviewScnapscus) ? null : WindowOverviewScnapscus.AusTabListeZaileOrdnetNaacLaage;
			var ScnapscusWindowOverviewColumnHeaderDistance = (null == WindowOverviewScnapscus) ? null : WindowOverviewScnapscus.ColumnHeaderDistance;

			AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAin();

			if (!(4 < ExtractFromOldAssembly.Bib3.Extension.CountNullable(ScnapscusWindowOverviewAusTabListeZaileOrdnetNaacLaage)))
			{
				return AufgaabeParamZerleegungErgeebnis;
			}

			if (WindowOverviewScnapscus.ZaileSindSortiirtNaacDistanceAufsctaigend ?? false)
			{
				return AufgaabeParamZerleegungErgeebnis;
			}

			if (null == ScnapscusWindowOverviewColumnHeaderDistance)
			{
				AufgaabeParamZerleegungErgeebnis.FüügeAn(
					AufgaabeParamAndere.KonstruktNaacNuzerMeldungZuEveOnline(SictNaacNuzerMeldungZuEveOnline.ErrorGenerel(-1,

					//	!!!!	Cause Anzupase:	Meldung das ColumnHeaderDistance feelt!

					new SictNaacNuzerMeldungZuEveOnlineCause(SictNaacNuzerMeldungZuEveOnlineCauseTypeBinär.OverviewSortingNotOptimized))),
					false);
			}
			else
			{
				AufgaabeParamZerleegungErgeebnis.FüügeAn(
					AufgaabeParamAndere.KonstruktColumnHeaderSort(ScnapscusWindowOverviewColumnHeaderDistance));
			}

			return AufgaabeParamZerleegungErgeebnis;
		}
	}
}
