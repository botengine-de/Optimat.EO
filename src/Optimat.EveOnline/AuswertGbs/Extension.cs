using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Bib3;
using Optimat.EveOnline.VonSensor;

namespace Optimat.EveOnline.AuswertGbs
{
	static public class Extension
	{
		static public Vektor2DInt AlsVektor2DInt(
			this	SictVektor2DSingle Vektor2DSingle)
		{
			return new Vektor2DInt((Int64)Vektor2DSingle.A, (Int64)Vektor2DSingle.B);
		}

		static public void SimulatioonApliziire(
			this	VonSensorikMesung VonSensorikScnapscus,
			SictOptimatParamSimu Simulatioon)
		{
			if (null == VonSensorikScnapscus)
			{
				return;
			}

			if (null == Simulatioon)
			{
				return;
			}

			var SimuScnapscus = new VonSensorikMesung();

			SimuScnapscus.SelfShipState = Simulatioon.SelbstShipZuusctand;

			Bib3.RefNezDiferenz.Extension.InRefNezApliziireErsazWoUnglaicDefault(VonSensorikScnapscus, SimuScnapscus);
		}

		static public TAus TypeKonvertNewtonsoftJson<TAus>(
			this	object Ain)
		{
			var Seriel = Newtonsoft.Json.JsonConvert.SerializeObject(Ain);

			return Newtonsoft.Json.JsonConvert.DeserializeObject<TAus>(Seriel);
		}

		static readonly Bib3.RefNezDiferenz.SictTypeBehandlungRictliinieMitTransportIdentScatescpaicer
			KonvertGbsAstInfoRictliinieMitScatescpaicer =
			new Bib3.RefNezDiferenz.SictTypeBehandlungRictliinieMitTransportIdentScatescpaicer(
				Bib3.RefNezDiferenz.NewtonsoftJson.SictMengeTypeBehandlungRictliinieNewtonsoftJson.KonstruktMengeTypeBehandlungRictliinie(
				new KeyValuePair<Type, Type>[]{
					new	KeyValuePair<Type, Type>(typeof(GbsAstInfo), typeof(SictGbsAstInfoSictAuswert)),
					new	KeyValuePair<Type, Type>(typeof(GbsAstInfo[]), typeof(SictGbsAstInfoSictAuswert[])),
				}));

		static public VonSensorikMesung SensorikScnapscusKonstrukt(
			this	Optimat.EveOnline.GbsAstInfo GbsBaum)
		{
			if (null == GbsBaum)
			{
				return null;
			}

			/*
			 * 2015.03.13
			 * 
			var GbsBaumScpez = GbsBaum.TypeKonvertNewtonsoftJson<SictGbsAstInfoSictAuswert>();
			 * */
			var GbsBaumScpez =
				SictRefNezKopii.ObjektKopiiErsctele(
				GbsBaum,
				null,
				new SictRefBaumKopiiParam(null, KonvertGbsAstInfoRictliinieMitScatescpaicer),
				null,
				null)
				as SictGbsAstInfoSictAuswert;

			if (null == GbsBaumScpez)
			{
				return null;
			}

			int InBaumAstIndexZääler = 0;

			GbsBaumScpez.AbgelaiteteAigescafteBerecne(ref	InBaumAstIndexZääler);

			var Auswert = new SictAuswertGbsAgr(GbsBaumScpez);

			Auswert.Berecne();

			return Auswert.AuswertErgeebnis;
		}

