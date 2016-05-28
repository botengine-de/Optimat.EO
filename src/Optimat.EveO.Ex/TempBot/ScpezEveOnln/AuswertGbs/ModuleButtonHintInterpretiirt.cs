using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Bib3;
using Newtonsoft.Json;
using Optimat.EveOnline.VonSensor;
using ExtractFromOldAssembly.Bib3;

namespace Optimat.EveOnline.AuswertGbs
{
	[JsonObject(MemberSerialization.OptIn)]
	public class ModuleButtonHintInterpretiirt
	{
		static public ModuleButtonHintInterpretiirt Interpretiire(ModuleButtonHint ModuleButtonHint)
		{
			if (null == ModuleButtonHint)
			{
				return null;
			}

			var Auswert = new ModuleButtonHintInterpretiirt(ModuleButtonHint);
			Auswert.Berecne();

			return Auswert;
		}

		/// <summary>
		/// 2014.00.27	Beobactung Bsp:
		/// "<b>6x Heavy Missile Launcher II</b>"
		/// 
		/// 2014.01.26	Beobactung Bsp:
		/// "<b>5x Dual 150mm Railgun I</b>"
		/// 
		/// 2015.00.03	Bsp:
		/// "<b>75mm Gatling Rail I</b>"
		/// </summary>
		static readonly string[] MengeIndikatorWirkmitelDestruktZaileTitelRegexPattern = new string[]{
			"Missile Launcher",
			"Rocket Launcher",
			"AutoCannon",
			"Railgun",
			"Gatling Rail",
			"Ion Blaster",
			"Neutron Blaster",
			"Electron Blaster",
			"Particle Accelerator",
            "Beam Laser",
			"Pulse Laser",
			"Cannon",
			"Artillery",
			"Howitzer",

		};

		static readonly string[] MengeIndikatorKanLaadeMiningCrystalZaileTitelRegexPattern = new string[]{
			"Strip\\s*Miner",
		};

		static readonly string SurveyScannerZaileTitelRegexPattern = "Survey\\s*Scan";

		[JsonProperty]
		readonly public ModuleButtonHint ModuleButtonHint;

		/// <summary>
		/// Anzaal der Module welce in ModuleButtonHint zusamegefast sin.
		/// </summary>
		[JsonProperty]
		public int? ModuleAnzaal
		{
			private set;
			get;
		}

		[JsonProperty]
		public int? ZyklusDauerMili
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? IstAfterburner
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? IstHardener
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? IstMiner
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? KanLaadeMiningCrystal
		{
			private set;
			get;
		}

		[JsonProperty]
		public OreTypSictEnum? MiningCrystalGelaade
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? IstSurveyScanner
		{
			private set;
			get;
		}

		[JsonProperty]
		public KeyValuePair<int, int>? VolumeMiliPerTimespanMili
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? IstShieldBoosterOderArmorRepairer
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? IstShieldBoosterSelbsct
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? IstArmorRepairerSelbsct
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? IstWirkmitelDestrukt
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? IstTargetPainter
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? ChargeLoaded
		{
			private set;
			get;
		}

		[JsonProperty]
		public KeyValuePair<Int64, int>[] ZyklusMengeZuDamageTypeIconIdentDamage
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictDamageMitBetraagIntValue[] ZyklusMengeZuDamageTypeDamage
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? DamagePerSecondMili
		{
			private set;
			get;
		}

		/// <summary>
		/// ModuleButtonHint scaint in von Nuzer Scnapscus mancmaal unvolsctändig zu sain. Versuuce diise hiir zu markiire.
		/// </summary>
		[JsonProperty]
		public bool? AnnaameModuleInfoVolsctändig
		{
			private set;
			get;
		}

		/// <summary>
		/// z.B. aus Missile "Max Flight Range"
		/// </summary>
		[JsonProperty]
		public Int64? RangeMax
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? RangeOptimal
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? RangeFalloff
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64[] TargetAssignedFilterMengeTexturIdent
		{
			private set;
			get;
		}

		public ModuleButtonHintInterpretiirt()
		{
		}

		public ModuleButtonHintInterpretiirt(ModuleButtonHint ModuleButtonHint)
		{
			this.ModuleButtonHint = ModuleButtonHint;
		}

