﻿using BotEngine.Interface;
using MapToOldInterface;
using Sanderling;
using System;

namespace Optimat.EveO.Nuzer
{
	/// <summary>
	/// This Type must reside in an Assembly that can be resolved by the default assembly resolver.
	/// </summary>
	public class InterfaceAppDomainSetup
	{
		static InterfaceAppDomainSetup()
		{
			BotEngine.Interface.InterfaceAppDomainSetup.Setup();
		}
	}

	public partial class App
	{
		readonly Sanderling.SimpleInterfaceServerDispatcher sensorServerDispatcher = new Sanderling.SimpleInterfaceServerDispatcher
		{
			InterfaceAppDomainSetupType = typeof(InterfaceAppDomainSetup),
			InterfaceAppDomainSetupTypeLoadFromMainModule = true,
		};

		void SanderlingMemreadInitConnection()
		{
			sensorServerDispatcher.CyclicExchangeStart();
		}

		Int64 LastMeasurementAttemptTime = 0;

		const int measurementTimeDistanceMin = 1000;

		Sanderling.Interface.MemoryStruct.IMemoryMeasurement SimulateMemoryMeasurement;

		FromProcessMeasurement<Sanderling.Interface.MemoryStruct.IMemoryMeasurement> FromProcessMeasurementLast;

		void GetMeasurementIfDue()
		{
			sensorServerDispatcher?.Exchange();

			Int64? assumptionLastMeasurementTime;

			var requestedMeasurementTime =
				this.RequestedMeasurementTimeKapseltInLog(
					out assumptionLastMeasurementTime);

			var time = Bib3.Glob.StopwatchZaitMiliSictInt();

			var lastMeasurementAttemptAge = time - LastMeasurementAttemptTime;

			if (lastMeasurementAttemptAge < 4000 && (lastMeasurementAttemptAge < measurementTimeDistanceMin || !(requestedMeasurementTime < time)))
				return;

			LastMeasurementAttemptTime = time;

			var eveOnlineClientProcessId = GbsAingaabeZiilProcessId ?? 0;

			var response =
				sensorServerDispatcher?.InterfaceAppManager?.MeasurementTakeRequest(
					eveOnlineClientProcessId,
					Bib3.Glob.StopwatchZaitMiliSictInt());

			var measurementNewStructure = FromProcessMeasurementLast = response?.MemoryMeasurement;

			var simulateMemoryMeasurement = this.SimulateMemoryMeasurement;

			if (null != simulateMemoryMeasurement)
				measurementNewStructure = new FromProcessMeasurement<Sanderling.Interface.MemoryStruct.IMemoryMeasurement>(simulateMemoryMeasurement, time, time, eveOnlineClientProcessId);

			SensorClient.MemoryMeasurementLast = measurementNewStructure;
		}
	}
}
