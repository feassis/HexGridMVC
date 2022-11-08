using MVC.Model.Grid;

namespace MVC.Controller.Grid
{
    public class GridService
    {
        private HexTileModel hexTileModel;

        public void Setup()
        {
            hexTileModel = new HexTileModel();
        }

        public int GetTileCost(HexType hexType)
        {
            return hexTileModel.GetCost(hexType);
        }

        public bool IsTileAnObstacle(HexType hexType)
        {
            return hexTileModel.IsObstacle(hexType);
        }
    }
}

