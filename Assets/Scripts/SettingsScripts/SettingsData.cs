using UnityEngine;
using System;

[Serializable]

//variables need to match column name in database
public class SettingsData : MonoBehaviour
{
    //Audio Setting Script
    public string master_volume;
    public string music_volume;
    public string sound_effects;
    public string dialog_voice;
    public string proximity_chat;
    public string subtitles;
    public string ui_sound_fx;

    //Visual Settings Script
    public string contrast;
    public string brightness;
    public string shadows;
    public string anti_aliasing;
    public string color_blind;
    public string resolution;
    public string graphics;
    public string textures;
    public string shaders;
    public string screen;
    public string aspect_ratios;

    //Social Settings Script
    public string messaging;
    public string chat_profanity;
    public string party_chat;
    public string notifications;
    public string online_status;

    //Preferences Settings Script
    public string the_division;
    public string HUD_theme;
    public string HUD_location;
    public string languages;
    //public string server; SERVER NOT SET UP IN UI YET
    public string camera_sensitivity;
    public string camera_sway;
    public string HUD_transparency;
    public string gamertags;
    public string gore;
    public string game_tips;

    //Account Settings Script
    public string friend_requests;
    public string outpost_requests;
    public string assistance_requests;
    public string messages_everyone;
    public string messages_friends;
    public string messages_outpost;
    public string emails_all;
    public string emails_seasonEvents;
    public string emails_specialEvents;
    public string emails_newsletters;

    //Survivor Ranking Script
    public string level;
    public string exp;
    public string outpost_ranking;
    public string strength;
    public string dexterity;
    public string intellect;
    public string endurance;
    public string charm;
    public string stealth;
    public string total_health;
    public string total_stamina;
    public string total_protection;
    public string total_progression;
    public string skill_points;
    public string character_name;

    //Item Description Script & Gear Bonuses Script
    public string gearName;
    public string gearWeight;
    public string protectionModifier;
    public string immunityModifier;
    public string speedModifier;
    public string agroModifier;
    public string staminaModifier;
    public string meleeModifier;
    public string craftModifier;
    public string illuminateFOV;
    public string batteryLifeMin;
    public string rarity;
    public string durability;
    public string itemName;
    public string itemDescription;
    public string itemProperties_1;
    public string itemProperties_2;

    //Equipped Gear Script
    public string totalWeight;

    //Gear Bonuses Script
    public string protection;
    public string meleeBonus;
    public string speedBonus;
    public string craftBonus;
    public string staminaBonus;
    public string immunityBonus;
    public string agroBonus;
}
