using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectWithCondition : MonoBehaviour
{
    [SerializeField] private TeamDefine ownerTeamDefine;
    private bool enemyDetected = false;
    public bool EnemyDetected => enemyDetected;
    [SerializeField] private EnumCondition condition;

    private List<priorityEnemyDetected> enemiesDetected = new List<priorityEnemyDetected>();
    private bool IsEnemiesDetectedContainTransform(Transform transform)
    {
        for (int i = 0; i < enemiesDetected.Count; i++)
        {
            if (transform == enemiesDetected[i].transform)
                return true;
        }
        return false;
    }

    private void RemoveTransformFromEnemiesDetected(Transform transform)
    {
        for (int i = 0; i < enemiesDetected.Count; i++)
        {
            if (transform == enemiesDetected[i].transform)
            {
                enemiesDetected.RemoveAt(i);
                return;
            }
        }
    }

    //Hold
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

        for (int i = 0; i < enemiesDetected.Count; i++)
        {
            Debug.Log(gameObject.ToString() + "with enemiesDetected: " + "obj name: " + enemiesDetected[i].transform.gameObject.ToString() + "_priorityPoint: " + enemiesDetected[i].priorityPoint);
        }
    }

    //Hold
    private void RefreshEnemiesDetected()
    {
        if (enemiesDetected.Count <= 0)
            return;
        for (int i = 0; i < enemiesDetected.Count; i++)
        {
            if (!enemiesDetected[i].transform.gameObject.activeInHierarchy)
            {
                enemiesDetected.RemoveAt(i);
                RefreshEnemiesDetected();
                return;
            }
        }
    }

    public Transform GetClosestEnemy()
    {
        RefreshEnemiesDetected();
        
        if (enemiesDetected.Count >= 1)
        {
            priorityEnemyDetected closestEnemy = enemiesDetected[0];
            for (int i = 1; i < enemiesDetected.Count; i++)
            {
                if (enemiesDetected[i].priorityPoint > closestEnemy.priorityPoint)
                {
                    closestEnemy = enemiesDetected[i];
                    continue;
                }
                else if (enemiesDetected[i].priorityPoint == closestEnemy.priorityPoint)
                {
                    if (Vector2.Distance(transform.position, enemiesDetected[i].transform.position) < Vector2.Distance(transform.position, closestEnemy.transform.position))
                    {
                        closestEnemy = enemiesDetected[i];
                    }
                }
            }
            return closestEnemy.transform;
        }
        return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TeamDefine teamDefine = collision.GetComponent<TeamDefine>();
        if (teamDefine)
        {
            priorityEnemyDetected enemy = new priorityEnemyDetected();
            int priorityPoint;
            if (ownerTeamDefine.Team.IsOpponent(teamDefine.Team) && condition.isObjectTypeConditionFound(teamDefine.ObjectType, out priorityPoint))
            {
                enemy.priorityPoint += priorityPoint;
                enemy.transform = collision.transform;
                if (condition.isTowerTypeCondition)
                {
                    Tower tower = collision.GetComponent<Tower>();
                    if (tower && condition.isTowerTypeConditionFound(tower.TowerType, out priorityPoint))
                    {
                        enemy.priorityPoint += priorityPoint;
                        //goto detected;
                    }
                    //return;
                }
                //detected:
                enemyDetected = true;
                if (!IsEnemiesDetectedContainTransform(collision.transform))
                    enemiesDetected.Add(enemy);
                return;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        TeamDefine teamDefine = collision.GetComponent<TeamDefine>();
        if (teamDefine)
        {
            int priorityPoint;
            if (ownerTeamDefine.Team.IsOpponent(teamDefine.Team) && condition.isObjectTypeConditionFound(teamDefine.ObjectType, out priorityPoint))
            {
                if (condition.isTowerTypeCondition)
                {
                    Tower tower = collision.GetComponent<Tower>();
                    if (tower && condition.isTowerTypeConditionFound(tower.TowerType, out priorityPoint))
                    {
                        //goto found;
                    }
                    //return;
                }
                //found:
                if (IsEnemiesDetectedContainTransform(collision.transform))
                    RemoveTransformFromEnemiesDetected(collision.transform);
                return;
            }
        }
    }
}

public class priorityEnemyDetected
{
    public int priorityPoint;
    public Transform transform;
    public priorityEnemyDetected()
    {
        priorityPoint = 0;
    }
}
