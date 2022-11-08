using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using NaughtyAttributes;

namespace MVC.View.Unit
{
    [CreateAssetMenu(fileName = "UnitGraphicConfig", menuName = "Unity/Graphics/Config")]
    public class UnitGraphicConfig : ScriptableObject
    {
        [InfoBox("Do Not Repeat Ids")]
        public List<UnitConfig> UnitConfigs;

        public UnitGraphics GetUnitGraphics(int id)
        {
            foreach (var unit in UnitConfigs)
            {
                if(unit.Id == id)
                {
                    return unit.UnitGraphics;
                }
            }

            return null;
        }
    }

    [Serializable]
    public struct UnitConfig
    {
        public int Id;
        public UnitGraphics UnitGraphics;
    }
}