		static public void FüleMengeFeldAusListItem(
			this	InventoryItem InventoryItem,
			InventoryItemDetailsColumnTyp?[] ListeColumnTyp)
		{
			string Name = null;
			string Group = null;
			string Category = null;
			string QuantitySictStringAbbild = null;
			int? Quantity = null;

			try
			{
				var ListItem = InventoryItem.ListItem;

				if (null == ListeColumnTyp)
				{
					return;
				}

				if (null == ListItem)
				{
					return;
				}

				var ListItemListeZeleText = ListItem.ListeZeleText;

				if (null == ListItemListeZeleText)
				{
					return;
				}

				for (int ColumnIndex = 0; ColumnIndex < ListeColumnTyp.Length; ColumnIndex++)
				{
					var ColumnTyp = ListeColumnTyp[ColumnIndex];

					var ZeleText = ListItemListeZeleText.ElementAtOrDefault(ColumnIndex);

					if (!ColumnTyp.HasValue)
					{
						continue;
					}

					switch (ColumnTyp.Value)
					{
						case InventoryItemDetailsColumnTyp.Name:
							Name = ZeleText;
							break;
						case InventoryItemDetailsColumnTyp.Quantity:
							QuantitySictStringAbbild = ZeleText;
							break;
						case InventoryItemDetailsColumnTyp.Group:
							Group = ZeleText;
							break;
						case InventoryItemDetailsColumnTyp.Category:
							Category = ZeleText;
							break;
						case InventoryItemDetailsColumnTyp.Size:
							break;
						case InventoryItemDetailsColumnTyp.Slot:
							break;
						case InventoryItemDetailsColumnTyp.Volume:
							break;
						case InventoryItemDetailsColumnTyp.Meta_Level:
							break;
						case InventoryItemDetailsColumnTyp.Tech_Level:
							break;
						default:
							break;
					}
				}

				Quantity = Bib3.Glob.TryParseInt(
					QuantitySictStringAbbild,
					Glob.SctandardNumberFormatInfo,
					System.Globalization.NumberStyles.Integer | System.Globalization.NumberStyles.AllowThousands);
			}
			finally
			{
				InventoryItem.Name = Name;
				InventoryItem.Group = Group;
				InventoryItem.Category = Category;
				InventoryItem.QuantitySictStringAbbild = QuantitySictStringAbbild;
				InventoryItem.Quantity = Quantity;
			}
		}

		/// <summary>
		/// 2014.00.00	Bsp:
		/// "Corpum Priest Wreck <color=#66FFFFFF>145 m</color>"
		/// 2014.00.07	Bsp:
		/// "Guristas Personnel Transport Wreck <color=#66FFFFFF>2.291 m</color>"
		/// 2014.09.04	Bsp:
		/// "Drone Bay"
		/// "Procure (Procurer)"
		/// </summary>
		static string TreeViewEntryLabelTextRegexPattern = "([^\\<]+)(\\<[^\\>]+\\>([\\s\\d\\w\\,\\.]+)|$)";

		static public void LabelTaileBerecne(
			this	TreeViewEntry Entry)
		{
			string LabelTextTailObjektName = null;
			string LabelTextTailObjektDistance = null;
			Int64? ObjektDistance = null;

			try
			{
				var LabelText = Entry.LabelText;

				if (null == LabelText)
				{
					return;
				}

				var LabelTextMatch = Regex.Match(LabelText, TreeViewEntryLabelTextRegexPattern);

				if (!LabelTextMatch.Success)
				{
					return;
				}

				LabelTextTailObjektName = Bib3.Glob.TrimNullable(LabelTextMatch.Groups[1].Value);
				LabelTextTailObjektDistance = Bib3.Glob.TrimNullable(LabelTextMatch.Groups[3].Value);
			}
			finally
			{
				ObjektDistance = TempAuswertGbs.Extension.AusDistanceSictStringAbbildGanzzaalDurcMeeterScrankeMax(LabelTextTailObjektDistance);

				Entry.LabelTextTailObjektName = LabelTextTailObjektName;
				Entry.LabelTextTailObjektDistance = LabelTextTailObjektDistance;
				Entry.ObjektDistance = ObjektDistance;
			}
		}

		static public IEnumerable<SictGbsAstInfoSictAuswert> BaumEnumFlacListeKnoote(
			this	SictGbsAstInfoSictAuswert SuuceWurzel,
			int? TiifeScrankeMax = null,
			int? TiifeScrankeMin = null)
		{
			return
				Bib3.Extension.BaumEnumFlacListeKnoote(
				SuuceWurzel,
				(Ast) => null == Ast ? null : Ast.ListeChildBerecne().OfTypeNullable<SictGbsAstInfoSictAuswert>(),
				TiifeScrankeMax,
				TiifeScrankeMin);
		}

