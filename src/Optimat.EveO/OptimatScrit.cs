using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bib3;
using Newtonsoft.Json;
using Optimat.EveOnline.Base;
//using Optimat.EveOnline.AuswertGbs;

namespace Optimat.EveOnline
{
	[JsonObject(MemberSerialization.OptIn)]
	public class SictGbsAstSictDiferenzScritAbbild
	{
		readonly public KeyValuePair<string, object>[] MengeZuMemberNameWert;

		[JsonProperty]
		readonly public string MengeMeldungMemberWertSictSerielAbbild;

		[JsonProperty]
		readonly public Int64?[] ListeChildHerkunftAdrese;

		[JsonProperty]
		readonly public bool? ListeChildNull;

		public SictGbsAstSictDiferenzScritAbbild()
		{
		}

		public SictGbsAstSictDiferenzScritAbbild(
			KeyValuePair<string, object>[] MengeZuMemberNameWert = null,
			string MengeMeldungMemberWertSictSerielAbbild = null,
			Int64?[] ListeChildHerkunftAdrese = null,
			bool? ListeChildNull = null)
		{
			this.MengeZuMemberNameWert = MengeZuMemberNameWert;
			this.MengeMeldungMemberWertSictSerielAbbild = MengeMeldungMemberWertSictSerielAbbild;
			this.ListeChildHerkunftAdrese = ListeChildHerkunftAdrese;
			this.ListeChildNull = ListeChildNull;
		}

