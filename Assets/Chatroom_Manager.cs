using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class Chatroom_Manager : MonoBehaviour
{
    //chat room panels
    //public GameObject[] chatrooms;
    public TMP_InputField inputField;

    //script references
    public GameManager gameManager;
    public UIMainMenu uIMainMenu;

    //PHP URL
    private string chatroomsURL = "http://localhost:8888/sqlconnect/outpostChatrooms.php?action=insert";

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
        Debug.Log("Send Chats called");
        // Create a WWWForm to send data to the PHP script
        WWWForm form = new WWWForm();

        form.AddField("characterID", gameManager.loadedCharacter);
        form.AddField("chat_type", chatType);
        form.AddField("content", inputField.text);

        // Create a UnityWebRequest to send the form data to the PHP script
        UnityWebRequest www = UnityWebRequest.Post(chatroomsURL, form);
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
                Debug.LogError("Insert failed. Error: " + response);
            }
        }
        else
        {
            Debug.LogError("Network error: " + www.error);
        }
    }

    private string chatType;

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
