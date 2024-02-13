using UnityEngine;
using UnityEngine.UI;

public class CreateCharacter : MonoBehaviour
{
    public Canvas selectCharacterCanvas;
    public Canvas nameCharacterCanvas;
    public Canvas characterCustomizationCanvas;
    public GameObject characterUIs;
    public Button createCharacter;
    public Button characterPanel_1;
    public Button characterPanel_2;
    public Button characterPanel_3;
    public Button characterPanel_4;
    public Image highlighter_1;
    public Image highlighter_2;
    public Image highlighter_3;
    public Image highlighter_4;
    private Image chosenHighlighter;

    private void Start()
    {
        createCharacter.interactable = false;
        highlighter_1.enabled = false;
        highlighter_2.enabled = false;
        highlighter_3.enabled = false;
        highlighter_4.enabled = false;
    }

    public void CreateCharacterButton()
    {
        selectCharacterCanvas.enabled = false;
        nameCharacterCanvas.enabled = true;
        characterCustomizationCanvas.enabled = false;
        characterUIs.gameObject.SetActive(false);
    }

    public void AssignHighligther(Image highlighter)
    {
        chosenHighlighter = highlighter;
    }

    public void AssignCharacterSlot()
    {
        if(createCharacter.interactable == false)
        {
            createCharacter.interactable = true;
            chosenHighlighter.enabled = true;
        }
        else
        {
            createCharacter.interactable = false;
            chosenHighlighter.enabled = false;
        }
    }

    public void ConfirmNameButton()
    {
        nameCharacterCanvas.enabled = false;
        characterCustomizationCanvas.enabled = true;
        characterUIs.gameObject.SetActive(true);
    }
}
