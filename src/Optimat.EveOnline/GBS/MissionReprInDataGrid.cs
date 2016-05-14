using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Bib3;
using Newtonsoft.Json;
using Optimat.EveOnline.VonSensor;

namespace Optimat.EveOnline.GBS
{
	[JsonObject(MemberSerialization.OptIn)]
	public class SictMissionReprInDataGridParam
	{
		[JsonProperty]
		public int? RewardLpDurcIsk;

		public SictMissionReprInDataGridParam()
		{
		}

		public SictMissionReprInDataGridParam(
			int? RewardLpDurcIsk)
		{
			this.RewardLpDurcIsk = RewardLpDurcIsk;
		}
	}

	public class SictMissionReprInDataGrid
	{
		const string IskBetraagStringFormat = "### ### ### ###";

		readonly public SictMissionReprInDataGridParam SictParam;

		public const int BrushCellBackgroundOpazitäät = 0x60;

		static readonly public Color BrushKonstantColor = Color.FromArgb(BrushCellBackgroundOpazitäät, 0x80, 0x80, 0x80);
		static readonly public Color BrushAktiivColor = Color.FromArgb(BrushCellBackgroundOpazitäät, 0, 0x80, 0);

		static public Brush FürCellBrushMitColorErsctele(Color Color)
		{
			return Optimat.GBS.Glob.BrushGradientDiagonalGesctraiftZwaifarbigKonstruiire(Colors.Transparent, Color, 4);
		}

		static readonly public Brush BrushKonstant = FürCellBrushMitColorErsctele(BrushKonstantColor);
		static readonly public Brush BrushAktiiv = FürCellBrushMitColorErsctele(BrushAktiivColor);

		static readonly public Brush BrushSecurityLevelZuGering = new SolidColorBrush(Color.FromArgb(0x40, 0xa0, 0, 0));

		static public Brush CellBackgroundFürSecurityLevelMili(int? SecurityLevelMili)
		{
			if (500 <= SecurityLevelMili)
			{
				return null;
			}

			return BrushSecurityLevelZuGering;
		}

		static public Brush CellBackgroundFürWertAktiiv(bool? Aktiiv)
		{
			if (!Aktiiv.HasValue)
			{
				return null;
			}

			return Aktiiv.Value ? BrushAktiiv : null;
		}

		static public Brush CellBackgroundFürWertKonstant(bool? Konstant)
		{
			if (!Konstant.HasValue)
			{
				return null;
			}

			return Konstant.Value ? BrushKonstant : null;
		}

		static string ZaalSictRöömisc(int? Zaal)
		{
			if (!Zaal.HasValue)
			{
				return null;
			}

			return Optimat.Glob.ZaalSictRöömisc(Zaal.Value);
		}

		virtual public SictMissionZuusctand MissionBerecne()
		{
			return null;
		}

		public	SictMissionReprInDataGrid(Optimat.EveOnline.GBS.SictMissionReprInDataGridParam SictParam)
		{
			this.SictParam = SictParam;
		}

		public Int64? Ident
		{
			get
			{
				var Mission = this.MissionBerecne();

				if (null == Mission)
				{
					return null;
				}

				return Mission.Ident;
			}
		}

		public string AgentName
		{
			get
			{
				var Mission = this.MissionBerecne();

				if (null == Mission)
				{
					return null;
				}

				return Mission.AgentName;
			}
		}

		public int?	AgentLevel
		{
			get
			{
				var Mission = this.MissionBerecne();

				if (null == Mission)
				{
					return null;
				}

				return Mission.AgentLevel;
			}
		}

		public string AgentLevelSictStringRöömisc
		{
			get
			{
				return ZaalSictRöömisc(AgentLevel);
			}
		}

		public string AgentLocationLocationName
		{
			get
			{
				var Mission = this.MissionBerecne();

				if (null == Mission)
				{
					return null;
				}

				var AgentLocation = Mission.AgentLocation;

				if (null == AgentLocation)
				{
					return null;
				}

				return AgentLocation.LocationName;
			}
		}

		public string Titel
		{
			get
			{
				var Mission = this.MissionBerecne();

				if (null == Mission)
				{
					return null;
				}

				return Mission.Titel;
			}
		}

		public string FittingIdent
		{
			get
			{
				var Mission = this.MissionBerecne();

				if (null == Mission)
				{
					return null;
				}

				return Mission.FürMissionFittingBezaicner;
			}
		}

