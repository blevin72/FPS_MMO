
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;


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


    private string settingsURL = "http://localhost:8888/sqlconnect/settings.php?action=update"; // Replace with your actual registration URL.
                                                                                                // Pathway to the settings.php file

    public void SaveSettingsButton()
    {
        Debug.Log("Saved all audio settings");
        StartCoroutine(SavePlayerSettings());
    }

    private IEnumerator SavePlayerSettings()
    {

        Debug.Log("SavePlayerSettings() was called");


        // Create a WWWForm to send data to the PHP script
        WWWForm form = new WWWForm();

        // Add player settings to the form
        // String values match columns name in database
        form.AddField("accountID", DB_Manager.accountID);
        form.AddField("master_volume", (int)masterVolume.value);
        form.AddField("music_volume", (int)musicVolume.value);
        form.AddField("sound_effects", (int)soundEffects.value);
        form.AddField("dialog_voice", (int)dialogVoice.value);
        form.AddField("proximity_chat", proximityChat.isOn ? "1" : "0"); // Convert boolean to 1 or 0
        form.AddField("subtitles", subtitles.isOn ? "1" : "0");
        form.AddField("ui_sound_fx", uiSoundFX.isOn ? "1" : "0");

        // Create a UnityWebRequest to send the form data to the PHP script
        UnityWebRequest www = UnityWebRequest.Post(settingsURL, form);
        www.SendWebRequest();

        while (!www.isDone)
        {
            yield return null;
        }

        if (www.result == UnityWebRequest.Result.Success)
        {
            // Check the response from the PHP script
            string response = www.downloadHandler.text;

            if (response.StartsWith("0"))
            {
                Debug.Log(response);
            }
            else
            {
                Debug.LogError("Update failed. Error: " + response);
            }
        }
        else
        {
            Debug.LogError("Network error: " + www.error);
        }
    }
}

