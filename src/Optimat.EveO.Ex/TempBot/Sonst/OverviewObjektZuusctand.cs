using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Optimat.ScpezEveOnln;
using Bib3;
using Bib3.RefNezDiferenz;
//using Optimat.EveOnline.AuswertGbs;
using Optimat.EveOnline.Base;
using Optimat.EveOnline.VonSensor;
using ExtractFromOldAssembly.Bib3;

namespace Optimat.EveOnline.Anwendung
{
	/// <summary>
	/// 
	/// Hiir werden Info aus meerere Scnapscus zu ainem InRaumObjekt aus Overview Zaile aggregiirt.
	/// Zum hersctelen des zusamehang von Overview Zaile zwisce versciidene Scnapscus werd vorerst aine Metoode angewandt welce sctark von der Sensoorik abhängt und bai
	/// Umsctelung auf Sensorik per Window Client Raster warscainlic waiterverwendet were kan: Als Identitäät werd di Adrese des OverviewScrollEntry Python Object verwendet solange
	/// di wictigere Bedingunge (Type, Name und MainIcon Color identisc) erfült sind.
	/// </summary>
	[JsonObject(MemberSerialization.OptIn)]
	public class SictOverViewObjektZuusctand : SictInRaumObjektReprZuusctand<VonSensor.OverviewZaile>
	{
		/// <summary>
		/// 2014.09.04	Bsp:
		/// "Asteroid (Veldspar)"
		/// </summary>
		/*
		 * 2014.09.25
		 * 
		const string OverviewObjektNameRegexPattern = "([^\\(]+)(\\([^\\)]*\\)|$)";
		 * */
		const string OverviewObjektNameRegexPattern = "roid([^\\(]*)\\(([^\\)]*)\\)";

		static public bool OverviewObjektBescriftungIstAsteroid(
			SictOverViewObjektZuusctand OverviewObjekt)
		{
			string OreTypSictString;
			OreTypSictEnum? OreTyp;

			return OverviewObjektBescriftungIstAsteroid(OverviewObjekt, out OreTypSictString, out OreTyp);
		}

		static public bool OverviewObjektBescriftungIstAsteroid(
			SictOverViewObjektZuusctand OverviewObjekt,
			out string OreTypSictString,
			out OreTypSictEnum? OreTyp)
		{
			OreTypSictString = null;
			OreTyp = null;

			if (null == OverviewObjekt)
			{
				return false;
			}

			var OverviewObjektName = OverviewObjekt.Name;
			var OverviewObjektType = OverviewObjekt.Type;

			if (0 < OverviewObjektName.CountNullable())
			{
				var NameMatch = Regex.Match(
					OverviewObjektName, OverviewObjektNameRegexPattern, RegexOptions.IgnoreCase);

				if (NameMatch.Success)
				{
					OreTypSictString = NameMatch.Groups[2].Value.Trim();

					OreTyp = TempAuswertGbs.Extension.OreTypBerecneAusOreTypSictString(OreTypSictString);

					return true;
				}
			}

			OreTyp = TempAuswertGbs.Extension.OreTypBerecneAusOreTypSictString(OverviewObjektType);

			if (OreTyp.HasValue)
			{
				OreTypSictString = OverviewObjektType;

				return true;
			}

			return false;
		}

		public Int64? DistanceScrankeMinKombi
		{
			get
			{
				var AusViewportFolgeLezteDistanceScrankeMin = this.AusViewportFolgeLezteDistanceScrankeMin;

				return Bib3.Glob.Max((AusViewportFolgeLezteDistanceScrankeMin ?? default(SictWertMitZait<Int64>)).Wert, SictungLezteDistanceScrankeMinScpezOverview);
			}
		}

		public Int64? DistanceScrankeMaxKombi
		{
			get
			{
				return Bib3.Glob.Max(SictungLezteDistanceScrankeMaxScpezOverview, DistanceScrankeMinKombi);
			}
		}

		[JsonProperty]
		public SictWertMitZait<Int64>? AusViewportFolgeLezteDistanceScrankeMin
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? SictungLezteDistanceScrankeMinScpezOverview
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? SictungLezteDistanceScrankeMaxScpezOverview
		{
			private set;
			get;
		}

		[JsonProperty]
		public string SictungFrühesteType
		{
			private set;
			get;
		}

		[JsonProperty]
		public string SictungFrühesteName
		{
			private set;
			get;
		}

		[JsonProperty]
		public string Type
		{
			private set;
			get;
		}

