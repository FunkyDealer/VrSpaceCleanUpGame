using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveButton : ObjectiveInteractor, IInteractible
{
    [SerializeField]
    string gameName;
    [SerializeField]
    string description;

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

    public void Interact(PlayerController player)
    {
        if (manager.getCurrentQuestObjective(objective) == objective.ID)
        {
            EndObjective();
            _dialogManager.RunSpeech(objective.speechID, objective.numberOfSentences);
        }
    }

    private void Action()
    {

        EndObjective();
        _dialogManager.RunSpeech(objective.speechID, objective.numberOfSentences);
        //active next
    }

    public (string, string) getInfo(PlayerController player)
    {
        return (gameName, description);
    }
}
