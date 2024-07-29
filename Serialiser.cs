using System.IO;
using UnityEngine;
/*
 * Requires UnityEngine
 * Requires UnityEngine.CoreModule
 */
namespace KSPTools
{
    public static class Serialiser
    {
        /// <summary>
        /// Converts the orbit into a ConfigNode for saving purposes
        /// </summary>
        /// <param name="orbit"></param>
        /// <returns></returns>
        public static ConfigNode SaveOrbit(Orbit orbit)
        {
            ConfigNode node = new ConfigNode("ORBIT");
            OrbitSnapshot snapshot = new OrbitSnapshot(orbit);
            snapshot.Save(node);
            return node;
        }
        /// <summary>
        /// Converts a ConfigNode into an Orbit object. Make sure this ConfigNode contains a previously saved orbit.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static Orbit LoadOrbit(ConfigNode node)
        {
            OrbitSnapshot snapshot = new OrbitSnapshot(node);
            return snapshot.Load();
        }
        /// <summary>
        /// Helps setting the value of a ConfigNode. If the value does not yet exists it creates it for you.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="valueName"></param>
        /// <param name="value"></param>
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
