using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bib3;
using Newtonsoft.Json;


namespace Optimat.EveOnline.VonSensor
{
	public class FarbeHueSatVal
	{
		public int? HMilli;

		public int? SMilli;

		public int? VMilli;

		public FarbeHueSatVal()
		{
		}

		public FarbeHueSatVal(
			int? HMilli,
			int? SMilli,
			int? LMilli)
		{
			this.HMilli = HMilli;
			this.SMilli = SMilli;
			this.VMilli = LMilli;
		}

		static public int? HueDistanzMiliBerecne(
			FarbeHueSatVal O0,
			FarbeHueSatVal O1)
		{
			if (null == O0 || null == O1)
			{
				return null;
			}

			var DistanzMili = (((O0.HMilli + 500) - O1.HMilli) % 1000) - 500;

			return DistanzMili;
		}
	}

	public struct FarbeARGBVal
	{
		public int? AMilli;

		public int? RMilli;

		public int? GMilli;

		public int? BMilli;

		public bool AleUnglaicNul()
		{
			return AMilli.HasValue && RMilli.HasValue && GMilli.HasValue && BMilli.HasValue;
		}

		public FarbeARGBVal(
			int? AMilli,
			int? RMilli,
			int? GMilli,
			int? BMilli)
		{
			this.AMilli = AMilli;
			this.RMilli = RMilli;
			this.GMilli = GMilli;
			this.BMilli = BMilli;
		}

		override public string ToString()
		{
			return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
		}
	}

	public class FarbeARGB
	{
		public int? AMilli;

		public int? RMilli;

		public int? GMilli;

		public int? BMilli;

		public bool AleUnglaicNul()
		{
			return AMilli.HasValue && RMilli.HasValue && GMilli.HasValue && BMilli.HasValue;
		}

		public FarbeARGB()
		{
		}

		public FarbeARGB(FarbeARGBVal? FarbeVal)
			:
			this(
			FarbeVal.HasValue ? FarbeVal.Value.AMilli : null,
			FarbeVal.HasValue ? FarbeVal.Value.RMilli : null,
			FarbeVal.HasValue ? FarbeVal.Value.GMilli : null,
			FarbeVal.HasValue ? FarbeVal.Value.BMilli : null)
		{
		}

		static public FarbeARGB VonVal(FarbeARGBVal? FarbeValNulbar)
		{
			if (!FarbeValNulbar.HasValue)
			{
				return null;
			}

			return new FarbeARGB(FarbeValNulbar);
		}

		public FarbeARGB(
			int? AMilli,
			int? RMilli,
			int? GMilli,
			int? BMilli)
		{
			this.AMilli = AMilli;
			this.RMilli = RMilli;
			this.GMilli = GMilli;
			this.BMilli = BMilli;
		}

		override public string ToString()
		{
			return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
		}

		static public bool Glaicwertig(
			FarbeARGB O0,
			FarbeARGB O1)
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
				O0.AMilli == O1.AMilli &&
				O0.RMilli == O1.RMilli &&
				O0.GMilli == O1.GMilli &&
				O0.BMilli == O1.BMilli;
		}

		static public int? HueDistanzMiliBerecne(
			FarbeARGB O0,
			FarbeARGB O1)
		{
			if (null == O0 || null == O1)
			{
				return null;
			}

			var O0HSV = O0.VonRGBNaacHueSatVal();
			var O1HSV = O1.VonRGBNaacHueSatVal();

			return FarbeHueSatVal.HueDistanzMiliBerecne(O0HSV, O1HSV);
		}

		public FarbeHueSatVal VonRGBNaacHueSatVal()
		{
			var RMilli = this.RMilli;
			var GMilli = this.GMilli;
			var BMilli = this.BMilli;

			if (!RMilli.HasValue || !GMilli.HasValue || !BMilli.HasValue)
			{
				return new FarbeHueSatVal();
			}

			int HMilli, SMilli, VMilli;

			Optimat.Glob.RGBKonvertiirtNaacHueSatVal(
				RMilli.Value,
				GMilli.Value,
				BMilli.Value,
				1000,
				out	HMilli,
				out	SMilli,
				out	VMilli);

			return new FarbeHueSatVal(HMilli, SMilli, VMilli);
		}
	}

	public class GbsElement : ObjektMitIdentInt64
	{
		/*
		 * 
		readonly static SictIdentInt64Fabrik TempDebugIdentFabrik = new SictIdentInt64Fabrik(1000);

		readonly Int64 TempDebugIdent;
		 * */

		public OrtogoonInt InGbsFläce;

		/*
		 * 2015.02.17
		 * 
		/// <summary>
		/// klainerer Wert isc waiter Vorne (höhere Sictbarkait)
		/// </summary>
		readonly public int? InGbsParentChildIndex;
		 * */

		/// <summary>
		/// klainerer Wert isc waiter Vorne (höhere Sictbarkait)
		/// </summary>
		readonly public int? InGbsBaumAstIndex;

		public Int64 GrenzeLinx
		{
			get
			{
				return InGbsFläce.Min0;
			}
		}
		public Int64 GrenzeRecz
		{
			get
			{
				return InGbsFläce.Max0;
			}
		}

		/// <summary>
		/// Tailfläce welce bevorzuugt verwendet werde sol wen versuuct werd ain Kontextmenu zu diise Ast zu ersctele.
		/// </summary>
		/// <returns></returns>
		virtual public GbsElement FläceMenuWurzelBerecne()
		{
			return this;
		}

		public GbsElement()
			:
			this((GbsElement)null)
		{
		}

		public GbsElement(
			GbsElement ZuKopiire)
			:
			this(
			ZuKopiire,
			(null == ZuKopiire) ? OrtogoonInt.Leer : ZuKopiire.InGbsFläce,
			(null == ZuKopiire) ? null : ZuKopiire.InGbsBaumAstIndex)
		{
		}

		public GbsElement(
			ObjektMitIdentInt64 ZuKopiire,
			OrtogoonInt InGbsFläce = default(OrtogoonInt),
			int? InGbsBaumAstIndex	= null)
			:
			base(ZuKopiire)
		{
			//	TempDebugIdent = TempDebugIdentFabrik.IdentBerecne();

			this.InGbsFläce = InGbsFläce;

			this.InGbsBaumAstIndex = InGbsBaumAstIndex;
		}
	}

	public class GbsElementMitBescriftung : GbsElement
	{
		readonly public string Bescriftung;

		public GbsElementMitBescriftung(
			GbsElement GbsElement,
			string Bescriftung = null)
			:
			base(GbsElement)
		{
			this.Bescriftung = Bescriftung;
		}

		public GbsElementMitBescriftung(
			GbsElementMitBescriftung ZuKopiire)
			:
			base(ZuKopiire)
		{
			if (null != ZuKopiire)
			{
				this.Bescriftung = ZuKopiire.Bescriftung;
			}
		}

		public GbsElementMitBescriftung()
		{
		}
	}

}
