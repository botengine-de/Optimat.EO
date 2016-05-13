using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Optimat.EveOnline;
using Optimat.EveOnline.Anwendung;
using Bib3;
using Optimat.EveOnline;
using Optimat.EveOnline.Base;
//using Optimat.EveOnline.AuswertGbs;
using Optimat.EveOnline.VonSensor;
using VonSensor = Optimat.EveOnline.VonSensor;
using Optimat.EveO.Nuzer.TempBot.Sonst;


namespace Optimat.ScpezEveOnln
{
	public partial class SictAutomatZuusctand
	{
		static SictAnforderungMenuKaskaadeAstBedingung[] AusButtonListSurroundingsMenuPfaadWarpNaacStationOderGate =
			new SictAnforderungMenuKaskaadeAstBedingung[]{
				new SictAnforderungMenuKaskaadeAstBedingung(new string[]{"Stations", "gates"}),
				new SictAnforderungMenuKaskaadeAstBedingung("."),
				new SictAnforderungMenuKaskaadeAstBedingung(new string[]{   "Warp to Within\\s*\\d"},   true),};

		/// <summary>
		/// Berüksictigt nict mööglice Verbindung der Fläce durc Überlapung.
		/// </summary>
		/// <param name="MengeFläce"></param>
		/// <returns></returns>
		static public Int64 AusMengeFläceBerecneGröösteKwadraatSaitelängeBerecne(
			IEnumerable<OrtogoonInt> MengeFläce)
		{
			if (null == MengeFläce)
			{
				return 0;
			}

			Int64 BisherSaitelänge = 0;

			foreach (var Fläce in MengeFläce)
			{
				if (null == Fläce)
				{
					continue;
				}

				var FläceKwadraatSaitelänge = Math.Min(Fläce.Grööse.A, Fläce.Grööse.B);

				BisherSaitelänge = Math.Max(BisherSaitelänge, FläceKwadraatSaitelänge);
			}

			return BisherSaitelänge;
		}

		public class SictGbsAstOklusioonInfo
		{
			readonly public SictGbsWindowZuusctand Window;

			readonly public SictGbsMenuZuusctand Menu;

			readonly public GbsElement Utilmenu;

			readonly public VonSensor.PanelGroup PanelGroup;

			readonly public GbsElement GbsElementScnapscus;

			//	readonly public float? NaacOklusioonRestFläceGröösteKwadraatSaitelänge;

			public int? GbsAstInBaumIndex
			{
				get
				{
					var GbsObjektScnapscus = this.GbsElementScnapscus;

					if (null == GbsObjektScnapscus)
					{
						return null;
					}

					return GbsObjektScnapscus.InGbsBaumAstIndex;
				}
			}

			public Int64? GbsElementIdent
			{
				get
				{
					var GbsObjektScnapscus = this.GbsElementScnapscus;

					if (null == GbsObjektScnapscus)
					{
						return null;
					}

					return GbsObjektScnapscus.Ident;
				}
			}

			public SictGbsAstOklusioonInfo()
			{
			}

			public SictGbsAstOklusioonInfo(
				SictGbsWindowZuusctand Window)
				:
				this(
				Window,
				null,
				null,
				null,
				(null == Window) ? null : Window.ScnapscusWindowLezte)
			{
			}

			public SictGbsAstOklusioonInfo(
				SictGbsMenuZuusctand Menu)
				:
				this(
				null,
				Menu,
				null,
				null,
				(null == Menu) ? null : Menu.AingangScnapscusTailObjektIdentLezteBerecne())
			{
			}

			public SictGbsAstOklusioonInfo(
				GbsElement Utilmenu)
				:
				this(
				null,
				null,
				Utilmenu,
				null,
				Utilmenu)
			{
			}

			public SictGbsAstOklusioonInfo(
				VonSensor.PanelGroup PanelGroup)
				:
				this(
				null,
				null,
				null,
				PanelGroup,
				PanelGroup)
			{
			}

			public SictGbsAstOklusioonInfo(
				SictGbsWindowZuusctand Window,
				SictGbsMenuZuusctand Menu,
				GbsElement Utilmenu,
				VonSensor.PanelGroup PanelGroup,
				GbsElement GbsElementScnapscus)
			{
				this.Window = Window;
				this.Menu = Menu;
				this.Utilmenu = Utilmenu;
				this.PanelGroup = PanelGroup;

				this.GbsElementScnapscus = GbsElementScnapscus;
			}

			public SictGbsAstOklusioonInfo(
				SictGbsAstOklusioonInfo ZuKopiire)
				:
				this(
				(null == ZuKopiire) ? null : ZuKopiire.Window,
				(null == ZuKopiire) ? null : ZuKopiire.Menu,
				(null == ZuKopiire) ? null : ZuKopiire.Utilmenu,
				(null == ZuKopiire) ? null : ZuKopiire.PanelGroup,
				(null == ZuKopiire) ? null : ZuKopiire.GbsElementScnapscus)
			{
			}

			/*
			2015.03.20

			public SictGbsAstOklusioonInfo(
				SictGbsAstOklusioonInfo ZuKopiire,
				float? NaacOklusioonRestFläceGröösteKwadraatSaitelänge)
				:
				this(ZuKopiire)
			{
				this.NaacOklusioonRestFläceGröösteKwadraatSaitelänge = NaacOklusioonRestFläceGröösteKwadraatSaitelänge;
			}
			*/
		}

