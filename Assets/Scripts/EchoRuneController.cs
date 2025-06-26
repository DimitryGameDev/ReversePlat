using UnityEngine;

public class EchoRuneController : MonoBehaviour
{
    [Header("Echo Rune Settings")]
    [SerializeField] private KeyCode echoKey = KeyCode.E;
    [SerializeField] private GameObject phantomPrefab;
    [SerializeField] private GameObject runePrefab;
    [SerializeField] private float freezeDuration = 5f;

    private Vector2 firstMark;
    private bool hasMark = false;
    private GameObject currentPhantom;
    private GameObject runeInstance;
    private void Update()
    {
        if (Input.GetKeyDown(echoKey))
        {
            if (currentPhantom != null)
                return;
            if (!hasMark)
            {
                // первое нажатие - метка
                firstMark = transform.position;
                hasMark = true;
                runeInstance = Instantiate(runePrefab, transform.position, Quaternion.identity);
            }
            else
            {
                // второе нажатие - спавн фантома, телепорт, ресет
                Vector3 secondPos = transform.position;
                SpawnPhantom(secondPos);
                if (runeInstance != null)
                {
                    Destroy(runeInstance);
                    runeInstance = null;
                }
                transform.position = firstMark;
                hasMark = false;
            }
        }
    }

    private void SpawnPhantom(Vector3 position)
    {
        if (phantomPrefab == null) return;

        GameObject phantom = Instantiate(phantomPrefab, position, Quaternion.identity);
        currentPhantom = phantom;
        var ctrl = phantom.GetComponent<PhantomController>();
        if (ctrl != null)
            ctrl.Initialize(freezeDuration);
    }
}