using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Bib3;
using Optimat.EveOnline.Base;

namespace Optimat.EveOnline.Nuzer
{
	public	class	SictNuzer
	{
		public Int64 NaacOptimatZaitMili;

		public Int64 NaacOptimatMeldungWiderhoolungZaitDistanzMili = 30000;

		/// <summary>
		/// !!!!	Kostet meer recenzait wen True
		/// </summary>
		public bool TempDebugGbsBaumSictDiferenzVerifiziire = false;

		public Int64? TempDebugGbsBaumSictDiferenzVerifikatioonFeelsclaagLezteZaitMili
		{
			private set;
			get;
		}

		readonly List<SictWertMitZait<string>> TempDebugGbsBaumSictDiferenzVerifikatioonListeScnapscusGbsBaumSictSeriel =
			new List<SictWertMitZait<string>>();

		readonly SictGbsBaumSictSume<GbsAstInfo> TempDebugGbsBaumSictDiferenzVerifikatioonSictSume = new SictGbsBaumSictSume<GbsAstInfo>();

		readonly public List<Optimat.EveOnline.SictVonOptimatNaacrict> TempVonAnwendungListeNaacrict =
			new List<Optimat.EveOnline.SictVonOptimatNaacrict>();

		readonly public	List<SictWertMitZait<SictDataiIdentUndSuuceHinwais>> TempBerictListeWindowClientRasterGescriibe =
			new List<SictWertMitZait<SictDataiIdentUndSuuceHinwais>>();

		public SictVerlaufBeginUndEndeRef<GbsAstInfo> TempNaacAnwendungZuMeldeGbsBaumWurzel;

		public SictOptimatScrit TempNaacAnwendungZuMeldeOptimatScritVorherigListeWirkungErgeebnis;

		public SictNaacOptimatMeldungZuusctand NaacOptimatMeldungZuusctand;

		public SictVonOptimatMeldungZuusctand VonOptimatMeldungZuusctand;

		readonly SictGbsBaumSictDiferenz GbsBaumSictDiferenz = new SictGbsBaumSictDiferenz();

		/// <summary>
		/// Zusamefasung aler Objekte welce per Optimat.RefNezDiferenz.SictRefNezDiferenz naac Optimat gemeldet werde.
		/// </summary>
		readonly Bib3.RefNezDiferenz.SictRefNezDiferenz NaacOptimatMeldungSictDiferenz = new Bib3.RefNezDiferenz.SictRefNezDiferenz(
			Bib3.RefNezDiferenz.NewtonsoftJson.SictMengeTypeBehandlungRictliinieNewtonsoftJson.KonstruktSictParam());

		static	public Bib3.RefNezDiferenz.SictDiferenzSictParam VonServerBerictOptimatZuusctandSictParam =
			VonServerBerictOptimatZuusctandSictParamBerecne();

		static Bib3.RefNezDiferenz.SictDiferenzSictParam VonServerBerictOptimatZuusctandSictParamBerecne()
		{
			return Bib3.RefNezDiferenz.NewtonsoftJson.SictMengeTypeBehandlungRictliinieNewtonsoftJson.KonstruktSictParam();
		}

		readonly Bib3.RefNezDiferenz.SictRefNezSume VonOptimatMeldungZuusctandSictSume =
			new Bib3.RefNezDiferenz.SictRefNezSume(VonServerBerictOptimatZuusctandSictParam);

		readonly SictSctroomNaacrictAusSctroomChar ScnitServerVonAnwendungAingangPufer =
			new SictSctroomNaacrictAusSctroomChar(SictSctroomNaacrictAusSctroomChar.ScnitKlientSymboolNaacrictTrenungSctandard);

		readonly public SictTransportPufer<Optimat.EveOnline.SictNaacOptimatNaacrict> VonNuzerNaacAnwendungListeNaacrict =
			new SictTransportPufer<Optimat.EveOnline.SictNaacOptimatNaacrict>();

