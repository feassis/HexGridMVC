
using System;

namespace MVC.Model.Grid
{
    public class HexTileModel
    {
        public int GetCost(HexType hexType) => hexType switch
        {
            HexType.Default => 10,
            HexType.Difficult => 20,
            HexType.Road => 5,
            _ => throw new Exception($"Hex Type {hexType} Not Supported"),
        };

        public bool IsObstacle(HexType hexType)
        {
            return hexType == HexType.Obstacle || hexType == HexType.Water;
        }
    }


    public enum HexType
    {
        None = 0,
        Default = 1,
        Difficult = 2,
        Road = 3,
        Water = 4,
        Obstacle = 5
    }
}

