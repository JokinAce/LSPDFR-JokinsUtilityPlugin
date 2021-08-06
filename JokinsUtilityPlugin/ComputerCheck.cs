using Rage;
using System.Windows.Forms;

public static class ComputerCheck {
    //Call this instead of Game.IsKeyDown
    public static bool IsKeyDown(Keys KeyPressed) {
        if (Rage.Native.NativeFunction.CallByName<int>("UPDATE_ONSCREEN_KEYBOARD") != 0) {
            return Game.IsKeyDown(KeyPressed);
        } else {
            return false;
        }
    }

    //Call this instead of Game.IsKeyDownRightNow
    public static bool IsKeyDownRightNow(Keys KeyPressed) {
        if (Rage.Native.NativeFunction.CallByName<int>("UPDATE_ONSCREEN_KEYBOARD") != 0) {
            return Game.IsKeyDownRightNow(KeyPressed);
        } else {
            return false;
        }
    }
}