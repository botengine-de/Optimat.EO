using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Optimat.EveOnline.Berict.Auswert;
using Optimat.GBS;
using Bib3;
using Bib3.FCL.GBS;

namespace Optimat.EveOnline.GBS
{
	/// <summary>
	/// Interaction logic for SictBerictInspekt.xaml
	/// </summary>
	public partial class SictBerictInspekt : UserControl
	{
		public IBerictInspektDaatenKwele Auswert;
		public IBerictInspektDaatenKwele ReprAuswertLezte;

		public string BerictWindowClientRasterVerzaicnisPfaad;

		string BerictWindowClientRasterVerzaicnisPfaadLezte;

		readonly SictScatenscpaicerDict<Int64, SictZuScnapscusRasterInfo> DictVonNuzerZaitMiliWindowClientRaster =
			new SictScatenscpaicerDict<Int64, SictZuScnapscusRasterInfo>();

		SictZuScnapscusRasterInfo AngezaigtScnapscusWindowClientRaster;

		readonly SictScatenscpaicerDict<Int64, JToken> DictZuAnwendungZaitMiliScnapcusAutomaatZuusctandSictJson = new SictScatenscpaicerDict<Int64, JToken>();

		readonly Optimat.EveOnline.GBS.SictMissionReprInDataGridParam MissionReprParam = new Optimat.EveOnline.GBS.SictMissionReprInDataGridParam(1000);

		readonly List<IBerictInspektErwaiterung> ListeErwaiterungAuswaalZaitpunkt = new List<IBerictInspektErwaiterung>();

		readonly List<SictBerictInspektErwaiterungRepr> ListeErwaiterungAuswaalZaitpunktRepr = new List<SictBerictInspektErwaiterungRepr>();

		byte[] ScnapcusWindowClientRasterAbbildLezteSictSeriel = null;

		public Brush OptimatScritZaitaintailungVonProcessLeeseBrush = new SolidColorBrush(Colors.CornflowerBlue);
		public Brush OptimatScritZaitaintailungVonProcessLeeseBisNaacProcessWirkungBrush = new SolidColorBrush(Colors.Gray);
		public Brush OptimatScritZaitaintailungNaacProcessWirkungBrush = new SolidColorBrush(Colors.Orange);
		public Brush OptimatScritZaitaintailungVonNaacProcessWirkungBisVonProcessLeeseBrush = new SolidColorBrush(Colors.OliveDrab);

		IEnumerable<IBerictInspektErwaiterung> ListeErwaiterung
		{
			get
			{
				return ListeErwaiterungAuswaalZaitpunkt;
			}
		}

		public void ListeErwaiterungAuswaalZaitpunktSeze(
			IEnumerable<IBerictInspektErwaiterung> ListeErwaiterungAuswaalZaitpunkt)
		{
			Bib3.Glob.PropagiireListeRepräsentatioonMitReprUndIdentPerClrReferenz(
				ListeErwaiterungAuswaalZaitpunkt,
				this.ListeErwaiterungAuswaalZaitpunkt);
		}

		public SictBerictInspekt()
		{
			InitializeComponent();

			DataGridMengeMission.DataGridMengeMissionHeaderLayoutUpdate();

			this.AddHandler(Optimat.GBS.SictInRaum1DAuswaalRegioonUndPunkt.AuswaalRegioonGeändertEvent, new RoutedEventHandler(AuswaalRegioonGeändertEventHandler));
			this.AddHandler(Optimat.GBS.SictInRaum1DAuswaalRegioonUndPunkt.AuswaalPunktGeändertEvent, new RoutedEventHandler(AuswaalPunktGeändertEventHandler));

			this.AddHandler(SictDataGridMengeMission.AuswaalZaitraumEvent, new RoutedEventHandler(AuswaalZaitraumEventHandler));

			ZaitlaisteAuswaalGlobaal.AuswaalSctoierelementRicte();

			ZaitlaisteAuswaalGlobaal.ListeKomponenteFüügeAin(new SictAnzaigeKomponente(null, "select timespan", true, true));
			ZaitlaisteAuswaalGlobaal.ListeKomponenteFüügeAin(new SictAnzaigeKomponente(null, "select point in time", true, false, true, true));

			ZaitlaisteAuswaalAusGlobaalAuswaal.ListeKomponenteFüügeAin(new SictAnzaigeKomponente(null, "select point in time", true, false, true, true));

			ScnapcusWindowClientRasterSictfenster.TransformZuRepräsentiirendesGeändert += ScnapcusWindowClientRasterSictfenster_TransformZuRepräsentiirendesGeändert;
		}

		void ScnapcusWindowClientRasterSictfenster_TransformZuRepräsentiirendesGeändert(object sender, RoutedEventArgs e)
		{
			try
			{
				ScnapcusSict2DSictfensterImageLayoutAktualisiire();
			}
			catch (System.Exception Exception)
			{
				Bib3.FCL.GBS.Extension.MessageBoxException(Exception);
			}
		}

		Panel ScnapcusWindowClientRasterSictfensterPanel
		{
			get
			{
				if (!IsInitialized)
				{
					return null;
				}

				return ScnapcusWindowClientRasterSictfenster.ZuPräsentiirende as Panel;
			}
		}

		Image ScnapcusWindowClientRasterImage
		{
			get
			{
				var ScnapcusSict2DSictfensterPanel = this.ScnapcusWindowClientRasterSictfensterPanel;

				if (null == ScnapcusSict2DSictfensterPanel)
				{
					return null;
				}

				return ScnapcusSict2DSictfensterPanel.Children.OfType<Image>().FirstOrDefault();
			}
		}

