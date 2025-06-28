using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private Image _totalHealth;
    [SerializeField] private Image _currentHealth;

    private void Awake()
    {
        _totalHealth.fillAmount = _playerHealth.MaxHealth / 10f;
        _currentHealth.fillAmount = _playerHealth.MaxHealth / 10f;
    }
    private void OnEnable()
    {
        _playerHealth.OnHealthChanged += OnPlayerHealthChange;
    }
    private void OnDisable()
    {
        _playerHealth.OnHealthChanged -= OnPlayerHealthChange;
    }
    private void OnPlayerHealthChange(int newValue)
    {
        _currentHealth.fillAmount = newValue / 10f;
    }
}
