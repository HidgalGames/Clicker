using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Entity : MonoBehaviour
{
    private bool canBeDamaged = false;
    private Damageable damageableBehaviour;

    [HideInInspector] public Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void DoOnTouchAction(Vector3 touchPosition)
    {
        if (canBeDamaged)
        {
            damageableBehaviour.TakeDamage(1f);
        }
    }

    public void AddDamageableBehaviour(Damageable behaviour)
    {
        canBeDamaged = true;
        damageableBehaviour = behaviour;
    }
}
