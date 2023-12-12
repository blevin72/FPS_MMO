using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject inventoryDescription;

    // Start is called before the first frame update
    void Start()
    {
        inventoryDescription.active = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //check to see if an item has been assigned, if true, show inventoryDescription panel
        Image image = GetComponent<Image>();
        if(image != null && image.sprite != null)
        {
            inventoryDescription.active = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        inventoryDescription.active = false;
    }
}
