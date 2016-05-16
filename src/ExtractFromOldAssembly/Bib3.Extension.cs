using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Collections;
using System.IO.Compression;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Concurrent;
using Bib3;

namespace ExtractFromOldAssembly.Bib3
{
	static public class Extension
	{
		static public IOrderedEnumerable<T> OrderByNullable<T, OrdnungT>(
			this IEnumerable<T> Enumerable,
			Func<T, OrdnungT> FunkOrdnung)
		{
			if (null == Enumerable)
			{
				return null;
			}

			return Enumerable.OrderBy(FunkOrdnung);
		}

		static public IOrderedEnumerable<T> OrderByDescendingNullable<T, OrdnungT>(
			this IEnumerable<T> Enumerable,
			Func<T, OrdnungT> FunkOrdnung)
		{
			if (null == Enumerable)
			{
				return null;
			}

			return Enumerable.OrderByDescending(FunkOrdnung);
		}

		static public IOrderedEnumerable<T> ThenByNullable<T, OrdnungT>(
			this IOrderedEnumerable<T> Enumerable,
			Func<T, OrdnungT> FunkOrdnung)
		{
			if (null == Enumerable)
			{
				return null;
			}

			return Enumerable.ThenBy(FunkOrdnung);
		}

		static public IOrderedEnumerable<T> ThenByDescendingNullable<T, OrdnungT>(
			this IOrderedEnumerable<T> Enumerable,
			Func<T, OrdnungT> FunkOrdnung)
		{
			if (null == Enumerable)
			{
				return null;
			}

			return Enumerable.ThenByDescending(FunkOrdnung);
		}

		static public bool? AnyNullable<T>(
			this IEnumerable<T> Enumerable,
			Func<T, bool> Prädikaat)
		{
			if (null == Enumerable)
			{
				return null;
			}

			if (null == Prädikaat)
			{
				return null;
			}

			return Enumerable.Any(Prädikaat);
		}

		static public bool? AllHaveValueNullable<T>(
			this IEnumerable<Nullable<T>> Enumerable)
			where T : struct
		{
			if (null == Enumerable)
			{
				return null;
			}

			return Enumerable.All((Element) => Element.HasValue);
		}

		static public bool? AllNullable<T>(
			this IEnumerable<T> Enumerable,
			Func<T, bool> Prädikaat)
		{
			if (null == Enumerable)
			{
				return null;
			}

			if (null == Prädikaat)
			{
				return null;
			}

			return Enumerable.All(Prädikaat);
		}

		static public T[] ToArrayNullable<T>(
			this IEnumerable<T> Enumerable)
		{
			if (null == Enumerable)
			{
				return null;
			}

			return Enumerable.ToArray();
		}

		static public T[] ToArrayFalsNitLeer<T>(
			this IEnumerable<T> Enumerable)
		{
			if (null == Enumerable)
			{
				return null;
			}

			if (Enumerable.Count() < 1)
			{
				return null;
			}

			var Array = Enumerable.ToArray();

			if (Array.Length < 1)
			{
				return null;
			}

			return Array;
		}

		static public void ForEach<T>(
			this IEnumerable<T> Menge,
			Action<T, int> Aktioon)
		{
			if (null == Menge)
			{
				return;
			}

			if (null == Aktioon)
			{
				return;
			}

			int ElementIndex = 0;

			foreach (var item in Menge)
			{
				Aktioon(item, ElementIndex);

				++ElementIndex;
			}
		}

		static public void ForEach<T>(
			this IEnumerable<T> Menge,
			Action<T> Aktioon)
		{
			if (null == Menge)
			{
				return;
			}

			if (null == Aktioon)
			{
				return;
			}

			foreach (var item in Menge)
			{
				Aktioon(item);
			}
		}

		static public void ForEachNullable<T>(
			this IEnumerable<T> Menge,
			Action<T, int> Aktioon) => ForEach(Menge, Aktioon);

		static public void ForEachNullable<T>(
			this IEnumerable<T> Menge,
			Action<T> Aktioon) => ForEach(Menge, Aktioon);

		static public bool ContainsNullable<T>(
			this IEnumerable<T> Menge,
			T Value)
		{
			if (null == Menge)
			{
				return false;
			}

			return Menge.Contains(Value);
		}

		static public bool ContainsNullable<T>(
			this IEnumerable<T> Menge,
			T Value,
			IEqualityComparer<T> Comparer)
		{
			if (null == Menge)
			{
				return false;
			}

			return Menge.Contains(Value, Comparer);
		}

		static public IEnumerable<T> WhereNullable<T>(
			this IEnumerable<T> Menge,
			Func<T, bool> Prädikaat)
		{
			if (null == Menge)
			{
				return null;
			}

			return Menge.Where(Prädikaat);
		}

		static public IEnumerable<T> WhereNullable<T>(
			this IEnumerable<T> Menge,
			Func<T, int, bool> Prädikaat)
		{
			if (null == Menge)
			{
				return null;
			}

			return Menge.Where(Prädikaat);
		}

		static public IEnumerable<TAus> SelectNullable<TAin, TAus>(
			this IEnumerable<TAin> Menge,
			Func<TAin, TAus> Sict)
		{
			if (null == Menge)
			{
				return null;
			}

			return Menge.Select(Sict);
		}

		static public IEnumerable<T> TakeWhileNullable<T>(
			this IEnumerable<T> Menge,
			Func<T, bool> Prädikaat)
		{
			if (null == Menge)
			{
				return null;
			}

			return Menge.TakeWhile(Prädikaat);
		}

		static public IEnumerable<T> TakeWhileNullable<T>(
			this IEnumerable<T> Menge,
			Func<T, int, bool> Prädikaat)
		{
			if (null == Menge)
			{
				return null;
			}

			return Menge.TakeWhile(Prädikaat);
		}

		static public IEnumerable<T> TakeNullable<T>(
			this IEnumerable<T> Menge,
			int count)
		{
			if (null == Menge)
			{
				return null;
			}

			return Menge.Take(count);
		}

