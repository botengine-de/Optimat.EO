using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Optimat.EveOnline.Anwendung;
//using Optimat.EveOnline.AuswertGbs;
using Optimat.EveOnline.VonSensor;
using Optimat.ScpezEveOnln;
using Sanderling.Parse;

namespace Optimat.EveOnline.Anwendung
{
	/// <summary>
	/// Metoode für Entscaidunge im Gefect und Felder di (Zwisce)ergeebnise diiser Entscaidunge scpaicern.
	/// </summary>
	[JsonObject(MemberSerialization.OptIn)]
	public partial class SictInRaumAktioonUndGefect
	{
		[JsonProperty]
		public SictVerzwaigungNaacShipZuusctandScranke InRaumVerhalteGefectFortsazScranke
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictVerzwaigungNaacShipZuusctandScranke InRaumVerhalteBeweegungUnabhängigVonGefectScranke
		{
			private set;
			get;
		}

		/// <summary>
		/// Von Automaat auszufüürende Aufgaabe.
		/// enthält nict Afterburner, ShieldBooster, Drones. Diise werden von Automaat abhängig vom Aufgaabetyp (Distanz/UnLock/Lock/....) vor oder naac
		/// der von diiser Property angegeebene Aufgabe aingeordnet.
		/// </summary>
		[JsonProperty]
		public SictAufgaabeParam AufgaabeAuszufüüreNääxte
		{
			private set;
			get;
		}

		/// <summary>
		/// Diise Aufgaabe wääre vom Automaat auszufüüre fals nict vorrangig di Sictbarkait von Objekte in Overview gereegelt
		/// werde sol (durc Auswaal Tab oder Scrole)
		/// </summary>
		[JsonProperty]
		public SictAufgaabeParam AufgaabeAuszufüüreNääxteTailVorReegelungOverviewObjSictbar
		{
			private set;
			get;
		}

		[JsonProperty]
		public List<SictAufgaabeGrupePrio> ListePrioGrupeMengeAufgaabeParam
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictOverviewObjektGrupeEnum[] MengeOverviewObjektGrupeMesungZuErsctele
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictOverviewObjektGrupeEnum[] MengeOverviewObjektGrupeVerwendet
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictOverviewObjektGrupeEnum[] MengeOverviewObjektGrupeVerwendetNaacBerüksictigungPrio
		{
			private set;
			get;
		}

		[JsonProperty]
		public OverviewPresetDefaultTyp[] OverviewListePresetDefaultPrio
		{
			private set;
			get;
		}

		[JsonProperty]
		public KeyValuePair<string, OverviewPresetDefaultTyp>[] OverviewMengeZuTabNamePresetDefault
		{
			private set;
			get;
		}

		[JsonProperty]
		public string OverviewTabZuAktiviireFürMaidungScroll
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? FürAinsctelungDistanceZurükzuleegendeSctrekeBetraag
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictNaacNuzerMeldungZuEveOnlineCause	AnforderungFluctUrsace
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictNaacNuzerMeldungZuEveOnlineCause	AktioonUndockFraigaabeNictUrsace
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictNaacNuzerMeldungZuEveOnlineCause	AnforderungDockUrsace
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? ShipFittingPasendNictZuMissionAktuelLezteZait
		{
			private set;
			get;
		}

		/// <summary>
		/// Sobald Warp mööglic sol di Raise zum Ziil fortgesezt werde.
		/// </summary>
		[JsonProperty]
		public bool?	ZiilLocationNääxteErraict
		{
			private set;
			get;
		}

		/// <summary>
		/// Wen diise Aigenscaft=false kan davon ausgegange werde das als nääxtes das verlasen des Grid (z.B. durc Warp) anscteet.
		/// Somit kan z.B. auf das UnLocke von nict meer benöötigte Target verzictet were.
		/// </summary>
		[JsonProperty]
		public bool? InRaumAktioonFortsaz
		{
			private set;
			get;
		}

		/// <summary>
		/// Route aus dem InfoPanelRoute sol abgearbaitet werde.
		/// </summary>
		[JsonProperty]
		public	bool?	InfoPanelRouteFraigaabe
		{
			private set;
			get;
		}

