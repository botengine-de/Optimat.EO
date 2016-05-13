using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VonSensor=Optimat.EveOnline.VonSensor;
using Optimat.ScpezEveOnln;
using Optimat.EveOnline.VonSensor;
using Bib3;


namespace Optimat.EveOnline.Anwendung
{
	[JsonObject(MemberSerialization.OptIn)]
	public class AufgaabeParamScrollDown : SictAufgaabeParam
	{
		[JsonProperty]
		readonly public VonSensor.Scroll AusScnapscusScroll;

		public AufgaabeParamScrollDown()
		{
		}

		public AufgaabeParamScrollDown(
			VonSensor.Scroll AusScnapscusScroll)
		{
			this.AusScnapscusScroll = AusScnapscusScroll;
		}

		override	public SictAufgaabeParamZerleegungErgeebnis Zerleege(
			ISictAutomatZuusctand AutomaatZuusctand,
			SictAufgaabeKombiZuusctand KombiZuusctand)
		{
			return ZerleegeScrollNaacUnte(AutomaatZuusctand, AusScnapscusScroll);
		}

		override public IEnumerable<string> ZwekListeKomponenteAusParamBerecne()
		{
			//	Hiir von ISictAutomaat das deen Titel des Window bescafe welcer das AusScnapscusScroll enthalt.

			return new string[] { "Scroll down" };
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

		static public SictAufgaabeParamZerleegungErgeebnis ZerleegeScrollNaacUnte(
			ISictAutomatZuusctand AutomaatZuusctand,
			VonSensor.Scroll AusScnapscusScroll)
		{
			var AufgaabeParamZerleegungErgeebnis = new SictAufgaabeParamZerleegungErgeebnis(null,	true);

			if (null == AusScnapscusScroll)
			{
				return AufgaabeParamZerleegungErgeebnis;
			}

			var AusScnapscusScrollScrollHandleGrenze = AusScnapscusScroll.ScrollHandleGrenze;

			var AusScnapscusScrollScrollHandle = AusScnapscusScroll.ScrollHandle;

			var AusScnapscusScrollScrollHandleGrenzeFläce = AusScnapscusScrollScrollHandleGrenze.InGbsFläceNullable() ?? OrtogoonInt.Leer;
			var AusScnapscusScrollScrollHandleFläce = AusScnapscusScrollScrollHandle.InGbsFläceNullable() ?? OrtogoonInt.Leer;

			if (AusScnapscusScrollScrollHandleGrenzeFläce.IsLeer || AusScnapscusScrollScrollHandleFläce.IsLeer)
			{
				//	wen kain ScrollHandle vorhande dan isc nit genüügend Inhalt zum Scrole vorhande, d.h. Viewport raict beraits bis Top -> Erfolg.
				return AufgaabeParamZerleegungErgeebnis;
			}

			var FürScrolKlikFläce = OrtogoonInt.AusPunktZentrumUndGrööse(
				new Vektor2DInt(
				AusScnapscusScrollScrollHandleGrenzeFläce.ZentrumLaage.A,
				AusScnapscusScrollScrollHandleFläce.ZentrumLaage.B +
				AusScnapscusScrollScrollHandleFläce.Grööse.B / 2 + 3)
				+
				AufgaabeParamScrollToTop.StatScrollHandleVersaz,
				new Vektor2DInt(AusScnapscusScrollScrollHandleFläce.Grööse.A, 6));

			var KlikZiilFäce = SictGbsWindowZuusctand.FläceProjeziirtAufGbsAst(FürScrolKlikFläce, AusScnapscusScrollScrollHandleGrenze);

			AufgaabeParamZerleegungErgeebnis.FüügeAn(AufgaabeParamAndere.KonstruktMausPfaad(
				SictAufgaabeParamMausPfaad.KonstruktMausKlikLinx(KlikZiilFäce)));

			return AufgaabeParamZerleegungErgeebnis;
		}
	}
}
