using MVC.Model.Grid;
using System;
using UnityEngine;


namespace MVC.View.Grid
{
    [SelectionBase]
    public class HexTileGraphics : MonoBehaviour
    {
        [SerializeField]
        private HexCoordinates hexCoordinates;

        public HexType hexType;

        [SerializeField]
        private GlowHighlight highlight;

        public Vector3Int HexCoords => hexCoordinates.GetHexCoords();

        private void Awake()
        {
            if(hexCoordinates == null)
            {
                hexCoordinates = GetComponent<HexCoordinates>();
            }

            if (highlight == null)
            {
                highlight = GetComponent<GlowHighlight>();
            }
        }

        public void EnableHighlight()
        {
            highlight.ToggleGlow(true);
        }

        public void DisableHighlight()
        {
            highlight.ToggleGlow(false);
        }
    }
}

