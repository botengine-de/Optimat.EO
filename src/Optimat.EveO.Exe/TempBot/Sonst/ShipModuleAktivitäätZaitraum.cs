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
	public class ModuleRampAgregatioon
	{
		[JsonProperty]
		readonly public SictWertMitZait<int>[] AingangListeZuZaitMiliRampRotatioonMili;

		[JsonProperty]
		readonly public int GescwindigkaitScrankeMili;

		/*
		 * 2014.09.28
		 * Entfernt da hiir unbegrenzt Scpaicer beleegt würde (abhängig von der Länge der Aktivitäät des Module.
		 * 
		/// <summary>
		/// Darf nit in Berict aufgenome werde daa diis ale ModuleRampAgregatioon sait Begin der Aktivitäät umfast.
		/// </summary>
		readonly public ModuleRampAgregatioon[] MengeVorherigeZuBerüksictige;
		 * */

		[JsonProperty]
		public Int64[] ListeGescwindigkaitMikro
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? GescwindigkaitMikro
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? NuldurcgangZaitMili
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictWertMitZait<int>[] FürGescwindigkaitMengeZuScnapscusZaitpunktMengeAuslasungAnzaal
		{
			private set;
			get;
		}

		public SictWertMitZait<int>[] ZuZaitpunktMengeAuslasungAnzaalOrdnet
		{
			private set;
			get;
		}

		public SictWertMitZait<int>[] AingangListeZuZaitRampRotatioonMiliFiltertNaacVorherige
		{
			private set;
			get;
		}

		public ModuleRampAgregatioon()
		{
		}

		public ModuleRampAgregatioon(
			SictWertMitZait<int>[] AingangListeZuZaitMiliRampRotatioonMili,
			int GescwindigkaitScrankeMili)
		{
			this.AingangListeZuZaitMiliRampRotatioonMili = AingangListeZuZaitMiliRampRotatioonMili;
			this.GescwindigkaitScrankeMili = GescwindigkaitScrankeMili;
		}

		static public DiferenzZwisceScnapscus[] ListeDiferenzZwisceScnapscusBerecne(
			SictWertMitZait<int>[] ListeZuZaitMiliRampRotatioonMili,
			int IntervalScritAnzaal)
		{
			if (null == ListeZuZaitMiliRampRotatioonMili)
			{
				return null;
			}

			if (IntervalScritAnzaal < 1)
			{
				return null;
			}

			return
				ListeZuZaitMiliRampRotatioonMili
				.Skip(IntervalScritAnzaal)
				.Select((Scnapscus1Wert, Scnapscus0Index) =>
					new	DiferenzZwisceScnapscus(ListeZuZaitMiliRampRotatioonMili[Scnapscus0Index], Scnapscus1Wert))
				.ToArray();
		}

		static public int? AusMengeKandidaatBerecneElementMitGeringsteDistanzZuAndere(
			int[] MengeKandidaat)
		{
			return
				(int?)AusMengeKandidaatBerecneElementMitGeringsteDistanzZuAndere(MengeKandidaat.SelectNullable((Kandidaat) => (Int64)Kandidaat).ToArrayNullable());
		}

		static public Int64? AusMengeKandidaatBerecneElementMitGeringsteDistanzZuAndere(
			Int64[] MengeKandidaat)
		{
			if (null == MengeKandidaat)
			{
				return null;
			}

			if (MengeKandidaat.Length < 1)
			{
				return null;
			}

			var BisherBesteWertUndDistanzZuAndere = new KeyValuePair<Int64, Int64>(Int64.MinValue, Int64.MaxValue);

			for (int KandidaatIndex = 0; KandidaatIndex < MengeKandidaat.Length; KandidaatIndex++)
			{
				var KandidaatWert = MengeKandidaat[KandidaatIndex];

				var KandidaatDistanzZuAndereSume =
					MengeKandidaat
					.Select((KandidaatGescwindigkaitAndere) => Math.Abs(KandidaatGescwindigkaitAndere - KandidaatWert))
					.Sum();

				if (KandidaatDistanzZuAndereSume < BisherBesteWertUndDistanzZuAndere.Value)
				{
					BisherBesteWertUndDistanzZuAndere = new KeyValuePair<Int64, Int64>(KandidaatWert, KandidaatDistanzZuAndereSume);
				}
			}

			return BisherBesteWertUndDistanzZuAndere.Key;
		}

		public struct DiferenzZwisceScnapscus
		{
			readonly public SictWertMitZait<int> Scnapscus0;
			readonly public SictWertMitZait<int> Scnapscus1;

			readonly public Int64 GescwindigkaitMikro;

			public DiferenzZwisceScnapscus(
				SictWertMitZait<int> Scnapscus0,
				SictWertMitZait<int> Scnapscus1,
				Int64 GescwindigkaitMikro)
			{
				this.Scnapscus0 = Scnapscus0;
				this.Scnapscus1 = Scnapscus1;
				this.GescwindigkaitMikro = GescwindigkaitMikro;
			}

			public DiferenzZwisceScnapscus(
				SictWertMitZait<int> Scnapscus0,
				SictWertMitZait<int> Scnapscus1)
				:
				this(
				Scnapscus0,
				Scnapscus1,
				((Scnapscus1.Wert - Scnapscus0.Wert).SictUmgebrocen(-400, 600) * 1000000) / (Scnapscus1.Zait - Scnapscus0.Zait))
			{
			}
		}

		/// <summary>
		/// Mespunkte welce in meerere Vorherige aussortiirt wurde werde ausgefiltert.
		/// </summary>
		/// <param name="AingangListeZuZaitMiliRampRotatioonMili"></param>
		/// <param name="MengeVorherigeZuBerüksictige"></param>
		/// <returns></returns>
		static public SictWertMitZait<int>[] FilterMitBerüksictigungVorherige(
			SictWertMitZait<int>[] AingangListeZuZaitMiliRampRotatioonMili,
			ModuleRampAgregatioon[] MengeVorherigeZuBerüksictige,
			int	AnzaalScrankeMin,
			int	AnzaalScrankeMax,
			out	SictWertMitZait<int>[] ZuZaitpunktMengeAuslasungAnzaalOrdnet)
		{
			ZuZaitpunktMengeAuslasungAnzaalOrdnet = null;

			if (null == MengeVorherigeZuBerüksictige)
			{
				return AingangListeZuZaitMiliRampRotatioonMili;
			}

			if (null == AingangListeZuZaitMiliRampRotatioonMili)
			{
				return null;
			}

			var ZuZaitpunktMengeAuslasungAnzaal =
				AingangListeZuZaitMiliRampRotatioonMili.Select((MespunktZaitUndRotatioon) =>
					new	SictWertMitZait<int>(
						MespunktZaitUndRotatioon.Zait,
						(null	== MengeVorherigeZuBerüksictige)	?	0	:
						MengeVorherigeZuBerüksictige
						.Select((VorherigeZuBerüksictige) =>
							VorherigeZuBerüksictige.FürGescwindigkaitMengeZuScnapscusZaitpunktMengeAuslasungAnzaal
							.FirstOrDefaultNullable((k) => k.Zait == MespunktZaitUndRotatioon.Zait).Wert).Sum()))
				.ToArray();

			var	InternZuZaitpunktMengeAuslasungAnzaalOrdnet	=
				ZuZaitpunktMengeAuslasungAnzaal
				.OrderBy((Kandidaat) => Kandidaat.Wert)
				.ThenBy((Kandidaat) => Kandidaat.Zait)
				.ToArray();

			ZuZaitpunktMengeAuslasungAnzaalOrdnet = InternZuZaitpunktMengeAuslasungAnzaalOrdnet;

			var	Liste	= new	List<SictWertMitZait<int>>();

			for (int i = 0; i < ZuZaitpunktMengeAuslasungAnzaalOrdnet.Length; i++)
			{
				if(AnzaalScrankeMin	<= i)
				{
					if(6 < ZuZaitpunktMengeAuslasungAnzaalOrdnet[i].Wert)
					{
						break;
					}
				}

				if (AnzaalScrankeMax <= i)
				{
					break;
				}

				Liste.Add(AingangListeZuZaitMiliRampRotatioonMili.FirstOrDefault((Kandidaat) => Kandidaat.Zait == InternZuZaitpunktMengeAuslasungAnzaalOrdnet[i].Zait));
			}

			return
				Liste
				.OrderBy((Kandidaat) => Kandidaat.Zait)
				.ToArrayFalsNitLeer();
		}

		static SictWertMitZait<int>[] MengeZuScnapscusZaitVerwendungAnzaal(
			Int64[]	MengeScnapscusZait,
			DiferenzZwisceScnapscus[] MengeDiferenz)
		{
			if (null == MengeScnapscusZait)
			{
				return null;
			}

			return
				MengeScnapscusZait
				.Select((ScnapscusZait) => new SictWertMitZait<int>(
					ScnapscusZait,
					(int)(MengeDiferenz.CountNullable((Kandidaat) => Kandidaat.Scnapscus0.Zait == ScnapscusZait || Kandidaat.Scnapscus1.Zait == ScnapscusZait) ?? 0)))
				.ToArray();
		}

		public void Berecne(
			ModuleRampAgregatioon[] MengeVorherigeZuBerüksictige)
		{
			Int64[] ListeGescwindigkaitMikro = null;
			Int64? GescwindigkaitMikro = null;
			Int64? NuldurcgangZaitMili = null;
			SictWertMitZait<int>[] AingangListeZuZaitRampRotatioonMiliFiltertNaacVorherige = null;
			SictWertMitZait<int>[] FürGescwindigkaitMengeZuScnapscusZaitpunktMengeAuslasungAnzaal = null;
			SictWertMitZait<int>[] ZuZaitpunktMengeAuslasungAnzaalOrdnet = null;

			try
			{
				var AingangListeZuZaitMiliRampRotatioonMili = this.AingangListeZuZaitMiliRampRotatioonMili;
				var GescwindigkaitScrankeMili = this.GescwindigkaitScrankeMili;

				if (null == AingangListeZuZaitMiliRampRotatioonMili)
				{
					return;
				}

				if (GescwindigkaitScrankeMili < 1)
				{
					return;
				}

				var	AingangListeScnapscusZait	=
					AingangListeZuZaitMiliRampRotatioonMili
					.Select((ZuZaitMiliRampRotatioonMili) => ZuZaitMiliRampRotatioonMili.Zait)
					.ToArrayNullable();

				AingangListeZuZaitRampRotatioonMiliFiltertNaacVorherige =
					FilterMitBerüksictigungVorherige(
					AingangListeZuZaitMiliRampRotatioonMili,
					MengeVorherigeZuBerüksictige,
					5,
					7,
					out	ZuZaitpunktMengeAuslasungAnzaalOrdnet);

				var ListeDiferenzZwisceScnapscusVorFilter =
					ModuleRampAgregatioon.ListeDiferenzZwisceScnapscusBerecne(AingangListeZuZaitRampRotatioonMiliFiltertNaacVorherige, 1)
					.Concat(ModuleRampAgregatioon.ListeDiferenzZwisceScnapscusBerecne(AingangListeZuZaitRampRotatioonMiliFiltertNaacVorherige, 2))
					//.Concat(ModuleRampAgregatioon.ListeGescwindigkaitMikroBerecne(AingangListeZuZaitRampRotatioonMili, 3))
					.ToArray();

				var ListeDiferenzZwisceScnapscus =
					ListeDiferenzZwisceScnapscusVorFilter
					.Where((Diferenz) => -GescwindigkaitScrankeMili * 1000 <= Diferenz.GescwindigkaitMikro && Diferenz.GescwindigkaitMikro <= GescwindigkaitScrankeMili * 1000)
					.ToArray();

				var	VorAuslasungMengeZuScnapscusZaitMengeVerwendungAnzaal	=
					MengeZuScnapscusZaitVerwendungAnzaal(AingangListeScnapscusZait,	ListeDiferenzZwisceScnapscus);

				ListeGescwindigkaitMikro =
					ListeDiferenzZwisceScnapscus
					.Select((Diferenz) => Diferenz.GescwindigkaitMikro)
					.ToArrayNullable();

				if (!(0 < ListeGescwindigkaitMikro.CountNullable()))
				{
					return;
				}

				var MengeKombiListeGescwindigkaitMikro =
					Bib3.Kombinatoorik.MengeKombinatioonTailmengeOoneOrdnung(
					ListeDiferenzZwisceScnapscus,
					Math.Max(5, Math.Max(ListeGescwindigkaitMikro.Length - 2, (ListeGescwindigkaitMikro.Length * 2) / 3 + 1)));

				if (!(1 < MengeKombiListeGescwindigkaitMikro.CountNullable()))
				{
					return;
				}

				var MengeKombiErgeebnisGescwindigkaitUndDistanzAgr =
					MengeKombiListeGescwindigkaitMikro
					.Select((Kombi, KombiIndex) =>
						{
							Int64 DistanzAgregatioon;

							var Gescwindigkait = Fiting.Fit1DMitKomponenteExponent(
								Kombi.Select((KombiElement) => KombiElement.GescwindigkaitMikro).ToArrayNullable(), 100, out	DistanzAgregatioon);

							if (!Gescwindigkait.HasValue)
							{
								DistanzAgregatioon = Int64.MaxValue;
							}

							return new	KeyValuePair<DiferenzZwisceScnapscus[], KeyValuePair<Int64, Int64>>(
								Kombi,
								new	KeyValuePair<Int64, Int64>(Gescwindigkait ?? Int64.MinValue, DistanzAgregatioon));
						})
						.ToArray();

				var MengeKombiErgeebnisGescwindigkaitUndDistanzAgrBeste =
					MengeKombiErgeebnisGescwindigkaitUndDistanzAgr
					.OrderBy((Kandidaat) => Kandidaat.Value.Value)
					.FirstOrDefault();

				/*
				 * 2014.09.25
				 * 
				var NaacAuslasungMengeZuScnapscusZaitMengeVerwendungAnzaal =
					MengeZuScnapscusZaitVerwendungAnzaal(AingangListeScnapscusZait, MengeKombiErgeebnisGescwindigkaitUndDistanzAgrBeste.Key);

				FürGescwindigkaitMengeZuScnapscusZaitpunktMengeAuslasungAnzaal =
					AingangListeScnapscusZait
					.Select((KandidaatScnapscusZait) =>
						new	SictWertMitZait<int>(
							KandidaatScnapscusZait,
							VorAuslasungMengeZuScnapscusZaitMengeVerwendungAnzaal.FirstOrDefault((t) => t.Zait	== KandidaatScnapscusZait).Wert	-
							NaacAuslasungMengeZuScnapscusZaitMengeVerwendungAnzaal.FirstOrDefault((t) => t.Zait	== KandidaatScnapscusZait).Wert))
					.Where((Kandidaat) => 0 < Kandidaat.Wert)
					.ToArray();
				 * */

				GescwindigkaitMikro =
					MengeKombiErgeebnisGescwindigkaitUndDistanzAgrBeste.Value.Key;

				if (!GescwindigkaitMikro.HasValue)
				{
					return;
				}

				/*
				 * 2014.09.27
				 * 
				var	FilterGescwindigkaitMikroScrankeMin	= (GescwindigkaitMikro * 7) / 10;
				var	FilterGescwindigkaitMikroScrankeMax	= (GescwindigkaitMikro * 13) / 10;
				 * */
				var FilterGescwindigkaitMikroScrankeMin = GescwindigkaitMikro / 2;
				var FilterGescwindigkaitMikroScrankeMax = GescwindigkaitMikro * 2;

				var ListeDiferenzZwisceScnapscusFilterNaacGescwindigkaitErgeebnis =
					ListeDiferenzZwisceScnapscus
					.Where((Kandidaat) =>
						FilterGescwindigkaitMikroScrankeMin <= Kandidaat.GescwindigkaitMikro &&
						Kandidaat.GescwindigkaitMikro <= FilterGescwindigkaitMikroScrankeMax)
					.ToArray();

				var NaacAuslasungMengeZuScnapscusZaitMengeVerwendungAnzaal =
					MengeZuScnapscusZaitVerwendungAnzaal(AingangListeScnapscusZait, ListeDiferenzZwisceScnapscusFilterNaacGescwindigkaitErgeebnis);

				FürGescwindigkaitMengeZuScnapscusZaitpunktMengeAuslasungAnzaal =
					AingangListeScnapscusZait
					.Select((KandidaatScnapscusZait) =>
						new SictWertMitZait<int>(
							KandidaatScnapscusZait,
							VorAuslasungMengeZuScnapscusZaitMengeVerwendungAnzaal.FirstOrDefault((t) => t.Zait == KandidaatScnapscusZait).Wert -
							NaacAuslasungMengeZuScnapscusZaitMengeVerwendungAnzaal.FirstOrDefault((t) => t.Zait == KandidaatScnapscusZait).Wert))
					.Where((Kandidaat) => 0 < Kandidaat.Wert)
					.ToArray();

				if (0 == GescwindigkaitMikro)
				{
					return;
				}

				{
					var ZyyklusDauerMili = (Int64)(1e+9) / GescwindigkaitMikro.Value;

					var UmbrucRegioonBegin = (AingangListeZuZaitMiliRampRotatioonMili.Last().Zait - ZyyklusDauerMili / 2);
					var UmbrucRegioonEnde = UmbrucRegioonBegin + ZyyklusDauerMili;

					var MengeKandidaatNuldurcgangZaitMili =
						AingangListeZuZaitMiliRampRotatioonMili
						.Select((ZuZaitMiliRampRotatioonMili) =>
							((ZuZaitMiliRampRotatioonMili.Zait - (ZuZaitMiliRampRotatioonMili.Wert * 1000000 / GescwindigkaitMikro.Value))
							.SictUmgebrocen(UmbrucRegioonBegin, UmbrucRegioonEnde)))
						.ToArray();

					NuldurcgangZaitMili = Fiting.Fit1DMitKomponenteExponent(MengeKandidaatNuldurcgangZaitMili, 100);
				}
			}
			finally
			{
				this.ListeGescwindigkaitMikro = ListeGescwindigkaitMikro;
				this.ZuZaitpunktMengeAuslasungAnzaalOrdnet = ZuZaitpunktMengeAuslasungAnzaalOrdnet;
				this.AingangListeZuZaitRampRotatioonMiliFiltertNaacVorherige = AingangListeZuZaitRampRotatioonMiliFiltertNaacVorherige;
				this.GescwindigkaitMikro = GescwindigkaitMikro;
				this.NuldurcgangZaitMili = NuldurcgangZaitMili;
				this.FürGescwindigkaitMengeZuScnapscusZaitpunktMengeAuslasungAnzaal = FürGescwindigkaitMengeZuScnapscusZaitpunktMengeAuslasungAnzaal;
			}
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class AusZaitraumMengeTargetAssignmentBerict
	{
		[JsonProperty]
		readonly public int FilterTransitioonScritIndexScrankeMin;

		[JsonProperty]
		readonly public int FilterTransitioonScritIndexScrankeMax;

		[JsonProperty]
		readonly public bool? FilterTransitioonZiilWert;

		[JsonProperty]
		readonly public Int64[] FilterMengeTexturIdent;

		[JsonProperty]
		public	Int64?	Zait
		{
			private set;
			get;
		}

		[JsonProperty]
		public KeyValuePair<SictTargetZuusctand, TransitionInfo<Int64, IEnumerable<Int64>, MengeInGrupeAnzaalTransitioon<Int64>>>[] MengeKandidaatTargetUndAssignedTransitioon
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictTargetZuusctand[] MengeTarget
		{
			private set;
			get;
		}

		public AusZaitraumMengeTargetAssignmentBerict()
		{
		}

		public AusZaitraumMengeTargetAssignmentBerict(
			int FilterTransitioonScritIndexScrankeMin,
			int FilterTransitioonScritIndexScrankeMax,
			bool? FilterTransitioonZiilWert,
			Int64[] FilterMengeTexturIdent)
		{
			this.FilterTransitioonScritIndexScrankeMin = FilterTransitioonScritIndexScrankeMin;
			this.FilterTransitioonScritIndexScrankeMax = FilterTransitioonScritIndexScrankeMax;
			this.FilterTransitioonZiilWert = FilterTransitioonZiilWert;
			this.FilterMengeTexturIdent = FilterMengeTexturIdent;
		}

		public void Berecne(ISictAutomatZuusctand Automaat)
		{
			var MengeTarget = new List<SictTargetZuusctand>();
			var MengeKandidaatTargetUndAssignedTransitioon =
				new List<KeyValuePair<SictTargetZuusctand, TransitionInfo<Int64, IEnumerable<Int64>, MengeInGrupeAnzaalTransitioon<Int64>>>>();

			try
			{
				if (null == Automaat)
				{
					return;
				}

				Zait = Automaat.NuzerZaitMili;

				var MengeKandidaatTarget = Automaat.MengeTarget();

				var FilterMengeTexturIdent = this.FilterMengeTexturIdent;

				if (null != MengeKandidaatTarget)
				{
					foreach (var Target in MengeKandidaatTarget)
					{
						if (null == Target)
						{
							continue;
						}

						var TargetListeAssignedTransitioon = Target.ListeAssignedTransitioon();

						if (null == TargetListeAssignedTransitioon)
						{
							continue;
						}

						var TargetListeAssignedTransitioonLezte =
							TargetListeAssignedTransitioon.LastOrDefault();

						MengeKandidaatTargetUndAssignedTransitioon.Add(
							new KeyValuePair<SictTargetZuusctand, TransitionInfo<Int64, IEnumerable<Int64>, MengeInGrupeAnzaalTransitioon<Int64>>>(
								Target, TargetListeAssignedTransitioonLezte));

						foreach (var KandidaatTargetAssignedTransitioon in TargetListeAssignedTransitioon)
						{
							var TargetAssignedScritIndex = Automaat.ZuNuzerZaitBerecneScritIndex(KandidaatTargetAssignedTransitioon.ScritIdent);

							/*
							 * 2014.09.22
							 * 
							var ZaitDiferenzScritAnzaal = TargetAssignedScritIndex - ModuleTransitioonBeginScritIndex;

							if (!ZaitDiferenzScritAnzaal.HasValue)
							{
								continue;
							}

							if (1 < Math.Abs(ZaitDiferenzScritAnzaal.Value))
							{
								continue;
							}
							 * */

							if (!(FilterTransitioonScritIndexScrankeMin <= TargetAssignedScritIndex))
							{
								continue;
							}

							if (!(TargetAssignedScritIndex	<= FilterTransitioonScritIndexScrankeMax))
							{
								continue;
							}

							var MengeAssignedGrupeZuunaame = KandidaatTargetAssignedTransitioon.ZuusazInfo.ZiilWertMengeInGrupeZuunaame;

							if (MengeAssignedGrupeZuunaame.NullOderLeer())
							{
								continue;
							}

							if (!FilterMengeTexturIdent.NullOderLeer())
							{
								if (!MengeAssignedGrupeZuunaame.Any((AssignedGrupeZuunaame) => FilterMengeTexturIdent.Contains(AssignedGrupeZuunaame.Key)))
								{
									continue;
								}
							}

							MengeTarget.Add(Target);
							break;
						}
					}
				}
			}
			finally
			{
				this.MengeKandidaatTargetUndAssignedTransitioon = MengeKandidaatTargetUndAssignedTransitioon.ToArrayFalsNitLeer();
				this.MengeTarget = MengeTarget.ToArrayFalsNitLeer();
			}
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class ModuleAktivitäätZaitraum : SictVerlaufBeginUndEndeRef<int>
	{
		[JsonProperty]
		readonly public SictShipUiModuleReprZuusctand Module;

		/*
		 * 2014.09.22
		 * 
		[JsonProperty]
		public bool BeginMengeTargetGesezt
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictTargetZuusctand[] BeginMengeTarget
		{
			private set;
			get;
		}
		 * */

		[JsonProperty]
		public AusZaitraumMengeTargetAssignmentBerict BeginTargetAssignment
		{
			private set;
			get;
		}

		[JsonProperty]
		readonly Queue<SictWertMitZait<int>> ListeZuZaitRotatioonMili = new Queue<SictWertMitZait<int>>();

		//	[JsonProperty]
		readonly Queue<ModuleRampAgregatioon> ListeRampAgregatioon = new Queue<ModuleRampAgregatioon>();

		[JsonProperty]
		public ModuleRampAgregatioon ScritLezteRampAgregatioon
		{
			private set;
			get;
		}

		[JsonProperty]
		readonly Queue<Int64> InternRotatioonListeNuldurcgangZait = new Queue<Int64>();

		[JsonProperty]
		public Int64? RotatioonProjektioonNuldurcgangNääxte
		{
			private set;
			get;
		}

		public IEnumerable<Int64> RotatioonListeNuldurcgangZait
		{
			get
			{
				return InternRotatioonListeNuldurcgangZait;
			}
		}

		public Int64? RotatioonNuldurcgangLezteZait
		{
			get
			{
				var RotatioonListeNuldurcgangZait = this.RotatioonListeNuldurcgangZait;

				return RotatioonListeNuldurcgangZait.Cast<Int64?>().LastOrDefault();
			}
		}

		public int RotatioonListeNuldurcgangAnzaalMitZaitScrankeMin(Int64 ZaitScrankeMin)
		{
			return InternRotatioonListeNuldurcgangZait.Count((NuldurcgangZait) => ZaitScrankeMin <= NuldurcgangZait);
		}

		public int RotatioonListeNuldurcgangAnzaal
		{
			get
			{
				return InternRotatioonListeNuldurcgangZait.Count;
			}
		}

		/*
		 * 2014.09.14
		 * 
		[JsonProperty]
		public Int64? RotatioonNuldurclaufNääxteZait
		{
			get
			{
			}
		}
		 * */

		public SictTargetZuusctand[] BeginMengeTarget
		{
			get
			{
				var BeginTargetAssignment = this.BeginTargetAssignment;

				if (null == BeginTargetAssignment)
				{
					return null;
				}

				return BeginTargetAssignment.MengeTarget;
			}
		}

		public SictTargetZuusctand BeginTarget
		{
			get
			{
				var BeginMengeTarget = this.BeginMengeTarget;

				if (1 == BeginMengeTarget.CountNullable())
				{
					return BeginMengeTarget.FirstOrDefaultNullable();
				}

				return null;
			}
		}

		/*
		 * 2014.09.27
		 * 
		public ModuleRampAgregatioon ScritLezteRampAgregatioon
		{
			get
			{
				return	ListeRampAgregatioon.LastOrDefault();
			}
		}
		 * */

		public ModuleAktivitäätZaitraum()
		{
		}

		public ModuleAktivitäätZaitraum(
			Int64 Zait,
			SictShipUiModuleReprZuusctand Module)
			:
			base(Zait)
		{
			this.Module = Module;
		}

		public void BeginTargetAssignmentSeze(
			AusZaitraumMengeTargetAssignmentBerict BeginTargetAssignment)
		{
			if (null != this.BeginTargetAssignment)
			{
				return;
			}

			this.BeginTargetAssignment = BeginTargetAssignment;
		}

		public struct LineFitPerVersazUndSctaigung
		{
			readonly public double Versaz;
			readonly public double Sctaigung;
			readonly public double Güüte;

			public LineFitPerVersazUndSctaigung(
				double Versaz,
				double Sctaigung,
				double Güüte)
			{
				this.Versaz = Versaz;
				this.Sctaigung = Sctaigung;
				this.Güüte = Güüte;
			}

			static public T[][] MengeKombiTailmengeBerecne<T>(
				T[] Menge,
				int MengeElementZuVerwendeAnzaal)
			{
				if (null == Menge)
				{
					return null;
				}

				if (MengeElementZuVerwendeAnzaal < 1)
				{
					return new T[][] { };
				}

				if (Menge.Length <= MengeElementZuVerwendeAnzaal)
				{
					return new T[][] { Menge };
				}

				var MengeElementAuszulaseAnzaal = Menge.Length - MengeElementZuVerwendeAnzaal;

				var MengeKombiAnzaal =
					Bib3.Glob.Fakultäät(Menge.Length) / Bib3.Glob.Fakultäät(MengeElementAuszulaseAnzaal) / Bib3.Glob.Fakultäät(MengeElementZuVerwendeAnzaal);

				if (1 < MengeElementZuVerwendeAnzaal)
				{
					throw new NotImplementedException();
				}

				throw new NotImplementedException();
			}

			/*
			 * 2014.09.28
			 * 
			static public LineFitPerVersazUndSctaigung? FitLineFürValueVersazUndSctaigung(
				KeyValuePair<double, double>[] MengePunkt)
			{
				if ((MengePunkt.CountNullable() ?? 0) < 2)
				{
					return null;
				}

				var x = MengePunkt.Select((Punkt) => Punkt.Key).ToArray();
				var y = MengePunkt.Select((Punkt) => Punkt.Value).ToArray();

				var VersazUndSctaigung = MathNet.Numerics.Fit.Line(x, y);

				var Versaz = VersazUndSctaigung.Item1;
				var Sctaigung = VersazUndSctaigung.Item2;

				var MengePunktVorhersaage = x.Select((PunktA) => Versaz + PunktA * Sctaigung).ToArray();

				return new LineFitPerVersazUndSctaigung(
					Versaz,
					Sctaigung,
					MathNet.Numerics.GoodnessOfFit.RSquared(MengePunktVorhersaage, y));
			}
			 * */
		}

		/*
		 * 2014.09.28
		 * 
		static public SictWertMitZait<Int64>? RotatioonZaitNulUndGescwindigkaitMikroBerecne(
			IEnumerable<SictWertMitZait<int>> MengeScnapscusZaitUndRotatioonMili)
		{
			if (null == MengeScnapscusZaitUndRotatioonMili)
			{
				return null;
			}

			//	Di für fiting genuzte Funktioon berüksictigt noc nit deen Zyyklisce Raum, daher nur kurze Liste von Scnapscus verwende um nit meer als ainen Umbruc drine zu haabe.
			var MengeScnapscusZaitUndRotatioonMiliReduziirt =
				MengeScnapscusZaitUndRotatioonMili.Skip(MengeScnapscusZaitUndRotatioonMili.Count() - 6)
				.ToArrayNullable();

			var MengeKombi =
				ListeZuZaitRotatioonMiliBerecneMengeKombi(MengeScnapscusZaitUndRotatioonMiliReduziirt);

			if (null == MengeKombi)
			{
				return null;
			}

			var MengeKombiFitErgeebnis =
				MengeKombi
				.WhereNullable((KandidaatKombi) => 2 < KandidaatKombi.CountNullable())
				.SelectNullable((Kombi) => LineFitPerVersazUndSctaigung.FitLineFürValueVersazUndSctaigung(
					Kombi.Select((Punkt) => new KeyValuePair<double, double>(Punkt.Zait, Punkt.Wert)).ToArray()))
				.ToArrayNullable();

			var MengeKombiFitErgeebnisBeste =
				MengeKombiFitErgeebnis
				.OrderByDescending((Kandidaat) => Kandidaat.HasValue ? Kandidaat.Value.Güüte : int.MinValue)
				.FirstOrDefault();

			if (!MengeKombiFitErgeebnisBeste.HasValue)
			{
				return null;
			}

			var Versaz = MengeKombiFitErgeebnisBeste.Value.Versaz;
			var Sctaigung = MengeKombiFitErgeebnisBeste.Value.Sctaigung;

			//	Ergeebnis Umrecne so das Versaz auf Zait anzuwende isc.

			var ZyyklusDauer = 1e+3 / Sctaigung;

			var InZaitVersaz = Versaz / Sctaigung;

			var VersazFensterBegin = MengeScnapscusZaitUndRotatioonMili.LastOrDefault().Zait;

			var InZaitVersazUmgebroce = InZaitVersaz.SictUmgebrocen(VersazFensterBegin, VersazFensterBegin + ZyyklusDauer);

			return new SictWertMitZait<Int64>((Int64)InZaitVersazUmgebroce, (Int64)(Sctaigung * 1e+3));
		}
		 * */

		static public T[][] MengeKombinatioonTailmengeMiinusAinsBerecne<T>(
			T[] Menge)
		{
			if (null == Menge)
			{
				return null;
			}

			var MengeKombiTailmenge =
				Enumerable.Range(0, Menge.Length)
				.Select((ElementAuszulaseIndex) =>
					Menge
					.Take(ElementAuszulaseIndex)
					.Concat(Menge.Skip(ElementAuszulaseIndex + 1))
					.ToArrayNullable())
				.ToArrayNullable();

			return MengeKombiTailmenge;
		}

		/// <summary>
		/// berüksictigung des zyyklisce Raum.
		/// </summary>
		/// <param name="ListeScnapscusZuZaitRotatioonMili"></param>
		/// <returns></returns>
		static public SictWertMitZait<int>[][] ListeZuZaitRotatioonMiliBerecneMengeKombi(
			SictWertMitZait<int>[] ListeScnapscusZuZaitRotatioonMili)
		{
			if (null == ListeScnapscusZuZaitRotatioonMili)
			{
				return null;
			}

			/*
			 * 2014.09.14
			 * 
			var MengeKombiTailmenge =
				Enumerable.Range(0, ListeScnapscusZuZaitRotatioonMili.Length)
				.Select((ElementAuszulaseIndex) =>
					ListeScnapscusZuZaitRotatioonMili
					.Take(ElementAuszulaseIndex)
					.Concat(ListeScnapscusZuZaitRotatioonMili.Skip(ElementAuszulaseIndex + 1))
					.ToArrayNullable())
				.ToArrayNullable();
			 * */

			/*
			 * 2014.09.14
			 * 
			var MengeKombiTailmenge =
				Bib3.Glob.ArrayAusListeFeldGeflact(
				MengeKombinatioonTailmengeMiinusAinsBerecne(ListeScnapscusZuZaitRotatioonMili)
				.SelectNullable((tKombi) => MengeKombinatioonTailmengeMiinusAinsBerecne(tKombi)));
			 * */

			var MengeKombiTailmenge =
				Bib3.Kombinatoorik.MengeKombinatioonTailmengeOoneOrdnung(
				ListeScnapscusZuZaitRotatioonMili, ListeScnapscusZuZaitRotatioonMili.Length - 2);

			var MengeKombiVersezt =
				MengeKombiTailmenge
				.Select((VorherKombi) =>
				Enumerable.Range(0, VorherKombi.Length)
				.Select((VersazEndeElementIndex) =>
					VorherKombi.Select((ScnapscusZuZaitRotatioonMili, ScnapscusIndex) => new SictWertMitZait<int>(
						ScnapscusZuZaitRotatioonMili.Zait,
						ScnapscusZuZaitRotatioonMili.Wert - (ScnapscusIndex < VersazEndeElementIndex ? 1000 : 0))).ToArray())
				.ToArray())
				.ToArray();

			return Bib3.Glob.ArrayAusListeFeldGeflact(MengeKombiVersezt);
		}

		public void AingangZuZaitRotatioonMili(
			Int64 Zait,
			int RotatioonMili)
		{
			if (EndeZait.HasValue)
			{
				return;
			}

			if (0 < ListeZuZaitRotatioonMili.Count)
			{
				if (Zait <= ListeZuZaitRotatioonMili.LastOrDefault().Zait)
				{
					return;
				}
			}

			ListeZuZaitRotatioonMili.Enqueue(new SictWertMitZait<int>(Zait, RotatioonMili));

			var ListeZuZaitRotatioonMiliBerüksictigt = ListeZuZaitRotatioonMili.Skip(ListeZuZaitRotatioonMili.Count - 9).ToArray();

			if (6 < ListeZuZaitRotatioonMiliBerüksictigt.Length)
			{
				var RampAgregatioon = new ModuleRampAgregatioon(ListeZuZaitRotatioonMiliBerüksictigt, 200);

				RampAgregatioon.Berecne(ListeRampAgregatioon.ToArrayNullable());

				ScritLezteRampAgregatioon = RampAgregatioon;
				ListeRampAgregatioon.Enqueue(RampAgregatioon);
				ListeRampAgregatioon.ListeKürzeBegin(8);

				var NuldurcgangZaitMili = RampAgregatioon.NuldurcgangZaitMili;
				var GescwindigkaitMikro = RampAgregatioon.GescwindigkaitMikro;

				if (NuldurcgangZaitMili.HasValue && GescwindigkaitMikro.HasValue)
				{
					var ZyyklusDauerMili = (Int64)(1e+9) / GescwindigkaitMikro.Value;

					var NuldurcgangNääxteZait = NuldurcgangZaitMili.Value;

					var NuldurcgangNääxteZaitDistanz = NuldurcgangNääxteZait - Zait;

					RotatioonProjektioonNuldurcgangNääxte =
						NuldurcgangNääxteZait.SictUmgebrocen(Zait, Zait + ZyyklusDauerMili);

					/*
					 * 2014.09.27
					 * 
					if (Math.Abs(NuldurcgangNääxteZaitDistanz) < 4444)
					 * */
					if (NuldurcgangNääxteZaitDistanz <= 0 && Math.Abs(NuldurcgangNääxteZaitDistanz)	< 8888)
					{
						var NuldurcgangVerdrängtDurcVorherige = false;

						var RotatioonNuldurcgangLezteZait = this.RotatioonNuldurcgangLezteZait ?? this.BeginZait;

						if (RotatioonNuldurcgangLezteZait.HasValue)
						{
							var RotatioonNuldurcgangLezteZaitDistanz = RotatioonNuldurcgangLezteZait.Value - NuldurcgangNääxteZait;

							if (Math.Abs(RotatioonNuldurcgangLezteZaitDistanz) < Math.Abs(ZyyklusDauerMili) / 2)
							{
								NuldurcgangVerdrängtDurcVorherige = true;
							}
						}

						if (!NuldurcgangVerdrängtDurcVorherige)
						{
							InternRotatioonListeNuldurcgangZait.Enqueue(NuldurcgangNääxteZait);

							InternRotatioonListeNuldurcgangZait.ListeKürzeBegin(8);
							InternRotatioonListeNuldurcgangZait.ListeKürzeBegin((Element) => 1000 * 60 * 15 < Zait - Element);
						}
					}
				}
			}

			ListeZuZaitRotatioonMili.ListeKürzeBegin(10);
		}
	}

}
