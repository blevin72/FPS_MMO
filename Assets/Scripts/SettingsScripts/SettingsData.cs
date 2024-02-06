using UnityEngine;
using System;

[Serializable]

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
}
