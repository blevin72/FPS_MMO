using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;

public class Registration : MonoBehaviour
{
    public TMP_InputField usernameField;
    public TMP_InputField passwordField;
    public TMP_InputField confirmPasswordField;
    public TMP_InputField emailField;
    public Canvas loginCanvas;
    public Canvas registrationCanvas;
    public Button registerButton;

    //used for the tab feature through InputFields
    public TMP_InputField[] inputFields;
    private int lastIndex = -1; // Initialize as -1 to indicate no field has been selected initially

    private string registrationURL = "http://localhost:8888/sqlconnect/register.php"; // Replace with your actual registration URL.

    private void Start()
    {
        emailField.onEndEdit.AddListener(delegate { VerifyInputs(); });
        passwordField.onEndEdit.AddListener(delegate { VerifyInputs(); });
    }

    //tab through InputFields
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !Input.GetKey(KeyCode.LeftShift) && inputFields.Length > 0)
        {
            int nextIndex = (lastIndex + 1) % inputFields.Length;
            inputFields[nextIndex].Select();
            lastIndex = nextIndex;
        }
    }

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
        // Password requirements regex
        string passwordPattern = @"^(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$";
        Regex regex = new Regex(passwordPattern);

        bool isPasswordValid = regex.IsMatch(passwordField.text);

        // Disable the register button if the username and password do not meet the requirements.
        registerButton.interactable = (emailField.text.Contains("@") && isPasswordValid);
    }
}
