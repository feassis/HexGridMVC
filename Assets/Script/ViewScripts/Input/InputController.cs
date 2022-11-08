using MVC.View.Grid;
using MVC.View.Unit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace MVC.View.InputHandler
{
    public class InputController : MonoBehaviour
    {
        [SerializeField] private Camera myCamera;
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                HandleLeftClickInput();
            }
        }

        private void HandleLeftClickInput()
        {
            Debug.Log("clicked");
            var gameObject = ObjectClicked();

            Debug.Log($"GameObject: {gameObject == null}");

            if (gameObject == null)
            {
                return;
            }

            if (gameObject.TryGetComponent<HexTileGraphicInfo>(out HexTileGraphicInfo selectedTile))
            {
                Debug.Log($"Tile Selected: x - {selectedTile.GridPosition.x} | y - {selectedTile.GridPosition.y}");
                return;
            }

            if (gameObject.TryGetComponent<UnitGraphics>(out UnitGraphics selectedUnit))
            {
                Debug.Log($"Unit Selected: x - {selectedUnit.CurrentUnitPosition.x} | y - {selectedUnit.CurrentUnitPosition.y}");
                return;
            }
        }

        private GameObject ObjectClicked()
        {
            RaycastHit hit;
            Ray ray = myCamera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit))
            {
                return hit.transform.gameObject;
            }

            return null;
        }
    }
}

