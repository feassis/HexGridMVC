using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools;
using MVC.View.Grid;

public class HexGrid : MonoBehaviour
{
    Dictionary<Vector3Int, HexTileGraphics> hexTileDict = new Dictionary<Vector3Int, HexTileGraphics>();
    Dictionary<Vector3Int, List<Vector3Int>> hexTileNeighboursDict = new Dictionary<Vector3Int, List<Vector3Int>>();

    private void Awake()
    {
        ServiceLocator.RegisterService<HexGrid>(this);
    }

    private void Start()
    {
        foreach (var hex in FindObjectsOfType<HexTileGraphics>())
        {
            hexTileDict[hex.HexCoords] = hex;
        }
    }

    public HexTileGraphics GetTileAt(Vector3Int hexCoordinates)
    {

        hexTileDict.TryGetValue(hexCoordinates, out HexTileGraphics tile);
        return tile;
    }

    public List<Vector3Int> GetNeighBoursFor(Vector3Int hexCoordinate)
    {
        if (!hexTileDict.ContainsKey(hexCoordinate))
        {
            return new List<Vector3Int>();
        }

        if (hexTileNeighboursDict.ContainsKey(hexCoordinate))
        {
            return hexTileNeighboursDict[hexCoordinate];
        }

        hexTileNeighboursDict.Add(hexCoordinate, new List<Vector3Int>());

        foreach (var direction in Direction.GetDirectionList(hexCoordinate.z))
        {
            if(hexTileDict.ContainsKey(hexCoordinate + direction))
            {
                hexTileNeighboursDict[hexCoordinate].Add(hexCoordinate + direction);
            }
        }

        return hexTileNeighboursDict[hexCoordinate];
    }
}


public static class Direction
{
    public static List<Vector3Int> DirectionOffsetOdd = new List<Vector3Int>
    {
        new Vector3Int(-1, 0, 1), //N1
        new Vector3Int(0, 0, 1), //N2
        new Vector3Int(1, 0, 0), //E
        new Vector3Int(0, 0, -1), //S2
        new Vector3Int(-1, 0, -1), //S1
        new Vector3Int(-1, 0, 0) //W
    };

    public static List<Vector3Int> DirectionOffsetEven = new List<Vector3Int>
    {
        new Vector3Int(0, 0, 1), //N1
        new Vector3Int(1, 0, 1), //N2
        new Vector3Int(1, 0, 0), //E
        new Vector3Int(1, 0, -1), //S2
        new Vector3Int(0, 0, -1), //S1
        new Vector3Int(-1, 0, 0) //W
    };

    public static List<Vector3Int> GetDirectionList(int z) => z % 2 == 0 ? DirectionOffsetEven : DirectionOffsetOdd;
}