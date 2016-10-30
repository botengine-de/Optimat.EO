using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Optimat.ScpezEveOnln;
using Bib3;

namespace Optimat.EveOnline.Anwendung
{
	[JsonObject(MemberSerialization.OptIn)]
	public class SictStrategikonInRaumAtomZwisceergeebnis
	{
		[JsonProperty]
		readonly public	Int64? BeginZait;

		[JsonProperty]
		public Int64? CargoDurcsuuceErfolgZait
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? ZersctööreErfolgZait
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? AnfliigeErfolgZait
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? ObjektExistentErfolgZait
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? ErfolgTailVorMengeOverviewObjektGrupeMesungZuErscteleZait
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? ErfolgTailVorMengeOverviewObjektGrupeMesungZuErscteleZaitLezte
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? MengeObjektGrupeMesungZaitScrankeMin
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? EntscaidungErfolgLezteZait
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? EntscaidungErfolgFrühesteZait
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictAufgaabeParam[] MengeAufgaabeObjektZuBearbaite
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictOverviewObjektGrupeEnum[] MengeOverviewObjektGrupeMesungZuErsctele
		{
			private set;
			get;
		}

		/// <summary>
		/// Für besere Überblik baim Debuge werden baim ersctele des AtomZwisceergeebnis di bai der Prüüfung der BeginBedingungErfült berüksictigte
		/// Ergeebnise anderer Atome hiir dauerhaft gescpaicert.
		/// </summary>
		[JsonProperty]
		readonly public KeyValuePair<int, Int64?>[] MengeBedingungKonjunktAtomBezaicerUndErfolgZait;

		/// <summary>
		/// Für besere Überblik baim Debuge werden baim ersctele des AtomZwisceergeebnis di bai der Prüüfung der BeginBedingungErfült berüksictigte
		/// Ergeebnise anderer Atome hiir dauerhaft gescpaicert.
		/// </summary>
		[JsonProperty]
		readonly public KeyValuePair<int, Int64?>[] MengeBedingungDisjunktAtomBezaicerUndErfolgZait;

		public SictStrategikonInRaumAtomZwisceergeebnis()
		{
		}

		public SictStrategikonInRaumAtomZwisceergeebnis(
			Int64 BeginZait,
			KeyValuePair<int, Int64?>[] MengeBedingungKonjunktAtomBezaicerUndErfolgZait,
			KeyValuePair<int, Int64?>[] MengeBedingungDisjunktAtomBezaicerUndErfolgZait)
		{
			this.BeginZait = BeginZait;
			this.MengeBedingungKonjunktAtomBezaicerUndErfolgZait = MengeBedingungKonjunktAtomBezaicerUndErfolgZait;
			this.MengeBedingungDisjunktAtomBezaicerUndErfolgZait = MengeBedingungDisjunktAtomBezaicerUndErfolgZait;
		}

		public bool? EntscaidungErfolg
		{
			get
			{
				var EntscaidungErfolgLezteZait = this.EntscaidungErfolgLezteZait;

				if (EntscaidungErfolgLezteZait.HasValue)
				{
					return true;
				}

				return null;
			}
		}

