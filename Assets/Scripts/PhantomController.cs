using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PhantomController : MonoBehaviour
{
    private float lifetime;

    public void Initialize(float duration)
    {
        lifetime = duration;
    
        var rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
        StartCoroutine(FreezeAndDestroy());
    }

    private IEnumerator FreezeAndDestroy()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}
