using TMPro;
using UnityEngine;

public class LoginUI : MonoBehaviour
{
    public Canvas loginCanvas;
    public Canvas registrationCanvas;
    public Canvas recoverPasswordCanvas;
    public Canvas notificationCanvas;

    public TMP_InputField[] loginInputFields;
    public TMP_InputField[] registrationInputFields;
    public TMP_InputField[] recoverPasswordInputFields;

    // Start is called before the first frame update
    void Start()
    {
        EnableInputFields(loginInputFields);
        DisableInputFields(registrationInputFields);
        DisableInputFields(recoverPasswordInputFields);

        SetCanvasVisibility(true, false, false, false);
    }

    void SetCanvasVisibility(bool login, bool registration, bool recoverPassword, bool notification)
    {
        loginCanvas.enabled = login;
        registrationCanvas.enabled = registration;
        recoverPasswordCanvas.enabled = recoverPassword;
        notificationCanvas.enabled = notification;
    }

    public void OnRegisterButtonClick()
    {
        SetCanvasVisibility(false, true, false, false);
        EnableInputFields(registrationInputFields);
        DisableInputFields(loginInputFields);
    }

    public void OnBackButtonCLick()
    {
        SetCanvasVisibility(true, false, false, false);
        EnableInputFields(loginInputFields);
    }

    public void OnReturnButtonClick()
    {
        SetCanvasVisibility(true, false, false, false);
        EnableInputFields(loginInputFields);
    }

    public void OnForgotPasswordButtonClick()
    {
        SetCanvasVisibility(false, false, true, false);
        EnableInputFields(recoverPasswordInputFields);
    }

    void EnableInputFields(TMP_InputField[] fields)
    {
        foreach (TMP_InputField field in fields)
        {
            field.interactable = true;
        }
    }

    void DisableInputFields(TMP_InputField[] fields)
    {
        foreach (TMP_InputField field in fields)
        {
            field.interactable = false;
        }
    }
}
