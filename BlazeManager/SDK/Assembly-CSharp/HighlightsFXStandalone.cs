using System;
using BlazeIL;
using BlazeIL.il2cpp;
using UnityEngine;

public sealed class HighlightsFXStandalone : HighlightsFX
{
    public HighlightsFXStandalone(IntPtr ptr) : base(ptr) => base.ptr = ptr;

    public HighlightsFXStandalone() : base(IntPtr.Zero)
    {
        ptr = IL2Import.il2cpp_object_new(Instance_Class.ptr);
        Instance_Class.GetConstructor().Invoke(ptr);
    }

    public Color highlightColor
    {
        get
        {
            IL2Field field = Instance_Class.GetField(nameof(m_Material));
            if (field == null)
                (field = Instance_Class.GetField(x => x.ReturnType.Name == Color.Instance_Class.FullName)).Name = nameof(m_Material);

            IL2Object result = field.GetValue(ptr);
            if (result == null)
                return default;

            return result.unbox_Unmanaged<Color>();
        }
        set
        {
            IL2Field field = Instance_Class.GetField(nameof(m_Material));
            if (field == null)
                (field = Instance_Class.GetField(x => x.ReturnType.Name == Color.Instance_Class.FullName)).Name = nameof(m_Material);

            field?.SetValue(ptr, value.MonoCast());
        }
    }

    public static new IL2Type Instance_Class = Assemblies.a[LangTransfer.values[cAssemblies.offset + (long)eAssemblies.assemblycsharp]].GetClass("HighlightsFXStandalone");
}