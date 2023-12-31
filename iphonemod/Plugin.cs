﻿using BepInEx;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;
using Utilla;

// TODO: FIX POSITION
// TODO: ADD BUTTONS
// TODO: CREATE GUI
// TODO: CREATE MOD ENABLER/DISABALER (APPSTORE)
// TODO: CREATE THE CAMERA APP WITH THE THREE CAMERA LENSES
// TODO: (OPT) CREATE IMESSAGE APP
// TODO: BUG FIXES
namespace iphonemod
{
    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        
        bool inRoom;
        public GameObject phone;
        public AssetBundle LoadAssetBundle(string path)
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
            AssetBundle bundle = AssetBundle.LoadFromStream(stream);
            stream.Close();
            return bundle;
        }

        void Start()
        {
            Utilla.Events.GameInitialized += OnGameInitialized;
        }

        void OnEnable()
        {
            HarmonyPatches.ApplyHarmonyPatches();
        }

        void OnDisable()
        {
            HarmonyPatches.RemoveHarmonyPatches();
        }

        void OnGameInitialized(object sender, EventArgs e)
        {
            var bundle = LoadAssetBundle("iphonemod.assets.iphone12pm");
            phone = Instantiate(bundle.LoadAsset<GameObject>("iPhone12ProMax"));
            phone.transform.SetParent(GorillaTagger.Instance.offlineVRRig.rightHandTransform, true);
            Debug.Log("Asset Bundle should be loaded and parented");
        }
        public Vector3 RHTP = GorillaTagger.Instance.offlineVRRig.rightHandTransform.position;
        void FixedUpdate()
        {
            Vector3 phoneposition = RHTP + new Vector3(0, (float)-0.2, (float)0.2);
            phone.transform.position = phoneposition;
        }

        [ModdedGamemodeJoin]
        public void OnJoin(string gamemode)
        {
            inRoom = true;
        }

        [ModdedGamemodeLeave]
        public void OnLeave(string gamemode)
        {
            inRoom = false;
        }
    }
}
