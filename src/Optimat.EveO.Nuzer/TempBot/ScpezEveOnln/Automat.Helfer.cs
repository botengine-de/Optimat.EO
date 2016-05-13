using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Bib3;
using Newtonsoft.Json;
using Optimat.EveOnline;
using Optimat.EveOnline.Anwendung;
//using Optimat.EveOnline.AuswertGbs;
using VonSensor = Optimat.EveOnline.VonSensor;


namespace Optimat.ScpezEveOnln
{
	[JsonObject(MemberSerialization.OptIn)]
	public class SictAnforderungMenuKaskaadeAstBedingung
	{
		/// <summary>
		/// Für jeede Menu in der Kaskaade kan für aine Raihe von Prioritääten jewails ain Suucmuster angegeeben werden.
		/// Fals für aine Prioritäät ain zum Suucmuster pasender Entry gefunden wird, wird diiser ausgewäält.
		/// </summary>
		[JsonProperty]
		public string[] ListePrioEntryRegexPattern
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? AuswaalEntryVerursactMenuSclus
		{
			private set;
			get;
		}

		public SictAnforderungMenuKaskaadeAstBedingung()
			:
			this((string[])null)
		{
		}

		public SictAnforderungMenuKaskaadeAstBedingung(
			string EntryRegexPattern,
			bool? AuswaalEntryVerursactMenuSclus	= null)
			:
			this(
			new string[] { EntryRegexPattern },
			AuswaalEntryVerursactMenuSclus)
		{
		}

		public SictAnforderungMenuKaskaadeAstBedingung(
			string[] ListePrioEntryRegexPattern,
			bool? AuswaalEntryVerursactMenuSclus	= null)
		{
			this.ListePrioEntryRegexPattern = ListePrioEntryRegexPattern;
			this.AuswaalEntryVerursactMenuSclus = AuswaalEntryVerursactMenuSclus;
		}

		public VonSensor.MenuEntry[] AusMengeMenuEntryGibUntermengePasendFürFrüühestePasendePrio(
			IEnumerable<VonSensor.MenuEntry> MengeMenuEntry)
		{
			var ListePrioMengeMenuEntryPasend = AusMengeMenuEntryGibUntermengePasendGrupiirtNaacPrio(MengeMenuEntry);

			if (null == ListePrioMengeMenuEntryPasend)
			{
				return null;
			}

			var PrioMengeMenuEntryPasend =
				ListePrioMengeMenuEntryPasend
				.FirstOrDefault((Kandidaat) => (null == Kandidaat) ? false : 0 < Kandidaat.Length);

			return PrioMengeMenuEntryPasend;
		}

		public VonSensor.MenuEntry[][] AusMengeMenuEntryGibUntermengePasendGrupiirtNaacPrio(
			IEnumerable<VonSensor.MenuEntry> MengeMenuEntry)
		{
			if (null == MengeMenuEntry)
			{
				return null;
			}

			var ListePrioEntryRegexPattern = this.ListePrioEntryRegexPattern;

			if (null == ListePrioEntryRegexPattern)
			{
				return null;
			}

			var ListePrioMengeEntryPasend = new List<VonSensor.MenuEntry[]>();

			for (int PrioIndex = 0; PrioIndex < ListePrioEntryRegexPattern.Length; PrioIndex++)
			{
				var PrioMengeEntryPasend = new List<VonSensor.MenuEntry>();

				try
				{
					var PrioEntryRegex = ListePrioEntryRegexPattern[PrioIndex];

					if (null == PrioEntryRegex)
					{
						continue;
					}

					var MengeEntryPasend =
						MengeMenuEntry
						.Where((KandidaatEntry) => Regex.Match(KandidaatEntry.Bescriftung ?? "", PrioEntryRegex, RegexOptions.IgnoreCase).Success)
						.ToArray();

					PrioMengeEntryPasend.AddRange(MengeEntryPasend);
				}
				finally
				{
					ListePrioMengeEntryPasend.Add(PrioMengeEntryPasend.ToArray());
				}
			}

			return ListePrioMengeEntryPasend.ToArray();
		}
	}

	public	struct	SictAusZuusctandAblaitungFürEntscaidungVorsclaagWirkung
	{
		public	bool VonNuzerParamAutoFraigaabe;
		public bool VonNuzerParamAutoRouteFraigaabe;

		public	Int64?	SelbstShipWarpingLezteAlterMiliNulbar;
		public	Int64?	SelbstShipJumpingLezteAlterMiliNulbar;
		public Int64? SelbstShipUndockingLezteAlterMiliNulbar;
		public Int64 SelbstShipWarpingOderJumpingLezteAlterMili;

		public Int64 FluctLezteAlterMili;
	}