		public int AnwendungBenaacrictigeÜüberÄnderungDistanzScrankeMin = 500;
		public Int64 AnwendungBenaacrictigeÜüberÄnderungLezteZait	= int.MinValue;

		public SictWertMitZait<Int64>? MesungAnwendungServerZaitMikro
		{
			set;
			get;
		}

		public Int64? AnwendungServerTaagBeginZaitMikro
		{
			set;
			get;
		}

		readonly List<char[]> NaacAnwendungZuScraibeListeListeOktet = new List<char[]>();

		public IEnumerable<SictVorsclaagNaacProcessWirkung> VonOptimatMeldungVorsclaagListeWirkung
		{
			get
			{
				var VonOptimatMeldungZuusctand = this.VonOptimatMeldungZuusctand;

				if (null == VonOptimatMeldungZuusctand)
				{
					return null;
				}

				return VonOptimatMeldungZuusctand.VorsclaagListeWirkung;
			}
		}

		public IEnumerable<SictNaacProcessWirkung> NaacOptimatMeldungListeWirkung
		{
			get
			{
				var NaacOptimatMeldungZuusctand = this.NaacOptimatMeldungZuusctand;

				if (null == NaacOptimatMeldungZuusctand)
				{
					return null;
				}

				return NaacOptimatMeldungZuusctand.ListeNaacProcessWirkung;
			}
		}

		public SictVerlaufBeginUndEndeRefMitZaitAinhaitMili<int> NaacOptimatMeldungVonProcessLeeseLezte
		{
			get
			{
				var NaacOptimatMeldungZuusctand = this.NaacOptimatMeldungZuusctand;

				if (null == NaacOptimatMeldungZuusctand)
				{
					return null;
				}

				var ListeVonProcessLeese = NaacOptimatMeldungZuusctand.ListeVonProcessLeese;

				return ListeVonProcessLeese.LastOrDefaultNullable();
			}
		}

		public SictNuzer()
		{
		}

		public SictNuzer(
			bool	TempDebugGbsBaumSictDiferenzVerifiziire = false)
		{
			this.TempDebugGbsBaumSictDiferenzVerifiziire	= TempDebugGbsBaumSictDiferenzVerifiziire;
		}

		public char[] NaacAnwendungZuScraibenListeListeCharNimHerausAle()
		{
			return Optimat.Glob.AusListeNimHerausAgregiirtLocked(NaacAnwendungZuScraibeListeListeOktet);
		}

		void NaacAnwendungZuScraibenListeListeCharFüügeAin(char[] NaacAnwendungZuScraibenListeOktet)
		{
			lock (NaacAnwendungZuScraibeListeListeOktet)
			{
				NaacAnwendungZuScraibeListeListeOktet.Add(NaacAnwendungZuScraibenListeOktet);
			}
		}

