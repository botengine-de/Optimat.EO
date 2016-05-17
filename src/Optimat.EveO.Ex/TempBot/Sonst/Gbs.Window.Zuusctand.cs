using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Optimat.EveOnline.Anwendung;
using VonSensor = Optimat.EveOnline.VonSensor;
using Optimat.ScpezEveOnln;
using Optimat.EveOnline.VonSensor;
using Bib3;
using ExtractFromOldAssembly.Bib3;

namespace Optimat.EveOnline.Anwendung
{
	[JsonObject(MemberSerialization.OptIn)]
	public class SictGbsWindowZuusctand : SictZuGbsObjektZuusctandMitIdentPerIntMitZuusazInfo<VonSensor.Window,	SictGbsAgregatioonAusTransitionInfo>
	{
		public VonSensor.Window ScnapscusWindowLezte
		{
			get
			{
				return this.AingangScnapscusTailObjektIdentLezteBerecne();
			}
		}

		[JsonProperty]
		VonSensor.GbsElement HeaderButtonMinimizeSictungLezte
		{
			set;
			get;
		}

		[JsonProperty]
		GbsElement HeaderButtonCloseSictungLezte
		{
			set;
			get;
		}

		[JsonProperty]
		public GbsListGroupedZuusctand ListHaupt
		{
			private set;
			get;
		}

		static public GbsElement FläceProjeziirtAufGbsAst(
			GbsElement Fläce,
			GbsElement Ident)
		{
			if (null == Fläce || null == Ident)
			{
				return null;
			}

			return new GbsElement(
				new ObjektMitIdentInt64(Ident.Ident),
					Fläce.InGbsFläce,
					Fläce.InGbsBaumAstIndex
					);
		}

		static public GbsElement FläceProjeziirtAufGbsAst(
			OrtogoonInt Fläce,
			GbsElement Ident)
		{
			if (null == Fläce || null == Ident)
			{
				return null;
			}

			return new GbsElement(
				new ObjektMitIdentInt64(Ident.Ident),
					Fläce,
					Ident.InGbsBaumAstIndex
					);
		}

		public GbsElement HeaderButtonMinimizeBerecne()
		{
			//	Ersaz des Ident des GBS Ast aus ältere Scnapscus durc Ident aus Window aus lezte Scnapscus damit GBS Ast mit angegeebene Ident in GBS Baum auffindbar isc.
			return FläceProjeziirtAufGbsAst(
				HeaderButtonMinimizeSictungLezte,
				ScnapscusWindowLezte);
		}

		public GbsElement HeaderButtonCloseBerecne()
		{
			//	Ersaz des Ident des GBS Ast aus ältere Scnapscus durc Ident aus Window aus lezte Scnapscus damit GBS Ast mit angegeebene Ident in GBS Baum auffindbar isc.
			return FläceProjeziirtAufGbsAst(
				HeaderButtonCloseSictungLezte,
				ScnapscusWindowLezte);
		}

		[JsonProperty]
		public GbsElement HeaderFläceOoneSctoierelement
		{
			private set;
			get;
		}

		public string WindowHeaderCaptionText
		{
			get
			{
				var ScnapscusWindowLezte = this.ScnapscusWindowLezte;

				if (null == ScnapscusWindowLezte)
				{
					return null;
				}

				return	ScnapscusWindowLezte.HeaderCaptionText;
			}
		}

		/// <summary>
		/// 2015.09.01
		/// Änderung InGbsBaumAstIndex:
		/// Übernaame von noie Sensor InTreeIndex: Element occludes other Elements with lower Value.
		/// </summary>
		public Int64? ZIndex
		{
			get
			{
				var ScnapscusWindowLezte = this.ScnapscusWindowLezte;

				if (null == ScnapscusWindowLezte)
				{
					return null;
				}

				return ScnapscusWindowLezte.InGbsBaumAstIndex;
			}
		}

		public bool? isModal
		{
			get
			{
				var ScnapscusWindowLezte = this.ScnapscusWindowLezte;

				if (null == ScnapscusWindowLezte)
				{
					return null;
				}

				return ScnapscusWindowLezte.isModal;
			}
		}

		public OrtogoonInt? InGbsFläce
		{
			get
			{
				var ScnapscusWindowLezte = this.ScnapscusWindowLezte;

				if (null == ScnapscusWindowLezte)
				{
					return null;
				}

				return ScnapscusWindowLezte.InGbsFläce;
			}
		}