		[JsonProperty]
		public string Name
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? Targeted
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? Targeting
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? TargetingOderTargeted
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? AttackingMe
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? InWarpDistance
		{
			private set;
			get;
		}

		/// <summary>
		/// Sätigung der Color des IconMain ist hinraicend gering das davon ausgegange werde kan das Objekt nit gelb (gehört andere Scpiiler/Corp) oder blau (Abandoned) isc.
		/// z.B. Wais oder Grau oder Scwarz.
		/// </summary>
		[JsonProperty]
		public bool? IconMainColorSätigungGering
		{
			private set;
			get;
		}

		/// <summary>
		/// Overview Preset in welcen das Objekt scon gesictet wurde.
		/// </summary>
		[JsonProperty]
		public IDictionary<string, Int64> DictZuOverviewPresetSictungLezteZait
		{
			private set;
			get;
		}

		public SictWertMitZait<VonSensor.OverviewZaile>? OverviewZaileMitZaitLezte
		{
			get
			{
				return AingangScnapscusTailObjektIdentMitZaitLezteBerecne();
			}
		}

		[JsonProperty]
		public SictWertMitZait<VonSensor.OverviewZaile>? OverviewZaileSictbarMitZaitLezte
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictWertMitZait<VonSensor.OverviewZaile>? OverviewZaileSictbarMitZaitVorLezte
		{
			private set;
			get;
		}

		public Int64? SictungFrühesteZait
		{
			get
			{
				return this.ScnapscusFrühesteZait;
			}
		}

		public Int64? SictungLezteZait
		{
			get
			{
				return AingangUnglaicDefaultLezteZait;
			}
		}

		/// <summary>
		/// Anzaal der Messcrite sait lezte Sictbarkait.
		/// glaic 0 solange Objekt in Overview sictbar.
		/// </summary>
		[JsonProperty]
		public int? SaitSictbarkaitLezteListeScritAnzaal
		{
			private set;
			get;
		}

		/// <summary>
		/// War in lezte Scrit nict sictbar und kan auc nict durc Änderung des Overview Viewport (durc Wexel Tab oder Scrole) sictbar gemact werde.
		/// </summary>
		[JsonProperty]
		public bool? ScritLezteSictbarAusgesclose
		{
			private set;
			get;
		}

		/// <summary>
		/// Zait lezter nict näher scpezifiziirter EWar Wirkung.
		/// </summary>
		[JsonProperty]
		public Int64? EWarWirkungLezteZait
		{
			private set;
			get;
		}

		[JsonProperty]
		public IDictionary<SictEWarTypeEnum, Int64> DictZuEWarTypeWirkungLezteZait
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? IstAsteroid
		{
			private set;
			get;
		}

		[JsonProperty]
		public string ScnapscusVorLezteOverviewTypeSelectionName
		{
			private set;
			get;
		}

		[JsonProperty]
		public string ScnapscusLezteOverviewTypeSelectionName
		{
			private set;
			get;
		}


		override public bool MenuWurzelPasendZuInRaumObjektRepr(
			GbsElement MenuWurzel)
		{
			if (null == MenuWurzel)
			{
				return false;
			}

			var MenuWurzelAlsOverviewRowInterpretiirt = MenuWurzel as VonSensor.OverviewZaile;
			var MenuWurzelAlsOverviewRow = MenuWurzel as VonSensor.OverviewZaile;

			if (null != MenuWurzelAlsOverviewRowInterpretiirt)
			{
				return MenuWurzelAlsOverviewRowInterpretiirt.Ident == GbsAstHerkunftAdrese;
			}

			if (null != MenuWurzelAlsOverviewRow)
			{
				return MenuWurzelAlsOverviewRow.Ident == GbsAstHerkunftAdrese;
			}

			return false;
		}

		public Int64? ZuMengeEWarLezteZait(SictEWarTypeEnum[] MengeEWarType)
		{
			if (null == MengeEWarType)
			{
				return null;
			}

			var DictZuEWarTypeWirkungLezteZait = this.DictZuEWarTypeWirkungLezteZait;

			if (null == DictZuEWarTypeWirkungLezteZait)
			{
				return null;
			}

			Int64? BisherLezte = null;

			foreach (var ZuEWarTypeWirkungLezteZait in DictZuEWarTypeWirkungLezteZait)
			{
				if (MengeEWarType.Contains(ZuEWarTypeWirkungLezteZait.Key))
				{
					BisherLezte = Bib3.Glob.Max(BisherLezte, ZuEWarTypeWirkungLezteZait.Value);
				}
			}

			return BisherLezte;
		}

