using UnityEngine;
using TMPro;

public class ChangeSkinTone : MonoBehaviour
{
    //change skin tone methods
    #region
    public Renderer wholeBody;
    public Material[] cleanSkin;
    public Material[] dirtySkin;
    private Material[] selectedSkinType = null;
    private int cleanSkinIndex = 0;
    private int dirtySkinIndex = 0;
    private int activeSkinType = 0;
    private int selectedSkinColorIndex = 0;


    private void Start()
    {
        selectedSkinType = cleanSkin;
    }

    public void ChangeSkinTypes(TMP_Dropdown change)
    {
        switch (change.value)
        {
            case 0:
                activeSkinType = 0;
                cleanSkinIndex = dirtySkinIndex;
                headColorIndex = dirtySkinIndex;
                SelectSkinType();
                ChangeSkins();
                break;
            case 1:
                activeSkinType = 1;
                dirtySkinIndex = cleanSkinIndex;
                headColorIndex = cleanSkinIndex;
                SelectSkinType();
                ChangeSkins();
                break;
        }
    }

    private void SelectSkinType() 
    {
        switch (activeSkinType)
        {
            case 0:
                selectedSkinType = cleanSkin;
                break;
            case 1:
                selectedSkinType = dirtySkin;
                break;
        }
    }

    public void ChangeSkins()
    {
        if (selectedSkinType != null && selectedSkinType.Length > 0)
        {
            switch (activeSkinType)
            {
                case 0:
                    cleanSkinIndex = (cleanSkinIndex + 1) % selectedSkinType.Length;
                    wholeBody.material = selectedSkinType[cleanSkinIndex];
                    selectedSkinColorIndex = cleanSkinIndex;
                    Debug.Log("Skin Tone: " + cleanSkinIndex);
                    ChangeHeadColor();
                    break;
                case 1:
                    dirtySkinIndex = (dirtySkinIndex + 1) % selectedSkinType.Length;
                    wholeBody.material = selectedSkinType[dirtySkinIndex];
                    selectedSkinColorIndex = dirtySkinIndex;
                    Debug.Log("Skin Tone: " + dirtySkinIndex);
                    ChangeHeadColor();
                    break;
                default:
                    Debug.LogError("Invalid skin type");
                    break;
            }
        }
    }

    public void ChangeSkinsReverse()
    {
        if (selectedSkinType != null && selectedSkinType.Length > 0 && selectedSkinColorIndex != 0)
        {
            switch (activeSkinType)
            {
                case 0:
                    cleanSkinIndex = (cleanSkinIndex - 1) % selectedSkinType.Length;
                    wholeBody.material = selectedSkinType[cleanSkinIndex];
                    selectedSkinColorIndex = cleanSkinIndex;
                    Debug.Log("Skin Tone: " + cleanSkinIndex);
                    ChangeHeadColorReverse();
                    break;
                case 1:
                    dirtySkinIndex = (dirtySkinIndex - 1) % selectedSkinType.Length;
                    wholeBody.material = selectedSkinType[dirtySkinIndex];
                    selectedSkinColorIndex = dirtySkinIndex;
                    Debug.Log("Skin Tone: " + dirtySkinIndex);
                    ChangeHeadColorReverse();
                    break;
                default:
                    Debug.LogError("Invalid skin type");
                    break;
            }
        }
    }
    #endregion

    //change head colormethods
    #region
    [SerializeField] SkinnedMeshRenderer head;
    [SerializeField] Material[] headColor;
    private int headColorIndex = 0;

    private void ChangeHeadColor()
    {
        if (headColorIndex < headColor.Length - 1)
        {
            headColorIndex++;
        }
        else
        {
            headColorIndex = 0;
        }

        // Ensure the head renderer has enough materials
        if (head.materials != null && head.materials.Length > 4 && headColor.Length > headColorIndex && headColor[headColorIndex] != null)
        {
            // Clone the existing materials array to modify it
            Material[] newMaterials = head.materials.Clone() as Material[];

            // Change the material at index 4 of the cloned array
            newMaterials[4] = headColor[headColorIndex];

            // Assign the modified array back to head.materials
            head.materials = newMaterials;

            Debug.Log("Head Color:" + headColorIndex);
        }
        else
        {
            Debug.LogError("Invalid index or material. Check head renderer and headColor array.");
        }
    }

    //selectedSkinType != null && selectedSkinType.Length > 0 && selectedSkinColorIndex != 0

    private void ChangeHeadColorReverse()
    {
        if (headColorIndex < headColor.Length - 1 && headColorIndex > 0 && headColorIndex != 0)
        {
            headColorIndex--;
        }
        else
        {
            headColorIndex = 0;
        }

        // Ensure the head renderer has enough materials
        if (head.materials != null && head.materials.Length > 4 && headColor.Length > headColorIndex && headColor[headColorIndex] != null)
        {
            // Clone the existing materials array to modify it
            Material[] newMaterials = head.materials.Clone() as Material[];

            // Change the material at index 4 of the cloned array
            newMaterials[4] = headColor[headColorIndex];

            // Assign the modified array back to head.materials
            head.materials = newMaterials;

            Debug.Log("Head Color:" + headColorIndex);
        }
        else
        {
            Debug.LogError("Invalid index or material. Check head renderer and headColor array.");
        }
    }
    #endregion
}
