using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bib3;
using Newtonsoft.Json;
//using Optimat.EveOnline.AuswertGbs;
using Optimat.EveOnline.VonSensor;
using Optimat.ScpezEveOnln;
using ExtractFromOldAssembly.Bib3;

namespace Optimat.EveOnline.Anwendung
{
	[JsonObject(MemberSerialization.OptIn)]
	public class AufgaabeParamScrollToTop : SictAufgaabeParam
	{
		/// <summary>
		/// 2015.01.05	Beobactung:
		/// Automaat klikt in Overview Scroll zu wait Linx.
		/// Wen di Maus Kante des vorderste Window beweegt werd isc jedoc kain Versaz zu erkene.
		/// Daher werd beobacteter Versaz vorersct als für Scroll scpeziifisc angenome.
		/// </summary>
		static readonly public Vektor2DInt StatScrollHandleVersaz = new Vektor2DInt(1, 0);

		[JsonProperty]
		readonly public VonSensor.Scroll AusScnapscusScroll;

		public AufgaabeParamScrollToTop()
		{
		}

		public AufgaabeParamScrollToTop(
			VonSensor.Scroll AusScnapscusScroll)
		{
			this.AusScnapscusScroll = AusScnapscusScroll;
		}

		override	public SictAufgaabeParamZerleegungErgeebnis Zerleege(
			ISictAutomatZuusctand AutomaatZuusctand,
			SictAufgaabeKombiZuusctand KombiZuusctand)
		{
			return ZerleegeScrollNaacOobe(AutomaatZuusctand, AusScnapscusScroll);
		}

		override public IEnumerable<string> ZwekListeKomponenteAusParamBerecne()
		{
			//	Hiir von ISictAutomaat das deen Titel des Window bescafe welcer das AusScnapscusScroll enthalt.

			return new string[] { "Scroll to Top" };
		}

		override public bool HinraicendGlaicwertigFürFortsazScpez(
			SictAufgaabeParam Andere)
		{
			var AndereScpez = Andere as AufgaabeParamScrollToTop;

			if (null == AndereScpez)
			{
				return false;
			}

			return GbsElement.Identisc(this.AusScnapscusScroll, AndereScpez.AusScnapscusScroll);
		}

		static public SictAufgaabeParamZerleegungErgeebnis ZerleegeScrollNaacOobe(
			ISictAutomatZuusctand AutomaatZuusctand,
			VonSensor.Scroll AusScnapscusScroll)
		{
			var AufgaabeParamZerleegungErgeebnis = new SictAufgaabeParamZerleegungErgeebnis(null,	true);

			if (null == AusScnapscusScroll)
			{
				return AufgaabeParamZerleegungErgeebnis;
			}

			var AusScnapscusScrollScrollHandleGrenze = AusScnapscusScroll.ScrollHandleGrenze;

			var AusScnapscusScrollScrollHandleGrenzeFläce = AusScnapscusScrollScrollHandleGrenze.InGbsFläceNullable() ?? OrtogoonInt.Leer;

			if (AusScnapscusScrollScrollHandleGrenzeFläce.IsLeer)
			{
				//	wen kain ScrollHandle vorhande dan isc nit genüügend Inhalt zum Scrole vorhande, d.h. Viewport raict beraits bis Top -> Erfolg.
				return AufgaabeParamZerleegungErgeebnis;
			}

			var FläceHöhe = 8;

			var FürScrolKlikFläce = OrtogoonInt.AusPunktZentrumUndGrööse(
				new Vektor2DInt(
				AusScnapscusScrollScrollHandleGrenzeFläce.ZentrumLaage.A,
				AusScnapscusScrollScrollHandleGrenzeFläce.PunktMin.B + FläceHöhe / 2)
				+
				StatScrollHandleVersaz,
				new Vektor2DInt(6, FläceHöhe));

			var KlikZiilFäce = SictGbsWindowZuusctand.FläceProjeziirtAufGbsAst(FürScrolKlikFläce, AusScnapscusScrollScrollHandleGrenze);

			AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.KonstruktMausPfaad(
				SictAufgaabeParamMausPfaad.KonstruktMausKlikLinx(KlikZiilFäce)));

			return AufgaabeParamZerleegungErgeebnis;
		}
	}
}
