﻿using BE4v.Patch;
using BE4v.SDK;
using BE4v.SDK.CPP2IL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine;

namespace BE4v.SDK
{
    public static class SDKLoader
    {
        public static string mainDir = Path.Combine(Environment.CurrentDirectory, "BlazeEngine");

        public static void Start()
        {
            if (!Directory.Exists(mainDir))
                Directory.CreateDirectory(mainDir);

            Patch_QuitFix.Start();
        }

        public static void Finish()
        {
            BVault.Load();
        }
    }
}

public static class FileDebug
{
    public static void AddFileDebug(string file, string text)
    {
        try
        {
            using (StreamWriter streamWriter = File.AppendText(file))
            {
                streamWriter.WriteLine(text);
            }
        }
        catch
        {
            Console.WriteLine("Error: 0x00D");
        }
    }
    public static void debugGameObject(string file, GameObject gameObject)
    {
        try
        {
            using (StreamWriter streamWriter = File.CreateText(file))
            {
                streamWriter.WriteLine(debugGetInfoGameObject(gameObject, 0));
            }
        }
        catch
        {
            Console.WriteLine("Error: 0x00D");
        }
    }

    public static string debugGetInfoGameObject(GameObject gameObject, int Index)
    {
        string result = string.Empty;
        if (gameObject == null)
            return result;

        string IndexChar = string.Empty.AddChar('\t', Index);

        result += IndexChar + "{\n";
        IndexChar = string.Empty.AddChar('\t', ++Index);
        result += IndexChar + "\"Name\":\"" + gameObject.name + "\"\n";
        result += IndexChar + "\"Components\":\n";
        result += IndexChar + "{\n";
        IndexChar = string.Empty.AddChar('\t', ++Index);
        foreach (Component component in gameObject.GetComponents<Component>())
        {
            result += IndexChar + "\"" + component.ToString() + "\"\n";
        }
        IndexChar = string.Empty.AddChar('\t', --Index);
        result += IndexChar + "}\n";
        if (gameObject.transform.childCount > 0)
        {
            result += IndexChar + "\"GameObjects\":\n";
            result += IndexChar + "[\n";
            foreach (Transform transform in gameObject.transform)
            {
                result += debugGetInfoGameObject(transform.gameObject, Index + 1);
            }
            result += IndexChar + "]\n";
        }
        IndexChar = string.Empty.AddChar('\t', --Index);
        result += IndexChar + "},\n";
        return result;
    }

    public static string AddChar(this string str, char chr, int Index)
    {
        return str += string.Empty.PadRight(Index, chr);
    }
}
public static class Assembler
{
    private static IntPtr domain;
    private static List<IL2Assembly> listAssemblies = new List<IL2Assembly>();

    private static void Loading()
    {
        domain = Import.Domain.il2cpp_domain_get();

        uint count = 0;
        IntPtr assemblies = Import.Domain.il2cpp_domain_get_assemblies(domain, ref count);
        IntPtr[] assembliesarr = SDKUtils.IntPtrToStructureArray<IntPtr>(assemblies, count);
        foreach (IntPtr assembly in assembliesarr)
            if (assembly != IntPtr.Zero)
                listAssemblies.Add(new IL2Assembly(Import.Assembly.il2cpp_assembly_get_image(assembly)));
    }

    public static IL2Assembly[] GetAssemblies()
    {
        if (listAssemblies.Count == 0)
            Loading();

        return listAssemblies.ToArray();
    }

    public static IL2Assembly GetAssembly(string name)
    {
        if (string.IsNullOrEmpty(name))
            return null;
        IL2Assembly returnval = null;
        foreach (IL2Assembly asm in GetAssemblies())
        {
            if (asm.Name.Equals(name))
            {
                returnval = asm;
                break;
            }
        }
        return returnval;
    }

    private static Dictionary<string, IL2Assembly> getAssemblyList()
    {
        Dictionary<string, IL2Assembly> result = new Dictionary<string, IL2Assembly>();
        foreach(var aName in assemblers)
        {
            result.Add(aName.Key, GetAssembly(aName.Value));
        }
        return result;
    }

    public static Dictionary<string, string> assemblers = new Dictionary<string, string>()
    {
        {  "acs", "Assembly-CSharp" },
        {  "mscorlib", "mscorlib" },
        {  "System.Core", "System.Core" },
        {  "VRCCore-Standalone", "VRCCore-Standalone" },
        {  "UnityEngine.CoreModule", "UnityEngine.CoreModule" },
        {  "UnityEngine.InputLegacyModule", "UnityEngine.InputLegacyModule" },
        {  "UnityEngine.Analytics", "UnityEngine.UnityAnalyticsModule" },
        {  "UnityEngine.AnimationModule", "UnityEngine.AnimationModule" },
        {  "UnityEngine.PhysicsModule", "UnityEngine.PhysicsModule" },
        {  "UnityEngine.UI", "UnityEngine.UI" },
        {  "UnityEngine.IMGUI", "UnityEngine.IMGUIModule" },
        {  "Photon", "Photon-DotNet" },
        {  "VRCSDKBase", "VRCSDKBase" },
        {  "VRCSDK2", "VRCSDK2" },
        {  "VRCSDK3", "VRCSDK3" },
        {  "VRCSDK3A", "VRCSDK3A" },
        {  "VRC.Udon", "VRC.Udon" },
        {  "Steamworks", "Facepunch.Steamworks.Win64" },
        {  "Transmtn", "Transmtn" },
        {  "Unity.TextMeshPro", "Unity.TextMeshPro" }
    };


    public static Dictionary<string, IL2Assembly> list = getAssemblyList();
}
