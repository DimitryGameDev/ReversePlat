using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MeleeAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private int attackDamage = 25;
    [SerializeField] private LayerMask hitLayers;
    [SerializeField] private float attacksPerSecond = 1f;

    private Animator animator;
    private float nextAttackTime = 0f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

 
    public void Attack()
    {
        if (Time.time < nextAttackTime) return;
            nextAttackTime = Time.time + 1f / attacksPerSecond;
       
        animator.SetTrigger("Attack");
       

        Collider2D[] hits = Physics2D.OverlapCircleAll(
            attackPoint.position, attackRange, hitLayers);
        foreach (var hit in hits)
        {
            hit.GetComponent<IDamageable>()?.TakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
