using UnityEngine;

[RequireComponent(typeof(MeleeAttack))]
public class PlayerMeleeInput : MonoBehaviour
{
    [SerializeField] private KeyCode attackKey = KeyCode.Mouse0;
    private MeleeAttack melee;

    private void Awake()
    {
        melee = GetComponent<MeleeAttack>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(attackKey))
        {
            melee.Attack();
        }
           
        
    }
}