using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

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

    private void Start()
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

    private string settingsURL = "http://localhost:8888/sqlconnect/settings.php"; // Replace with your actual registration URL.
                                                                                  // Pathway to the settings.php file



    public void SaveSettingsButton()
    {
        masterVolume.value = masterVolume.value;
        musicVolume.value = musicVolume.value;
        soundEffects.value = soundEffects.value;
        dialogVoice.value = dialogVoice.value;
        proximityChat.isOn = proximityChat.isOn;
        subtitles.isOn = subtitles.isOn;
        uiSoundFX.isOn = subtitles.isOn;

        Debug.Log("Saved all audio settings");

        StartCoroutine(SavePlayerSettings());
    }

    private IEnumerator SavePlayerSettings()
    {
        Debug.Log("SavePlayerSettings() was called");

        // Create a WWWForm to send data to the PHP script
        WWWForm form = new WWWForm();

        // Add player settings to the form
        form.AddField("accountID", DB_Manager.accountID);
        form.AddField("masterVolume", (int)masterVolume.value);
        form.AddField("musicVolume", (int)musicVolume.value);
        form.AddField("soundEffects", (int)soundEffects.value);
        form.AddField("dialogVoice", (int)dialogVoice.value);
        form.AddField("proximityChat", proximityChat.isOn ? "1" : "0"); // Convert boolean to 1 or 0
        form.AddField("subtitles", subtitles.isOn ? "1" : "0");
        form.AddField("uiSoundFX", uiSoundFX.isOn ? "1" : "0");

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
                Debug.Log("Update successful");
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
