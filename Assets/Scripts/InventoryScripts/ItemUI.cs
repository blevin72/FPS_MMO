using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Text itemNameText; // Reference to UI text for displaying item name

    private ItemData itemData;

    public void SetItemData(ItemData data)
    {
        itemData = data;
        DisplayItemInfo();
    }

    void DisplayItemInfo()
    {
        if (itemData != null && itemNameText != null)
        {
            itemNameText.text = itemData.itemName;

            // Modify UI or behavior based on itemData.width, itemData.height, etc.
            // For instance, adjust the size or appearance of the ItemUI prefab based on width/height
        }
    }
}
