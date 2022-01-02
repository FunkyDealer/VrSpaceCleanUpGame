using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MandatoryCleanUpManager : ObjectiveInteractor
{
    List<GameObject> debris;

    bool Active;

    protected override void Awake()
    {
        base.Awake();
        if (debris == null) debris = new List<GameObject>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void StartObjective()
    {
        Active = true;

        if (debris.Count == 0) EndObjective();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Debri"))
        {
            if (debris == null) debris = new List<GameObject>();
            debris.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Debri"))
        {
            debris.Remove(other.gameObject);

            if (Active && debris.Count == 0) EndObjective();
        }
    }

}
