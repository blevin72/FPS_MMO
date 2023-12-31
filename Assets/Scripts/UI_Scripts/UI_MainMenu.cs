using UnityEngine;

public class UIMainMenu : MonoBehaviour
{
//Canvas'
    public Canvas titleMenuCanvas;
    public Canvas settingsCanvas;
    public Canvas outpostCanvas;
    public Canvas tradeCanvas;
    public Canvas radioCanvas;
    public Canvas survivorCanvas;

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
    public GameObject outpostRankingPanel;
    public GameObject storagePanel;
    public GameObject votePanel;
    public GameObject conversePanel;
    public GameObject generalChatSV;
    public GameObject missionsChatSV;
    public GameObject eventsChatSV;
    public GameObject tradeChatSV;
    public GameObject territoryPanel;
    public GameObject mapPanel;
    public GameObject requestsPanel;
    public GameObject alliancesPanel;
    public GameObject supportPanel;
    public GameObject requestPanel;
    public GameObject buyPanel;
    public GameObject sellPanel;
    public GameObject barterPanel;
    public GameObject survivorMainPanel;
    public GameObject survivorStatsPanel;
    public GameObject survivorSkillPanel;
    public GameObject missionAbilitiesPanel;
    public GameObject tabsPanel;
    public GameObject survivorRankingPanel;
    
#endregion

    void Start()
    {
        //add false for each panel
        titleMenuCanvas.enabled = true;
        settingsCanvas.enabled = false;
        outpostCanvas.enabled = false;
        tradeCanvas.enabled = false;
        radioCanvas.enabled = false;
        audioPanel.active = false;
        visualPanel.active = false;
        gameplayPanel.active = false;
        preferencesPanel.active = false;
        socialPanel.active = false;
        accountPanel.active = false;
        accessPanel.active = false;
        privacyPanel.active = false;
        worldPanel.active = false;
        outpostRankingPanel.active = false;
        storagePanel.active = false;
        votePanel.active = false;
        conversePanel.active = false;
        generalChatSV.active = false;
        missionsChatSV.active = false;
        eventsChatSV.active = false;
        tradeChatSV.active = false;
        territoryPanel.active = false;
        mapPanel.active = false;
        requestsPanel.active = false;
        alliancesPanel.active = false;
        supportPanel.active = false;
        requestPanel.active = false;
        buyPanel.active = false;
        sellPanel.active = false;
        barterPanel.active = false;
        survivorMainPanel.active = false;
        survivorStatsPanel.active = false;
        survivorSkillPanel.active = false;
        missionAbilitiesPanel.active = false;
        tabsPanel.active = false;
        survivorRankingPanel.active = false;

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
        outpostRankingPanel.active = true;
    }

    public void OnSurvivorButtonClick()
    {
        titleMenuCanvas.enabled = false;
        survivorCanvas.enabled = true;
        tabsPanel.active = true;
        survivorRankingPanel.active = true;
        survivorMainPanel.active = true;
    }
    #endregion

//Survivor Canvas
#region
    public void OnMainButtonClick()
    {
        survivorMainPanel.active = true;
        survivorStatsPanel.active = false;
        survivorSkillPanel.active = false;
    }

    public void OnStatsButtonClick()
    {
        survivorStatsPanel.active = true;
        survivorMainPanel.active = false;
        survivorSkillPanel.active = false;
    }

    public void OnSkillTreeButtonClick()
    {
        survivorSkillPanel.active = true;
        survivorStatsPanel.active = false;
        survivorMainPanel.active = false;
    }

    public void OnMissionAbilityButtonClick()
    {
        missionAbilitiesPanel.active = true;
    }

    public void OnCancelMissionAbilityButtonClick()
    {
        missionAbilitiesPanel.active = false;
    }
#endregion

//Settings Canvas
#region
    public void OnReturnToMainButtonClick()
    {
        //title menu enabled, all other canvas' diable
        titleMenuCanvas.enabled = true;
        settingsCanvas.enabled = false;
        outpostCanvas.enabled = false;
        radioCanvas.enabled = false;
        tradeCanvas.enabled = false;
        //Settings Canvas - disable panels
        visualPanel.active = false;
        audioPanel.active = false;
        gameplayPanel.active = false;
        preferencesPanel.active = false;
        socialPanel.active = false;
        accountPanel.active = false;
        accessPanel.active = false;
        privacyPanel.active = false;
        worldPanel.active = false;
        //Outpost Canvas - disable panels
        outpostRankingPanel.active = false;
        storagePanel.active = false;
        votePanel.active = false;
        conversePanel.active = false;
        territoryPanel.active = false;
        //Trade Outpost - disable panels
        buyPanel.active = false;
        sellPanel.active = false;
        barterPanel.active = false;
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
        mapPanel.active = false;
        requestsPanel.active = false;
        alliancesPanel.active = false;
    }

