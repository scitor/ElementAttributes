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
                if (config.elements.Count < 1)
                {
                    return;
                }

                foreach (ElementEntry entry in config.elements)
                {
                    Element element = ElementLoader.FindElementByName(entry.name);
                    if (element == null)
                    {
                        Log(string.Format("Ignoring unknown element \"{0}\"", entry.name));
                        continue;
                    }
                    Log(string.Format("Modifying element: {0}", entry.name));
                    if (entry.overheat != null)
                    {
                        ReplaceAttribute(element, Db.Get().BuildingAttributes.OverheatTemperature.Id, (float)entry.overheat);
                    }
                    if (entry.decor != null)
                    {
                        ReplaceAttribute(element, Db.Get().BuildingAttributes.Decor.Id, (float)entry.decor / 100, true);
                    }
                }
                Log("Done modifying elements");
            }
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
            if (config != null && !reload)
            {
                return;
            }

            try
            {
                string configFile = Config.initPath();
                if ((config = Config.readJson(configFile)) == null)
                {
                    Log(string.Format("Creating config {0}", configFile));
                    Config.writeJson(configFile, config = Config.generate());

                    // open json directory (once)
                    UnityEngine.Application.OpenURL(Path.GetDirectoryName(configFile));
                }
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }
    }
}
