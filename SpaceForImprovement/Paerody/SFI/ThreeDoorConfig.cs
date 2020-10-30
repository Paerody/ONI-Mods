using TUNING;
using UnityEngine;
using Paerody.Lib;

namespace Paerody.SFI
{
	internal class ThreeDoorConfig : DoorConfig
	{
		new public const string ID = "Paerody.ThreePneumaticDoor";

        public static void Setup()
        {
            BuildingUtils.AddStrings(ID,
                "Tall Pneumatic Door",
                STRINGS.BUILDINGS.PREFABS.DOOR.DESC,
                STRINGS.BUILDINGS.PREFABS.DOOR.EFFECT);
            BuildingUtils.AddBuilding(ID, "Base", DoorConfig.ID);
        }

        public override BuildingDef CreateBuildingDef()
        {
            int width = 1;
            int height = 3;
            string anim = "door_internal_kanim";
            int hitpoints = 30;
            float construction_time = 5f;
            float[] construction_mass = new float[1] { 150f };
            string[] allMetals = MATERIALS.ALL_METALS;
            float melting_point = 1600f;
            BuildLocationRule build_location_rule = BuildLocationRule.Tile;
            EffectorValues none = TUNING.NOISE_POLLUTION.NONE;
            BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(ID, width, height, anim, hitpoints, construction_time, construction_mass, allMetals, melting_point, build_location_rule, TUNING.BUILDINGS.DECOR.NONE, none, 1f);
            buildingDef.Entombable = true;
            buildingDef.Floodable = false;
            buildingDef.IsFoundation = false;
            buildingDef.AudioCategory = "Metal";
            buildingDef.PermittedRotations = PermittedRotations.R90;
            buildingDef.ForegroundLayer = Grid.SceneLayer.InteriorWall;
            SoundEventVolumeCache.instance.AddVolume(anim, "Open_DoorInternal", TUNING.NOISE_POLLUTION.NOISY.TIER2);
            SoundEventVolumeCache.instance.AddVolume(anim, "Close_DoorInternal", TUNING.NOISE_POLLUTION.NOISY.TIER2);
            return buildingDef;
        }

        public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
        {
            base.DoPostConfigurePreview(def, go);
            go.AddComponent<KAminControllerResize>().height = 1.5f;
        }

        public override void DoPostConfigureUnderConstruction(GameObject go)
        {
            base.DoPostConfigureUnderConstruction(go);
            go.AddComponent<KAminControllerResize>().height = 1.5f;
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
            base.DoPostConfigureComplete(go);
            go.AddComponent<KAminControllerResize>().height = 1.5f;
            //go.AddOrGet<Workable>().workTime = 2f;
        }
    }
}