using System;
using System.IO;
using HarmonyLib;
using Klei.AI;
using KMod;

namespace ElementAttributes
{
	public class Main : UserMod2
	{
        private static Config config = null;

        public override void OnLoad(Harmony harmony)
        {
            LoadConfig(true);
            harmony.PatchAll();
        }

        [HarmonyPatch(typeof(LegacyModMain), "ConfigElements")]
        public static class LegacyModMain_ConfigElements_Postfix_Patch
        {
            public static void Postfix()
            {
                LoadConfig();
                if (config == null || config.elements.Count < 1) {
                    Log("Nothing to modify. Define elements in config or check for exceptions.");
                    return;
                }

                foreach (ElementEntry entry in config.elements) {
                    Element element = ElementLoader.FindElementByName(entry.name);
                    if (element == null) {
                        Log(string.Format("Ignoring unknown element \"{0}\"", entry.name));
                        continue;
                    }
                    Log(string.Format("Modifying element: {0}", entry.name));
                    if (entry.overheat != null) {
                        if (entry.overheat == 0)
                            RemoveAttribute(element, Db.Get().BuildingAttributes.OverheatTemperature.Id);
                        else
                            ReplaceAttribute(element, Db.Get().BuildingAttributes.OverheatTemperature.Id, (float)entry.overheat);
                    }
                    if (entry.decor != null) {
                        if (entry.decor == 0)
                            RemoveAttribute(element, Db.Get().BuildingAttributes.Decor.Id);
                        else
                            ReplaceAttribute(element, Db.Get().BuildingAttributes.Decor.Id, (float)entry.decor / 100, true);
                    }
                }
                Log("Done modifying elements");
            }
        }

        private static void RemoveAttribute(Element element, string attributeId)
        {
            element.attributeModifiers.RemoveAll(am => am.AttributeId == attributeId);
            Log(string.Format("-> {0} removed", attributeId));
        }

        private static void ReplaceAttribute(Element element, string attributeId, float newValue, bool isMultiplier = false)
        {
            element.attributeModifiers.RemoveAll(am => am.AttributeId == attributeId);
            element.attributeModifiers.Add(new AttributeModifier(attributeId, newValue, element.name, is_multiplier:isMultiplier));
            Log(string.Format("-> {0} {1}", attributeId, newValue));
        }

        private static void Log(string msg)
        {
            Debug.Log("[ElementAttributes] " + msg);
        }

        private static void LoadConfig(bool reload = false)
        {
            if (config != null && !reload) {
                return;
            }

            try {
                string configFile = Config.InitPath();
                if ((config = Config.ReadJson(configFile)) == null) {
                    Log(string.Format("Creating config {0}", configFile));
                    config = Config.Generate();
                    Config.WriteJson(configFile, config);
                    // open json directory (once)
                    UnityEngine.Application.OpenURL(Path.GetDirectoryName(configFile));
                }
            } catch (Exception ex) {
                Debug.LogError("[ElementAttributes] LoadConfig failed");
                Debug.LogException(ex);
            }
        }
    }
}
