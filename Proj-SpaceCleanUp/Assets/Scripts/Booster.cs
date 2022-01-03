﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : ObjectiveInteractor, IInteractible
{
    [SerializeField]
    GameObject boosterObj;


    protected override void Awake()
    {
        base.Awake();

    }

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
        if (player.getObjective(objective) == objective)
        {

            Action();
        }
    }

    private void Action()
    {
        gameObject.SetActive(false);

        boosterObj.SetActive(true);

        EndObjective();
        //active next
    }
}
