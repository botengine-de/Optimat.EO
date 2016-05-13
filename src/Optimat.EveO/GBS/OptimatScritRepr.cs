using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bib3;
using Optimat.EveOnline.Base;

namespace Optimat.EveOnline.GBS
{
	public	class SictOptimatScritRepr
	{
		readonly public SictOptimatScrit Repräsentiirte;

		static public bool ReprPastZuOptimatScrit(
			SictOptimatScritRepr Repr,
			SictOptimatScrit OptimatScrit)
		{
			if (null	== OptimatScrit &&	null	== Repr)
			{
				return true;
			}

			if (null == Repr)
			{
				return false;
			}

			return Repr.Repräsentiirte == OptimatScrit;
		}

		/*
		 * 2015.03.03
		 * 
		SictVonProcessLeese VonProcessLeese
		{
			get
			{
				var Repräsentiirte = this.Repräsentiirte;

				if (null == Repräsentiirte)
				{
					return null;
				}

				return Repräsentiirte.VonProcessLeese;
			}
		}
		 * */

		VonProcessMesung<VonSensorikMesung> VonProcessLeese
		{
			get
			{
				var Repräsentiirte = this.Repräsentiirte;

				if (null == Repräsentiirte)
				{
					return null;
				}

				return Repräsentiirte.VonProcessMesung;
			}
		}


		public	Int64? ProcessLeeseBeginZaitMili
		{
			get
			{
				var VonProcessLeese = this.VonProcessLeese;

				if (null == VonProcessLeese)
				{
					return null;
				}

				return VonProcessLeese.BeginZait;
			}
		}

		public Int64? VonProcessLeeseDistanzVonBeginBisEndeMili
		{
			get
			{
				var VonProcessLeese = this.VonProcessLeese;

				if (null == VonProcessLeese)
				{
					return null;
				}

				return VonProcessLeese.Dauer;
			}
		}

		public string ProcessLeeseBeginZaitKalenderString
		{
			get
			{
				var ProcessLeeseBeginZaitMili = this.ProcessLeeseBeginZaitMili;

				if (!ProcessLeeseBeginZaitMili.HasValue)
				{
					return null;
				}

				return	Optimat.Glob.ZuStopwatchZaitMiliSictKalenderStringTailInTag(ProcessLeeseBeginZaitMili.Value,	1);
			}
		}

		public SictVorsclaagNaacProcessWirkung[] VorsclaagListeWirkung
		{
			get
			{
				var Repräsentiirte = this.Repräsentiirte;

				if (null == Repräsentiirte)
				{
					return null;
				}

				return Repräsentiirte.VorsclaagListeWirkung;
			}
		}

		public SictNaacProcessWirkung[] NaacZiilProcessListeWirkung
		{
			get
			{
				var Repräsentiirte = this.Repräsentiirte;

				if (null == Repräsentiirte)
				{
					return null;
				}

				return Repräsentiirte.NaacProcessListeWirkung;
			}
		}

		Int64? WirkungEndeZaitMili
		{
			get
			{
				var NaacZiilProcessListeWirkung = this.NaacZiilProcessListeWirkung;

				if (null == NaacZiilProcessListeWirkung)
				{
					return null;
				}

				var NaacZiilProcessListeWirkungEndeZait =
					NaacZiilProcessListeWirkung
					.Select((Wirkung) => (null == Wirkung) ? null : Wirkung.EndeZaitMili)
					.ToArray();

				return Bib3.Glob.Max(NaacZiilProcessListeWirkungEndeZait);
			}
		}

		public string[][] VorsclaagListeWirkungZwekListeKomponente
		{
			get
			{
				var VorsclaagListeWirkung = this.VorsclaagListeWirkung;

				if (null == VorsclaagListeWirkung)
				{
					return null;
				}

				return VorsclaagListeWirkung
					.Select((VorsclaagWirkung) => (null == VorsclaagListeWirkung) ? null : VorsclaagWirkung.WirkungZwekListeKomponente)
					.ToArray();
			}
		}

		public Int64? ZaitDistanzVonZiilProcessLeeseBeginBisWirkungEndeMili
		{
			get
			{
				return WirkungEndeZaitMili - ProcessLeeseBeginZaitMili;
			}
		}

		static public string VorsclaagListeWirkungZwekSictStringSictAggregatioon(
			string[][] VorsclaagListeWirkungZwekListeKomponente)
		{
			if (null == VorsclaagListeWirkungZwekListeKomponente)
			{
				return null;
			}

			var VorsclaagListeWirkungZwekListeKomponenteSictString =
				VorsclaagListeWirkungZwekListeKomponente
				.Select((VorsclaagWirkungZwekListeKomponente) =>
					VorsclaagWirkungZwekSictStringSictAggregatioon(VorsclaagWirkungZwekListeKomponente))
				.ToArray();

			return string.Join(Environment.NewLine, VorsclaagListeWirkungZwekListeKomponenteSictString);
		}

		static public string VorsclaagWirkungZwekSictStringSictAggregatioon(
			string[] VorsclaagWirkungZwekListeKomponente)
		{
			if (null == VorsclaagWirkungZwekListeKomponente)
			{
				return null;
			}

			return string.Join(" -> ", VorsclaagWirkungZwekListeKomponente);
		}

		public string VorsclaagWirkungZwekSictString
		{
			get
			{
				return VorsclaagListeWirkungZwekSictStringSictAggregatioon(VorsclaagListeWirkungZwekListeKomponente);
			}
		}

		public bool? WirkungFüüreAusErfolg
		{
			get
			{
				var NaacZiilProcessListeWirkung = this.NaacZiilProcessListeWirkung;

				if (null == NaacZiilProcessListeWirkung)
				{
					return null;
				}

				return NaacZiilProcessListeWirkung.All((NaacZiilProcessWirkung) => true == ((null == NaacZiilProcessWirkung) ? null : NaacZiilProcessWirkung.Erfolg));
			}
		}

		/*
		 * 2014.07.14
		 * 
		 * Ersaz durc SictNaacNuzerBerictAutomaatZuusctand NaacNuzerBerictAutomaatZuusctand
		 * 
		public SictAusOptimatScritNaacNuzerMeldung[] NaacNuzerMengeMeldung
		{
			get
			{
				var Repräsentiirte = this.Repräsentiirte;

				if (null == Repräsentiirte)
				{
					return null;
				}

				return Repräsentiirte.NaacNuzerMengeMeldung;
			}
		}

		public System.Windows.Visibility PanelMengeMeldungVisibility
		{
			get
			{
				var PanelMengeMeldungText = this.PanelMengeMeldungText;

				if (null == PanelMengeMeldungText)
				{
					return System.Windows.Visibility.Collapsed;
				}

				if (PanelMengeMeldungText.Length	< 1)
				{
					return System.Windows.Visibility.Collapsed;
				}

				return System.Windows.Visibility.Visible;
			}
		}

		public string PanelMengeMeldungText
		{
			get
			{
				var NaacNuzerMengeMeldung = this.NaacNuzerMengeMeldung;

				if (null == NaacNuzerMengeMeldung)
				{
					return null;
				}

				var	NaacNuzerMengeMeldungString	=
					NaacNuzerMengeMeldung
					.Select((Meldung) => Meldung.MeldungString)
					.Where((MeldungString) => null	!= MeldungString)
					.ToArray();

				return string.Join(Environment.NewLine, NaacNuzerMengeMeldungString);
			}
		}
		 * */

		public SictOptimatScritRepr(
			SictOptimatScrit Repräsentiirte)
		{
			this.Repräsentiirte = Repräsentiirte;
		}
	}
}
