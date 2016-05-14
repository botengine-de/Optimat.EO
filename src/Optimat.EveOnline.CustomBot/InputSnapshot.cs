using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Optimat.EveOnline.Base;

namespace Optimat.EveOnline.CustomBot
{
	public class InputSnapshot
	{
		readonly public Int64 Time;

		readonly public ToCustomBotSnapshot Snapshot;

		readonly public string SourceUri;

		public VonSensorikMesung SensorMeasurement
		{
			get
			{
				var Snapshot = this.Snapshot;

				if (null == Snapshot)
				{
					return null;
				}

				return	Snapshot.MemoryMeasurement.Wert;
			}
		}

		public InputSnapshot(
			Int64 Time,
			ToCustomBotSnapshot Snapshot,
			string SourceUri = null)
		{
			this.Time = Time;
			this.Snapshot = Snapshot;
			this.SourceUri = SourceUri;
		}
	}

	/*
	 * 2015.02.19
	 * 
	public class InputSnapshotFromFile : InputSnapshot
	{
		readonly public string SourceFilePath;

		public InputSnapshotFromFile(
			Int64 Time,
			ToCustomBotSnapshot Snapshot,
			string	SourceFilePath)
			:
			base(Time, Snapshot)
		{
			this.SourceFilePath = SourceFilePath;
		}
	}
	 * */
}
