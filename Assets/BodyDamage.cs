using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyDamage : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float damageDelay;

    private bool onCoolDown = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        try
        {
            DamageableBase damageable = collision.transform.GetComponent<DamageableBase>();
            if (damageable && !onCoolDown)
            {
                damageable.TakeDamage(damage);
                StartCoroutine(CoolDownBodyDamage());
            }
        }
        catch (System.Exception)
        {

            throw;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        try
        {
            DamageableBase damageable = collision.transform.GetComponent<DamageableBase>();
            if (damageable && !onCoolDown)
            {
                damageable.TakeDamage(damage);
                StartCoroutine(CoolDownBodyDamage());
            }
        }
        catch (System.Exception)
        {

            throw;
        }
    }

    private IEnumerator CoolDownBodyDamage()
    {
        onCoolDown = true;
        yield return new WaitForSeconds(damageDelay);
        onCoolDown = false;
    }
}
