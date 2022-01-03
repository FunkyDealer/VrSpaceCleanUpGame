﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveButton : ObjectiveInteractor, IInteractible
{




    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact(PlayerController player)
    {
        if (manager.getCurrentQuestObjective(objective) == objective.ID) EndObjective();
    }

    private void Action()
    {

        EndObjective();
        //active next
    }
}