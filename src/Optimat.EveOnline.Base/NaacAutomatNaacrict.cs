using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Optimat.EveOnline.AuswertGbs;

namespace Optimat.EveOnline.Base
{
	[JsonObject(MemberSerialization.OptIn)]
	public class NaacAutomatMeldungZuusctand
	{
		[JsonProperty]
		public Int64 NuzerZaitMili;

		[JsonProperty]
		public Int64 BerecnungVorsclaagZaitScrankeMin;

		[JsonProperty]
		public IList<SictNaacProcessWirkung> ListeNaacProcessWirkung;

		[JsonProperty]
		public IList<SictVerlaufBeginUndEndeRefMitZaitAinhaitMili<int>> ListeVonProcessLeese;

		[JsonProperty]
		public VonSensorikMesung VonSensorikScnapscus;
	}

	/*
	 * 2015.03.26
	 * 
	[JsonObject(MemberSerialization.OptIn)]
	public class NaacAutomatNaacrict
	{
		[JsonProperty]
		public Int64? NuzerZaitMili;

		/// <summary>
		/// Vorersct werd GbsBaum hiir noc nit abgebildet da beraits bescteehende für GbsAst CLR Type spezialisiirte Implementatioon kompaktere serialisatioon bringt.
		/// </summary>
		[JsonProperty]
		public Bib3.RefNezDiferenz.SictRefNezDiferenzScritSictJson ZuusctandDiferenz;
	}
	 * */
}
