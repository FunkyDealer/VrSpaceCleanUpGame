using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnparentObjectsTriggerable : Triggerable
{
    [SerializeField]
    List<GameObject> ObjectsToUnparent;

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

        foreach (var o in ObjectsToUnparent)
        {
            o.transform.parent = null;
        }
    }
}
