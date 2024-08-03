using KSPTools;
using UnityEngine;


namespace KSPToolsTest
{
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public class Test : MonoBehaviour
    {
        public void Start()
        {
            EventManager eventManager = FindObjectOfType<EventManager>();

            eventManager.SetEvent("TestEvent", 500000);
        }
    }

}
