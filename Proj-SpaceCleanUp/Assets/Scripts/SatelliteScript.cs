using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteScript : ObjectiveInteractor
{
    Animator myAnim;

    protected override void Awake()
    {
        base.Awake();

        myAnim = GetComponent<Animator>();
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

    public override void StartObjective()
    {
        base.StartObjective();

        myAnim.SetTrigger("Move");
    }


}
