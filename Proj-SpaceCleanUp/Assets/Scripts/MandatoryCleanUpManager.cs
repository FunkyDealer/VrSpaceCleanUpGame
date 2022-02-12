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
    
    private DialogManager _dialogManager;
    
    
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
        _dialogManager = FindObjectOfType<DialogManager>();
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

        StartCoroutine(startLocators());

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
            if (!debris.Contains(other.gameObject)) debris.Add(other.gameObject);
            GameObject o = Instantiate(debriLocator, other.gameObject.transform);
            debriLocators.Add(o);
            o.SetActive(Active);

            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Debri"))
        {
            for (int i = 0; i < other.transform.childCount; i++)
            {
                debriLocators.Remove(other.gameObject.transform.GetChild(i).gameObject);
                Destroy(other.gameObject.transform.GetChild(i).gameObject);
            }
            debris.Remove(other.gameObject);


            if (Active && debris.Count == 0)
            {
                EndObjective();
                _dialogManager.RunSpeech(objective.speechID, objective.numberOfSentences);
            }
        }
    }

    IEnumerator startLocators()
    {
        yield return new WaitForSeconds(3f);

        foreach (var d in debriLocators)
        {
            d.SetActive(true);
        }
    }

}
