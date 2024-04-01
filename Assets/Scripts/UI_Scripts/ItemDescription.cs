using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using TMPro;
using Newtonsoft.Json;

public class ItemDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject inventoryDescription;
    public RawImage gearImage;//may need to change this to Image
    public Image gearType;
    public TextMeshProUGUI gearName;
    public TextMeshProUGUI gearWeight;
    public TextMeshProUGUI gearProtectionModifier;
    public TextMeshProUGUI gearSpeedModifier;
    public TextMeshProUGUI gearImmunityModifier;
    public TextMeshProUGUI gearAgroModifier;
    public TextMeshProUGUI gearStaminaModifier;
    public TextMeshProUGUI gearMeleeModifier;
    public TextMeshProUGUI gearCraftModifier;
    public TextMeshProUGUI illuminateFOV;
    public TextMeshProUGUI batteryLife;
    public TextMeshProUGUI gearRarity;
    public TextMeshProUGUI gearDurability;
    public TextMeshProUGUI itemDescription;
    public TextMeshProUGUI itemProperties_1;
    public TextMeshProUGUI itemProperties_2;
    public TextMeshProUGUI[] gearStats;
    //public TextMeshProUGUI gearStat_5;
    //public TextMeshProUGUI gearStat_6;

    public LoadSurvivor loadSurvivor; //reference to LoadSurvivor class (needed for SetTextValue())

    //PHP URL's
    private string loadItemDetailsURL = "http://localhost:8888/sqlconnect/gearDescription.php?action=fetch_details";

    // Start is called before the first frame update
    void Start()
    {
        inventoryDescription.active = false;
    }

    private IEnumerator LoadGearDetails()
    {
        string gearTypeName = gearType.name;
        string getRequestURL = loadItemDetailsURL + "&gearName=" + DB_Manager.gearName + "&gearTypeName=" + gearTypeName;

        UnityWebRequest www = UnityWebRequest.Get(getRequestURL);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string responseText = www.downloadHandler.text;

            // Deserialize JSON to SettingsData
            SettingsData settingsData = JsonConvert.DeserializeObject<SettingsData>(responseText);

            if(gearType.name == "Torso" || gearType.name == "Legs")
            {
                loadSurvivor.SetTextValue(gearName, settingsData.gearName);
                loadSurvivor.SetTextValue(gearWeight, "Weight: " + settingsData.gearWeight + "KG");
                loadSurvivor.SetTextValue(gearProtectionModifier, "Protection: +" + settingsData.protectionModifier);
                loadSurvivor.SetTextValue(gearSpeedModifier, "Speed: +" + settingsData.speedModifier);
                loadSurvivor.SetTextValue(gearRarity, "Rarity: " + settingsData.rarity);
                loadSurvivor.SetTextValue(gearDurability, "Durability: " + settingsData.durability);

                Debug.Log("GearName: " + gearName);

                //Handle binary data separately
                //byte[] imageData = www.downloadHandler.data;
                //Texture2D texture = new Texture2D(1, 1);
                //texture.LoadImage(imageData);
                //gearImage.texture = texture;
            }
            else if(gearType.name == "Head")
            {
                loadSurvivor.SetTextValue(gearName, settingsData.gearName);
                loadSurvivor.SetTextValue(gearWeight, "Weight: " + settingsData.gearWeight + "KG");
                loadSurvivor.SetTextValue(gearProtectionModifier, "Protection: +" + settingsData.protectionModifier);
                loadSurvivor.SetTextValue(gearImmunityModifier, "Immunity: +" + settingsData.immunityModifier);
                loadSurvivor.SetTextValue(gearRarity, "Rarity: " + settingsData.rarity);
                loadSurvivor.SetTextValue(gearDurability, "Durability: " + settingsData.durability);

                Debug.Log("GearName: " + gearName);

                //Handle binary data separately
                //byte[] imageData = www.downloadHandler.data;
                //Texture2D texture = new Texture2D(1, 1);
                //texture.LoadImage(imageData);
                //gearImage.texture = texture;
            }
            else if(gearType.name == "Hands")
            {
                loadSurvivor.SetTextValue(gearName, settingsData.gearName);
                loadSurvivor.SetTextValue(gearWeight, "Weight: " + settingsData.gearWeight + "KG");
                loadSurvivor.SetTextValue(gearProtectionModifier, "Protection: +" + settingsData.protectionModifier);
                loadSurvivor.SetTextValue(gearMeleeModifier, "Melee: +" + settingsData.meleeModifier);
                loadSurvivor.SetTextValue(gearCraftModifier, "Craft: +" + settingsData.craftModifier);
                loadSurvivor.SetTextValue(gearRarity, "Rarity: " + settingsData.rarity);
                loadSurvivor.SetTextValue(gearDurability, "Durability: " + settingsData.durability);

                //Handle binary data separately
                //byte[] imageData = www.downloadHandler.data;
                //Texture2D texture = new Texture2D(1, 1);
                //texture.LoadImage(imageData);
                //gearImage.texture = texture;
            }
            else if (gearType.name == "Shoulder")
            {
                loadSurvivor.SetTextValue(gearName, settingsData.gearName);
                loadSurvivor.SetTextValue(gearWeight, "Weight: " + settingsData.gearWeight + "KG");
                loadSurvivor.SetTextValue(illuminateFOV, "Illuminate FOV: " + settingsData.illuminateFOV);
                loadSurvivor.SetTextValue(batteryLife, "Battery Life: " + settingsData.batteryLifeMin);
                loadSurvivor.SetTextValue(gearRarity, "Rarity: " + settingsData.rarity);

                //Handle binary data separately
                //byte[] imageData = www.downloadHandler.data;
                //Texture2D texture = new Texture2D(1, 1);
                //texture.LoadImage(imageData);
                //gearImage.texture = texture;
            }
            else if(gearType.name == "Feet")
            {
                loadSurvivor.SetTextValue(gearName, settingsData.gearName);
                loadSurvivor.SetTextValue(gearWeight, "Weight: " + settingsData.gearWeight + "KG");
                loadSurvivor.SetTextValue(gearProtectionModifier, "Protection: +" + settingsData.protectionModifier);
                loadSurvivor.SetTextValue(gearAgroModifier, "Agro: +" + settingsData.agroModifier);
                loadSurvivor.SetTextValue(gearSpeedModifier, "Speed: +" + settingsData.speedModifier);
                loadSurvivor.SetTextValue(gearStaminaModifier, "Stamina: +" + settingsData.staminaModifier);
                loadSurvivor.SetTextValue(gearRarity, "Rarity: " + settingsData.rarity);
                loadSurvivor.SetTextValue(gearDurability, "Durability: " + settingsData.durability);

                //Handle binary data separately
                //byte[] imageData = www.downloadHandler.data;
                //Texture2D texture = new Texture2D(1, 1);
                //texture.LoadImage(imageData);
                //gearImage.texture = texture;
            }
            else if(gearType.name == "Item_Slot")
            {
                loadSurvivor.SetTextValue(gearName, settingsData.itemName);
                loadSurvivor.SetTextValue(itemDescription, settingsData.itemDescription);
                loadSurvivor.SetTextValue(itemProperties_1, settingsData.itemProperties_1);
                loadSurvivor.SetTextValue(itemProperties_2, settingsData.itemProperties_2);
                loadSurvivor.SetTextValue(gearRarity, settingsData.rarity);

                //Handle binary data separately
                //byte[] imageData = www.downloadHandler.data;
                //Texture2D texture = new Texture2D(1, 1);
                //texture.LoadImage(imageData);
                //gearImage.texture = texture;
            }
        }
        else
        {
            Debug.LogError("UnityWebRequest failed: " + www.error);
        }
    }

    //reset Gear Description Panel (needed to make sure overlapping stats don't stay on the panel when switching btwn items)
    private void ResetGearDescriptionPanel()
    {
        foreach(TextMeshProUGUI gearStat in gearStats)
        {
            gearStat.text = "";
        }
    }

    //check to see if an item has been assigned, if true, show inventoryDescription panel and call LoadGearDetails();
    public void OnPointerEnter(PointerEventData eventData)
    {
        ResetGearDescriptionPanel();

        // Check if an item has been assigned
        Image image = GetComponentInChildren<Image>();

        // Find the child GameObject by name
        Transform child = transform.Find("Item_Slot");

        if (child != null)
        {
            // Get the Image component of the child GameObject
            Image childImage = child.GetComponent<Image>();

            if (childImage != null && childImage.sprite != null)
            {
                // Get the name of the sprite of the child Image component
                string spriteName = childImage.sprite.name;

                // Set the gear name to the sprite name
                DB_Manager.gearName = spriteName;

                // Log the gear name for debugging
                Debug.Log("Gear Name: " + DB_Manager.gearName);

                // Activate the inventoryDescription panel
                inventoryDescription.SetActive(true);

                // Load gear details asynchronously
                StartCoroutine(LoadGearDetails());
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        inventoryDescription.active = false;
    }
}
