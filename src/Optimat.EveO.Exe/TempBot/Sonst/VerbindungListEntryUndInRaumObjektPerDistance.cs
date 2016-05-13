using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Bib3;
//using Optimat.EveOnline.AuswertGbs;
using Optimat.EveOnline.VonSensor;

namespace Optimat.EveOnline.Anwendung
{
	[JsonObject(MemberSerialization.OptIn)]
	public class ZuTargetAinscrankungMengeSurveyScanItem
	{
		[JsonProperty]
		readonly public SictTargetZuusctand Target;

		[JsonProperty]
		public string TargetOreTypSictString
		{
			private set;
			get;
		}

		[JsonProperty]
		public	ZuOreTypAusSurveyScanInfo	ZuOreTypSurveyScanInfo
		{
			private set;
			get;
		}

		[JsonProperty]
		public	List<GbsListGroupedEntryZuusctand>	MengeKandidaatListItem
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? OreQuantityScrankeMin
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? OreQuantityScrankeMax
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? OreVolumeMiliScrankeMin
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? OreVolumeMiliScrankeMax
		{
			private set;
			get;
		}

		/// <summary>
		/// Ale Item welce für Target vorkome wurde erfast.
		/// Taile der List köne z.B. dan verborge blaibe wen Viewport klainer als Liste der Entry.
		/// </summary>
		[JsonProperty]
		public bool? MengeKandidaatListItemVolsctändig
		{
			private set;
			get;
		}

		public GbsListGroupedEntryZuusctand ListGroup
		{
			get
			{
				var ZuOreTypSurveyScanInfo = this.ZuOreTypSurveyScanInfo;

				if (null == ZuOreTypSurveyScanInfo)
				{
					return null;
				}

				return ZuOreTypSurveyScanInfo.EntryGroup;
			}
		}

		public bool? OreTypInScnapscusSurveyScanEntryIstOoberste
		{
			get
			{
				var ZuOreTypSurveyScanInfo = this.ZuOreTypSurveyScanInfo;

				if (null == ZuOreTypSurveyScanInfo)
				{
					return null;
				}

				return ZuOreTypSurveyScanInfo.InSurveyScanScnapscusIstOoberste;
			}
		}

		public bool? OreTypInScnapscusSurveyScanEntryIstUnterste
		{
			get
			{
				var ZuOreTypSurveyScanInfo = this.ZuOreTypSurveyScanInfo;

				if (null == ZuOreTypSurveyScanInfo)
				{
					return null;
				}

				return ZuOreTypSurveyScanInfo.InSurveyScanScnapscusIstUnterste;
			}
		}

		public ZuTargetAinscrankungMengeSurveyScanItem()
		{
		}

		public ZuTargetAinscrankungMengeSurveyScanItem(
			SictTargetZuusctand Target)
		{
			this.Target = Target;

			MengeKandidaatListItem = new List<GbsListGroupedEntryZuusctand>();
		}

