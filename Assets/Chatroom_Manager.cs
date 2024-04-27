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

        StartCoroutine(RetrieveChats());
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

            //Clear existing messages from all chat panels
            ClearChatPanels();

            // Assuming chatrooms[0] is the reference to your ScrollView object
            GameObject generalPanel = chatrooms[0];
            GameObject missionPanel = chatrooms[1];
            GameObject eventPanel = chatrooms[2];
            GameObject tradePanel = chatrooms[3];

            // Find the Content object within the hierarchy of chatroom[0]
            Transform generalContentTransform = generalPanel.transform.Find("Viewport/Content");
            Transform missionContentTransform = missionPanel.transform.Find("Viewport/Content");
            Transform eventContentTransform = eventPanel.transform.Find("Viewport/Content");
            Transform tradeContentTransform = tradePanel.transform.Find("Viewport/Content");

            // Check if the Content object is found
            if (generalContentTransform != null)
            {
                // Iterate over the list of messages
                foreach (var message in messages)
                {
                    // Instantiate the new message object with Content as the parent
                    GameObject newGeneralMessage = Instantiate(messagePrefab, generalContentTransform);
                    GameObject newMissionMessage = Instantiate(messagePrefab, missionContentTransform);
                    GameObject newEventMessage = Instantiate(messagePrefab, eventContentTransform);
                    GameObject newTradeMessage = Instantiate(messagePrefab, tradeContentTransform);


                    // Set the text value of the new message object
                    loadSurvivor.SetTextValue(newGeneralMessage.GetComponent<TextMeshProUGUI>(), message);
                    loadSurvivor.SetTextValue(newMissionMessage.GetComponent<TextMeshProUGUI>(), message);
                    loadSurvivor.SetTextValue(newEventMessage.GetComponent<TextMeshProUGUI>(), message);
                    loadSurvivor.SetTextValue(newTradeMessage.GetComponent<TextMeshProUGUI>(), message);
                }
            }
        }
        else
        {
            Debug.LogError("UnityWebRequest failed: " + www.error);
        }
    }

    //clear chats when switching from one panel to the next
    private void ClearChatPanels()
    {
        // Clear existing messages in each panel
        foreach (GameObject panel in chatrooms)
        {
            Transform contentTransform = panel.transform.Find("Viewport/Content");
            if (contentTransform != null)
            {
                foreach (Transform child in contentTransform)
                {
                    Destroy(child.gameObject);
                }
            }
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
