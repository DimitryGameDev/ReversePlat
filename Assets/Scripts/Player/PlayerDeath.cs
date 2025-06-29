using System.Collections;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private Transform _respawnPoint;
    [SerializeField] private float _respawnDelay = 2f;
    private Collider2D _collider;
    private bool _isDead = false;
    public bool IsDead => _isDead;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    public void Die(PlayerHealth playerHealth)
    {
        if (!_isDead)
            StartCoroutine(RespawnWithDelay(playerHealth));

    }

    private IEnumerator RespawnWithDelay(PlayerHealth playerHealth)
    {
        _isDead = true;
        _collider.enabled = false;
        yield return new WaitForSeconds(_respawnDelay);
        
        _isDead = false;
        if (_respawnPoint == null)
        {
            Debug.LogError("Respawn Point is not specified. Player is respawned at (0,0)");
            transform.position = Vector2.zero;
        }
        else
        {
            transform.position = _respawnPoint.position;
        }
        _collider.enabled = true;
        playerHealth.Initialize();
    }
}
