using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bib3;
using Newtonsoft.Json;
using ExtractFromOldAssembly.Bib3;

namespace Optimat.EveOnline.Anwendung.Berict.Auswert
{
	public struct SictBerictDataiIdentInDsInfo
	{
		readonly public string DataiPfaad;

		/// <summary>
		/// Verwendet fals Datai ain Zip-Archive.
		/// </summary>
		readonly public string InArchiveEntryPfaad;

		public SictBerictDataiIdentInDsInfo(
			string DataiPfaad,
			string InArchiveEntryPfaad)
		{
			this.DataiPfaad = DataiPfaad;
			this.InArchiveEntryPfaad = InArchiveEntryPfaad;
		}
	}

	public class SictBerictListeScritDiferenzAuswert
	{
		readonly Bib3.RefNezDiferenz.SictRefNezSume BerictSictSume = new Bib3.RefNezDiferenz.SictRefNezSume(
			Optimat.ScpezEveOnln.SictAutomat.ZuusctandSictDiferenzSictParam);

		public bool ZiilMemberExistentNictIgnoriire = true;

		readonly public Int64 BeginBerictScritIndex;

		public Int64? ScritVerwertetLezteIndex
		{
			private set;
			get;
		}

		readonly List<SictWertMitZait<Optimat.ScpezEveOnln.SictAutomatZuusctand>> InternListeZuZaitAutomaatZuusctand =
			new List<SictWertMitZait<Optimat.ScpezEveOnln.SictAutomatZuusctand>>();

		public IEnumerable<SictWertMitZait<Optimat.ScpezEveOnln.SictAutomatZuusctand>> ListeZuZaitAutomaatZuusctand
		{
			get
			{
				return InternListeZuZaitAutomaatZuusctand.ToArray();
			}
		}

		public SictBerictListeScritDiferenzAuswert(
			Int64 BeginBerictScritIndex,
			bool ZiilMemberExistentNictIgnoriire)
		{
			this.BeginBerictScritIndex = BeginBerictScritIndex;
			this.ZiilMemberExistentNictIgnoriire = ZiilMemberExistentNictIgnoriire;
		}

		public void Berecne(
			Func<Int64, SictBerictDataiIdentInDsInfo> FunkZuBerictScritIndexDataiIdent,
			Func<string, byte[]> FunkZuDataiPfaadDataiInhalt,
			//	Func<Int64, SictWertMitZait<Optimat.Anwendung.RefNezDiferenz.SictRefNezDiferenzScritSictJson>> FunkZuBerictScritIndexBerictScritBerecne,
			Int64 BerecnungEndeAnwendungZaitMili)
		{
			while (true)
			{
				var ScritLezteAutomaatZuusctand = InternListeZuZaitAutomaatZuusctand.LastOrDefault().Wert;

				if (null != ScritLezteAutomaatZuusctand)
				{
					if (BerecnungEndeAnwendungZaitMili <= ScritLezteAutomaatZuusctand.NuzerZaitMili)
					{
						break;
					}
				}

				if (null == FunkZuBerictScritIndexDataiIdent)
				{
					return;
				}

				if (null == FunkZuDataiPfaadDataiInhalt)
				{
					return;
				}

				var ScritIndex = (ScritVerwertetLezteIndex + 1) ?? BeginBerictScritIndex;

				var BerictScritDataiIdent = FunkZuBerictScritIndexDataiIdent(ScritIndex);

				if (null == BerictScritDataiIdent.DataiPfaad)
				{
					return;
				}

				var DataiInhalt = FunkZuDataiPfaadDataiInhalt(BerictScritDataiIdent.DataiPfaad);

				using (var ZipArchive = new System.IO.Compression.ZipArchive(new MemoryStream(DataiInhalt), System.IO.Compression.ZipArchiveMode.Read))
				{
					var MengeEntry = ZipArchive.Entries;

					if (null != MengeEntry)
					{
						foreach (var Entry in MengeEntry)
						{
							using (var EntryStream = Entry.Open())
							{
								var EntryInhalt = Optimat.Glob.AusStreamLeese(EntryStream);

								var EntrySictUTF8 = Encoding.UTF8.GetString(EntryInhalt);

								var EntrySictSctrukt = SictBerictAusDsAuswert.AusStringVonAnwendungBerictBerecne(EntrySictUTF8);

								//	!!!! Hiir werd davon ausgegange das MengeAutomaatZuusctandSictSeriel jewails komplete Zuusctand (Diferenz von 0) enthalte.

								var MengeAutomaatZuusctandSictSeriel =
									EntrySictSctrukt.MengeAutomaatZuusctandMitZait ??
									EntrySictSctrukt.MengeAutomaatZuusctandDiferenzScritMitZait;

								if (null != MengeAutomaatZuusctandSictSeriel)
								{
									foreach (var AutomaatZuusctandSictSerielMitZait in MengeAutomaatZuusctandSictSeriel)
									{
										var AutomaatZuusctandSictSeriel = AutomaatZuusctandSictSerielMitZait.Wert;

										var ScritIndexNulbar = AutomaatZuusctandSictSeriel.ScritIndex;

										if (!ScritIndexNulbar.HasValue)
										{
											continue;
										}

										if (ScritVerwertetLezteIndex.HasValue)
										{
											var AutomaatZuusctandBerictScritErwartetIndex = (ScritVerwertetLezteIndex + 1) ?? 0;

											if (!(AutomaatZuusctandBerictScritErwartetIndex == ScritIndexNulbar))
											{
												continue;
											}
										}

										ScritVerwertetLezteIndex = ScritIndexNulbar;

										Optimat.ScpezEveOnln.SictAutomatZuusctand AutomaatZuusctandKopii = null;

										try
										{
											var AutomaatZuusctandBerictScritDiferenz = AutomaatZuusctandSictSeriel;

											var ScritSume =
												BerictSictSume.BerecneScritSumeListeWurzelRefClrUndErfolg(
												AutomaatZuusctandBerictScritDiferenz,
												ZiilMemberExistentNictIgnoriire);

											if (null == ScritSume)
											{
												continue;
											}

											if (!ScritSume.Volsctändig)
											{
												continue;
											}

											var ScritSumeListeObjektReferenzClr = ScritSume.ListeWurzelRefClr;

											if (null == ScritSumeListeObjektReferenzClr)
											{
												continue;
											}

											var AutomaatZuusctand =
												(Optimat.ScpezEveOnln.SictAutomatZuusctand)
												ScritSumeListeObjektReferenzClr.FirstOrDefault();

											if (null == AutomaatZuusctand)
											{
												continue;
											}

											/*
											 * 2015.00.04
											 * 
											 * Kopii nit meer durc SictRefNezKopii mööglic da Dictionary<,> verwendet werd.
											 * 
									AutomaatZuusctandKopii = SictRefNezKopii.ObjektKopiiErsctele(AutomaatZuusctand, null, null);
											 * */

											/*
											 * 2015.00.06
											 * 
											 * Kopii wiider durc SictRefNezKopii mööglic da SictRefNezKopii naacgerüsct wurd um Verhalte von TypeBehandlungRictliinie RefNezDif zu verwende.
											 * 
											AutomaatZuusctandKopii = RefNezDiferenz.SictRefNezSume.ObjektKopiiBerecne(AutomaatZuusctand,
												RefNezDiferenz.NewtonsoftJson.SictMengeTypeBehandlungRictliinieNewtonsoftJson.KonstruktSictParam());
											 * */

											AutomaatZuusctandKopii = SictRefNezKopii.ObjektKopiiErsctele(AutomaatZuusctand, null, null);
										}
										finally
										{
											InternListeZuZaitAutomaatZuusctand.Add(
												new SictWertMitZait<Optimat.ScpezEveOnln.SictAutomatZuusctand>(
													AutomaatZuusctandSictSerielMitZait.Zait, AutomaatZuusctandKopii));
										}
									}
								}
							}
						}
					}
				}
			}
		}
	}

