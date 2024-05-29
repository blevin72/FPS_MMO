using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text.RegularExpressions;

public class Login : MonoBehaviour
{
    public TMP_InputField passwordField;
    public TMP_InputField emailField;
    public Canvas loginCanvas;
    public Canvas notificationCanvas;
    public Button signInButton;

    private string loginURL = "http://localhost:8888/sqlconnect/login.php"; // Replace with your actual registration URL.

    private void Start()
    {
        emailField.onEndEdit.AddListener(delegate { VerifyInputs(); });
        passwordField.onEndEdit.AddListener(delegate { VerifyInputs(); });
    }

    public void CallSignIn()
    {
        StartCoroutine(SignInPlayer());
    }

    IEnumerator SignInPlayer()
    {
        WWWForm form = new WWWForm();
        form.AddField("password", passwordField.text);
        form.AddField("email", emailField.text);

        UnityWebRequest www = UnityWebRequest.Post(loginURL, form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Network error: " + www.error);
        }
        else
        {
            string responseText = www.downloadHandler.text;

            if (responseText[0] == '0')
            {
                // Split the response to get accountID
                string[] responseParts = responseText.Split(':');
                if (responseParts.Length == 2 && int.TryParse(responseParts[1], out int accountID))
                {
                    DB_Manager.accountID = accountID;
                    DB_Manager.email = emailField.text;

                    UnityEngine.SceneManagement.SceneManager.LoadScene(1);
                    Debug.Log("User logged in. Email: " + DB_Manager.email + ", AccountID: " + DB_Manager.accountID);
                }
                else
                {
                    Debug.LogError("Error parsing accountID from response: " + responseText);
                }
            }
            else if (responseText.StartsWith("6:"))
            {
                Debug.Log("Incorrect Password: Displaying Notification Canvas");
                loginCanvas.enabled = false;
                notificationCanvas.enabled = true;
            }
            else
            {
                Debug.Log("User login failed. Error #" + responseText);
            }
        }
    }

    public void VerifyInputs()
    {
        // Define the regex patterns for email and password validation
        string emailPattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$"; //email must include @
        string passwordPattern = @"^(?=.*[A-Z])(?=.*[!@#$%^&*]).{8,}$"; //PW must be 8 characters, capital letter, special character

        // Create regex objects with the patterns
        Regex emailRegex = new Regex(emailPattern);
        Regex passwordRegex = new Regex(passwordPattern);

        // Check if the email and password meet the minimum requirements and match the regex patterns
        bool isEmailValid = emailRegex.IsMatch(emailField.text);
        bool isPasswordValid = passwordRegex.IsMatch(passwordField.text);

        signInButton.interactable = isEmailValid && isPasswordValid;
    }

    //tab feature (update)
    #region
    public TMP_InputField[] inputFields;
    private int lastIndex = -1; // Initialize as -1 to indicate no field has been selected initially

    private void Update()
    {
        // Check for Tab key press
        if (Input.GetKeyDown(KeyCode.Tab) && inputFields.Length > 0)
        {
            int nextIndex = (lastIndex + 1) % inputFields.Length;
            inputFields[nextIndex].Select();
            lastIndex = nextIndex;
        }
    }
    #endregion
}
