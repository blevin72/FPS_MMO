using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTipUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject setting;
    public GameObject settingTP;
    

    public float hoverDelay = 0.5f;
    private bool isHovering = false;

    // Start is called before the first frame update
    void Start()
    {
        settingTP.active = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        Invoke("ShowToolTip", hoverDelay);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        CancelInvoke("ShowToolTip");
        settingTP.active = false;
    }

    private void ShowToolTip()
    {
        settingTP.active = true;
    }
}
