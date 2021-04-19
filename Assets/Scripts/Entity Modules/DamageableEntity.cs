using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DamageableEntity : MonoBehaviour
{
    private Animator animator;

    private float currentHealth;
    [SerializeField] private float maxHealth = 10f;

    private bool isDead = false;

    public delegate void OnHealthChange(float currentHealth);
    public event OnHealthChange OnHealthChangeEvent;

    void Awake()
    {
        animator = GetComponent<Animator>();
        TouchDetector.Instance.OnTouchEvent += OnTouchAction;
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
            Respawn(); // Исключительно для теста
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
    }

    private void Respawn()
    {
        ChangeHealth(maxHealth);
        isDead = false;
        animator.SetBool("IsDead", false);
    }
}
