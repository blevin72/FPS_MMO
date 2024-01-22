using UnityEngine;
using TMPro;

public class ChangeBeard : MonoBehaviour
{
    public Renderer mustache;
    public Renderer goatee;
    public Renderer beard;

    public void Start()
    {
        mustache.enabled = false;
        goatee.enabled = false;
        beard.enabled = false;
    }

    public void ChangeBeardType(TMP_Dropdown change)
    {
        switch (change.value)
        {
            case 0:
                mustache.enabled = false;
                goatee.enabled = false;
                beard.enabled = false;
                break;
            case 1:
                mustache.enabled = true;
                goatee.enabled = false;
                beard.enabled = false;
                break;
            case 2:
                mustache.enabled = false;
                goatee.enabled = true;
                beard.enabled = false;
                break;
            case 3:
                mustache.enabled = false;
                goatee.enabled = false;
                beard.enabled = true;
                break;
            default:
                Debug.Log("Invalid Dropdown option");
                break;
        }
    }


    public Material[] beardColor;
    private int beardColorIndex = 0;

    public void ChangeBeardColor()
    {
        if (beardColorIndex < beardColor.Length - 1)
        {
            beardColorIndex++;
        }
        else
        {
            beardColorIndex = 0;
        }
        mustache.material = beardColor[beardColorIndex];
        goatee.material = beardColor[beardColorIndex];
        beard.material = beardColor[beardColorIndex];
        Debug.Log("Hair Color: " + beardColorIndex);
    }

    public void ChangeBeardColorReverse()
    {
        if (beardColorIndex < beardColor.Length - 1 & beardColorIndex != 0)
        {
            beardColorIndex--;
        }
        else
        {
            beardColorIndex = 0;
        }
        mustache.material = beardColor[beardColorIndex];
        goatee.material = beardColor[beardColorIndex];
        beard.material = beardColor[beardColorIndex];
        Debug.Log("Hair Color: " + beardColorIndex);
    }
}
