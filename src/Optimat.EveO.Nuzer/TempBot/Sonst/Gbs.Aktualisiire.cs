using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Optimat.EveOnline.Anwendung;
using Optimat.ScpezEveOnln;
using Bib3;
using Optimat.EveOnline.Base;
//using Optimat.EveOnline.AuswertGbs;
using Optimat.EveOnline.VonSensor;
using Optimat.EveOnline.TempAuswertGbs;

namespace Optimat.EveOnline.Anwendung
{
	public partial class SictGbsZuusctand
	{
		static public bool MenuPrädikaatMengeEntryPerRegexPattern(
			SictGbsMenuZuusctand Menu,
			RegexOptions RegexOptions,
			IEnumerable<string> MengeBedingungEntryExistentKonjunktPattern,
			IEnumerable<string> MengeBedingungEntryExistentDisjunktPattern = null)
		{
			var MengeBedingungEntryExistentKonjunktPatternUndOptions =
				(null == MengeBedingungEntryExistentKonjunktPattern) ? null :
				MengeBedingungEntryExistentKonjunktPattern.Select((Pattern) => new KeyValuePair<string, RegexOptions>(Pattern, RegexOptions));

			var MengeBedingungEntryExistentDisjunktPatternUndOptions =
				(null == MengeBedingungEntryExistentDisjunktPattern) ? null :
				MengeBedingungEntryExistentDisjunktPattern.Select((Pattern) => new KeyValuePair<string, RegexOptions>(Pattern, RegexOptions));

			return MenuPrädikaatMengeEntryPerRegexPattern(
				Menu,
				MengeBedingungEntryExistentKonjunktPatternUndOptions,
				MengeBedingungEntryExistentDisjunktPatternUndOptions);
		}

		static public bool MenuPrädikaatMengeEntryPerRegexPattern(
			SictGbsMenuZuusctand Menu,
			IEnumerable<KeyValuePair<string, RegexOptions>> MengeBedingungEntryExistentKonjunktPatternUndOptions,
			IEnumerable<KeyValuePair<string, RegexOptions>> MengeBedingungEntryExistentDisjunktPatternUndOptions = null)
		{
			if (null == Menu)
			{
				return false;
			}

			var Scnapscus = Menu.AingangScnapscusTailObjektIdentLezteBerecne();

			if (null == Scnapscus)
			{
				return false;
			}

			var ScnapscusMengeEntry = Scnapscus.ListeEntry;

			if (null != MengeBedingungEntryExistentKonjunktPatternUndOptions)
			{
				foreach (var BedingungEntryExistentKonjunkt in MengeBedingungEntryExistentKonjunktPatternUndOptions)
				{
					var EntryExistent = VonSensor.MenuEntry.MengeEntryEnthaltEntryMitBescriftungRegexPattern(
						ScnapscusMengeEntry, BedingungEntryExistentKonjunkt.Key, BedingungEntryExistentKonjunkt.Value);

					if (!EntryExistent)
					{
						return false;
					}
				}
			}

			if (null != MengeBedingungEntryExistentDisjunktPatternUndOptions)
			{
				var BedingungDisjunktErfült = false;

				foreach (var BedingungEntryExistentDisjunkt in MengeBedingungEntryExistentDisjunktPatternUndOptions)
				{
					var EntryExistent = VonSensor.MenuEntry.MengeEntryEnthaltEntryMitBescriftungRegexPattern(
						ScnapscusMengeEntry, BedingungEntryExistentDisjunkt.Key, BedingungEntryExistentDisjunkt.Value);

					if (EntryExistent)
					{
						BedingungDisjunktErfült = true;
						break;
					}
				}

				if (!BedingungDisjunktErfült)
				{
					return false;
				}
			}

			return true;
		}

		static public bool ButtonListSurroundingsMenuPrädikaatTailSctaatisc(SictGbsMenuZuusctand Menu)
		{
			if (null == Menu)
			{
				return false;
			}

			var MengeEntryKonjunktRegexPattern = new string[]{
				"Show Info",
				"Stargates"};

			return MenuPrädikaatMengeEntryPerRegexPattern(Menu, RegexOptions.IgnoreCase, MengeEntryKonjunktRegexPattern);
		}

