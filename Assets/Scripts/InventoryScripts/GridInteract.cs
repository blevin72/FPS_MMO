using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ItemGrid))]
public class GridInteract : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    InventoryController inventoryController;
    ItemGrid itemGrid;

    private void Awake()
    {
        inventoryController = FindObjectOfType(typeof(InventoryController)) as InventoryController;
        itemGrid = GetComponent<ItemGrid>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        inventoryController.selectedItemGrid = itemGrid;
        Debug.Log("Pointer enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        inventoryController.selectedItemGrid = null;
        Debug.Log("Pointer exit");
    }
}
