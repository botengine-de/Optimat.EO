using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Optimat.EveOnline.Anwendung;

namespace Optimat.ScpezEveOnln
{
	[JsonObject(MemberSerialization.OptIn)]
	public class SictAufgaabeKombiZuusctand
	{
		[JsonProperty]
		readonly public int? ListeAufgaabeReegelungDistanceAnzaalScrankeMax;

		[JsonProperty]
		public SictAufgaabeZuusctand ManööverBeleegtAufgaabe
		{
			private set;
			get;
		}

		[JsonProperty]
		public	SictAufgaabeZuusctand	FürVerdrängteAufgaabeZuWarte
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictAufgaabeZuusctand TargetBeleegtAufgaabe
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictAufgaabeZuusctand OverViewTabBeleegtAufgaabe
		{
			private set;
			get;
		}

		[JsonProperty]
		public	SictAufgaabeZuusctand	MausBeleegtAufgaabe
		{
			private set;
			get;
		}

		[JsonProperty]
		public SictAufgaabeZuusctand WindowMinimizeBeleegtAufgaabe
		{
			private set;
			get;
		}

		[JsonProperty]
		readonly List<KeyValuePair<SictAufgaabeZuusctand, Int64?>> InternListeAufgaabeReegelungDistanceMitScpiilraumRest = new List<KeyValuePair<SictAufgaabeZuusctand, Int64?>>();

		[JsonProperty]
		readonly List<KeyValuePair<SictAufgaabeZuusctand, SictShipUiModuleReprZuusctand>> InternListeAufgaabeModuleBeleegt =
			new List<KeyValuePair<SictAufgaabeZuusctand, SictShipUiModuleReprZuusctand>>();

		public IEnumerable<KeyValuePair<SictAufgaabeZuusctand, Int64?>> ListeAufgaabeReegelungDistanceMitScpiilraumRest
		{
			get
			{
				return InternListeAufgaabeReegelungDistanceMitScpiilraumRest;
			}
		}

		static public void AusListeAufgaabeBerecneAnzaalUndScpiilraumRest(
			IEnumerable<KeyValuePair<SictAufgaabeZuusctand, Int64?>> ListeAufgaabeReegelungDistanceMitScpiilraumRest,
			SictAufgaabeZuusctand Aufgaabe,
			out	int AufgaabeAnzaal,
			out	Int64? ScpiilraumRest)
		{
			AufgaabeAnzaal = 0;
			ScpiilraumRest = null;

			if (null == ListeAufgaabeReegelungDistanceMitScpiilraumRest)
			{
				return;
			}

			foreach (var AufgaabeZuBerüksictigeUndScpiilraumRest in ListeAufgaabeReegelungDistanceMitScpiilraumRest)
			{
				var AufgaabeZuBerüksictige = AufgaabeZuBerüksictigeUndScpiilraumRest.Key;

				if (null != AufgaabeZuBerüksictige &&
					null != Aufgaabe)
				{
					if (AufgaabeZuBerüksictige == Aufgaabe)
					{
						continue;
					}

					var MengeKomponente = AufgaabeZuBerüksictige.MengeKomponenteTransitiivBerecne();

					if (null != MengeKomponente)
					{
						if (MengeKomponente.Contains(Aufgaabe))
						{
							//	Aufgaabe isc in bescrankende Ast enthalte daaher werd diiser nit als für Aufgaabe bescrankend gewertet.
							continue;
						}
					}
				}

				++AufgaabeAnzaal;
				ScpiilraumRest = Bib3.Glob.Min(ScpiilraumRest, AufgaabeZuBerüksictigeUndScpiilraumRest.Value);
			}
		}

		public SictAufgaabeKombiZuusctand()
		{
		}

		public SictAufgaabeKombiZuusctand(
			int? ListeAufgaabeReegelungDistanceAnzaalScrankeMax)
		{
			this.ListeAufgaabeReegelungDistanceAnzaalScrankeMax = ListeAufgaabeReegelungDistanceAnzaalScrankeMax;
		}

		public void FürVerdrängteAufgaabeZuWarteSeze(SictAufgaabeZuusctand FürVerdrängteAufgaabeZuWarte)
		{
			if (null != this.FürVerdrängteAufgaabeZuWarte)
			{
				return;
			}

			this.FürVerdrängteAufgaabeZuWarte = FürVerdrängteAufgaabeZuWarte;
		}

		public void ManööverBeleegtAufgaabeSeze(SictAufgaabeZuusctand ManööverBeleegtAufgaabe)
		{
			if (null != this.ManööverBeleegtAufgaabe)
			{
				return;
			}

			this.ManööverBeleegtAufgaabe = ManööverBeleegtAufgaabe;
		}

		public void TargetBeleegtAufgaabeSeze(SictAufgaabeZuusctand TargetBeleegtAufgaabe)
		{
			if (null != this.TargetBeleegtAufgaabe)
			{
				return;
			}

			this.TargetBeleegtAufgaabe = TargetBeleegtAufgaabe;
		}

