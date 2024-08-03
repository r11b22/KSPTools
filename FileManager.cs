using UnityEngine;
using System.IO;
using Smooth.Algebraics;
using System.Collections.Generic;
/*
* Requires UnityEngine
* Requires UnityEngine.CoreModule
* Requires UnityEngien.ImageConversionModule(For PNGToTexture)
*/
namespace KSPTools
{
    [KSPAddon(KSPAddon.Startup.EveryScene, false)]
    public class FileManager : MonoBehaviour
    {  
        private ConfigNode save;
        private const string ROOTNAME = "CustomSave";

        public void Awake()
        {
            Debug.Log("OnAwake");
            GameEvents.onGameStateSave.Add(OnSave);
            GameEvents.onGameStateLoad.Add(OnLoad);
        }
        

        private void OnLoad(ConfigNode data)
        {
            Debug.Log(data);
            if (data.HasNode(ROOTNAME))
            {
                Debug.Log("HasNode");
                save = data.GetNode(ROOTNAME);
            }else
            {
                Debug.Log("Creating new node");
                save = new ConfigNode(ROOTNAME);
                data.AddNode(save);
            }
            
        }
        private void OnSave(ConfigNode data)
        {
            
            if (save != null)
            {
                data.RemoveNode(ROOTNAME);
                data.AddNode(save);
            }  
        }

        public void OnDestroy()
        {
            Debug.Log("OnDestroy");
            GameEvents.onGameStateSave.Remove(OnSave);
            GameEvents.onGameStateLoad.Remove(OnLoad);
        }
        /// <summary>
        /// This function sets up a node in the save file. If it already exists it uses the version in the file
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ConfigNode SetupSaveNode(string name)
        {
            if (save.HasNode(name))
            {
                Debug.Log(save.GetNode(name));
                Debug.Log(save);
                return save.GetNode(name);
            }
            else
            {
                ConfigNode node = new ConfigNode(name);
                save.AddNode(node);
                Debug.Log(save.GetNode(name));
                Debug.Log(save);
                return node;
            }
            
            
        }

        public void AddSaveNode(ConfigNode node)
        {
            Debug.Log("A config node was set up for saving");
            save.AddNode(node);
            
        }

        /// <summary>
        /// Saves a ConfigNode to a file.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="node"></param>
        public static void SaveToFile(string path, ConfigNode node)
        {
            Debug.Log("Saving to: " + path);
            try
            {
                node.Save(path);
            }
            catch
            {
                Debug.Log("Save directory: " + path + " \ndoes not exist: \nTry creating it before saving");
            }
        }
        /// <summary>
        /// Load a file into a ConfigNode object from path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Convert a PNG image into a unity Texture object2D.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Texture2D LoadPNG(string path)
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
