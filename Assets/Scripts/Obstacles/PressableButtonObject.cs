using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PressableButtonObject : MonoBehaviour
{
    [SerializeField] private Color _pressColor = Color.gray;
    [SerializeField] private float _colorChangeDuration = 0.5f;
    private Color _originalColor;
    private SpriteRenderer _spriteRenderer;
    private bool _isPressedOnce = false;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originalColor = _spriteRenderer.color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_isPressedOnce)
        {
            _isPressedOnce = true;
            StartCoroutine(FlashOnPressed());
        }
    }

    private IEnumerator FlashOnPressed()
    {
        _spriteRenderer.color = _pressColor;
        yield return new WaitForSeconds(_colorChangeDuration);
        _spriteRenderer.color = _originalColor;
    }
}
