using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthGUI : MonoBehaviour
{
    private Text healthText;
    public Location CurrentLocation;

    private DamageableEntity currentDamageableEntity;

    void Awake()
    {
        healthText = GetComponent<Text>();
    }

    void Start()
    {
        CurrentLocation.OnEntitySpawnEvent += SetCurrentEntity;
    }

    private void SetCurrentEntity(GameObject entityObject)
    {
        currentDamageableEntity = entityObject.GetComponent<DamageableEntity>(); 
        if (currentDamageableEntity)
        {
            currentDamageableEntity.OnHealthChangeEvent += OnHealthChanged;
        }
        else
        {
            healthText.text = "";
        }
    }

    void OnHealthChanged(float health)
    {
        if(health > 0)
        {
            healthText.text = ((int)health).ToString() + "HP";
        }
        else
        {
            healthText.text = "";
        }
    }
}
