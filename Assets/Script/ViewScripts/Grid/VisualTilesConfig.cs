using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;


namespace MVC.View.Grid
{
    [CreateAssetMenu(fileName = "VisualTilesConfig", menuName = "Grid/Visual Tiles Config")]
    public class VisualTilesConfig : ScriptableObject
    {
        public List<TileConfigEntry> Library;

        public GameObject GetPrefab(int id)
        {
            foreach (var entry in Library)
            {
                if(entry.Id == id)
                {
                    return entry.GetPrefab();
                }
            }

            return null;
        }
    }

    [Serializable]
    public class TileConfigEntry
    {
        public int Id;
        public GameObject TileObject;

        public GameObject GetPrefab()
        {
            return TileObject;
        }
    }
}

