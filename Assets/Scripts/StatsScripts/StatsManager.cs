using UnityEngine;
using TMPro;
public class StatsManager : MonoBehaviour
{
    public GameObject strengthStats;
    public GameObject advStrengthStats;
    public GameObject dexterityStats;
    public GameObject advDexterityStats;
    public GameObject intellectStats;
    public GameObject advIntellectStats;
    public GameObject enduranceStats;
    public GameObject advEnduranceStats;
    public GameObject charmStats;
    public GameObject advCharmStats;
    public GameObject stealthStats;
    public GameObject advStealthStats;

    public void StrengthStatsSwitch()
    {
        strengthStats.SetActive(!strengthStats.activeSelf);
        advStrengthStats.SetActive(!advStrengthStats.activeSelf);
    }

    public void DexterityStatsSwitch()
    {
        dexterityStats.SetActive(!dexterityStats.activeSelf);
        advDexterityStats.SetActive(!advDexterityStats.activeSelf);
    }

    public void IntellectStatsSwitch()
    {
        intellectStats.SetActive(!intellectStats.activeSelf);
        advIntellectStats.SetActive(!advIntellectStats.activeSelf);
    }

    public void EnduranceStatsSwitch()
    {
        enduranceStats.SetActive(!enduranceStats.activeSelf);
        advEnduranceStats.SetActive(!advEnduranceStats.activeSelf);
    }

    public void CharmStatsSwitch()
    {
        charmStats.SetActive(!charmStats.activeSelf);
        advCharmStats.SetActive(!advCharmStats.activeSelf);
    }

    public void StealthStatsSwitch()
    {
        stealthStats.SetActive(!stealthStats.activeSelf);
        advStealthStats.SetActive(!advStealthStats.activeSelf);
    }
}
