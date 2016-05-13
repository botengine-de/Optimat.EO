using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Bib3;
using Newtonsoft.Json;
using Optimat.EveOnline.VonSensor;


namespace Optimat.EveOnline.Anwendung
{
	public class SictKonfigMissionTitel
	{
		static public SictMissionTitel MissionTitelPirateInvasion =
			SictMissionTitel.MissionTitelMitEntscaidungFactionStandard("Pirate Invasion");

		static public SictMissionTitel MissionTitelPirateIntrusion =
			SictMissionTitel.MissionTitelMitEntscaidungFactionStandard("Pirate Intrusion");

		static public SictMissionTitel MissionTitelTheBlockade =
			SictMissionTitel.MissionTitelMitEntscaidungFactionStandard("The Blockade");

		static public SictMissionTitel MissionTitelTheAssault =
			SictMissionTitel.MissionTitelMitEntscaidungFactionStandard("The Assault");

		static public SictMissionTitel MissionTitelTheScore =
			SictMissionTitel.MissionTitelMitEntscaidungFactionStandard("The Score");

		static public SictMissionTitel MissionTitelSuccessComesAtAPrice =
			SictMissionTitel.MissionTitelMitEntscaidungFactionStandard("Success Comes At A Price");

		static public SictMissionTitel MissionTitelPirateAggression =
			SictMissionTitel.MissionTitelMitEntscaidungFactionStandard("Pirate Aggression");

		static public SictMissionTitel MissionTitelVengeance =
			SictMissionTitel.MissionTitelMitEntscaidungFactionStandard("Vengeance");

		static public SictMissionTitel MissionTitelTheMissingConvoy =
			SictMissionTitel.MissionTitelMitEntscaidungFactionKonstant("The Missing Convoy", SictFactionSictEnum.Rogue_Drones);

		static public SictMissionTitel MissionTitelTheGoodWord =
			SictMissionTitel.MissionTitelMitEntscaidungFactionKonstant("The Good Word", SictFactionSictEnum.Scions_of_the_Superior_Gene);

		static public SictMissionTitel MissionTitelTheSpaceTelescope =
			SictMissionTitel.MissionTitelMitEntscaidungFactionKonstant("The Space Telescope", SictFactionSictEnum.Guristas_Pirates);

		static public SictMissionTitel MissionTitelWhatComesAroundGoesAround =
			SictMissionTitel.MissionTitelMitEntscaidungFactionKonstant("What Comes Around Goes Around", SictFactionSictEnum.Mercenaries);

		static public SictMissionTitel MissionTitelTheDamselInDistress =
			SictMissionTitel.MissionTitelMitEntscaidungFactionKonstant("The Damsel In Distress", SictFactionSictEnum.Mercenaries);

		static public SictMissionTitel MissionTitelStopTheThief =
			SictMissionTitel.MissionTitelMitEntscaidungFactionKonstant("Stop The Thief", SictFactionSictEnum.Mercenaries);

		static public SictMissionTitel MissionTitelTheGuristasSpies =
			SictMissionTitel.MissionTitelMitEntscaidungFactionKonstant("The Guristas Spies", SictFactionSictEnum.Guristas_Pirates);

		static public SictMissionTitel MissionTitelTheAngelCartelSpies =
			SictMissionTitel.MissionTitelMitEntscaidungFactionKonstant("The Angel Cartel Spies", SictFactionSictEnum.Angel_Cartel);

		static public SictMissionTitel MissionTitelTheBloodRaiderSpies =
			SictMissionTitel.MissionTitelMitEntscaidungFactionKonstant("The Blood Raider Spies", SictFactionSictEnum.The_Blood_Raider_Covenant);

		static public SictMissionTitel MissionTitelTechnologicalSecrets1of3 =
			SictMissionTitel.MissionTitelMitEntscaidungFactionKonstant(Regex.Escape("Technological Secrets (1 of 3)"), SictFactionSictEnum.Thukker_Tribe);

		static public SictMissionTitel MissionTitelSilenceTheInformant =
			SictMissionTitel.MissionTitelMitEntscaidungFactionKonstant(Regex.Escape("Silence The Informant"), SictFactionSictEnum.Mercenaries);

		static public SictMissionTitel MissionTitelGoneBerserk =
			SictMissionTitel.MissionTitelMitEntscaidungFactionKonstant(Regex.Escape("Gone Berserk"), SictFactionSictEnum.EoM);

		static public SictMissionTitel MissionTitelRogueDroneHarassment =
			SictMissionTitel.MissionTitelMitEntscaidungFactionKonstant(Regex.Escape("Rogue Drone Harassment"), SictFactionSictEnum.Rogue_Drones);

		static public SictMissionTitel MissionTitelTheDroneInfestation =
			SictMissionTitel.MissionTitelMitEntscaidungFactionKonstant(Regex.Escape("The Drone Infestation"), SictFactionSictEnum.Rogue_Drones);

		static public SictMissionTitel MissionTitelTheSevensPrisonFacility =
			SictMissionTitel.MissionTitelMitEntscaidungFactionKonstant(Regex.Escape("The Seven's Prison Facility"), SictFactionSictEnum.Mercenaries);

		static public SictMissionTitel MissionTitelTheBlackMarketHub =
			SictMissionTitel.MissionTitelMitEntscaidungFactionKonstant(Regex.Escape("The Black Market Hub"), SictFactionSictEnum.Mercenaries);

		static public SictMissionTitel MissionTitelGuristasExtravaganza =
			SictMissionTitel.MissionTitelMitEntscaidungFactionKonstant(Regex.Escape("Guristas Extravaganza"), SictFactionSictEnum.Guristas_Pirates);

		static public SictMissionTitel MissionTitelAngelExtravaganza =
			SictMissionTitel.MissionTitelMitEntscaidungFactionKonstant(Regex.Escape("Angel Extravaganza"), SictFactionSictEnum.Angel_Cartel);

		static public SictMissionTitel MissionTitelSaveAMansCareer =
			SictMissionTitel.MissionTitelMitEntscaidungFactionKonstant("Save a Man.* Career", SictFactionSictEnum.Rogue_Drones);

		static public SictMissionTitel MissionTitelTheSevensBrothel =
			SictMissionTitel.MissionTitelMitEntscaidungFactionKonstant("The Seven.* Brothel", SictFactionSictEnum.Mercenaries);

		static public SictMissionTitel MissionTitelFurrierFiasco =
			SictMissionTitel.MissionTitelMitEntscaidungFactionKonstant(Regex.Escape("Furrier Fiasco"), SictFactionSictEnum.Serpentis);

		static public SictMissionTitel MissionTitelAlluringEmanations =
			SictMissionTitel.MissionTitelMitEntscaidungFactionKonstant("Alluring Emanations", SictFactionSictEnum.Rogue_Drones);

		static public SictMissionTitel MissionTitelMysteriousSightings_1_of_4 =
			SictMissionTitel.MissionTitelMitEntscaidungFactionKonstant(Regex.Escape("Mysterious Sightings (1 of 4)"), SictFactionSictEnum.Serpentis);

		static public SictMissionTitel MissionTitelAfterTheSeven_1_of_5 =
			SictMissionTitel.MissionTitelMitEntscaidungFactionKonstant(Regex.Escape("After The Seven (1 of 5)"), SictFactionSictEnum.Serpentis);

		static public SictMissionTitel MissionTitelAfterTheSeven_2_of_5 =
			SictMissionTitel.MissionTitelMitEntscaidungFactionKonstant(Regex.Escape("After The Seven (2 of 5)"), SictFactionSictEnum.Mercenaries);

		static public SictMissionTitel MissionTitelAfterTheSeven_3_of_5 =
			SictMissionTitel.MissionTitelMitEntscaidungFactionKonstant(Regex.Escape("After The Seven (3 of 5)"), SictFactionSictEnum.Mercenaries);

		static public SictMissionTitel MissionTitelTheSerpentisSpies =
			SictMissionTitel.MissionTitelMitEntscaidungFactionKonstant("The Serpentis Spies", SictFactionSictEnum.Serpentis);

		static public SictMissionTitel MissionTitelTheDisgruntledEmployee =
			SictMissionTitel.MissionTitelMitEntscaidungFactionKonstant("The Disgruntled Employee", SictFactionSictEnum.Rogue_Drones);

		static public SictMissionTitel MissionTitelTheMordusHeadhunters =
			SictMissionTitel.MissionTitelMitEntscaidungFactionKonstant("The Mordus Headhunters", SictFactionSictEnum.Mordus_Legion);

		static public SictMissionTitel MissionTitelSoftDrinkWars =
			SictMissionTitel.MissionTitelMitEntscaidungFactionKonstant("Soft Drink Wars", SictFactionSictEnum.Mercenaries);

		static public SictMissionTitel MissionTitelTheRovingRogueDrones =
			SictMissionTitel.MissionTitelMitEntscaidungFactionKonstant("The Roving Rogue Drones", SictFactionSictEnum.Rogue_Drones);

		static public SictMissionTitel MissionTitelClosingtheGate =
			SictMissionTitel.MissionTitelMitEntscaidungFactionKonstant("Closing the Gate", SictFactionSictEnum.Serpentis);

		static public SictMissionTitel MissionTitelCarryingAIMEDs =
			SictMissionTitel.MissionTitelMitEntscaidungFactionKonstant("Carrying AIMEDs", SictFactionSictEnum.Mercenaries);

		static public SictMissionTitel MissionTitelTrimmingTheFat =
			SictMissionTitel.MissionTitelMitEntscaidungFactionKonstant("Trimming the Fat", SictFactionSictEnum.Mercenaries);

		static public SictMissionTitel MissionTitelMurdererBroughtToJustice =
			SictMissionTitel.MissionTitelMitEntscaidungFactionKonstant("Murderer Brought To Justice", SictFactionSictEnum.Mercenaries);

		static public SictMissionTitel MissionTitelMissionOfMercy =
			SictMissionTitel.MissionTitelMitEntscaidungFactionKonstant("Mission of Mercy", SictFactionSictEnum.Mercenaries);

		static public SictMissionTitel MissionTitelCustomsInterdiction_1_of_2 =
			SictMissionTitel.MissionTitelMitEntscaidungFactionKonstant("Customs Interdiction (1 of 2)", SictFactionSictEnum.Galente_Federation);

		static public SictMissionTitel MissionTitelInfiltratedOutposts =
			SictMissionTitel.MissionTitelMitEntscaidungFactionKonstant("Infiltrated Outposts", SictFactionSictEnum.Rogue_Drones);

		static public SictMissionTitel MissionTitelInterceptTheSaboteurs =
			SictMissionTitel.MissionTitelMitEntscaidungFactionStandard("Intercept The Saboteurs");

		static public SictMissionTitel MissionTitelEliminateThePirateCampers =
			SictMissionTitel.MissionTitelMitEntscaidungFactionStandard("Eliminate the Pirate Campers");

		static public SictMissionTitel MissionTitelSmugglerInterception =
			SictMissionTitel.MissionTitelMitEntscaidungFactionStandard("Smuggler Interception");

		static public SictMissionTitel MissionTitelRetribution =
			SictMissionTitel.MissionTitelMitEntscaidungFactionStandard(Regex.Escape("Retribution"));

		static public SictMissionTitel MissionTitelBreakTheirWill =
			SictMissionTitel.MissionTitelMitEntscaidungFactionStandard(Regex.Escape("Break Their Will"));

		static public SictMissionTitel MissionTitelTheRogueSlaveTrader1of2 =
			SictMissionTitel.MissionTitelMitEntscaidungFactionStandard(Regex.Escape("The Rogue Slave Trader (1 of 2)"));

