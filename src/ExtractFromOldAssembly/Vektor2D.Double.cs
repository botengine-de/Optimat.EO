using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExtractFromOldAssembly.Bib3
{
	public struct Vektor2DDouble
	{
		public double A, B;

		public Vektor2DDouble(double A, double B)
		{
			this.A = A;
			this.B = B;
		}

		static public Vektor2DDouble operator -(Vektor2DDouble Minuend, Vektor2DDouble Subtrahend)
		{
			return new Vektor2DDouble(Minuend.A - Subtrahend.A, Minuend.B - Subtrahend.B);
		}

		static public Vektor2DDouble operator -(Vektor2DDouble Subtrahend)
		{
			return new Vektor2DDouble(0, 0) - Subtrahend;
		}

		static public Vektor2DDouble operator +(Vektor2DDouble Vektor0, Vektor2DDouble Vektor1)
		{
			return new Vektor2DDouble(Vektor0.A + Vektor1.A, Vektor0.B + Vektor1.B);
		}

		static public Vektor2DDouble operator /(Vektor2DDouble Dividend, double Divisor)
		{
			return new Vektor2DDouble(Dividend.A / Divisor, Dividend.B / Divisor);
		}

		static public Vektor2DDouble operator *(Vektor2DDouble Vektor0, double Faktor)
		{
			return new Vektor2DDouble(Vektor0.A * Faktor, Vektor0.B * Faktor);
		}

		static public Vektor2DDouble operator *(double Faktor, Vektor2DDouble Vektor0)
		{
			return new Vektor2DDouble(Vektor0.A * Faktor, Vektor0.B * Faktor);
		}

		static public bool operator ==(Vektor2DDouble Vektor0, Vektor2DDouble Vektor1)
		{
			return Vektor0.A == Vektor1.A && Vektor0.B == Vektor1.B;
		}

		static public bool operator !=(Vektor2DDouble Vektor0, Vektor2DDouble Vektor1)
		{
			return !(Vektor0 == Vektor1);
		}

		public double BetraagQuadriirt
		{
			get
			{
				return A * A + B * B;
			}
		}

		public double Betraag
		{
			get
			{
				return Math.Sqrt(BetraagQuadriirt);
			}
		}

		public Vektor2DDouble Normalisiirt()
		{
			var Betrag = this.Betraag;

			return new Vektor2DDouble(this.A / Betrag, this.B / Betrag);
		}

		public void Normalisiire()
		{
			var Length = this.Betraag;

			this.A = this.A / Length;
			this.B = this.B / Length;
		}

		static public double Skalarprodukt(
			Vektor2DDouble vektor0,
			Vektor2DDouble vektor1)
		{
			return
				vektor0.A * vektor1.A + vektor0.B * vektor1.B;
		}

		static public double Kroizprodukt(
			Vektor2DDouble vektor0,
			Vektor2DDouble vektor1)
		{
			return
				vektor0.A * vektor1.B - vektor0.B * vektor1.A;
		}

		override public string ToString()
		{
			return "{ A:" + A.ToString() + ", B:" + B.ToString() + "}";
		}
	}
}
