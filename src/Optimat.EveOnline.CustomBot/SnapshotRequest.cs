using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Optimat;


namespace Optimat.EveOnline.CustomBot
{
	public class SnapshotRequestResult
	{
		readonly public Int64 Time;
		readonly public System.Exception Exception;
		readonly public ToCustomBotSnapshot ResponseSnapshot;


		public SnapshotRequestResult(
			Int64 Time,
			System.Exception Exception,
			ToCustomBotSnapshot ResponseSnapshot)
		{
			this.Time = Time;
			this.Exception = Exception;
			this.ResponseSnapshot = ResponseSnapshot;
		}
	}

	public class SnapshotRequest
	{
		static public async Task<ToCustomBotSnapshot> RequestSnapshotAsync(
			string ApiUri,
			FromCustomBotSnapshot FromBotSnapshot = null)
		{
			string RequestData = null;

			if (null != FromBotSnapshot)
			{
				RequestData = ToCustomBotSnapshot.SerializeToString(FromBotSnapshot);
			}

			string ResponseData = null;

			using (var client = new WebClient())
			{
				client.Encoding = Encoding.UTF8;
				client.Headers[HttpRequestHeader.ContentType] = "application/json";

				ResponseData = await client.UploadStringTaskAsync(ApiUri, "POST", RequestData ?? "");
			}

			if (null == ResponseData)
			{
				return null;
			}

			return ToCustomBotSnapshot.DeserializeFromString<ToCustomBotSnapshot>(ResponseData);
		}

		static public async Task<SnapshotRequestResult> RequestSnapshotAsyncEncapsulated(
			string ApiUri,
			int DurationLimit,
			FromCustomBotSnapshot FromBotSnapshot = null)
		{
			var Time = Bib3.Glob.StopwatchZaitMiliSictInt();

			System.Exception Exception = null;
			ToCustomBotSnapshot ResponseSnapshot = null;

			try
			{
				if (null == ApiUri)
				{
					throw new ArgumentNullException("ApiUrl");
				}

				var ResponseSnapshotTask =
					RequestSnapshotAsync(ApiUri, FromBotSnapshot)
					.TimeoutAfter(DurationLimit);

				ResponseSnapshot = await ResponseSnapshotTask;
			}
			catch (System.Exception tException)
			{
				Exception = tException;
			}
			finally
			{
			}

			return new SnapshotRequestResult(
				Time,
				Exception,
				ResponseSnapshot);
		}

		//	readonly public int? ServerAddressTcp;
		readonly public string ApiUri;
		readonly public int DurationLimit;
		readonly public FromCustomBotSnapshot FromCustomBotSnapshot;

		//public Int64 Time
		//{
		//	private set;
		//	get;
		//}

		//public System.Exception Exception
		//{
		//	private set;
		//	get;
		//}

		public SnapshotRequestResult Result
		{
			private set;
			get;
		}

		public ToCustomBotSnapshot ResultResponseSnapshot
		{
			get
			{
				var Result = this.Result;

				if (null == Result)
				{
					return null;
				}

				return Result.ResponseSnapshot;
			}
		}

		public SnapshotRequest(
			string ApiUri,
			int DurationLimit,
			FromCustomBotSnapshot FromCustomBotSnapshot = null)
		{
			this.ApiUri = ApiUri;
			this.DurationLimit = DurationLimit;
			this.FromCustomBotSnapshot = FromCustomBotSnapshot;
		}

		public void Execute()
		{
			/*
			 * 2015.02.19
			 * 
			Time = Bib3.Glob.StopwatchZaitMiliSictInt();

			try
			{
				if (null == ApiUrl)
				{
					throw new ArgumentNullException("ApiUrl");
				}

				var ResponseSnapshotTask =
					RequestSnapshotAsync(ApiUrl)
					.TimeoutAfter(DurationLimit);

				var	ResponseSnapshot = ResponseSnapshotTask.Result;

				Thread.MemoryBarrier();

				this.ResponseSnapshot = ResponseSnapshot;
			}
			catch (System.Exception tException)
			{
				Exception = tException;
			}
			finally
			{
			}
			 * */

			var Result = RequestSnapshotAsyncEncapsulated(ApiUri, DurationLimit, FromCustomBotSnapshot).Result;

			Thread.MemoryBarrier();

			this.Result = Result;
		}

	}
}
