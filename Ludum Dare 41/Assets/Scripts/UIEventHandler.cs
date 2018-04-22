using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UIEventHandler
{
    public delegate void TurretEventHandler(Turret turret);
    public static event TurretEventHandler OnTurretSpawned;

    public static void CallTurretSpawned(Turret turret)
    {
        if (OnTurretSpawned != null)
        {
            OnTurretSpawned(turret);
        }
    }

}
