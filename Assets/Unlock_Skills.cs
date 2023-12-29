using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Unlock_Skills : MonoBehaviour
{
    public SkillTree_Manager skillTree_Manager; //reference the SkillTree_Manager script so we can access the GameObject[] for the skill icons

    public TextMeshProUGUI partySkillPoints;
    int partySkillPointsInteger;

    public TextMeshProUGUI inventorySkillPoints;
    int inventorySkillPointsInteger;

    public TextMeshProUGUI fightingSkillPoints;
    int fightingSkillPointsInteger;

    public void UnlockSkills()
    {
        UnlockGreyLevelOne();
        UnlockBlueLevelOne();
        UnlockGreenLevelOne();
        UnlockRedLevelOne();
    }

    private void UnlockGreyLevelOne()
    {
        int.TryParse(inventorySkillPoints.text, out inventorySkillPointsInteger);

        if(inventorySkillPointsInteger >= 3)
        {
            foreach (GameObject skillIcon in skillTree_Manager.greySkillsLevelOne)
            {
                Button button = skillIcon.GetComponent<Button>();
                button.interactable = true;
            }
        }
    }

    private void UnlockBlueLevelOne()
    {
        int.TryParse(partySkillPoints.text, out partySkillPointsInteger);

        if(partySkillPointsInteger >= 3)
        {
            foreach(GameObject skillIcon in skillTree_Manager.blueSkillLevelOne)
            {
                Button button = skillIcon.GetComponent<Button>();
                button.interactable = true;
            }
        }
    }

    private void UnlockGreenLevelOne()
    {
        int.TryParse(partySkillPoints.text, out partySkillPointsInteger);

        if (partySkillPointsInteger >= 5)
        {
            foreach (GameObject skillIcon in skillTree_Manager.greenSkillLevelOne)
            {
                Button button = skillIcon.GetComponent<Button>();
                button.interactable = true;
            }
        }
    }

    private void UnlockRedLevelOne()
    {
        int.TryParse(fightingSkillPoints.text, out fightingSkillPointsInteger);

        if(fightingSkillPointsInteger >= 3)
        {
            foreach (GameObject skillIcon in skillTree_Manager.redSkillLevelOne)
            {
                Button button = skillIcon.GetComponent<Button>();
                button.interactable = true;
            }
        }
    }
}
