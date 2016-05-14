using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bib3;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Optimat.EveOnline.Base;

namespace Optimat.EveOnline
{
	/*
	 * 2015.02.23
	 * 
	 * Ersaz durc Bib3.OrtogoonInt
	 * 
	[JsonObject(MemberSerialization.OptIn)]
	public class OrtogoonSingle
	{
		[JsonProperty]
		public SictVektor2DSingle MiteLaage;

		[JsonProperty]
		public SictVektor2DSingle Grööse;

		public float Betraag()
		{
			return Grööse.A * Grööse.B;
		}

		static public float? GrenzeLinx(
			OrtogoonSingle Fläce)
		{
			if (null == Fläce)
			{
				return null;
			}

			return Fläce.EkeLinxOobeLaage().A;
		}

		static public float? GrenzeRecz(
			OrtogoonSingle Fläce)
		{
			if (null == Fläce)
			{
				return null;
			}

			return Fläce.EkeReczUnteLaage().A;
		}

		static public bool Glaicwertig(
			OrtogoonSingle O0,
			OrtogoonSingle O1)
		{
			if (O0 == O1)
			{
				return true;
			}

			if (null == O0 || null == O1)
			{
				return false;
			}

			return O0.MiteLaage == O1.MiteLaage && O0.Grööse == O1.Grööse;
		}

		static public float? DiferenzLaageUndGrööseSume(
			OrtogoonSingle O0,
			OrtogoonSingle O1)
		{
			if (object.Equals(O0, O1))
			{
				return 0;
			}

			if (null == O0)
			{
				return float.MaxValue;
			}

			if (null == O1)
			{
				return float.MaxValue;
			}

			var DiferenzLaage = O1.MiteLaage - O0.MiteLaage;
			var DiferenzGrööse = O1.MiteLaage - O0.MiteLaage;

			return DiferenzLaage.A + DiferenzLaage.B + DiferenzGrööse.A + DiferenzGrööse.B;
		}

		public SictVektor2DSingle[] ListeEkeLaage()
		{
			var ListeEkeLaage = new SictVektor2DSingle[4];

			for (int EkeIndex = 0; EkeIndex < 4; EkeIndex++)
			{
				/*
				 * 2015.01.05
				 * 
				 * Änderung Ordnung.
				 * 
				var EkeVersazVonMite =
					new SictVektor2DSingle(
						(float)(Grööse.A * (0.5 - (EkeIndex % 2))),
						(float)(Grööse.B * (0.5 - ((EkeIndex / 2) % 2))));
				 * *

				var RictungAGrenzeIndex = (((EkeIndex + 1) / 2) % 2);
				var RictungBGrenzeIndex = (((EkeIndex + 0) / 2) % 2);

				var EkeVersazVonMite =
					new SictVektor2DSingle(
						(float)(Grööse.A * (RictungAGrenzeIndex - 0.5)),
						(float)(Grööse.B * (RictungBGrenzeIndex - 0.5)));

				var EkeLaage = MiteLaage + EkeVersazVonMite;

				ListeEkeLaage[EkeIndex] = EkeLaage;
			}

			return ListeEkeLaage;
		}

		public OrtogoonSingle()
			:
			this(default(SictVektor2DSingle), default(SictVektor2DSingle))
		{
		}

		public OrtogoonSingle(
			SictVektor2DSingle MiteLaage,
			SictVektor2DSingle Grööse)
		{
			this.MiteLaage = MiteLaage;
			this.Grööse = Grööse;
		}

		public OrtogoonSingle(
			OrtogoonSingle ZuKopiire)
			:
			this(
			null == ZuKopiire ? default(SictVektor2DSingle) : ZuKopiire.MiteLaage,
			null == ZuKopiire ? default(SictVektor2DSingle) : ZuKopiire.Grööse)
		{
		}

		public OrtogoonSingle Vergröösert(
			float VergrööserungRictungA,
			float VergrööserungRictungB)
		{
			return new OrtogoonSingle(MiteLaage, Grööse + new SictVektor2DSingle(VergrööserungRictungA, VergrööserungRictungB));
		}

		public OrtogoonSingle VergröösertBescrankt(
			float VergrööserungRictungA,
			float VergrööserungRictungB,
			float? GrööseAScrankeMin,
			float? GrööseBScrankeMin)
		{
			var GrööseA = Grööse.A + VergrööserungRictungA;
			var GrööseB = Grööse.B + VergrööserungRictungB;

			if (GrööseAScrankeMin.HasValue)
			{
				GrööseA = Math.Max(GrööseAScrankeMin.Value, GrööseA);
			}

			if (GrööseBScrankeMin.HasValue)
			{
				GrööseB = Math.Max(GrööseBScrankeMin.Value, GrööseB);
			}

			return new OrtogoonSingle(MiteLaage, new SictVektor2DSingle(GrööseA, GrööseB));
		}

		public OrtogoonSingle Versezt(
			float VersazRictungA,
			float VersazRictungB)
		{
			return Versezt(new SictVektor2DSingle(VersazRictungA, VersazRictungB));
		}

		public OrtogoonSingle Versezt(
			SictVektor2DSingle Versaz)
		{
			return new OrtogoonSingle(MiteLaage + Versaz, Grööse);
		}

		public SictVektor2DSingle EkeLinxOobeLaage()
		{
			return MiteLaage - Grööse * 0.5;
		}

		public SictVektor2DSingle EkeReczUnteLaage()
		{
			return MiteLaage + Grööse * 0.5;
		}

		public bool PunktLiigtInFläce(SictVektor2DSingle Punkt)
		{
			if (Punkt.A < MiteLaage.A - Grööse.A)
			{
				return false;
			}

			if (Punkt.A > MiteLaage.A + Grööse.A)
			{
				return false;
			}

			if (Punkt.B < MiteLaage.B - Grööse.B)
			{
				return false;
			}

			if (Punkt.B > MiteLaage.B + Grööse.B)
			{
				return false;
			}

			return true;
		}

		public KeyValuePair<SictVektor2DSingle, SictVektor2DSingle> GibPunktMinUndPunktMax()
		{
			var GrööseAbsHalb = new SictVektor2DSingle(Math.Abs(Grööse.A), Math.Abs(Grööse.B)) * 0.5;

			var Min = MiteLaage - GrööseAbsHalb;
			var Max = MiteLaage + GrööseAbsHalb;

			return new KeyValuePair<SictVektor2DSingle, SictVektor2DSingle>(Min, Max);
		}

		override public string ToString()
		{
			return JsonConvert.SerializeObject(this, Formatting.Indented);
		}

		static public OrtogoonSingle FläceAusEkeMininumLaageUndGrööse(
			SictVektor2DSingle EkeMinimumLaage,
			SictVektor2DSingle Grööse)
		{
			return new OrtogoonSingle(EkeMinimumLaage + Grööse * 0.5, Grööse);
		}

		static public OrtogoonSingle AusMinMax(
			float AMin, float AMax, float BMin, float BMax)
		{
			var Grööse = new SictVektor2DSingle(AMax - AMin, BMax - BMin);

			var MiteLaage = new SictVektor2DSingle((AMax + AMin) * 0.5f, (BMax + BMin) * 0.5f);

			return new OrtogoonSingle(MiteLaage, Grööse);
		}

		static public bool FläceLiigtInerhalbFläce(
			OrtogoonSingle FläceInen,
			OrtogoonSingle FläceAusen)
		{
			if (null == FläceInen || null == FläceAusen)
			{
				return false;
			}

			return FläceInen.ListeEkeLaage().All((FläceInenEke) => FläceAusen.PunktLiigtInFläce(FläceInenEke));
		}

		static bool PunktLiigtInRegioon1D(
			float RegioonMin,
			float RegioonMax,
			float Punkt)
		{
			return (RegioonMin <= Punkt && Punkt <= RegioonMax);
		}

		static float[] ListeGrenzeAusÜberscnaidung1D(
			float RegioonAScrankeMin,
			float RegioonAScrankeMax,
			float RegioonBScrankeMin,
			float RegioonBScrankeMax)
		{
			var RegioonAMinMax = new KeyValuePair<float, float>(RegioonAScrankeMin, RegioonAScrankeMax);
			var RegioonBMinMax = new KeyValuePair<float, float>(RegioonBScrankeMin, RegioonBScrankeMax);

			var MengeKandidaat = new KeyValuePair<float, KeyValuePair<float, float>>[]{
				new	KeyValuePair<float,	KeyValuePair<float,	float>>(RegioonAScrankeMin,RegioonBMinMax),
				new	KeyValuePair<float,	KeyValuePair<float,	float>>(RegioonAScrankeMax,RegioonBMinMax),
				new	KeyValuePair<float,	KeyValuePair<float,	float>>(RegioonBScrankeMin,RegioonAMinMax),
				new	KeyValuePair<float,	KeyValuePair<float,	float>>(RegioonBScrankeMax,RegioonAMinMax),};

			var ListeGrenzePunkt =
				MengeKandidaat
				.Where((Kandidaat) => PunktLiigtInRegioon1D(Kandidaat.Value.Key, Kandidaat.Value.Value, Kandidaat.Key))
				.Select((Kandidaat) => Kandidaat.Key)
				.ToArray();

			var ListeGrenzePunktDistinct =
				ListeGrenzePunkt.Distinct().ToArray();

			return ListeGrenzePunktDistinct;
		}

		static public bool ScnitfläceLeer(OrtogoonSingle O0, OrtogoonSingle O1)
		{
			if (null == O0)
			{
				return true;
			}

			if (null == O1)
			{
				return true;
			}

			var O0MinMax = O0.GibPunktMinUndPunktMax();
			var O1MinMax = O1.GibPunktMinUndPunktMax();

			if (O0MinMax.Value.A <= O1MinMax.Key.A ||
				O0MinMax.Value.B <= O1MinMax.Key.B ||
				O1MinMax.Value.A <= O0MinMax.Key.A ||
				O1MinMax.Value.B <= O0MinMax.Key.B)
			{
				//	Scpez Fal kaine Scnitmenge
				return true;
			}

			return false;
		}

		static public OrtogoonSingle Scnitfläce(OrtogoonSingle O0, OrtogoonSingle O1)
		{
			if (null == O0)
			{
				return null;
			}

			if (null == O1)
			{
				return null;
			}

			var O0MinMax = O0.GibPunktMinUndPunktMax();
			var O1MinMax = O1.GibPunktMinUndPunktMax();

			var ScnitfläceMinA = Math.Max(O0MinMax.Key.A, O1MinMax.Key.A);
			var ScnitfläceMaxA = Math.Min(O0MinMax.Value.A, O1MinMax.Value.A);

			var ScnitfläceMinB = Math.Max(O0MinMax.Key.B, O1MinMax.Key.B);
			var ScnitfläceMaxB = Math.Min(O0MinMax.Value.B, O1MinMax.Value.B);

			var ScnitfläceMiteLaage = new SictVektor2DSingle(
				(ScnitfläceMinA + ScnitfläceMaxA) * 0.5f,
				(ScnitfläceMinB + ScnitfläceMaxB) * 0.5f);

			var ScnitfläceGrööse = new SictVektor2DSingle(
				ScnitfläceMaxA - ScnitfläceMinA,
				ScnitfläceMaxB - ScnitfläceMinB);

			return new OrtogoonSingle(ScnitfläceMiteLaage, ScnitfläceGrööse);
		}

		static public OrtogoonSingle[] FläceMiinusFläce(OrtogoonSingle[] MengeMinuend, OrtogoonSingle Subtrahend)
		{
			if (null == MengeMinuend)
			{
				return null;
			}

			var DiferenzMengeFläce = new List<OrtogoonSingle>();

			foreach (var Minuend in MengeMinuend)
			{
				var FürMinduendDiff = FläceMiinusFläce(Minuend, Subtrahend);

				if (null == FürMinduendDiff)
				{
					continue;
				}

				DiferenzMengeFläce.AddRange(FürMinduendDiff);
			}

			return DiferenzMengeFläce.ToArray();
		}

		/// <summary>
		/// gibt Diferenz als Menge von Rectek zurük
		/// </summary>
		/// <param name="Minuend"></param>
		/// <param name="Subtrahend"></param>
		/// <returns></returns>
		static public OrtogoonSingle[] FläceMiinusFläce(OrtogoonSingle Minuend, OrtogoonSingle Subtrahend)
		{
			if (null == Minuend)
			{
				return null;
			}

			if (null == Subtrahend)
			{
				return new OrtogoonSingle[] { new OrtogoonSingle(Minuend) };
			}

			var MinuendMinMax = Minuend.GibPunktMinUndPunktMax();
			var SubtrahendMinMax = Subtrahend.GibPunktMinUndPunktMax();

			if (MinuendMinMax.Value.A <= SubtrahendMinMax.Key.A ||
				MinuendMinMax.Value.B <= SubtrahendMinMax.Key.B ||
				SubtrahendMinMax.Value.A <= MinuendMinMax.Key.A ||
				SubtrahendMinMax.Value.B <= MinuendMinMax.Key.B)
			{
				//	Scpez Fal kaine Scnitmenge
				return new OrtogoonSingle[] { new OrtogoonSingle(Minuend) };
			}

			if (MinuendMinMax.Value.A <= SubtrahendMinMax.Value.A &&
				MinuendMinMax.Value.B <= SubtrahendMinMax.Value.B &&
				SubtrahendMinMax.Key.A <= MinuendMinMax.Key.A &&
				SubtrahendMinMax.Key.B <= MinuendMinMax.Key.B)
			{
				//	Scpez Fal Minuend liigt volsctändig in Subtrahend
				return new OrtogoonSingle[0];
			}

			float[] RictungAMengeScranke =
				ListeGrenzeAusÜberscnaidung1D(
				MinuendMinMax.Key.A,
				MinuendMinMax.Value.A,
				SubtrahendMinMax.Key.A,
				SubtrahendMinMax.Value.A)
				.OrderBy((t) => t)
				.ToArray();

			float[] RictungBMengeScranke =
				ListeGrenzeAusÜberscnaidung1D(
				MinuendMinMax.Key.B,
				MinuendMinMax.Value.B,
				SubtrahendMinMax.Key.B,
				SubtrahendMinMax.Value.B)
				.OrderBy((t) => t)
				.ToArray();

			if (RictungAMengeScranke.Length < 1 || RictungBMengeScranke.Length < 1)
			{
				//	Scpez Fal kaine Scnitmenge,	(Redundant zur Prüüfung oobn)
				return new OrtogoonSingle[] { new OrtogoonSingle(Minuend) };
			}

			var RictungAMengeScrankeMitMinuendGrenze =
				new float[] { MinuendMinMax.Key.A }.Concat(RictungAMengeScranke).Concat(new float[] { MinuendMinMax.Value.A }).ToArray();

			var ListeFläce = new List<OrtogoonSingle>();

			for (int RictungAScrankeIndex = 0; RictungAScrankeIndex < RictungAMengeScrankeMitMinuendGrenze.Length - 1; RictungAScrankeIndex++)
			{
				var RictungAScrankeMinLaage = RictungAMengeScrankeMitMinuendGrenze[RictungAScrankeIndex];

				var RictungAScrankeMaxLaage = RictungAMengeScrankeMitMinuendGrenze[RictungAScrankeIndex + 1];

				if (SubtrahendMinMax.Value.A <= RictungAScrankeMinLaage ||
					RictungAScrankeMaxLaage <= SubtrahendMinMax.Key.A)
				{
					//	in RictungB unbescrankter Abscnit
					ListeFläce.Add(OrtogoonSingle.AusMinMax(RictungAScrankeMinLaage, RictungAScrankeMaxLaage, MinuendMinMax.Key.B, MinuendMinMax.Value.B));
				}
				else
				{
					var RictungBMengeScrankeFrüheste = RictungBMengeScranke.First();
					var RictungBMengeScrankeLezte = RictungBMengeScranke.Last();

					if (MinuendMinMax.Key.B < SubtrahendMinMax.Key.B)
					{
						ListeFläce.Add(OrtogoonSingle.AusMinMax(RictungAScrankeMinLaage, RictungAScrankeMaxLaage, MinuendMinMax.Key.B, SubtrahendMinMax.Key.B));
					}

					if (SubtrahendMinMax.Value.B < MinuendMinMax.Value.B)
					{
						ListeFläce.Add(OrtogoonSingle.AusMinMax(RictungAScrankeMinLaage, RictungAScrankeMaxLaage, SubtrahendMinMax.Value.B, MinuendMinMax.Value.B));
					}
				}
			}

			return ListeFläce.ToArray();
		}
	}
	 * */

