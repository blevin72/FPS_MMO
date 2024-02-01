using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;


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

    private string settingsURL = "http://localhost:8888/sqlconnect/settings.php?action=update"; // Replace with your actual registration URL.
                                                                                                // Pathway to the settings.php file

    private string getSettingsURL = "http://localhost:8888/sqlconnect/settings.php?action=get_settings"; // URL for retrieving current values in settings


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

    private IEnumerator GetPlayerSettings()
    {
        string getRequestURL = getSettingsURL + "&accountID=" + DB_Manager.accountID;

        UnityWebRequest www = UnityWebRequest.Get(getRequestURL);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string responseText = www.downloadHandler.text;

            Debug.Log("Raw JSON response: " + responseText);

            // Deserialize JSON to a dictionary
            Dictionary<string, string> settingsDictionary = JsonUtility.FromJson<Dictionary<string, string>>(responseText);

            // Create a new SettingsData instance and manually convert values
            SettingsData settingsData = new SettingsData();
            if (settingsDictionary.TryGetValue("master_volume", out string masterVolumeStr))
                settingsData.master_volume = int.Parse(masterVolumeStr);

            if (settingsDictionary.TryGetValue("music_volume", out string musicVolumeStr))
                settingsData.music_volume = int.Parse(musicVolumeStr);

            if (settingsDictionary.TryGetValue("sound_effects", out string soundEffectsStr))
                settingsData.sound_effects = int.Parse(soundEffectsStr);

            if (settingsDictionary.TryGetValue("dialog_voice", out string dialogVoiceStr))
                settingsData.dialog_voice = int.Parse(dialogVoiceStr);

            if (settingsDictionary.TryGetValue("proximity_chat", out string proximityChatStr))
                settingsData.proximity_chat = proximityChatStr;

            if (settingsDictionary.TryGetValue("subtitles", out string subtitlesStr))
                settingsData.subtitles = subtitlesStr;

            if (settingsDictionary.TryGetValue("ui_sound_fx", out string uiSoundFxStr))
                settingsData.ui_sound_fx = uiSoundFxStr;

            // Now you can use the settingsData instance as before
            masterVolume.value = settingsData.master_volume;
            musicVolume.value = settingsData.music_volume;
            soundEffects.value = settingsData.sound_effects;
            dialogVoice.value = settingsData.dialog_voice;
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