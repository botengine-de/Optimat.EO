using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractFromOldAssembly.Bib3
{
	public struct Vektor2DInt
	{
		public Vektor2DInt(Int64 A, Int64 B)
		{
			this.A = A;
			this.B = B;
		}

		public Vektor2DInt(Vektor2DInt ZuKopiirende)
			:
			this(ZuKopiirende.A, ZuKopiirende.B)
		{
		}

		public	Int64	A;

		public Int64	B;

		public Bib3.Vektor2DDouble AlsBib3Vektor2DDouble()
		{
			return new Bib3.Vektor2DDouble(A, B);
		}

		public Bib3.Vektor2DDouble Bib3Vektor2DDouble()
		{
			return new Bib3.Vektor2DDouble(A, B);
		}

		static public Vektor2DInt operator -(Vektor2DInt Minuend, Vektor2DInt Subtrahend)
		{
			return new Vektor2DInt(Minuend.A - Subtrahend.A, Minuend.B - Subtrahend.B);
		}

		static public Vektor2DInt operator -(Vektor2DInt Subtrahend)
		{
			return new Vektor2DInt(0, 0) - Subtrahend;
		}

		static public Vektor2DInt operator +(Vektor2DInt Vektor0, Vektor2DInt Vektor1)
		{
			return new Vektor2DInt(Vektor0.A + Vektor1.A, Vektor0.B + Vektor1.B);
		}

		static public Vektor2DInt operator /(Vektor2DInt Dividend, Int64 Divisor)
		{
			return new Vektor2DInt((Dividend.A / Divisor), (Dividend.B / Divisor));
		}

		static public Vektor2DInt operator *(Vektor2DInt Vektor0, Int64 Faktor)
		{
			return new Vektor2DInt((Vektor0.A * Faktor), (Vektor0.B * Faktor));
		}

		static public Vektor2DInt operator *(Int64 Faktor, Vektor2DInt Vektor0)
		{
			return new Vektor2DInt((Vektor0.A * Faktor), (Vektor0.B * Faktor));
		}

		static public bool operator ==(Vektor2DInt Vektor0, Vektor2DInt Vektor1)
		{
			return Vektor0.A == Vektor1.A && Vektor0.B == Vektor1.B;
		}

		static public bool operator !=(Vektor2DInt Vektor0, Vektor2DInt Vektor1)
		{
			return !(Vektor0 == Vektor1);
		}

		public Int64 BetraagQuadriirt
		{
			get
			{
				return A * A + B * B;
			}
		}

		public Int64 Betraag
		{
			get
			{
				return (Int64)Math.Sqrt(BetraagQuadriirt);
			}
		}

		public double BetraagDouble
		{
			get
			{
				return Math.Sqrt(BetraagQuadriirt);
			}
		}

		public Vektor2DInt Normalisiirt()
		{
			var Betraag = this.Betraag;

			return new Vektor2DInt((this.A / Betraag), (this.B / Betraag));
		}

		static public double Skalarprodukt(Vektor2DInt vektor0, Vektor2DInt vektor1)
		{
			return vektor0.A * vektor1.A + vektor0.B * vektor1.B;
		}

		static System.Globalization.NumberFormatInfo KomponenteNumberFormat = KomponenteNumberFormatBerecne();

		static System.Globalization.NumberFormatInfo KomponenteNumberFormatBerecne()
		{
			var NumberFormat = (System.Globalization.NumberFormatInfo)System.Globalization.NumberFormatInfo.InvariantInfo.Clone();

			NumberFormat.NumberGroupSeparator = ".";
			NumberFormat.NumberGroupSizes = Enumerable.Range(0, 10).Select((t) => 3).ToArray();
			NumberFormat.NumberDecimalDigits = 0;

			return NumberFormat;
		}

		static public string KomponenteToString(Int64? Komponente)
		{
			if (!Komponente.HasValue)
			{
				return null;
			}

			return KomponenteToString(Komponente.Value);
		}

		static public string KomponenteToString(Int64 Komponente)
		{
			//	return Komponente.ToString("d", KomponenteNumberFormat);

			string Zwisceergeebnis = null;

			Int64 Rest = Math.Abs(Komponente);

			do
			{
				var NaacherRest = Rest / 1000;

				var Grupe = Rest % 1000;

				var GrupeString =
					0	< NaacherRest	?
					Grupe.ToString("D3")	:
					Grupe.ToString();

				Zwisceergeebnis =
					null == Zwisceergeebnis ?
					GrupeString :
					GrupeString + " " + Zwisceergeebnis;

				Rest = NaacherRest;

			} while (0 < Rest);

			if (Komponente < 0)
			{
				return "-" + Zwisceergeebnis;
			}

			return Zwisceergeebnis;
		}

		override public string ToString()
		{
			return "A = " + KomponenteToString(A) + ", B = " + KomponenteToString(B);
		}
	}
}