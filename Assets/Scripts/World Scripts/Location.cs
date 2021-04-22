using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    public static Location Instance;

    public delegate void OnEntitySpawn(GameObject currentEntity);
    public event OnEntitySpawn OnEntitySpawnEvent;

    [SerializeField] private List<Transform> heroPositions;

    [SerializeField] private List<GameObject> locationEntities;
    [SerializeField] private Transform entitySpawnParent;
    private GameObject currentEntity = null;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Instance = this;

        SpawnEntity(0);
    }

    public DamageableEntity GetDamageableEntity()
    {
        if(currentEntity)
        {
            DamageableEntity dEntity;
            if (currentEntity.TryGetComponent<DamageableEntity>(out dEntity))
            {
                return dEntity;
            }            
        }

        return null;
    }

    private void SpawnEntity(int index)
    {
        currentEntity = Instantiate(locationEntities[index], entitySpawnParent);
        if (currentEntity)
        {
            OnEntitySpawnEvent?.Invoke(currentEntity);

            DamageableEntity dEntity;
            if(currentEntity.TryGetComponent<DamageableEntity>(out dEntity))
            {
                dEntity.OnEntityDeathEvent += OnEntityDeath;
            }
        }
    }

    public void SpawnHeroes(List<GameObject> heroes)
    {

    }

    private void OnEntityDeath()
    {
        currentEntity = null;
        StartCoroutine(SpawnEntityAfterTime(1.5f, 0));
    }

    private IEnumerator SpawnEntityAfterTime(float timeInSec, int entityIndex)
    {
        yield return new WaitForSeconds(timeInSec);
        SpawnEntity(entityIndex);
    }
}
