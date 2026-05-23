using UnityEngine;

public class SignalLight : MonoBehaviour
{
    [SerializeField] private Renderer lightRenderer;

    [Header("Colors")]
    [SerializeField] private Color colorOn = Color.white;
    [SerializeField] private Color colorOff = new Color(0.1f, 0.1f, 0.1f);

    private Material instanceMaterial;
    private static readonly int EmissionColorProperty = Shader.PropertyToID("_EmissionColor");

    private void Awake()
    {
        if (lightRenderer != null)
        {
            instanceMaterial = lightRenderer.material;
        }
    }

    public void SetOn(bool isOn)
    {
        if (instanceMaterial == null) return;

        if (isOn)
        {
            instanceMaterial.color = colorOn;
            instanceMaterial.EnableKeyword("_EMISSION");
            instanceMaterial.SetColor(EmissionColorProperty, colorOn * 1.5f);
        }
        else
        {
            instanceMaterial.color = colorOff;
            instanceMaterial.DisableKeyword("_EMISSION");
        }
    }
}
