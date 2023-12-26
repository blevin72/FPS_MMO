using UnityEngine;
using UnityEngine.UI;
public class SkillTree_Manager : MonoBehaviour
{
    //Initilization
    #region
    public GameObject partySkillIcon;
    public GameObject inventorySkillIcon;
    public GameObject fightingSkillIcon;

    public GameObject[] greySkillsLevelOne;
    public GameObject[] greySkillLevelTwo;

    public GameObject[] blueSkillLevelOne;
    public GameObject[] blueSkillLevelTwo;
    public GameObject[] blueSkillLevelThree;
    public GameObject[] blueSkillLevelFour;

    public GameObject[] greenSkillLevelOne;
    public GameObject[] greenSkillLevelTwo;
    public GameObject[] greenSkillLevelThree;
    public GameObject[] greenSkillLevelFour;

    public GameObject[] redSkillLevelOne;
    public GameObject[] redSkillLevelTwo;
    public GameObject[] redSkillLevelThree;
    public GameObject[] redSkillLevelFour;

    public GameObject[] purpleSkillLevelOne;
    public GameObject[] purpleSkillLevelTwo;
    public GameObject[] purpleSkillLevelThree;
   
    public GameObject[] tealSkillLevelOne;
    public GameObject[] tealSkillLevelTwo;
    public GameObject[] tealSkillLevelThree;

    public GameObject skillDescriptionPanel;
    
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        FilterDisableSkills();
        DisableSkillsDescriptionPanel();
    }

    public void DisableSkills(GameObject[] skillsArray)
    {
        foreach (GameObject skillsIcon in skillsArray)
        {
            Button button = skillsIcon.GetComponent<Button>();
            button.interactable = false;
        }
    }

    public void FilterDisableSkills()
    {
        DisableSkills(greySkillsLevelOne);
        DisableSkills(greySkillLevelTwo);
        DisableSkills(blueSkillLevelOne);
        DisableSkills(blueSkillLevelTwo);
        DisableSkills(blueSkillLevelThree);
        DisableSkills(blueSkillLevelFour);
        DisableSkills(greenSkillLevelOne);
        DisableSkills(greenSkillLevelTwo);
        DisableSkills(greenSkillLevelThree);
        DisableSkills(greenSkillLevelFour);
        DisableSkills(redSkillLevelOne);
        DisableSkills(redSkillLevelTwo);
        DisableSkills(redSkillLevelThree);
        DisableSkills(redSkillLevelFour);
        DisableSkills(purpleSkillLevelOne);
        DisableSkills(purpleSkillLevelTwo);
        DisableSkills(purpleSkillLevelThree);
        DisableSkills(tealSkillLevelOne);
        DisableSkills(tealSkillLevelTwo);
        DisableSkills(tealSkillLevelThree);
    }

    public void DisableSkillsDescriptionPanel()
    {
        skillDescriptionPanel.active = false;
    }
}
