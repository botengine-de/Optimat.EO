using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Bib3;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ExtractFromOldAssembly.Bib3;

namespace Optimat.EveOnline
{
	[JsonObject(MemberSerialization.OptIn)]
	public class SictKonfigMissionZuMissionFilterVerhalte	:	ICloneable
	{
		static TimeSpan FilterMissionTitelRegexPatternDauerScrankeMax = TimeSpan.FromMilliseconds(100);

		[JsonProperty]
		public string FilterMissionTitelRegexPattern;

		[JsonProperty]
		public SictAgentLevelOderAnySictEnum? FilterAgentLevel;

		[JsonProperty]
		public bool? AktioonFüüreAusAktiiv;

		[JsonProperty]
		public bool? AktioonAcceptAktiiv;
	
		[JsonProperty]
		public bool? AktioonDeclineAktiiv;

		[JsonProperty]
		public bool? AktioonDeclineUnabhängigVonStandingLossAktiiv;

		public SictKonfigMissionZuMissionFilterVerhalte()
		{
		}

		public SictKonfigMissionZuMissionFilterVerhalte(
			string FilterMissionTitelRegexPattern,
			SictAgentLevelOderAnySictEnum? FilterAgentLevel)
		{
			this.FilterMissionTitelRegexPattern = FilterMissionTitelRegexPattern;
			this.FilterAgentLevel = FilterAgentLevel;
		}

		static public bool HinraicendGlaicwertigFürIdentInOptimatParam(
			SictKonfigMissionZuMissionFilterVerhalte	O0,
			SictKonfigMissionZuMissionFilterVerhalte	O1)
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
				O0.FilterAgentLevel == O1.FilterAgentLevel &&
				O0.AktioonFüüreAusAktiiv == O1.AktioonFüüreAusAktiiv &&
				O0.AktioonAcceptAktiiv == O1.AktioonAcceptAktiiv &&
				O0.AktioonDeclineAktiiv == O1.AktioonDeclineAktiiv &&
				O0.AktioonDeclineUnabhängigVonStandingLossAktiiv == O1.AktioonDeclineUnabhängigVonStandingLossAktiiv &&
				O0.FilterMissionTitelRegexPattern == O1.FilterMissionTitelRegexPattern;
		}

		public bool FilterMissionTitelPasend(string MissionTitel)
		{
			var MissionTitelRegexPattern = this.FilterMissionTitelRegexPattern;

			if (null == MissionTitel || null == MissionTitelRegexPattern)
			{
				return false;
			}

			return Regex.Match(MissionTitel, MissionTitelRegexPattern, RegexOptions.IgnoreCase, FilterMissionTitelRegexPatternDauerScrankeMax).Success;
		}

		public bool? PasendZuAgentLevel(int? AgentLevel)
		{
			var FilterAgentLevel = this.FilterAgentLevel;

			if (!FilterAgentLevel.HasValue)
			{
				return false;
			}

			/*
			 * 2015.01.11
			 * Änderung: Msn kan jezt auc vor AgentEntry erfast werde, daher AgentLevel nit sofort bekant.
			 * 
			if (!((int)FilterAgentLevel.Value == AgentLevel) &&
				!(SictAgentLevelOderAnySictEnum.Any == FilterAgentLevel.Value))
			{
				return false;
			}
			 * */

			if (SictAgentLevelOderAnySictEnum.Any == FilterAgentLevel.Value)
			{
				return true;
			}

			if (!AgentLevel.HasValue)
			{
				return null;
			}

			return (int)FilterAgentLevel.Value == AgentLevel;
		}

		public bool?	PasendZuMissionUndAgent(
			string MissionTitel,
			SictFactionSictEnum[]	MissionMengeFaction,
			int? AgentLevel)
		{
			if (!FilterMissionTitelPasend(MissionTitel))
			{
				return false;
			}

			return PasendZuAgentLevel(AgentLevel);
		}

		public SictKonfigMissionZuMissionFilterVerhalte Kopii()
		{
			return SictRefBaumKopii.ObjektKopiiErsctele(this);
		}

