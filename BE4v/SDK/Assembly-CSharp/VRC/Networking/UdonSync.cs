﻿using System;
using System.Linq;
using UnityEngine;
using BE4v.SDK.CPP2IL;

namespace VRC.Networking
{
    public class UdonSync : VRCNetworkBehaviour
    {
        public UdonSync(IntPtr ptr) : base(ptr) => base.ptr = ptr;

        public static new IL2Class Instance_Class = Assembler.list["acs"].GetClasses().FindClass_ByMethodName("UdonSyncRunProgramAsRPC");
    }
}