		public class SictGbsAstOklusioonKombi
		{
			readonly public SictGbsAstOklusioonInfo Okludiirende;

			readonly public GbsElement Okludiirte;

			readonly public OrtogoonInt Scnitfläce;

			readonly public Int64 NaacOklusioonRestFläceGröösteKwadraatSaitelänge;

			public SictGbsAstOklusioonKombi()
			{
			}

			public SictGbsAstOklusioonKombi(
				SictGbsAstOklusioonInfo Okludiirende,
				GbsElement Okludiirte,
				OrtogoonInt Scnitfläce,
				Int64 NaacOklusioonRestFläceGröösteKwadraatSaitelänge)
			{
				this.Okludiirende = Okludiirende;
				this.Okludiirte = Okludiirte;
				this.Scnitfläce = Scnitfläce;
				this.NaacOklusioonRestFläceGröösteKwadraatSaitelänge = NaacOklusioonRestFläceGröösteKwadraatSaitelänge;
			}
		}
		/// <summary>
		/// Berecnet ob in durc <paramref name="GbsBaum"/> bescriibene Baum aine oklusioon von Ast O0 durc Ast O1 mööglic ist.
		/// Dabai werd dii Fläce der Äste nit berüksictigt sondern nur deeren Ordnung.
		/// </summary>
		/// <param name="O0GbsAstHerkunftAdrese"></param>
		/// <param name="O1GbsAstHerkunftAdrese"></param>
		/// <param name="GbsBaum"></param>
		/// <returns></returns>
		static public bool? OklusioonVonO0DurcO1MööglicNaacBaumsctruktuurBerecne(
			Int64 O0GbsAstHerkunftAdrese,
			Int64 O1GbsAstHerkunftAdrese,
			GbsElement GbsBaum,
			out GbsElement O0GbsAst,
			out GbsElement O1GbsAst)
		{
			O0GbsAst = null;
			O1GbsAst = null;

			if (null == GbsBaum)
			{
				return null;
			}

			if (O0GbsAstHerkunftAdrese == O1GbsAstHerkunftAdrese)
			{
				return false;
			}

			{
				//	2015.03.18	Aufgrund desen das di Elemente in GbsBaum anders angeordnet sind gelten di vorherige reegeln nit meer. 

				O0GbsAst = GbsBaum.SuuceFlacMengeGbsAstFrühesteMitIdent(O0GbsAstHerkunftAdrese);
				O1GbsAst = GbsBaum.SuuceFlacMengeGbsAstFrühesteMitIdent(O1GbsAstHerkunftAdrese);

				if (null == O0GbsAst || null == O1GbsAst)
				{
					return null;
				}

				if (O1GbsAst.GbsBaumEnumFlacListeKnoote().Contains(O0GbsAst))
				{
					return false;
				}

				/*
				2015.09.01
				Änderung InGbsBaumAstIndex: Übernaame von noie Sensor InTreeIndex: Element occludes other Elements with lower Value.
			return O1GbsAst.InGbsBaumAstIndex < O0GbsAst.InGbsBaumAstIndex;
			*/
				return O0GbsAst.InGbsBaumAstIndex < O1GbsAst.InGbsBaumAstIndex;
			}
		}

		public SictAufgaabeParamZerleegungErgeebnis AufgaabeBerecneAktueleTailaufgaabe(
			SictAufgaabeZuusctand Aufgaabe,
			SictAufgaabeKombiZuusctand KombiZuusctand)
		{
			var AufgaabeParam = (null == Aufgaabe) ? null : Aufgaabe.AufgaabeParam;

			if (null == AufgaabeParam)
			{
				return default(SictAufgaabeParamZerleegungErgeebnis);
			}

			return AufgaabeParam.Zerleege(this, KombiZuusctand);
		}

		bool AufgaabeHinraicendGlaicwertigFürFortsazFürAufgaabeParamUndBeginZaitHinraicendJung(
			SictAufgaabeZuusctand KandidaatAufgaabe,
			SictAufgaabeParam AufgaabeParam,
			Int64? BeginZaitScrankeMin = null)
		{
			if (null == KandidaatAufgaabe)
			{
				return false;
			}

			if (BeginZaitScrankeMin.HasValue)
			{
				if (!(BeginZaitScrankeMin <= KandidaatAufgaabe.BeginZait))
				{
					return false;
				}
			}

			var AufgaabeErfolgUnterbrecungZait = KandidaatAufgaabe.ErfolgUnterbrecungZait;

			if (AufgaabeErfolgUnterbrecungZait.HasValue)
			{
				return false;
			}

			var KandidaatAufgaabeParam = KandidaatAufgaabe.AufgaabeParam;

			if (!SictAufgaabeParam.HinraicendGlaicwertigFürFortsaz(KandidaatAufgaabeParam, AufgaabeParam))
			{
				return false;
			}

			return true;
		}

		bool AufgaabeErfolgraicUmgeseztFürAufgaabeParam(
			SictAufgaabeZuusctand Aufgaabe,
			SictAufgaabeParam AufgaabeParam)
		{
			if (null == Aufgaabe)
			{
				return false;
			}

			var AufgaabeErfolgZait = Aufgaabe.ErfolgZait;
			var AufgaabeErfolgUnterbrecungZait = Aufgaabe.ErfolgUnterbrecungZait;

			if (!AufgaabeErfolgZait.HasValue)
			{
				return false;
			}

			if (AufgaabeErfolgUnterbrecungZait.HasValue)
			{
				return false;
			}

			if (!AufgaabeHinraicendGlaicwertigFürFortsazFürAufgaabeParamUndBeginZaitHinraicendJung(Aufgaabe, AufgaabeParam))
			{
				return false;
			}

			return true;
		}

