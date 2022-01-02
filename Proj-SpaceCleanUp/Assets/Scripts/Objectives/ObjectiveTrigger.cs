using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveTrigger : ObjectiveInteractor
{


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<PlayerController>().getObjective(objective) == objective)
            {

                EndObjective();
            }


        }
    }

}
