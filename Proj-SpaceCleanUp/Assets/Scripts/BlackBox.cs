using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBox : Pickable, IInteractible
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact(PlayerController player)
    {
        player.getBlackBox();

        activated = true;
        playerPos = player.transform;
    }
}
