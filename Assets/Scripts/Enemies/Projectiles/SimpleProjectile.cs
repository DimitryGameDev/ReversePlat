using UnityEngine;

public class SimpleProjectile : Projectile, IProjectable
{
    private Vector2 _direction;

    private void Update()
    {
        transform.Translate(_direction * Time.deltaTime * _speed);
    }
    public void SetTargetTo(Transform target)
    {
        //float height = target.GetComponent<Collider2D>().bounds.size.y;
        Vector3 offset = Vector2.zero;
        // offset = new Vector3(0f, height / 2, 0);
        _direction = (target.position - transform.position + offset).normalized;
    }
}
