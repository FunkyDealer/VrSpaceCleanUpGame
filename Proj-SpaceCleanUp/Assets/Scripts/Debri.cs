﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Debri : Pickable, IInteractible
{

    DebriManager manager;

    [SerializeField]
    string type;


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
        base.Interact(player);
        if (player.getCurrentSpace() + size <= player.getMaxBackPackSpace()) //if player has space available
        {
            // this.gameObject.SetActive(false);
            activated = true;
            
        }
        else //if player doesn't have space available
        {
            //inform player that there isn't enough space
            player.BackPackFullWarning();
            Debug.Log("Not enough Space");
        }        
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (activated && other.CompareTag("Player"))
        {
            activated = false;
            player.pickUpObject(size);

            transform.position = new Vector3(999, 999, 999);
            if (manager != null) manager.removeDebri(this);
            StartCoroutine(CountDownToDeath(0.2f));

        }
    }

    private IEnumerator CountDownToDeath(float time)
    {
        yield return new WaitForSeconds(time);

        //this.gameObject.SetActive(false);
        Destroy(gameObject);
    }



    public void setDebriManager(DebriManager manager)
    {
        this.manager = manager;
    }

    public override (string, string) getInfo(PlayerController player)
    {
        return (gameName + $" Size: {size}", description);
    }


    void OnDrawGizmos()
    {
        Handles.Label(transform.position, $"Size: {size}, {type}");
    }

}
