using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public Sprite itemImage;
    public string itemName;
    public int width = 1;
    public int height = 1;
    public int itemID;
}
