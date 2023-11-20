using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [HideInInspector]
    public ItemGrid selectedItemGrid;

    InventoryItem selectedItem;
    RectTransform rectTransform;

    private void Update()
    {
        if(selectedItem != null)
        {
            rectTransform.position = Input.mousePosition;
        }

        if (selectedItemGrid == null) { return; } //return the mouse position (x,y) if the mouse is in the grid
                                                 
            if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log(selectedItemGrid.GetTileGridPosition(Input.mousePosition));
                Vector2Int tileGridPosition = selectedItemGrid.GetTileGridPosition(Input.mousePosition);
                if(selectedItem == null)
                {
                    selectedItem = selectedItemGrid.PickUpItem(tileGridPosition.x, tileGridPosition.y);
                    if(selectedItem != null)
                    {
                        rectTransform = selectedItem.GetComponent<RectTransform>();
                    }
                }
                else
                {
                    selectedItemGrid.PlaceItem(selectedItem, tileGridPosition.x, tileGridPosition.y);
                    selectedItem = null;
                }
            }
    }
}
