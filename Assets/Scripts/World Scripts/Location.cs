using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    public delegate void OnEntitySpawn(GameObject currentEntity);
    public event OnEntitySpawn OnEntitySpawnEvent;

    [SerializeField] private List<Transform> heroPositions;

    [SerializeField] private List<GameObject> locationEntities;
    private GameObject currentEntity;

    void OnEnable()
    {
        SpawnEntity(0);
    }

    private void SpawnEntity(int index)
    {
        currentEntity = Instantiate(locationEntities[index]);
        if (currentEntity)
        {
            OnEntitySpawnEvent?.Invoke(currentEntity);
        }
    }
}
