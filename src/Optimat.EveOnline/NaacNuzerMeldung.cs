using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bib3;
using Newtonsoft.Json;


namespace Optimat.EveOnline
{
	public enum SictNaacNuzerMeldungZuEveOnlineCauseTypeBinär
	{
		Kain	= 0,

		SimulatedNotification	= 110,

		//	Optimat Server
		OptimatServerExpectedDowntime = 1110,
		OptimatServerSessionEnd	= 1120,

		//	Direkt in UI Scnapscus sictbaare.
		EveServerExpectedDowntime = 1310,
		EveServerConnectionLost	= 1320,
		LoginCompletedNot	= 1330,
		CharSelectionCompletedNot	= 1340,
		BlockedByWindow	= 1380,

		LanguageNotSetToEnglish	= 1390,

		InfoPanelLocationNearestMissing	= 1410,
		RoutePathSecurityLevelTooLow	= 1420,

		OverviewSortingNotOptimized	= 1510,
		OverviewTabCountTooLow	= 1511,

		//	Indirekt ermitelte, nit unbedingt aus Scnapscus erkenbaare.
		ShipCapacitorOrHitpointsTooLow = 3100,
		ShipCapacitorTooLow = 3110,
		ShipHitpointsShieldTooLow = 3220,
		ShipHitpointsArmorTooLow = 3230,
		ShipHitpointsStructTooLow = 3240,

		ShipFittingInappropriate	= 4110,
		ShipFittingMissingWeapon = 4111,
		ShipFittingMissingMiner = 4112,

		MissionCurrentNoFittingConfigured = 4310,

		Incursion	= 6110,

		EstimatedGankRiskTooHigh	= 7110,
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictNaacNuzerMeldungZuEveOnlineSictNuzer
	{
		[JsonProperty]
		public SictNaacNuzerMeldungZuEveOnline Meldung
		{
			private set;
			get;
		}

		[JsonProperty]
		readonly public byte[] SizungIdent;

		[JsonProperty]
		public bool? VonServerNocExistent;

		static	public bool GlaicwertigPerIdent(
			SictNaacNuzerMeldungZuEveOnlineSictNuzer MeldungSictNuzer,
			SictNaacNuzerMeldungZuEveOnline Meldung)
		{
			if (object.ReferenceEquals(Meldung, MeldungSictNuzer))
			{
				return true;
			}

			if (null == MeldungSictNuzer)
			{
				return false;
			}

			return SictNaacNuzerMeldungZuEveOnline.GlaicwertigPerIdent(MeldungSictNuzer.Meldung, Meldung);
		}

		public void MeldungSeze(SictNaacNuzerMeldungZuEveOnline Meldung)
		{
			this.Meldung = Meldung;
		}

		public Int64? MeldungIdent
		{
			get
			{
				var Meldung = this.Meldung;

				if (null == Meldung)
				{
					return null;
				}

				return Meldung.Ident;
			}
		}

		public Int64? AktiivLezteZait
		{
			get
			{
				var Meldung = this.Meldung;

				if (null == Meldung)
				{
					return null;
				}

				return Meldung.AktiivLezteZait;
			}
		}

		public SictNaacNuzerMeldungZuEveOnlineSictNuzer()
		{
		}

		public SictNaacNuzerMeldungZuEveOnlineSictNuzer(
			SictNaacNuzerMeldungZuEveOnline Meldung,
			byte[] SizungIdent,
			bool? VonServerNocExistent)
		{
			this.Meldung = Meldung;
			this.SizungIdent = SizungIdent;
			this.VonServerNocExistent = VonServerNocExistent;
		}
	}

	public class SictNaacNuzerMeldungZuEveOnlineEqualityHinraicenGlaicwertigFürFortsaz : IEqualityComparer<SictNaacNuzerMeldungZuEveOnline>
	{
		public int GetHashCode(SictNaacNuzerMeldungZuEveOnline O)
		{
			if (null == O)
			{
				return 0;
			}

			return O.GetHashCode();
		}

