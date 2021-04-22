using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DamageableEntity : MonoBehaviour
{
    private Animator animator;

    private float currentHealth;
    [SerializeField] private float maxHealth = 10f;

    [HideInInspector] public bool isDead = false;

    public delegate void OnHealthChange(float currentHealth);
    public event OnHealthChange OnHealthChangeEvent;

    public delegate void OnEntityDeath();
    public event OnEntityDeath OnEntityDeathEvent;

    private SphereCollider col;

    void Awake()
    {
        animator = GetComponent<Animator>();
        TouchDetector.Instance.OnTouchEvent += OnTouchAction;

        col = GetComponent<SphereCollider>(); //Для Теста
    }

    private void Start()
    {
        Respawn();
    }

    public void OnTouchAction(Vector3 touchPosition)
    {
        if (TakeDamage(1f))
        {
            DamageParticlesEmitter.Instance.PlayDamageParticle(touchPosition);
        }
    }

    public bool TakeDamage(float damage)
    {
        if (!isDead)
        {
            ChangeHealth(currentHealth - damage);
            animator.Play("TakeDamage", 0);
            return true;
        }
        else
        {
            return false;
        }
    }

    private void ChangeHealth(float newHealthValue)
    {
        currentHealth = newHealthValue;
        Mathf.Clamp(currentHealth, 0f, maxHealth);

        OnHealthChangeEvent?.Invoke(currentHealth);

        if (currentHealth == 0f)
        {
            Death();
        }
    }

    private void Death()
    {
        isDead = true;
        animator.SetBool("IsDead", true);

        OnEntityDeathEvent?.Invoke();

        col.enabled = true;
    }

    private void Respawn()
    {
        animator.SetBool("IsDead", false);
        col.enabled = false;
        ChangeHealth(maxHealth);

        //isDead = false;
    }
}
