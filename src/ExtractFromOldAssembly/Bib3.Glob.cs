using System;
using System.Globalization;

namespace ExtractFromOldAssembly.Bib3
{
	static public class Glob
	{
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
	}
}
