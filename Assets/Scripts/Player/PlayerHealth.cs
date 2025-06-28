using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PlayerDeath))]
public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private float _colorChangeDuration = 0.2f;
    [SerializeField] private Color _damageColor = Color.red;
    private PlayerDeath _playerDeath;
    private int _health;
    private SpriteRenderer _spriteRenderer;
    private Color _originalColor;

    public event Action<int> OnHealthChanged;
    public int MaxHealth => _maxHealth;
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originalColor = _spriteRenderer.color;
        _health = _maxHealth;
        _playerDeath = GetComponent<PlayerDeath>();
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
            _playerDeath.Die(this);
        }
        OnHealthChanged?.Invoke(_health);
    }

    public void Initialize()
    {
        _health = _maxHealth;
        OnHealthChanged?.Invoke(_health);
    }

    private IEnumerator FlashOnTakenDamage()
    {
        _spriteRenderer.color = _damageColor;
        yield return new WaitForSeconds(_colorChangeDuration);
        _spriteRenderer.color = _originalColor;
    }
}
