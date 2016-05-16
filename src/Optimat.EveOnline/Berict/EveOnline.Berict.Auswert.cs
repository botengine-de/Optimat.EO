using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bib3;
using Newtonsoft.Json;

namespace Optimat.EveOnline.Berict.Auswert
{
	public interface IBerictInspektDaatenKwele
	{
		byte[] AnwendungSizungIdentBerecne();

		SictOptimatScrit ZuScritIndexScritInfoBerecne(int OptimatScritIndex);

		IEnumerable<KeyValuePair<Int64, SictOptimatScrit>> ListeZuVonProcessLeeseBeginZaitScritInfoBerecne();

		IEnumerable<KeyValuePair<Int64, SictWertMitZait<SictMissionZuusctand>>> MengeMissionIdentUndZuusctandLezteBerecne();

		SictOptimatScrit ZuNuzerZaitMiliScritInfoNääxte(Int64 SuuceUrscprungNuzerZaitMili);

		IEnumerable<KeyValuePair<SictOptimatScrit, int>> ZuNuzerZaitMiliListeScritInfoNääxte(
			Int64 SuuceUrscprungNuzerZaitMili,
			int RükwärtsListeScnapscusAnzaal = 0,
			int VorwärtsListeScnapscusAnzaal = 0,
			bool ScnapscusAutomaatZuusctandLaade = false);

		Int64? BerictBeginNuzerZaitMiliBerecne();

		Int64? BerictEndeNuzerZaitMiliBerecne();

		Int64? BerictDauerMiliBerecne();

		Int64? ZuAnwendungZaitMiliBerecneNuzerZaitMili(Int64 AnwendungZait);

		Int64? ZuEveOnlineClientClockSekundeBerecneNuzerZaitMili(int InTaagSekunde);
		int? ZuNuzerZaitMiliBerecneEveOnlineClientClockSekunde(Int64 NuzerZaitMili);

		SictWertMitZait<SictDataiIdentUndSuuceHinwais> ZuNuzerZaitMiliWindowClientRasterSuuceHinwaisNääxte(Int64 NuzerZaitMili);
	}

	public abstract class SictBerictInspektDaatenKweleImpl : IBerictInspektDaatenKwele
	{
		abstract public byte[] AnwendungSizungIdentBerecne();

		abstract public SictOptimatScrit ZuScritIndexScritInfoBerecne(int OptimatScritIndex);

		abstract public IEnumerable<KeyValuePair<Int64, SictOptimatScrit>> ListeZuVonProcessLeeseBeginZaitScritInfoBerecne();

		abstract public IEnumerable<KeyValuePair<Int64, SictWertMitZait<SictMissionZuusctand>>> MengeMissionIdentUndZuusctandLezteBerecne();

		abstract public SictOptimatScrit ZuNuzerZaitMiliScritInfoNääxte(Int64 SuuceUrscprungNuzerZaitMili);

		abstract public IEnumerable<KeyValuePair<SictOptimatScrit, int>> ZuNuzerZaitMiliListeScritInfoNääxte(
			Int64 SuuceUrscprungNuzerZaitMili,
			int RükwärtsListeScnapscusAnzaal = 0,
			int VorwärtsListeScnapscusAnzaal = 0,
			bool ScnapscusAutomaatZuusctandLaade = false);

		abstract public Int64? BerictBeginNuzerZaitMiliBerecne();

		abstract public Int64? BerictEndeNuzerZaitMiliBerecne();

		abstract public Int64? BerictDauerMiliBerecne();

		abstract public Int64? ZuAnwendungZaitMiliBerecneNuzerZaitMili(Int64 AnwendungZait);

		abstract public SictWertMitZait<SictDataiIdentUndSuuceHinwais> ZuNuzerZaitMiliWindowClientRasterSuuceHinwaisNääxte(Int64 NuzerZaitMili);

		public void ZuMengeScritInfoCallback(Action<SictOptimatScrit> Callback)
		{
			if (null == Callback)
			{
				return;
			}

			var ScritIndex = 0;

			while (true)
			{
				var ScritInfo = ZuScritIndexScritInfoBerecne(ScritIndex);

				if (null == ScritInfo)
				{
					return;
				}

				Callback(ScritInfo);

				++ScritIndex;
			}
		}

