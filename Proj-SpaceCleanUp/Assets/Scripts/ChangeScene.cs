using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private string sceneName;
    
    void Start() { }
    void Update() { }

    public void Change()
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
