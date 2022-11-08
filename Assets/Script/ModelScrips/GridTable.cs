using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVC.Model.Grid
{
    public class GridTable
    {
        private int[,] GridActorInfo;
        public GridTable(Vector2Int gridSize)
        {
            GridActorInfo = new int[gridSize.x, gridSize.y];
        }
    }
}

