using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConclusionSecondPart : AnimatorSetter
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [HideInInspector]
    public override void SetAnimatorVariables(Animator animator, int dummy = 1)
    {
        base.SetAnimatorVariables(animator);

        animator.SetBool("debriStatus", AppManager.DebriStatus);


     }


    
}
