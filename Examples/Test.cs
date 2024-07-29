﻿using KSPTools;
using UnityEngine;


namespace KSPToolsTest
{
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public class Test : MonoBehaviour
    {
        AppIconManager iconManager;
        public void Start()
        {
            string path = Globals.GetGameFolderPath() + "/GameData/KerbalRevival/Images/button.png";
            Texture2D iconTexture = FileManager.LoadPNG(path);
            iconManager = new AppIconManager(iconTexture, KSP.UI.Screens.ApplicationLauncher.AppScenes.FLIGHT, TestFunction);
        }
        public void TestFunction()
        {
            Debug.Log("TEST ICON");
        }
        public void OnDestroy()
        {
            iconManager.OnDestroy();
            iconManager = null;
            
        }
    }

}