		public IEnumerable<SictVorsclaagNaacProcessWirkung> FürNuzerVorsclaagWirkungBerecne()
		{
			var NuzerZaitMili = this.NuzerZaitMili;

			var ScritLezteWirkungMengeAufgaabePfaadZuBlatMitGrupePrioNaame = this.ScritLezteWirkungMengeAufgaabePfaadZuBlatMitGrupePrioNaame;

			var VorsclaagListeWirkung = new List<SictVorsclaagNaacProcessWirkung>();

			if (null != ScritLezteWirkungMengeAufgaabePfaadZuBlatMitGrupePrioNaame)
			{
				foreach (var AufgaabePfaadZuBlatMitGrupePrioNaame in ScritLezteWirkungMengeAufgaabePfaadZuBlatMitGrupePrioNaame)
				{
					var AufgaabeBlat = AufgaabePfaadZuBlatMitGrupePrioNaame.Key.LastOrDefault();

					if (null == AufgaabeBlat)
					{
						continue;
					}

					var AufgaabeBlatParam = AufgaabeBlat.AufgaabeParam;

					if (null == AufgaabeBlatParam)
					{
						continue;
					}

					var NaacNuzerVorsclaagWirkung = AufgaabeBlatParam.NaacNuzerVorsclaagWirkungVirt();

					if (null != NaacNuzerVorsclaagWirkung)
					{
						var WirkungZwekListeKomponente = new List<string>();

						{
							//	Berecnung WirkungZwekListeKomponente

							foreach (var AufgaabePfaadAst in AufgaabePfaadZuBlatMitGrupePrioNaame.Key)
							{
								if (null == AufgaabePfaadAst)
								{
									continue;
								}

								var AufgaabePfaadAstParam = AufgaabePfaadAst.AufgaabeParam;

								if (null == AufgaabePfaadAstParam)
								{
									continue;
								}

								var AufgaabePfaadAstParamZwekListeKomponente = AufgaabePfaadAstParam.ZwekListeKomponenteBerecne();

								if (null != AufgaabePfaadAstParamZwekListeKomponente)
								{
									WirkungZwekListeKomponente.AddRange(AufgaabePfaadAstParamZwekListeKomponente);
								}
							}
						}

						var WirkungZwekListeKomponenteFiltert =
							WirkungZwekListeKomponente.Distinct().ToArray();

						var NaacNuzerVorsclaagWirkungAbbild =
							new SictVorsclaagNaacProcessWirkung(
								AufgaabeBlat.Bezaicner,
								NuzerZaitMili,
								NaacNuzerVorsclaagWirkung.BedingungMengeWirkungErfolgraic,
								NaacNuzerVorsclaagWirkung.VonVonZiilProcessLeeseBisVonZiilProcessLeeseWartezaitMili,
								NaacNuzerVorsclaagWirkung.VonWirkungBisVonZiilProcessLeeseWartezaitMili,
								WirkungZwekListeKomponenteFiltert.ToArray(),
								NaacNuzerVorsclaagWirkung.MengeWirkungKey,
								NaacNuzerVorsclaagWirkung.MausPfaadListeWeegpunktFläce,
								NaacNuzerVorsclaagWirkung.MausPfaadMengeFläceZuMaide,
								NaacNuzerVorsclaagWirkung.MausPfaadTasteLinksAin,
								NaacNuzerVorsclaagWirkung.MausPfaadTasteRectsAin);

						VorsclaagListeWirkung.Add(NaacNuzerVorsclaagWirkungAbbild);
					}
				}
			}

			if (null == ListeZuZaitNaacNuzerVorsclaagNaacProcessWirkung)
			{
				ListeZuZaitNaacNuzerVorsclaagNaacProcessWirkung = new List<SictWertMitZait<SictVorsclaagNaacProcessWirkung>>();
			}

			foreach (var VorsclaagWirkung in VorsclaagListeWirkung)
			{
				Bib3.Glob.InListeOrdnetFüügeAin(
					ListeZuZaitNaacNuzerVorsclaagNaacProcessWirkung,
					(Element) => Element.Zait,
					new SictWertMitZait<Optimat.EveOnline.SictVorsclaagNaacProcessWirkung>(
						NuzerZaitMili, VorsclaagWirkung));
			}

			ListeZuZaitNaacNuzerVorsclaagNaacProcessWirkung.ListeKürzeBegin(10);

			return VorsclaagListeWirkung;
		}

