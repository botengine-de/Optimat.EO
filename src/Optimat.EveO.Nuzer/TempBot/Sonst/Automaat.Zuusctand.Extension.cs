using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Optimat.EveOnline.AuswertGbs;
using Optimat.EveOnline.VonSensor;
using Optimat.ScpezEveOnln;


namespace Optimat.EveOnline.Anwendung
{
	static public class AutomaatZuusctandExtension
	{
		/*
		 * 2015.02.05
		 * 
		 * Verscoobe naac Bib Optimat.EveOnline.
		 * 
		static public int? FürOreTypVolumeMili(OreTypSictEnum OreTyp)
		{
			switch (OreTyp)
			{
				case OreTypSictEnum.Arkonor:
					//	https://www.fuzzwork.co.uk/ore/
					return 16000;
				case OreTypSictEnum.Bistot:
					//	https://www.fuzzwork.co.uk/ore/
					return 16000;
				case OreTypSictEnum.Crokite:
					//	https://www.fuzzwork.co.uk/ore/
					return 16000;
				case OreTypSictEnum.Dark_Ochre:
					//	https://www.fuzzwork.co.uk/ore/
					return 8000;
				case OreTypSictEnum.Gneiss:
					//	https://www.fuzzwork.co.uk/ore/
					return 5000;
				case OreTypSictEnum.Hedbergite:
					//	https://www.fuzzwork.co.uk/ore/
					return 3000;
				case OreTypSictEnum.Hemorphite:
					//	https://www.fuzzwork.co.uk/ore/
					return 3000;
				case OreTypSictEnum.Jaspet:
					//	https://www.fuzzwork.co.uk/ore/
					return 2000;
				case OreTypSictEnum.Kernite:
					//	https://www.fuzzwork.co.uk/ore/
					return 1200;
				case OreTypSictEnum.Mercoxit:
					//	https://www.fuzzwork.co.uk/ore/
					return 40000;
				case OreTypSictEnum.Omber:
					//	https://www.fuzzwork.co.uk/ore/
					return 600;
				case OreTypSictEnum.Plagioclase:
					return 350;
				case OreTypSictEnum.Pyroxeres:
					return 300;
				case OreTypSictEnum.Scordite:
					return 150;
				case OreTypSictEnum.Spodumain:
					//	https://www.fuzzwork.co.uk/ore/
					return 16000;
				case OreTypSictEnum.Veldspar:
					return 100;
			}

			return null;
		}
		 * */

		static public SictGbsMenuKaskaadeZuusctand MenuKaskaadeLezte(
			this	ISictAutomatZuusctand Automaat)
		{
			var Gbs = Automaat.Gbs();

			if (null == Gbs)
			{
				return null;
			}

			return Gbs.MenuKaskaadeLezte;
		}

		static public int? FürOreTypVolumeMili(
			this	ISictAutomatZuusctand Automaat,
			OreTypSictEnum OreTyp)
		{
			return	WorldConfig.FürOreTypVolumeMili(OreTyp);
		}

		static public Int64? ScnapscusFrühesteZaitAlterMili<ObjektIdentScnapscusType, SictScnapscusAlsFortsazZuuläsigType, SictZuusazInfoScnapscusType>(
			this	ISictAutomatZuusctand Automaat,
			SictZuObjektMengeScnapscusZuZaitAggr<ObjektIdentScnapscusType, SictScnapscusAlsFortsazZuuläsigType, SictZuusazInfoScnapscusType> ObjektMitScnapscusFrühesteZait)
			where SictScnapscusAlsFortsazZuuläsigType : SictScnapscusAlsFortsazZuuläsig<ObjektIdentScnapscusType>, new()
		{
			if (null == ObjektMitScnapscusFrühesteZait)
			{
				return null;
			}

			if (null == Automaat)
			{
				return null;
			}

			return Automaat.NuzerZaitMili - ObjektMitScnapscusFrühesteZait.ScnapscusFrühesteZait;
		}

		static public int? ZuObjektBerecneAlterScnapscusAnzaal<ObjektIdentScnapscusType, SictScnapscusAlsFortsazZuuläsigType, SictZuusazInfoScnapscusType>(
			this	ISictAutomatZuusctand Automaat,
			SictZuObjektMengeScnapscusZuZaitAggr<ObjektIdentScnapscusType, SictScnapscusAlsFortsazZuuläsigType, SictZuusazInfoScnapscusType> ObjektMitScnapscusFrühesteZait)
			where SictScnapscusAlsFortsazZuuläsigType : SictScnapscusAlsFortsazZuuläsig<ObjektIdentScnapscusType>, new()
		{
			if (null == ObjektMitScnapscusFrühesteZait)
			{
				return null;
			}

			return Automaat.ZuNuzerZaitMiliBerecneAlterScnapscusAnzaal(ObjektMitScnapscusFrühesteZait.ScnapscusFrühesteZait);
		}

