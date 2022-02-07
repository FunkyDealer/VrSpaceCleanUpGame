using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbiter : MonoBehaviour
{
    [SerializeField]
    GameObject planet;
    [SerializeField]
    float speed = 0.1f; //orbit speed
    [SerializeField]
    GameObject sunSetPredictionObject; //use to predict when a sunset or sunrise is coming


    [SerializeField]
    GameObject sun;
    [SerializeField]
    Light sunLight;

    [SerializeField]
    LayerMask interactionMask;

    [SerializeField]
    float maxIntensity = 1.3f;
    [SerializeField]
    float lightChangeSpeed = 0.15f;

    bool night = false;

    void Awake()
    {
        //move prediction object forward in orbit
        sunSetPredictionObject.transform.RotateAround(planet.transform.position, planet.transform.up, 15);
    
    }

    // Start is called before the first frame update
    void Start()
    {
        sunLight.intensity = maxIntensity;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (night && sunLight.intensity >= 0f)
        {
            sunLight.intensity -= lightChangeSpeed * Time.deltaTime;
        }
        else if (!night && sunLight.intensity <= maxIntensity)
        {
            sunLight.intensity += lightChangeSpeed * Time.deltaTime;
        }


    }


    void FixedUpdate()
    {
        transform.RotateAround(planet.transform.position, planet.transform.up, speed);

        sunSetPredictionObject.transform.RotateAround(planet.transform.position, planet.transform.up, speed);

        calculateLight();
    }

    void calculateLight()
    {
        Vector3 dir = sunSetPredictionObject.transform.position - sun.transform.position;
        float distance = Vector3.Distance(sunSetPredictionObject.transform.position, sun.transform.position);

        //Debug.DrawRay(sun.transform.position, dir * 999, Color.yellow);

        RaycastHit hit;
        if (Physics.Raycast(sun.transform.position, dir, out hit, distance, layerMask: interactionMask))
        {            

            if (hit.collider.CompareTag("Player"))
            {
                night = false;

                Debug.DrawRay(sun.transform.position, dir * 999, Color.yellow);
            }
            else
            {
                night = true;

                Debug.DrawRay(sun.transform.position, dir * 999, Color.white);
            }

        }


     }
}
