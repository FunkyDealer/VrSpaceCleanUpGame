using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonRotator : MonoBehaviour
{
    [SerializeField]
    GameObject earth;


    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.RotateAround(earth.transform.position, earth.transform.up, -123.8f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
