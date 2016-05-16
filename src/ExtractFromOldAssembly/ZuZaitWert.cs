using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractFromOldAssembly.Bib3
{
	/*
	 * 2015.02.25
	 * 
	public struct ZuZaitpunktWert<T>
	{
		readonly public Int64 Zait;

		readonly public T Wert;

		public ZuZaitpunktWert(Int64 Zait, T Wert)
		{
			this.Zait = Zait;
			this.Wert = Wert;
		}
	}
	 * */

	public class IntervalInt64
	{
		readonly public Int64 Begin;

		readonly public Int64 Ende;

		public IntervalInt64()
		{
		}

		public IntervalInt64(IntervalInt64 Base)
		{
			if (null != Base)
			{
				this.Begin = Base.Begin;
				this.Ende = Base.Ende;
			}
		}

		public IntervalInt64(Int64 Begin, Int64 Ende)
		{
			this.Begin = Begin;
			this.Ende = Ende;
		}
	}

	public class FieldGenMitIntervalInt64<T> : IntervalInt64
	{
		readonly public T Wert;

		public FieldGenMitIntervalInt64()
		{
		}

		public FieldGenMitIntervalInt64(
			T Wert,
			IntervalInt64 Base = null)
			:
			base(Base)
		{
			this.Wert = Wert;
		}

		public FieldGenMitIntervalInt64(
			T Wert,
			Int64 Begin,
			Int64 Ende)
			:
			base(Begin, Ende)
		{
			this.Wert = Wert;
		}

		public FieldGenMitIntervalInt64(
			T Wert,
			Int64 BeginUndEnde)
			:
			base(BeginUndEnde, BeginUndEnde)
		{
			this.Wert = Wert;
		}
	}

	public class WertZuZaitraum<T> : FieldGenMitIntervalInt64<T>
	{
		/*
		 * 2015.02.25
		 * !!!!	Vorersct verzict auf readonly da Bib3.RefNezDif diise in Struct noc nit kopiire kan.	!!!!
		 * */
		/*
		 * 2015.04.27
		 * Ersaz durc FieldGenMitIntervalInt64<T>
		 * 
		readonly public T Wert;
		 * */

		/*
		 * Ersaz durc Field in IntervalInt64
		 * 
		readonly public Int64 BeginZait;
		readonly public Int64 EndeZait;
		 * */

		public Int64 BeginZait
		{
			get
			{
				return Begin;
			}
		}

		public Int64 EndeZait
		{
			get
			{
				return Ende;
			}
		}

		public Int64 Dauer
		{
			get
			{
				return EndeZait - BeginZait;
			}
		}

		public WertZuZaitraum(
			T Wert,
			Int64 BeginZait,
			Int64 EndeZait)
			:
			base(Wert, BeginZait, EndeZait)
		{
			/*
			 * 2015.04.27
			 * 
			this.Wert = Wert;
			 * */
			/*
			 * Ersaz durc Field in IntervalInt64
			 * 
				this.BeginZait = BeginZait;
				this.EndeZait = EndeZait;
			 * */
		}

		public WertZuZaitraum(
			T Wert,
			Int64 Zait)
			:
			this(Wert, Zait, Zait)
		{
		}

		public WertZuZaitraum(
			T Wert)
			:
			this(Wert, 0)
		{
		}

		public WertZuZaitraum()
		{
		}
	}
	public struct WertZuZaitraumStruct<T>
	{
		/*
		 * 2015.02.25
		 * !!!!	Vorersct verzict auf readonly da Bib3.RefNezDif diise in Struct noc nit kopiire kan.	!!!!
		 * */
		public T Wert;

		public Int64 BeginZait;
		public Int64 EndeZait;

		public Int64 Dauer
		{
			get
			{
				return EndeZait - BeginZait;
			}
		}

		public WertZuZaitraumStruct(
			T Wert,
			Int64 BeginZait,
			Int64 EndeZait)
		{
			this.Wert = Wert;
			this.BeginZait = BeginZait;
			this.EndeZait = EndeZait;
		}

		public WertZuZaitraumStruct(
			T Wert,
			Int64 Zait)
			:
			this(Wert, Zait, Zait)
		{
		}

		public WertZuZaitraumStruct(
			T Wert)
			:
			this(Wert, 0)
		{
		}
	}
}
