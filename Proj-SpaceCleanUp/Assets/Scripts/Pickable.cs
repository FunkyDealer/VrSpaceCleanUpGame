using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour, IInteractible
{
    [SerializeField]
    protected int size;
    [SerializeField]
    protected float movementSpeed = 7;

    protected Transform playerPos = null;
    protected bool activated = false;
    protected Vector3 dir;
    protected Rigidbody myRigidBody;


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

    public virtual void Interact(PlayerController player)
    {
       
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            activated = false;
            this.gameObject.SetActive(false);

        }
    }

}
