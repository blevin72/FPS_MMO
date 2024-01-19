using UnityEngine;
using TMPro;

public class ChangePants : MonoBehaviour
{
    public Renderer dressPants;
    public Renderer shorts;
    public Renderer joggers;
    private int activePantsType = 0;

    private void Start()
    {
        dressPants.enabled = true;
        shorts.enabled = false;
        joggers.enabled = false;
        selectedPantsType = dressPantsColors; //set the selectedPantsType[] to match the dressPantsColors[];
    }

    public void ChangePantsType(TMP_Dropdown change)
    {
        switch (change.value)
        {
            case 0:
                dressPants.enabled = true;
                shorts.enabled = false;
                joggers.enabled = false;
                activePantsType = 0;
                SelectPantsType();
                break;
            case 1:
                dressPants.enabled = false;
                shorts.enabled = true;
                joggers.enabled = false;
                activePantsType = 1;
                SelectPantsType();
                break;
            case 2:
                dressPants.enabled = false;
                shorts.enabled = false;
                joggers.enabled = true;
                activePantsType = 2;
                SelectPantsType();
                break;
            default:
                Debug.Log("No pants are equipped");
                break;
        }
    }

    private void SelectPantsType() //match the selectedPantsType[] materials to = the corresponding []'s
    {
        switch (activePantsType)
        {
            case 0:
                selectedPantsType = dressPantsColors;
                break;
            case 1:
                selectedPantsType = shortsColors;
                break;
            case 2:
                selectedPantsType = joggersColors;
                break;
        }
    }

    public Material[] dressPantsColors;
    public Material[] shortsColors;
    public Material[] joggersColors;
    private Material[] selectedPantsType = null;
    private int dressPantsColorIndex = 0;
    private int shortsColorIndex = 0;
    private int joggersColorIndex = 0;
    private int selectedPantsColorIndex = 0;

    public void ChangsPantsColors()
    {
        if (selectedPantsType != null && selectedPantsType.Length > 0)
        {
            switch (activePantsType)
            {
                case 0:
                    dressPantsColorIndex = (dressPantsColorIndex + 1) % selectedPantsType.Length;
                    dressPants.material = selectedPantsType[dressPantsColorIndex];
                    selectedPantsColorIndex = dressPantsColorIndex;
                    Debug.Log("Dress Pants Color: " + dressPantsColorIndex);
                    break;
                case 1:
                    shortsColorIndex = (shortsColorIndex + 1) % selectedPantsType.Length;
                    shorts.material = selectedPantsType[shortsColorIndex];
                    selectedPantsColorIndex = shortsColorIndex;
                    Debug.Log("Shorts Color: " + shortsColorIndex);
                    break;
                case 2:
                    joggersColorIndex = (joggersColorIndex + 1) % selectedPantsType.Length;
                    joggers.material = selectedPantsType[joggersColorIndex];
                    selectedPantsColorIndex = joggersColorIndex;
                    Debug.Log("Joggers Hat Color: " + joggersColorIndex);
                    break;              
                default:
                    Debug.LogError("Invalid active pants type");
                    break;
            }
        }
    }

    public void ChangePantsColorsReverse()
    {
        if (selectedPantsType != null && selectedPantsType.Length > 0 && selectedPantsColorIndex != 0)
        {
            switch (activePantsType)
            {
                case 0:
                    dressPantsColorIndex = (dressPantsColorIndex - 1) % selectedPantsType.Length;
                    dressPants.material = selectedPantsType[dressPantsColorIndex];
                    selectedPantsColorIndex = dressPantsColorIndex;
                    Debug.Log("Dress Pants Color: " + dressPantsColorIndex);
                    break;
                case 1:
                    shortsColorIndex = (shortsColorIndex - 1) % selectedPantsType.Length;
                    shorts.material = selectedPantsType[shortsColorIndex];
                    selectedPantsColorIndex = shortsColorIndex;
                    Debug.Log("Shorts Color: " + shortsColorIndex);
                    break;
                case 2:
                    joggersColorIndex = (joggersColorIndex - 1) % selectedPantsType.Length;
                    joggers.material = selectedPantsType[joggersColorIndex];
                    selectedPantsColorIndex = joggersColorIndex;
                    Debug.Log("Joggers Hat Color: " + joggersColorIndex);
                    break;              
                default:
                    Debug.LogError("Invalid active pants type");
                    break;
            }
        }
    }
}