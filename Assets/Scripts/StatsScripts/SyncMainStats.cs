using TMPro;
using UnityEngine;

public class SyncMainStats : MonoBehaviour
{
    private SurvivorStats survivorStats;
    public TextMeshProUGUI[] statValuesTextStatsPanel; // Array of text fields for displaying stats (Stats Panel)
    public TextMeshProUGUI[] statValuesTextMainPanel;  // Array of text fields for dispalying stats (Main Panel)
    public TextMeshProUGUI[] advStatValuesMainPanel;   // Array of text fields for dispalying the adv stats (Main Panel)
    public TextMeshProUGUI[] advStatValuesStatsPanel;  // Array of text fields for dispalying the adv stats (Stats Panel)

    public void Start()
    {
        survivorStats = GameObject.FindObjectOfType<SurvivorStats>();
        if (survivorStats != null)
        {
            int[] stats = survivorStats.characterStats;
            // Access the characterStats array here
        }
    }

    //change the stat TMP UI when distributing points
    public void UpdateStatUI(int statIndex)
    {
        if (statIndex >= 0 && statIndex < statValuesTextStatsPanel.Length)
        {
            if (statValuesTextStatsPanel[statIndex] != null)
            {
                statValuesTextStatsPanel[statIndex].text = survivorStats.characterStats[statIndex].ToString(); // Update the stat UI display
            }
        }
    }

    //called in the DistributePoints() & the RemovePoints()
    //syncs the stats from the stats panel to the main panel
    public void SynchronizeMainStatValues()
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

    //called in each of the SetAdvStat()
    //syncs the adv stats from the stats panel to the main panel
    public void SynchronizeAdvStatValues()
    {
        for (int i = 0; i < advStatValuesMainPanel.Length; i++)
        {
            if (advStatValuesMainPanel[i] != null && advStatValuesStatsPanel[i] != null)
            {
                advStatValuesMainPanel[i].text = advStatValuesStatsPanel[i].text;
            }
        }
    }
}