		static public int? ZuNuzerZaitMiliBerecneAlterScnapscusAnzaal(
			this	ISictAutomatZuusctand Automaat,
			Int64? NuzerZaitMili)
		{
			if (null == Automaat)
			{
				return null;
			}

			var ScritZait = Automaat.ZuNuzerZaitBerecneScritZait(NuzerZaitMili);

			if (!ScritZait.HasValue)
			{
				return null;
			}

			return Automaat.ScritLezteIndex - ScritZait.Value.ScritIndex;
		}

		static public Int64? ZuScnapscusFrühesteZaitAlsNuzerZaitBerecneScritVorherNuzerZait<ObjektIdentScnapscusType, SictScnapscusAlsFortsazZuuläsigType, SictZuusazInfoScnapscusType>(
			this	ISictAutomatZuusctand Automaat,
			SictZuObjektMengeScnapscusZuZaitAggr<ObjektIdentScnapscusType, SictScnapscusAlsFortsazZuuläsigType, SictZuusazInfoScnapscusType> ObjektMitScnapscusFrühesteZait)
			where SictScnapscusAlsFortsazZuuläsigType : SictScnapscusAlsFortsazZuuläsig<ObjektIdentScnapscusType>, new()
		{
			if (null == ObjektMitScnapscusFrühesteZait)
			{
				return null;
			}

			return Automaat.ZuNuzerZaitBerecneScritVorherNuzerZait(ObjektMitScnapscusFrühesteZait.ScnapscusFrühesteZait);
		}

		static public Int64? ZuNuzerZaitBerecneScritVorherNuzerZait(
			this	ISictAutomatZuusctand Automaat,
			Int64? NuzerZaitNullable)
		{
			if (!NuzerZaitNullable.HasValue)
			{
				return null;
			}

			return Automaat.ZuNuzerZaitBerecneScritVorherNuzerZait(NuzerZaitNullable.Value);
		}

		static public ZuScritInfoZait? ZuNuzerZaitBerecneScritZait(
			this	ISictAutomatZuusctand Automaat,
			Int64? NuzerZait)
		{
			if (!NuzerZait.HasValue)
			{
				return null;
			}

			return Automaat.ZuNuzerZaitBerecneScritZait(NuzerZait.Value);
		}

		static public ZuScritInfoZait? ZuNuzerZaitBerecneScritZait(
			this	ISictAutomatZuusctand Automaat,
			Int64 NuzerZait)
		{
			if (null == Automaat)
			{
				return null;
			}

			var ListeScritZait = Automaat.ListeScritZait;

			foreach (var ScritZait in ListeScritZait.Reverse())
			{
				if (ScritZait.NuzerZait == NuzerZait)
				{
					return ScritZait;
				}
			}

			return null;
		}

		static public ZuScritInfoZait? ZuNuzerZaitBerecneScritVorherZait(
			this	ISictAutomatZuusctand Automaat,
			Int64 NuzerZait)
		{
			if (null == Automaat)
			{
				return null;
			}

			var ListeScritZait = Automaat.ListeScritZait;

			foreach (var ScritZait in ListeScritZait.Reverse())
			{
				if (ScritZait.NuzerZait < NuzerZait)
				{
					return ScritZait;
				}
			}

			return null;
		}

		static public int? ZuNuzerZaitBerecneScritIndex(
			this	ISictAutomatZuusctand Automaat,
			Int64 NuzerZait)
		{
			var ZuScritZaitInfo = Automaat.ZuNuzerZaitBerecneScritVorherZait(NuzerZait);

			if (!ZuScritZaitInfo.HasValue)
			{
				return null;
			}

			return ZuScritZaitInfo.Value.ScritIndex;
		}

		static public int? ZuNuzerZaitBerecneAlterScritAnzaal(
			this	ISictAutomatZuusctand Automaat,
			Int64 NuzerZait)
		{
			if (null == Automaat)
			{
				return null;
			}

			var ZuScritIndex = ZuNuzerZaitBerecneScritIndex(Automaat, NuzerZait);

			return Automaat.ScritLezteIndex - ZuScritIndex;
		}