		void AnwendungBenaacrictigeÜüberÄnderung()
		{
			var BeginZaitMili = NaacOptimatZaitMili;

			var NaacAnwendungNaacrict = new Optimat.EveOnline.SictNaacOptimatNaacrict();

			NaacAnwendungNaacrict.NuzerZaitMili = NaacOptimatZaitMili;

			var TempNaacAnwendungZuMeldeGbsBaumWurzelMitZait = this.TempNaacAnwendungZuMeldeGbsBaumWurzel;
			var TempNaacAnwendungZuMeldeOptimatScritVorherigListeWirkungErgeebnis = this.TempNaacAnwendungZuMeldeOptimatScritVorherigListeWirkungErgeebnis;

			var	NaacOptimatMeldungZuusctandDiferenzScrit	=
				NaacOptimatMeldungSictDiferenz.BerecneScritDif(
				NaacOptimatMeldungSictDiferenz.ScritLezteIndex	- 100,
				new object[] { NaacOptimatMeldungZuusctand });

			if (null != NaacOptimatMeldungZuusctandDiferenzScrit)
			{
				if (!NaacOptimatMeldungZuusctandDiferenzScrit.MengeZuReferenzDiferenz.NullOderLeer())
				{
					NaacAnwendungNaacrict.ZuusctandDiferenz =
						Bib3.RefNezDiferenz.SictRefNezDiferenzScritSictJson.Konstrukt(NaacOptimatMeldungZuusctandDiferenzScrit);
				}
			}

			if (null != TempNaacAnwendungZuMeldeGbsBaumWurzelMitZait)
			{
				var TempNaacAnwendungZuMeldeGbsBaumWurzel = TempNaacAnwendungZuMeldeGbsBaumWurzelMitZait.Wert;

				var DiferenzZuBaumBeraitsBerecnet = false;

				if (!DiferenzZuBaumBeraitsBerecnet)
				{
					var ScnapscusEndeZaitNulbar = TempNaacAnwendungZuMeldeGbsBaumWurzelMitZait.EndeZait;

					if (ScnapscusEndeZaitNulbar.HasValue)
					{
						var MeldungZaitScrankeMin = ScnapscusEndeZaitNulbar - NaacOptimatMeldungWiderhoolungZaitDistanzMili;

						var GbsBaumScritDiferenz =
							GbsBaumSictDiferenz.BerecneScritDif(
							ScnapscusEndeZaitNulbar.Value,
							TempNaacAnwendungZuMeldeGbsBaumWurzel,
							MeldungZaitScrankeMin);

						var GbsBaumScritDiferenzMitZait = new SictVerlaufBeginUndEndeRef<SictGbsBaumSictDiferenzScritAbbild>(
							TempNaacAnwendungZuMeldeGbsBaumWurzelMitZait.BeginZait,
							TempNaacAnwendungZuMeldeGbsBaumWurzelMitZait.EndeZait,
							GbsBaumScritDiferenz);

						if (!GbsBaumScritDiferenz.MengeAstMeldungDiferenz.NullOderLeer())
						{
							NaacAnwendungNaacrict.GbsBaumDiferenz = GbsBaumScritDiferenz;
						}

						if (TempDebugGbsBaumSictDiferenzVerifiziire)
						{
							var VerifikatioonMengeGbsBaumWurzel =
								TempDebugGbsBaumSictDiferenzVerifikatioonSictSume.BerecneScritSume(GbsBaumScritDiferenz);

							var VerifikatioonGbsBaumWurzel =
								(null == VerifikatioonMengeGbsBaumWurzel) ? null :
								VerifikatioonMengeGbsBaumWurzel.FirstOrDefault();

							var SerializerSettings = new JsonSerializerSettings();

							SerializerSettings.DefaultValueHandling = DefaultValueHandling.Ignore;

							string VerifikatioonGbsBaumWurzelSictSeriel;
							string UurpsrungGbsBaumWurzelSictSeriel;

							if (!Bib3.RefNezDiferenz.Extension.EqualPerNewtonsoftJsonSerializer(
								VerifikatioonGbsBaumWurzel,
								TempNaacAnwendungZuMeldeGbsBaumWurzel,
								out	VerifikatioonGbsBaumWurzelSictSeriel,
								out	UurpsrungGbsBaumWurzelSictSeriel,
								SerializerSettings))
							{
								TempDebugGbsBaumSictDiferenzVerifikatioonFeelsclaagLezteZaitMili = TempNaacAnwendungZuMeldeGbsBaumWurzelMitZait.EndeZait;
							}

							TempDebugGbsBaumSictDiferenzVerifikatioonListeScnapscusGbsBaumSictSeriel.Add(
								new SictWertMitZait<string>(TempNaacAnwendungZuMeldeGbsBaumWurzelMitZait.EndeZait ?? -1, UurpsrungGbsBaumWurzelSictSeriel));

							//	Liste Kürze da sonst Scpaicerbeleegung scteetig Sctaigend
							Bib3.Extension.ListeKürzeBegin(TempDebugGbsBaumSictDiferenzVerifikatioonListeScnapscusGbsBaumSictSeriel, 40);
						}

						{
							//	Temp Ast für Debug

							if (null != GbsBaumScritDiferenz)
							{
								if (0 < GbsBaumScritDiferenz.MengeAstMeldungDiferenz.CountNullable())
								{
								}
							}
						}

						var NaacAnwendungMeldungVonZiilProcessLeese = new SictVonProcessLeese(
							GbsBaumScritDiferenzMitZait.BeginZait,
							GbsBaumScritDiferenzMitZait.EndeZait,
							null,
							GbsBaumScritDiferenzMitZait.Wert);
					}
				}
			}

			if (0 < TempBerictListeWindowClientRasterGescriibe.Count)
			{
				NaacAnwendungNaacrict.MengeWindowClientRasterMitZaitMili = TempBerictListeWindowClientRasterGescriibe.ToArray();
				TempBerictListeWindowClientRasterGescriibe.Clear();
			}

			if (null != NaacAnwendungNaacrict.ZuusctandDiferenz	||
				null != NaacAnwendungNaacrict.GbsBaumDiferenz ||
				null != NaacAnwendungNaacrict.MengeWindowClientRasterMitZaitMili)
			{
				AnwendungBenaacrictigeÜüberÄnderungLezteZait = BeginZaitMili;

				ScnitOptimatAnwendungScraibe(NaacAnwendungNaacrict);
			}
		}

