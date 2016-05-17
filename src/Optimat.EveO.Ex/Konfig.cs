using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Optimat.EveO.Nuzer
{
	[JsonObject(MemberSerialization.OptIn)]
	public class SictZiilProcessWirkungKonfig
	{
		[JsonProperty]
		public System.Windows.Input.Key[][] MengeKeyKombiWirkungUnterbrece;
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictKonfiguratioon
	{
		[JsonProperty]
		public string ScnitOptimatServerLizenzDataiPfaad;

		[JsonProperty]
		public string SensorServerApiUri;

		[JsonProperty]
		public Int64? ScnitOptimatServerVersuucVerbindungZaitDistanz;

		[JsonProperty]
		public string BerictSizungPersistVerzaicnisPfaad;

		[JsonProperty]
		public string BerictHauptPersistVerzaicnisPfaad;

		[JsonProperty]
		public Int64? BerictHauptPersistKapazitäätScranke;

		[JsonProperty]
		public Int64? BerictHauptPersistMengeDataiAnzaalScranke;

		[JsonProperty]
		public string BerictWindowClientRasterPersistVerzaicnisPfaad;

		[JsonProperty]
		public Int64? BerictWindowClientRasterPersistKapazitäätScranke;

		[JsonProperty]
		public SictAuswaalWindowsProcessPräferenz AuswaalZiilProcess;

		[JsonProperty]
		public System.Windows.Input.Key[][] ZiilProcessWirkungPauseMengeKeyKombi;

		[JsonProperty]
		public int? CustomBotAdreseTcp;
	}
}
