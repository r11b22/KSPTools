using KSPTools;
using UnityEngine;
/*
* This example sets the orbit of the active vessel after 5 seconds in flight
*/
namespace KSPToolsTest.Examples
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
                SetOrbit();
                Ran = true;
            }
            else
            {
                Timer -= Time.deltaTime;
            }

        }
        public void SetOrbit()
        {
            Orbit orbit = new Orbit(10, 0, 688887, 0, 0, 0, 11348, FlightGlobals.currentMainBody); //this can be any orbit
            OrbitSetter.SetVesselOrbit(FlightGlobals.ActiveVessel, orbit); // this does the orbit setting
        }
    }
   
}
