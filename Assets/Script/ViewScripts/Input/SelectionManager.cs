using System;
using Tools;
using System.Collections.Generic;
using UnityEngine;
using MVC.View.Grid;

namespace MVC.View.InputHandler
{
    public class SelectionManager : MonoBehaviour
    {
        [SerializeField]
        private Camera mainCamera;

        public LayerMask selectionMask;

        private List<Vector3Int> neighbours = new List<Vector3Int>();

        [SerializeField]
        private PlayerInput playerInput;

        private void Awake()
        {
            if (mainCamera == null)
            {
                mainCamera = Camera.main;
            }

            ServiceLocator.RegisterService<SelectionManager>(this);
            playerInput.RegisterToPointerClicked(HandleClick);
        }

        private void OnDestroy()
        {
            playerInput.DeregisterToPointerClicked(HandleClick);
            ServiceLocator.DeregisterService<SelectionManager>();
        }

        public void HandleClick(Vector3 mousePosition)
        {
            var hexGrid = ServiceLocator.GetService<HexGrid>();

            if (FindTarget(mousePosition, out GameObject result))
            {
                HexTileGraphics selectedHex = result.GetComponent<HexTileGraphics>();

                selectedHex.DisableHighlight();

                foreach (var neighbour in neighbours)
                {
                    hexGrid.GetTileAt(neighbour).DisableHighlight();
                }

                var graphSearch = ServiceLocator.GetService<GraphSearch>();
                BFSResult bfsResult = graphSearch.BFSGetRange(hexGrid, selectedHex.HexCoords, 20);
                neighbours = new List<Vector3Int>(bfsResult.GetRangePositions());

                foreach (var neighboursPos in neighbours)
                {
                    hexGrid.GetTileAt(neighboursPos).EnableHighlight();
                }
            }
        }

        private bool FindTarget(Vector3 mousePosition, out GameObject result)
        {
            Ray ray = mainCamera.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, selectionMask))
            {
                result = hit.collider.gameObject;
                return true;
            }

            result = null;
            return false;
        }
    }
}

