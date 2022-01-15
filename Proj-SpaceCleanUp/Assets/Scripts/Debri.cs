using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debri : Pickable, IInteractible
{

    DebriManager manager;

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
        if (player.getCurrentSpace() + size <= player.getMaxSpace()) //if player has space available
        {
            // this.gameObject.SetActive(false);
            player.pickUpObject(size);
            activated = true;
            
        }
        else //if player doesn't have space available
        {
            //inform player that there isn't enough space
            Debug.Log("Not enough Space");
        }        
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            activated = false;
            

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

}
