using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExtractFromOldAssembly.Bib3
{
	static public class Geometrik
	{
		static public bool ScnaidendGeraadeSegmentMitGeraadeSegment(
			Vektor2DDouble GeraadeSegment0Begin,
			Vektor2DDouble GeraadeSegment0Ende,
			Vektor2DDouble GeraadeSegment1Begin,
			Vektor2DDouble GeraadeSegment1Ende)
		{
			bool Paralel;
			bool Kolinear;
			bool Überlapend;

			if (ScnitpunktGeraadeSegmentMitGeraadeSegment(
				GeraadeSegment0Begin,
				GeraadeSegment0Ende,
				GeraadeSegment1Begin,
				GeraadeSegment1Ende,
				out	Paralel,
				out	Kolinear,
				out	Überlapend).HasValue)
			{
				return true;
			}

			if (Überlapend)
			{
				return true;
			}

			return false;
		}

		/// <summary>
		/// http://stackoverflow.com/questions/563198/how-do-you-detect-where-two-line-segments-intersect
		/// </summary>
		/// <param name="GeraadeSegment0Begin"></param>
		/// <param name="GeraadeSegment0Ende"></param>
		/// <param name="GeraadeSegment1Begin"></param>
		/// <param name="GeraadeSegment1Ende"></param>
		/// <param name="Paralel"></param>
		/// <returns></returns>
		static public Vektor2DDouble? ScnitpunktGeraadeSegmentMitGeraadeSegment(
			Vektor2DDouble GeraadeSegment0Begin,
			Vektor2DDouble GeraadeSegment0Ende,
			Vektor2DDouble GeraadeSegment1Begin,
			Vektor2DDouble GeraadeSegment1Ende,
			out	bool Paralel,
			out	bool Kolinear,
			out	bool Überlapend)
		{
			Kolinear = false;
			Überlapend = false;

			//	Suppose the two line segments run from p to p + r and from q to q + s
			var Segment0Vektor = GeraadeSegment0Ende - GeraadeSegment0Begin;
			var Segment1Vektor = GeraadeSegment1Ende - GeraadeSegment1Begin;

			var VektorKroizprodukt =
				Vektor2DDouble.Kroizprodukt(Segment0Vektor, Segment1Vektor);

			//	 Then any point on the first line is representable as p + t r (for a scalar parameter t)
			//	and any point on the second line as q + u s (for a scalar parameter u).

			//	t = (q − p) × s / (r × s)
			var ScnitpunktAufSegment0Antail =
				Vektor2DDouble.Kroizprodukt((GeraadeSegment1Begin - GeraadeSegment0Begin), Segment1Vektor) /
				VektorKroizprodukt;

			//	u = (q − p) × r / (r × s)
			var ScnitpunktAufSegment1Antail =
				Vektor2DDouble.Kroizprodukt((GeraadeSegment1Begin - GeraadeSegment0Begin), Segment0Vektor) /
				VektorKroizprodukt;

			if (0 == VektorKroizprodukt)
			{
				Paralel = true;

				if (0 == Vektor2DDouble.Kroizprodukt((GeraadeSegment1Begin - GeraadeSegment0Begin), Segment0Vektor))
				{
					//	1.If r × s = 0 and (q − p) × r = 0, then the two lines are collinear.
					Kolinear = true;

					var Temp0 = Vektor2DDouble.Skalarprodukt(GeraadeSegment1Begin - GeraadeSegment0Begin, Segment0Vektor);
					var Temp1 = Vektor2DDouble.Skalarprodukt(GeraadeSegment0Begin - GeraadeSegment1Begin, Segment1Vektor);

					if (
						0 <= Temp0 && Temp0 <= Vektor2DDouble.Skalarprodukt(Segment0Vektor, Segment0Vektor) ||
						0 <= Temp1 && Temp1 <= Vektor2DDouble.Skalarprodukt(Segment1Vektor, Segment1Vektor))
					{
						//	If in addition, either 0 ≤ (q − p) · r ≤ r · r or 0 ≤ (p − q) · s ≤ s · s, then the two lines are overlapping.

						Überlapend = true;
						return null;
					}
				}
			}
			else
			{
				Paralel = false;

				//	4.If r × s ≠ 0 and 0 ≤ t ≤ 1 and 0 ≤ u ≤ 1, the two line segments meet at the point p + t r = q + u s.
				if (0 <= ScnitpunktAufSegment0Antail && ScnitpunktAufSegment0Antail <= 1 &&
					0 <= ScnitpunktAufSegment1Antail && ScnitpunktAufSegment1Antail <= 1)
				{
					return GeraadeSegment0Begin + ScnitpunktAufSegment0Antail * Segment0Vektor;
				}
			}

			return null;
		}

		static public Vektor2DDouble NääxterPunktAufGeraadeSegment(
			Vektor2DDouble GeraadeSegmentBegin,
			Vektor2DDouble GeraadeSegmentEnde,
			Vektor2DDouble SuuceUrscprungPunktLaage)
		{
			double AufGeraadeNääxtePunktLaage;

			return NääxterPunktAufGeraadeSegment(
				GeraadeSegmentBegin,
				GeraadeSegmentEnde,
				SuuceUrscprungPunktLaage,
				out	AufGeraadeNääxtePunktLaage);
		}

		static public Vektor2DDouble NääxterPunktAufGeraadeSegment(
			Vektor2DDouble GeraadeSegmentBegin,
			Vektor2DDouble GeraadeSegmentEnde,
			Vektor2DDouble SuuceUrscprungPunktLaage,
			out	double AufGeraadeNääxtePunktLaage)
		{
			var GeraadeSegmentLängeQuadraat = (GeraadeSegmentEnde - GeraadeSegmentBegin).BetraagQuadriirt;

			if (GeraadeSegmentLängeQuadraat <= 0)
			{
				if (SuuceUrscprungPunktLaage == GeraadeSegmentBegin)
				{
					AufGeraadeNääxtePunktLaage = 0;
				}
				else
				{
					AufGeraadeNääxtePunktLaage = double.PositiveInfinity;
				}

				return GeraadeSegmentBegin;
			}

			AufGeraadeNääxtePunktLaage =
				Vektor2DDouble.Skalarprodukt(
				SuuceUrscprungPunktLaage - GeraadeSegmentBegin,
				GeraadeSegmentEnde - GeraadeSegmentBegin) /
				GeraadeSegmentLängeQuadraat;

			if (AufGeraadeNääxtePunktLaage < 0)
			{
				return GeraadeSegmentBegin;
			}

			if (1 < AufGeraadeNääxtePunktLaage)
			{
				return GeraadeSegmentEnde;
			}

			return
				GeraadeSegmentBegin +
				AufGeraadeNääxtePunktLaage * (GeraadeSegmentEnde - GeraadeSegmentBegin);
		}

		static public Vektor2DDouble NääxterPunktAufGeraade(
			Vektor2DDouble GeradeRichtung,
			Vektor2DDouble Punkt)
		{
			GeradeRichtung.Normalisiire();

			var PositionAufGerade = Punkt.A * GeradeRichtung.A + Punkt.B * GeradeRichtung.B;

			return new Vektor2DDouble(GeradeRichtung.A * PositionAufGerade, GeradeRichtung.B * PositionAufGerade);
		}

		static public Vektor2DDouble NääxterPunktAufGeraade(
			Vektor2DDouble GeradeRichtung,
			Vektor2DDouble Punkt,
			Vektor2DDouble GeradeVersatz)
		{
			return NääxterPunktAufGeraade(GeradeRichtung, Punkt - GeradeVersatz) + GeradeVersatz;
		}

		static public double DistanzVonPunktZuGeraade(
			Vektor2DDouble GeradeRichtung,
			Vektor2DDouble Punkt)
		{
			return (NääxterPunktAufGeraade(GeradeRichtung, Punkt) - Punkt).Betraag;
		}

		static public double DistanzVonPunktZuGeraadeSegment(
			Vektor2DDouble GeraadeSegmentBegin,
			Vektor2DDouble GeraadeSegmentEnde,
			Vektor2DDouble Punkt)
		{
			var AufGeraadeSegmentNääxterPunkt =
				Bib3.Geometrik.NääxterPunktAufGeraadeSegment(GeraadeSegmentBegin, GeraadeSegmentEnde, Punkt);

			return (Punkt - AufGeraadeSegmentNääxterPunkt).Betraag;
		}

		/// <summary>
		/// berecnet aus der Menge der Punkte in der Folge aines Zyyklis wiiderhoolten Punkt den nääxten Punkt zu <paramref name="SuuceUrscprungPunktLaage"/>.
		/// </summary>
		/// <param name="SuuceUrscprungPunktLaage"></param>
		/// <param name="ZyyklusPunktVersaz"></param>
		/// <param name="ZyyklusLänge"></param>
		/// <returns></returns>
		static public double InFolgePunktNääxteBerecne(
			double SuuceUrscprungPunktLaage,
			double ZyyklusPunktLaage,
			double ZyyklusLänge)
		{
			var ZyyklusPunktLaageNääxteZuNul =
				((((((ZyyklusPunktLaage / ZyyklusLänge) + 1) % 1) + 1.5) % 1) - 0.5) * ZyyklusLänge;

			var ZyyklusIndex = (Int64)((SuuceUrscprungPunktLaage - ZyyklusPunktLaageNääxteZuNul) / ZyyklusLänge + 0.5);

			return ZyyklusIndex * ZyyklusLänge + ZyyklusPunktLaageNääxteZuNul;
		}

		static public int[] KonvexeHüleListePunktIndexBerecne(
			Vektor2DDouble[] ListePunkt)
		{
			if (null == ListePunkt)
			{
				return null;
			}

			if (ListePunkt.Length < 4)
			{
				return Enumerable.Range(0, ListePunkt.Length).ToArray();
			}

			var MengePunktMitIndex =
				ListePunkt.Select((Punkt, Index) => new KeyValuePair<int, Vektor2DDouble>(Index, Punkt)).ToArray();

			var BeginPunktMitIndex =
				MengePunktMitIndex.OrderBy((Kandidaat) => Kandidaat.Value.A).FirstOrDefault();

			var HüleMengePunktIndex = new List<int>();

			HüleMengePunktIndex.Add(BeginPunktMitIndex.Key);

			var ZwisceergeebnisLeztePunktIndex = BeginPunktMitIndex.Key;
			var ZwisceergeebnisLeztePunktLaage = BeginPunktMitIndex.Value;
			double ZwisceergeebnisLezteRotatioon = 1.0 / 4;

			while (true)
			{
				var KandidaatNääxtePunktIndex = ZwisceergeebnisLeztePunktIndex;
				double KandidaatNääxteRotatioon = 1;

				for (int KandidaatPunktIndex = 0; KandidaatPunktIndex < ListePunkt.Length; KandidaatPunktIndex++)
				{
					if (KandidaatPunktIndex == ZwisceergeebnisLeztePunktIndex)
					{
						continue;
					}

					var KandidaatPunkt = ListePunkt[KandidaatPunktIndex];

					var KandidaatRotatioon =
						(Rotatioon(KandidaatPunkt - ZwisceergeebnisLeztePunktLaage) -
						ZwisceergeebnisLezteRotatioon + 1) % 1;

					if (KandidaatNääxtePunktIndex == ZwisceergeebnisLeztePunktIndex ||
						KandidaatRotatioon < KandidaatNääxteRotatioon)
					{
						KandidaatNääxteRotatioon = KandidaatRotatioon;
						KandidaatNääxtePunktIndex = KandidaatPunktIndex;
					}
				}

				if (KandidaatNääxtePunktIndex == BeginPunktMitIndex.Key)
				{
					break;
				}

				HüleMengePunktIndex.Add(KandidaatNääxtePunktIndex);

				var TempZwisceergeebnisLeztePunktLaage = ListePunkt[KandidaatNääxtePunktIndex];

				ZwisceergeebnisLeztePunktIndex = KandidaatNääxtePunktIndex;
				ZwisceergeebnisLezteRotatioon = Rotatioon(TempZwisceergeebnisLeztePunktLaage - ZwisceergeebnisLeztePunktLaage);
				ZwisceergeebnisLeztePunktLaage = TempZwisceergeebnisLeztePunktLaage;
			}

			return HüleMengePunktIndex.ToArray();
		}

		/// <summary>
		/// Winkel(0) => (a=1,b=0)
		/// Winkel(1/4) => (a=0,b=1)
		/// Winkel(2/4) => (a=-1,b=0)
		/// Winkel(3/4) => (a=0,b=-1)
		/// </summary>
		/// <param name="Vektor"></param>
		static public double Rotatioon(Vektor2DDouble Vektor)
		{
			Vektor.Normalisiire();

			var Winkel = Math.Acos(Vektor.A) / Math.PI / 2;

			if (Vektor.B < 0)
			{
				Winkel = 1 - Winkel;
			}

			return Winkel;
		}

		static public double Rotatioon(
			Vektor2DDouble Vektor0,
			Vektor2DDouble Vektor1)
		{
			var Richtung0 = Vektor0.Normalisiirt();
			var Richtung1 = Vektor1.Normalisiirt();

			var Punktprodukt = Math.Min(1, Math.Max(-1, Vektor2DDouble.Skalarprodukt(Richtung0, Richtung1)));

			var Rotatioon = Math.Acos(Punktprodukt) / Math.PI / 2;

			return Rotatioon;
		}

		/// <summary>
		/// Links &lt; 0;
		/// Rechts &gt; 0;
		/// </summary>
		/// <param name="GeraadeRictung"></param>
		/// <param name="Punkt"></param>
		/// <returns></returns>
		static public int SaiteVonGeraadeZuPunkt(
			Vektor2DDouble GeraadeRictung,
			Vektor2DDouble Punkt)
		{
			return Math.Sign(Vektor2DDouble.Skalarprodukt(Punkt, new Vektor2DDouble(-GeraadeRictung.B, GeraadeRictung.A)));
		}

		static bool PunktLiigtInRegioon1D(
			Int64 RegioonMin,
			Int64 RegioonMax,
			Int64 Punkt)
		{
			return (RegioonMin <= Punkt && Punkt <= RegioonMax);
		}

		static Int64[] ListeGrenzeAusÜberscnaidung1D(
			Int64 RegioonAScrankeMin,
			Int64 RegioonAScrankeMax,
			Int64 RegioonBScrankeMin,
			Int64 RegioonBScrankeMax)
		{
			var RegioonAMinMax = new KeyValuePair<Int64, Int64>(RegioonAScrankeMin, RegioonAScrankeMax);
			var RegioonBMinMax = new KeyValuePair<Int64, Int64>(RegioonBScrankeMin, RegioonBScrankeMax);

			var MengeKandidaat = new KeyValuePair<Int64, KeyValuePair<Int64, Int64>>[]{
				new	KeyValuePair<Int64,	KeyValuePair<Int64,	Int64>>(RegioonAScrankeMin,RegioonBMinMax),
				new	KeyValuePair<Int64,	KeyValuePair<Int64,	Int64>>(RegioonAScrankeMax,RegioonBMinMax),
				new	KeyValuePair<Int64,	KeyValuePair<Int64,	Int64>>(RegioonBScrankeMin,RegioonAMinMax),
				new	KeyValuePair<Int64,	KeyValuePair<Int64,	Int64>>(RegioonBScrankeMax,RegioonAMinMax),};

			var ListeGrenzePunkt =
				MengeKandidaat
				//	.Where((Kandidaat) => PunktLiigtInRegioon1D(Kandidaat.Value.Key, Kandidaat.Value.Value, Kandidaat.Key))
				.Where(Kandidaat => Kandidaat.Value.Key <= Kandidaat.Key && Kandidaat.Key <= Kandidaat.Value.Value)
				.Select((Kandidaat) => Kandidaat.Key)
				.ToArray();

			var ListeGrenzePunktDistinct =
				ListeGrenzePunkt.Distinct().ToArray();

			return ListeGrenzePunktDistinct;
		}

		static public IEnumerable<OrtogoonInt> Diferenz(
			this OrtogoonInt Minuend,
			OrtogoonInt Subtrahend)
		{
			if (null == Minuend)
			{
				yield break;
			}

			if (null == Subtrahend)
			{
				yield return Minuend;
				yield break;
			}

			var MinuendMinMax = new KeyValuePair<Vektor2DInt, Vektor2DInt>(Minuend.PunktMin, Minuend.PunktMax);
			var SubtrahendMinMax = new KeyValuePair<Vektor2DInt, Vektor2DInt>(Subtrahend.PunktMin, Subtrahend.PunktMax);

			if (MinuendMinMax.Value.A <= SubtrahendMinMax.Key.A ||
				MinuendMinMax.Value.B <= SubtrahendMinMax.Key.B ||
				SubtrahendMinMax.Value.A <= MinuendMinMax.Key.A ||
				SubtrahendMinMax.Value.B <= MinuendMinMax.Key.B)
			{
				//	Scpez Fal kaine Scnitmenge
				yield return Minuend;
				yield break;
			}

			if (MinuendMinMax.Value.A <= SubtrahendMinMax.Value.A &&
				MinuendMinMax.Value.B <= SubtrahendMinMax.Value.B &&
				SubtrahendMinMax.Key.A <= MinuendMinMax.Key.A &&
				SubtrahendMinMax.Key.B <= MinuendMinMax.Key.B)
			{
				//	Scpez Fal Minuend liigt volsctändig in Subtrahend
				yield break;
			}

			Int64[] RictungAMengeScranke =
				ListeGrenzeAusÜberscnaidung1D(
				MinuendMinMax.Key.A,
				MinuendMinMax.Value.A,
				SubtrahendMinMax.Key.A,
				SubtrahendMinMax.Value.A)
				.OrderBy((t) => t)
				.ToArray();

			Int64[] RictungBMengeScranke =
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
				yield return Minuend;
				yield break;
			}

			var RictungAMengeScrankeMitMinuendGrenze =
				new Int64[] { MinuendMinMax.Key.A }.Concat(RictungAMengeScranke).Concat(new Int64[] { MinuendMinMax.Value.A }).ToArray();

			for (int RictungAScrankeIndex = 0; RictungAScrankeIndex < RictungAMengeScrankeMitMinuendGrenze.Length - 1; RictungAScrankeIndex++)
			{
				var RictungAScrankeMinLaage = RictungAMengeScrankeMitMinuendGrenze[RictungAScrankeIndex];

				var RictungAScrankeMaxLaage = RictungAMengeScrankeMitMinuendGrenze[RictungAScrankeIndex + 1];

				if (SubtrahendMinMax.Value.A <= RictungAScrankeMinLaage ||
					RictungAScrankeMaxLaage <= SubtrahendMinMax.Key.A)
				{
					//	in RictungB unbescrankter Abscnit
					yield return new OrtogoonInt(RictungAScrankeMinLaage, RictungAScrankeMaxLaage, MinuendMinMax.Key.B, MinuendMinMax.Value.B);
				}
				else
				{
					var RictungBMengeScrankeFrüheste = RictungBMengeScranke.First();
					var RictungBMengeScrankeLezte = RictungBMengeScranke.Last();

					if (MinuendMinMax.Key.B < SubtrahendMinMax.Key.B)
					{
						yield return new OrtogoonInt(RictungAScrankeMinLaage, RictungAScrankeMaxLaage, MinuendMinMax.Key.B, SubtrahendMinMax.Key.B);
					}

					if (SubtrahendMinMax.Value.B < MinuendMinMax.Value.B)
					{
						yield return new OrtogoonInt(RictungAScrankeMinLaage, RictungAScrankeMaxLaage, SubtrahendMinMax.Value.B, MinuendMinMax.Value.B);
					}
				}
			}
		}

	}
}
