using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    
    public void UpdateSlider(float value)
    {
        slider.value = value;
    }
}
