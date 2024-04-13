using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class GearBonuses : MonoBehaviour
{
    public TextMeshProUGUI melee;
    public TextMeshProUGUI capacity;
    public TextMeshProUGUI critical;
    public TextMeshProUGUI escape;
    public TextMeshProUGUI speed;
    public TextMeshProUGUI lockpick;
    public TextMeshProUGUI craft;
    public TextMeshProUGUI experience;
    public TextMeshProUGUI health;
    public TextMeshProUGUI stamina;
    public TextMeshProUGUI immunity;
    public TextMeshProUGUI haggle;
    public TextMeshProUGUI reputation;
    public TextMeshProUGUI agro;
    public TextMeshProUGUI stealth_kill;
    public TextMeshProUGUI protection;

    public Image[] equippedGear;
    public GameManager gameManager;
    public LoadSurvivor loadSurvivor;

    private string protectionBonusURL = "http://localhost:8888/sqlconnect/gearBonus.php?action=protectionQuery";
    private string meleeBonusURL = "http://localhost:8888/sqlconnect/gearBonus.php?action=meleeQuery";

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>(); //ensures the GameManager instance is located
    }

    internal void ApplyAllBonuses() //being called in UI_MainMenu (OnSurvivorButtonClick() for testing)
    {
        StartCoroutine(ApplyProtectionBonus());
        StartCoroutine(ApplyMeleeBonus());
    }

    private IEnumerator ApplyProtectionBonus()
    {
        string getRequestURL = protectionBonusURL + "&characterID=" + gameManager.loadedCharacter;
        UnityWebRequest www = UnityWebRequest.Get(getRequestURL);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string responseText = www.downloadHandler.text;

            // Deserialize JSON to SettingsData
            SettingsData settingsData = JsonConvert.DeserializeObject<SettingsData>(responseText);

            //loadSurvivor.SetTextValue(speed, settingsData.speedModifier);
            //loadSurvivor.SetTextValue(craft, settingsData.craftModifier);
            //loadSurvivor.SetTextValue(stamina, settingsData.staminaModifier);
            //loadSurvivor.SetTextValue(immunity, settingsData.immunityModifier);
            //loadSurvivor.SetTextValue(agro, settingsData.agroModifier);
            loadSurvivor.SetTextValue(protection, settingsData.protection);

            Debug.Log("Protection: " + settingsData.protection);
        }
        else
        {
            Debug.LogError("UnityWebRequest failed: " + www.error);
        }
    }

    private IEnumerator ApplyMeleeBonus()
    {
        string getRequestURL = meleeBonusURL + "&characterID=" + gameManager.loadedCharacter;
        UnityWebRequest www = UnityWebRequest.Get(getRequestURL);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string responseText = www.downloadHandler.text;

            // Deserialize JSON to SettingsData
            SettingsData settingsData = JsonConvert.DeserializeObject<SettingsData>(responseText);

            loadSurvivor.SetTextValue(melee, settingsData.meleeBonus);
            Debug.Log("Melee: " + settingsData.meleeBonus);
        }
    }
}
