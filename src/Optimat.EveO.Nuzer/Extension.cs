using BotEngine.EveOnline.Interface;
using Optimat.EveOnline.CustomBot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimat.EveO.Nuzer
{
	static public class Extension
	{
		static public FromSensorToConsumerMessage OoneMesungWindow(
			this FromSensorToConsumerMessage ToCustomBotSnapshot)
		{
			if (null == ToCustomBotSnapshot)
			{
				return null;
			}

			return new FromSensorToConsumerMessage()
			{
				Time = ToCustomBotSnapshot.Time,
				MemoryMeasurement = ToCustomBotSnapshot.MemoryMeasurement,
			};
		}
	}
}
