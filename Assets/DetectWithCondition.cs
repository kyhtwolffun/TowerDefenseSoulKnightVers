using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectWithCondition : MonoBehaviour
{
    [SerializeField] private TeamDefine ownerTeamDefine;
    private bool enemyDetected = false;
    public bool EnemyDetected => enemyDetected;
    [SerializeField] private EnumCondition condition;
    [SerializeField] private bool ignoreOthers;

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
                if (enemiesDetected[i].mainPriorityPoint > closestEnemy.mainPriorityPoint)
                {
                    closestEnemy = enemiesDetected[i];
                    continue;
                }
                else if (enemiesDetected[i].mainPriorityPoint == closestEnemy.mainPriorityPoint)
                {
                    if (enemiesDetected[i].subPriorityPoint > closestEnemy.subPriorityPoint)
                    {
                        closestEnemy = enemiesDetected[i];
                        continue;
                    }
                    else if (enemiesDetected[i].subPriorityPoint == closestEnemy.subPriorityPoint)
                    {
                        if (enemiesDetected[i].referPoint > closestEnemy.referPoint)
                        {
                            closestEnemy = enemiesDetected[i];
                            continue;
                        }
                        else if (enemiesDetected[i].referPoint == closestEnemy.referPoint
                            && Vector2.Distance(transform.position, enemiesDetected[i].transform.position) < Vector2.Distance(transform.position, closestEnemy.transform.position))
                        {
                            closestEnemy = enemiesDetected[i];
                        }
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

            if (ownerTeamDefine.Team.IsOpponent(teamDefine.Team))
            {
                enemy.transform = collision.transform;

                if (condition.isObjectTypeConditionFound(teamDefine.ObjectType, out int mainPriorityPoint, out int subPriorityPoint))
                {
                    if (enemy.mainPriorityPoint < mainPriorityPoint)
                    {
                        enemy.mainPriorityPoint = mainPriorityPoint;
                        enemy.subPriorityPoint = subPriorityPoint;
                    }
                    enemy.referPoint++;
                }
                
                if (condition.isTowerTypeCondition)
                {
                    Tower tower = collision.GetComponent<Tower>();
                    if (tower && condition.isTowerTypeConditionFound(tower.TowerType, out int _mainPriorityPoint, out int _subPriorityPoint))
                    {
                        if (enemy.mainPriorityPoint < _mainPriorityPoint)
                        {
                            enemy.mainPriorityPoint = _mainPriorityPoint;
                            enemy.subPriorityPoint = _subPriorityPoint;
                        }
                        enemy.referPoint++;
                    }
                }

                if (ignoreOthers && enemy.mainPriorityPoint == 0)
                    return;

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
            if (ownerTeamDefine.Team.IsOpponent(teamDefine.Team))
            {
                if (IsEnemiesDetectedContainTransform(collision.transform))
                    RemoveTransformFromEnemiesDetected(collision.transform);
                return;
            }
        }
    }
}

public class priorityEnemyDetected
{
    public int mainPriorityPoint;
    public int subPriorityPoint;
    public int referPoint;
    public Transform transform;
    public priorityEnemyDetected()
    {
        mainPriorityPoint = 0;
        subPriorityPoint = 0;
        referPoint = 0;
    }
}
