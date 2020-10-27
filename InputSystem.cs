using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum KeyButton {
    UP,DOWN,LEFT,RIGHT,SHOOT, DASH,DASH_TO_MOUSE,RUN,RAGE,PAUSE,NEXT_MUSIC,PREVIOUS_MUSIC,QUICK_WEAPON_SWAP,
    WEAPON_1,WEAPON_2, WEAPON_3, WEAPON_4, WEAPON_5
}
public class InputSystem {

    private static Dictionary<KeyButton, KeyCode> inputs;
    public static bool InputLock { get; set; }
    private static InputSystem _instance;
    public static InputSystem Instance {
        get {
            if(_instance == null) {
                _instance = new InputSystem();
            }
            return _instance;
        }
    }
    
    public InputSystem() {
        InputLock = false;
        inputs = new Dictionary<KeyButton, KeyCode>(11);

        inputs.Add(KeyButton.UP, KeyCode.W);
        inputs.Add(KeyButton.DOWN, KeyCode.S);
        inputs.Add(KeyButton.LEFT, KeyCode.A);
        inputs.Add(KeyButton.RIGHT, KeyCode.D);
        inputs.Add(KeyButton.SHOOT, KeyCode.Mouse0);
        inputs.Add(KeyButton.DASH, KeyCode.B);
        inputs.Add(KeyButton.DASH_TO_MOUSE, KeyCode.V);
        inputs.Add(KeyButton.RUN, KeyCode.LeftShift);
        inputs.Add(KeyButton.PAUSE, KeyCode.Escape);
        inputs.Add(KeyButton.NEXT_MUSIC, KeyCode.U);
        inputs.Add(KeyButton.PREVIOUS_MUSIC, KeyCode.Y);
        inputs.Add(KeyButton.QUICK_WEAPON_SWAP, KeyCode.Q);
        inputs.Add(KeyButton.WEAPON_1, KeyCode.Alpha1);
        inputs.Add(KeyButton.WEAPON_2, KeyCode.Alpha2);
        inputs.Add(KeyButton.WEAPON_3, KeyCode.Alpha3);
        inputs.Add(KeyButton.WEAPON_4, KeyCode.Alpha4);
        inputs.Add(KeyButton.WEAPON_5, KeyCode.Alpha5);
        inputs.Add(KeyButton.RAGE, KeyCode.Mouse2);
    }

    public bool GetButtonDown(KeyButton button) {
        if (InputLock) return false;
        return Input.GetKeyDown(inputs[button]);
    }
    public bool GetButtonUp(KeyButton button) {
        if (InputLock) return false;
        return Input.GetKeyUp(inputs[button]);
    }
    public bool GetButton(KeyButton button) {
        if (InputLock) return false;
        return Input.GetKey(inputs[button]);
    }
    public void SetButton(KeyButton button, KeyCode key) {
        inputs[button] = key;
    }

    
}
