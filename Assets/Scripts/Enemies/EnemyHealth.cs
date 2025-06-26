using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    private int _health;

    private void Start()
    {
        _health = _maxHealth;
    }
    public void TakeDamage(int damage)
    {
        if (_health >= damage)
        {
            _health -= damage;
        }
        else
        {
            _health = 0;
        }
        Debug.Log("Enemy HP is " + _health);
    }
}
