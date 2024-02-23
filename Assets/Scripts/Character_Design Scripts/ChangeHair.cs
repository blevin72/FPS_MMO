using UnityEngine;
using TMPro;

public class ChangeHair : MonoBehaviour
{
    public Renderer shortHair;
    public Renderer mediumHair;
    public Renderer longHair;
    public Renderer longHairForHats;
    public Renderer mediumHairForHats;

    public ChangeHat changeHat; /*reference to ChangeHat class needed for ChangeHairType() to ensure hair types change depending on
                                 which hat the player chooses
                                 i.e long hair --> long hair for hats meshes*/

    public void Start()
    {
        shortHair.enabled = false;
        mediumHair.enabled = false;
        longHair.enabled = false;
        longHairForHats.enabled = false;
        mediumHairForHats.enabled = false;
    }

    public void ChangeHairType(TMP_Dropdown change)
    {
        switch (change.value)
        {
            case 0:
                shortHair.enabled = false;
                mediumHair.enabled = false;
                longHair.enabled = false;
                longHairForHats.enabled = false;
                mediumHairForHats.enabled = false;
                break;
            case 1:
                mediumHair.enabled = false;
                longHair.enabled = false;
                longHairForHats.enabled = false;
                mediumHairForHats.enabled = false;
                if(changeHat.activeHatType == 0)
                {
                    shortHair.enabled = true;
                }
                else
                {
                    shortHair.enabled = false;
                }
                break;
            case 2:
                shortHair.enabled = false;
                longHair.enabled = false;
                longHairForHats.enabled = false;
                mediumHairForHats.enabled = false;
                if(changeHat.activeHatType == 0)
                {
                    mediumHair.enabled = true;
                }
                else
                {
                    mediumHairForHats.enabled = true;
                }
                break;
            case 3:
                shortHair.enabled = false;
                mediumHair.enabled = false;
                if(changeHat.activeHatType == 0)
                {
                    longHair.enabled = true;
                }
                else
                {
                    longHairForHats.enabled = true;
                }
                break;
            default:
                Debug.Log("Invalid Dropdown option");
                break;
        }
    }

   
    public Material[] hairColor;
    private int hairColorIndex = 0;

    public void ChangeHairColor()
    {
        if(hairColorIndex < hairColor.Length - 1)
        {
            hairColorIndex++;
        }
        else
        {
            hairColorIndex = 0;
        }
        shortHair.material = hairColor[hairColorIndex];
        longHair.material = hairColor[hairColorIndex];
        mediumHair.material = hairColor[hairColorIndex];
        mediumHairForHats.material = hairColor[hairColorIndex];
        longHairForHats.material = hairColor[hairColorIndex];
        Debug.Log("Hair Color: " + hairColorIndex);
    }

    public void ChangeHairColorReverse()
    {
        if (hairColorIndex < hairColor.Length - 1 & hairColorIndex != 0)
        {
            hairColorIndex--;
        }
        else
        {
            hairColorIndex = 0;
        }
        shortHair.material = hairColor[hairColorIndex];
        longHair.material = hairColor[hairColorIndex];
        mediumHair.material = hairColor[hairColorIndex];
        mediumHairForHats.material = hairColor[hairColorIndex];
        longHairForHats.material = hairColor[hairColorIndex];
        Debug.Log("Hair Color: " + hairColorIndex);
    }
}