		public void Aktualisiire(IVonBerictNaacGbsRepr	SictRepr)
		{
			Bib3.Glob.PropagiireListeRepräsentatioon(
				ListeErwaiterungAuswaalZaitpunkt,
				ListeErwaiterungAuswaalZaitpunktRepr	as	System.Collections.IList,
				(Erwaiterung) => new	SictBerictInspektErwaiterungRepr(Erwaiterung),
				(KandidaatRepr, Erwaiterung) => KandidaatRepr.Repräsentiirte == Erwaiterung,
				(Repr, Erwaiterung) => Repr.Aktualisiire());

			Bib3.Glob.PropagiireListeRepräsentatioon(
				ListeErwaiterungAuswaalZaitpunktRepr,
				TabControlAuswaalZaitpunkt.Items,
				(TabItem) => TabItem.TabItem,
				(KandidaatTabItem, Repr) => Repr.TabItem == KandidaatTabItem,
				null,
				true);

			AktualisiireZaitlaiseRaum();

			ReprRicteNaacAuswert(SictRepr);
		}

		void AktualisiireZaitlaiseRaum()
		{
			Int64? BerictBeginZaitMili = null;
			Int64? BerictEndeZaitMili = null;

			var Auswert = this.Auswert;

			if (null != Auswert)
			{
				BerictBeginZaitMili = Auswert.BerictBeginNuzerZaitMiliBerecne();
				BerictEndeZaitMili = Auswert.BerictEndeNuzerZaitMiliBerecne();
			}

			ZaitlaisteAuswaalGlobaal.RaumGrenzeLinx = BerictBeginZaitMili;
			ZaitlaisteAuswaalGlobaal.RaumGrenzeRecz = BerictEndeZaitMili;

			ZaitlaisteAuswaalGlobaal.AuswaalSctoierelementRicte();
		}

		void ReprRicteNaacAuswert(IVonBerictNaacGbsRepr	SictRepr)
		{
			var Auswert = this.Auswert;

			var MengeMissionInDataGridRepr = DataGridMengeMission.MengeMissionRepr;

			if (Auswert != ReprAuswertLezte)
			{
				MengeMissionInDataGridRepr.Clear();
			}

			ReprAuswertLezte = Auswert;

			if (null == SictRepr)
			{
				MengeMissionInDataGridRepr.Clear();
			}
			else
			{
				SictRepr.PropagiireNaacMengeMissionRepr(Auswert, MengeMissionInDataGridRepr);
			}

			foreach (var MissionInDataGridRepr in MengeMissionInDataGridRepr)
			{
				MissionInDataGridRepr.RaisePropertyChanged();
			}
		}

