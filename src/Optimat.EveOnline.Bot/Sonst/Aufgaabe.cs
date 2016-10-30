using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Optimat.EveOnline.Anwendung.AuswertGbs;
using Bib3;
//using Optimat.EveOnline.AuswertGbs;


namespace Optimat.EveOnline.Anwendung
{
	public struct AufgaabeVerdrängungUndWarte
	{
		[JsonProperty]
		readonly public SictAufgaabeZuusctand VerdrängendeAufgaabe;

		[JsonProperty]
		readonly public int VerdrängendeAufgaabeAlterScritAnzaal;

		[JsonProperty]
		readonly public int FürVerdrängendeAufgaabeZuWarteScritAnzaal;

		[JsonProperty]
		readonly public bool FürVerdrängendeAufgaabeZuWarte;

		public	AufgaabeVerdrängungUndWarte(
			SictAufgaabeZuusctand VerdrängendeAufgaabe,
			int VerdrängendeAufgaabeAlterScritAnzaal,
			int FürVerdrängendeAufgaabeZuWarteScritAnzaal)
		{
			this.VerdrängendeAufgaabe = VerdrängendeAufgaabe;
			this.VerdrängendeAufgaabeAlterScritAnzaal = VerdrängendeAufgaabeAlterScritAnzaal;
			this.FürVerdrängendeAufgaabeZuWarteScritAnzaal = FürVerdrängendeAufgaabeZuWarteScritAnzaal;
			this.FürVerdrängendeAufgaabeZuWarte = VerdrängendeAufgaabeAlterScritAnzaal < FürVerdrängendeAufgaabeZuWarteScritAnzaal;
		}
	}

	/// <summary>
	/// Wirkung Pfaad kan aus meerere aufainander aufbauende (Bsp. EveOnline Menu Kaskaade) Wirkung bescteehe.
	/// 
	/// Für jeede Scrit werd Erfolg überprüüft.
	/// 
	/// 
	/// </summary>
	[JsonObject(MemberSerialization.OptIn)]
	public class SictAufgaabeZuusctand : SictObjektMitBezaicnerInt
	{
		static	readonly	Bib3.SictIdentInt64Fabrik	TempDebugIdentFabrik	= new	Bib3.SictIdentInt64Fabrik();

		[JsonProperty]
		readonly Int64 TempDebugIdent = TempDebugIdentFabrik.IdentBerecne();

		[JsonProperty]
		public SictAufgaabeParam AufgaabeParam
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictWirkungPfaadFabrik HerkunftFabrik
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? BeginZait
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? VorsclaagWirkungBeginZait
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? AbbrucDurcZaitüberscraitung
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? AbsclusTailWirkungZait
		{
			private set;
			get;
		}

		/*
		 * 2015.01.16
		 * 
		[JsonProperty]
		public KeyValuePair<SictAufgaabeZuusctand, int>? AufgaabeScritLezteVerdrängtDurcAufgaabeErsazUndAlterScritAnzaal;

		[JsonProperty]
		public bool AufgaabeScritLezteAbzuwarte;
		 * */

		[JsonProperty]
		public AufgaabeVerdrängungUndWarte? ScritLezteVerdrängungDurcAufgaabeVorher;

		/// <summary>
		/// Über diises Field werd deem Konsumente signalisiirt ob durc dii aus ZiilProcess ermitelte Informatioone Erfolg der Wirkung erkant wurde.
		/// </summary>
		public bool? Erfolg
		{
			get
			{
				return ErfolgZait.HasValue;
			}
		}

		[JsonProperty]
		public Int64? ErfolgZait
		{
			private set;
			get;
		}

