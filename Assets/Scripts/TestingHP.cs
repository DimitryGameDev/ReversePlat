using UnityEngine;

public class TestingHP : MonoBehaviour, IDamageable
{
    public void TakeDamage(float damage)
    {
        Debug.Log("Damage taken " + damage);
    }
}
