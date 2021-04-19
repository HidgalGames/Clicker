using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthGUI : MonoBehaviour
{
    private Text healthText;
    public Location CurrentLocation;

    private Damageable currentEntity;

    void Awake()
    {
        healthText = GetComponent<Text>();
    }

    void Start()
    {
        CurrentLocation.onEntitySpawn += SetCurrentEntity;
    }

    private void SetCurrentEntity(GameObject entity)
    {
        currentEntity = CurrentLocation.GetCurrentEntity().GetComponent<Damageable>();
        if (currentEntity)
        {
            currentEntity.onHealthChange += OnHealthChanged;
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
