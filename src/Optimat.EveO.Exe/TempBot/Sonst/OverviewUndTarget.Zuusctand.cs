using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Optimat.ScpezEveOnln;
using Bib3;
using Optimat.EveOnline.Base;
//using Optimat.EveOnline.AuswertGbs;
using Optimat.EveOnline.Anwendung.AuswertGbs;
using Optimat.EveOnline.VonSensor;

namespace Optimat.EveOnline.Anwendung
{
	public enum SictShipClassEnum
	{
		Kain = 0,
		frigate,
		destroyer,
		cruiser,
		battlecruiser,
		battleship,
		sentry,
	}

	[JsonObject(MemberSerialization.OptIn)]
	public struct SictAusOverviewTypeUndName
	{
		[JsonProperty]
		public string Type;

		[JsonProperty]
		public string Name;
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictInRaumObjektGrupe
	{
		/// <summary>
		/// Filterkriterien um zuugehöörigkait aines Kandidaaten zu diiser Grupe zu entscaide.
		/// </summary>
		[JsonProperty]
		public SictStrategikonOverviewObjektFilter Identitäät;

		/// <summary>
		/// Aus Overview Zaile Type.
		/// </summary>
		[JsonProperty]
		public string Type;

		/// <summary>
		/// Aus Overview Zaile Name.
		/// </summary>
		[JsonProperty]
		public string Name;

		/// <summary>
		/// Color des IconMain aus Overview Zaile.
		/// </summary>
		[JsonProperty]
		public FarbeARGB IconMainColor;

		/// <summary>
		/// Annaame der Ship class.
		/// </summary>
		[JsonProperty]
		public SictWertMitZait<string>? ShipClassSictString;

		[JsonProperty]
		public SictWertMitZait<SictShipClassEnum?> ShipClassSictEnum;

		[JsonProperty]
		public SictWertMitZait<SictOverViewObjektZuusctand[]> OverviewViewportLezteMengeObjektZaile;

		public SictInRaumObjektGrupe()
		{
		}

		public SictInRaumObjektGrupe(VonSensor.OverviewZaile OverviewZaile)
		{
			if (null != OverviewZaile)
			{
				Type = OverviewZaile.Type;
				Name = OverviewZaile.Name;
				IconMainColor = OverviewZaile.IconMainColor;
			}
		}

		public bool ObjektPastInGrupe(VonSensor.OverviewZaile ObjektOverviewZaile)
		{
			if (null == ObjektOverviewZaile)
			{
				return false;
			}

			return
				string.Equals(ObjektOverviewZaile.Type, Type) &&
				string.Equals(ObjektOverviewZaile.Name, Name) &&
				FarbeARGB.Glaicwertig(ObjektOverviewZaile.IconMainColor, IconMainColor);
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictIndikatorTargetReprOverviewRow
	{
		[JsonProperty]
		readonly public SictTargetZuusctand Target;

		[JsonProperty]
		readonly public SictOverViewObjektZuusctand OverviewRow;

		public SictIndikatorTargetReprOverviewRow()
		{
		}

		public SictIndikatorTargetReprOverviewRow(
			SictTargetZuusctand Target,
			SictOverViewObjektZuusctand OverviewRow)
		{
			this.Target = Target;
			this.OverviewRow = OverviewRow;
		}
	}

	/// <summary>
	/// Samelt Info über Menge InRaumObjekt aus WindowOverview und LayerTarget.
	/// Ship Class: Frigate/Destroyer/Cruiser/Battlecruiser/Battleship/....
	/// EWar: Jam,WarpScramble,Web
	/// </summary>
	[JsonObject(MemberSerialization.OptIn)]
	public partial class SictOverviewUndTargetZuusctand
	{
		[JsonProperty]
		Int64? OverviewObjektSictungLezteAlterScrankeMax = 60 * 4;

		[JsonProperty]
		public List<SictOverViewObjektZuusctand> MengeOverViewObjekt
		{
			private set;
			get;
		}

		[JsonProperty]
		public List<SictTargetZuusctand> MengeTarget
		{
			private set;
			get;
		}

		public IEnumerable<SictTargetZuusctand> MengeTargetNocSictbar
		{
			get
			{
				return MengeTarget.WhereNullable((Kandidaat) => Kandidaat.InLezteScnapscusSictbar()).ToArrayNullable();
			}
		}

		[JsonProperty]
		public SictOverViewObjektZuusctand AusOverviewObjektInputFookusExklusiiv
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictWertMitZait<VonSensor.WindowOverView> ScnapscusLezteWindowOverViewMitZait
		{
			private set;
			get;
		}

		[JsonProperty]
		public string OverviewTabAktiivBezaicner
		{
			private set;
			get;
		}

		[JsonProperty]
		public KeyValuePair<string, SictOverviewObjektGrupeEnum[]>? OverviewPresetAktiivIdentUndMengeObjektGrupeSictbar
		{
			private set;
			get;
		}

		[JsonProperty]
		public KeyValuePair<string, SictOverviewObjektGrupeEnum[]>[] MengeZuOverviewTabMengeObjektGrupeSictbar
		{
			private set;
			get;
		}

		/// <summary>
		/// Ident des Preset welce in Overview gelaade werde sol.
		/// </summary>
		[JsonProperty]
		public string OverviewPresetZuLaadeIdent
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictZuInRaumObjektReprScnapscusZuusazinfo ScritLezteZuInRaumObjektScnapscusZuusazInfo
		{
			private set;
			get;
		}

		/// <summary>
		/// Folge der Overview Viewport welce als lezte begone und noc nict volsctändig ist.
		/// </summary>
		[JsonProperty]
		public SictOverviewPresetFolgeViewport OverviewPresetFolgeViewportAktuel
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictOverviewPresetFolgeViewport OverviewPresetFolgeViewportFertigLezte
		{
			private set;
			get;
		}

		/// <summary>
		/// Zu Name von Overview Preset di lezte Folge von Viewport welce (durc hinraicende Überlapung) ale Objekte in
		/// diisem Overview Preset erfast hat.
		/// </summary>
		[JsonProperty]
		public IDictionary<string, SictOverviewPresetFolgeViewport> DictZuOverviewPresetNameFolgeViewportVolsctändig
		{
			private set;
			get;
		}

		/// <summary>
		/// Zu Overview Objekt Grupe di lezte Folge von Viewport welce (durc hinraicende Überlapung) ale Objekte in
		/// ainem Overview Preset welces di Grupe enthält erfast hat.
		/// </summary>
		[JsonProperty]
		public IDictionary<SictOverviewObjektGrupeEnum, SictOverviewPresetFolgeViewport> DictZuOverviewObjektGrupeFolgeViewportVolsctändig
		{
			private set;
			get;
		}

		[JsonProperty]
		public KeyValuePair<SictWindowInventoryVerknüpfungMitOverview, SictOverViewObjektZuusctand[]>[] MengeZuWindowInventoryAuswaalReczMengeKandidaatInRaumObjekt
		{
			private set;
			get;
		}

		[JsonProperty]
		SictTargetZuusctand ScritLezteTargetInputFookusExklusiiv;

		[JsonProperty]
		SictOverViewObjektZuusctand ScritLezteOverviewRowInputFookusExklusiiv;

		[JsonProperty]
		readonly List<SictWertMitZait<SictIndikatorTargetReprOverviewRow>> ListeIndikatorTargetReprOverViewRow =
		new List<SictWertMitZait<SictIndikatorTargetReprOverviewRow>>();

		[JsonProperty]
		readonly List<KeyValuePair<SictOverViewObjektZuusctand, SictTargetZuusctand>> MengeAnnaameZuOverviewRowTarget =
		new List<KeyValuePair<SictOverViewObjektZuusctand, SictTargetZuusctand>>();

		[JsonProperty]
		readonly List<SictWertMitZait<VonSensor.MenuEntry[]>> ListeAusOverviewMenuLoadPresetListeEntry = new List<SictWertMitZait<VonSensor.MenuEntry[]>>();

		/// <summary>
		/// Menge der Overview Preset aus VonNuzerParam von deene Konsumente anneeme sole das diise feele.
		/// </summary>
		[JsonProperty]
		public string[] MengeOverviewPresetFeelend
		{
			private set;
			get;
		}

		/*
		 * 2015.00.04
		 * 
/// <summary>
/// Menge der Overview Tab aus VonNuzerParam von deene Konsumente anneeme sole das diise feele.
/// </summary>
[JsonProperty]
public string[] MengeOverviewTabFeelend
{
	private set;
	get;
}

[JsonProperty]
public string[] MengeOverviewPresetOoneTab
{
	private set;
	get;
}
		 * */

		/*
		 * 2015.00.04
		 * 
[JsonProperty]
public KeyValuePair<string,	SictOverviewObjektGrupeEnum[]>[] VonNuzerParamMengeOverviewPresetAbzüüglicFeelende
{
	private set;
	get;
}
		 * */

		[JsonProperty]
		Queue<SictWertMitZait<int>> InternListeZuZaitMengeZaileAbwaicendZuSortedDistanceAnzaal = new Queue<SictWertMitZait<int>>();

		[JsonProperty]
		public bool? SortedNaacDistanceNict
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? OverViewScrolledToTopLezteZait
		{
			private set;
			get;
		}

		public SictOverviewPresetFolgeViewport ZuPresetBerecneOverviewPresetFolgeViewportFertigLezte(OverviewPresetDefaultTyp PresetDefaultTyp)
		{
			return ZuPresetNameBerecneOverviewPresetFolgeViewportFertigLezte(PresetDefaultTyp.ToString());
		}

		public SictOverviewPresetFolgeViewport ZuPresetNameBerecneOverviewPresetFolgeViewportFertigLezte(string PresetName)
		{
			return
				DictZuOverviewPresetNameFolgeViewportVolsctändig
				.FirstOrDefaultNullable((Kandidaat) => string.Equals(Kandidaat.Key, PresetName, StringComparison.InvariantCultureIgnoreCase)).Value;
		}

		public IEnumerable<SictWertMitZait<int>> ListeZuZaitMengeZaileAbwaicendZuSortedDistanceAnzaal
		{
			get
			{
				return InternListeZuZaitMengeZaileAbwaicendZuSortedDistanceAnzaal;
			}
		}

		static readonly public string OverviewMenuEntryLaadePresetNaacTabRegexPattern = "load preset to tab";

		public SictOverViewObjektZuusctand InRaumObjektZuusctandFürOverviewZaile(
			VonSensor.OverviewZaile OverviewZaile)
		{
			if (null == OverviewZaile)
			{
				return null;
			}

			var MengeInRaumObjekt = this.MengeOverViewObjekt;

			if (null == MengeInRaumObjekt)
			{
				return null;
			}

			foreach (var InRaumObjekt in MengeInRaumObjekt)
			{
				if (null == InRaumObjekt)
				{
					continue;
				}

				var KandidaatOverviewZaileLezte = InRaumObjekt.OverviewZaileSictbarMitZaitLezte;

				if (!KandidaatOverviewZaileLezte.HasValue)
				{
					continue;
				}

				if (Optimat.EveOnline.Extension.HinraicendGlaicwertigFürIdent(KandidaatOverviewZaileLezte.Value.Wert, OverviewZaile))
				{
					return InRaumObjekt;
				}
			}

			return null;
		}

		static public string StringSictFürVerglaicZwisceTargetUndOverview(
			string String)
		{
			if (null == String)
			{
				return null;
			}

			return String.Replace(" ", "");
		}

		public SictTargetZuusctand[] MengeTargetTailmengePasendZuOverviewObjektBerecne(
			SictOverViewObjektZuusctand OverviewObjekt)
		{
			if (null == OverviewObjekt)
			{
				return null;
			}

			var MengeAnnaameZuOverviewRowTarget = this.MengeAnnaameZuOverviewRowTarget;

			if (null != MengeAnnaameZuOverviewRowTarget)
			{
				var AnnaameZuOverviewRowTarget = MengeAnnaameZuOverviewRowTarget.FirstOrDefault((Kandidaat) => Kandidaat.Key == OverviewObjekt);

				if (AnnaameZuOverviewRowTarget.Key == OverviewObjekt)
				{
					return new SictTargetZuusctand[] { AnnaameZuOverviewRowTarget.Value };
				}
			}

			var MengeTarget = this.MengeTargetNocSictbar;

			if (null == MengeTarget)
			{
				return null;
			}

			return
				MengeTarget
				.Where((KandidaatTarget) => OverViewObjektPastZuTarget(OverviewObjekt, KandidaatTarget))
				.ToArrayNullable();
		}

		public SictOverViewObjektZuusctand[] MengeOverviewObjektTailmengePasendZuTargetBerecne(
			SictTargetZuusctand Target)
		{
			if (null == Target)
			{
				return null;
			}

			var MengeAnnaameZuOverviewRowTarget = this.MengeAnnaameZuOverviewRowTarget;

			if (null != MengeAnnaameZuOverviewRowTarget)
			{
				var AnnaameZuOverviewRowTarget = MengeAnnaameZuOverviewRowTarget.FirstOrDefault((Kandidaat) => Kandidaat.Value == Target);

				if (AnnaameZuOverviewRowTarget.Value == Target)
				{
					return new SictOverViewObjektZuusctand[] { AnnaameZuOverviewRowTarget.Key };
				}
			}

			var MengeOverViewObjekt = this.MengeOverViewObjekt;

			if (null == MengeOverViewObjekt)
			{
				return null;
			}

			return
				MengeOverViewObjekt
				.Where((KandidaatOverViewObjekt) => OverViewObjektPastZuTarget(KandidaatOverViewObjekt, Target))
				.ToArrayNullable();
		}

		static public bool OverViewObjektPastZuTarget(
			SictOverViewObjektZuusctand OverviewObjekt,
			SictTargetZuusctand Target)
		{
			if (null == OverviewObjekt)
			{
				return false;
			}

			if (null == Target)
			{
				return false;
			}

			if (!OverViewObjektTypeOderNaamePasendZuTargetBescriftung(OverviewObjekt, Target))
			{
				return false;
			}

			var OverviewObjektSictungLezteDistanceScrankeMin =
				OverviewObjekt?.AingangUnglaicDefaultZuZaitLezte?.Wert.Key?.DistanceMin	??
                OverviewObjekt.SictungLezteDistanceScrankeMinScpezOverview;

			var OverviewObjektSictungLezteDistanceScrankeMax =
				OverviewObjekt?.AingangUnglaicDefaultZuZaitLezte?.Wert.Key?.DistanceMax ??
				OverviewObjekt.SictungLezteDistanceScrankeMaxScpezOverview;

			var TargetDistanceScrankeMin = Target.SictungLezteDistanceScrankeMinScpezTarget;
			var TargetDistanceScrankeMax = Target.SictungLezteDistanceScrankeMaxScpezTarget;

			if (OverviewObjekt.SaitSictbarkaitLezteListeScritAnzaal <= 0)
			{
				var ScnapshusLezteTarget = OverviewObjekt?.AingangUnglaicDefaultZuZaitLezte?.Wert.Key?.IconTargetedByMeSictbar ?? false;

				if (!(true == OverviewObjekt.TargetingOderTargeted || ScnapshusLezteTarget))
				{
					return false;
				}

				if (!(OverviewObjektSictungLezteDistanceScrankeMin - 1777 < TargetDistanceScrankeMax &&
					TargetDistanceScrankeMin - 1777 < OverviewObjektSictungLezteDistanceScrankeMax))
				{
					return false;
				}
			}

			return true;
		}

		static public bool OverViewObjektTypeOderNaamePasendZuTargetBescriftung(
			SictOverViewObjektZuusctand OverviewObjekt,
			SictTargetZuusctand Target)
		{
			if (null == OverviewObjekt || null == Target)
			{
				return false;
			}

			/*
				* 2014.04.27
				* Baiscpiil aus:
				* \\rs211275.rs.hosteurope.de\Optimat.Demo 0 .Berict\Berict\Berict.Nuzer\[ZAK=2014.04.26.21.20.10,NB=6].Anwendung.Berict:
				* "Wifrerante Vaydaerer" Level2
				* "The Blockade", "The_Blood_Raider_Covenant"
				* OverviewZaile.Type:"Starbase Stasis Tower"
				* OverviewZaile.Name:"Amarr Stasis Tower"
				* Target.OoberhalbDistanceListeZaile[0]:"Starbase Stasis Tower"
				* ->
				* OverviewZaile.Name nict enthalte in Target.OoberhalbDistanceListeZaile
				*
				* */
			var TargetOoberhalbDistanceListeZaile = Target.OoberhalbDistanceListeZaile;

			if (null == TargetOoberhalbDistanceListeZaile)
			{
				return false;
			}

			var OverviewObjektTypeAbbild = StringSictFürVerglaicZwisceTargetUndOverview(OverviewObjekt.Type);
			var OverviewObjektNameAbbild = StringSictFürVerglaicZwisceTargetUndOverview(OverviewObjekt.Name);
			var TargetBescriftungAbbild = StringSictFürVerglaicZwisceTargetUndOverview(string.Join("", TargetOoberhalbDistanceListeZaile));

			if (null == TargetBescriftungAbbild)
			{
				return false;
			}

			if (null != OverviewObjektTypeAbbild)
			{
				if (Regex.Match(TargetBescriftungAbbild, OverviewObjektTypeAbbild, RegexOptions.IgnoreCase).Success)
				{
					return true;
				}
			}

			if (null != OverviewObjektNameAbbild)
			{
				if (Regex.Match(TargetBescriftungAbbild, OverviewObjektNameAbbild, RegexOptions.IgnoreCase).Success)
				{
					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// Gibt di Untermenge zurük für welce naac deer Zait <paramref name="SictOverviewObjektGrupeEnum[] MengeObjektGrupe"/> kaine Volsctändige Mesung meer durcgefüürt wurde.
		/// </summary>
		/// <param name="MengeObjektGrupe"></param>
		/// <param name="ZaitScrankeMin"></param>
		/// <returns></returns>
		public SictOverviewObjektGrupeEnum[] MengeObjektGrupeUntermengeOoneOverviewViewportFolgeVolsctändigNaacZait(
			SictOverviewObjektGrupeEnum[] MengeObjektGrupe,
			Int64 ZaitScrankeMin)
		{
			if (null == MengeObjektGrupe)
			{
				return null;
			}

			var DictZuOverviewObjektGrupeFolgeViewportVolsctändig = this.DictZuOverviewObjektGrupeFolgeViewportVolsctändig;

			if (null == DictZuOverviewObjektGrupeFolgeViewportVolsctändig)
			{
				return MengeObjektGrupe;
			}

			var MengeObjektGrupeUntermengeMitOverviewViewportFolgeVolsctändigNaacZait = MengeObjektGrupe.Take(0).ToList();

			foreach (var ObjektGrupe in MengeObjektGrupe.Distinct())
			{
				var ZuObjektGrupeFolge = Optimat.Glob.TAD(DictZuOverviewObjektGrupeFolgeViewportVolsctändig, ObjektGrupe);

				if (null == ZuObjektGrupeFolge)
				{
					//	Zu diiser Objekt Grupe gaab es noc kaine Mesung.
					continue;
				}

				if (!(ZaitScrankeMin <= ZuObjektGrupeFolge.BeginZait))
				{
					continue;
				}

				MengeObjektGrupeUntermengeMitOverviewViewportFolgeVolsctändigNaacZait.Add(ObjektGrupe);
			}

			var MengeObjektGrupeUntermengeOoneOverviewViewportFolgeVolsctändigNaacZait =
				MengeObjektGrupe.Except(MengeObjektGrupeUntermengeMitOverviewViewportFolgeVolsctändigNaacZait).ToArray();

			return MengeObjektGrupeUntermengeOoneOverviewViewportFolgeVolsctändigNaacZait;
		}

		public Int64? ZuOverviewObjektGrupeFolgeVolsctändigBeginZaitFrüheste(
			SictOverviewObjektGrupeEnum ObjektGrupe)
		{
			return ZuMengeOverviewObjektGrupeFolgeVolsctändigBeginZaitFrüheste(
				new SictOverviewObjektGrupeEnum[] { ObjektGrupe });
		}

		/// <summary>
		/// Gibt deen scpäätesten Zaitpunkt zurük naacdeem für ale mit <paramref name="MengeObjektGrupe"/> angegeebene Overview Objekt Grupe Overview Folge ersctelt wurden welce
		/// ale Objekte in der jewailigen Type Selection umfasten.
		/// </summary>
		/// <param name="MengeObjektGrupe"></param>
		/// <returns></returns>
		public Int64? ZuMengeOverviewObjektGrupeFolgeVolsctändigBeginZaitFrüheste(
			SictOverviewObjektGrupeEnum[] MengeObjektGrupe)
		{
			if (null == MengeObjektGrupe)
			{
				return null;
			}

			var DictZuOverviewObjektGrupeFolgeViewportVolsctändig = this.DictZuOverviewObjektGrupeFolgeViewportVolsctändig;

			if (null == DictZuOverviewObjektGrupeFolgeViewportVolsctändig)
			{
				return null;
			}

			Int64? FolgeFrühesteBeginZait = null;

			foreach (var ObjektGrupe in MengeObjektGrupe)
			{
				var ZuObjektGrupeFolge = Optimat.Glob.TAD(DictZuOverviewObjektGrupeFolgeViewportVolsctändig, ObjektGrupe);

				if (null == ZuObjektGrupeFolge)
				{
					//	Zu diiser Objekt Grupe gaab es noc kaine Mesung.
					return null;
				}

				var ZuObjektGrupeFolgeBeginZait = ZuObjektGrupeFolge.BeginZait;

				if (!ZuObjektGrupeFolgeBeginZait.HasValue)
				{
					return null;
				}

				FolgeFrühesteBeginZait = Bib3.Glob.Min(FolgeFrühesteBeginZait, ZuObjektGrupeFolgeBeginZait.Value);
			}

			return FolgeFrühesteBeginZait;
		}

	}
}
