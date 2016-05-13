using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bib3;

namespace Optimat.EveO.Nuzer
{
	interface IMengeSictIdIntScpaicer
	{
		Int64 Rictung0(Int64 t);
		Int64? Rictung1Nullbale(Int64 t);
	}

	class MengeSictIdIntScpaicer : IMengeSictIdIntScpaicer
	{
		readonly object Lock = new object();

		readonly Bib3.SictIdentInt64Fabrik Fabrik = new Bib3.SictIdentInt64Fabrik(1000);

		readonly Dictionary<Int64, Int64> DictRictung0 = new Dictionary<Int64, Int64>();
		readonly Dictionary<Int64, Int64> DictRictung1 = new Dictionary<Int64, Int64>();

		public long Rictung0(long t)
		{
			if(!DictRictung0.ContainsKey(t))
			{
				lock (Lock)
				{
					var	Abbild	= Fabrik.IdentBerecne();

					DictRictung0[t] = Abbild;

					DictRictung1[Abbild] = t;
				}
			}

			return DictRictung0[t];
		}

		public long? Rictung1Nullbale(long t)
		{
			return	DictRictung1.TADNulbar(t);
		}
	}
}
