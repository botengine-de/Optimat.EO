using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
//using Optimat.EveOnline.AuswertGbs;
using Optimat.EveOnline.Base;
using Optimat.EveOnline.VonSensor;

namespace Optimat.EveOnline.Anwendung.AuswertGbs
{
	[JsonObject(MemberSerialization.OptIn)]
	public class SictWindowInventoryVerknüpfungMitOverview
	{
		[JsonProperty]
		public VonSensor.WindowInventoryPrimary WindowInventory
		{
			private set;
			get;
		}

		/// <summary>
		/// Zu aus deem linke Baum TreeEntry jewails di Liste der OverView Zaile welce das Objekt repräsentiire könten welces durc das betrefende TreeEntry repräsentiirt werd.
		/// </summary>
		[JsonProperty]
		public KeyValuePair<VonSensor.TreeViewEntry, VonSensor.OverviewZaile[]>[] LinxTreeMengeZuEntryMengeKandidaatOverviewZaile
		{
			private set;
			get;
		}

		[JsonProperty]
		public VonSensor.OverviewZaile[] ZuAuswaalReczMengeKandidaatOverviewZaile
		{
			private set;
			get;
		}

		public SictWindowInventoryVerknüpfungMitOverview()
		{
		}

		public SictWindowInventoryVerknüpfungMitOverview(
			VonSensor.WindowInventoryPrimary WindowInventory,
			KeyValuePair<VonSensor.TreeViewEntry, VonSensor.OverviewZaile[]>[] LinxTreeMengeZuEntryMengeKandidaatOverviewZaile,
			VonSensor.OverviewZaile[] ZuAuswaalReczMengeKandidaatOverviewZaile)
		{
			this.WindowInventory = WindowInventory;
			this.LinxTreeMengeZuEntryMengeKandidaatOverviewZaile = LinxTreeMengeZuEntryMengeKandidaatOverviewZaile;
			this.ZuAuswaalReczMengeKandidaatOverviewZaile = ZuAuswaalReczMengeKandidaatOverviewZaile;
		}

