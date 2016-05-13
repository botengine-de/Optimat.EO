using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Optimat.EveOnline;
using Bib3;

namespace Optimat.EveO.Nuzer
{
	public	partial	class App
	{
		Int64 EveOnlnServerProzesScnapscusAusgewertetLezteZait = 0;

		SictOptimatParam EveOnlnOptimatParamBerecne()
		{
			var Param = GbsAingaabeEveOnlinePräferenz;

			var	GbsAingaabeEveOnlnSimuFraigaabe	= this.GbsAingaabeEveOnlnSimuFraigaabe;
			var	GbsAingaabeVorgaabeDamageType	= this.GbsAingaabeVorgaabeDamageType;
			var GbsAingaabeSimuAnforderungFittingIgnoriire = this.GbsAingaabeSimuAnforderungFittingIgnoriire;
			var GbsAingaabeEveOnlnSimuSelbstShipZuusctand = this.GbsAingaabeEveOnlnSimuSelbstShipZuusctand;

			if (null == Param)
			{
				Param = new SictOptimatParam();
			}

			Param.AutoFraigaabe = GbsAingaabeEveOnlnWirkungFraigaabe;

			var Simu = new SictOptimatParamSimu();

			if (GbsAingaabeVorgaabeDamageType.HasValue)
			{
				Simu.VorgaabeFürGefectListeDamageTypePrio =
					Enum.GetValues(typeof(SictDamageTypeSictEnum))
					.OfType<SictDamageTypeSictEnum>()
					.Select((DamageTypeSictEnum) => new SictDamageMitBetraagIntValue(DamageTypeSictEnum, 30 + ((GbsAingaabeVorgaabeDamageType == DamageTypeSictEnum) ? 60 : 0)))
					.ToArray();
			}

			Simu.MissionAnforderungFittingIgnoriire = GbsAingaabeSimuAnforderungFittingIgnoriire;

			Simu.AufgaabeDistanceScteleAinObjektNääxteFraigaabe = GbsAingaabeSimuAufgaabeDistanceScteleAinObjektNääxteFraigaabe;
			Simu.AufgaabeDistanceScteleAinObjektNääxteDistanceSol = GbsAingaabeSimuAufgaabeDistanceScteleAinObjektNääxteDistanceSol;
			Simu.AufgaabeOverviewScroll = GbsAingaabeSimuOverviewScroll;
			Simu.AufgaabeMausAufWindowVordersteEkeOderKanteIndex = GbsAingaabeSimuMausAufWindowVordersteEkeIndex;

			Simu.SelbstShipZuusctand = GbsAingaabeEveOnlnSimuSelbstShipZuusctand;

			if (!SictOptimatParamSimu.HinraicendGlaicwertigFürIdentInOptimatParam(Simu, Param.Simu))
			{
				Param.Simu = Simu;
			}

			Param.SimuFraigaabe = GbsAingaabeEveOnlnSimuFraigaabe;

			return Param;
		}

		/*
		 * 2015.03.03
		 * 
		Queue<Int64> EveOnlnServerPerfListeVonNuzerBerictAingangDauerMikro = new Queue<Int64>();

		/// <summary>
		/// Optimat sol nit über Änderung benaacrictigt werde wen dese Vorgesclaagene Wirkung noc
		/// nit ausgefüürt wurde oder wen sait lezte Wirkung noc kaine Mesung von GBS durcgefüürt wurde.
		/// </summary>
		public bool OptimatBenaacrictigeÜüberÄnderung
		{
			get
			{
				var VonOptimatMeldungZuusctandLezte = this.VonOptimatMeldungZuusctandLezte;

				if (null == VonOptimatMeldungZuusctandLezte)
				{
					return true;
				}

				var VorsclaagListeWirkung = VonOptimatMeldungZuusctandLezte.VorsclaagListeWirkung;

				var VorsclaagListeWirkungTailmengeNocNitAusgefüürt =
					VorsclaagListeWirkung
					.WhereNullable((KandidaatVorsclaagWirkung) =>
						!(ListeNaacProcessWirkung.AnyNullable((KandidaatWirkung) => KandidaatVorsclaagWirkung.Ident == KandidaatWirkung.VorsclaagWirkungIdent)	?? false))
					.ToArrayNullable();

				if (VorsclaagListeWirkungTailmengeNocNitAusgefüürt.NullOderLeer())
				{
					return true;
				}

				var VorsclaagWirkungNocNitAusgefüürtLezte =
					VorsclaagListeWirkungTailmengeNocNitAusgefüürt
					.OrderByDescending((Kandidaat) => Kandidaat.NuzerZaitMili ?? int.MinValue)
					.FirstOrDefault();

				var VorsclaagWirkungNocNitAusgefüürtLezteAlter =
					Bib3.Glob.StopwatchZaitMiliSictInt() - VorsclaagWirkungNocNitAusgefüürtLezte.NuzerZaitMili;

				if (VorsclaagWirkungNocNitAusgefüürtLezteAlter < 3333)
				{
					return false;
				}

				var WirkungAusgefüürtLezte =
					ListeNaacProcessWirkung
					.OrderByDescendingNullable((Kandidaat) => Kandidaat.EndeZait ?? int.MinValue)
					.FirstOrDefaultNullable();

				if (null != WirkungAusgefüürtLezte)
				{
					if (null == NaacAnwendungZuMeldeGbsBaumWurzel)
					{
						return false;
					}

					if (NaacAnwendungZuMeldeGbsBaumWurzel.BeginZait < WirkungAusgefüürtLezte.EndeZait)
					{
						return false;
					}
				}

				return true;
			}
		}

		void EveOnlnServerKümere()
		{
			var BeginZaitMikro = Bib3.Glob.StopwatchZaitMikroSictInt();

			var BeginZaitMili = BeginZaitMikro / 1000;

			var EveOnlnSensoScnapscusAuswertLezteFertig = this.EveOnlnSensoScnapscusAuswertLezteFertig;

			if (null == EveOnlnSensoScnapscusAuswertLezteFertig)
			{
				return;
			}

			var EveOnlnSensoScnapscusAuswertLezteFertigBeginZait = EveOnlnSensoScnapscusAuswertLezteFertig.BeginZait;

			if (!EveOnlnSensoScnapscusAuswertLezteFertigBeginZait.HasValue)
			{
				return;
			}

			if (EveOnlnServerProzesScnapscusAusgewertetLezteZait == EveOnlnSensoScnapscusAuswertLezteFertigBeginZait)
			{
				return;
			}

			EveOnlnServerProzesScnapscusAusgewertetLezteZait = EveOnlnSensoScnapscusAuswertLezteFertigBeginZait ?? 0;

			if (null == EveOnlnSensoScnapscusAuswertLezteFertig.Wert)
			{
				return;
			}

			NaacAnwendungZuMeldeGbsBaumWurzel = EveOnlnSensoScnapscusAuswertLezteFertig;
		}
		 * */
	}
}
