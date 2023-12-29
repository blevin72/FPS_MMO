using UnityEngine;
using TMPro;


public class Skill_Level : MonoBehaviour
{
    public SkillTree_Manager skillTree_Manager; //reference SkillTree_Manager script

    //Referencing GameObjects/arrays from SkillTree_Manager script
    //public void Start()
    //{
    //    //GetGameObjects();
    //    //AccessTMPObjects();
    //}

    public void EnableSkillDescriptionPanel()
    {
        skillTree_Manager.skillDescriptionPanel.active = true;

       // skillTree_Manager.skillDescriptionPanel.GetComponentInChildren<TextMeshProUGUI>().text = partySkillTMP.text;
    }

    public enum SkillType
    {
        PartySkill
    }

    //Level Up Method
    #region
    public TextMeshProUGUI availablePoints;
    public TextMeshProUGUI currentLevel;
    public TextMeshProUGUI nextLevel;
    public TextMeshProUGUI[] skillLevelTMP;

    int skillPointsAvailable;
    int currentLevelInteger;
    int nextLevelInteger;

    public void LevelUp()
    {
        if(int.TryParse(availablePoints.text, out skillPointsAvailable)) //convert TMP string to an integer
        {
            if(skillPointsAvailable > 0)
            {
                skillPointsAvailable--; //decrease points available
                availablePoints.text = skillPointsAvailable.ToString(); //convert integer back to TMP string

                int.TryParse(currentLevel.text, out currentLevelInteger);
                currentLevelInteger++;
                currentLevel.text = currentLevelInteger.ToString();

                int.TryParse(nextLevel.text, out nextLevelInteger);
                nextLevelInteger++;
                nextLevel.text = nextLevelInteger.ToString();

                foreach(var skillLevelTMP in skillLevelTMP)
                {
                    int.TryParse(skillLevelTMP.text, out int skillLevel);

                    switch (GetSkillType(skillLevelTMP.gameObject.name))
                    {
                        case SkillType.PartySkill:
                            skillLevel++;
                            skillLevelTMP.text = skillLevel.ToString();
                            break;
                    }
                }
            }
        }
    }
    #endregion

    private SkillType GetSkillType(string skillName)
    {
        switch (skillName)
        {
            case "PartySkillTMP":
                return SkillType.PartySkill;
            // Add cases for other skill types if needed
            default:
                return default; // Handle default case or return other enum value
        }
    }

    public void AcceptButton()
    {
        skillTree_Manager.skillDescriptionPanel.active = false;
    }

    //public void AccessTMPObjects() //accessing the TMP component inside the child GameObjects of the Skill Icons Game Objects
    //{
    //    TextMeshProUGUI partySkillTMP = skillTree_Manager.partySkillIcon.GetComponentInChildren<TextMeshProUGUI>();
    //    TextMeshProUGUI inventorySkillTMP = skillTree_Manager.inventorySkillIcon.GetComponentInChildren<TextMeshProUGUI>();
    //    TextMeshProUGUI fightingSkillTMP = skillTree_Manager.fightingSkillIcon.GetComponentInChildren<TextMeshProUGUI>();
    //}

    //private void GetGameObjects()
    //{
    //    GameObject skillDescriptionPanel = skillTree_Manager.skillDescriptionPanel;

    //    GameObject partySkillIcon = skillTree_Manager.partySkillIcon;
    //    GameObject inventorySkillIcon = skillTree_Manager.inventorySkillIcon;
    //    GameObject fightingSkillIcon = skillTree_Manager.fightingSkillIcon;

    //    GameObject[] greySkillsLevelOne = skillTree_Manager.greySkillsLevelOne;
    //    GameObject[] greySkillsLevelTwo = skillTree_Manager.greySkillLevelTwo;
    //    GameObject[] blueSkillsLevelOne = skillTree_Manager.blueSkillLevelOne;
    //    GameObject[] blueSkillsLevelTwo = skillTree_Manager.blueSkillLevelTwo;
    //    GameObject[] blueSkillsLevelThree = skillTree_Manager.blueSkillLevelThree;
    //    GameObject[] blueSkillsLevelFour = skillTree_Manager.blueSkillLevelFour;
    //    GameObject[] greenSkillsLevelOne = skillTree_Manager.greenSkillLevelOne;
    //    GameObject[] greenSkillsLevelTwo = skillTree_Manager.greenSkillLevelTwo;
    //    GameObject[] greenSkillsLevelThree = skillTree_Manager.greenSkillLevelThree;
    //    GameObject[] greenSkillsLevelFour = skillTree_Manager.greenSkillLevelFour;
    //    GameObject[] redSkillsLevelOne = skillTree_Manager.redSkillLevelOne;
    //    GameObject[] redSkillsLevelTwo = skillTree_Manager.redSkillLevelTwo;
    //    GameObject[] redSkillsLevelThree = skillTree_Manager.redSkillLevelThree;
    //    GameObject[] redSkillsLevelFour = skillTree_Manager.redSkillLevelFour;
    //    GameObject[] pupleSkillsLevelOne = skillTree_Manager.purpleSkillLevelOne;
    //    GameObject[] purpleSkillsLevelTwo = skillTree_Manager.purpleSkillLevelTwo;
    //    GameObject[] purpleSkillsLevelThree = skillTree_Manager.purpleSkillLevelThree;
    //    GameObject[] tealSkillsLevelOne = skillTree_Manager.tealSkillLevelOne;
    //    GameObject[] tealSkillsLevelTwo = skillTree_Manager.tealSkillLevelTwo;
    //    GameObject[] tealSkillsLevelThree = skillTree_Manager.tealSkillLevelThree;
    //}
}
