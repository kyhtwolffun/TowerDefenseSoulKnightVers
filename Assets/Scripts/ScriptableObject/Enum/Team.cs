using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Team", menuName = "Data/ Team")]
public class Team : ScriptableObject
{
    [SerializeField] private List<Team> OpponentTeams;
    [SerializeField] private LayerMask teamLayerMask;
    [SerializeField] private LayerMask teamBulletLayerMask;

    public LayerMask TeamBulletLayerMask => teamBulletLayerMask;

    public bool IsOpponent(Team team)
    {
        for (int i = 0; i < OpponentTeams.Count; i++)
        {
            if (team == OpponentTeams[i])
                return true;
        }
        return false;
    }
}
