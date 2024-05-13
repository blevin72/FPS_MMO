using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.Networking;

public class RequestSupport : MonoBehaviour
{
    public GameManager gameManager;

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
