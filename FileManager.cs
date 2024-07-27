using UnityEngine;
using System.IO;
/*
 * Requires UnityEngine
 * Requires UnityEngine.CoreModule
 * Requires UnityEngien.ImageConversionModule(For PNGToTexture)
 */
namespace KSPTools
{
    public static class FileManager
    {
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
        public static Texture2D PNGToTexture(string path)
        {
           
            if (File.Exists(path))
            {
                byte[] data = File.ReadAllBytes(path);
                Texture2D tex = new Texture2D(2, 2);
                tex.LoadImage(data);
                return tex;
            }else
            {
                Debug.Log("PNG File at: " + path + "\n does not exist");
            }
            return null;
        }
    }
}
