using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamTarget : MonoBehaviour
{
    [SerializeField] private List<TeamTargetList> teamTargetList;
    public List<TeamTargetList> TeamTargetList => teamTargetList;

    public static TeamTarget instance;
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public List<Transform> GetTargetList(Team team)
    {
        for (int i = 0; i < teamTargetList.Count; i++)
        {
            if (team == teamTargetList[i].team)
                return teamTargetList[i].target;
        }
        return null;
    }
}

[System.Serializable]
public struct TeamTargetList
{
    public Team team;
    public List<Transform> target;
}
