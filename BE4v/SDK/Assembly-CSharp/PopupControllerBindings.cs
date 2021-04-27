﻿using System;
using System.Linq;
using BE4v.SDK.CPP2IL;

public class PopupControllerBindings : VRCUiPopup
{
    public PopupControllerBindings(IntPtr ptr) : base(ptr) => base.ptr = ptr;

	public static new IL2Class Instance_Class = Assembler.list["acs"].GetClasses().FirstOrDefault(x => x.GetMethod("SetBindingsPresetTypeA") != null);
}