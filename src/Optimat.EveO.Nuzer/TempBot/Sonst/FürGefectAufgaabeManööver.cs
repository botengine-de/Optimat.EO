using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Optimat.EveOnline.Anwendung
{
	[JsonObject(MemberSerialization.OptIn)]
	public	class SictFürGefectAufgaabeManööverErgeebnis
	{
		[JsonProperty]
		readonly public SictAufgaabeParam AufgaabeParam;

		[JsonProperty]
		readonly public SictVerlaufBeginUndEndeRef<Optimat.EveOnline.Anwendung.AuswertGbs.ShipUiIndicationAuswert> ShipUIIndication;

		public SictOverViewObjektZuusctand OverViewObjekt
		{
			get
			{
				var AufgaabeParam = this.AufgaabeParam;

				if (null == AufgaabeParam)
				{
					return null;
				}

				return AufgaabeParam.OverViewObjektZuBearbaiteVirt();
			}
		}

		public SictFürGefectAufgaabeManööverErgeebnis()
		{
		}

		public SictFürGefectAufgaabeManööverErgeebnis(
			SictAufgaabeParam AufgaabeParam,
			SictVerlaufBeginUndEndeRef<Optimat.EveOnline.Anwendung.AuswertGbs.ShipUiIndicationAuswert> ShipUIIndication)
		{
			this.AufgaabeParam = AufgaabeParam;
			this.ShipUIIndication = ShipUIIndication;
		}
	}
}
