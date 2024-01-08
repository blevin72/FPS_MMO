using UnityEngine;
using UnityEngine.UI;

public class Mission_Abilities : MonoBehaviour
{
    private Sprite selectedAbility;
    public Image equippedMissionAbility_1;
    public Image equippedMissionAbility_2;
    public Image equippedMissionAbility_3;

    public void SelectAbilityFromSV(Sprite ability)
    {
        selectedAbility = ability;
        //Debug.Log("Selected Ability is " + selectedAbility);
    }

    public void AssignAbility(Image equippedAbility)
    {
        /*check to see if an ability was selected from scroll view before attemtping to assign to a slot
          check to see if the ability the player is attempting to assign is not already assigned to a slot to prevent double assignment*/

        if (selectedAbility != null && equippedMissionAbility_1.sprite != selectedAbility && equippedMissionAbility_2.sprite != selectedAbility
            && equippedMissionAbility_3.sprite != selectedAbility)
        {
            equippedAbility.sprite = selectedAbility; //assign selected ability to the designated equipped ability slot

            Color imageColor = equippedAbility.color; //changing empty item slot image from fully transparent to fully opaque
            imageColor.a = 1f; // Setting alpha to 1 (fully opaque)
            equippedAbility.color = imageColor;
        }
    }
}
