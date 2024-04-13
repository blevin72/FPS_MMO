using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class LoadSurvivor : MonoBehaviour
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
    //public TextMeshProUGUI skillPoints; NEED TO READD UI SINCE DELETING SURVIVOR STATS PANEL

    public TextMeshProUGUI characterName;

    public GameManager gameManager; //references the GameManager class

    //PHP URL's
    private string loadSurvivorRankingsURL = "http://localhost:8888/sqlconnect/survivorRanking.php?action=get_characterStats";
    private string saveSurvivorRankingsURL = "http://localhost:8888/sqlconnect/survivorRanking.php?action=save_characterStats";

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>(); //ensures the GameManager instance is located
    }

    public void SaveStats()
    {
        StartCoroutine(SaveSurvivorRankings());
    }


    /*called in UI_MainMenu Script --> Region: Main Menu --> OnSurvivorButtonClick()
     Loads the survivor ranking panel (top right corner of Survivor Canvas as well as survivor stats*/
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
            //SetTextValue(skillPoints, settingsData.skill_points); OFF for testing
            SetTextValue(characterName, settingsData.character_name);
        }
        else
        {
            Debug.LogError("UnityWebRequest failed: " + www.error);
        }
    }

    //Saves survivor stats to the database
    public IEnumerator SaveSurvivorRankings()
    {
        // Create a WWWForm to send data to the PHP script
        WWWForm form = new WWWForm();

        form.AddField("characterID", gameManager.loadedCharacter);
        form.AddField("strength", int.Parse(strengthPoints.text)); //remember to convert to int (TMP is string, columns in DB are Int's)
        form.AddField("dexterity", int.Parse(dexterityPoints.text));
        form.AddField("intellect", int.Parse(intellectPoints.text));
        form.AddField("endurance", int.Parse(endurancePoints.text));
        form.AddField("charm", int.Parse(charmPoints.text));
        form.AddField("stealth", int.Parse(stealthPoints.text));
        form.AddField("total_health", int.Parse(maximumHealth.text));
        form.AddField("total_stamina", int.Parse(maximumStamina.text));
        form.AddField("total_protection", int.Parse(totalProtection.text));
        form.AddField("total_progression", int.Parse(experienceBoost.text));

        // Create a UnityWebRequest to send the form data to the PHP script
        UnityWebRequest www = UnityWebRequest.Post(saveSurvivorRankingsURL, form);
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

    internal void SetTextValue(TextMeshProUGUI tmpComponent, string value)
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
