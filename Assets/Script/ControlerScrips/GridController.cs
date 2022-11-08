using MVC.Model.Grid;
using System.Collections.Generic;
using UnityEngine;

public class GridController
{
    private List<GridConfig> availableGrids = new List<GridConfig>();
    private GridTable gridTable;
    public void Setup(GridConfig config)
    {
        availableGrids.Add(config);
        gridTable = new GridTable(GetGridSize());
    }

    public Vector2Int GetGridSize()
    {
        return new Vector2Int(availableGrids[0].GridData.GridConfig.Length, availableGrids[0].GridData.GridConfig[0].Row.Length);
    }

    public int[,] GetGridSetup()
    {
        var size = GetGridSize();
        int[,] generatedGrid = new int[size.x, size.y];

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                generatedGrid[i, j] = availableGrids[0].GridData.GridConfig[i].Row[j];
            }
        }

        return generatedGrid;
    }

    public List<UnitSpawnPosition> GetUnitSpawnPositions()
    {
        return availableGrids[0].UnitSpawnPosition;
    }
}