		/// <summary>
		/// Fals di durc diise Aufgaabe erziilte Wirkung über ainen Zaitraum andauern kan:
		/// 
		/// Der Zaitpunkt zu welcem erkant wurde das dii durc diise Aufgaabe erziilte Wirkung unterbroce wurde.
		/// Di Unterbrecung kan durc Info aus GBS oder durc andere durcgefüürte Aufgaabe erkant werde.
		/// Bsp.: Manöver Orbit -> Manöver kan durc anderes Manöver unterbroce werde. Diis kan durc untersciidlice Info aus GBS erkenbar sain (z.B. Aintrit in Warp, Manöver zu Objekt mit andere Name)
		/// </summary>
		public Int64? ErfolgUnterbrecungZait
		{
			get
			{
				return Bib3.Glob.Min(ErfolgUnterbrecungDurcGbsZuusctandZait, ErfolgUnterbrecungDurcVorsclaagWirkungZait);
			}
		}

		[JsonProperty]
		public Int64? ErfolgUnterbrecungDurcGbsZuusctandZait
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? ErfolgUnterbrecungDurcVorsclaagWirkungZait
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictBeobacterTransitioonBoolRef BetailigtAnVorsclaagWirkung = new SictBeobacterTransitioonBoolRef(true, 4);

		[JsonProperty]
		public SictWertMitZait<SictAufgaabeScritZerleegungErgeebnis> ZerleegungErgeebnisLezteZuZait
		{
			private set;
			get;
		}

		[JsonProperty]
		public List<SictAufgaabeZuusctand> MengeKomponente
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictVerlaufBeginUndEndeRef<ShipUiIndicationAuswert> ManööverErgeebnis
		{
			private set;
			get;
		}

		public bool? ZerleegungVolsctändig
		{
			get
			{
				var ZerleegungErgeebnisLezteZuZait = this.ZerleegungErgeebnisLezteZuZait;

				if (null == ZerleegungErgeebnisLezteZuZait.Wert)
				{
					return null;
				}

				return ZerleegungErgeebnisLezteZuZait.Wert.ZerleegungVolsctändig;
			}
		}

		public SictAufgaabeZuusctand()
		{
		}

		public SictAufgaabeZuusctand(
			Int64? Ident,
			SictAufgaabeParam AufgaabeParam,
			SictWirkungPfaadFabrik HerkunftFabrik,
			Int64? BeginZait)
			:
			base(Ident)
		{
			this.AufgaabeParam = AufgaabeParam;
			this.HerkunftFabrik = HerkunftFabrik;
			this.BeginZait = BeginZait;
		}

		void VorsclaagWirkungBegin(Int64 BeginZait)
		{
			if (this.VorsclaagWirkungBeginZait.HasValue)
			{
				return;
			}

			this.VorsclaagWirkungBeginZait = BeginZait;
		}

		public void AingangInfoBetailigtAnVorsclaagWirkung(Int64 Zait, bool BetailigtAnVorsclaagWirkung)
		{
			if (BetailigtAnVorsclaagWirkung)
			{
				VorsclaagWirkungBegin(Zait);
			}

			this.BetailigtAnVorsclaagWirkung.AingangWertZuZait(Zait, BetailigtAnVorsclaagWirkung);
		}

		public void AbsclusTailWirkungZaitSeze(Int64 Zait)
		{
			if (this.AbsclusTailWirkungZait.HasValue)
			{
				return;
			}

			this.AbsclusTailWirkungZait = Zait;
		}

		public void ErfolgUnterbrecungDurcVorsclaagWirkungZaitSeze(
			Int64 Zait)
		{
			if (this.ErfolgUnterbrecungDurcVorsclaagWirkungZait.HasValue)
			{
				return;
			}

			this.ErfolgUnterbrecungDurcVorsclaagWirkungZait = Zait;
		}

		public void ManööverErgeebnisSeze(SictVerlaufBeginUndEndeRef<ShipUiIndicationAuswert> ManööverErgeebnis)
		{
			if (null != this.ManööverErgeebnis)
			{
				return;
			}

			this.ManööverErgeebnis = ManööverErgeebnis;
		}

