using UnityEngine;

public class centralStimulus : MonoBehaviour
{
    public Material idleMaterial;
    private Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {
    }

    public void SetColor(Color color)
    {
        if (renderer != null)
        {
            MaterialPropertyBlock propBlock = new MaterialPropertyBlock();
            renderer.GetPropertyBlock(propBlock);
            propBlock.SetColor("_Color", color);
            renderer.SetPropertyBlock(propBlock);
        }
    }


    public void Highlight(bool isFocused)
    {
        SetColor(isFocused ? Color.green : Color.red);
    }

    public void Finished()
    {
    if (renderer != null && idleMaterial != null)
    {

        renderer.SetPropertyBlock(null);
        renderer.material = idleMaterial;
    }
    }
}