		[JsonProperty]
		public EWarTypeEnum[][] ListePrioMengeEWarTypeZuZersctööre
		{
			private set;
			get;
		}

		[JsonProperty]
		public VonSensor.InventoryItem AusInventoryItemZuÜbertraageNaacActiveShip
		{
			private set;
			get;
		}

		/// <summary>
		/// Menge der Target aus GBS welce geUnLocked were solen.
		/// </summary>
		[JsonProperty]
		public KeyValuePair<VonSensor.ShipUiTarget, SictAktioonPrioEnum>[] MengeTargetZuUnLockeMitPrio
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? MengeObjektZuDestruiireNääxteDistance
		{
			private set;
			get;
		}

		[JsonProperty]
		readonly public SictBeobacterTransitioonBoolRef AnforderungDroneLaunch = new SictBeobacterTransitioonBoolRef(true,	3);

		[JsonProperty]
		readonly public SictBeobacterTransitioonBoolRef AnforderungDroneReturn = new SictBeobacterTransitioonBoolRef(true, 3);

		[JsonProperty]
		public int NaacEntscaidungDroneReturnWartezaitBisFraigaabeRaumVerlase = 1000 * 40;

		public bool? AnforderungDroneLaunchLezte
		{
			get
			{
				return	SictBeobacterTransitioonRef<bool>.AingangLezteWertNulbar(AnforderungDroneLaunch);
			}
		}

		public bool? AnforderungDroneReturnLezte
		{
			get
			{
				return SictBeobacterTransitioonRef<bool>.AingangLezteWertNulbar(AnforderungDroneReturn);
			}
		}

		public Int64? AnforderungDroneReturnBeginZaitMili
		{
			get
			{
				var AnforderungDroneReturn = this.AnforderungDroneReturn;

				if (null == AnforderungDroneReturn)
				{
					return null;
				}

				return AnforderungDroneReturn.WertTrueBeginZaitBerecne();
			}
		}

		[JsonProperty]
		public	bool?	DronesVersuucAnforderungReturnFälig
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? WarteAufDronePrioVorRaumVerlase
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? RaumVerlaseFraigaabe
		{
			private set;
			get;
		}

		[JsonProperty]
		readonly public SictBeobacterTransitioonBoolRef AnforderungShieldBooster = new SictBeobacterTransitioonBoolRef(true, 3);

		public bool? AnforderungShieldBoosterLezte
		{
			get
			{
				return SictBeobacterTransitioonRef<bool>.AingangLezteWertNulbar(AnforderungShieldBooster);
			}
		}

		[JsonProperty]
		readonly public SictBeobacterTransitioonBoolRef AnforderungArmorRepairer	= new SictBeobacterTransitioonBoolRef(true, 3);

		public bool? AnforderungArmorRepairerLezte
		{
			get
			{
				return SictBeobacterTransitioonRef<bool>.AingangLezteWertNulbar(AnforderungArmorRepairer);
			}
		}

		[JsonProperty]
		SictScpaicerTransitioonNaacHasValue<Int64> InternAnforderungAfterburnerLezteZaitMili;

		public SictScpaicerTransitioonNaacHasValue<Int64> AnforderungAfterburnerLezteZaitMili
		{
			get
			{
				return InternAnforderungAfterburnerLezteZaitMili;
			}
		}

		[JsonProperty]
		public bool? GefectBaitritFraigaabe
		{
			private set;
			get;
		}

		/// <summary>
		/// Fals true isc davon auszugehe das GefectBaitritFraigaabe mit fortscraitender Zait erraict werd oone das aktuele Grid zu verlase.
		/// (kaine angraifende Objekte vorhande, kaine di Treferpunkte des Scif mindernde Ainflüse vorhande.)
		/// </summary>
		[JsonProperty]
		public bool? InRaumAktuelTreferpunkteVolsctändigRegeneriirbar
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? GefectBescteeheFraigaabe
		{
			private set;
			get;
		}

		/// <summary>
		/// Das Gefect kan unabhängig von Beweegung des Scif fortgefüürt werde, somit kan Beweegung für andere Zweke (z.B. Loot) verwendet werde.
		/// </summary>
		[JsonProperty]
		public bool? GefectUnabhängigVonBeweegungFraigaabe
		{
			private set;
			get;
		}

