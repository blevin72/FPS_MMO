using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class Registration : MonoBehaviour
{
    public TMP_InputField usernameField;
    public TMP_InputField passwordField;
    public TMP_InputField confirmPasswordField;
    public TMP_InputField emailField;
    public Canvas loginCanvas;
    public Canvas registrationCanvas;

    public Button registerButton;

    private string registrationURL = "http://localhost:8888/sqlconnect/register.php"; // Replace with your actual registration URL.

    public void CallRegister()
    {
        StartCoroutine(Register());
    }

    IEnumerator Register()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", usernameField.text); //the "text" is accessing the text component of the Input Field Game Object
        form.AddField("password", passwordField.text); // Make sure the field name matches the one in your PHP script.
        form.AddField("email", emailField.text);

        UnityWebRequest www = UnityWebRequest.Post(registrationURL, form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("User creation failed. Error: " + www.error);
        }
        else
        {
            string responseText = www.downloadHandler.text;
            if (responseText == "0")
            {
                Debug.Log("User created successfully");
                registrationCanvas.enabled = false;
                loginCanvas.enabled = true;
            }
            else
            {
                Debug.Log("User creation failed. Error #" + responseText);
            }
        }
    }

    public void VerifyInputs()
    {
        // Disable the register button if the username and password do not meet the minimum length requirements.
        registerButton.interactable = (usernameField.text.Length >= 8 && passwordField.text.Length >= 8);
    }
}