		static public IEnumerable<T> SkipWhileNullable<T>(
			this IEnumerable<T> Menge,
			Func<T, bool> Prädikaat)
		{
			if (null == Menge)
			{
				return null;
			}

			return Menge.SkipWhile(Prädikaat);
		}

		static public IEnumerable<T> SkipWhileNullable<T>(
			this IEnumerable<T> Menge,
			Func<T, int, bool> Prädikaat)
		{
			if (null == Menge)
			{
				return null;
			}

			return Menge.SkipWhile(Prädikaat);
		}

		static public IEnumerable<T> SkipNullable<T>(
			this IEnumerable<T> Menge,
			int count)
		{
			if (null == Menge)
			{
				return null;
			}

			return Menge.Skip(count);
		}

		static public T ElementAtOrDefaultNullable<T>(
			this IEnumerable<T> Menge,
			int index)
		{
			if (null == Menge)
			{
				return default(T);
			}

			return Menge.ElementAtOrDefault(index);
		}

		static public Int64? SumNullable(
			this IEnumerable<int> Menge)
		{
			if (null == Menge)
			{
				return null;
			}

			return Menge.Sum();
		}

		static public Int64? SumNullable(
			this IEnumerable<Int64> Menge)
		{
			if (null == Menge)
			{
				return null;
			}

			return Menge.Sum();
		}

		static public double? SumNullable(
			this IEnumerable<double> Menge)
		{
			if (null == Menge)
			{
				return null;
			}

			return Menge.Sum();
		}

		static public float? SumNullable(
			this IEnumerable<float> Menge)
		{
			if (null == Menge)
			{
				return null;
			}

			return Menge.Sum();
		}

		static public Int64? MinNullable(
			this IEnumerable<Int64> Menge)
		{
			if (Menge.NullOderLeer())
			{
				return null;
			}

			return Menge.Min();
		}

		static public int? MinNullable(
			this IEnumerable<int> Menge)
		{
			if (Menge.NullOderLeer())
			{
				return null;
			}

			return Menge.Min();
		}

		static public Int64? MaxNullable(
			this IEnumerable<Int64> Menge)
		{
			if (Menge.NullOderLeer())
			{
				return null;
			}

			return Menge.Max();
		}

		static public int? MaxNullable(
			this IEnumerable<int> Menge)
		{
			if (Menge.NullOderLeer())
			{
				return null;
			}

			return Menge.Max();
		}

		public delegate TAus SelectWhereFuncMitIndex<in TAin, out TAus>(TAin Ain, int Index, out bool Inkludiire);
		public delegate TAus SelectWhereFunc<in TAin, out TAus>(TAin Ain, out bool Inkludiire);

		static public IEnumerable<TAus> SelectWhereNullable<TAin, TAus>(
			this IEnumerable<TAin> Menge,
			SelectWhereFunc<TAin, TAus> Sict)
		{
			return
				SelectWhereNullable(
				Menge,
				(TAin Ain, int Index, out bool Inkludiire) => Sict(Ain, out Inkludiire));
		}

		static public IEnumerable<TAus> SelectWhereNullable<TAin, TAus>(
			this IEnumerable<TAin> Menge,
			SelectWhereFuncMitIndex<TAin, TAus> Sict)
		{
			if (null == Menge)
			{
				yield break;
			}

			int Index = 0;

			foreach (var Element in Menge)
			{
				bool Inkludiire;

				var ElementAbbild = Sict(Element, Index, out Inkludiire);

				++Index;

				if (!Inkludiire)
				{
					continue;
				}

				yield return ElementAbbild;
			}
		}

		static public IEnumerable<TAus> SelectWhereNullable<TAin, TAus>(
			this IEnumerable<TAin> Menge,
			Func<TAin, int, KeyValuePair<TAus, bool>> Sict)
		{
			if (null == Menge)
			{
				return null;
			}

			return
				Menge
				.Select((Element, Index) => Sict(Element, Index))
				.Where((Element) => Element.Value)
				.Select((Element) => Element.Key);
		}

		static public IEnumerable<T> ConcatNullable<T>(
			IEnumerable<IEnumerable<T>> Liste)
		{
			if (null == Liste)
			{
				yield break;
			}

			foreach (var item in Liste)
			{
				if (null == item)
				{
					continue;
				}

				foreach (var item1 in item)
				{
					yield return item1;
				}
			}
		}

		static public IEnumerable<T> ConcatNullable<T>(
			this IEnumerable<T> Liste0,
			IEnumerable<T> Liste1)
		{
			if (null == Liste0)
			{
				return Liste1;
			}

			if (null == Liste1)
			{
				return Liste0;
			}

			return Liste0.Concat(Liste1);
		}

		static public IEnumerable<TAus> SelectNullable<TAin, TAus>(
			this IEnumerable<TAin> Menge,
			Func<TAin, int, TAus> Sict)
		{
			if (null == Menge)
			{
				return null;
			}

			return Menge.Select(Sict);
		}

		static public IEnumerable<TAus> OfTypeNullable<TAus>(
			this IEnumerable Menge)
		{
			if (null == Menge)
			{
				return null;
			}

			return Menge.OfType<TAus>();
		}

		static public IEnumerable<TAus> IntersectNullable<TAus>(
			this IEnumerable<TAus> O0,
			IEnumerable<TAus> O1)
		{
			if (object.ReferenceEquals(O0, O1))
			{
				return O0;
			}

			if (null == O0 || null == O1)
			{
				return null;
			}

			return O0.Intersect(O1);
		}

		static public TAus FirstOrDefaultNullable<TAus>(
			this IEnumerable<TAus> Menge,
			Func<TAus, bool> Prädikaat)
		{
			if (null == Menge || null == Prädikaat)
			{
				return default(TAus);
			}

			return Menge.FirstOrDefault(Prädikaat);
		}

		static public TAus FirstOrDefaultNullable<TAus>(
			this IEnumerable<TAus> Menge)
		{
			if (null == Menge)
			{
				return default(TAus);
			}

			return Menge.FirstOrDefault();
		}

