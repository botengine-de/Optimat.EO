using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Bib3;
using Newtonsoft.Json;
//using Optimat.EveOnline.AuswertGbs;
using Optimat.EveOnline.Base;
using ExtractFromOldAssembly.Bib3;

namespace Optimat.EveOnline.Anwendung
{
	[JsonObject(MemberSerialization.OptIn)]
	public class SictOverviewObjektFilterTypeUndName : SictObjektIdentitäätPerTypeUndNameRegex
	{
		/// <summary>
		/// Menge der Overview Grupe in welcen das Objekt auftauce kan.
		/// </summary>
		[JsonProperty]
		public SictOverviewObjektGrupeEnum[] MengeGrupeZuDurcsuuce;

		public SictOverviewObjektFilterTypeUndName()
		{
		}

		public SictOverviewObjektFilterTypeUndName(
			SictOverviewObjektGrupeEnum[] MengeGrupeZuDurcsuuce,
			string ObjektTypeRegexPattern,
			string ObjektNameRegexPattern = null)
			:
			base(ObjektTypeRegexPattern, ObjektNameRegexPattern)
		{
			this.MengeGrupeZuDurcsuuce = MengeGrupeZuDurcsuuce;
		}

		public SictOverviewObjektFilterTypeUndName(
			SictOverviewObjektGrupeEnum GrupeZuDurcsuuce,
			string ObjektTypeRegexPattern,
			string ObjektNameRegexPattern = null)
			:
			this(new SictOverviewObjektGrupeEnum[] { GrupeZuDurcsuuce },	ObjektTypeRegexPattern, ObjektNameRegexPattern)
		{
		}

		/*
		 * 2014.04.26
		 * 
		static public SictOverviewObjektFilterTypeUndName FilterAccelerationGate()
		{
			var TypeUndNameFilterAccGate = SictMissionRaumZuusctandEndeScnapscus.FilterAccGate;

			return new SictOverviewObjektFilterTypeUndName(SictOverviewObjektGrupeEnum.AccelerationGate, TypeUndNameFilterAccGate.ObjektTypeRegexPattern, TypeUndNameFilterAccGate.ObjektNameRegexPattern);
		}
		 * */

		static public SictOverviewObjektFilterTypeUndName[] MengeFilterAccelerationGate()
		{
			var MengeTypeUndNameFilterAccGate = SictMissionZuusctand.MengeFilterAccGate ;

			return
				MengeTypeUndNameFilterAccGate.Select((TypeUndNameFilterAccGate)	=>
				new SictOverviewObjektFilterTypeUndName(
					SictOverviewObjektGrupeEnum.AccelerationGate,
					TypeUndNameFilterAccGate.ObjektTypeRegexPattern,
					TypeUndNameFilterAccGate.ObjektNameRegexPattern)).ToArray();
		}

		static public SictOverviewObjektFilterTypeUndName FilterCargoContainer()
		{
			return new SictOverviewObjektFilterTypeUndName(SictOverviewObjektGrupeEnum.CargoContainer, "cargo container", "cargo");
		}

		static public SictOverviewObjektFilterTypeUndName FilterWreckMitNameRegex(string NameRegex = null)
		{
			return new SictOverviewObjektFilterTypeUndName(SictOverviewObjektGrupeEnum.Wreck, "wreck", NameRegex);
		}

		readonly	static	public	string	WreckTypeRegex	= "Wreck";

