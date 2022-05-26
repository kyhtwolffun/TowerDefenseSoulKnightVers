using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableRange : MonoBehaviour
{
    [SerializeField] private List<Team> availableTeam;

    private List<GameObject> availableCharacter = new List<GameObject>();
    public bool IsAbleToPlaceBuilding(GameObject gameObject)
    {
        for (int i = 0; i < availableCharacter.Count; i++)
        {
            if (gameObject == availableCharacter[i])
                return true;
        }
        return false;
    }

    public static PlaceableRange instance;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TeamDefine enteringTeam = collision.GetComponent<TeamDefine>();
        if (enteringTeam)
        {
            for (int i = 0; i < availableTeam.Count; i++)
            {
                if (enteringTeam.Team == availableTeam[i])
                {
                    //Debug.Log("Available for " + enteringTeam.Team.ToString());
                    if (!availableCharacter.Contains(collision.gameObject))
                    {
                        availableCharacter.Add(collision.gameObject);
                    }
                    return;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        TeamDefine enteringTeam = collision.GetComponent<TeamDefine>();
        if (enteringTeam)
        {
            for (int i = 0; i < availableTeam.Count; i++)
            {
                if (enteringTeam.Team == availableTeam[i])
                {
                    //Debug.Log("No more available for " + enteringTeam.Team.ToString());
                    if (availableCharacter.Contains(collision.gameObject))
                    {
                        availableCharacter.Remove(collision.gameObject);
                    }
                    return;
                }
            }
        }
    }
}