		static public TAus LastOrDefaultNullable<TAus>(
			this IEnumerable<TAus> Menge,
			Func<TAus, bool> Prädikaat)
		{
			if (null == Menge || null == Prädikaat)
			{
				return default(TAus);
			}

			return Menge.LastOrDefault(Prädikaat);
		}

		static public TAus LastOrDefaultNullable<TAus>(
			this IEnumerable<TAus> Menge)
		{
			if (null == Menge)
			{
				return default(TAus);
			}

			return Menge.LastOrDefault();
		}

		static public IList<T> ToListNullable<T>(
			this IEnumerable<T> Enumerable)
		{
			if (null == Enumerable)
			{
				return null;
			}

			return Enumerable.ToList();
		}

		static public void ListeKürzeBegin<T>(
			this Queue<T> Liste, int? ItemAnzaalScrankeMax)
		{
			if (null == Liste)
			{
				return;
			}

			var ZuEntferneAnzaal = Liste.Count - ItemAnzaalScrankeMax;

			if (ZuEntferneAnzaal.HasValue)
			{
				for (int i = 0; i < ZuEntferneAnzaal.Value; i++)
				{
					Liste.Dequeue();
				}
			}
		}

		static public void ListeKürzeBegin<T>(
			this ConcurrentQueue<T> Liste,
			int? ItemAnzaalScrankeMax)
		{
			if (null == Liste)
			{
				return;
			}

			while (ItemAnzaalScrankeMax < Liste?.Count)
			{
				Liste.TryDequeueOrDefault();
			}
		}

		/// <summary>
		/// Kürzt di Liste auf <paramref name="ItemAnzaalScrankeMax"/>.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="Liste"></param>
		/// <param name="ItemAnzaalScrankeMax"></param>
		static public void ListeKürzeBegin<T>(
			this IList<T> Liste, int? ItemAnzaalScrankeMax)
		{
			if (null == Liste)
			{
				return;
			}

			var ListeScpez = Liste as List<T>;

			var ZuEntferneAnzaal = Liste.Count - ItemAnzaalScrankeMax;

			if (ZuEntferneAnzaal.HasValue)
			{
				if (0 < ZuEntferneAnzaal)
				{
					if (null != ListeScpez)
					{
						ListeScpez.RemoveRange(0, ZuEntferneAnzaal.Value);
					}
					else
					{
						for (int i = 0; i < ZuEntferneAnzaal.Value; i++)
						{
							Liste.RemoveAt(0);
						}
					}
				}
			}
		}

		static public void ListeKürzeBegin<T>(
			this IList<T> Liste,
			Func<T, bool> Prädikaat)
		{
			if (null == Liste)
			{
				return;
			}

			var ListeScpez = Liste as List<T>;

			while (true)
			{
				if (Liste.Count < 1)
				{
					break;
				}

				var Element = Liste[0];

				if (!Prädikaat(Element))
				{
					break;
				}

				Liste.RemoveAt(0);
			}
		}

		static public void ListeKürzeBegin<T>(
			this Queue<T> Liste,
			Func<T, bool> Prädikaat)
		{
			if (null == Liste)
			{
				return;
			}

			var ListeScpez = Liste as List<T>;

			while (true)
			{
				if (Liste.Count < 1)
				{
					break;
				}

				var Element = Liste.Peek();

				if (!Prädikaat(Element))
				{
					break;
				}

				Liste.Dequeue();
			}
		}

		static public bool NullOderLeer(
			IEnumerable Enumerable)
		{
			if (null == Enumerable)
			{
				return true;
			}

			return !Enumerable.GetEnumerator().MoveNext();

			if (!Enumerable.GetEnumerator().MoveNext())
			{
				return true;
			}

			return false;
		}

		static public Int64? CountNullable<T>(
			this IEnumerable<T> Enumerable)
		{
			if (null == Enumerable)
			{
				return null;
			}

			return Enumerable.Count();
		}

		static public Int64? CountNullable<T>(
			this IEnumerable<T> Enumerable,
			Func<T, bool> Prädikaat)
		{
			if (null == Enumerable)
			{
				return null;
			}

			return Enumerable.Count(Prädikaat);
		}

		static public IEnumerable<T> ExceptNullable<T>(
			this IEnumerable<T> Enumerable,
			IEnumerable<T> second)
		{
			if (null == Enumerable)
			{
				return null;
			}

			if (null == second)
			{
				return Enumerable;
			}

			return Enumerable.Except(second);
		}

		static public T AggregateNullable<T>(
			this IEnumerable<T> Enumerable,
			T seed,
			Func<T, T, T> func)
		{
			if (null == Enumerable)
			{
				return default(T);
			}

			return Enumerable.Aggregate(seed, func);
		}

		static public T AggregateNullable<T>(
			this IEnumerable<T> Enumerable,
			Func<T, T, T> func)
		{
			if (null == Enumerable)
			{
				return default(T);
			}

			return Enumerable.Aggregate(func);
		}

		static public IEnumerable<T> BaumEnumFlacListeKnoote<T>(
			this T SuuceWurzel,
			Func<T, IEnumerable<T>> FuncZuKnooteListeKind,
			int? TiifeScrankeMax = null,
			int? TiifeScrankeMin = null)
		{
			var MengeKnooteMitPfaad = BaumEnumFlacListeNaacKnootePfaad(SuuceWurzel, FuncZuKnooteListeKind);

			return
				MengeKnooteMitPfaad
				.SkipWhileNullable((NaacKnootePfaad) =>
					TiifeScrankeMin.HasValue ? (TiifeScrankeMin <= NaacKnootePfaad.CountNullable()) : false)
				.TakeWhileNullable((NaacKnootePfaad) =>
					TiifeScrankeMax.HasValue ? (NaacKnootePfaad.CountNullable() <= TiifeScrankeMax) : true)
				.SelectNullable((NaacKnootePfaad) => NaacKnootePfaad.LastOrDefaultNullable());
		}

