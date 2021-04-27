﻿using System;
using System.Linq;
using UnityEngine;
using BE4v.SDK.CPP2IL;

public class CC_Base : MonoBehaviour
{
    public CC_Base(IntPtr ptr) : base(ptr) => base.ptr = ptr;

	public static new IL2Class Instance_Class = Assembler.list["acs"].GetClass(CC_Glitch.Instance_Class.BaseType.FullName);
}