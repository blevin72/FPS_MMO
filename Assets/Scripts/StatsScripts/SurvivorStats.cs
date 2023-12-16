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

    public Stamina_HP_Exp stamina_HP_Exp;
    public SyncMainStats syncMainStats;
    public SyncAdvStats syncAdvStats;

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

        stamina_HP_Exp = GetComponent<Stamina_HP_Exp>();
        stamina_HP_Exp.SetHealthStaminaExp(syncAdvStats.advEnduranceStats, syncAdvStats.advIntellectStats);
        stamina_HP_Exp.SynchronizeAttributes();

        syncMainStats = GetComponent<SyncMainStats>();
        syncAdvStats = GetComponent<SyncAdvStats>();

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

//add & subtract stat points
#region
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
                    syncMainStats.UpdateStatUI(statIndex); //Update UI display for the chosen stat
                    syncMainStats.SynchronizeMainStatValues(); //Sync stats btwn main panel and stats panel
                    stamina_HP_Exp.SynchronizeAttributes();

                    //running the Advance Stat method for the corresponding characterStats index
                    switch (statIndex)
                    {
                        case 0:
                            syncAdvStats.SetAdvStrengthStats();
                            break;
                        case 1:
                            syncAdvStats.SetAdvDexterityStats();
                            break;
                        case 2:
                            syncAdvStats.SetAdvIntellectStats();
                            //stamina_HP_Exp.SetHealthStaminaExp(syncAdvStats.advEnduranceStats, syncAdvStats.advIntellectStats);
                            break;
                        case 3:
                            syncAdvStats.SetAdvEnduranceStats();
                            //stamina_HP_Exp.SetHealthStaminaExp(syncAdvStats.advEnduranceStats, syncAdvStats.advIntellectStats);
                            break;
                        case 4:
                            syncAdvStats.SetAdvCharmStats();
                            break;
                        case 5:
                            syncAdvStats.SetAdvStealthStats();
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
                    syncMainStats.UpdateStatUI(statIndex); // Update UI display for the chosen stat
                    syncMainStats.SynchronizeMainStatValues(); //Update UI dispaly (matching the stats btwn the Stats Panel & Main Panel)
                    stamina_HP_Exp.SynchronizeAttributes();

                    //running the Advance Stat method for the corresponding characterStats index
                    switch (statIndex)
                    {
                        case 0:
                            syncAdvStats.SetAdvStrengthStats();
                            break;
                        case 1:
                            syncAdvStats.SetAdvDexterityStats();
                            break;
                        case 2:
                            syncAdvStats.SetAdvIntellectStats();
                            //stamina_HP_Exp.SetHealthStaminaExp(syncAdvStats.advEnduranceStats, syncAdvStats.advIntellectStats);
                            break;
                        case 3:
                            syncAdvStats.SetAdvEnduranceStats();
                            //stamina_HP_Exp.SetHealthStaminaExp(syncAdvStats.advEnduranceStats, syncAdvStats.advIntellectStats);
                            break;
                        case 4:
                            syncAdvStats.SetAdvCharmStats();
                            break;
                        case 5:
                            syncAdvStats.SetAdvStealthStats();
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
}
