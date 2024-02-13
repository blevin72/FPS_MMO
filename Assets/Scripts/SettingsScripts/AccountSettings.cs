using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using Newtonsoft.Json;

public class AccountSettings : MonoBehaviour
{
    public Toggle friendRequests;
    public Toggle outpostRequests;
    public Toggle assistanceRequests;

    public Toggle everyoneMessages;
    public Toggle friendsMessages;
    public Toggle outpostMessages;

    public Toggle allEmails;
    public Toggle seasonEvents;
    public Toggle specialEvents;
    public Toggle newsletters;

    private void Start()
    {
        StartCoroutine(GetAccountSettings());
    }

    public void SetAccountSettingsDefault()
    {
        friendRequests.isOn = true;
        outpostRequests.isOn = true;
        assistanceRequests.isOn = true;
        everyoneMessages.isOn = true;
        friendsMessages.isOn = true;
        outpostMessages.isOn = true;
        allEmails.isOn = true;
        seasonEvents.isOn = true;
        specialEvents.isOn = true;
        newsletters.isOn = true;
    }

    public void SaveAccountSettingsButton()
    {
        Debug.Log("Saved all audio settings");
        StartCoroutine(SaveAccountSettings());
    }

    private string accountSettingsURL = "http://localhost:8888/sqlconnect/accountSettings.php?action=update";
    private string getAccountSettingsURL = "http://localhost:8888/sqlconnect/accountSettings.php?action=get_settings";

    private IEnumerator SaveAccountSettings()
    {
        // Create a WWWForm to send data to the PHP script
        WWWForm form = new WWWForm();

        //add player settings to the form
        form.AddField("accountID", DB_Manager.accountID);
        form.AddField("friend_requests", friendRequests.isOn ? "1" : "0");
        form.AddField("outpost_requests", outpostRequests.isOn ? "1" : "0");
        form.AddField("assistance_requests", assistanceRequests.isOn ? "1" : "0");
        form.AddField("messages_everyone", everyoneMessages.isOn ? "1" : "0");
        form.AddField("messages_friends", friendsMessages.isOn ? "1" : "0");
        form.AddField("messages_outpost", outpostMessages.isOn ? "1" : "0");
        form.AddField("emails_all", allEmails.isOn ? "1" : "0");
        form.AddField("emails_seasonEvents", seasonEvents.isOn ? "1" : "0");
        form.AddField("emails_specialEvents", specialEvents.isOn ? "1" : "0");
        form.AddField("emails_newsletters", newsletters.isOn ? "1" : "0");

        // Create a UnityWebRequest to send the form data to the PHP script
        UnityWebRequest www = UnityWebRequest.Post(accountSettingsURL, form);
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

    private IEnumerator GetAccountSettings()
    {
        string getRequestURL = getAccountSettingsURL + "&accountID=" + DB_Manager.accountID;
        UnityWebRequest www = UnityWebRequest.Get(getRequestURL);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string responseText = www.downloadHandler.text;

            // Deserialize JSON to SettingsData
            SettingsData settingsData = JsonConvert.DeserializeObject<SettingsData>(responseText);

            friendRequests.isOn = settingsData.friend_requests == "1";
            outpostRequests.isOn = settingsData.outpost_requests == "1";
            assistanceRequests.isOn = settingsData.assistance_requests == "1";
            everyoneMessages.isOn = settingsData.messages_everyone == "1";
            friendsMessages.isOn = settingsData.messages_friends== "1";
            outpostMessages.isOn = settingsData.messages_outpost== "1";
            allEmails.isOn = settingsData.emails_all == "1";
            seasonEvents.isOn = settingsData.emails_seasonEvents == "1";
            specialEvents.isOn = settingsData.emails_specialEvents == "1";
            newsletters.isOn = settingsData.emails_newsletters == "1";
        }
    }
}
