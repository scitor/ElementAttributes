# Element Attributes
This mod changes some attributes of elements, currently `overheat temperature` and `decor` modifier.

- First-time start generates config and opens folder.
- There are example config entries, remove / modify as you desire.

default config.json:
```json
{
  "elements":[
    {"name":"Steel"          , "decor": 20                },
    {"name":"Obsidian"       , "decor": 10                },
    {"name":"Lead"           , "decor":-10                },
    {"name":"SuperInsulator" , "decor":-20                },
    {"name":"Tungsten"       , "decor": 20, "overheat":100},
    {"name":"Wolframite"     , "decor": 10, "overheat": 50},
    {"name":"Cobalt"         , "decor": 20, "overheat":100},
    {"name":"Cobaltite"      , "decor": 10, "overheat": 50},
    {"name":"Gold"           ,              "overheat": 50},
    {"name":"GoldAmalgam"    , "decor": 20                },
    {"name":"Electrum"       , "decor": 20, "overheat": 20},
    {"name":"Copper"         ,              "overheat": 50},
    {"name":"Cuprite"        ,              "overheat": 20},
    {"name":"IronOre"        ,              "overheat": 20},
    {"name":"FoolsGold"      ,              "overheat": 20},
    {"name":"EnrichedUranium",              "overheat":-50},
    {"name":"DepletedUranium",              "overheat":-50},
    {"name":"UraniumOre"     ,              "overheat":-20}
  ]
}

```
