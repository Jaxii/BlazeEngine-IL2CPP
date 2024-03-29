﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using BE4v.MenuEdit;
using BE4v.MenuEdit.IMGUI;
using BE4v.MenuOverlay;
using BE4v.SDK;
using BE4v.SDK.CPP2IL;
using BE4v.Utils;
using UnityEngine;
using VRC;
using VRC.Animation;
using VRC.Core;
using VRC.SDKBase;
using VRC.UI;

namespace BE4v.Mods
{
    public delegate void _Threads_Update(IntPtr instance);
    public static class Threads
    {
        public static void Start()
        {

            try
            {
                IL2Method method = InteractivePlayer.Instance_Class.GetMethod("Update");
                if (method == null)
                    throw new Exception("BE4V: Not found a thread (Update)");

                var patch = new IL2Patch(method, (_Threads_Update)Update);
                _delegateThreads_Update = patch.CreateDelegate<_Threads_Update>();
            }
            catch (Exception ex)
            {
                ex.ToString().WriteMessage("Patch");
            }
            TabMenu.Start();
        }

        public static void Update(IntPtr instance)
        {
            TabMenu.Update();
            if (Status.isInfinityJump)
                Mod_InfinityJump.Update();
            if (Status.isFly)
                Mod_Fly.Update();
            if (Status.isSpeedHack)
                Mod_SpeedHack.Update();

            _delegateThreads_Update.Invoke(instance);

            Mod_Invisible.Update();

            if (isFirstControl > 0)
            {
                if (--isFirstControl == 0)
                {
                    Application.targetFrameRate = 101;
                    /*
                    foreach (var t in Assembler.GetAssemblies())
                    {
                        FileDebug.AddFileDebug("tsoul_a.txt", t.Name);
                    }
                    /*
                    foreach (var a in Assembler.list.Keys)
                    {
                        if (a == "acs")
                        {
                            continue;
                        }
                        foreach (var t in Assembler.list[a].GetClasses())
                        {
                            FileDebug.AddFileDebug("tsoul.txt", t.FullName + " | " + a);
                        }
                        /*
                        }
                        */
                    //}
                    BE4V_MainMenu.Delete();
                    BE4V_MainMenu.Start();
                    BE4V_UserPanel.Start();
                    QuickMenu.ChangeColorMenu(Color.green, Color.white);
                    QuickMenu.ChangeColorButtons(Color.green, Color.green);
                }
                return;
            }

            if (!Input.GetKey(KeyCode.LeftControl)) return;
            if (!Status.is3thCam)
            {
                float mouseWheel = Input.GetAxis("Mouse ScrollWheel");
                if (mouseWheel > 0.1)
                {
                    Camera.main.transform.localPosition += (Vector3.up * 0.1f);
                }
                else if (mouseWheel < -0.1)
                {
                    Camera.main.transform.localPosition -= (Vector3.up * 0.1f);
                }
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                Mod_Fly.Toggle();
                return;
            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                Mod_SpeedHack.Toggle();
                return;
            }
            if (Input.GetKeyDown(KeyCode.Mouse2))
            {
                Mod_FastTP.Teleport();
                return;
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                // Notification.SendInvite(VRC.Player.Instance, "");
                return;
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                Mod_Invisible.Toggle();
                return;
                /*
                GameObject gameObject = VRCPlayer.Instance.avatarGameObject;
                Transform parent = gameObject.transform.parent;
                GameObject newObject = new GameObject(PrimitiveType.Cube);
                newObject.transform.position = gameObject.transform.position;
                newObject.transform.SetParent(parent);
                gameObject.transform.SetParent(newObject.transform);
                newObject.transform.localPosition = new Vector3(0, -1000, 0);
                /*
                string avatarId = GUIUtility.systemCopyBuffer;
                if (Avatars.Utils.IsValidId(avatarId))
                    Avatars.Utils.ChangeAvatarById(avatarId);
                else
                    Console.WriteLine("Not found: " + avatarId);
                // new MainForm();
                /*
                GameObject gameObject = VRC.Network.Instantiate(VRC_EventHandler.VrcBroadcastType.Always, "Portals/PortalInternalDynamic", new Vector3Ex(new IL2String("B\0").ptr, new IL2String("B\0").ptr, new IL2String("B\0").ptr), new Quaternion(0,0,0,0));
                if (gameObject == null)
                    return;
                isAttack = true;
                VRC.Network.RPC(VRC_EventHandler.VrcTargetType.AllBufferOne, gameObject, "ConfigurePortal", new IntPtr[]
                {
                    new IL2String("wrld_a61806c2-4f5c-4c00-8aae-c5f6d5c3bfde").ptr,
                    new IL2String("B\0").ptr,
                    Import.Object.CreateNewObject(0, IL2SystemClass.Int32)
                });
                isAttack = false;
                */
            }
            /*
            if (Input.GetKey(KeyCode.X))
            {
                VRCMotionState motionState = VRCPlayer.Instance.GetComponent<VRCMotionState>();
                if (motionState == null) return;
                motionState.PlayerVelocity = new Vector3(float.NaN, float.NaN, float.NaN);
            }
            */
            // FileDebug.debugGameObject("test.txt", QuickMenu.Instance.gameObject);

        }

        private static bool isAttack = false;

        private static int isFirstControl = 50;

        private static _Threads_Update _delegateThreads_Update;
    }
}
