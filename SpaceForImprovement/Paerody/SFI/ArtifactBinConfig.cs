using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TUNING;
using UnityEngine;
using Paerody.Lib;

namespace Paerody.SFI
{
    class ArtifactBinConfig : IBuildingConfig
    {
		public const string ID = "Paerody.ArtifactBin";
		public static readonly Tag Tag_ArtifactBin = TagManager.Create(ID);

		public static void Setup()
		{
			BuildingUtils.AddStrings(ID,
				"Artifact Bin",
				"Of what, then, is an empty launchpad a sign? - Abe", 
				"Stores up to 50 space artifacts. Use caution when emptying.");
			BuildingUtils.AddBuilding(ID, "Base", StorageLockerConfig.ID, "CargoI");
		}

		public override BuildingDef CreateBuildingDef()
		{
			int width = 1;
			int height = 2;
			string anim = "storagelocker_kanim";
			int hitpoints = 30;
			float construction_time = 10f;
			float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
			string[] raw_MINERALS = MATERIALS.RAW_MINERALS;
			float melting_point = 1600f;
			BuildLocationRule build_location_rule = BuildLocationRule.OnFloor;
			EffectorValues none = NOISE_POLLUTION.NONE;
			BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(ID, width, height, anim, hitpoints, construction_time, tier, raw_MINERALS, melting_point, build_location_rule, BUILDINGS.DECOR.PENALTY.TIER1, none, 0.2f);
			buildingDef.Floodable = false;
			buildingDef.AudioCategory = "Metal";
			buildingDef.Overheatable = false;
			return buildingDef;
		}

		// Token: 0x060034C1 RID: 13505 RVA: 0x00123094 File Offset: 0x00121294
		public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
		{
			SoundEventVolumeCache.instance.AddVolume("storagelocker_kanim", "StorageLocker_Hit_metallic_low", NOISE_POLLUTION.NOISY.TIER1);
			Prioritizable.AddRef(go);
			Storage storage = go.AddOrGet<Storage>();
			storage.showInUI = true;
			storage.allowItemRemoval = true;
			storage.showDescriptor = true;
			storage.capacityKg = 1250f;
			storage.storageFilters = new List<Tag> { GameTags.Artifact };
			storage.storageFullMargin = STORAGE.STORAGE_LOCKER_FILLED_MARGIN;
			storage.fetchCategory = Storage.FetchCategory.GeneralStorage;
			go.AddOrGet<CopyBuildingSettings>().copyGroupTag = Tag_ArtifactBin;
			go.AddOrGet<ArtifactBin>();
			go.AddOrGet<UserNameable>();
		}

		// Token: 0x060034C2 RID: 13506 RVA: 0x000F6290 File Offset: 0x000F4490
		public override void DoPostConfigureComplete(GameObject go)
		{
			go.AddOrGetDef<StorageController.Def>();
		}
	}
}
