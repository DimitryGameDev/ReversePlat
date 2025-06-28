using System.Collections;
using UnityEngine;

public class FireObstacle : MonoBehaviour
{
    [SerializeField] private Transform _topPoint;
    [SerializeField] private Transform _bottomPoint;
    [SerializeField] private float _movementSpeed = 1f;
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _triggerOffsetY = 0.25f;
    private Collider2D _collider;

    private Transform _originTransform;
    private Coroutine _currentCoroutine = null;
    private bool _isMovingToTop;
    private Vector2 _targetPosition;
    private void Start()
    {
        _originTransform = transform;
        _collider = GetComponent<Collider2D>();
        MoveToBottom();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        float yPos = collision.ClosestPoint(_originTransform.position).y;
        yPos += _collider.bounds.size.y / 2;
        Vector2 offsetPos;
        if (_isMovingToTop)
        {
            offsetPos = new Vector2(_originTransform.position.x, yPos + _triggerOffsetY);
        }
        else
        {
            offsetPos = new Vector2(_originTransform.position.x, yPos - _triggerOffsetY);
        }
        MoveToTriggerOffset(offsetPos);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_isMovingToTop)
        {
            MoveToTop();
        }
        else
        {
            MoveToBottom();
        }   
    }

    private void MoveToTop()
    {
        _isMovingToTop = true;
        _targetPosition = _topPoint.position;
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);
        _currentCoroutine = StartCoroutine(StartMovingToTop());
    }

    private void MoveToBottom()
    {
        _isMovingToTop = false;
        _targetPosition = _bottomPoint.position;
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);
        _currentCoroutine = StartCoroutine(StartMovingToBottom());

    }

    private void MoveToTriggerOffset(Vector2 offsetPos)
    {
        _targetPosition = offsetPos;
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);
        _currentCoroutine = StartCoroutine(StartMovingToTriggerOffset());
    }

    private IEnumerator StartMovingToTop()
    {
        while (Vector2.Distance(_originTransform.position, _targetPosition) > 0.001f)
        {
            Vector2 newPos = Vector2.MoveTowards(_originTransform.position, _targetPosition, Time.fixedDeltaTime * _movementSpeed);
            _originTransform.position = newPos;
            yield return new WaitForFixedUpdate();
        }
        MoveToBottom();
    }

    private IEnumerator StartMovingToBottom()
    {
        while (Vector2.Distance(_originTransform.position, _targetPosition) > 0.001f)
        {
            Vector2 newPos = Vector2.MoveTowards(_originTransform.position, _targetPosition, Time.fixedDeltaTime * _movementSpeed);
            _originTransform.position = newPos;
            yield return new WaitForFixedUpdate();
        }
        MoveToTop();
    }

    private IEnumerator StartMovingToTriggerOffset()
    {
        while (Vector2.Distance(_originTransform.position, _targetPosition) > 0.001f)
        {
            Vector2 newPos = Vector2.MoveTowards(_originTransform.position, _targetPosition, Time.fixedDeltaTime * _movementSpeed);
            _originTransform.position = newPos;
            yield return new WaitForFixedUpdate();
        }
    }



    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(_topPoint.position, 0.2f);
        Gizmos.DrawSphere(_bottomPoint.position, 0.2f);
    }
}
