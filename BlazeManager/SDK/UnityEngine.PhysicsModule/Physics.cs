﻿using System;
using System.Linq;
using BlazeIL;
using BlazeIL.il2cpp;

namespace UnityEngine
{
    public static class Physics
    {
        public static Vector3 gravity
        {
            get => Instance_Class.GetProperty(nameof(gravity)).GetGetMethod().Invoke().unbox_Unmanaged<Vector3>();
            set => Instance_Class.GetProperty(nameof(gravity)).GetSetMethod().Invoke(new IntPtr[] { value.MonoCast() });
        }

        private static IL2Method RayCastMini = null;
        public static bool Raycast(Ray ray, out RaycastHit hitInfo)
        {
            if(RayCastMini == null)
            {
                RayCastMini = Instance_Class.GetMethods()
                    .Where(x => x.Name == "Raycast" && x.GetParameters().Length == 2)
                    .First(x => IL2Import.il2cpp_type_get_name(x.GetParameters()[1].ptr) == "UnityEngine.RaycastHit&");

                if (RayCastMini == null)
                {
                    hitInfo = new RaycastHit();
                    return default;
                }
            }

            unsafe
            {
                fixed (RaycastHit* hitInfoPtr = &hitInfo)
                {
                    return RayCastMini.Invoke(new IntPtr[] { ray.MonoCast(), new IntPtr(hitInfoPtr) }).Unbox<bool>();
                }
            }
        }

        public static IL2Type Instance_Class = Assemblies.a["UnityEngine.PhysicsModule"].GetClass("Physics", "UnityEngine");
    }
}