		static public Int64? ZuNuzerZaitBerecneScritVorherNuzerZait(
			this	ISictAutomatZuusctand Automaat,
			Int64 NuzerZait)
		{
			var ZuScritZaitInfo = Automaat.ZuNuzerZaitBerecneScritVorherZait(NuzerZait);

			if (!ZuScritZaitInfo.HasValue)
			{
				return null;
			}

			return ZuScritZaitInfo.Value.NuzerZait;
		}

		static public SictGbsMenuKaskaadeZuusctand
			GbsMenuLezteInAst<GbsObjektScnapscusType, SictZuusazInfoScnapscusType>(
			this	ISictAutomatZuusctand Automaat,
			SictZuGbsObjektZuusctandMitIdentPerIntMitZuusazInfo<GbsObjektScnapscusType, SictZuusazInfoScnapscusType> ObjektAusGbs)
			where GbsObjektScnapscusType : GbsElement
		{
			if (null == ObjektAusGbs)
			{
				return null;
			}

			return GbsMenuLezteInAstMitHerkunftAdrese(Automaat, ObjektAusGbs.GbsAstHerkunftAdrese);
		}

		static public SictGbsMenuKaskaadeZuusctand
			GbsMenuLezteInAst(
			this	ISictAutomatZuusctand Automaat,
			GbsElement GbsAst)
		{
			if (null == GbsAst)
			{
				return null;
			}

			return GbsMenuLezteInAstMitHerkunftAdrese(Automaat, GbsAst.Ident);
		}

		static public SictGbsMenuKaskaadeZuusctand
			GbsMenuLezteInAstMitHerkunftAdrese(
			this	ISictAutomatZuusctand Automaat,
			Int64? GbsAstHerkunftAdrese)
		{
			if (null == Automaat)
			{
				return null;
			}

			if (!GbsAstHerkunftAdrese.HasValue)
			{
				return null;
			}

			/*
			 * 2015.03.12
			 * 
			 * Ersaz durc ToCustomBotSnapshot
				
			 var OptimatScritAktuelGbsBaum = Automaat.VonNuzerMeldungZuusctandTailGbsBaum;
				 * */
			var OptimatScritAktuelGbsBaum = Automaat.ListeScnapscusLezteAuswertungErgeebnisNaacSimu;

			var Gbs = Automaat.Gbs();

			if (null == Gbs)
			{
				return null;
			}

			var ListeMenuKaskaade = Gbs.ListeMenuKaskaade;

			if (null == ListeMenuKaskaade)
			{
				return null;
			}

			var ListeMenuKaskaadeInAst = new List<SictGbsMenuKaskaadeZuusctand>();

			foreach (var MenuKaskaade in ListeMenuKaskaade)
			{
				var MenuWurzelAnnaameLezte = MenuKaskaade.MenuWurzelAnnaameLezte;

				if (null == MenuWurzelAnnaameLezte)
				{
					continue;
				}

				var MenuWurzelAnnaameLezteHerkunftAdrese = (Int64?)MenuWurzelAnnaameLezte.Ident;

				if (!MenuWurzelAnnaameLezteHerkunftAdrese.HasValue)
				{
					continue;
				}

				var MenuWurzelLiigtInGbsAst = false;

				try
				{
					if (MenuWurzelAnnaameLezte.Ident == GbsAstHerkunftAdrese)
					{
						MenuWurzelLiigtInGbsAst = true;
						continue;
					}

					if (null != OptimatScritAktuelGbsBaum)
					{
						if (OptimatScritAktuelGbsBaum.AstMitHerkunftAdreseEnthaltAstMitHerkunftAdrese(
							GbsAstHerkunftAdrese.Value,
							MenuWurzelAnnaameLezteHerkunftAdrese.Value))
						{
							MenuWurzelLiigtInGbsAst = true;
						}
					}
				}
				finally
				{
					if (MenuWurzelLiigtInGbsAst)
					{
						ListeMenuKaskaadeInAst.Add(MenuKaskaade);
					}
				}
			}

			return
				ListeMenuKaskaadeInAst
				.OrderByDescending((Kandidaat) => Kandidaat.BeginZait ?? int.MinValue)
				.FirstOrDefault();
		}

		static public ModuleButtonHintInterpretiirt
			ScnapscusLezteModuleButtonHint(
			this	ISictAutomatZuusctand Automaat)
		{
			if (null == Automaat)
			{
				return null;
			}

			var ListeScnapscusLezteAuswertungErgeebnisNaacSimu = Automaat.ListeScnapscusLezteAuswertungErgeebnisNaacSimu;

			if (null == ListeScnapscusLezteAuswertungErgeebnisNaacSimu)
			{
				return null;
			}

			return ListeScnapscusLezteAuswertungErgeebnisNaacSimu.ModuleButtonHintInterpretiirt;
		}