		static public SictWindowInventoryVerknüpfungMitOverview Ersctele(
			VonSensor.WindowInventoryPrimary WindowInventory,
			VonSensor.OverviewZaile[] MengeOverviewZaile)
		{
			var LinxTreeMengeZuEntryMengeKandidaatOverviewZaile = new List<KeyValuePair<VonSensor.TreeViewEntry, VonSensor.OverviewZaile[]>>();

			var LinxTreeListeEntry = (null == WindowInventory) ? null : WindowInventory.LinxTreeListeEntry;

			if (null != LinxTreeListeEntry && null != MengeOverviewZaile)
			{
				foreach (var LinxTreeEntry in LinxTreeListeEntry)
				{
					if (null == LinxTreeEntry)
					{
						continue;
					}

					var TreeEntryObjektName = LinxTreeEntry.LabelTextTailObjektName;
					var TreeEntryObjektDistanceSictString = LinxTreeEntry.LabelTextTailObjektDistance;
					var LinxTreeEntryTopContIconColor = LinxTreeEntry.TopContIconColor;

					if (null == TreeEntryObjektName)
					{
						continue;
					}

					Int64? TreeEntryObjektDistanceScrankeMin;
					Int64? TreeEntryObjektDistanceScrankeMax;

					TempAuswertGbs.Extension.AusDistanceSictStringAbbildGanzzaalDurcMeeter(
						TreeEntryObjektDistanceSictString, out	TreeEntryObjektDistanceScrankeMin, out	TreeEntryObjektDistanceScrankeMax);

					var FürEntryMengeOverviewZaile = MengeOverviewZaile.Take(0).ToList();

					foreach (var OverviewZaile in MengeOverviewZaile)
					{
						if (null == OverviewZaile)
						{
							continue;
						}

						var OverviewZaileAusOverviewZaile = OverviewZaile;

						if (null == OverviewZaileAusOverviewZaile)
						{
							continue;
						}

						/*
						 * 2014.04.28
						 * Beobactung Probleem in Test:
						 * \\rs211275.rs.hosteurope.de\Optimat.Demo 0 .Berict\Berict\Berict.Nuzer\[ZAK=2014.04.28.00.53.21,NB=1].Anwendung.Berict
						 * Agent.Name:"Rulie Isoryn"
						 * Agent.Level:1
						 * Mission.Titel:"Unauthorized Military Presence"
						 * 
						 * OverviewZaile Type und Name geegenüüber bisherige Tests vertausct:
						 * 
						 * TreeEntryObjektName="Caldari Personnel Transport Wreck"
						 * OverviewZaileAusOverviewZaile.AusZaileTypeWert="Caldari Personnel Transport Wreck"
						 * OverviewZaileAusOverviewZaile.AusZaileNameWert="Caldari Medium Wreck"
						 * 
						if (!string.Equals(OverviewZaileAusOverviewZaile.AusZaileNameWert, TreeEntryObjektName))
						{
							continue;
						}
						 * */

						bool TreeEntryObjektNamePasend = false;

						if (string.Equals(TreeEntryObjektName, OverviewZaileAusOverviewZaile.Type, StringComparison.InvariantCultureIgnoreCase))
						{
							TreeEntryObjektNamePasend = true;
						}

						if (string.Equals(TreeEntryObjektName, OverviewZaileAusOverviewZaile.Name, StringComparison.InvariantCultureIgnoreCase))
						{
							TreeEntryObjektNamePasend = true;
						}

						if (!TreeEntryObjektNamePasend)
						{
							continue;
						}

						if (!(OverviewZaile.DistanceMax < (TreeEntryObjektDistanceScrankeMin * 11) / 10 + 100) ||
							!(OverviewZaile.DistanceMin > (TreeEntryObjektDistanceScrankeMax * 10) / 11 - 100))
						{
							continue;
						}

						var AusOverviewIconMainColor = OverviewZaileAusOverviewZaile.IconMainColor;

						var HueDistanz = FarbeARGB.HueDistanzMiliBerecne(AusOverviewIconMainColor, LinxTreeEntryTopContIconColor);

						if (!(Optimat.Glob.Betraag(HueDistanz) < 10))
						{
							/*
							 * 16.04.26
							 * Seems color in inventory does not anymore match color in overview.
							 * 
							//	Farbtoon mus sco äänlic sain.
							continue;
							*/
						}

						FürEntryMengeOverviewZaile.Add(OverviewZaile);
					}

					LinxTreeMengeZuEntryMengeKandidaatOverviewZaile.Add(new KeyValuePair<VonSensor.TreeViewEntry, VonSensor.OverviewZaile[]>(
						LinxTreeEntry, FürEntryMengeOverviewZaile.ToArray()));
				}
			}

			VonSensor.OverviewZaile[] ZuAuswaalReczMengeKandidaatOverviewZaile = null;

			if (null != WindowInventory)
			{
				var ZuAuswaalReczMengeKandidaatLinxTreeEntry = WindowInventory.ZuAuswaalReczMengeKandidaatLinxTreeEntry;

				if (null != ZuAuswaalReczMengeKandidaatLinxTreeEntry)
				{
					ZuAuswaalReczMengeKandidaatOverviewZaile =
						Bib3.Glob.ArrayAusListeListeGeflact(
						LinxTreeMengeZuEntryMengeKandidaatOverviewZaile
						.Where((Kandidaat) => ZuAuswaalReczMengeKandidaatLinxTreeEntry.Contains(Kandidaat.Key))
						.Select((Kandidaat) => Kandidaat.Value));
				}
			}

			return new SictWindowInventoryVerknüpfungMitOverview(
				WindowInventory,
				LinxTreeMengeZuEntryMengeKandidaatOverviewZaile.ToArray(),
				ZuAuswaalReczMengeKandidaatOverviewZaile);
		}
	}

}
