﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlazeIL.il2ch;
using BlazeIL.il2cpp;
using BlazeIL.il2generic;
using UnityEngine;
using UnityEngine.UI;
using BlazeTools;

namespace Addons.Patch
{
    public delegate void _VRC_Player_DispatchedUpdate(IntPtr instance, float fTimer);
    public static class patch_ColoredPlates
    {
        public static void Start()
        {
            IL2Method method = null;
            try
            {
                method = VRCPlayer.Instance_Class.GetMethods().First(x => x.GetParameters().Length == 1 && x.GetParameters()[0].ReturnType.Name == typeof(float).FullName);
                if (method == null)
                    new Exception();

                var patch = IL2Ch.Patch(method, (_VRC_Player_DispatchedUpdate)VRC_Player_DispatchedUpdate);
                _delegateVRC_Player_DispatchedUpdate = patch.CreateDelegate<_VRC_Player_DispatchedUpdate>();
                Dll_Loader.success_Patch.Add("Colored Plates");
            }
            catch
            {
                Dll_Loader.failed_Patch.Add("Colored Plates");
            }
        }


        public static void VRC_Player_DispatchedUpdate(IntPtr instance, float timer)
        {
            if (instance == IntPtr.Zero)
                return;

            _delegateVRC_Player_DispatchedUpdate.Invoke(instance, timer);

            VRCPlayer vrcPlayer = new VRCPlayer(instance);
            /*
            VRCPlayer vrcPlayer = new VRCPlayer(instance);
            VRC.Core.APIUser apiuser = vrcPlayer?.player.user;
            if (apiuser == null) return;

            SocialRank rank = VRCPlayer.GetSocialRank(apiuser);
            string textRank = string.Empty;
            if (rank == SocialRank.VRChatTeam)
                textRank = "Moderator";
            else if (rank == SocialRank.Legend)
                textRank = "Legend";
            else if (rank == SocialRank.VeteranUser)
                textRank = "Veteran";
            else if (rank == SocialRank.TrustedUser)
                textRank = "Trust";
            else if (rank == SocialRank.KnownUser)
                textRank = "Known";

            if (string.IsNullOrWhiteSpace(textRank)) return;
            */
        }


        public static _VRC_Player_DispatchedUpdate _delegateVRC_Player_DispatchedUpdate;
    }
}
