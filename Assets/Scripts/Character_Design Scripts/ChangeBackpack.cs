using UnityEngine;

public class ChangeBackpack : MonoBehaviour
{
    public Renderer backpack;
    public Material[] backpackColors;
    private int backpackColorIndex = 0;

    private void Start()
    {
        backpack.enabled = true;
    }

    public void ChangeBackpackColor()
    {
        if(backpackColorIndex < backpackColors.Length - 1)
        {
            backpackColorIndex++;
            Debug.Log("Backpack Color is:" + backpackColorIndex);
        }
        else
        {
            backpackColorIndex = 0;
        }

        backpack.material = backpackColors[backpackColorIndex];
    }

    public void ChangeBackpackColorReverse()
    {
        if (backpackColorIndex < backpackColors.Length - 1 && backpackColorIndex != 0)
        {
            backpackColorIndex--;
            Debug.Log("Backpack Color is:" + backpackColorIndex);
        }
        else
        {
            backpackColorIndex = 0;
        }

        backpack.material = backpackColors[backpackColorIndex];
    }
}
