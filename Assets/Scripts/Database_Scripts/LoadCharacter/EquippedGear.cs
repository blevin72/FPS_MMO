using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using TMPro;
using Newtonsoft.Json;

public class EquippedGear : MonoBehaviour
{
    public GameManager gameManager; //references GameManager script to access loadedCharacter ID
    public LoadSurvivor loadSurvivor; //referencees LoadSurvivor script to access SetTextValue() used in SetGearWeight()

    public Image[] equippedGear;

    private string updateGearURL = "http://localhost:8888/sqlconnect/saveGear.php?action=update";
    private string setTotalGearWeightURL = "http://localhost:8888/sqlconnect/saveGear.php?action=getWeight";

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>(); //ensures the GameManager instance is located
    }

    internal IEnumerator SaveEquippedGear() //being called in UI_MainMenu (OnSurvivorButtonClick() for testing)
    {
        // Create a WWWForm to send data to the PHP script
        WWWForm form = new WWWForm();

        form.AddField("characterID", gameManager.loadedCharacter);
        form.AddField("equipped_head", equippedGear[0].sprite != null ? equippedGear[0].sprite.name : "");
        form.AddField("equipped_hands", equippedGear[1].sprite != null ? equippedGear[1].sprite.name : "");
        form.AddField("equipped_flashlights", equippedGear[2].sprite != null ? equippedGear[2].sprite.name : "");
        form.AddField("equipped_torso", equippedGear[3].sprite != null ? equippedGear[3].sprite.name : "");
        form.AddField("equipped_holster", equippedGear[4].sprite != null ? equippedGear[4].sprite.name : "");
        form.AddField("equipped_legs", equippedGear[5].sprite != null ? equippedGear[5].sprite.name : "");
        form.AddField("equipped_feet", equippedGear[6].sprite != null ? equippedGear[6].sprite.name : "");

        // Create a UnityWebRequest to send the form data to the PHP script
        UnityWebRequest www = UnityWebRequest.Post(updateGearURL, form);
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

    public TextMeshProUGUI totalWeight;

    internal IEnumerator SetGearWeight() //being called in UI_MainMenu (OnSurvivorButtonClick() for testing)
    {
        string getRequestURL = setTotalGearWeightURL + "&characterID=" + gameManager.loadedCharacter;
        UnityWebRequest www = UnityWebRequest.Get(getRequestURL);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string responseText = www.downloadHandler.text;

            // Deserialize JSON to SettingsData
            SettingsData settingsData = JsonConvert.DeserializeObject<SettingsData>(responseText);

            string totalWeightString = "Weight: " + settingsData.totalWeight + " kg";

            loadSurvivor.SetTextValue(totalWeight, totalWeightString);
        }
        else
        {
            Debug.LogError("UnityWebRequest failed: " + www.error);
        }
    }  
}