	public class SictBerictAusDsAuswert : Optimat.EveOnline.Berict.Auswert.SictBerictInspektDaatenKweleAusScpaicer
	{
		readonly object Lock = new object();

		public string BerictHauptVerzaicnisPfaad;

		public string BerictWindowClientRasterVerzaicnisPfaad;

		public int WorkaroundAusBerictBeginListeScritAnzaalUnvolsctändigAnzaal;

		public bool ZiilMemberExistentNictIgnoriire = true;

		public Int64 ScatenscpaicerZaitraumGrenzeBeginMili;
		public Int64 ScatenscpaicerZaitraumGrenzeEndeMili;

		/// <summary>
		/// Ainhait=Oktet.
		/// </summary>
		public Int64 ScatenscpaicerBerictDataiKapazitäätScranke;

		public Int64 ScatenscpaicerScritZuusctandAnzaalScrankeMax;

		/// <summary>
		/// Ainhait=Bildpunkte.
		/// </summary>
		public Int64 ScatenscpaicerWindowClientRasterKapazitäätScranke;

		/// <summary>
		/// Key: Index des Scrit aus Berict von Anwendung
		/// Value.Key: Server.Anwendung.Zaitpunkt.Mili
		/// Value.Value: Pfaad zur Datai welce den Zuusctand zu diisem Zaitpunkt bescraibt.
		/// 
		/// Werd nii geleert.
		/// </summary>
		readonly IDictionary<Int64, SictWertMitZait<SictBerictDataiIdentInDsInfo>> DictVonAnwendungBerictScritIndexPfaadZuAutomaatZuusctandInfo =
			new Dictionary<Int64, SictWertMitZait<SictBerictDataiIdentInDsInfo>>();

		readonly IDictionary<Int64, SictOptimatScrit> DictZuAutomaatZaitMiliOptimatScrit =
			new Dictionary<Int64, SictOptimatScrit>();

		readonly SictScatenscpaicerDict<Int64, Optimat.ScpezEveOnln.SictAutomatZuusctand> DictVonAutomaatZaitMiliAutomaatZuusctand =
			new SictScatenscpaicerDict<Int64, Optimat.ScpezEveOnln.SictAutomatZuusctand>();

