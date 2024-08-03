using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;
/*
 * Requires UnityEngine
 * Requires UnityEngine.CoreModule
 * Requires FileManager.cs KSPTools
 * Requires Serialiser.cs KSPTools
 */
namespace KSPTools
{
    [KSPAddon(KSPAddon.Startup.AllGameScenes, false)]
    public class EventManager : MonoBehaviour
    {
        public const string SAVENAME = "Events";
        ConfigNode save;
        Dictionary<string, double> eventDict = new Dictionary<string, double>();
        FileManager fileManager;
        public void Awake()
        {
            GameEvents.onGameStateLoad.Add(OnLoad);
        }
        public void OnDestroy()
        {
            GameEvents.onGameStateLoad.Remove(OnLoad);
        }
        private void OnLoad(ConfigNode data)
        {
            Debug.Log("LoadEventManager");
            fileManager = FindObjectOfType<FileManager>();
            Debug.Log("set file manager");
            Debug.Log(fileManager);
            save = fileManager.SetupSaveNode(SAVENAME);
            Debug.Log("load save");
            foreach (ConfigNode node in save.GetNodes())
            {
                Debug.Log(node);
                Debug.Log(node.GetValue("time"));
                Debug.Log(Convert.ToDouble(node.GetValue("time")));
                
                eventDict.Add(node.GetValue("name"), Convert.ToDouble(node.GetValue("time")));
                Debug.Log("Add to event dict");
            }
        }
        public void SetEvent(string EventName, double Seconds)
        {
            
            Seconds += Globals.GetGameTime();
            Debug.Log(Seconds);
            ConfigNode eventNode = new ConfigNode(EventName.Replace(" ", "_"));
            Serialiser.SetConfigValue(eventNode, "name", EventName);
            Serialiser.SetConfigValue(eventNode, "time", Seconds.ToString());
            save.AddNode(eventNode);
            if(eventDict.ContainsKey(EventName))
            {
                Debug.Log("event changed");
                eventDict[EventName] = Seconds;
            }
            else
            {
                Debug.Log("new event scheduled");
                eventDict.Add(EventName, Seconds);
            }     
        }
        public void RemoveEvent(string EventName)
        {
            Debug.Log("Removing event: " + EventName);
            save.RemoveNode(EventName);
            eventDict.Remove(EventName);
        }
        public void Update()
        {
            double currentTime = Globals.GetGameTime();

            foreach (KeyValuePair<string, double> entry in eventDict)
            {

                if (entry.Value <= currentTime)
                {
                    RemoveEvent(entry.Key);
                    Debug.Log("Folowing event was fired: " + entry.Key);
                }
            }
        }
    }
}
