using UnityEngine;
/*
 * Requires UnityEngine
 * Requires UnityEngine.CoreModule
 */
namespace KSPToolsTest
{
    public static class Globals
    {
        /// <summary>
        /// returns the path of ksp folder
        /// </summary>
        /// <returns></returns>
        public static string GetGameFolderPath()
        {
            return KSPUtil.ApplicationRootPath;
        }
        /// <summary>
        /// returns only the folder name (not the full path)
        /// </summary>
        /// <returns></returns>
        public static string GetSaveFolderName() 
        {
            return HighLogic.SaveFolder;
        }
        /// <summary>
        /// returns the full path to the current save folder
        /// </summary>
        /// <returns></returns>
        public static string GetSaveFolderPath() 
        {
            return KSPUtil.ApplicationRootPath + "/saves/" + HighLogic.SaveFolder;
        }
        /// <summary>
        /// returns the active vessel
        /// </summary>
        /// <returns></returns>
        public static Vessel GetActiveVessel() 
        {
            return FlightGlobals.ActiveVessel;
        }
        /// <summary>
        /// returns the current celestial body
        /// </summary>
        /// <returns></returns>
        public static CelestialBody GetCurrentBody() 
        {
            return FlightGlobals.currentMainBody;
        }
    }
}
