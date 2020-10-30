using System;
using Harmony;
using UnityEngine;

namespace Paerody.SFI
{
	public class BuildingGenerationPatches
	{
		[HarmonyPatch(typeof(GeneratedBuildings))]
		[HarmonyPatch("LoadGeneratedBuildings")]
		public static class GeneratedBuildings_LoadGeneratedBuildings_Patch
		{
			public static void Prefix()
			{
				SpaceGlassTileConfig.Setup();
				SpaceLadderConfig.Setup();
				SpaceMeshConfig.Setup();
				ArtifactPedestalConfig.Setup();
				ArtifactBinConfig.Setup();
				ThreeDoorConfig.Setup();
				RocketDoorConfig.Setup();
			}
		}
	}
}
