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
        // Create a WWWForm to send data to the PHP script
        WWWForm form = new WWWForm();

        //Add to the form
        form.AddField("characterID", DB_Manager.characterID);
        form.AddField("skin_type", skin.options[skin.value].text);
        form.AddField("hair_type", hair.options[hair.value].text);
        form.AddField("beard_type", beard.options[beard.value].text);
        form.AddField("hat_type", hat.options[hat.value].text);
        form.AddField("shirt_type", shirt.options[shirt.value].text);
        form.AddField("outerwear_type", outerwear.options[outerwear.value].text);
        form.AddField("pants_type", pants.options[pants.value].text);
        form.AddField("shoes_type", shoes.options[shoes.value].text);
        form.AddField("glasses", glasses.options[glasses.value].text);
        CheckHairType(form);
        CheckBeardType(form);
        CheckHatType(form);
        CheckShirtType(form);
        CheckOuterwearType(form);
        CheckPantsType(form);
        CheckShoesType(form);
        CheckGlasses(form);
        ChangeBackpack(form);

        UnityWebRequest www = UnityWebRequest.Post(characterDesignURL, form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Character design failed. Error: " + www.error);
        }
        else
        {
            string responseText = www.downloadHandler.text;
            if (responseText.StartsWith("0"))
            {
                Debug.Log("Character Design saved successfully");
            }
            else
            {
                Debug.Log("Character Design save failed. Error: " + responseText);
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
        // Check which beard type is active and add the corresponding field
        if (changeBeard.mustache.enabled)
        {
            string beardColor = changeBeard.mustache.material.name;
            // Remove the "(Instance)" part
            beardColor = beardColor.Replace(" (Instance)", "");
            Debug.Log("Beard Color: " + beardColor);
            form.AddField("beard_color", beardColor);
        }
        else if (changeBeard.goatee.enabled)
        {
            string beardColor = changeBeard.goatee.material.name;
            beardColor = beardColor.Replace(" (Instance)", "");
            Debug.Log("Beard Color: " + beardColor);
            form.AddField("beard_color", beardColor);
        }
        else if (changeBeard.beard.enabled)
        {
            string beardColor = changeBeard.beard.material.name;
            beardColor = beardColor.Replace(" (Instance)", "");
            Debug.Log("Beard Color: " + beardColor);
            form.AddField("beard_color", beardColor);
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
            string hatColor = changeHat.baseballCap.material.name;
            hatColor = hatColor.Replace(" (Instance)", "");
            Debug.Log("Hat Color: " + hatColor);
            form.AddField("hat_color", hatColor);
        }
        else if (changeHat.beanie.enabled)
        {
            string beanieColor = changeHat.beanie.material.name;
            beanieColor = beanieColor.Replace(" (Instance)", "");
            Debug.Log("Beanie Color: " + beanieColor);
            form.AddField("hat_color", beanieColor);
        }
        else if (changeHat.cowboyHat.enabled)
        {
            string cowboyHatColor = changeHat.cowboyHat.material.name;
            cowboyHatColor = cowboyHatColor.Replace(" (Instance)", "");
            Debug.Log("Cowboy Hat Color: " + cowboyHatColor);
            form.AddField("hat_color", cowboyHatColor);
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
            string shirtColor = changeShirt.shirt.material.name;
            shirtColor = shirtColor.Replace(" (Instance)", "");
            Debug.Log("Shirt Color: " + shirtColor);
            form.AddField("shirt_color", shirtColor);
        }
        else if (changeShirt.tanktop.enabled)
        {
            string tanktopColor = changeShirt.tanktop.material.name;
            tanktopColor = tanktopColor.Replace(" (Instance)", "");
            Debug.Log("Tanktop Color: " + tanktopColor);
            form.AddField("shirt_color", tanktopColor);
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
            string sweaterColor = changeOuterwear.sweater.material.name;
            sweaterColor = sweaterColor.Replace(" (Instance)", "");
            Debug.Log("Sweater Color: " + sweaterColor);
            form.AddField("outerwear_color", sweaterColor);
        }
        else if (changeOuterwear.windbreaker.enabled)
        {
            string windbreakerColor = changeOuterwear.windbreaker.material.name;
            windbreakerColor = windbreakerColor.Replace(" (Instance)", "");
            Debug.Log("Windbreaker Color: " + windbreakerColor);
            form.AddField("outerwear_color", windbreakerColor);
        }
        else if (changeOuterwear.openShirt.enabled)
        {
            string openShirtColor = changeOuterwear.openShirt.material.name;
            openShirtColor = openShirtColor.Replace(" (Instance)", "");
            Debug.Log("Open Shirt Color: " + openShirtColor);
            form.AddField("outerwear_color", openShirtColor);
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
            string pantsColor = changePants.dressPants.material.name;
            pantsColor = pantsColor.Replace(" (Instance)", "");
            Debug.Log("Pants Color: " + pantsColor);
            form.AddField("pants_color", pantsColor);
        }
        else if (changePants.shorts.enabled)
        {
            string shortsColor = changePants.shorts.material.name;
            shortsColor = shortsColor.Replace(" (Instance)", "");
            Debug.Log("Shorts Color: " + shortsColor);
            form.AddField("pants_color", shortsColor);
        }
        else if (changePants.joggers.enabled)
        {
            string joggersColor = changePants.joggers.material.name;
            joggersColor = joggersColor.Replace(" (Instance)", "");
            Debug.Log("Joggers Color: " + joggersColor);
            form.AddField("pants_color", joggersColor);
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
            string bootsColor = changeShoes.boots.material.name;
            bootsColor = bootsColor.Replace(" (Instance)", "");
            Debug.Log("Boots Color: " + bootsColor);
            form.AddField("shoes_color", bootsColor);
        }
        else if (changeShoes.sneakers.enabled)
        {
            string sneakersColor = changeShoes.sneakers.material.name;
            sneakersColor = sneakersColor.Replace(" (Instance)", "");
            Debug.Log("Sneakers Color: " + sneakersColor);
            form.AddField("shoes_color", sneakersColor);
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
            string glassesColor = equipGlasses.glasses.material.name;
            glassesColor = glassesColor.Replace(" (Instance)", "");
            Debug.Log("Glasses Color: " + glassesColor);
            form.AddField("glasses_color", glassesColor);
        }
        else
        {
            Debug.Log("No active glasses found");
        }
    }

    private void ChangeBackpack(WWWForm form)
    {
        string backpackColor = changeBackpack.backpack.material.name;
        backpackColor = backpackColor.Replace(" (Instance)", "");
        Debug.Log("BackpackColor " + backpackColor);
        form.AddField("backpack_color", changeBackpack.backpack.material.name);
    }
}
