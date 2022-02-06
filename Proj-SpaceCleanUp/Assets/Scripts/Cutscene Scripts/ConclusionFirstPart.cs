using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConclusionFirstPart : AnimatorSetter
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

        switch (AppManager.MissionStatus)
        {
            case AppManager.EMissionStatus.none:
                //do nothing
                break;
            case AppManager.EMissionStatus.partial:
                animator.SetBool("partialSecondary", true);
                animator.SetBool("FullSecondary", false);
                break;
            case AppManager.EMissionStatus.complete:
                animator.SetBool("partialSecondary", false);
                animator.SetBool("FullSecondary", true);
                break;
            default:
                break;
        }
    }
}