		public IEnumerable<KeyValuePair<int, Int64>> EveOnlineClientClockMengeTransitionBerecneInTaagSekundeUndNuzerZaitMili(
			int TransitionFilterMengeScritAnzaalScranke = 0)
		{
			var ListeWertVorherig = new Queue<int>();

			var ListeTransition = new List<KeyValuePair<int, Int64>>();

			var ScritInfoCallback = new Action<SictOptimatScrit>((ScritInfo) =>
				{
					if (null == ScritInfo)
					{
						return;
					}

					var ScritNuzerZaitMili = (Int64?)ScritInfo.NuzerZait;

					if (!ScritNuzerZaitMili.HasValue)
					{
						return;
					}

					var NaacNuzerBerictAutomaatZuusctand = ScritInfo.NaacNuzerBerictAutomaatZuusctand	as	SictVonOptimatMeldungZuusctand;

					if (null == NaacNuzerBerictAutomaatZuusctand)
					{
						return;
					}

					var ScritEveOnlineZaitKalenderModuloTaagMinMax = NaacNuzerBerictAutomaatZuusctand.EveOnlineZaitKalenderModuloTaagMinMax;

					if (!ScritEveOnlineZaitKalenderModuloTaagMinMax.HasValue)
					{
						return;
					}

					var ScritEveOnlineClientClockWert = ScritEveOnlineZaitKalenderModuloTaagMinMax.Value.Key;

					try
					{
						var TransitionFilterMengeScritAnzaalScrankeErfült = false;

						if (0 < ListeTransition.Count)
						{
							var ListeTransitionLezte = ListeTransition.LastOrDefault();

							if (ListeTransitionLezte.Key	== ScritEveOnlineClientClockWert)
							{
								//	Wert unverändert.
								return;
							}

							if (TransitionFilterMengeScritAnzaalScranke < 1)
							{
								TransitionFilterMengeScritAnzaalScrankeErfült = true;
							}
							else
							{
								if (ListeWertVorherig.Reverse().Take(TransitionFilterMengeScritAnzaalScranke).All((WertVorherig) => WertVorherig == ScritEveOnlineClientClockWert))
								{
									TransitionFilterMengeScritAnzaalScrankeErfült = true;
								}
							}
						}
						else
						{
							TransitionFilterMengeScritAnzaalScrankeErfült = true;
						}

						if (TransitionFilterMengeScritAnzaalScrankeErfült)
						{
							ListeTransition.Add(new KeyValuePair<int, Int64>(
								ScritEveOnlineClientClockWert, ScritNuzerZaitMili.Value));
						}
					}
					finally
					{
						ListeWertVorherig.Enqueue(ScritEveOnlineClientClockWert);
					}
				});

			ZuMengeScritInfoCallback(ScritInfoCallback);

			return ListeTransition;
		}

		public Int64? ZuEveOnlineClientClockSekundeBerecneNuzerZaitMili(int InTaagSekunde)
		{
			var EveOnlineClientClockMengeTransitionInTaagSekundeUndNuzerZaitMili = EveOnlineClientClockMengeTransitionBerecneInTaagSekundeUndNuzerZaitMili(3);

			for(int	TransitionIndex	= 0; TransitionIndex < EveOnlineClientClockMengeTransitionInTaagSekundeUndNuzerZaitMili.Count(); ++TransitionIndex)
			{
				var	TransitionInTaagSekundeUndNuzerZaitMili	= EveOnlineClientClockMengeTransitionInTaagSekundeUndNuzerZaitMili.ElementAtOrDefault(TransitionIndex);
				var	FolgendeTransitionInTaagSekundeUndNuzerZaitMili	= EveOnlineClientClockMengeTransitionInTaagSekundeUndNuzerZaitMili.ElementAtOrDefault(TransitionIndex + 1);

				var DistanzZuTransition = InTaagSekunde - TransitionInTaagSekundeUndNuzerZaitMili.Key;
				var DistanzZuFolgendeTransition = InTaagSekunde - FolgendeTransitionInTaagSekundeUndNuzerZaitMili.Key;

				var DistanzNuzerZaitMili = FolgendeTransitionInTaagSekundeUndNuzerZaitMili.Value - TransitionInTaagSekundeUndNuzerZaitMili.Value;

				if (0 <= DistanzZuTransition)
				{
					if (EveOnlineClientClockMengeTransitionInTaagSekundeUndNuzerZaitMili.Count() <= TransitionIndex + 1)
					{
						//	Wert liigt hinter lezte Transitioon.

						return TransitionInTaagSekundeUndNuzerZaitMili.Value + DistanzZuTransition * 1000;
					}
					else
					{
						if (DistanzZuFolgendeTransition <= 0)
						{
							//	Interpolatioon.

							return TransitionInTaagSekundeUndNuzerZaitMili.Value
								+ (DistanzNuzerZaitMili * DistanzZuTransition) / Math.Max(1, DistanzZuTransition - DistanzZuFolgendeTransition);
						}
					}
				}
			}

			return null;
		}

		public int? ZuNuzerZaitMiliBerecneEveOnlineClientClockSekunde(Int64 NuzerZaitMili)
		{
			var EveOnlineClientClockMengeTransitionInTaagSekundeUndNuzerZaitMili = EveOnlineClientClockMengeTransitionBerecneInTaagSekundeUndNuzerZaitMili(3);

			for (int TransitionIndex = 0; TransitionIndex < EveOnlineClientClockMengeTransitionInTaagSekundeUndNuzerZaitMili.Count(); ++TransitionIndex)
			{
				var TransitionInTaagSekundeUndNuzerZaitMili = EveOnlineClientClockMengeTransitionInTaagSekundeUndNuzerZaitMili.ElementAtOrDefault(TransitionIndex);
				var FolgendeTransitionInTaagSekundeUndNuzerZaitMili = EveOnlineClientClockMengeTransitionInTaagSekundeUndNuzerZaitMili.ElementAtOrDefault(TransitionIndex + 1);

				var DistanzZuTransition = NuzerZaitMili - TransitionInTaagSekundeUndNuzerZaitMili.Value;
				var DistanzZuFolgendeTransition = NuzerZaitMili - FolgendeTransitionInTaagSekundeUndNuzerZaitMili.Value;

				var DistanzEveOnlineClientClock = FolgendeTransitionInTaagSekundeUndNuzerZaitMili.Key - TransitionInTaagSekundeUndNuzerZaitMili.Key;

				if (0 <= DistanzZuTransition)
				{
					if (EveOnlineClientClockMengeTransitionInTaagSekundeUndNuzerZaitMili.Count() <= TransitionIndex + 1)
					{
						//	Wert liigt hinter lezte Transitioon.

						return	(int)(TransitionInTaagSekundeUndNuzerZaitMili.Key + DistanzZuTransition / 1000);
					}
					else
					{
						if (DistanzZuFolgendeTransition <= 0)
						{
							//	Interpolatioon.

							return (int)(TransitionInTaagSekundeUndNuzerZaitMili.Key
								+ (DistanzEveOnlineClientClock * DistanzZuTransition) / Math.Max(1, DistanzZuTransition - DistanzZuFolgendeTransition));
						}
					}
				}
			}

			return null;
		}
	}

