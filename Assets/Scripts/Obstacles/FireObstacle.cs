using System.Collections;
using UnityEngine;

public class FireObstacle : MonoBehaviour
{
    [SerializeField] private Transform _topPoint;
    [SerializeField] private Transform _bottomPoint;
    [SerializeField] private float _movementSpeed = 1f;
    [SerializeField] private float _applyDamageDelay = 1f;
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _triggerOffsetY = 0.25f;
    private Collider2D _collider;

    private Transform _originTransform;
    private Coroutine _currentCoroutine = null;
    private bool _isMovingToTop;
    private Vector2 _targetPosition;
    
    
    private bool _canApplyDamage = true;
    private IDamageable _damageable;
    private void Start()
    {
        _originTransform = transform;
        _collider = GetComponent<Collider2D>();

        MoveToBottom();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            _damageable = damageable;
            if (_canApplyDamage)
            {
                ApplyDamage();
            }
            
            if (_isMovingToTop)
                return;
 
            Vector2 offsetPos;
            float yPos;
            yPos = collision.transform.position.y + collision.bounds.size.y;
            yPos += _collider.bounds.size.y / 2;
            offsetPos = new Vector2(_originTransform.position.x, yPos - _triggerOffsetY);

            MoveToTriggerOffset(offsetPos);

        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_damageable != null && _canApplyDamage)
            ApplyDamage();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            if (_damageable == damageable)
            {
                if (_canApplyDamage)
                {
                    ApplyDamage();
                }
                _damageable = null;
            }

            if (_isMovingToTop)
            {
                MoveToTop();
            }
            else
            {
                MoveToBottom();
            }
        }

    }

    private void MoveToTop()
    {
        _isMovingToTop = true;
        _targetPosition = _topPoint.position;
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);
        if(gameObject.activeInHierarchy)
            _currentCoroutine = StartCoroutine(StartMovingToTop());
    }

    private void MoveToBottom()
    {
        _isMovingToTop = false;
        _targetPosition = _bottomPoint.position;
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);
        if(gameObject.activeInHierarchy)
            _currentCoroutine = StartCoroutine(StartMovingToBottom());

    }

    private void MoveToTriggerOffset(Vector2 offsetPos)
    {
        _targetPosition = offsetPos;
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);
        if(gameObject.activeInHierarchy)
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
    private void ApplyDamage()
    {
        _damageable.TakeDamage(_damage);
        if(gameObject.activeInHierarchy)
            StartCoroutine(WaitForApplyDamageDelay());
    }
    private IEnumerator WaitForApplyDamageDelay()
    {
        _canApplyDamage = false;
        yield return new WaitForSeconds(_applyDamageDelay);
        _canApplyDamage = true;
    }


    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(_topPoint.position, 0.2f);
        Gizmos.DrawSphere(_bottomPoint.position, 0.2f);
    }
}