		static public SictGbsAstInfoSictAuswert[] StaticListeChild(
			this	SictGbsAstInfoSictAuswert Ast)
		{
			if (null == Ast)
			{
				return null;
			}

			return Ast.ListeChild;
		}

		static public SictVektor2DSingle? LaagePlusVonParentErbeLaage(
			this	SictGbsAstInfoSictAuswert Ast)
		{
			if (null == Ast)
			{
				return null;
			}

			var VonParentErbeLaage = Ast.VonParentErbeLaage;

			if (!VonParentErbeLaage.HasValue)
			{
				return Ast.Laage;
			}

			return Ast.Laage + VonParentErbeLaage;
		}

		static public SictGbsAstInfoSictAuswert Konvert(
			this	GbsAstInfo GbsAst)
		{
			if (null == GbsAst)
			{
				return null;
			}

			SictRefBaumKopiiProfile Profile = null;

			return
				SictRefBaumKopii.ObjektKopiiErscteleUndErsazType<GbsAstInfo, SictGbsAstInfoSictAuswert>(
					GbsAst, ref	Profile) as SictGbsAstInfoSictAuswert;
		}


		static public string LabelText(
			this	SictGbsAstInfoSictAuswert Ast)
		{
			if (null == Ast)
			{
				return null;
			}

			return Ast.SetText;
		}

		static public void AbgelaiteteAigescafteBerecne(
			this	SictGbsAstInfoSictAuswert Ast,
			ref	int InBaumAstIndexZääler,
			int? TiifeScrankeMax = null,
			SictVektor2DSingle? VonParentErbeLaage = null,
			float? VonParentErbeClippingFläceLinx = null,
			float? VonParentErbeClippingFläceOobn = null,
			float? VonParentErbeClippingFläceRecz = null,
			float? VonParentErbeClippingFläceUntn = null)
		{
			if (null == Ast)
			{
				return;
			}

			if (TiifeScrankeMax < 0)
			{
				return;
			}

			Ast.InBaumAstIndex = ++InBaumAstIndexZääler;
			Ast.VonParentErbeLaage = VonParentErbeLaage;

			var FürChildVonParentErbeLaage = Ast.Laage;

			/*
			 * 2015.02.23
			 * 
			OrtogoonSingle SelbsctFläce = null;
			 * */

			var LaagePlusVonParentErbeLaage = Ast.LaagePlusVonParentErbeLaage();
			var Grööse = Ast.Grööse;

			/*
			 * 2015.02.23
			 * 
			if(LaagePlusVonParentErbeLaage.HasValue	&&
				Grööse.HasValue)
			{
				SelbsctFläce = OrtogoonSingle.FläceAusEkeMininumLaageUndGrööse(LaagePlusVonParentErbeLaage.Value, Grööse.Value);
			}
			 * */

			var FürChildVonParentErbeClippingFläceLinx = VonParentErbeClippingFläceLinx;
			var FürChildVonParentErbeClippingFläceOobn = VonParentErbeClippingFläceOobn;
			var FürChildVonParentErbeClippingFläceRecz = VonParentErbeClippingFläceRecz;
			var FürChildVonParentErbeClippingFläceUntn = VonParentErbeClippingFläceUntn;

			if (LaagePlusVonParentErbeLaage.HasValue && Grööse.HasValue)
			{
				FürChildVonParentErbeClippingFläceLinx = Bib3.Glob.Max(FürChildVonParentErbeClippingFläceLinx, LaagePlusVonParentErbeLaage.Value.A);
				FürChildVonParentErbeClippingFläceRecz = Bib3.Glob.Min(FürChildVonParentErbeClippingFläceRecz, LaagePlusVonParentErbeLaage.Value.A);
				FürChildVonParentErbeClippingFläceOobn = Bib3.Glob.Max(FürChildVonParentErbeClippingFläceOobn, LaagePlusVonParentErbeLaage.Value.B);
				FürChildVonParentErbeClippingFläceUntn = Bib3.Glob.Min(FürChildVonParentErbeClippingFläceUntn, LaagePlusVonParentErbeLaage.Value.B);
			}

			if (VonParentErbeLaage.HasValue)
			{
				if (FürChildVonParentErbeLaage.HasValue)
				{
					FürChildVonParentErbeLaage = FürChildVonParentErbeLaage.Value + VonParentErbeLaage.Value;
				}
				else
				{
					FürChildVonParentErbeLaage = VonParentErbeLaage;
				}
			}

			var ListeChild = Ast.ListeChild;

			if (null != ListeChild)
			{
				for (int ChildIndex = 0; ChildIndex < ListeChild.Length; ChildIndex++)
				{
					var Child = ListeChild[ChildIndex];

					if (null == Child)
					{
						continue;
					}

					Child.InParentListeChildIndex = ChildIndex;
					Child.AbgelaiteteAigescafteBerecne(
						ref	InBaumAstIndexZääler,
						TiifeScrankeMax - 1,
						FürChildVonParentErbeLaage,
						FürChildVonParentErbeClippingFläceLinx,
						FürChildVonParentErbeClippingFläceOobn,
						FürChildVonParentErbeClippingFläceRecz,
						FürChildVonParentErbeClippingFläceUntn);
				}
			}
		}

