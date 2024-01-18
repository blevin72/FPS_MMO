using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

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
                DB_Manager.email = emailField.text;
<<<<<<< HEAD
                //DB_Manager.experience = int.Parse(responseText.Split('\t')[1]);
=======
>>>>>>> af324c0768c6974e98be73fe5f3f89bd0be906c9
                UnityEngine.SceneManagement.SceneManager.LoadScene(1);
                Debug.Log("User logged in. Email: " + DB_Manager.email);
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
        // Disable the register button if the username and password do not meet the minimum length requirements.
        signInButton.interactable = (emailField.text.Contains("@") && passwordField.text.Length >= 8);

        //I need to change the password requirements to more complex security
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
