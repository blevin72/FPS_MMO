using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using Newtonsoft.Json;
using TMPro;
using System.Collections.Generic;

public class PreferencesSettings : MonoBehaviour
{
    public TMP_Dropdown theDivision;
    public TMP_Dropdown hudTheme;
    public TMP_Dropdown hudLocation;
    public TMP_Dropdown language;
    public TMP_Dropdown server;

    public Slider cameraSensitivity;
    public Slider cameraSway;
    public Slider hudTransparency;

    public Toggle gamertags;
    public Toggle goreViolence;
    public Toggle gameTips;

    private void Start()
    {
        StartCoroutine(GetPreferencesSettings());
    }

    public void SetPreferencesSettingsDefault()
    {
        GetDropdownOptions();
        theDivision.value = 0;
        hudTheme.value = 0;
        hudLocation.value = 0;
        language.value = 0;
        //server.value = 0; SERVER NOT DONE ON UI YET

        cameraSensitivity.value = 50;
        cameraSway.value = 50;
        hudTransparency.value = 50;

        gamertags.isOn = true;
        goreViolence.isOn = true;
        gameTips.isOn = true;
    }

    public void SaveSocialSettingsButton() //change this to SavePreferencesSettingsButton()
    {
        StartCoroutine(SavePreferencesSettings());
    }

    private string preferencesSettingsURL = "http://localhost:8888/sqlconnect/preferencesSettings.php?action=update";
    private string getPreferencesSettingsURL = "http://localhost:8888/sqlconnect/preferencesSettings.php?action=get_settings";

    private IEnumerator SavePreferencesSettings()
    {
        // Create a WWWForm to send data to the PHP script
        WWWForm form = new WWWForm();

        // Add player settings to the form
        form.AddField("accountID", DB_Manager.accountID);
        form.AddField("the_division", theDivision.options[theDivision.value].text);
        form.AddField("HUD_theme", hudTheme.options[hudTheme.value].text);
        form.AddField("HUD_location", hudLocation.options[hudLocation.value].text);
        form.AddField("languages", language.options[language.value].text);
        //form.AddField("server", server.options[server.value].text); 
        form.AddField("camera_sensitivity", cameraSensitivity.value.ToString()); //use this syntax for sliders
        form.AddField("camera_sway", cameraSway.value.ToString());
        form.AddField("HUD_transparency", hudTransparency.value.ToString());
        form.AddField("gamertags", gamertags.isOn ? "1" : "0"); //use this syntax for toggles
        form.AddField("gore", goreViolence.isOn ? "1" : "0");
        form.AddField("game_tips", gameTips.isOn ? "1" : "0");

        // Create a UnityWebRequest to send the form data to the PHP script
        UnityWebRequest www = UnityWebRequest.Post(preferencesSettingsURL, form);
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

    private IEnumerator GetPreferencesSettings()
    {
        string getRequestURL = getPreferencesSettingsURL + "&accountID=" + DB_Manager.accountID;
        UnityWebRequest www = UnityWebRequest.Get(getRequestURL);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string responseText = www.downloadHandler.text;

            // Deserialize JSON to SettingsData
            SettingsData settingsData = JsonConvert.DeserializeObject<SettingsData>(responseText);

            // Handle dropdown values
            SetDropdownValue(theDivision, settingsData.the_division);
            SetDropdownValue(hudTheme, settingsData.HUD_theme);
            SetDropdownValue(hudLocation, settingsData.HUD_location);
            SetDropdownValue(language, settingsData.languages);
            //SetDropdownValue(server, settingsData.screen); SERVER NOT SET UP YET IN UI

            // Now convert string values to appropriate types
            float parsedCameraSensitivity, parsedCameraSway, parsedHUDTransparency;

            if (float.TryParse(settingsData.camera_sensitivity, out parsedCameraSensitivity))
                cameraSensitivity.value = parsedCameraSensitivity;

            if (float.TryParse(settingsData.camera_sway, out parsedCameraSway))
                cameraSway.value = parsedCameraSway;

            if (float.TryParse(settingsData.HUD_transparency, out parsedHUDTransparency))
                hudTransparency.value = parsedHUDTransparency;

            gamertags.isOn = settingsData.gamertags == "1";
            goreViolence.isOn = settingsData.gore == "1";
            gameTips.isOn = settingsData.game_tips == "1";
        }
    }

    private void SetDropdownValue(TMP_Dropdown dropdown, string value)
    {
        int index = System.Array.FindIndex(dropdown.options.ToArray(), option => option.text == value);
        if (index != -1)
        {
            dropdown.value = index;
        }
        else
        {
            Debug.LogError($"{dropdown.name} value not found in dropdown options.");
        }
    }

    private void GetDropdownOptions()
    {
        theDivision.AddOptions(new List<TMP_Dropdown.OptionData>
        {
            new TMP_Dropdown.OptionData("First Steps"),
            new TMP_Dropdown.OptionData("Medium Rare"),
            new TMP_Dropdown.OptionData("Survivalist"),
            new TMP_Dropdown.OptionData("Apocalyptic"),
        });

        hudTheme.AddOptions(new List<TMP_Dropdown.OptionData>
        {
            new TMP_Dropdown.OptionData("Dark"),
            new TMP_Dropdown.OptionData("Light"),
            new TMP_Dropdown.OptionData("Custom"),
        });

        hudLocation.AddOptions(new List<TMP_Dropdown.OptionData>
        {
            new TMP_Dropdown.OptionData("Bottom"),
            new TMP_Dropdown.OptionData("Top"),
            new TMP_Dropdown.OptionData("Left"),
            new TMP_Dropdown.OptionData("Right"),
        });

        language.AddOptions(new List<TMP_Dropdown.OptionData>
        {
            new TMP_Dropdown.OptionData("English"),
            new TMP_Dropdown.OptionData("Spanish"),
            new TMP_Dropdown.OptionData("French"),
            new TMP_Dropdown.OptionData("German"),
            new TMP_Dropdown.OptionData("Italian"),
            new TMP_Dropdown.OptionData("Japanese"),
            new TMP_Dropdown.OptionData("Korean"),
            new TMP_Dropdown.OptionData("Chinese"),
            new TMP_Dropdown.OptionData("Russian"),
            new TMP_Dropdown.OptionData("Portuguese"),
        });

        //SERVER HAS NOT BEEN COMPLETED IN THE UI YET
        //server.AddOptions(new List<TMP_Dropdown.OptionData>
        //{
        //    new TMP_Dropdown.OptionData("Bottom"),
        //    new TMP_Dropdown.OptionData("Top"),
        //    new TMP_Dropdown.OptionData("Left"),
        //    new TMP_Dropdown.OptionData("Right"),
        //});
    }
}