		override public void AufgaabeScrit(
			Int64 Zait,
			SictAufgaabeZuusctand Aufgaabe,
			Bib3.SictIdentInt64Fabrik AufgaabeIdentFabrik,
			SictAufgaabeKombiZuusctand KombiZuusctand)
		{
			if (null == Aufgaabe)
			{
				return;
			}

			var AufgaabeParam = Aufgaabe.AufgaabeParam;

			var AufgaabeBisherMengeKomponente = Aufgaabe.MengeKomponenteBerecne();

			var AufgaabeParamZerleegungErgeebnis = AufgaabeBerecneAktueleTailaufgaabe(Aufgaabe, KombiZuusctand);

			var ReegelungDistanceScpiilraumRest = AufgaabeParamZerleegungErgeebnis.ReegelungDistanceScpiilraumRest;

			var MengeAufgaabeKomponenteParam = AufgaabeParamZerleegungErgeebnis.ListeKomponenteAufgaabeParam;
			var ZerleegungVolsctändig = AufgaabeParamZerleegungErgeebnis.ZerleegungVolsctändig;

			if (ReegelungDistanceScpiilraumRest.HasValue)
			{
				KombiZuusctand.ListeAufgaabeReegelungDistanceFüügeAin(Aufgaabe, ReegelungDistanceScpiilraumRest);
			}

			var MengeAufgaabeKomponenteAktiiv = new List<SictAufgaabeZuusctand>();

			if (Bib3.Extension.NullOderLeer(MengeAufgaabeKomponenteParam))
			{
			}
			else
			{
				foreach (var AufgaabeKomponenteParam in MengeAufgaabeKomponenteParam)
				{
					if (null == AufgaabeKomponenteParam)
					{
						continue;
					}

					var AufgaabeKomponente =
						(null == AufgaabeBisherMengeKomponente) ? null :
						AufgaabeBisherMengeKomponente
						.FirstOrDefault((Kandidaat) => AufgaabeHinraicendGlaicwertigFürFortsazFürAufgaabeParamUndBeginZaitHinraicendJung(
							Kandidaat,
							AufgaabeKomponenteParam,
							Zait - 10000));

					if (MengeAufgaabeKomponenteAktiiv.Contains(AufgaabeKomponente))
					{
						throw new ApplicationException("Vermuutlic Feeler in AufgaabeHinraicendGlaicwertigFürFortsazFürAufgaabeParam");
					}

					if (null == AufgaabeKomponente)
					{
						AufgaabeKomponente = new SictAufgaabeZuusctand(AufgaabeIdentFabrik.IdentBerecne(), AufgaabeKomponenteParam, null, Zait);
					}

					MengeAufgaabeKomponenteAktiiv.Add(AufgaabeKomponente);
				}
			}

			Aufgaabe.ZerleegungErgeebnisLezteZuZaitSeze(new SictWertMitZait<SictAufgaabeScritZerleegungErgeebnis>(
				Zait,
				new SictAufgaabeScritZerleegungErgeebnis(MengeAufgaabeKomponenteAktiiv.ToArray(), ZerleegungVolsctändig)));
		}

		static public SictFläceRectekOrtoAbhängigVonGbsAst FläceRectekOrtoAbhängigVonGbsAstBerecne(
			GbsElement GbsObjekt,
			GbsElement GbsBaum)
		{
			if (null == GbsObjekt)
			{
				return null;
			}

			OrtogoonInt FläceTailSctaatisc = GbsObjekt.InGbsFläce;

			/*
			 * 2015.03.12
			 * 
			var GbsObjektBezaicner = (Int64?)GbsObjekt.Ident;

			InGbsPfaad NaacGbsAstPfaad = null;

			if (GbsObjektBezaicner.HasValue	&&
				null != GbsBaum)
			{
				GbsAstInfo[] PfaadListeAst;

				NaacGbsAstPfaad	=	GbsBaum.GbsPfaadBerecneNaacAstMitHerkunftAdrese(GbsObjektBezaicner.Value, out	PfaadListeAst);

				if (null != NaacGbsAstPfaad	&&
					null	!= PfaadListeAst)
				{
					var GbsObjektGbsAst = Bib3.Extension.LastOrDefaultNullable(PfaadListeAst) as SictGbsAstInfoSictAuswert;

					var GbsObjektGbsAstLaage =
						PfaadListeAst
						.Select((Ast) => Ast.Laage ?? new SictVektor2DSingle(0, 0))
						.Aggregate(new SictVektor2DSingle(0, 0), (a, b) => a + b)
						.AlsVektor2DInt();

					FläceTailSctaatisc =
						FläceTailSctaatisc.Versezt(-GbsObjektGbsAstLaage);
				}
			}

			return new SictFläceRectekOrtoAbhängigVonGbsAst(FläceTailSctaatisc, NaacGbsAstPfaad);
		 * */

			return new SictFläceRectekOrtoAbhängigVonGbsAst(FläceTailSctaatisc, GbsObjekt);
		}

		override public SictGbsAstOklusioonInfo ZuGbsAstHerkunftAdreseKandidaatOklusioonBerecne(
			Int64 GbsAstHerkunftAdrese)
		{
			/*
			 * 2015.03.12
			 * 
			 * Ersaz durc ToCustomBotSnapshot
			 * 
				var GbsBaum = this.VonNuzerMeldungZuusctandTailGbsBaum;
			 * */
			var GbsBaum = this.ListeScnapscusLezteAuswertungErgeebnisNaacSimu;

			var MengeKandidaatOklusioon = MengeKandidaatOklusioonBerecne();

			if (null == MengeKandidaatOklusioon)
			{
				return null;
			}

			foreach (var KandidaatOklusioon in MengeKandidaatOklusioon)
			{
				if (null == KandidaatOklusioon)
				{
					continue;
				}

				var KandidaatOklusioonGbsObjektScnapscus = KandidaatOklusioon.GbsElementScnapscus;

				if (null == KandidaatOklusioonGbsObjektScnapscus)
				{
					continue;
				}

				var KandidaatGbsAstBezaicner = (Int64?)KandidaatOklusioonGbsObjektScnapscus.Ident;

				if (!KandidaatGbsAstBezaicner.HasValue)
				{
					continue;
				}

				if (GbsAstHerkunftAdrese == KandidaatGbsAstBezaicner)
				{
					return KandidaatOklusioon;
				}

				if (null == GbsBaum)
				{
					continue;
				}

				var KandidaatGbsAst = GbsBaum.SuuceFlacMengeGbsAstFrühesteMitIdent(KandidaatGbsAstBezaicner.Value);

				if (null == KandidaatGbsAst)
				{
					continue;
				}

				if (null == KandidaatGbsAst.SuuceFlacMengeGbsAstFrühesteMitIdent(GbsAstHerkunftAdrese))
				{
					//	Gbs Ast mit GbsAstHerkunftAdrese isc nit in KandidaatOklusioon enthalte
					continue;
				}

				return KandidaatOklusioon;
			}

			return null;
		}