		/// <summary>
		/// Verwendet di Annaame das Inhalt der Datai nict geändert werd.
		/// </summary>
		readonly SictScatenscpaicerDict<string, byte[]> DictZuDataiPfaadDataiInhalt = new SictScatenscpaicerDict<string, byte[]>();

		readonly IDictionary<string, int> DictZuDataiPfaadDataiVerwertungVersuucAnzaal = new Dictionary<string, int>();

		readonly IDictionary<string, bool> DictZuDataiPfaadDataiVerwertungErfolg = new Dictionary<string, bool>();

		/// <summary>
		/// Lezte berictete Zuusctand zu Mission. Werd nii geleert.
		/// </summary>
		readonly IDictionary<Int64, SictWertMitZait<SictMissionZuusctand>> DictVonMissionBezaicnerMissionZuusctandLezte =
			new Dictionary<Int64, SictWertMitZait<SictMissionZuusctand>>();

		readonly IDictionary<Int64, Int64> DictZuNuzerZaitMiliAnwendungZaitMili = new Dictionary<Int64, Int64>();

		public Int64[] MengeAutomaatZuusctandScnapscusAngefordertZaitMili;

		SictBerictListeScritDiferenzAuswert MengeAutomaatZuusctandScnapscusAngefordertAuswert;

		public Int64[] AnwendungMengeScnapscusZaitMili
		{
			private set;
			get;
		}

		public Int64? ListeDataiAnzaal
		{
			private set;
			get;
		}

		byte[] InhaltAusDataiMitPfaad(string DataiPfaad)
		{
			return DictZuDataiPfaadDataiInhalt.ValueFürKey(DataiPfaad, Bib3.Glob.InhaltAusDataiMitPfaad);
		}

		public SictOptimatScrit ZuAutomaatZaitMiliNääxteOptimatScrit(Int64 SuuceUrsprungAutomaatZaitMili)
		{
			var DictZuAutomaatZaitMiliOptimatScrit = this.DictZuAutomaatZaitMiliOptimatScrit;

			lock (Lock)
			{
				if (null == DictZuAutomaatZaitMiliOptimatScrit)
				{
					return null;
				}

				var BisherNääxte = default(KeyValuePair<SictOptimatScrit, Int64>);

				foreach (var ZuAutomaatZaitMiliOptimatScrit in DictZuAutomaatZaitMiliOptimatScrit)
				{
					if (null == ZuAutomaatZaitMiliOptimatScrit.Value)
					{
						continue;
					}

					var Distanz = SuuceUrsprungAutomaatZaitMili - ZuAutomaatZaitMiliOptimatScrit.Key;

					if (null == BisherNääxte.Key ||
						Math.Abs(Distanz) < Math.Abs(BisherNääxte.Value))
					{
						BisherNääxte = new KeyValuePair<SictOptimatScrit, Int64>(ZuAutomaatZaitMiliOptimatScrit.Value, Distanz);
					}
				}

				return BisherNääxte.Key;
			}
		}

		public KeyValuePair<SictWertMitZait<Optimat.ScpezEveOnln.SictAutomatZuusctand>, int>[]
			ZuAutomaatZaitMiliListeScnapscusNääxte(
			Int64 SuuceUrscprungAnwendungZaitMili,
			int RükwärtsListeScnapscusAnzaal = 0,
			int VorwärtsListeScnapscusAnzaal = 0,
			bool ScnapscusAutomaatZuusctandLaade = false)
		{
			var ListeOptimatScritMitIndexRelatiiv =
				base.ZuNuzerZaitMiliListeScritInfoNääxte(
				SuuceUrscprungAnwendungZaitMili,
				RükwärtsListeScnapscusAnzaal,
				VorwärtsListeScnapscusAnzaal,
				ScnapscusAutomaatZuusctandLaade);

			/*
			 * 2014.10.17
			 * 
			var	ListeOptimatScritZaitMiliMitIndexRelatiiv	=
				ListeOptimatScritMitIndexRelatiiv
				.Select((OptimatScritMitIndexRelatiiv) => new	KeyValuePair<Int64,	int>(
					OptimatScritMitIndexRelatiiv.Key.VonProcessLeeseBeginZait	?? -1,
					OptimatScritMitIndexRelatiiv.Value))
				.ToArray();
			 * */

			var ListeOptimatScritZaitMiliMitIndexRelatiiv =
				ListeOptimatScritMitIndexRelatiiv
				.Select((OptimatScritMitIndexRelatiiv) => new KeyValuePair<Int64, int>(
					OptimatScritMitIndexRelatiiv.Key.NuzerZait,
					OptimatScritMitIndexRelatiiv.Value))
				.ToArray();

			lock (this.Lock)
			{
				var FunkFürAnwendungZaitMiliScnapscusLaade =
					ScnapscusAutomaatZuusctandLaade ?
					new Func<Int64, Optimat.ScpezEveOnln.SictAutomatZuusctand>(AutomaatZuusctandZuZaitMiliLaade) :
					null;

				FunkFürAnwendungZaitMiliScnapscusLaade = null;

				var ListeScnapscus =
					ListeOptimatScritZaitMiliMitIndexRelatiiv
					.Select((ScnapcusZaitMili) =>
						new KeyValuePair<SictWertMitZait<Optimat.ScpezEveOnln.SictAutomatZuusctand>, int>(
						new SictWertMitZait<Optimat.ScpezEveOnln.SictAutomatZuusctand>(
						ScnapcusZaitMili.Key,
						DictVonAutomaatZaitMiliAutomaatZuusctand.ValueFürKey(ScnapcusZaitMili.Key, FunkFürAnwendungZaitMiliScnapscusLaade)),
						ScnapcusZaitMili.Value))
					.ToArray();

				return ListeScnapscus;
			}
		}

