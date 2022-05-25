using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItemSystem : MonoBehaviour
{
    [SerializeField] private Collectable collectablePrefab;
    [SerializeField] private List<WeaponData> weaponList;

    private Collectable collectable;
    private List<Collectable> CollectableList = new List<Collectable>();

    public static SpawnItemSystem instance;

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
        for (int i = 0; i < weaponList.Count; i++)
        {
            SpawnItem(weaponList[i], (Vector2)transform.position + Vector2.right * Random.Range(-5f,5f) + Vector2.up * Random.Range(-5f, 5f));
        }
    }

    public void SpawnItem(CollectableDataBase collectableData, Vector2 spawnPosition)
    {
        collectable = GetFreeCollectable();
        if (collectable)
        {
            collectable.InitCollectable(collectableData);
            collectable.gameObject.transform.SetPositionAndRotation(spawnPosition, Quaternion.identity);
            collectable.gameObject.SetActive(true);
            return;
        }
        collectable = Instantiate(collectablePrefab, spawnPosition, Quaternion.identity, gameObject.transform);
        collectable.InitCollectable(collectableData);
        CollectableList.Add(collectable);
    }

    private Collectable GetFreeCollectable()
    {
        for (int i = 0; i < CollectableList.Count; i++)
        {
            if (CollectableList[i].gameObject.activeInHierarchy == false)
            {
                return CollectableList[i];
            }
        }
        return null;
    }
}
