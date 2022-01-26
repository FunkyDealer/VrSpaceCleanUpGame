using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HoverInfoHud : MonoBehaviour
{
    [SerializeField]
    private TMP_Text tname;
    [SerializeField]
    private TMP_Text description;
    [SerializeField]
    private Image image;
    
    public void LoadText(string n, string d)
    {
        image.enabled = true;
        tname.text = n;
        description.text = d;
    }
    
    public void UnloadText()
    {
        image.enabled = false;
        tname.text = "";
        description.text = "";
    }
}