		public void Aktualisiire(ISictAutomatZuusctand Automaat)
		{
			string TargetOreTypSictString = null;
			OreTypSictEnum? TargetOreTyp = null;
			ZuOreTypAusSurveyScanInfo ZuOreTypSurveyScanInfo = null;

			var MengeKandidaatListItem = new List<GbsListGroupedEntryZuusctand>();
			bool? MengeKandidaatListItemVolsctändig = null;
			Int64? OreQuantityScrankeMin = null;
			Int64? OreQuantityScrankeMax = null;
			Int64? OreVolumeMiliScrankeMin = null;
			Int64? OreVolumeMiliScrankeMax = null;

			try
			{
				if (null == Automaat)
				{
					return;
				}

				var Target = this.Target;

				if (null == Target)
				{
					return;
				}

				var TargetIstAsteroid =
					SictOverviewUndTargetZuusctand.TargetBescriftungIstAsteroid(Target, out	TargetOreTypSictString, out	TargetOreTyp);

				if (!TargetIstAsteroid)
				{
					return;
				}

				var MengeZuListEntryTarget = Automaat.MengeZuListEntryTargetAinscrankungPerDistance();

				ZuOreTypSurveyScanInfo =
					Automaat.ZuOreTypSictStringSurveyScanInfo(TargetOreTypSictString);

				if (null == ZuOreTypSurveyScanInfo)
				{
					return;
				}

				try
				{
					IEnumerable<SictWertMitZait<KeyValuePair<SictTargetZuusctand, GbsListGroupedEntryZuusctand>>> ListeIndikatorZuTargetSurveyScanListEntry = null;

					{
						var AutomaatGbs = Automaat.Gbs;

						if (null != AutomaatGbs)
						{
							var AgregatioonAusTransitionInfo = AutomaatGbs.AgregatioonAusTransitionInfo;

							if (null != AgregatioonAusTransitionInfo)
							{
								ListeIndikatorZuTargetSurveyScanListEntry = AgregatioonAusTransitionInfo.ListeIndikatorZuTargetSurveyScanListEntry;
							}
						}
					}

					var IndikatorZuTargetSurveyScanListEntryLezte =
						ListeIndikatorZuTargetSurveyScanListEntry
						.LastOrDefaultNullable((Kandidaat) => Kandidaat.Wert.Key == Target);

					var AusIndikatorSurveyScanListEntry =
						IndikatorZuTargetSurveyScanListEntryLezte.Wert.Value;

					var ZuOreTypSurveyScanInfoListeItem = ZuOreTypSurveyScanInfo.ListeEntryItem;

					if (null == ZuOreTypSurveyScanInfoListeItem)
					{
						return;
					}
		
					if (null != AusIndikatorSurveyScanListEntry)
					{
						if (ZuOreTypSurveyScanInfoListeItem.ContainsNullable(AusIndikatorSurveyScanListEntry))
						{
							MengeKandidaatListItem.Add(AusIndikatorSurveyScanListEntry);
							return;
						}
					}

					foreach (var AusSurveyScanListEntry in ZuOreTypSurveyScanInfoListeItem)
					{
						var ListItemAusgesclosePerDistance = false;

						if (null != MengeZuListEntryTarget)
						{
							var ZuListEntryTarget = MengeZuListEntryTarget.FirstOrDefault((Kandidaat) => Kandidaat.ListEntry == AusSurveyScanListEntry);

							if (null != ZuListEntryTarget)
							{
								if (!ZuListEntryTarget.MengeInRaumObjekt.ContainsNullable(Target))
								{
									ListItemAusgesclosePerDistance = true;
								}
							}
						}

						if (ListItemAusgesclosePerDistance)
						{
							continue;
						}

						MengeKandidaatListItem.Add(AusSurveyScanListEntry);
					}
				}
				finally
				{
					this.ZuOreTypSurveyScanInfo = ZuOreTypSurveyScanInfo;

					var MengeKandidaatListItemOreQuantity =
						MengeKandidaatListItem
						.SelectNullable((KandidaatListItem) => KandidaatListItem.Quantity)
						.ToArrayNullable();

					var MengeKandidaatListIteOreVolumeMili =
						MengeKandidaatListItem
						.SelectNullable((KandidaatListItem) => KandidaatListItem.OreVolumeMili)
						.ToArrayNullable();

					if (MengeKandidaatListItemOreQuantity.AllHaveValueNullable()	?? false)
					{
						OreQuantityScrankeMin = Bib3.Glob.Min(MengeKandidaatListItemOreQuantity);
						OreQuantityScrankeMax = Bib3.Glob.Max(MengeKandidaatListItemOreQuantity);
					}

					if (MengeKandidaatListIteOreVolumeMili.AllHaveValueNullable() ?? false)
					{
						OreVolumeMiliScrankeMin = Bib3.Glob.Min(MengeKandidaatListIteOreVolumeMili);
						OreVolumeMiliScrankeMax = Bib3.Glob.Max(MengeKandidaatListIteOreVolumeMili);
					}

					//	Aktualisatioon hiir findet nur sctat wen WindowSurveyScanView noc ofe.

					this.TargetOreTypSictString = TargetOreTypSictString;

					Bib3.Glob.PropagiireListeRepräsentatioonMitReprUndIdentPerClrReferenz(
						MengeKandidaatListItem,
						this.MengeKandidaatListItem);

					this.MengeKandidaatListItemVolsctändig = MengeKandidaatListItemVolsctändig;

					this.OreQuantityScrankeMin = OreQuantityScrankeMin;
					this.OreQuantityScrankeMax = OreQuantityScrankeMax;

					this.OreVolumeMiliScrankeMin = OreVolumeMiliScrankeMin;
					this.OreVolumeMiliScrankeMax = OreVolumeMiliScrankeMax;
				}
			}
			finally
			{
			}
		}

		static public void MengeZuTargetAinscrankungAktualisiire(
			ISictAutomatZuusctand Automaat,
			IList<ZuTargetAinscrankungMengeSurveyScanItem>	ZiilMenge)
		{
			var MengeTarget = (null == Automaat) ? null : Automaat.MengeTarget();

			Bib3.Glob.PropagiireListeRepräsentatioon(
				MengeTarget,
				ZiilMenge,
				(Target) => new ZuTargetAinscrankungMengeSurveyScanItem(Target),
				(KandidaatRepr, Target) => KandidaatRepr.Target == Target,
				(Repr, Target) => Repr.Aktualisiire(Automaat),
				false);
		}
	}

	public class VerbindungListEntryUndTargetPerDistance :
		VerbindungListEntryUndInRaumObjektPerDistance<SictTargetZuusctand, ShipUiTarget>
	{
	}

