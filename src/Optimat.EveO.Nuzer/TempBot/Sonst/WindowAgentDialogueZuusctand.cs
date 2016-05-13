using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
//using Optimat.EveOnline.AuswertGbs;


namespace Optimat.EveOnline.Anwendung
{
	public struct SictWindowAgentDialogueSictFürEntscaidungHinraicendGlaicwertigFürÜbernaameMissionInfo
	{
		readonly	public string AgentName;
		readonly	public bool IstOffer;
		readonly	public bool IstAccepted;

		readonly public string MissionTitel;

		//	readonly	public SictAusGbsWindowAgentMissionObjectiveObjective MissionObjective;

		readonly public string MissionObjectiveSictString;

		/*
		 * 2015.02.07
		 * 
		readonly public string ZusamefasungMissionInfoHtmlStr;
		 * */

		public SictWindowAgentDialogueSictFürEntscaidungHinraicendGlaicwertigFürÜbernaameMissionInfo(
			VonSensor.WindowAgentDialogue WindowAgentDialogue)
		{
			AgentName = null;

			IstOffer = false;
			IstAccepted = false;

			MissionTitel = null;
			MissionObjectiveSictString = null;

			/*
			 * 2015.02.07
			 * 
			ZusamefasungMissionInfoHtmlStr = null;
			 * */

			if (null == WindowAgentDialogue)
			{
				return;
			}

			AgentName = WindowAgentDialogue.AgentName;

			IstOffer = true == WindowAgentDialogue.IstOffer;
			IstAccepted = true == WindowAgentDialogue.IstAccepted;

			var ZusamefasungMissionInfo = WindowAgentDialogue.ZusamefasungMissionInfo;

			if (null != ZusamefasungMissionInfo)
			{
			/*
			 * 2015.02.07
			 * 
				ZusamefasungMissionInfoHtmlStr = ZusamefasungMissionInfo.Htmlstr;
			 * */

				MissionTitel = ZusamefasungMissionInfo.MissionTitel;

				if (null != ZusamefasungMissionInfo.Objective)
				{
					MissionObjectiveSictString = JsonConvert.SerializeObject(ZusamefasungMissionInfo.Objective);
				}
			}
		}
	}

	/// <summary>
	/// Agregiirt Info zu SictAusGbsWindowAgentDialogue aus meerere Scnapscus
	/// </summary>
	[JsonObject(MemberSerialization.OptIn)]
	public class SictWindowAgentDialogueZuusctand : SictZuGbsObjektZuusctandMitIdentPerInt<VonSensor.WindowAgentDialogue>
	{
		/// <summary>
		/// In WindowAgentDialogue gezaigte Mission ändert sic oftmals kurz naac Öfnen aines Window.
		/// (z.B. werd für komplete Objective naac öfnen des Window ersct der zuvor für diise Agent angezaigte Wert und wexelt dan oone waitere Aingaabe auf andere Wert.)
		/// Diise Member zaigt den Zuusctand aus deem lezten Scnapscus fals auc der vorlezte hinraicend äänlic war.
		/// </summary>
		[JsonProperty]
		public SictWertMitZait<VonSensor.WindowAgentDialogue>? WindowAgentDialogueZuZaitFürÜbernaameMissionInfo;

		public SictWindowAgentDialogueZuusctand()
			:
			base(3)
		{
		}

		public SictWindowAgentDialogueZuusctand(
			Int64	ZaitMili,
			VonSensor.WindowAgentDialogue	Scnapscus)
			:
			base(
			3,
			ZaitMili,
			Scnapscus)
		{
		}

		static public bool HinraicendGlaicwertigFürÜbernaameMissionInfo(
			VonSensor.WindowAgentDialogue O0,
			VonSensor.WindowAgentDialogue O1)
		{
			if (O0 == O1)
			{
				return true;
			}

			if (null == O0 || null == O1)
			{
				return false;
			}

			var O0Abbild = new SictWindowAgentDialogueSictFürEntscaidungHinraicendGlaicwertigFürÜbernaameMissionInfo(O0);
			var O1Abbild = new SictWindowAgentDialogueSictFürEntscaidungHinraicendGlaicwertigFürÜbernaameMissionInfo(O1);

			return object.Equals(O0Abbild, O1Abbild);
		}

		override protected void NaacAingangScnapscus(
			Int64	ScnapscusZait,
			VonSensor.WindowAgentDialogue	ScnapscusWert)
		{
			SictWertMitZait<VonSensor.WindowAgentDialogue>? WindowAgentDialogueZuZaitFürÜbernaameMissionInfo = null;

			try
			{
				{
					/*
					 * 2014.05.13
					 * 
					var ScnapscusLezte = ListeScnapscusZuZait.LastOrDefault();
					var ScnapscusVorLezte = ListeScnapscusZuZait.ElementAtOrDefault(ListeScnapscusZuZait.Count() - 2);
					 * */

					var ScnapscusLezteNulbar = AingangScnapscusTailObjektIdentMitZaitLezteBerecne();
					var ScnapscusVorLezteNulbar = AingangScnapscusTailObjektIdentMitZaitVorLezteBerecne();

					if (ScnapscusLezteNulbar.HasValue && ScnapscusVorLezteNulbar.HasValue)
					{
						var ScnapscusLezte = ScnapscusLezteNulbar.Value;
						var ScnapscusVorLezte = ScnapscusVorLezteNulbar.Value;

						var ScnapscusLezteWindow = ScnapscusLezte.Wert;
						var ScnapscusVorLezteWindow = ScnapscusVorLezte.Wert;

						if (null != ScnapscusLezteWindow && null != ScnapscusVorLezteWindow)
						{
							if (HinraicendGlaicwertigFürÜbernaameMissionInfo(ScnapscusLezteWindow, ScnapscusVorLezteWindow))
							{
								WindowAgentDialogueZuZaitFürÜbernaameMissionInfo = ScnapscusLezteNulbar;
							}
						}
					}
				}
			}
			finally
			{
				this.WindowAgentDialogueZuZaitFürÜbernaameMissionInfo = WindowAgentDialogueZuZaitFürÜbernaameMissionInfo;
			}
		}
	}
}
