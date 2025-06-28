using UnityEngine;

public class HoldableButtonObject : MonoBehaviour
{
    [SerializeField] private Door _assignedDoor;
    [SerializeField] private Color _pressColor = Color.gray;
    private Color _originalColor;
    private SpriteRenderer _spriteRenderer;


    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originalColor = _spriteRenderer.color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamageable>(out _))
        {
            if (_assignedDoor != null)
                _assignedDoor.Open();
            _spriteRenderer.color = _pressColor;
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamageable>(out _))
        {
            if (_assignedDoor != null)
                    _assignedDoor.Close();

            _spriteRenderer.color = _originalColor;
        }

    }

}
