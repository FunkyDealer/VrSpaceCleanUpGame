using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveInteractor : MonoBehaviour
{
    [SerializeField]
    protected PlayerController player;
    [SerializeField]
    protected ObjectiveManager manager;

    [SerializeField]
    protected Objective objective;

    [SerializeField]
    protected List<MyScriptableObject> scripts; //scripts List

    protected Triggerable[] triggerables; //Triggerable list

    [SerializeField]
    protected bool HideAtStart = false;


    protected virtual void Awake()
    {
        if (objective.addedAtStart) manager.AddObjective(objective);

        triggerables = GetComponents<Triggerable>();

        
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        if (HideAtStart) this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void StartObjective()
    {

    }

    public virtual void EndObjective()
    {
        manager.ObjectiveDone(objective); //tell manager that objective is done and ask if the quest is finished;

        if (scripts.Count > 0) foreach (var s in scripts) s.Activate(); //Activate Scripts if any
        if (triggerables.Length > 0) for (int i = 0; i < triggerables.Length; i++) triggerables[i].Activate(); //Activate modules if any
    }

    public Objective GetObjective()
    {
        return objective;
    }

}
