using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bib3;
using BotEngine;
using Optimat.EveOnline;
using Optimat.EveOnline.CustomBot;
using BotEngine.Common;
using BotEngine.EveOnline.Interface;
using BotEngine.EveOnline.Interface.MemoryStruct;
using BotEngine.Interface;
using ExtractFromOldAssembly.Bib3;

namespace Optimat.EveO.Nuzer
{
	public class SensorAppDomainSetup
	{
		static SensorAppDomainSetup()
		{
			BotEngine.Interface.InterfaceAppDomainSetup.Setup();

			/*
			2015.08.29

			Ersaz durc BotEngine.Interface.InterfaceAppDomainSetup.Setup.

            Setup();
        }

        static bool SetupComplete = false;

        static public void Setup()
        {
            if(SetupComplete)
            {
                return;
            }

            SetupComplete = true;

            InternSetup();
        }

        static void InternSetup()
        {
            SetupProtobuf();
        }

        static public void SetupProtobuf()
        {
			/*
			2015.08.29
			Ersaz durc BotEngine.Interface.Protobuf.ProtobufSetup.

            BotEngine.Interface.Protobuf.ProtobufSetup();

            var MengeType = new Type[]
            {
                typeof(Bib3.RefNezDiferenz.SictZuNezSictDiferenzScritAbbild),
                typeof(BotEngine.Common.Extension),
                typeof(BotEngine.Interface.InterfaceProxyMessage),
                //  tmp.FullName = "Optimat.EveOnline.VonProcessMesung`1[[Optimat.EveOnline.VonSensorikMesung, Optimat.EveOnline.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]"
                //  typeof()
            };

            MengeType.Select(Type => Type.Assembly)
                .ForEachNullable(Assembly => BotEngine.ProtobufRuntimeTypeModelSetup.SetupForAssembly(Assembly));
			*

			BotEngine.Interface.Protobuf.ProtobufSetup();
        }
		*/
		}
	}

	/*
	2015.05.00
	Kan nur mit noiere Versioon von BotEngine verwandt werde.

public class SensorAppDomainSetup
{
	readonly BotEngine.Sensor.SensorAppDomainSetup BotEngineSetup = new BotEngine.Sensor.SensorAppDomainSetup();
}
*/



	public class SensorClient
	{
		readonly List<FromSensorToConsumerMessage> TempDebugListeMessage = new List<FromSensorToConsumerMessage>();

		readonly BotEngine.Interface.InterfaceAppManager SensorAppManager = new BotEngine.Interface.InterfaceAppManager(typeof(SensorAppDomainSetup));

		FromSensorToConsumerMessage SensorMessageLast
		{
			set;
			get;
		}

		public FromProcessMeasurement<MemoryMeasurement> MemoryMeasurementLast
		{
			private set;
			get;
		}

		public FromProcessMeasurement<WindowMeasurement> WindowMeasurementLast
		{
			private set;
			get;
		}

		/*
		public void FromConsumer(Optimat.EveOnline.CustomBot.FromCustomBotSnapshot Snapshot)
		{
			SensorAppManager.FromConsumer(new BotEngine.Sensor.FromConsumerToSensorProxyMessage(
				Snapshot.SerializeSingleBib3RefNezDifProtobuf()));
		}
        */

		/*
		* 16.04.15
		* 
		public void FromServer(BotEngine.Interface.FromServerToInterfaceAppManagerMessage Message)
		{
			SensorAppManager.FromServer(Message);
		}

		public BotEngine.Interface.FromInterfaceAppManagerToServerMessage ToServer()
		{
			return SensorAppManager.ToServer();
		}
		*/

		public FromSensorToConsumerMessage SensorExchange(
			FromConsumerToSensorMessage ToSensorMessage)
		{
			FromSensorToConsumerMessage SensorMessageLast = this.SensorMessageLast;

			try
			{
				BotEngine.Interface.InterfaceAppDomainSetup.Setup();

				var FromSensorAppMessage = SensorAppManager?.ConsumerExchange(
					new BotEngine.Interface.FromConsumerToInterfaceProxyMessage(
						ToSensorMessage.SerializeSingleBib3RefNezDifProtobuf()));

				if (null == FromSensorAppMessage)
				{
					return null;
				}

				var AppSpecific = FromSensorAppMessage.AppSpecific;

				//  if (null != AppSpecific)
				{
					/*
                    SensorMessageLast =
                        Optimat.EveOnline.CustomBot.ToCustomBotSnapshot.DeserializeFromString<Optimat.EveOnline.CustomBot.ToCustomBotSnapshot>(AppSpecific);
                        */

					/*
                    SensorMessageLast =
                        Convert.FromBase64String(AppSpecific).ProtobufDeserialize<Optimat.EveOnline.CustomBot.ToCustomBotSnapshot>();
                        */

					/*
                    SensorMessageLast =
                        Convert.FromBase64String(AppSpecific).UniversalSerializerDeSerialize<Optimat.EveOnline.CustomBot.ToCustomBotSnapshot>();
                        */

					SensorMessageLast =
						AppSpecific.DeSerializeProtobufBib3RefNezDif().FirstOrDefaultNullable() as FromSensorToConsumerMessage;

					TempDebugListeMessage.Add(SensorMessageLast);
					TempDebugListeMessage.ListeKürzeBegin(44);

					return SensorMessageLast;
				}
			}
			finally
			{
				MemoryMeasurementLast = SensorMessageLast?.MemoryMeasurement ?? MemoryMeasurementLast;
				WindowMeasurementLast = SensorMessageLast?.WindowMeasurement ?? WindowMeasurementLast;

				this.SensorMessageLast = SensorMessageLast;
			}
		}
	}
}
