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
    private Animator _animator;
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
        _animator = GetComponent<Animator>();
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
        Vector2 direction = _originTransform.right;
        if (!_sprite.isVisible)
            return null;
        // RaycastHit2D hit = Physics2D.Raycast(_originTransform.position, direction,
        //      _detectionDistance, _layerMask);

        RaycastHit2D hit = Physics2D.BoxCast(_originTransform.position, new Vector2(1f, 1f), 0f,
            direction, _detectionDistance, _layerMask);
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
            _animator.SetTrigger("Attack");
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
                _originTransform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                _targetPoint = _pointB;
                _originTransform.rotation = Quaternion.Euler(0, 0, 0);
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