		void VonAnwendungAingangNaacrict(Optimat.EveOnline.SictVonOptimatNaacrict VonAnwendungAingangNaacrict)
		{
			var BeginZaitMikro = Bib3.Glob.StopwatchZaitMikroSictInt();

			var BeginZaitMili = BeginZaitMikro / 1000;

			if (null == VonAnwendungAingangNaacrict)
			{
				return;
			}

			TempVonAnwendungListeNaacrict.Add(VonAnwendungAingangNaacrict);

			var AnwendungServerZaitMili = VonAnwendungAingangNaacrict.AnwendungServerZaitMili;
			var AnwendungServerTaagBeginZaitMili = VonAnwendungAingangNaacrict.AnwendungServerTaagBeginZaitMili;
			var VonOptimatMeldungZuusctandDiferenz = VonAnwendungAingangNaacrict.ZuusctandDiferenz;

			{
				if (AnwendungServerZaitMili != null)
				{
					this.MesungAnwendungServerZaitMikro = new SictWertMitZait<Int64>(
						BeginZaitMikro, AnwendungServerZaitMili.Value * 1000);
				}

				if (AnwendungServerTaagBeginZaitMili != null)
				{
					this.AnwendungServerTaagBeginZaitMikro = AnwendungServerTaagBeginZaitMili * 1000;
				}
			}

			if (null != VonOptimatMeldungZuusctandDiferenz)
			{
				var NaacNuzerBerictScritDiferenz = Bib3.RefNezDiferenz.SictRefNezDiferenzScritSictJson.KonstruktZurük(VonOptimatMeldungZuusctandDiferenz);

				var ScritSumeErgeebnis = VonOptimatMeldungZuusctandSictSume.BerecneScritSumeListeWurzelRefClrUndErfolg(NaacNuzerBerictScritDiferenz);

				VonOptimatMeldungZuusctandSictSume.MengeObjektInfoEntferneÄltere((VonOptimatMeldungZuusctandSictSume.SelbstScritLezteIndex	?? 0) - 500);

				if (null == ScritSumeErgeebnis)
				{
					//	Meldung Feeler
				}
				else
				{
					if (ScritSumeErgeebnis.Volsctändig)
					{
						var ListeWurzelReferenzClr = ScritSumeErgeebnis.ListeWurzelRefClr;

						if (0 < ListeWurzelReferenzClr.CountNullable())
						{
							var WurzelReferenzClr = ListeWurzelReferenzClr.FirstOrDefault();

							var VonOptimatMeldungZuusctand = WurzelReferenzClr as Optimat.EveOnline.SictVonOptimatMeldungZuusctand;

							if (null == VonOptimatMeldungZuusctand)
							{
								//	Meldung Feeler
							}
							else
							{
								this.VonOptimatMeldungZuusctand = VonOptimatMeldungZuusctand;
							}
						}
					}
				}
			}
		}

