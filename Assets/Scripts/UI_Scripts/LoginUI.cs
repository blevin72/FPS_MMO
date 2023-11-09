using UnityEngine;

public class LoginUI : MonoBehaviour
{
    public Canvas loginCanvas;
    public Canvas registrationCanvas;
    public Canvas recoverPasswordCanvas;
    public Canvas notificationCanvas;

    // Start is called before the first frame update
    void Start()
    {
        loginCanvas.enabled = true;
        registrationCanvas.enabled = false;
        recoverPasswordCanvas.enabled = false;
        notificationCanvas.enabled = false;
    }

    public void OnRegisterButtonClick()
    {
        registrationCanvas.enabled = true;
        loginCanvas.enabled = false;
        recoverPasswordCanvas.enabled = false;
        notificationCanvas.enabled = false;
    }

    public void OnBackButtonCLick()
    {
        loginCanvas.enabled = true;
        registrationCanvas.enabled = false;
        recoverPasswordCanvas.enabled = false;
    }

    public void OnReturnButtonClick()
    {
        loginCanvas.enabled = true;
        notificationCanvas.enabled = false;
    }
}
