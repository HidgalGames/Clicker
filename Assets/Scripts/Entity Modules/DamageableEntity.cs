using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DamageableEntity : MonoBehaviour
{
    private Animator animator;

    private float currentHealth;
    [SerializeField] private float maxHealth = 10f;

<<<<<<< Updated upstream
    private bool isDead = false;
=======
    [HideInInspector] public bool isDead = false;
>>>>>>> Stashed changes

    public delegate void OnHealthChange(float currentHealth);
    public event OnHealthChange OnHealthChangeEvent;

<<<<<<< Updated upstream
=======
    public delegate void OnEntityDeath();
    public event OnEntityDeath OnEntityDeathEvent;

    private SphereCollider col;

>>>>>>> Stashed changes
    void Awake()
    {
        animator = GetComponent<Animator>();
        TouchDetector.Instance.OnTouchEvent += OnTouchAction;
<<<<<<< Updated upstream
=======

        col = GetComponent<SphereCollider>(); //Для Теста
>>>>>>> Stashed changes
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

<<<<<<< Updated upstream
=======
    void OnMouseDown()
    {
        if (isDead)
        {
            Respawn(); // Исключительно для теста
        }
    }

>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
            Respawn(); // Исключительно для теста
=======
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
=======

        OnEntityDeathEvent?.Invoke();

        col.enabled = true;
>>>>>>> Stashed changes
    }

    private void Respawn()
    {
<<<<<<< Updated upstream
        ChangeHealth(maxHealth);
        isDead = false;
        animator.SetBool("IsDead", false);
=======
        animator.SetBool("IsDead", false);
        col.enabled = false;
        ChangeHealth(maxHealth);

        //isDead = false;
>>>>>>> Stashed changes
    }
}