		void AuswaalRegioonGeändertEventHandler(object sender, RoutedEventArgs e)
		{
			try
			{
				if (null == e)
				{
					return;
				}

				if (Optimat.GBS.Glob.DependencyObjectGibMengeLogicalChild(ZaitlaisteAuswaalGlobaal, null).Contains(e.OriginalSource))
				{
					ZaitlaiseGlobaalAuswaalRegioonNaacGeändert();
				}

				if (Optimat.GBS.Glob.DependencyObjectGibMengeLogicalChild(ZaitlaisteAuswaalAusGlobaalAuswaal, null).Contains(e.OriginalSource))
				{
				}
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}

		void ZaitlaiseGlobaalAuswaalRegioonNaacGeändert()
		{
			ZaitlaisteAuswaalGlobaal.AuswaalSctoierelementRicte();

			var AuswaalRegioonGrenzeLinx = ZaitlaisteAuswaalGlobaal.AuswaalRegioonGrenzeLinx;
			var AuswaalRegioonGrenzeRecz = ZaitlaisteAuswaalGlobaal.AuswaalRegioonGrenzeRecz;

			ZaitlaisteAuswaalAusGlobaalAuswaal.RaumGrenzeLinx = AuswaalRegioonGrenzeLinx;
			ZaitlaisteAuswaalAusGlobaalAuswaal.RaumGrenzeRecz = AuswaalRegioonGrenzeRecz;
		}

		void ZaitlaiseGlobaalAuswaalPunktNaacGeändert()
		{
			var AuswaalPunkt = ZaitlaisteAuswaalGlobaal.AuswaalPunkt;

			ZaitlaisteAuswaalAusGlobaalAuswaal.AuswaalPunkt = AuswaalPunkt;

			ZaitlaisteAuswaalGlobaal.AuswaalSctoierelementRicte();
			ZaitlaisteAuswaalAusGlobaalAuswaal.AuswaalSctoierelementRicte();

			AuswaalZaitpunktReprAktualisiire();
		}

		void ZaitlaiseAusGlobaalAuswaalAuswaalPunktNaacGeändert()
		{
			ZaitlaisteAuswaalAusGlobaalAuswaal.AuswaalSctoierelementRicte();

			var AuswaalPunkt = ZaitlaisteAuswaalAusGlobaalAuswaal.AuswaalPunkt;

			ZaitlaisteAuswaalGlobaal.AuswaalPunkt = AuswaalPunkt;

			ZaitlaisteAuswaalGlobaal.AuswaalSctoierelementRicte();

			AuswaalZaitpunktReprAktualisiire();
		}

		void SezeScnapscusAuswaalAufNääxtePunkt(Int64 SuuceUurscprung)
		{
			var Auswert = this.Auswert;

			if (null == Auswert)
			{
				return;
			}

			var ListeScnapscusMitZait = Auswert.ZuNuzerZaitMiliListeScritInfoNääxte(SuuceUurscprung);

			if (null == ListeScnapscusMitZait)
			{
				return;
			}

			var ScnapscusMitZait = ListeScnapscusMitZait.FirstOrDefault();

			var AuswaalPunktScritInfo = ScnapscusMitZait.Key;

			/*
			 * 2014.10.17
			 * 
			var AuswaalZaitpunkt = (null == AuswaalPunktScritInfo) ? null : AuswaalPunktScritInfo.VonProcessLeeseBeginZait;
			 * */

			var AuswaalZaitpunkt = AuswaalPunktScritInfo.NuzerZait;

			ZaitlaisteAuswaalAusGlobaalAuswaal.AuswaalPunkt = AuswaalZaitpunkt;
			ZaitlaisteAuswaalAusGlobaalAuswaal.AuswaalSctoierelementRicte();

			ZaitlaisteAuswaalGlobaal.AuswaalPunkt = AuswaalZaitpunkt;
			ZaitlaisteAuswaalGlobaal.AuswaalSctoierelementRicte();

			AuswaalZaitpunktReprAktualisiire();
		}

		void AuswaalZaitpunktReprAktualisiire()
		{
			var Auswert = this.Auswert;
			var AuswaalPunktNulbar = ZaitlaisteAuswaalGlobaal.AuswaalPunkt;

			SictOptimatScrit OptimatScrit = null;
			SictOptimatScrit OptimatScritFolgende = null;

			SictVonProcessLeese OptimatScritVonProcessLeese = null;
			SictVonProcessLeese OptimatScritFolgendeVonProcessLeese = null;
			SictVonOptimatMeldungZuusctand	OptimatScritAutomaatZuusctand	= null;
			SictNaacProcessWirkung[] OptimatScritNaacProcessListeWirkung = null;

			SictAusGbsLocationInfo	OptimatScritAutomaatZuusctandLocation	= null;
			ShipState OptimatScritAutomaatZuusctandCharShip = null;
			string OptimatScritAutomaatZuusctandFittingInfoAgrString = null;

			Int64? NuzerScnacpscusZaitMili = null;

			var OptimatScritZaitaintailungListeKomponenteMitBetraag = new List<KeyValuePair<Bib3.FCL.GBS.SictDiagramProportioonKomponente, int>>();
			string	AuswaalZaitpunktEveOnlineClientClockSictString	= null;

			try
			{
				if (null == Auswert)
				{
					return;
				}

				if (!AuswaalPunktNulbar.HasValue)
				{
					return;
				}

				var ListeScnapscusZait =
					Auswert.ZuNuzerZaitMiliListeScritInfoNääxte(
					AuswaalPunktNulbar.Value, 1, 1,	true);

				if (null != ListeScnapscusZait)
				{
					OptimatScrit = ListeScnapscusZait.FirstOrDefault((Kandidaat) => 0 == Kandidaat.Value).Key;
					OptimatScritFolgende = ListeScnapscusZait.FirstOrDefault((Kandidaat) => 1 == Kandidaat.Value).Key;
				}

				if (null != OptimatScrit)
				{
					NuzerScnacpscusZaitMili = OptimatScrit.NuzerZait;

					OptimatScritVonProcessLeese = OptimatScrit.VonProcessLeese;
					OptimatScritAutomaatZuusctand = OptimatScrit.NaacNuzerBerictAutomaatZuusctand	as	SictVonOptimatMeldungZuusctand;
					OptimatScritNaacProcessListeWirkung = OptimatScrit.NaacProcessListeWirkung;
				}

				if (null != OptimatScritFolgende)
				{
					OptimatScritFolgendeVonProcessLeese = OptimatScritFolgende.VonProcessLeese;
				}

				if (NuzerScnacpscusZaitMili.HasValue)
				{
					var EveOnlineClientClockSekunde = Auswert.ZuNuzerZaitMiliBerecneEveOnlineClientClockSekunde(NuzerScnacpscusZaitMili.Value);

					if (EveOnlineClientClockSekunde.HasValue)
					{
						AuswaalZaitpunktEveOnlineClientClockSictString = EveClientClockSictStringSekundeBerecneAusInTaagSekunde(EveOnlineClientClockSekunde.Value);
					}
				}

				if (null != OptimatScritAutomaatZuusctand)
				{
					OptimatScritAutomaatZuusctandLocation = OptimatScritAutomaatZuusctand.CurrentLocation;
					OptimatScritAutomaatZuusctandCharShip = OptimatScritAutomaatZuusctand.ShipZuusctand;
					OptimatScritAutomaatZuusctandFittingInfoAgrString = OptimatScritAutomaatZuusctand.FittingInfoAgrString;
				}

				if (null != OptimatScritVonProcessLeese)
				{
					OptimatScritZaitaintailungListeKomponenteMitBetraag.Add(
						new KeyValuePair<Bib3.FCL.GBS.SictDiagramProportioonKomponente, int>(
							new Bib3.FCL.GBS.SictDiagramProportioonKomponente("read from process", OptimatScritZaitaintailungVonProcessLeeseBrush),
							(int)(OptimatScritVonProcessLeese.Dauer ?? 0)));

					if (null != OptimatScritNaacProcessListeWirkung)
					{
						var OptimatScritNaacProcessListeWirkungFrüühesteBeginZait =
							OptimatScritNaacProcessListeWirkung.DefaultIfEmpty()
							.Min((Wirkung) => ((null == Wirkung) ? null : Wirkung.BeginZaitMili) ?? Int64.MaxValue);

						var OptimatScritNaacProcessListeWirkungScpäätesteEndeZait =
							OptimatScritNaacProcessListeWirkung.DefaultIfEmpty()
							.Min((Wirkung) => ((null == Wirkung) ? null : Wirkung.EndeZaitMili) ?? Int64.MinValue);

						var OptimatScritVonVonProcessLeeseBisNaacProcessListeWirkungDauer =
							OptimatScritNaacProcessListeWirkungFrüühesteBeginZait -
							(OptimatScritVonProcessLeese.EndeZait ?? 0);

						var OptimatScritNaacProcessListeWirkungDauer =
							OptimatScritNaacProcessListeWirkungScpäätesteEndeZait -
							OptimatScritNaacProcessListeWirkungFrüühesteBeginZait;

						if (OptimatScritVonProcessLeese.EndeZait <= OptimatScritNaacProcessListeWirkungFrüühesteBeginZait &&
							OptimatScritNaacProcessListeWirkungFrüühesteBeginZait <= OptimatScritNaacProcessListeWirkungScpäätesteEndeZait)
						{
							OptimatScritZaitaintailungListeKomponenteMitBetraag.Add(
								new KeyValuePair<Bib3.FCL.GBS.SictDiagramProportioonKomponente, int>(
									new Bib3.FCL.GBS.SictDiagramProportioonKomponente("from (read from process).end to input.begin",
										OptimatScritZaitaintailungVonProcessLeeseBisNaacProcessWirkungBrush),
									(int)(OptimatScritVonVonProcessLeeseBisNaacProcessListeWirkungDauer)));

							OptimatScritZaitaintailungListeKomponenteMitBetraag.Add(
								new KeyValuePair<Bib3.FCL.GBS.SictDiagramProportioonKomponente, int>(
									new Bib3.FCL.GBS.SictDiagramProportioonKomponente("input to process", OptimatScritZaitaintailungNaacProcessWirkungBrush),
									(int)(OptimatScritNaacProcessListeWirkungDauer)));

							if (null != OptimatScritFolgendeVonProcessLeese)
							{
								var VonNaacProcessInputBisNääxteScritVonProcessLeeseDauerNulbar =
									OptimatScritFolgendeVonProcessLeese.BeginZaitMili - OptimatScritNaacProcessListeWirkungScpäätesteEndeZait;

								if(0	<= VonNaacProcessInputBisNääxteScritVonProcessLeeseDauerNulbar)
								{
									OptimatScritZaitaintailungListeKomponenteMitBetraag.Add(
										new KeyValuePair<Bib3.FCL.GBS.SictDiagramProportioonKomponente, int>(
											new Bib3.FCL.GBS.SictDiagramProportioonKomponente("from input.end to (next step.(read from process)).begin", OptimatScritZaitaintailungVonNaacProcessWirkungBisVonProcessLeeseBrush),
											(int)(VonNaacProcessInputBisNääxteScritVonProcessLeeseDauerNulbar.Value)));
								}
							}
						}
					}
				}
			}
			finally
			{
				foreach (var Erwaiterung in ListeErwaiterung)
				{
					if (null == Erwaiterung)
					{
						continue;
					}

					System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback((t) => Erwaiterung.AuswaalZaitpunktSeze(AuswaalPunktNulbar)));
				}

				DictZuAnwendungZaitMiliScnapcusAutomaatZuusctandSictJson.BescrankeEntferneLängerNitVerwendete(10);

				ScnapcusReprWindowClientRasterSezeFürNuzerZaitMili(NuzerScnacpscusZaitMili);

				AuswaalZaitpunktCharLocationInspekt.Repräsentiire(OptimatScritAutomaatZuusctandLocation);
				AuswaalZaitpunktShipInspekt.Repräsentiire(OptimatScritAutomaatZuusctandCharShip);
				TextBoxAuswaalZaitpunktFittingInspekt.Text = OptimatScritAutomaatZuusctandFittingInfoAgrString;

				TextBoxScnapcusAuswaalZaitpunktInspekt.Text = Optimat.Glob.SictStringTausenderGetrent(NuzerScnacpscusZaitMili, " ");
				TextBoxScnapcusAuswaalZaitpunktInspektEveClientClock.Text = AuswaalZaitpunktEveOnlineClientClockSictString;

				AuswaalZaitpunktOptimatScritZaitaintailung.Repräsentiire(OptimatScritZaitaintailungListeKomponenteMitBetraag.ToArray());
			}
		}

