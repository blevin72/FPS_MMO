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
    public TextMeshProUGUI[] characterID;
    public TextMeshProUGUI[] divisionProgress;
    //internal string loadedCharacterID;
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
                    UpdateOutpost(i, characterData);
                    UpdateLevel(i, characterData);
                    UpdateClass(i, characterData);
                    UpdateCharacterID(i, characterData);       
                }
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

        for (int i = 0; i < characterName.Length; i++)
        {
            form.AddField("character_name" + i, characterName[i].text);
            form.AddField("outpost_name" + i, characterOutpost[i].text);
            form.AddField("level" + i, characterLevel[i].text);
            form.AddField("classID" + i, characterClass[i].text);
            form.AddField("characterID" + i, characterID[i].text);           
        }
    }

    private void UpdateOutpost(int i, string[] characterData)
    {
        if (characterData.Length > 1) // Check if characterData has at least 2 elements
        {
            if (string.IsNullOrEmpty(characterData[1]))
            {
                characterOutpost[i].text = "Join an Outpost!";
            }
            else
            {
                characterOutpost[i].text = characterData[1];
            }
        }
        else
        {
            // Handle the case where characterData doesn't have enough elements
            characterOutpost[i].text = null;
        }
    }

    private void UpdateLevel(int i, string[] characterData)
    {
        if (characterData.Length > 2)
        {
            characterLevel[i].text = characterData[2];
        }
        else
        {
            characterLevel[i].text = null;
        }
    }

    private void UpdateClass(int i, string[] characterData)
    {
        if (characterData.Length > 3)
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
        else
        {
            characterClass[i].text = null;
        }
    }

    private void UpdateCharacterID(int i, string[] characterData)
    {
        if(characterData.Length > 4)
        {
            characterID[i].text = characterData[4];
        }
        else
        {
            characterID[i].text = null;
        }
    }

    //NOT ACTIVE YET
    //private void UpdateDivisionProgress(int i, string[] characterData)
    //{
    //    if (characterData.Length > 5) // Check if characterData has at least 2 elements
    //    {
    //        if (string.IsNullOrEmpty(characterData[1]))
    //        {
    //            divisionProgress[i].text = "Coming Soon";
    //        }
    //        else
    //        {
    //            divisionProgress[i].text = characterData[5];
    //        }
    //    }
    //    else
    //    {
    //        // Handle the case where characterData doesn't have enough elements
    //        divisionProgress[i].text = null;
    //    }
    //}
}
