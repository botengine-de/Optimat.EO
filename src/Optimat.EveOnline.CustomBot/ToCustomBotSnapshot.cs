using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bib3;
using BotEngine.Common;
using BotEngine;
using ExtractFromOldAssembly.Bib3;

namespace Optimat.EveOnline.CustomBot
{
	/// <summary>
	/// Measurement should be started if the two conditions are met:
	/// +RequestedMeasurementTime <= Current Time
	/// +(null == AssumptionLastMeasurementTime) || (End Time of the Last Measurement taken <= AssumptionLastMeasurementTime)
	/// </summary>
	public class FromCustomBotSnapshot
	{
		public int? RequestedMeasurementProcessId;

		public Int64? MeasurementMemoryRequestTime;

		public Int64? MeasurementWindowRequestTime;

		public Int64? MeasurementMemoryReceivedLastTime;

		public Int64? MeasurementWindowReceivedLastTime;

		public FromCustomBotSnapshot()
		{
		}

		public FromCustomBotSnapshot(
			int? RequestedMeasurementProcessId,
			Int64? MeasurementMemoryRequestTime	= null,
			Int64? MeasurementWindowRequestTime	= null,
			Int64? MeasurementMemoryReceivedLastTime = null,
			Int64? MeasurementWindowReceivedLastTime = null)
		{
			this.RequestedMeasurementProcessId = RequestedMeasurementProcessId;
			this.MeasurementMemoryRequestTime = MeasurementMemoryRequestTime;
			this.MeasurementWindowRequestTime = MeasurementWindowRequestTime;
			this.MeasurementMemoryReceivedLastTime = MeasurementMemoryReceivedLastTime;
			this.MeasurementWindowReceivedLastTime = MeasurementWindowReceivedLastTime;
		}
	}

	public class WindowMesung
	{
		readonly public Raster2D<UInt32> ClientRectRaster;

		readonly public OrtogoonInt ClientRect;

		readonly public string WindowTitle;

		public WindowMesung()
		{
		}

		public WindowMesung(
			Raster2D<UInt32> ClientRectRaster,
			OrtogoonInt ClientRect,
			string WindowTitle = null)
		{
			this.ClientRectRaster = ClientRectRaster;
			this.ClientRect = ClientRect;
			this.WindowTitle = WindowTitle;
		}
	}

	public class ToCustomBotSnapshot
	{
		readonly public Int64 Time;

		/*
		 * 2015.03.03
		 * 
		readonly public int? SensorProcessId;

		readonly public WertZuZaitraumStruct<VonSensorikMesung> SensorMeasurementInTimeframe;
		 * */
		readonly public VonProcessMesung<VonSensorikMesung> MemoryMeasurement;

		/// <summary>
		/// Area is Window Client Area.
		/// </summary>
		readonly public VonProcessMesung<WindowMesung> WindowMeasurement;

		public ToCustomBotSnapshot()
		{
		}

		public ToCustomBotSnapshot(
			Int64 Time,
			VonProcessMesung<VonSensorikMesung> MemoryMeasurement = null,
			VonProcessMesung<WindowMesung> WindowMeasurement = null)
		{
			this.Time = Time;
			this.MemoryMeasurement = MemoryMeasurement;
			this.WindowMeasurement = WindowMeasurement;
		}

		static public Bib3.RefNezDiferenz.SictMengeTypeBehandlungRictliinie SerialisPolicyConstruct()
		{
			return Bib3.RefNezDiferenz.NewtonsoftJson.SictMengeTypeBehandlungRictliinieNewtonsoftJson.KonstruktMengeTypeBehandlungRictliinie();
		}

		static Bib3.RefNezDiferenz.SictMengeTypeBehandlungRictliinie SerialisPolicy = SerialisPolicyConstruct();

		static Bib3.RefNezDiferenz.SictTypeBehandlungRictliinieMitTransportIdentScatescpaicer SerialisPolicyAndCache =
			new Bib3.RefNezDiferenz.SictTypeBehandlungRictliinieMitTransportIdentScatescpaicer(SerialisPolicy);

		static public string SerializeToString<T>(T Snapshot)
		{
			return Bib3.RefNezDiferenz.Extension.WurzelSerialisiireZuJson(Snapshot, SerialisPolicyAndCache);
		}

		static public byte[] SerializeToUTF8<T>(T Snapshot)
		{
			return Encoding.UTF8.GetBytes(SerializeToString(Snapshot));
		}

		static public T DeserializeFromString<T>(string Json)
		{
			var ListRoot = Bib3.RefNezDiferenz.Extension.ListeWurzelDeserialisiireVonJson(Json, SerialisPolicyAndCache);

			return (T)ListRoot.FirstOrDefaultNullable();
		}

		static public T DeserializeFromUTF8<T>(byte[] Utf8)
		{
			if (null == Utf8)
			{
				return default(T);
			}

			return DeserializeFromString<T>(Encoding.UTF8.GetString(Utf8));
		}
	}
}
