using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerHealth : MonoBehaviour, IPlayerDamageable
{
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private float _colorChangeDuration = 0.2f;
    [SerializeField] private Color _damageColor = Color.red;
    private int _health;
    private SpriteRenderer _spriteRenderer;
    private Color _originalColor;
    

    private void Start()
    {
        _health = _maxHealth;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originalColor = _spriteRenderer.color;
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
        StartCoroutine(FlashOnTakenDamage());
        Debug.Log("Player HP is " + _health);
    }

    private IEnumerator FlashOnTakenDamage()
    {
        _spriteRenderer.color = _damageColor;
        yield return new WaitForSeconds(_colorChangeDuration);
        _spriteRenderer.color = _originalColor;
    }
}