		public bool? PasendZuModuleReprBerecne(ShipUiModule ModuleRepr)
		{
			var ModuleButtonHint = this.ModuleButtonHint;

			if (null == ModuleButtonHint)
			{
				return null;
			}

			return ModuleButtonHint.PasendZuModuleReprBerecne(ModuleRepr);
		}

		/// <summary>
		/// Beobactunge zaige das in von Nuzer geliiferte GBS Scnapscus mancmaal unvolsctändige (z.B. feelende Zaile) ModuleButtonHint enthalte sind.
		/// Diise Funktioon beantwortet ob zwai ModuleButtonHint sowait glaicwertig sind das fals diise aus direkt aufainanderfolgende Scnapscus komen
		/// davon ausgegange werde kan das diise volsctändig sind.
		/// </summary>
		/// <returns></returns>
		static public bool AusMengeScnapscusHinraicendGlaicwertigFürÜbernaame(
			ModuleButtonHintInterpretiirt O0,
			ModuleButtonHintInterpretiirt O1)
		{
			if (O0 == O1)
			{
				return true;
			}

			if (null == O0 || null == O1)
			{
				return false;
			}

			var O0ModuleButtonHint = O0.ModuleButtonHint;
			var O1ModuleButtonHint = O1.ModuleButtonHint;

			return Optimat.EveOnline.Extension.AusMengeScnapscusHinraicendGlaicwertigFürÜbernaame(O0ModuleButtonHint, O1ModuleButtonHint);
		}

		static readonly public KeyValuePair<SictDamageTypeSictEnum, string>[] ListeDamageTypeSictString =
			new KeyValuePair<SictDamageTypeSictEnum, string>[]{
				new	KeyValuePair<SictDamageTypeSictEnum,	string>(SictDamageTypeSictEnum.EM, "EM"),
				new	KeyValuePair<SictDamageTypeSictEnum,	string>(SictDamageTypeSictEnum.Therm, "Ther"),
				new	KeyValuePair<SictDamageTypeSictEnum,	string>(SictDamageTypeSictEnum.Exp, "Exp"),
				new	KeyValuePair<SictDamageTypeSictEnum,	string>(SictDamageTypeSictEnum.Kin, "Kin")};

		/*
		 * 2013.10.09 Bsp:
		 * Zaile[0]:	"<b>10MN Afterburner II</b> [ALT-F1]"
		 * Zaile[1]:	"Max Velocity without: 175,00 m/s\\nMax Velocity with: 380,72 m/s"
		 * */
		static readonly public string AfterburnerRegexPattern = Regex.Escape("Afterburner");

		static readonly public string ResistanceBonusRegexPattern = Regex.Escape("resistance bonus");

		/// <summary>
		/// 2014.09.04	Bsp:
		/// "1406m3 per 169,2 s"
		/// </summary>
		static readonly public string VolumePerTimespanRegexPattern =
			TempAuswertGbs.Extension.SctandardZaalRegexPatternGroup + "\\s*m3\\s*per\\s*" +
			TempAuswertGbs.Extension.SctandardZaalRegexPatternGroup + "\\s*s";

		static readonly public string MiningCrystalRegexPattern = "(.*)Mining\\s*Crystal";

		/// <summary>
		/// 2014.05.08	Bsp:
		/// "35% Signature Radius Bonus"
		/// 
		/// 2016.01.01	Bsp:
		/// "36% Signature Radius Modifier"
		/// </summary>
		static readonly public string SignatureRadiusBonusRegexPattern = Regex.Escape(@"Signature\s*Radius\s*(Bonus|Modifier)");

		/*
		 * 2013.10.16 Bsp:
		 * "<b>Adaptive Invulnerability Field II</b>"
		 * */
		static readonly public string ShieldHardenerAdaptiveTitelRegexPattern = Regex.Escape("Adaptive Invulnerability Field");

		/// <summary>
		/// 2013.10.16	Bsp:
		/// Zaile[0]:"<b>Large Shield Booster II</b> [ALT-F5]" 
		/// http://forum.botengine.de/thread/mission-running-bot/: "Small C5-L Emergency Shield Overload I"
		/// </summary>
		static readonly public string ShieldBoosterTitelRegexPattern = @"Shield\s*(Booster|Overload)";