	public	partial	class SictAutomat
	{
		static OrtogoonInt[] MengeFläceRandUmFläce(
			OrtogoonInt FläceZuUmrande,
			int RandGrööse)
		{
			if (null == FläceZuUmrande)
			{
				return null;
			}

			var RandListeTailFläce = new List<OrtogoonInt>();

			for (int SaiteIndex = 0; SaiteIndex < 4; SaiteIndex++)
			{
				var AxeIndex = SaiteIndex % 2;
				var AxeNictIndex = (AxeIndex + 1) % 2;

				var RictungVorzaice = ((SaiteIndex / 2) % 2) * 2 - 1;

				var RandTailFläceMiteDistanzVonFläceMite =
					new Vektor2DInt(
						(AxeIndex * (FläceZuUmrande.Grööse.A + RandGrööse) / 2) * RictungVorzaice,
						(AxeNictIndex * (FläceZuUmrande.Grööse.B + RandGrööse) / 2) * RictungVorzaice);

				var RandTailFläceMite =
					FläceZuUmrande.ZentrumLaage + RandTailFläceMiteDistanzVonFläceMite;

				var RandTailFläce = OrtogoonInt.AusPunktZentrumUndGrööse(RandTailFläceMite, new Vektor2DInt(
					RandGrööse * (1 + AxeNictIndex * 2),
					RandGrööse * (1 + AxeIndex * 2)));

				RandListeTailFläce.Add(RandTailFläce);
			}

			return RandListeTailFläce.ToArray();
		}

		/*
		 * 2013.07.23
		 * Mus noc geänert were so das Verfüügbaare Distanze automaatisc erkante were
		 * */
		static KeyValuePair<int, string>[] ErwartungMengeOrbitDistanzMitMenuEntryText = new KeyValuePair<int, string>[]{
			/*
			 * 2013.08.29
			 * 
			 * 0m komt in Orbit Menu nit vor
			 * 
			new	KeyValuePair<int,	string>(0, "0 m"),
			 * */
			new	KeyValuePair<int,	string>(1000, "1.000 m"),
			new	KeyValuePair<int,	string>(2500, "2.500 m"),
			new	KeyValuePair<int,	string>(5000, "5.000 m"),
			new	KeyValuePair<int,	string>(7500, "7.500 m"),

			new	KeyValuePair<int,	string>(10000, "10 km"),
			new	KeyValuePair<int,	string>(15000, "15 km"),
			new	KeyValuePair<int,	string>(20000, "20 km"),
			new	KeyValuePair<int,	string>(25000, "25 km"),
			new	KeyValuePair<int,	string>(30000, "30 km"),
		};

		static public SictAnforderungMenuKaskaadeAstBedingung[] MenuPfaadOrbitFürDistanzMax(Int64 DistanzMax)
		{
			var MenuOrbitListeEntryOrdnetNaacPrio =
				ErwartungMengeOrbitDistanzMitMenuEntryText
				.OrderBy((Kandidaat) => Kandidaat.Key)
				.TakeWhile((Kandidaat, Index) => Index < 1 || Kandidaat.Key <= DistanzMax)
				.OrderByDescending((Kandidaat) => Kandidaat.Key)
				.Select((Kandidaat) => Kandidaat.Value)
				.ToArray();

			/*
			 * 2013.08.25
			 * Änderung Pfaad für Berüksictigung des Fal das Objekt in Warp Distanz
			 *
			var Pfaad =
				new SictAnforderungMenuKaskaadeAstBedingung[]{
					new	SictAnforderungMenuKaskaadeAstBedingung("Orbit"),
					new	SictAnforderungMenuKaskaadeAstBedingung(MenuOrbitListeEntryOrdnetNaacPrio),
					};
			 * */
			/*
			 * 2013.08.26
			 * Ersaz von "Warp to" durc "Align to".
			 * Grund: Warp to kan in mance Mission nit verwendet were "natural phenomena are disrupting warp".
			 * Auserdeem würde warp das bisheerige System zur trenung von Pfaade in Mission durcenand bringe.
			 * 
			var Pfaad =
				new SictAnforderungMenuKaskaadeAstBedingung[]{
					//	new	SictAnforderungMenuKaskaadeAstBedingung(new	string[]{"Orbit",	"Warp to Within.*m",	"Warp to Within"}),
					new	SictAnforderungMenuKaskaadeAstBedingung(new	string[]{"Orbit",	"Warp to Within"}),
					new	SictAnforderungMenuKaskaadeAstBedingung(MenuOrbitListeEntryOrdnetNaacPrio),
					};
			 * */

			/*
			 * 2013.08.28
			 * 
			 * Wegnaame "Align to": Diise Funktioon isc nur für Orbit zuusctändig.
			 * Berüksictigung des Fal das Objekt in Warp Distanz werd jezt von Aufruufende Funktioon übernome.
			 * 
			var Pfaad =
				new SictAnforderungMenuKaskaadeAstBedingung[]{
					new	SictAnforderungMenuKaskaadeAstBedingung(new	string[]{"Orbit",	"Align to"}),
					new	SictAnforderungMenuKaskaadeAstBedingung(MenuOrbitListeEntryOrdnetNaacPrio),
					};
			 * */

			var Pfaad =
				new SictAnforderungMenuKaskaadeAstBedingung[]{
					new	SictAnforderungMenuKaskaadeAstBedingung(new	string[]{"Orbit"}),
					new	SictAnforderungMenuKaskaadeAstBedingung(MenuOrbitListeEntryOrdnetNaacPrio,	true),
					};

			return Pfaad;
		}
	}
}
