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
    }

    public void OnAudioButtonClick()
    {
        //add for each panel 
        audioPanel.active = true;
        visualPanel.active = false;
        gameplayPanel.active = false;
        preferencesPanel.active = false;
    }

    public void OnVisualButtonClick()
    {
        //add for each panel
        visualPanel.active = true;
        audioPanel.active = false;
        gameplayPanel.active = false;
        preferencesPanel.active = false;
    }

    public void OnGameplayButtonClick()
    {
        //add for each panel
        gameplayPanel.active = true;
        audioPanel.active = false;
        visualPanel.active = false;
        preferencesPanel.active = false;
    }

    public void OnPreferencesButtonClick()
    {
        preferencesPanel.active = true;
        audioPanel.active = false;
        visualPanel.active = false;
        gameplayPanel.active = false;
    }
}
#endregion
