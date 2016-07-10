using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimat.EveOnline
{
	static	public	class WorldConfig
	{
		static public KeyValuePair<SictFactionSictEnum, Int64>[] MengeFürFactionLogoBezaicer =
			new KeyValuePair<SictFactionSictEnum, Int64>[]{
				new	KeyValuePair<SictFactionSictEnum,	Int64>(SictFactionSictEnum.Guristas_Pirates, 500010),
				new	KeyValuePair<SictFactionSictEnum,	Int64>(SictFactionSictEnum.Angel_Cartel, 500011),
				new	KeyValuePair<SictFactionSictEnum,	Int64>(SictFactionSictEnum.The_Blood_Raider_Covenant, 500012),
				new	KeyValuePair<SictFactionSictEnum,	Int64>(SictFactionSictEnum.Mordus_Legion, 500018),
				new	KeyValuePair<SictFactionSictEnum,	Int64>(SictFactionSictEnum.Sanshas_Nation, 500019),
				new	KeyValuePair<SictFactionSictEnum,	Int64>(SictFactionSictEnum.Serpentis, 500020),
			};

		static public Int64? FürFactionLogoBezaicer(SictFactionSictEnum Faction)
		{
			var MengeLogoBezaicner =
				MengeFürFactionLogoBezaicer
				.Where((Kandidaat) => Kandidaat.Key == Faction)
				.ToArray();

			if (MengeLogoBezaicner.Length < 1)
			{
				return null;
			}

			return MengeLogoBezaicner.FirstOrDefault().Value;
		}

		static public SictFactionSictEnum? AusFactionLogoIdBerecneFaction(Int64 FactionLogoId)
		{
			return
				MengeFürFactionLogoBezaicer.Select((FürFactionLogoBezaicer) =>
				new KeyValuePair<SictFactionSictEnum?, Int64>(FürFactionLogoBezaicer.Key, FürFactionLogoBezaicer.Value))
				.FirstOrDefault((Kandidaat) => Kandidaat.Value == FactionLogoId)
				.Key;
		}

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
	}
}