		public void ZerleegungErgeebnisLezteZuZaitSeze(
			SictWertMitZait<SictAufgaabeScritZerleegungErgeebnis> ZerleegungErgeebnisLezteZuZait)
		{
			this.ZerleegungErgeebnisLezteZuZait = ZerleegungErgeebnisLezteZuZait;

			var MengeKomponenteAktiiv = (null == ZerleegungErgeebnisLezteZuZait.Wert) ? null : ZerleegungErgeebnisLezteZuZait.Wert.MengeKomponente;

			if (null != MengeKomponenteAktiiv)
			{
				var MengeKomponente = this.MengeKomponente;

				foreach (var Komponente in MengeKomponenteAktiiv)
				{
					if (null == Komponente)
					{
						continue;
					}

					if (null == MengeKomponente)
					{
						this.MengeKomponente = MengeKomponente = new List<SictAufgaabeZuusctand>();
					}

					if (!MengeKomponente.Contains(Komponente))
					{
						MengeKomponente.Add(Komponente);
					}
				}
			}
		}

		public IEnumerable<SictAufgaabeZuusctand> MengeKomponenteBerecne()
		{
			return this.MengeKomponente;
		}

		public bool EnthaltVorsclaagWirkungKey()
		{
			return
				0	<
				ExtractFromOldAssembly.Bib3.Extension.CountNullable(MengeKomponenteBerecneTransitiivTailmengeAufgaabeParamPasendZuPrädikaat(
				(KandidaatAufgaabeParam) =>
				{
					if (null == KandidaatAufgaabeParam)
					{
						return false;
					}

					var NaacNuzerVorsclaagWirkung = KandidaatAufgaabeParam.NaacNuzerVorsclaagWirkungVirt();

					if (null == NaacNuzerVorsclaagWirkung)
					{
						return false;
					}

					var VorsclaagWirkungMengeWirkungKey = NaacNuzerVorsclaagWirkung.MengeWirkungKey;

					return 0 < ExtractFromOldAssembly.Bib3.Extension.CountNullable(VorsclaagWirkungMengeWirkungKey);
				}));
		}

		public IEnumerable<SictAufgaabeZuusctand> MengeKomponenteBerecneTransitiivTailmengeAufgaabeParamPasendZuPrädikaat(
			Func<SictAufgaabeParam,	bool>	AufgaabeParamPrädikaat,
			Int64?	AbsclusTailWirkungZaitScrankeMin	= null)
		{
			return
				MengeKomponenteBerecneTransitiivTailmengeAufgaabeParamPasendZuPrädikaat<SictAufgaabeParam>(
				AufgaabeParamPrädikaat,
				AbsclusTailWirkungZaitScrankeMin);

			if (null == AufgaabeParamPrädikaat)
			{
				return null;
			}

			var	PrädikaatKomponente	=	new	Func<SictAufgaabeZuusctand,	bool>((KandidaatKomponente) =>
				{
					if(null	== KandidaatKomponente)
					{
						return	false;
					}

					return	AufgaabeParamPrädikaat(KandidaatKomponente.AufgaabeParam);
				});

			return MengeKomponenteBerecneTransitiivTailmengeAufgaabePasendZuPrädikaat(
				PrädikaatKomponente,
				AbsclusTailWirkungZaitScrankeMin);
		}

		public IEnumerable<SictAufgaabeZuusctand> MengeKomponenteBerecneTransitiivTailmengeAufgaabeParamPasendZuPrädikaat<AufgaabeParamType>(
			Func<AufgaabeParamType, bool> AufgaabeParamPrädikaat,
			Int64?	AbsclusTailWirkungZaitScrankeMin	= null)
			where	AufgaabeParamType	:	SictAufgaabeParam
		{
			if (null == AufgaabeParamPrädikaat)
			{
				return null;
			}

			var	PrädikaatKomponente	=	new	Func<SictAufgaabeZuusctand,	bool>((KandidaatKomponente) =>
				{
					if(null	== KandidaatKomponente)
					{
						return	false;
					}

					return AufgaabeParamPrädikaat(KandidaatKomponente.AufgaabeParam as AufgaabeParamType);
				});

			return MengeKomponenteBerecneTransitiivTailmengeAufgaabePasendZuPrädikaat(
				PrädikaatKomponente,
				AbsclusTailWirkungZaitScrankeMin);
		}

