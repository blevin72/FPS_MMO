using UnityEngine;
using System.Collections;

public class UIMainMenu : MonoBehaviour
{
    public GameManager gameManager;
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
    public GameObject compensationPanel;
    public GameObject buyPanel;
    public GameObject sellPanel;
    public GameObject barterPanel;
    public GameObject survivorPanel;
    public GameObject survivorSkillPanel;
    public GameObject missionAbilitiesPanel;
    public GameObject tabsPanel;
    public GameObject survivorRankingPanel;
    
#endregion

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>(); //ensures the GameManager script persists through the scene

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
        survivorPanel.active = false;
        survivorSkillPanel.active = false;
        missionAbilitiesPanel.active = false;
        tabsPanel.active = false;
        survivorRankingPanel.active = false;

    }

//Main Menu Canvas
#region
    public LoadSurvivor loadSurvivor; //referencing Survivor Ranking script for LoadSurvivorRanking()
    public EquippedGear equippedGear; //referencing Equipped Gear script for SaveEquippedGear()
    public GearBonuses gearBonuses; //referencing Gear Bonuses script for ApplyGearBonuses()

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
        StartCoroutine(LoadSurvivorAndApplyGearBonuses());
        titleMenuCanvas.enabled = false;
        survivorCanvas.enabled = true;
        tabsPanel.active = true;
        survivorRankingPanel.active = true;
        survivorPanel.active = true;
    }

    //Need this to ensure each Coroutine runs and finishes prior to the next one starting
    private IEnumerator LoadSurvivorAndApplyGearBonuses()
    {
        // Load survivor rankings and wait until it's done
        yield return StartCoroutine(loadSurvivor.LoadSurvivorRankings());

        // Save equipped gear and wait until it's done
        yield return StartCoroutine(equippedGear.SaveEquippedGear());

        // Set gear weight and wait until it's done
        yield return StartCoroutine(equippedGear.SetGearWeight());

        // Apply gear bonuses
        gearBonuses.ApplyAllBonuses();

    }
    #endregion

//Survivor Canvas
#region
    public void OnMainButtonClick()
    {
        survivorPanel.active = true;
        survivorSkillPanel.active = false;
    }

    public void OnStatsButtonClick()
    {
        survivorPanel.active = true;
        survivorSkillPanel.active = false;
    }

    public void OnSkillTreeButtonClick()
    {
        survivorSkillPanel.active = true;
        survivorPanel.active = false;
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
    public Chatroom_Manager chatroom_Manager; //reference Chatroom Manager script for RetrieveChats()

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
        StartCoroutine(chatroom_Manager.RetrieveChats()); //retrieve historical chats (general panel by default)

        conversePanel.active = true;
        //chat panels
        generalChatSV.active = true;
        missionsChatSV.active = false;
        eventsChatSV.active = false;
        tradeChatSV.active = false;

        //deactivate other Outpost Panels
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
        compensationPanel.active = false;
    }

    public void OnTradeButtonClick()
    {
        tradeCanvas.enabled = true;
        buyPanel.active = true;
        sellPanel.active = false;
        barterPanel.active = false;
        outpostCanvas.enabled = false;
    }

    //Converse Panel
    #region
    public void OnGeneralChatButtonClick()
    {
        generalChatSV.active = true;
        missionsChatSV.active = false;
        eventsChatSV.active = false;
        tradeChatSV.active = false;
        StartCoroutine(chatroom_Manager.RetrieveChats());
    }

    public void OnMissionsChatButtonClick()
    {
        missionsChatSV.active = true;
        generalChatSV.active = false;
        eventsChatSV.active = false;
        tradeChatSV.active = false;
        StartCoroutine(chatroom_Manager.RetrieveChats());
    }

    public void OnEventsChatButtonClick()
    {
        eventsChatSV.active = true;
        missionsChatSV.active = false;
        generalChatSV.active = false;
        tradeChatSV.active = false;
        StartCoroutine(chatroom_Manager.RetrieveChats());
    }

    public void OnTradeChatButtonClick()
    {
        tradeChatSV.active = true;
        generalChatSV.active = false;
        missionsChatSV.active = false;
        eventsChatSV.active = false;
        StartCoroutine(chatroom_Manager.RetrieveChats());
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

    //Radio Canvas
    #region
    public RequestSupport requestSupport; //refernce Request Support script for RetrieveAllDistressCalls()
    public CompensationForm compensationForm; //refernce Compensation Form script for FillCompensationForm()

    public void OnAllianceButtonClick()
    {
        alliancesPanel.active = true;
        supportPanel.active = false;
    }

    public void OnSupportButtonClick()
    {
        StartCoroutine(requestSupport.RetrieveAllDistressCalls());
        supportPanel.active = true;
        alliancesPanel.active = false;
    }

    public void OnRequestButtonClick()
    {
        requestPanel.active = true;
    }

    public void OnDetailsButtonClick()
    {
        StartCoroutine(compensationForm.FillCompensationForm());
        compensationPanel.active = true;
    }

    public void OnCompensationBackButtonClick()
    {
        compensationPanel.active = false;
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
    #endregion
}




