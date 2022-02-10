using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallDebriGenerator : MonoBehaviour
{
    [SerializeField]
    float outterRadius = 500;
    [SerializeField]
    float innerRadius = 100;

    [SerializeField]
    float InitialDelay = 5;
    [SerializeField]
    float averageDelay = 3;
    [SerializeField]
    float delayChange = 2;

    [SerializeField]
    GameObject Debri;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(initiate());
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    IEnumerator initiate()
    {
        float delay = Random.Range(-delayChange, delayChange);

        yield return new WaitForSeconds(InitialDelay + delay);

        StartCoroutine(SpawnDebri());
    }

    IEnumerator SpawnDebri()
    {
        while (true)
        {
            float delay = Random.Range(-delayChange, delayChange);

            yield return new WaitForSeconds(averageDelay + delay);


            Vector3 spawnPos = transform.position + Random.onUnitSphere * outterRadius;
            Vector3 target = transform.position + Random.onUnitSphere * innerRadius;
            Vector3 direction = target - spawnPos;

            GameObject o = Instantiate(Debri, spawnPos, Quaternion.identity);
            SmallDebri d = o.GetComponent<SmallDebri>();

            d.direction = direction;


        }
    }

    private void OnTriggerEnter(Collider other)
    {
            Destroy(gameObject);
    }




}
