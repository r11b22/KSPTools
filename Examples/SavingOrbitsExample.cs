using KSPTools;
using UnityEngine;
/*
 * This example saves the orbit of the active vessel to a file inside the ksp directory
 */
namespace KSPToolsTest.Examples
{
    public class SavingOrbitsExample
    {
        [KSPAddon(KSPAddon.Startup.Flight, false)]
        public class SetOrbitExample : MonoBehaviour
        {
            bool Ran = false;
            float Timer = 5f;
            public void Update()
            {
                if (Timer <= 0 && !Ran)
                {
                    SaveOrbit();
                    Ran = true;
                }
                else
                {
                    Timer -= Time.deltaTime;
                }

            }
            public void SaveOrbit()
            {
                ConfigNode orbitNode = Serialiser.SaveOrbit(FlightGlobals.ActiveVessel.GetOrbit()); // saves to orbit to the confignode: orbitNode
                FileManager.SaveToFile(Globals.GetSaveFolderPath() + "/Orbit.save", orbitNode); // saves the confignode to the current save folder 
            }
        }
       
    }
}