    public void OnVoteButtonClick()
    {
        votePanel.active = true;
        storagePanel.active = false;
        conversePanel.active = false;
        territoryPanel.active = false;
        mapPanel.active = false;
        requestsPanel.active = false;
        alliancesPanel.active = false;
    }

    public void OnConverseButtonClick()
    {
        conversePanel.active = true;
        //panels within the converse panel
        generalChatSV.active = true;
        missionsChatSV.active = false;
        eventsChatSV.active = false;
        tradeChatSV.active = false;

        votePanel.active = false;
        storagePanel.active = false;
        territoryPanel.active = false;
        mapPanel.active = false;
        requestsPanel.active = false;
        alliancesPanel.active = false;
    }

    public void OnTerritoryButtonClick()
    {
        territoryPanel.active = true;
        mapPanel.active = true;
        conversePanel.active = false;
        votePanel.active = false;
        storagePanel.active = false;
        requestsPanel.active = false;
        alliancesPanel.active = false;
    }

    public void OnRadioButtonClick()
    {
        radioCanvas.enabled = true;
        outpostCanvas.enabled = false;
        alliancesPanel.active = true;
        requestsPanel.active = false;
        supportPanel.active = false;
    }

    public void OnTradeButtonClick()
    {
        tradeCanvas.enabled = true;
        buyPanel.active = true;
        sellPanel.active = false;
        barterPanel.active = false;
        outpostCanvas.enabled = false;
    }
    #endregion

//Trade Canvas
#region
    public void OnBuyButtonClick()
    {
        buyPanel.active = true;
        sellPanel.active = false;
        barterPanel.active = false;
    }

    public void OnSellButtonClick()
    {
        sellPanel.active = true;
        buyPanel.active = false;
        barterPanel.active = false;
    }

    public void OnBarterButtonClick()
    {
        barterPanel.active = true;
        buyPanel.active = false;
        sellPanel.active = false;
    }

    public void OnTradeToOutpostButtonClick() //back button on trade canvas sending you back to the outpost canvas
    {
        outpostCanvas.enabled = true;
        tradeCanvas.enabled = false;
        buyPanel.active = false;
        sellPanel.active = false;
        barterPanel.active = false;
    }
    #endregion

//Converse Panel
#region
    public void OnGeneralChatButtonClick()
    {
        generalChatSV.active = true;
        missionsChatSV.active = false;
        eventsChatSV.active = false;
        tradeChatSV.active = false;
    }

    public void OnMissionsChatButtonClick()
    {
        missionsChatSV.active = true;
        generalChatSV.active = false;
        eventsChatSV.active = false;
        tradeChatSV.active = false;
    }

    public void OnEventsChatButtonClick()
    {
        eventsChatSV.active = true;
        missionsChatSV.active = false;
        generalChatSV.active = false;
        tradeChatSV.active = false;
    }

    public void OnTradeChatButtonClick()
    {
        tradeChatSV.active = true;
        generalChatSV.active = true;
        missionsChatSV.active = true;
        eventsChatSV.active = true;

    }
    #endregion

//Radio Canvas
#region
    public void OnAllianceButtonClick()
    {
        alliancesPanel.active = true;
        supportPanel.active = false;
    }

    public void OnSupportButtonClick()
    {
        supportPanel.active = true;
        alliancesPanel.active = false;
    }

    public void OnRequestButtonClick()
    {
        requestPanel.active = true;
    }

    public void OnRequestToRadioButtonClick()
    {
        requestPanel.active = false;
    }

    public void OnRadioToOutpostButtonClick()
    {
        outpostCanvas.enabled = true;
        radioCanvas.enabled = false;
        supportPanel.active = false;
        alliancesPanel.active = false;
    }
#endregion
}




