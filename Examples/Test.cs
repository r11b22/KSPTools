using KSPTools;
using UnityEngine;


namespace KSPToolsTest
{
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public class Test : MonoBehaviour
    {
        bool Ran = false;
        float Timer = 5f;
        public void Update()
        {
            if(Timer <= 0 && !Ran)
            {
                Debug.Log("Start");

                GUIManager manager = new GUIManager("TestWindow", "TestWindow");

                GUIElement button = new Button("TestButton", PrintTest);
                GUIElement label = new Label("TestLabel");

                manager.AddElement(button);
                manager.AddElement(label);

                manager.OpenWindow();

                Orbit orbit = new Orbit(10, 0, 688887, 0, 0, 0, 11348, FlightGlobals.currentMainBody);
                OrbitSetter.SetVesselOrbit(FlightGlobals.ActiveVessel, orbit);
                Ran = true;
            }
            else
            {
                Timer -= Time.deltaTime;
            }
            
        }
        public void PrintTest()
        {
            Debug.Log("Test");
        }
    }

}
