﻿using System;
using UnityEngine;
using BlazeIL.il2cpp;

public class VRC_StationInternal2 : VRC_StationInternal
{
    public VRC_StationInternal2(IntPtr ptr) : base(ptr) => base.ptr = ptr;

    public static new IL2Type Instance_Class = Assemblies.a[LangTransfer.values[cAssemblies.offset + (long)eAssemblies.assemblycsharp]].GetClass("VRC_StationInternal2");
}
