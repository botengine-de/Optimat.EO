using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Optimat.EveOnline.Anwendung
{
	[JsonObject(MemberSerialization.OptIn)]
	public struct SictScpaicerTransitioonNaacHasValue<T>
		where T : struct
	{
		[JsonProperty]
		public Nullable<T> AingangLezte;

		[JsonProperty]
		public Nullable<T> AingangHasValueLezte;

		[JsonProperty]
		public Nullable<T> AingangTransitioonNaacHasValueLezte;

		public void AingangWert(Nullable<T> WertNoi)
		{
			var WertVorher = this.AingangLezte;

			this.AingangLezte = WertNoi;

			if (WertNoi.HasValue)
			{
				AingangHasValueLezte = WertNoi;

				if (!WertVorher.HasValue)
				{
					AingangTransitioonNaacHasValueLezte = WertNoi;
				}
			}
		}

		public SictScpaicerTransitioonNaacHasValue(Nullable<T> WertNoi)
			:
			this()
		{
			this.AingangWert(WertNoi);
		}

		public static explicit operator Nullable<T>(SictScpaicerTransitioonNaacHasValue<T> value)
		{
			return value.AingangLezte;
		}

		public static implicit operator SictScpaicerTransitioonNaacHasValue<T>(Nullable<T> value)
		{
			return new SictScpaicerTransitioonNaacHasValue<T>(value);
		}

		public static implicit operator SictScpaicerTransitioonNaacHasValue<T>(T value)
		{
			return new SictScpaicerTransitioonNaacHasValue<T>(value);
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictBeobacterTransitioonRef<T>
	{
		[JsonProperty]
		readonly public bool AingangFilterSctrengMonotoonSctaigend;

		[JsonProperty]
		public int? ListeTransitioonAnzaalScrankeMax
		{
			set;
			get;
		}

		[JsonProperty]
		Queue<SictWertMitZait<KeyValuePair<T, T>>> ListeTransitioonZaitWertVorherWertNaacher
		{
			set;
			get;
		}

		[JsonProperty]
		public	SictWertMitZait<T>?	AingangLezteZaitUndWert
		{
			private	set;
			get;
		}

		[JsonProperty]
		public Int64? SaitTransitionLezteListeAingangAnzaal
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? ListeAingangAnzaal
		{
			private set;
			get;
		}

		public SictBeobacterTransitioonRef()
		{
		}

		public SictBeobacterTransitioonRef(
			bool AingangFilterSctrengMonotoonSctaigend,
			int? ListeTransitioonAnzaalScrankeMax	= null)
		{
			this.AingangFilterSctrengMonotoonSctaigend = AingangFilterSctrengMonotoonSctaigend;
			this.ListeTransitioonAnzaalScrankeMax = ListeTransitioonAnzaalScrankeMax;
		}

		public void AingangWertZuZait(Int64 Zait, T Wert)
		{
			var BisherAingangLezteWert = default(T);

			var TransitioonFüügeAin = false;

			{
				var BisherAingangLezteZaitUndWertNulbar = this.AingangLezteZaitUndWert;

				if (BisherAingangLezteZaitUndWertNulbar.HasValue)
				{
					if (AingangFilterSctrengMonotoonSctaigend)
					{
						if (Zait <= BisherAingangLezteZaitUndWertNulbar.Value.Zait)
						{
							//	Aingang werd verworfe wail Argument für Ordnung ("Zait") nit grööser als vorheriger Aingang.
							return;
						}
					}

					BisherAingangLezteWert = BisherAingangLezteZaitUndWertNulbar.Value.Wert;

					if (!object.Equals(BisherAingangLezteWert, Wert))
					{
						TransitioonFüügeAin = true;
					}
				}
				else
				{
					TransitioonFüügeAin = true;
				}
			}

			if (TransitioonFüügeAin)
			{
				SaitTransitionLezteListeAingangAnzaal = 0;

				var ListeTransitioonAnzaalScrankeMax = this.ListeTransitioonAnzaalScrankeMax;
				var ListeTransitioonZaitWertVorherWertNaacher = this.ListeTransitioonZaitWertVorherWertNaacher;

				if (null == ListeTransitioonZaitWertVorherWertNaacher)
				{
					this.ListeTransitioonZaitWertVorherWertNaacher = ListeTransitioonZaitWertVorherWertNaacher =
						new Queue<SictWertMitZait<KeyValuePair<T, T>>>();
				}

				ListeTransitioonZaitWertVorherWertNaacher.Enqueue(
					new SictWertMitZait<KeyValuePair<T, T>>(Zait, new KeyValuePair<T, T>(BisherAingangLezteWert, Wert)));

				if (ListeTransitioonAnzaalScrankeMax.HasValue)
				{
					while (ListeTransitioonAnzaalScrankeMax.Value < ListeTransitioonZaitWertVorherWertNaacher.Count)
					{
						ListeTransitioonZaitWertVorherWertNaacher.Dequeue();
					}
				}
			}
			else
			{
				this.SaitTransitionLezteListeAingangAnzaal = (this.SaitTransitionLezteListeAingangAnzaal ?? 0) + 1;
			}

			this.ListeAingangAnzaal = (this.ListeAingangAnzaal ?? 0) + 1;

			this.AingangLezteZaitUndWert = new SictWertMitZait<T>(Zait, Wert);
		}

		public Int64? ZuWertBeginZait(T Wert)
		{
			var AingangLezteZaitUndWert = this.AingangLezteZaitUndWert;
			var ListeTransitioonZaitWertVorherWertNaacher = this.ListeTransitioonZaitWertVorherWertNaacher;

			if (!AingangLezteZaitUndWert.HasValue)
			{
				return null;
			}

			if (!object.Equals(AingangLezteZaitUndWert.Value.Wert, Wert))
			{
				//	Wert isc unglaic zulezt aingegangene.
				return null;
			}

			var BisherFrüheste = AingangLezteZaitUndWert.Value.Zait;

			if (null == ListeTransitioonZaitWertVorherWertNaacher)
			{
				return BisherFrüheste;
			}

			foreach (var Transitioon in ListeTransitioonZaitWertVorherWertNaacher.Reverse())
			{
				if (!object.Equals(Transitioon.Wert.Value, Wert))
				{
					break;
				}

				BisherFrüheste = Transitioon.Zait;
			}

			return BisherFrüheste;
		}

		public Int64? ZuWertLezteZait(T Wert)
		{
			var AingangLezteZaitUndWert = this.AingangLezteZaitUndWert;
			var ListeTransitioonZaitWertVorherWertNaacher = this.ListeTransitioonZaitWertVorherWertNaacher;

			if (AingangLezteZaitUndWert.HasValue)
			{
				if (object.Equals(AingangLezteZaitUndWert.Value.Wert, Wert))
				{
					return AingangLezteZaitUndWert.Value.Zait;
				}
			}

			if (null == ListeTransitioonZaitWertVorherWertNaacher)
			{
				return null;
			}

			foreach (var Transitioon in ListeTransitioonZaitWertVorherWertNaacher.Reverse())
			{
				if (object.Equals(Transitioon.Wert.Key,Wert))
				{
					return Transitioon.Zait - 1;
				}
			}

			return null;
		}

		static public TVal? AingangLezteWertNulbar<TVal>(SictBeobacterTransitioonRef<TVal> Beobacter)
			where TVal : struct
		{
			if (null == Beobacter)
			{
				return null;
			}

			var AingangLezteZaitUndWert = Beobacter.AingangLezteZaitUndWert;

			if (!AingangLezteZaitUndWert.HasValue)
			{
				return null;
			}

			return AingangLezteZaitUndWert.Value.Wert;
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictBeobacterTransitioonBoolRef : SictBeobacterTransitioonRef<bool>
	{
		public SictBeobacterTransitioonBoolRef()
		{
		}

		public SictBeobacterTransitioonBoolRef(
			bool AingangFilterSctrengMonotoonSctaigend,
			int? ListeTransitioonAnzaalScrankeMax	= null)
			:
			base(
			AingangFilterSctrengMonotoonSctaigend,
			ListeTransitioonAnzaalScrankeMax)
		{
		}

		public Int64? WertFalseLezteZaitBerecne()
		{
			return ZuWertLezteZait(false);
		}

		public Int64? WertTrueLezteZaitBerecne()
		{
			return ZuWertLezteZait(true);
		}

		public Int64? WertFalseBeginZaitBerecne()
		{
			return ZuWertBeginZait(false);
		}

		public Int64? WertTrueBeginZaitBerecne()
		{
			return ZuWertBeginZait(true);
		}

		public void AingangTransitionZuZait(
			Int64 ZaitMili,
			bool	TransitionNaacFalse,
			bool	TransitionNaacTrue,
			bool	WertInitial	= false)
		{
			var WertVorher = AingangLezteWertNulbar(this) ?? WertInitial;

			var WertNaacher = WertVorher;

			if (TransitionNaacTrue)
			{
				WertNaacher = true;
			}

			if (TransitionNaacFalse)
			{
				WertNaacher = false;
			}

			AingangWertZuZait(ZaitMili, WertNaacher);
		}
	}

}
