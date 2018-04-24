using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEventHandler
{
    public delegate void PickupEventHandler(Pickup pickup);
    public static event PickupEventHandler OnPickupDestroyed;

    public delegate void PlayerEventHandler(Player player);
    public static event PlayerEventHandler OnPlayerDeath;
    public static event PlayerEventHandler OnLapComplete;
    public static event PlayerEventHandler OnPlayerWin;

    public delegate void TowerEventHandler(Tower tower);
    public static event TowerEventHandler OnTowerDeath;


    public delegate void PlayerSelectEventHandler(PlayerSelect playerSelect);
    public static event PlayerSelectEventHandler OnPlayerEnterGame;
    public static event PlayerSelectEventHandler OnPlayerLeaveGame;

    public static event PlayerSelectEventHandler OnPlayerReady;
    public static event PlayerSelectEventHandler OnPlayerUnReady;

    public static void CallOnPlayerWin(Player player)
    {
        if (OnPlayerWin != null)
        {
            OnPlayerWin(player);
        }
    }

    public static void CallOnLapComplete(Player player)
    {
        if (OnLapComplete != null)
        {
            OnLapComplete(player);
        }
    }

    public static void CallPlayerLeaveGame(PlayerSelect playerSelect)
    {
        if (OnPlayerLeaveGame != null)
        {
            OnPlayerLeaveGame(playerSelect);
        }
    }

    public static void CallPlayerEnterGame(PlayerSelect playerSelect)
    {
        if (OnPlayerEnterGame != null)
        {
            OnPlayerEnterGame(playerSelect);
        }
    }


    public static void CallPlayerReady(PlayerSelect playerSelect)
    {
        if (OnPlayerReady != null)
        {
            OnPlayerReady(playerSelect);
        }
    }

    public static void CallOnPlayerUnready(PlayerSelect playerSelect)
    {
        if (OnPlayerUnReady != null)
        {
            OnPlayerUnReady(playerSelect);
        }
    }

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

    public static void CallOnTowerDeath(Tower tower)
    {
        if (OnTowerDeath != null)
        {
            OnTowerDeath(tower);
        }
    }
}
