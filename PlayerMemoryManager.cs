using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMemoryManager {
    private static void SetFloatPref(string name, float value) {
        PlayerPrefs.SetFloat(name, value);
        PlayerPrefs.Save();
    }
    public static void SetSoundVolumePref(float value) {
        SetFloatPref("SoundVolume", value);
    }
    public static float GetSoundVolumePref() {
        return PlayerPrefs.GetFloat("SoundVolume",1f);
    }

    public static void SetMusicVolumePref(float value) {
        SetFloatPref("MusicVolume", value);
    }
    public static float GetMusicVolumePref() {
        return PlayerPrefs.GetFloat("MusicVolume",1f);
    }

    public static void SetSpeechVolumePref(float value) {
        SetFloatPref("SpeechVolume", value);
    }
    public static float GetSpeechVolumePref() {
        return PlayerPrefs.GetFloat("SpeechVolume",1f);
    }
}
