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
using Optimat.GBS;

namespace Optimat.EveOnline.GBS
{
	/// <summary>
	/// Interaction logic for MengeMissionKonfig.xaml
	/// </summary>
	public partial class SictMengeMissionTypKonfig : UserControl
	{
		ObservableCollection<SictObservable<SictMissionTypKonfigReprInDataGrid>> MengeMissionReprInDataGrid =
			new ObservableCollection<SictObservable<SictMissionTypKonfigReprInDataGrid>>();

		public SictMengeMissionTypKonfig()
		{
			InitializeComponent();
		}
	}

	public class SictMissionTypKonfigReprInDataGrid
	{
		static public string FactionSictStringFürNuzer(SictFactionSictEnum? Faction)
		{
			if (!Faction.HasValue)
			{
				return null;
			}

			return Faction.Value.ToString();
		}

		readonly SictMissionTypKonfig Repräsentiirte;

		public SictMissionTypKonfigReprInDataGrid(
			SictMissionTypKonfig Repräsentiirte)
		{
			this.Repräsentiirte = Repräsentiirte;
		}

		public int? AgentLevel
		{
			get
			{
				var Repräsentiirte = this.Repräsentiirte;

				return (null == Repräsentiirte) ? null : Repräsentiirte.AgentLevel;
			}
		}

		public string MissionTitelRegexPattern
		{
			get
			{
				var Repräsentiirte = this.Repräsentiirte;

				return (null == Repräsentiirte) ? null : Repräsentiirte.MissionTitelRegexPattern;
			}
		}

		public string MengeFactionAggrString
		{
			get
			{
				var Repräsentiirte = this.Repräsentiirte;

				if(null	== Repräsentiirte)
				{
					return	null;
				}

				var	MengeFaction	= Repräsentiirte.MengeFaction;

				if (null == MengeFaction)
				{
					return null;
				}

				var MengeFactionAggrString = string.Join(",", MengeFaction.Select((Faction) => FactionSictStringFürNuzer(Faction)).ToArray());

				return MengeFactionAggrString;
			}
		}

		public bool? FüüreAusFraigaabe
		{
			get
			{
				var Repräsentiirte = this.Repräsentiirte;

				return (null == Repräsentiirte) ? null : Repräsentiirte.FüüreAusFraigaabe;
			}
		}

		public bool? OfferAcceptFraigaabe
		{
			get
			{
				var Repräsentiirte = this.Repräsentiirte;

				return (null == Repräsentiirte) ? null : Repräsentiirte.OfferAcceptFraigaabe;
			}
		}

		public bool? OfferDeclineFraigaabe
		{
			get
			{
				var Repräsentiirte = this.Repräsentiirte;

				return (null == Repräsentiirte) ? null : Repräsentiirte.OfferDeclineFraigaabe;
			}
		}


	}
}
