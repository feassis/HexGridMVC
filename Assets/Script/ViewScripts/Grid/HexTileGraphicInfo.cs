using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVC.View.Grid
{
    public class HexTileGraphicInfo : MonoBehaviour
    {
        public Vector2Int GridPosition;
        [SerializeField] private Vector3 ofsetVector;
        public Vector3 GetCenterPosition()
        {
            return transform.position + ofsetVector;
        }
    }
}