		void ScnitOptimatAnwendungScraibe(Optimat.EveOnline.SictNaacOptimatNaacrict NaacAnwendungNaacrict)
		{
			var SerializeSettings = new JsonSerializerSettings();

			SerializeSettings.DefaultValueHandling = DefaultValueHandling.Ignore;

			var NaacAnwendungNaacrictSictStringAbbild = JsonConvert.SerializeObject(NaacAnwendungNaacrict, SerializeSettings);

			var NaacAnwendungNaacrictSictStringAbbildMitTrenzaicen =
				NaacAnwendungNaacrictSictStringAbbild +
				new string(SictSctroomNaacrictAusSctroomChar.ScnitKlientSymboolNaacrictTrenungSctandard);

			NaacAnwendungZuScraibenListeListeCharFüügeAin(NaacAnwendungNaacrictSictStringAbbildMitTrenzaicen.ToCharArray());
		}

		public void Kümere(
			bool AnwendungBenaacrictigeÜüberÄnderungSol,
			char[]	VonAnwendungAingangListeChar)
		{
			var BeginZaitMili = NaacOptimatZaitMili;

			do
			{
				if (VonAnwendungAingangListeChar == null)
				{
					break;
				}

				if (VonAnwendungAingangListeChar.Length < 1)
				{
					break;
				}

				System.Exception ErgeebnisAusnaame;
				Optimat.EveOnline.SictVonOptimatNaacrict[] VonAnwendungAingangListeNaacrict;

				ScnitServerVonAnwendungAingangPufer.VerarbaiteAingangAbbildJson(
					VonAnwendungAingangListeChar,
					out	ErgeebnisAusnaame,
					out	VonAnwendungAingangListeNaacrict);

				if (VonAnwendungAingangListeNaacrict != null)
				{
					foreach (var VonAnwendungNaacrict in VonAnwendungAingangListeNaacrict)
					{
						VonAnwendungAingangNaacrict(VonAnwendungNaacrict);
					}
				}
			} while (false);

			var AnwendungBenaacrictigeÜüberÄnderungLezteAlter = BeginZaitMili - AnwendungBenaacrictigeÜüberÄnderungLezteZait;

			if (AnwendungBenaacrictigeÜüberÄnderungSol &&
				AnwendungBenaacrictigeÜüberÄnderungDistanzScrankeMin <= AnwendungBenaacrictigeÜüberÄnderungLezteAlter)
			{
				AnwendungBenaacrictigeÜüberÄnderung();
			}

			{
				while (0 < VonNuzerNaacAnwendungListeNaacrict.ListeNaacrictAnzaal)
				{
					var VonNuzerNaacAnwendungNaacrictNääxte = VonNuzerNaacAnwendungListeNaacrict.ListeNaacrictNimHeraus();

					ScnitOptimatAnwendungScraibe(VonNuzerNaacAnwendungNaacrictNääxte);
				}
			}
		}

