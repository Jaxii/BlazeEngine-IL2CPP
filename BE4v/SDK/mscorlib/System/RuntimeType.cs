﻿using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using BE4v.SDK.CPP2IL;
using BE4v.SDK;

namespace System
{
    public class RuntimeType : IL2Base
    {
        public RuntimeType(IntPtr ptr) : base(ptr) => base.ptr = ptr;

        public IntPtr MakeGenericType(Type[] types) => MakeGenericType(types.Select(x => x.IL2Typeof()).ToArray());
        public IntPtr MakeGenericType(IntPtr[] intPtrs)
        {
            return Import.Class.il2cpp_class_from_system_type(Instance_Class.GetMethod(nameof(MakeGenericType)).Invoke(new IL2Class(ptr).IL2Typeof(), new IntPtr[] { intPtrs.ArrayToIntPtr(Assembler.list["mscorlib"].GetClass("Type", "System")) }).ptr);
        }

        public static IL2Class Instance_Class = Assembler.list["mscorlib"].GetClass("RuntimeType", "System");
    }
}
