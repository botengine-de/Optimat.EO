using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Bib3;
using ExtractFromOldAssembly.Bib3;
using Optimat.EveOnline.VonSensor;

namespace Optimat.EveOnline.TempAuswertGbs
{
	static   public class Extension
    {
		public const string SctandardZaalRegexPatternCharacterClass = "[\\d\\.\\,]";

		public const string SctandardZaalRegexPattern = SctandardZaalRegexPatternCharacterClass + "+";

		public const string SctandardZaalRegexPatternGroup = "(" + SctandardZaalRegexPattern + ")";

		/// <summary>
		/// 2013.08.00 Bsp:
		/// "Gone Berserk Objectives"
		/// 
		/// 2013.08.07 naac Interpretation Html Bsp:
		/// "Eliminate the Pirate Campers"
		/// 
		/// 2014.01.26	Bsp:
		/// "After The Seven (1 of 5) \"Replacement\""
		/// </summary>
		static readonly public string MissionTitelRegexMengeCharClass = "\\w\\d\\s" + Regex.Escape("(/)'\"#?.-");

		static readonly SictScatenscpaicerDict<OreTypSictEnum, Regex> ScatenscpaicerZuOreTypeRegex =
			new SictScatenscpaicerDict<OreTypSictEnum, Regex>();

		static readonly SictScatenscpaicerDict<KeyValuePair<string, string>, bool> ScatescpaicerTargetBescriftungOreTypPasendZuSurveyScanEntryBescriftung =
			new SictScatenscpaicerDict<KeyValuePair<string, string>, bool>();

		public const string DistanceKomponenteAinhaitRegexPattern = "\\w+";

		/*
		 * 2015.02.27
		 * 
		 * Funktioniirt nit wen whitespace als thousand separator verwendet werd (russisch, bulgarisch)
		 * 
		public const string DistanceKomponenteBetraagRegexPattern = SctandardZaalRegexPattern;
		public const string DistanceRegexPattern = DistanceKomponenteBetraagRegexPattern + "\\s*" + DistanceKomponenteAinhaitRegexPattern;

		/// <summary>
		/// 2013.07.14	Bsp:
		/// "1.584.570 km"
		/// "8,3 AU"
		/// </summary>
		public const string DistanceAufgetailungNaacKomponenteRegexPattern =
			"(" + DistanceKomponenteBetraagRegexPattern + ")\\s*(" + DistanceKomponenteAinhaitRegexPattern + ")";
		 * */

		public const string DistanceRegexPatternGroupBetraagIdent = "betraag";
		public const string DistanceRegexPatternGroupAinhaitIdent = "ainhait";

		readonly static public string DistanceRegexPattern =
			@"(?<" + DistanceRegexPatternGroupBetraagIdent + ">" +
			SctandardNumberFormatRegexPatternBerecne(true, true) +
			@")\s*" +
			@"(?<" + DistanceRegexPatternGroupAinhaitIdent + ">" +
			DistanceKomponenteAinhaitRegexPattern +
			")";

		const string InNumberRegexPatternSignGrupeNaame = "Sign";
		const string InNumberRegexPatternVorDezimaaltrenzaiceGrupeNaame = "VorDezimaaltrenzaice";
		const string InNumberRegexPatternNaacDezimaaltrenzaiceGrupeNaame = "NaacDezimaaltrenzaice";
		const string InNumberRegexPatternDezimaaltrenzaiceGrupeNaame = "Dezimaaltrenzaice";
		const string InNumberRegexPatternZiferngrupiirungGrupeNaame = "Ziferngrupiirung";

		static readonly string SctandardNumberFormatRegexPattern = SctandardNumberFormatRegexPatternBerecne();

