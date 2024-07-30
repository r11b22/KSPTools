﻿using KSPTools;
using UnityEngine;


namespace KSPToolsTest
{
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public class Test : MonoBehaviour
    {
        public void Start()
        {
            Vessel vessel = Globals.GetActiveVessel();
            foreach(Part part in vessel.parts)
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
