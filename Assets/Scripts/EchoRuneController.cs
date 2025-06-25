using UnityEngine;

public class EchoRuneController : MonoBehaviour
{
    [Header("Echo Rune Settings")]
    [SerializeField] private KeyCode echoKey = KeyCode.E;
    [SerializeField] private GameObject phantomPrefab;
    [SerializeField] private float freezeDuration = 5f;

    private Vector2 firstMark;
    private bool hasMark = false;

    private void Update()
    {
        if (Input.GetKeyDown(echoKey))
        {
            if (!hasMark)
            {
                // первое нажатие - метка
                firstMark = transform.position;
                hasMark = true;
            }
            else
            {
                // второе нажатие - спавн фантома, телепорт, ресет
                Vector3 secondPos = transform.position;
                SpawnPhantom(secondPos);
                
                transform.position = firstMark;
                hasMark = false;
            }
        }
    }

    private void SpawnPhantom(Vector3 position)
    {
        if (phantomPrefab == null) return;

        GameObject phantom = Instantiate(phantomPrefab, position, Quaternion.identity);
        var ctrl = phantom.GetComponent<PhantomController>();
        if (ctrl != null)
            ctrl.Initialize(freezeDuration);
    }
}