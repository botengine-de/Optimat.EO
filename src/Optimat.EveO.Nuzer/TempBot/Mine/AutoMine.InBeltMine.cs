using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Optimat.ScpezEveOnln;
using Bib3;
using Newtonsoft.Json;

namespace Optimat.EveOnline.Anwendung
{
	public partial class SictAutoMine
	{
		static readonly string OverviewDefaultMiningIdent = "Mining";

		public IEnumerable<SictAufgaabeParam> FürMineListeAufgaabeNääxteParamBerecneTailInBeltMine(
			ISictAutomatZuusctand AutomaatZuusctand)
		{
			if (null == AutomaatZuusctand)
			{
				return null;
			}

			var ListeAufgaabeParam = new List<SictAufgaabeParam>();

			var AsteroidLockListeAufgaabeParam = new List<SictAufgaabeParam>();

			var AsteroidMineListeAufgaabeParam = new List<SictAufgaabeParam>();

			var AsteroidMineModuleScalteAinListeAufgaabeParam = new List<SictAufgaabeParam>();

			var FittingUndShipZuusctand = AutomaatZuusctand.FittingUndShipZuusctand;
			var OverviewUndTarget = AutomaatZuusctand.OverviewUndTarget;

			if (null == FittingUndShipZuusctand)
			{
				return null;
			}

			var ShipMengeModule = AutomaatZuusctand.ShipMengeModule();

			var ShipMengeModuleMiner =
				ShipMengeModule
				.WhereNullable((Kandidaat) => Kandidaat.IstMiner ?? false)
				.ToArrayNullable();

			var ShipZuusctand = FittingUndShipZuusctand.ShipZuusctand;

			if (null == ShipZuusctand)
			{
				return null;
			}

			var CharZuusctandWarping = ShipZuusctand.Warping;
			var CharZuusctandDocking = ShipZuusctand.Docking;

			var ListeTargetVerwendet =
				MengeTargetVerwendet
				.OrderByNullable((Kandidaat) => Kandidaat.Key.SictungLezteDistanceScrankeMaxScpezTarget ?? int.MaxValue)
				.OrderByNullable((Kandidaat) => true == Kandidaat.Key.InputFookusTransitioonLezteZiilWert ? -1 : 0)
				.ToArrayNullable();

			var OverViewScrolledToTopLezteAlter = AutomaatZuusctand.OverViewScrolledToTopLezteAlter();
			var WindowOverviewScroll = AutomaatZuusctand.WindowOverviewScnapscusLezteScroll();

			var MengeOverviewObjektVerwendetFraigaabe =
				MengeOverviewObjektVerwendet
				.WhereNullable((Kandidaat) => Kandidaat.Value.OreTypFraigaabe ?? false)
				.ToArrayNullable();

			ListeTargetVerwendet.ForEachNullable((Target) =>
				{
					if (!Target.Value.OreTypFraigaabe ?? false)
					{
						ListeAufgaabeParam.Add(
							SictAufgaabeParam.KonstruktAufgaabeParam(
							AufgaabeParamAndere.KonstruktTargetUnLock(Target.Key),
							"Target Ore Type not suiting Preferences"));
					}
				});

			var AsteroidKeepInRangeListeAufgaabeParam = new List<SictAufgaabeParam>();

			if ((MengeAsteroidInRaicwaiteAnzaalAusraicend ?? false) &&
				(0 < MengeTargetVerwendet.CountNullable((Kandidaat) => Kandidaat.Value.OreTyp.HasValue)))
			{
				AsteroidKeepInRangeListeAufgaabeParam.Add(new AufgaabeParamShipStop());
			}
			else
			{
				if (null != OverviewObjektFraigaabeLockedNictNääxte.Key)
				{
					var TargetOoneAssignedWaitestEntfernte =
						MengeTargetVerwendet
						.WhereNullable((Kandidaat) => !(0 < Kandidaat.Key.ScnapscusMengeAssignedAnzaal()))
						.OrderByDescendingNullable((Kandidaat) => Kandidaat.Key.SictungLezteDistanceScrankeMax() ?? int.MaxValue)
						.FirstOrDefaultNullable();

					if (null != TargetOoneAssignedWaitestEntfernte.Key)
					{
						if (OverviewObjektFraigaabeLockedNictNääxte.Key.DistanceScrankeMaxKombi < TargetOoneAssignedWaitestEntfernte.Key.SictungLezteDistanceScrankeMin() - 1111)
						{
							ListeAufgaabeParam.Add(AufgaabeParamAndere.KonstruktTargetUnLock(TargetOoneAssignedWaitestEntfernte.Key));
						}
					}
				}

				if (!(AutomaatZuusctand.OverViewScrolledToTopLezteAlter() < 13111))
				{
					AsteroidKeepInRangeListeAufgaabeParam.Add(new AufgaabeParamScrollToTop(WindowOverviewScroll));
				}

				if (null != TargetAnzufliigeNääxte)
				{
					AsteroidKeepInRangeListeAufgaabeParam.Add(AufgaabeParamAndere.AufgaabeDistanceAinzusctele(TargetAnzufliigeNääxte, null, 4444));
				}
			}

			ListeAufgaabeParam.AddRange(
				AsteroidKeepInRangeListeAufgaabeParam
				.Select((AufgaabeParam) => SictAufgaabeParam.KonstruktAufgaabeParam(
					AufgaabeParam, "keep Asteroid in Range")));

			if (null == OverviewTabBevorzuugtTitel)
			{
			}
			else
			{
				//	ListeAufgaabeParam.Add(AufgaabeParamAndere.KonstruktOverviewTabAktiviire(OverviewTabBevorzuugtTitel));

				//	Laade Overview Default "Mining" naac Tab.

				ListeAufgaabeParam.Add(new AufgaabeParamOverviewPresetLaade(OverviewPresetTyp.Default, OverviewDefaultMiningIdent, OverviewTabBevorzuugtTitel));
			}

			if (null != ShipMengeModuleMiner)
			{
				foreach (var ModuleMiner in ShipMengeModuleMiner)
				{
					var ModuleMinerTarget = ModuleMiner.TargetAktuel();

					var ModuleMinerTargetInfo =
						MengeTargetVerwendet
						.FirstOrDefaultNullable((Kandidaat) => Kandidaat.Key == ModuleMinerTarget);

					if (ModuleMiner.AktiivBerecne(1))
					{
						string ModuleScalteAusUrsaceSictString = null;

						if (true == AnnaameNaacAbbrucMinerZyyklusOreHoldGefült)
						{
							ModuleScalteAusUrsaceSictString = "Ore Hold Full";
						}

						if (null != ModuleMinerTargetInfo.Key)
						{
							if (ModuleMinerTargetInfo.Value.ErzMengeRestScrankeMin <= 0)
							{
								ModuleScalteAusUrsaceSictString = "Asteroid depleted";
							}
						}

						if (null != ModuleScalteAusUrsaceSictString)
						{
							AsteroidMineListeAufgaabeParam.Add(
								SictAufgaabeParam.KonstruktAufgaabeParam(
								AufgaabeParamAndere.KonstruktModuleScalteAus(ModuleMiner),
								ModuleScalteAusUrsaceSictString));
						}

						continue;
					}

					if (true == DockUndOffloadPrioVorMine)
					{
						continue;
					}

					var ModuleMinerRange = (ModuleMiner.RangeMax ?? ModuleMiner.RangeOptimal);

					if (!ModuleMinerRange.HasValue)
					{
						continue;
					}

					SictTargetZuusctand ZuModuleTargetVerwendet = null;

					if (!(true == CharZuusctandWarping))
					{
						if (null != ListeTargetVerwendet)
						{
							foreach (var Target in ListeTargetVerwendet)
							{
								if (null != ZuModuleTargetVerwendet)
								{
									break;
								}

								if (Target.Key.SictungLezteDistanceScrankeMaxScpezTarget <= TargetDistanceScrankeMax)
								{
									//	Pro Scrit nur ain Module Assigne.
									if (AsteroidMineModuleScalteAinListeAufgaabeParam.Count < 1)
									{
										if (0 < Target.Value.MengeAssignedAnzaal)
										{
											//	zu diisem Target isc beraits mindestens ain Module Assigned.

											if (0 < TargetAssignmentMeerereKarenzDauerRest)
											{
												continue;
											}
										}

										ZuModuleTargetVerwendet = Target.Key;

										if (true == Target.Key.InputFookusTransitioonLezteZiilWert)
										{
											if (0 < TargetAssignmentKarenzDauerRest)
											{
											}
											else
											{
												AsteroidMineModuleScalteAinListeAufgaabeParam.Add(AufgaabeParamAndere.KonstruktModuleScalteAin(ModuleMiner));
											}
										}
										else
										{
											AsteroidMineListeAufgaabeParam.Add(AufgaabeParamAndere.KonstruktTargetInputFookusSeze(Target.Key));
										}
									}
								}
								else
								{
								}
							}
						}

						if (null != ZuModuleTargetVerwendet)
						{
							continue;
						}
					}
				}
			}

			AsteroidMineListeAufgaabeParam.AddRange(AsteroidMineModuleScalteAinListeAufgaabeParam.Take(1));

			if (null != AsteroidZuLockeNääxte)
			{
				AsteroidLockListeAufgaabeParam.Add(new AufgaabeParamLockTarget(AsteroidZuLockeNääxte));
			}

			if (InOverviewSuuceAsteroid ?? false)
			{
				/*
				 * 2014.10.05
				 * 
				if (OverViewScrolledToTopLezteAlter < 13333)
				{
					AsteroidLockListeAufgaabeParam.Add(new AufgaabeParamScrollDown(WindowOverviewScroll));
				}
				else
				{
					AsteroidLockListeAufgaabeParam.Add(new AufgaabeParamScrollToTop(WindowOverviewScroll));
				}
				 * */

				AsteroidLockListeAufgaabeParam.Add(AufgaabeParamAndere.KonstruktInOverviewTabFolgeViewportDurcGrid());
			}

			ListeAufgaabeParam.AddRange(AsteroidMineListeAufgaabeParam);
			ListeAufgaabeParam.AddRange(AsteroidLockListeAufgaabeParam);
			
			return ListeAufgaabeParam;
		}
	}
}