		static public SictMissionTitel MissionTitelDowningtheSlavers2of2 =
			SictMissionTitel.MissionTitelMitEntscaidungFactionStandard(Regex.Escape("Downing the Slavers (2 of 2)"));

		static public SictMissionTitel MissionTitelSeekandDestroy =
			SictMissionTitel.MissionTitelMitEntscaidungFactionStandard(Regex.Escape("Seek and Destroy"));

		static public SictMissionTitel MissionTitelCargoDelivery =
			SictMissionTitel.MissionTitelMitEntscaidungFactionStandard(Regex.Escape("Cargo Delivery"));

		static public SictMissionTitel MissionTitelDuoOfDeath =
			SictMissionTitel.MissionTitelMitEntscaidungFactionStandard(Regex.Escape("Duo of Death"));

		static public SictMissionTitel MissionTitelTheSpyStash =
			SictMissionTitel.MissionTitelMitEntscaidungFactionStandard(Regex.Escape("The Spy Stash"));

		static public SictMissionTitel MissionTitelTheHiddenStash =
			SictMissionTitel.MissionTitelMitEntscaidungFactionStandard(Regex.Escape("The Hidden Stash"));

		static public SictMissionTitel MissionTitelUnauthorizedMilitaryPresence =
			SictMissionTitel.MissionTitelMitEntscaidungFactionStandard(Regex.Escape("Unauthorized Military Presence"));

		static public SictMissionTitel MissionTitelInterceptThePirateSmugglers =
			SictMissionTitel.MissionTitelMitEntscaidungFactionStandard("Intercept The Pirate Smugglers");

		static public SictMissionTitel MissionTitelAvengeAFallenComrade =
			SictMissionTitel.MissionTitelMitEntscaidungFactionStandard("Avenge a Fallen Comrade");

		static public SictMissionTitel MissionTitelEliminateAPirateNuisance =
			SictMissionTitel.MissionTitelMitEntscaidungFactionStandard("Eliminate a Pirate Nuisance");

		static public SictMissionTitel MissionTitelViolentExpulsion =
			SictMissionTitel.MissionTitelMitEntscaidungFactionStandard("Violent Expulsion");

		static public SictMissionTitel MissionTitelLostRecords =
			SictMissionTitel.MissionTitelMitEntscaidungFactionStandard("Lost Records");

		static public SictMissionTitel MissionTitelTheDrugBust =
			SictMissionTitel.MissionTitelMitEntscaidungFactionStandard("The Drug Bust");

		static public SictMissionTitel MissionTitelMassiveAttack =
			SictMissionTitel.MissionTitelMitEntscaidungFactionStandard("Massive Attack");

		static public SictMissionTitel MissionTitelSmallMisunderstanding_1_of_3 =
			SictMissionTitel.MissionTitelMitEntscaidungFactionStandard(Regex.Escape("Small Misunderstanding (1 of 3)"));

		static public SictMissionTitel MissionTitelSmallMisunderstanding_2_of_3 =
			SictMissionTitel.MissionTitelMitEntscaidungFactionStandard(Regex.Escape("Small Misunderstanding (2 of 3)"));

		static public SictMissionTitel MissionTitelSmallMisunderstanding_3_of_3 =
			SictMissionTitel.MissionTitelMitEntscaidungFactionStandard(Regex.Escape("Small Misunderstanding (3 of 3)"));

		static public SictMissionTitel MissionTitelAnAncientRoster =
			SictMissionTitel.MissionTitelMitEntscaidungFactionStandard("An Ancient Roster");

		static public SictMissionTitel MissionTitelCorporateRecords =
			SictMissionTitel.MissionTitelMitEntscaidungFactionStandard("Corporate Records");

		static public SictMissionTitel MissionTitelAttackTheAngelHideout =
			SictMissionTitel.MissionTitelMitEntscaidungFactionKonstant("Attack the Angel Hideout", SictFactionSictEnum.Angel_Cartel);

		static public SictMissionTitel MissionTitelNoHonor =
			SictMissionTitel.MissionTitelMitEntscaidungFactionKonstant("No Honor", SictFactionSictEnum.Mercenaries);

		static public SictMissionTitel MissionTitelDroneDetritus =
			SictMissionTitel.MissionTitelMitEntscaidungFactionKonstant("Drone Detritus", SictFactionSictEnum.Rogue_Drones);

		static public SictMissionTitel MissionTitelTheRightHandOfZazzmatazz	=
			SictMissionTitel.MissionTitelMitEntscaidungFactionKonstant("The Right Hand Of Zazzmatazz", SictFactionSictEnum.Mercenaries);

	}

	public partial class SictKonfigMissionStrategikon
	{
		static public SictMissionStrategikon MissionStrategikonFürMissionOffer(
			WindowAgentDialogue MissionOffer)
		{
			if (null == MissionOffer)
			{
				return null;
			}

			return StrategikonFürMissionInfo(MissionOffer.ZusamefasungMissionInfo);
		}

		static public SictMissionStrategikon StrategikonFürMissionInfo(
			WindowAgentMissionInfo MissionInfo)
		{
			return MengeFactionUndStrategikonFürMissionInfo(MissionInfo).Value;
		}

		static public KeyValuePair<SictFactionSictEnum[], SictMissionStrategikon>
			MengeFactionUndStrategikonFürMissionInfo(
			WindowAgentMissionInfo MissionInfo)
		{
			if (null == MissionInfo)
			{
				return default(KeyValuePair<SictFactionSictEnum[], SictMissionStrategikon>);
			}

			var MengePasend = new List<KeyValuePair<SictFactionSictEnum[], SictMissionStrategikon>>();

			foreach (var MissionStrategikon in MengeMissionStrategikon)
			{
				if (null == MissionStrategikon)
				{
					continue;
				}

				SictFactionSictEnum[] AusOfferMengeFaction;

				if (MissionStrategikon.PasendZuMissionOffer(MissionInfo, out	AusOfferMengeFaction))
				{
					var MengeFaction = AusOfferMengeFaction;

					if(MengeFaction.NullOderLeer())
					{
						MengeFaction = MissionStrategikon.MengeFactionPassend;
					}

					MengePasend.Add(new KeyValuePair<SictFactionSictEnum[], SictMissionStrategikon>(MengeFaction, MissionStrategikon));
				}
			}

			return MengePasend.FirstOrDefault();
		}

		static public SictMissionStrategikon[] MengeMissionStrategikonFürMissionTitel(
			string MissionTitel)
		{
			if (null == MissionTitel)
			{
				return null;
			}

			var MengePasend = new List<SictMissionStrategikon>();

			foreach (var MissionStrategikon in MengeMissionStrategikon)
			{
				if (null == MissionStrategikon)
				{
					continue;
				}

				if (MissionStrategikon.PasendZuMissionTitel(MissionTitel))
				{
					MengePasend.Add(MissionStrategikon);
				}
			}

			return MengePasend.ToArray();
		}

		static public SictDamageMitBetraagIntValue[] ProfiilDamageGlaicmääsig =
					new SictDamageMitBetraagIntValue[]{
						new	SictDamageMitBetraagIntValue(SictDamageTypeSictEnum.EM, 30),
						new	SictDamageMitBetraagIntValue(SictDamageTypeSictEnum.Kin, 30),
						new	SictDamageMitBetraagIntValue(SictDamageTypeSictEnum.Therm, 30),
						new	SictDamageMitBetraagIntValue(SictDamageTypeSictEnum.Exp, 30),};

		static public SictDamageMitBetraagIntValue[] ProfiilTankRogueDrones =
					new SictDamageMitBetraagIntValue[]{
						new	SictDamageMitBetraagIntValue(SictDamageTypeSictEnum.EM, 100),
						new	SictDamageMitBetraagIntValue(SictDamageTypeSictEnum.Therm, 60),
						new	SictDamageMitBetraagIntValue(SictDamageTypeSictEnum.Kin, 30),
						new	SictDamageMitBetraagIntValue(SictDamageTypeSictEnum.Exp, 30),};

		static public SictDamageMitBetraagIntValue[] ProfiilTankAngelCartel =
					new SictDamageMitBetraagIntValue[]{
						new	SictDamageMitBetraagIntValue(SictDamageTypeSictEnum.Exp, 100),
						new	SictDamageMitBetraagIntValue(SictDamageTypeSictEnum.Kin, 50),};

		static public SictDamageMitBetraagIntValue[] ProfiilDamageGuristas =
					new SictDamageMitBetraagIntValue[]{
						new	SictDamageMitBetraagIntValue(SictDamageTypeSictEnum.Kin, 100),
						new	SictDamageMitBetraagIntValue(SictDamageTypeSictEnum.Therm, 50),};

		static public SictDamageMitBetraagIntValue[] ProfiilTankGuristas = ProfiilDamageGuristas;

		static public SictDamageMitBetraagIntValue[] ProfiilDamageThukkerTribe =
					new SictDamageMitBetraagIntValue[]{
						new	SictDamageMitBetraagIntValue(SictDamageTypeSictEnum.Exp, 100),
						new	SictDamageMitBetraagIntValue(SictDamageTypeSictEnum.Therm, 50),};

		static public SictDamageMitBetraagIntValue[] ProfiilTankThukkerTribe =
					new SictDamageMitBetraagIntValue[]{
						new	SictDamageMitBetraagIntValue(SictDamageTypeSictEnum.Exp, 100),
						new	SictDamageMitBetraagIntValue(SictDamageTypeSictEnum.Kin, 60),
						new	SictDamageMitBetraagIntValue(SictDamageTypeSictEnum.EM, 30),
						new	SictDamageMitBetraagIntValue(SictDamageTypeSictEnum.Therm, 30),};

		static public SictDamageMitBetraagIntValue[] ProfiilTankThe_Blood_Raider_Covenant =
					new SictDamageMitBetraagIntValue[]{
						new	SictDamageMitBetraagIntValue(SictDamageTypeSictEnum.EM, 100),
						new	SictDamageMitBetraagIntValue(SictDamageTypeSictEnum.Therm, 60),
						new	SictDamageMitBetraagIntValue(SictDamageTypeSictEnum.Kin, 30),
						new	SictDamageMitBetraagIntValue(SictDamageTypeSictEnum.Exp, 30),};

		static public SictDamageMitBetraagIntValue[] ProfiilDamageSanshaNation =
					new SictDamageMitBetraagIntValue[]{
						new	SictDamageMitBetraagIntValue(SictDamageTypeSictEnum.EM, 100),
						new	SictDamageMitBetraagIntValue(SictDamageTypeSictEnum.Therm, 50),};

		static public SictDamageMitBetraagIntValue[] ProfiilTankSanshaNation = ProfiilDamageSanshaNation;


