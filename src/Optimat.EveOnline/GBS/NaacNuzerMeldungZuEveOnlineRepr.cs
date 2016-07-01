using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimat.EveOnline.GBS
{
	public	class SictNaacNuzerMeldungZuEveOnlineRepr
	{
		readonly public SictNaacNuzerMeldungZuEveOnlineSictNuzer Repräsentiirte;

		public Int64? ZaitMili;

		public SictNaacNuzerMeldungZuEveOnlineRepr(
			SictNaacNuzerMeldungZuEveOnlineSictNuzer Repräsentiirte)
		{
			this.Repräsentiirte = Repräsentiirte;
		}

		public SictNaacNuzerMeldungZuEveOnline RepräsentiirteMeldung
		{
			get
			{
				var Repräsentiirte = this.Repräsentiirte;

				if (null == Repräsentiirte)
				{
					return null;
				}

				return Repräsentiirte.Meldung;
			}
		}

		public Bib3.FCL.GBS.SictSymboolAkzeptanzFeelerWarnung.SymboolTypSictEnum SymboolTyp
		{
			get
			{
				return Glob.SymboolTypBerecneAusNaacNuzerMeldung(RepräsentiirteMeldung);
			}
		}

		public string MeldungSictString
		{
			get
			{
				return Base.TempUsedByBotAndUI.Extension.MeldungSictStringBerecneAusNaacNuzerMeldung(RepräsentiirteMeldung);
			}
		}

		public int? MeldungDringlickait
		{
			get
			{
				var RepräsentiirteMeldung = this.RepräsentiirteMeldung;

				if (null == RepräsentiirteMeldung)
				{
					return null;
				}

				var	Severity	= RepräsentiirteMeldung.Severity;

				if (!Severity.HasValue)
				{
					return null;
				}

				return (int)Severity.Value;
			}
		}

		public string MeldungDringlickaitSictString
		{
			get
			{
				var RepräsentiirteMeldung = this.RepräsentiirteMeldung;

				if (null == RepräsentiirteMeldung)
				{
					return null;
				}

				var	Severity	= RepräsentiirteMeldung.Severity;

				return Severity.ToString();
			}
		}

		public bool? VonServerNocExistent
		{
			get
			{
				var Repräsentiirte = this.Repräsentiirte;

				if (null == Repräsentiirte)
				{
					return null;
				}

				return Repräsentiirte.VonServerNocExistent;
			}
		}

		public Int64? MeldungIdent
		{
			get
			{
				var Repräsentiirte = this.RepräsentiirteMeldung;

				if (null == Repräsentiirte)
				{
					return null;
				}

				return Repräsentiirte.Ident;
			}
		}

		public Int64? SictungFrüühesteAlter
		{
			get
			{
				var Repräsentiirte = this.RepräsentiirteMeldung;

				if (null == Repräsentiirte)
				{
					return null;
				}

				return (ZaitMili - Repräsentiirte.BeginZait)	/ 1000;
			}
		}

		public Int64? SictungLezteAlter
		{
			get
			{
				var Repräsentiirte = this.RepräsentiirteMeldung;

				if (null == Repräsentiirte)
				{
					return null;
				}

				return (ZaitMili - Repräsentiirte.AktiivLezteZait)	/ 1000;
			}
		}
	}
}
