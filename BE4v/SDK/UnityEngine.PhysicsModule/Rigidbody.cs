﻿using System;
using BE4v.SDK;
using BE4v.SDK.CPP2IL;

namespace UnityEngine
{
    public class Rigidbody : Component
    {
        public Rigidbody(IntPtr ptr) : base(ptr) => this.ptr = ptr;

        unsafe public Vector3 velocity
        {
            get => Instance_Class.GetProperty(nameof(velocity)).GetGetMethod().Invoke(ptr).GetValuе<Vector3>();
            set => Instance_Class.GetProperty(nameof(velocity)).GetSetMethod().Invoke(ptr, new IntPtr[] { new IntPtr(&value) });
        }

        unsafe public Vector3 angularVelocity
        {
            get => Instance_Class.GetProperty(nameof(angularVelocity)).GetGetMethod().Invoke(ptr).GetValuе<Vector3>();
            set => Instance_Class.GetProperty(nameof(angularVelocity)).GetSetMethod().Invoke(ptr, new IntPtr[] { new IntPtr(&value) });
        }

        unsafe public bool isKinematic
        {
            get => Instance_Class.GetProperty(nameof(isKinematic)).GetGetMethod().Invoke(ptr).GetValuе<bool>();
            set => Instance_Class.GetProperty(nameof(isKinematic)).GetSetMethod().Invoke(ptr, new IntPtr[] { new IntPtr(&value) });
        }

        unsafe public bool useGravity
        {
            get => Instance_Class.GetProperty(nameof(useGravity)).GetGetMethod().Invoke(ptr).GetValuе<bool>();
            set => Instance_Class.GetProperty(nameof(useGravity)).GetSetMethod().Invoke(ptr, new IntPtr[] { new IntPtr(&value) });
        }

        public static new IL2Class Instance_Class = Assembler.list["UnityEngine.PhysicsModule"].GetClass("Rigidbody", "UnityEngine");
    }
}
