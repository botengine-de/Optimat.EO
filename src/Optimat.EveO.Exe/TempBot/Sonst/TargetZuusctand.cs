using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Bib3;
//using Optimat.EveOnline.AuswertGbs;
using Optimat.EveOnline.Base;
using Optimat.EveOnline.VonSensor;
using ExtractFromOldAssembly.Bib3;

namespace Optimat.EveOnline.Anwendung
{
	/// <summary>
	/// Aggregiirt Info aus meerere Scnapscus zu Target aus LayerTarget.
	/// </summary>
	public	class SictTargetZuusctand	:	SictInRaumObjektReprZuusctand<ShipUiTarget>
	{
		public Int64? SictungLezteZait
		{
			get
			{
				return AingangUnglaicDefaultLezteZait;
			}
		}

		[JsonProperty]
		public string[] OoberhalbDistanceListeZaile
		{
			private set;
			get;
		}

		[JsonProperty]
		AusSequenzTransitioonMiinusZuusazInfo<Int64, bool> SequenzTransitioonInputFookus	= new	AusSequenzTransitioonMiinusZuusazInfo<Int64, bool>(2, 3);

		public	SictWertMitZait<bool>?	InputFookusTransitioonLezteZaitUndZiilWert
		{
			get
			{
				var SequenzTransitioonInputFookus = this.SequenzTransitioonInputFookus;

				if (null == SequenzTransitioonInputFookus)
				{
					return null;
				}

				var TransitioonLezte = SequenzTransitioonInputFookus.ListeTransitioonLezteInfo();

				return new SictWertMitZait<bool>(TransitioonLezte.ScritIdent, TransitioonLezte.ZiilWert);
			}
		}

		public Int64? InputFookusTransitioonLezteZait
		{
			get
			{
				var InputFookusTransitioonLezteZaitUndZiilWert = this.InputFookusTransitioonLezteZaitUndZiilWert;

				if (!InputFookusTransitioonLezteZaitUndZiilWert.HasValue)
				{
					return null;
				}

				return InputFookusTransitioonLezteZaitUndZiilWert.Value.Zait;
			}
		}

		public bool? InputFookusTransitioonLezteZiilWert
		{
			get
			{
				var InputFookusTransitioonLezteZaitUndZiilWert = this.InputFookusTransitioonLezteZaitUndZiilWert;

				if (!InputFookusTransitioonLezteZaitUndZiilWert.HasValue)
				{
					return null;
				}

				return InputFookusTransitioonLezteZaitUndZiilWert.Value.Wert;
			}
		}

		[JsonProperty]
		public Int64? SictungLezteDistanceScrankeMinScpezTarget
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? SictungLezteDistanceScrankeMaxScpezTarget
		{
			private set;
			get;
		}

		[JsonProperty]
		readonly AusSequenzMengeInGrupeAnzaalTransitioon<Int64, Int64> InternMengeAssignedFilterTransitioon =
		new AusSequenzMengeInGrupeAnzaalTransitioon<Int64, Int64>(2, 3);

		public IEnumerable<TransitionInfo<Int64, IEnumerable<Int64>, MengeInGrupeAnzaalTransitioon<Int64>>> ListeAssignedTransitioon()
		{
			return InternMengeAssignedFilterTransitioon.ListeTransitioonInfo();
		}

		public int? ScnapscusMengeAssignedAnzaal()
		{
			var	Scnapscus	= AingangScnapscusTailObjektIdentLezteBerecne();

			if (null == Scnapscus)
			{
				return null;
			}

			return (int?)Scnapscus.MengeAssignedGrupe.CountNullable();
		}

		public string[] MengeKandidaatObjektNameSictFürVerglaicBerecne(int ZaileListeZaicenAnzaalScrankeMin)
		{
			var TargetReprLezteMitZaitNulbar = this.TargetReprLezteMitZait;

			if (!TargetReprLezteMitZaitNulbar.HasValue)
			{
				return null;
			}

			var TargetReprLezte = TargetReprLezteMitZaitNulbar.Value.Wert;

			if (null == TargetReprLezte)
			{
				return null;
			}

			return TargetReprLezte.MengeKandidaatObjektNameSictFürVerglaicBerecne(ZaileListeZaicenAnzaalScrankeMin);
		}

		public SictWertMitZait<ShipUiTarget>? TargetReprLezteMitZait
		{
			get
			{
				return AingangScnapscusTailObjektIdentMitZaitLezteBerecne();
			}
		}

		public SictTargetZuusctand()
		{
		}

		public SictTargetZuusctand(
			SictWertMitZait<ShipUiTarget> TargetReprMitZait,
			SictZuInRaumObjektReprScnapscusZuusazinfo ZuusazInfo)
			:
			base(
			TargetReprMitZait.Zait,
			TargetReprMitZait.Wert,
			ZuusazInfo)
		{
		}

