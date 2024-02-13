using UnityEngine;

public class centralStimulus : MonoBehaviour
{
    private Renderer renderer;
    private Color defaultColor = Color.white; // Initial default color

    void Start()
    {
        renderer = GetComponent<Renderer>();
        SetColor(defaultColor); // Set initial color
    }

    void Update()
    {
        // The color change logic is now moved to the EyeTracking script,
        // but you can use messages or direct method calls to change the color from here.
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

    // This method can be called by the EyeTracking script to update the color based on gaze focus.
    public void Highlight(bool isFocused)
    {
        SetColor(isFocused ? Color.green : Color.red);
    }
}
