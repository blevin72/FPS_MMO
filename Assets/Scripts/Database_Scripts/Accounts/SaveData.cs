using System.Collections;
using UnityEngine.Networking;
using UnityEngine;

public class SavaData : MonoBehaviour
{
    private string saveDataURL = "http://localhost:8888/sqlconnect/savedata.php"; // Replace with your actual registration URL.

    private void Awake()
    {
        if(DB_Manager.email == null)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }

    public void CallSaveData()
    {
        StartCoroutine(SavePlayerData());
    }

    IEnumerator SavePlayerData()
    {
        WWWForm form = new WWWForm();
        form.AddField("email", DB_Manager.email);

        UnityWebRequest www = UnityWebRequest.Post(saveDataURL, form);
        yield return www.SendWebRequest();
        if(www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Game Saved");
        }
        else
        {
            Debug.Log("Save failed. Error #" + www.error);
        }
        DB_Manager.LogOut();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    /*temporary increase expereince method for testing
     * will need to create new methods for players earning experience by completing
     * missions, killing enemies, etc.*/
}
