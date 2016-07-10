using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Optimat.EveOnline.Anwendung
{
	/*
	 * 2014.06.11
	 * 
	 * Ersaz durc Klase aus Optimat.EveOnline
	 * 
	[JsonObject(MemberSerialization.OptIn)]
	public	class SictStation
	{
		[JsonProperty]
		public bool? RepairVerfüügbar
		{
			private set;
			get;
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public	class SictSolarSystem
	{
		[JsonProperty]
		readonly List<SictStation> MengeStation = new List<SictStation>();

	}

	[JsonObject(MemberSerialization.OptIn)]
	public	class SictTopologii
	{
		[JsonProperty]
		readonly List<SictSolarSystem> MengeSolarSystem = new List<SictSolarSystem>();

		IEnumerable<SictSolarSystem> PfaadBerecne(
			SictSolarSystem PfaadBegin,
			SictSolarSystem PfaadEnde)
		{
			throw new NotImplementedException();
		}
	}
	 * */
}