		public KeyValuePair<SictVorsclaagNaacProcessWirkung, SictNaacProcessWirkung>[] ListeVorsclaagWirkungUndWirkungBerecne()
		{
			var VorsclaagListeWirkung = this.VonOptimatMeldungVorsclaagListeWirkung;
			var NaacZiilProcessListeWirkung = this.NaacOptimatMeldungListeWirkung;

			if (null == NaacZiilProcessListeWirkung && null == VorsclaagListeWirkung)
			{
				return null;
			}

			var ListeVorsclaagWirkungUndWirkung = new KeyValuePair<SictVorsclaagNaacProcessWirkung, SictNaacProcessWirkung>[
				Bib3.Glob.Max(VorsclaagListeWirkung.CountNullable(), NaacZiilProcessListeWirkung.CountNullable()) ?? 0];

			for (int i = 0; i < ListeVorsclaagWirkungUndWirkung.Length; i++)
			{
				var VorsclaagWirkung = VorsclaagListeWirkung.ElementAtOrDefault(i);
				var NaacZiilProcessWirkung = NaacZiilProcessListeWirkung.ElementAtOrDefault(i);

				ListeVorsclaagWirkungUndWirkung[i] = new KeyValuePair<SictVorsclaagNaacProcessWirkung, SictNaacProcessWirkung>(
					VorsclaagWirkung, NaacZiilProcessWirkung);
			}

			return ListeVorsclaagWirkungUndWirkung;
		}

		public Int64? FolgendeScritVonZiilProcessLeeseBeginZaitScrankeMinMiliBerecne(
			Int64? VonWirkungWartezaitScrankeMinimumMili = null)
		{
			var VonZiilProcessLeese = this.NaacOptimatMeldungVonProcessLeeseLezte;

			var ListeVorsclaagWirkungUndWirkung = ListeVorsclaagWirkungUndWirkungBerecne();

			Int64? NaacVonZiilProcessLeeseNääxteVonZiilProcessLeeseBeginZaitScrankeMili = null;
			Int64? NaacWirkungNääxteVonZiilProcessLeeseBeginZaitScrankeMili = null;

			if (null != ListeVorsclaagWirkungUndWirkung)
			{
				foreach (var VorsclaagWirkungUndWirkung in ListeVorsclaagWirkungUndWirkung)
				{
					var VorsclaagWirkung = VorsclaagWirkungUndWirkung.Key;
					var NaacZiilProcessWirkung = VorsclaagWirkungUndWirkung.Value;

					var VonVonZiilProcesLeeseBisVonZiilProcesLeeseWartezaitMili = (null == VorsclaagWirkung) ? null : VorsclaagWirkung.VonVonZiilProcessLeeseBisVonZiilProcessLeeseWartezaitMili;
					var VonWirkungBisVonZiilProcesLeeseWartezaitMili =
						Bib3.Glob.Max(VonWirkungWartezaitScrankeMinimumMili,
						(null == VorsclaagWirkung) ? null : VorsclaagWirkung.VonWirkungBisVonZiilProcessLeeseWartezaitMili);

					if (null != VonZiilProcessLeese)
					{
						NaacVonZiilProcessLeeseNääxteVonZiilProcessLeeseBeginZaitScrankeMili =
							Bib3.Glob.Max(
							NaacVonZiilProcessLeeseNääxteVonZiilProcessLeeseBeginZaitScrankeMili,
							VonZiilProcessLeese.BeginZaitMili + VonVonZiilProcesLeeseBisVonZiilProcesLeeseWartezaitMili);
					}

					if (null != NaacZiilProcessWirkung)
					{
						NaacWirkungNääxteVonZiilProcessLeeseBeginZaitScrankeMili =
							Bib3.Glob.Max(
							NaacWirkungNääxteVonZiilProcessLeeseBeginZaitScrankeMili,
							NaacZiilProcessWirkung.EndeZaitMili + (VonWirkungBisVonZiilProcesLeeseWartezaitMili ?? 0));
					}
				}
			}

			var FolgendeScritVonZiilProcessLeeseBeginZaitScrankeMinMili = Bib3.Glob.Max(
				NaacVonZiilProcessLeeseNääxteVonZiilProcessLeeseBeginZaitScrankeMili,
				NaacWirkungNääxteVonZiilProcessLeeseBeginZaitScrankeMili);

			return FolgendeScritVonZiilProcessLeeseBeginZaitScrankeMinMili;
		}
	}
}
