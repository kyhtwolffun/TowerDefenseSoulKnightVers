using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Condition", menuName = "Data/ Condition")]
public class EnumCondition : ScriptableObject
{
    [SerializeField] private PriorityCondition<ObjectType> objectType;
    public PriorityCondition<ObjectType> ObjectType => objectType;
    public bool isObjectTypeCondition => objectType.conditionList.Count > 0;
    public bool isObjectTypeConditionFound(ObjectType _objectType, out int mainPriorityPoint, out int subPriorityPoint)
    {
        if (!isObjectTypeCondition)
        {
            mainPriorityPoint = 0;
            subPriorityPoint = 0;
            return true;
        }
        for (int i = 0; i < objectType.conditionList.Count; i++)
        {
            if (_objectType == objectType.conditionList[i])
            {
                mainPriorityPoint = objectType.priority;
                subPriorityPoint = objectType.conditionList.Count - i;
                return true;
            }
        }
        mainPriorityPoint = 0;
        subPriorityPoint = 0;
        return false;
    }

    [SerializeField] PriorityCondition<TowerType> towerType;
    public PriorityCondition<TowerType> TowerType => towerType;
    public bool isTowerTypeCondition => towerType.conditionList.Count > 0;
    public bool isTowerTypeConditionFound(TowerType _towerType, out int mainPriorityPoint, out int subPriorityPoint)
    {
        if (!isTowerTypeCondition)
        {
            mainPriorityPoint = 0;
            subPriorityPoint = 0;
            return true;
        }
        for (int i = 0; i < towerType.conditionList.Count; i++)
        {
            if (_towerType == towerType.conditionList[i])
            {
                mainPriorityPoint = towerType.priority;
                subPriorityPoint = towerType.conditionList.Count - i;
                return true;
            }
        }
        mainPriorityPoint = 0;
        subPriorityPoint = 0;
        return false;
    }
}

[System.Serializable]
public struct PriorityCondition <T>
{
    public int priority;
    public List<T> conditionList;
}


