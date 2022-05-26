using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class DetectSystem : MonoBehaviour
{
    [SerializeField] private TeamDefine ownerTeamDefine;
    [SerializeField] private List<DetectWithCondition> detectWithConditionList;

    [Header("Direct control")]
    [SerializeField] private PointingDirection pointingDirection;
    [SerializeField] private AIDestinationSetter aiDestinationSetter;


    private Transform currentEnemy;

    public Transform GetClosestEnemy()
    {
        if (detectWithConditionList.Count > 0)
        {
            for (int i = 0; i < detectWithConditionList.Count; i++)
            {
                if (detectWithConditionList[i] && detectWithConditionList[i].EnemyDetected && detectWithConditionList[i].GetClosestEnemy() != null)
                    return detectWithConditionList[i].GetClosestEnemy();
            }
        }

        return TeamTarget.instance.GetTargetList(ownerTeamDefine.Team)[0];
    }

    private void FixedUpdate()
    {
        Transform enemy = GetClosestEnemy();
        if (currentEnemy != enemy)
        {
            currentEnemy = enemy;
            pointingDirection.SetTarget(currentEnemy);
            aiDestinationSetter.target = currentEnemy;
        }
    }
}
