using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using Newtonsoft.Json;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class VisualSettings : MonoBehaviour
{
    public TMP_Dropdown resolution;
    public TMP_Dropdown graphics;
    public TMP_Dropdown textures;
    public TMP_Dropdown shaders;
    public TMP_Dropdown screen;
    public TMP_Dropdown aspectRatios;

    public Slider contrast;
    public Slider brightness;

    public Toggle shadows;
    public Toggle antiAliasing;
    public Toggle colorBlind;

    public Button saveSettings;
    public Button resetDefault;

    private void Start()
    {
        StartCoroutine(GetVisualSettings());
    }

    public void SetVisualSettingsDefault()
    {
        GetDropdownOptions();

        resolution.value = 0;
        graphics.value = 1;
        textures.value = 1;
        shaders.value = 1;
        screen.value = 0;
        aspectRatios.value = 0;

        contrast.value = 50;
        brightness.value = 50;

        shadows.isOn = true;
        antiAliasing.isOn = true;
        colorBlind.isOn = false;
    }

    public void SaveSettingsButton()
    {
        Debug.Log("Saved all audio settings");
        StartCoroutine(SaveVisualSettings());
    }

    private string visualSettingsURL = "http://localhost:8888/sqlconnect/visualSettings.php?action=update";
    private string getVisualSettingsURL = "http://localhost:8888/sqlconnect/visualSettings.php?action=get_settings";

    private IEnumerator SaveVisualSettings()
    {
        // Create a WWWForm to send data to the PHP script
        WWWForm form = new WWWForm();

        // Add player settings to the form
        form.AddField("accountID", DB_Manager.accountID);
        form.AddField("resolution", resolution.options[resolution.value].text); //use this syntax for dropdown values
        form.AddField("graphics", graphics.options[graphics.value].text);
        form.AddField("textures", textures.options[textures.value].text);
        form.AddField("shaders", shaders.options[shaders.value].text);
        form.AddField("screen_size", screen.options[screen.value].text);
        form.AddField("aspect_ratios", aspectRatios.options[aspectRatios.value].text);
        form.AddField("contrast", contrast.value.ToString());  //use this syntax for sliders
        form.AddField("brightness", brightness.value.ToString());
        form.AddField("shadows", shadows.isOn ? "1" : "0"); //use this syntax for toggles
        form.AddField("anti_aliasing", antiAliasing.isOn ? "1" : "0");
        form.AddField("color_blind", colorBlind.isOn ? "1" : "0");

        // Create a UnityWebRequest to send the form data to the PHP script
        UnityWebRequest www = UnityWebRequest.Post(visualSettingsURL, form);
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

private IEnumerator GetVisualSettings()
    {
        string getRequestURL = getVisualSettingsURL + "&accountID=" + DB_Manager.accountID;

        UnityWebRequest www = UnityWebRequest.Get(getRequestURL);
        yield return www.SendWebRequest();

        if(www.result == UnityWebRequest.Result.Success)
        {
            string responseText = www.downloadHandler.text;

            // Deserialize JSON to SettingsData
            SettingsData settingsData = JsonConvert.DeserializeObject<SettingsData>(responseText);

            // Handle dropdown values
            SetDropdownValue(resolution, settingsData.resolution);
            SetDropdownValue(graphics, settingsData.graphics);
            SetDropdownValue(textures, settingsData.textures);
            SetDropdownValue(shaders, settingsData.shaders);
            SetDropdownValue(screen, settingsData.screen);
            SetDropdownValue(aspectRatios, settingsData.aspect_ratios);

            // Now convert string values to appropriate types
            float parsedContrast, parsedBrightness;

            if (float.TryParse(settingsData.contrast, out parsedContrast))
                contrast.value = parsedContrast;

            if (float.TryParse(settingsData.brightness, out parsedBrightness))
                brightness.value = parsedBrightness;

            shadows.isOn = settingsData.shadows == "1";
            antiAliasing.isOn = settingsData.anti_aliasing == "1";
            colorBlind.isOn = settingsData.color_blind == "1";
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
        resolution.AddOptions(new List<TMP_Dropdown.OptionData>
        {
            new TMP_Dropdown.OptionData("1600x900"),
            new TMP_Dropdown.OptionData("1920x1080"),
            new TMP_Dropdown.OptionData("2560x1140"),
        });

        graphics.AddOptions(new List<TMP_Dropdown.OptionData>
        {
            new TMP_Dropdown.OptionData("Low"),
            new TMP_Dropdown.OptionData("Medium"),
            new TMP_Dropdown.OptionData("High"),
        });

        textures.AddOptions(new List<TMP_Dropdown.OptionData>
        {
            new TMP_Dropdown.OptionData("Low"),
            new TMP_Dropdown.OptionData("Medium"),
            new TMP_Dropdown.OptionData("High"),
        });

        shaders.AddOptions(new List<TMP_Dropdown.OptionData>
        {
            new TMP_Dropdown.OptionData("Low"),
            new TMP_Dropdown.OptionData("Medium"),
            new TMP_Dropdown.OptionData("High"),
        });

        screen.AddOptions(new List<TMP_Dropdown.OptionData>
        {
            new TMP_Dropdown.OptionData("Full Screen"),
            new TMP_Dropdown.OptionData("Windowed"),
        });

        aspectRatios.AddOptions(new List<TMP_Dropdown.OptionData>
        {
            new TMP_Dropdown.OptionData("16:9"),
            new TMP_Dropdown.OptionData("16:10"),
            new TMP_Dropdown.OptionData("21:9"),
        });
    }
}
