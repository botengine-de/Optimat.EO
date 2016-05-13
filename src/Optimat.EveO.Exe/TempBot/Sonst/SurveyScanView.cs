using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bib3;
using Newtonsoft.Json;

namespace Optimat.EveOnline.Anwendung
{
	[JsonObject(MemberSerialization.OptIn)]
	public class ZuOreTypAusSurveyScanInfo
	{
		[JsonProperty]
		readonly	public string OreTypSictString;

		[JsonProperty]
		public OreTypSictEnum? OreTypSictEnum
		{
			private set;
			get;
		}

		[JsonProperty]
		public	GbsListGroupedEntryZuusctand	EntryGroup
		{
			private set;
			get;
		}

		[JsonProperty]
		public List<GbsListGroupedEntryZuusctand>	ListeEntryItem
		{
			private set;
			get;
		}

		/// <summary>
		/// Der ooberste im lezte Scnapscus sictbaare Entry ist in diise Grupe enthalte.
		/// </summary>
		[JsonProperty]
		public bool? InSurveyScanScnapscusIstOoberste
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? InSurveyScanScnapscusIstUnterste
		{
			private set;
			get;
		}

		static	public bool	 HinraicendGlaicwertigFürFortsaz(
			string Instanz0OreTypSictString,
			string Instanz1OreTypSictString)
		{
			return string.Equals(Bib3.Glob.TrimNullable(Instanz0OreTypSictString), Bib3.Glob.TrimNullable(Instanz1OreTypSictString), StringComparison.InvariantCultureIgnoreCase);
		}

		public ZuOreTypAusSurveyScanInfo()
		{
		}

		public ZuOreTypAusSurveyScanInfo(
			string OreTypSictString)
		{
			this.OreTypSictString = OreTypSictString;
		}

		public void Aktualisiire(ISictAutomatZuusctand Automaat)
		{
			OreTypSictEnum? OreTypSictEnum = null;
			GbsListGroupedEntryZuusctand EntryGroup = null;
			var ListeEntryItem = new List<GbsListGroupedEntryZuusctand>();
			bool? InSurveyScanScnapscusIstOoberste = null;
			bool? InSurveyScanScnapscusIstUnterste = null;

			if (null == Automaat)
			{
				return;
			}

			var OreTypSictString = this.OreTypSictString;

			if (null == OreTypSictString)
			{
				return;
			}

			OreTypSictEnum = TempAuswertGbs.Extension.OreTypBerecneAusOreTypSictString(OreTypSictString);

			var WindowSurveyScanView = Automaat.WindowSurveyScanView();

			if (null == WindowSurveyScanView)
			{
				return;
			}

			var WindowSurveyScanViewList = WindowSurveyScanView.ListHaupt;

			if (null == WindowSurveyScanViewList)
			{
				return;
			}

			try
			{
				var WindowSurveyScanViewListListeEntry = WindowSurveyScanViewList.ListeEntry();

				if (null == WindowSurveyScanViewListListeEntry)
				{
					return;
				}

				foreach (var AusSurveyScanListEntry in WindowSurveyScanViewListListeEntry)
				{
					if (!string.Equals(AusSurveyScanListEntry.OreTypSictString, this.OreTypSictString, StringComparison.InvariantCultureIgnoreCase))
					{
						continue;
					}

					if (AusSurveyScanListEntry.IstGroup ?? false)
					{
						EntryGroup = AusSurveyScanListEntry;
						continue;
					}

					ListeEntryItem.Add(AusSurveyScanListEntry);
				}

				var InScnapscusListeEntrySictbar =
					ListeEntryItem
					.Where((Kandidaat) => Kandidaat.InLezteScnapscusSictbar())
					.OrderBy((Kandidaat) => Kandidaat.LaageB)
					.ToArray();

				var InScnapscusMengeEntrySictbarOoberste =
					InScnapscusListeEntrySictbar.FirstOrDefault();

				var InScnapscusMengeEntrySictbarUnterste =
					InScnapscusListeEntrySictbar.LastOrDefault();

				if (null != InScnapscusMengeEntrySictbarOoberste &&
					null != InScnapscusMengeEntrySictbarUnterste)
				{
					InSurveyScanScnapscusIstOoberste =
						!WindowSurveyScanViewListListeEntry.Any((EntryAndere) =>
							(EntryAndere.InLezteScnapscusSictbar()) &&
							EntryAndere.LaageB < InScnapscusMengeEntrySictbarOoberste.LaageB);

					InSurveyScanScnapscusIstUnterste =
						!WindowSurveyScanViewListListeEntry.Any((EntryAndere) =>
							(EntryAndere.InLezteScnapscusSictbar()) &&
							InScnapscusMengeEntrySictbarUnterste.LaageB < EntryAndere.LaageB);
				}
			}
			finally
			{
				this.OreTypSictEnum = OreTypSictEnum;
				this.EntryGroup = EntryGroup;
				this.InSurveyScanScnapscusIstOoberste = InSurveyScanScnapscusIstOoberste;
				this.InSurveyScanScnapscusIstUnterste = InSurveyScanScnapscusIstUnterste;

				if (null == this.ListeEntryItem)
				{
					this.ListeEntryItem = new List<GbsListGroupedEntryZuusctand>();
				}

				Bib3.Glob.PropagiireListeRepräsentatioonMitReprUndIdentPerClrReferenz(
					ListeEntryItem,
					this.ListeEntryItem);
			}
		}

		static public void MengeZuOreTypZuusctandAktualisiire(
			ISictAutomatZuusctand Automaat,
			IList<ZuOreTypAusSurveyScanInfo> ZiilMenge)
		{
			var ListeOreTypSictString = new List<string>();

			var WindowSurveyScanView = Automaat.WindowSurveyScanView();

			if (null != WindowSurveyScanView)
			{
				var WindowSurveyScanViewList = WindowSurveyScanView.ListHaupt;

				if (null != WindowSurveyScanViewList)
				{
					var WindowSurveyScanViewListListeEntry = WindowSurveyScanViewList.ListeEntry();

					if (null != WindowSurveyScanViewListListeEntry)
					{
						foreach (var ListEntry in WindowSurveyScanViewListListeEntry)
						{
							var ListEntryOreTypSictString = ListEntry.OreTypSictString;

							if (null == ListEntryOreTypSictString)
							{
								continue;
							}

							ListeOreTypSictString.Add(ListEntryOreTypSictString);
						}
					}
				}
			}

			Bib3.Glob.PropagiireListeRepräsentatioon(
				ListeOreTypSictString.Distinct(),
				ZiilMenge,
				(OreTypSictString) => new ZuOreTypAusSurveyScanInfo(OreTypSictString),
				(KandidaatRepr, OreTypSictString) => HinraicendGlaicwertigFürFortsaz(KandidaatRepr.OreTypSictString, OreTypSictString),
				(Repr, Target) => Repr.Aktualisiire(Automaat),
				false);
		}


	}
}