		/// <summary>
		/// 2015.00.18
		/// d Scwiizr nuze Apostroph als Tausendertrenzaice: "0.0/8'500.0 m³"
		/// 
		/// </summary>
		/// <returns></returns>
		static public string SctandardNumberFormatRegexPatternBerecne(
			bool AkzeptiireVorherig = false,
			bool AkzeptiireFolgend = false,
			string GrupeIdentSufix = null)
		{
			return
				NumberFormatRegexPatternBerecne(
					new string[] { "+", "-" },
					new string[] { ".", "," },
					new string[] { ".", ",", "'", " ", ""
						//	،: "Pashto" (ps)
						,"،"
					},
					AkzeptiireVorherig,
					AkzeptiireFolgend,
					GrupeIdentSufix);
		}

		static readonly public Regex SctandardNumberFormatRegex =
			new Regex(SctandardNumberFormatRegexPatternBerecne(), RegexOptions.IgnoreCase | RegexOptions.Compiled);

		static public string RegexPatternAlternatiiveBerecne(
			string[] MengeOptioon)
		{
			if (null == MengeOptioon)
			{
				return null;
			}

			var MengeKandidaatEscaped =
				MengeOptioon
				.Where((Kandidaat) => null != Kandidaat)
				.Select((Kandidaat) =>
				{
					if (0 < Kandidaat.Length && Kandidaat.TrimNullable().Length < 1)
					{
						return @"\s";
					}

					return Regex.Escape(Kandidaat);
				}).ToArray();

			return
				"(" +
				string.Join(
				"|",
				MengeKandidaatEscaped) +
				")";
		}

		static public string NumberFormatRegexPatternBerecne(
			string[] MengeSignOptioon,
			string[] MengeDezimaaltrenzaiceOptioon,
			string[] MengeZiferngrupiirungOptioon,
			bool AkzeptiireVorherig = false,
			bool AkzeptiireFolgend = false,
			string GrupeIdentSufix = null)
		{
			var InNumberRegexPatternZiferngrupiirungGrupeNaame =
				Extension.InNumberRegexPatternZiferngrupiirungGrupeNaame + (GrupeIdentSufix ?? "");

			var InNumberRegexPatternSignGrupeNaame =
				Extension.InNumberRegexPatternSignGrupeNaame + (GrupeIdentSufix ?? "");

			var InNumberRegexPatternVorDezimaaltrenzaiceGrupeNaame =
				Extension.InNumberRegexPatternVorDezimaaltrenzaiceGrupeNaame + (GrupeIdentSufix ?? "");

			MengeSignOptioon = (MengeSignOptioon ?? new string[0]).Where((Kandidaat) => null != Kandidaat).Concat(new string[] { "" }).ToArray();

			MengeZiferngrupiirungOptioon = (MengeZiferngrupiirungOptioon ?? new string[0]).Where((Kandidaat) => null != Kandidaat).ToArray();

			var TailSign = RegexPatternAlternatiiveBerecne(MengeSignOptioon);

			var PatternDezimaaltrenzaice = "(?!\\<" + InNumberRegexPatternZiferngrupiirungGrupeNaame + ">)" + RegexPatternAlternatiiveBerecne(MengeDezimaaltrenzaiceOptioon);
			var PatternZiferngrupiirung = RegexPatternAlternatiiveBerecne(MengeZiferngrupiirungOptioon);

			/*
			 * 2015.02.27
			 * 
			 * Änderung: nur noc Grupe direkt vor Dezimaltrenzaice mus drai Zifern enthalte. Grupe Linx davon dürfe zwai oder drai Zifern enthalte.
			 * 
			//	Vorersct nur Ziferngrupe mit drai Zaice erlaube.
			var TailVorDezimaaltrenzaice =
				"\\d+(" +
				"((?<" + InNumberRegexPatternZiferngrupiirungGrupeNaame + ">" + PatternZiferngrupiirung + ")\\d{3})" +
				"(\\<" + InNumberRegexPatternZiferngrupiirungGrupeNaame + ">\\d{3})*|)";
			 * */

			//	Grupe direkt vor Dezimaltrenzaice mus drai Zifern enthalte. Grupe Linx davon dürfe zwai oder drai Zifern enthalte.
			//	d.h. inerhalb der optionaale Grupe welce ale Zaice zwisce inklusiiv früühescte Grupetrenzaice und Dezimaltrenzaice enthalt
			//	isc zuusäzlic auf linker Saite optionaale Grupe für Ziferngrupe mit variabler Anzaal von Zifern enthalte.
			//	da di Grupe welce das Zaice für Ziferngrupiirung ersctmaals fängt linx von ale andere vorkome des Ziferngrupiirungszaice scteehe sol werd inerhalb
			//	der optionaale Grupe di Folge von Ziferngrupe und Ziferngrupiirungszaice umgekeert.
			var TailVorDezimaaltrenzaiceTailOptioonTailOptioonVarAnz =
				@"(\d{2,3}" +
				@"\<" + InNumberRegexPatternZiferngrupiirungGrupeNaame + @">)*";

			var TailVorDezimaaltrenzaiceTailOptioon =
				"((?<" + InNumberRegexPatternZiferngrupiirungGrupeNaame + ">" + PatternZiferngrupiirung + @")" +
				TailVorDezimaaltrenzaiceTailOptioonTailOptioonVarAnz +
				@"\d{3})";

			var TailVorDezimaaltrenzaice =
				@"\d+" +
				@"((" + TailVorDezimaaltrenzaiceTailOptioon + ")|)";

			//	Vorersct naac Dezimaaltrenzaice Zifer Anzaal unglaic drai erlaube.
			var TailNaacDezimaaltrenzaice = @"(\d{0,2}|\d{4,})";

			var Begin = AkzeptiireVorherig ? "" : "^\\s*";

			var Ende =
				//	2015.02.27	Zuusaz: Aussclus naacfolgende Zifer (negative lookahead)
				@"(?!\d)" +
				(AkzeptiireFolgend ? "" : "\\s*$");

			return
				Begin +
				"(?<" + InNumberRegexPatternSignGrupeNaame + ">" +
				TailSign + ")" +
				"(?<" + InNumberRegexPatternVorDezimaaltrenzaiceGrupeNaame + ">" +
				TailVorDezimaaltrenzaice + ")" +
				"(|(?<" + InNumberRegexPatternDezimaaltrenzaiceGrupeNaame + ">" + PatternDezimaaltrenzaice + ")" +
				"(?<" + InNumberRegexPatternNaacDezimaaltrenzaiceGrupeNaame + ">" +
				TailNaacDezimaaltrenzaice +
				"))" +
				Ende;
		}

