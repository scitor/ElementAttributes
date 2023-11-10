using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using KMod;

namespace ElementAttributes
{
    public class Config
    {
        public List<ElementEntry> elements = new List<ElementEntry>();

        public static Config Generate()
        {
            Config config = new Config();

            config.elements.Add(new ElementEntry { name = "Steel", decor = 20 });
            config.elements.Add(new ElementEntry { name = "Obsidian", decor = 10 });
            config.elements.Add(new ElementEntry { name = "Lead", decor = -10 });
            config.elements.Add(new ElementEntry { name = "SuperInsulator", decor = -20 });

            config.elements.Add(new ElementEntry { name = "Tungsten", overheat = 100, decor = 20 });
            config.elements.Add(new ElementEntry { name = "Wolframite", overheat = 50, decor = 10 });

            config.elements.Add(new ElementEntry { name = "Cobalt", overheat = 100, decor = 20 });
            config.elements.Add(new ElementEntry { name = "Cobaltite", overheat = 50, decor = 10 });

            config.elements.Add(new ElementEntry { name = "Gold", overheat = 50 });
            config.elements.Add(new ElementEntry { name = "GoldAmalgam", decor = 20 });
            config.elements.Add(new ElementEntry { name = "Electrum", overheat = 20, decor = 20 });

            config.elements.Add(new ElementEntry { name = "Copper", overheat = 50 });
            config.elements.Add(new ElementEntry { name = "Cuprite", overheat = 20 });

            config.elements.Add(new ElementEntry { name = "IronOre", overheat = 20 });
            config.elements.Add(new ElementEntry { name = "FoolsGold", overheat = 20 });

            config.elements.Add(new ElementEntry { name = "EnrichedUranium", overheat = -50 });
            config.elements.Add(new ElementEntry { name = "DepletedUranium", overheat = -50 });
            config.elements.Add(new ElementEntry { name = "UraniumOre", overheat = -20 });

            return config;
        }

        public static string InitPath()
        {
            string configPath = Path.Combine(Manager.GetDirectory(), "config", "ElementAttributes");
            if (!Directory.Exists(configPath)) {
                Directory.CreateDirectory(configPath);
            }

            return Path.Combine(configPath, "config.json");
        }

        public static Config ReadJson(string configFile)
        {
            if (!File.Exists(configFile)) {
                return null; 
            }
            return JsonConvert.DeserializeObject<Config>(File.ReadAllText(configFile));
        }

        public static void WriteJson(string configFile, Config config)
        {
            File.WriteAllText(configFile, JsonConvert.SerializeObject(config, Formatting.Indented,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore })
            );
        }
    }

    public class ElementEntry
    {
        public string name;
        public int? overheat;
        public int? decor;
    }
}
