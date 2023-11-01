using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Canvas titleMenuCanvas;
    public Canvas settingsCanvas;
    public Canvas outpostCanvas;
    public GameObject audioPanel;
    public GameObject visualPanel;
    public GameObject gameplayPanel;
    public GameObject preferencesPanel;
    public GameObject socialPanel;
    public GameObject accountPanel;
    public GameObject accessPanel;
    public GameObject privacyPanel;
    public GameObject worldPanel;

    void Start()
    {
        //add false for each panel
        titleMenuCanvas.enabled = true;
        settingsCanvas.enabled = false;
        outpostCanvas.enabled = false;
        audioPanel.active = false;
        visualPanel.active = false;
        gameplayPanel.active = false;
        preferencesPanel.active = false;
        socialPanel.active = false;
        accountPanel.active = false;
        accessPanel.active = false;
        privacyPanel.active = false;
        worldPanel.active = false;
    }

//Main Menu
#region
    public void OnSettingsButtonClick()
    {
        titleMenuCanvas.enabled = false;
        settingsCanvas.enabled = true;
        audioPanel.active = true;
    }

    public void OnOutpostButtonClick()
    {
        titleMenuCanvas.enabled = false;
        outpostCanvas.enabled = true;
    }
    #endregion

//Settings Menu
#region
    public void OnReturnToMainButtonClick()
    {
        //add for each Panel
        settingsCanvas.enabled = false;
        titleMenuCanvas.enabled = true;
        visualPanel.active = false;
        audioPanel.active = false;
        gameplayPanel.active = false;
        preferencesPanel.active = false;
        socialPanel.active = false;
        accountPanel.active = false;
        accessPanel.active = false;
        privacyPanel.active = false;
        worldPanel.active = false;
    }

    public void OnAudioButtonClick()
    {
        //add for each panel 
        audioPanel.active = true;
        visualPanel.active = false;
        gameplayPanel.active = false;
        preferencesPanel.active = false;
        socialPanel.active = false;
        accountPanel.active = false;
        accessPanel.active = false;
        privacyPanel.active = false;
        worldPanel.active = false;
    }

    public void OnVisualButtonClick()
    {
        //add for each panel
        visualPanel.active = true;
        audioPanel.active = false;
        gameplayPanel.active = false;
        preferencesPanel.active = false;
        socialPanel.active = false;
        accountPanel.active = false;
        accessPanel.active = false;
        privacyPanel.active = false;
        worldPanel.active = false;
    }

    public void OnGameplayButtonClick()
    {
        //add for each panel
        gameplayPanel.active = true;
        audioPanel.active = false;
        visualPanel.active = false;
        preferencesPanel.active = false;
        socialPanel.active = false;
        accountPanel.active = false;
        accessPanel.active = false;
        privacyPanel.active = false;
        worldPanel.active = false;
    }

    public void OnPreferencesButtonClick()
    {
        preferencesPanel.active = true;
        audioPanel.active = false;
        visualPanel.active = false;
        gameplayPanel.active = false;
        socialPanel.active = false;
        accountPanel.active = false;
        accessPanel.active = false;
        privacyPanel.active = false;
    }

    public void OnSocialButtonClick()
    {
        socialPanel.active = true;
        audioPanel.active = false;
        visualPanel.active = false;
        gameplayPanel.active = false;
        preferencesPanel.active = false;
        accountPanel.active = false;
        accessPanel.active = false;
        privacyPanel.active = false;
        worldPanel.active = false;
    }

    public void OnAccountButtonClick()
    {
        accountPanel.active = true;
        socialPanel.active = false;
        audioPanel.active = false;
        visualPanel.active = false;
        gameplayPanel.active = false;
        preferencesPanel.active = false;
        accessPanel.active = false;
        privacyPanel.active = false;
        worldPanel.active = false;
    }

    public void OnAccessButtonClick()
    {
        accessPanel.active = true;
        accountPanel.active = false;
        socialPanel.active = false;
        audioPanel.active = false;
        visualPanel.active = false;
        gameplayPanel.active = false;
        preferencesPanel.active = false;
        privacyPanel.active = false;
        worldPanel.active = false;
    }

    public void OnPrivacyButtonClick()
    {
        privacyPanel.active = true;
        accessPanel.active = false;
        accountPanel.active = false;
        socialPanel.active = false;
        audioPanel.active = false;
        visualPanel.active = false;
        gameplayPanel.active = false;
        preferencesPanel.active = false;
        worldPanel.active = false;
    }

    public void OnWorldButtonClick()
    {
        worldPanel.active = true;
        privacyPanel.active = false;
        accessPanel.active = false;
        accountPanel.active = false;
        socialPanel.active = false;
        audioPanel.active = false;
        visualPanel.active = false;
        gameplayPanel.active = false;
        preferencesPanel.active = false;
    }
}
#endregion
