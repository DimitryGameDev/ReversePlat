using System.Collections;
using UnityEngine;

public class DamagingObject : MonoBehaviour
{
    [SerializeField] private int _damage = 50;
    [SerializeField] private float _applyDamageDelay = 2f;
    private bool _canApplyDamage = true;
    private IPlayerDamageable _playerDamageable;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<IPlayerDamageable>(out IPlayerDamageable playerDamageable))
        {
            _playerDamageable = playerDamageable;
            if (_canApplyDamage)
            {
                _playerDamageable.TakeDamage(_damage);
                StartCoroutine(WaitForApplyDamageDelay());
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<IPlayerDamageable>(out IPlayerDamageable playerDamageable))
        {
            if (_playerDamageable == playerDamageable)
            {
                if (_canApplyDamage)
                {
                    _playerDamageable.TakeDamage(_damage);
                    StartCoroutine(WaitForApplyDamageDelay());
                }
                _playerDamageable = null;
            }
        }
    }

    private void FixedUpdate()
    {
        if (_playerDamageable != null && _canApplyDamage)
        {
            _playerDamageable.TakeDamage(_damage);
            StartCoroutine(WaitForApplyDamageDelay());
        }
    }

    private IEnumerator WaitForApplyDamageDelay()
    {
        _canApplyDamage = false;
        yield return new WaitForSeconds(_applyDamageDelay);
        _canApplyDamage = true;
    }
}
