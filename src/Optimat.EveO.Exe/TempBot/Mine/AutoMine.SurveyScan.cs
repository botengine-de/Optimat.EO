using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Optimat.ScpezEveOnln;
using Bib3;
using Newtonsoft.Json;
//using Optimat.EveOnline.AuswertGbs;
using Optimat.EveOnline.VonSensor;
using ExtractFromOldAssembly.Bib3;

namespace Optimat.EveOnline.Anwendung
{
	public partial class SictAutoMine
	{
		public IEnumerable<SictAufgaabeParam> FürMineListeAufgaabeNääxteParamBerecneTailSurveyScan(
			ISictAutomatZuusctand AutomaatZuusctand)
		{
			if (null == AutomaatZuusctand)
			{
				return null;
			}

			var NuzerZaitMili = AutomaatZuusctand.NuzerZaitMili;

			var ListeAufgaabeParam = new List<SictAufgaabeParam>();

			var MenuKaskaadeLezte = AutomaatZuusctand.MenuKaskaadeLezte();
			var MenuKaskaadeLezteAlterScritAnzaal = AutomaatZuusctand.ZuObjektBerecneAlterScnapscusAnzaal(MenuKaskaadeLezte);
			var TargetInputFookusTransitioonLezteAlterScritAnzaal = AutomaatZuusctand.TargetInputFookusTransitioonLezteAlterScritAnzaal();

			var MinerAktivitäätZyyklusGrenzeNaheSurveyScan =
				MengeTargetVerwendet
				.AnyNullable((Kandidaat) => Kandidaat.Value.MinerAktivitäätZyyklusGrenzeNaheSurveyScan ?? false);

			var ShipMengeModule = AutomaatZuusctand.ShipMengeModule();

			var ShipModuleSurveyScanner =
				ShipMengeModule
				.FirstOrDefaultNullable((Kandidaat) => Kandidaat.IstSurveyScanner ?? false);

			var WindowSurveyScanView = AutomaatZuusctand.WindowSurveyScanView();

			var WindowSurveyScanViewList = (null == WindowSurveyScanView) ? null : WindowSurveyScanView.ListHaupt;

			var WindowSurveyScanViewListListeEntry = (null == WindowSurveyScanViewList) ? null : WindowSurveyScanViewList.ListeEntry();

			var WindowSurveyScanViewListListeEntryGrupiirtNaacOreTyp =
				(null == WindowSurveyScanViewListListeEntry) ? null :
				WindowSurveyScanViewListListeEntry
				.GroupBy((Kandidaat) => Kandidaat.OreTypSictString ?? Kandidaat.BescriftungTailTitel)
				.ToArray();

			var SurveyScanAlterMili = AutomaatZuusctand.ScnapscusFrühesteZaitAlterMili(WindowSurveyScanViewList);

			var BisSurveyScanBeginZaitDauer = SurveyScanBeginZaitScrankeMin - NuzerZaitMili;

			var WindowSurveyScanViewListScnapscus = (null == WindowSurveyScanViewList) ? null : WindowSurveyScanViewList.AingangScnapscusTailObjektIdentLezteBerecne();

			if (MengeTargetVerwendet.WhereNullable((Kandidaat) => Kandidaat.Value.OreTypFraigaabe ?? false).NullOderLeer())
			{
				return ListeAufgaabeParam;
			}

			if (!(0 < BisSurveyScanBeginZaitDauer))
			{
				//	Verbindung zwisce SurveyScan und Target mese.

				var SurveyScanNoi = false;

				if (null == WindowSurveyScanViewList ||
					!(SurveyScanAlterMili < 1000 * 60 * 4) ||
					(4000 < SaitSurveyScanLezteShipStreke) ||
					(!(SurveyScanAlterMili < 1000 * 10) &&
					(MinerAktivitäätZyyklusGrenzeNaheSurveyScan ?? false)))
				{
					SurveyScanNoi = true;
				}
				else
				{
					var VonMinerNuldurcgangBisSurveyScanZait =
						(WindowSurveyScanViewList.ScnapscusFrühesteZait ?? 0) - SurveyScanBeginZaitScrankeMin;

					if (VonMinerNuldurcgangBisSurveyScanZait < 0)
					{
						SurveyScanNoi = true;
					}
				}

				if (SurveyScanNoi)
				{
					SictAufgaabeParam SurveyScanKonstruktAufgaabeParam = null;

					if (null == WindowSurveyScanView)
					{
						SurveyScanKonstruktAufgaabeParam = AufgaabeParamAndere.KonstruktModuleScalteAin(ShipModuleSurveyScanner);
					}
					else
					{
						var WindowSurveyScanViewScnapscus =
							WindowSurveyScanView.AingangScnapscusTailObjektIdentLezteBerecne() as WindowSurveyScanView;

						if (null == WindowSurveyScanViewScnapscus)
						{
						}
						else
						{
							SictAufgaabeParam SurveyScanEntferneAufgaabeParam = null;

							var ButtonClear = WindowSurveyScanViewScnapscus.ButtonClear;

							if (null == ButtonClear)
							{
								SurveyScanEntferneAufgaabeParam = AufgaabeParamAndere.KonstruktWindowMinimize(WindowSurveyScanView);
							}
							else
							{
								SurveyScanEntferneAufgaabeParam = AufgaabeParamAndere.KonstruktMausPfaad(SictAufgaabeParamMausPfaad.KonstruktMausKlikLinx(ButtonClear));
							}

							if (null != SurveyScanEntferneAufgaabeParam)
							{
								SurveyScanKonstruktAufgaabeParam =
									SictAufgaabeParam.KonstruktAufgaabeParam(
									SurveyScanEntferneAufgaabeParam,
									"clear Survey Scan");
							}
						}
					}

					if (null != SurveyScanKonstruktAufgaabeParam)
					{
						ListeAufgaabeParam.Add(SictAufgaabeParam.KonstruktAufgaabeParam(SurveyScanKonstruktAufgaabeParam, "create new Survey Scan"));
					}
				}

				if (!SurveyScanNoi && null != WindowSurveyScanViewList)
				{
					if (null != ListeTargetVerwendetMengeErzRestZuMeseNääxte.Key &&
						!(MenuKaskaadeLezteAlterScritAnzaal < 2) &&
						!(TargetInputFookusTransitioonLezteAlterScritAnzaal < 2))
					{
						var Target = ListeTargetVerwendetMengeErzRestZuMeseNääxte;

						if (1 < MengeTargetVerwendet.CountNullable())
						{
							//	zunääxt InputFookus auf andere Target seze damit bai Auswaal über SurveyScanView Entry aine Transitioon beoobactet werde kan.

							if (Target.Key.InputFookusTransitioonLezteZiilWert ?? false)
							{
								var TargetAndere =
									MengeTargetVerwendet
									.FirstOrDefault((Kandidaat) => !(Kandidaat.Key == Target.Key));

								ListeAufgaabeParam.Add(
									AufgaabeParamAndere.KonstruktTargetInputFookusSeze(TargetAndere.Key));
							}
						}

						var TargetAusSurveyScanListGroup = Target.Value.AusSurveyScanListGroup;
						var AusSurveyScanMengeListItemPasendZuInRaumObjekt = Target.Value.AusSurveyScanMengeListItemPasendZuInRaumObjekt;

						var ZuTargetAusSurveyScanInfo = Target.Value.AusSurveyScanInfo;

						var TargetAusSurveyScanListGroupInScnapscusLezteSictbar =
							(null == TargetAusSurveyScanListGroup) ? (bool?)null : TargetAusSurveyScanListGroup.InLezteScnapscusSictbar();

						var TargetAusSurveyScanMengeListItemPasendZuOreTypeMitMenuLezte =
							AusSurveyScanMengeListItemPasendZuInRaumObjekt
							.WhereNullable((ListEntry) => !(true == ListEntry.IstGroup))
							.SelectNullable((ListEntry) =>
								new KeyValuePair<GbsListGroupedEntryZuusctand, SictGbsMenuKaskaadeZuusctand>(
									ListEntry,
									AutomaatZuusctand.GbsMenuLezteInAstMitHerkunftAdrese(ListEntry.GbsAstHerkunftAdrese)))
							.ToArrayNullable();

						var TargetAusSurveyScanMengeListItemPasendZuOreTypeMitMenuLezteOrdnet =
							TargetAusSurveyScanMengeListItemPasendZuOreTypeMitMenuLezte
							.OrderByNullable((Kandidaat) => ((null == Kandidaat.Value) ? null : Kandidaat.Value.BeginZait) ?? int.MinValue)
							.ToArrayNullable();

						{
							ListeTargetVerwendetMengeErzRestZuMeseNääxte = Target;

							var WindowSurveyScanViewListListeGroup =
								WindowSurveyScanViewList.ListeEntry()
								.WhereNullable((Kandidaat) => Kandidaat.IstGroup ?? false)
								.ToArrayNullable();

							{
								//	Ale andere Group collapse.

								var WindowSurveyScanViewListListeGroupZuCollapse =
									WindowSurveyScanViewListListeGroup
									.WhereNullable((Kandidaat) =>
										(Kandidaat.IstExpanded ?? true) &&
										!(Kandidaat == TargetAusSurveyScanListGroup))
									.OrderByDescendingNullable((Kandidaat) => (Kandidaat.IstExpanded ?? false) ? 1 : 0)
									.ToArrayNullable();

								foreach (var ListGroup in WindowSurveyScanViewListListeGroupZuCollapse)
								{
									ListeAufgaabeParam.Add(AufgaabeParamAndere.KonstruktListEntryCollapse(ListGroup));
									break;
								}
							}

							var SurveyScanListEntryZuMeseNääxte =
								TargetAusSurveyScanMengeListItemPasendZuOreTypeMitMenuLezteOrdnet
								.FirstOrDefaultNullable((Kandidaat) => !(Kandidaat.Key.IstGroup ?? false) && (Kandidaat.Key.InLezteScnapscusSictbar()));

							if (null != SurveyScanListEntryZuMeseNääxte.Key)
							{
								var SurveyScanListEntryZuMeseNääxteLezteMenu = SurveyScanListEntryZuMeseNääxte.Value;

								var SurveyScanListEntryZuMeseNääxteLezteMenuBeginZait =
									(null == SurveyScanListEntryZuMeseNääxteLezteMenu) ? null : SurveyScanListEntryZuMeseNääxteLezteMenu.BeginZait;

								var SurveyScanListEntryZuMeseNääxteLezteMenuAlter =
									NuzerZaitMili - (SurveyScanListEntryZuMeseNääxteLezteMenuBeginZait ?? int.MinValue);

								if (33333 < SurveyScanListEntryZuMeseNääxteLezteMenuAlter)
								{
									ListeAufgaabeParam.Add(AufgaabeParamAndere.KonstruktMenuBegin(
										SurveyScanListEntryZuMeseNääxte.Key.FläceFürMenuWurzelBerecne(),
										new SictAnforderungMenuKaskaadeAstBedingung[]{
											new	SictAnforderungMenuKaskaadeAstBedingung("non existant Entry", false)}));
								}
							}

							if (null == TargetAusSurveyScanListGroup)
							{
							}
							else
							{
								if (!(true == TargetAusSurveyScanListGroup.IstExpanded))
								{
									ListeAufgaabeParam.Add(AufgaabeParamAndere.KonstruktListEntryExpand(TargetAusSurveyScanListGroup));
								}
							}

							if (null != WindowSurveyScanViewListScnapscus)
							{
								var WindowSurveyScanViewListScnapscusScroll = WindowSurveyScanViewListScnapscus.Scroll;

								if (null != WindowSurveyScanViewListScnapscusScroll)
								{
									if (null != ZuTargetAusSurveyScanInfo)
									{
										if (WindowSurveyScanViewListScnapscusScroll.ScrollHandleAntailAnGesamtMili < 990)
										{
											if ((ZuTargetAusSurveyScanInfo.OreTypInScnapscusSurveyScanEntryIstOoberste ?? false) &&
												3 < WindowSurveyScanViewListScnapscusScroll.ScrollHandleFläceGrenzeOobnAntailAnGesamtMili &&
												!(TargetAusSurveyScanListGroupInScnapscusLezteSictbar ?? false))
											{
												ListeAufgaabeParam.Add(
													new AufgaabeParamScrollToTop(WindowSurveyScanViewListScnapscusScroll));
											}
											else
											{
												if ((ZuTargetAusSurveyScanInfo.OreTypInScnapscusSurveyScanEntryIstUnterste ?? false) &&
													WindowSurveyScanViewListScnapscusScroll.ScrollHandleFläceGrenzeUntnAntailAnGesamtMili < 997)
												{
													ListeAufgaabeParam.Add(
														new AufgaabeParamScrollDown(WindowSurveyScanViewListScnapscusScroll));
												}
											}
										}
									}
								}
							}
						}

						/*
						 * 2014.10.00
						 * 
						ListeAufgaabeParam.AddRange(
							VerbindungSurveyScanZuTargetListeAufgaabeParam
							.Select((AufgaabeParam) => SictAufgaabeParam.KonstruktAufgaabeParam(AufgaabeParam, "connect Survey Scan to Target")));
						 * */
					}
				}
			}

			return ListeAufgaabeParam;
		}
	}
}
