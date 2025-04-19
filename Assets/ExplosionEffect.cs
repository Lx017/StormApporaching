using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    public float duration = 0.5f;
    public float maxScale = 3f;
    public Light explosionLight;

    private float timer = 0f;
    private Material mat;
    private Material childMat;
    private Color startColor;
    private Color childStartColor;
    public Transform childSphere;

    void Start()
    {
        // Get material for this object
        mat = GetComponent<Renderer>().material;
        startColor = mat.color;

        // Find and cache the child sphere (assumes only one child)
        if (transform.childCount > 0)
        {
            Renderer childRenderer = childSphere.GetComponent<Renderer>();
            if (childRenderer != null)
            {
                childMat = childRenderer.material;
                childStartColor = childMat.color;
            }
        }

        // Initialize scale
        transform.localScale = Vector3.zero;
        if (childSphere != null)
            childSphere.localScale = Vector3.zero;
    }

    void Update()
    {
        timer += Time.deltaTime;
        float t = timer / duration;

        // Scale up main object
        float scale = Mathf.Lerp(0.1f, maxScale, t);
        transform.localScale = Vector3.one * scale * 1.5f;

        // Scale up child object
        if (childSphere != null)
        {
            childSphere.localScale = Vector3.one * scale *2f;
        }

        // Light fade
        if (explosionLight != null)
            explosionLight.intensity = Mathf.Lerp(3000f, 0f, t);

        // Fade alpha main
        Color c = startColor;
        c.a = Mathf.Lerp(1f, 0f, t);
        mat.color = c;

        // Fade alpha child
        if (childMat != null)
        {
            Color cc = childStartColor;
            cc.a = Mathf.Lerp(0.5f, 0f, t*2);
            childMat.color = cc;
        }

        // Auto-destroy
        if (timer >= duration)
        {
            Destroy(gameObject);
        }
    }
}