		public void AktualisiireTailMengeNaacNuzerMeldung(
			Int64 Zait,
			IEnumerable<SictNaacNuzerMeldungZuEveOnline> MengeMeldungAktiiv)
		{
			var MengeMeldungAktiivDistinct =
				(null == MengeMeldungAktiiv) ? null :
				MengeMeldungAktiiv.Distinct(new SictNaacNuzerMeldungZuEveOnlineEqualityHinraicenGlaicwertigFürFortsaz()).ToArray();

			Bib3.Glob.PropagiireListeRepräsentatioon(
				MengeMeldungAktiivDistinct,
				this.MengeNaacNuzerMeldung as IList<SictNaacNuzerMeldungZuEveOnline>,
				(MeldungAktiiv) => new SictNaacNuzerMeldungZuEveOnline(NaacNuzerMeldungIdent++, Zait, MeldungAktiiv),
				(KandidaatRepr, MeldungAktiiv) => SictNaacNuzerMeldungZuEveOnline.HinraicenGlaicwertigFürFortsaz(KandidaatRepr, MeldungAktiiv),
				(Repr, MeldungAktiiv) =>
				{
					if (null != MeldungAktiiv)
					{
						Repr.AktiivLezteSeze(Zait, MeldungAktiiv.AktiivLezteInWindowClientFläce);
					}
				},
				true);

			var MeldungAktiivLezteZaitScrankeMin = Zait - 10000;

			MengeNaacNuzerMeldung.RemoveAll((Kandidaat) => !(MeldungAktiivLezteZaitScrankeMin < Kandidaat.AktiivLezteZait));
		}

