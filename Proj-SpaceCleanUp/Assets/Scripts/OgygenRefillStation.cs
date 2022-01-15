using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OgygenRefillStation : MonoBehaviour, IInteractible
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact(PlayerController player)
    {
        player.replenishOxygen();
    }

}