		void ScnapcusReprWindowClientRasterSeze(SictZuScnapscusRasterInfo	WindowClientRaster)
		{
			byte[] RasterSictSeriel = null;
			BitmapSource SictImageSourceAbbild = null;
			string HerkunftDataiPfaad = null;

			try
			{
				AngezaigtScnapscusWindowClientRaster = WindowClientRaster;

				if (null == WindowClientRaster)
				{
					return;
				}

				HerkunftDataiPfaad = WindowClientRaster.HerkunftDataiPfaad;

				RasterSictSeriel = WindowClientRaster.SictSeriel;

				if (null != RasterSictSeriel)
				{
					SictImageSourceAbbild = Bib3.FCL.Glob.SictBitmapImageBerecne(RasterSictSeriel);
				}
			}
			finally
			{
				ScnapcusWindowClientRasterAbbildLezteSictSeriel = RasterSictSeriel;
				TextBoxScnapscusWindowClientRasterHerkunftDataiPfaad.Text = HerkunftDataiPfaad;
				ScnapcusWindowClientRasterImageSourceSeze(SictImageSourceAbbild);
			}
		}

		public ImageSource ScnapcusWindowClientRasterImageSource
		{
			get
			{
				var ScnapcusSict2DSictfensterImage = this.ScnapcusWindowClientRasterImage;

				if (null == ScnapcusSict2DSictfensterImage)
				{
					return null;
				}

				return ScnapcusSict2DSictfensterImage.Source;
			}
		}

		void ScnapcusWindowClientRasterImageSourceSeze(
			BitmapSource SictImageSourceAbbild)
		{
			var ScnapcusSict2DSictfensterImage = this.ScnapcusWindowClientRasterImage;

			var	ScnapcusWindowClientRasterSictfenster	= this.ScnapcusWindowClientRasterSictfenster;

			if (null != ScnapcusSict2DSictfensterImage)
			{
				ScnapcusSict2DSictfensterImage.Source = SictImageSourceAbbild;

				if (null != SictImageSourceAbbild)
				{
					//	Image skaliire pasend zu Auflöösung von CompositionTarget.TransformToDevice

					double WindowDpiX, WindowDpiY;

					Bib3.FCL.GBS.Extension.ZuCurrentApplicationMainWindowErmitleDpi(out	WindowDpiX, out	WindowDpiY);

					var ScnapcusSict2DSictfensterImageTransform = ScnapcusSict2DSictfensterImage.LayoutTransform	as	ScaleTransform;

					if (null == ScnapcusSict2DSictfensterImageTransform)
					{
						ScnapcusSict2DSictfensterImageTransform = new ScaleTransform();

						ScnapcusSict2DSictfensterImage.LayoutTransform = ScnapcusSict2DSictfensterImageTransform;
					}

					ScnapcusSict2DSictfensterImageTransform.ScaleX = SictImageSourceAbbild.DpiX / WindowDpiX;
					ScnapcusSict2DSictfensterImageTransform.ScaleY = SictImageSourceAbbild.DpiY / WindowDpiY;

					ScnapcusSict2DSictfensterImageLayoutAktualisiire();
				}
			}
		}

