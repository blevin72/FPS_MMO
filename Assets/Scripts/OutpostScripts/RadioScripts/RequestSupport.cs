using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Collections.Generic;

public class RequestSupport : MonoBehaviour
{
    public GameManager gameManager;
    public LoadSurvivor loadSurvivor;
    public GameObject distressCallPrefab;

    public TMP_Dropdown missionType;
    public TMP_Dropdown commencement;
    public TMP_InputField rations;
    public TMP_InputField bandages;
    public TMP_InputField lockpick;
    public TMP_InputField medkits;
    public TMP_InputField water;
    public TMP_InputField ammoBox_1;
    public TMP_InputField ammoBox_2;
    public TMP_InputField ammoBox_3;
    public TMP_Dropdown ammoType_1;
    public TMP_Dropdown ammoType_2;
    public TMP_Dropdown ammoType_3;
    public TMP_InputField message;

    private string requestSupportURL = "http://localhost:8888/sqlconnect/requestSupport.php?action=insert";
    private string retrieveDistressCallsURL = "http://localhost:8888/sqlconnect/requestSupport.php?action=select";

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>(); //ensures the GameManager instance is located
        DefaultInputFields();
    }

    public void AcceptButton()
    {
        StartCoroutine(SendHelpRequest());
    }

    private IEnumerator SendHelpRequest()
    {
        // Create a WWWForm to send data to the PHP script
        WWWForm form = new WWWForm();

        form.AddField("characterID", gameManager.loadedCharacter);
        form.AddField("mission_type", missionType.options[missionType.value].text);
        form.AddField("commencement", commencement.options[commencement.value].text);
        form.AddField("rations", rations.text);
        form.AddField("bandages", bandages.text);
        form.AddField("lock_picks", lockpick.text);
        form.AddField("med_kits", medkits.text);
        form.AddField("water", water.text);
        form.AddField("ammo_boxes_one", ammoBox_1.text);
        form.AddField("ammo_boxes_two", ammoBox_2.text);
        form.AddField("ammo_boxes_three", ammoBox_3.text);
        form.AddField("ammo_type_one", ammoType_1.options[ammoType_1.value].text);
        form.AddField("ammo_type_two", ammoType_2.options[ammoType_2.value].text);
        form.AddField("ammo_type_three", ammoType_3.options[ammoType_3.value].text);
        form.AddField("request_message", message.text);

        UnityWebRequest www = UnityWebRequest.Post(requestSupportURL, form);
        yield return www.SendWebRequest();

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

    private TextMeshProUGUI OP_Name;
    private TextMeshProUGUI OP_Rank;
    private TextMeshProUGUI missionName;
    //private TextMeshProUGUI compensation;
    private TextMeshProUGUI waitingTime;
    public GameObject openDistressCall_SV = null;

    //eventually this will need to be on a per server basis in the database
    internal IEnumerator RetrieveAllDistressCalls()
    {
        string getRequestURL = retrieveDistressCallsURL;

        UnityWebRequest www = UnityWebRequest.Get(getRequestURL);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string responseText = www.downloadHandler.text;

            Debug.Log("Reponse Text: " + responseText);

            // Deserialize JSON array to a List of strings
            List<SettingsData> distressCalls= JsonConvert.DeserializeObject<List<SettingsData>>(responseText);

            // Find the Content object
            Transform openDistressCallsContentTransform = openDistressCall_SV.transform.Find("Viewport/Content");

            foreach (var settingsData in distressCalls)
            {
                // Instantiate the new distress call object with Content as the parent
                GameObject newDistressCall = Instantiate(distressCallPrefab, openDistressCallsContentTransform);

                // Get TextMeshProUGUI components of the instantiated prefab
                TextMeshProUGUI OP_Name = newDistressCall.transform.Find("OP_Name").GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI OP_Rank = newDistressCall.transform.Find("OP_Rank").GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI missionName = newDistressCall.transform.Find("Mission_Name").GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI waitingTime = newDistressCall.transform.Find("Waiting_Time").GetComponent<TextMeshProUGUI>();

                //Get values from TMP values
                loadSurvivor.SetTextValue(OP_Name, settingsData.outpost_name);
                loadSurvivor.SetTextValue(OP_Rank, settingsData.outpost_ranking);
                loadSurvivor.SetTextValue(missionName, settingsData.mission_type);
                loadSurvivor.SetTextValue(waitingTime, settingsData.commencement);
            

                ///*Splitting the response from the database into an array of strings since the distressCall prefab has multiple TMP objects
                // as children*/
                //string[] distressCallData = distressCall.Split(',');

                //// Instantiate the new message object with Content as the parent
                //GameObject newDistressCall = Instantiate(distressCallPrefab, openDistressCallsContentTransform);

                //// Get TextMeshProUGUI components of the instantiated prefab
                //OP_Name = newDistressCall.transform.Find("OP_Name").GetComponent<TextMeshProUGUI>();
                //OP_Rank = newDistressCall.transform.Find("OP_Rank").GetComponent<TextMeshProUGUI>();
                //missionName = newDistressCall.transform.Find("MissionName").GetComponent<TextMeshProUGUI>();
                ////TextMeshProUGUI compensation = newDistressCall.transform.Find("Compensation").GetComponent<TextMeshProUGUI>();
                //waitingTime = newDistressCall.transform.Find("WaitingTime").GetComponent<TextMeshProUGUI>();

                //// Set text values based on distress call data
                ////OP_Name.text = distressCallData[0];
                ////OP_Rank.text = distressCallData[1];
                ////missionName.text = distressCallData[2];
                //////compensation.text = distressCallData[3];
                ////waitingTime.text = distressCallData[4];

                //// Set the text value of the new message object
                //loadSurvivor.SetTextValue(newDistressCall.GetComponent<TextMeshProUGUI>(), distressCallData[0]);
                //loadSurvivor.SetTextValue(newDistressCall.GetComponent<TextMeshProUGUI>(), distressCallData[1]);
                //loadSurvivor.SetTextValue(newDistressCall.GetComponent<TextMeshProUGUI>(), distressCallData[2]);
                ////loadSurvivor.SetTextValue(newDistressCall.GetComponent<TextMeshProUGUI>(), distressCallData[3]);
                //loadSurvivor.SetTextValue(newDistressCall.GetComponent<TextMeshProUGUI>(), distressCallData[4]);

                //OP_Name.text = distressCallData[0];
                //OP_Rank.text = distressCallData[1];
                //missionName.text = distressCallData[2];
                ////compensation.text = distressCallData[3];
                //waitingTime.text = distressCallData[4];
            }
        }
        else
        {
            Debug.LogError("UnityWebRequest failed: " + www.error);
        }
    }

    /*needed to set Default Text values for input fields or else if the user did not input a value (wanted to leave it as default 0) 
     * it would be read as an empty string when the value in the database is an Integer*/
    private void DefaultInputFields()
    {
        rations.text = "0";
        bandages.text = "0";
        lockpick.text = "0";
        medkits.text = "0";
        water.text = "0";
        ammoBox_1.text = "0";
        ammoBox_2.text = "0";
        ammoBox_3.text = "0";
        message.text = "Send Help!";
    }
}
