using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Bib3;
using ExtractFromOldAssembly.Bib3;

namespace Optimat.EveOnline.Anwendung
{
	[JsonObject(MemberSerialization.OptIn)]
	public struct TransitionInfo<ScritIdentT, ScritWertT, TransitionZuusazInfoT>
	{
		[JsonProperty]
		readonly public int ScritIndex;

		[JsonProperty]
		readonly public ScritIdentT ScritIdent;

		[JsonProperty]
		readonly public ScritWertT ZiilWert;

		[JsonProperty]
		readonly public TransitionZuusazInfoT ZuusazInfo;

		public TransitionInfo(
			ScritIdentT ScritIdent,
			ScritWertT ZiilWert,
			TransitionZuusazInfoT ZuusazInfo = default(TransitionZuusazInfoT))
			:
			this(
			-1,
			ScritIdent,
			ZiilWert,
			ZuusazInfo)
		{
		}

		public TransitionInfo(
			int ScritIndex,
			ScritIdentT ScritIdent,
			ScritWertT ZiilWert,
			TransitionZuusazInfoT ZuusazInfo	= default(TransitionZuusazInfoT))
			:
			this()
		{
			this.ScritIndex = ScritIndex;
			this.ScritIdent = ScritIdent;
			this.ZiilWert = ZiilWert;
			this.ZuusazInfo = ZuusazInfo;
		}

		public TransitionInfo<ScritIdentT, ScritWertT, TransitionZuusazInfoT> SezeScritIndex(
			int	ScritIndex)
		{
			return new TransitionInfo<ScritIdentT, ScritWertT, TransitionZuusazInfoT>(
				ScritIndex,
				ScritIdent,
				ZiilWert,
				ZuusazInfo);
		}

	}

	/*
	 * 2014.09.13
	 * 
	public struct TransitionInfoAbzüüglicZuusazInfo<ScritIdentT,	ScritWertT>	: TransitionInfo<ScritIdentT,	ScritWertT,	int>
	{
		readonly public ScritIdentT ScritIdent;

		readonly public ScritWertT ZiilWert;

		public TransitionInfoAbzüüglicZuusazInfo(
			ScritIdentT ScritIdent,
			ScritWertT ZiilWert)
			:
			base(
			Scr
		{
			this.ScritIdent = ScritIdent;
			this.ZiilWert = ZiilWert;
		}
	}
	 * */

	[JsonObject(MemberSerialization.OptIn)]
	public class AusSequenzTransitioonMiinusZuusazInfo<ScritIdentTyp, ScritWertTyp> : AusSequenzTransitioon<ScritIdentTyp, ScritWertTyp, int>
	{
		public AusSequenzTransitioonMiinusZuusazInfo()
		{
		}

