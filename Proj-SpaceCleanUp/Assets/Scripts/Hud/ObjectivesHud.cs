using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectivesHud : MonoBehaviour
{
    [SerializeField]
    private List<TMP_Text> _tmpTexts;

    private string[] _strings = {"", "", ""};
    
    public void LoadString(string st)
    {
        for (var i = 0; i < 3; i++)
        {
            if (_strings[i] != "") continue;
            _strings[i] = st;
            _tmpTexts[i].text = st;
            break;
        }
    }

    public void DeleteString(string st)
    {
        for (var i = 0; i < 3; i++) //Deletes text if it exists
        {
            if (_strings[i] != st) continue;
            _strings[i] = "";
            _tmpTexts[i].text = "";
        }

        StartCoroutine(RearrangeCoroutine());
    }

    private IEnumerator RearrangeCoroutine()
    {
        yield return new WaitForSeconds(0.01f);
        for (var i = 0; i < 2; i++) //Tries to rearrange the lines after a short delay
        {
            if (_strings[i] != "") continue;
            _strings[i] = _strings[i + 1];
            _strings[i + 1] = "";
        }
    }
}
