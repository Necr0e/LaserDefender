using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 0.5f;
    private Material myMaterial;
    private Vector2 offset;
    
    private void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
        offset = new Vector2(0, scrollSpeed);
    }

    private void Update()
    {
        myMaterial.mainTextureOffset += offset * Time.deltaTime;
    }
}