		static public SictGbsAstInfoSictAuswert SuuceFlacMengeAstFrüheste(
			this	SictGbsAstInfoSictAuswert[] SuuceMengeWurzel,
			Func<SictGbsAstInfoSictAuswert, bool> Prädikaat,
			int? TiifeScrankeMax = null,
			int? TiifeScrankeMin = null)
		{
			if (null == SuuceMengeWurzel)
			{
				return null;
			}

			foreach (var Wurzel in SuuceMengeWurzel)
			{
				var Fund = Wurzel.SuuceFlacMengeAstFrüheste(Prädikaat, TiifeScrankeMax, TiifeScrankeMin);

				if (null != Fund)
				{
					return Fund;
				}
			}

			return null;
		}

		static public SictGbsAstInfoSictAuswert GröösteLabel(
			this	SictGbsAstInfoSictAuswert SuuceWurzel,
			int? TiifeScrankeMax = null)
		{
			var MengeLabelSictbar =
				SuuceWurzel.SuuceFlacMengeAst((Kandidaat) => Glob.GbsAstTypeIstLabel(Kandidaat), null, TiifeScrankeMax);

			if (null == MengeLabelSictbar)
			{
				return null;
			}

			SictGbsAstInfoSictAuswert BisherBeste = null;

			foreach (var LabelAst in MengeLabelSictbar)
			{
				var LabelAstGrööse = LabelAst.Grööse;

				if (!LabelAstGrööse.HasValue)
				{
					continue;
				}

				if (null == BisherBeste)
				{
					BisherBeste = LabelAst;
				}
				else
				{
					if (BisherBeste.Grööse.Value.BetraagQuadriirt < LabelAstGrööse.Value.BetraagQuadriirt)
					{
						BisherBeste = LabelAst;
					}
				}
			}

			return BisherBeste;
		}

		/*
		 * 2015.03.12
		 * 
		 * Error	1	The call is ambiguous between the following methods or properties: 'Optimat.EveOnline.AuswertGbs.Extension.SuuceFlacMengeAstFrüheste(Optimat.EveOnline.AuswertGbs.SictGbsAstInfoSictAuswert, System.Func<Optimat.EveOnline.AuswertGbs.SictGbsAstInfoSictAuswert,bool>, int?, int?)' and 'Optimat.EveOnline.AuswertGbs.Extension.SuuceFlacMengeAstFrüheste(Optimat.EveOnline.AuswertGbs.SictGbsAstInfoSictAuswert, System.Func<Optimat.EveOnline.AuswertGbs.SictGbsAstInfoSictAuswert,bool>, int?, int?)'	t:\günta\projekt\optimat.eveonline\impl.sensor\assembly\optimat.eveo.nuzer\sln\optimat.eveonline.temp\sictgbs\extension.cs	635	16	Optimat.EveOnline.Temp

		static public SictGbsAstInfoSictAuswert SuuceFlacMengeAstFrüheste(
			this	SictGbsAstInfoSictAuswert SuuceWurzel,
			Func<SictGbsAstInfoSictAuswert, bool> Prädikaat,
			int? TiifeScrankeMax = null,
			int? TiifeScrankeMin = null)
		{
			if (null == SuuceWurzel)
			{
				return null;
			}

			return SuuceWurzel.SuuceFlacMengeAstFrüheste(Prädikaat, TiifeScrankeMax, TiifeScrankeMin);
		}
		 * */