		void ScnapcusSict2DSictfensterImageLayoutAktualisiire()
		{
			var ScnapcusSict2DSictfensterImage = this.ScnapcusWindowClientRasterImage;

			if (null == ScnapcusSict2DSictfensterImage)
			{
				return;
			}

			var InterpolatioonNearest = (null == ScnapcusWindowClientRasterSictfenster) ? false : ScnapcusWindowClientRasterSictfenster.SkalatioonNeutral;

			InterpolatioonNearest = false;

			var bitmapScalingMode = InterpolatioonNearest ? BitmapScalingMode.NearestNeighbor : BitmapScalingMode.HighQuality;

			ScnapcusSict2DSictfensterImage.SnapsToDevicePixels = InterpolatioonNearest;

			RenderOptions.SetBitmapScalingMode(ScnapcusSict2DSictfensterImage, bitmapScalingMode);
		}

		void ScnapcusReprWindowClientRasterSezeFürNuzerZaitMili(Int64?	ScnapcusNuzerZaitMili)
		{
			SictZuScnapscusRasterInfo WindowClientRaster = null;

			try
			{
				var Auswert = this.Auswert;

				if (!ScnapcusNuzerZaitMili.HasValue)
				{
					return;
				}

				WindowClientRaster = WindowClientRasterFürVonNuzerZaitMili(ScnapcusNuzerZaitMili.Value);
			}
			finally
			{
				ScnapcusReprWindowClientRasterSeze(WindowClientRaster);
			}
		}

		KeyValuePair<Int64, BitmapSource> ScreenshotLezteVorZaitpunktBitmapSource(Int64 ZaitpunktMili)
		{
			throw new NotImplementedException();
		}

		Int64? ZuAnwendungZaitMiliBerecneNuzerZaitMili(Int64? AnwendungZaitMili)
		{
			if (!AnwendungZaitMili.HasValue)
			{
				return null;
			}

			var Auswert = this.Auswert;

			if (null == Auswert)
			{
				return null;
			}

			return Auswert.ZuAnwendungZaitMiliBerecneNuzerZaitMili(AnwendungZaitMili.Value);
		}

