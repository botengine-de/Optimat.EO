using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractFromOldAssembly.Bib3
{
	public struct OrtogoonInt
	{
		public Int64 Min0;
		public Int64 Min1;
		public Int64 Max0;
		public Int64 Max1;

		public Vektor2DInt PunktMin
		{
			get
			{
				return new Vektor2DInt(Min0, Min1);
			}
		}

		public Vektor2DInt PunktMax
		{
			get
			{
				return new Vektor2DInt(Max0, Max1);
			}
		}

		public Int64 LängeRictung0
		{
			get
			{
				return Max0 - Min0;
			}
		}

		public Int64 LängeRictung1
		{
			get
			{
				return Max1 - Min1;
			}
		}

		public Vektor2DInt ZentrumLaage
		{
			get
			{
				return new Vektor2DInt((Min0 + Max0) / 2, (Min1 + Max1) / 2);
			}
		}

		public Vektor2DInt Grööse
		{
			get
			{
				return new Vektor2DInt(LängeRictung0, LängeRictung1);
			}
		}

		public IEnumerable<Vektor2DInt> ListeEkeLaage()
		{
			for (int EkeIndex = 0; EkeIndex < 4; EkeIndex++)
			{
				var RictungAGrenzeIndex = 1 == (((EkeIndex + 1) / 2) % 2);
				var RictungBGrenzeIndex = 1 == (((EkeIndex + 0) / 2) % 2);

				yield return new Vektor2DInt(RictungAGrenzeIndex ? Max0 : Min0, RictungBGrenzeIndex ? Max1 : Min1);
			}
		}

		static public OrtogoonInt Leer
		{
			get
			{
				return new OrtogoonInt(0, 0, 0, 0);
			}
		}

		static public OrtogoonInt LeerMin
		{
			get
			{
				return new OrtogoonInt(Int64.MinValue, Int64.MinValue, Int64.MinValue, Int64.MinValue);
			}
		}

		public OrtogoonInt(
			Int64 Min0,
			Int64 Min1,
			Int64 Max0,
			Int64 Max1)
		{
			this.Min0 = Min0;
			this.Min1 = Min1;
			this.Max0 = Max0;
			this.Max1 = Max1;
		}

		public OrtogoonInt(
			OrtogoonInt ZuKopiirende)
			:
			this(
			ZuKopiirende.Min0,
			ZuKopiirende.Min1,
			ZuKopiirende.Max0,
			ZuKopiirende.Max1)
		{
		}

		static public OrtogoonInt AusPunktMinUndPunktMax(
			Vektor2DInt PunktMinInkl,
			Vektor2DInt PunktMaxExkl)
		{
			return new OrtogoonInt(PunktMinInkl.A, PunktMinInkl.B, PunktMaxExkl.A, PunktMaxExkl.B);
		}

		static public OrtogoonInt AusPunktZentrumUndGrööse(
			Vektor2DInt ZentrumLaage,
			Vektor2DInt Grööse)
		{
			return OrtogoonInt.AusPunktMinUndPunktMax(
				(ZentrumLaage - Grööse / 2),
				(ZentrumLaage + ((Grööse + new Vektor2DInt(1, 1)) / 2)));
		}

		public OrtogoonInt Versezt(Vektor2DInt Vektor)
		{
			return new OrtogoonInt(
				Min0 + Vektor.A,
				Min1 + Vektor.B,
				Max0 + Vektor.A,
				Max1 + Vektor.B);
		}

		public OrtogoonInt VerseztAufZentrumLaage(Vektor2DInt ZentrumLaage)
		{
			var Versaz = ZentrumLaage - this.ZentrumLaage;

			return this.Versezt(Versaz);
		}

		public OrtogoonInt GrööseGeseztAngelpunktZentrum(Vektor2DInt Grööse)
		{
			return AusPunktZentrumUndGrööse(ZentrumLaage, Grööse);
		}

		static public OrtogoonInt Scnitfläce(OrtogoonInt O0, OrtogoonInt O1)
		{
			var ScnitfläceMin0 = Math.Min(O0.Max0, Math.Max(O0.Min0, O1.Min0));
			var ScnitfläceMin1 = Math.Min(O0.Max1, Math.Max(O0.Min1, O1.Min1));

			return new OrtogoonInt(
				ScnitfläceMin0,
				ScnitfläceMin1,
				Math.Max(ScnitfläceMin0, Math.Min(O0.Max0, O1.Max0)),
				Math.Max(ScnitfläceMin1, Math.Min(O0.Max1, O1.Max1)));
		}

		static public OrtogoonInt operator -(OrtogoonInt Minuend, Vektor2DInt Subtrahend)
		{
			return Minuend.Versezt(-Subtrahend);
		}

		static public OrtogoonInt operator +(OrtogoonInt Sumand0, Vektor2DInt Sumand1)
		{
			return Sumand0.Versezt(Sumand1);
		}

		static public OrtogoonInt operator *(OrtogoonInt Faktor0, Int64 Faktor1)
		{
			return new OrtogoonInt(Faktor0.Min0 * Faktor1, Faktor0.Min1 * Faktor1, Faktor0.Max0 * Faktor1, Faktor0.Max1 * Faktor1);
		}

		static public bool operator ==(OrtogoonInt O0, OrtogoonInt O1)
		{
			return
				O0.Min0 == O1.Min0 &&
				O0.Min1 == O1.Min1 &&
				O0.Max0 == O1.Max0 &&
				O0.Max1 == O1.Max1;
		}

		override public bool Equals(object Obj)
		{
			var AlsOrtogoon = Extension.CastToNullable<OrtogoonInt>(Obj);

			if (!AlsOrtogoon.HasValue)
			{
				return false;
			}

			return AlsOrtogoon.Value == this;
		}

		override public int GetHashCode()
		{
			return (Min0 + Min1 + Max0 + Max1).GetHashCode();
		}

		static public bool operator !=(OrtogoonInt Vektor0, OrtogoonInt Vektor1)
		{
			return !(Vektor0 == Vektor1);
		}

		public Int64 Betraag
		{
			get
			{
				return LängeRictung0 * LängeRictung1;
			}
		}

		public bool IsLeer
		{
			get
			{
				return 0 == Betraag;
			}
		}

		public bool EnthältPunktFürMinInklusiivUndMaxExklusiiv(Vektor2DInt Punkt)
		{
			return
				Min0 <= Punkt.A &&
				Min1 <= Punkt.B &&
				Punkt.A < Max0 &&
				Punkt.B < Max1;
		}

		public bool EnthältPunktFürMinInklusiivUndMaxInklusiiv(Vektor2DInt Punkt)
		{
			return
				Min0 <= Punkt.A &&
				Min1 <= Punkt.B &&
				Punkt.A <= Max0 &&
				Punkt.B <= Max1;
		}

		static public string KomponenteToString(Int64? Komponente)
		{
			return Vektor2DInt.KomponenteToString(Komponente);
		}

		override public string ToString()
		{
			return
				"Min0 = " + KomponenteToString(Min0) +
				", Min1 = " + KomponenteToString(Min1) +
				", Max0 = " + KomponenteToString(Max0) +
				", Max1 = " + KomponenteToString(Max1);
		}
	}
}