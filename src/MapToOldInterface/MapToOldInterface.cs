using Sanderling.Parse;
using System;

namespace MapToOldInterface
{
	static public class MapToOldInterface
	{
		static public BotEngine.EveOnline.Interface.MemoryStruct.MemoryMeasurement AsOld(
			this Sanderling.Interface.MemoryStruct.IMemoryMeasurement memoryMeasurement)
		{
			var parsed = memoryMeasurement?.Parse();

			throw new NotImplementedException();
		}
	}
}
