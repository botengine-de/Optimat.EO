using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimat.EveOnline.GBS
{
	static	public	class Glob
	{
		/*
		 * 2014.07.29
		 * 
		 * Verscoobe naac SictNaacNuzerMeldungZuEveOnlineCause
		 * 
		static public string MeldungSictStringBerecneAusNaacNuzerMeldung(
			SictNaacNuzerMeldungZuEveOnlineCause Ursace)
		{
			if (null == Ursace)
			{
				return null;
			}

			var WindowBlockingTitel = Ursace.WindowBlockingTitel;

			var WindowArrangementWindowStackTitel = Ursace.WindowArrangementWindowStackTitel;

			var FittingManagementMissingFitting = Ursace.FittingManagementMissingFitting;
			var PreferencesOverviewMissingTypeGroup = Ursace.PreferencesOverviewMissingTypeGroup;

			var OverviewMissingPreset = Ursace.OverviewMissingPreset;
			var OverviewMissingTab = Ursace.OverviewMissingTab;

			var CauseBinary = Ursace.CauseBinary;

			if (null != WindowBlockingTitel)
			{
				return "blocked by Window[\"" + WindowBlockingTitel + "\"]";
			}

			if (null != WindowArrangementWindowStackTitel)
			{
				return "WindowStack[\"" + WindowArrangementWindowStackTitel + "\"] detected, please decompose Window Stack";
			}

			if (null != FittingManagementMissingFitting)
			{
				return "missing Fitting Entry[\"" + FittingManagementMissingFitting + "\"]";
			}

			if (null != PreferencesOverviewMissingTypeGroup)
			{
				return "Preferences.Overview missing Overview Type Group[\"" + PreferencesOverviewMissingTypeGroup + "\"]";
			}

			if (null != OverviewMissingPreset)
			{
				return "Overview missing Preset[\"" + OverviewMissingPreset + "\"]";
			}

			if (null != OverviewMissingTab)
			{
				return "Overview missing Tab[\"" + OverviewMissingTab + "\"]";
			}

			if (CauseBinary.HasValue)
			{
				return SictNaacNuzerMeldungZuEveOnlineCause.CauseTypeStringBerecne(CauseBinary);
			}

			return null;
		}
		 * */

		static	public Bib3.FCL.GBS.SictSymboolAkzeptanzFeelerWarnung.SymboolTypSictEnum SymboolTypBerecneAusNaacNuzerMeldung(
			SictNaacNuzerMeldungZuEveOnline Meldung)
		{
			if (null == Meldung)
			{
				return Bib3.FCL.GBS.SictSymboolAkzeptanzFeelerWarnung.SymboolTypSictEnum.Kain;
			}

			var Severity = Meldung.Severity;

			if (!Severity.HasValue)
			{
				return Bib3.FCL.GBS.SictSymboolAkzeptanzFeelerWarnung.SymboolTypSictEnum.Kain;
			}

			switch (Severity.Value)
			{
				case SictNaacNuzerMeldungZuEveOnlineSeverity.None:
					break;
				case SictNaacNuzerMeldungZuEveOnlineSeverity.Info:
					break;
				case SictNaacNuzerMeldungZuEveOnlineSeverity.Warning:
					return Bib3.FCL.GBS.SictSymboolAkzeptanzFeelerWarnung.SymboolTypSictEnum.Warnung;
				case SictNaacNuzerMeldungZuEveOnlineSeverity.Error:
					return Bib3.FCL.GBS.SictSymboolAkzeptanzFeelerWarnung.SymboolTypSictEnum.Feeler;
			}

			return Bib3.FCL.GBS.SictSymboolAkzeptanzFeelerWarnung.SymboolTypSictEnum.Kain;
		}
	}
}