		static public IEnumerable<T[]> BaumEnumFlacListeNaacKnootePfaad<T>(
			this T SuuceWurzel,
			Func<T, IEnumerable<T>> FuncZuKnooteListeKind)
		{
			var SclangePfaad = new Queue<T[]>();

			SclangePfaad.Enqueue(new T[] { SuuceWurzel });

			while (0 < SclangePfaad.Count)
			{
				var AstPfaad = SclangePfaad.Dequeue();

				yield return AstPfaad;

				if (null == FuncZuKnooteListeKind)
				{
					continue;
				}

				var Ast = AstPfaad.LastOrDefault();

				var AstListeKind = FuncZuKnooteListeKind(Ast);

				if (null == AstListeKind)
				{
					continue;
				}

				foreach (var AstKind in AstListeKind)
				{
					SclangePfaad.Enqueue(AstPfaad.Concat(new T[] { AstKind }).ToArray());
				}
			}
		}

		static public Nullable<T> CastToNullable<T>(
			this object Ref)
			where T : struct
		{
			if (Ref is T)
			{
				return (T)Ref;
			}

			return null;
		}

		static public IEnumerable<KeyValuePair<KeyT, ValueT?>> ElementValueCastToNullable<KeyT, ValueT>(
			this IEnumerable<KeyValuePair<KeyT, ValueT>> orig)
			where ValueT : struct =>
			orig?.Select(origElement => new KeyValuePair<KeyT, ValueT?>(origElement.Key, origElement.Value));

		static public decimal ZaitMikroSictDecimal(this Stopwatch Stopwatch)
		{
			return ((decimal)Stopwatch.ElapsedTicks * 1000000) / Stopwatch.Frequency;
		}

		static public Int64 ZaitMikroSictInt(this Stopwatch Stopwatch)
		{
			return (Int64)ZaitMikroSictDecimal(Stopwatch);
		}

		static public void AddRangeNullable<T>(
			this List<T> List,
			IEnumerable<T> O)
		{
			if (null == List)
			{
				return;
			}

			if (null == O)
			{
				return;
			}

			List.AddRange(O);
		}

		static public IEnumerable<byte[]> LeeseMengeBlok(
			this System.IO.Stream Stream,
			int BlokListeOktetAnzaal)
		{
			if (null == Stream)
			{
				yield break;
			}

			var Pufer = new byte[BlokListeOktetAnzaal];

			while (true)
			{
				var AnzaalGeleese = Stream.Read(Pufer, 0, Pufer.Length);

				if (AnzaalGeleese < 1)
				{
					break;
				}

				var Blok = new byte[AnzaalGeleese];

				Buffer.BlockCopy(Pufer, 0, Blok, 0, AnzaalGeleese);

				yield return Blok;
			}
		}

		static public T[] ListeArrayAgregiire<T>(
			this T[][] ArrayArray)
		{
			if (null == ArrayArray)
			{
				return null;
			}

			var ArrayArrayUnglaicNull = ArrayArray.WhereNotDefault().ToArray();

			var ListeElementAnzaal = ArrayArrayUnglaicNull.Select(Array => Array.Length).Sum();

			var Agregatioon = new T[ListeElementAnzaal];

			if (Agregatioon.Length < 1)
			{
				return Agregatioon;
			}

			var ElementType = ArrayArrayUnglaicNull.FirstOrDefaultNullable().GetType().GetElementType();

			var ElementTypeSize = System.Runtime.InteropServices.Marshal.SizeOf(ElementType);

			int ElementIndex = 0;

			for (int i = 0; i < ArrayArrayUnglaicNull.Length; i++)
			{
				var Array = ArrayArray[i];

				if (0 == Array.Length)
				{
					continue;
				}

				if (ElementType.IsPrimitive)
				{
					Buffer.BlockCopy(Array, 0, Agregatioon, ElementTypeSize * ElementIndex, ElementTypeSize * Array.Length);
				}
				else
				{
					System.Array.ConstrainedCopy(Array, 0, Agregatioon, ElementIndex, Array.Length);
				}

				ElementIndex += Array.Length;
			}

			return Agregatioon;
		}

		static public byte[] LeeseGesamt(
			this System.IO.Stream Stream,
			int BlokListeOktetAnzaal = 0x10000)
		{
			if (null == Stream)
			{
				return null;
			}

			var ListeBlok =
				LeeseMengeBlok(Stream, BlokListeOktetAnzaal).ToArray();

			if (ListeBlok.Length <= 1)
			{
				return ListeBlok.FirstOrDefault();
			}

			return ListeArrayAgregiire(ListeBlok);
		}

		static public Array ArraySegment(
			this Array Array, int SegmentBeginElementIndex, int SegmentListeElementAnzaalScrankeMax)
		{
			if (null == Array)
			{
				return null;
			}

			SegmentBeginElementIndex = Math.Max(0, SegmentBeginElementIndex);

			var SegmentListeElementAnzaal = Math.Max(0, Math.Min(SegmentListeElementAnzaalScrankeMax, Array.Length - SegmentBeginElementIndex));

			var SegmentArray = Array.CreateInstance(Array.GetType().GetElementType(), SegmentListeElementAnzaal);

			Array.ConstrainedCopy(Array, SegmentBeginElementIndex, SegmentArray, 0, SegmentListeElementAnzaal);

			return SegmentArray;
		}

		static public byte[] DeflateKompres(
			this byte[] ListeOktet,
			CompressionLevel CompressionLevel = CompressionLevel.Optimal)
		{
			if (null == ListeOktet)
			{
				return null;
			}

			using (var Compressed = new MemoryStream())
			{
				using (var DeflateStream = new DeflateStream(Compressed, CompressionLevel, true))
				{
					DeflateStream.Write(ListeOktet, 0, ListeOktet.Length);
				}

				Compressed.Seek(0, SeekOrigin.Begin);

				return Compressed.ToArray();
			}
		}

		static public byte[] DeflateDeKompres(
			this byte[] ListeOktet,
			Int64 ListeOktetAnzaalScrankeMax)
		{
			if (null == ListeOktet)
			{
				return null;
			}

			using (var CompressedStream = new MemoryStream(ListeOktet))
			{
				using (var DeflateStream = new DeflateStream(CompressedStream, CompressionMode.Decompress))
				{
					var BlokListeOktetAnzaal = 0x10000;

					var ListeBlokAnzaalScrankeMax = (ListeOktetAnzaalScrankeMax - 1) / BlokListeOktetAnzaal + 1;

					var ListeBlok =
						DeflateStream
						.LeeseMengeBlok(BlokListeOktetAnzaal)
						.Take((int)ListeBlokAnzaalScrankeMax)
						.ToArray();

					return ListeBlok.ConcatNullable().ToArray();
				}
			}
		}