		public void AktualisiireTailEntscaidungErfolgFrühesteZait()
		{
			var EntscaidungErfolgLezteZait = this.EntscaidungErfolgLezteZait;

			if (EntscaidungErfolgLezteZait.HasValue)
			{
				if (!EntscaidungErfolgFrühesteZait.HasValue)
				{
					EntscaidungErfolgFrühesteZait = EntscaidungErfolgLezteZait;
				}
			}
			else
			{
				EntscaidungErfolgFrühesteZait = null;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="AutomaatZuusctand"></param>
		/// <param name="StrategikonInstanz"></param>
		/// <param name="InStrategikonAtom"></param>
		/// <param name="MengeObjektGrupeMesungZaitScrankeMin">
		/// Früheste Zait zu der dii Mesung der Objekt Grupe zum ausscliise der Existenz noc zu bearbaitender Objekte begine darf.</param>
		public void Aktualisiire(
			SictAutomatZuusctand AutomaatZuusctand,
			SictMissionStrategikonInstanz StrategikonInstanz,
			SictMissionStrategikonInRaumAtom InStrategikonAtom,
			Int64? MengeObjektGrupeMesungZaitScrankeMin)
		{
			this.MengeObjektGrupeMesungZaitScrankeMin = MengeObjektGrupeMesungZaitScrankeMin;

			var AtomZwisceergeebnis = this;

			if (null == AutomaatZuusctand)
			{
				return;
			}

			if (null == StrategikonInstanz)
			{
				return;
			}

			var ZaitMili = AutomaatZuusctand.NuzerZaitMili;

			var RaumMengeObjektCargoDurcsuuct = StrategikonInstanz.MengeObjektCargoDurcsuuct;

			var OverviewUndTarget = (null == AutomaatZuusctand) ? null : AutomaatZuusctand.OverviewUndTarget;

			var MengeInRaumObjekt = (null == OverviewUndTarget) ? null : OverviewUndTarget.MengeOverViewObjekt;

			Int64? ErfolgTailVorMengeOverviewObjektGrupeMesungZuErscteleZait = null;
			SictOverviewObjektGrupeEnum[] ZuMengeObjektGrupeMesungVolsctändigFeelend = null;
			var AtomZwisceergeebnisErfolg = false;
			Int64? MesungMengeObjektGrupeZaitScrankeMinPlusBeruhigungszait = null;

			try
			{
				if (null == InStrategikonAtom)
				{
					AtomZwisceergeebnisErfolg = true;
					return;
				}

				if (null == MengeInRaumObjekt)
				{
					return;
				}

				var InStrategikonAtomMengeObjektFilter = InStrategikonAtom.MengeObjektFilter;

				var InStrategikonAtomMengeObjektFilterGrupe =
					(null == InStrategikonAtomMengeObjektFilter) ? null :
					Bib3.Glob.ArrayAusListeFeldGeflact(
					InStrategikonAtomMengeObjektFilter.Select((ObjektFilter) =>
					{
						var BedingungTypeUndName = ObjektFilter.BedingungTypeUndName;

						if (null == BedingungTypeUndName)
						{
							return null;
						}

						return BedingungTypeUndName.MengeGrupeZuDurcsuuce;
					})
					.Where((Kandidaat) => null != Kandidaat))
					.Distinct()
					.ToArray();

				var MengeObjektGefiltert = MengeInRaumObjektGefiltert(MengeInRaumObjekt, InStrategikonAtomMengeObjektFilter);

				var MengeObjektGefiltertNocSictbar =
					(null == MengeObjektGefiltert) ? null :
					MengeObjektGefiltert.Where((OverViewObjekt) => ZaitMili <= OverViewObjekt.SictungLezteZait).ToArray();

				var BedingungObjektExistentErfült = false;

				SictOverViewObjektZuusctand[] MengeObjektCargoZuDurcsuuce = null;
				SictOverViewObjektZuusctand[] MengeObjektZuZersctööre = null;
				KeyValuePair<SictOverViewObjektZuusctand, Int64>? ObjektAnzufliigeUndDistanceScranke = null;

				var FilterCargoZuDurcsuuce =
					new Func<SictOverViewObjektZuusctand, bool>((InRaumObjekt) => SictMissionZuusctand.FilterCargoZuDurcsuuce(InRaumObjekt, RaumMengeObjektCargoDurcsuuct));

				if (!(true == InStrategikonAtom.BedingungObjektExistent) ||
					!MengeObjektGefiltertNocSictbar.IsNullOrEmpty())
				{
					BedingungObjektExistentErfült = true;
				}

				var InStrategikonAtomZuObjektDistanzAinzuscteleScrankeMax = InStrategikonAtom.ZuObjektDistanzAinzuscteleScrankeMax;

				if (null != MengeObjektGefiltert)
				{
					if (true == InStrategikonAtom.ObjektDurcsuuceCargo)
					{
						MengeObjektCargoZuDurcsuuce = MengeObjektGefiltert.Where(FilterCargoZuDurcsuuce).ToArray();
					}

					if (true == InStrategikonAtom.ObjektZersctööre)
					{
						MengeObjektZuZersctööre = MengeObjektGefiltert;
					}

					if (InStrategikonAtomZuObjektDistanzAinzuscteleScrankeMax.HasValue)
					{
						var MengeObjektGefiltertDistanceNitPasend =
							MengeObjektGefiltert.Where((InRaumObjekt) => !(InRaumObjekt.SictungLezteDistanceScrankeMaxScpezOverview <= InStrategikonAtomZuObjektDistanzAinzuscteleScrankeMax.Value)).ToArray();

						if (0 < MengeObjektGefiltertDistanceNitPasend.Length)
						{
							ObjektAnzufliigeUndDistanceScranke = new KeyValuePair<SictOverViewObjektZuusctand, Int64>(
								MengeObjektGefiltertDistanceNitPasend.FirstOrDefault(), InStrategikonAtomZuObjektDistanzAinzuscteleScrankeMax.Value);
						}
					}
				}

				var AtomZwisceergeebnisMengeAufgaabeObjektZuBearbaite = new List<SictAufgaabeParam>();

				if (ObjektAnzufliigeUndDistanceScranke.HasValue)
				{
					if (null != ObjektAnzufliigeUndDistanceScranke.Value.Key)
					{
						AtomZwisceergeebnisMengeAufgaabeObjektZuBearbaite.Add(
							AufgaabeParamAndere.AufgaabeDistanceAinzusctele(
							ObjektAnzufliigeUndDistanceScranke.Value.Key, null, ObjektAnzufliigeUndDistanceScranke.Value.Value));
					}
				}

				if (null != MengeObjektCargoZuDurcsuuce)
				{
					AtomZwisceergeebnisMengeAufgaabeObjektZuBearbaite.AddRange(
						MengeObjektCargoZuDurcsuuce.Select((InRaumObjekt) => AufgaabeParamAndere.AufgaabeAktioonCargoDurcsuuce(InRaumObjekt)));
				}

				if (null != MengeObjektZuZersctööre)
				{
					AtomZwisceergeebnisMengeAufgaabeObjektZuBearbaite.AddRange(
						MengeObjektZuZersctööre.Select((InRaumObjekt) => new AufgaabeParamDestrukt(InRaumObjekt)));
				}

				AtomZwisceergeebnis.MengeAufgaabeObjektZuBearbaite = AtomZwisceergeebnisMengeAufgaabeObjektZuBearbaite.ToArray();

				if (MengeObjektCargoZuDurcsuuce.IsNullOrEmpty())
				{
					if (!AtomZwisceergeebnis.CargoDurcsuuceErfolgZait.HasValue)
					{
						AtomZwisceergeebnis.CargoDurcsuuceErfolgZait = ZaitMili;
					}
				}
				else
				{
					AtomZwisceergeebnis.CargoDurcsuuceErfolgZait = null;
				}

				if (MengeObjektZuZersctööre.IsNullOrEmpty())
				{
					if (!AtomZwisceergeebnis.ZersctööreErfolgZait.HasValue)
					{
						AtomZwisceergeebnis.ZersctööreErfolgZait = ZaitMili;
					}
				}
				else
				{
					AtomZwisceergeebnis.ZersctööreErfolgZait = null;
				}

				if (ObjektAnzufliigeUndDistanceScranke.HasValue)
				{
					AtomZwisceergeebnis.AnfliigeErfolgZait = null;
				}
				else
				{
					if (!AtomZwisceergeebnis.AnfliigeErfolgZait.HasValue)
					{
						AtomZwisceergeebnis.AnfliigeErfolgZait = ZaitMili;
					}
				}

				if (BedingungObjektExistentErfült)
				{
					if (!AtomZwisceergeebnis.ObjektExistentErfolgZait.HasValue)
					{
						AtomZwisceergeebnis.ObjektExistentErfolgZait = ZaitMili;
					}
				}
				else
				{
					AtomZwisceergeebnis.ObjektExistentErfolgZait = null;
				}

				var ScritBedingungErfültBeruhigungszaitMili = InStrategikonAtom.ScritBedingungErfültBeruhigungszaitMili ?? 1000;

				var AtomZwisceergeebnisMengeScpezielErfültZaitNulbar = new Int64?[]{
							AtomZwisceergeebnis.CargoDurcsuuceErfolgZait,
							AtomZwisceergeebnis.ZersctööreErfolgZait,
							AtomZwisceergeebnis.AnfliigeErfolgZait,
							AtomZwisceergeebnis.ObjektExistentErfolgZait};

				if (AtomZwisceergeebnisMengeScpezielErfültZaitNulbar.Any((ScpezielErfültZaitNulbar) => !ScpezielErfültZaitNulbar.HasValue))
				{
					//	Aine der Scpezialisiirte Aufgaabe isc noc nit erfült.
					return;
				}

				ErfolgTailVorMengeOverviewObjektGrupeMesungZuErscteleZait =
					Bib3.Glob.Max(AtomZwisceergeebnisMengeScpezielErfültZaitNulbar) ?? 0;

				var MesungMengeObjektGrupeZaitScrankeMin =
					Bib3.Glob.Max(ErfolgTailVorMengeOverviewObjektGrupeMesungZuErscteleZait, MengeObjektGrupeMesungZaitScrankeMin) ?? 0;

				MesungMengeObjektGrupeZaitScrankeMinPlusBeruhigungszait =
					MesungMengeObjektGrupeZaitScrankeMin + ScritBedingungErfültBeruhigungszaitMili;

				ZuMengeObjektGrupeMesungVolsctändigFeelend =
					OverviewUndTarget.MengeObjektGrupeUntermengeOoneOverviewViewportFolgeVolsctändigNaacZait(
					InStrategikonAtomMengeObjektFilterGrupe,
					MesungMengeObjektGrupeZaitScrankeMin);

				if (!ZuMengeObjektGrupeMesungVolsctändigFeelend.IsNullOrEmpty())
				{
					//	Verzwaigung Für Debug Haltepunkt
				}

				if (MesungMengeObjektGrupeZaitScrankeMinPlusBeruhigungszait < ZaitMili)
				{
					/*
					 * 2014.06.12
					 * 
					 * Korektur: Versciibe diise Blok naac finally um überscraibe des vorherige Wert mit null zu ermööglice fals try Blok scon vorher unterbroce werd.
					 * 
					AtomZwisceergeebnis.MengeOverviewObjektGrupeMesungZuErsctele = ZuMengeObjektGrupeMesungVolsctändigFeelend;

					//	if (ZuMengeObjektGrupeMesungVolsctändigFeelend.Length < 1)
					if (Optimat.Glob.NullOderLeer(ZuMengeObjektGrupeMesungVolsctändigFeelend))
					{
						AtomZwisceergeebnisErfolg = true;
					}
					 * */
				}
			}
			finally
			{
				AtomZwisceergeebnis.MengeOverviewObjektGrupeMesungZuErsctele = ZuMengeObjektGrupeMesungVolsctändigFeelend;

				/*
				 * 2014.07.10	Korektur: zuusäzlice Bedingung:
				 * MesungMengeObjektGrupeZaitScrankeMinPlusBeruhigungszait < ZaitMili
				 * 
				//	if (ZuMengeObjektGrupeMesungVolsctändigFeelend.Length < 1)
				if (Optimat.Glob.NullOderLeer(ZuMengeObjektGrupeMesungVolsctändigFeelend))
				{
					AtomZwisceergeebnisErfolg = true;
				}
				 * */

				if (MesungMengeObjektGrupeZaitScrankeMinPlusBeruhigungszait < ZaitMili)
				{
					if (ZuMengeObjektGrupeMesungVolsctändigFeelend.IsNullOrEmpty())
						AtomZwisceergeebnisErfolg = true;
				}

				AtomZwisceergeebnis.ErfolgTailVorMengeOverviewObjektGrupeMesungZuErscteleZait =
					ErfolgTailVorMengeOverviewObjektGrupeMesungZuErscteleZait;

				if (ErfolgTailVorMengeOverviewObjektGrupeMesungZuErscteleZait.HasValue)
				{
					AtomZwisceergeebnis.ErfolgTailVorMengeOverviewObjektGrupeMesungZuErscteleZaitLezte = ErfolgTailVorMengeOverviewObjektGrupeMesungZuErscteleZait;
				}

				if (AtomZwisceergeebnisErfolg)
				{
					AtomZwisceergeebnis.EntscaidungErfolgLezteZait = ZaitMili;
				}
				else
				{
					AtomZwisceergeebnis.EntscaidungErfolgLezteZait = null;
				}

				AtomZwisceergeebnis.AktualisiireTailEntscaidungErfolgFrühesteZait();
			}
		}

		static public SictOverViewObjektZuusctand[] MengeInRaumObjektGefiltert(
			IEnumerable<SictOverViewObjektZuusctand> MengeInRaumObjekt,
			SictStrategikonOverviewObjektFilter[] MengeFilterDisjunkt)
		{
			if (null == MengeInRaumObjekt)
			{
				return null;
			}

			if (null == MengeFilterDisjunkt)
			{
				return null;
			}

			var MengeInRaumObjektFiltert = new List<SictOverViewObjektZuusctand>();

			foreach (var InRaumObjekt in MengeInRaumObjekt)
			{
				if (null == InRaumObjekt)
				{
					continue;
				}

				foreach (var FilterDisjunkt in MengeFilterDisjunkt)
				{
					if (null == FilterDisjunkt)
					{
						continue;
					}

					if (FilterDisjunkt.Pasend(InRaumObjekt))
					{
						MengeInRaumObjektFiltert.Add(InRaumObjekt);
						break;
					}
				}
			}

			return MengeInRaumObjektFiltert.ToArray();
		}
	}
}