		override public string ToString()
		{
			return JsonConvert.SerializeObject(this, Formatting.Indented);
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictGbsBaumSictDiferenzScritAbbild
	{
		[JsonProperty]
		readonly public Int64[] MengeGbsAstWurzelAdrese;

		[JsonProperty]
		readonly public KeyValuePair<Int64, SictGbsAstSictDiferenzScritAbbild>[] MengeAstMeldungDiferenz;

		public SictGbsBaumSictDiferenzScritAbbild()
		{
		}

		public SictGbsBaumSictDiferenzScritAbbild(
			Int64[] MengeGbsAstWurzelAdrese,
			KeyValuePair<Int64, SictGbsAstSictDiferenzScritAbbild>[] MengeAstMeldungDiferenz = null)
		{
			this.MengeGbsAstWurzelAdrese = MengeGbsAstWurzelAdrese;
			this.MengeAstMeldungDiferenz = MengeAstMeldungDiferenz;
		}

		override public string ToString()
		{
			return JsonConvert.SerializeObject(this, Formatting.Indented);
		}
	}

	/*
	 * 2015.03.03
	 * 
	[JsonObject(MemberSerialization.OptIn)]
	public class SictAusZiilProcessGbsObjektFilterAusgehendVonAdrese
	{
		/// <summary>
		/// Adrese an der das Objekt gesuuct werde sol.
		/// </summary>
		[JsonProperty]
		public Int64? UursprungGbsObjektAdrese;

		/// <summary>
		/// Tiife der Suuce Child unter dem durc UursprungGbsObjektAdrese bezaicnetem Ast.
		/// </summary>
		[JsonProperty]
		public int? MengeChildSuuceTiifeScranke;

		/// <summary>
		/// Diiser String mus in dem Ast oder ainer desen Children enthalte sain damit das Objekt als zum Filter pasend gewertet werd.
		/// </summary>
		[JsonProperty]
		public string FilterStringEnthalteInAst;

		public SictAusZiilProcessGbsObjektFilterAusgehendVonAdrese()
		{
		}

		public SictAusZiilProcessGbsObjektFilterAusgehendVonAdrese(
			Int64? UursprungGbsObjektAdrese,
			int? MengeChildSuuceTiifeScranke,
			string FilterStringEnthalteInAst = null)
		{
			this.UursprungGbsObjektAdrese = UursprungGbsObjektAdrese;
			this.MengeChildSuuceTiifeScranke = MengeChildSuuceTiifeScranke;
			this.FilterStringEnthalteInAst = FilterStringEnthalteInAst;
		}

		public bool PastZuFilter(GbsAstInfo GbsAst)
		{
			if (null == GbsAst)
			{
				return false;
			}

			var UursprungGbsObjektAdrese = this.UursprungGbsObjektAdrese;
			var MengeChildSuuceTiifeScranke = this.MengeChildSuuceTiifeScranke;
			var FilterStringEnthalteInAst = this.FilterStringEnthalteInAst;

			if (!(UursprungGbsObjektAdrese == GbsAst.HerkunftAdrese))
			{
				return false;
			}

			var GbsAstMengeMemberDelegate = SictRefBaumKopii.ScatescpaicerAppDomain.ZuTypeMengeDelegate<GbsAstInfo>();

			if (null != FilterStringEnthalteInAst)
			{
				var MengeChildBerüksictigt = GbsAst.MengeChildAstTransitiiveHüle(MengeChildSuuceTiifeScranke ?? 0);

				var MengeAstBerüksictigt =
					(MengeChildBerüksictigt ?? new GbsAstInfo[0])
					.Concat(new GbsAstInfo[] { GbsAst }).ToArray();

				var StringEnthalte = false;

				foreach (var AstZuDurcsuuce in MengeAstBerüksictigt)
				{
					if (null == AstZuDurcsuuce)
					{
						continue;
					}

					foreach (var MemberDelegate in GbsAstMengeMemberDelegate)
					{
						var MemberWert = MemberDelegate.Getter(AstZuDurcsuuce);

						var MemberWertAlsString = MemberWert as String;

						if (null == MemberWertAlsString)
						{
							continue;
						}

						if (string.Equals(MemberWertAlsString, FilterStringEnthalteInAst))
						{
							StringEnthalte = true;
							break;
						}
					}
				}

				if (!StringEnthalte)
				{
					return false;
				}
			}

			return true;
		}
	}
	 * */

	/*
	 * 2015.03.03
	 * 
	[JsonObject(MemberSerialization.OptIn)]
	public class SictFläceRectekOrtoAbhängigVonGbsAst : InProcessGbsFläceRectekOrto
	{
		[JsonProperty]
		public InGbsPfaad NaacGbsAstPfaad;

		public SictFläceRectekOrtoAbhängigVonGbsAst()
		{
		}

		public SictFläceRectekOrtoAbhängigVonGbsAst(
			OrtogoonInt FläceTailSctaatisc,
			InGbsPfaad NaacGbsAstPfaad = null)
			:
			base(FläceTailSctaatisc)
		{
			this.NaacGbsAstPfaad = NaacGbsAstPfaad;
		}

		public SictFläceRectekOrtoAbhängigVonGbsAst Versezt(Vektor2DInt Versaz)
		{
			var FläceTailSctaatisc = this.FläceTailSctaatisc;

			var FläceTailSctaatiscVersezt = FläceTailSctaatisc.Versezt(Versaz);

			return new SictFläceRectekOrtoAbhängigVonGbsAst(FläceTailSctaatiscVersezt, NaacGbsAstPfaad);
		}

		static public OrtogoonInt? FläceErgeebnisBerecne(
			SictFläceRectekOrtoAbhängigVonGbsAst FläceAbhängigVonGbsAst,
			GbsAstInfo[] MengeGbsAstInfo)
		{
			if (null == FläceAbhängigVonGbsAst)
			{
				return null;
			}

			var FläceTailSctaatisc = FläceAbhängigVonGbsAst.FläceTailSctaatisc;
			var NaacGbsAstPfaad = FläceAbhängigVonGbsAst.NaacGbsAstPfaad;

			if (null == NaacGbsAstPfaad)
			{
				return FläceTailSctaatisc;
			}

			if (null == MengeGbsAstInfo)
			{
				return null;
			}

			var PfaadWurzelAdreseNulbar = NaacGbsAstPfaad.WurzelAstAdrese;

			if (!PfaadWurzelAdreseNulbar.HasValue)
			{
				return null;
			}

			var PfaadWurzel = MengeGbsAstInfo.FirstOrDefault((Kandidaat) => Kandidaat.HerkunftAdrese == PfaadWurzelAdreseNulbar);

			if (null == PfaadWurzel)
			{
				return null;
			}

			var PfaadListeAstAdrese = NaacGbsAstPfaad.ListeAstAdrese;

			var PfaadListeAstLaageAggr = (PfaadWurzel.Laage ?? new SictVektor2DSingle(0, 0)).AlsVektor2DInt();

			if (null != PfaadListeAstAdrese)
			{
				foreach (var PfaadAstAdrese in PfaadListeAstAdrese)
				{
					var PfaadAst = MengeGbsAstInfo.FirstOrDefault((KandidaatAst) => KandidaatAst.HerkunftAdrese == PfaadAstAdrese);

					if (null == PfaadAst)
					{
						return null;
					}

					PfaadListeAstLaageAggr += (PfaadAst.Laage ?? new SictVektor2DSingle(0, 0)).AlsVektor2DInt();
				}
			}

			return OrtogoonInt.AusPunktZentrumUndGrööse(FläceTailSctaatisc.ZentrumLaage + PfaadListeAstLaageAggr, FläceTailSctaatisc.Grööse);
		}
	}
	 * */

	/*
	 * 2015.03.03
	 * 
	[JsonObject(MemberSerialization.OptIn)]
	public class SictVonProcessLeese : SictVerlaufBeginUndEndeRefMitZaitAinhaitMili<object>
	{
		[JsonProperty]
		public GbsAstInfo ErgeebnisGbsBaum;

		[JsonProperty]
		public SictGbsBaumSictDiferenzScritAbbild ErgeebnisGbsBaumDiferenzZuScritVorher;

		[JsonProperty]
		public string MainWindowTitle;

		public SictVonProcessLeese()
		{
		}

		public SictVonProcessLeese(
			Int64? BeginZaitMili,
			Int64? EndeZaitMili,
			GbsAstInfo ErgeebnisGbsBaum = null,
			SictGbsBaumSictDiferenzScritAbbild ErgeebnisGbsBaumDiferenzZuScritVorher = null,
			string MainWindowTitle	= null)
			:
			base(BeginZaitMili, EndeZaitMili)
		{
			this.ErgeebnisGbsBaum = ErgeebnisGbsBaum;
			this.ErgeebnisGbsBaumDiferenzZuScritVorher = ErgeebnisGbsBaumDiferenzZuScritVorher;
			this.MainWindowTitle = MainWindowTitle;
		}
	}
	 * */

	/*
	 * 2015.03.16
	 * 
	 * Verscoobe naac Optimat.EveOnline.
	 * 
	[JsonObject(MemberSerialization.OptIn)]
	public class SictOptimatScrit
    {
		[JsonProperty]
		public Int64 NuzerZait;

		[JsonProperty]
		public byte[] NuzerSizungIdent;

		[JsonProperty]
		public byte[] AnwendungSizungIdent;

		[JsonProperty]
		public Int64? AnwendungZaitMili;

		[JsonProperty]
		public VonProcessMesung<VonSensorikMesung> VonProcessMesung;

		[JsonProperty]
		public SictVonWindowLeeseErgeebnis VonWindowLeese;

		[JsonProperty]
		public SictVorsclaagNaacProcessWirkung[] VorsclaagListeWirkung;

		[JsonProperty]
		public SictNaacProcessWirkung[] NaacProcessListeWirkung;

		[JsonProperty]
		public Bib3.RefNezDiferenz.SictZuNezSictDiferenzScritAbbild NaacNuzerBerictAutomaatZuusctandScritDiferenz;

		[JsonProperty]
		public VonAutomatMeldungZuusctand NaacNuzerBerictAutomaatZuusctand;

		[JsonProperty]
		public SictDataiIdentUndSuuceHinwais ProcessWindowClientRasterIdentUndSuuceHinwais;

		public Int64? VonProcessLeeseBeginZait
		{
			get
			{
				var VonProcessLeese = this.VonProcessMesung;

				if (null == VonProcessLeese)
				{
					return null;
				}

				return VonProcessLeese.BeginZait;
			}
		}

		public SictOptimatScrit()
		{
		}

		public SictOptimatScrit(
			Int64	NuzerZait,
			VonProcessMesung<VonSensorikMesung> VonSensorikMesung,
			SictVonWindowLeeseErgeebnis VonWindowLeese = null
			,
			SictVorsclaagNaacProcessWirkung[] VorsclaagListeWirkung = null,
			SictNaacProcessWirkung[] NaacProcessListeWirkung = null
			)
		{
			this.NuzerZait = NuzerZait;
			this.VonProcessMesung = VonSensorikMesung;
			this.VonWindowLeese = VonWindowLeese;

			this.VorsclaagListeWirkung = VorsclaagListeWirkung;
			this.NaacProcessListeWirkung = NaacProcessListeWirkung;
		}

		static public VonProcessMesung<VonSensorikMesung> OptimatScritSictFürBerict(
			VonProcessMesung<VonSensorikMesung> VonProcessLeese,
			bool GbsBaumErhalte)
		{
			if (null == VonProcessLeese)
			{
				return null;
			}

			var VonProcessLeeseAbbild = new VonProcessMesung<VonSensorikMesung>(
				null,
				VonProcessLeese.BeginZait,
				VonProcessLeese.EndeZait,
				VonProcessLeese.ProcessId);

			return VonProcessLeeseAbbild;
		}

		static public SictOptimatScrit OptimatScritSictFürBerict(
			SictOptimatScrit OptimatScrit,
			bool GbsBaumErhalte)
		{
			if (null == OptimatScrit)
			{
				return null;
			}

			var VonProcessLeese = OptimatScrit.VonProcessMesung;

			var VonProcessLeeseAbbild = OptimatScritSictFürBerict(VonProcessLeese, GbsBaumErhalte);

			var OptimatScritAbbild = new SictOptimatScrit(
				OptimatScrit.NuzerZait,
				VonProcessLeeseAbbild,
				SictRefBaumKopii.ObjektKopiiErsctele(OptimatScrit.VonWindowLeese),
				SictRefBaumKopii.ObjektKopiiErsctele(OptimatScrit.VorsclaagListeWirkung),
				SictRefBaumKopii.ObjektKopiiErsctele(OptimatScrit.NaacProcessListeWirkung));

			OptimatScritAbbild.AnwendungSizungIdent = SictRefBaumKopii.ObjektKopiiErsctele(OptimatScrit.AnwendungSizungIdent);
			OptimatScritAbbild.AnwendungZaitMili = OptimatScrit.AnwendungZaitMili;

			OptimatScritAbbild.NaacNuzerBerictAutomaatZuusctand = SictRefBaumKopii.ObjektKopiiErsctele(OptimatScrit.NaacNuzerBerictAutomaatZuusctand);
			OptimatScritAbbild.NaacNuzerBerictAutomaatZuusctandScritDiferenz = SictRefBaumKopii.ObjektKopiiErsctele(OptimatScrit.NaacNuzerBerictAutomaatZuusctandScritDiferenz);

			OptimatScritAbbild.ProcessWindowClientRasterIdentUndSuuceHinwais = SictRefBaumKopii.ObjektKopiiErsctele(OptimatScrit.ProcessWindowClientRasterIdentUndSuuceHinwais);

			return OptimatScritAbbild;
		}
	}
	 * */
}
