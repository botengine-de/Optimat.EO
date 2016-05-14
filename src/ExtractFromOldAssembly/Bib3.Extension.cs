using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractFromOldAssembly.Bib3
{
	static public class Extension
	{
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

		static public IEnumerable<TAus> OfTypeNullable<TAus>(
			this IEnumerable Menge)
		{
			if (null == Menge)
			{
				return null;
			}

			return Menge.OfType<TAus>();
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
	}
}
