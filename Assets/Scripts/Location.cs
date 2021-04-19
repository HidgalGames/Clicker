using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    [SerializeField] private List<GameObject> entitiesList;
    [SerializeField] private Transform entitySpawnPoint;
    private GameObject currentEntity;

    public delegate void EntitySpawned(GameObject entity);
    public event EntitySpawned onEntitySpawn;

    void Start()
    {
        SpawnEntity();   
    }

    private void SpawnEntity()
    {
        currentEntity = Instantiate(entitiesList[0], entitySpawnPoint);
        onEntitySpawn?.Invoke(currentEntity);
    }

    public GameObject GetCurrentEntity()
    {
        return currentEntity;
    }
}
