using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        UIEventHandler.OnTurretSpawned += SpawnTurretTimer;	
	}


    private void SpawnTurretTimer(Turret turret)
    {
        GameObject t = ObjectPooler.Instance.GetPooledGameObject(PooledObjectType.TimerUI);
        t.GetComponent<TurretUITimer>().Initialize(GetComponent<Canvas>(), turret);

    }
}