		public object Clone()
		{
			return Kopii();
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictPräferenzZuZaitVerhalte	:	ICloneable
	{
		/// <summary>
		/// Ab diisem Zaitpunkt geet nix meer.
		/// </summary>
		[JsonProperty]
		public Int64? DiinstUnterbrecungNääxteZait;

		/// <summary>
		/// Naac überscraite diiser Scranke sol Ship gedockt were.
		/// </summary>
		[JsonProperty]
		public Int64? InRaumAktioonZaitScrankeMili;

		/// <summary>
		/// Kaine Mission Accept naac überscraite diiser Scranke.
		/// </summary>
		[JsonProperty]
		public Int64? MissionAktioonAcceptZaitScrankeMili;

		/// <summary>
		/// Kaine Mission Füüre aus naac überscraite diiser Scranke.
		/// </summary>
		[JsonProperty]
		public Int64? MissionAktioonFüüreAusZaitScrankeMili;

		public SictPräferenzZuZaitVerhalte()
		{
		}

		public SictPräferenzZuZaitVerhalte Kopii()
		{
			return SictRefBaumKopii.ObjektKopiiErsctele(this);
		}

		public object Clone()
		{
			return Kopii();
		}

		static public SictPräferenzZuZaitVerhalte KombiniireNaacMinimum(
			SictPräferenzZuZaitVerhalte O0,
			SictPräferenzZuZaitVerhalte O1)
		{
			if (null == O0 && null == O1)
			{
				return null;
			}

			if (null == O0)
			{
				return O1.Kopii();
			}

			if (null == O1)
			{
				return O0.Kopii();
			}

			var Kombi = new	SictPräferenzZuZaitVerhalte();

			Kombi.DiinstUnterbrecungNääxteZait = Bib3.Glob.Min(O0.DiinstUnterbrecungNääxteZait, O1.DiinstUnterbrecungNääxteZait);
			Kombi.InRaumAktioonZaitScrankeMili = Bib3.Glob.Min(O0.InRaumAktioonZaitScrankeMili, O1.InRaumAktioonZaitScrankeMili);
			Kombi.MissionAktioonAcceptZaitScrankeMili = Bib3.Glob.Min(O0.MissionAktioonAcceptZaitScrankeMili, O1.MissionAktioonAcceptZaitScrankeMili);
			Kombi.MissionAktioonFüüreAusZaitScrankeMili = Bib3.Glob.Min(O0.MissionAktioonFüüreAusZaitScrankeMili, O1.MissionAktioonFüüreAusZaitScrankeMili);

			return Kombi;
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictOptimatParamMine
	{
		[JsonProperty]
		public bool? SurveyScannerFraigaabe;

		[JsonProperty]
		public OreTypSictEnum[] MengeOreTypFraigaabe;

		[JsonProperty]
		public bool? MengeOreTypeBescrankeNaacMiningCrystal;
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictOptimatParamMission
	{
		/// <summary>
		/// Maximaale Dauer naac der ain Raum abgebrocen werden sol. Damit werd auc der Pfaad abgebrocen.
		/// </summary>
		[JsonProperty]
		public int? AutoMissionFüüreAusRaumAlterScrankeMax;

		[JsonProperty]
		public bool? AktioonAcceptFraigaabe;

		[JsonProperty]
		public bool? AktioonDeclineFraigaabe;

		[JsonProperty]
		public bool? AktioonDeclineUnabhängigVonStandingLossFraigaabe;

		[JsonProperty]
		public string[] AktioonAcceptMengeAgentTypFraigaabe;

		[JsonProperty]
		public int[] AktioonAcceptMengeAgentLevelFraigaabe;

		/// <summary>
		/// Aktioon Decline werd auf Mission Offer angewendet für welce AgentLevel in AktioonAcceptMengeAgentLevelFraigaabe enthalte isc.
		/// </summary>
		[JsonProperty]
		public bool? AktioonDeclineAlsSctandardFürSonstigeFraigaabe;

		[JsonProperty]
		public SictKonfigMissionZuMissionFilterVerhalte[] MengeZuMissionFilterVerhalte;

		[JsonProperty]
		public KeyValuePair<string,	bool>[] SuuceAgentMengeStation;

		/// <summary>
		/// Bezaicner des Fitting welces für aine Mission geege angegebene Faction gelaade were sol.
		/// </summary>
		[JsonProperty]
		public KeyValuePair<string, string>[] MengeZuFactionFittingBezaicner;

		override public string ToString()
		{
			return JsonConvert.SerializeObject(this);
		}

		public SictKonfigMissionZuMissionFilterVerhalte ZuMissionVerhalteBerecne(
			SictMissionZuusctand	Mission)
		{
			if (null == Mission)
			{
				return null;
			}

			var MissionTitel = Mission.Titel;
			var AgentLevel = Mission.AgentLevel;
			var SecurityLevelMiliMinimum = Mission.SecurityLevelMinimumMili;

			return ZuMissionOfferVerhalteBerecne(
				MissionTitel,
				Mission.ObjectiveMengeFaction,
				AgentLevel,
				SecurityLevelMiliMinimum);
		}

		public SictKonfigMissionZuMissionFilterVerhalte ZuMissionOfferVerhalteBerecne(
			string MissionTitel,
			SictFactionSictEnum[] MissionMengeFaction,
			int? AgentLevel,
			int? SecurityLevelMiliMinimum)
		{
			var MengeMissionFilterPasend =
				AusMengeZuMissionFilterVerhalteEnthalteTailmengeZuMissionUndAgent(MissionTitel, MissionMengeFaction, AgentLevel)
				.ToArrayNullable();

			if(false)
			{
				//	2015.02.12	Filter Faction werd vorersct ignoriirt.

				/*
				 * !!!!	Temp:	2014.04.25	Zuusaz Bedingung Faction
				 * */

				if (MissionMengeFaction.NullOderLeer())
				{
					MengeMissionFilterPasend = null;
				}
			}

			var AktioonAcceptFraigaabe = this.AktioonAcceptFraigaabe ?? false;
			var AktioonDeclineFraigaabe = this.AktioonDeclineFraigaabe ?? false;
			var AktioonDeclineUnabhängigVonStandingLossFraigaabe = this.AktioonDeclineUnabhängigVonStandingLossFraigaabe ?? false;

			var AktioonDeclineAlsSctandardFürSonstigeFraigaabe = this.AktioonDeclineAlsSctandardFürSonstigeFraigaabe ?? false;

			var AktioonAcceptFraigaabeFürAgentLevel =
				AgentLevel.HasValue ?
				this.AktioonAcceptFraigaabeFürAgentLevel(AgentLevel.Value) :
				false;

			bool VerhalteSctandardDecline = false;

			if (AktioonDeclineAlsSctandardFürSonstigeFraigaabe &&
				AktioonDeclineFraigaabe	&&
				(AgentLevel.HasValue && AktioonAcceptFraigaabeFürAgentLevel)	||
				MengeMissionFilterPasend.NullOderLeer())
			{
				VerhalteSctandardDecline = true;
			}

			SictKonfigMissionZuMissionFilterVerhalte VerhalteSctandard = null;

			if (VerhalteSctandardDecline)
			{
				VerhalteSctandard = new SictKonfigMissionZuMissionFilterVerhalte(null, null);

				VerhalteSctandard.AktioonDeclineAktiiv = true;
				VerhalteSctandard.AktioonDeclineUnabhängigVonStandingLossAktiiv = AktioonDeclineUnabhängigVonStandingLossFraigaabe;
			}

			if (null == MengeMissionFilterPasend)
			{
				return VerhalteSctandard;
			}

			//	Scpezielere Filter sole hööher priorisiirt were.

			var MengeMissionFilterPasendOrdnet =
				MengeMissionFilterPasend
				.OrderBy((ZuMissionFilterVerhalte) => SictAgentLevelOderAnySictEnum.Any == ZuMissionFilterVerhalte.FilterAgentLevel)
				.ToArray();

			var ZuMissionFilterVerhaltePrioHööcste = MengeMissionFilterPasendOrdnet.FirstOrDefault();

			if (null == ZuMissionFilterVerhaltePrioHööcste)
			{
				return VerhalteSctandard;
			}

			var ZuMissionFilterVerhalteAbbild = ZuMissionFilterVerhaltePrioHööcste.Kopii();

			if (!(500 <= SecurityLevelMiliMinimum))
			{
				//	Mission Offer naac Low Sec werde nit Ausgefüürt oder accepted.
				ZuMissionFilterVerhalteAbbild.AktioonFüüreAusAktiiv = false;
				ZuMissionFilterVerhalteAbbild.AktioonAcceptAktiiv = false;
			}

			if (AgentLevel.HasValue)
			{
				if (!AktioonAcceptFraigaabeFürAgentLevel)
				{
					ZuMissionFilterVerhalteAbbild.AktioonAcceptAktiiv = false;
				}

				if (!AktioonFüüreAusFraigaabeFürAgentLevel(AgentLevel.Value))
				{
					ZuMissionFilterVerhalteAbbild.AktioonFüüreAusAktiiv = false;
				}
			}
			else
			{
				/*
				 * 2015.01.11
				 * Änderung: Msn kan jezt auc vor AgentEntry erfast werde, daher AgentLevel nit sofort bekant.
				 * 
				ZuMissionFilterVerhalteAbbild.AktioonAcceptAktiiv = false;
				ZuMissionFilterVerhalteAbbild.AktioonFüüreAusAktiiv = false;
				 * */
			}

			if (!(true == ZuMissionFilterVerhalteAbbild.AktioonAcceptAktiiv) &&
				VerhalteSctandardDecline)
			{
				ZuMissionFilterVerhalteAbbild.AktioonDeclineAktiiv = true;
			}

			if (!AktioonAcceptFraigaabe)
			{
				ZuMissionFilterVerhalteAbbild.AktioonAcceptAktiiv = false;
			}

			if (!AktioonDeclineFraigaabe)
			{
				ZuMissionFilterVerhalteAbbild.AktioonDeclineAktiiv = false;
			}

			if (!ZuMissionFilterVerhalteAbbild.AktioonDeclineUnabhängigVonStandingLossAktiiv.HasValue)
			{
				ZuMissionFilterVerhalteAbbild.AktioonDeclineUnabhängigVonStandingLossAktiiv = AktioonDeclineUnabhängigVonStandingLossFraigaabe;
			}

			return ZuMissionFilterVerhalteAbbild;
		}

		public bool AktioonFüüreAusFraigaabeFürAgentLevel(int AgentLevel)
		{
			return AktioonAcceptFraigaabeFürAgentLevel(AgentLevel);
		}

		public bool AktioonAcceptFraigaabeFürAgentLevel(int AgentLevel)
		{
			var AktioonAcceptMengeAgentLevelFraigaabe = this.AktioonAcceptMengeAgentLevelFraigaabe;

			if (null == AktioonAcceptMengeAgentLevelFraigaabe)
			{
				return false;
			}

			return AktioonAcceptMengeAgentLevelFraigaabe.Contains(AgentLevel);
		}

		public bool ZuMissionFilterAktioonFüüreAusNitAusgesclose(
			string MissionTitel,
			SictFactionSictEnum[] MissionMengeFaction,
			int? AgentLevel,
			int? SecurityLevelMiliMinimum)
		{
			var ZuMissionFilterVerhalte = ZuMissionOfferVerhalteBerecne(MissionTitel, MissionMengeFaction, AgentLevel, SecurityLevelMiliMinimum);

			if (null == ZuMissionFilterVerhalte)
			{
				return false;
			}

			return ZuMissionFilterVerhalte.AktioonFüüreAusAktiiv	?? true;
		}

		public IEnumerable<SictKonfigMissionZuMissionFilterVerhalte> AusMengeZuMissionFilterVerhalteEnthalteTailmengeZuMissionUndAgent(
			string	MissionTitel,
			SictFactionSictEnum[]	MissionMengeFaction,
			int?	AgentLevel)
		{
			var AktioonAcceptMengeAgentLevelFraigaabe = this.AktioonAcceptMengeAgentLevelFraigaabe;

			var Prädikaat = new Func<SictKonfigMissionZuMissionFilterVerhalte, bool>(
				(ZuMissionFilterVerhalte) =>
					ZuMissionFilterVerhalte.PasendZuMissionUndAgent(MissionTitel, MissionMengeFaction, AgentLevel)	?? true);

			return AusMengeZuMissionFilterVerhalteTailmenge(Prädikaat);
		}

		public IEnumerable<SictKonfigMissionZuMissionFilterVerhalte> AusMengeZuMissionFilterVerhalteTailmenge(
			Func<SictKonfigMissionZuMissionFilterVerhalte, bool> Prädikaat)
		{
			var MengeZuMissionFilterVerhalte = this.MengeZuMissionFilterVerhalte;

			if (null == MengeZuMissionFilterVerhalte)
			{
				return null;
			}

			return MengeZuMissionFilterVerhalte.Where(Prädikaat);
		}

		public bool InMengeZuMissionFilterVerhalteEnthalte(
			Func<SictKonfigMissionZuMissionFilterVerhalte, bool> Prädikaat)
		{
			var Tailmenge = AusMengeZuMissionFilterVerhalteTailmenge(Prädikaat);

			return 0 < Tailmenge.CountNullable();
		}

		public bool InMengeZuMissionFilterVerhalteEnthalteMitAktioonFüüreAus(
			Func<SictKonfigMissionZuMissionFilterVerhalte, bool> Prädikaat)
		{
			var PrädikaatKonjunktMitAktioonFüüreAus = new Func<SictKonfigMissionZuMissionFilterVerhalte, bool>(
				(ZuMissionFilterVerhalte) =>
				{
					if (null == ZuMissionFilterVerhalte)
					{
						return false;
					}

					if (!(true == ZuMissionFilterVerhalte.AktioonFüüreAusAktiiv))
					{
						return false;
					}

					return Prädikaat(ZuMissionFilterVerhalte);
				});

			return InMengeZuMissionFilterVerhalteEnthalte(PrädikaatKonjunktMitAktioonFüüreAus);
		}
	}

	/// <summary>
	/// Zwiscenlöösung zur Konfiguratioon welce Info aus Fitting und aus Mission kombiniirt.
	/// </summary>
	[JsonObject(MemberSerialization.OptIn)]
	public class SictOptimatParamFittingVerkürzt
	{
		[JsonProperty]
		public SictParamInRaumAktioonVerzwaigungNaacShipZuusctandScranke InRaumVerhalte;

		[JsonProperty]
		public KeyValuePair<string, string>[] MengeZuFactionAusFittingManagementFittingZuLaade;

		public SictOptimatParamFitting FittingErsctele(
			string	FittingBezaicner,
			string AusFittingManagementFittingZuLaade)
		{
			var Fitting = new SictOptimatParamFitting();

			Fitting.FittingBezaicner = FittingBezaicner;
			Fitting.AusFittingManagementFittingZuLaade = AusFittingManagementFittingZuLaade;

			/*
			 * 2014.01.07
			 * Ersaz durc InRaumVerhalteBaasis
			 * 
			Fitting.InRaumVerhalte = InRaumVerhalte;
			 * */

			return Fitting;
		}

		public	KeyValuePair<string,	SictOptimatParamFitting>[] MengeFittingErsctele()
		{
			var MengeZuFactionAusFittingManagementFittingZuLaade = this.MengeZuFactionAusFittingManagementFittingZuLaade;

			if (null == MengeZuFactionAusFittingManagementFittingZuLaade)
			{
				return null;
			}

			var MengeFitting = new List<KeyValuePair<string, SictOptimatParamFitting>>();

			foreach (var ZuFactionAusFittingManagementFittingZuLaade in MengeZuFactionAusFittingManagementFittingZuLaade)
			{
				var Fitting = FittingErsctele(ZuFactionAusFittingManagementFittingZuLaade.Value, ZuFactionAusFittingManagementFittingZuLaade.Value);

				MengeFitting.Add(new KeyValuePair<string, SictOptimatParamFitting>(
					ZuFactionAusFittingManagementFittingZuLaade.Key,	Fitting));
			}

			return
				MengeFitting.ToArray();
		}
	}

	public class SictOptimatParamFittingComparer : IEqualityComparer<SictOptimatParamFitting>
	{
		public bool Equals(
			SictOptimatParamFitting x,
			SictOptimatParamFitting	y)
		{
			return SictOptimatParamFitting.HinraicendGlaicwertigFürInVonNuzerParamIdent(x, y);
		}

		public int GetHashCode(SictOptimatParamFitting obj)
		{
			if (null == obj)
			{
				return 0;
			}

			return 0;

			return obj.GetHashCode();
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictOptimatParamFitting
	{
		[JsonProperty]
		public string FittingBezaicner;

		[JsonProperty]
		public string AusFittingManagementFittingZuLaade;

		[JsonProperty]
		public SictParamInRaumAktioonVerzwaigungNaacShipZuusctandScranke InRaumVerhalte;

		override public string ToString()
		{
			return JsonConvert.SerializeObject(this);
		}

		static public bool HinraicendGlaicwertigFürInVonNuzerParamIdent(
			SictOptimatParamFitting	O0,
			SictOptimatParamFitting	O1)
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
				string.Equals(O0.FittingBezaicner, O1.FittingBezaicner) &&
				string.Equals(O0.AusFittingManagementFittingZuLaade, O1.AusFittingManagementFittingZuLaade) &&
				SictParamInRaumAktioonVerzwaigungNaacShipZuusctandScranke.HinraicendGlaicwertigFürInVonNuzerParamIdent(O0.InRaumVerhalte, O1.InRaumVerhalte);
		}

		public SictOptimatParamFitting()
		{
		}

		public SictOptimatParamFitting(
			string FittingBezaicner,
			string AusFittingManagementFittingZuLaade	=	null,
			SictParamInRaumAktioonVerzwaigungNaacShipZuusctandScranke InRaumVerhalte	= null)
		{
			this.FittingBezaicner = FittingBezaicner;
			this.AusFittingManagementFittingZuLaade = AusFittingManagementFittingZuLaade;
			this.InRaumVerhalte = InRaumVerhalte;
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictVerzwaigungNaacShipZuusctandScranke
	{
		[JsonProperty]
		public	KeyValuePair<int,	bool>? ShieldScrankeBetraagMiliUndAin;

		[JsonProperty]
		public KeyValuePair<int, bool>? ArmorScrankeBetraagMiliUndAin;

		[JsonProperty]
		public KeyValuePair<int, bool>? StructScrankeBetraagMiliUndAin;

		[JsonProperty]
		public KeyValuePair<int, bool>? CapacitorScrankeBetraagMiliUndAin;

		static public bool HinraicendGlaicwertigFürInVonNuzerParamIdent(
			SictVerzwaigungNaacShipZuusctandScranke O0,
			SictVerzwaigungNaacShipZuusctandScranke O1)
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
				object.Equals(O0.ShieldScrankeBetraagMiliUndAin, O1.ShieldScrankeBetraagMiliUndAin) &&
				object.Equals(O0.ArmorScrankeBetraagMiliUndAin, O1.ArmorScrankeBetraagMiliUndAin) &&
				object.Equals(O0.StructScrankeBetraagMiliUndAin, O1.StructScrankeBetraagMiliUndAin) &&
				object.Equals(O0.CapacitorScrankeBetraagMiliUndAin, O1.CapacitorScrankeBetraagMiliUndAin);
		}

		public SictVerzwaigungNaacShipZuusctandScranke()
		{
		}

		public SictVerzwaigungNaacShipZuusctandScranke(
			int? ShieldScrankeBetraagMili,
			int? ArmorScrankeBetraagMili,
			int? StructScrankeBetraagMili,
			int? CapacitorScrankeBetraagMili)
		{
			this.ShieldScrankeBetraagMili = ShieldScrankeBetraagMili;
			this.ArmorScrankeBetraagMili = ArmorScrankeBetraagMili;
			this.StructScrankeBetraagMili = StructScrankeBetraagMili;
			this.CapacitorScrankeBetraagMili = CapacitorScrankeBetraagMili;
		}

		static int? ScrankeOoneScalterBinär(KeyValuePair<int, bool>? ScrankeMitScalterBinär)
		{
			if (!ScrankeMitScalterBinär.HasValue)
			{
				return null;
			}

			if (!ScrankeMitScalterBinär.Value.Value)
			{
				return null;
			}

			return ScrankeMitScalterBinär.Value.Key;
		}

		static KeyValuePair<int, bool>? BetraagMiliUndAinAusScrankeOoneScalterBinär(
			int? BetraagMili,
			KeyValuePair<int, bool>? ScrankeBetraagMiliUndAin)
		{
			if (!BetraagMili.HasValue)
			{
				return null;
			}

			return new	KeyValuePair<int, bool>(BetraagMili.Value, true);
		}

		public int? ShieldScrankeBetraagMili
		{
			set
			{
				ShieldScrankeBetraagMiliUndAin = BetraagMiliUndAinAusScrankeOoneScalterBinär(value, ShieldScrankeBetraagMiliUndAin);
			}

			get
			{
				return ScrankeOoneScalterBinär(ShieldScrankeBetraagMiliUndAin);
			}
		}

		public int? ArmorScrankeBetraagMili
		{
			set
			{
				ArmorScrankeBetraagMiliUndAin = BetraagMiliUndAinAusScrankeOoneScalterBinär(value, ArmorScrankeBetraagMiliUndAin);
			}

			get
			{
				return ScrankeOoneScalterBinär(ArmorScrankeBetraagMiliUndAin);
			}
		}

		public int? StructScrankeBetraagMili
		{
			set
			{
				StructScrankeBetraagMiliUndAin = BetraagMiliUndAinAusScrankeOoneScalterBinär(value, StructScrankeBetraagMiliUndAin);
			}

			get
			{
				return ScrankeOoneScalterBinär(StructScrankeBetraagMiliUndAin);
			}
		}

		public int? CapacitorScrankeBetraagMili
		{
			set
			{
				CapacitorScrankeBetraagMiliUndAin = BetraagMiliUndAinAusScrankeOoneScalterBinär(value, CapacitorScrankeBetraagMiliUndAin);
			}

			get
			{
				return ScrankeOoneScalterBinär(CapacitorScrankeBetraagMiliUndAin);
			}
		}

		override public string ToString()
		{
			return JsonConvert.SerializeObject(this);
		}

		static public bool TailNormiirtMiliAingehalte(
			SictTreferpunkteTotalUndRel Treferpunkte,
			int NormiirtMiliScrankeMin)
		{
			if (null == Treferpunkte)
			{
				return false;
			}

			if (!(NormiirtMiliScrankeMin <= Treferpunkte.NormiirtMili))
			{
				return false;
			}

			return true;
		}

		static public bool TailNormiirtMiliAingehalte(
			int? TreferpunkteRelMili,
			int NormiirtMiliScrankeMin)
		{
			if (!TreferpunkteRelMili.HasValue)
			{
				return false;
			}

			if (!(NormiirtMiliScrankeMin <= TreferpunkteRelMili))
			{
				return false;
			}

			return true;
		}

		public ShipHitpointsAndEnergy SictShipTreferpunkteUndCapacitorZuusctand()
		{
			var	StructScrankeBetraagMili	= this.StructScrankeBetraagMili;
			var	ArmorScrankeBetraagMili	= this.ArmorScrankeBetraagMili;
			var	ShieldScrankeBetraagMili	= this.ShieldScrankeBetraagMili;
			var	CapacitorScrankeBetraagMili	= this.CapacitorScrankeBetraagMili;

			var SictShipTreferpunkteUndCapacitorZuusctandAbbild = new ShipHitpointsAndEnergy(
				StructScrankeBetraagMili,
				ArmorScrankeBetraagMili,
				ShieldScrankeBetraagMili,
				CapacitorScrankeBetraagMili);

			return SictShipTreferpunkteUndCapacitorZuusctandAbbild;
		}

		public bool AingehalteDurcTreferpunkteUndCapacitor(
			ShipHitpointsAndEnergy Treferpunkte)
		{
			if (null == Treferpunkte)
			{
				return false;
			}

			var ShieldScrankeBetraagMili = this.ShieldScrankeBetraagMili;
			var ArmorScrankeBetraagMili = this.ArmorScrankeBetraagMili;
			var StructScrankeBetraagMili = this.StructScrankeBetraagMili;
			var CapacitorScrankeBetraagMili = this.CapacitorScrankeBetraagMili;

			if (ShieldScrankeBetraagMili.HasValue)
			{
				if (!TailNormiirtMiliAingehalte(Treferpunkte.Shield, ShieldScrankeBetraagMili.Value))
				{
					return false;
				}
			}

			if (ArmorScrankeBetraagMili.HasValue)
			{
				if (!TailNormiirtMiliAingehalte(Treferpunkte.Armor, ArmorScrankeBetraagMili.Value))
				{
					return false;
				}
			}

			if (StructScrankeBetraagMili.HasValue)
			{
				if (!TailNormiirtMiliAingehalte(Treferpunkte.Struct, StructScrankeBetraagMili.Value))
				{
					return false;
				}
			}

			if (CapacitorScrankeBetraagMili.HasValue)
			{
				if (!TailNormiirtMiliAingehalte(Treferpunkte.Capacitor, CapacitorScrankeBetraagMili.Value))
				{
					return false;
				}
			}

			return true;
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictParamInRaumAktioonVerzwaigungNaacShipZuusctandScranke
	{
		[JsonProperty]
		public SictVerzwaigungNaacShipZuusctandScranke GefectFortsazScranke;

		[JsonProperty]
		public SictVerzwaigungNaacShipZuusctandScranke GefectBaitritScranke;

		[JsonProperty]
		public SictVerzwaigungNaacShipZuusctandScranke BeweegungUnabhängigVonGefectScranke;

		[JsonProperty]
		public int? ModuleRegenAinScrankeMili;

		[JsonProperty]
		public int? ModuleRegenAusScrankeMili;

		override public string ToString()
		{
			return JsonConvert.SerializeObject(this);
		}

		static public bool HinraicendGlaicwertigFürInVonNuzerParamIdent(
			SictParamInRaumAktioonVerzwaigungNaacShipZuusctandScranke	O0,
			SictParamInRaumAktioonVerzwaigungNaacShipZuusctandScranke	O1)
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
				O0.ModuleRegenAinScrankeMili == O1.ModuleRegenAinScrankeMili &&
				O0.ModuleRegenAusScrankeMili == O1.ModuleRegenAusScrankeMili &&
				SictVerzwaigungNaacShipZuusctandScranke.HinraicendGlaicwertigFürInVonNuzerParamIdent(O0.GefectFortsazScranke, O1.GefectFortsazScranke) &&
				SictVerzwaigungNaacShipZuusctandScranke.HinraicendGlaicwertigFürInVonNuzerParamIdent(O0.GefectBaitritScranke, O1.GefectBaitritScranke) &&
				SictVerzwaigungNaacShipZuusctandScranke.HinraicendGlaicwertigFürInVonNuzerParamIdent(O0.BeweegungUnabhängigVonGefectScranke, O1.BeweegungUnabhängigVonGefectScranke);
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictOptimatParam
	{
		[JsonProperty]
		public bool? AutoFraigaabe;

		[JsonProperty]
		public bool? AutoPilotFraigaabe;

		[JsonProperty]
		public bool? AutoPilotLowSecFraigaabe;

		[JsonProperty]
		public bool? AutoMissionFraigaabe;

		[JsonProperty]
		public bool? AutoMineFraigaabe;

		[JsonProperty]
		public bool? AutoShipRepairFraigaabe;

		[JsonProperty]
		public bool? AutoChatLocalVerbergeNictFraigaabe;

		[JsonProperty]
		public bool? AutoChatLocalÖfneFraigaabe;

		[JsonProperty]
		public bool? AutoMobileTractorUnitFraigaabe;

		[JsonProperty]
		public SictPräferenzZuZaitVerhalte ZuZaitVerhalte;

		[JsonProperty]
		public SictParamInRaumAktioonVerzwaigungNaacShipZuusctandScranke InRaumVerhalteBaasis;

		[JsonProperty]
		public SictOptimatParamMine Mine;

		[JsonProperty]
		public SictOptimatParamMission Mission;

		[JsonProperty]
		public SictOptimatParamFitting[] MengeFitting;

		[JsonProperty]
		public bool? SimuFraigaabe;

		[JsonProperty]
		public SictOptimatParamSimu Simu;

		public SictOptimatParamSimu SimuNaacBerüksictigungFraigaabeBerecne()
		{
			var SimuFraigaabe = this.SimuFraigaabe;

			if (!(true == SimuFraigaabe))
			{
				return null;
			}

			return Simu;
		}

		override public string ToString()
		{
			return JsonConvert.SerializeObject(this);
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictOptimatParamSimu
	{
		/// <summary>
		/// Hiirmit köne di von Automaat berecnete Prio überscriibe were.
		/// </summary>
		[JsonProperty]
		public SictDamageMitBetraagIntValue[] VorgaabeFürGefectListeDamageTypePrio;

		[JsonProperty]
		public bool? MissionAnforderungFittingIgnoriire;

		[JsonProperty]
		public ShipState SelbstShipZuusctand;

		[JsonProperty]
		public bool? AufgaabeDistanceScteleAinObjektNääxteFraigaabe;

		[JsonProperty]
		public Int64? AufgaabeDistanceScteleAinObjektNääxteDistanceSol;

		[JsonProperty]
		public bool? AufgaabeOverviewScroll;

		[JsonProperty]
		public int? AufgaabeMausAufWindowVordersteEkeOderKanteIndex;

		override public string ToString()
		{
			return JsonConvert.SerializeObject(this);
		}

		static public bool HinraicendGlaicwertigFürIdentInOptimatParam(
			SictOptimatParamSimu O0,
			SictOptimatParamSimu O1)
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
				O0.AufgaabeOverviewScroll == O1.AufgaabeOverviewScroll &&
				O0.AufgaabeMausAufWindowVordersteEkeOderKanteIndex == O1.AufgaabeMausAufWindowVordersteEkeOderKanteIndex &&
				O0.MissionAnforderungFittingIgnoriire == O1.MissionAnforderungFittingIgnoriire &&
				O0.AufgaabeDistanceScteleAinObjektNääxteDistanceSol == O1.AufgaabeDistanceScteleAinObjektNääxteDistanceSol &&
				O0.AufgaabeDistanceScteleAinObjektNääxteFraigaabe == O1.AufgaabeDistanceScteleAinObjektNääxteFraigaabe &&
				Bib3.Glob.SequenceEqual(O0.VorgaabeFürGefectListeDamageTypePrio, O1.VorgaabeFürGefectListeDamageTypePrio) &&
				ShipState.HinraicendGlaicwertigFürIdentInOptimatParam(
				O0.SelbstShipZuusctand, O1.SelbstShipZuusctand);
		}
	}
}
