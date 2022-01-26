using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveButton : ObjectiveInteractor, IInteractible
{
    [SerializeField]
    string gameName;
    [SerializeField]
    string description;



    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact(PlayerController player)
    {
        if (manager.getCurrentQuestObjective(objective) == objective.ID) EndObjective();
    }

    private void Action()
    {

        EndObjective();
        //active next
    }

    public (string, string) getInfo(PlayerController player)
    {
        return (gameName, description);
    }
}
