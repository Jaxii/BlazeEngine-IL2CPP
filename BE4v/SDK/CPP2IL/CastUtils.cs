﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BE4v.SDK.CPP2IL
{
    public static class CastUtils
    {
        unsafe public static IntPtr MonoCast<T>(this T obj) where T : unmanaged => new IntPtr(&obj);
        unsafe public static T1 MonoCast<T1>(this IntPtr ptr)
        {
            if (typeof(T1) == typeof(string))
                return (T1)(object)(new IL2String(ptr).ToString());
            return (T1)typeof(T1).GetConstructors().First(x => x.GetParameters().Length == 1).Invoke(new object[] { ptr });

        }

        internal static IntPtr IL2Typeof(this Type type)
        {
            IL2Class ilType = null;

            ilType = Assembler.list["mscorlib"].GetClass(type.Name, type.Namespace);
            if (ilType == null)
                ilType = (IL2Class)type.GetFields().First(x => x.IsStatic && x.FieldType == typeof(IL2Class)).GetValue(null);

            return IL2Typeof(ilType);
        }

        unsafe public static IntPtr IL2Typeof(this IL2Class type)
        {
            IntPtr ptr = Import.Class.il2cpp_class_get_type(type.ptr);
            if (ptr == IntPtr.Zero)
                return IntPtr.Zero;

            IntPtr result = Import.Class.il2cpp_type_get_object(ptr);
            if (result == IntPtr.Zero)
                return IntPtr.Zero;

            return result;
        }
    }
}
