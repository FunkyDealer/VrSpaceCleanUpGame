using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebriManager : MonoBehaviour
{
    List<Debri> debriList;


    void Awake()
    {

        debriList = new List<Debri>();

    }

    // Start is called before the first frame update
    void Start()
    {

        if (transform.childCount > 0) {

            int children = transform.childCount;

            for (int i = 0; i < children; ++i)
            {

                Transform t = transform.GetChild(i);
                Debri d = t.gameObject.GetComponent<Debri>();

                if (d != null) debriList.Add(d);
            }
        }



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
