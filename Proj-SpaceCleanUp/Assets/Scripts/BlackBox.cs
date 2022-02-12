using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBox : Pickable, IInteractible
{
    [SerializeField]
    ObjectiveManager manager;

    [SerializeField]
    Objective objective;

    [SerializeField]
    List<MyScriptableObject> scripts; //scripts List

    Triggerable[] triggerables; //Triggerable list

    
    private DialogManager _dialogManager;

    protected override void Awake()
    {
        base.Awake();
        if (objective.addedAtStart) manager.AddObjective(objective);

        triggerables = GetComponents<Triggerable>();

    }

    // Start is called before the first frame update
    void Start()
    {
        _dialogManager = FindObjectOfType<DialogManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public override void Interact(PlayerController player)
    {
        base.Interact(player);
        player.getBlackBox();

        activated = true;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (activated && other.CompareTag("Player"))
        {
            EndObjective();
            _dialogManager.RunSpeech(objective.speechID, objective.numberOfSentences);
            activated = false;
            this.gameObject.SetActive(false);

        }
    }

    public virtual void EndObjective()
    {
        manager.AddObjective(objective);
        

        if (scripts.Count > 0) foreach (var s in scripts) s.Activate(); //Activate Scripts if any
        if (triggerables.Length > 0) for (int i = 0; i < triggerables.Length; i++) triggerables[i].Activate(); //Activate modules if any

        manager.ObjectiveDone(objective); //tell manager that objective is done and ask if the quest is finished;
    }
}
