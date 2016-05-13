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
	public class SictVonAnwendungBerict
	{
		/// <summary>
		/// Zait ist Server.Anwendung.Zait.Mili.
		/// </summary>
		[JsonProperty]
		public WertZuZaitpunktStruct<Bib3.RefNezDiferenz.SictZuNezSictDiferenzScritAbbild>[] MengeAutomaatZuusctandMitZait;

		[JsonProperty]
		public WertZuZaitpunktStruct<Bib3.RefNezDiferenz.SictZuNezSictDiferenzScritAbbild>[] MengeAutomaatZuusctandDiferenzScritMitZait;

		public SictVonAnwendungBerict()
		{
		}

		static public SictVonAnwendungBerict KonstruktFürAutomaatZuusctand(
			WertZuZaitpunktStruct<Bib3.RefNezDiferenz.SictZuNezSictDiferenzScritAbbild> AutomaatZuusctand)
		{
			return new SictVonAnwendungBerict()
			{
				MengeAutomaatZuusctandMitZait = new[] { AutomaatZuusctand },
			};
		}

		static public SictVonAnwendungBerict KonstruktFürAutomaatZuusctandDiferenzScrit(
			WertZuZaitpunktStruct<Bib3.RefNezDiferenz.SictZuNezSictDiferenzScritAbbild> AutomaatZuusctandDiferenzScritMitZait)
		{
			return new SictVonAnwendungBerict()
			{
				MengeAutomaatZuusctandDiferenzScritMitZait = new[] { AutomaatZuusctandDiferenzScritMitZait },
			};
		}

	}

}