	public class SictBerictInspektDaatenKweleAusScpaicer : SictBerictInspektDaatenKweleImpl
	{
		readonly object Lock = new object();

		byte[] AnwendungSizungIdent;

		public Int64? BerictBeginNuzerZaitMili
		{
			protected set;
			get;
		}

		public Int64? BerictEndeNuzerZaitMili
		{
			protected set;
			get;
		}

		readonly List<KeyValuePair<Int64, SictOptimatScrit>> ListeZuVonProcessLeeseBeginZaitScritInfo = new List<KeyValuePair<Int64, SictOptimatScrit>>();

		readonly Dictionary<Int64, Int64> DictZuNuzerZaitMiliAnwendungZaitMili = new Dictionary<Int64, Int64>();

		/// <summary>
		/// Lezte berictete Zuusctand zu Mission. Werd nii geleert.
		/// </summary>
		readonly Dictionary<Int64, SictWertMitZait<SictMissionZuusctand>> DictZuMissionBezaicnerMissionZuusctandLezte =
			new Dictionary<Int64, SictWertMitZait<SictMissionZuusctand>>();

		/// <summary>
		/// Suuce Hinwais wii von Nuzer berictet. Werd nii geleert.
		/// </summary>
		readonly Dictionary<Int64, SictDataiIdentUndSuuceHinwais> DictVonNuzerZaitMiliWindowClientRasterSuuceHinwais =
			new Dictionary<Int64, SictDataiIdentUndSuuceHinwais>();

		readonly List<Int64> ListeZaitpunktOptimatScritOoneWindowClientRasterIdent = new List<Int64>();

		readonly Dictionary<Int64, SictNaacProcessWirkung> DictVonVorsclaagWirkungIdentZuNaacProcessWirkung =
			new Dictionary<Int64, SictNaacProcessWirkung>();

		readonly List<Int64> ListeZaitpunktOptimatScritOoneNaacProcessWirkung = new List<Int64>();

		override public byte[] AnwendungSizungIdentBerecne()
		{
			return this.AnwendungSizungIdent.ToArray();
		}

		override public Int64? BerictBeginNuzerZaitMiliBerecne()
		{
			return BerictBeginNuzerZaitMili;
		}

		override public Int64? BerictEndeNuzerZaitMiliBerecne()
		{
			return BerictEndeNuzerZaitMili;
		}

		override public Int64? BerictDauerMiliBerecne()
		{
			return BerictEndeNuzerZaitMiliBerecne() - BerictBeginNuzerZaitMiliBerecne();
		}

		override public IEnumerable<KeyValuePair<Int64, SictOptimatScrit>> ListeZuVonProcessLeeseBeginZaitScritInfoBerecne()
		{
			return ListeZuVonProcessLeeseBeginZaitScritInfo.ToArray();
		}

		override public SictOptimatScrit ZuScritIndexScritInfoBerecne(int OptimatScritIndex)
		{
			var ListeZuVonProcessLeeseBeginZaitScritInfo = this.ListeZuVonProcessLeeseBeginZaitScritInfo;

			if (null == ListeZuVonProcessLeeseBeginZaitScritInfo)
			{
				return null;
			}

			return ListeZuVonProcessLeeseBeginZaitScritInfo.ElementAtOrDefault(OptimatScritIndex).Value;
		}

		public SictDataiIdentUndSuuceHinwais ZuNuzerZaitMiliWindowClientRasterSuuceHinwaisBerecne(
			Int64 NuzerZaitMili)
		{
			return Optimat.Glob.TAD(DictVonNuzerZaitMiliWindowClientRasterSuuceHinwais, NuzerZaitMili);
		}

