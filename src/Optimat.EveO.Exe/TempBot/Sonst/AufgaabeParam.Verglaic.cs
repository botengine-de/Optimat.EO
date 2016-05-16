using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bib3;
//using Optimat.EveOnline.AuswertGbs;
using Optimat.EveOnline.VonSensor;
using Optimat.ScpezEveOnln;
using ExtractFromOldAssembly.Bib3;

namespace Optimat.EveOnline.Anwendung
{
	public partial class SictAufgaabeParam
	{
		static public bool HinraicendGlaicwertigFürFortsaz(
			VonSensor.Window O0,
			VonSensor.Window O1)
		{
			if (O0 == O1)
			{
				return true;
			}

			if (null == O0 || null == O1)
			{
				return false;
			}

			return
				MenuWurzelHinraicendGlaicwertigFürFortsaz(
				(GbsElement)O0,
				(GbsElement)O1);
		}

		static public bool HinraicendGlaicwertigFürFortsaz(
			GbsElement[] O0,
			GbsElement[] O1)
		{
			if (O0 == O1)
			{
				return true;
			}

			if (null == O0 || null == O1)
			{
				return false;
			}

			if (!(O0.Length == O1.Length))
			{
				return false;
			}

			return O0.Select((O0GbsObjekt, Index) => MenuWurzelHinraicendGlaicwertigFürFortsaz(O0GbsObjekt, O1[Index])).All((t) => t);
		}

		static public bool HinraicendGlaicwertigFürFortsaz(
			SictAufgaabeParamMausPfaad O0,
			SictAufgaabeParamMausPfaad O1)
		{
			if (O0 == O1)
			{
				return true;
			}

			if (null == O0 || null == O1)
			{
				return false;
			}

			return
				O0.MausTasteLinxAin == O1.MausTasteLinxAin &&
				O0.MausTasteReczAin == O1.MausTasteReczAin &&
				HinraicendGlaicwertigFürFortsaz(O0.ListeWeegpunktGbsObjekt, O1.ListeWeegpunktGbsObjekt);
		}

		static public bool HinraicendGlaicwertigFürFortsaz(
			VonSensor.MenuEntry O0,
			VonSensor.MenuEntry O1)
		{
			if (O0 == O1)
			{
				return true;
			}

			if (null == O0 || null == O1)
			{
				return false;
			}

			return
				string.Equals(O0.Bescriftung, O1.Bescriftung) &&
				O0.EnthältAndere == O1.EnthältAndere &&
				MenuWurzelHinraicendGlaicwertigFürFortsaz(
				(GbsElement)O0,
				(GbsElement)O1);
		}

		static public bool MenuWurzelHinraicendGlaicwertigFürFortsaz(
			OrtogoonInt O0,
			OrtogoonInt O1)
		{
			if (O0 == O1)
			{
				return true;
			}

			if (null == O0 || null == O1)
			{
				return false;
			}

			return O0 == O1;

			return
				(O0.ZentrumLaage - O1.ZentrumLaage).BetraagQuadriirt < 1 &&
				(O0.Grööse - O1.Grööse).BetraagQuadriirt < 1;
		}

		static public bool HinraicendGlaicwertigFürFortsaz(
			SictInventoryItemTransport O0,
			SictInventoryItemTransport O1)
		{
			if (object.ReferenceEquals(O0, O1))
			{
				return true;
			}

			if (null == O0 || null == O1)
			{
				return false;
			}

			return false;
		}

		static public bool HinraicendGlaicwertigFürFortsaz(
			SictAufgaabeParamGbsAstOklusioonVermaidung O0,
			SictAufgaabeParamGbsAstOklusioonVermaidung O1)
		{
			if (O0 == O1)
			{
				return true;
			}

			if (null == O0 || null == O1)
			{
				return false;
			}

			return
				MenuWurzelHinraicendGlaicwertigFürFortsaz(O0.GbsAst, O1.GbsAst) &&
				O0.RestFläceKwadraatSaitenlängeScrankeMin == O1.RestFläceKwadraatSaitenlängeScrankeMin;
		}

