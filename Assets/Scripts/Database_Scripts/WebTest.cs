using System.Collections;
using UnityEngine.Networking;
using UnityEngine;

public class WebTest : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        UnityWebRequest request = UnityWebRequest.Get("http://localhost:8888/sqlconnect/webtest.php");
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Error: " + request.error);
        }
        else
        {
            if (request.downloadHandler.text == "")
            {
                Debug.Log("Downloaded text NULL");
            }

            Debug.Log("Success. \tLine was:" + request.downloadHandler.text);
        }
    }
}
