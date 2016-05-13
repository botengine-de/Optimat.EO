using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Optimat.ScpezEveOnln;
using Bib3;
using Optimat.EveOnline.AuswertGbs;
using Optimat.EveOnline.Anwendung.AuswertGbs;

namespace Optimat.EveOnline.Anwendung
{
	[JsonObject(MemberSerialization.OptIn)]
	public class SictZuDistanceMesungBinär
	{
		[JsonProperty]
		readonly public Int64? Distance;

		[JsonProperty]
		readonly public bool? ZuDistanceAigenscaftBinär;

		public SictZuDistanceMesungBinär()
		{
		}

		public SictZuDistanceMesungBinär(
			Int64? Distance,
			bool? ZuDistanceAigenscaftBinär)
		{
			this.Distance = Distance;
			this.ZuDistanceAigenscaftBinär = ZuDistanceAigenscaftBinär;
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictVersuucFitLoadErgeebnis
	{
		[JsonProperty]
		readonly public string FittingBezaicner;

		[JsonProperty]
		public SictWertMitZait<bool>? EntscaidungErfolg;

		/// <summary>
		/// Ziilprozes hat Feelsclaag gemeldet.
		/// </summary>
		[JsonProperty]
		public bool? MeldungFeelsclaag;

		[JsonProperty]
		public VonSensor.MessageBox ErgeebnisMessageBox;

		public SictVersuucFitLoadErgeebnis()
		{
		}

		public SictVersuucFitLoadErgeebnis(
			string FittingBezaicner,
			SictWertMitZait<bool>? EntscaidungErfolg = null,
			bool? MeldungFeelsclaag = null,
			VonSensor.MessageBox ErgeebnisMessageBox = null)
		{
			this.FittingBezaicner = FittingBezaicner;
			this.EntscaidungErfolg = EntscaidungErfolg;
			this.MeldungFeelsclaag = MeldungFeelsclaag;
			this.ErgeebnisMessageBox = ErgeebnisMessageBox;
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public partial class SictShipZuusctandMitFitting
	{
		[JsonProperty]
		public Int64? TempDebugAktualisiireLezteZait
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? TempDebugAktualisiireLezteZait1
		{
			private set;
			get;
		}

		/// <summary>
		/// Ältere Werte wern hiir gescpaicert un für Vorhersaage vun zuukünftige Zuusctand verwendet.
		/// </summary>
		[JsonProperty]
		public List<SictWertMitZait<ShipState>> ListeSelbstScifZuusctandVergangenhait
		{
			private set;
			get;
		}

		[JsonProperty]
		ShipState ScnapscusVorLezteSelbsctShipZuusctand
		{
			set;
			get;
		}

		[JsonProperty]
		public ShipState ShipZuusctand
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? RepairNüzlicLezteZait
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? RepairErfolgLezteZait
		{
			private set;
			get;
		}

		[JsonProperty]
		public List<SictVerlaufBeginUndEndeRef<ShipUiIndicationAuswert>> ListeAusShipUIIndicationMitZait
		{
			private set;
			get;
		}

		[JsonProperty]
		readonly SictBeobacterTransitioonBoolRef UndockedMesungShipTypIstPodIndikator = new SictBeobacterTransitioonBoolRef(true, 3);

		[JsonProperty]
		public Int64? UndockedMesungShipIsPodLezteZaitMili
		{
			private set;
			get;
		}

		[JsonProperty]
		readonly public int VorhersaageSelbstScifTreferpunkteZaitDistanz = 16;

		[JsonProperty]
		public SictWertMitZait<ShipHitpointsAndEnergy> VorhersaageSelbstScifTreferpunkte
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? DockedLezteZaitMili
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? DockingLezteZaitMili
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? WarpingLezteZaitMili
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? JumpingLezteZaitMili
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool ZaitraumDockedLezteHinraicendAltFürBeurtailungShipZuusctand
		{
			private set;
			get;
		}

		[JsonProperty]
		List<SictWertMitZait<KeyValuePair<Int64, bool>>> ListeMesungZuDistanceIndikatorTargetingVerfügbar
		{
			set;
			get;
		}

		[JsonProperty]
		SictWertMitZait<Int64>? ZwisceergeebnisTargetingRangeScrankeMin
		{
			set;
			get;
		}

		[JsonProperty]
		SictWertMitZait<Int64>? ZwisceergeebnisTargetingRangeScrankeMax
		{
			set;
			get;
		}

		[JsonProperty]
		public KeyValuePair<SictDamageTypeSictEnum, string>[] MengeDamageTypAmmoRegexPattern = SictKonfig.MengeDamageTypAmmoRegexPattern;

		[JsonProperty]
		readonly public List<SictShipUiModuleReprZuusctand> MengeModuleRepr = new List<SictShipUiModuleReprZuusctand>();

		/// <summary>
		/// Lezte erfolgraic gelaadene Fitting von dem nit angenome werd das diises scun wider geändert wurde.
		/// </summary>
		[JsonProperty]
		public SictWertMitZait<string>? FitLoadedLezteNocAktiiv
		{
			private set;
			get;
		}

		/// <summary>
		/// Lezte Mesung der Resistenze mit aingescaltete Module.
		/// </summary>
		[JsonProperty]
		public SictWertMitZait<KeyValuePair<SictDamageTypeSictEnum, int>[]> MesungResistMitModuleAktiivLezte;

		[JsonProperty]
		public List<SictWertMitZait<SictVersuucFitLoadErgeebnis>> ListeVersuucFitLoad;

		[JsonProperty]
		public int? ListeVersuucFitLoadAnzaalScrankeMax = 10;

		[JsonProperty]
		public SictWertMitZait<VonSensor.MessageBox> AusGbsMessageBoxLezte;

		/// <summary>
		/// Da 2013.10.16 Mesung von nur tailwaise vorhandene (feelende Zaile) ModuleButtonHint bai desen Abbau beobactet wurde werd zuusäzlicer Zwiscenscpaicer aingefüürt.
		/// </summary>
		[JsonProperty]
		public SictWertMitZait<SictVerknüpfungAusGbsModuleReprMitModuleButtonHint> VorherScnapscusModuleReprMitButtonHint;

		[JsonProperty]
		public int ModuleButtonHintÜbernaameHiliteAlterScrankeMinMili = 1555;

		[JsonProperty]
		public int ModuleSictbarNictDauerScrankeMax = 16;

		[JsonProperty]
		public SictShipUiModuleReprZuusctand AnnaameModuleAfterburner
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictShipUiModuleReprZuusctand AnnaameModuleShieldBoosterSelbsct
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictShipUiModuleReprZuusctand AnnaameModuleArmorRepairerSelbsct
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? AnnaameModuleDestruktRangeMax
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? AnnaameModuleDestruktRangeOptimum
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? AnnaameTargetingDistanceScrankeMax
		{
			private set;
			get;
		}

		[JsonProperty]
		public List<SictVerlaufBeginUndEndeRef<SictMessageStringAuswert>> ListeMessungDroneCommandRange
		{
			private set;
			get;
		}

		[JsonProperty]
		public List<SictVerlaufBeginUndEndeRef<SictMessageStringAuswert>> ListeMessungDroneControlCount
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? AnnaameDroneCommandRange
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? AnnaameDroneControlCountScrankeMax
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? SelbsctShipWarpScrambled
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? AmmoLoadLezteBeginZait
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictVerlaufBeginUndEndeRef<VonSensor.Message> AbovemainMessageCannotWarpThereLezte
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictVerlaufBeginUndEndeRef<VonSensor.Message> AbovemainMessageDockingLezte
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictVerlaufBeginUndEndeRef<VonSensor.Message> AbovemainMessageCloakingLezte
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictVerlaufBeginUndEndeRef<SictMessageStringAuswert> AbovemainMessageMengeTargetAnzaalSkillScrankeMaxLezte
		{
			private set;
			get;
		}

		[JsonProperty]
		public int? MengeTargetAnzaalSkillScrankeMax
		{
			private set;
			get;
		}

		[JsonProperty]
		public int? MengeTargetAnzaalScrankeMax
		{
			private set;
			get;
		}


		[JsonProperty]
		public SictWertMitZait<VonSensor.InventoryCapacityGaugeInfo> OreHoldCapacityMesungLezteMitZait
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictWertMitZait<VonSensor.InventoryCapacityGaugeInfo> OreHoldCapacityMesungLezteMitZaitNocGültig
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? ModuleMinerAktiivLezteZait
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? AnnaameOreHoldLeer
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? AnnaameOreInMengeModuleMinerZyyklusVolumeMili
		{
			private set;
			get;
		}

		[JsonProperty]
		public int? AnnaameNaacAbbrucMinerZyyklusOreHoldGefültMili
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? ScritNääxteJammed
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? ScritÜüberNääxteJammed
		{
			private set;
			get;
		}

		[JsonProperty]
		Queue<SictWertMitZait<Int64>> ListeZuZaitGescwindigkait = new Queue<SictWertMitZait<Int64>>();

		[JsonProperty]
		public SictWertMitZait<bool>? InventoryMesungShipIsPodLezteZaitUndWert
		{
			private set;
			get;
		}

		public IEnumerable<SictWertMitZait<bool>> MengeMesungShipIsPodZaitUndWert
		{
			get
			{
				var MengeMesung = new List<SictWertMitZait<bool>>();

				var InventoryMesungShipIsPodLezteZaitUndWert = this.InventoryMesungShipIsPodLezteZaitUndWert;
				var UndockedMesungShipTypIstPodLezteZaitMili = this.UndockedMesungShipIsPodLezteZaitMili;

				if (InventoryMesungShipIsPodLezteZaitUndWert.HasValue)
				{
					MengeMesung.Add(InventoryMesungShipIsPodLezteZaitUndWert.Value);
				}

				if (UndockedMesungShipTypIstPodLezteZaitMili.HasValue)
				{
					MengeMesung.Add(new SictWertMitZait<bool>(UndockedMesungShipTypIstPodLezteZaitMili.Value, true));
				}

				return MengeMesung;
			}
		}

		public SictWertMitZait<bool>? MesungShipIsPodLezteZaitUndWert
		{
			get
			{
				var MengeMesungShipIsPodZaitUndWert = this.MengeMesungShipIsPodZaitUndWert;

				return
					MengeMesungShipIsPodZaitUndWert
					.OrderBy((Kandidaat) => Kandidaat.Zait)
					.Cast<SictWertMitZait<bool>?>()
					.LastOrDefault();
			}
		}

		public bool? Cloaked
		{
			get
			{
				var ShipZuusctand = this.ShipZuusctand;

				if (null == ShipZuusctand)
				{
					return null;
				}

				return ShipZuusctand.Cloaked;
			}
		}

		public SictVerlaufBeginUndEndeRef<ShipUiIndicationAuswert> AusShipUIIndicationLezteMitZait
		{
			get
			{
				return Bib3.Extension.LastOrDefaultNullable(ListeAusShipUIIndicationMitZait);
			}
		}

		public SictVerlaufBeginUndEndeRef<ShipUiIndicationAuswert> AusShipUIIndicationAktiivMitZait
		{
			get
			{
				var AusShipUIIndicationLezteMitZait = this.AusShipUIIndicationLezteMitZait;

				if (null == AusShipUIIndicationLezteMitZait)
				{
					return null;
				}

				if (AusShipUIIndicationLezteMitZait.EndeZait.HasValue)
				{
					return null;
				}

				return AusShipUIIndicationLezteMitZait;
			}
		}

		/// <summary>
		/// 2014.00.26	Beobactung AbovemainMessage:
		/// "You cannot warp there because natural phenomena are disrupting the warp."
		/// </summary>
		readonly static public string[] AbovemainMessageCannotWarpThereMengeRegexPattern = new string[]{
			"cannot warp there",
		};

		/// <summary>
		/// 2014.00.10	Beobactung AbovemainMessage:
		/// "Setting course to docking perimeter"
		/// "Requested to dock at **** station"
		/// "You cannot do that while docking."
		/// </summary>
		readonly static public string[] AbovemainMessageDockingMengeRegexPattern = new string[]{
			"Setting course to docking perimeter",
			"Requested to dock at .+ station",
			"You cannot do that while docking",
		};

		/// <summary>
		/// 2014.00.10	Beobactung AbovemainMessage:
		/// "Interference from the cloaking you are doing is preventing your systems from functioning at this time."
		/// </summary>
		readonly static public string[] AbovemainMessageCloakingMengeRegexPattern = new string[]{
			"Interference.*cloak.*preventing",
		};

		public IEnumerable<SictShipUiModuleReprZuusctand> MengeModuleReprFiltert(Func<SictShipUiModuleReprZuusctand, bool> Prädikaat)
		{
			var MengeModuleReprFiltert =
				this.MengeModuleRepr.Where((Kandidaat) => (null == Prädikaat) ? false : Prädikaat(Kandidaat));

			return MengeModuleReprFiltert;
		}

		public IEnumerable<SictShipUiModuleReprZuusctand> MengeModuleReprFiltert(Func<ModuleButtonHintInterpretiirt, bool> Prädikaat)
		{
			var PrädikaatModuleRepr =
				new Func<SictShipUiModuleReprZuusctand, bool>((Kandidaat) =>
					{
						if (null == Prädikaat)
						{
							return false;
						}

						var ModuleButtonHintNocGültig = Kandidaat.ModuleButtonHintGültigMitZait;

						if (null == ModuleButtonHintNocGültig.Wert)
						{
							return false;
						}

						return Prädikaat(ModuleButtonHintNocGültig.Wert);
					});

			return MengeModuleReprFiltert(PrädikaatModuleRepr);
		}

		public IEnumerable<SictShipUiModuleReprZuusctand> MengeModuleReprHardenerBerecne()
		{
			return MengeModuleReprFiltert(new Func<ModuleButtonHintInterpretiirt, bool>((Kandidaat) => true == Kandidaat.IstHardener));
		}

		public IEnumerable<SictShipUiModuleReprZuusctand> MengeModuleReprWirkmitelDestruktBerecne()
		{
			return MengeModuleReprFiltert(new Func<ModuleButtonHintInterpretiirt, bool>((Kandidaat) => true == Kandidaat.IstWirkmitelDestrukt));
		}

		/// <summary>
		/// 2014.00.06 Bsp:
		/// "Loading the Heavy Missile into the Missile Launcher Heavy; this will take approximately 10,00 seconds."
		/// "Heavy Missile Launcher II is currently being reloaded. Please wait until it is finished and try again."
		/// 
		/// !!!!	Zu erleedige: Message für Turret ermitle.
		/// </summary>
		static readonly public string[] GbsAbovemainMessageAmmoLoadMengeRegexPattern = new string[]{
			"Loading the ([\\w\\s]+) into the ([\\w\\s]+); this will take approximately ([\\d\\,\\.]+) seconds",
		};

		static public SictVerlaufBeginUndEndeRef<ShipUiIndicationAuswert> IndicationCaptionRegexMatchLezte(
			IEnumerable<SictVerlaufBeginUndEndeRef<ShipUiIndicationAuswert>> ListeIndicationOrdnet,
			string RegexPattern,
			RegexOptions RegexOptions = RegexOptions.None)
		{
			return IndicationCaptionRegexMatchLezte(
				ListeIndicationOrdnet,
				new string[] { RegexPattern },
				RegexOptions);
		}

		static readonly public int NaacJumpingAnnaameCloakDauer = 30;

		static public SictVerlaufBeginUndEndeRef<ShipUiIndicationAuswert> IndicationCaptionRegexMatchLezte(
			IEnumerable<SictVerlaufBeginUndEndeRef<ShipUiIndicationAuswert>> ListeIndicationOrdnet,
			string[] MengeRegexPattern,
			RegexOptions RegexOptions = RegexOptions.None)
		{
			var Prädikaat = new Func<ShipUiIndicationAuswert, bool>((IndicationAuswert) =>
				{
					if (null == IndicationAuswert)
					{
						return false;
					}

					var Indication = IndicationAuswert.ShipUIIndication;

					if (null == Indication)
					{
						return false;
					}

					var IndicationCaption = Indication.IndicationCaption;

					if (null == IndicationCaption)
					{
						return false;
					}

					foreach (var RegexPattern in MengeRegexPattern)
					{
						if (null == RegexPattern)
						{
							continue;
						}

						var Match = Regex.Match(IndicationCaption, RegexPattern, RegexOptions);

						if (Match.Success)
						{
							return true;
						}
					}

					return false;
				});

			return IndicationCaptionPasendZuPrädikaatLezte(ListeIndicationOrdnet, Prädikaat);
		}

		static public SictVerlaufBeginUndEndeRef<ShipUiIndicationAuswert> IndicationCaptionMitManöverTypLezte(
			IEnumerable<SictVerlaufBeginUndEndeRef<ShipUiIndicationAuswert>> ListeIndicationOrdnet,
			SictZuInRaumObjektManööverTypEnum ManöverTyp)
		{
			var Prädikaat = new Func<ShipUiIndicationAuswert, bool>((IndicationAuswert) =>
			{
				if (null == IndicationAuswert)
				{
					return false;
				}

				return IndicationAuswert.ManöverTyp == ManöverTyp;
			});

			return IndicationCaptionPasendZuPrädikaatLezte(ListeIndicationOrdnet, Prädikaat);
		}

		public Int64 SctrekeZurükgeleegtMiliInZaitraum(
			Int64 ZaitraumBegin,
			Int64 ZaitraumEnde)
		{
			return BetraagSumeInIntervalBerecne(
				ZaitraumBegin,
				ZaitraumEnde,
				this.ListeZuZaitGescwindigkait);
		}

		static public Int64 BetraagDurcscnitMiliInIntervalBerecne(
			Int64 IntervalBeginInklusiiv,
			Int64 IntervalEndeExklussiv,
			IEnumerable<SictWertMitZait<Int64>> ListeMespunkt)
		{
			var BetraagSumeInInterval =
				BetraagSumeInIntervalBerecne(IntervalBeginInklusiiv, IntervalEndeExklussiv, ListeMespunkt);

			return
				(BetraagSumeInInterval * 1000) /
				(IntervalEndeExklussiv - IntervalBeginInklusiiv);
		}

		static public Int64 BetraagSumeInIntervalBerecne(
			Int64 IntervalBeginInklusiiv,
			Int64 IntervalEndeExklussiv,
			IEnumerable<SictWertMitZait<Int64>> ListeMespunkt)
		{
			if (null == ListeMespunkt)
			{
				return 0;
			}

			if (IntervalEndeExklussiv == IntervalBeginInklusiiv)
			{
				return 0;
			}

			if (IntervalEndeExklussiv < IntervalBeginInklusiiv)
			{
				return -BetraagSumeInIntervalBerecne(IntervalEndeExklussiv, IntervalBeginInklusiiv, ListeMespunkt);
			}

			var Enumerator = ListeMespunkt.GetEnumerator();

			SictWertMitZait<Int64>? MespunktFrüheste = null;
			SictWertMitZait<Int64>? MespunktLezte = null;

			Int64 Sume = 0;

			while (Enumerator.MoveNext())
			{
				var Mespunkt = Enumerator.Current;

				if (!MespunktFrüheste.HasValue)
				{
					MespunktFrüheste = Mespunkt;

					var VorMespunktFrühesteIntervalLänge =
						Math.Min(IntervalEndeExklussiv, Mespunkt.Zait) - IntervalBeginInklusiiv;

					if (0 < VorMespunktFrühesteIntervalLänge)
					{
						Sume = VorMespunktFrühesteIntervalLänge * Mespunkt.Wert;
					}
				}

				if (MespunktLezte.HasValue)
				{
					var ZwisceMespunktScnitIntervalBegin =
						Math.Max(IntervalBeginInklusiiv, MespunktLezte.Value.Zait);

					var ZwisceMespunktScnitIntervalEnde =
						Math.Min(IntervalEndeExklussiv, Mespunkt.Zait);

					var ScnitLänge =
						ZwisceMespunktScnitIntervalEnde - ZwisceMespunktScnitIntervalBegin;

					if (0 < ScnitLänge)
					{
						var ZwisceMespunktBetraagDiferenz = Mespunkt.Wert - MespunktLezte.Value.Wert;

						var ZwisceMespunktScnitIntervalBeginBetraag =
							MespunktLezte.Value.Wert + ((ZwisceMespunktBetraagDiferenz * (ZwisceMespunktScnitIntervalBegin - MespunktLezte.Value.Zait)) / ScnitLänge);

						var ZwisceMespunktScnitIntervalEndeBetraag =
							MespunktLezte.Value.Wert + ((ZwisceMespunktBetraagDiferenz * (ZwisceMespunktScnitIntervalEnde - MespunktLezte.Value.Zait)) / ScnitLänge);

						var InScnitBetraag =
							(ZwisceMespunktScnitIntervalBeginBetraag + ZwisceMespunktScnitIntervalEndeBetraag) * ScnitLänge / 2;

						Sume += InScnitBetraag;
					}
				}

				if (IntervalEndeExklussiv <= Mespunkt.Zait)
				{
					break;
				}

				MespunktLezte = Mespunkt;
			}

			if (MespunktLezte.HasValue)
			{
				var NaacMespunktLezteIntervalLänge =
					IntervalEndeExklussiv - Math.Max(IntervalBeginInklusiiv, MespunktLezte.Value.Zait);

				if (0 < NaacMespunktLezteIntervalLänge)
				{
					Sume += NaacMespunktLezteIntervalLänge * MespunktLezte.Value.Wert;
				}
			}

			return Sume;
		}

		static public SictVerlaufBeginUndEndeRef<ShipUiIndicationAuswert> IndicationCaptionPasendZuPrädikaatLezte(
			IEnumerable<SictVerlaufBeginUndEndeRef<ShipUiIndicationAuswert>> ListeIndicationOrdnet,
			Func<ShipUiIndicationAuswert, bool> Prädikaat)
		{
			if (null == Prädikaat || null == ListeIndicationOrdnet)
			{
				return null;
			}

			foreach (var IndicationMitZait in ListeIndicationOrdnet.Reversed())
			{
				var IndicationAuswert = IndicationMitZait.Wert;

				if (null == IndicationAuswert)
				{
					continue;
				}

				if (Prädikaat(IndicationAuswert))
				{
					return IndicationMitZait;
				}
			}

			return null;
		}

		/// <summary>
		/// Durcsuuct <paramref name="ListeMessageOrdnet"/> rükwärts.
		/// </summary>
		/// <param name="ListeMessageOrdnet"></param>
		/// <param name="MengeRegexPattern"></param>
		/// <param name="RegexOptions"></param>
		/// <returns></returns>
		static public SictVerlaufBeginUndEndeRef<VonSensor.Message> MessageRegexMatchLezte(
			IEnumerable<SictVerlaufBeginUndEndeRef<VonSensor.Message>> ListeMessageOrdnet,
			string[] MengeRegexPattern,
			RegexOptions RegexOptions = RegexOptions.None)
		{
			if (null == MengeRegexPattern || null == ListeMessageOrdnet)
			{
				return null;
			}

			foreach (var MessageMitZait in ListeMessageOrdnet.Reversed())
			{
				var Message = MessageMitZait.Wert;

				if (null == Message)
				{
					continue;
				}

				var MessageLabelText = Message.LabelText;

				if (null == MessageLabelText)
				{
					continue;
				}

				foreach (var RegexPattern in MengeRegexPattern)
				{
					if (null == RegexPattern)
					{
						continue;
					}

					var Match = Regex.Match(MessageLabelText, RegexPattern, RegexOptions);

					if (Match.Success)
					{
						return MessageMitZait;
					}
				}
			}

			return null;
		}

		static public string MengeModuleInfoAgrStringBerecne(
			IEnumerable<SictShipUiModuleReprZuusctand> MengeModuleRepr)
		{
			if (null == MengeModuleRepr)
			{
				return null;
			}

			var MengeModuleAbbildString = new List<string>();

			foreach (var ModuleRepr in MengeModuleRepr)
			{
				if (null == ModuleRepr)
				{
					continue;
				}

				var ModuleAbbildString = "Unknown Module";

				try
				{
					var ModuleButtonHintGültigMitZait = ModuleRepr.ModuleButtonHintGültigMitZait;

					if (null == ModuleButtonHintGültigMitZait.Wert)
					{
						continue;
					}

					var ModuleButtonHint = ModuleButtonHintGültigMitZait.Wert.ModuleButtonHint;

					if (null == ModuleButtonHint)
					{
						continue;
					}

					var ZaileTitel = ModuleButtonHint.ZaileTitel;

					if (null == ZaileTitel)
					{
						continue;
					}

					ModuleAbbildString = ZaileTitel.BescriftungMiinusFormat.TrimNullable();
				}
				finally
				{
					MengeModuleAbbildString.Add(ModuleAbbildString);
				}
			}

			return
				string.Join(Environment.NewLine, MengeModuleAbbildString);
		}
	}
}
