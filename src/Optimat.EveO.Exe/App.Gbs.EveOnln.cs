using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using Optimat.EveOnline;
using System.Windows.Media;
using Optimat.EveOnline.GBS;
//using Optimat.EveOnline.Berict.Auswert;
using Bib3;
using ExtractFromOldAssembly.Bib3;

namespace Optimat.EveO.Nuzer
{
	public	partial	class App
	{
		SictVonOptimatMeldungZuusctand VonOptimatMeldungZuusctandLezte
		{
			get
			{
				/*
				 * 2015.03.04
				 * 
				var Nuzer = this.Nuzer;

				if (null == Nuzer.Key)
				{
					return null;
				}

				return Nuzer.Key.VonOptimatMeldungZuusctand;
				 * */

				//	!!!!	Zu erseze durc Ergeebnis aus Automaat.


				var Automat = this.Automat;

				if(null==Automat)
				{
					return null;
				}

				return Automat.NaacNuzerMeldungZuusctand;
			}
		}

		readonly List<SictWertMitZait<object>> ListeObjektZuÜbertraageNaacScnapscusInspektor = new	List<SictWertMitZait<object>>();

		readonly List<SictNaacNuzerMeldungZuEveOnlineSictNuzer> AnwendungSizungMengeNaacNuzerMeldungZuEveOnline =
			new List<SictNaacNuzerMeldungZuEveOnlineSictNuzer>();

		static int GbsEveOnlineUIUndConfigMengeMeldungZuErhalteScrankeMax = 10;

		bool GbsEveOnlinePräferenzGelaadeNaacGbs = false;

		void GbsEveOnlineMengeMissionAktualisiire(IVonBerictNaacGbsRepr SictRepr)
		{
			KeyValuePair<Int64,	SictWertMitZait<SictMissionZuusctand>>[]	MengeMission	= null;

			var	VonOptimatMeldungZuusctandLezte	= this.VonOptimatMeldungZuusctandLezte;

			if(null	!= VonOptimatMeldungZuusctandLezte)
			{
				MengeMission	=
					VonOptimatMeldungZuusctandLezte.MengeMission
					.SelectNullable((Mission) => new	KeyValuePair<Int64,	SictWertMitZait<SictMissionZuusctand>>(
						Mission.Ident, new	SictWertMitZait<SictMissionZuusctand>(Mission.SictungFrühesteZaitMili	?? -1, Mission)))
					.ToArrayNullable();
			}

			var GbsSctoierelementHaupt = this.GbsSctoierelementHaupt;

			if (null == GbsSctoierelementHaupt)
			{
				return;
			}

			var DataGridMengeMission = GbsSctoierelementHaupt.DataGridMengeMission;

			if (null == DataGridMengeMission)
			{
				return;
			}

			var MengeMissionInDataGridRepr = DataGridMengeMission.MengeMissionRepr;

			if (null == SictRepr)
			{
				MengeMissionInDataGridRepr.Clear();
			}
			else
			{
				SictVonBerictNaacGbsRepr.PropagiireNaacMengeMissionRepr(MengeMission, MengeMissionInDataGridRepr, false);
			}

			foreach (var MissionInDataGridRepr in MengeMissionInDataGridRepr)
			{
				MissionInDataGridRepr.RaisePropertyChanged();
			}
		}

