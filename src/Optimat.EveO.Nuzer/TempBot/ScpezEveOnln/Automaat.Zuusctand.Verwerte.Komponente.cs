using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Optimat.EveOnline.Anwendung;


namespace Optimat.ScpezEveOnln
{
	public	struct SictAufgaabeParamZerleegungErgeebnis
	{
		public IEnumerable<SictAufgaabeParam> ListeKomponenteAufgaabeParam
		{
			private set;
			get;
		}
			
		public bool ZerleegungVolsctändig
		{
			private	set;
			get;
		}

		public	Int64? ReegelungDistanceScpiilraumRest
		{
			private set;
			get;
		}

		static public SictAufgaabeParamZerleegungErgeebnis ZerleegungLeerVolsctändig()
		{
			return new SictAufgaabeParamZerleegungErgeebnis(null, true);
		}

		public SictAufgaabeParamZerleegungErgeebnis(
			IEnumerable<SictAufgaabeParam> ListeKomponenteAufgaabeParam,
			bool ZerleegungVolsctändig)
			:
			this()
		{
			this.ListeKomponenteAufgaabeParam = ListeKomponenteAufgaabeParam;
			this.ZerleegungVolsctändig = ZerleegungVolsctändig;
		}

		public SictAufgaabeParamZerleegungErgeebnis Kombiniire(
			SictAufgaabeParamZerleegungErgeebnis O1)
		{
			return new SictAufgaabeParamZerleegungErgeebnis(
				Bib3.Glob.ListeEnumerableAgregiirt(this.ListeKomponenteAufgaabeParam, O1.ListeKomponenteAufgaabeParam),
				this.ZerleegungVolsctändig & O1.ZerleegungVolsctändig);
		}

		public SictAufgaabeParamZerleegungErgeebnis Kombiniire(
			IEnumerable<SictAufgaabeParam> ListeKomponenteAufgaabeParam,
			bool	ZerleegungVolsctändig	= true)
		{
			return new SictAufgaabeParamZerleegungErgeebnis(
				Bib3.Glob.ListeEnumerableAgregiirt(this.ListeKomponenteAufgaabeParam, ListeKomponenteAufgaabeParam),
				this.ZerleegungVolsctändig	& ZerleegungVolsctändig);
		}

		public void FüügeAn(
			SictAufgaabeParamZerleegungErgeebnis O1)
		{
			FüügeAn(O1.ListeKomponenteAufgaabeParam, O1.ZerleegungVolsctändig);
		}

		public void FüügeAn(
			IEnumerable<SictAufgaabeParam> ListeKomponenteAufgaabeParam,
			bool	ZerleegungVolsctändig	= true)
		{
			this.ListeKomponenteAufgaabeParam = Bib3.Glob.ListeEnumerableAgregiirt(this.ListeKomponenteAufgaabeParam, ListeKomponenteAufgaabeParam);
			this.ZerleegungVolsctändig	= this.ZerleegungVolsctändig	& ZerleegungVolsctändig;
		}

		public SictAufgaabeParamZerleegungErgeebnis Kombiniire(
			SictAufgaabeParam	KomponenteAufgaabeParam,
			bool ZerleegungVolsctändig = true)
		{
			return Kombiniire(new SictAufgaabeParam[] { KomponenteAufgaabeParam }, ZerleegungVolsctändig);
		}

		public void FüügeAn(
			SictAufgaabeParam KomponenteAufgaabeParam,
			bool ZerleegungVolsctändig = true)
		{
			FüügeAn(new SictAufgaabeParam[] { KomponenteAufgaabeParam }, ZerleegungVolsctändig);
		}

		public void FüügeAnReegelungDistanceScpiilraumRest(
			Int64? ReegelungDistanceScpiilraumRest)
		{
			if (!ReegelungDistanceScpiilraumRest.HasValue)
			{
				return;
			}

			if (this.ReegelungDistanceScpiilraumRest.HasValue)
			{
				this.ReegelungDistanceScpiilraumRest = Math.Min(this.ReegelungDistanceScpiilraumRest.Value, ReegelungDistanceScpiilraumRest.Value);
			}
			else
			{
				this.ReegelungDistanceScpiilraumRest = ReegelungDistanceScpiilraumRest;
			}
		}

		public	void	ZerleegungVolsctändigSezeAus()
		{
			ZerleegungVolsctändig	= false;
		}

		public	void	ZerleegungVolsctändigSezeAin()
		{
			ZerleegungVolsctändig	= true;
		}
	}
}
