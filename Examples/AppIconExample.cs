using KSPTools;
using UnityEngine;

namespace KSPToolsExamples
{
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public class AppIconExample : MonoBehaviour
    {
        AppIconManager iconManager;
        public void Start()
        {
            string path = Globals.GetGameFolderPath() + "/GameData/KerbalRevival/Images/button.png";
            Texture2D iconTexture = FileManager.LoadPNG(path);
            iconManager = new AppIconManager(iconTexture, KSP.UI.Screens.ApplicationLauncher.AppScenes.FLIGHT, TestFunction); //Sets up the icon logic and creates it
        } 
        // This function gets called every time the icon is clicked 
        public void TestFunction()
        {
            Debug.Log("TEST ICON");
        }
        public void OnDestroy()
        {
            iconManager.OnDestroy(); // the icon manager does not destroy the icon by default MAKE SURE TO DO THIS MANUALLY
        }
    }
}