		void GbsEveOnlnAktualisiire()
		{
			var BeginZaitMili = Bib3.Glob.StopwatchZaitMiliSictInt();

			var GbsSctoierelementHaupt = this.GbsSctoierelementHaupt;

			if (null == GbsSctoierelementHaupt)
			{
				return;
			}

			var SctoierelementEveOnlinePräferenz = GbsSctoierelementHaupt.SctoierelementEveOnlinePräferenz;

			SictAusGbsLocationInfo VonOptimatMeldungCurrentLocationLezte = null;
			ShipState VonOptimatMeldungShipZuusctandLezte = null;
			string	VonOptimatMeldungFittingInfoAgrString	= null;

			var ZuEveOnlineGbsUndKonfigMengeMeldungTyp = new List<Bib3.FCL.GBS.SictSymboolAkzeptanzFeelerWarnung.SymboolTypSictEnum>();

			ZuEveOnlineGbsUndKonfigMengeMeldungTyp.Add(Bib3.FCL.GBS.SictSymboolAkzeptanzFeelerWarnung.SymboolTypSictEnum.Akzeptanz);

			Bib3.FCL.GBS.SictSymboolAkzeptanzFeelerWarnung.SymboolTypSictEnum[] ZuEveOnlineSimuMengeMeldungTyp = null;
			Bib3.FCL.GBS.SictSymboolAkzeptanzFeelerWarnung.SymboolTypSictEnum[] ZuEveOnlineMengeMeldungTyp = null;

			byte[] VonServerMeldungAnwendungSizungIdent	= null;
			var VonOptimatMeldungZuusctandLezte = this.VonOptimatMeldungZuusctandLezte;

			var ListeOptimatScrit = this.ListeOptimatScrit;

			try
			{
				if (!GbsEveOnlinePräferenzGelaadeNaacGbs)
				{
					GbsSctoierelementHaupt.EveOnlineAutoKonfigSezeZurük();

					GbsSctoierelementHaupt.EveOnlinePräferenzLaadeVonDataiPfaadUndBericteNaacGbs();

					GbsEveOnlinePräferenzGelaadeNaacGbs = true;
				}

				if (null != VonOptimatMeldungZuusctandLezte)
				{
					VonServerMeldungAnwendungSizungIdent = VonOptimatMeldungZuusctandLezte.SizungIdent;
				}

				var VonServerMeldungAutomaatZuusctandLezteMengeMeldungZuEveOnline =
					(null == VonOptimatMeldungZuusctandLezte) ? null :
					ExtractFromOldAssembly.Bib3.Extension.WhereNullable(
					VonOptimatMeldungZuusctandLezte.MengeMeldungZuEveOnline,
					(Kandidaat) => null	!= Kandidaat)
					.ToArrayNullable();

				{
					{
						//	Ale Meldunge entferne welce aus andere Sizung kumen.

						AnwendungSizungMengeNaacNuzerMeldungZuEveOnline.RemoveAll((Kandidaat) =>
							!Bib3.Glob.SequenceEqualPerObjectEquals(Kandidaat.SizungIdent, VonServerMeldungAnwendungSizungIdent));
					}

					if (null != VonOptimatMeldungZuusctandLezte)
					{
						Bib3.Glob.PropagiireListeRepräsentatioon(
							VonServerMeldungAutomaatZuusctandLezteMengeMeldungZuEveOnline,
							AnwendungSizungMengeNaacNuzerMeldungZuEveOnline as IList<SictNaacNuzerMeldungZuEveOnlineSictNuzer>,
							(Meldung) => new SictNaacNuzerMeldungZuEveOnlineSictNuzer(Meldung, VonServerMeldungAnwendungSizungIdent, null),
							(KandidaatRepr, Meldung) => SictNaacNuzerMeldungZuEveOnlineSictNuzer.GlaicwertigPerIdent(KandidaatRepr, Meldung),
							(Repr, Meldung) =>
							{
								if (null != Meldung)
								{
									Repr.MeldungSeze(Meldung);
								}
							},
							true);
					}

					foreach (var Meldung in AnwendungSizungMengeNaacNuzerMeldungZuEveOnline)
					{
						Meldung.VonServerNocExistent =
							null != ExtractFromOldAssembly.Bib3.Extension.FirstOrDefaultNullable(VonServerMeldungAutomaatZuusctandLezteMengeMeldungZuEveOnline,
							(Kandidaat) => SictNaacNuzerMeldungZuEveOnlineSictNuzer.GlaicwertigPerIdent(Meldung, Kandidaat));
					}

					{
						//	Aus der Menge der nit meer von Server gemeldete Meldunge di ältere Tailmenge entferne welce üwer Anzaal Scranke Max.

						var TailmengeVonServerNocExistent =
							AnwendungSizungMengeNaacNuzerMeldungZuEveOnline
							.Where((Kandidaat) => true == Kandidaat.VonServerNocExistent).ToArray();

						var TailmengeNocZuErhalte =
							TailmengeVonServerNocExistent
							.Concat(
							AnwendungSizungMengeNaacNuzerMeldungZuEveOnline
							.Except(TailmengeVonServerNocExistent)
							.OrderByDescending((Kandidaat) => Kandidaat.AktiivLezteZait ?? int.MinValue)
							.Take(GbsEveOnlineUIUndConfigMengeMeldungZuErhalteScrankeMax - TailmengeVonServerNocExistent.Length))
							.ToArray();

						AnwendungSizungMengeNaacNuzerMeldungZuEveOnline.RemoveAll((Kandidaat) => !TailmengeNocZuErhalte.Contains(Kandidaat));
					}

					ZuEveOnlineGbsUndKonfigMengeMeldungTyp.AddRange(
						AnwendungSizungMengeNaacNuzerMeldungZuEveOnline
						.Where((Kandidaat) => true == Kandidaat.VonServerNocExistent)
						.Select((MeldungZuEveOnline) => Optimat.EveOnline.GBS.Glob.SymboolTypBerecneAusNaacNuzerMeldung(MeldungZuEveOnline.Meldung)));
				}

				SctoierelementEveOnlinePräferenz.VonOptimatZuusctandApliziire(VonOptimatMeldungZuusctandLezte);

				ZuEveOnlineSimuMengeMeldungTyp =
					(false == GbsAingaabeEveOnlnSimuFraigaabe) ? null :
					new Bib3.FCL.GBS.SictSymboolAkzeptanzFeelerWarnung.SymboolTypSictEnum[]{
						Bib3.FCL.GBS.SictSymboolAkzeptanzFeelerWarnung.SymboolTypSictEnum.Warnung};

				if (null != VonOptimatMeldungZuusctandLezte)
				{
					VonOptimatMeldungShipZuusctandLezte = VonOptimatMeldungZuusctandLezte.ShipZuusctand;
					VonOptimatMeldungCurrentLocationLezte = VonOptimatMeldungZuusctandLezte.CurrentLocation;
					VonOptimatMeldungFittingInfoAgrString	=	VonOptimatMeldungZuusctandLezte.FittingInfoAgrString;
				}
			}
			finally
			{
				ZuEveOnlineMengeMeldungTyp =
					Bib3.Glob.ListeEnumerableAgregiirt(new IEnumerable<Bib3.FCL.GBS.SictSymboolAkzeptanzFeelerWarnung.SymboolTypSictEnum>[]{
						ZuEveOnlineSimuMengeMeldungTyp,
						ZuEveOnlineGbsUndKonfigMengeMeldungTyp})
					.ToArrayNullable();

				/*
				 * 16.04.15
				 * Deactivate UI.
				 * 
				GbsSctoierelementHaupt.EveOnlineSctaatusInspekt.Repräsentiire(
					Optimat.GBS.Glob.MengeMeldungAkzeptanzFeelerWarnungAgregatioon(ZuEveOnlineMengeMeldungTyp));

				GbsSctoierelementHaupt.EveOnlineGbsUndKonfigSctaatusInspekt.Repräsentiire(
					Optimat.GBS.Glob.MengeMeldungAkzeptanzFeelerWarnungAgregatioon(ZuEveOnlineGbsUndKonfigMengeMeldungTyp));

				GbsSctoierelementHaupt.EveOnlineSimuSctaatusInspekt.Repräsentiire(
					Optimat.GBS.Glob.MengeMeldungAkzeptanzFeelerWarnungAgregatioon(ZuEveOnlineSimuMengeMeldungTyp));
				*/

				GbsSctoierelementHaupt.EveOnlineMengeNaacNuzerMeldung.Repräsentiire(
					AnwendungSizungMengeNaacNuzerMeldungZuEveOnline,
					BeginZaitMili);

				GbsSctoierelementHaupt.EveOnlnCharShipStateInspekt.Repräsentiire(VonOptimatMeldungShipZuusctandLezte);

				GbsSctoierelementHaupt.TextBoxEveOnlnShipInspektFitting.Text = VonOptimatMeldungFittingInfoAgrString;

				GbsSctoierelementHaupt.EveOnlineZuusctandInspektCurrentLocation.Repräsentiire(VonOptimatMeldungCurrentLocationLezte);

				GbsEveOnlineMengeMissionAktualisiire(new Optimat.EveOnline.GBS.SictVonBerictNaacGbsRepr());
			}

			var GbsEveOnlnCharShipStateAktualisiireBeginZaitMikro = Bib3.Glob.StopwatchZaitMikroSictInt();
		}