		static public SictOverviewObjektFilterTypeUndName[] MengeFilterWreckMitTypeRegex(string TypeRegex = null)
		{
			var MengeTypeRegex = new string[]{
				WreckTypeRegex	+ ".*" + TypeRegex,
				TypeRegex	+	".*"	+ WreckTypeRegex,};

			return
				MengeTypeRegex
				.Select((TypeRegexPattern) => new SictOverviewObjektFilterTypeUndName(SictOverviewObjektGrupeEnum.Wreck, TypeRegexPattern, null)).ToArray();
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictObjektIdentitäätPerTypeUndNameRegex
	{
		[JsonProperty]
		public string ObjektTypeRegexPattern;

		[JsonProperty]
		public string ObjektNameRegexPattern;

		public SictObjektIdentitäätPerTypeUndNameRegex()
			:
			this(null)
		{
		}

		public SictObjektIdentitäätPerTypeUndNameRegex(
			string ObjektTypeRegexPattern,
			string ObjektNameRegexPattern = null)
		{
			this.ObjektTypeRegexPattern = ObjektTypeRegexPattern;
			this.ObjektNameRegexPattern = ObjektNameRegexPattern;
		}

		override public string ToString()
		{
			return JsonConvert.SerializeObject(this, Formatting.Indented);
		}

		static public bool Glaicwertig(
			SictObjektIdentitäätPerTypeUndNameRegex O0,
			SictObjektIdentitäätPerTypeUndNameRegex O1)
		{
			if (O0 == O1)
			{
				return true;
			}

			if (null == O0 || null == O1)
			{
				return false;
			}

			if (!(string.Equals(O0.ObjektTypeRegexPattern, O1.ObjektTypeRegexPattern)))
			{
				return false;
			}

			if (!(string.Equals(O0.ObjektNameRegexPattern, O1.ObjektNameRegexPattern)))
			{
				return false;
			}

			return false;
		}

		virtual public bool Pasend(SictOverViewObjektZuusctand InRaumObjekt)
		{
			if (null == InRaumObjekt)
			{
				return false;
			}

			return Pasend(InRaumObjekt.Type, InRaumObjekt.Name);
		}

		public bool Pasend(VonSensor.OverviewZaile OverviewZaile)
		{
			if (null == OverviewZaile)
			{
				return false;
			}

			var ObjektType = OverviewZaile.Type;
			var ObjektName = OverviewZaile.Name;

			return Pasend(ObjektType, ObjektName);
		}

		public bool Pasend(string	ObjektType,	string	ObjektName)
		{
			var ObjektTypeRegexPattern = this.ObjektTypeRegexPattern;
			var ObjektNameRegexPattern = this.ObjektNameRegexPattern;

			if (null != ObjektTypeRegexPattern)
			{
				if (null == ObjektType)
				{
					return false;
				}

				var TypeMatch = Regex.Match(ObjektType, ObjektTypeRegexPattern, RegexOptions.IgnoreCase);

				if (!TypeMatch.Success)
				{
					return false;
				}
			}

			if (null != ObjektNameRegexPattern)
			{
				if (null == ObjektName)
				{
					return false;
				}

				var NameMatch = Regex.Match(ObjektName, ObjektNameRegexPattern, RegexOptions.IgnoreCase);

				if (!NameMatch.Success)
				{
					return false;
				}
			}

			return true;
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictStrategikonOverviewObjektFilter
	{
		[JsonProperty]
		public SictOverviewObjektFilterTypeUndName BedingungTypeUndName;

		[JsonProperty]
		public bool? BedingungMainIconRootWiiRat;

		public SictStrategikonOverviewObjektFilter()
			:
			this(null)
		{
		}

		public SictStrategikonOverviewObjektFilter(
			SictOverviewObjektFilterTypeUndName BedingungTypeUndName,
			bool BedingungMainIconRootWiiRat = false)
		{
			this.BedingungTypeUndName = BedingungTypeUndName;

			this.BedingungMainIconRootWiiRat = BedingungMainIconRootWiiRat;
		}

		public SictStrategikonOverviewObjektFilter(
			SictOverviewObjektGrupeEnum[]	MengeOverviewGrupe,
			string ObjektTypeRegexPattern,
			string ObjektNameRegexPattern,
			bool BedingungMainIconRootWiiRat = false)
			:
			this(
			new SictOverviewObjektFilterTypeUndName(MengeOverviewGrupe, ObjektTypeRegexPattern, ObjektNameRegexPattern),
			BedingungMainIconRootWiiRat)
		{
		}

		public SictStrategikonOverviewObjektFilter(
			SictOverviewObjektGrupeEnum	OverviewGrupe,
			string ObjektTypeRegexPattern,
			string ObjektNameRegexPattern,
			bool BedingungMainIconRootWiiRat = false)
			:
			this(new	SictOverviewObjektGrupeEnum[]{OverviewGrupe}, ObjektTypeRegexPattern, ObjektNameRegexPattern, BedingungMainIconRootWiiRat)
		{
		}

		override public string ToString()
		{
			return JsonConvert.SerializeObject(this, Formatting.Indented);
		}

		static public	SictStrategikonOverviewObjektFilter FilterRat()
		{
			return FilterRatMitNaameRegexPattern(null);
		}

		static public SictStrategikonOverviewObjektFilter FilterRatMitNaameRegexPattern(string NaameRegexPattern)
		{
			return new SictStrategikonOverviewObjektFilter(
				SictOverviewObjektGrupeEnum.Rat, ".", NaameRegexPattern, true);
		}

		static public	SictStrategikonOverviewObjektFilter FilterCargoContainer()
		{
			return new SictStrategikonOverviewObjektFilter(SictOverviewObjektFilterTypeUndName.FilterCargoContainer(), false);
		}

		/*
		 * 2014.04.26
		 * 
		 * Ersaz durc MengeFilterAccelerationGate
		 * 
		static public	SictStrategikonOverviewObjektFilter FilterAccelerationGate()
		{
			return new SictStrategikonOverviewObjektFilter(SictOverviewObjektFilterTypeUndName.FilterAccelerationGate(), false);
		}
		 * */

		static public SictStrategikonOverviewObjektFilter[] MengeFilterAccelerationGate()
		{
			return
				SictOverviewObjektFilterTypeUndName.MengeFilterAccelerationGate()
				.Select((OverviewObjektFilterTypeUndName) => new SictStrategikonOverviewObjektFilter(OverviewObjektFilterTypeUndName, false))
				.ToArray();
		}

		static public SictStrategikonOverviewObjektFilter FilterWreckMitNameRegex(string NameRegex)
		{
			return new SictStrategikonOverviewObjektFilter(SictOverviewObjektFilterTypeUndName.FilterWreckMitNameRegex(NameRegex), false);
		}

		static public SictStrategikonOverviewObjektFilter[] MengeFilterWreckMitTypeOderNameRegex(string TypeOderNameRegex)
		{
			var MengeOverviewObjektFilterTypeUndName =
				new SictOverviewObjektFilterTypeUndName[]{
					SictOverviewObjektFilterTypeUndName.FilterWreckMitNameRegex(TypeOderNameRegex)}
				.Concat(
				SictOverviewObjektFilterTypeUndName.MengeFilterWreckMitTypeRegex(TypeOderNameRegex)).ToArray();

			return
				MengeOverviewObjektFilterTypeUndName.Select((ObjektFilterTypeUndName) =>
					new SictStrategikonOverviewObjektFilter(ObjektFilterTypeUndName, false)).ToArray();
		}

		static public bool Glaicwertig(
			SictStrategikonOverviewObjektFilter O0,
			SictStrategikonOverviewObjektFilter O1)
		{
			if (O0 == O1)
			{
				return true;
			}

			if (null == O0 || null == O1)
			{
				return false;
			}

			if (!(O0.BedingungMainIconRootWiiRat == O1.BedingungMainIconRootWiiRat))
			{
				return false;
			}

			return SictObjektIdentitäätPerTypeUndNameRegex.Glaicwertig(O0.BedingungTypeUndName, O1.BedingungTypeUndName);
		}

		public bool Pasend(SictOverViewObjektZuusctand	InRaumObjekt)
		{
			if (null == InRaumObjekt)
			{
				return false;
			}

			var InRaumObjektOverviewZaileLezte = InRaumObjekt.OverviewZaileSictbarMitZaitLezte;

			if(!InRaumObjektOverviewZaileLezte.HasValue)
			{
				return	false;
			}

			return Pasend(InRaumObjektOverviewZaileLezte.Value.Wert);
		}

		public bool Pasend(VonSensor.OverviewZaile	OverviewZaile)
		{
			var BedingungTypeUndName = this.BedingungTypeUndName;

			if (null	!= BedingungTypeUndName)
			{
				if (!BedingungTypeUndName.Pasend(OverviewZaile))
				{
					return false;
				}
			}

			if (null == OverviewZaile)
			{
				return false;
			}

			if (true	== BedingungMainIconRootWiiRat)
			{
				if (!(OverviewZaile.OverviewIconMainColorIsRedAsRat()))
				{
					return false;
				}
			}

			return true;
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictObjektIdentitäätPerTypeUndNameRegexUndMainIconFarbeMitPrio
	{
		[JsonProperty]
		public SictStrategikonOverviewObjektFilter Identitäät;

		[JsonProperty]
		public int? Prioritäät;

		/// <summary>
		/// Vernictung diiser Objekte hat Vorrang vor deem ainsctele der Distanz zu angraifende Objekte.
		/// </summary>
		[JsonProperty]
		public bool? PrioritäätVorGefectOptimaleDistanzAndere;

		public SictObjektIdentitäätPerTypeUndNameRegexUndMainIconFarbeMitPrio()
		{
		}

		public SictObjektIdentitäätPerTypeUndNameRegexUndMainIconFarbeMitPrio(
			SictStrategikonOverviewObjektFilter Identitäät,
			int? Prioritäät = null,
			bool? PrioritäätVorGefectOptimaleDistanzAndere = null)
		{
			this.Identitäät = Identitäät;
			this.Prioritäät = Prioritäät;
			this.PrioritäätVorGefectOptimaleDistanzAndere = PrioritäätVorGefectOptimaleDistanzAndere;
		}

		override public string ToString()
		{
			return JsonConvert.SerializeObject(this, Formatting.Indented);
		}

		virtual public bool Pasend(VonSensor.OverviewZaile	OverviewZaile)
		{
			var Identitäät = this.Identitäät;

			if (null == Identitäät)
			{
				return false;
			}

			return Identitäät.Pasend(OverviewZaile);
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictMissionStrategikonInRaumAtom
	{
		[JsonProperty]
		public SictStrategikonOverviewObjektFilter[] MengeObjektFilter
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? ZuObjektDistanzAinzuscteleScrankeMin
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? ZuObjektDistanzAinzuscteleScrankeMax
		{
			private set;
			get;
		}

		/// <summary>
		/// Menge der gefilterte Objekte sol zersctöört werde.
		/// </summary>
		[JsonProperty]
		public bool? ObjektZersctööre
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? ObjektDurcsuuceCargo
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? BedingungObjektExistent
		{
			private set;
			get;
		}

		/// <summary>
		/// Untere Scranke der Länge des Zaitraum in welcem di Bedingunge in diisem Scrit erfült sain müse damit diiser Scrit als erleedigt gekenzaicnet werd.
		/// </summary>
		[JsonProperty]
		public Int64? ScritBedingungErfültBeruhigungszaitMili
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictInRaumObjektBearbaitungPrio Prioritäät
		{
			private set;
			get;
		}

		public SictMissionStrategikonInRaumAtom()
		{
		}

		public SictMissionStrategikonInRaumAtom(
			SictStrategikonOverviewObjektFilter[] MengeObjektFilter,
			SictInRaumObjektBearbaitungPrio Prioritäät	= default(SictInRaumObjektBearbaitungPrio),
			Int64? BeruhigungszaitMili	=	null)
		{
			this.MengeObjektFilter = MengeObjektFilter;
			this.Prioritäät = Prioritäät;
			this.ScritBedingungErfültBeruhigungszaitMili = BeruhigungszaitMili;
		}

		static public SictMissionStrategikonInRaumAtom AtomZersctööre(
			SictStrategikonOverviewObjektFilter ObjektZuZersctööreFilter,
			SictInRaumObjektBearbaitungPrio Prioritäät = default(SictInRaumObjektBearbaitungPrio),
			Int64? BeruhigungszaitMili	=	null)
		{
			return AtomZersctööre(
				new SictStrategikonOverviewObjektFilter[]{
					ObjektZuZersctööreFilter},
					Prioritäät,
					BeruhigungszaitMili);
		}

		static public SictMissionStrategikonInRaumAtom AtomZersctööre(
			SictStrategikonOverviewObjektFilter[] ObjektZuZersctööreMengeFilter,
			SictInRaumObjektBearbaitungPrio Prioritäät = default(SictInRaumObjektBearbaitungPrio),
			Int64? BeruhigungszaitMili	= null)
		{
			var Atom = new SictMissionStrategikonInRaumAtom(ObjektZuZersctööreMengeFilter,	Prioritäät, BeruhigungszaitMili);

			Atom.ObjektZersctööre = true;

			return Atom;
		}

		static public SictMissionStrategikonInRaumAtom AtomZersctööreAleRat(
			SictInRaumObjektBearbaitungPrio Prioritäät	= default(SictInRaumObjektBearbaitungPrio),
			Int64? BeruhigungszaitMili	=	null)
		{
			return AtomZersctööre(SictStrategikonOverviewObjektFilter.FilterRat(), Prioritäät, BeruhigungszaitMili);
		}

		static public SictMissionStrategikonInRaumAtom AtomDurcsuuceCargo(
			SictStrategikonOverviewObjektFilter ObjektFilter,
			SictInRaumObjektBearbaitungPrio Prioritäät = default(SictInRaumObjektBearbaitungPrio),
			Int64? BeruhigungszaitMili	=	null)
		{
			var MengeObjektFilter = new SictStrategikonOverviewObjektFilter[] { ObjektFilter };

			var Atom = new SictMissionStrategikonInRaumAtom(MengeObjektFilter, Prioritäät, BeruhigungszaitMili);

			Atom.ObjektDurcsuuceCargo = true;

			return Atom;
		}

		static public SictMissionStrategikonInRaumAtom AtomObjektExistent(
			SictStrategikonOverviewObjektFilter ObjektFilter,
			SictInRaumObjektBearbaitungPrio Prioritäät = default(SictInRaumObjektBearbaitungPrio),
			Int64? BeruhigungszaitMili	=	null)
		{
			var MengeObjektFilter = new SictStrategikonOverviewObjektFilter[] { ObjektFilter };

			var Atom = new SictMissionStrategikonInRaumAtom(MengeObjektFilter, Prioritäät, BeruhigungszaitMili);

			Atom.BedingungObjektExistent = true;

			return Atom;
		}

		static public SictMissionStrategikonInRaumAtom AtomFliigeAn(
			SictStrategikonOverviewObjektFilter ObjektFilter,
			Int64 DistanceScrankeMax,
			SictInRaumObjektBearbaitungPrio Prioritäät = default(SictInRaumObjektBearbaitungPrio),
			Int64? BeruhigungszaitMili	=	null)
		{
			var MengeObjektFilter = new SictStrategikonOverviewObjektFilter[] { ObjektFilter };

			var Atom = new SictMissionStrategikonInRaumAtom(MengeObjektFilter, Prioritäät, BeruhigungszaitMili);

			Atom.ZuObjektDistanzAinzuscteleScrankeMax = DistanceScrankeMax;

			return Atom;
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictMissionStrategikonInRaumSctruktuur
	{
		[JsonProperty]
		public SictMissionStrategikonInRaumAtom Atom
		{
			private set;
			get;
		}

		/// <summary>
		/// Menge von Atome welce ale durcgefüürt werde müse bevor diise begone werd.
		/// </summary>
		[JsonProperty]
		public KeyValuePair<int, bool>[] MengeBedingungKonjunkt
		{
			private set;
			get;
		}

		/// <summary>
		/// Menge von Atome von welcem aines durcgefüürt werde mus bevor diise begone werd.
		/// </summary>
		[JsonProperty]
		public KeyValuePair<int, bool>[] MengeBedingungDisjunkt
		{
			private set;
			get;
		}

		public SictMissionStrategikonInRaumSctruktuur()
		{
		}

		public SictMissionStrategikonInRaumSctruktuur(
			SictMissionStrategikonInRaumAtom Atom,
			KeyValuePair<int, bool>[] MengeBedingungKonjunkt	=	null,
			KeyValuePair<int, bool>[] MengeBedingungDisjunkt = null)
		{
			this.Atom = Atom;
			this.MengeBedingungKonjunkt = MengeBedingungKonjunkt;
			this.MengeBedingungDisjunkt = MengeBedingungDisjunkt;
		}

		static public SictMissionStrategikonInRaumSctruktuur AtomZersctööreAleRat(
			SictInRaumObjektBearbaitungPrio Prioritäät = default(SictInRaumObjektBearbaitungPrio),
			Int64? BeruhigungszaitMili	=	null)
		{
			return new SictMissionStrategikonInRaumSctruktuur(
				SictMissionStrategikonInRaumAtom.AtomZersctööreAleRat(Prioritäät,	BeruhigungszaitMili));
		}

		static public SictMissionStrategikonInRaumSctruktuur AtomZersctööre(
			SictStrategikonOverviewObjektFilter ObjektZuZersctööreFilter,
			SictInRaumObjektBearbaitungPrio Prioritäät = default(SictInRaumObjektBearbaitungPrio),
			Int64? BeruhigungszaitMili	=null)
		{
			return AtomZersctööre(
				new SictStrategikonOverviewObjektFilter[]{
					ObjektZuZersctööreFilter},
					Prioritäät,
					BeruhigungszaitMili);
		}

		static public SictMissionStrategikonInRaumSctruktuur AtomZersctööre(
			SictStrategikonOverviewObjektFilter[] ObjektZuZersctööreMengeFilter,
			SictInRaumObjektBearbaitungPrio Prioritäät = default(SictInRaumObjektBearbaitungPrio),
			Int64? BeruhigungszaitMili	=	null)
		{
			return new SictMissionStrategikonInRaumSctruktuur(SictMissionStrategikonInRaumAtom.AtomZersctööre(
				ObjektZuZersctööreMengeFilter, Prioritäät, BeruhigungszaitMili));
		}

		static public SictMissionStrategikonInRaumSctruktuur AtomDurcsuuceCargo(
			SictStrategikonOverviewObjektFilter	ObjektFilter,
			SictInRaumObjektBearbaitungPrio Prioritäät = default(SictInRaumObjektBearbaitungPrio),
			Int64? BeruhigungszaitMili	=	null)
		{
			return new SictMissionStrategikonInRaumSctruktuur(SictMissionStrategikonInRaumAtom.AtomDurcsuuceCargo(
				ObjektFilter, Prioritäät, BeruhigungszaitMili));
		}

		static public SictMissionStrategikonInRaumSctruktuur AtomObjektExistent(
			SictStrategikonOverviewObjektFilter	ObjektFilter,
			SictInRaumObjektBearbaitungPrio Prioritäät = default(SictInRaumObjektBearbaitungPrio),
			Int64? BeruhigungszaitMili	=	null)
		{
			return new SictMissionStrategikonInRaumSctruktuur(SictMissionStrategikonInRaumAtom.AtomObjektExistent(
				ObjektFilter, Prioritäät, BeruhigungszaitMili));
		}

		static public SictMissionStrategikonInRaumSctruktuur AtomFliigeAn(
			SictStrategikonOverviewObjektFilter	ObjektFilter,
			Int64	DistanceScrankeMax,
			SictInRaumObjektBearbaitungPrio Prioritäät = default(SictInRaumObjektBearbaitungPrio),
			Int64? BeruhigungszaitMili	=	null)
		{
			return new SictMissionStrategikonInRaumSctruktuur(SictMissionStrategikonInRaumAtom.AtomFliigeAn(
				ObjektFilter, DistanceScrankeMax, Prioritäät, BeruhigungszaitMili));
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public	class SictMissionStrategikon	:	ICloneable
	{
		public bool PasendZuMissionTitel(
			string	ZuPrüüfeMissionTitel)
		{
			if (null == ZuPrüüfeMissionTitel)
			{
				return false;
			}

			var MissionTitel = this.MissionTitel;

			if (null == MissionTitel)
			{
				return false;
			}

			return	MissionTitel.PasendZuTitel(ZuPrüüfeMissionTitel);
		}

		public	bool	PasendZuMissionOffer(
			VonSensor.WindowAgentMissionInfo	MissionInfo,
			out	SictFactionSictEnum[] AusOfferMengeFaction)
		{
			AusOfferMengeFaction = null;

			if(null	== MissionInfo)
			{
				return	false;
			}

			var MissionTitel = this.MissionTitel;
			var MengeFactionPassend = this.MengeFactionPassend;

			if (null == MissionTitel)
			{
				return false;
			}

			if (false)
			{
				//	2015.02.12	Filter Faction werd vorersct ignoriirt.

				if (null == MengeFactionPassend)
				{
					return false;
				}

				AusOfferMengeFaction = MissionTitel.ZuMissionMengeFaction(MissionInfo);

				if (null == AusOfferMengeFaction)
				{
					return false;
				}

				if (AusOfferMengeFaction.Length < 1)
				{
					return false;
				}

				if (MengeFactionPassend.Any((Faction) => Faction == SictFactionSictEnum.Andere))
				{
					//	Konfig wurde als auc für andere Faction pasend markiirt.
					return true;
				}

				return AusOfferMengeFaction.All((AusOfferFaction) => MengeFactionPassend.Any((FactionPasend) => FactionPasend == AusOfferFaction));
			}
			else
			{
				return MissionTitel.PasendZuTitel(MissionInfo.MissionTitel);
			}
		}

		[JsonProperty]
		public SictMissionTitel MissionTitel;

		[JsonProperty]
		public SictFactionSictEnum[] MengeFactionPassend;

		/// <summary>
		/// Für Fitting: Annaame aingehende DPS
		/// </summary>
		[JsonProperty]
		public SictDamageMitBetraagIntValue[] AnnaameDamage;

		/// <summary>
		/// Für Waal Wirkmitel/Munition: Annaame Tank der zu vernictende Objekte.
		/// </summary>
		[JsonProperty]
		public SictDamageMitBetraagIntValue[] AnnaameTank;

		[JsonProperty]
		public List<KeyValuePair<int,	SictMissionStrategikonInRaumSctruktuur>> RaumMengeZuBezaicnerAtom
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? FürLevel4WarpToWithinFürDistanceAbhängigVonWirkungDestruktRange;

		public SictMissionStrategikon()
			:
			this((SictMissionStrategikonInRaumSctruktuur)null)
		{
		}

		public SictMissionStrategikon(
			KeyValuePair<int, SictMissionStrategikonInRaumSctruktuur>[] RaumMengeZuBezaicnerAtom)
		{
			this.RaumMengeZuBezaicnerAtom =
				new List<KeyValuePair<int, SictMissionStrategikonInRaumSctruktuur>>(RaumMengeZuBezaicnerAtom ?? new KeyValuePair<int, SictMissionStrategikonInRaumSctruktuur>[0]);
		}

		public SictMissionStrategikon(
			SictMissionStrategikonInRaumSctruktuur RaumBedingungAbsclus)
			:
			this(
			new KeyValuePair<int, SictMissionStrategikonInRaumSctruktuur>[]{
				new	KeyValuePair<int, SictMissionStrategikonInRaumSctruktuur>(0,	RaumBedingungAbsclus)})
		{
		}

		override public string ToString()
		{
			return JsonConvert.SerializeObject(this, Formatting.Indented);
		}

		static readonly public string LocationEncounterRegexPattern = Regex.Escape("Encounter");

		static readonly public string LocationAgentStationRegexPattern = "Agent[\\s\\w]*Base";

		static readonly public string AccelerationGateTypeRegexPattern = "Acceleration Gate";

		static readonly public SictObjektIdentitäätPerTypeUndNameRegex ObjektFilterAccelerationGate =
			new SictObjektIdentitäätPerTypeUndNameRegex(AccelerationGateTypeRegexPattern);

		static public SictMissionStrategikon StrategikonZersctööre(
			IEnumerable<KeyValuePair<SictStrategikonOverviewObjektFilter, SictInRaumObjektBearbaitungPrio>> ObjektZuZersctööreMengeFilterMitPrio,
			Int64? BeruhigungszaitMili = null)
		{
			return StrategikonZersctööreUndDurcsuuceCargo(
				ObjektZuZersctööreMengeFilterMitPrio,
				(SictStrategikonOverviewObjektFilter)null,
				BeruhigungszaitMili);
		}
	
		static public SictMissionStrategikon StrategikonZersctööre(
			SictStrategikonOverviewObjektFilter[] ObjektZuZersctööreMengeFilter,
			SictInRaumObjektBearbaitungPrio Prioritäät = default(SictInRaumObjektBearbaitungPrio),
			Int64? BeruhigungszaitMili = null)
		{
			var ObjektZuZersctööreMengeFilterMitPrio =
			(null == ObjektZuZersctööreMengeFilter) ? null :
			ObjektZuZersctööreMengeFilter
			.Select((ObjektZuZersctööreFilter) => new KeyValuePair<SictStrategikonOverviewObjektFilter, SictInRaumObjektBearbaitungPrio>(
				ObjektZuZersctööreFilter, Prioritäät)).ToArray();

			return StrategikonZersctööre(
				ObjektZuZersctööreMengeFilterMitPrio,
				BeruhigungszaitMili);
		}

		static public SictMissionStrategikon StrategikonZersctööre(
			SictStrategikonOverviewObjektFilter ObjektZuZersctööreFilter,
			SictInRaumObjektBearbaitungPrio Prioritäät = default(SictInRaumObjektBearbaitungPrio),
			Int64? BeruhigungszaitMili = null)
		{
			return new SictMissionStrategikon(SictMissionStrategikonInRaumSctruktuur.AtomZersctööre(
				ObjektZuZersctööreFilter,	Prioritäät, BeruhigungszaitMili));
		}

		static public SictMissionStrategikon StrategikonZersctööreAleRat(
			SictInRaumObjektBearbaitungPrio Prioritäät = default(SictInRaumObjektBearbaitungPrio),
			Int64? BeruhigungszaitMili = null)
		{
			return new SictMissionStrategikon(
				SictMissionStrategikonInRaumSctruktuur.AtomZersctööreAleRat(
				Prioritäät,
				BeruhigungszaitMili));
		}

		static public SictMissionStrategikon StrategikonZersctööreAleRatUndDurcsuuceCargo(
			SictStrategikonOverviewObjektFilter ObjektDurcsuuceCargoFilter,
			Int64? BeruhigungszaitMili = null)
		{
			return StrategikonZersctööreUndDurcsuuceCargo(
				SictStrategikonOverviewObjektFilter.FilterRat(),
				ObjektDurcsuuceCargoFilter,
				BeruhigungszaitMili);
		}

		static public SictMissionStrategikon StrategikonRükgaabeUndSezeWarpToLocationWithinFürLevel4(
			SictMissionStrategikon Strategikon)
		{
			return StrategikonRükgaabeUndAktioon(Strategikon, (s) => s.FürLevel4WarpToWithinFürDistanceAbhängigVonWirkungDestruktRange = true);
		}

		static public SictMissionStrategikon StrategikonRükgaabeUndAktioon(
			SictMissionStrategikon Strategikon,
			Action<SictMissionStrategikon> Aktioon)
		{
			if (null != Aktioon)
			{
				Aktioon(Strategikon);
			}

			return Strategikon;
		}

		static public SictMissionStrategikon StrategikonZersctööreUndDurcsuuceCargo(
			SictStrategikonOverviewObjektFilter ObjektZuZersctööreFilter,
			SictStrategikonOverviewObjektFilter ObjektDurcsuuceCargoFilter,
			Int64? BeruhigungszaitMili = null)
		{
			return StrategikonZersctööreUndDurcsuuceCargo(
				new SictStrategikonOverviewObjektFilter[] { ObjektZuZersctööreFilter },
				ObjektDurcsuuceCargoFilter,
				BeruhigungszaitMili);
		}

		static public SictMissionStrategikon StrategikonDurcsuuceCargo(
			SictStrategikonOverviewObjektFilter ObjektDurcsuuceCargoFilter,
			Int64? BeruhigungszaitMili = null)
		{
			return StrategikonZersctööreUndDurcsuuceCargo(
				(SictStrategikonOverviewObjektFilter[])null,
				ObjektDurcsuuceCargoFilter,
				BeruhigungszaitMili);
		}

		static public SictMissionStrategikon StrategikonZersctööreUndDurcsuuceCargo(
			SictStrategikonOverviewObjektFilter[] ObjektZuZersctööreMengeFilter,
			SictStrategikonOverviewObjektFilter ObjektDurcsuuceCargoFilter,
			Int64? BeruhigungszaitMili = null)
		{
			var ObjektZuZersctööreMengeFilterMitPrio =
				(null == ObjektZuZersctööreMengeFilter) ? null :
				ObjektZuZersctööreMengeFilter
				.Select((ObjektZuZersctööreFilter) => new KeyValuePair<SictStrategikonOverviewObjektFilter, SictInRaumObjektBearbaitungPrio>(
					ObjektZuZersctööreFilter, new SictInRaumObjektBearbaitungPrio())).ToArray();

			return StrategikonZersctööreUndDurcsuuceCargo(
				ObjektZuZersctööreMengeFilterMitPrio,
				ObjektDurcsuuceCargoFilter,
				BeruhigungszaitMili);
		}

		static public SictMissionStrategikon StrategikonZersctööreUndDurcsuuceCargo(
			IEnumerable<SictStrategikonOverviewObjektFilter> ObjektZuZersctööreMengeFilter,
			IEnumerable<SictStrategikonOverviewObjektFilter> ObjektDurcsuuceCargoFilterMengeFilter,
			Int64? BeruhigungszaitMili = null)
		{
			return StrategikonZersctööreUndDurcsuuceCargo(
				MengeObjektFilterKombiniirtMitPrio(ObjektZuZersctööreMengeFilter, new SictInRaumObjektBearbaitungPrio()),
				MengeObjektFilterKombiniirtMitPrio(ObjektDurcsuuceCargoFilterMengeFilter, new SictInRaumObjektBearbaitungPrio()),
				BeruhigungszaitMili);
		}

		static public IEnumerable<KeyValuePair<SictStrategikonOverviewObjektFilter, SictInRaumObjektBearbaitungPrio>> MengeObjektFilterKombiniirtMitPrio(
			IEnumerable<SictStrategikonOverviewObjektFilter> MengeObjektFilter,
			SictInRaumObjektBearbaitungPrio Prio)
		{
			if (null == MengeObjektFilter)
			{
				return null;
			}

			return MengeObjektFilter.Select((ObjektFilter) => new KeyValuePair<SictStrategikonOverviewObjektFilter, SictInRaumObjektBearbaitungPrio>(ObjektFilter, Prio));
		}

		static public SictMissionStrategikon StrategikonZersctööreUndDurcsuuceCargo(
			IEnumerable<KeyValuePair<SictStrategikonOverviewObjektFilter,SictInRaumObjektBearbaitungPrio>> ObjektZuZersctööreMengeFilterMitPrio,
			SictStrategikonOverviewObjektFilter ObjektDurcsuuceCargoFilter,
			Int64? BeruhigungszaitMili = null)
		{
			var ObjektDurcsuuceCargoMengeFilterMitPrio = new List<KeyValuePair<SictStrategikonOverviewObjektFilter, SictInRaumObjektBearbaitungPrio>>();

			if (null != ObjektDurcsuuceCargoFilter)
			{
				ObjektDurcsuuceCargoMengeFilterMitPrio.Add(new KeyValuePair<SictStrategikonOverviewObjektFilter, SictInRaumObjektBearbaitungPrio>(
					ObjektDurcsuuceCargoFilter, new SictInRaumObjektBearbaitungPrio()));
			}

			return StrategikonZersctööreUndDurcsuuceCargo(
				ObjektZuZersctööreMengeFilterMitPrio,
				ObjektDurcsuuceCargoMengeFilterMitPrio,
				BeruhigungszaitMili);
		}

		static public SictMissionStrategikon StrategikonZersctööreUndDurcsuuceCargo(
			IEnumerable<KeyValuePair<SictStrategikonOverviewObjektFilter, SictInRaumObjektBearbaitungPrio>> ObjektZuZersctööreMengeFilterMitPrio,
			IEnumerable<SictStrategikonOverviewObjektFilter> ObjektDurcsuuceCargoMengeFilter,
			Int64? BeruhigungszaitMili = null)
		{
			var ObjektDurcsuuceCargoMengeFilterMitPrio =
				ExtractFromOldAssembly.Bib3.Extension.SelectNullable(
				ObjektDurcsuuceCargoMengeFilter,
				(ObjektDurcsuuceCargoFilter) => new KeyValuePair<SictStrategikonOverviewObjektFilter, SictInRaumObjektBearbaitungPrio>(
					ObjektDurcsuuceCargoFilter,
					new SictInRaumObjektBearbaitungPrio()))
				.ToArrayNullable();

			return StrategikonZersctööreUndDurcsuuceCargo(
				ObjektZuZersctööreMengeFilterMitPrio,
				ObjektDurcsuuceCargoMengeFilterMitPrio,
				BeruhigungszaitMili);
		}

		static public SictMissionStrategikon StrategikonZersctööreUndDurcsuuceCargo(
			IEnumerable<KeyValuePair<SictStrategikonOverviewObjektFilter, SictInRaumObjektBearbaitungPrio>> ObjektZuZersctööreMengeFilterMitPrio,
			IEnumerable<KeyValuePair<SictStrategikonOverviewObjektFilter, SictInRaumObjektBearbaitungPrio>> ObjektDurcsuuceCargoMengeFilterMitPrio,
			Int64? BeruhigungszaitMili = null)
		{
			var MengeAtomZersctööre = new List<KeyValuePair<int, SictMissionStrategikonInRaumSctruktuur>>();

			if (null != ObjektZuZersctööreMengeFilterMitPrio)
			{
				foreach (var ObjektZuZersctööreFilterMitPrio in ObjektZuZersctööreMengeFilterMitPrio)
				{
					if (null == ObjektZuZersctööreFilterMitPrio.Key)
					{
						continue;
					}

					var AtomZersctöre =
						new KeyValuePair<int, SictMissionStrategikonInRaumSctruktuur>(101 + MengeAtomZersctööre.Count,
						SictMissionStrategikonInRaumSctruktuur.AtomZersctööre(ObjektZuZersctööreFilterMitPrio.Key, ObjektZuZersctööreFilterMitPrio.Value, BeruhigungszaitMili));

					MengeAtomZersctööre.Add(AtomZersctöre);
				}
			}

			var MengeAtomDurcsuuceCargo = new List<KeyValuePair<int, SictMissionStrategikonInRaumSctruktuur>>();

			if (null != ObjektDurcsuuceCargoMengeFilterMitPrio)
			{
				foreach (var ObjektDurcsuuceCargoFilterMitPrio in ObjektDurcsuuceCargoMengeFilterMitPrio)
				{
					if (null == ObjektDurcsuuceCargoFilterMitPrio.Key)
					{
						continue;
					}

					var AtomDurcsuuceCargo =
						new KeyValuePair<int, SictMissionStrategikonInRaumSctruktuur>(301 + MengeAtomDurcsuuceCargo.Count,
						SictMissionStrategikonInRaumSctruktuur.AtomDurcsuuceCargo(ObjektDurcsuuceCargoFilterMitPrio.Key, ObjektDurcsuuceCargoFilterMitPrio.Value, BeruhigungszaitMili));

					MengeAtomDurcsuuceCargo.Add(AtomDurcsuuceCargo);
				}
			}

			var AtomWurzel =
				new KeyValuePair<int, SictMissionStrategikonInRaumSctruktuur>(
					0,
					new SictMissionStrategikonInRaumSctruktuur(null,
						MengeAtomZersctööre.Select((AtomZersctööre) => new KeyValuePair<int, bool>(AtomZersctööre.Key, false))
						.Concat(
						MengeAtomDurcsuuceCargo.Select((AtomCargoDurcsuuce) => new	KeyValuePair<int,	bool>(AtomCargoDurcsuuce.Key, false))
						).ToArray()));

			return new SictMissionStrategikon(
				MengeAtomZersctööre.Concat(
				MengeAtomDurcsuuceCargo.Concat(
				new KeyValuePair<int,	SictMissionStrategikonInRaumSctruktuur>[]{
					AtomWurzel})).ToArray());
		}

		static public SictMissionStrategikon StrategikonZersctööreUndDurcsuuceCargoWenNictObjektExistent(
			SictStrategikonOverviewObjektFilter[] ObjektZuZersctööreMengeFilter,
			SictStrategikonOverviewObjektFilter ObjektDurcsuuceCargoFilter,
			SictStrategikonOverviewObjektFilter[] ObjektExistentObjektMengeFilter,
			Int64? BeruhigungszaitMili = null)
		{
			var ZuZersctöörePrioritäät = default(SictInRaumObjektBearbaitungPrio);

			return StrategikonZersctööreUndDurcsuuceCargoWenNictObjektExistent(
				ObjektZuZersctööreMengeFilter
				?.Select((ObjektZuZersctööreFilter) =>
					new KeyValuePair<SictStrategikonOverviewObjektFilter, SictInRaumObjektBearbaitungPrio>(ObjektZuZersctööreFilter, ZuZersctöörePrioritäät))?.ToArray(),
				ObjektDurcsuuceCargoFilter,
				ObjektExistentObjektMengeFilter);
		}

		static public SictMissionStrategikon StrategikonZersctööreUndDurcsuuceCargoWenNictObjektExistent(
			KeyValuePair<SictStrategikonOverviewObjektFilter, SictInRaumObjektBearbaitungPrio>[] ObjektZuZersctööreMengeFilterMitPrio,
			SictStrategikonOverviewObjektFilter ObjektDurcsuuceCargoFilter,
			SictStrategikonOverviewObjektFilter[] ObjektExistentObjektMengeFilter,
			Int64? BeruhigungszaitMili = null)
		{
			return StrategikonZersctööreUndDurcsuuceCargoWenNictObjektExistent(
				ObjektZuZersctööreMengeFilterMitPrio,
				(null == ObjektDurcsuuceCargoFilter) ? null :
				new SictStrategikonOverviewObjektFilter[] { ObjektDurcsuuceCargoFilter },
				ObjektExistentObjektMengeFilter,
				BeruhigungszaitMili);
		}

		static public SictMissionStrategikon StrategikonZersctööreUndDurcsuuceCargoWenNictObjektExistent(
			KeyValuePair<SictStrategikonOverviewObjektFilter, SictInRaumObjektBearbaitungPrio>[] ObjektZuZersctööreMengeFilterMitPrio,
			SictStrategikonOverviewObjektFilter[] ObjektDurcsuuceCargoMengeFilter,
			SictStrategikonOverviewObjektFilter[] ObjektExistentObjektMengeFilter,
			Int64? BeruhigungszaitMili = null)
		{
			var ObjektDurcsuuceCargoMengeFilterMitPrio =
				ExtractFromOldAssembly.Bib3.Extension.SelectNullable(
				ObjektDurcsuuceCargoMengeFilter,
				(ObjektDurcsuuceCargoFilter) => new KeyValuePair<SictStrategikonOverviewObjektFilter, SictInRaumObjektBearbaitungPrio>(
					ObjektDurcsuuceCargoFilter, new SictInRaumObjektBearbaitungPrio()))
				.ToArrayNullable();

			return StrategikonZersctööreUndDurcsuuceCargoWenNictObjektExistent(
				ObjektZuZersctööreMengeFilterMitPrio,
				ObjektDurcsuuceCargoMengeFilterMitPrio,
				ObjektExistentObjektMengeFilter,
				BeruhigungszaitMili);
		}

		static public SictMissionStrategikon StrategikonZersctööreUndDurcsuuceCargoWenNictObjektExistent(
			KeyValuePair<SictStrategikonOverviewObjektFilter,	SictInRaumObjektBearbaitungPrio>[] ObjektZuZersctööreMengeFilterMitPrio,
			KeyValuePair<SictStrategikonOverviewObjektFilter, SictInRaumObjektBearbaitungPrio>[] ObjektDurcsuuceCargoMengeFilterMitPrio,
			SictStrategikonOverviewObjektFilter[] ObjektExistentMengeObjektFilter,
			Int64? BeruhigungszaitMili = null)
		{
			var Prioritäät = default(SictInRaumObjektBearbaitungPrio);

			var MengeAtomZersctöreBezaicnerUndSctruktur =
					ExtractFromOldAssembly.Bib3.Extension.SelectNullable(
					ObjektZuZersctööreMengeFilterMitPrio,
					(ObjektFilterMitPrio,	AtomZersctööreTailIndex) =>
					new KeyValuePair<int, SictMissionStrategikonInRaumSctruktuur>(101 + AtomZersctööreTailIndex,
					SictMissionStrategikonInRaumSctruktuur.AtomZersctööre(ObjektFilterMitPrio.Key, ObjektFilterMitPrio.Value, BeruhigungszaitMili)))
					.ToArrayNullable();

			var MengeAtomCargoDurcsuuceBezaicnerUndSctruktur =
					ExtractFromOldAssembly.Bib3.Extension.SelectNullable(
					ObjektDurcsuuceCargoMengeFilterMitPrio, (ObjektFilterMitPrio, AtomCargoDurcsuuceTailIndex) =>
					new KeyValuePair<int, SictMissionStrategikonInRaumSctruktuur>(301 + AtomCargoDurcsuuceTailIndex,
					SictMissionStrategikonInRaumSctruktuur.AtomDurcsuuceCargo(ObjektFilterMitPrio.Key, ObjektFilterMitPrio.Value, BeruhigungszaitMili)))
					.ToArrayNullable();

			var AtomZersctöreKonjunktBezaicnerUndSctruktur =
				new KeyValuePair<int, SictMissionStrategikonInRaumSctruktuur>(100,
				new	SictMissionStrategikonInRaumSctruktuur(null,
					ExtractFromOldAssembly.Bib3.Extension.SelectNullable(
					MengeAtomZersctöreBezaicnerUndSctruktur,
					(AtomZersctöreBezaicnerUndSctruktur) => new	KeyValuePair<int,	bool>(AtomZersctöreBezaicnerUndSctruktur.Key, false))
					.ToArrayNullable()));

			var AtomCargoDurcsuuceKonjunktBezaicnerUndSctruktur =
				new KeyValuePair<int, SictMissionStrategikonInRaumSctruktuur>(300,
				new SictMissionStrategikonInRaumSctruktuur(null,
					ExtractFromOldAssembly.Bib3.Extension.SelectNullable(
					MengeAtomCargoDurcsuuceBezaicnerUndSctruktur,
					(AtomCargoDurcsuuceBezaicnerUndSctruktur) => new KeyValuePair<int, bool>(AtomCargoDurcsuuceBezaicnerUndSctruktur.Key, false))
					.ToArrayNullable()));

			var MengeAtomObjektExistentBezaicnerUndSctruktur =
					ExtractFromOldAssembly.Bib3.Extension.SelectNullable(
					ObjektExistentMengeObjektFilter, (ObjektFilter, AtomTailIndex) =>
					new KeyValuePair<int, SictMissionStrategikonInRaumSctruktuur>(401 + AtomTailIndex,
					SictMissionStrategikonInRaumSctruktuur.AtomObjektExistent(ObjektFilter, new	SictInRaumObjektBearbaitungPrio(), BeruhigungszaitMili)))
					.ToArrayNullable();

			var AtomObjektExistentDisjunktBezaicnerUndSctruktur =
				new KeyValuePair<int, SictMissionStrategikonInRaumSctruktuur>(400,
					Bib3.Extension.NullOderLeer(MengeAtomObjektExistentBezaicnerUndSctruktur) ? null :
				new SictMissionStrategikonInRaumSctruktuur(
					null,
					null,
					ExtractFromOldAssembly.Bib3.Extension.SelectNullable(
					MengeAtomObjektExistentBezaicnerUndSctruktur,
					(AtomBezaicnerUndSctruktur) => new KeyValuePair<int, bool>(AtomBezaicnerUndSctruktur.Key, false))
					.ToArrayNullable()));

			var	WurzelMengeBedingungDisjunkt	=
				(null	== AtomObjektExistentDisjunktBezaicnerUndSctruktur.Value)	?	null	:
				new KeyValuePair<int, bool>[]{
					new	KeyValuePair<int,	bool>(AtomObjektExistentDisjunktBezaicnerUndSctruktur.Key, false),};

			var AtomWurzel =
				new KeyValuePair<int, SictMissionStrategikonInRaumSctruktuur>(
					0,
					new SictMissionStrategikonInRaumSctruktuur(null,
						new KeyValuePair<int, bool>[]{
					new	KeyValuePair<int,	bool>(AtomZersctöreKonjunktBezaicnerUndSctruktur.Key, false),
					new	KeyValuePair<int,	bool>(AtomCargoDurcsuuceKonjunktBezaicnerUndSctruktur.Key, false),},
						WurzelMengeBedingungDisjunkt));

			var StrategikonMengeAtom = new List<KeyValuePair<int, SictMissionStrategikonInRaumSctruktuur>>();

			StrategikonMengeAtom.AddRange(MengeAtomZersctöreBezaicnerUndSctruktur);
			StrategikonMengeAtom.AddRange(MengeAtomCargoDurcsuuceBezaicnerUndSctruktur);

			if (null != MengeAtomObjektExistentBezaicnerUndSctruktur)
			{
				StrategikonMengeAtom.AddRange(MengeAtomObjektExistentBezaicnerUndSctruktur);
			}

			StrategikonMengeAtom.Add(AtomZersctöreKonjunktBezaicnerUndSctruktur);
			StrategikonMengeAtom.Add(AtomCargoDurcsuuceKonjunktBezaicnerUndSctruktur);

			if (null != AtomObjektExistentDisjunktBezaicnerUndSctruktur.Value)
			{
				StrategikonMengeAtom.Add(AtomObjektExistentDisjunktBezaicnerUndSctruktur);
			}

			StrategikonMengeAtom.Add(AtomWurzel);

			return new SictMissionStrategikon(StrategikonMengeAtom.ToArray());
		}

		static public SictMissionStrategikon StrategikonFliigeAnUndZersctööre(
			SictStrategikonOverviewObjektFilter ObjektAnzufliigeFilter,
			Int64 ObjektAnzufliigeDistanceScrankeMax,
			SictStrategikonOverviewObjektFilter ObjektZuZersctööreFilter,
			Int64? BeruhigungszaitMili = null)
		{
			return StrategikonFliigeAnUndZersctööre(
				ObjektAnzufliigeFilter,
				ObjektAnzufliigeDistanceScrankeMax,
				new SictStrategikonOverviewObjektFilter[] { ObjektZuZersctööreFilter },
				BeruhigungszaitMili);
		}

		static public SictMissionStrategikon StrategikonFliigeAnUndZersctööre(
			SictStrategikonOverviewObjektFilter ObjektAnzufliigeFilter,
			Int64	ObjektAnzufliigeDistanceScrankeMax,
			SictStrategikonOverviewObjektFilter[] ObjektZuZersctööreMengeFilter,
			Int64? BeruhigungszaitMili = null)
		{
			var Prioritäät = default(SictInRaumObjektBearbaitungPrio);

			var AtomZersctöre =
				new KeyValuePair<int, SictMissionStrategikonInRaumSctruktuur>(1,
				SictMissionStrategikonInRaumSctruktuur.AtomZersctööre(ObjektZuZersctööreMengeFilter, Prioritäät, BeruhigungszaitMili));

			var AtomFliigeAn =
				new KeyValuePair<int, SictMissionStrategikonInRaumSctruktuur>(3,
				SictMissionStrategikonInRaumSctruktuur.AtomFliigeAn(ObjektAnzufliigeFilter, ObjektAnzufliigeDistanceScrankeMax, Prioritäät, BeruhigungszaitMili));

			var AtomWurzel =
				new KeyValuePair<int, SictMissionStrategikonInRaumSctruktuur>(
					0,
					new SictMissionStrategikonInRaumSctruktuur(null, new KeyValuePair<int, bool>[]{
				new	KeyValuePair<int,	bool>(AtomZersctöre.Key, false),
				new	KeyValuePair<int,	bool>(AtomFliigeAn.Key, false),}));

			return new SictMissionStrategikon(
				new KeyValuePair<int, SictMissionStrategikonInRaumSctruktuur>[]{
					AtomZersctöre,
					AtomFliigeAn,
					AtomWurzel});
		}

		public SictOverviewObjektGrupeEnum[] MengeOverviewObjektGrupeVerwendetBerecne()
		{
			var RaumMengeZuBezaicnerAtom = this.RaumMengeZuBezaicnerAtom;

			if (null == RaumMengeZuBezaicnerAtom)
			{
				return null;
			}

			var MengeOverviewObjektGrupeVerwendet = new List<SictOverviewObjektGrupeEnum>();

			foreach (var ZuBezaicnerAtom in RaumMengeZuBezaicnerAtom)
			{
				if (null == ZuBezaicnerAtom.Value)
				{
					continue;
				}

				var Atom = ZuBezaicnerAtom.Value.Atom;

				if (null == Atom)
				{
					continue;
				}

				var MengeObjektFilter = Atom.MengeObjektFilter;

				if (null == MengeObjektFilter)
				{
					continue;
				}

				foreach (var ObjektFilter in MengeObjektFilter)
				{
					var	ObjektFilterBedingungTypeUndName	= ObjektFilter.BedingungTypeUndName;

					if(null	== ObjektFilterBedingungTypeUndName)
					{
						continue;
					}

					var ObjektFilterBedingungTypeUndNameMengeGrupeZuDurcsuuce = ObjektFilterBedingungTypeUndName.MengeGrupeZuDurcsuuce;

					if (null == ObjektFilterBedingungTypeUndNameMengeGrupeZuDurcsuuce)
					{
						continue;
					}

					MengeOverviewObjektGrupeVerwendet.AddRange(ObjektFilterBedingungTypeUndNameMengeGrupeZuDurcsuuce);
				}
			}

			return MengeOverviewObjektGrupeVerwendet.Distinct().ToArray();
		}

		public SictMissionStrategikon Kopii()
		{
			/*
			 * 2013.11.25
			 * Beobactung Probleem baim Kopiire von SictDictZuList über Glob.JsonConvertKopii: Kopii von SictDictZuList.List enthalt meer ainträäge als zu kopiirende.
			 * Daher nääxte Test mit SictRefBaumKopii.ObjektKopiiErsctele.

			 var Kopii = Glob.JsonConvertKopii(this);

			 * */

			var Kopii = SictRefBaumKopii.ObjektKopiiErsctele<SictMissionStrategikon>(this);

			return Kopii;
		}

		public object Clone()
		{
			return Kopii();
		}

		public string FürMissionFittingBezaicnerBerecne(
			SictOptimatParam OptimatParam,
			IEnumerable<SictFactionSictEnum> InMissionMengeFaction,
			out	KeyValuePair<string, string>[] MengeZuFactionFittingBezaicner)
		{
			MengeZuFactionFittingBezaicner = null;

			if (null == OptimatParam)
			{
				return null;
			}

			var OptimatParamMission = OptimatParam.Mission;

			if (null == OptimatParamMission)
			{
				return null;
			}

			MengeZuFactionFittingBezaicner = OptimatParamMission.MengeZuFactionFittingBezaicner;

			return FürMissionFittingBezaicnerBerecne(
				MengeZuFactionFittingBezaicner,
				InMissionMengeFaction);
		}

		public string FürMissionFittingBezaicnerBerecne(
			KeyValuePair<string, string>[] MengeFürFactionFittingBezaicner,
			IEnumerable<SictFactionSictEnum> InMissionMengeFaction)
		{
			var MengeFürFactionSictEnumFittingBezaicner =
				MengeFürFactionFittingBezaicner
				.SelectNullable((FürFactionFittingBezaicner) => new KeyValuePair<SictFactionSictEnum?, string>(
					Optimat.Glob.EnumParseNulbar<SictFactionSictEnum>(FürFactionFittingBezaicner.Key), FürFactionFittingBezaicner.Value))
				.WhereNullable((Kandidaat) => Kandidaat.Key.HasValue)
				.SelectNullable((FürFactionFittingBezaicner) => new KeyValuePair<SictFactionSictEnum, string>(FürFactionFittingBezaicner.Key.Value, FürFactionFittingBezaicner.Value))
				.ToArrayNullable();

			return FürMissionFittingBezaicnerBerecne(
				MengeFürFactionSictEnumFittingBezaicner,
				InMissionMengeFaction);
		}

		public string FürMissionFittingBezaicnerBerecne(
			KeyValuePair<SictFactionSictEnum, string>[] MengeFürFactionFittingBezaicner,
			IEnumerable<SictFactionSictEnum>	InMissionMengeFaction)
		{
			if (null == MengeFürFactionFittingBezaicner)
			{
				return null;
			}

			if (null == InMissionMengeFaction)
			{
				return null;
			}

			var MengeFittingZuordnung =
				MengeFürFactionFittingBezaicner
				.Where((Kandidaat) => Kandidaat.Key == InMissionMengeFaction.FirstOrDefault())
				.ToArray();

			if (0 < MengeFittingZuordnung.Length)
			{
				return MengeFittingZuordnung.FirstOrDefault().Value;
			}

			var FittingFürFactionSonstige = MengeFürFactionFittingBezaicner.FirstOrDefault((Kandidaat) => SictFactionSictEnum.Andere == Kandidaat.Key);

			return FittingFürFactionSonstige.Value;
		}
	}
}
