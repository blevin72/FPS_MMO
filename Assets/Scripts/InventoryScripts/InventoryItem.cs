using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public ItemData itemData; //referencing the ItemData script

    public int OnGridPositionX;
    public int OnGridPositionY;

    internal void Set(ItemData itemData)
    {
        this.itemData = itemData;
        GetComponent<Image>().sprite = itemData.itemIcon;

        Vector2 size = new Vector2();
        size.x = itemData.width * (ItemGrid.tileSizeWidth / 2);
        size.y = itemData.height * (ItemGrid.tileSizeHeight / 2);
        GetComponent<RectTransform>().sizeDelta = size;
    }
}
