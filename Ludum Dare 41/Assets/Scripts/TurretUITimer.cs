using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretUITimer : PoolableObject
{
    public Image image;

    float time;
    float timer;

    public void Initialize(Canvas parent, Turret turret)
    {
        transform.SetParent(parent.transform);
        transform.position = Camera.main.WorldToScreenPoint(turret.transform.position);
        gameObject.SetActive(true);
        timer = 0;

        lifeTime = turret.lifeTime;

        if (lifeTime > 0)
        {
            Invoke("Destroy", lifeTime);
        }
    }


    private void Update()
    {
        timer += Time.deltaTime;
        image.fillAmount =  1 - (timer / lifeTime);
    }

}
