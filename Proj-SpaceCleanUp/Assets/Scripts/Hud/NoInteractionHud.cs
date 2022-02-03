using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoInteractionHud : MonoBehaviour
{
    [SerializeField]
    float timeToDeactivate = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Enable()
    {
        gameObject.SetActive(true);
        StartCoroutine(run());
    }

    void OnEnable()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator run()
    {
        

        yield return new WaitForSeconds(timeToDeactivate);

        gameObject.SetActive(false);
    }
}
