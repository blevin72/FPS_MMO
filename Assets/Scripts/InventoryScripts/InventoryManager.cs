using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject gridTilePrefab; //Reference to the Grid Tile Game object
    public Transform content; // Reference to the Content object in your Scroll View
    public ItemData[] availableItems; //
    public int numberOfTiles = 1000; // Define the number of tiles you want to create

    void Start()
    {
        GenerateTiles();
    }

    void GenerateTiles()
    {
        for (int i = 0; i < numberOfTiles; i++)
        {
            GameObject newItem = Instantiate(gridTilePrefab, content);
        }
    }
}
