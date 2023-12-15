using TMPro;
using UnityEngine;

public class Stamina_HP_Exp : MonoBehaviour
{
    public TextMeshProUGUI maximumHealth;
    public TextMeshProUGUI maximumStamina;
    public TextMeshProUGUI experienceBoost;

    private int previousHealthBonusValue = 0;
    private int previousStaminaBonusValue = 0;
    private int previousExpBoostBonusValue = 0;

    //adjust health, stamina, progression via advanced statistics
    public void SetHealthStaminaExp(TextMeshProUGUI[] advEnduranceStats, TextMeshProUGUI[] advIntellectStats)
    {
        UpdateStatFromBonus(advEnduranceStats[0], ref previousHealthBonusValue, maximumHealth);
        UpdateStatFromBonus(advEnduranceStats[1], ref previousStaminaBonusValue, maximumStamina);
        UpdateStatFromBonus(advIntellectStats[2], ref previousExpBoostBonusValue, experienceBoost);
    }

    private void UpdateStatFromBonus(TextMeshProUGUI bonusText, ref int previousBonusValue, TextMeshProUGUI statText)
    {
        int currentValue = int.Parse(statText.text);
        int bonusValue;

        if (int.TryParse(bonusText.text, out bonusValue))
        {
            int bonusDifference = bonusValue - previousBonusValue;
            previousBonusValue = bonusValue;

            int newTotal = currentValue + bonusDifference;

            if (bonusDifference > 0)
            {
                statText.text = newTotal.ToString();
            }
            else if (bonusDifference < 0)
            {
                statText.text = Mathf.Max(0, newTotal).ToString();
            }
        }
    }
}
