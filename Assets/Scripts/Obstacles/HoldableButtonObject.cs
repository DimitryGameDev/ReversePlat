using System.Collections;
using UnityEngine;

public class HoldableButtonObject : MonoBehaviour
{
    [SerializeField] private Door _assignedDoor;
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

    //TODO: to change
    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (!_isPressedOnce)
    //     {
    //         if(_assignedDoor != null)
    //             _assignedDoor.Open();

    //         _isPressedOnce = true;
    //         StartCoroutine(FlashOnPressed());
    //     }
    // }


    // private IEnumerator FlashOnPressed()
    // {
    //     _spriteRenderer.color = _pressColor;
    //     yield return new WaitForSeconds(_colorChangeDuration);
    //     _spriteRenderer.color = _originalColor;
    // }
}
