using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bib3;
using Newtonsoft.Json;
using Optimat.EveOnline.Anwendung.AuswertGbs;
//using Optimat.EveOnline.AuswertGbs;
using Optimat.EveOnline.VonSensor;
using Optimat.ScpezEveOnln;
using ExtractFromOldAssembly.Bib3;

namespace Optimat.EveOnline.Anwendung
{
	[JsonObject(MemberSerialization.OptIn)]
	public class SictAufgaabeParamMausPfaad
	{
		[JsonProperty]
		public GbsElement[] ListeWeegpunktGbsObjekt
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? MausTasteLinxAin
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? MausTasteReczAin
		{
			private set;
			get;
		}

		public SictAufgaabeParamMausPfaad()
		{
		}

		public SictAufgaabeParamMausPfaad(
			GbsElement[] ListeWeegpunktGbsObjekt,
			bool? MausTasteLinxAin	= null,
			bool? MausTasteReczAin	= null)
		{
			this.ListeWeegpunktGbsObjekt = ListeWeegpunktGbsObjekt;
			this.MausTasteLinxAin = MausTasteLinxAin;
			this.MausTasteReczAin = MausTasteReczAin;
		}

		static	public SictAufgaabeParamMausPfaad Konstrukt(
			GbsElement ZiilFläce,
			bool? MausTasteLinxAin = null,
			bool? MausTasteReczAin = null)
		{
			return new SictAufgaabeParamMausPfaad(new GbsElement[] { ZiilFläce }, MausTasteLinxAin, MausTasteReczAin);
		}

		static public SictAufgaabeParamMausPfaad KonstruktMausKlikLinx(GbsElement ZiilFläce)
		{
			return Konstrukt(ZiilFläce, true, null);
		}

