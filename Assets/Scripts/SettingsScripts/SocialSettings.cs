using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using Newtonsoft.Json;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class SocialSettings : MonoBehaviour
{
    public TMP_Dropdown messaging;

    public Toggle chat_profanity;
    public Toggle party_chat;
    public Toggle notifications;
    public Toggle online_status;

    private void Start()
    {
        StartCoroutine(GetSocialSettings());
    }

    public void SetSocialSettingsDefault()
    {
        GetDropdownOptions();
        messaging.value = 0;

        chat_profanity.isOn = true;
        party_chat.isOn = true;
        notifications.isOn = true;
        online_status.isOn = true;
    }

    public void SaveSocialSettingsButton()
    {
        StartCoroutine(SaveSocialSettings());
    }

    private string socialSettingsURL = "http://localhost:8888/sqlconnect/socialSettings.php?action=update";
    private string getSocialSettingsURL = "http://localhost:8888/sqlconnect/socialSettings.php?action=get_settings";

    private IEnumerator SaveSocialSettings()
    {
        // Create a WWWForm to send data to the PHP script
        WWWForm form = new WWWForm();

        // Add player settings to the form
        form.AddField("accountID", DB_Manager.accountID);
        form.AddField("messaging", messaging.options[messaging.value].text); //use this syntax for dropdown values
        form.AddField("chat_profanity", chat_profanity.isOn ? "1" : "0"); //use this syntax for toggles
        form.AddField("party_chat", party_chat.isOn ? "1" : "0");
        form.AddField("notifications", notifications.isOn ? "1" : "0");
        form.AddField("online_status", online_status.isOn ? "1" : "0");

        // Create a UnityWebRequest to send the form data to the PHP script
        UnityWebRequest www = UnityWebRequest.Post(socialSettingsURL, form);
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

    private IEnumerator GetSocialSettings()
    {
        string getRequestURL = getSocialSettingsURL + "&accountID=" + DB_Manager.accountID;
        UnityWebRequest www = UnityWebRequest.Get(getRequestURL);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string responseText = www.downloadHandler.text;

            // Deserialize JSON to SettingsData
            SettingsData settingsData = JsonConvert.DeserializeObject<SettingsData>(responseText);

            // Handle dropdown values
            SetDropdownValue(messaging, settingsData.messaging);

            chat_profanity.isOn = settingsData.chat_profanity == "1";
            party_chat.isOn = settingsData.party_chat == "1";
            notifications.isOn = settingsData.notifications == "1";
            online_status.isOn = settingsData.online_status == "1";
        }
    }

    private void SetDropdownValue(TMP_Dropdown dropdown, string value)
    {
        //Debug.Log($"{dropdown.name} dropdown options: {string.Join(", ", dropdown.options.Select(option => option.text))}");

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
        messaging.AddOptions(new List<TMP_Dropdown.OptionData>
        {
            new TMP_Dropdown.OptionData("Everyone"),
            new TMP_Dropdown.OptionData("Friends & OP"),
            new TMP_Dropdown.OptionData("Friends Only"),
        });
    }
}