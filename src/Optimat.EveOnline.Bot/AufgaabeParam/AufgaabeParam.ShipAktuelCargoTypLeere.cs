using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
//using Optimat.EveOnline.AuswertGbs;
using Optimat.ScpezEveOnln;


namespace Optimat.EveOnline.Anwendung
{
	[JsonObject(MemberSerialization.OptIn)]
	public class AufgaabeParamShipAktuelCargoLeereTyp : SictAufgaabeParam
	{
		[JsonProperty]
		readonly public SictShipCargoTypSictEnum ShipAktuelCargoLeereTyp;

		public AufgaabeParamShipAktuelCargoLeereTyp()
		{
		}

		public AufgaabeParamShipAktuelCargoLeereTyp(
			SictShipCargoTypSictEnum ShipAktuelCargoLeereTyp)
		{
			this.ShipAktuelCargoLeereTyp = ShipAktuelCargoLeereTyp;
		}

		override	public SictAufgaabeParamZerleegungErgeebnis Zerleege(
			ISictAutomatZuusctand AutomaatZuusctand,
			SictAufgaabeKombiZuusctand KombiZuusctand)
		{
			return ZerleegeShipAktuelOpenCargoLeereTyp(AutomaatZuusctand, ShipAktuelCargoLeereTyp);
		}

		override public IEnumerable<string> ZwekListeKomponenteAusParamBerecne()
		{
			return	new	string[]{	"Ship.Empty Cargo[" + ShipAktuelCargoLeereTyp.ToString() + "]"};
		}

		override public bool HinraicendGlaicwertigFürFortsazScpez(
			SictAufgaabeParam Andere)
		{
			var AndereScpez = Andere as AufgaabeParamShipAktuelCargoLeereTyp;

			if (null == AndereScpez)
			{
				return false;
			}

			return ShipAktuelCargoLeereTyp == AndereScpez.ShipAktuelCargoLeereTyp;
		}

		public SictAufgaabeParamZerleegungErgeebnis ZerleegeShipAktuelOpenCargoLeereTyp(
			ISictAutomatZuusctand AutomaatZuusctand,
			SictShipCargoTypSictEnum CargoTyp)
		{
			VonSensor.WindowInventoryPrimary ErgeebnisWindowShipInventory = null;
			VonSensor.Inventory ErgeebnisShipInventory = null;

			return ZerleegeShipAktuelOpenCargoLeereTyp(
				AutomaatZuusctand,
				CargoTyp,
				out	ErgeebnisWindowShipInventory,
				out	ErgeebnisShipInventory);
		}

		public SictAufgaabeParamZerleegungErgeebnis ZerleegeShipAktuelOpenCargoLeereTyp(
			ISictAutomatZuusctand AutomaatZuusctand,
			SictShipCargoTypSictEnum CargoTyp,
			out	VonSensor.WindowInventoryPrimary ErgeebnisWindowShipInventory,
			out	VonSensor.Inventory ErgeebnisShipInventory)
		{
			ErgeebnisWindowShipInventory = null;
			ErgeebnisShipInventory = null;

			var AufgaabeParamZerleegungErgeebnis = new SictAufgaabeParamZerleegungErgeebnis();

			if (null == AutomaatZuusctand)
			{
				return AufgaabeParamZerleegungErgeebnis;
			}

			var NuzerZaitMili = AutomaatZuusctand.NuzerZaitMili;

			var TailInventoryAuswaalZerleegungErgeebnis =
				AufgaabeParamShipAktuelOpenInventoryCargoTyp.ZerleegeShipAktuelOpenInventoryCargoTyp(
				AutomaatZuusctand,
				CargoTyp,
				out	ErgeebnisWindowShipInventory,
				out	ErgeebnisShipInventory);

			AufgaabeParamZerleegungErgeebnis.FüügeAn(TailInventoryAuswaalZerleegungErgeebnis);

			if (null == ErgeebnisShipInventory)
			{
				AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAus();

				return AufgaabeParamZerleegungErgeebnis;
			}

			if (!Bib3.Extension.NullOderLeer(TailInventoryAuswaalZerleegungErgeebnis.ListeKomponenteAufgaabeParam))
			{
				return AufgaabeParamZerleegungErgeebnis;
			}

			if (true == ErgeebnisShipInventory.SictwaiseScaintGeseztAufListNict)
			{
				//	!!!!	Meldung Warnung

				AufgaabeParamZerleegungErgeebnis.FüügeAn(
					AufgaabeParamAndere.KonstruktInventorySezeSictTypAufList(ErgeebnisWindowShipInventory),
					false);
			}
			else
			{
				var InventoryLinxTreeListeEntry = ErgeebnisWindowShipInventory.LinxTreeListeEntry;

				if (null != InventoryLinxTreeListeEntry)
				{
					var LinxEntryItemHangar =
						InventoryLinxTreeListeEntry
						.FirstOrDefault((Kandidaat) => Regex.Match(Kandidaat.LabelText ?? "", "item hangar", RegexOptions.IgnoreCase).Success);

					var AuswaalReczListeItem = ErgeebnisShipInventory.ListeItem;

					if (null != AuswaalReczListeItem &&
						null != LinxEntryItemHangar)
					{
						var AuswaalReczListeItemFrühesteDrai = AuswaalReczListeItem.Take(3).ToArray();

						if (0 < AuswaalReczListeItemFrühesteDrai.Length)
						{
							//	zu klikende Item mit etwas zuufal auswääle
							var AuswaalItem =
								AuswaalReczListeItemFrühesteDrai.ElementAtOrDefault((int)((NuzerZaitMili / 100) % AuswaalReczListeItemFrühesteDrai.Length));

							/*
							 * 2014.03.28
							 * 
							 * Tail select all vorersct sctilgeleegt
							 * 
							if (null != WindowInventoryZuusctand)
							{
								if ((!VersuucMenuEntryKlikLezteZait.HasValue ||
									VersuucMenuEntryKlikLezteZait < WindowInventoryZuusctand.BeginZait) &&
									3 < AuswaalReczListeItem.Length)
								{
									//	Saitdeem WindowInventory ersctelt wurde, wurde noc kain Versuuc unternome ain MenuEntry zu aktiviire.

									//	Versuuce ale Item auszuwääle

									var AnforderungGbsMenu =
										new SictAnforderungMenuBescraibung(
											AuswaalItem,
											AnforderungLeereCargoWindowInventory,
											true,
											new SictAnforderungMenuKaskaadeAstBedingung[]{
												new	SictAnforderungMenuKaskaadeAstBedingung("select all"),});

									return new SictWirkungNaacGbsUndShipSlot(new string[] { ZwekBeginKomponente, "select all items" }, AnforderungGbsMenu);
								}
							}
							 * *
							 * */

							AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.KonstruktInventoryItemTransport(
								new SictInventoryItemTransport(
									ErgeebnisWindowShipInventory,
									LinxEntryItemHangar,
									new VonSensor.InventoryItem[] { AuswaalItem })));

							if (AuswaalReczListeItem.Length <= 1)
							{
								AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändigSezeAin();
							}
						}
					}
				}
			}

			return AufgaabeParamZerleegungErgeebnis;
		}
	}
}
