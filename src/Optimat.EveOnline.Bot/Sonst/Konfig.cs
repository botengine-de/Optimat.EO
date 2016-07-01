using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimat.EveOnline.Anwendung
{
	public	class SictKonfig
	{
		static public readonly KeyValuePair<SictDamageTypeSictEnum, string>[] MengeDamageTypAmmoRegexPattern =
			new KeyValuePair<SictDamageTypeSictEnum, string>[]{
				new	KeyValuePair<SictDamageTypeSictEnum,	string>(SictDamageTypeSictEnum.EM, "Mjolnir.*Missile"),
				new	KeyValuePair<SictDamageTypeSictEnum,	string>(SictDamageTypeSictEnum.Kin, "Scourge.*Missile"),
				new	KeyValuePair<SictDamageTypeSictEnum,	string>(SictDamageTypeSictEnum.Therm, "Inferno.*Missile"),
				new	KeyValuePair<SictDamageTypeSictEnum,	string>(SictDamageTypeSictEnum.Exp, "Nova.*Missile"),
				new	KeyValuePair<SictDamageTypeSictEnum,	string>(SictDamageTypeSictEnum.EM, "Mjolnir.*Torp"),
				new	KeyValuePair<SictDamageTypeSictEnum,	string>(SictDamageTypeSictEnum.Kin, "Scourge.*Torp"),
				new	KeyValuePair<SictDamageTypeSictEnum,	string>(SictDamageTypeSictEnum.Therm, "Inferno.*Torp"),
				new	KeyValuePair<SictDamageTypeSictEnum,	string>(SictDamageTypeSictEnum.Exp, "Nova.*Torp"),
			};
	}
}