		override public bool MenuPfaadBeginMööglicFürListeMenuBerecne(IEnumerable<SictWertMitZait<VonSensor.Menu>> ListeMenuMitBeginZait)
		{
			//	Bedingung: Scnitmenge (Menu.Fläce, TargetRepr.Fläce) nict Leer

			var ListeMenuMitBeginZaitFrüheste = ListeMenuMitBeginZait.FirstOrDefault();

			if (null == ListeMenuMitBeginZaitFrüheste.Wert)
			{
				return false;
			}

			var MenuFläce = ListeMenuMitBeginZaitFrüheste.Wert.InGbsFläce;

			if (null == MenuFläce)
			{
				return false;
			}

			var TargetReprLezteMitZaitNulbar = this.TargetReprLezteMitZait;

			if (!TargetReprLezteMitZaitNulbar.HasValue)
			{
				return	false;
			}

			var TargetReprLezte = TargetReprLezteMitZaitNulbar.Value.Wert;

			if (null == TargetReprLezte)
			{
				return false;
			}

			var TargetReprLezteFläce = TargetReprLezte.InGbsFläce;

			if (null == TargetReprLezteFläce)
			{
				return false;
			}

			if (OrtogoonInt.Scnitfläce(TargetReprLezteFläce, MenuFläce).IsLeer)
			{
				return false;
			}

			return MenuPfaadFortsazMööglicBerecne();
		}

		override public bool MenuWurzelPasendZuInRaumObjektRepr(
			GbsElement MenuWurzel)
		{
			if (null == MenuWurzel)
			{
				return false;
			}

			var MenuWurzelAlsTarget = MenuWurzel as ShipUiTarget;

			if (null != MenuWurzelAlsTarget)
			{
				return MenuWurzelAlsTarget.Ident == GbsAstHerkunftAdrese;
			}

			var	AingangScnapscusUnglaicDefaultLezte	= AingangScnapscusUnglaicDefaultTailObjektIdentLezteBerecne();

			if (null == AingangScnapscusUnglaicDefaultLezte)
			{
				return false;
			}

			var GbsObjektInputFookusSeze = AingangScnapscusUnglaicDefaultLezte.GbsObjektInputFookusSeze;

			if (null != GbsObjektInputFookusSeze)
			{
				if (GbsObjektInputFookusSeze.Ident == MenuWurzel.Ident)
				{
					return true;
				}
			}

			return false;
		}

		override public bool MenuPfaadFortsazMööglicBerecne()
		{
			/*
			 * 2014.00.17	Beobactung:
			 * Kontextmenu zu Target werd geöfnet oone das Target Active gescalte
			 * */

			return true;
		}

		public bool AingangReprMitZait(
			SictWertMitZait<ShipUiTarget> TargetReprMitZait,
			SictZuInRaumObjektReprScnapscusZuusazinfo	ZuusazInfo	= null)
		{
			if (null == TargetReprMitZait.Wert)
			{
				base.AingangScnapscusLeer(TargetReprMitZait.Zait, ZuusazInfo);
				return false;
			}
			else
			{
				return base.AingangScnapscus(TargetReprMitZait.Zait, TargetReprMitZait.Wert, ZuusazInfo);
			}
		}

		override protected void NaacAingangScnapscus(
			Int64	Zait,
			ShipUiTarget TargetScnapscus,
			SictZuInRaumObjektReprScnapscusZuusazinfo ZuusazInfo)
		{
			base.NaacAingangScnapscus(Zait, TargetScnapscus, ZuusazInfo);

			var	ScnapscusInputFookus	= ((null	== TargetScnapscus)	?	null	: TargetScnapscus.Active)	?? false;

			SequenzTransitioonInputFookus.AingangScrit(Zait, ScnapscusInputFookus);

			SictungLezteDistanceScrankeMinScpezTarget = (null == TargetScnapscus) ? null : TargetScnapscus.DistanceScrankeMin;
			SictungLezteDistanceScrankeMaxScpezTarget = (null == TargetScnapscus) ? null : TargetScnapscus.DistanceScrankeMax;

			var	MengeAssignedModuleOderDroneGrupeTexturIdent	=
				(null == TargetScnapscus) ? null :
				TargetScnapscus.MengeAssignedModuleOderDroneGrupeTexturIdent().ToArrayNullable();

			OoberhalbDistanceListeZaile =
				(null == TargetScnapscus) ? null : TargetScnapscus.ÜberDistanceListeZaile;

			InternMengeAssignedFilterTransitioon.AingangScrit(
				Zait,
				MengeAssignedModuleOderDroneGrupeTexturIdent);
		}

		override	public Int64? SictungLezteDistanceScrankeMin()
		{
			return SictungLezteDistanceScrankeMinScpezTarget;
		}

		override	public Int64? SictungLezteDistanceScrankeMax()
		{
			return SictungLezteDistanceScrankeMaxScpezTarget;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns>GBS Objekt desen Klik das seze des Input Fookus auslööst.</returns>
		public GbsElement SezeInputFookusGbsObjektBerecne()
		{
			return FläceFürMenuWurzelBerecne();
		}
	}
}