		void AuswaalZaitraumEventHandler(object sender, RoutedEventArgs e)
		{
			try
			{
				var EventArgs = e as SictAuswaalZaitraumEventArgs;

				if (null == EventArgs)
				{
					return;
				}

				var Auswert = this.Auswert;

				var AuswaalZaitraumBeginZaitMili = EventArgs.ZaitraumBegin;
				var AuswaalZaitraumEndeZaitMili = EventArgs.ZaitraumEnde;

				var ZaitgeeberIstAnwendung =
					e.OriginalSource == this.DataGridMengeMission;

				if (ZaitgeeberIstAnwendung)
				{
					AuswaalZaitraumBeginZaitMili = ZuAnwendungZaitMiliBerecneNuzerZaitMili(AuswaalZaitraumBeginZaitMili);
					AuswaalZaitraumEndeZaitMili = ZuAnwendungZaitMiliBerecneNuzerZaitMili(AuswaalZaitraumEndeZaitMili);
				}

				if (null != Auswert)
				{
					/*
					 * 2014.02.08
					 * Vorersct werd hiir Scrankenwert für Zaitpunkt aingesezt wen diiser vorher Nul isc.
					 * Hiirmit werd erraict das in der Zaitlaiste di Detailansict des ausgewäälte Zaitraum genuzt werde kan.
					 * */
					AuswaalZaitraumBeginZaitMili = AuswaalZaitraumBeginZaitMili ?? Auswert.BerictBeginNuzerZaitMiliBerecne();
					AuswaalZaitraumEndeZaitMili = AuswaalZaitraumEndeZaitMili ?? Auswert.BerictEndeNuzerZaitMiliBerecne();
				}

				ZaitlaisteAuswaalGlobaal.AuswaalRegioonGrenzeLinx = AuswaalZaitraumBeginZaitMili;
				ZaitlaisteAuswaalGlobaal.AuswaalRegioonGrenzeRecz = AuswaalZaitraumEndeZaitMili;

				ZaitlaiseGlobaalAuswaalRegioonNaacGeändert();
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}

		void AuswaalPunktGeändertEventHandler(object sender, RoutedEventArgs e)
		{
			try
			{
				if (null == e)
				{
					return;
				}

				if(Optimat.GBS.Glob.DependencyObjectGibMengeLogicalChild(ZaitlaisteAuswaalGlobaal,	null).Contains(e.OriginalSource))
				{
					ZaitlaiseGlobaalAuswaalPunktNaacGeändert();
				}

				if (Optimat.GBS.Glob.DependencyObjectGibMengeLogicalChild(ZaitlaisteAuswaalAusGlobaalAuswaal, null).Contains(e.OriginalSource))
				{
					ZaitlaiseAusGlobaalAuswaalAuswaalPunktNaacGeändert();
				}
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}

		private void DataGridMengeMissionColumnAktioonZeleClick(object sender, RoutedEventArgs e)
		{
			var Source = e.OriginalSource as FrameworkElement;

			if (null == Source)
			{
				return;
			}

			Source.ContextMenu.IsOpen = true;
		}

		public class SictZuScnapscusRasterInfo
		{
			readonly public string HerkunftDataiPfaad;

			readonly public byte[] SictSeriel;

			public SictZuScnapscusRasterInfo(
				string HerkunftDataiPfaad,
				byte[] SictSeriel	= null)
			{
				this.HerkunftDataiPfaad = HerkunftDataiPfaad;
				this.SictSeriel = SictSeriel;
			}

			static public Int64? GrööseBerecne(
				SictZuScnapscusRasterInfo	O)
			{
				if (null == O)
				{
					return null;
				}

				return O.GrööseBerecne();
			}

			public Int64? GrööseBerecne()
			{
				return Bib3.Extension.CountNullable(SictSeriel);
			}
		}

		SictZuScnapscusRasterInfo WindowClientRasterFürVonNuzerZaitMili(Int64 NuzerZaitMili)
		{
			var BerictWindowClientRasterVerzaicnisPfaad = this.BerictWindowClientRasterVerzaicnisPfaad;
			var BerictWindowClientRasterVerzaicnisPfaadLezte = this.BerictWindowClientRasterVerzaicnisPfaadLezte;

			if (!string.Equals(BerictWindowClientRasterVerzaicnisPfaad, BerictWindowClientRasterVerzaicnisPfaadLezte))
			{
				//	Pfaad zum Verzaicnis in deem naac Berict gesuuct were sol wurde geändert, deshalb ale Ainträäge aus Scatenscpaicer entferne.

				DictVonNuzerZaitMiliWindowClientRaster.Leere();

				this.BerictWindowClientRasterVerzaicnisPfaadLezte = BerictWindowClientRasterVerzaicnisPfaad;
			}

			var Auswert = this.Auswert;

			if (null == Auswert)
			{
				return	null;
			}

			/*
			 * 2014.11.07
			 * 
			var OptimatScrit = Auswert.ZuNuzerZaitMiliScritInfoNääxte(NuzerZaitMili);

			if (null == OptimatScrit)
			{
				return null;
			}

			var DataiIdent = OptimatScrit.ProcessWindowClientRasterIdentUndSuuceHinwais;
			 * */

			var	NääxteScreenshotZaitUndIdent	= Auswert.ZuNuzerZaitMiliWindowClientRasterSuuceHinwaisNääxte(NuzerZaitMili);

			var FundDistanz = NääxteScreenshotZaitUndIdent.Zait - NuzerZaitMili;

			if (11111 < Math.Abs(FundDistanz))
			{
				return null;
			}

			var DataiIdent = NääxteScreenshotZaitUndIdent.Wert;

			if (null == DataiIdent)
			{
				return	null;
			}

			var WindowClientRasterInfo =
				DictVonNuzerZaitMiliWindowClientRaster.ValueFürKey(NuzerZaitMili,
				(ZaitMili) => WindowClientRasterFürVonNuzerDataiIdent(BerictWindowClientRasterVerzaicnisPfaad, DataiIdent));

			DictVonNuzerZaitMiliWindowClientRaster.BescrankeEntferneLängerNitVerwendete(
				null, (Int64)1e+7, (Aintraag) => 1000 + (((null == Aintraag) ? null : Aintraag.GrööseBerecne())	?? 0));

			return WindowClientRasterInfo;
		}

		static SictZuScnapscusRasterInfo WindowClientRasterFürVonNuzerDataiIdent(
			string	BerictWindowClientRasterVerzaicnisPfaad,
			SictDataiIdentUndSuuceHinwais	VonNuzerDataiIdent)
		{
			if (null == VonNuzerDataiIdent)
			{
				return null;
			}

			var DataiIdentSHA1 = VonNuzerDataiIdent.IdentSHA1;

			if (null == DataiIdentSHA1)
			{
				return null;
			}

			var SuuceMengeHinwais = VonNuzerDataiIdent.SuuceMengeHinwais;

			if (null == SuuceMengeHinwais)
			{
				return null;
			}

			if (null == BerictWindowClientRasterVerzaicnisPfaad)
			{
				return null;
			}

			var MengeDataiPfaad =
				SuuceMengeHinwais
				.Select((SuuceHinwais) => BerictWindowClientRasterVerzaicnisPfaad +
					System.IO.Path.DirectorySeparatorChar + System.IO.Path.GetFileName(SuuceHinwais.NaameString)).ToArray();

			foreach (var DataiPfaad in MengeDataiPfaad)
			{
				byte[] DataiInhalt, DataiInhaltHashSHA1;

				Bib3.Glob.LaadeInhaltAusDataiPfaad(DataiPfaad, out	DataiInhalt, out	DataiInhaltHashSHA1);

				if (null == DataiInhalt)
				{
					continue;
				}

				if (!DataiIdentSHA1.SequenceEqual(DataiInhaltHashSHA1))
				{
					continue;
				}

				return new SictZuScnapscusRasterInfo(DataiPfaad, DataiInhalt);
			}

			return null;
		}

		private void ButtonScnapscusAuswaalZaitpunktSezeJezt_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				var ZaitpunktSictString = TextBoxScnapcusAuswaalZaitpunktAingaabe.Text;

				if (null == ZaitpunktSictString)
				{
					return;
				}

				var Zaitpunkt = Bib3.Glob.TryParseInt64(ZaitpunktSictString.Replace(" ", ""), System.Globalization.CultureInfo.CurrentCulture.NumberFormat);

				if (!Zaitpunkt.HasValue)
				{
					return;
				}

				SezeScnapscusAuswaalAufNääxtePunkt(Zaitpunkt.Value);
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}

		private void ScnapscusWindowClientRasterScraibeNaacDatai_Drop(object sender, DragEventArgs e)
		{
			try
			{
				var ScnapcusWindowClientRasterAbbildLezteSictSeriel = this.ScnapcusWindowClientRasterAbbildLezteSictSeriel;

				if (null == ScnapcusWindowClientRasterAbbildLezteSictSeriel)
				{
					throw new ArgumentNullException("ScnapcusWindowClientRasterAbbildLezteSictSeriel");
				}

				var DataiPfaad = Bib3.FCL.Glob.DataiPfaadAlsKombinatioonAusSctandardPfaadUndFileDrop("Scnapscus.Window.Client.Raster", e);

				Optimat.Glob.ScraibeNaacDataiMitPfaadListeOktet(DataiPfaad, ScnapcusWindowClientRasterAbbildLezteSictSeriel);
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}

		void SezeScnapscusAuswaalAufNääxtePunktMitScritDistanzZuAuswaalVorherig(int ScnapscusScritDistanz)
		{
			var BisherAuswaalPunktNulbar = ZaitlaisteAuswaalGlobaal.AuswaalPunkt;

			if (!BisherAuswaalPunktNulbar.HasValue)
			{
				return;
			}

			var BisherAuswaalPunkt = BisherAuswaalPunktNulbar.Value;

			SezeScnapscusAuswaalAufNääxtePunktMitScritDistanzZuZait(BisherAuswaalPunkt, ScnapscusScritDistanz);
		}

		void SezeScnapscusAuswaalAufNääxtePunktMitScritDistanzZuZait(
			Int64 UursprungZait,
			int ScnapscusScritDistanz)
		{
			Int64 AuswaalScnapscusZait = -1;

			try
			{
				var Auswert = this.Auswert;

				if (null == Auswert)
				{
					return;
				}

				var BisherAuswaalPunktNulbar = ZaitlaisteAuswaalGlobaal.AuswaalPunkt;

				if (!BisherAuswaalPunktNulbar.HasValue)
				{
					return;
				}

				var BisherAuswaalPunkt = BisherAuswaalPunktNulbar.Value;

				var ListeScnapscusScritDistanzUndZait =
					Auswert.ZuNuzerZaitMiliListeScritInfoNääxte(BisherAuswaalPunkt, -ScnapscusScritDistanz, ScnapscusScritDistanz);

				var ScnapscusScritDistanzUndZait =
					ListeScnapscusScritDistanzUndZait.FirstOrDefault((Kandidaat) => Kandidaat.Value == ScnapscusScritDistanz);

				if (!(ScnapscusScritDistanz == ScnapscusScritDistanzUndZait.Value))
				{
					//	Es wurde kain Scnapscus in angegeebene Rictung gefunde (z.B. am Begin Suuce Rükwärts oder am Ende suuce Vorwärts)
					//	Auswaal sol auf bisherigem Wert blaibe
					AuswaalScnapscusZait = BisherAuswaalPunkt;
					return;
				}

				var AuswaalScritInfo = ScnapscusScritDistanzUndZait.Key;

				if (null == AuswaalScritInfo)
				{
					return;
				}

				/*
				 * 2014.10.17
				 * 
				var AuswaalScritInfoVonZiilProcessLeeseBeginZait = AuswaalScritInfo.VonProcessLeeseBeginZait;

				if (AuswaalScritInfoVonZiilProcessLeeseBeginZait.HasValue)
				{
					AuswaalScnapscusZait = AuswaalScritInfoVonZiilProcessLeeseBeginZait.Value;
				}
				 * */

				AuswaalScnapscusZait = AuswaalScritInfo.NuzerZait;
			}
			finally
			{
				SezeScnapscusAuswaalAufNääxtePunkt(AuswaalScnapscusZait);
			}
		}

		private void ButtonScnapscusAuswaalInBerictVorherigeJezt_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				SezeScnapscusAuswaalAufNääxtePunktMitScritDistanzZuAuswaalVorherig(-1);
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}

		private void ButtonScnapscusAuswaalInBerictFolgendeJezt_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				SezeScnapscusAuswaalAufNääxtePunktMitScritDistanzZuAuswaalVorherig(1);
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}

