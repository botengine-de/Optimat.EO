using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Optimat.EveOnline.Base;
using Optimat.EveOnline.CustomBot;

namespace Optimat.EveO.Nuzer
{
	public class CustomBotServer : IDisposable
	{
		readonly public Int64 BeginZait;

		readonly public int AdreseTcp;
		readonly public string ApiUri;

		public WebServer WebServer
		{
			private set;
			get;
		}

		public SictWertMitZait<System.Exception> ListeExceptionLezte
		{
			private set;
			get;
		}

		public Int64? ListeRequestLezteZait
		{
			private set;
			get;
		}

		public int RequestAnzaal
		{
			private set;
			get;
		}

		public bool IsListening
		{
			get
			{
				var WebServer = this.WebServer;

				if (null == WebServer)
				{
					return false;
				}

				return WebServer.IsListening;
			}
		}

		ToCustomBotSnapshot InternNaacBotScnapscus;

		public ToCustomBotSnapshot NaacBotScnapscus
		{
			set
			{
				InternNaacBotScnapscus = value;

				Thread.MemoryBarrier();
			}

			get
			{
				return InternNaacBotScnapscus;
			}
		}

		public FromCustomBotSnapshot VonBotScnapscusLezte
		{
			private set;
			get;
		}

		public Int64? RequestedMeasurementTimeLezte
		{
			private set;
			get;
		}

		T KapseleInCatch<T>(Func<T> Funk)
		{
			try
			{
				if (null == Funk)
				{
					return default(T);
				}

				return Funk();
			}
			catch (System.Exception Exception)
			{
				this.ListeExceptionLezte = new SictWertMitZait<System.Exception>(Bib3.Glob.StopwatchZaitMiliSictInt(), Exception);
				return default(T);
			}
		}

		void KapseleInCatch(Action Action)
		{
			KapseleInCatch(() =>
				{
					Action();
					return 0;
				});
		}

		public string Request(System.Net.HttpListenerRequest Request)
		{
			return
				KapseleInCatch(new Func<string>(() =>
				{
					if (null == Request)
					{
						return null;
					}

					++RequestAnzaal;

					ListeRequestLezteZait = Bib3.Glob.StopwatchZaitMiliSictInt();

					var Url = Request.RawUrl;

					FromCustomBotSnapshot VonBotScnapscus = null;

					try
					{
						if (Request.HasEntityBody)
						{
							var RequestBodyStream = Request.InputStream;

							if (null != RequestBodyStream)
							{
								var RequestBodyStreamReader = new StreamReader(RequestBodyStream);

								var RequestBody = RequestBodyStreamReader.ReadToEnd();

								VonBotScnapscus = ToCustomBotSnapshot.DeserializeFromString<FromCustomBotSnapshot>(RequestBody);
							}
						}
					}
					finally
					{
						Thread.MemoryBarrier();

						this.VonBotScnapscusLezte = VonBotScnapscus;

						if (null != VonBotScnapscus)
						{
							this.RequestedMeasurementTimeLezte = Bib3.Glob.Max(RequestedMeasurementTimeLezte, VonBotScnapscus.MeasurementMemoryRequestTime);
						}
					}

					var NaacBotScnapscus = this.NaacBotScnapscus;

					Thread.MemoryBarrier();

					if (null == NaacBotScnapscus)
					{
						return "";
					}

					return ToCustomBotSnapshot.SerializeToString(NaacBotScnapscus);
				}));
		}

		static public string PräfixKonstrukt(int? AdreseTcp)
		{
			var ClientApiUrl = TempApi.ApiUrlConstructTempLocalhost(AdreseTcp);

			if (null == ClientApiUrl)
			{
				return null;
			}

			return System.Text.RegularExpressions.Regex.Match(ClientApiUrl, @".*api\/").Value;
		}

		public CustomBotServer(
			int AdreseTcp)
		{
			BeginZait = Bib3.Glob.StopwatchZaitMiliSictInt();

			this.AdreseTcp = AdreseTcp;

			ApiUri = PräfixKonstrukt(AdreseTcp);

			KapseleInCatch(() =>
				{
					//	var Prefix = @"http://localhost:" + AdreseTcp.ToString() + @"/api/";

					this.WebServer = new WebServer(Request, ApiUri);

					WebServer.Run();
				});
		}

		public void Stop()
		{
			var WebServer = this.WebServer;

			if (null != WebServer)
			{
				WebServer.Stop();
			}
		}

		public void Dispose()
		{
			Stop();
		}
	}
}
