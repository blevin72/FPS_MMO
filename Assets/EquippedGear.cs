using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class EquippedGear : MonoBehaviour
{
    public GameManager gameManager; //references GameManager script to access loadedCharacter ID

    public Image[] equippedGear;

    private string updateGearURL = "http://localhost:8888/sqlconnect/saveGear.php?action=update";

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>(); //ensures the GameManager instance is located
    }

    internal IEnumerator SaveEquippedGear()
    {
        Debug.Log("Saved Equipped Gear");
        // Create a WWWForm to send data to the PHP script
        WWWForm form = new WWWForm();
        Debug.Log("CharacterID: " + gameManager.loadedCharacter);

        Debug.Log("EquippedGear[0]: " + (equippedGear[0].sprite != null ? equippedGear[0].sprite.name : "Sprite is null"));
        Debug.Log("EquippedGear[1]: " + (equippedGear[1].sprite != null ? equippedGear[1].sprite.name : "Sprite is null"));
        Debug.Log("EquippedGear[2]: " + (equippedGear[2].sprite != null ? equippedGear[2].sprite.name : "Sprite is null"));
        Debug.Log("EquippedGear[3]: " + (equippedGear[3].sprite != null ? equippedGear[3].sprite.name : "Sprite is null"));
        Debug.Log("EquippedGear[4]: " + (equippedGear[4].sprite != null ? equippedGear[4].sprite.name : "Sprite is null"));
        Debug.Log("EquippedGear[5]: " + (equippedGear[5].sprite != null ? equippedGear[5].sprite.name : "Sprite is null"));
        Debug.Log("EquippedGear[6]: " + (equippedGear[6].sprite != null ? equippedGear[6].sprite.name : "Sprite is null"));

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
}
