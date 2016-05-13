using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Optimat.ScpezEveOnln;

namespace Optimat.EveOnline.Anwendung
{
	public partial class SictMissionStrategikonInstanz
	{
		static KeyValuePair<int, Int64?>[] MengeBedingungAtomBezaicnerUndZaitErfült(
			KeyValuePair<int,	bool>[]	MengeBedingungAtomBezaicnerUndNegatioon,
			IDictionary<int,	SictStrategikonInRaumAtomZwisceergeebnis>	MengeZuAtomBezaicnerZwisceergeebnis)
		{
			if (null == MengeBedingungAtomBezaicnerUndNegatioon)
			{
				return null;
			}

			var MengeBedingungAtomBezaicnerUndZaitErfült = new List<KeyValuePair<int, Int64?>>();

			foreach (var BedingungAtomBezaicnerUndNegatioon in MengeBedingungAtomBezaicnerUndNegatioon)
			{
				var	 BedingungAtomBezaicner	= BedingungAtomBezaicnerUndNegatioon.Key;
				var BedingungNegatioon = BedingungAtomBezaicnerUndNegatioon.Value;

				Int64?	BedingungZaitErfült = null;

				try
				{
					if (null == MengeZuAtomBezaicnerZwisceergeebnis)
					{
						continue;
					}

					SictStrategikonInRaumAtomZwisceergeebnis	BedingungAtomZwisceergeebnis	= null;

					MengeZuAtomBezaicnerZwisceergeebnis.TryGetValue(BedingungAtomBezaicner, out	BedingungAtomZwisceergeebnis);

					if (null == BedingungAtomZwisceergeebnis)
					{
						continue;
					}

					var	BedingungAtomZwisceergeebnisEntscaidungErfolgFrühesteZait	= BedingungAtomZwisceergeebnis.EntscaidungErfolgFrühesteZait;

					if (BedingungNegatioon)
					{
						if (BedingungAtomZwisceergeebnisEntscaidungErfolgFrühesteZait.HasValue)
						{
							continue;
						}

						BedingungZaitErfült = BedingungAtomZwisceergeebnis.BeginZait;
					}
					else
					{
						BedingungZaitErfült = BedingungAtomZwisceergeebnisEntscaidungErfolgFrühesteZait;
					}
				}
				finally
				{
					MengeBedingungAtomBezaicnerUndZaitErfült.Add(
						new KeyValuePair<int, Int64?>(BedingungAtomBezaicner, BedingungZaitErfült));
				}
			}

			return MengeBedingungAtomBezaicnerUndZaitErfült.ToArray();
		}

