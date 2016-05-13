using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Bib3;
//using Optimat.EveOnline.AuswertGbs;
using Optimat.EveOnline.VonSensor;

namespace Optimat.EveOnline.Anwendung
{
	static public class Extension
	{
		static public IEnumerable<MenuEntryScpez> MengeEntryAgentMissionBerecne(
			this	Menu Menu)
		{
			return Menu.MengeEntryBerecneTailmenge(
				MenuEntryScpez.PrädikaatListeEntryAgentMissionTitel, MenuEntryScpez.SictEntryAgentMission);
		}

		static public IEnumerable<GbsElement>
			MengeAuswertungErgeebnisZuAstMitHerkunftAdrese(
			this	VonSensorikMesung ScnapscusAuswertung,
			Int64 AstHerkunftAdrese)
		{
			return
				Bib3.RefNezDiferenz.SictRefNezDiferenz.ZuRefNezMengeClrTypeInstanceReferenziirt(
				new object[] { ScnapscusAuswertung },
				Optimat.ScpezEveOnln.SictAutomat.ZuusctandSictDiferenzRictliinieScatescpaicer)
				.OfTypeNullable<GbsElement>()
				.WhereNullable((Kandidaat) => GbsElement.HatBezaicner(Kandidaat, AstHerkunftAdrese));
		}

		/// <summary>
		/// 2014.00.17.14 Bsp (als JSON):
		///   "OoberhalbDistanceListeZaile": ["Russo Tyristka", "[RUS D]", "Amarr Shuttle"]
		///   
		/// -> Kan auc Leerzaice enthalte.
		/// </summary>
		static readonly string TargetCorpTagRegexPattern = Regex.Escape("[") + "[\\w\\d\\s]+" + Regex.Escape("]");

		/// <summary>
		/// In Target wird in mance Fal der Naame des Objekt üüber meerere Zaile vertailt.
		/// Auser Naame kan z.B. auc noc Corp Tag in ekige Klamer in der Zaile welce deen lezte Tail des Naame enthält enthalte sain.
		/// Corp Tag kan auc in aigene Zaile oone andere Komponente enthalte sain.
		/// Diise Funktioon berecnet unter Berüksictigung diiser Beoobactunge meerere Kandidaate welce deem Name des Objekt abzüüglic Whitespace entscprece könte.
		/// 
		///
		/// 2013.09.15 Bsp (als JSON):
		///   "OoberhalbDistanceListeZaile": ["Denys Jinsing [SAK]", "Raven"]
		///
		/// 2014.00.17.14 Bsp (als JSON):
		///   "OoberhalbDistanceListeZaile": ["Russo Tyristka", "[RUS D]", "Amarr Shuttle"]
		/// </summary>
		/// <returns></returns>
		static public string[] MengeKandidaatObjektNameSictFürVerglaicBerecne(
			this	VonSensor.ShipUiTarget Target,
			int ZaileListeZaicenAnzaalScrankeMin)
		{
			var OoberhalbDistanceListeZaile = Target.ÜberDistanceListeZaile;

			if (null == OoberhalbDistanceListeZaile)
			{
				return null;
			}

			var CorpTagRegexPattern = TargetCorpTagRegexPattern;

			var MengeKandidaatObjektNameSictFürVerglaic = new List<string>();

			var KombiListeZaileAnzaalScrankeMax = 3;

			for (int KombiBeginZaileIndex = 0; KombiBeginZaileIndex < OoberhalbDistanceListeZaile.Length; KombiBeginZaileIndex++)
			{
				var KombiListeZaileAbbild = new List<string>();

				for (int KombiZaileIndex = KombiBeginZaileIndex; KombiZaileIndex < OoberhalbDistanceListeZaile.Length &&
					KombiZaileIndex < KombiBeginZaileIndex + KombiListeZaileAnzaalScrankeMax; KombiZaileIndex++)
				{
					var ZaileText = OoberhalbDistanceListeZaile[KombiZaileIndex];

					if (null == ZaileText)
					{
						continue;
					}

					var ZaileTextFürEndeVersionAbbild = ZaileText;

					var CorpTagListeMatch = Regex.Matches(ZaileText, CorpTagRegexPattern);

					if (null != CorpTagListeMatch)
					{
						var CorpTagListeMatchLezte = CorpTagListeMatch.OfType<Match>().LastOrDefault();

						if (null != CorpTagListeMatchLezte)
						{
							ZaileTextFürEndeVersionAbbild = ZaileText.Substring(0, CorpTagListeMatchLezte.Index);
						}
					}

					//	2013.10.18	Beobactung:	Zwiscedurc köne ac mool leere Zaile vorkome
					if (ZaileTextFürEndeVersionAbbild.Length < ZaileListeZaicenAnzaalScrankeMin)
					{
						break;
					}

					var KombiSictString = String.Join(null, KombiListeZaileAbbild.Concat(new string[] { ZaileTextFürEndeVersionAbbild }).ToArray());

					MengeKandidaatObjektNameSictFürVerglaic.Add(
						Optimat.EveOnline.Anwendung.SictOverviewUndTargetZuusctand.StringSictFürVerglaicZwisceTargetUndOverview(KombiSictString));

					KombiListeZaileAbbild.Add(ZaileText);
				}
			}

			return MengeKandidaatObjektNameSictFürVerglaic.ToArray();
		}