		public const int ListeScnapscusOverviewZaileAnzaalScrankeMax = 10;

		static public bool ZaileIscAusgewäält(
			SictWertMitZait<VonSensor.OverviewZaile>? OverviewZaileMitZaitNulbar)
		{
			if (!OverviewZaileMitZaitNulbar.HasValue)
			{
				return false;
			}

			return OverviewZaileMitZaitNulbar.Value.Wert.HatInputFookus();
		}

		override protected void NaacAingangScnapscus(
			Int64 Zait,
			VonSensor.OverviewZaile ScnapscusWert,
			SictZuInRaumObjektReprScnapscusZuusazinfo ZuusazInfo)
		{
			var MengeZuEWarTypeTextureIdent = (null == ZuusazInfo) ? null : ZuusazInfo.MengeZuEWarTypeTextureIdent;
			var OverviewPresetFolgeViewportFertigLezte = (null == ZuusazInfo) ? null : ZuusazInfo.OverviewPresetFolgeViewportFertigLezte;

			var ScnapscusOverviewZaileMitZait = new SictWertMitZait<VonSensor.OverviewZaile>(Zait, ScnapscusWert);

			var BisherOverviewZaileSictbarMitZaitLezte = this.OverviewZaileSictbarMitZaitLezte;

			if (BisherOverviewZaileSictbarMitZaitLezte.HasValue)
			{
				if (BisherOverviewZaileSictbarMitZaitLezte.Value.Zait < Zait)
				{
					OverviewZaileSictbarMitZaitVorLezte = BisherOverviewZaileSictbarMitZaitLezte;
				}
				else
				{
				}
			}

			if (null != ScnapscusOverviewZaileMitZait.Wert)
			{
				OverviewZaileSictbarMitZaitLezte = ScnapscusOverviewZaileMitZait;
			}

			this.ScnapscusVorLezteOverviewTypeSelectionName = ScnapscusLezteOverviewTypeSelectionName;
			this.ScnapscusLezteOverviewTypeSelectionName = ZuusazInfo.OverviewTypeSelectionName;

			/*
			2015.09.03	Änderung:
			Vorzuug berecnung SaitSictbarkaitLezteListeScritAnzaal.
			*/
			if (null == ScnapscusOverviewZaileMitZait.Wert)
			{
				++SaitSictbarkaitLezteListeScritAnzaal;
			}
			else
			{
				SaitSictbarkaitLezteListeScritAnzaal = 0;
			}

			AusListeScnapscusOverviewZaileBerecneAbgelaitete();

			if (null == ScnapscusOverviewZaileMitZait.Wert)
			{
				/*
				2015.09.03	Änderung:
				Vorzuug berecnung SaitSictbarkaitLezteListeScritAnzaal.
					++SaitSictbarkaitLezteListeScritAnzaal;
					*/
			}
			else
			{
				SaitSictbarkaitLezteListeScritAnzaal = 0;

				var ScnapscusOverviewZaileAusOverviewZaile = ScnapscusOverviewZaileMitZait.Wert;

				if (null != ScnapscusOverviewZaileAusOverviewZaile)
				{
					{
						//	Ermitlung EWar

						var RightAlignedIconMengeTextureIdent =
							ScnapscusOverviewZaileAusOverviewZaile.RightAlignedMengeIconTextureIdent.ToArrayFalsNitLeer();

						if (null != RightAlignedIconMengeTextureIdent)
						{
							if (0 < RightAlignedIconMengeTextureIdent.Length)
							{
								//	Hiir wird davon ausgegangen das jeedes Icon welces im AlignedIconContainer erscaint ain EWar Icon ist.
								EWarWirkungLezteZait = Bib3.Glob.Max(EWarWirkungLezteZait, Zait);
							}

							var DictZuEWarTypeWirkungLezteZait = this.DictZuEWarTypeWirkungLezteZait;

							foreach (var RightAlignedIconTextureIdent in RightAlignedIconMengeTextureIdent)
							{
								foreach (var ZuEWarTypeTextureIdent in MengeZuEWarTypeTextureIdent)
								{
									if (ZuEWarTypeTextureIdent.Value == RightAlignedIconTextureIdent)
									{
										if (null == DictZuEWarTypeWirkungLezteZait)
										{
											DictZuEWarTypeWirkungLezteZait = new Dictionary<SictEWarTypeEnum, Int64>();
										}

										DictZuEWarTypeWirkungLezteZait[ZuEWarTypeTextureIdent.Key] = ScnapscusOverviewZaileMitZait.Zait;
									}
								}
							}

							this.DictZuEWarTypeWirkungLezteZait = DictZuEWarTypeWirkungLezteZait;
						}
					}
				}
			}

			var DictZuOverviewPresetSictungLezteZait = this.DictZuOverviewPresetSictungLezteZait;

			if (0 < SaitSictbarkaitLezteListeScritAnzaal)
			{
				if (null != OverviewPresetFolgeViewportFertigLezte && null != DictZuOverviewPresetSictungLezteZait)
				{
					var OverviewPresetFolgeViewportFertigLezteEndeObjektDistance = OverviewPresetFolgeViewportFertigLezte.EndeObjektDistance;

					var MengeFolgeViewportAusscliisend = new List<SictOverviewPresetFolgeViewport>();

					if (DictZuOverviewPresetSictungLezteZait.Any((Kandidaat) =>
						string.Equals(Kandidaat.Key, OverviewPresetFolgeViewportFertigLezte.OverviewPresetName, StringComparison.InvariantCultureIgnoreCase)) &&
						OverviewPresetFolgeViewportFertigLezte.BeginScrollHandleFläceGrenzeOobnAntailAnGesamtMili < 3 &&
						OverviewPresetFolgeViewportFertigLezteEndeObjektDistance.HasValue)
					{
						AusViewportFolgeLezteDistanceScrankeMin = new SictWertMitZait<Int64>(
							OverviewPresetFolgeViewportFertigLezte.BeginZait ?? -1,
							OverviewPresetFolgeViewportFertigLezteEndeObjektDistance.Value);
					}
				}
			}
			else
			{
				AusViewportFolgeLezteDistanceScrankeMin = null;
			}

			{
				//	Des hiir sctimt nit, hiir werd noc nit ausgesclose das Objekt durc Scrole oder Wexel Tab sictbar gemact werde könt.
				ScritLezteSictbarAusgesclose = 1 == SaitSictbarkaitLezteListeScritAnzaal;
			}

			base.NaacAingangScnapscus(Zait, ScnapscusWert, ZuusazInfo);
		}

