using LSPD_First_Response;
using Rage;
using LSPD_First_Response.Mod.API;
using System.Windows.Forms;

namespace JokinsUtilityPlugin {

    public class Main : Plugin {

        public override void Initialize() {

            Functions.OnOnDutyStateChanged += OnOnDutyStateChangedHandler;

            Game.LogTrivial("JokinsUtility " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() + " has been initialized.");
            Game.LogTrivial("Go on duty to fully load JokinsUtility.");
        }

        public override void Finally() {
            Game.LogTrivial("JokinsUtility has been cleaned up.");
        }

        private static void OnOnDutyStateChangedHandler(bool OnDuty) {
            if (OnDuty) {
                Game.DisplayNotification("Mein mod ist geladen, salak.\nU = Car Repair and Wash\nK = Panic Button");

                GameFiber.StartNew(PanicButton);
                GameFiber.StartNew(RepairAndWashCar);
            }
        }

        private static void PanicButton() {
            while (true) {
                GameFiber.Yield();

                if (ComputerCheck.IsKeyDown(Keys.K)) {
                    Functions.PlayScannerAudio("PANIC_BUTTON");
                    Game.DisplayNotification("Code 999: Officer in Not");

                    GameFiber.StartNew(PanicButton_Backup);
                    GameFiber.Sleep(5000);
                }
            }

            void PanicButton_Backup() {
                for (int i = 0; i < 2; i++) {
                    Functions.RequestBackup(Game.LocalPlayer.Character.Position, EBackupResponseType.Code3, EBackupUnitType.LocalUnit);
                    Functions.RequestBackup(Game.LocalPlayer.Character.Position, EBackupResponseType.Code3, EBackupUnitType.NooseTeam);
                    GameFiber.Sleep(1000);
                }
            }
        }


        private static void RepairAndWashCar() {
            while (true) {
                GameFiber.Yield();

                if (ComputerCheck.IsKeyDown(Keys.U)) {
                    if (Game.LocalPlayer.Character.IsInAnyVehicle(false)) {
                        Game.LocalPlayer.Character.CurrentVehicle.Repair();
                        Game.LocalPlayer.Character.CurrentVehicle.Wash();

                        Game.DisplayNotification("Auto repariert und gewaschen von Stripper");
                    } else {
                        Game.DisplayNotification("Wo Auto?");
                    }
                }
            }
        }
    }
}
