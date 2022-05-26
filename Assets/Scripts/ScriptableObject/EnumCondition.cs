using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Condition", menuName = "Data/ Condition")]
public class EnumCondition : ScriptableObject
{
    [SerializeField] private PriorityCondition<ObjectType> objectType;
    public PriorityCondition<ObjectType> ObjectType => objectType;
    public bool isObjectTypeCondition => objectType.conditionList.Count > 0;
    public bool isObjectTypeConditionFound(ObjectType _objectType, out int priorityPoint)
    {
        if (!isObjectTypeCondition)
        {
            priorityPoint = 0;
            return true;
        }
        for (int i = 0; i < objectType.conditionList.Count; i++)
        {
            if (_objectType == objectType.conditionList[i])
            {
                priorityPoint = 1 * objectType.priority + (objectType.conditionList.Count - i);
                return true;
            }
        }
        priorityPoint = 0;
        return false;
    }

    [SerializeField] PriorityCondition<TowerType> towerType;
    public PriorityCondition<TowerType> TowerType => towerType;
    public bool isTowerTypeCondition => towerType.conditionList.Count > 0;
    public bool isTowerTypeConditionFound(TowerType _towerType, out int priorityPoint)
    {
        if (!isTowerTypeCondition)
        {
            priorityPoint = 0;
            return true;
        }
        for (int i = 0; i < towerType.conditionList.Count; i++)
        {
            if (_towerType == towerType.conditionList[i])
            {
                priorityPoint = 1 * towerType.priority + (towerType.conditionList.Count - i);
                return true;
            }
        }
        priorityPoint = 0;
        return false;
    }
}

[System.Serializable]
public struct PriorityCondition <T>
{
    public int priority;
    public List<T> conditionList;
}


