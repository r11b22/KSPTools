using KSPTools;
using UnityEngine;

namespace KSPToolsTest
{
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public class Test : MonoBehaviour
    {
        void Start()
        {
            Tools.TestMessage("TestMessage");
        }
    }
}
