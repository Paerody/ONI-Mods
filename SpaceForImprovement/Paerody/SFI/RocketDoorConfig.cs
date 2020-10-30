using TUNING;
using UnityEngine;
using Paerody.Lib;

namespace Paerody.SFI
{
	internal class RocketDoorConfig : BunkerDoorConfig
	{
        new public const string ID = "Paerody.RocketDoor";

        public static void Setup()
        {
            BuildingUtils.AddStrings(ID,
                "Rocket Door",
                STRINGS.BUILDINGS.PREFABS.BUNKERDOOR.DESC + " Perfectly sized for rockets.",
                STRINGS.BUILDINGS.PREFABS.BUNKERDOOR.EFFECT);
            BuildingUtils.AddBuilding(ID, "Base", DoorConfig.ID, "EnginesI");
        }

        public override BuildingDef CreateBuildingDef()
        {
            float[] construction_mass = new float[1] { 1000f };
            string[] construction_materials = new string[1]
            {
                SimHashes.Steel.ToString()
            };
            EffectorValues none1 = NOISE_POLLUTION.NONE;
            EffectorValues none2 = BUILDINGS.DECOR.NONE;
            EffectorValues noise = none1;
            string anim = "door_bunker_kanim";
            BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(ID, 7, 1, anim, 1000, 120f, construction_mass, construction_materials, 1600f, BuildLocationRule.Tile, none2, noise, 1f);
            buildingDef.RequiresPowerInput = true;
            buildingDef.EnergyConsumptionWhenActive = 120f;
            buildingDef.OverheatTemperature = 1273.15f;
            buildingDef.Entombable = false;
            buildingDef.IsFoundation = true;
            buildingDef.AudioCategory = "Metal";
            buildingDef.PermittedRotations = PermittedRotations.R90;
            buildingDef.SceneLayer = Grid.SceneLayer.TileMain;
            buildingDef.ForegroundLayer = Grid.SceneLayer.InteriorWall;
            buildingDef.TileLayer = ObjectLayer.FoundationTile;
            buildingDef.LogicInputPorts = DoorConfig.CreateSingleInputPortList(new CellOffset(-1, 0));
            SoundEventVolumeCache.instance.AddVolume(anim, "Open_DoorInternal", NOISE_POLLUTION.NOISY.TIER2);
            SoundEventVolumeCache.instance.AddVolume(anim, "Close_DoorInternal", NOISE_POLLUTION.NOISY.TIER2);
            return buildingDef;
        }

        public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
        {
            base.DoPostConfigurePreview(def, go);
            go.AddComponent<KAminControllerResize>().width = 1.75f;
        }

        public override void DoPostConfigureUnderConstruction(GameObject go)
        {
            base.DoPostConfigureUnderConstruction(go);
            go.AddComponent<KAminControllerResize>().width = 1.75f;
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
            base.DoPostConfigureComplete(go);
            go.AddComponent<KAminControllerResize>().width = 1.75f;
            //go.AddOrGet<Workable>().workTime = 2f;
        }
    }
}