using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detect : MonoBehaviour
{
    [SerializeField] protected TeamDefine ownerTeamDefine;
    protected bool enemyDetected = false;
    public bool EnemyDetected => enemyDetected;

    protected List<Transform> enemiesDetected = new List<Transform>();

    //Hold
    protected virtual void Update()
    {
        RefreshEnemiesDetected();

        if (enemiesDetected.Count <= 0)
        {
            enemyDetected = false;
        }
        else
        {
            enemyDetected = true;
        }
    }

    //Hold
    protected virtual void RefreshEnemiesDetected()
    {
        if (enemiesDetected.Count <= 0)
            return;
        for (int i = 0; i < enemiesDetected.Count; i++)
        {
            if (!enemiesDetected[i].gameObject.activeInHierarchy)
            {
                enemiesDetected.RemoveAt(i);
                RefreshEnemiesDetected();
                return;
            }
        }
    }

    //Done  Inherit-Update
    public virtual Transform GetClosestEnemy()
    {
        if (enemiesDetected.Count >= 1 && !enemiesDetected[0].gameObject.activeInHierarchy)
        {
            enemiesDetected.RemoveAt(0);
            return GetClosestEnemy();
        }

        if (enemiesDetected.Count >=1 && enemiesDetected[0])
        {
            Transform closestEnemy = enemiesDetected[0];
            for (int i = 1; i < enemiesDetected.Count; i++)
            {
                again:
                if (enemiesDetected.Count > i)
                {
                    if (!enemiesDetected[i].gameObject.activeInHierarchy)
                    {
                        enemiesDetected.RemoveAt(i);
                        goto again;
                    }
                }
                else
                {
                    goto result;
                }

                if (Vector2.Distance(transform.position, enemiesDetected[i].position) < Vector2.Distance(transform.position, closestEnemy.position))
                {
                    closestEnemy = enemiesDetected[i];
                }
            }
            result:
            return closestEnemy;
        }
        return null;
    }

    //Done  Inherit-Update
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        TeamDefine teamDefine = collision.GetComponent<TeamDefine>();
        if (teamDefine)
        {
            if (ownerTeamDefine.Team.IsOpponent(teamDefine.Team))
            {
                enemyDetected = true;
                if (!enemiesDetected.Contains(collision.transform))
                    enemiesDetected.Add(collision.transform);
                return;
            }
        }
        //enemyDetected = false;
    }

    //Done  Inherit-Update
    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        TeamDefine teamDefine = collision.GetComponent<TeamDefine>();
        if (teamDefine)
        {
            if (ownerTeamDefine.Team.IsOpponent(teamDefine.Team))
            {
                if (enemiesDetected.Contains(collision.transform))
                    enemiesDetected.Remove(collision.transform);
            }
        }
    }
}
