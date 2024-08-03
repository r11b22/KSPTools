using KSPTools;
using UnityEngine;
/*
 * Sets all resources on a vessel to zero and then adds 10.
 */
namespace KSPToolsExamples
{
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public class ResourceExample : MonoBehaviour
    {
        public void Start()
        {
            Vessel vessel = Globals.GetActiveVessel();
            foreach (Part part in vessel.parts)
            {
                foreach (PartResource resource in part.Resources)
                {
                    ResourceManager.SetResource(resource, 0);
                    ResourceManager.AddResource(resource, 10);
                }
            }
        }
    }
}
