using UnityEngine;
using UnityEngine.UI;

public class SphericalHarmonicsController : MonoBehaviour
{
    public Material originalMaterial;  // Assign this in the Inspector
    public Slider lSlider;
    public Slider mSlider;
    private Material instanceMaterial;  // Unique material for this instance

    public int l = 0;
    public int m = 0;

    void Start()
    {
        // Create a new material instance so changes don't affect all objects
        instanceMaterial = GetComponent<Renderer>().material;

        // Check if sliders exist before adding listeners
        if (lSlider != null)
            lSlider.onValueChanged.AddListener(delegate { UpdateL(); });

        if (mSlider != null)
            mSlider.onValueChanged.AddListener(delegate { UpdateM(); });

        // Set initial values if sliders exist
        if (lSlider != null) UpdateL();
        if (mSlider != null) UpdateM();
    }

    void UpdateL()
    {
        l = Mathf.RoundToInt(lSlider.value); // Ensure integer values
        instanceMaterial.SetFloat("_L", l);
    }

    void UpdateM()
    {
        m = Mathf.RoundToInt(mSlider.value); // Ensure integer values
        instanceMaterial.SetFloat("_M", m);
    }
}


