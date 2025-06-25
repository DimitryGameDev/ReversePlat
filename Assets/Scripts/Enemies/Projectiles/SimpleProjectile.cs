using UnityEngine;

public class SimpleProjectile : Projectile, IProjectable
{
    private Vector2 _direction;

    private void Update()
    {
        
    }
    public void SetTargetTo(Transform target)
    {
        _direction = (target.position - transform.position).normalized;
    }
}