		/*
		 * 2015.03.04
		 * 
		void ButtonEveOnlnSensoScnapscusAuswertLezteFertigScraibeNaacDatai_Drop(object sender, DragEventArgs e)
		{
			try
			{
				var EveOnlnSensoScnapscusAuswertLezteFertig = this.EveOnlnSensoScnapscusAuswertLezteFertig;

				if (null == EveOnlnSensoScnapscusAuswertLezteFertig)
				{
					throw new ArgumentNullException("EveOnlnSensoScnapscusAuswertLezteFertig");
				}

				var GbsBaumWurzelInfo = EveOnlnSensoScnapscusAuswertLezteFertig.Wert;

				if (null == GbsBaumWurzelInfo)
				{
					throw new ArgumentNullException("GbsBaumWurzelInfo");
				}

				var ZiilPfaad = Bib3.FCL.Glob.DataiPfaadAlsKombinatioonAusSctandardPfaadUndFileDrop("Scnapscus.Auswert.Lezte", e);

				var AuswertSictStringAbbild = JsonConvert.SerializeObject(GbsBaumWurzelInfo, Formatting.Indented);

				Bib3.Glob.ScraibeInhaltNaacDataiPfaad(ZiilPfaad, Encoding.UTF8.GetBytes(AuswertSictStringAbbild));
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}
		 * */


	}
}
