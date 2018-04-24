using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    public GameObject[] winImages;


	// Use this for initialization
	void Start ()
    {
        UIEventHandler.OnTurretSpawned += SpawnTurretTimer;
        GameEventHandler.OnPlayerWin += OnPlayerWin;
	}


    private void SpawnTurretTimer(Turret turret)
    {
        GameObject t = ObjectPooler.Instance.GetPooledGameObject(PooledObjectType.TimerUI);
        t.GetComponent<TurretUITimer>().Initialize(GetComponent<Canvas>(), turret);

    }

    private void OnPlayerWin(Player player)
    {
        winImages[(int)player._id].SetActive(true);
    }
}
