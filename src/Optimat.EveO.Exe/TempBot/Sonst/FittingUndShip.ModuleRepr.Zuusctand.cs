using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Bib3;
using Optimat;
using Optimat.EveOnline.AuswertGbs;
using Optimat.EveOnline.VonSensor;
using ExtractFromOldAssembly.Bib3;

namespace Optimat.EveOnline.Anwendung
{
	[JsonObject(MemberSerialization.OptIn)]
	public class SictMenuModuleButtonAuswert
	{
		/// <summary>
		/// 2014.09.17	Bsp:
		/// "Scordite Mining Crystal II [3]"
		/// </summary>
		const string MiningCrystalRegexPattern = "(.*)Mining\\s*Crystal";

		[JsonProperty]
		readonly public VonSensor.Menu GbsMenu;

		[JsonProperty]
		public KeyValuePair<SictDamageTypeSictEnum, VonSensor.MenuEntry>[] MengeZuAmmoDamageTypeMenuEntry
		{
			private set;
			get;
		}

		[JsonProperty]
		public KeyValuePair<VonSensor.MenuEntry, KeyValuePair<string, OreTypSictEnum?>>[] MengeZuMenuEntryMiningCrystalOreType
		{
			private set;
			get;
		}

		[JsonProperty]
		public VonSensor.MenuEntry ReloadAllMenuEntry
		{
			private set;
			get;
		}

		public SictMenuModuleButtonAuswert()
		{
		}

		public SictMenuModuleButtonAuswert(
			VonSensor.Menu GbsMenu)
		{
			this.GbsMenu = GbsMenu;
		}

		static public SictMenuModuleButtonAuswert Konstrukt(
			VonSensor.Menu GbsMenu,
			KeyValuePair<SictDamageTypeSictEnum, string>[] MengeDamageTypAmmoRegexPattern)
		{
			if (null == GbsMenu)
			{
				return null;
			}

			var Instanz = new SictMenuModuleButtonAuswert(GbsMenu);

			Instanz.Berecne(MengeDamageTypAmmoRegexPattern);

			return Instanz;
		}