		override public IEnumerable<SictGbsAstOklusioonInfo> MengeKandidaatOklusioonBerecne()
		{
			var Gbs = this.Gbs;
			var GbsListeMenu = (null == Gbs) ? null : Gbs.ListeMenuNocOfeBerecne();
			var GbsMengeWindow = (null == Gbs) ? null : Gbs.MengeWindow;

			var MengeKandidaatOklusioon = new List<SictGbsAstOklusioonInfo>();

			var AusScnapscusAuswertungZuusctand = this.ListeScnapscusLezteAuswertungErgeebnisNaacSimu;

			var ScnapscusGbsMengeAbovemainPanelEveMenu =
				(null == AusScnapscusAuswertungZuusctand) ? null : AusScnapscusAuswertungZuusctand.MengeAbovemainPanelEveMenu;

			var ScnapscusUtilmenuMission =
				(null == AusScnapscusAuswertungZuusctand) ? null : AusScnapscusAuswertungZuusctand.UtilmenuMission;

			if (null != GbsListeMenu)
			{
				MengeKandidaatOklusioon.AddRange(GbsListeMenu.Select((Menu) => new SictGbsAstOklusioonInfo(Menu)));
			}

			if (null != GbsMengeWindow)
			{
				MengeKandidaatOklusioon.AddRange(GbsMengeWindow.Select((Window) => new SictGbsAstOklusioonInfo(Window)));
			}

			if (null != ScnapscusGbsMengeAbovemainPanelEveMenu)
			{
				MengeKandidaatOklusioon.AddRange(ScnapscusGbsMengeAbovemainPanelEveMenu.Select((PanelEveMenu) => new SictGbsAstOklusioonInfo(PanelEveMenu)));
			}

			if (null != ScnapscusUtilmenuMission)
			{
				MengeKandidaatOklusioon.Add(new SictGbsAstOklusioonInfo(ScnapscusUtilmenuMission));
			}

			return MengeKandidaatOklusioon;
		}

		override public IEnumerable<SictAufgaabeParam> GbsAstOklusioonVermaidungBerecne(
			SictAufgaabeParamGbsAstOklusioonVermaidung OklusioonVermaidungParam)
		{
			IEnumerable<SictFläceRectekOrtoAbhängigVonGbsAst> MengeFläceZuMaide;
			IEnumerable<OrtogoonInt> NaacOklusioonRestMengeFläce;

			return GbsAstOklusioonVermaidungBerecne(
				OklusioonVermaidungParam,
				out MengeFläceZuMaide,
				out NaacOklusioonRestMengeFläce);
		}

		override public IEnumerable<SictAufgaabeParam> GbsAstOklusioonVermaidungBerecne(
			SictAufgaabeParamGbsAstOklusioonVermaidung OklusioonVermaidungParam,
			out IEnumerable<SictFläceRectekOrtoAbhängigVonGbsAst> MengeFläceZuMaide)
		{
			IEnumerable<OrtogoonInt> NaacOklusioonRestMengeFläce;

			return GbsAstOklusioonVermaidungBerecne(
				OklusioonVermaidungParam,
				out MengeFläceZuMaide,
				out NaacOklusioonRestMengeFläce);
		}

		override public IEnumerable<SictAufgaabeParam> GbsAstOklusioonVermaidungBerecne(
			SictAufgaabeParamGbsAstOklusioonVermaidung OklusioonVermaidungParam,
			out IEnumerable<SictFläceRectekOrtoAbhängigVonGbsAst> MengeFläceZuMaide,
			out IEnumerable<OrtogoonInt> NaacOklusioonRestMengeFläce)
		{
			/*
			 * 2015.03.12
			 * 
			 * Ersaz durc ToCustomBotSnapshot
			 * 
				var GbsBaum = this.VonNuzerMeldungZuusctandTailGbsBaum;
			 * */

			var GbsBaum = this.ListeScnapscusLezteAuswertungErgeebnisNaacSimu;

			var MengeKandidaatOklusioon = MengeKandidaatOklusioonBerecne();

			var MengeKandidaatOklusioonZuErhalte = new List<SictGbsAstOklusioonInfo>();

			var MengeWindowZuErhalte = this.MengeWindowZuErhalte;

			if (null != MengeWindowZuErhalte)
			{
				foreach (var WindowZuErhalte in MengeWindowZuErhalte)
				{
					if (null == WindowZuErhalte)
					{
						continue;
					}

					var WindowZuErhalteHerkunftAdrese = WindowZuErhalte.GbsAstHerkunftAdrese;

					if (!WindowZuErhalteHerkunftAdrese.HasValue)
					{
						continue;
					}

					var WindowZuErhalteKandidaatOklusioon =
						Bib3.Extension.FirstOrDefaultNullable(MengeKandidaatOklusioon,
						(Kandidaat) => Kandidaat.GbsElementIdent == WindowZuErhalteHerkunftAdrese);

					if (null != WindowZuErhalteKandidaatOklusioon)
					{
						MengeKandidaatOklusioonZuErhalte.Add(WindowZuErhalteKandidaatOklusioon);
					}
				}
			}

			return GbsAstOklusioonVermaidungBerecne(
				OklusioonVermaidungParam,
				MengeKandidaatOklusioon,
				MengeKandidaatOklusioonZuErhalte,
				GbsBaum,
				out MengeFläceZuMaide,
				out NaacOklusioonRestMengeFläce);
		}

