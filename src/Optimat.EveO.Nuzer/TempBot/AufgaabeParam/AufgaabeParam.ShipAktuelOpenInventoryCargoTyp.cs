using System;
using Bib3;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Optimat.ScpezEveOnln;
//using Optimat.EveOnline.AuswertGbs;


namespace Optimat.EveOnline.Anwendung
{
	[JsonObject(MemberSerialization.OptIn)]
	public class AufgaabeParamShipAktuelOpenInventoryCargoTyp : SictAufgaabeParam
	{
		[JsonProperty]
		readonly public SictShipCargoTypSictEnum ShipAktuelOpenInventoryCargoTyp;

		public AufgaabeParamShipAktuelOpenInventoryCargoTyp()
		{
		}

		public AufgaabeParamShipAktuelOpenInventoryCargoTyp(
			SictShipCargoTypSictEnum ShipAktuelOpenInventoryCargoTyp)
		{
			this.ShipAktuelOpenInventoryCargoTyp = ShipAktuelOpenInventoryCargoTyp;
		}

		override public SictAufgaabeParamZerleegungErgeebnis Zerleege(
			ISictAutomatZuusctand AutomaatZuusctand,
			SictAufgaabeKombiZuusctand KombiZuusctand)
		{
			return ZerleegeShipAktuelOpenInventoryCargoTyp(AutomaatZuusctand, ShipAktuelOpenInventoryCargoTyp);
		}

		override public IEnumerable<string> ZwekListeKomponenteAusParamBerecne()
		{
			return new string[] { "Ship.Open Inventory[" + ShipAktuelOpenInventoryCargoTyp.ToString() + "]" };
		}

		override public bool HinraicendGlaicwertigFürFortsazScpez(
			SictAufgaabeParam Andere)
		{
			var AndereScpez = Andere as AufgaabeParamShipAktuelOpenInventoryCargoTyp;

			if (null == AndereScpez)
			{
				return false;
			}

			return ShipAktuelOpenInventoryCargoTyp == AndereScpez.ShipAktuelOpenInventoryCargoTyp;
		}

		static public SictAufgaabeParamZerleegungErgeebnis ZerleegeShipAktuelOpenInventoryCargoTyp(
			ISictAutomatZuusctand AutomaatZuusctand,
			SictShipCargoTypSictEnum CargoTyp)
		{
			VonSensor.WindowInventoryPrimary ErgeebnisWindowShipInventory;
			VonSensor.Inventory ErgeebnisShipInventory;

			return ZerleegeShipAktuelOpenInventoryCargoTyp(
				AutomaatZuusctand,
				CargoTyp,
				out ErgeebnisWindowShipInventory,
				out ErgeebnisShipInventory);
		}

