using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthGUI : MonoBehaviour
{
    private Text healthText;

    private DamageableEntity currentDamageableEntity;

    void Awake()
    {
        healthText = GetComponent<Text>();
    }

    void Start()
    {
        Location.Instance.OnEntitySpawnEvent += SetCurrentEntity;
    }

    private void SetCurrentEntity(GameObject entityObject)
    {
        currentDamageableEntity = Location.Instance.GetDamageableEntity(); 
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
