using CommNet;
using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace RP0
{
    [KSPAddon(KSPAddon.Startup.Instantly, true)]
    public class HarmonyPatcher : MonoBehaviour
    {
        internal void Start()
        {
            var harmony = new Harmony("RP0.HarmonyPatcher");
            harmony.PatchAll();
        }

        [HarmonyPatch(typeof(Vessel))]
        [HarmonyPatch("GetControlLevel")]
        internal class PatchVesselGetControlLevel
        {
            internal static bool Prefix(Vessel __instance, List<ProtoCrewMember> ___crew, ref Vessel.ControlLevel __result)
            {
                if (__instance.isEVA && ___crew.Count > 0)
                {
                    if (!___crew[0].inactive)
                    {
                        __result = Vessel.ControlLevel.PARTIAL_MANNED;
                        return false;
                    }
                    __result = Vessel.ControlLevel.NONE;
                    return false;
                }

                if (__instance.connection != null && CommNetScenario.Instance != null && CommNetScenario.CommNetEnabled)
                {
                    __result = __instance.connection.GetControlLevel();
                    if (__result == Vessel.ControlLevel.NONE) return false;
                }

                var controlLevel = Vessel.ControlLevel.NONE;
                int count = __instance.parts.Count;
                while (count-- > 0)
                {
                    Part part = __instance.parts[count];
                    if (part.isControlSource > controlLevel)
                    {
                        controlLevel = part.isControlSource;
                    }
                }

                if (controlLevel < __result) __result = controlLevel;

                return false;
            }
        }
    }
}
