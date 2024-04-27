using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Collections.Generic;

public class Chatroom_Manager : MonoBehaviour
{
    //chat room panels
    public GameObject[] chatrooms;
    public TMP_InputField inputField;
    public GameObject messagePrefab;

    //script references
    public GameManager gameManager;
    public UIMainMenu uIMainMenu;
    public LoadSurvivor loadSurvivor;

    //PHP URL
    private string chatroomsURL = "http://localhost:8888/sqlconnect/outpostChatrooms.php?action=insert";
    private string loadChatroomsURL = "http://localhost:8888/sqlconnect/outpostChatrooms.php?action=select";

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>(); //ensures the GameManager instance is located
    }

    public void SubmitChat()
    {
        ChatType();
        StartCoroutine(SendChats());
        inputField.text = ""; //reset input field
    }

    private IEnumerator SendChats()
    {
        WWWForm form = new WWWForm();
        form.AddField("characterID", gameManager.loadedCharacter);
        form.AddField("chat_type", chatType);
        form.AddField("content", inputField.text);
        UnityWebRequest www = UnityWebRequest.Post(chatroomsURL, form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string response = www.downloadHandler.text;
            if (response.StartsWith("0")) // Check if the insertion failed
            {
                Debug.LogError("Insert failed. Error: " + response);
            }
            else
            {
                Debug.Log("Insertion successful");
            }
        }
        else
        {
            Debug.LogError("Network error: " + www.error);
        }

        // Reset input field after submission
        inputField.text = "";
    }

    public IEnumerator RetrieveChats()
    {
        ChatType();
        Debug.Log("Chat Type is:" + chatType);
        Debug.Log("Retrieve Chats method called");

        string getRequestURL = loadChatroomsURL + "&characterID=" + gameManager.loadedCharacter + "&chat_type=" + chatType;

        UnityWebRequest www = UnityWebRequest.Get(getRequestURL);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string responseText = www.downloadHandler.text;

            Debug.Log("Response Text:" + responseText);

            // Deserialize JSON array to a List of strings
            List<string> messages = JsonConvert.DeserializeObject<List<string>>(responseText);

            // Assuming chatrooms[0] is the reference to your ScrollView object
            GameObject conversePanel = chatrooms[0];

            // Find the Content object within the hierarchy of chatroom[0]
            Transform contentTransform = conversePanel.transform.Find("Viewport/Content");

            // Check if the Content object is found
            if (contentTransform != null)
            {
                // Iterate over the list of messages
                foreach (var message in messages)
                {
                    // Instantiate the new message object with Content as the parent
                    GameObject newMessageObject = Instantiate(messagePrefab, contentTransform);

                    // Set the text value of the new message object
                    loadSurvivor.SetTextValue(newMessageObject.GetComponent<TextMeshProUGUI>(), message);
                }
            }
        }
        else
        {
            Debug.LogError("UnityWebRequest failed: " + www.error);
        }
    }


    private string chatType = "general";

    //called in Converse Panel Region in UI_MainMenu class
    internal void ChatType()
    {
        if (uIMainMenu.generalChatSV.activeSelf)
        {
            chatType = "general";
        }
        else if (uIMainMenu.missionsChatSV.activeSelf)
        {
            chatType = "missions";
        }
        else if (uIMainMenu.tradeChatSV.activeSelf)
        {
            chatType = "trade";
        }
        else if (uIMainMenu.eventsChatSV.activeSelf)
        {
            chatType = "event";
        }
    }
}
