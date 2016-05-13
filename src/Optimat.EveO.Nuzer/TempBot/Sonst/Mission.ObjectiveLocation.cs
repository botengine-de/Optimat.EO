using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Optimat.EveOnline.Base;
//using Optimat.EveOnline.AuswertGbs;

namespace Optimat.EveOnline.Anwendung
{
	[JsonObject(MemberSerialization.OptIn)]
	public class SictObjectiveLocationAuswert
	{
		[JsonProperty]
		public VonSensor.WindowAgentMissionObjectiveObjective Objective
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? LocationErfordertDock
		{
			private set;
			get;
		}

		[JsonProperty]
		public bool? LocationErraict
		{
			private set;
			get;
		}

		/// <summary>
		/// Sonesystem der Location isc erraict.
		/// </summary>
		[JsonProperty]
		public bool? LocationErraictTailSystem
		{
			private set;
			get;
		}

		/// <summary>
		/// Das in InfoPanelRoute gesezte Ziil hat glaices Sonesysteem wii Location.
		/// </summary>
		[JsonProperty]
		public bool? LocationSystemGlaicInfoPanelRouteDestinationSystem
		{
			private set;
			get;
		}

		public SictObjectiveLocationAuswert()
		{
		}

		public SictObjectiveLocationAuswert(VonSensor.WindowAgentMissionObjectiveObjective Objective)
		{
			this.Objective = Objective;
		}

		public void Berecne(VonSensorikMesung AusScnapscusAuswertungZuusctand)
		{
			bool? LocationErfordertDock = null;
			bool? LocationErraict = null;
			bool? LocationErraictTailSystem = null;
			bool? LocationSystemGlaicInfoPanelRouteDestinationSystem = null;

			try
			{
				var Objective = this.Objective;

				if (null == Objective)
				{
					return;
				}

				var ObjectiveLocation = Objective.Location;

				if (null == ObjectiveLocation)
				{
					return;
				}

				if (null == AusScnapscusAuswertungZuusctand)
				{
					return;
				}

				var CurrentLocation =
					(null == AusScnapscusAuswertungZuusctand) ? null : AusScnapscusAuswertungZuusctand.CurrentLocationInfo();

				var InfoPanelRoute = (null == AusScnapscusAuswertungZuusctand) ? null : AusScnapscusAuswertungZuusctand.InfoPanelRoute;

				var InfoPanelRouteCurrent = (null == InfoPanelRoute) ? null : InfoPanelRoute.CurrentInfo;
				var InfoPanelRouteEnd = (null == InfoPanelRoute) ? null : InfoPanelRoute.EndInfo;

				var Docked = (null == AusScnapscusAuswertungZuusctand) ? null : AusScnapscusAuswertungZuusctand.Docked();

				LocationErfordertDock =
					SictMissionObjectiveObjectiveElementTyp.LocationPickUp == Objective.Typ ||
					SictMissionObjectiveObjectiveElementTyp.LocationDropOff == Objective.Typ;

				var ObjectiveLocationTailSystem = ObjectiveLocation.LocationNameTailSystem;

				var CurrentLocationSolarSystemName = (null == CurrentLocation) ? null : CurrentLocation.SolarSystemName;

				if (null != CurrentLocation)
				{
					LocationErraictTailSystem =
						(null	== CurrentLocationSolarSystemName)	?	false	:
						((null	== ObjectiveLocationTailSystem)	?	(bool?)null	:
						ObjectiveLocationTailSystem.Contains(CurrentLocationSolarSystemName));

					LocationErraict =
						(true == LocationErraictTailSystem) &&
						string.Equals(ObjectiveLocation.LocationName, CurrentLocation.NearestName) &&
						((false == LocationErfordertDock) || (true == Docked));
				}

				if (null == InfoPanelRouteEnd)
				{
					//	Wen Ziil der Route nur ain Systeem entfernt ist dan scteet Ziil nit in EndInfo sondern in CurrentInfo.

					if (null == InfoPanelRouteCurrent)
					{
						//	Wen auc kain CurrentInfo vorhande dan isc warscainlic noc kaine Route gesezt.
						LocationSystemGlaicInfoPanelRouteDestinationSystem = false;
					}
					else
					{
						var SolarSystemName = InfoPanelRouteCurrent.SolarSystemName;

						if (null != ObjectiveLocationTailSystem	&&
							null != SolarSystemName)
						{
							LocationSystemGlaicInfoPanelRouteDestinationSystem =
								ObjectiveLocationTailSystem.Contains(InfoPanelRouteCurrent.SolarSystemName);
						}
					}
				}
				else
				{
					if (null != ObjectiveLocationTailSystem)
					{
						LocationSystemGlaicInfoPanelRouteDestinationSystem =
							ObjectiveLocationTailSystem.Contains(InfoPanelRouteEnd.SolarSystemName);
					}
				}
			}
			finally
			{
				this.LocationErfordertDock = LocationErfordertDock;
				this.LocationErraict = LocationErraict;
				this.LocationErraictTailSystem = LocationErraictTailSystem;
				this.LocationSystemGlaicInfoPanelRouteDestinationSystem = LocationSystemGlaicInfoPanelRouteDestinationSystem;
			}
		}
	}

}
