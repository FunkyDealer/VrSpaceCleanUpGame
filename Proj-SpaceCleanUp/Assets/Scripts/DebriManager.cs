using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebriManager : ObjectiveInteractor
{
    List<Debri> debriList; 


    protected override void Awake()
    {
        base.Awake();
        debriList = new List<Debri>();

    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        if (transform.childCount > 0) {

            int children = transform.childCount;

            for (int i = 0; i < children; ++i)
            {

                Transform t = transform.GetChild(i);
                Debri d = t.gameObject.GetComponent<Debri>();

                if (d != null)
                {
                    debriList.Add(d);
                    d.setDebriManager(this);

                }
            }
        }



    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void removeDebri(Debri d)
    {
        debriList.Remove(d);

        if (debriList.Count == 0)
        {
            EndObjective();
        }
    }


}
