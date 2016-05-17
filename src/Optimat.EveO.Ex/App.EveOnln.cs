using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bib3.Windows;
using Optimat.EveOnline;
using Bib3;

namespace Optimat.EveO.Nuzer
{
    public partial class App
    {
        public const string BotEngineApiUriDefault = @"http://classic.eveonline.api.botengine.de:4030/api";

        static public SictKonfiguratioon KonfigSctandardWertBerecne()
        {
            var Konfig = new SictKonfiguratioon();

            var AuswaalZiilProcess = new SictAuswaalWindowsProcessPräferenz();

            AuswaalZiilProcess.FilterMainModuleDataiNaame = "ExeFile.exe";
            AuswaalZiilProcess.FilterMainModuleDataiPfaad = Path.DirectorySeparatorChar.ToString() + AuswaalZiilProcess.FilterMainModuleDataiNaame;

            AuswaalZiilProcess.ÜberneemeKandidaatMitMaxBewertungAutoFraigaabe = true;

            Konfig.AuswaalZiilProcess = AuswaalZiilProcess;

            Konfig.SensorServerApiUri = BotEngineApiUriDefault;

            Konfig.ZiilProcessWirkungPauseMengeKeyKombi = new System.Windows.Input.Key[][] { ZiilProcessWirkungPauseKeyKombiSctandard };

            return Konfig;
        }

        static public SictOptimatParamMission EveOnlinePräferenzSctandardWertBerecneTailMission()
        {
            var Präferenz = new SictOptimatParamMission();

            Präferenz.AktioonAcceptFraigaabe = true;
            Präferenz.AktioonDeclineFraigaabe = false;

            Präferenz.AktioonAcceptMengeAgentLevelFraigaabe = Enumerable.Range(1, 4).ToArray();

            return Präferenz;
        }

        static public SictOptimatParam EveOnlinePräferenzSctandardWertBerecne()
        {
            //	Präferenz für Shield Tank

            var GefectFortsazScranke = new SictVerzwaigungNaacShipZuusctandScranke();
            var GefectBaitritScranke = new SictVerzwaigungNaacShipZuusctandScranke();

            GefectFortsazScranke.ShieldScrankeBetraagMili = 400;
            GefectFortsazScranke.ArmorScrankeBetraagMili = 900;
            GefectFortsazScranke.StructScrankeBetraagMili = 980;
            GefectFortsazScranke.CapacitorScrankeBetraagMili = 330;

            GefectBaitritScranke.ShieldScrankeBetraagMili = 800;
            GefectBaitritScranke.ArmorScrankeBetraagMili = 950;
            GefectBaitritScranke.StructScrankeBetraagMili = 990;
            GefectBaitritScranke.CapacitorScrankeBetraagMili = 700;

            var InRaumVerhalteBaasis = new SictParamInRaumAktioonVerzwaigungNaacShipZuusctandScranke();

            InRaumVerhalteBaasis.GefectFortsazScranke = GefectFortsazScranke;
            InRaumVerhalteBaasis.GefectBaitritScranke = GefectBaitritScranke;

            InRaumVerhalteBaasis.ModuleRegenAinScrankeMili = 800;
            InRaumVerhalteBaasis.ModuleRegenAusScrankeMili = 950;

            var Präferenz = new SictOptimatParam();

            Präferenz.InRaumVerhalteBaasis = InRaumVerhalteBaasis;

            Präferenz.AutoPilotFraigaabe = true;

            Präferenz.Mission = EveOnlinePräferenzSctandardWertBerecneTailMission();

            return Präferenz;
        }
    }
}
