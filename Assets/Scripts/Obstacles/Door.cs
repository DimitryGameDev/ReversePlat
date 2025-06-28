using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform _topPoint;
    [SerializeField] private Transform _bottomPoint;
    [SerializeField] private float _movementSpeed = 1f;

    private Transform _originTransform;
    private Coroutine _currentCoroutine = null;

    private void Start()
    {
        _originTransform = transform;
    }
    public void Open()
    {
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);
        _currentCoroutine = StartCoroutine(StartOpening());
    }

    public void Close()
    {
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);
        _currentCoroutine = StartCoroutine(StartClosing());

    }

    private IEnumerator StartOpening()
    {
        while (Vector2.Distance(_originTransform.position, _topPoint.position) > 0.001f)
        {
            Vector2 newPos = Vector2.MoveTowards(_originTransform.position, _topPoint.position, Time.fixedDeltaTime * _movementSpeed);
            _originTransform.position = newPos;
            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator StartClosing()
    {
        while (Vector2.Distance(_originTransform.position, _bottomPoint.position) > 0.001f)
        {
            Vector2 newPos = Vector2.MoveTowards(_originTransform.position, _bottomPoint.position, Time.fixedDeltaTime * _movementSpeed);
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
