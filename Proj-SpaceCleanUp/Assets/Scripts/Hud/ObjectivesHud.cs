using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectivesHud : MonoBehaviour
{
    [SerializeField] private List<TMP_Text> _tmpTexts;
    private string[] _strings = {""};
    
    public void LoadString(string st)
    {
        for (var i = 0; i < 3; i++)
        {
            if (_strings[i] != "") continue;
            _strings[i] = st;
            _tmpTexts[i].text = st;
        }
    }

    public void DeleteString(string st)
    {
        for (var i = 0; i < 3; i++)
        {
            if (_strings[i] != st) continue;
            _strings[i] = "";
            _tmpTexts[i].text = "";
        }
    }
}
