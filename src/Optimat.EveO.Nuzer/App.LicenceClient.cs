using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bib3;

namespace Optimat.EveO.Nuzer
{
	public partial class App
	{
		//	const string TestServerUri = @"http://localhost:1138/api";

		WertZuZaitpunktStruct<BotEngine.Interface.LicenseClient> LicenseClient;

		const int LicenseClientKümereZaitDistanz = 1000;

		CallRateScranke LicenseClientKümereRateBescranke = null;

		readonly object LicenseClientLock = new object();

		public bool LicenseClientStatusOk
		{
			get
			{
				var LicenseClient = this.LicenseClient;

				if (null == LicenseClient.Wert)
				{
					return false;
				}

				if (!LicenseClient.Wert.AuthCompleted)
				{
					return false;
				}

				var HttpExchangeSuccessfulLast = LicenseClient.Wert.HttpExchangeSuccessfulLast;

				if (null == HttpExchangeSuccessfulLast)
				{
					return false;
				}

				if (null == HttpExchangeSuccessfulLast.Wert)
				{
					return false;
				}

				var HttpExchangeSuccessfulLastAlter = Bib3.Glob.StopwatchZaitMiliSictInt() - HttpExchangeSuccessfulLast.BeginZait;

				return HttpExchangeSuccessfulLastAlter < 4000;
			}
		}

		void LicenseClientKümere()
		{
			lock (LicenseClientLock)
			{
				var Zait = Bib3.Glob.StopwatchZaitMiliSictInt();

				if (null == LicenseClient.Wert)
				{
					LicenseClient = new WertZuZaitpunktStruct<BotEngine.Interface.LicenseClient>(
						new BotEngine.Interface.LicenseClient(), Zait);
				}

				Task.Run(() =>
				{
					LicenseClient.Wert.Timeout = 4000;

					LicenseClient.Wert.ServerAddress = GbsAingaabeSensorServerApiUri;

					if (LicenseClient.Wert.AuthCompleted)
					{
						var FromSensorAppManagerMessage = SensorClient.ToServer();

						var ToServerMessage = new BotEngine.Interface.FromClientToServerMessage()
						{
							Time = Bib3.Glob.StopwatchZaitMiliSictInt(),
							Interface = FromSensorAppManagerMessage,
						};

						var FromServerMessage = LicenseClient.Wert.ExchangePayload(ToServerMessage);

						if (null != FromServerMessage)
						{
							SensorClient.FromServer(FromServerMessage.Interface);
						}
					}
					else
					{
						LicenseClient.Wert.ExchangeAuth();
					}
				});
			}
		}
	}
}