		/// <summary>
		/// Berecnet anhand der bisherige Entscaidunge zu Atome welce Atome bearbaitet werde sole.
		/// Berecnet zu Atom MengeObjekt.
		/// Berecnet zu Atom Entscaidung Erfolg.
		/// </summary>
		/// <param name="AutomaatZuusctand"></param>
		/// <param name="Strategikon"></param>
		/// <param name="StrategikonInstanz"></param>
		static public void StrategikonInRaumFortscritBerecne(
			SictAutomatZuusctand AutomaatZuusctand,
			SictMissionStrategikon Strategikon,
			SictMissionStrategikonInstanz	StrategikonInstanz)
		{
			if (null == Strategikon)
			{
				return;
			}

			if (null == StrategikonInstanz)
			{
				return;
			}

			if (null == AutomaatZuusctand)
			{
				return;
			}

			var ZaitMili = AutomaatZuusctand.NuzerZaitMili;

			var OverviewUndTarget = (null == AutomaatZuusctand) ? null : AutomaatZuusctand.OverviewUndTarget;

			var MengeInRaumObjekt = (null == OverviewUndTarget) ? null : OverviewUndTarget.MengeOverViewObjekt;

			var RaumZuStrategikonAtomZwisceergeebnis = StrategikonInstanz.ZuStrategikonAtomZwisceergeebnis;

			var RaumMengeObjektCargoDurcsuuct = StrategikonInstanz.MengeObjektCargoDurcsuuct;

			var StrategikonRaumMengeZuBezaicnerAtom = Strategikon.RaumMengeZuBezaicnerAtom;

			if (null == StrategikonRaumMengeZuBezaicnerAtom)
			{
				return;
			}

			if (null != RaumZuStrategikonAtomZwisceergeebnis)
			{
				//	Berecnung noi zu beginende Atome

				foreach (var StrategikonRaumZuBezaicnerAtom in StrategikonRaumMengeZuBezaicnerAtom)
				{
					var InStrategikonAtomBezaicner = StrategikonRaumZuBezaicnerAtom.Key;
					var StrategikonRaumAtom = StrategikonRaumZuBezaicnerAtom.Value;

					if (null == StrategikonRaumAtom)
					{
						continue;
					}

					var BisherZwisceergeebnis = Optimat.Glob.TAD(RaumZuStrategikonAtomZwisceergeebnis, InStrategikonAtomBezaicner);

					if (null != BisherZwisceergeebnis)
					{
						//	Abarbaitung diises Atom wurde beraits begone.
						continue;
					}

					var	BeginMengeBedingungKonjunktAtomBezaicerUndErfolgZait	=
						MengeBedingungAtomBezaicnerUndZaitErfült(StrategikonRaumAtom.MengeBedingungKonjunkt,RaumZuStrategikonAtomZwisceergeebnis);

					var	BeginMengeBedingungDisjunktAtomBezaicerUndErfolgZait	=
						MengeBedingungAtomBezaicnerUndZaitErfült(StrategikonRaumAtom.MengeBedingungDisjunkt,RaumZuStrategikonAtomZwisceergeebnis);

					var	BedingungKonjunktErfült	=
						(null	== BeginMengeBedingungKonjunktAtomBezaicerUndErfolgZait)	?	(bool?)null	:
						BeginMengeBedingungKonjunktAtomBezaicerUndErfolgZait.All((BedingungKonjunktTail) => BedingungKonjunktTail.Value.HasValue);

					var	BedingungDisjunktErfült	=
						(null	== BeginMengeBedingungDisjunktAtomBezaicerUndErfolgZait)	?	(bool?)null	:
						BeginMengeBedingungDisjunktAtomBezaicerUndErfolgZait.Any((BedingungKonjunktTail) => BedingungKonjunktTail.Value.HasValue);

					if ((null == BedingungKonjunktErfült && null == BedingungDisjunktErfült) ||
						true == BedingungKonjunktErfült || true == BedingungDisjunktErfült)
					{
						if (0 == StrategikonRaumZuBezaicnerAtom.Key)
						{
							//	Verzwaigung für Haltepunkt (Wurzel Atom werd aingescaltet)
						}

						var Zwisceergeebnis = new SictStrategikonInRaumAtomZwisceergeebnis(
							ZaitMili,
							BeginMengeBedingungKonjunktAtomBezaicerUndErfolgZait,
							BeginMengeBedingungDisjunktAtomBezaicerUndErfolgZait);

						RaumZuStrategikonAtomZwisceergeebnis[InStrategikonAtomBezaicner] = Zwisceergeebnis;
					}
				}
			}

			if (null != RaumZuStrategikonAtomZwisceergeebnis)
			{
				//	Berecnung welce Atome beraits fertiggesctelt

				foreach (var AtomZwisceergeebnisMitBezaicner in RaumZuStrategikonAtomZwisceergeebnis)
				{
					var InStrategikonAtomBezaicner = AtomZwisceergeebnisMitBezaicner.Key;
					var AtomZwisceergeebnis = AtomZwisceergeebnisMitBezaicner.Value;

					/*
					 * 2014.03.28
					 * 
					var InStrategikonAtomSctruktuur = Optimat.Glob.TAD(StrategikonRaumMengeZuBezaicnerAtom, InStrategikonAtomBezaicner);
					 * */
					var InStrategikonAtomSctruktuur = Bib3.Extension.FirstOrDefaultNullable(
						StrategikonRaumMengeZuBezaicnerAtom,
						(Kandidaat) => Kandidaat.Key == InStrategikonAtomBezaicner).Value;

					if (null == AtomZwisceergeebnis)
					{
						continue;
					}

					var InStrategikonAtom = (null == InStrategikonAtomSctruktuur) ? null : InStrategikonAtomSctruktuur.Atom;

					var MengeAtomZwiscenergeebnisInArbaitParalel = new List<SictStrategikonInRaumAtomZwisceergeebnis>();

					if (null != InStrategikonAtomSctruktuur)
					{
						var	MengeBedingungAtomBezaicner	= new	List<int>();

						var InStrategikonAtomSctruktuurMengeBedingungKonjunkt = InStrategikonAtomSctruktuur.MengeBedingungKonjunkt;
						var InStrategikonAtomSctruktuurMengeBedingungDisjunkt = InStrategikonAtomSctruktuur.MengeBedingungDisjunkt;

						if (null != InStrategikonAtomSctruktuurMengeBedingungKonjunkt)
						{
							MengeBedingungAtomBezaicner.AddRange(InStrategikonAtomSctruktuurMengeBedingungKonjunkt.Select((Bedingung) => Bedingung.Key));
						}

						if (null != InStrategikonAtomSctruktuurMengeBedingungDisjunkt)
						{
							MengeBedingungAtomBezaicner.AddRange(InStrategikonAtomSctruktuurMengeBedingungDisjunkt.Select((Bedingung) => Bedingung.Key));
						}

						foreach (var AtomParalelBezaicner in MengeBedingungAtomBezaicner)
						{
							if(AtomParalelBezaicner	== AtomZwisceergeebnisMitBezaicner.Key)
							{
								//	Selbsct
								continue;
							}

							var AtomParalelZwiscenergeebnis = Optimat.Glob.TAD(RaumZuStrategikonAtomZwisceergeebnis, AtomParalelBezaicner);

							if (null == AtomParalelZwiscenergeebnis)
							{
								continue;
							}

							MengeAtomZwiscenergeebnisInArbaitParalel.Add(AtomParalelZwiscenergeebnis);
						}
					}

					var MengeAtomZwiscenergeebnisInArbaitParalelErfolgTailVorMesungObjektGrupe =
						MengeAtomZwiscenergeebnisInArbaitParalel
						.Select((AtomParalelZwisceergeebnis) => AtomParalelZwisceergeebnis.ErfolgTailVorMengeOverviewObjektGrupeMesungZuErscteleZait)
						.ToArray();

					/*
					 * Als Scranke für di Zait zu der zulezt geprüüft werd ob noc zu bearbaitende Objekte vorhande sind werden
					 * von alen Paralel bearbaitete Atome di Zaite entnome zu der jewails in Atom ErfolgTailVorMesungObjektGrupe festgesctelt wurde.
					 * 
					 * Diis werd z.B. benöötigt in Mission in der ain Objekt desen Cargo durcsuuct were sol sictbar werd naacdeem aines der zu
					 * zersctöörende Objekte zersctöört wurde. Di Anwaisunge zum zersctööre sind in andere Atom enthalte als di Anwaisunge zum durcsuuce
					 * von Cargo. Über den Parameter MengeObjektGrupeMesungZaitScrankeMin werd di Zait des Erfolg aus ainem AtomZwisceergebnis dan als Scranke
					 * für ale Paralel ausgefüürte Atome übernome.
					 * */
					Int64? MengeObjektGrupeMesungZaitScrankeMin = Bib3.Glob.Max(MengeAtomZwiscenergeebnisInArbaitParalelErfolgTailVorMesungObjektGrupe);

					AtomZwisceergeebnis.Aktualisiire(
						AutomaatZuusctand,
						StrategikonInstanz,
						InStrategikonAtom,
						MengeObjektGrupeMesungZaitScrankeMin);
				}
			}
		}

	}
}
