namespace Optimat.EveOnline.Base.TempUsedByBotAndUI
{
	static public class Extension
	{
		static public string MeldungSictStringBerecneAusNaacNuzerMeldung(
			this SictNaacNuzerMeldungZuEveOnline meldung)
		{
			if (null == meldung)
			{
				return null;
			}

			var GeneralCause = meldung.GeneralCause;

			var UndockPreventedCause = meldung.UndockPreventedCause;
			var DockForcedCause = meldung.DockForcedCause;

			if (null != UndockPreventedCause)
			{
				return "undocking prevented, cause=\"" + SictNaacNuzerMeldungZuEveOnlineCause.CauseSictStringBerecne(UndockPreventedCause) + "\"";
			}

			if (null != DockForcedCause)
			{
				return "dock forced, cause=\"" + SictNaacNuzerMeldungZuEveOnlineCause.CauseSictStringBerecne(DockForcedCause) + "\"";
			}

			if (null != GeneralCause)
			{
				return SictNaacNuzerMeldungZuEveOnlineCause.CauseSictStringBerecne(GeneralCause);
			}

			return null;
		}
	}
}
