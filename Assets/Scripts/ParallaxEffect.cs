using UnityEngine;
using Unity.Cinemachine;

public class ParallaxEffect : MonoBehaviour
{
    [Header("Cinemachine")]
   
    [SerializeField] private Transform camTarget;

    [Header("Background Layers (furthest → nearest)")]
    [SerializeField] private Transform[] backgrounds;

    [Header("Parallax Settings")]
   // регулирует насколько жесткий параллакс
    [SerializeField] private float smoothing = 1f;

          
    private Vector3 previousCamPos;   
    private float[] parallaxScales;   
    private void Awake()
    {
     
        if (camTarget == null && Camera.main != null)
            camTarget = Camera.main.transform;

        previousCamPos = camTarget.position;
       
    }

    private void Start()
    {
        previousCamPos = camTarget.position;
        parallaxScales = new float[backgrounds.Length];

        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = -backgrounds[i].position.z;
        }
    }

    private void LateUpdate()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float deltaX = previousCamPos.x - camTarget.position.x;
            float parallax = deltaX * parallaxScales[i];
            float targetX = backgrounds[i].position.x + parallax;

            Vector3 targetPos = new Vector3(
                targetX,
                backgrounds[i].position.y,
                backgrounds[i].position.z
            );

        
            backgrounds[i].position = Vector3.Lerp(
                backgrounds[i].position,
                targetPos,
                smoothing * Time.deltaTime
            );
        }

  
        previousCamPos = camTarget.position;
    }
}
