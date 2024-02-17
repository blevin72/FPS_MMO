using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class CharacterDesign : MonoBehaviour
{
    public TMP_Dropdown skin;
    public TMP_Dropdown hair;
    public TMP_Dropdown beard;
    public TMP_Dropdown hat;
    public TMP_Dropdown shirt;
    public TMP_Dropdown outerwear;
    public TMP_Dropdown pants;
    public TMP_Dropdown shoes;
    public TMP_Dropdown glasses;
    public ChangeHair changeHair; //reference to ChangeHair script
    public ChangeBeard changeBeard; //reference to ChangeBeard script
    public ChangeHat changeHat; //reference to ChangeHat script
    public ChangeShirt changeShirt; //reference to ChangeShirt script
    public ChangeOuterwear changeOuterwear; //reference to ChangeOuterwear script
    public ChangePants changePants; //reference to ChangePants script
    public ChangeShoes changeShoes; //reference to ChangeShoes script
    public EquipGlasses equipGlasses; //reference to EquipGlasses script
    public ChangeBackpack changeBackpack; //reference to ChangeBackpack script

    private string characterDesignURL = "http://localhost:8888/sqlconnect/characterDesign.php?action=update";

    private IEnumerator SaveCharacterDesign()
    {
        Debug.Log("CharacterDesign Co-Routine Started");
        // Create a WWWForm to send data to the PHP script
        WWWForm form = new WWWForm();

        //Add to the form
        form.AddField("skin_type", skin.options[skin.value].text);
        form.AddField("hair_type", hair.options[hair.value].text);
        form.AddField("beard_type", beard.options[beard.value].text);
        form.AddField("hat_type", hat.options[hat.value].text);
        form.AddField("shirt_type", shirt.options[shirt.value].text);
        form.AddField("outerwear_type", outerwear.options[outerwear.value].text);
        form.AddField("pants_type", pants.options[pants.value].text);
        form.AddField("shoes_type", shoes.options[shoes.value].text);
        form.AddField("glasses", glasses.options[glasses.value].text);
        form.AddField("backpack_color", changeBackpack.backpack.material.name);
        CheckHairType(form);
        CheckBeardType(form);
        CheckHatType(form);
        CheckShirtType(form);
        CheckOuterwearType(form);
        CheckPantsType(form);
        CheckShoesType(form);
        CheckGlasses(form);

        UnityWebRequest www = UnityWebRequest.Post(characterDesignURL, form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Character design failed. Error: " + www.error);
        }
        else
        {
            string responseText = www.downloadHandler.text;
            if (responseText == "0")
            {
                Debug.Log("Character Design saved successfully");
            }
            else
            {
                Debug.Log("Character Design save failed. Error #" + responseText);
            }
        }
    }

    public void ConfirmCharacterDesignButton()
    {
        StartCoroutine(SaveCharacterDesign());
    }

    private void CheckHairType(WWWForm form)
    {
        // Check which hair type is active and add the corresponding field
        if (changeHair.shortHair.enabled)
        {
            form.AddField("hair_color", changeHair.shortHair.material.name);
        }
        else if (changeHair.mediumHair.enabled)
        {
            form.AddField("hair_color", changeHair.mediumHair.material.name);
        }
        else if (changeHair.longHair.enabled)
        {
            form.AddField("hair_color", changeHair.longHair.material.name);
        }
        else if (changeHair.mediumHairForHats.enabled)
        {
            form.AddField("hair_color", changeHair.mediumHairForHats.material.name);
        }
        else if (changeHair.longHairForHats.enabled)
        {
            form.AddField("hair_color", changeHair.longHairForHats.material.name);
        }
        else
        {
            // Handle the case where no hair type is active
            Debug.Log("No active hair type found.");
        }
    }

    private void CheckBeardType(WWWForm form)
    {
        //Check which beard type is active and add the corresponding field
        if (changeBeard.mustache.enabled)
        {
            form.AddField("beard_color", changeBeard.mustache.material.name);
        }
        else if (changeBeard.goatee.enabled)
        {
            form.AddField("beard_color", changeBeard.goatee.material.name);
        }
        else if (changeBeard.beard.enabled)
        {
            form.AddField("beard_color", changeBeard.beard.material.name);
        }
        else
        {
            Debug.Log("No active facial hair type found");
        }
    }

    private void CheckHatType(WWWForm form)
    {
        //Check which hat type is active and add the corresponding field
        if (changeHat.baseballCap.enabled)
        {
            form.AddField("hat_color", changeHat.baseballCap.material.name);
        }
        else if (changeHat.beanie.enabled)
        {
            form.AddField("hat_color", changeHat.beanie.material.name);
        }
        else if (changeHat.cowboyHat.enabled)
        {
            form.AddField("hat_color", changeHat.cowboyHat.material.name);
        }
        else
        {
            Debug.Log("No active hat type found");
        }
    }

    private void CheckShirtType(WWWForm form)
    {
        if (changeShirt.shirt.enabled)
        {
            form.AddField("shirt_color", changeShirt.shirt.material.name);
        }
        else if (changeShirt.tanktop.enabled)
        {
            form.AddField("shirt_color", changeShirt.tanktop.material.name);
        }
        else
        {
            Debug.Log("No active shirt type found");
        }
    }

    private void CheckOuterwearType(WWWForm form)
    {
        if (changeOuterwear.sweater.enabled)
        {
            form.AddField("outerwear_color", changeOuterwear.sweater.material.name);
        }
        else if (changeOuterwear.windbreaker.enabled)
        {
            form.AddField("outerwear_color", changeOuterwear.windbreaker.material.name);
        }
        else if (changeOuterwear.openShirt.enabled)
        {
            form.AddField("outerwear_color", changeOuterwear.openShirt.material.name);
        }
        else
        {
            Debug.Log("No active outerwear type found");
        }
    }

    private void CheckPantsType(WWWForm form)
    {
        if (changePants.dressPants.enabled)
        {
            form.AddField("pants_color", changePants.dressPants.material.name);
        }
        else if (changePants.shorts.enabled)
        {
            form.AddField("pants_color", changePants.shorts.material.name);
        }
        else if (changePants.joggers.enabled)
        {
            form.AddField("pants_color", changePants.joggers.material.name);
        }
        else
        {
            Debug.Log("No active pants type found");
        }
    }

    private void CheckShoesType(WWWForm form)
    {
        if (changeShoes.boots.enabled)
        {
            form.AddField("shoes_color", changeShoes.boots.material.name);
        }
        else if (changeShoes.sneakers.enabled)
        {
            form.AddField("shoes_color", changeShoes.sneakers.material.name);
        }
        else
        {
            Debug.Log("No active shoes type found");
        }
    }

    private void CheckGlasses(WWWForm form)
    {
        if (equipGlasses.glasses.enabled)
        {
            form.AddField("glasses_color", equipGlasses.glasses.material.name);
        }
        else
        {
            Debug.Log("No active glasses found");
        }
    }
}
