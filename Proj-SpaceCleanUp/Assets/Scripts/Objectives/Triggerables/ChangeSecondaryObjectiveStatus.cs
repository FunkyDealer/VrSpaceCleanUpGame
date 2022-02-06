using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSecondaryObjectiveStatus : Triggerable
{
    [SerializeField]
    AppManager.EMissionStatus newStatus;

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

        AppManager.setMissionStatus(newStatus);
    }
}