		static public SictAufgaabeParamZerleegungErgeebnis ZerleegeShipAktuelOpenInventoryCargoTyp(
			ISictAutomatZuusctand AutomaatZuusctand,
			SictShipCargoTypSictEnum CargoTyp,
			out VonSensor.WindowInventoryPrimary ErgeebnisWindowShipInventory,
			out VonSensor.Inventory ErgeebnisShipInventory)
		{
			bool ZerleegungVolsctändig = true;
			var InternMengeAufgaabeKomponenteParam = new List<SictAufgaabeParam>();

			ErgeebnisWindowShipInventory = null;
			ErgeebnisShipInventory = null;

			VonSensor.WindowInventoryPrimary ScnapscusShipWindowInventory = null;

			var Gbs = AutomaatZuusctand.Gbs();

			var AusScnapscusAuswertungZuusctand = AutomaatZuusctand.ListeScnapscusLezteAuswertungErgeebnisNaacSimu;

			var AusScnapcusMengeWindowInventory =
				(null == AusScnapscusAuswertungZuusctand) ? null : AusScnapscusAuswertungZuusctand.MengeWindowInventory;

			if (null != AusScnapcusMengeWindowInventory)
			{
				if (1 == AusScnapcusMengeWindowInventory.Length)
				{
					var KandidaatAnforderungLeereCargoWindowInventory = AusScnapcusMengeWindowInventory.FirstOrDefault();

					if (null != KandidaatAnforderungLeereCargoWindowInventory)
					{
						if (null != KandidaatAnforderungLeereCargoWindowInventory.LinxTreeEntryActiveShip)
						{
							ScnapscusShipWindowInventory = KandidaatAnforderungLeereCargoWindowInventory;
						}
					}
				}
			}

			var ScnapscusShipWindowInventoryLinxTreeEntryActiveShip =
				(null == ScnapscusShipWindowInventory) ? null : ScnapscusShipWindowInventory.LinxTreeEntryActiveShip;

			if (null == ScnapscusShipWindowInventory)
			{
				ZerleegungVolsctändig = false;

				var NeocomButtonInventory = AusScnapscusAuswertungZuusctand.NeocomButtonInventory;

				if (null == NeocomButtonInventory)
				{
				}
				else
				{
					InternMengeAufgaabeKomponenteParam.Add(AufgaabeParamAndere.KonstruktMausPfaad(
						SictAufgaabeParamMausPfaad.KonstruktMausKlikLinx(NeocomButtonInventory)));
				}
			}
			else
			{
				var WindowInventoryZuusctand =
					(null == Gbs) ? null :
					Gbs.ZuHerkunftAdreseWindow(ScnapscusShipWindowInventory.Ident);

				{
					//	2015.09.02	Erwaiterung:

					var ScnapscusWindowInventoryLinxTreeListeEntryNitBenöötigt =
						ScnapscusShipWindowInventory?.LinxTreeListeEntry
						?.Where(Kandidaat => !(Kandidaat == ScnapscusShipWindowInventory.LinxTreeEntryActiveShip))
						?.ToArray();

					foreach (var LinxTreeEntryNitBenöötigt in ScnapscusWindowInventoryLinxTreeListeEntryNitBenöötigt)
					{
						if (!(0 < LinxTreeEntryNitBenöötigt.MengeChild.CountNullable()))
						{
							continue;
						}

						var LinxTreeEntryNitBenöötigtExpandCollapseToggleFläce = LinxTreeEntryNitBenöötigt.ExpandCollapseToggleFläce;

						if (null == LinxTreeEntryNitBenöötigtExpandCollapseToggleFläce)
						{
						}
						else
						{
							InternMengeAufgaabeKomponenteParam.Add(
								SictAufgaabeParam.KonstruktAufgaabeParam(
								AufgaabeParamAndere.KonstruktMausPfaad(SictAufgaabeParamMausPfaad.KonstruktMausKlikLinx(LinxTreeEntryNitBenöötigtExpandCollapseToggleFläce)),
								"In Inventory collapse unnecessary branch"));
						}
					}
				}

				if (null == ScnapscusShipWindowInventoryLinxTreeEntryActiveShip)
				{
				}
				else
				{
					var ZuCargoTypTreeEntry =
						Optimat.EveOnline.Extension.TreeEntryBerecneAusCargoTyp(
						ScnapscusShipWindowInventoryLinxTreeEntryActiveShip,
						CargoTyp);

					var ZuAuswaalReczMengeKandidaatLinxTreeEntry = ScnapscusShipWindowInventory.ZuAuswaalReczMengeKandidaatLinxTreeEntry;

					if (null == ZuCargoTypTreeEntry)
					{
						//	Sicersctele das AnforderungLeereCargoWindowInventoryLinxTreeEntryActiveShip expanded.

						if (0 < Bib3.Extension.CountNullable(ScnapscusShipWindowInventoryLinxTreeEntryActiveShip.MengeChild))
						{
							//	ist beraits Expanded.

							InternMengeAufgaabeKomponenteParam.Add(AufgaabeParamAndere.KonstruktNaacNuzerMeldungZuEveOnline(
								SictNaacNuzerMeldungZuEveOnline.ErrorGenerel(-1, new SictNaacNuzerMeldungZuEveOnlineCause(
									ShipCargoMissingTyp: CargoTyp))));
						}
						else
						{
							var ExpandCollapseToggleFläce = ScnapscusShipWindowInventoryLinxTreeEntryActiveShip.ExpandCollapseToggleFläce;

							if (null == ExpandCollapseToggleFläce)
							{
							}
							else
							{
								InternMengeAufgaabeKomponenteParam.Add(AufgaabeParamAndere.KonstruktMausPfaad(
									SictAufgaabeParamMausPfaad.KonstruktMausKlikLinx(ExpandCollapseToggleFläce)));
							}
						}
					}
					else
					{
						var ZuAuswaalReczLinxTreeEntry = ZuAuswaalReczMengeKandidaatLinxTreeEntry.FirstOrDefaultNullable();
						var AuswaalReczInventory = ScnapscusShipWindowInventory.AuswaalReczInventory;

						if (1 == Bib3.Extension.CountNullable(ZuAuswaalReczMengeKandidaatLinxTreeEntry) &&
							ZuCargoTypTreeEntry == ZuAuswaalReczLinxTreeEntry)
						{
							if (null == AuswaalReczInventory)
							{
								InternMengeAufgaabeKomponenteParam.Add(AufgaabeParamAndere.KonstruktMausPfaad(
								SictAufgaabeParamMausPfaad.KonstruktMausKlikLinx(ScnapscusShipWindowInventoryLinxTreeEntryActiveShip.TopContLabel)));
							}
							else
							{
								//	Erfolg.

								ErgeebnisWindowShipInventory = ScnapscusShipWindowInventory;
								ErgeebnisShipInventory = AuswaalReczInventory;
							}
						}
						else
						{
							InternMengeAufgaabeKomponenteParam.Add(AufgaabeParamAndere.KonstruktMausPfaad(
								SictAufgaabeParamMausPfaad.KonstruktMausKlikLinx(ZuCargoTypTreeEntry.TopContLabel)));
						}
					}
				}
			}

			return new SictAufgaabeParamZerleegungErgeebnis(InternMengeAufgaabeKomponenteParam, ZerleegungVolsctändig);
		}
	}
}