		Int64? DebugAusListeScnapscusOverviewZaileBerecneAbgelaitete_0_Zait;
		Int64? DebugAusListeScnapscusOverviewZaileBerecneAbgelaitete_1_Zait;

		/// <summary>
		/// Berecnet Aigenscafte für Konsumente aus lezte und Vorlezte Scnapscus.
		/// Geleegentlic enthalte di Bescriftunge aus der Sensoorik zuufälige Werte.
		/// Daher werd bai Berecnung von Distance, Type, Name, usw. auc der Wert aus deem vorherigem Scnapscus berüksictigt.
		/// </summary>
		void AusListeScnapscusOverviewZaileBerecneAbgelaitete()
		{
			Int64? DistanceScrankeMin = null;
			Int64? DistanceScrankeMax = null;
			var Type = this.Type;
			var Name = this.Name;
			var Targeted = this.Targeted;
			bool? Targeting = null;
			var TargetingOderTargeted = this.TargetingOderTargeted;
			bool? AttackingMe = null;
			bool? IconMainColorSätigungGering = null;
			bool? IstAsteroid = null;

			try
			{
				DebugAusListeScnapscusOverviewZaileBerecneAbgelaitete_0_Zait = OverviewZaileMitZaitLezte?.Zait;

				var OverviewZaileSictbarMitZaitLezteNulbar = this.OverviewZaileSictbarMitZaitLezte;
				var OverviewZaileSictbarMitZaitVorLezteNulbar = this.OverviewZaileSictbarMitZaitVorLezte;

				var OverviewZaileSictbarLezte =
					OverviewZaileSictbarMitZaitLezteNulbar.HasValue ?
					OverviewZaileSictbarMitZaitLezteNulbar.Value.Wert : null;

				var OverviewZaileSictbarVorLezte =
					OverviewZaileSictbarMitZaitVorLezteNulbar.HasValue ?
					OverviewZaileSictbarMitZaitVorLezteNulbar.Value.Wert : null;

				var ScnapscusLezteAusOverviewZaile = (null == OverviewZaileSictbarLezte) ? null : OverviewZaileSictbarLezte;
				var ScnapscusVorLezteAusOverviewZaile = (null == OverviewZaileSictbarVorLezte) ? null : OverviewZaileSictbarVorLezte;

				/*
				 * 2015.02.17
				 * 
					var ScnapscusLezteOverviewTypeSelectionName = (null == OverviewZaileSictbarLezte) ? null : OverviewZaileSictbarLezte.OverviewTypeSelectionName;
					var ScnapscusVorLezteOverviewTypeSelectionName = (null == OverviewZaileSictbarVorLezte) ? null : OverviewZaileSictbarVorLezte.OverviewTypeSelectionName;
				 * */

				var ScnapscusLezteOverviewTypeSelectionName = this.ScnapscusLezteOverviewTypeSelectionName;
				var ScnapscusVorLezteOverviewTypeSelectionName = this.ScnapscusVorLezteOverviewTypeSelectionName;

				if (null != OverviewZaileSictbarLezte)
				{
					DistanceScrankeMin = OverviewZaileSictbarLezte.DistanceMin;
					DistanceScrankeMax = OverviewZaileSictbarLezte.DistanceMax;
				}

				if (null != OverviewZaileSictbarVorLezte)
				{
					if (!DistanceScrankeMin.HasValue)
					{
						DistanceScrankeMin = OverviewZaileSictbarVorLezte.DistanceMin;
					}

					if (!DistanceScrankeMax.HasValue)
					{
						DistanceScrankeMax = OverviewZaileSictbarVorLezte.DistanceMax;
					}
				}

				{
					//	Berecnung in welcem Overview Preset das Objekt zu welcer Zait als leztes gesictet wurde.
					//	Hiirbai werd gefiltert so das ain Aintraag für ain Overview Preset nur zusctandekomt fals das Objekt in zwai direkt aufainanderfolgenden Scnapscüsen in dem Overview Preset gesictet wurde.

					var DictZuOverviewPresetSictungLezteZait = this.DictZuOverviewPresetSictungLezteZait;

					if (null == DictZuOverviewPresetSictungLezteZait)
					{
						DictZuOverviewPresetSictungLezteZait = new Dictionary<string, Int64>();
					}

					if (null != ScnapscusLezteOverviewTypeSelectionName)
					{
						var KainAintraagBisher = DictZuOverviewPresetSictungLezteZait.Count < 1;

						if (string.Equals(ScnapscusVorLezteOverviewTypeSelectionName, ScnapscusLezteOverviewTypeSelectionName) ||
							KainAintraagBisher)
						{
							var tDebug = AingangUnglaicDefaultZuZaitLezte?.Wert.Key?.IconMainColor?.OverviewIconMainColorIsRedAsRat() ?? false;

							if (tDebug)
							{

							}

							//	2015.09.03	Zuusäzlice Bedingung: sictbar.
							if ((null != ScnapscusLezteAusOverviewZaile && null != ScnapscusVorLezteAusOverviewZaile) ||
								KainAintraagBisher)
							{
								if (SaitSictbarkaitLezteListeScritAnzaal <= 0)
								{
									DictZuOverviewPresetSictungLezteZait[ScnapscusLezteOverviewTypeSelectionName] = OverviewZaileSictbarMitZaitLezteNulbar.Value.Zait;
								}
							}
							else
							{

							}
						}
					}

					this.DictZuOverviewPresetSictungLezteZait = DictZuOverviewPresetSictungLezteZait;
				}

				if (null != ScnapscusLezteAusOverviewZaile)
				{
					if (null == SictungFrühesteType)
					{
						Type = SictungFrühesteType = ScnapscusLezteAusOverviewZaile.Type;
					}

					if (null == SictungFrühesteName)
					{
						Name = SictungFrühesteName = ScnapscusLezteAusOverviewZaile.Name;
					}

					AttackingMe = ScnapscusLezteAusOverviewZaile.IconAttackingMeSictbar;

					Targeting = true == ScnapscusLezteAusOverviewZaile.IconTargetingSictbar;
				}

				if (null != ScnapscusLezteAusOverviewZaile &&
					null != ScnapscusVorLezteAusOverviewZaile)
				{
					//	Diise Werte werden nur übernome fals diise in deen lezten baiden Scnapscus glaic waaren.

					if (string.Equals(ScnapscusLezteAusOverviewZaile.Type, ScnapscusVorLezteAusOverviewZaile.Type))
					{
						Type = ScnapscusLezteAusOverviewZaile.Type;
					}

					if (string.Equals(ScnapscusLezteAusOverviewZaile.Name, ScnapscusVorLezteAusOverviewZaile.Name))
					{
						Name = ScnapscusLezteAusOverviewZaile.Name;
					}

					if (true == ScnapscusVorLezteAusOverviewZaile.IconTargetingSictbar)
					{
						Targeting = true;
					}

					if (ScnapscusLezteAusOverviewZaile.IconTargetedByMeSictbar == ScnapscusVorLezteAusOverviewZaile.IconTargetedByMeSictbar)
					{
						Targeted = (true == ScnapscusLezteAusOverviewZaile.IconTargetedByMeSictbar);
					}

					var ScnapscusLezteTargetingOderTargeted =
						true == ScnapscusLezteAusOverviewZaile.IconTargetingSictbar ||
						true == ScnapscusLezteAusOverviewZaile.IconTargetedByMeSictbar;

					var ScnapscusVorLezteTargetingOderTargeted =
						true == ScnapscusVorLezteAusOverviewZaile.IconTargetingSictbar ||
						true == ScnapscusVorLezteAusOverviewZaile.IconTargetedByMeSictbar;

					if (ScnapscusLezteTargetingOderTargeted == ScnapscusVorLezteTargetingOderTargeted)
					{
						TargetingOderTargeted = ScnapscusLezteTargetingOderTargeted;
					}

					if (true == Targeting)
					{
						TargetingOderTargeted = true;
					}

					var LezteIconMainColor = ScnapscusLezteAusOverviewZaile.IconMainColor;
					var VorLezteIconMainColor = ScnapscusVorLezteAusOverviewZaile.IconMainColor;

					if (null != LezteIconMainColor && null != VorLezteIconMainColor)
					{
						var LezteIconMainColorHSV = LezteIconMainColor.VonRGBNaacHueSatVal();
						var VorLezteIconMainColorHSV = VorLezteIconMainColor.VonRGBNaacHueSatVal();

						IconMainColorSätigungGering =
							LezteIconMainColorHSV.SMilli < 4 &&
							VorLezteIconMainColorHSV.SMilli < 4;
					}
				}

				DebugAusListeScnapscusOverviewZaileBerecneAbgelaitete_1_Zait = OverviewZaileMitZaitLezte?.Zait;
			}
			finally
			{
				this.SictungLezteDistanceScrankeMinScpezOverview = DistanceScrankeMin;
				this.SictungLezteDistanceScrankeMaxScpezOverview = DistanceScrankeMax;
				this.Type = Type;
				this.Name = Name;
				this.Targeted = Targeted;
				this.Targeting = Targeting;
				this.TargetingOderTargeted = TargetingOderTargeted;
				this.AttackingMe = AttackingMe;
				this.IconMainColorSätigungGering = IconMainColorSätigungGering;

				this.IstAsteroid = OverviewObjektBescriftungIstAsteroid(this);
			}
		}