		/// <summary>
		/// 2014.01.26	Bsp:
		/// Zaile[0]:"<b>Medium Armor Repairer II</b>"
		/// </summary>
		static readonly public string ArmorRepairerTitelRegexPattern = "Armor Repairer";

		/// <summary>
		/// 2013.10.16	Bsp (Shield):
		/// Zaile[1]:"493 HP bonus per 4 s"
		/// 
		/// 2014.01.26	Bsp (Armor):
		/// Zaile[1]:"368 HP repaired per 9,6 s"
		/// </summary>
		static readonly public string ShieldBoosterArmorRepairerBetraagHpUndDauerRegexPattern =
			TempAuswertGbs.Extension.SctandardZaalRegexPatternGroup + "\\s*" +
			"HP (bonus|repaired) per\\s*" +
			TempAuswertGbs.Extension.SctandardZaalRegexPatternGroup + "\\s*" +
			Regex.Escape("s");

		/*
		 * 2013.09.11 Bsp:
		 * "1356 HP EM damage"
		 * */
		static readonly public string ZyklusDamageRegexPattern = "(\\d+)\\s*HP\\s+(\\w+)\\s+" + Regex.Escape("damage");

		/// <summary>
		/// 2014.02.22	Bsp:
		/// "Damage Per Second 138,3"
		/// </summary>
		static readonly public string DamagePerSecondRegexPattern = "Damage Per Second\\s*" + TempAuswertGbs.Extension.SctandardZaalRegexPatternGroup;

		static SictDamageMitBetraagIntValue? ZyklusDamageAusZaileBescriftung(string ZaileBescriftung)
		{
			if (null == ZaileBescriftung)
			{
				return null;
			}

			var ZyklusDamageRegexMatch = Regex.Match(ZaileBescriftung, ZyklusDamageRegexPattern, RegexOptions.IgnoreCase);

			if (!ZyklusDamageRegexMatch.Success)
			{
				return null;
			}

			var DamageBetraagSictString = ZyklusDamageRegexMatch.Groups[1].Value;
			var DamageTypeSictString = ZyklusDamageRegexMatch.Groups[2].Value;

			if (null == DamageBetraagSictString)
			{
				return null;
			}

			if (null == DamageTypeSictString)
			{
				return null;
			}

			var MengeDamageTypeZuordnung =
				ListeDamageTypeSictString
				.Where((Kandidaat) => Regex.Match(DamageTypeSictString, Kandidaat.Value, RegexOptions.IgnoreCase).Success)
				.ToArray();

			if (MengeDamageTypeZuordnung.Length < 1)
			{
				return null;
			}

			var DamageBetraag = ExtractFromOldAssembly.Bib3.Glob.TryParseInt(DamageBetraagSictString);
			var DamageType = MengeDamageTypeZuordnung[0].Key;

			if (!DamageBetraag.HasValue)
			{
				return null;
			}

			return new SictDamageMitBetraagIntValue(DamageType, DamageBetraag);
		}

		/// <summary>
		/// 2013.09.14 Bsp:
		/// "Max flight range<br>62 km"
		/// 
		/// 2014.09.04	Bsp (Strip Miner):
		/// "Range within 15 km"
		/// </summary>
		static readonly string RangeMaxRegexPattern = "Max flight range[^\\d]*\\s*(" + TempAuswertGbs.Extension.DistanceRegexPattern + ")";

		static readonly string RangeOptimalRegexPattern = "range within[^\\d]*\\s*(" + TempAuswertGbs.Extension.DistanceRegexPattern + ")";

