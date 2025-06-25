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
    private RangeWeapon _weapon;
    private Transform _detectedPlayer;
    private Transform _originTransform;
    private bool _isNextProjectile = true;
    
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
    }

    private void Update()
    {
        _detectedPlayer = DetectPlayer();
        if (_detectedPlayer != null)
        {
            AttackPlayer();
        }
    }
    private Transform DetectPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(_originTransform.position, Vector2.right, _detectionDistance, _layerMask);
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

}