		/*
		 * 2015.03.12
		 * 
		 * Error	56	The call is ambiguous between the following methods or properties: 'Optimat.EveOnline.AuswertGbs.Extension.SuuceFlacMengeAstFrühesteMitHerkunftAdrese(Optimat.EveOnline.AuswertGbs.SictGbsAstInfoSictAuswert, long?, int?, int?)' and 'Optimat.EveOnline.AuswertGbs.Extension.SuuceFlacMengeAstFrühesteMitHerkunftAdrese(Optimat.EveOnline.AuswertGbs.SictGbsAstInfoSictAuswert, long?, int?, int?)'	t:\günta\projekt\optimat.eveonline\impl.sensor\assembly\optimat.eveo.nuzer\sln\optimat.eveonline.temp\sictgbs\gbsastinfo.cs	189	25	Optimat.EveOnline.Temp

		 * 
		static public SictGbsAstInfoSictAuswert SuuceFlacMengeAstFrühesteMitHerkunftAdrese(
			this	SictGbsAstInfoSictAuswert SuuceWurzel,
			Int64? AstHerkunftAdrese,
			int? TiifeScrankeMax = null,
			int? TiifeScrankeMin = null)
		{
			if (null == SuuceWurzel)
			{
				return null;
			}

			var Prädikaat = new Func<SictGbsAstInfoSictAuswert, bool>((Kandidaat) => (null == Kandidaat) ? false : Kandidaat.HerkunftAdrese == AstHerkunftAdrese);

			return SuuceWurzel.SuuceFlacMengeAstFrüheste(Prädikaat, TiifeScrankeMax, TiifeScrankeMin);
		}
		 * */

		static public SictGbsAstInfoSictAuswert[] SuuceFlacMengeAst(
			this	SictGbsAstInfoSictAuswert[] SuuceMengeWurzel,
			Func<SictGbsAstInfoSictAuswert, bool> Prädikaat,
			int? ListeFundAnzaalScrankeMax = null,
			int? TiifeScrankeMax = null,
			int? TiifeScrankeMin = null,
			bool LaseAusMengeChildUnterhalbTrefer = false)
		{
			if (null == SuuceMengeWurzel)
			{
				return null;
			}

			var MengeFund = new List<SictGbsAstInfoSictAuswert>();

			foreach (var Wurzel in SuuceMengeWurzel)
			{
				var WurzelMengeFund = Wurzel.SuuceFlacMengeAst(
					Prädikaat,
					ListeFundAnzaalScrankeMax - MengeFund.Count,
					TiifeScrankeMax,
					TiifeScrankeMin,
					LaseAusMengeChildUnterhalbTrefer);

				if (null != WurzelMengeFund)
				{
					MengeFund.AddRange(WurzelMengeFund);
				}
			}

			return MengeFund.ToArray();
		}

