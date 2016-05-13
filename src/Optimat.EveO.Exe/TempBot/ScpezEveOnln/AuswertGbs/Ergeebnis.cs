using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bib3;
using Newtonsoft.Json;
using Optimat.EveOnline.AuswertGbs;

namespace Optimat.EveOnline.Anwendung.AuswertGbs
{
	public class SictAusGbsScnapscusAuswertungSrv : VonSensorikMesung
	{
		static readonly Bib3.RefNezDiferenz.SictTypeBehandlungRictliinieMitTransportIdentScatescpaicer
			RictliinieScatescpaicer =
			new Bib3.RefNezDiferenz.SictTypeBehandlungRictliinieMitTransportIdentScatescpaicer(
			Bib3.RefNezDiferenz.NewtonsoftJson.SictMengeTypeBehandlungRictliinieNewtonsoftJson.KonstruktMengeTypeBehandlungRictliinie());

		[JsonProperty]
		public ModuleButtonHintInterpretiirt ModuleButtonHintInterpretiirt;

		[JsonProperty]
		public SictWindowInventoryVerknüpfungMitOverview[] MengeWindowInventoryVerknüpfungMitOverview;

		new	public	SictAusGbsScnapscusAuswertungSrv	Kopii()
		{
			return SictRefBaumKopii.ObjektKopiiErsctele(this);
		}

		public SictAusGbsScnapscusAuswertungSrv()
		{
		}

		public SictAusGbsScnapscusAuswertungSrv(
			VonSensorikMesung Base)
		{
			Bib3.SictRefNezKopii.KopiireFlacVon(
				this,
				Base,
				RictliinieScatescpaicer);

			ModuleButtonHintInterpretiirt = ModuleButtonHintInterpretiirt.Interpretiire(ModuleButtonHint);

			if (null != WindowOverview)
			{
				var AusWindowOverviewListeZaile = WindowOverview.AusTabListeZaileOrdnetNaacLaage;

				if (null != MengeWindowInventory)
				{
					this.MengeWindowInventoryVerknüpfungMitOverview =
						MengeWindowInventory.Select((WindowInventory) =>
							SictWindowInventoryVerknüpfungMitOverview.Ersctele(WindowInventory, AusWindowOverviewListeZaile)).ToArray();
				}
			}

		}
	}
}