		static public SictAufgaabeParamMausPfaad KonstruktMausKlikRecz(GbsElement ZiilFläce)
		{
			return Konstrukt(ZiilFläce, null, true);
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictAufgaabeParamGbsAstOklusioonVermaidung
	{
		[JsonProperty]
		public GbsElement GbsAst
		{
			private set;
			get;
		}

		[JsonProperty]
		public int? RestFläceKwadraatSaitenlängeScrankeMin
		{
			private set;
			get;
		}

		public SictAufgaabeParamGbsAstOklusioonVermaidung()
		{
		}

		public SictAufgaabeParamGbsAstOklusioonVermaidung(
			GbsElement GbsAst,
			int? RestFläceKwadraatSaitenlängeScrankeMin = null)
		{
			this.GbsAst = GbsAst;
			this.RestFläceKwadraatSaitenlängeScrankeMin = RestFläceKwadraatSaitenlängeScrankeMin;
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public partial class SictAufgaabeParam
	{
		[JsonProperty]
		public string[] ZwekListeKomponenteZuusaz
		{
			private set;
			get;
		}

		/// <summary>
		/// Werd verwendet zur Scactelung, z.B. um zuusäzlice ZwekListeKomponente vorne anzuhänge.
		/// </summary>
		[JsonProperty]
		public SictAufgaabeParam AufgaabeParam
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? NaacAbsclusTailWirkungWartezaitScranke
		{
			private set;
			get;
		}

		/*
		 * 2014.09.12
		 * 
		 * Ersaz durc abgelaitete Type
		 * 
		[JsonProperty]
		public SictShipCargoTypSictEnum? ShipAktuelOpenInventoryCargoTyp
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictShipCargoTypSictEnum? ShipAktuelCargoLeereTyp
		{
			private set;
			get;
		}
		 * */

		virtual public int WartezaitBisEntscaidungErfolgScritAnzaalMax()
		{
			return 0;
		}

		virtual public SictAufgaabeParamZerleegungErgeebnis Zerleege(
			ISictAutomatZuusctand AutomaatZuusctand,
			SictAufgaabeKombiZuusctand	KombiZuusctand)
		{
			var Ergeebnis = new SictAufgaabeParamZerleegungErgeebnis();

			Ergeebnis.FüügeAn(this.AufgaabeParam);

			Ergeebnis.ZerleegungVolsctändigSezeAin();

			return Ergeebnis;
		}

		static public string ModuleIdentSictZwekKomponente(
			SictShipUiModuleReprZuusctand	ModuleRepr)
		{
			if (null == ModuleRepr)
			{
				return null;
			}

			string ModuleTitelBescriftung = null;
			string LaageSictString = null;

			var	LaageLezte	= ModuleRepr.ListeLaageLezteBerecne();

			if (LaageLezte.HasValue)
			{
				LaageSictString = ((int)LaageLezte.Value.A).ToString() + "," + ((int)LaageLezte.Value.B).ToString();
			}

			var ModuleButtonHintGültigMitZait = ModuleRepr.ModuleButtonHintGültigMitZait;

			if (null != ModuleButtonHintGültigMitZait.Wert)
			{
				var ModuleButtonHint = ModuleButtonHintGültigMitZait.Wert.ModuleButtonHint;

				if (null != ModuleButtonHint)
				{
					var ModuleButtonHintZaileTitel = ModuleButtonHint.ZaileTitel;

					if (null != ModuleButtonHintZaileTitel)
					{
						var ZaileTitelBescriftungMiinusFormat = ModuleButtonHintZaileTitel.BescriftungMiinusFormat;

						if (null != ZaileTitelBescriftungMiinusFormat)
						{
							ModuleTitelBescriftung	= ZaileTitelBescriftungMiinusFormat;
						}
					}
				}
			}

			return	(ModuleTitelBescriftung	?? "Unknown") + "(" + (LaageSictString	?? "") + ")";
		}

		static public string ManööverEntrySictZwekKomponente(
			SictVerlaufBeginUndEndeRef<ShipUiIndicationAuswert>	ManööverMitZait)
		{
			if (null == ManööverMitZait)
			{
				return null;
			}

			var Manööver = ManööverMitZait.Wert;

			if (null == Manööver)
			{
				return null;
			}

			var	ShipUIIndication	= Manööver.ShipUIIndication;

			if (null == ShipUIIndication)
			{
				return null;
			}

			return (ShipUIIndication.IndicationCaption ?? "") + " " + (ShipUIIndication.IndicationText ?? "");
		}

		static public string TreeViewEntrySictZwekKomponente(
			VonSensor.TreeViewEntry	Entry)
		{
			if (null == Entry)
			{
				return null;
			}

			return Entry.LabelText;
		}

		static public string ObjektSictZwekKomponente(
			IEnumerable<VonSensor.InventoryItem>	MengeInventoryItem)
		{
			return "Item[" + (MengeInventoryItemSictZwekKomponente(MengeInventoryItem)	?? "")	+ "]";
		}

		static public string MengeInventoryItemSictZwekKomponente(
			IEnumerable<VonSensor.InventoryItem>	MengeInventoryItem)
		{
			if (null == MengeInventoryItem)
			{
				return null;
			}

			return
				string.Join(",", MengeInventoryItem.Select((InventoryItem) => ((null == InventoryItem) ? null : InventoryItem.Name) ?? "").ToArray());
		}

		static public string ObjektSictZwekKomponente(
			VonSensor.Window	Window)
		{
			return "Window[" + (WindowSictZwekKomponente(Window) ?? "") + "]";
		}

		static public string ColumnHeaderSictZwekKomponente(
			VonSensor.ListColumnHeader	ColumnHeader)
		{
			return (null == ColumnHeader) ? null : ColumnHeader.HeaderBescriftung;
		}

		static public string ObjektSictZwekKomponente(
			VonSensor.ListColumnHeader	ColumnHeader)
		{
			return "Column Header[" + (ColumnHeaderSictZwekKomponente(ColumnHeader) ?? "") + "]";
		}

		static public string ListEntrySictZwekKomponente(
			GbsListGroupedEntryZuusctand ListEntry)
		{
			return (null == ListEntry) ? null : ListEntry.Bescriftung;
		}

		static public string ObjektSictZwekKomponente(
			GbsListGroupedEntryZuusctand ListEntry)
		{
			return "List Entry[" + (ListEntrySictZwekKomponente(ListEntry) ?? "") + "]";
		}

		static public string ObjektSictZwekKomponente(
			SictShipUiModuleReprZuusctand ModuleRepr)
		{
			return "Module[" + (ModuleIdentSictZwekKomponente(ModuleRepr) ?? "") + "]";
		}

		static public string OverViewObjektSictZwekKomponente(SictOverViewObjektZuusctand OverviewObjekt)
		{
			if (null == OverviewObjekt)
			{
				return null;
			}

			return OverviewObjekt.Name;
		}

		static public string ObjektSictZwekKomponente(SictTargetZuusctand Target)
		{
			return "Target[" + (TargetSictZwekKomponente(Target) ?? "") + "]";
		}

		static public string TargetSictZwekKomponente(SictTargetZuusctand Target)
		{
			if (null == Target)
			{
				return null;
			}

			var TargetAingangScnapscusLezte = Target.AingangScnapscusTailObjektIdentLezteBerecne();

			if (null == TargetAingangScnapscusLezte)
			{
				return null;
			}

			var	OoberhalbDistanceListeZaile	= TargetAingangScnapscusLezte.ÜberDistanceListeZaile;

			if (null == OoberhalbDistanceListeZaile)
			{
				return null;
			}

			return string.Join("", OoberhalbDistanceListeZaile.Where((t) => null != t));
		}

		static public string ObjektSictZwekKomponente(InfoPanelTypSictEnum InfoPanel)
		{
			return "InfoPanel[" + (InfoPanel.ToString()) + "]";
		}

		static public string ObjektSictZwekKomponente(SictGbsWindowZuusctand Window)
		{
			return "Window[" + (WindowSictZwekKomponente(Window) ?? "") + "]";
		}

		static public string WindowSictZwekKomponente(VonSensor.Window Window)
		{
			if (null == Window)
			{
				return null;
			}

			return Window.HeaderCaptionText;
		}

		static public string WindowSictZwekKomponente(SictGbsWindowZuusctand Window)
		{
			if (null == Window)
			{
				return null;
			}

			return Window.WindowHeaderCaptionText;
		}

		static public string ObjektSictZwekKomponente(SictOptimatParamFitting Fitting)
		{
			return "Fitting[" + (FittingSictZwekKomponente(Fitting) ?? "") + "]";
		}

		static public string FittingSictZwekKomponente(SictOptimatParamFitting Fitting)
		{
			if (null == Fitting)
			{
				return null;
			}

			return Fitting.AusFittingManagementFittingZuLaade;
		}

		static public string ObjektSictZwekKomponente(VonSensor.LobbyAgentEntry	AusLobbyAgentEntry)
		{
			return "Lobby.Agent[" + (AusLobbyAgentEntrySictZwekKomponente(AusLobbyAgentEntry) ?? "") + "]";
		}

		static public string AusLobbyAgentEntrySictZwekKomponente(VonSensor.LobbyAgentEntry AusLobbyAgentEntry)
		{
			if (null == AusLobbyAgentEntry)
			{
				return null;
			}

			return AusLobbyAgentEntry.AgentName;
		}

		static public string ObjektSictZwekKomponente(SictMissionZuusctand	Mission)
		{
			return "Mission[" + (MissionSictZwekKomponente(Mission) ?? "") + "]";
		}

		static public string MissionSictZwekKomponente(SictMissionZuusctand Mission)
		{
			if (null == Mission)
			{
				return null;
			}

			var TailFürNuzer = Mission.TailFürNuzer;

			if (null == TailFürNuzer)
			{
				return null;
			}

			return	(TailFürNuzer.Titel ?? "") + "," + (TailFürNuzer.AgentName	?? "");
		}

		static public string ObjektSictZwekKomponente(OrtogoonInt? Fläce)
		{
			return "Area[" + (FläceSictZwekKomponente(Fläce) ?? "null") + "]";
		}

		static public string FläceSictZwekKomponente(OrtogoonInt? Fläce)
		{
			if (!Fläce.HasValue)
			{
				return null;
			}

			/*
			 * 2015.02.23
			 * 
			var	PunktMinUndPunktMax	= Fläce.GibPunktMinUndPunktMax();

			return
				((int)PunktMinUndPunktMax.Key.A).ToString() + "," +
				((int)PunktMinUndPunktMax.Key.B).ToString() + "," +
				((int)PunktMinUndPunktMax.Value.A).ToString() + "," +
				((int)PunktMinUndPunktMax.Value.B).ToString() + ",";
			 * */

			/*
			 * 2015.02.24
			 * 
			return Fläce.ToString();
			 * */

			return
				(Fläce.Value.PunktMin.A).ToString() + "," +
				(Fläce.Value.PunktMin.B).ToString() + "," +
				(Fläce.Value.PunktMax.A).ToString() + "," +
				(Fläce.Value.PunktMax.B).ToString() + ",";
		}

		virtual public IEnumerable<string> ZwekListeKomponenteAusParamBerecne()
		{
			return null;
		}

		public IEnumerable<string> ZwekListeKomponenteBerecne()
		{
			return Optimat.Glob.ListeErwaitertAlsArray(ZwekListeKomponenteZuusaz,	ZwekListeKomponenteAusParamBerecne());
		}

		/// <summary>
		/// Gibt an ob für deen Erfolg der Aufgaabe di Gbs Zuusctand Grupe welce durc diise Aufgaabe und Komponente geändert wurde unverändert blaibe mus naacdeem ale Wirkunge aingegeebe wurde.
		/// klasisces Baispiil isc Tooltip welce nur dan erscaint wen Mauszaiger auf GBS Ast verwailt.
		/// </summary>
		/// <returns></returns>
		virtual	public bool WirkungGbsMengeZuusctandGrupeVorrausgeseztAlsUnverändertNaacAbsclusTailWirkung()
		{
			return false;
		}

		virtual public bool IstBlatNaacNuzerVorsclaagWirkung()
		{
			return false;
		}

		static public string FürNuzerObjektBescraibungBerecne(SictTargetZuusctand Objekt)
		{
			if (null == Objekt)
			{
				return null;
			}

			var TargetReprLezteMitZait = Objekt.TargetReprLezteMitZait;

			string TargetReprString = null;

			if (TargetReprLezteMitZait.HasValue)
			{
				if (null != TargetReprLezteMitZait.Value.Wert)
				{
					var OoberhalbDistanceListeZaile = TargetReprLezteMitZait.Value.Wert.ÜberDistanceListeZaile;

					if (null != OoberhalbDistanceListeZaile)
					{
						TargetReprString =
							string.Join("", OoberhalbDistanceListeZaile.Where((Zaile) => null != Zaile).ToArray());
					}
				}
			}

			return "Target[" + (TargetReprString ?? "") + "]";
		}

		static public string FürNuzerObjektBescraibungBerecne(SictOverViewObjektZuusctand Objekt)
		{
			if (null == Objekt)
			{
				return null;
			}

			return "OverviewObject[" + (Objekt.Name ?? "") + "]";
		}

		public SictAufgaabeParam()
		{
		}

		public SictAufgaabeParam(
			string[] ZwekListeKomponenteZuusaz = null,
			SictAufgaabeParam AufgaabeParam	= null)
		{
			this.ZwekListeKomponenteZuusaz = ZwekListeKomponenteZuusaz;

			this.AufgaabeParam = AufgaabeParam;
		}

		static public SictAufgaabeParam KonstruktAufgaabeParam(
			SictAufgaabeParam AufgaabeParam,
			string ZwekKomponenteZuusaz)
		{
			return new	SictAufgaabeParam(new string[] { ZwekKomponenteZuusaz }, AufgaabeParam);
		}

		static public SictAufgaabeParam KonstruktAufgaabeParam(
			SictAufgaabeParam AufgaabeParam,
			string[] ZwekListeKomponenteZuusaz = null)
		{
			return new SictAufgaabeParam(ZwekListeKomponenteZuusaz: ZwekListeKomponenteZuusaz, AufgaabeParam: AufgaabeParam);
		}

		virtual public SictVorsclaagNaacProcessWirkung NaacNuzerVorsclaagWirkungVirt()
		{
			return null;
		}

		virtual public SictNaacNuzerMeldungZuEveOnline NaacNuzerMeldungZuEveOnlineVirt()
		{
			return null;
		}

		virtual public SictAufgaabeParamMausPfaad MausPfaadVirt()
		{
			return null;
		}

		virtual public SictGbsWindowZuusctand WindowMinimizeVirt()
		{
			return null;
		}

		virtual public SictOverViewObjektZuusctand OverViewObjektZuBearbaiteVirt()
		{
			return null;
		}

		virtual public SictTargetZuusctand TargetZuBearbaiteVirt()
		{
			return null;
		}

		virtual public bool? AktioonWirkungDestruktVirt()
		{
			return null;
		}

		virtual public Int64? DistanzAinzuscteleScrankeMinVirt()
		{
			return null;
		}

		virtual public Int64? DistanzAinzuscteleScrankeMaxVirt()
		{
			return null;
		}

		virtual public bool? VorrangVorManööverUnterbreceNictVirt()
		{
			return null;
		}

		virtual public SictVerlaufBeginUndEndeRef<ShipUiIndicationAuswert> ManööverUnterbreceNictVirt()
		{
			return null;
		}

	}
}