		/*
		 * 2015.03.12
		 * 
		 * Error	20	The call is ambiguous between the following methods or properties: 'Optimat.EveOnline.AuswertGbs.Extension.SuuceFlacMengeAst(Optimat.EveOnline.AuswertGbs.SictGbsAstInfoSictAuswert, System.Func<Optimat.EveOnline.AuswertGbs.SictGbsAstInfoSictAuswert,bool>, int?, int?, int?, bool)' and 'Optimat.EveOnline.AuswertGbs.Extension.SuuceFlacMengeAst(Optimat.EveOnline.AuswertGbs.SictGbsAstInfoSictAuswert, System.Func<Optimat.EveOnline.AuswertGbs.SictGbsAstInfoSictAuswert,bool>, int?, int?, int?, bool)'	T:\Günta\Projekt\Optimat.EveOnline\Impl.Sensor\Assembly\Optimat.EveO.Nuzer\sln\Optimat.EveOnline.Temp\SictGbs\AuswertGbs.DroneView.cs	422	5	Optimat.EveOnline.Temp

		 * 
		static public SictGbsAstInfoSictAuswert[] SuuceFlacMengeAst(
			SictGbsAstInfoSictAuswert SuuceWurzel,
			Func<SictGbsAstInfoSictAuswert, bool> Prädikaat,
			int? ListeFundAnzaalScrankeMax = null,
			int? TiifeScrankeMax = null,
			int? TiifeScrankeMin = null,
			bool LaseAusMengeChildUnterhalbTrefer = false)
		{
			if (null == SuuceWurzel)
			{
				return null;
			}

			return SuuceWurzel.SuuceFlacMengeAst(Prädikaat, ListeFundAnzaalScrankeMax, TiifeScrankeMax, TiifeScrankeMin, LaseAusMengeChildUnterhalbTrefer);
		}
		 * */

		static public SictGbsAstInfoSictAuswert[] SuuceFlacMengeAst(
			this	SictGbsAstInfoSictAuswert Ast,
			Func<SictGbsAstInfoSictAuswert, bool> Prädikaat,
			int? ListeFundAnzaalScrankeMax = null,
			int? TiifeScrankeMax = null,
			int? TiifeScrankeMin = null,
			bool LaseAusMengeChildUnterhalbTrefer = false)
		{
			var MengeAstMitPfaad = Ast.SuuceFlacMengeAstMitPfaad(
				Prädikaat,
				ListeFundAnzaalScrankeMax, TiifeScrankeMax, TiifeScrankeMin, LaseAusMengeChildUnterhalbTrefer);

			if (null == MengeAstMitPfaad)
			{
				return null;
			}

			var MengeAst = MengeAstMitPfaad.Select((AstMitPfaad) => AstMitPfaad.LastOrDefault()).ToArray();

			return MengeAst;
		}

		static public SictGbsAstInfoSictAuswert SuuceFlacMengeAstFrühesteMitHerkunftAdrese(
			this	SictGbsAstInfoSictAuswert Ast,
			Int64? HerkunftAdrese,
			int? TiifeScrankeMax = null,
			int? TiifeScrankeMin = null)
		{
			return Extension.SuuceFlacMengeAstFrüheste(
				Ast,
				(Kandidaat) => Kandidaat.HerkunftAdrese == HerkunftAdrese,
				TiifeScrankeMax,
				TiifeScrankeMin);
		}

		static public SictGbsAstInfoSictAuswert SuuceFlacMengeAstFrüheste(
			this	SictGbsAstInfoSictAuswert Ast,
			Func<SictGbsAstInfoSictAuswert, bool> Prädikaat,
			int? TiifeScrankeMax = null,
			int? TiifeScrankeMin = null)
		{
			var MengeAst = Ast.SuuceFlacMengeAst(Prädikaat, 1, TiifeScrankeMax, TiifeScrankeMin, true);

			if (null == MengeAst)
			{
				return null;
			}

			var FundAst = MengeAst.FirstOrDefault();

			return FundAst;
		}

		static public SictGbsAstInfoSictAuswert[] SuuceFlacMengeAstMitPfaadFrüheste(
			this	SictGbsAstInfoSictAuswert Ast,
			Func<SictGbsAstInfoSictAuswert, bool> Prädikaat,
			int? TiifeScrankeMax = null,
			int? TiifeScrankeMin = null)
		{
			var MengeAstMitPfaad = Ast.SuuceFlacMengeAstMitPfaad(Prädikaat, 1, TiifeScrankeMax, TiifeScrankeMin, true);

			if (null == MengeAstMitPfaad)
			{
				return null;
			}

			var AstMitPfaad = MengeAstMitPfaad.FirstOrDefault();

			return AstMitPfaad;
		}

