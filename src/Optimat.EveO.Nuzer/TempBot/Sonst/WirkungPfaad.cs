using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Optimat.EveOnline.Anwendung
{
	public enum SictGbsZuusctandGrupe
	{
		Kain = 0,

		/// <summary>
		/// Laage der Maus kan z.B. für ModuleButtonHint aussclaaggeebend sain. (wen Maus von ModuleButton fortbeweegt werd, werd infolge ModuleButtonHint entfernt)
		/// </summary>
		MausLaage	= 10,

		/// <summary>
		/// z.B. geöfnete Menu oder UtilMenu
		/// </summary>
		MenuZuusctand	= 30,

		/// <summary>
		/// Änderung des Input Fookus von/auf aine Repräsentatioon aines InRaumObjekt (z.B. Overview Zaile oder Target)
		/// 
		/// in Eve Online Klient wurde Wexelwirkung zwisce Auswaal Target und Auswaal Overview Zaile beobactet.
		/// Daher werde diise hiir in aine Grupe zusamegefast.
		/// </summary>
		ZuInRaumObjektInputFookus = 40,

		/// <summary>
		/// Manöver welces auf in Raum Objekt (welce u.a. durc Overview Zaile oder Target repräsentiirt werde) bezuug nimt. z.B. "Approach" oder "Orbit"
		/// </summary>
		ZuInRaumObjektManööver	= 50,
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictZuAufgaabeInfoKompatibilitäät
	{
		/// <summary>
		/// Tail des GBS Zuusctand welcer für Ausfüürung diiser Aufgaabe als unverändert vorrausgesezt werd (Bsp.: für Aufgaabe Module Toggle werd vorrausgesezt Selected Target unverändert)
		/// (Klasifikatioon als unverändert werd üüblicerwaise verwendet mit Bezuug des mit lezte Optimat Scrit von Nuzer geliiferte Bescraibung des GBS Zuusctand)
		/// daraus folgt: wen sait der Berecnung diiser Aufgaabe aus Mesung Zuusctand aine Aufgaabe ausgefüürt wurde welce aine in diise Field bezaicnete Taile in desen
		/// Field WirkungGbsZuusctandVerändert enthalt kan diise Aufgaabe nit meer durcgefüürt werde.
		/// </summary>
		[JsonProperty]
		public SictGbsZuusctandGrupe[] BedingungGbsZuusctandVerändertNict;

		/// <summary>
		/// Tail des Gbs Zuusctand welce durc diise Aufgaabe mööglicerwaise geändert werd.
		/// </summary>
		[JsonProperty]
		public SictGbsZuusctandGrupe[] WirkungGbsZuusctandVerändert;

		public SictZuAufgaabeInfoKompatibilitäät()
		{
		}

		public SictZuAufgaabeInfoKompatibilitäät(
			SictGbsZuusctandGrupe[] BedingungGbsZuusctandVerändertNict,
			SictGbsZuusctandGrupe[] WirkungGbsZuusctandVerändert)
		{
			this.BedingungGbsZuusctandVerändertNict = BedingungGbsZuusctandVerändertNict;
			this.WirkungGbsZuusctandVerändert = WirkungGbsZuusctandVerändert;
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictVorsclaagNaacZiilProcessWirkungMitInfoKompatibilitäät
	{
		[JsonProperty]
		public SictVorsclaagNaacProcessWirkung VorsclaagWirkung;

		/// <summary>
		/// Tail des Gbs Zuusctand welcer erhalte blaibe mus damit diise Aufgaabe ausgefüürt werde kan.
		/// daraus folgt: wen sait der Berecnung diiser Aufgaabe aus Mesung Zuusctand aine Aufgaabe ausgefüürt wurde welce aine in diise Field bezaicnete Taile in desen
		/// Field BedingungGbsZuusctandErhalte enthalt kan diise Aufgaabe nit meer durcgefüürt werde.
		/// </summary>
		[JsonProperty]
		public SictZuAufgaabeInfoKompatibilitäät Kompatibilitäät;

		public SictVorsclaagNaacZiilProcessWirkungMitInfoKompatibilitäät()
		{
		}

		public SictVorsclaagNaacZiilProcessWirkungMitInfoKompatibilitäät(
			SictVorsclaagNaacProcessWirkung VorsclaagWirkung,
			SictZuAufgaabeInfoKompatibilitäät Kompatibilitäät)
		{
			this.VorsclaagWirkung = VorsclaagWirkung;
			this.Kompatibilitäät = Kompatibilitäät;
		}
	}

	public enum SictWirkungPfaadFeelsclaagTypEnum
	{
		Kain	= 0,
		FeelsclaagZaitüberscraitung	= 10,
	}

	/// <summary>
	/// Bescrankt di Häufigkait mit der aine Fabrik Wirkunge ersctele sol.
	/// </summary>
	[JsonObject(MemberSerialization.OptIn)]
	public class SictZuWirkungFabrikDurcsazScranke
	{

	}

	[JsonObject(MemberSerialization.OptIn)]
	public	class SictWirkungPfaadFabrik : SictObjektMitBezaicnerInt
	{
		public delegate object MethodWirkungScritDelegate(
			SictAufgaabeZuusctand	WirkungPfaad,
			ref object	ZuusctandZuusaz);
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictAufgaabeScritZerleegungErgeebnis
	{
		[JsonProperty]
		public SictAufgaabeZuusctand[] MengeKomponente
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? ZerleegungVolsctändig
		{
			private set;
			get;
		}

		public SictAufgaabeScritZerleegungErgeebnis()
		{
		}

		public SictAufgaabeScritZerleegungErgeebnis(
			SictAufgaabeZuusctand[] MengeKomponente,
			bool? ZerleegungVolsctändig	= null)
		{
			this.MengeKomponente = MengeKomponente;
			this.ZerleegungVolsctändig = ZerleegungVolsctändig;
		}
	}

	/// <summary>
	/// meerere AufgaabePfaadExklusiiv köne nit paralel abgearbaitet werde.
	/// 
	/// Bsp Scpez EveOnline:
	/// Auswaal Menu Entry aus Menu Kaskaade.
	/// 
	/// Geegebaiscpiil Scpez EveOnline:
	/// Aktiviirung Module durc Tastatur isc unabhängig von Aingaabefookus inerhalb EveOnline Process.
	/// 
	/// </summary>
	class SictAufgaabePfaadExklusiiv
	{
	}
}
