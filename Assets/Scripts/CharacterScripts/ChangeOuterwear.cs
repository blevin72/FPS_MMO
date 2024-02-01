using TMPro;
using UnityEngine;

public class ChangeOuterwear : MonoBehaviour
{
    public Renderer sweater;
    public Renderer windbreaker;
    public Renderer openShirt;
    internal int activeOuterwearType = 0;
    public ChangeShirt changeShirt; //reference the ChangeShirt class (needed for ChangeOuterwearType()

    public void Start()
    {
        sweater.enabled = false;
        windbreaker.enabled = false;
        openShirt.enabled = false;
    }

    public void ChangeOuterwearType(TMP_Dropdown change)
    {
        switch (change.value)
        {
            case 0:
                activeOuterwearType = 0;
                sweater.enabled = false;
                windbreaker.enabled = false;
                openShirt.enabled = false;
                SelectOuterwearType();
                if(changeShirt.activeShirtType == 1)
                {
                    changeShirt.shirt.enabled = true;
                }
                else if(changeShirt.activeShirtType == 2)
                {
                    changeShirt.tanktop.enabled = true;
                }
                break;
            case 1:
                activeOuterwearType = 1;
                sweater.enabled = true;
                windbreaker.enabled = false;
                openShirt.enabled = false;
                changeShirt.shirt.enabled = false; //remove shirt when sweater is enabled to avoid overlapping meshes
                changeShirt.tanktop.enabled = false; //remove tanktop when sweater is enabled to avoid overlapping meshes
                SelectOuterwearType();
                break;
            case 2:
                activeOuterwearType = 2;
                sweater.enabled = false;
                windbreaker.enabled = true;
                openShirt.enabled = false;
                changeShirt.shirt.enabled = false; //remove shirt when windbreaker is enabled to avoid overlapping meshes
                changeShirt.tanktop.enabled = false; //remove tanktop when windbreaker is enabled to avoid overlapping meshes
                SelectOuterwearType();
                break;
            case 3:
                activeOuterwearType = 3;
                sweater.enabled = false;
                windbreaker.enabled = false;
                openShirt.enabled = true;
                SelectOuterwearType();
                break;
            default:
                Debug.Log("Invalid Dropdown option");
                break;
        }
    }


    public Material[] sweaterColors;
    public Material[] windbreakerColors;
    public Material[] openShirtColors;
    private Material[] selectedOuterwearType = null;
    private int sweaterColorIndex = 0;
    private int windbreakerColorIndex = 0;
    private int openShirtColorIndex = 0;
    private int selectedOuterwearColorIndex = 0;

    private void SelectOuterwearType()
    {
        switch (activeOuterwearType)
        {
            case 0:
                selectedOuterwearType = null;
                break;
            case 1:
                selectedOuterwearType = sweaterColors;
                break;
            case 2:
                selectedOuterwearType = windbreakerColors;
                break;
            case 3:
                selectedOuterwearType = openShirtColors;
                break;
        }
    }

    public void ChangeOuterwearColor()
    {
        if (selectedOuterwearType != null && selectedOuterwearType.Length > 0)
        {
            switch (activeOuterwearType)
            {
                case 0:
                    Debug.Log("No hat assigned.");
                    break;
                case 1:
                    sweaterColorIndex = (sweaterColorIndex + 1) % selectedOuterwearType.Length;
                    sweater.material = selectedOuterwearType[sweaterColorIndex];
                    selectedOuterwearColorIndex = sweaterColorIndex;
                    Debug.Log("Sweater Color: " + sweaterColorIndex);
                    break;
                case 2:
                    windbreakerColorIndex = (windbreakerColorIndex + 1) % selectedOuterwearType.Length;
                    windbreaker.material = selectedOuterwearType[windbreakerColorIndex];
                    selectedOuterwearColorIndex = windbreakerColorIndex;
                    Debug.Log("Windbreaker Color: " + windbreakerColorIndex);
                    break;
                case 3:
                    openShirtColorIndex = (openShirtColorIndex + 1) % selectedOuterwearType.Length;
                    openShirt.material = selectedOuterwearType[openShirtColorIndex];
                    selectedOuterwearColorIndex = openShirtColorIndex;
                    Debug.Log("Open Shirt Color: " + openShirtColorIndex);
                    break;
                default:
                    Debug.LogError("Invalid active outerwear type");
                    break;
            }
        }
    }

    public void ChangeOuterwearColorReverse()
    {
        if (selectedOuterwearType != null && selectedOuterwearType.Length > 0 && selectedOuterwearColorIndex != 0)
        {
            switch (activeOuterwearType)
            {
                case 0:
                    Debug.Log("No hat assigned.");
                    break;
                case 1:
                    sweaterColorIndex = (sweaterColorIndex - 1) % selectedOuterwearType.Length;
                    sweater.material = selectedOuterwearType[sweaterColorIndex];
                    selectedOuterwearColorIndex = sweaterColorIndex;
                    Debug.Log("Sweater Color: " + sweaterColorIndex);
                    break;
                case 2:
                    windbreakerColorIndex = (windbreakerColorIndex - 1) % selectedOuterwearType.Length;
                    windbreaker.material = selectedOuterwearType[windbreakerColorIndex];
                    selectedOuterwearColorIndex = windbreakerColorIndex;
                    Debug.Log("Windbreaker Color: " + windbreakerColorIndex);
                    break;
                case 3:
                    openShirtColorIndex = (openShirtColorIndex - 1) % selectedOuterwearType.Length;
                    openShirt.material = selectedOuterwearType[openShirtColorIndex];
                    selectedOuterwearColorIndex = openShirtColorIndex;
                    Debug.Log("Open Shirt Color: " + openShirtColorIndex);
                    break;
                default:
                    Debug.LogError("Invalid active outerwear type");
                    break;
            }
        }
    }
}
