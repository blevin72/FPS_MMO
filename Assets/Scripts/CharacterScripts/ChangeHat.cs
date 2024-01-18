using TMPro;
using UnityEngine;

public class ChangeHat : MonoBehaviour
{
    public Renderer baseballCap;
    public Renderer beanie;
    public Renderer cowboyHat;
    internal int activeHatType = 0;
    public ChangeHair changeHair; /*reference to ChangeHat class needed for ChangeHairType() to ensure hair types change depending on
                                   which hat the player chooses
                                   i.e long hair --> long hair for hats meshes*/

    public void Start()
    {
        baseballCap.enabled = false;
        beanie.enabled = false;
        cowboyHat.enabled = false;
    }

    public void ChangeHatType(TMP_Dropdown change)
    {
        switch (change.value)
        {
            case 0:
                activeHatType = 0;
                baseballCap.enabled = false;
                beanie.enabled = false;
                cowboyHat.enabled = false;
                if(changeHair.longHairForHats.enabled == true)  //if a hat is currently equipped and is removed change hair types accordingly
                {                                               //i.e. longHairForHats --> longHair; since the hat is being removed.
                    changeHair.longHairForHats.enabled = false;
                    changeHair.longHair.enabled = true;
                }
                else if(changeHair.mediumHairForHats.enabled == true) //see above comment
                {
                    changeHair.mediumHairForHats.enabled = false;
                    changeHair.mediumHair.enabled = true;
                }
                SelectHatType();
                break;
            case 1:
                activeHatType = 1;
                beanie.enabled = false;
                cowboyHat.enabled = false;
                baseballCap.enabled = true;
                CheckHairType(); //check hair type so we can switch between meshes for or not for hats (i.e. longHairForHats or longHair)
                SelectHatType(); //select hat type so we access the correct color materials for that hat
                break;
            case 2:
                activeHatType = 2;
                baseballCap.enabled = false;
                beanie.enabled = true;
                cowboyHat.enabled = false;
                CheckHairType();
                SelectHatType();
                break;
            case 3:
                activeHatType = 3;
                baseballCap.enabled = false;
                beanie.enabled = false;
                cowboyHat.enabled = true;
                CheckHairType();
                SelectHatType();
                break;
            default:
                Debug.Log("Invalid Dropdown option");
                break;
        }
    }


    public Material[] baseballCapColors;
    public Material[] beanieColors;
    public Material[] cowboyHatColors;
    private Material[] selectedHatType = null;
    private int baseballCapColorIndex = 0;
    private int beanieColorIndex = 0;
    private int cowboyHatColorIndex = 0;
    private int selectedHatColorIndex = 0;

    private void SelectHatType()
    {
        switch (activeHatType)
        {
            case 0:
                selectedHatType = null;
                break;
            case 1:
                selectedHatType = baseballCapColors;
                break;
            case 2:
                selectedHatType = beanieColors;
                break;
            case 3:
                selectedHatType = cowboyHatColors;
                break;
        }
    }

    private void CheckHairType()
    {
        if (changeHair.shortHair.enabled == true)
        {
            changeHair.shortHair.enabled = false;
        }
        else if (changeHair.mediumHair.enabled == true)
        {
            changeHair.mediumHair.enabled = false;
            changeHair.mediumHairForHats.enabled = true;
        }
        else if (changeHair.longHair.enabled == true)
        {
            changeHair.longHair.enabled = false;
            changeHair.longHairForHats.enabled = true;
        }
    }

    public void ChangeHatColor()
    {
        if (selectedHatType != null && selectedHatType.Length > 0)
        {
            switch (activeHatType)
            {
                case 0:
                    Debug.Log("No hat assigned.");
                    break;
                case 1:
                    baseballCapColorIndex = (baseballCapColorIndex + 1) % selectedHatType.Length;
                    baseballCap.material = selectedHatType[baseballCapColorIndex];
                    selectedHatColorIndex = baseballCapColorIndex;
                    Debug.Log("Baseball Cap Color: " + baseballCapColorIndex);
                    break;
                case 2:
                    beanieColorIndex = (beanieColorIndex + 1) % selectedHatType.Length;
                    beanie.material = selectedHatType[beanieColorIndex];
                    selectedHatColorIndex = beanieColorIndex;
                    Debug.Log("Beanie Color: " + beanieColorIndex);
                    break;
                case 3:
                    cowboyHatColorIndex = (cowboyHatColorIndex + 1) % selectedHatType.Length;
                    cowboyHat.material = selectedHatType[cowboyHatColorIndex];
                    selectedHatColorIndex = cowboyHatColorIndex;
                    Debug.Log("Cowboy Hat Color: " + cowboyHatColorIndex);
                    break;
                default:
                    Debug.LogError("Invalid active hat type");
                    break;
            }
        }
    }

    public void ChangeHatColorReverse()
    {
        if (selectedHatType != null && selectedHatType.Length > 0 && selectedHatColorIndex != 0)
        {
            switch (activeHatType)
            {
                case 0:
                    Debug.Log("No hat assigned.");
                    break;
                case 1:
                    baseballCapColorIndex = (baseballCapColorIndex - 1) % selectedHatType.Length;
                    baseballCap.material = selectedHatType[baseballCapColorIndex];
                    selectedHatColorIndex = baseballCapColorIndex; //
                    Debug.Log("Baseball Cap Color: " + baseballCapColorIndex);
                    break;
                case 2:
                    beanieColorIndex = (beanieColorIndex - 1) % selectedHatType.Length;
                    beanie.material = selectedHatType[beanieColorIndex];
                    selectedHatColorIndex = beanieColorIndex;//
                    Debug.Log("Beanie Color: " + beanieColorIndex);
                    break;
                case 3:
                    cowboyHatColorIndex = (cowboyHatColorIndex - 1) % selectedHatType.Length;
                    cowboyHat.material = selectedHatType[cowboyHatColorIndex];
                    selectedHatColorIndex = cowboyHatColorIndex;//
                    Debug.Log("Cowboy Hat Color: " + cowboyHatColorIndex);
                    break;
                default:
                    Debug.LogError("Invalid active hat type");
                    break;
            }
        }
    }
}
