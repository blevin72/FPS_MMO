using UnityEngine;

public class ChangeSkinTone : MonoBehaviour
{
    //change skin tone methods
    #region
    public Renderer wholeBody;
    public Material[] cleanSkin;
    public Material[] dirtySkin;
    private int cleanSkinIndex = 0;

    public void ChangeSkins()
    {
        if(cleanSkinIndex < cleanSkin.Length - 1)
        {
            cleanSkinIndex++;
        }
        else
        {
            cleanSkinIndex = 0; //Reset to the first material
        }
        if(cleanSkin.Length > 0)
        {
            wholeBody.material = cleanSkin[cleanSkinIndex];
            Debug.Log("Skin Tone:" + cleanSkinIndex);

            ChangeHeadColor();
        }
    }

    public void ChangeSkinsReverse()
    {
        if (cleanSkinIndex < cleanSkin.Length - 1 && cleanSkinIndex != 0)
        {
            cleanSkinIndex--;
        }
        else
        {
            cleanSkinIndex = 0; //Reset to the first material
        }
        if (cleanSkin.Length > 0)
        {
            wholeBody.material = cleanSkin[cleanSkinIndex];
            Debug.Log("Skin Tone:" + cleanSkinIndex);

            ChangeHeadColorReverse();
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

    private void ChangeHeadColorReverse()
    {
        if (headColorIndex < headColor.Length - 1 && headColorIndex != 0)
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
