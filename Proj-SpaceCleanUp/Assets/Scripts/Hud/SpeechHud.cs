using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpeechHud : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private TMP_Text textName;
    [SerializeField] private Image image;
    [SerializeField] private float textSpeed;
    [SerializeField] private float windowTime;
    
    private ushort myBool = 0;

    //Starts the typing method and stops all coroutines to avoid closing a needed window
    public void WriteText(string st, string stName = "", float textSpd = 0)
    {
        if (textSpd == 0) { textSpd = textSpeed;}
        image.enabled = true;
        text.text = "";
        StopAllCoroutines();
        StartCoroutine(TypingCoroutine(st, stName, textSpd));
    }

    public ushort StoppedTyping()
    {
        return myBool;
    }

    //Runs trough every char in a string and waits [textSpeed] seconds
    private IEnumerator TypingCoroutine(string st, string stName, float textSpd)
    {
        myBool = 1;
        textName.text = stName;
        foreach (var c in st)
        {
            text.text += c;
            yield return new WaitForSeconds(textSpd * Time.deltaTime);
        }

        StartCoroutine(CloseTextBox());
    }

    //Waits [windowTime] seconds then clears text and disables image
    private IEnumerator CloseTextBox()
    {
        myBool = 0;
        yield return new WaitForSeconds(windowTime);
        text.text = "";
        textName.text = "";
        image.enabled = false;
    }
}
