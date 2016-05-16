using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Bib3;
using Newtonsoft.Json;
//using Optimat.EveOnline.AuswertGbs;
using Optimat.EveOnline.VonSensor;
using ExtractFromOldAssembly.Bib3;

namespace Optimat.EveOnline.Anwendung
{
	[JsonObject(MemberSerialization.OptIn)]
	public class SictZuInRaumObjektReprScnapscusZuusazinfo
	{
		[JsonProperty]
		readonly public string OverviewTypeSelectionName;

		[JsonProperty]
		readonly	public	KeyValuePair<SictEWarTypeEnum, Int64>[] MengeZuEWarTypeTextureIdent;

		[JsonProperty]
		readonly public SictGbsMenuKaskaadeZuusctand MenuKaskaadeLezte;

		[JsonProperty]
		readonly public VonSensor.MenuEntry MenuLezteEntryAuswaal;

		[JsonProperty]
		readonly public SictOverviewPresetFolgeViewport OverviewPresetFolgeViewportFertigLezte;

		[JsonProperty]
		readonly public	IEnumerable<KeyValuePair<string, SictOverviewPresetFolgeViewport>> MengeZuOverviewPresetNameFolgeViewportVolsctändig;

		public SictZuInRaumObjektReprScnapscusZuusazinfo()
		{
		}

		public SictZuInRaumObjektReprScnapscusZuusazinfo(
			string OverviewTypeSelectionName,
			KeyValuePair<SictEWarTypeEnum, Int64>[] MengeZuEWarTypeTextureIdent,
			SictGbsMenuKaskaadeZuusctand MenuKaskaadeLezte,
			VonSensor.MenuEntry MenuLezteEntryAuswaal,
			SictOverviewPresetFolgeViewport OverviewPresetFolgeViewportFertigLezte,
			IEnumerable<KeyValuePair<string, SictOverviewPresetFolgeViewport>> MengeZuOverviewPresetNameFolgeViewportVolsctändig)
		{
			this.OverviewTypeSelectionName = OverviewTypeSelectionName;
			this.MengeZuEWarTypeTextureIdent = MengeZuEWarTypeTextureIdent;
			this.MenuKaskaadeLezte = MenuKaskaadeLezte;
			this.MenuLezteEntryAuswaal = MenuLezteEntryAuswaal;
			this.OverviewPresetFolgeViewportFertigLezte = OverviewPresetFolgeViewportFertigLezte;
			this.MengeZuOverviewPresetNameFolgeViewportVolsctändig = MengeZuOverviewPresetNameFolgeViewportVolsctändig;
		}
	}

	public interface IInRaumObjektReprZuusctand
	{
		Int64? SictungLezteDistanceScrankeMin();
		Int64? SictungLezteDistanceScrankeMax();
	}

