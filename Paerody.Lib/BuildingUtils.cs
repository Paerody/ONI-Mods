using System.Collections.Generic;
using TUNING;
using System;
using System.Linq;

/*
 * Credit to Nightinggale and Cairath on github.
 */

namespace Paerody.Lib
{
    public static class BuildingUtils
    {

        public static void AddBuilding(string ID, string buildCategory, string parentID = null, string techCategory = null)
        {
            ToPlanScreen(ID, buildCategory, parentID);
            if (techCategory != null)
                ToTechTree(ID, techCategory);
        }

        private static void ToPlanScreen(string buildingId, HashedString buildCategory, string parentID)
        {
            var index = BUILDINGS.PLANORDER.FindIndex(x => x.category == buildCategory);

            if (index == -1)
                return;

            var planOrderList = BUILDINGS.PLANORDER[index].data as IList<string>;
            if (planOrderList == null)
            {
                Debug.Log("[Paerody] - Could not add " + buildingId + " to the building menu.");
                return;
            }

            var neighborIdx = planOrderList.IndexOf(parentID);

            if (neighborIdx != -1)
                planOrderList.Insert(neighborIdx + 1, buildingId);
            else
                planOrderList.Add(buildingId);
        }

        private static void ToTechTree(string buildingId, string tech)
        {
            var techList = new List<string>(Database.Techs.TECH_GROUPING[tech]) { buildingId };
            Database.Techs.TECH_GROUPING[tech] = techList.ToArray();
        }

        public static void AddStrings(string ID, string Name, string Description, string Effect)
        {
            Strings.Add($"STRINGS.BUILDINGS.PREFABS.{ID.ToUpperInvariant()}.NAME", STRINGS.UI.FormatAsLink(Name, ID));
            Strings.Add($"STRINGS.BUILDINGS.PREFABS.{ID.ToUpperInvariant()}.DESC", Description);
            Strings.Add($"STRINGS.BUILDINGS.PREFABS.{ID.ToUpperInvariant()}.EFFECT", Effect);
        }
    }
}