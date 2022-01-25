using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveController : MonoBehaviour
{
    [SerializeField] private ObjectivesHud objectivesHud;
    
    private Dictionary<string, Objective> currentObjectives;
    
    public void AddObjective(Objective o)
        {            
            if (currentObjectives == null)
            {
                currentObjectives = new Dictionary<string, Objective>();
            }
    
            if (!currentObjectives.ContainsKey(o.questLine)) //if questline doesn't exist
            {
                currentObjectives.Add(o.questLine, o); //add it
            }
            else
            {
                currentObjectives[o.questLine] = o; //else change the objective to the new one
            }
    
            objectivesHud.LoadString(o.description);
            Debug.Log($"new Objective: {o.description}");
        }
    
        public void RemoveObjective(Objective o)
        {
            ObjectiveDone(o);
            currentObjectives.Remove(o.questLine);
            Debug.Log($"Completed Objective: {o.description}");
        }

        public void ObjectiveDone(Objective o)
        {
            objectivesHud.DeleteString(o.description);
        }
       
        public Objective GetObjective(Objective o) //get questline objective
        {
            return currentObjectives.ContainsKey(o.questLine) ? currentObjectives[o.questLine] : null;
        }
}
