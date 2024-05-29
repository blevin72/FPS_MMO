using System.Collections;
using TMPro;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class CompensationForm : MonoBehaviour
{
    public TextMeshProUGUI rationsAmount;
    public TextMeshProUGUI bandagesAmount;
    public TextMeshProUGUI lockpicksAmount;
    public TextMeshProUGUI medkitsAmount;
    public TextMeshProUGUI waterAmount;
    public TextMeshProUGUI ammoOneAmount;
    public TextMeshProUGUI ammoOneType;
    public TextMeshProUGUI ammoTwoAmount;
    public TextMeshProUGUI ammoTwoType;
    public TextMeshProUGUI ammoThreeAmount;
    public TextMeshProUGUI ammoThreeType;
    public TextMeshProUGUI message;

    public RequestSupport requestSupport;
    public LoadSurvivor loadSurvivor;
    public GameManager gameManager;

    private string requestSupportURL = "http://localhost:8888/sqlconnect/compensationForm.php?action=select";

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>(); //ensures the GameManager instance is located
    }

    internal IEnumerator FillCompensationForm()
    {
        string getRequestURL = requestSupportURL + "&distressCall_ID=" + gameManager.distressCallID;

        UnityWebRequest www = UnityWebRequest.Get(getRequestURL);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string responseText = www.downloadHandler.text;

            Debug.Log("Response Text:" + responseText);

            // Deserialize JSON to SettingsData
            SettingsData settingsData = JsonConvert.DeserializeObject<SettingsData>(responseText);

            Debug.Log("Settings Data:" + settingsData);

            //TextMeshProUGUI rationsAmountText = rations.transform.Find("Amount").GetComponent<TextMeshProUGUI>();
            //TextMeshProUGUI bandagesAmountText = bandages.transform.Find("Amount").GetComponent<TextMeshProUGUI>();
            //TextMeshProUGUI lockpicksAmountText = lockpicks.transform.Find("Amount").GetComponent<TextMeshProUGUI>();
            //TextMeshProUGUI medkitsAmountText = medkits.transform.Find("Amount").GetComponent<TextMeshProUGUI>();
            //TextMeshProUGUI waterAmountText = water.transform.Find("Amount").GetComponent<TextMeshProUGUI>();
            //TextMeshProUGUI ammoOneAmountText = ammo_1.transform.Find("Amount").GetComponent<TextMeshProUGUI>();
            //TextMeshProUGUI ammoOneTypeText = ammo_1.transform.Find("Item_Type").GetComponent<TextMeshProUGUI>();
            //TextMeshProUGUI ammoTwoAmountText = ammo_2.transform.Find("Amount").GetComponent<TextMeshProUGUI>();
            //TextMeshProUGUI ammoTwoTypeText = ammo_2.transform.Find("Item_Type").GetComponent<TextMeshProUGUI>();
            //TextMeshProUGUI ammoThreeAmountText = ammo_3.transform.Find("Amount").GetComponent<TextMeshProUGUI>();
            //TextMeshProUGUI ammoThreeTypeText = ammo_3.transform.Find("Item_Type").GetComponent<TextMeshProUGUI>();

            //loadSurvivor.SetTextValue(rationsAmount, settingsData.rations);
            //loadSurvivor.SetTextValue(bandagesAmount, settingsData.bandages);
            //loadSurvivor.SetTextValue(lockpicksAmount, settingsData.lock_picks);
            //loadSurvivor.SetTextValue(medkitsAmount, settingsData.med_kits);
            //loadSurvivor.SetTextValue(waterAmount, settingsData.water);
            //loadSurvivor.SetTextValue(ammoOneAmount, settingsData.ammo_boxes_one);
            //loadSurvivor.SetTextValue(ammoOneType, settingsData.ammo_type_one);
            //loadSurvivor.SetTextValue(ammoTwoAmount, settingsData.ammo_boxes_two);
            //loadSurvivor.SetTextValue(ammoTwoType, settingsData.ammo_type_two);
            //loadSurvivor.SetTextValue(ammoThreeAmount, settingsData.ammo_boxes_three);
            //loadSurvivor.SetTextValue(ammoThreeType, settingsData.ammo_type_three);


            rationsAmount.SetText(settingsData.rations);
            bandagesAmount.SetText(settingsData.bandages);
            lockpicksAmount.SetText(settingsData.lock_picks);
            medkitsAmount.SetText(settingsData.med_kits);
            waterAmount.SetText(settingsData.water);
            ammoOneAmount.SetText(settingsData.ammo_boxes_one);
            ammoOneType.SetText(settingsData.ammo_type_one);
            ammoTwoAmount.SetText(settingsData.ammo_boxes_two);
            ammoTwoType.SetText(settingsData.ammo_type_two);
            ammoThreeAmount.SetText(settingsData.ammo_boxes_three);
            ammoThreeType.SetText(settingsData.ammo_type_three);
            message.SetText(settingsData.request_message);
        }
        else
        {
            Debug.LogError("UnityWebRequest failed: " + www.error);
        }
    }
}


