using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bib3;
using Newtonsoft.Json;
using Optimat.EveOnline.AuswertGbs;
using ExtractFromOldAssembly.Bib3;

namespace Optimat.EveOnline.Anwendung
{
	[JsonObject(MemberSerialization.OptIn)]
	public class SictOverviewPresetFolgeViewport
	{
		[JsonProperty]
		public string OverviewPresetName
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? BeginZait
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? BeginScrollHandleFläceGrenzeOobnAntailAnGesamtMili
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? EndeScrollHandleFläceGrenzeUntnAntailAnGesamtMili
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? BeginObjektDistance
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? EndeObjektDistance
		{
			private set;
			get;
		}

		[JsonProperty]
		public Int64? EndeZait
		{
			private set;
			get;
		}

		/// <summary>
		/// Folge dekt den Beraic inerhalb 1e+6 Meeter ab.
		/// </summary>
		[JsonProperty]
		public bool? VolsctändigTailInGrid
		{
			private set;
			get;
		}

		public bool Fertig
		{
			get
			{
				return EndeZait.HasValue;
			}
		}

		/// <summary>
		/// WindowOverview welces im vorherigen Berecnungsscrit ainging.
		/// </summary>
		[JsonProperty]
		public SictWertMitZait<VonSensor.WindowOverView> WindowOverviewVorherigMitZait
		{
			private set;
			get;
		}

		public SictOverviewPresetFolgeViewport()
		{
		}

		public SictOverviewPresetFolgeViewport(SictWertMitZait<VonSensor.WindowOverView> WindowOverviewMitZait)
		{
			AingangNääxte(WindowOverviewMitZait);
		}

