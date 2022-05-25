using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableBase : MonoBehaviour, IDamageable
{
    [Header("Health Properties")]
    [SerializeField] protected int maxHealth;
    [SerializeField] protected int health;
    [SerializeField] protected float takeDmgCdr;
    [SerializeField] protected SliderBehaviour healthBar;

    protected bool isDamageable = true;

    protected void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
        health = maxHealth;
        //healthBar.SetHealth(health);
    }

    public void TakeDamage(int damage)
    {
        if (!isDamageable)
            return;

        health -= damage;
        //Update health bar
        healthBar.SetHealth(health);
        //Die
        if (health <= 0)
        {
            transform.parent.gameObject.SetActive(false);
            return;
        }
        StartCoroutine(CoolingDownTakeDmg());
    }

    protected IEnumerator CoolingDownTakeDmg()
    {
        isDamageable = false;
        yield return new WaitForSeconds(takeDmgCdr);
        isDamageable = true;
    }
}