		Optimat.ScpezEveOnln.SictAutomatZuusctand AutomaatZuusctandZuZaitMiliLaade(
			Int64 ScnapcusAnwendungZaitMili)
		{
			var AutomaaZaitMiliUndPfaad =
				DictVonAnwendungBerictScritIndexPfaadZuAutomaatZuusctandInfo
				.FirstOrDefault((Kandidaat) => Kandidaat.Value.Zait == ScnapcusAnwendungZaitMili);

			var Pfaad = AutomaaZaitMiliUndPfaad.Value.Wert;

			if (null == Pfaad.DataiPfaad)
			{
				return null;
			}

			var DataiInhalt = this.InhaltAusDataiMitPfaad(Pfaad.DataiPfaad);

			var SerializeSettings = new JsonSerializerSettings();

			SerializeSettings.DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate;
			SerializeSettings.TypeNameHandling = TypeNameHandling.Auto;
			SerializeSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
			SerializeSettings.PreserveReferencesHandling = PreserveReferencesHandling.All;

			if (null == Pfaad.InArchiveEntryPfaad)
			{
				throw new NotImplementedException();
			}
			else
			{
				using (var ZipArchive = new System.IO.Compression.ZipArchive(new MemoryStream(DataiInhalt), System.IO.Compression.ZipArchiveMode.Read))
				{
					var MengeEntry = ZipArchive.Entries;

					if (null != MengeEntry)
					{
						foreach (var Entry in MengeEntry)
						{
							if (!string.Equals(Entry.FullName, Pfaad.InArchiveEntryPfaad))
							{
								continue;
							}

							using (var EntryStream = Entry.Open())
							{
								var EntryInhalt = Optimat.Glob.AusStreamLeese(EntryStream, null, 0x400000);

								var EntrySictUTF8 = Encoding.UTF8.GetString(EntryInhalt);

								var EntrySictSctrukt = AusStringVonAnwendungBerictBerecne(EntrySictUTF8);

								var MengeAutomaatZuusctandSictDiferenzMitZait = EntrySictSctrukt.MengeAutomaatZuusctandMitZait;

								if (null != MengeAutomaatZuusctandSictDiferenzMitZait)
								{
									foreach (var AutomaatZuusctandSictDiferenzMitZait in MengeAutomaatZuusctandSictDiferenzMitZait)
									{
										if (AutomaatZuusctandSictDiferenzMitZait.Zait != ScnapcusAnwendungZaitMili)
										{
											continue;
										}

										var AutomaatZuusctandSictDiferenz = AutomaatZuusctandSictDiferenzMitZait.Wert;

										if (null == AutomaatZuusctandSictDiferenz)
										{
											continue;
										}

										var AutomaatZuusctand =
											(Optimat.ScpezEveOnln.SictAutomatZuusctand)
											Bib3.RefNezDiferenz.Extension.ListeWurzelDeserialisiire(AutomaatZuusctandSictDiferenz)
											.FirstOrDefaultNullable();

										return AutomaatZuusctand;
									}
								}
							}
						}
					}
				}
			}

			return null;
		}

		static public SictVonAnwendungBerict AusStringVonAnwendungBerictBerecne(
			string VonAnwendungBerictSictString)
		{
			if (null == VonAnwendungBerictSictString)
			{
				return null;
			}

			return JsonConvert.DeserializeObject<SictVonAnwendungBerict>(VonAnwendungBerictSictString);
		}

		Bib3.RefNezDiferenz.SictRefNezSume AutomaatZuusctandBerictSictSume = new Bib3.RefNezDiferenz.SictRefNezSume(
			Optimat.ScpezEveOnln.SictAutomat.ZuusctandSictDiferenzSictParam);

		public IReadOnlyDictionary<Type, Bib3.RefNezDiferenz.SictRefNezSumeProfileZuType> DictProfileZuTypeBerecne()
		{
			return AutomaatZuusctandBerictSictSume.DictProfileZuTypeBerecne();
		}

		public Int64? AutomaatZuusctandBerictScritLezteIndex
		{
			private set;
			get;
		}

		public KeyValuePair<Int64, Optimat.ScpezEveOnln.SictAutomatZuusctand>[] BerecneLezteListeAutomaatZuusctand
		{
			private set;
			get;
		}

