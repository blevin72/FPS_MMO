using UnityEngine;
using TMPro;

public class SurvivorStats : MonoBehaviour
{
    public GameObject strengthStats;
    public GameObject strengthAdvStats;
    public GameObject dexterityStats;
    public GameObject dexterityAdvStats;
    public GameObject intelectStats;
    public GameObject intelectAdvStats;
    public GameObject enduranceStats;
    public GameObject enduranceAdvStats;
    public GameObject charmStats;
    public GameObject charmAdvStats;
    public GameObject stealthStats;
    public GameObject stealthAdvStats;

    //set all stats to active and all advanced stats to inactive
    void Start()
    {
        strengthStats.active = true;
        strengthAdvStats.active = false;
        dexterityStats.active = true;
        dexterityAdvStats.active = false;
        intelectStats.active = true;
        intelectAdvStats.active = false;
        enduranceStats.active = true;
        enduranceAdvStats.active = false;
        charmStats.active = true;
        charmAdvStats.active = false;
        stealthStats.active = true;
        stealthAdvStats.active = false;

        SetAdvStrengthStats();
        SetAdvDexterityStats();
        SetAdvIntellectStats();
        SetAdvEnduranceStats();
        SetAdvCharmStats();
        SetAdvStealthStats();
    }

    //toggle between Stats & Adv Stats methods
    #region
    //toggle strength stats
    public void StrengthIconClick()
    {
        if (strengthStats.active == true)
        {
            strengthStats.active = false;
            strengthAdvStats.active = true;
        }
        else
        {
            strengthStats.active = true;
            strengthAdvStats.active = false;
        }
    }

    //toggle dexterity stats
    public void DexterityIconClick()
    {
        if (dexterityStats.active == true)
        {
            dexterityStats.active = false;
            dexterityAdvStats.active = true;
        }
        else
        {
            dexterityStats.active = true;
            dexterityAdvStats.active = false;
        }
    }

    //toggle intelect stats
    public void IntelectIconClick()
    {
        if (intelectStats.active == true)
        {
            intelectStats.active = false;
            intelectAdvStats.active = true;
        }
        else
        {
            intelectStats.active = true;
            intelectAdvStats.active = false;
        }
    }

    //toggle endurance stats
    public void EnduranceIconClick()
    {
        if (enduranceStats.active == true)
        {
            enduranceStats.active = false;
            enduranceAdvStats.active = true;
        }
        else
        {
            enduranceStats.active = true;
            enduranceAdvStats.active = false;
        }
    }

    //toggle charm stats
    public void CharmIconClick()
    {
        if (charmStats.active == true)
        {
            charmStats.active = false;
            charmAdvStats.active = true;
        }
        else
        {
            charmStats.active = true;
            charmAdvStats.active = false;
        }
    }

    //toggle stealth stats
    public void StealthIconClick()
    {
        if (stealthStats.active == true)
        {
            stealthStats.active = false;
            stealthAdvStats.active = true;
        }
        else
        {
            stealthStats.active = true;
            stealthAdvStats.active = false;
        }
    }
    #endregion

//add & subtract stat points when leveling up
#region
    public TextMeshProUGUI[] statValuesTextStatsPanel; // Array of text fields for displaying stats (Stats Panel)
    public TextMeshProUGUI[] statValuesTextMainPanel;  // Array of text fields for dispalying stats (Main Panel)
    public TextMeshProUGUI availablePoints;
    public int[] characterStats = new int[6];
    private bool pointsLocked = false;

