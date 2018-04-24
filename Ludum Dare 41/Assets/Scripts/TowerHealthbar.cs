using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TowerHealthbar : MonoBehaviour
{

    public GameObject target;
    public Image bar;

    public PlayerNumber number;
    // Use this for initialization

    // Update is called once per frame
    void Update()
    {
        if (!target.activeSelf)
        {
            gameObject.SetActive(false);
        }

        IHealthInterface targetHealth = target.GetComponent<IHealthInterface>();

        Vector3 screenPos = Camera.main.WorldToScreenPoint(target.transform.position);
        transform.position = screenPos;

        bar.fillAmount = targetHealth.CurrentHealth / targetHealth.TotalHealth;
    }
}