		override	public	SictWertMitZait<SictDataiIdentUndSuuceHinwais>	ZuNuzerZaitMiliWindowClientRasterSuuceHinwaisNääxte(
			Int64 NuzerZaitMili)
		{
			var BisherBeste = default(SictWertMitZait<SictDataiIdentUndSuuceHinwais>);

			if (null != DictVonNuzerZaitMiliWindowClientRasterSuuceHinwais)
			{
				foreach (var VonNuzerZaitMiliWindowClientRasterSuuceHinwais in DictVonNuzerZaitMiliWindowClientRasterSuuceHinwais)
				{
					var Distanz = VonNuzerZaitMiliWindowClientRasterSuuceHinwais.Key - NuzerZaitMili;
					var BisherBesteDistanz = BisherBeste.Zait - NuzerZaitMili;

					if (Math.Abs(Distanz) < Math.Abs(BisherBesteDistanz))
					{
						BisherBeste = new SictWertMitZait<SictDataiIdentUndSuuceHinwais>(VonNuzerZaitMiliWindowClientRasterSuuceHinwais.Key, VonNuzerZaitMiliWindowClientRasterSuuceHinwais.Value);
					}
				}
			}

			return BisherBeste;
		}

		override public Int64? ZuAnwendungZaitMiliBerecneNuzerZaitMili(Int64 AnwendungZaitMili)
		{
			var DictZuNuzerZaitMiliAnwendungZaitMili = this.DictZuNuzerZaitMiliAnwendungZaitMili;

			if (null == DictZuNuzerZaitMiliAnwendungZaitMili)
			{
				return null;
			}

			var ListeZuNuzerZaitMiliAnwendungZaitMiliOrdnetNaacDistanz =
				DictZuNuzerZaitMiliAnwendungZaitMili
				.OrderBy((ZuNuzerZaitMiliAnwendungZaitMili) => Math.Abs(ZuNuzerZaitMiliAnwendungZaitMili.Value - AnwendungZaitMili))
				.ToArray();

			if (ListeZuNuzerZaitMiliAnwendungZaitMiliOrdnetNaacDistanz.Length < 1)
			{
				return null;
			}

			var NääxteZuNuzerZaitMiliAnwendungZaitMili =
				ListeZuNuzerZaitMiliAnwendungZaitMiliOrdnetNaacDistanz.FirstOrDefault();

			return NääxteZuNuzerZaitMiliAnwendungZaitMili.Key + (AnwendungZaitMili - NääxteZuNuzerZaitMiliAnwendungZaitMili.Value);
		}

		override public SictOptimatScrit ZuNuzerZaitMiliScritInfoNääxte(Int64 SuuceUrscprungNuzerZaitMili)
		{
			var ZuNuzerZaitMiliListeScritInfoNääxte = this.ZuNuzerZaitMiliListeScritInfoNääxte(SuuceUrscprungNuzerZaitMili);

			if (null == ZuNuzerZaitMiliListeScritInfoNääxte)
			{
				return null;
			}

			return ZuNuzerZaitMiliListeScritInfoNääxte.FirstOrDefault((Kandidaat) => 0 == Kandidaat.Value).Key;
		}

		override public IEnumerable<KeyValuePair<SictOptimatScrit, int>> ZuNuzerZaitMiliListeScritInfoNääxte(
			Int64 SuuceUrscprungNuzerZaitMili,
			int RükwärtsListeScnapscusAnzaal = 0,
			int VorwärtsListeScnapscusAnzaal = 0,
			bool ScnapscusAutomaatZuusctandLaade = false)
		{
			lock (Lock)
			{
				return
					ExtractFromOldAssembly.Bib3.Extension.SelectNullable(
					Optimat.Glob.AusListeOrdnungWertListeElementNääxte(
					ListeZuVonProcessLeeseBeginZaitScritInfo,
					(ScritInfo) => ScritInfo.Key,
					SuuceUrscprungNuzerZaitMili,
					RükwärtsListeScnapscusAnzaal,
					VorwärtsListeScnapscusAnzaal),
					(t) => new KeyValuePair<SictOptimatScrit, int>(t.Key.Value, t.Value));
			}
		}

		override public IEnumerable<KeyValuePair<Int64, SictWertMitZait<SictMissionZuusctand>>> MengeMissionIdentUndZuusctandLezteBerecne()
		{
			return this.DictZuMissionBezaicnerMissionZuusctandLezte.ToArray();
		}

		protected void NuzerZaitMiliWindowClientRasterSuuceHinwaisFüügeAin(
			Int64 NuzerZaitMili,
			SictDataiIdentUndSuuceHinwais DataiIdentUndSuuceHinwais)
		{
			if (null == DataiIdentUndSuuceHinwais)
			{
				return;
			}

			DictVonNuzerZaitMiliWindowClientRasterSuuceHinwais[NuzerZaitMili] = DataiIdentUndSuuceHinwais;

			var MengeAingefüügt = ListeZaitpunktOptimatScritOoneWindowClientRasterIdent.Take(0).ToList();

			foreach (var ZaitpunktOptimatScritOoneWindowClientRasterIdent in ListeZaitpunktOptimatScritOoneWindowClientRasterIdent)
			{
				var OptimatScrit = ZuNuzerZaitMiliScritInfoNääxte(ZaitpunktOptimatScritOoneWindowClientRasterIdent);

				if (null == OptimatScrit)
				{
					continue;
				}

				var OptimatScritZaitDistanz = ZaitpunktOptimatScritOoneWindowClientRasterIdent - NuzerZaitMili;

				if (1111 < Math.Abs(OptimatScritZaitDistanz))
				{
					//	Zait Distanz zu wait
					continue;
				}

				OptimatScrit.ProcessWindowClientRasterIdentUndSuuceHinwais = DataiIdentUndSuuceHinwais;

				ZuScritInfoFüügeAin(OptimatScrit);

				MengeAingefüügt.Add(ZaitpunktOptimatScritOoneWindowClientRasterIdent);
			}

			foreach (var AingefüügtZait in MengeAingefüügt)
			{
				ListeZaitpunktOptimatScritOoneWindowClientRasterIdent.Remove(AingefüügtZait);
			}
		}

