using TMPro;
using UnityEngine;

public class DistributePoints : MonoBehaviour
{
    private SyncMainStats syncMainStats;
    private Stamina_HP_Exp stamina_HP_Exp;
    private SyncAdvStats syncAdvStats;

    public TextMeshProUGUI availablePoints;
    public int[] characterStats = new int[6];
    private bool pointsLocked = false;

    public void Start()
    {
        GetComponents();
    }

    public void GetComponents()
    {
        syncMainStats = GetComponent<SyncMainStats>();
        stamina_HP_Exp = GetComponent<Stamina_HP_Exp>();
        syncAdvStats = GetComponent<SyncAdvStats>();
    }

    public void AddStatPoints(int statIndex)
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
                            break;
                        case 3:
                            syncAdvStats.SetAdvEnduranceStats();
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
                            break;
                        case 3:
                            syncAdvStats.SetAdvEnduranceStats();
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
}
