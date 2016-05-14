using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bib3;

namespace Optimat.EveOnline
{
	public class VonProcessMesung<T> : WertZuZaitraum<T>
	{
		readonly public int? ProcessId;

		public T Mesung
		{
			get
			{
				return this.Wert;
			}
		}

		public VonProcessMesung()
		{
		}

		public VonProcessMesung(
			T Mesung,
			Int64 BeginZait,
			Int64 EndeZait,
			int? ProcessId = null)
			:
			base(Mesung, BeginZait, EndeZait)
		{
			this.ProcessId = ProcessId;
		}

		public VonProcessMesung(
			T Mesung,
			Int64 Zait)
			:
			this(Mesung, Zait, Zait)
		{
		}
	}
}
