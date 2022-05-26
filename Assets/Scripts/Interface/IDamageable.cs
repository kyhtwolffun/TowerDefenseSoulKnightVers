using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void InitHealth(int health);
    void TakeDamage(int damage);
}
