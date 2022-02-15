using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevelTriggerable : Triggerable
{
    [SerializeField]
    string nextScene = "MainMenu";

    [SerializeField] private DialogManager dialogManager;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Activate()
    {
        base.Activate();

        StartCoroutine(changeLevel());
    }

    IEnumerator changeLevel()
    {
        if (AppManager.MissionStatus == AppManager.EMissionStatus.none || AppManager.MissionStatus == AppManager.EMissionStatus.partial) { dialogManager.RunSpeech(8,1); }
        else dialogManager.RunSpeech(19,1);
        
        yield return new WaitForSeconds(5f);
        
        SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
    }
}
