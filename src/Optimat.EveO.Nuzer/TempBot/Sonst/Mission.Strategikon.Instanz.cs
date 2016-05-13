using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bib3;
using Newtonsoft.Json;

namespace Optimat.EveOnline.Anwendung
{
	[JsonObject(MemberSerialization.OptIn)]
	public partial class SictMissionStrategikonInstanz
	{
		[JsonProperty]
		readonly public Int64? BeginZait;

		[JsonProperty]
		public IDictionary<int, SictStrategikonInRaumAtomZwisceergeebnis> ZuStrategikonAtomZwisceergeebnis
		{
			private set;
			get;
		}

		[JsonProperty]
		public List<SictOverViewObjektZuusctand> MengeObjektCargoDurcsuuct
		{
			private set;
			get;
		}

		public void MengeObjektCargoDurcsuuctFüügeAin(SictOverViewObjektZuusctand Objekt)
		{
			if (null == Objekt)
			{
				return;
			}

			var MengeObjektCargoDurcsuuct = this.MengeObjektCargoDurcsuuct;

			if (null == MengeObjektCargoDurcsuuct)
			{
				MengeObjektCargoDurcsuuct = new List<SictOverViewObjektZuusctand>();
			}

			if (!MengeObjektCargoDurcsuuct.Contains(Objekt))
			{
				MengeObjektCargoDurcsuuct.Add(Objekt);
			}

			this.MengeObjektCargoDurcsuuct = MengeObjektCargoDurcsuuct;
		}

		public Int64? ErfolgFrühesteZaitBerecne() =>
			ZuStrategikonAtomZwisceergeebnis.TryGetValueOrDefault(0)?.EntscaidungErfolgFrühesteZait;

		public bool? ErfolgBerecne()
		{
			return ErfolgFrühesteZaitBerecne().HasValue;
		}

		public SictMissionStrategikonInstanz()
				:
				this(null)
		{
		}

		public SictMissionStrategikonInstanz(
			Int64? BeginZait)
		{
			this.BeginZait = BeginZait;
			this.ZuStrategikonAtomZwisceergeebnis = new Dictionary<int, SictStrategikonInRaumAtomZwisceergeebnis>();
		}
	}

}