		public void Berecne()
		{
			Int64? DamagePerSecondMili = null;
			SictDamageMitBetraagIntValue[] ZyklusMengeDamage = null;
			Int64? RangeMax = null;
			Int64? RangeOptimal = null;
			bool? IstAfterburner = null;
			bool? IstHardener = null;
			bool? IstMiner = null;
			bool? KanLaadeMiningCrystal = null;
			OreTypSictEnum? MiningCrystalGelaade = null;
			bool? IstSurveyScanner = null;
			bool? IstShieldBoosterOderArmorRepairer = null;
			bool? IstShieldBoosterSelbsct = null;
			bool? IstArmorRepairerSelbsct = null;
			bool? IstWirkmitelDestrukt = null;
			bool? IstTargetPainter = null;
			bool? AnnaameModuleInfoVolsctändig = null;
			bool? ChargeLoaded = null;
			int? ModuleAnzaal = null;
			KeyValuePair<int, int>? VolumeMiliPerTimespanMili = null;
			var TargetAssignedFilterMengeTexturIdent = new List<Int64>();

			try
			{
				var MengeIndikatorWirkmitelDestruktZaileTitelRegexPattern = ModuleButtonHintInterpretiirt.MengeIndikatorWirkmitelDestruktZaileTitelRegexPattern;
				var MengeIndikatorKanLaadeMiningCrystalZaileTitelRegexPattern = ModuleButtonHintInterpretiirt.MengeIndikatorKanLaadeMiningCrystalZaileTitelRegexPattern;

				var ModuleButtonHint = this.ModuleButtonHint;

				if (null == ModuleButtonHint)
				{
					return;
				}

				var ZaileTitel = ModuleButtonHint.ZaileTitel;

				if (null != ZaileTitel)
				{
					var ZaileTitelBescriftung = ZaileTitel.Bescriftung;
					var ZaileTitelMengeIconTextureIdent = ZaileTitel.MengeIconTextureIdent;

					if (null != ZaileTitelMengeIconTextureIdent)
					{
						TargetAssignedFilterMengeTexturIdent.AddRange(ZaileTitelMengeIconTextureIdent);
					}

					var ZaileTitelBescriftungMiinusXml = Optimat.Glob.StringEntferneMengeXmlTag(ZaileTitelBescriftung);

					if (null != ZaileTitelBescriftungMiinusXml)
					{
						var BescriftungMultiMatch = Regex.Match(ZaileTitelBescriftungMiinusXml, "^(\\d)x\\s", RegexOptions.IgnoreCase);

						if (BescriftungMultiMatch.Success)
						{
							ModuleAnzaal = ExtractFromOldAssembly.Bib3.Glob.TryParseInt(BescriftungMultiMatch.Groups[1].Value);
						}
					}

					if (null != ZaileTitelBescriftung &&
						null != MengeIndikatorKanLaadeMiningCrystalZaileTitelRegexPattern)
					{
						foreach (var IndikatorKanLaadeMiningCrystalZaileTitelRegexPattern in MengeIndikatorKanLaadeMiningCrystalZaileTitelRegexPattern)
						{
							if (null == IndikatorKanLaadeMiningCrystalZaileTitelRegexPattern)
							{
								continue;
							}

							if (Regex.Match(ZaileTitelBescriftung, IndikatorKanLaadeMiningCrystalZaileTitelRegexPattern, RegexOptions.IgnoreCase).Success)
							{
								KanLaadeMiningCrystal = true;
								break;
							}
						}
					}

					if (null != ZaileTitelBescriftung &&
						null != MengeIndikatorWirkmitelDestruktZaileTitelRegexPattern)
					{
						foreach (var IndikatorWirkmitelDestruktZaileTitelRegexPattern in MengeIndikatorWirkmitelDestruktZaileTitelRegexPattern)
						{
							if (null == IndikatorWirkmitelDestruktZaileTitelRegexPattern)
							{
								continue;
							}

							var Match = Regex.Match(ZaileTitelBescriftung, IndikatorWirkmitelDestruktZaileTitelRegexPattern, RegexOptions.IgnoreCase);

							if (Match.Success)
							{
								IstWirkmitelDestrukt = true;
								break;
							}
						}

						if (Regex.Match(ZaileTitelBescriftung, SurveyScannerZaileTitelRegexPattern, RegexOptions.IgnoreCase).Success)
						{
							IstSurveyScanner = true;
						}
					}
				}

				var ModuleButtonHintZaileTitel = ModuleButtonHint.ZaileTitel;

				var ModuleButtonHintListeZaile = ModuleButtonHint.ListeZaile;

				var ZyklusMengeDamageList = new List<SictDamageMitBetraagIntValue>();

				if (null != ModuleButtonHintListeZaile)
				{
					AnnaameModuleInfoVolsctändig =
						ModuleButtonHintListeZaile
						.All((Zaile) =>
						{
							if (null == Zaile)
							{
								return false;
							}

							if (null == Zaile.Bescriftung)
							{
								return false;
							}

							/*
							 * 2013.10.16
							 * 
							return 0 < Zaile.Bescriftung.Length;
							 * */

							return true;
						});

					bool? TitelShieldBoosterVorhande = null;
					bool? TitelArmorRepairerVorhande = null;

					if (null != ModuleButtonHintZaileTitel)
					{
						var ModuleButtonHintZaileTitelBescriftung = ModuleButtonHintZaileTitel.Bescriftung;

						if (null != ModuleButtonHintZaileTitelBescriftung)
						{
							{
								var ShieldBoosterTitelMatch = Regex.Match(ModuleButtonHintZaileTitelBescriftung, ShieldBoosterTitelRegexPattern, RegexOptions.IgnoreCase);

								if (ShieldBoosterTitelMatch.Success)
								{
									TitelShieldBoosterVorhande = true;
								}
							}

							{
								var ArmorRepairerTitelMatch = Regex.Match(ModuleButtonHintZaileTitelBescriftung, ArmorRepairerTitelRegexPattern, RegexOptions.IgnoreCase);

								if (ArmorRepairerTitelMatch.Success)
								{
									TitelArmorRepairerVorhande = true;
								}
							}

							{
								var DamageControlTitelMatch = Regex.Match(ModuleButtonHintZaileTitelBescriftung, "Damage\\s*Control", RegexOptions.IgnoreCase);

								if (DamageControlTitelMatch.Success)
								{
									IstHardener = true;
								}
							}
						}
					}

					foreach (var Zaile in ModuleButtonHintListeZaile)
					{
						if (null == Zaile)
						{
							continue;
						}

						var ZaileBescriftung = Zaile.Bescriftung;

						var ZaileBescriftungAbzüüglicXml = Optimat.Glob.StringEntferneMengeXmlTag(ZaileBescriftung);

						if (null == ZaileBescriftung)
						{
							continue;
						}

						{
							var DamagePerSecondMatch = Regex.Match(ZaileBescriftung, DamagePerSecondRegexPattern, RegexOptions.IgnoreCase);

							if (DamagePerSecondMatch.Success)
							{
								DamagePerSecondMili = TempAuswertGbs.Extension.SctandardNumberFormatZaalBetraagMili(DamagePerSecondMatch.Groups[1].Value);
							}
						}

						{
							var ResistanceBonusMatch = Regex.Match(ZaileBescriftung, ResistanceBonusRegexPattern, RegexOptions.IgnoreCase);

							if (ResistanceBonusMatch.Success)
							{
								IstHardener = true;
							}
						}

						{
							var VolumePerCycleMatch = Regex.Match(ZaileBescriftung, VolumePerTimespanRegexPattern, RegexOptions.IgnoreCase);

							if (VolumePerCycleMatch.Success)
							{
								var VolumeSictString = VolumePerCycleMatch.Groups[1].Value;
								var TimespanSictString = VolumePerCycleMatch.Groups[2].Value;

								var VolumeMili = TempAuswertGbs.Extension.SctandardNumberFormatZaalBetraagMili(VolumeSictString);
								var TimespanMili = TempAuswertGbs.Extension.SctandardNumberFormatZaalBetraagMili(TimespanSictString);

								if (VolumeMili.HasValue && TimespanMili.HasValue)
								{
									VolumeMiliPerTimespanMili = new KeyValuePair<int, int>((int)VolumeMili.Value, (int)TimespanMili.Value);

									IstMiner = true;
								}
							}
						}

						{
							var MiningCrystalMatch = Regex.Match(ZaileBescriftung, MiningCrystalRegexPattern, RegexOptions.IgnoreCase);

							if (MiningCrystalMatch.Success)
							{
								var OreTypSictString = MiningCrystalMatch.Groups[1].Value.Trim();

								var OreTyp = TempAuswertGbs.Extension.OreTypBerecneAusOreTypSictString(OreTypSictString);

								if (OreTyp.HasValue)
								{
									MiningCrystalGelaade = OreTyp;
								}
							}
						}

						{
							var SignatureRadiusBonusMatch = Regex.Match(ZaileBescriftung, SignatureRadiusBonusRegexPattern, RegexOptions.IgnoreCase);

							if (SignatureRadiusBonusMatch.Success)
							{
								IstTargetPainter = true;
							}
						}

						{
							var ShieldHardenerAdaptiveTitelMatch = Regex.Match(ZaileBescriftung, ShieldHardenerAdaptiveTitelRegexPattern, RegexOptions.IgnoreCase);

							if (ShieldHardenerAdaptiveTitelMatch.Success)
							{
								IstHardener = true;
							}
						}

						{
							{
								var ShieldBoosterArmorRepairerBetraagHpUndDauerMatch =
									Regex.Match(ZaileBescriftung, ShieldBoosterArmorRepairerBetraagHpUndDauerRegexPattern, RegexOptions.IgnoreCase);

								if (ShieldBoosterArmorRepairerBetraagHpUndDauerMatch.Success)
								{
									IstShieldBoosterOderArmorRepairer = true;
								}

								if (true == IstShieldBoosterOderArmorRepairer)
								{
									if (true == TitelShieldBoosterVorhande)
									{
										IstShieldBoosterSelbsct = true;
									}

									if (true == TitelArmorRepairerVorhande)
									{
										IstArmorRepairerSelbsct = true;
									}
								}
							}
						}

						{
							var AfterburnerMatch = Regex.Match(ZaileBescriftung, AfterburnerRegexPattern, RegexOptions.IgnoreCase);

							if (AfterburnerMatch.Success)
							{
								IstAfterburner = true;
							}
						}

						{
							var RangeMaxMatch = Regex.Match(ZaileBescriftung, RangeMaxRegexPattern, RegexOptions.IgnoreCase);

							if (RangeMaxMatch.Success)
							{
								var RangeSictString = RangeMaxMatch.Groups[1].Value;

								var Range = TempAuswertGbs.Extension.AusDistanceSictStringAbbildGanzzaalDurcMeeterScrankeMin(RangeSictString);

								if (Range.HasValue)
								{
									RangeMax = Range;
								}
							}
						}

						{
							var RangeOptimalMatch = Regex.Match(ZaileBescriftung, RangeOptimalRegexPattern, RegexOptions.IgnoreCase);

							if (RangeOptimalMatch.Success)
							{
								var RangeSictString = RangeOptimalMatch.Groups[1].Value;

								var Range = TempAuswertGbs.Extension.AusDistanceSictStringAbbildGanzzaalDurcMeeterScrankeMin(RangeSictString);

								if (Range.HasValue)
								{
									RangeOptimal = Range;
								}
							}
						}

						var ZyklusDamage = ZyklusDamageAusZaileBescriftung(ZaileBescriftung);

						if (ZyklusDamage.HasValue)
						{
							IstWirkmitelDestrukt = true;
							ChargeLoaded = true;

							ZyklusMengeDamageList.Add(ZyklusDamage.Value);
						}
					}
				}

				if(DamagePerSecondMili.HasValue)
				{
					IstWirkmitelDestrukt = true;
					ChargeLoaded = true;
				}

				ZyklusMengeDamage = ZyklusMengeDamageList.ToArrayFalsNitLeer();
			}
			finally
			{
				this.AnnaameModuleInfoVolsctändig = AnnaameModuleInfoVolsctändig;
				this.ModuleAnzaal = ModuleAnzaal;
				this.DamagePerSecondMili = DamagePerSecondMili;
				this.IstAfterburner = IstAfterburner;
				this.IstHardener = IstHardener;
				this.IstMiner = IstMiner;
				this.KanLaadeMiningCrystal = KanLaadeMiningCrystal;
				this.MiningCrystalGelaade = MiningCrystalGelaade;
				this.IstSurveyScanner = IstSurveyScanner;
				this.VolumeMiliPerTimespanMili = VolumeMiliPerTimespanMili;
				this.IstShieldBoosterOderArmorRepairer = IstShieldBoosterOderArmorRepairer;
				this.IstShieldBoosterSelbsct = IstShieldBoosterSelbsct;
				this.IstArmorRepairerSelbsct = IstArmorRepairerSelbsct;
				this.IstWirkmitelDestrukt = IstWirkmitelDestrukt;
				this.IstTargetPainter = IstTargetPainter;
				this.ZyklusMengeZuDamageTypeDamage = ZyklusMengeDamage;
				this.RangeMax = RangeMax;
				this.RangeOptimal = RangeOptimal;
				this.ChargeLoaded = ChargeLoaded;
				this.TargetAssignedFilterMengeTexturIdent = TargetAssignedFilterMengeTexturIdent.ToArrayFalsNitLeer();
			}
		}
	}

}
