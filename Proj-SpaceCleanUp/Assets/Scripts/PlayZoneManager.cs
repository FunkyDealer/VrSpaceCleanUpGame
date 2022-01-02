using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayZoneManager : MonoBehaviour
{
    [SerializeField]
    PlayerController player;




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
        if (other.gameObject.CompareTag("Player")) {
            player.setinField(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.setinField(false);
        }
    }


}
