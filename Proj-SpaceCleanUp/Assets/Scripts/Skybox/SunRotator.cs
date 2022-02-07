using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunRotator : MonoBehaviour
{
    [SerializeField]
    float speed = 0.05f;

    [SerializeField]
    GameObject earth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        gameObject.transform.Rotate(earth.transform.up, -speed);
    }

}
