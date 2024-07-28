﻿using System.IO;
using UnityEngine;
/*
 * Requires UnityEngine
 * Requires UnityEngine.CoreModule
 */
namespace KSPTools
{
    public static class Serialiser
    {
        public static ConfigNode SaveOrbit(Orbit orbit)
        {
            ConfigNode node = new ConfigNode("ORBIT");
            OrbitSnapshot snapshot = new OrbitSnapshot(orbit);
            snapshot.Save(node);
            return node;
        }
        public static Orbit LoadOrbit(ConfigNode node)
        {
            OrbitSnapshot snapshot = new OrbitSnapshot(node);
            return snapshot.Load();
        }
        
        public static void SetConfigValue(ConfigNode node, string valueName, string value)
        {
            if (node.HasValue(valueName))
            {
                node.SetValue(valueName, value);
            }
            else
            {
                node.AddValue(valueName, value);
            }
        }
    }
}
