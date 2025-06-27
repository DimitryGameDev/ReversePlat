using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform _topPoint;
    [SerializeField] private Transform _bottomPoint;

    private Coroutine _currentCoroutine = null;


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
        yield return null;
        yield return new WaitForFixedUpdate();
    }

    private IEnumerator StartClosing()
    {
        yield return null;
        yield return new WaitForFixedUpdate();
    }
}
