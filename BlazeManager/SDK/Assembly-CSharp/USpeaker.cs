﻿using System;
using BlazeIL;
using BlazeIL.il2cpp;
using UnityEngine;

public class USpeaker : MonoBehaviour
{
    public USpeaker(IntPtr ptr) : base(ptr) => base.ptr = ptr;

    public bool Mute
    {
        get => Instance_Class.GetField(nameof(Mute)).GetValue(ptr).unbox_Unmanaged<bool>();
        set => Instance_Class.GetField(nameof(Mute)).SetValue(ptr, value.MonoCast());
    }

    public static float LocalGain
    {
        get
        {
            IL2Field field = Instance_Class.GetField(nameof(LocalGain));
            if (field == null)
                (field = Instance_Class.GetField(x => x.Token == 0x4)).Name = nameof(LocalGain);
            IL2Object result = field.GetValue();
            if (result == null)
                return default;
            return result.unbox_Unmanaged<float>();
        }
        set
        {
            IL2Field field = Instance_Class.GetField(nameof(LocalGain));
            if (field == null)
                (field = Instance_Class.GetField(x => x.Token == 0x4)).Name = nameof(LocalGain);
            field?.SetValue(value.MonoCast());
        }
    }

    public static new IL2Type Instance_Class = Assemblies.a["Assembly-CSharp"].GetClass("USpeaker");
}