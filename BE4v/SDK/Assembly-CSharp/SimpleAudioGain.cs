﻿using System;
using System.Linq;
using UnityEngine;
using BE4v.SDK.CPP2IL;

public class SimpleAudioGain : MonoBehaviour
{
    public SimpleAudioGain(IntPtr ptr) : base(ptr) => base.ptr = ptr;

    public static new IL2Class Instance_Class = Assembler.list["acs"].GetClasses().FirstOrDefault(x => x.GetMethod("OnAudioFilterRead") != null && x.GetField(y => y.ReturnType.Name == "UnityEngine.AudioSource") != null);
}