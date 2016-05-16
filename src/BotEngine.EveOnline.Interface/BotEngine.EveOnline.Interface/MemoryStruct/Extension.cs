using System;
using System.Linq;
using System.Collections.Generic;

namespace BotEngine.EveOnline.Interface.MemoryStruct
{
	static public class Extension
	{
		static public ObjectIdInMemory AsObjectIdInMemory(this Int64 Id)
		{
			return new ObjectIdInMemory(new ObjectIdInt64(Id));
		}

		static public IEnumerable<object> EnumerateReferencedTransitive(
			this object Parent) =>
			Bib3.RefNezDiferenz.Extension.EnumMengeRefAusNezAusWurzel(Parent, FromSensorToConsumerMessage.UITreeComponentTypeHandlePolicyCache);

		static public IEnumerable<UIElement> EnumerateReferencedUIElementTransitive(
			this object Parent) =>
			EnumerateReferencedTransitive(Parent)
			?.OfType<UIElement>();

		static public T CopyByPolicyMemoryMeasurement<T>(this T ToBeCopied)
			where T : class =>
			//	Bib3.SictRefBaumKopii.ObjektKopiiErsctele(ToBeCopied, new Bib3.SictRefBaumKopiiParam(null, FromSensorToConsumerMessage.SerialisPolicyCache));
			Bib3.SictRefNezKopii.ObjektKopiiErsctele(ToBeCopied, new Bib3.RefBaumKopii.Param(null, FromSensorToConsumerMessage.SerialisPolicyCache), null); //	This should work as well....
	}
}
