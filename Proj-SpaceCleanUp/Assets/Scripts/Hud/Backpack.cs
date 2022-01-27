using TMPro;
using UnityEngine;

public class Backpack : MonoBehaviour
{
    [SerializeField] private TMP_Text BackPackText;

    [SerializeField]
    private TMP_Text MoneyText;
    
    public void UpdateBackPack(int currentSpace, int maxSpace)
    {
        BackPackText.text = $"{currentSpace} / {maxSpace}";
    }

    public void updateMoney(int value)
    {
        MoneyText.text = value.ToString() + "$";
    }
}
