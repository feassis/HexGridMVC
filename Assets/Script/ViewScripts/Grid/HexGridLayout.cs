using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Tools;


namespace MVC.View.Grid
{
    //Class Based On Hex Grid Tutorial: https://www.youtube.com/watch?v=EPaSmQ2vtek&list=PLL_zf3MigDAPckjYBJ1nha_Toww3zHY7j&index=1
    public class HexGridLayout : MonoBehaviour
    {
        [Header("Grid Settings")]
        public Vector2Int GridSize;

        [Header("Tile Settings")]
        public float InnerRadious;
        public float OuterRadious;
        public float Height;
        public bool IsFlatToped;
        [HideInInspector]
        public HexTileGraphicInfo[,] GridObjects;

        //Configs
        [SerializeField] private VisualTilesConfig tileVisualConfig;//change this after figureout adressables
        private int[,] gridToBeRendered;

        private void Awake()
        {
            GameInitialization.SubscribeOnAfterControllerInit(Init);
        }

        private void Init()
        {
            var gridControler = ServiceLocator.GetService<GridController>();
            GridSize = gridControler.GetGridSize();
            gridToBeRendered = gridControler.GetGridSetup();
            ServiceLocator.RegisterService<HexGridLayout>(this);

            LayoutGrid();
        }

        public Vector3 GetTileCenterPosition(Vector2Int desiredPosition)
        {
            return GridObjects[desiredPosition.x, desiredPosition.y].GetCenterPosition();
        }

        private void LayoutGrid()
        {
            ClearLayoutGrid();

            for (int y = 0; y < GridSize.y; y++)
            {
                for (int x = 0; x < GridSize.x; ++x)
                {
                    GameObject prefab = tileVisualConfig.GetPrefab(gridToBeRendered[x, y]);
                        
                    GameObject tile = Instantiate(prefab);
                    GridObjects[x, y] = tile.GetComponent<HexTileGraphicInfo>();
                    tile.name = $"Hex {x} , {y}";
                    tile.transform.position = GetPositionForHexFromCoordinate(new Vector2Int(x, y));
                    GridObjects[x, y].GridPosition = new Vector2Int(x, y);

                    HexRenderer hexRenderer = tile.GetComponent<HexRenderer>();
                    hexRenderer.IsFlatToped = IsFlatToped;
                    hexRenderer.OuterRadious = OuterRadious;
                    hexRenderer.InnerRadious = InnerRadious;
                    hexRenderer.Height = Height;
                    hexRenderer.DrawMesh();

                    tile.transform.SetParent(transform);
                }
            }
        }

        private void ClearLayoutGrid()
        {
            if (GridObjects == null)
            {
                GridObjects = new HexTileGraphicInfo[GridSize.x, GridSize.y];
            }

            for (int y = 0; y < GridSize.y; y++)
            {
                for (int x = 0; x < GridSize.x; x++)
                {
                    Destroy(GridObjects[x, y]);
                }
            }

            GridObjects = new HexTileGraphicInfo[GridSize.x, GridSize.y];
        }

        public Vector3 GetPositionForHexFromCoordinate(Vector2Int coordinate)
        {
            int column = coordinate.x;
            int row = coordinate.y;

            float width;
            float height;
            float yPosition;
            float xPosition;
            bool shouldOffset;
            float horizontalDistance;
            float verticalDistance;
            float offset;
            float size = OuterRadious;

            if (IsFlatToped)
            {
                shouldOffset = (row % 2) == 0;
                width = Mathf.Sqrt(3f) * size;
                height = 2f * size;

                horizontalDistance = width;
                verticalDistance = height * (3f / 4f);

                offset = (shouldOffset) ? width / 2 : 0;

                yPosition = (column * horizontalDistance) + offset;
                xPosition = row * verticalDistance;
            }
            else
            {
                shouldOffset = (column % 2) == 0;
                width = 2f * size;
                height = Mathf.Sqrt(3f) * size;

                horizontalDistance = width * (3f / 4f);
                verticalDistance = height;

                offset = (shouldOffset) ? height / 2 : 0;

                yPosition = column * horizontalDistance;
                xPosition = (row * verticalDistance) - offset;
            }

            return new Vector3(-xPosition, 0, yPosition);
        }

    }
}

