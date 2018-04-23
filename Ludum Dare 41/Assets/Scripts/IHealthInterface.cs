using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealthInterface
{
    float TotalHealth { get; set; }
    float CurrentHealth { get; set; }
    void TakeDamage(int value, PlayerNumber number);
}
