using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/*
 * Credit to Nightinggale on github. Allows changing building tints within the config file. 
 */
namespace Paerody.Lib
{
    [SkipSaveFileSerialization]
    public class BuildingColor : KMonoBehaviour
    {
        [SerializeField]
        public Color32 color = new Color32(0, 0, 0, 0);

        protected override void OnSpawn()
        {
            base.OnSpawn();

            if (color.a != 0)
            {
                KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
                if (component != null)
                {
                    component.TintColour = this.color;
                }
            }
        }
    }
}
