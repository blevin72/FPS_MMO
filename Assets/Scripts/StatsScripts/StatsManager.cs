using UnityEngine;

public class StatsManager : MonoBehaviour
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

    private Stamina_HP_Exp stamina_HP_Exp;
    public SyncMainStats syncMainStats;
    private SyncAdvStats syncAdvStats;
    public DistributePoints distributePoints;
    

    //set all stats to active and all advanced stats to inactive
    void Start()
    {
        SetStatsUI();
        GetComponents();
    }

    public void SetStatsUI()
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
    }

    public void GetComponents()
    {
        stamina_HP_Exp = GetComponent<Stamina_HP_Exp>();
        distributePoints = GetComponent<DistributePoints>();
        syncMainStats = GetComponent<SyncMainStats>();
        syncAdvStats = GetComponent<SyncAdvStats>();
        stamina_HP_Exp.SetHealthStaminaExp(syncAdvStats.advEnduranceStats, syncAdvStats.advIntellectStats);
        stamina_HP_Exp.SynchronizeAttributes();
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
}
