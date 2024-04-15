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
    private string speedBonusURL = "http://localhost:8888/sqlconnect/gearBonus.php?action=speedQuery";
    private string craftBonusURL = "http://localhost:8888/sqlconnect/gearBonus.php?action=craftQuery";
    private string staminaBonusURL = "http://localhost:8888/sqlconnect/gearBonus.php?action=staminaQuery";
    private string immunityBonusURL = "http://localhost:8888/sqlconnect/gearBonus.php?action=immunityQuery";
    private string agroBonusURL = "http://localhost:8888/sqlconnect/gearBonus.php?action=agroQuery";

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>(); //ensures the GameManager instance is located
    }

    internal void ApplyAllBonuses() //being called in UI_MainMenu (OnSurvivorButtonClick() for testing)
    {
        StartCoroutine(ApplyProtectionBonus());
        StartCoroutine(ApplyMeleeBonus());
        StartCoroutine(ApplySpeedBonus());
        StartCoroutine(ApplyCraftBonus());
        StartCoroutine(ApplyStaminaBonus());
        StartCoroutine(ApplyImmunityBonus());
        StartCoroutine(ApplyAgroBonus());
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

            loadSurvivor.SetTextValue(protection, settingsData.protection);
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
        }
        else
        {
            Debug.LogError("UnityWebRequest failed: " + www.error);
        }
    }

    private IEnumerator ApplySpeedBonus()
    {
        string getRequestURL = speedBonusURL + "&characterID=" + gameManager.loadedCharacter;
        UnityWebRequest www = UnityWebRequest.Get(getRequestURL);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string responseText = www.downloadHandler.text;

            // Deserialize JSON to SettingsData
            SettingsData settingsData = JsonConvert.DeserializeObject<SettingsData>(responseText);

            loadSurvivor.SetTextValue(speed, settingsData.speedBonus);
        }
        else
        {
            Debug.LogError("UnityWebRequest failed: " + www.error);
        }
    }

    private IEnumerator ApplyCraftBonus()
    {
        string getRequestURL = craftBonusURL + "&characterID=" + gameManager.loadedCharacter;
        UnityWebRequest www = UnityWebRequest.Get(getRequestURL);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string responseText = www.downloadHandler.text;

            // Deserialize JSON to SettingsData
            SettingsData settingsData = JsonConvert.DeserializeObject<SettingsData>(responseText);

            loadSurvivor.SetTextValue(craft, settingsData.craftBonus);
        }
        else
        {
            Debug.LogError("UnityWebRequest failed: " + www.error);
        }
    }

    private IEnumerator ApplyStaminaBonus()
    {
        string getRequestURL = staminaBonusURL + "&characterID=" + gameManager.loadedCharacter;
        UnityWebRequest www = UnityWebRequest.Get(getRequestURL);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string responseText = www.downloadHandler.text;

            // Deserialize JSON to SettingsData
            SettingsData settingsData = JsonConvert.DeserializeObject<SettingsData>(responseText);

            loadSurvivor.SetTextValue(stamina, settingsData.staminaBonus);
        }
        else
        {
            Debug.LogError("UnityWebRequest failed: " + www.error);
        }
    }

    private IEnumerator ApplyImmunityBonus()
    {
        string getRequestURL = immunityBonusURL + "&characterID=" + gameManager.loadedCharacter;
        UnityWebRequest www = UnityWebRequest.Get(getRequestURL);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string responseText = www.downloadHandler.text;

            // Deserialize JSON to SettingsData
            SettingsData settingsData = JsonConvert.DeserializeObject<SettingsData>(responseText);

            loadSurvivor.SetTextValue(immunity, settingsData.immunityBonus);
        }
        else
        {
            Debug.LogError("UnityWebRequest failed: " + www.error);
        }
    }

    private IEnumerator ApplyAgroBonus()
    {
        string getRequestURL = agroBonusURL + "&characterID=" + gameManager.loadedCharacter;
        UnityWebRequest www = UnityWebRequest.Get(getRequestURL);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string responseText = www.downloadHandler.text;

            // Deserialize JSON to SettingsData
            SettingsData settingsData = JsonConvert.DeserializeObject<SettingsData>(responseText);

            loadSurvivor.SetTextValue(agro, settingsData.agroBonus);
        }
        else
        {
            Debug.LogError("UnityWebRequest failed: " + www.error);
        }
    }
}
