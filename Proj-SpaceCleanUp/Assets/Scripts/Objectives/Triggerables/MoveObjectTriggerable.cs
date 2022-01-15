using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjectTriggerable : Triggerable
{
    [SerializeField, Min(0.2f)]
    private float smoothTime = 0.3f;
    [SerializeField, Min(0f)]
    private float rangedMovement = 5f;
    [SerializeField]
    private float maxSpeed = 10;

    private Vector3 currentVelocity;
    [SerializeField]
    private Transform movementObjective;
    private Vector3 target;
    private int index = 0;

    private bool active = false;


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
        if (active)
        {
            target = movementObjective.position;
            transform.position = Vector3.SmoothDamp(transform.position, target, ref currentVelocity, smoothTime, maxSpeed);

            if (transform.position == target)
            {
                active = false;
            }
        }
    }

    public override void Activate()
    {
        base.Activate();

        MoveObject();

    }

    private void MoveObject()
    {
        active = true;
    }
}
