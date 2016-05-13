using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimat.EveOnline.Berict.Auswert
{
	public	class SictZuOptimatScritInfo
	{
		readonly public Int64 ScritIndex;

		readonly public SictVerlaufBeginUndEndeRefMitZaitAinhaitMili<SictVonProcessLeese> VonProcessLeese;

		readonly	public	SictVonOptimatMeldungZuusctand VonOptimatZuusctand;

		readonly public SictNaacProcessWirkung[] VorsclaagListeNaacProcessWirkung;

		readonly public SictNaacProcessWirkung[] MengeNaacProcessWirkungErgeebnis;

		readonly public SictDataiIdentUndSuuceHinwais WindowClientRasterIdentUndSuuceHinwais;

		public Int64? VonProcessLeeseBeginZait
		{
			get
			{
				var VonProcessLeese = this.VonProcessLeese;

				if (null == VonProcessLeese)
				{
					return null;
				}

				return VonProcessLeese.BeginZait;
			}
		}

		public SictZuOptimatScritInfo(
			SictVerlaufBeginUndEndeRefMitZaitAinhaitMili<SictVonProcessLeese> VonProcessLeese,
			SictVonOptimatMeldungZuusctand VonOptimatZuusctand,
			SictNaacProcessWirkung[] VorsclaagListeNaacProcessWirkung,
			SictNaacProcessWirkung[] MengeNaacProcessWirkungErgeebnis,
			SictDataiIdentUndSuuceHinwais WindowClientRasterIdentUndSuuceHinwais)
		{
			this.VonProcessLeese = VonProcessLeese;
			this.VonOptimatZuusctand = VonOptimatZuusctand;
			this.VorsclaagListeNaacProcessWirkung = VorsclaagListeNaacProcessWirkung;
			this.MengeNaacProcessWirkungErgeebnis = MengeNaacProcessWirkungErgeebnis;
			this.WindowClientRasterIdentUndSuuceHinwais = WindowClientRasterIdentUndSuuceHinwais;
		}
	}
}
