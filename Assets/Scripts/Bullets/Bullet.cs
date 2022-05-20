using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject hitEffect;
    [SerializeField] protected float destroyEffectDelay;
    [SerializeField] private float destroyBulletDelay;
    [SerializeField] private bool rotateEffect;
    [SerializeField] protected Team team;

    public void SetTeam(Team ownerTeam)
    {
        team = ownerTeam;
    }

    private void Start()
    {
        if (destroyBulletDelay > 0)
            DestroyBullet();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TeamDefine teamDefine = collision.gameObject.GetComponent<TeamDefine>();
        if (teamDefine)
        {
            if (teamDefine.Team.IsOpponent(team))
            {
                goto hit;
            }
            return;
        }

        hit:
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
