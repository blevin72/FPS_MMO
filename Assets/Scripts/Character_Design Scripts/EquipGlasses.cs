using UnityEngine;
using TMPro;

public class EquipGlasses : MonoBehaviour
{
    public Renderer glasses;
    public TMP_Dropdown glassesDropdown;

    private void Start()
    {
        glasses.enabled = false;
    }

    public void Equip_Remove_Glasses(TMP_Dropdown change)
    {
        switch (change.value)
        {
            case 0:
                glasses.enabled = false;
                break;
            case 1:
                glasses.enabled = true;
                break; 
        }
    }

    public Material[] glassesColors;
    private int glassesColorsIndex = 0;

    public void ChangeGlassesColors()
    {
        if(glassesColorsIndex < glassesColors.Length - 1)
        {
            glassesColorsIndex++;
            Debug.Log("Glasses Color is: " + glassesColorsIndex);
        }
        else
        {
            glassesColorsIndex = 0;
        }

        glasses.material = glassesColors[glassesColorsIndex];
    }

    public void ChangeGlassesColorsReverse()
    {
        if (glassesColorsIndex < glassesColors.Length - 1 && glassesColorsIndex != 0)
        {
            glassesColorsIndex--;
            Debug.Log("Glasses Color is: " + glassesColorsIndex);
        }
        else
        {
            glassesColorsIndex = 0;
        }

        glasses.material = glassesColors[glassesColorsIndex];
    }
}
