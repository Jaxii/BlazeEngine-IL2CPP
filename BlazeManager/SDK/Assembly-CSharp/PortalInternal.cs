﻿using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using BlazeIL;
using BlazeIL.il2cpp;

public class PortalInternal : MonoBehaviour
{
    public PortalInternal(IntPtr ptr) : base(ptr) => base.ptr = ptr;

    public void SetTimerRPC(float timer, VRC.Player player)
    {
        Instance_Class.GetMethod(nameof(SetTimerRPC)).Invoke(ptr, new IntPtr[] { timer.MonoCast(), player.ptr });
    }

    public static new IL2Type Instance_Class = Assemblies.a[LangTransfer.values[cAssemblies.offset + (long)eAssemblies.assemblycsharp]].GetClasses().FirstOrDefault(x => x.GetMethod("SetTimerRPC") != null);
}
