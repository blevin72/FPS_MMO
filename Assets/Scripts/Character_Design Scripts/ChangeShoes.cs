using UnityEngine;
using TMPro;

public class ChangeShoes : MonoBehaviour
{
    public Renderer boots;
    public Renderer sneakers;
    private int activeShoeType = 0;

    private void Start()
    {
        boots.enabled = true;
        sneakers.enabled = false;
        selectedShoeType = bootsColors;
    }

    public void ChangeShoesType(TMP_Dropdown change)
    {
        switch (change.value)
        {
            case 0:
                activeShoeType = 0;
                boots.enabled = true;
                sneakers.enabled = false;
                SelectShoeType();
                break;
            case 1:
                activeShoeType = 1;
                boots.enabled = false;
                sneakers.enabled = true;
                SelectShoeType();
                break;
            default:
                Debug.Log("No active shoes");
                break;
        }
    }

    private Material[] selectedShoeType = null;
    public Material[] bootsColors;
    public Material[] sneakersColors;
    private int bootsColorIndex = 0;
    private int sneakersColorIndex = 0;
    private int selectedShoeColorIndex = 0;

    private void SelectShoeType()
    {
        switch (activeShoeType)
        {
            case 0:
                selectedShoeType = bootsColors;
                break;
            case 1:
                selectedShoeType = sneakersColors;
                break;
        }
    }

    public void ChangeShoeColor()
    {
        if (selectedShoeType != null && selectedShoeType.Length > 0)
        {
            switch (activeShoeType)
            {               
                case 0:
                    bootsColorIndex = (bootsColorIndex + 1) % selectedShoeType.Length;
                    boots.material = selectedShoeType[bootsColorIndex];
                    selectedShoeColorIndex = bootsColorIndex;
                    Debug.Log("Boots Color: " + bootsColorIndex);
                    break;
                case 1:
                    sneakersColorIndex = (sneakersColorIndex + 1) % selectedShoeType.Length;
                    sneakers.material = selectedShoeType[sneakersColorIndex];
                    selectedShoeColorIndex = sneakersColorIndex;
                    Debug.Log("Sneakers Color: " + sneakersColorIndex);
                    break;
                default:
                    Debug.LogError("Invalid active shoe type");
                    break;
            }
        }
    }

    public void ChangeShoeColorReverse()
    {
        if (selectedShoeType != null && selectedShoeType.Length > 0 && selectedShoeColorIndex != 0)
        {
            switch (activeShoeType)
            {
                case 0:
                    bootsColorIndex = (bootsColorIndex - 1) % selectedShoeType.Length;
                    boots.material = selectedShoeType[bootsColorIndex];
                    selectedShoeColorIndex = bootsColorIndex;
                    Debug.Log("Boots Color: " + bootsColorIndex);
                    break;
                case 1:
                    sneakersColorIndex = (sneakersColorIndex - 1) % selectedShoeType.Length;
                    sneakers.material = selectedShoeType[sneakersColorIndex];
                    selectedShoeColorIndex = sneakersColorIndex;
                    Debug.Log("Sneakers Color: " + sneakersColorIndex);
                    break;
                default:
                    Debug.LogError("Invalid active shoe type");
                    break;
            }
        }
    }
}
