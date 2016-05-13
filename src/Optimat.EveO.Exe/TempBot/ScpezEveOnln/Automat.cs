using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Bib3;
using Newtonsoft.Json;
using Optimat.EveOnline;
using Optimat.EveOnline.Anwendung;
using Optimat.EveOnline.Anwendung.AuswertGbs;
using Optimat.EveOnline.CustomBot;
//using Optimat.EveOnline.AuswertGbs;
using VonSensor = Optimat.EveOnline.VonSensor;


namespace Optimat.ScpezEveOnln
{
	[JsonObject(MemberSerialization.OptIn)]
	public class SictCargoÜbernaame
	{
		[JsonProperty]
		public string KweleObjektNaame;

		[JsonProperty]
		public VonSensor.OverviewZaile ObjektAusOverviewZaile;

		[JsonProperty]
		public string[] MengeItemÜbernome;

		public SictCargoÜbernaame()
		{
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictInventoryItemTransport
	{
		[JsonProperty]
		readonly public VonSensor.WindowInventoryPrimary KweleWindowInventory;

		[JsonProperty]
		readonly public VonSensor.TreeViewEntry ZiilObjektTreeViewEntry;

		[JsonProperty]
		readonly public VonSensor.InventoryItem[] MengeItem;

		public SictInventoryItemTransport()
		{
		}

		public SictInventoryItemTransport(
			VonSensor.WindowInventoryPrimary KweleWindowInventory,
			VonSensor.TreeViewEntry ZiilObjektTreeViewEntry,
			VonSensor.InventoryItem[] MengeItem = null)
		{
			this.KweleWindowInventory = KweleWindowInventory;
			this.ZiilObjektTreeViewEntry = ZiilObjektTreeViewEntry;
			this.MengeItem = MengeItem;
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public partial class SictVersuucInventoryItemDrag
	{
		[JsonProperty]
		public VonSensor.WindowInventoryPrimary WindowInventory;

		[JsonProperty]
		public VonSensor.InventoryItem DragItem;

		[JsonProperty]
		public VonSensor.TreeViewEntry DragZiil;

		public SictVersuucInventoryItemDrag()
			:
			this(null, null, null)
		{
		}

		public SictVersuucInventoryItemDrag(
			VonSensor.WindowInventoryPrimary WindowInventory,
			VonSensor.InventoryItem DragItem,
			VonSensor.TreeViewEntry DragZiil)
		{
			this.WindowInventory = WindowInventory;
			this.DragItem = DragItem;
			this.DragZiil = DragZiil;
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictNaacNuzerBerict
	{
		[JsonProperty]
		public IDictionary<Int64, SictWertMitZait<EveOnline.SictMissionZuusctand>> DictZuMissionZuZaitBerictLezte;

		[JsonProperty]
		public List<KeyValuePair<int, SictVonServerBerict>> ListeNaacNuzerNaacrict;

		public int? ListeNaacNuzerNaacrictLezteBezaicnerBerecne()
		{
			var ListeNaacNuzerNaacrict = this.ListeNaacNuzerNaacrict;

			if (null == ListeNaacNuzerNaacrict)
			{
				return null;
			}

			var ListeNaacNuzerNaacrictLezte = ListeNaacNuzerNaacrict.LastOrDefault();

			return ListeNaacNuzerNaacrictLezte.Key;
		}

		public SictNaacNuzerBerict()
		{
		}
	}

	public partial class SictAutomat
	{
		readonly static byte[][] StaticMengeClientIdentAktuel = new byte[][] {
			//	2015.02.26.12
			new byte[]{0x79,0x7F,0x58,0xC9,0xA7,0x53,0xBF,0x58,0x65,0x24,0x42,0x2D,0x87,0x35,0xBF,0xFB,0xD6,0xD1,0x32,0x5E},
			new byte[]{4},
		};

		public byte[] AnwendungSizungIdent
		{
			set;
			get;
		}

		readonly public byte[][] MengeClientIdentAktuel = StaticMengeClientIdentAktuel;

		public Int64? NuzerZaitMili
		{
			set;
			get;
		}

		public Int64? ServerZaitMili
		{
			set;
			get;
		}

		public KeyValuePair<Int64, Int64>[] TempDebugListeScritBerecneStopwatchZaitUndNuzerZait
		{
			set;
			get;
		}

		public SictNaacOptimatMeldungZuusctand VonNuzerMeldungZuusctand;

		public BotEngine.EveOnline.Interface.FromSensorToConsumerMessage VonSensorikMesungLezte;

		/*
		 * 2015.03.12
		 * 
		 * Ersaz durc ToCustomBotSnapshot

		public SictGbsAstInfoSictAuswert VonNuzerMeldungZuusctandTailGbsBaum;

		public SictWertMitZait<SictAuswertGbsAgr> ScnapscusAuswertLezte
		{
			private set;
			get;
		}
		 * */

		public SictVonOptimatMeldungZuusctand NaacNuzerMeldungZuusctand
		{
			get
			{
				var InternZuusctand = this.InternZuusctand;

				if (null == InternZuusctand)
				{
					return null;
				}

				return InternZuusctand.NaacNuzerMeldungZuusctand;
			}
		}

		readonly ISictAutomatZuusctandMitErwaiterungFürBerict InternZuusctand = new SictAutomatZuusctand();

		public SictAutomat()
		{
		}

		public SictAutomat(ISictAutomatZuusctandMitErwaiterungFürBerict AutomaatZuusctand)
		{
			this.InternZuusctand = AutomaatZuusctand;
		}

		static public KeyValuePair<Type, Type>[] ZuusctandSictFürNuzerMengeZuHerkunftTypeZiilTypeBerecne()
		{
			var MengeZuHerkunftTypeZiilTypeBerecne = new Dictionary<Type, Type>();

			var HerkunftAssembly = System.Reflection.Assembly.GetExecutingAssembly();

			var BaseTypeAssembly = System.Reflection.Assembly.GetAssembly(typeof(Optimat.EveOnline.SictMissionZuusctand));

			var HerkunftMengeType = HerkunftAssembly.GetTypes();

			var MengeZuHerkunftTypeZiilType =
				Bib3.Extension.WhereNullable(
				Optimat.Glob.ZuMengeTypeBerecneBaseTypeAusAssembly(HerkunftMengeType, BaseTypeAssembly),
				(Kandidaat) => null != Kandidaat.Value)
				.ToArrayNullable();

			if (null == MengeZuHerkunftTypeZiilType)
			{
				throw new ArgumentNullException("MengeZuHerkunftTypeZiilType");
			}

			return MengeZuHerkunftTypeZiilType;
		}

		static public readonly Bib3.RefNezDiferenz.IZuTypeEntscaidungBinäär ZuusctandSictFürNuzerTypeAbbildFraigaabeRictliinie =
			new Bib3.RefNezDiferenz.ZuTypeEntscaidungBinäärEnthalteInAssemblyOderReferenziirte(
				new System.Reflection.Assembly[]{
				typeof(Optimat.EveOnline.SictVonOptimatMeldungZuusctand).Assembly,
					//	2015.03.12	typeof(Optimat.EveOnline.SictFläceRectekOrtoAbhängigVonGbsAst).Assembly,
				}, true);

		static public Bib3.RefNezDiferenz.SictDiferenzSictParam ZuusctandSictFürNuzerSictParamBerecne()
		{
			var MengeZuHerkunftTypeZiilType = ZuusctandSictFürNuzerMengeZuHerkunftTypeZiilTypeBerecne();

			var Param = Bib3.RefNezDiferenz.NewtonsoftJson.SictMengeTypeBehandlungRictliinieNewtonsoftJson.KonstruktSictParam(
				MengeZuHerkunftTypeZiilType, ZuusctandSictFürNuzerTypeAbbildFraigaabeRictliinie);

			return Param;
		}

		static public Bib3.RefNezDiferenz.SictDiferenzSictParam ZuusctandSictDiferenzSictParamBerecne()
		{
			var Param = Bib3.RefNezDiferenz.NewtonsoftJson.SictMengeTypeBehandlungRictliinieNewtonsoftJson.KonstruktSictParam();

			ZuusctandSictDiferenzRictliinieScatescpaicer = Param.TypeBehandlungRictliinieMitScatescpaicer;

			return Param;
		}

		static public Bib3.RefNezDiferenz.SictTypeBehandlungRictliinieMitTransportIdentScatescpaicer ZuusctandSictDiferenzRictliinieScatescpaicer
		{
			private set;
			get;
		}

		static readonly public Bib3.RefNezDiferenz.SictDiferenzSictParam ZuusctandSictFürNuzerSictParam = ZuusctandSictFürNuzerSictParamBerecne();
		static readonly public Bib3.RefNezDiferenz.SictDiferenzSictParam ZuusctandSictDiferenzSictParam = ZuusctandSictDiferenzSictParamBerecne();

		readonly Bib3.RefNezDiferenz.SictRefNezDiferenz ZuusctandSictDiferenz =
			new Bib3.RefNezDiferenz.SictRefNezDiferenz(ZuusctandSictDiferenzSictParam);

		public SictAutomatZuusctand ZuusctandKopiiBerecne()
		{
			var InternZuusctand = this.InternZuusctand;

			if (null == InternZuusctand)
			{
				return null;
			}

			return InternZuusctand.KopiiBerecne();
		}

		public Bib3.RefNezDiferenz.SictZuNezSictDiferenzScritAbbild ZuusctandScritDiferenzBerecne(
			int WiiderhoolungDistanzScrankeMax)
		{
			lock (ZuusctandSictDiferenz)
			{
				var ÄltesteZuVerwendendeScritIndexScrankeMin =
					ZuusctandSictDiferenz.ScritLezteIndex - WiiderhoolungDistanzScrankeMax;

				return ZuusctandSictDiferenz.BerecneScritDif(
					ÄltesteZuVerwendendeScritIndexScrankeMin,
					new object[] { InternZuusctand });
			}
		}

		static SictKonfigMissionZuMissionFilterVerhalte[] MengeZuMissionFilterAktioonVerfüügbarScatescpaicer;

		static public SictKonfigMissionZuMissionFilterVerhalte[] MengeZuMissionFilterAktioonVerfüügbarBerecne()
		{
			var MengeZuMissionFilterAktioonVerfüügbar = new List<SictKonfigMissionZuMissionFilterVerhalte>();

			var MengeMissionStrategikon = SictKonfigMissionStrategikon.MengeMissionStrategikon;

			if (null != MengeMissionStrategikon)
			{
				foreach (var MissionStrategikon in MengeMissionStrategikon)
				{
					if (null == MissionStrategikon)
					{
						continue;
					}

					var MissionStrategikonMissionTitel = MissionStrategikon.MissionTitel;

					if (null == MissionStrategikonMissionTitel)
					{
						continue;
					}

					var FilterMissionTitelRegexPattern = MissionStrategikonMissionTitel.MissionTitelRegexPattern;

					if (null == FilterMissionTitelRegexPattern)
					{
						continue;
					}

					var ZuMissionFilterVerhalte = new SictKonfigMissionZuMissionFilterVerhalte(
						FilterMissionTitelRegexPattern, SictAgentLevelOderAnySictEnum.Any);

					ZuMissionFilterVerhalte.AktioonAcceptAktiiv = true;

					MengeZuMissionFilterAktioonVerfüügbar.Add(ZuMissionFilterVerhalte);
				}
			}

			var GrupeMissionTitelUndAgentLevelZuMissionFilterAktioonVerfüügbar =
				MengeZuMissionFilterAktioonVerfüügbar
				.GroupBy((Kandidaat) => new KeyValuePair<string, SictAgentLevelOderAnySictEnum?>(Kandidaat.FilterMissionTitelRegexPattern, Kandidaat.FilterAgentLevel))
				.ToArray();

			//	Vorersct werd unterscaidung durc Faction nit berüksictigt.
			return
				GrupeMissionTitelUndAgentLevelZuMissionFilterAktioonVerfüügbar
				.Select((Grupe) => Grupe.FirstOrDefault()).ToArray();

			return MengeZuMissionFilterAktioonVerfüügbar.ToArray();
		}

		static public SictKonfigMissionZuMissionFilterVerhalte[] MengeZuMissionFilterAktioonVerfüügbar()
		{
			if (null == MengeZuMissionFilterAktioonVerfüügbarScatescpaicer)
			{
				MengeZuMissionFilterAktioonVerfüügbarScatescpaicer = MengeZuMissionFilterAktioonVerfüügbarBerecne();
			}

			return MengeZuMissionFilterAktioonVerfüügbarScatescpaicer;
		}

		public void VonNuzerListeBerictWindowClientRaster(IEnumerable<SictWertMitZait<SictDataiIdentUndSuuceHinwais>> VonNuzerListeBerictWindowClientRaster)
		{
			if (null == VonNuzerListeBerictWindowClientRaster)
			{
				return;
			}

			InternZuusctand.VonNuzerListeBerictWindowClientRasterFüügeAin(VonNuzerListeBerictWindowClientRaster);
		}

		public void VonWirtAingangParamZuZaitVerhalte(
			SictPräferenzZuZaitVerhalte ZuZaitVerhalte)
		{
			InternZuusctand.VonWirtParamZuZaitVerhalte = ZuZaitVerhalte;
		}

		public bool ScritBerecne()
		{
			var Dauer = new SictMesungZaitraumAusStopwatch(true);

			InternZuusctand.AnwendungSizungIdent = AnwendungSizungIdent;
			InternZuusctand.MengeClientIdentAktuel = this.MengeClientIdentAktuel;
			InternZuusctand.NuzerZaitMili = this.NuzerZaitMili ?? -1;
			InternZuusctand.ServerZaitMili = this.ServerZaitMili ?? -1;
			InternZuusctand.VonSensorScnapscus = VonSensorikMesungLezte.AlsToCustomBotSnapshot();
			InternZuusctand.VonNuzerMeldungZuusctand = VonNuzerMeldungZuusctand;

			{
				var InternZuusctandScpez = InternZuusctand as SictAutomatZuusctandWaiterlaitung;

				if (null != InternZuusctandScpez)
					InternZuusctandScpez.TempDebugListeScritBerecneStopwatchZaitUndNuzerZait = this.TempDebugListeScritBerecneStopwatchZaitUndNuzerZait;
			}

			if (InternZuusctand.VorsclaagWirkungAusgefüürtNictLezteAlterBerecne() < 5555)
				return false;

			{
				SictAusGbsScnapscusAuswertungSrv AuswertErgeebnis = null;

				var MemoryMeasurement = InternZuusctand?.VonSensorScnapscus?.MemoryMeasurement?.Mesung;

				if (null != MemoryMeasurement)
					AuswertErgeebnis = new SictAusGbsScnapscusAuswertungSrv(MemoryMeasurement);

				InternZuusctand.ListeScnapscusLezteAuswertungErgeebnis = AuswertErgeebnis;

				var OptimatParam = InternZuusctand.OptimatParam();

				var VonNuzerParamSimuFraigaabe = OptimatParam?.SimuFraigaabe;

				var VonNuzerParamSimu = OptimatParam?.Simu;

				var VonNuzerParamAutoFraigaabe = OptimatParam?.AutoFraigaabe;

				var ListeScnapscusLezteAuswertungErgeebnisNaacSimu =
					ApliziireSimulatioon(AuswertErgeebnis,
					(true == VonNuzerParamSimuFraigaabe) ? VonNuzerParamSimu : null);

				InternZuusctand.ListeScnapscusAuswertungErgeebnisNaacSimu.Add(ListeScnapscusLezteAuswertungErgeebnisNaacSimu);
				Bib3.Extension.ListeKürzeBegin(InternZuusctand.ListeScnapscusAuswertungErgeebnisNaacSimu, 2);
			}

			InternZuusctand.ScteleSicerAktuel();

			Dauer.EndeSezeJezt();

			return true;
		}

		static public SictAusGbsScnapscusAuswertungSrv ApliziireSimulatioon(
			SictAusGbsScnapscusAuswertungSrv VorSimulatioonZuusctand,
			SictOptimatParamSimu Simulatioon)
		{
			/*
			 * 2015.03.12
			 * 
				if (null == Simulatioon)
				{
					return
						(null == VorSimulatioonZuusctand) ? null : VorSimulatioonZuusctand.Kopii();
				}

				if (null == VorSimulatioonZuusctand)
				{
					return null;
				}

				var Ergeebnis = VorSimulatioonZuusctand.Kopii();

				Ergeebnis.SimulatioonApliziire(Simulatioon);

				return Ergeebnis;
			 * **/

			return VorSimulatioonZuusctand;
		}

	}
}
