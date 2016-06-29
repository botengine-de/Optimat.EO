using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Optimat.EveOnline;
using Optimat.EveOnline.Anwendung;
using Optimat.EveOnline.Anwendung.AuswertGbs;
using Optimat.EveOnline.CustomBot;
using VonSensor = Optimat.EveOnline.VonSensor;
using ExtractFromOldAssembly.Bib3;

namespace Optimat.ScpezEveOnln
{
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

	public partial class SictAutomat
	{
		public byte[] AnwendungSizungIdent
		{
			set;
			get;
		}

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

		public SictVonOptimatMeldungZuusctand NaacNuzerMeldungZuusctand =>
			stateReported?.NaacNuzerMeldungZuusctand;

		readonly ISictAutomatZuusctandMitErwaiterungFürBerict stateReported = new SictAutomatZuusctand();

		public SictAutomat()
		{
		}

		public SictAutomat(ISictAutomatZuusctandMitErwaiterungFürBerict automaatZuusctand)
		{
			this.stateReported = automaatZuusctand;
		}

		static public KeyValuePair<Type, Type>[] ZuusctandSictFürNuzerMengeZuHerkunftTypeZiilTypeBerecne()
		{
			var MengeZuHerkunftTypeZiilTypeBerecne = new Dictionary<Type, Type>();

			var HerkunftAssembly = System.Reflection.Assembly.GetExecutingAssembly();

			var BaseTypeAssembly = System.Reflection.Assembly.GetAssembly(typeof(Optimat.EveOnline.SictMissionZuusctand));

			var HerkunftMengeType = HerkunftAssembly.GetTypes();

			var MengeZuHerkunftTypeZiilType =
				ExtractFromOldAssembly.Bib3.Extension.WhereNullable(
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
			var InternZuusctand = this.stateReported;

			if (null == InternZuusctand)
			{
				return null;
			}

			return InternZuusctand.KopiiBerecne();
		}

		public Bib3.RefNezDiferenz.SictZuNezSictDiferenzScritAbbild ZuusctandScritDiferenzBerecne(
			int wiiderhoolungDistanzScrankeMax)
		{
			lock (ZuusctandSictDiferenz)
			{
				var ÄltesteZuVerwendendeScritIndexScrankeMin =
					ZuusctandSictDiferenz.ScritLezteIndex - wiiderhoolungDistanzScrankeMax;

				return ZuusctandSictDiferenz.BerecneScritDif(
					ÄltesteZuVerwendendeScritIndexScrankeMin,
					new object[] { stateReported });
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

		public bool StepProcess()
		{
			stateReported.AnwendungSizungIdent = AnwendungSizungIdent;
			stateReported.NuzerZaitMili = this.NuzerZaitMili ?? -1;
			stateReported.ServerZaitMili = this.ServerZaitMili ?? -1;
			stateReported.VonSensorScnapscus = VonSensorikMesungLezte.AlsToCustomBotSnapshot();
			stateReported.VonNuzerMeldungZuusctand = VonNuzerMeldungZuusctand;

			if (stateReported.VorsclaagWirkungAusgefüürtNictLezteAlterBerecne() < 5555)
				return false;

			var memoryMeasurement = stateReported?.VonSensorScnapscus?.MemoryMeasurement?.Mesung;

			SictAusGbsScnapscusAuswertungSrv parseResult = null;

			if (null != memoryMeasurement)
				parseResult = new SictAusGbsScnapscusAuswertungSrv(memoryMeasurement);

			stateReported.ListeScnapscusLezteAuswertungErgeebnis = parseResult;

			stateReported.ListeScnapscusAuswertungErgeebnisNaacSimu.Add(parseResult);
			Bib3.Extension.ListeKürzeBegin(stateReported.ListeScnapscusAuswertungErgeebnisNaacSimu, 2);

			stateReported.Update();

			return true;
		}
	}
}
