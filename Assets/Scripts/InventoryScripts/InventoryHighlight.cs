using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InventoryHighlight : MonoBehaviour
{
    [SerializeField] RectTransform highlighter;

    public void Show(bool b)
    {
        highlighter.gameObject.SetActive(b);

        if (b)
        {
            highlighter.transform.SetAsLastSibling();
        }
    }

    public void SetSize(InventoryItem targetItem)
    {
        Vector2 size = new Vector2();
        size.x = targetItem.itemData.width * ItemGrid.tileSizeWidth / 2;
        size.y = targetItem.itemData.height * ItemGrid.tileSizeHeight / 2;
        highlighter.sizeDelta = size;
    }

    public void SetPosition(ItemGrid targetGrid, InventoryItem targetItem)
    {
        highlighter.SetParent(targetGrid.GetComponent<RectTransform>());

        Vector2 pos = targetGrid.CalculatePositionOnGrid(targetItem, targetItem.OnGridPositionX, targetItem.OnGridPositionY + targetItem.itemData.height);

        highlighter.localPosition = pos;
    }
}
