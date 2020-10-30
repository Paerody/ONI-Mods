using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TUNING;
using UnityEngine;
using Paerody.Lib;

namespace Paerody.SFI
{
    class SpaceMeshConfig : IBuildingConfig
    {
		public const string ID = "Paerody.SpaceMesh";

		internal static void Setup()
        {
			BuildingUtils.AddStrings(ID,
				"Space Mesh",
				"Nikola tried to teach me how this works, but I'm pretty sure it's just magic.",
				"Resists " + STRINGS.UI.FormatAsLink("gas", "ELEMENTS_GAS") + " flow without obstructing " + STRINGS.UI.FormatAsLink("liquids", "ELEMENTS_LIQUID") + ". Falling liquids may force gas up into and through this block. ");
			BuildingUtils.AddBuilding(ID, "Base", GasPermeableMembraneConfig.ID, "Jetpacks");
		}

		public override BuildingDef CreateBuildingDef()
		{
			int width = 1;
			int height = 1;
			string anim = "floor_gasperm_kanim";
			int hitpoints = 100;
			float construction_time = 30f;
			float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER2;
			string[] construction_materials = new string[1]
			{
				SimHashes.Niobium.ToString()
			};
			float melting_point = 1600f;
			BuildLocationRule build_location_rule = BuildLocationRule.Tile;
			EffectorValues none = NOISE_POLLUTION.NONE;
			BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(ID, width, height, anim, hitpoints, construction_time, tier, construction_materials, melting_point, build_location_rule, BUILDINGS.DECOR.PENALTY.TIER0, none, 0.2f);
			BuildingTemplates.CreateFoundationTileDef(buildingDef);
			buildingDef.Floodable = false;
			buildingDef.Entombable = false;
			buildingDef.Overheatable = false;
			buildingDef.AudioCategory = "Metal";
			buildingDef.AudioSize = "small";
			buildingDef.BaseTimeUntilRepair = -1f;
			buildingDef.SceneLayer = Grid.SceneLayer.TileMain;
			buildingDef.isKAnimTile = true;
			buildingDef.BlockTileAtlas = Assets.GetTextureAtlas("tiles_gasmembrane");
			buildingDef.BlockTilePlaceAtlas = Assets.GetTextureAtlas("tiles_gasmembrane_place");
			buildingDef.BlockTileMaterial = Assets.GetMaterial("tiles_solid");
			buildingDef.DecorBlockTileInfo = Assets.GetBlockTileDecorInfo("tiles_mesh_tops_decor_info");
			buildingDef.DecorPlaceBlockTileInfo = Assets.GetBlockTileDecorInfo("tiles_mesh_tops_decor_place_info");
			buildingDef.ConstructionOffsetFilter = BuildingDef.ConstructionOffsetFilter_OneDown;
			return buildingDef;
		}

		public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
		{
			GeneratedBuildings.MakeBuildingAlwaysOperational(go);
			BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
			SimCellOccupier simCellOccupier = go.AddOrGet<SimCellOccupier>();
			simCellOccupier.setGasImpermeable = true;
			simCellOccupier.doReplaceElement = false;
			go.AddOrGet<KAnimGridTileVisualizer>().blockTileConnectorID = MeshTileConfig.BlockTileConnectorID;
			go.AddOrGet<BuildingHP>().destroyOnDamaged = true;
			go.AddComponent<SimTemperatureTransfer>();
		}

		public override void DoPostConfigureComplete(GameObject go)
		{
			go.AddComponent<ZoneTile>();
			go.GetComponent<KPrefabID>().AddTag(GameTags.FloorTiles, false);
		}

		public override void DoPostConfigureUnderConstruction(GameObject go)
		{
			base.DoPostConfigureUnderConstruction(go);
			go.AddOrGet<KAnimGridTileVisualizer>();
		}


	}
}
