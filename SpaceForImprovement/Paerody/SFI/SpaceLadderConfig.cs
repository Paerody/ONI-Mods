using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TUNING;
using UnityEngine;
using Paerody.Lib;

namespace Paerody.SFI
{

    class SpaceLadderConfig : IBuildingConfig
    {
        public const string ID = "Paerody.SpaceLadder";

        public static void Setup()
        {
            BuildingUtils.AddStrings(ID,
                "Space Ladder",
                "Slippery when wet.",
                "Extremely resistant to damage.");
            BuildingUtils.AddBuilding(ID, "Base", LadderConfig.ID, "BasicRocketry");
        }

        public override BuildingDef CreateBuildingDef()
        {
            int width = 1;
            int height = 1;
            string anim = "ladder_kanim";
            int hitpoints = 1000;
            float construction_time = 10f;
            float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER2;
            string[] construction_materials = new string[1]
            {
                SimHashes.Niobium.ToString()
            };
            float melting_point = 1600f;
            BuildLocationRule build_location_rule = BuildLocationRule.Anywhere;
            EffectorValues none = NOISE_POLLUTION.NONE;
            BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(ID, width, height, anim, hitpoints, construction_time, tier, construction_materials, melting_point, build_location_rule, BUILDINGS.DECOR.PENALTY.TIER0, none, 0.2f);
            BuildingTemplates.CreateLadderDef(buildingDef);
            buildingDef.Floodable = false;
            buildingDef.Overheatable = false;
            buildingDef.Entombable = false;
            buildingDef.AudioCategory = "Metal";
            buildingDef.AudioSize = "small";
            buildingDef.BaseTimeUntilRepair = -1f;
            buildingDef.DragBuild = true;
            return buildingDef;
        }

        public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
        {
            GeneratedBuildings.MakeBuildingAlwaysOperational(go);
            Ladder ladder = go.AddOrGet<Ladder>();
            ladder.upwardsMovementSpeedMultiplier = 1f;
            ladder.downwardsMovementSpeedMultiplier = 1f;
            go.AddComponent<BuildingColor>().color = new Color(0.2f, 0.2f, 0.6f, 0.8f);
            go.AddOrGet<AnimTileable>();
            go.GetComponent<KPrefabID>().AddTag(GameTags.Bunker, true);
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
        }
    }
}
