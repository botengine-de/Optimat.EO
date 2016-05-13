using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
//using Optimat.EveOnline.AuswertGbs;
using Optimat.EveOnline.VonSensor;

namespace Optimat.EveOnline.Anwendung.AuswertGbs
{
	[JsonObject(MemberSerialization.OptIn)]
	public class ShipUiIndicationAuswert
	{
		[JsonProperty]
		readonly public ShipUiIndication ShipUIIndication;

		[JsonProperty]
		public string InRaumObjektNaame
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictZuInRaumObjektManööverTypEnum? ManöverTyp
		{
			private set;
			get;
		}

		[JsonProperty]
		public string DistanceSictString
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? Distance
		{
			private set;
			get;
		}

		public bool NictLeer()
		{
			var ShipUIIndication = this.ShipUIIndication;

			if (null == ShipUIIndication)
			{
				return false;
			}

			return
				!Bib3.Extension.NullOderLeer(ShipUIIndication.IndicationCaption) ||
				!Bib3.Extension.NullOderLeer(ShipUIIndication.IndicationText);
		}

		public ShipUiIndicationAuswert()
		{
		}

		public ShipUiIndicationAuswert(
			ShipUiIndication ShipUIIndication)
		{
			this.ShipUIIndication = ShipUIIndication;

			Berecne();
		}

		static readonly public string IndicationTextInRaumObjektNaameUndDistanceRegexPatternVersioon0 = "(.*)\\s*\\-\\s*(" +
			TempAuswertGbs.Extension.DistanceRegexPattern + ")";

		static readonly public IEnumerable<KeyValuePair<string, SictZuInRaumObjektManööverTypEnum>> MengeZuRegexPatternManöverTyp = MengeZuRegexPatternManöverTypBerecne();

		static public IEnumerable<KeyValuePair<string, SictZuInRaumObjektManööverTypEnum>> MengeZuRegexPatternManöverTypBerecne()
		{
			return new KeyValuePair<string, SictZuInRaumObjektManööverTypEnum>[]{
				new	KeyValuePair<string, SictZuInRaumObjektManööverTypEnum>("Stop", SictZuInRaumObjektManööverTypEnum.Stop),
				new	KeyValuePair<string, SictZuInRaumObjektManööverTypEnum>("Approach", SictZuInRaumObjektManööverTypEnum.Approach),
				new	KeyValuePair<string, SictZuInRaumObjektManööverTypEnum>("Orbit", SictZuInRaumObjektManööverTypEnum.Orbit),
				new	KeyValuePair<string, SictZuInRaumObjektManööverTypEnum>("keep.*at.*range", SictZuInRaumObjektManööverTypEnum.KeepAtRange),
				new	KeyValuePair<string, SictZuInRaumObjektManööverTypEnum>("Warp", SictZuInRaumObjektManööverTypEnum.Warp),
				new	KeyValuePair<string, SictZuInRaumObjektManööverTypEnum>("Jump", SictZuInRaumObjektManööverTypEnum.Jump),
				new	KeyValuePair<string, SictZuInRaumObjektManööverTypEnum>("Dock", SictZuInRaumObjektManööverTypEnum.Dock)};
		}

		void Berecne()
		{
			string InRaumObjektNaame = null;
			SictZuInRaumObjektManööverTypEnum? ManöverTyp = null;
			string DistanceSictString = null;
			Int64? Distance = null;

			try
			{
				var ShipUIIndication = this.ShipUIIndication;

				if (null == ShipUIIndication)
				{
					return;
				}

				var IndicationCaption = ShipUIIndication.IndicationCaption;
				var IndicationText = ShipUIIndication.IndicationText;

				if (Bib3.Extension.NullOderLeer(IndicationCaption))
				{
					return;
				}

				if (null != MengeZuRegexPatternManöverTyp)
				{
					foreach (var ZuRegexPatternManöverTyp in MengeZuRegexPatternManöverTyp)
					{
						var RegexPattern = ZuRegexPatternManöverTyp.Key;

						if (null == RegexPattern)
						{
							continue;
						}

						var Match = Regex.Match(IndicationCaption, RegexPattern, RegexOptions.IgnoreCase);

						if (!Match.Success)
						{
							continue;
						}

						ManöverTyp = ZuRegexPatternManöverTyp.Value;
					}
				}

				if (Bib3.Extension.NullOderLeer(IndicationText))
				{
					return;
				}

				var InRaumObjektNaameUndDistanceMatch = Regex.Match(IndicationText, IndicationTextInRaumObjektNaameUndDistanceRegexPatternVersioon0);

				if (InRaumObjektNaameUndDistanceMatch.Success)
				{
					var KandidaatDistanceSictString = InRaumObjektNaameUndDistanceMatch.Groups[2].Value;

					var KandidaatDistance = TempAuswertGbs.Extension.AusDistanceSictStringAbbildGanzzaalDurcMeeterScrankeMax(
						KandidaatDistanceSictString);

					if (KandidaatDistance.HasValue)
					{
						DistanceSictString = KandidaatDistanceSictString;
						Distance = KandidaatDistance;
						InRaumObjektNaame = Bib3.Glob.TrimNullable(InRaumObjektNaameUndDistanceMatch.Groups[1].Value);
					}
				}
			}
			finally
			{
				this.InRaumObjektNaame = InRaumObjektNaame;
				this.ManöverTyp = ManöverTyp;
				this.DistanceSictString = DistanceSictString;
				this.Distance = Distance;
			}
		}
	}

}