		static public SictGbsAstInfoSictAuswert[][] SuuceFlacMengeAstMitPfaad(
			this	SictGbsAstInfoSictAuswert Ast,
			Func<SictGbsAstInfoSictAuswert, bool> Prädikaat,
			int? ListeFundAnzaalScrankeMax = null,
			int? TiifeScrankeMax = null,
			int? TiifeScrankeMin = null,
			bool LaseAusMengeChildUnterhalbTrefer = false)
		{
			if (null == Ast)
			{
				return null;
			}

			return Optimat.Glob.SuuceFlacMengeAstMitPfaad(
				Ast,
				Prädikaat,
				(Kandidaat) => Kandidaat.ListeChild,
				ListeFundAnzaalScrankeMax,
				TiifeScrankeMax,
				TiifeScrankeMin,
				LaseAusMengeChildUnterhalbTrefer);
		}

		static public SictVektor2DSingle? GrööseMaxAusListeChild(
			this	SictGbsAstInfoSictAuswert Ast)
		{
			if (null == Ast)
			{
				return null;
			}

			SictVektor2DSingle? GrööseMax = null;

			var ThisGrööse = Ast.Grööse;

			if (ThisGrööse.HasValue)
			{
				GrööseMax = ThisGrööse;
			}

			var ListeChild = Ast.ListeChild;

			if (null != ListeChild)
			{
				foreach (var Child in ListeChild)
				{
					var ChildGrööse = Child.Grööse;

					if (ChildGrööse.HasValue)
					{
						if (GrööseMax.HasValue)
						{
							GrööseMax = new SictVektor2DSingle(
								Math.Max(GrööseMax.Value.A, ChildGrööse.Value.A),
								Math.Max(GrööseMax.Value.B, ChildGrööse.Value.B));
						}
						else
						{
							GrööseMax = ChildGrööse;
						}
					}
				}
			}

			return GrööseMax;
		}

		static string[] UIRootVorgaabeGrööseListeName = new string[] { "l_main", "l_viewstate" };

		static public SictVektor2DSingle? GrööseAusListeChildFürScpezUIRootBerecne(
			this	SictGbsAstInfoSictAuswert Ast)
		{
			if (null == Ast)
			{
				return null;
			}

			var ListeChild = Ast.ListeChild;

			if (null != ListeChild)
			{
				foreach (var Child in ListeChild)
				{
					var ChildGrööse = Child.Grööse;

					if (ChildGrööse.HasValue)
					{
						if (UIRootVorgaabeGrööseListeName.Any((AstNaame) => string.Equals(AstNaame, Child.Name)))
						{
							return ChildGrööse;
						}
					}
				}
			}

			return null;
		}

		static public SictGbsAstInfoSictAuswert[] MengePfaadScnitmenge(
			this	SictGbsAstInfoSictAuswert AstPfaadUurscprung,
			IEnumerable<SictGbsAstInfoSictAuswert> MengePfaadBlat)
		{
			if (null == AstPfaadUurscprung || null == MengePfaadBlat)
			{
				return null;
			}

			var MengePfaad =
				MengePfaadBlat.Select((PfaadBlat) =>
					Extension.SuuceFlacMengeAstMitPfaadFrüheste(AstPfaadUurscprung, (t) => t == PfaadBlat)).ToArray();

			if (Bib3.Extension.NullOderLeer(MengePfaad))
			{
				return null;
			}

			var Pfaad0 = MengePfaad.FirstOrDefault();

			var TailPfaadÜberscnaidung =
				Pfaad0.TakeWhile((PfaadZwisceAst, InPfaadAstIndex) =>
					MengePfaad.All((AnderePfaad) => (null == AnderePfaad) ? false : (AnderePfaad.ElementAtOrDefault(InPfaadAstIndex) == PfaadZwisceAst)))
				.ToArray();

			return TailPfaadÜberscnaidung;
		}


	}
}