		static IEnumerable<SictGbsWindowZuusctand> MengeWindowEnthalteInWindowGrund(
			this	SictGbsZuusctand Gbs,
			SictGbsWindowZuusctand WindowParent)
		{
			if (null == Gbs)
			{
				yield break;
			}

			if (null == WindowParent)
			{
				yield break;
			}

			yield return WindowParent;

			var ScnapscusWindowLezte = WindowParent.ScnapscusWindowLezte;

			var ScnapscusWindowLezteWindowStack = ScnapscusWindowLezte as WindowStack;

			if (null == ScnapscusWindowLezteWindowStack)
			{
				yield break;
			}

			var WindowStackWindowAktiiv = ScnapscusWindowLezteWindowStack.WindowAktiiv;

			if (null == WindowStackWindowAktiiv)
			{
				yield break;
			}

			yield return Gbs.ZuHerkunftAdreseWindow(WindowStackWindowAktiiv.Ident);
		}

		static public IEnumerable<SictGbsWindowZuusctand> MengeWindowEnthalteInWindow(
			this	SictGbsZuusctand Gbs,
			SictGbsWindowZuusctand WindowParent)
		{
			return MengeWindowEnthalteInWindowGrund(Gbs, WindowParent).WhereNotDefault();
		}

		static public SictKonfigMissionZuMissionFilterVerhalte AusMengeZuMissionFilterVerhalteBestePasende(
			this	WindowAgentMissionInfo AusGbsWindowAgentMissionInfo,
			int? AgentLevel,
			IEnumerable<SictKonfigMissionZuMissionFilterVerhalte> MengeZuMissionFilterVerhalte)
		{
			if (null == MengeZuMissionFilterVerhalte)
			{
				return null;
			}

			var MissionTitel = AusGbsWindowAgentMissionInfo.MissionTitel;

			if (null == MissionTitel)
			{
				return null;
			}

			var MengeKandidaatFilterMissionTitelPasend =
				MengeZuMissionFilterVerhalte
				.Where((Kandidaat) =>
				{
					var FilterMissionTitelRegexPattern = Kandidaat.FilterMissionTitelRegexPattern;

					if (null == FilterMissionTitelRegexPattern)
					{
						return false;
					}

					var MissionTitelMatch = Regex.Match(MissionTitel, FilterMissionTitelRegexPattern);

					return MissionTitelMatch.Success;
				})
				.ToArray();

			var FundAgentLevelGlaic =
				MengeKandidaatFilterMissionTitelPasend
				.FirstOrDefault((Kandidaat) =>
				{
					var FilterAgentLevel = Kandidaat.FilterAgentLevel;

					if (!FilterAgentLevel.HasValue)
					{
						return false;
					}

					if (!((int)(FilterAgentLevel.Value) == AgentLevel))
					{
						return false;
					}

					return true;
				});

			if (null != FundAgentLevelGlaic)
			{
				return FundAgentLevelGlaic;
			}

			var FundAgentLevelAny =
				MengeKandidaatFilterMissionTitelPasend
				.FirstOrDefault((Kandidaat) =>
				{
					var FilterAgentLevel = Kandidaat.FilterAgentLevel;

					return FilterAgentLevel == SictAgentLevelOderAnySictEnum.Any;
				});

			return FundAgentLevelAny;
		}

		static public SictKonfigMissionZuMissionFilterVerhalte AusVonNuzerParamZuMissionVerhalte(
			this	WindowAgentMissionInfo AusGbsWindowAgentMissionInfo,
			int? AgentLevel,
			SictOptimatParam OptimatParam)
		{
			if (null == OptimatParam)
			{
				return null;
			}

			var OptimatParamMission = OptimatParam.Mission;

			if (null == OptimatParamMission)
			{
				return null;
			}

			var ZuMissionFilterVerhalte =
				AusGbsWindowAgentMissionInfo.AusMengeZuMissionFilterVerhalteBestePasende(
				AgentLevel,
				OptimatParamMission.MengeZuMissionFilterVerhalte);

			return ZuMissionFilterVerhalte;
		}

		static public bool PasendZuVonNuzerParamMissionAcceptFraigaabe(
			this	WindowAgentMissionInfo AusGbsWindowAgentMissionInfo,
			int? AgentLevel,
			SictOptimatParam OptimatParam)
		{
			var VonNuzerParamFürMissionVerhalte = AusGbsWindowAgentMissionInfo.AusVonNuzerParamZuMissionVerhalte(AgentLevel, OptimatParam);

			if (null == VonNuzerParamFürMissionVerhalte)
			{
				return false;
			}

			return true == VonNuzerParamFürMissionVerhalte.AktioonAcceptAktiiv;
		}


		static public Int64? Ident(this SictMissionZuusctand Mission) => Mission?.TailFürNuzer?.Ident;

		static public Int64? SictungFrühesteZaitMili(this SictMissionZuusctand Mission) => Mission?.TailFürNuzer?.SictungFrühesteZaitMili;

		static public Int64? EndeZaitMili(this SictMissionZuusctand Mission) => Mission?.TailFürNuzer?.EndeZaitMili;

		static public MissionLocation AgentLocation(this SictMissionZuusctand Mission) => Mission?.TailFürNuzer?.AgentLocation;

		static public string Titel(this SictMissionZuusctand Mission) => Mission?.TailFürNuzer?.Titel;

		static public string FürMissionFittingBezaicner(this SictMissionZuusctand Mission) => Mission?.TailFürNuzer?.FürMissionFittingBezaicner;

		static public bool? ConstraintFittingSatisfied(this SictMissionZuusctand Mission) => Mission?.TailFürNuzer?.ConstraintFittingSatisfied;

		static public IEnumerable<SictMissionLocationPfaadZuusctand> ListePfaad(this SictMissionZuusctand Mission)	=>
			Mission?.TailFürNuzer?.ListeLocationPfaad
			?.Select(LocationPfaad => LocationPfaad as SictMissionLocationPfaadZuusctand);

	}
}
