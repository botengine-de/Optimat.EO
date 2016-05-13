using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Bib3;
//using Optimat.EveOnline.AuswertGbs;
using Optimat.EveOnline.VonSensor;

namespace Optimat.EveOnline.Anwendung
{
	[JsonObject(MemberSerialization.OptIn)]
	public class SictGbsMenuZuusctand : SictZuGbsObjektZuusctandMitIdentPerInt<Menu>
	{
		new	const int ListeScnapscusZuZaitZuErhalteAnzaalScrankeMax = 1;

		[JsonProperty]
		public SictWertMitZait<MenuEntry> MenuEntryAktiviirtLezteMitZait
		{
			private set;
			get;
		}

		[JsonProperty]
		public VonSensor.MenuEntry AnnaameMenuEntryAktiiv
		{
			private set;
			get;
		}

		public SictGbsMenuZuusctand()
			:
			base(ListeScnapscusZuZaitZuErhalteAnzaalScrankeMax)
		{
		}

		public SictGbsMenuZuusctand(
			Int64 BeginZait,
			VonSensor.Menu Menu)
			:
			base(ListeScnapscusZuZaitZuErhalteAnzaalScrankeMax, BeginZait, Menu)
		{
		}

		override protected void NaacAingangScnapscus(
			Int64 ScnapscusZait,
			VonSensor.Menu ScnapscusWert)
		{
			AnnaameMenuEntryAktiivAktualisiire();
		}

		public void MenuEntryAktiviirtLezteMitZaitSeze(SictWertMitZait<VonSensor.MenuEntry> MenuEntryAktiviirtLezteMitZait)
		{
			this.MenuEntryAktiviirtLezteMitZait = MenuEntryAktiviirtLezteMitZait;

			AnnaameMenuEntryAktiivAktualisiire();
		}

		void AnnaameMenuEntryAktiivAktualisiire()
		{
			this.AnnaameMenuEntryAktiiv = AnnaameMenuEntryAktiivBerecne();
		}

		public VonSensor.MenuEntry[] ListeEntryBerecne()
		{
			var AingangScnapscusLezte = AingangScnapscusTailObjektIdentLezteBerecne();
			var AingangScnapscusVorLezte = AingangScnapscusTailObjektIdentVorLezteBerecne();

			var AingangScnapscusLezteListeEntry = (null == AingangScnapscusLezte) ? null : AingangScnapscusLezte.ListeEntry;
			var AingangScnapscusVorLezteListeEntry = (null == AingangScnapscusVorLezte) ? null : AingangScnapscusVorLezte.ListeEntry;

			return AingangScnapscusLezteListeEntry ?? AingangScnapscusVorLezteListeEntry;
		}

