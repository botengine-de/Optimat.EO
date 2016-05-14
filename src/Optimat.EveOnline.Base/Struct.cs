using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimat.EveOnline
{
	public enum SictShipCargoTypSictEnum
	{
		Kain = 0,
		General = 1,
		DroneBay = 3,
		OreHold = 7,
	}

	public enum SictMissionObjectiveObjectiveElementTyp
	{
		Kain = 0,
		Location = 1,
		LocationDropOff = 2,
		LocationPickUp = 3,
		Item = 4,
		Cargo = 5,
	}

	public enum SictOverviewObjektGrupeEnum
	{
		Kain = 0,
		Sun = 10,
		Planet,
		Moon,
		AsteroidBelt,
		Station,
		Stargate,
		Asteroid = 30,
		Rat = 40,
		Wreck = 50,
		CargoContainer = 60,
		/// <summary>
		/// 2014.00.00 Vorkome Bsp:	LVL3 Cargo Delivery BloodRaiders ("warehouse")
		/// </summary>
		SpawnContainer,
		AccelerationGate = 70,
		LargeCollidableStructure = 80,
		DeadspaceStructure = 90,
	}

	/// <summary>
	/// 2014.10.00	Bsp aus ShipUI.Timer:
	/// "Centum Execrator<br>Miscellaneous"
	/// </summary>
	public enum SictEWarTypeEnum
	{
		Kain,
		Jam,
		TrackingDisrupt,
		RemoteSensorDamp,
		WarpScramble,
		Webify,
		EnergyNeut,
		EnergyVampire,
		TargetPaint,
		Miscellaneous,
	}

	public enum SictZuInRaumObjektManööverTypEnum
	{
		Kain = 0,
		Stop = 10,
		Approach = 11,
		Orbit = 13,
		KeepAtRange = 14,
		Warp = 30,
		Jump = 31,
		Dock = 40,
	}

	public enum SictAgentLevelOderAnySictEnum
	{
		Any = 0,
		I = 1,
		II = 2,
		III = 3,
		IV = 4,
		V = 5,
	}

	public enum OverviewPresetTyp
	{
		Kain = 0,
		Default = 1,
		Preset = 3,
	}

	public enum OverviewPresetDefaultTyp
	{
		Kain = 0,
		All = 10,
		General = 30,
		Loot = 40,
		Mining = 50,
		Drones = 60,
		WarpTo = 70,
		PvP = 80,
	}

	public enum SictFactionSictEnum
	{
		Andere = 0,
		CONCORD_Assembly = 100,
		Caldari_State = 300,
		Amarr_Empire = 400,
		Galente_Federation = 500,
		Minmatar_Republic = 600,
		Rogue_Drones = 1010,
		Jove_Empire = 1020,
		Ammatar_Mandate = 1030,
		Khanid_Kingdom = 1040,
		The_Syndicate = 1050,
		Guristas_Pirates = 1060,
		Angel_Cartel = 1070,
		The_Blood_Raider_Covenant = 1080,
		The_InterBus = 1090,
		ORE = 1100,
		Thukker_Tribe = 1110,
		The_Servant_Sisters_of_EVE = 1120,
		Mordus_Legion = 1130,
		Sanshas_Nation = 1140,
		Serpentis = 1150,
		Scions_of_the_Superior_Gene = 1160,
		Mercenaries = 1170,
		EoM = 1180,
	}

}
