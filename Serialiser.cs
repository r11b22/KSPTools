using System.IO;
using UnityEngine;
/*
 * Requires UnityEngine
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
        public static void SaveToFile(string path, ConfigNode node)
        {
            Debug.Log("Saving to: " + path);
            if (Directory.Exists(path))
            {
                node.Save(path);
            }
            else
            {
                Debug.Log("Save directory: " + path + " \ndoes not exist: \nTry creating it before saving");
            }
        }
        public static ConfigNode LoadFromFile(string path)
        {
            ConfigNode node = new ConfigNode();
            if (File.Exists(path))
            {
                node = ConfigNode.Load(path);
            }
            else
            {
                Debug.Log("Save directory: " + path + " \ndoes not exist: \nTry creating it before loading");
            }

            return node;
        }
        public static void SetConfigValue(ConfigNode node, string ValueName, string Value)
        {
            if (node.HasValue(ValueName))
            {
                node.SetValue(ValueName, Value);
            }
            else
            {
                node.AddValue(ValueName, Value);
            }
        }
    }
}
