using ExtractFromOldAssembly;
using MapToOldInterface;

namespace Optimat.EveOnline.CustomBot
{
	static public class Extension
	{
		static public ToCustomBotSnapshot AlsToCustomBotSnapshot(this BotEngine.EveOnline.Interface.FromSensorToConsumerMessage message) =>
			null == message ? null :
			new ToCustomBotSnapshot(
				message.Time,
				message.MemoryMeasurement.AlsVonProcessMesung().Sict(measurement => measurement?.AsOld()?.AlsVonSensorikMesung()),
				message.WindowMeasurement.AlsVonProcessMesung().Sict(AlsWindowMesung));

		static public WindowMesung AlsWindowMesung(this BotEngine.Interface.WindowMeasurement windowMeasurement) =>
			null == windowMeasurement ? null :
			new WindowMesung(windowMeasurement.ClientRectRaster, windowMeasurement.ClientRect.AsOrtogoonInt(), windowMeasurement.WindowTitle);
	}
}
