using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject hitEffect;
    [SerializeField] float destroyEffectDelay;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject effect = Instantiate(hitEffect, (Vector2)transform.position, Quaternion.identity);
        Destroy(effect, destroyEffectDelay);
        Destroy(gameObject);
    }
}
