using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;
using Tools;
using MVC.View.Grid;


namespace MVC.View.Unit
{
    public class UnitGraphicManager : MonoBehaviour
    {
        [SerializeField] private UnitGraphicConfig unitGraphicConfig;
        private Dictionary<Vector2Int, UnitGraphics> units;
       
        private void Awake()
        {
            UnitController.SubscribeToUnitSpwanedEvent(RenderUnit);
        }

        private void RenderUnit(Vector2Int position, int unitId)
        {
            var unit = unitGraphicConfig.GetUnitGraphics(unitId);
            var getGridPosition = ServiceLocator.GetService<HexGridLayout>().GetTileCenterPosition(position);

            UnitGraphics unitGrapfics = Instantiate(unit);
            unitGrapfics.transform.position = getGridPosition;
            unitGrapfics.CurrentUnitPosition = position;
        }
    }
}

