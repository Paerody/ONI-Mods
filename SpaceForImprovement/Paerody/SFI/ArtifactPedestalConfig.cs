﻿using System.Collections.Generic;
using TUNING;
using UnityEngine;
using Paerody.Lib;

namespace Paerody.SFI
{
    class ArtifactPedestalConfig : IBuildingConfig
    {
		public const string ID = "Paerody.ArtifactPedestal";

		internal static void Setup()
		{
			BuildingUtils.AddStrings(ID,
				"Artifact Pedestal",
				"Ergonomic!",
				"A display " + STRINGS.UI.FormatAsLink("Pedestal", ItemPedestalConfig.ID) + " specifically designed for space artifacts.");
			BuildingUtils.AddBuilding(ID, "Furniture", ItemPedestalConfig.ID, "Artistry");
		}

		public override BuildingDef CreateBuildingDef()
		{
			int width = 1;
			int height = 2;
			string anim = "pedestal_kanim";
			int hitpoints = 10;
			float construction_time = 30f;
			float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER2;
			string[] raw_MINERALS = MATERIALS.RAW_MINERALS;
			float melting_point = 800f;
			BuildLocationRule build_location_rule = BuildLocationRule.OnFloor;
			EffectorValues none = NOISE_POLLUTION.NONE;
			BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(ID, width, height, anim, hitpoints, construction_time, tier, raw_MINERALS, melting_point, build_location_rule, BUILDINGS.DECOR.BONUS.TIER0, none, 0.2f);
			buildingDef.DefaultAnimState = "pedestal";
			buildingDef.Floodable = false;
			buildingDef.Overheatable = false;
			buildingDef.ViewMode = OverlayModes.Decor.ID;
			buildingDef.AudioCategory = "Glass";
			buildingDef.AudioSize = "small";
			return buildingDef;
		}

		public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
		{
			go.AddOrGet<Storage>().SetDefaultStoredItemModifiers(new List<Storage.StoredItemModifier>(new Storage.StoredItemModifier[]
			{
			Storage.StoredItemModifier.Seal,
			Storage.StoredItemModifier.Preserve
			}));
			Prioritizable.AddRef(go);
			SingleEntityReceptacle singleEntityReceptacle = go.AddOrGet<SingleEntityReceptacle>();
			singleEntityReceptacle.AddDepositTag(GameTags.Artifact);
			singleEntityReceptacle.occupyingObjectRelativePosition = new Vector3(0f, 1.2f, -1f);
			go.AddOrGet<DecorProvider>();
			go.AddOrGet<ItemPedestal>();
			go.GetComponent<KPrefabID>().AddTag(GameTags.Decoration, false);
		}

		public override void DoPostConfigureComplete(GameObject go)
		{
		}
	}
}
