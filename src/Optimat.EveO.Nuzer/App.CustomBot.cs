using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Optimat.EveOnline;
//using Optimat.EveOnline.CustomBot;

namespace Optimat.EveO.Nuzer
{
	public partial class App
	{
		void ScnapscusPropagiireNaacCustomBot()
		{
			if (!AingaabeErwaitertFraigaabe)
			{
				return;
			}

			var ControlErwaitert = GbsSctoierelementHaupt.ControlErwaitert;

			if (null == ControlErwaitert)
			{
				return;
			}

			var CustomBotServer = ControlErwaitert.CustomBotServer;

			if (null == CustomBotServer)
			{
				return;
			}

			/*
			 * 2015.03.04
			 * 
		VonSensorikMesung VonSensorikMesung = null;

		Int64 VonSensorikMesungBeginZait = 0;
		Int64 VonSensorikMesungEndeZait = 0;

		{
			var NaacAnwendungZuMeldeGbsBaumWurzel = this.NaacAnwendungZuMeldeGbsBaumWurzel;

			if (null != NaacAnwendungZuMeldeGbsBaumWurzel)
			{
				var SensorikScnapscusMitAndereIdent =
					Optimat.EveOnline.AuswertGbs.Extension.SensorikScnapscusKonstrukt(NaacAnwendungZuMeldeGbsBaumWurzel.Wert);

				var SensorikScnapscus = ObjKopiiMitScnitCustErsazId(SensorikScnapscusMitAndereIdent);

				VonSensorikMesung = SensorikScnapscus;
				VonSensorikMesungBeginZait = NaacAnwendungZuMeldeGbsBaumWurzel.BeginZait ?? 0;
				VonSensorikMesungEndeZait = NaacAnwendungZuMeldeGbsBaumWurzel.EndeZait ?? 0;
			}

		}

		var Scnapscus = new ToCustomBotSnapshot(
			Bib3.Glob.StopwatchZaitMiliSictInt(),
			GbsAingaabeZiilProcessId,
			new WertZuZaitraumStruct<VonSensorikMesung>(VonSensorikMesung, VonSensorikMesungBeginZait, VonSensorikMesungEndeZait));
			 * */

			/*
			2015.08.29

			ToCustomBotSnapshot Scnapscus = null;

			{
				var SensorClient = this.SensorClient;

				if (null != SensorClient)
				{
                    Scnapscus = SensorSnapshotLastAgr.OoneMesungWindow();
				}
			}

			CustomBotServer.NaacBotScnapscus = Scnapscus;
			*/
		}
	}
}
