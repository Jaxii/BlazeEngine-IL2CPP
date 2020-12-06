﻿using System;
using BlazeIL;
using BlazeIL.il2cpp;
using BlazeIL.il2reflection;

namespace UnityEngine
{
	/// <summary>
	///   <para>Behaviours are Components that can be enabled or disabled.</para>
	/// </summary>
	public class Shader : Object
	{
		public Shader(IntPtr ptr) : base(ptr) => base.ptr = ptr;

		public static Shader Find(string name)
		{
			return Instance_Class.GetMethod(nameof(Find)).Invoke(new IntPtr[] { new IL2String(name).ptr })?.unbox<Shader>();
		}

		public static new IL2Type Instance_Class = Assemblies.a["UnityEngine.CoreModule"].GetClass("Shader", "UnityEngine");
	}
}
