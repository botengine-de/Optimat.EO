using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Optimat.EveOnline.Base
{
	[JsonObject(MemberSerialization.OptIn)]
	public class VonAutomatMeldungZuusctand
	{
		[JsonProperty]
		public byte[] SizungIdent;

		[JsonProperty]
		public Int64? BerecnungVorsclaagLezteZait;

		[JsonProperty]
		public Int64 MesungNääxteZaitScrankeMin;

		[JsonProperty]
		public	List<SictVorsclaagNaacProcessWirkung> VorsclaagListeWirkung;

	}

	/*
	 * 2015.03.26
	 * 
	[JsonObject(MemberSerialization.OptIn)]
	public class VonAutomatNaacrict
	{
		[JsonProperty]
		public Bib3.RefNezDiferenz.SictRefNezDiferenzScritSictJson ZuusctandDiferenz;

		public VonAutomatNaacrict()
		{
		}

		public VonAutomatNaacrict(
			Bib3.RefNezDiferenz.SictRefNezDiferenzScritSictJson ZuusctandDiferenz)
		{
			this.ZuusctandDiferenz = ZuusctandDiferenz;
		}
	}
	 * */

}