		public IEnumerable<SictAufgaabeZuusctand> MengeKomponenteBerecneTransitiivTailmengeAufgaabePasendZuPrädikaat(
			Func<SictAufgaabeZuusctand, bool> AufgaabePrädikaat,
			Int64? AbsclusTailWirkungZaitScrankeMin = null)
		{
			if (null == AufgaabePrädikaat)
			{
				return null;
			}

			var PrädikaatKomponente = new Func<SictAufgaabeZuusctand, bool>((KandidaatKomponente) =>
			{
				if (null == KandidaatKomponente)
				{
					return false;
				}

				if (AbsclusTailWirkungZaitScrankeMin.HasValue)
				{
					if (!(AbsclusTailWirkungZaitScrankeMin <= KandidaatKomponente.AbsclusTailWirkungZait))
					{
						return false;
					}
				}

				return AufgaabePrädikaat(KandidaatKomponente);
			});

			return
				ExtractFromOldAssembly.Bib3.Extension.WhereNullable(
				MengeKomponenteTransitiivBerecne(),
				PrädikaatKomponente);
		}

		public bool MengeKomponenteTransitiivEnthaltAufgaabe(
			SictAufgaabeZuusctand Aufgaabe)
		{
			if (null == Aufgaabe)
			{
				return false;
			}

			var MengeKomponenteTransitiiv = MengeKomponenteTransitiivBerecne();

			if (null == MengeKomponenteTransitiiv)
			{
				return false;
			}

			return MengeKomponenteTransitiiv.Contains(Aufgaabe);
		}

		public IEnumerable<SictAufgaabeZuusctand> MengeKomponenteTransitiivBerecne()
		{
			var MengeKomponenteTransitiiv = new List<SictAufgaabeZuusctand>();

			MengeKomponenteTransitiiv.Add(this);

			var MengeKomponente = MengeKomponenteBerecne();

			if (null != MengeKomponente)
			{
				foreach (var Komponente in MengeKomponente)
				{
					if (null == Komponente)
					{
						continue;
					}

					/*
					 * 2014.06.11
					 * 
					 * Ersaz durc MengeKomponenteTransitiiv.Add(this);
					 * 
					MengeKomponenteTransitiiv.Add(Komponente);
					 * */

					var KomponenteMengeKomponente = Komponente.MengeKomponenteTransitiivBerecne();

					if (null != KomponenteMengeKomponente)
					{
						MengeKomponenteTransitiiv.AddRange(KomponenteMengeKomponente.Where((t) => null != t));
					}

				}
			}

			return	MengeKomponenteTransitiiv;
		}

		public bool IstBlatNaacNuzerVorsclaagWirkung()
		{
			var MengeKomponente = MengeKomponenteBerecne();

			if (!MengeKomponente.IsNullOrEmpty())
				return false;

			var AufgaabeParam = this.AufgaabeParam;

			if (null == AufgaabeParam)
			{
				return false;
			}

			return AufgaabeParam.IstBlatNaacNuzerVorsclaagWirkung();
		}

		public IEnumerable<SictAufgaabeZuusctand> MengeBlatBerecne()
		{
			if (IstBlatNaacNuzerVorsclaagWirkung())
			{
				return new SictAufgaabeZuusctand[] { this };
			}

			var MengeKomponente = MengeKomponenteBerecne();

			var MengeBlat = new List<SictAufgaabeZuusctand>();

			if (null != MengeKomponente)
			{
				foreach (var Komponente in MengeKomponente)
				{
					if (null == Komponente)
					{
						continue;
					}

					MengeBlat.AddRange(Komponente.MengeBlatBerecne());
				}
			}

			return MengeBlat;
		}

