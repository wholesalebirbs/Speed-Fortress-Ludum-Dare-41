using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEventHandler
{
    public delegate void PickupEventHandler(Pickup pickup);
    public static event PickupEventHandler OnPickupDestroyed;

    public delegate void PlayerEventHandler(Player player);
    public static event PlayerEventHandler OnPlayerDeath;

    public delegate void TowerEventHandler(Tower tower);
    public static event TowerEventHandler OnTowerDeath;

    public static void CallOnPickupDestroyed(Pickup pickup)
    {
        if (OnPickupDestroyed != null)
        {
            OnPickupDestroyed(pickup);
        }
    }

    public static void CallOnPlayerDeath(Player player)
    {
        if (OnPlayerDeath != null)
        {
            OnPlayerDeath(player);
        }
    }
}