		static public KeyValuePair<SictFactionSictEnum, SictDamageMitBetraagIntValue[]>[] MengeFürFactionProfilTank =
			new KeyValuePair<SictFactionSictEnum, SictDamageMitBetraagIntValue[]>[]{
				new	KeyValuePair<SictFactionSictEnum,	SictDamageMitBetraagIntValue[]>(
					SictFactionSictEnum.Rogue_Drones, ProfiilTankRogueDrones),
				new	KeyValuePair<SictFactionSictEnum,	SictDamageMitBetraagIntValue[]>(
					SictFactionSictEnum.Guristas_Pirates, ProfiilTankGuristas),
				new	KeyValuePair<SictFactionSictEnum,	SictDamageMitBetraagIntValue[]>(
					SictFactionSictEnum.Angel_Cartel, ProfiilTankAngelCartel),
				new	KeyValuePair<SictFactionSictEnum,	SictDamageMitBetraagIntValue[]>(
					SictFactionSictEnum.Scions_of_the_Superior_Gene, ProfiilTankGuristas),
				new	KeyValuePair<SictFactionSictEnum,	SictDamageMitBetraagIntValue[]>(
					SictFactionSictEnum.Sanshas_Nation, ProfiilTankSanshaNation),
				new	KeyValuePair<SictFactionSictEnum,	SictDamageMitBetraagIntValue[]>(
					SictFactionSictEnum.Mercenaries, ProfiilTankGuristas),
				new	KeyValuePair<SictFactionSictEnum,	SictDamageMitBetraagIntValue[]>(
					SictFactionSictEnum.Thukker_Tribe, ProfiilTankThukkerTribe),
				new	KeyValuePair<SictFactionSictEnum,	SictDamageMitBetraagIntValue[]>(
					SictFactionSictEnum.The_Blood_Raider_Covenant, ProfiilTankThe_Blood_Raider_Covenant),
				new	KeyValuePair<SictFactionSictEnum,	SictDamageMitBetraagIntValue[]>(
					SictFactionSictEnum.EoM, ProfiilTankGuristas),
			};

		static public SictDamageMitBetraagIntValue[] FürFactionProfilDamage(SictFactionSictEnum Faction)
		{
			return null;
			/*
			 * 2013.09.15
			 * Werd ersezt durc für Faction Vorgaabe Fitting und zu Mission Info Profil Damage
			 * 
				return MengeFürFactionProfilDamage.FirstOrDefault((Kandidaat) => Kandidaat.Key == Faction).Value;
			 * */
		}

		static public SictDamageMitBetraagIntValue[] FürFactionProfilTank(SictFactionSictEnum Faction)
		{
			return MengeFürFactionProfilTank.FirstOrDefault((Kandidaat) => Kandidaat.Key == Faction).Value;
		}

