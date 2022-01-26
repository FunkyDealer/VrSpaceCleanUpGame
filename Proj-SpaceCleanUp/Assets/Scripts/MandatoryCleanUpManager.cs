using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MandatoryCleanUpManager : ObjectiveInteractor
{
    List<GameObject> debris;

    bool Active;

    List<GameObject> debriLocators;

    [SerializeField]
    GameObject debriLocator;

    protected override void Awake()
    {
        base.Awake();
        if (debris == null) debris = new List<GameObject>();
        if (debriLocators == null) debriLocators = new List<GameObject>();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void StartObjective()
    {
        Active = true;

        foreach (var d in debriLocators)
        {
            d.SetActive(true);
        }

        if (debris.Count == 0) EndObjective();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Debri"))
        {
            if (debris == null)
            {
                debris = new List<GameObject>();                               
            }
            debris.Add(other.gameObject);
            GameObject o = Instantiate(debriLocator, other.gameObject.transform);
            debriLocators.Add(o);
            if (!Active) o.SetActive(false);

            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Debri"))
        {
            debriLocators.Remove(other.gameObject.transform.GetChild(0).gameObject);
            debris.Remove(other.gameObject);
            

            Destroy(other.gameObject.transform.GetChild(0).gameObject);

            if (Active && debris.Count == 0) EndObjective();
        }
    }

}