	public class VerbindungListEntryUndOverviewObjektPerDistance :
		VerbindungListEntryUndInRaumObjektPerDistance<SictOverViewObjektZuusctand, VonSensor.OverviewZaile>
	{
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class VerbindungListEntryUndInRaumObjektPerDistance<InRaumObjektType,	ScnapscusType>
		where InRaumObjektType : SictInRaumObjektReprZuusctand<ScnapscusType>
		where ScnapscusType : GbsElement
	{
		public ISictAutomatZuusctand Automaat
		{
			private set;
			get;
		}

		[JsonProperty]
		public GbsListGroupedEntryZuusctand ListEntry
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? SaitWindowSurveyScanViewListBeginShipSctreke
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? DistanceScrankeMin
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? DistanceScrankeMax
		{
			private set;
			get;
		}

		[JsonProperty]
		public InRaumObjektType[] MengeInRaumObjekt
		{
			private set;
			get;
		}

		public VerbindungListEntryUndInRaumObjektPerDistance()
		{
		}

		static public void DistanceScrankeBerecne(
			GbsListGroupedEntryZuusctand ListEntry,
			ISictAutomatZuusctand	Automaat,
			out	Int64?	SaitWindowSurveyScanViewListBeginShipSctreke,
			out	Int64?	DistanceScrankeMin,
			out	Int64? DistanceScrankeMax)
		{
			SaitWindowSurveyScanViewListBeginShipSctreke = null;
			DistanceScrankeMin = null;
			DistanceScrankeMax = null;

			if (null == ListEntry)
			{
				return;
			}

			var ListEntryDistance = ListEntry.Distance;

			if (!ListEntryDistance.HasValue)
			{
				return;
			}

			if (null == Automaat)
			{
				return;
			}

			var	WindowSurveyScanView	= Automaat.WindowSurveyScanView();

			if (null == WindowSurveyScanView)
			{
				//	Vorersct nur Scpez für Window SurveyScanView
				return;
			}

			var WindowSurveyScanViewList = WindowSurveyScanView.ListHaupt;

			if (null == WindowSurveyScanViewList)
			{
				return;
			}

			var WindowSurveyScanViewListBeginZait = WindowSurveyScanViewList.ScnapscusFrühesteZait;

			if (!WindowSurveyScanViewListBeginZait.HasValue)
			{
				return;
			}

			SaitWindowSurveyScanViewListBeginShipSctreke =
				Automaat.ShipSctrekeZurükgeleegtMiliInZaitraum(WindowSurveyScanViewListBeginZait.Value - 3333, Automaat.NuzerZaitMili)	/ 1000;

			var SaitWindowSurveyScanViewListBeginShipSctrekePlusSicerhait =
				(SaitWindowSurveyScanViewListBeginShipSctreke * 11) / 10 + 100;

			DistanceScrankeMin = ListEntryDistance - SaitWindowSurveyScanViewListBeginShipSctrekePlusSicerhait;
			DistanceScrankeMax = ListEntryDistance + SaitWindowSurveyScanViewListBeginShipSctrekePlusSicerhait;
		}

		public	void Berecne(
			GbsListGroupedEntryZuusctand ListEntry,
			ISictAutomatZuusctand Automaat)
		{
			this.ListEntry = ListEntry;
			this.Automaat = Automaat;

			Int64? SaitWindowSurveyScanViewListBeginShipSctreke = null;
			Int64? DistanceScrankeMin = null;
			Int64? DistanceScrankeMax = null;
			InRaumObjektType[] MengeInRaumObjekt = null;

			try
			{
				if (null == ListEntry)
				{
					return;
				}

				if (null == Automaat)
				{
					return;
				}

				DistanceScrankeBerecne(ListEntry, Automaat, out	SaitWindowSurveyScanViewListBeginShipSctreke, out	DistanceScrankeMin, out	DistanceScrankeMax);

				var MengeKandidaatInRaumObjekt = new List<InRaumObjektType>();

				MengeKandidaatInRaumObjekt.AddRange(
					Automaat.MengeTarget().OfType<InRaumObjektType>());

				/*
				 * 2015.03.12
				 * 
				if (typeof(SictAuswertGbsTarget) == typeof(InRaumObjektType))
				{
				}
				 * */

				MengeInRaumObjekt =
					MengeKandidaatInRaumObjekt
					.Where((Kandidaat) =>
						Kandidaat.SictungLezteDistanceScrankeMin() <= DistanceScrankeMax &&
						DistanceScrankeMin <= Kandidaat.SictungLezteDistanceScrankeMax())
					.ToArray();
			}
			finally
			{
				this.SaitWindowSurveyScanViewListBeginShipSctreke = SaitWindowSurveyScanViewListBeginShipSctreke;
				this.DistanceScrankeMin = DistanceScrankeMin;
				this.DistanceScrankeMax = DistanceScrankeMax;
				this.MengeInRaumObjekt = MengeInRaumObjekt;
			}
		}

		static public void AktualisiireMengeZuEntryInRaumObjekt<T>(
			IEnumerable<GbsListGroupedEntryZuusctand>	MengeKandidaatEntry,
			IList<T> MengeErgeebnis,
			ISictAutomatZuusctand AutomaatZuustand)
			where T : VerbindungListEntryUndInRaumObjektPerDistance<InRaumObjektType, ScnapscusType>,	new()
		{
			Bib3.Glob.PropagiireListeRepräsentatioon(
				MengeKandidaatEntry,
				MengeErgeebnis,
				(Entry) =>
					{
						var	Ergeebnis	= new T();

						Ergeebnis.Berecne(Entry, AutomaatZuustand);

						return	Ergeebnis;
					},
				(KandidaatRepr, Entry) => KandidaatRepr.ListEntry == Entry,
				null,
				false);
		}
	}
}