		static public void EnqueueSeq<T>(
			this Queue<T> Queue,
			IEnumerable<T> Sequence)
		{
			if (null == Sequence || null == Queue)
			{
				return;
			}

			foreach (var item in Sequence)
			{
				Queue.Enqueue(item);
			}
		}

		static public void Enqueue<T>(
			this Queue<T> Queue,
			IEnumerable<T> Sequence)
		{
			Queue.EnqueueSeq(Sequence);
		}

		static public IEnumerable<T> DequeueEnumerator<T>(
			this Queue<T> Queue,
			Func<T, bool> PeekPredicate = null)
		{
			if (null == Queue)
			{
				yield break;
			}

			while (0 < Queue.Count)
			{
				if (null != PeekPredicate)
				{
					if (!PeekPredicate(Queue.Peek()))
					{
						yield break;
					}
				}

				yield return Queue.Dequeue();
			}
		}

		static public IEnumerable<T> DequeueEnumerator<T>(
			this Queue<T> Queue,
			Func<bool> Predicate)
		{
			var PeekPredicate = null == Predicate ? null : new Func<T, bool>((t) => Predicate());

			return DequeueEnumerator(Queue, PeekPredicate);
		}

		static public IEnumerable<T> DequeueEnum<T>(
			this Queue<T> Queue)
		{
			return DequeueEnumerator(Queue);
		}

		static public void TypeClrAssemblyQualifiedNameExtraktAssemblyNameUndTypeFullName(
			string TypeClrAssemblyQualifiedName,
			out string AssemblyName,
			out string TypeFullName)
		{
			var ParsedAssemblyQualifiedName = new ParsedAssemblyQualifiedName.ParsedAssemblyQualifiedName(TypeClrAssemblyQualifiedName);

			AssemblyName = ParsedAssemblyQualifiedName.ShortAssemblyName;
			TypeFullName = ParsedAssemblyQualifiedName.TypeName;
		}

		static public WertZuZaitpunktStruct<TAus> CastGen<TAin, TAus>(
			this WertZuZaitpunktStruct<TAin> Ain)
			where TAin : TAus
		{
			return new WertZuZaitpunktStruct<TAus>(Ain.Wert, Ain.Zait);
		}

		static public WertZuZaitpunktStruct<TAus>? CastGen<TAin, TAus>(
			this WertZuZaitpunktStruct<TAin>? Ain)
			where TAin : TAus
		{
			if (!Ain.HasValue)
			{
				return null;
			}

			return CastGen<TAin, TAus>(Ain.Value);
		}

		static public WertZuZaitraumStruct<TAus> CastGen<TAin, TAus>(
			this WertZuZaitraumStruct<TAin> Ain)
			where TAin : TAus
		{
			return new WertZuZaitraumStruct<TAus>(Ain.Wert, Ain.BeginZait, Ain.EndeZait);
		}

		static public WertZuZaitraumStruct<TAus>? CastGen<TAin, TAus>(
			this WertZuZaitraumStruct<TAin>? Ain)
			where TAin : TAus
		{
			if (!Ain.HasValue)
			{
				return null;
			}

			return CastGen<TAin, TAus>(Ain.Value);
		}

		static public WertZuZaitraumStruct<TAus> CastScpez<TAin, TAus>(
			this WertZuZaitraumStruct<TAin> Ain)
			where TAus : class
		{
			return new WertZuZaitraumStruct<TAus>(Ain.Wert as TAus, Ain.BeginZait, Ain.EndeZait);
		}

		static public WertZuZaitraumStruct<TAus>? CastScpez<TAin, TAus>(
			this WertZuZaitraumStruct<TAin>? Ain)
			where TAus : class
		{
			if (!Ain.HasValue)
			{
				return null;
			}

			return CastScpez<TAin, TAus>(Ain.Value);
		}

		static public WertZuZaitraum<TAus> AbbildWert<TAin, TAus>(
			this WertZuZaitraum<TAin> Orig,
			Func<TAin, TAus> Sict)
		{
			if (null == Orig)
			{
				return null;
			}

			return new WertZuZaitraum<TAus>(
				Sict(Orig.Wert), Orig.Begin, Orig.Ende);
		}

		static public WertZuZaitraum<TAus> CastScpez<TAin, TAus>(
			this WertZuZaitraum<TAin> Ain)
			where TAus : class
		{
			if (null == Ain)
			{
				return null;
			}

			return AbbildWert<TAin, TAus>(Ain, AinWert => AinWert as TAus);

			return new WertZuZaitraum<TAus>(Ain.Wert as TAus, Ain.BeginZait, Ain.EndeZait);
		}

		static public IEnumerable<T> Yield<T>(
			T w)
		{
			yield return w;
		}

		static public WertZuZaitpunktStruct<T>? AlsBeginZaitpunktStruct<T>(
			this WertZuZaitraum<T> w)
		{
			if (null == w)
			{
				return null;
			}

			return new WertZuZaitpunktStruct<T>(w.Wert, w.BeginZait);
		}

		static public WertZuZaitpunktStruct<T>? AlsEndeZaitpunktStruct<T>(
			this WertZuZaitraum<T> w)
		{
			if (null == w)
			{
				return null;
			}

			return new WertZuZaitpunktStruct<T>(w.Wert, w.EndeZait);
		}

		static public IEnumerable<T> WhereNotNullSelectValue<T>(
			this IEnumerable<Nullable<T>> sequenz)
			where T : struct
		{
			return
				sequenz
				.WhereNullable((t) => t.HasValue)
				.SelectNullable((t) => t.Value);
		}