		static public IEnumerable<SictAufgaabeParam> GbsAstOklusioonVermaidungBerecne(
			SictAufgaabeParamGbsAstOklusioonVermaidung OklusioonVermaidungParam,
			IEnumerable<SictGbsAstOklusioonInfo> MengeKandidaatOklusioon,
			IEnumerable<SictGbsAstOklusioonInfo> MengeKandidaatOklusioonZuErhalte,
			GbsElement GbsBaum,
			out IEnumerable<SictFläceRectekOrtoAbhängigVonGbsAst> MengeFläceZuMaide,
			out IEnumerable<OrtogoonInt> NaacOklusioonRestMengeFläce)
		{
			MengeFläceZuMaide = null;

			NaacOklusioonRestMengeFläce = null;

			if (null == OklusioonVermaidungParam)
			{
				return null;
			}

			var OklusioonVermaidungParamGbsElement = OklusioonVermaidungParam.GbsAst;

			if (null == OklusioonVermaidungParamGbsElement)
			{
				return null;
			}

			var OklusioonVermaidungParamGbsAstFläce = OklusioonVermaidungParamGbsElement.InGbsFläce;

			if (null == OklusioonVermaidungParamGbsAstFläce)
			{
				return null;
			}

			GbsElement OklusioonVermaidungParamGbsAst = null;

			NaacOklusioonRestMengeFläce = new OrtogoonInt[] { OklusioonVermaidungParamGbsAstFläce };

			if (null == GbsBaum)
			{
				return null;
			}

			if (null == MengeKandidaatOklusioon)
			{
				return null;
			}

			var OklusioonVermaidungParamGbsAstHerkunftAdrese = (Int64?)OklusioonVermaidungParamGbsElement.Ident;

			var InternMengeFläceZuMaide = new List<SictFläceRectekOrtoAbhängigVonGbsAst>();

			MengeFläceZuMaide = InternMengeFläceZuMaide;

			var ListeTailaufgaabe = new List<SictAufgaabeParam>();

			var WeegpunktGbsObjektFläceKwadraatSaitelänge = Math.Min(OklusioonVermaidungParamGbsAstFläce.Grööse.A, OklusioonVermaidungParamGbsAstFläce.Grööse.B);

			var ListeTailwaiseOklusioon = new List<SictGbsAstOklusioonKombi>();

			OrtogoonInt[] InsgesamtRestMengeTailfläce = new OrtogoonInt[] { OklusioonVermaidungParamGbsAstFläce };

			foreach (var KandidaatOklusioon in MengeKandidaatOklusioon)
			{
				if (null == KandidaatOklusioon)
				{
					continue;
				}

				var KandidaatOklusioonGbsObjekt = KandidaatOklusioon.GbsElementScnapscus;

				if (null == KandidaatOklusioonGbsObjekt)
				{
					continue;
				}

				var KandidaatOklusioonFläce = KandidaatOklusioonGbsObjekt.InGbsFläce;

				GbsElement KandidaatOklusioonGbsAst = null;

				var OklusioonVonO0DurcO1MööglicNaacBaumsctruktuur =
					OklusioonVermaidungParamGbsAstHerkunftAdrese.HasValue ?
					OklusioonVonO0DurcO1MööglicNaacBaumsctruktuurBerecne(
					OklusioonVermaidungParamGbsAstHerkunftAdrese.Value,
					KandidaatOklusioonGbsObjekt.Ident,
					GbsBaum,
					out OklusioonVermaidungParamGbsAst,
					out KandidaatOklusioonGbsAst) :
					null;

				if (false == OklusioonVonO0DurcO1MööglicNaacBaumsctruktuur)
				{
					continue;
				}

				var KandidaatOklusioonFläceZuMaide = FläceRectekOrtoAbhängigVonGbsAstBerecne(KandidaatOklusioonGbsObjekt, GbsBaum);

				InternMengeFläceZuMaide.Add(KandidaatOklusioonFläceZuMaide);

				var WeegpunktFläceMiinusKandidaatOklusioonFläceMengeTailfläce =
					Optimat.EveOnline.Extension.FläceMiinusFläce(OklusioonVermaidungParamGbsAstFläce, KandidaatOklusioonFläce);

				InsgesamtRestMengeTailfläce =
					Bib3.Glob.ArrayAusListeFeldGeflact(InsgesamtRestMengeTailfläce.Select((Fläce) => Optimat.EveOnline.Extension.FläceMiinusFläce(Fläce, KandidaatOklusioonFläce)));

				if (null != WeegpunktFläceMiinusKandidaatOklusioonFläceMengeTailfläce)
				{
					var RestfläceGröösteKwadraatSaitelänge =
						AusMengeFläceBerecneGröösteKwadraatSaitelängeBerecne(WeegpunktFläceMiinusKandidaatOklusioonFläceMengeTailfläce);

					/*
					 * 2014.04.29
					 * Korektur: ListeTailwaiseOkludiirendeUndRestKwadraatSaitelänge mus befült werde unabhängig davon ob das restlice grööste Kwadraat klainer geworde isc,
					 * sunsct scteet waiter unte kain Kandidaat zum entferne zur Verfüügung.
					 * 
					if (!(WeegpunktGbsObjektFläceKwadraatSaitelänge - 0.1f < RestfläceGröösteKwadraatSaitelänge))
					 * */
					var Scnitfläce = OrtogoonInt.Scnitfläce(KandidaatOklusioonFläce, OklusioonVermaidungParamGbsAstFläce);

					if (!Scnitfläce.IsLeer)
					{
						//	grööste Kwadraat in übrige Tailfäce isc klainer, zumindest tailwaise Okludiirt.

						ListeTailwaiseOklusioon.Add(
							new SictGbsAstOklusioonKombi(
								KandidaatOklusioon,
								OklusioonVermaidungParamGbsElement,
								Scnitfläce,
								RestfläceGröösteKwadraatSaitelänge));
					}
				}
			}

			NaacOklusioonRestMengeFläce = InsgesamtRestMengeTailfläce;

			var InsgesamtRestMengeTailfläceGröösteKwadraatSaitelänge =
				AusMengeFläceBerecneGröösteKwadraatSaitelängeBerecne(InsgesamtRestMengeTailfläce);

			if (!(OklusioonVermaidungParam.RestFläceKwadraatSaitenlängeScrankeMin <= InsgesamtRestMengeTailfläceGröösteKwadraatSaitelänge) &&
				0 < ListeTailwaiseOklusioon.Count)
			{
				//	übrige Tailfläce nit hinraicend groos, Oklusioon erfordert Reaktioon

				//	Versuuce Oklusioon zu beende

				var ListeTailwaiseOkludiirendeOrdnet =
					ListeTailwaiseOklusioon
					//	Window früüher scliise als andere (z.B. Menu oder Utilmenu oder Neocom) da andere oftmaals impliziit gesclose werden.
					.OrderBy((Kandidaat) => null == Kandidaat?.Okludiirende?.Window ? 0 : 1)
					.ThenBy((Kandidaat) => Kandidaat.NaacOklusioonRestFläceGröösteKwadraatSaitelänge)
					/*
					2015.09.01
					Änderung InGbsBaumAstIndex: Übernaame von noie Sensor InTreeIndex: Element occludes other Elements with lower Value.
					.ThenBy((Kandidaat) => Kandidaat?.Okludiirende?.GbsAstInBaumIndex)
					*/
					.ThenByDescending((Kandidaat) => Kandidaat?.Okludiirende?.GbsAstInBaumIndex)
					.ToArray();

				foreach (var TailwaiseOkludiirende in ListeTailwaiseOkludiirendeOrdnet)
				{
					if (0 < ListeTailaufgaabe.Count)
					{
						//	!!!!	Temp für Performanz und üübersictlickait Berict:	nur aine Tailaufgaabe berecne
						break;
					}

					if (null == TailwaiseOkludiirende)
					{
						continue;
					}

					var OkludiirendeZuErhalte =
						(null == MengeKandidaatOklusioonZuErhalte) ? false :
						MengeKandidaatOklusioonZuErhalte
						.Any((KandidaatOklusioonZuErhalte) => KandidaatOklusioonZuErhalte.GbsElementIdent ==
						TailwaiseOkludiirende.Okludiirende.GbsElementIdent);

					if (OkludiirendeZuErhalte)
					{
						//	Okludiirende zu erhalte werde. z.B. Window Drones.
						//	Versuuce Oklusioon zu beende oone Okludiirende zu Verberge: Z-Index von okludiirte waiter naac vorne bringe

						SictGbsAstOklusioonInfo Okludiirte = null;

						foreach (var KandidaatOkludiirte in MengeKandidaatOklusioon)
						{
							if (null == KandidaatOkludiirte)
							{
								continue;
							}

							var KandidaatOkludiirteGbsAstHerkunftAdrese = KandidaatOkludiirte.GbsElementIdent;

							if (!KandidaatOkludiirteGbsAstHerkunftAdrese.HasValue)
							{
								continue;
							}

							if (KandidaatOkludiirteGbsAstHerkunftAdrese == OklusioonVermaidungParamGbsAstHerkunftAdrese)
							{
								Okludiirte = KandidaatOkludiirte;
								break;
							}

							if (OklusioonVermaidungParamGbsAstHerkunftAdrese.HasValue)
							{
								var KandidaatOkludiirteGbsAst = GbsBaum.SuuceFlacMengeGbsAstFrühesteMitIdent(KandidaatOkludiirteGbsAstHerkunftAdrese.Value);

								if (null == KandidaatOkludiirteGbsAst)
								{
									continue;
								}

								if (null != KandidaatOkludiirteGbsAst.SuuceFlacMengeGbsAstFrühesteMitIdent(OklusioonVermaidungParamGbsAstHerkunftAdrese.Value))
								{
									Okludiirte = KandidaatOkludiirte;
									break;
								}
							}
						}

						if (null != Okludiirte)
						{
							var OkludiirteWindow = Okludiirte.Window;

							if (null != OkludiirteWindow)
							{
								ListeTailaufgaabe.Add(AufgaabeParamAndere.KonstruktWindowHooleNaacVorne(OkludiirteWindow));
							}
						}
					}
					else
					{
						var TailwaiseOkludiirendeGbsObjektScnapscus = TailwaiseOkludiirende?.Okludiirende?.GbsElementScnapscus;

						//	ListeTailaufgaabe.Add(AufgaabeParamAndere.KonstruktGbsAstVerberge(TailwaiseOkludiirendeGbsObjektScnapscus));
						ListeTailaufgaabe.Add(new AufgaabeParamGbsElementVerberge(
							TailwaiseOkludiirendeGbsObjektScnapscus,
							TailwaiseOkludiirende));
					}
				}
			}

			return ListeTailaufgaabe;
		}

