using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallDebri : MonoBehaviour
{
    [SerializeField]
    float speed = 10;

    [SerializeField]
    float minRotationSpeed = 0.1f;
    [SerializeField]
    float maxRotationSpeed = 1;

    float rotationSpeed = 0.5f;

    [SerializeField]
    float maxLifeTime = 30;

    Vector3 rotationAxis = Vector3.up;

    public Vector3 direction;

    void Awake()
    {
        StartCoroutine(Kill());


        rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
    }

    // Start is called before the first frame update
    void Start()
    {
        rotationAxis = new Vector3(Random.Range(0, 361), Random.Range(0, 361), Random.Range(0, 361));
        gameObject.transform.Rotate(rotationAxis, Random.Range(0, 360));


     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {

        transform.position += direction * speed;

        transform.Rotate(rotationAxis, rotationSpeed);

    }


    IEnumerator Kill()
    {
        yield return new WaitForSeconds(maxLifeTime);

        Destroy(this.gameObject);
    }

}
