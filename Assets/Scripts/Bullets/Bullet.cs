using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected int damage;
    public void SetDamage(int _damage)
    {
        damage = _damage;
    }
    [SerializeField] protected GameObject hitEffect;
    [SerializeField] protected float destroyEffectDelay;
    [SerializeField] private float destroyBulletDelay;
    [SerializeField] private bool rotateEffect;
    [SerializeField] protected Team team;

    public void SetTeam(Team ownerTeam)
    {
        team = ownerTeam;
        gameObject.layer = (int)Mathf.Log(team.TeamBulletLayerMask.value, 2);
    }

    private void Start()
    {
        if (destroyBulletDelay > 0)
            DestroyBullet();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        //hit
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

        GameObject effect = Instantiate(hitEffect, (Vector2)transform.position, Quaternion.identity);
        if (rotateEffect)
            effect.transform.SetPositionAndRotation((Vector2)transform.position, transform.rotation);
        Destroy(effect, destroyEffectDelay);
        Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
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

        GameObject effect = Instantiate(hitEffect, (Vector2)transform.position, Quaternion.identity);
        if (rotateEffect)
            effect.transform.SetPositionAndRotation((Vector2)transform.position, transform.rotation);
        Destroy(effect, destroyEffectDelay);
        Destroy(gameObject);
    }

    private void DestroyBullet()
    {
        Destroy(gameObject, destroyBulletDelay);
    }
}
