using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Bib3;
using Optimat.EveOnline.VonSensor;
using ExtractFromOldAssembly.Bib3;

namespace Optimat.EveOnline.Anwendung
{
	/// <summary>
	/// Baasisklase für Entscaidung ob ain Scnapscus als Fortsezung zu ainem Objekt zuuläsig isc.
	/// Von diiser Abgelaitete Klase scpaicern deen Tail des Objekt der zur Identifikatioon der Repräsentatioon des Objekt in ainem Scnapscus benöötigt werd.
	/// Baiscpiil: Gbs Objekt könte z.B. durc desen Fläce identifiziirt werde.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public	abstract	class SictScnapscusAlsFortsazZuuläsig<T>
	{
		abstract public bool ScnapscusAlsFortsazZuuläsigBerecne(T Scnapscus);

		abstract public void AingangScnapscusAlsFortsaz(T Scnapscus);
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictScnapscusAlsFortsazZuuläsigFürObjektMitLaage<ScnapscusType> : SictScnapscusAlsFortsazZuuläsig<ScnapscusType>
		where ScnapscusType : GbsElement
	{
		[JsonProperty]
		OrtogoonInt ScnapscusLezteFläce;

		override public bool ScnapscusAlsFortsazZuuläsigBerecne(ScnapscusType Scnapscus)
		{
			if (null == ScnapscusLezteFläce)
			{
				//	Laage noc nit noc nit fesctgeleegt.
				return true;
			}

			if (null == Scnapscus)
			{
				return false;
			}

			var ScnapscusInGbsFläce = Scnapscus.InGbsFläce;

			if (null == ScnapscusInGbsFläce)
			{
				return false;
			}

			var Scnitfläce = OrtogoonInt.Scnitfläce(ScnapscusLezteFläce, ScnapscusInGbsFläce);

			if (null == Scnitfläce)
			{
				return false;
			}

			if (Scnitfläce.Betraag() < ScnapscusLezteFläce.Betraag() / 2)
			{
				return false;
			}

			return true;
		}

		override public void AingangScnapscusAlsFortsaz(ScnapscusType Scnapscus)
		{
			if (null != Scnapscus)
			{
				ScnapscusLezteFläce = Scnapscus.InGbsFläce;
			}
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictScnapscusAlsFortsazZuuläsigFürObjektMitBezaicnerInt<ScnapscusType> : SictScnapscusAlsFortsazZuuläsig<ScnapscusType>
		where	ScnapscusType	: ObjektMitIdentInt64
	{
		[JsonProperty]
		Int64? ObjektIdent;

		override public bool ScnapscusAlsFortsazZuuläsigBerecne(ScnapscusType Scnapscus)
		{
			var ObjektIdent = this.ObjektIdent;

			if (!ObjektIdent.HasValue)
			{
				//	Identitäät noc nit fesctgeleegt.
				return true;
			}

			if (null == Scnapscus)
			{
				return false;
			}

			var AusScnapscusIdent = Scnapscus.Ident;

			return AusScnapscusIdent == ObjektIdent;
		}

		override public void AingangScnapscusAlsFortsaz(ScnapscusType Scnapscus)
		{
			if (null != Scnapscus)
			{
				ObjektIdent = Scnapscus.Ident;
			}
		}
	}

	/// <summary>
	/// Agregiirt info zu Objekt aus meerere Scnapscus welce Info zu Objekt enthalte.
	/// </summary>
	/// <typeparam name="ObjektIdentScnapscusType"></typeparam>
	/// <typeparam name="SictScnapscusAlsFortsazZuuläsigType"></typeparam>
	[JsonObject(MemberSerialization.OptIn)]
	public class SictZuObjektMengeScnapscusZuZaitAggr<ObjektIdentScnapscusType, SictScnapscusAlsFortsazZuuläsigType,	SictZuusazInfoScnapscusType>
		where SictScnapscusAlsFortsazZuuläsigType : SictScnapscusAlsFortsazZuuläsig<ObjektIdentScnapscusType>, new()
	{
		readonly SictScnapscusAlsFortsazZuuläsigType SictScnapscusAlsFortsazZuuläsig = new SictScnapscusAlsFortsazZuuläsigType();

		[JsonProperty]
		readonly	public	int? ListeScnapscusZuZaitZuErhalteAnzaalScrankeMax;

		[JsonProperty]
		readonly public bool ZuusazInfoErhalteInListeScnapscus;

		[JsonProperty]
		readonly Queue<SictWertMitZait<KeyValuePair<ObjektIdentScnapscusType,	SictZuusazInfoScnapscusType>>> ListeScnapscusZuZait =
		new Queue<SictWertMitZait<KeyValuePair<ObjektIdentScnapscusType, SictZuusazInfoScnapscusType>>>();

		/// <summary>
		/// Lezte Scnapscus für welce Tail ObjektIdent unglaic default war.
		/// </summary>
		[JsonProperty]
		public SictWertMitZait<KeyValuePair<int,	KeyValuePair<ObjektIdentScnapscusType, SictZuusazInfoScnapscusType>>>? AingangUnglaicDefaultZuZaitMitIndexLezte
		{
			private set;
			get;
		}

		public int? AingangUnglaicDefaultLezteIndex
		{
			get
			{
				var AingangUnglaicDefaultZuZaitMitIndexLezte = this.AingangUnglaicDefaultZuZaitMitIndexLezte;

				if (!AingangUnglaicDefaultZuZaitMitIndexLezte.HasValue)
				{
					return null;
				}

				return AingangUnglaicDefaultZuZaitMitIndexLezte.Value.Wert.Key;
			}
		}

		public SictWertMitZait<KeyValuePair<ObjektIdentScnapscusType, SictZuusazInfoScnapscusType>>? AingangUnglaicDefaultZuZaitLezte
		{
			get
			{
				var AingangUnglaicDefaultZuZaitMitIndexLezte = this.AingangUnglaicDefaultZuZaitMitIndexLezte;

				if (!AingangUnglaicDefaultZuZaitMitIndexLezte.HasValue)
				{
					return null;
				}

				return new SictWertMitZait<KeyValuePair<ObjektIdentScnapscusType, SictZuusazInfoScnapscusType>>(
					AingangUnglaicDefaultZuZaitMitIndexLezte.Value.Zait,
					AingangUnglaicDefaultZuZaitMitIndexLezte.Value.Wert.Value);
			}
		}

		/// <summary>
		/// Zait des früheste Scnapscus.
		/// </summary>
		[JsonProperty]
		public Int64? ScnapscusFrühesteZait
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? ScnapscusLeerFrühesteZait
		{
			private set;
			get;
		}

		public Int64? AingangUnglaicDefaultLezteZait
		{
			get
			{
				var AingangUnglaicDefaultZuZaitLezte = this.AingangUnglaicDefaultZuZaitLezte;

				if (!AingangUnglaicDefaultZuZaitLezte.HasValue)
				{
					return null;
				}

				return AingangUnglaicDefaultZuZaitLezte.Value.Zait;
			}
		}

		[JsonProperty]
		public int ListeScnapscusZuZaitAingangBisherAnzaal
		{
			private set;
			get;
		}

		public int? SaitAingangUnglaicDefaultLezteListeAingangAnzaal()
		{
			return ListeScnapscusZuZaitAingangBisherAnzaal - AingangUnglaicDefaultLezteIndex;
		}

		public IEnumerable<SictWertMitZait<KeyValuePair<ObjektIdentScnapscusType, SictZuusazInfoScnapscusType>>> ListeScnapscusZuZaitKopiiBerecne()
		{
			return ListeScnapscusZuZait.ToArrayNullable();
		}

		static public void ZuZaitAingangMengeObjektScnapscus<AbgelaiteteType>(
			Int64 Zait,
			IEnumerable<ObjektIdentScnapscusType> MengeObjektScnapscus,
			IList<AbgelaiteteType> ZiilListeObjektZuusctandAggr,
			bool TailmengeWelceNictInScnapscusRepräsentiirtEntferne = false,
			SictZuusazInfoScnapscusType	ZuusazInfoScnapscus	= default(SictZuusazInfoScnapscusType))
			where AbgelaiteteType : SictZuObjektMengeScnapscusZuZaitAggr<ObjektIdentScnapscusType, SictScnapscusAlsFortsazZuuläsigType, SictZuusazInfoScnapscusType>, new()
		{
			if (null == ZiilListeObjektZuusctandAggr)
			{
				return;
			}

			var MengeObjektRepräsentiirtInScnapscus = new List<AbgelaiteteType>();

			if (null != MengeObjektScnapscus)
			{
				foreach (var ObjektScnapscus in MengeObjektScnapscus)
				{
					var ObjektZuusctandAggr = ZiilListeObjektZuusctandAggr.FirstOrDefault((Kandidaat) => Kandidaat.PasendZuBisherige(ObjektScnapscus));

					if (null == ObjektZuusctandAggr)
					{
						ObjektZuusctandAggr = new AbgelaiteteType();

						ZiilListeObjektZuusctandAggr.Add(ObjektZuusctandAggr);
					}

					MengeObjektRepräsentiirtInScnapscus.Add(ObjektZuusctandAggr);

					ObjektZuusctandAggr.AingangScnapscus(Zait, ObjektScnapscus, ZuusazInfoScnapscus);
				}
			}

			var MengeObjektRepräsentiirtInScnapscusNict =
				ZiilListeObjektZuusctandAggr.Except(MengeObjektRepräsentiirtInScnapscus).ToArray();

			foreach (var ObjektRepräsentiirtInScnapscusNict in MengeObjektRepräsentiirtInScnapscusNict)
			{
				if (TailmengeWelceNictInScnapscusRepräsentiirtEntferne)
				{
					ZiilListeObjektZuusctandAggr.Remove(ObjektRepräsentiirtInScnapscusNict);
				}
				else
				{
					if (null != ObjektRepräsentiirtInScnapscusNict)
					{
						ObjektRepräsentiirtInScnapscusNict.AingangScnapscusLeer(Zait, ZuusazInfoScnapscus);
					}
				}
			}

			//	!!!!	Ainbau erhaltung Ordnung
		}

		public SictWertMitZait<AbgelaiteteType> SictWertMitZaitBerecneFürBeginZait<AbgelaiteteType>(
			Int64 BeginZaitDefault = default(Int64))
			where AbgelaiteteType : SictZuObjektMengeScnapscusZuZaitAggr<ObjektIdentScnapscusType, SictScnapscusAlsFortsazZuuläsigType, SictZuusazInfoScnapscusType>
		{
			return SictZuObjektMengeScnapscusZuZaitAggr<ObjektIdentScnapscusType, SictScnapscusAlsFortsazZuuläsigType, SictZuusazInfoScnapscusType>
				.SictWertMitZaitBerecneFürBeginZait<AbgelaiteteType>(
				this as AbgelaiteteType, BeginZaitDefault);
		}

		/// <summary>
		/// Konstruiirt noie Instanz fals Scnapscus nit pasend zu bisheerige.
		/// </summary>
		/// <typeparam name="AbgelaiteteType"></typeparam>
		/// <param name="ObjektZuusctand"></param>
		/// <param name="Zait"></param>
		/// <param name="Scnapscus"></param>
		/// <param name="ZuusazInfoScnapscus"></param>
		static public void ObjektKonstruktOderAingangScnapscus<AbgelaiteteType>(
			ref	AbgelaiteteType ObjektZuusctand,
			Int64	Zait,
			ObjektIdentScnapscusType	Scnapscus,
			SictZuusazInfoScnapscusType	ZuusazInfoScnapscus	= default(SictZuusazInfoScnapscusType))
			where AbgelaiteteType : SictZuObjektMengeScnapscusZuZaitAggr<ObjektIdentScnapscusType, SictScnapscusAlsFortsazZuuläsigType, SictZuusazInfoScnapscusType>,	new()
		{
			if (null == Scnapscus)
			{
				ObjektZuusctand = null;
				return;
			}

			if (null != ObjektZuusctand)
			{
				if (!ObjektZuusctand.PasendZuBisherige(Scnapscus))
				{
					ObjektZuusctand = null;
				}
			}

			if (null == ObjektZuusctand)
			{
				ObjektZuusctand = new AbgelaiteteType();
			}

			ObjektZuusctand.AingangScnapscus(Zait, Scnapscus, ZuusazInfoScnapscus);
		}

		static public SictWertMitZait<AbgelaiteteType> SictWertMitZaitBerecneFürBeginZait<AbgelaiteteType>(
			AbgelaiteteType ZuusctandAggr,
			Int64 BeginZaitDefault = default(Int64))
			where AbgelaiteteType : SictZuObjektMengeScnapscusZuZaitAggr<ObjektIdentScnapscusType, SictScnapscusAlsFortsazZuuläsigType, SictZuusazInfoScnapscusType>
		{
			return new SictWertMitZait<AbgelaiteteType>(
				((null == ZuusctandAggr) ? null : ZuusctandAggr.ScnapscusFrühesteZait) ?? BeginZaitDefault, ZuusctandAggr);
		}

		static public IEnumerable<SictWertMitZait<AbgelaiteteType>> ListeZuusctandAggrMitBeginZaitBerecneAusListeZuusctandAggr<AbgelaiteteType>(
			IEnumerable<AbgelaiteteType> ListeZuusctandAggr,
			Int64 BeginZaitDefault = default(Int64))
			where AbgelaiteteType : SictZuObjektMengeScnapscusZuZaitAggr<ObjektIdentScnapscusType, SictScnapscusAlsFortsazZuuläsigType, SictZuusazInfoScnapscusType>
		{
			if (null == ListeZuusctandAggr)
			{
				return null;
			}

			var ListeZuusctandAggrMitBeginZait =
				ListeZuusctandAggr
				.Select((ZuusctandAggr) => SictWertMitZaitBerecneFürBeginZait(ZuusctandAggr, BeginZaitDefault));

			return ListeZuusctandAggrMitBeginZait;
		}

		static public IEnumerable<SictWertMitZait<KeyValuePair<ObjektIdentScnapscusType, SictZuusazInfoScnapscusType>?>>
			ListeScnapscusLezteWertMitZaitBerecneAusListeZuusctandAggrMitZait<AbgelaiteteType>(
			IEnumerable<SictWertMitZait<AbgelaiteteType>> ListeZuusctandAggrMitZait)
			where AbgelaiteteType : SictZuObjektMengeScnapscusZuZaitAggr<ObjektIdentScnapscusType, SictScnapscusAlsFortsazZuuläsigType, SictZuusazInfoScnapscusType>
		{
			if (null == ListeZuusctandAggrMitZait)
			{
				return null;
			}

			var ListeScnapscusLezteWertMitZait =
				ListeZuusctandAggrMitZait
				.Select((ZuusctandAggrMitZait) => new SictWertMitZait<KeyValuePair<ObjektIdentScnapscusType, SictZuusazInfoScnapscusType>?>(
					ZuusctandAggrMitZait.Zait,
					(null == ZuusctandAggrMitZait.Wert) ? null : ZuusctandAggrMitZait.Wert.AingangScnapscusLezteBerecne()));

			return ListeScnapscusLezteWertMitZait;
		}

		static public IEnumerable<SictWertMitZait<ObjektIdentScnapscusType>>
			ListeScnapscusLezteWertTailObjektIdentMitZaitBerecneAusListeZuusctandAggrMitZait<AbgelaiteteType>(
			IEnumerable<SictWertMitZait<AbgelaiteteType>> ListeZuusctandAggrMitZait)
			where AbgelaiteteType : SictZuObjektMengeScnapscusZuZaitAggr<ObjektIdentScnapscusType, SictScnapscusAlsFortsazZuuläsigType, SictZuusazInfoScnapscusType>
		{
			var ListeScnapscusLezteWertMitZait =
				ListeScnapscusLezteWertMitZaitBerecneAusListeZuusctandAggrMitZait(ListeZuusctandAggrMitZait);

			if (null == ListeScnapscusLezteWertMitZait)
			{
				return null;
			}

			var ListeScnapscusLezteWertTailObjektIdentMitZait =
				ListeScnapscusLezteWertMitZait
				.Select((ZuusctandAggrMitZait) => new SictWertMitZait<ObjektIdentScnapscusType>(
					ZuusctandAggrMitZait.Zait,
					ZuusctandAggrMitZait.Wert.HasValue ? ZuusctandAggrMitZait.Wert.Value.Key	: default(ObjektIdentScnapscusType)));

			return ListeScnapscusLezteWertTailObjektIdentMitZait;
		}

		static public void PropagiireVonScnapscusNaacMengeZuusctand<ObjektType>(
			Int64 ScnapscusZait,
			IEnumerable<ObjektIdentScnapscusType> ScnapscusMengeObjektZuusctand,
			ICollection<ObjektType> MengeObjektZuusctand)
			where ObjektType : SictZuObjektMengeScnapscusZuZaitAggr<ObjektIdentScnapscusType, SictScnapscusAlsFortsazZuuläsigType, SictZuusazInfoScnapscusType>, new()
		{
			if (null == MengeObjektZuusctand)
			{
				return;
			}

			var MengeObjektZuusctandBeraitsVersorgt = MengeObjektZuusctand.Take(0).ToList();

			if (null != ScnapscusMengeObjektZuusctand)
			{
				foreach (var ScnapscusObjektZuusctand in ScnapscusMengeObjektZuusctand)
				{
					var ObjektZuusctand =
						MengeObjektZuusctand.FirstOrDefault((Kandidaat) => Kandidaat.PasendZuBisherige(ScnapscusObjektZuusctand));

					if (null == ObjektZuusctand)
					{
						ObjektZuusctand = new ObjektType();

						MengeObjektZuusctand.Add(ObjektZuusctand);
					}

					MengeObjektZuusctandBeraitsVersorgt.Add(ObjektZuusctand);

					ObjektZuusctand.AingangScnapscus(ScnapscusZait, ScnapscusObjektZuusctand);
				}
			}

			var MengeObjektOoneAusScnapscusZuusctand =
				MengeObjektZuusctand.Except(MengeObjektZuusctandBeraitsVersorgt);

			foreach (var ObjektOoneAusScnapscusZuusctand in MengeObjektOoneAusScnapscusZuusctand)
			{
				ObjektOoneAusScnapscusZuusctand.AingangScnapscusLeer(ScnapscusZait);
			}
		}

		public SictZuObjektMengeScnapscusZuZaitAggr()
		{
		}

		public SictZuObjektMengeScnapscusZuZaitAggr(
			int? ListeScnapscusZuZaitZuErhalteAnzaalScrankeMax,
			bool	ZuusazInfoErhalteInListeScnapscus	= false)
		{
			this.ListeScnapscusZuZaitZuErhalteAnzaalScrankeMax = ListeScnapscusZuZaitZuErhalteAnzaalScrankeMax;
			this.ZuusazInfoErhalteInListeScnapscus = ZuusazInfoErhalteInListeScnapscus;
		}

		public SictZuObjektMengeScnapscusZuZaitAggr(
			int? ListeScnapscusZuZaitZuErhalteAnzaalScrankeMax,
			Int64 Zait,
			ObjektIdentScnapscusType GbsObjektScnapscus,
			SictZuusazInfoScnapscusType	ZuusazInfoScnapscus,
			bool ZuusazInfoErhalteInListeScnapscus = false)
			:
			this(
			ListeScnapscusZuZaitZuErhalteAnzaalScrankeMax,
			ZuusazInfoErhalteInListeScnapscus)
		{
			AingangScnapscus(Zait, GbsObjektScnapscus, ZuusazInfoScnapscus);
		}

		public bool PasendZuBisherige(ObjektIdentScnapscusType KandidaatScnapscus)
		{
			if (ListeScnapscusZuZait.Count < 1)
			{
				return true;
			}

			return SictScnapscusAlsFortsazZuuläsig.ScnapscusAlsFortsazZuuläsigBerecne(KandidaatScnapscus);
		}

		public bool AingangScnapscusLeer(
			Int64 Zait,
			SictZuusazInfoScnapscusType ZuusazInfoScnapscus = default(SictZuusazInfoScnapscusType))
		{
			if (0 < ListeScnapscusZuZait.Count)
			{
				var ListeScnapscusZuZaitLezte = ListeScnapscusZuZait.LastOrDefault();

				if (!(ListeScnapscusZuZaitLezte.Zait < Zait))
				{
					return false;
				}
			}

			InternAingangScnapscus(Zait, default(ObjektIdentScnapscusType), ZuusazInfoScnapscus);

			return true;
		}

		public bool AingangScnapscus(
			Int64 Zait,
			ObjektIdentScnapscusType KandidaatObjektScnapscus,
			SictZuusazInfoScnapscusType ZuusazInfoScnapscus	= default(SictZuusazInfoScnapscusType))
		{
			if (0 < ListeScnapscusZuZait.Count)
			{
				var ListeScnapscusZuZaitLezte = ListeScnapscusZuZait.LastOrDefault();

				if (!(ListeScnapscusZuZaitLezte.Zait < Zait))
				{
					//	Scnapscus nur sctreng monotoon sctaigende Raihefolge zuulase
					return false;
				}
			}

			if (!PasendZuBisherige(KandidaatObjektScnapscus))
			{
				return false;
			}

			InternAingangScnapscus(Zait, KandidaatObjektScnapscus, ZuusazInfoScnapscus);

			return true;
		}

		void InternAingangScnapscus(
			Int64 Zait,
			ObjektIdentScnapscusType ObjektIdentScnapscus,
			SictZuusazInfoScnapscusType	ZuusazInfoScnapscus)
		{
			if (!ScnapscusFrühesteZait.HasValue)
			{
				if (object.Equals(default(ObjektIdentScnapscusType), ObjektIdentScnapscus))
				{
					//	Für Konstruktor oone Übergaabe Scnapscus Abbruc.
					return;
				}

				ScnapscusFrühesteZait = Zait;
			}

			if (object.Equals(default(ObjektIdentScnapscusType), ObjektIdentScnapscus))
			{
				if (!ScnapscusLeerFrühesteZait.HasValue)
				{
					ScnapscusLeerFrühesteZait = Zait;
				}
			}

			SictScnapscusAlsFortsazZuuläsig.AingangScnapscusAlsFortsaz(ObjektIdentScnapscus);

			var	ScnapscusZuZait	= new SictWertMitZait<KeyValuePair<ObjektIdentScnapscusType,	SictZuusazInfoScnapscusType>>(
				Zait,
				new	KeyValuePair<ObjektIdentScnapscusType,	SictZuusazInfoScnapscusType>(ObjektIdentScnapscus,
					ZuusazInfoErhalteInListeScnapscus	? ZuusazInfoScnapscus	: default(SictZuusazInfoScnapscusType)));

			var ScnapscusZuZaitMitIndex =
				new SictWertMitZait<KeyValuePair<int,	KeyValuePair<ObjektIdentScnapscusType, SictZuusazInfoScnapscusType>>>(
				Zait,
				new KeyValuePair<int, KeyValuePair<ObjektIdentScnapscusType, SictZuusazInfoScnapscusType>>(
					ListeScnapscusZuZaitAingangBisherAnzaal	+ 1,
					new	KeyValuePair<ObjektIdentScnapscusType, SictZuusazInfoScnapscusType>(
						ObjektIdentScnapscus,
					ZuusazInfoErhalteInListeScnapscus ? ZuusazInfoScnapscus : default(SictZuusazInfoScnapscusType))));

			ListeScnapscusZuZait.Enqueue(ScnapscusZuZait);

			if (!(object.Equals(default(ObjektIdentScnapscusType), ObjektIdentScnapscus)))
			{
				AingangUnglaicDefaultZuZaitMitIndexLezte = ScnapscusZuZaitMitIndex;
			}

			ListeScnapscusZuZaitAingangBisherAnzaal = ScnapscusZuZaitMitIndex.Wert.Key;

			var ListeScnapscusZuZaitZuErhalteAnzaalScrankeMax = this.ListeScnapscusZuZaitZuErhalteAnzaalScrankeMax;

			if (ListeScnapscusZuZaitZuErhalteAnzaalScrankeMax.HasValue)
			{
				Bib3.Extension.ListeKürzeBegin(ListeScnapscusZuZait, ListeScnapscusZuZaitZuErhalteAnzaalScrankeMax.Value);
			}

			NaacAingangScnapscus(Zait, ObjektIdentScnapscus, ZuusazInfoScnapscus);
		}

		public ObjektIdentScnapscusType	AingangScnapscusTailObjektIdentLezteBerecne()
		{
			var AingangScnapscusLezte = this.AingangScnapscusLezteBerecne();

			if (!AingangScnapscusLezte.HasValue)
			{
				return default(ObjektIdentScnapscusType);
			}

			return AingangScnapscusLezte.Value.Key;
		}

		public ObjektIdentScnapscusType	AingangScnapscusTailObjektIdentVorLezteBerecne()
		{
			var AingangScnapscusVorLezte = this.AingangScnapscusVorLezteBerecne();

			if (!AingangScnapscusVorLezte.HasValue)
			{
				return default(ObjektIdentScnapscusType);
			}

			return AingangScnapscusVorLezte.Value.Key;
		}

		public KeyValuePair<ObjektIdentScnapscusType, SictZuusazInfoScnapscusType>? AingangScnapscusLezteBerecne()
		{
			var AingangScnapscusMitZaitLezte = this.AingangScnapscusMitZaitLezteBerecne();

			if (!AingangScnapscusMitZaitLezte.HasValue)
			{
				return null;
			}

			return AingangScnapscusMitZaitLezte.Value.Wert;
		}

		public KeyValuePair<ObjektIdentScnapscusType, SictZuusazInfoScnapscusType>? AingangScnapscusVorLezteBerecne()
		{
			var AingangScnapscusMitZaitVorLezte = this.AingangScnapscusMitZaitVorLezteBerecne();

			if (!AingangScnapscusMitZaitVorLezte.HasValue)
			{
				return null;
			}

			return AingangScnapscusMitZaitVorLezte.Value.Wert;
		}

		public	ObjektIdentScnapscusType	AingangScnapscusUnglaicDefaultTailObjektIdentLezteBerecne()
		{
			var AingangScnapscusUnglaicDefaultLezte = this.AingangScnapscusUnglaicDefaultLezteBerecne();

			if (!AingangScnapscusUnglaicDefaultLezte.HasValue)
			{
				return default(ObjektIdentScnapscusType);
			}

			return AingangScnapscusUnglaicDefaultLezte.Value.Key;
		}

		public KeyValuePair<ObjektIdentScnapscusType, SictZuusazInfoScnapscusType>? AingangScnapscusUnglaicDefaultLezteBerecne()
		{
			var AingangScnapscusUnglaicDefaultMitZaitLezte = this.AingangScnapscusUnglaicDefaultMitZaitLezteBerecne();

			if (!AingangScnapscusUnglaicDefaultMitZaitLezte.HasValue)
			{
				return null;
			}

			return AingangScnapscusUnglaicDefaultMitZaitLezte.Value.Wert;
		}

		static public SictWertMitZait<ObjektIdentScnapscusType>?
			AingangScnapscusTailObjektIdentMitZaitLezteBerecne<AbgelaiteteType>(
			AbgelaiteteType ObjektZuusctandAggr)
			where AbgelaiteteType : SictZuObjektMengeScnapscusZuZaitAggr<ObjektIdentScnapscusType, SictScnapscusAlsFortsazZuuläsigType, SictZuusazInfoScnapscusType>
		{
			var AingangScnapscusMitZaitLezte = AingangScnapscusMitZaitLezteBerecne(ObjektZuusctandAggr);

			if (!AingangScnapscusMitZaitLezte.HasValue)
			{
				return null;
			}

			return	new	SictWertMitZait<ObjektIdentScnapscusType>(AingangScnapscusMitZaitLezte.Value.Zait,	AingangScnapscusMitZaitLezte.Value.Wert.Key);
		}

		static public SictWertMitZait<KeyValuePair<ObjektIdentScnapscusType, SictZuusazInfoScnapscusType>>? AingangScnapscusMitZaitLezteBerecne<AbgelaiteteType>(
			AbgelaiteteType	ObjektZuusctandAggr)
			where AbgelaiteteType : SictZuObjektMengeScnapscusZuZaitAggr<ObjektIdentScnapscusType, SictScnapscusAlsFortsazZuuläsigType,	SictZuusazInfoScnapscusType>
		{
			if(null	== ObjektZuusctandAggr)
			{
				return	null;
			}

			return ObjektZuusctandAggr.AingangScnapscusMitZaitLezteBerecne();
		}

		public SictWertMitZait<ObjektIdentScnapscusType>? AingangScnapscusTailObjektIdentMitZaitVorLezteBerecne()
		{
			var AingangScnapscusMitZaitVorLezte = AingangScnapscusMitZaitVorLezteBerecne();

			if (!AingangScnapscusMitZaitVorLezte.HasValue)
			{
				return null;
			}

			return new SictWertMitZait<ObjektIdentScnapscusType>(AingangScnapscusMitZaitVorLezte.Value.Zait, AingangScnapscusMitZaitVorLezte.Value.Wert.Key);
		}

		public SictWertMitZait<ObjektIdentScnapscusType>? AingangScnapscusTailObjektIdentMitZaitLezteBerecne()
		{
			var AingangScnapscusMitZaitLezte = AingangScnapscusMitZaitLezteBerecne();

			if (!AingangScnapscusMitZaitLezte.HasValue)
			{
				return null;
			}

			return new SictWertMitZait<ObjektIdentScnapscusType>(AingangScnapscusMitZaitLezte.Value.Zait, AingangScnapscusMitZaitLezte.Value.Wert.Key);
		}

		public SictWertMitZait<KeyValuePair<ObjektIdentScnapscusType, SictZuusazInfoScnapscusType>>? AingangScnapscusMitZaitLezteBerecne()
		{
			var ListeScnapscusZuZait = this.ListeScnapscusZuZait;

			if (ListeScnapscusZuZait.Count < 1)
			{
				return null;
			}

			return ListeScnapscusZuZait.Last();
		}

		public SictWertMitZait<KeyValuePair<ObjektIdentScnapscusType, SictZuusazInfoScnapscusType>>? AingangScnapscusUnglaicDefaultMitZaitLezteBerecne()
		{
			return this.AingangUnglaicDefaultZuZaitLezte;
		}

		public SictWertMitZait<ObjektIdentScnapscusType>? AingangScnapscusUnglaicDefaultTailObjektIdentMitZaitLezteBerecne()
		{
			var AingangScnapscusUnglaicDefaultMitZaitLezte = AingangScnapscusUnglaicDefaultMitZaitLezteBerecne();

			if (!AingangScnapscusUnglaicDefaultMitZaitLezte.HasValue)
			{
				return null;
			}

			return new	SictWertMitZait<ObjektIdentScnapscusType>(AingangScnapscusUnglaicDefaultMitZaitLezte.Value.Zait, AingangScnapscusUnglaicDefaultMitZaitLezte.Value.Wert.Key);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="AlterScritAnzaal">0 = Lezte Scnapscus</param>
		/// <returns></returns>
		public SictWertMitZait<KeyValuePair<ObjektIdentScnapscusType, SictZuusazInfoScnapscusType>>? AingangScnapscusMitAlterScritAnzaalBerecne(
			int	AlterScritAnzaal)
		{
			var ListeScnapscusZuZait = this.ListeScnapscusZuZait;

			return ListeScnapscusZuZait.Cast<SictWertMitZait<KeyValuePair<ObjektIdentScnapscusType, SictZuusazInfoScnapscusType>>?>().Reversed().ElementAtOrDefault(AlterScritAnzaal);
		}

		public SictWertMitZait<KeyValuePair<ObjektIdentScnapscusType, SictZuusazInfoScnapscusType>>? AingangScnapscusMitZaitVorLezteBerecne()
		{
			return AingangScnapscusMitAlterScritAnzaalBerecne(1);
		}

		virtual protected void NaacAingangScnapscus(
			Int64	ScnapscusZait,
			ObjektIdentScnapscusType	ScnapscusWert,
			SictZuusazInfoScnapscusType ZuusazInfoScnapscus)
		{
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictZuGbsObjektZuusctandMitIdentPerLaageMitZuusazInfo<GbsObjektScnapscusType, SictZuusazInfoScnapscusType> :
		SictZuObjektMengeScnapscusZuZaitAggr<GbsObjektScnapscusType, SictScnapscusAlsFortsazZuuläsigFürObjektMitLaage<GbsObjektScnapscusType>, SictZuusazInfoScnapscusType>
		where GbsObjektScnapscusType : GbsElement
	{
		public Int64? ScnapscusLezteGbsAstHerkunftAdrese
		{
			get
			{
				var AingangScnapscusUnglaicDefaultTailObjektIdentLezte = AingangScnapscusUnglaicDefaultTailObjektIdentLezteBerecne();

				if (null == AingangScnapscusUnglaicDefaultTailObjektIdentLezte)
				{
					return null;
				}

				return AingangScnapscusUnglaicDefaultTailObjektIdentLezte.Ident;
			}
		}

		public bool InLezteScnapscusSictbar()
		{
			return 0 == SaitAingangUnglaicDefaultLezteListeAingangAnzaal();
		}

		public SictZuGbsObjektZuusctandMitIdentPerLaageMitZuusazInfo()
		{
		}

		public SictZuGbsObjektZuusctandMitIdentPerLaageMitZuusazInfo(
			int? ListeScnapscusZuZaitZuErhalteAnzaalScrankeMax)
			:
			base(ListeScnapscusZuZaitZuErhalteAnzaalScrankeMax)
		{
		}

		public SictZuGbsObjektZuusctandMitIdentPerLaageMitZuusazInfo(
			int? ListeScnapscusZuZaitZuErhalteAnzaalScrankeMax,
			Int64 Zait,
			GbsObjektScnapscusType GbsObjektScnapscus,
			SictZuusazInfoScnapscusType	ZuusazInfoScnapscus	= default(SictZuusazInfoScnapscusType))
			:
			base(
			ListeScnapscusZuZaitZuErhalteAnzaalScrankeMax,
			Zait,
			GbsObjektScnapscus,
			ZuusazInfoScnapscus)
		{
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictZuGbsObjektZuusctandMitIdentPerIntMitZuusazInfo<GbsObjektScnapscusType, SictZuusazInfoScnapscusType> :
		SictZuObjektMengeScnapscusZuZaitAggr<GbsObjektScnapscusType, SictScnapscusAlsFortsazZuuläsigFürObjektMitBezaicnerInt<GbsObjektScnapscusType>, SictZuusazInfoScnapscusType>
		where GbsObjektScnapscusType : ObjektMitIdentInt64
	{
		public Int64? GbsAstHerkunftAdrese
		{
			get
			{
				var AingangScnapscusUnglaicDefaultTailObjektIdentLezte = AingangScnapscusUnglaicDefaultTailObjektIdentLezteBerecne();

				if (null == AingangScnapscusUnglaicDefaultTailObjektIdentLezte)
				{
					return null;
				}

				return AingangScnapscusUnglaicDefaultTailObjektIdentLezte.Ident;
			}
		}

		public bool InLezteScnapscusSictbar()
		{
			return 0 == SaitAingangUnglaicDefaultLezteListeAingangAnzaal();
		}

		virtual public GbsElement FläceFürMenuWurzelBerecne()
		{
			var ScnapscusLezte = AingangScnapscusTailObjektIdentLezteBerecne();

			if (null == ScnapscusLezte)
			{
				return null;
			}

			var ScnapscusLezteScpez = ScnapscusLezte as GbsElement;

			if (null != ScnapscusLezteScpez)
			{
				var ScnapscusLezteScpezFläceMenu = ScnapscusLezteScpez.FläceMenuWurzelBerecne();

				if (null != ScnapscusLezteScpezFläceMenu)
				{
					return ScnapscusLezteScpezFläceMenu;
				}
			}

			return ScnapscusLezteScpez;
		}

		public SictZuGbsObjektZuusctandMitIdentPerIntMitZuusazInfo()
		{
		}

		public SictZuGbsObjektZuusctandMitIdentPerIntMitZuusazInfo(
			int? ListeScnapscusZuZaitZuErhalteAnzaalScrankeMax)
			:
			base(ListeScnapscusZuZaitZuErhalteAnzaalScrankeMax)
		{
		}

		public SictZuGbsObjektZuusctandMitIdentPerIntMitZuusazInfo(
			int? ListeScnapscusZuZaitZuErhalteAnzaalScrankeMax,
			Int64 Zait,
			GbsObjektScnapscusType GbsObjektScnapscus,
			SictZuusazInfoScnapscusType	ZuusazInfoScnapscus	= default(SictZuusazInfoScnapscusType))
			:
			base(
			ListeScnapscusZuZaitZuErhalteAnzaalScrankeMax,
			Zait,
			GbsObjektScnapscus,
			ZuusazInfoScnapscus)
		{
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictZuGbsObjektZuusctandMitIdentPerInt<GbsObjektScnapscusType> :
		SictZuGbsObjektZuusctandMitIdentPerIntMitZuusazInfo<GbsObjektScnapscusType, int>
		where GbsObjektScnapscusType : ObjektMitIdentInt64
	{
		public SictZuGbsObjektZuusctandMitIdentPerInt()
		{
		}

		public SictZuGbsObjektZuusctandMitIdentPerInt(
			int? ListeScnapscusZuZaitZuErhalteAnzaalScrankeMax)
			:
			base(ListeScnapscusZuZaitZuErhalteAnzaalScrankeMax)
		{
		}

		public SictZuGbsObjektZuusctandMitIdentPerInt(
			int? ListeScnapscusZuZaitZuErhalteAnzaalScrankeMax,
			Int64 Zait,
			GbsObjektScnapscusType GbsObjektScnapscus)
			:
			base(
			ListeScnapscusZuZaitZuErhalteAnzaalScrankeMax,
			Zait,
			GbsObjektScnapscus,
			0)
		{
		}

		override	protected	void	NaacAingangScnapscus(
			Int64 ScnapscusZait,
			GbsObjektScnapscusType ScnapscusWert,
			int	t)
		{
			NaacAingangScnapscus(ScnapscusZait, ScnapscusWert);
		}

		virtual protected void NaacAingangScnapscus(
			Int64 ScnapscusZait,
			GbsObjektScnapscusType ScnapscusWert)
		{
		}
	}
}
