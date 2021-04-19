using UnityEngine;

[RequireComponent(typeof(Entity))]
public class Damageable : MonoBehaviour
{
    private Entity entityComponent;

    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth = 10f;

    private bool isDead = false;

    public delegate void OnHealthChange(float currentHealth);
    public event OnHealthChange onHealthChange;

    void Awake()
    {
        entityComponent = GetComponent<Entity>();
        entityComponent.AddDamageableBehaviour(this);
    }

    private void Start()
    {
        Respawn();
    }

    public void TakeDamage(float damage)
    {
        if (!isDead)
        {
            ChangeHealth(currentHealth - damage);
            entityComponent.animator.Play("TakeDamage", 0);
        }
        else
        {
            Respawn();
        }
    }

    private void ChangeHealth(float newHealthValue)
    {
        currentHealth = newHealthValue;
        Mathf.Clamp(currentHealth, 0f, maxHealth);
        onHealthChange?.Invoke(currentHealth);

        if (currentHealth == 0f)
        {
            Death();
        }
    }

    private void Death()
    {
        isDead = true;
        entityComponent.animator.SetBool("IsDead", true);
    }

    private void Respawn()
    {
        ChangeHealth(maxHealth);
        isDead = false;
        entityComponent.animator.SetBool("IsDead", false);
    }
}
