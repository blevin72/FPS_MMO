using UnityEngine;
using TMPro;

public class Skill_Level : MonoBehaviour
{
    public SkillTree_Manager skillTree_Manager; //reference SkillTree_Manager script
    public Unlock_Skills unlock_Skills; //reference Unlock_Skills script

    public void EnableSkillDescriptionPanel() //EnableSkillDescriptionPanel when player selects a skill icon
    {
        skillTree_Manager.skillDescriptionPanel.active = true;
    }




    public TextMeshProUGUI availablePoints; //how many skill points does the player have available from leveling up
    int skillPointsAvailable; //variable for when TMP object is parsed into an integer

    public TextMeshProUGUI currentLevel; //what is the current level shown in the skill description panel (Left Side)
    int currentLevelInteger; // "       "       "       "       "       "       "

    public TextMeshProUGUI nextLevel; //what is the next level if the player levels up that skill (Right Side)
    int nextLevelInteger;//     "       "       "       "       "       "       "

    public void LevelUpDescription()
    {
        if (int.TryParse(availablePoints.text, out skillPointsAvailable)) //convert TMP string to an integer
        {
            if (skillPointsAvailable > 0)
            {
                skillPointsAvailable--; //decrease points available
                availablePoints.text = skillPointsAvailable.ToString(); //convert integer back to TMP string

                int.TryParse(currentLevel.text, out currentLevelInteger);
                currentLevelInteger++;
                currentLevel.text = currentLevelInteger.ToString();

                int.TryParse(nextLevel.text, out nextLevelInteger);
                nextLevelInteger++;
                nextLevel.text = nextLevelInteger.ToString();

                LevelUpSkillTree(); //call the LevelUpSkillTree method
            }
        }
    }




    TextMeshProUGUI chosenSkill = null;
    int chosenSkillInteger;

    public void ChosenSkill(TextMeshProUGUI selectedSkill) //Assigned as a OnClick() to each SkillIcon Button (parameter is the TMP Child Object)
    {
        chosenSkill = selectedSkill; //assign TMP GameObject as the new value of chosenSkill variable (replacing null)
    }

    private void LevelUpSkillTree()
    {
        if (int.TryParse(chosenSkill.text, out chosenSkillInteger))//convert TMP string to an integer
        {
            chosenSkillInteger++;//increase skill points
            chosenSkill.text = chosenSkillInteger.ToString();//convert integer back to string
        }
    }




    public void AcceptButton()
    {
        skillTree_Manager.skillDescriptionPanel.active = false;
        unlock_Skills.UnlockSkills(); //run the UnlockSkills() from the unlock_skills script
    }


}
