using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using Newtonsoft.Json;

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
        StartCoroutine(GetPlayerSettings());
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

    private string settingsURL = "http://localhost:8888/sqlconnect/settings.php?action=update";
    private string getSettingsURL = "http://localhost:8888/sqlconnect/settings.php?action=get_settings";

    public void SaveSettingsButton()
    {
        Debug.Log("Saved all audio settings");
        StartCoroutine(SavePlayerSettings());
    }

    private IEnumerator SavePlayerSettings()
    {
        // Create a WWWForm to send data to the PHP script
        WWWForm form = new WWWForm();

        // Add player settings to the form
        form.AddField("accountID", DB_Manager.accountID);
        form.AddField("master_volume", masterVolume.value.ToString());
        form.AddField("music_volume", musicVolume.value.ToString());
        form.AddField("sound_effects", soundEffects.value.ToString());
        form.AddField("dialog_voice", dialogVoice.value.ToString());
        form.AddField("proximity_chat", proximityChat.isOn ? "1" : "0");
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

    private IEnumerator GetPlayerSettings()
    {
        string getRequestURL = getSettingsURL + "&accountID=" + DB_Manager.accountID;

        UnityWebRequest www = UnityWebRequest.Get(getRequestURL);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string responseText = www.downloadHandler.text;

            // Deserialize JSON to SettingsData
            SettingsData settingsData = JsonConvert.DeserializeObject<SettingsData>(responseText);

            // Now convert string values to appropriate types
            float parsedMasterVolume, parsedMusicVolume, parsedSoundEffects, parsedDialogVoice;

            if (float.TryParse(settingsData.master_volume, out parsedMasterVolume))
                masterVolume.value = parsedMasterVolume;

            if (float.TryParse(settingsData.music_volume, out parsedMusicVolume))
                musicVolume.value = parsedMusicVolume;

            if (float.TryParse(settingsData.sound_effects, out parsedSoundEffects))
                soundEffects.value = parsedSoundEffects;

            if (float.TryParse(settingsData.dialog_voice, out parsedDialogVoice))
                dialogVoice.value = parsedDialogVoice;

            proximityChat.isOn = settingsData.proximity_chat == "1";
            subtitles.isOn = settingsData.subtitles == "1";
            uiSoundFX.isOn = settingsData.ui_sound_fx == "1";

            Debug.Log("Settings retrieved successfully");
        }
        else
        {
            Debug.LogError("Error retrieving settings: " + www.error);
        }
    }
}