		public SictNaacProcessWirkung[] FürOptimatScritListeNaacProcessWirkungBerecne(
			SictOptimatScrit OptimatScrit,
			out	bool ListeNaacProcessWirkungGeändert,
			out	bool ListeNaacProcessWirkungVolsctändig)
		{
			ListeNaacProcessWirkungGeändert = false;

			if (null == OptimatScrit)
			{
				ListeNaacProcessWirkungVolsctändig = true;
				return null;
			}

			var VorsclaagListeWirkung = OptimatScrit.VorsclaagListeWirkung;

			if (null == VorsclaagListeWirkung)
			{
				ListeNaacProcessWirkungVolsctändig = true;
				return null;
			}

			var BisherNaacProcessListeWirkung = OptimatScrit.NaacProcessListeWirkung;

			var NaacProcessListeWirkungFeelendVorsclaagIdent = new List<Int64>();

			var NoiNaacProcessListeWirkung = new List<SictNaacProcessWirkung>();

			if (null != VorsclaagListeWirkung)
			{
				foreach (var KandidaatVorsclaagWirkung in VorsclaagListeWirkung)
				{
					if (null == KandidaatVorsclaagWirkung)
					{
						continue;
					}

					var KandidaatVorsclaagWirkungIdent = (Int64?)KandidaatVorsclaagWirkung.Ident;

					if (!KandidaatVorsclaagWirkungIdent.HasValue)
					{
						continue;
					}

					if (null != BisherNaacProcessListeWirkung)
					{
						var BisherNaacProcessWirkung =
							BisherNaacProcessListeWirkung.FirstOrDefault((Kandidaat) => (null == Kandidaat ? null : Kandidaat.VorsclaagWirkungIdent) == KandidaatVorsclaagWirkungIdent);

						if (null != BisherNaacProcessWirkung)
						{
							//	Zu diise Vorsclaag isc scun Wirkung aingetraage.
							continue;
						}
					}

					SictNaacProcessWirkung ZuVorsclaagNaacProcessWirkung = null;

					DictVonVorsclaagWirkungIdentZuNaacProcessWirkung.TryGetValue(KandidaatVorsclaagWirkungIdent.Value, out	ZuVorsclaagNaacProcessWirkung);

					if (null != ZuVorsclaagNaacProcessWirkung)
					{
						NoiNaacProcessListeWirkung.Add(ZuVorsclaagNaacProcessWirkung);
					}
					else
					{
						NaacProcessListeWirkungFeelendVorsclaagIdent.Add(KandidaatVorsclaagWirkungIdent.Value);
					}
				}
			}

			ListeNaacProcessWirkungVolsctändig = NaacProcessListeWirkungFeelendVorsclaagIdent.Count < 1;

			if (0 < NoiNaacProcessListeWirkung.Count)
			{
				ListeNaacProcessWirkungGeändert = true;

				return Bib3.Glob.ListeEnumerableAgregiirt(
					new IEnumerable<SictNaacProcessWirkung>[] { BisherNaacProcessListeWirkung, NoiNaacProcessListeWirkung }).ToArray();
			}
			else
			{
				return BisherNaacProcessListeWirkung;
			}
		}

		protected void VonNuzerMeldungNaacProcessWirkungFüügeAin(
			IEnumerable<SictNaacProcessWirkung> MengeNaacProcessWirkung)
		{
			if (null == MengeNaacProcessWirkung)
			{
				return;
			}

			foreach (var NaacProcessWirkung in MengeNaacProcessWirkung)
			{
				var VorsclaagWirkungIdent = NaacProcessWirkung.VorsclaagWirkungIdent;

				if (!VorsclaagWirkungIdent.HasValue)
				{
					return;
				}

				DictVonVorsclaagWirkungIdentZuNaacProcessWirkung[VorsclaagWirkungIdent.Value] = NaacProcessWirkung;
			}

			foreach (var ZaitpunktOptimatScritOoneNaacProcessWirkung in ListeZaitpunktOptimatScritOoneNaacProcessWirkung)
			{
				var OptimatScrit = ZuNuzerZaitMiliScritInfoNääxte(ZaitpunktOptimatScritOoneNaacProcessWirkung);

				if (null == OptimatScrit)
				{
					continue;
				}

				bool ListeNaacProcessWirkungGeändert;
				bool ListeNaacProcessWirkungVolsctändig;

				var OptimatScritListeNaacProcessWirkung = FürOptimatScritListeNaacProcessWirkungBerecne(
					OptimatScrit,
					out	ListeNaacProcessWirkungGeändert,
					out	ListeNaacProcessWirkungVolsctändig);

				if (ListeNaacProcessWirkungGeändert)
				{
					OptimatScrit.NaacProcessListeWirkung = OptimatScritListeNaacProcessWirkung;

					ZuScritInfoFüügeAin(OptimatScrit);
				}
			}
		}

