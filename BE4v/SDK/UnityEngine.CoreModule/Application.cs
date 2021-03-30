using System;
using BE4v.SDK.CPP2IL;

namespace UnityEngine
{
    public static class Application
    {
        public static int targetFrameRate
        {
            get => Instance_Class.GetProperty(nameof(targetFrameRate)).GetGetMethod().Invoke().GetValu�<int>();
            set => Instance_Class.GetProperty(nameof(targetFrameRate)).GetSetMethod().Invoke(new IntPtr[] { value.MonoCast() });
        }

        public static string unityVersion
        {
            get => Instance_Class.GetProperty(nameof(unityVersion)).GetGetMethod().Invoke()?.GetValue<string>();
        }

        public static string streamingAssetsPath
        {
            get => Instance_Class.GetProperty(nameof(streamingAssetsPath)).GetGetMethod().Invoke()?.GetValue<string>();
        }

        public static IL2Class Instance_Class = Assembler.list["UnityEngine.CoreModule"].GetClass("Application", "UnityEngine");
    }
}
