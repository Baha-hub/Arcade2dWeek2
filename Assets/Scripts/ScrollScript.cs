using UnityEngine;

public class ScrollScript : MonoBehaviour
{
    public float scrollSpeed = 0.1f;
    private MeshRenderer _meshRenderer;
    private float y_scroll;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        Scroll();
    }
    private void Scroll()
    {
        y_scroll = Time.time * scrollSpeed;
        Vector2 offset = new Vector2(0f, y_scroll);
        _meshRenderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}