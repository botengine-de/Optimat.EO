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

		readonly public VonProcessMesung<VonSensorikMesung> VonProcessMesung;

		readonly	public	SictVonOptimatMeldungZuusctand VonOptimatZuusctand;

		readonly public SictNaacProcessWirkung[] VorsclaagListeNaacProcessWirkung;

		readonly public SictNaacProcessWirkung[] MengeNaacProcessWirkungErgeebnis;

		readonly public SictDataiIdentUndSuuceHinwais WindowClientRasterIdentUndSuuceHinwais;

		public Int64? VonProcessLeeseBeginZait
		{
			get
			{
				var VonProcessMesung = this.VonProcessMesung;

				if (null == VonProcessMesung)
				{
					return null;
				}

				return VonProcessMesung.BeginZait;
			}
		}

		public SictZuOptimatScritInfo(
			VonProcessMesung<VonSensorikMesung> VonProcessMesung,
			SictVonOptimatMeldungZuusctand VonOptimatZuusctand,
			SictNaacProcessWirkung[] VorsclaagListeNaacProcessWirkung,
			SictNaacProcessWirkung[] MengeNaacProcessWirkungErgeebnis,
			SictDataiIdentUndSuuceHinwais WindowClientRasterIdentUndSuuceHinwais)
		{
			this.VonProcessMesung = VonProcessMesung;
			this.VonOptimatZuusctand = VonOptimatZuusctand;
			this.VorsclaagListeNaacProcessWirkung = VorsclaagListeNaacProcessWirkung;
			this.MengeNaacProcessWirkungErgeebnis = MengeNaacProcessWirkungErgeebnis;
			this.WindowClientRasterIdentUndSuuceHinwais = WindowClientRasterIdentUndSuuceHinwais;
		}
	}
}
