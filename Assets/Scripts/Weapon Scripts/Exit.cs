using UnityEngine;

public class Exit : MonoBehaviour
{

private void Update()
    {
        if(Input.GetKeyDown("escape")) {
			Application.Quit();
		}
    }
}
