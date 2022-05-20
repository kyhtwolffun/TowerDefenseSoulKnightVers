using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeBullet : Bullet
{
    [SerializeField] GameObject explodeEffect;
    [SerializeField] float spreadingExplodeDelay;
    [SerializeField] bool spreadingIfHit = false;

    [SerializeField] private BaseRangeAttack afterExlodeAttack;

    private void Start()
    {
        StartCoroutine(WaitForSpreadingExplode());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (spreadingIfHit)
            SpreadingExplode();
        else
            Explode();
    }

    private void Explode()
    {
        GameObject effect = Instantiate(explodeEffect, (Vector2)transform.position, Quaternion.identity);
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