		/// <summary>
		/// Bsp:
		/// "System.data, version=1.0.3300.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
		/// </summary>
		static readonly System.Text.RegularExpressions.Regex AssemblyShortNameRegex =
			new System.Text.RegularExpressions.Regex(@"^([^,]+)\s*,", System.Text.RegularExpressions.RegexOptions.Compiled);

		static public string AssemblyShortNameVonAssemblyName(
			this string AssemblyName)
		{
			if (null == AssemblyName)
			{
				return null;
			}

			var Match = AssemblyShortNameRegex.Match(AssemblyName);

			if (!Match.Success)
			{
				return AssemblyName;
			}

			return Match.Groups[1].Value;
		}

		static public T NullIfEmpty<T>(
			this T Sequenz)
			where T : IEnumerable
		{
			if (Sequenz.NullOderLeer())
			{
				return default(T);
			}

			return Sequenz;
		}

		static public void RGBKonvertiirtNaacHueSatVal(
			int R,
			int G,
			int B,
			int RGBWerteberaicMax,
			int HueSatValWerteberaicMax,
			out int Hue,
			out int Sat,
			out int Val)
		{
			Hue = 0;

			var RGB_Min = Math.Min(R, Math.Min(G, B));
			var RGB_Max = Math.Max(R, Math.Max(G, B));

			var RGB_Delta_Max = RGB_Max - RGB_Min;             //Delta RGB value 

			Val = (RGB_Max * HueSatValWerteberaicMax) / RGBWerteberaicMax;

			if (RGB_Delta_Max == 0)                     //This is a gray, no chroma...
			{
				Hue = 0;                                //HSV results from 0 to 1
				Sat = 0;
			}
			else                                    //Chromatic data...
			{
				Sat = (RGB_Delta_Max * HueSatValWerteberaicMax * HueSatValWerteberaicMax) / RGB_Max / RGBWerteberaicMax;

				var del_R = ((((RGB_Max - R) / 6) + (RGB_Delta_Max / 2)) * HueSatValWerteberaicMax * HueSatValWerteberaicMax) / RGB_Delta_Max / RGBWerteberaicMax;
				var del_G = ((((RGB_Max - G) / 6) + (RGB_Delta_Max / 2)) * HueSatValWerteberaicMax * HueSatValWerteberaicMax) / RGB_Delta_Max / RGBWerteberaicMax;
				var del_B = ((((RGB_Max - B) / 6) + (RGB_Delta_Max / 2)) * HueSatValWerteberaicMax * HueSatValWerteberaicMax) / RGB_Delta_Max / RGBWerteberaicMax;

				if (R == RGB_Max)
				{
					Hue = del_B - del_G;
				}
				else
				{
					if (G == RGB_Max)
					{
						Hue = (HueSatValWerteberaicMax / 3) + del_R - del_B;
					}
					else
					{
						if (B == RGB_Max)
						{
							Hue = ((HueSatValWerteberaicMax * 2) / 3) + del_G - del_R;
						}
					}
				}

				if (Hue < 0) Hue += 1;
				if (Hue > 1) Hue -= 1;
			}
		}


		/// <summary>
		/// Constraint zu System.Enum nict mööglic, daher werden di für System.Enum als Basis verwendete Klase/Interface als Constraint verwendet.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="EnumSictString"></param>
		/// <returns></returns>
		static public T? EnumParseNullable<T>(
			this string EnumSictString,
			bool IgnoreCase = false)
			where T : struct, IConvertible, IComparable, IFormattable
		{
			if (null == EnumSictString)
			{
				return null;
			}

			if (!typeof(T).IsEnum)
			{
				throw new ArgumentException("!typeof(T).IsEnum");
			}

			T Result;

			if (Enum.TryParse<T>(EnumSictString, IgnoreCase, out Result))
			{
				return Result;
			}

			return null;
		}

		static public TValue TryGetValueOrDefault<TKey, TValue>(
			this IDictionary<TKey, TValue> Dict,
			TKey Key)
		{
			if (null != Dict)
			{
				TValue Value;

				if (Dict.TryGetValue(Key, out Value))
				{
					return Value;
				}
			}

			return default(TValue);
		}

		static public Nullable<TValue> TryGetValueNullable<TKey, TValue>(
			this IDictionary<TKey, TValue> Dict,
			TKey Key)
			where TValue : struct
		{
			if (null != Dict)
			{
				TValue Value;

				if (Dict.TryGetValue(Key, out Value))
				{
					return Value;
				}
			}

			return null;
		}

		static public T? FirstOrNull<T>(this IEnumerable<T> Seq)
			where T : struct
		{
			if (null == Seq)
			{
				return null;
			}

			foreach (var item in Seq)
			{
				return item;
			}

			return null;
		}

		static public T? FirstOrNull<T>(
			this IEnumerable<T> Seq,
			Func<T, bool> Predicate)
			where T : struct
		{
			if (null == Seq)
			{
				return null;
			}

			foreach (var item in Seq)
			{
				if (!Predicate(item))
				{
					continue;
				}

				return item;
			}

			return null;
		}

		static public T? LastOrNull<T>(this IEnumerable<T> Seq)
			where T : struct
		{
			if (null == Seq)
			{
				return null;
			}

			return Seq.Reverse().FirstOrNull();
		}

		static public T? LastOrNull<T>(
			this IEnumerable<T> Seq,
			Func<T, bool> Predicate)
			where T : struct
		{
			if (null == Seq)
			{
				return null;
			}

			return Seq.Reverse().FirstOrNull(Predicate);
		}

		static public bool SequenceEqualPerReferenceEqual(
			this IEnumerable Collection0,
			IEnumerable Collection1)
		{
			if (object.Equals(Collection0, Collection1))
			{
				return true;
			}

			if (null == Collection0 || null == Collection1)
			{
				return false;
			}

			var Collection0Enumerator = Collection0.GetEnumerator();
			var Collection1Enumerator = Collection1.GetEnumerator();

			while (true)
			{
				var Collection0EndeNict = Collection0Enumerator.MoveNext();
				var Collection1EndeNict = Collection1Enumerator.MoveNext();

				if (!(Collection0EndeNict && Collection1EndeNict))
				{
					if (Collection0EndeNict || Collection1EndeNict)
					{
						return false;
					}

					return true;
				}

				if (!object.ReferenceEquals(Collection0Enumerator.Current, Collection1Enumerator.Current))
				{
					return false;
				}
			}
		}

