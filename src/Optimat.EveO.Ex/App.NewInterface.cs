using BotEngine.Interface;
using Sanderling;
using System;
using System.Threading;

namespace Optimat.EveO.Nuzer
{
	public partial class App
	{
		Sanderling.SimpleInterfaceServerDispatcher sensorServerDispatcher = new Sanderling.SimpleInterfaceServerDispatcher();

		void SanderlingMemreadInitConnection()
		{
			var licenseClientConfig = new BotEngine.Client.LicenseClientConfig
			{
				ApiVersionAddress = ExeConfig.ConfigApiVersionAddressDefault,
				Request = new BotEngine.Client.AuthRequest
				{
					ServiceId = ExeConfig.ConfigServiceId,
					LicenseKey = ExeConfig.ConfigLicenseKeyFree,
					Consume = true,
				},
			};

			var licenseClient = new Func<LicenseClient>(() => sensorServerDispatcher.LicenseClient);

			while (!(licenseClient()?.AuthCompleted ?? false))
			{
				sensorServerDispatcher.Exchange(licenseClientConfig);

				Thread.Sleep(1111);
			}

			var AuthResult = licenseClient()?.ExchangeAuthLast?.Value?.Response;

			var LicenseServerSessionId = AuthResult?.SessionId;

			Console.WriteLine("Auth completed, SessionId = " + (LicenseServerSessionId ?? "null"));

			Console.WriteLine("\nstarting to set up the sensor and read from memory.\nthe initial measurement takes longer.");
		}

		Int64 LastMeasurementAttemptTime = 0;

		const int measurementTimeDistanceMin = 1000;

		void GetMeasurement()
		{
			sensorServerDispatcher?.Exchange();

			var time = Bib3.Glob.StopwatchZaitMiliSictInt();

			var lastMeasurementAttemptAge = time - LastMeasurementAttemptTime;

			if (lastMeasurementAttemptAge < measurementTimeDistanceMin)
				return;

			LastMeasurementAttemptTime = time;

			var eveOnlineClientProcessId = GbsAingaabeZiilProcessId ?? 0;

			var response =
				sensorServerDispatcher?.InterfaceAppManager?.MeasurementTakeRequest(
					eveOnlineClientProcessId,
					Bib3.Glob.StopwatchZaitMiliSictInt());

			var measurementNewStructure = response?.MemoryMeasurement;

			SensorClient.MemoryMeasurementLast = measurementNewStructure.MapValue(MapToOldInterface.MapToOldInterface.AsOld);
		}
	}
}
