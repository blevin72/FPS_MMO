using UnityEngine;

public class UIMainMenu : MonoBehaviour
{
    public Canvas titleMenuCanvas;
    public Canvas settingsCanvas;
    public Canvas outpostCanvas;
    public Canvas tradeCanvas;


//Panels
#region
    public GameObject audioPanel;
    public GameObject visualPanel;
    public GameObject gameplayPanel;
    public GameObject preferencesPanel;
    public GameObject socialPanel;
    public GameObject accountPanel;
    public GameObject accessPanel;
    public GameObject privacyPanel;
    public GameObject worldPanel;
    public GameObject storagePanel;
    public GameObject votePanel;
    public GameObject conversePanel;
    public GameObject territoryPanel;
    public GameObject requestsPanel;
    public GameObject alliancesPanel;
#endregion

    void Start()
    {
        //add false for each panel
        titleMenuCanvas.enabled = true;
        settingsCanvas.enabled = false;
        outpostCanvas.enabled = false;
        tradeCanvas.enabled = false;
        audioPanel.active = false;
        visualPanel.active = false;
        gameplayPanel.active = false;
        preferencesPanel.active = false;
        socialPanel.active = false;
        accountPanel.active = false;
        accessPanel.active = false;
        privacyPanel.active = false;
        worldPanel.active = false;
        storagePanel.active = false;
        votePanel.active = false;
        conversePanel.active = false;
        territoryPanel.active = false;
        requestsPanel.active = false;
        alliancesPanel.active = false;
    }

//Main Menu Canvas
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
        storagePanel.active = true;
    }
    #endregion

//Settings Canvas
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
    #endregion

//Outpost Canvas
#region
    public void OnStorageButtonClick()
    {
        storagePanel.active = true;
        votePanel.active = false;
        conversePanel.active = false;
        territoryPanel.active = false;
        requestsPanel.active = false;
        alliancesPanel.active = false;
    }

    public void OnVoteButtonClick()
    {
        votePanel.active = true;
        storagePanel.active = false;
        conversePanel.active = false;
        territoryPanel.active = false;
        requestsPanel.active = false;
        alliancesPanel.active = false;
    }

    public void OnConverseButtonClick()
    {
        conversePanel.active = true;
        votePanel.active = false;
        storagePanel.active = false;
        territoryPanel.active = false;
        requestsPanel.active = false;
        alliancesPanel.active = false;
    }

    public void OnTerritoryButtonClick()
    {
        territoryPanel.active = true;
        conversePanel.active = false;
        votePanel.active = false;
        storagePanel.active = false;
        requestsPanel.active = false;
        alliancesPanel.active = false;
    }

    public void OnTradeButtonClick()
    {
        tradeCanvas.enabled = true;
        outpostCanvas.enabled = false;
    }

    public void OnRequestsButtonClick()
    {
        requestsPanel.active = true;
        territoryPanel.active = false;
        conversePanel.active = false;
        votePanel.active = false;
        storagePanel.active = false;
        alliancesPanel.active = false;
    }

    public void OnAlliancesButtonClick()
    {
        alliancesPanel.active = true;
        requestsPanel.active = false;
        territoryPanel.active = false;
        conversePanel.active = false;
        votePanel.active = false;
        storagePanel.active = false;
    }
    #endregion

//Trade Canvas
#region

#endregion
}