		static public Optimat.EveOnline.SictOptimatParamMine
			VonNuzerParamMine(
			this	ISictAutomatZuusctand Automaat)
		{
			if (null == Automaat)
			{
				return null;
			}

			var OptimatParam = Automaat.OptimatParam();

			if (null == OptimatParam)
			{
				return null;
			}

			return OptimatParam.Mine;
		}

		static public SictTargetZuusctand TargetAktuel(
			this	SictShipUiModuleReprZuusctand Module)
		{
			if (null == Module)
			{
				return null;
			}

			var ModuleListeAktivitäätLezte = Module.ListeAktivitäätLezte;

			if (null == ModuleListeAktivitäätLezte)
			{
				return null;
			}

			return ModuleListeAktivitäätLezte.BeginTarget;
		}

		static public IEnumerable<SictShipUiModuleReprZuusctand>
			ShipMengeModule(
			this	ISictAutomatZuusctand Automaat)
		{
			if (null == Automaat)
			{
				return null;
			}

			var FittingUndShipZuusctand = Automaat.FittingUndShipZuusctand;

			if (null == FittingUndShipZuusctand)
			{
				return null;
			}

			return FittingUndShipZuusctand.MengeModuleRepr;
		}

		/*
		 * 2014.09.26
		 * 
	static public IEnumerable<KeyValuePair<SictTargetZuusctand, SictWertMitZait<KeyValuePair<GbsListGroupedEntryZuusctand, Int64>>>>
		MengeZuTargetMesungSurveyScanListEntryUndErzMenge(
		this	ISictAutomatZuusctand Automaat)
	{
		var AgregatioonAusTransitionInfo = Automaat.AgregatioonAusTransitionInfo();

		if (null == AgregatioonAusTransitionInfo)
		{
			return null;
		}

		return AgregatioonAusTransitionInfo.MengeZuTargetMesungSurveyScanListEntryUndErzMenge;
	}
		 * */

		static public SictGbsAgregatioonAusTransitionInfo AgregatioonAusTransitionInfo(
			this	ISictAutomatZuusctand Automaat)
		{
			var Gbs = Automaat.Gbs();

			if (null == Gbs)
			{
				return null;
			}

			return Gbs.AgregatioonAusTransitionInfo;
		}

		static public SictGbsWindowZuusctand WindowOverView(
			this	ISictAutomatZuusctand Automaat)
		{
			var Gbs = Automaat.Gbs();

			if (null == Gbs)
			{
				return null;
			}

			return Gbs.WindowOverView();
		}

		static public SictGbsWindowZuusctand WindowSurveyScanView(
			this	ISictAutomatZuusctand Automaat)
		{
			var Gbs = Automaat.Gbs();

			if (null == Gbs)
			{
				return null;
			}

			return Gbs.WindowSurveyScanView();
		}

		static public SictGbsZuusctand Gbs(
			this	ISictAutomatZuusctand Automaat)
		{
			if (null == Automaat)
			{
				return null;
			}

			return Automaat.Gbs;
		}

		static public ShipState ShipZuusctand(
			this	ISictAutomatZuusctand Automaat)
		{
			var FittingUndShipZuusctand = Automaat.FittingUndShipZuusctand();

			if (null == FittingUndShipZuusctand)
			{
				return null;
			}

			return FittingUndShipZuusctand.ShipZuusctand;
		}

		static public SictShipZuusctandMitFitting FittingUndShipZuusctand(
			this	ISictAutomatZuusctand Automaat)
		{
			if (null == Automaat)
			{
				return null;
			}

			return Automaat.FittingUndShipZuusctand;
		}

		static public SictOverviewUndTargetZuusctand OverviewUndTarget(
			this	ISictAutomatZuusctand Automaat)
		{
			if (null == Automaat)
			{
				return null;
			}

			return Automaat.OverviewUndTarget;
		}

		static public IEnumerable<SictTargetZuusctand> MengeTarget(
			this	ISictAutomatZuusctand Automaat)
		{
			var OverviewUndTarget = Automaat.OverviewUndTarget();

			if (null == OverviewUndTarget)
			{
				return null;
			}

			return OverviewUndTarget.MengeTarget;
		}

		static public IEnumerable<SictTargetZuusctand> MengeTargetNocSictbar(
			this	ISictAutomatZuusctand Automaat)
		{
			var OverviewUndTarget = Automaat.OverviewUndTarget();

			if (null == OverviewUndTarget)
			{
				return null;
			}

			return OverviewUndTarget.MengeTargetNocSictbar;
		}
	}
}
