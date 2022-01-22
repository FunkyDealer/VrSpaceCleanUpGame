using TMPro;
using UnityEngine;

public class Backpack : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    
    public void UpdateText(string value)
    {
        text.text = value;
    }
}
