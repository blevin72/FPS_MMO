using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class SurvivorRanking : MonoBehaviour
{
    public TextMeshProUGUI survivorLevel;
    public TextMeshProUGUI outpostRanking; //not currently in use
    public TextMeshProUGUI experience;

    public GameManager gameManager; //references the GameManager class

    private string loadSurvivorRankingsURL = "http://localhost:8888/sqlconnect/survivorRanking.php?action=get_character";

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>(); //ensures the GameManager instance is located
    }

    //called in UI_MainMenu Script --> Region: Main Menu --> OnSurvivorButtonClick()
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
            Debug.Log("Survivor Level is: " + settingsData.level);
            //SetTextValue(outpostRanking, settingsData.outpost_ranking); OUTPOSTS NOT CURRENTLY SET UP YET
            SetTextValue(experience, settingsData.exp);
            Debug.Log("Survivor Experience is: " + settingsData.exp);
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