		static public MenuEntry MenuEntryTargetUnLock(
			this	Menu Menu)
		{
			if (null == Menu)
			{
				return null;
			}

			return MenuEntryTargetUnLock(Menu.ListeEntry);
		}

		static public MenuEntry MenuEntryTargetUnLock(
			this	IEnumerable<MenuEntry> MengeMenuEntry)
		{
			return MengeMenuEntry.FirstOrDefaultNullable((KandidaatEntry) => Regex.Match(KandidaatEntry.Bescriftung ?? "", "unlock", RegexOptions.IgnoreCase).Success);
		}

		static public bool TargetBescriftungOreTypPasendZuSurveyScanEntryBescriftung(
			string TargetBescriftungOreTyp,
			string SurveyScanEntryBescriftung)
		{
			try
			{
				return ScatescpaicerTargetBescriftungOreTypPasendZuSurveyScanEntryBescriftung
					.ValueFürKey(new KeyValuePair<string, string>(TargetBescriftungOreTyp, SurveyScanEntryBescriftung),
					(TargetBescriftungUndSurveyScanEntryBescriftung) => TargetBescriftungOreTypPasendZuSurveyScanEntryBescriftungBerecne(
						TargetBescriftungUndSurveyScanEntryBescriftung.Key,
						TargetBescriftungUndSurveyScanEntryBescriftung.Value));
			}
			finally
			{
				ScatescpaicerTargetBescriftungOreTypPasendZuSurveyScanEntryBescriftung.BescrankeEntferneLängerNitVerwendete(30);
			}
		}

