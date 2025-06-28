using System.Collections;
using UnityEngine;

public class DamagingObject : MonoBehaviour
{
    [SerializeField] private int _damage = 50;
    [SerializeField] private float _applyDamageDelay = 0.5f;
    [SerializeField] private float _damageForce = 5f;
    private bool _canApplyDamage = true;
    private IDamageable _damageable;
    private Vector2 _damageDirection;
    private Rigidbody2D _targetRB;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            _damageable = damageable;
            _damageDirection = collision.contacts[0].normal * -1;
            _targetRB = collision.rigidbody;
            if (_canApplyDamage)
            {
                ApplyDamage();
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            if (_damageable == damageable)
            {
                if (_canApplyDamage)
                {
                    ApplyDamage();
                }
                _damageable = null;
            }
        }
    }

    private void FixedUpdate()
    {
        if (_damageable != null && _canApplyDamage)
        {
            ApplyDamage();
        }
    }
    private void ApplyDamage()
    {
        _damageable.TakeDamage(_damage);
        _targetRB.AddForce(_damageDirection * _damageForce, ForceMode2D.Impulse);
        StartCoroutine(WaitForApplyDamageDelay());
    }
    private IEnumerator WaitForApplyDamageDelay()
    {
        _canApplyDamage = false;
        yield return new WaitForSeconds(_applyDamageDelay);
        _canApplyDamage = true;
    }
}
