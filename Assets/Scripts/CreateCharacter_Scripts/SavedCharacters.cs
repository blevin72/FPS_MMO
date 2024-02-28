using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Networking;

public class SavedCharacters : MonoBehaviour
{
    public TextMeshProUGUI[] characterName;
    public TextMeshProUGUI[] characterOutpost;
    public TextMeshProUGUI[] characterLevel;
    public TextMeshProUGUI[] characterClass;
    //public TextMeshProUGUI[] outpostRanking;
    //public TextMeshProUGUI[] outpostReputation;
    //public TextMeshProUGUI[] divisionProgress;
    //public TextMeshProUGUI[] onlinePlayers;
    //public TextMeshProUGUI[] currentResources;

    private string savedCharactersURL = "http://localhost:8888/sqlconnect/savedCharacters.php?action=select";

    internal IEnumerator RetrieveSavedCharacters()
    {
        WWWForm form = new WWWForm();

        //add to the form
        AddFields(form);

        // Create a UnityWebRequest to send the form data to the PHP script
        UnityWebRequest www = UnityWebRequest.Post(savedCharactersURL, form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Character retrieval failed. Error: " + www.error);
        }
        else
        {
            string responseText = www.downloadHandler.text;

            if (!string.IsNullOrEmpty(responseText))
            {
                // Split the response string by "|" to get individual character data
                string[] characterDataArray = responseText.Split('|');

                for (int i = 0; i < characterDataArray.Length; i++)
                {
                    // Split each character's data by ","
                    string[] characterData = characterDataArray[i].Split(',');

                    // Update TextMeshProUGUI elements with the retrieved data
                    characterName[i].text = characterData[0];
                    characterOutpost[i].text = characterData[1];
                    characterLevel[i].text = characterData[2];
                    UpdateClass(i, characterData);
                }

                Debug.Log("Character retrieval successful");
            }
            else
            {
                Debug.Log("No characters found for the given accountID");
            }
        }
    }

    private void AddFields(WWWForm form)
    {
        form.AddField("accountID", DB_Manager.accountID);

        foreach (TextMeshProUGUI names in characterName)
        {
            form.AddField("character_name", names.text);
        }
        foreach (TextMeshProUGUI outposts in characterOutpost)
        {
            form.AddField("outpost_name", outposts.text);
        }
        foreach (TextMeshProUGUI levels in characterLevel)
        {
            form.AddField("level", levels.text);
        }
        foreach (TextMeshProUGUI classes in characterClass)
        {
            form.AddField("classID", classes.text);
        }
    }

    private void UpdateClass(int i, string[] characterData)
    {
        if (characterData[3] == "1")
        {
            characterClass[i].text = "Scout";
        }
        else if (characterData[3] == "2")
        {
            characterClass[i].text = "Medic";
        }
        else if (characterData[3] == "3")
        {
            characterClass[i].text = "Fighter";
        }
        else if (characterData[3] == "4")
        {
            characterClass[i].text = "Engineer";
        }
    }
}