		public SictOverViewObjektZuusctand()
		{
		}

		public SictOverViewObjektZuusctand(
			Int64 Zait,
			VonSensor.OverviewZaile OverviewZaileLezte,
			SictZuInRaumObjektReprScnapscusZuusazinfo ZuusazInfoScnapscus)
			:
			base(
			Zait,
			OverviewZaileLezte,
			ZuusazInfoScnapscus)
		{
		}

		override public bool MenuPfaadBeginMööglicFürListeMenuBerecne(IEnumerable<SictWertMitZait<VonSensor.Menu>> ListeMenuMitBeginZait)
		{
			if (!ZaileIscAusgewäält(this.OverviewZaileMitZaitLezte))
			{
				return false;
			}

			return MenuPfaadFortsazMööglicBerecne();
		}

		override public bool MenuPfaadFortsazMööglicBerecne()
		{
			if (!ZaileIscAusgewäält(this.OverviewZaileSictbarMitZaitLezte))
			{
				return false;
			}

			return true;
		}

		new public SictOverViewObjektZuusctand Kopii()
		{
			return Bib3.RefNezDiferenz.Extension.ObjektKopiiKonstrukt(this);

			/*
			 * 2015.02.18
			 * 
			return Bib3.RefNezDiferenz.Extension.JsonConvertKopii(this);
			 * */
		}

		override public Int64? SictungLezteDistanceScrankeMin()
		{
			return SictungLezteDistanceScrankeMinScpezOverview;
		}

		override public Int64? SictungLezteDistanceScrankeMax()
		{
			return SictungLezteDistanceScrankeMaxScpezOverview;
		}


	}

}