    public void DistributeStatPoints(int statIndex)
    {
        if (!pointsLocked && availablePoints != null && statIndex >= 0 && statIndex < characterStats.Length)
        {
            int statPointsValue;
            if (int.TryParse(availablePoints.text, out statPointsValue))
            {
                if (statPointsValue > 0)
                {
                    characterStats[statIndex]++; // Increase the chosen stat
                    statPointsValue--; // Decrease available stat points
                    availablePoints.text = statPointsValue.ToString(); // Update UI
                    UpdateStatUI(statIndex); // Update UI display for the chosen stat
                    SynchronizeStatValues();

                    //running the Advance Stat method for the corresponding characterStats index
                    switch (statIndex)
                    {
                        case 0:
                            SetAdvStrengthStats();
                            break;
                        case 1:
                            SetAdvDexterityStats();
                            break;
                        case 2:
                            SetAdvIntellectStats();
                            break;
                        case 3:
                            SetAdvEnduranceStats();
                            break;
                        case 4:
                            SetAdvCharmStats();
                            break;
                        case 5:
                            SetAdvStealthStats();
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    Debug.LogWarning("No available stat points.");
                }
            }
            else
            {
                Debug.LogError("Failed to parse the value in TextMeshPro as an integer.");
            }
        }
        else
        {
            Debug.LogError("TextMeshPro reference is missing or invalid stat index.");
        }
    }

    public void RemoveStatPoints(int statIndex)
    {
        if (!pointsLocked && statIndex >= 0 && statIndex < characterStats.Length)
        {
            if (characterStats[statIndex] > 0)
            {
                characterStats[statIndex]--; // Decrease the chosen stat
                int statPointsValue;
                if (int.TryParse(availablePoints.text, out statPointsValue))
                {
                    statPointsValue++; // Increase available stat points
                    availablePoints.text = statPointsValue.ToString(); // Update UI for available points
                    UpdateStatUI(statIndex); // Update UI display for the chosen stat
                    SynchronizeStatValues(); //Update UI dispaly (matching the stats btwn the Stats Panel & Main Panel)

                    //running the Advance Stat method for the corresponding characterStats index
                    switch (statIndex)
                    {
                        case 0:
                            SetAdvStrengthStats();
                            break;
                        case 1:
                            SetAdvDexterityStats();
                            break;
                        case 2:
                            SetAdvIntellectStats();
                            break;
                        case 3:
                            SetAdvEnduranceStats();
                            break;
                        case 4:
                            SetAdvCharmStats();
                            break;
                        case 5:
                            SetAdvStealthStats();
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    Debug.LogError("Failed to parse the value in TextMeshPro as an integer.");
                }
            }
            else
            {
                Debug.LogWarning("Stat value is already at its minimum.");
            }
        }
        else
        {
            Debug.LogError("Invalid stat index.");
        }
    }

    //confirm point distribution when player hits Confirm Button
    public void LockPoints()
    {
        pointsLocked = true;
    }

    //change the stat TMP UI when distributing points
    public void UpdateStatUI(int statIndex)
    {
        if (statIndex >= 0 && statIndex < statValuesTextStatsPanel.Length)
        {
            if (statValuesTextStatsPanel[statIndex] != null)
            {
                statValuesTextStatsPanel[statIndex].text = characterStats[statIndex].ToString(); // Update the stat UI display
            }
            else
            {
                Debug.LogError("Stat TextMeshPro reference is missing.");
            }
        }
        else
        {
            Debug.LogError("Invalid stat index.");
        }
    }

    //method for making sure that when the stats on the Survivor Stats Panel change due to point distribution, the stats on the Stats Main Panel change as well
    public void SynchronizeStatValues()
    {
        // Ensure that both arrays have the same length
        if (statValuesTextMainPanel.Length != statValuesTextStatsPanel.Length)
        {
            Debug.LogError("Array lengths don't match.");
            return;
        }

        // Synchronize values between the two arrays
        for (int i = 0; i < statValuesTextMainPanel.Length; i++)
        {
            if (statValuesTextMainPanel[i] != null && statValuesTextStatsPanel[i] != null)
            {
                statValuesTextMainPanel[i].text = statValuesTextStatsPanel[i].text;
            }
            else
            {
                Debug.LogError("One or more TextMeshPro references is missing.");
            }
        }
    }
    #endregion

//adjust advanced statistics based off main stats
#region
    public TextMeshProUGUI[] advStrengthStats;
    public TextMeshProUGUI[] advDexterityStats;
    public TextMeshProUGUI[] advIntellectStats;
    public TextMeshProUGUI[] advEnduranceStats;
    public TextMeshProUGUI[] advCharmStats;
    public TextMeshProUGUI[] advStealthStats;

    public void SetAdvStrengthStats()
    {
        int strengthMain = characterStats[0]; //taking the index from the characterStats[] for the strength stat, which has an idex of 0
        int bonus = strengthMain / 2; // Calculate the bonus based on strength

        for (int i = 0; i < advStrengthStats.Length; i++)
        {
            advStrengthStats[i].text = bonus.ToString(); // Update the TextMeshPro field with the new value
        }
    }

    public void SetAdvDexterityStats()
    {
        int dexterityMain = characterStats[1];
        int bonus = dexterityMain / 2;

        for(int i = 0; i < advDexterityStats.Length; i++)
        {
            advDexterityStats[i].text = bonus.ToString();
        }
    }

    public void SetAdvIntellectStats()
    {
        int intellectMain = characterStats[2];
        int bonus = intellectMain / 2;

        for (int i = 0; i < advIntellectStats.Length; i++)
        {
            advIntellectStats[i].text = bonus.ToString();
        }
    }

    public void SetAdvEnduranceStats()
    {
        int enduranceMain = characterStats[3];
        int bonus = enduranceMain / 2;

        for (int i = 0; i < advEnduranceStats.Length; i++)
        {
            advEnduranceStats[i].text = bonus.ToString();
        }
    }

    public void SetAdvCharmStats()
    {
        int charmMain = characterStats[4];
        int bonus = charmMain / 2;

        for (int i = 0; i < advCharmStats.Length; i++)
        {
            advCharmStats[i].text = bonus.ToString();
        }
    }

    public void SetAdvStealthStats()
    {
        int stealthMain = characterStats[5];
        int bonus = stealthMain / 2;

        for (int i = 0; i < advStealthStats.Length; i++)
        {
            advStealthStats[i].text = bonus.ToString();
        }
    }

    #endregion
}
