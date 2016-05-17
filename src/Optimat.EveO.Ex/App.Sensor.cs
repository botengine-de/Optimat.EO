using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bib3;
using Optimat.EveOnline.CustomBot;
using BotEngine.EveOnline.Interface;

namespace Optimat.EveO.Nuzer
{
	public partial class App
	{
		const int SensorKümereZaitDistanz = 100;
		CallRateScranke SensorKümereRateBescranke = null;

		readonly object SensorLock = new object();

		static public readonly Bib3.RefNezDiferenz.SictTypeBehandlungRictliinieMitTransportIdentScatescpaicer
			ScnitCustSictIdTypeBehandlungRictliinieUndScatescpaicer =
			new Bib3.RefNezDiferenz.SictTypeBehandlungRictliinieMitTransportIdentScatescpaicer(
				Optimat.EveOnline.Extension.GbsBaumMengeTypeBehandlungRictliinieKonstrukt());

		readonly SensorClient SensorClient = new SensorClient();

		public ToCustomBotSnapshot SensorSnapshotLastAgrClassic =>
			SensorSnapshotLastAgr.AlsToCustomBotSnapshot();

		public FromSensorToConsumerMessage SensorSnapshotLastAgr
		{
			get
			{
				if (null == SensorClient)
				{
					return null;
				}

				var MemoryMeasurementLast = SensorClient?.MemoryMeasurementLast;
				var WindowMeasurementLast = SensorClient?.WindowMeasurementLast;

				return new FromSensorToConsumerMessage()
				{
					Time = MemoryMeasurementLast?.End ?? 0,
					MemoryMeasurement = MemoryMeasurementLast,
					WindowMeasurement = WindowMeasurementLast,
				};
			}
		}

		static Optimat.EveOnline.CustomBot.InputSnapshot AlsInputSnapshot(
			Optimat.EveOnline.CustomBot.ToCustomBotSnapshot SensorSnapshot)
		{
			if (null == SensorSnapshot)
			{
				return null;
			}

			return new Optimat.EveOnline.CustomBot.InputSnapshot(SensorSnapshot.Time, SensorSnapshot);
		}

		public Bib3.FCL.GBS.SictSymboolAkzeptanzFeelerWarnung.SymboolTypSictEnum SensorClientStatusSymbool
		{
			get
			{
				var SensorClientStatus = this.SensorClientStatus;

				if (SensorClientStatus.HasValue)
				{
					return
						SensorClientStatus.Value ?
						Bib3.FCL.GBS.SictSymboolAkzeptanzFeelerWarnung.SymboolTypSictEnum.Akzeptanz :
						Bib3.FCL.GBS.SictSymboolAkzeptanzFeelerWarnung.SymboolTypSictEnum.Feeler;
				}

				return Bib3.FCL.GBS.SictSymboolAkzeptanzFeelerWarnung.SymboolTypSictEnum.InArbait;
			}
		}

		public bool? SensorClientStatus
		{
			get
			{
				var SensorSnapshotLast = this.SensorSnapshotLastAgrClassic;

				if (null == SensorSnapshotLast)
				{
					return null;
				}

				var SensorMeasurementInTimeframe = SensorSnapshotLast.MemoryMeasurement;

				if (null == SensorMeasurementInTimeframe)
				{
					return null;
				}

				if (!(SensorMeasurementInTimeframe.ProcessId == GbsAingaabeZiilProcessId))
				{
					return null;
				}

				if (null == SensorMeasurementInTimeframe.Mesung)
				{
					return false;
				}

				var SensorMeasurementAlter = Bib3.Glob.StopwatchZaitMiliSictInt() - SensorMeasurementInTimeframe.BeginZait;

				if (1e+4 < SensorMeasurementAlter)
				{
					return false;
				}

				return true;
			}
		}

		void SensorKümere()
		{
			lock (SensorLock)
			{
				var CustomBotServer = this.CustomBotServer;

				Int64? AssumptionLastMeasurementTime;

				var RequestedMeasurementTime =
					this.RequestedMeasurementTimeKapseltInLog(
						out AssumptionLastMeasurementTime);

				var FromBotSnapshot =
					new BotEngine.EveOnline.Interface.FromConsumerToSensorMessage()
					{
						RequestedMeasurementProcessId = GbsAingaabeZiilProcessId,
						MeasurementMemoryRequestTime = RequestedMeasurementTime,
						MeasurementMemoryReceivedLastTime = AssumptionLastMeasurementTime,
						MeasurementWindowRequestTime = SensorClient?.WindowMeasurementLast?.End,
					};

				/*
				2015.08.29

				if (null != CustomBotServer)
				{
					FromBotSnapshot = CustomBotServer.VonBotScnapscusLezte;
				}
				*/

				if (null != FromBotSnapshot)
				{
					if (!FromBotSnapshot.RequestedMeasurementProcessId.HasValue)
					{
						FromBotSnapshot.RequestedMeasurementProcessId = GbsAingaabeZiilProcessId;
					}
				}

				SensorClient.SensorExchange(FromBotSnapshot);
			}
		}

	}
}
