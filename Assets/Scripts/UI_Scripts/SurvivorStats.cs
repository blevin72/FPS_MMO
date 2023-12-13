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

//toggle between main stats & advanced stats
#region
    public void ToggleStatsVisibility(GameObject stats, GameObject advStats)
    {
        stats.SetActive(!stats.activeSelf);
        advStats.SetActive(!advStats.activeSelf);
    }

    public void StrengthIconClick()
    {
        ToggleStatsVisibility(strengthStats, strengthAdvStats);
    }

    public void DexterityIconClick()
    {
        ToggleStatsVisibility(dexterityStats, dexterityAdvStats);
    }

    public void IntellectIconClick()
    {
        ToggleStatsVisibility(intelectStats, intelectAdvStats);
    }

    public void EnduranceIconClick()
    {
        ToggleStatsVisibility(enduranceStats, enduranceAdvStats);
    }

    public void CharmIconClick()
    {
        ToggleStatsVisibility(charmStats, charmAdvStats);
    }

    public void StealthIconClick()
    {
        ToggleStatsVisibility(stealthStats, stealthAdvStats);
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
            }
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
            }
        }   
    }

    //confirm point distribution when player hits Confirm Button
    public void LockPoints()
    {
        pointsLocked = true;
    }
    #endregion

//adjust main stats UI and sync between main/stats panel
#region
    //change the stat TMP UI when distributing points
    public void UpdateStatUI(int statIndex)
    {
        if (statIndex >= 0 && statIndex < statValuesTextStatsPanel.Length)
        {
            if (statValuesTextStatsPanel[statIndex] != null)
            {
                statValuesTextStatsPanel[statIndex].text = characterStats[statIndex].ToString(); // Update the stat UI display
            }
        }
    }

    //method for making sure that when the stats on the Survivor Stats Panel change due to point distribution, the stats on the Stats Main Panel change as well
    public void SynchronizeStatValues()
    {
        SynchronizeMainStatValues();
    }

    private void SynchronizeMainStatValues()
    {
        // Synchronize values between the two arrays
        for (int i = 0; i < statValuesTextMainPanel.Length; i++)
        {
            if (statValuesTextMainPanel[i] != null && statValuesTextStatsPanel[i] != null)
            {
                statValuesTextMainPanel[i].text = statValuesTextStatsPanel[i].text;
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

    public TextMeshProUGUI meleePointsMain;
    public TextMeshProUGUI capacityPointsMain;
    public TextMeshProUGUI criticalPointsMain;
    public TextMeshProUGUI escapePointsMain;
    public TextMeshProUGUI speedPointsMain;
    public TextMeshProUGUI lockpickPointsMain;
    public TextMeshProUGUI craftPointsMain;
    public TextMeshProUGUI healthPointsMain;
    public TextMeshProUGUI staminaPointsMain;
    public TextMeshProUGUI immunityPointsMain;
    public TextMeshProUGUI hagglePointsMain;
    public TextMeshProUGUI reputationPointsMain;
    public TextMeshProUGUI agroPointsMain;
    public TextMeshProUGUI stealthKillPointsMain;



    public void SetAdvStrengthStats()
    {
        int strengthMain = characterStats[0]; //taking the index from the characterStats[] for the strength stat, which has an idex of 0
        int bonus = strengthMain / 2; // Calculate the bonus based on strength

        for (int i = 0; i < advStrengthStats.Length; i++)
        {
            advStrengthStats[i].text = bonus.ToString(); // Update the TextMeshPro field with the new value
        }

        meleePointsMain.text = advStrengthStats[0].text;
        Debug.Log("meleePointsMain.text: " + meleePointsMain.text); // Check the value after assignment
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
