using UnityEngine;
using UnityEngine.UI;

public class OxygenSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    
    public void UpdateSlider(float value, float max)
    {
        float sliderValue;
        sliderValue = (value * 100) / max;

        sliderValue = sliderValue * 0.01f;
        slider.value = sliderValue;
    }
}