		static public object ReturnValueOrException<TAin, TReturn>(
			this Func<TAin, TReturn> Funk,
			TAin Ain)
		{
			try
			{
				return Funk(Ain);
			}
			catch (Exception Exception)
			{
				return Exception;
			}
		}

		static public IEnumerable<T> ExceptionCatch<T>(
			this IEnumerable<T> seq,
			Action<Exception> CallbackException = null)
		{
			using (var enumerator = seq.GetEnumerator())
			{
				bool next = true;

				while (next)
				{
					try
					{
						next = enumerator.MoveNext();
					}
					catch (Exception ex)
					{
						if (CallbackException != null)
						{
							CallbackException(ex);
						}
						continue;
					}

					if (next)
					{
						yield return enumerator.Current;
					}
				}
			}
		}

		static public IEnumerable<T> CatchException<T>(
			this IEnumerable<T> seq,
			Action<Exception> CallbackException = null)
		{
			return ExceptionCatch(seq, CallbackException);
		}

		static public IEnumerable<T> ExceptionMap<T>(
			this IEnumerable<T> seq,
			Func<Exception, T> CallbackExceptionMap = null)
		{
			using (var enumerator = seq.GetEnumerator())
			{
				bool next = true;

				while (next)
				{
					var ExceptionMapped = default(T);
					bool Catched = false;

					try
					{
						next = enumerator.MoveNext();
					}
					catch (Exception ex)
					{
						ExceptionMapped = CallbackExceptionMap(ex);
						Catched = true;
					}

					if (Catched)
					{
						yield return ExceptionMapped;
					}
					else
					{
						if (next)
						{
							yield return enumerator.Current;
						}
					}
				}
			}
		}

		static public IEnumerable<T> EnumGetValues<T>()
		{
			return Enum.GetValues(typeof(T)).OfType<T>();
		}

		static public IEnumerable<Nullable<T>> CastToNullable<T>(
			this IEnumerable<T> Source)
			where T : struct
		{
			if (null == Source)
			{
				return null;
			}

			return Source.Select(element => (Nullable<T>)element);
		}

		static public IEnumerable<T> EmptyIfNull<T>(
			IEnumerable<T> Source)
		{
			if (null == Source)
			{
				return new T[0];
			}

			return Source;
		}

		static public Vektor2DInt AlsVektor2DInt(
			this Vektor2DDouble Vektor)
		{
			return new Vektor2DInt((int)Vektor.A, (int)Vektor.B);
		}


		static public WertZuZaitpunktStruct<T>? Max<T>(
			this IEnumerable<WertZuZaitpunktStruct<T>> Source)
		{
			return Source.OrderByDescending(t => t.Zait).FirstOrDefault();
		}

		static public WertZuZaitpunktStruct<T> Max<T>(
			WertZuZaitpunktStruct<T> O0,
			WertZuZaitpunktStruct<T> O1)
		{
			return new[] { O0, O1 }.Max() ?? default(WertZuZaitpunktStruct<T>);
		}

		static public IDictionary<KeyT, ValueT> ToDictionary<KeyT, ValueT>(
			this IEnumerable<KeyValuePair<KeyT, ValueT>> source)
		{
			if (null == source)
			{
				return null;
			}

			return source.ToDictionary(t => t.Key, t => t.Value);
		}

		static public IDictionary<KeyT, ValueT[]> ToDictionary<KeyT, ValueT>(
			this IEnumerable<IGrouping<KeyT, ValueT>> source)
		{
			if (null == source)
			{
				return null;
			}

			return
				source.Select(group => new KeyValuePair<KeyT, ValueT[]>(group.Key, group.ToArray()))
				.ToDictionary();
		}

		static public void WriteToFileAndCreateDirectoryIfNotExisting(this string FilePath, byte[] File)
		{
			if (null == FilePath)
			{
				return;
			}

			var FileInfo = new FileInfo(FilePath);

			if (!FileInfo.Directory.Exists)
			{
				FileInfo.Directory.Create();
			}

			var FileStream = FileInfo.Create();

			try
			{
				FileStream.Write(File, 0, File.Length);
			}
			finally
			{
				FileStream.Close();
			}
		}

		static public void Push<T>(
			this Stack<T> dest,
			IEnumerable<T> source)
		{
			if (null == dest || null == source)
			{
				return;
			}

			foreach (var item in source)
			{
				dest.Push(item);
			}
		}

		static public void PropagiireList<T>(
			this IEnumerable<T> Herkunft,
			IList<T> Ziil,
			Func<T, T, bool> CallbackEquals)
		{
			if (null == Ziil)
			{
				return;
			}

			if (null == Herkunft)
			{
				Ziil.Clear();
				return;
			}

			var index = 0;

			foreach (var item in Herkunft)
			{
				try
				{
					if (Ziil.Count <= index)
					{
						Ziil.Add(item);
						continue;
					}

					if (CallbackEquals(item, Ziil[index]))
					{
						continue;
					}

					Ziil[index] = item;
				}
				finally
				{
					++index;
				}
			}

			while (index < Ziil.Count)
			{
				Ziil.RemoveAt(index);
			}
		}

		static public void PropagiireListObjectEquals<T>(
			this IEnumerable<T> Herkunft,
			IList<T> Ziil)
		{
			PropagiireList(Herkunft, Ziil, (a, b) => object.Equals(a, b));
		}

		static public void PropagiireList(
			this IEnumerable Herkunft,
			IList Ziil,
			Func<object, object, bool> CallbackEquals)
		{
			if (null == Ziil)
			{
				return;
			}

			if (null == Herkunft)
			{
				Ziil.Clear();
				return;
			}

			var index = 0;

			foreach (var item in Herkunft)
			{
				try
				{
					if (Ziil.Count <= index)
					{
						Ziil.Add(item);
						continue;
					}

					if (CallbackEquals(item, Ziil[index]))
					{
						continue;
					}

					/*
					2015.08.05
					Beobactung Probleem mit Verwendung in WPF Panel.Children:
					Exception: "Specified index is already in use. Disconnect the Visual child at the specified index first." aus VisualCollection.SetItem.

					Ziil[index] = item;
					*/

					Ziil.RemoveAt(index);
					Ziil.Insert(index, item);
				}
				finally
				{
					++index;
				}
			}

			while (index < Ziil.Count)
			{
				Ziil.Remove(index);
			}
		}

