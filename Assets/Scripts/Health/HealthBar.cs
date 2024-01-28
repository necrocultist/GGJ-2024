using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private GameObject healthBarObject;
    [SerializeField] private Slider healthBarSlider;

    private void Start()
    {
        // Ensure the healthBarSlider component is assigned in the Inspector
        if (healthBarSlider == null)
        {
            Debug.LogError("HealthBarSlider Slider component is not assigned.");
            enabled = false; // Disable the script to prevent errors
        }
    }

    public void EnableHealthBar()
    {
        if (healthBarObject != null)
            healthBarObject.SetActive(true);
    }

    public void DisableHealthBar()
    {
        if (healthBarObject != null)
            healthBarObject.SetActive(false);
    }

    public void SetHealthBarValue(float healthPercent)
    {
        if (healthBarSlider != null)
            healthBarSlider.value *= Mathf.Clamp01(healthPercent);
    }
}