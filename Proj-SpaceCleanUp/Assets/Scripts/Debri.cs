using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debri : MonoBehaviour, IInteractible
{
    [SerializeField]
    private int size;
    [SerializeField]
    private float movementSpeed = 7;

    private Transform playerPos = null;
    private bool activated = false;
    private Vector3 dir;
    private Rigidbody myRigidBody;

    void Awake()
    {
        myRigidBody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (activated)
        {
          dir = playerPos.position - this.transform.position;
          myRigidBody.velocity = dir * movementSpeed;
        }
    }

    public void Interact(PlayerController player)
    {
        if (player.getCurrentSpace() + size <= player.getMaxSpace()) //if player has space available
        {

            // this.gameObject.SetActive(false);
            player.pickUpObject(size);
            activated = true;
            playerPos = player.transform;
        }
        else //if player doesn't have space available
        {
            //inform player that there isn't enough space
            Debug.Log("Not enough Space");
        }        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            activated = false;
            this.gameObject.SetActive(false);
            
        }
    }
}