		static public void PropagiireListObjectEquals(
			this IEnumerable Herkunft,
			IList Ziil)
		{
			PropagiireList(Herkunft, Ziil, (a, b) => object.Equals(a, b));
		}

		static public OrtogoonInt VergröösertAngelpunktZentrum(
			this OrtogoonInt Ortogoon,
			Vektor2DInt Grööse)
		{
			return Ortogoon.GrööseGeseztAngelpunktZentrum(Ortogoon.Grööse + Grööse);
		}

		static public OrtogoonInt VergröösertAngelpunktZentrum(
			this OrtogoonInt Ortogoon,
			int Grööse)
		{
			return Ortogoon.VergröösertAngelpunktZentrum(new Vektor2DInt(Grööse, Grööse));
		}

		static public DateTime? ZaitKalenderAlsDateTime(
			this string ZaitKalender,
			int KomponenteAnzMin = 3)
		{
			if (null == ZaitKalender)
			{
				return null;
			}

			var ListeKomponenteString = ZaitKalender.Split(new[] { '.' });

			var ListeKomponenteWert =
				ListeKomponenteString
				.Select(KomponenteString => KomponenteString.TryParseInt())
				.TakeWhile(KomponenteWert => KomponenteWert.HasValue)
				.WhereNotNullSelectValue()
				.ToArray();

			if (ListeKomponenteWert.Length < KomponenteAnzMin)
			{
				return null;
			}

			var Jaar = ListeKomponenteWert.ElementAtOrDefault(0);
			var InJaarMoonat = ListeKomponenteWert.ElementAtOrDefault(1);
			var InMoonatTaag = ListeKomponenteWert.ElementAtOrDefault(2);
			var InTaagSctunde = ListeKomponenteWert.ElementAtOrDefault(3);
			var InSctundeMinuute = ListeKomponenteWert.ElementAtOrDefault(4);
			var InMinuuteSekunde = ListeKomponenteWert.ElementAtOrDefault(5);

			return new DateTime(Jaar, InJaarMoonat + 1, InMoonatTaag + 1, InTaagSctunde, InSctundeMinuute, InMinuuteSekunde);
		}

		static public int? IndexOfElementClosest<T>(
			this IEnumerable<T> Sequence,
			Func<T, Int64> CallbackDistanceOfElement) =>
			Sequence
			?.Select((item, index) => new KeyValuePair<int, T>(index, item))
			?.OrderBy(IndexAndElement => Math.Abs(CallbackDistanceOfElement(IndexAndElement.Value)))
			?.Select(IndexAndElement => (int?)IndexAndElement.Key)
			?.FirstOrDefault();

		static public IEnumerable<KeyValuePair<int, T>> SubSequenceAroundElementClosest<T>(
			this IEnumerable<T> Sequence,
			Func<T, Int64> CallbackDistanceOfElement,
			int PreviousElementCountMax = 0,
			int FollowingElementCountMax = 0)
		{
			var ClosestElementIndex = IndexOfElementClosest(Sequence, CallbackDistanceOfElement);

			if (!ClosestElementIndex.HasValue)
			{
				return null;
			}

			var IndexLow = Math.Max(0, ClosestElementIndex.Value - PreviousElementCountMax);

			return
				Sequence.Skip(IndexLow)
				.Select((element, index) => new KeyValuePair<int, T>(IndexLow + index - ClosestElementIndex.Value, element))
				.Take(ClosestElementIndex.Value + FollowingElementCountMax - IndexLow + 1);
		}

		static public Regex AlsRegex(this string RegexPattern, RegexOptions RegexOptions) =>
			null == RegexPattern ? null : new Regex(RegexPattern, RegexOptions);

		static public Regex AlsRegexIgnoreCaseCompiled(this string RegexPattern, RegexOptions RegexOptions = RegexOptions.None) =>
			null == RegexPattern ? null : new Regex(RegexPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions);

		static public T TryPeekOrDefault<T>(this ConcurrentQueue<T> Queue)
		{
			var Element = default(T);

			if (Queue?.TryPeek(out Element) ?? false)
			{
				return Element;
			}

			return default(T);
		}

		static public T TryDequeueOrDefault<T>(this ConcurrentQueue<T> Queue)
		{
			var Element = default(T);

			if (Queue?.TryDequeue(out Element) ?? false)
			{
				return Element;
			}

			return default(T);
		}

		static public Int64 GetInt64(this System.Security.Cryptography.RandomNumberGenerator Source)
		{
			var Pufer = new byte[8];

			Source.GetBytes(Pufer);

			return BitConverter.ToInt64(Pufer, 0);
		}

		static public IEnumerable<KeyValuePair<string[], FileInfo>> EnumFileFromDirectory(
			this string DirectoryPath,
			int? DepthMax = null)
		{
			var Directory = new DirectoryInfo(DirectoryPath);

			if (!Directory.Exists)
			{
				yield break;
			}

			foreach (var File in Directory.GetFiles())
			{
				yield return new KeyValuePair<string[], FileInfo>(new[] { File.Name }, File);
			}

			foreach (var SubDirectory in Directory.GetDirectories())
			{
				foreach (var SubEntry in EnumFileFromDirectory(SubDirectory.FullName, DepthMax - 1))
				{
					yield return new KeyValuePair<string[], FileInfo>(SubDirectory.Name.Yield().ConcatNullable(SubEntry.Key).ToArray(), SubEntry.Value);
				}
			}
		}

		static public string Truncate(this string Orig, int LengthMax) =>
			LengthMax < Orig?.Length ? Orig.Substring(0, LengthMax) : Orig;
	}
}
