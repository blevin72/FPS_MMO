using UnityEngine;

public class ItemGrid : MonoBehaviour
{
    // Constants defining the size of each tile in the grid
    const float tileSizeWidth = 64f;
    const float tileSizeHeight = 64f;

    InventoryItem[,] inventoryItemSlot;

    RectTransform rectTransform;

    //defining the size of the grid
    [SerializeField] int gridSizeWidth = 10;
    [SerializeField] int gridSizeHeight = 57;

    [SerializeField] GameObject inventoryItemPrefab;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        Init(gridSizeWidth, gridSizeHeight);

        // Instantiating an inventory item prefab and placing it at position (1, 1) in the grid
        InventoryItem inventoryItem = Instantiate(inventoryItemPrefab).GetComponent<InventoryItem>();
        PlaceItem(inventoryItem, 0, 0);

        inventoryItem = Instantiate(inventoryItemPrefab).GetComponent<InventoryItem>();
        PlaceItem(inventoryItem, 1, 0);

        inventoryItem = Instantiate(inventoryItemPrefab).GetComponent<InventoryItem>();
        PlaceItem(inventoryItem, 2, 0);
    }

    public InventoryItem PickUpItem(int x, int y)
    {
        InventoryItem toReturn = inventoryItemSlot[x, y];
        inventoryItemSlot[x, y] = null;
        return toReturn;
    }

    private void Init(int width, int height)
    {
        inventoryItemSlot = new InventoryItem[width, height];
        Vector2 size = new Vector2(width * tileSizeWidth, height * tileSizeHeight);
        rectTransform.sizeDelta = size;
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

    public void PlaceItem(InventoryItem inventoryItem, int posX, int posY)
    {
        RectTransform rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(this.rectTransform);
        inventoryItemSlot[posX, posY] = inventoryItem;

        Vector2 position = new Vector2();
        position.x = posX * (tileSizeWidth/2); //* tileSizeWidth + tileSizeWidth / 2; //refer to near 29min mark in tutorial if needing fix
        position.y = -posY * (tileSizeHeight/2); //* tileSizeHeight + tileSizeHeight / 2);

        rectTransform.localPosition = position;
    }
}
