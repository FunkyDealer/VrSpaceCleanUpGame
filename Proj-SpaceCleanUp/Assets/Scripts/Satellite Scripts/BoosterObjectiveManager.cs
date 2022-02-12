using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterObjectiveManager : ObjectiveInteractor
{
    bool code1 = false;
    bool code2 = false;

    private DialogManager _dialogManager;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _dialogManager = FindObjectOfType<DialogManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void receiveNoticePrompt(int code)
    {
        if (code == 1) code1 = true;
        else if (code == 2) code2 = true;


        if (code1 && code2)
        {
           
            EndObjective();
        }
    }

    public void ReceiveNoticeActivate()
    {
        EndObjective();
    }

    public bool getObjectiveConfirmation()
    {
        return this.objective.ID == manager.getCurrentQuestObjective(this.objective);
    }
}