	[JsonObject(MemberSerialization.OptIn)]
	public class SictNaacZiilprozesWirkungMitBescriftungZait
	{
		[JsonProperty]
		public string ZaitSictKalender;

		[JsonProperty]
		public SictVorsclaagNaacProcessWirkung Wirkung;

		public SictNaacZiilprozesWirkungMitBescriftungZait(
			SictVorsclaagNaacProcessWirkung Wirkung,
			Int64 StopwatchZaitMikro)
		{
			this.Wirkung = Wirkung;

			var ZaitDateTime = Bib3.Glob.SictDateTimeVonStopwatchZaitMikro(StopwatchZaitMikro);

			ZaitSictKalender = Bib3.Glob.SictwaiseKalenderString(ZaitDateTime, ".", 0);
		}

		override public string ToString()
		{
			return JsonConvert.SerializeObject(this, Formatting.Indented);
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictNaacProcessWirkungKey
	{
		[JsonProperty]
		public int[] MengeKey;

		[JsonProperty]
		public int[] MengeModifier;

		public SictNaacProcessWirkungKey()
			:
			this(null,	null)
		{
		}

		public SictNaacProcessWirkungKey(
			int[] MengeKey,
			int[] MengeModifier = null)
		{
			this.MengeKey = MengeKey;
			this.MengeModifier = MengeModifier;
		}

		public SictNaacProcessWirkungKey(
			int[] MengeKey,
			int Modifier)
			:
			this(
			MengeKey,
			new int[] { Modifier })
		{
		}

		public SictNaacProcessWirkungKey(
			int Key,
			int? Modifier = null)
			:
			this(
			new int[] { Key },
			Modifier.HasValue ? new int[] { Modifier.Value } : null)
		{
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictNaacProcessWirkungTailMausErgeebnis
	{
		[JsonProperty]
		public OrtogoonInt[] MausPfaadListeWeegpunktFläceErgeebnis;

		[JsonProperty]
		public OrtogoonInt[] MausPfaadMengeFläceZuMaideErgeebnis;

		[JsonProperty]
		public Vektor2DInt? MausPfaadBeginPunkt;

		[JsonProperty]
		public Vektor2DInt? MausPfaadEndePunkt;

		[JsonProperty]
		public bool? MausPfaadInZiilWindowClientRect;

		[JsonProperty]
		public bool? Erfolg;

		public SictNaacProcessWirkungTailMausErgeebnis()
		{
		}

		public SictNaacProcessWirkungTailMausErgeebnis(
			OrtogoonInt[] MausPfaadListeWeegpunktFläceErgeebnis,
			OrtogoonInt[] MausPfaadMengeFläceZuMaideErgeebnis,
			Vektor2DInt? MausPfaadBeginPunkt = null,
			Vektor2DInt? MausPfaadEndePunkt = null,
			bool? MausPfaadInZiilWindowClientRect = null,
			bool? Erfolg	= null)
		{
			this.MausPfaadListeWeegpunktFläceErgeebnis = MausPfaadListeWeegpunktFläceErgeebnis;
			this.MausPfaadMengeFläceZuMaideErgeebnis = MausPfaadMengeFläceZuMaideErgeebnis;
			this.MausPfaadBeginPunkt = MausPfaadBeginPunkt;
			this.MausPfaadEndePunkt = MausPfaadEndePunkt;
			this.MausPfaadInZiilWindowClientRect = MausPfaadInZiilWindowClientRect;
			this.Erfolg = Erfolg;
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictVorsclaagWirkungAbhängigkaitVonWirkungErgeebnis
	{
		/// <summary>
		/// Bezaicnet deen VorsclaagWirkung desen Wirkung Ergeebnis zur Bedingung gemact werde sol.
		/// </summary>
		[JsonProperty]
		public Int64? VorsclaagWirkungIdent;

		/// <summary>
		/// untere Scranke für Zait Distanz in Milisekunde zwisce der durc WirkungIdent bezaicnete Wirkung.EndeZait und abhängige Wirkung.BeginZait
		/// </summary>
		[JsonProperty]
		public int? ZaitDistanzScrankeMinMili;

		/// <summary>
		/// wen true dan sol di abhängige Wirkung auc durcgefüürt werde fals di durc WirkungIdent bezaicnete Wirkung nit erfolgraic war.
		/// (Es blaibt als Abhängigkait noc üübrig das di durc WirkungIdent bezaicnete Wirkung zumindest Versuuct wurde.)
		/// </summary>
		[JsonProperty]
		public bool? UnabhängigVonErfolg;

		public SictVorsclaagWirkungAbhängigkaitVonWirkungErgeebnis()
		{
		}

		public SictVorsclaagWirkungAbhängigkaitVonWirkungErgeebnis(
			Int64? VorsclaagWirkung,
			int? ZaitDistanzScrankeMinMili,
			bool? UnabhängigVonErfolg)
		{
			this.VorsclaagWirkungIdent = VorsclaagWirkung;
			this.ZaitDistanzScrankeMinMili = ZaitDistanzScrankeMinMili;
			this.UnabhängigVonErfolg = UnabhängigVonErfolg;
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class InProcessGbsFläceRectekOrto
	{
		/// <summary>
		/// Mite der Fläce befindet sic auf Mite der Fläce des durc NaacGbsAstPfaad besctimte GBS Ast.
		/// </summary>
		[JsonProperty]
		readonly public OrtogoonInt FläceTailSctaatisc;

		public InProcessGbsFläceRectekOrto()
		{
		}

		public InProcessGbsFläceRectekOrto(
			OrtogoonInt FläceTailSctaatisc)
		{
			this.FläceTailSctaatisc = FläceTailSctaatisc;
		}

		virtual	public InProcessGbsFläceRectekOrto Versezt(Vektor2DInt Versaz)
		{
			var	FläceTailSctaatisc	= this.FläceTailSctaatisc;

			//var FläceTailSctaatiscVersezt =
			//	null == FläceTailSctaatisc ? null : FläceTailSctaatisc.Versezt(Versaz);

			var FläceTailSctaatiscVersezt = FläceTailSctaatisc.Versezt(Versaz);

			return new InProcessGbsFläceRectekOrto(FläceTailSctaatiscVersezt);
		}
	}

	public class SictVorsclaagNaacProcessWirkung : ObjektMitIdentInt64
	{
		public Int64? NuzerZaitMili;

		/// <summary>
		/// Menge der vorgesclaagene Wirkung von welce der Versuuc der Ausfüürung abhängig gemact werde sol.
		/// </summary>
		public SictVorsclaagWirkungAbhängigkaitVonWirkungErgeebnis[] BedingungMengeWirkungErfolgraic;

		/// <summary>
		/// Dauer welce zwisce lezte VonZiilProcesLeese Begin Zait und nääxte VonZiilProcesLeese Begin Zait liige sol.
		/// </summary>
		public Int64? VonVonZiilProcessLeeseBisVonZiilProcessLeeseWartezaitMili;

		/// <summary>
		/// Dauer welce naac Ausfüürung der Wirkung und vor folgende VonZiilProcesLeese Begin Zait liige sol.
		/// </summary>
		public Int64? VonWirkungBisVonZiilProcessLeeseWartezaitMili;

		public string[] WirkungZwekListeKomponente;

		public SictNaacProcessWirkungKey[] MengeWirkungKey;

		public string AingaabeText;

		/// <summary>
		/// Di endgültige Laage der Fläce ergiibt sic ersct baim Nuzer da diiser di Laage der referenziirte GBS Ast noi berecnet für deen Fal das deren Laage sait
		/// der zum Server gesendete Mesung geändert wurde.
		/// 
		/// Ablauf:
		/// Maus werd zu Element[0] aus Pfaad beweegt.
		/// Maus Taste werd DOWNed fals MausPfaadTasteLinksAin oder MausPfaadTasteRectsAin.
		/// Maus werd über Punkte in Fläce aus andere Elemente beweegt.
		/// Maus Taste werd geUPed.
		/// 
		/// </summary>
		public InProcessGbsFläceRectekOrto[] MausPfaadListeWeegpunktFläce;

		/// <summary>
		/// Menge der Fläce welce bai Auswaal der Weegpunkte des MausPfaad zu maide sind.
		/// 
		/// Fals aine der hiir bescriibene Fläce volsctändig aine der MausPfaadListeWeegpunktFläce verdekt werd der Mauspfaad nit ausgefüürt und als feelgesclaage gemeldet.
		/// </summary>
		public InProcessGbsFläceRectekOrto[] MausPfaadMengeFläceZuMaide;

		/// <summary>
		/// ob Taste Links zwisce Begin und Ende des Maus Pfaad gedrükt sain sol.
		/// </summary>
		public bool? MausPfaadTasteLinksAin;

		/// <summary>
		/// ob Taste Rects zwisce Begin und Ende des Maus Pfaad gedrükt sain sol.
		/// </summary>
		public bool? MausPfaadTasteRectsAin;

		override public string ToString()
		{
			return JsonConvert.SerializeObject(this, Formatting.Indented);
		}

		public SictVorsclaagNaacProcessWirkung()
		{
		}

		public SictVorsclaagNaacProcessWirkung(
			SictVorsclaagNaacProcessWirkung ZuKopiire)
			:
			this(
			(null == ZuKopiire) ? (Int64?)null : ZuKopiire.Ident,
			(null == ZuKopiire) ? null : ZuKopiire.NuzerZaitMili,
			(null == ZuKopiire) ? null : ZuKopiire.BedingungMengeWirkungErfolgraic,
			(null == ZuKopiire) ? null : ZuKopiire.VonVonZiilProcessLeeseBisVonZiilProcessLeeseWartezaitMili,
			(null == ZuKopiire) ? null : ZuKopiire.VonWirkungBisVonZiilProcessLeeseWartezaitMili,
			(null == ZuKopiire) ? null : ZuKopiire.WirkungZwekListeKomponente,
			(null == ZuKopiire) ? null : ZuKopiire.MengeWirkungKey,
			(null == ZuKopiire) ? null : ZuKopiire.MausPfaadListeWeegpunktFläce,
			(null == ZuKopiire) ? null : ZuKopiire.MausPfaadMengeFläceZuMaide,
			(null == ZuKopiire) ? null : ZuKopiire.MausPfaadTasteLinksAin,
			(null == ZuKopiire) ? null : ZuKopiire.MausPfaadTasteRectsAin,
			(null == ZuKopiire) ? null : ZuKopiire.AingaabeText)
		{
		}

		public SictVorsclaagNaacProcessWirkung(
			Int64?	Ident,
			Int64? NuzerZaitMili	= null,
			SictVorsclaagWirkungAbhängigkaitVonWirkungErgeebnis[] BedingungMengeWirkungErfolgraic	= null,
			Int64? VonVonZiilProcesLeeseBisVonZiilProcesLeeseWartezaitMili	=	null,
			Int64? VonWirkungBisVonZiilProcesLeeseWartezaitMili	=	null,
			string[] WirkungZwekListeKomponente	=	null,
			SictNaacProcessWirkungKey[] MengeWirkungKey	=	null,
			InProcessGbsFläceRectekOrto[] MausPfaadListeWeegpunktFläce = null,
			InProcessGbsFläceRectekOrto[] MausPfaadMengeFläceZuMaide = null,
			bool? MausPfaadTasteLinksAin	=	null,
			bool? MausPfaadTasteRectsAin	=	null,
			string AingaabeText = null)
			:
			base(Ident	?? -1)
		{
			this.NuzerZaitMili = NuzerZaitMili;

			this.BedingungMengeWirkungErfolgraic = BedingungMengeWirkungErfolgraic;
			this.VonVonZiilProcessLeeseBisVonZiilProcessLeeseWartezaitMili = VonVonZiilProcesLeeseBisVonZiilProcesLeeseWartezaitMili;
			this.VonWirkungBisVonZiilProcessLeeseWartezaitMili = VonWirkungBisVonZiilProcesLeeseWartezaitMili;

			this.WirkungZwekListeKomponente = WirkungZwekListeKomponente;

			this.MengeWirkungKey = MengeWirkungKey;

			this.MausPfaadListeWeegpunktFläce = MausPfaadListeWeegpunktFläce;
			this.MausPfaadMengeFläceZuMaide = MausPfaadMengeFläceZuMaide;

			this.MausPfaadTasteLinksAin	= MausPfaadTasteLinksAin;
			this.MausPfaadTasteRectsAin	= MausPfaadTasteRectsAin;

			this.AingaabeText = AingaabeText;
		}

		static public SictVorsclaagNaacProcessWirkung VorsclaagWirkungKey(
			System.Windows.Input.Key Key)
		{
			return	VorsclaagWirkungMengeKey(
				new	SictNaacProcessWirkungKey[]{	new	SictNaacProcessWirkungKey((int)Key)});
		}

		static public SictVorsclaagNaacProcessWirkung VorsclaagWirkungMengeKey(
			SictNaacProcessWirkungKey[]	MengeWirkungKey)
		{
			return new SictVorsclaagNaacProcessWirkung(
				null,
				null,
				null,
				null,
				null,
				null,
				MengeWirkungKey);
		}

		static public SictVorsclaagNaacProcessWirkung VorsclaagWirkungMausKlikLinx(
			OrtogoonInt MausZiilFläce,
			InProcessGbsFläceRectekOrto[] MausPfaadMengeFläceZuMaide = null)
		{
			return VorsclaagWirkungMausFläce(
				new InProcessGbsFläceRectekOrto(MausZiilFläce), true, null, MausPfaadMengeFläceZuMaide);
		}

		static public SictVorsclaagNaacProcessWirkung VorsclaagWirkungMausKlikRecz(
			OrtogoonInt MausZiilFläce,
			InProcessGbsFläceRectekOrto[] MausPfaadMengeFläceZuMaide = null)
		{
			return VorsclaagWirkungMausFläce(
				new InProcessGbsFläceRectekOrto(MausZiilFläce), false, true, MausPfaadMengeFläceZuMaide);
		}

		static public SictVorsclaagNaacProcessWirkung VorsclaagWirkungMausFläce(
			InProcessGbsFläceRectekOrto MausZiilFläce,
			bool? MausPfaadTasteLinksAin	= null,
			bool? MausPfaadTasteRectsAin	= null,
			InProcessGbsFläceRectekOrto[] MausPfaadMengeFläceZuMaide = null)
		{
			return VorsclaagWirkungMausPfaad(
				new InProcessGbsFläceRectekOrto[] { MausZiilFläce },
				MausPfaadTasteLinksAin,
				MausPfaadTasteRectsAin,
				MausPfaadMengeFläceZuMaide);
		}

		static public SictVorsclaagNaacProcessWirkung VorsclaagWirkungMausPfaad(
			InProcessGbsFläceRectekOrto[] MausPfaadListeWeegpunktFläce,
			bool? MausPfaadTasteLinksAin,
			bool? MausPfaadTasteRectsAin,
			InProcessGbsFläceRectekOrto[] MausPfaadMengeFläceZuMaide = null)
		{
			return new SictVorsclaagNaacProcessWirkung(
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				MausPfaadListeWeegpunktFläce,
				MausPfaadMengeFläceZuMaide,
				MausPfaadTasteLinksAin,
				MausPfaadTasteRectsAin);
		}

		static public SictVorsclaagNaacProcessWirkung VorsclaagWirkungMouseMove(
			OrtogoonInt MausZiilFläce)
		{
			return VorsclaagWirkungMouseMove(null, MausZiilFläce);
		}

		static public SictVorsclaagNaacProcessWirkung VorsclaagWirkungMouseMove(
			Int64?	Ident,
			OrtogoonInt MausZiilFläce)
		{
			return VorsclaagWirkungMouseMove(Ident, new InProcessGbsFläceRectekOrto(MausZiilFläce));
		}

		static public SictVorsclaagNaacProcessWirkung VorsclaagWirkungMouseMove(
			Int64?	Ident,
			InProcessGbsFläceRectekOrto MausZiilFläce)
		{
			var VorsclaagWirkung = new SictVorsclaagNaacProcessWirkung(Ident);

			VorsclaagWirkung.MausPfaadListeWeegpunktFläce = new InProcessGbsFläceRectekOrto[] { MausZiilFläce };

			return VorsclaagWirkung;
		}

		static public SictVorsclaagNaacProcessWirkung VorsclaagWirkungWarte(
			Int64? VonVonZiilProcesLeeseBisVonZiilProcesLeeseWartezaitMili,
			Int64? VonWirkungBisVonZiilProcesLeeseWartezaitMili,
			string[] WirkungZwekListeKomponente = null)
		{
			var VorsclaagWirkung = new SictVorsclaagNaacProcessWirkung();

			VorsclaagWirkung.VonVonZiilProcessLeeseBisVonZiilProcessLeeseWartezaitMili = VonVonZiilProcesLeeseBisVonZiilProcesLeeseWartezaitMili;
			VorsclaagWirkung.VonWirkungBisVonZiilProcessLeeseWartezaitMili = VonWirkungBisVonZiilProcesLeeseWartezaitMili;
			VorsclaagWirkung.WirkungZwekListeKomponente = WirkungZwekListeKomponente;

			return VorsclaagWirkung;
		}

		static public SictVorsclaagNaacProcessWirkung VorsclaagWirkungWarte(
			Int64? VonVonZiilProcesLeeseBisVonZiilProcesLeeseWartezaitMili,
			Int64? VonWirkungBisVonZiilProcesLeeseWartezaitMili,
			string WirkungZwek	= null)
		{
			return VorsclaagWirkungWarte(VonVonZiilProcesLeeseBisVonZiilProcesLeeseWartezaitMili, VonWirkungBisVonZiilProcesLeeseWartezaitMili, new string[] { WirkungZwek });
		}

		static public SictVorsclaagNaacProcessWirkung VorsclaagWirkungNaacScnapscusWarte(
			Int64 VonVonZiilProcesLeeseBisVonZiilProcesLeeseWartezaitMili,
			string WirkungZwek = null)
		{
			return VorsclaagWirkungWarte(VonVonZiilProcesLeeseBisVonZiilProcesLeeseWartezaitMili,	null, new string[] { WirkungZwek });
		}

		static public SictVorsclaagNaacProcessWirkung VorsclaagWirkungNaacWirkungWarte(
			Int64 VonWirkungBisVonZiilProcesLeeseWartezaitMili,
			string WirkungZwek = null)
		{
			return VorsclaagWirkungWarte(null, VonWirkungBisVonZiilProcesLeeseWartezaitMili, new string[] { WirkungZwek });
		}
	}
}
