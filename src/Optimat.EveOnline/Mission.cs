using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bib3;
using Newtonsoft.Json;
using Optimat.EveOnline.VonSensor;

namespace Optimat.EveOnline
{
	public enum SictMissionZaitraumTypSictEnum
	{
		Kain = 0,
		VonSictungFrühesteBisLezte,
		VonAcceptBisComplete,
		VonFüüreAusBeginBisComplete,
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictProzesMitBeginZaitUndEndeEraignis<TEndeEraignis>
	{
		[JsonProperty]
		readonly public Int64? BeginZait;

		[JsonProperty]
		public SictWertMitZait<TEndeEraignis>? Ende
		{
			private set;
			get;
		}

		public Int64? EndeZait
		{
			get
			{
				var Ende = this.Ende;

				if (!Ende.HasValue)
				{
					return null;
				}

				return Ende.Value.Zait;
			}
		}

		public SictProzesMitBeginZaitUndEndeEraignis()
		{
		}

		public SictProzesMitBeginZaitUndEndeEraignis(
			Int64? BeginZait)
		{
			this.BeginZait = BeginZait;
		}

		public void EndeSeze(SictWertMitZait<TEndeEraignis> Ende)
		{
			if (this.Ende.HasValue)
			{
				return;
			}

			this.Ende = Ende;
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictMissionLocationRaumEnde
	{
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictMissionLocationPfaadEnde
	{
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictMissionLocationPfaad : SictProzesMitBeginZaitUndEndeEraignis<SictMissionLocationPfaadEnde>
	{
		[JsonProperty]
		public List<SictMissionLocationRaum> ListeRaum;

		public SictMissionLocationRaum BeginRaum
		{
			get
			{
				return ListeRaum.FirstOrDefaultNullable();
			}
		}

		public SictAusGbsLocationInfo BeginRaumBeginLocation
		{
			get
			{
				var BeginRaum = this.BeginRaum;

				if (null == BeginRaum)
				{
					return null;
				}

				return BeginRaum.BeginLocation;
			}
		}

		public string BeginRaumBeginLocationSolarSystemName
		{
			get
			{
				var BeginRaumBeginLocation = this.BeginRaumBeginLocation;

				if (null == BeginRaumBeginLocation)
				{
					return null;
				}

				return BeginRaumBeginLocation.SolarSystemName;
			}
		}

		public SictMissionLocationPfaad()
		{
		}

		public SictMissionLocationPfaad(
			Int64? BeginZait)
			:
			base(BeginZait)
		{
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictMissionLocationRaum : SictProzesMitBeginZaitUndEndeEraignis<SictMissionLocationRaumEnde>
	{
		[JsonProperty]
		readonly public SictAusGbsLocationInfo BeginLocation;

		public SictMissionLocationRaum()
		{
		}

		public SictMissionLocationRaum(
			Int64? BeginZait,
			SictAusGbsLocationInfo BeginLocation)
			:
			base(BeginZait)
		{
			this.BeginLocation = BeginLocation;
		}
	}

	public class SictMissionZuusctand : ObjektMitIdentInt64
	{
		/// <summary>
		/// Frühester Zaitpunkt zu Mission gesictet wurde.
		/// </summary>
		public Int64? SictungFrühesteZaitMili;

		/// <summary>
		/// Frühester Zaitpunkt zu dem das Offer gesictet wurde.
		/// </summary>
		public Int64? OfferFrühesteZaitMili;

		/// <summary>
		/// Frühester Zaitpunkt zu dem erkant wurde das Mission Accepted ist.
		/// </summary>
		public Int64? AcceptedFrühesteZaitMili;

		/// <summary>
		/// früheste Zaitpunkt zu dem erkant wurde das Mission nict meer verfüügbar (InfoPanel).
		/// </summary>
		public Int64? EndeZaitMili;

		/// <summary>
		/// früheste Zaitpunkt zu welcem Automaat ainen Handlungsvorsclaag zur ausfüürung der Mission berecnet hat.
		/// </summary>
		public Int64? AktioonFüüreAusFrühesteZaitMili;

		/// <summary>
		/// wurde in lezte Optimat Scrit bearbaitet.
		/// </summary>
		public bool? AktioonFüüreAusAktiiv;

		public bool? VersuucFittingAktiiv;

		/// <summary>
		/// Ende wurde durc Complete verursact.
		/// </summary>
		public bool? CompleteErfolg;

		/// <summary>
		/// Ende wurde durc Decline verursact.
		/// </summary>
		public bool? DeclineErfolg;

		public string Titel;

		/// <summary>
		/// 2013.09.04 Beobactung Feeler in EVE Odyssey Version: 8.32.616505:
		/// bai Verwendung von "Read Details" in Utilmenu Mission werd falscer Agent-Name angezaigt wen beraits
		/// vorher ain Fenster diises Typ mit ainer Mission von anderem Agent geöfnet war.
		/// </summary>
		public string AgentName;

		public int? AgentLevel;

		public MissionLocation AgentLocation;

		public string FürMissionFittingBezaicner;

		public bool? ConstraintFittingSatisfied;

		public SictFactionSictEnum[] ObjectiveMengeFaction;

		public SictWertMitZait<MissionInfo> MissionInfoLezte;

		/// <summary>
		/// Minimum aus Menge der Security Level aler in Mission verwendete Location.
		/// </summary>
		public int? SecurityLevelMinimumMili;

		public int? SecurityLevelMinimumIncludingRouteMili;

		public bool? RequiresDestruktion;

		public bool? RequiresMining;

		public Int64? RequiredCargoCapacityMili;

		public List<SictMissionLocationPfaad> ListeLocationPfaad;

		public SictKonfigMissionZuMissionFilterVerhalte AusPräferenzEntscaidungVerhalte;

		public SictMissionZuusctand()
		{
		}

		public SictMissionZuusctand(
			Int64? Bezaicner,
			Int64? SictungFrühesteZaitMili,
			Int64? OfferFrühesteZaitMili,
			string MissionTitel,
			string AgentName,
			int? AgentLevel,
			MissionLocation AgentLocation = null)
			:
			base(Bezaicner	?? -1)
		{
			this.SictungFrühesteZaitMili = SictungFrühesteZaitMili;
			this.OfferFrühesteZaitMili = OfferFrühesteZaitMili;
			this.Titel = MissionTitel;
			this.AgentName = AgentName;
			this.AgentLevel = AgentLevel;
			this.AgentLocation = AgentLocation;
		}

		public bool IstAkzeptiirtUndNictBeendet()
		{
			if (!AcceptedFrühesteZaitMili.HasValue)
			{
				return false;
			}

			if (EndeZaitMili.HasValue)
			{
				return false;
			}

			return true;
		}

		public KeyValuePair<Int64?, Int64?> FürZaitraumTypBerecneBeginUndEnde(
			SictMissionZaitraumTypSictEnum ZaitraumTyp)
		{
			Int64? ZaitraumBegin;
			Int64? ZaitraumEnde;

			FürZaitraumTypBerecneBeginUndEnde(ZaitraumTyp, out	ZaitraumBegin, out	ZaitraumEnde);

			return new KeyValuePair<Int64?, Int64?>(ZaitraumBegin, ZaitraumEnde);
		}

		static public void FürZaitraumTypBerecneBeginUndEnde(
			SictMissionZuusctand Mission,
			SictMissionZaitraumTypSictEnum ZaitraumTyp,
			out	Int64? ZaitraumBegin,
			out	Int64? ZaitraumEnde)
		{
			ZaitraumBegin = null;
			ZaitraumEnde = null;

			if (null == Mission)
			{
				return;
			}

			Mission.FürZaitraumTypBerecneBeginUndEnde(ZaitraumTyp, out	ZaitraumBegin, out	ZaitraumEnde);
		}

		public void FürZaitraumTypBerecneBeginUndEnde(
			SictMissionZaitraumTypSictEnum ZaitraumTyp,
			out	Int64? ZaitraumBegin,
			out	Int64? ZaitraumEnde)
		{
			ZaitraumBegin = null;
			ZaitraumEnde = null;

			switch (ZaitraumTyp)
			{
				case SictMissionZaitraumTypSictEnum.VonSictungFrühesteBisLezte:
					ZaitraumBegin = SictungFrühesteZaitMili;
					ZaitraumEnde = EndeZaitMili;
					break;
				case SictMissionZaitraumTypSictEnum.VonAcceptBisComplete:
					ZaitraumBegin = AcceptedFrühesteZaitMili;
					if (true == CompleteErfolg)
					{
						ZaitraumEnde = EndeZaitMili;
					}
					break;
				case SictMissionZaitraumTypSictEnum.VonFüüreAusBeginBisComplete:
					ZaitraumBegin = AktioonFüüreAusFrühesteZaitMili;
					if (true == CompleteErfolg)
					{
						ZaitraumEnde = EndeZaitMili;
					}
					break;
			}
		}

		public SictMissionZuusctand Kopii()
		{
			return SictRefBaumKopii.ObjektKopiiErsctele(this);
		}

		override public string ToString()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}
