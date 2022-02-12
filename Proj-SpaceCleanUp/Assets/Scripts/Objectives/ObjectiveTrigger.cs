using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveTrigger : ObjectiveInteractor
{
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<ObjectiveController>().GetObjective(objective) == objective)
            {

                EndObjective();
                _dialogManager.RunSpeech(objective.speechID, objective.numberOfSentences);
            }
        }
    }
}