		/// <summary>
		/// Liste der für Gefect bevorzuugt zu wäälende DamageType da zwisce Munitioon mit untersciidlice Scaadensbeträäge gewäält were sol
		/// werd zu jeedem DamageType auc gewictung angegeebe.
		/// </summary>
		[JsonProperty]
		public SictDamageMitBetraagIntValue[] FürGefectListeDamageTypePrio
		{
			private	set;
			get;
		}

		[JsonProperty]
		public SictOverViewObjektZuusctand WirkungDestruktDroneTargetNääxteOverviewObj
		{
			private set;
			get;
		}

		[JsonProperty]
		public	SictAufgaabeParam[] PrioMengeAufgaabeObjektWirkungDestruktFürDroneBeraitsTargetedPrioHööcste
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictAufgaabeParam[] PrioMengeAufgaabeObjektWirkungDestruktFürModuleDestruktBeraitsTargetedPrioHööcste
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? FürWirkungDestruktAufgaabeDroneEngageTarget
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictWertMitZait<SictGefectAngraifendeHalteAufDistanceScritErgeebnis> GefectAngraifendeHalteAufDistanceLezteErgeebnisZuZait
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? GefectModusAngraifendeObjekteAufDistanzHalte
		{
			private set;
			get;
		}

		/*
		 * 2014.05.00
		 * 
		 * Ersaz durc SictFürGefectAufgaabeManööverErgeebnis
		 * 
		[JsonProperty]
		KeyValuePair<SictOverViewObjektZuusctand,	SictVerlaufBeginUndEndeRef<ShipUiIndicationAuswert>>? GefectAngraifendeHalteAufDistanceAufgaabeManööverErgeebnis;
		 * */

		[JsonProperty]
		SictFürGefectAufgaabeManööverErgeebnis FürGefectAufgaabeManööverErgeebnis;

