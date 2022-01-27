using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevelTriggerable : Triggerable
{
    [SerializeField]
    string nextScene = "MainMenu";
     
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
        yield return new WaitForSeconds(5f);


        SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
    }
}
