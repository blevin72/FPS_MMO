using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioSettings : MonoBehaviour
{
    public Slider masterVolume;
    public Slider musicVolume;
    public Slider soundEffects;
    public Slider dialogVoice;
    public Toggle proximityChat;
    public Toggle subtitles;
    public Toggle uiSoundFX;
    public Button saveSettings;
    public Button resetDefault;

    void Start()
    {
        SetAudioSettingsDefault();
    }

    public void SetAudioSettingsDefault()
    {
        masterVolume.value = 50;
        musicVolume.value = 50;
        soundEffects.value = 50;
        dialogVoice.value = 50;
        proximityChat.isOn = true;
        subtitles.isOn = true;
        uiSoundFX.isOn = true;
    }
}