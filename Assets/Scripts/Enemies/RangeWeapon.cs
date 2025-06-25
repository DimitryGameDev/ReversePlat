using UnityEngine;

public class RangeWeapon : MonoBehaviour
{
    [SerializeField] private GameObject _selectedProjectile;

    public void SetProjectileTo(GameObject targetProjectile)
    {
        _selectedProjectile = targetProjectile;
    }

    public void LaunchProjectileTo(Transform targetTransform)
    {
        GameObject instance = Instantiate(_selectedProjectile);

        if (instance != null)
        {
            instance.GetComponent<IProjectable>().SetTargetTo(targetTransform);
        }
    }
}