		public void Berecne(
			out System.Exception LaadeDataiException,
			int? VerwertungMengeDataiAnzaalScranke)
		{
			LaadeDataiException = null;

			var BerictVerzaicnisPfaad = this.BerictHauptVerzaicnisPfaad;
			var BerictWindowClientRasterVerzaicnisPfaad = this.BerictWindowClientRasterVerzaicnisPfaad;

			var MengeAutomaatZuusctandScnapscusAngefordertZaitMili = this.MengeAutomaatZuusctandScnapscusAngefordertZaitMili;

			var MengeAutomaatZuusctandScnapscusAngefordertAuswert = this.MengeAutomaatZuusctandScnapscusAngefordertAuswert;

			var WorkaroundAusBerictBeginListeScritAnzaalUnvolsctändigAnzaal = this.WorkaroundAusBerictBeginListeScritAnzaalUnvolsctändigAnzaal;

			var BerecneLezteListeAutomaatZuusctand = new List<KeyValuePair<Int64, Optimat.ScpezEveOnln.SictAutomatZuusctand>>();

			try
			{
				if (null != MengeAutomaatZuusctandScnapscusAngefordertZaitMili)
				{
					var MengeAutomaatZuusctandScnapscusVorhandeZaitMili = DictVonAutomaatZaitMiliAutomaatZuusctand.MengeKeyBerecne();

					var MengeAutomaatZuusctandScnapscusNocZuLaadeZaitMili =
						MengeAutomaatZuusctandScnapscusAngefordertZaitMili
						.Where((KandidaatNocZuLaade) => !MengeAutomaatZuusctandScnapscusVorhandeZaitMili.Contains(KandidaatNocZuLaade)).ToArray();

					if (0 < MengeAutomaatZuusctandScnapscusNocZuLaadeZaitMili.Length)
					{
						var AutomaatZuusctandScnapscusNocZuLaadeZaitMili = MengeAutomaatZuusctandScnapscusNocZuLaadeZaitMili.OrderBy((t) => t).FirstOrDefault();

						var BeginBerictScritIndex =
							Math.Max(0,
							DictVonAnwendungBerictScritIndexPfaadZuAutomaatZuusctandInfo
							.LastOrDefault((Kandidaat) => Kandidaat.Value.Zait <= AutomaatZuusctandScnapscusNocZuLaadeZaitMili).Key -
							WorkaroundAusBerictBeginListeScritAnzaalUnvolsctändigAnzaal - 4);

						var AuswertErhalte = false;

						if (null != MengeAutomaatZuusctandScnapscusAngefordertAuswert)
						{
							var BeginScritDistanzVonBisherAuswert = BeginBerictScritIndex - MengeAutomaatZuusctandScnapscusAngefordertAuswert.ScritVerwertetLezteIndex;

							if (MengeAutomaatZuusctandScnapscusAngefordertAuswert.ListeZuZaitAutomaatZuusctand.LastOrDefault().Zait < AutomaatZuusctandScnapscusNocZuLaadeZaitMili &&
								BeginScritDistanzVonBisherAuswert < 10)
							{
								AuswertErhalte = true;
							}
						}

						if (!AuswertErhalte)
						{
							this.MengeAutomaatZuusctandScnapscusAngefordertAuswert = MengeAutomaatZuusctandScnapscusAngefordertAuswert =
								new SictBerictListeScritDiferenzAuswert(BeginBerictScritIndex, ZiilMemberExistentNictIgnoriire);
						}

						MengeAutomaatZuusctandScnapscusAngefordertAuswert.Berecne(
							(ScritIndex) => Optimat.Glob.TAD(this.DictVonAnwendungBerictScritIndexPfaadZuAutomaatZuusctandInfo, ScritIndex).Wert,
							InhaltAusDataiMitPfaad,
							AutomaatZuusctandScnapscusNocZuLaadeZaitMili);
					}
				}

				if (null != MengeAutomaatZuusctandScnapscusAngefordertAuswert)
				{
					var ListeZuZaitAutomaatZuusctand = MengeAutomaatZuusctandScnapscusAngefordertAuswert.ListeZuZaitAutomaatZuusctand;

					if (null != ListeZuZaitAutomaatZuusctand)
					{
						var ListeZuZaitAutomaatZuusctandVermuutungVolsctändig = ListeZuZaitAutomaatZuusctand;

						if (0 < MengeAutomaatZuusctandScnapscusAngefordertAuswert.BeginBerictScritIndex)
						{
							ListeZuZaitAutomaatZuusctandVermuutungVolsctändig =
								ListeZuZaitAutomaatZuusctand.Skip(WorkaroundAusBerictBeginListeScritAnzaalUnvolsctändigAnzaal);
						}

						foreach (var ZuZaitAutomaatZuusctand in ListeZuZaitAutomaatZuusctandVermuutungVolsctändig)
						{
							if (null != ZuZaitAutomaatZuusctand.Wert)
							{
								var AutomaatScritIdentZaitMili = (Int64?)ZuZaitAutomaatZuusctand.Wert.NuzerZaitMili;

								if (!AutomaatScritIdentZaitMili.HasValue)
								{
									continue;
								}

								DictVonAutomaatZaitMiliAutomaatZuusctand.AintraagErsctele(AutomaatScritIdentZaitMili.Value, ZuZaitAutomaatZuusctand.Wert);
							}
						}
					}
				}

				try
				{
					if (!(VerwertungMengeDataiAnzaalScranke <= 0) &&
						null != BerictVerzaicnisPfaad)
					{
						/*
						 * Ermitelt ob noie Dataie vorhande und verwertet diise gegeebenenfals.
						 * */

						var BerictVerzaicnis = new DirectoryInfo(BerictVerzaicnisPfaad);

						var MengeFile = BerictVerzaicnis.GetFiles();

						var ListeFile =
							MengeFile
							.OrderBy((KandidaatFile) => KandidaatFile.Name)
							.ToArray();

						int VerwertungMengeDataiAnzaalBisher = 0;

						foreach (var File in ListeFile)
						{
							if (VerwertungMengeDataiAnzaalScranke <= VerwertungMengeDataiAnzaalBisher)
							{
								break;
							}

							var DataiPfaad = File.FullName;

							var DataiBeraitsVerwertet = Optimat.Glob.TAD(DictZuDataiPfaadDataiVerwertungErfolg, DataiPfaad);

							if (DataiBeraitsVerwertet)
							{
								continue;
							}

							int DataiVerwertungVersuucAnzaal = 0;

							DictZuDataiPfaadDataiVerwertungVersuucAnzaal.TryGetValue(DataiPfaad, out DataiVerwertungVersuucAnzaal);

							if (3 < DataiVerwertungVersuucAnzaal)
							{
								continue;
							}

							DictZuDataiPfaadDataiVerwertungVersuucAnzaal[DataiPfaad] = 1 + DataiVerwertungVersuucAnzaal;

							var DataiInhalt = InhaltAusDataiMitPfaad(DataiPfaad);

							if (null == DataiInhalt)
							{
								continue;
							}

							using (var ZipArchive = new System.IO.Compression.ZipArchive(new MemoryStream(DataiInhalt), System.IO.Compression.ZipArchiveMode.Read))
							{
								var MengeEntry = ZipArchive.Entries;

								if (null != MengeEntry)
								{
									foreach (var Entry in MengeEntry)
									{
										var EntryIdent = new SictBerictDataiIdentInDsInfo(DataiPfaad, Entry.FullName);

										using (var EntryStream = Entry.Open())
										{
											var EntryInhalt = Optimat.Glob.AusStreamLeese(EntryStream);

											var EntrySictUTF8 = Encoding.UTF8.GetString(EntryInhalt);

											var EntrySictSctrukt = AusStringVonAnwendungBerictBerecne(EntrySictUTF8);

											//	!!!! Hiir werd davon ausgegange das MengeAutomaatZuusctandSictSeriel jewails komplete Zuusctand (Diferenz von 0) enthalte.

											var MengeAutomaatZuusctandSictSeriel =
												EntrySictSctrukt.MengeAutomaatZuusctandMitZait ??
												EntrySictSctrukt.MengeAutomaatZuusctandDiferenzScritMitZait;

											if (null != MengeAutomaatZuusctandSictSeriel)
											{
												foreach (var AutomaatZuusctandSictSeriel in MengeAutomaatZuusctandSictSeriel)
												{
													var ScritIndexNulbar = AutomaatZuusctandSictSeriel.Wert.ScritIndex;

													if (!ScritIndexNulbar.HasValue)
													{
														continue;
													}

													var ScritIndex = ScritIndexNulbar.Value;

													var AutomaatZuusctandBerictScritErwartetIndex = (AutomaatZuusctandBerictScritLezteIndex + 1) ?? 0;

													if (!(AutomaatZuusctandBerictScritErwartetIndex == ScritIndex))
													{
														continue;
													}

													AutomaatZuusctandBerictScritLezteIndex = ScritIndex;

													var AutomaatZuusctandBerictScritDiferenz = AutomaatZuusctandSictSeriel;

													var ScritSume =
														AutomaatZuusctandBerictSictSume.BerecneScritSumeListeWurzelRefClrUndErfolg(
														AutomaatZuusctandBerictScritDiferenz.Wert,
														ZiilMemberExistentNictIgnoriire);

													if (null == ScritSume)
													{
														continue;
													}

													var ScritSumeListeObjekt = ScritSume.ListeWurzelRefClr;

													//	!!!!	zu erseze durc entferne taatsäclic nit meer benöötigte.
													AutomaatZuusctandBerictSictSume.MengeObjektInfoEntferneÄltere(ScritIndex - 44);

													if (null == ScritSumeListeObjekt)
													{
														continue;
													}

													var AutomaatZuusctand =
														(Optimat.ScpezEveOnln.SictAutomatZuusctand)
														ScritSumeListeObjekt.FirstOrDefault();

													if (null == AutomaatZuusctand)
													{
														continue;
													}

													var AutomaatScritIdentZaitMili = (Int64?)AutomaatZuusctand.NuzerZaitMili;

													if (!AutomaatScritIdentZaitMili.HasValue)
													{
														continue;
													}

													/*
													 * 2015.00.04
													 * 
													 * Kopii nit meer durc SictRefNezKopii mööglic da Dictionary<,> verwendet werd.
													 * 
													var AutomaatZuusctandKopii = SictRefNezKopii.ObjektKopiiErsctele(AutomaatZuusctand, null, null);
													 * */

													/*
													 * 2015.00.06
													 * 
													 * Kopii wiider durc SictRefNezKopii mööglic da SictRefNezKopii naacgerüsct wurd um Verhalte von TypeBehandlungRictliinie RefNezDif zu verwende.

															var AutomaatZuusctandKopii = RefNezDiferenz.SictRefNezSume.ObjektKopiiBerecne(AutomaatZuusctand,
																RefNezDiferenz.NewtonsoftJson.SictMengeTypeBehandlungRictliinieNewtonsoftJson.KonstruktSictParam());
													 * */
													var AutomaatZuusctandKopii = SictRefNezKopii.ObjektKopiiErsctele(AutomaatZuusctand, null, null);

													BerecneLezteListeAutomaatZuusctand.Add(new KeyValuePair<Int64, Optimat.ScpezEveOnln.SictAutomatZuusctand>(
														AutomaatScritIdentZaitMili.Value,
														AutomaatZuusctandKopii));

													DictVonAnwendungBerictScritIndexPfaadZuAutomaatZuusctandInfo[ScritIndexNulbar.Value] =
														new SictWertMitZait<SictBerictDataiIdentInDsInfo>(AutomaatScritIdentZaitMili.Value, EntryIdent);

													ZuAutomaatZuusctandVonZaitMiliAblaitungScpaicere(
														new SictWertMitZait<Optimat.ScpezEveOnln.SictAutomatZuusctand>(
														AutomaatZuusctandSictSeriel.Zait, AutomaatZuusctandKopii));

													if (DictVonAutomaatZaitMiliAutomaatZuusctand.Count < ScatenscpaicerScritZuusctandAnzaalScrankeMax)
													{
														DictVonAutomaatZaitMiliAutomaatZuusctand.AintraagErsctele(AutomaatScritIdentZaitMili.Value, AutomaatZuusctandKopii);
													}

												}
											}
										}
									}
								}
							}

							DictZuDataiPfaadDataiVerwertungErfolg[DataiPfaad] = true;

							++VerwertungMengeDataiAnzaalBisher;
						}

					}
				}
				catch (System.Exception Exception)
				{
					LaadeDataiException = Exception;
				}

				{
					//	Scatescpaicer Scranke apliziire.

					DictZuDataiPfaadDataiInhalt.BescrankeEntferneLängerNitVerwendete(
						null,
						ScatenscpaicerBerictDataiKapazitäätScranke,
						(DataiInhalt) => 1000 + ((null == DataiInhalt) ? 0 : DataiInhalt.LongLength));

					DictVonAutomaatZaitMiliAutomaatZuusctand.BescrankeEntferneLängerNitVerwendete(
						ScatenscpaicerScritZuusctandAnzaalScrankeMax);
				}

				{
					AnwendungMengeScnapscusZaitMili =
						DictVonAnwendungBerictScritIndexPfaadZuAutomaatZuusctandInfo
						.Select((t) => t.Value.Zait).ToArray();

					ListeDataiAnzaal = DictZuDataiPfaadDataiVerwertungErfolg.Count;
				}
			}
			finally
			{
				this.BerecneLezteListeAutomaatZuusctand = BerecneLezteListeAutomaatZuusctand.ToArray();
			}
		}

