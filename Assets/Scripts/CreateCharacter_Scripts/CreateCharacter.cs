using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System.Collections;

public class CreateCharacter : MonoBehaviour
{
    public TMP_InputField characterName;
    public TMP_Dropdown chooseClass;
    private int classID;

    private string createCharacterURL = "http://localhost:8888/sqlconnect/createCharacter.php?action=update";

    internal IEnumerator SaveCharacterDetails()
    {
        GetClassID();

        // Create a WWWForm to send data to the PHP script
        WWWForm form = new WWWForm();

        // Add to the form
        form.AddField("accountID", DB_Manager.accountID);
        form.AddField("character_name", characterName.text); //taking the text input from the characterName Input Field
        form.AddField("classID", classID); //taking the class chosen from the chooseClass Dropdown

        // Create a UnityWebRequest to send the form data to the PHP script
        UnityWebRequest www = UnityWebRequest.Post(createCharacterURL, form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Character creation failed. Error: " + www.error);
        }
        else
        {
            string responseText = www.downloadHandler.text;
            if (responseText == "0")
            {
                Debug.Log("Character created successfully");
            }
            else
            {
                Debug.Log("Character creation failed. Error #" + responseText);
            }
        }
    }

    /*convert text value of chooseClass dropdown into an int that matches the Class ID's in the database
     i.e. Scout = ID 1; Medic = ID 2; Fighter = ID 3; Engineer = ID 4*/
    private void GetClassID()
    {
        string classID_text = chooseClass.options[chooseClass.value].text;

        switch (classID_text)
        {
            case "Scout":
                classID = 1;
                break;
            case "Medic":
                classID = 2;
                break;
            case "Fighter":
                classID = 3;
                break;
            case "Engineer":
                classID = 4;
                break;
        }
    }
}