		static string AsteroidOreTypBescriftungAbgebildetFürVerglaicZwisceTargetUndSurveyScanListEntry(
			string Bescriftung)
		{
			if (null == Bescriftung)
			{
				return null;
			}

			return Regex.Replace(Bescriftung, "[^\\w\\d]+", "");
		}

		static bool TargetBescriftungOreTypPasendZuSurveyScanEntryBescriftungBerecne(
			string TargetBescriftungOreTyp,
			string SurveyScanEntryBescriftung)
		{
			if (string.Equals(TargetBescriftungOreTyp, SurveyScanEntryBescriftung, StringComparison.InvariantCultureIgnoreCase))
			{
				return true;
			}

			if (null == TargetBescriftungOreTyp)
			{
				return false;
			}

			if (null == SurveyScanEntryBescriftung)
			{
				return false;
			}

			return string.Equals(
				AsteroidOreTypBescriftungAbgebildetFürVerglaicZwisceTargetUndSurveyScanListEntry(TargetBescriftungOreTyp),
				AsteroidOreTypBescriftungAbgebildetFürVerglaicZwisceTargetUndSurveyScanListEntry(SurveyScanEntryBescriftung),
				StringComparison.InvariantCultureIgnoreCase);
		}

		static public string AusBescriftungOreTypBerecneRegexPattern(string BescriftungOreTyp)
		{
			if (null == BescriftungOreTyp)
			{
				return null;
			}

			return Regex.Replace(BescriftungOreTyp, "[^\\w\\d]+", "[^\\w\\d]*");
		}

