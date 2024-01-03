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

    public TextMeshProUGUI[] greyLevelOnePoints;
    public TextMeshProUGUI[] greyLevelTwoPoints;
    public TextMeshProUGUI[] blueLevelOnePoints;
    public TextMeshProUGUI[] blueLevelTwoPoints;
    public TextMeshProUGUI[] blueLevelThreePoints;
    public TextMeshProUGUI[] greenLevelOnePoints;
    public TextMeshProUGUI[] greenLevelTwoPoints;
    public TextMeshProUGUI[] greenLevelThreePoints;
    public TextMeshProUGUI[] redLevelOnePoints;
    public TextMeshProUGUI[] redLevelTwoPoints;
    public TextMeshProUGUI[] redLevelThreePoints;
    public TextMeshProUGUI[] purpleLevelOnePoints;
    public TextMeshProUGUI[] purpleLevelTwoPoints;
    public TextMeshProUGUI[] tealLevelOnePoints;
    public TextMeshProUGUI[] tealLevelTwoPoints;

    public void UnlockSkills()
    {
        UnlockGreyLevelOne();
        UnlockGreyLevelTwo();
        UnlockBlueLevelOne();
        UnlockBlueLevelTwo();
        UnlockBlueLevelThree();
        UnlockBlueLevelFour();
        UnlockGreenLevelOne();
        UnlockGreenLevelTwo();
        UnlockGreenLevelThree();
        UnlockGreenLevelFour();
        UnlockRedLevelOne();
        UnlockRedLevelTwo();
        UnlockRedLevelThree();
        UnlockRedLevelFour();
        UnlockPurpleAndTealLevelOne();
        UnlockPurpleLevelTwo();
        UnlockPurpleLevelThree();
        UnlockTealLevelTwo();
        UnlockTealLevelThree();
    }
    //unlock grey skills methods
    #region
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

    private void UnlockGreyLevelTwo()
    {
        int totalGreyPoints = 0;

        foreach (TextMeshProUGUI skill in greyLevelOnePoints)
        {
            int greySkillPoints;
            if (int.TryParse(skill.text, out greySkillPoints))
            {
                totalGreyPoints += greySkillPoints;
            }
        }
            if(totalGreyPoints >= 3)
            {
                foreach (GameObject skillIcon in skillTree_Manager.greySkillLevelTwo)
                {
                    Button button = skillIcon.GetComponent<Button>();
                    button.interactable = true;
                }
            }
        
    }
    #endregion

    //unlock purple skills methods
    #region
    private void UnlockPurpleAndTealLevelOne()
    {
        int.TryParse(inventorySkillPoints.text, out inventorySkillPointsInteger);

        if (inventorySkillPointsInteger >= 5)
        {
            foreach (GameObject skillIcon in skillTree_Manager.purpleSkillLevelOne)
            {
                Button button = skillIcon.GetComponent<Button>();
                button.interactable = true;
            }

            foreach (GameObject skillIcon in skillTree_Manager.tealSkillLevelOne)
            {
                Button button = skillIcon.GetComponent<Button>();
                button.interactable = true;
            }
        }
    }

    private void UnlockPurpleLevelTwo()
    {
        int totalLevelOnePurplePoints = 0;

        foreach (TextMeshProUGUI skill in purpleLevelOnePoints)
        {
            int purpleLevelOnePoints;
            if (int.TryParse(skill.text, out purpleLevelOnePoints))
            {
                totalLevelOnePurplePoints += purpleLevelOnePoints;
            }
        }

        if (totalLevelOnePurplePoints >= 5)
        {
            foreach (GameObject skillIcon in skillTree_Manager.purpleSkillLevelTwo)
            {
                Button button = skillIcon.GetComponent<Button>();
                button.interactable = true;
            }
        }
    }

    private void UnlockPurpleLevelThree()
    {
        int totalLevelTwoPurplePoints = 0;

        foreach (TextMeshProUGUI skill in purpleLevelTwoPoints)
        {
            int purpleLevelTwoPoints;
            if (int.TryParse(skill.text, out purpleLevelTwoPoints))
            {
                totalLevelTwoPurplePoints += purpleLevelTwoPoints;
            }
        }

        if (totalLevelTwoPurplePoints >= 3)
        {
            foreach (GameObject skillIcon in skillTree_Manager.purpleSkillLevelThree)
            {
                Button button = skillIcon.GetComponent<Button>();
                button.interactable = true;
            }
        }
    }
    #endregion

    private void UnlockTealLevelTwo()
    {
        int totalLevelOneTealPoints = 0;

        foreach (TextMeshProUGUI skill in tealLevelOnePoints)
        {
            int tealLevelOnePoints;
            if (int.TryParse(skill.text, out tealLevelOnePoints))
            {
                totalLevelOneTealPoints += tealLevelOnePoints;
            }
        }

        if (totalLevelOneTealPoints >= 5)
        {
            foreach (GameObject skillIcon in skillTree_Manager.tealSkillLevelTwo)
            {
                Button button = skillIcon.GetComponent<Button>();
                button.interactable = true;
            }
        }
    }

    private void UnlockTealLevelThree()
    {
        int totalLevelTwoTealPoints = 0;

        foreach (TextMeshProUGUI skill in tealLevelTwoPoints)
        {
            int tealLevelTwoPoints;
            if (int.TryParse(skill.text, out tealLevelTwoPoints))
            {
                totalLevelTwoTealPoints += tealLevelTwoPoints;
            }
        }

        if (totalLevelTwoTealPoints >= 3)
        {
            foreach (GameObject skillIcon in skillTree_Manager.tealSkillLevelThree)
            {
                Button button = skillIcon.GetComponent<Button>();
                button.interactable = true;
            }
        }
    }

    //unlock blue skills methods
    #region
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

    private void UnlockBlueLevelTwo()
    {
        int totalLevelOneBluePoints = 0;

        foreach(TextMeshProUGUI skill in blueLevelOnePoints)
        {
            int blueLevelOnePoints;
            if(int.TryParse(skill.text, out blueLevelOnePoints))
            {
                totalLevelOneBluePoints += blueLevelOnePoints;
            }
        }

        if(totalLevelOneBluePoints >= 5)
        {
            foreach(GameObject skillIcon in skillTree_Manager.blueSkillLevelTwo)
            {
                Button button = skillIcon.GetComponent<Button>();
                button.interactable = true;
            }
        }
    }

    private void UnlockBlueLevelThree()
    {
        int totalLevelTwoBluePoints = 0;

        foreach (TextMeshProUGUI skill in blueLevelTwoPoints)
        {
            int blueLevelTwoPoints;
            if (int.TryParse(skill.text, out blueLevelTwoPoints))
            {
                totalLevelTwoBluePoints += blueLevelTwoPoints;
            }
        }

        if (totalLevelTwoBluePoints >= 5)
        {
            foreach (GameObject skillIcon in skillTree_Manager.blueSkillLevelThree)
            {
                Button button = skillIcon.GetComponent<Button>();
                button.interactable = true;
            }
        }
    }

    private void UnlockBlueLevelFour()
    {
        int totalLevelThreeBluePoints = 0;

        foreach (TextMeshProUGUI skill in blueLevelThreePoints)
        {
            int blueLevelThreePoints;
            if (int.TryParse(skill.text, out blueLevelThreePoints))
            {
                totalLevelThreeBluePoints += blueLevelThreePoints;
            }
        }

        if (totalLevelThreeBluePoints >= 5)
        {
            foreach (GameObject skillIcon in skillTree_Manager.blueSkillLevelFour)
            {
                Button button = skillIcon.GetComponent<Button>();
                button.interactable = true;
            }
        }
    }
    #endregion

    //unlock green skills methods
    #region
    private void UnlockGreenLevelOne()
    {
        int.TryParse(partySkillPoints.text, out partySkillPointsInteger);

        if (partySkillPointsInteger >= 3)
        {
            foreach (GameObject skillIcon in skillTree_Manager.greenSkillLevelOne)
            {
                Button button = skillIcon.GetComponent<Button>();
                button.interactable = true;
            }
        }
    }

    private void UnlockGreenLevelTwo()
    {
        int totalLevelOneGreenPoints = 0;

        foreach (TextMeshProUGUI skill in greenLevelOnePoints)
        {
            int greenLevelOnePoints;
            if (int.TryParse(skill.text, out greenLevelOnePoints))
            {
                totalLevelOneGreenPoints += greenLevelOnePoints;
            }
        }

        if (totalLevelOneGreenPoints >= 5)
        {
            foreach (GameObject skillIcon in skillTree_Manager.greenSkillLevelTwo)
            {
                Button button = skillIcon.GetComponent<Button>();
                button.interactable = true;
            }
        }
    }

    private void UnlockGreenLevelThree()
    {
        int totalLevelTwoGreenPoints = 0;

        foreach (TextMeshProUGUI skill in greenLevelTwoPoints)
        {
            int greenLevelTwoPoints;
            if (int.TryParse(skill.text, out greenLevelTwoPoints))
            {
                totalLevelTwoGreenPoints += greenLevelTwoPoints;
            }
        }

        if (totalLevelTwoGreenPoints >= 3)
        {
            foreach (GameObject skillIcon in skillTree_Manager.greenSkillLevelThree)
            {
                Button button = skillIcon.GetComponent<Button>();
                button.interactable = true;
            }
        }
    }

    private void UnlockGreenLevelFour()
    {
        int totalLevelThreeGreenPoints = 0;

        foreach (TextMeshProUGUI skill in greenLevelThreePoints)
        {
            int greenLevelThreePoints;
            if (int.TryParse(skill.text, out greenLevelThreePoints))
            {
                totalLevelThreeGreenPoints += greenLevelThreePoints;
            }
        }

        if (totalLevelThreeGreenPoints >= 5)
        {
            foreach (GameObject skillIcon in skillTree_Manager.greenSkillLevelFour)
            {
                Button button = skillIcon.GetComponent<Button>();
                button.interactable = true;
            }
        }
    }
    #endregion

    //unlock red skills methods
    #region
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

    private void UnlockRedLevelTwo()
    {
        int totalLevelOneRedPoints = 0;

        foreach (TextMeshProUGUI skill in redLevelOnePoints)
        {
            int redLevelOnePoints;
            if (int.TryParse(skill.text, out redLevelOnePoints))
            {
                totalLevelOneRedPoints += redLevelOnePoints;
            }
        }

        if (totalLevelOneRedPoints >= 5)
        {
            foreach (GameObject skillIcon in skillTree_Manager.redSkillLevelTwo)
            {
                Button button = skillIcon.GetComponent<Button>();
                button.interactable = true;
            }
        }
    }

    private void UnlockRedLevelThree()
    {
        int totalLevelTwoRedPoints = 0;

        foreach (TextMeshProUGUI skill in redLevelTwoPoints)
        {
            int redLevelTwoPoints;
            if (int.TryParse(skill.text, out redLevelTwoPoints))
            {
                totalLevelTwoRedPoints += redLevelTwoPoints;
            }
        }

        if (totalLevelTwoRedPoints >= 7)
        {
            foreach (GameObject skillIcon in skillTree_Manager.redSkillLevelThree)
            {
                Button button = skillIcon.GetComponent<Button>();
                button.interactable = true;
            }
        }
    }

    private void UnlockRedLevelFour()
    {
        int totalLevelThreeRedPoints = 0;

        foreach (TextMeshProUGUI skill in redLevelThreePoints)
        {
            int redLevelThreePoints;
            if (int.TryParse(skill.text, out redLevelThreePoints))
            {
                totalLevelThreeRedPoints += redLevelThreePoints;
            }
        }

        if (totalLevelThreeRedPoints >= 3)
        {
            foreach (GameObject skillIcon in skillTree_Manager.redSkillLevelFour)
            {
                Button button = skillIcon.GetComponent<Button>();
                button.interactable = true;
            }
        }
    }
    #endregion


}
