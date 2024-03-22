using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class SurvivorRanking : MonoBehaviour
{
    //Survivor Ranking TMP Objects
    public TextMeshProUGUI survivorLevel;
    public TextMeshProUGUI outpostRanking;
    public TextMeshProUGUI experience;


    //Survivor Stats TMP Objects
    public TextMeshProUGUI strengthPoints;
    public TextMeshProUGUI dexterityPoints;
    public TextMeshProUGUI intellectPoints;
    public TextMeshProUGUI endurancePoints;
    public TextMeshProUGUI charmPoints;
    public TextMeshProUGUI stealthPoints;
    public TextMeshProUGUI maximumHealth;
    public TextMeshProUGUI maximumStamina;
    public TextMeshProUGUI totalProtection;
    public TextMeshProUGUI experienceBoost;


    public GameManager gameManager; //references the GameManager class


    //PHP URL's
    private string loadSurvivorRankingsURL = "http://localhost:8888/sqlconnect/survivorRanking.php?action=get_characterRanking";
    private string loadSurvivorStatsURL = "http://localhost:8888/sqlconnect/survivorRanking.php?action=get_characterStas";


    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>(); //ensures the GameManager instance is located
    }


    /*called in UI_MainMenu Script --> Region: Main Menu --> OnSurvivorButtonClick()
     Loads the survivor ranking panel (top right corner of Survivor Canvas*/
    internal IEnumerator LoadSurvivorRankings()
    {
        string getRequestURL = loadSurvivorRankingsURL + "&characterID=" + gameManager.loadedCharacter;

        UnityWebRequest www = UnityWebRequest.Get(getRequestURL);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string responseText = www.downloadHandler.text;

            // Deserialize JSON to SettingsData
            SettingsData settingsData = JsonConvert.DeserializeObject<SettingsData>(responseText);

            //Get values from TMP values
            SetTextValue(survivorLevel, settingsData.level);
            SetTextValue(outpostRanking, settingsData.outpost_ranking);
            SetTextValue(experience, settingsData.exp);
            SetTextValue(strengthPoints, settingsData.strength);
            SetTextValue(dexterityPoints, settingsData.dexterity);
            SetTextValue(intellectPoints, settingsData.intellect);
            SetTextValue(endurancePoints, settingsData.endurance);
            SetTextValue(charmPoints, settingsData.charm);
            SetTextValue(stealthPoints, settingsData.stealth);
            SetTextValue(maximumHealth, settingsData.total_health);
            SetTextValue(maximumStamina, settingsData.total_stamina);
            SetTextValue(totalProtection, settingsData.total_protection);
            SetTextValue(experienceBoost, settingsData.total_progression);
        }
        else
        {
            Debug.LogError("UnityWebRequest failed: " + www.error);
        }
    }

    private void SetTextValue(TextMeshProUGUI tmpComponent, string value)
    {
        if(tmpComponent != null)
        {
            tmpComponent.text = value;
        }
        else
        {
            Debug.Log("TMP component is null");
        }
    }
}