		public SictGbsWindowZuusctand()
			:
			this(-1,null)
		{
		}

		public SictGbsWindowZuusctand(
			Int64	BeginZait,
			VonSensor.Window ScnapscusWindow)
			:
			base(
			2,
			BeginZait,
			ScnapscusWindow)
		{
		}

		public SictGbsWindowZuusctand(
			SictWertMitZait<VonSensor.Window>	ScnapscusWindowMitZait)
			:
			this(
			ScnapscusWindowMitZait.Zait,
			(null	== ScnapscusWindowMitZait.Wert)	?	null	: ScnapscusWindowMitZait.Wert)
		{
			//	Aktualisiire(ScnapscusWindowMitZait.Zait, ScnapscusWindowMitZait.Wert);
		}

		override	protected void NaacAingangScnapscus(
			Int64 ScnapscusZait,
			VonSensor.Window ScnapscusWert,
			SictGbsAgregatioonAusTransitionInfo ZuusazInfo)
		{
			Aktualisiire(ScnapscusZait, ScnapscusWert, ZuusazInfo);
		}

		void Aktualisiire(
			Int64	Zait,
			VonSensor.Window	ScnapscusWindow,
			SictGbsAgregatioonAusTransitionInfo ZuusazInfo)
		{
			GbsElement HeaderFläceOoneSctoierelement = null;

			try
			{
				var VorherScnapscusWindow = this.ScnapscusWindowLezte;

				{
					var ScnapscusListHaupt = (null == ScnapscusWindow) ? null : ScnapscusWindow.ListHaupt();

					var ListHaupt = this.ListHaupt;

					GbsListGroupedZuusctand.ObjektKonstruktOderAingangScnapscus(
						ref	ListHaupt,
						Zait,
						ScnapscusListHaupt,
						ZuusazInfo);

					this.ListHaupt = ListHaupt;
				}

				if (null != ScnapscusWindow)
				{
					var ScnapscusWindowInGbsFläce = ScnapscusWindow.InGbsFläce;

					{
						if (null != VorherScnapscusWindow)
						{
							if (1 < Optimat.EveOnline.Extension.DiferenzLaageUndGrööseSume(VorherScnapscusWindow.InGbsFläce, ScnapscusWindow.InGbsFläce))
							{
								//	Laage des Window wurde geändert

								HeaderButtonMinimizeSictungLezte = null;
								HeaderButtonCloseSictungLezte = null;
							}
						}

						if (null != ScnapscusWindowInGbsFläce)
						{
							//	Berecnung HeaderFläceOoneSctoierelement

							var AnnaameHeaderFläceHöhe = 10;

							var AnnaameHeaderSctoierelementLinxBraite = 30;

							//	Scon bis zu viir Sctoierelemente auf recter Saite beobactet worden (Inventory)
							var AnnaameHeaderSctoierelementReczBraite = 68;

							HeaderFläceOoneSctoierelement =
								ScnapscusWindow.SictFläceGesezt(
								OrtogoonInt.AusPunktZentrumUndGrööse(
									ScnapscusWindowInGbsFläce.ZentrumLaage +
									new Vektor2DInt(0, (AnnaameHeaderFläceHöhe - ScnapscusWindowInGbsFläce.Grööse.B) / 2),
									new Vektor2DInt(
										ScnapscusWindowInGbsFläce.Grööse.A -
										AnnaameHeaderSctoierelementLinxBraite - AnnaameHeaderSctoierelementReczBraite,
										AnnaameHeaderFläceHöhe)));
						}

						var ScnapscusWindowHeaderButtonMinimize = ScnapscusWindow.HeaderButtonMinimize;
						var ScnapscusWindowHeaderButtonClose = ScnapscusWindow.HeaderButtonClose;

						if (null != ScnapscusWindowHeaderButtonMinimize)
						{
							HeaderButtonMinimizeSictungLezte = ScnapscusWindowHeaderButtonMinimize;
						}

						if (null != ScnapscusWindowHeaderButtonClose)
						{
							HeaderButtonCloseSictungLezte = ScnapscusWindowHeaderButtonClose;
						}
					}
				}
			}
			finally
			{
				this.HeaderFläceOoneSctoierelement = HeaderFläceOoneSctoierelement;
			}
		}
	}
}