		protected bool ZuScritInfoFüügeAin(
			SictOptimatScrit OptimatScrit)
		{
			if (null == OptimatScrit)
			{
				return false;
			}

			var NaacProcessListeWirkung = OptimatScrit.NaacProcessListeWirkung;

			if (null != NaacProcessListeWirkung)
			{
				VonNuzerMeldungNaacProcessWirkungFüügeAin(NaacProcessListeWirkung);
			}

			var OptimatScritNuzerZait = OptimatScrit.NuzerZait;

			var ScritAutomaatZuusctand = OptimatScrit.NaacNuzerBerictAutomaatZuusctand	as	SictVonOptimatMeldungZuusctand;

			var BisherAnwendungSizungIdent = this.AnwendungSizungIdent;

			var OptimatScritAnwendungSizungIdent = OptimatScrit.AnwendungSizungIdent;

			if (null == BisherAnwendungSizungIdent)
			{
				this.AnwendungSizungIdent = OptimatScritAnwendungSizungIdent;
			}
			else
			{
				if (!Bib3.Glob.SequenceEqualPerObjectEquals(BisherAnwendungSizungIdent, OptimatScritAnwendungSizungIdent))
				{
					return false;
				}
			}

			lock (Lock)
			{
				if (null == OptimatScrit.ProcessWindowClientRasterIdentUndSuuceHinwais)
				{
					var ProcessWindowClientRasterIdentUndSuuceHinwais = this.ZuNuzerZaitMiliWindowClientRasterSuuceHinwaisBerecne(OptimatScritNuzerZait);

					OptimatScrit.ProcessWindowClientRasterIdentUndSuuceHinwais = ProcessWindowClientRasterIdentUndSuuceHinwais;

					if (null == ProcessWindowClientRasterIdentUndSuuceHinwais)
					{
						ListeZaitpunktOptimatScritOoneWindowClientRasterIdent.Add(OptimatScritNuzerZait);
					}
				}

				{
					bool ListeNaacProcessWirkungGeändert;
					bool ListeNaacProcessWirkungVolsctändig;

					var OptimatScritListeNaacProcessWirkung = FürOptimatScritListeNaacProcessWirkungBerecne(
						OptimatScrit,
						out	ListeNaacProcessWirkungGeändert,
						out	ListeNaacProcessWirkungVolsctändig);

					if (ListeNaacProcessWirkungGeändert)
					{
						OptimatScrit.NaacProcessListeWirkung = OptimatScritListeNaacProcessWirkung;

						//	ZuScritInfoFüügeAin(OptimatScrit);
					}

					if (!ListeNaacProcessWirkungVolsctändig)
					{
						ListeZaitpunktOptimatScritOoneNaacProcessWirkung.Add(OptimatScritNuzerZait);
					}
				}

				var ScritAnwendungZaitMiliNulbar = OptimatScrit.AnwendungZaitMili;

				if (ScritAnwendungZaitMiliNulbar.HasValue)
				{
					DictZuNuzerZaitMiliAnwendungZaitMili[OptimatScritNuzerZait] = ScritAnwendungZaitMiliNulbar.Value;
				}

				Optimat.Glob.InListeOrdnetFüügeAin(
					ListeZuVonProcessLeeseBeginZaitScritInfo,
					(t) => t.Key,
					new KeyValuePair<Int64, SictOptimatScrit>(
					OptimatScritNuzerZait, OptimatScrit), OptimatScritNuzerZait);

				BerictBeginNuzerZaitMili = Bib3.Glob.Min(BerictBeginNuzerZaitMili, OptimatScritNuzerZait);
				BerictEndeNuzerZaitMili = Bib3.Glob.Max(BerictEndeNuzerZaitMili, OptimatScritNuzerZait);

				if (null != ScritAutomaatZuusctand)
				{
					var MengeMission = ScritAutomaatZuusctand.MengeMission;

					if (null != MengeMission)
					{
						foreach (var Mission in MengeMission)
						{
							if (null == Mission)
							{
								continue;
							}

							var MissionBezaicnerNulbar = (Int64?)Mission.Ident;

							if (!MissionBezaicnerNulbar.HasValue)
							{
								continue;
							}

							var MissionBezaicner = MissionBezaicnerNulbar.Value;

							var BisherMissionMitZait = Optimat.Glob.TAD(DictZuMissionBezaicnerMissionZuusctandLezte, MissionBezaicner);

							var BisherMissionErhalte = false;

							if (null != BisherMissionMitZait.Wert)
							{
								BisherMissionErhalte = OptimatScritNuzerZait < BisherMissionMitZait.Zait;
							}

							if (!BisherMissionErhalte)
							{
								DictZuMissionBezaicnerMissionZuusctandLezte[MissionBezaicner] =
									new SictWertMitZait<SictMissionZuusctand>(
									OptimatScritNuzerZait, Mission);
							}
						}
					}
				}

				return true;
			}
		}
	}

	public struct SictBerictDataiIdentInDsInfo
	{
		readonly public string DataiPfaad;

		/// <summary>
		/// Verwendet fals Datai ain Zip-Archive.
		/// </summary>
		readonly public string InArchiveEntryPfaad;