		/// <summary>
		/// Extrahiirt Info aus AutomaatZuusctand und scpaicert diise naac Dict.
		/// </summary>
		/// <param name="AutomaatZuusctand"></param>
		void ZuAutomaatZuusctandVonZaitMiliAblaitungScpaicere(
			SictWertMitZait<Optimat.ScpezEveOnln.SictAutomatZuusctand> AutomaatZuusctand)
		{
			if (null == AutomaatZuusctand.Wert)
			{
				return;
			}

			var ReferenzZait = AutomaatZuusctand.Wert.NuzerZaitMili;

			var TempVonNuzerListeBerictWindowClientRaster = AutomaatZuusctand.Wert.TempVonNuzerListeBerictWindowClientRaster;

			var ScnapscusAuswertungErgeebnis = AutomaatZuusctand.Wert.ListeScnapscusLezteAuswertungErgeebnisNaacSimu;

			var ListeZuZaitVonNuzerMeldungNaacProcessWirkung = AutomaatZuusctand.Wert.ListeZuZaitVonNuzerMeldungNaacProcessWirkung;

			if (null != ListeZuZaitVonNuzerMeldungNaacProcessWirkung)
			{
				base.VonNuzerMeldungNaacProcessWirkungFüügeAin(
					ListeZuZaitVonNuzerMeldungNaacProcessWirkung
					.Select((Element) => Element.Wert));
			}

			var FittingUndShipZuusctand = AutomaatZuusctand.Wert.FittingUndShipZuusctand;

			/*
			 * 2014.10.15
			 * 
			var OptimatScritAktuel = AutomaatZuusctand.Wert.OptimatScritAktuel;

			var OptimatScritVonProcessLeese =
				(null	== OptimatScritAktuel)	?	null	:
				OptimatScritAktuel.VonZiilProcessLeeseSictAuswert ?? OptimatScritAktuel.VonZiilProcessLeese;

			var OptimatScritVonProcessLeeseBeginZaitNulbar = (null == OptimatScritVonProcessLeese) ? null : OptimatScritVonProcessLeese.BeginZait;

			if (!OptimatScritVonProcessLeeseBeginZaitNulbar.HasValue)
			{
				//	Oone zaitlice Ainordnung werd nix gescpaicert.
				return;
			}

			var OptimatScritVonProcessLeeseBeginZait = OptimatScritVonProcessLeeseBeginZaitNulbar.Value;
			 * */

			var OptimatScritAktuel = new SictOptimatScrit(AutomaatZuusctand.Wert.NuzerZaitMili, null);

			var OptimatScritAbbild = SictOptimatScrit.OptimatScritSictFürBerict(OptimatScritAktuel, false);

			OptimatScritAbbild.AnwendungZaitMili = OptimatScritAbbild.AnwendungZaitMili ?? AutomaatZuusctand.Wert.NuzerZaitMili;

			/*
			 * 2014.10.15
			 * 
			if (null != OptimatScritAktuel)
			{
				if (null != OptimatScritVonProcessLeese)
				{
					var VonProcessLeeseBeginZaitMili = OptimatScritVonProcessLeese.BeginZaitMili;

					if (VonProcessLeeseBeginZaitMili.HasValue)
					{
						DictZuNuzerZaitMiliAnwendungZaitMili[VonProcessLeeseBeginZaitMili.Value] = AutomaatZuusctand.Zait;
					}
				}
			}
			 * */

			if (null != OptimatScritAktuel)
			{
				DictZuNuzerZaitMiliAnwendungZaitMili[ReferenzZait] = AutomaatZuusctand.Zait;
			}

			if (null != TempVonNuzerListeBerictWindowClientRaster)
			{
				foreach (var VonNuzerBerictWindowClientRaster in TempVonNuzerListeBerictWindowClientRaster)
				{
					base.NuzerZaitMiliWindowClientRasterSuuceHinwaisFüügeAin(VonNuzerBerictWindowClientRaster.Zait, VonNuzerBerictWindowClientRaster.Wert);
				}
			}

			if (null != OptimatScritAbbild)
			{
				OptimatScritAbbild.AnwendungSizungIdent = AutomaatZuusctand.Wert.AnwendungSizungIdent;

				var AutomaatZuusctandMengeZuClrTypeMengeInstanceAnzaal =
					Bib3.RefNezDiferenz.SictRefNezDiferenz.ZuRefNezMengeClrTypeInstanceAnzaalBerecne(
					new object[] { AutomaatZuusctand.Wert },
					Optimat.ScpezEveOnln.SictAutomat.ZuusctandSictDiferenzSictParam.TypeBehandlungRictliinieMitScatescpaicer);

				/*
				2015.09.06

				OptimatScritAbbild.AutomaatZuusctandMengeZuClrTypeMengeInstanceAnzaal =
					AutomaatZuusctandMengeZuClrTypeMengeInstanceAnzaal.ToArrayNullable();
				*/

				var ScritNaacNuzerBerictAutomaatZuusctand = OptimatScritAbbild.NaacNuzerBerictAutomaatZuusctand;

				if (null == ScritNaacNuzerBerictAutomaatZuusctand)
				{
					//	OptimatScritAbbild.NaacNuzerBerictAutomaatZuusctand = ScritNaacNuzerBerictAutomaatZuusctand = new SictNaacNuzerBerictAutomaatZuusctand();

					OptimatScritAbbild.NaacNuzerBerictAutomaatZuusctand = ScritNaacNuzerBerictAutomaatZuusctand = AutomaatZuusctand.Wert.NaacNuzerMeldungZuusctand;
				}
			}

			lock (Lock)
			{
				DictZuAutomaatZaitMiliOptimatScrit[ReferenzZait] = OptimatScritAbbild;
			}

			base.ZuScritInfoFüügeAin(OptimatScritAbbild);

			var AgentUndMissionInfo = AutomaatZuusctand.Wert.AgentUndMission;

			if (null != AgentUndMissionInfo)
			{
				var MengeMission = AgentUndMissionInfo.MengeMission;

				if (null != MengeMission)
				{
					foreach (var Mission in MengeMission)
					{
						if (null == Mission)
						{
							continue;
						}

						{
							//	Temp Verzwaigung Debug

							if (Mission.EndeZaitMili().HasValue)
							{
							}
						}

						var MissionBezaicnerNulbar = Mission.Ident();

						if (!MissionBezaicnerNulbar.HasValue)
						{
							continue;
						}

						var MissionBezaicner = MissionBezaicnerNulbar.Value;

						var BisherMissionMitZait = Optimat.Glob.TAD(DictVonMissionBezaicnerMissionZuusctandLezte, MissionBezaicner);

						var BisherMissionErhalte = false;

						if (null != BisherMissionMitZait.Wert)
						{
							BisherMissionErhalte = AutomaatZuusctand.Zait < BisherMissionMitZait.Zait;
						}

						if (BisherMissionErhalte)
						{
							continue;
						}

						DictVonMissionBezaicnerMissionZuusctandLezte[MissionBezaicner] =
							new SictWertMitZait<SictMissionZuusctand>(
							AutomaatZuusctand.Zait, Mission);
					}
				}
			}
		}
	}
}
