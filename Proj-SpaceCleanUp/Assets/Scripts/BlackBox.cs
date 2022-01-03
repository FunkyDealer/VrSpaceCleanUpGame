using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBox : Pickable, IInteractible
{
    [SerializeField]
    PlayerController player;
    [SerializeField]
    ObjectiveManager manager;

    [SerializeField]
    Objective objective;

    [SerializeField]
    List<MyScriptableObject> scripts; //scripts List

    Triggerable[] triggerables; //Triggerable list

    void Awake()
    {
        if (objective.addedAtStart) manager.AddObjective(objective);

        triggerables = GetComponents<Triggerable>();

    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact(PlayerController player)
    {
        player.getBlackBox();

        activated = true;
        playerPos = player.transform;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EndObjective();
            activated = false;
            this.gameObject.SetActive(false);

        }
    }

    public virtual void EndObjective()
    {
        manager.ObjectiveDone(objective); //tell manager that objective is done and ask if the quest is finished;

        if (scripts.Count > 0) foreach (var s in scripts) s.Activate(); //Activate Scripts if any
        if (triggerables.Length > 0) for (int i = 0; i < triggerables.Length; i++) triggerables[i].Activate(); //Activate modules if any
    }
}
