using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Bib3;
using System.Text.RegularExpressions;
using ExtractFromOldAssembly.Bib3;
//using Optimat.EveOnline.AuswertGbs;

namespace Optimat.EveOnline.Anwendung
{
	[JsonObject(MemberSerialization.OptIn)]
	public class GbsListGroupedEntryZuusctand :
		SictZuGbsObjektZuusctandMitIdentPerIntMitZuusazInfo<VonSensor.ScnapscusListEntry, SictGbsAgregatioonAusTransitionInfo>
	{
		[JsonProperty]
		public GbsListGroupedEntryZuusctand[] ListeEntryEnthalte
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool?	IstExpanded
		{
			private set;
			get;
		}

		[JsonProperty]
		public string Name
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? Distance
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? Quantity
		{
			private set;
			get;
		}

		[JsonProperty]
		public string OreTypSictString
		{
			private set;
			get;
		}

		[JsonProperty]
		public OreTypSictEnum? OreTyp
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? VolumeMili
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? OreVolumeMili
		{
			private set;
			get;
		}

		public bool? IstGroup
		{
			get
			{
				var ScnapscusLezte = AingangScnapscusTailObjektIdentLezteBerecne();

				if (null == ScnapscusLezte)
				{
					return null;
				}

				return ScnapscusLezte.IstGroup;
			}
		}

		public string	Bescriftung
		{
			get
			{
				var ScnapscusUnglaicDefaultLezte = AingangScnapscusUnglaicDefaultTailObjektIdentLezteBerecne();

				if (null == ScnapscusUnglaicDefaultLezte)
				{
					return null;
				}

				return ScnapscusUnglaicDefaultLezte.Bescriftung;
			}
		}

		public string BescriftungTailTitel
		{
			get
			{
				var ScnapscusUnglaicDefaultLezte = AingangScnapscusUnglaicDefaultTailObjektIdentLezteBerecne();

				if (null == ScnapscusUnglaicDefaultLezte)
				{
					return null;
				}

				return ScnapscusUnglaicDefaultLezte.BescriftungTailTitel;
			}
		}

		public int? LaageB
		{
			get
			{
				return	(int?)AingangScnapscusTailObjektIdentLezteBerecne().InGbsFläceMiteLaageB();
			}
		}

		public GbsListGroupedEntryZuusctand()
			:
			this(-1,null)
		{
		}

		public GbsListGroupedEntryZuusctand(
			Int64	BeginZait,
			VonSensor.ScnapscusListEntry ScnapscusEntry)
			:
			base(
			2,
			BeginZait,
			ScnapscusEntry)
		{
		}

		public GbsListGroupedEntryZuusctand(
			SictWertMitZait<VonSensor.ScnapscusListEntry> ScnapscusEntryMitZait)
			:
			this(
			ScnapscusEntryMitZait.Zait,
			(null	== ScnapscusEntryMitZait.Wert)	?	null	: ScnapscusEntryMitZait.Wert)
		{
		}

		override	protected void NaacAingangScnapscus(
			Int64 ScnapscusZait,
			VonSensor.ScnapscusListEntry ScnapscusEntry,
			SictGbsAgregatioonAusTransitionInfo	ZuusazInfo)
		{
			Aktualisiire(ScnapscusZait, ScnapscusEntry, ZuusazInfo);
		}

		void Aktualisiire(
			Int64 Zait,
			VonSensor.ScnapscusListEntry ScnapscusEntry,
			SictGbsAgregatioonAusTransitionInfo ZuusazInfo)
		{
			bool? IstExpanded = null;

			try
			{
				if (null == ScnapscusEntry)
				{
					return;
				}

				Name = ScnapscusEntry.Name ?? Name;
				Distance = ScnapscusEntry.Distance ?? Distance;
				Quantity = ScnapscusEntry.Quantity ?? Quantity;
				OreTypSictString = ScnapscusEntry.OreTypSictString ?? OreTypSictString;
				OreTyp = ScnapscusEntry.OreTypSictEnum ?? OreTyp;
				VolumeMili = ScnapscusEntry.VolumeMili ?? VolumeMili;
				OreVolumeMili = ScnapscusEntry.OreVolumeMili ?? OreVolumeMili;

				IstExpanded = ScnapscusEntry.IsExpanded;

				if (null == ZuusazInfo)
				{
					return;
				}

				/*
				 * 2015.02.26
				 * 
				 * Ersaz durc ScnapscusEntry.IsExpanded
				 * 
				 * 
				var	EntryGroupExpanderTexturIdent	= ScnapscusEntry.GroupExpanderTexturIdent;

				if (EntryGroupExpanderTexturIdent.HasValue)
				{
					var	EntryGroupExpanderTexturIdentBedoitung	=	ZuusazInfo.ZuTexturIdentBedoitungLezteMitZait(EntryGroupExpanderTexturIdent.Value);

					if (EntryGroupExpanderTexturIdentBedoitung.HasValue)
					{
						switch (EntryGroupExpanderTexturIdentBedoitung.Value.Wert)
						{
							case SictGbsSymbolBedoitung.Expanded:
								IstExpanded = true;
								break;
							case SictGbsSymbolBedoitung.Collapsed:
								IstExpanded = false;
								break;
						}
					}
				}
				 * */
			}
			finally
			{
				this.IstExpanded = IstExpanded;
			}
		}

		public void ListeEntryEnthalteSeze(
			GbsListGroupedEntryZuusctand[] ListeEntryEnthalte)
		{
			this.ListeEntryEnthalte = ListeEntryEnthalte;
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class GbsListGroupedZuusctand : SictZuGbsObjektZuusctandMitIdentPerIntMitZuusazInfo<VonSensor.ScnapscusListGrouped,	SictGbsAgregatioonAusTransitionInfo>
	{
		[JsonProperty]
		readonly List<GbsListGroupedEntryZuusctand> InternListeEntry = new List<GbsListGroupedEntryZuusctand>();

		public IEnumerable<GbsListGroupedEntryZuusctand> ListeEntry()
		{
			return
				InternListeEntry
				.OrderByNullable((Entry) => Entry.LaageB)
				.ToArrayNullable();
		}

		public IEnumerable<GbsListGroupedEntryZuusctand> ListeEntrySctrukturiirt()
		{
			return
				InternListeEntry
				.OrderByNullable((Entry) => Entry.LaageB)
				.ToArrayNullable();
		}

		public GbsListGroupedZuusctand()
			:
			this(-1,null)
		{
		}

		public GbsListGroupedZuusctand(
			Int64	BeginZait,
			VonSensor.ScnapscusListGrouped ScnapscusList)
			:
			base(
			2,
			BeginZait,
			ScnapscusList)
		{
		}

		public GbsListGroupedZuusctand(
			SictWertMitZait<VonSensor.ScnapscusListGrouped> ScnapscusListMitZait)
			:
			this(
			ScnapscusListMitZait.Zait,
			(null	== ScnapscusListMitZait.Wert)	?	null	: ScnapscusListMitZait.Wert)
		{
		}

		override	protected void NaacAingangScnapscus(
			Int64 ScnapscusZait,
			VonSensor.ScnapscusListGrouped ScnapscusWert,
			SictGbsAgregatioonAusTransitionInfo ZuusazInfo)
		{
			Aktualisiire(ScnapscusZait, ScnapscusWert, ZuusazInfo);
		}

		void Aktualisiire(
			Int64 Zait,
			VonSensor.ScnapscusListGrouped ScnapscusList,
			SictGbsAgregatioonAusTransitionInfo ZuusazInfo)
		{
			var	ScnapscusListeEntry	= (null	== ScnapscusList)	? null	: ScnapscusList.ListeEntry;

			GbsListGroupedEntryZuusctand.ZuZaitAingangMengeObjektScnapscus(
				Zait,
				ScnapscusListeEntry,
				InternListeEntry,
				false,
				ZuusazInfo);

			//	!!!!	zu ergänze: entferne von Entry welce (nit sictbar) UND (in Entry enthalte der als Expanded angenome werd).
		}

		public string ZuHeaderRegexUndItemBerecneZeleWert(
			Regex HeaderRegex,
			GbsListGroupedEntryZuusctand	EntryZuustand)
		{
			if (null == EntryZuustand)
			{
				return null;
			}

			return ZuHeaderRegexUndItemBerecneZeleWert(HeaderRegex, EntryZuustand.AingangScnapscusTailObjektIdentLezteBerecne());
		}

		public string ZuHeaderRegexUndItemBerecneZeleWert(
			Regex HeaderRegex,
			VonSensor.ScnapscusListEntry Item)
		{
			var	ScnapscusLezte	= AingangScnapscusTailObjektIdentLezteBerecne();

			if (null == ScnapscusLezte)
			{
				return null;
			}

			return Optimat.EveOnline.Extension.ZuHeaderRegexUndItemBerecneZeleWert(HeaderRegex, Item);
		}
	}
}
