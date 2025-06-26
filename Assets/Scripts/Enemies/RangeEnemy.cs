using UnityEngine;

public enum ProjectileType
{
    SimpleProjectile,
    ChasingProjectile
}

[RequireComponent(typeof(RangeWeapon))]
public class RangeEnemy : MonoBehaviour
{
    [SerializeField] private GameObject _simpleProjectile;
    [SerializeField] private GameObject _chasingProjectile;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _detectionDistance = 10.0f;
    [SerializeField] private ProjectileType _projectileType = ProjectileType.SimpleProjectile;
    [SerializeField] private Transform _pointA;
    [SerializeField] private Transform _pointB;
    [SerializeField] private float _movementSpeed = 0.5f;
    private Transform _targetPoint;
    private RangeWeapon _weapon;
    private Transform _detectedPlayer;
    private Transform _originTransform;
    private Rigidbody2D _rb;
    private SpriteRenderer _sprite;
    // private bool _isNextProjectile = true;
    private void Start()
    {
        _weapon = GetComponent<RangeWeapon>();
        switch (_projectileType)
        {
            case ProjectileType.SimpleProjectile:
                _weapon.SetProjectileTo(_simpleProjectile);
                break;
            case ProjectileType.ChasingProjectile:
                _weapon.SetProjectileTo(_chasingProjectile);
                break;
        }

        _originTransform = transform;
        
        _sprite = GetComponent<SpriteRenderer>();
        _targetPoint = _pointB;
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _detectedPlayer = DetectPlayer();
        if (_detectedPlayer != null)
        {
            AttackPlayer();
        }
        else
        {
            Move();
        }
    }
    private Transform DetectPlayer()
    {
        Vector2 direction = _sprite.flipX ? Vector2.left : Vector2.right;
        if (!_sprite.isVisible)
            return null;
        RaycastHit2D hit = Physics2D.Raycast(_originTransform.position, direction,
             _detectionDistance, _layerMask);
        if (hit.collider != null)
        {
            return hit.collider.transform;
        }
        return null;
    }

    private void AttackPlayer()
    {
        if (_weapon.IsReloaded)
        {
            _weapon.LaunchProjectileTo(_detectedPlayer);
        }
    }

    private void Move()
    {
        Vector2 newPos = Vector2.MoveTowards(_rb.position, _targetPoint.position, Time.fixedDeltaTime * _movementSpeed);
        _rb.MovePosition(newPos);
        if (Vector2.Distance(_originTransform.position, _targetPoint.position) < 0.001f)
        {
            if (_targetPoint == _pointB)
            {
                _targetPoint = _pointA;
                _sprite.flipX = true;
            }
            else
            {
                _targetPoint = _pointB;
                _sprite.flipX = false;
            }
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_pointA.position, 0.2f);
        Gizmos.DrawSphere(_pointB.position, 0.2f);
    }

}