		private void ButtonScnapscusAuswaalInAuswaalVorherigeJezt_Click(object sender, RoutedEventArgs e)
		{
			try
			{
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}

		private void ButtonScnapscusAuswaalInAuswaalFolgendeJezt_Click(object sender, RoutedEventArgs e)
		{
			try
			{
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}

		string TextBoxScnapcusAuswaalZaitpunktEveClientClockRicteTrenzaice = ":";

		public	string EveClientClockSictStringSekundeBerecneAusInTaagSekunde(int	InTaagSekunde)
		{
			InTaagSekunde	= (int)Optimat.Glob.SictUmgebrocen(InTaagSekunde, 0, 60 * 60 * 24);

			var Sctunde = InTaagSekunde / 3600;

			var InSctundeMinuute = (InTaagSekunde / 60) % 60;

			var InMinuuteSekunde = InTaagSekunde % 60;

			return
				Sctunde.ToString("D2") + TextBoxScnapcusAuswaalZaitpunktEveClientClockRicteTrenzaice +
				InSctundeMinuute.ToString("D2") + TextBoxScnapcusAuswaalZaitpunktEveClientClockRicteTrenzaice +
				InMinuuteSekunde.ToString("D2");
		}

		public	int? ScnapcusAuswaalZaitpunktEveClientClockSekundeBerecneAusAingaabeString(string Aingaabe)
		{
			if (null == Aingaabe)
			{
				return null;
			}

			var ListeKomponenteString = Aingaabe.Split(new string[] { TextBoxScnapcusAuswaalZaitpunktEveClientClockRicteTrenzaice }, StringSplitOptions.RemoveEmptyEntries);

			var ListeKomponenteInt =
				ListeKomponenteString
				.Select((KomponenteString) => Bib3.Glob.TryParseInt(KomponenteString))
				.TakeWhile((Komponente) => Komponente.HasValue)
				.ToArray();

			if (ListeKomponenteInt.NullOderLeer())
			{
				return null;
			}

			var InTagSekunde =
				ListeKomponenteInt
				.Select((KomponenteBetraag, KomponenteScteleIndex) =>
					{
						var KomponenteScteleWert = (int)(3600 / (Math.Pow(60, KomponenteScteleIndex)));

						var KomponenteBetraagUmgebroce = KomponenteBetraag.Value.SictUmgebrocen(0, 0 < KomponenteScteleIndex ? 60 : 24);

						return KomponenteScteleWert * (int)KomponenteBetraagUmgebroce;
					})
				.Sum();

			return InTagSekunde;
		}

		void TextBoxScnapcusAuswaalZaitpunktAingaabeRicteNaacAingaabeEveClientClock()
		{
			var InTaagSekunde = ScnapcusAuswaalZaitpunktEveClientClockSekundeBerecneAusAingaabeString(TextBoxScnapcusAuswaalZaitpunktEveClientClock.Text);

			if (!InTaagSekunde.HasValue)
			{
				return;
			}

			var	NuzerZaitMili	= Auswert.ZuEveOnlineClientClockSekundeBerecneNuzerZaitMili(InTaagSekunde.Value);

			if (!NuzerZaitMili.HasValue)
			{
				return;
			}

			TextBoxScnapcusAuswaalZaitpunktAingaabe.TextBoxSetTextBedingtChangeToken(Optimat.Glob.SictStringTausenderGetrent(NuzerZaitMili), 1000);
		}

		void TextBoxScnapcusAuswaalZaitpunktEveClientClockRicteNaacAingaabeZaitpunkt()
		{
			string EveOnlineInTaagSekundeSictString = null;

			try
			{
				var NuzerZaitMili = TextBoxScnapcusAuswaalZaitpunktAingaabe.Text.Replace(" ", "").TryParseInt64();

				if (!NuzerZaitMili.HasValue)
				{
					return;
				}

				var EveOnlineInTaagSekunde = Auswert.ZuNuzerZaitMiliBerecneEveOnlineClientClockSekunde(NuzerZaitMili.Value);

				if (!EveOnlineInTaagSekunde.HasValue)
				{
					return;
				}

				EveOnlineInTaagSekundeSictString = EveClientClockSictStringSekundeBerecneAusInTaagSekunde(EveOnlineInTaagSekunde.Value);
			}
			finally
			{
				TextBoxScnapcusAuswaalZaitpunktEveClientClock.TextBoxSetTextBedingtChangeToken(EveOnlineInTaagSekundeSictString, 1000);
			}
		}

		private void TextBoxScnapcusAuswaalZaitpunktAingaabe_TextChanged(object sender, TextChangedEventArgs e)
		{
			try
			{
				using (var ChangeToken = TextBoxScnapcusAuswaalZaitpunktAingaabe.GetChangeToken(10))
				{
					if (null == ChangeToken)
					{
						return;
					}

					TextBoxScnapcusAuswaalZaitpunktEveClientClockRicteNaacAingaabeZaitpunkt();
				}
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}

		private void TextBoxScnapcusAuswaalZaitpunktEveClientClock_TextChanged(object sender, TextChangedEventArgs e)
		{
			try
			{
				using (var ChangeToken = TextBoxScnapcusAuswaalZaitpunktEveClientClock.GetChangeToken(10))
				{
					if (null == ChangeToken)
					{
						return;
					}
					
					TextBoxScnapcusAuswaalZaitpunktAingaabeRicteNaacAingaabeEveClientClock();
				}
			}
			catch (System.Exception Exception)
			{
				Optimat.Glob.ZaigeMessageBoxException(Exception);
			}
		}
	}

	public interface IVonBerictNaacGbsRepr
	{
		void PropagiireNaacMengeMissionRepr(
			Berict.Auswert.IBerictInspektDaatenKwele Berict,
			IList<SictObservable<SictMissionReprInDataGrid>> MengeMissionRepr,
			bool RepräsentatioonEntferneNict	= false);
	}

	public class SictVonBerictNaacGbsRepr : IVonBerictNaacGbsRepr
	{
		static public bool ReprPasendZuMission(
			SictObservable<SictMissionReprInDataGrid> ReprObservable,
			SictMissionZuusctand Mission)
		{
			if (null == ReprObservable)
			{
				return false;
			}

			var Repr = ReprObservable.Wert;

			if (null == Repr)
			{
				return false;
			}

			var	ReprMission	= Repr.MissionBerecne();

			if (Mission == ReprMission)
			{
				return true;
			}

			if (null == Mission)
			{
				return false;
			}

			if (null == ReprMission)
			{
				return false;
			}

			return ReprMission.Ident == Mission.Ident;
		}

		public void PropagiireNaacMengeMissionRepr(
			Berict.Auswert.IBerictInspektDaatenKwele Berict,
			IList<SictObservable<SictMissionReprInDataGrid>> MengeMissionRepr,
			bool RepräsentatioonEntferneNict	= false)
		{
			if (null == Berict)
			{
				MengeMissionRepr.Clear();
				return;
			}

			var	HerkunftMengeMission	= Berict.MengeMissionIdentUndZuusctandLezteBerecne();

			PropagiireNaacMengeMissionRepr(
				HerkunftMengeMission,
				MengeMissionRepr,
				RepräsentatioonEntferneNict);
		}

		static	public void PropagiireNaacMengeMissionRepr(
			IEnumerable<KeyValuePair<Int64,	SictWertMitZait<SictMissionZuusctand>>>	MengeMission,
			IList<SictObservable<SictMissionReprInDataGrid>> MengeMissionRepr,
			bool RepräsentatioonEntferneNict	= false)
		{
			Bib3.Glob.PropagiireListeRepräsentatioon(
				MengeMission,
				MengeMissionRepr,
				(Mission) => new SictObservable<SictMissionReprInDataGrid>(new SictMissionReprInDataGridAusMissionZuusctand(Mission.Value.Wert)),
				(KandidaatRepr, KandidaatMission) =>	ReprPasendZuMission(KandidaatRepr, KandidaatMission.Value.Wert),
				(ReprObservable, Mission) =>
				{
					var ReprScpez = ReprObservable.Wert as SictMissionReprInDataGridAusMissionZuusctand;

					ReprScpez.Mission = Mission.Value.Wert;

					ReprObservable.RaisePropertyChanged();
				},
				RepräsentatioonEntferneNict);
		}
	}
}