		static public bool PrädikaatMenuEntryKlikErfolg(SictAufgaabeZuusctand Aufgaabe)
		{
			if (null == Aufgaabe)
			{
				return false;
			}

			return Aufgaabe.SictAufgaabeMenuPfaadErfolgErgeebnisBerecne(true).HasValue;
		}

		public SictWertMitZait<KeyValuePair<SictAufgaabeParam, VonSensor.MenuEntry[]>>?
			SictAufgaabeMenuPfaadErfolgErgeebnisBerecne(
			bool NurFertige = false)
		{
			var ErfolgZaitNulbar = this.ErfolgZait;

			ErfolgZaitNulbar = this.AbsclusTailWirkungZait;	//	ErfolgZait werd noc nit berecnet. Daher verwendung AbsclusTailWirkungZait.

			if (NurFertige)
			{
				if (!ErfolgZaitNulbar.HasValue)
				{
					return null;
				}

				var ErfolgZait = ErfolgZaitNulbar.Value;
			}

			var AufgaabeParam = this.AufgaabeParam	as	AufgaabeParamAndere;

			if (null == AufgaabeParam)
			{
				return null;
			}

			var MenuListeAstBedingung = AufgaabeParam.MenuListeAstBedingung;

			if (null == MenuListeAstBedingung)
			{
				return null;
			}

			var ListeKomponente = MengeKomponenteBerecne();

			if (null == ListeKomponente)
			{
				return null;
			}

			var ListeMenuEntry = new List<VonSensor.MenuEntry>();

			Int64? KomponenteLezteErfolgZait = null;

			foreach (var Komponente in ListeKomponente)
			{
				if (null == Komponente)
				{
					continue;
				}

				var KomponenteErfolgZait = Komponente.ErfolgZait;	//	ErfolgZait werd noc nit berecnet. Daher verwendung AbsclusTailWirkungZait.

				KomponenteErfolgZait = Komponente.AbsclusTailWirkungZait;

				if (!KomponenteErfolgZait.HasValue)
				{
					continue;
				}

				var KomponenteAufgaabeParam = Komponente.AufgaabeParam	as	AufgaabeParamAndere;

				if (null == KomponenteAufgaabeParam)
				{
					continue;
				}

				var KomponenteAufgaabeParamMenuEntry = KomponenteAufgaabeParam.MenuEntry;

				if (null == KomponenteAufgaabeParamMenuEntry)
				{
					continue;
				}

				KomponenteLezteErfolgZait = Bib3.Glob.Max(KomponenteLezteErfolgZait, KomponenteErfolgZait);

				ListeMenuEntry.Add(KomponenteAufgaabeParamMenuEntry);
			}

			return
				new SictWertMitZait<KeyValuePair<SictAufgaabeParam, VonSensor.MenuEntry[]>>(
					ErfolgZaitNulbar ?? KomponenteLezteErfolgZait ?? 0,
					new KeyValuePair<SictAufgaabeParam, VonSensor.MenuEntry[]>(
					AufgaabeParam, ListeMenuEntry.ToArray()));
		}

		static public SictAufgaabeZuusctand[] SuuceFlacMengeAst(
			SictAufgaabeZuusctand SuuceWurzel,
			Func<SictAufgaabeZuusctand, bool> Prädikaat)
		{
			return Bib3.Glob.SuuceFlacMengeAst(SuuceWurzel, Prädikaat, (Aufgaabe) => null == Aufgaabe ? null : Aufgaabe.MengeKomponenteBerecne());
		}

		static public SictAufgaabeZuusctand[] SuuceFlacMengeAst(
			IEnumerable<SictAufgaabeZuusctand> SuuceMengeWurzel,
			Func<SictAufgaabeZuusctand, bool> Prädikaat)
		{
			return Bib3.Glob.SuuceFlacMengeAst(SuuceMengeWurzel, Prädikaat, (Aufgaabe) => null == Aufgaabe ? null : Aufgaabe.MengeKomponenteBerecne());
		}
	}

}
