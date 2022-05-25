using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detect : MonoBehaviour
{
    [SerializeField] private TeamDefine ownerTeamDefine;
    private bool enemyDetected = false;
    public bool EnemyDetected => enemyDetected;

    private List<Transform> enemiesDetected = new List<Transform>();

    private void Update()
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

    private void RefreshEnemiesDetected()
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

    public Transform GetClosestEnemy()
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

    private void OnTriggerEnter2D(Collider2D collision)
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

    private void OnTriggerExit2D(Collider2D collision)
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
