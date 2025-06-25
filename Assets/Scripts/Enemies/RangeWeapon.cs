using System.Collections;
using UnityEngine;

public class RangeWeapon : MonoBehaviour
{
    [SerializeField] private GameObject _selectedProjectile;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _reloadTime = 2.0f;
    [field: SerializeField] public bool IsReloaded { get; private set; } = true;

    public void SetProjectileTo(GameObject targetProjectile)
    {
        _selectedProjectile = targetProjectile;
    }

    public void LaunchProjectileTo(Transform targetTransform)
    {
        if (!IsReloaded)
            return;


        GameObject instance = Instantiate(_selectedProjectile, _spawnPoint.position, Quaternion.identity);

        if (instance != null)
        {
            instance.GetComponent<IProjectable>().SetTargetTo(targetTransform);
            StartCoroutine(ReloadWeapon());
        }
    }

    private IEnumerator ReloadWeapon()
    {
        IsReloaded = false;
        yield return new WaitForSeconds(_reloadTime);
        IsReloaded = true;
    }
}