		static public SictMissionStrategikon[] MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon MissionStrategikonBaasis,
			SictFactionSictEnum Faction)
		{
			return MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
				MissionStrategikonBaasis,
				new SictFactionSictEnum[] { Faction });
		}

		static public SictMissionStrategikon[] MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon MissionStrategikonBaasis,
			SictMissionTitel MissionTitel)
		{
			if (null == MissionStrategikonBaasis)
			{
				return null;
			}

			if (null == MissionTitel)
			{
				return null;
			}

		/*
		 * 2015.02.07
		 * 
		 * Vorersct kaine Scpezialisatioon des Strategikon für Faction.
		 * 
			var MissionTitelMengeEntscaidungAnnaameFaction = MissionTitel.MengeEntscaidungAnnaameFaction;

			if (null == MissionTitelMengeEntscaidungAnnaameFaction)
			{
				System.Diagnostics.Debugger.Break();
				return null;
			}

			MissionStrategikonBaasis = MissionStrategikonBaasis.Kopii();

			MissionStrategikonBaasis.MissionTitel = MissionTitel;

			var MengeFaction =
				MissionTitelMengeEntscaidungAnnaameFaction
				.Select((EntscaidungAnnaameFaction) => EntscaidungAnnaameFaction.Faction)
				.Where((Faction) => Faction.HasValue)
				.Select((Faction) => Faction.Value)
				.ToArray();

			return MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
				MissionStrategikonBaasis,
				MengeFaction);
			* */

			var Strategikon = MissionStrategikonBaasis.Kopii();

			Strategikon.MissionTitel = MissionTitel;

			if (MissionTitel.Faction.HasValue)
			{
				Strategikon.MengeFactionPassend = new SictFactionSictEnum[] { MissionTitel.Faction.Value };
			}

			var Faction = Strategikon.MengeFactionPassend.FirstOrDefaultNullable();

			Strategikon.AnnaameDamage = FürFactionProfilDamage(Faction);
			Strategikon.AnnaameTank = FürFactionProfilTank(Faction);

			return new SictMissionStrategikon[] { Strategikon };
		}


		/// <summary>
		/// Erstellt jewails für Faction aus MengeFaction ain Strategikon und füügt dort Standardprofiile für Dps und Tank für diise Faction ain.
		/// </summary>
		/// <param name="MissionStrategikonBaasis"></param>
		/// <param name="MengeFaction"></param>
		/// <returns></returns>
		static public SictMissionStrategikon[] MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon MissionStrategikonBaasis,
			SictFactionSictEnum[] MengeFaction)
		{
			if (null == MissionStrategikonBaasis)
			{
				return null;
			}

			var MengeMissionStrategikon = new List<SictMissionStrategikon>();

			if (null != MengeFaction)
			{
				for (int FactionIndex = 0; FactionIndex < MengeFaction.Length; FactionIndex++)
				{
					var Faction = MengeFaction[FactionIndex];

					var MissionStrategikon = MissionStrategikonBaasis.Kopii();

					MissionStrategikon.MengeFactionPassend = new SictFactionSictEnum[] { Faction };
					MissionStrategikon.AnnaameDamage = FürFactionProfilDamage(Faction);
					MissionStrategikon.AnnaameTank = FürFactionProfilTank(Faction);

					MengeMissionStrategikon.Add(MissionStrategikon);
				}
			}

			return MengeMissionStrategikon.ToArray();
		}


		/*
			* 2013.08.22
			* 
			* The Missing Convoy
			* 
			* Bring Item Objective
			* Acquire these goods: Item	1 x Special Delivery (0,1 m³)
			* 
			* Objective
			* Destroy the Lesser Drone Hive, loot the special delivery package, and then report back to your agent.
			*/
		static public SictMissionStrategikon[] MengeMissionStrategikonTheMissingConvoy =
				MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			/*
			 * 2014.02.09
			 * 
			 * Änderung Strategikon: Ainfüürung höhere Prio Destrukt für Forcefield und Drone Bunker
			 * 
			SictMissionStrategikon.StrategikonZersctööreUndDurcsuuceCargoWenNictObjektExistent(
			new	SictStrategikonOverviewObjektFilter[]{
				new	SictStrategikonOverviewObjektFilter(SictOverviewObjektGrupeEnum.LargeCollidableStructure, "Forcefield", "Powerful.*Forcefield", false),
				new	SictStrategikonOverviewObjektFilter(SictOverviewObjektGrupeEnum.LargeCollidableStructure, "Drone Bunker", "Hive", false),

				/*
				 * 2014.01.15
				 * Naac Beobactung das Automaat maistens rauswarpe mus da Treferpunkte zu tiif sinken werd vorersct das angraife aler Rat scon zu Begin als Strategikon verwendet.
				 * *
				SictStrategikonOverviewObjektFilter.FilterRat(),
			},
				SictStrategikonOverviewObjektFilter.FilterCargoContainer(),
				SictStrategikonOverviewObjektFilter.FilterAccelerationGate()),
			 * */
				SictMissionStrategikon.StrategikonZersctööreUndDurcsuuceCargo(
				new KeyValuePair<SictStrategikonOverviewObjektFilter, SictInRaumObjektBearbaitungPrio>[]{
					new	KeyValuePair<SictStrategikonOverviewObjektFilter,	SictInRaumObjektBearbaitungPrio>(
						new	SictStrategikonOverviewObjektFilter(SictOverviewObjektGrupeEnum.LargeCollidableStructure, "Forcefield", "Powerful.*Forcefield", false),
						SictInRaumObjektBearbaitungPrio.KonstruktFürInGrupeIndex(8)),
					new	KeyValuePair<SictStrategikonOverviewObjektFilter,	SictInRaumObjektBearbaitungPrio>(
						new	SictStrategikonOverviewObjektFilter(SictOverviewObjektGrupeEnum.LargeCollidableStructure, "Drone Bunker", "Hive", false),
						SictInRaumObjektBearbaitungPrio.KonstruktFürInGrupeIndex(4)),
					new	KeyValuePair<SictStrategikonOverviewObjektFilter,	SictInRaumObjektBearbaitungPrio>(
						SictStrategikonOverviewObjektFilter.FilterRat(),
						new	SictInRaumObjektBearbaitungPrio()),
						},
					SictStrategikonOverviewObjektFilter.FilterCargoContainer()),
					SictKonfigMissionTitel.MissionTitelTheMissingConvoy);

		/*
		 * 2013.09.05
		 * 
		 * The Blockade
		 * 
		 * Objective
		 * Eliminate the leader of the pirates blockading the stargate. You will be informed when he has been dispatched, his demise should disperse the pirate gang eventually.
		 */
		static public SictMissionStrategikon[] MengeMissionStrategikonTheBlockade =
				MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
					SictMissionStrategikon.StrategikonZersctööreAleRat(),
					SictKonfigMissionTitel.MissionTitelTheBlockade);

		/*
		 * 2013.09.05
		 * 
		 * The Score
		 * 
		 * Objective
		 * Time to settle the score. Go and destroy all the ships at the encounter. Your main target will be lurking around the radio telescope, seek and destroy then return to me.
		 */
		static public SictMissionStrategikon[] MengeMissionStrategikonTheScore =
				MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
				SictMissionStrategikon.StrategikonZersctööreAleRat(),
					SictKonfigMissionTitel.MissionTitelTheScore);

		/*
		 * 2013.09.05
		 * 
		 * The Good Word
		 * 
		 * Bring Item Objective:
		 * Acquire these goods:	Item	1 x Preacher (1,0 m³)
		 * 
		 * Objective:
		 * Take the preacher called Tolmak from the Yunshin Z refining complex to your agent.
		 */
		static public SictMissionStrategikon[] MengeMissionStrategikonTheGoodWord =
				MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
				SictMissionStrategikon.StrategikonDurcsuuceCargo(
				new SictStrategikonOverviewObjektFilter(SictOverviewObjektGrupeEnum.SpawnContainer,
					"Preacher", "Preacher.*Quarter", false)),
					SictKonfigMissionTitel.MissionTitelTheGoodWord);

		/*
		 * 2013.09.05
		 * 
		 * Intercept The Saboteurs
		 * 
		 * Bring Item Objective
		 * Acquire these goods:	Item	40 x Confiscated Viral Agent (20,0 m³)
		 * 
		 * Objective
		 * Intercept the transport ships.
		 */
		static public SictMissionStrategikon[] MengeMissionStrategikonInterceptTheSaboteurs =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööreAleRatUndDurcsuuceCargo(SictStrategikonOverviewObjektFilter.FilterCargoContainer()),
			SictKonfigMissionTitel.MissionTitelInterceptTheSaboteurs);

		/*
		 * 2013.09.05
		 * 
		 * What Comes Around Goes Around
		 * 
		 * Objective
		 * Destroy the bounty hunter and his merc henchmen, then report back to your agent.
		 */
		static public SictMissionStrategikon[] MengeMissionStrategikonWhatComesAroundGoesAround =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööreAleRat(),
				SictKonfigMissionTitel.MissionTitelWhatComesAroundGoesAround);

		/*
		 * 2013.09.11
		 * 
		 * Smuggler Interception
		 * 
		 * Bring Item Objective:
		 * Acquire these goods:	Item	10 x Militants (20,0 m³)	
		 * 
		 * Objective:
		 * Destroy convoy and return with cargo to agent.
		 */
		static public SictMissionStrategikon[] MengeMissionStrategikonSmugglerInterception =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööreUndDurcsuuceCargo(
			new KeyValuePair<SictStrategikonOverviewObjektFilter, SictInRaumObjektBearbaitungPrio>[]{
				new	KeyValuePair<SictStrategikonOverviewObjektFilter,	SictInRaumObjektBearbaitungPrio>(
					SictStrategikonOverviewObjektFilter.FilterRatMitNaameRegexPattern("Person"),	SictInRaumObjektBearbaitungPrio.KonstruktFürInGrupeIndex(4)),
				new	KeyValuePair<SictStrategikonOverviewObjektFilter,	SictInRaumObjektBearbaitungPrio>(
					SictStrategikonOverviewObjektFilter.FilterRat(),	new	SictInRaumObjektBearbaitungPrio()),
			},
			SictStrategikonOverviewObjektFilter.MengeFilterWreckMitTypeOderNameRegex("Person")),
				SictKonfigMissionTitel.MissionTitelSmugglerInterception);

		/// <summary>
		/// 2013.11.27
		/// The Hidden Stash
		/// 
		/// Bring Item Objective
		/// Acquire these goods:	Item	15 x Small Sealed Cargo Containers (150,0 m³)	
		/// 
		/// Objective
		/// Destroy the warehouse and retrieve the sealed containers, eliminate the pirate threat, and then return to your agent with the goods.
		/// </summary>
		static public SictMissionStrategikon[] MengeMissionStrategikonTheHiddenStash =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööreUndDurcsuuceCargo(
			new SictStrategikonOverviewObjektFilter[]{
				SictStrategikonOverviewObjektFilter.FilterRat(),
				new	SictStrategikonOverviewObjektFilter(SictOverviewObjektGrupeEnum.LargeCollidableStructure, "storage", "warehouse", false),},
				SictStrategikonOverviewObjektFilter.FilterCargoContainer()),
				SictKonfigMissionTitel.MissionTitelTheHiddenStash);

		/*
		 * 2013.09.05
		 * 
		 * Pirate Invasion
		 * 
		 * Objective
		 * Neutralize the entire enemy force and halt their sneak attack.
		 */
		static public SictMissionStrategikon[] MengeMissionStrategikonPirateInvasion =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööreAleRat(),
				SictKonfigMissionTitel.MissionTitelPirateInvasion);

		/*
		 * 2013.09.05
		 * 
		 * The Damsel In Distress
		 * 
		 * Bring Item Objective
		 * Acquire these goods:	Item	1 x The Damsel (0,1 m³)
		 * 
		 * Objective
		 * Infiltrate Kruul's hideout and rescue the damsel. She is currently being held inside Kruul's Pleasure Hub.
		 */
		static public SictMissionStrategikon[] MengeMissionStrategikonTheDamselInDistress =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööreUndDurcsuuceCargo(
			new SictStrategikonOverviewObjektFilter[]{
				new	SictStrategikonOverviewObjektFilter(SictOverviewObjektGrupeEnum.LargeCollidableStructure, "module", "pleasure", false),
				new	SictStrategikonOverviewObjektFilter(SictOverviewObjektGrupeEnum.LargeCollidableStructure, "pleasure hub", null, false),
				/*
				 * 2013.10.04
				 * Vermuutung Angrif auf Kruul lööst Reinforcements aus, daher diisen scpääter angraife.
				 * *
				new	SictStrategikonOverviewObjektFilter(SictOverviewObjektGrupeEnum.Rat, ".", "kruul", true),
				 * */
				},
				SictStrategikonOverviewObjektFilter.FilterCargoContainer()),
				SictKonfigMissionTitel.MissionTitelTheDamselInDistress);

		/*
		 * 2013.09.09
		 * 
		 * Eliminate the Pirate Campers
		 * 
		 * Objective
		 * Eliminate the Blood Raider ships roaming around the stargate near the coordinates of your bookmark.
		 */
		static public SictMissionStrategikon[] MengeMissionStrategikonEliminateThePirateCampers =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööreAleRat(),
				SictKonfigMissionTitel.MissionTitelEliminateThePirateCampers);

		/*
		 * 2013.09.12
		 * 
		 * Retribution
		 * 
		 * Objective
		 * Destroy the Guristas Outpost and then report back to your agent.
		 */
		static public SictMissionStrategikon[] MengeMissionStrategikonRetribution =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			/*
			 * 2014.00.07
			 * 
			new SictMissionStrategikon(
				null,
				null,
				null,
				new	SictObjektIdentitäätPerTypeUndNameRegexUndMainIconFarbe[]{
					//	new	SictObjektIdentitäätPerTypeUndNameRegexUndMainIconFarbe(".", null, true),
					new	SictObjektIdentitäätPerTypeUndNameRegexUndMainIconFarbe("outpost", "outpost", false),
				}),
			 * */
			SictMissionStrategikon.StrategikonZersctööre(
				new SictStrategikonOverviewObjektFilter(SictOverviewObjektGrupeEnum.LargeCollidableStructure, "outpost", "outpost", false)),
				SictKonfigMissionTitel.MissionTitelRetribution);

		/*
		 * 2013.09.12
		 * 
		 * Break Their Will
		 * 
		 * Objective
		 * Find the repair station at the location your agent gives you, and attack and destroy it. It may or may not be guarded.
		 */
		static public SictMissionStrategikon[] MengeMissionStrategikonBreakTheirWill =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööre(
			/*
			 * 2014.05.28
			 * Änderung naac Beobactung in MSN von LVL3 Agent:
			 * Repair Station scaint Rats zu repariire und zu Begin des Raums werd Scpiiler ainmaal von Rat gejammed dese Destrukt daraufhin hööhere Prio erhält als Repair Station.
			 * 
			new	SictStrategikonOverviewObjektFilter[]{
				new	SictStrategikonOverviewObjektFilter(SictOverviewObjektGrupeEnum.LargeCollidableStructure, "repair", "repair station", false),
				new	SictStrategikonOverviewObjektFilter(SictOverviewObjektGrupeEnum.LargeCollidableStructure, "shipyard", "repair station", false),}),
			 * */
			new KeyValuePair<SictStrategikonOverviewObjektFilter, SictInRaumObjektBearbaitungPrio>[]{
				new	KeyValuePair<SictStrategikonOverviewObjektFilter,	SictInRaumObjektBearbaitungPrio>(
					new	SictStrategikonOverviewObjektFilter(SictOverviewObjektGrupeEnum.LargeCollidableStructure, "repair", "repair station", false),
					new	SictInRaumObjektBearbaitungPrio(true,	true,	new	SictEWarTypeEnum[]{ SictEWarTypeEnum.Webify, SictEWarTypeEnum.Jam})),
				new	KeyValuePair<SictStrategikonOverviewObjektFilter,	SictInRaumObjektBearbaitungPrio>(
					new	SictStrategikonOverviewObjektFilter(SictOverviewObjektGrupeEnum.LargeCollidableStructure, "shipyard", "repair station", false),
					new	SictInRaumObjektBearbaitungPrio(true,	true,	new	SictEWarTypeEnum[]{ SictEWarTypeEnum.Webify, SictEWarTypeEnum.Jam})),
			}),
				SictKonfigMissionTitel.MissionTitelBreakTheirWill);

		/*
		 * 2013.09.14
		 * 
		 * Stop The Thief
		 * 
		 * Bring Item Objective
		 * Acquire these goods:	Item	1 x Reports (0,1 m³)	
		 * 
		 * Objective
		 * Destroy the Thief, loot the reports, and report back to your agent.
		 */
		static public SictMissionStrategikon[] MengeMissionStrategikonStopTheThief =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			/*
			 * 2013.11.24
			 * 
			new SictMissionStrategikon(
				null,
				null,
				new SictOverviewObjektFilterTypeUndName[]{
					SictOverviewObjektFilterTypeUndName.FilterCargoContainer(),
				},
				new SictObjektIdentitäätPerTypeUndNameRegexUndMainIconFarbeMitPrio[]{
					new	SictObjektIdentitäätPerTypeUndNameRegexUndMainIconFarbeMitPrio(
						SictStrategikonOverviewObjektFilter.FilterRat())
				}),
			 * */
			/*
			 * 2014.00.06
			 * 
			SictMissionStrategikon.StrategikonZersctööreAleRatUndDurcsuuceCargo(SictStrategikonOverviewObjektFilter.FilterCargoContainer()),
			 * */
			SictMissionStrategikon.StrategikonZersctööreUndDurcsuuceCargo(
				new SictStrategikonOverviewObjektFilter(SictOverviewObjektGrupeEnum.Rat, "Thief", "Thief", true),
				SictStrategikonOverviewObjektFilter.FilterCargoContainer()),
				SictKonfigMissionTitel.MissionTitelStopTheThief);

		/*
		 * 2013.09.14
		 * 
		 * Success Comes At A Price
		 * 
		 * Objective
		 * Destroy the rogue agent and any ships nearby, including the Guristas Emissary.
		 * */
		static public SictMissionStrategikon[] MengeMissionStrategikonSuccessComesAtAPrice =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööreAleRat(),
				SictKonfigMissionTitel.MissionTitelSuccessComesAtAPrice);

		/*
		 * 2013.09.14
		 * 
		 * The Space Telescope
		 * 
		 * Objective
		 * Destroy the Guristas Space Telescope and then report back to your agent.
		 * */
		static public SictMissionStrategikon[] MengeMissionStrategikonTheSpaceTelescope =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööre(
			new SictStrategikonOverviewObjektFilter(SictOverviewObjektGrupeEnum.DeadspaceStructure, "Telescope", "Telescope", false)),
				SictKonfigMissionTitel.MissionTitelTheSpaceTelescope);

		/*
		 * 2013.09.15
		 * 
		 * Technological Secrets (1 of 3)
		 * 
		 * Bring Item Objective
		 * Acquire these goods:	Item	1 x Mercenary Pilot (1,0 m³)	
		 * 
		 * Objective
		 * Fly to the coordinates in your journal. Destroy the Thukker mercenaries there, and wait for their reinforcements. Once all of the reinforcements have been destroyed, along with their captain, then report back to your agent.
		 * */
		static public SictMissionStrategikon[] MengeMissionStrategikonTechnologicalSecrets1of3 =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööreAleRatUndDurcsuuceCargo(
				SictStrategikonOverviewObjektFilter.FilterCargoContainer(),
			/*
			 * 2013.08.02
			 * Di losn sic tailwaise bis zu viir minuute Zait.
			 */
				(4 * 60 + 10) * 1000),
				SictKonfigMissionTitel.MissionTitelTechnologicalSecrets1of3);

		/*
		 * 2013.09.15
		 * 
		 * The Rogue Slave Trader (1 of 2)
		 * 
		 * Bring Item Objective
		 * Acquire these goods:	Item	20 x Prisoners (150,0 m³)	
		 * 
		 * Objective
		 * Find the slave pen and rescue the slaves.
		 * */
		static public SictMissionStrategikon[] MengeMissionStrategikonTheRogueSlaveTrader =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööreUndDurcsuuceCargo(
				new SictStrategikonOverviewObjektFilter(SictOverviewObjektGrupeEnum.LargeCollidableStructure, "asteroid", "slave pen", false),
				SictStrategikonOverviewObjektFilter.FilterCargoContainer()),
				SictKonfigMissionTitel.MissionTitelTheRogueSlaveTrader1of2);

		/*
		 * 2013.09.15
		 * 
		 * Downing the Slavers (2 of 2)
		 * 
		* Objective
		* Find the Blood Raider slavers and destroy them.
		 * */
		static public SictMissionStrategikon[] MengeMissionStrategikonDowningtheSlavers2of2 =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööre(new SictStrategikonOverviewObjektFilter(SictOverviewObjektGrupeEnum.Rat, ".", "slaver", true)),
				SictKonfigMissionTitel.MissionTitelDowningtheSlavers2of2);

		/*
		 * 2013.09.15
		 * 
		 * The Guristas Spies
		 * 
		 * Objective
		 * Eliminate the Guristas Spies. 
		 * */
		static public SictMissionStrategikon[] MengeMissionStrategikonTheGuristasSpies =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööreAleRat(),
				SictKonfigMissionTitel.MissionTitelTheGuristasSpies);

		/*
		 * 2013.09.16
		 * 
		 * Silence The Informant
		 * 
		 * Objective
		 * Find the former secret agent and eliminate him before he can reveal the top-secret information to agents from DED. 
		 * */
		static public SictMissionStrategikon[] MengeMissionStrategikonSilenceTheInformant =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööreAleRat(),
				SictKonfigMissionTitel.MissionTitelSilenceTheInformant);

		/*
		 * 2013.09.16
		 * 
		 * Gone Berserk
		 * 
		 * Objective
		 * Take care of the terrorist fanatics and then report back to your agent.
		 * */
		static public SictMissionStrategikon[] MengeMissionStrategikonGoneBerserk =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööreAleRat(),
				SictKonfigMissionTitel.MissionTitelGoneBerserk);

		/*
		 * 2013.09.17
		 * 
		 * Seek and Destroy
		 * 
		 * Objective
		 * Eliminate the pirates who are harassing innocent passersby.
		 * */
		static public SictMissionStrategikon[] MengeMissionStrategikonSeekandDestroy =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööreAleRat(),
				SictKonfigMissionTitel.MissionTitelSeekandDestroy);

		/*
		 * 2013.09.17
		 * 
		 * Rogue Drone Harassment
		 * 
		 * Objective
		 * Destroy the Rogue Drones, and then report back to your agent.
		 * */
		static public SictMissionStrategikon[] MengeMissionStrategikonRogueDroneHarassment =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööreAleRat(),
				SictKonfigMissionTitel.MissionTitelRogueDroneHarassment);

		/*
		 * 2013.09.17
		 * 
		 * Cargo Delivery
		 * 
		 * Bring Item Objective
		 * Acquire these goods:	Item	10 x Spiced Wine (5,0 m³)
		 * 
		 * Objective
		 * Retrieve the cargo from the warehouse, and bring it to your agent.
		 * */
		static public SictMissionStrategikon[] MengeMissionStrategikonCargoDelivery =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonDurcsuuceCargo(
			new SictStrategikonOverviewObjektFilter(SictOverviewObjektGrupeEnum.SpawnContainer, "warehouse", "warehouse", false)),
				SictKonfigMissionTitel.MissionTitelCargoDelivery);

		/*
		 * 2013.09.17
		 * 
		 * The Seven's Prison Facility
		 * 
		 * Objective
		 * Find and eliminate the leader of the 'Seven's' forces within their compound and his bodyguard, then report back to your agent.
		 * */
		static public SictMissionStrategikon[] MengeMissionStrategikonTheSevensPrisonFacility =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööreAleRat(),
				SictKonfigMissionTitel.MissionTitelTheSevensPrisonFacility);

		/*
		 * 2013.09.17
		 * 
		 * Duo of Death
		 * 
		 * Bring Item Objective
		 * Acquire these goods:	Item	1 x Special Delivery (0,1 m³)
		 * 
		 * Objective
		 * Defeat the pirate duo and retrieve the stolen special delivery for your agent.
		 * */
		static public SictMissionStrategikon[] MengeMissionStrategikonDuoOfDeath =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööreUndDurcsuuceCargo(
			new SictStrategikonOverviewObjektFilter(SictOverviewObjektGrupeEnum.Rat, "pith", null, true),
			SictStrategikonOverviewObjektFilter.FilterCargoContainer()),
				SictKonfigMissionTitel.MissionTitelDuoOfDeath);

		/*
		 * 2013.09.17
		 * 
		 * The Spy Stash
		 * 
		 * Bring Item Objective
		 * Acquire these goods:	Item	5 x Classified Report - Station Defenses (0,5 m³)
		 * 
		 * Objective
		 * Take the reports from the Guristas Officer's quarters and return them to your agent. The acceleration gates in this complex are not locked, and therefore do not require you to eliminate the guards to get through.
		 * */
		static public SictMissionStrategikon[] MengeMissionStrategikonTheSpyStash =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			/*
			 * 2014.00.08 Beobactung Abovemain Message:
			 * "<center>You cannot access the Guristas Officer's Quarters_Mission_Spy Stash (Guristas Officer's Quarters) while it is being guarded."
			 * -> In dem Raum in deem sic der Container befindet müsen ale Rat zersctöört werde.
			 * -> Daraus folgt ac das Zersctöörung der Rat mit höhere Prio erfolge sol da Automat sic sunsct sctändig mit deem Versuuc des Officer Quarter zu öfne bescäftigt.
			 * 2014.00.08 Beobactung:
			 * Cargo kan entnome werde obwool noc angraifende Sentry vorhande.
			 * 
			 * 2014.00.10 Beobactung Probleem:
			 * Ausfüürung Mission funktioniirt noc nit: in Raum zersctöört Automaat ale Rat obwool er scon am Acc-Gate scteet.
			 * */
			SictMissionStrategikon.StrategikonZersctööreUndDurcsuuceCargoWenNictObjektExistent(
				new KeyValuePair<SictStrategikonOverviewObjektFilter, SictInRaumObjektBearbaitungPrio>[]{
					new	KeyValuePair<SictStrategikonOverviewObjektFilter,	SictInRaumObjektBearbaitungPrio>(
						SictStrategikonOverviewObjektFilter.FilterRat(),	new	SictInRaumObjektBearbaitungPrio(null,	null,	null, 4))
				},
					new SictStrategikonOverviewObjektFilter(SictOverviewObjektGrupeEnum.SpawnContainer, "Officer.*Quarter", "Officer.*Quarter", false),
					SictStrategikonOverviewObjektFilter.MengeFilterAccelerationGate()),
				SictKonfigMissionTitel.MissionTitelTheSpyStash);

		/*
		 * 2013.09.18
		 * 
		 * Unauthorized Military Presence
		 * 
		 * Bring Item Objective
		 * Acquire these goods:	Item	10 x Militants (20,0 m³)
		 * 
		 * Objective
		 * Intercept the personnel transport ship, destroy its guards and bring the militants to your agent.
		 * */
		static public SictMissionStrategikon[] MengeMissionStrategikonUnauthorizedMilitaryPresence =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			/*
			 * 2014.04.22
			 * 
			 * Ainbau hööhere Prio für Personnel transport
			 * 
			SictMissionStrategikon.StrategikonZersctööreUndDurcsuuceCargoWenNictObjektExistent(
			new	SictStrategikonOverviewObjektFilter[]{
				SictStrategikonOverviewObjektFilter.FilterRat()},
				SictStrategikonOverviewObjektFilter.FilterWreckMitNameRegex("Person.*Wreck"),
				SictStrategikonOverviewObjektFilter.FilterAccelerationGate(),
				//	5555
				null
				),
			 * */
			SictMissionStrategikon.StrategikonZersctööreUndDurcsuuceCargoWenNictObjektExistent(
			new KeyValuePair<SictStrategikonOverviewObjektFilter, SictInRaumObjektBearbaitungPrio>[]{
				new	KeyValuePair<SictStrategikonOverviewObjektFilter,	SictInRaumObjektBearbaitungPrio>(
					SictStrategikonOverviewObjektFilter.FilterRat(),
					new	SictInRaumObjektBearbaitungPrio()),
				new	KeyValuePair<SictStrategikonOverviewObjektFilter,	SictInRaumObjektBearbaitungPrio>(
					SictStrategikonOverviewObjektFilter.FilterRatMitNaameRegexPattern("Person"),
					SictInRaumObjektBearbaitungPrio.KonstruktFürInGrupeIndex(4)),},
			/*
			 * 2014.04.24
			 * 
		SictStrategikonOverviewObjektFilter.FilterWreckMitNameRegex("Person.*Wreck"),
			 * */
					SictStrategikonOverviewObjektFilter.MengeFilterWreckMitTypeOderNameRegex("Person"),
				SictStrategikonOverviewObjektFilter.MengeFilterAccelerationGate(),
				null
				),
				SictKonfigMissionTitel.MissionTitelUnauthorizedMilitaryPresence);

		/*
		 * 2013.09.18
		 * 
		 * The Black Market Hub
		 * 
		 * Bring Item Objective
		 * Acquire these goods:	Item	10 x Small Sealed Cargo Containers (100,0 m³)
		 * 
		 * Objective
		 * Find the sealed containers, and then return to your agent with the goods.
		 * */
		static public SictMissionStrategikon MissionStrategikonTheBlackMarketHub =
			SictMissionStrategikon.StrategikonZersctööreUndDurcsuuceCargoWenNictObjektExistent(
				new SictStrategikonOverviewObjektFilter[]{
					new	SictStrategikonOverviewObjektFilter(SictOverviewObjektGrupeEnum.LargeCollidableStructure,	"Bestower", "Bestower", false),
				},
				SictStrategikonOverviewObjektFilter.FilterCargoContainer(),
				SictStrategikonOverviewObjektFilter.MengeFilterAccelerationGate());

		static public SictMissionStrategikon[] MengeMissionStrategikonTheBlackMarketHub =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			MissionStrategikonTheBlackMarketHub,
				SictKonfigMissionTitel.MissionTitelTheBlackMarketHub);

		/*
		 * 2013.09.21
		 * 
		 * Guristas Extravaganza
		 * 
		 * Objective
		 * Prevent the Guristas attack by flying to the 5 rendezvous points and destroying a large portion of their forces, including their highest ranking officer.
		 * */
		static public SictMissionStrategikon[] MengeMissionStrategikonGuristasExtravaganza =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			/*
			 * 2014.05.10
			 * Ainbau Priorisatioon für Jamming Rat
			 * 
			SictMissionStrategikon.StrategikonZersctööre(new	SictStrategikonOverviewObjektFilter[]{
				SictStrategikonOverviewObjektFilter.FilterRat(),
				//	In ainem Raum werd Acc Gate durc "Powerful EM Forcefield" blokiirt.
				new	SictStrategikonOverviewObjektFilter(SictOverviewObjektGrupeEnum.LargeCollidableStructure,	"Powerful.*Forcefield", "Forcefield", false)}),
			 * */
			SictMissionStrategikon.StrategikonZersctööre(
			new KeyValuePair<SictStrategikonOverviewObjektFilter, SictInRaumObjektBearbaitungPrio>[]{
				new	KeyValuePair<SictStrategikonOverviewObjektFilter,	SictInRaumObjektBearbaitungPrio>(
					SictStrategikonOverviewObjektFilter.FilterRat(),
					new	SictInRaumObjektBearbaitungPrio()),
					//	In ainem Raum werd Acc Gate durc "Powerful EM Forcefield" blokiirt.
				new	KeyValuePair<SictStrategikonOverviewObjektFilter,	SictInRaumObjektBearbaitungPrio>(
					new	SictStrategikonOverviewObjektFilter(SictOverviewObjektGrupeEnum.LargeCollidableStructure,	"Powerful.*Forcefield", "Forcefield", false),
					new	SictInRaumObjektBearbaitungPrio()),
					//	2014.05.09	Sictung "Dire Pithi Despoiler<br>Jamming"
				new	KeyValuePair<SictStrategikonOverviewObjektFilter,	SictInRaumObjektBearbaitungPrio>(
					SictStrategikonOverviewObjektFilter.FilterRatMitNaameRegexPattern("Despoiler"),
					SictInRaumObjektBearbaitungPrio.KonstruktFürInGrupeIndex(4)),
			}),
				SictKonfigMissionTitel.MissionTitelGuristasExtravaganza);

		/*
		 * 2013.09.21
		 * 
		 * Angel Extravaganza
		 * 
		 * Objective
		 * Prevent the Angel Cartel attack by flying to the 5 rendezvous points and destroying a large portion of their forces, including their highest ranking officer.
		 * */
		static public SictMissionStrategikon[] MengeMissionStrategikonAngelExtravaganza =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööre(new SictStrategikonOverviewObjektFilter[]{
				SictStrategikonOverviewObjektFilter.FilterRat(),
				//	In ainem Raum werd Acc Gate durc "Powerful EM Forcefield" blokiirt.
				new	SictStrategikonOverviewObjektFilter(SictOverviewObjektGrupeEnum.LargeCollidableStructure,	"Powerful.*Forcefield", "Forcefield", false)}),
				SictKonfigMissionTitel.MissionTitelAngelExtravaganza);

		/*
		 * 2013.09.22
		 * 
		 * Intercept The Pirate Smugglers
		 * 
		 * Objective
		 * Fly to the Space-Port, ambush the convoy when it arrives, and then report back to your agent.
		 * */
		static public SictMissionStrategikon[] MengeMissionStrategikonInterceptThePirateSmugglers =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonFliigeAnUndZersctööre(
			new SictStrategikonOverviewObjektFilter(SictOverviewObjektGrupeEnum.LargeCollidableStructure, "Shipyard", "Space-Port", false),
			16000,
			SictStrategikonOverviewObjektFilter.FilterRat(),
				1000 * 60),
				SictKonfigMissionTitel.MissionTitelInterceptThePirateSmugglers);

		/*
		 * 2013.10.18
		 * 
		 * The Assault
		 * 
		 * LVL4
		 * 
		 * Objective
		 * Kill all enemy forces.
		 * */
		static public SictMissionStrategikon[] MengeMissionStrategikonTheAssault =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööreAleRat(),
				SictKonfigMissionTitel.MissionTitelTheAssault);

		/// <summary>
		/// 2013.11.27
		/// Pirate Intrusion
		/// 
		/// Objective
		/// Our surveillance cameras just spotted a few groups of hostile ships enter our system. Head out and bring me their heads on a plate, spare no one.
		/// </summary>
		static public SictMissionStrategikon[] MengeMissionStrategikonPirateIntrusion =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööreAleRat(),
				SictKonfigMissionTitel.MissionTitelPirateIntrusion);

		/// <summary>
		/// 2013.11.27
		/// The Drone Infestation
		/// 
		/// Objective
		/// Destroy the Drone Silo and then report back to your agent.
		/// </summary>
		static public SictMissionStrategikon[] MengeMissionStrategikonTheDroneInfestation =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööre(new SictStrategikonOverviewObjektFilter(SictOverviewObjektGrupeEnum.LargeCollidableStructure, "drone", "silo", false)),
				SictKonfigMissionTitel.MissionTitelTheDroneInfestation);

		/// <summary>
		/// 2013.11.28
		/// Save a Man's Career
		/// 
		/// Bring Item Objective
		/// Acquire these goods:	Item	1 x Reports (0,1 m³)	
		/// 
		/// Objective
		/// Find Sangrel Minn, destroy his ship and loot the reports.
		/// </summary>
		static public SictMissionStrategikon[] MengeMissionStrategikonSaveAMansCareer =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööreUndDurcsuuceCargo(
			new SictStrategikonOverviewObjektFilter(SictOverviewObjektGrupeEnum.Rat, ".", "Sangrel Minn", true),
			SictStrategikonOverviewObjektFilter.FilterCargoContainer()),
				SictKonfigMissionTitel.MissionTitelSaveAMansCareer);

		/// <summary>
		/// 2013.11.30
		/// The Seven's Brothel
		/// 
		/// Objective
		/// Find and eliminate the leader of the 'Seven's' forces within their compound, then report back to your agent.
		/// </summary>
		static public SictMissionStrategikon[] MengeMissionStrategikonTheSevensBrothel =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööreAleRat(),
				SictKonfigMissionTitel.MissionTitelTheSevensBrothel);


		/// <summary>
		/// 2014.01.26
		/// Furrier Fiasco
		/// LVL1
		/// 
		/// Objective
		/// Destroy the infested laboratory.
		/// </summary>
		static public SictMissionStrategikon[] MengeMissionStrategikonFurrierFiasco =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööre(new SictStrategikonOverviewObjektFilter(SictOverviewObjektGrupeEnum.LargeCollidableStructure, ".", "infested laboratory", false)),
				SictKonfigMissionTitel.MissionTitelFurrierFiasco);

		/// <summary>
		/// 2014.01.26
		/// Pirate Aggression
		/// LVL1
		/// 
		/// Objective
		/// Destroy the Angel Cartel goons at the location specified in your journal and then report back to your agent.
		/// </summary>
		static public SictMissionStrategikon[] MengeMissionStrategikonPirateAggression =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööreAleRat(),
				SictKonfigMissionTitel.MissionTitelPirateAggression);

		/// <summary>
		/// 2014.01.26
		/// LVL1
		/// 
		/// Objective
		/// Look for signs of any "alien activity".
		/// </summary>
		static public SictMissionStrategikon[] MengeMissionStrategikonMysteriousSightings_1_of_4 =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonDurcsuuceCargo(SictStrategikonOverviewObjektFilter.FilterCargoContainer(), 11111),
				SictKonfigMissionTitel.MissionTitelMysteriousSightings_1_of_4);

		/// <summary>
		/// 2014.01.26
		/// LVL1
		/// 
		/// Objective
		/// Investigate the asteroid belt. Retreat if you encounter overwhelming force.
		/// </summary>
		static public SictMissionStrategikon[] MengeMissionStrategikonAlluringEmanations =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööreAleRat(),
				SictKonfigMissionTitel.MissionTitelAlluringEmanations);

		/// <summary>
		/// 2014.01.26
		/// The Disgruntled Employee
		/// LVL1
		/// 
		/// Objective
		/// Eliminate the disgruntled ex-employee.
		/// </summary>
		static public SictMissionStrategikon[] MengeMissionStrategikonTheDisgruntledEmployee =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			/*
			 * 2014.02.01
			 * 
			SictMissionStrategikon.StrategikonZersctööreAleRat(),
			 * */
			SictMissionStrategikon.StrategikonZersctööre(new KeyValuePair<SictStrategikonOverviewObjektFilter, SictInRaumObjektBearbaitungPrio>[]{
				new	KeyValuePair<SictStrategikonOverviewObjektFilter,	SictInRaumObjektBearbaitungPrio>(
					SictStrategikonOverviewObjektFilter.FilterRatMitNaameRegexPattern("employee"), new	SictInRaumObjektBearbaitungPrio(null,	null,	null,	4)),
				new	KeyValuePair<SictStrategikonOverviewObjektFilter,	SictInRaumObjektBearbaitungPrio>(
					SictStrategikonOverviewObjektFilter.FilterRat(), new	SictInRaumObjektBearbaitungPrio()),
			}),
				SictKonfigMissionTitel.MissionTitelTheDisgruntledEmployee);

		/// <summary>
		/// 2014.01.26
		/// After The Seven (1 of 5) "Replacement"
		/// LVL1
		/// 
		/// Objective
		/// Protect the civilian convoy.
		/// </summary>
		static public SictMissionStrategikon[] MengeMissionStrategikonAfterTheSeven_1_of_5 =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööreAleRat(),
				SictKonfigMissionTitel.MissionTitelAfterTheSeven_1_of_5);

		/// <summary>
		/// 2014.01.26
		/// LVL1
		/// 
		/// Bring Item Objective
		/// Acquire these goods:
		/// Item	10 x Kidnapped Citizens (10,0 m³)	
		/// 
		/// Objective
		/// Rescue the kidnapped civilians.
		/// </summary>
		static public SictMissionStrategikon[] MengeMissionStrategikonAfterTheSeven_2_of_5 =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööreAleRatUndDurcsuuceCargo(
			SictStrategikonOverviewObjektFilter.FilterCargoContainer()),
				SictKonfigMissionTitel.MissionTitelAfterTheSeven_2_of_5);

		/// <summary>
		/// 2014.01.26
		/// After The Seven (3 of 5) "Idiot"
		/// LVL1
		/// 
		/// Bring Item Objective
		/// Acquire these goods:
		/// Item	1 x Phenod's DNA (0,1 m³)	
		/// 
		/// Objective
		/// Obtain a sample of Phenod's DNA.
		/// </summary>
		static public SictMissionStrategikon[] MengeMissionStrategikonAfterTheSeven_3_of_5 =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööreAleRatUndDurcsuuceCargo(
			SictStrategikonOverviewObjektFilter.FilterWreckMitNameRegex("Phenod")),
				SictKonfigMissionTitel.MissionTitelAfterTheSeven_3_of_5);

		/// <summary>
		/// 2014.02.00
		/// The Serpentis Spies
		/// LVL1
		/// 
		/// Objective
		/// Eliminate the Serpentis Spies. 
		/// </summary>
		static public SictMissionStrategikon[] MengeMissionStrategikonTheSerpentisSpies =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööreAleRat(),
				SictKonfigMissionTitel.MissionTitelTheSerpentisSpies);

		/// <summary>
		/// 2014.02.01
		/// The Mordus Headhunters
		/// LVL1
		/// 
		/// Objective
		/// One of our patrols spotted a Mordu's Legion strike force not far from here. We have intel that they will try an assassination attempt on one of our high-ranking members. We can't let this happen, so destroy them all and make sure no one survives. 
		/// </summary>
		static public SictMissionStrategikon[] MengeMissionStrategikonTheMordusHeadhunters =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööreAleRat(),
				SictKonfigMissionTitel.MissionTitelTheMordusHeadhunters);

		static readonly public string[] MissionSoftDrinkWarsMengeEmployeeName = new string[]{
			"Blackish",
			"Nemirda",
			"Famis",
			"Mirith"
		};

		/// <summary>
		/// 2014.02.01
		/// Soft Drink Wars
		/// LVL1
		/// 
		/// Bring Item Objective
		/// Acquire these goods:
		/// Item	1 x Quafe Unleashed formula (0,1 m³)	
		/// 
		/// Objective
		/// Recover the Quafe Unleashed formula and return it to your agent.
		/// </summary>
		static public SictMissionStrategikon[] MengeMissionStrategikonSoftDrinkWars =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööreUndDurcsuuceCargo(
			MissionSoftDrinkWarsMengeEmployeeName.Select((EmployeeName) => SictStrategikonOverviewObjektFilter.FilterRatMitNaameRegexPattern(EmployeeName)),
			new SictStrategikonOverviewObjektFilter[]{
				SictStrategikonOverviewObjektFilter.FilterCargoContainer()}
				.Concat(
			MissionSoftDrinkWarsMengeEmployeeName.Select((EmployeeName) => SictStrategikonOverviewObjektFilter.FilterWreckMitNameRegex(EmployeeName)))),
				SictKonfigMissionTitel.MissionTitelSoftDrinkWars);

		static public SictMissionStrategikon[] MengeMissionStrategikonTheRovingRogueDrones =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööreAleRat(),
				SictKonfigMissionTitel.MissionTitelTheRovingRogueDrones);

		/// <summary>
		/// 2014.02.01
		/// Closing the Gate
		/// LVL1
		/// 
		/// Objective
		/// Destroy the mysterious jump gate and its guards.
		/// </summary>
		static public SictMissionStrategikon[] MengeMissionStrategikonClosingtheGate =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööre(
			new SictStrategikonOverviewObjektFilter[]{
				SictStrategikonOverviewObjektFilter.FilterRat(),
				new	SictStrategikonOverviewObjektFilter(SictOverviewObjektGrupeEnum.LargeCollidableStructure, ".", "smuggler stargate",	false),
				new	SictStrategikonOverviewObjektFilter(SictOverviewObjektGrupeEnum.LargeCollidableStructure, "smuggler stargate", null,	false),
			}),
				SictKonfigMissionTitel.MissionTitelClosingtheGate);

		/// <summary>
		/// 2014.02.01
		/// Avenge a Fallen Comrade
		/// LVL1
		/// 
		/// Objective
		/// Destroy the habitat of the pirate leaders then report back to your agent. 
		/// </summary>
		static public SictMissionStrategikon[] MengeMissionStrategikonAvengeAFallenComrade =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööre(
			new SictStrategikonOverviewObjektFilter[]{
				new	SictStrategikonOverviewObjektFilter(SictOverviewObjektGrupeEnum.DeadspaceStructure, "Habitat", "Habitat",	false),
			}),
				SictKonfigMissionTitel.MissionTitelAvengeAFallenComrade);

		/// <summary>
		/// 2014.02.01
		/// Carrying AIMEDs
		/// LVL1
		/// 
		/// Bring Item Objective
		/// Acquire these goods:
		/// Item	1 x AIMEDs (25,0 m³)	
		/// 
		/// Objective
		/// Pick up the AIMEDs and return them to your agent. 
		/// </summary>
		static public SictMissionStrategikon[] MengeMissionStrategikonCarryingAIMEDs =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööreUndDurcsuuceCargo(
			new KeyValuePair<SictStrategikonOverviewObjektFilter, SictInRaumObjektBearbaitungPrio>[]{
				new	KeyValuePair<SictStrategikonOverviewObjektFilter,	SictInRaumObjektBearbaitungPrio>(
					SictStrategikonOverviewObjektFilter.FilterRat(), new	SictInRaumObjektBearbaitungPrio()),
				new	KeyValuePair<SictStrategikonOverviewObjektFilter,	SictInRaumObjektBearbaitungPrio>(
					SictStrategikonOverviewObjektFilter.FilterRatMitNaameRegexPattern("Thief"), SictInRaumObjektBearbaitungPrio.KonstruktFürInGrupeIndex(4)),
			},
				SictStrategikonOverviewObjektFilter.FilterCargoContainer()),
				SictKonfigMissionTitel.MissionTitelCarryingAIMEDs);

		/// <summary>
		/// 2014.02.02
		/// Eliminate a Pirate Nuisance
		/// LVL1
		/// 
		/// Objective
		/// Eliminate the pirates causing the nuisance.
		/// 
		/// </summary>
		static public SictMissionStrategikon[] MengeMissionStrategikonEliminateAPirateNuisance =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööreAleRat(),
				SictKonfigMissionTitel.MissionTitelEliminateAPirateNuisance);

		/// <summary>
		/// 2014.02.02
		/// Violent Expulsion
		/// LVL1
		/// 
		/// Objective
		/// Destroy the Gurista vandals.
		/// </summary>
		static public SictMissionStrategikon[] MengeMissionStrategikonViolentExpulsion =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööreAleRat(),
				SictKonfigMissionTitel.MissionTitelViolentExpulsion);

		/// <summary>
		/// 2014.02.07
		/// Lost Records
		/// LVL1
		/// 
		/// Bring Item Objective
		/// Acquire these goods:
		/// Item	1 x Federation Court Logs (1,0 m³)	
		/// 
		/// Objective
		/// Locate the records, place them in your cargo hold and get it back to me here.
		/// </summary>
		static public SictMissionStrategikon[] MengeMissionStrategikonLostRecords =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonDurcsuuceCargo(
			SictStrategikonOverviewObjektFilter.FilterCargoContainer()),
				SictKonfigMissionTitel.MissionTitelLostRecords);

		/// <summary>
		/// 2014.02.08
		/// Trimming the Fat
		/// LVL2
		/// 
		/// Objective
		/// Eliminate the incompetent executive.
		/// </summary>
		static public SictMissionStrategikon[] MengeMissionStrategikonTrimmingTheFat =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööre(
			new KeyValuePair<SictStrategikonOverviewObjektFilter, SictInRaumObjektBearbaitungPrio>[]{
			new	KeyValuePair<SictStrategikonOverviewObjektFilter,	SictInRaumObjektBearbaitungPrio>(
				SictStrategikonOverviewObjektFilter.FilterRatMitNaameRegexPattern("executive"),	SictInRaumObjektBearbaitungPrio.KonstruktFürInGrupeIndex(4)),
			new	KeyValuePair<SictStrategikonOverviewObjektFilter,	SictInRaumObjektBearbaitungPrio>(
				SictStrategikonOverviewObjektFilter.FilterRat(),	new	SictInRaumObjektBearbaitungPrio()),
			}),
				SictKonfigMissionTitel.MissionTitelTrimmingTheFat);

		/// <summary>
		/// 2014.02.08
		/// The Drug Bust
		/// LVL2
		/// 
		/// Bring Item Objective
		/// Acquire these goods:
		/// Item	9 x Prisoner (9,0 m³)	
		/// 
		/// Objective
		/// Break up the drug deal and capture prisoners for questioning.
		/// </summary>
		static public SictMissionStrategikon[] MengeMissionStrategikonTheDrugBust =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööreAleRatUndDurcsuuceCargo(
			SictStrategikonOverviewObjektFilter.FilterCargoContainer()),
				SictKonfigMissionTitel.MissionTitelTheDrugBust);

		/// <summary>
		/// 2014.02.08
		/// Murderer Brought To Justice
		/// LVL2
		/// 
		/// Objective
		/// Find and destroy the murderer and his guards, then report back to your agent.
		/// </summary>
		static public SictMissionStrategikon[] MengeMissionStrategikonMurdererBroughtToJustice =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööreAleRat(),
				SictKonfigMissionTitel.MissionTitelMurdererBroughtToJustice);

		/// <summary>
		/// 2014.02.08
		/// Mission of Mercy
		/// LVL2
		/// 
		/// Objective
		/// Secure the Medical Drone and ensure the safe arrival of the repair teams.
		/// </summary>
		static public SictMissionStrategikon[] MengeMissionStrategikonMissionOfMercy =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööre(
			new KeyValuePair<SictStrategikonOverviewObjektFilter, SictInRaumObjektBearbaitungPrio>[]{
			new	KeyValuePair<SictStrategikonOverviewObjektFilter,	SictInRaumObjektBearbaitungPrio>(
				SictStrategikonOverviewObjektFilter.FilterRatMitNaameRegexPattern("Lieutenant"),	SictInRaumObjektBearbaitungPrio.KonstruktFürInGrupeIndex(4)),
			new	KeyValuePair<SictStrategikonOverviewObjektFilter,	SictInRaumObjektBearbaitungPrio>(
				SictStrategikonOverviewObjektFilter.FilterRat(),	new	SictInRaumObjektBearbaitungPrio()),
			}),
				SictKonfigMissionTitel.MissionTitelMissionOfMercy);

		/// <summary>
		/// 2014.02.09
		/// 
		/// LVL2
		/// 
		/// 
		/// </summary>
		static public SictMissionStrategikon[] MengeMissionStrategikonCustomsInterdiction_1_of_2 =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööreUndDurcsuuceCargo(
			new KeyValuePair<SictStrategikonOverviewObjektFilter, SictInRaumObjektBearbaitungPrio>[]{
			new	KeyValuePair<SictStrategikonOverviewObjektFilter,	SictInRaumObjektBearbaitungPrio>(
				SictStrategikonOverviewObjektFilter.FilterRatMitNaameRegexPattern("transport"),	SictInRaumObjektBearbaitungPrio.KonstruktFürInGrupeIndex(4)),
				/*
				 * 2014.02.09
				 * 
			new	KeyValuePair<SictStrategikonOverviewObjektFilter,	SictInRaumObjektBearbaitungPrio>(
				SictStrategikonOverviewObjektFilter.FilterRat(),	new	SictInRaumObjektBearbaitungPrio()),
				 * */
			},
			SictStrategikonOverviewObjektFilter.FilterCargoContainer()),
				SictKonfigMissionTitel.MissionTitelCustomsInterdiction_1_of_2);

		/// <summary>
		/// 2014.02.28
		/// Vengeance
		/// 
		/// LVL4
		/// 
		/// Objective
		/// Assassinate Rachen Mysuna and his entourage, then report back to your agent.
		/// </summary>
		static public SictMissionStrategikon[] MengeMissionStrategikonVengeance =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööreAleRat(),
				SictKonfigMissionTitel.MissionTitelVengeance);

		/// <summary>
		/// 2014.02.29
		/// Infiltrated Outposts
		/// 
		/// LVL4
		/// 
		/// Objective
		/// Destroy all rogue drone structures and the surrounding drones.
		/// </summary>
		static public SictMissionStrategikon[] MengeMissionStrategikonInfiltratedOutposts =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööre(
			new KeyValuePair<SictStrategikonOverviewObjektFilter, SictInRaumObjektBearbaitungPrio>[]{
				new	KeyValuePair<SictStrategikonOverviewObjektFilter,	SictInRaumObjektBearbaitungPrio>(
					new	SictStrategikonOverviewObjektFilter(SictOverviewObjektGrupeEnum.LargeCollidableStructure, "Drone Bunker", "Drone", false),
					SictInRaumObjektBearbaitungPrio.KonstruktFürInGrupeIndex(4)),
				new	KeyValuePair<SictStrategikonOverviewObjektFilter,	SictInRaumObjektBearbaitungPrio>(
					new	SictStrategikonOverviewObjektFilter(SictOverviewObjektGrupeEnum.LargeCollidableStructure, "Drone", "Drone Bunker", false),
					SictInRaumObjektBearbaitungPrio.KonstruktFürInGrupeIndex(4)),
				new	KeyValuePair<SictStrategikonOverviewObjektFilter,	SictInRaumObjektBearbaitungPrio>(
					SictStrategikonOverviewObjektFilter.FilterRat(),
					default(SictInRaumObjektBearbaitungPrio)),
				new	KeyValuePair<SictStrategikonOverviewObjektFilter,	SictInRaumObjektBearbaitungPrio>(
					new	SictStrategikonOverviewObjektFilter(SictOverviewObjektGrupeEnum.LargeCollidableStructure, "Drone", "Drone", false),
					SictInRaumObjektBearbaitungPrio.KonstruktFürInGrupeIndex(-11)),
					}),
				SictKonfigMissionTitel.MissionTitelInfiltratedOutposts);

		/// <summary>
		/// 2014.02.29
		/// Massive Attack
		/// 
		/// LVL4
		/// 
		/// Objective
		/// Kill all attacking forces.
		/// </summary>
		static public SictMissionStrategikon[] MengeMissionStrategikonMassiveAttack =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööreAleRat(),
				SictKonfigMissionTitel.MissionTitelMassiveAttack);

		/// <summary>
		/// 2014.04.29
		/// The Blood Raider Spies
		/// Vanulard Inard
		/// Level 2
		/// 
		/// Objective
		/// Eliminate the Blood Raider Spies.
		/// </summary>
		static public SictMissionStrategikon[] MengeMissionStrategikonTheBloodRaiderSpies =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööreAleRat(),
				SictKonfigMissionTitel.MissionTitelTheBloodRaiderSpies);

		/// <summary>
		/// 2014.04.29
		/// Attack the Angel Hideout
		/// Vanulard Inard
		/// Level 2
		/// 
		/// Objective
		/// Destroy the Mother-in-law and her escort.
		/// </summary>
		static public SictMissionStrategikon[] MengeMissionStrategikonAttackTheAngelHideout =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööreAleRat(),
				SictKonfigMissionTitel.MissionTitelAttackTheAngelHideout);

		/// <summary>
		/// 2014.04.29
		/// No Honor
		/// Vanulard Inard
		/// Level 2
		/// 
		/// Bring Item Objective
		/// Acquire these goods:
		/// Item
		/// 1 x Stolen Goods (80,0 m³)
		/// 
		/// Objective
		/// Destroy enemy ship; return stolen cargo to agent.
		/// </summary>
		static public SictMissionStrategikon[] MengeMissionStrategikonNoHonor =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööreUndDurcsuuceCargo(
			new KeyValuePair<SictStrategikonOverviewObjektFilter, SictInRaumObjektBearbaitungPrio>[]{
			new	KeyValuePair<SictStrategikonOverviewObjektFilter,	SictInRaumObjektBearbaitungPrio>(
				SictStrategikonOverviewObjektFilter.FilterRatMitNaameRegexPattern("Industrial"),	SictInRaumObjektBearbaitungPrio.KonstruktFürInGrupeIndex(4)),
			},
			SictStrategikonOverviewObjektFilter.FilterCargoContainer()),
				SictKonfigMissionTitel.MissionTitelNoHonor);

		/// <summary>
		/// 2014.05.01
		/// Small Misunderstanding (1 of 3)
		/// Welukenur Annodi
		/// Level 2
		/// 
		/// Objective
		/// Destroy the damaged Caldari ship.
		/// 
		/// (das "Caldari ship" scaint ain "Large Collidable Structure" zu sain. Type="static caracal navy issue", name="caldari ship")
		/// </summary>
		static public SictMissionStrategikon[] MengeMissionStrategikonSmallMisunderstanding_1_of_3 =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööre(
			new KeyValuePair<SictStrategikonOverviewObjektFilter, SictInRaumObjektBearbaitungPrio>[]{
			new	KeyValuePair<SictStrategikonOverviewObjektFilter,	SictInRaumObjektBearbaitungPrio>(
				new	SictStrategikonOverviewObjektFilter(SictOverviewObjektGrupeEnum.LargeCollidableStructure, "static", "caldari ship", false),
				new	SictInRaumObjektBearbaitungPrio()),
			}),
			SictKonfigMissionTitel.MissionTitelSmallMisunderstanding_1_of_3);

		/// <summary>
		/// 2014.05.01
		/// Small Misunderstanding (2 of 3)
		/// Welukenur Annodi
		/// Level 2
		/// 
		/// Bring Item Objective
		/// Acquire these goods:
		/// Item	1 x Ship logs (1,0 m³)	
		/// 
		/// Objective
		/// Destroy the Guristas; Recover the stolen ship log.
		/// </summary>
		static public SictMissionStrategikon[] MengeMissionStrategikonSmallMisunderstanding_2_of_3 =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööreAleRatUndDurcsuuceCargo(
				SictStrategikonOverviewObjektFilter.FilterCargoContainer()),
			SictKonfigMissionTitel.MissionTitelSmallMisunderstanding_2_of_3);

		/// <summary>
		/// 2014.05.01
		/// Small Misunderstanding (3 of 3)
		/// Welukenur Annodi
		/// Level 2
		/// 
		/// Objective
		/// Meet with the buyer; kill any Guristas.
		/// </summary>
		static public SictMissionStrategikon[] MengeMissionStrategikonSmallMisunderstanding_3_of_3 =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonFliigeAnUndZersctööre(
			new SictStrategikonOverviewObjektFilter(SictOverviewObjektGrupeEnum.LargeCollidableStructure, ".", "buyer", false),
			4000,
			SictStrategikonOverviewObjektFilter.FilterRat()),
			SictKonfigMissionTitel.MissionTitelSmallMisunderstanding_3_of_3);

		/// <summary>
		/// 2014.10.27
		/// An Ancient Roster
		/// Huslavard Alfikan
		/// Level 1
		/// 
		/// Objective
		/// Acquire these goods:
		/// Item:	1 x Brutor Tribe Roster (0.2 m³)
		/// </summary>
		static public SictMissionStrategikon[] MengeMissionStrategikonAnAncientRoster =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonDurcsuuceCargo(
			SictStrategikonOverviewObjektFilter.FilterCargoContainer()),
				SictKonfigMissionTitel.MissionTitelAnAncientRoster);

		/// <summary>
		/// 2014.10.28
		/// The Angel Cartel Spies
		/// Haftildar Honledok
		/// Level 2
		/// 
		/// Objective
		/// Destroy the Cartel spies at the bookmarked location.
		/// </summary>
		static public SictMissionStrategikon[] MengeMissionStrategikonTheAngelCartelSpies =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööreAleRat(),
				SictKonfigMissionTitel.MissionTitelTheAngelCartelSpies);

		/// <summary>
		/// 2014.10.29
		/// Drone Detritus
		/// Arrald Arilfer
		/// Level 1
		/// 
		/// Bring Item Objective
		/// Acquire these goods:
		/// Item	1 x Rogue Drone A.I. Core (0,0 m³)	
		/// </summary>
		static public SictMissionStrategikon[] MengeMissionStrategikonDroneDetritus =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööreAleRatUndDurcsuuceCargo(
				SictStrategikonOverviewObjektFilter.FilterCargoContainer()),
			SictKonfigMissionTitel.MissionTitelDroneDetritus);


		/// <summary>
		/// 2015.01.14
		/// Corporate Records
		/// 
		/// Niyabainen IX - Moon 21 - Hyasyoda Corporation Mining Outpost
		/// Aatvi Nupiroda
		/// Level 1
		/// 
		/// Objective
		/// Retrieve the jettisoned Ishokune corporate records.
		/// 
		/// Bring Item Objective
		/// Acquire these goods:
		/// Item	1 x Ishukone Corporate Records (0,1 m³)
		/// 
		/// </summary>
		static public SictMissionStrategikon[] MengeMissionStrategikonCorporateRecords =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonDurcsuuceCargo(
			SictStrategikonOverviewObjektFilter.FilterCargoContainer()),
				SictKonfigMissionTitel.MissionTitelCorporateRecords);


		/// <summary>
		/// 2015.02.22
		/// The Right Hand Of Zazzmatazz
		/// 
		/// Funtanainen IV - Moon 1 - Caldari Navy Logistic Support
		/// Asalova Iella
		/// Level 4
		/// 
		/// Objective
		/// Destroy the headquarters within the Seven's compound, as well as its guardian, 'Zor', and then report back to your agent.
		/// 
		/// </summary>
		static public SictMissionStrategikon[] MengeMissionStrategikonTheRightHandOfZazzmatazz =
			MengeMissionStrategikonAusMissionStrategikonMalMengeFaction(
			SictMissionStrategikon.StrategikonZersctööre(
			new	SictStrategikonOverviewObjektFilter[]
			{
				SictStrategikonOverviewObjektFilter.FilterRat(),
				new	SictStrategikonOverviewObjektFilter(SictOverviewObjektGrupeEnum.LargeCollidableStructure, ".", @"Outpost\s*Headquarter"),
			}),
			SictKonfigMissionTitel.MissionTitelTheRightHandOfZazzmatazz);


		static public SictMissionStrategikon[] MengeMissionStrategikon =
			Bib3.Glob.ArrayAusListeFeldGeflact(
			new SictMissionStrategikon[][]{
				MengeMissionStrategikonTheBlockade,
				MengeMissionStrategikonTheScore,
				MengeMissionStrategikonTheMissingConvoy,
				MengeMissionStrategikonTheGoodWord,
				MengeMissionStrategikonInterceptTheSaboteurs,
				MengeMissionStrategikonWhatComesAroundGoesAround,
				MengeMissionStrategikonPirateInvasion,
				MengeMissionStrategikonPirateIntrusion,
				MengeMissionStrategikonTheDamselInDistress,
				MengeMissionStrategikonEliminateThePirateCampers,
				MengeMissionStrategikonSmugglerInterception,
				MengeMissionStrategikonRetribution,
				MengeMissionStrategikonBreakTheirWill,
				MengeMissionStrategikonStopTheThief,
				MengeMissionStrategikonSuccessComesAtAPrice,
				MengeMissionStrategikonTheSpaceTelescope,
				MengeMissionStrategikonTechnologicalSecrets1of3,
				MengeMissionStrategikonTheRogueSlaveTrader,
				MengeMissionStrategikonDowningtheSlavers2of2,
				MengeMissionStrategikonTheGuristasSpies,
				MengeMissionStrategikonSilenceTheInformant,
				MengeMissionStrategikonGoneBerserk,
				MengeMissionStrategikonSeekandDestroy,
				MengeMissionStrategikonRogueDroneHarassment,
				MengeMissionStrategikonTheDroneInfestation,
				MengeMissionStrategikonCargoDelivery,
				MengeMissionStrategikonTheSevensPrisonFacility,
				MengeMissionStrategikonDuoOfDeath,
				MengeMissionStrategikonTheSpyStash,
				MengeMissionStrategikonTheHiddenStash,
				MengeMissionStrategikonUnauthorizedMilitaryPresence,
				MengeMissionStrategikonTheBlackMarketHub,
				MengeMissionStrategikonGuristasExtravaganza,
				MengeMissionStrategikonAngelExtravaganza,
				MengeMissionStrategikonInterceptThePirateSmugglers,
				MengeMissionStrategikonTheAssault,
				MengeMissionStrategikonSaveAMansCareer,
				MengeMissionStrategikonTheSevensBrothel,
				MengeMissionStrategikonFurrierFiasco,
				MengeMissionStrategikonPirateAggression,
				MengeMissionStrategikonMysteriousSightings_1_of_4,
				MengeMissionStrategikonAlluringEmanations,
				MengeMissionStrategikonTheDisgruntledEmployee,
				MengeMissionStrategikonAfterTheSeven_1_of_5,
				MengeMissionStrategikonAfterTheSeven_2_of_5,
				MengeMissionStrategikonAfterTheSeven_3_of_5,
				MengeMissionStrategikonTheSerpentisSpies,
				MengeMissionStrategikonTheMordusHeadhunters,
				MengeMissionStrategikonSoftDrinkWars,
				MengeMissionStrategikonTheRovingRogueDrones,
				MengeMissionStrategikonClosingtheGate,
				MengeMissionStrategikonAvengeAFallenComrade,
				MengeMissionStrategikonCarryingAIMEDs,
				MengeMissionStrategikonEliminateAPirateNuisance,
				MengeMissionStrategikonViolentExpulsion,
				MengeMissionStrategikonLostRecords,
				MengeMissionStrategikonTrimmingTheFat,
				MengeMissionStrategikonTheDrugBust,
				MengeMissionStrategikonMurdererBroughtToJustice,
				MengeMissionStrategikonMissionOfMercy,
				MengeMissionStrategikonVengeance,
				MengeMissionStrategikonInfiltratedOutposts,
				MengeMissionStrategikonMassiveAttack,
				MengeMissionStrategikonTheBloodRaiderSpies,
				MengeMissionStrategikonAttackTheAngelHideout,
				MengeMissionStrategikonNoHonor,
				MengeMissionStrategikonSmallMisunderstanding_1_of_3,
				MengeMissionStrategikonSmallMisunderstanding_2_of_3,
				MengeMissionStrategikonSmallMisunderstanding_3_of_3,
				MengeMissionStrategikonAnAncientRoster,
				MengeMissionStrategikonTheAngelCartelSpies,
				MengeMissionStrategikonDroneDetritus,
				MengeMissionStrategikonCorporateRecords,
				MengeMissionStrategikonTheRightHandOfZazzmatazz,
			});
	}

}