	[JsonObject(MemberSerialization.OptIn)]
	public	abstract class SictInRaumObjektReprZuusctand<InRaumObjektReprScnapscusType> :
		SictZuGbsObjektZuusctandMitIdentPerIntMitZuusazInfo<InRaumObjektReprScnapscusType, SictZuInRaumObjektReprScnapscusZuusazinfo>,
		IInRaumObjektReprZuusctand
		where	InRaumObjektReprScnapscusType	:	ObjektMitIdentInt64
	{
		new const int ListeScnapscusZuZaitZuErhalteAnzaalScrankeMax = 1;

		static	readonly public string OverviewEntryMenuEntryShipClassRegexPattern = "Remove\\s*(.*)\\s*from Overview";

		[JsonProperty]
		public Int64 AktualisLezteZait
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictWertMitZait<KeyValuePair<string, SictShipClassEnum?>> AusMenuShipClassSictStringMitZait
		{
			private set;
			get;
		}

		[JsonProperty]
		public	SictGbsMenuKaskaadeZuusctand	MenuKaskaadeLezte
		{
			private set;
			get;
		}

		/*
		 * 2014.05.14
		 * 
		 * Ersaz durc SictGbsMenuKaskaadeZuusctand	MenuKaskaadeLezte
		 * 
		[JsonProperty]
		public SictVerlaufBeginUndEndeRef<VonSensor.Menu> MenuLezteMitZait
		{
			private set;
			get;
		}

		[JsonProperty]
		public List<SictWertMitZait<SictMenuPfaadAuswaalZuusctand>> ListeMenuPfaadAuswaalMitBeginZait
		{
			private set;
			get;
		}
		 * */

		public SictVerlaufBeginUndEndeRef<VonSensor.Menu> MenuLezteMitZait
		{
			get
			{
				var MenuKaskaadeLezte = this.MenuKaskaadeLezte;

				if (null == MenuKaskaadeLezte)
				{
					return null;
				}

				return MenuKaskaadeLezte.MenuFrühesteMitBeginZaitUndEndeZaitBerecne();
			}
		}

		public SictInRaumObjektReprZuusctand()
			:
			this(ListeScnapscusZuZaitZuErhalteAnzaalScrankeMax)
		{
		}

		public SictInRaumObjektReprZuusctand(
			int? ListeScnapscusZuZaitZuErhalteAnzaalScrankeMax)
			:
			base(ListeScnapscusZuZaitZuErhalteAnzaalScrankeMax)
		{
		}

		public SictInRaumObjektReprZuusctand(
			Int64	Zait,
			InRaumObjektReprScnapscusType	GbsObjektScnapscus,
			SictZuInRaumObjektReprScnapscusZuusazinfo	ZuusazInfoScnapscus)
			:
			base(
			ListeScnapscusZuZaitZuErhalteAnzaalScrankeMax,
			Zait,
			GbsObjektScnapscus,
			ZuusazInfoScnapscus)
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="KandidaatMenu"></param>
		/// <returns>Ob übergeebene Menu für Objekt gültig.</returns>
		public bool AuswertungMenuPfaadBegin(IEnumerable<SictWertMitZait<VonSensor.Menu>> KandidaatListeMenuMitBeginZait)
		{
			if (null == KandidaatListeMenuMitBeginZait)
			{
				return false;
			}

			var KandidaatMenuFrüheste = KandidaatListeMenuMitBeginZait.FirstOrDefault();

			if (null == KandidaatMenuFrüheste.Wert)
			{
				return false;
			}

			string AusOverviewZaileMenuShipClassSictString = null;

			//	Aus Menu Entry Remove.*(Frigate|Cruiser|Battlecruiser|usw).*from Overview Ship class ermitle.

			var MenuFrühesteMengeEntry = KandidaatMenuFrüheste.Wert.ListeEntry;

			if (null == MenuFrühesteMengeEntry)
			{
				return false;
			}

			foreach (var Entry in MenuFrühesteMengeEntry)
			{
				if (null == Entry)
				{
					continue;
				}

				var EntryBescriftung = Entry.Bescriftung;

				if (null == EntryBescriftung)
				{
					continue;
				}

				var EntryBescriftungRegexMatch = Regex.Match(EntryBescriftung, OverviewEntryMenuEntryShipClassRegexPattern, RegexOptions.IgnoreCase);

				if (!EntryBescriftungRegexMatch.Success)
				{
					continue;
				}

				AusOverviewZaileMenuShipClassSictString = EntryBescriftungRegexMatch.Groups[1].Value.TrimNullable();
				break;
			}

			if (null == AusOverviewZaileMenuShipClassSictString)
			{
				//	Nur wen aus dem Menu di Ship Class ersictlic isc werd davon ausgegange das es ain Menu zu ainem InRaumObjekt isc.
				return false;
			}

			var AusOverviewZaileMenuShipClassSictEnumNulbar =
				ShipClassEnumAusMenuShipClassString(AusOverviewZaileMenuShipClassSictString);

			this.AusMenuShipClassSictStringMitZait =
				new SictWertMitZait<KeyValuePair<string, SictShipClassEnum?>>(KandidaatMenuFrüheste.Zait,
					new KeyValuePair<string, SictShipClassEnum?>(AusOverviewZaileMenuShipClassSictString, AusOverviewZaileMenuShipClassSictEnumNulbar));

			return true;
		}

		override protected void NaacAingangScnapscus(
			Int64	Zait,
			InRaumObjektReprScnapscusType	ScnapscusWert,
			SictZuInRaumObjektReprScnapscusZuusazinfo	ZuusazInfo)
		{
			try
			{
				var BisherMenuLezteMitZait = this.MenuLezteMitZait;

				var MenuKaskaadeLezte = (null == ZuusazInfo) ? null : ZuusazInfo.MenuKaskaadeLezte;
				var MenuLezteEntryAuswaal = (null == ZuusazInfo) ? null : ZuusazInfo.MenuLezteEntryAuswaal;

				var MenuLezteEntryAuswaalBescriftung = (null == MenuLezteEntryAuswaal) ? null : MenuLezteEntryAuswaal.Bescriftung;

				var ListeMenuMitBeginZait = (null == MenuKaskaadeLezte) ? null : MenuKaskaadeLezte.ListeMenuScnapscusLezteMitBeginZaitBerecne();

				if (null != MenuKaskaadeLezte)
				{
					//	ma scauen ob diise MenuKaskaade zu diise InRaumObjektRepr gehöört.

					//	nur wen Objekt in lezte Scnapscus sictbar war sol Verbindung mit Menu hergesctelt werde.
					if (!MenuKaskaadeLezte.EndeZait.HasValue	&&
						null	!= AingangScnapscusTailObjektIdentLezteBerecne())
					{
						if (this is SictTargetZuusctand)
						{
							//	Temp Verzwaigung für Debug Breakpoint
						}

						var MenuKaskaadeLezteMenuWurzelAnnaameLezte = MenuKaskaadeLezte.MenuWurzelAnnaameLezte;

						if (MenuWurzelPasendZuInRaumObjektRepr(MenuKaskaadeLezteMenuWurzelAnnaameLezte))
						{
							if (MenuPfaadBeginMööglicFürListeMenuBerecne(MenuKaskaadeLezte.ListeMenuScnapscusLezteMitBeginZaitBerecne()))
							{
								this.MenuKaskaadeLezte = MenuKaskaadeLezte;
							}
						}
					}
				}
			}
			finally
			{
				this.AktualisLezteZait = Zait;
			}
		}

		virtual public bool MenuWurzelPasendZuInRaumObjektRepr(
			GbsElement MenuWurzel)
		{
			return false;
		}

		/// <summary>
		/// Prüüft ob Begin aines Menu Pfaad mööglic.
		/// Di Bedingungen werden für OverViewObjektZuusctand und TargetZuusctand untersciidlic formuliirt.
		/// </summary>
		/// <returns></returns>
		virtual public bool MenuPfaadBeginMööglicFürListeMenuBerecne(IEnumerable<SictWertMitZait<VonSensor.Menu>> ListeMenuMitBeginZait)
		{
			return false;
		}

		/// <summary>
		/// Prüüft ob ain Fortsaz des Menu Pfaad mööglic.
		/// Di Bedingungen werden für OverViewObjektZuusctand und TargetZuusctand untersciidlic formuliirt.
		/// </summary>
		/// <returns></returns>
		virtual public bool MenuPfaadFortsazMööglicBerecne()
		{
			return false;
		}

		/// <summary>
		/// 2014.02.26	Bsp:
		/// "Mission Generic Frigates"
		/// </summary>
		/// <param name="ShipClassString"></param>
		/// <returns></returns>
		static public SictShipClassEnum? ShipClassEnumAusMenuShipClassString(string ShipClassString)
		{
			if (null == ShipClassString)
			{
				return null;
			}

			var ShipClassStringUntrimmed = " " + ShipClassString + " ";

			var ListeShipClassName = Enum.GetNames(typeof(SictShipClassEnum));

			foreach (var ShipClassName in ListeShipClassName)
			{
				//	Pattern: vor ShipClassNaame ain Leerzaice und naac ShipClassNaame ain 's' oder Leerzaice
				var ShipClassRegexPattern = " " + Regex.Escape(ShipClassName) + "( |s)";

				if (Regex.Match(ShipClassStringUntrimmed, ShipClassRegexPattern, RegexOptions.IgnoreCase).Success)
				{
					return (SictShipClassEnum)Enum.Parse(typeof(SictShipClassEnum), ShipClassName);
				}
			}

			return null;
		}

		virtual public SictInRaumObjektReprZuusctand<InRaumObjektReprScnapscusType> Kopii()
		{
			return SictRefBaumKopii.ObjektKopiiErsctele(this);
		}

		public object Clone()
		{
			return Kopii();
		}

		abstract public	Int64? SictungLezteDistanceScrankeMin();
		abstract public Int64? SictungLezteDistanceScrankeMax();
	}
}