		public bool Equals(
			SictNaacNuzerMeldungZuEveOnline O0,
			SictNaacNuzerMeldungZuEveOnline O1)
		{
			return SictNaacNuzerMeldungZuEveOnline.HinraicenGlaicwertigFürFortsaz(O0, O1);
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictNaacNuzerMeldungZuEveOnlineCause
	{
		[JsonProperty]
		readonly	public SictNaacNuzerMeldungZuEveOnlineCauseTypeBinär?	CauseBinary;

		[JsonProperty]
		readonly public string CauseText;

		[JsonProperty]
		readonly public string WindowBlockingTitel;

		[JsonProperty]
		readonly public string WindowArrangementWindowStackTitel;

		[JsonProperty]
		readonly public string WindowArrangementWindowClippedTitel;

		[JsonProperty]
		readonly public string FittingManagementMissingFitting;

		[JsonProperty]
		readonly public string PreferencesOverviewMissingTypeGroup;

		[JsonProperty]
		readonly public string OverviewMissingPreset;

		[JsonProperty]
		readonly public string OverviewMissingTab;

		[JsonProperty]
		readonly public SictShipCargoTypSictEnum? ShipCargoMissingTyp;

		static public string CauseSictStringBerecne(
			SictNaacNuzerMeldungZuEveOnlineCause Ursace)
		{
			if (null == Ursace)
			{
				return null;
			}

			var CauseText = Ursace.CauseText;
			var WindowBlockingTitel = Ursace.WindowBlockingTitel;

			var WindowArrangementWindowStackTitel = Ursace.WindowArrangementWindowStackTitel;

			var FittingManagementMissingFitting = Ursace.FittingManagementMissingFitting;
			var PreferencesOverviewMissingTypeGroup = Ursace.PreferencesOverviewMissingTypeGroup;

			var OverviewMissingPreset = Ursace.OverviewMissingPreset;
			var OverviewMissingTab = Ursace.OverviewMissingTab;

			var ShipCargoMissingTyp = Ursace.ShipCargoMissingTyp;

			var CauseBinary = Ursace.CauseBinary;

			if (null != WindowBlockingTitel)
			{
				return "blocked by Window[\"" + WindowBlockingTitel + "\"]";
			}

			if (null != WindowArrangementWindowStackTitel)
			{
				return "WindowStack[\"" + WindowArrangementWindowStackTitel + "\"] detected, please decompose Window Stack";
			}

			if (null != FittingManagementMissingFitting)
			{
				return "missing Fitting Entry[\"" + FittingManagementMissingFitting + "\"]";
			}

			if (null != PreferencesOverviewMissingTypeGroup)
			{
				return "Preferences.Overview missing Overview Type Group[\"" + PreferencesOverviewMissingTypeGroup + "\"]";
			}

			if (null != OverviewMissingPreset)
			{
				return "Overview missing Preset[\"" + OverviewMissingPreset + "\"]";
			}

			if (null != OverviewMissingTab)
			{
				return "Overview missing Tab[\"" + OverviewMissingTab + "\"]";
			}

			if (ShipCargoMissingTyp.HasValue)
			{
				return "Ship Cargo Type[\"" + ShipCargoMissingTyp.ToString() + "\"] missing";
			}

			if (CauseBinary.HasValue)
			{
				return SictNaacNuzerMeldungZuEveOnlineCause.CauseTypeStringBerecne(CauseBinary);
			}

			return CauseText;
		}

		static public string CauseTypeStringBerecne(
			SictNaacNuzerMeldungZuEveOnlineCauseTypeBinär? CauseType)
		{
			if (!CauseType.HasValue)
			{
				return null;
			}

			switch (CauseType.Value)
			{
				case SictNaacNuzerMeldungZuEveOnlineCauseTypeBinär.Kain:
					break;
				case SictNaacNuzerMeldungZuEveOnlineCauseTypeBinär.SimulatedNotification:
					break;
				case SictNaacNuzerMeldungZuEveOnlineCauseTypeBinär.OptimatServerExpectedDowntime:
					break;
				case SictNaacNuzerMeldungZuEveOnlineCauseTypeBinär.OptimatServerSessionEnd:
					break;
				case SictNaacNuzerMeldungZuEveOnlineCauseTypeBinär.EveServerExpectedDowntime:
					break;
				case SictNaacNuzerMeldungZuEveOnlineCauseTypeBinär.EveServerConnectionLost:
					break;
				case SictNaacNuzerMeldungZuEveOnlineCauseTypeBinär.LoginCompletedNot:
					break;
				case SictNaacNuzerMeldungZuEveOnlineCauseTypeBinär.CharSelectionCompletedNot:
					break;
				case SictNaacNuzerMeldungZuEveOnlineCauseTypeBinär.BlockedByWindow:
					break;
				case SictNaacNuzerMeldungZuEveOnlineCauseTypeBinär.InfoPanelLocationNearestMissing:
					break;
				case SictNaacNuzerMeldungZuEveOnlineCauseTypeBinär.RoutePathSecurityLevelTooLow:
					break;
				case SictNaacNuzerMeldungZuEveOnlineCauseTypeBinär.ShipCapacitorOrHitpointsTooLow:
					break;
				case SictNaacNuzerMeldungZuEveOnlineCauseTypeBinär.ShipCapacitorTooLow:
					break;
				case SictNaacNuzerMeldungZuEveOnlineCauseTypeBinär.ShipHitpointsShieldTooLow:
					break;
				case SictNaacNuzerMeldungZuEveOnlineCauseTypeBinär.ShipHitpointsArmorTooLow:
					break;
				case SictNaacNuzerMeldungZuEveOnlineCauseTypeBinär.ShipHitpointsStructTooLow:
					break;
				case SictNaacNuzerMeldungZuEveOnlineCauseTypeBinär.ShipFittingInappropriate:
					break;
				case SictNaacNuzerMeldungZuEveOnlineCauseTypeBinär.MissionCurrentNoFittingConfigured:
					return "Current Mission has no Fitting configured";
				case SictNaacNuzerMeldungZuEveOnlineCauseTypeBinär.Incursion:
					break;
				case SictNaacNuzerMeldungZuEveOnlineCauseTypeBinär.EstimatedGankRiskTooHigh:
					break;
				default:
					break;
			}

			return CauseTypeStringSctandardBerecne(CauseType);
		}

		static	public string CauseTypeStringSctandardBerecne(SictNaacNuzerMeldungZuEveOnlineCauseTypeBinär? CauseType)
		{
			if (!CauseType.HasValue)
			{
				return null;
			}

			var CauseTypeSictString =
				string.Join(" ", Optimat.Glob.StringSplitTransitioonLetterOderVonLowerNaacUpper(CauseType.Value.ToString()));

			return CauseTypeSictString;
		}

		public SictNaacNuzerMeldungZuEveOnlineCause()
		{
		}

		public SictNaacNuzerMeldungZuEveOnlineCause(
			SictNaacNuzerMeldungZuEveOnlineCauseTypeBinär? CauseBinary = null,
			string	CauseText	= null,
			string WindowBlockingTitel = null,
			string WindowArrangementWindowStackTitel = null,
			string WindowArrangementWindowClippedTitel = null,
			string FittingManagementMissingFitting = null,
			string PreferencesOverviewMissingTypeGroup = null,
			string OverviewMissingPreset = null,
			string OverviewMissingTab = null,
			SictShipCargoTypSictEnum? ShipCargoMissingTyp	= null)
		{
			this.CauseBinary = CauseBinary;

			this.CauseText = CauseText;

			this.WindowBlockingTitel = WindowBlockingTitel;

			this.WindowArrangementWindowStackTitel = WindowArrangementWindowStackTitel;
			this.WindowArrangementWindowClippedTitel = WindowArrangementWindowClippedTitel;

			this.FittingManagementMissingFitting = FittingManagementMissingFitting;

			this.PreferencesOverviewMissingTypeGroup = PreferencesOverviewMissingTypeGroup;

			this.OverviewMissingPreset = OverviewMissingPreset;
			this.OverviewMissingTab = OverviewMissingTab;

			this.ShipCargoMissingTyp = ShipCargoMissingTyp;
		}

		static public bool HinraicenGlaicwertigFürFortsaz(
			SictNaacNuzerMeldungZuEveOnlineCause O0,
			SictNaacNuzerMeldungZuEveOnlineCause O1)
		{
			if (object.ReferenceEquals(O0, O1))
			{
				return true;
			}

			if (null == O0 || null == O1)
			{
				return true;
			}

			return
				O0.CauseBinary == O1.CauseBinary &&

				string.Equals(O0.CauseText, O1.CauseText) &&

				string.Equals(O0.WindowBlockingTitel, O1.WindowBlockingTitel) &&

				string.Equals(O0.WindowArrangementWindowStackTitel, O1.WindowArrangementWindowStackTitel) &&
				string.Equals(O0.WindowArrangementWindowClippedTitel, O1.WindowArrangementWindowClippedTitel) &&

				string.Equals(O0.FittingManagementMissingFitting, O1.FittingManagementMissingFitting) &&

				string.Equals(O0.PreferencesOverviewMissingTypeGroup, O1.PreferencesOverviewMissingTypeGroup) &&

				string.Equals(O0.OverviewMissingPreset, O1.OverviewMissingPreset) &&
				string.Equals(O0.OverviewMissingTab, O1.OverviewMissingTab)	&&

				object.Equals(O0.ShipCargoMissingTyp, O1.ShipCargoMissingTyp);
		}
	}

	public enum SictNaacNuzerMeldungZuEveOnlineSeverity
	{
		None	= 0,
		Info	= 5,
		Warning	= 10,
		Error	= 15,
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictNaacNuzerMeldungZuEveOnline
	{
		[JsonProperty]
		readonly public Int64 Ident;

		[JsonProperty]
		readonly	public Int64? BeginZait;

		[JsonProperty]
		public Int64? AktiivLezteZait
		{
			private set;
			get;
		}

		[JsonProperty]
		readonly public SictNaacNuzerMeldungZuEveOnlineSeverity? Severity;

		[JsonProperty]
		readonly public SictNaacNuzerMeldungZuEveOnlineCause GeneralCause;

		[JsonProperty]
		readonly public SictNaacNuzerMeldungZuEveOnlineCause UndockPreventedCause;

		[JsonProperty]
		readonly public SictNaacNuzerMeldungZuEveOnlineCause DockForcedCause;

		[JsonProperty]
		public OrtogoonInt AktiivLezteInWindowClientFläce
		{
			private set;
			get;
		}

		public void AktiivLezteSeze(
			Int64 Zait,
			OrtogoonInt InWindowClientFläce)
		{
			AktiivLezteZait = Zait;
			AktiivLezteInWindowClientFläce = InWindowClientFläce;
		}

		public SictNaacNuzerMeldungZuEveOnline()
		{
		}

		public SictNaacNuzerMeldungZuEveOnline(
			Int64 Ident,
			Int64? BeginZait,
			SictNaacNuzerMeldungZuEveOnline ZuKopiire)
			:
			this(
			Ident,

			BeginZait,
			AktiivLezteInWindowClientFläce: (null == ZuKopiire) ? OrtogoonInt.Leer : ZuKopiire.AktiivLezteInWindowClientFläce,

			Severity: (null == ZuKopiire) ? null : ZuKopiire.Severity,

			GeneralCause: (null == ZuKopiire) ? null : ZuKopiire.GeneralCause,

			UndockPreventedCause: (null == ZuKopiire) ? null : ZuKopiire.UndockPreventedCause,
			DockForcedCause: (null == ZuKopiire) ? null : ZuKopiire.DockForcedCause
			)
		{
		}

		public SictNaacNuzerMeldungZuEveOnline(
			Int64 Ident,
			Int64? BeginZait = null,
			OrtogoonInt AktiivLezteInWindowClientFläce = default(OrtogoonInt),
			SictNaacNuzerMeldungZuEveOnlineSeverity? Severity = null,
			SictNaacNuzerMeldungZuEveOnlineCause GeneralCause = null,
			SictNaacNuzerMeldungZuEveOnlineCause UndockPreventedCause = null,
			SictNaacNuzerMeldungZuEveOnlineCause DockForcedCause = null)
		{
			this.Ident = Ident;

			this.BeginZait = BeginZait;
			this.AktiivLezteZait = BeginZait;

			this.AktiivLezteInWindowClientFläce = AktiivLezteInWindowClientFläce;

			this.Severity = Severity;

			this.GeneralCause = GeneralCause;

			this.UndockPreventedCause = UndockPreventedCause;
			this.DockForcedCause = DockForcedCause;
		}

		static	public SictNaacNuzerMeldungZuEveOnline WarningGenerel(
			Int64	Ident,
			SictNaacNuzerMeldungZuEveOnlineCause GeneralCause)
		{
			return new SictNaacNuzerMeldungZuEveOnline(
				Ident,
				Severity: SictNaacNuzerMeldungZuEveOnlineSeverity.Warning,
				GeneralCause: GeneralCause);
		}

		static public SictNaacNuzerMeldungZuEveOnline ErrorGenerel(
			Int64	Ident,
			SictNaacNuzerMeldungZuEveOnlineCause GeneralCause)
		{
			return new SictNaacNuzerMeldungZuEveOnline(
				Ident,
				Severity: SictNaacNuzerMeldungZuEveOnlineSeverity.Error,
				GeneralCause: GeneralCause);
		}

		static public SictNaacNuzerMeldungZuEveOnline WarningGenerel(
			Int64	Ident,
			SictNaacNuzerMeldungZuEveOnlineCauseTypeBinär GeneralCause)
		{
			return new SictNaacNuzerMeldungZuEveOnline(
				Ident,
				Severity: SictNaacNuzerMeldungZuEveOnlineSeverity.Warning,
				GeneralCause: new	SictNaacNuzerMeldungZuEveOnlineCause(GeneralCause));
		}

		static public SictNaacNuzerMeldungZuEveOnline ErrorGenerel(
			Int64	Ident,
			SictNaacNuzerMeldungZuEveOnlineCauseTypeBinär GeneralCause)
		{
			return new SictNaacNuzerMeldungZuEveOnline(
				Ident,
				Severity: SictNaacNuzerMeldungZuEveOnlineSeverity.Error,
				GeneralCause: new	SictNaacNuzerMeldungZuEveOnlineCause(GeneralCause));
		}

		static public bool GlaicwertigPerIdent(
			SictNaacNuzerMeldungZuEveOnline O0,
			SictNaacNuzerMeldungZuEveOnline O1)
		{
			if (object.ReferenceEquals(O0, O1))
			{
				return true;
			}

			if (null == O0 || null == O1)
			{
				return true;
			}

			return O0.Ident == O1.Ident;
		}

		static public bool HinraicenGlaicwertigFürFortsaz(
			SictNaacNuzerMeldungZuEveOnline	O0,
			SictNaacNuzerMeldungZuEveOnline	O1)
		{
			if (object.ReferenceEquals(O0, O1))
			{
				return true;
			}

			if (null == O0 || null == O1)
			{
				return true;
			}

			return
				O0.Severity == O1.Severity &&
				SictNaacNuzerMeldungZuEveOnlineCause.HinraicenGlaicwertigFürFortsaz(O0.GeneralCause, O1.GeneralCause)	&&

				SictNaacNuzerMeldungZuEveOnlineCause.HinraicenGlaicwertigFürFortsaz(O0.UndockPreventedCause, O1.UndockPreventedCause) &&
				SictNaacNuzerMeldungZuEveOnlineCause.HinraicenGlaicwertigFürFortsaz(O0.DockForcedCause, O1.DockForcedCause);
		}
	}

	/*
	 * 2014.07.28
	 * 
	 * Nit meer verwendet.
	 * 
	[JsonObject(MemberSerialization.OptIn)]
	public class SictAusOptimatScritNaacNuzerMeldung
	{
		[JsonProperty]
		public string MeldungString;

		[JsonProperty]
		public bool? ProbleemWindowArrangement;

		[JsonProperty]
		public bool? ProbleemFittingManagementFittingEntry;

		public bool? Probleem
		{
			get
			{
				if (true == ProbleemWindowArrangement)
				{
					return true;
				}

				if (true == ProbleemFittingManagementFittingEntry)
				{
					return true;
				}

				return null;
			}
		}

		public SictAusOptimatScritNaacNuzerMeldung()
		{
		}

		public SictAusOptimatScritNaacNuzerMeldung(
			string MeldungString,
			bool? ProbleemWindowArrangement = null,
			bool? ProbleemFittingManagementFittingEntry = null)
		{
			this.MeldungString = MeldungString;
			this.ProbleemWindowArrangement = ProbleemWindowArrangement;
			this.ProbleemFittingManagementFittingEntry = ProbleemFittingManagementFittingEntry;
		}
	}
	 * */

}
