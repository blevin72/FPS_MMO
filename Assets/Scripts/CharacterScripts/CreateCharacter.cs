using UnityEngine;
using UnityEngine.UI;

public class CreateCharacter : MonoBehaviour
{
    public Canvas selectCharacterCanvas;
    public Canvas characterCustomizationCanvas;
    public GameObject characterUIs;

    public void CreateCharacterButton()
    {
        selectCharacterCanvas.enabled = false;
        characterCustomizationCanvas.enabled = true;
        characterUIs.gameObject.SetActive(true);
    }
}
