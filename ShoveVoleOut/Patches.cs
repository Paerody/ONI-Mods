using System.Linq;
using Database;
using Harmony;
using ProcGen;

namespace Paerody.ShoveVoleOut
{
    public class Patches
    {
        //Force Dusty Dwarf @ 50k ring
        [HarmonyPatch(typeof(SpacecraftManager))]
        [HarmonyPatch("OnPrefabInit")]
        public class SpacecraftManager_PrefabInit_Patch
        {
            public static void Postfix()
            {
                if (!SpacecraftManager.instance.destinationsGenerated)
                {
                    int nextID = SpacecraftManager.instance.destinations.Count;
                    SpacecraftManager.instance.destinations.Add(new SpaceDestination(nextID, Db.Get().SpaceDestinationTypes.DustyMoon.Id, 4));
                }
            }
        }

        //Remove Space biome Mole tag to prevent Shove Vole spawns
        [HarmonyPatch(typeof(SettingsCache))]
        [HarmonyPatch("LoadFiles")]
        public class SettingsCache_LoadFiles_Patch
        {
            public static void Postfix()
            {
                SubWorld surface;
                SettingsCache.subworlds.TryGetValue("subworlds/space/Surface", out surface);
                foreach (WeightedBiome biome in surface.biomes)
                {
                    if(biome.tags.Contains("Mole"))
                        biome.tags.Remove("Mole");
                }
                SubWorld surfaceCrags;
                SettingsCache.subworlds.TryGetValue("subworlds/space/SurfaceCrags", out surfaceCrags);
                foreach (WeightedBiome biome in surfaceCrags.biomes)
                {
                    if (biome.tags.Contains("Mole"))
                        biome.tags.Remove("Mole");
                }
            }
        }

        // Make Vole Pup/Vole Egg Care Packages require that Vole Eggs be discovered first
        [HarmonyPatch(typeof(Immigration))]
        [HarmonyPatch("ConfigureCarePackages")]
        public class Immigration_ConfigureCarePackages_Patch
        {
            public static void Postfix(ref CarePackageInfo[] ___carePackages)
            {
                //Debug Only - to quickly test CarePackages
                //___carePackages = new CarePackageInfo[]
                //{
                //    new CarePackageInfo("MoleBaby", 1f, null),
                //    new CarePackageInfo("MoleEgg", 3f, null),
                //    new CarePackageInfo("Funky_Vest", 1f, null)
                //};
                for(int i = 0;  i < ___carePackages.Length; i++)
                {
                    if(___carePackages[i].id == "MoleBaby")
                    {
                        ___carePackages[i] = new CarePackageInfo("MoleBaby", 1f, () => WorldInventory.Instance.IsDiscovered("MoleEgg"));
                        Debug.Log("[Paerody] - Updated Vole Pup care package requirements.");
                    }
                    else if(___carePackages[i].id == "MoleEgg")
                    {
                        ___carePackages[i] = new CarePackageInfo("MoleEgg", 3f, () => WorldInventory.Instance.IsDiscovered("MoleEgg"));
                        Debug.Log("[Paerody] - Updated Vole Egg care package requirements.");
                    }
                }
            }
        }

        //Debug Only - to check discovered status on world load
        //[HarmonyPatch(typeof(WorldInventory))]
        //[HarmonyPatch("OnSpawn")]
        //public class WorldInventory_OnSpawn_Patch
        //{
        //    public static void Prefix()
        //    {
        //        Debug.Log("[Paerody] - Vole Pups discovered?: " + WorldInventory.Instance.IsDiscovered("MoleBaby"));
        //        Debug.Log("[Paerody] - Vole Eggs discovered?: " + WorldInventory.Instance.IsDiscovered("MoleEgg"));
        //    }
        //    public static void Postfix()
        //    {
        //        Debug.Log("[Paerody] - Vole Pups discovered?: " + WorldInventory.Instance.IsDiscovered("MoleBaby"));
        //        Debug.Log("[Paerody] - Vole Eggs discovered?: " + WorldInventory.Instance.IsDiscovered("MoleEgg"));
        //    }
        //}


        //Add Shove Voles to Dusty Dwarf loot tables
        [HarmonyPatch(typeof(Db))]
        [HarmonyPatch("Initialize")]
        public class Db_Initialize_Patch
        {
            public static void Postfix()
            {
                SpaceDestinationType dustyMoon = Db.Get().SpaceDestinationTypes.DustyMoon;
                if (dustyMoon.recoverableEntities == null)
                    dustyMoon.recoverableEntities = new System.Collections.Generic.Dictionary<string, int>();
                if(!dustyMoon.recoverableEntities.ContainsKey("Mole"))
                    dustyMoon.recoverableEntities.Add("Mole", 2);
            }
        }
    }
}
