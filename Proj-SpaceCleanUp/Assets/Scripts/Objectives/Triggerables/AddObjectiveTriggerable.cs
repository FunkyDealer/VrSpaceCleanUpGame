using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddObjectiveTriggerable : Triggerable
{
    [SerializeField]
    ObjectiveManager objectiveManager;
    
    [SerializeField]
    List<ObjectiveInteractor> objectives;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Activate()
    {
        base.Activate();


        foreach (var o in objectives)
        {
            objectiveManager.AddObjective(o.GetObjective());
        }
    }
}
