using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbiter : MonoBehaviour
{
    [SerializeField]
    GameObject planet;

    [SerializeField]
    float speed = 0.1f;



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
        transform.RotateAround(planet.transform.position, planet.transform.up, speed);
    }
}