		public void Berecne(
			KeyValuePair<SictDamageTypeSictEnum, string>[] MengeDamageTypAmmoRegexPattern)
		{
			KeyValuePair<SictDamageTypeSictEnum, VonSensor.MenuEntry>[] MengeZuAmmoDamageTypeMenuEntry = null;
			var MengeZuMenuEntryMiningCrystalOreType = new List<KeyValuePair<VonSensor.MenuEntry, KeyValuePair<string, OreTypSictEnum?>>>();
			VonSensor.MenuEntry ReloadAllMenuEntry = null;

			try
			{
				var GbsMenu = this.GbsMenu;

				if (null == GbsMenu)
				{
					return;
				}

				var MenuMengeEntry = GbsMenu.ListeEntry;

				if (null == MenuMengeEntry)
				{
					return;
				}

				var MengeZuAmmoDamageTypeMenuEntryList = new List<KeyValuePair<SictDamageTypeSictEnum, VonSensor.MenuEntry>>();

				foreach (var MenuEntry in MenuMengeEntry)
				{
					var MenuEntryBescriftung = MenuEntry.Bescriftung;

					if (null == MenuEntryBescriftung)
					{
						continue;
					}

					{
						var ReloadAllMatch = Regex.Match(MenuEntryBescriftung, "reload.*all", RegexOptions.IgnoreCase);

						if (ReloadAllMatch.Success)
						{
							ReloadAllMenuEntry = MenuEntry;
						}
					}

					{
						var MiningCrystalMatch = Regex.Match(MenuEntryBescriftung, MiningCrystalRegexPattern, RegexOptions.IgnoreCase);

						if (MiningCrystalMatch.Success)
						{
							var OreTypeSictString = MiningCrystalMatch.Groups[1].Value;

							var OreType = TempAuswertGbs.Extension.OreTypBerecneAusOreTypSictString(OreTypeSictString);

							MengeZuMenuEntryMiningCrystalOreType.Add(new KeyValuePair<VonSensor.MenuEntry, KeyValuePair<string, OreTypSictEnum?>>(
								MenuEntry, new KeyValuePair<string, OreTypSictEnum?>(OreTypeSictString, OreType)));
						}
					}

					if (null != MengeDamageTypAmmoRegexPattern)
					{
						foreach (var DamageTypePattern in MengeDamageTypAmmoRegexPattern)
						{
							if (null != DamageTypePattern.Value)
							{
								var Match = Regex.Match(MenuEntryBescriftung, DamageTypePattern.Value, RegexOptions.IgnoreCase);

								if (Match.Success)
								{
									MengeZuAmmoDamageTypeMenuEntryList.Add(new KeyValuePair<SictDamageTypeSictEnum, VonSensor.MenuEntry>(DamageTypePattern.Key, MenuEntry));
								}
							}
						}
					}
				}

				MengeZuAmmoDamageTypeMenuEntry = MengeZuAmmoDamageTypeMenuEntryList.ToArray();
			}
			finally
			{
				this.MengeZuAmmoDamageTypeMenuEntry = MengeZuAmmoDamageTypeMenuEntry;
				this.MengeZuMenuEntryMiningCrystalOreType = MengeZuMenuEntryMiningCrystalOreType.ToArrayFalsNitLeer();
				this.ReloadAllMenuEntry = ReloadAllMenuEntry;
			}
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public struct SictVerknüpfungAusGbsModuleReprMitModuleButtonHint
	{
		[JsonProperty]
		readonly public ShipUiModule	ModuleRepr;

		[JsonProperty]
		readonly public ModuleButtonHintInterpretiirt ModuleButtonHint;

		public SictVerknüpfungAusGbsModuleReprMitModuleButtonHint(
			ShipUiModule	ModuleRepr,
			ModuleButtonHintInterpretiirt ModuleButtonHint)
		{
			this.ModuleRepr = ModuleRepr;
			this.ModuleButtonHint = ModuleButtonHint;
		}
	}

	/// <summary>
	/// Prüüfung Änderung auf
	/// 	public class SictShipUiModuleReprZuusctand : SictZuGbsObjektZuusctandMitIdentPerHerkunftAdrese<SictAusShipUiModuleRepr>
	/// </summary>
	[JsonObject(MemberSerialization.OptIn)]
	public class SictShipUiModuleReprZuusctand :
		SictZuGbsObjektZuusctandMitIdentPerLaageMitZuusazInfo<ShipUiModule, AutomaatZuusctandUndGeneric<int>>
	{
		[JsonProperty]
		readonly public Int64 DebugInspektIdent = Optimat.Glob.AppDomainIdentInt64Berecne();

		[JsonProperty]
		readonly public List<SictWertMitZait<Vektor2DInt>> ListeLaageZuZait = new List<SictWertMitZait<Vektor2DInt>>();

		[JsonProperty]
		readonly public Queue<SictWertMitZait<int?>> ListeRampRotatioonMiliZuZait = new Queue<SictWertMitZait<int?>>();

		[JsonProperty]
		public SictWertMitZait<ModuleButtonHintInterpretiirt> ModuleButtonHintGültigMitZait
		{
			private set;
			get;
		}

		[JsonProperty]
		readonly int AktivitäätTransitioonListeScritAnzaalScrankeMin = 3;

		[JsonProperty]
		readonly AusSequenzTransitioonMiinusZuusazInfo<Int64, bool> InternListeAktivitäätFilterTransitioon =
		new AusSequenzTransitioonMiinusZuusazInfo<Int64, bool>();

		[JsonProperty]
		readonly Queue<ModuleAktivitäätZaitraum> InternListeAktivitäät = new Queue<ModuleAktivitäätZaitraum>();

		public IEnumerable<ModuleAktivitäätZaitraum> ListeAktivitäät
		{
			get
			{
				return InternListeAktivitäät;
			}
		}

		public ModuleAktivitäätZaitraum ListeAktivitäätLezte
		{
			get
			{
				return InternListeAktivitäät.LastOrDefault();
			}
		}

		public OreTypSictEnum? MiningCrystalGelaade
		{
			get
			{
				var ModuleButtonHintGültigMitZait = this.ModuleButtonHintGültigMitZait;

				if (null == ModuleButtonHintGültigMitZait.Wert)
				{
					return null;
				}

				return ModuleButtonHintGültigMitZait.Wert.MiningCrystalGelaade;
			}
		}

		public bool? AnnaameAinscalteVerhindertDurcCloak
		{
			get
			{
				//	!!!!	zu ergänze:	Berecnung aus Auftauce von Abovemain Message naac Versuuc Ainscalt.

				var ModuleButtonHintGültigMitZait = this.ModuleButtonHintGültigMitZait;

				if (null != ModuleButtonHintGültigMitZait.Wert)
				{
					if (true == ModuleButtonHintGültigMitZait.Wert.IstHardener)
					{
						return true;
					}
				}

				return null;
			}
		}

		/// <summary>
		/// ModuleButtonHint welcer vor aktuelem Gültig war.
		/// Äusere Zait isc di lezte Zait zu welcer ModuleButtonIcon zu dem Hint gepast hat.
		/// </summary>
		[JsonProperty]
		public SictWertMitZait<SictWertMitZait<ModuleButtonHintInterpretiirt>> ModuleButtonHintVorherigMitZait
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? TempDebugInspektAingangScnapscusLezteZait
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictGbsMenuKaskaadeZuusctand MenuLezte
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictWertMitZait<SictMenuModuleButtonAuswert> MenuLezteScpezModuleButtonMitZait
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictScpaicerTransitioonNaacHasValue<Int64> SictbarLezteZait;

		[JsonProperty]
		public SictScpaicerTransitioonNaacHasValue<Int64> HiliteLezteZait;

		[JsonProperty]
		readonly SictBeobacterTransitioonBoolRef AktiivLezteZait = new SictBeobacterTransitioonBoolRef(true, 3);

		[JsonProperty]
		public SictWertMitZait<KeyValuePair<ShipUiModule, ModuleButtonHintInterpretiirt>> ScnapscusLezteModuleReprUndButtonHintMitZait
		{
			private set;
			get;
		}

		[JsonProperty]
		SictWertMitZait<KeyValuePair<ShipUiModule, ModuleButtonHintInterpretiirt>> InternScnapscusVorLezteModuleReprUndButtonHintMitZait;

		[JsonProperty]
		readonly IDictionary<Int64, SictWertMitZait<ModuleButtonHintInterpretiirt>> DictZuModuleButtonIconHintLezteMitZait =
			new Dictionary<Int64, SictWertMitZait<ModuleButtonHintInterpretiirt>>();

		[JsonProperty]
		public int? ChargeAnzaal
		{
			private set;
			get;
		}

		[JsonProperty]
		public int? AnnaameChargeAnzaalScrankeMax
		{
			private set;
			get;
		}

		[JsonProperty]
		public int? AnnaameChargeAnzaalScrankeMaxScrankeMin
		{
			private set;
			get;
		}

		/// <summary>
		/// !!!!
		/// Zu korigiire: Reload Dauer Mese
		/// </summary>
		[JsonProperty]
		readonly public int? AnnaameReloadDauer = 10;

		[JsonProperty]
		public int? AnnaameDurcReloadZaiterscpaarnis
		{
			private set;
			get;
		}

		/// <summary>
		/// Ende der Regioon der Ramp in welcer das inaktiiv werde des Module erwartet werd.
		/// Eve Online Client scaint üüber beende aine Module mancmaal verzöögert informiirt zu werde so das Ramp in Eve Online Client noc noie Zyyklus begint und dan mite
		/// in Zyyklus unsictbar werd.
		/// Diis isc warscainlic auc abhängig von Ping.
		/// </summary>
		[JsonProperty]
		public int? ModuleRampEndeRegioonEndeRotatioonMili
		{
			private set;
			get;
		}

		/*
		 * 2014.09.15
		 * 
		/// <summary>
		/// Beobactung lezte Rotatioon Mili der Ramp vor unsictbar werde der Ramp. Werd herangezooge für Berecnung von ModuleRampEndeRegioonEndeRotatioonMili
		/// </summary>
		[JsonProperty]
		List<SictWertMitZait<int>> ModuleRampEndeBeobactungZaitUndRotatioonMili;
		 * */

		new const int ListeScnapscusZuZaitZuErhalteAnzaalScrankeMax = 1;

		public SictShipUiModuleReprZuusctand()
			:
			base(ListeScnapscusZuZaitZuErhalteAnzaalScrankeMax)
		{
		}

		public GbsElement GbsObjektToggleBerecne()
		{
			var ScnapscusLezteModuleReprMitZait = this.ScnapscusLezteModuleReprMitZait;

			if (null == ScnapscusLezteModuleReprMitZait.Wert)
			{
				return null;
			}

			return ScnapscusLezteModuleReprMitZait.Wert.ModuleButtonFlächeToggle;
		}

		public SictWertMitZait<ShipUiModule> ScnapscusLezteModuleReprMitZait
		{
			get
			{
				return new SictWertMitZait<ShipUiModule>(ScnapscusLezteModuleReprUndButtonHintMitZait.Zait, ScnapscusLezteModuleReprUndButtonHintMitZait.Wert.Key);
			}
		}


		public bool? IstWirkmitelDestrukt
		{
			get
			{
				var ModuleButtonHintGültigMitZait = this.ModuleButtonHintGültigMitZait;
				var ModuleButtonHintVorherigMitZait = this.ModuleButtonHintVorherigMitZait;

				if (null != ModuleButtonHintGültigMitZait.Wert)
				{
					return ModuleButtonHintGültigMitZait.Wert.IstWirkmitelDestrukt;
				}

				if (null != ModuleButtonHintVorherigMitZait.Wert.Wert)
				{
					return ModuleButtonHintVorherigMitZait.Wert.Wert.IstWirkmitelDestrukt;
				}

				return null;
			}
		}

		public bool? IstTargetPainter
		{
			get
			{
				var ModuleButtonHintGültigMitZait = this.ModuleButtonHintGültigMitZait;

				if (null == ModuleButtonHintGültigMitZait.Wert)
				{
					return null;
				}

				return ModuleButtonHintGültigMitZait.Wert.IstTargetPainter;
			}
		}

		public bool? IstMiner
		{
			get
			{
				var ModuleButtonHintGültigMitZait = this.ModuleButtonHintGültigMitZait;

				if (null == ModuleButtonHintGültigMitZait.Wert)
				{
					return null;
				}

				return ModuleButtonHintGültigMitZait.Wert.IstMiner;
			}
		}

		public bool? KanLaadeMiningCrystal
		{
			get
			{
				var ModuleButtonHintGültigMitZait = this.ModuleButtonHintGültigMitZait;

				if (null == ModuleButtonHintGültigMitZait.Wert)
				{
					return null;
				}

				return ModuleButtonHintGültigMitZait.Wert.KanLaadeMiningCrystal;
			}
		}

		public bool? IstSurveyScanner
		{
			get
			{
				var ModuleButtonHintGültigMitZait = this.ModuleButtonHintGültigMitZait;

				if (null == ModuleButtonHintGültigMitZait.Wert)
				{
					return null;
				}

				return ModuleButtonHintGültigMitZait.Wert.IstSurveyScanner;
			}
		}

		public Int64? RangeMax
		{
			get
			{
				var ModuleButtonHintGültigMitZait = this.ModuleButtonHintGültigMitZait;

				if (null == ModuleButtonHintGültigMitZait.Wert)
				{
					return null;
				}

				return ModuleButtonHintGültigMitZait.Wert.RangeMax;
			}
		}

		public Int64? RangeOptimal
		{
			get
			{
				var ModuleButtonHintGültigMitZait = this.ModuleButtonHintGültigMitZait;

				if (null == ModuleButtonHintGültigMitZait.Wert)
				{
					return null;
				}

				return ModuleButtonHintGültigMitZait.Wert.RangeOptimal;
			}
		}

		public bool? ChargeLoaded
		{
			get
			{
				var ModuleButtonHintGültigMitZait = this.ModuleButtonHintGültigMitZait;

				if (null != ModuleButtonHintGültigMitZait.Wert)
				{
					return ModuleButtonHintGültigMitZait.Wert.ChargeLoaded;
				}

				return null;
			}
		}

		public int? RotatioonMiliGefiltertRangordnungBerecne(
			int ListeScnapscusAnzaal,
			int RangMili)
		{
			RangMili = Math.Max(0, Math.Min(1000, RangMili));

			var MengeScnapscusBerüksictigt =
				this.ListeRampRotatioonMiliZuZait.Reverse()
				.Take(ListeScnapscusAnzaal)
				.Where((Kandidaat) => Kandidaat.Wert.HasValue)
				.ToArray();

			if (MengeScnapscusBerüksictigt.Length < ListeScnapscusAnzaal)
			{
				return null;
			}

			var MengeScnapscusBerüksictigtOrdnet =
				MengeScnapscusBerüksictigt
				.OrderBy((Kandidaat) => Kandidaat.Wert ?? int.MinValue)
				.ToArray();

			var RangElement =
				MengeScnapscusBerüksictigtOrdnet.ElementAtOrDefault(RangMili * (MengeScnapscusBerüksictigt.Length - 1) / 1000);

			return RangElement.Wert;
		}

		override protected void NaacAingangScnapscus(
			Int64 ScnapscusZait,
			ShipUiModule	ScnapscusWert,
			AutomaatZuusctandUndGeneric<int> ZuusazInfo)
		{
			Aktualisiire(ScnapscusZait, ScnapscusWert, ZuusazInfo);
		}

		void Aktualisiire(
			Int64 Zait,
			ShipUiModule	ScnapscusWert,
			AutomaatZuusctandUndGeneric<int>	ZuusazInfo)
		{
			TempDebugInspektAingangScnapscusLezteZait = Zait;

			var ModuleButtonHintGültigMitZait = default(SictWertMitZait<ModuleButtonHintInterpretiirt>);

			var Automaat = ZuusazInfo.Automaat;

			var ScnapscusModuleButtonHint = Automaat.ScnapscusLezteModuleButtonHint();

			var Sictbar = false;
			var Hilite = false;
			var Aktiiv = false;

			var ModuleReprInGbsFläce = (null == ScnapscusWert) ? OrtogoonInt.Leer : ScnapscusWert.InGbsFläce;
			var ScnapscusRampRotatioonMili = (null == ScnapscusWert) ? null : ScnapscusWert.RampRotatioonMili;

			ListeRampRotatioonMiliZuZait.Enqueue(new SictWertMitZait<int?>(Zait, ScnapscusRampRotatioonMili));
			Bib3.Extension.ListeKürzeBegin(ListeRampRotatioonMiliZuZait, 8);

			var MenuKaskaadeLezte = Automaat.GbsMenuLezteInAstMitHerkunftAdrese(this.ScnapscusLezteGbsAstHerkunftAdrese);

			if (null != MenuKaskaadeLezte)
			{
				var MenuKaskaadeLezteMenuBegin = MenuKaskaadeLezte.ListeMenuScnapscusLezteMitBeginZaitBerecne().FirstOrDefaultNullable();

				if (null != MenuKaskaadeLezteMenuBegin.Wert)
				{
					var EntryIndikatorModule =
						MenuKaskaadeLezteMenuBegin.Wert.ListeEntry.FirstOrDefault((Kandidaat) =>
							Regex.Match(Kandidaat.Bescriftung ?? "", "put\\s*(off|on)line", RegexOptions.IgnoreCase).Success ||
							Regex.Match(Kandidaat.Bescriftung ?? "", "reload", RegexOptions.IgnoreCase).Success ||
							Regex.Match(Kandidaat.Bescriftung ?? "", "unload", RegexOptions.IgnoreCase).Success);

					var EntryIndikatorModuleKontra =
						MenuKaskaadeLezteMenuBegin.Wert.ListeEntry.FirstOrDefault((Kandidaat) =>
							Regex.Match(Kandidaat.Bescriftung ?? "", "camera", RegexOptions.IgnoreCase).Success ||
							Regex.Match(Kandidaat.Bescriftung ?? "", "stargates", RegexOptions.IgnoreCase).Success);

					if (!(MenuKaskaadeLezte == this.MenuLezte) &&
						null != EntryIndikatorModule &&
						null == EntryIndikatorModuleKontra)
					{
						var MengeDamageTypAmmoRegexPattern = SictKonfig.MengeDamageTypAmmoRegexPattern;

						this.MenuLezte = MenuKaskaadeLezte;

						this.MenuLezteScpezModuleButtonMitZait = new SictWertMitZait<SictMenuModuleButtonAuswert>(
							MenuKaskaadeLezte.ScnapscusFrühesteZait ?? -1,
							SictMenuModuleButtonAuswert.Konstrukt(
							MenuKaskaadeLezte.ListeMenuScnapscusLezteMitBeginZaitBerecne().FirstOrDefaultNullable().Wert,
							MengeDamageTypAmmoRegexPattern));
					}
				}
			}

			if (!ModuleReprInGbsFläce.IsLeer)
			{
				var LaageLezte = ListeLaageLezteBerecne();

				if (!(LaageLezte == ModuleReprInGbsFläce.ZentrumLaage))
				{
					ListeLaageZuZait.Add(new SictWertMitZait<Vektor2DInt>(Zait, ModuleReprInGbsFläce.ZentrumLaage));

					Bib3.Extension.ListeKürzeBegin(ListeLaageZuZait, 4);
				}
			}

			try
			{
				{
					//	dii aufruufende Funktioon verantwortet nict di Zugehörigkait des ModuleButtonHint zum Module, diise werd hiir sicergesctelt.
					if (null == ScnapscusWert)
					{
						ScnapscusModuleButtonHint = null;
					}
					else
					{
						if (!(true == ScnapscusWert.SpriteHiliteSictbar))
						{
							ScnapscusModuleButtonHint = null;
						}
					}

					if (null != ScnapscusModuleButtonHint)
					{
						if (!(true == ScnapscusModuleButtonHint.PasendZuModuleReprBerecne(ScnapscusWert)))
						{
							ScnapscusModuleButtonHint = null;
						}
					}
				}

				if (this.ScnapscusLezteModuleReprUndButtonHintMitZait.Zait < Zait)
				{
					InternScnapscusVorLezteModuleReprUndButtonHintMitZait = this.ScnapscusLezteModuleReprUndButtonHintMitZait;
				}
				else
				{
					if (Zait < this.ScnapscusLezteModuleReprUndButtonHintMitZait.Zait)
					{
						InternScnapscusVorLezteModuleReprUndButtonHintMitZait =
							default(SictWertMitZait<KeyValuePair<ShipUiModule, ModuleButtonHintInterpretiirt>>);
					}
				}

				this.ScnapscusLezteModuleReprUndButtonHintMitZait =
					new SictWertMitZait<KeyValuePair<ShipUiModule, ModuleButtonHintInterpretiirt>>(Zait,
						new KeyValuePair<ShipUiModule, ModuleButtonHintInterpretiirt>(ScnapscusWert, ScnapscusModuleButtonHint));

				if (null != ScnapscusLezteModuleReprUndButtonHintMitZait.Wert.Key)
				{
					ChargeAnzaal = ScnapscusLezteModuleReprUndButtonHintMitZait.Wert.Key.ModuleButtonQuantityInt;

					AnnaameChargeAnzaalScrankeMaxScrankeMin = Bib3.Glob.Max(
						AnnaameChargeAnzaalScrankeMaxScrankeMin, ScnapscusLezteModuleReprUndButtonHintMitZait.Wert.Key.ModuleButtonQuantityInt);

					Sictbar = Zait <= ScnapscusLezteModuleReprUndButtonHintMitZait.Zait;

					if (Sictbar)
					{
						Hilite = true == ScnapscusLezteModuleReprUndButtonHintMitZait.Wert.Key.SpriteHiliteSictbar;

						Aktiiv = true == ScnapscusLezteModuleReprUndButtonHintMitZait.Wert.Key.RampAktiiv;

						{
							//	Berecnung ob Module Aktiiv: es kan vorkome das mit Scnapscus zuufälige Werte geliifert werde oder das Ramp nit sictbar wail geraade nahe 0.
							//	Daher Reegel Annaame Aktiiv:
							//	(in lezte Scnapscus Ramp.RotatioonMili unglaic 0)	|| ((in VorLezte Scnapscus Ramp.RotatioonMili unglaic 0) && erwartete (extrapoliirte Ramp.RotatioonMili auserhalb Regioon Ramp ende))

							//	ScnapscusLezteModuleReprUndButtonHintMitZait.Wert.Key.Ramp.RotatioonMili
						}
					}
				}

				if (null != ScnapscusLezteModuleReprUndButtonHintMitZait.Wert.Key &&
					null != InternScnapscusVorLezteModuleReprUndButtonHintMitZait.Wert.Key)
				{
					var ModuleButtonTextureIdentNulbar = ScnapscusLezteModuleReprUndButtonHintMitZait.Wert.Key.ModuleButtonIconTextureIdent;

					if (ModuleButtonTextureIdentNulbar.HasValue)
					{
						var ModuleButtonTextureIdent = ModuleButtonTextureIdentNulbar.Value;

						if (ModuleButtonTextureIdentNulbar == InternScnapscusVorLezteModuleReprUndButtonHintMitZait.Wert.Key.ModuleButtonIconTextureIdent)
						{
							//	Berecnung aus Scnapscus zu überneemende ModuleButtonHint

							if (null != ScnapscusLezteModuleReprUndButtonHintMitZait.Wert.Value &&
								null != InternScnapscusVorLezteModuleReprUndButtonHintMitZait.Wert.Value)
							{
								if (ModuleButtonHintInterpretiirt.AusMengeScnapscusHinraicendGlaicwertigFürÜbernaame(
									ScnapscusLezteModuleReprUndButtonHintMitZait.Wert.Value,
									InternScnapscusVorLezteModuleReprUndButtonHintMitZait.Wert.Value))
								{
									DictZuModuleButtonIconHintLezteMitZait[ModuleButtonTextureIdent] =
										new SictWertMitZait<ModuleButtonHintInterpretiirt>(
											ScnapscusLezteModuleReprUndButtonHintMitZait.Zait,
											ScnapscusLezteModuleReprUndButtonHintMitZait.Wert.Value);

									DictZuModuleButtonIconHintLezteMitZait.EntferneMengeAintraagSolangeAnzaalGrööserScranke(
										4, (IconIdentMitHintMitZait) => IconIdentMitHintMitZait.Value.Zait);
								}
							}
						}

						{
							//	Berecnung aktuel gültige ModuleButtonHint

							var ZuModuleButtonIconHintLezteMitZaitNulbar =
								Optimat.Glob.TADNulbar(DictZuModuleButtonIconHintLezteMitZait, ModuleButtonTextureIdent);

							if (ZuModuleButtonIconHintLezteMitZaitNulbar.HasValue)
							{
								if (null != ZuModuleButtonIconHintLezteMitZaitNulbar.Value.Wert)
								{
									ModuleButtonHintGültigMitZait = ZuModuleButtonIconHintLezteMitZaitNulbar.Value;
								}
							}
						}
					}
				}

				if (null != ScnapscusLezteModuleReprUndButtonHintMitZait.Wert.Key &&
					null != InternScnapscusVorLezteModuleReprUndButtonHintMitZait.Wert.Key)
				{
					{
						//	!!!!	zu ergänze: entprele: nit nur lezte sondern auc vorvorlezte berüksictige
						if ((InternScnapscusVorLezteModuleReprUndButtonHintMitZait.Wert.Key.ModuleButtonQuantityInt ?? 0) <
							ScnapscusLezteModuleReprUndButtonHintMitZait.Wert.Key.ModuleButtonQuantityInt)
						{
							//	Quantity isc von Scnapscus zu Scnapscus gesctiige (-> Annaame Load/Reload hat sctatgefunde)

							AnnaameChargeAnzaalScrankeMax = ScnapscusLezteModuleReprUndButtonHintMitZait.Wert.Key.ModuleButtonQuantityInt;
						}
					}
				}

				//	!!!!	zu ergänze:	AnnaameChargeAnzaalScrankeMax sol zurükgesezt werde wen Munitioonstyp wexelt diise abhängig von Munitionstyp

				{
					/*
					 * 2014.09.13
					 * 
					var ListeAktivitäätLezte = InternListeAktivitäät.LastOrDefaultNullable();

					var AktivitäätTransitioonListeScritAnzaalScrankeMin =
						Math.Max(1,
						Math.Min(this.ListeScnapscusZuZaitZuErhalteAnzaalScrankeMax ?? 0, this.AktivitäätTransitioonListeScritAnzaalScrankeMin));

					var BerecnungAktivitäätListeScnapscus =
						Enumerable.Range(0, AktivitäätTransitioonListeScritAnzaalScrankeMin)
						.Select((ScnapscusAlter) => this.AingangScnapscusMitAlterScritAnzaalBerecne(ScnapscusAlter))
						.WhereNullable((Kandidaat) => Kandidaat.HasValue)
						.SelectNullable((Kandidaat) => Kandidaat.Value)
						.ToArrayNullable();

					if (AktivitäätTransitioonListeScritAnzaalScrankeMin <= BerecnungAktivitäätListeScnapscus.CountNullable())
					{
						var BerecnungAktivitäätListeScnapscusBeginZait =
							BerecnungAktivitäätListeScnapscus.Select((tScnapscus) => tScnapscus.Zait).Min();

						var ListeScnapscusAktiiv =
							BerecnungAktivitäätListeScnapscus.Select((tScnapscus) =>
								{
									if (null == tScnapscus.Wert.Key)
									{
										return false;
									}

									return tScnapscus.Wert.Key.RampAktiiv ?? false;
								}).ToArray();

						if (ListeScnapscusAktiiv.All((ScnapscusAktiiv) => ScnapscusAktiiv))
						{
							var AktivitäätObjektKonstrukt = true;

							if (null != ListeAktivitäätLezte)
							{
								if (!ListeAktivitäätLezte.EndeZait.HasValue)
								{
									AktivitäätObjektKonstrukt = false;
								}
							}

							if (AktivitäätObjektKonstrukt)
							{
								var BeginMengeTarget = new List<SictTargetZuusctand>();

								var MengeTarget = ZuusazInfo.MengeTarget();

								if (null != MengeTarget)
								{
								}

								var Aktivitäät = new ModuleAktivitäätZaitraum(BerecnungAktivitäätListeScnapscusBeginZait, this, BeginMengeTarget.ToArrayFalsNitLeer());

								InternListeAktivitäät.Enqueue(Aktivitäät);
							}
						}

						if (ListeScnapscusAktiiv.All((ScnapscusAktiiv) => !ScnapscusAktiiv))
						{
							if (null != ListeAktivitäätLezte)
							{
								if (!ListeAktivitäätLezte.EndeZait.HasValue)
								{
									ListeAktivitäätLezte.EndeZait = BerecnungAktivitäätListeScnapscusBeginZait;
								}
							}
						}
					}
					 * */

					var AktivitäätTransitioon =
						InternListeAktivitäätFilterTransitioon.AingangScrit(
						Zait, Aktiiv,
						AktivitäätTransitioonListeScritAnzaalScrankeMin,
						3);

					var ListeAktivitäätLezte = InternListeAktivitäät.LastOrDefaultNullable();

					var ListeAktivitäätLezteTransitioon = InternListeAktivitäätFilterTransitioon.ListeTransitioonLezteInfo();

					//	AktivitäätTransitioon = ListeAktivitäätLezteTransitioon;

					if (null != ListeAktivitäätLezte)
					{
						var ListeAktivitäätLezteBeginIndex = Automaat.ZuNuzerZaitBerecneScritIndex(ListeAktivitäätLezte.BeginZait ?? -1);
						var BeginTargetAssignmentScritIndexScrankeMin = ListeAktivitäätLezteBeginIndex - 1;
						var BeginTargetAssignmentScritIndexScrankeMax = ListeAktivitäätLezteBeginIndex + 1;

						if (null == ListeAktivitäätLezte.BeginTargetAssignment &&
							BeginTargetAssignmentScritIndexScrankeMax <= Automaat.ScritLezteIndex)
						{
							Int64[] TargetAssignedFilterMengeTexturIdent = null;

							/*
							 * 2014.09.22	Beobactung:
							 * T:\Günta\Projekt\Optimat.EveOnline\Test\Server.Berict\Berict.Nuzer\[ZAK=2014.09.22.22.04.43,NB=0].Anwendung.Berict:
							 * In Target verwendete Icon mit andere TexturIdent als in ModuleButtonHint enthaltene.
							 * In glaice Sizung wexelt oone Jump der TexturIdent aus ZaileTitel ind ModuleButtonHint.
							 *
							 * Andere Löösung wääre deen ModuleButtonHint für Miner noi zu mese wen kaine pasende Assignment in Target gefunde werde.
							 * 
							if (null != ModuleButtonHintGültigMitZait.Wert)
							{
								TargetAssignedFilterMengeTexturIdent = ModuleButtonHintGültigMitZait.Wert.TargetAssignedFilterMengeTexturIdent;
							}
							 * */

							var BeginTargetAssignment = new AusZaitraumMengeTargetAssignmentBerict(
								BeginTargetAssignmentScritIndexScrankeMin.Value,
								BeginTargetAssignmentScritIndexScrankeMax.Value,
								true,
								TargetAssignedFilterMengeTexturIdent);

							BeginTargetAssignment.Berecne(Automaat);

							ListeAktivitäätLezte.BeginTargetAssignmentSeze(BeginTargetAssignment);
						}
					}

					if (AktivitäätTransitioon.HasValue)
					{
						var ModuleTransitioonBeginZait = AktivitäätTransitioon.Value.ScritIdent;

						if (null != ListeAktivitäätLezte)
						{
							if (!ListeAktivitäätLezte.EndeZait.HasValue)
							{
								ListeAktivitäätLezte.EndeZait = ModuleTransitioonBeginZait;
							}
						}

						if (AktivitäätTransitioon.Value.ZiilWert)
						{
							//	Transitioon naac Aktiiv.

							var Aktivitäät = new ModuleAktivitäätZaitraum(ModuleTransitioonBeginZait, this);

							InternListeAktivitäät.Enqueue(Aktivitäät);
						}
						else
						{
						}
					}

					if (null != ListeAktivitäätLezte)
					{
						if (ScnapscusRampRotatioonMili.HasValue)
						{
							ListeAktivitäätLezte.AingangZuZaitRotatioonMili(Zait, ScnapscusRampRotatioonMili.Value);
						}
					}
				}
			}
			finally
			{
				SictbarLezteZait.AingangWert(Sictbar ? (Int64?)Zait : null);
				HiliteLezteZait.AingangWert(Hilite ? (Int64?)Zait : null);
				AktiivLezteZait.AingangWertZuZait(Zait, Aktiiv);

				{
					var BisherModuleButtonHintGültigMitZait = this.ModuleButtonHintGültigMitZait;

					if (!(ModuleButtonHintGültigMitZait.Wert == BisherModuleButtonHintGültigMitZait.Wert))
					{
						if (null != BisherModuleButtonHintGültigMitZait.Wert)
						{
							ModuleButtonHintVorherigMitZait =
								new SictWertMitZait<SictWertMitZait<ModuleButtonHintInterpretiirt>>(
									Zait, BisherModuleButtonHintGültigMitZait);
						}
					}
				}

				this.ModuleButtonHintGültigMitZait = ModuleButtonHintGültigMitZait;

				InternListeAktivitäät.ListeKürzeBegin(4);
			}
		}

		public SictWertMitZait<Vektor2DInt>? ListeLaageMitZaitLezteBerecne()
		{
			var ListeLaageZuZait = this.ListeLaageZuZait;

			if (null == ListeLaageZuZait)
			{
				return null;
			}

			return ListeLaageZuZait.Select((t) => (SictWertMitZait<Vektor2DInt>?)t).LastOrDefault();
		}

		public SictDamageMitBetraagIntValue[] MengeDamageBerecne()
		{
			var ModuleButtonHintGültig = this.ModuleButtonHintGültigMitZait;

			if (null == ModuleButtonHintGültig.Wert)
			{
				return null;
			}

			if (!(true == ModuleButtonHintGültig.Wert.IstWirkmitelDestrukt))
			{
				return null;
			}

			var ZyklusMengeDamage = ModuleButtonHintGültig.Wert.ZyklusMengeZuDamageTypeDamage;

			return ZyklusMengeDamage;
		}

		public Vektor2DInt? ListeLaageLezteBerecne()
		{
			var LaageLezteMitZait = ListeLaageMitZaitLezteBerecne();

			if (!LaageLezteMitZait.HasValue)
			{
				return null;
			}

			return LaageLezteMitZait.Value.Wert;
		}

		public OrtogoonInt? ToggleFläceBerecne()
		{
			var SictbarLezteZait = this.SictbarLezteZait;

			if (!(true == SictbarLezteZait.AingangLezte.HasValue))
			{
				return null;
			}

			var LaageLezte = ListeLaageLezteBerecne();

			if (!LaageLezte.HasValue)
			{
				return null;
			}

			return OrtogoonInt.AusPunktZentrumUndGrööse(LaageLezte.Value, new Vektor2DInt(20, 20));
		}

		public bool AktiivBerecne(
			int SaitTransitionLezteListeScnapscusAnzaalScrankeMin = 0)
		{
			var AktiivLezteZait = this.AktiivLezteZait;

			if (null == AktiivLezteZait)
			{
				return false;
			}

			var AingangLezteZaitUndWert = AktiivLezteZait.AingangLezteZaitUndWert;

			if (!AingangLezteZaitUndWert.HasValue)
			{
				return false;
			}

			if (AingangLezteZaitUndWert.Value.Wert)
			{
				return true;
			}

			return AktiivLezteZait.SaitTransitionLezteListeAingangAnzaal < SaitTransitionLezteListeScnapscusAnzaalScrankeMin;
		}

		public bool SictbarBerecne()
		{
			return SictbarLezteZait.AingangLezte.HasValue;
		}
	}

}
