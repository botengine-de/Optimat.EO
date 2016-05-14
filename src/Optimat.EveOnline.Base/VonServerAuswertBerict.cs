using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Optimat.EveOnline
{
	public enum SictDamageTypeSictEnum
	{
		Kain = 0,
		EM = 1,
		Therm = 2,
		Kin = 3,
		Exp = 4,
	}

	public enum OreTypSictEnum
	{
		Kain = 0,
		Arkonor = 100,
		Bistot = 110,
		Crokite = 120,
		Dark_Ochre = 130,
		Gneiss = 140,
		Hedbergite = 150,
		Hemorphite = 160,
		Jaspet = 170,
		Kernite = 180,
		Mercoxit = 190,
		Omber = 200,
		Plagioclase = 210,
		Pyroxeres = 220,
		Scordite = 230,
		Spodumain = 240,
		Veldspar = 250,
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictAusGbsLocationInfo
	{
		[JsonProperty]
		readonly public string NearestName;

		[JsonProperty]
		readonly public string SolarSystemName;

		[JsonProperty]
		readonly public string SolarSystemSecurityLevelString;

		[JsonProperty]
		readonly public int? SolarSystemSecurityLevelMili;

		[JsonProperty]
		readonly public string ConstellationName;

		[JsonProperty]
		readonly public string RegionName;

		public SictAusGbsLocationInfo()
		{
		}

		public SictAusGbsLocationInfo(
			string NearestName,
			string SolarSystemName,
			string SolarSystemSecurityLevelString = null,
			int? SolarSystemSecurityLevelMili = null,
			string ConstellationName = null,
			string RegionName = null)
		{
			this.NearestName = NearestName;
			this.SolarSystemName = SolarSystemName;
			this.SolarSystemSecurityLevelString = SolarSystemSecurityLevelString;
			this.SolarSystemSecurityLevelMili = SolarSystemSecurityLevelMili;
			this.ConstellationName = ConstellationName;
			this.RegionName = RegionName;
		}

		override public string ToString()
		{
			return JsonConvert.SerializeObject(this, Formatting.Indented);
		}

	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictAgentIdentSystemStationName : ICloneable
	{
		[JsonProperty]
		public string SystemName;

		[JsonProperty]
		public string StationName;

		[JsonProperty]
		public string AgentName;

		public SictAgentIdentSystemStationName()
		{
		}

		public SictAgentIdentSystemStationName(
			string SystemName,
			string StationName = null,
			string AgentName = null)
		{
			this.SystemName = SystemName;
			this.StationName = StationName;
			this.AgentName = AgentName;
		}

		override public bool Equals(object ZuVerglaice)
		{
			var ZuVerglaiceScpez = ZuVerglaice as SictAgentIdentSystemStationName;

			if (null == ZuVerglaiceScpez)
			{
				return false;
			}

			return
				string.Equals(SystemName, ZuVerglaiceScpez.SystemName, StringComparison.InvariantCultureIgnoreCase) &&
				string.Equals(StationName, ZuVerglaiceScpez.StationName, StringComparison.InvariantCultureIgnoreCase) &&
				string.Equals(AgentName, ZuVerglaiceScpez.AgentName, StringComparison.InvariantCultureIgnoreCase);
		}

		override public int GetHashCode()
		{
			var SystemName = this.SystemName;
			var StationName = this.StationName;
			var AgentName = this.AgentName;

			return ((SystemName ?? "") + (StationName ?? "") + (AgentName ?? "")).GetHashCode();
		}

		public SictAgentIdentSystemStationName Kopii()
		{
			/*
			 * 2015.02.18
			 * 
			var Kopii = Bib3.RefNezDiferenz.Extension.JsonConvertKopii(this);

			return Kopii;
			 * */

			return Bib3.RefNezDiferenz.Extension.ObjektKopiiKonstrukt(this);
		}

		public object Clone()
		{
			return Kopii();
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictTreferpunkteTotalUndRel
	{
		[JsonProperty]
		public int? Maximum;

		[JsonProperty]
		public int? Antail;

		[JsonProperty]
		public int? NormiirtMili;

		public SictTreferpunkteTotalUndRel(
			int? Maximum,
			int? Antail,
			int? NormiirtMili)
		{
			this.Maximum = Maximum;
			this.Antail = Antail;
			this.NormiirtMili = NormiirtMili;
		}

		public SictTreferpunkteTotalUndRel()
		{
		}

		override public string ToString()
		{
			return JsonConvert.SerializeObject(this, Formatting.Indented);
		}

		static public bool O0NormiirtKlainerglaicO1Normiirt(
			SictTreferpunkteTotalUndRel O0,
			SictTreferpunkteTotalUndRel O1)
		{
			if (O0 == O1)
			{
				return true;
			}

			if (null == O0 || null == O1)
			{
				return false;
			}

			return O0.NormiirtMili <= O1.NormiirtMili;
		}

		static public bool HinraicendGlaicwertigFürIdentInOptimatParam(
			SictTreferpunkteTotalUndRel O0,
			SictTreferpunkteTotalUndRel O1)
		{
			if (O0 == O1)
			{
				return true;
			}

			if (null == O0 || null == O1)
			{
				return false;
			}

			return
				O0.Antail == O1.Antail &&
				O0.Maximum == O1.Maximum &&
				O0.NormiirtMili == O1.NormiirtMili;
		}

		static public bool UnglaicNul(SictTreferpunkteTotalUndRel Treferpunkte)
		{
			if (null == Treferpunkte)
			{
				return false;
			}

			return Treferpunkte.NormiirtMili.HasValue;
		}

		static public SictTreferpunkteTotalUndRel FürNormiirtMili(int? NormiirtMili)
		{
			return new SictTreferpunkteTotalUndRel(null, null, NormiirtMili);
		}

		static public SictTreferpunkteTotalUndRel FürNormiirtMiliFalsHasValue(int? NormiirtMili)
		{
			if (!NormiirtMili.HasValue)
			{
				return null;
			}

			return new SictTreferpunkteTotalUndRel(null, null, NormiirtMili);
		}

		static public SictTreferpunkteTotalUndRel FürNormiirtFalsHasValue(float? Normiirt)
		{
			if (!Normiirt.HasValue)
			{
				return null;
			}

			if (float.IsNaN(Normiirt.Value))
			{
				return null;
			}

			if (float.IsInfinity(Normiirt.Value))
			{
				return null;
			}

			return new SictTreferpunkteTotalUndRel(null, null, (int)(Normiirt.Value * 1000));
		}
	}
	/*
	 * 2015.02.24
	 * 
	 * */

	public class ShipHitpointsAndEnergy
	{
		readonly public int? Struct;
		readonly public int? Armor;
		readonly public int? Shield;
		readonly public int? Capacitor;

		public ShipHitpointsAndEnergy()
		{
		}

		public ShipHitpointsAndEnergy(
			int? Struct,
			int? Armor,
			int? Shield,
			int? Capacitor = null)
		{
			this.Struct = Struct;
			this.Armor = Armor;
			this.Shield = Shield;
			this.Capacitor = Capacitor;
		}

		static public bool O0KlainerglaicO1(
			ShipHitpointsAndEnergy O0,
			ShipHitpointsAndEnergy O1)
		{
			if (O0 == O1)
			{
				return true;
			}

			if (null == O0 || null == O1)
			{
				return false;
			}

			return
				O0.Struct <= O1.Struct &&
				O0.Armor <= O1.Armor &&
				O0.Shield <= O1.Shield &&
				O0.Capacitor <= O1.Capacitor;
		}

		static public bool NotNull(ShipHitpointsAndEnergy O)
		{
			if (null == O)
			{
				return false;
			}

			return
				O.Struct.HasValue &&
				O.Armor.HasValue &&
				O.Shield.HasValue &&
				O.Capacitor.HasValue;
		}
	}

	/// <summary>
	/// Unit is m³/1000
	/// </summary>
	public struct CargoCapacityInfo
	{
		/*
		 * 2015.02.24
		 * Vorersct Verwendung von Property sctat readonly modifier da Bib3.RefNezDiferenz bisher kaine readonly Field in Struct bescraibt.
		 * */
		public Int64? MaxMili
		{
			set;
			get;
		}

		public Int64? UsedMili
		{
			set;
			get;
		}

		public CargoCapacityInfo(
			Int64? MaxMili,
			Int64? UsedMili)
			:
			this()
		{
			this.MaxMili = MaxMili;
			this.UsedMili = UsedMili;
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class ShipState
	{
		/*
		 * 2015.02.24
		 * 
		[JsonProperty]
		readonly public SictShipTreferpunkteZuusctand Treferpunkte;

		[JsonProperty]
		readonly public SictTreferpunkteTotalUndRel CapacitorCapacity;
		 * */

		[JsonProperty]
		readonly public ShipHitpointsAndEnergy HitpointsRelMili;

		[JsonProperty]
		readonly public Int64? SpeedDurcMeterProSekunde;

		[JsonProperty]
		readonly public bool? Docked;

		[JsonProperty]
		readonly public bool? Docking;

		[JsonProperty]
		readonly public bool? Warping;

		[JsonProperty]
		readonly public bool? Jumping;

		[JsonProperty]
		readonly public bool? Cloaked;

		[JsonProperty]
		readonly public bool? Jammed;

		[JsonProperty]
		readonly public bool? UnDocking;

		[JsonProperty]
		readonly public CargoCapacityInfo CargoHoldCapacity;

		[JsonProperty]
		readonly public CargoCapacityInfo OreHoldCapacity;

		[JsonProperty]
		readonly public CargoCapacityInfo DroneBayCapacity;

		public ShipState()
		{
		}

		public ShipState(
			ShipHitpointsAndEnergy HitpointsRelMili,
			Int64? SpeedDurcMeterProSekunde = null,
			bool? Docked = null,
			bool? Docking = null,
			bool? Warping = null,
			bool? Jumping = null,
			bool? Cloaked = null,
			bool? Jammed = null,
			bool? UnDocking = null,
			CargoCapacityInfo CargoHoldCapacity = default(CargoCapacityInfo),
			CargoCapacityInfo OreHoldCapacity = default(CargoCapacityInfo),
			CargoCapacityInfo DroneBayCapacity = default(CargoCapacityInfo)
			)
		{
			this.HitpointsRelMili = HitpointsRelMili;
			this.SpeedDurcMeterProSekunde = SpeedDurcMeterProSekunde;

			this.Docked = Docked;
			this.Docking = Docking;
			this.Warping = Warping;
			this.Jumping = Jumping;
			this.Cloaked = Cloaked;
			this.Jammed = Jammed;
			this.UnDocking = UnDocking;

			this.CargoHoldCapacity = CargoHoldCapacity;
			this.OreHoldCapacity = OreHoldCapacity;
			this.DroneBayCapacity = DroneBayCapacity;
		}

		public bool HitpointsNotNull()
		{
			return ShipHitpointsAndEnergy.NotNull(HitpointsRelMili);
		}

		/*
		 * 2015.02.24
		 * 
		public bool TreferpunkteUnglaicNul()
		{
			return SictShipTreferpunkteZuusctand.UnglaicNul(HitpointsRelMili);
		}

		/// <summary>
		/// sezt ShipZuusctand aus Taile der Elemente in <paramref name="ListePrioShipZuusctand"/> zusame so das Taile welce null sind durc
		/// naacfolgende Elemente in der Liste ersezt werden. früühere Elemente in der Liste hän vorrang.
		/// </summary>
		/// <param name="ListePrioShipZuusctand"></param>
		/// <returns></returns>
		static public ShipState KombiAusElementTailUnglaicNul(
			IEnumerable<ShipState> ListePrioShipZuusctand)
		{
			if (null == ListePrioShipZuusctand)
			{
				return null;
			}

			SictTreferpunkteTotalUndRel KombiTreferpunkteStruct = null;
			SictTreferpunkteTotalUndRel KombiTreferpunkteArmor = null;
			SictTreferpunkteTotalUndRel KombiTreferpunkteShield = null;
			SictTreferpunkteTotalUndRel KombiCapacitorCapacity = null;
			Int64? KombiSpeedDurcMeterProSekunde = null;
			bool? KombiDocked = null;
			bool? KombiDocking = null;
			bool? KombiWarping = null;
			bool? KombiJumping = null;
			bool? KombiCloaked = null;
			bool? KombiJammed = null;

			foreach (var PrioShipZuusctand in ListePrioShipZuusctand)
			{
				if (null == PrioShipZuusctand)
				{
					continue;
				}

				var PrioShipZuusctandTreferpunkte = PrioShipZuusctand.Treferpunkte;

				if (null != PrioShipZuusctandTreferpunkte)
				{
					var PrioShipZuusctandTreferpunkteStruct = PrioShipZuusctandTreferpunkte.Struct;
					var PrioShipZuusctandTreferpunkteArmor = PrioShipZuusctandTreferpunkte.Armor;
					var PrioShipZuusctandTreferpunkteShield = PrioShipZuusctandTreferpunkte.Shield;

					if (null == KombiTreferpunkteStruct)
					{
						KombiTreferpunkteStruct = PrioShipZuusctandTreferpunkteStruct;
					}
					if (null == KombiTreferpunkteArmor)
					{
						KombiTreferpunkteArmor = PrioShipZuusctandTreferpunkteArmor;
					}
					if (null == KombiTreferpunkteShield)
					{
						KombiTreferpunkteShield = PrioShipZuusctandTreferpunkteShield;
					}
				}

				if (null == KombiCapacitorCapacity)
				{
					KombiCapacitorCapacity = PrioShipZuusctand.CapacitorCapacity;
				}

				if (!KombiSpeedDurcMeterProSekunde.HasValue)
				{
					KombiSpeedDurcMeterProSekunde = PrioShipZuusctand.SpeedDurcMeterProSekunde;
				}

				if (!KombiDocked.HasValue)
				{
					KombiDocked = PrioShipZuusctand.Docked;
				}

				if (!KombiDocking.HasValue)
				{
					KombiDocking = PrioShipZuusctand.Docking;
				}

				if (!KombiWarping.HasValue)
				{
					KombiWarping = PrioShipZuusctand.Warping;
				}

				if (!KombiJumping.HasValue)
				{
					KombiJumping = PrioShipZuusctand.Jumping;
				}

				if (!KombiCloaked.HasValue)
				{
					KombiCloaked = PrioShipZuusctand.Cloaked;
				}

				if (!KombiJammed.HasValue)
				{
					KombiJammed = PrioShipZuusctand.Jammed;
				}
			}

			var KombiShipZuusctandTreferpunkte = new SictShipTreferpunkteZuusctand(
				KombiTreferpunkteStruct,
				KombiTreferpunkteArmor,
				KombiTreferpunkteShield);

			var KombiShipZuusctand = new ShipState(
				KombiShipZuusctandTreferpunkte,
				KombiCapacitorCapacity,
				KombiSpeedDurcMeterProSekunde,
				KombiDocked,
				KombiDocking,
				KombiWarping,
				KombiJumping,
				KombiCloaked,
				KombiJammed);

			return KombiShipZuusctand;
		}
		 * */

		override public string ToString()
		{
			return JsonConvert.SerializeObject(this, Formatting.Indented);
		}

		static public bool HinraicendGlaicwertigFürIdentInOptimatParam(
			ShipState O0,
			ShipState O1)
		{
			if (O0 == O1)
			{
				return true;
			}

			if (null == O0 || null == O1)
			{
				return false;
			}

			return
				O0.Docked == O1.Docked &&
				O0.Docking == O1.Docking &&
				O0.Warping == O1.Warping &&
				O0.Jumping == O1.Jumping &&
				O0.Cloaked == O1.Cloaked &&
				O0.Jammed == O1.Jammed &&
				O0.SpeedDurcMeterProSekunde == O1.SpeedDurcMeterProSekunde &&
				/*
				 * 2015.02.24
				 * 
				ShipHitpointsAndEnergy.HinraicendGlaicwertigFürIdentInOptimatParam(
				O0.HitpointsRelMili, O1.HitpointsRelMili)
				 * */
				object.Equals(O0.HitpointsRelMili, O1.HitpointsRelMili)
				;
		}
	}

	public enum SictShipTreferpunkteZuusctandKlas1D
	{
		ReparaturNotwendig = 0,
		FluctNotwendig = 1,
		GefectBescteheFraigaabe = 2,

		/// <summary>
		/// Gefect fordert di Treferpunkte so weenig das Distanz zu Geegner nit beactet were mus. So köne troz Gefect glaiczaitig andere Aufgaabe (z.B. Loot) erleedigt were.
		/// </summary>
		BeweegungUnabhängigVonGefectFraigaabe = 3,

		GefectBaitritFraigaabe = 4,
	}

	/*
	 * 2015.02.24
	 * 
	[JsonObject(MemberSerialization.OptIn)]
	public class SictShipTreferpunkteZuusctand
	{
		[JsonProperty]
		readonly public SictTreferpunkteTotalUndRel Shield;

		[JsonProperty]
		readonly public SictTreferpunkteTotalUndRel Armor;

		[JsonProperty]
		readonly public SictTreferpunkteTotalUndRel Struct;

		public SictShipTreferpunkteZuusctand()
		{
		}

		public SictShipTreferpunkteZuusctand(
			SictShipTreferpunkteZuusctand ZuKopiire)
			:
			this(
			(null == ZuKopiire) ? null : ZuKopiire.Struct,
			(null == ZuKopiire) ? null : ZuKopiire.Armor,
			(null == ZuKopiire) ? null : ZuKopiire.Shield)
		{
		}

		public SictShipTreferpunkteZuusctand(
			SictTreferpunkteTotalUndRel Struct,
			SictTreferpunkteTotalUndRel Armor,
			SictTreferpunkteTotalUndRel Shield)
		{
			this.Struct = Struct;
			this.Armor = Armor;
			this.Shield = Shield;
		}

		static public bool HinraicendGlaicwertigFürIdentInOptimatParam(
			SictShipTreferpunkteZuusctand O0,
			SictShipTreferpunkteZuusctand O1)
		{
			if (O0 == O1)
			{
				return true;
			}

			if (null == O0 || null == O1)
			{
				return false;
			}

			return
				SictTreferpunkteTotalUndRel.HinraicendGlaicwertigFürIdentInOptimatParam(O0.Struct, O1.Struct) &&
				SictTreferpunkteTotalUndRel.HinraicendGlaicwertigFürIdentInOptimatParam(O0.Armor, O1.Armor) &&
				SictTreferpunkteTotalUndRel.HinraicendGlaicwertigFürIdentInOptimatParam(O0.Shield, O1.Shield);
		}

		static public SictShipTreferpunkteZuusctand SictShipTreferpunkteZuusctandAusNormiirtMili(
			int? ShieldNormiirtMili,
			int? ArmorNormiirtMili,
			int? StructNormiirtMili)
		{
			return new SictShipTreferpunkteZuusctand(
				SictTreferpunkteTotalUndRel.FürNormiirtMili(ShieldNormiirtMili),
				SictTreferpunkteTotalUndRel.FürNormiirtMili(ArmorNormiirtMili),
				SictTreferpunkteTotalUndRel.FürNormiirtMili(StructNormiirtMili));
		}

		static public bool O0NormiirtKlainerglaicO1Normiirt(
			SictShipTreferpunkteZuusctand O0,
			SictShipTreferpunkteZuusctand O1)
		{
			if (O0 == O1)
			{
				return true;
			}

			if (null == O0 || null == O1)
			{
				return false;
			}

			return
				SictTreferpunkteTotalUndRel.O0NormiirtKlainerglaicO1Normiirt(O0.Shield, O1.Shield) &&
				SictTreferpunkteTotalUndRel.O0NormiirtKlainerglaicO1Normiirt(O0.Armor, O1.Armor) &&
				SictTreferpunkteTotalUndRel.O0NormiirtKlainerglaicO1Normiirt(O0.Struct, O1.Struct);
		}

		static public bool UnglaicNul(SictShipTreferpunkteZuusctand Treferpunkte)
		{
			if (null == Treferpunkte)
			{
				return false;
			}

			return
				SictTreferpunkteTotalUndRel.UnglaicNul(Treferpunkte.Shield) &&
				SictTreferpunkteTotalUndRel.UnglaicNul(Treferpunkte.Armor) &&
				SictTreferpunkteTotalUndRel.UnglaicNul(Treferpunkte.Struct);
		}

		override public string ToString()
		{
			return JsonConvert.SerializeObject(this, Formatting.Indented);
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictShipTreferpunkteUndCapacitorZuusctand : SictShipTreferpunkteZuusctand
	{
		[JsonProperty]
		readonly public SictTreferpunkteTotalUndRel Capacitor;

		public SictShipTreferpunkteUndCapacitorZuusctand()
		{
		}

		public SictShipTreferpunkteUndCapacitorZuusctand(
			SictShipTreferpunkteZuusctand Treferpunkte,
			SictTreferpunkteTotalUndRel Capacitor)
			:
			base(Treferpunkte)
		{
			this.Capacitor = Capacitor;
		}

		public SictShipTreferpunkteUndCapacitorZuusctand(
			SictTreferpunkteTotalUndRel Struct,
			SictTreferpunkteTotalUndRel Armor,
			SictTreferpunkteTotalUndRel Shield,
			SictTreferpunkteTotalUndRel Capacitor)
			:
			base(Struct, Armor, Shield)
		{
			this.Capacitor = Capacitor;
		}
	}
	 * */

	public enum SictShipSlotTyp
	{
		Kain = 0,
		Low = 1,
		Medium = 2,
		High = 3,
	}

	[JsonObject(MemberSerialization.OptIn)]
	public struct SictDamageMitBetraagIntValue
	{
		[JsonConverter(typeof(StringEnumConverter))]
		[JsonProperty]
		public SictDamageTypeSictEnum? DamageType;

		[JsonProperty]
		public int? BetraagInt;

		public SictDamageMitBetraagIntValue(
			SictDamageTypeSictEnum? DamageType,
			int? BetraagInt)
		{
			this.DamageType = DamageType;
			this.BetraagInt = BetraagInt;
		}
	}


}
