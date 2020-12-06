﻿using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using BlazeIL.il2cpp;

public class GamelikeInputController : LocomotionInputController
{
    public GamelikeInputController(IntPtr ptr) : base(ptr) => base.ptr = ptr;

    public static new IL2Type Instance_Class = Assemblies.a["Assembly-CSharp"].GetClass("GamelikeInputController");
}