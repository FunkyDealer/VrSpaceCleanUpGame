using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    private Dictionary<string ,List<Objective>> QuestList;


    private Dictionary<string, int> currentObjectiveList;

    [SerializeField]
    PlayerController player;

    void Awake()
    {

        
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (List<Objective> list in QuestList.Values) //add initial objectives
        {
            if (list.Count > 0)
            {
                foreach (Objective i in list) //search the questlist for the first objective of each initial quest
                {
                    if (i.ID == 1) player.addObjective(i);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void createList()
    {

    }

    public void AddObjective(Objective o)
    {
        if (QuestList == null)
        {
            QuestList = new Dictionary<string, List<Objective>>();
            currentObjectiveList = new Dictionary<string, int>();
        }

        if (!QuestList.ContainsKey(o.questLine)) //if quest doesn't exist, create it
        {
            List<Objective> newQuest = new List<Objective>();
            newQuest.Add(o);

            QuestList.Add(o.questLine, newQuest);

            int currentQuestObjective = 1;
            currentObjectiveList.Add(o.questLine, currentQuestObjective);
        }
        else //else add new objective to quest
        {
            if (!QuestList[o.questLine].Contains(o)) QuestList[o.questLine].Add(o);
        }

    }

    public int getCurrentQuestObjective(Objective o)
    {
        return currentObjectiveList[o.questLine];
    }

    public void ObjectiveDone(Objective o)
    {
        QuestList[o.questLine].Remove(o);

        if (QuestList[o.questLine].Count > 0)
        {
            currentObjectiveList[o.questLine] = o.ID + 1; 

            player.addObjective(getNextObjective(o));

        }
        else
        {
            player.RemoveObjective(o);
        }


    }

    private Objective getNextObjective(Objective o)
    {
        Objective newObjective = null;

        List<Objective> objectives = QuestList[o.questLine];
        foreach (var i in objectives)
        {
            if (i.ID == currentObjectiveList[o.questLine]) newObjective = i;
        }

        if (newObjective == null) throw new System.Exception($"new Objective after objective {o.ID} could not be found");
        return newObjective;
    }


}
