using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using System.IO;

namespace KSPTools
{
    public static class Tools
    {
        public static void TestMessage(string Message)
        {
            Debug.Log(Message);
        }
        

    }
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
            if(File.Exists(path))
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
    public static class OrbitSetter
    {
        public static void SetVesselOrbit(Vessel vessel, Orbit orbit)
        {
            try
            {
                OrbitPhysicsManager.HoldVesselUnpack(60);
            }
            catch (NullReferenceException)
            {
                Debug.Log("OrbitPhysicsManager.HoldVesselUnpack threw NullReferenceException");
            }
            PrepVesselTeleport(vessel);
            SetOrbit(vessel, orbit);
        }
        private static void SetOrbit(Vessel vessel, Orbit orbit)
        {

            var allVessels = FlightGlobals.fetch?.vessels ?? (IEnumerable<Vessel>)new[] { vessel };
            foreach (var v in allVessels)
                v.GoOnRails();

            var oldBody = vessel.orbitDriver.orbit.referenceBody;

            vessel.orbitDriver.orbit.inclination = orbit.inclination;
            vessel.orbitDriver.orbit.eccentricity = orbit.eccentricity;
            vessel.orbitDriver.orbit.semiMajorAxis = orbit.semiMajorAxis;
            vessel.orbitDriver.orbit.LAN = orbit.LAN;
            vessel.orbitDriver.orbit.argumentOfPeriapsis = orbit.argumentOfPeriapsis;
            vessel.orbitDriver.orbit.meanAnomalyAtEpoch = orbit.meanAnomalyAtEpoch;
            vessel.orbitDriver.orbit.epoch = orbit.epoch;

            vessel.orbitDriver.orbit.Init();

            vessel.orbitDriver.orbit.UpdateFromUT(Planetarium.GetUniversalTime());
            if (vessel.orbitDriver.orbit.referenceBody != orbit.referenceBody)
            {
                vessel.orbitDriver.OnReferenceBodyChange?.Invoke(orbit.referenceBody);
            }

            vessel.orbitDriver.pos = vessel.orbit.pos.xzy;
            vessel.orbitDriver.vel = vessel.orbit.vel;

            var newBody = vessel.orbitDriver.orbit.referenceBody;
            if (newBody != oldBody)
            {
                var evnt = new GameEvents.HostedFromToAction<Vessel, CelestialBody>(vessel, oldBody, newBody);
                GameEvents.onVesselSOIChanged.Fire(evnt);
            }
        }
        public static void PrepVesselTeleport(Vessel vessel)
        {
            if (vessel.Landed)
            {
                vessel.Landed = false;
                Debug.Log("Set ActiveVessel.Landed = false");
            }
            if (vessel.Splashed)
            {
                vessel.Splashed = false;
                Debug.Log("Set ActiveVessel.Splashed = false");
            }
            if (vessel.landedAt != string.Empty)
            {
                vessel.landedAt = string.Empty;
                Debug.Log("Set ActiveVessel.landedAt = \"\"");
            }
            var parts = vessel.parts;
            if (parts != null)
            {
                var killcount = 0;
                foreach (var part in parts.Where(part => part.Modules.OfType<LaunchClamp>().Any()).ToList())
                {
                    killcount++;
                    part.Die();
                }
                if (killcount != 0)
                {
                    Debug.Log($"Removed {killcount} launch clamps from {vessel.vesselName}");
                }
            }
        }
    }
    public class GUIManager
    {
        private string WindowName;
        private string WindowTitle;
        private const string CONTROLLOCKNAME = "UILock";
        DialogGUIBase dialogGUI;
        private const ControlTypes ControlLock = ControlTypes.ALL_SHIP_CONTROLS;

        public GUIManager(string windowName, string windowTitle)
        {
            WindowName = windowName;
            WindowTitle = windowTitle;
            dialogGUI = new DialogGUIVerticalLayout();

        }
        public void LockControls()
        {
            InputLockManager.SetControlLock(ControlLock, CONTROLLOCKNAME);
            Debug.Log("Controls disabled");
        }
        public void UnlockControls()
        {
            InputLockManager.RemoveControlLock(CONTROLLOCKNAME);
            Debug.Log("Controls enabled");
        }
        public void CreateWindow(bool ControlsLocked = true)
        {
            DialogGUIButton CloseButton = new DialogGUIButton("Close", CloseWindow, false);
            AddElement(CloseButton);
            MultiOptionDialog dialog = new MultiOptionDialog(WindowName, "", WindowTitle, HighLogic.UISkin, dialogGUI);
            PopupDialog.SpawnPopupDialog(dialog, false, HighLogic.UISkin);
            if (ControlsLocked)
            {
                LockControls();
            }
        }
        public void CloseWindow()
        {
            PopupDialog.DismissPopup(WindowName);
            UnlockControls();
        }
        public void AddElement(DialogGUIBase element)
        {
            dialogGUI.AddChild(element);
        }

    }

}



