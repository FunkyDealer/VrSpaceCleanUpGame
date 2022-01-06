using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObjectTriggerable : Triggerable
{

    [SerializeField]
    List<GameObject> ObjectToActivate;

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

        if (ObjectToActivate.Count > 0) foreach (var g in ObjectToActivate) g.SetActive(true);
    }
}
