using System;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace LeagueApi
{
    public class Item
    {
        private WebClient wc = new WebClient() { Encoding = Encoding.UTF8 };
        public ItemList items = new ItemList();
        public ItemData getItemFromId(int itemID)
        {
            foreach (string key in items.data.Keys) {
                if (itemID.ToString() == key)
                {
                    ItemData ret;
                    items.data.TryGetValue(key, out ret);
                    return ret;
                }
            }
            return null;
        }
        public ItemData getItemFromName(string itemName)
        {
            foreach (ItemData data in items.data.Values) if (itemName == data.name) return data;
            return null;
        }
        public Item(string language)
        {
            string ItemJSON;
        GetItems:
            try
            {
                ItemJSON = wc.DownloadString("http://ddragon.leagueoflegends.com/cdn/9.9.1/data/" + language + "/item.json");
            }
            catch (Exception e)
            {
                System.Threading.Thread.Sleep(1000 / 60);
                goto GetItems;
            }
            items = JsonConvert.DeserializeObject<ItemList>(ItemJSON.Replace("\"base\"", "\"_base\""));
            items.dataAsArray = new ItemData[items.data.Count];
            int i = 0;
            foreach (ItemData data in items.data.Values)
            {
                items.dataAsArray[i] = data;
                i++;
            }
        }
        [Serializable]
        public class ItemList
        {
            public string type;
            public string version;
            public BasicData basic;
            public Dictionary<string, ItemData> data;
            public ItemGroups[] groups;
            public ItemTree[] tree;
            public ItemData[] dataAsArray;
        }
        [Serializable]
        public class BasicData
        {
            public string name;
            public RuneData rune;
            public GoldData gold;
            public string group;
            public string description;
            public string colloq;
            public string plaintext;
            public bool consumed;
            public int stacks;
            public int depth;
            public bool consumeOnFull;
            public string[] from;
            public string[] into;
            public int specialRecipe;
            public bool inStore;
            public bool hideFromAll;
            public string requiredChampion;
            public string requiredAlly;
            public ItemStat stats;
            public string[] tags;
            public Dictionary<string, bool> maps;
        }
        [Serializable]
        public class RuneData
        {
            public bool isrune;
            public int tier;
            public string type;
        }
        [Serializable]
        public class GoldData
        {
            public int _base;
            public int total;
            public int sell;
            public bool purchasable;
        }
        [Serializable]
        public class ItemStat
        {
            public float FlatHPPoolMod = 0;
            public float rFlatHPModPerLevel = 0;
            public float FlatMPPoolMod = 0;
            public float rFlatMPModPerLevel = 0;
            public float PercentHPPoolMod = 0;
            public float PercentMPPoolMod = 0;
            public float FlatHPRegenMod = 0;
            public float rFlatHPRegenModPerLevel = 0;
            public float PercentHPRegenMod = 0;
            public float FlatMPRegenMod = 0;
            public float rFlatMPRegenModPerLevel = 0;
            public float PercentMPRegenMod = 0;
            public float FlatArmorMod = 0;
            public float rFlatArmorModPerLevel = 0;
            public float PercentArmorMod = 0;
            public float rFlatArmorPenetrationMod = 0;
            public float rFlatArmorPenetrationModPerLevel = 0;
            public float rPercentArmorPenetrationMod = 0;
            public float rPercentArmorPenetrationModPerLevel = 0;
            public float FlatPhysicalDamageMod = 0;
            public float rFlatPhysicalDamageModPerLevel = 0;
            public float PercentPhysicalDamageMod = 0;
            public float FlatMagicDamageMod = 0;
            public float rFlatMagicDamageModPerLevel = 0;
            public float PercentMagicDamageMod = 0;
            public float FlatMovementSpeedMod = 0;
            public float rFlatMovementSpeedModPerLevel = 0;
            public float PercentMovementSpeedMod = 0;
            public float rPercentMovementSpeedModPerLevel = 0;
            public float FlatAttackSpeedMod = 0;
            public float PercentAttackSpeedMod = 0;
            public float rPercentAttackSpeedModPerLevel = 0;
            public float rFlatDodgeMod = 0;
            public float rFlatDodgeModPerLevel = 0;
            public float PercentDodgeMod = 0;
            public float FlatCritChanceMod = 0;
            public float rFlatCritChanceModPerLevel = 0;
            public float PercentCritChanceMod = 0;
            public float FlatCritDamageMod = 0;
            public float rFlatCritDamageModPerLevel = 0;
            public float PercentCritDamageMod = 0;
            public float FlatBlockMod = 0;
            public float PercentBlockMod = 0;
            public float FlatSpellBlockMod = 0;
            public float rFlatSpellBlockModPerLevel = 0;
            public float PercentSpellBlockMod = 0;
            public float FlatEXPBonus = 0;
            public float PercentEXPBonus = 0;
            public float rPercentCooldownMod = 0;
            public float rPercentCooldownModPerLevel = 0;
            public float rFlatTimeDeadMod = 0;
            public float rFlatTimeDeadModPerLevel = 0;
            public float rPercentTimeDeadMod = 0;
            public float rPercentTimeDeadModPerLevel = 0;
            public float rFlatGoldPer10Mod = 0;
            public float rFlatMagicPenetrationMod = 0;
            public float rFlatMagicPenetrationModPerLevel = 0;
            public float rPercentMagicPenetrationMod = 0;
            public float rPercentMagicPenetrationModPerLevel = 0;
            public float FlatEnergyRegenMod = 0;
            public float rFlatEnergyRegenModPerLevel = 0;
            public float FlatEnergyPoolMod = 0;
            public float rFlatEnergyModPerLevel = 0;
            public float PercentLifeStealMod = 0;
            public float PercentSpellVampMod = 0;
        }
        [Serializable]
        public class ItemData
        {
            public string name;
            public string description;
            public string colloq;
            public string plaintext;
            public string[] into;
            public ImageData image;
            public GoldData gold;
            public string[] tags;
            public Dictionary<string, bool> maps;
            public ItemStat stats;
        }
        [Serializable]
        public class ImageData
        {
            public string full;
            public string sprite;
            public string group;
            public int x;
            public int y;
            public int w;
            public int h;
        }
        [Serializable]
        public class ItemGroups
        {
            public string id;
            public string MaxGroupOwnable;
        }
        [Serializable]
        public class ItemTree
        {
            public string header;
            public string[] tags;
        }
    }
}
