using TMPro;
using UnityEngine;

public class ChangeShirt : MonoBehaviour
{
    public Renderer shirt;
    public Renderer tanktop;
    internal int activeShirtType = 0; /*internal meaning I can access it in a different class (ChangeOuterwear script) but it does
                                       not appear 'publicly' in the inspector like a public variable would*/

    public void Start()
    {
        shirt.enabled = false;
        tanktop.enabled = false;
    }

    public void ChangeShirtType(TMP_Dropdown change)
    {
        switch (change.value)
        {
            case 0:
                activeShirtType = 0;
                shirt.enabled = false;
                tanktop.enabled = false;
                SelectShirtType();
                break;
            case 1:
                activeShirtType = 1;
                shirt.enabled = true;
                tanktop.enabled = false;
                SelectShirtType();
                break;
            case 2:
                activeShirtType = 2;
                shirt.enabled = false;
                tanktop.enabled = true;
                SelectShirtType();
                break;
            default:
                Debug.Log("Invalid Dropdown option");
                break;
        }
    }


    public Material[] shirtColor;
    public Material[] tanktopColor;
    private Material[] selectedShirtType = null;
    private int shirtColorIndex = 0;
    private int tanktopColorIndex = 0;
    private int selectedShirtColorIndex;

    private void SelectShirtType()
    {
        switch (activeShirtType)
        {
            case 0:
                selectedShirtType = null;
                break;
            case 1:
                selectedShirtType = shirtColor;
                break;
            case 2:
                selectedShirtType = tanktopColor;
                break;
        }
    }

    public void ChangeShirtColor()
    {
        if (selectedShirtType != null && selectedShirtType.Length > 0)
        {
            switch (activeShirtType)
            {
                case 0:
                    Debug.Log("No shirt assigned.");
                    break;
                case 1:
                    shirtColorIndex = (shirtColorIndex + 1) % selectedShirtType.Length;
                    shirt.material = selectedShirtType[shirtColorIndex];
                    selectedShirtColorIndex = shirtColorIndex;
                    Debug.Log("Shirt Color: " + shirtColorIndex);
                    break;
                case 2:
                    tanktopColorIndex = (tanktopColorIndex + 1) % selectedShirtType.Length;
                    tanktop.material = selectedShirtType[tanktopColorIndex];
                    selectedShirtColorIndex = tanktopColorIndex;
                    Debug.Log("Tanktop Color: " + tanktopColorIndex);
                    break;
                default:
                    Debug.LogError("Invalid active shirt type");
                    break;
            }
        }
    }

    public void ChangeShirtColorReverse()
    {
        if (selectedShirtType != null && selectedShirtType.Length > 0 && selectedShirtColorIndex != 0)
        {
            switch (activeShirtType)
            {
                case 0:
                    Debug.Log("No shirt assigned.");
                    break;
                case 1:
                    shirtColorIndex = (shirtColorIndex - 1) % selectedShirtType.Length;
                    shirt.material = selectedShirtType[shirtColorIndex];
                    selectedShirtColorIndex = shirtColorIndex;
                    Debug.Log("Shirt Color: " + shirtColorIndex);
                    break;
                case 2:
                    tanktopColorIndex = (tanktopColorIndex - 1) % selectedShirtType.Length;
                    tanktop.material = selectedShirtType[tanktopColorIndex];
                    selectedShirtColorIndex = tanktopColorIndex;
                    Debug.Log("Tanktop Color: " + tanktopColorIndex);
                    break;
                default:
                    Debug.LogError("Invalid active shirt type");
                    break;
            }
        }
    }
}
