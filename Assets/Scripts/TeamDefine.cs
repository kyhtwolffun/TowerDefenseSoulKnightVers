using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamDefine : MonoBehaviour
{
    [SerializeField] private Team team;
    public Team Team => team;
    [SerializeField] private ObjectType objectType;
    public ObjectType ObjectType => objectType;
}
