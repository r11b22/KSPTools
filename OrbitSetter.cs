using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/*
 * Requires UnityEngine
 * Requires UnityEngine.CoreModule
 */
namespace KSPTools
{
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
}