		/// <summary>
		/// Unterstüzt den Aufbau ainer Folge nuur durc Scrole naac unte.
		/// </summary>
		/// <param name="WindowOverviewNoiMitZait"></param>
		public void AingangNääxte(SictWertMitZait<VonSensor.WindowOverView> WindowOverviewNoiMitZait)
		{
			var WindowOverviewNoi = WindowOverviewNoiMitZait.Wert;

			if (null == WindowOverviewNoi)
			{
				return;
			}

			if (true == Fertig)
			{
				return;
			}

			var ScnapscusZaitDistanzScrankeMax = 5000;

			var FolgeDauerScrankeMax = 30000;

			var WindowOverviewNoiAusTabListeZaile = WindowOverviewNoi.AusTabListeZaileOrdnetNaacLaage;
			var WindowOverviewNoiTypeSelectionName = WindowOverviewNoi.OverviewPresetIdent;
			var WindowOverviewNoiScroll = WindowOverviewNoi.Scroll;

			if (null == WindowOverviewNoiTypeSelectionName ||
				null == WindowOverviewNoiScroll)
			{
				goto FolgeBeEnde;
			}

			if (null == WindowOverviewNoiAusTabListeZaile &&
				!(true == WindowOverviewNoi.MeldungMengeZaileLeer))
			{
				goto FolgeBeEnde;
			}

			var WindowOverviewNoiAusTabListeZaileFiltert =
				ExtractFromOldAssembly.Bib3.Extension.WhereNullable(
				WindowOverviewNoiAusTabListeZaile,
				(Kandidaat) => Kandidaat.DistanceMax.HasValue)
				.ToArrayNullable();

			var WindowOverviewNoiAusTabListeZaileFiltertFrüheste =
				ExtractFromOldAssembly.Bib3.Extension.FirstOrDefaultNullable(WindowOverviewNoiAusTabListeZaileFiltert);

			var EndeObjekt =
				ExtractFromOldAssembly.Bib3.Extension.LastOrDefaultNullable(
				WindowOverviewNoiAusTabListeZaileFiltert,
				(Zaile) => Zaile.DistanceMin.HasValue);

			try
			{
				if (!BeginZait.HasValue)
				{
					BeginZait = WindowOverviewNoiMitZait.Zait;
					BeginScrollHandleFläceGrenzeOobnAntailAnGesamtMili = WindowOverviewNoiScroll.ScrollHandleFläceGrenzeOobnAntailAnGesamtMili;

					if (null != WindowOverviewNoiAusTabListeZaileFiltertFrüheste)
					{
						BeginObjektDistance = WindowOverviewNoiAusTabListeZaileFiltertFrüheste.DistanceMax;
					}

					OverviewPresetName = WindowOverviewNoiTypeSelectionName;
					return;
				}

				var FolgeDauer = WindowOverviewNoiMitZait.Zait - BeginZait;

				if (FolgeDauerScrankeMax < FolgeDauer)
				{
					goto FolgeBeEnde;
				}

				var WindowOverviewVorherigMitZait = this.WindowOverviewVorherigMitZait;

				var WindowOverviewVorherig = WindowOverviewVorherigMitZait.Wert;

				if (null == WindowOverviewVorherig)
				{
					goto FolgeBeEnde;
				}

				if (WindowOverviewNoiMitZait.Zait <= WindowOverviewVorherigMitZait.Zait)
				{
					//	Noier Scnapscus hat Zaitpunkt vor vorherigem Scnapscus.
					goto FolgeBeEnde;
				}

				if (ScnapscusZaitDistanzScrankeMax < WindowOverviewNoiMitZait.Zait - WindowOverviewVorherigMitZait.Zait)
				{
					//	Zaitraum zwisce noier Scnapscus und vorheriger Scnapscus isc zu lang.
					goto FolgeBeEnde;
				}

				var WindowOverviewVorherigScroll = WindowOverviewVorherig.Scroll;

				if (null == WindowOverviewVorherigScroll)
				{
					goto FolgeBeEnde;
				}

				if (!(string.Equals(OverviewPresetName, WindowOverviewNoiTypeSelectionName)))
				{
					goto FolgeBeEnde;
				}

				var WindowOverviewNoiScrollHandleFläce = WindowOverviewNoiScroll.ScrollHandleFläce;
				var WindowOverviewVorherigScrollHandleFläce = WindowOverviewVorherigScroll.ScrollHandleFläce;
				var WindowOverviewVorherigMeldungMengeZaileLeer = WindowOverviewVorherig.MeldungMengeZaileLeer;

				if ((null == WindowOverviewNoiScrollHandleFläce &&
					null == WindowOverviewVorherigScrollHandleFläce) ||
					(true == WindowOverviewVorherigMeldungMengeZaileLeer &&
					true == WindowOverviewNoi.MeldungMengeZaileLeer))
				{
					//	In diisem Fal kan davon ausgegange werde das das Scrole nit mööglic da ale Zaile in deen Viewport pase.

					//	Es werden Viewport aus mindestes Scnapscus benöötigt. Daher BeginZait = WindowOverviewVorherigMitZait.Zait;
					BeginZait = WindowOverviewVorherigMitZait.Zait;
					BeginScrollHandleFläceGrenzeOobnAntailAnGesamtMili = 0;
					VolsctändigTailInGrid = true;
					goto FolgeBeEnde;
				}

				if (!(true == WindowOverviewNoiMitZait.Wert.ZaileSindSortiirtNaacDistanceAufsctaigend))
				{
					goto FolgeBeEnde;
				}

				if (null == WindowOverviewNoiAusTabListeZaileFiltert)
				{
					goto FolgeBeEnde;
				}

				if (WindowOverviewNoiAusTabListeZaileFiltert.Length < 4)
				{
					//	Für hinraicende Überlapung müse mindestens viir Zaile sictbar sain.
					goto FolgeBeEnde;
				}

				//	Überlapung mit vorherigem Viewport berecne.

				var WindowOverviewVorherigScrollClipperFläce = WindowOverviewVorherigScroll.ClipperFläce;
				var WindowOverviewNoiScrollClipperFläce = WindowOverviewNoiScroll.ClipperFläce;

				if (null == WindowOverviewVorherigScrollClipperFläce ||
					null == WindowOverviewNoiScrollClipperFläce)
				{
					goto FolgeBeEnde;
				}

				var WindowOverviewVorherigInhaltGrööse = WindowOverviewVorherigScrollClipperFläce.Value.Grööse.B * 1000 / WindowOverviewVorherigScroll.ScrollHandleAntailAnGesamtMili;
				var WindowOverviewNoiInhaltGrööse = WindowOverviewNoiScrollClipperFläce.Value.Grööse.B * 1000 / WindowOverviewVorherigScroll.ScrollHandleAntailAnGesamtMili;

				var WindowOverviewVorherigViewportGrenzeUnte =
					WindowOverviewVorherigScroll.ScrollHandleFläceGrenzeUntnAntailAnGesamtMili * WindowOverviewVorherigInhaltGrööse / 1000;

				var WindowOverviewNoiViewportGrenzeOobe =
					WindowOverviewNoiScroll.ScrollHandleFläceGrenzeOobnAntailAnGesamtMili * WindowOverviewNoiInhaltGrööse / 1000;

				var ÜberlappungInBildpunkte = WindowOverviewVorherigViewportGrenzeUnte - WindowOverviewNoiViewportGrenzeOobe;

				if (!(88 < ÜberlappungInBildpunkte))
				{
					//	Unzuraicende Überlapung zwisce Viewport
					goto FolgeBeEnde;
				}

				EndeObjektDistance = (null == EndeObjekt) ? null : EndeObjekt.DistanceMin;

				EndeScrollHandleFläceGrenzeUntnAntailAnGesamtMili = WindowOverviewNoiScroll.ScrollHandleFläceGrenzeUntnAntailAnGesamtMili;

				var FolgeBeginHinraicendNaheAnOberkanteFürAnnaameVolsctändig =
					BeginScrollHandleFläceGrenzeOobnAntailAnGesamtMili < 3;

				if (1e+6 < EndeObjektDistance &&
					FolgeBeginHinraicendNaheAnOberkanteFürAnnaameVolsctändig)
				{
					VolsctändigTailInGrid = true;
				}

				if (998 < WindowOverviewNoiScroll.ScrollHandleFläceGrenzeUntnAntailAnGesamtMili)
				{
					//	Wail der Automaat den Punkt zum Scrole noc nit genau trift werd hiir am Rand vorersct auf e paar tausendstel verzictet.
					EndeZait = WindowOverviewNoiMitZait.Zait;

					if (FolgeBeginHinraicendNaheAnOberkanteFürAnnaameVolsctändig)
					{
						VolsctändigTailInGrid = true;
						return;
					}
				}

				if (!(WindowOverviewVorherigScroll.ScrollHandleFläceGrenzeOobnAntailAnGesamtMili <
					WindowOverviewNoiScroll.ScrollHandleFläceGrenzeOobnAntailAnGesamtMili))
				{
					//	Oberkante des noie Viewport liigt nit tiifer als di Ooberkante des vorherige Viewport.
					goto FolgeBeEnde;
				}

				return;
			}
			finally
			{
				this.WindowOverviewVorherigMitZait = WindowOverviewNoiMitZait;
			}

		FolgeBeEnde:
			EndeZait = WindowOverviewNoiMitZait.Zait;
			return;
		}
	}

}
