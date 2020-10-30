using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using TUNING;
using Paerody.Lib;

namespace Paerody.SFI
{
    class SpaceGlassTileConfig : IBuildingConfig
    {
		public const string ID = "Paerody.SpaceGlassTile";

		public static void Setup()
		{
			BuildingUtils.AddStrings(ID,
				"Space Glass",
				"A remarkable alloy, transparent to light and Space Scanners, opaque to fluids and meteors. Repels water. Still attracts fingerprints and dust.",
				"Can withstand extreme pressures and impacts. Allows " + STRINGS.UI.FormatAsLink("Light", "LIGHT") + " and " + STRINGS.UI.FormatAsLink("Decor", "DECOR") + " to pass through.");
			BuildingUtils.AddBuilding(ID, "Base", GlassTileConfig.ID, "SkyDetectors");
		}

		public override BuildingDef CreateBuildingDef()
        {
			int width = 1;
			int height = 1;
			string anim = "floor_glass_kanim";
			int hitpoints = 1000;
			float construction_time = 30f;
			float[] construction_mass = new float[]
			{
				BUILDINGS.CONSTRUCTION_MASS_KG.TIER1[0],
				BUILDINGS.CONSTRUCTION_MASS_KG.TIER1[0]
			};
			string[] construction_materials = new string[2]
			{
				SimHashes.Niobium.ToString(),
				SimHashes.Glass.ToString()
			};
			float melting_point = 800f;
			BuildLocationRule build_location_rule = BuildLocationRule.Tile;
			EffectorValues none = NOISE_POLLUTION.NONE;
			BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(ID, width, height, anim, hitpoints, construction_time, construction_mass, construction_materials, melting_point, build_location_rule, BUILDINGS.DECOR.BONUS.TIER0, none, 0.2f);
			BuildingTemplates.CreateFoundationTileDef(buildingDef);
			buildingDef.Floodable = false;
			buildingDef.Entombable = false;
			buildingDef.Overheatable = false;
			buildingDef.UseStructureTemperature = false;
			buildingDef.AudioCategory = "Glass";
			buildingDef.AudioSize = "small";
			buildingDef.BaseTimeUntilRepair = -1f;
			buildingDef.SceneLayer = Grid.SceneLayer.GlassTile;
			buildingDef.isKAnimTile = true;
			buildingDef.isSolidTile = false;
			buildingDef.BlockTileIsTransparent = true;
			buildingDef.BlockTileAtlas = Assets.GetTextureAtlas("tiles_glass");
			buildingDef.BlockTilePlaceAtlas = Assets.GetTextureAtlas("tiles_glass_place");
			buildingDef.BlockTileMaterial = Assets.GetMaterial("tiles_solid");
			buildingDef.DecorBlockTileInfo = Assets.GetBlockTileDecorInfo("tiles_glass_tops_decor_info");
			buildingDef.DecorPlaceBlockTileInfo = Assets.GetBlockTileDecorInfo("tiles_glass_tops_decor_place_info");
			buildingDef.ConstructionOffsetFilter = BuildingDef.ConstructionOffsetFilter_OneDown;
			return buildingDef;
		}

		public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
		{
			GeneratedBuildings.MakeBuildingAlwaysOperational(go);
			BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
			SimCellOccupier simCellOccupier = go.AddOrGet<SimCellOccupier>();
			simCellOccupier.strengthMultiplier = 10f;
			simCellOccupier.setTransparent = true;
			simCellOccupier.notifyOnMelt = true;
			simCellOccupier.setLiquidImpermeable = true;
			simCellOccupier.setGasImpermeable = true;
			go.AddOrGet<TileTemperature>();
			go.AddOrGet<SimCellOccupier>().doReplaceElement = false;
			go.AddOrGet<KAnimGridTileVisualizer>().blockTileConnectorID = GlassTileConfig.BlockTileConnectorID;
			go.AddOrGet<BuildingHP>().destroyOnDamaged = true;
			go.GetComponent<KPrefabID>().AddTag(GameTags.Bunker, true);
		}

		// Token: 0x0600306B RID: 12395 RVA: 0x0010D3BF File Offset: 0x0010B5BF
		public override void DoPostConfigureComplete(GameObject go)
		{
			GeneratedBuildings.RemoveLoopingSounds(go);
			go.GetComponent<KPrefabID>().AddTag(GameTags.FloorTiles, false);
		}

		// Token: 0x0600306C RID: 12396 RVA: 0x000F32B8 File Offset: 0x000F14B8
		public override void DoPostConfigureUnderConstruction(GameObject go)
		{
			base.DoPostConfigureUnderConstruction(go);
			go.AddOrGet<KAnimGridTileVisualizer>();
		}
	}
}
