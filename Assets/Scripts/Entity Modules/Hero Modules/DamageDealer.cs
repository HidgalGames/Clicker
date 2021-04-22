using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DamageDealer : MonoBehaviour
{
    public AttackSpeed Aspeed { get; }
    public Damage Damage { get; }

    public delegate bool DoDamage(float damage);
    public event DoDamage DoDamageEvent;

    private bool autoAttackIsActive = false;

    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        CheckAutoAttack();
    }

    public void UpgradeStat(StatType type, int levelsToAdd)
    {
        switch (type)
        {
            case StatType.AtackSpeed:
                Aspeed.UpgradeStat(levelsToAdd);
                animator.SetFloat("Aspeed", Aspeed.Value);
                CheckAutoAttack();
                break;

            case StatType.Damage:
                Damage.UpgradeStat(levelsToAdd);
                break;
        }
    }

    private void CheckAutoAttack()
    {
        if (!autoAttackIsActive)
        {
            if (Aspeed.Value > 0f)
            {
                DamageableEntity dEntity = Location.Instance.GetDamageableEntity();
                if (dEntity)
                {
                    StartCoroutine(AutoAttackProcess());
                    autoAttackIsActive = true;

                    DoDamageEvent += dEntity.TakeDamage;
                    dEntity.OnEntityDeathEvent += StopAutoAttack;
                }
            }
        }
    }

    private void StopAutoAttack()
    {
        if (autoAttackIsActive)
        {
            autoAttackIsActive = false;
            StopCoroutine(AutoAttackProcess());
        }
    }

    private IEnumerator AutoAttackProcess()
    {
        while (autoAttackIsActive)
        {
            yield return new WaitForSeconds(1f / Aspeed.Value);
            float damage = CalculateDamage();
            DoDamageEvent?.Invoke(damage);
        }
    }

    private float CalculateDamage()
    {
        return Damage.Value;
    }
}
