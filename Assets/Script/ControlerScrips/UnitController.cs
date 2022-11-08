using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Tools;

public class UnitController
{
    private static event Action<Vector2Int, int> UnitSpawned;
    private Dictionary<Vector2Int, int> liveUnits = new Dictionary<Vector2Int, int>();
    
    public void SpawnUnit(Vector2Int position, int unitID)
    {
        if (liveUnits.ContainsKey(position))
        {
            Debug.LogError($"Already Have Unit At This Position: {position}");
            return;
        }

        liveUnits.Add(position, unitID);
        UnitSpawned?.Invoke(position, unitID);
    }

    public static void SubscribeToUnitSpwanedEvent(Action<Vector2Int, int> actionToSubscribe)
    {
        UnitSpawned += actionToSubscribe; //later I need to create de unsubscribe method
    }
}
