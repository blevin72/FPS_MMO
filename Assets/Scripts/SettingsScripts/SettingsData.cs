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
    public string goreViolence;
    public string gameTips;
}