		public void Aktualisiire(
			SictAutomatZuusctand AutomaatZuusctand,
			VonSensorikMesung AusScnapscusAuswertungZuusctand)
		{
			if (null == AutomaatZuusctand)
			{
				return;
			}

			var AgregatioonAusTransitionInfo = this.AgregatioonAusTransitionInfo;

			var ZaitMili = AutomaatZuusctand.NuzerZaitMili;

			var OverviewUndTarget = AutomaatZuusctand.OverviewUndTarget;

			/*
			 * 2015.03.12
			 * 
			 * Ersaz durc ToCustomBotSnapshot
				var GbsBaum = AutomaatZuusctand.VonNuzerMeldungZuusctandTailGbsBaum;
			 * */

			var GbsBaum = AutomaatZuusctand.ListeScnapscusLezteAuswertungErgeebnisNaacSimu;

			var MengeWindow = this.MengeWindow;
			var ListeAbovemainMessageAuswertMitZait = this.ListeAbovemainMessageAuswertMitZait;

			var ScnapscusWindowOverview = (null == AusScnapscusAuswertungZuusctand) ? null : AusScnapscusAuswertungZuusctand.WindowOverview;

			var NeocomClockZaitKalenderModuloTaagStringUndMinMax = (null == AusScnapscusAuswertungZuusctand) ? null :
				AusScnapscusAuswertungZuusctand.NeocomClockZaitKalenderModuloTaagMinMax;

			var ScnapscusWindowOverviewZaileInputFookusExklusiiv = (null == ScnapscusWindowOverview) ? null : ScnapscusWindowOverview.ZaileMitInputFookusExklusiiv();

			var ScnapscusMengeWindow = (null == AusScnapscusAuswertungZuusctand) ? null :
				AusScnapscusAuswertungZuusctand.MengeWindowBerecne();

			var ScnapscusMengeWindowSictbar =
				Bib3.Extension.WhereNullable(ScnapscusMengeWindow, (Kandidaat) => null == Kandidaat ? false : (true == Kandidaat.Sictbar))
				.ToArrayNullable();

			var ScnapscusMengeMenu = (null == AusScnapscusAuswertungZuusctand) ? null : AusScnapscusAuswertungZuusctand.MengeMenu;

			if (null == AgregatioonAusTransitionInfo)
			{
				this.AgregatioonAusTransitionInfo = AgregatioonAusTransitionInfo = new SictGbsAgregatioonAusTransitionInfo();
			}

			AgregatioonAusTransitionInfo.Agregiire(AutomaatZuusctand);

			if (null == MengeWindow)
			{
				this.MengeWindow = MengeWindow = new List<SictGbsWindowZuusctand>();
			}

			if (NeocomClockZaitKalenderModuloTaagStringUndMinMax.HasValue)
			{
				this.NeocomClockZaitModuloTaagMinMax = NeocomClockZaitKalenderModuloTaagStringUndMinMax.Value;
			}

			var ListeAusGbsAbovemainMessageMitZait = AutomaatZuusctand.ListeAusGbsAbovemainMessageMitZait();

			var ListeAusGbsAbovemainMessageMitZaitLezte =
				(null == ListeAusGbsAbovemainMessageMitZait) ? null : ListeAusGbsAbovemainMessageMitZait.LastOrDefault();

			if (null != AusScnapscusAuswertungZuusctand)
			{
				if (null != AusScnapscusAuswertungZuusctand.InfoPanelLocationInfo)
				{
					InfoPanelLocationInfoSictbarLezteZaitMili = ZaitMili;
				}

				if (null != AusScnapscusAuswertungZuusctand.InfoPanelRoute)
				{
					InfoPanelRouteSictbarLezteZaitMili = ZaitMili;
				}

				if (null != AusScnapscusAuswertungZuusctand.InfoPanelMissions)
				{
					InfoPanelMissionsSictbarLezteZaitMili = ZaitMili;
				}
			}

			SictGbsWindowZuusctand.ZuZaitAingangMengeObjektScnapscus(
				ZaitMili,
				ScnapscusMengeWindowSictbar,
				MengeWindow,
				false,
				AgregatioonAusTransitionInfo);

			{
				//	Window entferne welce zu lange nit meer geöfnet sind.
				MengeWindow.RemoveAll((Kandidaat) => !Kandidaat.InLezteScnapscusSictbar());
			}

			{
				var MengeWindowGrupe = MengeWindow.GroupBy((Kandidaat) => Kandidaat.GbsAstHerkunftAdrese).ToArray();

				if (MengeWindowGrupe.Any((Grupe) => 1 < Grupe.Count()))
				{
					//	Temp für Debug Verzwaigung
				}
			}

			{
				//	Berecnung	ListeAbovemainMessageAuswertMitZait

				if (null != ListeAusGbsAbovemainMessageMitZaitLezte)
				{
					var ListeAusGbsAbovemainMessageLezte = ListeAusGbsAbovemainMessageMitZaitLezte.Wert;

					var ListeAusGbsAbovemainMessageLezteText = ListeAusGbsAbovemainMessageLezte.LabelText;

					if (null != ListeAusGbsAbovemainMessageLezteText)
					{
						if (null != ListeAusGbsAbovemainMessageLezte)
						{
							if (null == ListeAbovemainMessageAuswertMitZait)
							{
								this.ListeAbovemainMessageAuswertMitZait = ListeAbovemainMessageAuswertMitZait =
									new List<SictVerlaufBeginUndEndeRef<SictMessageStringAuswert>>();
							}

							SictVerlaufBeginUndEndeRef<SictMessageStringAuswert> AbovemainMessageAuswertMitZait =
								ListeAbovemainMessageAuswertMitZait
								.LastOrDefault((Kandidaat) => Kandidaat.BeginZait == ListeAusGbsAbovemainMessageMitZaitLezte.BeginZait);

							if (null == AbovemainMessageAuswertMitZait)
							{
								var AbovemainMessageAuswert = new SictMessageStringAuswert(ListeAusGbsAbovemainMessageLezteText);

								AbovemainMessageAuswertMitZait = new SictVerlaufBeginUndEndeRef<SictMessageStringAuswert>(
									ListeAusGbsAbovemainMessageMitZaitLezte.BeginZait,
									ListeAusGbsAbovemainMessageMitZaitLezte.EndeZait,
									AbovemainMessageAuswert);

								AbovemainMessageAuswert.Berecne();

								ListeAbovemainMessageAuswertMitZait.Add(AbovemainMessageAuswertMitZait);

								if (AbovemainMessageAuswert.ClusterShutdownZaitDistanz.HasValue)
								{
									AbovemainMessageClusterShutdownLezte = AbovemainMessageAuswertMitZait;
								}
							}

						}
					}
				}
			}

			ListeAbovemainMessageDronesLezteAuswertMitZait =
				ListeAbovemainMessageAuswertMitZait
				.LastOrDefaultNullable((Kandidaat) =>
					null == Kandidaat.Wert ? false :
					(Kandidaat.Wert.DroneBandwithAvailableMili.HasValue ||
					Kandidaat.Wert.DroneCommandRange.HasValue ||
					Kandidaat.Wert.DroneControlCountScrankeMax.HasValue));

			ListeAbovemainMessageDronesLezteAlter =
				ZaitMili - (null == ListeAbovemainMessageDronesLezteAuswertMitZait ? (Int64?)null : (ListeAbovemainMessageDronesLezteAuswertMitZait.EndeZait ?? ZaitMili));

			if (null != ListeAbovemainMessageAuswertMitZait)
			{
				foreach (var AbovemainMessageAuswertMitZait in ListeAbovemainMessageAuswertMitZait.Reversed().Take(4))
				{
					if (AbovemainMessageAuswertMitZait.EndeZait.HasValue)
					{
						continue;
					}

					var AbovemainMessageMitZait =
						Bib3.Extension.FirstOrDefaultNullable(
						ListeAusGbsAbovemainMessageMitZait,
						(Kandidaat) => Kandidaat.BeginZait == AbovemainMessageAuswertMitZait.BeginZait);

					if (null == AbovemainMessageMitZait)
					{
						AbovemainMessageAuswertMitZait.EndeZait = ZaitMili;
					}
					else
					{
						AbovemainMessageAuswertMitZait.EndeZait = AbovemainMessageMitZait.EndeZait;
					}
				}

				{
					//	Naacricte welce älter sind als Viir minuute entferne

					var ListeAbovemainMessageAuswertMitZaitZuErhalteZaitScranke = ZaitMili - 1000 * 60 * 4;

					ListeAbovemainMessageAuswertMitZait
						.RemoveAll((Kandidaat) => Kandidaat.BeginZait < ListeAbovemainMessageAuswertMitZaitZuErhalteZaitScranke);
				}
			}

			{
				//	Berecnung ListeGbsMenu

				//	Menge Menu werd geordnet für den Fal das in aine Scnapscus meerere Menu auftauce
				//	2014.02.27	Beobactung: lezte Menu erhält 0=InGbsZIndex -> noieste isc am waiteste vorne (okludiirt andere)

				var ScnapscusListeMenu =
					(null == ScnapscusMengeMenu) ? null :
					ScnapscusMengeMenu
					/*
					2015.09.01
					Änderung InGbsBaumAstIndex: Übernaame von noie Sensor InTreeIndex: Element occludes other Elements with lower Value.

					.OrderByDescending((ScnapscusMenu) => ScnapscusMenu.InGbsBaumAstIndex ?? -1)
					*/
					.OrderBy((ScnapscusMenu) => ScnapscusMenu.InGbsBaumAstIndex ?? -1)
					.ToArray();

				var ScnapscusListeMenuFrüüheste =
					Bib3.Extension.FirstOrDefaultNullable(ScnapscusListeMenu);

				var ScnapscusListeMenuFrüühesteListeEntry =
					(null == ScnapscusListeMenuFrüüheste) ? null :
					ScnapscusListeMenuFrüüheste.ListeEntry;

				var MenuKaskaadeLezte = Bib3.Extension.LastOrDefaultNullable(ListeMenuKaskaade);

				if (null == ScnapscusListeMenuFrüühesteListeEntry)
				{
					if (null != MenuKaskaadeLezte)
					{
						MenuKaskaadeLezte.AingangScnapscusLeer(ZaitMili);
					}

					MenuKaskaadeLezte = null;
				}
				else
				{
					GbsElement MenuWurzelAnnaameInitiaal = null;

					var MengeAufgaabeZuusctand = AutomaatZuusctand.MengeAufgaabeZuusctand;

					var ScritLezteWirkungMengeAufgaabePfaadZuBlatMitGrupePrioNaame =
						AutomaatZuusctand.ScritLezteWirkungMengeAufgaabePfaadZuBlatMitGrupePrioNaame
						.ToArrayNullable();

					if (null != ScritLezteWirkungMengeAufgaabePfaadZuBlatMitGrupePrioNaame)
					{
						foreach (var ScritLezteWirkungAufgaabePfaadZuBlatMitGrupePrioNaame in ScritLezteWirkungMengeAufgaabePfaadZuBlatMitGrupePrioNaame)
						{
							if (null == ScritLezteWirkungAufgaabePfaadZuBlatMitGrupePrioNaame.Key)
							{
								continue;
							}

							foreach (var AufgaabeAst in ScritLezteWirkungAufgaabePfaadZuBlatMitGrupePrioNaame.Key)
							{
								var AufgaabeAstAufgaabeParam = AufgaabeAst.AufgaabeParam as AufgaabeParamAndere;

								if (null == AufgaabeAstAufgaabeParam)
								{
									continue;
								}

								var AufgaabeAstAufgaabeParamMenuWurzelGbsObjekt = AufgaabeAstAufgaabeParam.MenuWurzelGbsObjekt;

								if (null == AufgaabeAstAufgaabeParamMenuWurzelGbsObjekt)
								{
									continue;
								}

								var AufgaabeAstAufgaabeParamMenuWurzelGbsObjektIdentNulbar = (Int64?)AufgaabeAstAufgaabeParamMenuWurzelGbsObjekt.Ident;

								if (!AufgaabeAstAufgaabeParamMenuWurzelGbsObjektIdentNulbar.HasValue)
								{
									continue;
								}

								var AufgaabeAstAufgaabeParamMenuWurzelGbsObjektIdent = AufgaabeAstAufgaabeParamMenuWurzelGbsObjektIdentNulbar.Value;

								var AufgaabeAstAufgaabeParamMenuWurzelGbsObjektFläce = AufgaabeAstAufgaabeParamMenuWurzelGbsObjekt.InGbsFläce;

								if (null == AufgaabeAstAufgaabeParamMenuWurzelGbsObjektFläce)
								{
									continue;
								}

								var AufgaabeAstAufgaabeParamMenuWurzelGbsObjektFläceVergröösertFürTestÜberscnaidung =
									AufgaabeAstAufgaabeParamMenuWurzelGbsObjektFläce.Vergröösert(40, 10);

								var ScnapscusAufgaabeMenuWurzelTarget = AufgaabeAstAufgaabeParamMenuWurzelGbsObjekt as ShipUiTarget;
								var ScnapscusAufgaabeMenuWurzelOverviewRow =
									(GbsElement)(AufgaabeAstAufgaabeParamMenuWurzelGbsObjekt as VonSensor.OverviewZaile);

								bool AnnaameMenuPasendZuAusAufgaabeMenuWurzel = true;

								var ScnapscusLezteMenuWurzelGbsObjektTarget =
									AusScnapscusAuswertungZuusctand.TargetEnthaltendGbsAstBerecne(AufgaabeAstAufgaabeParamMenuWurzelGbsObjekt);

								var ScnapscusLezteMenuWurzelGbsObjektOverviewRow = (null == ScnapscusWindowOverview) ? null :
									ScnapscusWindowOverview.OverviewRowEnthaltendGbsAstBerecne(AufgaabeAstAufgaabeParamMenuWurzelGbsObjekt);

								{
									/*
									 * 2014.04.22	Beobactung Probleem:
									 * In vorherige Scrit für OverviewZaile geöfnete Menu werd waiterverwendet als Menu welces für ain in Inventory angezaigte Objekt ersctelt werde solte.
									 * In Folge werd der falsce Cargo Container Abandoned.
									 * 
									 * Daher Ainfüürung zuusäzlice Bedingung:
									 * Menu früheste Fläce mus überscnaide mit MenuWurzelGbsObjektAbbild.Fläce
									 * */

									//	Bedingung: Fläce von ScnapscusAufgaabeMenuWurzelTarget und Menu müsen sic scnaide

									if (OrtogoonInt.Scnitfläce(
										AufgaabeAstAufgaabeParamMenuWurzelGbsObjektFläceVergröösertFürTestÜberscnaidung,
										ScnapscusListeMenuFrüüheste.InGbsFläce).IsLeer)
									{
										AnnaameMenuPasendZuAusAufgaabeMenuWurzel = false;
									}
								}

								{
									/*
									 * 2014.04.22	Beobactung Probleem:
									 * In vorherige Scrit für OverviewZaile geöfnete Menu werd waiterverwendet als Menu welces für ain in Inventory angezaigte Objekt ersctelt werde solte.
									 * In Folge werd der falsce Cargo Container Abandoned.
									 * 
									 * Daher 2014.04.29 Ainfüürung zuusäzlice Bedingung:
									 * Fals MenuWurzelGbsObjekt in aine Window enthalte isc mus diises das vorderste Window sain.
									 * */

									if (null == GbsBaum ||
										null == MengeWindow)
									{
										AnnaameMenuPasendZuAusAufgaabeMenuWurzel = false;
									}
									else
									{
										SictGbsWindowZuusctand MenuWurzelGbsObjektWindow = null;

										foreach (var KandidaatGbsWindow in MengeWindow)
										{
											var GbsWindowHerkunftAdrese = KandidaatGbsWindow.GbsAstHerkunftAdrese;

											if (!GbsWindowHerkunftAdrese.HasValue)
											{
												continue;
											}

											var GbsWindowAst = GbsBaum.SuuceFlacMengeGbsAstFrühesteMitIdent(GbsWindowHerkunftAdrese.Value);

											if (null == GbsWindowAst)
											{
												continue;
											}

											if (null != GbsWindowAst.SuuceFlacMengeGbsAstFrühesteMitIdent(AufgaabeAstAufgaabeParamMenuWurzelGbsObjektIdent))
											{
												MenuWurzelGbsObjektWindow = KandidaatGbsWindow;
											}
										}

										if (null != MenuWurzelGbsObjektWindow)
										{
											/*
											2015.09.01
											Änderung InGbsBaumAstIndex: Übernaame von noie Sensor InTreeIndex: Element occludes other Elements with lower Value.
																	if (MengeWindow.Any((GbsWindow) => GbsWindow.ZIndex.HasValue && GbsWindow.ZIndex < (MenuWurzelGbsObjektWindow.ZIndex ?? int.MaxValue)))
																	*/
											if (MengeWindow.Any((GbsWindow) => GbsWindow.ZIndex.HasValue && GbsWindow.ZIndex > (MenuWurzelGbsObjektWindow.ZIndex ?? int.MaxValue)))
											{
												//	Ain anderes Window befindet sic waiter vorne als das Window welces MenuWurzelGbsObjekt enthalt.
												AnnaameMenuPasendZuAusAufgaabeMenuWurzel = false;
											}
										}
									}
								}

								if (null != ScnapscusAufgaabeMenuWurzelTarget)
								{
									if (null == ScnapscusLezteMenuWurzelGbsObjektTarget)
									{
										AnnaameMenuPasendZuAusAufgaabeMenuWurzel = false;
									}
									else
									{
										if (null == ScnapscusAufgaabeMenuWurzelTarget.InGbsFläce ||
											null == ScnapscusLezteMenuWurzelGbsObjektTarget.InGbsFläce)
										{
											AnnaameMenuPasendZuAusAufgaabeMenuWurzel = false;
										}
										else
										{
											var BeweegungSctreke =
												ScnapscusLezteMenuWurzelGbsObjektTarget.InGbsFläce.ZentrumLaage -
												ScnapscusAufgaabeMenuWurzelTarget.InGbsFläce.ZentrumLaage;

											if (!(BeweegungSctreke.Betraag < 4))
											{
												//	Target solte zwisce deen zwai Scnapscus nit beweegt haabe.
												AnnaameMenuPasendZuAusAufgaabeMenuWurzel = false;
											}
										}

										var KandidaatMenuEntryIndikatorTarget = ScnapscusListeMenuFrüühesteListeEntry.MenuEntryTargetUnLock();

										if (null == KandidaatMenuEntryIndikatorTarget)
										{
											AnnaameMenuPasendZuAusAufgaabeMenuWurzel = false;
										}
									}
								}

								if (null != ScnapscusAufgaabeMenuWurzelOverviewRow)
								{
									if (null == ScnapscusLezteMenuWurzelGbsObjektOverviewRow)
									{
										AnnaameMenuPasendZuAusAufgaabeMenuWurzel = false;
									}
									else
									{
										if (!(ObjektMitIdentInt64.Identisc(ScnapscusLezteMenuWurzelGbsObjektOverviewRow, ScnapscusWindowOverviewZaileInputFookusExklusiiv)))
										{
											AnnaameMenuPasendZuAusAufgaabeMenuWurzel = false;
										}
									}
								}

								if (!AnnaameMenuPasendZuAusAufgaabeMenuWurzel)
								{
									continue;
								}

								MenuWurzelAnnaameInitiaal = AufgaabeAstAufgaabeParamMenuWurzelGbsObjekt;
							}
						}
					}

					Action MenuKaskaadeKonstrukt = delegate ()
					{
						MenuKaskaadeLezte = new SictGbsMenuKaskaadeZuusctand(
							ZaitMili,
							ScritLezteWirkungMengeAufgaabePfaadZuBlatMitGrupePrioNaame,
							MenuWurzelAnnaameInitiaal,
							ScnapscusListeMenu);
						ListeMenuKaskaade.Enqueue(MenuKaskaadeLezte);
					};

					if (null == MenuKaskaadeLezte)
					{
						/*
						 * 2015.01.16
						 * 
						MenuKaskaadeLezte = new SictGbsMenuKaskaadeZuusctand(ZaitMili, MenuWurzelAnnaameInitiaal, ScnapscusListeMenu);
						ListeMenuKaskaade.Enqueue(MenuKaskaadeLezte);
						 * */
						MenuKaskaadeKonstrukt();
					}
					else
					{
						if (!MenuKaskaadeLezte.AingangScnapscus(ZaitMili, ScnapscusListeMenu))
						{
							/*
							 * 2015.01.16
							 * 
							MenuKaskaadeLezte = new SictGbsMenuKaskaadeZuusctand(ZaitMili, MenuWurzelAnnaameInitiaal, ScnapscusListeMenu);
							ListeMenuKaskaade.Enqueue(MenuKaskaadeLezte);
							 * */
							MenuKaskaadeKonstrukt();
						}
					}
				}

				ListeMenuKaskaade.ListeKürzeBegin(30);

				var ListeMenu =
					(null == MenuKaskaadeLezte) ? null :
					MenuKaskaadeLezte.ListeMenu;

				var AusButtonListSurroundingsMenu =
					Bib3.Extension.FirstOrDefaultNullable(
					ListeMenu, ButtonListSurroundingsMenuPrädikaatTailSctaatisc);

				if (null != AusButtonListSurroundingsMenu)
				{
					if (1 < AusButtonListSurroundingsMenu.ListeScnapscusZuZaitAingangBisherAnzaal)
					{
						//	Übernaame ersct bai meer als ain Scnapscus da eventuel in früühescte Scnapscus noc nit ale Entry enthalte
						AusButtonListSurroundingsMenuLezteMitBeginZait = AusButtonListSurroundingsMenu.SictWertMitZaitBerecneFürBeginZait<SictGbsMenuZuusctand>();
					}
				}
			}

		}
	}
}
