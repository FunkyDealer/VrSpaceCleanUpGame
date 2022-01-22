using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpeechHud : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private Image image;
    [SerializeField] private float textSpeed;
    [SerializeField] private float windowTime;
    private void Update() //Debug only
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            WriteText("WWWWHHHHAAAATTTTTTT TTTTTHHHHHHEEEEEE HHHHHEEEEEELLLLLLL");
        }
    }

    //Starts the typing method and stops all coroutines to avoid closing a needed window
    public void WriteText(string st)
    {
        image.enabled = true;
        text.text = "";
        StopAllCoroutines();
        StartCoroutine(TypingCoroutine(st));
    }

    //Runs trough every char in a string and waits [textSpeed] seconds
    private IEnumerator TypingCoroutine(string st)
    {
        foreach (var c in st)
        {
            text.text += c;
            yield return new WaitForSeconds(textSpeed * Time.deltaTime);
        }

        StartCoroutine(CloseTextBox());
    }

    //Waits [windowTime] seconds then clears text and disables image
    private IEnumerator CloseTextBox()
    {
        yield return new WaitForSeconds(windowTime);
        text.text = "";
        image.enabled = false;
    }
}
