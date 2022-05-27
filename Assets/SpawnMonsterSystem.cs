using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMonsterSystem : MonoBehaviour
{
    [SerializeField] private float spawnInterval;
    [SerializeField] private float waveInterval;
    [SerializeField] private int numberOfWaves;
    [SerializeField] private float buffPercentBetweenWaves;
    [SerializeField] private List<MonsterPortal> portals;
    [SerializeField] private List<MonsterData> monsterDataList;

    public static SpawnMonsterSystem instance;

    private Monster monster;
    private List<Monster> monsterList = new List<Monster>();
    private int currentWave = 0;

    //private bool isAbleToSpawn = true;
    //private bool isInWave = true;

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

    private void Start()
    {
        StartCoroutine(SpawnMonsterWave());
    }

    private IEnumerator SpawnMonsterWave()
    {
        int portalsOut = 0;
        for (int i = 0; i < portals.Count; i++)
        {
            if (portals[i].canSpawnMore)
            {
                SpawnMonster(monsterDataList[0], portals[i].portalTransform.position, currentWave * buffPercentBetweenWaves);
                portals[i].numberOfSpawnedMonster++;
                yield return new WaitForSeconds(spawnInterval);
            }
            else
            {
                portalsOut++;
            }
        }

        if (portalsOut == portals.Count)
        {
            currentWave++;
            if (currentWave < numberOfWaves)
            {
                for (int i = 0; i < portals.Count; i++)
                {
                    portals[i].Reset();
                }
                yield return new WaitForSeconds(waveInterval);
                StartCoroutine(SpawnMonsterWave());
                yield break;
            }
        }
        else
        {
            StartCoroutine(SpawnMonsterWave());
        }
        
    }

    //private IEnumerator WaveCoolDown(float time)
    //{
    //    isInWave = false;
    //    yield return new WaitForSeconds(time);
    //    isInWave = true;
    //}

    //private IEnumerator SpawnCoolDown(float time)
    //{
    //    isAbleToSpawn = false;
    //    yield return new WaitForSeconds(time);
    //    isAbleToSpawn = true;
    //}

    public void SpawnMonster(MonsterData monsterData, Vector2 spawnPosition, float buff)
    {
        monster = GetFreeMonster();
        if (monster)
        {
            //Monster keeps beforelife-weapon 
            monster.Init(monsterData, buff);
            monster.gameObject.transform.SetPositionAndRotation(spawnPosition, Quaternion.identity);
            monster.gameObject.SetActive(true);
            return;
        }
        monster = Instantiate(monsterData.Prefab, spawnPosition, Quaternion.identity, gameObject.transform);
        monster.Init(monsterData, buff);
        monsterList.Add(monster);
    }

    private Monster GetFreeMonster()
    {
        for (int i = 0; i < monsterList.Count; i++)
        {
            if (monsterList[i].gameObject.activeInHierarchy == false)
            {
                return monsterList[i];
            }
        }
        return null;
    }
}

[System.Serializable]
public class MonsterPortal
{
    public Transform portalTransform;
    public int numberOfMonster;
    public int numberOfSpawnedMonster;
    public bool canSpawnMore => numberOfSpawnedMonster < numberOfMonster;
    public void Reset() => numberOfSpawnedMonster = 0;
}


