using KSP.UI.Screens;
using UnityEngine;
/*
* Requires UnityEngine
* Requires UnityEngine.CoreModule
* Requires UnityEngine.AnimationModule
*/
namespace KSPTools
{
    public class AppIconManager
    {
        ApplicationLauncherButton appbutton;

        Callback onPress;
        ApplicationLauncher.AppScenes appScenes;
        Texture2D iconTexture;
        /// <summary>
        /// Create a bar icon.
        /// </summary>
        /// <param name="iconTexture"></param>
        /// <param name="appScenes"></param>
        /// <param name="onPress"></param>
        public AppIconManager(Texture2D iconTexture, ApplicationLauncher.AppScenes appScenes, Callback onPress)
        {
            this.iconTexture = iconTexture;
            this.appScenes = appScenes;
            this.onPress = onPress;     
            GameEvents.onGUIApplicationLauncherReady.Add(OnGUIAppLauncherReady);
            GameEvents.onGUIApplicationLauncherDestroyed.Add(OnGUIAppLauncherDestroyed);
        }
        private void OnGUIAppLauncherReady()
        {   
            appbutton = ApplicationLauncher.Instance.AddModApplication(onPress, onPress, null, null, null, null, appScenes, iconTexture);
        }
        private void OnGUIAppLauncherDestroyed()
        {
            ApplicationLauncher.Instance.RemoveModApplication(appbutton);
        }
        //Issue: this must be called manually for some reason
        /// <summary>
        /// Call this in the OnDestroy function in your KSPAddon. If this function is not called the icon never gets destroyed(this is an issue that has not been fixed).
        /// </summary>
        public void OnDestroy()
        {
            GameEvents.onGUIApplicationLauncherReady.Remove(OnGUIAppLauncherReady);
            GameEvents.onGUIApplicationLauncherDestroyed.Remove(OnGUIAppLauncherDestroyed);
            OnGUIAppLauncherDestroyed();
        }
       

    }
}
