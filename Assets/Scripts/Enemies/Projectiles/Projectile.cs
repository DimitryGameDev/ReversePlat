using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _destroyDelay = 5.0f;
    [SerializeField] private float _damage = 20.0f;
    [SerializeField] protected float _speed = 3.0f;
    protected Transform _originTransform;
    private void Start()
    {
        _originTransform = transform;
        Destroy(this.gameObject, _destroyDelay);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<IDamageable>(out IDamageable Damageable))
        {
            Damageable.TakeDamage(_damage);
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        //TODO - casting some effect on destroy
    }
}
