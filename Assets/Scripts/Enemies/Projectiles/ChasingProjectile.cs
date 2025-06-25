using UnityEngine;

public class ChasingProjectile : Projectile, IProjectable
{
    private Transform _chasingTarget;
    private float _chasingHeight;

    private void Update()
    {
        
        var direction = (_chasingTarget.position - _originTransform.position + new Vector3(0f, _chasingHeight / 2, 0)).normalized;
        transform.Translate(direction * Time.deltaTime * _speed);
    }
    public void SetTargetTo(Transform target)
    {
        _chasingHeight = target.GetComponent<Collider2D>().bounds.size.y;
        _chasingTarget = target;
    }
}
