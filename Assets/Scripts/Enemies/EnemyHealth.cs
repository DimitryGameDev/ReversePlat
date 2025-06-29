using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private float _colorChangeDuration = 0.2f;
    [SerializeField] private Color _damageColor = Color.red;
    private int _health;
    private SpriteRenderer _spriteRenderer;
    private Color _originalColor;


    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originalColor = _spriteRenderer.color;
        _health = _maxHealth;
    }
    public void TakeDamage(int damage)
    {
        if (_health > damage)
        {
            _health -= damage;
            StartCoroutine(FlashOnTakenDamage());
        }
        else
        {
            _health = 0;
        }
        Debug.Log("Enemy HP is " + _health);
        // If health has dropped to zero or below, remove the enemy from the scene
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator FlashOnTakenDamage()
    {
        _spriteRenderer.color = _damageColor;
        yield return new WaitForSeconds(_colorChangeDuration);
        _spriteRenderer.color = _originalColor;
    }
}