		VonSensor.MenuEntry AnnaameMenuEntryAktiivBerecne()
		{
			var MenuEntryAktiviirtLezteMitZait = this.MenuEntryAktiviirtLezteMitZait;

			if (null == MenuEntryAktiviirtLezteMitZait.Wert)
			{
				return null;
			}

			var ListeEntry = ListeEntryBerecne();

			if (null == ListeEntry)
			{
				return null;
			}

			var EntryAktiiv = ListeEntry.FirstOrDefault((Kandidaat) => Kandidaat.Ident == MenuEntryAktiviirtLezteMitZait.Wert.Ident);

			if (null == EntryAktiiv)
			{
				return null;
			}

			if (!(true == EntryAktiiv.Highlight))
			{
				return null;
			}

			return EntryAktiiv;
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public partial class SictGbsMenuKaskaadeZuusctand
		:
		SictZuGbsObjektZuusctandMitIdentPerIntMitZuusazInfo<VonSensor.Menu, VonSensor.Menu[]>
	{
		/*
		 * 2014.09.16
		 * 
		 * Ersaz durc ScnapscusFrühesteZait
		 * 
		[JsonProperty]
		public Int64? BeginZait
		{
			private set;
			get;
		}
		 * */

		public Int64? BeginZait
		{
			get
			{
				return this.ScnapscusFrühesteZait;
			}
		}

		/*
		 * 2014.09.16
		 * 
		 * Ersaz durc ScnapscusLeerFrühesteZait
		 * 
		[JsonProperty]
		public Int64? EndeZait
		{
			private set;
			get;
		}
		 * */

		public Int64? EndeZait
		{
			get
			{
				return ScnapscusLeerFrühesteZait;
			}
		}

		[JsonProperty]
		readonly public KeyValuePair<SictAufgaabeZuusctand[], string>[] ScritLezteWirkungMengeAufgaabePfaadZuBlatMitGrupePrioNaame;

		[JsonProperty]
		readonly public GbsElement MenuWurzelAnnaameInitiaal;

		[JsonProperty]
		public GbsElement MenuWurzelAnnaameLezte
		{
			private set;
			get;
		}

		[JsonProperty]
		readonly public List<SictGbsMenuZuusctand> ListeMenu = new List<SictGbsMenuZuusctand>();

		public IEnumerable<SictWertMitZait<SictGbsMenuZuusctand>> ListeMenuMitBeginZaitBerecne()
		{
			return SictGbsMenuZuusctand.ListeZuusctandAggrMitBeginZaitBerecneAusListeZuusctandAggr(ListeMenu);
		}

		public IEnumerable<VonSensor.MenuEntry> AusMenu0ListeEntryBerecne()
		{
			var	Menu0	= ListeMenuMitBeginZaitBerecne().FirstOrDefaultNullable();

			if (null == Menu0.Wert)
			{
				return null;
			}

			return	Menu0.Wert.ListeEntryBerecne();
		}

		public IEnumerable<SictWertMitZait<VonSensor.Menu>> ListeMenuScnapscusLezteMitBeginZaitBerecne()
		{
			return SictGbsMenuZuusctand.ListeScnapscusLezteWertTailObjektIdentMitZaitBerecneAusListeZuusctandAggrMitZait(ListeMenuMitBeginZaitBerecne());
		}

		public SictVerlaufBeginUndEndeRef<VonSensor.Menu> MenuFrühesteMitBeginZaitUndEndeZaitBerecne()
		{
			var ListeMenuMitBeginZait = ListeMenuMitBeginZaitBerecne();

			if (null == ListeMenuMitBeginZait)
			{
				return null;
			}

			var	MenuMitBeginZait	= ListeMenuMitBeginZait.FirstOrDefault();

			if (null == MenuMitBeginZait.Wert)
			{
				return null;
			}

			return new SictVerlaufBeginUndEndeRef<VonSensor.Menu>(MenuMitBeginZait.Zait, this.EndeZait, MenuMitBeginZait.Wert.AingangScnapscusTailObjektIdentLezteBerecne());
		}

		public SictGbsMenuKaskaadeZuusctand()
			:
			this(0)
		{
		}

		public SictGbsMenuKaskaadeZuusctand(
			Int64	Zait,
			KeyValuePair<SictAufgaabeZuusctand[], string>[] ScritLezteWirkungMengeAufgaabePfaadZuBlatMitGrupePrioNaame	= null,
            GbsElement MenuWurzelAnnaameInitiaal = null,
			VonSensor.Menu[] ListeMenuScnapscus	= null)
			:
			base(3)
		{
			this.ScritLezteWirkungMengeAufgaabePfaadZuBlatMitGrupePrioNaame = ScritLezteWirkungMengeAufgaabePfaadZuBlatMitGrupePrioNaame;
			this.MenuWurzelAnnaameInitiaal = MenuWurzelAnnaameInitiaal;
			this.MenuWurzelAnnaameLezte = MenuWurzelAnnaameInitiaal;

			base.AingangScnapscus(
				Zait,
				Bib3.Extension.FirstOrDefaultNullable(ListeMenuScnapscus),
				ListeMenuScnapscus);

		}

		public bool AingangScnapscus(
			Int64 Zait,
			VonSensor.Menu[] ListeMenu)
		{
			return
				base.AingangScnapscus(
				Zait,
				Bib3.Extension.FirstOrDefaultNullable(ListeMenu),
				ListeMenu);
		}

		override protected void NaacAingangScnapscus(
			Int64 Zait,
			VonSensor.Menu Menu = null,
			VonSensor.Menu[] ListeMenuScnapscus = null)
		{
			if (EndeZait.HasValue)
			{
				return;
			}

			SictGbsMenuZuusctand.ZuZaitAingangMengeObjektScnapscus(
				Zait,
				ListeMenuScnapscus,
				ListeMenu,
				false);
		}
	}
}