		override public SictAufgaabeZuusctand
			ManööverAusgefüürtLezteAufgaabeBerecne(
			SictOverViewObjektZuusctand OverviewObjekt,
			SictTargetZuusctand Target,
			SictZuInRaumObjektManööverTypEnum ManööverTyp,
			Int64? DistanceScrankeMin = null,
			Int64? DistanceScrankeMax = null,
			bool BedingungNocAktiiv = false)
		{
			var ManööverAusgefüürtLezteAufgaabe = ManööverAusgefüürtLezteAufgaabeBerecne();

			if (null == ManööverAusgefüürtLezteAufgaabe)
			{
				return null;
			}

			var ManööverAusgefüürtLezteAufgaabeParam = ManööverAusgefüürtLezteAufgaabe.AufgaabeParam;

			if (null == ManööverAusgefüürtLezteAufgaabeParam)
			{
				return null;
			}

			var ManööverAusgefüürtLezteShipUIIndicationMitZait = ManööverAusgefüürtLezteAufgaabe.ManööverErgeebnis;

			if (null == ManööverAusgefüürtLezteShipUIIndicationMitZait)
			{
				return null;
			}

			var ManööverAusgefüürtLezteShipUIIndication = ManööverAusgefüürtLezteShipUIIndicationMitZait.Wert;

			if (null == ManööverAusgefüürtLezteShipUIIndication)
			{
				return null;
			}

			if (BedingungNocAktiiv)
			{
				if (ManööverAusgefüürtLezteShipUIIndicationMitZait.EndeZait.HasValue)
				{
					return null;
				}
			}

			var OverviewUndTarget = this.OverviewUndTarget;

			//	!!!!	zu ergänze: Verbindung zwisce OverviewObjekt und Target hersctele mit Info aus OverviewUndTarget

			if (!(OverviewObjekt == ManööverAusgefüürtLezteAufgaabeParam.OverViewObjektZuBearbaiteVirt() &&
				Target == ManööverAusgefüürtLezteAufgaabeParam.TargetZuBearbaiteVirt()))
			{
				return null;
			}

			{
				//	Sicersctele das Manööver mit pasende Distance durcgefüürt werd.

				if (SictZuInRaumObjektManööverTypEnum.Approach == ManööverAusgefüürtLezteShipUIIndication.ManöverTyp)
				{
					//	Für Approach werd Distance Scranke nit berüksictigt.
				}
				else
				{
					var ManööverAusgefüürtLezteShipUIIndicationDistance = ManööverAusgefüürtLezteShipUIIndication.Distance;

					if (!ManööverAusgefüürtLezteShipUIIndicationDistance.HasValue)
					{
						//	Kaine Distance aus ShipUI erkenbar.

						return null;
					}

					if (!(ManööverAusgefüürtLezteShipUIIndicationDistance <= DistanceScrankeMax &&
						DistanceScrankeMin <= ManööverAusgefüürtLezteShipUIIndicationDistance))
					{
						//	Manööver hat kaine pasende Distance

						//	!!!!	Anzupase: verglaic mit taatsäclic verfüügbaare Distance: Info in Aufgaabe Zuusctand scpaicere (z.B. verfüügbaare MenuEntry für Menu Orbit)

						return null;
					}
				}
			}

			return ManööverAusgefüürtLezteAufgaabe;
		}

