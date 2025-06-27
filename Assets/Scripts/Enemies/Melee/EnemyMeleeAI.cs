using UnityEngine;

[RequireComponent(typeof(MeleeAttack))]
public class EnemyMeleeAI : MonoBehaviour
{
    [Header("AI Settings")]
    [SerializeField] private Transform target;
    [SerializeField] private float detectRadius = 5f;
    [SerializeField] private float attackDistance = 1f;
    
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float leftBoundaryX = -10f;
    [SerializeField] private float rightBoundaryX = 10f;

    private MeleeAttack melee;
    private bool playerDetected = false;

    private void Awake()
    {
        melee = GetComponent<MeleeAttack>();
        if (target == null)
        {
            var playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null)
                target = playerObj.transform;
            else
                Debug.LogError("EnemyMeleeAI: не найден объект с тегом 'Player'.");
        }
    }

    private void Update()
    {
        if (target == null)
            return;

        float dist = Vector2.Distance(transform.position, target.position);

      
        if (!playerDetected && dist <= detectRadius)
            playerDetected = true;
        if (!playerDetected)
            return;

        
        if (target.position.x < leftBoundaryX || target.position.x > rightBoundaryX)
            return;

 
        FlipTowards(target.position.x);

        
        if (dist <= attackDistance)
        {
            melee.Attack();
            return;  
        }

        
        float targetX = Mathf.Clamp(target.position.x, leftBoundaryX, rightBoundaryX);
        Vector2 moveTarget = new Vector2(targetX, transform.position.y);
        transform.position = Vector2.MoveTowards(
            transform.position,
            moveTarget,
            moveSpeed * Time.deltaTime
        );
    }

    private void FlipTowards(float targetX)
    {
        Vector3 ls = transform.localScale;
        ls.x = (targetX < transform.position.x) 
               ? Mathf.Abs(ls.x) 
               : -Mathf.Abs(ls.x);
        transform.localScale = ls;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(
            new Vector3(leftBoundaryX, transform.position.y - 1f, 0f),
            new Vector3(leftBoundaryX, transform.position.y + 1f, 0f)
        );
        Gizmos.DrawLine(
            new Vector3(rightBoundaryX, transform.position.y - 1f, 0f),
            new Vector3(rightBoundaryX, transform.position.y + 1f, 0f)
        );
    }
}