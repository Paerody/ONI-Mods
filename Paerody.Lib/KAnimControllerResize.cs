using System;
using STRINGS;
using UnityEngine;

/**
 *  Credit to Nightinggale on github
 */
namespace Paerody.Lib
{
    [SkipSaveFileSerialization]
    public class KAminControllerResize : KMonoBehaviour
    {
        public float width = 1f;
        public float height = 1f;

        [MyCmpGet]
        private KBatchedAnimController controller;

        protected override void OnSpawn()
        {
            base.OnSpawn();
            if (controller != null)
            {
                if (this.width != 1f)
                {
                    controller.animWidth = this.width;
                }
                if (this.height != 1f)
                {
                    controller.animHeight = this.height;
                }
            }
        }
    }

}