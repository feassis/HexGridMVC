using UnityEngine;
using Tools;
using UnityEngine.AddressableAssets;
using MVC.Model.Grid; // change this once you figure out how adressable work
using System.Threading.Tasks;
using System;
using MVC.Controller.Grid;

public class GameInitialization : MonoBehaviour
{
    [SerializeField] private AssetReference gridConfigReference; // change this once you figure out how adressable work

    private static Action OnAfterGridControllerInit;
    private static Action OnInitalizationEnd;

    public static void SubscribeOnAfterControllerInit(Action actionToSubscribe)
    {
        OnAfterGridControllerInit += actionToSubscribe;
    }

    public static void SubscribeOnInitializationEnd(Action actionToSubscribe)
    {
        OnInitalizationEnd += actionToSubscribe;
    }

    private void Start()
    {
        Setup();
    }

    private void Setup()
    {
        InitializeGraphSearchService();
        InitializeGridService();

        //await InitializeGridController();
        //InitializeUnitControler();
    }


    private void InitializeGridService()
    {
        var gridService = new GridService();
        gridService.Setup();
        ServiceLocator.RegisterService<GridService>(gridService);
    }
    private async Task InitializeGridController()
    {
        var gridController = new GridController();
        var gridConfigOperation = gridConfigReference.LoadAssetAsync<GridConfig>();
        GridConfig gridConfig = await gridConfigOperation.Task;
        gridController.Setup(gridConfig);
        ServiceLocator.RegisterService<GridController>(gridController);
        OnAfterGridControllerInit?.Invoke();
    }

    private void InitializeGraphSearchService()
    {
        var graphSearch = new GraphSearch();
        ServiceLocator.RegisterService<GraphSearch>(graphSearch);
    }

    private void InitializeUnitControler()
    {
        var unitController = new UnitController();
        ServiceLocator.RegisterService<UnitController>(unitController);
        var unitsSpawnPosition = ServiceLocator.GetService<GridController>().GetUnitSpawnPositions();

        foreach (UnitSpawnPosition unit in unitsSpawnPosition)
        {
            unitController.SpawnUnit(unit.SpawnPosition, unit.UnitId);
        }
    }
}
