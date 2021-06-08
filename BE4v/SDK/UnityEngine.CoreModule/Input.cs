using System;
using BE4v.SDK;
using BE4v.SDK.CPP2IL;

namespace UnityEngine
{
    public static class Input
    {
        public static float GetAxis(string axisName) => Instance_Class.GetMethod(nameof(GetAxis)).Invoke(new IntPtr[] { new IL2String(axisName).ptr }).GetValu�<float>();
        unsafe public static bool GetKey(KeyCode key) => Instance_Class.GetMethod("GetKeyInt").Invoke(new IntPtr[] { new IntPtr(&key) })?.GetValu�<bool>() ?? false;
        unsafe public static bool GetKeyDown(KeyCode key) => Instance_Class.GetMethod("GetKeyDownInt").Invoke(new IntPtr[] { new IntPtr(&key) })?.GetValu�<bool>() ?? false;
        unsafe public static bool GetKeyUp(KeyCode key) => Instance_Class.GetMethod("GetKeyUpInt").Invoke(new IntPtr[] { new IntPtr(&key) })?.GetValu�<bool>() ?? false;
        public static bool GetButtonDown(string buttonName) => Instance_Class.GetMethod(nameof(GetButtonDown)).Invoke(new IntPtr[] { new IL2String(buttonName).ptr }).GetValu�<bool>();

        public static Vector3 mousePosition
        {
            get => Instance_Class.GetProperty(nameof(mousePosition)).GetGetMethod().Invoke().GetValu�<Vector3>();
        }

        public static IL2Class Instance_Class = Assembler.list["UnityEngine.CoreModule"].GetClass("Input", "UnityEngine");
    }
}
