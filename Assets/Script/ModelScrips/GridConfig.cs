using System;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace MVC.Model.Grid
{
    [CreateAssetMenu(fileName = "Grid Config", menuName = "Grid/Config")]
    [Serializable]
    public class GridConfig : ScriptableObject
    {
        public List<TileDataEntry> TileOptions;
        [InfoBox("Remember this needs to be within grid size")]
        public List<UnitSpawnPosition> UnitSpawnPosition;
        public GridData GridData;
    }

    [Serializable]
    public struct UnitSpawnPosition
    {
        public Vector2Int SpawnPosition;
        public int UnitId;
    }

    [Serializable]
    public class TileDataEntry
    {
        public int Id;
        public TileConfig Tile;
    }

    [Serializable]
    public class GridData
    {
        public RowData[] GridConfig = new RowData[20];
    }

    //Sturcture based on https://www.youtube.com/watch?v=mxqD1B2e4ME
    [Serializable]
    public struct RowData
    {
        public int[] Row;
    }
}


