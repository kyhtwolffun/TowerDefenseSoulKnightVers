using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeBullet : Bullet
{
    [SerializeField] float spreadingExplodeDelay;
    [SerializeField] bool spreadingIfHit = false;

    [SerializeField] private BaseRangeAttack afterExlodeAttack;

    private void Start()
    {
        StartCoroutine(WaitForSpreadingExplode());
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        try
        {
            DamageableBase damageable = collision.transform.GetComponent<DamageableBase>();
            if (damageable)
                damageable.TakeDamage(damage);
        }
        catch (System.Exception)
        {

            throw;
        }

        if (spreadingIfHit)
            SpreadingExplode();
        else
            Explode();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        try
        {
            DamageableBase damageable = collision.transform.GetComponent<DamageableBase>();
            if (damageable)
                damageable.TakeDamage(damage);
        }
        catch (System.Exception)
        {

            throw;
        }

        if (spreadingIfHit)
            SpreadingExplode();
        else
            Explode();
    }

    private void Explode()
    {
        GameObject effect = Instantiate(hitEffect, (Vector2)transform.position, Quaternion.identity);
        Destroy(effect, destroyEffectDelay);
        Destroy(gameObject);
    }

    private IEnumerator WaitForSpreadingExplode()
    {
        yield return new WaitForSeconds(spreadingExplodeDelay);
        SpreadingExplode();
    }

    private void SpreadingExplode()
    {
        Explode();
        afterExlodeAttack?.Attack(transform, 0f, team);
    }
}