		static Regex ZuOreTypeRegexBerecne(OreTypSictEnum OreType)
		{
			var RegexPattern = Regex.Replace(OreType.ToString(), "[\\s_]+", "[\\s]*");

			return new Regex(RegexPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
		}

		static public Regex ZuOreTypeRegex(OreTypSictEnum OreType)
		{
			return ScatenscpaicerZuOreTypeRegex.ValueFürKey(OreType, ZuOreTypeRegexBerecne);
		}

		static public OreTypSictEnum? OreTypBerecneAusOreTypSictString(string OreTypSictString)
		{
			if (null == OreTypSictString)
			{
				return null;
			}

			foreach (var OreTyp in Enum.GetValues(typeof(OreTypSictEnum)))
			{
				var OreTypScpez = (OreTypSictEnum)OreTyp;

				var Regex = ZuOreTypeRegex(OreTypScpez);

				if (Regex.Match(OreTypSictString).Success)
				{
					return OreTypScpez;
				}
			}

			return null;
		}

		/// <summary>
		/// Eve Online Client bildet Zaale abhängig von Konfig Windows ab ("Region und Sprache" -> "Formate").
		/// Diise Funktioon sol Zaale ainleese köne welce mit deen Formate en[us] oder de[de] abgebildet wurde.
		/// Hiirfür entscaidet sii anhand der Anordnung ob Punkt und Koma als Dezimaaltrenzaice oder als Symbool für Zifergrupiirung verwendet werden.
		/// 
		/// 
		/// </summary>
		/// <param name="ZaalSictString"></param>
		/// <returns></returns>
		static public Int64? SctandardNumberFormatZaalBetraagMili_MultiKultur(
			string ZaalSictString)
		{
			if (null == ZaalSictString)
			{
				return null;
			}

			var MengeRegexMatch = SctandardNumberFormatRegex.Matches(ZaalSictString.TrimNullable());

			if (null == MengeRegexMatch)
			{
				return null;
			}

			var MengeKandidaat = new List<Int64>();

			foreach (var RegexMatch in MengeRegexMatch.OfType<Match>())
			{
				if (!RegexMatch.Success)
				{
					continue;
				}

				var TailSign = RegexMatch.Groups[InNumberRegexPatternSignGrupeNaame].Value;

				var SignBetraag = 1;

				if ("-" == TailSign)
				{
					SignBetraag = -1;
				}

				var TailVorDezimaaltrenzaice = RegexMatch.Groups[InNumberRegexPatternVorDezimaaltrenzaiceGrupeNaame].Value;
				var TailNaacDezimaaltrenzaice = RegexMatch.Groups[InNumberRegexPatternNaacDezimaaltrenzaiceGrupeNaame].Value;

				var TailVorDezimaaltrenzaiceTailZifer = Regex.Replace(TailVorDezimaaltrenzaice, "[^\\d]+", "");
				var TailNaacDezimaaltrenzaiceTailZifer = Regex.Replace(TailNaacDezimaaltrenzaice, "[^\\d]+", "");

				var TailVorDezimaaltrenzaiceBetraag =
					0 < TailVorDezimaaltrenzaiceTailZifer.Length ?
					Int64.Parse(TailVorDezimaaltrenzaiceTailZifer) :
					0;

				var TailNaacDezimaaltrenzaiceBetraagMikro =
					0 < TailNaacDezimaaltrenzaiceTailZifer.Length ?
					(Int64)(Int64.Parse(TailNaacDezimaaltrenzaiceTailZifer) * Math.Pow(10, 6 - TailNaacDezimaaltrenzaiceTailZifer.Length)) :
					0;

				MengeKandidaat.Add(
					SignBetraag *
					(TailVorDezimaaltrenzaiceBetraag * 1000 + TailNaacDezimaaltrenzaiceBetraagMikro / 1000));
			}

			if (0 < MengeKandidaat.Count)
			{
				return MengeKandidaat.Max();
			}

			return null;
		}

		static public Int64? SctandardNumberFormatZaalBetraagMili(
			string ZaalSictString)
		{
			return SctandardNumberFormatZaalBetraagMili_MultiKultur(ZaalSictString);
		}

		static public Int64? SctandardNumberFormatZaalBetraagMili(
			string ZaalSictString,
			System.Globalization.NumberFormatInfo NumberFormat,
			System.Globalization.NumberStyles NumberStyle = NumberStyles.Number)
		{
			if (null == ZaalSictString)
			{
				return null;
			}

			/*
			 * 2013.07.14
			 * Bsp: "1.584.570"
			 * Bsp: "8,3"
			 * */

			ZaalSictString = ZaalSictString.Trim();

			double ZaalAlsDouble;

			if (double.TryParse(
				ZaalSictString,
				NumberStyle,
				NumberFormat,
				out	ZaalAlsDouble))
			{
				var DistanzMili = (Int64)(ZaalAlsDouble * 1e+3);

				return DistanzMili;
			}

			return null;
		}

		static public Int64? SctandardNumberFormatZaalBetraag(string ZaalSictString)
		{
			return SctandardNumberFormatZaalBetraagMili(ZaalSictString) / 1000;
		}

		/// <summary>
		/// http://www.nature.com/news/the-astronomical-unit-gets-fixed-1.11416
		/// Wert aus 2012.August vote at the International Astronomical Union's meeting in Beijing, China
		/// </summary>
		public const Int64 AstronomicalUnit = 149597870700;

		static public void AusDistanceSictStringAbbildGanzzaalDurcMeeter(
			string DistanceSictString,
			out	Int64? DistanceScrankeMin,
			out	Int64? DistanceScrankeMax)
		{
			DistanceScrankeMin = null;
			DistanceScrankeMax = null;

			if (null == DistanceSictString)
			{
				return;
			}

			/*
			 * 2015.02.27
			 * 
			 * Feelsclaag für Bsp: "9,121.00 m"
			 * Feelsclaag für Bsp: "9,121 m"
			 * 
			var Match = Regex.Match(DistanceSictString, DistanceAufgetailungNaacKomponenteRegexPattern);

			if (!Match.Success)
			{
				return;
			}

			var TailZaalSictString = Match.Groups[1].Value;
			var TailAinhaitSictString = Match.Groups[2].Value;
			 * */

			var Match = Regex.Match(DistanceSictString, TempAuswertGbs.Extension.DistanceRegexPattern);

			if (!Match.Success)
			{
				return;
			}

			var TailBetraagSictString = Match.Groups[TempAuswertGbs.Extension.DistanceRegexPatternGroupBetraagIdent].Value;
			var TailAinhaitSictString = Match.Groups[TempAuswertGbs.Extension.DistanceRegexPatternGroupAinhaitIdent].Value;

			Int64? Ainheit = null;

			if (string.Equals("m", TailAinhaitSictString))
			{
				Ainheit = 1;
			}

			if (string.Equals("km", TailAinhaitSictString))
			{
				Ainheit = 1000;
			}

			if (string.Equals("AU", TailAinhaitSictString))
			{
				Ainheit = AstronomicalUnit;
			}

			var BetraagMili = TempAuswertGbs.Extension.SctandardNumberFormatZaalBetraagMili(TailBetraagSictString);

			var BescriftungDistanz = (BetraagMili * Ainheit) / 1000;

			//	Tatsäclic vorhandene Naackomasctele wern hiir vorersct ignoriirt.
			var DiferenzVonMinNaacMax = Ainheit;

			if (1e+6 < Ainheit)
			{
				//	Eve Online Client 2013.11.20 Rubikon verwendet für ainhait "AU" aine naackomasctele
				DiferenzVonMinNaacMax = Ainheit / 10;
			}

			/*
			 * 2013.11.20 Rubikon:
			 * Beobactung Rundungsverhalten in Overview Zaile:
			 * bai Annäherung an Objekt Übergang von "10 km" naac "9.995 m"
			 * */

			DistanceScrankeMin = BescriftungDistanz;
			DistanceScrankeMax = DistanceScrankeMin + DiferenzVonMinNaacMax;
		}

		static public Int64? AusDistanceSictStringAbbildGanzzaalDurcMeeterScrankeMin(
			string DistanceSictString)
		{
			Int64? DistanceScrankeMin;
			Int64? DistanceScrankeMax;

			AusDistanceSictStringAbbildGanzzaalDurcMeeter(DistanceSictString, out	DistanceScrankeMin, out	DistanceScrankeMax);

			return DistanceScrankeMin;
		}

		static public Int64? AusDistanceSictStringAbbildGanzzaalDurcMeeterScrankeMax(
			string DistanceSictString)
		{
			Int64? DistanceScrankeMin;
			Int64? DistanceScrankeMax;

			AusDistanceSictStringAbbildGanzzaalDurcMeeter(DistanceSictString, out	DistanceScrankeMin, out	DistanceScrankeMax);

			return DistanceScrankeMax;
		}

		static readonly public string VonStringMissionLocationNameKomponentePlanetRegexPattern =
			"(.{3,99})\\s+([IVXLCDM]+)\\s*$";

		static public SictAusGbsLocationInfo VonStringMissionLocationNameExtraktLocationInfo(
			string MissionLocationName)
		{
			if (null == MissionLocationName)
			{
				return null;
			}

			var ListeKomponente = MissionLocationName.Split(new char[] { '-' });

			var KomponentePlanet = ListeKomponente.FirstOrDefault();

			string SolarSystemName = null;

			var KomponentePlanetMitZaalMatch = Regex.Match(KomponentePlanet, VonStringMissionLocationNameKomponentePlanetRegexPattern);

			if (KomponentePlanetMitZaalMatch.Success)
			{
				SolarSystemName = KomponentePlanetMitZaalMatch.Groups[1].Value.Trim();
			}
			else
			{
				SolarSystemName = KomponentePlanet.Trim();
			}

			var Location = new SictAusGbsLocationInfo(
                MissionLocationName.TrimNullable(),
				SolarSystemName);

			return Location;
		}

		static public SictAusGbsLocationInfo VonStringCurrentStationStationExtraktLocationInfo(
			string CurrentStation)
		{
			return VonStringMissionLocationNameExtraktLocationInfo(CurrentStation);
		}


	}
}
