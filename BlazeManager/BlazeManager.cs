﻿using System;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Collections.Generic;
using BlazeTools;
using BlazeTools.Json;
using BlazeIL.il2cpp;
using Addons;
using Addons.Patch;

public class BlazeManager
{
    [HandleProcessCorruptedStateExceptions]
    public static void Start()
    {
        Assemblies.a = new Dictionary<string, IL2Assembly>();
        //# BlazeSDK.Main.LoadModule(Environment.CurrentDirectory + "\\BlazeEngine\\MonoLib\\SharpDisasm.dll", string.Empty, string.Empty, false);
        LoadDefaultSettings();

        Threads.Start();
        #region Patch
        //# patch_UpdateYoutube_dl.Start();
        patch_QuitFix.Start();
        patch_Spoofer.Start();
        patch_AntiBlock.Start();
        patch_AntiKick.Start();
        patch_AvatarSteal.Start();
        patch_AvatarTools.Start();
        patch_EventManager.Start();
        patch_GlobalEvents.Start();
        patch_GlobalDynamicBones.Start();
        patch_InvisAPI.Start();
        patch_MorePortals.Start();
        patch_NoAvatars.Start();
        patch_NoPortal.Start();
        patch_Network.Start();
        patch_QuickMenu.Start();
        // patch_ForceMute.Start();
        // patch_RPC.Start();
        // patch_VipPlates.Start();
        // patch_NoUdonJump.Start();
        //# patch_NoUdon.Start();
        #endregion
        // CoreMask.Start();

        LoadSettings();
    }

    public static void SetIfNullForPlayer(string key, object value)
    {
        if (!settings.ContainsKey(key))
            SetForPlayer(key, value);
    }

    public static void SetForPlayer(string key, object value)
    {
        if (settings.ContainsKey(key))
            settings[key] = new JsonData(value);
        else
            settings.Add(key, new JsonData(value));
    }

    public static T GetForPlayer<T>(string key) => ((JsonData)GetForPlayer(key)).ReadData<T>();
    public static object GetForPlayer(string key)
    {
        if (settings.ContainsKey(key))
            return settings[key];

        return null;
    }

    public static void LoadDefaultSettings(bool ret = true)
    {
        if (ret == false)
            return;
        SetIfNullForPlayer("Fly Type", false);
        SetIfNullForPlayer("AntiKick", true);
        SetIfNullForPlayer("VoiceDotFade", false);
        SetIfNullForPlayer("Photon Serilize", false);
        SetIfNullForPlayer("Force Mute Friend", false);
        SetIfNullForPlayer("More Portals", false);
        SetIfNullForPlayer("Invis API", false);
        SetIfNullForPlayer("No Portal Join", false);
        SetIfNullForPlayer("No Portal Spawn", false);
        SetIfNullForPlayer("Global Events", false);
        SetIfNullForPlayer("AntiBlock", false);
        SetIfNullForPlayer("RPC Block", false);
        SetIfNullForPlayer("NoMove", false);
        SetIfNullForPlayer("Hide Pickup", false);
        SetIfNullForPlayer("Fast Join", false);
        SetIfNullForPlayer("GlobalDynamicBones", false);
        SetIfNullForPlayer("Steam Spoof", true);
        SetIfNullForPlayer("ESP Capsule", false);
        SetIfNullForPlayer("JumpHack", false);
        SetIfNullForPlayer("SpeedHack", false);
        SetForPlayer("Fly Enable", false);
        SetForPlayer("DeathMap", false);
    }

    public static void SaveSettings(bool ret = true)
    {
        if (ret == false)
            return;
        string src = Path.Combine(Environment.CurrentDirectory, "BlazeEngine");
        src += "\\data.json";

        LoadDefaultSettings();
        JsonManager.Create(src, settings);
    }

    public static void LoadSettings(bool ret = true)
    {
        if (ret == false)
            return;
        string src = Path.Combine(Environment.CurrentDirectory, "BlazeEngine");
        src += "\\data.json";
        if (!File.Exists(src))
        {
            SaveSettings();
            ConSole.Print(ConsoleColor.Red, "[Config] Not found!", "Creating file!");
            return;
        }
        settings = JsonManager.Reader(src);
        LoadDefaultSettings();
        ConSole.Print(ConsoleColor.Green, "[Config] Found! File loaded!");
    }

    private static Dictionary<string, JsonData> settings = new Dictionary<string, JsonData>();
}