		public void OverViewTabBeleegtAufgaabeSeze(SictAufgaabeZuusctand OverViewTabBeleegtAufgaabe)
		{
			if (null != this.OverViewTabBeleegtAufgaabe)
			{
				return;
			}

			this.OverViewTabBeleegtAufgaabe = OverViewTabBeleegtAufgaabe;
		}

		public void MausBeleegtAufgaabeSeze(SictAufgaabeZuusctand MausBeleegtAufgaabe)
		{
			if (null != this.MausBeleegtAufgaabe)
			{
				return;
			}

			this.MausBeleegtAufgaabe = MausBeleegtAufgaabe;
		}

		public void WindowMinimizeBeleegtAufgaabeSeze(SictAufgaabeZuusctand WindowMinimizeBeleegtAufgaabe)
		{
			if (null != this.WindowMinimizeBeleegtAufgaabe)
			{
				return;
			}

			this.WindowMinimizeBeleegtAufgaabe = WindowMinimizeBeleegtAufgaabe;
		}

		public bool WindowMinimizeBeleegungFraigaabeFürAufgaabePfaad(IEnumerable<SictAufgaabeZuusctand> AufgaabePfaad)
		{
			var WindowMinimizeBeleegtAufgaabe = this.WindowMinimizeBeleegtAufgaabe;

			if (null == WindowMinimizeBeleegtAufgaabe)
			{
				return true;
			}

			return AufgaabeEnthalteInAufgaabePfaad(
				WindowMinimizeBeleegtAufgaabe,
				AufgaabePfaad);
		}

		public bool MausBeleegungFraigaabeFürAufgaabePfaad(IEnumerable<SictAufgaabeZuusctand> AufgaabePfaad)
		{
			var MausBeleegtAufgaabe = this.MausBeleegtAufgaabe;

			if (null == MausBeleegtAufgaabe)
			{
				return true;
			}

			return AufgaabeEnthalteInAufgaabePfaad(
				MausBeleegtAufgaabe,
				AufgaabePfaad);
		}

		public bool	AufgaabeEnthalteInAufgaabePfaad(
			SictAufgaabeZuusctand	Aufgaabe,
			IEnumerable<SictAufgaabeZuusctand> AufgaabePfaad)
		{
			if (null == AufgaabePfaad)
			{
				return false;
			}

			return AufgaabePfaad.Contains(Aufgaabe);
		}

		public void ListeAufgaabeReegelungDistanceFüügeAin(
			SictAufgaabeZuusctand Aufgaabe,
			Int64? ScpiilraumRest)
		{
			if (null == Aufgaabe)
			{
				return;
			}

			InternListeAufgaabeReegelungDistanceMitScpiilraumRest.Add(
				new KeyValuePair<SictAufgaabeZuusctand, Int64?>(Aufgaabe, ScpiilraumRest));
		}

		public void ListeAufgaabeModuleBeleegtFüügeAin(
			SictAufgaabeZuusctand Aufgaabe)
		{
			if (null == Aufgaabe)
			{
				return;
			}

			var AufgaabeParam = Aufgaabe.AufgaabeParam	as	AufgaabeParamAndere;

			if (null == AufgaabeParam)
			{
				return;
			}

			var Module =
				AufgaabeParam.ModuleScalteUm ??
				AufgaabeParam.ModuleScalteAin ??
				AufgaabeParam.ModuleScalteAus;

			if (null == Module)
			{
				return;
			}

			InternListeAufgaabeModuleBeleegt.Add(new KeyValuePair<SictAufgaabeZuusctand, SictShipUiModuleReprZuusctand>(Aufgaabe, Module));
		}

		public bool ModuleVerwendungFraigaabe(SictShipUiModuleReprZuusctand Module)
		{
			var InternListeAufgaabeModuleBeleegt = this.InternListeAufgaabeModuleBeleegt;

			if (null == InternListeAufgaabeModuleBeleegt)
			{
				return true;
			}

			return !InternListeAufgaabeModuleBeleegt.Any((Kandidaat) => Kandidaat.Value == Module);
		}

		public bool AufgaabeReegelungDistanceFürAufgaabeFraigaabeBerecne(
			SictAufgaabeZuusctand Aufgaabe)
		{
			int AufgaabeAnzaal;
			Int64? ScpiilraumRest;

			AusListeAufgaabeBerecneAnzaalUndScpiilraumRest(
				InternListeAufgaabeReegelungDistanceMitScpiilraumRest,
				Aufgaabe,
				out	AufgaabeAnzaal,
				out	ScpiilraumRest);

			if (ListeAufgaabeReegelungDistanceAnzaalScrankeMax <= AufgaabeAnzaal)
			{
				return false;
			}

			if (ScpiilraumRest < 1111)
			{
				return false;
			}

			return true;
		}

	}

}
