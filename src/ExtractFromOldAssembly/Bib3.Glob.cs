using System;
using System.Globalization;

namespace ExtractFromOldAssembly.Bib3
{
	static public class Glob
	{
		static public System.Globalization.NumberFormatInfo NumberFormat
		{
			get
			{
				var NumberFormat = new System.Globalization.NumberFormatInfo();
				NumberFormat.NegativeSign = "-";
				NumberFormat.PositiveSign = "+";
				NumberFormat.NumberDecimalSeparator = ".";

				return NumberFormat;
			}
		}

		static public string TrimNullable(
			this string String)
		{
			if (null == String)
			{
				return null;
			}

			return String.Trim();
		}

		static public int? TryParseInt(
			this string ZuParsende,
			NumberStyles NumberStyle = NumberStyles.Integer)
		{
			return (int?)TryParseInt64(ZuParsende, NumberFormatInfo.CurrentInfo, NumberStyle);
		}

		static public int? TryParseInt(
			this string ZuParsende,
			NumberFormatInfo NumberFormat,
			NumberStyles NumberStyle = NumberStyles.Integer)
		{
			return (int?)TryParseInt64(ZuParsende, NumberFormat, NumberStyle);
		}

		static public Int64? TryParseInt64(
			this string ZuParsende,
			NumberStyles NumberStyle = NumberStyles.Integer)
		{
			return TryParseInt64(ZuParsende, NumberFormatInfo.CurrentInfo, NumberStyle);
		}

		static public Int64? TryParseInt64(
			this string ZuParsende,
			NumberFormatInfo NumberFormat,
			NumberStyles NumberStyle = NumberStyles.Integer)
		{
			Int64 Ergeebnis;

			if (!Int64.TryParse(ZuParsende, NumberStyle, NumberFormat, out Ergeebnis))
			{
				return null;
			}

			return Ergeebnis;
		}

		static public double? TryParseDoubleNulbar(
			this string ZuParsende,
			NumberStyles NumberStyle = NumberStyles.Float)
		{
			return TryParseDoubleNulbar(ZuParsende, Bib3.Glob.NumberFormat, NumberStyle);
		}

		static public double? TryParseDoubleNulbar(
			this string ZuParsende,
			NumberFormatInfo NumberFormat,
			NumberStyles NumberStyle = NumberStyles.Float)
		{
			double Ergeebnis;

			if (!double.TryParse(ZuParsende, NumberStyle, NumberFormat, out Ergeebnis))
			{
				return null;
			}

			return Ergeebnis;
		}

		static public string ToStringNullable<T>(
			this T Obj)
			where T : class
		{
			if (null == Obj)
			{
				return null;
			}

			return Obj.ToString();
		}

		static public string ScteleSicerEndung(
			this string String, string Endung)
		{
			if (String == null)
			{
				return Endung;
			}

			for (int EndungRestLänge = 0; EndungRestLänge < Endung.Length; EndungRestLänge++)
			{
				var InStringZaiceIndex = String.Length - Endung.Length + EndungRestLänge;

				if (InStringZaiceIndex < 0)
				{
					continue;
				}

				if (String.Substring(InStringZaiceIndex) == Endung.Substring(0, Endung.Length - EndungRestLänge))
				{
					return String + Endung.Substring(Endung.Length - EndungRestLänge);
				}
			}

			return String + Endung;
		}
	}
}
