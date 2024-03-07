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

    private string loadSurvivorRankingsURL = "http://localhost:8888/sqlconnect/survivorRankings.php?action=get_settings";

    //called in UI_MainMenu Script --> Region: Main Menu --> OnSurvivorButtonClick()
    internal IEnumerator LoadSurvivorRankings()
    {
        string getRequestURL = loadSurvivorRankingsURL + "&characterID=" + DB_Manager.characterID;
        Debug.Log("Character ID: " + DB_Manager.characterID);
        UnityWebRequest www = UnityWebRequest.Get(getRequestURL);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string responseText = www.downloadHandler.text;

            // Deserialize JSON to SettingsData
            SettingsData settingsData = JsonConvert.DeserializeObject<SettingsData>(responseText);

            //Get values from TMP values
            SetTextValue(survivorLevel, settingsData.level);
            //SetTextValue(outpostRanking, settingsData.outpost_ranking); OUTPOSTS NOT CURRENTLY SET UP YET
            SetTextValue(experience, settingsData.exp);

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