		public int? VonFüüreAusBeginBisCompleteDauer
		{
			get
			{
				var Mission = this.MissionBerecne();

				if (null == Mission)
				{
					return null;
				}

				if (!(true == Mission.CompleteErfolg))
				{
					return null;
				}

				return (int?)((Mission.EndeZaitMili - Mission.AktioonFüüreAusFrühesteZaitMili) / 1000);
			}
		}

		public int? VonAcceptBisCompleteDauer
		{
			get
			{
				var Mission = this.MissionBerecne();

				if (null == Mission)
				{
					return null;
				}

				if (!(true == Mission.CompleteErfolg))
				{
					return null;
				}

				return (int?)((Mission.EndeZaitMili - Mission.AcceptedFrühesteZaitMili) / 1000);
			}
		}

		static public string IskBetraagSictString(Int64? IskBetraag)
		{
			if (!IskBetraag.HasValue)
			{
				return null;
			}

			return IskBetraag.Value.ToString(IskBetraagStringFormat).Trim();
		}

		public MissionInfo MissionInfoBerecne()
		{
			var Mission = MissionBerecne();

			if (null == Mission)
			{
				return null;
			}

			return Mission.MissionInfoLezte.Wert;
		}

		public Int64? RewardMitBonusTailIskAnzaal
		{
			get
			{
				var MissionInfo = MissionInfoBerecne();

				if (null == MissionInfo)
				{
					return null;
				}

				return MissionInfo.RewardIskAnzaal	+	(MissionInfo.BonusRewardIskAnzaal	?? 0);
			}
		}

		public Int64? RewardTailLpAnzaal
		{
			get
			{
				var MissionInfo = MissionInfoBerecne();

				if (null == MissionInfo)
				{
					return null;
				}

				return MissionInfo.RewardLpAnzaal;
			}
		}

		public bool? AktioonFüüreAusAktiiv
		{
			get
			{
				var Mission	= MissionBerecne();

				if (null == Mission)
				{
					return null;
				}

				return Mission.AktioonFüüreAusAktiiv;
			}
		}

		public bool? VersuucFittingAktiiv
		{
			get
			{
				var Mission	= MissionBerecne();

				if (null == Mission)
				{
					return null;
				}

				return Mission.VersuucFittingAktiiv;
			}
		}

		public Int64? EndeZaitMiliOderMaximum
		{
			get
			{
				return EndeZaitMili ?? Int64.MaxValue;
			}
		}

		public Int64? EndeZaitMili
		{
			get
			{
				var Mission = MissionBerecne();

				if (null == Mission)
				{
					return null;
				}

				return Mission.EndeZaitMili;
			}
		}

		public bool? Fertig
		{
			get
			{
				return EndeZaitMili.HasValue;
			}
		}

		public SictKonfigMissionZuMissionFilterVerhalte AusPräferenzEntscaidungVerhalteBerecne()
		{
			var Mission = MissionBerecne();

			if (null == Mission)
			{
				return null;
			}

			return Mission.AusPräferenzEntscaidungVerhalte;
		}

		public bool? ZuMissionVerhalteAktioonFüüreAusAktiiv
		{
			get
			{
				var AusPräferenzEntscaidungVerhalte = AusPräferenzEntscaidungVerhalteBerecne();

				if (null == AusPräferenzEntscaidungVerhalte)
				{
					return null;
				}

				return AusPräferenzEntscaidungVerhalte.AktioonFüüreAusAktiiv;
			}
		}

		public bool? ZuMissionVerhalteAktioonAcceptAktiiv
		{
			get
			{
				var AusPräferenzEntscaidungVerhalte = AusPräferenzEntscaidungVerhalteBerecne();

				if (null == AusPräferenzEntscaidungVerhalte)
				{
					return null;
				}

				return AusPräferenzEntscaidungVerhalte.AktioonAcceptAktiiv;
			}
		}

		public bool? ZuMissionVerhalteAktioonDeclineAktiiv
		{
			get
			{
				var AusPräferenzEntscaidungVerhalte = AusPräferenzEntscaidungVerhalteBerecne();

				if (null == AusPräferenzEntscaidungVerhalte)
				{
					return null;
				}

				return AusPräferenzEntscaidungVerhalte.AktioonDeclineAktiiv;
			}
		}

		public Brush CellBackgroundAktioonFüüreAusAktiiv
		{
			get
			{
				return CellBackgroundFürWertAktiiv(AktioonFüüreAusAktiiv);
			}
		}

		public Brush CellBackgroundVersuucFittingAktiiv
		{
			get
			{
				return CellBackgroundFürWertAktiiv(VersuucFittingAktiiv);
			}
		}