		public SictBerictDataiIdentInDsInfo(
			string DataiPfaad,
			string InArchiveEntryPfaad)
		{
			this.DataiPfaad = DataiPfaad;
			this.InArchiveEntryPfaad = InArchiveEntryPfaad;
		}
	}

	public class SictBerictAuswert : SictBerictInspektDaatenKweleAusScpaicer
	{
		readonly object Lock = new object();

		public Int64 ScatenscpaicerZaitraumGrenzeBeginMili;
		public Int64 ScatenscpaicerZaitraumGrenzeEndeMili;

		public Int64 ScatenscpaicerScritZuusctandAnzaalScrankeMax;

		/// <summary>
		/// Ainhait=Bildpunkte.
		/// </summary>
		public Int64 ScatenscpaicerWindowClientRasterKapazitäätScranke;

		/// <summary>
		/// Scpaicert zu Server.Anwendung.Zaitpunkt.Mili den Pfaad zur Datai welce den Zuusctand zu diisem Zaitpunkt bescraibt.
		/// </summary>
		readonly Dictionary<Int64, SictBerictDataiIdentInDsInfo> DictVonAnwendungZaitMiliPfaadZuAutomaatZuusctandInfo = new Dictionary<Int64, SictBerictDataiIdentInDsInfo>();

		readonly protected Dictionary<Int64, SictBerictDataiIdentInDsInfo> DictVonNuzerZaitMiliPfaadZuBerictElement = new Dictionary<Int64, SictBerictDataiIdentInDsInfo>();

		public byte[] AnwendungSizungIdent
		{
			private set;
			get;
		}

		/// <summary>
		/// Lezte berictete Zuusctand zu Mission. Werd nii geleert.
		/// </summary>
		readonly protected Dictionary<Int64, SictWertMitZait<SictMissionZuusctand>> DictVonMissionBezaicnerMissionZuusctandLezte =
			new Dictionary<Int64, SictWertMitZait<SictMissionZuusctand>>();

		readonly protected Dictionary<Int64, Int64> DictZuNuzerZaitMiliAnwendungZaitMili = new Dictionary<Int64, Int64>();

		public bool AingangOptimatScrit(
			SictOptimatScrit OptimatScrit)
		{
			return base.ZuScritInfoFüügeAin(OptimatScrit);
		}
	}

	public class SictBerictAuswertAusDatai : SictBerictAuswert
	{
		public string BerictHauptVerzaicnisPfaad;

		public string BerictWindowClientRasterVerzaicnisPfaad;

		/// <summary>
		/// Verwendet di Annaame das Inhalt der Datai nict geändert werd.
		/// </summary>
		readonly SictScatenscpaicerDict<string, byte[]> DictZuDataiPfaadDataiInhalt = new SictScatenscpaicerDict<string, byte[]>();

		readonly Dictionary<string, bool> DictZuDataiPfaadDataiBeraitsVerwertet = new Dictionary<string, bool>();

		/// <summary>
		/// Ainhait=Oktet.
		/// </summary>
		public Int64 ScatenscpaicerBerictDataiKapazitäätScranke;

		public Int64? ListeDataiAnzaal
		{
			private set;
			get;
		}

		byte[] InhaltAusDataiMitPfaad(string DataiPfaad)
		{
			return DictZuDataiPfaadDataiInhalt.ValueFürKey(DataiPfaad, Bib3.Glob.InhaltAusDataiMitPfaad);
		}

		/// <summary>
		/// In lezte Scrit berecne wurden ale zuvor noc nict ausgewertete Dataie verarbaitet.
		/// </summary>
		public bool BerecneScritLezteListeDataiEnde
		{
			private set;
			get;
		}

