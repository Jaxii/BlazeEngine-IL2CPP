﻿using System;
using System.Linq;
using BlazeIL;
using BlazeIL.il2cpp;
using BlazeIL.il2reflection;

namespace UnityEngine
{
    public sealed class Resources
    {
        public static T[] FindObjectsOfTypeAll<T>()
        {
            Object[] objects = FindObjectsOfTypeAll(typeof(T));
            T[] result = new T[objects.Length];
            for (int i = 0; i < objects.Length; i++)
            {
                result[i] = objects[i].MonoCast<T>();
            }
            return result;
        }
        public static Object[] FindObjectsOfTypeAll(Type type)
        {
            IL2TypeObject typeObject = IL2GetType.IL2Typeof(type);
            if (typeObject == null)
                return null;

            IL2Object @object = Instance_Class.GetMethod(nameof(FindObjectsOfTypeAll), x => x.ReturnType.Name == Object.Instance_Class.FullName + "[]").Invoke(new IntPtr[] { typeObject.ptr });
            if (@object == null)
                return new Object[0];

            return @object.UnboxArray<Object>();
        }

        public static IL2Type Instance_Class = Assemblies.a[LangTransfer.values[cAssemblies.offset + (long)eAssemblies.unityenginecoremodule]].GetClass("Resources", "UnityEngine");
    }
}