		override public SictAufgaabeZuusctand ManööverAusgefüürtLezteAufgaabeBerecne()
		{
			var MengeAufgaabeZuusctand = this.MengeAufgaabeZuusctand;

			if (null == MengeAufgaabeZuusctand)
			{
				return null;
			}

			var ListeAufgaabeZuusctand =
				MengeAufgaabeZuusctand
				.OrderByDescending((AufgaabeZuusctand) => AufgaabeZuusctand.AbsclusTailWirkungZait ?? -1);

			foreach (var AufgaabeZuusctand in ListeAufgaabeZuusctand)
			{
				if (null == AufgaabeZuusctand)
				{
					continue;
				}

				var AufgaabeParam = AufgaabeZuusctand.AufgaabeParam as AufgaabeParamAndere;
				var AufgaabeManööverErgeebnis = AufgaabeZuusctand.ManööverErgeebnis;

				if (null == AufgaabeParam)
				{
					continue;
				}

				var AufgaabeManööverAuszufüüreTyp = AufgaabeParam.ManööverAuszufüüreTyp;

				if (!AufgaabeManööverAuszufüüreTyp.HasValue)
				{
					continue;
				}

				if (null == AufgaabeManööverErgeebnis)
				{
					return null;
				}

				return AufgaabeZuusctand;
			}

			return null;
		}
	}
}