		public void Berecne(
			out	System.Exception LaadeDataiException,
			int? VerwertungMengeDataiAnzaalScranke)
		{
			LaadeDataiException = null;

			var BerictVerzaicnisPfaad = this.BerictHauptVerzaicnisPfaad;
			var BerictWindowClientRasterVerzaicnisPfaad = this.BerictWindowClientRasterVerzaicnisPfaad;

			bool BerecneScritLezteListeDataiEnde = false;

			try
			{
				try
				{
					if (!(VerwertungMengeDataiAnzaalScranke <= 0) &&
						null != BerictVerzaicnisPfaad)
					{
						/*
						 * Ermitelt ob noie Dataie vorhande und verwertet diise gegeebenenfals.
						 * */

						var BerictVerzaicnis = new DirectoryInfo(BerictVerzaicnisPfaad);

						var MengeFile = BerictVerzaicnis.GetFiles();

						int VerwertungMengeDataiAnzaalBisher = 0;

						foreach (var File in MengeFile)
						{
							if (VerwertungMengeDataiAnzaalScranke <= VerwertungMengeDataiAnzaalBisher)
							{
								break;
							}

							var DataiPfaad = File.FullName;

							var DataiNaame = System.IO.Path.GetFileName(DataiPfaad);

							var DataiBeraitsVerwertet = Optimat.Glob.TAD(DictZuDataiPfaadDataiBeraitsVerwertet, DataiPfaad);

							if (MengeFile.LastOrDefault() == File)
							{
								BerecneScritLezteListeDataiEnde = true;
							}

							if (DataiBeraitsVerwertet)
							{
								continue;
							}

							DictZuDataiPfaadDataiBeraitsVerwertet[DataiPfaad] = true;

							var DataiInhalt = InhaltAusDataiMitPfaad(DataiPfaad);

							if (null == DataiInhalt)
							{
								continue;
							}

							var NaacDekompresListeDataiNaameUndInhalt = new List<KeyValuePair<string, byte[]>>();

							{
								var ZipIdentString = "PK";

								var ZipIdentListeOktet = Encoding.ASCII.GetBytes(ZipIdentString);

								var AnnaameDataiIstZipArciiv = Bib3.Glob.SequenceEqualPerObjectEquals(ZipIdentListeOktet, DataiInhalt.Take(ZipIdentListeOktet.Length));

								if (AnnaameDataiIstZipArciiv)
								{
									using (var ZipArchive = new System.IO.Compression.ZipArchive(new MemoryStream(DataiInhalt), System.IO.Compression.ZipArchiveMode.Read))
									{
										var MengeEntry = ZipArchive.Entries;

										if (null != MengeEntry)
										{
											foreach (var Entry in MengeEntry)
											{
												var EntryIdent = new SictBerictDataiIdentInDsInfo(DataiPfaad, Entry.FullName);

												using (var EntryStream = Entry.Open())
												{
													var EntryInhalt = Optimat.Glob.AusStreamLeese(EntryStream);

													NaacDekompresListeDataiNaameUndInhalt.Add(new KeyValuePair<string, byte[]>(Entry.FullName, EntryInhalt));
												}
											}
										}
									}
								}
								else
								{
									NaacDekompresListeDataiNaameUndInhalt.Add(new KeyValuePair<string, byte[]>(null, DataiInhalt));
								}
							}

							foreach (var NaacDekompresDataiNaameUndInhalt in NaacDekompresListeDataiNaameUndInhalt)
							{
								var NaacDekompresDataiInhalt = NaacDekompresDataiNaameUndInhalt.Value;

								if (null == NaacDekompresDataiInhalt)
								{
									continue;
								}

								var PfaadNaacBerictElement = new SictBerictDataiIdentInDsInfo(DataiPfaad, NaacDekompresDataiNaameUndInhalt.Key);

								AingangBerictElement(PfaadNaacBerictElement, NaacDekompresDataiInhalt);
							}

							++VerwertungMengeDataiAnzaalBisher;
						}

					}
				}
				catch (System.Exception Exception)
				{
					LaadeDataiException = Exception;
				}

				{
					//	Scatescpaicer Scranke apliziire.

					DictZuDataiPfaadDataiInhalt.BescrankeEntferneLängerNitVerwendete(
						null,
						ScatenscpaicerBerictDataiKapazitäätScranke,
						(DataiInhalt) => 1000 + ((null == DataiInhalt) ? 0 : DataiInhalt.LongLength));
				}

				{
					ListeDataiAnzaal = DictZuDataiPfaadDataiBeraitsVerwertet.Count;
				}

				/*
				 * 2014.06.01
				 * 
				KopiiAktualisiire();
				 * */
			}
			finally
			{
				this.BerecneScritLezteListeDataiEnde = BerecneScritLezteListeDataiEnde;
			}
		}

		virtual protected void AingangBerictElement(
			SictBerictDataiIdentInDsInfo PfaadZuBerictElement,
			byte[] BerictElementSictSerielAbbild)
		{
		}
	}

	/*
	 * 2015.03.26
	 * 
	public class SictBerictAuswertAusDataiScpez<TypScpez> : SictBerictAuswertAusDatai
	{
		override protected void AingangBerictElement(
			SictBerictDataiIdentInDsInfo PfaadZuBerictElement,
			byte[] BerictElementSictSerielAbbild)
		{
			if (null == BerictElementSictSerielAbbild)
			{
				return;
			}

			var BerictElementSictUTF8 = Encoding.UTF8.GetString(BerictElementSictSerielAbbild);

			var BerictElement = JsonConvert.DeserializeObject<Optimat.SictBerictKeteGliidBehältnisScpez<TypScpez>>(BerictElementSictUTF8);

			var BerictElementScpez = BerictElement.Scpez as Optimat.EveOnline.Berict.SictBerictKeteGliid;

			if (null != BerictElementScpez)
			{
				var MengeWindowClientRasterMitZaitMili = BerictElementScpez.MengeWindowClientRasterMitZaitMili;

				if (null != MengeWindowClientRasterMitZaitMili)
				{
					foreach (var WindowClientRasterMitZaitMili in MengeWindowClientRasterMitZaitMili)
					{
						base.NuzerZaitMiliWindowClientRasterSuuceHinwaisFüügeAin(WindowClientRasterMitZaitMili.Zait, WindowClientRasterMitZaitMili.Wert);
					}
				}

				var ListeOptimatScrit = BerictElementScpez.ListeOptimatScrit;

				if (null != ListeOptimatScrit)
				{
					foreach (var OptimatScrit in ListeOptimatScrit)
					{
						var OptimatScritNuzerZait = OptimatScrit.NuzerZait;

						DictVonNuzerZaitMiliPfaadZuBerictElement[OptimatScritNuzerZait] = PfaadZuBerictElement;

						AingangOptimatScrit(OptimatScrit);
					}
				}
			}
		}

	}
	 * */
}
