using TMPro;
using UnityEngine;

public class SyncAdvStats : MonoBehaviour
{
    private SurvivorStats survivorStats;
    private SyncMainStats syncMainStats;

    public TextMeshProUGUI[] advStrengthStats;
    public TextMeshProUGUI[] advDexterityStats;
    public TextMeshProUGUI[] advIntellectStats;
    public TextMeshProUGUI[] advEnduranceStats;
    public TextMeshProUGUI[] advCharmStats;
    public TextMeshProUGUI[] advStealthStats;

    public void Start()
    {
        survivorStats = GameObject.FindObjectOfType<SurvivorStats>();
        syncMainStats = GetComponent<SyncMainStats>();    
        int[] stats = survivorStats.characterStats;

        SetAdvStrengthStats();
        SetAdvDexterityStats();
        SetAdvIntellectStats();
        SetAdvEnduranceStats();
        SetAdvCharmStats();
        SetAdvStealthStats();
        
    }

    public void SetAdvStrengthStats()
    {
        int strengthMain = survivorStats.characterStats[0]; //taking the index from the characterStats[] for the strength stat, which has an idex of 0
        int bonus = strengthMain / 2; // Calculate the bonus based on strength

        for (int i = 0; i < advStrengthStats.Length; i++)
        {
            advStrengthStats[i].text = bonus.ToString(); // Update the TextMeshPro field with the new value
        }

        syncMainStats.SynchronizeAdvStatValues();
    }

    public void SetAdvDexterityStats()
    {
        int dexterityMain = survivorStats.characterStats[1];
        int bonus = dexterityMain / 2;

        for (int i = 0; i < advDexterityStats.Length; i++)
        {
            advDexterityStats[i].text = bonus.ToString();
        }

        syncMainStats.SynchronizeAdvStatValues();
    }

    public void SetAdvIntellectStats()
    {
        int intellectMain = survivorStats.characterStats[2];
        int bonus = intellectMain / 2;

        for (int i = 0; i < advIntellectStats.Length; i++)
        {
            advIntellectStats[i].text = bonus.ToString();
        }

        syncMainStats.SynchronizeAdvStatValues();
    }

    public void SetAdvEnduranceStats()
    {
        int enduranceMain = survivorStats.characterStats[3];
        int bonus = enduranceMain / 2;

        for (int i = 0; i < advEnduranceStats.Length; i++)
        {
            advEnduranceStats[i].text = bonus.ToString();
        }

        syncMainStats.SynchronizeAdvStatValues();
    }

    public void SetAdvCharmStats()
    {
        int charmMain = survivorStats.characterStats[4];
        int bonus = charmMain / 2;

        for (int i = 0; i < advCharmStats.Length; i++)
        {
            advCharmStats[i].text = bonus.ToString();
        }

        syncMainStats.SynchronizeAdvStatValues();
    }

    public void SetAdvStealthStats()
    {
        int stealthMain = survivorStats.characterStats[5];
        int bonus = stealthMain / 2;

        for (int i = 0; i < advStealthStats.Length; i++)
        {
            advStealthStats[i].text = bonus.ToString();
        }

        syncMainStats.SynchronizeAdvStatValues();
    }
}
