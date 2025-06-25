using UnityEngine;

public class ChasingProjectile : Projectile, IProjectable
{
    private Transform _chasingTarget;

    private void Update()
    {
        
    }
    public void SetTargetTo(Transform target)
    {
        _chasingTarget = target;
    }
}
