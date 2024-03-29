﻿using System;
using System.Linq;
using UnityEngine;
using BE4v.SDK.CPP2IL;

public class VRC_PlayerAudioOverrideInternal : VRCNetworkBehaviour
{
    public VRC_PlayerAudioOverrideInternal(IntPtr ptr) : base(ptr) => base.ptr = ptr;

    public static new IL2Class Instance_Class = Assembler.list["acs"].GetClasses().FindClass_ByMethodName("InternalApplyOverrideRPC");
}