		public AusSequenzTransitioonMiinusZuusazInfo(
			int TransitioonListeScritAnzaalScranke,
			int ListeTransitioonZuErhalteAnzaalScranke,
			IEqualityComparer<ScritWertTyp> EqualityComparer = null)
			:
			base(
			TransitioonListeScritAnzaalScranke,
			ListeTransitioonZuErhalteAnzaalScranke,
			EqualityComparer)
		{
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class AusSequenzTransitioon<ScritIdentTyp,	ScritWertTyp,	TransitionZuusazInfoTyp>
	{
		[JsonProperty]
		public int? ScritLezteIndex
		{
			private set;
			get;
		}

		[JsonProperty]
		public int TransitioonListeScritAnzaalScranke;

		[JsonProperty]
		public int ListeTransitioonZuErhalteAnzaalScranke;

		[JsonProperty]
		readonly Queue<KeyValuePair<ScritIdentTyp, ScritWertTyp>> ListeScritIdentUndWert = new Queue<KeyValuePair<ScritIdentTyp, ScritWertTyp>>();

		[JsonProperty]
		readonly Queue<TransitionInfo<ScritIdentTyp, ScritWertTyp, TransitionZuusazInfoTyp>> InternListeTransitioonScritIdentUndZiilWert =
		new Queue<TransitionInfo<ScritIdentTyp, ScritWertTyp, TransitionZuusazInfoTyp>>();

		public IEnumerable<ScritIdentTyp> ListeTransitioonScritIdent()
		{
			return
				ListeTransitioonInfo()
				.SelectNullable((TransitionInfo) => TransitionInfo.ScritIdent);
		}

		public IEnumerable<KeyValuePair<ScritIdentTyp, ScritWertTyp>> ListeTransitioonScritIdentUndZiilWert()
		{
			return
				ListeTransitioonInfo()
				.SelectNullable((TransitionInfo) => new	KeyValuePair<ScritIdentTyp, ScritWertTyp>(TransitionInfo.ScritIdent, TransitionInfo.ZiilWert));
		}

		public IEnumerable<TransitionInfo<ScritIdentTyp, ScritWertTyp, TransitionZuusazInfoTyp>> ListeTransitioonInfo()
		{
			return InternListeTransitioonScritIdentUndZiilWert;
		}

		public	TransitionInfo<ScritIdentTyp, ScritWertTyp, TransitionZuusazInfoTyp> ListeTransitioonLezteInfo()
		{
			return InternListeTransitioonScritIdentUndZiilWert.LastOrDefault();
		}

		readonly IEqualityComparer<ScritWertTyp> EqualityComparer;

		public bool ScritWertEquals(
			ScritWertTyp Scrit0Wert,
			ScritWertTyp Scrit1Wert)
		{
			var EqualityComparer = this.EqualityComparer;

			if (null == EqualityComparer)
			{
				return object.Equals(Scrit0Wert, Scrit1Wert);
			}

			return EqualityComparer.Equals(Scrit0Wert, Scrit1Wert);
		}

		public AusSequenzTransitioon()
		{
		}

		public AusSequenzTransitioon(
			int TransitioonListeScritAnzaalScranke,
			int ListeTransitioonZuErhalteAnzaalScranke,
			IEqualityComparer<ScritWertTyp> EqualityComparer = null)
		{
			this.TransitioonListeScritAnzaalScranke = TransitioonListeScritAnzaalScranke;
			this.ListeTransitioonZuErhalteAnzaalScranke = ListeTransitioonZuErhalteAnzaalScranke;
			this.EqualityComparer = EqualityComparer;
		}

		public TransitionInfo<ScritIdentTyp, ScritWertTyp, TransitionZuusazInfoTyp>? AingangScrit(
			KeyValuePair<ScritIdentTyp, ScritWertTyp> ScritIdentUndWert)
		{
			return AingangScrit(ScritIdentUndWert.Key, ScritIdentUndWert.Value);
		}

		public TransitionInfo<ScritIdentTyp, ScritWertTyp, TransitionZuusazInfoTyp>? AingangScrit(
			ScritIdentTyp ScritIdent,
			ScritWertTyp ScritWert)
		{
			return AingangScrit(
				ScritIdent,
				ScritWert,
				this.TransitioonListeScritAnzaalScranke,
				this.ListeTransitioonZuErhalteAnzaalScranke);
		}

		public TransitionInfo<ScritIdentTyp, ScritWertTyp, TransitionZuusazInfoTyp>?
			AingangScrit(
			ScritIdentTyp ScritIdent,
			ScritWertTyp ScritWert,
			int TransitioonListeScritAnzaalScranke,
			int ListeTransitioonZuErhalteAnzaalScranke)
		{
			var VorherScritIndex = this.ScritLezteIndex;

			var ScritIndex = (VorherScritIndex + 1) ?? 0;

			try
			{
				var ListeScritIdentUndWertVorherCount = ListeScritIdentUndWert.Count;

				ListeScritIdentUndWert.Enqueue(new KeyValuePair<ScritIdentTyp, ScritWertTyp>(ScritIdent, ScritWert));

				TransitionInfo<ScritIdentTyp, ScritWertTyp, TransitionZuusazInfoTyp>? Transitioon = null;

				if (0 < ListeScritIdentUndWertVorherCount)
				{
					var PrüüfungBeginIndex = ListeScritIdentUndWert.Count - TransitioonListeScritAnzaalScranke;

					var TransitioonLezteZiilWert = InternListeTransitioonScritIdentUndZiilWert.LastOrDefault().ZiilWert;

					var PrüüfungTransitioonMengeScrit = ListeScritIdentUndWert.Skip(PrüüfungBeginIndex);

					Transitioon = InternAingangScrit(
						ScritIdent,
						ScritWert,
						TransitioonLezteZiilWert,
						PrüüfungTransitioonMengeScrit);
				}
				else
				{
					Transitioon = new TransitionInfo<ScritIdentTyp, ScritWertTyp,	TransitionZuusazInfoTyp>(ScritIdent, ScritWert);
				}

				if (Transitioon.HasValue)
				{
					InternListeTransitioonScritIdentUndZiilWert.Enqueue(Transitioon.Value.SezeScritIndex(ScritIndex));

					InternListeTransitioonScritIdentUndZiilWert.ListeKürzeBegin(Math.Max(1, ListeTransitioonZuErhalteAnzaalScranke));
				}

				ListeScritIdentUndWert.ListeKürzeBegin(Math.Max(1, TransitioonListeScritAnzaalScranke));

				return Transitioon;
			}
			finally
			{
				this.ScritLezteIndex = ScritIndex;
				this.TransitioonListeScritAnzaalScranke = TransitioonListeScritAnzaalScranke;
				this.ListeTransitioonZuErhalteAnzaalScranke = ListeTransitioonZuErhalteAnzaalScranke;
			}
		}

		/// <summary>
		/// Transitioon erfolgt dan wen kainer der lezte <paramref name="TransitioonListeScritAnzaalScranke"/> Scrite meer deen glaice Wert hat wii lezte Transitioon.Ziil.
		/// </summary>
		/// <param name="ScritIdent"></param>
		/// <param name="ScritWert"></param>
		/// <returns></returns>
		virtual protected TransitionInfo<ScritIdentTyp, ScritWertTyp, TransitionZuusazInfoTyp>? InternAingangScrit(
			ScritIdentTyp ScritIdent,
			ScritWertTyp ScritWert,
			ScritWertTyp TransitioonLezteZiilWert,
			IEnumerable<KeyValuePair<ScritIdentTyp, ScritWertTyp>> PrüüfungTransitioonListeScrit)
		{
			TransitioonListeScritAnzaalScranke = Math.Max(1, TransitioonListeScritAnzaalScranke);

			TransitionInfo<ScritIdentTyp, ScritWertTyp, TransitionZuusazInfoTyp>? Transitioon = null;

			if (PrüüfungTransitioonListeScrit.All((tScritIdentUndWert) => !this.ScritWertEquals(tScritIdentUndWert.Value, TransitioonLezteZiilWert)))
			{
				var ScritMitGlaiceWertFrühesteIndex =
					Math.Max(0,
					PrüüfungTransitioonListeScrit
					.FrühesteIndex((KandidaatScritIdentUndWert) => this.ScritWertEquals(KandidaatScritIdentUndWert.Value, ScritWert)) ?? 0);

				var TransitioonBeginElement = PrüüfungTransitioonListeScrit.ElementAt(ScritMitGlaiceWertFrühesteIndex);

				Transitioon = new TransitionInfo<ScritIdentTyp, ScritWertTyp, TransitionZuusazInfoTyp>(TransitioonBeginElement.Key, TransitioonBeginElement.Value);
			}

			return Transitioon;
		}
	}

	public struct MengeInGrupeAnzaalTransitioon<GrupeElementTyp>
	{
		readonly public KeyValuePair<GrupeElementTyp, int>[] ZiilWertMengeInGrupeAnzaal;

		readonly public KeyValuePair<GrupeElementTyp, int>[] ZiilWertMengeInGrupeZuunaame;

		readonly public KeyValuePair<GrupeElementTyp, int>[] ZiilWertMengeInGrupeAbnaame;

		public bool Glaicwertig()
		{
			return
				ZiilWertMengeInGrupeZuunaame.NullOderLeer() &&
				ZiilWertMengeInGrupeAbnaame.NullOderLeer();
		}

		public MengeInGrupeAnzaalTransitioon(
			KeyValuePair<GrupeElementTyp, int>[] ZiilWertMengeInGrupeAnzaal,
			KeyValuePair<GrupeElementTyp, int>[] ZiilWertMengeInGrupeZuunaame	= null,
			KeyValuePair<GrupeElementTyp, int>[] ZiilWertMengeInGrupeAbnaame	= null)
		{
			this.ZiilWertMengeInGrupeAnzaal = ZiilWertMengeInGrupeAnzaal;
			this.ZiilWertMengeInGrupeZuunaame = ZiilWertMengeInGrupeZuunaame;
			this.ZiilWertMengeInGrupeAbnaame = ZiilWertMengeInGrupeAbnaame;
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class AusSequenzMengeInGrupeAnzaalTransitioon<ScritIdentTyp, GrupeElementTyp> :
		AusSequenzTransitioon<ScritIdentTyp, IEnumerable<GrupeElementTyp>, MengeInGrupeAnzaalTransitioon<GrupeElementTyp>>
	{
		readonly IEqualityComparer<GrupeElementTyp> ElementEqualityComparer;

		public bool ScritWertElementEquals(
			GrupeElementTyp Element0Wert,
			GrupeElementTyp Element1Wert)
		{
			var ElementEqualityComparer = this.ElementEqualityComparer;

			if (null == ElementEqualityComparer)
			{
				return object.Equals(Element0Wert, Element1Wert);
			}

			return ElementEqualityComparer.Equals(Element0Wert, Element1Wert);
		}

		public AusSequenzMengeInGrupeAnzaalTransitioon()
		{
		}

		public AusSequenzMengeInGrupeAnzaalTransitioon(
			int TransitioonListeScritAnzaalScranke,
			int ListeTransitioonZuErhalteAnzaalScranke,
			IEqualityComparer<GrupeElementTyp> ElementEqualityComparer = null)
			:
			base(
			TransitioonListeScritAnzaalScranke,
			ListeTransitioonZuErhalteAnzaalScranke)
		{
			this.ElementEqualityComparer = ElementEqualityComparer;
		}

		public bool Glaicwertig(
			IEnumerable<GrupeElementTyp> HerkunftWertMengeElement,
			IEnumerable<GrupeElementTyp> ZiilWertMengeElement)
		{
			if (Bib3.Glob.SequenceEqualPerObjectEquals(ZiilWertMengeElement, HerkunftWertMengeElement))
			{
				//	Für Performanz Abkürzung zuusäzlice Prüüfung.
				return true;
			}

			return	TransitioonBerecne(HerkunftWertMengeElement, ZiilWertMengeElement).Glaicwertig();
		}

		public MengeInGrupeAnzaalTransitioon<GrupeElementTyp> TransitioonBerecne(
			IEnumerable<GrupeElementTyp> HerkunftWertMengeElement,
			IEnumerable<GrupeElementTyp> ZiilWertMengeElement)
		{
			var ZiilWertMengeGrupe	=
				(null == ZiilWertMengeElement) ? null :
				ZiilWertMengeElement.GroupBy((t) => t, this.ElementEqualityComparer).ToArray();

			var ZiilWertMengeInGrupeAnzaal =
				ZiilWertMengeGrupe
				.SelectNullable((Grupe) => new KeyValuePair<GrupeElementTyp, int>(Grupe.Key, Grupe.Count()))
				.ToArrayNullable();

			if (HerkunftWertMengeElement == ZiilWertMengeElement)
			{
				return new MengeInGrupeAnzaalTransitioon<GrupeElementTyp>(ZiilWertMengeInGrupeAnzaal);
			}

			if (null == HerkunftWertMengeElement)
			{
				return new MengeInGrupeAnzaalTransitioon<GrupeElementTyp>(ZiilWertMengeInGrupeAnzaal,	ZiilWertMengeInGrupeAnzaal,	null);
			}

			var HerkunftWertMengeGrupe =
				HerkunftWertMengeElement.GroupBy((t) => t, this.ElementEqualityComparer).ToArray();

			var HerkunftWertMengeInGrupeAnzaal =
				HerkunftWertMengeGrupe
				.SelectNullable((Grupe) => new KeyValuePair<GrupeElementTyp, int>(Grupe.Key, Grupe.Count()))
				.ToArrayNullable();

			var ZiilWertMengeInGrupeZuunaame =
				ZiilWertMengeInGrupeAnzaal
				.SelectNullable((ZiilWertGrupe) =>
					new	KeyValuePair<GrupeElementTyp,	int>(
						ZiilWertGrupe.Key,
						ZiilWertGrupe.Value -
						HerkunftWertMengeInGrupeAnzaal
						.FirstOrDefaultNullable((KandidaatHerkunftGrupe) => ScritWertElementEquals(KandidaatHerkunftGrupe.Key, ZiilWertGrupe.Key)).Value))
				.WhereNullable((GrupeZuunaame) => 0 < GrupeZuunaame.Value);

			var ZiilWertMengeInGrupeAbnaame =
				HerkunftWertMengeInGrupeAnzaal
				.SelectNullable((HerkunftWertGrupe) =>
					new	KeyValuePair<GrupeElementTyp,	int>(
						HerkunftWertGrupe.Key,
						HerkunftWertGrupe.Value -
						ZiilWertMengeInGrupeAnzaal
						.FirstOrDefaultNullable((ZiilWertGrupe) => ScritWertElementEquals(ZiilWertGrupe.Key, HerkunftWertGrupe.Key)).Value))
				.WhereNullable((GrupeAbnaame) => 0 < GrupeAbnaame.Value);

			var ZiilWertMengeInGrupeZuunaameArray = ZiilWertMengeInGrupeZuunaame.ToArrayFalsNitLeer();
			var ZiilWertMengeInGrupeAbnaameArray = ZiilWertMengeInGrupeAbnaame.ToArrayFalsNitLeer();

			return new MengeInGrupeAnzaalTransitioon<GrupeElementTyp>(
				ZiilWertMengeInGrupeAnzaal,
				ZiilWertMengeInGrupeZuunaameArray,
				ZiilWertMengeInGrupeAbnaameArray);
		}

		override protected TransitionInfo<ScritIdentTyp, IEnumerable<GrupeElementTyp>, MengeInGrupeAnzaalTransitioon<GrupeElementTyp>>?
			InternAingangScrit(
			ScritIdentTyp ScritIdent,
			IEnumerable<GrupeElementTyp> ScritWert,
			IEnumerable<GrupeElementTyp> TransitioonLezteZiilWert,
			IEnumerable<KeyValuePair<ScritIdentTyp, IEnumerable<GrupeElementTyp>>> PrüüfungTransitioonListeScrit)
		{
			var ListePrüüfungTransitionScritVonTransitionLezteErgeebnis = new List<MengeInGrupeAnzaalTransitioon<GrupeElementTyp>>();

			foreach (var PrüüfungTransitionScrit in PrüüfungTransitioonListeScrit)
			{
				var TransitionScritVonTransitionLezteErgeebnis =	TransitioonBerecne(TransitioonLezteZiilWert, PrüüfungTransitionScrit.Value);

				if (TransitionScritVonTransitionLezteErgeebnis.Glaicwertig())
				{
					return null;
				}

				ListePrüüfungTransitionScritVonTransitionLezteErgeebnis.Add(TransitionScritVonTransitionLezteErgeebnis);
			}

			var	ListeScritMitGlaiceWertFrüheste	=
				PrüüfungTransitioonListeScrit.FirstOrDefault((Kandidaat) => Glaicwertig(ScritWert, Kandidaat.Value));

			return	new	TransitionInfo<ScritIdentTyp, IEnumerable<GrupeElementTyp>, MengeInGrupeAnzaalTransitioon<GrupeElementTyp>>
				(ListeScritMitGlaiceWertFrüheste.Key,
				ListeScritMitGlaiceWertFrüheste.Value,
				TransitioonBerecne(TransitioonLezteZiilWert, ListeScritMitGlaiceWertFrüheste.Value));
		}
	}
}