		static public bool TailIdentHinraicendGlaicwertigFürFortsaz(
			GbsElement O0,
			GbsElement O1)
		{
			if (O0 == O1)
			{
				return true;
			}

			if (null == O0 || null == O1)
			{
				return false;
			}

			return
				O0.Ident == O1.Ident;
		}

		static public bool MenuWurzelHinraicendGlaicwertigFürFortsaz(
			GbsElement O0,
			GbsElement O1)
		{
			if (O0 == O1)
			{
				return true;
			}

			if (null == O0 || null == O1)
			{
				return false;
			}

			return
				O0.Ident == O1.Ident	&&
				//	2015.02.07	string.Equals(O0.GbsAstName, O1.GbsAstName) &&
				MenuWurzelHinraicendGlaicwertigFürFortsaz(O0.InGbsFläce, O1.InGbsFläce);
		}

		static public bool HinraicendGlaicwertigFürFortsaz(
			VonSensor.LobbyAgentEntry O0,
			VonSensor.LobbyAgentEntry O1)
		{
			if (O0 == O1)
			{
				return true;
			}

			if (null == O0 || null == O1)
			{
				return false;
			}

			return
				string.Equals(O0.AgentTyp, O1.AgentTyp) &&
				string.Equals(O0.AgentName, O1.AgentName) &&
				O0.AgentLevel == O1.AgentLevel;
		}

		static public bool HinraicendGlaicwertigFürFortsaz(
			SictAnforderungMenuKaskaadeAstBedingung O0,
			SictAnforderungMenuKaskaadeAstBedingung O1)
		{
			if (O0 == O1)
			{
				return true;
			}

			if (null == O0 || null == O1)
			{
				return false;
			}

			return
				Bib3.Glob.SequenceEqualPerObjectEquals(O0.ListePrioEntryRegexPattern, O1.ListePrioEntryRegexPattern) &&
				O0.AuswaalEntryVerursactMenuSclus == O1.AuswaalEntryVerursactMenuSclus;
		}

		static public bool HinraicendGlaicwertigFürFortsaz(
			SictAnforderungMenuKaskaadeAstBedingung[] O0,
			SictAnforderungMenuKaskaadeAstBedingung[] O1)
		{
			if (O0 == O1)
			{
				return true;
			}

			if (null == O0 || null == O1)
			{
				return false;
			}

			if (O0.Length != O1.Length)
			{
				return false;
			}

			return O0.Select((O0AstBedingung, AstIndex) => HinraicendGlaicwertigFürFortsaz(O0AstBedingung, O1[AstIndex])).All((t) => t);
		}

		static public bool HinraicendGlaicwertigFürFortsaz(
			SictOptimatParamFitting O0,
			SictOptimatParamFitting O1)
		{
			if (O0 == O1)
			{
				return true;
			}

			if (null == O0 || null == O1)
			{
				return false;
			}

			return
				string.Equals(O0.AusFittingManagementFittingZuLaade, O1.AusFittingManagementFittingZuLaade);
		}

		virtual public bool HinraicendGlaicwertigFürFortsazScpez(
			SictAufgaabeParam Andere)
		{
			return true;
		}

		/// <summary>
		/// Verainfacung Prüüfung:
		/// ale Field welce nit expliziit per Attribut ausgenome wurden mit object.Equals verglaice
		/// per Field Attribut Verglaicfunktioon fesctleege
		/// </summary>
		/// <param name="O0"></param>
		/// <param name="O1"></param>
		/// <returns></returns>
		static public bool HinraicendGlaicwertigFürFortsaz(
			SictAufgaabeParam O0,
			SictAufgaabeParam O1)
		{
			if (object.Equals(O0, O1))
			{
				return true;
			}

			if (null == O0 || null == O1)
			{
				return false;
			}

			if (!O0.HinraicendGlaicwertigFürFortsazScpez(O1))
			{
				return false;
			}

			if (!O1.HinraicendGlaicwertigFürFortsazScpez(O0))
			{
				return false;
			}

			if (!HinraicendGlaicwertigFürFortsaz(O0.AufgaabeParam, O1.AufgaabeParam))
			{
				return false;
			}

			return true;
		}

	}
}
