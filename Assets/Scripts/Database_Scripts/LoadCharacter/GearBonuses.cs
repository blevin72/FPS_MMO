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

    private string gearBonusURL = "http://localhost:8888/sqlconnect/gearBonus.php?action=select";

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>(); //ensures the GameManager instance is located
    }

    internal IEnumerator ApplyGearBonuses() //being called in UI_MainMenu (OnSurvivorButtonClick() for testing)
    {
        Debug.Log("Apply Gear Bonuses Method called");

        string getRequestURL = gearBonusURL + "&characterID=" + gameManager.loadedCharacter;
        UnityWebRequest www = UnityWebRequest.Get(getRequestURL);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string responseText = www.downloadHandler.text;

            // Deserialize JSON to SettingsData
            SettingsData settingsData = JsonConvert.DeserializeObject<SettingsData>(responseText);

            loadSurvivor.SetTextValue(melee, settingsData.meleeModifier);
            //loadSurvivor.SetTextValue(capacity, settingsData.capacity); //At the moment no gear increases capacity bonus
            //loadSurvivor.SetTextValue(critical, settingsData.critical); //ATM no gear gear increases critical bonus
            //loadSurvivor.SetTextValue(escape, settingsData.escape); //ATM no gear increaess escape bonus
            loadSurvivor.SetTextValue(speed, settingsData.speedModifier);
            //loadSurvivor.SetTextValue(lockpick, settingsData.lockpick); //ATM no gear increaess lockpick bonus
            loadSurvivor.SetTextValue(craft, settingsData.craftModifier);
            //loadSurvivor.SetTextValue(experience, settingsData.experience); //ATM no gear increases experience bonus
            //loadSurvivor.SetTextValue(health, settingsData.health); //ATM no gear increases health bonus
            loadSurvivor.SetTextValue(stamina, settingsData.staminaModifier);
            loadSurvivor.SetTextValue(immunity, settingsData.immunityModifier);
            //loadSurvivor.SetTextValue(haggle, settingsData.haggle); //ATM no gear increases haggle bonus
            //loadSurvivor.SetTextValue(reputation, settingsData.reputation); //ATM no gear increases reputation bonus
            loadSurvivor.SetTextValue(agro, settingsData.agroModifier);
            //loadSurvivor.SetTextValue(stealth_kill, settingsData.stealth_kill); //ATM no gear increases stealth_kill bonus
            loadSurvivor.SetTextValue(protection, settingsData.protection);

            Debug.Log("Protection:" + settingsData.protection);
        }
        else
        {
            Debug.LogError("UnityWebRequest failed: " + www.error);
        }
    }
}
