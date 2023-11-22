using System;
using UnityEngine;

public class ItemGrid : MonoBehaviour
{
    // Constants defining the size of each tile in the grid
    public const float tileSizeWidth = 64f;
    public const float tileSizeHeight = 64f;

    InventoryItem[,] inventoryItemSlot;

    RectTransform rectTransform;

    //defining the size of the grid
    [SerializeField] const int gridSizeWidth = 10;
    [SerializeField] const int gridSizeHeight = 57;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        Init(gridSizeWidth, gridSizeHeight);
    }

    public InventoryItem PickUpItem(int x, int y)
    {
        if (x < gridSizeWidth && y < gridSizeHeight && inventoryItemSlot[x, y] != null)
        {
            InventoryItem toReturn = inventoryItemSlot[x, y];
            CleanGridReference(toReturn);

            return toReturn;
        }
        else
        {
            Debug.LogWarning("Trying to pick up an item outside grid bounds or at an empty position.");
            return null;
        }
    }

    private void CleanGridReference(InventoryItem item)
    {
        for (int ix = 0; ix < item.itemData.width; ix++)
        {
            for (int iy = 0; iy < item.itemData.height; iy++)
            {
                inventoryItemSlot[item.OnGridPositionX + ix, item.OnGridPositionY + iy] = null;
            }
        }
    }

    private void Init(int width, int height)
    {
        inventoryItemSlot = new InventoryItem[width, height];
        Vector2 size = new Vector2(width * tileSizeWidth, height * tileSizeHeight);
        rectTransform.sizeDelta = size;
    }

    internal InventoryItem GetItem(int x, int y)
    {
        return inventoryItemSlot[x, y];
    }

    Vector2 positionOnTheGrid = new Vector2();
    Vector2 tileGridPosition = new Vector2Int();

    public Vector2Int GetTileGridPosition(Vector2 mousePosition)
    {
        positionOnTheGrid.x = mousePosition.x - rectTransform.position.x;
        positionOnTheGrid.y = rectTransform.position.y - mousePosition.y;

        tileGridPosition.x = Mathf.FloorToInt(positionOnTheGrid.x / tileSizeWidth);
        tileGridPosition.y = Mathf.FloorToInt(positionOnTheGrid.y / tileSizeHeight);

        return new Vector2Int((int)tileGridPosition.x, (int)tileGridPosition.y);
    }

    public bool PlaceItem(InventoryItem inventoryItem, int posX, int posY, ref InventoryItem overlapItem)
    {
        if (BoundaryCheck(posX, posY, inventoryItem.itemData.width, inventoryItem.itemData.height) == false)
        {
            return false;
        }

        if (OverlapCheck(posX, posY, inventoryItem.itemData.width, inventoryItem.itemData.height, ref overlapItem) == false)
        {
            overlapItem = null;
            return false;
        }

        if (overlapItem != null)
        {
            CleanGridReference(overlapItem);
        }

        RectTransform rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(this.rectTransform);

        for (int x = 0; x < inventoryItem.itemData.width; x++)
        {
            for (int y = 0; y < inventoryItem.itemData.height; y++)
            {
                // Ensure that the grid position is within the bounds
                if (posX + x < gridSizeWidth && posY + y < gridSizeHeight)
                {
                    // Add the item to the inventoryItemSlot grid
                    inventoryItemSlot[posX + x, posY + y] = inventoryItem;
                }
                else
                {
                    Debug.LogWarning("Item placed outside grid bounds.");
                }
            }
        }

        inventoryItem.OnGridPositionX = posX;
        inventoryItem.OnGridPositionY = posY;
        Vector2 position = CalculatePositionOnGrid(inventoryItem, posX, posY);

        rectTransform.localPosition = position;

        return true;
    }

    public Vector2 CalculatePositionOnGrid(InventoryItem inventoryItem, int posX, int posY)
    {
        Vector2 position = new Vector2();
        position.x = posX * (tileSizeWidth / 2); //* tileSizeWidth + tileSizeWidth / 2; //refer to near 29min mark in tutorial if needing fix
        position.y = -posY * (tileSizeHeight / 2); //* tileSizeHeight + tileSizeHeight / 2);
        return position;
    }

    private bool OverlapCheck(int posX, int posY, int width, int height, ref InventoryItem overlapItem)
    {
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                if (inventoryItemSlot[posX + x, posY + y] != null)
                {
                    if(overlapItem == null)
                    {
                        overlapItem = inventoryItemSlot[posX + x, posY + y];
                    }
                    else
                    {
                        if(overlapItem != inventoryItemSlot[posX + x, posY + y])
                        {
                            return false;
                        }
                    }
                }
            }
        }
        return true;
    }

    bool PositionCheck(int posX, int posY)
    {
        if(posX < 0 || posY < 0)
        {
            return false;
        }

        if(posX >= gridSizeWidth || posY >= gridSizeHeight)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool BoundaryCheck(int posX, int posY, int width, int height)
    {
        if(PositionCheck(posX, posY) == false) { return false; }

        posX += width -1;
        posY += height -1;

        if (PositionCheck(posX, posY) == false) { return false; }

        return true;
    }
}
