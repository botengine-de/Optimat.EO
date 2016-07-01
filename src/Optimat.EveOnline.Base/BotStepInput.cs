using System;

namespace Optimat.EveOnline.Base
{
	public class MotionResult
	{
		public Int64 MotionRecommendationId;

		public bool Success;
	}

	public class BotStepInput
	{
		public Int64 TimeMilli;

		public BotEngine.Interface.FromProcessMeasurement<Sanderling.Interface.MemoryStruct.IMemoryMeasurement> FromProcessMemoryMeasurement;

		public string PreferencesSerial;

		public MotionResult[] StepLastMotionResult;
	}
}
