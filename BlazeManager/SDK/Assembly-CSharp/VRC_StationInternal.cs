﻿using System;
using UnityEngine;
using BlazeIL.il2cpp;

public class VRC_StationInternal : MonoBehaviour
{
    public VRC_StationInternal(IntPtr ptr) : base(ptr) => base.ptr = ptr;

    public static new IL2Type Instance_Class = Assemblies.a["Assembly-CSharp"].GetClass("VRC_StationInternal");
}
