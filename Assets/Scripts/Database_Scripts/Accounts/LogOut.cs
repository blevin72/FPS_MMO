using UnityEngine;
using UnityEngine.UI;

public class LogOut : MonoBehaviour
{
    public Button logOutButton;

    public void CallLogOut()
    {
        DB_Manager.LogOut();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}