		static public IEnumerable<IGrouping<SictShipClassEnum?,	SictAufgaabeParam>> MengeAufgaabeOrdnetNaacShipClassSigRadius(
			IEnumerable<SictAufgaabeParam> MengeAufgaabeInRaumObjektZuBearbaite,
			bool	Absctaigend	= false)
		{
			if (null == MengeAufgaabeInRaumObjektZuBearbaite)
			{
				return null;
			}

			var	ShipClassOrdnungSignatureRadiusAufstaigendMitIndex	=
				ShipClassOrdnungSignatureRadiusAufstaigend
				.Select((ShipClass,	Index) => new	KeyValuePair<SictShipClassEnum,	int>(ShipClass,	Index))
				.ToArray();

			var MengeAufgaabeInRaumObjektZuBearbaiteGrupiirt =
				MengeAufgaabeInRaumObjektZuBearbaite
				.GroupBy((AufgaabeInRaumObjektZuBearbaite) =>
					{
						/*
						 * 2014.03.00
						 * 
						if (null == AufgaabeInRaumObjektZuBearbaite)
						{
							return null;
						}

						var ObjektRepr =
							((SictInRaumObjektReprZuusctand)AufgaabeInRaumObjektZuBearbaite.OverViewObjektZuBearbeite) ??
							AufgaabeInRaumObjektZuBearbaite.TargetZuBearbeite;

						if (null == ObjektRepr)
						{
							return null;
						}

						var ObjektReprShipClassNulbar = ObjektRepr.AusMenuShipClassSictStringMitZait.Wert.Value;

						return ObjektReprShipClassNulbar;
						 * */
						if (null == AufgaabeInRaumObjektZuBearbaite)
						{
							return null;
						}

						var OverViewObjektZuBearbeite = AufgaabeInRaumObjektZuBearbaite.OverViewObjektZuBearbaiteVirt();
						var TargetZuBearbeite = AufgaabeInRaumObjektZuBearbaite.TargetZuBearbaiteVirt();

						if (null != OverViewObjektZuBearbeite)
						{
							var ObjektReprShipClassNulbar = OverViewObjektZuBearbeite.AusMenuShipClassSictStringMitZait.Wert.Value;

							if (ObjektReprShipClassNulbar.HasValue)
							{
								return ObjektReprShipClassNulbar;
							}
						}

						if (null != TargetZuBearbeite)
						{
							var ObjektReprShipClassNulbar = TargetZuBearbeite.AusMenuShipClassSictStringMitZait.Wert.Value;

							if (ObjektReprShipClassNulbar.HasValue)
							{
								return ObjektReprShipClassNulbar;
							}
						}

						return null;
					});

			var MengeAufgaabeInRaumObjektZuBearbaiteOrdnet =
				MengeAufgaabeInRaumObjektZuBearbaiteGrupiirt
				.OrderBy((AufgaabeInRaumObjektZuBearbaiteGrupe) =>
					{
						var ObjektReprShipClassNulbar = AufgaabeInRaumObjektZuBearbaiteGrupe.Key;

						if (!ObjektReprShipClassNulbar.HasValue)
						{
							return int.MaxValue;
						}

						var ShipClassMitIndex =
							ShipClassOrdnungSignatureRadiusAufstaigendMitIndex.FirstOrDefault((Kandidaat) => Kandidaat.Key == ObjektReprShipClassNulbar.Value);

						if (!(ShipClassMitIndex.Key == ObjektReprShipClassNulbar))
						{
							return int.MaxValue;
						}

						return ShipClassMitIndex.Value	* (Absctaigend	? -1 : 1);
					});

			return MengeAufgaabeInRaumObjektZuBearbaiteOrdnet;
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictGefectAngraifendeHalteAufDistanceScritErgeebnis
	{
		[JsonProperty]
		readonly	public Int64? ObjektAufDistanceZuHalteDistanceScrankeMax;

		[JsonProperty]
		readonly	public SictAufgaabeParam[] ListeAufgaabeAufDistanceZuHalteParam;

		[JsonProperty]
		readonly public bool? BeginNaacrangig;

		public SictGefectAngraifendeHalteAufDistanceScritErgeebnis()
		{
		}

		public SictGefectAngraifendeHalteAufDistanceScritErgeebnis(
			Int64? ObjektAufDistanceZuHalteDistanceScrankeMax,
			SictAufgaabeParam[] ListeAufgaabeAufDistanceZuHalteParam,
			bool? BeginNaacrangig)
		{
			this.ObjektAufDistanceZuHalteDistanceScrankeMax	= ObjektAufDistanceZuHalteDistanceScrankeMax;
			this.ListeAufgaabeAufDistanceZuHalteParam	= ListeAufgaabeAufDistanceZuHalteParam;
			this.BeginNaacrangig = BeginNaacrangig;
		}
	}

	/// <summary>
	/// Bescraibt wii Objekte bai der Priorisatioon ainzuornde sind.
	/// Werd zum Baiscpiil verwendet um anzufliigende Objekte, zu aktiviirende Acc-Gate oder zu zersctöörende Objekte Missionsscpeziifisc zu Priorisiire.
	/// </summary>
	[JsonObject(MemberSerialization.OptIn)]
	public struct SictInRaumObjektBearbaitungPrio
	{
		/// <summary>
		/// Objekt werd aingeordnet in Grupe welce auf Distanz gehalte werde um Scaade an aigene Ship zu vermaide.
		/// werd (in Kombi mit InGrupeIndex>0) z.B. für Strategikon für Mission "Recon" verwendet in der ain Acc-Gate aktiviirt werde sol um zu flücte.
		/// Prio Grupe FürGefectAngraiferAufDistanzHalte werd nur gebildet wen Treferpunkte hinraicend gering.
		/// </summary>
		[JsonProperty]
		public bool? InGrupeFürGefectAngraiferAufDistanzHalte
		{
			private set;
			get;
		}

		/// <summary>
		/// Objekt werd aingeordnet in Grupe angraifender Objekte.
		/// Prio Grupe Angraifend werd gebildet um Angraifer zuersct Anzugraife bevor andere geaggroed wern.
		/// </summary>
		[JsonProperty]
		public bool? InGrupeAngraifend
		{
			private set;
			get;
		}

		/// <summary>
		/// Objekt werd aingeordnet in Prio Grupe als häte das Objekt ale mit diisem Feld angegeebene EWar gewirkt.
		/// Wii di ainzelne EWar aingeordnet werde kan u.a. von Fitting abhängig sain (so werd zum Baispiil TrackingDisrupt hööher Priorisiirt wen aigene Ship Turret verwendet.)
		/// </summary>
		[JsonProperty]
		public EWarTypeEnum[]	InGrupeEWar
		{
			private set;
			get;
		}

		/// <summary>
		/// Kan verwendet werden inerhalb der Grupe welce durc ale anderen Felder (FürMission, EWar) bescriibe werd zuusäzlic zu diferenziire.
		/// Sind in ainer durc dii andere Felder gebildete Grupe Objekte mit InGrupeIndex != null enthalte, dan werd diise Grupe in waitere Grupe Untertailt.
		/// hööhere Zaal = hööhere Prio.
		/// </summary>
		[JsonProperty]
		public int? InGrupeIndex
		{
			private set;
			get;
		}

		public SictInRaumObjektBearbaitungPrio PrioritäätVersezt(int InGrupeIndexVersaz)
		{
			return new SictInRaumObjektBearbaitungPrio(
				InGrupeFürGefectAngraiferAufDistanzHalte,
				InGrupeAngraifend,
				InGrupeEWar,
				(InGrupeIndex ?? 0) + InGrupeIndexVersaz);
		}

		public SictInRaumObjektBearbaitungPrio(
			bool? InGrupeFürGefectAngraiferAufDistanzHalte = null,
			bool? InGrupeAngraifend = null,
			EWarTypeEnum[] InGrupeEWar = null,
			int? InGrupeIndex	=	null)
			:
			this()
		{
			this.InGrupeFürGefectAngraiferAufDistanzHalte = InGrupeFürGefectAngraiferAufDistanzHalte;
			this.InGrupeAngraifend = InGrupeAngraifend;
			this.InGrupeEWar = InGrupeEWar;
			this.InGrupeIndex = InGrupeIndex;
		}

		static public SictInRaumObjektBearbaitungPrio KonstruktFürInGrupeIndex(int? InGrupeIndex)
		{
			return new SictInRaumObjektBearbaitungPrio(null, null, null, InGrupeIndex);
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictAufgaabeInRaumObjektZuBearbaiteMitPrio
	{
		[JsonProperty]
		public	SictAufgaabeParam	AufgaabeObjektZuBearbaite
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictInRaumObjektBearbaitungPrio Prioritäät
		{
			private set;
			get;
		}

		public	SictAufgaabeInRaumObjektZuBearbaiteMitPrio()
		{
		}

		public	SictAufgaabeInRaumObjektZuBearbaiteMitPrio(
			SictAufgaabeParam	AufgaabeObjektZuBearbaite,
			SictInRaumObjektBearbaitungPrio Prioritäät	=	default(SictInRaumObjektBearbaitungPrio))
		{
			this.AufgaabeObjektZuBearbaite	= AufgaabeObjektZuBearbaite;
			this.Prioritäät	= Prioritäät;
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictAufgaabeGrupePrio
	{
		[JsonProperty]
		public SictAufgaabeParam[] MengeAufgaabe
		{
			private set;
			get;
		}

		[JsonProperty]
		public string	GrupePrioNaame
		{
			private set;
			get;
		}

		public SictAufgaabeGrupePrio()
		{
		}

		public SictAufgaabeGrupePrio(
			SictAufgaabeParam	AufgaabeParam,
			string GrupePrioNaame)
			:
			this(
			new	SictAufgaabeParam[]{	AufgaabeParam},
			GrupePrioNaame)
		{
		}

		public SictAufgaabeGrupePrio(
			SictAufgaabeParam[] MengeAufgaabeParam,
			string GrupePrioNaame)
		{
			this.MengeAufgaabe = MengeAufgaabeParam;
			this.GrupePrioNaame = GrupePrioNaame;
		}
	}

	public enum SictAktioonPrioEnum
	{
		Kain	= 0,

		/// <summary>
		/// Aktioon sol durcgefüürt werde bevor Wirkmitel auf InRaumObjekt aktiviirt werde.
		/// </summary>
		VorWirkung,

		/// <summary>
		/// Aktioon sol durcgefüürt werde bevor Objekt gelocked werd.
		/// </summary>
		VorLock,

		VorUnLock,

		VorRegelungDistanzZuObjekt,
	}
}
