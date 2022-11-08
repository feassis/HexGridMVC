using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCoordinates : MonoBehaviour
{
    public static float XOffset = 2f;
    public static float YOffset = 1f;
    public static float ZOffset = 1.73f;

    internal Vector3Int GetHexCoords() => offsetCoordinates;

    [Header("Offset Coordinates")]
    [SerializeField]
    private Vector3Int offsetCoordinates;

    private void Awake()
    {
        offsetCoordinates = ConversPositionToOffset(transform.position);
    }

    private Vector3Int ConversPositionToOffset(Vector3 position)
    {
        int x = Mathf.CeilToInt(position.x / XOffset);
        int y = Mathf.RoundToInt(position.y / YOffset);
        int z = Mathf.RoundToInt(position.z / ZOffset);

        return new Vector3Int(x, y, z);
    }
}
