using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LongPressIndicatorHud : MonoBehaviour
{
    bool active;
    Image image;

    [SerializeField]
    [Range(0f, 1f)]
    float minTime = 0.3f;

    [SerializeField]
    Color newColor;
    Color originalColor;

    public void Press()
    {
        StartCoroutine(initiate());
    }


    public void Release()
    {
        StopAllCoroutines();
        active = false;
        image.fillAmount = 0;
        image.color = originalColor;

        
    }

    void Awake()
    {
        image = GetComponent<Image>();

        originalColor = image.color;

        
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            image.fillAmount += 1 * Time.deltaTime;

            if (image.fillAmount >= 1) image.color = newColor;
        }
    }


    IEnumerator initiate()
    {
        yield return new WaitForSeconds(minTime);

        image.fillAmount = minTime;
        active = true;
    }

}