		public Brush CellBackgroundSecurityLevelMinimum
		{
			get
			{
				return CellBackgroundFürSecurityLevelMili(SecurityLevelMiliMinimum);
			}
		}

		public Int64? RewardMitBonusTailIskDurcFüüreAusDauerSctunde
		{
			get
			{
				var RewardMitBonusTailIskAnzaal = this.RewardMitBonusTailIskAnzaal;

				var VonFüüreAusBeginBisCompleteDauer = this.VonFüüreAusBeginBisCompleteDauer;

				if (!VonFüüreAusBeginBisCompleteDauer.HasValue)
				{
					return null;
				}

				return (RewardMitBonusTailIskAnzaal * 60 * 60) / VonFüüreAusBeginBisCompleteDauer.Value;
			}
		}

		public Int64? RewardTailLpDurcFüüreAusDauerSctunde
		{
			get
			{
				var RewardTailLpAnzaal = this.RewardTailLpAnzaal;

				var VonFüüreAusBeginBisCompleteDauer = this.VonFüüreAusBeginBisCompleteDauer;

				if (!VonFüüreAusBeginBisCompleteDauer.HasValue)
				{
					return null;
				}

				return (RewardTailLpAnzaal * 60 * 60) / VonFüüreAusBeginBisCompleteDauer.Value;
			}
		}

		public	SictFactionSictEnum[] MengeFaction
		{
			get
			{
				var	Mission	= MissionBerecne();
				
				if(null	== Mission)
				{
					return	null;
				}

				return Mission.ObjectiveMengeFaction;
			}
		}

		public string MengeFactionAggrSictString
		{
			get
			{
				var MengeFaction = this.MengeFaction;

				if (null == MengeFaction)
				{
					return null;
				}

				return string.Join(", ", MengeFaction);
			}
		}

		public Int64? AcceptedFrühesteZaitMili
		{
			get
			{
				var Mission = MissionBerecne();

				if (null == Mission)
				{
					return null;
				}

				return Mission.AcceptedFrühesteZaitMili;
			}
		}

		public bool? Accepted
		{
			get
			{
				return AcceptedFrühesteZaitMili.HasValue;
			}
		}

		public int? InLocationSystemListePfaadAnzaal
		{
			get
			{
				var Mission = MissionBerecne();

				if (null == Mission)
				{
					return null;
				}

				return (int?)Mission.ListeLocationPfaad.CountNullable();
			}
		}

		public bool? ConstraintFittingSatisfied
		{
			get
			{
				var Mission = MissionBerecne();

				if (null == Mission)
				{
					return null;
				}

				return Mission.ConstraintFittingSatisfied;
			}
		}

		public int? SecurityLevelMiliMinimum
		{
			get
			{
				var Mission = MissionBerecne();

				if (null == Mission)
				{
					return null;
				}

				return Mission.SecurityLevelMinimumMili;
			}
		}

		static	NumberFormatInfo NumberFormatInfoSecurityLevel = NumberFormatInfoSecurityLevelBerecne();

		static public NumberFormatInfo NumberFormatInfoSecurityLevelBerecne()
		{
			var NumberFormatInfo = new NumberFormatInfo();

			NumberFormatInfo.NumberDecimalSeparator = ".";

			return NumberFormatInfo;
		}

		static public string SecurityLevelSictString(int? SecurityLevelMili)
		{
			if (!SecurityLevelMili.HasValue)
			{
				return null;
			}

			return	string.Format(NumberFormatInfoSecurityLevel, "{0}", (SecurityLevelMili * 1e-3));

			return (SecurityLevelMili / 1000).ToString() + "." + (SecurityLevelMili % 1000).ToString();
		}

		public string SecurityLevelMinimumSictString
		{
			get
			{
				return SecurityLevelSictString(SecurityLevelMiliMinimum);
			}
		}
	}

	public class SictMissionReprInDataGridAusMissionZuusctand : SictMissionReprInDataGrid
	{
		public SictMissionZuusctand Mission;

		public SictMissionReprInDataGridAusMissionZuusctand(
			SictMissionZuusctand Mission)
			:
			this(null,	Mission)
		{
		}

		public SictMissionReprInDataGridAusMissionZuusctand(
			Optimat.EveOnline.GBS.SictMissionReprInDataGridParam SictParam,
			SictMissionZuusctand Mission)
			:
			base(SictParam)
		{
			this.Mission = Mission;
		}

		override	public SictMissionZuusctand MissionBerecne()
		{
			return Mission;
		}


	}
}
