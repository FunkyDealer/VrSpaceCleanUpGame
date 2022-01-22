using UnityEngine;
using UnityEngine.UI;

public class OxygenSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    
    public void UpdateSlider(float value)
    {
        slider.value = value;
    }
}
