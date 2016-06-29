using System.Linq;
using Newtonsoft.Json;
using Optimat.EveOnline.AuswertGbs;
using ExtractFromOldAssembly.Bib3;

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
