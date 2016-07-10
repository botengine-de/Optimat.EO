using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Optimat.EveOnline
{
	[JsonObject(MemberSerialization.OptIn)]
	public class SictStation
	{
		[JsonProperty]
		readonly	public string StationName;

		[JsonProperty]
		public bool? RepairVerfüügbar;

		public SictStation()
		{
		}

		public SictStation(
			string StationName)
		{
			this.StationName = StationName;
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictAsteroidBelt
	{
		[JsonProperty]
		readonly	public string Bescriftung;

		public SictAsteroidBelt()
		{
		}

		public SictAsteroidBelt(
			string Bescriftung)
		{
			this.Bescriftung = Bescriftung;
		}

		static public bool Glaicwertig(
			SictAsteroidBelt O0,
			SictAsteroidBelt O1)
		{
			if (object.ReferenceEquals(O0, O1))
			{
				return true;
			}

			if (null == O0 || null == O1)
			{
				return false;
			}

			return Glaicwertig(O0, O1.Bescriftung);
		}
		static public bool Glaicwertig(
			SictAsteroidBelt O0,
			string	O1Bescriftung)
		{
			if (null == O0)
			{
				return false;
			}

			return string.Equals(O0.Bescriftung, O1Bescriftung, StringComparison.InvariantCultureIgnoreCase);
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictSolarSystem
	{
		[JsonProperty]
		readonly public string SystemName;

		[JsonProperty]
		readonly	public List<SictStation> MengeStation	= new	List<SictStation>();

		[JsonProperty]
		readonly public List<SictAsteroidBelt> MengeAsteroidBelt = new List<SictAsteroidBelt>();

		[JsonProperty]
		public int? SecurityLevelMili;

		public SictSolarSystem()
		{
		}

		public SictSolarSystem(
			string SystemName)
		{
			this.SystemName = SystemName;
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictEveWeltTopologii
	{
		[JsonProperty]
		readonly	public List<SictSolarSystem> MengeSolarSystem = new List<SictSolarSystem>();

		IEnumerable<SictSolarSystem> PfaadBerecne(
			SictSolarSystem PfaadBegin,
			SictSolarSystem PfaadEnde)
		{
			throw new NotImplementedException();
		}

		public SictEveWeltTopologii()
		{
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictVonOptimatMeldungZuusctand : Optimat.EveOnline.Base.VonAutomatMeldungZuusctand
	{
		[JsonProperty]
		public KeyValuePair<string, SictNaacNuzerMeldungZuEveOnlineSeverity>[] MengeMeldungGenerel;

		[JsonProperty]
		public List<SictNaacNuzerMeldungZuEveOnline> MengeMeldungZuEveOnline;

		[JsonProperty]
		public SictEveWeltTopologii EveWeltTopologii;

		[JsonProperty]
		public ShipState ShipZuusctand;

		[JsonProperty]
		public SictAusGbsLocationInfo CurrentLocation;

		[JsonProperty]
		public string FittingInfoAgrString;

		[JsonProperty]
		public List<SictMissionZuusctand> MengeMission;

		[JsonProperty]
		public string EveOnlineZaitKalenderModuloTaagString;

		[JsonProperty]
		public	KeyValuePair<int,	int>? EveOnlineZaitKalenderModuloTaagMinMax;

		[JsonProperty]
		public List<string[]> FittingManagementMengeFittingPfaadListeGrupeNaame;

		[JsonProperty]
		public SictKonfigMissionZuMissionFilterVerhalte[] MengeZuMissionFilterAktioonVerfüügbar;

	}
}
