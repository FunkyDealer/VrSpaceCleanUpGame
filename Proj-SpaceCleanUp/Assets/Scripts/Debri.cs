using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Debri : Pickable, IInteractible
{

    DebriManager manager;

    [SerializeField]
    string type;

    Vector3 thisRotationVector;
    float rotationSpeed = 5;

    protected override void Awake()
    {
        base.Awake();

        thisRotationVector = gameObject.transform.up;

        rotationSpeed = Random.value * 0.8f;
    }

    // Start is called before the first frame update
    void Start()
    {
        thisRotationVector = new Vector3(Random.Range(0, 361), Random.Range(0, 361), Random.Range(0, 361));

        gameObject.transform.Rotate(thisRotationVector, Random.Range(0, 360));


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (!activated) gameObject.transform.Rotate(thisRotationVector, rotationSpeed);

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
        //Handles.Label(transform.position, $"Size: {size}, {type}");
    }

}
